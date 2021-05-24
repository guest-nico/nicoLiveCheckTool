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
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel.Design;
using System.Threading;
using System.Net;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
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
							if (check.checkedLvIdList.Find(x => x.lvId == l.lvId) == null) {
								if (string.IsNullOrEmpty(l.description)) {
									l = setHosoInfo(l);
									if (l == null) continue;
								}
								check.foundLive(new List<RssItem>{l});
							}
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
				
				addLiveListDay(url + DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), openTimeList);
				addLiveListDay(url + DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd"), openTimeList);
				addLiveListDay(url + DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd"), openTimeList);
				addLiveListDay(url + DateTime.Now.AddDays(-4).ToString("yyyy-MM-dd"), openTimeList);
				addLiveListDay(url + DateTime.Now.AddDays(-5).ToString("yyyy-MM-dd"), openTimeList);
				
				timeTableList.Sort((RssItem x,RssItem y) => string.Compare(x.pubDate, y.pubDate));
				
				var onairCount = timeTableList.Count((a) => a.pubDateDt < DateTime.Now);
				
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
				var isAutoReserve = bool.Parse(check.form.config.get("IsAutoReserve"));
				var aiList = check.form.alartListDataSource.ToList();
				TimeLineInfo[] list = null;
				string res = null;
				var isNew = true;
				if (isNew) {
					url = "https://live.nicovideo.jp/timetable?date=" + url.Substring(url.IndexOf("=") + 1).Replace("-", "");
					res = util.getPageSource(url);
					if (res == null) {
						util.debugWriteLine("timetable zerotimeline res null");
						check.form.addLogText("公式番組表からデータが取得できませんでした2 " + url);
						return;
					}
					list = getNewTimeLineList(res);
				} else {
					res = util.getPageSource(url);
					if (res == null) {
						util.debugWriteLine("timetable zerotimeline res null");
						//check.form.addLogText("公式番組表からデータが取得できませんでした " + url);
						return;
					}
					
					list = JsonConvert.DeserializeObject<timelineTop>(res).timeline.stream_list;
				}
				
				foreach (var l in list) {
					if (l.provider_type != "official") continue;
					
					l.startTime = DateTime.Parse(l.start_date + " " + l.start_time);
					if ((l.status != "onair" && l.startTime < DateTime.Now.AddHours(-2))) continue;
					
					//if ((l.status == "onair" || l.startTime > DateTime.Now.AddHours(-2))) {
						 var listRi = timeTableList.Find(n => n.lvId == "lv" + l.id);
						//if (((isAllCheck && l.status == "onair") || l.startTime > DateTime.Now.AddHours(-2)) && liveList.Find(n => n.lvId == l.id) == null) {
						
						RssItem ri = null;
						var opentimeItem = openTimeList.Find(x => x.id == "lv" + l.id);
						if (opentimeItem != null) {
							
							ri = new RssItem(l.title, "lv" + l.id, opentimeItem.showTime.beginAt.ToString("yyyy/MM/dd HH:mm:ss"),
									opentimeItem.description, 
									opentimeItem.contentOwner.name, 
									opentimeItem.socialGroupId,
									"", 
									opentimeItem.thumbnailUrl, opentimeItem.isMemberOnly.ToString(), "", l.isPayment);
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
							ri = new RssItem(l.title, "lv" + l.id, hig.dt.ToString("yyyy/MM/dd HH:mm:ss"),
									hig.description, 
									util.getCommunityName(hig.communityId, out _isFollow, null), 
									hig.communityId,
									"", 
									hig.thumbnail, "false", "", hig.isPayment);
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
						
						if (isAutoReserve) {
							foreach (var ai in aiList) {
								if (ai.isAutoReserve && ri.pubDateDt > DateTime.Now) {
									var isSuccessAccess = true;
									var isAlart = check.isAlartItem(ri, ai, out isSuccessAccess);
									if (isAlart || !isSuccessAccess) {
										var ret = new Reservation(check.container, ri.lvId, config).live2Reserve(bool.Parse(config.get("IsOverwriteOldReserve")));
										if (ret == "ok") {
											check.form.addLogText(ri.lvId + " " + ri.comName + (string.IsNullOrEmpty(ri.comName) ? (ri.title) : (ri.comName + "(" + ri.title + ")")) + "のタイムシフトを予約しました");
										} else {
											if (ret != "既に予約済みです。")
												check.form.addLogText(ri.lvId + "(" + ri.comName + ")のタイムシフトの予約に失敗しました " + ret);
										}
									}
								}
							}
						}
					//}
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
				var ri = new RssItem(hig.title, lvid, hig.dt.ToString("yyyy/MM/dd HH:mm:ss"),
						hig.description, 
						util.getCommunityName(hig.communityId, out _isFollow, null), 
						hig.communityId,
						"", 
						hig.thumbnail, "false", "", hig.isPayment);
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
		private TimeLineInfo[] getNewTimeLineList(string res) {
			var m = new Regex("(<tr[\\s\\S]+?</tr>)").Matches(res);
			var ret = new List<TimeLineInfo>();
			foreach (Match _m in m) {
				try {
					var mValue = _m.Value.Replace("\n", "").Replace("\r", "");
					
					var lv = util.getRegGroup(mValue, "id=\"stream_lv(\\d+)");
					//var title = util.getRegGroup(mValue, "<h2 class=\"item_title\">[\\s\\S]+?<a[\\s\\S]+?>([\\s\\S]*?<span id=\"play_arrow[\\s\\S]*?</span>)([\\s\\S]*?)</a>", 2);
					var title = getInnerText(util.getRegGroup(mValue, "<h2 class=\"item_title\">([\\s\\S]*?)</h2>"));
					var description = getInnerText(util.getRegGroup(mValue, "<p class=\"item_description\">([\\s\\S]*?)</p"));
					var isChannel = mValue.IndexOf("<li class=\"timetablePage-ProgramList_TitleIcon-channel\">") > -1;
					var isOfficial = mValue.IndexOf("<li class=\"timetablePage-ProgramList_TitleIcon-official\">") > -1;
					var provider_type = isOfficial ? "official" : (isChannel ? "channel" : "community");
					var thumbnail_url = util.getRegGroup(mValue, "<div class=\"item_thumb\">[\\s\\S]*?src=\"(.+?)\"");
					
					var start_date = getInnerText(util.getRegGroup(mValue, "(<span class=\"start_date\"[\\s\\S]*?</span>)"));
					var end_date = getInnerText(util.getRegGroup(mValue, "(<span id=\"end_date[\\s\\S]*?</span>)"));
					var start_time = util.getRegGroup(mValue, "<span class=\"start_time\">(.*?)</span>");
					var end_time = util.getRegGroup(mValue, "<span id=\"end_date[\\s\\S]*?(\\d+:\\d+)</span>");
					if (string.IsNullOrEmpty(start_date)) {
						util.debugWriteLine("start time no " + lv);
						check.form.addLogText("start time no lv" + lv);
						continue;
					}
					var total_time = (DateTime.Parse(end_date + " " + end_time) - DateTime.Parse(start_date + " " + start_time)).ToString();
					var isTsEnd = mValue.IndexOf("class=\"item ts_end\">") > -1;
					var isTs = mValue.IndexOf("class=\"item play\">") > -1;
					var isOnAir = mValue.IndexOf(" play onair\">") > -1;
					var status = isOnAir ? "onair" : (isTs ? "timeshift" : "tsend");
					if (lv == null || title == null || description == null || provider_type == null ||
							thumbnail_url == null || start_date == null || start_time == null ||
							end_date == null || end_time == null || total_time == null ||
							status == null) {
						util.debugWriteLine("timetable timelinePage null " + lv + " " + title + " " + description + " " +
								provider_type + " " + thumbnail_url + " " + start_date +
								" " + start_time + " " + end_date + " " + end_time + " " + 
								total_time + " " + status);
						#if DEBUG
							check.form.addLogText(lv + " title " + title + " desc " + description + " type " +
								provider_type + " thumb " + thumbnail_url + " startdt " + start_date +
								" starttime " + start_time + " end dt " + end_date + " endtime " + end_time + " totaltime " + 
								total_time + " status " + status);
						#endif
						continue;
					}
					var isPayment = mValue.IndexOf("\"timetablePage-ProgramList_TitleIcon-pay\">") > -1;
					ret.Add(new TimeLineInfo(lv, title, description, provider_type, thumbnail_url, start_date, end_date, start_time, end_time, total_time, status, isPayment));
				} catch (Exception e) {
					util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
					util.debugWriteLine(_m.Value);
					#if DEBUG
						check.form.addLogText("timetable timelinePage ParseEror " + _m.Value);
					#endif
				}
			}
			return ret.ToArray();
		}
		private string getInnerText(string t) {
			if (t == null) return null;
			var r = new Regex("<.+?>").Replace(t, "");
			if (r == null) return null;
			return r.Trim();
		}
	}
	
	class timelineTop {
		public timelineSemi timeline = null;
	}
	class timelineSemi {
		public string date = null;
		public TimeLineInfo[] stream_list = new TimeLineInfo[0];
	}
	public class TimeLineInfo {
		public TimeLineInfo(string id, string title, string description,
				string provider_type, string thumbnail_url, string start_date,
				string end_date, string start_time, string end_time,
				string total_time, string status, bool isPayment) {
			this.id = id;
			this.title = title;
			this.description = description; 
			this.provider_type = provider_type;
			this.thumbnail_url = thumbnail_url;
			this.start_date = start_date;
			this.end_date = end_date;
			this.start_time = start_time;
			this.end_time = end_time;
			this.total_time = total_time;
			this.status = status;
			this.isPayment = isPayment;
		}
		public string id = null;
		public string title = null;
		public string description = null;
		public string provider_type = null;
		public string thumbnail_url = null;
		public string start_date = null;
		public string end_date = null;
		public string start_time = null;
		public string end_time = null;
		public string total_time = null;
		public string status = null;
		public bool isPayment = false;
		
		public DateTime startTime = DateTime.MinValue;
	}
}
