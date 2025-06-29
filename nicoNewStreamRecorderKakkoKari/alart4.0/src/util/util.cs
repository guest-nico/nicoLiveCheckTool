﻿using System;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using Microsoft.Win32;
using System.Diagnostics;
using System.Threading;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Drawing;
using namaichi.alart;
using namaichi.utility;
using Newtonsoft.Json;
using Un4seen.Bass;
using namaichi.config;
using namaichi.info;
using namaichi;

class app {
	public static void Mains(string[] args) {
		string a = util.getRegGroup("as32df5gh", "\\d([^0-9]).(.)", 1);
		Console.WriteLine(a);
		Console.WriteLine(util.getPath());
		Console.WriteLine(util.getTime());
		Console.WriteLine(util.getJarPath());
		Console.WriteLine(util.getOkFileName(".a\\\"aa|a", false));
		//Console.WriteLine(util.getRecFolderFilePath("host", "group", "title", "lvid", "comnum")[0]);
		//Console.WriteLine(util.getRecFolderFilePath("host", "group", "title", "lvid", "comnum")[1]);
	}
}
class util {
	public static string versionStr = "ver0.1.8.10";
	public static string versionDayStr = "2025/06/14";
	public static string osName = null;
	public static string osType = null;
	public static bool isWebRequestOk = false;
	public static bool isShowWindow = true;
	public static bool isStdIO = false;
	public static string[] jarPath = null;
	public static bool isCurl = true;
	
	public static string getRegGroup(string target, string reg, int group = 1, Regex r = null) {
		if (r == null)
			 r = new Regex(reg);
		var m = r.Match(target);
//		Console.WriteLine(m.Groups.Count +""+ m.Groups[0]);
		if (m.Groups.Count>group) {
			return m.Groups[group].ToString();
		} else return null;
	}	
	public static string getPath() {
		string p  = System.IO.Path.GetDirectoryName(
			System.IO.Path.GetFullPath(
			System.Reflection.Assembly.GetExecutingAssembly().Location));
//		Console.WriteLine(p);
		return p;
	}
	public static string getTime() {
		return DateTime.Now.ToString("yyyy\"/\"MM\"/\"dd HH\":\"mm\":\"ss");
		
	}
	public static int getUnixTime() {
		return (int)(((TimeSpan)(DateTime.Now - new DateTime(1970, 1, 1))).TotalSeconds);
	}
	public static int getUnixTime(DateTime dt) {
		return (int)(((TimeSpan)(dt - new DateTime(1970, 1, 1))).TotalSeconds);
	}
	public static String[] getJarPath() {
		if (jarPath != null) return jarPath;
		
		bool isTestMode = false;
		
		if (isTestMode) {	
			return new String[]{"C:\\Users\\pc\\desktop", "util", "exe"};
		} else {
//			string f=Environment.GetCommandLineArgs()[0];
			string f = System.Reflection.Assembly.GetExecutingAssembly().Location;
//			util.debugWriteLine(Environment.GetCommandLineArgs().Length);
			f=System.IO.Path.GetFileName(f);

			string withoutKakutyousi = (f.IndexOf(".") < 0) ? f :
					util.getRegGroup(f,"^(.*)\\.");
			string kakutyousi = (f.IndexOf(".") < 0) ? null :
					util.getRegGroup(f,"^.*\\.(.*)");
			
			util.debugWriteLine(getPath() + " " +withoutKakutyousi+" "+kakutyousi);
			//0-dir 1-withoutKakutyousi 2-kakutyousi
			var ret = new String[]{getPath(), withoutKakutyousi, kakutyousi};
			jarPath = ret;
			return ret;
		}
	}

	
	public static string[] getRecFolderFilePath(string host, 
			string group, string title, string lvId, 
			string communityNum, string userId,  
			bool isTimeShift, 
			long _openTime, AppSettingInfo asi) {
		
		host = getOkArg(host);
		group = getOkArg(group);
		title = getOkArg(title);
		
		
		//string[] jarpath = getJarPath();
//		util.debugWriteLine(jarpath);
		//string dirPath = jarpath[0] + "\\rec\\" + host;
		//string _dirPath = baseDir;
		string dirPath = asi.baseDir;
		
		string sfn = null;
		//if (cfg.get("IscreateSubfolder") == "true") {
		if (bool.Parse(asi.IscreateSubfolder)) {
			sfn = getSubFolderName(host, group, title, lvId, communityNum, userId, asi.subFolderNameType);
			if (sfn.Length > 120) sfn = sfn.Substring(0, 120);
			if (sfn == null) return null;
			dirPath += "/" + sfn;
		}


		var segmentSaveType = "0"; //cfg.get("segmentSaveType");
		//if (cfg.get("EngineMode") != "0" || isRtmp) segmentSaveType = "0";
		
		bool _isTimeShift = false; //isTimeShift;
		//if (cfg.get("EngineMode") != "0") _isTimeShift = false;

		var name = getFileName(host, group, title, lvId, communityNum, _openTime, userId, asi.fileNameType, asi.filenameformat);
		if (name.Length > 200) name = name.Substring(0, 200);
		
		//長いパス調整
		if (name.Length + dirPath.Length > 234) {
			name = lvId;
			if (name.Length + dirPath.Length > 234 && sfn != null) {
				sfn = sfn.Substring(0, 3);
				dirPath = dirPath + "/" + sfn;
								
				if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);
				if (!Directory.Exists(dirPath)) return null;
				
			}
		}
		if (name.Length + dirPath.Length > 234) return new string[]{null, name + " " + dirPath, null};
		
