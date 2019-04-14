/*
 * Created by SharpDevelop.
 * User: user
 * Date: 2019/01/11
 * Time: 20:21
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Threading;
using System.Text.RegularExpressions;
using System.Net;
using namaichi;

namespace namaichi.alart
{
	/// <summary>
	/// Description of FollowChecker.
	/// </summary>
	public class FollowChecker
	{
		private MainForm form;
		private CookieContainer container;
		
		private Regex myPageFollowRegex = new Regex("<h5><a href=\".*?(\\d+)\">(.*?)</a></h5>");
		public FollowChecker(MainForm form, CookieContainer container)
		{
			this.form = form;
			this.container = container;
		}
		public void check() {
			checkFromMypage();
		}
		private void checkFromMypage() {
			var followList = getFollowList();
			updateAlartList(followList);
		}
		public List<string[]> getFollowList(bool[] types = null) {
			var ret = new List<string[]>();
			var urls = new string[] {
				"https://www.nicovideo.jp/my/fav/user",
				"https://www.nicovideo.jp/my/channel",
				"https://www.nicovideo.jp/my/community"
			};
			for (var i = 0; i < urls.Length; i++) {
				if (types != null && !types[i]) continue;
				
				var l = checkFollowPage(urls[i]);
				//if (l == null) continue;
				ret.AddRange(l);
			}
			return ret;
		}
		private List<string[]> checkFollowPage(string url) {
			var ret = new List<string[]>();
			var idType = (url.IndexOf("user") > -1) ? "" : ((url.IndexOf("community") > -1) ? "co" : "ch");
			for (var i = 1; i < 50; i++) {
				var res = "";
				for (var j = 0; j < 10; j++) {
					res = util.getPageSource(url + ((i == 1) ? "" : ("?page=" + i)), container);
					if (res != null) break;
					Thread.Sleep(3000);
				}
				if (res == null) break;
				var mm = myPageFollowRegex.Matches(res);
				foreach (Match m in mm) {
					ret.Add(new string[]{idType + m.Groups[1].Value, m.Groups[2].Value});
				}
				if (res.IndexOf(">次へ</a></div>") == -1) break;
			}
			return ret;
		}
		private void updateAlartList(List<string[]> followList) {
			form.followUpdate(followList);
			
			
		}
	}
}
