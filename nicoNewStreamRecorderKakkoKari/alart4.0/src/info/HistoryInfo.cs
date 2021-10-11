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
using System.Threading.Tasks;
using namaichi.rec;

namespace namaichi.info
{
	/// <summary>
	/// Description of AlartHistoryInfo.
	/// </summary>
	public class HistoryInfo
	{
		public DateTime dt = DateTime.Now;
		private string dtStr = null;
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
		public bool isInListMemberOnly = true;
		public string favorite = "";
		public Color textColor = Color.Black;
		public Color backColor = Color.White;
		//public RssItem ri;
		public string keyword = null;
		public int onAirMode = 1;//0-no 1-onAir 2-followerOnly
		public string type = "";
		public bool isMemberOnly = false;
		public bool isPayment = false;
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
			//description = null;
		}
		//public HistoryInfo(RssItem ri, SortableBindingList<AlartInfo> alartData, List<AlartInfo> targetAi)
		public HistoryInfo(RssItem ri, MainForm form, List<AlartInfo> targetAi = null)
		{
			this.dt = DateTime.Parse(ri.pubDate);
			this.dtStr = dt.ToString("yyyy\"/\"MM\"/\"dd HH\":\"mm\":\"ss");
			this.lvid = ri.lvId;
			this.title = util.removeTag(ri.title);
			this.userName = ri.hostName == null ? "" : util.removeTag(ri.hostName);
			this.communityName = util.removeTag(ri.comName);
			this.userId = ri.userId;
			this.communityId = ri.comId;
			if (string.IsNullOrEmpty(ri.description))
				Task.Run(() => setDescription(form));
			else description = util.removeTag(ri.description);
			this.type = ri.type;
			//this.ri = ri;
			onAirMode = ri.isMemberOnly ? 2 : 1;
			this.isMemberOnly = ri.isMemberOnly;
			this.isPayment = ri.isPayment;
			
			if (targetAi != null)
				setFavoriteFromAiList(targetAi, ri, this);
			else favorite = "Twitter";
			
			
		}
		private static void setFavoriteFromAiList(List<AlartInfo> targetAi, RssItem ri, HistoryInfo hi) {
			while (true) {
				try {
					foreach (var ai in targetAi) {
						if (!string.IsNullOrEmpty(ai.communityId)) {
							if (ai.communityId == hi.communityId || 
							    	(ai.communityId == "official" && ri.type == "official")) {
								if (hi.favorite.IndexOf("ｺﾐｭﾆﾃｨID") == -1) {
									if (hi.favorite != "") hi.favorite += ",";
									hi.favorite += "ｺﾐｭﾆﾃｨID";
								}
	//							if (memo != "") memo += ",";
	//							memo += ai.memo;
								hi.textColor = ai.textColor;
								hi.backColor = ai.backColor;
								
							} else hi.isInListCom = false; 
						}
						if (!string.IsNullOrEmpty(ai.hostId)) {
							if (ai.hostId == hi.userId) {
								if (hi.favorite.IndexOf("ﾕｰｻﾞｰID") == -1) {
									if (hi.favorite != "") hi.favorite += ",";
									hi.favorite += "ﾕｰｻﾞｰID";
								}
	//							if (memo != "") memo += ",";
	//							memo += ai.memo;
								hi.textColor = ai.textColor;
								hi.backColor = ai.backColor;
							} else hi.isInListUser = false;
						}
						
						if (!string.IsNullOrEmpty(ai.Keyword) && ri.isMatchKeyword(ai)) {
							if (hi.favorite.IndexOf("ｷｰﾜｰﾄ:") == -1) {
								if (hi.favorite != "") hi.favorite += ",";
								hi.favorite += "ｷｰﾜｰﾄ:" + ai.Keyword;
							}
//							if (memo != "") memo += ",";
//							memo += ai.memo;
							hi.textColor = ai.textColor;
							hi.backColor = ai.backColor;
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
							favorite += "ﾕｰｻﾞｰID";
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
		private void setDescription(MainForm form) {
			var hg = new HosoInfoGetter();
			if (hg.get("https://live2.nicovideo.jp/watch/" + lvid, null)) {
				form.formAction(() => {
					description = hg.description;
					form.historyList.Refresh();
					form.notAlartList.Refresh();
				});
			}
		}
		public string Dt
        {
			get { return dtStr == null ? dt.ToString("yyyy\"/\"MM\"/\"dd HH\":\"mm\":\"ss") : dtStr; }
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
            set { this.userId = value; }
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
		public string IsMemberOnly
        {
			get {
				var t = isMemberOnly ? "限定" : "";
				if (isPayment) t += (t == "" ? "" : ",") + "有料";
				return t; 
			}
			set { 
				this.isMemberOnly = value.IndexOf("限定") > -1;
				this.isPayment = value.IndexOf("有料") > -1;
			}
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
