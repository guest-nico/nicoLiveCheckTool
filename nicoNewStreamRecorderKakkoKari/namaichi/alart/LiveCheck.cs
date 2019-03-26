/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2019/03/14
 * Time: 8:47
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
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
			
			
			var newItems = getLiveItems();
			
			var delMin = double.Parse(form.config.get("liveListDelMinutes"));
			var now = DateTime.Now;
			lock (form.liveListLock) {
				var curCellLv = (form.liveList.CurrentCell == null) ? null : form.liveListDataSource[form.liveList.CurrentCell.RowIndex].lvId;
				var curCellCellI = (form.liveList.CurrentCell == null) ? -1 : form.liveList.CurrentCell.ColumnIndex;
				
				var delList = new List<LiveInfo>();
				foreach (var l in form.liveListDataSource) {
					if (newItems.Find((x) => x.lvId == l.lvId) == null)
						delList.Add(l);
				}
				foreach (var l in form.liveListDataReserve) {
					if (newItems.Find((x) => x.lvId == l.lvId) == null)
						delList.Add(l);
				}
				
				util.debugWriteLine("l");
				form.getVisiRow(true);
				
				foreach (var d in delList) 
					form.removeLiveListItem(d);
				
				util.debugWriteLine("k");
				form.getVisiRow();
				
				var isBlindA = bool.Parse(form.config.get("BlindOnlyA"));
				var isBlindB = bool.Parse(form.config.get("BlindOnlyB"));
				var isBlindQuestion = bool.Parse(form.config.get("BlindQuestion"));
				var cateChar = form.getCategoryChar();
				foreach (var i in newItems) {
					if (delMin != 0 && ((TimeSpan)(now - i.pubDateDt)).TotalMinutes > delMin)
						continue;
						
					var isContain = false;
					foreach (var a in form.liveListDataSource)
						if (a.lvId == i.lvId) 
							isContain = true;
//					for (var j = 0; j < form.liveListDataSource.Count; j++)
//						if (form.liveListDataSource[j].lvId == i.lvId)
//							util.debugWriteLine("conta " + j + " " + i.lvId + " " + form.liveListDataSource.Count);
					if (form.liveListDataReserve.Find((LiveInfo li) => li.lvId == i.lvId) != null) isContain = true;
					if (isContain) 
						continue;

					form.addLiveListItem(i, cateChar, isBlindA, isBlindB, isBlindQuestion);
				}
				
				var ccc = form.liveListDataSource.Count + form.liveListDataReserve.Count;
				util.debugWriteLine("j");
				form.getVisiRow();
				
				if (bool.Parse(form.config.get("AutoSort")))
					form.sortLiveList();
				if (bool.Parse(form.config.get("FavoriteUp")))
					form.upLiveListFavorite();
				
				try {
					var c = form.liveList.Rows.Count;
					form.formAction(() => {
					                	
						foreach (var r in form.liveListDataSource) {
							var isDisp = (r.MainCategory[0] == cateChar || 
							            cateChar == '全');
					        
									
								
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
					    if (curCellLv != null) {
							for (var i = 0; i < form.liveListDataSource.Count; i++)
								if (form.liveListDataSource[i].lvId == curCellLv)
									form.liveList.CurrentCell = form.liveList[curCellCellI, i];
						}
					});
				} catch (Exception e) {
					util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				}
				
				
			}
			form.setLiveListNum();
			
			isLoading = false;
		}
		private List<LiveInfo> getLiveItems() {
			var loadTime = DateTime.Now;
			var tab = getTabName();
			var url = "http://live.nicovideo.jp/recent/rss?tab=" + tab + "&p=";
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
						var li = new LiveInfo(rssItem, form.alartListDataSource);
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
		public void startAutoUpdate() {
			autoUpdateO = new object();
			var t = autoUpdateO;
			Task.Run(() => {
				while (autoUpdateO == t) {
					load();
					Thread.Sleep(int.Parse(form.config.get("liveListUpdateMinutes")) * 60000);
				}
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
			else if (c == "5") return "face";
			else if (c == "6") return "totu";
			//else if (c == "7") return "r18";
			else return "";
		}
	}
}
