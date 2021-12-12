/*
 * Created by SharpDevelop.
 * User: user
 * Date: 2019/01/04
 * Time: 7:23
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Collections.Generic;
using System.IO;
using namaichi.info;
using namaichi.rec;
using namaichi.alart;

namespace namaichi
{
	/// <summary>
	/// Description of addForm.
	/// </summary>
	public partial class addForm : Form
	{
//		public int addType = -1; //-1-no 0-user 1-community or channel 2-official
		public AlartInfo ret = null;
		private MainForm form;
		//private string lastGetThumbUser = null;
		//private string lastGetThumbCom = null;
		private AlartInfo editAi = null;
		private List<CustomKeywordInfo> customKw = null;
		private bool isUserMode = false;
		private SortableBindingList<AlartInfo> dataSource = null;
		public RssItem inputLvidItem = null;
		public bool isInputLvidItemClosed = false;
		public addForm(MainForm form, string id, AlartInfo editAi = null, bool isUserMode = false)
		{
			this.form = form;
			this.editAi = editAi;
			this.isUserMode = isUserMode;
			
			dataSource = isUserMode ? form.userAlartListDataSource : form.alartListDataSource;
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			setMemberOnlyListCloseEventHandler(this);
			util.setFontSize(int.Parse(form.config.get("fontSize")), this, false);
			
			if (editAi != null) {
				setEditModeDisplay(editAi);
				
				if (isUserMode) setUserModeForm();
				return;
			}
			
			for (var i = 0; i < memberOnlyCheckList.Items.Count; i++) {
				memberOnlyCheckList.SetItemChecked(i, true);
			}
			closeMemberOnlyList();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			setDefaultBehavior();
			setAppliName();
			
			if (id == null) {
				if (isUserMode) setUserModeForm();
				return;
			}
			
			if (id.StartsWith("lv")) hosoIdText.Text = id;
			else if (id.StartsWith("c")) {
				if (bool.Parse(form.config.get("IsdragCom"))) {
					hosoIdText.Text = id;
				}
				communityId.Text = id;
			}
			else userIdText.Text = id;
			
			if (isUserMode) {
				//communityId.Text = communityNameText.Text = "";
				//keywordText.Text = "";
				setUserModeForm();
			}
		}
		private void setMemberOnlyListCloseEventHandler(Control parent) {
			foreach (Control c in parent.Controls) {
				if (c.Name != "memberOnlyList" &&
				    (c.Name != "memberOnlyCheckList")) {
				    c.Click += (o, e) => {
				    	memberOnlyList.DroppedDown = false;
				    	closeMemberOnlyList();
					};
					setMemberOnlyListCloseEventHandler(c);
				}
			}
			parent.Click += (o, e) => {
		    	memberOnlyList.DroppedDown = memberOnlyCheckList.Visible = false;
			};
		}
		
		void Button4Click(object sender, EventArgs e)
		{
			Close();
		}
		
		void Button3Click(object sender, EventArgs e)
		{
			if (editAi != null) editOkBtnProcess();
			else addOkBtnProcess();
			
		}
		void addOkBtnProcess() {
			var comId = communityId.Text == "official" ? "official" : util.getRegGroup(communityId.Text, "((ch|co)*\\d+)");
			var userId = util.getRegGroup(userIdText.Text, "(\\d+)");
			communityNameText.Text = "";
			userNameText.Text = "";
			if (comId != null) GetCommunityInfoBtnClickProcess(true);
			if (userId != null) GetUserInfoBtnClickProcess(true);
			var isNoKeyword = (isSimpleKeywordRadioBtn.Checked && keywordText.Text == "") ||
					(isCustomKeywordRadioBtn.Checked && customKw == null);
			if (communityNameText.Text == "" && userNameText.Text == "" && isNoKeyword) {
				util.showMessageBoxCenterForm(this, "有効なコミュニティIDかユーザーIDかキーワードが入力されていないです");
				return;
			}
			
			if (isCustomKeywordRadioBtn.Checked) {
				if (customKw == null) {
					util.showMessageBoxCenterForm(this, "カスタムキーワードが未設定です");
					return;
				}
				var isAllNot = true;
				foreach (var c in customKw) {
					if (c.matchType != "含まない") isAllNot = false;
				}
				if (communityNameText.Text == "" && userNameText.Text == "" && isAllNot) {
					util.showMessageBoxCenterForm(this, "「含まない」以外の行が必要です");
					return;
				}
			}
			
			
			var now = DateTime.Now.ToString("yyyy\"/\"MM\"/\"dd HH\":\"mm\":\"ss");
			var addDate = now;//now.Substring(0, now.Length - 3);
			var comFollow = string.IsNullOrEmpty(comId) || form.check.container == null ? "" :
					((communityFollowChkBox.Checked) ? "フォロー解除する" : "フォローする");
			var userFollow = string.IsNullOrEmpty(userId) || form.check.container == null ? "" :
					(userFollowChkBox.Checked) ? "フォロー解除する" : "フォローする";
			if (communityNameText.Text == "" || comId == "official") comFollow = "";
			if (userNameText.Text == "") userFollow = "";
			
			var memberOnly = getMemberOnly();
			if (memberOnly == null) {
				util.showMessageBoxCenterForm(this, "通常放送、限定放送、有料放送のいずれかにチェックが必要です");
				return;
			}
			var _ret = new AlartInfo(comId, userId, 
					communityNameText.Text, userNameText.Text, 
					"", addDate, isPopupChkBox.Checked, 
					isBaloonChkBox.Checked, isWebChkBox.Checked, 
					isMailChkBox.Checked, isSoundChkBox.Checked,
					appliAChkBox.Checked, appliBChkBox.Checked, 
					appliCChkBox.Checked, appliDChkBox.Checked, 
					appliEChkBox.Checked, appliFChkBox.Checked,
					appliGChkBox.Checked, appliHChkBox.Checked,					
					appliIChkBox.Checked, appliJChkBox.Checked,
					memoText.Text, comFollow,
					userFollow, "", keywordText.Text, 
					textColorBtn.BackColor, backColorBtn.BackColor,
					defaultSoundList.SelectedIndex, 
					isDefaultSoundIdChkBox.Checked, isMustComChkBox.Checked,
					isMustUserChkBox.Checked, isMustKeywordChkBox.Checked,
					customKw, isCustomKeywordRadioBtn.Checked, memberOnly,
					isAutoReserveChkBox.Checked, 0, "");
			if (inputLvidItem != null) {
				_ret.lastHosoDt = inputLvidItem.pubDateDt;
				_ret.lastHostDate = inputLvidItem.pubDateDt.ToString("yyyy\"/\"MM\"/\"dd HH\":\"mm\":\"ss");
				_ret.lastLvid = inputLvidItem.lvId;
				_ret.lastLvType = inputLvidItem.type;
				_ret.lastLvTitle = inputLvidItem.title;
				if (!isInputLvidItemClosed)
					_ret.recentColorMode = bool.Parse(form.config.get("IscheckRecent")) ? ((inputLvidItem.isMemberOnly) ? 2 : 1) : 0;				
			}
			if (!duplicationCheckOk(_ret)) return;
			ret = _ret;
			util.debugWriteLine("addform add okbtn " + _ret.hostId + " " + _ret.communityId + " " + _ret.keyword);
			Close();
		}
		void editOkBtnProcess() {
			var comId = communityId.Text == "official" ? "official" : util.getRegGroup(communityId.Text, "((ch|co)*\\d+)");
			var userId = util.getRegGroup(userIdText.Text, "(\\d+)");
			communityNameText.Text = "";
			userNameText.Text = "";
			if (comId != null) GetCommunityInfoBtnClickProcess(true);
			if (userId != null) GetUserInfoBtnClickProcess(true);
			var comFollow = string.IsNullOrEmpty(comId) || form.check.container == null ? "" :
					(communityFollowChkBox.Checked) ? "フォロー解除する" : "フォローする";
			if (communityNameText.Text == "" || comId == "official") comFollow = "";
			var userFollow = string.IsNullOrEmpty(userId) || form.check.container == null ? "":
					(userFollowChkBox.Checked) ? "フォロー解除する" : "フォローする";
			
			var isNoKeyword = (isSimpleKeywordRadioBtn.Checked && keywordText.Text == "") ||
					(isCustomKeywordRadioBtn.Checked && customKw == null);
			if (communityNameText.Text == "" && userNameText.Text == "" && isNoKeyword) {
				util.showMessageBoxCenterForm(this, "有効なコミュニティIDかユーザーIDかキーワードが入力されていないです");
				return;
			}
			
			if (customKw != null) {
				var isAllNot = true;
				foreach (var c in customKw) {
					if (c.matchType != "含まない") isAllNot = false;
				}
				if (communityNameText.Text == "" && userNameText.Text == "" && isAllNot) {
					util.showMessageBoxCenterForm(this, "「含まない」以外の行が必要です");
					return;
				}
			}
			
			var memberOnly = getMemberOnly();
			if (memberOnly == null) {
				util.showMessageBoxCenterForm(this, "通常放送、限定放送、有料放送のいずれかにチェックが必要です");
				return;
			}
			
			editAi.communityId = comId;
			editAi.hostId = userId;
			editAi.communityName = communityNameText.Text;
			editAi.hostName = userNameText.Text;
			editAi.communityFollow = comFollow;
			editAi.hostFollow = userFollow;
			editAi.memo = memoText.Text;
			editAi.keyword = keywordText.Text;
			editAi.popup = isPopupChkBox.Checked;
			editAi.baloon = isBaloonChkBox.Checked;
			editAi.browser = isWebChkBox.Checked;
			editAi.mail = isMailChkBox.Checked;
			editAi.sound = isSoundChkBox.Checked;
			editAi.appliA = appliAChkBox.Checked;
			editAi.appliB = appliBChkBox.Checked;
			editAi.appliC = appliCChkBox.Checked;
			editAi.appliD = appliDChkBox.Checked;
			editAi.appliE = appliEChkBox.Checked;
			editAi.appliF = appliFChkBox.Checked;
			editAi.appliG = appliGChkBox.Checked;
			editAi.appliH = appliHChkBox.Checked;
			editAi.appliI = appliIChkBox.Checked;
			editAi.appliJ = appliJChkBox.Checked;
			editAi.textColor = textColorBtn.BackColor;
			editAi.backColor = backColorBtn.BackColor;
			editAi.soundType = defaultSoundList.SelectedIndex;
			editAi.isSoundId = isDefaultSoundIdChkBox.Checked;
			editAi.isMustCom = isMustComChkBox.Checked;
			editAi.isMustUser = isMustUserChkBox.Checked;
			editAi.isMustKeyword = isMustKeywordChkBox.Checked;
			editAi.cki = customKw;
			editAi.isCustomKeyword = isCustomKeywordRadioBtn.Checked;
			editAi.memberOnlyMode = memberOnly;
			editAi.isAutoReserve = isAutoReserveChkBox.Checked;
			/*
			var _ret = new AlartInfo(comId, userId, 
					communityNameText.Text, userNameText.Text, 
					"", editAi.addDate, isPopupChkBox.Checked, 
					isBaloonChkBox.Checked, isWebChkBox.Checked, 
					isMailChkBox.Checked, isSoundChkBox.Checked,
					appliAChkBox.Checked, appliBChkBox.Checked, 
					appliCChkBox.Checked, appliDChkBox.Checked, 
					appliEChkBox.Checked, appliFChkBox.Checked,
					appliGChkBox.Checked, appliHChkBox.Checked,					
					appliIChkBox.Checked, appliJChkBox.Checked,
					memoText.Text, comFollow,
					userFollow, "", keywordText.Text);
			ret = _ret;
			*/
			ret = editAi;
			util.debugWriteLine("addform edit okbtn " + editAi.hostId + " " + editAi.communityId + " " + editAi.keyword);
			Close();
		}
		void GetCommunityInfoBtnClick(object sender, EventArgs e)
		{
			GetCommunityInfoBtnClickProcess();
		}
		void GetCommunityInfoBtnClickProcess(bool isOkBtn = false) {
			util.debugWriteLine("GetCommunityInfoBtnClickProcess " + communityId.Text);
			if (communityId.Text == "official") {
				communityNameText.Text = "公式生放送";
				communityFollowChkBox.Checked = false;
				setThunb("co00", false);
				return;
			}
			
			var num = util.getRegGroup(communityId.Text, "((ch|co)*\\d+)");
			if (num == null) return;
			var isChannel = num.StartsWith("ch");
			if (!num.StartsWith("c")) num = "co" + num;
			communityId.Text = num;
			/*
			var url = "https://com.nicovideo.jp/community/" + communityId.Text;
			var res = util.getPageSource(url, null, null, false, 5000);
			if (res == null) return;
			*/
			var isFollow = false;
			var comName = util.getCommunityName(num, out isFollow, form.check.container);
			if (comName == null) return;
			communityFollowChkBox.Checked = isFollow;
			communityNameText.Text = comName;
			
			setThunb(num, isOkBtn);
		}
		
		void GetUserInfoBtnClick(object sender, EventArgs e)
		{
			GetUserInfoBtnClickProcess();
		}
		void GetUserInfoBtnClickProcess(bool isOkBtn = false) {
			util.debugWriteLine("GetUserInfoBtnClickProcess " + userIdText.Text);
			var num = util.getRegGroup(userIdText.Text, "(\\d+)");
			if (num == null) return;
			userIdText.Text = num;
			var isFollow = false;
			var userName = util.getUserName(num, out isFollow, form.check.container, true, form.config);
			if (userName == null) return;
			userFollowChkBox.Checked = isFollow;
			userNameText.Text = userName;
			
			setThunb(num, isOkBtn);
		}
		void GetInfoFromHosoIdBtnClick(object sender, EventArgs e)
		{
			util.debugWriteLine("getInfoFromHosoIdBtnClick " + hosoIdText.Text);
			if (hosoIdText.Text == "") return;
			
			var t = hosoIdText.Text;
			string id = null;
			var lv = util.getRegGroup(t, "(lv\\d+)");
			if (lv != null) id = lv;
			else {
				var coch = util.getRegGroup(t, "(c[oh]\\d+)");
				if (coch != null) id = coch;
				else {
					var user = util.getRegGroup(t, "user/(\\d+)");
					if (user != null) id = user;
				}
			}
			hosoIdText.Text = id;
			if (id == null || (!id.StartsWith("l") && !id.StartsWith("c")))
				util.showMessageBoxCenterForm(this, "しっぱい");
			
			communityId.Text = communityNameText.Text = 
					userIdText.Text = userNameText.Text = "";
			communityFollowChkBox.Checked = userFollowChkBox.Checked = false;
			
			var url =  "https://live2.nicovideo.jp/watch/" + id;
			var hig = new HosoInfoGetter();
			hig.get(url, form.check.container);
			
			communityId.Text = hig.communityId;
			userIdText.Text = hig.userId;
			GetCommunityInfoBtnClick(null, null);
			GetUserInfoBtnClick(null, null);
			
			try {
				if (hig.communityId == null && hig.userId == null)
					util.showMessageBoxCenterForm(this, hig.type == "official" ? "公式放送でした" : "しっぱい");
				
				inputLvidItem = new RssItem(hig.title, id, hig.openDt.ToString(), hig.description, hig.group, hig.communityId, hig.userName, hig.thumbnail, hig.isMemberOnly.ToString(), "", hig.isPayment);
				inputLvidItem.setUserId(hig.userId);
				inputLvidItem.setTag(hig.tags);
				inputLvidItem.category = hig.category;
				inputLvidItem.type = hig.type;
				inputLvidItem.pubDateDt = hig.openDt;
				isInputLvidItemClosed = hig.isClosed;
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.Source + ee.StackTrace + ee.TargetSite);
			}
			/*
			if (id.StartsWith("c")) {
				if (hig.communityId != null) {
					var isFollow = false;
					var name = util.getCommunityName(hig.communityId, out isFollow, form.check.container);
					communityId.Text = hig.communityId;
					if (name != null)
						communityNameText.Text = name;
				}
				if (hig.userId != null) {
					var isFollow = false;
					var name = util.getUserName(hig.userId, out isFollow, form.check.container);
					if (name != null)
						userNameText.Text = name;
				}
			} else {
				communityId.Text = hig.communityId;
				userIdText.Text = hig.userId;
				GetCommunityInfoBtnClick(null, null);
				GetUserInfoBtnClick(null, null);
				
				if (hig.communityId == null && hig.userId == null)
					util.showMessageBoxCenterForm(this, hig.type == "official" ? "公式放送でした" : "しっぱい");
			}
			*/
		}
		bool duplicationCheckOk(AlartInfo ai) {
			try {
				var count = form.getAlartListCount(isUserMode);
				for (var i = 0; i < count; i++) {
					if (ai.communityId != null && ai.communityId != "" && dataSource[i].communityId == 
					    	ai.communityId) {
						var m = (ai.communityId.StartsWith("co")) ? "コミュニティ" : (ai.communityId == "official" ? "official" : "チャンネル");
						
					    //var res = util.showMessageBoxCenterForm(this, m + "ID" + ai.communityId + "は既に登録されています。削除しますか？(はい＝削除　いいえ＝削除　キャンセル＝フォームに戻る)", "確認", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
					    form.setAlartListScrollIndex(i, isUserMode);
					    var res = util.showMessageBoxCenterForm(this, m + "ID:" + ai.communityId + "は既に登録されています。\n(" + dataSource[i].toString() + ")\n\n既に存在している行を削除しますか？\nはい=削除して登録　いいえ=既に存在する行を削除せず登録　キャンセル=登録しない", "確認", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
					    
					    if (res == DialogResult.Yes) {
					    	form.alartListRemove(dataSource[i], isUserMode);
					    	i--;
					    	count--;
	//				    	form.alartListAdd(ai);
					    }
					    else if (res == DialogResult.No) {
	//				    	form.alartListAdd(ai);
					    }
					    else if (res == DialogResult.Cancel) return false;
					}
				}
				count = form.getAlartListCount(isUserMode);
				for (var i = 0; i < count; i++) {
					if (ai.hostId != null && ai.hostId != "" && dataSource[i].hostId ==
						   ai.hostId) {
						var m = "ユーザー";
					    //var res = util.showMessageBoxCenterForm(this, m + "ID" + ai.communityId + "は既に登録されています。削除しますか？(はい＝削除して登録　いいえ＝登録　キャンセル＝フォームに戻る)", "確認", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
					    form.setAlartListScrollIndex(i, isUserMode);
					    var res = util.showMessageBoxCenterForm(this, m + "ID:" + ai.hostId + "は既に登録されています。\n(" + dataSource[i].toString() + ")\n\n既に存在している行を削除しますか？\nはい=削除して登録　いいえ=既に存在する行を削除せず登録　キャンセル=登録しない", "確認", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
					    
					    if (res == DialogResult.Yes) {
					    	form.alartListRemove(dataSource[i], isUserMode);
					    	i--;
					    	count--;
	//				    	form.alartListAdd(ai);
					    }
					    else if (res == DialogResult.No) {
	//				    	form.alartListAdd(ai);
					    }
					    else if (res == DialogResult.Cancel) return false;
						
					}
				}
			} catch (Exception e) {
				util.debugWriteLine("duplicationcheckok " + e.Message + e.Source + e.StackTrace + e.TargetSite);
				return false;
			}
			return true;
		}
		
		void AddFormLoad(object sender, EventArgs e)
		{
			if (editAi != null) return;
			if (communityId.Text != "") getCommunityInfoBtn.PerformClick();
			if (userIdText.Text != "") getUserInfoBtn.PerformClick();
			if (hosoIdText.Text != "") getInfoFromHosoIdBtn.PerformClick();
		}
		
		void AddFormDragDrop(object sender, DragEventArgs e)
		{
			try {
				util.debugWriteLine("dragdrop");
				
				var t = e.Data.GetData(DataFormats.Text).ToString();
				string id = null;
				var lv = util.getRegGroup(t, "(lv\\d+)");
				if (lv != null) id = lv;
				else {
					var coch = util.getRegGroup(t, "(c[oh]\\d+)");
					if (coch != null) id = coch;
					else {
						var user = util.getRegGroup(t, "user/(\\d+)");
						if (user != null) id = user;
					}
				}
				if (id == null) return;
				
				if (id.StartsWith("l") || id.StartsWith("c")) {
					hosoIdText.Text = id.ToString();
					getInfoFromHosoIdBtn.PerformClick();
				} else {
					userIdText.Text = id;
					getUserInfoBtn.PerformClick();
				}
				
				
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		void AddFormDragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent("UniformResourceLocator") ||
			    e.Data.GetDataPresent("UniformResourceLocatorW") ||
			    e.Data.GetDataPresent(DataFormats.Text)) {
				util.debugWriteLine(e.Effect);
				e.Effect = DragDropEffects.Copy;
				
			}
		}
		private void setThunb(string num, bool isOkBtn) {
			var isUser = !num.StartsWith("c");
			Image img;
			if (ThumbnailManager.isExist(num, out img)) {
				var type = isUser ? "ユーザー" : "コミュニティ";
				if (isUser) userThumbBox.Image = new Bitmap(img, userThumbBox.Size);
				else comThumbBox.Image = new Bitmap(img, comThumbBox.Size);
			
				var res = DialogResult.No;
				if (!isOkBtn && num != "co00") {
					res = util.showMessageBoxCenterForm(this, type + "画像を取得し直しますか？", 
							type + "画像が既に存在します", MessageBoxButtons.YesNo,
							MessageBoxIcon.Warning);
				}
				if (res == DialogResult.No) {
					if (isUser) {
						userThumbBox.Image = new Bitmap(img, userThumbBox.Size);
					} else {
						comThumbBox.Image = new Bitmap(img, comThumbBox.Size);
					}
					return;
				}
			}
			var newImg = ThumbnailManager.getImageId(num);
			if (newImg == null) {
				return;
			}
			if (bool.Parse(form.config.get("alartCacheIcon")))
				ThumbnailManager.saveImage(newImg, num);
			if (isUser) {
				userThumbBox.Image = new Bitmap(newImg, userThumbBox.Size);
			} else {
				comThumbBox.Image = new Bitmap(newImg, comThumbBox.Size);
			}
			newImg.Dispose();
		}
		void setEditModeDisplay(AlartInfo editAi) {
			Text = "お気に入り編集";
			communityId.Text = string.IsNullOrEmpty(editAi.communityId) ? "" : editAi.communityId;
			communityNameText.Text = string.IsNullOrEmpty(editAi.communityName) ? "" : editAi.communityName;
			userIdText.Text = string.IsNullOrEmpty(editAi.hostId) ? "" : editAi.hostId;
			userNameText.Text = string.IsNullOrEmpty(editAi.hostName) ? "" : editAi.hostName;
			keywordText.Text = string.IsNullOrEmpty(editAi.keyword) ? "" : editAi.keyword;
			memoText.Text = string.IsNullOrEmpty(editAi.memo) ? "" : editAi.memo;
			communityFollowChkBox.Checked = editAi.communityFollow == "フォロー解除する";
			userFollowChkBox.Checked = editAi.hostFollow == "フォロー解除する";
			
			isPopupChkBox.Checked = editAi.popup;
			isBaloonChkBox.Checked = editAi.baloon;
			isWebChkBox.Checked = editAi.browser;
			isMailChkBox.Checked = editAi.mail;
			isSoundChkBox.Checked = editAi.sound;
			appliAChkBox.Checked = editAi.appliA;
			appliBChkBox.Checked = editAi.appliB;
			appliCChkBox.Checked = editAi.appliC;
			appliDChkBox.Checked = editAi.appliD;
			appliEChkBox.Checked = editAi.appliE;
			appliFChkBox.Checked = editAi.appliF;
			appliGChkBox.Checked = editAi.appliG;
			appliHChkBox.Checked = editAi.appliH;
			appliIChkBox.Checked = editAi.appliI;
			appliJChkBox.Checked = editAi.appliJ;
			textColorBtn.BackColor = sampleColorText.ForeColor = 
				editAi.textColor;
			backColorBtn.BackColor = sampleColorText.BackColor = 
				editAi.backColor;
			defaultSoundList.SelectedIndex = editAi.soundType;
			isDefaultSoundIdChkBox.Checked = editAi.isSoundId;
			setMemberOnlyListInit(editAi);
			//memberOnlyList.SelectedIndex = editAi.memberOnlyMode;
			
			Image comThumb = null, userThumb = null;
			if (!string.IsNullOrEmpty(editAi.communityId) && editAi.communityId != "official")
				if (ThumbnailManager.isExist(editAi.communityId, out comThumb))
					comThumbBox.Image = comThumb;
			if (!string.IsNullOrEmpty(editAi.hostId))
				if (ThumbnailManager.isExist(editAi.hostId, out userThumb))
					userThumbBox.Image = userThumb;
			isMustComChkBox.Checked = editAi.isMustCom;
			isMustUserChkBox.Checked = editAi.isMustUser;
			isMustKeywordChkBox.Checked = editAi.isMustKeyword;
			customKw = editAi.cki;
			if (customKw != null) customKeywordBtn.Text = "カスタム設定(" + customKw[0].name + ")";
			if (editAi.isCustomKeyword) isCustomKeywordRadioBtn.Checked = true;
			isAutoReserveChkBox.Checked = editAi.isAutoReserve;
		}
		private void setDefaultBehavior() {
			var conf = form.config.get("defaultBehavior");
			var l = conf.Split(',');
			for (var i = 0; i < l.Length; i++) {
				var d = l[i].Split(':');
				if (d.Length == 1) {
					if (typeof(CheckBox) != behaviorGroupBox.Controls[i].GetType()) 
						continue;
					CheckBox chkbox = (CheckBox)behaviorGroupBox.Controls[i];
					chkbox.Checked = l[i] == "1";
				} else {
					var c = behaviorGroupBox.Controls[d[0]];
					if (c is CheckBox)
						((CheckBox)c).Checked = bool.Parse(d[1]);
				}
			}
			
			textColorBtn.BackColor = sampleColorText.ForeColor = ColorTranslator.FromHtml(form.config.get("defaultTextColor"));
			backColorBtn.BackColor = sampleColorText.BackColor = ColorTranslator.FromHtml(form.config.get("defaultBackColor"));
			defaultSoundList.SelectedIndex = int.Parse(form.config.get("defaultSound"));
			isDefaultSoundIdChkBox.Checked = bool.Parse(form.config.get("IsDefaultSoundId"));
			//memberOnlyList.SelectedIndex = 0;
			isAutoReserveChkBox.Checked = bool.Parse(form.config.get("IsDefaultAutoReserve"));
		}
		private void setMemberOnlyListInit(AlartInfo editAi) {
			var c = editAi.memberOnlyMode;
			
			if (editAi.memberOnlyMode == null) editAi.memberOnlyMode = "True,True,True";
			if (c.IndexOf(",") == -1) {
				if (c == "0") editAi.memberOnlyMode = "True,True,True";
				else if (c == "1") editAi.memberOnlyMode = "True,False,False";
				else if (c == "2") editAi.memberOnlyMode = "False,True,True";
			}
			
			var types = editAi.memberOnlyMode.Split(',');
			memberOnlyCheckList.SetItemChecked(1, bool.Parse(types[0]));
			memberOnlyCheckList.SetItemChecked(2, bool.Parse(types[1]));
			memberOnlyCheckList.SetItemChecked(3, bool.Parse(types[2]));
			if (memberOnlyCheckList.CheckedIndices.Count == 3)
				memberOnlyCheckList.SetItemChecked(0, true);
			closeMemberOnlyList();
		}
		void TextColorBtnClick(object sender, EventArgs e)
		{
			var c = new ColorDialog();
			c.FullOpen = true;
			c.Color = textColorBtn.BackColor;
			if (c.ShowDialog() != DialogResult.OK) return;
			textColorBtn.BackColor = c.Color;
			sampleColorText.ForeColor = c.Color;
		}
		void BackColorBtnClick(object sender, EventArgs e)
		{
			var c = new ColorDialog();
			c.FullOpen = true;
			c.Color = backColorBtn.BackColor;
			if (c.ShowDialog() != DialogResult.OK) return;
			backColorBtn.BackColor = c.Color;
			sampleColorText.BackColor = c.Color;
		}
		void DefaultColorBtnClick(object sender, EventArgs e)
		{
			backColorBtn.BackColor = Color.FromArgb(255,224,255);
			sampleColorText.BackColor = Color.FromArgb(255,224,255);
			textColorBtn.BackColor = Color.Black;
			sampleColorText.ForeColor = Color.Black;
		}
		void CustomKeywordBtnClick(object sender, EventArgs e)
		{
			var f = new CustomKeywordForm(int.Parse(form.config.get("fontSize")), true, customKw);
			f.ShowDialog();
			if (f.ret == null) {
				if (f.DialogResult == DialogResult.OK) {
					customKeywordBtn.Text = "カスタム設定";
					customKw = null;
				}
				return;
			}
			customKw = f.ret;
			customKeywordBtn.Text = "カスタム設定(" + customKw[0].name + ")";
		}
		
		void LinkLabel1LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			var url = "\"" + util.getJarPath()[0] + "/readme.html\"";
			util.openUrlBrowser(url, form.config);
		}
		
		void IsDefaultSoundIdChkBoxCheckedChanged(object sender, EventArgs e)
		{
			 
			var comId = util.getRegGroup(communityId.Text, "(c[oh]\\d+)");
			var userId = util.getRegGroup(userIdText.Text, "(\\d+)");
			existSoundFileLabel.Visible =
					isDefaultSoundIdChkBox.Checked &&
				 		(!(comId == null || !File.Exists(util.getJarPath()[0] + "/Sound/" + comId + ".wav")) ||
				 		!(userId == null || !File.Exists(util.getJarPath()[0] + "/Sound/" + userId + ".wav")));
			
		}
		void setUserModeForm() {
			label1.Visible = label2.Visible = 
					communityId.Visible = communityNameText.Visible =
					getCommunityInfoBtn.Visible = communityFollowChkBox.Visible = 
					isMustComChkBox.Visible = 
					label6.Visible = label9.Visible = 
					keywordText.Visible = customKeywordBtn.Visible =
					isCustomKeywordRadioBtn.Visible = isMustKeywordChkBox.Visible = 
					isSimpleKeywordRadioBtn.Visible = officialBtn.Visible = false;
		}
		
		void OfficialBtnClick(object sender, EventArgs e)
		{
			communityId.Text = "official";
			getCommunityInfoBtn.PerformClick();
		}
		
		void MemberOnlyListDropDown(object sender, EventArgs e)
		{
			//memberOnlyCheckList.Visible = true;
		}
		
		void MemberOnlyCheckListLeave(object sender, EventArgs e)
		{
			util.debugWriteLine("MemberOnlyCheckListLeave " + memberOnlyList.Focused);
			if (!memberOnlyList.Focused) {
				closeMemberOnlyList();
			}
		}
		void MemberOnlyListLeave(object sender, EventArgs e)
		{
			util.debugWriteLine("MemberOnlyListLeave " + memberOnlyCheckList.Focused);
			if (!memberOnlyCheckList.Focused) {
				closeMemberOnlyList();
			}
		}
		
		void MemberOnlyListMouseDown(object sender, MouseEventArgs e)
		{
			util.debugWriteLine("MemberOnlyListMouseDown");
			util.debugWriteLine("MemberOnlyListMouseDown " + " " + memberOnlyCheckList.Visible);
			if (memberOnlyCheckList.Visible) {
				closeMemberOnlyList();
				memberOnlyList.DroppedDown = false;
			} else {
				memberOnlyCheckList.Visible = true;
				memberOnlyCheckList.Focus();
			}
		}
		
		void MemberOnlyCheckListClick(object sender, EventArgs e)
		{
			
		}
		
		void MemberOnlyListDropDownClosed(object sender, EventArgs e)
		{
			util.debugWriteLine("close");
			if (memberOnlyCheckList.Focused) {
				//memberOnlyList.DroppedDown = true;
			} else {
				var l = memberOnlyCheckList.Location;
				//var rec = RectangleToScreen(new Rectangle(l.X, l.Y, 0,0));
				var lefttop = memberOnlyCheckList.PointToScreen(new Point(0,0));
				var rightbottom = memberOnlyCheckList.PointToScreen(new Point(Width,Height));
				
				var x = MousePosition.X;
				var y = MousePosition.Y;
				if (x < lefttop.X || 
					     y < lefttop.Y ||
					     x > rightbottom.X || 
					     y > rightbottom.Y) {
					closeMemberOnlyList();
				}
				else memberOnlyList.DroppedDown = true;
			}
		}
		void closeMemberOnlyList() {
			memberOnlyCheckList.Visible = false;
			var t = "";
			var indices = memberOnlyCheckList.CheckedIndices;
			if (indices.IndexOf(0) > -1) t = "条件を設定しない";
			else {
				foreach (int i in indices) {
					if (t != "") t += "・";
					t += ((string)memberOnlyCheckList.Items[i]).Substring(0, 2);
				}
				t = t == "" ? "何も通知しない" : (t + "放送を通知する");
			}
			memberOnlyList.Items.Add(t);
			//var a = memberOnlyList.Items;
			memberOnlyList.SelectedIndex = -1;
			memberOnlyList.SelectedItem = null;
			
			memberOnlyList.Text = t;
			//memberOnlyList.SelectedText = t;
		}
		
		void MemberOnlyCheckListItemCheck(object sender, ItemCheckEventArgs e)
		{
			util.debugWriteLine(e.Index + " " + (e.NewValue == CheckState.Checked));
			var selectI = memberOnlyCheckList.SelectedIndex;
			if (selectI != e.Index) return;
			try {
				if (e.Index == 3 && e.NewValue == CheckState.Unchecked ||
					e.Index == 0 && memberOnlyCheckList.GetItemChecked(0) == true) {
					DialogResult result = util.showMessageBoxCenterForm(this, "無料部分（チラ見せ）のある有料放送も通知されなくなります");
				}
				if (e.Index == 0) {
					for (var i = 1; i < memberOnlyCheckList.Items.Count; i++) {
						memberOnlyCheckList.SetItemChecked(i, e.NewValue == CheckState.Checked);
					}
				} else {
					var isAllCheck = true;
					for (var i = 1; i < memberOnlyCheckList.Items.Count; i++) {
						var c = e.Index == i ? e.NewValue == CheckState.Checked : memberOnlyCheckList.GetItemChecked(i); 
						if (!c) isAllCheck = false;
					}
					if (isAllCheck) memberOnlyCheckList.SetItemChecked(0, true);
					else memberOnlyCheckList.SetItemChecked(0, false);
				}
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		private string getMemberOnly() {
			var ret = "";
			for (var i = 1; i < memberOnlyCheckList.Items.Count; i++) {
				if (i != 1) ret += ",";
				ret += memberOnlyCheckList.CheckedIndices.IndexOf(i) > -1;
			}
			return Array.IndexOf(ret.Split(','), "True") == -1 ? null : ret;
		}
		
		void IsOfficialChkBtnCheckedChanged(object sender, EventArgs e)
		{
			if (isOfficialChkBtn.Checked) {
				communityId.Text = "official";
				communityId.Enabled = false;
				getCommunityInfoBtn.PerformClick();
			} else {
				communityId.Text = "";
				communityNameText.Text = "";
				communityId.Enabled = true;
				getCommunityInfoBtn.PerformClick();
			}
		}
		void setAppliName() {
			for (var i = 0; i < 10; i++) {
				var n = "appli" + (char)('A' + i);
				var c = form.config.get(n + "Name");
				if (string.IsNullOrEmpty(c)) continue;
				Controls.Find(n + "ChkBox", true)[0].Text =	"ｱﾌﾟﾘ" + ((char)('A' + i)) + "(" + c + ")";
			}
		}
	}

}