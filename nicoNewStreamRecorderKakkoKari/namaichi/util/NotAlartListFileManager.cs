﻿/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2019/08/10
 * Time: 22:25
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Text;

namespace namaichi.utility
{
	/// <summary>
	/// Description of NotAlartListFileManager.
	/// </summary>
	public class NotAlartListFileManager
	{
		public NotAlartListFileManager()
		{
		}
		public void save(MainForm form)
		{
			var path = util.getJarPath()[0] + "\\";

			if (File.Exists(path + "notAlartList.ini"))
			{
				try
				{
					var _len = File.ReadAllText(path + "notAlartlist.ini").Length;
					if (_len > 5)
						File.Copy(path + "notAlartList.ini", path + "notAlartList_backup.ini", true);
				}
				catch (Exception e)
				{
					util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				}
			}

			try
			{
				var json = JToken.FromObject(form.notAlartListDataSource).ToString(Formatting.None);
				using (var sw = new StreamWriter(path + "notAlartList.ini", false, Encoding.UTF8))
				{
					sw.Write(json);
					//sw.Close();
				}
			}
			catch (Exception e)
			{
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}

		}
		public void load(MainForm form)
		{
			ReadNamarokuList(form, form.notAlartListDataSource, util.getJarPath()[0] + "\\notAlartList.ini");

		}
		public void ReadNamarokuList(MainForm form, SortableBindingList<info.HistoryInfo> alartListDataSource, string fileName)
		{
			string data;

			try
			{
				if (!File.Exists(fileName)) return;
				using (var sr = new StreamReader(fileName, Encoding.UTF8))
				{
					data = sr.ReadToEnd().Replace("\r", "");
					//sr.Close();
				}
			}
			catch (Exception ee)
			{
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
				return;
			}

			try
			{
				var listData = JsonConvert.DeserializeObject<SortableBindingList<info.HistoryInfo>>(data);
				foreach (var hi in listData)
					form.addNotAlartList(hi);
			}
			catch (Exception ee)
			{
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
				form.showMessageBox("アラート履歴リストを読み込めませんでした", "");
				return;
			}

		}
	}
}
