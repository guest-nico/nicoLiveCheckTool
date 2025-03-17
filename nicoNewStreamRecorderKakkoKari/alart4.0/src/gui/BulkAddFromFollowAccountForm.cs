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
		public bool isAddToCom {get; set;}
		public bool isBulkAddAuto {get; set;}
		public string bulkTypes = null;
		public BulkAddFromFollowAccountForm(int fontSize)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			util.setFontSize(fontSize, this, false);
		}
		
		void CancelBtnClick(object sender, EventArgs e)
		{
			Close();
		}
		void OkBtnClick(object sender, EventArgs e)
		{
			mail = mailText.Text;
			pass = passText.Text;
			isAddToCom = comRadioBtn.Checked;
			follow[0] = userChkBox.Checked;
			follow[2] = comChkBox.Checked && isAddToCom;
			follow[1] = channelChkBox.Checked && isAddToCom;
			isBulkAddAuto = IsBuldAddAutoChkBox.Checked;
			DialogResult = DialogResult.OK;
			Close();
		}
		void UserRadioBtnCheckedChanged(object sender, EventArgs e)
		{
			comChkBox.Enabled = channelChkBox.Enabled = comRadioBtn.Checked;
		}
	}
}
