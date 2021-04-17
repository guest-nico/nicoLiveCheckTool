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
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using namaichi.config;
using System.IO;
using SunokoLibrary.Application;
using SunokoLibrary.Windows.ViewModels;

//using NAudio.Midi;



namespace namaichi
{
	/// <summary>
	/// Description of optionForm.
	/// </summary>
	public partial class optionForm : Form
	{
		private config.config cfg;
		
		static readonly Uri TargetUrl = new Uri("https://live.nicovideo.jp/");
		private MainForm form; 
		
		public optionForm(config.config cfg, MainForm form)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			this.StartPosition = FormStartPosition.CenterParent;
			//util.debugWriteLine(p.X + " " + p.Y);
			InitializeComponent();
			//this.Location = p;
			this.cfg = cfg;
			this.form = form;
			
			nicoSessionComboBox1.Selector.PropertyChanged += Selector_PropertyChanged;
//			nicoSessionComboBox2.Selector.PropertyChanged += Selector2_PropertyChanged;
			//setFormFromConfig();
			setBackColor(Color.FromArgb(int.Parse(cfg.get("alartBackColor"))));
			setForeColor(Color.FromArgb(int.Parse(cfg.get("alartForeColor"))));
			
			util.setFontSize(int.Parse(cfg.get("fontSize")), this, false);
		}
		
