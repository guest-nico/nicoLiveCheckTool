/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2019/01/08
 * Time: 14:32
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;
using namaichi.info;
using namaichi.alart;

namespace namaichi.utility
{
	/// <summary>
	/// Description of AlartListFileManager.
	/// </summary>
	public class AlartListFileManager
	{
		public AlartListFileManager()
		{
			
		}
		public void save(MainForm form)
		{
			var sw = new StreamWriter("favoritecom.ini");
			sw.WriteLine("130");
			foreach (AlartInfo ai in form.alartListDataSource) {
				for (var i = 0; i < 36; i++) { //namaroku 29 save 32
					if (i == 1) sw.WriteLine(ai.communityId);
					else if (i == 2) sw.WriteLine(ai.hostId);
					else if (i == 4) sw.WriteLine(ai.communityName);
					else if (i == 5) sw.WriteLine(ai.hostName);
					else if (i == 7) sw.WriteLine(ai.lastHostDate);
					else if (i == 15) sw.WriteLine(ai.addDate);
					else if (i == 17) sw.WriteLine(ai.popup.ToString().ToLower());
					else if (i == 18) sw.WriteLine(ai.baloon.ToString().ToLower());
					else if (i == 19) sw.WriteLine(ai.browser.ToString().ToLower());
					else if (i == 20) sw.WriteLine(ai.mail.ToString().ToLower());
					else if (i == 21) sw.WriteLine(ai.sound.ToString().ToLower());
					else if (i == 24) sw.WriteLine(ai.appliA.ToString().ToLower());
					else if (i == 25) sw.WriteLine(ai.appliB.ToString().ToLower());
					else if (i == 26) sw.WriteLine(ai.appliC.ToString().ToLower());
					else if (i == 27) sw.WriteLine(ai.appliD.ToString().ToLower());
					else if (i == 28) sw.WriteLine(ai.appliE.ToString().ToLower());
					else if (i == 29) sw.WriteLine(ai.appliF.ToString().ToLower());
					else if (i == 30) sw.WriteLine(ai.appliG.ToString().ToLower());
					else if (i == 31) sw.WriteLine(ai.appliH.ToString().ToLower());
					else if (i == 32) sw.WriteLine(ai.appliI.ToString().ToLower());
					else if (i == 33) sw.WriteLine(ai.appliJ.ToString().ToLower());
					else if (i == 34) sw.WriteLine(ai.memo);
                    else if (i == 8) sw.WriteLine(ai.communityFollow);
                    else if (i == 9) sw.WriteLine(ai.hostFollow);
                    else if (i == 13) sw.WriteLine(ai.lastLvid);
                    else if (i == 3) sw.WriteLine(ai.keyword);
					else sw.WriteLine("");
				}
			}
			sw.WriteLine("EndLine");
			sw.Close();
		}
		public void load(MainForm form)
		{
			ReadNamarokuList(form, form.alartListDataSource, "favoritecom.ini", false, false);
			
		}
		public void ReadNamarokuList(MainForm form, SortableBindingList<AlartInfo> alartListDataSource, string fileName, bool isUpdateComHost, bool isDuplicateCheck)
		{
			string[] lines;
			
			try {
				var sr = new StreamReader(fileName);
				lines = sr.ReadToEnd().Replace("\r", "").Split('\n');
				sr.Close();
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
				return;
			}
			var itemLineNum = (lines[0] == "120") ? 29 : 36;
			if ((lines.Length - 3) % itemLineNum != 0) {
				form.showMessageBox("読み込めませんでした", "");
				return;
			}
			
			var readAiList = new List<AlartInfo>();
			
			for (var i = 1; i < lines.Length - 2; i += itemLineNum) {
				
				var isFollow = false;
				var comFollow = "";
				var userFollow = "";
				if (isUpdateComHost) {
					/*
					if (lines[i + 1] != null && lines[i + 1] != "") {
						var comName = util.getCommunityName(lines[i + 1], out isFollow, form.check.container);
						if (comName != null)
							comFollow = (isFollow) ? "フォロー解除する" : "フォローする";
						else lines[i + 1] = "";
					} else lines[i + 4] = "";
					*/
					/*
					if (lines[i + 2] != null && lines[i + 2] != "") {
						var userName = util.getUserName(lines[i + 2], out isFollow, form.check.container);
						if (userName != null)
							userFollow = (isFollow) ? "フォロー解除する" : "フォローする";
						else lines[i + 2] = "";
					} else lines[i + 5] = "";
					*/
				}
				//if (lines[i + 1] == "" && lines[i + 2] == "" && lines[i + 3] == "") continue;
				AlartInfo ai;
				if (itemLineNum == 29) {
		            ai = new AlartInfo(lines[i + 1], 
							lines[i + 2], lines[i + 4], lines[i + 5], 
							lines[i + 7], lines[i + 15], 
							lines[i + 17] == "true",
							lines[i + 18] == "true",
							lines[i + 19] == "true", 
							lines[i + 20] == "true",
							lines[i + 21] == "true",
							lines[i + 24] == "true", 
							lines[i + 25] == "true", 
							lines[i + 26] == "true",
							false, 
							false, 
							false,
							false,
							false,
							false,
							false,
							lines[i + 28],
							comFollow, userFollow, lines[i + 13], 
							lines[i + 3]);
				} else {
					ai = new AlartInfo(lines[i + 1], 
							lines[i + 2], lines[i + 4], lines[i + 5], 
							lines[i + 7], lines[i + 15], 
							lines[i + 17] == "true",
							lines[i + 18] == "true",
							lines[i + 19] == "true", 
							lines[i + 20] == "true",
							lines[i + 21] == "true",
							lines[i + 24] == "true", 
							lines[i + 25] == "true", 
							lines[i + 26] == "true",
							lines[i + 27] == "true", 
							lines[i + 28] == "true", 
							lines[i + 29] == "true",
							lines[i + 30] == "true",
							lines[i + 31] == "true",
							lines[i + 32] == "true",
							lines[i + 33] == "true",
							lines[i + 34],
							comFollow, userFollow, lines[i + 13], 
							lines[i + 3]);
				}
				//if ((ai.communityId == null || ai.communityId == "") &&
				//    (ai.hostId == null || ai.hostId == "")) continue;
				readAiList.Add(ai);
			}
			for (var j = 0; j < 100; j++) {
	       		if (!form.IsDisposed && form.IsHandleCreated) break;
	       		Thread.Sleep(1000);
			}
			var dupliNumList = (isDuplicateCheck) ? getDuplicateNum(readAiList, form) : null;
			if (isDuplicateCheck && dupliNumList.Count > 0) {
				form.showMessageBox(dupliNumList.Count.ToString() + "件の重複が見つかりました");
				
				
			}
			
			var addList = new List<AlartInfo>();
			var isContinueCancel = 0;
			for (var i = 0; i < readAiList.Count; i++) {
				var ai = readAiList[i];
				if (isDuplicateCheck) {
					if (!isDuplicateOk(ai, dupliNumList, i, form)) {
						isContinueCancel++;
						if (isContinueCancel % 5 == 0) {
							var res = form.showMessageBox("全てキャンセルしますか？", "", MessageBoxButtons.YesNo);
							if (res == DialogResult.Yes) break;
						}
						continue;//break;
					} else isContinueCancel = 0;
				}
				addList.Add(ai);
				//form.alartListAdd(ai);
			}
			foreach (var ai in addList)
				form.alartListAdd(ai);
			
			if (form.check.container != null) 
				new FollowChecker(form, form.check).check();
			
			form.recentLiveCheck();
		}
		private List<int> getDuplicateNum(List<AlartInfo> readAiList, MainForm form) {
			var ret = new List<int>();
			var c = form.getAlartListCount();
			for (var i = 0; i < readAiList.Count; i++) {
				var ai = readAiList[i];
				for (var j = 0; j < c; j++) {
					if ((ai.communityId != null && ai.communityId != "" && form.alartListDataSource[j].communityId == 
					    	ai.communityId) ||
					    (ai.hostId != null && ai.hostId != "" && form.alartListDataSource[j].hostId ==
					    	 ai.hostId)) {
						ret.Add(i);
						break;
					}
				}
			}
			return ret;
		}
		private bool isDuplicateOk(AlartInfo ai, List<int> dupList, int _i, MainForm form) {
			if (dupList.IndexOf(_i) == -1) return true;
			
			
			
			try {
				var count = form.getAlartListCount();
				for (var i = 0; i < count; i++) {
					if (ai.communityId != null && ai.communityId != "" && form.alartListDataSource[i].communityId == 
					    	ai.communityId) {
						var m = (ai.communityId.StartsWith("co")) ? "コミュニティ" : "チャンネル";
						
						form.setAlartListScrollIndex(i);
					    //var res = MessageBox.Show(m + "ID" + ai.communityId + "は既に登録されています。削除しますか？(はい＝削除　いいえ＝削除　キャンセル＝フォームに戻る)", "確認", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
					    var res = form.showMessageBox(m + "ID:" + ai.communityId + "は既に登録されています。\n(" + form.alartListDataSource[i].toString() + ")\n\n既に存在している行を削除しますか？\nはい=削除して登録　いいえ=既に存在する行を削除せず登録　キャンセル=登録しない(" + _i.ToString() + "/" + dupList.Count + ")", "確認", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
					    
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
						form.setAlartListScrollIndex(i);
						var m = "ユーザー";
					    //var res = MessageBox.Show(m + "ID" + ai.communityId + "は既に登録されています。削除しますか？(はい＝削除して登録　いいえ＝登録　キャンセル＝フォームに戻る)", "確認", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
					    var res = form.showMessageBox(m + "ID:" + ai.hostId + "は既に登録されています。\n(" + form.alartListDataSource[i].toString() + ")\n\n既に存在している行を削除しますか？\nはい=削除して登録　いいえ=既に存在する行を削除せず登録　キャンセル=登録しない(" + _i.ToString() + "/" + dupList.Count + ")", "確認", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
					    
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
	}
}
