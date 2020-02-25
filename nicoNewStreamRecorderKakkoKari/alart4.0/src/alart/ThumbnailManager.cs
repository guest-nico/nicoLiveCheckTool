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
		public static Image getThumbnailRssUrl(string url, bool isSaveCache, bool isGetIcon) {
			if (url == null) return null;
			
			var id = util.getRegGroup(url, "(c[oh]\\d+)");
			if (id != null) {
				//if (id == null) return null;
				var dir = util.getJarPath()[0] + "/ImageCommunity";
				Image img = null;
				if (isExist(id, out img, dir)) 
					return img;
				
				if (!isGetIcon) {
					try {
						return Image.FromFile(util.getJarPath()[0] + "/ImageCommunity/no thumb com.jpg");
					} catch (Exception e) {
						return null;
					}
				}
				//img = getImage(url);
				img = getImageId(id);
				
				
				if (isSaveCache)
					saveImage(img, id, dir);
				
				return img;
			} else {
				return getImage(url);
			}
		}
		public static Image getThumbnailId(string id) {
			
			//var id = util.getRegGroup(url, "(c[oh]\\d+)");
			var dir = util.getJarPath()[0] + "/" + (!id.StartsWith("c") ? "ImageUser" : "ImageCommunity");
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
				dir = util.getJarPath()[0] + "/" + (isUser ? "ImageUser" : "ImageCommunity");
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
		public static Image getImageId(string id, MainForm form = null) {
			util.debugWriteLine("getimageid id " + id);
			
			
			string url = null;
			if (!id.StartsWith("c")) {
				var thumbUrl = "https://ext.nicovideo.jp/thumb_user/" + id;
				var thumbRes = util.getPageSource(thumbUrl, null);
				url = util.getRegGroup(thumbRes, "(http[^\"]+" + id + "[^\"]*(jpg|gif)[^\"]*)\"");
				if (url == null) {
					util.debugWriteLine("getImageId id " + id + " res " + thumbRes);
					if (form != null) {
						if (thumbRes.IndexOf("blank.jpg\"") > -1) 
							form.addLogText(id + "はサムネイルが設定されていませんでした");
						if (thumbRes.IndexOf("このコミュニティは非公開(クローズ)コミュニティです。") > -1) 
							form.addLogText(id + "は非公開コミュニティでした");
						form.addLogText("サムネイルの取得に失敗しました(" + id + ")");
					}
					return null;
				}
			} else {
				url = id.StartsWith("co") ? ("https://secure-dcdn.cdn.nimg.jp/comch/community-icon/128x128/" + id + ".jpg") : 
				 	("https://secure-dcdn.cdn.nimg.jp/comch/channel-icon/128x128/" + id + ".jpg");
			}
			
			/*
			var thumbUrl = !id.StartsWith("c") ? 
		           ("https://ext.nicovideo.jp/thumb_user/" + id) : 
					(id.StartsWith("co") ? ("https://ext.nicovideo.jp/thumb_community/" + id) : 
				 	("https://ext.nicovideo.jp/thumb_channel/" + id));
			*/
			
			var ret = getImage(url);
			if (ret == null && form != null) {
				form.addLogText("サムネイルの取得に失敗しました(" + id + ")");
			}
			return ret;
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
				Image _ret = null;
				using (var dataStream = res.GetResponseStream()) {
					
					var ret = Image.FromStream(dataStream);
					//dataStream.Dispose();
					//dataStream.Close();
					_ret = ret;
				}
				return _ret;
			} catch (Exception e) {
				//System.Threading.Tasks.Task.Factory.StartNew(() => {
				//	util.debugWriteLine("thumbnail getpage error " + url + e.Message+e.StackTrace);
				//});
				util.debugWriteLine("getImage url exception " + url + " " + e.StackTrace);
				try {
					if (util.getRegGroup(url, "(c[oh]\\d+)") != null) {
						var ret = Image.FromFile(util.getJarPath()[0] + "/ImageCommunity/no thumb com.jpg");
						return ret;
					} else 
						return Image.FromFile(util.getJarPath()[0] + "/ImageUser/blank.jpg");
				} catch (Exception ee) {
					util.debugWriteLine("getImage exception catch catch " + ee.Message + e.StackTrace);
					return null;
				}
			}
		}
		public static void saveImage(Image img, string id, string dir = null) {
			if (img == null) return;
			var maxSize = id.StartsWith("c") ? 128 : 150;
			if (img.Size.Width > maxSize || img.Size.Width > maxSize) {
				var size = (img.Size.Width > img.Size.Height) ? 
					(new Size(maxSize, maxSize * img.Size.Height / img.Size.Width)) :
					(new Size(maxSize * img.Size.Width / img.Size.Height, maxSize));
				Bitmap buf;
				using (var bmp = new Bitmap(img, size)) {
					buf = (Bitmap)bmp.Clone();
					bmp.Dispose();
				}
				img = buf;
			}
			if (dir == null)
				dir = util.getJarPath()[0] + "/" + (!id.StartsWith("c") ? "ImageUser" : "ImageCommunity");
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
		public static void deleteImageId(string id) {
			var isUser = !id.StartsWith("c");
			var dir = util.getJarPath()[0] + "/" + (isUser ? "ImageUser" : "ImageCommunity");
			if (!Directory.Exists(dir)) return;
			var isExists = File.Exists(dir + "/" + id + ".jpg");
			if (isExists) {
				try {
					File.Delete(dir + "/" + id + ".jpg");
				} catch (Exception e) {
					util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				}
			}
		}
	}
}
