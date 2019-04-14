/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2019/01/14
 * Time: 7:51
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace namaichi
{
	partial class SmallPopupForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SmallPopupForm));
			this.descryptionLabel = new System.Windows.Forms.Label();
			this.thumbnailPictureBox = new System.Windows.Forms.PictureBox();
			this.titleLabel = new System.Windows.Forms.LinkLabel();
			this.communityNameLabel = new System.Windows.Forms.Label();
			this.hostNameLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.thumbnailPictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// descryptionLabel
			// 
			this.descryptionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.descryptionLabel.Location = new System.Drawing.Point(73, 32);
			this.descryptionLabel.Name = "descryptionLabel";
			this.descryptionLabel.Size = new System.Drawing.Size(42, 43);
			this.descryptionLabel.TabIndex = 5;
			this.descryptionLabel.Click += new System.EventHandler(this.allClick);
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
			this.thumbnailPictureBox.TabIndex = 4;
			this.thumbnailPictureBox.TabStop = false;
			this.thumbnailPictureBox.Click += new System.EventHandler(this.allClick);
			// 
			// titleLabel
			// 
			this.titleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.titleLabel.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.titleLabel.LinkColor = System.Drawing.SystemColors.ControlText;
			this.titleLabel.Location = new System.Drawing.Point(0, 0);
			this.titleLabel.MaximumSize = new System.Drawing.Size(330, 30);
			this.titleLabel.Name = "titleLabel";
			this.titleLabel.Size = new System.Drawing.Size(115, 30);
			this.titleLabel.TabIndex = 3;
			this.titleLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.TitleLabelLinkClicked);
			this.titleLabel.Click += new System.EventHandler(this.allClick);
			// 
			// communityNameLabel
			// 
			this.communityNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.communityNameLabel.Location = new System.Drawing.Point(118, 99);
			this.communityNameLabel.Name = "communityNameLabel";
			this.communityNameLabel.Size = new System.Drawing.Size(0, 18);
			this.communityNameLabel.TabIndex = 7;
			this.communityNameLabel.Text = "official";
			this.communityNameLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// hostNameLabel
			// 
			this.hostNameLabel.Location = new System.Drawing.Point(1, 99);
			this.hostNameLabel.Name = "hostNameLabel";
			this.hostNameLabel.Size = new System.Drawing.Size(150, 18);
			this.hostNameLabel.TabIndex = 6;
			this.hostNameLabel.Text = "公式生放送";
			// 
			// SmallPopupForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(116, 94);
			this.ContextMenuStrip = null;
			this.Controls.Add(this.communityNameLabel);
			this.Controls.Add(this.hostNameLabel);
			this.Controls.Add(this.descryptionLabel);
			this.Controls.Add(this.thumbnailPictureBox);
			this.Controls.Add(this.titleLabel);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(132, 38);
			this.Name = "SmallPopupForm";
			this.Opacity = 0.9D;
			this.ShowIcon = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "公式生放送";
			this.TopMost = true;
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SmallPopupFormFormClosed);
			this.Load += new System.EventHandler(this.SmallPopupFormLoad);
			this.Click += new System.EventHandler(this.allClick);
			((System.ComponentModel.ISupportInitialize)(this.thumbnailPictureBox)).EndInit();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Label hostNameLabel;
		private System.Windows.Forms.Label communityNameLabel;
		private System.Windows.Forms.LinkLabel titleLabel;
		private System.Windows.Forms.PictureBox thumbnailPictureBox;
		private System.Windows.Forms.Label descryptionLabel;
	}
}
