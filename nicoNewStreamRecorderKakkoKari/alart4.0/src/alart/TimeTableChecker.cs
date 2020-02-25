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
using System.Net;
using System.Xml.Linq;
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
					var alartedItem2 = new List<RssItem>();
					foreach (var _l in timeTableList) {
						var l = _l;
						if (DateTime.Now.AddSeconds(10) > l.pubDateDt) {
							util.debugWriteLine("timeline alart " + l.lvId + " " + l.title);
							if (string.IsNullOrEmpty(l.description)) {
								l = setHosoInfo(l);
								if (l == null) continue;
							}
							check.foundLive(new List<RssItem>{l});
							alartedItem.Add(l);
							alartedItem2.Add(_l);
						}
					}
					foreach (var l in alartedItem) {
						util.debugWriteLine("timeline alarted item remove index " + l.lvId + " " + timeTableList.IndexOf(l));
						var r = timeTableList.Remove(l);
						util.debugWriteLine("timeline alarted item remove " + l.lvId + " " + r);
					}
					foreach (var l in alartedItem2) {
						util.debugWriteLine("timeline2 alarted item remove index " + l.lvId + " " + timeTableList.IndexOf(l));
						var r = timeTableList.Remove(l);
						util.debugWriteLine("timeline2 alarted item remove " + l.lvId + " " + r);
					}
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
				var openTimeList = getCasOpenTimeList();
				var url = "https://live.nicovideo.jp/api/getZeroTimeline?date=";//2019-09-15";
				addLiveListDay(url + DateTime.Now.ToString("yyyy-MM-dd"), openTimeList);
				addLiveListDay(url + DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"), openTimeList);
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
		private void addLiveListDay(string url, List<TanzakuItem> openTimeList) {
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
						
						RssItem ri = null;
						var opentimeItem = openTimeList.Find(x => x.id == "lv" + l.id);
						if (opentimeItem != null) {
							
							ri = new RssItem(l.title, "lv" + l.id, opentimeItem.showTime.beginAt.ToString(),
									opentimeItem.description, 
									opentimeItem.contentOwner.name, 
									opentimeItem.socialGroupId,
									"", 
									opentimeItem.thumbnailUrl, opentimeItem.isMemberOnly.ToString(), "");
							ri.type = "official";
							ri.tags = new string[]{""};
							ri.pubDateDt = opentimeItem.onAirTime.beginAt;
							//if (!string.IsNullOrEmpty(hig.userName)) ri.hostName = hig.userName;
							//if (hig.openDt != hig.dt) util.debugWriteLine("hig open start tigau " + hig.openDt + " " + hig.dt);
							//else util.debugWriteLine("hig open start onaji " + hig.openDt + " " + hig.dt);
							
							
						} else {
							
							var hig = new HosoInfoGetter();
							for (var i = 0; i < 2; i++) {
								#if DEBUG
									if (i > 0) check.form.addLogText("timetable higget false " + l.id + " " + i);
								#endif
								var r = hig.get("https://live.nicovideo.jp/watch/lv" + l.id, check.container);
								if (r) break;
								Thread.Sleep(2000);
							}
							var _isFollow = false; 
							ri = new RssItem(l.title, "lv" + l.id, hig.dt.ToString(),
									hig.description, 
									util.getCommunityName(hig.communityId, out _isFollow, null), 
									hig.communityId,
									"", 
									hig.thumbnail, "false", "");
							ri.type = hig.type;
							ri.tags = hig.tags;
							ri.pubDateDt = hig.openDt;
							if (!string.IsNullOrEmpty(hig.userName)) ri.hostName = hig.userName;
							if (hig.openDt != hig.dt) util.debugWriteLine("hig open start tigau " + hig.openDt + " " + hig.dt + " " + ri.lvId);
							else util.debugWriteLine("hig open start onaji " + hig.openDt + " " + hig.dt + " " + ri.lvId);
						
						}
						
						/*
						if (hig.openDt == DateTime.MinValue) {
							if (ceCommingSoonRes == null) {
								ceCommingSoonRes = util.getPageSource("http://api.ce.nicovideo.jp/liveapi/v1/video.comingsoon?__format=xml&from=0&limit=148&pt=official");
								//ceCommingSoonRes = util.getPageSource("http://api.ce.nicovideo.jp/liveapi/v1/video.onairlist?__format=xml&from=0&limit=148&pt=official");
								if (ceCommingSoonRes == null) continue;
								ceCommingSoonRes = WebUtility.HtmlDecode(ceCommingSoonRes);
							}
							hig = new HosoInfoGetter();
							var openDt = getOpenDt(l.id, ceCommingSoonRes);
							if (l.startTime < DateTime.Now || openDt == DateTime.MinValue)
								hig.openDt = l.startTime;
							else hig.openDt = openDt;
						}
						*/
						
						
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
		private DateTime getOpenDt(string id, string res) {
			
			if (res == null) return DateTime.MinValue;
			var ind = res.IndexOf("<id>lv" + id + "</id>");
			if (ind == -1) return DateTime.MinValue;
			var ot = util.getRegGroup(res.Substring(ind), "open_time>(.+?)</open_time");
			util.debugWriteLine("timetable getOpenDt " + id + " " + ot);
			var ret = DateTime.Parse(ot);
			return ret;
			
		}
		private RssItem setHosoInfo(RssItem item) {
			for (var i = 0; i < 10; i++) {
				var hig = new HosoInfoGetter();
				var r = hig.get("https://live.nicovideo.jp/watch/" + item.lvId, check.container);
				if (!r) {
					Thread.Sleep(3000);
					continue;
				}
				
				var _isFollow = false;
				var lvid = item.lvId.StartsWith("lv") ? item.lvId : ("lv" + item.lvId);
				var ri = new RssItem(hig.title, lvid, hig.dt.ToString(),
						hig.description, 
						util.getCommunityName(hig.communityId, out _isFollow, null), 
						hig.communityId,
						"", 
						hig.thumbnail, "false", "");
				ri.type = hig.type;
				ri.tags = hig.tags;
				ri.pubDateDt = hig.openDt;
				if (!string.IsNullOrEmpty(hig.userName)) ri.hostName = hig.userName;
				return ri;
			}
			return null;
		}
		public void stop() {
			isRetry = false;
		}
		private List<TanzakuItem> getCasOpenTimeList() {
			var ret = new List<TanzakuItem>();
			var endTime = DateTime.Parse(DateTime.Now.AddDays(2).ToShortDateString());
			try {
				for (var i = 10; i < 200; i += 10) {
					var url = "https://api.cas.nicovideo.jp/v2/tanzakus/topic/live/content-groups/reserved/items?cursor=" + i + "/cursorEnd/" + (i - 10) + "";
					var res = util.getPageSource(url);
					if (res == null) break;
					
					var tanzakuObj = Newtonsoft.Json.JsonConvert.DeserializeObject<TanzakuOnAir>(res);
					if (tanzakuObj.meta.status != "200") break;
					
					var isEnd = false;
					foreach (var o in tanzakuObj.data.items) {
						util.debugWriteLine("getCasOpentime " + o.showTime.beginAt);
						if (o.showTime.beginAt > endTime || !o.isChannelRelatedOfficial) {
							isEnd = true;
							break;
						} else ret.Add(o); 
					}
					if (tanzakuObj.data.cursor.IndexOf("cursorEnd/cursorEnd") > -1) isEnd = true;
					if (isEnd) break;
				}
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
			util.debugWriteLine("getCasOpenTime onair " + ret.Count);
			try {
				for (var i = 10; i < 200; i += 10) {
					var url = "https://api.cas.nicovideo.jp/v2/tanzakus/topic/live/content-groups/onair/items?cursor=" + i + "/cursorEnd/" + (i - 10) + "";
					var res = util.getPageSource(url);
					if (res == null) break;
					
					var tanzakuObj = Newtonsoft.Json.JsonConvert.DeserializeObject<TanzakuOnAir>(res);
					if (tanzakuObj.meta.status != "200") break;
					
					var isEnd = false;
					foreach (var o in tanzakuObj.data.items) {
						util.debugWriteLine("getCasOpentime " + o.showTime.beginAt);
						if (!o.isChannelRelatedOfficial) {
							isEnd = true;
							break;
						} else 
							ret.Add(o);
					}
					if (tanzakuObj.data.cursor.IndexOf("cursorEnd/cursorEnd") > -1) isEnd = true;
					if (isEnd) break;
				}
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
			
			util.debugWriteLine("load cas opentimelist item " + ret.Count);
			return ret;
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
