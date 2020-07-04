/*
 * Created by SharpDevelop.
 * User: user
 * Date: 2019/02/04
 * Time: 20:22
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Security.Authentication;
using System.Text.RegularExpressions;
using System.Text;
using System.Net;
using namaichi.alart;
using WebSocket4Net;
using namaichi.info;

namespace namaichi.alart
{
	/// <summary>
	/// Description of PushReceiver.
	/// </summary>
	public class PushReceiver
	{
		private PushCrypto pc = new PushCrypto();
		private byte[] privateKey;
		private byte[] publicKey;
		private byte[] auth;
		private string uaid;
		private string channelId;
		
		private WebSocket ws;
		private DateTime lastWebsocketConnectTime;
		
		private Check check;
		private config.config config;
		private bool isRetry = true;
		
		private DateTime startTime = DateTime.Now;
		
		public PushReceiver(Check check, config.config config) {
			this.check = check;
			this.config = config;
		}
		public void start ()
		{
			
			string pri, pub, _auth;
			pri = pub = _auth = null;
			
			pri = config.get("pushPri");
			pub = config.get("pushPub");
			_auth = config.get("pushAuth");
			this.uaid = config.get("pushUa");
			this.channelId = config.get("pushChId");
			
			if (pri == "" || pub == "" ||
			    	_auth == "" || uaid == "" ||
			    	channelId == "") {
				
				privateKey = publicKey = auth = null;
				uaid = channelId = null;
				
				clearConfigSetting();
				
				try {
					pc.generateKey(out privateKey, out publicKey);
					config.set("pushPri", Convert.ToBase64String(privateKey));
					config.set("pushPub", Convert.ToBase64String(publicKey));
				} catch (Exception e) {
					util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
					return;
				}
				//auth = pc.generateAuth();
//				config.set("pushAuth", Convert.ToBase64String(auth));
			} else {
				privateKey = Convert.FromBase64String(pri);
				publicKey = Convert.FromBase64String(pub);
				auth = Convert.FromBase64String(_auth);
			}
			connect();
			
		}
		public bool connect() {
			
			lock(this) {
				var  isPass = (TimeSpan.FromSeconds(5) > (DateTime.Now - lastWebsocketConnectTime));
				if (isPass) 
					Thread.Sleep(5000);
				lastWebsocketConnectTime = DateTime.Now;
			}
			
			try {
				var url = "wss://push.services.mozilla.com";
				var headers = new List<KeyValuePair<string, string>>();
				headers.Add(new KeyValuePair<string, string>("Sec-WebSocket-Protocol", "push-notification"));
				headers.Add(new KeyValuePair<string, string>("Ssc-WebSocket-Version", "13"));
				
				util.debugWriteLine("push connect  ");
				ws = new WebSocket(url, "", null, headers, "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36", "", WebSocketVersion.Rfc6455, null, SslProtocols.None);
				ws.Opened += onOpen;
				ws.Closed += onClose;
				ws.DataReceived += onDataReceive;
				ws.MessageReceived += onMessageReceive;
				ws.Error += onError;
				
				ws.Open();
			} catch (Exception ee) {
				util.debugWriteLine("push connect exception " + ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
				return false;
			}
			
			var _ws = ws;
			try {
				Thread.Sleep(5000);
				if (_ws != null && _ws.State == WebSocketState.Connecting) {
					util.debugWriteLine("ws connect 5 seconds close");
					try {
						_ws.Close();
					} catch (Exception e) {
						util.debugWriteLine("connect timeout ws exception " + e.Message + e.Source + e.StackTrace + e.TargetSite);
						
					}
					return false;
				}
				
			} catch (Exception ee) {
				try {
					ws.Close();
				} catch (Exception eee) {
					util.debugWriteLine("ws connect exception " + eee.Message + eee.Source + eee.StackTrace + eee.TargetSite);
				}
				try {
					_ws.Close();
				} catch (Exception eee) {
					util.debugWriteLine("ws connect exception " + eee.Message + eee.Source + eee.StackTrace + eee.TargetSite);
				}
				util.debugWriteLine("ws connect exception " + ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
				return false;
			}
			
			return true;
		}
		private void onOpen(object sender, EventArgs e) {
			
			util.debugWriteLine("on open ");
			
			//var uaid = "ba4ca9f1cf784a1f979be6ed12d8f5da";
			var mes = (uaid == null) ?
				"{\"messageType\":\"hello\",\"broadcasts\":null,\"use_webpush\":true}"
				: "{\"messageType\":\"hello\",\"broadcasts\":null,\"use_webpush\":true,\"uaid\":\"" + uaid + "\"}";
			try {
				ws.Send(mes);
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
			
		}
		private void onClose(object sender, EventArgs e) {
			
			util.debugWriteLine("on close " + e.ToString());
			check.form.addLogText("ブラウザプッシュ通知の受信を終了しました");
			
			while (true && isRetry) {
				try {
					if (!connect()) {
						Thread.Sleep(5000);
						continue;
					}
					break;
				} catch (Exception ee) {
					util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
				}
			}
			
		}
		private void onError(object sender, SuperSocket.ClientEngine.ErrorEventArgs e) {
			util.debugWriteLine("on error " + e.Exception.Message + e.Exception.Source + e.Exception.StackTrace + e.Exception.TargetSite);
		}
		private void onDataReceive(object sender, WebSocket4Net.DataReceivedEventArgs e) {
			util.debugWriteLine("on data " + e.Data);
		}
		private void onMessageReceive(object sender, MessageReceivedEventArgs e) {
			
			util.debugWriteLine("on message " + e.Message);
			
			if (e.Message.IndexOf("hello") > -1 && e.Message.IndexOf("200") > -1) {
				if (channelId != null && uaid != null)
					check.form.addLogText("ブラウザプッシュ通知の受信を開始しました");
				
				if (channelId == null) {
					//var _chid2 = System.Guid.NewGuid().ToString();
					var _chid = util.getTimeGuid();
					var pubBase64 = Convert.ToBase64String(publicKey);
					pubBase64 = pubBase64.Replace("/", "_").Replace("+", "-");
					//test
					pubBase64 = "BC08Fdr2JChSL0kr5imO99L6zZG6Rn0tBAWNTlrZfJtsDoeAvmJSa7CnUOHpNhd5zOk0YnRToEOT47YLet8Dpig=";
					
					var regMes = "{\"channelID\":\"" + _chid + "\",\"messageType\":\"register\",\"key\":\"" + pubBase64 + "\"}";
					util.debugWriteLine("register send " + regMes);
					try {
						ws.Send(regMes);
					} catch (Exception ee) {
						util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
					}
				}
				if (uaid == null) {
					uaid = util.getRegGroup(e.Message, "\"uaid\":\"(.+?)\"");
					config.set("pushUa", uaid);
				}
				return;
			}
			
			if (e.Message.IndexOf("\"messageType\":\"register\"") > -1) {
				if (check.container == null) {
					check.form.addLogText("ブラウザプッシュ通知のIDが取得できませんでした");
					
					while (isRetry && check.container == null) {
						Thread.Sleep(3000);
					}
					try {
						ws.Close();
					} catch (Exception ee) {
						util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
					}
					return;
				}
					
				channelId = util.getRegGroup(e.Message, "\"channelID\":\"(.+?)\"");
				if (channelId == null) util.debugWriteLine("channelId not found");
				config.set("pushChId", channelId);
				
				var endpoint = util.getRegGroup(e.Message, "\"pushEndpoint\":\"(.+?)\"");
				if (sendEndpoint(endpoint))
					check.form.addLogText("ブラウザプッシュ通知の受信を開始しました");
				else check.form.addLogText("ブラウザプッシュ通知の接続に失敗しました");
					
			}
			
			if (e.Message.IndexOf("\"messageType\":\"notification\"") > -1) {
				if (privateKey == null || publicKey == null || auth == null) return;
				var data = util.getRegGroup(e.Message, "\"data\":\"(.+?)\"");
				if (data == null) return;
				try {
					var dec = pc.decrypt(data, privateKey, publicKey, auth);
					util.debugWriteLine("dec " + dec);
					if (dec.IndexOf(".onairs\",\"") == -1 ||
					   	dec == null) return;
					
					var items = getItem(dec);
					if (items != null) check.foundLive(items);
					else {
						var gir = new GetItemRetryPr(dec, this);
						Task.Factory.StartNew(() => {
							for (var i = 0; i < 10; i++) {
				         		Thread.Sleep(5000);
				         		items = gir.getItem();
				         		if (items == null) continue;
				         		check.foundLive(items);
				         		break;
							}
						});
					}
					
				} catch (Exception ee) {
					util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
					return;
				}
			}
			
		}
		private bool sendEndpoint(string endpoint) {
			
			if (check.container == null) {
				check.form.addLogText("Cookieが見つからなかったためブラウザプッシュ通知の登録ができませんでした");
				return false;
			}
			var cookies = check.container.GetCookies(new Uri("https://public.api.nicovideo.jp/v1/nicopush/webpush/endpoints.json"));
			if (cookies == null) {
				check.form.addLogText("送信先のCookieが見つからなかったためブラウザプッシュ通知の登録ができませんでした");
				return false;
			} else if (cookies["user_session"] == null || string.IsNullOrEmpty(cookies["user_session"].Value)) {
				check.form.addLogText("送信先との接続に使うCookieが見つからなかったためブラウザプッシュ通知の登録ができませんでした");
				return false;
			}
			//POST /v1/nicopush/webpush/endpoints.json HTTP/1.1
			//Host: public.api.nicovideo.jp
			//User-Agent: Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:65.0) Gecko/20100101 Firefox/65.0
			//Accept: application/json
			//Accept-Language: ja,en-US;q=0.7,en;q=0.3
			//Accept-Encoding: gzip, deflate, br
			//Referer: https://account.nicovideo.jp/my/account
			//x-request-with: https://account.nicovideo.jp/my/account
			//x-frontend-id: 8
			//content-type: application/json
			//Origin: https://account.nicovideo.jp
			//Content-Length: 431
			//Connection: keep-alive
			//Pragma: no-cache
			//Cache-Control: no-cache
			
			
			var sendAuth = Convert.ToBase64String(pc.generateAuth());
			var _pub = Convert.ToBase64String(publicKey);
			var param = "{\"clientapp\":\"nico_account_webpush\",\"endpoint\":{\"endpoint\":\"" + endpoint + "\",\"auth\":\"" + sendAuth + "\",\"p256dh\":\"" + _pub + "\"}}";
			var url = "https://public.api.nicovideo.jp/v1/nicopush/webpush/endpoints.json";
			util.debugWriteLine("param " + param);
			util.debugWriteLine("send register cookie " + check.container.GetCookieHeader(new Uri(url)));
			try {
				
				
				
				//var handler = new System.Net.Http.HttpClientHandler();
				//handler.UseCookies = true;
				//handler.CookieContainer = check.container;
				//handler.Proxy = null;
				
				//var http = new System.Net.Http.HttpClient(handler);
				//http.DefaultRequestHeaders.Referrer = new Uri(url);
				
				
				//var content = new System.Net.Http.FormUrlEncodedContent(_param);
				//var postDataBytes = content.ReadAsByteArrayAsync().Result;
				
				byte[] postDataBytes = Encoding.ASCII.GetBytes(param);
				
				
				var req = (HttpWebRequest)WebRequest.Create(url);
				req.Method = "POST";
				req.Proxy = null;
				req.CookieContainer = check.container;
				req.Referer = "https://account.nicovideo.jp/my/account";
				req.ContentLength = postDataBytes.Length;
				
				req.ContentType = "application/json";
				req.Headers.Add("x-request-with", "https://account.nicovideo.jp/my/account");
				req.Headers.Add("x-frontend-id", "8");
				req.Headers.Add("Accept-Encoding", "gzip,deflate");
				req.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
				//req.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:74.0) Gecko/20100101 Firefox/74.0";
				//req.Headers.Add("Accept-Encoding", "gzip, deflate, br");
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
				using (var resStream = new StreamReader(getResStream)) {
					foreach (var h in res.Headers) util.debugWriteLine("header " + h + " " + res.Headers[h.ToString()]);
					var resStr = resStream.ReadToEnd();
					util.debugWriteLine("nico send post res " + resStr);
					if (resStr == null) {
						check.form.addLogText("ブラウザプッシュ通知の登録時に正常な応答がありませんでした");
					}
					if (resStr.IndexOf("\"status\":200") > -1) {
						auth = Convert.FromBase64String(sendAuth);
						config.set("pushAuth", sendAuth);
						return true;
					} else {
						check.form.addLogText("ブラウザプッシュ通知の登録が正常に行えませんでした" + resStr);
					}
				}
				
			} catch (Exception e) {
				check.form.addLogText("ブラウザプッシュ通知の登録中に問題が発生しました" + e.Message + e.StackTrace + e.Source + e.TargetSite);
				util.debugWriteLine(e.Message+e.StackTrace);
				
			}
			
			return false;
		}
		public List<RssItem> getItem(string dec) {
			
			//com "{\"title\":\"すのーまんさんが生放送を開始\",\"body\":\"名作探訪録 で、「【GB】ポケットキョロちゃん【初見プレイ】」を放送\",\"icon\":\"https://secure-dcdn.cdn.nimg.jp/nicoaccount/usericon/3122/31221850.jpg?1496114222\",\"data\":{\"on_click\":\"http://live.nicovideo.jp/watch/lv318353209?from=webpush&_topic=live_user_program_onairs\",\"created_at\":\"2019-02-06T09:00:05.738+09:00\",\"ttl\":600,\"log_params\":{\"content_type\":\"live.user.program.onairs\",\"content_ids\":\"lv318353209\"}}}";
			//ch "{\"title\":\"KawaiianTVが生放送を開始\",\"body\":\"～夢アド可鈴の深堀り情報番組！？～ YUMEステーション #62\",\"icon\":\"https://secure-dcdn.cdn.nimg.jp/comch/channel-icon/128x128/ch2601967.jpg?1548247262\",\"data\":{\"on_click\":\"http://live.nicovideo.jp/watch/lv318248031?from=webpush&_topic=live_channel_program_onairs\",\"created_at\":\"2019-02-06T19:30:10.941+09:00\",\"ttl\":600,\"log_params\":{\"content_type\":\"live.channel.program.onairs\",\"content_ids\":\"lv318248031\"}}}";
			
			try {
				var isCom = dec.IndexOf("\"content_type\":\"live.user.program.onairs\"") > -1;
				var isJikken = dec.IndexOf("\"content_type\":\"live.user.program.cas.onairs\"") > -1;
				string title, lvid, thumbnail, dt, comName, hostName;//
				hostName = null;
				
				lvid = util.getRegGroup(dec, "\"content_ids\":\"(lv\\d+)\"");
				thumbnail = util.getRegGroup(dec, "\"icon\":\"(.+?)\"");
				dt = util.getRegGroup(dec, "\"created_at\":\"(.+?)\"");
				if (dt != null && DateTime.Parse(dt) < startTime && !bool.Parse(config.get("IsStartTimeAllCheck")))
					return new List<RssItem>();
				if (isCom || isJikken) {
					title = util.getRegGroup(dec, "\"body\":\".+? で、*「(.+?)」を放送\"");
					comName = util.getRegGroup(dec, "\"body\":\"(.+?) で、*「");
					hostName = util.getRegGroup(dec, "\"title\":\"(.+?)さんが生放送(\\(実験放送\\))*を開始\"");
					
				} else {
					title = util.getRegGroup(dec, "\"body\":\"(.+?)\",\"icon\"");
					comName = util.getRegGroup(dec, "\"title\":\"(.+?)が生放送を開始\"");
				}
				
				var hg = new namaichi.rec.HosoInfoGetter();
				var r = hg.get(lvid, check.container);
				//description = hg.description;
				
				if (!r) {
					hg.description = hg.userId = hg.communityId = "";
					hg.tags = new string[]{};
					check.form.addLogText("プッシュ通知から取得した放送のページが取得できませんでした " + lvid);
					util.debugWriteLine("push page error !r " + lvid);
					return null;
				}
				if (hg.isClosed) 
					return new List<RssItem>();
				
				util.debugWriteLine("description " + hg.description);
				util.debugWriteLine("userId " + hg.userId);
				util.debugWriteLine("userName " + hostName);
				
				if (title == null || lvid == null || thumbnail == null ||
				    	dt == null || comName == null || hg.communityId == null ||
				    	hg.tags == null || hg.description == null ||
				    	(isCom && (hostName == null || hg.userId == null))) {
					check.form.addLogText("push error " + dec);
					util.debugWriteLine("push error null " + dec);
					return null;
					
				}
//				util.debugWriteLine("communityId " + hg.communityId);
//				util.debugWriteLine("tags " + string.Join(", ", hg.tags));
//				util.debugWriteLine("userid " + hg.userId);
//				util.debugWriteLine("description " + hg.description);
				
				var i = new RssItem(title, lvid, dt, hg.description, comName, hg.communityId, hostName, hg.thumbnail, hg.isMemberOnly.ToString(), "", hg.isPayment);
				i.setUserId(hg.userId);
				i.setTag(hg.tags);
				i.category = hg.category;
				i.type = hg.type;
				i.pubDateDt = DateTime.Parse(dt);
				var ret = new List<RssItem>();
				ret.Add(i);
				return ret;
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
			return null;
		}
		public void stop() {
			isRetry = false;
			try {
				ws.Close();
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
		}
		public void clearConfigSetting() {
			var n = new string[] {"pushPri", "pushPub", 
					"pushAuth", "pushUa", "pushChId"};
			var resetDict = new Dictionary<string, string> ();
			foreach (var _n in n) resetDict.Add(_n, "");
			config.saveFromForm(resetDict);
		}
		
	}
	class GetItemRetryPr {
		private string dec;
		private PushReceiver pr;
		public GetItemRetryPr(string dec, PushReceiver pr) {
			this.dec = dec;
			this.pr = pr;
		}
		public List<RssItem> getItem() {
			return pr.getItem(dec);
		}
	}
	
}

/*
\"{\"title\":\"すのーまんさんが生放送を開始\",\"body\":\"名作探訪録 で、「【GB】ポケットキョロちゃん【初見プレイ】」を放送\",\"icon\":\"https://secure-dcdn.cdn.nimg.jp/nicoaccount/usericon/3122/31221850.jpg?1496114222\",\"data\":{\"on_click\":\"http://live.nicovideo.jp/watch/lv318353209?from=webpush&_topic=live_user_program_onairs\",\"created_at\":\"2019-02-06T09:00:05.738+09:00\",\"ttl\":600,\"log_params\":{\"content_type\":\"live.user.program.onairs\",\"content_ids\":\"lv318353209\"}}}\";


Cookieの取得に成功しました
push error {"title":"ﾃﾛｯﾌﾟ奨励さんが生放送(実験放送)を開始","body":"201111111111 で「【チカチカ注意】休憩と言う名の超絶放置をしながらネタを貼る放送【ミュート推奨】」を放送","icon":"https://secure-dcdn.cdn.nimg.jp/nicoaccount/usericon/966/9662936.jpg?1337479306","data":{"on_click":"https://cas.nicovideo.jp/lv318396546?from=webpush&_topic=live_user_program_cas_onairs","created_at":"2019-02-08T14:00:07.509+09:00","ttl":600,"log_params":{"content_type":"live.user.program.cas.onairs","content_ids":"lv318396546"}}}
push error {"title":"ふじさわさんが生放送(実験放送)を開始","body":"藤沢牧場 で「きょうのりりっくのていてん　2月8日　夜」を放送","icon":"https://secure-dcdn.cdn.nimg.jp/nicoaccount/usericon/77/770606.jpg?1539041196","data":{"on_click":"https://cas.nicovideo.jp/lv318401367?from=webpush&_topic=live_user_program_cas_onairs","created_at":"2019-02-08T15:51:14.889+09:00","ttl":600,"log_params":{"content_type":"live.user.program.cas.onairs","content_ids":"lv318401367"}}}
*/