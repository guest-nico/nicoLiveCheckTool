/*
 * Created by SharpDevelop.
 * User: ajkkh
 * Date: 2023/08/07
 * Time: 18:14
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using namaichi.utility;

namespace namaichi.alart
{
	/// <summary>
	/// Description of PushReceiverCurl.
	/// </summary>
	public class PushReceiverCurl : PushReceiver
	{
		private IntPtr easy = IntPtr.Zero;
		
		public PushReceiverCurl(Check check, config.config config) : 
				base(check, config)
		{
		}
		override public bool connect() {
			string mes = "";
			if (!util.libcurlWsDllCheck(out mes)) {
				check.form.formAction(() => 
						check.form.showMessageBox("フォルダ内にDLLファイルが存在していませんでした。 「.net4.0環境用/XP用ライブラリ(XPの場合、このフォルダの中身も上のフォルダへ)」フォルダ内の全てのDLLファイルをEXEファイルと同じフォルダに配置する必要があります"));
				return false;                      
			}
			if (!util.vcr140Check(out mes)) {
				check.form.formAction(() => 
						check.form.showMessageBox("Windows XPでブラウザプッシュ通知を受信するにはVisual Studio 2015 の Visual C++ 再頒布可能パッケージが必要です。https://www.microsoft.com/ja-jp/download/details.aspx?id=48145"));
				//return false;                      
			}
			while (isRetry) {
				try {
					var r = connectCore();
					if (!r) {
						Thread.Sleep(60000);
					}
				} finally {
					releaseHandle();
				}
				
			}
			return true;
		}
		private bool connectCore() {
			lock(this) {
				var  isPass = (TimeSpan.FromSeconds(5) > (DateTime.Now - lastWebsocketConnectTime));
				if (isPass) 
					Thread.Sleep(5000);
				lastWebsocketConnectTime = DateTime.Now;
			}
			
			try {
				var url = "wss://push.services.mozilla.com";
				
				easy = Curl.curl_easy_init();
				
				if (easy == IntPtr.Zero) {
					check.form.addLogText("ライブラリよりブラウザプッシュ通知の受信を開始できませんでした");
					return false;
				}
				util.debugWriteLine("curl push connect  ");
				
				Curl.curl_easy_setopt(easy, CURLoption.CURLOPT_URL, url);
				Curl.curl_easy_setopt(easy, CURLoption.CURLOPT_SSL_VERIFYPEER, 0);
				
				var headers = new List<KeyValuePair<string, string>>();
				headers.Add(new KeyValuePair<string, string>("Sec-WebSocket-Protocol", "push-notification"));
				headers.Add(new KeyValuePair<string, string>("Ssc-WebSocket-Version", "13"));
				
				Curl.curl_easy_setopt(easy, CURLoption.CURLOPT_CONNECT_ONLY, 2L);
				
				var code = Curl.curl_easy_perform(easy);
				util.debugWriteLine("curl ws connect code " + code);
				if(code != CURLcode.CURLE_OK) {
					util.debugWriteLine("curl easy error " + Curl.curl_easy_strerror(code));
					check.form.addLogText("ライブラリよりブラウザプッシュ通知の通信を開始できませんでした");
					if (easy != IntPtr.Zero)
						Curl.curl_easy_cleanup(easy);
					Thread.Sleep(5000);
					return false;
				} else {
					Thread.Sleep(1000);
					int outN = 0;
					//string uaid = null;
					var mes = (uaid == null) ?
							"{\"messageType\":\"hello\",\"broadcasts\":null,\"use_webpush\":true}"
							: "{\"messageType\":\"hello\",\"broadcasts\":null,\"use_webpush\":true,\"uaid\":\"" + uaid + "\"}";
					var sendCode = Curl.curl_ws_send_wrap(easy, mes, mes.Length, out outN, 0, (int)curlWsFlags.CURLWS_TEXT);
					util.debugWriteLine("curl ws sendCode " + sendCode + " " + mes);
					while (isRetry) {
						if (DateTime.Now > lastWebsocketConnectTime + TimeSpan.FromMinutes(5)) {
							releaseHandle();
							lastWebsocketConnectTime = DateTime.Now;
							util.debugWriteLine("curl ws reconnect");
							break;
						}
						IntPtr recvPtr = IntPtr.Zero;
						try {
							uint recvN = 0;
							Thread.Sleep(1000);
							var wsFramePtr = IntPtr.Zero;
							var recvBytes = new byte[3000];
							
							CURLcode recvCode;
							recvPtr = Curl.curl_ws_recv_wrap(easy, out recvCode, out recvN);
							util.debugWriteLine("curl ws recvCode " + recvCode);
							
							var recvNI = (int)recvN;
							Marshal.Copy(recvPtr, recvBytes, 0, recvNI);
							var recvS = Encoding.ASCII.GetString(recvBytes, 0, recvNI);
							if (recvS == null || recvS.Length == 0) continue;
							onMessageReceiveCore(recvS);
						} catch (Exception e) {
							util.debugWriteLine(e.Message + e.Source + e.StackTrace);
						} finally {
							if (recvPtr != IntPtr.Zero) Curl.memFree(recvPtr);
						}
						
					}
			    }
				//Curl.curl_easy_cleanup(easy);
				
				return true;
				
			} catch (Exception ee) {
				util.debugWriteLine("push connect exception " + ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
				return false;
			}
		}
		override protected void wsSend(string mes) {
			if (easy == IntPtr.Zero) {
				util.debugWriteLine("curl ws send no easy " + mes);
				return;
			}
			int outN = 0;
			var sendCode = Curl.curl_ws_send_wrap(easy, mes, mes.Length, out outN, 0, (int)curlWsFlags.CURLWS_TEXT);
			util.debugWriteLine("curl ws send code " + sendCode + " " + mes);
		}
		new public void stop() {
			isRetry = false;
			releaseHandle();
		}
		void releaseHandle() {
			try {
				lock(this) {
					if (easy != IntPtr.Zero) {
						Curl.curl_easy_cleanup(easy);
						easy = IntPtr.Zero;
					}
				}
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
		}
	}
}
