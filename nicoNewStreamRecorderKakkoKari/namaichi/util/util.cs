using Microsoft.Win32;
using namaichi;
using namaichi.config;
using namaichi.info;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

class app
{
	public static void Mains(string[] args)
	{
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
class util
{
	public static string versionStr = "ver0.1.7.50";
	public static string versionDayStr = "2019/12/28";
	public static bool isShowWindow = true;
	public static bool isStdIO = false;
	public static string[] jarPath = null;

	private static HttpClient httpClient = new HttpClient(new HttpClientHandler { UseCookies = false });

	public static string getRegGroup(string target, string reg, int group = 1, Regex r = null)
	{
		if (r == null)
			r = new Regex(reg);
		var m = r.Match(target);
		//		Console.WriteLine(m.Groups.Count +""+ m.Groups[0]);
		if (m.Groups.Count > group)
		{
			return m.Groups[group].ToString();
		}
		else return null;
	}
	public static string getPath()
	{
		string p = System.IO.Path.GetDirectoryName(
			System.IO.Path.GetFullPath(
			System.Reflection.Assembly.GetExecutingAssembly().Location));
		//		Console.WriteLine(p);
		return p;
	}
	public static string getTime()
	{
		return DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

	}
	public static int getUnixTime()
	{
		return (int)(((TimeSpan)(DateTime.Now - new DateTime(1970, 1, 1))).TotalSeconds);
	}
	public static String[] getJarPath()
	{
		if (jarPath != null) return jarPath;

		bool isTestMode = false;

		if (isTestMode)
		{
			return new String[] { "C:\\Users\\pc\\desktop", "util", "exe" };
		}
		else
		{
			//			string f=Environment.GetCommandLineArgs()[0];
			string f = System.Reflection.Assembly.GetExecutingAssembly().Location;
			//			util.debugWriteLine(Environment.GetCommandLineArgs().Length);
			f = System.IO.Path.GetFileName(f);

			string withoutKakutyousi = (f.IndexOf(".") < 0) ? f :
					util.getRegGroup(f, "^(.*)\\.");
			string kakutyousi = (f.IndexOf(".") < 0) ? null :
					util.getRegGroup(f, "^.*\\.(.*)");

			util.debugWriteLine(getPath() + " " + withoutKakutyousi + " " + kakutyousi);
			//0-dir 1-withoutKakutyousi 2-kakutyousi
			var ret = new String[] { getPath(), withoutKakutyousi, kakutyousi };
			jarPath = ret;
			return ret;
		}
	}

	/*
	public static string[] getRecFolderFilePath(string host, 
			string group, string title, string lvId, 
			string communityNum, string userId, config cfg, 
			bool isTimeShift, TimeShiftConfig tsConfig, 
			long _openTime, bool isRtmp) {
		
		host = getOkFileName(host, isRtmp);
		group = getOkFileName(group, isRtmp);
		title = getOkFileName(title, isRtmp);
		
		string[] jarpath = getJarPath();
//		util.debugWriteLine(jarpath);
		//string dirPath = jarpath[0] + "\\rec\\" + host;
		string _dirPath = (cfg.get("IsdefaultRecordDir") == "true") ?
			(jarpath[0] + "\\rec") : cfg.get("recordDir");
		string dirPath = _dirPath;
		
		string sfn = null;
		if (cfg.get("IscreateSubfolder") == "true") {
			sfn = getSubFolderName(host, group, title, lvId, communityNum, userId,  cfg);
			if (sfn.Length > 120) sfn = sfn.Substring(0, 120);
			if (sfn == null) return null;
			dirPath += "/" + sfn;
		}


		var segmentSaveType = cfg.get("segmentSaveType");
		if (cfg.get("EngineMode") != "0" || isRtmp) segmentSaveType = "0";
		
		bool _isTimeShift = isTimeShift;
		if (cfg.get("EngineMode") != "0") _isTimeShift = false;

		var name = getFileName(host, group, title, lvId, communityNum,  cfg, _openTime);
		if (name.Length > 200) name = name.Substring(0, 200);
		
		//�����p�X����
		if (name.Length + dirPath.Length > 234) {
			name = lvId;
			if (name.Length + dirPath.Length > 234 && sfn != null) {
				sfn = sfn.Substring(0, 3);
				dirPath = _dirPath + "/" + sfn;
								
				if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);
				if (!Directory.Exists(dirPath)) return null;
				
			}
		}
		if (name.Length + dirPath.Length > 234) return new string[]{null, name + " " + dirPath, null};
		
		if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);
		if (!Directory.Exists(dirPath)) return null;
		
		var files = Directory.GetFiles(dirPath);
		string existFile = null;
		string existDt = null;
		string existDtFile = null;
		for (int i = 0; i < 1000000; i++) {
			var fName = dirPath + "/" + name + "_" + ((_isTimeShift) ? "ts" : "") + i.ToString();
			var originName = dirPath + "/" + name;
			util.debugWriteLine(dirPath + " " + fName);
			
			if (!_isTimeShift) {
				if (segmentSaveType == "0" && isExistAllExt(fName)) continue;
				else if (segmentSaveType == "1") {
					if (Directory.Exists(fName)) continue;
					Directory.CreateDirectory(fName);
					if (!Directory.Exists(fName)) return null;
				}
				
				string[] reta = {dirPath, fName, originName};
				return reta;
			} else {
				if (isRtmp) {
					if (isExistAllExt(fName)) continue;
					string[] reta = {dirPath, fName, originName};
					return reta;
				}
				
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
			}
		}
		return null;
	}
	*/
	public static string getOkFileName(string name, bool isRtmp)
	{
		if (isRtmp) name = getOkSJisOut(name);

		name = name.Replace("\\", "��");
		name = name.Replace("/", "�^");
		name = name.Replace(":", "�F");
		name = name.Replace("*", "��");
		name = name.Replace("?", "�H");
		name = name.Replace("\"", "�h");
		name = name.Replace("<", "��");
		name = name.Replace(">", "��");
		name = name.Replace("|", "�b");
		/*
		string[] replaceCharacter = {"\\", "/", ":", "*", "?", "\"", "<", ">", "|"};
		foreach (string s in replaceCharacter) {
			name = name.Replace(s, "_");
		}
		*/
		return name;
	}
	private static string getSubFolderName(string host, string group, string title, string lvId, string communityNum, string userId, config cfg)
	{
		var n = cfg.get("subFolderNameType");
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
	private static string getFileName(string host, string group, string title, string lvId, string communityNum, config cfg, long _openTime)
	{
		var n = cfg.get("fileNameType");
		//var _hiduke = DateTime.Now;
		var _hiduke = getUnixToDatetime(_openTime);
		var month = (_hiduke.Month < 10) ? ("0" + _hiduke.Month.ToString()) : (_hiduke.Month.ToString());
		var day = (_hiduke.Day < 10) ? ("0" + _hiduke.Day.ToString()) : (_hiduke.Day.ToString());
		var hiduke = _hiduke.Year + "�N" + month + "��" + day + "��";
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
		else if (n == "10") return getDokujiSetteiFileName(host, group, title, lvId, communityNum, cfg.get("filenameformat"), _hiduke);
		else return host + "_" + communityNum + "(" + group + ")_" + lvId + "(" + title + ")";


	}
	public static string getDokujiSetteiFileName(string host, string group, string title, string lvId, string communityNum, string format, DateTime _openTime)
	{
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
		type = getOkFileName(type, false);
		return type;

	}
	public static string getFileNameTypeSample(string filenametype)
	{
		//var format = cfg.get("filenameformat");
		return getDokujiSetteiFileName("�����Җ�", "�R�~����", "�^�C�g��", "lv12345", "co9876", filenametype, DateTime.Now);
	}
	public static string getOkCommentFileName(config cfg, string fName, string lvid, bool isTimeShift, bool isRtmp)
	{
		var kakutyousi = (cfg.get("IsgetcommentXml") == "true") ? ".xml" : ".json";
		var engineMode = cfg.get("EngineMode");
		if (cfg.get("segmentSaveType") == "0" || engineMode != "0" || isRtmp)
		{
			//renketu
			if (isTimeShift && engineMode == "0" && !isRtmp)
			{
				var time = getRegGroup(fName, "(_\\d+h\\d+m\\d+s_)");
				fName = fName.Replace(time, "");
			}
			util.debugWriteLine("comment file path " + fName + kakutyousi);
			return fName + kakutyousi;
		}
		else
		{

			var name = util.getRegGroup(fName, ".+/(.+)");
			if (fName.Length + name.Length > 245) name = lvid;
			util.debugWriteLine("comment file path " + fName + "/" + name + kakutyousi);
			return fName + "/" + name + kakutyousi;
		}
	}
	public static string getLastTimeshiftFileName(string host,
			string group, string title, string lvId, string communityNum,
			string userId, config cfg, long _openTime)
	{
		host = getOkFileName(host, false);
		group = getOkFileName(group, false);
		title = getOkFileName(title, false);

		string[] jarpath = getJarPath();
		//		util.debugWriteLine(jarpath);
		//string dirPath = jarpath[0] + "\\rec\\" + host;
		string _dirPath = (cfg.get("IsdefaultRecordDir") == "true") ?
			(jarpath[0] + "\\rec") : cfg.get("recordDir");
		string dirPath = _dirPath;

		string sfn = null;
		if (cfg.get("IscreateSubfolder") == "true")
		{
			sfn = getSubFolderName(host, group, title, lvId, communityNum, userId, cfg);
			if (sfn.Length > 120) sfn = sfn.Substring(0, 120);
			if (sfn == null) return null;
			dirPath += "/" + sfn;
		}
		util.debugWriteLine("getLastTimeshiftFileName dirPath " + dirPath + " sfn " + sfn);

		var segmentSaveType = cfg.get("segmentSaveType");

		var name = getFileName(host, group, title, lvId, communityNum, cfg, _openTime);
		if (name.Length > 200) name = name.Substring(0, 200);

		util.debugWriteLine("getLastTimeshiftFileName name " + name);

		//�����p�X����
		if (name.Length + dirPath.Length > 234)
		{
			name = lvId;
			if (name.Length + dirPath.Length > 234 && sfn != null)
			{
				sfn = sfn.Substring(0, 3);
				dirPath = _dirPath + "/" + sfn;

				//				if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);

				if (!Directory.Exists(dirPath))
				{
					util.debugWriteLine("getLastTS FN too long not exist dir path " + dirPath);
					return null;
				}

			}
		}

		if (name.Length + dirPath.Length > 234)
		{
			util.debugWriteLine("too long " + name + " " + dirPath + " " + name.Length + " " + dirPath.Length);
			return null;
		}

		//		if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);
		if (!Directory.Exists(dirPath))
		{
			util.debugWriteLine("no exists " + dirPath);
			return null;
		}

		util.debugWriteLine("getLast TS FN 00");

		string existFile = null;
		var files = Directory.GetFiles(dirPath);
		for (int i = 0; i < 1000; i++)
		{
			var fName = dirPath + "/" + name + "_" + "ts" + i.ToString();

			if (segmentSaveType == "0")
			{
				//util.existFile(dirPath, name + "_ts_\\d+h\\d+m\\d+s_" + i.ToString());
				var _existFile = util.existFile(files, "_ts_(\\d+h\\d+m\\d+s)_" + i.ToString() + ".ts", name);
				util.debugWriteLine("getLastTimeshiftFileName existfile " + _existFile);
				if (_existFile != null)
				{
					existFile = _existFile;
					continue;
				}
			}

			if (segmentSaveType == "1")
			{
				if (Directory.Exists(fName))
				{
					return fName;
				}
				if (File.Exists(fName)) continue;

				/*
				try {
					//Directory.CreateDirectory(fName);
					if (Directory.Exists(fName)) return fName;
					continue;
				} catch (Exception e) {
					util.debugWriteLine("get last timeshift file create dir exception " + fName + e.Message + e.StackTrace + e.Source + e.TargetSite);
					continue;
				}
				*/

				return null;
			}

			util.debugWriteLine("getLastTimeshiftFileName dirpath " + dirPath + " fname " + fName);

			if (i == 0)
			{
				util.debugWriteLine("last timeshift file " + dirPath + "/" + name + "_" + "ts" + (i - 0).ToString());
				return null;
				//			} else util.debugWriteLine("last timeshift file " + dirPath + "/" + name + "_" + "ts" + (i - 1).ToString());
			}
			else util.debugWriteLine("last timeshift file " + existFile);
			//			return dirPath + "/" + name + "_" + "ts" + (i - 1).ToString();
			return existFile;
			//			string[] ret = {dirPath, dirPath + "/" + name + "_" + "ts" + (i - 1).ToString()};
		}
		return null;
	}
	public static string[] getLastTimeShiftFileTime(string lastFile, string segmentSaveType)
	{
		if (lastFile == null) return null;
		string fname = null;
		if (segmentSaveType == "0")
		{
			fname = lastFile + "";
		}
		else
		{
			var ss = new List<string>();
			var key = new List<int>();
			foreach (var f in Directory.GetFiles(lastFile))
			{
				if (!f.EndsWith(".ts")) continue;
				var name = util.getRegGroup(f, ".+\\\\(.+)");
				if (name == null) continue;
				if (util.getRegGroup(name, "(\\d+h\\d+m\\d+s)") == null) continue; ;
				var _k = util.getRegGroup(name, "(\\d+)");
				if (_k == null) continue;
				ss.Add(f);
				key.Add(int.Parse(_k));

			}
			if (ss.Count == 0) return null;
			var ssArr = ss.ToArray();
			Array.Sort(key.ToArray(), ssArr);
			fname = ssArr[ssArr.Length - 1];
		}
		if (!File.Exists(fname)) return null;
		var _name = util.getRegGroup(fname, ".+\\\\(.+)");
		var h = util.getRegGroup(_name, "(\\d+)h");
		var m = util.getRegGroup(_name, "(\\d+)m");
		var s = util.getRegGroup(_name, "(\\d+)s");
		if (h == null || m == null || s == null) return null;
		var ret = new string[] { h, m, s };
		return ret;
	}
	private static bool isExistAllExt(string fName)
	{
		var ext = new string[] {".ts", ".xml", ".flv", ".avi", ".mp4",
				".mov", ".wmv", ".vob", ".mkv", ".mp3",
				".wav", ".wma", ".aac", ".ogg"};
		foreach (var e in ext)
			if (File.Exists(fName + e)) return true;
		return false;
	}
	public static string incrementRecFolderFile(string recFolderFile)
	{
		if (recFolderFile.EndsWith("xml") || recFolderFile.EndsWith("json"))
		{
			var r = new Regex("(\\d+)\\.(xml|json)$");
			var m = r.Match(recFolderFile);
			if (m == null || m.Length <= 0) return null;//rp.getRecFilePath()[1];

			for (int i = int.Parse(m.Groups[1].Value); i < 10000; i++)
			{
				var _new = (i + 1).ToString() + "." + m.Groups[2];
				var _ret = r.Replace(recFolderFile, _new);
				if (File.Exists(_ret)) continue;
				return _ret;
			}
		}
		else
		{
			var r = new Regex("(\\d+)$");
			var m = r.Match(recFolderFile);
			if (m == null || m.Length <= 0) return null;//rp.getRecFilePath()[1];

			for (int i = int.Parse(m.Groups[1].Value); i < 10000; i++)
			{
				var _new = (int.Parse(m.Groups[1].Value) + 1).ToString();
				var _ret = r.Replace(recFolderFile, _new);
				if (File.Exists(_ret)) continue;
				return _ret;
			}
		}
		return null;
	}
	public static string getPageSource(string _url, ref WebHeaderCollection getheaders, CookieContainer container = null, string referer = null, bool isFirstLog = true, int timeoutMs = 5000)
	{
		//util.debugWriteLine("getpage 01");
		timeoutMs = 5000;
		/*
		string a;
		try {
//			a = container.GetCookieHeader(new Uri(_url));
		} catch (Exception e) {
			util.debugWriteLine("getpage get cookie header error " + _url + e.Message+e.StackTrace);
			return null;
		}
		*/
		//		if (isFirstLog)
		//			util.debugWriteLine("getpagesource " + _url + " ");

		//		util.debugWriteLine("getpage 02");
		for (int i = 0; i < 1; i++)
		{
			try
			{
				var isWebRequest = true;
				if (isWebRequest)
				{
					var req = (HttpWebRequest)WebRequest.Create(_url);

					req.Proxy = null;
					req.AllowAutoRedirect = true;
					if (referer != null) req.Referer = referer;
					if (container != null) req.CookieContainer = container;
					req.UserAgent = "NicoLiveCheckTool " + versionStr + " guestnicon@gmail.com";

					req.Timeout = timeoutMs;
					var res = (HttpWebResponse)req.GetResponse();
					using (var dataStream = res.GetResponseStream())
					using (var reader = new StreamReader(dataStream))
					{
						var resStr = reader.ReadToEnd();
						getheaders = res.Headers;

						//dataStream.Dispose();
						//reader.Dispose();
						return resStr;
					}
				}
				else
				{
					//var handler = new HttpClientHandler();
					//handler.CookieContainer = container;
					var s = getHttpStringAsync(container, _url).Result;
					return s;

				}
			}
			catch (Exception e)
			{
				System.Threading.Tasks.Task.Factory.StartNew(() =>
				{
					util.debugWriteLine("getpage error " + _url + e.Message + e.StackTrace);
				});
				//				System.Threading.Thread.Sleep(3000);
				continue;
			}
		}

		return null;
	}
	public static string getPageSource(string _url, CookieContainer container = null, string referer = null, bool isFirstLog = true, int timeoutMs = 5000)
	{
		timeoutMs = 5000;
		/*
		string a = "";
		try {
//			a = container.GetCookieHeader(new Uri(_url));
		} catch (Exception e) {
			util.debugWriteLine("getpage get cookie header error " + _url + e.Message+e.StackTrace);
			return null;
		}
		if (isFirstLog)
			util.debugWriteLine("getpagesource " + _url + " " + a);
		*/
		//		util.debugWriteLine("getpage 02");
		for (int i = 0; i < 1; i++)
		{
			try
			{
				var isWebRequest = true;
				if (isWebRequest)
				{
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

					req.Timeout = timeoutMs;
					//				util.debugWriteLine("getpage 0");
					var res = (HttpWebResponse)req.GetResponse();
					//				util.debugWriteLine("getpage 1");
					using (var dataStream = res.GetResponseStream())
					using (var reader = new StreamReader(dataStream))
					{

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
				else
				{
					//var handler = new HttpClientHandler();
					//handler.CookieContainer = container;
					var s = getHttpStringAsync(container, _url).Result;

					return s;

				}
			}
			catch (Exception e)
			{
				System.Threading.Tasks.Task.Factory.StartNew(() =>
				{
					util.debugWriteLine("getpage error " + _url + e.Message + e.StackTrace);
				});
				//				System.Threading.Thread.Sleep(3000);
				continue;
			}
		}

		return null;
	}
	async private static System.Threading.Tasks.Task<string> getHttpStringAsync(CookieContainer container, string url)
	{
		try
		{
			if (container != null)
				httpClient.DefaultRequestHeaders.Add("Cookie", container.GetCookieHeader(new Uri(url)));
			var s = await httpClient.GetStringAsync(url);
			return s;
		}
		catch (Exception e)
		{
			util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			return null;
		}
	}
	public static byte[] getFileBytes(string url, CookieContainer container)
	{
		//		var a = container.GetCookieHeader(new Uri(_url));
		//util.debugWriteLine("getfilebyte " + url);
		for (int i = 0; i < 1; i++)
		{
			try
			{
				var req = (HttpWebRequest)WebRequest.Create(url);
				req.Proxy = null;
				req.AllowAutoRedirect = true;
				req.Timeout = 2000;
				//			req.Headers = getheaders;
				//				if (referer != null) req.Referer = referer;
				if (container != null) req.CookieContainer = container;
				var res = (HttpWebResponse)req.GetResponse();
				byte[] ret = null;
				using (var dataStream = res.GetResponseStream())
				{

					//test
					var isMs = true;
					if (isMs)
					{
						using (var ms = new MemoryStream())
						{
							dataStream.CopyTo(ms);
							ret = ms.ToArray();
						}
					}
					else
					{
						//				var reader = new StreamReader(dataStream);
						byte[] b = new byte[10000000];
						int pos = 0;
						var r = 0;
						while ((r = dataStream.Read(b, pos, 1000000)) > 0)
						{
							//					if (dataStream.Read(b, (int)j, (int)dataStream.Length) == 0) break;
							//					j = dataStream.Position;
							pos += r;
						}
						Array.Resize(ref b, pos);
						ret = b;
					}
				}
				return ret;
			}
			catch (Exception e)
			{
				System.Threading.Tasks.Task.Factory.StartNew(() =>
				{
					util.debugWriteLine("getfile error " + url + e.Message + e.StackTrace);
				});
				//				System.Threading.Thread.Sleep(3000);
				continue;
			}
		}
		return null;
	}
	public static string postResStr(string url, Dictionary<string, string> headers, byte[] content)
	{
		try
		{
			var res = sendRequest(url, headers, content, "POST");
			if (res == null)
			{
				debugWriteLine("postResStr res null");
				return null;
			}

			debugWriteLine(res.StatusCode + " " + res.StatusDescription);

			//var resStream = res.GetResponseStream();
			using (var getResStream = res.GetResponseStream())
			using (var resStream = new System.IO.StreamReader(getResStream))
			{
				//foreach (var h in res.Headers) Debug.WriteLine("header " + h + " " + res.Headers[h.ToString()]);
				/*
				List<byte> rb = new List<byte>();
				for (var i = 0; i < 10; i++) {
					var a = new byte[100000];
					var readC = resStream.Read(a, 0, a.Length);
					if (readC == 0) break;
					Debug.WriteLine("read c " + readC);
					for (var j = 0; j < readC; j++) rb.Add(a[j]);
					
					Debug.WriteLine("read " + i);
				}
				*/
				var resStr = resStream.ReadToEnd();
				//return getRegGroup(resStr,

				return resStr;
			}
		}
		catch (Exception ee)
		{
			debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			return null;
		}
	}
	public static byte[] postResBytes(string url, Dictionary<string, string> headers, byte[] content)
	{
		try
		{
			var res = sendRequest(url, headers, content, "POST");

			debugWriteLine(res.StatusCode + " " + res.StatusDescription);

			//var resStream = res.GetResponseStream();
			using (var resStream = res.GetResponseStream())
			{
				var buf = new List<byte>();
				for (var k = 0; k < 10; k++)
				{
					var b = new byte[1000];
					var c = resStream.Read(b, 0, b.Length);
					if (c == 0) break;
					for (var j = 0; j < c; j++) buf.Add(b[j]);
				}
				return buf.ToArray();
			}

		}
		catch (Exception ee)
		{
			debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			return null;
		}
	}
	public static HttpWebResponse sendRequest(string url, Dictionary<string, string> headers, byte[] content, string method)
	{
		try
		{
			var req = (HttpWebRequest)WebRequest.Create(url);
			req.Method = method;
			req.Proxy = null;

			foreach (var h in headers)
			{
				if (h.Key.ToLower().Replace("-", "") == "contenttype")
					req.ContentType = h.Value;
				else if (h.Key.ToLower().Replace("-", "") == "useragent")
					req.UserAgent = h.Value;
				else if (h.Key.ToLower().Replace("-", "") == "connection")
					req.KeepAlive = h.Value.ToLower().Replace("-", "") == "keepalive";
				else req.Headers.Add(h.Key, h.Value);
			}

			if (content != null)
			{
				using (var stream = req.GetRequestStream())
				{
					try
					{
						stream.Write(content, 0, content.Length);
					}
					catch (Exception ee)
					{
						debugWriteLine(ee.Message + " " + ee.StackTrace + " " + ee.Source + " " + ee.TargetSite);
					}
				}
			}
			//					stream.Close();

			return (HttpWebResponse)req.GetResponse();
		}
		catch (Exception ee)
		{
			debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			return null;
		}
	}

	public static bool isEndedProgram(string lvid, CookieContainer container, bool isSub)
	{
		var url = "https://live2.nicovideo.jp/watch/" + lvid;

		var res = util.getPageSource(url, container);
		util.debugWriteLine("isendedprogram url " + url + " res==null " + (res == null) + util.getMainSubStr(isSub, true));
		//			util.debugWriteLine("isendedprogram res " + res + util.getMainSubStr(isSub, true));
		if (res == null) return false;
		var isEnd = res.IndexOf("\"content_status\":\"closed\"") != -1 ||
				res.IndexOf("<title>�ԑg���݂���܂���") != -1 ||
				res.IndexOf("�ԑg��������܂���</span>") != -1;
		util.debugWriteLine("is ended program " + isEnd + util.getMainSubStr(isSub, true));
		return isEnd;
	}
	public static string existFile(string[] files, string reg, string startWith)
	{
		//		var files = Directory.GetFiles(dirPath);
		foreach (var f in files)
		{
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
	public static string getSecondsToStr(double seconds)
	{
		//		var dotSecond = ((int)((seconds % 1) * 10)).ToString("0");
		var second = ((int)((seconds % 60) * 1)).ToString("00");
		var minute = ((int)((seconds % 3600 / 60))).ToString("00");
		var hour = ((int)((seconds / 3600) * 1));
		var _hour = (hour < 100) ? hour.ToString("00") : hour.ToString(); ;
		var timeStr = _hour + "h" + minute + "m" + second + "s";
		return timeStr;
	}
	public static int getSecondsFromStr(string _s)
	{
		var h = getRegGroup(_s, "(\\d+)h");
		var m = getRegGroup(_s, "(\\d+)m");
		var s = getRegGroup(_s, "(\\d+)s");
		if (h == null || m == null || s == null) return -1;
		return int.Parse(h) * 3600 + int.Parse(m) * 60 + int.Parse(s);
	}
	public static int getPageType(string res)
	{
		//if (res.IndexOf("siteId&quot;:&quot;nicolive2") > -1) {
		var data = util.getRegGroup(res, "<script id=\"embedded-data\" data-props=\"([\\d\\D]+?)</script>");
		var status = (data == null) ? null : util.getRegGroup(data, "&quot;status&quot;:&quot;(.+?)&quot;");
		if (res.IndexOf("<!doctype html>") > -1 && data != null && status == "ON_AIR" && data.IndexOf("webSocketUrl&quot;:&quot;ws") > -1) return 0;
		else if (res.IndexOf("<!doctype html>") > -1 && data != null && status == "ENDED" && data.IndexOf("webSocketUrl&quot;:&quot;ws") > -1) return 7;
		else if (util.getRegGroup(res, "(���G���ł����A�v���~�A������̕��͗D�悵�ē��ꂪ�ł��܂�)") != null ||
				util.getRegGroup(res, "(�������܁A�����̂��ߓ���ł��܂���)") != null) return 1;
		//		else if (util.getRegGroup(res, "<div id=\"comment_arealv\\d+\">[^<]+���̔ԑg��\\d+/\\d+/\\d+\\(.\\) \\d+:\\d+�ɏI���������܂����B<br>") != null) return 2;
		else if (res.IndexOf(" onclick=\"Nicolive.ProductSerial") > -1) return 8;
		//else if (res.IndexOf("�����̕����̓^�C���V�t�g�ɑΉ����Ă���܂���B") > -1 && 
		//         res.IndexOf("�ɏI���������܂���") > -1) return 2;
		//else if (util.getRegGroup(res, "(�R�~���j�e�B�t�H�����[����ԑg�ł��B<br>)") != null) return 4;
		else if (res.IndexOf("isFollowerOnly&quot;:true") > -1 && res.IndexOf("isFollowed&quot;:false") > -1) return 4;
		else if (data.IndexOf("webSocketUrl&quot;:&quot;ws") == -1 &&
				 status == "ENDED") return 2;

		else if (status == "ENDED" && res.IndexOf(" onclick=\"Nicolive.WatchingReservation") > -1) return 9;
		//else if (util.getRegGroup(res, "(�ɏI���������܂���)") != null) return 2;
		else if (status == "ENDED") return 2;
		else if (util.getRegGroup(res, "(<archive>1</archive>)") != null) return 3;
		else if (util.getRegGroup(res, "(�`�����l���������ԑg�ł��B<br>)") != null) return 4;
		else if (util.getRegGroup(res, "(<h3>�y���̂��ē��z</h3>)") != null) return 6;
		else if (util.getRegGroup(res, "(���̔ԑg�͕����҂ɂ��폜����܂����B<br />|�폜���ꂽ�\��������܂��B<br />)") != null) return 2;
		return 5;
		//}
		//return 5;
	}
	public static int getPageTypeRtmp(string res, ref bool isTimeshift, bool isSub)
	{
		//		var res = getPlayerStatusRes;
		if (res.IndexOf("status=\"ok\"") > -1 && res.IndexOf("<archive>0</archive>") > -1)
		{
			isTimeshift = false;
			return 0;
		}
		if (res.IndexOf("status=\"ok\"") > -1 && res.IndexOf("<archive>1</archive>") > -1)
		{
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

	public static DateTime getUnixToDatetime(long unix)
	{
		DateTime UNIX_EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
		return UNIX_EPOCH.AddSeconds(unix).ToLocalTime();
	}
	public static string getSecondsToKeikaJikan(double seconds)
	{
		var second = ((int)((seconds % 60) * 1)).ToString("00");
		var minute = ((int)((seconds % 3600 / 60))).ToString("00");
		var hour = ((int)((seconds / 3600) * 1));
		var _hour = (hour < 100) ? hour.ToString("00") : hour.ToString(); ;
		return _hour + "����" + minute + "��" + second + "�b";
	}
	public static void writeFile(string name, string str)
	{
		using (var f = new System.IO.FileStream(name, FileMode.Append))
		using (var w = new StreamWriter(f))
		{
			try
			{
				w.WriteLine(str);
			}
			catch (Exception e)
			{
				util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.TargetSite);
			}
		}
		//		w.Close();
		//		f.Close();
	}
	public static string getOkSJisOut(string s)
	{
		var a = System.Text.Encoding.GetEncoding("shift_jis");
		return a.GetString(a.GetBytes(s)).Replace("?", "_");
	}
	public static bool isLogFile = false;
	public static StreamWriter exceptionSw = null;
	public static List<string> debugWriteBuf = new List<string>();
	//public static Task debugWriteTask = null;
	public static void debugWriteLine(object str)
	{
		var dt = DateTime.Now.ToLongTimeString();
		//		System.Console.WriteLine(dt + " " + str);
		try
		{
#if DEBUG
			System.Diagnostics.Debug.WriteLine(str);
			//      		System.Diagnostics.util.debugWriteLine(
#else
				if (isLogFile) {
					System.Console.WriteLine(dt + " " + str);
					//debugWriteBuf.Add(dt + " " + str);
				}
#endif
		}
		catch (Exception e)
		{
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
	public static void showException(Exception eo, bool isMessageBox = true)
	{
		var frameCount = new System.Diagnostics.StackTrace().FrameCount;
#if DEBUG
		if (isMessageBox && isLogFile)
		{
			if (frameCount > 150)
			{
				System.Windows.Forms.MessageBox.Show("framecount stack", frameCount.ToString() + " " + namaichi.Program.arg + " " + DateTime.Now.ToString());
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
		if (exceptionSw != null)
		{
			exceptionSw.WriteLine(dt + " 0 message " + eo.Message + "\nsource " +
				eo.Source + "\nstacktrace " + eo.StackTrace +
				"\n targetsite " + eo.TargetSite + "\n\n");
			exceptionSw.Flush();
		}

		var _eo = eo.GetBaseException();
		util.debugWriteLine("eo " + _eo);
		if (_eo != null)
		{
			util.debugWriteLine("1 message " + _eo.Message + "\nsource " +
					_eo.Source + "\nstacktrace " + _eo.StackTrace +
					"\n targetsite " + _eo.TargetSite + "\n\n");
			if (exceptionSw != null)
			{
				exceptionSw.WriteLine(dt + " 1 message " + _eo.Message + "\nsource " +
					_eo.Source + "\nstacktrace " + _eo.StackTrace +
					"\n targetsite " + _eo.TargetSite + "\n\n");
				exceptionSw.Flush();
			}
		}

		_eo = eo.InnerException;
		util.debugWriteLine("eo " + _eo);
		if (_eo != null)
		{
			util.debugWriteLine("2 message " + _eo.Message + "\nsource " +
					_eo.Source + "\nstacktrace " + _eo.StackTrace +
					"\n targetsite " + _eo.TargetSite);
			if (exceptionSw != null)
			{
				exceptionSw.WriteLine(dt + " 2 message " + _eo.Message + "\nsource " +
					_eo.Source + "\nstacktrace " + _eo.StackTrace +
					"\n targetsite " + _eo.TargetSite);
				exceptionSw.Flush();
			}
		}


#if DEBUG
		if (isMessageBox && isLogFile)
			System.Windows.Forms.MessageBox.Show("error " + eo.Message, "error " + namaichi.Program.arg);
#else
			
#endif
	}
	public static void setLog(config config, string lv)
	{
		//test
		if (bool.Parse(config.get("IsLogFile")))
		{
			//var name = (args.Length == 0) ? "lv_" : util.getRegGroup(args[0], "(lv\\d+)");
			var name = (lv == null) ? "lv_" : lv;
			var logPath = util.getJarPath()[0] + "/" + name + ".txt";

			try
			{
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
			}
			catch (Exception e)
			{
				util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
			}
			util.isLogFile = true;
			exceptionSw = new StreamWriter(util.getJarPath()[0] + "/errorLog.txt", true);
		}
	}
	public static bool isOkDotNet()
	{
		var ver = Get45PlusFromRegistry();
		return ver >= 4.52;
	}
	public static double Get45PlusFromRegistry()
	{
		const string subkey = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\";

		using (RegistryKey ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(subkey))
		{
			if (ndpKey != null && ndpKey.GetValue("Release") != null)
			{
				return CheckFor45PlusVersion((int)ndpKey.GetValue("Release"));
				//			Console.WriteLine(".NET Framework Version: " + CheckFor45PlusVersion((int) ndpKey.GetValue("Release")));
			}
			else
			{
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
	public static string getMainSubStr(bool isSub, bool isKakko = false)
	{
		var ret = (isSub) ? "�T�u" : "���C��";
		if (isKakko) ret = "(" + ret + ")";
		return ret;
	}
	public static string getUserName(string userId, out bool isFollow, CookieContainer container)
	{
		isFollow = false;
		if (userId == "official" || userId == null || userId == "") return null;

		//http://ext.nicovideo.jp/thumb_user/10000
		//var url = "http://seiga.nicovideo.jp/api/user/info?id=" + userId;
		var url = "https://www.nicovideo.jp/user/" + userId;
		var res = util.getPageSource(url, container);

		if (res == null) return null;
		var name = util.getRegGroup(res, "<meta property=\"og:title\" content=\"(.+?)\">");
		if (name == null)
			name = util.getRegGroup(res, "<meta property=\"og:title\" content=\"(.+?)����̃��[�U�[�y�[�W\">");
		if (name == null) return null;
		if (name.EndsWith(" - niconico(�j�R�j�R)"))
			name = name.Replace(" - niconico(�j�R�j�R)", "");
		//watching nowatching class
		if (res.IndexOf("class=\"watching\"") > -1) isFollow = true;
		return name;
	}
	public static string getCommunityName(string communityNum, out bool isFollow, CookieContainer cc)
	{
		isFollow = false;
		if (communityNum == null || communityNum == "" || communityNum == "official") return null;

		var isChannel = communityNum.IndexOf("ch") > -1;
		var url = (isChannel) ?
			("https://ch.nicovideo.jp/" + communityNum) :
			("https://com.nicovideo.jp/community/" + communityNum);

		//			var wc = new WebHeaderCollection();
		var res = util.getPageSource(url, cc);
		//util.debugWriteLine(cc.GetCookieHeader(new Uri(url)));

		if (res == null)
		{
			url = (isChannel) ?
				("https://ch.nicovideo.jp/" + communityNum) :
				("https://com.nicovideo.jp/motion/" + communityNum);
			res = util.getPageSource(url, cc);
			if (res == null) return null;
			isFollow = res.IndexOf("<h2 class=\"pageHeader_title\">�R�~���j�e�B�Ƀt�H���[���N�G�X�g�𑗂�</h2>") == -1 &&
					util.getRegGroup(res, "<p class=\"error_description\">[\\s\\S]*?(�R�~���j�e�B�t�H�����[)�ł͂���܂���B") == null &&
					res.IndexOf("<h2 class=\"pageHeader_title\">�R�~���j�e�B���t�H���[����</h2>") == -1;
		}
		else
		{
			isFollow = (isChannel) ?
				(res.IndexOf("class=\"bookmark following btn_follow\"") > -1) :
				(res.IndexOf("followButton follow\">�t�H���[") == -1);
		}
		if (res == null) return null;
		var title = (isChannel) ?
			//			util.getRegGroup(res, "<meta property=\"og\\:title\" content=\"(.+?) - �j�R�j�R�`�����l��") :
			util.getRegGroup(res, "<meta property=\"og:site_name\" content=\"(.+?)\"") :
			util.getRegGroup(res, "<meta property=\"og\\:title\" content=\"(.+?)-�j�R�j�R�~���j�e�B\"");
		if (title == null) title = util.getRegGroup(res, "<meta property=\"og:title\" content=\"(.+?)����̃R�~���j�e�B-�j�R�j�R�~���j�e�B\">");

		//not login
		if (title == null)
		{
			url = "https://ext.nicovideo.jp/thumb_" + ((isChannel) ? "channel" : "community") + "/" + communityNum;
			res = getPageSource(url, cc, null, false, 3);
			title = getRegGroup(res, "<p class=\"chcm_tit\">(.+?)</p>");
		}
		else
		{
			title = WebUtility.HtmlDecode(title);
		}
		if (title == null) isFollow = false;

		return title;
	}
	public static void openUrlBrowser(string url, config config)
	{
		try
		{
			if (config.get("IsdefaultBrowserPath") == "true")
				Process.Start(url);
			else
			{
				var path = config.get("browserPath");
				if (path == null || path == "")
					Process.Start(url);
				else Process.Start(path, url);
			}
		}
		catch (Exception e)
		{
			util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
		}
	}
	public static void setClipBordText(string s)
	{
		try
		{
			Clipboard.SetText(s);
		}
		catch (Exception e)
		{
			util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
		}
	}
	public static string getArrStr(byte[] arr)
	{
		var s = "";
		foreach (var b in arr) s += ", " + b;
		return s;
	}
	public static void appliProcess(string appliPath, string url)
	{
		if (appliPath == null || appliPath == "") return;

		try
		{
			Process.Start(appliPath, url);
		}
		catch (Exception e)
		{
			util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
		}
	}
	public static void playSound(config cfg, AlartInfo ai)
	{
		try
		{
			string path = null;
			float volume = 0;
			var basePath = util.getJarPath()[0];
			if (ai != null && ai.isSoundId)
			{
				try
				{
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
				}
				catch (Exception e)
				{
					util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				}
			}
			if (ai != null && path == null)
			{
				if (ai.soundType == 1)
				{
					path = cfg.get("soundPathA");
					volume = float.Parse(cfg.get("soundAVolume")) / 100;
				}
				else if (ai.soundType == 2)
				{
					path = cfg.get("soundPathB");
					volume = float.Parse(cfg.get("soundBVolume")) / 100;
				}
				else if (ai.soundType == 3)
				{
					path = cfg.get("soundPathC");
					volume = float.Parse(cfg.get("soundCVolume")) / 100;
				}
			}
			if (path == null || !File.Exists(path))
			{
				path = util.getJarPath()[0] + "/Sound/se_soc01.wav";
				volume = float.Parse(cfg.get("soundVolume")) / 100;
			}

			playSoundCore(volume, path);

			/*
			var path = (bool.Parse(cfg.get("IsSoundDefault"))) ? 
				(util.getJarPath()[0] + "/Sound/se_soc02.wav") : cfg.get("soundPath");
			util.debugWriteLine(path);
			var m = new System.Media.SoundPlayer(path);
			
			m.PlaySync();
			*/
		}
		catch (Exception e)
		{
			util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
		}
	}
	public static void playSoundCore(float volume, string path)
	{
		try
		{
			util.debugWriteLine("sound path " + path);

			var reader = new NAudio.Wave.AudioFileReader(path);
			var waveOut = new NAudio.Wave.WaveOut(NAudio.Wave.WaveCallbackInfo.FunctionCallback());
			waveOut.Init(reader);
			if (volume < 0) volume = (float)0;
			if (volume > 1) volume = (float)1;
			waveOut.Volume = volume;
			util.debugWriteLine("volume " + waveOut.Volume);
			waveOut.Play();
			while (waveOut.PlaybackState == NAudio.Wave.PlaybackState.Playing) Thread.Sleep(100);
			//waveOut.Dispose();
			reader.Close();
		}
		catch (Exception e)
		{
			util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
		}
	}
	public static bool sendMail(string from, string to,
			string subject, string body, string _smtp,
			string _port, string user, string pass,
			bool isSsl, out string msg)
	{
		msg = null;
		if (from == "" || to == "" || _smtp == "" ||
				_port == "" || user == "" || pass == "")
			return false;
		int port;
		if (!int.TryParse(_port, out port)) port = 587;
		try
		{
			using (var m = new MailMessage(from, to, subject, body))
			using (var smtp = new SmtpClient(_smtp, port))
			{
				smtp.Credentials = new NetworkCredential(user, pass);
				smtp.EnableSsl = isSsl;
				//ServicePointManager.SecurityProtocol = SecurityProtocolType.
				smtp.Send(m);
			}
			return true;
		}
		catch (Exception e)
		{
			util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			msg = e.Message;
			return false;
		}
	}
	[DllImport("user32.dll")]
	public static extern int MessageBox(IntPtr hwnd, String text, String caption, uint type);
	public static int showModelessMessageBox(string text, string caption, MainForm form, int style = 0)
	{
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
	public static string removeTag(string s)
	{
		if (s == null)
		{
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
	public enum NotifyFlags
	{
		NIF_MESSAGE = 0x01, NIF_ICON = 0x02, NIF_TIP = 0x04, NIF_INFO = 0x10, NIF_STATE = 0x08,
		NIF_GUID = 0x20, NIF_SHOWTIP = 0x80, NIF_REALTIME = 0x40,
	}
	[DllImport("shell32.dll")]
	public static extern System.Int32 Shell_NotifyIcon(NotifyCommand cmd, ref NOTIFYICONDATA data);

	public enum NotifyCommand { NIM_ADD = 0x0, NIM_DELETE = 0x2, NIM_MODIFY = 0x1, NIM_SETVERSION = 0x4 }

	[StructLayout(LayoutKind.Sequential)]
	public struct NOTIFYICONDATA
	{
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
			IntPtr icon, IntPtr hwnd)
	{
		NOTIFYICONDATA data = new NOTIFYICONDATA();
		var NIIF_USER = 4;
		var NIIF_NONE = 0;
		var NIIF_LARGE_ICON = 0x20;
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
	public static bool addNotifyIcon(IntPtr hwnd)
	{
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
	public static bool deleteNotifyIcon(IntPtr hwnd)
	{
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
	public static string getUnicodeToUtf8(string s)
	{
		var b = Encoding.Unicode.GetBytes(s);
		var utB = Encoding.Convert(Encoding.Unicode, Encoding.UTF8, b);
		return Encoding.UTF8.GetString(utB);
	}
	public static void dllCheck(namaichi.MainForm form)
	{
		var path = getJarPath()[0];
		var dlls = new string[]{"websocket4net.dll", "NAudio.dll",
				"Interop.IWshRuntimeLibrary.dll", "SnkLib.App.CookieGetter.Forms.dll",
				"SnkLib.App.CookieGetter.dll", "SuperSocket.ClientEngine.dll",
				"Microsoft.Web.XmlTransform.dll", "Newtonsoft.Json.dll",
				"System.Data.SQLite.dll", "x64/SQLite.Interop.dll",
				"x86/SQLite.Interop.dll", "x86/SnkLib.App.CookieGetter.x86Proxy.exe",
				//"Google.Protobuf.dll", "Google.Protobuf.dll",
				"protobuf-net.dll",
				"BouncyCastle.Crypto.dll"};
		var isOk = new string[dlls.Length];
		var msg = "";
		foreach (var n in dlls)
		{
			if (!File.Exists(path + "/" + n))
				msg += (msg == "" ? "" : ",") + n;
		}
		if (msg == "") return;
		form.formAction(() => System.Windows.Forms.MessageBox.Show(path + "����" + msg + "��������܂���ł���"));
	}
}
