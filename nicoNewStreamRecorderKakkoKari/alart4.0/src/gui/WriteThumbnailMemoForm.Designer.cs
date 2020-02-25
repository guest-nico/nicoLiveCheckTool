/*
 * Created by SharpDevelop.
 * User: user
 * Date: 2019/03/22
 * Time: 0:57
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace namaichi
{
	partial class WriteThumbnailMemoForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WriteThumbnailMemoForm));
			this.thumbBox = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.xList = new System.Windows.Forms.NumericUpDown();
			this.yList = new System.Windows.Forms.NumericUpDown();
			this.label5 = new System.Windows.Forms.Label();
			this.sizeList = new System.Windows.Forms.NumericUpDown();
			this.text = new System.Windows.Forms.TextBox();
			this.okBtn = new System.Windows.Forms.Button();
			this.cancelBtn = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.thumbBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.xList)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.yList)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.sizeList)).BeginInit();
			this.SuspendLayout();
			// 
			// thumbBox
			// 
			this.thumbBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("thumbBox.BackgroundImage")));
			this.thumbBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.thumbBox.Location = new System.Drawing.Point(15, 32);
			this.thumbBox.Name = "thumbBox";
			this.thumbBox.Size = new System.Drawing.Size(64, 64);
			this.thumbBox.TabIndex = 0;
			this.thumbBox.TabStop = false;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(81, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(71, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "文字の位置";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(81, 43);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(71, 16);
			this.label2.TabIndex = 1;
			this.label2.Text = "文字の大きさ";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(81, 76);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(71, 16);
			this.label3.TabIndex = 1;
			this.label3.Text = "文字";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(148, 13);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(25, 16);
			this.label4.TabIndex = 2;
			this.label4.Text = "X：";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// xList
			// 
			this.xList.Location = new System.Drawing.Point(177, 10);
			this.xList.Maximum = new decimal(new int[] {
									128,
									0,
									0,
									0});
			this.xList.Name = "xList";
			this.xList.Size = new System.Drawing.Size(54, 19);
			this.xList.TabIndex = 3;
			this.xList.ValueChanged += new System.EventHandler(this.TextEvent);
			// 
			// yList
			// 
			this.yList.Location = new System.Drawing.Point(270, 10);
			this.yList.Maximum = new decimal(new int[] {
									128,
									0,
									0,
									0});
			this.yList.Name = "yList";
			this.yList.Size = new System.Drawing.Size(54, 19);
			this.yList.TabIndex = 5;
			this.yList.ValueChanged += new System.EventHandler(this.TextEvent);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(242, 13);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(25, 16);
			this.label5.TabIndex = 4;
			this.label5.Text = "Y：";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// sizeList
			// 
			this.sizeList.Location = new System.Drawing.Point(158, 41);
			this.sizeList.Maximum = new decimal(new int[] {
									128,
									0,
									0,
									0});
			this.sizeList.Minimum = new decimal(new int[] {
									1,
									0,
									0,
									0});
			this.sizeList.Name = "sizeList";
			this.sizeList.Size = new System.Drawing.Size(54, 19);
			this.sizeList.TabIndex = 6;
			this.sizeList.Value = new decimal(new int[] {
									22,
									0,
									0,
									0});
			this.sizeList.ValueChanged += new System.EventHandler(this.TextEvent);
			// 
			// text
			// 
			this.text.Location = new System.Drawing.Point(158, 73);
			this.text.Name = "text";
			this.text.Size = new System.Drawing.Size(84, 19);
			this.text.TabIndex = 7;
			this.text.TextChanged += new System.EventHandler(this.TextEvent);
			// 
			// okBtn
			// 
			this.okBtn.Location = new System.Drawing.Point(177, 115);
			this.okBtn.Name = "okBtn";
			this.okBtn.Size = new System.Drawing.Size(75, 23);
			this.okBtn.TabIndex = 8;
			this.okBtn.Text = "OK";
			this.okBtn.UseVisualStyleBackColor = true;
			this.okBtn.Click += new System.EventHandler(this.OkBtnClick);
			// 
			// cancelBtn
			// 
			this.cancelBtn.Location = new System.Drawing.Point(258, 115);
			this.cancelBtn.Name = "cancelBtn";
			this.cancelBtn.Size = new System.Drawing.Size(75, 23);
			this.cancelBtn.TabIndex = 8;
			this.cancelBtn.Text = "キャンセル";
			this.cancelBtn.UseVisualStyleBackColor = true;
			this.cancelBtn.Click += new System.EventHandler(this.CancelBtnClick);
			// 
			// WriteThumbnailMemoForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(344, 150);
			this.Controls.Add(this.cancelBtn);
			this.Controls.Add(this.okBtn);
			this.Controls.Add(this.text);
			this.Controls.Add(this.sizeList);
			this.Controls.Add(this.yList);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.xList);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.thumbBox);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "WriteThumbnailMemoForm";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "サムネにメモを書く";
			((System.ComponentModel.ISupportInitialize)(this.thumbBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.xList)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.yList)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.sizeList)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.Button cancelBtn;
		private System.Windows.Forms.Button okBtn;
		private System.Windows.Forms.TextBox text;
		private System.Windows.Forms.NumericUpDown sizeList;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.NumericUpDown yList;
		private System.Windows.Forms.NumericUpDown xList;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox thumbBox;
	}
}
