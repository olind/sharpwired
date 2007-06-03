using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace WiredControls.ToolStripItems
{
	public class AnimatedLoaderItem: ToolStripMenuItem
	{
		public AnimatedLoaderItem(string text):this()
		{
			this.Text = text;
		}

		public AnimatedLoaderItem()
		{
			InitializeComponent();
			LoadImages();
			
		}

		private Timer imageChangeTimer;
		private System.ComponentModel.IContainer components;

		Bitmap[] images = new Bitmap[4];

		private void LoadImages()
		{
			images[0] = AnimatedLoaderItemResources._1_3;
			images[1] = AnimatedLoaderItemResources._3_6;
			images[2] = AnimatedLoaderItemResources._6_9;
			images[3] = AnimatedLoaderItemResources._9_12;
		}

		private volatile int mImageIndex;

		public new int ImageIndex
		{
			get { return mImageIndex; }
			set
			{
				if (mImageIndex != value)
				{
					mImageIndex = value;
					mImageIndex %= images.Length;
					SetImage();
				}
			}
		}

		private void SetImage()
		{
			this.Image = images[mImageIndex];
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.imageChangeTimer = new System.Windows.Forms.Timer(this.components);
			// 
			// imageChangeTimer
			// 
			this.imageChangeTimer.Tick += new System.EventHandler(this.imageChangeTimer_Tick);

		}

		private void imageChangeTimer_Tick(object sender, EventArgs e)
		{
			ImageIndex += 1;
		}

		public void Start()
		{
			ImageIndex = 0;
			imageChangeTimer.Start();
		}

		public void Stop()
		{
			imageChangeTimer.Stop();
			Image = null;
		}
	}
}