/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2019/01/14
 * Time: 7:51
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using namaichi.info;
using namaichi.alart;

namespace namaichi
{
	/// <summary>
	/// Description of SmallPopupForm.
	/// </summary>
	public partial class SmallPopupForm : PopupFormBase
	{
//		private config.config config;
//		private RssItem ri;
		private bool isTopMostPara = false;
		
		public SmallPopupForm(RssItem item, config.config config, 
				PopupDisplay pd, int showIndex, AlartInfo ai,
				bool isTest = false, string poploc = null, int poptime = 0,
				bool isClickClose = true,  
				bool isTopMost = true, Color[] isColor = null, double opacity = 0.9)
		{
			this.config = config;
			this.ri = item;
			this.pd = pd;
			this.showIndex = showIndex;
			
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//var dt = DateTime.Now;
			var dtStr = "";
			if (item.pubDate != null && item.pubDate != "") {
				var dt = DateTime.Parse(item.pubDate);
				dtStr = dt.ToString("yyyy\"/\"MM\"/\"dd(ddd) HH:mm") + "開始";
			}
//			util.debugWriteLine(dtStr);
			
			//var t = "haimasa_qm(歌ってみた)fw";
			var t = item.title;
			if (isOkStrWidth(t + " - ")) t += " - ";
			if (isOkStrWidth(t + dtStr)) t += dtStr;
			titleLabel.Text = util.removeTag(t);
			//titleLabel.Text = (item.isMemberOnly ? "(限定)" : "") + titleLabel.Text;
			
			hostNameLabel.Text = util.removeTag(item.hostName);
			communityNameLabel.Text = util.removeTag(item.comName);
			descryptionLabel.Text = util.removeTag(item.description);
			var _Text = util.removeTag(item.hostName + "/" + item.comName);
			if (ai != null && ai.keyword != null && ai.keyword != "") _Text = ai.keyword + "-" + _Text;
			Text = _Text;
			
			if (isTest && isColor != null) {
				BackColor = isColor[0];//Color.FromArgb(255,224,255);
				ForeColor = isColor[1];//Color.Black;
				titleLabel.LinkColor = ForeColor;
			} else if (ai != null && bool.Parse(config.get("IsColorPopup"))) {
				BackColor = ai.backColor;
				ForeColor = ai.textColor;
				titleLabel.LinkColor = ForeColor;
			}
			var url = "https://live.nicovideo.jp/watch/" + item.lvId;
			titleLabel.Links.Add(0, 0, url);
			if (isTest) this.isTopMostPara = isTopMost;
			else this.isTopMostPara = bool.Parse(config.get("IsTopMostPopup"));
			
			Opacity = isTest ? (opacity / 100) : double.Parse(config.get("popupOpacity")) / 100;
			
			this.isTestClosePopup = isClickClose;
			this.testPopTime = poptime;
			this.isTest = isTest;
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			ContextMenuStrip = contextMenuStrip1;
			setAppliMenuVisible();
			
			if (item.isMemberOnly) {
				try {
					ShowIcon = true;
					var icon = new Icon(util.getJarPath()[0] + "/Icon/lock.ico");
					Icon = util.changeIconColor(icon, ColorTranslator.FromHtml(config.get("onlyIconColor")));
				} catch (Exception e) {
					util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				}
			}
		}
		private bool isOkStrWidth(string s) {
			var w = TextRenderer.MeasureText(s, titleLabel.Font).Width;
			return (w < titleLabel.Width * 2 - 2); 
		}
		public void setSamune(string url) {
			//if (string.IsNullOrEmpty(url)) return;
			if (isTest) {
				thumbnailPictureBox.Image = new Bitmap(Image.FromFile(ri.thumbnailUrl), thumbnailPictureBox.Size);
					return;
			}
			var img = ThumbnailManager.getImageId(ri.comId);
			var isSaveCache = bool.Parse(config.get("alartCacheIcon"));
			if (img != null && isSaveCache) ThumbnailManager.saveImage(img, ri.comId);
			if (img == null && !string.IsNullOrEmpty(url)) 
				img = ThumbnailManager.getThumbnailRssUrl(url, isSaveCache, true);
			thumbnailPictureBox.Image = img;
			/*
       		if (!util.isShowWindow) return;
       		if (IsDisposed) return;
       		WebClient cl = new WebClient();
       		cl.Proxy = null;
			
       		System.Drawing.Icon icon =  null;
			try {
       			byte[] pic = cl.DownloadData(url);
				
			
				var  st = new System.IO.MemoryStream(pic);
				icon = Icon.FromHandle(new System.Drawing.Bitmap(st).GetHicon());
				st.Close();
				
				
			} catch (Exception e) {
				util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
				util.debugWriteLine("set samune url error " + url);
				return;
			}
			
			try {
	        	Invoke((MethodInvoker)delegate() {
       			       	try {
						    thumbnailPictureBox.Image = icon.ToBitmap();
							
       			       	} catch (Exception e) {
       			       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
       			       	}
       			});
			} catch (Exception e) {
       			util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
       		}
					
       			
//       			Icon = new System.Drawing.Icon(url);
			*/
		}
		
		
		void TitleLabelLinkClicked(object _sender, LinkLabelLinkClickedEventArgs e)
		{
			LinkLabel sender = (LinkLabel)_sender;
			if (e.Button == MouseButtons.Left) {
				if (sender.Links.Count > 0 && sender.Links[0].Length != 0) {
					string url = (string)sender.Links[0].LinkData;
					util.openUrlBrowser(url, config);
				}
				if ((isTest && isTestClosePopup) ||
			    		(!isTest && config.get("Isclosepopup") == "true"))
					Close();
			} else {
//				if (sender.Links.Count > 0 && sender.Links[0].Length != 0) {
//					labelUrl = (string)sender.Links[0].LinkData;
//					mainWindowRightClickMenu.Show(Cursor.Position);
//				}
			}
		}
		void timeoutCloseProcess() {
			var t = (isTest) ? testPopTime : int.Parse(config.get("poptime"));
			Thread.Sleep(t * 1000);
			for (var o = Opacity; o > -0.00; o -= 0.05) {
				setOpacity(o);
				Thread.Sleep(100);
			}
			_close();
			
		}
		void setOpacity(double o) {
			try {
	        	Invoke((MethodInvoker)delegate() {
       			       	try {
						    Opacity = o;
       			       	} catch (Exception e) {
       			       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
       			       	}
       			});
			} catch (Exception e) {
       			util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
       		}
		}
		void _close() {
			try {
	        	Invoke((MethodInvoker)delegate() {
       			       	try {
				       		Close();
       			       	} catch (Exception e) {
       			       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
       			       	}
       			});
			} catch (Exception e) {
       			util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
       		}
		}
		void SmallPopupFormLoad(object sender, EventArgs e)
		{
			setSamune(ri.thumbnailUrl);
			//Task.Factory.StartNew(() => setSamune(ri.thumbnailUrl));
			Task.Factory.StartNew(() => timeoutCloseProcess());
		}
		
		
		void SmallPopupFormFormClosed(object sender, FormClosedEventArgs e)
		{
			try {
				pd.posList.Remove(showIndex);
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		protected override bool ShowWithoutActivation {
	        get {
	            return true;
	        }
	    }
		protected override CreateParams CreateParams {
	        get {
				CreateParams p = base.CreateParams;
				if (isTopMostPara) {
					 var WS_EX_TOPMOST = 0x00000008;
					p.ExStyle |= WS_EX_TOPMOST;
				}
	            return p;
	        }
	    }
		
	}
}
