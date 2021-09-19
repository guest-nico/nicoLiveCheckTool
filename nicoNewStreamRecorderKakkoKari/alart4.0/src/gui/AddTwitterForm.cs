/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2021/03/01
 * Time: 15:30
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using namaichi.alart;
using namaichi.info;

namespace namaichi
{
	/// <summary>
	/// Description of AddTwitterForm.
	/// </summary>
	public partial class AddTwitterForm : Form
	{
		public TwitterInfo ret = null;
		public AddTwitterForm(string id = null)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			if (id != null)
				accountText.Text = id;
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void GetInfoFromHosoIdBtnClick(object sender, EventArgs e)
		{
			var name = strToName(accountText.Text);
			if (name == null) {
				MessageBox.Show("アカウントが見つかりませんでした");
				return;
			}
			accountText.Text = name;
			var id = TwitterCheck.isUserExist(name);
			
			MessageBox.Show(id == null ? "アカウントが見つかりませんでした" : "成功");
		}
		string strToName(string s) {
			var m = new Regex("(https://twitter.com/)*@*([^/]+)").Match(accountText.Text);
			return m.Success ? m.Groups[2].Value : null;
		}
		void Button3Click(object sender, EventArgs e)
		{
			var name = strToName(accountText.Text);
			if (name == null) {
				MessageBox.Show("アカウントが見つかりませんでした");
				return;
			}
			accountText.Text = name;
			var id = TwitterCheck.isUserExist(name);
			if (id == null) {
				MessageBox.Show("アカウントが見つかりませんでした");
				return;
			}
			
			var now = DateTime.Now.ToString("yyyy\"/\"MM\"/\"dd HH\":\"mm\":\"ss");
			var addDate = now;
			
			var _ret = new TwitterInfo(name, addDate,
					isPopupChkBox.Checked,
					isBaloonChkBox.Checked, isWebChkBox.Checked, 
					isMailChkBox.Checked, isSoundChkBox.Checked,
					appliAChkBox.Checked, appliBChkBox.Checked, 
					appliCChkBox.Checked, appliDChkBox.Checked, 
					appliEChkBox.Checked, appliFChkBox.Checked,
					appliGChkBox.Checked, appliHChkBox.Checked,
					appliIChkBox.Checked, appliJChkBox.Checked,
					memoText.Text, id);
			
			ret = _ret;
			Close();
		}
		
		void Button4Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
