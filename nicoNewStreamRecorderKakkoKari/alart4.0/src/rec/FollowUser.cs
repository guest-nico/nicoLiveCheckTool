/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2019/01/07
 * Time: 14:27
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using namaichi.config;

namespace namaichi.rec
{
	/// <summary>
	/// Description of FollowUser.
	/// </summary>
	public class FollowUser
	{
		public FollowUser()
		{
		}
		public bool followUser(string comId, CookieContainer cc, MainForm form, config.config cfg) {
//			var isJikken = res.IndexOf("siteId&quot;:&quot;nicocas") > -1;
//			var comId = (isJikken) ? util.getRegGroup(res, "&quot;followPageUrl&quot;\\:&quot;.+?motion/(.+?)&quot;") :
//					util.getRegGroup(res, "Nicolive_JS_Conf\\.Recommend = \\{type\\: 'community', community_id\\: '(co\\d+)'");
			if (comId == null) {
				form.addLogText("このユーザーはフォローできませんでした。");
				return false;
			}
			
			for (var i = 0; i < 1; i++) {
				var isJoinedTask = join3(comId, cc, form, cfg);
				if (isJoinedTask) {
					util.debugWriteLine("user follow ok i " + i);
					return isJoinedTask;
				}
			}
			return false;
//			isJoinedTask.Wait();
			
//			return false;
		}
		/*
		private bool join(string comId, CookieContainer cc, MainForm form, config.config cfg) {
			util.debugWriteLine("follow user " + comId);
			for (int i = 0; i < 3; i++) {
				var comUrl = "https://www.nicovideo.jp/user/" + comId; 
				var url = "https://www.nicovideo.jp/api/watchitem/add";
				var headers = new WebHeaderCollection();
//				headers.Add("Upgrade-Insecure-Requests", "1");
				headers.Add("User-Agent", util.userAgent);
				try {
					/*
					var cg = new CookieGetter(cfg);
					var cgret = cg.getHtml5RecordCookie(url, isSub);
					cgret.Wait();
					                                  
					
		//			cgret.ConfigureAwait(false);
					if (cgret == null || cgret.Result == null) {
						System.Threading.Thread.Sleep(3000);
						continue;
					}
					var _cc = cgret.Result[0];
					
//					var _cc = cgret.Result[(isSub) ? 1 : 0];
//					util.debugWriteLine(cg.pageSource);
					
					var isJidouShounin = util.getPageSource(url, ref headers, cc, comUrl).IndexOf("自動承認されます") > -1;
	//				var _compage = util.getPageSource(url, ref headers, cc);
	//				var gateurl = "http://live.nicovideo.jp/gate/lv313793991";
	//				var __gatePage = util.getPageSource(gateurl, ref headers, cc);
	//				var _compage2 = util.getPageSource(url, ref headers, cc);
//					util.debugWriteLine(cc.GetCookieHeader(new Uri(url)));
					var msg = (isJidouShounin ? "フォローを試みます。" : "自動承認ではありませんでした。") + util.getMainSubStr(isSub, true);
					form.addLogText(msg);
					
					
					if (!isJidouShounin) return false;
					*/
					/*
				} catch (Exception) {
					return false;
				}
				
				
				try {
					var pageRes = util.getPageSource(comUrl, cc);
					if (pageRes == null) continue;
					var token = util.getRegGroup(pageRes, "data-csrf-token=\"(.+?)\"");
					if (token == null) token = util.getRegGroup(pageRes, "Globals.hash = '(.+?)'");
					if (token == null) {
						util.debugWriteLine("user follow token null " + comId);
						return false;
					}
					
					var handler = new System.Net.Http.HttpClientHandler();
					handler.UseCookies = true;
					handler.CookieContainer = cc;
					handler.Proxy = null;
					
					
					var http = new System.Net.Http.HttpClient(handler);
					http.DefaultRequestHeaders.Referrer = new Uri(url);
					/*
					var content = new System.Net.Http.FormUrlEncodedContent(new Dictionary<string, string>
					{
						{"mode", "commit"}, {"title", "フォローリクエスト"}
					});
					*/
					/*
					var enc = Encoding.GetEncoding("UTF-8");
					string data =
					    "item_type=1&item_id=" + comId + "&token=" + token;
					util.debugWriteLine(data);
					byte[] postDataBytes = Encoding.ASCII.GetBytes(data);
	
					util.debugWriteLine("access__ followUser join");
					var req = (HttpWebRequest)WebRequest.Create(url);
					req.Method = "POST";
					req.Proxy = null;
					req.CookieContainer = cc;
					req.Referer = url;
					req.ContentLength = postDataBytes.Length;
					req.ContentType = "application/x-www-form-urlencoded";
					req.Headers.Add("X-Requested-With", "XMLHttpRequest");
					req.Headers.Add("Accept-Encoding", "gzip,deflate");
					req.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
					using (var stream = req.GetRequestStream()) {
						try {
							stream.Write(postDataBytes, 0, postDataBytes.Length);
						} catch (Exception e) {
				       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
				       	}
					}
	//					stream.Close();
					
	
					var res = req.GetResponse();
					
					using (var getResStream = res.GetResponseStream())
					using (var resStream = new System.IO.StreamReader(getResStream)) {
						var resStr = resStream.ReadToEnd();
		
						var isSuccess = resStr.IndexOf("{\"status\":\"ok\"}") > -1;
						if (!isSuccess) {
							util.debugWriteLine(resStr);
							Thread.Sleep(3000);
							continue;
						}
						//var _m = (form.rec.isPlayOnlyMode) ? "視聴" : "録画";
						//form.addLogText((isSuccess ?
						//                 "フォローしました。" + _m + "開始までしばらくお待ちください。" : "フォローに失敗しました。") + util.getMainSubStr(isSub, true));
						return isSuccess;
					}
					
	//				resStream.Close();
					
					
	//				Task<HttpResponseMessage> _resTask = http.PostAsync(url, content);
					
	//				_resTask.Wait();
	//				var _res = _resTask.Result;
					
	//				var resTask = _res.Content.ReadAsStringAsync();
	//				resTask.Wait();
	//				var res = resTask.Result;
		//			var a = _res.Headers;
					
		//			if (res.IndexOf("login_status = 'login'") < 0) return null;
					
	//				var cc = handler.CookieContainer;
					
				} catch (Exception e) {
					//form.addLogText("フォローに失敗しました。");
					util.debugWriteLine(e.Message+e.StackTrace);
					continue;
//					return false;
				}
			}
			//form.addLogText("フォローに失敗しました。");
			util.debugWriteLine("フォロー失敗");
			return false;
		}
		*/
		private bool join2(string comId, CookieContainer cc, MainForm form, config.config cfg) {
			util.debugWriteLine("follow user2 " + comId);
			try {
				var url = "https://public.api.nicovideo.jp/v1/user/followees/niconico-users/" + comId + ".json";
				var headers = new Dictionary<string, string>{
					
					{"Content-Type", "application/json"},
					{"X-Frontend-Id", "6"},
					{"X-Frontend-Version", "0"},
					{"X-Request-With", "https://www.nicovideo.jp/user/" + comId},
					{"Cookie", cc.GetCookieHeader(new Uri(url))},
				};
				
				string d = null;
				var res = util.postResStr(url, headers, d);
				util.debugWriteLine("user follow2 res " + res);
				if (res == null) {
					util.debugWriteLine("user join2 null ");
					return false;
				}
				return res.IndexOf("\"status\":200") > -1;
				
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				return false;
			}
		}
		private bool join3(string comId, CookieContainer cc, MainForm form, config.config cfg) {
			util.debugWriteLine("follow user2 " + comId);
			try {
				var url = "https://user-follow-api.nicovideo.jp/v1/user/followees/niconico-users/" + comId + ".json";
				var headers = new Dictionary<string, string>{
					//{"Content-Type", "application/json"},
					{"X-Frontend-Id", "6"},
					{"X-Frontend-Version", "0"},
					{"X-Request-With", "https://www.nicovideo.jp"},
					{"Referer", "https://www.nicovideo.jp/"},
					{"Cookie", cc.GetCookieHeader(new Uri(url))},
				};
				
				string d = null;
				var res = util.postResStr(url, headers, d);
				util.debugWriteLine("user follow2 res " + res);
				if (res == null) {
					util.debugWriteLine("user join3 null ");
					return false;
				}
				return res.IndexOf("\"status\":200") > -1;
				
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				return false;
			}
		}
		public bool unFollowUser(string comId, CookieContainer cc, MainForm form, config.config cfg) {
			if (comId == null) {
				form.addLogText("このユーザーはフォロー解除できませんでした。");
				return false;
			}
			
			var isUnJoinedTask = unJoin3(comId, cc, form, cfg);
//			isJoinedTask.Wait();
			return isUnJoinedTask;
//			return false;
		}
		/*
		private bool unJoin(string comId, CookieContainer cc, MainForm form, config.config cfg) {
			for (int i = 0; i < 3; i++) {
//				var myPageUrl = "http://www.nicovideo.jp/my";
				var comUrl = "https://www.nicovideo.jp/user/" + comId; 
				var url = "https://www.nicovideo.jp/api/watchitem/delete";
				var headers = new WebHeaderCollection();
//				headers.Add("Upgrade-Insecure-Requests", "1");
				headers.Add("User-Agent", util.userAgent);
				/*
				try {
					var cg = new CookieGetter(cfg);
					var cgret = cg.getHtml5RecordCookie(url, isSub);
					cgret.Wait();

					if (cgret == null || cgret.Result == null) {
						System.Threading.Thread.Sleep(3000);
						continue;
					}
					var _cc = cgret.Result[0];

				} catch (Exception e) {
					return false;
				}
				*/
				/*
				try {
					var pageRes = util.getPageSource(comUrl, cc);
					var token = util.getRegGroup(pageRes, "data-csrf-token=\"(.+?)\"");
					if (token == null) token = util.getRegGroup(pageRes, "Globals.hash = '(.+?)'");
					if (token == null) {
						util.debugWriteLine("user unfollow token null " + comId);
						return false;
					}
					
					var handler = new System.Net.Http.HttpClientHandler();
					handler.UseCookies = true;
					handler.CookieContainer = cc;
					handler.Proxy = null;
					
					var http = new System.Net.Http.HttpClient(handler);
					http.DefaultRequestHeaders.Referrer = new Uri(url);
					/*
					var content = new System.Net.Http.FormUrlEncodedContent(new Dictionary<string, string>
					{
						{"commit", "はい、フォローを解除します"}, {"time", time}, {"commit_key", commit_key}
					});
					*/
					/*
					var enc = Encoding.GetEncoding("UTF-8");
					string data =
					    "id_list[1][]=" + comId + "&token=" + token;
					byte[] postDataBytes = Encoding.ASCII.GetBytes(data);
					
					util.debugWriteLine("access__ followUser unjoin");
					var req = (HttpWebRequest)WebRequest.Create(url);
					req.Method = "POST";
					req.Proxy = null;
					req.CookieContainer = cc;
					req.Referer = url;
					req.ContentLength = postDataBytes.Length;
					req.ContentType = "application/x-www-form-urlencoded";
					req.Headers.Add("X-Requested-With", "XMLHttpRequest");
					req.Headers.Add("Accept-Encoding", "gzip,deflate");
					req.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
					using (var stream = req.GetRequestStream()) {
						try {
							stream.Write(postDataBytes, 0, postDataBytes.Length);
						} catch (Exception e) {
				       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
				       	}
					}
	
					var res = req.GetResponse();
					
					using (var getResStream = res.GetResponseStream())
					using (var resStream = new System.IO.StreamReader(getResStream)) {
						var resStr = resStream.ReadToEnd();
		
						var isSuccess = resStr.IndexOf("{\"delete_count\":1,\"status\":\"ok\"}") > -1;
						if (!isSuccess) {
							util.debugWriteLine(resStr);
							Thread.Sleep(3000);
							continue;
						}
						return isSuccess;
					}
					
				} catch (Exception e) {
					//form.addLogText("フォロー解除に失敗しました。");
					util.debugWriteLine(e.Message+e.StackTrace);
					continue;
				}
			}
			//form.addLogText("フォロー解除に失敗しました。");
			util.debugWriteLine("フォロー解除失敗 " + comId);
			return false;
		}
		*/
		/*
		private bool unJoin2(string comId, CookieContainer cc, MainForm form, config.config cfg) {
			util.debugWriteLine("follow user2 " + comId);
			try {
				var url = "https://public.api.nicovideo.jp/v1/user/followees/niconico-users/" + comId + ".json";
				var headers = new Dictionary<string, string>{
					{"Content-Type", "application/json"},
					{"X-Frontend-Id", "6"},
					{"X-Frontend-Version", "0"},
					{"X-Request-With", "https://www.nicovideo.jp/user/" + comId},
					{"Cookie", cc.GetCookieHeader(new Uri(url))},
				};
				var res = util.sendRequest(url, headers, null, "DELETE", false);
				using (var r = res.GetResponseStream())
				using (var sr = new StreamReader(r)) {
					var rr = sr.ReadToEnd();
					util.debugWriteLine("unjoin2 res " + rr);
					return rr.IndexOf("\"status\":200") > -1;
				}
				
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				return false;
			}
		}
		*/
		private bool unJoin3(string comId, CookieContainer cc, MainForm form, config.config cfg) {
			util.debugWriteLine("follow user3 " + comId);
			try {
				var url = "https://user-follow-api.nicovideo.jp/v1/user/followees/niconico-users/" + comId + ".json";
				var headers = new Dictionary<string, string>{
					//{"Content-Type", "application/json"},
					{"X-Frontend-Id", "6"},
					{"X-Frontend-Version", "0"},
					{"X-Request-With", "https://www.nicovideo.jp"},
					{"Referer", "https://www.nicovideo.jp/"},
					{"Cookie", cc.GetCookieHeader(new Uri(url))},
				};
				
				string d = null; 
				var rr = util.postResStr(url, headers, d, false, "DELETE");
				util.debugWriteLine("unjoin2 res " + rr);
				return rr.IndexOf("\"status\":200") > -1;
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				return false;
			}
		}
	}
}
