/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2019/08/10
 * Time: 22:24
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
	/// Description of ReserveHistoryListFileManager.
	/// </summary>
	public class ReserveHistoryListFileManager
	{
		public ReserveHistoryListFileManager()
		{
		}
		public void save(MainForm form)
		{
			var path = util.getJarPath()[0] + "\\";
			try {
				var json = JToken.FromObject(form.reserveHistoryListDataSource).ToString(Formatting.None);
				var f = path + "reservehistorylist.ini_";
				using (var sw = new StreamWriter(f, false, Encoding.UTF8)) {
					sw.Write(json);
				}
				File.Copy(f, f.Substring(0, f.Length - 1), true);
				File.Delete(f);
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
			util.saveBackupList(path, "reservehistorylist");
		}
		public void load(MainForm form)
		{
			ReadNamarokuList(form, util.getJarPath()[0] + "\\reservehistorylist.ini");
			
		}
		public void ReadNamarokuList(MainForm form, string fileName)
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
				foreach (var hi in listData)
					form.addReserveHistoryList(hi);
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
				form.showMessageBox("予約履歴リストを読み込めませんでした", "");
				return;
			}
			
		}
	}
}
