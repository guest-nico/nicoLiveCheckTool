/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2019/01/17
 * Time: 0:19
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using namaichi.info;

namespace namaichi.alart
{
	/// <summary>
	/// Description of PopupDisplay.
	/// </summary>
	public partial class PopupDisplay : Form
	{
		public MainForm form;
		public List<int> posList = new List<int>();
		private Rectangle workingArea;
		private Size[] popupSize = null; //0-normal 1-small
		public PopupDisplay(MainForm form)
		{
			this.form = form;
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			
			workingArea = System.Windows.Forms.Screen.GetWorkingArea(new Point(0,0));
		}
		public void show(RssItem ri, AlartInfo ai) {
			if (popupSize == null) {
				setPopupSize();
			}
			
			var isSmall = bool.Parse(form.config.get("Issmallpopup"));
			var posI = 0;
			var pos = getPos(isSmall, out posI, form.config.get("poploc"),
					bool.Parse(form.config.get("Isfixpopup")));
			util.debugWriteLine("popupDisplay posI " + posI + " pos " + pos + " " + ri.lvId);
			//test
//			if (posList.Count > 10) return;
			form.DisplayPopup(ri, pos, isSmall, this, posI, ai);
		}
		private Point getPos(bool isSmall, out int posI, string poploc, 
				bool isFix) {
			posI = 0;
			for (var i = 0; i < posList.Count + 1; i++)
				if (posList.IndexOf(i) == -1) {
				posI = i;
				break;
			}
			
			//var poploc = form.config.get("poploc");
			//var isFix = bool.Parse(form.config.get("Isfixpopup"));
			if (isFix) posI = 0;
			
			var size = popupSize[isSmall ? 1 : 0];
			var rowCount = (int)(workingArea.Height / size.Height);
			
			int x = 0, y = 0;
			if (poploc.StartsWith("左")) {
				x = ((int)(posI / rowCount)) * size.Width;
			} else {
				x = workingArea.Width - size.Width 
					- (int)(posI / rowCount) * size.Width;
			}
			if (poploc.EndsWith("上")) {
				y = (posI % rowCount) * size.Height;
			} else {
				y = workingArea.Height - size.Height 
					- (posI % rowCount) * size.Height;
			}
			
			posList.Add(posI);
			return new Point(x,y);
		}
		public void showTest(string poploc, int poptime,
					bool isClickClose, bool isSmall,
					bool isTopMost, Color[] isColor, double opacity) {
			var thumbnailUrl = util.getJarPath()[0] + "/ImageCommunity/no thumb com.jpg";
			var ri = new namaichi.info.RssItem("タイトル", "lv1000000",
					DateTime.Now.ToString("yyyy\"/\"MM\"/\"dd HH\":\"mm\":\"ss"), "放送説明", "チャンネル名",
					"チャンネルID", "放送者名", thumbnailUrl, "true", "", false);
			try {
				popupSize = new Size[]{new PopupForm(ri, form.config, this, 0, null, form.check.container).Size,
						new SmallPopupForm(ri, form.config, this, 0, null, form.check.container).Size};
				
				var posI = 0;
				var pos = getPos(isSmall, out posI, poploc,
						true);
				form.DisplayPopup(ri, pos, isSmall, this, posI, null, true, 
						poploc, poptime, isClickClose, 
						isTopMost, isColor, opacity);
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
		}
		public void setPopupSize() {
			var thumbnailUrl = util.getJarPath()[0] + "/ImageCommunity/no thumb com.jpg";
			var ri = new namaichi.info.RssItem("タイトル", "lv1000000",
					DateTime.Now.ToString("yyyy\"/\"MM\"/\"dd HH\":\"mm\":\"ss"), "放送説明", "チャンネル名",
					"チャンネルID", "放送者名", thumbnailUrl, "true", "", false);
			popupSize = new Size[]{new PopupForm(ri, form.config, this, 0, null, form.check.container).Size,
						new SmallPopupForm(ri, form.config, this, 0, null, form.check.container).Size};
		}
	}
}
