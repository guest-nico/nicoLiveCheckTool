/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2019/09/14
 * Time: 20:57
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Threading;
using Newtonsoft.Json;

using namaichi.info;
using namaichi.rec;

namespace namaichi.alart
{
	/// <summary>
	/// Description of TimeTableChecker.
	/// </summary>
	public class TimeTableChecker
	{
		Check check;
		config.config config;
		private bool isRetry = true;
		private List<RssItem> timeTableList = new List<RssItem>();
		private DateTime lastGetTime = DateTime.MinValue;
		private bool isAllCheck = false;
		
		public TimeTableChecker(Check check, config.config config)
		{
			isAllCheck = bool.Parse(config.get("IsStartTimeAllCheck"));
			this.check = check;
			this.config = config;
		}
		public void start() {
			check.form.addLogText("番組表からの取得を開始します");
			
			while (isRetry) {
				try {
					util.debugWriteLine("timeline check liveList count " + timeTableList.Count);
					
					if (DateTime.Now - lastGetTime > TimeSpan.FromHours(3)) {
						setLiveList();
						lastGetTime = DateTime.Now;
					}
					
					var alartedItem = new List<RssItem>();
					foreach (var l in timeTableList) {
						if (DateTime.Now.AddSeconds(10) > l.pubDateDt) {
							util.debugWriteLine("timeline alart " + l.lvId + " " + l.title);
							check.foundLive(new List<RssItem>{l});
							alartedItem.Add(l);
						}
					}
					foreach (var l in alartedItem)
						timeTableList.Remove(l);
					isAllCheck = false;
					Thread.Sleep(3000);
				} catch (Exception e) {
					util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				}
			}
			check.form.addLogText("番組表からの取得を終了します");
		}
		private void setLiveList() {
			try {
				var url = "https://live.nicovideo.jp/api/getZeroTimeline?date=";//2019-09-15";
				addLiveListDay(url + DateTime.Now.ToString("yyyy-MM-dd"));
				addLiveListDay(url + DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"));
				timeTableList.Sort((RssItem x,RssItem y) => string.Compare(x.pubDate, y.pubDate));
				
				if (!isAllCheck) {
					var delList = new List<RssItem>();
					foreach (var l in timeTableList)
						if (DateTime.Now > l.pubDateDt) delList.Add(l);
					foreach (var l in delList)
						timeTableList.Remove(l);
				}
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
		}
		private void addLiveListDay(string url) {
			try {
				var res = util.getPageSource(url);
				if (res == null) return;
				var list = JsonConvert.DeserializeObject<timelineTop>(res);
				foreach (var l in list.timeline.stream_list) {
					if (l.provider_type != "official") continue;
					
					l.startTime = DateTime.Parse(l.start_date + " " + l.start_time);
					if ((l.status == "onair" || l.startTime > DateTime.Now.AddHours(-2))) {
						 var listRi = timeTableList.Find(n => n.lvId == "lv" + l.id);
					//if (((isAllCheck && l.status == "onair") || l.startTime > DateTime.Now.AddHours(-2)) && liveList.Find(n => n.lvId == l.id) == null) {
						
						var hig = new HosoInfoGetter();
						for (var i = 0; i < 10; i++) {
							#if DEBUG
								if (i > 0) check.form.addLogText("timetable higget false " + l.id + " " + i);
							#endif
							var r = hig.get("https://live.nicovideo.jp/watch/lv" + l.id);
							if (r) break;
							Thread.Sleep(2000);
						}
						if (hig.openDt == DateTime.MinValue) continue;
						
						var _isFollow = false; 
						var ri = new RssItem(l.title, "lv" + l.id, hig.dt.ToString(),
								hig.description, 
								util.getCommunityName(hig.communityId, out _isFollow, null), 
								hig.communityId,
								"", 
								hig.thumbnail, "false", "");
						ri.type = hig.type;
						ri.tags = hig.tags;
						ri.pubDateDt = hig.openDt;
						if (!string.IsNullOrEmpty(hig.userName)) ri.hostName = hig.userName;
						if (hig.openDt != hig.dt) util.debugWriteLine("hig open start tigau " + hig.openDt + " " + hig.dt);
						else util.debugWriteLine("hig open start onaji " + hig.openDt + " " + hig.dt);
						if (listRi == null) timeTableList.Add(ri);
						else {
							#if DEBUG
								if (listRi.pubDateDt != ri.pubDateDt) {
									check.form.addLogText("timetable pubDateDt change " + DateTime.Now + " " + listRi.lvId + " " + listRi.pubDateDt + " " + ri.pubDateDt);
									util.debugWriteLine("timetable pubDateDt change " + listRi.lvId + " " + listRi.pubDateDt + " " + ri.pubDateDt + " " + res);
								}
							#endif                      
							listRi.pubDateDt = ri.pubDateDt;
						}
						
					}
				}
				                                         
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
		}
		public void stop() {
			isRetry = false;
		}
	}
	class timelineTop {
		public timelineSemi timeline;
	}
	class timelineSemi {
		public string date;
		public TimeLineInfo[] stream_list = new TimeLineInfo[0];
	}
	class TimeLineInfo {
		public string id;
		public string title;
		public string description;
		public string provider_type;
		public string thumbnail_url;
		public string start_date;
		public string end_date;
		public string start_time;
		public string end_time;
		public string total_time;
		public string status;
		
		public DateTime startTime = DateTime.MinValue;
	}
}
