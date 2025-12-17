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
using namaichi.utility;

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
					//setDescription(items);
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
					var _t = int.Parse(config.get("rssUpdateInterval"));
					if (_t < 15) _t = 15;
					Thread.Sleep(_t * 1000);
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
						//res = util.getPageSource(url.Replace("#", i.ToString()), null);
						var h = util.getHeader(null, null, null);
						res = new Curl().getStr(url.Replace("#", i.ToString()), h, CurlHttpVersion.CURL_HTTP_VERSION_2TLS, "GET", null, false);
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
						var isContainLv = items.FindIndex(x => check.checkedLvIdList.FindIndex(y => y.lvId == x.lvId) > -1) > -1;
						if (end == 1000 && 
							    (!_isStartTimeAllCheck || isContainLv) && 
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
						if (items.IndexOf(item) == -1 && check.checkedLvIdList.FindIndex(x => x.lvId == item.lvId) == -1) {
							items.Add(item);
							check.checkedLvIdList.Add(item);
						} else {
							isContainAddedLv = true;
							setChDataToHistoryList(item);
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
		void setChDataToHistoryList(RssItem ri) {
			try {
				if (ri.type == "community" || ri.type == "user") return;
				
				var _hi = check.form.historyListDataSource.FirstOrDefault(x => x.lvid == ri.lvId);
				if (_hi != null && !string.IsNullOrEmpty(ri.hostName) && _hi.userName != ri.hostName) _hi.userName = ri.hostName;
				_hi = check.form.notAlartListDataSource.FirstOrDefault(x => x.lvid == ri.lvId);
				if (_hi != null && !string.IsNullOrEmpty(ri.hostName) && _hi.userName != ri.hostName) _hi.userName = ri.hostName;
				var _li = check.form.liveListDataSource.FirstOrDefault(x => x.lvId == ri.lvId);
				if (_li != null && !string.IsNullOrEmpty(ri.hostName) && _li.hostName != ri.hostName) _li.hostName = ri.hostName;
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
		}
		public void stop() {
			isRetry = false;
		}
	}
}
