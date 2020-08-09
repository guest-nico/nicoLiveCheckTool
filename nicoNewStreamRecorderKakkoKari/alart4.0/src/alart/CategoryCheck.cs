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
using System.Linq;
using System.Threading;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using namaichi.info;

namespace namaichi.alart
{
	/// <summary>
	/// Description of RssCheck.
	/// </summary>
	public class CategoryCheck
	{
		//private string[] lastLv = new string[6];
		public bool isStartTimeAllCheck = false;
		
		Check check;
		config.config config;
		private bool isRetry = true;
		
		private string[] urls = new string[7];
		//private string[] categoryStrList = new string[]{"一般(その他)", ""};
		//private static string[] categoryNames = new string[]{"common", "try", "live", "req", "superichiba", "face", "totu"};
		private static string[] categoryNames = new string[]{"req", "live", "try", "common"};
		
		public CategoryCheck(Check check, config.config config)
		{
			isStartTimeAllCheck = bool.Parse(config.get("IsStartTimeAllCheck"));
			
			this.check = check;
			this.config = config;
			
			
			urls = new string[4];
			for (var i = 0; i < categoryNames.Length; i++)
				urls[i] = "https://live.nicovideo.jp/front/api/pages/recent/v1/programs?tab=" + categoryNames[i] + "&offset=#&sortOrder=recentDesc";
		}
		public void start(bool isOnlyStartTimeCheck = false) {
			if (!isOnlyStartTimeCheck)
				check.form.addLogText("カテゴリページからの取得を開始します");
			var isFirst = true;
			while (isRetry) {
				//util.debugWriteLine("category check lastlv" + lastLv + " " + DateTime.Now);
				util.debugWriteLine("checked lv list start count " + check.checkedLvIdList.Count);
				
				try {
					var items = new List<RssItem>();
					items = getFoundLvList(isFirst, items, isStartTimeAllCheck);
					if (isStartTimeAllCheck) {
						Task.Factory.StartNew(() => {
							Thread.Sleep(100000);
							items = getFoundLvList(true, items, true);
							check.foundLive(items);
						});
						
					}

					check.foundLive(items);
					setDescription(items);
					if (isOnlyStartTimeCheck) return;
					
					isStartTimeAllCheck = false;
					isFirst = false;
					
					
					if (check.checkedLvIdList.Count > 20000)
						check.deleteOldCheckedLvIdList();
					
					var _t = int.Parse(config.get("rssUpdateInterval"));
					if (_t < 15) _t = 15;
					Thread.Sleep(_t * 1000);
				} catch (Exception e) {
					util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				}
			}
			check.form.addLogText("カテゴリページからの取得を終了します");
		}
		private List<RssItem> getFoundLvList(bool isFirst, List<RssItem> items, bool _isStartTimeAllCheck) {
			//var items = new List<RssItem>();
			for (var j = 0; j < urls.Length; j++) {
				var url = urls[j];
				
				var end = 1000 ;
				string lastRes = null;
				for (var i = 0; i < 1000 && i < end; i++) {
					util.debugWriteLine("category page i " + i + " " + url);
				
					string res = null;
					for (var k = 0; k < 10; k++) {
						res = util.getPageSource(url.Replace("#", i.ToString()), null);
						if (res == null) {
							Thread.Sleep(5000);
							//break;
							
							continue;
						}
						break;
					}
					
					if (res == null) {
						var t = int.Parse(config.get("rssUpdateInterval"));
						if (t < 15) t = 15;
						Thread.Sleep(t * 1000);
						check.form.addLogText("カテゴリページの取得に失敗しました " + url.Replace("#", i.ToString()));
						break;
					}
					
					if (lastRes == res) 
						break;
					var isEndFile = false;
					var isContainAddedLv = getRssItems(res, ref items, ref isEndFile, categoryNames[j]);
					if (isContainAddedLv) {
						var isContainLv = items.FindIndex(x => check.checkedLvIdList.IndexOf(x.lvId) > -1) != -1;
						if (end == 1000 && 
							    (!_isStartTimeAllCheck || (isContainLv)) && 
							    !_isStartTimeAllCheck) end = i + 1;
//						break;
					}
					lastRes = res;
					if (isEndFile) 
						break;
					if (isFirst && !_isStartTimeAllCheck) 
						break;
					
					if (_isStartTimeAllCheck) {
						check.foundLive(items);
						items = new List<RssItem>();
					}
				}
				//if (items.Count > 0)
				//	lastLv = items[0].lvId;
				
				util.debugWriteLine("checked lv list count " + check.checkedLvIdList.Count);
				util.debugWriteLine("get category items " + items.Count);
				if (items.Count > -1) {
					foreach (RssItem it in items) {
						util.debugWriteLine(it.lvId + " " + it.comId + " " + it.hostName + " " + it.title + " " + it.pubDate);
					}
				}
			}
			util.debugWriteLine("category check lv list count " + items.Count);
			return items;
		}
		public bool getRssItems(string res, ref List<RssItem> items, ref bool isEndFile, string categoryName) {
			var isContainAddedLv = false;
			try {
				var categoryObj = (CategoryRecent)Newtonsoft.Json.JsonConvert.DeserializeObject<CategoryRecent>(res);
				if (categoryObj.meta.errorCode != "OK") return false;
				
				foreach(var d in categoryObj.data) {
					try {
						var item = d.getRssItem(categoryName);
						if (item == null) continue;
						if (items.IndexOf(item) == -1 && check.checkedLvIdList.IndexOf(item.lvId) == -1) {
							items.Add(item);
							check.checkedLvIdList.Add(item.lvId);
						} else {
							isContainAddedLv = true;
							//util.debugWriteLine("tuika nasi " + item.lvId + " " + item.title + " " + item.comId);
						}
					} catch (Exception e) {
						util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
					}
				}
				if (categoryObj.data.Count == 0) isEndFile = true;
				
				return isContainAddedLv;
			
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				return isContainAddedLv;
			}
		}
		private void setDescription(List<RssItem> items) {
			try {
				if (items.Count == 0) return;
				
				var setOkNum = 0;
				var oldestDt = DateTime.MaxValue;
				foreach (var item in items) 
					if (oldestDt > item.pubDateDt) oldestDt = item.pubDateDt;
				for (var i = 10; i < 100; i += 10) {
					var url = "https://api.cas.nicovideo.jp/v2/tanzakus/topic/live/content-groups/onair/items?cursor=" + i + "/cursorEnd/cursorEnd/cursorEnd/" + (i - 10);
					var res = util.getPageSource(url);
					if (res == null) return;
					
					var tanzakuObj = Newtonsoft.Json.JsonConvert.DeserializeObject<TanzakuOnAir>(res);
					if (tanzakuObj.meta.status != "200") return;
					
					foreach (var o in tanzakuObj.data.items) {
						var ri = items.Find(x => x.lvId == o.id);
						if (ri == null) continue;
						if (o.description.Length > 100) 
							o.description = o.description.Substring(0, 100);
						ri.description = o.description;
						
						var li = check.form.liveListDataSource.FirstOrDefault(x => x.lvId == o.id);
						if (li != null) {
							li.description = o.description;
							var liI = check.form.liveListDataSource.IndexOf(li);
							if (liI > -1) check.form.liveList.UpdateCellValue(5, liI);
						} else {
							//util.debugWriteLine("aa");
						}
						li = check.form.liveListDataReserve.Find(x => x.lvId == o.id);
						if (li != null) li.description = o.description;
						var hi = check.form.historyListDataSource.FirstOrDefault(x => x.lvid == o.id);
						if (hi != null) {
							hi.description = o.description;
							var hiI = check.form.historyListDataSource.IndexOf(hi);
							if (hiI > -1) check.form.historyList.UpdateCellValue(9, hiI);
						}
						hi = check.form.notAlartListDataSource.FirstOrDefault(x => x.lvid == o.id);
						if (hi != null) {
							hi.description = o.description;
							var hiI = check.form.notAlartListDataSource.IndexOf(hi);
							if (hiI > -1) check.form.notAlartList.UpdateCellValue(9, hiI);
						}
						
						setOkNum++;
						if (setOkNum >= items.Count) return;
						if (o.showTime.beginAt < oldestDt) return;
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
	
}
