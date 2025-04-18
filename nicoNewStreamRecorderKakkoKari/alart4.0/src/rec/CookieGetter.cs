﻿/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2018/05/13
 * Time: 4:02
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using namaichi.alart;
using namaichi.gui;
using namaichi.utility;
using SunokoLibrary.Application;
using System.Net.Http;
using System.Collections.Generic;

namespace namaichi.rec
{
	/// <summary>
	/// Description of CookieGetter.
	/// </summary>
	public class CookieGetter
	{
		public string pageSource = null;
		public bool isHtml5 = false;
		private config.config cfg;
		public string log = "";
		public string id = null;
		static readonly Uri TargetUrl = new Uri("https://live.nicovideo.jp/");
		static readonly Uri TargetUrl2 = new Uri("https://live2.nicovideo.jp");
		static readonly Uri TargetUrl3 = new Uri("https://com.nicovideo.jp");
		static readonly Uri TargetUrl4 = new Uri("https://watch.live.nicovideo.jp/api/");
		static readonly Uri TargetUrl5 = new Uri("https://www.nicovideo.jp/");
		static readonly Uri TargetUrl6 = new Uri("https://public.api.nicovideo.jp/");
		static readonly Uri TargetUrl7 = new Uri("https://ch.nicovideo.jp/");
		static readonly Uri TargetUrl8 = new Uri("https://api.cas.nicovideo.jp/");
		static readonly Uri TargetUrl9 = new Uri("https://api.gadget.nicovideo.jp/");
		
		
		
		//private bool isSub;
		//private bool isRtmp;
		private MainForm form;
		
