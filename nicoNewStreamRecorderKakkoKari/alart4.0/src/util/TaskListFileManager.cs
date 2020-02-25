/*
 * Created by SharpDevelop.
 * User: user
 * Date: 2019/01/18
 * Time: 23:59
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using namaichi.info;

namespace namaichi.utility
{
	/// <summary>
	/// Description of TaskListFileManager.
	/// </summary>
	public class TaskListFileManager
	{
		public TaskListFileManager()
		{
		}
		public void save(MainForm form)
		{
			var path = util.getJarPath()[0] + "\\";
			
			if (File.Exists(path + "tasklist.ini")) {
				try {
					var _lineLen = File.ReadAllLines(path + "tasklist.ini").Length;
					if (_lineLen > 5)
						File.Copy(path + "tasklist.ini", path + "tasklist_backup.ini", true);
				} catch (Exception e) {
					util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				}
			}
			
			using (var sw = new StreamWriter(path + "tasklist.ini", false, Encoding.UTF8)) {
				sw.WriteLine("200");
				foreach (var ti in form.taskListDataSource) {
					for (var i = 0; i < 22; i++) { //namaroku 29 save 32
						if (i == 0) sw.WriteLine(ti.taskTimeStr);
						else if (i == 1) sw.WriteLine(ti.lvId);
						else if (i == 2) sw.WriteLine(ti.args);
						else if (i == 3) sw.WriteLine(ti.addDate);
						else if (i == 4) sw.WriteLine(ti.status);
						else if (i == 5) sw.WriteLine(ti.popup.ToString().ToLower());
						else if (i == 6) sw.WriteLine(ti.baloon.ToString().ToLower());
						else if (i == 7) sw.WriteLine(ti.browser.ToString().ToLower());
						else if (i == 8) sw.WriteLine(ti.sound.ToString().ToLower());
						else if (i == 9) sw.WriteLine(ti.appliA.ToString().ToLower());
						else if (i == 10) sw.WriteLine(ti.appliB.ToString().ToLower());
						else if (i == 11) sw.WriteLine(ti.appliC.ToString().ToLower());
						else if (i == 12) sw.WriteLine(ti.appliD.ToString().ToLower());
						else if (i == 13) sw.WriteLine(ti.appliE.ToString().ToLower());
						else if (i == 14) sw.WriteLine(ti.appliF.ToString().ToLower());
						else if (i == 15) sw.WriteLine(ti.appliG.ToString().ToLower());
						else if (i == 16) sw.WriteLine(ti.appliH.ToString().ToLower());
						else if (i == 17) sw.WriteLine(ti.appliI.ToString().ToLower());
						else if (i == 18) sw.WriteLine(ti.appliJ.ToString().ToLower());
						else if (i == 19) sw.WriteLine(ti.memo);
	                    else if (i == 20) sw.WriteLine(ti.isDelete.ToString().ToLower());
	                    else if (i == 21) sw.WriteLine(ti.mail.ToString().ToLower());
						//else sw.WriteLine("");
					}
				}
				sw.WriteLine("EndLine");
				//sw.Close();
			}
		}
		public void load(MainForm form)
		{
			ReadNamarokuList(form, form.taskListDataSource, util.getJarPath()[0] + "\\tasklist.ini");
			
		}
		public void ReadNamarokuList(MainForm form, SortableBindingList<TaskInfo> alartListDataSource, string fileName)
		{
			string[] lines;
			
			try {
				if (!File.Exists(fileName)) return;
				using (var sr = new StreamReader(fileName, Encoding.UTF8)) {
					lines = sr.ReadToEnd().Replace("\r", "").Split('\n');
					//sr.Close();
				}
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
				return;
			}
			if ((lines.Length - 3) % 22 != 0 || lines[0] != "200") {
				form.showMessageBox("タスクリストを読み込めませんでした", "");
				return;
			}
			
			var readTiList = new List<TaskInfo>();
			
			for (var i = 1; i < lines.Length - 2; i += 22) {
				
				TaskInfo ti;
				
	            ti = new TaskInfo(lines[i + 0], 
				        lines[i + 1], lines[i + 2], lines[i + 3], lines[i + 4],
						lines[i + 5] == "true",
						lines[i + 6] == "true",
						lines[i + 7] == "true",lines[i + 21] == "true",
						lines[i + 8] == "true",
						lines[i + 9] == "true", 
						lines[i + 10] == "true", 
						lines[i + 11] == "true",
						lines[i + 12] == "true",
						lines[i + 13] == "true",
						lines[i + 14] == "true",
						lines[i + 15] == "true",
						lines[i + 16] == "true",
						lines[i + 17] == "true",
						lines[i + 18] == "true",
						lines[i + 19],
						lines[i + 20] == "true");
				
				readTiList.Add(ti);
			}
			for (var j = 0; j < 100; j++) {
	       		if (!form.IsDisposed && form.IsHandleCreated) break;
	       		Thread.Sleep(1000);
			}
			
			
			for (var i = 0; i < readTiList.Count; i++) {
				var ti = readTiList[i];
				form.taskListAdd(ti);
			}
		}
	}
}
