/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2019/03/27
 * Time: 8:41
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Net;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using namaichi.info;
using namaichi.alart;
using SuperSocket.ClientEngine;

namespace namaichi.rec
{
	/// <summary>
	/// Description of ExistCoChUserChecker.
	/// </summary>
	public class ToolMenuProcess
	{
		private MainForm form;
		ToolMenuLock bulkAddFromFollowComLock = null;
		ToolMenuLock setUserInfoLock = null;
		ToolMenuLock coChExistsCheckLock = null;
		ToolMenuLock userExistsCheckLock = null;
		ToolMenuLock comThumbLock = null;
		ToolMenuLock userThumbLock = null;
		
		public ToolMenuProcess(MainForm form)
		{
			this.form = form;
		}
		public void exitsCheckClicked(bool isUser) {
			var str = isUser ? "ユーザー" : "コミュニティ";
			if ((isUser && userExistsCheckLock != null) ||
			    	(!isUser && coChExistsCheckLock != null)) {
				var res = form.showMessageBox("既に" + str + "存在チェック実行中です。中断しますか？", 
						"確認", MessageBoxButtons.OKCancel, 
						MessageBoxIcon.Warning, 
						MessageBoxDefaultButton.Button2);
				if (res == DialogResult.Cancel) return;
				setExistsCheckLock(isUser, null);
				setToolMenuStatusBar();
			} else {
				var res = form.showMessageBox("チェック完了までに" + str + "ID登録件数の\"約2倍の秒数\"がかかります。チェックしますか？",
						"確認", MessageBoxButtons.OKCancel, 
						MessageBoxIcon.Warning, 
						MessageBoxDefaultButton.Button2);
				if (res == DialogResult.Cancel) return;
				var l = isUser ? new ToolMenuLock("ユーザー存在チェック中", -1) : new ToolMenuLock("コミュ存在チェック中", -1); 
				setExistsCheckLock(isUser, l);
				setToolMenuStatusBar();
				Task.Factory.StartNew(() => existsCheck(l, form, str, isUser));
			}
		}
		private void existsCheck(ToolMenuLock _lock, MainForm form, string str, bool isUser) {
			var idList = new List<string>();
			while (true) {
				try {
					idList.Clear();
					foreach (var ai in form.alartListDataSource) {
						var id = isUser ? ai.hostId : ai.communityId;
						if (string.IsNullOrEmpty(id)) continue;
						if (id == "official") continue;
						idList.Add(id);
					}
					foreach (var ai in form.userAlartListDataSource) {
						var id = isUser ? ai.hostId : ai.communityId;
						if (string.IsNullOrEmpty(id)) continue;
						idList.Add(id);
					}
					break;
				} catch (Exception e) {
					util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				}
			}
			_lock.all = idList.Count;
			setToolMenuStatusBar();
			
			//var notExistList = new List<string>();
			int exist = 0, delete = 0, error = 0;
			for (var i = 0; i < idList.Count; i++) {
				var id = idList[i];
				if ((isUser && userExistsCheckLock != _lock) ||
				    	(!isUser && coChExistsCheckLock != _lock)) {
					util.showModelessMessageBox(str + "存在チェックが中断されました", "", form);
					setToolMenuStatusBar();
					return;
				}
				
				bool isError, isExist;
				string name = null;
				getExistsInfo(id, out isError, out isExist, out name);
				
				if (isError) {
					error++;
					form.alartListExistColorChange(id, isUser, 2);
				} else {
					if (isExist) {
						exist++;
						form.alartListExistColorChange(id, isUser, 0);
						if (name != null)
							setName(id, isUser, form, name);
					}
					else {
						delete++;
						form.alartListExistColorChange(id, isUser, 1);
					}
				}
				_lock.end = i;
				setToolMenuStatusBar();
				Thread.Sleep(2000);
			}
			util.showModelessMessageBox("存在：" + exist + "　削除：" + delete + "　エラー：" + error, str + "存在チェック終了", form);
			setExistsCheckLock(isUser, null);
			setToolMenuStatusBar();
			//コミュニティ存在チェック終了
			//存在：128　　削除：0　エラー：159
		}
		private void getExistsInfo(string id, out bool isError, out bool isExist, out string name) {
			isError = isExist = false;
			name = null;
			var isThumb = form.check.container == null && id.StartsWith("co");
			if (isThumb) {
				var url = "https://ext.nicovideo.jp/thumb_" + 
					(id.StartsWith("c") ? (id.StartsWith("co") ? "community" : "channel") : "user") + "/" + id;
				var res = util.getPageSource(url);
				if (res == null) isError = true;
				else {
					isExist = (id.StartsWith("ch") && res.IndexOf("<h1 id=\"chSymbol\"><a href=\"https://ch.nicovideo.jp/channel/\" target=\"_blank\">") == -1) ||
						(id.StartsWith("co") && res.IndexOf("<p class=\"TXT12\">お探しのコミュニティは存在しないか") == -1) ||
						(!id.StartsWith("c") && res.IndexOf("<p class=\"TXT10\">ユーザーID：<strong>") > -1);
					if (isExist) name = util.getRegGroup(res, "name=\"(.+?)\"");
				}
			} else {
				if (id.StartsWith("c")) {
					var url = id.StartsWith("co") ? ("https://com.nicovideo.jp/api/v1/communities/" + id.Substring(2) + "/authority.json") :
				 			("https://secure-dcdn.cdn.nimg.jp/comch/channel-icon/128x128/" + id + ".jpg");
					var r = util.getPageSource(url);
					if (r == null) isError = true;
					else isExist = true;
				} else {
					var isFollow = false;
					name = util.getUserName(id, out isFollow, form.check.container, false, form.config);
					if (name != null) isExist = true;
					else isError = true;
				}
			}
			util.debugWriteLine("aa");
		}
		private void setExistsCheckLock(bool isUser, ToolMenuLock val) {
			if (isUser) userExistsCheckLock = val;
			else coChExistsCheckLock = val;
		}
		private void setName(string id, bool isUser, MainForm form, string name) {
			
			while (true) {
				try {
					foreach (var ai in form.alartListDataSource) {
						if (isUser && ai.hostId == id 
						    	&& string.IsNullOrEmpty(ai.hostName)) {
							//string name = util.getRegGroup(res, "name=\"(.+?)\"");
							form.alartListSetName(ai, isUser, name);
						} else if (!isUser && ai.communityId == id 
						    	&& string.IsNullOrEmpty(ai.communityName)) {
							//if (id.StartsWith("ch")) name = util.getRegGroup(res, "name=\"(.+?)\"");
							//else name = util.getRegGroup(res, "name=\"(.+?)\"");
							form.alartListSetName(ai, isUser, name);
						}
					}
					foreach (var ai in form.userAlartListDataSource) {
						if (isUser && ai.hostId == id 
						    	&& string.IsNullOrEmpty(ai.hostName)) {
							//string name = util.getRegGroup(res, "name=\"(.+?)\"");
							form.alartListSetName(ai, isUser, name);
						} else if (!isUser && ai.communityId == id 
						    	&& string.IsNullOrEmpty(ai.communityName)) {
							//if (id.StartsWith("ch")) name = util.getRegGroup(res, "name=\"(.+?)\"");
							//else name = util.getRegGroup(res, "name=\"(.+?)\"");
							form.alartListSetName(ai, isUser, name);
						}
					}
					break;
				} catch (Exception e) {
					util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				}
			}
		}
		private void setToolMenuStatusBar() {
			string t = "";
			if (bulkAddFromFollowComLock != null) t += bulkAddFromFollowComLock.getLabel();
			if (setUserInfoLock != null) t += setUserInfoLock.getLabel();
			if (coChExistsCheckLock != null) t += coChExistsCheckLock.getLabel();
			if (userExistsCheckLock != null) t += userExistsCheckLock.getLabel();
			if (comThumbLock != null) t += comThumbLock.getLabel();
			if (userThumbLock != null) t += userThumbLock.getLabel();
	        
			form.setToolMenuStatusBar(t);
		}
		public void getUserInfoFromComStart() 
		{
			if (setUserInfoLock != null) {
				var res = form.showMessageBox("既にユーザー情報取得実行中です。中断しますか？", 
						"確認", MessageBoxButtons.OKCancel, 
						MessageBoxIcon.Warning, 
						MessageBoxDefaultButton.Button2);
				if (res == DialogResult.Cancel) return;
				setUserInfoLock = null;
				setToolMenuStatusBar();
			} else {
				var res = form.showMessageBox("取得完了までに未取得ユーザーID数の\"約2倍の秒数\"がかかります。取得しますか？", 
						"確認", MessageBoxButtons.OKCancel, 
						MessageBoxIcon.Warning, 
						MessageBoxDefaultButton.Button2);
				if (res == DialogResult.Cancel) return;
				var l = new ToolMenuLock("未取得ユーザー取得中", -1);
				setUserInfoLock = l;
				setToolMenuStatusBar();
				Task.Factory.StartNew(() => getUserInfoFromCom(l, form));
			}
		}
		private void getUserInfoFromCom(ToolMenuLock _lock, MainForm form) {
			var getAiList = new List<AlartInfo>();
			while (true) {
				try {
					foreach (var ai in form.alartListDataSource) {
						if (!string.IsNullOrEmpty(ai.communityId) &&
								ai.communityId.StartsWith("co") &&
							    (string.IsNullOrEmpty(ai.hostId) || 
							     string.IsNullOrEmpty(ai.hostName)))
							if (getAiList.IndexOf(ai) == -1)
								getAiList.Add(ai);
					}
					
					break;
				} catch (Exception e) {
					util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				}
			}
			_lock.all = getAiList.Count;
			setToolMenuStatusBar();
			
			int got = 0, error = 0;
			for (var i = 0; i < getAiList.Count; i++) {
				var ai = getAiList[i];
				if (setUserInfoLock != _lock) {
					util.showModelessMessageBox("未取得ユーザー情報取得が中断されました", "", form);
					setToolMenuStatusBar();
					return;
				}
				var hig = new HosoInfoGetter();
				var r = hig.get("https://live.nicovideo.jp/watch/" + ai.communityId, form.check.container);
				if (!r) {
					error++;
					_lock.end = i;
					setToolMenuStatusBar();
					continue;
				}
				var isFollow = false;
				var name = util.getUserName(hig.userId, out isFollow, form.check.container, true, form.config);
				if (string.IsNullOrEmpty(name)) {
					error++;
					_lock.end = i;
					setToolMenuStatusBar();
					continue;
				}
				form.formAction(() => {
	           		ai.hostId = hig.userId;
	           		ai.hostName = name;
	           		if (form.check.container == null) ai.hostFollow = "";
	           		else ai.hostFollow = isFollow ? "フォロー解除する" : "フォローする";
	           		var j = form.alartListDataSource.IndexOf(ai);
	           		if (j > -1) {
	           			form.alartList.UpdateCellValue(1, j);
	           			form.alartList.UpdateCellValue(3, j);
	           		}
	           		
				});
				got++;
				_lock.end = i;
				setToolMenuStatusBar();
				Thread.Sleep(2000);
			}
			util.showModelessMessageBox("取得：" + got + "　失敗：" + error, "未取得ユーザー取得終了", form);
			setUserInfoLock = null;
			setToolMenuStatusBar();
		}
		public void addBulkFromFollowComStart() 
		{
			if (bulkAddFromFollowComLock != null) {
				var res = form.showMessageBox("既に参加コミュ一括登録実行中です。中断しますか？", 
						"確認", MessageBoxButtons.OKCancel, 
						MessageBoxIcon.Warning, 
						MessageBoxDefaultButton.Button2);
				if (res == DialogResult.Cancel) return;
				bulkAddFromFollowComLock = null;
				setToolMenuStatusBar();
			} else {
				Task.Factory.StartNew(() => {
					try {
						var f = new BulkAddFromFollowAccountForm(int.Parse(form.config.get("fontSize")));
						Task.Factory.StartNew(() => {
							form.formAction(() => f.ShowDialog(form));
						}).Wait();
						
						if (f.mail == null) return;
						
						if (f.mail == "" || f.pass == "") {
							util.showModelessMessageBox("メールアドレスとパスワードを入力してください", "", form);
							return;
						}
								
						var cc = getUserSession(f.mail, f.pass);
						if (cc == null) return;
						
						/*
						var res = util.getPageSource("https://www.nicovideo.jp/my/", cc);
						if (res == null) {
							form.showMessageBox("マイページが取得できませんでした", "");
							return;
						}
						var name = util.getRegGroup(res, "<span id=\"siteHeaderUserNickNameContainer\">(.+?)</span>");
						var id = util.getRegGroup(res, "User = \\{ id: (\\d+)");
						if (name == null || id == null) {
							form.showMessageBox("マイページからの取得に失敗しました", "");
							return;
						}
						*/
						var us = cc.GetCookies(new Uri("https://www.nicovideo.jp"))["user_session"];
						if (us == null) {
							form.addLogText("Cookie内にセッション情報が見つかりませんでした");
							return;
						}
						var id = util.getRegGroup(us.ToString(), "(\\d+)");
						var name = util.getMyName(cc, util.getRegGroup(us.ToString(), "=(.+)"));
						
						var followList = new FollowChecker(form, cc).getFollowList(f.follow);
						if (followList == null) {
							form.addLogText("フォローリストが見つかりませんでした");
							return;
						}
						var addFollowList = getAddFollowList(followList);
						if (addFollowList == null) return;
						var isStartRet = -1;
						Task.Factory.StartNew(() =>
								isStartRet = util.showModelessMessageBox(name + "(" + id + ") の" + (f.isAddToCom ? "参加コミュ" : "フォローユーザーID") + "は\r\n未登録：" + addFollowList.Count + "件　登録済み：" + (followList.Count - addFollowList.Count) + "　です。\r\n未登録の" + (f.isAddToCom ? "参加コミュニティ" : "フォローユーザーID") + "を登録しますか？", "確認", form, 1 | 0x100 | 0x20)
						).Wait();
						if (isStartRet != 1) return;
						
						var l = new ToolMenuLock((f.isAddToCom ? "参加コミュ" : "フォローユーザー") + "一括登録中", addFollowList.Count);
						bulkAddFromFollowComLock = l;
						setToolMenuStatusBar();
						Task.Factory.StartNew(() => bulkAddFromFollowCom(l, addFollowList, followList.Count, f.follow, cc, f.isAddToCom));
						
					} catch (Exception e) {
						util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
						form.addLogText("一括登録中に未知のエラーが発生しました" + e.Message + e.Source + e.StackTrace + e.TargetSite);
					}
				});
				
			}
		}
		private CookieContainer getUserSession(string mail, string pass) {
			var TargetUrl = new Uri("https://live.nicovideo.jp/");
			
			try {
				var cg = new rec.CookieGetter(form.config, form);
				//var cc = cg.getAccountCookie(mail, pass).Result;
				var cc = cg.getAccountCookie(mail, pass).Result;
				
				if (cc == null || 
				    	cc.GetCookies(TargetUrl)["user_session"] == null) {
					util.showModelessMessageBox("失敗：メールアドレスまたはパスワードが間違っているため、ログインできません", "", form);
					return null;
				}
				
				return cc;
			} catch (Exception e) {
				util.showModelessMessageBox("失敗：メールアドレスまたはパスワードが間違っているため、ログインできません", "", form);
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				return null;
			}
		}
		private List<string[]> getAddFollowList(List<string[]> followList) {
			try {
				var noList = new List<string[]>();
	//			foreach (var _followList in followList) {
					foreach (var id in followList) {
						while (true) {
							try {
								var isContain = false;
								foreach (var ai in form.alartListDataSource) {
									if ((id[0].StartsWith("c") && id[0] == ai.communityId) ||
									    (!id[0].StartsWith("c") && id[0] == ai.hostId))
										isContain = true;
								}
								foreach (var ai in form.userAlartListDataSource) {
									if ((id[0].StartsWith("c") && id[0] == ai.communityId) ||
									    (!id[0].StartsWith("c") && id[0] == ai.hostId))
										isContain = true;
								}
								if (!isContain) noList.Add(id);
								break;
							} catch (Exception e) {
								util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
							}
						}
					}
	//			}
				return noList;
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				form.addLogText("追加するIDの作成中にエラーが発生しました" + e.Message + e.Source + e.StackTrace + e.TargetSite);
				return null;
			}
		}
		private void bulkAddFromFollowCom(ToolMenuLock _lock, List<string[]> addList, int allNum, bool[] followMode, CookieContainer cc, bool isAddToCom) {
			var mainFollowList = new FollowChecker(form, cc)
					.getFollowList(followMode);
			if (mainFollowList == null) return;
			var behaviors = form.config.get("defaultBehavior").Split(',').Select<string, bool>(x => x == "1").ToArray();
			var textColor = ColorTranslator.FromHtml(form.config.get("defaultTextColor"));
			var backColor = ColorTranslator.FromHtml(form.config.get("defaultBackColor"));
			var isAutoReserve = bool.Parse(form.config.get("IsDefaultAutoReserve"));
			int got = 0, error = 0;
			for (var i = 0; i < addList.Count; i++) {
				var id = addList[i];
				if (bulkAddFromFollowComLock != _lock) {
					util.showModelessMessageBox("参加コミュ一括登録が中断されました", "", form);
					setToolMenuStatusBar();
					return;
				}
				var isUser = id[0].StartsWith("c");
				var comId = isUser ? id[0] : "";
				var comName = isUser ? id[1] : "";
				var comFollow = (!isUser && mainFollowList.Find((x) => id[0] == x[0]) != null) ?
					"フォロー解除する" : "フォローする";
				if (comId == "") comFollow = "";
				var userId = isUser ? "" : id[0];
				var userName = isUser ? "" : id[1];
				var userFollow = (isUser && mainFollowList.Find((x) => id[0] == x[0]) != null) ?
					"フォロー解除する" : "フォローする";
				if (userId == "") userFollow = "";
				
				var now = DateTime.Now.ToString("yyyy\"/\"MM\"/\"dd HH\":\"mm\":\"ss");
				var ai = new AlartInfo(comId, userId, 
						comName, userName, "", now, false, false, 
						false, false, false, false, false, 
						false, false, false, false, false, 
						false, false, false, "", 
						comFollow, userFollow, "", "", "True,True,True", 
						isAutoReserve, 0);
				ai.setBehavior(behaviors);
				ai.textColor = textColor;
				ai.backColor = backColor;
				
				form.formAction(() => {
					var ds = isAddToCom ? form.alartListDataSource : 
							form.userAlartListDataSource;
					ds.Add(ai);
				});
				got++;
				_lock.end = i;
				setToolMenuStatusBar();
				//Thread.Sleep(2000);
			}
			util.showModelessMessageBox("新規登録：" + got + "　登録済み：" + (allNum - got) + "　エラー：" + error, "参加コミュ登録完了", form);
			bulkAddFromFollowComLock = null;
			setToolMenuStatusBar();
			//form.changedListContent();
			new utility.AlartListFileManager(false, form).save();
		}
		public void getThumbBulk(bool isUser) {
			if ((isUser && userThumbLock != null) ||
			   		(!isUser && comThumbLock != null)) {
				var res = form.showMessageBox("既に未取得ユーザが取得実行中です。中断しますか？", 
						"確認", MessageBoxButtons.OKCancel, 
						MessageBoxIcon.Warning, 
						MessageBoxDefaultButton.Button2);
				if (res == DialogResult.Cancel) return;
				if (isUser) userThumbLock = null;
				else comThumbLock = null;
				setToolMenuStatusBar();
			} else {
				var res = form.showMessageBox("取得完了までに未取得" + (isUser ? "ユーザ" : "コミュ") + "画数のの\"約3倍の秒数\"がかかります。チェックしますか？",
						"確認", MessageBoxButtons.OKCancel, 
						MessageBoxIcon.Warning, 
						MessageBoxDefaultButton.Button2);
				if (res == DialogResult.Cancel) return;
				var l = isUser ? new ToolMenuLock("未取得ユーザ画取得中", -1) : new ToolMenuLock("未取得コミュ画取得中", -1); 
				if (isUser) userThumbLock = l;
				else comThumbLock = l;
				setToolMenuStatusBar();
				Task.Factory.StartNew(() => getThumbBulkCore(l, form, isUser));
			}
		}
		public void getThumbBulkCore(ToolMenuLock _lock, MainForm form, bool isUser) {
			var getAiList = new List<AlartInfo>();
			while (true) {
				try {
					foreach (var ai in form.alartListDataSource) {
						Image img = null;
						if (isUser && 
							    !string.IsNullOrEmpty(ai.hostId) &&
							    !ThumbnailManager.isExist(ai.hostId, out img) &&
								getAiList.IndexOf(ai) == -1)
							getAiList.Add(ai);
						if (!isUser && 
							    !string.IsNullOrEmpty(ai.communityId) &&
							    !ThumbnailManager.isExist(ai.communityId, out img) &&
								getAiList.IndexOf(ai) == -1)
							getAiList.Add(ai);
					}
					foreach (var ai in form.userAlartListDataSource) {
						Image img = null;
						if (isUser && 
							    !string.IsNullOrEmpty(ai.hostId) &&
							    !ThumbnailManager.isExist(ai.hostId, out img) &&
								getAiList.IndexOf(ai) == -1)
							getAiList.Add(ai);
						if (!isUser && 
							    !string.IsNullOrEmpty(ai.communityId) &&
							    !ThumbnailManager.isExist(ai.communityId, out img) &&
								getAiList.IndexOf(ai) == -1)
							getAiList.Add(ai);
					}
					break;
				} catch (Exception e) {
					util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				}
			}
			_lock.all = getAiList.Count;
			setToolMenuStatusBar();
			
			int got = 0, error = 0, no = 0;
			for (var i = 0; i < getAiList.Count; i++) {
				var ai = getAiList[i];
				if ((isUser && userThumbLock != _lock) ||
				    	(!isUser && comThumbLock != _lock)) {
					util.showModelessMessageBox("未取得" + (isUser ? "ユーザ" : "コミュ") + "画取得が中断されました", "", form);
					setToolMenuStatusBar();
					return;
				}
				var id = isUser ? ai.hostId : ai.communityId;
				var img = ThumbnailManager.getImageId(id, form);
				if (img == null) {
					error++;
					form.alartListExistColorChange(id, isUser, 2);
				} else {
					ThumbnailManager.saveImage(img, id);
					got++;
					form.alartListExistColorChange(id, isUser, 0);
				}
				_lock.end = i;
				setToolMenuStatusBar();
				Thread.Sleep(3000);
			}
			util.showModelessMessageBox("取得：" + got + "　画像無し：" + no + "　エラー：" + error, "未取得" + (isUser ? "ユーザ" : "コミュ") + "画取得終了", form);
			if (isUser) userThumbLock = null;
			else comThumbLock = null;
			setToolMenuStatusBar();
		}
		public class ToolMenuLock {
			private string mode = null;
			public int all;
			public int end;
			public ToolMenuLock (string mode, int all) {
				this.mode = mode;
				this.all = all;
			}
			public string getLabel() {
				return all != -1 ? ("[" + mode + "(" + end + "/" + all + "]")
						:  ("[" + mode + "]");
			}
		}
	}
}
