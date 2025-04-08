/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2021/03/03
 * Time: 17:37
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Runtime.InteropServices;
using namaichi.info;
using namaichi.rec;

namespace namaichi.alart
{
	/// <summary>
	/// Description of TwitterCheck.
	/// </summary>
	public class TwitterCheck
	{
		private MainForm form;
		private SortableBindingList<TwitterInfo> twitterListDataSource;
		private DateTime startTime = DateTime.Now;
		
		public TwitterCheck(SortableBindingList<TwitterInfo> twitterListDataSource, MainForm form)
		{
			this.twitterListDataSource = twitterListDataSource;
			this.form = form;
		}
		public void start() {
			while (true) {
				var now = DateTime.Now;
				try {
					var twList = twitterListDataSource.ToArray();
					var alartLvList = new List<TwitterInfo>();
					foreach (var ti in twList) {
						var timelineLvList = getUserTweetLvidList(ti);
						if (timelineLvList == null) continue;
						process(timelineLvList, ti);
					}
				} catch (Exception e) {
					util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				}
				Thread.Sleep(60 * 10 * 1000);
			}
		}
		private List<string> getUserTweetLvidList(TwitterInfo ti) {
			var timeline = getTimeline(ti.id);
			util.debugWriteLine("timeline get " + ti.id + " " + ti.account + " " + timeline);
			if (timeline == null) return null;
			var ret = new List<string>();
			
			var reg = new Regex("#(lv\\d+)");
        	var m = reg.Matches(timeline);
        	
        	foreach (Match _m in m) {
        		var t = _m.Groups[1].Value;
        		util.debugWriteLine("getUserTweetLvid " + ti.account + " " + t);
        		if (t != null && 
				    	//form.check.checkedLvIdList.Find(x => x.lvId == t) == null &&
				    	form.historyListDataSource.Where(x => x.lvid == t).Count() == 0 &&
				    	//&& form.notAlartListDataSource.Where(x => x.lvid == t).Count() == 0
				    	form.check.processedLvidList.IndexOf(t) == -1
				    )
        			ret.Add(t);
        	}
			return ret;
		}
		[DllImport("_libcurl.dll")]
        public static extern int globalInit();
        [DllImport("_libcurl.dll")]
        public static extern void globalCleanup();
        [DllImport("_libcurl.dll")]
        public static extern void easyReset(int easy);
        [DllImport("_libcurl.dll")]
        public static extern void easyCleanup(int easy);
        [DllImport("_libcurl.dll")]
        public static extern int easyInit();
        [DllImport("_libcurl.dll")]
        public static extern StringBuilder easyGetData(int easy, string url, string headerStr);
        [DllImport("_libcurl.dll")]
        public static extern int curl_easy_setoptInt(int easy, int opt, int val);
        [DllImport("_libcurl.dll")]
        public static extern int curl_easy_setoptStr(int easy, int opt, string val);
        [DllImport("_libcurl.dll")]
        public static extern int curl_easy_setoptFunc(int easy, int opt, IntPtr val);
        [DllImport( "libcurl.dll" )]
		public static extern IntPtr curl_slist_append(IntPtr p, string s);
		[DllImport( "libcurl.dll" )]
		public static extern void curl_slist_free_all(IntPtr pList);
		public static string isUserExist(string name) {
        	try {
	        	var url =  "https://api.twitter.com/2/users/by?usernames=" + name + "&user.fields=description,created_at";
	        	var bearer = "";
	        	
	        	//var curl = globalInit();
	        	var easy = easyInit();
	        	var r = easyGetData(easy, url, "Authorization: Bearer " + bearer);
	        	//var r = easyGetData(easy, url, cookie);
	        	easyCleanup(easy);
	        	//globalCleanup();
	        	util.debugWriteLine(r);
	        	if (r == null ||	        	
	        	    	r.ToString().IndexOf("{\"errors\":") > -1) return null;
	        	return util.getRegGroup(r.ToString(), "\"id\":\"(\\d+)");
	        	
        	} catch (Exception ee) {
        		util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace);
        		return null;
        	}
        }
        private string getTimeline(string id) {
        	try {
	        	var url =  "https://api.twitter.com/2/users/" + id + "/tweets?tweet.fields=created_at";

	        	var bearer = "";
	        	
	        	//var curl = globalInit();
	        	var easy = easyInit();
	        	var r = easyGetData(easy, url, "Authorization: Bearer " + bearer);
	        	
	        	//Debug.WriteLine(r);
	        	
	        	//var r = easyGetData(easy, url, cookie);
	        	easyCleanup(easy);
	        	//globalCleanup();
	        	util.debugWriteLine(r);
	        	if (r == null) return null;
	        	
	        	return r.ToString();
	        	
        	} catch (Exception ee) {
        		util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace);
        		return null;
        	}
        }
		private void process(List<string> lvList, TwitterInfo ti) {
			foreach (var lv in lvList) {
				try {
					var hig = new HosoInfoGetter();
					RssItem item;
					if (hig.get("https://live.nicovideo.jp/watch/" + lv, form.check.container)) {
						item = new RssItem(hig.title, lv, hig.openDt.ToString(), hig.description, hig.group, hig.communityId, hig.userName, null, hig.isMemberOnly ? "限定" : "", "", hig.isPayment);
						item.setUserId(hig.userId);
						item.setTag(hig.tags);
						item.category = hig.category;
						item.type = hig.type;
						if (hig.openDt < startTime || hig.openDt > DateTime.Now + TimeSpan.FromMinutes(3)) return;
					} else item = new RssItem(lv, lv, DateTime.Now.ToString(), "", "", "",
					                     "", "", "", "", false);
					
					
					
					form.check.checkedLvIdList.Add(item);
					form.check.processedLvidList.Add(item.lvId);
							               
					if (ti.appliA && !form.notifyOffList[7]) {
						var appliPath = form.config.get("appliAPath");
						var args = form.config.get("appliAArgs");
						appliProcessFromLvid(appliPath, lv, args, 0);
					}
					if (ti.appliB && !form.notifyOffList[8]) {
						var appliPath = form.config.get("appliBPath");
						var args = form.config.get("appliBArgs");
						appliProcessFromLvid(appliPath, lv, args, 1);
					}
					if (ti.appliC && !form.notifyOffList[9]) {
						var appliPath = form.config.get("appliCPath");
						var args = form.config.get("appliCArgs");
						appliProcessFromLvid(appliPath, lv, args, 2);
					}
					if (ti.appliD && !form.notifyOffList[10]) {
						var appliPath = form.config.get("appliDPath");
						var args = form.config.get("appliDArgs");
						appliProcessFromLvid(appliPath, lv, args, 3);
					}
					if (ti.appliE && !form.notifyOffList[11]) {
						var appliPath = form.config.get("appliEPath");
						var args = form.config.get("appliEArgs");
						appliProcessFromLvid(appliPath, lv, args, 4);
					}
					if (ti.appliF && !form.notifyOffList[12]) {
						var appliPath = form.config.get("appliFPath");
						var args = form.config.get("appliFArgs");
						appliProcessFromLvid(appliPath, lv, args, 5);
					}
					if (ti.appliG && !form.notifyOffList[13]) {
						var appliPath = form.config.get("appliGPath");
						var args = form.config.get("appliGArgs");
						appliProcessFromLvid(appliPath, lv, args, 6);
					}
					if (ti.appliH && !form.notifyOffList[14]) {
						var appliPath = form.config.get("appliHPath");
						var args = form.config.get("appliHArgs");
						appliProcessFromLvid(appliPath, lv, args, 7);
					}
					if (ti.appliI && !form.notifyOffList[15]) {
						var appliPath = form.config.get("appliIPath");
						var args = form.config.get("appliIArgs");
						appliProcessFromLvid(appliPath, lv, args, 8);
					}
					if (ti.appliJ && !form.notifyOffList[16]) {
						var appliPath = form.config.get("appliJPath");
						var args = form.config.get("appliJArgs");
						appliProcessFromLvid(appliPath, lv, args, 9);
					}
					if (ti.popup && !form.notifyOffList[2]) {
						TaskCheck.displayPopup(item, form);
					}
					if (ti.baloon && !form.notifyOffList[3]) {
						TaskCheck.displayBaloon(item, form);
					}
					if (ti.browser && !form.notifyOffList[4]) {
						TaskCheck.openBrowser(item, form);
					}
					if (ti.mail && !form.notifyOffList[5]) {
						form.check.mail(item);
					}
					if (ti.sound && !form.notifyOffList[6]) {
						TaskCheck.sound(form);
					}
					var hi = new HistoryInfo(item, null);
					form.addHistoryList(hi);
				} catch (Exception e) {
					util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				}
			}
		}
		void appliProcessFromLvid(string path , string lvid, string args, int appNum) {
			util.appliProcessFromLvid(path, lvid, args, form.check.container, form.config, appNum, form);
		}
	}
}
