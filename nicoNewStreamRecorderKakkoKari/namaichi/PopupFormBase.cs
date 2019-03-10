/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2019/01/17
 * Time: 4:15
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using namaichi.info;
using namaichi.alart;

namespace namaichi
{
	/// <summary>
	/// Description of PopupFormBase.
	/// </summary>
	public partial class PopupFormBase : Form
	{
		protected config.config config;
		protected RssItem ri;
		protected PopupDisplay pd;
		protected int showIndex = 0;
		
		public PopupFormBase()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			
		}
		
		protected void CopyUrlMenuClick(object sender, EventArgs e)
		{
			if (ri.lvId == null || ri.lvId == "") return;
			var url = "http://live2.nicovideo.jp/watch/lv" + util.getRegGroup(ri.lvId, "(\\d+)");
			util.setClipBordText(url);
		}
		
		protected void CopyCommunityUrlMenuClick(object sender, EventArgs e)
		{
			var isChannel = ri.comId.IndexOf("ch") > -1;
			var url = (isChannel) ? 
				("http://ch.nicovideo.jp/" + ri.comId) :
				("https://com.nicovideo.jp/community/" + ri.comId);
			util.setClipBordText(url);
		}
		
		
		
		void CopyTitleMenuClick(object sender, EventArgs e)
		{
			util.setClipBordText(ri.title);
		}
		
		void CopyHostNameMenuClick(object sender, EventArgs e)
		{
			util.setClipBordText(ri.hostName);
		}
		
		void CopyCommunityNameMenuClick(object sender, EventArgs e)
		{
			util.setClipBordText(ri.comName);
		}
		void OpenWebBrowserMenuClick(object sender, EventArgs e)
		{
			if (ri.lvId == null || ri.lvId == "") return;
			var url = "http://live2.nicovideo.jp/watch/lv" + util.getRegGroup(ri.lvId, "(\\d+)");
			util.openUrlBrowser(url, config);
		}
		void OpenAppliMenuClick(object sender, EventArgs e)
		{
			var i = ((ToolStripMenuItem)sender).Name[9];
			var path = config.get("appli" + i.ToString() + "Path");
			var url = "http://live2.nicovideo.jp/watch/lv" + util.getRegGroup(ri.lvId, "(\\d+)");
			var args = config.get("appli" + i.ToString() + "Args");
			appliProcess(path, url + " " + args);
		}
		
		private void appliProcess(string appliPath, string url) {
			if (appliPath == null || appliPath == "") return;

			try {
				Process.Start(appliPath, url);
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
		}
		protected void setAppliMenuVisible() {
			var isTask = ri.hostName == "";
			var n = (char)'A';
			for (var i = 0; i < 10; i++) {
				var l = ((char)(n + i));
				//ShowTaskAppliA ShowAppA
				var s = ((isTask) ? "ShowTaskAppli" : "ShowApp") + l;
				var isDisp = bool.Parse(config.get(s));
				contextMenuStrip1.Items[8 + i].Visible = isDisp;
				
				var name = config.get("appli" + l + "Name");
				if (name == "") name = "アプリ" + l;
				contextMenuStrip1.Items[8 + i].Text = name + "で開く";
			}
			//var nnn = nn;
		}
	}
}
