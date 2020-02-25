/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2019/02/08
 * Time: 6:12
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using namaichi.info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace namaichi.alart
{
	/// <summary>
	/// Description of RssCheck.
	/// </summary>
	public class CategoryCheck
	{
		//private string[] lastLv = new string[6];
		public bool isStartTimeAllCheck = false;
		private readonly Check check;
		private readonly config.config config;
		private bool isRetry = true;

		private readonly string[] urls = new string[7];
		//private string[] categoryStrList = new string[]{"一般(その他)", ""};
		//private static string[] categoryNames = new string[]{"common", "try", "live", "req", "superichiba", "face", "totu"};
		private static readonly string[] categoryNames = new string[] { "req", "live", "try", "common" };

		public CategoryCheck(Check check, config.config config)
		{
			isStartTimeAllCheck = bool.Parse(config.get("IsStartTimeAllCheck"));

			this.check = check;
			this.config = config;


			urls = new string[4];
			for (int i = 0; i < categoryNames.Length; i++)
			{
				urls[i] = "https://live.nicovideo.jp/front/api/pages/recent/v1/programs?tab=" + categoryNames[i] + "&offset=#&sortOrder=recentDesc";
			}
		}
		public void start(bool isOnlyStartTimeCheck = false)
		{
			if (!isOnlyStartTimeCheck)
			{
				check.form.addLogText("カテゴリページからの取得を開始します");
			}

			bool isFirst = true;
			while (isRetry)
			{
				//util.debugWriteLine("category check lastlv" + lastLv + " " + DateTime.Now);
				util.debugWriteLine("checked lv list start count " + check.checkedLvIdList.Count);

				try
				{
					List<RssItem> items = new List<RssItem>();
					for (int j = 0; j < urls.Length; j++)
					{
						string url = urls[j];

						int end = 1000;
						for (int i = 0; i < 1000 && i < end; i++)
						{
							util.debugWriteLine("category page i " + i + " " + url);

							string res = null;
							for (int k = 0; k < 10; k++)
							{
								res = util.getPageSource(url.Replace("#", i.ToString()), null);
								if (res == null)
								{
									Thread.Sleep(5000);
									//break;

									continue;
								}
								break;
							}
							if (res == null)
							{
								int t = int.Parse(config.get("rssUpdateInterval"));
								if (t < 15)
								{
									t = 15;
								}

								Thread.Sleep(t * 1000);
								check.form.addLogText("カテゴリページの取得に失敗しました " + url.Replace("#", i.ToString()));
								break;
							}

							bool isEndFile = false;
							bool isContainAddedLv = getRssItems(res, ref items, ref isEndFile, categoryNames[j]);
							if (isContainAddedLv)
							{
								bool isContainLv = items.FindIndex(x => check.checkedLvIdList.IndexOf(x.lvId) > -1) != -1;
								if (end == 1000 &&
										(!isStartTimeAllCheck || (isContainLv)) &&
										!isStartTimeAllCheck)
								{
									end = i + 1;
								}
								//						break;
							}
							if (isEndFile)
							{
								break;
							}

							if (isFirst && !isStartTimeAllCheck)
							{
								break;
							}
						}
						//if (items.Count > 0)
						//	lastLv = items[0].lvId;

						util.debugWriteLine("checked lv list count " + check.checkedLvIdList.Count);
						util.debugWriteLine("get category items " + items.Count);
						if (items.Count > -1)
						{
							foreach (RssItem it in items)
							{
								util.debugWriteLine(it.lvId + " " + it.comId + " " + it.hostName + " " + it.title + " " + it.pubDate);
							}
						}

					}
					check.foundLive(items);
					setDescription(items);
					if (isOnlyStartTimeCheck)
					{
						return;
					}

					isStartTimeAllCheck = false;
					isFirst = false;


					if (check.checkedLvIdList.Count > 20000)
					{
						check.deleteOldCheckedLvIdList();
					}

					int _t = int.Parse(config.get("rssUpdateInterval"));
					if (_t < 15)
					{
						_t = 15;
					}

					Thread.Sleep(_t * 1000);
				}
				catch (Exception e)
				{
					util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				}
			}
			check.form.addLogText("カテゴリページからの取得を終了します");
		}
		public bool getRssItems(string res, ref List<RssItem> items, ref bool isEndFile, string categoryName)
		{
			bool isContainAddedLv = false;
			try
			{
				CategoryRecent categoryObj = Newtonsoft.Json.JsonConvert.DeserializeObject<CategoryRecent>(res);
				if (categoryObj.meta.errorCode != "OK")
				{
					return false;
				}

				foreach (_data d in categoryObj.data)
				{
					try
					{
						RssItem item = d.getRssItem(categoryName);
						if (item == null)
						{
							continue;
						}

						if (items.IndexOf(item) == -1 && check.checkedLvIdList.IndexOf(item.lvId) == -1)
						{
							items.Add(item);
							check.checkedLvIdList.Add(item.lvId);
						}
						else
						{
							isContainAddedLv = true;
							//util.debugWriteLine("tuika nasi " + item.lvId + " " + item.title + " " + item.comId);
						}
					}
					catch (Exception e)
					{
						util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
					}
				}
				if (categoryObj.data.Count == 0)
				{
					isEndFile = true;
				}

				return isContainAddedLv;

			}
			catch (Exception e)
			{
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				return isContainAddedLv;
			}
		}
		private void setDescription(List<RssItem> items)
		{
			try
			{
				if (items.Count == 0)
				{
					return;
				}

				int setOkNum = 0;
				DateTime oldestDt = DateTime.MaxValue;
				foreach (RssItem item in items)
				{
					if (oldestDt > item.pubDateDt)
					{
						oldestDt = item.pubDateDt;
					}
				}

				for (int i = 10; i < 100; i += 10)
				{
					string url = "https://api.cas.nicovideo.jp/v2/tanzakus/topic/live/content-groups/onair/items?cursor=" + i + "/cursorEnd/cursorEnd/cursorEnd/" + (i - 10);
					string res = util.getPageSource(url);
					if (res == null)
					{
						return;
					}

					TanzakuOnAir tanzakuObj = Newtonsoft.Json.JsonConvert.DeserializeObject<TanzakuOnAir>(res);
					if (tanzakuObj.meta.status != "200")
					{
						return;
					}

					foreach (TanzakuItem o in tanzakuObj.data.items)
					{
						RssItem ri = items.Find(x => x.lvId == o.id);
						if (ri == null)
						{
							continue;
						}

						if (o.description.Length > 100)
						{
							o.description = o.description.Substring(0, 100);
						}

						ri.description = o.description;

						LiveInfo li = check.form.liveListDataSource.FirstOrDefault(x => x.lvId == o.id);
						if (li != null)
						{
							li.description = o.description;
							int liI = check.form.liveListDataSource.IndexOf(li);
							if (liI > -1)
							{
								check.form.liveList.UpdateCellValue(5, liI);
							}
						}
						else
						{
							//util.debugWriteLine("aa");
						}
						li = check.form.liveListDataReserve.Find(x => x.lvId == o.id);
						if (li != null)
						{
							li.description = o.description;
						}

						HistoryInfo hi = check.form.historyListDataSource.FirstOrDefault(x => x.lvid == o.id);
						if (hi != null)
						{
							hi.description = o.description;
							int hiI = check.form.historyListDataSource.IndexOf(hi);
							if (hiI > -1)
							{
								check.form.historyList.UpdateCellValue(9, hiI);
							}
						}
						hi = check.form.notAlartListDataSource.FirstOrDefault(x => x.lvid == o.id);
						if (hi != null)
						{
							hi.description = o.description;
							int hiI = check.form.notAlartListDataSource.IndexOf(hi);
							if (hiI > -1)
							{
								check.form.notAlartList.UpdateCellValue(9, hiI);
							}
						}

						setOkNum++;
						if (setOkNum >= items.Count)
						{
							return;
						}

						if (o.showTime.beginAt < oldestDt)
						{
							return;
						}
					}
				}
			}
			catch (Exception e)
			{
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
		}
		public void stop()
		{
			isRetry = false;
		}

	}

}
