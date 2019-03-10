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
using namaichi.info;
using namaichi.rec;

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
		public addForm(MainForm form, string id)
		{
			this.form = form;
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			if (id == null) return;
			
			if (id.StartsWith("lv")) hosoIdText.Text = id;
			else if (id.StartsWith("c")) {
				if (bool.Parse(form.config.get("IsdragCom"))) {
					hosoIdText.Text = id;
				}
				communityId.Text = id;
			}
			else userIdText.Text = id; 
		}
		
		void Button4Click(object sender, EventArgs e)
		{
			Close();
		}
		
		void Button3Click(object sender, EventArgs e)
		{
			var comId = util.getRegGroup(communityId.Text, "((ch|co)*\\d+)");
			var userId = util.getRegGroup(userIdText.Text, "(\\d+)");
			communityNameText.Text = "";
			userNameText.Text = "";
			if (comId != null) GetCommunityInfoBtnClick(null, null);
			if (userId != null) GetUserInfoBtnClick(null, null);
			if (communityNameText.Text == "" && userNameText.Text == "" && keywordText.Text == "") {
				MessageBox.Show("有効なコミュニティIDかユーザーIDかキーワードが入力されていないです");
				return;
			}
			var now = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
			var addDate = now;//now.Substring(0, now.Length - 3);
			var comFollow = (communityFollowChkBox.Checked) ? "フォロー解除する" : "フォローする";
			var userFollow = (userFollowChkBox.Checked) ? "フォロー解除する" : "フォローする";
			if (communityNameText.Text == "") comFollow = "";
			if (userNameText.Text == "") userFollow = "";
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
					userFollow, "", keywordText.Text);
			if (!duplicationCheckOk(_ret)) return;
			ret = _ret;
			Close();
		}
		void GetCommunityInfoBtnClick(object sender, EventArgs e)
		{
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
		}
		
		
		void GetUserInfoBtnClick(object sender, EventArgs e)
		{
			var num = util.getRegGroup(userIdText.Text, "(\\d+)");
			if (num == null) return;
			userIdText.Text = num;
			var isFollow = false;
			var userName = util.getUserName(num, out isFollow, form.check.container);
			if (userName == null) return;
			userFollowChkBox.Checked = isFollow;
			userNameText.Text = userName;
		}
		
		void GetInfoFromHosoIdBtnClick(object sender, EventArgs e)
		{
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
				MessageBox.Show("しっぱい");
			
			communityId.Text = communityNameText.Text = 
					userIdText.Text = userNameText.Text = "";
			communityFollowChkBox.Checked = userFollowChkBox.Checked = false;
			
			var url =  "http://live2.nicovideo.jp/watch/" + id;
			var hig = new HosoInfoGetter();
			hig.get(url);
			
			communityId.Text = hig.communityId;
			userIdText.Text = hig.userId;
			GetCommunityInfoBtnClick(null, null);
			GetUserInfoBtnClick(null, null);
			
			if (hig.communityId == null && hig.userId == null)
				MessageBox.Show(hig.type == "official" ? "公式放送でした" : "しっぱい");
				
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
					MessageBox.Show(hig.type == "official" ? "公式放送でした" : "しっぱい");
			}
			*/
		}
		bool duplicationCheckOk(AlartInfo ai) {
			try {
				var count = form.getAlartListCount();
				for (var i = 0; i < count; i++) {
					if (ai.communityId != null && ai.communityId != "" && form.alartListDataSource[i].communityId == 
					    	ai.communityId) {
						var m = (ai.communityId.StartsWith("co")) ? "コミュニティ" : "チャンネル";
						
					    //var res = MessageBox.Show(m + "ID" + ai.communityId + "は既に登録されています。削除しますか？(はい＝削除　いいえ＝削除　キャンセル＝フォームに戻る)", "確認", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
					    form.setAlartListScrollIndex(i);
					    var res = MessageBox.Show(m + "ID:" + ai.communityId + "は既に登録されています。\n(" + form.alartListDataSource[i].toString() + ")\n\n既に存在している行を削除しますか？\nはい=削除して登録　いいえ=既に存在する行を削除せず登録　キャンセル=登録しない", "確認", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
					    
					    if (res == DialogResult.Yes) {
					    	form.alartListRemove(form.alartListDataSource[i]);
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
				count = form.getAlartListCount();
				for (var i = 0; i < count; i++) {
					if (ai.hostId != null && ai.hostId != "" && form.alartListDataSource[i].hostId ==
						   ai.hostId) {
						var m = "ユーザー";
					    //var res = MessageBox.Show(m + "ID" + ai.communityId + "は既に登録されています。削除しますか？(はい＝削除して登録　いいえ＝登録　キャンセル＝フォームに戻る)", "確認", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
					    form.setAlartListScrollIndex(i);
					    var res = MessageBox.Show(m + "ID:" + ai.hostId + "は既に登録されています。\n(" + form.alartListDataSource[i].toString() + ")\n\n既に存在している行を削除しますか？\nはい=削除して登録　いいえ=既に存在する行を削除せず登録　キャンセル=登録しない", "確認", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
					    
					    if (res == DialogResult.Yes) {
					    	form.alartListRemove(form.alartListDataSource[i]);
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
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				return false;
			}
			return true;
		}
		
		void AddFormLoad(object sender, EventArgs e)
		{
			if (hosoIdText.Text != "") getInfoFromHosoIdBtn.PerformClick();
			if (communityId.Text != "") getCommunityInfoBtn.PerformClick();
			if (userIdText.Text != "") getUserInfoBtn.PerformClick();
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

	}
}
