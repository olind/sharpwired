/*
 * SecureSocket.cs 
 * Created by Ola Lindberg, 2006-06-20
 * 
 * SharpWired - a Wired client.
 * See: http://www.zankasoftware.com/wired/ for more infromation about Wired
 * 
 * Copyright (C) Ola Lindberg (http://olalindberg.com)
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

namespace SharpWired.Connection.Sockets
{
    /// <summary>
    /// This class handels all socket connections.
    /// 
    /// NOTE: This class has derived from the Socio Project. See http://socio.sf.net/
    /// </summary>
    public class SecureSocket
    {
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
        private int buffer_size = 2048;

        /// <summary>
        /// Default transmission parameters. Only used internally
        /// </summary>
        protected static readonly int BUFFER_BLOCK_SIZE = 256;	// The number of bytes to receive in every block

        /// <summary>
        /// A delegate type for hooking up message received notifications.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="message"></param>
        public delegate void MessageReceivedHandler(object sender, EventArgs e, string message);
        /// <summary>
        /// Message raised when a message is received from the server
        /// </summary>
        public event MessageReceivedHandler MessageReceived;

        /// <summary>
        /// Constructor
        /// </summary>
        public SecureSocket()
        {
        }

        /// <summary>
        /// Connects the client to the server.
        /// </summary>
        /// <param name="serverPort">The port for the server to use for this connection</param>
        /// <param name="machineName">The host running the server application</param>
        /// <param name="serverName">The machine name for the server, must match the machine name in the server certificate</param>
        private void Connect(int serverPort, string machineName, string serverName)
        {

            // Create a TCP/IP client socket.
            // Set up a temporary connection that is unencrypted, used to transfer the certificates?
            try
            {
                client = new TcpClient(machineName, serverPort);
                Console.WriteLine("Client connected.");
            }
            catch (ArgumentNullException argExp)
            {
                throw new ConnectionException("Host or machine name is null", argExp);
            }
            catch (ArgumentOutOfRangeException argORExp)
            {
                throw new ConnectionException ("The Port is incorrect", argORExp);
            }
            catch (SocketException argSExp)
            {
                //argSExp.SocketErrorCode == HostNotFound
                throw new ConnectionException("There is a problem with the Socket", argSExp );
            }

            // Create an SSL stream that will close the client's stream.
            //FIXME: The validate server certificate allways returns true
            //       If the validation fails we should ask the user to connect anyway
            sslStream = new SslStream(client.GetStream(), 
                false, 
                new RemoteCertificateValidationCallback(ValidateServerCertificate), 
                null);

            // The server name must match the name on the server certificate.
            try
            {
                sslStream.AuthenticateAsClient(serverName);
            }
            catch (ArgumentNullException argExp)
            {
                throw new ConnectionException("Target host is null", argExp);
            }
            catch (AuthenticationException argExp)
            {
                throw new ConnectionException("The authentication failed and left this object in an unusable state.", argExp ); 
            }
            catch (InvalidOperationException argExp)
            {
                throw new ConnectionException ("Authentication has already occurred or Server authentication using this SslStream was tried previously org Authentication is already in progress.", argExp);
            }

            // When we are connected we can now set up our receive mechanism
            byte[] readBuffer = new byte[buffer_size];
            sslStream.BeginRead(readBuffer, 0, readBuffer.Length, new AsyncCallback(ReadCallback), readBuffer);
        }

        /// <summary>
        /// Send a message to the server.
        /// </summary>
        /// <param name="message">The message to be sent (without any EOT).</param>
        public void SendMessage(string message) {
            if (sslStream != null)
            {
                byte[] messsage = Encoding.UTF8.GetBytes(message + Utility.EOT);
                sslStream.Write(messsage);
                sslStream.Flush();
            }
        }

        /// <summary>
        /// Disconnect this connection
        /// </summary>
        public void Disconnect()
        {
             // Close the client connection.
            client.Close();
        }

        /// <summary>
        /// Verifies the remote Secure Sockets Layer (SSL) certificate used for authentication.
        /// </summary>
        /// <param name="sender">An object that contains state information for this validation.</param>
        /// <param name="certificate">The certificate used to authenticate the remote party.</param>
        /// <param name="chain">The chain of certificate authorities associated with the remote certificate.</param>
        /// <param name="sslPolicyErrors">One or more errors associated with the remote certificate.</param>
        /// <returns>Returns true all the time, shoulh be: True if the certificate is valid, false otherwise</returns>
        private bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;

            // FIXME: We should trow an exception if the validate is not valid, 
            //        for now return true anyway
            return true;
        }

        /// <summary>
        /// The read callback acts as the asynchronous message receive loop.
        /// Note: This code is inspired from Socio (see: socio.sf.net for more information)
        /// </summary>
        /// <param name="result">TODO: !!</param>
        private void ReadCallback(IAsyncResult result)
        {
            sslStream.EndRead(result);
            string data_received = Encoding.UTF8.GetString((byte[])result.AsyncState);

            string msg;
            int index_EOT;
            System.Text.RegularExpressions.Regex eot_regex = new System.Text.RegularExpressions.Regex("[" + Utility.EOT + "]");
            while (eot_regex.IsMatch(data_received))
            {
                // Are there more than one EOTs?
                index_EOT = data_received.IndexOf(Utility.EOT);
                msg = data_received.Substring(0, index_EOT);
                data_received = data_received.Remove(0, index_EOT + 1);
                MessageReceived(this, null, msg);
            }

            System.Text.RegularExpressions.Regex alpha_numeric = new System.Text.RegularExpressions.Regex("[a-zA-Z0-9]");
            if (alpha_numeric.IsMatch(data_received))
            {
                // Extend the buffer and continue reading until we receive a complete message
                byte[] saved_bytes = Encoding.UTF8.GetBytes(data_received);
                byte[] read_buffer = new byte[buffer_size + saved_bytes.Length];
                Array.Copy(saved_bytes, read_buffer, saved_bytes.Length);
                sslStream.BeginRead(read_buffer, saved_bytes.Length, buffer_size, new AsyncCallback(ReadCallback), read_buffer);
            }
            else
            {
				// What do we do here?!
                byte[] read_buffer = new byte[buffer_size];
                sslStream.BeginRead(read_buffer, 0, read_buffer.Length, new AsyncCallback(ReadCallback), read_buffer);
            }
        }


		/// <summary>
		/// Connects to the server using Connect(Port, MachineName, ServerName).
		/// </summary>
		/// <param name="server">The Server to connect to.</param>
        internal void Connect(Server server)
        {
            Connect(server.ServerPort, server.MachineName, server.ServerName);
        }
    }
}