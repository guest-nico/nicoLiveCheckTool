﻿using System;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2019/01/14
 * Time: 3:35
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace namaichi
{
	partial class PopupForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopupForm));
			this.titleLabel = new System.Windows.Forms.LinkLabel();
			this.thumbnailPictureBox = new System.Windows.Forms.PictureBox();
			this.descryptionLabel = new System.Windows.Forms.Label();
			this.hostNameLabel = new System.Windows.Forms.Label();
			this.communityNameLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.thumbnailPictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// titleLabel
			// 
			this.titleLabel.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.titleLabel.LinkColor = System.Drawing.SystemColors.ControlText;
			this.titleLabel.Location = new System.Drawing.Point(0, 0);
			this.titleLabel.MaximumSize = new System.Drawing.Size(330, 30);
			this.titleLabel.Name = "titleLabel";
			this.titleLabel.Size = new System.Drawing.Size(325, 30);
			this.titleLabel.TabIndex = 0;
			this.titleLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.TitleLabelLinkClicked);
			// 
			// thumbnailPictureBox
			// 
			this.thumbnailPictureBox.Cursor = System.Windows.Forms.Cursors.Hand;
			this.thumbnailPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("thumbnailPictureBox.Image")));
			this.thumbnailPictureBox.InitialImage = ((System.Drawing.Image)(resources.GetObject("thumbnailPictureBox.InitialImage")));
			this.thumbnailPictureBox.Location = new System.Drawing.Point(3, 32);
			this.thumbnailPictureBox.Name = "thumbnailPictureBox";
			this.thumbnailPictureBox.Size = new System.Drawing.Size(64, 64);
			this.thumbnailPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.thumbnailPictureBox.TabIndex = 1;
			this.thumbnailPictureBox.TabStop = false;
			this.thumbnailPictureBox.Click += new System.EventHandler(this.allClick);
			// 
			// descryptionLabel
			// 
			this.descryptionLabel.Location = new System.Drawing.Point(73, 32);
			this.descryptionLabel.Name = "descryptionLabel";
			this.descryptionLabel.Size = new System.Drawing.Size(252, 64);
			this.descryptionLabel.TabIndex = 2;
			this.descryptionLabel.Text = "弾幕歓迎！荒らすの現金、あ、厳禁(。-`ω´-)ｂlt;br /gt;lt;br /gt;リアルな知り合い以外はコテハン呼び捨てにしますのでご了承を。lt;br " +
			"/gt;ltbr /gt;いらっしゃいませ。";
			this.descryptionLabel.Click += new System.EventHandler(this.allClick);
			// 
			// hostNameLabel
			// 
			this.hostNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.hostNameLabel.Location = new System.Drawing.Point(1, 99);
			this.hostNameLabel.Name = "hostNameLabel";
			this.hostNameLabel.Size = new System.Drawing.Size(101, 18);
			this.hostNameLabel.TabIndex = 3;
			this.hostNameLabel.Text = "公式生放送";
			this.hostNameLabel.Click += new System.EventHandler(this.allClick);
			// 
			// communityNameLabel
			// 
			this.communityNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.communityNameLabel.Location = new System.Drawing.Point(108, 99);
			this.communityNameLabel.Name = "communityNameLabel";
			this.communityNameLabel.Size = new System.Drawing.Size(216, 18);
			this.communityNameLabel.TabIndex = 3;
			this.communityNameLabel.Text = "official";
			this.communityNameLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.communityNameLabel.Click += new System.EventHandler(this.allClick);
			// 
			// PopupForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(332, 113);
			this.Controls.Add(this.communityNameLabel);
			this.Controls.Add(this.hostNameLabel);
			this.Controls.Add(this.descryptionLabel);
			this.Controls.Add(this.thumbnailPictureBox);
			this.Controls.Add(this.titleLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PopupForm";
			this.Opacity = 0.9D;
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "ニコ生放送開始の通知";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PopupFormFormClosed);
			this.Load += new System.EventHandler(this.PopupFormLoad);
			this.Click += new System.EventHandler(this.allClick);
			((System.ComponentModel.ISupportInitialize)(this.thumbnailPictureBox)).EndInit();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Label communityNameLabel;
		private System.Windows.Forms.Label hostNameLabel;
		private System.Windows.Forms.Label descryptionLabel;
		private System.Windows.Forms.PictureBox thumbnailPictureBox;
		private System.Windows.Forms.LinkLabel titleLabel;
	}
	
}
