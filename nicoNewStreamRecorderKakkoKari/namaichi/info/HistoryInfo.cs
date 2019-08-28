/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2019/04/09
 * Time: 13:25
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace namaichi.info
{
	/// <summary>
	/// Description of AlartHistoryInfo.
	/// </summary>
	public class HistoryInfo
	{
		public DateTime dt = DateTime.Now;
		public string lvid = null;
		public string title = null;
		public string userName = null;
		public string communityName = null;
		public string userId = null;
		public string communityId = null;
		public string description = null;
		public bool isInListUser = true;
		public bool isInListCom = true;
		public bool isInListKeyword = true;
		public string favorite = "";
		public Color textColor = Color.Black;
		public Color backColor = Color.White;
		public RssItem ri;
		public string keyword = null;
		public int onAirMode = 1;//0-no 1-onAir 2-followerOnly
		/*
		public HistoryInfo(DateTime dt, string lvid,
				string title, string userName, 
				string comName, string userId, 
				string comId, string description)
		{
			this.dt = dt;
			this.lvid = lvid;
			this.title = title;
			this.userName = userName;
			this.communityName = comName;
			this.userId = userId;
			this.communityId = comId;
			this.description = description;
		}
		*/
		public HistoryInfo() {
			
		}
		//public HistoryInfo(RssItem ri, SortableBindingList<AlartInfo> alartData, List<AlartInfo> targetAi)
		public HistoryInfo(RssItem ri, List<AlartInfo> targetAi)
		{
			this.dt = DateTime.Parse(ri.pubDate);
			this.lvid = ri.lvId;
			this.title = util.removeTag(ri.title);
			this.userName = ri.hostName == null ? "" : util.removeTag(ri.hostName);
			this.communityName = util.removeTag(ri.comName);
			this.userId = ri.userId;
			this.communityId = ri.comId;
			this.description = util.removeTag(ri.description);
			this.ri = ri;
			onAirMode = ri.isMemberOnly ? 2 : 1;
			
			
			setFavoriteFromAiList(targetAi);
			
		}
		private void setFavoriteFromAiList(List<AlartInfo> targetAi) {
			while (true) {
				try {
					foreach (var ai in targetAi) {
						if (!string.IsNullOrEmpty(ai.communityId)) {
							if (ai.communityId == communityId) {
								if (favorite != "") favorite += ",";
								favorite += "ｺﾐｭﾆﾃｨID";
	//							if (memo != "") memo += ",";
	//							memo += ai.memo;
								textColor = ai.textColor;
								backColor = ai.backColor;
								
							} else isInListCom = false; 
						}
						if (!string.IsNullOrEmpty(ai.hostId)) {
							if (ai.hostId == userId) {
								if (favorite != "") favorite += ",";
								favorite += "ﾕｰｻﾞｰ名?";
	//							if (memo != "") memo += ",";
	//							memo += ai.memo;
								textColor = ai.textColor;
								backColor = ai.backColor;
							} else isInListUser = false;
						}
						
						if (!string.IsNullOrEmpty(ai.Keyword) && ri.isMatchKeyword(ai)) {
							if (favorite != "") favorite += ",";
							favorite += "ｷｰﾜｰﾄ:" + ai.Keyword;
//							if (memo != "") memo += ",";
//							memo += ai.memo;
							textColor = ai.textColor;
							backColor = ai.backColor;
						}
					}
					break;
				} catch (Exception e) {
					util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
					Thread.Sleep(1000);
				}
			}
		}
		/*
		private void setFavorite(SortableBindingList<AlartInfo> aiList) {
			while (true) {
				try {
					foreach (var ai in aiList) {
						if (communityId != null && communityId == ai.communityId) {
							if (favorite != "") favorite += ",";
							favorite += "ｺﾐｭﾆﾃｨID";
//							if (memo != "") memo += ",";
//							memo += ai.memo;
							textColor = ai.textColor;
							backColor = ai.backColor;
							isInListCom = true;
						}
						if (userName != null && userName == ai.hostName) {
							if (favorite != "") favorite += ",";
							favorite += "ﾕｰｻﾞｰ名?";
//							if (memo != "") memo += ",";
//							memo += ai.memo;
							textColor = ai.textColor;
							backColor = ai.backColor;
							isInListUser = true;
						}
						
						if (!string.IsNullOrEmpty(ai.Keyword) && ri.isMatchKeyword(ai)) {
							if (favorite != "") favorite += ",";
							favorite += "ｷｰﾜｰﾄ:" + ai.Keyword;
//							if (memo != "") memo += ",";
//							memo += ai.memo;
							textColor = ai.textColor;
							backColor = ai.backColor;
						}
					}
					break;
				} catch (Exception e) {
					util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				}
			}
		}
		*/
		public string Dt
        {
			get { return dt.ToString("yyyy/MM/dd HH:mm:ss"); }
            set {  }
        }
		public string Title
        {
			get { return title; }
            set { this.title = value; }
        }
		public string UserName
        {
			get { return userName; }
            set { this.userName = value; }
        }
		public string CommunityName
        {
			get { return communityName; }
            set { this.communityName = value; }
        }
		public string Lvid
        {
			get { return lvid; }
            set { this.lvid = value; }
        }
		public string UserId
        {
			get { return userId; }
            set { this.userName = value; }
        }
		public string CommunityId
        {
			get { return communityId; }
            set { this.communityId = value; }
        }
		public string Keyword
        {
			get { return keyword; }
            set { this.keyword = value; }
        }
		public string Favorite
        {
			get { return favorite; }
            set { this.favorite = value; }
        }
		public string Description
        {
			get { return description; }
            set { this.description = value; }
        }
		
	}
}
