/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2019/01/06
 * Time: 3:09
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net;
using System.IO;
using System.Media;
using namaichi.info;
using namaichi.rec;

namespace namaichi.alart
{
	/// <summary>
	/// Description of Check.
	/// </summary>
	public class Check
	{
		//private config.config config;
		public MainForm form;
		private string lastLv = null;
		
		private SortableBindingList<AlartInfo> alartListDataSource;
		private DateTime lastUserNameCheckTime = DateTime.Now;
		public CookieContainer container;
		public List<string> checkedLvIdList = new List<string>();
		public List<string> processedLvidList = new List<string>();
		
		private Regex categoryReg = new Regex("<category>(.*?)</category>");
		private int userNameUpdateInterval;
		//public SoundPlayer soundPlayer = null;
		
		public PopupDisplay popup = null;
		private object foundLiveLock = new object();
		private RssCheck rc = null;
		private PushReceiver pr = null;
		private AppPushReceiver apr = null;
		
		public Check(SortableBindingList<AlartInfo> alartListDataSource, MainForm form)
		{
			//this.config = config;
			this.alartListDataSource = alartListDataSource;
			this.form = form;
			/*
			try {
				soundPlayer = new SoundPlayer("Sound/se_soc01.wav");
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
			*/
			popup = new PopupDisplay(form);
			userNameUpdateInterval = int.Parse(form.config.get("userNameUpdateInterval"));
		}
		public void start() {
			setCookie();
			
			//Task.Run(() => reserveStreamCheck());
			if (bool.Parse(form.config.get("IsRss"))) {
				Task.Run(() => {
				    rc = new RssCheck(this, form.config);
				    rc.start();
		         	
		        });
			}
			if (bool.Parse(form.config.get("IsPush"))) {
				Task.Run(() => {
					pr = new PushReceiver(this, form.config);
					pr.start();
				});
			}
			if (bool.Parse(form.config.get("IsAppPush"))) {
				Task.Run(() => {
					apr = new AppPushReceiver(this, form.config);
					apr.start();
				});
			}
			Task.Run(() => regularlyProcess());
		}
		
