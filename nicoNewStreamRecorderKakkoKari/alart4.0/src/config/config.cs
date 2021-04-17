/*
 * Created by SharpDevelop.
 * User: pc
 * Date: 2018/04/29
 * Time: 20:51
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Threading;
using System.IO;

namespace namaichi.config {
/// <summary>
/// Description of config.
/// </summary>
public class config
{
	private Configuration cfg;
	public Dictionary<string, string> defaultConfig;
	public Dictionary<string, string> argConfig = new Dictionary<string, string>();
	public string brokenCopyFile = null;
	
	public config()
	{
		cfg = getConfig();
		defaultMergeFile();
        
 	}
	public Configuration getConfig() {
		for (var i = 0; i < 5; i++) {
			try {
				var jarPath = util.getJarPath();
				var configFile = jarPath[0] + "\\" + jarPath[1] + ".config";
				if (i > 3) System.IO.File.Delete(configFile);
				//util.debugWriteLine(configFile);
		        var exeFileMap = new System.Configuration. ExeConfigurationFileMap { ExeConfigFilename = configFile };
		        var cfg     = ConfigurationManager.OpenMappedExeConfiguration(exeFileMap, ConfigurationUserLevel.None);
		        return cfg;
			} catch (Exception e) {
				util.debugWriteLine("getconfig " + e.Message + " " + e.StackTrace + " " + e.TargetSite);
				if (e.Message.IndexOf("レベルのデータ") > -1) break;
				Thread.Sleep(3000);
				continue;
				//ルート レベルのデータが無効です。
			}
		}
		resetConfig();
		return this.cfg;
	}
	public void set(string key, string value) {
		if (key.IndexOf("user_session") == -1 && 
				key.IndexOf("account") == -1)
			util.debugWriteLine("config set " + key + " " + value);
		else util.debugWriteLine("config set " + key);
		for (var i = 0; i < 100; i++) {
			cfg = getConfig();
			
			
			var keys = cfg.AppSettings.Settings.AllKeys;
			if (System.Array.IndexOf(keys, key) < 0)
				cfg.AppSettings.Settings.Add(key, value);
			else cfg.AppSettings.Settings[key].Value = value;
			try {
				cfg.Save();
				return;
			} catch (Exception e) {
				util.debugWriteLine(e.Message + " " + e.StackTrace);
				System.Threading.Thread.Sleep(500);
				continue;
			}
		}
	}
	public void set(List<KeyValuePair<string, string>> l) {
		foreach (var _l in l) {
			if (_l.Key.IndexOf("user_session") == -1 && 
					_l.Key.IndexOf("account") == -1)
				util.debugWriteLine("config set " + _l.Key + " " + _l.Value);
			else util.debugWriteLine("config set " + _l.Key);
		}
		for (var i = 0; i < 100; i++) {
			cfg = getConfig();
			
			var keys = cfg.AppSettings.Settings.AllKeys;
			
			foreach (var _l in l) {
				if (System.Array.IndexOf(keys, _l.Key) < 0)
					cfg.AppSettings.Settings.Add(_l.Key, _l.Value);
				else cfg.AppSettings.Settings[_l.Key].Value = _l.Value;
			}
			try {
				cfg.Save();
				return;
			} catch (Exception e) {
				util.debugWriteLine(e.Message + " " + e.StackTrace);
				System.Threading.Thread.Sleep(500);
				continue;
			}
		}
	}
	public string get(string key) {
		util.debugWriteLine("config get " + key);
		try {
			if (key.IndexOf("user_session") == -1 && 
			    	key.IndexOf("account") == -1) {
				util.debugWriteLine(key + " " + cfg.AppSettings.Settings[key].Value);
			} else util.debugWriteLine(key);
		} catch (Exception e) {
			util.debugWriteLine("config get exception " + key + " " + e.Message + e.Source + e.StackTrace + e.TargetSite);
			return null;
		}
		try {
			if (argConfig.ContainsKey(key)) 
				return argConfig[key];
			return cfg.AppSettings.Settings[key].Value;
		} catch (Exception) {
			return null;
		}
	}
	private void defaultMergeFile() {
		defaultConfig = new Dictionary<string, string>(){
			{"accountId",""},
			{"accountPass",""},
			{"user_session",""},
			{"user_session_secure",""},
			{"browserNum","1"},
			{"browserPath",""},
			{"issecondlogin","false"},
			{"age_auth","0"},
			
			{"IsdefaultBrowserPath","true"},
			{"appliAPath",""},
			{"appliBPath",""},
			{"appliCPath",""},
			{"appliDPath",""},
			{"appliEPath",""},
			{"appliFPath",""},
			{"appliGPath",""},
			{"appliHPath",""},
			{"appliIPath",""},
			{"appliJPath",""},
			
			{"appliAArgs",""},
			{"appliBArgs",""},
			{"appliCArgs",""},
			{"appliDArgs",""},
			{"appliEArgs",""},
			{"appliFArgs",""},
			{"appliGArgs",""},
			{"appliHArgs",""},
			{"appliIArgs",""},
			{"appliJArgs",""},
			
			{"appliAName",""},
			{"appliBName",""},
			{"appliCName",""},
			{"appliDName",""},
			{"appliEName",""},
			{"appliFName",""},
			{"appliGName",""},
			{"appliHName",""},
			{"appliIName",""},
			{"appliJName",""},
			{"log","false"},
			{"IsStartTimeAllCheck","false"},
			//{"Ischeck30min","true"},
			{"IscheckRecent","true"},
			{"Ischeck30min","false"},
			{"IscheckOnAir","true"},
			{"IschangeIcon","false"},
			{"IsAlartListRecentColor","false"},
			{"recentColor","#FFE0FF"},
			{"IsFollowerOnlyOtherColor","false"},
			{"followerOnlyColor","#FFE0FF"},
			
			{"IstasktrayStart","false"},
			{"IsdragCom","false"},
			{"doublecmode","なにもしない"},
			{"IsNotAllMatchNotifyNoRecent","false"},
			{"delThumb","false"},
			{"IsConfirmFollow","false"},
			{"alartCacheIcon","true"},
			{"IsAddAlartedComUser","false"},
			{"IsAddAlartedUserToUserList","false"},
			
			{"IsbroadLog","false"},
			{"IsLogFile","false"},
			{"maxHistoryDisplay","100"},
			{"maxNotAlartDisplay","100"},
			{"maxLogDisplay","100"},
			{"IsStartUp","false"},
			{"IsAllowMultiProcess","false"},
			
			{"poploc","右下"},
			{"poptime","10"},
			{"Isclosepopup","true"},
			{"Isfixpopup","false"},
			{"Issmallpopup","false"},
			{"IsTopMostPopup","true"},
			{"IsColorPopup","true"},
			{"popupOpacity","90"},
			{"rssUpdateInterval","15"},
			{"userNameUpdateInterval","15"},
			
			
			{"mailFrom",""},
			{"mailTo",""},
			{"mailSmtp",""},
			{"mailPort",""},
			{"mailUser",""},
			{"mailPass",""},
			{"IsmailSsl","false"},
			//{"IsSoundDefault","true"},
			{"soundPathA",""},
			{"soundPathB",""},
			{"soundPathC",""},
			{"soundVolume","50"},
			{"soundAVolume","50"},
			{"soundBVolume","50"},
			{"soundCVolume","50"},
			{"onlyIconColor","#000000"},
			
			{"defaultBehavior","0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0"},
			{"defaultTextColor","#000000"},
			{"defaultBackColor","#ffe0ff"},
			{"defaultSound","0"},
			{"IsDefaultSoundId","true"},
			{"IsDefaultAutoReserve","false"},
			
			{"cookieFile",""},
			{"iscookie","false"},
			{"IsBrowserShowAll","false"},
			
			{"ShowComId","true"},
			{"ShowUserId","true"},
			{"ShowComName","true"},
			{"ShowUserName","true"},
			{"ShowKeyword","true"},
			{"ShowIsAnd","true"},
			{"ShowComFollow","true"},
			{"ShowUserFollow","true"},
			{"ShowLatestTime","true"},
			{"ShowRegistTime","true"},
			{"ShowPop","true"},
			{"ShowBalloon","true"},
			{"ShowWeb","true"},
			{"ShowMail","true"},
			{"ShowSound","true"},
			{"ShowAppA","true"},
			{"ShowAppB","true"},
			{"ShowAppC","true"},
			{"ShowAppD","false"},
			{"ShowAppE","false"},
			{"ShowAppF","false"},
			{"ShowAppG","false"},
			{"ShowAppH","false"},
			{"ShowAppI","false"},
			{"ShowAppJ","false"},
			{"ShowSoundType","false"},
			{"ShowMemo","true"},
				
			{"ShowTaskStartDt","true"},
			{"ShowTaskLvid","true"},
			{"ShowTaskArgs","true"},
			{"ShowTaskAddDt","true"},
			{"ShowTaskStatus","true"},
			{"ShowTaskPopup","true"},
			{"ShowTaskBalloon","true"},
			{"ShowTaskWeb","true"},
			{"ShowTaskMail","true"},
			{"ShowTaskSound","true"},
			{"ShowTaskAppliA","true"},
			{"ShowTaskAppliB","true"},
			{"ShowTaskAppliC","true"},
			{"ShowTaskAppliD","false"},
			{"ShowTaskAppliE","false"},
			{"ShowTaskAppliF","false"},
			{"ShowTaskAppliG","false"},
			{"ShowTaskAppliH","false"},
			{"ShowTaskAppliI","false"},
			{"ShowTaskAppliJ","false"},
			{"ShowTaskDelete","true"},
			{"ShowTaskMemo","true"},
			
			{"ShowLiveColumns","11111111111111111"},
			{"ShowHistoryColumns","1111111011"},
			{"ShowNotAlartColumns","1111111111"},
			{"ColorAlartListColumns","000000000100000000000000000"},
			{"ColorHistoryListRecentColumns","1000000000"},
			{"ColorLiveListColumns","001000000000000000"},
			
			{"disableFollowColumns", "false"},
			
			{"OffPop", "false"},
	        {"OffBalloon", "false"},
	        {"OffWeb", "false"},
	        {"OffMail", "false"},
	        {"OffSound", "false"},
	        {"OffAppA", "false"},
	        {"OffAppB", "false"},
	        {"OffAppC", "false"},
	        {"OffAppD", "false"},
	        {"OffAppE", "false"},
	        {"OffAppF", "false"},
	        {"OffAppG", "false"},
	        {"OffAppH", "false"},
	        {"OffAppI", "false"},
	        {"OffAppJ", "false"},
	        
	        {"IsRss", "true"},
	        {"IsPush", "true"},
	        {"IsAppPush", "true"},
	        {"IsTimeTable", "true"},
	        {"IsAutoReserve", "false"},
	        {"pushPri", ""},
	        {"pushPub", ""},
	        {"pushAuth", ""},
	        {"pushUa", ""},
	        {"pushChId", ""},
	        {"appPushId", ""},
	        {"appPushToken", ""},
	        {"nicoCasAppVer", "3.9.0"},
	        {"niconicoAppVer", "6.14.1"},
			
	        {"thresholdpage", "10"},
	        {"brodouble", "0"},
	        {"alartAddLive", "1"},
	        {"cateCategoryType", "0"},
	        {"liveListDelMinutes", "30"},
	        {"FavoriteUp", "true"},
	        {"FavoriteOnly", "false"},
	        {"AutoSort", "true"},
	        {"BlindOnlyA", "false"},
	        {"BlindOnlyB", "false"},
	        {"BlindQuestion", "false"},
	        {"AutoStart", "false"},
	        {"liveListUpdateMinutes", "1"},
	        {"liveListCacheIcon", "true"},
	        {"liveListGetIcon", "true"},
	        
	        
			{"Height","480"},
			{"Width","830"},
			{"X",""},
			{"Y",""},
			{"fontSize","9"},
			{"evenRowsColor","#f5f5f5"},
			{"LiveListColumnWidth",""},
			{"AlartListColumnWidth",""},
			{"UserAlartListColumnWidth",""},
			{"TaskListColumnWidth",""},
			{"LogListColumnWidth",""},
			{"HistoryListColumnWidth",""},
			{"NotAlartListColumnWidth",""},
			{"HistoryPanelDistance",""},
			{"activeTab","0"},
			{"favoriteActiveTab","0"},
			
			{"alartBackColor","-986896"},
			{"alartForeColor","-16777216"},
			
			{"liveListSortOrder","no"},
			{"liveListSortColumn","1"},
			{"alartListSortOrder","no"},
			{"alartListSortColumn","0"},
			{"userAlartListSortOrder","no"},
			{"userAlartListSortColumn","0"},
			{"taskListSortOrder","no"},
			{"taskListSortColumn","0"},
			{"historyListSortOrder","no"},
			{"historyListSortColumn","0"},
			{"notAlartListSortOrder","no"},
			{"notAlartListSortColumn","0"},
		};

		try {
			var buf = new Dictionary<string,string>();
			foreach (var k in cfg.AppSettings.Settings.AllKeys) {
				buf.Add(k, cfg.AppSettings.Settings[k].Value);
			}
			
			cfg.AppSettings.Settings.Clear();
			foreach (var k in defaultConfig.Keys) {
				var v = (buf.ContainsKey(k)) ? buf[k] : defaultConfig[k];
				cfg.AppSettings.Settings.Add(k, v);
			}
			try {
				cfg.Save();
			} catch (Exception e) {
				util.debugWriteLine(e.Message + " " + e.StackTrace);
			}
		} catch (Exception e) {
			util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			resetConfig();
		}
		
		// Dictionary<string, string>
	}
	public void saveFromForm(Dictionary<string, string> formData) {
		cfg = getConfig();
		
		foreach (var k in formData.Keys) {
			cfg.AppSettings.Settings[k].Value = formData[k];
			//util.debugWriteLine(k + formData[k]);
		}		
		try {
			cfg.Save();
		} catch (Exception e) {
			util.debugWriteLine(e.Message + " " + e.StackTrace);
		}
	}
//	private string[] defaultConfig = {};
	private bool resetConfig() {
		var jarPath = util.getJarPath();
		var configFile = jarPath[0] + "\\" + jarPath[1] + ".config";
		if (File.Exists(configFile)) {
			var n = DateTime.Now;
			var fn = jarPath[0] + "\\" + n.ToString("yyyyMMddhhmmss") + "ニコ生放送チェックツール（仮.config";
			try {
				File.Copy(configFile, fn);
				File.Delete(configFile);
				brokenCopyFile = fn;
				cfg = getConfig();
				defaultMergeFile();
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				try {
					for (var i = 0; i < 10000; i++) {
						fn = configFile + i.ToString();
						if (File.Exists(fn)) continue;
						File.Copy(configFile, fn);
						File.Delete(configFile);
						brokenCopyFile = fn;
						cfg = getConfig();
						defaultMergeFile();
						break;
					}
				} catch (Exception ee) {
					util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
					return false;
				}
			}
		}
		return true;
	}
}

}