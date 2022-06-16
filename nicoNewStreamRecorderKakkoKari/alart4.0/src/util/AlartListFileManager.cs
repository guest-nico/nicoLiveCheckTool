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
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;
using System.Drawing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using namaichi.info;
using namaichi.alart;

namespace namaichi.utility
{
	/// <summary>
	/// Description of AlartListFileManager.
	/// </summary>
	public class AlartListFileManager
	{
		private bool isUserMode = false;
		private SortableBindingList<AlartInfo> dataSource = null;
		private DataGridView list = null;
		private MainForm form = null;
		public AlartListFileManager(bool isUserMode, MainForm form)
		{
			this.isUserMode = isUserMode;
			dataSource = isUserMode ? form.userAlartListDataSource : form.alartListDataSource;
			list = isUserMode ? form.userAlartList : form.alartList;
			this.form = form;
		}
		public void save()
		{
			var path = util.getJarPath()[0] + "\\";
			var mode = isUserMode ? "user" : "com";
			
			
			var f = path + "favorite" + mode + ".ini_";
			try {
				using (var sw = new StreamWriter(f, false, Encoding.UTF8)) {
					sw.WriteLine("130");
					foreach (AlartInfo ai in dataSource) {
						for (var i = 0; i < 36; i++) { //namaroku 29 save 32
							if (i == 1) sw.WriteLine(ai.communityId);
							else if (i == 2) sw.WriteLine(ai.hostId);
							else if (i == 4) sw.WriteLine(ai.communityName);
							else if (i == 5) sw.WriteLine(ai.hostName);
							else if (i == 7) sw.WriteLine(ai.lastHostDate);
							else if (i == 10) sw.WriteLine(ColorTranslator.ToHtml(ai.textColor));
							else if (i == 11) sw.WriteLine(ColorTranslator.ToHtml(ai.backColor));
							else if (i == 12) sw.WriteLine(ai.soundType + "," + ai.isSoundId.ToString().ToLower());
							else if (i == 15) sw.WriteLine(ai.addDate);
							//else if (i == 16) sw.WriteLine(ai.isAnd.ToString().ToLower());
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
		                    else if (i == 14) sw.WriteLine(ai.isMustCom.ToString().ToLower() + "," + ai.isMustUser.ToString().ToLower() + "," + ai.isMustKeyword.ToString().ToLower());
		                    else if (i == 6) {
		                    	var ckiStr = "";
		                    	try {
		                    		if (ai.cki != null) {
		                    			ckiStr = Newtonsoft.Json.Linq.JToken.FromObject(ai.cki).ToString(Newtonsoft.Json.Formatting.None);
		                    			ckiStr = (ai.isCustomKeyword ? "1" : "0") + ckiStr;
		                    		}
		                    	} catch (Exception e) {
		                    		util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
		                    	}
		                    	sw.WriteLine(ckiStr);
		                    }
		                    else if (i == 35) sw.WriteLine(ai.memberOnlyMode.ToString());
		                    else if (i == 0) {
		                    	var etc = new AiEtcInfo();
		                    	etc.isAutoReserve = ai.isAutoReserve;
		                    	etc.recentColorMode = ai.recentColorMode;
		                    	etc.lastLvTitle = ai.lastLvTitle;
		                    	etc.alartHistory = ai.alartHistory;
		                    	var json = JToken.FromObject(etc).ToString(Formatting.None);
		                    	sw.WriteLine(json);
		                    }
							else sw.WriteLine("");
						}
					}
					sw.WriteLine("EndLine");
					//sw.Close();
				}
				File.Copy(f, f.Substring(0, f.Length - 1), true);
				File.Delete(f);
				
				util.saveBackupList(path, "favorite" + mode);
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
			
		}
		public void load()
		{
			try {
				var mode = isUserMode ? "user" : "com";
				//var dataSource = isUserMode ? form.userAlartListDataSource : form.alartListDataSource;
				ReadNamarokuList(form, dataSource, util.getJarPath()[0] + "\\favorite" + mode + ".ini", false, false);
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
		}
		public void ReadNamarokuList(MainForm form, SortableBindingList<AlartInfo> dataSource, string fileName, bool isUpdateComHost, bool isDuplicateCheck)
		{
			string[] lines;
			
			try {
				if (!File.Exists(fileName)) return;
				using (var sr = new StreamReader(fileName, Encoding.UTF8)) {
					lines = sr.ReadToEnd().Replace("\r", "").Split('\n');
				}
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
				try {
					using (var sr = new StreamReader(fileName)) {
						lines = sr.ReadToEnd().Replace("\r", "").Split('\n');
					}
				} catch (Exception e) {
					form.addLogText("お気に入りリストが正常に読み込めませんでした");
					util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
					return;
				}
			}
			var itemLineNum = (lines[0] == "120") ? 29 : 36;
			if ((lines.Length - 3) % itemLineNum != 0) {
				form.showMessageBox("読み込めませんでした", "");
				return;
			}
			
			string namarokuRecRead = null;
			if (itemLineNum == 29) {
				var f = new NamarokuRecordCheckSettingForm(int.Parse(form.config.get("fontSize")));
				form.formAction(() => f.ShowDialog());
				if (f.recordCheck == null) return;
				else namarokuRecRead = f.recordCheck;
			}
			
			var readAiList = new List<AlartInfo>();
			
			for (var i = 1; i < lines.Length - 2; i += itemLineNum) {
				try {
					var comFollow = "";
					var userFollow = "";
					Color textColor = Color.Black, backColor = Color.FromArgb(255,224,255);
					if (lines[i + 10] != "" && lines[i + 11] != "") {
						try {
							textColor = ColorTranslator.FromHtml(lines[i + 10]);
							backColor = ColorTranslator.FromHtml(lines[i + 11]);
						} catch (Exception e) {
							util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
						}
					}
					int defaultSound = 0;
					bool isDefaultSoundId = true;
					if (itemLineNum != 29 && lines[i + 12] != "") {
						try {
							var b = lines[i + 12].Split(',');
							defaultSound = int.Parse(b[0]);
							isDefaultSoundId = bool.Parse(b[1]);
						} catch (Exception e) {
							util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
						}
					}
					/*
					bool isAnd = true;
					if (lines[i + 16] != "") {
						try {
							isAnd = lines[i + 16] == "true";
						} catch (Exception e) {
							util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
						}
					}
					*/
					bool isMustCom = true, isMustUser = true, isMustKeyword = true;
					if (itemLineNum != 29 && lines[i + 14] != "") {
						try {
							var isMustArr = lines[i + 14].Split(',');
							isMustCom = bool.Parse(isMustArr[0]);
							isMustUser = bool.Parse(isMustArr[1]);
							isMustKeyword = bool.Parse(isMustArr[2]);
						} catch (Exception e) {
							util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
						}
					}
					List<CustomKeywordInfo> cki = null;
					bool isCustomKeyword = false;
					if (itemLineNum != 29 && lines[i + 6] != "") {
						try {
							cki = Newtonsoft.Json.JsonConvert.DeserializeObject< List<CustomKeywordInfo>>(lines[i + 6].Substring(1));
							isCustomKeyword = lines[i + 6][0] == '1';
						} catch (Exception e) {
							util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
							cki = null;
							isCustomKeyword = false;
						}
					}
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
					var isAutoReserve = false;
					var recentColorMode = 0;
					var lastLvTitle = "";
					var alartHistory = new List<KeyValuePair<DateTime, string>>();
					if (itemLineNum != 29 && !string.IsNullOrEmpty(lines[i + 0])) {
						var etc = JsonConvert.DeserializeObject<AiEtcInfo>(lines[i + 0]);
						isAutoReserve = etc.isAutoReserve;
						recentColorMode = etc.recentColorMode;
						lastLvTitle = etc.lastLvTitle;
						alartHistory = etc.alartHistory;
					}
					//if (lines[i + 1] == "" && lines[i + 2] == "" && lines[i + 3] == "") continue;
					AlartInfo ai;
					var comName = string.IsNullOrEmpty(lines[i + 4]) ? lines[i + 4] : WebUtility.HtmlDecode(lines[i + 4]);
					var memberOnlyMode = getMemberOnly(lines[i + 35]);
					
					if (itemLineNum == 29) {
			            ai = new AlartInfo(lines[i + 1], 
								lines[i + 2], comName, lines[i + 5], 
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
								lines[i + 3], textColor, 
								backColor, defaultSound, isDefaultSoundId, 
								false, false, false, null, false, "True,True,True",
								false, 0, "");
						if (lines[i + 23] == "true") setNamarokuRead(ai, namarokuRecRead);
					} else {
						ai = new AlartInfo(lines[i + 1], 
								lines[i + 2], comName, lines[i + 5], 
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
								lines[i + 3], textColor,
								backColor, defaultSound, isDefaultSoundId,
								isMustCom, isMustUser, isMustKeyword, cki,
								isCustomKeyword, memberOnlyMode, isAutoReserve, 
								recentColorMode, lastLvTitle);
						ai.alartHistory = alartHistory;
					}
					//if ((ai.communityId == null || ai.communityId == "") &&
					//    (ai.hostId == null || ai.hostId == "")) continue;
					if (isUserMode && string.IsNullOrEmpty(ai.hostId)) continue;
					if (isUserMode) ai.communityId = ai.communityName = "";
					readAiList.Add(ai);
				} catch (Exception) {
					form.addLogText(i + "行目から始まるお気に入りが読み込めませんでした");
				}
			}
			
			try {
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
				var isContinueYes = 0;
				var isContinueNo = 0;
				var allMode = -1;//-1-none 0-cancel 1-yes 2-no
				for (var i = 0; i < readAiList.Count; i++) {
					var ai = readAiList[i];
					if (isDuplicateCheck) {
						var dialogRet = isDuplicateOk(ai, dupliNumList, i, form, allMode); 
						if (dialogRet == DialogResult.Cancel) {
							isContinueCancel++;
							isContinueYes = isContinueNo = 0;
							if (isContinueCancel % 5 == 0 && allMode < 1) {
								var res = form.showMessageBox("全てキャンセルしますか？", "", MessageBoxButtons.YesNo);
								if (res == DialogResult.Yes) break;
							}
							continue;//break;
						} else if (dialogRet == DialogResult.Yes) {
							isContinueYes++;
							isContinueCancel = isContinueNo = 0;
							if (isContinueYes % 5 == 0 && allMode < 1) {
								var res = form.showMessageBox("全て「はい」を選択しますか？", "", MessageBoxButtons.YesNo);
								if (res == DialogResult.Yes) allMode = 1;
							}
						} else if (dialogRet == DialogResult.No) {
							isContinueNo++;
							isContinueCancel = isContinueYes = 0;
							if (isContinueNo % 5 == 0 && allMode < 1) {
								var res = form.showMessageBox("全て「いいえ」を選択しますか？", "", MessageBoxButtons.YesNo);
								if (res == DialogResult.Yes) allMode = 2;
							}
						} 
					}
					addList.Add(ai);
					//form.alartListAdd(ai);
				}
				foreach (var ai in addList)
					form.alartListAdd(ai, isUserMode);
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				form.addLogText("読み込んだリストを追加中にエラーが発生しました" + e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
			if (form.check.container != null) 
				new FollowChecker(form, form.check.container).check();
				
			form.recentLiveCheck();
		}
		private List<int> getDuplicateNum(List<AlartInfo> readAiList, MainForm form) {
			
			var ret = new List<int>();
			var c = form.getAlartListCount(isUserMode);
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
		private DialogResult isDuplicateOk(AlartInfo ai, List<int> dupList, int _i, MainForm form, int allMode) {
			var ret = DialogResult.None;
			if (dupList.IndexOf(_i) == -1) return ret;
			
			try {
				var count = form.getAlartListCount(isUserMode);
				for (var i = 0; i < count; i++) {
					if (ai.communityId != null && ai.communityId != "" && ai.communityId != "official" && form.alartListDataSource[i].communityId == 
					    	ai.communityId) {
						var m = (ai.communityId.StartsWith("co")) ? "コミュニティ" : "チャンネル";
						
						
						form.setAlartListScrollIndex(i, isUserMode);
					    //var res = MessageBox.Show(m + "ID" + ai.communityId + "は既に登録されています。削除しますか？(はい＝削除　いいえ＝削除　キャンセル＝フォームに戻る)", "確認", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
					    DialogResult res;
					    if (allMode == 1) res = DialogResult.Yes;
					    else if (allMode == 2) res = DialogResult.No;
					    else {
							res = form.showMessageBox(m + "ID:" + ai.communityId + "は既に登録されています。\n(" + form.alartListDataSource[i].toString() + ")\n\n既に存在している行を削除しますか？\nはい=削除して登録　いいえ=既に存在する行を削除せず登録　キャンセル=登録しない(" + _i.ToString() + "/" + dupList.Count + ")", "確認", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
					    }
					    ret = res;
					    
					    if (res == DialogResult.Yes) {
					    	form.alartListRemove(form.alartListDataSource[i], isUserMode);
					    	i--;
					    	count--;
	//				    	form.alartListAdd(ai);
					    }
					    else if (res == DialogResult.No) {
	//				    	form.alartListAdd(ai);
					    }
					    else if (res == DialogResult.Cancel) return ret;
					}
				}
				count = form.getAlartListCount(isUserMode);
				for (var i = 0; i < count; i++) {
					if (ai.hostId != null && ai.hostId != "" && form.alartListDataSource[i].hostId ==
						   ai.hostId) {
						form.setAlartListScrollIndex(i, isUserMode);
						var m = "ユーザー";
						
						//var res = MessageBox.Show(m + "ID" + ai.communityId + "は既に登録されています。削除しますか？(はい＝削除　いいえ＝削除　キャンセル＝フォームに戻る)", "確認", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
					    DialogResult res;
					    if (allMode == 1) res = DialogResult.Yes;
					    else if (allMode == 2) res = DialogResult.No;
					    else {
					    	//var res = MessageBox.Show(m + "ID" + ai.communityId + "は既に登録されています。削除しますか？(はい＝削除して登録　いいえ＝登録　キャンセル＝フォームに戻る)", "確認", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
							res = form.showMessageBox(m + "ID:" + ai.hostId + "は既に登録されています。\n(" + form.alartListDataSource[i].toString() + ")\n\n既に存在している行を削除しますか？\nはい=削除して登録　いいえ=既に存在する行を削除せず登録　キャンセル=登録しない(" + _i.ToString() + "/" + dupList.Count + ")", "確認", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
					    }
					    ret = res;
					    
					    if (res == DialogResult.Yes) {
					    	form.alartListRemove(form.alartListDataSource[i], isUserMode);
					    	i--;
					    	count--;
	//				    	form.alartListAdd(ai);
					    }
					    else if (res == DialogResult.No) {
	//				    	form.alartListAdd(ai);
					    }
					    else if (res == DialogResult.Cancel) return ret;
						
					}
				}
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				return ret;
			}
			return ret;
		}
		private void setNamarokuRead(AlartInfo ai, string namarokuRecRead) {
			if (namarokuRecRead == "appliA") ai.appliA = true;
			else if (namarokuRecRead == "appliB") ai.appliB = true;
			else if (namarokuRecRead == "appliC") ai.appliC = true;
			else if (namarokuRecRead == "appliD") ai.appliD = true;
			else if (namarokuRecRead == "appliE") ai.appliE = true;
			else if (namarokuRecRead == "appliF") ai.appliF = true;
			else if (namarokuRecRead == "appliG") ai.appliG = true;
			else if (namarokuRecRead == "appliH") ai.appliH = true;
			else if (namarokuRecRead == "appliI") ai.appliI = true;
			else if (namarokuRecRead == "appliJ") ai.appliJ = true;
		}
		private string getMemberOnly(string c) {
			if (string.IsNullOrEmpty(c)) return "True,True,True";
			
			if (c.IndexOf(",") == -1) {
				if (c == "0") return "True,True,True";
				else if (c == "1") return "True,False,False";
				else if (c == "2") return "False,True,True";
				else return "True,True,True";
			}
			return c;
		}
		private class AiEtcInfo {
			public bool isAutoReserve = false;
			public int recentColorMode = 0;
			public string lastLvTitle = "";
			public List<KeyValuePair<DateTime, string>> alartHistory = new List<KeyValuePair<DateTime, string>>();
		}
	}
}
