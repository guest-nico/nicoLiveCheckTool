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
using System.Threading;
using namaichi.info;

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
			                     "", "", "", "");
					               
			if (ti.appliA && !form.notifyOffList[6]) {
				var appliAPath = form.config.get("appliAPath");
				var args = form.config.get("appliAArgs") + " " + ti.args;
				appliProcess(appliAPath, ti.lvId, args);
			}
			if (ti.appliB && !form.notifyOffList[7]) {
				var appliBPath = form.config.get("appliBPath");
				var args = form.config.get("appliBArgs") + " " + ti.args;
				appliProcess(appliBPath, ti.lvId, args);
			}
			if (ti.appliC && !form.notifyOffList[8]) {
				var appliCPath = form.config.get("appliCPath");
				var args = form.config.get("appliCArgs") + " " + ti.args;
				appliProcess(appliCPath, ti.lvId, args);
			}
			if (ti.appliD && !form.notifyOffList[9]) {
				var appliDPath = form.config.get("appliDPath");
				var args = form.config.get("appliDArgs") + " " + ti.args;
				appliProcess(appliDPath, ti.lvId, args);
			}
			if (ti.appliE && !form.notifyOffList[10]) {
				var appliEPath = form.config.get("appliEPath");
				var args = form.config.get("appliEArgs") + " " + ti.args;
				appliProcess(appliEPath, ti.lvId, args);
			}
			if (ti.appliF && !form.notifyOffList[11]) {
				var appliFPath = form.config.get("appliFPath");
				var args = form.config.get("appliFArgs") + " " + ti.args;
				appliProcess(appliFPath, ti.lvId, args);
			}
			if (ti.appliG && !form.notifyOffList[12]) {
				var appliGPath = form.config.get("appliGPath");
				var args = form.config.get("appliGArgs") + " " + ti.args;
				appliProcess(appliGPath, ti.lvId, args);
			}
			if (ti.appliH && !form.notifyOffList[13]) {
				var appliHPath = form.config.get("appliHPath");
				var args = form.config.get("appliHArgs") + " " + ti.args;
				appliProcess(appliHPath, ti.lvId, args);
			}
			if (ti.appliI && !form.notifyOffList[14]) {
				var appliIPath = form.config.get("appliIPath");
				var args = form.config.get("appliIArgs") + " " + ti.args;
				appliProcess(appliIPath, ti.lvId, args);
			}
			if (ti.appliJ && !form.notifyOffList[15]) {
				var appliJPath = form.config.get("appliJPath");
				var args = form.config.get("appliJArgs") + " " + ti.args;
				appliProcess(appliJPath, ti.lvId, args);
			}
			if (ti.popup && !form.notifyOffList[2]) {
				displayPopup(item);
			}
			if (ti.baloon && !form.notifyOffList[3]) {
				displayBaloon(item);
			}
			if (ti.browser && !form.notifyOffList[4]) {
				openBrowser(item);
			}
			if (ti.sound && !form.notifyOffList[5]) {
				sound(item);
			}
			
			form.taskListUpdateState(ti);
			if (ti.isDelete) form.taskListRemoveLine(ti);
		}
		private void appliProcess(string appliPath, string lvid, string args) {
			if (appliPath == null || appliPath == "") return;
			var url = "http://live2.nicovideo.jp/watch/lv" + util.getRegGroup(lvid, "(\\d+)");

			try {
				appliPath = appliPath.Trim();
				
				Process.Start(appliPath, url + " " + args);
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
		}
		private void displayPopup(RssItem item) {
			form.check.popup.show(item, null);
		}
		private void displayBaloon(RssItem item) {
			form.DisplayBalloon(item, null);
		}
		private void sound(RssItem ri) {
			try {
				/*
				if (form.check.soundPlayer == null) 
					form.check.soundPlayer = new SoundPlayer("Sound/se_soc01.wav");
				form.check.soundPlayer.Play();
				*/
				util.playSound(form.config);
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
		}
		private void openBrowser(RssItem item) {
			var url = "http://live2.nicovideo.jp/watch/lv" + util.getRegGroup(item.lvId, "(\\d+)");
			util.openUrlBrowser(url, form.config);
		}
		
	}
}