		public CookieGetter(config.config cfg, MainForm form)
		{
			this.cfg = cfg;
			this.form = form;
		}
		async public Task<CookieContainer[]> getHtml5RecordCookie(string url, bool isSub) {
			CookieContainer cc;
			if (!isSub) {
				cc = await getCookieContainer(cfg.get("BrowserNum"),
						cfg.get("issecondlogin"), cfg.get("accountId"), 
						cfg.get("accountPass"), cfg.get("user_session_setting"),
						cfg.get("user_session"), cfg.get("user_session_secure"), false,
						url);
				if (cc != null) {
					var c = cc.GetCookies(TargetUrl)["user_session"];
					var secureC = cc.GetCookies(TargetUrl)["user_session_secure"];
					var age_auth = cc.GetCookies(TargetUrl)["age_auth"];
					
					var l = new List<KeyValuePair<string, string>>();
					if (c != null)
//						cfg.set("user_session", c.Value);
						l.Add(new KeyValuePair<string, string>("user_session", c.Value));
					if (secureC != null)
//						cfg.set("user_session_secure", secureC.Value);
						l.Add(new KeyValuePair<string, string>("user_session_secure", secureC.Value));
					if (age_auth != null)
						l.Add(new KeyValuePair<string, string>("age_auth", age_auth.Value));
					cfg.set(l);
				}
				
			} else {
				cc = await getCookieContainer(cfg.get("BrowserNum2"),
						cfg.get("issecondlogin2"), cfg.get("accountId2"), 
						cfg.get("accountPass2"), cfg.get("user_session_setting2"),
						cfg.get("user_session2"), cfg.get("user_session_secure2"), true, 
						url);
				if (cc != null) {
					var c = cc.GetCookies(TargetUrl)["user_session2"];
					var secureC = cc.GetCookies(TargetUrl)["user_session_secure2"];
					var age_auth = cc.GetCookies(TargetUrl)["age_auth"];
					
					var l = new List<KeyValuePair<string, string>>();
					if (c != null)
//						cfg.set("user_session2", c.Value);
						l.Add(new KeyValuePair<string, string>("user_session2", c.Value));
					if (secureC != null)
//						cfg.set("user_session_secure2", secureC.Value);
						l.Add(new KeyValuePair<string, string>("user_session_secure2", secureC.Value));
					l.Add(new KeyValuePair<string, string>("age_auth", age_auth.Value));
					cfg.set(l);
				}
			}
			
			var ret = new CookieContainer[]{cc};
			return ret;
		}
		async private Task<CookieContainer> getCookieContainer(
				string browserNum, string isSecondLogin, string accountId,
				string accountPass, string userSessionSetting, string userSession, 
				string userSessionSecure, bool isSub, string url) {
			
			var userSessionCC = getUserSessionCC(userSession, userSessionSecure);
			log += (userSessionCC == null) ? "前回のユーザーセッションが見つかりませんでした。" : "前回のユーザーセッションが見つかりました。";
			if (userSessionCC != null && false) {
//				util.debugWriteLine(userSessionCC.GetCookieHeader(TargetUrl));
				util.debugWriteLine("usersessioncc ishtml5login" + util.getMainSubStr(isSub));
				if (isHtml5Login(userSessionCC, url)) {
					/*
					var c = userSessionCC.GetCookies(TargetUrl)["user_session"];
					var secureC = userSessionCC.GetCookies(TargetUrl)["user_session_secure"];
					if (c != null)
						//cfg.set("user_session", c.Value);
						us = c.Value;
					if (secureC != null)
						//cfg.set("user_session_secure", secureC.Value);
						uss = secureC.Value;
					*/
					return userSessionCC;
				}
			}
			
			if (browserNum == "2") {
				CookieContainer cc = await getBrowserCookie(isSub).ConfigureAwait(false);
				log += (cc == null) ? "ブラウザからユーザーセッションを取得できませんでした。" : "ブラウザからユーザーセッションを取得しました。";
				if (cc != null) {
					util.debugWriteLine("browser ishtml5login" + util.getMainSubStr(isSub));
					if (isHtml5Login(cc, url)) {
//						util.debugWriteLine("browser 1 " + cc.GetCookieHeader(TargetUrl));
//						util.debugWriteLine("browser 2 " + cc.GetCookieHeader(new Uri("http://live2.nicovideo.jp")));
						util.debugWriteLine("browser login ok" + util.getMainSubStr(isSub));
						/*
						var c = cc.GetCookies(TargetUrl)["user_session"];
						var secureC = cc.GetCookies(TargetUrl)["user_session_secure"];
						if (c != null)
							//cfg.set("user_session", c.Value);
							us = c.Value;
						if (secureC != null)
							//cfg.set("user_session_secure", secureC.Value);
							uss = secureC.Value;
						*/
						return cc;
					}
					
				}
			}
			
			if (browserNum == "1" || 
			    isSecondLogin == "true") {
				var mail = accountId;
				var pass = accountPass;
				//var accCC = await getAccountCookie(mail, pass).ConfigureAwait(false);
				var accCC = getAccountCookie(mail, pass);
				log += (accCC == null) ? "アカウントログインからユーザーセッションを取得できませんでした。" : "アカウントログインからユーザーセッションを取得しました。";
				if (accCC != null) {
					util.debugWriteLine("account ishtml5login" + util.getMainSubStr(isSub));
					if (isHtml5Login(accCC, url)) {
						util.debugWriteLine("account login ok" + util.getMainSubStr(isSub));
						/*
						var c = accCC.GetCookies(TargetUrl)["user_session"];
						var secureC = accCC.GetCookies(TargetUrl)["user_session_secure"];
						if (c != null)
							//cfg.set("user_session", c.Value);
							us = c.Value;
						if (secureC != null)
							//cfg.set("user_session_secure", secureC.Value);
							uss = secureC.Value;
						*/
						return accCC;
					}
				}
			}
			
			if (browserNum == "3" || 
			    	isSecondLogin == "true") {
				var accCC = getUserSessionCookie(userSessionSetting);
				log += (accCC == null) ? "ユーザーセッションログインからユーザーセッションを取得できませんでした。" : "アカウントログインからユーザーセッションを取得しました。";
				if (accCC != null) {
					util.debugWriteLine("userSession setting ishtml5login");
					if (isHtml5Login(accCC, url)) {
						util.debugWriteLine("userSession setting login ok");
						return accCC;
					}
				}
			}
			return null;
		}
		private CookieContainer getUserSessionCC(string us, string uss) {
			//var us = cfg.get("user_session");
			//var uss = cfg.get("user_session_secure");
			if ((us == null || us.Length == 0) &&
			    (uss == null || uss.Length == 0)) return null;
			var cc = new CookieContainer();
			
			var c = new Cookie("user_session", us);
			var secureC = new Cookie("user_session_secure", uss);
			var age_auth = new Cookie("age_auth", cfg.get("age_auth"));
			cc = setUserSession(cc, c, secureC, age_auth);
 			cc.Add(TargetUrl, new Cookie("player_version", "leo"));
			
			//test
//			cc.Add(TargetUrl, new Cookie("nicosid", "1527623077.1259703149"));
//			cc.Add(TargetUrl, new Cookie("_td", "9278c72a-9d4e-4b77-ac40-73f972913d26"));
//			cc.Add(TargetUrl, new Cookie("_gid", "GA1.2.266519775.1527623073"));
//			cc.Add(TargetUrl, new Cookie("_ga", "GA1.2.1892636543.1527623073"));
//			cc.SetCookies(TargetUrl,"optimizelyBuckets=%7B%7D; optimizelySegments=%7B%223152721399%22%3A%22search%22%2C%223155720808%22%3A%22gc%22%2C%223199620088%22%3A%22false%22%2C%223214930722%22%3A%22false%22%2C%223218750517%22%3A%22referral%22%2C%223219110468%22%3A%22none%22%2C%223233940089%22%3A%22gc%22%2C%223235780522%22%3A%22none%22%2C%225140350011%22%3A%22%25E3%2583%25AD%25E3%2582%25B0%25E3%2582%25A4%25E3%2583%25B3%25E6%25B8%2588%22%2C%225130920861%22%3A%22%25E4%25B8%2580%25E8%2588%25AC%25E4%25BC%259A%25E5%2593%25A1%22%2C%225137970544%22%3A%22216pt%25E6%259C%25AA%25E6%25BA%2580%22%2C%229019961413%22%3A%22%25E9%259D%259E%25E5%25AF%25BE%25E8%25B1%25A1%22%7D; nicorepo_filter=all;  optimizelyEndUserId=oeu1527671506390r0.4517391591303288; " +
//			cc.Add(c);
//			cc.Add(TargetUrl, c);
			return cc;
		}
		async private Task<CookieContainer> getBrowserCookie(bool isSub) {
			var si = SourceInfoSerialize.load(isSub);
			if (si == null) return null;
			
			ICookieImporter importer = null;
			var mode = 1;
			if (mode == 0)
				importer = await SunokoLibrary.Application.CookieGetters.Default.GetInstanceAsync(si, false).ConfigureAwait(false);
			else if (mode == 1) {
				var ty = Type.GetType(si.EngineId + ", SnkLib.App.CookieGetter");
				if (ty == null) return null;
				var factory = (ICookieImporterFactory)Activator.CreateInstance(ty);
				if (factory == null) return null;
				importer = factory.GetCookieImporter(si);
			}
			if (importer == null) return null;
			
			CookieImportResult result = await importer.GetCookiesAsync(TargetUrl).ConfigureAwait(false);
			if (result.Status != CookieImportState.Success) return null;
			
			var requireCookies = new List<Cookie>();
			var cc = new CookieContainer();
			cc.PerDomainCapacity = 200;
			foreach(Cookie _c in result.Cookies) {
				try {
					//cc.Add(_c);
					if (_c.Name == "age_auth" || _c.Name.IndexOf("user_session") > -1) {
						requireCookies.Add(new Cookie(_c.Name, _c.Value, "/", ".nicovideo.jp"));
					}
				} catch (Exception e) {
					util.debugWriteLine("cookie add browser " + _c.ToString() + e.Message + e.Source + e.StackTrace + e.TargetSite + util.getMainSubStr(isSub));
				}
			}
//			result.AddTo(cc);
			foreach (var _c in requireCookies) cc.Add(_c);
			
			var c = cc.GetCookies(TargetUrl)["user_session"];
			var secureC = cc.GetCookies(TargetUrl)["user_session_secure"];
			cc = setUserSession(cc, c, secureC);
			
			
			return cc;
			
		}
		public bool isHtml5Login(CookieContainer cc, string url) {
			
			//var c = cc.GetCookieHeader(new Uri(url));
			//for (var i = 0; i < 1; i++) {
				/*
				var headers = new WebHeaderCollection();
				try {
					util.debugWriteLine("ishtml5login getpage " + url + util.getMainSubStr(isSub));
//					ar _url = (isRtmp) ? ("https://live.nicovideo.jp/api/getplayerstatus/" + util.getRegGroup(url, "(lv\\d+)")) : url;
					pageSource = util.getPageSource(_url, ref headers, cc);
//					util.debugWriteLine(cc.GetCookieHeader(new Uri(_url)));
					util.debugWriteLine("ishtml5login getpage ok" + util.getMainSubStr(isSub));
				} catch (Exception e) {
					util.debugWriteLine("cookiegetter ishtml5login " + e.Message+e.StackTrace + util.getMainSubStr(isSub));
					pageSource = "";
					log += "ページの取得中にエラーが発生しました。" + e.Message + e.Source + e.TargetSite + e.StackTrace;
					continue;
				}
	//			isHtml5 = (headers.Get("Location") == null) ? false : true;
				if (pageSource == null) {
					log += "ページが取得できませんでした。";
					util.debugWriteLine("not get page" + util.getMainSubStr(isSub));
					continue;
				}
				var isLogin = !(pageSource.IndexOf("\"login_status\":\"login\"") < 0 &&
				   	pageSource.IndexOf("login_status = 'login'") < 0);
				
				*/
				
				//test
				//test(cc, url);
				
				var isLogin = false;
				//var isFollow = false;
				string uid = null;
				try {
					//var fl = new FollowChecker(form, cc).getFollowList(new bool[]{true, false, false}, false);
					//if (fl == null) return false;
					
					var us = cc.GetCookies(new Uri(url))["user_session"];
					uid = us == null ? null : util.getRegGroup(us.Value, "user_session_(.+?)_");
					//form.addLogText("usersession " + (us != null) + " uid " + (uid != null));
						
					if (uid != null) {
						if (!isLogin) {
							//test0
							util.debugWriteLine("access__ isHtml5Login 0 ");
							var n = util.getMyName(cc, cc.GetCookies(new Uri(url))["user_session"].Value);
							isLogin = n != null;
						}
					}
				} catch (Exception e) {
					util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				}
				
				util.debugWriteLine("islogin " + isLogin);
				log += (isLogin) ? "ログインに成功しました。" : "ログインに失敗しました";
	//			if (!isLogin) log += pageSource;
				if (isLogin) {
	//				id = (isRtmp) ? util.getRegGroup(pageSource, "<user_id>(\\d+)</user_id>")
	//					: util.getRegGroup(pageSource, "\"user_id\":(\\d+)");
					//id = util.getRegGroup(pageSource, "\"user_id\":(\\d+)");
					//if (id == null) id = util.getRegGroup(pageSource, "user_id = (\\d+)");
					util.debugWriteLine("id " + uid);
				} else {
					util.debugWriteLine("not login " + uid);
				}
				return isLogin;
			//}
			//return false;
		}
		public CookieContainer getAccountCookie(string mail, string pass) {
			
			if (mail == null || pass == null) return null;
			
			var isNew = true;
			
			string loginUrl;
			Dictionary<string, string> param;
			if (isNew) {
				loginUrl = "https://account.nicovideo.jp/login/redirector?show_button_twitter=1&site=niconico&show_button_facebook=1&sec=header_pc&next_url=/";
				param = new Dictionary<string, string> {
					{"mail_tel", mail}, {"password", pass}, {"auth_id", "15263781"}//dummy
				};
			} else {
				loginUrl = "https://secure.nicovideo.jp/secure/login?site=nicolive";
				param = new Dictionary<string, string> {
					{"mail", mail}, {"password", pass}
				};
			}
			
			try {
				var h = new Dictionary<string, string>();
				h.Add("Referer", "https://account.nicovideo.jp/login?site=niconico&next_url=%2F&sec=header_pc&cmnhd_ref=device%3Dpc%26site%3Dniconico%26pos%3Dheader_login%26page%3Dtop");
				h.Add("Content-Type", "application/x-www-form-urlencoded");
				h.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,*/*;q=0.8");
				h.Add("User-Agent", util.userAgent);
				
				var _d = "mail_tel=" + HttpUtility.UrlEncode(param["mail_tel"]) + "&password=" + HttpUtility.UrlEncode(param["password"]) + "&auth_id=" + param["auth_id"];
				var d = Encoding.ASCII.GetBytes(_d);
				var cc = new CookieContainer();
				
				util.debugWriteLine(cc.GetCookieHeader(new Uri(loginUrl)));
				
				if (util.isUseCurl(CurlHttpVersion.CURL_HTTP_VERSION_1_1)) {
					var curlR = new Curl().getStr(loginUrl, h, CurlHttpVersion.CURL_HTTP_VERSION_1_1, "POST", _d, true, true, false);
					if (curlR == null) {
						log += "ログインページに接続できませんでした";
						return null;
					}
					var m = new Regex("Set-Cookie: (.+?)=(.+?);").Matches(curlR);
					if (m.Count == 0) return null;
					Cookie us = null, secureC = null;  
					foreach (Match _m in m) {
						if (_m.Groups[1].Value == "user_session") us = new Cookie(_m.Groups[1].Value, _m.Groups[2].Value);
						if (_m.Groups[1].Value == "user_session_secure") secureC = new Cookie(_m.Groups[1].Value, _m.Groups[2].Value);
					}
					if (us != null) {
						setUserSession(cc, us, secureC);
						return cc;
					}
					
					var locationM = new Regex("Location: (.+)").Matches(curlR);
					if (locationM.Count == 0) {
						log += "ログイン接続の転送先が見つかりませんでした";
						return null;
					}
					var location = locationM[locationM.Count - 1].Groups[1].Value;
					location = util.getRegGroup(curlR, "Location: (.+)\r");
					if (location == null) return null;
					//location = WebUtility.UrlDecode(location);
					
					var setCookie = new Dictionary<string, string>();
					var setCookieM = new Regex("Set-Cookie: (.+?)=(.*?);").Matches(curlR);
					foreach (Match _m in setCookieM) {
						var key = _m.Groups[1].Value;
						if (setCookie.ContainsKey(key)) {
						    	if (_m.Groups[2].Value == "") setCookie.Remove(key);
						    	else if (_m.Groups[2].Value != "") setCookie[key] = _m.Groups[2].Value;
						    }
						else if (_m.Groups[2].Value != "") setCookie.Add(key, _m.Groups[2].Value);
					}
					h["Cookie"] = string.Join("; ", setCookie.Select(x => x.Key + "=" + x.Value).ToArray());
					h.Remove("Content-Type");
					var curlR2 = new Curl().getStr(location, h, CurlHttpVersion.CURL_HTTP_VERSION_1_1, "GET", null, true, true, false);
					
					var browName = util.getRegGroup(curlR2, "id=\"deviceNameInput\".+?value=\"(.+?)\"");
	                if (browName == null) browName = "Google Chrome (Windows)";
	                var mfaUrl = util.getRegGroup(curlR2, "<form action=\"(.+?)\"");
	                if (mfaUrl == null || mfaUrl.IndexOf("/mfa") == -1) {
	                	log += "ログインに失敗しました";
						return null;
	                }
	                mfaUrl = "https://account.nicovideo.jp" + mfaUrl;
	                //mfaUrl = WebUtility.UrlDecode(mfaUrl);
	                var sendTo = util.getRegGroup(curlR2, "class=\"userAccount\">(.+?)</span>");
	                if (sendTo == null && util.getRegGroup(curlR2, "(スマートフォンのアプリを使って)") != null) {
	                	sendTo = "app";
	                }
	                var f = new MfaInputForm(sendTo);
	                f.TopMost = true;
	                
	                DialogResult dr = DialogResult.Cancel;
	                form.formAction(() => {dr = f.ShowDialog(form);}, int.MaxValue);
	                //var dr = f.ShowDialog(form);
	                if (f.code == null) {
	                	log += "二段階認証のコードが入力されていませんでした";
	                	return null;
	                }
	                util.debugWriteLine(mfaUrl);
	                h["Referer"] = location;
	                h["Origin"] = "https://account.nicovideo.jp";
	                h["Content-Type"] = "application/x-www-form-urlencoded";
	                _d = "otp=" + f.code + "&loginBtn=%E3%83%AD%E3%82%B0%E3%82%A4%E3%83%B3&device_name=Google+Chrome+%28Windows%29";
	                var curlR3 = new Curl().getStr(mfaUrl, h, CurlHttpVersion.CURL_HTTP_VERSION_1_1, "POST", _d, true, true, false);
	                if (curlR3 == null) {
	                	log += "二段階認証のコードを正常に送信できませんでした";
	                	return null;
	                }
	                setCookieM = new Regex("Set-Cookie: (.+?)=(.*?);").Matches(curlR3);
					foreach (Match _m in setCookieM) {
						var key = _m.Groups[1].Value;
						if (setCookie.ContainsKey(key)) {
						    	if (_m.Groups[2].Value == "") setCookie.Remove(key);
						    	else setCookie[key] = _m.Groups[2].Value;
						    }
						else setCookie.Add(key, _m.Groups[2].Value);
					}
	                h["Cookie"] = string.Join("; ", setCookie.Select(x => x.Key + "=" + x.Value).ToArray());
	                var location2 = util.getRegGroup(curlR3, "Location: (.+)\r");
	                
	                var curlR4 = new Curl().getStr(location2, h, CurlHttpVersion.CURL_HTTP_VERSION_1_1, "GET", null, true, true, false);
	                m = new Regex("Set-Cookie: (.+?)=(.+?);").Matches(curlR4);
					if (m.Count == 0) return null;
					foreach (Match _m in m) {
						if (_m.Groups[1].Value == "user_session") us = new Cookie(_m.Groups[1].Value, _m.Groups[2].Value);
						if (_m.Groups[1].Value == "user_session_secure") secureC = new Cookie(_m.Groups[1].Value, _m.Groups[2].Value);
					}
					if (us != null) {
						setUserSession(cc, us, secureC);
						return cc;
					}
					return null; 
				}
				
				HttpWebResponse r = null;
				for (var i = 0; i < 3; i++) {
					r = util.sendRequest(loginUrl, h, d, "POST", true, cc);
					if (r != null) break;
				}
				if (r == null) {
					log += "ログインページに接続できませんでした";
					return null;
				}
				var _cc = cc.GetCookies(new Uri(loginUrl));
				if (_cc["user_session"] != null) {
					//cc.Add(r.Cookies["user_session"]);
					return cc;
				}
				if (r.ResponseUri == null || !r.ResponseUri.AbsolutePath.StartsWith("/mfa")) {
					log += "ログインに失敗しました。";
					return null;
				}
				using (var sr = new StreamReader(r.GetResponseStream())) {
					var res = sr.ReadToEnd();
					util.debugWriteLine(res);
					
					var browName = util.getRegGroup(res, "id=\"deviceNameInput\".+?value=\"(.+?)\"");
	                if (browName == null) browName = "Google Chrome (Windows)";
	                var mfaUrl = util.getRegGroup(res, "<form action=\"(.+?)\"");
	                if (mfaUrl == null) {
	                	log += "2段階認証のURLを取得できませんでした。";
						return null;
	                }
	                mfaUrl = "https://account.nicovideo.jp" + mfaUrl;
	                var sendTo = util.getRegGroup(res, "class=\"userAccount\">(.+?)</span>");
	                if (sendTo == null && util.getRegGroup(res, "(スマートフォンのアプリを使って)") != null) {
	                	sendTo = "app";
	                }
	                var f = new MfaInputForm(sendTo);
	                f.TopMost = true;
	                
	                DialogResult dr = DialogResult.Cancel;
	                form.formAction(() => {dr = f.ShowDialog(form);}, int.MaxValue);
	                //var dr = f.ShowDialog(form);
	                if (f.code == null) {
	                	log += "二段階認証のコードが入力されていませんでした";
	                	return null;
	                }
	                util.debugWriteLine(mfaUrl);
	                h["Referer"] = r.ResponseUri.OriginalString;
	                h["Origin"] = "https://account.nicovideo.jp";
	                _d = "otp=" + f.code + "&loginBtn=%E3%83%AD%E3%82%B0%E3%82%A4%E3%83%B3&device_name=Google+Chrome+%28Windows%29";
	                d = Encoding.ASCII.GetBytes(_d);
	                var _r = util.sendRequest(mfaUrl, h, d, "POST", true, cc);
	                if (_r == null) {
	                	log += "二段階認証のコードを正常に送信できませんでした";
	                	return null;
	                }
	                using (var _sr = new StreamReader(_r.GetResponseStream())) {
	                	res = _sr.ReadToEnd();
	                	util.debugWriteLine(res);
	                }
	                _cc = cc.GetCookies(new Uri(loginUrl));
					if (_cc["user_session"] != null) {
						return cc;
	                } else {
	                	log += "2段階認証のログインに失敗しました";
	                	return null;
	                }
				}
			} catch (Exception e) {
				util.debugWriteLine(e.Message+e.StackTrace);
				return null;
			}
		}
		public CookieContainer getUserSessionCookie(string us) {
			try {
				if (string.IsNullOrEmpty(us)) {
					log += "UserSessionが設定されていませんでした " + (us == null ? "null" : "空欄");
					return null;
				}
				var cc = new CookieContainer();
				setUserSession(cc, new Cookie("user_session", us), null, null);
				return cc;
			} catch (Exception e) {
				util.debugWriteLine(e.Message+e.StackTrace);
				log += e.Message + e.Source + e.StackTrace;
				return null;
			}
		}
		/*
		async public Task<CookieContainer> getAppAccountCookie(string mail, string pass) {
			util.debugWriteLine("access__ getAppAccount");
			
			if (mail == null || pass == null) return null;
			
			var loginUrl = "https://account.nicovideo.jp/api/v2/login/mail_or_tel_password?site=nicocas_android&is_send_mfa_mail=1";
//			var param = "mail=" + mail + "&password=" + pass;
			
			try {
				var acPassHeaders = new Dictionary<string, string>() {
					{"Content-Type", "application/json; charset=UTF-8"},
					{"User-Agent", "okhttp/3.10.0"},
					//{"Cookie", "user_session=" + userSession},
					{"X-Frontend-Id", "90"},
					{"X-Frontend-Version", "3.3.0"},
					//{"X-Os-Version", "22"},
					{"X-Request-With", "dream2qltechn"},
					//{"X-Connection-Environment", "wifi"},
					{"Connection", "Keep-Alive"},
					//{"Accept-Encoding", "gzip"},
				};
				var _content = "{\"mail_or_tel\": \"" +  mail + "\",\"mfa_trusted_device_token\": \"\", \"password\": \"" + pass + "\"}";
				util.debugWriteLine(_content);
				var content = System.Text.Encoding.ASCII.GetBytes(_content);
				
				util.debugWriteLine("access__ getAppAccount ");
				var req = (HttpWebRequest)WebRequest.Create(loginUrl);
				req.Method = "POST";
				req.Proxy = null;
				
				
				foreach (var h in acPassHeaders) {
					if (h.Key.ToLower().Replace("-", "") == "contenttype")
						req.ContentType = h.Value;
					else if (h.Key.ToLower().Replace("-", "") == "useragent")
						req.UserAgent = h.Value;
					else if (h.Key.ToLower().Replace("-", "") == "connection")
						req.KeepAlive = h.Value.ToLower().Replace("-", "") == "keepalive";
					else req.Headers.Add(h.Key, h.Value);
				}
				
				using (var stream = req.GetRequestStream()) {
					try {
						stream.Write(content, 0, content.Length);
					} catch (Exception ee) {
			       		util.debugWriteLine(ee.Message + " " + ee.StackTrace + " " + ee.Source + " " + ee.TargetSite);
			       	}
				}
	
				using (var res = (HttpWebResponse)req.GetResponse())
				using (var resStream = res.GetResponseStream())
				using (var resSr = new System.IO.StreamReader(resStream)) {
					var r = resSr.ReadToEnd();
					var cc = new CookieContainer();
					
					var c = new Cookie("user_session", util.getRegGroup(r, "user_session\":\\s*\"(.+?)\""));
					var secureC = new Cookie("user_session_secure", util.getRegGroup(r, "user_session_secure\":\\s*\"(.+?)\""));
					//var c = cc.GetCookies(new Uri(loginUrl))["user_session"];
					//var secureC = cc.GetCookies(new Uri(loginUrl))["user_session_secure"];
					cc = copyUserSession(cc, c, secureC);
					log += (c == null) ? "ユーザーセッションが見つかりませんでした。" : "ユーザーセッションが見つかりました。";
					log += (secureC == null) ? "secureユーザーセッションが見つかりませんでした。" : "secureユーザーセッションが見つかりました。";
					if (c == null && secureC == null) return null;
					
					//passport
					var acPassHeaders2 = new Dictionary<string, string>() {
						//{"Content-Type", "application/json; charset=UTF-8"},
						{"User-Agent", "okhttp/3.10.0"},
						{"Cookie", "user_session=" + c.Value},
						{"X-Frontend-Id", "90"},
						{"X-Frontend-Version", "3.3.0"},
						//{"X-Os-Version", "22"},
						{"X-Request-With", "dream2qltechn"},
						//{"X-Connection-Environment", "wifi"},
						{"Connection", "Keep-Alive"},
						//{"Accept-Encoding", "gzip"},
					};
					
					var url = "https://account.nicovideo.jp/api/v1/users/account_passport";
					var _r = util.postResStr(url, acPassHeaders2, null);
					util.debugWriteLine("account_passport " + _r);
				
				
					return cc;
				}
				
			} catch (Exception e) {
				util.debugWriteLine(e.Message+e.StackTrace);
				return null;
			}
			
				
		}
		*/
		
		private CookieContainer setUserSession(CookieContainer cc, 
				Cookie c, Cookie secureC, Cookie age_auth = null) {
			if (c != null && c.Value != "") {
				cc.Add(new Cookie(c.Name, c.Value, "/", ".nicovideo.jp"));
				cc.Add(new Cookie("SP_SESSION_KEY", c.Value, "/", ".nicovideo.jp"));
			}
			if (secureC != null && secureC.Value != "") {
				cc.Add(new Cookie(secureC.Name, secureC.Value, "/", ".nicovideo.jp"));
			}
			if (age_auth != null && age_auth.Value != "") {
				cc.Add(new Cookie(age_auth.Name, age_auth.Value, "/", ".nicovideo.jp"));
			}
			return cc;
		}
	}
	
}
