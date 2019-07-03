/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2019/02/08
 * Time: 6:12
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Threading;
using System.Text.RegularExpressions;
using namaichi.info;

namespace namaichi.alart
{
	/// <summary>
	/// Description of RssCheck.
	/// </summary>
	public class RssCheck
	{
		private string lastLv = null;
		public bool isStartTimeAllCheck = false;
		
		Check check;
		config.config config;
		private bool isRetry = true;
		
		private Regex rssReg = new Regex("<item>[\\s\\S]*?<title>([\\s\\S]*?)</title>[\\s\\S]*?<guid [\\s\\S]*?(lv\\d*)</guid>[\\s\\S]*?<pubDate>([\\s\\S]*?)</pubDate>[\\s\\S]*?<description>([\\s\\S]*?)</description>[\\s\\S]*?<category>([\\s\\S]*?)</category>[\\s\\S]*?<media:thumbnail url=\"(.+?)\"[\\s\\S]*?<nicolive:community_name>([\\s\\S]*?)</nicolive:community_name>[\\s\\S]*?<nicolive:community_id>([\\s\\S]*?)</nicolive:community_id>[\\s\\S]*?<nicolive:member_only>(.*?)</nicolive:member_only>[\\s\\S]*?<nicolive:type>([\\s\\S]*?)</nicolive:type>[\\s\\S]*?<nicolive:owner_name>([\\s\\S]*?)</nicolive:owner_name>");
		
		
		public RssCheck(Check check, config.config config)
		{
			isStartTimeAllCheck = bool.Parse(config.get("IsStartTimeAllCheck"));
			this.check = check;
			this.config = config;
		}
		public void start() {
			check.form.addLogText("RSSからの取得を開始します");
			var url = "https://live.nicovideo.jp/recent/rss?tab=&p=";
			while (isRetry) {
				util.debugWriteLine("rss check lastlv" + lastLv + " " + DateTime.Now);
				util.debugWriteLine("checked lv list start count " + check.checkedLvIdList.Count);
				var items = new List<RssItem>();
				var end = 1000 ;
				for (var i = 1; i < 100 && i < end; i++) {
					util.debugWriteLine("rss page i " + i);
					var res = util.getPageSource(url + i.ToString(), null);
					if (res == null) {
						Thread.Sleep(3000);
						break;
					}
					var isEndFile = false;
					if (!getRssItems(res, ref items, ref isEndFile)) {
						if (end == 1000 && 
						    (!isStartTimeAllCheck || lastLv != null)) end = i + 5;
//						break;
					}
					if (isEndFile) break;
					if (lastLv == null && !isStartTimeAllCheck) break;
					
					
				}
				if (items.Count > 0)
					lastLv = items[0].lvId;
				
				
				util.debugWriteLine("checked lv list count " + check.checkedLvIdList.Count);
				util.debugWriteLine("get rss items " + items.Count);
				if (items.Count > -1) {
					foreach (RssItem it in items) util.debugWriteLine(it.lvId + " " + it.comId + " " + it.hostName + " " + it.title);
				}
				
				check.foundLive(items);
				
				if (check.checkedLvIdList.Count > 20000)
					check.deleteOldCheckedLvIdList();
				
				var t = int.Parse(config.get("rssUpdateInterval"));
				if (t < 15) t = 15;
				Thread.Sleep(t * 1000);
			}
			check.form.addLogText("RSSからの取得を終了します");
		}
		private bool getRssItems(string res, ref List<RssItem> items, ref bool isEndFile) {
			var m = rssReg.Matches(res);
			util.debugWriteLine("rss reg matches count " + m.Count);
			
			var ret = true;
			foreach(Match _m in m) {
				var item = new RssItem(_m.Groups[1].Value,
						_m.Groups[2].Value, _m.Groups[3].Value,
						_m.Groups[4].Value, _m.Groups[7].Value,
						_m.Groups[8].Value, _m.Groups[11].Value,
						_m.Groups[6].Value, _m.Groups[9].Value,
						_m.Groups[0].Value);
				var cate = new List<string>();
				cate.AddRange(_m.Groups[5].Value.Split(','));
				item.category = cate;
				item.type = _m.Groups[10].Value;
//				util.debugWriteLine("m check mae " + item.lvId + " " + item.title + " " + item.comId);
//				if (item.lvId == lastLv) return false;
//				if (checkedLvIdList.IndexOf(item.lvId) > -1) return false;
				if (items.IndexOf(item) == -1 && check.checkedLvIdList.IndexOf(item.lvId) == -1) {
					items.Add(item);
					check.checkedLvIdList.Add(item.lvId);
					
				} else {
					
					ret = false;
//					util.debugWriteLine("tuika nasi " + item.lvId + " " + item.title + " " + item.comId);
					
				}
			}
			if (m.Count == 0) isEndFile = true;
			return ret;
		}
		public void stop() {
			isRetry = false;
		}
	}
}
