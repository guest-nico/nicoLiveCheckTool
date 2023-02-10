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
					
					if (DateTime.Now - lastGetTime > TimeSpan.FromMinutes(10)) {
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
			var url = "https://public.api.nicovideo.jp/v1/timelines/nicolive/last-1-month/my/android/entries.json";
			string min = null;
			var recentUpdateDt = DateTime.MinValue;
			var aiList = check.form.alartListDataSource.ToList();
			aiList.AddRange(check.form.userAlartListDataSource.ToList());
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
							var pageUrl = "https://live.nicovideo.jp/watch/" + lvid;
							var hig = new HosoInfoGetter();
							var higGetOk = hig.get(pageUrl, check.container); 
							if ((higGetOk && hig.openDt > DateTime.Now) || !higGetOk) {
								RssItem ri = null; 
								if (!isReserveAiLive(o, lvid, higGetOk ? hig : null, aiList, out ri)) continue;
								
								var ret = new Reservation(check.container, lvid, config).live2Reserve(bool.Parse(config.get("IsOverwriteOldReserve")));
								if (ret == "ok") {
									check.form.addLogText(lvid + " " + o.actor.name + "(" + hig.title + ")のタイムシフトを予約しました");
									var hi = new HistoryInfo(ri, check.form);
									check.form.addReserveHistoryList(hi);
								} else {
									if (ret != "既に予約済みです。")
										check.form.addLogText(lvid + " " + o.actor.name + "(" + hig.title + ")のタイムシフトの予約に失敗しました " + ret);
								}
							}
						} 
					}
				} catch (Exception e) {
					util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				}
			}
			if (recentUpdateDt > lastCheckedItemTime) lastCheckedItemTime = recentUpdateDt;
		}
		private bool isReserveAiLive(TimelineItem o, string lvid, HosoInfoGetter hig, List<AlartInfo> aiList, out RssItem ri) {
			string desc = "", comName = "", comId = "", hostName = "", 
				isMemberOnly = "false";
			bool isPayment = false;
			if (hig != null) {
				desc = hig.description;
				comName = hig.group;
				comId = hig.communityId;
				hostName = o.actor.name;
				isMemberOnly = hig.isMemberOnly.ToString();
				isPayment = hig.isPayment;
			}
			ri = new RssItem(o.@object.name, lvid, hig.openDt.ToString(), desc, comName, comId, hostName, "", isMemberOnly, "", isPayment);
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
}
