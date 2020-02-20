﻿/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2019/03/14
 * Time: 8:51
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
//using SuperSocket.ClientEngine;
using namaichi.alart;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace namaichi.info
{
	/// <summary>
	/// Description of LiveInfo.
	/// </summary>
	public class LiveInfo
	{
		public string title;
		public string lvId;
		public DateTime pubDateDt;
		public string description;
		public List<string> category = new List<string>();
		public Image thumbnail;
		public string comName;
		public string comId;
		public string memberOnly;
		public string type;

		public string hostName;

		//public string[] tags = null;

		public string face;
		public string rush;
		public string cruise;

		public string favorite = "";
		public string memo = "";

		public DateTime addTime;
		public string thumbnailUrl;

		public Color textColor = Color.Black;
		//public Color backColor = Color.FromArgb(255,224,255);
		public Color backColor = Color.White;
		//private RssItem ri = null;
		public DateTime lastExistTime = DateTime.Now;

		//byte[] deflateThumb = null;
		//Image testImg = null;

		public LiveInfo(List<KeyValuePair<string, string>> item, SortableBindingList<AlartInfo> alartData, config.config config, SortableBindingList<AlartInfo> userAlartData)
		{
			foreach (var l in item)
			{
				try
				{
					if (l.Key == "title") title = l.Value;
					else if (l.Key == "guid") lvId = l.Value;
					else if (l.Key == "pubDate") pubDateDt = DateTime.Parse(l.Value);
					else if (l.Key == "description") description = l.Value;
					else if (l.Key == "category") category.Add(l.Value);
					else if (l.Key == "thumbnail")
					{
						thumbnail = getThumbnail(l.Value, bool.Parse(config.get("liveListCacheIcon")));
						thumbnailUrl = l.Value;
					}
					else if (l.Key == "community_name") comName = l.Value;
					else if (l.Key == "community_id") comId = l.Value;
					else if (l.Key == "member_only") memberOnly = bool.Parse(l.Value) ? "限定" : "";
					//else if (l.Key == "community_id") comId = l.Value;
					else if (l.Key == "type")
					{
						type = l.Value;
						if (type == "user") type = "community";
					}
					else if (l.Key == "owner_name") hostName = l.Value;
					//else if (l.Key == "type") type = l.Value;
					else
					{
						var pass = new string[]{"creator", "link",
								"num_res", "view"};
						if (Array.IndexOf(pass, l.Key) > -1) continue;

					}
				}
				catch (Exception e)
				{
					util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				}
			}
			var ri = new RssItem(title, lvId, "", description, comName, comId, hostName, thumbnailUrl, memberOnly, "");
			favorite = getFavorite(alartData, ri, favorite);
			favorite = getFavorite(userAlartData, ri, favorite);

			if (category == null)
			{
				util.debugWriteLine("not found category 11 " + lvId);
				category = new List<string>() { "一般(その他)" };
			}

			//thumbnailUrl = getThumbnail(comId);



			//deflateThumb = b;
			//testImg = new Bitmap(thumbnail);
		}
		public LiveInfo(RssItem item,
				SortableBindingList<AlartInfo> alartData,
				config.config config,
				SortableBindingList<AlartInfo> userAlartData)
		{
			var ri = item;
			title = ri.title;
			lvId = ri.lvId;
			pubDateDt = DateTime.Parse(ri.pubDate);
			description = ri.description;
			thumbnail = getThumbnail(ri.thumbnailUrl, bool.Parse(config.get("liveListCacheIcon")));
			thumbnailUrl = ri.thumbnailUrl;
			comName = ri.comName;
			comId = ri.comId;
			memberOnly = ri.isMemberOnly ? "限定" : "";
			type = ri.type;
			if (type == "user") type = "community";

			category = ri.category != null ? ri.category : new List<string>() { "一般(その他)" };

			hostName = ri.hostName;

			favorite = getFavorite(alartData, ri, favorite);
			favorite = getFavorite(userAlartData, ri, favorite);

			/*
			var b = (byte[])new ImageConverter().ConvertTo(thumbnail, typeof(byte[]));
			b = System.Text.Encoding.UTF8.GetBytes("あああああああああああああ");
			byte[] outb;
			using (var m = new MemoryStream()) {
				//m.Write(b, 0, b.Length);
				using (var d = new DeflateStream(m, CompressionMode.Compress, true)) {
					d.Write(b, 0, b.Length);
					d.Close();
					outb = m.ToArray();
				}
				
			}
			 */

		}
		public Image getThumbnail(string url, bool isSaveCache)
		{
			//return ThumbnailManager.getThumbnailRssUrl(url, isSaveCache);
			var img = ThumbnailManager.getThumbnailRssUrl(url, isSaveCache);
			if (img == null) return null;
			return new Bitmap(img, 50, 50);
			//return null;
		}

		private string getFavorite(SortableBindingList<AlartInfo> aiList, RssItem ri, string _favorite)
		{
			try
			{
				foreach (var ai in aiList)
				{
					if (!getTargetOk(ai, ri, type)) continue;


					if ((!string.IsNullOrEmpty(comId) && (comId == ai.communityId)) ||
							(type == "official" && ai.communityId == "official"))
					{
						if (_favorite.IndexOf("コミュニティID") == -1)
						{
							if (_favorite != "") _favorite += ",";
							_favorite += "コミュニティID";
						}
						if (memo != "") memo += ",";
						memo += ai.memo;
						textColor = ai.textColor;
						backColor = ai.backColor;
					}
					if (!string.IsNullOrEmpty(hostName) && hostName == ai.hostName)
					{

						if (_favorite.IndexOf("ユーザー名?") == -1)
						{
							if (_favorite != "") _favorite += ",";
							_favorite += "ユーザー名?";
						}
						if (memo != "") memo += ",";
						memo += ai.memo;
						textColor = ai.textColor;
						backColor = ai.backColor;
					}
					if (!string.IsNullOrEmpty(ai.Keyword) && ri.isMatchKeyword(ai))
					{
						if (_favorite.IndexOf("ｷｰﾜｰﾄ:") == -1)
						{
							if (_favorite != "") _favorite += ",";
							_favorite += "ｷｰﾜｰﾄ:" + ai.Keyword;
						}
						if (memo != "") memo += ",";
						memo += ai.memo;
						textColor = ai.textColor;
						backColor = ai.backColor;
					}
				}
			}
			catch (Exception e)
			{
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
			return _favorite;
		}
		private static bool getTargetOk(AlartInfo alartItem, RssItem ri, string type)
		{
			//var targetAi = new List<AlartInfo>();
			//for (var i = 0; i < aiList.Count; i++) {
			//	var alartItem = (AlartInfo)aiList[i];

			var isNosetComId = alartItem.communityId == "" ||
					alartItem.communityId == null;
			var isNosetHostName = alartItem.hostName == "" ||
					alartItem.hostName == null;
			var isNosetKeyword = (alartItem.isCustomKeyword && alartItem.cki == null) ||
				(!alartItem.isCustomKeyword && alartItem.keyword == "" || alartItem.keyword == null);
			if (isNosetComId && isNosetHostName && isNosetKeyword) return false;

			var isComOk = alartItem.communityId == ri.comId || (alartItem.communityId == "official" && type == "official");
			var isUserOk = alartItem.hostName == ri.hostName;
			var isKeywordOk = ri.isMatchKeyword(alartItem);

			if ((string.IsNullOrEmpty(alartItem.communityId) !=
					 string.IsNullOrEmpty(alartItem.communityName)) ||
				 (string.IsNullOrEmpty(alartItem.hostId) !=
					  string.IsNullOrEmpty(alartItem.hostName))) return false;

			if (!isAlartMatch(alartItem, isComOk,
					isUserOk, isKeywordOk, isNosetComId,
					isNosetHostName, isNosetKeyword))
				return false;

			return true;


		}
		private static bool isAlartMatch(AlartInfo alartItem, bool isComOk,
				bool isUserOk, bool isKeywordOk, bool isNosetComId,
				bool isNosetHostName, bool isNosetKeyword)
		{
			if (!isComOk && !isUserOk && !isKeywordOk) return false;
			if (alartItem.isMustCom && !isNosetComId && !isComOk)
				return false;
			if (alartItem.isMustUser && !isNosetHostName && !isUserOk)
				return false;
			if (alartItem.isMustKeyword && !isNosetKeyword && !isKeywordOk)
				return false;

			/*
			if ((!isNosetComId && isComOk) ||
				    (!isNosetHostName && isUserOk) ||
				    (!isNosetKeyword && isKeywordOk)) {
				
			} else {
				continue;
			}
			*/

			if (!(!isNosetComId && isComOk) &&
					!(!isNosetHostName && isUserOk) &&
					!(!isNosetKeyword && isKeywordOk))
			{
				return false;
			}

			return true;
		}
		public Image ThumbnailUrl
		{
			get
			{
				//var b = new ImageConverter().ConvertTo(thumbnail, typeof(byte[]));
				//var t = (Image)new ImageConverter().ConvertFrom(b);

				//return t;
				//return null;
				return thumbnail;
			}
			//get { return null; }
			//get { return new Bitmap(thumbnail, 50, 50); }
			set { this.thumbnail = value; }
		}
		public string Title
		{
			get { return title; }
			set { this.title = value; }
		}
		public string HostName
		{
			get { return hostName; }
			set { this.hostName = value; }
		}
		public string ComName
		{
			get { return comName; }
			set { this.comName = value; }
		}
		public string Description
		{
			get { return description; }
			set { this.description = value; }
		}
		public string LvId
		{
			get { return lvId; }
			set { this.lvId = value; }
		}
		public string ComId
		{
			get { return comId; }
			set { this.comId = value; }
		}
		public string elapsedTime
		{
			get
			{
				TimeSpan d = DateTime.Now - pubDateDt;
				var sign = d.TotalDays < 0 ? "-" : "";
				if (Math.Abs(d.TotalDays) >= 1) return sign + d.ToString("d'日'hh'時間'mm'分'ss'秒'");
				else if (Math.Abs(d.TotalHours) >= 1) return sign + d.ToString("hh'時間'mm'分'ss'秒'");
				else if (Math.Abs(d.TotalMinutes) >= 1) return sign + d.ToString("mm'分'ss'秒'");
				else return sign + d.ToString("ss'秒'");
			}
			set { }
		}
		public string MainCategory
		{
			get
			{
				//{"一般(その他)", "政治",
				//		"動物", "料理", "演奏してみた", "歌ってみた", "踊ってみた",
				//		"描いてみた", "講座", "ゲーム", "動画紹介", "R18",
				//		"顔出し", "凸待ち", "クルーズ待ち"};

				try
				{
					/*
					
					*/
					return category[0];
				}
				catch (Exception e)
				{
					util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
					//return "一般(その他)";
					return "一般";
				}

			}
			set { }
		}
		public bool isDisplay(char cateChar)
		{
			if (cateChar == '全') return true;

			var okList = new List<string>();
			if (cateChar == '一') okList.AddRange(new string[] { "一般(その他)", "政治", "動物", "一般" });
			else if (cateChar == 'や') okList.AddRange(new string[] { "料理", "演奏してみた", "歌ってみた", "描いてみた", "踊ってみた", "講座", "やってみた" });
			else if (cateChar == 'ゲ') okList.AddRange(new string[] { "ゲーム" });
			else if (cateChar == '動') okList.AddRange(new string[] { "動画紹介" });
			//else if (cateChar == '生') okList.AddRange(new string[]{"生ゲームで遊ぶ"});
			//else if (cateChar == '顔') okList.AddRange(new string[]{"顔出し"});
			//else if (cateChar == '凸') okList.AddRange(new string[]{"凸待ち"});

			try
			{
				foreach (var c in category)
				{
					if (okList.IndexOf(c) > -1) return true;
				}
				return false;
			}
			catch (Exception e)
			{
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				return false;
			}
		}
		public string Face
		{
			get { return (category.IndexOf("顔出し") > -1) ? "顔" : ""; }
			set { }
		}
		public string Rush
		{
			get { return (category.IndexOf("凸待ち") > -1) ? "凸" : ""; }
			set { }
		}
		public string Cruise
		{
			get { return (category.IndexOf("クルーズ待ち") > -1) ? "ｸﾙｰｽﾞ" : ""; }
			set { }
		}
		public string Cas
		{
			get { return (category.IndexOf("実験放送") > -1) ? "実験" : ""; }
			set { }
		}
		public string MemberOnly
		{
			get { return memberOnly; }
			set { this.memberOnly = value; }
		}
		public string Type
		{
			get { return type; }
			set { this.type = value; }
		}
		public string Favorite
		{
			get { return favorite; }
			set { this.favorite = value; }
		}
		public string Memo
		{
			get { return memo; }
			set { this.memo = value; }
		}
	}
}
