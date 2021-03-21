/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2021/03/03
 * Time: 20:14
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
	/// Description of TwitterListFileManager
	/// </summary>
	public class TwitterListFileManager
	{
		public TwitterListFileManager()
		{
		}
		public void save(MainForm form)
		{
			var path = util.getJarPath()[0] + "\\";
			
			
			
			try {
				var json = JToken.FromObject(form.twitterListDataSource).ToString(Formatting.None);
				var f = path + "twlist.ini_";
				using (var sw = new StreamWriter(f, false, Encoding.UTF8)) {
					sw.Write(json);
					//sw.Close();
				}
				File.Copy(f, f.Substring(0, f.Length - 1), true);
				File.Delete(f);
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
			util.saveBackupList(path, "twlist");
		}
		public void load(MainForm form)
		{
			var fileName = util.getJarPath()[0] + "\\twlist.ini";
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
				var listData = JsonConvert.DeserializeObject<SortableBindingList<info.TwitterInfo>>(data);
				foreach (var ti in listData)
					form.addTwitterList(ti);
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
				form.showMessageBox("Twitterリストを読み込めませんでした", "");
				return;
			}
			
		}
	}
}
