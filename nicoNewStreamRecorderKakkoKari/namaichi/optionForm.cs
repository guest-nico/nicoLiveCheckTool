/*
 * Created by SharpDevelop.
 * User: pc
 * Date: 2018/05/06
 * Time: 20:47
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

using namaichi.config;
using SunokoLibrary.Application;
using SunokoLibrary.Windows.ViewModels;

namespace namaichi
{
	/// <summary>
	/// Description of optionForm.
	/// </summary>
	public partial class optionForm : Form
	{
		private config.config cfg;
		
		static readonly Uri TargetUrl = new Uri("http://live.nicovideo.jp/");
//		private string 
		
		public optionForm(config.config cfg)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			this.StartPosition = FormStartPosition.CenterParent;
			//util.debugWriteLine(p.X + " " + p.Y);
			InitializeComponent();
			//this.Location = p;
			this.cfg = cfg;
			
			nicoSessionComboBox1.Selector.PropertyChanged += Selector_PropertyChanged;
//			nicoSessionComboBox2.Selector.PropertyChanged += Selector2_PropertyChanged;
			setFormFromConfig();
		}
		
		void optionOk_Click(object sender, EventArgs e)
		{
			var formData = getFormData();
			cfg.saveFromForm(formData);
			
			//main cookie
			var importer = nicoSessionComboBox1.Selector.SelectedImporter;
			if (importer != null && importer.SourceInfo != null) {
				var si = importer.SourceInfo;
				
				if (isCookieFileSiteiChkBox.Checked)
					SourceInfoSerialize.save(si.GenerateCopy(si.BrowserName, si.ProfileName, cookieFileText.Text), false);
				else SourceInfoSerialize.save(si, false);
			}

			DialogResult = DialogResult.OK;
			Close();
		}

		private Dictionary<string, string> getFormData() {
			//var selectedImporter = nicoSessionComboBox1.Selector.SelectedImporter;
//			var browserName = (selectedImporter != null) ? selectedImporter.SourceInfo.BrowserName : "";
			var browserNum = (useCookieRadioBtn.Checked) ? "2" : "1";
//			var browserNum2 = (useCookieRadioBtn2.Checked) ? "2" : "1";
			return new Dictionary<string, string>(){
				{"accountId",mailText.Text},
				{"accountPass",passText.Text},
				//{"user_session",passText.Text},
				{"browserNum",browserNum},
//				{"isAllBrowserMode",checkBoxShowAll.Checked.ToString().ToLower()},
				{"issecondlogin",useSecondLoginChkBox.Checked.ToString().ToLower()},
				{"IsdefaultBrowserPath",isDefaultBrowserPathChkBox.Checked.ToString().ToLower()},
				{"browserPath",browserPathText.Text},
				{"appliAPath",appliAPathText.Text},
				{"appliBPath",appliBPathText.Text},
				{"appliCPath",appliCPathText.Text},
				{"appliDPath",appliDPathText.Text},
				{"appliEPath",appliEPathText.Text},
				{"appliFPath",appliFPathText.Text},
				{"appliGPath",appliGPathText.Text},
				{"appliHPath",appliHPathText.Text},
				{"appliIPath",appliIPathText.Text},
				{"appliJPath",appliJPathText.Text},
				{"appliAArgs",argAText.Text},
				{"appliBArgs",argBText.Text},
				{"appliCArgs",argCText.Text},
				{"appliDArgs",argDText.Text},
				{"appliEArgs",argEText.Text},
				{"appliFArgs",argFText.Text},
				{"appliGArgs",argGText.Text},
				{"appliHArgs",argHText.Text},
				{"appliIArgs",argIText.Text},
				{"appliJArgs",argJText.Text},
				{"appliAName",nameAText.Text},
				{"appliBName",nameBText.Text},
				{"appliCName",nameCText.Text},
				{"appliDName",nameDText.Text},
				{"appliEName",nameEText.Text},
				{"appliFName",nameFText.Text},
				{"appliGName",nameGText.Text},
				{"appliHName",nameHText.Text},
				{"appliIName",nameIText.Text},
				{"appliJName",nameJText.Text},
				
				{"IsStartTimeAllCheck",isStartTimeAllCheckChkBox.Checked.ToString().ToLower()},
				{"IscheckRecent",isRecentCheckRadioBtn.Checked.ToString().ToLower()},
				{"Ischeck30min",isCheck30minRadioBtn.Checked.ToString().ToLower()},
				{"IscheckOnAir",isCheckOnAirRadioBtn.Checked.ToString().ToLower()},
				{"IschangeIcon",isChangeIconChkBox.Checked.ToString().ToLower()},
				{"IstasktrayStart",isTasktrayStartChkBox.Checked.ToString().ToLower()},
				{"IsdragCom",isdragComChkBox.Checked.ToString().ToLower()},
				{"doublecmode",doublecmodeList.Text},
				
				{"rssUpdateInterval",rssUpdateIntervalList.Text},
				{"userNameUpdateInterval",userNameUpdateIntervalList.Text},
				{"log",isLogChkBtn.Checked.ToString().ToLower()},
				{"IsbroadLog",isBroadLogChkBox.Checked.ToString().ToLower()},
				{"poploc",poplocList.Text},
				{"poptime",poptimeList.Text},
				{"Isclosepopup",IsclosepopupChkBox.Checked.ToString().ToLower()},
				{"Isfixpopup",IsfixpopupChkBox.Checked.ToString().ToLower()},
				{"Issmallpopup",IssmallpopupChkBox.Checked.ToString().ToLower()},
				{"IsTopMostPopup",isTopMostPopupChkBox.Checked.ToString().ToLower()},
				{"mailFrom",mailFromText.Text},
				{"mailTo",mailToText.Text},
				{"mailSmtp",mailSmtpText.Text},
				{"mailPort",mailPortText.Text},
				{"mailUser",mailUserText.Text},
				{"mailPass",mailPassText.Text},
				{"IsmailSsl",isMailSslChkBox.Checked.ToString().ToLower()},
				{"soundPath",soundPathText.Text},
				{"IsSoundDefault",isDefaultSoundChkBtn.Checked.ToString().ToLower()},
				{"soundVolume",volumeBar.Value.ToString()},
				
				{"IsRss",isRssChkBox.Checked.ToString().ToLower()},
				{"IsPush",isPushChkBox.Checked.ToString().ToLower()},
				{"IsAppPush",isAppPushChkBox.Checked.ToString().ToLower()},
				
				{"thresholdpage",thresholdpageList.Value.ToString()},
				{"brodouble",brodoubleList.SelectedIndex.ToString()},
				{"liveListUpdateMinutes",liveListUpdateMinutesList.Value.ToString()},
				
				{"cookieFile",cookieFileText.Text},
				{"iscookie",isCookieFileSiteiChkBox.Checked.ToString().ToLower()},
				{"user_session",""},
				{"user_session_secure",""},
				
			};
			
		}
		
		async void Selector_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
			//if (isInitRun) initRec();
			
            switch(e.PropertyName)
            {
                case "SelectedIndex":
                    var cookieContainer = new CookieContainer();
                    var currentGetter = nicoSessionComboBox1.Selector.SelectedImporter;
                    if (currentGetter != null)
                    {
                        var result = await currentGetter.GetCookiesAsync(TargetUrl);
                        
                        var cookie = result.Status == CookieImportState.Success ? result.Cookies["user_session"] : null;
//                        foreach (var c in result.Cookies)
//                        	util.debugWriteLine(c);
                        //logText.Text += cookie.Name + cookie.Value+ cookie.Expires;
                        
                        //UI更新
//                        txtCookiePath.Text = currentGetter.SourceInfo.CookiePath;
//                        btnOpenCookieFileDialog.Enabled = true;
//                        txtUserSession.Text = cookie != null ? cookie.Value : null;
//                        txtUserSession.Enabled = result.Status == CookieImportState.Success;
                        //Properties.Settings.Default.SelectedSourceInfo = currentGetter.SourceInfo;
                        //Properties.Settings.Default.Save();
                        //cfg.set("browserNum", nicoSessionComboBox1.Selector.SelectedIndex.ToString());
                        //if (cookie != null) cfg.set("user_session", cookie.Value);
                        //cfg.set("isAllBrowserMode", nicoSessionComboBox1.Selector.IsAllBrowserMode.ToString());
                    }
                    else
                    {
//                        txtCookiePath.Text = null;
//                        txtUserSession.Text = null;
//                        txtUserSession.Enabled = false;
//                        btnOpenCookieFileDialog.Enabled = false;
                    }
                    break;
            }
        }
		
		void btnReload_Click(object sender, EventArgs e)
        { 
			//var si = nicoSessionComboBox1.Selector.SelectedImporter.SourceInfo;
			//util.debugWriteLine(si.EngineId + " " + si.BrowserName + " " + si.ProfileName);
//			var a = new SunokoLibrary.Application.Browsers.FirefoxImporterFactory();
//			foreach (var b in a.GetCookieImporters()) {
//				var c = b.GetCookiesAsync(TargetUrl);
//				c.ConfigureAwait(false);
				
//				util.debugWriteLine(c.Result.Cookies["user_session"]);
//			}
				
//			a.GetCookieImporter(new CookieSourceInfo("
			var tsk = nicoSessionComboBox1.Selector.UpdateAsync(); 
		}
		
        void btnOpenCookieFileDialog_Click(object sender, EventArgs e)
        { var tsk = nicoSessionComboBox1.ShowCookieDialogAsync(); }
        void checkBoxShowAll_CheckedChanged(object sender, EventArgs e)
        { nicoSessionComboBox1.Selector.IsAllBrowserMode = checkBoxShowAll.Checked;
//        	cfg.set("isAllBrowserMode", nicoSessionComboBox1.Selector.IsAllBrowserMode.ToString());
        }

        private void setFormFromConfig() {
        	mailText.Text = cfg.get("accountId");
        	passText.Text = cfg.get("accountPass");
        	
        	if (cfg.get("browserNum") == "1") useAccountLoginRadioBtn.Checked = true;
        	else useCookieRadioBtn.Checked = true; 
        	useSecondLoginChkBox.Checked = bool.Parse(cfg.get("issecondlogin"));
        	isDefaultBrowserPathChkBox.Checked = bool.Parse(cfg.get("IsdefaultBrowserPath"));
        	isDefaultBrowserPathChkBox_UpdateAction();
        	browserPathText.Text = cfg.get("browserPath");
			appliAPathText.Text = cfg.get("appliAPath");
			appliBPathText.Text = cfg.get("appliBPath");
			appliCPathText.Text = cfg.get("appliCPath");
			appliDPathText.Text = cfg.get("appliDPath");
			appliEPathText.Text = cfg.get("appliEPath");
			appliFPathText.Text = cfg.get("appliFPath");
			appliGPathText.Text = cfg.get("appliGPath");
			appliHPathText.Text = cfg.get("appliHPath");
			appliIPathText.Text = cfg.get("appliIPath");
			appliJPathText.Text = cfg.get("appliJPath");
			argAText.Text = cfg.get("appliAArgs");
			argBText.Text = cfg.get("appliBArgs");
			argCText.Text = cfg.get("appliCArgs");
			argDText.Text = cfg.get("appliDArgs");
			argEText.Text = cfg.get("appliEArgs");
			argFText.Text = cfg.get("appliFArgs");
			argGText.Text = cfg.get("appliGArgs");
			argHText.Text = cfg.get("appliHArgs");
			argIText.Text = cfg.get("appliIArgs");
			argJText.Text = cfg.get("appliJArgs");
			nameAText.Text = cfg.get("appliAName");
			nameBText.Text = cfg.get("appliBName");
			nameCText.Text = cfg.get("appliCName");
			nameDText.Text = cfg.get("appliDName");
			nameEText.Text = cfg.get("appliEName");
			nameFText.Text = cfg.get("appliFName");
			nameGText.Text = cfg.get("appliGName");
			nameHText.Text = cfg.get("appliHName");
			nameIText.Text = cfg.get("appliIName");
			nameJText.Text = cfg.get("appliJName");
			isStartTimeAllCheckChkBox.Checked = bool.Parse(cfg.get("IsStartTimeAllCheck"));
			
			isRecentCheckRadioBtn.Checked = bool.Parse(cfg.get("IscheckRecent"));
			isCheck30minRadioBtn.Checked = bool.Parse(cfg.get("Ischeck30min"));
			isCheckOnAirRadioBtn.Checked = bool.Parse(cfg.get("IscheckOnAir"));
			isChangeIconChkBox.Checked = bool.Parse(cfg.get("IschangeIcon"));
			isCheckRecentChkBox_Update();
			
			isTasktrayStartChkBox.Checked = bool.Parse(cfg.get("IstasktrayStart"));
			isdragComChkBox.Checked = bool.Parse(cfg.get("IsdragCom"));
			doublecmodeList.Text = cfg.get("doublecmode");
			
			rssUpdateIntervalList.Text = cfg.get("rssUpdateInterval");
			userNameUpdateIntervalList.Text = cfg.get("userNameUpdateInterval");
			isLogChkBtn.Checked = bool.Parse(cfg.get("log"));
			isBroadLogChkBox.Checked = bool.Parse(cfg.get("IsbroadLog"));
//			setConvertList(int.Parse(cfg.get("afterConvertMode")));
			poplocList.Text = cfg.get("poploc");
			poptimeList.Text = cfg.get("poptime");
			IsclosepopupChkBox.Checked = bool.Parse(cfg.get("Isclosepopup"));
			IsfixpopupChkBox.Checked = bool.Parse(cfg.get("Isfixpopup"));
			IssmallpopupChkBox.Checked = bool.Parse(cfg.get("Issmallpopup"));
			isTopMostPopupChkBox.Checked = bool.Parse(cfg.get("IsTopMostPopup"));
			mailFromText.Text = cfg.get("mailFrom");
			mailToText.Text = cfg.get("mailTo");
			mailSmtpText.Text = cfg.get("mailSmtp");
			mailPortText.Text = cfg.get("mailPort");
			mailUserText.Text = cfg.get("mailUser");
			mailPassText.Text = cfg.get("mailPass");
			isMailSslChkBox.Checked = bool.Parse(cfg.get("IsmailSsl"));
			soundPathText.Text = cfg.get("soundPath");
			isDefaultSoundChkBtn.Checked = bool.Parse(cfg.get("IsSoundDefault"));
			volumeBar.Value = int.Parse(cfg.get("soundVolume"));
			
			isRssChkBox.Checked = bool.Parse(cfg.get("IsRss"));
			isPushChkBox.Checked = bool.Parse(cfg.get("IsPush"));
			isAppPushChkBox.Checked = bool.Parse(cfg.get("IsAppPush"));
			
			thresholdpageList.Value = int.Parse(cfg.get("thresholdpage"));
			brodoubleList.SelectedIndex = int.Parse(cfg.get("brodouble"));
			liveListUpdateMinutesList.Value = decimal.Parse(cfg.get("liveListUpdateMinutes"));
			
			
        	isCookieFileSiteiChkBox.Checked = bool.Parse(cfg.get("iscookie"));
        	isCookieFileSiteiChkBox_UpdateAction();
        	cookieFileText.Text = cfg.get("cookieFile");
	
        	var si = SourceInfoSerialize.load(false);
        	nicoSessionComboBox1.Selector.SetInfoAsync(si);
        }
        
		void optionCancel_Click(object sender, EventArgs e)
		{
			Close();
		}
		
		void cookieFileSiteiSanshouBtn_Click(object sender, EventArgs e)
		{
			var dialog = new OpenFileDialog();
			dialog.Multiselect = false;
			var result = dialog.ShowDialog();
			if (result != DialogResult.OK) return;
			
			cookieFileText.Text = dialog.FileName;
		}
		
		void isCookieFileSiteiChkBox_CheckedChanged(object sender, EventArgs e)
		{
			isCookieFileSiteiChkBox_UpdateAction();
		}
		
		void isCookieFileSiteiChkBox_UpdateAction() {
//			cookieFileText.Enabled = isCookieFileSiteiChkBox.Checked;
//			cookieFileSanshouBtn.Enabled = isCookieFileSiteiChkBox.Checked;
		}
		
		async void loginBtn_Click(object sender, EventArgs e)
		{
			
			var cg = new rec.CookieGetter(cfg);
			var cc = await cg.getAccountCookie(mailText.Text, passText.Text);
			if (cc == null) {
				MessageBox.Show("login error", "", MessageBoxButtons.OK);
				return;
			}
			if (cc.GetCookies(TargetUrl)["user_session"] == null &&
				                   cc.GetCookies(TargetUrl)["user_session_secure"] == null)
				MessageBox.Show("no login", "", MessageBoxButtons.OK);
			else MessageBox.Show("login ok", "", MessageBoxButtons.OK);
			
			//MessageBox.Show("aa");
		}
		
		void isDefaultBrowserPathChkBox_CheckedChanged(object sender, EventArgs e)
		{
			isDefaultBrowserPathChkBox_UpdateAction();
		}
		void isDefaultBrowserPathChkBox_UpdateAction()
		{
			browserPathText.Enabled = !isDefaultBrowserPathChkBox.Checked;
			browserPathSanshouBtn.Enabled = !isDefaultBrowserPathChkBox.Checked;
		}
		void browserPathSanshouBtn_Click(object sender, EventArgs e)
		{
			var dialog = new OpenFileDialog();
			dialog.Multiselect = false;
			var result = dialog.ShowDialog();
			if (result != DialogResult.OK) return;
			
			browserPathText.Text = dialog.FileName;
		}
		void appliPathSanshouBtn_Click(object sender, EventArgs e)
		{
			var n = ((Button)sender).Name[5];
			var t = this.Controls.Find("appli" + n + "PathText", true)[0];
			
			appliPathSanshouBtnClickProcess((TextBox)t);
		}
		void appliPathSanshouBtnClickProcess(TextBox tb) {
			var dialog = new OpenFileDialog();
			dialog.Multiselect = false;
			var result = dialog.ShowDialog();
			if (result != DialogResult.OK) return;
			
			tb.Text = dialog.FileName;
		}
		private void isCheckRecentChkBox_Update() {
			isCheck30minRadioBtn.Enabled = isRecentCheckRadioBtn.Checked;
			isCheckOnAirRadioBtn.Enabled = isRecentCheckRadioBtn.Checked;
			isChangeIconChkBox.Enabled = isRecentCheckRadioBtn.Checked;
		}
		void IsRecentChkBoxCheckedChanged(object sender, EventArgs e)
		{
			isCheckRecentChkBox_Update();
		}
		void IsDefaultSoundChkBtnCheckedChanged(object sender, EventArgs e)
		{
			updateIsSoundEndChkBox();
		}
		void SoundSanshouBtnClick(object sender, EventArgs e)
		{
			var f = new OpenFileDialog();
			f.DefaultExt = ".wav";
			f.FileName = "";
			f.Filter = "Wav形式(*.wav)|*.wav*";
			f.Multiselect = false;
			var a = f.ShowDialog();
			if (a == DialogResult.OK) {
				soundPathText.Text = f.FileName;
			}
		}
		void updateIsSoundEndChkBox() {
			soundPathText.Enabled = !isDefaultSoundChkBtn.Checked;
			soundSanshouBtn.Enabled = !isDefaultSoundChkBtn.Checked;
		}
		void VolumeBarValueChanged(object sender, EventArgs e)
		{
			util.debugWriteLine(volumeBar.Value);
			volumeText.Text = "音量：" + volumeBar.Value;
		}
		
		void SendTestMailBtnClick(object sender, EventArgs e)
		{
			if (mailFromText.Text == "") {
				MessageBox.Show("メール送信失敗：パラメータ 'from' を空の文字列にすることはできません。\nパラメータ名: from");
				return;
			}
			if (mailToText.Text == "") {
				MessageBox.Show("メール送信失敗：パラメータ 'to' を空の文字列にすることはできません。\nパラメータ名: to");
				return;
			}
			try {
				var s = new System.Net.Mail.MailAddress(mailFromText.Text);
				s = new System.Net.Mail.MailAddress(mailToText.Text);
			} catch (Exception ee) {
				MessageBox.Show("メール送信失敗：指定された文字列は、電子メールアドレスに必要な形式ではありません。");
				return;
			}
			if (mailSmtpText.Text == "") {
				MessageBox.Show("メール送信失敗：このプロパティに空の文字列を指定することはできません。\nパラメータ名: value");
				return;
			}
			int _p;
			if (!int.TryParse(mailPortText.Text, out _p)) {
				MessageBox.Show("メール送信失敗：入力文字列の形式が正しくありません。");
				return;
			}
			
			
			var title = "[ニコ生]ユーザー名の放送開始";
			var msg = DateTime.Now.ToString() + "\nユーザー名 が コミュニティ名 で 放送タイトル を開始しました。\nhttp://live.nicovideo.jp/watch/lv********\nhttp://com.nicovideo.jp/community/co*******";
			
			Task.Run(() => {
				string eMsg;
				var ret = util.sendMail(mailFromText.Text, mailToText.Text, 
						title, msg, mailSmtpText.Text, mailPortText.Text, 
						mailUserText.Text, mailPassText.Text, 
						isMailSslChkBox.Checked, out eMsg);
				var msgTitle = (ret) ? "" : "";
				//var msgText = (ret) ? "メール送信成功" : "メール送信失敗：メールを送信できませんでした。";
				var msgText = (ret) ? "メール送信成功" : ("メール送信失敗：" + eMsg);
				
				this.Invoke((MethodInvoker)delegate() {
					MessageBox.Show(msgText, msgTitle);
				});
			});
		}
	}
}
