/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2019/08/10
 * Time: 22:25
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

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
			
			
			
			try {
				var json = JToken.FromObject(form.notAlartListDataSource).ToString(Formatting.None);
				var f = path + "notAlartList.ini_";
				using (var sw = new StreamWriter(f, false, Encoding.UTF8)) {
					sw.Write(json);
					//sw.Close();
				}
				File.Copy(f, f.Substring(0, f.Length - 1), true);
				File.Delete(f);
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
			util.saveBackupList(path, "notAlartList");
		}
		public void load(MainForm form)
		{
			ReadNamarokuList(form, form.notAlartListDataSource, util.getJarPath()[0] + "\\notAlartList.ini");
			
		}
		public void ReadNamarokuList(MainForm form, SortableBindingList<info.HistoryInfo> alartListDataSource, string fileName)
		{
			string data;
			
			try {
				if (!File.Exists(fileName)) return;
				using (var sr = new StreamReader(fileName, Encoding.UTF8)) {
					data = sr.ReadToEnd().Replace("\r", "");
					//sr.Close();
				}
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
				return;
			}
			
			try {
				var listData = JsonConvert.DeserializeObject<SortableBindingList<info.HistoryInfo>>(data);
				var max = int.Parse(form.config.get("maxNotAlartDisplay"));
				foreach (var hi in listData)
					form.addNotAlartList(hi, max);
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
				form.showMessageBox("アラート履歴リストを読み込めませんでした", "");
				return;
			}
			
		}
	}
}