		void optionOk_Click(object sender, EventArgs e)
		{
			var formData = getFormData();
			cfg.saveFromForm(formData);
			
			setStartUpMenu2(!bool.Parse(cfg.get("IsStartUp")));
				
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
				{"IsAlartListRecentColor",isAlartListColorRecent.Checked.ToString().ToLower()},
				{"recentColor",ColorTranslator.ToHtml(recentSampleColorText.BackColor)},
				{"IsFollowerOnlyOtherColor",isFollowerOnlyOtherColor.Checked.ToString().ToLower()},
				{"followerOnlyColor",ColorTranslator.ToHtml(followerOnlySampleColorText.BackColor)},
				
				{"IstasktrayStart",isTasktrayStartChkBox.Checked.ToString().ToLower()},
				{"IsdragCom",isdragComChkBox.Checked.ToString().ToLower()},
				{"doublecmode",doublecmodeList.Text},
				{"IsNotAllMatchNotifyNoRecent",isNotAllMatchNotifyNoRecentChkBox.Checked.ToString().ToLower()},
				{"delThumb",delThumbChkBox.Checked.ToString().ToLower()},
				{"IsConfirmFollow",IsConfirmFollowChkBox.Checked.ToString().ToLower()},
				{"alartCacheIcon",alartCacheIconChkBox.Checked.ToString().ToLower()},
				{"IsAddAlartedComUser",IsAddAlartedComUserChkBox.Checked.ToString().ToLower()},
				{"IsAddAlartedUserToUserList",isAddAlartedUserToUserListChkBox.Checked.ToString().ToLower()},
				
				{"rssUpdateInterval",rssUpdateIntervalList.Text},
				{"userNameUpdateInterval",userNameUpdateIntervalList.Text},
				{"log",isLogChkBtn.Checked.ToString().ToLower()},
				{"IsbroadLog",isBroadLogChkBox.Checked.ToString().ToLower()},
				{"IsLogFile",isLogFileChkBox.Checked.ToString().ToLower()},
				{"maxHistoryDisplay",maxHistoryDisplayList.Text},
				{"maxNotAlartDisplay",maxNotAlartDisplayList.Text},
				{"maxLogDisplay",maxLogDisplayList.Text},
				{"IsStartUp",isStartUpChkBox.Checked.ToString().ToLower()},
				{"IsAllowMultiProcess",isAllowMultiProcessChkBox.Checked.ToString().ToLower()},
				{"poploc",poplocList.Text},
				{"poptime",poptimeList.Text},
				{"Isclosepopup",IsclosepopupChkBox.Checked.ToString().ToLower()},
				{"Isfixpopup",IsfixpopupChkBox.Checked.ToString().ToLower()},
				{"Issmallpopup",IssmallpopupChkBox.Checked.ToString().ToLower()},
				{"IsTopMostPopup",isTopMostPopupChkBox.Checked.ToString().ToLower()},
				{"IsColorPopup",isColorPopupChkBox.Checked.ToString().ToLower()},
				{"popupOpacity",popupOpacityList.Text},
				{"mailFrom",mailFromText.Text},
				{"mailTo",mailToText.Text},
				{"mailSmtp",mailSmtpText.Text},
				{"mailPort",mailPortText.Text},
				{"mailUser",mailUserText.Text},
				{"mailPass",mailPassText.Text},
				{"IsmailSsl",isMailSslChkBox.Checked.ToString().ToLower()},
				{"soundPathA",soundPathAText.Text},
				{"soundPathB",soundPathBText.Text},
				{"soundPathC",soundPathCText.Text},
				//{"IsSoundDefault",isDefaultSoundChkBtn.Checked.ToString().ToLower()},
				{"soundVolume",volumeBar.Value.ToString()},
				{"soundAVolume",volumeABar.Value.ToString()},
				{"soundBVolume",volumeBBar.Value.ToString()},
				{"soundCVolume",volumeCBar.Value.ToString()},
				{"onlyIconColor", ColorTranslator.ToHtml(onlyIconColorBtn.BackColor)},
				{"defaultBehavior", getDefaultBehavior()},
				{"defaultTextColor", ColorTranslator.ToHtml(textColorBtn.BackColor)},
				{"defaultBackColor", ColorTranslator.ToHtml(backColorBtn.BackColor)},
				{"defaultSound",defaultSoundList.SelectedIndex.ToString()},
				{"IsDefaultSoundId",isDefaultSoundIdChkBox.Checked.ToString().ToLower()},
				{"IsDefaultAutoReserve",isDefaultAutoReserveChkBox.Checked.ToString().ToLower()},
				
				{"IsRss",isRssChkBox.Checked.ToString().ToLower()},
				{"IsPush",isPushChkBox.Checked.ToString().ToLower()},
				{"IsAppPush",isAppPushChkBox.Checked.ToString().ToLower()},
				{"IsTimeTable",isTimeTableChkBox.Checked.ToString().ToLower()},
				{"IsAutoReserve",isAutoReserveChkBox.Checked.ToString().ToLower()},
				
				{"thresholdpage",thresholdpageList.Value.ToString()},
				{"brodouble",brodoubleList.SelectedIndex.ToString()},
				{"alartAddLive",alartAddLiveBox.SelectedIndex.ToString()},
				{"liveListUpdateMinutes",liveListUpdateMinutesList.Value.ToString()},
				{"liveListCacheIcon",liveListCacheIconChkBox.Checked.ToString().ToLower()},
				{"liveListGetIcon",liveListGetIconChkBox.Checked.ToString().ToLower()},
				
				{"evenRowsColor", ColorTranslator.ToHtml(evenRowsColorBtn.BackColor)},
				{"fontSize",fontList.Value.ToString()},
				
				{"cookieFile",cookieFileText.Text},
				{"iscookie",isCookieFileSiteiChkBox.Checked.ToString().ToLower()},
				{"IsBrowserShowAll",checkBoxShowAll.Checked.ToString().ToLower()},
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
		/*
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
			//var tsk = nicoSessionComboBox1.Selector.UpdateAsync(); 
		}
		*/
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
			//var a = ColorTranslator.FromHtml(cfg.get("recentColor"));
			recentColorBtn.BackColor = recentSampleColorText.BackColor = ColorTranslator.FromHtml(cfg.get("recentColor"));
			isAlartListColorRecent.Checked = bool.Parse(cfg.get("IsAlartListRecentColor"));
			//a = ColorTranslator.FromHtml(cfg.get("followerOnlyColor"));
			followerOnlyColorBtn.BackColor = followerOnlySampleColorText.BackColor = ColorTranslator.FromHtml(cfg.get("followerOnlyColor"));
			isFollowerOnlyOtherColor.Checked = bool.Parse(cfg.get("IsFollowerOnlyOtherColor"));
			isFollowerOnlyOtherColor.Enabled = isCheckOnAirRadioBtn.Checked;
			IsFollowerOnlyOtherColorChecked_update();
			
			isTasktrayStartChkBox.Checked = bool.Parse(cfg.get("IstasktrayStart"));
			isdragComChkBox.Checked = bool.Parse(cfg.get("IsdragCom"));
			doublecmodeList.Text = cfg.get("doublecmode");
			isNotAllMatchNotifyNoRecentChkBox.Checked = bool.Parse(cfg.get("IsNotAllMatchNotifyNoRecent"));
			delThumbChkBox.Checked = bool.Parse(cfg.get("delThumb"));
			IsConfirmFollowChkBox.Checked = bool.Parse(cfg.get("IsConfirmFollow"));
			alartCacheIconChkBox.Checked = bool.Parse(cfg.get("alartCacheIcon"));
			IsAddAlartedComUserChkBox.Checked = bool.Parse(cfg.get("IsAddAlartedComUser"));
			isAddAlartedUserToUserListChkBox.Checked = bool.Parse(cfg.get("IsAddAlartedUserToUserList"));
			
			
			rssUpdateIntervalList.Text = cfg.get("rssUpdateInterval");
			userNameUpdateIntervalList.Text = cfg.get("userNameUpdateInterval");
			isLogChkBtn.Checked = bool.Parse(cfg.get("log"));
			isBroadLogChkBox.Checked = bool.Parse(cfg.get("IsbroadLog"));
			isLogFileChkBox.Checked = bool.Parse(cfg.get("IsLogFile"));
			maxHistoryDisplayList.Text = cfg.get("maxHistoryDisplay");
			maxNotAlartDisplayList.Text = cfg.get("maxNotAlartDisplay");
			maxLogDisplayList.Text = cfg.get("maxLogDisplay");
			isStartUpChkBox.Checked = bool.Parse(cfg.get("IsStartUp"));
			isAllowMultiProcessChkBox.Checked = bool.Parse(cfg.get("IsAllowMultiProcess"));
			
			poplocList.Text = cfg.get("poploc");
			poptimeList.Text = cfg.get("poptime");
			IsclosepopupChkBox.Checked = bool.Parse(cfg.get("Isclosepopup"));
			IsfixpopupChkBox.Checked = bool.Parse(cfg.get("Isfixpopup"));
			IssmallpopupChkBox.Checked = bool.Parse(cfg.get("Issmallpopup"));
			isTopMostPopupChkBox.Checked = bool.Parse(cfg.get("IsTopMostPopup"));
			isColorPopupChkBox.Checked = bool.Parse(cfg.get("IsColorPopup"));
			popupOpacityList.Text = cfg.get("popupOpacity");
			mailFromText.Text = cfg.get("mailFrom");
			mailToText.Text = cfg.get("mailTo");
			mailSmtpText.Text = cfg.get("mailSmtp");
			mailPortText.Text = cfg.get("mailPort");
			mailUserText.Text = cfg.get("mailUser");
			mailPassText.Text = cfg.get("mailPass");
			isMailSslChkBox.Checked = bool.Parse(cfg.get("IsmailSsl"));
			soundPathAText.Text = cfg.get("soundPathA");
			soundPathBText.Text = cfg.get("soundPathB");
			soundPathCText.Text = cfg.get("soundPathC");
			//isDefaultSoundChkBtn.Checked = bool.Parse(cfg.get("IsSoundDefault"));
			volumeBar.Value = int.Parse(cfg.get("soundVolume"));
			volumeABar.Value = int.Parse(cfg.get("soundAVolume"));
			volumeBBar.Value = int.Parse(cfg.get("soundBVolume"));
			volumeCBar.Value = int.Parse(cfg.get("soundCVolume"));
			onlyIconColorBtn.BackColor = ColorTranslator.FromHtml(cfg.get("onlyIconColor"));
			setOnlyIconColor(onlyIconColorBtn.BackColor);
			setDefaultBehavior(cfg.get("defaultBehavior"));
			textColorBtn.BackColor = sampleColorText.ForeColor = ColorTranslator.FromHtml(cfg.get("defaultTextColor"));
			backColorBtn.BackColor = sampleColorText.BackColor = ColorTranslator.FromHtml(cfg.get("defaultBackColor"));
			defaultSoundList.SelectedIndex = int.Parse(cfg.get("defaultSound"));
			isDefaultSoundIdChkBox.Checked = bool.Parse(cfg.get("IsDefaultSoundId"));
			isDefaultAutoReserveChkBox.Checked = bool.Parse(cfg.get("IsDefaultAutoReserve"));
			
			isRssChkBox.Checked = bool.Parse(cfg.get("IsRss"));
			isPushChkBox.Checked = bool.Parse(cfg.get("IsPush"));
			isAppPushChkBox.Checked = bool.Parse(cfg.get("IsAppPush"));
			isTimeTableChkBox.Checked = bool.Parse(cfg.get("IsTimeTable"));
			isAutoReserveChkBox.Checked = bool.Parse(cfg.get("IsAutoReserve"));
			
			thresholdpageList.Value = int.Parse(cfg.get("thresholdpage"));
			brodoubleList.SelectedIndex = int.Parse(cfg.get("brodouble"));
			alartAddLiveBox.SelectedIndex = int.Parse(cfg.get("alartAddLive"));
			
			liveListUpdateMinutesList.Value = decimal.Parse(cfg.get("liveListUpdateMinutes"));
			liveListCacheIconChkBox.Checked = bool.Parse(cfg.get("liveListCacheIcon"));
			liveListGetIconChkBox.Checked = bool.Parse(cfg.get("liveListGetIcon"));
			
        	isCookieFileSiteiChkBox.Checked = bool.Parse(cfg.get("iscookie"));
        	isCookieFileSiteiChkBox_UpdateAction();
        	cookieFileText.Text = cfg.get("cookieFile");
        	checkBoxShowAll.Checked = bool.Parse(cfg.get("IsBrowserShowAll"));
	
        	evenRowsColorBtn.BackColor = ColorTranslator.FromHtml(cfg.get("evenRowsColor"));
        	fontList.Value = decimal.Parse(cfg.get("fontSize"));
        	
        	var si = SourceInfoSerialize.load(false);
        	if (si != null)
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
			
			var cg = new rec.CookieGetter(cfg, form);
			//var cc = await cg.getAccountCookie(mailText.Text, passText.Text);
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
			recentColorBtn.Enabled = recentSampleColorText.Enabled =
					defaultRecentColorBtn.Enabled = 
					isAlartListColorRecent.Enabled =
						isRecentCheckRadioBtn.Checked;
		}
		void IsRecentChkBoxCheckedChanged(object sender, EventArgs e)
		{
			isCheckRecentChkBox_Update();
		}
		/*
		void IsDefaultSoundChkBtnCheckedChanged(object sender, EventArgs e)
		{
			updateIsSoundEndChkBox();
		}
		*/
		void SoundSanshouBtnClick(object sender, EventArgs e)
		{
			var f = new OpenFileDialog();
			f.DefaultExt = ".wav";
			f.FileName = "";
			f.Filter = "Wav形式(*.wav)|*.wav*";
			f.Multiselect = false;
			var a = f.ShowDialog();
			if (a == DialogResult.OK) {
				if (sender == soundASanshouBtn) 
					soundPathAText.Text = f.FileName;
				if (sender == soundBSanshouBtn) 
					soundPathBText.Text = f.FileName;
				if (sender == soundCSanshouBtn) 
					soundPathCText.Text = f.FileName;
			}
		}
		/*
		void updateIsSoundEndChkBox() {
			soundPathAText.Enabled = !isDefaultSoundChkBtn.Checked;
			soundASanshouBtn.Enabled = !isDefaultSoundChkBtn.Checked;
		}
		*/
		void VolumeBarValueChanged(object sender, EventArgs e)
		{
			util.debugWriteLine(volumeBar.Value);
			volumeText.Text = "デフォルト音量：" + volumeBar.Value;
		}
		void VolumeABarValueChanged(object sender, EventArgs e)
		{
			util.debugWriteLine(volumeABar.Value);
			volumeAText.Text = "音量：" + volumeABar.Value;
		}
		void VolumeBBarValueChanged(object sender, EventArgs e)
		{
			util.debugWriteLine(volumeBBar.Value);
			volumeBText.Text = "音量：" + volumeBBar.Value;
		}
		void VolumeCBarValueChanged(object sender, EventArgs e)
		{
			util.debugWriteLine(volumeCBar.Value);
			volumeCText.Text = "音量：" + volumeCBar.Value;
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
			} catch (Exception) {
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
			var msg = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\nユーザー名 が コミュニティ名 で 放送タイトル を開始しました。\nhttps://live.nicovideo.jp/watch/lv********\nhttps://com.nicovideo.jp/community/co*******";
			
			Task.Factory.StartNew(() => {
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
		private string getDefaultBehavior() {
			var l = new List<string>();
			for (var i = 0; i < defaultBehaviorGroupBox.Controls.Count; i++) {
				if (typeof(CheckBox) != defaultBehaviorGroupBox.Controls[i].GetType()) 
					continue;
				CheckBox chkbox = (CheckBox)defaultBehaviorGroupBox.Controls[i];
				l.Add(chkbox.Checked ? "1" : "0");
				if (l.Count == 15) break;
			}
			return string.Join(",", l);
		}
		private void setDefaultBehavior(string conf) {
			var l = conf.Split(',');
			for (var i = 0; i < l.Length; i++) {
				if (typeof(CheckBox) != defaultBehaviorGroupBox.Controls[i].GetType()) 
					continue;
				CheckBox chkbox = (CheckBox)defaultBehaviorGroupBox.Controls[i];
				chkbox.Checked = l[i] == "1";
			}
		}
		void TextColorBtnClick(object sender, EventArgs e)
		{
			var c = new ColorDialog();
			c.FullOpen = true;
			c.Color = textColorBtn.BackColor;
			if (c.ShowDialog() != DialogResult.OK) return;
			textColorBtn.BackColor = c.Color;
			sampleColorText.ForeColor = c.Color;
		}
		void BackColorBtnClick(object sender, EventArgs e)
		{
			var c = new ColorDialog();
			c.FullOpen = true;
			c.Color = backColorBtn.BackColor;
			if (c.ShowDialog() != DialogResult.OK) return;
			backColorBtn.BackColor = c.Color;
			sampleColorText.BackColor = c.Color;
		}
		void DefaultColorBtnClick(object sender, EventArgs e)
		{
			backColorBtn.BackColor = Color.FromArgb(255,224,255);
			sampleColorText.BackColor = Color.FromArgb(255,224,255);
			textColorBtn.BackColor = Color.Black;
			sampleColorText.ForeColor = Color.Black;
		}
		void TestPopupBtnClick(object sender, EventArgs e)
		{
			var color = isColorPopupChkBox.Checked ? new Color[]{backColorBtn.BackColor, textColorBtn.BackColor} : null;
			var pm = new namaichi.alart.PopupDisplay(form);
			pm.showTest(poplocList.Text, int.Parse(poptimeList.Text),
					IsclosepopupChkBox.Checked, IssmallpopupChkBox.Checked,
					isTopMostPopupChkBox.Checked, color,
					double.Parse(popupOpacityList.Text));
		}
		void SoundTestBtnClick(object sender, EventArgs e)
		{
			try {
				var name = ((System.Windows.Forms.Button)sender).Name;
				string path = null;
				float volume = -1;
				if (name == "soundTestBtn") {
					path = util.getJarPath()[0] + "/Sound/se_soc01.wav";
					volume = (float)volumeBar.Value;
				} else if (name == "soundATestBtn") {
					path = soundPathAText.Text;
					volume = (float)volumeABar.Value;
				} else if (name == "soundBTestBtn") {
					path = soundPathBText.Text;
					volume = (float)volumeBBar.Value;
				} else if (name == "soundCTestBtn") {
					path = soundPathCText.Text;
					volume = (float)volumeCBar.Value;
				}
				if (!File.Exists(path)) {
					MessageBox.Show("ファイルが見つかりませんでした");
					return;
				}
				util.playSoundCore(volume / 100, path, form);
			} catch (Exception ee ) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		void RecentColorBtnClick(object sender, EventArgs e)
		{
			var c = new ColorDialog();
			c.FullOpen = true;
			c.Color = recentColorBtn.BackColor;
			if (c.ShowDialog() != DialogResult.OK) return;
			recentColorBtn.BackColor = c.Color;
			recentSampleColorText.BackColor = c.Color;
		}
		void DefaultRecentColorBtnClick(object sender, EventArgs e)
		{
			recentColorBtn.BackColor = Color.FromArgb(255,224,255);
			recentSampleColorText.BackColor = Color.FromArgb(255,224,255);
		}
		
		void IsAlartListColorRecentCheckedChanged(object sender, EventArgs e)
		{
			recentColorBtn.Enabled = recentSampleColorText.Enabled = !isAlartListColorRecent.Checked;
		}
		void setStartUpMenu(bool isDelete) {
			try {
				var exe = Application.ExecutablePath;
				
				var dir = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
				util.debugWriteLine(dir);
				
				var files = Directory.GetFiles(dir);
				foreach (var f in files) {
					util.debugWriteLine("f " + f);
					if (!f.EndsWith(".lnk")) continue;
					
					
					IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShell();
		            IWshRuntimeLibrary.IWshShortcut shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(f);
		            string targetPath = shortcut.TargetPath.ToString();
		            
		            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(shortcut);
		            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(shell);
			    
		            if (Path.GetFullPath(targetPath) == Path.GetFullPath(exe)) {
		            	util.debugWriteLine("exist " + targetPath);
		            	if (isDelete) File.Delete(f);
		            	else return;
		            }
				}
				util.debugWriteLine(files.Length);
				if (isDelete) return;
				
			    IWshRuntimeLibrary.WshShell shell2 = new IWshRuntimeLibrary.WshShell();
			    IWshRuntimeLibrary.IWshShortcut shortcut2 = (IWshRuntimeLibrary.IWshShortcut)shell2.CreateShortcut(dir + "/放送チェックツール（仮.lnk");
			    shortcut2.TargetPath = exe;
			    shortcut2.WorkingDirectory = util.getJarPath()[0];
			    shortcut2.WindowStyle = 1;
			    shortcut2.Arguments = "-shortcut";
			    //shortcut.IconLocation = Application.ExecutablePath + ",0";
			    shortcut2.Save();
			 
			    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(shortcut2);
			    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(shell2);
			    
				
				
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
		}
		void setStartUpMenu2(bool isDelete) {
			try {
				var exe = Application.ExecutablePath;
				//var exeName = Path.GetFileNameWithoutExtension(exe);
				var exeName = "放送チェックツール（仮";
				
				var dir = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
				util.debugWriteLine(dir);
				
				var files = Directory.GetFiles(dir);
				foreach (var f in files) {
					util.debugWriteLine("f " + f);
					var fName = Path.GetFileNameWithoutExtension(f);
					if (!f.EndsWith(".lnk") && !f.EndsWith(".bat")) continue;
					
					var n = exeName;
					if (fName != exeName) continue;
					
					if (File.Exists(f))
						File.Delete(f);
				}
				util.debugWriteLine(files.Length);
				if (isDelete) return;
				
				using (var sw = new StreamWriter(dir + "/" + exeName + ".bat", false, Encoding.GetEncoding("shift_jis"))) {
					sw.WriteLine("start \"\" \"" + exe + "\"");
				}
				
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
		}
		
		void CheckBoxShowAllCheckedChanged(object sender, EventArgs e)
		{
			nicoSessionComboBox1.Selector.IsAllBrowserMode = checkBoxShowAll.Checked;
		}
		
		void IsCookieFileSiteiChkBoxCheckedChanged(object sender, EventArgs e)
		{
			isCookieFileSiteiChkBox_UpdateAction();
		}
		
		void CookieFileSanshouBtnClick(object sender, EventArgs e)
		{
			var dialog = new OpenFileDialog();
			dialog.Multiselect = false;
			var result = dialog.ShowDialog();
			if (result != DialogResult.OK) return;
			
			cookieFileText.Text = dialog.FileName;
		}
		
		void BtnReloadClick(object sender, EventArgs e)
		{
			var tsk = nicoSessionComboBox1.Selector.UpdateAsync();
		}
		
		void IsCheckOnAirRadioBtnCheckedChanged(object sender, EventArgs e)
		{
			isFollowerOnlyOtherColor.Enabled = isCheckOnAirRadioBtn.Checked;
			IsFollowerOnlyOtherColorChecked_update();
		}
		void IsFollowerOnlyOtherColorCheckedChanged(object sender, EventArgs e)
		{
			IsFollowerOnlyOtherColorChecked_update();
		}
		void IsFollowerOnlyOtherColorChecked_update()
		{
			var isEnabled = 
				isCheckOnAirRadioBtn.Checked && isFollowerOnlyOtherColor.Checked;
			followerOnlyColorBtn.Enabled = isEnabled;
			followerOnlySampleColorText.Enabled = isEnabled;
			defaultFollowerOnlyColorBtn.Enabled = isEnabled;
		}
		
		void FollowerOnlyColorBtnClick(object sender, EventArgs e)
		{
			var c = new ColorDialog();
			c.FullOpen = true;
			c.Color = followerOnlyColorBtn.BackColor;
			if (c.ShowDialog() != DialogResult.OK) return;
			followerOnlyColorBtn.BackColor = c.Color;
			followerOnlySampleColorText.BackColor = c.Color;
		}
		
		void DefaultFollowerOnlyColorBtnClick(object sender, EventArgs e)
		{
			followerOnlyColorBtn.BackColor = Color.FromArgb(255,224,255);
			followerOnlySampleColorText.BackColor = Color.FromArgb(255,224,255);
		}
		
		void OptionFormLoad(object sender, EventArgs e)
		{
			setFormFromConfig();
			this.BtnReloadClick(null, null);
		}
		private void setBackColor(Color color) {
			BackColor = color;
			var c = getChildControls(this);
			foreach (var _c in c)
				if (//_c.GetType() == typeof(GroupBox) ||
				    _c.GetType() == typeof(System.Windows.Forms.Panel) || 
				    _c.GetType() == typeof(System.Windows.Forms.Form) 
				   	//_c.GetType() == typeof(System.Windows.Forms.TabPage) ||
				   //	_c.GetType() == typeof(System.Windows.Forms.TabControl)
				   )
						_c.BackColor = color;
		}
		private void setForeColor(Color color) {
			var c = getChildControls(this);
			foreach (var _c in c)
				if (//_c.GetType() == typeof(GroupBox) ||
				    _c.GetType() == typeof(Label) ||
				    _c.GetType() == typeof(CheckBox) ||
				   	_c.GetType() == typeof(RadioButton)) _c.ForeColor = color;
			
		}
		private List<Control> getChildControls(Control c) {
			util.debugWriteLine("cname " + c.Name);
			var ret = new List<Control>();
			foreach (Control _c in c.Controls) {
				ret.Add(_c);
				if (_c.GetType() != typeof(GroupBox)) {
					var children = getChildControls(_c);
					ret.AddRange(children);
				   }
				//util.debugWriteLine(c.Name + " " + children.Count);
			}
			util.debugWriteLine(c.Name + " " + ret.Count);
			return ret;
		}
		void ApplyBtnClick(object sender, EventArgs e)
		{
			util.setFontSize((int)fontList.Value, this, false);
		}
		void IsAddAlartedComUserChkBoxCheckedChanged(object sender, EventArgs e)
		{
			isAddAlartedUserToUserListChkBox.Enabled = IsAddAlartedComUserChkBox.Checked;
		}
		private void setOnlyIconColor(Color c) {
			var icon = new Icon(util.getJarPath()[0] + "/Icon/lock.ico");
			icon = util.changeIconColor(icon, c);
			onlyIconSampleBox.Image = icon.ToBitmap();
		}
		void OnlyIconColorBtnClick(object sender, EventArgs e)
		{
			var c = new ColorDialog();
			c.FullOpen = true;
			c.Color = onlyIconColorBtn.BackColor;
			if (c.ShowDialog() != DialogResult.OK) return;
			onlyIconColorBtn.BackColor = c.Color;
			setOnlyIconColor(c.Color);
		}
		
		void EvenRowsColorBtnClick(object sender, EventArgs e)
		{
			var c = new ColorDialog();
			c.FullOpen = true;
			c.Color = evenRowsColorBtn.BackColor;
			if (c.ShowDialog() != DialogResult.OK) return;
			evenRowsColorBtn.BackColor = c.Color;
		}
	}
}
