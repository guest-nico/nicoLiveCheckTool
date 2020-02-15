/*
 * Created by SharpDevelop.
 * User: user
 * Date: 2019/03/04
 * Time: 0:33
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using McsProto;
//using Google.Protobuf;

using namaichi.info;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace namaichi.alart
{
	/// <summary>
	/// Description of AppPushReceiver.
	/// </summary>
	public class AppPushReceiver
	{
		private Check check;
		private config.config config;

		private SslStream sslStream = null;
		private bool isRetry = true;
		private DateTime startTime = DateTime.Now;

		public AppPushReceiver(Check check, config.config config)
		{
			this.check = check;
			this.config = config;
		}
		public void start()
		{
			var id = config.get("appPushId");
			var token = config.get("appPushToken");

			if (id == "" || token == "")
			{

				id = token = null;

				clearConfigSetting();

				if (!getOkIdToken(out id, out token))
				{
					check.form.addLogText("スマホ通知のIDが取得できませんでした");
					return;
				}
				config.set("appPushId", id);
				config.set("appPushToken", token);

			}
			connect(id, token);
		}
		public void stop()
		{
			util.debugWriteLine("app push stop");
			isRetry = false;
			if (sslStream == null) return;
			try
			{
				sslStream.Close();
			}
			catch (Exception e)
			{
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
		}
		private bool getOkIdToken(out string id, out string token)
		{
			util.debugWriteLine("app push getOkIdToken");
			id = token = null;

			string _id, _token;
			if (!checkin(out _id, out _token)) return false;
			var pushToken = getToken(_id, _token);
			if (pushToken == null) return false;
			if (!sendTokenNico2(pushToken)) return false;

			id = _id;
			token = _token;
			return true;
		}
		private bool checkin(out string id, out string token)
		{
			util.debugWriteLine("app push checkin");
			id = token = null;
			try
			{
				var url = "http://android.clients.google.com/checkin";

				var cr = new CheckinRequest();

				//cr.Imei = "109269993813709";// # "".join(random.choice("0123456789") for _ in range(15))
				cr.Imei = "000000000000000";// # "".join(random.choice("0123456789") for _ in range(15))
				cr.androidId = 0;


				var build = new CheckinRequest.Checkin.Build();
				build.Fingerprint = "google/razor/flo:5.0.1/LRX22C/1602158:user/release-keys";
				build.Hardware = "flo";
				build.Brand = "google";
				build.Radio = "FLO-04.04";
				//build.bootloader = "FLO-04.04"
				build.clientId = "android-google";

				var checkinM = new CheckinRequest.Checkin();
				checkinM.build = build;
				cr.checkin = checkinM;

				cr.checkin.lastCheckinMs = 0;

				cr.Locale = "en";
				//cr.loggingId = random.getrandbits(63)
				cr.loggingId = 1;
				//cr.marketCheckin
				//cr.macAddress.append("".join(random.choice("ABCDEF0123456789") for _ in range(12)))
				//cr.MacAddress.Add("47D435B5CCCA");
				cr.macAddresses.Add("111111111111");
				//cr.meid = "".join(random.choice("0123456789") for _ in range(14))
				cr.Meid = "01234567890123";
				cr.accountCookies.Add("");
				cr.timeZone = "GMT";
				cr.Version = 3;
				cr.otaCerts.Add("--no-output--"); // 71Q6Rn2DDZl1zPDVaaeEHItd
												  //cr.serial 
												  //cr.esn = "".join(random.choice("ABCDEF0123456789") for _ in range(8))
				cr.Esn = "01234567";
				//cr.deviceConfiguration
				cr.macAddressTypes.Add("wifi");
				cr.Fragment = 0;
				//cr.username
				cr.userSerialNumber = 0;

				//var postDataBytes = cr.ToByteArray();
				byte[] postDataBytes;
				using (var ms = new MemoryStream())
				{
					Serializer.Serialize(ms, cr);
					postDataBytes = ms.ToArray();
				}

				//foreach (var _p in postDataBytes) Debug.Write((char)_p);
				//util.debugWriteLine("");
				//foreach (var _p in postDataBytes) Debug.Write(_p + " ");
				//util.debugWriteLine("");
				//var reqCoded = new CodedOutputStream(
				/*
				byte[] seby;
				using( MemoryStream stream = new MemoryStream())
		        {
		            cr.WriteTo(stream);
		            seby = stream.ToArray();
		        }
				*/
				var headers = new Dictionary<string, string>() {
					{"contenttype", "application/x-protobuffer"},
					{"useragent", "Android-Checkin/2.0 (vbox86p JLS36G); gzip"}
				};
				var rb = util.postResBytes(url, headers, postDataBytes);
				if (rb == null)
				{
					util.debugWriteLine("checkin post error");
					check.form.addLogText("スマホ通知のチェックインのPOSTに失敗しました");
					return false;
				}

				using (var ms = new MemoryStream(rb))
				{
					var checkinRes = new CheckinResponse();
					checkinRes = Serializer.Deserialize<CheckinResponse>(ms);
					util.debugWriteLine("androidId " + checkinRes.androidId);
					util.debugWriteLine("securityToken " + checkinRes.securityToken);
					id = checkinRes.androidId.ToString();
					token = checkinRes.securityToken.ToString();
					return true;
				}
				/*
				var parser = new Google.Protobuf.MessageParser<CheckinResponse>(() => new CheckinResponse());
				using (var ms = new MemoryStream(rb)) {
					var checkinRes = parser.ParseFrom(ms);
					util.debugWriteLine("androidId " + checkinRes.AndroidId);
		            util.debugWriteLine("securityToken " + checkinRes.SecurityToken);
		            id = checkinRes.AndroidId.ToString();
		            token = checkinRes.SecurityToken.ToString();
		            return true;
				}
				*/
			}
			catch (Exception e)
			{
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				check.form.addLogText("スマホ通知のチェックインの取得中にエラーが発生しました" + e.Message + e.Source + e.StackTrace + e.TargetSite);
				return false;
			}
		}
		private string getToken(string androidId, string securityToken)
		{
			util.debugWriteLine("app push getToken " + androidId + " " + securityToken);
			try
			{
				var headers = new Dictionary<string, string>(){
					{"Authorization","AidLogin " + androidId + ":" + securityToken},
					{"contenttype", "application/x-www-form-urlencoded"}
				};
				string param;
				var isNicoCas = true;
				if (isNicoCas)
				{
					param = "app=jp.co.dwango.nicocas";
					param += "&sender=13323994513";
				}
				else
				{
					param = "app=jp.nicovideo.android";
					param += "&sender=812879448480"; //niconicoアプリ
													 //param += "&cert=8b25f54d3ab466dba54bef9302f27acdfdd70cb1";
													 //param += "&cert=0005f54d3ab466dba54bef9302f27acdfdd70cb1";

				}
				param += "&device=" + androidId;
				param += "&app_ver=107";
				param += "&gcm_ver=15090013";
				//param += "&X-appid=$randomString";
				param += "&X-scope=GCM";
				param += "&X-app_ver_name=4.48.0";
				//param += "&info=sxu5CRtfHmsco0hB01boBVwFAxLXkBY";

				byte[] postDataBytes = Encoding.ASCII.GetBytes(param);
				util.debugWriteLine(param);

				var url = "https://android.clients.google.com/c2dm/register3";
				var r = util.postResStr(url, headers, postDataBytes);
				util.debugWriteLine(r);
				var token = new Regex("token=(.+)").Match(r).Groups[1].Value;
				util.debugWriteLine(token);

				if (token == null)
				{
					check.form.addLogText("スマホ通知のトークンの取得に失敗しました");
				}
				return token;
			}
			catch (Exception e)
			{
				util.debugWriteLine("gettoken error " + e.Message + e.Source + e.StackTrace + e.TargetSite);
				check.form.addLogText("スマホ通知のトークンの取得中にエラーが発生しました" + e.Message + e.Source + e.StackTrace + e.TargetSite);
				return null;
			}
		}
		private bool sendTokenNico(string pushToken)
		{
			util.debugWriteLine("app push sendTokenNico " + pushToken);
			try
			{
				if (check.container == null)
				{
					check.form.addLogText("Cookieが確認できなかったためスマホ通知のトークンを送信できませんでした");
					return false;
				}
				var urlCookie = check.container.GetCookieHeader(new Uri("https://live2.nicovideo.jp")) + ";";
				var userSession = util.getRegGroup(urlCookie, "user_session=(.+?);");

				var url = "https://api.gadget.nicovideo.jp/notification/clientapp/registration";

				//ok
				var headers = new Dictionary<string, string>() {
					{"Content-Type", "application/x-www-form-urlencoded; charset=utf-8"},
					{"User-Agent", "Niconico/1.0 (Linux; U; Android 5.1.1; ja-jp; nicoandroid SM-G9550) Version/5.16.0"},
					{"Cookie", "SP_SESSION_KEY=" + userSession},
					//{"Cookie2", "$Version=1"},
					{"Accept-Language", "ja-jp"},
					{"X-Nicovideo-Connection-Type", "wifi"},
					{"X-Frontend-Id", "1"},
					{"X-Frontend-Version", "5.16.0"},
					{"X-Os-Version", "5.1.1"},
					{"X-Request-With", "nicoandroid"},
					{"X-Model-Name", "dreamqltecmcc"},
					{"Connection", "Keep-Alive"},
					{"Accept-Encoding", "gzip"},
				};



				var param = "token=" + userSession;
				param += "&registerId=" + pushToken;

				byte[] postDataBytes = Encoding.ASCII.GetBytes(param);
				var r = util.postResStr(url, headers, postDataBytes);
				if (r == null) check.form.addLogText("スマホ通知のトークンの送信に失敗しました");
				util.debugWriteLine(r);
				return r == "{}";
			}
			catch (Exception e)
			{
				util.debugWriteLine("gettoken error " + e.Message + e.Source + e.StackTrace + e.TargetSite);
				check.form.addLogText("スマホ通知のトークンの送信中にエラーが発生しました" + e.Message + e.Source + e.StackTrace + e.TargetSite);
				return false;
			}
		}
		private bool sendTokenNico2(string pushToken)
		{
			util.debugWriteLine("app push sendTokenNico " + pushToken);
			try
			{
				if (check.container == null)
				{
					check.form.addLogText("Cookieが確認できなかったためスマホ通知のトークンを送信できませんでした");
					return false;
				}

				//ok
				var urlCookie = check.container.GetCookieHeader(new Uri("https://live2.nicovideo.jp")) + ";";
				var userSession = util.getRegGroup(urlCookie, "user_session=(.+?);");
				var headers = new Dictionary<string, string>() {
					{"Content-Type", "application/json; charset=UTF-8"},
					{"User-Agent", "nicocas-Android/3.3.0"},
					{"Cookie", "user_session=" + userSession},
					{"X-Frontend-Id", "90"},
					{"X-Frontend-Version", "3.3.0"},
					{"X-Os-Version", "22"},
					{"X-Model-Name", "dream2qltechn"},
					{"X-Connection-Environment", "wifi"},
					{"Connection", "Keep-Alive"},
					//{"Accept-Encoding", "gzip"},
				};
				var acPassHeaders = new Dictionary<string, string>() {
					//{"Content-Type", "application/json; charset=UTF-8"},
					{"User-Agent", "okhttp/3.10.0"},
					{"Cookie", "user_session=" + userSession},
					{"X-Frontend-Id", "90"},
					{"X-Frontend-Version", "3.3.0"},
					//{"X-Os-Version", "22"},
					{"X-Request-With", "dream2qltechn"},
					//{"X-Connection-Environment", "wifi"},
					{"Connection", "Keep-Alive"},
					//{"Accept-Encoding", "gzip"},
				};

				//var url = "https://account.nicovideo.jp/api/v1/users/account_passport";
				//var r = util.postResStr(url, acPassHeaders, null);
				//util.debugWriteLine("account_passport " + r);

				var url = "https://api.cas.nicovideo.jp/v1/services/ex/app/nicocas_android/installations";

				var param = "{\"token\": \"" + pushToken + "\"}";
				byte[] postDataBytes = Encoding.ASCII.GetBytes(param);
				var res = util.postResStr(url, headers, postDataBytes);
				if (res == null) check.form.addLogText("スマホ通知のトークンの送信に失敗しました");
				util.debugWriteLine("app push send token " + res);

				url = "https://api.cas.nicovideo.jp/v1/services/ex/app/nicocas_android/notification/blocks";
				param = "{\"all\": [\"nicocas\"],\"channel\": [],\"user\": []}";
				postDataBytes = Encoding.ASCII.GetBytes(param);
				var _res = util.sendRequest(url, headers, postDataBytes, "DELETE");
				if (res == null) check.form.addLogText("スマホ通知のブロック設定の送信に失敗しました");
				if (res != null)
				{
					using (var getResStream = _res.GetResponseStream())
					using (var resStream = new System.IO.StreamReader(getResStream))
					{
						var _r = resStream.ReadToEnd();
						util.debugWriteLine("app push blocks delete " + _r);
						if (_r == null || _r.IndexOf("200") == -1)
							check.form.addLogText("スマホ通知のブロック設定に失敗しました " + _r);
					}
				}
				else util.debugWriteLine("app push blocks delete null");

				url = "https://api.cas.nicovideo.jp/v1/services/ex/app/nicocas_android/notification/time";
				param = "{\"status\": \"disabled\",\"time\": {\"end\": \"0:00\", \"start\": \"7:00\"}}";
				postDataBytes = Encoding.ASCII.GetBytes(param);
				_res = util.sendRequest(url, headers, postDataBytes, "PUT");
				if (res == null) check.form.addLogText("スマホ通知の時間設定の送信に失敗しました");
				if (res != null)
				{
					using (var getResStream = _res.GetResponseStream())
					using (var resStream = new System.IO.StreamReader(getResStream))
					{
						var _r = resStream.ReadToEnd();
						util.debugWriteLine("app push time put " + _r);
						if (_r == null || _r.IndexOf("200") == -1)
							check.form.addLogText("スマホ通知の時間設定に失敗しました" + _r);
					}
				}
				else util.debugWriteLine("app push time put null");

				return res != null;
			}
			catch (Exception e)
			{
				util.debugWriteLine("gettoken error " + e.Message + e.Source + e.StackTrace + e.TargetSite);
				check.form.addLogText("スマホ通知のトークンの送信中にエラーが発生しました" + e.Message + e.Source + e.StackTrace + e.TargetSite);
				return false;
			}
		}
		public void connect(string androidId, string securityToken)
		{
			util.debugWriteLine("connect appPush " + androidId + " " + securityToken);
			check.form.addLogText("スマホプッシュ通知の受信を開始しました");
			while (isRetry)
			{
				connectMcs(androidId, securityToken);
			}
			check.form.addLogText("スマホプッシュ通知の受信を終了しました");
		}
		public bool connectMcs(string androidId, string securityToken)
		{
			util.debugWriteLine("connectMcs " + androidId + " " + securityToken);
			try
			{
				var lr = new LoginRequest();
				lr.AdaptiveHeartbeat = false;
				lr.auth_service = LoginRequest.AuthService.AndroidId;
				lr.AuthToken = securityToken;
				lr.Id = "android-11";
				lr.Domain = "mcs.android.com";
				lr.DeviceId = "android-" + long.Parse(androidId).ToString("x");
				lr.NetworkType = 1;
				lr.Resource = androidId;
				lr.User = androidId;
				lr.UseRmq2 = true;
				lr.AccountId = long.Parse(androidId);
				lr.ReceivedPersistentIds.Add("");  //# possible source of error
				var setting = new Setting();
				setting.Name = "new_vc";
				setting.Value = "1";
				lr.Settings.Add(setting);
				//var x = lr.ToByteArray();

				byte[] x;
				using (var ms = new MemoryStream())
				{
					Serializer.Serialize(ms, lr);
					x = ms.ToArray();
				}


				using (var client = new TcpClient("mtalk.google.com", 5228))
				using (sslStream = new SslStream(client.GetStream(), false, delegate { return true; }))
				{
					sslStream.AuthenticateAsClient("mtalk.google.com");
					//var _buf = VarintBitConverter.GetVarintBytes((int)x.Length);
					var _buf = VarintBitConverter.GetVarintBytes((uint)x.Length);
					/*
				    var num = x.Length;
				    var _buf = new List<byte>();
					while (true) {
						var towrite = num & 0x7f;
						num >>= 7;
						if (num == 1) 
							_buf.Add((byte)(towrite | 0x80));
						else {
							_buf.Add((byte)towrite);
							break;
						}
					}
					*/
					//util.debugWriteLine(_buf.ToArray() + " x len " + x.Length);
					util.debugWriteLine(_buf + " x len " + x.Length);
					sslStream.Write(new byte[] { 41, 2 });
					sslStream.Write(_buf);
					sslStream.Write(x);
					sslStream.Flush();

					var version = sslStream.ReadByte();
					util.debugWriteLine(version);

					Task.Factory.StartNew(() =>
					{
						while (isRetry)
						{
							//Thread.Sleep(15000);

							Thread.Sleep(60 * 60 * 1000);
							util.debugWriteLine(DateTime.Now.ToString() + " ping " + sslStream.GetHashCode());
							try
							{
								sslStream.Write(new byte[] { 0x07, 0x0e, 0x10, 0x01, 0x1a, 0x00, 0x3a, 0x04, 0x08, 0x0d, 0x12, 0x00, 0x50, 0x03, 0x60, 0x00 });
								//s.send(b'\x00\x04\x10\x08\x18\x00')
							}
							catch (Exception e)
							{
								util.debugWriteLine("ping error " + e.Message + e.Source + e.StackTrace + e.TargetSite);
								break;
							}
						}
						util.debugWriteLine("ping end " + sslStream.GetHashCode());
					});

					while (isRetry)
					{
						var responseTag = sslStream.ReadByte();

						util.debugWriteLine(DateTime.Now + " resp tag " + responseTag + " " + (char)responseTag);

						var msg = new List<byte>();
						if (responseTag == 0x03 || responseTag == 0x07
								|| responseTag == 0x08)
						{
							//var length = varIntDecodeStream(sslStream);
							var length = 0;
							var _lenBuf = new List<byte>();
							for (var i = 0; i < 10; i++)
							{
								var _slb = sslStream.ReadByte();
								_lenBuf.Add((byte)_slb);
								if (_lenBuf[_lenBuf.Count - 1] > 128) _lenBuf.Add((byte)sslStream.ReadByte());
								try
								{
									length = VarintBitConverter.ToInt32(_lenBuf.ToArray()) * 1;
									var length2 = VarintBitConverter.ToInt16(_lenBuf.ToArray()) * 1;
									var length3 = VarintBitConverter.ToInt64(_lenBuf.ToArray()) * 1;
									var length4 = VarintBitConverter.ToUInt16(_lenBuf.ToArray()) * 1;
									var length5 = VarintBitConverter.ToUInt32(_lenBuf.ToArray()) * 1;
									length = (int)length5;
									break;
								}
								catch (Exception e)
								{
									util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
								}
							}
							if (length == 0) return false;
							util.debugWriteLine("calc len " + length);


							while (msg.Count < length)
							{
								if (msg.Count != 0) util.debugWriteLine("recv msg 2shuume");

								byte[] readbuf = new byte[1000];
								var i = sslStream.Read(readbuf, 0, length - msg.Count);
								for (var j = 0; j < i; j++) msg.Add(readbuf[j]);

								util.debugWriteLine("recv  allLen " + length + " msg len " + msg.Count + " " + msg);
							}
							util.debugWriteLine("calc len " + length + " msg len " + msg.Count + " msg " + msg);
							//foreach (var bb in msg) Debug.Write((char)bb);
							//util.debugWriteLine("");
							//foreach (var bb in msg) Debug.Write(bb.ToString("x") + " ");
							util.debugWriteLine("");
						}

						if (responseTag == 0x03)
						{
							var lresp = new LoginResponse();
							using (var ms = new MemoryStream(msg.ToArray()))
							{
								lresp = Serializer.Deserialize<LoginResponse>(ms);
							}
							/*
							using (var ms = new MemoryStream(msg.ToArray()))
							using (var cs = new  CodedInputStream(ms)) {
								lresp.MergeFrom(cs);
								
							}
							*/
							util.debugWriteLine("RECV LOGIN RESP " + lresp);
						}
						else if (responseTag == 0x07)
						{
							//var lresp = LoginResponse.Parser.ParseFrom(msg.ToArray());
							var lresp = new IqStanza();

							using (var ms = new MemoryStream(msg.ToArray()))
							{
								lresp = Serializer.Deserialize<IqStanza>(ms);
							}
							/*
							using (var ms = new MemoryStream(msg.ToArray()))
							using (var cs = new CodedInputStream(ms)) {
								//lresp.MergeFrom(cs);
								try {
									var iqstanza = IqStanza.Parser.ParseFrom(msg.ToArray());
								} catch (Exception e) {
									util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
								}
	
								//util.debugWriteLine(iqstanza);
								//LoginResponse.Descriptor.
							} 
							*/
							//iqs = mcs_pb2.IqStanza();
							//iqs.ParseFromString(msg);
							util.debugWriteLine("RECV IQ  id " + lresp);// + lresp.Id + " time " + lresp.ServerTimestamp + " streamid " + lresp.StreamId + " ");

						}
						else if (responseTag == 0x08)
						{
							try
							{
								DataMessageStanza lresp;
								using (var ms = new MemoryStream(msg.ToArray()))
								{
									lresp = Serializer.Deserialize<DataMessageStanza>(ms);
								}

								//var lresp = DataMessageStanza.Parser.ParseFrom(msg.ToArray());

								//dms = mcs_pb2.DataMessageStanza();
								//dms.ParseFromString(msg);
								util.debugWriteLine("RECV DATA MESSAGE " + lresp);//lresp.Id + " time " + lresp.ServerTimestamp + " streamid " + lresp.StreamId + " ");
																				  //var lvid = util.getRegGroup(lresp.AppData.ToString(), "\"lvid\"[\\s\\S]+(lv\\d+)");
																				  //var lvid = util.getRegGroup(lresp.AppData.ToString(), "\"program_id\":\"(lv\\d+)");
								var d = "";
								foreach (var a in lresp.AppDatas) d += a.Value;
								//var lvid = util.getRegGroup(d, "program_id\\\\\":\\\\\"(lv\\d+)"); google protobuf
								var lvid = util.getRegGroup(d, "program_id\":\"(lv\\d+)"); //google protobuf 
								if (lvid != null)
								{
									util.debugWriteLine("appData lvid " + lvid);
									var items = getNicoCasItem(lvid, lresp);
									if (items != null)
									{
										check.foundLive(items);
									}
									else
									{
										var gir = new GetItemRetryApr(lvid, lresp, this);
										Task.Factory.StartNew(() =>
										{
											for (var i = 0; i < 10; i++)
											{
												Thread.Sleep(5000);
												items = gir.getItem();
												if (items == null) continue;
												check.foundLive(items);
												break;
											}
										});
									}
								}



							}
							catch (Exception ee)
							{
								util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
							}

						}
						else if (responseTag == 0x04)
						{
							break;
						}
						else
						{
							if (responseTag == 0x00)
								break;
							util.debugWriteLine("unknown response: " + responseTag.ToString());
						}


					}
				}
				util.debugWriteLine("closed");
				return true;
			}
			catch (Exception e)
			{
				util.debugWriteLine("mcs connect error " + e.Message + e.Source + e.StackTrace + e.TargetSite);
				return false;
			}
		}

		public List<RssItem> getItem(string lvid, McsProto.DataMessageStanza msg)
		{
			try
			{
				string title, thumbnail, comName, hostName, description;
				DateTime dt = util.getUnixToDatetime(msg.Sent / 1000);
				if (dt < startTime) return null;
				hostName = "";

				var hg = new namaichi.rec.HosoInfoGetter();
				var r = hg.get(lvid, check.container);

				bool isCom;
				if (!r)
				{
					check.form.addLogText("スマホプッシュ通知から取得した放送のページが取得できませんでした " + lvid);
					util.debugWriteLine("app push page error !r " + lvid);
					return null;

					hg.description = hg.userId = hg.communityId = hg.thumbnail = "";
					hg.tags = new String[] { };
					var reg = new Regex("\\[生放送開始\\](.+?)さんが「(.+?)」を開始しました。");
					var m = reg.Match(msg.AppDatas.ToString());
					if (m.Length != 0)
					{
						hostName = m.Groups[1].Value;
						isCom = true;
						comName = "";
					}
					else
					{
						reg = new Regex("\\[生放送開始\\](.+?)「(.+?)」を開始しました。");
						m = reg.Match(msg.AppDatas.ToString());
						comName = m.Groups[1].Value;
						isCom = false;
						hostName = "";
					}
					title = m.Groups[2].Value;
				}
				else
				{
					if (hg.type == "community" || hg.type == "user")
					{
						var reg = new Regex("\\[生放送開始\\](.+?)さんが「(.+?)」を開始しました。");
						var d = "";
						foreach (var a in msg.AppDatas) d += a.Value;
						var m = reg.Match(d);
						hostName = m.Groups[1].Value;
						comName = (hg.group != null) ? hg.group : "";
						title = m.Groups[2].Value;
						isCom = true;
					}
					else
					{
						var reg = new Regex("\\[生放送開始\\](.+?)「(.+?)」を開始しました。");
						var d = "";
						foreach (var a in msg.AppDatas) d += a.Value;
						var m = reg.Match(d);
						comName = m.Groups[1].Value;
						title = m.Groups[2].Value;
						isCom = false;
						if (!string.IsNullOrEmpty(hg.userName)) hostName = hg.userName;
					}

				}


				util.debugWriteLine("description " + hg.description);
				util.debugWriteLine("userId " + hg.userId);
				util.debugWriteLine("userName " + hostName);
				util.debugWriteLine("comName " + comName);
				//thumbnail = "";

				if (title == null || lvid == null || hg.thumbnail == null ||
						dt == DateTime.MinValue || comName == null || hg.communityId == null ||
						hg.tags == null || hg.description == null ||
						(isCom && (hostName == null || hg.userId == null)))
				{
					check.form.addLogText("app push error " + msg);
					util.debugWriteLine("app push error nullinfo " + msg);
					return null;

				}

				var i = new RssItem(title, lvid, dt.ToString(), hg.description, comName, hg.communityId, hostName, hg.thumbnail, hg.isMemberOnly.ToString(), "");
				i.setUserId(hg.userId);
				i.setTag(hg.tags);
				i.category = hg.category;
				i.type = hg.type;
				var ret = new List<RssItem>();
				ret.Add(i);
				return ret;

			}
			catch (Exception e)
			{
				util.debugWriteLine("app push getitem error " + e.Message + e.Source + e.StackTrace + e.TargetSite);
				return null;
			}
		}
		public List<RssItem> getNicoCasItem(string lvid, McsProto.DataMessageStanza msg)
		{
			try
			{
				string title, thumbnail, comName, hostName, description;
				DateTime dt = util.getUnixToDatetime(msg.Sent / 1000);
				if (dt < startTime || dt < DateTime.Now - TimeSpan.FromMinutes(10)) return null;
				hostName = "";

				var hg = new namaichi.rec.HosoInfoGetter();
				var r = hg.get(lvid, check.container);

				var d = "";
				foreach (var a in msg.AppDatas) d += a.Value;
				var appData = d;
				bool isCom;
				if (!r)
				{
					check.form.addLogText("スマホプッシュ通知から取得した放送のページが取得できませんでした " + lvid);
					util.debugWriteLine("app push page error !r " + lvid);
					return null;

					hg.description = hg.userId = hg.communityId = hg.thumbnail = "";
					hg.tags = new String[] { };


					if (appData.IndexOf("\"user_id\\\":") > -1)
					{
						hostName = util.getRegGroup(appData, "user_name\\\\\":\\\\\"(.+?)\\\\\"");
						isCom = true;
						comName = "";
					}
					else
					{
						//reg = new Regex("\\[生放送開始\\](.+?)「(.+?)」を開始しました。");
						//m = reg.Match(msg.AppData.ToString());
						comName = util.getRegGroup(appData, "channel_name\\\\\":\\\\\"(.+?)\\\\\"");
						isCom = false;
						hostName = "";
					}
					title = util.getRegGroup(appData, "program_title\\\\\":\\\\\"(.+?)\\\\\"");
				}
				else
				{
					if (hg.type == "community" || hg.type == "user")
					{
						//var reg = new Regex("\\[生放送開始\\](.+?)さんが「(.+?)」を開始しました。");
						//var m = reg.Match(msg.AppData.ToString());
						//hostName = util.getRegGroup(appData, "user_name\\\\\":\\\\\"(.+?)\\\\\"");
						hostName = util.getRegGroup(appData, "user_name\":\"(.+?)\"");
						comName = (hg.group != null) ? hg.group : "";
						isCom = true;
					}
					else
					{
						//var reg = new Regex("\\[生放送開始\\](.+?)「(.+?)」を開始しました。");
						//var m = reg.Match(msg.AppData.ToString());
						//comName = util.getRegGroup(appData, "channel_name\\\\\":\\\\\"(.+?)\\\\\"");
						comName = util.getRegGroup(appData, "channel_name\":\"(.+?)\"");
						//comId = util.getRegGroup(appData, "channel_id\\\\\":\\\\\"(.+?)\\\\\"");
						isCom = false;
						if (!string.IsNullOrEmpty(hg.userName)) hostName = hg.userName;
					}
					//title = util.getRegGroup(appData, "program_title\\\\\":\\\\\"(.+?)\\\\\"");
					title = util.getRegGroup(appData, "program_title\":\"(.+?)\"");

				}


				util.debugWriteLine("description " + hg.description);
				util.debugWriteLine("userId " + hg.userId);
				util.debugWriteLine("userName " + hostName);
				util.debugWriteLine("comName " + comName);
				//thumbnail = "";

				if (title == null || lvid == null || hg.thumbnail == null ||
						dt == DateTime.MinValue || comName == null || hg.communityId == null ||
						hg.tags == null || hg.description == null ||
						(isCom && (hostName == null || hg.userId == null)))
				{
					check.form.addLogText("app push error " + msg);
					util.debugWriteLine("app push error nullinfo " + msg);
					return null;

				}

				var i = new RssItem(title, lvid, dt.ToString(), hg.description, comName, hg.communityId, hostName, hg.thumbnail, hg.isMemberOnly.ToString(), "");
				i.setUserId(hg.userId);
				i.setTag(hg.tags);
				i.category = hg.category;
				i.type = hg.type;
				var ret = new List<RssItem>();
				ret.Add(i);
				return ret;

			}
			catch (Exception e)
			{
				util.debugWriteLine("app push getitem error " + e.Message + e.Source + e.StackTrace + e.TargetSite);
				return null;
			}
		}
		public void clearConfigSetting()
		{
			config.set("appPushId", "");
			config.set("appPushToken", "");
		}
	}
	class GetItemRetryApr
	{
		private string lvid;
		private McsProto.DataMessageStanza lresp;
		private AppPushReceiver pr;
		public GetItemRetryApr(string lvid, McsProto.DataMessageStanza lresp, AppPushReceiver pr)
		{
			this.lvid = lvid;
			this.lresp = lresp;
			this.pr = pr;
		}
		public List<RssItem> getItem()
		{
			return pr.getNicoCasItem(lvid, lresp);
		}
	}

}
//RECV DATA MESSAGE { "id": "6D44DD46", "from": "13323994513", "category": "jp.nicovideo.android", "appData": [ { "key": "nx", "value": "{\"type\":\"start_channel_publish\",\"relation\":\"follower\",\"channel_id\":\"ch2604516\",\"program_id\":\"lv322411981\",\"program_title\":\"⛔【限定】[ ASMR/耳舐め ] ハロウィン♡魔女ウィッチがご奉仕♡【実写カメラ】Ear  licking Video Stream\",\"channel_name\":\"all standard is ｃ☆。\",\"channel_icon\":\"https://secure-dcdn.cdn.nimg.jp/comch/channel-icon/128x128/ch2604516.jpg?1572956462\"}" }, { "key": "message", "value": "all standard is ｃ☆。が番組を開始しました" } ], "persistentId": "0:1573920045367966%75f5c14df9fd7ecd", "ttl": 86357, "sent": "1573920045351" }
//RECV DATA MESSAGE { "id": "6D44DD3E", "from": "13323994513", "category": "jp.nicovideo.android", "appData": [ { "key": "nx", "value": "{\"type\":\"start_publish\",\"relation\":\"follower\",\"user_id\":\"14508141\",\"program_id\":\"lv322948514\",\"program_title\":\"真夜中の 【怪談 UMA 怪事件 超常現象 】 鑑賞会\",\"user_name\":\"ジャワ男\",\"user_icon\":\"https://secure-dcdn.cdn.nimg.jp/nicoaccount/usericon/1450/14508141.jpg?1539494321\"}" }, { "key": "message", "value": "ジャワ男さんが番組を開始しました" } ], "persistentId": "0:1573914974863994%75f5c14df9fd7ecd", "ttl": 86400, "sent": "1573914974858" }

/*
 * using (var ms = new MemoryStream())
  {
    Serializer.Serialize(ms, human);
    byte[] bytes = ms.ToArray();
    Console.WriteLine(BitConverter.ToString(bytes));

    // デシリアライズ
    // human = Serializer.Deserialize<Human>(ms);
  }
  
  Task.Factory.StartNew() 
  
 */
