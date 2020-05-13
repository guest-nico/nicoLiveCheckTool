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
using System.Collections.Generic;
using System.Net;

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
		public bool isPayment;
		public string[] tags = null;
		public string itemRes;
		public string userId = null;
		
		public List<string> category = null;
		public string type = null;
		
		static Regex getStringToWordsListReg = new Regex("(\\s)(\"\"|\".*?[^\\\\]\")(\\s)");
		public DateTime pubDateDt = DateTime.MinValue;
		public bool isAlarted = false;
		
		public RssItem(string title, string lvId, 
				string pubDate, string description,
		        string comName, string comId,
		        string hostName, string thumbnailUrl,
		        string memberOnly, string itemRes, bool isPayment) {
			this.lvId = lvId;
			this.hostName = WebUtility.HtmlDecode(hostName);
			this.comId = comId;
			this.comName = WebUtility.HtmlDecode(comName);
			this.description = WebUtility.HtmlDecode(description);
			this.pubDate = pubDate;
			this.title = WebUtility.HtmlDecode(title);
			this.thumbnailUrl = thumbnailUrl;
			try {
				this.isMemberOnly = memberOnly != null && memberOnly.ToLower() == "true";
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				
			}
			this.itemRes = itemRes;
			this.isPayment = isPayment;
		}
		public bool isContainKeyword(string keyword) {
			return util.getRegGroup(lvId, "(" + keyword + ")") != null ||
				util.getRegGroup(hostName == null ? "" : hostName, "(" + keyword + ")") != null ||
			    util.getRegGroup(comId, "(" + keyword + ")") != null ||
			    util.getRegGroup(comName, "(" + keyword + ")") != null ||
			    util.getRegGroup(description, "(" + keyword + ")") != null ||
			    util.getRegGroup(title, "(" + keyword + ")") != null;

		}
		public bool isMatchKeyword(AlartInfo ai) {
			if (ai.isCustomKeyword) {
				return isMatchCustomKeyword(ai.cki);
			} else {
				return isMatchSimpleKeyword(ai.keyword);
			}
			
		}
		private bool isMatchSimpleKeyword(string keyword) {
			var keys = getStringToWordsList(keyword);
			//var keys = keyword.Split(new char[]{' ', '　'});
			//if (description.IndexOf("ロク") > -1)
			//	util.debugWriteLine(keyword);
			
			//not
			foreach (var k in keys) {
				try {
					var isNot = k.StartsWith("-") || k.StartsWith("ー");
					if (!isNot) continue;
					var _k = k.Remove(0,1);
					if (isNot && (util.getRegGroup(lvId, "(" + _k + ")") != null ||
							util.getRegGroup(hostName == null ? "" : hostName, "(" + _k + ")") != null ||
						    util.getRegGroup(comId, "(" + _k + ")") != null ||
						    util.getRegGroup(comName, "(" + _k + ")") != null ||
						    util.getRegGroup(description, "(" + _k + ")") != null ||
						    util.getRegGroup(title, "(" + _k + ")") != null))
						return false;
				} catch (Exception e) {
					util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				}
			}
			/*
			//and
			foreach (var k in keys) {
				try {
					var isNot = k.StartsWith("-") || k.StartsWith("ー");
					if (isNot) continue;
					if (!isNot && (util.getRegGroup(lvId, "(" + k + ")") == null &&
							util.getRegGroup(hostName == null ? "" : hostName, "(" + k + ")") == null &&
						    util.getRegGroup(comId, "(" + k + ")") == null &&
						    util.getRegGroup(comName, "(" + k + ")") == null &&
						    util.getRegGroup(description, "(" + k + ")") == null &&
						    util.getRegGroup(title, "(" + k + ")") == null))
						return false;
				} catch (Exception e) {
					util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				}
			}
			*/
			//or
			foreach (var k in keys) {
				try {
					var isNot = k.StartsWith("-") || k.StartsWith("ー");
					if (isNot) continue;
					if (!isNot && (util.getRegGroup(lvId, "(" + k + ")") != null || 
							util.getRegGroup(hostName == null ? "" : hostName, "(" + k + ")") != null ||
						    util.getRegGroup(comId, "(" + k + ")") != null ||
						    util.getRegGroup(comName, "(" + k + ")") != null ||
						    util.getRegGroup(description, "(" + k + ")") != null ||
						    util.getRegGroup(title, "(" + k + ")") != null))
						return true;
				} catch (Exception e) {
					util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				}
			}
			return false;
		}
		
		private List<string> getStringToWordsList(string s) {
			var quot = new List<string>();
			s = " " + s + " ";
			for (var i = 0; i < 100; i++) {
				//Match m = Regex.Match(s, "(\\s)(\"\"|\".*?[^\\\\]\")(\\s)");
				Match m = getStringToWordsListReg.Match(s);
				if (m.Length == 0) break;
				s = s.Remove(m.Index, m.Length - 1);
				if (m.Groups[2].Value.Length == 2) continue;
				quot.Add(m.Groups[2].Value);
			}
//			Debug.WriteLine(a  + " " + quot);
			
			var ret = new List<string>();
			foreach (var c in s.Split(new char[]{' ', '　'})) {
				var d = c.Trim();
				if (d.Length == 0) continue;
//				Debug.WriteLine(d);
				ret.Add(d);
			}
			foreach (var q in quot) {
//				Debug.WriteLine(q.Trim('"'));
				ret.Add(q.Trim(new char[]{'"', ' ', '　'}));
			}
			return ret;
		}
		public string[] getTag(Regex r) {
			if (tags != null) return tags;
			if (itemRes == null) return new string[]{""};
			
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
		private bool isMatchCustomKeyword(List<CustomKeywordInfo> ckis) {
			if (!isNotOk(ckis)) return false;
			if (!isAndOk(ckis)) return false;
			if (!isOrOk(ckis)) return false;
			return true;
		}
		bool isNotOk(List<CustomKeywordInfo> ckis) {
			foreach (var c in ckis) {
				if (c.type == "ワード") {
					if (string.IsNullOrEmpty(c.str)) continue;
					if (c.matchType == "含まない" && isContainKeyword(c.str))
						return false;
				} else {
					//カスタム
					if (c.cki == null) continue;
					if (c.matchType == "含まない" && isMatchCustomKeyword(c.cki))
						return false;
				}
			}
			return true;
		}
		bool isAndOk(List<CustomKeywordInfo> ckis) {
			foreach (var c in ckis) {
				if (c.type == "ワード") {
					if (string.IsNullOrEmpty(c.str)) continue;
					if (c.matchType == "必ず含む" && !isContainKeyword(c.str))
						return false;
				} else {
					//カスタム
					if (c.cki == null) continue;
					if (c.matchType == "必ず含む" && !isMatchCustomKeyword(c.cki))
						return false;
				}
			}
			return true;
		}
		bool isOrOk(List<CustomKeywordInfo> ckis) {
			var isOr = false;
			foreach (var c in ckis) {
				if (c.type == "ワード") {
					if (string.IsNullOrEmpty(c.str)) continue;
					
					if (c.matchType == "いずれかを含む") {
						if (isContainKeyword(c.str)) 
							return true;
						isOr = true;
					}
				} else {
					//カスタム
					if (c.cki == null) continue;
					if (c.matchType == "いずれかを含む") {
						if (isMatchCustomKeyword(c.cki)) 
							return true;
						isOr = true;
					}
				}
			}
			if (isOr) return false;
			return true;
		}
	}
}
