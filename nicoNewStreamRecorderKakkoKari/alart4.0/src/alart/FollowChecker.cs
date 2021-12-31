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
using SuperSocket.ClientEngine;

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
		private bool[] result = new bool[]{false,false,false};
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
				var followList = getFollowList();
				foreach (var f in followList)
					util.debugWriteLine("fff " + f[0] + " " + f[1]);
				
				if (Array.IndexOf(result, true) > -1)
					form.addLogText("フォローリストを取得しました " + followList.Count + "件");
				updateAlartList(followList);
				
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				form.addLogText("フォローリストの取得中に未知のエラーが発生しました" + e.Message + e.Source + e.StackTrace + e.TargetSite);	
			}
		}
		public List<string[]> getFollowListFromApp(bool[] types = null, bool isLog = true) {
			try {
				var ret = new List<string[]>();
				var urls = new string[] {
					//"https://www.nicovideo.jp/my/fav/user",
					//"https://www.nicovideo.jp/my/channel",
					//"https://www.nicovideo.jp/my/community"
					//"https://api.cas.nicovideo.jp/v1/user/following/users",
					//"https://api.cas.nicovideo.jp/v1/user/following/channels",
					//"https://api.cas.nicovideo.jp/v1/user/following/community/owners"
					//"https://api.gadget.nicovideo.jp/notification/video_uploaded/users",
					//"https://api.gadget.nicovideo.jp/notification/video_uploaded/channels",
					//"https://api.gadget.nicovideo.jp/notification/live_started/communities",
					"https://public.api.nicovideo.jp/v1/nicoex/user/followees.json?limit=", //25&cursor=
					"https://api.cas.nicovideo.jp/v1/user/following/channels?offset=", //0&limit=25"
					"https://api.cas.nicovideo.jp/v1/user/following/community/owners?page="//0&pageSize=25
						
				};
				var isSuccess = false;
				for (var i = 0; i < urls.Length; i++) {
					if (types != null && !types[i]) continue;
					
					var l = checkFollowPageApp(urls[i]);
					if (l == null) {
						if (isLog)
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
				if (isLog)
					form.addLogText("フォローリストの作成中にエラーが発生しました" + e.Message + e.Source + e.StackTrace + e.TargetSite);
				return null;
			}
		}
		public List<string[]> getFollowList(bool[] types = null, bool isLog = true) {
			try {
				var ret = new List<string[]>();
				
				if (types == null || types[0]) {
					var userList = getUserList();
					if (userList != null) {
						ret.AddRange(userList);
						result[0] = true;
					} else form.addLogText("ユーザーフォローの取得に失敗しました");
				}
				if (types == null || types[1]) {
					var chList = getChList();
					if (chList != null) {
						ret.AddRange(chList);
						result[1] = true;
					} else form.addLogText("チャンネルフォローの取得に失敗しました");
				}
				if (types == null || types[2]) {
					var coList = getCoList();
					if (coList != null) {
						ret.AddRange(coList);
						result[2] = true;
					} else form.addLogText("コミュニティフォローの取得に失敗しました");
				}
				
				return ret;
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				if (isLog)
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
		private List<string[]> checkFollowPageApp(string url) {
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
						if (h == null) return null;
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
							if (res.IndexOf("\"code\":\"UPDATE_REQUIRED\"") > -1) {
								util.updateAppVersion("niconico", form.config);
								continue;
							}
							if (res.IndexOf("\"code\":\"Unauthorized\"") > -1) return null;
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
			form.followUpdate(followList, false);
			form.followUpdate(followList, true);
			
		}
		private Dictionary<string, string> getHeader(int type = 0) {
			var c = container.GetCookies(new Uri("https://www.nicovideo.jp"));
			if (c == null) return null;
			if (c["user_session"] == null) return null;
			var _c = c["user_session"].Value;
			
			var h = new Dictionary<string, string>();
			if (type == 0) {
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
			} else if (type == 1) {
				//cas
				h.Add("User-Agent", form.config.get("nicoCasAppVer"));
				h.Add("Cookie", "user_session=" + _c);
				h.Add("X-Frontend-Id", "90");
				h.Add("X-Frontend-Version", form.config.get("nicoCasAppVer"));
				h.Add("X-Os-Version", "25");
				h.Add("X-Model-Name", "GAOO747-UK");
				h.Add("X-Connection-Environment", "wifi");
				return h;
			} else if (type == 2) {
				//mypage
				h.Add("Cookie", "user_session=" + _c);
				h.Add("X-Frontend-Id", "6");
				h.Add("X-Frontend-Version", "0");
				return h;
			}
			return null;
			/*
			h.Add("X-Request-With", "nicoandroid");
			h.Add("X-Frontend-Id", "1");
			h.Add("X-Frontend-Version", "5.27.0");
			h.Add("X-Os-Version", "5.1.1");
			h.Add("Cookie", "SP_SESSION_KEY=user_session_278215_1c52ad018016dfa09654b1d0ed2a1f005063c3070cb83dca122635d234d");
			*/
		}
		private List<string[]> getUserList() {
			var url = "https://public.api.nicovideo.jp/v1/nicoex/user/followees.json?limit=25"; //25&cursor=
			var cur = "";
			try {
				var ret = new List<string[]>();
				for (var i = 0; i < 1000; i++) {
					var h = getHeader(1);
					var r = util.sendRequest(url + (i == 0 ? "" : "&cursor=" + cur), h, null, "GET");
					if (r == null) return null;
					using (var rr = r.GetResponseStream())
					using (var sr = new StreamReader(rr)) {
						var res = sr.ReadToEnd();
						if (res == null) return null;
						var m = new Regex("\"userId\":\"(\\d+)\",\"nickname\":\"(.+?)\"").Matches(res);
						foreach (Match _m in m) {
							ret.Add(new string[]{_m.Groups[1].Value, _m.Groups[2].Value});
						}
						cur = util.getRegGroup(res, "\"cursor\":\"(.+?)\"");
						if (cur == "cursorEnd") break;
					}
				}
				return ret;
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace);
				return null;
			}
		}
		/*
		private List<string[]> getUserList2() {
			var url = "https://nvapi.nicovideo.jp/v1/users/me/following/users?pageSize=100"; //25&cursor=
			var cur = "";
			try {
				var ret = new List<string[]>();
				for (var i = 0; i < 1000; i++) {
					var h = getHeader(2);
					var r = util.sendRequest(url + (i == 0 ? "" : "&cursor=" + cur), h, null, "GET");
					if (r == null) return null;
					using (var rr = r.GetResponseStream())
					using (var sr = new StreamReader(rr)) {
						var res = sr.ReadToEnd();
						if (res == null) return null;
						var m = new Regex("\"id\":(\\d+),\"nickname\":\"(.+?)\"").Matches(res);
						foreach (Match _m in m) {
							ret.Add(new string[]{_m.Groups[1].Value, _m.Groups[2].Value});
						}
						cur = util.getRegGroup(res, "\"cursor\":\"(.+?)\"");
						if (cur == "cursorEnd") break;
					}
				}
				return ret;
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace);
				return null;
			}
		}
		*/
		private List<string[]> getChList() {
			var url = "https://api.cas.nicovideo.jp/v1/user/following/channels?offset="; //0&limit=25"
			try {
				var ret = new List<string[]>();
				for (var i = 0; i < 1000; i++) {
					var h = getHeader(1);
					var r = util.sendRequest(url + (i * 25) + "&limit=25", h, null, "GET");
					if (r == null) return null;
					using (var rr = r.GetResponseStream())
					using (var sr = new StreamReader(rr)) {
						var res = sr.ReadToEnd();
						if (res == null) return null;
						var m = new Regex("\"id\":\"(ch\\d+)\",\"name\":\"(.+?)\"").Matches(res);
						if (m.Count == 0) break;
						foreach (Match _m in m) {
							ret.Add(new string[]{_m.Groups[1].Value, _m.Groups[2].Value});
						}
					}
				}
				return ret;
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace);
				return null;
			}
		}
		private List<string[]> getCoList() {
			var url = "https://api.cas.nicovideo.jp/v1/user/following/community/owners?page=";//0&pageSize=25
			try {
				var ret = new List<string[]>();
				for (var i = 0; i < 1000; i++) {
					var h = getHeader(1);
					var r = util.sendRequest(url + i + "&pageSize=25", h, null, "GET");
					if (r == null) return null;
					using (var rr = r.GetResponseStream())
					using (var sr = new StreamReader(rr)) {
						var res = sr.ReadToEnd();
						if (res == null) return null;
						var m = new Regex("\"id\":\"(co\\d+)\",\"name\":\"(.+?)\"").Matches(res);
						if (m.Count == 0) break;
						foreach (Match _m in m) {
							ret.Add(new string[]{_m.Groups[1].Value, _m.Groups[2].Value});
						}
					}
				}
				return ret;
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace);
				return null;
			}
		}
	}
}

