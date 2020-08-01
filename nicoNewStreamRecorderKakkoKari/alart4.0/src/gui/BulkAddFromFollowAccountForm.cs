/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2019/03/31
 * Time: 1:25
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace namaichi
{
	/// <summary>
	/// Description of BulkAddFromFollowAccountForm.
	/// </summary>
	public partial class BulkAddFromFollowAccountForm : Form
	{
		public string mail = null;
		public string pass = null;
		public bool[] follow = new bool[3];
		public BulkAddFromFollowAccountForm(config.config config)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			util.setFontSize(int.Parse(config.get("fontSize")), this, false);
		}
		
		void CancelBtnClick(object sender, EventArgs e)
		{
			Close();
		}
		void OkBtnClick(object sender, EventArgs e)
		{
			mail = mailText.Text;
			pass = passText.Text;
			follow[0] = userChkBox.Checked;
			follow[2] = comChkBox.Checked;
			follow[1] = channelChkBox.Checked;
			Close();
		}
	}
}