		if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);
		if (!Directory.Exists(dirPath)) return null;
		
		var files = Directory.GetFiles(dirPath);
		//string existFile = null;
		//string existDt = null;
		//string existDtFile = null;
		for (int i = 0; i < 1000000; i++) {
			var fName = dirPath + "/" + name + "_" + ((_isTimeShift) ? "ts" : "") + i.ToString();
			var originName = dirPath + "/" + name;
			util.debugWriteLine(dirPath + " " + fName);
			
			//if (!_isTimeShift) {
				if (segmentSaveType == "0" && isExistAllExt(fName)) continue;
				else if (segmentSaveType == "1") {
					if (Directory.Exists(fName)) continue;
					Directory.CreateDirectory(fName);
					if (!Directory.Exists(fName)) return null;
				}
				
				string[] reta = {dirPath, fName, originName};
				return reta;
			//}
			/*else {
				
				if (segmentSaveType == "0") {
					var _existFile = util.existFile(files, "_ts(_\\d+h\\d+m\\d+s_)*" + i.ToString() + "", name);
					var _existDt = util.existFile(files, "_ts_(\\d+h\\d+m\\d+s)_" + i.ToString() + ".ts", name);
					var reg = "_ts_(\\d+h\\d+m\\d+s)_" + i.ToString() + ".ts";
					if (_existDt != null) {
						existDt = util.getRegGroup(_existDt, "(\\d+h\\d+m\\d+s)");
						existDtFile = _existDt;
					}
					if (_existFile != null) {
						existFile = _existFile;
						continue;
					}
					if (tsConfig.isContinueConcat) {
						if (i == 0 || existDt == null) {
							var firstFile = dirPath + "/" + name + "_ts_0h0m0s_" + i.ToString();
							string[] retb = {dirPath, firstFile, originName};
							return retb;
						} else {
							//fName = dirPath + "/" + name + "_" + ((isTimeShift) ? "ts" : "") + (i - 1).ToString();
//							if (_existDt == null) existFile = dirPath + "/" + name + "_ts_" + existDt + "_" + i.ToString() + ".ts";
//							existFile = Regex.Replace(existDtFile, "\\d+h\\d+m\\d+s", existDt);
							existFile = existDtFile.Substring(0, existDtFile.LastIndexOf("."));
//							existFile = existFile.Substring(0, existFile.Length - 3);
							string[] retc = {dirPath, existFile, originName};
							return retc;
						}
					} else {
						var firstFile = dirPath + "/" + name + "_ts_0h0m0s_" + i.ToString();
						string[] retd = {dirPath, firstFile, originName};
						return retd;
					}
//					continue;
				} else if (segmentSaveType == "1") {
					if (Directory.Exists(fName)) {
						string[] rete = {dirPath, fName, originName};
						return rete;
					} else if (File.Exists(fName)) {
						continue;
					}
					util.debugWriteLine(dirPath + " " + fName);
					Directory.CreateDirectory(fName);
					if (!Directory.Exists(fName)) return null;
					string[] retf = {dirPath, fName, originName};
					return retf;
				}
			}*/
		}
		return null;
	}
	
	public static string getOkFileName(string name, bool isRtmp) {
		if (isRtmp) name = getOkSJisOut(name);
		
		name = name.Replace("\\", "￥");
		name = name.Replace("/", "／");
		name = name.Replace(":", "：");
		name = name.Replace("*", "＊");
		name = name.Replace("?", "？");
		name = name.Replace("\"", "”");
		name = name.Replace("<", "＜");
		name = name.Replace(">", "＞");
		name = name.Replace("|", "｜");
		/*
		string[] replaceCharacter = {"\\", "/", ":", "*", "?", "\"", "<", ">", "|"};
		foreach (string s in replaceCharacter) {
			name = name.Replace(s, "_");
		}
		*/
		return name;
	}
	public static string getOkArg(string arg) {
		arg = getOkFileName(arg, false);
		return arg.Replace("\"", "”");
	}
	
	private static string getSubFolderName(string host, string group, string title, string lvId, string communityNum, string userId, string subDirType) {
		var n = subDirType;
		if (n == null) n = "1";
		if (n == "1") return host;
		else if (n == "2") return userId;
		else if (n == "3") return userId + "_" + host + "";
		else if (n == "4") return group;
		else if (n == "5") return communityNum;
		else if (n == "6") return communityNum + "_" + group + "";
		else if (n == "7") return communityNum + "_" + host + "";
		else if (n == "8") return host + "_" + communityNum + "";
		else return host;
	}
	
	private static string getFileName(string host, string group, string title, string lvId, string communityNum, long _openTime, string hostId, string fileNameType, string fileNameFormat) {
		var n = fileNameType; //cfg.get("fileNameType");
		//var _hiduke = DateTime.Now;
		var _hiduke = getUnixToDatetime(_openTime);
		var month = (_hiduke.Month < 10) ? ("0" + _hiduke.Month.ToString()) : (_hiduke.Month.ToString());
		var day = (_hiduke.Day < 10) ? ("0" + _hiduke.Day.ToString()) : (_hiduke.Day.ToString());
		var hiduke = _hiduke.Year + "年" + month + "月" + day + "日";
		if (n == null) n = "1";
		if (n == "1") return host + "_" + communityNum + "(" + group + ")_" + lvId + "(" + title + ")";
		else if (n == "2") return communityNum + "(" + group + ")_" + host + "_" + lvId + "(" + title + ")";
		
		else if (n == "3") return lvId + "(" + title + ")_" + host + "_" + communityNum + "(" + group + ")";
		else if (n == "4") return host + "_" + group + "(" + communityNum + ")_" + title + "(" + lvId + ")";
		else if (n == "5") return group + "(" + communityNum + ")_" + host + "_" + title + "(" + lvId + ")";
		else if (n == "6") return title + "(" + lvId + ")_" + host + "_" + group + "(" + communityNum + ")";
		else if (n == "7") return hiduke + "_" + host + "_" + group + "(" + communityNum + ")_" + title + "(" + lvId + ")";
		else if (n == "8") return hiduke + "_" + group + "(" + communityNum + ")_" + host + "_" + title + "(" + lvId + ")";
		else if (n == "9") return hiduke + "_" + title + "(" + lvId + ")_" + host + "_" + group + "(" + communityNum + ")";
		//else if (n == "10") return getDokujiSetteiArg(host, group, title, lvId, communityNum, fileNameFormat, _openTime, hostId,   "0");
		else if (n == "10") return getDokujiSetteiFileName(host, group, title, lvId, communityNum, fileNameFormat, _hiduke, hostId);
		else return host + "_" + communityNum + "(" + group + ")_" + lvId + "(" + title + ")";
	}
	public static string getDokujiSetteiFileName(string host, string group, string title, string lvId, string communityNum, string format, DateTime _openTime, string hostId) {
		var type = format;
		if (type == null) return "";
		//var dt = DateTime.Now;
		var dt = _openTime;
		var yearBuf = ("0000" + dt.Year.ToString());
		var year2 = yearBuf.Substring(yearBuf.Length - 2);
		var year4 = yearBuf.Substring(yearBuf.Length - 4);
		var monthBuf = "00" + dt.Month.ToString();
		var month = monthBuf.Substring(monthBuf.Length - 2);
		var dayBuf = "00" + dt.Day.ToString();
		var day = dayBuf.Substring(dayBuf.Length - 2);
		
		var week = dt.ToString("ddd");
		var hour = dt.ToString("HH");
		var minute = dt.ToString("mm");
		var second = dt.ToString("ss");
		
		type = type.Replace("{Y}", year4);
		type = type.Replace("{y}", year2);
		type = type.Replace("{M}", month);
		type = type.Replace("{D}", day);
		type = type.Replace("{W}", week);
		type = type.Replace("{h}", hour);
		type = type.Replace("{m}", minute);
		type = type.Replace("{s}", second);
		type = type.Replace("{0}", lvId);
		type = type.Replace("{1}", title);
		type = type.Replace("{2}", host);
		type = type.Replace("{3}", communityNum);
		type = type.Replace("{4}", group);
		type = type.Replace("{5}", hostId);
		type = getOkArg(type);
		return type;
		
	}
	public static string getDokujiSetteiArg(string host, string group, 
			string title, string lvId, string communityNum, 
			string format, DateTime _openTime, string hostId, 
			string us, AppSettingInfo asi, MainForm form) {
		try {
			var type = format;
			if (string.IsNullOrEmpty(type)) type = "{url}";
			if (type.IndexOf("{url}") == -1 && type.IndexOf("{nourl}") == -1)
				type = "{url} " + type;
			if (type.IndexOf("{nourl}") > -1) {
				type = type.Replace("{url}", " ");
				type = type.Replace("{nourl}", " ");
			}
			type = type.Replace("{nourl}", " ");
			
			var b = "";
			//var m = new Regex(@"[^\s""]+|""([^""]*)""").Matches(type);
			var m = new Regex(@"[^\s""=]+=""[^""]*""|[^\s""]+|""[^""]*""").Matches(type);
			foreach (Match _m in m) {
				if (b != "") b += " ";			
				
				var s = _m.Groups[0].Value;
				if (s.StartsWith("\"")) {
					b += s;
					continue;
				}
				if (s.IndexOf("{1}") > -1 ||
				    	s.IndexOf("{2}") > -1 ||
				    	s.IndexOf("{4}") > -1)
					b += "\"" + s + "\"";
				else b += s;
			}
			type = b;
			                  
			//var dt = DateTime.Now;
			var dt = _openTime;
			var yearBuf = ("0000" + dt.Year.ToString());
			var year2 = yearBuf.Substring(yearBuf.Length - 2);
			var year4 = yearBuf.Substring(yearBuf.Length - 4);
			var monthBuf = "00" + dt.Month.ToString();
			var month = monthBuf.Substring(monthBuf.Length - 2);
			var dayBuf = "00" + dt.Day.ToString();
			var day = dayBuf.Substring(dayBuf.Length - 2);
			
			var week = dt.ToString("ddd");
			var hour = dt.ToString("HH");
			var minute = dt.ToString("mm");
			var second = dt.ToString("ss");
			
			type = type.Replace("{Y}", year4);
			type = type.Replace("{y}", year2);
			type = type.Replace("{M}", month);
			type = type.Replace("{D}", day);
			type = type.Replace("{W}", week);
			type = type.Replace("{h}", hour);
			type = type.Replace("{m}", minute);
			type = type.Replace("{s}", second);
			type = type.Replace("{0}", lvId);
			type = type.Replace("{1}", getOkArg(title));
			type = type.Replace("{2}", getOkArg(host));
			type = type.Replace("{3}", communityNum);
			type = type.Replace("{4}", getOkArg(group));
			type = type.Replace("{5}", hostId);
			type = type.Replace("{url}", "https://live.nicovideo.jp/watch/" + lvId);
			type = type.Replace("{us}", us);
			type = type.Replace("{noarg}", " ");
			if (type.IndexOf("{file}") > -1) 
				type = type.Replace("{file}", "\"" + getRecFolderFilePath(getOkArg(host), getOkArg(group), getOkArg(title), lvId, communityNum, hostId, false, util.getUnixTime(_openTime), asi)[1] + (asi.ext != "" ? ("." + asi.ext) : "") + "\"");
			return type;
		} catch (Exception e) {
			util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			form.addLogText(e.Message + e.Source + e.StackTrace + e.TargetSite);
			return "";
		}
	}
	
	public static string getFileNameTypeSample(string filenametype, bool isArgMode = true) {
		if (isArgMode)
			return getDokujiSetteiArg("放送者名", "チャンネル名", "タイトル", "lv12345", "ch9876", filenametype, DateTime.Now, "123", "user_session_12345_abcde", new AppSettingInfo(), null) ;
		else return util.getDokujiSetteiFileName("放送者名", "チャンネル", "タイトル", "lv12345", "ch9876", filenametype, DateTime.Now, "1000");
	}
	public static string getOkCommentFileName(config cfg, string fName, string lvid, bool isTimeShift, bool isRtmp) {
		var kakutyousi = (cfg.get("IsgetcommentXml") == "true") ? ".xml" : ".json";
		var engineMode = cfg.get("EngineMode");
		if (cfg.get("segmentSaveType") == "0" || engineMode != "0" || isRtmp) {
			//renketu
			if (isTimeShift && engineMode == "0" && !isRtmp) {
				var time = getRegGroup(fName, "(_\\d+h\\d+m\\d+s_)");
				fName = fName.Replace(time, "");
			}
			util.debugWriteLine("comment file path " + fName + kakutyousi);
			return fName + kakutyousi;
		} else {
			
			var name = util.getRegGroup(fName, ".+/(.+)");
			if (fName.Length + name.Length > 245) name = lvid;
			util.debugWriteLine("comment file path " + fName + "/" + name + kakutyousi);
			return fName + "/" + name + kakutyousi;
		}
	}
	private static bool isExistAllExt(string fName) {
		var ext = new string[] {".ts", ".xml", ".flv", ".avi", ".mp4",
				".mov", ".wmv", ".vob", ".mkv", ".mp3",
				".wav", ".wma", ".aac", ".ogg", ""};
		foreach (var e in ext) 
			if (File.Exists(fName + e)) return true;
		return false;
	}
	public static string incrementRecFolderFile(string recFolderFile) {
		if (recFolderFile.EndsWith("xml") || recFolderFile.EndsWith("json")) {
			var r = new Regex("(\\d+)\\.(xml|json)$");
			var m = r.Match(recFolderFile);
			if (m == null || m.Length <= 0) return null;//rp.getRecFilePath()[1];
			
			for (int i = int.Parse(m.Groups[1].Value); i < 10000; i++) {
				var _new = (i + 1).ToString() + "." + m.Groups[2];
				var _ret = r.Replace(recFolderFile, _new);
				if (File.Exists(_ret)) continue;
				return _ret;
			}
		} else {
			var r = new Regex("(\\d+)$");
			var m = r.Match(recFolderFile);
			if (m == null || m.Length <= 0) return null;//rp.getRecFilePath()[1];
			
			for (int i = int.Parse(m.Groups[1].Value); i < 10000; i++) {
				var _new = (int.Parse(m.Groups[1].Value) + 1).ToString();
				var _ret = r.Replace(recFolderFile, _new);
				if (File.Exists(_ret)) continue;
				return _ret;
			}
		}
		return null;
	}
	public static string userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/114.0.0.0 Safari/537.36";
	public static string getPageSource(string _url, CookieContainer container = null, string referer = null, bool isFirstLog = true, int timeoutMs = 5000) {
	
		util.debugWriteLine("access__ getpage Source 1" + _url);
		if (isCurl) {
			var curlH = getHeader(container, referer, _url);
			var curlR = new Curl().getStr(_url, curlH, CurlHttpVersion.CURL_HTTP_VERSION_1_1, "GET", null, false);
			return curlR;
		}
		timeoutMs = 5000;
		
		for (int i = 0; i < 1; i++) {
			try {
				var isWebRequest = true;
				if (isWebRequest) {
	//				util.debugWriteLine("getpage 00");
					var req = (HttpWebRequest)WebRequest.Create(_url);
					req.Proxy = null;
					req.AllowAutoRedirect = true;
		//			req.Headers = getheaders;
	//				util.debugWriteLine("getpage 03");
					if (referer != null) req.Referer = referer;
	//				util.debugWriteLine("getpage 04");
					if (container != null) req.CookieContainer = container;
	//				util.debugWriteLine("getpage 05");
					req.UserAgent = "NicoLiveCheckTool " + versionStr + " guestnicon@gmail.com";
					req.Headers.Add("Accept-Encoding", "gzip,deflate");
					req.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
					//ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | (SecurityProtocolType)0x00000C00 | (SecurityProtocolType)0x00000300;

					req.Timeout = timeoutMs;
	//				util.debugWriteLine("getpage 0");
					var res = (HttpWebResponse)req.GetResponse();
	//				util.debugWriteLine("getpage 1");
					using (var dataStream = res.GetResponseStream())
					using (var reader = new StreamReader(dataStream)) {
						
						/*
						var resStrTask = reader.ReadToEndAsync();
						if (!resStrTask.Wait(5000)) return null;
						string resStr = resStrTask.Result;
						*/
		//				util.debugWriteLine("getpage 3");
						var resStr = reader.ReadToEnd();
		//				util.debugWriteLine("getpage 4");
						
		//				getheaders = res.Headers;
						//dataStream.Dispose();
						//reader.Dispose();
		
						return resStr;
					}
				}
			} catch (Exception e) {
				System.Threading.Tasks.Task.Factory.StartNew(() => {
					util.debugWriteLine("getpage error " + _url + e.Message+e.StackTrace);
				});
	//				System.Threading.Thread.Sleep(3000);
				continue;
			}
		}
			
		return null;
	}
	public static byte[] getFileBytes(string url, CookieContainer container) {
		util.debugWriteLine("access__ getFileBytes" + url);
//		var a = container.GetCookieHeader(new Uri(_url));
		//util.debugWriteLine("getfilebyte " + url);
		for (int i = 0; i < 1; i++) {
			try {
				var req = (HttpWebRequest)WebRequest.Create(url);
				req.Proxy = null;
				req.AllowAutoRedirect = true;
				req.Timeout = 2000;
	//			req.Headers = getheaders;
//				if (referer != null) req.Referer = referer;
				if (container != null) req.CookieContainer = container;
				var res = (HttpWebResponse)req.GetResponse();
				byte[] ret = null;
				using (var dataStream = res.GetResponseStream()) {
					
					//test
					var isMs = true;
					if (isMs) {
						using (var ms = new MemoryStream()) {
							dataStream.CopyTo(ms);
							ret = ms.ToArray();
						}
					} else {
		//				var reader = new StreamReader(dataStream);
						byte[] b = new byte[10000000];
						int pos = 0;
						var r = 0;
						while ((r = dataStream.Read(b, pos, 1000000)) > 0) {
		//					if (dataStream.Read(b, (int)j, (int)dataStream.Length) == 0) break;
		//					j = dataStream.Position;
							pos += r;
						}
						Array.Resize(ref b, pos);
						ret = b;
					}
				}
				return ret;
			} catch (Exception e) {
				System.Threading.Tasks.Task.Factory.StartNew(() => {
					util.debugWriteLine("getfile error " + url + e.Message+e.StackTrace);
				});
//				System.Threading.Thread.Sleep(3000);
				continue;
			}
		}
		return null;
	}
	public static string getResStr(string url, Dictionary<string, string> headers, bool isGetErrorMessage = false) {
		string d = null; 
		return postResStr(url, headers, d, isGetErrorMessage, "GET");
	}
	public static string postResStr(string url, Dictionary<string, string> headers, byte[] content, bool isGetErrorMessage = false, string method = "POST") {
		var d = content == null ? null : Encoding.UTF8.GetString(content);
		return postResStr(url, headers, d, isGetErrorMessage, method);
	}
	public static string postResStr(string url, Dictionary<string, string> headers, string content, bool isGetErrorMessage = false, string method = "POST") {
		try {
			if (isCurl) {
				
				var r = new Curl().getStr(url, headers, CurlHttpVersion.CURL_HTTP_VERSION_1_1, method, content, false, true);
				return r;
			} else {
				var d = content == null ? null :  Encoding.UTF8.GetBytes(content);
				var res = sendRequest(url, headers, d, method, isGetErrorMessage);
				if (res == null) {
					debugWriteLine("postResStr res null");
					return null;
				}
				debugWriteLine(res.StatusCode + " " + res.StatusDescription);
				
				using (var getResStream = res.GetResponseStream())
				using (var resStream = new System.IO.StreamReader(getResStream)) {
					var resStr = resStream.ReadToEnd();
					return resStr;
				}
			}
		} catch (Exception ee) {
			debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			return null;
		}
	}
	public static byte[] postResBytes(string url, Dictionary<string, string> headers, byte[] content, string method = "POST") {
		try {
			if (isCurl && false) {
				//var d = content == null ? null : Encoding.UTF8.GetString(content);
				var r = new Curl().getBytes(url, headers, CurlHttpVersion.CURL_HTTP_VERSION_2TLS, method, content, false);
				return r;
			} else {
				var res = sendRequest(url, headers, content, method);
			
				debugWriteLine(res.StatusCode + " " + res.StatusDescription);
				
				//var resStream = res.GetResponseStream();
				var a = res.StatusCode;
				using (var resStream = res.GetResponseStream()) {
					var buf = new List<byte>();
					for (var k = 0; k < 10; k++) {
						var b = new byte[1000];
						var c = resStream.Read(b, 0, b.Length);
						if (c == 0) break;
						for (var j = 0; j < c; j++) buf.Add(b[j]);
					}
					return buf.ToArray();
				}
			}
		} catch (Exception ee) {
			debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			return null;
		}
	}
	public static HttpWebResponse sendRequest(string url, Dictionary<string, string> headers, byte[] content, string method, bool isGetErrorMessage = false, CookieContainer cc = null) {
		util.debugWriteLine("access__ sendRequest" + url);
		try {
			var req = (HttpWebRequest)WebRequest.Create(url);
			req.Method = method;
			req.Proxy = null;
			//req.Headers.Add("Accept-Encoding", "gzip,deflate");
			//req.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
			req.Timeout = 5000;
			req.CookieContainer = cc;
			
			foreach (var h in headers) {
				if (h.Key.ToLower().Replace("-", "") == "contenttype")
					req.ContentType = h.Value;
				else if (h.Key.ToLower().Replace("-", "") == "useragent")
					req.UserAgent = h.Value;
				else if (h.Key.ToLower().Replace("-", "") == "connection")
					req.KeepAlive = h.Value.ToLower().Replace("-", "") == "keepalive";
				else if (h.Key.ToLower() == "accept")
					req.Accept = h.Value;
				else if (h.Key.ToLower().Replace("-", "") == "referer")
						req.Referer = h.Value;
				else req.Headers.Add(h.Key, h.Value);
			}
			
			if (content != null) {
				using (var stream = req.GetRequestStream()) {
					try {
						stream.Write(content, 0, content.Length);
					} catch (Exception ee) {
			       		debugWriteLine(ee.Message + " " + ee.StackTrace + " " + ee.Source + " " + ee.TargetSite);
			       	}
				}
			}
//					stream.Close();

			return (HttpWebResponse)req.GetResponse();
		} catch (Exception ee) {
			debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			if (isGetErrorMessage) {
				try {
					if (ee is WebException) {
						var r = (HttpWebResponse)((WebException)ee).Response;
						return r;
					}
				} catch (Exception eee) {
					util.debugWriteLine(eee.Message + eee.Source + eee.StackTrace + eee.TargetSite);
				}
			}
			return null;
		}
	}
	/*
	public static bool isEndedProgram(string lvid, CookieContainer container, bool isSub) {
		var url = "https://live2.nicovideo.jp/watch/" + lvid;
		
		var res = util.getPageSource(url, container);
		util.debugWriteLine("isendedprogram url " + url + " res==null " + (res == null) + util.getMainSubStr(isSub, true));
//			util.debugWriteLine("isendedprogram res " + res + util.getMainSubStr(isSub, true));
		if (res == null) return false;
		var isEnd = res.IndexOf("\"content_status\":\"closed\"") != -1 ||
				res.IndexOf("<title>番組がみつかりません") != -1 ||
				res.IndexOf("番組が見つかりません</span>") != -1;
		util.debugWriteLine("is ended program " + isEnd + util.getMainSubStr(isSub, true));
		return isEnd; 
	}
	*/
	public static string existFile(string[] files, string reg, string startWith) {
//		var files = Directory.GetFiles(dirPath);
		foreach (var f in files) {
			var _f = getRegGroup(f, ".+\\\\(.+)");
			var isStartsWith = _f.StartsWith(startWith);
			if (!isStartsWith) continue;
			var _reg = util.getRegGroup(_f.Substring(startWith.Length), reg);
			if (_reg == null) continue;
//			util.debugWriteLine(_f.StartsWith(startWith));
//			util.debugWriteLine(util.getRegGroup(_f.Substring(startWith.Length), reg));
			return f;
//			if (issta_f.StartsWith(startWith) && 
//			    util.getRegGroup(_f.Substring(startWith.Length), reg) != null) return f;
		}
		return null;
	}
	public static string getSecondsToStr(double seconds) {
//		var dotSecond = ((int)((seconds % 1) * 10)).ToString("0");
		var second = ((int)((seconds % 60) * 1)).ToString("00");
		var minute = ((int)((seconds % 3600 / 60))).ToString("00");
		var hour = ((int)((seconds / 3600) * 1));
		var _hour = (hour < 100) ? hour.ToString("00") : hour.ToString();;
		var timeStr = _hour + "h" + minute + "m" + second + "s";
		return timeStr;
	}
	public static int getSecondsFromStr(string _s) {
		var h = getRegGroup(_s, "(\\d+)h");
		var m = getRegGroup(_s, "(\\d+)m");
		var s = getRegGroup(_s, "(\\d+)s");
		if (h == null || m == null || s == null) return -1;
		return int.Parse(h) * 3600 + int.Parse(m) * 60 + int.Parse(s);
	}
	public static int getPageType(string res) {
		//if (res.IndexOf("siteId&quot;:&quot;nicolive2") > -1) {
			var data = util.getRegGroup(res, "<script id=\"embedded-data\" data-props=\"([\\d\\D]+?)</script>");
			var status = (data == null) ? null : util.getRegGroup(data, "&quot;status&quot;:&quot;(.+?)&quot;");
			if (res.IndexOf("<!doctype html>") > -1 && data != null && status == "ON_AIR" && data.IndexOf("webSocketUrl&quot;:&quot;ws") > -1) return 0;
			else if (res.IndexOf("<!doctype html>") > -1 && data != null && status == "ENDED" && data.IndexOf("webSocketUrl&quot;:&quot;ws") > -1) return 7;
			else if (util.getRegGroup(res, "(混雑中ですが、プレミアム会員の方は優先して入場ができます)") != null ||
			        util.getRegGroup(res, "(ただいま、満員のため入場できません)") != null) return 1;
	//		else if (util.getRegGroup(res, "<div id=\"comment_arealv\\d+\">[^<]+この番組は\\d+/\\d+/\\d+\\(.\\) \\d+:\\d+に終了いたしました。<br>") != null) return 2;
			else if (res.IndexOf(" onclick=\"Nicolive.ProductSerial") > -1) return 8;
			//else if (res.IndexOf("※この放送はタイムシフトに対応しておりません。") > -1 && 
			//         res.IndexOf("に終了いたしました") > -1) return 2;
			//else if (util.getRegGroup(res, "(コミュニティフォロワー限定番組です。<br>)") != null) return 4;
			else if (res.IndexOf("isFollowerOnly&quot;:true") > -1 && res.IndexOf("isFollowed&quot;:false") > -1) return 4;
			else if (data != null && data.IndexOf("webSocketUrl&quot;:&quot;ws") == -1 && 
			         status == "ENDED") return 2;
			
			else if (status == "ENDED" && res.IndexOf(" onclick=\"Nicolive.WatchingReservation") > -1) return 9;
			//else if (util.getRegGroup(res, "(に終了いたしました)") != null) return 2;
			else if (status == "ENDED") return 2;
			else if (util.getRegGroup(res, "(<archive>1</archive>)") != null) return 3;
			else if (util.getRegGroup(res, "(チャンネル会員限定番組です。<br>)") != null) return 4;
			else if (util.getRegGroup(res, "(<h3>【会場のご案内】</h3>)") != null) return 6;
			else if (util.getRegGroup(res, "(この番組は放送者により削除されました。<br />|削除された可能性があります。<br />)") != null) return 2;
			return 5;
		//}
		//return 5;
	}
	public static int getPageTypeRtmp(string res, ref bool isTimeshift, bool isSub) {
//		var res = getPlayerStatusRes;
		if (res.IndexOf("status=\"ok\"") > -1 && res.IndexOf("<archive>0</archive>") > -1) {
			isTimeshift = false;
			return 0;
		}
		if (res.IndexOf("status=\"ok\"") > -1 && res.IndexOf("<archive>1</archive>") > -1) {
			isTimeshift = true;
			return 7;
		}
		else if (res.IndexOf("<code>require_community_member</code>") > -1) return 4;
		else if (res.IndexOf("<code>closed</code>") > -1) return 2;
		else if (res.IndexOf("<code>comingsoon</code>") > -1) return 5;
		else if (res.IndexOf("<code>notfound</code>") > -1) return 2;
		else if (res.IndexOf("<code>deletedbyuser</code>") > -1) return 2;
		else if (res.IndexOf("<code>deletedbyvisor</code>") > -1) return 2;
		else if (res.IndexOf("<code>violated</code>") > -1) return 2;
		else if (res.IndexOf("<code>usertimeshift</code>") > -1) return 2;
		else if (res.IndexOf("<code>tsarchive</code>") > -1) return 2;
		else if (res.IndexOf("<code>unknown_error</code>") > -1) return 5;
		else if (res.IndexOf("<code>timeshift_ticket_exhaust</code>") > -1) return 2;
		else if (res.IndexOf("<code>timeshiftfull</code>") > -1) return 1;
		else if (res.IndexOf("<code>maintenance</code>") > -1) return 5;
		else if (res.IndexOf("<code>noauth</code>") > -1) return 5;
		else if (res.IndexOf("<code>full</code>") > -1) return 1;
		else if (res.IndexOf("<code>block_now_count_overflow</code>") > -1) return 5;
		else if (res.IndexOf("<code>premium_only</code>") > -1) return 5;
		else if (res.IndexOf("<code>selected-country</code>") > -1) return 5;
		else if (res.IndexOf("<code>notlogin</code>") > -1) return 8;
//		rm.form.addLogText(res + util.getMainSubStr(isSub, true));
		return 5;
	}
	
	public static DateTime getUnixToDatetime(long unix) {
		DateTime UNIX_EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
		return UNIX_EPOCH.AddSeconds(unix).ToLocalTime();
	}
	public static string getSecondsToKeikaJikan(double seconds) {
		var second = ((int)((seconds % 60) * 1)).ToString("00");
		var minute = ((int)((seconds % 3600 / 60))).ToString("00");
		var hour = ((int)((seconds / 3600) * 1));
		var _hour = (hour < 100) ? hour.ToString("00") : hour.ToString();;
		return _hour + "時間" + minute + "分" + second + "秒";
	}
	public static void writeFile(string name, string str) {
		using (var f = new System.IO.FileStream(name, FileMode.Append))
        using (var w = new StreamWriter(f)) {
	       	try {
				w.WriteLine(str);
	       	} catch (Exception e) {
	       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.TargetSite);
	       	}
       }
