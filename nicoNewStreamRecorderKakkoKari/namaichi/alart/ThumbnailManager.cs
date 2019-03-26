/*
 * Created by SharpDevelop.
 * User: user
 * Date: 2019/03/21
 * Time: 20:15
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace namaichi.alart
{
	/// <summary>
	/// Description of ThumbnailManager.
	/// </summary>
	public class ThumbnailManager
	{
		public ThumbnailManager()
		{
		}
		public static Image getThumbnailRssUrl(string url) {
			
			var id = util.getRegGroup(url, "(c[oh]\\d+)");
			var dir = "ImageCommunity";
			Image img = null;
			if (isExist(id, out img, dir))
				return img;
			img = getImage(url);
			saveImage(img, id, dir);
			return img;
		}
		public static Image getThumbnailId(string id) {
			
			//var id = util.getRegGroup(url, "(c[oh]\\d+)");
			var dir = !id.StartsWith("c") ? "ImageUser" : "ImageCommunity";
			Image img = null;
			if (isExist(id, out img, dir))
				return img;
			img = getImage(id);
			saveImage(img, id, dir);
			return img;
		}
		public static bool isExist(string id, out Image img, string dir = null) {
			img = null;
			var isUser = !id.StartsWith("c");
			if (dir == null)
				dir = isUser ? "ImageUser" : "ImageCommunity";
			if (!Directory.Exists(dir)) return false;
			var isExists = File.Exists(dir + "/" + id + ".jpg");
			if (isExists) {
				//Image buf;
				using (var buf = Image.FromFile(dir + "/" + id + ".jpg")) {
					img = new Bitmap(buf);
				}
			}
			
			return isExists;
		}
		public static Image getImageId(string id) {
			util.debugWriteLine("getimageid id " + id);
			var thumbUrl = !id.StartsWith("c") ? 
		           ("https://ext.nicovideo.jp/thumb_user/" + id) : 
					(id.StartsWith("co") ? ("https://ext.nicovideo.jp/thumb_community/" + id) : 
				 	("https://ext.nicovideo.jp/thumb_channel/" + id));
			var thumbRes = util.getPageSource(thumbUrl, null);
			var url = util.getRegGroup(thumbRes, "(http[^\"]+" + id + "[^\"]*(jpg|gif)[^\"]*)\"");
			if (url == null) {
				util.debugWriteLine("getImageId id " + id + " res " + thumbRes);
				return null;
			}
			return getImage(url);
		}
		public static Image getImage(string url) {
			try {
				var req = (HttpWebRequest)WebRequest.Create(url);
				req.Proxy = null;
				req.AllowAutoRedirect = true;
				//if (referer != null) req.Referer = referer;
				//if (container != null) req.CookieContainer = container;

				req.Timeout = 5000;
				var res = (HttpWebResponse)req.GetResponse();
				var dataStream = res.GetResponseStream();
				
				var ret = Image.FromStream(dataStream);
				dataStream.Close();
				return ret;
	
			} catch (Exception e) {
				//System.Threading.Tasks.Task.Run(() => {
				//	util.debugWriteLine("thumbnail getpage error " + url + e.Message+e.StackTrace);
				//});
				util.debugWriteLine("getImage url exception " + url + " " + e.StackTrace);
				try {
					if (util.getRegGroup(url, "(c[oh]\\d+)") != null) {
						var ret = Image.FromFile("ImageCommunity/no thumb com.jpg");
						return ret;
					} else 
						return Image.FromFile("ImageUser/blank.jpg");
				} catch (Exception ee) {
					util.debugWriteLine("getImage exception catch catch " + ee.Message + e.StackTrace);
					return null;
				}
			}
		}
		public static void saveImage(Image img, string id, string dir = null) {
			if (img == null) return;
			if (img.Size.Width > 150 || img.Size.Width > 150) {
				var size = (img.Size.Width > img.Size.Height) ? 
					(new Size(150, 150 * img.Size.Height / img.Size.Width)) :
					(new Size(150 * img.Size.Width / img.Size.Height, 150));
				Bitmap buf;
				using (var bmp = new Bitmap(img, size)) {
					buf = (Bitmap)bmp.Clone();
				}
				img = buf;
			}
			if (dir == null)
				dir = !id.StartsWith("c") ? "ImageUser" : "ImageCommunity";
			if (!Directory.Exists(dir))
				Directory.CreateDirectory(dir);
			img.Save(dir + "/" + id + ".jpg");
		}
		public static Image writeMemo(Image img)
		{
			var f = new WriteThumbnailMemoForm(img);
			var ret = f.ShowDialog();
			if (ret != DialogResult.OK) return null;
			return f.resultImage;
		}
		public static Image writeNG(Image img)
		{
			img = new Bitmap(img, 64, 64);
			var g = Graphics.FromImage(img);
			//var font = new Font("MS UI Gothic", 25, FontStyle.Regular);
			g.DrawLine(new Pen(Color.Red, 3), 0, 0, img.Width, img.Height);
			g.DrawLine(new Pen(Color.Red, 3), img.Width, 0, 0, img.Height);
			var path = new System.Drawing.Drawing2D.GraphicsPath();
			path.AddString("NG", FontFamily.GenericSansSerif, 0, 30, new PointF(3, 11), StringFormat.GenericDefault);
			g.FillPath(new SolidBrush(Color.Red), path);
			g.DrawPath(new Pen(Color.FromArgb(10, 10, 10)), path);
			g.Flush();
			
			//if (ret != DialogResult.OK) return null;
			return img;
		}
	}
}
