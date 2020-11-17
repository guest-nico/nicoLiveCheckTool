/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2019/03/14
 * Time: 8:47
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Linq;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Threading;
using namaichi.info;

namespace namaichi.alart
{
	/// <summary>
	/// Description of LiveCheck.
	/// </summary>
	public class LiveCheck
	{
		private MainForm form;
		//private SortableBindingList<LiveInfo> taskListDataSource;
		private bool isLoading = false;
		private object autoUpdateO = null;
		
		public LiveCheck(MainForm form)
		{
			this.form = form;
		}
		public void load() {
			util.debugWriteLine("live check  isLoading " + isLoading);
			if (isLoading) return;
			
			isLoading = true;
			
			var newItems = getCategoryLiveItems();
			if (newItems == null) {
				isLoading = false;
				return;
			}
			var delMin = double.Parse(form.config.get("liveListDelMinutes"));
			var now = DateTime.Now;
			
			var _livelistDataSource = form.liveListDataSource.ToArray();
			var _livelistDataReserve = form.liveListDataReserve.ToArray();
				
			if (Thread.CurrentThread == form.madeThread)
					util.debugWriteLine("lock form thread load");
			
			deleteEndedLive(newItems, _livelistDataSource, _livelistDataReserve, now);
			
			form.liveListLockAction(() =>
					_load(newItems, delMin, now, _livelistDataSource, _livelistDataReserve), "load0");
			
			var sI = form.liveList.FirstDisplayedScrollingRowIndex;
			if (bool.Parse(form.config.get("AutoSort")))
				form.sortLiveList();
			form.setScrollIndex(form.liveList, sI);
			
			form.liveListLockAction(() => {
				if (bool.Parse(form.config.get("FavoriteUp")))
					form.upLiveListFavorite();
			}, "load1");
			
			form.setLiveListNum();
			
			isLoading = false;
		}
		private void _load(List<LiveInfo> newItems, double delMin, DateTime now, LiveInfo[] _livelistDataSource, LiveInfo[] _livelistDataReserve) {
			util.debugWriteLine("live list _load " + newItems.Count + " " + delMin + " " + now);
			if (newItems.Count == 0) return;
			
			try {
				util.debugWriteLine("live list _load new item 0 " + newItems[0]);
				var curCellLv = (form.liveList.CurrentCell == null) ? null : form.liveListDataSource[form.liveList.CurrentCell.RowIndex].lvId;
				var curCellCellI = (form.liveList.CurrentCell == null) ? -1 : form.liveList.CurrentCell.ColumnIndex;
				
				//form.setScrollIndex(form.liveList, scrollI);
				
				var isBlindA = bool.Parse(form.config.get("BlindOnlyA"));
				var isBlindB = bool.Parse(form.config.get("BlindOnlyB"));
				var isBlindQuestion = bool.Parse(form.config.get("BlindQuestion"));
				var isFavoriteOnly = bool.Parse(form.config.get("FavoriteOnly"));
				var cateChar = form.getCategoryChar();
				
				//scrollI = form.liveList.FirstDisplayedScrollingRowIndex;
				foreach (var i in newItems) {
					if (delMin != 0 && ((TimeSpan)(now - i.pubDateDt)).TotalMinutes > delMin)
						continue;
						
					var isContain = false;
					foreach (var a in _livelistDataSource)
						if (a.lvId == i.lvId) 
							isContain = true;
					if (form.liveListDataReserve.Find((LiveInfo li) => li.lvId == i.lvId) != null) isContain = true;
					if (isContain) 
						continue;
	
					var _i = new List<LiveInfo>(){i};
					form.addLiveListItem(_i, cateChar, isBlindA, isBlindB, isBlindQuestion, isFavoriteOnly);
				}
				//form.setScrollIndex(form.liveList, scrollI);
				
				//var ccc = form.liveListDataSource.Count + form.liveListDataReserve.Count;
				
				//scrollI = form.liveList.FirstDisplayedScrollingRowIndex;
				
				//form.setScrollIndex(form.liveList, scrollI);
			
			
			
				var c = form.liveList.Rows.Count;
				form.formAction(() => {
				    
					foreach (var r in _livelistDataSource) {
						//var isDisp = (r.MainCategory[0] == cateChar || 
						//            cateChar == '全');
						var isDisp = r.isDisplay(cateChar);
				        /*
						if (form.liveList.CurrentCell != null && 
								form.liveList.CurrentCell.RowIndex == r.Index && !vi) {
							form.liveList.CurrentCell = null;
							util.debugWriteLine("currencell null set");
						}
						*/
						if (!isDisp) {
							form.liveListDataReserve.Add(r);
							form.liveListDataSource.Remove(r);
						}
						
					}
				    
				    /*
				    if (curCellLv != null) {
						for (var i = 0; i < form.liveListDataSource.Count; i++)
							if (form.liveListDataSource[i].lvId == curCellLv)
								form.liveList.CurrentCell = form.liveList[curCellCellI, i];
					}
					*/
				});
				
				form.removeDuplicateLiveList();
				
				
				
				
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
			util.debugWriteLine("live check _load after reserve");
		}
		private void deleteEndedLive(List<LiveInfo> newItems, LiveInfo[] _livelistDataSource, LiveInfo[] _livelistDataReserve, DateTime now) {
			var delList = new List<LiveInfo>();
			foreach (var l in _livelistDataSource) {
				
				if (newItems.Find((x) => x.lvId == l.lvId) == null) {
					//util.debugWriteLine("dellist found " + l.lvId + " " + l.title + " " + l.lastExistTime);
					if (((TimeSpan)(now - l.lastExistTime)).Minutes > 5)
						delList.Add(l);
				} else {
					//util.debugWriteLine("dellist not found " + l.lvId + " " + l.title + " " + l.lastExistTime);
					l.lastExistTime = now;
				}
			}
			foreach (var l in _livelistDataReserve) {
				if (newItems.Find((x) => x.lvId == l.lvId) == null) {
					if (((TimeSpan)(now - l.lastExistTime)).Minutes > 5)
						delList.Add(l);
				} else l.lastExistTime = now;
			}
			util.debugWriteLine("deleteEndedLive delList " + delList.Count() + " new " + newItems.Count() + " dataSource " + _livelistDataSource.Count());
			
			foreach (var d in delList) {
				util.debugWriteLine("delList remove " + d.lvId + " " + d.title);
				form.removeLiveListItem(d);
			}
		}
		/*
		private List<LiveInfo> getLiveItems() {
			var loadTime = DateTime.Now;
			var tab = getTabName();
			var url = "https://live.nicovideo.jp/recent/rss?tab=" + tab + "&p=";
			util.debugWriteLine(url);
			var readNum = int.Parse(form.config.get("thresholdpage")) + 1;
			var buf = new List<LiveInfo>();
			for (var i = 1; i < readNum; i++) {
				util.debugWriteLine("rss page i " + i);
				try {
					var xdocu = XDocument.Load(url + i.ToString());
					XElement item = (XElement)xdocu.Root.FirstNode;
					var itemCount = 0;
					foreach (XElement it in item.Nodes()) {
						if (it.Name != "item") continue;
						var rssItem = new List<KeyValuePair<string, string>>();
						foreach (XElement _it in it.Nodes()) {
							//Debug.WriteLine(_it.Name.LocalName + " " + _it.Value);
							if (_it.Name.LocalName == "thumbnail")
								rssItem.Add(new KeyValuePair<string, string>(_it.Name.LocalName, _it.Attribute(XName.Get("url")).Value));
							else rssItem.Add(new KeyValuePair<string, string>(_it.Name.LocalName, _it.Value));
						}
						
						var li = new LiveInfo(rssItem, form.alartListDataSource, form.config, form.userAlartListDataSource);
						buf.Add(li);
						itemCount++;
						//Debug.WriteLine("");
					}
					
					if (itemCount == 0) break;
				} catch (Exception e) {
					util.debugWriteLine("getliveitems xml " + e.Message + e.Source + e.StackTrace + e.TargetSite);
					Thread.Sleep(180000);
				}
			}
			util.debugWriteLine("load rss item " + buf.Count);
			return buf;
		}
		*/
		private List<LiveInfo> getCategoryLiveItems() {
			var loadTime = DateTime.Now;
			var tab = getTabName();
			var categoryNames = new string[]{"common", "try", "live", "req"};
			
			var readNum = int.Parse(form.config.get("thresholdpage"));
			var buf = new List<LiveInfo>();
			var liveListLv = form.liveListDataSource.Select(x => x.lvId).ToList();
			foreach (var name in categoryNames) {
				if (name != tab && tab != "") continue;
				var url = "https://live.nicovideo.jp/front/api/pages/recent/v1/programs?tab=" + name + "&offset=#&sortOrder=recentDesc";
				util.debugWriteLine(url);
				
				for (var i = 0; i < readNum; i++) {
					util.debugWriteLine("livecheck category page i " + i + " " + name);
					try {
						var res = util.getPageSource(url.Replace("#", i.ToString()), null);
						if (res == null) {
							Thread.Sleep(10000);
							continue;
						}
						var categoryObj = (CategoryRecent)Newtonsoft.Json.JsonConvert.DeserializeObject<CategoryRecent>(res);
						if (categoryObj.meta.errorCode != "OK") continue;
						
						var itemCount = 0;
						foreach(var d in categoryObj.data) {
							try {
								var item = d.getRssItem(name);
								if (item == null) continue;
								if (buf.Find(x => x.lvId == item.lvId) != null)	continue;
								//if (liveListLv.IndexOf(item.lvId) > -1) continue;
								
								var li = new LiveInfo(item, form.alartListDataSource.ToArray(), form.config, form.userAlartListDataSource.ToArray());
								buf.Add(li);
								itemCount++;
							} catch (Exception e) {
								util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
							}
						}
						//if (itemCount == 0) break;
						
					} catch (Exception e) {
						util.debugWriteLine("getliveitems xml " + e.Message + e.Source + e.StackTrace + e.TargetSite);
						Thread.Sleep(180000);
						return buf;
					}
				}
			}
			util.debugWriteLine("load category item " + buf.Count);
			return buf;
		}
		public void startAutoUpdate() {
			autoUpdateO = new object();
			var t = autoUpdateO;
			Task.Factory.StartNew(() => {
				util.debugWriteLine("livelist autoupdate start");
				while (autoUpdateO == t) {
					load();
					Thread.Sleep((int)(double.Parse(form.config.get("liveListUpdateMinutes")) * 60000));
					//Thread.Sleep(10000);
				}
				util.debugWriteLine("livelist autoupdate end");
			});
			
		}
		public void stopAutoUpdate() {
			autoUpdateO = null;
		}
		private string getTabName() {
			var c = form.config.get("cateCategoryType");
			if (c == "1") return "common";
			else if (c == "2") return "try";
			else if (c == "3") return "live";
			else if (c == "4") return "req";
			//else if (c == "5") return "superichiba";
			//else if (c == "6") return "face";
			//else if (c == "7") return "totu";
			//else if (c == "7") return "r18";
			else return "";
		}
		
	}
}
