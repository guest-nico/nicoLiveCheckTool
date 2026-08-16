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
using System.Net;
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
		config.config cfg = null;
		MainForm form = null;
		public CookieContainer cc = null;
		public BulkAddFromFollowAccountForm(int fontSize, config.config cfg, MainForm form)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			this.cfg = cfg;
			this.form = form;
			util.setFontSize(fontSize, this, false);
		}
		
		void CancelBtnClick(object sender, EventArgs e)
		{
			Close();
		}
		void OkBtnClick(object sender, EventArgs e)
		{
			//mail = mailText.Text;
			//pass = passText.Text;
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
		
		void ReleaseBtnClick(object sender, EventArgs e)
		{
			cfg.set("IsBulkAddAuto", "false");
		}
		
		void LoginBtnClick(object sender, EventArgs e)
		{
			#if NET40
				MessageBox.Show("現在、こちらの機能は.NET Framework 4.5版でのみ動作が可能となっております。申し訳ありません。");
				return;
			#endif
			if (!util.isWebView2Installed()) {
				MessageBox.Show("現在のところ、こちらのログイン方法ではWebView2 Runtimeが必要です。\nhttps://developer.microsoft.com/ja-jp/microsoft-edge/webview2?form=MA13LH&cs=2463970835#download");
				util.openUrlBrowser("https://developer.microsoft.com/ja-jp/microsoft-edge/webview2?form=MA13LH&cs=2463970835#download", cfg);
				return;
			}
			var cg = new rec.CookieGetter(cfg, form);
			//var cc = await cg.getAccountCookie(mailText.Text, passText.Text);
			var cc = cg.getAccountCookie(mailText.Text, passText.Text);
			if (cc == null) {
				MessageBox.Show("login error", "", MessageBoxButtons.OK);
				return;
			}
			var TargetUrl = new Uri("https://live.nicovideo.jp/");
			if (cc.GetCookies(TargetUrl)["user_session"] == null &&
				                   cc.GetCookies(TargetUrl)["user_session_secure"] == null)
				MessageBox.Show("no login", "", MessageBoxButtons.OK);
			else {
				MessageBox.Show("login ok", "", MessageBoxButtons.OK);
				this.cc = cc;
			}
		}
	}
}
