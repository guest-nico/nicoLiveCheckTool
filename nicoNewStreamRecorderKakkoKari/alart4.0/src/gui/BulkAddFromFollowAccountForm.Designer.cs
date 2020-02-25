/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2019/03/31
 * Time: 1:25
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace namaichi
{
	partial class BulkAddFromFollowAccountForm
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.mailText = new System.Windows.Forms.TextBox();
			this.passText = new System.Windows.Forms.TextBox();
			this.cancelBtn = new System.Windows.Forms.Button();
			this.okBtn = new System.Windows.Forms.Button();
			this.channelChkBox = new System.Windows.Forms.CheckBox();
			this.comChkBox = new System.Windows.Forms.CheckBox();
			this.userChkBox = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(85, 19);
			this.label1.TabIndex = 0;
			this.label1.Text = "メールアドレス：";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 55);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(85, 19);
			this.label2.TabIndex = 0;
			this.label2.Text = "パスワード：";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// mailText
			// 
			this.mailText.Location = new System.Drawing.Point(103, 21);
			this.mailText.Name = "mailText";
			this.mailText.Size = new System.Drawing.Size(317, 19);
			this.mailText.TabIndex = 0;
			// 
			// passText
			// 
			this.passText.Location = new System.Drawing.Point(103, 52);
			this.passText.Name = "passText";
			this.passText.Size = new System.Drawing.Size(317, 19);
			this.passText.TabIndex = 1;
			// 
			// cancelBtn
			// 
			this.cancelBtn.Location = new System.Drawing.Point(345, 88);
			this.cancelBtn.Name = "cancelBtn";
			this.cancelBtn.Size = new System.Drawing.Size(75, 23);
			this.cancelBtn.TabIndex = 6;
			this.cancelBtn.Text = "キャンセル";
			this.cancelBtn.UseVisualStyleBackColor = true;
			this.cancelBtn.Click += new System.EventHandler(this.CancelBtnClick);
			// 
			// okBtn
			// 
			this.okBtn.Location = new System.Drawing.Point(264, 88);
			this.okBtn.Name = "okBtn";
			this.okBtn.Size = new System.Drawing.Size(75, 23);
			this.okBtn.TabIndex = 5;
			this.okBtn.Text = "OK";
			this.okBtn.UseVisualStyleBackColor = true;
			this.okBtn.Click += new System.EventHandler(this.OkBtnClick);
			// 
			// channelChkBox
			// 
			this.channelChkBox.Checked = true;
			this.channelChkBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.channelChkBox.Location = new System.Drawing.Point(181, 88);
			this.channelChkBox.Name = "channelChkBox";
			this.channelChkBox.Size = new System.Drawing.Size(72, 19);
			this.channelChkBox.TabIndex = 4;
			this.channelChkBox.Text = "チャンネル";
			this.channelChkBox.UseVisualStyleBackColor = true;
			// 
			// comChkBox
			// 
			this.comChkBox.Checked = true;
			this.comChkBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.comChkBox.Location = new System.Drawing.Point(103, 88);
			this.comChkBox.Name = "comChkBox";
			this.comChkBox.Size = new System.Drawing.Size(72, 19);
			this.comChkBox.TabIndex = 3;
			this.comChkBox.Text = "コミュニティ";
			this.comChkBox.UseVisualStyleBackColor = true;
			// 
			// userChkBox
			// 
			this.userChkBox.Checked = true;
			this.userChkBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.userChkBox.Location = new System.Drawing.Point(25, 88);
			this.userChkBox.Name = "userChkBox";
			this.userChkBox.Size = new System.Drawing.Size(72, 19);
			this.userChkBox.TabIndex = 2;
			this.userChkBox.Text = "ユーザー";
			this.userChkBox.UseVisualStyleBackColor = true;
			// 
			// BulkAddFromFollowAccountForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(440, 129);
			this.Controls.Add(this.userChkBox);
			this.Controls.Add(this.comChkBox);
			this.Controls.Add(this.channelChkBox);
			this.Controls.Add(this.cancelBtn);
			this.Controls.Add(this.okBtn);
			this.Controls.Add(this.passText);
			this.Controls.Add(this.mailText);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "BulkAddFromFollowAccountForm";
			this.ShowIcon = false;
			this.Text = "参加コミュを登録したいニコニコ動画アカウントの入力";
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.CheckBox userChkBox;
		private System.Windows.Forms.CheckBox comChkBox;
		private System.Windows.Forms.CheckBox channelChkBox;
		private System.Windows.Forms.Button okBtn;
		private System.Windows.Forms.Button cancelBtn;
		private System.Windows.Forms.TextBox passText;
		private System.Windows.Forms.TextBox mailText;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
	}
}
