/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2018/09/24
 * Time: 18:24
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace namaichi.rec
{
	/// <summary>
	/// Description of Reservation.
	/// </summary>
	public class Reservation
	{
		CookieContainer cc;
		string lv;
		config.config cfg;
		DateTime dt;
		
		string useUrl = "https://live.nicovideo.jp/api/timeshift.ticket.use";
		//string reservationsUrl = "https://live.nicovideo.jp/api/timeshift.reservations";
		public string delLog = null;
		
		public Reservation(CookieContainer cc, string lv, DateTime dt, config.config cfg)
		{
			this.cc = cc;
			this.lv = lv;
			this.dt = dt;
			this.cfg = cfg;
		}
		/*
		public string reserve() {
			var id = util.getRegGroup(lv, "(\\d+)");
			//var url = "https://live.nicovideo.jp/api/watchingreservation?mode=confirm_watch_my&vid=" + id + "&next_url&analytic";
			var getMode = "watchNum";
			var url = "";
			if (getMode == "watchNum") {
				url = "https://live.nicovideo.jp/api/watchingreservation?mode=watch_num&vid=" + id + "&next_url=gate%2F" + lv + "&analytic=gate_modal_0_official_" + lv + "_closed";
			} else {
 
			}
 			
			var res = util.getPageSource(url, cc, null, false, 3000);
			if (res == null) return "no";
			var mode = "";
			if (res.IndexOf("watching_reservation_regist") > -1) mode = "watching_reservation_regist";
			if (res.IndexOf("regist_finished") > -1) mode = "regist_finished";
						
			var token = util.getRegGroup(res, "(ulck_\\d+)");
			if (token == null) {
				if (res.IndexOf("申し込み期限切れです。") > -1) return "申し込み期限切れです。";
				if (res.IndexOf("既に、視聴済みです。") > -1) return "既に、視聴済みです。";
				if (res.IndexOf("タイムシフトの予約上限に達しました。") > -1) return "タイムシフトの予約上限に達しました。";
				if (res.IndexOf("既に予約済みです。") > -1) return "既に予約済みです。";
				//moveWatch
				return "予約できませんでした。";
			}
			while (true) {
				var wrTask = watchingReservation(token, id, mode);
				wrTask.Wait();
				var wrRet = wrTask.Result;
				util.debugWriteLine(wrRet);
				if (wrRet.IndexOf("を削除して、新しくタイムシフト予約をします。") > -1) {
					return "予約リストが一杯です。";
				}
//				if (wrRet.IndexOf("Nicolive.TimeshiftActions.moveWatch") > -1) {
//					return "ok";
//				}
				if (wrRet.IndexOf("システムエラーが") > -1 ||
				    wrRet.IndexOf("nicolive_video_response status=\"fail\"") > -1) {
					res = util.getPageSource(url, cc, null, false, 3000);
					util.debugWriteLine(res);
					if (res == null) return "no";
					if (res.IndexOf("watching_reservation_regist") > -1) mode = "watching_reservation_regist";
					if (res.IndexOf("regist_finished") > -1) mode = "regist_finished";
					
					token = util.getRegGroup(res, "(ulck_\\d+)");
					if (token == null) {
						if (res.IndexOf("申し込み期限切れです。") > -1) return "申し込み期限切れです。";
						if (res.IndexOf("既に、視聴済みです。") > -1) return "既に、視聴済みです。";
						if (res.IndexOf("タイムシフトの予約上限に達しました。") > -1) return "タイムシフトの予約上限に達しました。";
						if (res.IndexOf("既に予約済みです。") > -1) return "既に予約済みです。";
						//moveWatch
						return "予約できませんでした。";
					}
				}
				if (wrRet.IndexOf("nicolive_video_response status=\"ok\"") > -1 ||
				    wrRet.IndexOf("タイムシフト予約リストに登録しました") > -1) return "ok";
				if (wrRet.IndexOf("regist_finished") > -1) mode = "regist_finished";
				Thread.Sleep(1000);
			}
			//return "ok";
		}
		*/
		/*
		async private Task<string> watchingReservation(string token, string id, string mode) {
			util.debugWriteLine("watching reservation post " + token + " " + mode);
			try {
				var handler = new System.Net.Http.HttpClientHandler();
				handler.UseCookies = true;
				handler.CookieContainer = cc;
				var http = new System.Net.Http.HttpClient(handler);
				handler.UseProxy = true;
				handler.Proxy = null;
				
				
				//var contentStr = "mode=auto_register&vid=" + id + "&token=" + token + "&_=";
				//util.debugWriteLine("reservation " + contentStr);
				
				var _content = new List<KeyValuePair<string, string>>();
				if (mode == "watching_reservation_regist") {
					_content.Add(new KeyValuePair<string, string>("mode", "auto_register"));
					_content.Add(new KeyValuePair<string, string>("vid", id));
					_content.Add(new KeyValuePair<string, string>("token", token));
					_content.Add(new KeyValuePair<string, string>("_", ""));
				} else if (mode == "regist_finished") {
					_content.Add(new KeyValuePair<string, string>("accept", "true"));
					_content.Add(new KeyValuePair<string, string>("mode", "use"));
					_content.Add(new KeyValuePair<string, string>("vid", id));
					_content.Add(new KeyValuePair<string, string>("token", token));
					_content.Add(new KeyValuePair<string, string>("", ""));
				}
				//var content = new System.Net.Http.StringContent(contentStr);
				var content = new System.Net.Http.FormUrlEncodedContent(_content);
				//content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

				http.Timeout = TimeSpan.FromSeconds(3);
				var url = "https://live.nicovideo.jp/api/watchingreservation";
				var _t = http.PostAsync(url, content);
				_t.Wait();
				var _res = _t.Result;
				var res = await _res.Content.ReadAsStringAsync();
	//			var a = _res.Headers;
				
	//			if (res.IndexOf("login_status = 'login'") < 0) return null;
				
//				cc = handler.CookieContainer;
				
				return res;
//				return cc;
			} catch (Exception e) {
				util.debugWriteLine("get watching exception " + e.Message+e.StackTrace);
				return null;
			}
		}
		*/
		public string live2Reserve(bool isOverwrite) {
			//var r = getLive2ReserveRes(isOverwrite);
			var r = getLive2ReserveRes2();
			if (r == "タイムシフトの予約上限に達しました。" 
			    	&& isOverwrite) {
				return overwriteReserve2();
				//return r;
			}
			return r;
		}
		/*
		private string getLive2ReserveRes(bool isOverwrite) {
			var id = util.getRegGroup(lv, "(\\d+)");
			var header = getLive2ReserveHeader(id);
			
			var reserveData = Encoding.ASCII.GetBytes("vid=" + id + "&overwrite=" + 
					(isOverwrite ? "1" : "0"));
			
			var r = util.sendRequest(reservationsUrl, header, reserveData, "POST", true);
			try {
				using (var rs = r.GetResponseStream())
				using (var sr = new StreamReader(rs)) {
					var res = sr.ReadToEnd();
					if (res.IndexOf("\"status\":200") > -1) return "ok";
					if (res.IndexOf("Timeshift_reservation was duplicated") > -1)
						return "既に予約済みです。";
					else if (res.IndexOf("Timeshift_reservation was over_use") > -1) {
						return "タイムシフトの予約上限に達しました。";
					} else if (res.IndexOf("Timeshift_reservation can overwrite") > -1) {
						return "タイムシフトの予約上限に達しました。";
					} else if (res.IndexOf("Timeshift_reservation was expired") > -1)
						return "タイムシフトの申し込み期限切れです。";
					else return "予約できませんでした" + util.getRegGroup(res, "\"description\":\"(.+?)\"");
				}
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				return "予約できませんでした " + e.Message;
			}
		}
		*/
		public bool useLive2Reserve() {
			var id = util.getRegGroup(lv, "(\\d+)");
			
			var useData = Encoding.ASCII.GetBytes("vid=" + id);
			var header = getLive2ReserveHeader(id);
			var res = util.postResStr(useUrl, header, useData);
			if (res == null) return false;
			return res.IndexOf("status\":200") > -1;
		}
		private Dictionary<string, string> getLive2ReserveHeader(string id) {
			if (id.StartsWith("lv")) id = util.getRegGroup(lv, "(\\d+)");
			var header = new Dictionary<string, string>();
			header.Add("Cookie", cc.GetCookieHeader(new Uri(useUrl)));
			header.Add("Accept", "application/json, text/plain, */*");
			header.Add("Content-Type", "application/x-www-form-urlencoded");
			header.Add("Referer", "https://live.nicovideo.jp/watch/lv" + id);
			header.Add("Origin", "https://live.nicovideo.jp");
			header.Add("User-Agent", util.userAgent);
			header.Add("X-Frontend-Id", "9");
			return header;
		}
		private Dictionary<string, string> getLive2ReserveHeader2(string id) {
			if (id.StartsWith("lv")) id = util.getRegGroup(lv, "(\\d+)");
			var url = "https://live2.nicovideo.jp/api/v2/programs/" + lv + "/timeshift/reservation";
			var header = new Dictionary<string, string>();
			header.Add("Cookie", cc.GetCookieHeader(new Uri(url)));
			header.Add("Accept", "application/json");
			header.Add("Referer", "https://live.nicovideo.jp/");
			header.Add("Origin", "https://live.nicovideo.jp");
			header.Add("User-Agent", util.userAgent);
			header.Add("X-Frontend-Id", "9");
			return header;
		}
		/*
		private bool overwriteReserve() {
			var res = util.getPageSource("https://live.nicovideo.jp/my_timeshift_list", cc);
			if (res == null) return false;
			var lastColumn = util.getRegGroup(res, ".+(<div class=\"column\">[\\d\\D]+)");
			if (lastColumn == null) return false;
			var vid = util.getRegGroup(lastColumn, "vid.+?(\\d+)");
			var ulck = util.getRegGroup(res, "(ulck_\\d+)");
			var url = "https://live.nicovideo.jp/my?delete=timeshift&vid=" + vid + "&confirm=" + ulck;
			res = util.getPageSource(url, cc);
			return res != null;
		}
		*/
		
		private string overwriteReserve2() {
			try {
				var res = util.getPageSource("https://live.nicovideo.jp/embed/timeshift-reservations", cc);
				if (res == null) return "予約の上書き中、予約リストの取得に失敗しました";
				var props = util.getRegGroup(res, "data-props=\"(.+)\"");
				if (props == null) return "予約の上書き中、予約リスト内の情報の取得に失敗しました";
				props = props.Replace("&quot;", "\"");
				
				var oldId = ""; 
				var oldTitle = "";
				var timeList = new List<DateTime>();
				var lvList = new List<DateTime>();
				var timeM = new Regex("\"openTime\":(.+?)").Matches(props);
				var lvM = new Regex("nicoliveProgramId\":\"(lv\\d+)").Matches(props);
				var titleM = new Regex("title\":\"(.+?)\"").Matches(props);
				
				if (timeM.Count == 0 || lvM.Count == 0)
					return "予約リストをの上書き中、予約リストから放送情報の取得に失敗しました";
				if (timeM.Count != lvM.Count || timeM.Count != lvM.Count) {
					oldId = util.getRegGroup(res, "nicoliveProgramId&quot;:&quot;(lv\\d+)");
					//return "予約の上書き中、予約リストの読み取りに失敗しました";
				} else {
					var minI = 0;
					var minT = DateTime.MaxValue;
					for (var i = 0; i < lvM.Count; i++) {
						//var t = DateTime.Parse(timeM[i].Groups[1].Value);
						var t = util.getUnixToDatetime(long.Parse(timeM[i].Groups[1].Value));
						if (t < minT) {
							minI = i;
							minT = t; 
						}
					}
					oldId = lvM[minI].Groups[1].Value;
					oldTitle = titleM[minI].Groups[1].Value;
				}
				
				//var oldId = util.getRegGroup(res, "programId&quot;:&quot;(lv\\d+)");
				if (string.IsNullOrEmpty(oldId)) return "予約の上書き中、予約中の放送IDの取得に失敗しました";
				var delUrl = "https://live2.nicovideo.jp/api/v2/timeshift/reservations?programIds=" + oldId; 
				var header = getLive2ReserveHeader2(lv);
				var r = util.sendRequest(delUrl, header, null, "DELETE", true);
				if (r == null) return "予約の上書き中、以前の予約の取り消しに失敗しました";
				using (var rr = r.GetResponseStream())
				using (var sr = new StreamReader(rr)) {
					var delRes = sr.ReadToEnd();
					if (delRes.IndexOf("\"status\":200") == -1) return "予約の上書き中、以前の予約の取り消しに失敗しました";
					util.debugWriteLine("reserve del " + oldId);
				}
				delLog = oldId + " " + oldTitle;
				return getLive2ReserveRes2();
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace);
				return "予約の上書き中、何らかの問題が発生しました。 " + e.Message + e.Source + e.StackTrace;
			}
		}
		
		public string getLive2ReserveRes2() {
			try {
				var url = "https://live2.nicovideo.jp/api/v2/programs/" + lv + "/timeshift/reservation";
				var header = getLive2ReserveHeader2(lv);
				string d = null;
				var res = util.postResStr(url, header, d, true, "POST");
				if (res.IndexOf("\"status\":200") > -1) return "ok";
				if (res.IndexOf("\"errorCode\":\"OVER_USE\"") > -1)
					return "タイムシフトの予約上限に達しました。";
				if (res.IndexOf("\"errorCode\":\"PROGRAM_NOT_FOUND\"") > -1)
					return "放送が予約に対応していない等、何らかの理由により予約できませんでした";
				if (res.IndexOf("\"errorCode\":\"DUPLICATED\"") > -1)
					return "既に予約済みです。";
				if (res.IndexOf("\"errorCode\":\"EXPIRED_GENERAL\"") > -1)
					return "申込み期限切れです";
				return null;
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace);
				return null;
			}
			
		}
	}
	 
}
