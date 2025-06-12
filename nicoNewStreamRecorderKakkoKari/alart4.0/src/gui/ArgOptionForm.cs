/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2018/05/07
 * Time: 16:17
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace namaichi
{
	/// <summary>
	/// Description of ArgOptionForm.
	/// </summary>
	public partial class ArgOptionForm : Form
	{
		public string ret;
		
		public ArgOptionForm(int fontSize)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			util.setFontSize(fontSize, this, false);
			FileNameTypeDefaultBtnClick(null, null);
			setSampleLabel();
		}
		
		void fileNameTypeOkBtn_Click(object sender, EventArgs e)
		{
			Close();
		}
		
		void fileNameTypeText_Changed(object sender, EventArgs e)
		{
			setSampleLabel();
		}
		void setSampleLabel() {
			fileNameTypeLabel.Text = 
					util.getFileNameTypeSample(fileNameTypeText.Text);
		}
		
		void CopyBtnClick(object sender, EventArgs e)
		{
			Clipboard.SetText(fileNameTypeText.Text);
		}
		
		void FileNameTypeDefaultBtnClick(object sender, EventArgs e)
		{
			fileNameTypeText.Text = "-url {url} -tag \"{4}_{1}\"";
		}
		
		void FileNameTypeDateBtnClick(object sender, EventArgs e)
		{
			fileNameTypeText.Text = "{nourl}";
		}
	}
}
