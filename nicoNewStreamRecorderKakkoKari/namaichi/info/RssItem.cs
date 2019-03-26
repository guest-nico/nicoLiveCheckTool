/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2019/01/10
 * Time: 1:06
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Text.RegularExpressions;

namespace namaichi.info
{
	/// <summary>
	/// Description of RssItem.
	/// </summary>
	public class RssItem {
		public string lvId;
		public string hostName;
		public string comId;
		public string comName;
		public string description;
		public string pubDate;
		public string title;
		public string thumbnailUrl;
		public bool isMemberOnly;
		public string[] tags = null;
		public string itemRes;
		public string userId = null;
		
		public RssItem(string title, string lvId, 
				string pubDate, string description,
		        string comName, string comId,
		        string hostName, string thumbnailUrl,
		        string menberOnly, string itemRes) {
			this.lvId = lvId;
			this.hostName = hostName;
			this.comId = comId;
			this.comName = comName;
			this.description = description;
			this.pubDate = pubDate;
			this.title = title;
			this.thumbnailUrl = thumbnailUrl;
			this.isMemberOnly = menberOnly == "true";
			this.itemRes = itemRes;
		}
		public bool isContainKeyword(string keyword) {
			return util.getRegGroup(lvId, "(" + keyword + ")") != null ||
				util.getRegGroup(hostName == null ? "" : hostName, "(" + keyword + ")") != null ||
			    util.getRegGroup(comId, "(" + keyword + ")") != null ||
			    util.getRegGroup(comName, "(" + keyword + ")") != null ||
			    util.getRegGroup(description, "(" + keyword + ")") != null ||
			    util.getRegGroup(title, "(" + keyword + ")") != null;

		}
		public string[] getTag(Regex r) {
			if (tags != null) return tags;
			
			var m = r.Matches(itemRes);
			var ret = new string[m.Count];
			for(var i = 0; i < m.Count; i++) {
				ret[i] = m[i].Groups[1].Value;
			}
			tags = ret; 
			return ret;
		}
		public void setUserId(string userId)
		{
			this.userId = userId;
		}
		public void setTag(string[] tag)
		{
			this.tags = tag;
		}
	}
}
