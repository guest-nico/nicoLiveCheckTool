/*
 * Created by SharpDevelop.
 * User: user
 * Date: 2019/03/22
 * Time: 0:57
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace namaichi
{
	/// <summary>
	/// Description of WriteThumbnailMemoForm.
	/// </summary>
	public partial class WriteThumbnailMemoForm : Form
	{
		private Image img;
		public Image resultImage;

		public WriteThumbnailMemoForm(Image img)
		{
			this.img = img;

			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();

			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			thumbBox.Image = resetImage();
		}
		void CancelBtnClick(object sender, EventArgs e)
		{
			Close();
		}
		void TextEvent(object sender, EventArgs e)
		{
			thumbBox.Image = resetImage();

			if (text.Text == "")
			{
				//thumbBox.Image = img;
				return;
			}
			//if (sizeList.Value == 0) return;
			//thumbBox.Image = new Bitmap(img);


			var g = Graphics.FromImage(thumbBox.Image);
			var font = new Font("MS UI Gothic", (int)sizeList.Value, FontStyle.Regular);
			var path = new System.Drawing.Drawing2D.GraphicsPath();
			path.AddString(text.Text, FontFamily.GenericSansSerif, 0, (float)sizeList.Value, new PointF((float)xList.Value, (float)yList.Value), StringFormat.GenericDefault);

			g.FillPath(new SolidBrush(Color.White), path);
			g.DrawPath(new Pen(Color.FromArgb(50, 50, 50)), path);
			g.Flush();
		}

		void OkBtnClick(object sender, EventArgs e)
		{
			resultImage = thumbBox.Image;
			DialogResult = DialogResult.OK;
			Close();
		}
		Bitmap resetImage()
		{
			Bitmap buf;
			using (var bmp = new Bitmap(img, thumbBox.Size))
			{
				buf = (Bitmap)bmp.Clone();
			}
			return buf;
		}
	}
}
