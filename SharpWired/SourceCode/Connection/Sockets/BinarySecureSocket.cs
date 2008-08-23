/*
 * SecureSocket.cs 
 * Created by Ola Lindberg, 2006-06-20
 * 
 * SharpWired - a Wired client.
 * See: http://www.zankasoftware.com/wired/ for more infromation about Wired
 * 
 * Copyright (C) Ola Lindberg (http://olalindberg.com) & Adam Lindberg (http://namsisi.com)
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301 USA
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Net;
using System.Net.Sockets;
using System.Security.Authentication;

using SharpWired;
using System.IO;
using System.Diagnostics;

namespace SharpWired.Connection.Sockets {
    /// <summary>
    /// This class handels all socket connections.
    /// 
    /// NOTE: This class has derived from the Socio Project. See http://socio.sf.net/
    /// </summary>
    public class BinarySecureSocket {
        /// <summary>
        /// Used to create the SSL Stream.
        /// </summary>
        TcpClient client;
        /// <summary>
        ///  The secure connection to the server.
        /// </summary>
        SslStream sslStream;

        /// <summary>
        /// The default size of the buffer to use
        /// </summary>
        private static int BUFFER_SIZE = 2048;

        /// <summary>
        /// Default transmission parameters. Only used internally
        /// </summary>
        protected static readonly int BUFFER_BLOCK_SIZE = 512;	// The number of bytes to receive in every block

        private Int64 bytesTransferred;

        public Int64 BytesTransferred {
            get { return bytesTransferred; }
        }

        /// <summary>
        /// A delegate type for hooking up message received notifications.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="data"></param>
        public delegate void BinaryMessageReceivedHandler(object sender, EventArgs e, byte[] data);

        #region Connect

        internal void Start() { }

        /// <summary>
        /// Connects to the server using Connect(Port, MachineName, ServerName).
        /// </summary>
        /// <param name="server">The Server to connect to.</param>
        /// <param name="stream"></param>
        /// <param name="fileSize"></param>
        /// <param name="offset"></param>
        internal void Connect(Server server, FileStream stream, long fileSize, long offset) {
            Connect(server.ServerPort, server.MachineName, server.ServerName, stream, fileSize, offset);
        }

        /// <summary>
        /// Connects the client to the server.
        /// </summary>
        /// <param name="serverPort">The port for the server to use for this connection</param>
        /// <param name="machineName">The host running the server application</param>
        /// <param name="serverName">The machine name for the server, must match the machine name in the server certificate</param>
        /// <param name="stream"></param>
        /// <param name="fileSize"></param>
        /// <param name="offset"></param>
        private void Connect(int serverPort, string machineName, string serverName, FileStream stream, long fileSize, long offset) {

            // Create a TCP/IP client socket.
            // Set up a temporary connection that is unencrypted, used to transfer the certificates?
            try {
                client = new TcpClient(machineName, serverPort);
                Debug.WriteLine("Client connected.");
            } catch (ArgumentNullException argExp) {
                throw new ConnectionException("Host or machine name is null", argExp);
            } catch (ArgumentOutOfRangeException argORExp) {
                throw new ConnectionException("The Port is incorrect", argORExp);
            } catch (SocketException argSExp) {
                //argSExp.SocketErrorCode == HostNotFound
                throw new ConnectionException("There is a problem with the Socket", argSExp);
            }

            // Create an SSL stream that will close the client's stream.
            //TODO: The validate server certificate allways returns true
            //      If the validation fails we should ask the user to connect anyway
            sslStream = new SslStream(client.GetStream(),
                false,
                new RemoteCertificateValidationCallback(ValidateServerCertificate),
                null);

            // The server name must match the name on the server certificate.
            try {
                sslStream.AuthenticateAsClient(serverName);
            } catch (ArgumentNullException argExp) {
                throw new ConnectionException("Target host is null", argExp);
            } catch (AuthenticationException argExp) {
                throw new ConnectionException("The authentication failed and left this object in an unusable state.", argExp);
            } catch (InvalidOperationException argExp) {
                throw new ConnectionException("Authentication has already occurred or Server authentication using this SslStream was tried previously org Authentication is already in progress.", argExp);
            }

            // When we are connected we can now set up our receive mechanism
            byte[] readBuffer = new byte[BUFFER_SIZE];
            FileTransferStateObject stateObj = new FileTransferStateObject();
            stateObj.fileSize = fileSize;
            stateObj.sslStream = sslStream;
            stateObj.target = stream;
            stateObj.transferBuffer = readBuffer;
            stateObj.transferOffset = offset;

            sslStream.BeginRead(readBuffer, 0, readBuffer.Length, new AsyncCallback(ReadCallback), stateObj);
        }

        /// <summary>
        /// Verifies the remote Secure Sockets Layer (SSL) certificate used for authentication.
        /// </summary>
        /// <param name="sender">An object that contains state information for this validation.</param>
        /// <param name="certificate">The certificate used to authenticate the remote party.</param>
        /// <param name="chain">The chain of certificate authorities associated with the remote certificate.</param>
        /// <param name="sslPolicyErrors">One or more errors associated with the remote certificate.</param>
        /// <returns>Returns true all the time, shoulh be: True if the certificate is valid, false otherwise</returns>
        private bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) {
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;

            // TODO: We should trow an exception if the validate is not valid, 
            //       for now return true anyway
            return true;
        }
        #endregion


        #region Send Message

        /// <summary>
        /// Send a message to the server.
        /// </summary>
        /// <param name="message">The message to be sent (without any EOT).</param>
        public void SendMessage(string message) {
            if (sslStream != null) {
                byte[] messsage = Encoding.UTF8.GetBytes(message + Utility.EOT);
                sslStream.Write(messsage);
                sslStream.Flush();
            }
        }
        #endregion


        /// <summary>
        /// Disconnect this connection
        /// </summary>
        public void Disconnect() {
            // Close the client connection.
            client.Close();
        }


        /// <summary>
        /// The read callback acts as the asynchronous message receive loop.
        /// Note: This code is inspired from Socio (see: socio.sf.net for more information)
        /// </summary>
        /// <param name="result">The result that the socket received</param>
        private void ReadCallback(IAsyncResult result) {
            FileTransferStateObject trans = (FileTransferStateObject)result.AsyncState;
            int bytesRead = trans.sslStream.EndRead(result);

            if (bytesRead > 0) // Check if there is any data
			{
                trans.transferOffset += bytesRead;
                bytesTransferred += bytesRead;

                // Synchronosly write data to file...
                // Make sure that when filestream is created it is created so
                // that data written to it appends to the file...
                trans.target.Write(trans.transferBuffer, 0, bytesRead);

                // Transfer might not be complete
                trans.transferBuffer = new byte[BUFFER_SIZE];
                IAsyncResult r = trans.sslStream.BeginRead(trans.transferBuffer, 0, BUFFER_SIZE,
                    new AsyncCallback(this.ReadCallback), trans);
            } else {
                // All data has been received close ssl connection
                //trans.sslStream.Shutdown();
                trans.sslStream.Close();

                // Close fileStream
                trans.target.Close();

                if (DataReceivedDoneEvent != null)
                    DataReceivedDoneEvent();
            }
        }

        /// <summary>
        /// Event for telling when received data is done
        /// </summary>
        public event DataReceivedDoneDelegate DataReceivedDoneEvent;
        public delegate void DataReceivedDoneDelegate();

        //FileTransfer classen år tänkt att skickas in som state objekt för överföringen
        internal class FileTransferStateObject {
            internal long fileSize; // Number of bytes in the file
            internal long transferOffset; // Number of bytes transfered
            internal byte[] transferBuffer = new byte[BUFFER_SIZE]; // A byte buffer for the transfer.
            internal FileStream target; //The file beng transfered
            internal SslStream sslStream;
        }
    }
}
