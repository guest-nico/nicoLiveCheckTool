/*
 * Created by SharpDevelop.
 * User: user
 * Date: 2019/01/18
 * Time: 20:46
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Diagnostics;
using System.Media;
using System.Net;
using System.Threading;
using namaichi.info;
using namaichi.rec;

namespace namaichi.alart
{
	/// <summary>
	/// Description of TaskCheck.
	/// </summary>
	public class TaskCheck
	{
		private MainForm form;
		private SortableBindingList<TaskInfo> taskListDataSource;
		
		public TaskCheck(SortableBindingList<TaskInfo> taskListDataSource, MainForm form)
		{
			this.taskListDataSource = taskListDataSource;
			this.form = form;
			
		}
		public void start() {
			while (true) {
				var now = DateTime.Now;
				try {
					var c = form.getTaskListCount();
					for (var i = 0; i < c; i++) {
						var ti = taskListDataSource[i];
						if (ti.taskDt < now && ti.status == "待機中") process(ti);
					}
				} catch (Exception e) {
					util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				}
				
				
				//recentLiveCheck();
				Thread.Sleep(1000);
			}
		}
		private void process(TaskInfo ti) {
			var item = new RssItem(ti.lvId, ti.lvId, "", ti.args, "", "",
			                     "", "", "", "", false);
					               
			if (ti.appliA && !form.notifyOffList[7]) {
				var appliPath = form.config.get("appliAPath");
				var args = form.config.get("appliAArgs") + " " + ti.args;
				appliProcessFromLvid(appliPath, ti.lvId, args, 0);
			}
			if (ti.appliB && !form.notifyOffList[8]) {
				var appliPath = form.config.get("appliBPath");
				var args = form.config.get("appliBArgs") + " " + ti.args;
				appliProcessFromLvid(appliPath, ti.lvId, args, 1);
			}
			if (ti.appliC && !form.notifyOffList[9]) {
				var appliPath = form.config.get("appliCPath");
				var args = form.config.get("appliCArgs") + " " + ti.args;
				appliProcessFromLvid(appliPath, ti.lvId, args, 2);
			}
			if (ti.appliD && !form.notifyOffList[10]) {
				var appliPath = form.config.get("appliDPath");
				var args = form.config.get("appliDArgs") + " " + ti.args;
				appliProcessFromLvid(appliPath, ti.lvId, args, 3);
			}
			if (ti.appliE && !form.notifyOffList[11]) {
				var appliPath = form.config.get("appliEPath");
				var args = form.config.get("appliEArgs") + " " + ti.args;
				appliProcessFromLvid(appliPath, ti.lvId, args, 4);
			}
			if (ti.appliF && !form.notifyOffList[12]) {
				var appliPath = form.config.get("appliFPath");
				var args = form.config.get("appliFArgs") + " " + ti.args;
				appliProcessFromLvid(appliPath, ti.lvId, args, 5);
			}
			if (ti.appliG && !form.notifyOffList[13]) {
				var appliPath = form.config.get("appliGPath");
				var args = form.config.get("appliGArgs") + " " + ti.args;
				appliProcessFromLvid(appliPath, ti.lvId, args, 6);
			}
			if (ti.appliH && !form.notifyOffList[14]) {
				var appliPath = form.config.get("appliHPath");
				var args = form.config.get("appliHArgs") + " " + ti.args;
				appliProcessFromLvid(appliPath, ti.lvId, args, 7);
			}
			if (ti.appliI && !form.notifyOffList[15]) {
				var appliPath = form.config.get("appliIPath");
				var args = form.config.get("appliIArgs") + " " + ti.args;
				appliProcessFromLvid(appliPath, ti.lvId, args, 8);
			}
			if (ti.appliJ && !form.notifyOffList[16]) {
				var appliPath = form.config.get("appliJPath");
				var args = form.config.get("appliJArgs") + " " + ti.args;
				appliProcessFromLvid(appliPath, ti.lvId, args, 9);
			}
			if (ti.popup && !form.notifyOffList[2]) {
				displayPopup(item, form);
			}
			if (ti.baloon && !form.notifyOffList[3]) {
				displayBaloon(item, form);
			}
			if (ti.browser && !form.notifyOffList[4]) {
				openBrowser(item, form);
			}
			if (ti.mail && !form.notifyOffList[5]) {
				form.check.mail(item);
			}
			if (ti.sound && !form.notifyOffList[6]) {
				sound(form);
			}
			
			form.taskListUpdateState(ti);
			if (ti.isDelete) form.taskListRemoveLine(ti);
		}
		public static void displayPopup(RssItem item, MainForm form) {
			form.check.popup.show(item, null);
		}
		public static void displayBaloon(RssItem item, MainForm form) {
			form.DisplayBalloon(item, null);
		}
		public static void sound(MainForm form) {
			try {
				/*
				if (form.check.soundPlayer == null) 
					form.check.soundPlayer = new SoundPlayer("Sound/se_soc01.wav");
				form.check.soundPlayer.Play();
				*/
				util.playSound(form.config, null, form);
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
		}
		public static void openBrowser(RssItem item, MainForm form) {
			var url = "https://live.nicovideo.jp/watch/lv" + util.getRegGroup(item.lvId, "(\\d+)");
			util.openUrlBrowser(url, form.config);
		}
		void appliProcessFromLvid(string path , string lvid, string args, int appNum) {
			util.appliProcessFromLvid(path, lvid, args, form.check.container, form.config, appNum, form);
		}
	}
}
