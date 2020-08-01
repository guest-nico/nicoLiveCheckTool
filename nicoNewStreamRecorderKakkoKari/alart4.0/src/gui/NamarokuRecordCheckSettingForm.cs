/*
 * Created by SharpDevelop.
 * User: user
 * Date: 2019/08/16
 * Time: 20:16
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace namaichi
{
	/// <summary>
	/// Description of NamarokuRecordCheckSettingForm.
	/// </summary>
	public partial class NamarokuRecordCheckSettingForm : Form
	{
		public string recordCheck = null;
		public NamarokuRecordCheckSettingForm(int fontSize)
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
			for (var i = 0; i < 11; i++) {
				var c = (RadioButton)groupBox1.Controls[i];
				if (c.Checked) recordCheck = c.Name;
			}
			//DialogResult = DialogResult.OK;
			Close();
		}
	}
}
