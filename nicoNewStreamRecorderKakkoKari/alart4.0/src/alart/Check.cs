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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using namaichi.alart;
using namaichi.info;
using namaichi.rec;
using namaichi.utility;
using namaichi;

namespace namaichi.alart
{
	/// <summary>
	/// Description of Check.
	/// </summary>
	public class Check
	{
		public MainForm form;
		
		private SortableBindingList<AlartInfo> alartListDataSource;
		
		public CookieContainer container;
		public List<RssItem> checkedLvIdList = new List<RssItem>();
		public List<string> processedLvidList = new List<string>();
		
		private Regex categoryReg = new Regex("<category>(.*?)</category>");
		private int userNameUpdateInterval;
		//public SoundPlayer soundPlayer = null;
		
		public PopupDisplay popup = null;
		private object foundLiveLock = new object();
		private CategoryCheck rc = null;
		private PushReceiver pr = null;
		private AppPushReceiver apr = null;
		private TimeTableChecker ttc = null;
		private AutoReserveChecker arc = null;
		
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
			
			//Task.Factory.StartNew(() => reserveStreamCheck());
			if (bool.Parse(form.config.get("IsRss"))) {
				Task.Factory.StartNew(() => {
				    rc = new CategoryCheck(this, form.config);
				    rc.start();
		         	
		        });
			} else if (bool.Parse(form.config.get("IsStartTimeAllCheck"))) {
				new CategoryCheck(this, form.config).start(true);
			}
			if (bool.Parse(form.config.get("IsPush"))) {
				Task.Factory.StartNew(() => {
					pr = new PushReceiver(this, form.config);
					pr.start();
				});
			}
			if (bool.Parse(form.config.get("IsAppPush"))) {
				Task.Factory.StartNew(() => {
					apr = new AppPushReceiver(this, form.config);
					apr.start();
				});
			}
			if (bool.Parse(form.config.get("IsTimeTable"))) {
				Task.Factory.StartNew(() => {
					ttc = new TimeTableChecker(this, form.config);
					ttc.start();
				});
			}
			if (bool.Parse(form.config.get("IsAutoReserve"))) {
				Task.Factory.StartNew(() => {
					arc = new AutoReserveChecker(this, form.config);
					arc.start();
				});
			}
			Task.Factory.StartNew(() => regularlyProcess());
		}
		