//		w.Close();
//		f.Close();
	}
	public static string getOkSJisOut(string s) {
		var a = System.Text.Encoding.GetEncoding("shift_jis");
		return a.GetString(a.GetBytes(s)).Replace("?", "_");
	}
	public static bool isLogFile = false;
	public static StreamWriter exceptionSw = null;
	public static List<string> debugWriteBuf = new List<string>();
	//public static Task debugWriteTask = null;
	public static void debugWriteLine(object str) {
		var dt = DateTime.Now.ToLongTimeString();
//		System.Console.WriteLine(dt + " " + str);
		try {
			#if DEBUG
				System.Diagnostics.Debug.WriteLine(str);
	//      		System.Diagnostics.util.debugWriteLine(
			#else
				if (isLogFile) {
					System.Console.WriteLine(dt + " " + str);
					//debugWriteBuf.Add(dt + " " + str);
				}
			#endif
		} catch (Exception e) {
//			util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.TargetSite + " " + e.Source);
			System.Diagnostics.Debug.WriteLine(e.Message + " " + e.StackTrace + " " + e.TargetSite + " " + e.Source);
		}
		
	}
	/*
	private static Task debugWriter() {
		while (true) {
			
		}
	}
	*/
	public static void showException(Exception eo, bool isMessageBox = true) {
		var frameCount = new System.Diagnostics.StackTrace().FrameCount;
		#if DEBUG
			if (isMessageBox && isLogFile) {
				if (frameCount > 150) {
					util.showMessageBoxCenterForm(null, "framecount stack", frameCount.ToString() + " " + namaichi.Program.arg + " " + DateTime.Now.ToString("yyyy\"/\"MM\"/\"dd HH\":\"mm\":\"ss"));
					return;
				}
			}
		#else
			
		#endif
		
		
		util.debugWriteLine("exception stacktrace framecount " + frameCount);
		util.debugWriteLine("show exception eo " + eo);
		if (eo == null) return;
		var dt = DateTime.Now.ToLongTimeString();
		
		util.debugWriteLine("0 message " + eo.Message + "\nsource " + 
				eo.Source + "\nstacktrace " + eo.StackTrace + 
				"\n targetsite " + eo.TargetSite + "\n\n");
		if (exceptionSw != null) {
			exceptionSw.WriteLine(dt + " 0 message " + eo.Message + "\nsource " + 
				eo.Source + "\nstacktrace " + eo.StackTrace + 
				"\n targetsite " + eo.TargetSite + "\n\n");
			exceptionSw.Flush();
		}
		
		var _eo = eo.GetBaseException();
		util.debugWriteLine("eo " + _eo);
		if (_eo != null) {
			util.debugWriteLine("1 message " + _eo.Message + "\nsource " + 
					_eo.Source + "\nstacktrace " + _eo.StackTrace + 
					"\n targetsite " + _eo.TargetSite + "\n\n");
			if (exceptionSw != null) {
				exceptionSw.WriteLine(dt + " 1 message " + _eo.Message + "\nsource " + 
					_eo.Source + "\nstacktrace " + _eo.StackTrace + 
					"\n targetsite " + _eo.TargetSite + "\n\n");
				exceptionSw.Flush();
			}
		}
		
		_eo = eo.InnerException;
		util.debugWriteLine("eo " + _eo);
		if (_eo != null) {
			util.debugWriteLine("2 message " + _eo.Message + "\nsource " + 
					_eo.Source + "\nstacktrace " + _eo.StackTrace + 
					"\n targetsite " + _eo.TargetSite);
			if (exceptionSw != null) {
				exceptionSw.WriteLine(dt + " 2 message " + _eo.Message + "\nsource " + 
					_eo.Source + "\nstacktrace " + _eo.StackTrace + 
					"\n targetsite " + _eo.TargetSite);
				exceptionSw.Flush();
			}
		}
		
		
		#if DEBUG
			if (isMessageBox && isLogFile)
				util.showMessageBoxCenterForm(null, "error " + eo.Message, "error " + namaichi.Program.arg);
		#else
			
		#endif
	}
	public static void setLog(config config, string lv) {
		//test
		if (bool.Parse(config.get("IsLogFile"))) {
			//var name = (args.Length == 0) ? "lv_" : util.getRegGroup(args[0], "(lv\\d+)");
			var name = (lv == null) ? "log" : lv;
			var logPath = util.getJarPath()[0] + "/" + name + ".txt";
			
			try {
				#if DEBUG
					System.Diagnostics.DefaultTraceListener dtl
				      = (System.Diagnostics.DefaultTraceListener)System.Diagnostics.Debug.Listeners["Default"];
					dtl.LogFileName = logPath;
				#else
					if (isStdIO) return; 
					FileStream fs = new FileStream(logPath, 
							FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
					var w = new System.IO.StreamWriter(fs);
					w.AutoFlush = true;
					System.Console.SetOut(w);
					
				#endif
			} catch (Exception e) {
				util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
			}
			util.isLogFile = true;
			try {
				//exceptionSw = new StreamWriter(util.getJarPath()[0] + "/errorLog.txt", true);
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + " " + ee.StackTrace + " " + ee.Source + " " + ee.TargetSite);
			}
		}
	}
	public static bool isOkDotNet() {
		var ver = Get45PlusFromRegistry();
		return ver >= 4.52;
	}
	public static double Get45PlusFromRegistry() {
		const string subkey = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\";
	
		using (RegistryKey ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(subkey))
		{
			if (ndpKey != null && ndpKey.GetValue("Release") != null) {
				return CheckFor45PlusVersion((int) ndpKey.GetValue("Release"));
//			Console.WriteLine(".NET Framework Version: " + CheckFor45PlusVersion((int) ndpKey.GetValue("Release")));
			}
			else {
	//			Console.WriteLine(".NET Framework Version 4.5 or later is not detected.");
			} 
		}
		return -1;
	}
	private static double CheckFor45PlusVersion(int releaseKey)
   {
      if (releaseKey >= 461808)
         return 4.72; //later
      if (releaseKey >= 461308)
         return 4.71;
      if (releaseKey >= 460798)
         return 4.7;
      if (releaseKey >= 394802)
         return 4.62;
      if (releaseKey >= 394254)
         return 4.61;      
      if (releaseKey >= 393295)
         return 4.6;      
      if (releaseKey >= 379893)
         return 4.52;      
      if (releaseKey >= 378675)
         return 4.51;      
      if (releaseKey >= 378389)
       return 4.5;      
    return -1;
   }
	public static string CheckOSName()
        {
            string result = "";

            System.Management.ManagementClass mc =
                new System.Management.ManagementClass("Win32_OperatingSystem");
            System.Management.ManagementObjectCollection moc = mc.GetInstances();

            try
            {
                foreach (System.Management.ManagementObject mo in moc)
                {
                    result = mo["Caption"].ToString();
                    if (mo["CSDVersion"] != null)
                        result += " " + mo["CSDVersion"].ToString();
                    result += " (" + mo["Version"].ToString() + ")";
                }
                osName = result;
            }
            catch (Exception e)
            {
                util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
                return result;
            }

            return result;
        }
        public static string CheckOSType()
        {
            string result = "";

            System.Management.ManagementClass mc =
                new System.Management.ManagementClass("Win32_OperatingSystem");
            System.Management.ManagementObjectCollection moc = mc.GetInstances();

            try
            {
                foreach (System.Management.ManagementObject mo in moc)
                {
                    if (mo["Version"].ToString().StartsWith("5.1"))
                        result = "XP";
                    else if (mo["Version"].ToString().StartsWith("6.0"))
                        result = "Vista";
                    else if (mo["Version"].ToString().StartsWith("6.1"))
                        result = "7";
                    else if (mo["Version"].ToString().StartsWith("6.2"))
                        result = "8";
                    else if (mo["Version"].ToString().StartsWith("6.3"))
                        result = "8.1";
                    else if (mo["Version"].ToString().StartsWith("10.0"))
                        result = "10";
                    else if (mo["Version"].ToString().StartsWith("11.0"))
                        result = "11";
                    else
                        result = "other";
                }
                osType = result;
            }
            catch (Exception e)
            {
                util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
                return result;
            }
            return result;
        }

	public static string getMainSubStr(bool isSub, bool isKakko = false) {
		var ret = (isSub) ? "サブ" : "メイン";
		if (isKakko) ret = "(" + ret + ")";
		return ret;		
	}
	public static string getUserName(string userId, out bool isFollow, CookieContainer container, bool isRequireFollow, config cfg) {
		util.debugWriteLine("access__ getUserName " + userId);
		isFollow = false; 
		if (userId == "official" || userId == null || userId == "") return null;
			
		if (isRequireFollow) {
			var name = getUserNameFollow(userId, out isFollow, container);
			if (name != null) return name;
		}
		
		var _url = "https://api.live2.nicovideo.jp/api/v1/user/nickname?userId=" + userId;
		var r = util.getPageSource(_url, null);
		if (r != null && !r.StartsWith("{\"error\":\"")) {
			var name = util.getRegGroup(r, "\"nickname\":\"(.*)\"");
			if (name != null) return name;
		}
		
		_url = "https://public.api.nicovideo.jp/v1/users/" + userId + ".json";
		var _h = new Dictionary<string, string>();
		_h.Add("User-Agent", "nicocas-Android/" + cfg.get("nicoCasAppVer"));
		_h.Add("X-Frontend-Id", "90");
		_h.Add("X-Frontend-Version", cfg.get("nicoCasAppVer"));
		_h.Add("X-Os-Version", "25");
		var _res = util.getPageSource(_url, container);
		if (_res != null) {
			var name = util.getRegGroup(_res, "\"nickname\":\"(.+?)\"");
			if (name != null) return name;
		}
		
		_url = "http://seiga.nicovideo.jp/api/user/info?id=" + userId;
		_res = util.getPageSource(_url, null);
			
		if (_res != null) {
			var name = util.getRegGroup(_res, "<nickname>(.+?)</nickname>");
			if (name != null) return name;
		}
		/*
		var url = "http://ext.nicovideo.jp/thumb_user/" + userId;
		var res = util.getPageSource(url, null);
		if (res != null) {
			var name = util.getRegGroup(res, "<title>(.+?)さんのプロフィール‐ニコニコ動画</title>");
			if (name != null) return name;
		}
		*/
		return null;
    }
    private static string getUserNameFollow(string userId, out bool isFollow, CookieContainer container) {
        isFollow = false;
		/*
		var url = "https://public.api.nicovideo.jp/v1/user/followees/niconico-users/" + userId + ".json";
		var h = new Dictionary<string, string>();
		h.Add("User-Agent", "nicocas-Android/" + cfg.get("nicoCasAppVer"));
		h.Add("X-Frontend-Id", "90");
		h.Add("X-Frontend-Version", cfg.get("nicoCasAppVer"));
		h.Add("X-Os-Version", "25");
		//var _res = util.sendRequest(url, h, null, "GET");
		var res = util.getPageSource(url, container);
		
		isFollow = res != null && res.IndexOf("true") > -1;
		*/
		
		var url = "https://www.nicovideo.jp/user/" + userId;
		var res = util.getPageSource(url, container);

		if (res == null) return null;
		if (res != null) {
			var name = util.getRegGroup(res, "<meta property=\"profile:username\" content=\"(.+?)\">"); 
			if (name == null) return null;
			if (res.IndexOf("&quot;isFollowing&quot;:true") > -1)
				isFollow = true;
			return name;
		}
		return null;
    }
	public static string getCommunityName(string communityNum, out bool isFollow, CookieContainer cc) {
		isFollow = false;
		if (communityNum == null || communityNum == "" || communityNum == "official") return null;
		
		var isChannel = communityNum.IndexOf("ch") > -1;
		var url = (isChannel) ? 
			("https://ch.nicovideo.jp/" + communityNum) :
			("https://com.nicovideo.jp/community/" + communityNum);
		
//			var wc = new WebHeaderCollection();
		var res = util.getPageSource(url, cc);
		//util.debugWriteLine(cc.GetCookieHeader(new Uri(url)));
		
		if (res == null) {
			url = (isChannel) ? 
				("https://ch.nicovideo.jp/" + communityNum) :
				("https://com.nicovideo.jp/motion/" + communityNum);
			res = util.getPageSource(url, cc);
			if (res != null) {
				isFollow = //res.IndexOf("<h2 class=\"pageHeader_title\">コミュニティにフォローリクエストを送る</h2>") == -1 &&
						//util.getRegGroup(res, "<p class=\"error_description\">[\\s\\S]*?(コミュニティフォロワー)ではありません。") == null &&
						//res.IndexOf(">コミュニティをフォローする</h2>") == -1;
						false;
			}
		} else {
			isFollow = (isChannel) ? 
				(res.IndexOf("class=\"bookmark following btn_follow\"") > -1):
				(res.IndexOf("followButton follow\">フォロー") == -1);
		}
		string title = null;
		if (res != null) {
			title = (isChannel) ? 
	//			util.getRegGroup(res, "<meta property=\"og\\:title\" content=\"(.+?) - ニコニコチャンネル") :
				util.getRegGroup(res, "<meta property=\"og:site_name\" content=\"(.+?)\"") :
				util.getRegGroup(res, "<meta property=\"og\\:title\" content=\"(.+?)-ニコニコミュニティ\"");
			if (title == null) title = util.getRegGroup(res, "<meta property=\"og:title\" content=\"(.+?)さんのコミュニティ-ニコニコミュニティ\">");
			
		}
		//not login
		if (title == null) {
			url = "https://ext.nicovideo.jp/thumb_" + ((isChannel) ? "channel" : "community") + "/" + communityNum;
			res = getPageSource(url, cc, null, false, 3);
			//title = getRegGroup(res, "<p class=\"chcm_tit\">(.+?)</p>");
			if (res != null)
				title = getRegGroup(res, "<title>(.+?)‐(ニコニコミュニティ|ニコニコチャンネル)</title>");
		} else {
			title = WebUtility.HtmlDecode(title);
		}
		if (title == null) isFollow = false;
		
		return title;
		
	}
	public static void openUrlBrowser(string url, config config) {
		try {
			if (config.get("IsdefaultBrowserPath") == "true")
				Process.Start(url);
			else {
				var path = config.get("browserPath");
				if (path == null || path == "")
					Process.Start(url);
				else Process.Start(path, url);
			}
		} catch (Exception e) {
			util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
		}
	}
	public static void setClipBordText(string s) {
		try {
			Clipboard.SetText(s);
		} catch (Exception e) {
			util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
		}
	}
	public static string getArrStr(byte[] arr) {
		var s = "";
		foreach (var b in arr) s += ", " + b;
		return s;
	}
	public static void appliProcess(string appliPath, string url, string args, RssItem ri, CookieContainer cc, config cfg, int appNum, MainForm form) {
		if (appliPath == null || appliPath == "") return;
		
		string arg = "未設定";
		try {
			var us = "";
			try {
				var c = cc.GetCookies(new Uri(url));
				us = c["user_session"] != null ? c["user_session"].Value : "";
			} catch (Exception e) {util.debugWriteLine(e.Message + e.Source + e.StackTrace);}
			
			
			var appName = (char)('A' + appNum);
			AppSettingInfo asi = null;
			try {
				var c = cfg.get("appli" + appName + "Setting");
				asi = JsonConvert.DeserializeObject<AppSettingInfo>(c);
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace);
				asi = new AppSettingInfo();
			}
			var userId = string.IsNullOrEmpty(ri.userId) ? (string.IsNullOrEmpty(ri.comId) ? "_" : ri.comId) : ri.userId;
			form.addLogText("a " + userId);
			arg = util.getDokujiSetteiArg(ri.hostName, ri.comName, ri.title, ri.lvId.Trim('e'), ri.comId, args, ri.pubDateDt, userId, us, asi, form);
			form.addLogText("arg " + arg);
						
			var isMin = false;
			var minimizedC = cfg.get("IsminimizedApp");
			if (!string.IsNullOrEmpty(minimizedC)) {
				var arr = minimizedC.Split(',');
				if (arr.Length == 10)
					bool.TryParse(arr[appNum], out isMin);
				else if (arr.Length > 0)
					bool.TryParse(arr[0], out isMin);
			}
				
			//var r = CreateProcess(appliPath, arg, isMin);
			var r = ShellExecute(appliPath, arg, isMin);
			
			if (bool.Parse(cfg.get("IsAppliLog")))
				form.addLogText(appliPath + (r ? "を起動しました" : "の起動に失敗しました") + " 引数：" + arg + " 最小化:" + isMin);
		} catch (Exception e) {
			util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			form.addLogText(appliPath + "の起動に失敗しました 引数：" + args + " " + e.Message + e.Source + e.StackTrace + e.TargetSite + "/" + appNum);
		}
	}
    public static void appliProcessFromLvid(string appliPath, string lvid, string args, CookieContainer cc, config cfg, int appNum, MainForm form) {
		if (appliPath == null || appliPath == "") return;
		var url = "https://live.nicovideo.jp/watch/lv" + util.getRegGroup(lvid, "(\\d+)");

		try {
			appliPath = appliPath.Trim();
			
			var ri = util.getRiFromLvid(lvid, cc);
			if (ri != null) {
				util.appliProcess(appliPath, url, args, ri, cc, cfg, appNum, form);
			}
			//else Process.Start(appliPath, url + " " + args);
			else {
				var isMin = false;
				var minimizedC = cfg.get("IsminimizedApp");
				if (!string.IsNullOrEmpty(minimizedC)) {
					var arr = minimizedC.Split(',');
					if (bool.Parse(arr[appNum])) {
						isMin = true;
					}
				}
				//var r = CreateProcess(appliPath, url + " " + args, isMin);
				var r = ShellExecute(appliPath, url + args, isMin);
				form.addLogText(appliPath + (r ? "を起動しました" : "の起動に失敗しました") + " 引数：" + args + " 最小化:" + isMin);
				util.debugWriteLine(appliPath + (r ? "を起動しました" : "の起動に失敗しました") + " 引数：" + args + " 最小化:" + isMin);
			}
		} catch (Exception e) {
			util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
		}
	}
	public static void playSound(config cfg, AlartInfo ai, MainForm form) {
		try {
			string path = null;
			float volume = 0;
			var basePath = util.getJarPath()[0];
			if (ai != null && ai.isSoundId) {
				try {
					if (!string.IsNullOrEmpty(ai.communityId) && 
					    	File.Exists(basePath + "/Sound/" + ai.communityId + ".wav"))
						path = basePath + "/Sound/" + ai.communityId + ".wav";
					else if (!string.IsNullOrEmpty(ai.hostId) && 
					    	File.Exists(basePath + "/Sound/" + ai.hostId + ".wav"))
						path = basePath + "/Sound/" + ai.hostId + ".wav";
					else if (!string.IsNullOrEmpty(ai.keyword) && 
					    	File.Exists(basePath + "/Sound/" + ai.keyword + ".wav"))
						path = basePath + "/Sound/" + ai.keyword + ".wav";
					if (path != null) volume = float.Parse(cfg.get("soundVolume")) / 100;
				} catch (Exception e) {
					util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				}
			}
			if (ai != null && path == null) {
				if (ai.soundType == 1) {
					path = cfg.get("soundPathA");
					volume = float.Parse(cfg.get("soundAVolume")) / 100;
				} else if (ai.soundType == 2) {
					path = cfg.get("soundPathB");
					volume = float.Parse(cfg.get("soundBVolume")) / 100;
				} else if (ai.soundType == 3) {
					path = cfg.get("soundPathC");
					volume = float.Parse(cfg.get("soundCVolume")) / 100;
				}
			}
			if (path == null || !File.Exists(path)) {
				path = util.getJarPath()[0] + "/Sound/se_soc01.wav";
				volume = float.Parse(cfg.get("soundVolume")) / 100;
			}
			
			playSoundCore(volume, path, form);
			
			/*
			var path = (bool.Parse(cfg.get("IsSoundDefault"))) ? 
				(util.getJarPath()[0] + "/Sound/se_soc02.wav") : cfg.get("soundPath");
			util.debugWriteLine(path);
			var m = new System.Media.SoundPlayer(path);
			
			m.PlaySync();
			*/
		} catch (Exception e) {
			util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
		}
	}
	public static void playSoundCore(float volume, string path, MainForm form) {
		try {
			util.debugWriteLine("sound path " + path);
			if (volume < 0) volume = (float)0;
			if (volume > 1) volume = (float)1;
			
			var mode = 2;
			
			if (mode == 0) {
				playSoundMCI(path, (int)(volume * 1000), true, form);
			} else if (mode == 1) {
				//naudio
				/*
				var reader = new NAudio.Wave.AudioFileReader(path);
				var waveOut = new NAudio.Wave.WaveOut(NAudio.Wave.WaveCallbackInfo.FunctionCallback());
				waveOut.Init(reader);
				
				waveOut.Volume = volume;
				util.debugWriteLine("volume " + waveOut.Volume);
				waveOut.Play();
				while (waveOut.PlaybackState == NAudio.Wave.PlaybackState.Playing) Thread.Sleep(100);
				//waveOut.Dispose();
				reader.Close();
				*/
			} else if (mode == 2) {
				Bass.BASS_Free();
				var init = Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
				util.debugWriteLine("bass init " + init);
				
				int handle = Bass.BASS_StreamCreateFile(path, 0, 0, BASSFlag.BASS_DEFAULT);
				util.debugWriteLine("bass handle " + handle);
				Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_GVOL_STREAM, (int)(volume * 10000));
				Bass.BASS_ChannelPlay(handle, false);
				
			}
			
		} catch (Exception e) {
			util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
		}
		
	}
	[System.Runtime.InteropServices.DllImport("winmm.dll",
    		CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
	private static extern int mciSendString(string command,
				[MarshalAs(UnmanagedType.LPTStr), Out] StringBuilder buffer, int bufferSize, IntPtr hwndCallback);
	[System.Runtime.InteropServices.DllImport("winmm.dll",
    		CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
    public static extern bool mciGetErrorString(int dwError, 
			[MarshalAs(UnmanagedType.LPTStr), Out] StringBuilder lpstrBuffer, int uLength);
    
	public static void playSoundMCI(string path, int volume, bool isAsync, MainForm form) {
		debugWriteLine("playsound mci " + path + " volume " + volume + " " + isAsync);
		
		var name = Guid.NewGuid().ToString();
	    var cmd = "open \"" + path + "\" type mpegvideo alias " + name;
	    debugWriteLine(cmd);
	    var ret = mciSendStringForm(cmd, null, 0, IntPtr.Zero, form);
	    
	    if (ret != 0) {
	    	debugWriteLine("playsound mci open error " + ret + " " + cmd);
	    	StringBuilder errMsg = new StringBuilder(1000);
			mciGetErrorString(ret, errMsg, 1000);
			debugWriteLine("mci err " + errMsg);
			form.addLogText("サウンドの再生中に問題が発生しました。ERROR:" + ret + ", メッセージ:" + errMsg + " パス:" + path + ", volume:" + volume);
	        return;
	    }
	    ret = mciSendStringForm("setaudio " + name + " volume to " + volume.ToString(), null, 0, IntPtr.Zero, form);
	    
	    if (ret != 0) {
	    	debugWriteLine("playsound mci setaudio error " + ret + " " + cmd);
	    	StringBuilder errMsg = new StringBuilder(1000);
			mciGetErrorString(ret, errMsg, 1000);
			debugWriteLine("mci err " + errMsg);
			form.addLogText("サウンドの再生中に問題が発生しました。ERROR:" + ret + ", メッセージ:" + errMsg + " パス:" + path + ", volume:" + volume);
	        return;
	    }
	    
	    ret = mciSendStringForm("play " + name, null, 0, IntPtr.Zero, form);
	    
	    if (ret != 0) {
	    	debugWriteLine("playsound mci play error " + ret + " " + cmd);
	    	StringBuilder errMsg = new StringBuilder(1000);
			mciGetErrorString(ret, errMsg, 1000);
			debugWriteLine("mci err " + errMsg);
			form.addLogText("サウンドの再生中に問題が発生しました。ERROR:" + ret + ", メッセージ:" + errMsg + " パス:" + path + ", volume:" + volume);
	        return;
	    }
	    
	    if (!isAsync) {
	    	StringBuilder status = new StringBuilder();
		    while(true) {
	    		Thread.Sleep(1000);
		    	status = new StringBuilder();
		    	ret = mciSendStringForm("status " + name + " mode", status, 256, IntPtr.Zero, form);
		    	
		    	debugWriteLine("rc " + ret + " " + status);
		    	if (status.ToString() != "playing") break;
		    }
	    }
	}
	private static int mciSendStringForm(string command,
			StringBuilder buffer, int bufferSize, IntPtr hwndCallback, MainForm form) {
		var ret = 0;
		form.formAction(() => 
		                	ret = mciSendString(command, buffer, bufferSize, hwndCallback)
		                	, -1);
				//ret = mciSendString(command, buffer, bufferSize, hwndCallback));
		return ret;
	}
	public static bool sendMail(string from, string to, 
			string subject, string body, string _smtp, 
			string _port, string user, string pass, 
			bool isSsl, out string msg) {
		msg = null;
		if (from == "" || to == "" || _smtp == "" ||
		    	_port == "" || user == "" || pass == "")
			return false;
		int port;
		if (!int.TryParse(_port, out port)) port = 587;
		try {
			using (var m = new MailMessage(from, to, subject, body))
				using (var smtp = new SmtpClient(_smtp, port)) {
					smtp.Credentials = new NetworkCredential(user, pass);
					smtp.EnableSsl = isSsl;
					//ServicePointManager.SecurityProtocol = SecurityProtocolType.
					smtp.Send(m);
			}
			return true;
		} catch (Exception e) {
			util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			msg = e.Message;
			return false;
		}
	}
	
	[DllImport("user32.dll")]
	public static extern int MessageBox(IntPtr hwnd, String text, String caption, uint type);
	public static int showModelessMessageBox(string text, string caption, MainForm form, int style = 0) {
		//0-ok 1-okcancel 2-MB_ABORTRETRYIGNORE 3-MB_YESNOCANCEL
		//20-MB_ICONQUESTION 10-MB_ICONSTOP 30-MB_ICONWARNING 40-MB_ICONINFORMATION
		//0-MB_DEFBUTTON1 100-MB_DEFBUTTON2 200-MB_DEFBUTTON3
		//10000-MB_SETFOREGROUND 40000-MB_TOPMOST
		//return 0-error 1-IDOK 2-IDCANCEL 3-IDABORT 6-IDYES 7-IDNO
		int res = 0;
		form.formAction(() =>
			res = MessageBox(new IntPtr(0), text, caption, (uint)style)
		);
		return res;
	}
	public static string removeTag(string s) {
		if (s == null) {
			util.debugWriteLine("removeTag s null");
			return "";
		}
		var ret = new Regex("</*font.*?>").Replace(s, "");
		ret = new Regex("<br.*?>").Replace(ret, "");
		ret = ret.Replace("<b>", "").Replace("</b>", "");
		ret = ret.Replace("<i>", "").Replace("</i>", "");
		ret = ret.Replace("<s>", "").Replace("</s>", "");
		ret = ret.Replace("<u>", "").Replace("</u>", "");
		return ret;
	}
	public enum NotifyFlags {
        NIF_MESSAGE = 0x01, NIF_ICON = 0x02, NIF_TIP = 0x04, NIF_INFO = 0x10, NIF_STATE = 0x08,
        NIF_GUID = 0x20, NIF_SHOWTIP = 0x80, NIF_REALTIME = 0x40,
    }
	[DllImport("shell32.dll")]
	public static extern System.Int32 Shell_NotifyIcon(NotifyCommand cmd, ref NOTIFYICONDATA data);
	    
    public enum NotifyCommand { NIM_ADD = 0x0, NIM_DELETE = 0x2, NIM_MODIFY = 0x1, NIM_SETVERSION = 0x4 }

    [StructLayout(LayoutKind.Sequential)]
    public struct NOTIFYICONDATA {
        public Int32 cbSize;
        public IntPtr hWnd;
        public Int32 uID;
        public NotifyFlags uFlags;
        public Int32 uCallbackMessage;
        public IntPtr hIcon;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public String szTip;
        public Int32 dwState;
        public Int32 dwStateMask;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public String szInfo;
        public Int32 uVersion;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public String szInfoTitle;
        public Int32 dwInfoFlags;
        public Guid guidItem; //> IE 6
        public IntPtr hBalloonIcon;
    }
	public static bool displayIconBalloon(string title, string mes, 
			IntPtr icon, IntPtr hwnd) {
		NOTIFYICONDATA data = new NOTIFYICONDATA();
        var NIIF_USER = 4;
        //var NIIF_NONE = 0;
        //var NIIF_LARGE_ICON = 0x20;
        var dwInfoFlags = NIIF_USER;// | NIIF_LARGE_ICON;
        //dwInfoFlags = NIIF_NONE;

        data.cbSize = Marshal.SizeOf(data);
        data.uID = 0;
        data.hWnd = hwnd;
        data.dwInfoFlags = dwInfoFlags;
        data.hIcon = IntPtr.Zero;
        data.hBalloonIcon = IntPtr.Zero;
        if (icon != IntPtr.Zero)
        {
        	//Debug.WriteLine(icon);
            data.hBalloonIcon = icon;
            //data.hBalloonIcon = ((Bitmap)image).GetHicon();
            //data.dwInfoFlags |= NIIF_LARGE_ICON;
        }
        
        data.szInfo = mes;
        data.szInfoTitle = title;
        //data.szInfo = "aaa";
        //data.szInfoTitle = "title";

        //data.uFlags = NotifyFlags.NIF_INFO | NotifyFlags.NIF_SHOWTIP | NotifyFlags.NIF_REALTIME;
		data.uFlags = NotifyFlags.NIF_INFO;
	
        var r = Shell_NotifyIcon(NotifyCommand.NIM_MODIFY, ref data);
        //var r = Shell_NotifyIcon(NotifyCommand.NIM_ADD, ref data);
        Debug.WriteLine("displayIconBalloon " + r);
        return r == 1;
	}
	public static bool addNotifyIcon(IntPtr hwnd) {
		NOTIFYICONDATA nid = new NOTIFYICONDATA();
    	//ZeroMemory(&nid, sizeof(NOTIFYICONDATA));
    	var NIIF_NONE = 0;
    	var dwInfoFlags = NIIF_NONE;
        nid.cbSize = Marshal.SizeOf(nid);
        nid.hWnd = hwnd;
        nid.uID = 0;
        nid.uFlags = NotifyFlags.NIF_ICON | NotifyFlags.NIF_MESSAGE | NotifyFlags.NIF_TIP;
        //nid.uCallbackMessage = ""MY_NOTIFYICON;
        nid.dwInfoFlags = dwInfoFlags;
        nid.hIcon = IntPtr.Zero;
        //nid.szTip = 
        var r = Shell_NotifyIcon(NotifyCommand.NIM_ADD, ref nid);
        debugWriteLine("addNotifyIcon " + r);
        return r == 1;
	}
	public static bool deleteNotifyIcon(IntPtr hwnd) {
		NOTIFYICONDATA nid = new NOTIFYICONDATA();
    	//ZeroMemory(&nid, sizeof(NOTIFYICONDATA));
        nid.cbSize = Marshal.SizeOf(nid);
        nid.hWnd = hwnd;
        nid.uID = 0;
        nid.uFlags = NotifyFlags.NIF_ICON | NotifyFlags.NIF_MESSAGE | NotifyFlags.NIF_TIP;
        //nid.uCallbackMessage = ""MY_NOTIFYICON;
        nid.hIcon = IntPtr.Zero;
        //nid.szTip = 
        var r = Shell_NotifyIcon(NotifyCommand.NIM_DELETE, ref nid);
        debugWriteLine("deleteNotifyIcon " + r);
        return r == 1;
	}
    public static string getUnicodeToUtf8(string s) {
        var b = Encoding.Unicode.GetBytes(s);
        var utB = Encoding.Convert(Encoding.Unicode, Encoding.UTF8, b);
        return Encoding.UTF8.GetString(utB);
    }
    public static void dllCheck(namaichi.MainForm form) {
		var path = getJarPath()[0];
		var dlls = new string[]{"websocket4net.dll", 
				//"NAudio.dll",
				"Interop.IWshRuntimeLibrary.dll", "SnkLib.App.CookieGetter.Forms.dll",
				"SnkLib.App.CookieGetter.dll", "SuperSocket.ClientEngine.dll",
				//"Microsoft.Web.XmlTransform.dll", 
				"Newtonsoft.Json.dll",
				"System.Data.SQLite.dll", "x64/SQLite.Interop.dll",
				"x86/SQLite.Interop.dll", "x86/SnkLib.App.CookieGetter.x86Proxy.exe",
				//"Google.Protobuf.dll", "Google.Protobuf.dll",
				"protobuf-net.dll",
				"BouncyCastle.Crypto.dll"};
				
		var isOk = new string[dlls.Length];
		var msg = "";
		foreach (var n in dlls) {
			if (!File.Exists(path + "/" + n)) 
				msg += (msg == "" ? "" : ",") + n;
		}
		if (msg != "") 
			form.formAction(() => util.showMessageBoxCenterForm(form, path + "内に" + msg + "が見つかりませんでした"));
		
		#if NET40
			//.net4 dll
			dlls = new string[]{"Microsoft.Threading.Tasks.dll", 
					"Microsoft.Threading.Tasks.Extensions.Desktop.dll",
					"Microsoft.Threading.Tasks.Extensions.dll", "System.IO.dll",
					"System.Net.Http.dll", "System.Net.Http.Primitives.dll",
					"System.Net.Http.WebRequest.dll", "System.Threading.Tasks.dll"};
			isOk = new string[dlls.Length];
			msg = "";
			foreach (var n in dlls) {
				if (!File.Exists(path + "/" + n)) 
					msg += (msg == "" ? "" : ",") + n;
			}
			if (msg != "") 
				form.formAction(() => util.showMessageBoxCenterForm(form, path + "内に" + msg + "が見つかりませんでした。ver0.1.7.45以前からの更新の場合、解凍してできたファイルをこのフォルダに全てコピーすると動作するかもしれません。"));
    	#endif
	}
    public static string getTimeGuid() {
		var dt = DateTime.Now;
		return dt.ToString("yyyyMMdd-HHmm-ssHH-ssHH-yyyyMMddyyyy");
	}
    public static void saveBackupList(string path, string f) {
    	try {
    		var p = path + "\\backup";
    		if (File.Exists(path + f + ".ini")) {
    			if (!Directory.Exists(p))
    				Directory.CreateDirectory(p);
    			if (!Directory.Exists(p)) return;
    			
    			var dt = DateTime.Now.ToString("yyyyMMdd");
    			File.Copy(path + f + ".ini", p + "\\" + f + dt + "backup.ini", true);
    		}
    		var _fList = new List<string>(Directory.GetFiles(p, "*" + f + "*"));
    		var fList = new List<string>();
    		var dtL = new List<int>();
    		foreach (var _f in _fList) {
    			var d = util.getRegGroup(_f, f + "(\\d+)backup.ini");
    			if (d == null) continue;
    			fList.Add(_f);
    			dtL.Add(int.Parse(d));
    		}
    		if (fList.Count <= 5) return;
    		var vals = fList.ToArray();
    		Array.Sort(dtL.ToArray(), vals);
    		for (var i = 0; i < vals.Length - 5; i++)
    			File.Delete(vals[i]);
    		
		} catch (Exception e) {
			util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
		}
    }
    public static void updateAppVersion(string appName, config config) {
    	string url = null;
   		string cfgName = null;
    	if (appName == "niconico") {
    		url = "https://play.google.com/store/apps/details?id=jp.nicovideo.android";
    		cfgName = "niconicoAppVer";
    	} else if (appName == "nicocas") {
    		url = "https://play.google.com/store/apps/details?id=jp.co.dwango.nicocas";
    		cfgName = "nicoCasAppVer";
    	}
    	var res = util.getPageSource(url);
    	if (res == null) return; 
		var ver = util.getRegGroup(res, ">Current Version<.+?>([\\d\\.]+?)<");
		if (ver == null) return;
		config.set(cfgName, ver);
    }
    public static bool loginCheck(CookieContainer cc, string url, string log = null) {
		try {
			var us = cc.GetCookies(new Uri(url))["user_session"];
			if (us == null) {
				if (log != null)
					log += "Cookie内にユーザーセッションが見つかりませんでした。";
				return false;
			}
			var uid = util.getRegGroup(us.Value, "user_session_(.+?)_");
			if (uid == null) {
				if (log != null)
					log += "ユーザーセッション内にIDが見つかりませんでした。";
				return false;
			}
			var _url = "https://public.api.nicovideo.jp/v1/user/followees/niconico-users/" + uid + ".json";
			var res = util.getPageSource(_url, cc);
			if (log != null)
				log += res != null ? "ログインを確認できました。" : "ログインを確認できませんでした";
			return res != null;
		} catch (Exception e) {
			util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			return false;
		}
	}
    public static string getMyName(CookieContainer cc, string us) {
		try {
			var url = "https://nvapi.nicovideo.jp/v1/users/me";
			//var us = cc.GetCookies(new Uri(url))["user_session"];
			//if (us == null) return null;
			var _h = new Dictionary<string, string>() {
				{"User-Agent", "Niconico/1.0 (Linux; U; Android 7.1.2; ja-jp; nicoandroid LGM-V300K) Version/6.14.1"},
				{"Cookie", "user_session=" + us},
					{"X-Frontend-Id", "1"},
					{"X-Frontend-Version", "6.14.1"},
					{"Connection", "keep-alive"},
					{"Upgrade-Insecure-Requests", "1"},
				};
			var res = getResStr(url, _h, false);
			if (res == null) {
				util.debugWriteLine("getMyName res null url " + url);
				return null;
			}
			var n = util.getRegGroup(res, "\"nickname\":\"(.+?)\"");
			return n;
		} catch (Exception e) {
			util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
		}
		return null;
	}
    public static List<Control> getChildControls(Control c) {
		//util.debugWriteLine("cname " + c.Name);
		var ret = new List<Control>();
		foreach (Control _c in c.Controls) {
			var children = getChildControls(_c);
			ret.Add(_c);
			ret.AddRange(children);
			//util.debugWriteLine(c.Name + " " + children.Count);
		}
		//util.debugWriteLine(c.Name + " " + ret.Count);
		return ret;
	}
    public static void setFontSize(float size, Form form, bool isKeepSize, int baseHeight = -1) {
    	try {
    		var workingArea = Screen.GetWorkingArea(new Point(0,0));
    		
			var _formsize = form.Size;
			var _bStyle = form.FormBorderStyle;
			
			baseHeight = baseHeight == -1 ? form.Height : baseHeight;
			
			if (size > form.Font.Size)
				form.Size = new Size(isKeepSize ? 918 : form.Width, baseHeight);
			
			var max = (int)(form.Font.Size * (workingArea.Height * 0.9 / form.Height));
			
			if (size > max) {
				size = max;
				util.showMessageBoxCenterForm(form, "画面上に表示できなくなる可能性があるため、" + size + "に設定されます");
			}
			
			form.Font = new Font(form.Font.FontFamily, size);
			if (isKeepSize) {
				//form.Size = _formsize;
			}
			
			var controls = util.getChildControls(form);
			foreach (Control c in controls) {
				if (c.ContextMenuStrip != null)
					c.ContextMenuStrip.Font = new Font(c.Font.FontFamily, size);

				if (c is DataGridView) {
					((DataGridView)c).ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
					((DataGridView)c).ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
					
					((DataGridView)c).RowTemplate.Height = (int)size;
					((DataGridView)c).AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
				}
				if (c is StatusStrip) {
					foreach (ToolStripLabel s in ((StatusStrip)c).Items)
						s.Font = new Font(c.Font.FontFamily, size);
				}
				c.Font = new Font(c.Font.FontFamily, size);
			}
    	} catch (Exception e) {
    		util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
    	}
	}
    public static void releaseMutex(Mutex mutex) {
    	try {
			if (mutex != null) {
				mutex.ReleaseMutex();
				mutex.Close();
			}
		} catch (Exception ee) {
			util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
		}
    }
    public static Mutex doubleRunCheck() {
		string appName = "ニコ生放送チェックツール";
		var mutex = new System.Threading.Mutex(false, appName);
		bool hasHandle = false;
		try {
			try {
	            hasHandle = mutex.WaitOne(0, false);
	        }
			catch (System.Threading.AbandonedMutexException) {
	            hasHandle = true;
	        }
			if (!hasHandle) {
	            util.showMessageBoxCenterForm(null, "すでに起動しています。2つ同時に起動できません。システムトレイを確認してください。", "ニコ生放送チェックツール（仮の多重起動禁止");
	            return null;
	        }
			return mutex;
		} catch (Exception e) {
			util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
		}
	    return null;
    }
    public static Icon changeIconColor(Icon icon, Color c) {
    	try {
	    	var b = icon.ToBitmap();
			for (var i = 0; i < b.Width; i++) {
				for (var j = 0; j < b.Height; j++) {
					//util.debugWriteLine(i + " " + j + " " + b.GetPixel(i, j));
					if (b.GetPixel(i, j).A == 255)
						b.SetPixel(i, j, c);
				}
			}
			return Icon.FromHandle(b.GetHicon());
    	} catch (Exception e) {
    		util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
    		return icon;
    	}
    }
    [DllImport("user32.dll")]
	static extern IntPtr GetWindowLong(IntPtr hWnd, int nIndex);
	[DllImport("kernel32.dll")]
	public static extern IntPtr GetCurrentThreadId();
	public delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);
	[DllImport("user32.dll")]
	public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
	[DllImport("user32.dll")]
	static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);
	[DllImport("user32.dll")]
	static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, IntPtr threadId);
	[DllImport("user32.dll")]
	public static extern bool UnhookWindowsHookEx(IntPtr hHook);
	[DllImport("user32.dll")]
	public static extern IntPtr CallNextHookEx(IntPtr hHook, int nCode, IntPtr wParam, IntPtr lParam);
	public struct RECT {
		public int left;
		public int top;
		public int right;
		public int bottom;
	}
	private static Form messageBoxOwnerForm = null;
	private static IntPtr mBHook;
	private static IntPtr CBTProc(int nCode, IntPtr wParam, IntPtr lParam) {
		var HCBT_ACTIVATE = 5;
		if (nCode == HCBT_ACTIVATE) {
			RECT rectF, rectM; 
			GetWindowRect(messageBoxOwnerForm.Handle, out rectF);
			GetWindowRect(wParam, out rectM);
			var x = rectF.left + ((rectF.right - rectF.left) - (rectM.right - rectM.left)) / 2;
			var y = rectF.top + ((rectF.bottom - rectF.top) - (rectM.bottom - rectM.top)) / 2;
			
			uint SWP_NOSIZE = 1;
			uint SWP_NOZORDER = 4;
			uint SWP_NOACTIVATE = 16;
			if (x >= 0 && y >= 0)
				SetWindowPos(wParam, 0, x, y, 0, 0, 
						SWP_NOSIZE | SWP_NOZORDER | SWP_NOACTIVATE);
			UnhookWindowsHookEx(mBHook);
		}
		return CallNextHookEx(mBHook, nCode, wParam, lParam);
	}
	public static DialogResult showMessageBoxCenterForm(Form form, string text, string caption = "", MessageBoxButtons btn = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.None, MessageBoxDefaultButton defBtn = MessageBoxDefaultButton.Button1) {
		if (form != null) {
			var GWL_HINSTANCE = -6;
			var hInstance = GetWindowLong(form.Handle, GWL_HINSTANCE);
		    var threadId = GetCurrentThreadId();
		    var whCbt = 5;
		    messageBoxOwnerForm = form;
		    mBHook = SetWindowsHookEx(whCbt, new HookProc(CBTProc), hInstance, threadId);
		}
		return System.Windows.Forms.MessageBox.Show(text, caption, btn, icon);
	}
	public static Dictionary<string, string> getHeader(CookieContainer cc, string referer, string url) {
		var ret = new Dictionary<string, string>() {
			{"Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,*/*;q=0.8"},
			{"Accept-Language", "ja,en-US;q=0.7,en;q=0.3"},
			{"Cache-Control", "no-cache"},
			{"User-Agent", userAgent}
		};
		if (cc != null) ret["Cookie"] = cc.GetCookieHeader(new Uri(url));
		if (referer != null) ret["Referer"] = referer;
		return ret;
	}
	public static bool isUseCurl(CurlHttpVersion httpVer) {
		if (httpVer != CurlHttpVersion.CURL_HTTP_VERSION_1_1) return true;
		if ((util.osName != null && 
		     (util.osName.IndexOf("Windows 1") > -1)) || util.isWebRequestOk)
			return false;
		return util.isCurl;
	}
	public static bool libcurlWsDllCheck(out string mes) {
		var path = getJarPath()[0];
		var dlls = new string[]{"libssl-3.dll",
				"libcrypto-3.dll", "nghttp2.dll"};
		mes = "";
		foreach (var n in dlls) {
			if (!File.Exists(path + "/" + n)) 
				mes += (mes == "" ? "" : ",") + n;
		}
		util.debugWriteLine("libcurlWsDllCheck " + mes);
		return mes == ""; 
	}
	public static bool vcr140Check(out string mes) {
		mes = "";
		bool isExists = false;
		try {
			var pp = Environment.GetEnvironmentVariable("PATH");
			if (pp == null) return false;
			var l = pp.Split(';');
			foreach (var _l in l) {
				var d = _l.Trim(new char[]{' ', '/', '\\'});
				if (File.Exists(d + "/VCRUNTIME140.dll"))
					isExists = true; 
			}
			if (!isExists) mes += "環境変数PATHの中にVCRUNTIME140.dllが見つかりませんでした";
		} catch (Exception e) {
			util.debugWriteLine(e.Message + e.Source + e.StackTrace);
			mes += e.Message + e.Source + e.StackTrace;
		}
		util.debugWriteLine("vcr140Check " + mes + " " + isExists);
		return isExists;
	}
	[DllImport("kernel32.dll")]
	extern static int SetThreadExecutionState(uint esFlags);
	public static void setThreadExecutionState() {
		try {
			//var r = SetThreadExecutionState(1 | 2 | 0x80000000);
			//var r = SetThreadExecutionState(1 | 0x80000000);
			var r = SetThreadExecutionState(1);
			util.debugWriteLine("setThreadExecutionState " + r);
			
		} catch (Exception e) {
			util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
		}
	}
	public static RssItem getRiFromLvid(string lvid, CookieContainer cc) {
		try {
			var hg = new namaichi.rec.HosoInfoGetter();
			var r = hg.get(lvid, cc);
			
			var i = new RssItem(hg.title, lvid, hg.openDt.ToString("yyyy\"/\"MM\"/\"dd HH\":\"mm\":\"ss"), hg.description, hg.group, hg.communityId, hg.userName, hg.thumbnail, hg.isMemberOnly.ToString(), "", hg.isPayment);
			i.setUserId(hg.userId);
			i.setTag(hg.tags);
			i.category = hg.category;
			i.type = hg.type;
			i.pubDateDt = hg.dt;
			return i;
		} catch (Exception e) {
			util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			return null;
		}
	}
	[DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    public static extern bool CreateProcess(
        string lpApplicationName,
        string lpCommandLine,
        IntPtr lpProcessAttributes,
        IntPtr lpThreadAttributes,
        bool bInheritHandles,
        uint dwCreationFlags,
        IntPtr lpEnvironment,
        string lpCurrentDirectory,
        ref STARTUPINFO lpStartupInfo,
        out PROCESS_INFORMATION lpProcessInformation);

    [StructLayout(LayoutKind.Sequential)]
    public struct STARTUPINFO
    {
        public uint cb;
        public string lpReserved;
        public string lpDesktop;
        public string lpTitle;
        public uint dwX;
        public uint dwY;
        public uint dwXSize;
        public uint dwYSize;
        public uint dwXCountChars;
        public uint dwYCountChars;
        public uint dwFillAttribute;
        public uint dwFlags;
        public ushort wShowWindow;
        public ushort cbReserved2;
        public IntPtr lpReserved2;
        public IntPtr hStdInput;
        public IntPtr hStdOutput;
        public IntPtr hStdError;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PROCESS_INFORMATION
    {
        public IntPtr hProcess;
        public IntPtr hThread;
        public uint dwProcessId;
        public uint dwThreadId;
    }
	public static bool CreateProcess(string f, string arg, bool isMin) {
		//if (f == null || arg == null) return;
		if (f == null) return false;
			
		STARTUPINFO si = new STARTUPINFO();
		PROCESS_INFORMATION pi = new PROCESS_INFORMATION();
		
		si.cb = (uint)Marshal.SizeOf(si);
		
		si.dwFlags = 0x00000001; //STARTF_USESHOWWINDOW
		ushort SW_SHOWNORMAL = 1;
		ushort SW_SHOWMINNOACTIVE = 7;
		si.wShowWindow = isMin ? SW_SHOWMINNOACTIVE : SW_SHOWNORMAL;
		bool r = CreateProcess(
				null,
				f + " " + arg,
				IntPtr.Zero,
				IntPtr.Zero,
				false,
				0,
				IntPtr.Zero,
				null,
				ref si,
				out pi);
		if (!r) {
			Thread.Sleep(1000);
		}
		util.debugWriteLine("create process " + r + " " + f + " " + arg + " ismin" + isMin);
		return r;
	}
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct SHELLEXECUTEINFO
    {
        public int cbSize;
        public uint fMask;
        public IntPtr hwnd;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string lpVerb;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string lpFile;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string lpParameters;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string lpDirectory;
        public int nShow;
        public IntPtr hInstApp;
        public IntPtr lpIDList;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string lpClass;
        public IntPtr hkeyClass;
        public uint dwHotKey;
        public IntPtr hIconOrMonitor;
        public IntPtr hProcess;
    }
    [DllImport("shell32.dll", CharSet = CharSet.Auto)]
    static extern bool ShellExecuteEx(ref SHELLEXECUTEINFO lpExecInfo);
    
    public static bool ShellExecute(string f, string arg, bool isMin) {
		if (f == null) return false;
		
		const int SW_SHOWNORMAL = 1;
		//const int SW_SHOWMINIMIZED = 2;
		//const int SW_SHOWNOACTIVATE = 4;
		const int SW_SHOWMINNOACTIVE = 7;
    	//const uint SEE_MASK_NOCLOSEPROCESS = 0x00000040;
    	
		SHELLEXECUTEINFO sei = new SHELLEXECUTEINFO();
        sei.cbSize = Marshal.SizeOf(sei);
        sei.lpVerb = "open";
        sei.lpFile = f;
        if (!string.IsNullOrEmpty(arg)) sei.lpParameters = arg;
        sei.nShow = isMin ? SW_SHOWMINNOACTIVE : SW_SHOWNORMAL;
        //sei.fMask = SEE_MASK_NOCLOSEPROCESS;

        var r = ShellExecuteEx(ref sei);
        
		if (!r) {
			Thread.Sleep(1000);
		}
		util.debugWriteLine("create process " + r + " " + f + " " + arg + " ismin" + isMin);
		return r;
	}
}
