/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2021/03/25
 * Time: 17:32
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Linq;
using System.Linq.Expressions;
using namaichi.info;
using namaichi.rec;

namespace namaichi.alart
{
	/// <summary>
	/// Description of AutoReserveChecker.
	/// </summary>
	public class AutoReserveChecker
	{
		Check check;
		config.config config;
		private bool isRetry = true;
		private DateTime lastGetTime = DateTime.MinValue;
		private DateTime lastCheckedItemTime = DateTime.MinValue;
		
		public AutoReserveChecker(Check check, config.config config)
		{
			this.check = check;
			this.config = config;
		}
		public void start() {
			check.form.addLogText("フォロー中の放送のタイムシフトの自動予約を開始します");
			
			while (isRetry) {
				try {
					util.debugWriteLine("auto reserve check ");
					
					if (DateTime.Now - lastGetTime > TimeSpan.FromHours(1)) {
						checkTimeline();
						lastGetTime = DateTime.Now;
					}
					Thread.Sleep(3000);
				} catch (Exception e) {
					util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				}
			}
			check.form.addLogText("タイムシフトの自動予約を終了します");
		}
		private void checkTimeline() {
			util.debugWriteLine("auto reserve checktimeline");
			
			
			var recentUpdateDt = DateTime.MinValue;
			var aiList = check.form.alartListDataSource.ToList();
			aiList.AddRange(check.form.userAlartListDataSource.ToList());
			
			
			//var addUrlBuf = getAddUrlList(ref recentUpdateDt);
			//var addUrlBuf = getAddUrlList2(ref recentUpdateDt);
			var addUrlBuf = getAddUrlList3(ref recentUpdateDt);
			
			foreach (var lvid in addUrlBuf) {
				var pageUrl = "https://live.nicovideo.jp/watch/" + lvid;
				var hig = new HosoInfoGetter();
				var higGetOk = hig.get(pageUrl, check.container); 
				if ((higGetOk && hig.openDt > DateTime.Now) || !higGetOk) {
					RssItem ri = null; 
					if (!isReserveAiLive(lvid, higGetOk ? hig : null, aiList, out ri)) continue;
					
					var r = new Reservation(check.container, lvid, config);
					var ret = r.live2Reserve(bool.Parse(config.get("IsOverwriteOldReserve")));
					if (r.delLog != null) check.form.addLogText(r.delLog + "のタイムシフト予約を削除しました");
					
					var hostName = higGetOk ? hig.userName : " ";
					if (ret == "ok") {
						check.form.addLogText(lvid + " " + hostName + "(" + hig.title + ")のタイムシフトを予約しました");
						var hi = new HistoryInfo(ri, check.form);
						check.form.addReserveHistoryList(hi);
					} else {
						if (ret != "既に予約済みです。")
							check.form.addLogText(lvid + " " + hostName + "(" + hig.title + ")のタイムシフトの予約に失敗しました " + ret);
					}
				}
			}
			if (recentUpdateDt > lastCheckedItemTime) lastCheckedItemTime = recentUpdateDt;
		}
		/*
		List<string> getAddUrlList(ref DateTime recentUpdateDt) {
			var addUrlBuf = new List<string>();
			string min = null;
			//var recentUpdateDt = DateTime.MinValue;
			var url = "https://public.api.nicovideo.jp/v1/timelines/nicolive/last-1-month/my/android/entries.json";
			for (var i = 0; i < 10; i++) {
				try {
					util.debugWriteLine("autoreserve i " + i);
					var _url = url + (min == null ? "" : ("?untilId=" + min));
					var res = util.getPageSource(_url, check.container);
					if (res == null) break;
					
					var timelineObj = Newtonsoft.Json.JsonConvert.DeserializeObject<ActivityTimeline>(res);
					if (timelineObj.meta.status != "200") {
						util.debugWriteLine("auto reserve check timeline not status 200 " + res);
						break;
					}
					if (!string.IsNullOrEmpty(timelineObj.meta.minId))
					    min = timelineObj.meta.minId;
					foreach (var o in timelineObj.data) {
						
						if (o.updated > recentUpdateDt) recentUpdateDt = o.updated;
						if (o.updated <= lastCheckedItemTime) {
							i = 5;
							continue;
						}
						
						if (o.title.EndsWith("予約しました")) {
							util.debugWriteLine("autoreserve data item opentime " + o.updated + " " + o.title);
							var lvid = util.getRegGroup(o.@object.url, "(lv\\d+)");
							if (lvid == null) continue;
							
							addUrlBuf.Add(lvid);
						}
					}
							
							
				} catch (Exception e) {
					util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				}
			}
			return addUrlBuf;
		}
		*/
		List<string> getAddUrlList2(ref DateTime recentUpdateDt) {
			var addUrlBuf = new List<string>();
			//string min = null;
			//var recentUpdateDt = DateTime.MinValue;
			var url = "https://api.repoline.nicovideo.jp/v1/timelines/nicorepo/last-1-month/my/pc/entries.json";
			for (var i = 0; i < 1; i++) {
				try {
					util.debugWriteLine("autoreserve i " + i);
					var h = util.getHeader(check.container, "https://www.nicovideo.jp/", url);
					h.Remove("Accept");
					h.Add("x-frontend-id", "6");
					h.Add("x-frontend-version", "0");
					h.Add("Origin", "https://www.nicovideo.jp");
					var r = util.sendRequest(url, h, null, "GET", false, check.container);
					if (r == null) return addUrlBuf;
					using (var _r = r.GetResponseStream())
					using (var sr = new StreamReader(_r)) {
						var res = sr.ReadToEnd();
						var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<entries>(res);
						foreach (var d in obj.data) {
							if (!d.title.EndsWith("生放送予定です")) continue;
							if (d.updated > recentUpdateDt) recentUpdateDt = d.updated;
							
							util.debugWriteLine("autoreserve data item opentime " + d.updated + " " + d.title);
							var lvid = util.getRegGroup(d.@object.url, "(lv\\d+)");
							if (lvid == null) continue;
							addUrlBuf.Add(lvid);
						}
					}
					/*
					var res = util.getPageSource(url, check.container);
					if (res == null) break;
					
					var timelineObj = Newtonsoft.Json.JsonConvert.DeserializeObject<ActivityTimeline>(res);
					if (timelineObj.meta.status != "200") {
						util.debugWriteLine("auto reserve check timeline not status 200 " + res);
						break;
					}
					if (!string.IsNullOrEmpty(timelineObj.meta.minId))
					    min = timelineObj.meta.minId;
					foreach (var o in timelineObj.data) {
						
						if (o.updated > recentUpdateDt) recentUpdateDt = o.updated;
						if (o.updated <= lastCheckedItemTime) {
							i = 5;
							continue;
						}
						
						if (o.title.EndsWith("予約しました")) {
							util.debugWriteLine("autoreserve data item opentime " + o.updated + " " + o.title);
							var lvid = util.getRegGroup(o.@object.url, "(lv\\d+)");
							if (lvid == null) continue;
							
							addUrlBuf.Add(lvid);
						}
					}
					*/
							
				} catch (Exception e) {
					util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				}
			}
			return addUrlBuf;
		}
		List<string> getAddUrlList3(ref DateTime recentUpdateDt) {
			var addUrlBuf = new List<string>();
			var baseUrl = "https://api.feed.nicovideo.jp/v1/activities/followings/live?";
			var cursor = "";
			for (var i = 0; i < 5; i++) {
				var url = baseUrl + (i == 0 ? "" : ("cursor=" + cursor + "&")) + "context=my_timeline";
				try {
					util.debugWriteLine("autoreserve i " + i);
					var h = util.getHeader(check.container, "https://www.nicovideo.jp/", url);
					h["Accept"] = "application/json";
					h.Add("x-frontend-id", "6");
					h.Add("x-frontend-version", "0");
					h.Add("Origin", "https://www.nicovideo.jp");
					var r = util.sendRequest(url, h, null, "GET", false, check.container);
					if (r == null) return addUrlBuf;
					using (var _r = r.GetResponseStream())
					using (var sr = new StreamReader(_r)) {
						var res = sr.ReadToEnd();
						var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<activitiesC>(res);
						if (obj.activities == null) break;
						cursor = obj.nextCursor;
						foreach (var d in obj.activities) {
							if (d.kind.IndexOf("reserve") == -1) continue;
							if (d.createdAt > recentUpdateDt) recentUpdateDt = d.createdAt;
							
							util.debugWriteLine("autoreserve data item opentime " + d.createdAt + " " + d.content.title);
							var lvid = d.content.id;
							if (lvid == null) continue;
							addUrlBuf.Add(lvid);
						}
					}
				} catch (Exception e) {
					util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				}
			}
			return addUrlBuf;
		}
		private bool isReserveAiLive(string lvid, HosoInfoGetter hig, List<AlartInfo> aiList, out RssItem ri) {
			string desc = "", comName = "", comId = "", hostName = "", 
				isMemberOnly = "false", title = "";
			bool isPayment = false;
			if (hig != null) {
				desc = hig.description;
				comName = hig.group;
				comId = hig.communityId;
				hostName = hig.userName;
				isMemberOnly = hig.isMemberOnly.ToString();
				isPayment = hig.isPayment;
				title = hig.title;
			}
			ri = new RssItem(title, lvid, hig.openDt.ToString(), desc, comName, comId, hostName, "", isMemberOnly, "", isPayment);
			if (hig != null) {
				ri.setUserId(hig.userId);
				ri.setTag(hig.tags);
				ri.category = hig.category;
				ri.type = hig.type;
			}
			foreach (var ai in aiList) {
				if (!ai.isAutoReserve) continue;
				
				var isSuccessAccess = true;
				var isAlart = check.isAlartItem(ri, ai, out isSuccessAccess);
				if (isAlart || !isSuccessAccess) return true;
			}
			return false;
		}
		public void stop() {
			isRetry = false;
		}
	}
	public class entries {
		public List<entriesData> data = null;
	}
	public class entriesData {
		public string id;
		public DateTime updated;
		public string title;
		public entriesDataObject @object; 
	}
	public class entriesDataObject {
		public string type;
		public string url;
	}
	public class activitiesC {
		public List<activity> activities = null;
		public string code = null;
		public string impressionId = null;
		public string nextCursor = null;
	}
	public class activity {
		public bool sensitive;
		public messageC message;
		public class messageC {
			public string text;
		}
		public string thumbnailUrl;
		public messageC label;
		public contentC content;
		public string kind;
		public DateTime createdAt;
		public class contentC {
			public string type;
			public string id;
			public string title;
			public string url;
			public DateTime startedAt;
			public class program {
				public string statusCode;
				public string providerType;
			}
		}
	}
}