		private bool gotStreamProcess(List<RssItem> items) {
			util.debugWriteLine("gotStreamProcess itemCount " + (items != null ? items.Count.ToString() : "null"));
			var isChanged = false;
			items.Reverse();
			while (true) {
				try {
					foreach (var _item in items) {
						try {
							var item = _item;
							var dpi = new DoProcessInfo();
							//bool isAppliA, isAppliB, isAppliC, isAppliD, isAppliE, isAppliF, isAppliG, isAppliH, isAppliI, isAppliJ, isPopup, isBaloon, isBrowser, isMail, isSound, isLog;
							//isAppliA = isAppliB = isAppliC = isAppliD = isAppliE = isAppliF = isAppliG = isAppliH = isAppliI = isAppliJ = isPopup = isBaloon = isBrowser = isMail = isSound = isLog = false;
							//var alartListCount = form.getAlartListCount();
						
							var targetAi = new List<AlartInfo>();
							AlartInfo nearAlartAi = null;
							
							bool isSuccessAccess = getAlartProcess(dpi,
									ref isChanged, 
									ref targetAi, ref nearAlartAi, ref item,
									alartListDataSource);
							bool isSuccessAccess2 = getAlartProcess(dpi,
							        ref isChanged,
									ref targetAi, ref nearAlartAi, ref item,
									form.userAlartListDataSource);
							
							//if (items[0].comName.IndexOf("ウェザー") > -1)
							//	util.debugWriteLine("ch");
							if (isSuccessAccess && isSuccessAccess2) {
								doProcess(item, targetAi, 
							        dpi, nearAlartAi);
							} else {
								Task.Factory.StartNew(() => {
									for (var i = 0; i < 200; i++) {
										isSuccessAccess = getAlartProcess(dpi, ref isChanged, 
											ref targetAi, ref nearAlartAi, ref item,
										alartListDataSource);
								        isSuccessAccess2 = getAlartProcess(dpi, ref isChanged, 
											ref targetAi, ref nearAlartAi, ref item,
										form.userAlartListDataSource);
										if (isSuccessAccess && isSuccessAccess2) break;
										Thread.Sleep(3000);
									}
								    doProcess(item, targetAi, 
								        dpi, nearAlartAi);
								});
							}
							
						} catch (Exception e) {
							util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
						}
					}
					break;
				} catch (Exception ee) {
					util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
					Thread.Sleep(1000);
				}
			}
			return isChanged;
		}
		public bool getAlartProcess(DoProcessInfo dpi, ref bool isChanged, 
				ref List<AlartInfo> targetAi,
				ref AlartInfo nearAlartAi, ref RssItem item, 
				SortableBindingList<AlartInfo> dataSource) {
			//return isSuccessAccess
			while (true) {
				try {
					foreach (var alartItem in dataSource) {
						var isSuccessAccess = false;
						bool isNosetComId, isNosetHostName, isNosetKeyword,
								isComOk, isUserOk, isKeywordOk;
						var isAlart = isAlartItem(item, alartItem, out isSuccessAccess,
								out isNosetComId, out isNosetHostName, out isNosetKeyword,
								out isComOk, out isUserOk, out isKeywordOk, ref nearAlartAi);
						
						if (!isSuccessAccess) return false;
						if (!isAlart) continue;
						
						/*
						if (isUserOk && !isUserIdFromLvidOk(item, alartItem.hostId)) {
							if (!isAlartMatch(alartItem, isComOk, 
									false, isKeywordOk, isNosetComId, 
									isNosetHostName, isNosetKeyword)) {
								if ((!isNosetComId && isComOk) ||
							    		(!isNosetHostName && isUserOk))
									nearAlartAi = alartItem;
								continue;
							}
						}
						*/
						if (alartItem.AppliA) dpi.isAppliA = true;
						if (alartItem.AppliB) dpi.isAppliB = true;
						if (alartItem.AppliC) dpi.isAppliC = true;
						if (alartItem.AppliD) dpi.isAppliD = true;
						if (alartItem.AppliE) dpi.isAppliE = true;
						if (alartItem.AppliF) dpi.isAppliF = true;
						if (alartItem.AppliG) dpi.isAppliG = true;
						if (alartItem.AppliH) dpi.isAppliH = true;
						if (alartItem.AppliI) dpi.isAppliI = true;
						if (alartItem.AppliJ) dpi.isAppliJ = true;
						if (alartItem.Popup) dpi.isPopup = true;
						if (alartItem.Baloon) dpi.isBaloon = true;
						if (alartItem.Browser) dpi.isBrowser = true;
						if (alartItem.mail) dpi.isMail = true;
						if (alartItem.sound) dpi.isSound = true;
						dpi.isLog = true;
						
						if (alartItem.communityId == "official")
							util.debugWriteLine("official");
						
						var pubDateDt = DateTime.Parse(item.pubDate);
						if (isSetLastHosoDate(isNosetComId, isNosetHostName , isNosetKeyword,
								isComOk, isUserOk, isKeywordOk, item.lvId)) {
							if (pubDateDt >= alartItem.lastHosoDt)
								form.updateLastHosoDate(alartItem, pubDateDt.ToString("yyyy/MM/dd HH:mm:ss"), item.lvId, item.isMemberOnly, item.type);
							else {
								if (pubDateDt < alartItem.lastHosoDt)
									//form.addLogText("pub<item dt " + alartItem.lastHosoDt + " " + pubDateDt);
									util.debugWriteLine("pub<item dt " + alartItem.lastHosoDt + " " + pubDateDt);
							}
						}
							
						else {
							//debug
							//form.addLogText("[最近の放送日時　非更新debug]" + DateTime.Now + item.lvId);
						}
						isChanged = true;
						//targetAi = alartItem;
						targetAi.Add(alartItem);
						item.isAlarted = true;
					}
					break;
				} catch (Exception e) {
					util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
					//return false;
					Thread.Sleep(3000);
				}
			}
			return true;
		}
		public bool isAlartItem(RssItem item, AlartInfo alartItem, 
				out bool isSuccessAccess) {
			bool isNosetComId, isNosetHostName, isNosetKeyword,
					isComOk, isUserOk, isKeywordOk;
			AlartInfo nearAlartAi = null;
			return isAlartItem(item, alartItem, out isSuccessAccess,
					out isNosetComId, out isNosetHostName, out isNosetKeyword,
					out isComOk, out isUserOk, out isKeywordOk, ref nearAlartAi);
		}
		private bool isAlartItem(RssItem item, AlartInfo alartItem, 
				out bool isSuccessAccess, out bool isNosetComId, 
				out bool isNosetHostName, out bool isNosetKeyword,
				out bool isComOk, out bool isUserOk, out bool isKeywordOk, 
				ref AlartInfo nearAlartAi) {
			isComOk = isUserOk = isKeywordOk = false;
			isSuccessAccess = true;
			if (string.IsNullOrEmpty(alartItem.communityId))
				alartItem.communityName = "";
			if (string.IsNullOrEmpty(alartItem.hostId))
				alartItem.hostName = "";
			
			
			isNosetComId = string.IsNullOrEmpty(alartItem.communityId);
			isNosetHostName = string.IsNullOrEmpty(alartItem.hostName);
			isNosetKeyword = (alartItem.isCustomKeyword && alartItem.cki == null) ||
				(!alartItem.isCustomKeyword && string.IsNullOrEmpty(alartItem.keyword));
			if (isNosetComId && isNosetHostName && isNosetKeyword) return false;
			
			isComOk = alartItem.communityId == item.comId || (alartItem.communityId == "official" && item.type == "official");
			isUserOk = alartItem.hostName == item.hostName || alartItem.hostId == item.userId;
			isKeywordOk = item.isMatchKeyword(alartItem);
			
			if ((string.IsNullOrEmpty(alartItem.communityId) !=
			     	string.IsNullOrEmpty(alartItem.communityName)) ||
			     (string.IsNullOrEmpty(alartItem.hostId) !=
			     	 string.IsNullOrEmpty(alartItem.hostName))) return false;
			
			if (isUserOk && !isNosetHostName && !isUserIdFromLvidOk(item, alartItem.hostId, out isSuccessAccess))
				isUserOk = false;
			if (!isSuccessAccess) {
				return false;
			}
			
			
			if (!isAlartMatch(alartItem, isComOk, 
					isUserOk, isKeywordOk, isNosetComId, 
					isNosetHostName, isNosetKeyword, item)) {
				if ((!isNosetComId && isComOk) ||
					    (!isNosetHostName && isUserOk)) {
					nearAlartAi = alartItem;
					isSuccessAccess = setUserId(item);
					if (!isSuccessAccess) return false;
					//isUserIdFromLvidOk(item, alartItem.hostId);
				}
				return false;
			}
			
			util.debugWriteLine("ok alart item " + item.lvId + " " + isComOk + " " + isUserOk + " " + isKeywordOk + " noset " + isNosetComId + " " + isNosetHostName + " " + isNosetKeyword + " ai.hostId " + alartItem.hostId + " rss.hostId " + item.userId);
			isSuccessAccess = setUserId(item);
			if (!isSuccessAccess) return false;
			return true;
		}
		private void doProcess(RssItem item, 
		        List<AlartInfo> targetAi, 
		        DoProcessInfo dpi, AlartInfo nearAlartAi) {
			if (processedLvidList.IndexOf(item.lvId) > -1) {
				util.debugWriteLine("processed lvid found " + item.lvId + " " + item.comName + " " + item.hostName);
				return;
			}
			
			if (dpi.isDoProcess())
				processedLvidList.Add(item.lvId);
			

			util.debugWriteLine(item.lvId + " A " + dpi.isAppliA + " B " + dpi.isAppliB + " C " + dpi.isAppliC + " D " + dpi.isAppliD + " E " + dpi.isAppliE + " F " + dpi.isAppliF + " G " + dpi.isAppliG + " H " + dpi.isAppliH + " I " + dpi.isAppliI + " J " + dpi.isAppliJ + " pop " + dpi.isPopup + " balloon " + dpi.isBaloon);
			processAlart(item, targetAi, 
			        dpi);
			
			if (dpi.isLog && form.config.get("log") == "true")
				writeFavoriteLog(item);
			
			if (targetAi.Count > 0) {
				addLogList(item, targetAi);
				form.setNotifyMenuHistory(new List<RssItem>(){item});
				if (bool.Parse(form.config.get("IsAddAlartedComUser")))
					addAlartedComUser(item);
			} else if (nearAlartAi != null)
				addNearAlartList(item, nearAlartAi);
				
		}
		private bool isAlartMatch(AlartInfo alartItem, bool isComOk, 
				bool isUserOk, bool isKeywordOk, bool isNosetComId, 
				bool isNosetHostName, bool isNosetKeyword, RssItem ri) {
			if (!isComOk && !isUserOk && !isKeywordOk) return false;
			if (alartItem.isMustCom && !isNosetComId && !isComOk) 
				return false;
			if (alartItem.isMustUser && !isNosetHostName && !isUserOk) 
				return false;
			if (alartItem.isMustKeyword && !isNosetKeyword && !isKeywordOk) 
				return false;
			
			/*
			if ((!isNosetComId && isComOk) ||
				    (!isNosetHostName && isUserOk) ||
				    (!isNosetKeyword && isKeywordOk)) {
				
			} else {
				continue;
			}
			*/
			
			if (!(!isNosetComId && isComOk) &&
				    !(!isNosetHostName && isUserOk) &&
				    !(!isNosetKeyword && isKeywordOk)) {
				return false;
			}
			if (!isMemberOnlyOk(alartItem, ri)) return false;
			
			return true;
		}
		private void appliProcess(string appliPath, string lvid, string args) {
			if (appliPath == null || appliPath == "") return;
			var url = "https://live2.nicovideo.jp/watch/lv" + util.getRegGroup(lvid, "(\\d+)");

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
		private void checkUserNameAndFollow(bool isUserMode) {
			try {
				var count = form.getAlartListCount(isUserMode);
				var dataSource = isUserMode ? form.userAlartListDataSource : form.alartListDataSource;
				
				var idList = new List<string>();
				for (var i = 0; i < count; i++) {
					var ai = (AlartInfo)dataSource[i];
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
							var userName = util.getUserName(id, out isFollow, form.check.container, true, form.config);
							if (userName == null) {
								//Thread.Sleep(3000);
								continue;
							}
							
							for (var i = 0; i < count; i++) {
								var ai = (AlartInfo)dataSource[i];
								if (ai.hostId == id) {
									form.updateUserName(i, userName, isFollow, isUserMode);
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
		private void processAlart(RssItem item, 
		        List<AlartInfo> targetAi,
		        DoProcessInfo dpi) {
			if (dpi.isAppliA && !form.notifyOffList[7]) {
				var appliAPath = form.config.get("appliAPath");
				var args = form.config.get("appliAArgs");
				appliProcess(appliAPath, item.lvId, args);
			}
			if (dpi.isAppliB && !form.notifyOffList[8]) {
				var appliBPath = form.config.get("appliBPath");
				var args = form.config.get("appliBArgs");
				appliProcess(appliBPath, item.lvId, args);
			}
			if (dpi.isAppliC && !form.notifyOffList[9]) {
				var appliCPath = form.config.get("appliCPath");
				var args = form.config.get("appliCArgs");
				appliProcess(appliCPath, item.lvId, args);
			}
			if (dpi.isAppliD && !form.notifyOffList[10]) {
				var appliDPath = form.config.get("appliDPath");
				var args = form.config.get("appliDArgs");
				appliProcess(appliDPath, item.lvId, args);
			}
			if (dpi.isAppliE && !form.notifyOffList[11]) {
				var appliEPath = form.config.get("appliEPath");
				var args = form.config.get("appliEArgs");
				appliProcess(appliEPath, item.lvId, args);
			}
			if (dpi.isAppliF && !form.notifyOffList[12]) {
				var appliFPath = form.config.get("appliFPath");
				var args = form.config.get("appliFArgs");
				appliProcess(appliFPath, item.lvId, args);
			}
			if (dpi.isAppliG && !form.notifyOffList[13]) {
				var appliGPath = form.config.get("appliGPath");
				var args = form.config.get("appliGArgs");
				appliProcess(appliGPath, item.lvId, args);
			}
			if (dpi.isAppliH && !form.notifyOffList[14]) {
				var appliHPath = form.config.get("appliHPath");
				var args = form.config.get("appliHArgs");
				appliProcess(appliHPath, item.lvId, args);
			}
			if (dpi.isAppliI && !form.notifyOffList[15]) {
				var appliIPath = form.config.get("appliIPath");
				var args = form.config.get("appliIArgs");
				appliProcess(appliIPath, item.lvId, args);
			}
			if (dpi.isAppliJ && !form.notifyOffList[16]) {
				var appliJPath = form.config.get("appliJPath");
				var args = form.config.get("appliJArgs");
				appliProcess(appliJPath, item.lvId, args);
			}
			if (dpi.isPopup && !form.notifyOffList[2] && targetAi.Count > 0) {
				displayPopup(item, targetAi[0]);
			}
			if (dpi.isBaloon && !form.notifyOffList[3] && targetAi.Count > 0) {
				displayBaloon(item, targetAi[0]);
			}
			if (dpi.isBrowser && !form.notifyOffList[4]) {
				openBrowser(item);
			}
			if (dpi.isMail && !form.notifyOffList[5]) {
				mail(item);
			}
			if (dpi.isSound && !form.notifyOffList[6] && targetAi.Count > 0) {
				sound(item, targetAi[0]);
			}
		}
		public void setCookie(bool isDisplayLog = true, bool isFollowCheck = true) {
			try {
				var url = "https://www.nicovideo.jp/my/top";
				url = "https://live.nicovideo.jp/my";
				
				var cg = new CookieGetter(form.config, form);
				var res = cg.getHtml5RecordCookie(url, false).Result;
				if (res == null || res[0] == null) {
					if (isDisplayLog)
						form.addLogText("Cookieの取得を確認できませんでした", true);
				} else {
					if (isDisplayLog)
						form.addLogText("Cookieの取得に成功しました", true);
					container = res[0];
				}
			} catch (Exception e) {util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);}
			
			if ((alartListDataSource.Count > 0 || form.userAlartListDataSource.Count > 0) && isFollowCheck) {
				Task.Factory.StartNew(() => new FollowChecker(form, container).check());
			}
		}
		public bool isUserIdFromLvidOk(RssItem rssItem, string alartUserId, out bool isSuccessAccess) {
			util.debugWriteLine("isUserIdFromLvidOk id " + alartUserId);
			isSuccessAccess = true;
			if (string.IsNullOrEmpty(alartUserId)) return true;
			if (rssItem.comId == null || rssItem.comId.StartsWith("ch")) return true;
			
			isSuccessAccess = setUserId(rssItem);
			if (!isSuccessAccess) return true;
			//var uid = (rssItem.userId != null) ? rssItem.userId : getUserIdFromLvid(rssItem.lvId);
			//if (rssItem.userId == null && uid != null) rssItem.userId = uid;
			/*
			var res = util.getPageSource("http://live.nicovideo.jp/api/getplayerstatus/" + lvid, container);
			if (res == null) return false;
			var uid = util.getRegGroup(res, "<owner_id>(.+?)</owner_id>");
			*/
			
			//if (uid == null) return false;
			util.debugWriteLine("isUserIdFromLvidOk rssItem.userId " + rssItem.userId);
			if (rssItem.userId == null) return true;
			return rssItem.userId == alartUserId;
		}
		public string getUserIdFromLvid(string lvid, out bool isFailureAccess, CookieContainer container) {
			var url = "https://live2.nicovideo.jp/watch/" + lvid;
			var res = util.getPageSource(url, container);
			isFailureAccess = res == null;
			//if (isFailureAccess) {
			//	return null;
			//}
			
			
			if (res == null) {
				return null;
			}
			var hig = new HosoInfoGetter();
			hig.setNicoLiveInfo(res);
			return hig.userId;
			
			/*
			if (res != null) {
				var hig = new HosoInfoGetter();
				hig.setNicoLiveInfo(res);
				if (!string.IsNullOrEmpty(hig.userId))
				    return hig.userId;
			}
			if (container == null) return null;
			
			util.debugWriteLine("getPlayerStatus userId check " + lvid);
			url = "https://live.nicovideo.jp/api/getplayerstatus/" + lvid;
			res = util.getPageSource(url, container);
			if (res == null) return null;
			var ret = util.getRegGroup(res, "<owner_name>(.*)</owner_name>");
			return ret;
			*/
			//return uid;
		}
		private bool setUserId(RssItem rssItem) {
			//return isSuccess
			var isFailureAccess = false;
			var uid = (rssItem.userId != null) ? rssItem.userId : getUserIdFromLvid(rssItem.lvId, out isFailureAccess, container);
			if (isFailureAccess) return false;  
			if (!rssItem.comId.StartsWith("co")) uid = "";
			if (rssItem.userId == null && uid != null) rssItem.userId = uid;
			return true;
		}
		/*
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
		*/
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
				if (!Directory.Exists(util.getJarPath()[0] + "/Log")) Directory.CreateDirectory("Log");
				while (true) {
					try {
						foreach(var ri in items) {
							var dt = DateTime.Parse(ri.pubDate);
							var p = util.getJarPath()[0] + "/Log/broadLog-" + dt.ToString("yyyy-MM-dd") + ".txt";
							using (var sw = new StreamWriter(p, true)) {
								writeLog(sw, ri);
								//sw.Close();
							}
						}
						return;
					} catch (Exception ee) {
						util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
					}
				}
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
		}
		//private StreamWriter favoriteLogSw = null;  
		private void writeFavoriteLog(RssItem ri) {
			//namaroku
			//2019/01/16 19:26:35 さくらこ♡－ lv317994167 co3871789
			//実験放送？　2019/01/16 19:30:00 － lv317992746 co2652877
			//2019/01/16 19:49:57 公式生放送－ lv317613421 official
			try {
				if (!Directory.Exists(util.getJarPath()[0] + "/Log")) 
					Directory.CreateDirectory("Log");
				while (true) {
					try {
						var dt = DateTime.Parse(ri.pubDate);
						using (var sw = new StreamWriter(util.getJarPath()[0] + "/Log/favoriteLog-" + dt.ToString("yyyy-MM-dd") + ".txt", true)) {
							//	favoriteLogSw = new StreamWriter(util.getJarPath()[0] + "/Log/favoriteLog.txt", true);
							writeLog(sw, ri);
							//sw.Close();
							return;
						}
					} catch (Exception ee) {
						util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
					}
				}
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
		}
		private void writeLog(StreamWriter sw, RssItem ri) {
			var tags = ri.getTag(categoryReg);
			var isJikken = Array.IndexOf(tags, "実験放送") > -1;
			var br = "";
			//sw.WriteLine("[放送開始時間] " + DateTime.Parse(ri.pubDate).ToString("yyyy/MM/dd HH:mm:ss") + br);
			sw.WriteLine("[放送開始時間] " + ri.pubDate + br);
			sw.WriteLine("[タイトル] " + ri.title + br);
			sw.WriteLine("[限定] " + ((ri.isMemberOnly) ? "限定" : "オープン") + br);
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
				checkedLvIdList.RemoveRange(0, checkedLvIdList.Count - 18000);
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
		}
		private void sound(RssItem ri, AlartInfo ai) {
			util.playSound(form.config, ai, form);
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
			var url = "https://live2.nicovideo.jp/watch/lv" + util.getRegGroup(item.lvId, "(\\d+)");
			util.openUrlBrowser(url, form.config);
		}
		public void foundLive(List<RssItem> items, bool isAddLive = true) {
			if (items.Count == 0) return;
			
			form.setHosoLogStatusBar(items[0]);
			lock(foundLiveLock) {
				var isChanged = gotStreamProcess(items);
				if (isChanged) {
					form.sortAlartList(false);
					form.sortAlartList(true);
				}
			}
			
			if (items.Count > 0 && form.config.get("IsbroadLog") == "true")
				Task.Factory.StartNew(() => writeBroadLog(items));
			
			var addLiveMode = form.config.get("alartAddLive");
			if (addLiveMode != "0" && isAddLive)
				Task.Factory.StartNew(() => addLiveList(items));
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
				Task.Factory.StartNew(() => {
					pr = new PushReceiver(this, form.config);
					pr.start();
				});
			}
			if (bool.Parse(form.config.get("IsAppPush"))) {
				Task.Factory.StartNew(() => {
					apr = new AppPushReceiver(this, form.config);
					apr.start();
				});
			}
			
			if (bool.Parse(form.config.get("IsRss"))) {
				if (rc == null) {
					Task.Factory.StartNew(() => {
						rc = new CategoryCheck(this, form.config);
						rc.start();
					});
				}
			} else {
				if (rc != null) {
					rc.stop();
					rc = null;
				}
			}
			if (bool.Parse(form.config.get("IsTimeTable"))) {
				if (ttc == null) {
					Task.Factory.StartNew(() => {
						ttc = new TimeTableChecker(this, form.config);
						ttc.start();
					});
				}
			} else {
				if (ttc != null) {
					ttc.stop();
					ttc = null;
				}
			}
			if (bool.Parse(form.config.get("IsAutoReserve"))) {
				if (arc == null) {
					Task.Factory.StartNew(() => {
						arc = new AutoReserveChecker(this, form.config);
						arc.start();
					});
				}
			} else {
				if (arc != null) {
					arc.stop();
					arc = null;
				}
			}
			userNameUpdateInterval = int.Parse(form.config.get("userNameUpdateInterval"));
		}
		public void regularlyProcess() {
			var lastUserNameCheckTime = DateTime.Now;
			var lastCheckLastRecentLiveTime = DateTime.Now;
			var lastSaveListTime = DateTime.Now;
			var lastCheckHistoryLiveTime = DateTime.Now;
			var lastGetCookieTime = DateTime.Now;
			while (true) {
				/*
				var ut = userNameUpdateInterval;
				if (ut < 15) ut = 15;
				if (DateTime.Now - lastUserNameCheckTime > TimeSpan.FromSeconds(ut * 60)) {
					Task.Factory.StartNew(() => {
					    new FollowChecker(form, container).check();
					    
					    lastUserNameCheckTime = DateTime.MaxValue;
			         	checkUserNameAndFollow(true);
			         	checkUserNameAndFollow(false);
			         	lastUserNameCheckTime = DateTime.Now;
					});
					//Task.Factory.StartNew(() => new FollowChecker(form, this).check());
					
				}
				*/
				var intervalSec = bool.Parse(form.config.get("IscheckOnAir")) ? 180 : 180;
				if (DateTime.Now - lastCheckLastRecentLiveTime > TimeSpan.FromSeconds(intervalSec)) {
					Task.Factory.StartNew(() => {
					    lastCheckLastRecentLiveTime = DateTime.MaxValue;
					    form.recentLiveCheck();
			         	lastCheckLastRecentLiveTime = DateTime.Now;
					});
				}
				
				if (DateTime.Now - form.lastChangeListDt > TimeSpan.FromMinutes(5)) {
					form.lastChangeListDt = DateTime.MaxValue;
					Task.Factory.StartNew(() => {
						new AlartListFileManager(false, form).save();
						new AlartListFileManager(true, form).save();
						new TaskListFileManager().save(form);
						new HistoryListFileManager().save(form);
						new NotAlartListFileManager().save(form);
						//new TwitterListFileManager().save(form);
						new ReserveHistoryListFileManager().save(form);
					});
				}
				
				if (DateTime.Now - lastCheckHistoryLiveTime > TimeSpan.FromMinutes(5)) {
					lastCheckHistoryLiveTime = DateTime.MaxValue;
					Task.Factory.StartNew(() => {
					    form.checkHistoryLive();
						lastCheckHistoryLiveTime = DateTime.Now;
					});
				}
				
				if (DateTime.Now - lastGetCookieTime> TimeSpan.FromHours(1)) {
					lastGetCookieTime= DateTime.MaxValue;
					Task.Factory.StartNew(() => {
						setCookie(false, false);
						lastGetCookieTime = DateTime.Now;
					});
				}
				Thread.Sleep(10000);
			}
			
		}
		public void mail(RssItem item) {
			try {
				util.debugWriteLine("pubdate mail " + item.pubDate);
				string dt = DateTime.Parse(item.pubDate).ToString("yyyy/MM/dd(ddd) HH:mm");
				string title, body;
				body = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\n";
				if (item.hostName != "" && item.hostName != null) {
					title = "[ニコ生]" + item.hostName + "の放送開始";
					body += item.hostName + " が " + item.comName + " で " + item.title + " - " + dt + "開始 を開始しました。\n";
				} else {
					title = "[ニコ生]" + item.comName + "の放送開始";
					body += item.comName + " で " + item.title + " - " + dt + "開始 を開始しました。\n";
				}
				body += "https://live.nicovideo.jp/watch/" + item.lvId + "\n";
				if (item.comId != null && item.comId.StartsWith("co"))
					body += "https://com.nicovideo.jp/community/" + item.comId;
				else if (item.comId != null && item.comId.StartsWith("ch"))
					body += "https://ch.nicovideo.jp/" + item.comId;
				
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
		private bool isSetLastHosoDate(bool isNosetComId, 
				bool isNosetHostName, bool isNosetKeyword,
				bool isComOk, bool isUserOk, bool isKeywordOk, string lvid) {
			util.debugWriteLine("isSetLastHosoDate " + lvid + " isNosetComId " + isNosetComId + " isNosetHostName " + isNosetHostName + " isnosetKey " + isNosetKeyword + " iscomok " + isComOk + " isuserok " + isUserOk + " iskeyok " + isKeywordOk);
			if (!bool.Parse(form.config.get("IsNotAllMatchNotifyNoRecent")))
				return true;
			if (!isNosetComId && !isComOk) return false;
			if (!isNosetHostName && !isUserOk) return false;
			if (!isNosetKeyword && !isKeywordOk) return false;
			
			return true;
		}
		private void addLogList(RssItem item, List<AlartInfo> targetAi) {
			var hi = new HistoryInfo(item, targetAi);
			//hi.userId = item.userId;
			form.addHistoryList(hi);
		}
		private void addNearAlartList(RssItem item, AlartInfo nearAlartAi) {
			var a = new List<AlartInfo>(){nearAlartAi};
			var hi = new HistoryInfo(item, a);
			
			var isNosetComId = nearAlartAi.communityId == "" ||
					nearAlartAi.communityId == null;
			var isNosetHostName = nearAlartAi.hostName == "" ||
					nearAlartAi.hostName == null;
			var isNosetKeyword = (nearAlartAi.isCustomKeyword && nearAlartAi.cki == null) ||
				(!nearAlartAi.isCustomKeyword && nearAlartAi.keyword == "" || nearAlartAi.keyword == null);
			//if (isNosetComId && isNosetHostName && isNosetKeyword) continue;
			
			var isComOk = nearAlartAi.communityId == item.comId;
			var isUserOk = nearAlartAi.hostName == item.hostName;
			var isKeywordOk = item.isMatchKeyword(nearAlartAi);
			var isMemberOnlyOk = this.isMemberOnlyOk(nearAlartAi, item);
			
			hi.isInListCom = (!isNosetComId && isComOk);
			hi.isInListUser = (!isNosetHostName && isUserOk);
			hi.isInListKeyword = (!isNosetKeyword && isKeywordOk);
			hi.isInListMemberOnly = isMemberOnlyOk; 
			hi.backColor = nearAlartAi.backColor;
			hi.textColor = nearAlartAi.textColor;
			//hi.userId = nearAlartAi.hostId;
			hi.keyword = nearAlartAi.keyword;
			form.addNotAlartList(hi);
		}
		private void addLiveList(List<RssItem> items) {
			try {
				var isBlindA = bool.Parse(form.config.get("BlindOnlyA"));
				var isBlindB = bool.Parse(form.config.get("BlindOnlyB"));
				var isBlindQuestion = bool.Parse(form.config.get("BlindQuestion"));
				var isFavoriteOnly = bool.Parse(form.config.get("FavoriteOnly"));
				var cateChar = form.getCategoryChar();

				if (Thread.CurrentThread == form.madeThread)
					util.debugWriteLine("lock form thread addLiveList");
				
				
				
				//var sI = form.liveList.FirstDisplayedScrollingRowIndex;
				_addLiveList(items, isBlindA, isBlindB, isBlindQuestion, isFavoriteOnly, cateChar);
				//form.setScrollIndex(form.liveList, sI);
				
				form.setLiveListNum();
				
				
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
		}
		private void _addLiveList(List<RssItem> items, bool isBlindA, bool isBlindB, bool isBlindQuestion, bool isFavoriteOnly, char cateChar) {
			var alartAddLive = form.config.get("alartAddLive");
			var addItems = new List<LiveInfo>();
			var _alartListDataSource = form.alartListDataSource.ToArray();
			var _liveListDataSource = form.liveListDataSource.ToArray();
			var _liveListDataReserve = form.liveListDataReserve.ToArray();
			foreach (var item in items) {
				if (alartAddLive == "2" && !item.isAlarted) continue;
				
				var li = new LiveInfo(item, _alartListDataSource, form.config, form.userAlartListDataSource.ToArray());
				
				var isContain = false;
				foreach (var a in _liveListDataSource)
					if (a.lvId == item.lvId) 
						isContain = true;
				foreach (var a in _liveListDataReserve)
					if (a.lvId == item.lvId) 
						isContain = true;
				if (isContain) 
					continue;
				
				var sameCommunityLive = new List<LiveInfo>();
				foreach (var l in _liveListDataSource)
					if (!string.IsNullOrEmpty(l.comId) && l.comId.StartsWith("co") && l.comId == item.comId)
						sameCommunityLive.Add(l);
				foreach (var l in _liveListDataReserve)
					if (!string.IsNullOrEmpty(l.comId) && l.comId.StartsWith("co") && l.comId == item.comId)
						sameCommunityLive.Add(l);
				foreach (var l in sameCommunityLive)
					form.removeLiveListItem(l);
				
				addItems.Add(li);
			}
			
			var isFavoriteTop = bool.Parse(form.config.get("FavoriteUp"));
			form.liveListLockAction(() => {
				form.addLiveListItem(addItems, cateChar, isBlindA, isBlindB, isBlindQuestion, isFavoriteOnly);
				form.removeDuplicateLiveList();
			});
				
			if (bool.Parse(form.config.get("AutoSort")))
				form.sortLiveList();
			form.liveListLockAction(() => {
				if (isFavoriteTop)
					form.upLiveListFavorite();
			});
		}
		private bool isMemberOnlyOk(AlartInfo alartItem, RssItem ri) {
			var c = alartItem.memberOnlyMode;
			if (c.IndexOf(",") == -1) {
				if (c == "0") alartItem.memberOnlyMode = "True,True,True";
				else if (c == "1") alartItem.memberOnlyMode = "True,False,False";
				else if (c == "2") alartItem.memberOnlyMode = "False,True,True";
			}
			
			var types = alartItem.memberOnlyMode.Split(',');
			if (!bool.Parse(types[0]) && !ri.isMemberOnly && !ri.isPayment) return false;
			else if (!bool.Parse(types[1]) && ri.isMemberOnly) return false;
			else if (!bool.Parse(types[2]) && ri.isPayment) return false;
			return true;
		}
		private void addAlartedComUser(RssItem ri) {
			try {
				var _alartListDataSource = form.alartListDataSource.ToList();
				var _userAlartListDataSource = form.userAlartListDataSource.ToList();
				bool isContainCom = string.IsNullOrEmpty(ri.comId), isContainUser = string.IsNullOrEmpty(ri.userId);
				foreach (var ai in _alartListDataSource) {
					if (!string.IsNullOrEmpty(ai.communityId) && 
					    	ai.communityId != "official" && ai.communityId == ri.comId) 
						isContainCom = true;
					if (!string.IsNullOrEmpty(ai.hostId) && 
					    	ai.hostId == ri.userId) 
						isContainUser = true;
				}
				foreach (var ai in _userAlartListDataSource) {
					if (!string.IsNullOrEmpty(ai.hostId) && 
					    	ai.hostId == ri.userId) 
						isContainUser = true;
				}
				//form.addLogText("放送ID:" + ri.lvId + " コミュID:" + ri.comId + " ユーザーID:" + ri.userId + " お気に入りリスト内に放送を開始したコミュニティが見つかり" + (isContainCom ? "ました" : "ませんでした") + " お気に入りリスト内に放送を開始したユーザーが見つかり" + (isContainUser ? "ました" : "ませんでした"));
				
				var textColor = ColorTranslator.FromHtml(form.config.get("defaultTextColor"));
				var backColor = ColorTranslator.FromHtml(form.config.get("defaultBackColor"));
				var behaviors = form.config.get("defaultBehavior").Split(',').Select<string, bool>(x => x == "1").ToArray();
				var isAutoReserve = bool.Parse(form.config.get("IsDefaultAutoReserve"));
				var now = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
				if (!isContainCom && !string.IsNullOrEmpty(ri.comId)) {
					var isFollow = false;
					var comName = util.getCommunityName(ri.comId, out isFollow, container);
					var comFollow = (comName == null || container == null) ? "" : (isFollow ? "フォロー解除する" : "フォローする");
					
					var ai = new AlartInfo(ri.comId, ri.userId, 
							ri.comName, ri.hostName, "", now, false, false, 
							false, false, false, false, false, 
							false, false, false, false, false, 
							false, false, false, "", 
							comFollow, "", "", "", "True,True,True", isAutoReserve, 0);
					ai.setBehavior(behaviors);
					ai.textColor = textColor;
					ai.backColor = backColor;
					alartListDataSource.Add(ai);
				}
				if (!isContainUser && !string.IsNullOrEmpty(ri.userId)) {
					var isFollow = false;
					var userName = util.getUserName(ri.userId, out isFollow, container, true, form.config);
					var userFollow = (userName == null || container == null) ? "" : (isFollow ? "フォロー解除する" : "フォローする");
					
					var ai = new AlartInfo(ri.comId, ri.userId, 
							ri.comName, ri.hostName, "", now, false, false, 
							false, false, false, false, false, 
							false, false, false, false, false, 
							false, false, false, "", 
							"", userFollow, "", "", "True,True,True", isAutoReserve, 0);
					ai.setBehavior(behaviors);
					ai.textColor = textColor;
					ai.backColor = backColor;
					if (!bool.Parse(form.config.get("IsAddAlartedUserToUserList")))
						alartListDataSource.Add(ai);
					else {
						ai.communityId = null;
						ai.communityName = null;
						form.userAlartListDataSource.Add(ai);
					}
				}
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
		}

	}
	public class DoProcessInfo {
		public bool isAppliA;
		public bool isAppliB;
		public bool isAppliC;
		public bool isAppliD;
		public bool isAppliE;
		public bool isAppliF;
		public bool isAppliG;
		public bool isAppliH;
		public bool isAppliI;
		public bool isAppliJ;
		public bool isPopup;
		public bool isBaloon;
		public bool isBrowser;
		public bool isMail;
		public bool isSound;
		public bool isLog;
		public DoProcessInfo() {}
		public DoProcessInfo(bool isAppliA, bool isAppliB, 
		        bool isAppliC, bool isAppliD, bool isAppliE, 
		        bool isAppliF, bool isAppliG, bool isAppliH, 
		        bool isAppliI, bool isAppliJ, bool isPopup, 
		        bool isBaloon, bool isBrowser, bool isMail, 
		        bool isSound, bool isLog) {
			this.isAppliA = isAppliA;
			this.isAppliB = isAppliB;
			this.isAppliC = isAppliC;
			this.isAppliD = isAppliD;
			this.isAppliE = isAppliE;
			this.isAppliF = isAppliF;
			this.isAppliG = isAppliG;
			this.isAppliH = isAppliH;
			this.isAppliI = isAppliI;
			this.isAppliJ = isAppliJ;
			this.isPopup = isPopup;
			this.isBaloon = isBaloon;
			this.isBrowser = isBrowser;
			this.isMail = isMail;
			this.isSound = isSound;
			this.isLog = isLog;
		}
		public bool isDoProcess() {
			return isAppliA || isAppliB || isAppliC ||
					isAppliD || isAppliE || isAppliF || isAppliG ||
					isAppliH || isAppliI || isAppliJ || isPopup ||
					isBaloon || isBrowser || isMail || isSound;
		}
	}
}
