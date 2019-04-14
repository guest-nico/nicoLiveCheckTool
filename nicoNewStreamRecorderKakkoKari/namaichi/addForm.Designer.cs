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
			this.behaviorGroupBox = new System.Windows.Forms.GroupBox();
			this.isPopupChkBox = new System.Windows.Forms.CheckBox();
			this.isBaloonChkBox = new System.Windows.Forms.CheckBox();
			this.isWebChkBox = new System.Windows.Forms.CheckBox();
			this.isMailChkBox = new System.Windows.Forms.CheckBox();
			this.isSoundChkBox = new System.Windows.Forms.CheckBox();
			this.appliAChkBox = new System.Windows.Forms.CheckBox();
			this.appliBChkBox = new System.Windows.Forms.CheckBox();
			this.appliCChkBox = new System.Windows.Forms.CheckBox();
			this.appliDChkBox = new System.Windows.Forms.CheckBox();
			this.appliEChkBox = new System.Windows.Forms.CheckBox();
			this.appliFChkBox = new System.Windows.Forms.CheckBox();
			this.existSoundFileLabel = new System.Windows.Forms.Label();
			this.appliGChkBox = new System.Windows.Forms.CheckBox();
			this.appliHChkBox = new System.Windows.Forms.CheckBox();
			this.appliIChkBox = new System.Windows.Forms.CheckBox();
			this.appliJChkBox = new System.Windows.Forms.CheckBox();
			this.sampleColorText = new System.Windows.Forms.TextBox();
			this.backColorBtn = new System.Windows.Forms.Button();
			this.textColorBtn = new System.Windows.Forms.Button();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.defaultColorBtn = new System.Windows.Forms.Button();
			this.isDefaultSoundIdChkBox = new System.Windows.Forms.CheckBox();
			this.defaultSoundList = new System.Windows.Forms.ComboBox();
			this.label12 = new System.Windows.Forms.Label();
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
			this.isMustComChkBox = new System.Windows.Forms.CheckBox();
			this.isMustUserChkBox = new System.Windows.Forms.CheckBox();
			this.isMustKeywordChkBox = new System.Windows.Forms.CheckBox();
			this.isSimpleKeywordRadioBtn = new System.Windows.Forms.RadioButton();
			this.isCustomKeywordRadioBtn = new System.Windows.Forms.RadioButton();
			this.customKeywordBtn = new System.Windows.Forms.Button();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.isAnd = new System.Windows.Forms.RadioButton();
			this.radioButton1 = new System.Windows.Forms.RadioButton();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.behaviorGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.comThumbBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.userThumbBox)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(265, 743);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(74, 23);
			this.button4.TabIndex = 3;
			this.button4.Text = "キャンセル";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.Button4Click);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(185, 743);
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
			// behaviorGroupBox
			// 
			this.behaviorGroupBox.Controls.Add(this.isPopupChkBox);
			this.behaviorGroupBox.Controls.Add(this.isBaloonChkBox);
			this.behaviorGroupBox.Controls.Add(this.isWebChkBox);
			this.behaviorGroupBox.Controls.Add(this.isMailChkBox);
			this.behaviorGroupBox.Controls.Add(this.isSoundChkBox);
			this.behaviorGroupBox.Controls.Add(this.appliAChkBox);
			this.behaviorGroupBox.Controls.Add(this.appliBChkBox);
			this.behaviorGroupBox.Controls.Add(this.appliCChkBox);
			this.behaviorGroupBox.Controls.Add(this.appliDChkBox);
			this.behaviorGroupBox.Controls.Add(this.appliEChkBox);
			this.behaviorGroupBox.Controls.Add(this.appliFChkBox);
			this.behaviorGroupBox.Controls.Add(this.existSoundFileLabel);
			this.behaviorGroupBox.Controls.Add(this.appliGChkBox);
			this.behaviorGroupBox.Controls.Add(this.appliHChkBox);
			this.behaviorGroupBox.Controls.Add(this.appliIChkBox);
			this.behaviorGroupBox.Controls.Add(this.appliJChkBox);
			this.behaviorGroupBox.Controls.Add(this.sampleColorText);
			this.behaviorGroupBox.Controls.Add(this.backColorBtn);
			this.behaviorGroupBox.Controls.Add(this.textColorBtn);
			this.behaviorGroupBox.Controls.Add(this.label11);
			this.behaviorGroupBox.Controls.Add(this.label10);
			this.behaviorGroupBox.Controls.Add(this.defaultColorBtn);
			this.behaviorGroupBox.Controls.Add(this.isDefaultSoundIdChkBox);
			this.behaviorGroupBox.Controls.Add(this.defaultSoundList);
			this.behaviorGroupBox.Controls.Add(this.label12);
			this.behaviorGroupBox.Location = new System.Drawing.Point(28, 441);
			this.behaviorGroupBox.Name = "behaviorGroupBox";
			this.behaviorGroupBox.Size = new System.Drawing.Size(345, 208);
			this.behaviorGroupBox.TabIndex = 13;
			this.behaviorGroupBox.TabStop = false;
			this.behaviorGroupBox.Text = "動作";
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
			// isBaloonChkBox
			// 
			this.isBaloonChkBox.Location = new System.Drawing.Point(89, 18);
			this.isBaloonChkBox.Name = "isBaloonChkBox";
			this.isBaloonChkBox.Size = new System.Drawing.Size(82, 24);
			this.isBaloonChkBox.TabIndex = 4;
			this.isBaloonChkBox.Text = "バルーン";
			this.isBaloonChkBox.UseVisualStyleBackColor = true;
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
			// isMailChkBox
			// 
			this.isMailChkBox.Location = new System.Drawing.Point(255, 18);
			this.isMailChkBox.Name = "isMailChkBox";
			this.isMailChkBox.Size = new System.Drawing.Size(68, 24);
			this.isMailChkBox.TabIndex = 13;
			this.isMailChkBox.Text = "メール";
			this.isMailChkBox.UseVisualStyleBackColor = true;
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
			// appliAChkBox
			// 
			this.appliAChkBox.Location = new System.Drawing.Point(89, 40);
			this.appliAChkBox.Name = "appliAChkBox";
			this.appliAChkBox.Size = new System.Drawing.Size(62, 24);
			this.appliAChkBox.TabIndex = 0;
			this.appliAChkBox.Text = "アプリA";
			this.appliAChkBox.UseVisualStyleBackColor = true;
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
			// appliCChkBox
			// 
			this.appliCChkBox.Location = new System.Drawing.Point(255, 40);
			this.appliCChkBox.Name = "appliCChkBox";
			this.appliCChkBox.Size = new System.Drawing.Size(62, 24);
			this.appliCChkBox.TabIndex = 2;
			this.appliCChkBox.Text = "アプリC";
			this.appliCChkBox.UseVisualStyleBackColor = true;
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
			// appliEChkBox
			// 
			this.appliEChkBox.Location = new System.Drawing.Point(89, 62);
			this.appliEChkBox.Name = "appliEChkBox";
			this.appliEChkBox.Size = new System.Drawing.Size(62, 24);
			this.appliEChkBox.TabIndex = 7;
			this.appliEChkBox.Text = "アプリE";
			this.appliEChkBox.UseVisualStyleBackColor = true;
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
			// existSoundFileLabel
			// 
			this.existSoundFileLabel.ForeColor = System.Drawing.Color.Black;
			this.existSoundFileLabel.Location = new System.Drawing.Point(25, 188);
			this.existSoundFileLabel.Name = "existSoundFileLabel";
			this.existSoundFileLabel.Size = new System.Drawing.Size(196, 15);
			this.existSoundFileLabel.TabIndex = 19;
			this.existSoundFileLabel.Text = "該当するサウンドファイルが見つかりました";
			this.existSoundFileLabel.Visible = false;
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
			// appliHChkBox
			// 
			this.appliHChkBox.Location = new System.Drawing.Point(6, 84);
			this.appliHChkBox.Name = "appliHChkBox";
			this.appliHChkBox.Size = new System.Drawing.Size(62, 24);
			this.appliHChkBox.TabIndex = 10;
			this.appliHChkBox.Text = "アプリH";
			this.appliHChkBox.UseVisualStyleBackColor = true;
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
			// appliJChkBox
			// 
			this.appliJChkBox.Location = new System.Drawing.Point(172, 84);
			this.appliJChkBox.Name = "appliJChkBox";
			this.appliJChkBox.Size = new System.Drawing.Size(62, 24);
			this.appliJChkBox.TabIndex = 12;
			this.appliJChkBox.Text = "アプリJ";
			this.appliJChkBox.UseVisualStyleBackColor = true;
			// 
			// sampleColorText
			// 
			this.sampleColorText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
			this.sampleColorText.Location = new System.Drawing.Point(225, 110);
			this.sampleColorText.Name = "sampleColorText";
			this.sampleColorText.Size = new System.Drawing.Size(57, 19);
			this.sampleColorText.TabIndex = 16;
			this.sampleColorText.Text = "例";
			this.sampleColorText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// backColorBtn
			// 
			this.backColorBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
			this.backColorBtn.Location = new System.Drawing.Point(159, 109);
			this.backColorBtn.Name = "backColorBtn";
			this.backColorBtn.Size = new System.Drawing.Size(60, 23);
			this.backColorBtn.TabIndex = 15;
			this.backColorBtn.UseVisualStyleBackColor = false;
			this.backColorBtn.Click += new System.EventHandler(this.BackColorBtnClick);
			// 
			// textColorBtn
			// 
			this.textColorBtn.BackColor = System.Drawing.Color.Black;
			this.textColorBtn.Location = new System.Drawing.Point(45, 109);
			this.textColorBtn.Name = "textColorBtn";
			this.textColorBtn.Size = new System.Drawing.Size(60, 23);
			this.textColorBtn.TabIndex = 15;
			this.textColorBtn.UseVisualStyleBackColor = false;
			this.textColorBtn.Click += new System.EventHandler(this.TextColorBtnClick);
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(118, 116);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(51, 12);
			this.label11.TabIndex = 14;
			this.label11.Text = "背景色";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(6, 116);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(51, 12);
			this.label10.TabIndex = 14;
			this.label10.Text = "文字色";
			// 
			// defaultColorBtn
			// 
			this.defaultColorBtn.Location = new System.Drawing.Point(288, 109);
			this.defaultColorBtn.Name = "defaultColorBtn";
			this.defaultColorBtn.Size = new System.Drawing.Size(35, 23);
			this.defaultColorBtn.TabIndex = 26;
			this.defaultColorBtn.Text = "戻す";
			this.defaultColorBtn.UseVisualStyleBackColor = true;
			this.defaultColorBtn.Click += new System.EventHandler(this.DefaultColorBtnClick);
			// 
			// isDefaultSoundIdChkBox
			// 
			this.isDefaultSoundIdChkBox.Location = new System.Drawing.Point(6, 161);
			this.isDefaultSoundIdChkBox.Name = "isDefaultSoundIdChkBox";
			this.isDefaultSoundIdChkBox.Size = new System.Drawing.Size(331, 24);
			this.isDefaultSoundIdChkBox.TabIndex = 29;
			this.isDefaultSoundIdChkBox.Text = "Soundフォルダに「{ID}.wav」がある場合、そちらを優先して再生する";
			this.isDefaultSoundIdChkBox.UseVisualStyleBackColor = true;
			this.isDefaultSoundIdChkBox.CheckedChanged += new System.EventHandler(this.IsDefaultSoundIdChkBoxCheckedChanged);
			// 
			// defaultSoundList
			// 
			this.defaultSoundList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.defaultSoundList.FormattingEnabled = true;
			this.defaultSoundList.Items.AddRange(new object[] {
									"デフォルトのまま使う",
									"音ファイルA",
									"音ファイルB",
									"音ファイルC"});
			this.defaultSoundList.Location = new System.Drawing.Point(89, 138);
			this.defaultSoundList.Name = "defaultSoundList";
			this.defaultSoundList.Size = new System.Drawing.Size(115, 20);
			this.defaultSoundList.TabIndex = 28;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(6, 141);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(77, 13);
			this.label12.TabIndex = 27;
			this.label12.Text = "音設定ファイル";
			// 
			// memoText
			// 
			this.memoText.Location = new System.Drawing.Point(53, 663);
			this.memoText.Name = "memoText";
			this.memoText.Size = new System.Drawing.Size(286, 19);
			this.memoText.TabIndex = 14;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(28, 666);
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
			this.keywordText.Location = new System.Drawing.Point(122, 329);
			this.keywordText.Name = "keywordText";
			this.keywordText.Size = new System.Drawing.Size(217, 19);
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
			this.getInfoFromHosoIdBtn.Location = new System.Drawing.Point(298, 711);
			this.getInfoFromHosoIdBtn.Name = "getInfoFromHosoIdBtn";
			this.getInfoFromHosoIdBtn.Size = new System.Drawing.Size(75, 23);
			this.getInfoFromHosoIdBtn.TabIndex = 21;
			this.getInfoFromHosoIdBtn.Text = "情報取得";
			this.getInfoFromHosoIdBtn.UseVisualStyleBackColor = true;
			this.getInfoFromHosoIdBtn.Click += new System.EventHandler(this.GetInfoFromHosoIdBtnClick);
			// 
			// hosoIdText
			// 
			this.hosoIdText.Location = new System.Drawing.Point(216, 713);
			this.hosoIdText.Name = "hosoIdText";
			this.hosoIdText.Size = new System.Drawing.Size(76, 19);
			this.hosoIdText.TabIndex = 20;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(12, 716);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(167, 15);
			this.label7.TabIndex = 19;
			this.label7.Text = "放送IDから情報を取得する・・・";
			// 
			// label8
			// 
			this.label8.Enabled = false;
			this.label8.Location = new System.Drawing.Point(163, 716);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(47, 15);
			this.label8.TabIndex = 22;
			this.label8.Text = "放送ID";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(117, 349);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(256, 48);
			this.label9.TabIndex = 23;
			this.label9.Text = "（コミュニティ名、コミュニティID、放送者名、タイトル、説明、放送ID。スペース区切りでワードを複数指定可(OR条件)。語頭に「-」で「含まない」判定(NOT条件" +
			")）。";
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
			// isMustComChkBox
			// 
			this.isMustComChkBox.Checked = true;
			this.isMustComChkBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.isMustComChkBox.Location = new System.Drawing.Point(258, 212);
			this.isMustComChkBox.Name = "isMustComChkBox";
			this.isMustComChkBox.Size = new System.Drawing.Size(81, 15);
			this.isMustComChkBox.TabIndex = 16;
			this.isMustComChkBox.Text = "必ず一致";
			this.isMustComChkBox.UseVisualStyleBackColor = true;
			// 
			// isMustUserChkBox
			// 
			this.isMustUserChkBox.Checked = true;
			this.isMustUserChkBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.isMustUserChkBox.Location = new System.Drawing.Point(258, 295);
			this.isMustUserChkBox.Name = "isMustUserChkBox";
			this.isMustUserChkBox.Size = new System.Drawing.Size(81, 15);
			this.isMustUserChkBox.TabIndex = 16;
			this.isMustUserChkBox.Text = "必ず一致";
			this.isMustUserChkBox.UseVisualStyleBackColor = true;
			// 
			// isMustKeywordChkBox
			// 
			this.isMustKeywordChkBox.Checked = true;
			this.isMustKeywordChkBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.isMustKeywordChkBox.Location = new System.Drawing.Point(258, 427);
			this.isMustKeywordChkBox.Name = "isMustKeywordChkBox";
			this.isMustKeywordChkBox.Size = new System.Drawing.Size(81, 15);
			this.isMustKeywordChkBox.TabIndex = 16;
			this.isMustKeywordChkBox.Text = "必ず一致";
			this.isMustKeywordChkBox.UseVisualStyleBackColor = true;
			// 
			// isSimpleKeywordRadioBtn
			// 
			this.isSimpleKeywordRadioBtn.Checked = true;
			this.isSimpleKeywordRadioBtn.Location = new System.Drawing.Point(102, 331);
			this.isSimpleKeywordRadioBtn.Name = "isSimpleKeywordRadioBtn";
			this.isSimpleKeywordRadioBtn.Size = new System.Drawing.Size(14, 15);
			this.isSimpleKeywordRadioBtn.TabIndex = 26;
			this.isSimpleKeywordRadioBtn.TabStop = true;
			this.isSimpleKeywordRadioBtn.UseVisualStyleBackColor = true;
			// 
			// isCustomKeywordRadioBtn
			// 
			this.isCustomKeywordRadioBtn.Location = new System.Drawing.Point(102, 401);
			this.isCustomKeywordRadioBtn.Name = "isCustomKeywordRadioBtn";
			this.isCustomKeywordRadioBtn.Size = new System.Drawing.Size(81, 15);
			this.isCustomKeywordRadioBtn.TabIndex = 26;
			this.isCustomKeywordRadioBtn.Text = "高度な設定";
			this.isCustomKeywordRadioBtn.UseVisualStyleBackColor = true;
			// 
			// customKeywordBtn
			// 
			this.customKeywordBtn.Location = new System.Drawing.Point(196, 397);
			this.customKeywordBtn.Name = "customKeywordBtn";
			this.customKeywordBtn.Size = new System.Drawing.Size(143, 23);
			this.customKeywordBtn.TabIndex = 27;
			this.customKeywordBtn.Text = "カスタム設定(未設定)";
			this.customKeywordBtn.UseVisualStyleBackColor = true;
			this.customKeywordBtn.Click += new System.EventHandler(this.CustomKeywordBtnClick);
			// 
			// linkLabel1
			// 
			this.linkLabel1.Location = new System.Drawing.Point(291, 145);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(74, 13);
			this.linkLabel1.TabIndex = 28;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "設定の説明";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel1LinkClicked);
			// 
			// isAnd
			// 
			this.isAnd.Location = new System.Drawing.Point(14, 15);
			this.isAnd.Name = "isAnd";
			this.isAnd.Size = new System.Drawing.Size(109, 15);
			this.isAnd.TabIndex = 26;
			this.isAnd.Text = "全てのﾜｰﾄﾞを含む";
			this.isAnd.UseVisualStyleBackColor = true;
			// 
			// radioButton1
			// 
			this.radioButton1.Checked = true;
			this.radioButton1.Location = new System.Drawing.Point(122, 15);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new System.Drawing.Size(129, 15);
			this.radioButton1.TabIndex = 26;
			this.radioButton1.TabStop = true;
			this.radioButton1.Text = "いずれかのﾜｰﾄﾞを含む";
			this.radioButton1.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.isAnd);
			this.groupBox1.Controls.Add(this.radioButton1);
			this.groupBox1.Location = new System.Drawing.Point(357, 312);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(259, 36);
			this.groupBox1.TabIndex = 29;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "スペース区切りでワードが複数指定された場合";
			this.groupBox1.Visible = false;
			// 
			// addForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(385, 772);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.linkLabel1);
			this.Controls.Add(this.customKeywordBtn);
			this.Controls.Add(this.isCustomKeywordRadioBtn);
			this.Controls.Add(this.isSimpleKeywordRadioBtn);
			this.Controls.Add(this.userThumbBox);
			this.Controls.Add(this.comThumbBox);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.getInfoFromHosoIdBtn);
			this.Controls.Add(this.hosoIdText);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.keywordText);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.isMustKeywordChkBox);
			this.Controls.Add(this.isMustUserChkBox);
			this.Controls.Add(this.isMustComChkBox);
			this.Controls.Add(this.communityFollowChkBox);
			this.Controls.Add(this.userFollowChkBox);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.memoText);
			this.Controls.Add(this.behaviorGroupBox);
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
			this.behaviorGroupBox.ResumeLayout(false);
			this.behaviorGroupBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.comThumbBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.userThumbBox)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.Label existSoundFileLabel;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton radioButton1;
		private System.Windows.Forms.RadioButton isAnd;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.Button customKeywordBtn;
		private System.Windows.Forms.RadioButton isCustomKeywordRadioBtn;
		private System.Windows.Forms.RadioButton isSimpleKeywordRadioBtn;
		private System.Windows.Forms.CheckBox isMustKeywordChkBox;
		private System.Windows.Forms.CheckBox isMustUserChkBox;
		private System.Windows.Forms.CheckBox isMustComChkBox;
		private System.Windows.Forms.ComboBox defaultSoundList;
		private System.Windows.Forms.CheckBox isDefaultSoundIdChkBox;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Button defaultColorBtn;
		private System.Windows.Forms.TextBox sampleColorText;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Button backColorBtn;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Button textColorBtn;
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
		private System.Windows.Forms.GroupBox behaviorGroupBox;
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
