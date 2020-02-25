/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2019/01/07
 * Time: 14:27
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
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
				var isJoinedTask = join(comId, cc, form, cfg);
				if (isJoinedTask) {
					util.debugWriteLine("user follow ok i " + i);
					return isJoinedTask;
				}
			}
			return false;
//			isJoinedTask.Wait();
			
//			return false;
		}
		private bool join(string comId, CookieContainer cc, MainForm form, config.config cfg) {
			util.debugWriteLine("follow user " + comId);
			for (int i = 0; i < 3; i++) {
				var comUrl = "https://www.nicovideo.jp/user/" + comId; 
				var url = "https://www.nicovideo.jp/api/watchitem/add";
				var headers = new WebHeaderCollection();
//				headers.Add("Upgrade-Insecure-Requests", "1");
				headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.87 Safari/537.36");
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
				} catch (Exception e) {
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
					var enc = Encoding.GetEncoding("UTF-8");
					string data =
					    "item_type=1&item_id=" + comId + "&token=" + token;
					util.debugWriteLine(data);
					byte[] postDataBytes = Encoding.ASCII.GetBytes(data);
	
					var req = (HttpWebRequest)WebRequest.Create(url);
					req.Method = "POST";
					req.Proxy = null;
					req.CookieContainer = cc;
					req.Referer = url;
					req.ContentLength = postDataBytes.Length;
					req.ContentType = "application/x-www-form-urlencoded";
					req.Headers.Add("X-Requested-With", "XMLHttpRequest");
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
		public bool unFollowUser(string comId, CookieContainer cc, MainForm form, config.config cfg) {
			if (comId == null) {
				form.addLogText("このユーザーはフォロー解除できませんでした。");
				return false;
			}
			
			var isUnJoinedTask = unJoin(comId, cc, form, cfg);
//			isJoinedTask.Wait();
			return isUnJoinedTask;
//			return false;
		}
		private bool unJoin(string comId, CookieContainer cc, MainForm form, config.config cfg) {
			for (int i = 0; i < 3; i++) {
//				var myPageUrl = "http://www.nicovideo.jp/my";
				var comUrl = "https://www.nicovideo.jp/user/" + comId; 
				var url = "https://www.nicovideo.jp/api/watchitem/delete";
				var headers = new WebHeaderCollection();
//				headers.Add("Upgrade-Insecure-Requests", "1");
				headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.87 Safari/537.36");
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
					var enc = Encoding.GetEncoding("UTF-8");
					string data =
					    "id_list[1][]=" + comId + "&token=" + token;
					byte[] postDataBytes = Encoding.ASCII.GetBytes(data);
					
					var req = (HttpWebRequest)WebRequest.Create(url);
					req.Method = "POST";
					req.Proxy = null;
					req.CookieContainer = cc;
					req.Referer = url;
					req.ContentLength = postDataBytes.Length;
					req.ContentType = "application/x-www-form-urlencoded";
					req.Headers.Add("X-Requested-With", "XMLHttpRequest");
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
	}
}