		private bool gotStreamProcess(List<RssItem> items) {
			
			var isChanged = false;
			items.Reverse();
			foreach (var item in items) {
				bool isAppliA, isAppliB, isAppliC, isAppliD, isAppliE, isAppliF, isAppliG, isAppliH, isAppliI, isAppliJ, isPopup, isBaloon, isBrowser, isMail, isSound, isLog;
				isAppliA = isAppliB = isAppliC = isAppliD = isAppliE = isAppliF = isAppliG = isAppliH = isAppliI = isAppliJ = isPopup = isBaloon = isBrowser = isMail = isSound = isLog = false;
				var alartListCount = form.getAlartListCount();
				
				AlartInfo targetAi = null;
				for (var i = 0; i < alartListDataSource.Count; i++) {
					var alartItem = (info.AlartInfo)alartListDataSource[i];

					var isNosetComId = alartItem.communityId == "" ||
							alartItem.communityId == null;
					var isNosetHostName = alartItem.hostName == "" ||
							alartItem.hostName == null;
					var isNosetKeyword = alartItem.keyword == "" ||
							alartItem.keyword == null;
					if (!(isNosetComId ||      
					     	alartItem.communityId == item.comId) ||
					     	!(isNosetHostName ||
					     	 alartItem.hostName == item.hostName) ||
					        !(isNosetKeyword ||
					     	 item.isContainKeyword(alartItem.keyword)))
							continue;
					if (isNosetComId && isNosetHostName && isNosetKeyword) continue;
					if (!isUserIdFromLvidOk(item, alartItem.hostId)) continue;
					
					
					
					if (alartItem.AppliA) isAppliA = true;
					if (alartItem.AppliB) isAppliB = true;
					if (alartItem.AppliC) isAppliC = true;
					if (alartItem.AppliD) isAppliD = true;
					if (alartItem.AppliE) isAppliE = true;
					if (alartItem.AppliF) isAppliF = true;
					if (alartItem.AppliG) isAppliG = true;
					if (alartItem.AppliH) isAppliH = true;
					if (alartItem.AppliI) isAppliI = true;
					if (alartItem.AppliJ) isAppliJ = true;
					if (alartItem.Popup) isPopup = true;
					if (alartItem.Baloon) isBaloon = true;
					if (alartItem.Browser) isBrowser = true;
					if (alartItem.mail) isMail = true;
					if (alartItem.sound) isSound = true;
					isLog = true;
					
					form.updateLastHosoDate(i, DateTime.Parse(item.pubDate).ToString("yyyy/MM/dd HH:mm:ss"), item.lvId);
					isChanged = true;
					targetAi = alartItem;
					
				}
				
				if (processedLvidList.IndexOf(item.lvId) > -1) {
					util.debugWriteLine("process lvid found " + item.lvId + " " + item.comName + " " + item.hostName);
					continue;
				}
				processedLvidList.Add(item.lvId);
					
				util.debugWriteLine(item.lvId + " A " + isAppliA + " B " + isAppliB + " C " + isAppliC + " D " + isAppliD + " E " + isAppliE + " F " + isAppliF + " G " + isAppliG + " H " + isAppliH + " I " + isAppliI + " J " + isAppliJ + " pop " + isPopup + " balloon " + isBaloon);
				
				if (isAppliA && !form.notifyOffList[7]) {
					var appliAPath = form.config.get("appliAPath");
					var args = form.config.get("appliAArgs");
					appliProcess(appliAPath, item.lvId, args);
				}
				if (isAppliB && !form.notifyOffList[8]) {
					var appliBPath = form.config.get("appliBPath");
					var args = form.config.get("appliBArgs");
					appliProcess(appliBPath, item.lvId, args);
				}
				if (isAppliC && !form.notifyOffList[9]) {
					var appliCPath = form.config.get("appliCPath");
					var args = form.config.get("appliCArgs");
					appliProcess(appliCPath, item.lvId, args);
				}
				if (isAppliD && !form.notifyOffList[10]) {
					var appliDPath = form.config.get("appliDPath");
					var args = form.config.get("appliDArgs");
					appliProcess(appliDPath, item.lvId, args);
				}
				if (isAppliE && !form.notifyOffList[11]) {
					var appliEPath = form.config.get("appliEPath");
					var args = form.config.get("appliEArgs");
					appliProcess(appliEPath, item.lvId, args);
				}
				if (isAppliF && !form.notifyOffList[12]) {
					var appliFPath = form.config.get("appliFPath");
					var args = form.config.get("appliFArgs");
					appliProcess(appliFPath, item.lvId, args);
				}
				if (isAppliG && !form.notifyOffList[13]) {
					var appliGPath = form.config.get("appliGPath");
					var args = form.config.get("appliGArgs");
					appliProcess(appliGPath, item.lvId, args);
				}
				if (isAppliH && !form.notifyOffList[14]) {
					var appliHPath = form.config.get("appliHPath");
					var args = form.config.get("appliHArgs");
					appliProcess(appliHPath, item.lvId, args);
				}
				if (isAppliI && !form.notifyOffList[15]) {
					var appliIPath = form.config.get("appliIPath");
					var args = form.config.get("appliIArgs");
					appliProcess(appliIPath, item.lvId, args);
				}
				if (isAppliJ && !form.notifyOffList[16]) {
					var appliJPath = form.config.get("appliJPath");
					var args = form.config.get("appliJArgs");
					appliProcess(appliJPath, item.lvId, args);
				}
				if (isPopup && !form.notifyOffList[2]) {
					displayPopup(item, targetAi);
				}
				if (isBaloon && !form.notifyOffList[3]) {
					displayBaloon(item, targetAi);
				}
				if (isBrowser && !form.notifyOffList[4]) {
					openBrowser(item);
				}
				if (isMail && !form.notifyOffList[5]) {
					mail(item);
				}
				if (isSound && !form.notifyOffList[6]) {
					sound(item);
				}
				if (isLog && form.config.get("log") == "true")
					writFavoriteLog(item);
				
				
			}
			return isChanged;
		}
		private void appliProcess(string appliPath, string lvid, string args) {
			if (appliPath == null || appliPath == "") return;
			var url = "http://live2.nicovideo.jp/watch/lv" + util.getRegGroup(lvid, "(\\d+)");

			try {
				appliPath = appliPath.Trim();
				/*
				string f, arg;
				if (appliPath.StartsWith("\"")) {
					f = util.getRegGroup(appliPath, "\"(.+?)\"");
					arg = util.getRegGroup(appliPath, "\".+?\"(.*)");
				} else {
					f = util.getRegGroup(appliPath, "(.+?) ");
					arg = util.getRegGroup(appliPath, ".+? (.*)");
					if (f == null) {
						f = appliPath;
						arg = "";
					}
				}
				if (arg == null) arg = "";
				arg += " " + url;
				*/
				Process.Start(appliPath, url + " " + args);
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
		}
		private void checkUserNameAndFollow() {
			try {
				var count = form.getAlartListCount();
				var idList = new List<string>();
				for (var i = 0; i < count; i++) {
					var ai = (AlartInfo)alartListDataSource[i];
					if (ai.hostId != null && ai.hostId != "") idList.Add(ai.hostId);
					if (ai.communityId != null && ai.communityId != "") idList.Add(ai.communityId);
				}
				
				foreach (var id in idList) {
					//while (true) {
						var isFollow = false;
						if (id.StartsWith("c")) {
							/*
							var comName = util.getCommunityName(id, out isFollow, form.check.container);
							if (comName == null) {
								//Thread.Sleep(3000);
								continue;
							}
							
							for (var i = 0; i < count; i++) {
								var ai = (AlartInfo)alartListDataSource[i];
								if (ai.communityId == id) {
									form.updateCommunityName(i, comName, isFollow);
								}
							}
							*/
						} else {
							var userName = util.getUserName(id, out isFollow, form.check.container);
							if (userName == null) {
								//Thread.Sleep(3000);
								continue;
							}
							
							for (var i = 0; i < count; i++) {
								var ai = (AlartInfo)alartListDataSource[i];
								if (ai.hostId == id) {
									form.updateUserName(i, userName, isFollow);
								}
							}
						}
						//break;
					//}
				}
				
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
		}
		public void setCookie() {
			var url = "https://www.nicovideo.jp/my";
			var cg = new CookieGetter(form.config);
			var res = cg.getHtml5RecordCookie(url, false).Result;
			if (res == null || res[0] == null) {
				form.addLogText("Cookieの取得を確認できませんでした", true);
			} else {
				form.addLogText("Cookieの取得に成功しました", true);
				container = res[0];
				
			}
			
			if (alartListDataSource.Count > 0) {
				Task.Run(() => new FollowChecker(form, this).check());
			}
		}
		public bool isUserIdFromLvidOk(RssItem rssItem, string alartUserId) {
			if (alartUserId == null || alartUserId == "") return true;
			
			var uid = (rssItem.userId != null) ? rssItem.userId : getUserIdFromLvid(rssItem.lvId);
			if (rssItem.userId == null && uid != null) rssItem.userId = uid;
			/*
			var res = util.getPageSource("http://live.nicovideo.jp/api/getplayerstatus/" + lvid, container);
			if (res == null) return false;
			var uid = util.getRegGroup(res, "<owner_id>(.+?)</owner_id>");
			*/
			if (uid == null) return false;
			return uid == alartUserId;
		}
		public string getUserIdFromLvid(string lvid) {
			var url = "http://live2.nicovideo.jp/watch/" + lvid;
			var res = util.getPageSource(url, container);
			if (res == null) return null;
			var uid = util.getRegGroup(res, "user/(\\d+)");
			return uid;
		}
		private void reserveStreamCheck() {
			while (true) {
				var nextStream = getNextStream();
				Thread.Sleep(30000);
			}
		}
		private List<RssItem> getNextStream(){
			var url = "http reserve";
			var res = util.getPageSource(url, null);
			if (res == null) return null;
			return null;
		}
		private void displayPopup(RssItem item, AlartInfo ai) {
			popup.show(item, ai);
		}
		private void displayBaloon(RssItem item, AlartInfo ai) {
			form.DisplayBalloon(item, ai);
		}
		private void writeBroadLog(List<RssItem> items) {
			//namaroku
			//2019/01/16 19:03:17 lv317993742,co2209093,21119
			//2019/01/16 19:03:19 lv317993753,co1640324,21001654
			try {
				if (!Directory.Exists("Log")) Directory.CreateDirectory("Log");
				foreach(var ri in items) {
					var dt = DateTime.Parse(ri.pubDate);
					var p = "Log/broadLog-" + dt.ToString("yyyy-MM-dd") + ".txt";
					var sw = new StreamWriter(p, true);
					writeLog(sw, ri);
					sw.Close();
				}
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
		}
		private void writFavoriteLog(RssItem ri) {
			//namaroku
			//2019/01/16 19:26:35 さくらこ♡－ lv317994167 co3871789
			//実験放送？　2019/01/16 19:30:00 － lv317992746 co2652877
			//2019/01/16 19:49:57 公式生放送－ lv317613421 official
	
			if (!Directory.Exists("Log")) Directory.CreateDirectory("Log");
			var sw = new StreamWriter("Log/favoriteLog.txt", true);
			writeLog(sw, ri);
			sw.Close();
		}
		private void writeLog(StreamWriter sw, RssItem ri) {
			var tags = ri.getTag(categoryReg);
			var isJikken = Array.IndexOf(tags, "実験放送") > -1;
			var br = "";
			sw.WriteLine("[放送開始時間] " + DateTime.Parse(ri.pubDate).ToString("yyyy/MM/dd HH:mm:ss") + br);
			sw.WriteLine("[タイトル] " + ri.title + br);
			sw.WriteLine("[限定] " + ((ri.isMenberOnly) ? "限定" : "オープン") + br);
			sw.WriteLine("[放送タイプ] " + ((isJikken) ? "nicocas" : "nicolive"));
			sw.WriteLine("[放送者] " + ri.hostName + br);
			sw.WriteLine("[コミュニティ名] " + ri.comName + br);
			sw.WriteLine("[コミュニティID] " + ri.comId + br);
			sw.WriteLine("[説明] " + ri.description + br);
			sw.WriteLine("[放送ID] " + ri.lvId + br);
//			if (groupUrl != null)
//				sw.WriteLine("[コミュニティURL] " + groupUrl + br);
//			if (hostUrl != null)
//				sw.WriteLine("[放送者URL] " + hostUrl + br);
			sw.WriteLine("[タグ] " + string.Join(",", tags) + br);
			sw.WriteLine("");
		}
		public void deleteOldCheckedLvIdList() {
			try {
				checkedLvIdList.Sort();
				checkedLvIdList.RemoveRange(0, checkedLvIdList.Count - 800);
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
		}
		private void sound(RssItem ri) {
			util.playSound(form.config);
			/*
			try {
				if (soundPlayer == null) 
					soundPlayer = new SoundPlayer("Sound/se_soc01.wav");
				soundPlayer.Play();
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
			*/
		}
		private void openBrowser(RssItem item) {
			var url = "http://live2.nicovideo.jp/watch/lv" + util.getRegGroup(item.lvId, "(\\d+)");
			util.openUrlBrowser(url, form.config);
		}
		public void foundLive(List<RssItem> items) {
			if (items.Count > 0) {
				form.setHosoLogStatusBar(items[0]);
			}
			
			if (items.Count > 0 && form.config.get("IsbroadLog") == "true")
				Task.Run(() => writeBroadLog(items));
			
			lock (foundLiveLock) {
				var isChanged = gotStreamProcess(items);
				if (isChanged) form.sortAlartList();
			}
		}
		public void resetCheck() {
			if (pr != null) {
				pr.clearConfigSetting();
				pr.stop();
				pr = null;
			}
			if (apr != null) {
				apr.clearConfigSetting();
				apr.stop();
				apr = null;
			}
			if (bool.Parse(form.config.get("IsPush"))) {
				Task.Run(() => {
					pr = new PushReceiver(this, form.config);
					pr.start();
				});
			}
			if (bool.Parse(form.config.get("IsAppPush"))) {
				Task.Run(() => {
					apr = new AppPushReceiver(this, form.config);
					apr.start();
				});
			}
			
			if (bool.Parse(form.config.get("IsRss"))) {
				if (rc == null) {
					Task.Run(() => {
						rc = new RssCheck(this, form.config);
						rc.start();
					});
				}
			} else {
				if (rc != null) {
					rc.stop();
					rc = null;
				}
			}
			
			userNameUpdateInterval = int.Parse(form.config.get("userNameUpdateInterval"));
		}
		public void regularlyProcess() {
			var lastCheckLast30minLiveTime = DateTime.Now;
			while (true) {
				var ut = userNameUpdateInterval;
				if (ut < 15) ut = 15;
				if (DateTime.Now - lastUserNameCheckTime > TimeSpan.FromSeconds(ut * 60)) {
					Task.Run(() => {
					    new FollowChecker(form, this).check();
					    
					    lastUserNameCheckTime = DateTime.MaxValue;
			         	checkUserNameAndFollow();
			         	lastUserNameCheckTime = DateTime.Now;
					});
					//Task.Run(() => new FollowChecker(form, this).check());
					
				}
				
				
				if (bool.Parse(form.config.get("Ischeck30min")) && 
				 	   DateTime.Now - lastCheckLast30minLiveTime > TimeSpan.FromSeconds(60)) {
					Task.Run(() => {
					    lastCheckLast30minLiveTime = DateTime.MaxValue;
					    form.recentLiveCheck();
			         	lastCheckLast30minLiveTime = DateTime.Now;
					});
				}
				Thread.Sleep(10000);
			}
			
		}
		private void mail(RssItem item) {
			try {
				util.debugWriteLine("pubdate mail " + item.pubDate);
				string dt = DateTime.Parse(item.pubDate).ToString("yyyy/MM/dd(ddd) HH:mm");
				string title, body;
				body = DateTime.Now.ToString() + "\n";
				if (item.hostName != "" && item.hostName != null) {
					title = "[ニコ生]" + item.hostName + "の放送開始";
					body += item.hostName + " が " + item.comName + " で " + item.title + " - " + dt + "開始 を開始しました。\n";
				} else {
					title = "[ニコ生]" + item.comName + "の放送開始";
					body += item.comName + " で " + item.title + " - " + dt + "開始 を開始しました。\n";
				}
				body += "http://live.nicovideo.jp/watch/" + item.lvId + "\n";
				if (item.comId != null && item.comId.StartsWith("co"))
					body += "http://com.nicovideo.jp/community/" + item.comId;
				else if (item.comId != null && item.comId.StartsWith("ch"))
					body += "http://ch.nicovideo.jp/" + item.comId;
				
				util.debugWriteLine("title " + title);
				util.debugWriteLine("body " + body);
				string eMsg;
				util.sendMail(form.config.get("mailFrom"), 
						form.config.get("mailTo"), title, body, 
						form.config.get("mailSmtp"), form.config.get("mailPort"), 
						form.config.get("mailUser"), form.config.get("mailPass"), 
						bool.Parse(form.config.get("IsmailSsl")), out eMsg);
			} catch (Exception e) {
				util.debugWriteLine("Send mail error " + e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
			
		}
	}
}
