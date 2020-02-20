﻿/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2018/09/29
 * Time: 20:08
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace namaichi.utility
{
	/// <summary>
	/// Description of ArgReader.
	/// </summary>
	public class ArgReader
	{
		private string[] args;
		private MainForm form;
		public bool isConcatMode = false;
		public string allPathStr;
		public Dictionary<string, string> argConfig = new Dictionary<string, string>();
		public string lvid = null;
		public config.config config;
		//public TimeShiftConfig tsConfig;
		public bool isPlayMode = false;

		public ArgReader(string[] args, config.config config, MainForm form)
		{
			this.args = args;
			this.config = config;
			this.form = form;
		}
		public void read()
		{
			if (isAllPath())
			{
				isConcatMode = true;
				return;
			}
			setArgConfig();
			util.debugWriteLine("args " + string.Join(" ", args));
			foreach (var a in argConfig) util.debugWriteLine(a.Key + " " + a.Value);
			isPlayMode = Array.IndexOf(args, "-play") > -1;
		}
		private bool isAllPath()
		{
			var isAllPath = true;
			foreach (var a in args)
			{
				try
				{
					if (!File.Exists(a) && !Directory.Exists(a)) isAllPath = false;
				}
				catch (Exception e)
				{
					isAllPath = false;
					util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				}
			}
			if (isAllPath) allPathStr = string.Join("|", args);
			return isAllPath;
		}
		private void setArgConfig()
		{
			var lowKeys = new List<string>(config.defaultConfig.Keys.Select((x) => x.ToLower()));
			var values = config.defaultConfig.Values.ToList<string>();
			var keys = config.defaultConfig.Keys.ToList();
			lowKeys.AddRange(new string[] { "ts-start", "ts-end", "ts-list", "ts-list-update", "ts-list-command", "ts-list-open", "ts-list-m3u8", "ts-vpos-starttime" });
			foreach (var a in args)
			{
				if (a.StartsWith("-"))
				{
					var name = util.getRegGroup(a, "-(.*)=");
					var val = util.getRegGroup(a, "=(.*)");
					if (name == null) continue;

					string setVal = null;
					string setName = null;
					if (!isValidConf(name, val, lowKeys, values, out setVal, out setName, keys)) continue;
					//argConfig.Add(setName, setVal);
					argConfig[setName] = setVal;
				}
				else
				{
					if (lvid == null) lvid = util.getRegGroup(a, "(lv\\d+(,\\d+)*)");
				}
			}
		}
		private bool isValidConf(string name, string val, List<string> lowKeys, List<string> defValues, out string setVal, out string setName, List<string> keys)
		{
			setVal = null;
			setName = null;
			for (var i = 0; i < lowKeys.Count; i++)
			{
				if (name.ToLower() != lowKeys[i]) continue;
				if (i < defValues.Count && (defValues[i] == "true" || defValues[i] == "false"))
				{
					if (val.ToLower() == "true" || val.ToLower() == "false")
					{
						setVal = val.ToLower();
						setName = keys[i];
						return true;
					}
					else
					{
						form.addLogText(name + "の値が設定できませんでした(true or false) " + val, false);
						return false; ;
					}
				}
				if (lowKeys[i] == "browsernum")
				{
					if (val == "1" || val == "2")
					{
						setVal = val;
						setName = keys[i];
						return true;
					}
					else
					{
						form.addLogText(name + "の値が設定できませんでした(1 or 2) " + val, false);
						return false;
					}
				}
				if (lowKeys[i] == "qualityrank")
				{
					if (isValidQualityRank(val))
					{
						setVal = val;
						setName = keys[i];
						return true;
					}
					else
					{
						form.addLogText(name + "の値が設定できませんでした(例 「0,1,2,5,4,3」) " + val, false);
						return false;
					}
				}
				if (lowKeys[i] == "segmentsavetype")
				{
					if (val == "0" || val == "1")
					{
						setVal = val;
						setName = keys[i];
						return true;
					}
					else
					{
						form.addLogText(name + "の値が設定できませんでした(0 or 1) " + val, false);
						return false;
					}
				}
				if (lowKeys[i] == "m3u8updateseconds")
				{
					double _s = 0;
					if (double.TryParse(val, out _s) && _s > 0)
					{
						setVal = val;
						setName = keys[i];
						return true;
					}
					else
					{
						form.addLogText(name + "の値が設定できませんでした(0以上の数値) " + val, false);
						return false;
					}
				}
				if (lowKeys[i] == "subfoldernametype")
				{
					int _s = 0;
					if (int.TryParse(val, out _s) && _s >= 1 && _s <= 8)
					{
						setVal = val;
						setName = keys[i];
						return true;
					}
					else
					{
						form.addLogText(name + "の値が設定できませんでした(1から8の整数) " + val, false);
						return false;
					}
				}
				if (lowKeys[i] == "filenametype")
				{
					int _s = 0;
					if (int.TryParse(val, out _s) && _s >= 1 && _s <= 10)
					{
						setVal = val;
						setName = keys[i];
						return true;
					}
					else
					{
						form.addLogText(name + "の値が設定できませんでした(1から10の整数) " + val, false);
						return false;
					}
				}
				if (lowKeys[i] == "filenameformat")
				{
					if (val.IndexOf("{0}") > -1)
					{
						setVal = val;
						setName = keys[i];
						return true;
					}
					else
					{
						form.addLogText(name + "の値が設定できませんでした({0}を含む文字列) " + val, false);
						return false;
					}
				}
				if (lowKeys[i] == "volume")
				{
					int _s = 0;
					if (int.TryParse(val, out _s) && _s >= 0 && _s <= 100)
					{
						setVal = val;
						setName = keys[i];
						return true;
					}
					else
					{
						form.addLogText(name + "の値が設定できませんでした(0から100の整数) " + val, false);
						return false;
					}
				}
				if (lowKeys[i] == "afterConvertMode")
				{
					int _s = 0;
					if (int.TryParse(val, out _s) && _s >= 0 && _s <= 14)
					{
						setVal = val;
						setName = keys[i];
						return true;
					}
					else
					{
						form.addLogText(name + "の値が設定できませんでした(0から14の整数) " + val, false);
						return false;
					}
				}
				if (lowKeys[i] == "EngineMode")
				{
					int _s = 0;
					if (int.TryParse(val, out _s) && _s >= 0 && _s <= 3)
					{
						setVal = val;
						setName = keys[i];
						return true;
					}
					else
					{
						form.addLogText(name + "の値が設定できませんでした(0から3の整数) " + val, false);
						return false;
					}
				}

				setName = keys[i];
				setVal = val;
				return true;
			}
			return false;

		}
		private bool isValidQualityRank(string val)
		{
			try
			{
				var l = val.Split(',').Select((x) => int.Parse(x));
				if (l.Count() != 6) return false;
				var a = new List<int> { 0, 1, 2, 3, 4, 5 };
				foreach (var _l in l) a.Remove(_l);
				return a.Count == 0;
			}
			catch (Exception e)
			{
				return false;
			}
		}

	}
}
