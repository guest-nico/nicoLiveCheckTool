/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2019/03/14
 * Time: 8:51
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
//using SuperSocket.ClientEngine;
using namaichi.alart;

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
		
		public LiveInfo(List<KeyValuePair<string, string>> item, SortableBindingList<AlartInfo> alartData)
		{
			foreach (var l in item) {
				if (l.Key == "title") title = l.Value;
				else if (l.Key == "guid") lvId = l.Value;
				else if (l.Key == "pubDate") pubDateDt = DateTime.Parse(l.Value);
				else if (l.Key == "description") description = l.Value;
				else if (l.Key == "category") category.Add(l.Value);
				else if (l.Key == "thumbnail") {
					thumbnail = getThumbnail(l.Value);
					thumbnailUrl = l.Value;
				}
				else if (l.Key == "community_name") comName = l.Value;
				else if (l.Key == "community_id") comId = l.Value;
				else if (l.Key == "member_only") memberOnly = bool.Parse(l.Value) ? "限定" : "";
				else if (l.Key == "community_id") comId = l.Value;
				else if (l.Key == "type") type = l.Value;
				else if (l.Key == "owner_name") hostName = l.Value;
				else if (l.Key == "type") type = l.Value;
				else {
					var pass = new string[]{"creator", "link",
							"num_res", "view"};
					if (Array.IndexOf(pass, l.Key) > -1) continue;
					
				}
			}
			setFavorite(alartData);
			//thumbnailUrl = getThumbnail(comId);
		}
		public Image getThumbnail(string url) {
			return ThumbnailManager.getThumbnailRssUrl(url);

		}
		private void setFavorite(SortableBindingList<AlartInfo> aiList) {
			
			try {
				foreach (var ai in aiList) {
					if (comId != null && comId == ai.communityId) {
						if (favorite != "") favorite += ",";
						favorite += "コミュニティID";
						if (memo != "") memo += ",";
						memo += ai.memo;
					}
					if (hostName != null && hostName == ai.hostName) {
						if (favorite != "") favorite += ",";
						favorite += "ユーザー名?";
						if (memo != "") memo += ",";
						memo += ai.memo;
					}
				}
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
			
		}
		public Image ThumbnailUrl
        {
			get { return thumbnail; }
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
            get {
				var d = DateTime.Now - pubDateDt;
				if (d.TotalDays >= 1) return d.ToString("d'日'hh'時間'mm'分'ss'秒'");
				else if (d.TotalHours >= 1) return d.ToString("hh'時間'mm'分'ss'秒'");
				else if (d.TotalMinutes >= 1) return d.ToString("mm'分'ss'秒'");
				else return d.ToString("ss'秒'");
			}
            set {  }
        }
		public string MainCategory
        {
			get { return category[0]; }
            set {  }
        }
		public string Face
        {
			get { return (category.IndexOf("顔出し") > -1) ? "顔" : "";}
            set {  }
        }
		public string Rush
        {
            get { return (category.IndexOf("凸待ち") > -1) ? "凸" : "";}
            set {  }
        }
		public string Cruise
        {
            get { return (category.IndexOf("クルーズ待ち") > -1) ? "ｸﾙｰｽﾞ" : "";}
            set {  }
        }
		public string Cas
        {
            get { return (category.IndexOf("実験放送") > -1) ? "実験" : "";}
            set {  }
        }
		public string MemberOnly
        {
            get { return memberOnly;}
            set { this.memberOnly = value; }
        }
		public string Type
        {
            get { return type;}
            set { this.type = value; }
        }
		public string Favorite
        {
            get { return favorite;}
            set { this.favorite = value; }
        }
		public string Memo
        {
            get { return memo;}
            set { this.memo = value; }
        }
	}
}
