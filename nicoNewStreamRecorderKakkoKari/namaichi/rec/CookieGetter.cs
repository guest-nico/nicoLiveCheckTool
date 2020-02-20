﻿/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2018/05/13
 * Time: 4:02
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using SunokoLibrary.Application;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace namaichi.rec
{
	/// <summary>
	/// Description of CookieGetter.
	/// </summary>
	public class CookieGetter
	{
		private CookieContainer cc;
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


		private bool isSub;
		private bool isRtmp;

		public CookieGetter(config.config cfg)
		{
			this.cfg = cfg;
		}
		async public Task<CookieContainer[]> getHtml5RecordCookie(string url, bool isSub)
		{
			this.isSub = isSub;

			CookieContainer cc;
			if (!isSub)
			{
				cc = await getCookieContainer(cfg.get("BrowserNum"),
						cfg.get("issecondlogin"), cfg.get("accountId"),
						cfg.get("accountPass"), cfg.get("user_session"),
						cfg.get("user_session_secure"), false,
						url);
				if (cc != null)
				{
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

			}
			else
			{
				cc = await getCookieContainer(cfg.get("BrowserNum2"),
						cfg.get("issecondlogin2"), cfg.get("accountId2"),
						cfg.get("accountPass2"), cfg.get("user_session2"),
						cfg.get("user_session_secure2"), true,
						url);
				if (cc != null)
				{
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

			var ret = new CookieContainer[] { cc };
			return ret;
		}
		async private Task<CookieContainer> getCookieContainer(
				string browserNum, string isSecondLogin, string accountId,
				string accountPass, string userSession, string userSessionSecure,
				bool isSub, string url)
		{

			var userSessionCC = getUserSessionCC(userSession, userSessionSecure);
			log += (userSessionCC == null) ? "前回のユーザーセッションが見つかりませんでした。" : "前回のユーザーセッションが見つかりました。";
			if (userSessionCC != null && true)
			{
				//				util.debugWriteLine(userSessionCC.GetCookieHeader(TargetUrl));
				util.debugWriteLine("usersessioncc ishtml5login" + util.getMainSubStr(isSub));
				if (isHtml5Login(userSessionCC, url))
				{
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

			if (browserNum == "2")
			{
				CookieContainer cc = await getBrowserCookie(isSub).ConfigureAwait(false);
				log += (cc == null) ? "ブラウザからユーザーセッションを取得できませんでした。" : "ブラウザからユーザーセッションを取得しました。";
				if (cc != null)
				{
					util.debugWriteLine("browser ishtml5login" + util.getMainSubStr(isSub));
					if (isHtml5Login(cc, url))
					{
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
				isSecondLogin == "true")
			{
				var mail = accountId;
				var pass = accountPass;
				//var accCC = await getAccountCookie(mail, pass).ConfigureAwait(false);
				var accCC = await getAccountCookie(mail, pass).ConfigureAwait(false);
				log += (accCC == null) ? "アカウントログインからユーザーセッションを取得できませんでした。" : "アカウントログインからユーザーセッションを取得しました。";
				if (accCC != null)
				{
					util.debugWriteLine("account ishtml5login" + util.getMainSubStr(isSub));
					if (isHtml5Login(accCC, url))
					{
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
			return null;
		}
		private CookieContainer getUserSessionCC(string us, string uss)
		{
			//var us = cfg.get("user_session");
			//var uss = cfg.get("user_session_secure");
			if ((us == null || us.Length == 0) &&
				(uss == null || uss.Length == 0)) return null;
			var cc = new CookieContainer();

			var c = new Cookie("user_session", us);
			var secureC = new Cookie("user_session_secure", uss);
			var age_auth = new Cookie("age_auth", cfg.get("age_auth"));
			cc = copyUserSession(cc, c, secureC, age_auth);
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
		async private Task<CookieContainer> getBrowserCookie(bool isSub)
		{
			var si = SourceInfoSerialize.load(isSub);

			//			var importer = await SunokoLibrary.Application.CookieGetters.Default.GetInstanceAsync(si, false);
			ICookieImporter importer = await SunokoLibrary.Application.CookieGetters.Default.GetInstanceAsync(si, false).ConfigureAwait(false);
			//			var importers = new SunokoLibrary.Application.CookieGetters(true, null);
			//			var importera = (await SunokoLibrary.Application.CookieGetters.Browsers.IEProtected.GetCookiesAsync(TargetUrl));
			//			foreach (var rr in importer.Cookies)
			//				util.debugWriteLine(rr);
			//importer = await importers.GetInstanceAsync(si, true);
			if (importer == null) return null;

			CookieImportResult result = await importer.GetCookiesAsync(TargetUrl).ConfigureAwait(false);
			if (result.Status != CookieImportState.Success) return null;

			//if (result.Cookies["user_session"] == null) return null;
			//var cookie = result.Cookies["user_session"].Value;

			//util.debugWriteLine("usersession " + cookie);

			var requireCookies = new List<Cookie>();
			var cc = new CookieContainer();
			foreach (Cookie _c in result.Cookies)
			{
				try
				{
					cc.Add(_c);
					if (_c.Name == "age_auth" || _c.Name.IndexOf("user_session") > -1)
					{
						requireCookies.Add(_c);
					}
				}
				catch (Exception e)
				{
					util.debugWriteLine("cookie add browser " + _c.ToString() + e.Message + e.Source + e.StackTrace + e.TargetSite + util.getMainSubStr(isSub));
				}
			}
			//			result.AddTo(cc);
			foreach (var _c in requireCookies) cc.Add(_c);

			var c = cc.GetCookies(TargetUrl)["user_session"];
			var secureC = cc.GetCookies(TargetUrl)["user_session_secure"];
			cc = copyUserSession(cc, c, secureC);


			return cc;

		}
		private bool isHtml5Login(CookieContainer cc, string url)
		{
			var c = cc.GetCookieHeader(new Uri(url));
			for (var i = 0; i < 10; i++)
			{
				var headers = new WebHeaderCollection();
				try
				{
					util.debugWriteLine("ishtml5login getpage " + url + util.getMainSubStr(isSub));
					var _url = (isRtmp) ? ("https://live.nicovideo.jp/api/getplayerstatus/" + util.getRegGroup(url, "(lv\\d+)")) : url;
					pageSource = util.getPageSource(_url, ref headers, cc);
					//					util.debugWriteLine(cc.GetCookieHeader(new Uri(_url)));
					util.debugWriteLine("ishtml5login getpage ok" + util.getMainSubStr(isSub));
				}
				catch (Exception e)
				{
					util.debugWriteLine("cookiegetter ishtml5login " + e.Message + e.StackTrace + util.getMainSubStr(isSub));
					pageSource = "";
					log += "ページの取得中にエラーが発生しました。" + e.Message + e.Source + e.TargetSite + e.StackTrace;
					continue;
				}
				//			isHtml5 = (headers.Get("Location") == null) ? false : true;
				if (pageSource == null)
				{
					log += "ページが取得できませんでした。";
					util.debugWriteLine("not get page" + util.getMainSubStr(isSub));
					continue;
				}
				var isLogin = !(pageSource.IndexOf("\"login_status\":\"login\"") < 0 &&
				   	pageSource.IndexOf("login_status = 'login'") < 0);
				if (isRtmp) isLogin = pageSource.IndexOf("<code>notlogin</code>") == -1;
				util.debugWriteLine("islogin " + isLogin + util.getMainSubStr(isSub));
				log += (isLogin) ? "ログインに成功しました。" : "ログインに失敗しました";
				//			if (!isLogin) log += pageSource;
				if (isLogin)
				{
					//				id = (isRtmp) ? util.getRegGroup(pageSource, "<user_id>(\\d+)</user_id>")
					//					: util.getRegGroup(pageSource, "\"user_id\":(\\d+)");
					id = util.getRegGroup(pageSource, "\"user_id\":(\\d+)");
					if (id == null) id = util.getRegGroup(pageSource, "user_id = (\\d+)");
					util.debugWriteLine("id " + id);
				}
				else
				{
					util.debugWriteLine("not login " + pageSource.Substring(0, 1000) + util.getMainSubStr(isSub));
				}
				return isLogin;
			}
			return false;
		}
		async public Task<CookieContainer> getAccountCookie(string mail, string pass)
		{

			if (mail == null || pass == null) return null;

			var loginUrl = "https://secure.nicovideo.jp/secure/login?site=nicolive";
			//			var param = "mail=" + mail + "&password=" + pass;

			try
			{
				var handler = new System.Net.Http.HttpClientHandler();
				handler.UseCookies = true;
				var http = new System.Net.Http.HttpClient(handler);
				var content = new System.Net.Http.FormUrlEncodedContent(new Dictionary<string, string>
				{
					{"mail", mail}, {"password", pass}
				});

				http.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/77.0.3865.120 Safari/537.36");
				var _res = await http.PostAsync(loginUrl, content).ConfigureAwait(false);
				var res = await _res.Content.ReadAsStringAsync().ConfigureAwait(false);
				//			var a = _res.Headers;

				//			if (res.IndexOf("login_status = 'login'") < 0) return null;

				cc = handler.CookieContainer;

				//				return cc;
			}
			catch (Exception e)
			{
				util.debugWriteLine(e.Message + e.StackTrace + util.getMainSubStr(isSub));
				return null;
			}


			var c = cc.GetCookies(TargetUrl)["user_session"];
			var secureC = cc.GetCookies(TargetUrl)["user_session_secure"];
			cc = copyUserSession(cc, c, secureC);
			log += (c == null) ? "ユーザーセッションが見つかりませんでした。" : "ユーザーセッションが見つかりました。";
			log += (secureC == null) ? "secureユーザーセッションが見つかりませんでした。" : "secureユーザーセッションが見つかりました。";
			if (c == null && secureC == null) return null;
			/*
			var encoder = System.Text.Encoding.GetEncoding("UTF=8");
			var sr = new System.IO.StreamReader(resStream, encoder);
			var xml = sr.ReadToEnd();
			sr.Close();
			resStream.Close();
			
			if (xml.IndexOf("not login") != -1) return null;
			*/
			return cc;

		}
		async public Task<CookieContainer> getAppAccountCookie(string mail, string pass)
		{

			if (mail == null || pass == null) return null;

			var loginUrl = "https://account.nicovideo.jp/api/v2/login/mail_or_tel_password?site=nicocas_android&is_send_mfa_mail=1";
			//			var param = "mail=" + mail + "&password=" + pass;

			try
			{
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
				var _content = "{\"mail_or_tel\": \"" + mail + "\",\"mfa_trusted_device_token\": \"\", \"password\": \"" + pass + "\"}";
				util.debugWriteLine(_content);
				var content = System.Text.Encoding.ASCII.GetBytes(_content);

				var req = (HttpWebRequest)WebRequest.Create(loginUrl);
				req.Method = "POST";
				req.Proxy = null;


				foreach (var h in acPassHeaders)
				{
					if (h.Key.ToLower().Replace("-", "") == "contenttype")
						req.ContentType = h.Value;
					else if (h.Key.ToLower().Replace("-", "") == "useragent")
						req.UserAgent = h.Value;
					else if (h.Key.ToLower().Replace("-", "") == "connection")
						req.KeepAlive = h.Value.ToLower().Replace("-", "") == "keepalive";
					else req.Headers.Add(h.Key, h.Value);
				}

				using (var stream = req.GetRequestStream())
				{
					try
					{
						stream.Write(content, 0, content.Length);
					}
					catch (Exception ee)
					{
						util.debugWriteLine(ee.Message + " " + ee.StackTrace + " " + ee.Source + " " + ee.TargetSite);
					}
				}

				using (var res = (HttpWebResponse)req.GetResponse())
				using (var resStream = res.GetResponseStream())
				using (var resSr = new System.IO.StreamReader(resStream))
				{
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

			}
			catch (Exception e)
			{
				util.debugWriteLine(e.Message + e.StackTrace + util.getMainSubStr(isSub));
				return null;
			}


		}
		private CookieContainer copyUserSession(CookieContainer cc,
				Cookie c, Cookie secureC, Cookie age_auth = null)
		{
			if (c != null && c.Value != "")
			{
				cc.Add(TargetUrl, new Cookie(c.Name, c.Value));
				cc.Add(TargetUrl2, new Cookie(c.Name, c.Value));
				cc.Add(TargetUrl3, new Cookie(c.Name, c.Value));
				cc.Add(TargetUrl4, new Cookie(c.Name, c.Value));
				cc.Add(TargetUrl5, new Cookie(c.Name, c.Value));
				cc.Add(TargetUrl6, new Cookie(c.Name, c.Value));
				cc.Add(TargetUrl7, new Cookie(c.Name, c.Value));
			}
			if (secureC != null && secureC.Value != "")
			{
				cc.Add(TargetUrl, new Cookie(secureC.Name, secureC.Value));
				cc.Add(TargetUrl2, new Cookie(secureC.Name, secureC.Value));
				cc.Add(TargetUrl3, new Cookie(secureC.Name, secureC.Value));
				cc.Add(TargetUrl4, new Cookie(secureC.Name, secureC.Value));
				cc.Add(TargetUrl5, new Cookie(secureC.Name, secureC.Value));
				cc.Add(TargetUrl6, new Cookie(secureC.Name, secureC.Value));
				cc.Add(TargetUrl7, new Cookie(secureC.Name, secureC.Value));
			}
			if (age_auth != null && age_auth.Value != "")
			{
				cc.Add(TargetUrl, new Cookie(age_auth.Name, age_auth.Value));
				cc.Add(TargetUrl2, new Cookie(age_auth.Name, age_auth.Value));
				cc.Add(TargetUrl3, new Cookie(age_auth.Name, age_auth.Value));
				cc.Add(TargetUrl4, new Cookie(age_auth.Name, age_auth.Value));
				cc.Add(TargetUrl5, new Cookie(age_auth.Name, age_auth.Value));
				cc.Add(TargetUrl6, new Cookie(age_auth.Name, age_auth.Value));
				cc.Add(TargetUrl7, new Cookie(age_auth.Name, age_auth.Value));
			}
			/*
			cc.Add(TargetUrl, new Cookie("age_auth", "1"));
			cc.Add(TargetUrl2, new Cookie("age_auth", "1"));
			cc.Add(TargetUrl3, new Cookie("age_auth", "1"));
			cc.Add(TargetUrl4, new Cookie("age_auth", "1"));
			cc.Add(TargetUrl5, new Cookie("age_auth", "1"));
			cc.Add(TargetUrl6, new Cookie("age_auth", "1"));
			cc.Add(TargetUrl7, new Cookie("age_auth", "1"));
			*/
			return cc;
		}
	}

}
