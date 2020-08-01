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
using System.IO;
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
		private Regex videoUploadedRegex = new Regex("itemId\":(\\d+),\"title\":\"(.+?)\"");
		private Regex followingAppRegex = new Regex("\"id\":.*?\"(.+?)\",\"name\":\"(.+?)\"");
		public FollowChecker(MainForm form, CookieContainer container)
		{
			this.form = form;
			this.container = container;
		}
		public void check() {
			checkAndUpdate();
		}
		private void checkAndUpdate() {
			form.addLogText("フォローリストを確認します");	
			try {
				if (container == null) {
					form.addLogText("Cookieが確認できなかったためフォローリストが取得できませんでした。");	
					return;
				}
				//var followList = getFollowListFromApp();
				var followList = getFollowListFromApp();
				
				if (followList != null) {
					form.addLogText("フォローリストの取得に成功しました " + followList.Count + "件");
					updateAlartList(followList);
				} else {
					form.addLogText("フォローリストが取得できませんでした。再試行します。");	
					followList = getFollowListFromMyPage();
					if (followList != null) {
						form.addLogText("フォローリストの取得に成功しました　" + followList.Count + "件");
						updateAlartList(followList);
					} else form.addLogText("フォローリストが取得できませんでした。");
				}
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				form.addLogText("フォローリストの取得中に未知のエラーが発生しました" + e.Message + e.Source + e.StackTrace + e.TargetSite);	
			}
		}
		public List<string[]> getFollowListFromApp(bool[] types = null) {
			try {
				var ret = new List<string[]>();
				var urls = new string[] {
					//"https://www.nicovideo.jp/my/fav/user",
					//"https://www.nicovideo.jp/my/channel",
					//"https://www.nicovideo.jp/my/community"
					//"https://api.cas.nicovideo.jp/v1/user/following/users",
					//"https://api.cas.nicovideo.jp/v1/user/following/channels",
					//"https://api.cas.nicovideo.jp/v1/user/following/community/owners"
					"https://api.gadget.nicovideo.jp/notification/video_uploaded/users",
					"https://api.gadget.nicovideo.jp/notification/video_uploaded/channels",
					"https://api.gadget.nicovideo.jp/notification/live_started/communities",
				};
				var isSuccess = false;
				for (var i = 0; i < urls.Length; i++) {
					if (types != null && !types[i]) continue;
					
					var l = checkFollowPage(urls[i]);
					if (l == null) {
						form.addLogText(urls[i].Substring(urls[i].LastIndexOf("/") + 1) + "のフォローリストの入手に失敗しました");
						continue;
					}
					isSuccess = true;
					//if (l == null) continue;
					ret.AddRange(l);
				}
				return isSuccess ? ret : null;
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				form.addLogText("フォローリストの作成中にエラーが発生しました" + e.Message + e.Source + e.StackTrace + e.TargetSite);
				return null;
			}
		}
		
		public List<string[]> getFollowListFromMyPage(bool[] types = null) {
			try {
				var ret = new List<string[]>();
				var urls = new string[] {
					"https://www.nicovideo.jp/my/fav/user",
					"https://www.nicovideo.jp/my/channel",
					"https://www.nicovideo.jp/my/community"
					//"https://api.cas.nicovideo.jp/v1/user/following/users",
					//"https://api.cas.nicovideo.jp/v1/user/following/channels",
					//"https://api.cas.nicovideo.jp/v1/user/following/community/owners"
					//"https://api.gadget.nicovideo.jp/notification/video_uploaded/users",
					//"https://api.gadget.nicovideo.jp/notification/video_uploaded/channels",
					//"https://api.gadget.nicovideo.jp/notification/live_started/communities",
				};
				for (var i = 0; i < urls.Length; i++) {
					if (types != null && !types[i]) continue;
					
					var l = checkFollowPageFromMyPage(urls[i]);
					if (l == null) continue;
					//if (l == null) continue;
					ret.AddRange(l);
				}
				return ret;
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				form.addLogText("フォローリストの作成中にエラーが発生しました" + e.Message + e.Source + e.StackTrace + e.TargetSite);
				return null;
			}
		}
		private List<string[]> checkFollowPage(string url) {
			//?offset=0&limit=1000  original25 cas
			//page=0&pageSize=50
			try {
				
				var c = container.GetCookieHeader(new Uri(url));
				var ret = new List<string[]>();
				var idType = (url.IndexOf("user") > -1) ? "" : ((url.IndexOf("communi") > -1) ? "co" : "ch");
				for (var i = 0; i < 50; i++) {
					var res = "";
					for (var j = 0; j < 2; j++) {
						var h = getHeader();
						//res = util.getPageSource(url + ((i == 1) ? "" : ("?page=" + i)), container);
						//var _url = url + "?offset=" + i + "&limit=25";
						
						
						var webRes = util.sendRequest(url + "?page=" + i + "&pageSize=50", h, null, "GET", true);
						if (webRes == null) {
							if (j == 0) {
								util.updateAppVersion("niconico", form.config);
								continue;
							}
							if (i == 0) return null;
							break;
						}
						using (var r = webRes.GetResponseStream())
						using (var sr = new StreamReader(r)) {
							res = sr.ReadToEnd();
							if (res != null) break;
						}
						//res = util.getPageSource(url + "?page=" + i + "&pageSize=50", ref h, cc);
						
						Thread.Sleep(3000);
					}
					if (res == null) break;
					//var mm = myPageFollowRegex.Matches(res);
					var mm = videoUploadedRegex.Matches(res);
					foreach (Match m in mm) {
						ret.Add(new string[]{idType + m.Groups[1].Value, WebUtility.HtmlDecode(m.Groups[2].Value)});
					}
					util.debugWriteLine("mm.count " + url + " " + mm.Count);
					
					//if (res.IndexOf(">次へ</a></div>") == -1) break;
					if (util.getRegGroup(res, "\"next\":(\\d+)") == null) break;
				}
				return ret;
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				form.addLogText("フォローのチェック中に問題が発生しました" + e.Message + e.Source + e.StackTrace + e.TargetSite);
				return null;
			}
		}
		private List<string[]> checkFollowPageFromMyPage(string url) {
			try
			{
				var ret = new List<string[]>();
				var idType = (url.IndexOf("user") > -1) ? "" : ((url.IndexOf("community") > -1) ? "co" : "ch");
				for (var i = 1; i < 50; i++)
				{
					var res = "";
					for (var j = 0; j < 2; j++)
					{
						var c = container.GetCookies(new Uri("https://www.nicovideo.jp"));
						if (c == null) break;
						var _c = c["user_session"].Value;
						var h = new Dictionary<string, string>();
						h.Add("Cookie", "user_session=" + _c);
						var webRes = util.sendRequest(url + ((i == 1) ? "" : ("?page=" + i)), h, null, "GET");
						if (webRes == null) break;
						using (var r = webRes.GetResponseStream())
						using (var sr = new StreamReader(r)) {
							res = sr.ReadToEnd();
							if (res != null) break;
						}
						
						//if (res != null) break;
						Thread.Sleep(3000);
					}
					if (res == null) break;
					var mm = myPageFollowRegex.Matches(res);
					foreach (Match m in mm)
					{
						ret.Add(new string[] { idType + m.Groups[1].Value, WebUtility.HtmlDecode(m.Groups[2].Value) });
					}
					if (res.IndexOf(">次へ</a></div>") == -1) break;
				}
				return ret;
			}
			catch (Exception e)
			{
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				form.addLogText("フォローのチェック中に問題が発生しました" + e.Message + e.Source + e.StackTrace + e.TargetSite);
				return null;
			}
		}
		private void updateAlartList(List<string[]> followList) {
			form.formAction(() => {
				form.followUpdate(followList, false);
				form.followUpdate(followList, true);
			                });
			
		}
		private Dictionary<string, string> getHeader() {
			var h = new Dictionary<string, string>();
			/* cas
			h.Add("User-Agent", "nicocas-Android/3.6.0");
			h.Add("X-Frontend-Id", "90");
			h.Add("X-Frontend-Version", "3.6.0");
			h.Add("X-Os-Version", "22");
			h.Add("X-Model-Name", "GAOO747-UK");
			h.Add("X-Connection-Environment", "wifi");
			*/
			/*
			h.Add("X-Request-With", "nicoandroid");
			h.Add("X-Frontend-Id", "1");
			h.Add("X-Frontend-Version", "5.27.0");
			h.Add("X-Os-Version", "5.1.1");
			h.Add("Cookie", "SP_SESSION_KEY=user_session_278215_1c52ad018016dfa09654b1d0ed2a1f005063c3070cb83dca122635d234d");
			*/
			var c = container.GetCookies(new Uri("https://www.nicovideo.jp"));
			if (c == null) return null;
			var _c = c["user_session"].Value;
			var _h = new Dictionary<string, string>() {
				{"User-Agent", "Niconico/1.0 (Linux; U; Android 7.1.2; ja-jp; nicoandroid LGM-V300K) Version/" + form.config.get("niconicoAppVer")},
				//{"Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8"},
				//{"Accept-Language", "ja,en-US;q=0.7,en;q=0.3"},
				//{"Accept-Encoding", "gzip, deflate, br"},
				{"Cookie", "SP_SESSION_KEY=" + _c},
					{"X-Frontend-Id", "1"},
					{"X-Request-With", "nicoandroid"},
					{"X-Frontend-Version", form.config.get("niconicoAppVer")},
					{"X-Os-Version", "7.1.2"},
					{"X-Model-Name", "GAOO747-UK"},
					{"X-Nicovideo-Connection-Type", "wifi"},
					{"Pragma", "no-cache"},
					{"Connection", "keep-alive"},
					{"Upgrade-Insecure-Requests", "1"},
					{"Cache-Control", "no-cache"},
				};
			return _h;


		}
	}
}

