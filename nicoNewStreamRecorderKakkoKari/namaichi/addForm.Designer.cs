/*
 * Created by SharpDevelop.
 * User: user
 * Date: 2019/01/04
 * Time: 7:23
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace namaichi
{
	partial class addForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(addForm));
			this.button4 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.communityId = new System.Windows.Forms.TextBox();
			this.communityNameText = new System.Windows.Forms.TextBox();
			this.getCommunityInfoBtn = new System.Windows.Forms.Button();
			this.getUserInfoBtn = new System.Windows.Forms.Button();
			this.userNameText = new System.Windows.Forms.TextBox();
			this.userIdText = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.isMailChkBox = new System.Windows.Forms.CheckBox();
			this.appliJChkBox = new System.Windows.Forms.CheckBox();
			this.appliIChkBox = new System.Windows.Forms.CheckBox();
			this.appliHChkBox = new System.Windows.Forms.CheckBox();
			this.appliGChkBox = new System.Windows.Forms.CheckBox();
			this.appliFChkBox = new System.Windows.Forms.CheckBox();
			this.appliEChkBox = new System.Windows.Forms.CheckBox();
			this.isWebChkBox = new System.Windows.Forms.CheckBox();
			this.isSoundChkBox = new System.Windows.Forms.CheckBox();
			this.isBaloonChkBox = new System.Windows.Forms.CheckBox();
			this.isPopupChkBox = new System.Windows.Forms.CheckBox();
			this.appliDChkBox = new System.Windows.Forms.CheckBox();
			this.appliCChkBox = new System.Windows.Forms.CheckBox();
			this.appliBChkBox = new System.Windows.Forms.CheckBox();
			this.appliAChkBox = new System.Windows.Forms.CheckBox();
			this.memoText = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.userFollowChkBox = new System.Windows.Forms.CheckBox();
			this.communityFollowChkBox = new System.Windows.Forms.CheckBox();
			this.keywordText = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.getInfoFromHosoIdBtn = new System.Windows.Forms.Button();
			this.hosoIdText = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.comThumbBox = new System.Windows.Forms.PictureBox();
			this.userThumbBox = new System.Windows.Forms.PictureBox();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.comThumbBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.userThumbBox)).BeginInit();
			this.SuspendLayout();
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(265, 598);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(74, 23);
			this.button4.TabIndex = 3;
			this.button4.Text = "キャンセル";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.Button4Click);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(185, 598);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(74, 23);
			this.button3.TabIndex = 2;
			this.button3.Text = "OK";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.Button3Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(28, 166);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(68, 15);
			this.label1.TabIndex = 4;
			this.label1.Text = "コミュニティID";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(28, 190);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(68, 15);
			this.label2.TabIndex = 5;
			this.label2.Text = "コミュニティ名";
			// 
			// communityId
			// 
			this.communityId.Location = new System.Drawing.Point(102, 163);
			this.communityId.Name = "communityId";
			this.communityId.Size = new System.Drawing.Size(146, 19);
			this.communityId.TabIndex = 6;
			// 
			// communityNameText
			// 
			this.communityNameText.Enabled = false;
			this.communityNameText.Location = new System.Drawing.Point(102, 187);
			this.communityNameText.Name = "communityNameText";
			this.communityNameText.Size = new System.Drawing.Size(237, 19);
			this.communityNameText.TabIndex = 6;
			// 
			// getCommunityInfoBtn
			// 
			this.getCommunityInfoBtn.Location = new System.Drawing.Point(264, 161);
			this.getCommunityInfoBtn.Name = "getCommunityInfoBtn";
			this.getCommunityInfoBtn.Size = new System.Drawing.Size(75, 23);
			this.getCommunityInfoBtn.TabIndex = 7;
			this.getCommunityInfoBtn.Text = "情報取得";
			this.getCommunityInfoBtn.UseVisualStyleBackColor = true;
			this.getCommunityInfoBtn.Click += new System.EventHandler(this.GetCommunityInfoBtnClick);
			// 
			// getUserInfoBtn
			// 
			this.getUserInfoBtn.Location = new System.Drawing.Point(264, 244);
			this.getUserInfoBtn.Name = "getUserInfoBtn";
			this.getUserInfoBtn.Size = new System.Drawing.Size(75, 23);
			this.getUserInfoBtn.TabIndex = 12;
			this.getUserInfoBtn.Text = "情報取得";
			this.getUserInfoBtn.UseVisualStyleBackColor = true;
			this.getUserInfoBtn.Click += new System.EventHandler(this.GetUserInfoBtnClick);
			// 
			// userNameText
			// 
			this.userNameText.Enabled = false;
			this.userNameText.Location = new System.Drawing.Point(102, 270);
			this.userNameText.Name = "userNameText";
			this.userNameText.Size = new System.Drawing.Size(237, 19);
			this.userNameText.TabIndex = 11;
			// 
			// userIdText
			// 
			this.userIdText.Location = new System.Drawing.Point(102, 246);
			this.userIdText.Name = "userIdText";
			this.userIdText.Size = new System.Drawing.Size(146, 19);
			this.userIdText.TabIndex = 10;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(28, 273);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(68, 15);
			this.label3.TabIndex = 9;
			this.label3.Text = "ユーザー名";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(28, 249);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(68, 15);
			this.label4.TabIndex = 8;
			this.label4.Text = "ユーザーID";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.isMailChkBox);
			this.groupBox1.Controls.Add(this.appliJChkBox);
			this.groupBox1.Controls.Add(this.appliIChkBox);
			this.groupBox1.Controls.Add(this.appliHChkBox);
			this.groupBox1.Controls.Add(this.appliGChkBox);
			this.groupBox1.Controls.Add(this.appliFChkBox);
			this.groupBox1.Controls.Add(this.appliEChkBox);
			this.groupBox1.Controls.Add(this.isWebChkBox);
			this.groupBox1.Controls.Add(this.isSoundChkBox);
			this.groupBox1.Controls.Add(this.isBaloonChkBox);
			this.groupBox1.Controls.Add(this.isPopupChkBox);
			this.groupBox1.Controls.Add(this.appliDChkBox);
			this.groupBox1.Controls.Add(this.appliCChkBox);
			this.groupBox1.Controls.Add(this.appliBChkBox);
			this.groupBox1.Controls.Add(this.appliAChkBox);
			this.groupBox1.Location = new System.Drawing.Point(28, 386);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(329, 118);
			this.groupBox1.TabIndex = 13;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "動作";
			// 
			// isMailChkBox
			// 
			this.isMailChkBox.Location = new System.Drawing.Point(255, 18);
			this.isMailChkBox.Name = "isMailChkBox";
			this.isMailChkBox.Size = new System.Drawing.Size(68, 24);
			this.isMailChkBox.TabIndex = 13;
			this.isMailChkBox.Text = "メール";
			this.isMailChkBox.UseVisualStyleBackColor = true;
			// 
			// appliJChkBox
			// 
			this.appliJChkBox.Location = new System.Drawing.Point(172, 84);
			this.appliJChkBox.Name = "appliJChkBox";
			this.appliJChkBox.Size = new System.Drawing.Size(62, 24);
			this.appliJChkBox.TabIndex = 12;
			this.appliJChkBox.Text = "アプリJ";
			this.appliJChkBox.UseVisualStyleBackColor = true;
			// 
			// appliIChkBox
			// 
			this.appliIChkBox.Location = new System.Drawing.Point(89, 84);
			this.appliIChkBox.Name = "appliIChkBox";
			this.appliIChkBox.Size = new System.Drawing.Size(62, 24);
			this.appliIChkBox.TabIndex = 11;
			this.appliIChkBox.Text = "アプリI";
			this.appliIChkBox.UseVisualStyleBackColor = true;
			// 
			// appliHChkBox
			// 
			this.appliHChkBox.Location = new System.Drawing.Point(6, 84);
			this.appliHChkBox.Name = "appliHChkBox";
			this.appliHChkBox.Size = new System.Drawing.Size(62, 24);
			this.appliHChkBox.TabIndex = 10;
			this.appliHChkBox.Text = "アプリH";
			this.appliHChkBox.UseVisualStyleBackColor = true;
			// 
			// appliGChkBox
			// 
			this.appliGChkBox.Location = new System.Drawing.Point(255, 62);
			this.appliGChkBox.Name = "appliGChkBox";
			this.appliGChkBox.Size = new System.Drawing.Size(62, 24);
			this.appliGChkBox.TabIndex = 9;
			this.appliGChkBox.Text = "アプリG";
			this.appliGChkBox.UseVisualStyleBackColor = true;
			// 
			// appliFChkBox
			// 
			this.appliFChkBox.Location = new System.Drawing.Point(172, 62);
			this.appliFChkBox.Name = "appliFChkBox";
			this.appliFChkBox.Size = new System.Drawing.Size(62, 24);
			this.appliFChkBox.TabIndex = 8;
			this.appliFChkBox.Text = "アプリF";
			this.appliFChkBox.UseVisualStyleBackColor = true;
			// 
			// appliEChkBox
			// 
			this.appliEChkBox.Location = new System.Drawing.Point(89, 62);
			this.appliEChkBox.Name = "appliEChkBox";
			this.appliEChkBox.Size = new System.Drawing.Size(62, 24);
			this.appliEChkBox.TabIndex = 7;
			this.appliEChkBox.Text = "アプリE";
			this.appliEChkBox.UseVisualStyleBackColor = true;
			// 
			// isWebChkBox
			// 
			this.isWebChkBox.Location = new System.Drawing.Point(172, 18);
			this.isWebChkBox.Name = "isWebChkBox";
			this.isWebChkBox.Size = new System.Drawing.Size(68, 24);
			this.isWebChkBox.TabIndex = 6;
			this.isWebChkBox.Text = "Web";
			this.isWebChkBox.UseVisualStyleBackColor = true;
			// 
			// isSoundChkBox
			// 
			this.isSoundChkBox.Location = new System.Drawing.Point(6, 40);
			this.isSoundChkBox.Name = "isSoundChkBox";
			this.isSoundChkBox.Size = new System.Drawing.Size(82, 24);
			this.isSoundChkBox.TabIndex = 5;
			this.isSoundChkBox.Text = "音";
			this.isSoundChkBox.UseVisualStyleBackColor = true;
			// 
			// isBaloonChkBox
			// 
			this.isBaloonChkBox.Location = new System.Drawing.Point(89, 18);
			this.isBaloonChkBox.Name = "isBaloonChkBox";
			this.isBaloonChkBox.Size = new System.Drawing.Size(82, 24);
			this.isBaloonChkBox.TabIndex = 4;
			this.isBaloonChkBox.Text = "バルーン";
			this.isBaloonChkBox.UseVisualStyleBackColor = true;
			// 
			// isPopupChkBox
			// 
			this.isPopupChkBox.Location = new System.Drawing.Point(6, 18);
			this.isPopupChkBox.Name = "isPopupChkBox";
			this.isPopupChkBox.Size = new System.Drawing.Size(82, 24);
			this.isPopupChkBox.TabIndex = 3;
			this.isPopupChkBox.Text = "ポップアップ";
			this.isPopupChkBox.UseVisualStyleBackColor = true;
			// 
			// appliDChkBox
			// 
			this.appliDChkBox.Location = new System.Drawing.Point(6, 62);
			this.appliDChkBox.Name = "appliDChkBox";
			this.appliDChkBox.Size = new System.Drawing.Size(62, 24);
			this.appliDChkBox.TabIndex = 2;
			this.appliDChkBox.Text = "アプリD";
			this.appliDChkBox.UseVisualStyleBackColor = true;
			// 
			// appliCChkBox
			// 
			this.appliCChkBox.Location = new System.Drawing.Point(255, 40);
			this.appliCChkBox.Name = "appliCChkBox";
			this.appliCChkBox.Size = new System.Drawing.Size(62, 24);
			this.appliCChkBox.TabIndex = 2;
			this.appliCChkBox.Text = "アプリC";
			this.appliCChkBox.UseVisualStyleBackColor = true;
			// 
			// appliBChkBox
			// 
			this.appliBChkBox.Location = new System.Drawing.Point(172, 40);
			this.appliBChkBox.Name = "appliBChkBox";
			this.appliBChkBox.Size = new System.Drawing.Size(62, 24);
			this.appliBChkBox.TabIndex = 1;
			this.appliBChkBox.Text = "アプリB";
			this.appliBChkBox.UseVisualStyleBackColor = true;
			// 
			// appliAChkBox
			// 
			this.appliAChkBox.Location = new System.Drawing.Point(89, 40);
			this.appliAChkBox.Name = "appliAChkBox";
			this.appliAChkBox.Size = new System.Drawing.Size(62, 24);
			this.appliAChkBox.TabIndex = 0;
			this.appliAChkBox.Text = "アプリA";
			this.appliAChkBox.UseVisualStyleBackColor = true;
			// 
			// memoText
			// 
			this.memoText.Location = new System.Drawing.Point(53, 518);
			this.memoText.Name = "memoText";
			this.memoText.Size = new System.Drawing.Size(286, 19);
			this.memoText.TabIndex = 14;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(28, 521);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(25, 21);
			this.label5.TabIndex = 15;
			this.label5.Text = "メモ";
			// 
			// userFollowChkBox
			// 
			this.userFollowChkBox.Enabled = false;
			this.userFollowChkBox.Location = new System.Drawing.Point(102, 295);
			this.userFollowChkBox.Name = "userFollowChkBox";
			this.userFollowChkBox.Size = new System.Drawing.Size(81, 15);
			this.userFollowChkBox.TabIndex = 16;
			this.userFollowChkBox.Text = "フォロー";
			this.userFollowChkBox.UseVisualStyleBackColor = true;
			// 
			// communityFollowChkBox
			// 
			this.communityFollowChkBox.Enabled = false;
			this.communityFollowChkBox.Location = new System.Drawing.Point(102, 212);
			this.communityFollowChkBox.Name = "communityFollowChkBox";
			this.communityFollowChkBox.Size = new System.Drawing.Size(81, 15);
			this.communityFollowChkBox.TabIndex = 16;
			this.communityFollowChkBox.Text = "フォロー";
			this.communityFollowChkBox.UseVisualStyleBackColor = true;
			// 
			// keywordText
			// 
			this.keywordText.Location = new System.Drawing.Point(102, 329);
			this.keywordText.Name = "keywordText";
			this.keywordText.Size = new System.Drawing.Size(237, 19);
			this.keywordText.TabIndex = 18;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(28, 332);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(68, 30);
			this.label6.TabIndex = 17;
			this.label6.Text = "キーワード";
			// 
			// getInfoFromHosoIdBtn
			// 
			this.getInfoFromHosoIdBtn.Location = new System.Drawing.Point(298, 566);
			this.getInfoFromHosoIdBtn.Name = "getInfoFromHosoIdBtn";
			this.getInfoFromHosoIdBtn.Size = new System.Drawing.Size(75, 23);
			this.getInfoFromHosoIdBtn.TabIndex = 21;
			this.getInfoFromHosoIdBtn.Text = "情報取得";
			this.getInfoFromHosoIdBtn.UseVisualStyleBackColor = true;
			this.getInfoFromHosoIdBtn.Click += new System.EventHandler(this.GetInfoFromHosoIdBtnClick);
			// 
			// hosoIdText
			// 
			this.hosoIdText.Location = new System.Drawing.Point(216, 568);
			this.hosoIdText.Name = "hosoIdText";
			this.hosoIdText.Size = new System.Drawing.Size(76, 19);
			this.hosoIdText.TabIndex = 20;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(12, 571);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(167, 15);
			this.label7.TabIndex = 19;
			this.label7.Text = "放送IDから情報を取得する・・・";
			// 
			// label8
			// 
			this.label8.Enabled = false;
			this.label8.Location = new System.Drawing.Point(163, 571);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(47, 15);
			this.label8.TabIndex = 22;
			this.label8.Text = "放送ID";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(102, 349);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(263, 34);
			this.label9.TabIndex = 23;
			this.label9.Text = "（コミュニティ名、コミュニティID、放送者名、タイトル、\r\n説明、放送ID）";
			// 
			// comThumbBox
			// 
			this.comThumbBox.Image = ((System.Drawing.Image)(resources.GetObject("comThumbBox.Image")));
			this.comThumbBox.InitialImage = ((System.Drawing.Image)(resources.GetObject("comThumbBox.InitialImage")));
			this.comThumbBox.Location = new System.Drawing.Point(46, 12);
			this.comThumbBox.Name = "comThumbBox";
			this.comThumbBox.Size = new System.Drawing.Size(128, 128);
			this.comThumbBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.comThumbBox.TabIndex = 24;
			this.comThumbBox.TabStop = false;
			// 
			// userThumbBox
			// 
			this.userThumbBox.Image = ((System.Drawing.Image)(resources.GetObject("userThumbBox.Image")));
			this.userThumbBox.InitialImage = ((System.Drawing.Image)(resources.GetObject("userThumbBox.InitialImage")));
			this.userThumbBox.Location = new System.Drawing.Point(211, 12);
			this.userThumbBox.Name = "userThumbBox";
			this.userThumbBox.Size = new System.Drawing.Size(128, 128);
			this.userThumbBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.userThumbBox.TabIndex = 25;
			this.userThumbBox.TabStop = false;
			// 
			// addForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(385, 636);
			this.Controls.Add(this.userThumbBox);
			this.Controls.Add(this.comThumbBox);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.getInfoFromHosoIdBtn);
			this.Controls.Add(this.hosoIdText);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.keywordText);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.communityFollowChkBox);
			this.Controls.Add(this.userFollowChkBox);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.memoText);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.getUserInfoBtn);
			this.Controls.Add(this.userNameText);
			this.Controls.Add(this.userIdText);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.getCommunityInfoBtn);
			this.Controls.Add(this.communityNameText);
			this.Controls.Add(this.communityId);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button3);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Name = "addForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "お気に入り追加";
			this.Load += new System.EventHandler(this.AddFormLoad);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.AddFormDragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.AddFormDragEnter);
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.comThumbBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.userThumbBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.PictureBox userThumbBox;
		private System.Windows.Forms.PictureBox comThumbBox;
		private System.Windows.Forms.CheckBox isMailChkBox;
		private System.Windows.Forms.CheckBox appliGChkBox;
		private System.Windows.Forms.CheckBox appliHChkBox;
		private System.Windows.Forms.CheckBox appliIChkBox;
		private System.Windows.Forms.CheckBox appliJChkBox;
		private System.Windows.Forms.CheckBox appliDChkBox;
		private System.Windows.Forms.CheckBox appliEChkBox;
		private System.Windows.Forms.CheckBox appliFChkBox;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.CheckBox isWebChkBox;
		private System.Windows.Forms.CheckBox isSoundChkBox;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox hosoIdText;
		private System.Windows.Forms.Button getInfoFromHosoIdBtn;
		private System.Windows.Forms.CheckBox isPopupChkBox;
		private System.Windows.Forms.CheckBox isBaloonChkBox;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox keywordText;
		private System.Windows.Forms.CheckBox communityFollowChkBox;
		private System.Windows.Forms.CheckBox userFollowChkBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox memoText;
		private System.Windows.Forms.CheckBox appliAChkBox;
		private System.Windows.Forms.CheckBox appliBChkBox;
		private System.Windows.Forms.CheckBox appliCChkBox;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox userIdText;
		private System.Windows.Forms.TextBox userNameText;
		private System.Windows.Forms.Button getUserInfoBtn;
		private System.Windows.Forms.Button getCommunityInfoBtn;
		private System.Windows.Forms.TextBox communityNameText;
		private System.Windows.Forms.TextBox communityId;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
	}
}
