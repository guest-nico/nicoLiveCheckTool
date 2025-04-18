﻿/*
 * Created by SharpDevelop.
 * User: pc
 * Date: 2018/04/06
 * Time: 20:55
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.ComponentModel;
using Microsoft.Win32;
using namaichi.gui;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SunokoLibrary.Application;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Runtime.CompilerServices;
using namaichi.alart;
using namaichi.config;
using namaichi.utility;
using namaichi.rec;
using namaichi.info;

namespace namaichi
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		
		public namaichi.config.config config = new namaichi.config.config();
		private string[] args;
		
		public SortableBindingList<AlartInfo> alartListDataSource = new SortableBindingList<AlartInfo>();
		public SortableBindingList<TaskInfo> taskListDataSource = new SortableBindingList<TaskInfo>();
		public SortableBindingList<LiveInfo> liveListDataSource = new SortableBindingList<LiveInfo>();
		public SortableBindingList<LogInfo> logListDataSource = new SortableBindingList<LogInfo>();
		public SortableBindingList<HistoryInfo> historyListDataSource = new SortableBindingList<HistoryInfo>();
		public SortableBindingList<HistoryInfo> notAlartListDataSource = new SortableBindingList<HistoryInfo>();
		public SortableBindingList<AlartInfo> userAlartListDataSource = new SortableBindingList<AlartInfo>();
		public SortableBindingList<TwitterInfo> twitterListDataSource = new SortableBindingList<TwitterInfo>();
		public SortableBindingList<HistoryInfo> reserveHistoryListDataSource = new SortableBindingList<HistoryInfo>();
		public List<LiveInfo> liveListDataReserve = new List<LiveInfo>();
		//public BindingSource alartListDataSource = new BindingSource();
		//public BindingSource taskListDataSource = new BindingSource();
		public Check check = null;
		public TaskCheck taskCheck = null;
		public LiveCheck liveCheck = null;
		public TwitterCheck twitterCheck = null;
		
		public Mutex mutex = null;
		//private Thread madeThread;
		
		private DateTime lastBalloonTime = DateTime.MinValue;
		private DateTime lastIconBalloonTime = DateTime.MinValue;
		private string notifyUrl = null;
		public bool[] notifyOffList = new bool[17];
		public bool[] alartListColorColumns;
		public bool[] historyListColorColumns;
		public bool[] liveListColorColumns;
		private bool isAddFormDisplaying = false;
		
		public Thread madeThread;
		private object liveListLockObj = new object();
		public List<int> liveListLock = new List<int>();
		//public SemaphoreSlim liveListLockS = new SemaphoreSlim(1, 1);
		//public bool isLiveListLocking = false;
		private bool isLiveListTimeProcessing = false;
		
		private ToolMenuProcess toolMenuProcess;
		public DateTime lastChangeListDt = DateTime.MaxValue;
		private Color recentColor = Color.FromArgb(255,224,255);
		private Color followerOnlyColor = Color.FromArgb(255,224,255);
		private Color evenRowsColor = Color.FromArgb(245,245,245);
		
		private Icon defaultNotifyIcon = null;
		private int historyContainerDistance = 0;
		
		private bool isDisplayIconBalloon = false;
		private int liveListScrollIndex = -1;
		
		private ToolStripItem lastClickMenu = null;
		private string[] liveListSearchStr = new string[]{""};
		private string[] alartListSearchStr = new string[]{""};
		
		public MainForm(string[] args)
		{
			madeThread = Thread.CurrentThread;
			toolMenuProcess = new ToolMenuProcess(this);
			
			//config.set("IsTimeTable", "false");
			//this.madeThread = Thread.CurrentThread;
			//args = "-nowindo -stdIO -IsmessageBox=false -IscloseExit=true lv316762771 -ts-start=1785s -ts-end=0s -ts-list=false -ts-list-m3u8=false -ts-list-update=5 -ts-list-open=false -ts-list-command=\"notepad{i}\" -ts-vpos-starttime=true -afterConvertMode=4 -qualityRank=0,1,2,3,4,5 -IsLogFile=true".Split(' ');
			//read std
			ThreadPool.SetMinThreads(20, 20);
			
			
			//if (Array.IndexOf(args, "-std-read") > -1) startStdRead();
			
//			#if !DEBUGE
//				if (config.get("IsLogFile") == "true") 
//					config.set("IsLogFile", "false");
//			#endif
					
			System.Diagnostics.Debug.Listeners.Clear();
			System.Diagnostics.Debug.Listeners.Add(new Logger.TraceListener());
		    
			InitializeComponent();
			Text = "放送チェックツール（仮 " + util.versionStr;
			
			this.args = args;
			
			//saveControlLayout();
			//rec = new rec.RecordingManager(this, config);
			
			
//			args = new string[]{"a", "-qualityrank=1,2,3,4,5,0", "lv315967820", "-istitlebarinfo=False", "-ts-start=25h2m", "-openUrlListCommand=notepad"};
//			args = new string[]{"Debug_1.ts"};
			if (Array.IndexOf(args, "-stdIO") > -1) util.isStdIO = true;
			
			var lv = (args.Length == 0) ? null : util.getRegGroup(args[0], "(lv\\d+(,\\d+)*)");
			util.setLog(config, lv);
			
			if (args.Length > 0) {
				var ar = new ArgReader(args, config, this);
				ar.read();
				if (ar.isConcatMode) {
					//urlText.Text = string.Join("|", args);
	            	//rec.rec();
				} else {
					//if (ar.lvid != null) urlText.Text = ar.lvid;
					config.argConfig = ar.argConfig;
					//rec.argTsConfig = ar.tsConfig;
					//rec.isRecording = true;
//					rec.setArgConfig(args);
					
				}
				//if (bool.Parse(config.get("Isminimized"))) {
				//	this.WindowState = FormWindowState.Minimized;
				//}
            }

			util.debugWriteLine(util.versionStr + " " + util.versionDayStr);
			util.debugWriteLine("arg len " + args.Length);
			util.debugWriteLine("arg join " + string.Join(" ", args));
			
			
            //nicoSessionComboBox1.Selector.PropertyChanged += Selector_PropertyChanged;
//            checkBoxShowAll.Checked = bool.Parse(config.get("isAllBrowserMode"));
			//if (isInitRun) initRec();
			var fontSize = config.get("fontSize");  
			if (fontSize != "9")
				util.setFontSize(int.Parse(fontSize), this, true, 480);
			
			try {
				Width = int.Parse(config.get("Width"));
				Height = int.Parse(config.get("Height"));
				if (bool.Parse(config.get("IstasktrayStart"))) {
					Visible = false;
					ShowInTaskbar = false;
					WindowState = FormWindowState.Minimized;
				}
			} catch (Exception e) {
				util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
			}
			try {
				var x = config.get("X");
				var y = config.get("Y");
				if (x != "" && y != "") {
					StartPosition = FormStartPosition.Manual;
					Location = new Point(int.Parse(x), int.Parse(y));
				}
			} catch (Exception e) {
				util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
				StartPosition = FormStartPosition.WindowsDefaultLocation;
			}
			
			liveListDataSource.cfg = config;
			defaultNotifyIcon = notifyIcon.Icon;
			
			check = new Check(alartListDataSource, this);
			taskCheck = new TaskCheck(taskListDataSource, this);
			//twitterCheck = new TwitterCheck(twitterListDataSource, this);
			
			//formInitSetting();
			
			recentColor = bool.Parse(config.get("IsAlartListRecentColor")) ?
					Color.Empty : ColorTranslator.FromHtml(config.get("recentColor"));
			followerOnlyColor = bool.Parse(config.get("IsFollowerOnlyOtherColor")) ?
				ColorTranslator.FromHtml(config.get("followerOnlyColor")) : Color.Empty;
			evenRowsColor = ColorTranslator.FromHtml(config.get("evenRowsColor"));
			
			var osName = config.get("osName");
			if (osName == "") {
				osName = util.CheckOSName();
				config.set("osName", osName);
			}
			util.osName = osName;
			
			var osType = config.get("osType");
			if (osType == "") {
				osType = util.CheckOSType();
				config.set("osType", osType);
			}
			util.debugWriteLine("OS: " + util.osName);
			util.debugWriteLine("OSType: " + osType);
			if (util.isCurl) {
				try {
					Curl.curl_global_init((1<<0) | (1<<1));//CURL_GLOBAL_SSL|CURL_GLOBAL_WIN32
				} catch (Exception e) {
					if (e.ToString().IndexOf("DllNotFound") > -1) {
						MessageBox.Show("libcurlのライブラリが正常に読み込めませんでした。libcurl.dllもしくは依存ライブラリが正常に配置されていない可能性があります");
					}
					if (util.osName != null && util.osName.IndexOf("XP") > -1)
							MessageBox.Show("Windows XPの場合、Visual Studio 2015 の Visual C++ 再頒布可能パッケージが必要です。");
					util.debugWriteLine(e.Message + e.Source + e.StackTrace);
				}
			}
		}
		private void formInitSetting() {
			alartList.DataSource = alartListDataSource;
			taskList.DataSource = taskListDataSource;
			liveList.DataSource = liveListDataSource;
			logList.DataSource = logListDataSource;
			historyList.DataSource = historyListDataSource;
			notAlartList.DataSource = notAlartListDataSource;
			userAlartList.DataSource = userAlartListDataSource;
			twitterList.DataSource = twitterListDataSource;
			reserveHistoryList.DataSource = reserveHistoryListDataSource;
			
			setDoubleBuffered(alartList);
			setDoubleBuffered(taskList);
			setDoubleBuffered(liveList);
			setDoubleBuffered(logList);
			setDoubleBuffered(historyList);
			setDoubleBuffered(notAlartList);
			setDoubleBuffered(userAlartList);
			setDoubleBuffered(twitterList);
			setDoubleBuffered(reserveHistoryList);
			
			setCategoryBorderPaint();
			//categoryRightBtn.Text += Convert.ToChar(9654);//右
			//categoryLeftBtn.Text += Convert.ToChar(9664);//左
			categoryLeftBtn.Paint += new PaintEventHandler(categoryMoveBorderPaint);
			categoryRightBtn.Paint += new PaintEventHandler(categoryMoveBorderPaint);
			
			
			foreach (var c in categoryBtnPanel.Controls) {
				var cc = c.GetType();
				if (c.GetType() == typeof(System.Windows.Forms.RadioButton))
					((System.Windows.Forms.RadioButton)c).CheckedChanged += 
						new EventHandler(CategoryBtnCheckedChanged);
			}
			
			setBackColor(Color.FromArgb(int.Parse(config.get("alartBackColor"))));
			setForeColor(Color.FromArgb(int.Parse(config.get("alartForeColor"))));
			
			setCheckableMenuClosingEvent();
			
			try {
				var liveFilterC = config.get("liveListFilter");
				if (liveFilterC != "")
					liveListFilterBtn.Tag = Newtonsoft.Json.JsonConvert.DeserializeObject< List<LiveListFilterInfo>>(liveFilterC);
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace);
				addLogText(e.Message + e.Source + e.StackTrace);
			}
			
			setFormState();
			
//			if (!util.isShowWindow) return;
			foreach (DataGridViewColumn c in alartList.Columns)
				c.SortMode = DataGridViewColumnSortMode.Automatic;
			foreach (DataGridViewColumn c in taskList.Columns)
				c.SortMode = DataGridViewColumnSortMode.Automatic;
			foreach (DataGridViewColumn c in liveList.Columns)
				c.SortMode = DataGridViewColumnSortMode.Automatic;
			foreach (DataGridViewColumn c in twitterList.Columns)
				c.SortMode = DataGridViewColumnSortMode.Automatic;
			foreach (DataGridViewColumn c in reserveHistoryList.Columns)
				c.SortMode = DataGridViewColumnSortMode.Automatic;
			
			liveList.RowTemplate.Height = liveList.Columns[1].Width;
			
			categoryBtnDisplayUpdate();
			
			setSort();
			
			applyMenuSetting();
			setAppliNameAndContextMenu();
		}
		
		
		/*
		async void Selector_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
			
			
	
			
            switch(e.PropertyName)
            {
                case "SelectedIndex":
                    var cookieContainer = new CookieContainer();
                    var currentGetter = nicoSessionComboBox1.Selector.SelectedImporter;
                    if (currentGetter != null)
                    {
//                        var result = await currentGetter.GetCookiesAsync(TargetUrl);
                        
//                        var cookie = result.Status == CookieImportState.Success ? result.Cookies["user_session"] : null;
                        
                        //logText.Text += cookie.Name + cookie.Value+ cookie.Expires;
                        
                        //UI更新
//                        txtCookiePath.Text = currentGetter.SourceInfo.CookiePath;
//                        btnOpenCookieFileDialog.Enabled = true;
//                        txtUserSession.Text = cookie != null ? cookie.Value : null;
//                        txtUserSession.Enabled = result.Status == CookieImportState.Success;
                        //Properties.Settings.Default.SelectedSourceInfo = currentGetter.SourceInfo;
                        //Properties.Settings.Default.Save();
//                        config.set("browserNum", nicoSessionComboBox1.Selector.SelectedIndex.ToString());
//                        if (cookie != null) config.set("user_session", cookie.Value);
//                        config.set("isAllBrowserMode", nicoSessionComboBox1.Selector.IsAllBrowserMode.ToString());
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
			util.debugWriteLine(DateTime.Now.ToString("{W}"));
			var si = nicoSessionComboBox1.Selector.SelectedImporter.SourceInfo;
			util.debugWriteLine(si.EngineId + " " + si.BrowserName + " " + si.ProfileName);
//			var a = new SunokoLibrary.Application.Browsers.FirefoxImporterFactory();
//			foreach (var b in a.GetCookieImporters()) {
//				var c = b.GetCookiesAsync(TargetUrl);
//				c.ConfigureAwait(false);
				
//				util.debugWriteLine(c.Result.Cookies["user_session"]);
//			}
			util.debugWriteLine(nicoSessionComboBox1.Selector.SelectedImporter.SourceInfo.CookiePath);
			//System.IO.Directory.CreateDirectory("aa/ss/u");
			//a.GetCookieImporter(new CookieSourceInfo("
			//var tsk = nicoSessionComboBox1.Selector.UpdateAsync(); 
		}
        void btnOpenCookieFileDialog_Click(object sender, EventArgs e)
        { var tsk = nicoSessionComboBox1.ShowCookieDialogAsync(); }
        void checkBoxShowAll_CheckedChanged(object sender, EventArgs e)
        { nicoSessionComboBox1.Selector.IsAllBrowserMode = checkBoxShowAll.Checked;
        	//config.set("isAllBrowserMode", nicoSessionComboBox1.Selector.IsAllBrowserMode.ToString());
        }
        void playBtn_Click(object sender, EventArgs e)
        { player.play();}
        */
        void optionItem_Select(object sender, EventArgs e)
        {
        	try {
	        	optionForm o = new optionForm(config, this);
	        	var size = config.get("fontSize");
	        	var r = o.ShowDialog();
	        	if (r == DialogResult.OK) {
	        		var newSize = config.get("fontSize");
	        		applyMenuSetting();
	        		if (size != newSize) {
	        			var formSize = Size;
	        			var loc = Location;
	        			loadControlLayout();
	        			util.setFontSize(int.Parse(newSize), this, true, 480);
	        			Size = formSize;
	        			Location = loc;
	        			check.popup.setPopupSize();
	        		}
	        		Task.Factory.StartNew(() => {
	        		    resetColorSetting();
	        		    
    		         	check.setCookie();
    		         	followCheck();
    		         	
    		         	check.resetCheck();
    		         	setAppliNameAndContextMenu();
    		         	recentLiveCheck();
    		         });
	        		
	        		if (bool.Parse(config.get("IsAllowMultiProcess"))) {
						if (mutex != null) util.releaseMutex(mutex);
						mutex = null;
					} else if (mutex == null) {
        				mutex = util.doubleRunCheck();
        			}
	        	}
	        } catch (Exception ee) {
        		util.debugWriteLine(ee.Message + " " + ee.StackTrace);
	        }
        }
        
        /*
        public async Task<Cookie> getCookie() {
        	var cookieContainer = new CookieContainer();
            var currentGetter = nicoSessionComboBox1.Selector.SelectedImporter;
            if (currentGetter != null)
            {
            	
            	var result = await currentGetter.GetCookiesAsync(TargetUrl).ConfigureAwait(false);
                var cookie = result.Status == CookieImportState.Success ? result.Cookies["user_session"] : null;
                //logText.Text += cookie.Name + cookie.Value+ cookie.Expires;
                return cookie;
            }
            else return null;
        }
        */
        public void addLogText(string t, bool isInvoke = true) {
       		formAction(() => {
       	    	try {
	        	    string _t = "";
			    	if (logText.Text.Length != 0) _t += "\r\n";
			    	_t += DateTime.Now.ToString("yyyy\"/\"MM\"/\"dd HH\":\"mm\":\"ss") + " " + t;
			    	
		    		logText.AppendText(_t);
					if (logText.Text.Length > 200000) 
						logText.Text = logText.Text.Substring(logText.TextLength - 10000);
   		       	} catch (Exception e) {
   		       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
   		       	}
       	           	
       	    });
       	    
		}
        public void addLogTextTest(string t) {
       		addLogText(t);
        }
		
        private void initRec() {
        	//util.debugWriteLine(int.Parse(config.get("browserName")));
        	//util.debugWriteLine(bool.Parse(config.get("isAllBrowserMode")));
        	
        	//try {
        	//	nicoSessionComboBox1.SelectedIndex = int.Parse(config.get("browserNum"));
        	//} catch (Exception e) {util.debugWriteLine(333); return;};
        	//var t = getCookie();
			//t.ConfigureAwait(false);
			//util.debugWriteLine(t.Result);
            if (args.Length > 0) {
            	//urlText.Text = args[0];
//            	rec = new rec.RecordingManager(this);
            	//rec.rec();

            }
			
//			isInitRun = false;
        }
		
		
		void endMenu_Click(object sender, EventArgs e)
		{
			try {
				kakuninClose();
				Application.Exit();
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + " " + ee.StackTrace + " " + ee.TargetSite);
			}
				
//			if (kakuninClose()) Close();;
		}
		
		void form_Close(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason != CloseReason.ApplicationExitCall) {
				e.Cancel = true;
				Visible = false;
				return;
			}
			if (!kakuninClose()) e.Cancel = true;
			util.releaseMutex(mutex);
		}
		bool kakuninClose() {
			/*
			if (rec.rfu != null) {
				var _m = (rec.isPlayOnlyMode) ? "視聴" : "録画";
				DialogResult res = MessageBox.Show(_m + "中ですが終了しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (res == DialogResult.No) return false;
			}
			*/
			try{
				util.debugWriteLine("width " + Width.ToString() + " height " + Height.ToString() + " restore width " + RestoreBounds.Width.ToString() + " restore height " + RestoreBounds.Height.ToString());
				if (this.WindowState == FormWindowState.Normal) {
					config.set("Width", Width.ToString());
					config.set("Height", Height.ToString());
					config.set("X", Location.X.ToString());
					config.set("Y", Location.Y.ToString());
				} else {
					config.set("Width", RestoreBounds.Width.ToString());
					config.set("Height", RestoreBounds.Height.ToString());
					config.set("X", RestoreBounds.X.ToString());
					config.set("Y", RestoreBounds.Y.ToString());
				}
				
				new AlartListFileManager(false, this).save();
				new AlartListFileManager(true, this).save();
				new TaskListFileManager().save(this);
				new HistoryListFileManager().save(this);
				new NotAlartListFileManager().save(this);
				//new TwitterListFileManager().save(this);
				new ReserveHistoryListFileManager().save(this);
				saveMenuSetting();
				saveFormState();
				saveSortState();
				
			} catch(Exception e) {
				util.debugWriteLine(e.Message + " " + e.StackTrace);
			}
			//player.stopPlaying(true, true);
			return true;
		}
		
		void mainForm_Load(object sender, EventArgs e)
		{
			tabControl1.TabPages.Remove(tabPage5);
			
			
			formInitSetting();
			setNotifyOff();
			setNotifyOffColumn();
			
			if (!bool.Parse(config.get("IsShowNoComAlartMessageAgain")) &&
					MessageBox.Show("2024年8月15日現在、ニコニコミュニティのサービスが停止しているため、コミュニティが条件に設定されている場合には正常に通知されないことがあります。\nこのバージョンではお気に入りの条件について、次のような動作となっています。\n\nコミュニティのみが条件に設定されている場合：\n通知を行わない\n\nユーザーIDやキーワードと共に条件が設定されている場合：\nコミュニティ以外の条件のみを用いて条件を判定\n\n次からこのメッセージを表示しない", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
				config.set("IsShowNoComAlartMessageAgain", "true");
			
			if (config.brokenCopyFile != null)
				System.Windows.Forms.MessageBox.Show("設定ファイルを読み込めませんでした。設定ファイルをバックアップしました。" + config.brokenCopyFile);
			
			Task.Factory.StartNew(() => {
				new AlartListFileManager(false, this).load();
				new AlartListFileManager(true, this).load();
				alartListDataSource.upOnAir = 
						userAlartListDataSource.upOnAir = 
						int.Parse(config.get("alartListUpOnAirMode"));
				sortAlartList(false);
				sortAlartList(true);
				
				new HistoryListFileManager().load(this);
				if (bool.Parse(config.get("IsExistInHistoryListNotAlart"))) {
					try {
						var hiLvidList = historyListDataSource.Select(x => 
								new RssItem(x.title, x.lvid, x.dt.ToString(), x.description, x.communityName, x.communityId, x.userName, "", x.isMemberOnly.ToString(), "", x.isPayment)).ToList();
						util.debugWriteLine("sss " + hiLvidList.Count);
						check.checkedLvIdList.AddRange(hiLvidList);
					} catch (Exception ee) {
						util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
					}
				}
				check.start();
			});
			Task.Factory.StartNew(() => {
				new TaskListFileManager().load(this);
				taskCheck.start();
			});
			/*
			Task.Factory.StartNew(() => {
				new TwitterListFileManager().load(this);
				twitterCheck.start();
			});
			*/
			Task.Factory.StartNew(() => {
			                      	/*
				new HistoryListFileManager().load(this);
				var hiLvidList = historyListDataSource.Select(x => 
						new RssItem(x.title, x.lvid, x.dt.ToString(), x.description, x.communityName, x.communityId, x.userName, "", x.isMemberOnly.ToString(), "", x.isPayment)).ToList();
				util.debugWriteLine("sss " + hiLvidList.Count);
				check.checkedLvIdList.AddRange(hiLvidList);
				*/
			});
			Task.Factory.StartNew(() => {
				new NotAlartListFileManager().load(this);
			});
			Task.Factory.StartNew(() => {
				new ReserveHistoryListFileManager().load(this);
			});
			Task.Factory.StartNew(() => {
			    liveCheck = new LiveCheck(this);
			    if (bool.Parse(config.get("AutoStart")))
			    	formAction(() => updateAutoUpdateStartMenu.PerformClick());
			    
			});
			
			//.net
			util.debugWriteLine(".net version check");
			var ver = util.Get45PlusFromRegistry();  
			util.debugWriteLine(".net ver " + ver);
			if (ver < 4 && ver != -1) {
				
				//Task.Factory.StartNew(() => {
				    //Invoke((MethodInvoker)delegate() {
						//var b = new DotNetMessageBox(ver);
						//b.Show(this); 
						System.Windows.Forms.MessageBox.Show("動作には.NET 4以上が推奨です。");
					//});
				//});
			}
			//dll
			var task = Task.Factory.StartNew(() => {
				util.dllCheck(this);
				xpTest();
			});
			
			try {
				var r = util.sendRequest("https://live.nicovideo.jp/", util.getHeader(null, null, null), null, "GET", false, null);
				if (r == null) throw new Exception("webrequest null");
				using (var s = r.GetResponseStream())
				using (var sr = new StreamReader(s)) {
					var srr = sr.ReadToEnd();
					util.isWebRequestOk = true;
					#if DEBUG
						addLogText("debug: webrequestを有効にします");
					#endif
				}
			} catch (Exception eee) {
				util.debugWriteLine(eee.Message + eee.Source);
				#if DEBUG
					addLogText("debug: curlを使用します");
				#endif
			}
			return;
		}
		void xpTest() {
			try {
				_xpText().Wait();
			} catch (Exception) {
				addLogText("正常に非同期処理が行えませんでした。Microsoft .NET Framework 4 KB2468871 https://www.microsoft.com/en-us/download/details.aspx?id=3556を導入してみると動作するかもしれません。");
				MessageBox.Show("正常に非同期処理が行えませんでした。Microsoft .NET Framework 4 KB2468871 https://www.microsoft.com/en-us/download/details.aspx?id=3556を導入してみると動作するかもしれません。");
				util.debugWriteLine("正常に非同期処理が行えませんでした。Microsoft .NET Framework 4 KB2468871 https://www.microsoft.com/en-us/download/details.aspx?id=3556を導入してみると動作するかもしれません。");
			}
		}
		async Task _xpText() {
			await Task.Factory.StartNew(() => {});
		}
		
		void versionMenu_Click(object sender, EventArgs e)
		{
			var v = new VersionForm(config, this);
			v.ShowDialog();
		}
		/*
		void startStdRead() {
			Task.Factory.StartNew(() => {
	         	while (true) {
					var a = Console.ReadLine();
					if (a == null || a.Length == 0) continue;
					if (a == "stop end") {
						//if (rec.rfu != null) {
						//	rec.stopRecording();
						//}
						//while (rec.recordRunningList.Count > 0) {
						//	Thread.Sleep(1000);
						//}
						Close();
					}
				}
			});
		}
		*/
		void AlartListDragDrop(object sender, DragEventArgs e)
		{
			try {
				util.debugWriteLine("dragdrop");
				
				var t = e.Data.GetData(DataFormats.Text).ToString();
				string id = null;
				var lv = util.getRegGroup(t, "(lv\\d+)");
				if (lv != null) id = lv;
				else {
					var coch = util.getRegGroup(t, "(c[oh]\\d+)");
					if (coch != null) id = coch;
					else {
						var user = util.getRegGroup(t, "user/(\\d+)");
						if (user != null) id = user;
					}
				}
				if (id == null) return;
				
				
				Task.Factory.StartNew(() => formAction(() => openAddForm(id)));
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		void TaskListDragDrop(object sender, DragEventArgs e)
		{
			try {
				util.debugWriteLine("dragdrop");
				
				var t = e.Data.GetData(DataFormats.Text).ToString();
				var lv = util.getRegGroup(t, "(lv\\d+)");
				if (lv == null) return;
				
				Task.Factory.StartNew(() =>
				         formAction(() => openAddYoyakuForm(lv))
					);
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		void listDragEnter(object sender, DragEventArgs e)
		{
			if (isAddFormDisplaying) return;
			
			if (e.Data.GetDataPresent("UniformResourceLocator") ||
			    e.Data.GetDataPresent("UniformResourceLocatorW") ||
			    e.Data.GetDataPresent(DataFormats.Text)) {
				util.debugWriteLine(e.Effect);
				e.Effect = DragDropEffects.Copy;
			}
		}
		void AddBtnClick(object sender, System.EventArgs e)
		{
			openAddForm();
		}
		void AddYoyakuBtnClick(object sender, System.EventArgs e)
		{
			openAddYoyakuForm();
		}
		void openAddForm(string id = null, AlartInfo editAi = null, bool isUserMode = false) {
			try {
				isAddFormDisplaying = true;
	        	var o = new addForm(this, id, editAi, isUserMode); o.ShowDialog(this);
	        	isAddFormDisplaying = false;
	        	if (o.ret == null) return;
	        	
	        	var dataSource = isUserMode ? userAlartListDataSource : alartListDataSource;
	        	var list = isUserMode ? userAlartList : alartList;
	        	
	        	util.debugWriteLine("add datasource " + dataSource.GetHashCode() + " comid " + o.ret.communityId + " comname " + o.ret.communityName + " hostid " + o.ret.hostId + " hostname " + o.ret.hostName + " keyword " + o.ret.keyword);
	        	if (editAi == null) {
	        		dataSource.Add(o.ret);
	        	} else {
	        		var i = dataSource.IndexOf(o.ret);
	        		if (i == -1) return;
	        		foreach (DataGridViewCell c in list.Rows[i].Cells)
	        			list.UpdateCellValue(c.ColumnIndex, c.RowIndex);
	        	}
	        	
	        	Task.Factory.StartNew(() => {
					new AlartListFileManager(false, this).save();
					new AlartListFileManager(true, this).save();
				});
	        	if (o.inputLvidItem != null) check.checkedLvIdList.Add(o.inputLvidItem);
	        		Task.Factory.StartNew(() => {
                      	var addLive = getOnAirLastLiveList(check.checkedLvIdList, o.ret);
                      	if (addLive != null)
	                      	//check.foundLive(addLive, false);
	                      	util.debugWriteLine("addform addlive after foundlive");
                      });
	        		                      
	        } catch (Exception ee) {
        		util.debugWriteLine(ee.Message + " " + ee.StackTrace);
	        }
		}
		List<RssItem> getOnAirLastLiveList(List<RssItem> _liveList, AlartInfo ai) {
			var liveList = _liveList.ToList().OrderByDescending(x => x.pubDateDt).ToArray();
			var isChanged = false;
			var ret = new List<RssItem>();
			var bindingList = new SortableBindingList<AlartInfo>(new List<AlartInfo>(){ai});
			try {
				var listAi = alartListDataSource.ToList();
				listAi.AddRange(userAlartListDataSource.ToList());
				for (var i = 0; i < liveList.Count(); i++) {
					var targetAi = new List<AlartInfo>();
					AlartInfo nearAlartAi = null;
					var dpi = new DoProcessInfo();
					bool isSuccessAccess = check.getAlartProcess(dpi,
							ref isChanged, 
							ref targetAi, ref nearAlartAi, ref liveList[i],
							bindingList);
					
					if (targetAi.Count > 0) {
						if (isOnAirLvid(liveList[i].lvId, liveList[i].type))
							ret.Add(liveList[i]);
						return ret;
					}
				}
				return ret;
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				return null;
			}
		}
		void openAddYoyakuForm(string id = null) {
			try {
	        	var o = new addTaskForm(this, id); o.ShowDialog();
	        	if (o.ret == null) return;
	        	taskListDataSource.Add(o.ret);
	        	
	        	Task.Factory.StartNew(() => {
					new TaskListFileManager().save(this);
				});
	        	//changedListContent();
	        } catch (Exception ee) {
        		util.debugWriteLine(ee.Message + " " + ee.StackTrace);
	        }
		}
		public void updateLastHosoDate(AlartInfo ai, string hosoDate, string lvid, bool isMemberOnly, string type, string title) {
			util.debugWriteLine("updateLastHosoDate " + ai.lastLvid + " " + ai.communityId + " " + ai.hostId + " " + lvid + " " + hosoDate);
			try {
				ai.LastHostDate = hosoDate;//now.Substring(0, now.Length - 0);
				ai.lastLvid = lvid;
				ai.recentColorMode = bool.Parse(config.get("IscheckRecent")) ? ((isMemberOnly) ? 2 : 1) : 0;
				ai.lastLvType = type;
				ai.lastLvTitle = title;
				ai.addHistory(ai.lastHosoDt, lvid.Replace("e", "") + " " + ai.lastHostDate + " " + title);
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace);
			}
			formAction(() => {
		         try {
					var i = alartListDataSource.IndexOf(ai);
					if (i > -1)
						alartListDataSource.ResetItem(i);
					i = userAlartListDataSource.IndexOf(ai);
					if (i > -1)
						userAlartListDataSource.ResetItem(i);
					
   		       	} catch (Exception e) {
   		       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
   		       	}  	
			});
			try {
				setNotifyIcon();
				changedListContent();
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace);
			}
		}
		public void updateUserName(int i, string userName, bool isFollow, bool isUserMode) {
			var dataSource = isUserMode ? userAlartListDataSource : alartListDataSource;
			formAction(() => {
				try {
       		        var ai = ((info.AlartInfo)dataSource[i]);
       		        var follow = (isFollow) ? "フォロー解除する" : "フォローする";
       		        if (ai.hostName != userName || ai.hostFollow != follow) {
						ai.hostName = userName;
						ai.hostFollow = follow;
						dataSource.ResetItem(i);
       		        }
   		       	} catch (Exception e) {
   		       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
   		       	}
			});
		}
		public void updateCommunityName(int i, string comName, bool isFollow) {
			formAction(() => {
			
   		       	try {
       		        var ai = ((info.AlartInfo)alartListDataSource[i]);
       		        var follow = (isFollow) ? "フォロー解除する" : "フォローする";
       		        if (ai.communityName != comName || ai.communityFollow != follow) {
       		        	ai.communityName = comName;
						ai.communityFollow = follow;
						alartListDataSource.ResetItem(i);
       		        }
					
   		       	} catch (Exception e) {
   		       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
   		       	}
	
			});
		}
		public int getAlartListCount(bool isUserMode) {
			var ret = 0;
			/*
			int c, cc;
			try {
				c = alartListDataSource.Count;
			} catch (Exception e) {util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);}
			try {
				cc = alartList.Rows.Count;
			} catch (Exception e) {util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);}
			*/
			
			formAction(() => {
	       		
   		       	try {
       		        if (isUserMode)
						ret = userAlartListDataSource.Count;
       		        else ret = alartListDataSource.Count;
   		       	} catch (Exception e) {
   		       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
   		       	}
			});
	       	return ret;
		}
		public int getTaskListCount() {
			var ret = 0;
			formAction(() => {
   		       	try {
					ret = taskListDataSource.Count;
   		       	} catch (Exception e) {
   		       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
   		       	}
	       		
			});
			return ret;
		}
		public int getLiveListCount() {
			var ret = 0;
			formAction(() => {
   		       	try {
					ret = liveListDataSource.Count;
   		       	} catch (Exception e) {
   		       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
   		       	}
	       		
			});
			return ret;
		}
		void AlartListCellClick(object sender, DataGridViewCellEventArgs e)
		{
			var isUserMode = ((DataGridView)sender).Name == "userAlartList";
			var dataSource = isUserMode ? userAlartListDataSource : alartListDataSource;
			var list = isUserMode ? userAlartList : alartList;
			
			util.debugWriteLine(e.ColumnIndex + " " + e.RowIndex);
			if (e.ColumnIndex == -1 || e.RowIndex == -1) return;
			if (list.Columns[6].ReadOnly) return;
			try {
				var val = list[e.ColumnIndex, e.RowIndex].Value;
				if (val.GetType() == typeof(string) &&
						string.IsNullOrEmpty((string)list[e.ColumnIndex, e.RowIndex].Value)) return;
			} catch (Exception) {
				
			}
			if (e.ColumnIndex == 6 && list[0, e.RowIndex].Value is string &&
			    	(string)list[0, e.RowIndex].Value != "") {
				comChannelFollowCellClick(dataSource[e.RowIndex]);
			}
			if (e.ColumnIndex == 7 && list[1, e.RowIndex].Value is string &&
			    	(string)list[1, e.RowIndex].Value != "") {
				userFollowCellClick(dataSource[e.RowIndex], dataSource, list);
			}
			
		}
		public bool comChannelFollowCellClick(AlartInfo ai, bool isLog = true) {
			try {
				var rowIndex = alartListDataSource.IndexOf(ai);
				if (rowIndex == -1) return false;
				if (alartList[6, rowIndex].Value is string && (string)alartList[6, rowIndex].Value == "") return false;
				
				var comId = ai.communityId;
				var isChannel = comId.StartsWith("ch");
				if (string.IsNullOrEmpty(alartList[6, rowIndex].Value.ToString())) return false;
				var isFollow = (string)alartList[6, rowIndex].Value == "フォローする";
				var isOk = false;
				
				if (bool.Parse(config.get("IsConfirmFollow"))) {
					var msg = (isChannel ? "チャンネル" : "コミュニティ") + "ID " + comId + "を";
					var r = MessageBox.Show(msg + "フォロー" + (isFollow ? "" : "解除") + "しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
					if (r == DialogResult.No) return false;
				}
				
				if (isChannel) {
					if (isFollow) {
						isOk = new FollowChannel(false).followChannel(comId, check.container, this, config);
						if (isOk) alartList[6, rowIndex].Value = "フォロー解除する";
					} else {
						isOk = new FollowChannel(false).unFollowChannel(comId, check.container, this, config);
						if (isOk) alartList[6, rowIndex].Value = "フォローする";
					}
				} else {
					if (isFollow) {
						isOk = new FollowCommunity(false).followCommunity(comId, check.container, this, config);
						if (isOk) alartList[6, rowIndex].Value = "フォロー解除する";
					} else {
						isOk = new FollowCommunity(false).unFollowCommunity(comId, check.container, this, config);
						if (isOk) alartList[6, rowIndex].Value = "フォローする";
					}
				}
				
				 if (isLog) {
					var kaijo = (isFollow) ? "" : "解除";
					var msg = (isChannel ? "チャンネル" : "コミュニティ") + "ID " + ai.communityId + "の";
					addLogText(msg + ((isOk) ? ("フォロー" + kaijo + "に成功しました") : ("フォロー" + kaijo + "に失敗しました。")));
				 }
				
				if (!isOk) {
					bool _isFollow;
					util.getCommunityName(comId, out _isFollow, check.container);
					alartList[6, rowIndex].Value = (_isFollow) ? "フォロー解除する" : "フォローする";
				}
				return isOk;
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				addLogText("チャンネルのフォロー中に問題が発生しました " + e.Message + e.Source + e.StackTrace + e.TargetSite);
				return false;
			}
		}
		public bool userFollowCellClick(AlartInfo ai, SortableBindingList<AlartInfo> dataSource, DataGridView list, bool isLog = true) {
			try {
				
					
				var rowIndex = dataSource.IndexOf(ai);
				if (rowIndex == -1) return false;
				if ((string)list[7, rowIndex].Value == "") return false;
				if (string.IsNullOrEmpty(ai.hostFollow)) return false;
				
				bool isOk = false;
				
				var isFollow = ai.hostFollow == "フォローする";
				if (bool.Parse(config.get("IsConfirmFollow"))) {
					var msg = "ユーザーID " + ai.hostId + "を";
					var r = MessageBox.Show(msg + "フォロー" + (isFollow ? "" : "解除") + "しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
					if (r == DialogResult.No) return false;
				}
				
				if (isFollow) {
					isOk = new FollowUser().followUser(ai.hostId, check.container, this, config);
					if (isOk) list[7, rowIndex].Value = "フォロー解除する";
					if (isLog) addLogText("ユーザーID " + ai.hostId + ((isOk) ? "のフォローに成功しました" : "のフォローに失敗しました。"));
				} else {
					isOk = new FollowUser().unFollowUser(ai.hostId, check.container, this, config);
					if (isOk) list[7, rowIndex].Value = "フォローする";
					if (isLog) addLogText("ユーザーID " + ai.hostId + ((isOk) ? "フォロー解除に成功しました" : "フォロー解除に失敗しました。"));
				}
				
				if (!isOk) {
					//bool isFollow;
					util.getUserName(ai.hostId, out isFollow, check.container, true, config);
					list[7, rowIndex].Value = (isFollow) ? "フォロー解除する" : "フォローする";
				} else {
					foreach (var _ai in alartListDataSource)
						if (_ai.hostId == ai.hostId)
							_ai.hostFollow = isFollow ? "フォロー解除する" : "フォローする";
					foreach (var _ai in userAlartListDataSource)
						if (_ai.hostId == ai.hostId)
							_ai.hostFollow = isFollow ? "フォロー解除する" : "フォローする";
				}
				return isOk;
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				addLogText("ユーザーのフォロー中に問題が発生しました " + e.Message + e.Source + e.StackTrace + e.TargetSite);
				return false;
			}
		}

		void AlartListCellParsing(object sender, System.Windows.Forms.DataGridViewCellParsingEventArgs e)
		{
			AlartListCellParsingCommon(alartListDataSource, e);
		}
		void AlartListCellParsingCommon(SortableBindingList<AlartInfo> dataSource, System.Windows.Forms.DataGridViewCellParsingEventArgs e) {
			util.debugWriteLine(e.ColumnIndex);
			try {
				var target = ((AlartInfo)dataSource[e.RowIndex]);
				if (e.ColumnIndex == 0) target.communityId = (string)e.Value;
				if (e.ColumnIndex == 1) target.hostId = (string)e.Value;
				if (e.ColumnIndex == 2) target.communityName = (string)e.Value;
				if (e.ColumnIndex == 3) target.hostName = (string)e.Value;
				if (e.ColumnIndex == 4) target.keyword = (string)e.Value;
				if (e.ColumnIndex == 26) target.memo = (string)e.Value;
				util.debugWriteLine("cell parcing");
				//changedListContent();
				Task.Factory.StartNew(() => {
					new AlartListFileManager(false, this).save();
					new AlartListFileManager(true, this).save();
				});
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		

		void AlartListCurrentCellDirtyStateChanged(object sender, EventArgs e)
		{
			//var isUserMode = ((DataGridView)sender).Name == "userAlartList";
			//var list = isUserMode ? userAlartList : alartList;
			try {
				var cc = alartList.CurrentCell;
				if (cc is DataGridViewCheckBoxCell) {
					foreach (DataGridViewCell selectCells in alartList.SelectedCells) {
						if (!(selectCells is DataGridViewCheckBoxCell)) return;
						if (selectCells.ColumnIndex < 10 || selectCells.ColumnIndex > 24) return;
					}
					foreach (DataGridViewCheckBoxCell selectCells in alartList.SelectedCells) {
						selectCells.Value = (bool)cc.Value;
					}
					
					foreach(DataGridViewCell _cc in alartList.SelectedCells) {
				        if (_cc.ColumnIndex >= 10 && _cc.ColumnIndex <= 24) {
							alartList.CommitEdit(DataGridViewDataErrorContexts.Commit);
							var target = ((AlartInfo)alartListDataSource[_cc.RowIndex]);
							if (_cc.ColumnIndex == 10) target.popup = (bool)(alartList[_cc.ColumnIndex, _cc.RowIndex].Value);
							if (_cc.ColumnIndex == 11) target.baloon = (bool)(alartList[_cc.ColumnIndex, _cc.RowIndex].Value);
							if (_cc.ColumnIndex == 12) target.browser = (bool)(alartList[_cc.ColumnIndex, _cc.RowIndex].Value);
							if (_cc.ColumnIndex == 13) target.mail = (bool)(alartList[_cc.ColumnIndex, _cc.RowIndex].Value);
							if (_cc.ColumnIndex == 14) target.sound = (bool)(alartList[_cc.ColumnIndex, _cc.RowIndex].Value);
							if (_cc.ColumnIndex == 15) target.appliA = (bool)(alartList[_cc.ColumnIndex, _cc.RowIndex].Value);
							if (_cc.ColumnIndex == 16) target.appliB = (bool)(alartList[_cc.ColumnIndex, _cc.RowIndex].Value);
							if (_cc.ColumnIndex == 17) target.appliC = (bool)(alartList[_cc.ColumnIndex, _cc.RowIndex].Value);
							if (_cc.ColumnIndex == 18) target.appliD = (bool)(alartList[_cc.ColumnIndex, _cc.RowIndex].Value);
							if (_cc.ColumnIndex == 19) target.appliE = (bool)(alartList[_cc.ColumnIndex, _cc.RowIndex].Value);
							if (_cc.ColumnIndex == 20) target.appliF = (bool)(alartList[_cc.ColumnIndex, _cc.RowIndex].Value);
							if (_cc.ColumnIndex == 21) target.appliG = (bool)(alartList[_cc.ColumnIndex, _cc.RowIndex].Value);
							if (_cc.ColumnIndex == 22) target.appliH = (bool)(alartList[_cc.ColumnIndex, _cc.RowIndex].Value);
							if (_cc.ColumnIndex == 23) target.appliI = (bool)(alartList[_cc.ColumnIndex, _cc.RowIndex].Value);
							if (_cc.ColumnIndex == 24) target.appliJ = (bool)(alartList[_cc.ColumnIndex, _cc.RowIndex].Value);
							util.debugWriteLine(target.appliA);
						}
					}
				}
				if (cc is DataGridViewComboBoxCell) {
					foreach (DataGridViewCell selectCells in alartList.SelectedCells) {
						if (!(selectCells is DataGridViewComboBoxCell)) return;
					}
					foreach (DataGridViewComboBoxCell selectCells in alartList.SelectedCells) {
						selectCells.Value = cc.Value;
					}
					
					foreach(DataGridViewCell _cc in alartList.SelectedCells) {
						alartList.CommitEdit(DataGridViewDataErrorContexts.Commit);
						var target = ((AlartInfo)alartListDataSource[cc.RowIndex]);
						if (cc.ColumnIndex == 5) target.IsAnd = (alartList[cc.ColumnIndex, cc.RowIndex].Value.ToString());
						if (cc.ColumnIndex == 25) target.SoundType = (alartList[cc.ColumnIndex, cc.RowIndex].Value.ToString());
					}
				}
				Task.Factory.StartNew(() => {
					new AlartListFileManager(false, this).save();
					new AlartListFileManager(true, this).save();
				});
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		void ReadNamarokuListMenuClick(object sender, EventArgs e)
		{
			var dialog = new OpenFileDialog();
			dialog.DefaultExt = ".ini";
			dialog.FileName = "favoritecom";
			dialog.Filter = "INI形式(*.ini)|*.ini*";
			
			dialog.Multiselect = false;
			var result = dialog.ShowDialog();
			if (result != DialogResult.OK) return;
			
			//存在チェック
			//Task.Factory.StartNew(() => new AlartListFileManager().ReadNamarokuList(this, alartListDataSource, dialog.FileName, true));
			Task.Factory.StartNew(() => {
				new AlartListFileManager(false, this).ReadNamarokuList(this, alartListDataSource, dialog.FileName, false, true);
				new AlartListFileManager(false, this).save();
			});
		}
		
		void AlartListColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			util.debugWriteLine("header " + e.ColumnIndex + " " + e.RowIndex + " " + e.Button);
			
			
			/*
			var dv = (DataView)dt.DefaultView;
			dv.Sort = "Column1, Column2 ASC";
				
			//a = alartListDataSource.SupportsSorting;
			*/
//			var a = alartListDataSource.SupportsSorting;
			/*
			var sortColumn = alartList.CurrentCell.OwningColumn;
			var sortDirection = ListSortDirection.Ascending;
			alartList.Sort(sortColumn, sortDirection);
			*/
			/*
		    if (alartList.SortedColumn != null &&
				    alartList.SortedColumn.Equals(sortColumn)) {
		        sortDirection = alartList.SortOrder == SortOrder.Ascending ?
		            	ListSortDirection.Descending : ListSortDirection.Ascending;
		    }
		    alartList.Sort(sortColumn, sortDirection);
			*/
		}
		void OpenLastHosoClick(object sender, EventArgs e)
		{
			var isUserMode = !favoriteCommunityBtn.Checked;
			var list = isUserMode ? userAlartList : alartList;
			var dataSource = isUserMode ? userAlartListDataSource : alartListDataSource;

			var selectedLvList = new List<string>();
			foreach (DataGridViewCell c in list.SelectedCells) {
				try {
					var ai = (AlartInfo)dataSource[c.RowIndex];
					if (ai.lastLvid == null || ai.lastLvid == "") continue;
					var url = "https://live.nicovideo.jp/watch/lv" + util.getRegGroup(ai.lastLvid, "(\\d+)");
					if (selectedLvList.IndexOf(url) > -1) continue;
					selectedLvList.Add(url);
				} catch (Exception ee) {
					util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
				}
			}
			if (selectedLvList.Count > 10 && MessageBox.Show(selectedLvList.Count + "の放送が選択されています。このまま開きますか？", "", MessageBoxButtons.YesNo) 
			    	!= DialogResult.Yes) return;
			foreach (var url in selectedLvList)
				util.openUrlBrowser(url, config);
		}
		void OpenCommunityUrlClick(object sender, EventArgs e)
		{
			var isUserMode = !favoriteCommunityBtn.Checked;
			var list = isUserMode ? userAlartList : alartList;
			var dataSource = isUserMode ? userAlartListDataSource : alartListDataSource;
			
			var selectedLvList = new List<string>();
			foreach (DataGridViewCell c in list.SelectedCells) {
				try {
					var ai = (AlartInfo)dataSource[c.RowIndex];
					if (ai.communityId == null || ai.communityId == "") continue;
					
					var isChannel = ai.communityId.IndexOf("ch") > -1;
					var url = (isChannel) ? 
						("https://ch.nicovideo.jp/" + ai.communityId) :
						("https://com.nicovideo.jp/community/" + ai.communityId);
					if (selectedLvList.IndexOf(url) > -1) continue;
					selectedLvList.Add(url);
				} catch (Exception ee) {
					util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
				}
			}
			if (selectedLvList.Count > 10 && MessageBox.Show(selectedLvList.Count + "の放送が選択されています。このまま開きますか？", "", MessageBoxButtons.YesNo) 
			    	!= DialogResult.Yes) return;
			foreach (var url in selectedLvList)
				util.openUrlBrowser(url, config);
		}
		void OpenUserUrlClick(object sender, EventArgs e)
		{
			var isUserMode = !favoriteCommunityBtn.Checked;
			var list = isUserMode ? userAlartList : alartList;
			var dataSource = isUserMode ? userAlartListDataSource : alartListDataSource;
			
			var selectedLvList = new List<string>();
			foreach (DataGridViewCell c in list.SelectedCells) {
				try {
					var ai = (AlartInfo)dataSource[c.RowIndex];
					if (ai.hostId == null || ai.hostId == "") continue;
					var url = "https://www.nicovideo.jp/user/" + ai.hostId;
					if (selectedLvList.IndexOf(url) > -1) continue;
					selectedLvList.Add(url);
				} catch (Exception ee) {
					util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
				}
			}
			if (selectedLvList.Count > 10 && MessageBox.Show(selectedLvList.Count + "の放送が選択されています。このまま開きますか？", "", MessageBoxButtons.YesNo) 
			    	!= DialogResult.Yes) return;
			foreach (var url in selectedLvList)
				util.openUrlBrowser(url, config);
		}
		
		void CopyLastHosoMenuClick(object sender, EventArgs e)
		{
			try {
				var isUserMode = !favoriteCommunityBtn.Checked;
				var list = isUserMode ? userAlartList : alartList;
				var dataSource = isUserMode ? userAlartListDataSource : alartListDataSource;
				
				var selectedLvList = new List<string>();
				foreach (DataGridViewCell c in list.SelectedCells) {
					try {
						var ai = (AlartInfo)dataSource[c.RowIndex];
						if (string.IsNullOrEmpty(ai.lastLvid)) continue;
						
						var url = "https://live.nicovideo.jp/watch/lv" + util.getRegGroup(ai.lastLvid, "(\\d+)");
						if (selectedLvList.IndexOf(url) > -1) continue;
						selectedLvList.Add(url);
					} catch (Exception ee) {
						util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
					}
				}
				Clipboard.SetText(string.Join("\n", selectedLvList.ToArray()));
				
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		
		void CopyCommunityUrlMenuClick(object sender, EventArgs e)
		{
			try {
				var isUserMode = !favoriteCommunityBtn.Checked;
				var list = isUserMode ? userAlartList : alartList;
				var dataSource = isUserMode ? userAlartListDataSource : alartListDataSource;
				
				var selectedLvList = new List<string>();
				foreach (DataGridViewCell c in list.SelectedCells) {
					try {
						var ai = (AlartInfo)dataSource[c.RowIndex];
						if (string.IsNullOrEmpty(ai.communityId)) continue;
						
						var isChannel = ai.communityId.IndexOf("ch") > -1;
						var url = (isChannel) ? 
								("https://ch.nicovideo.jp/" + ai.communityId) :
								("https://com.nicovideo.jp/community/" + ai.communityId);
						if (selectedLvList.IndexOf(url) > -1) continue;
						selectedLvList.Add(url);
					} catch (Exception ee) {
						util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
					}
				}
				Clipboard.SetText(string.Join("\n", selectedLvList.ToArray()));
				
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		
		void CopyUserUrlMenuClick(object sender, EventArgs e)
		{
			try {
				var isUserMode = !favoriteCommunityBtn.Checked;
				var list = isUserMode ? userAlartList : alartList;
				var dataSource = isUserMode ? userAlartListDataSource : alartListDataSource;
				
				var selectedLvList = new List<string>();
				foreach (DataGridViewCell c in list.SelectedCells) {
					try {
						var ai = (AlartInfo)dataSource[c.RowIndex];
						if (string.IsNullOrEmpty(ai.hostId)) continue;
						
						var url = "https://www.nicovideo.jp/user/" + ai.hostId;
						if (selectedLvList.IndexOf(url) > -1) continue;
						selectedLvList.Add(url);
					} catch (Exception ee) {
						util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
					}
				}
				Clipboard.SetText(string.Join("\n", selectedLvList.ToArray()));
				
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		
		void RemoveLineMenuClick(object sender, EventArgs e)
		{
			try {
				var isUserMode = !favoriteCommunityBtn.Checked;
				var list = isUserMode ? userAlartList : alartList;
				var dataSource = isUserMode ? userAlartListDataSource : alartListDataSource;
			
				if (list.SelectedCells.Count == 0) return;
				var selectedAIList = new List<AlartInfo>();
				foreach (DataGridViewCell c in list.SelectedCells) {
					try {
						var ai = (AlartInfo)dataSource[c.RowIndex];
						if (selectedAIList.IndexOf(ai) > -1) continue;
						else selectedAIList.Add(ai);
					} catch (Exception ee) {
						util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
					}
				}
				
				var ids = selectedAIList.Select(ai => 
						(!string.IsNullOrEmpty(ai.communityId)) ? 
						ai.communityId : 
						((!string.IsNullOrEmpty(ai.hostId)) ? ai.hostId : ai.keyword));
				var idsStr = string.Join("、", ids);
				
				
				var id = string.IsNullOrEmpty(idsStr) ? "こ" : (idsStr);
				if (System.Windows.Forms.MessageBox.Show(id + "の行を削除していいですか？", "確認", MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;
				foreach (var ai in selectedAIList)
					dataSource.Remove(ai);
				changedListContent();
				
				if (bool.Parse(config.get("delThumb"))) {
					while (true) {
						try {
							foreach (var ai in selectedAIList) {
								var isUserExists = string.IsNullOrEmpty(ai.hostId);
								var isCoChExists = string.IsNullOrEmpty(ai.communityId);
								foreach (var _ai in dataSource) {
									if (_ai.hostId == ai.hostId) 
										isUserExists = true;
									if (_ai.communityId == ai.communityId) 
										isCoChExists= true;
								}
								if (!isUserExists)
									ThumbnailManager.deleteImageId(ai.hostId);
								if (!isCoChExists)
									ThumbnailManager.deleteImageId(ai.communityId);
							}
							break;
						} catch (Exception ee) {
							util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
							Thread.Sleep(1000);
						}
					}
				}
			
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		
		void AlartListCellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (e.RowIndex == -1 && e.ColumnIndex != -1) {
				util.debugWriteLine("header click? " + alartList.SortOrder + " a " + alartList.SortedColumn + " ");
				if (e.Button == MouseButtons.Right) {
					var i = alartList.FirstDisplayedScrollingRowIndex;

					var bs = new BindingSource();
					bs.DataSource = alartListDataSource;
					bs.Sort = "";
		            alartList.DataSource = bs;
					            
		            try {
		            	if (i != -1)
		            		alartList.FirstDisplayedScrollingRowIndex = i;
		            	alartList.ClearSelection();
		            	//alartList.CurrentCell = null;
		            } catch (Exception ee) {
		            	util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
		            }
				}
			} else {
				var isUserMode = ((DataGridView)sender).Name == "userAlartList";
				var list = isUserMode ? userAlartList : alartList;
				setMouseDownSelect(list, e);
			}
		}
		void taskListCellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
		{
			setMouseDownSelect(taskList, e);
		}
		void LiveListCellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
		{
			setMouseDownSelect(liveList, e);
		}
		void LogListCellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
		{
			setMouseDownSelect(logList, e);
		}
		void setMouseDownSelect(DataGridView dgv, DataGridViewCellMouseEventArgs e) {
			try {
				if (e.Button == MouseButtons.Right) {
					var isInSelected = false;
					foreach (var c in dgv.SelectedCells)
						if (c == dgv[e.ColumnIndex, e.RowIndex])
							isInSelected = true;
					if (!isInSelected) {
						dgv.ClearSelection();
						dgv[e.ColumnIndex, e.RowIndex].Selected = true;
						dgv.CurrentCell = dgv[e.ColumnIndex, e.RowIndex];
					}
				}
				
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		public void setHosoLogStatusBar(RssItem item) {
			string buf = null;
			try {
				buf = "[" + DateTime.Parse(item.pubDate).ToString("yyyy\"/\"MM\"/\"dd HH\":\"mm\":\"ss") + "] 放送ID：" + item.lvId + " チャンネルID：" + item.comId + "　ユーザー名：" + item.hostName;
			} catch (Exception e) {
				util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
			}
			formAction(() => {
   		       	try {
					lastHosoStatusBar.Text = buf;
   		       	} catch (Exception e) {
   		       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
   		       	}
			});
		}
		public void sortAlartList(bool isUserMode) {
			var list = isUserMode ? userAlartList : alartList;
			var _order = (list.SortOrder == SortOrder.None) ? "none" : ((list.SortOrder == SortOrder.Ascending) ? "asc" : "des");
			util.debugWriteLine("sortAlartList " + _order + " isusermode " + isUserMode);
			
			var order = list.SortOrder;
			var column = list.SortedColumn;
			if (order == SortOrder.None) {
				if (config.get("alartListUpOnAirMode") != "0") {
					order = SortOrder.Ascending;
					foreach (DataGridViewColumn c in list.Columns)
						if (c.HeaderText == "最近の放送日時")
							column = c;
				} else return;
			}
			var direction = (order == SortOrder.Ascending) ? ListSortDirection.Ascending : ListSortDirection.Descending;
			formAction(() => {
	       		//var ret = 0;
   		       	try {
					list.Sort(column, direction);
   		       	} catch (Exception e) {
   		       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
   		       	}
			});
		}
		public void sortLiveList() {
			var order = (liveList.SortOrder == SortOrder.None) ? "none" : ((liveList.SortOrder == SortOrder.Ascending) ? "asc" : "des");
			util.debugWriteLine("sortLiveList " + order);
			
			if (liveList.SortOrder == SortOrder.None) return;
			var direction = (liveList.SortOrder == SortOrder.Ascending) ? ListSortDirection.Ascending : ListSortDirection.Descending;
			liveListLockAction(() => {
			    var sI = liveList.FirstDisplayedScrollingRowIndex;
				var dt = DateTime.Now;
				formAction(() => {
	   		       	try {
					    //resultLiveListSort();
					    
						liveList.Sort(liveList.SortedColumn, direction);
						
	   		       	} catch (Exception e) {
	   		       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
	   		       	}
				});
				setScrollIndex(liveList, sI);
				util.debugWriteLine("sortlivelist time " + (DateTime.Now - dt) + " scroll " + liveListScrollIndex);
			});
		}
		public void DisplayPopup(RssItem item, Point point, bool isSmall, 
				PopupDisplay pd, int showIndex, AlartInfo ai, 
				bool isTest = false, string poploc = null, int poptime = 0,
				bool isClickClose = true, bool isTopMost = true,
				Color[] isColor = null, double opacity = 0.9) {
			Form f = null;
			try {
   		        if (isTest) {
   		        	f = (isSmall) ? ((Form)new SmallPopupForm(item, config, pd, showIndex, ai, check.container, this, 
   		        	                                          isTest, poploc, poptime, isClickClose, isTopMost, isColor, opacity)) :
   		        		((Form)new PopupForm(item, config, pd, showIndex, ai, check.form, check.container, 
							isTest, poploc, poptime, isClickClose, isTopMost, isColor, opacity));
   		        } else {
					f = (isSmall) ? ((Form)new SmallPopupForm(item, config, pd, showIndex, ai, check.container, this, isTest)) : ((Form)new PopupForm(item, config, pd, showIndex, ai, this, check.container));
   		        }
				f.Location = point;
			} catch (Exception e) {
				util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
			}
			//formAction(() => {
   		       	try {
					f.Show(this);
   		       	} catch (Exception e) {
   		       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
   		       	}
			//});

		}
		public void DisplayBalloon(RssItem item, AlartInfo ai) {
			try {
	       		if (this.IsDisposed) return;
	       		if (!this.IsHandleCreated) return;
	       		//var ret = 0;
	       		//var tipTitle = ((item.isMemberOnly) ? "(限定)" : "") + item.comName;
	       		var tipTitle = item.comName;
				var url = "https://live.nicovideo.jp/watch/" + item.lvId;
				var content = url;
				if (ai != null && ai.keyword != null && ai.keyword != "") content = ai.keyword + "-" + url;
				
				var _balloonIcon = new Icon(util.getJarPath()[0] + "/Icon/lock.ico");
				_balloonIcon = util.changeIconColor(_balloonIcon, ColorTranslator.FromHtml(config.get("onlyIconColor")));
				var balloonIcon = _balloonIcon.ToBitmap().GetHicon();
				//var balloonIcon = new Bitmap(new System.Drawing.Imageutil.getJarPath()[0] + "/Icon/lock.ico"), new Size(16,16)).GetHicon();
				//var balloonIcon = new Icon(util.getJarPath()[0] + "/Icon//a.ico").ToBitmap().GetHicon();
				//var balloonIcon = new Icon("a.ico").ToBitmap().GetHicon();
				
				var handle = Handle;
				notifyUrl = url;
				formAction(() => {
					try {
						
						//var icon = item.isMemberOnly ? ToolTipIcon.Info : ToolTipIcon.None;
						if (item.isMemberOnly) {
							lastIconBalloonTime = DateTime.Now;
							if (!isDisplayIconBalloon) {
								var r = util.addNotifyIcon(handle);
								if (!r) return;
								isDisplayIconBalloon = true;
							}
							var _r = util.displayIconBalloon(tipTitle, content, balloonIcon, handle);
						} else {
							lastBalloonTime = DateTime.Now;
							//return;
							notifyIcon.ShowBalloonTip(10000, tipTitle, content, ToolTipIcon.None);
						}
						
						
       		       	} catch (Exception e) {
       		       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
       		       	}
				});
				try {
					if (item.isMemberOnly) {
						Task.Factory.StartNew(() => {
				         	Thread.Sleep(30000);
				         	isDisplayIconBalloon = false;
				         	if (DateTime.Now - lastIconBalloonTime > TimeSpan.FromSeconds(29)) 
				         		formAction(() => util.deleteNotifyIcon(handle));
						});
					} else {
						Task.Factory.StartNew(() => {
				         	Thread.Sleep(30000);
				         	if (DateTime.Now - lastBalloonTime > TimeSpan.FromSeconds(30)) 
				         		formAction(() => closeBalloon());
						});
					}
				} catch (Exception e) {
					util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
				}
					
				/*
	        	this.BeginInvoke((MethodInvoker)delegate() {
       		       	try {
						var handle = Handle;
						notifyUrl = url;
						//var icon = item.isMemberOnly ? ToolTipIcon.Info : ToolTipIcon.None;
						if (item.isMemberOnly) {
							lastIconBalloonTime = DateTime.Now;
							if (!isDisplayIconBalloon) {
								var r = util.addNotifyIcon(handle);
								if (!r) return;
								isDisplayIconBalloon = true;
							}
							var _r = util.displayIconBalloon(tipTitle, content, balloonIcon, handle);
							Task.Factory.StartNew(() => {
					         	Thread.Sleep(30000);
					         	isDisplayIconBalloon = false;
					         	if (DateTime.Now - lastIconBalloonTime > TimeSpan.FromSeconds(29)) 
					         		util.deleteNotifyIcon(handle);
							});
							
						} else {
							lastBalloonTime = DateTime.Now;
							//return;
							notifyIcon.ShowBalloonTip(10000, tipTitle, content, ToolTipIcon.None);
							Task.Factory.StartNew(() => {
					         	Thread.Sleep(30000);
					         	if (DateTime.Now - lastBalloonTime > TimeSpan.FromSeconds(30)) closeBalloon();
							});
						}
						
						
       		       	} catch (Exception e) {
       		       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
       		       	}
				});
				*/
	       		return;
	       	} catch (Exception e) {
	       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
	       	}
		}
		private void closeBalloon() {
			try {
				formAction(() => {
				    try {      
						notifyIcon.Visible = false;
						notifyIcon.Visible = true;
       		       	} catch (Exception e) {
       		       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
       		       	}  	
				});
				/*
	       		if (this.IsDisposed) return;
	       		if (!this.IsHandleCreated) return;
	        	this.BeginInvoke((MethodInvoker)delegate() {
       		       	try {      
						notifyIcon.Visible = false;
						notifyIcon.Visible = true;
       		       	} catch (Exception e) {
       		       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
       		       	}
				});
				*/
	       		return;
	       	} catch (Exception e) {
	       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
	       	}
		}
		void CloseNotifyIconMenuClick(object sender, EventArgs e)
		{
			//Close();
			kakuninClose();
			Application.Exit();
		}
		
		void NotifyIconDoubleClick(object sender, EventArgs e)
		{
			activateForm();
		}
		void activateForm() {
			Visible = true;
			ShowInTaskbar = true;
			if (WindowState == FormWindowState.Minimized) {
				WindowState = FormWindowState.Normal;
			}
			Activate();
			
		}
		void OpenNotifyIconMenuClick(object sender, EventArgs e)
		{
			activateForm();
		}
		private void setDoubleBuffered(Control dgv) {
			typeof(Control).
			    GetProperty("DoubleBuffered",
			        System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).
						SetValue(dgv, true, null);
		}
		void AlartListCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			//util.debugWriteLine("value " + alartList[7, e.RowIndex].Value);
			//util.debugWriteLine("alartlist cellformatting " + e.ColumnIndex + " " + e.RowIndex);
			//if (keikaTime < TimeSpan.FromSeconds(30)) {
			
			//if (e.ColumnIndex == 7 && alartListDataSource[e.RowIndex].lastHosoDt != DateTime.MinValue &&
			//	   	DateTime.Now - alartListDataSource[e.RowIndex].lastHosoDt < TimeSpan.FromMinutes(30)) {
			AlartListCellFormattingCommon(alartListDataSource, alartList, e);
			/*
			if (e.ColumnIndex == 8 && alartListDataSource[e.RowIndex].recentColorMode != 0) {
				Color color;
				if (recentColor == Color.Empty) {
					color = alartListDataSource[e.RowIndex].backColor;
				} else {
					if (alartListDataSource[e.RowIndex].recentColorMode == 1)
						color = recentColor;
					else {
						color = (followerOnlyColor == Color.Empty) ?
								recentColor : followerOnlyColor;
					}
				}
				alartList[8, e.RowIndex].Style.BackColor =　color; 
				
			} else if (e.ColumnIndex == 0 && alartListDataSource[e.RowIndex].comIdColorType != 0) {
				e.CellStyle.BackColor = (alartListDataSource[e.RowIndex].comIdColorType == 1) ? 
					Color.FromArgb(169, 169, 169) : Color.FromArgb(255, 255, 190);
			} else if (e.ColumnIndex == 1 && alartListDataSource[e.RowIndex].userIdColorType != 0) {
				e.CellStyle.BackColor = (alartListDataSource[e.RowIndex].userIdColorType == 1) ? 
					Color.FromArgb(169, 169, 169) : Color.FromArgb(255, 255, 190);
			} else if (e.ColumnIndex == 6 || e.ColumnIndex == 7) {
				var b = (DataGridViewButtonCell)alartList[e.ColumnIndex, e.RowIndex];
				Color c;
				if (e.Value == "") c = Color.White;
				else if (b.ReadOnly) {
					c = Color.FromArgb(200,200,200);
				} else {
					c = e.Value == "フォローする" ? Color.FromArgb(224,244,224) : Color.FromArgb(224,224,224);
				}
				b.Style.BackColor = c;
			} else if (alartListColorColumns[e.ColumnIndex]) {
				e.CellStyle.BackColor = alartListDataSource[e.RowIndex].backColor;
				e.CellStyle.ForeColor = alartListDataSource[e.RowIndex].textColor;
			} else {
				e.CellStyle.BackColor = (e.RowIndex % 2 != 0) ? 
						Color.FromArgb(245, 245, 245)
						: Color.FromName("window");
			}
			*/
			
		}
		void TaskListCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			//util.debugWriteLine("value " + alartList[7, e.RowIndex].Value);
			//util.debugWriteLine("alartlist cellformatting " + e.ColumnIndex + " " + e.RowIndex);
			//if (keikaTime < TimeSpan.FromSeconds(30)) {
			e.CellStyle.BackColor = (e.RowIndex % 2 != 0) ? 
					evenRowsColor
					: Color.FromName("window");
			
		}
		void LiveListCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			/*
			var a = e.Value;
			if (e.ColumnIndex == 2) {
				e.CellStyle.BackColor = liveListDataSource[e.RowIndex].backColor;
				e.CellStyle.ForeColor = liveListDataSource[e.RowIndex].textColor;
			}
			*/
			//var hi = liveListDataSource[e.RowIndex];
			//var style = liveList[e.ColumnIndex, e.RowIndex].Style;
			
			if (liveListColorColumns[e.ColumnIndex]) {
				e.CellStyle.BackColor = liveListDataSource[e.RowIndex].backColor;
				e.CellStyle.ForeColor = liveListDataSource[e.RowIndex].textColor;
			} else {
				e.CellStyle.BackColor = (e.RowIndex % 2 != 0) ? 
					evenRowsColor
					: Color.FromName("window");
			}
			
		}
		public void alartListRemove(AlartInfo ai, bool isUserMode) {
			try {
				formAction(() => {
					try {
	       		        if (isUserMode)
							userAlartListDataSource.Remove(ai);
	       				else alartListDataSource.Remove(ai);
       		       	} catch (Exception e) {
       		       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
       		       	}
				});
				/*
	       		if (this.IsDisposed) return;
	       		if (!this.IsHandleCreated) return;
	        	this.BeginInvoke((MethodInvoker)delegate() {
       		       	try {
	       		        if (isUserMode)
							userAlartListDataSource.Remove(ai);
	       				else alartListDataSource.Remove(ai);
       		       	} catch (Exception e) {
       		       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
       		       	}
				});
				*/
	       	} catch (Exception e) {
	       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
	       	}
		}
		public void alartListAdd(AlartInfo ai, bool isUserMode) {
			try {
				formAction(() => {
					try {
	       		        if (isUserMode) userAlartListDataSource.Add(ai);
						else alartListDataSource.Add(ai);
						
       		       	} catch (Exception e) {
       		       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
       		       	}
				});
				/*
	       		if (this.IsDisposed) return;
	       		if (!this.IsHandleCreated) return;
	        	this.BeginInvoke((MethodInvoker)delegate() {
       		       	try {
	       		        if (isUserMode) userAlartListDataSource.Add(ai);
						else alartListDataSource.Add(ai);
						//alartListDataSource.Add(ai);
       		       	} catch (Exception e) {
       		       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
       		       	}
				});
				*/
	       	} catch (Exception e) {
	       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
	       	}
		}
		public void taskListAdd(TaskInfo ti) {
			try {
				formAction(() => {
					try {
						taskListDataSource.Add(ti);
       		       	} catch (Exception e) {
       		       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
       		       	}
				});
				/*
	       		if (this.IsDisposed) return;
	       		if (!this.IsHandleCreated) return;
	        	this.BeginInvoke((MethodInvoker)delegate() {
       		       	try {
						taskListDataSource.Add(ti);
       		       	} catch (Exception e) {
       		       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
       		       	}
				});
				*/
	       	} catch (Exception e) {
	       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
	       	}
		}
		public void HistoryListAdd(HistoryInfo hi) {
			try {
				foreach (var _hi in historyListDataSource)
					if (_hi.lvid == hi.lvid) return;
				formAction(() => historyListDataSource.Add(hi));
			} catch (Exception e) {
	       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
	       	}
		}
		public void NotHistoryListAdd(HistoryInfo hi) {
			try {
				foreach (var _hi in notAlartListDataSource)
					if (_hi.lvid == hi.lvid) return;
				formAction(() => notAlartListDataSource.Add(hi));
			} catch (Exception e) {
	       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
	       	}
		}
		public void setAlartListScrollIndex(int i, bool isUserMode) {
			try {
				formAction(() => {
					try {
						if (isUserMode) {
							userAlartList.FirstDisplayedScrollingRowIndex = i;
							userAlartList.CurrentCell = userAlartList[0, i];
	            		} else {
	            			alartList.FirstDisplayedScrollingRowIndex = i;
							alartList.CurrentCell = alartList[0, i];
	            		}
       		       	} catch (Exception e) {
       		       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
       		       	}
				});
				/*
	       		if (this.IsDisposed) return;
	       		if (!this.IsHandleCreated) return;
	        	this.BeginInvoke((MethodInvoker)delegate() {
       		       	try {
						if (isUserMode) {
							userAlartList.FirstDisplayedScrollingRowIndex = i;
							userAlartList.CurrentCell = userAlartList[0, i];
	            		} else {
	            			alartList.FirstDisplayedScrollingRowIndex = i;
							alartList.CurrentCell = alartList[0, i];
	            		}
       		       	} catch (Exception e) {
       		       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
       		       	}
				});
				*/
	       	} catch (Exception e) {
	       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
	       	}
		}
		public DialogResult showMessageBox(string text, string caption = "", MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.None, MessageBoxDefaultButton defBtn = MessageBoxDefaultButton.Button1) {
			var ret = DialogResult.Cancel;
			formAction(() => {
		        try {
					ret = System.Windows.Forms.MessageBox.Show(text, caption, buttons, icon, defBtn);
   		       	} catch (Exception e) {
   		       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
   		       	}
			}, -1);
			return ret;
			/*
			try {
	       		if (this.IsDisposed) return DialogResult.Cancel;
	       		if (!this.IsHandleCreated) return DialogResult.Cancel;
	       		 
	        	this.BeginInvoke((MethodInvoker)delegate() {
       		       	try {
						ret = System.Windows.Forms.MessageBox.Show(text, caption, buttons, icon, defBtn);
       		       	} catch (Exception e) {
       		       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
       		       	}
				});
	       		return ret;
	       	} catch (Exception e) {
	       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
	       	}
			return ret;
			*/
		}
		void TaskListCellValidating(object sender, DataGridViewCellValidatingEventArgs e)
		{
			if (e.ColumnIndex == 0) {
				DateTime dt;
				if (!DateTime.TryParse(e.FormattedValue.ToString(), out dt)) {
					System.Windows.Forms.MessageBox.Show("有効な日時ではありません");
					e.Cancel = true;
				}
			}
		}
		
		
		void TaskListCurrentCellDirtyStateChanged(object sender, System.EventArgs e)
		{
			try {
				var cc = taskList.CurrentCell;
				if (cc is DataGridViewCheckBoxCell) {      
			        if (cc.ColumnIndex >= 5 && cc.ColumnIndex <= 20) {
						taskList.CommitEdit(DataGridViewDataErrorContexts.Commit);
						var target = ((TaskInfo)taskListDataSource[cc.RowIndex]);
						if (cc.ColumnIndex == 5) target.popup = (bool)(taskList[cc.ColumnIndex, cc.RowIndex].Value);
						if (cc.ColumnIndex == 6) target.baloon = (bool)(taskList[cc.ColumnIndex, cc.RowIndex].Value);
						if (cc.ColumnIndex == 7) target.browser = (bool)(taskList[cc.ColumnIndex, cc.RowIndex].Value);
						if (cc.ColumnIndex == 8) target.mail = (bool)(taskList[cc.ColumnIndex, cc.RowIndex].Value);
						if (cc.ColumnIndex == 9) target.sound = (bool)(taskList[cc.ColumnIndex, cc.RowIndex].Value);
						if (cc.ColumnIndex == 10) target.appliA = (bool)(taskList[cc.ColumnIndex, cc.RowIndex].Value);
						if (cc.ColumnIndex == 11) target.appliB = (bool)(taskList[cc.ColumnIndex, cc.RowIndex].Value);
						if (cc.ColumnIndex == 12) target.appliC = (bool)(taskList[cc.ColumnIndex, cc.RowIndex].Value);
						if (cc.ColumnIndex == 13) target.appliD = (bool)(taskList[cc.ColumnIndex, cc.RowIndex].Value);
						if (cc.ColumnIndex == 14) target.appliE = (bool)(taskList[cc.ColumnIndex, cc.RowIndex].Value);
						if (cc.ColumnIndex == 15) target.appliF = (bool)(taskList[cc.ColumnIndex, cc.RowIndex].Value);
						if (cc.ColumnIndex == 16) target.appliG = (bool)(taskList[cc.ColumnIndex, cc.RowIndex].Value);
						if (cc.ColumnIndex == 17) target.appliH = (bool)(taskList[cc.ColumnIndex, cc.RowIndex].Value);
						if (cc.ColumnIndex == 18) target.appliI = (bool)(taskList[cc.ColumnIndex, cc.RowIndex].Value);
						if (cc.ColumnIndex == 19) target.appliJ = (bool)(taskList[cc.ColumnIndex, cc.RowIndex].Value);
						if (cc.ColumnIndex == 20) target.isDelete = (bool)(taskList[cc.ColumnIndex, cc.RowIndex].Value);
	//					util.debugWriteLine(target.appliA);
					}
				}
				Task.Factory.StartNew(() =>
						new TaskListFileManager().save(this));
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		void TaskListCellParsing(object sender, DataGridViewCellParsingEventArgs e)
		{
			util.debugWriteLine("cell parsing " + e.ColumnIndex);
			var target = ((TaskInfo)taskListDataSource[e.RowIndex]);
			if (e.ColumnIndex == 21) target.memo = (string)e.Value;
			util.debugWriteLine("cell parcing");
			//changedListContent();
			Task.Factory.StartNew(() =>
					new TaskListFileManager().save(this));
		}
		void TaskListOpenUrlMenuClick(object sender, EventArgs e)
		{
			
			try {
				var selectedLvList = new List<string>();
				foreach (DataGridViewCell c in taskList.SelectedCells) {
					var ti = (TaskInfo)taskListDataSource[c.RowIndex];
					var url = "https://live.nicovideo.jp/watch/" + ti.lvId;
					if (selectedLvList.IndexOf(url) > -1) continue;
					selectedLvList.Add(url);
				}
				if (selectedLvList.Count > 10 && MessageBox.Show(selectedLvList.Count + "の放送が選択されています。このまま開きますか？", "", MessageBoxButtons.YesNo) 
				    	!= DialogResult.Yes) return;
				foreach (var url in selectedLvList)
					util.openUrlBrowser(url, config);
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		
		void TaskListCopyUrlMenuClick(object sender, EventArgs e)
		{
			try {
				var selectedRowUrl = new List<string>();
				foreach (DataGridViewCell c in taskList.SelectedCells) {
					try {
						var ti = (TaskInfo)taskListDataSource[c.RowIndex];
						//if (string.IsNullOrEmpty(ti.lvId)) continue;
						var url = "https://live.nicovideo.jp/watch/" + ti.lvId;
						//if (selectedRowUrl.IndexOf(url) > -1) continue;
						selectedRowUrl.Add(url);
					} catch (Exception ee) {
						util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace);
					}
				}
				Clipboard.SetText(string.Join("\n", selectedRowUrl.ToArray()));
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		
		void TaskListCopyArgsMenuClick(object sender, EventArgs e)
		{
			try {
				var selectedRowUrl = new List<string>();
				foreach (DataGridViewCell c in taskList.SelectedCells) {
					try {
						var ti = (TaskInfo)taskListDataSource[c.RowIndex];
						//if (string.IsNullOrEmpty(ti.args)) continue;
						var url = ti.args == null ? "" : ti.args;
						//if (selectedRowUrl.IndexOf(url) > -1) continue;
						selectedRowUrl.Add(url);
					} catch (Exception ee) {
						util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace);
					}
				}
				Clipboard.SetText(string.Join("\n", selectedRowUrl.ToArray()));
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		
		void TaskListRemoveLineMenuClick(object sender, EventArgs e)
		{
			try {
				if (taskList.SelectedCells.Count == 0) return;
				var selectedTIList = new List<TaskInfo>();
				foreach (DataGridViewCell c in taskList.SelectedCells) {
					try {
						var ai = (TaskInfo)taskListDataSource[c.RowIndex];
						if (selectedTIList.IndexOf(ai) > -1) continue;
						else selectedTIList.Add(ai);
					} catch (Exception ee) {
						util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
					}
				}
				
				var ids = selectedTIList.Select(ti => ti.lvId);
				var idsStr = string.Join("、", ids);
				
				var id = string.IsNullOrEmpty(idsStr) ? "こ" : (idsStr);
				if (System.Windows.Forms.MessageBox.Show(id + "の行を削除していいですか？", "確認", MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;
				foreach (var ti in selectedTIList)
					taskListDataSource.Remove(ti);
				changedListContent();
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		public void taskListRemoveLine(TaskInfo ti) {
			try {
				formAction(() => {
					try {
						taskListDataSource.Remove(ti);
       		       	} catch (Exception e) {
       		       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
       		       	}
				});
	       	} catch (Exception e) {
	       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
	       	}
		}
		public void taskListUpdateState(TaskInfo ti) {
			try {
				formAction(() => {
					try {
	       		    	ti.status = "完了";
	       		    	if (taskListDataSource.IndexOf(ti) != -1)
	       		    		taskList.UpdateCellValue(4, taskListDataSource.IndexOf(ti));
       		       	} catch (Exception e) {
       		       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
       		       	}
				});
	       		changedListContent();
	       	} catch (Exception e) {
	       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
	       	}
		}
		/*
		void IsDisplayCommunityIdTabMenuClick(object sender, EventArgs e)
		{
			changeColumnVisible(0);
		}
		void IsDisplayUserIdTabMenuClick(object sender, EventArgs e)
		{
			changeColumnVisible(1);
		}
		void IsDisplayCommunityNameTabMenuClick(object sender, EventArgs e)
		{
			changeColumnVisible(2);
		}
		void IsDisplayUserNameTabMenuClick(object sender, EventArgs e)
		{
			changeColumnVisible(3);
		}
		void IsDisplayKeywordTabMenuClick(object sender, EventArgs e)
		{
			changeColumnVisible(4);
		}
		void IsDisplayCommunityFollowTabMenuClick(object sender, EventArgs e)
		{
			changeColumnVisible(5);
		}
		void IsDisplayUserFollowTabMenuClick(object sender, EventArgs e)
		{
			changeColumnVisible(6);
		}
		void IsDisplayLastHosoDtTabMenuClick(object sender, EventArgs e)
		{
			changeColumnVisible(7);
		}
		void IsDisplayAddDateDtTabMenuClick(object sender, EventArgs e)
		{
			changeColumnVisible(8);
		}
		void IsDisplayPopupTabMenuClick(object sender, EventArgs e)
		{
			changeColumnVisible(9);
		}
		void IsDisplayBalloonTabMenuClick(object sender, EventArgs e)
		{
			changeColumnVisible(10);
		}
		void IsDisplayWebTabMenuClick(object sender, EventArgs e)
		{
			changeColumnVisible(11);
		}
		void IsDisplayMailTabMenuClick(object sender, EventArgs e)
		{
			changeColumnVisible(12);
		}
		void IsDisplaySoundTabMenuClick(object sender, EventArgs e)
		{
			changeColumnVisible(13);
		}
		void IsDisplayAppliATabMenuClick(object sender, EventArgs e)
		{
			changeColumnVisible(14);
		}
		void IsDisplayAppliBTabMenuClick(object sender, EventArgs e)
		{
			changeColumnVisible(15);
		}
		void IsDisplayAppliCTabMenuClick(object sender, EventArgs e)
		{
			changeColumnVisible(16);
		}
		void IsDisplayAppliDTabMenuClick(object sender, EventArgs e)
		{
			changeColumnVisible(17);
		}
		void IsDisplayAppliETabMenuClick(object sender, EventArgs e)
		{
			changeColumnVisible(18);
		}
		void IsDisplayAppliFTabMenuClick(object sender, EventArgs e)
		{
			changeColumnVisible(19);
		}
		void IsDisplayAppliGTabMenuClick(object sender, EventArgs e)
		{
			changeColumnVisible(20);
		}
		void IsDisplayAppliHTabMenuClick(object sender, EventArgs e)
		{
			changeColumnVisible(21);
		}
		void IsDisplayAppliITabMenuClick(object sender, EventArgs e)
		{
			changeColumnVisible(22);
		}
		void IsDisplayAppliJTabMenuClick(object sender, EventArgs e)
		{
			changeColumnVisible(23);
		}
		void IsDisplayMemoTabMenuClick(object sender, EventArgs e)
		{
			changeColumnVisible(24);
		}
		
		void changeColumnVisible(int columnIndex) {
			alartList.Columns[columnIndex].Visible = !alartList.Columns[columnIndex].Visible;
			var menu = (ToolStripMenuItem)displayFavoriteTabMenu.DropDownItems[columnIndex];
			menu.Checked = alartList.Columns[columnIndex].Visible;
		}
		*/
		void applyMenuSetting() {
			var columns = new string[] {"ShowComId",
					"ShowUserId","ShowComName","ShowUserName",
					"ShowKeyword","ShowIsAnd","ShowComFollow","ShowUserFollow",
					"ShowLatestTime","ShowRegistTime","ShowPop",
					"ShowBalloon","ShowWeb", "ShowMail", "ShowSound","ShowAppA",
					"ShowAppB","ShowAppC","ShowAppD","ShowAppE",
					"ShowAppF","ShowAppG","ShowAppH","ShowAppI",
					"ShowAppJ","ShowSoundType","ShowMemo"};
			for(var i = 0; i < columns.Length; i++) {
				var isDisplay = bool.Parse(config.get(columns[i]));
				if (i == 5) isDisplay = false;
				alartList.Columns[i].Visible = isDisplay;
				var menu = (ToolStripMenuItem)displayFavoriteTabMenu.DropDownItems[i];
				menu.Checked = isDisplay;
				
				if (i == 0 || i == 2 || i == 6 || i == 4) isDisplay = false;
				userAlartList.Columns[i].Visible = isDisplay;
			}
			var taskColumns = new string[] {
					"ShowTaskStartDt","ShowTaskLvid",
					"ShowTaskArgs","ShowTaskAddDt",
					"ShowTaskStatus","ShowTaskPopup",
					"ShowTaskBalloon","ShowTaskWeb",
					"ShowTaskMail", 
					"ShowTaskSound","ShowTaskAppliA",
					"ShowTaskAppliB","ShowTaskAppliC",
					"ShowTaskAppliD","ShowTaskAppliE",
					"ShowTaskAppliF","ShowTaskAppliG",
					"ShowTaskAppliH","ShowTaskAppliI",
					"ShowTaskAppliJ", "ShowTaskDelete", 
					"ShowTaskMemo"};
			for(var i = 0; i < taskColumns.Length; i++) {
				var isDisplay = bool.Parse(config.get(taskColumns[i]));
				taskList.Columns[i].Visible = isDisplay;
				var menu = (ToolStripMenuItem)displayTaskTabMenu.DropDownItems[i];
				menu.Checked = isDisplay;
			}
			var showLiveListColumns = config.get("ShowLiveColumns");
			for(var i = 0; i < liveList.Columns.Count - 1; i++) {
				liveList.Columns[i + 1].Visible = showLiveListColumns[i] == '1';
				var menu = (ToolStripMenuItem)displayOnAirTabMenu.DropDownItems[i];
				menu.Checked = liveList.Columns[i + 1].Visible;
			}
			
			var showHistoryListColumns = config.get("ShowHistoryColumns");
			if (showHistoryListColumns.Length == 10) 
				showHistoryListColumns = showHistoryListColumns.Insert(8, "0");
			for(var i = 0; i < historyList.Columns.Count; i++) {
				//util.debugWriteLine("test " + historyList.Columns[i].Name);
				historyList.Columns[i].Visible = showHistoryListColumns[i] == '1';
				var menu = (ToolStripMenuItem)displayHistoryListMenu.DropDownItems[i];
				menu.Checked = historyList.Columns[i].Visible;
			}
			
			var showNotAlartListColumns = config.get("ShowNotAlartColumns");
			if (showNotAlartListColumns.Length == 10) 
				showNotAlartListColumns = showNotAlartListColumns.Insert(8, "1");
			for(var i = 0; i < notAlartList.Columns.Count; i++) {
				notAlartList.Columns[i].Visible = showNotAlartListColumns[i] == '1';
				var menu = (ToolStripMenuItem)displayNotAlartListMenu.DropDownItems[i];
				menu.Checked = notAlartList.Columns[i].Visible;
			}
			
			var showReserveHistoryListColumns = config.get("ShowReserveHistoryColumns");
			for(var i = 0; i < reserveHistoryList.Columns.Count; i++) {
				var menu = (ToolStripMenuItem)displayReserveHistoryListMenu.DropDownItems[i];
				menu.Checked = showReserveHistoryListColumns[i] == '1';
				foreach (DataGridViewColumn c in reserveHistoryList.Columns)
					if (c.HeaderText == menu.Text)
						c.Visible = showReserveHistoryListColumns[i] == '1';
			}
			
			//更新メニュー
			var min = new int[]{0, 5, 10, 15, 20, 30, 60, 360};
			var delMin = int.Parse(config.get("liveListDelMinutes"));
			for (var i = 0; i < updateAutoDeleteMenu.DropDownItems.Count; i++) {
				((ToolStripMenuItem)updateAutoDeleteMenu.DropDownItems[i]).Checked =
					min[i] == delMin;
			}
			
			var cateType = int.Parse(config.get("cateCategoryType"));
			for (var i = 0; i < updateCateCategoryMenu.DropDownItems.Count; i++) {
				((ToolStripMenuItem)updateCateCategoryMenu.DropDownItems[i]).Checked =
					i == cateType;
			}
			
			updateAutoUpdateFirstMenu.Checked = bool.Parse(config.get("AutoStart"));
			
			updateTopFavoriteMenu.Checked = bool.Parse(config.get("FavoriteUp"));
			updateOnlyFavoriteMenu.Checked = bool.Parse(config.get("FavoriteOnly"));
			updateAutoSortMenu.Checked = bool.Parse(config.get("AutoSort"));
			updateHideMemberOnlyWithoutFavoriteMenu.Checked = bool.Parse(config.get("BlindOnlyA"));
			if (!updateHideMemberOnlyWithoutFavoriteMenu.Checked)
				updateHideMemberOnlyWithFavoriteMenu.Checked = bool.Parse(config.get("BlindOnlyB"));
			updateHideQuestionCategoryMenu.Checked = bool.Parse(config.get("BlindQuestion"));
			
			//alartlist color
			alartListColorColumns = new bool[alartList.Columns.Count];
			var colorColumn = config.get("ColorAlartListColumns");
			for (var i = 0; i < colorColumn.Length; i++) {
				var _check = colorColumn[i] == '1';
				alartListColorColumns[i] = _check;
				if (_check)
					((ToolStripMenuItem)colorColumnMenu.DropDownItems[i]).Checked = true;
			}
			
			//historylist color
			historyListColorColumns = new bool[historyList.Columns.Count];
			colorColumn = config.get("ColorHistoryListRecentColumns");
			for (var i = 0; i < colorColumn.Length; i++) {
				var _check = colorColumn[i] == '1';
				historyListColorColumns[i] = _check;
				if (_check)
					((ToolStripMenuItem)colorHistoryColorColumnMenu.DropDownItems[i]).Checked = true;
			}
			
			//livelist color
			try {
				liveListColorColumns = new bool[liveList.Columns.Count];
				colorColumn = config.get("ColorLiveListColumns");
				for (var i = 0; i < liveList.Columns.Count; i++) {
					var _c = liveList.Columns[i];
					var _check = colorColumn[i] == '1';
					liveListColorColumns[i] = _check;
					
					var item = new ToolStripMenuItem(_c.HeaderText);
					if (_check) item.Checked = true;
					if (!_c.Visible) item.Visible = false;
					
					liveListColorColumnMenu.DropDownItems.Add(item);
				}
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
			
			disableFollowMenu.Checked = bool.Parse(config.get("disableFollowColumns"));
			alartListUpOnAirMenu.Checked = config.get("alartListUpOnAirMode") != "0";
		}
		void saveMenuSetting() {
			var setting = new Dictionary<string, string>();
			var columns = new string[] {"ShowComId",
					"ShowUserId","ShowComName","ShowUserName",
					"ShowKeyword","ShowIsAnd","ShowComFollow","ShowUserFollow",
					"ShowLatestTime","ShowRegistTime","ShowPop",
					"ShowBalloon","ShowWeb","ShowMail","ShowSound","ShowAppA",
					"ShowAppB","ShowAppC","ShowAppD","ShowAppE",
					"ShowAppF","ShowAppG","ShowAppH","ShowAppI",
					"ShowAppJ","ShowSoundType","ShowMemo"};
			for(var i = 0; i < columns.Length; i++) {
				setting.Add(columns[i], alartList.Columns[i].Visible.ToString().ToLower());
			}
			
			var taskColumns = new string[] {
					"ShowTaskStartDt","ShowTaskLvid",
					"ShowTaskArgs","ShowTaskAddDt",
					"ShowTaskStatus","ShowTaskPopup",
					"ShowTaskBalloon","ShowTaskWeb",
					"ShowTaskMail",
					"ShowTaskSound","ShowTaskAppliA",
					"ShowTaskAppliB","ShowTaskAppliC",
					"ShowTaskAppliD","ShowTaskAppliE",
					"ShowTaskAppliF","ShowTaskAppliG",
					"ShowTaskAppliH","ShowTaskAppliI",
					"ShowTaskAppliJ", "ShowTaskDelete",
					"ShowTaskMemo"};
			for(var i = 0; i < taskColumns.Length; i++) {
				setting.Add(taskColumns[i], taskList.Columns[i].Visible.ToString().ToLower());
			}
			
			var buf = "";
			for(var i = 0; i < liveList.Columns.Count - 1; i++) {
				buf += liveList.Columns[i + 1].Visible ? "1" : "0";
			}
			setting.Add("ShowLiveColumns", buf);
			
			buf = "";
			for(var i = 0; i < historyList.Columns.Count; i++) {
				buf += historyList.Columns[i].Visible ? "1" : "0";
			}
			setting.Add("ShowHistoryColumns", buf);
			
			buf = "";
			for(var i = 0; i < notAlartList.Columns.Count; i++) {
				buf += notAlartList.Columns[i].Visible ? "1" : "0";
			}
			setting.Add("ShowNotAlartColumns", buf);
			
			buf = "";
			for(var i = 0; i < reserveHistoryList.Columns.Count; i++) {
				var t = displayReserveHistoryListMenu.DropDownItems[i].Text;
				foreach (DataGridViewColumn c in reserveHistoryList.Columns)
					if (c.HeaderText == t) buf += c.Visible ? "1" : "0";
			}
			setting.Add("ShowReserveHistoryColumns", buf);
			
			var notifyOffs = new string[] {
					"OffPop","OffBalloon",
					"OffWeb","OffMail","OffSound","OffAppA",
					"OffAppB","OffAppC","OffAppD",
					"OffAppE","OffAppF","OffAppG",
					"OffAppH","OffAppI","OffAppJ"};
			for(var i = 0; i < notifyOffs.Length; i++) {
				setting.Add(notifyOffs[i], notifyOffList[i + 2].ToString().ToLower());
			}
			config.saveFromForm(setting);
			
			//update
			config.set("AutoStart", updateAutoUpdateFirstMenu.Checked.ToString().ToLower());
		}
		void IsAlartListDisplayTabMenuClick(object sender, EventArgs e)
		{
			var i = displayFavoriteTabMenu.DropDownItems.IndexOf((ToolStripMenuItem)sender);
			alartList.Columns[i].Visible = !alartList.Columns[i].Visible;
			((ToolStripMenuItem)displayFavoriteTabMenu.DropDownItems[i]).Checked = alartList.Columns[i].Visible;
			
			userAlartList.Columns[i].Visible = 
				(i == 0 || i == 2 || i == 6 || i == 4) ? false :
					alartList.Columns[i].Visible;
			
			var columns = new string[] {"ShowComId",
					"ShowUserId","ShowComName","ShowUserName",
					"ShowKeyword","ShowIsAnd","ShowComFollow","ShowUserFollow",
					"ShowLatestTime","ShowRegistTime","ShowPop",
					"ShowBalloon","ShowWeb","ShowMail","ShowSound","ShowAppA",
					"ShowAppB","ShowAppC","ShowAppD","ShowAppE",
					"ShowAppF","ShowAppG","ShowAppH","ShowAppI",
					"ShowAppJ","ShowSoundType","ShowMemo"};
			config.set(columns[i], alartList.Columns[i].Visible.ToString().ToLower());
         	
			for (var ii = 0; ii < alartList.Columns.Count; ii++)
				util.debugWriteLine(ii + " " + alartList.Columns[ii].Name + " " + alartList.Columns[ii].Visible);
			
			if (i < 15 || i > 24) return;
			var n = (char)((int)'A' + i - 15);
			var item = (ToolStripMenuItem)contextMenuStrip1.Items.Find("openAppli" + n + "Menu", true)[0];
			item.Visible = alartList.Columns[i].Visible;
			var itemUser = (ToolStripMenuItem)contextMenuStrip4.Items.Find("openAppli" + n + "UserFavoriteMenu", true)[0];
			itemUser.Visible = alartList.Columns[i].Visible;
			item = (ToolStripMenuItem)contextMenuStrip3.Items.Find("liveListOpenAppli" + n + "Menu", true)[0];
			item.Visible = alartList.Columns[i].Visible;
			
			var item3 = (ToolStripMenuItem)historyListMenu.Items.Find("openAppli" + n + "HistoryMenu", true)[0];
			item3.Visible = alartList.Columns[i].Visible;
			var item4 = (ToolStripMenuItem)notAlartListMenu.Items.Find("openAppli" + n + "NotAlartMenu", true)[0];
			item4.Visible = alartList.Columns[i].Visible;
		}
		void IsTaskListDisplayTabMenuClick(object sender, EventArgs e)
		{
			var i = displayTaskTabMenu.DropDownItems.IndexOf((ToolStripMenuItem)sender);
			taskList.Columns[i].Visible = !taskList.Columns[i].Visible;
			((ToolStripMenuItem)displayTaskTabMenu.DropDownItems[i]).Checked = taskList.Columns[i].Visible; 
		}
		void UpButtonClick(object sender, EventArgs e)
		{
			var cur = alartList.CurrentCell;
			if (cur == null || cur.RowIndex < 1) return;
			var rowI = cur.RowIndex;
			//var columnI = cur.ColumnIndex;
			var ai = alartListDataSource[rowI - 1];
			alartListDataSource.RemoveAt(rowI - 1);
			alartListDataSource.Insert(rowI - 0, ai);
			//alartList.sele = new Point(columnI, rowI);
		}
		
		void DownButtonClick(object sender, EventArgs e)
		{
			var cur = alartList.CurrentCell;
			if (cur == null || cur.RowIndex == alartListDataSource.Count - 1) return;
			var rowI = cur.RowIndex;
			//var columnI = cur.ColumnIndex;
			var ai = alartListDataSource[rowI + 1];
			alartListDataSource.RemoveAt(rowI + 1);
			alartListDataSource.Insert(rowI - 0, ai);
		}
		/*
		void SearchBtnClick(object sender, EventArgs e)
		{
			if (searchText.Text == "") return;
			var searchStr = searchText.Text;
			var cur = alartList.CurrentCell;
			if (cur == null || cur.RowIndex == -1) return;
			var targetColumns = new int[] {0, 1, 2, 3, 4, alartList.Columns.Count - 1};
			try {
				for (var i = cur.RowIndex; i < alartList.RowCount; i++) {
					foreach (var c in targetColumns) {
						if (i == cur.RowIndex && c <= cur.ColumnIndex) continue;
						if (alartList[c, i].Value == null) continue;
						var t = alartList[c, i].Value.ToString();
						if (util.getRegGroup(t, "(" + searchStr + ")") != null) {
							if (!alartList.Columns[c].Visible) continue;
							alartList.CurrentCell = alartList[c, i];
							return;
						}
						
					}
				}
				for (var i = 0; i < cur.RowIndex + 1; i++) {
					foreach (var c in targetColumns) {
						if (i == cur.RowIndex && c >= cur.ColumnIndex) continue;
						var t = alartList[c, i].Value;
						if (t == null) continue;
						if (util.getRegGroup(t.ToString(), "(" + searchStr + ")") != null) {
							if (!alartList.Columns[c].Visible) continue;
							alartList.CurrentCell = alartList[c, i];
							return;
						}
						
					}
				}
				if (util.getRegGroup(cur.Value.ToString(), "(" + searchStr + ")") != null) return;
				System.Windows.Forms.MessageBox.Show("見つかりませんでした");
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		*/
		void SearchBtnClick(object sender, EventArgs e)
		{
			//var cur = alartList.CurrentCell;
			//if (cur == null || cur.RowIndex == -1) return;
			//var targetColumns = new int[] {0, 1, 2, 3, 4, alartList.Columns.Count - 1};
			searchBtnClickCore(searchText, alartList, alartListDataSource, searchBtn);
		}
		void searchBtnClickCore(System.Windows.Forms.TextBox _searchText, 
				System.Windows.Forms.DataGridView list,
				SortableBindingList<AlartInfo> dataSource, 
				System.Windows.Forms.Button btn) {
			try {
				if (_searchText.Text != "") {
					var isBeforeSearch = true;
					foreach (DataGridViewRow r in list.Rows)
						if (!r.Visible) isBeforeSearch = false;
					if (isBeforeSearch)	
						btn.Tag = list.FirstDisplayedScrollingRowIndex;
				}
				list.CurrentCell = null;
				for (var i = dataSource.Count - 1; i > -1; i--) {
					list.Rows[i].Visible = isAlartListSearchVisible(dataSource[i], _searchText.Text);
				}
				if (_searchText.Text == "" && btn.Tag != null)
					list.FirstDisplayedScrollingRowIndex = (int)btn.Tag;
				//if (util.getRegGroup(cur.Value.ToString(), "(" + searchStr + ")") != null) return;
				//System.Windows.Forms.MessageBox.Show("見つかりませんでした");
				_searchText.Focus();
				//searchText.SelectAll();
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		private bool isAlartListSearchVisible(AlartInfo ai, string t) {
			try {
				//var searchStr = liveListSearchStr;
				var sa = t.Split(new char[]{' ', '　'});
				//var sa = alartListSearchStr;
				var c = new string[]{ai.communityId, ai.hostId, ai.communityName, ai.hostName, ai.memo, ai.keyword};
				foreach (var s in sa) {
					var isDisplay = false;
					foreach (var i in c)
						if (i != null && i.IndexOf(s) > -1) isDisplay = true;
					if (!isDisplay) return false;
				}
				return true;
        	} catch (Exception e) {
        		util.debugWriteLine(e.Message + e.Source + e.StackTrace);
        		return true;
        	}
		}
		void AlartListRowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
		{
			favoriteNumLabel.Text = "登録数：" + alartList.RowCount + "件";
			if (!favoriteNumLabel.Visible) favoriteNumLabel.Visible = true;
		}
		void UserAlartListRowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
		{
			favoriteUserNumLabel.Text = "登録数：" + userAlartList.RowCount + "件";
			if (!favoriteUserNumLabel.Visible) favoriteUserNumLabel.Visible = true;
		}
		void AlartListRowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
		{
			favoriteNumLabel.Text = "登録数：" + alartList.RowCount + "件";
			if (!favoriteNumLabel.Visible) favoriteNumLabel.Visible = true;
		}
		void UserAlartListRowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
		{
			favoriteUserNumLabel.Text = "登録数：" + userAlartList.RowCount + "件";
			if (!favoriteUserNumLabel.Visible) favoriteUserNumLabel.Visible = true;
		}
		void NotifyIconBalloonTipClicked(object sender, EventArgs e)
		{
			var lvid = util.getRegGroup(notifyIcon.BalloonTipText + " " + notifyIcon.BalloonTipTitle, "(lv\\d+)");
			string url = null;
			if (lvid != null) {
				url = "https://live.nicovideo.jp/watch/" + lvid;
			} else {
				url = notifyUrl;
			}
			if (url == null) return;
			util.openUrlBrowser(url, config);
		}
		
		
		void NotifyOffMenuItemDropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			var item = (ToolStripMenuItem)e.ClickedItem;
			var items = notifyOffMenuItem.DropDownItems;
			var i = items.IndexOf(item);
			//item.Checked = !item.Checked;
			notifyOffList[i] = !notifyOffList[i];
			
			if (i == 0) {
				for (var j = 2; j < items.Count; j++) {
					notifyOffList[j] = notifyOffList[0];
				}
				
			} else {
				var isAllChecked = true;
				for(var j = 2; j < items.Count; j++) {
					if (!notifyOffList[j]) isAllChecked = false;
				}
				notifyOffList[0] = isAllChecked;
			}
			
			for (var j = 0; j < notifyOffList.Length; j++) {
				if (j == 1) continue;
				((ToolStripMenuItem)notifyOffMenuItem.DropDownItems[j]).Checked = notifyOffList[j];
			}
			setNotifyOffColumn();
		}
		
		void NotifyOffMenuItemDropDownOpening(object sender, EventArgs e)
		{
			for(var j = 0; j < notifyOffList.Length; j++) {
				if (j == 1) continue;
				((ToolStripMenuItem)notifyOffMenuItem.DropDownItems[j]).Checked = notifyOffList[j];
			}
		}
		/*
		public void followClear() {
			try {
	       		if (this.IsDisposed) return;
	       		if (!this.IsHandleCreated) return;
	        	this.Invoke((MethodInvoker)delegate() {
       		       	try {
	       		    	
   		    			while (true) {
	       		            try {
		            			foreach (var a in alartListDataSource) {
		            				a.communityFollow = "";
		            				a.hostFollow = "";
		            			}
	       		            	break;
	            			} catch (Exception e) {
	            				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
	            				Thread.Sleep(1000);
	            			}
	       		    	}
       		       	} catch (Exception e) {
       		       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
       		       	}
				});
	       	} catch (Exception e) {
	       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
	       	}
		}
		*/
		public void followUpdate(List<string[]> followList, bool isUserMode) {
			var dataSource = isUserMode ? userAlartListDataSource : alartListDataSource;
			var list = isUserMode ? userAlartList : alartList;
			while (true) {
				try {
					var c = getAlartListCount(isUserMode);
					for (var i = 0; i < c; i++) {
						var ai = dataSource[i];
						if (ai.communityId != null && ai.communityId != "" && ai.communityId != "official") {
							var isFollow = false;
							var name = ai.communityName;
							var newNameFollow = followList.Find(n => n[0] == ai.communityId);
							if (newNameFollow != null) {
								if (bool.Parse(check.form.config.get("IsUpdateComHostName")))
									ai.communityName = newNameFollow[1];
								isFollow = true; 
							}
							ai.communityFollow = (isFollow) ? "フォロー解除する" : "フォローする";
							
						}
						if (ai.hostId != null && ai.hostId != "") {
							var isFollow = false;
							var name = ai.hostName;
							var newNameFollow = followList.Find(n => n[0] == ai.hostId);
							if (newNameFollow != null) {
								if (bool.Parse(check.form.config.get("IsUpdateComHostName")))
									ai.hostName = newNameFollow[1];
								isFollow = true; 
							}
							ai.hostFollow = (isFollow) ? "フォロー解除する" : "フォローする";
						}
					}
					for (var i = 0; i < dataSource.Count; i++) {
						list.UpdateCellValue(6, i);
						list.UpdateCellValue(7, i);
					}
					break;
					
				} catch (Exception e) {
					util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
					Thread.Sleep(1000);
				}
			}
		}
		private void setAppliNameAndContextMenu() {
			var A = (int)'A';
			for (var i = 0; i < 10; i++) {
				var n = (char)(A + i);
				var item = (ToolStripMenuItem)contextMenuStrip1.Items.Find("openAppli" + n + "Menu", true)[0];
				var itemUser = (ToolStripMenuItem)contextMenuStrip4.Items.Find("openAppli" + n + "UserFavoriteMenu", true)[0];
				var item2 = (ToolStripMenuItem)contextMenuStrip3.Items.Find("liveListOpenAppli" + n + "Menu", true)[0];
				var item3 = (ToolStripMenuItem)historyListMenu.Items.Find("openAppli" + n + "HistoryMenu", true)[0];
				var item4 = (ToolStripMenuItem)notAlartListMenu.Items.Find("openAppli" + n + "NotAlartMenu", true)[0];
				var item5 = (ToolStripMenuItem)contextMenuStrip2.Items.Find("taskOpenAppli" + n + "Menu", true)[0];
				
				item.Visible = itemUser.Visible = item2.Visible = item3.Visible = item4.Visible = item5.Visible =  
						alartList.Columns[i + 15].Visible;
				
				var name = config.get("appli" + n + "Name");
				if (name == "") name = "アプリ" + n;
				formAction(() => {
					item.Text = "最近行われた放送のURLを" + name + "で開く";
					itemUser.Text = "最近行われた放送のURLを" + name + "で開く";
					item2.Text = "放送URLを" + name + "で開く";
					item3.Text = "放送URLを" + name + "で開く";
					item4.Text = "放送URLを" + name + "で開く";
					item5.Text = "放送URLを" + name + "で開く";
					
					alartList.Columns[i + 15].HeaderText = name;
					taskList.Columns[i + 10].HeaderText = name;
					userAlartList.Columns[i + 15].HeaderText = name;
				});
			}
		}
		void recentLiveAppliOpenMenu_Click(object sender, EventArgs e)
		{
			var selectedRowIndexList = new List<int>();
			var lvList = new List<string>();
			var parent = ((ToolStripMenuItem)sender).GetCurrentParent();
			if (parent == contextMenuStrip1 || parent == contextMenuStrip4) {
				var isUserMode = !favoriteCommunityBtn.Checked;
				var dataSource = isUserMode ? userAlartListDataSource : alartListDataSource;
				var list = isUserMode ? userAlartList : alartList;
				foreach (DataGridViewCell c in list.SelectedCells) {
					try {
						if (selectedRowIndexList.IndexOf(c.RowIndex) > -1) continue;
						selectedRowIndexList.Add(c.RowIndex);
						var ai = (AlartInfo)dataSource[c.RowIndex];
						lvList.Add(ai.lastLvid);
					} catch (Exception ee) {
						util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace);
					}
				}
			} else if (tabControl1.SelectedTab.Text == "通知履歴" ||
			          tabControl1.SelectedTab.Text == "予約履歴") {
				SortableBindingList<HistoryInfo> dataSource = null;
				DataGridView list = null;
				
				if (tabControl1.SelectedTab.Text == "通知履歴") {
					dataSource = parent == historyListMenu ? 
							historyListDataSource : notAlartListDataSource;
					list = parent == historyListMenu ? historyList : notAlartList;
				} else {
					dataSource = reserveHistoryListDataSource;
					list = reserveHistoryList;
				}
				
				foreach (DataGridViewCell c in list.SelectedCells) {
					try {
						if (selectedRowIndexList.IndexOf(c.RowIndex) > -1) continue;
						selectedRowIndexList.Add(c.RowIndex);
						var hi = (HistoryInfo)dataSource[c.RowIndex];
						lvList.Add(hi.lvid);
					} catch (Exception ee) {
						util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace);
					}
				}
			} else if (tabControl1.SelectedTab.Text == "時刻起動") {
				var dataSource = taskListDataSource;
				var list = taskList;
				
				foreach (DataGridViewCell c in list.SelectedCells) {
					try {
						if (selectedRowIndexList.IndexOf(c.RowIndex) > -1) continue;
						selectedRowIndexList.Add(c.RowIndex);
						var ti = taskListDataSource[c.RowIndex];
						lvList.Add(ti.lvId);
					} catch (Exception ee) {
						util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace);
					}
				}
			}
			
			foreach (var lv in lvList) {
				if (string.IsNullOrEmpty(lv)) continue;
				try {
					//var n = ((ToolStripMenuItem)sender).Name.Substring(9, 1);
					var n = util.getRegGroup(((ToolStripMenuItem)sender).Name, "Appli(.)");
					
					var path = config.get("appli" + n + "Path");
					var args = config.get("appli" + n + "Args");
					var url = "https://live.nicovideo.jp/watch/lv" + util.getRegGroup(lv, "(\\d+)");
					
					var ri = util.getRiFromLvid(lv, check.container);
					var appNum = (int)(n[0] - 'A');
					util.appliProcess(path, url, args, ri, check.container, config, appNum, this);
				} catch (Exception ee) {
					util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace);
				}
			}
		}
		private void setNotifyIcon() {
			//changeIcon(recentNum);
			var _alartListDataSource = alartListDataSource.ToArray();
			if (bool.Parse(config.get("IschangeIcon"))) {
		    	//(bool.Parse(config.get("IscheckOnAir")) ? 0 : 1) != -1)
		    	var c = 0;
				foreach (var ai in _alartListDataSource)
					if (ai.recentColorMode != 0) c++;
				changeIcon(c);
			} else if (notifyIcon.Icon != defaultNotifyIcon) {
				notifyIcon.Icon = defaultNotifyIcon;
			}
		}
		public void recentLiveCheck() {
			var recentNum = recentLiveCheckCore(true);
			recentNum += recentLiveCheckCore(false);
		}
		public int recentLiveCheckCore(bool isUserMode) {
			//var isCheck30min = bool.Parse(config.get("Ischeck30min"));
			//-1-no check 0-onair 1-30min
			var checkMode = bool.Parse(config.get("IscheckOnAir")) ? 0 : 1;
			if (!bool.Parse(config.get("IscheckRecent"))) checkMode = -1;
			
			var dataSource = isUserMode ? userAlartListDataSource : alartListDataSource;
			var list = isUserMode ? userAlartList : alartList;
			var testDt = DateTime.Now;
			while (true) {
				try {
					var recentNum = 0;
					var c = getAlartListCount(isUserMode);
					for (var i = c - 1; i > -1; i--) {
						//util.debugWriteLine(i + " " + alartListDataSource[i].lastHosoDt + " " + alartList[7, i].Style.BackColor);
						if (dataSource[i].recentColorMode == 0) {
							list.UpdateCellValue(8, i);
							continue;
						}
						
						var isRecentProcess = false;
						if (checkMode == 0) {
							isRecentProcess = isOnAir(dataSource[i]);
						} else if (checkMode == 1) {
							isRecentProcess = DateTime.Now - dataSource[i].lastHosoDt < TimeSpan.FromMinutes(30);
						} else if (checkMode == -1) {
							isRecentProcess = false;
						}
						
						if (!isRecentProcess) {
							dataSource[i].recentColorMode = 0;
							util.debugWriteLine("recentLiveCheckCore no live " + dataSource[i].lastLvid);
						}
						
						//util.debugWriteLine("test recent live check i " + i);
						if (isRecentProcess) recentNum++;
						
						//var isRecentColor = alartList[7, i].Style.BackColor == ((recentColor == Color.Empty) ? alartListDataSource[i].backColor : recentColor);
						//if (isRecentProcess != isRecentColor) {
							//alartList[7, i].Style.BackColor = (i % 2 != 0) ? 
							//	Color.FromArgb(245, 245, 245)
							//	: Color.FromName("window");
							for (var j = 0; j < list.Columns.Count; j++)
								list.UpdateCellValue(j, i);
						//}
						setNotifyIcon();
					}
					//var ii = notifyIcon.Icon == Icon;
					return recentNum;
					
					
				} catch (Exception e) {
					util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
					Thread.Sleep(1000);
				}
			}
		}
		private bool isOnAir(AlartInfo ai) {
			if (string.IsNullOrEmpty(ai.lastLvid)) return false;
			
			var elapsed = DateTime.Now - ai.lastHosoDt;
			var isCheck = false;
			if (ai.lastLvid.EndsWith("e")) {
				util.debugWriteLine("isOnAir end e deleteNotifyIconRecentItem " + ai.lastLvid);
				deleteNotifyIconRecentItem(ai.lastLvid);
				return false;
			}
			if (ai.communityId == null ||
			    	ai.communityId.StartsWith("ch") ||
			    	ai.communityId == "official") {
				isCheck = elapsed < TimeSpan.FromHours(100);
			} else isCheck = elapsed < TimeSpan.FromHours(24);
			if (!isCheck) {
				util.debugWriteLine("isOnAir isCheck false deleteNotifyIconRecentItem " + ai.lastLvid);
				deleteNotifyIconRecentItem(ai.lastLvid);
				ReflectionListLv(ai.lastLvid);
				return false;
			}
			
			util.debugWriteLine("check alart live onair " + ai.lastLvid);
			var isOnAir = isOnAirLvid(ai.lastLvid, ai.lastLvType, true);
			if (!isOnAir) {
				ai.lastLvid += "e";
			}
			return isOnAir;
		}
		public bool isOnAirLvid(string lvid, string type, bool debugWriteLog = false) {
			util.debugWriteLine("isOnairLvid " + lvid + " " + type);
			var isProgramInfo = (check.container != null &&
			    	(type == "user" || type == "community" ||
			     		type == "channel"));
			if (false && isProgramInfo) {
				/*
				var url = "https://live.nicovideo.jp/watch/" + lvid + "/programinfo";
				
				var h = new Dictionary<string, string>(){{"user_session", check.container.GetCookies(new Uri(url))["user_session"].Value}};
				
				var res = util.getPageSource(url, check.container);
				if (res == null) {
					var r = util.sendRequest(url, h, null, "GET", true);
					using (var rr = r.GetResponseStream())
					using (var sr = new StreamReader(rr)) {
						res = sr.ReadToEnd();
						if (res.IndexOf("status\":401,\"errorCode\":\"UNAUTHORIZED\"") > -1) {
							check.container = null;
							check.setCookie();
							if (check.container != null)
								res = util.getPageSource(url, check.container);
						}
					}
					if (res != null && res.IndexOf("\"status\":200,\"errorCode\":\"OK\"") == -1) {
						return false;
					}
				}
				if (res != null) {
					var ret = res.IndexOf("\"status\":\"end\"") == -1 &&
							res.IndexOf("errorCode\":\"NOT_FOUND\"") == -1;
					if (!ret) {
						util.debugWriteLine("終了判定 res " + res);
					}
					
					return ret;
				}*/
			}
			
			var _url = "https://live.nicovideo.jp/embed/" + lvid;
			//var _res = util.getPageSource(_url, null);
			var _res = new Curl().getStr(_url, util.getHeader(null, null, null), CurlHttpVersion.CURL_HTTP_VERSION_2TLS, "GET", null, false);
			if (_res == null) {
				util.debugWriteLine("放送の確認に失敗しました " + lvid);
				return true;
			}
			var startTStr = util.getRegGroup(_res, "datetime=\"(.+?)\"");
			var isNotStarted = (startTStr != null && DateTime.Parse(startTStr) > DateTime.Now);
			if (isNotStarted)
				util.debugWriteLine("is not started " + lvid);
			//var _ret = _res.IndexOf("status-onair\">") > -1 ||
			//			_res.IndexOf("status-comingsoon\">") > -1;
			var _ret = _res.IndexOf("data-status=\"onair\"") > -1 ||
					_res.IndexOf("data-status=\"comingsoon\"") > -1 ||
					(startTStr != null && DateTime.Parse(startTStr) > DateTime.Now - TimeSpan.FromMinutes(2));
			if (!_ret) {
				util.debugWriteLine("終了判定 embed " + lvid + " " + _res);
				util.debugWriteLine("isOnAirLvid !_ret deleteNotifyIconRecentItem " + lvid);
				deleteNotifyIconRecentItem(lvid);
				ReflectionListLv(lvid);
			}
			return _ret;
		}
		void deleteNotifyIconRecentItem(string lvid) {
			util.debugWriteLine("deleteNotifyIconRecentItem " + lvid);
			try {
				var items = notifyIconMenuStrip.Items; 
				for (var i = 0; i < items.Count - 4; i++) {
					if (((RssItem)items[i].Tag).lvId == lvid) {
						util.debugWriteLine("deleteNotifyIconRecentItem delete arglvid " + lvid + " del " + items[i].Text);
						formAction(() => items.Remove(items[i]));
					}
				}
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
		}
		private void changeIcon(int recentNum) {
			var n = recentNum;
			if (recentNum > 9) recentNum = 9;
			var icon = new Icon(util.getJarPath()[0] + "/Icon/number4_" + recentNum.ToString() + "c.ico");
			formAction(() => {
				notifyIcon.Icon = icon;
			});
			//this.Invoke((MethodInvoker)delegate() {
			//	Icon = new Icon("Icon/number4_" + recentNum.ToString() + "c.ico");
			//});
		}
		
		void AlartListCellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			var isUserMode = ((DataGridView)sender).Name == "userAlartList";
			var dataSource = isUserMode ? userAlartListDataSource : alartListDataSource;
			
			try {
				if (e.RowIndex < 0) return;
				var ai = dataSource[e.RowIndex];
				var action = config.get("doublecmode");
				
				if (action == "なにもしない") return;
				else if (action.StartsWith("最近行われた放送のURLを")) {
					if (ai.lastLvid != null && ai.lastLvid != "") {
						var url = "https://live.nicovideo.jp/watch/lv" + util.getRegGroup(ai.lastLvid, "(\\d+)");
						if (action.EndsWith("を開く"))
							util.openUrlBrowser(url, config);
						else if (action.EndsWith("コピー"))
							Clipboard.SetText(url);
						else if (action.EndsWith("で開く")) {
							var appName = action[16];
							var lvid = "lv" + util.getRegGroup(ai.lastLvid, "(\\d+)+");
							var path = config.get("appli" + appName + "Path");
							var arg = config.get("appli" + appName + "Args");
							var appNum = (int)(appName - 'A');
							Task.Factory.StartNew(() => 
									util.appliProcessFromLvid(path, lvid, arg, check.container, config, appNum, this));
						}
							
					}
				} else if (action.StartsWith("チャンネルURLを")) {
					if (ai.communityId != null && ai.communityId != "") {
						var url = (ai.communityId.IndexOf("ch") > -1) ? 
								("https://ch.nicovideo.jp/" + ai.communityId) :
								("https://com.nicovideo.jp/community/" + ai.communityId);
						if (action.EndsWith("開く"))
							util.openUrlBrowser(url, config);
						else Clipboard.SetText(url);
					}
				} else if (action.StartsWith("ユーザーURLを")) {
					if (ai.hostId != null && ai.hostId != "") {
						var url = "https://www.nicovideo.jp/user/" + ai.hostId;
						if (action.EndsWith("開く"))
							util.openUrlBrowser(url, config);
						else Clipboard.SetText(url);
					}
				}
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		/*
		public void setFollowText(string id, bool isFollow) {
			
			try {
	       		if (IsDisposed) return;
	       		if (!IsHandleCreated) return;
	        	Invoke((System.Windows.Forms.MethodInvoker)delegate() {
	       			setFollowTextCore(id, isFollow);
				});
	       	} catch (Exception e) {
	       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
	       	}
		}
		void setFollowTextCore(string id, bool isFollow) {
			while (true) {
	       		try {
					var isUser = !id.StartsWith("c");
	       			foreach (var ai in alartListDataSource) {
		    			if (isUser && ai.hostId == id) {
		    				ai.hostFollow = (isFollow) ? "フォロー解除する" : "フォローする";
		    				var row = alartListDataSource.IndexOf(ai);
		    				alartList.UpdateCellValue(7, row);
		    			}
		    			if (!isUser && 
						    ai.communityId == id) {
		    				ai.communityFollow = (isFollow) ? "フォロー解除する" : "フォローする";
		    				var row = alartListDataSource.IndexOf(ai);
		    				alartList.UpdateCellValue(6, row);
		    			}
	       			}
	       			break;
	       		} catch (Exception e) {
		       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
   		       	}
	       	}
		}
		*/
		void BulkUserFollowMenuClick(object sender, EventArgs e)
		{
			Task.Factory.StartNew(() =>
				new ListFollow(this, check).userFollow()
			);
		}
		void BulkCommunityFollowMenuClick(object sender, EventArgs e)
		{
			Task.Factory.StartNew(() =>
				new ListFollow(this, check).communityFollow()
			);
		}
		void BulkChannelFollowMenuClick(object sender, EventArgs e)
		{
			Task.Factory.StartNew(() =>
				new ListFollow(this, check).channelFollow()
			);
		}
		void categoryBorderPaint(object sender, PaintEventArgs e)
		{
			System.Drawing.Rectangle borderRectangle = ((System.Windows.Forms.RadioButton)sender).ClientRectangle;
			borderRectangle.Inflate(-0, -0);
			System.Windows.Forms.ControlPaint.DrawBorder3D(e.Graphics, borderRectangle, 
				System.Windows.Forms.Border3DStyle.Raised);
		}
		void setCategoryBorderPaint() {
			foreach (var _c in categoryBtnPanel.Controls) {
				var c = _c as System.Windows.Forms.RadioButton;
				if (c == null) continue;
				c.Paint += new System.Windows.Forms.PaintEventHandler(categoryBorderPaint);
			}
		}
		void categoryMoveBorderPaint(object sender, PaintEventArgs e)
		{
			//System.Drawing.Rectangle borderRectangle = ((System.Windows.Forms.Button)sender).ClientRectangle;
			System.Drawing.Rectangle borderRectangle = ((System.Windows.Forms.Button)sender).ClientRectangle;
			borderRectangle.Inflate(-0, -0);
			//System.Windows.Forms.ControlPaint.DrawBorder3D(e.Graphics, borderRectangle, 
			//	System.Windows.Forms.Border3DStyle.Raised);
			ControlPaint.DrawBorder(e.Graphics, borderRectangle, Color.FromArgb(173, 171, 179), ButtonBorderStyle.Solid);
			
			System.Drawing.Rectangle borderRectangle2 = ((System.Windows.Forms.Button)sender).ClientRectangle;
			borderRectangle2.Inflate(-1, -1);
			ControlPaint.DrawBorder(e.Graphics, borderRectangle2, Color.White, ButtonBorderStyle.Solid);
		}
		
		void CategoryLeftBtnClick(object sender, EventArgs e)
		{
			var btnsWidth = 0;
			foreach (Control b in categoryBtnPanel.Controls) btnsWidth += b.Width;
			if (btnsWidth <= categoryBtnPanel.Size.Width) return;
			for (var i = 1; i < categoryBtnPanel.Controls.Count; i++) {
				if (categoryBtnPanel.Controls[i].Visible) {
					categoryBtnPanel.Controls[i - 1].Visible = true;
					break;
				}
			}
		}
		void CategoryRightBtnClick(object sender, EventArgs e)
		{
			if (categoryBtnPanel.PreferredSize.Width < categoryBtnPanel.Width) return;
			
			var btnsWidth = 0;
			foreach (Control b in categoryBtnPanel.Controls) btnsWidth += b.Width;
			if (btnsWidth <= categoryBtnPanel.Size.Width) return;
			foreach (System.Windows.Forms.RadioButton b in categoryBtnPanel.Controls) {
				if (!b.Visible) continue;
				b.Visible = false;
				break;
			}
			categoryBtnDisplayUpdate();
		}
		void CategoryBtnPanelSizeChanged(object sender, EventArgs e)
		{
			util.debugWriteLine("category panel size " + categoryBtnPanel.Size.Width);
			//categoryBtnDisplayUpdate();
		}
		void categoryBtnDisplayUpdate() {
			//var freeWidth = liveListSearchText.Location.X - categoryBtnPanel.Location.X - 3;
			var btnsWidth = 8 * 3;
			foreach (Control b in categoryBtnPanel.Controls) btnsWidth += b.Width;
			
			var freeWidth = Size.Width - categoryBtnPanel.Location.X - 175;
			//var isDisplayBtn = categoryBtnPanel.PreferredSize.Width > freeWidth;
			var isDisplayBtn = btnsWidth > freeWidth;
			if (isDisplayBtn != categoryLeftBtn.Visible) {
				categoryLeftBtn.Visible = categoryRightBtn.Visible = isDisplayBtn;
				categoryBtnPanel.Width = (isDisplayBtn) ? (freeWidth - 35) : freeWidth;
			}
			
			var headCateIndex = 0;
			for (var i = 0; i < categoryBtnPanel.Controls.Count; i++) {
				if (categoryBtnPanel.Controls[i].Visible) {
					headCateIndex = i;
					break;
				}
			}
			if (headCateIndex > 0 && 
			    	categoryBtnPanel.Width - categoryBtnPanel.PreferredSize.Width > 
			    	categoryBtnPanel.Controls[headCateIndex - 1].Width + 5) {
				categoryBtnPanel.Controls[headCateIndex - 1].Visible = true;
			}
		}
		public void formAction(Action a, int timeout = 2000, [CallerMemberName] string memberName = "") {
			util.debugWriteLine("formaction0 " + memberName + " " + lastHosoStatusBar.Text);
			if (IsDisposed || !util.isShowWindow || !IsHandleCreated) return;
			
			util.debugWriteLine("formaction " + memberName + " " + lastHosoStatusBar.Text);
			//test
			//util.debugWriteLine("formaction st " + new StackTrace(true));
			if (Thread.CurrentThread == madeThread) {
				try {
					util.debugWriteLine("formaction begin sync " + memberName);
					a.Invoke();
					util.debugWriteLine("formaction sync end " + memberName + " " + lastHosoStatusBar.Text);
				} catch (Exception e) {
					util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				}
			} else {
				IAsyncResult r = null;
				try {
					util.debugWriteLine("formaction begin async " + memberName);
					r = BeginInvoke((MethodInvoker)delegate() {
					//Invoke((MethodInvoker)delegate() {
						try {
				       		a.Invoke();
				       	} catch (Exception e) {
							util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
						}
					
					});
					r.AsyncWaitHandle.WaitOne(timeout);
					//});
				} catch (Exception e) {
					util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
					addLogText("act " + e.Message + e.Source + e.StackTrace + e.TargetSite);
				} finally {
					util.debugWriteLine("formaction async end " + memberName + " " + lastHosoStatusBar.Text);
					if (r != null) {
						try {
							//EndInvoke(r);
						} catch (Exception e) {
							util.debugWriteLine(e.Message + e.Source + e.StackTrace);
						}
					}
				}
			}
			util.debugWriteLine("formaction end " + memberName + " " + lastHosoStatusBar.Text);
		}
		private void formActionAsync(Action a) {
			_formActionAsync(a).Wait();
		}
		async private Task _formActionAsync(Action a) {
			await Task.Factory.StartNew(a);
		}
		public void removeLiveListItem(LiveInfo li) {
			if (Thread.CurrentThread == madeThread)
					util.debugWriteLine("lock form thread removeLiveListItem");
			//liveListLockAction(() => 
					_removeLiveListItem(li);//);
		}
		public void _removeLiveListItem(LiveInfo li) {
			try {
				var img = li.thumbnail;
				//for (var i = liveList.Rows.Count - 1; i > -1; i--) {
					//if (liveList.Rows[i].DataBoundItem == li) {
//						util.debugWriteLine("remove mae [0] " + liveList.Rows[0].Visible + " " + liveList.Rows[0].Cells[3].Value + " " + liveListDataSource[0].hostName);
						//formAction(() => {liveList.Rows.RemoveAt(i);});
						formAction(() => {liveListDataSource.Remove(li);});
						util.debugWriteLine("remove " + li.hostName);
						
					//}
				//}
				liveListDataReserve.Remove(li);
				if (img != null) img.Dispose();
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
		}
		public void addLiveListItem(List<LiveInfo> liList, char cateChar, bool blindOnlyA, bool blindOnlyB, bool blindQuestion, bool isFavoriteOnly) {
			//var isDisp = (cateChar == '全' || li.MainCategory[0] == cateChar);
			//formAction(() => {
				addLiveListItemCore(liList, cateChar, blindOnlyA, blindOnlyB, blindQuestion, isFavoriteOnly);
			//});
			
			if (!isLiveListTimeProcessing) {
				isLiveListTimeProcessing = true;
				Task.Factory.StartNew(() => liveListTimeProcess());
			}
		}
		void addLiveListItemCore(List<LiveInfo> liList, char cateChar, bool blindOnlyA, bool blindOnlyB, bool blindQuestion, bool isFavoriteOnly) {
			var delMin = double.Parse(config.get("liveListDelMinutes"));
			var sI = liveList.FirstDisplayedScrollingRowIndex;
			var now = DateTime.Now;
			foreach (var li in liList) {
				var isDisp = li.isDisplay(cateChar);
				var isMemberOnly = !string.IsNullOrEmpty(li.memberOnly); 
		        if (blindOnlyA && isMemberOnly &&
				   		!string.IsNullOrEmpty(li.favorite))
		        	isDisp = false;
		        if (blindOnlyB && isMemberOnly)
		        	isDisp = false;
		        if (blindQuestion && cateChar != '全' &&
		            	string.IsNullOrEmpty(li.lvId))
		        	isDisp = false;
		        if (isFavoriteOnly && li.favorite == "")
		        	isDisp = false;
		        if (!isLiveListSearchVisible(li))
		        	isDisp = false;
		        if (delMin != 0 && ((TimeSpan)(now - li.pubDateDt)).TotalMinutes > delMin)
		        	continue;
		        if (!isLiveFilterMatch(li))
		        	isDisp = false;
		        formAction(() => {
			        if (isDisp)	{
						liveListDataSource.Add(li);
			        } else liveListDataReserve.Add(li);
				});
			}
		    setScrollIndex(liveList, sI);
		}
		/*
		public bool insertLiveListItem(LiveInfo li, int index, char cateChar) {
			//if (cateChar == '全' || li.MainCategory[0] == cateChar) {
			if (li.isDisplay(cateChar)) {
				
				formAction(() => {
					var sI = liveList.FirstDisplayedScrollingRowIndex;
					liveListDataSource.Insert(index, li);
					setScrollIndex(liveList);
				});
				return true;
			} else {
				liveListDataReserve.Add(li);
				return false;
			}
		}
		*/
		private void liveListTimeProcess() {
			while (liveList.Rows.Count > 0) {
				Thread.Sleep(10000);
				try {
					deleteLiveListTime();
					
					setLiveListNum();
				} catch (Exception e) {
					util.debugWriteLine("livelist time process exception " + e.Message + e.Source + e.StackTrace + e.TargetSite);
				}
				
			}
			
			isLiveListTimeProcessing = false;
		}
		private void deleteLiveListTime() {
			var delMin = double.Parse(config.get("liveListDelMinutes"));
			var now = DateTime.Now;
			
			if (Thread.CurrentThread == madeThread)
					util.debugWriteLine("lock form thread deleteLiveListTime");
			
			liveListLockAction(() => {
				var sI = liveList.FirstDisplayedScrollingRowIndex;
				_deleteLiveListTime(delMin, now);
				setScrollIndex(liveList, sI);
			});
			
			
			util.debugWriteLine("deletelivelistTime end " + (DateTime.Now - now));
		}
		private void _deleteLiveListTime(double delMin, DateTime now) {
			util.debugWriteLine("deletelivelistTime 0");
			LiveInfo[] _liveListDataSource = null;
			try {
				_liveListDataSource = liveListDataSource.ToArray();
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace);
				return;
			}
			//formAction(() => {
				try {
					for (var i = _liveListDataSource.Count() - 1; i > -1; i--) {
				        var li = _liveListDataSource[i];
				        if (li == null) continue;
				        var isDelete = (delMin != 0 && ((TimeSpan)(now - li.pubDateDt)).TotalMinutes > delMin) ||
				        	(!string.IsNullOrEmpty(li.comId) && 
				        	 	((li.comId.StartsWith("co") && now - li.pubDateDt > TimeSpan.FromHours(24)) || 
				        	  (li.comId.StartsWith("ch") && now - li.pubDateDt > TimeSpan.FromHours(100))));
				    	if (isDelete) {
				           	 var th = li.thumbnail;
				           	 if (th == null) continue;
				           	 formAction(() => {
							 	liveListDataSource.Remove(li);
							 });
							 if (th != null) th.Dispose();
						}
				        else formAction(() => liveList.UpdateCellValue(8, i));
						
					}
	           	} catch (Exception e) {
	           		util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
	           	}
			//});
			util.debugWriteLine("deletelivelistTime 1");
			try {
				for (var i = liveListDataReserve.Count - 1; i > -1; i--) {
					var li = liveListDataReserve[i];
					if (li == null) continue;
					var isDelete = (delMin != 0 && ((TimeSpan)(now - li.pubDateDt)).TotalMinutes > delMin) ||
				    		(string.IsNullOrEmpty(li.comId) && li.comId.StartsWith("co") && now - li.pubDateDt > TimeSpan.FromHours(24));
					
					if (isDelete) {
						var th = li.thumbnail;
						if (th == null) continue;
						liveListDataReserve.RemoveAt(i);
						if (th != null) th.Dispose();
					}
				}
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
		}
		void LiveListColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
		{
			//util.debugWriteLine("column index " + e.Column.Index);
			if (e.Column.Index != 1) return;
			if (e.Column.HeaderText != "ｻﾑﾈ") return;
			liveList.RowTemplate.Height = liveList.Columns[1].Width;
			liveListRowHeightUpdate();
			
		}
		public void liveListRowHeightUpdate() {
			try {
				foreach (DataGridViewRow r in liveList.Rows)
					r.Height = liveList.Columns[1].Width;
			} catch (Exception ee) {
				util.debugWriteLine("livelist row heightupdate exception " + ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
				
		}
		void LiveListCellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (e.ColumnIndex == -1 || e.RowIndex == -1) return;
			var act = int.Parse(config.get("brodouble"));
			var li = liveListDataSource[e.RowIndex];
			var url = "https://live.nicovideo.jp/watch/" + li.lvId;
			
			var comUrl = "";
			if (li.comId != null && li.comId != "") {
				var isChannel = liveListDataSource[e.RowIndex].comId.IndexOf("ch") > -1;
				comUrl = (isChannel) ? 
					("https://ch.nicovideo.jp/" + li.comId) :
					("https://com.nicovideo.jp/community/" + li.comId);
			}
			
			if (act == 1) {
				util.openUrlBrowser(url, config);
			} else if (act == 2) {
				util.openUrlBrowser(comUrl, config);
			} else if (act == 3) {
				Clipboard.SetText(url);
			} else if (act == 4) {
				Clipboard.SetText(comUrl);
			} else if (act == 5) {
				openAddForm(li.comId);
			} else if (act >= 6 && act <= 15) {
				var appName = (char)('A' + (act - 6));
				var path = config.get("appli" + appName + "Path");
				var arg = config.get("appli" + appName + "Args");
				var appNum = (int)(appName - 'A');
				Task.Factory.StartNew(() => 
						util.appliProcessFromLvid(path, li.lvId, arg, check.container, config, appNum, this));
							
			}
		}
		void LiveListOpenUrlMenuClick(object sender, EventArgs e)
		{
			try {
				var selectedLvList = new List<string>();
				foreach (DataGridViewCell c in liveList.SelectedCells) {
					try {
						var li = (LiveInfo)liveListDataSource[c.RowIndex];
						if (string.IsNullOrEmpty(li.lvId)) continue;
						
						var url = "https://live.nicovideo.jp/watch/lv" + util.getRegGroup(li.lvId, "(\\d+)");
						if (selectedLvList.IndexOf(url) > -1) continue;
						selectedLvList.Add(url);
					} catch (Exception ee) {
						util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
					}
				}
				if (selectedLvList.Count > 10 && MessageBox.Show(selectedLvList.Count + "の放送が選択されています。このまま開きますか？", "", MessageBoxButtons.YesNo) 
				    	!= DialogResult.Yes) return;
				foreach (var url in selectedLvList)
					util.openUrlBrowser(url, config);
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		void LiveListOpenCommunityUrlMenuClick(object sender, EventArgs e)
		{
			try {
				var selectedLvList = new List<string>();
				foreach (DataGridViewCell c in liveList.SelectedCells) {
					try {
						var li = (LiveInfo)liveListDataSource[c.RowIndex];
						if (string.IsNullOrEmpty(li.comId)) continue;
						
						var isChannel = li.comId.IndexOf("ch") > -1;
						var url = (isChannel) ? 
								("https://ch.nicovideo.jp/" + li.comId) :
								("https://com.nicovideo.jp/community/" + li.comId);
						if (selectedLvList.IndexOf(url) > -1) continue;
						selectedLvList.Add(url);
					} catch (Exception ee) {
						util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
					}
				}
				if (selectedLvList.Count > 10 && MessageBox.Show(selectedLvList.Count + "の放送が選択されています。このまま開きますか？", "", MessageBoxButtons.YesNo) 
				    	!= DialogResult.Yes) return;
				foreach (var url in selectedLvList)
					util.openUrlBrowser(url, config);
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		void LiveListAddFavoriteCommunityMenuClick(object sender, EventArgs e)
		{
			try {
				var curCell = liveList.CurrentCell;
				if (curCell == null || curCell.RowIndex == -1) return;
				var li = (LiveInfo)liveListDataSource[curCell.RowIndex];
				if (li.comId == null || li.comId == "") return;
				openAddForm(li.comId);
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		void LiveListRemoveFavoriteCommunityMenuClick(object sender, EventArgs e)
		{
			var cur = liveList.CurrentCell;
			if (cur.RowIndex == -1) return;
			var li = liveListDataSource[cur.RowIndex];
			if (li.comId == null || li.comId == "") return;
			if (System.Windows.Forms.MessageBox.Show("お気に入りリストから" + li.comId + "の行を削除していいですか？", "確認", MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;
			while (true) {
				try {
					var isRemove = false;
					foreach (var ai in alartListDataSource) {
						if (ai.communityId == li.comId) {
							alartListDataSource.Remove(ai);
							isRemove = true;
							break;
						}
					}
					if (!isRemove) break;
				} catch (Exception ee) {
					util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
				}
			}
		}
		
		void LiveListDeleteRowMenuClick(object sender, EventArgs e)
		{
			if (liveList.SelectedCells.Count == 0) return;
			var selectedLIList = new List<LiveInfo>();
			foreach (DataGridViewCell c in liveList.SelectedCells) {
				try {
					var li = (LiveInfo)liveListDataSource[c.RowIndex];
					if (selectedLIList.IndexOf(li) > -1) continue;
					else selectedLIList.Add(li);
				} catch (Exception ee) {
					util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
				}
			}
			
			var ids = selectedLIList.Select(li => li.lvId);
			var idsStr = string.Join("、", ids);
			
			var id = string.IsNullOrEmpty(idsStr) ? "こ" : (idsStr);
			if (System.Windows.Forms.MessageBox.Show(id + "の行を削除していいですか？", "確認", MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;
			foreach (var li in selectedLIList)
				liveListDataSource.Remove(li);
			changedListContent();
				
				
			
			/*
			var cur = liveList.CurrentCell;
			if (cur.RowIndex == -1) return;
			var li = liveListDataSource[cur.RowIndex];
			if (System.Windows.Forms.MessageBox.Show(li.lvId + "の行を削除していいですか？", "確認", MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;

		    if (Thread.CurrentThread == madeThread)
				util.debugWriteLine("lock form thread livelistDeleteRowMenuClick");
		    Task.Factory.StartNew(() => {
			    liveListLockAction(() => 
						formAction(() => liveListDataSource.Remove(li)));
			    setLiveListNum();
			});
			*/
		}
		
		void LiveListCopyMenuDropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			var t = e.ClickedItem.Text;
			try {
				var selectedLvList = new List<string>();
				foreach (DataGridViewCell c in liveList.SelectedCells) {
					try {
						var li = (LiveInfo)liveListDataSource[c.RowIndex];
						var url = "";
						if (t == "放送URL") {
							url = "https://live.nicovideo.jp/watch/" + li.lvId;
						} else if (t == "チャンネルURL") {
							var isChannel = li.comId.IndexOf("ch") > -1;
							url = (isChannel) ? 
									("https://ch.nicovideo.jp/" + li.comId) :
									("https://com.nicovideo.jp/community/" + li.comId);
						} else if (t == "放送タイトル") {
							url = li.title;
						} else if (t == "放送者") {
							url = li.hostName;
						} else if (t == "チャンネル名") {
							url = li.comName;
						} else if (t == "説明") {
							url = li.description;
						}
						if (string.IsNullOrEmpty(url)) continue;
						if (selectedLvList.IndexOf(url) > -1) continue;
						selectedLvList.Add(url);
					} catch (Exception ee) {
						util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
					}
				}
				Clipboard.SetText(string.Join("\n", selectedLvList.ToArray()));
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		
		public void setLiveListNum() {
			util.debugWriteLine("setLiveListNum");
			try {
				var dt = DateTime.Now;
				var l = new List<LiveInfo>(liveListDataSource);
				var l2 = new List<LiveInfo>(liveListDataReserve);
				var allC = l.Count + l2.Count;
				
				var mainCategories = new string[]{"一般", "やってみた",
						"ゲーム", "動画紹介"};
				
				var btns = new System.Windows.Forms.RadioButton[]{commonCategoryBtn, tryCategoryBtn,
						liveCategoryBtn, reqCategoryBtn};
				
				var btnsText = new List<string>();
				for (var i = 0; i < btns.Length; i++) {
					var n = l.Count(x => x.isDisplay(btns[i].Text[0]));
					var reserveN = l2.Count(x => x.isDisplay(btns[i].Text[0]));
					//var t = (mainCategories[i].StartsWith("一般")) ? "一般" : mainCategories[i];
					var t = mainCategories[i];
					btnsText.Add(string.Format(t + "({0}/{1})", n + reserveN, allC));
					
					
				}
				formAction(() => {
					try {
						allCategoryBtn.Text = string.Format("全て({0}/{1})", allC, allC);
						for (var i = 0; i < mainCategories.Length; i++) {
							btns[i].Text = btnsText[i];
							//util.debugWriteLine("btnsText[i] " + btnsText[i]);
						}
		           	} catch (Exception e) {
		           		util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
		           	}
				});
				util.debugWriteLine("setLiveListNum time " + (DateTime.Now - dt));
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
		}
		
		void CategoryBtnCheckedChanged(object sender, EventArgs e)
		{
			var btn = (System.Windows.Forms.RadioButton)sender;
			util.debugWriteLine("CategoryBtnCheckedChanged " + btn.Name + " " + btn.Checked);
			if (!btn.Checked) return;
			Task.Factory.StartNew(() => resetLiveList());
		}
		void resetLiveList() {
			if (Thread.CurrentThread == madeThread)
					util.debugWriteLine("lock form thread resetLiveList");
			
			liveListLockAction(() => 
					_resetLiveList(), "resetLiveList0");
			sortLiveList();
			
			liveListLockAction(() => {
				if (bool.Parse(config.get("FavoriteUp")))
					upLiveListFavorite();
			}, "resetLiveList1");
		}
		private void _resetLiveList() {
			util.debugWriteLine("_restLiveList");
			var mainCategories = new string[]{"一般", "やってみた",
					"ゲーム", "動画紹介"};
			var btns = new System.Windows.Forms.RadioButton[]{commonCategoryBtn, tryCategoryBtn,
					liveCategoryBtn, reqCategoryBtn};
			//var n = ((Control)sender).Name;
			System.Windows.Forms.RadioButton sender = null;
			foreach (var b in btns) 
				if (b.Checked) sender = b;
			if (allCategoryBtn.Checked) sender = allCategoryBtn;
			if (sender == null)	return;
			
			var isAll = sender == allCategoryBtn;
			var cateName = (isAll) ? null : mainCategories[Array.IndexOf(btns, sender)];
			bool BlindOnlyA = bool.Parse(config.get("BlindOnlyA"));
			bool BlindOnlyB = bool.Parse(config.get("BlindOnlyB"));
			bool BlindQuestion = bool.Parse(config.get("BlindQuestion"));
			bool isFavoriteOnly = bool.Parse(config.get("FavoriteOnly"));
			try {
				liveList.CurrentCell = null;
				var _liveListDataSource = liveListDataSource.ToArray();
				
				//formAction(() => {
					for (var i = _liveListDataSource.Count() - 1; i > -1; i--) {
						try {
							var li = _liveListDataSource[i];
							//var isDisplay = (isAll || li.MainCategory == cateName);
							var isDisplay = li.isDisplay(sender.Text[0]);
							var isMemberOnly = !string.IsNullOrEmpty(li.memberOnly);
							
					        if (BlindOnlyA && isMemberOnly &&
							   		string.IsNullOrEmpty(li.favorite))
					        	isDisplay = false;
					        if (BlindOnlyB && isMemberOnly)
					        	isDisplay = false;
							if (BlindQuestion && cateName != "全て" &&
					            	string.IsNullOrEmpty(li.lvId))
					        	isDisplay = false;
					        if (isFavoriteOnly && string.IsNullOrEmpty(li.favorite.Trim()))
					        	isDisplay = false;
					        if (!isLiveListSearchVisible(li))
					        	isDisplay = false;
					        if (!isLiveFilterMatch(li))
					        	isDisplay = false;
							if (!isDisplay) {
					        	formAction(() => {
									liveListDataReserve.Add(li);
									liveListDataSource.Remove(li);
								});
					        } else {
					        	//util.debugWriteLine("disp " + li.title);
					        }
		           		} catch (Exception e) {
							addLogText(e.Message + e.Source + e.StackTrace + e.TargetSite);
		           		}
				    }
				//});
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
			
			try {
				//formAction(() => {
					for (var i = liveListDataReserve.Count - 1; i > -1; i--) {
						var isDisplay = (isAll || liveListDataReserve[i].MainCategory == cateName);
						//var isDisplay = liveListDataReserve[i].isDisplay(sender.Text[0]);
						
						var isMemberOnly = !string.IsNullOrEmpty(liveListDataReserve[i].memberOnly);
						
				        if (BlindOnlyA && isMemberOnly &&
						   		string.IsNullOrEmpty(liveListDataReserve[i].favorite))
				        	isDisplay = false;
				        if (BlindOnlyB && isMemberOnly)
				        	isDisplay = false;
				        if (BlindQuestion && cateName != "全て" &&
				            	string.IsNullOrEmpty(liveListDataReserve[i].lvId))
				        	isDisplay = false;
				        if (isFavoriteOnly && string.IsNullOrEmpty(liveListDataReserve[i].favorite))
				        	isDisplay = false;
				        if (!isLiveListSearchVisible(liveListDataReserve[i]))
				        	isDisplay = false;
				        if (!isLiveFilterMatch(liveListDataReserve[i]))
		        			isDisplay = false;
				        
				        if (isDisplay) {
				        	formAction(() => {
					        	//var sI = liveList.FirstDisplayedScrollingRowIndex;
								liveListDataSource.Add(liveListDataReserve[i]);
								//setScrollIndex(liveList);
				       		});
				        	liveListDataReserve.RemoveAt(i);
						}
				    }
				//});
				
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
			
			try {
				var _liveListDataSource = liveListDataSource.ToArray();
				for (var i = _liveListDataSource.Count() - 1; i > -1; i--) {
					var li = _liveListDataSource[i];
					//var isDisp = (liveListDataSource[i].MainCategory == cateName ||
					//            cateName == null);
					var isDisp = li.isDisplay(sender.Text[0]);
					if (!isDisp) {
						formAction(() => {
							liveListDataReserve.Add(li);
							liveListDataSource.Remove(li);
						});
					}
				}
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace);
			}
			
			
		}
		public void getVisiRow(bool isAll = false) {
			var visiItems = new List<LiveInfo>();
			for (var i = 0; i < liveList.Rows.Count; i++) {
				var ii = liveList.Rows[i];
				if (ii.Visible) {
					visiItems.Add(((LiveInfo)ii.DataBoundItem));
//					util.debugWriteLine(((LiveInfo)ii.DataBoundItem).hostName + " " + i);
				}
//				if (isAll) util.debugWriteLine(((LiveInfo)ii.DataBoundItem).hostName + " " + i);
				
			}
			util.debugWriteLine(visiItems.Count);
		}
		void UpdateLiveListMenuClick(object sender, EventArgs e)
		{
			util.debugWriteLine("UpdateLiveListMenuClick");
			Task.Factory.StartNew(() => liveCheck.load());
		}
		void UpdateAutoDeleteMenuDropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			//((ToolStripMenuItem)updateMenuItem).HideDropDown();
			for (var i = 0; i < updateAutoDeleteMenu.DropDownItems.Count; i++) {
				((ToolStripMenuItem)updateAutoDeleteMenu.DropDownItems[i]).Checked =
					(e.ClickedItem == updateAutoDeleteMenu.DropDownItems[i]);
			}
			var min = new int[]{0, 5, 10, 15, 20, 30, 60, 360};
			var delMin = 0;
			for (var i = 0; i < updateAutoDeleteMenu.DropDownItems.Count; i++) {
				if (((ToolStripMenuItem)updateAutoDeleteMenu.DropDownItems[i]).Checked)
					delMin = i;
			}
			config.set("liveListDelMinutes", min[delMin].ToString());
			Task.Factory.StartNew(() => deleteLiveListTime());
		}
		public char getCategoryChar() {
			for (var i = 0; i < categoryBtnPanel.Controls.Count; i++) {
				var b = (System.Windows.Forms.RadioButton)
					categoryBtnPanel.Controls[i];
				if (b.Checked) return b.Text[0];
			}
			return '全';
		}
		void UpdateAutoUpdateStartMenuClick(object sender, EventArgs e)
		{
			liveCheck.startAutoUpdate();
			updateAutoUpdateStartMenu.Enabled = false;
			updateAutoUpdateStopMenu.Enabled = true;
		}
		void UpdateAutoUpdateStopMenuClick(object sender, EventArgs e)
		{
			liveCheck.stopAutoUpdate();
			updateAutoUpdateStartMenu.Enabled = true;
			updateAutoUpdateStopMenu.Enabled = false;
		}
		void UpdateCateCategoryMenuDropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			//((ToolStripMenuItem)updateMenuItem).HideDropDown();
			for (var i = 0; i < updateCateCategoryMenu.DropDownItems.Count; i++) {
				((ToolStripMenuItem)updateCateCategoryMenu.DropDownItems[i]).Checked =
					(e.ClickedItem == updateCateCategoryMenu.DropDownItems[i]);
			}
			//var min = new int[]{0, 5, 10, 15, 20, 30, 60, 360};
			var index = 0;
			for (var i = 0; i < updateCateCategoryMenu.DropDownItems.Count; i++) {
				if (((ToolStripMenuItem)updateCateCategoryMenu.DropDownItems[i]).Checked)
					index = i;
			}
			config.set("cateCategoryType", index.ToString());
		}
		void UpdateTopFavoriteMenuCheckedChanged(object sender, EventArgs e)
		{
			var v = ((ToolStripMenuItem)sender).Checked;;
			config.set("FavoriteUp", v.ToString().ToLower());
			
			Task.Factory.StartNew(() => resetLiveList());
		}
		void UpdateAutoSortMenuCheckedChanged(object sender, EventArgs e)
		{
			config.set("AutoSort", ((ToolStripMenuItem)sender).Checked.ToString().ToLower());
		}
		void UpdateHideMemberOnlyWithoutFavoriteMenuClick(object sender, EventArgs e)
		{
			updateHideMemberOnlyWithFavoriteMenu.Checked = false;
			updateHideMemberOnlyWithoutFavoriteMenu.Checked = 
					!updateHideMemberOnlyWithoutFavoriteMenu.Checked;
			
			config.set("BlindOnlyA", ((ToolStripMenuItem)sender).Checked.ToString().ToLower());
			Task.Factory.StartNew(() => resetLiveList());
		}
		void UpdateHideMemberOnlyWithFavoriteMenuClick(object sender, EventArgs e)
		{
			updateHideMemberOnlyWithoutFavoriteMenu.Checked = false;
			updateHideMemberOnlyWithFavoriteMenu.Checked = 
					!updateHideMemberOnlyWithFavoriteMenu.Checked;
			config.set("BlindOnlyB", ((ToolStripMenuItem)sender).Checked.ToString().ToLower());
			Task.Factory.StartNew(() => resetLiveList());
		}
		
		void UpdateHideQuestionCategoryMenuCheckedChanged(object sender, EventArgs e)
		{
			config.set("BlindQuestion", ((ToolStripMenuItem)sender).Checked.ToString().ToLower());
		}
		public void upLiveListFavorite() {
			try  {
				//var favorites = liveListDataSource.Select(li => !string.IsNullOrEmpty(li.Favorite));
				//liveListDataSource.
				//formAction(() => {
					var cateChar = getCategoryChar();
					
					var favoriteList = new List<LiveInfo>();
					foreach (var f in liveListDataSource) 
						if (!string.IsNullOrEmpty(f.favorite) &&
						    	f.isDisplay(cateChar))
							favoriteList.Add(f);
					
					favoriteList.Reverse();
					
					var sI = liveList.FirstDisplayedScrollingRowIndex;
					formAction(() => {
						foreach (var f in favoriteList) liveListDataSource.Remove(f);
						
						foreach (var f in favoriteList) {
							liveListDataSource.Insert(0, f);
						}
					});
					setScrollIndex(liveList, sI);
					
					
					//if (scrollIndex > liveList.Rows.Count -1 && scrollIndex > 0) 
					//	scrollIndex = liveList.Rows.Count -1;
//					
					
				//});
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
		}
		
		void LiveListSorted(object sender, EventArgs e)
		{
			util.debugWriteLine("liveListSorted");
			Task.Factory.StartNew(() => {
				if (bool.Parse(config.get("FavoriteUp"))) {
					liveListLockAction(() => upLiveListFavorite());
				}
			});
		}
		/*
		void LiveListSearchBtnClick(object sender, EventArgs e)
		{
			if (liveListSearchText.Text == "") return;
			var searchStr = liveListSearchText.Text;
			var cur = liveList.CurrentCell;
			if (cur == null || cur.RowIndex == -1) return;
			var targetColumns = new int[] {2, 3, 4, 5, 6, 7, liveList.Columns.Count - 1};
			for (var i = cur.RowIndex; i < liveList.RowCount; i++) {
				foreach (var c in targetColumns) {
					if (i == cur.RowIndex && c <= cur.ColumnIndex) continue;
					if (liveList[c, i].Value == null) {
						util.debugWriteLine("search null " + liveList[2, i].Value + " " + liveList[3, i].Value);
						continue;
					}
					var t = liveList[c, i].Value.ToString();
					if (util.getRegGroup(t, "(" + searchStr + ")") != null) {
						if (!liveList.Columns[c].Visible) continue;
						liveList.CurrentCell = liveList[c, i];
						return;
					}
					
				}
			}
			for (var i = 0; i < cur.RowIndex + 1; i++) {
				foreach (var c in targetColumns) {
					if (i == cur.RowIndex && c >= cur.ColumnIndex) continue;
					var t = liveList[c, i].Value;
					if (t == null) continue;
					if (util.getRegGroup(t.ToString(), "(" + searchStr + ")") != null) {
						if (!liveList.Columns[c].Visible) continue;
						liveList.CurrentCell = liveList[c, i];
						return;
					}
					
				}
			}
			if (util.getRegGroup(cur.Value.ToString(), "(" + searchStr + ")") != null) return;
			System.Windows.Forms.MessageBox.Show("見つかりませんでした");
		}
		*/
		
		public void LiveListSearchBtnClick(object sender, EventArgs e)
		{
			liveListSearchStr = liveListSearchText.Text.Split(new char[]{' ', '　'});
			//liveListSearchCore();
			_resetLiveList();
		}
		/*
		public void liveListSearchCore() {
			//liveList.CurrentCell = null;
			var targetColumns = new int[] {2, 3, 4, 5, 6, 7, liveList.Columns.Count - 1};
			try {
				for (var i = liveListDataSource.Count - 1; i > -1 ; i--) {
					if (liveListSearchStr.Length == 1 && liveListSearchStr[0] == "") {
						liveList.Rows[i].Visible = true;
						continue;
					}
					var li = (LiveInfo)liveList.Rows[i].DataBoundItem;
					if (!isLiveListSearchVisible(li)) {
						formAction(() => {
							liveListDataSource.Remove(li);
							liveListDataReserve.Add(li);
						});
					}
				}
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace);
			}
		}
		*/
		bool isLiveListSearchVisible(LiveInfo li) {
			try {
				//var searchStr = liveListSearchStr;
				var sa = liveListSearchStr;
				var c = new string[]{li.title, li.hostName, li.comName, li.comId, li.lvId, li.description, li.memo, li.type};
				foreach (var s in sa) {
					var isDisplay = false;
					foreach (var i in c)
						if (i != null && i.IndexOf(s) > -1) isDisplay = true;
					if (!isDisplay) return false;
				}
				return true;
        	} catch (Exception e) {
        		util.debugWriteLine(e.Message + e.Source + e.StackTrace);
        		return true;
        	}

		}
		void LiveListUpdateSamuneMenuClick(object sender, EventArgs e)
		{
			var cur = liveList.CurrentCell;
			if (cur == null || cur.RowIndex == -1) return;
			var li = liveListDataSource[cur.RowIndex];
			if (li.thumbnailUrl == null) {
				util.debugWriteLine("liveListUpdateSamuneMenuClick li.thumbnailUrl null");
				return;
			}
			
			var img = ThumbnailManager.getImage(li.thumbnailUrl);
			if (img == null) return;
			li.thumbnail = new Bitmap(img, 50, 50);
			if (bool.Parse(config.get("liveListCacheIcon")))
				ThumbnailManager.saveImage(img, li.comId);
			else ThumbnailManager.deleteImageId(li.comId);
			
			try {
				var rowI = liveListDataSource.IndexOf(li);
				if (rowI == -1) return;
				liveList.UpdateCellValue(1, rowI);
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
			/*
			if (img == null) return;
			
			ThumbnailManager.saveImage(img, liveListDataSource[cur.RowIndex].comId);
			liveListDataSource[cur.RowIndex].thumbnail = img;
			liveList.UpdateCellValue(1, cur.RowIndex);
			*/
		}
		void LiveListWriteSamuneMemoMenuClick(object sender, EventArgs e)
		{
			var cur = liveList.CurrentCell;
			if (cur == null || cur.RowIndex == -1) return;
			var li = liveListDataSource[cur.RowIndex];
			
			var originThumb = ThumbnailManager.getThumbnailRssUrl(li.thumbnailUrl, false, true);
			if (originThumb == null) return;
			
			var img = ThumbnailManager.writeMemo(originThumb, int.Parse(config.get("fontSize")));
			if (img == null) return;
			li.thumbnail = new Bitmap(img, 50, 50);
			ThumbnailManager.saveImage(img, li.comId);
			
			try {
				var rowI = liveListDataSource.IndexOf(li);
				if (rowI == -1) return;
				liveList.UpdateCellValue(1, rowI);
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		
		void LiveListNGthumbnailMenuClick(object sender, EventArgs e)
		{
			var cur = liveList.CurrentCell;
			if (cur == null || cur.RowIndex == -1) return;
			var li = liveListDataSource[cur.RowIndex];
			
			var originThumb = ThumbnailManager.getThumbnailRssUrl(li.thumbnailUrl, false, true);
			if (originThumb == null) return;
			
			var img = ThumbnailManager.writeNG(originThumb);
			if (img == null) return;
			li.thumbnail = new Bitmap(img, 50, 50);
			ThumbnailManager.saveImage(img, li.comId);
			
			try {
				var rowI = liveListDataSource.IndexOf(li);
				if (rowI == -1) return;
				liveList.UpdateCellValue(1, rowI);
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		
		void AlartListRowEnter(object sender, DataGridViewCellEventArgs e)
		{
			util.debugWriteLine("alartlist row enter " + e.RowIndex + " " + e.ColumnIndex);
			var isUserMode = ((DataGridView)sender).Name == "userAlartList";
			if (e.RowIndex == -1)
				return;
			var ai = isUserMode ? 
				userAlartListDataSource[e.RowIndex] : alartListDataSource[e.RowIndex];
			
			var isExistComThumb = false;
			System.Drawing.Image img = null;
			if (!string.IsNullOrEmpty(ai.communityId)) {
				isExistComThumb = ThumbnailManager.isExist(ai.communityId, out img);
			}
			
			try {
				if (!isUserMode) {
					comThumbBox.Image = isExistComThumb ?
							new Bitmap(img, comThumbBox.Size) :
						new Bitmap(System.Drawing.Image.FromFile(util.getJarPath()[0] + "/ImageCommunity/no thumb com.jpg"), comThumbBox.Size);
				}
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
			
			var isExistUserThumb = false;
			if (!string.IsNullOrEmpty(ai.hostId)) {
				isExistUserThumb = ThumbnailManager.isExist(ai.hostId, out img);
			}
			try {
				var _img = isExistUserThumb ?
						new Bitmap(img, userThumbBox.Size) :
					new Bitmap(System.Drawing.Image.FromFile(util.getJarPath()[0] + "/ImageUser/blank.jpg"), userThumbBox.Size);
				
				if (isUserMode) favoriteUserThumbBox.Image = _img;
				else userThumbBox.Image = _img;

					
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
			
		}
		
		void DisplayOnAirTabMenuDropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			var i = displayOnAirTabMenu.DropDownItems.IndexOf(e.ClickedItem);
			liveList.Columns[i + 1].Visible = !liveList.Columns[i + 1].Visible;
			((ToolStripMenuItem)displayOnAirTabMenu.DropDownItems[i]).Checked = liveList.Columns[i + 1].Visible; 
		}
		List<string> deleteColorUserIdList = new List<string>();
		List<string> errorColorUserIdList = new List<string>();
		List<string> deleteColorCmChList = new List<string>();
		List<string> errorColorCmChIdList = new List<string>();
		public void alartListExistColorChange(string notExistId, bool isUser, int setType) {
			//setType 0-ok 1-delete 2-error
			//Color getColor = delegate(int type, int rowIndex) => {
//			foreach (DataGridViewCell b in alartList.Rows[0].Cells)
//				util.debugWriteLine(b.ColumnIndex + " " + b.Value);
			
			//formAction(() => {
			        for (var i = 0; i < 10; i++) {
						try {
		           			foreach (DataGridViewRow r in alartList.Rows) {
				           		//util.debugWriteLine("r.cells.value " + r.Cells[2].Value + " " + r.Cells[1].Value);
				           		if (isUser && alartListDataSource[r.Index].hostId == notExistId) {
				           			formAction(() => {
					           			alartListDataSource[r.Index].userIdColorType = setType;
					           			alartList.UpdateCellValue(1, r.Index);
									});
								}
				           		if (!isUser && alartListDataSource[r.Index].communityId == notExistId) {
				           			formAction(() => {
					           			alartListDataSource[r.Index].comIdColorType = setType;
					           			alartList.UpdateCellValue(0, r.Index);
				           			});
				           		}
				           	}
				           	foreach (DataGridViewRow r in userAlartList.Rows) {
				           		//util.debugWriteLine("r.cells.value " + r.Cells[2].Value + " " + r.Cells[1].Value);
				           		if (isUser && userAlartListDataSource[r.Index].hostId == notExistId) {
				           			formAction(() => {
					           			userAlartListDataSource[r.Index].userIdColorType = setType;
					           			userAlartList.UpdateCellValue(1, r.Index);
									});
								}
				           	}
			           		break;
		           		} catch (Exception e) {
		           			util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
		           			Thread.Sleep(1000);
		           		}
			    	}
	           	//});
		}
		//private bool[] checkMenuLock = new bool[3];
		public void setToolMenuStatusBar(string t) {
			formAction(() => {
				existCheckStatusBar.Text = t;
			});
		}
		void EditLineMenuClick(object sender, EventArgs e)
		{
			var isUserMode = !favoriteCommunityBtn.Checked;
			var list = isUserMode ? userAlartList : alartList;
			var dataSource = isUserMode ? userAlartListDataSource : alartListDataSource;
			
			if (list.SelectedCells.Count == 0) return;
			var min = int.MaxValue;
			foreach (DataGridViewCell c in list.SelectedCells)
				if (c.RowIndex < min) min = c.RowIndex;
			var ai = (AlartInfo)dataSource[min];
			
			//if (dataSource.IndexOf(ai) == -1) return;
			//var cur = list.CurrentCell;
			//if (cur.RowIndex == -1) return;
			openAddForm(null, ai, isUserMode);
		}
		public void alartListSetName(AlartInfo ai, bool isUser, string name) {
			formAction(() => {
				if (isUser) ai.hostName = name;
				else ai.communityName = name;
				try {
					var columnI = isUser ? 3 : 2;
					for (var i = 0; i < alartListDataSource.Count; i++) {
						if (alartListDataSource[i] == ai)
							alartList.UpdateCellValue(columnI, i);
					}
					for (var i = 0; i < userAlartListDataSource.Count; i++) {
						if (userAlartListDataSource[i] == ai)
							userAlartList.UpdateCellValue(columnI, i);
					}
				} catch (Exception e) {
					util.debugWriteLine(e.Message + e.Source + e.TargetSite);
				}
			});
		}
		void CheckExistsComMenuClick(object sender, EventArgs e)
		{
			toolMenuProcess.exitsCheckClicked(false);
		}
		
		void CheckExistsUserMenuClick(object sender, EventArgs e)
		{
			toolMenuProcess.exitsCheckClicked(true);
		}
		 
		
		void GetUserInfoFromComMenuClick(object sender, EventArgs e)
		{
			toolMenuProcess.getUserInfoFromComStart();
		}
		void BulkAddFromFollowComMenuClick(object sender, EventArgs e)
		{
			toolMenuProcess.addBulkFromFollowComStart();
		}
		void saveFormState() {
			var liveListWidth = new List<string>();
			foreach (DataGridViewColumn c in liveList.Columns)
				liveListWidth.Add(c.Width.ToString());
			config.set("LiveListColumnWidth", string.Join(",", liveListWidth.ToArray()));
			
			var alartListWidth = new List<string>();
			foreach (DataGridViewColumn c in alartList.Columns)
				alartListWidth.Add(c.Width.ToString());
			config.set("AlartListColumnWidth", string.Join(",", alartListWidth.ToArray()));
			
			var userAlartListWidth = new List<string>();
			foreach (DataGridViewColumn c in userAlartList.Columns)
				userAlartListWidth.Add(c.Width.ToString());
			config.set("UserAlartListColumnWidth", string.Join(",", userAlartListWidth.ToArray()));
			
			var taskListWidth = new List<string>();
			foreach (DataGridViewColumn c in taskList.Columns)
				taskListWidth.Add(c.Width.ToString());
			config.set("TaskListColumnWidth", string.Join(",", taskListWidth.ToArray()));
			
			var logListWidth = new List<string>();
			foreach (DataGridViewColumn c in logList.Columns)
				logListWidth.Add(c.Width.ToString());
			config.set("LogListColumnWidth", string.Join(",", logListWidth.ToArray()));
			
			var historyListWidth = new List<string>();
			foreach (DataGridViewColumn c in historyList.Columns)
				historyListWidth.Add(c.Width.ToString());
			config.set("HistoryListColumnWidth", string.Join(",", historyListWidth.ToArray()));
			
			var notAlartListWidth = new List<string>();
			foreach (DataGridViewColumn c in notAlartList.Columns)
				notAlartListWidth.Add(c.Width.ToString());
			config.set("NotAlartListColumnWidth", string.Join(",", notAlartListWidth.ToArray()));
			
			var reserveHistoryListWidth = new List<string>();
			foreach (DataGridViewColumn c in reserveHistoryList.Columns)
				reserveHistoryListWidth.Add(c.Width.ToString());
			config.set("ReserveHistoryListColumnWidth", string.Join(",", reserveHistoryListWidth.ToArray()));
			
			config.set("HistoryPanelDistance", historySplitContainer.SplitterDistance.ToString());
			
			config.set("activeTab", tabControl1.SelectedIndex.ToString());
			config.set("favoriteActiveTab", favoriteCommunityBtn.Checked ? "0" : "1");
		}
		void setFormState() {
			try {
				var liveListWidth = config.get("LiveListColumnWidth");
				if (liveListWidth != "") {
					var w = liveListWidth.Split(',');
					for (var i = 0; i < w.Length; i++) {
						DataGridViewColumn c  = liveList.Columns[i];
						c.Width = int.Parse(w[i]);
					}
				}
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}

			try {
				var alartListWidth = config.get("AlartListColumnWidth");
				if (alartListWidth != "") {
					var w = alartListWidth.Split(',');
					for (var i = 0; i < w.Length; i++) {
						DataGridViewColumn c  = alartList.Columns[i];
						c.Width = int.Parse(w[i]);
					}
				}
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
			
			try {
				var userAlartListWidth = config.get("userAlartListColumnWidth");
				if (userAlartListWidth != "") {
					var w = userAlartListWidth.Split(',');
					for (var i = 0; i < w.Length; i++) {
						DataGridViewColumn c  = userAlartList.Columns[i];
						c.Width = int.Parse(w[i]);
					}
				}
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
			
			try {
				var taskListWidth = config.get("TaskListColumnWidth");
				if (taskListWidth != "") {
					var w = taskListWidth.Split(',');
					for (var i = 0; i < w.Length; i++) {
						DataGridViewColumn c  = taskList.Columns[i];
						c.Width = int.Parse(w[i]);
					}
				}
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
			
			try {
				var logListWidth = config.get("LogListColumnWidth");
				if (logListWidth != "") {
					var w = logListWidth.Split(',');
					for (var i = 0; i < w.Length; i++) {
						DataGridViewColumn c  = logList.Columns[i];
						c.Width = int.Parse(w[i]);
					}
				}
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
			
			try {
				var historyListWidth = config.get("HistoryListColumnWidth");
				if (historyListWidth != "") {
					var w = historyListWidth.Split(',');
					for (var i = 0; i < w.Length; i++) {
						DataGridViewColumn c  = historyList.Columns[i];
						c.Width = int.Parse(w[i]);
					}
				}
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
			
			try {
				var notAlartListWidth = config.get("NotAlartListColumnWidth");
				if (notAlartListWidth != "") {
					var w = notAlartListWidth.Split(',');
					for (var i = 0; i < w.Length; i++) {
						DataGridViewColumn c  = notAlartList.Columns[i];
						c.Width = int.Parse(w[i]);
					}
				}
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
			
			try {
				var reserveHistoryListWidth = config.get("ReserveHistoryListColumnWidth");
				if (reserveHistoryListWidth != "") {
					var w = reserveHistoryListWidth.Split(',');
					for (var i = 0; i < w.Length; i++) {
						DataGridViewColumn c  = reserveHistoryList.Columns[i];
						c.Width = int.Parse(w[i]);
					}
				}
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
			
			try {
				//var a = historySplitContainer.
				var historyPanelDistance = config.get("HistoryPanelDistance");
				if (historyPanelDistance != "") {
					if (historySplitContainer.Height != 0)
						historySplitContainer.SplitterDistance = int.Parse(historyPanelDistance);
					else 
						historyContainerDistance = int.Parse(historyPanelDistance);
				}
				//historySplitContainer.SplitterDistance = int.Parse(historyPanelDistance);
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
			
			tabControl1.SelectedIndex = int.Parse(config.get("activeTab"));
			if (config.get("favoriteActiveTab") == "0") favoriteCommunityBtn.Checked = true;
			else favoriteUserBtn.Checked = true;
		}
		
		
		void ColorColumnMenuDropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			//displayMenuItem.HideDropDown();
			var item = ((ToolStripMenuItem)e.ClickedItem);
			var index = colorColumnMenu.DropDownItems.IndexOf(e.ClickedItem);
			item.Checked = !item.Checked;
			alartListColorColumns[index] = item.Checked;

			while (true) {
				try {
					for (var i = alartList.FirstDisplayedScrollingRowIndex; i < alartListDataSource.Count; i++)
						if (i != -1) alartList.UpdateCellValue(index, i);
					for (var i = userAlartList.FirstDisplayedScrollingRowIndex; i < userAlartListDataSource.Count; i++)
						if (i != -1) userAlartList.UpdateCellValue(index, i);
					break;
				} catch (Exception ee) {
					util.debugWriteLine(ee.Message + ee.Source + ee.TargetSite + ee.StackTrace);
				}
			}
			var buf = "";
			var items = ((ToolStripMenuItem)sender).DropDownItems;
			foreach (var i in items)
				buf += ((ToolStripMenuItem)i).Checked ? "1" : "0";
			config.set("ColorAlartListColumns", buf);
		}
		public void changedListContent() {
			lastChangeListDt = DateTime.Now;
		}
		void DisableFollowMenuCheckedChanged(object sender, EventArgs e)
		{
			var comFollowIndex = -1;
			foreach (DataGridViewColumn c in alartList.Columns) 
				if (c.Name == "ﾁｬﾝﾈﾙﾌｫﾛｰ") comFollowIndex = c.Index;
			
			var isFollow = disableFollowMenu.Checked;
			//((DataGridViewButtonColumn)alartList.Columns[comFollowIndex]).di
			alartList.Columns[comFollowIndex].ReadOnly = isFollow;
			alartList.Columns[comFollowIndex + 1].ReadOnly = isFollow;
			
			formAction(() => {
				for (var i = 0; i < 10; i++) {
					try {
	           			foreach (DataGridViewRow r in alartList.Rows) {
			           		alartList.UpdateCellValue(comFollowIndex, r.Index);
							alartList.UpdateCellValue(comFollowIndex + 1, r.Index);
			           	}
		           		break;
	           		} catch (Exception ee) {
	           			util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
	           			Thread.Sleep(1000);
	           		}
	           	}
			});
			
			config.set("disableFollowColumns", isFollow.ToString().ToLower());
		}
		void AlartListUpOnAirMenuCheckedChanged(object sender, EventArgs e)
		{
			var upOnAir = alartListUpOnAirMenu.Checked ? 1 : 0;
			config.set("alartListUpOnAirMode", upOnAir.ToString());
			alartListDataSource.upOnAir = userAlartListDataSource.upOnAir = upOnAir;
			sortAlartList(false);
			sortAlartList(true);
		}
		void AlartListCellValidating(object sender, DataGridViewCellValidatingEventArgs e)
		{
			if (e.ColumnIndex == 4 && alartListDataSource[e.RowIndex].isCustomKeyword &&
			    (string)e.FormattedValue != alartListDataSource[e.RowIndex].Keyword)
				MessageBox.Show("カスタム設定はテーブル上から編集できません");
		}
		void resetColorSetting() {
			recentColor = bool.Parse(config.get("IsAlartListRecentColor")) ?
					Color.Empty : ColorTranslator.FromHtml(config.get("recentColor"));
			followerOnlyColor = bool.Parse(config.get("IsFollowerOnlyOtherColor")) ?
				ColorTranslator.FromHtml(config.get("followerOnlyColor")) : Color.Empty;
			evenRowsColor = ColorTranslator.FromHtml(config.get("evenRowsColor"));
			
			while (true) {
				try {
					for (var i = 0; i < alartList.Rows.Count; i++) 
						alartList.UpdateCellValue(8, i);
					break;
				} catch (Exception e) {
					util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				}
			}
			try {
				formAction(() => {
		           	var t = tabControl1.SelectedTab.Text;
		           	var dgv = t == "放送中" ? liveList : 
							(t == "お気に入り設定" ? alartList : 
						 	(t == "予約起動" ? taskList : 
						  	(t == "Twitter" ? twitterList : 
						   (t == "通知履歴" ? historyList : null))));
					if (dgv != null) {
						dgv.Refresh();
						if (dgv == historyList) 
							notAlartList.Refresh();
					}
				});
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.StackTrace);
			}
		}
		
		void LogListMenuOpening(object sender, CancelEventArgs e)
		{
			var cur = logList.CurrentCell;
			if (cur.RowIndex == -1) return;
			/*
			var type = logListDataSource[cur.RowIndex].type;
			var normalLogMenuIndexArr = new int[]{4, 8, 9};
			for (var i = 0; i < logListMenu.Items.Count; i++)
				logListMenu.Items[i].Visible = 
						Array.IndexOf(normalLogMenuIndexArr, i) > -1;
			*/
		}
		void LogListCopyMessageMenuClick(object sender, EventArgs e)
		{
			var cur = logList.CurrentCell;
			if (cur.RowIndex == -1) return;
			
			var li = logListDataSource[cur.RowIndex];
			if (string.IsNullOrEmpty(li.msg)) return;
			try {
				Clipboard.SetText(li.msg);
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		void LogListDeleteRowMenuClick(object sender, EventArgs e)
		{
			var cur = logList.CurrentCell;
			if (cur.RowIndex == -1) return;
			
			try {
				logListDataSource.RemoveAt(cur.RowIndex);
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		
		void HistoryListOpenUrlMenuClick(object sender, EventArgs e)
		{
			DataGridView list;
			SortableBindingList<HistoryInfo> ds;
			if (tabControl1.SelectedTab.Text == "通知履歴") {
				list = historyList;;
				ds = historyListDataSource;
			} else {
				list = reserveHistoryList;
				ds = reserveHistoryListDataSource;
			}
			
			try {
				var selectedLvList = new List<string>();
				foreach (DataGridViewCell c in list.SelectedCells) {
					try {
						var hi = (HistoryInfo)ds[c.RowIndex];
						if (string.IsNullOrEmpty(hi.lvid)) continue;
						
						var url = "https://live.nicovideo.jp/watch/lv" + util.getRegGroup(hi.lvid, "(\\d+)");
						if (selectedLvList.IndexOf(url) > -1) continue;
						selectedLvList.Add(url);
					} catch (Exception ee) {
						util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
					}
				}
				if (selectedLvList.Count > 10 && MessageBox.Show(selectedLvList.Count + "の放送が選択されています。このまま開きますか？", "", MessageBoxButtons.YesNo) 
				    	!= DialogResult.Yes) return;
				foreach (var url in selectedLvList)
					util.openUrlBrowser(url, config);
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		void HistoryListOpenCommunityUrlMenuClick(object sender, EventArgs e)
		{
			DataGridView list;
			SortableBindingList<HistoryInfo> ds;
			if (tabControl1.SelectedTab.Text == "通知履歴") {
				list = historyList;;
				ds = historyListDataSource;
			} else {
				list = reserveHistoryList;
				ds = reserveHistoryListDataSource;
			}
			
			try {
				var selectedLvList = new List<string>();
				foreach (DataGridViewCell c in list.SelectedCells) {
					try {
						var hi = (HistoryInfo)ds[c.RowIndex];
						if (string.IsNullOrEmpty(hi.communityId)) continue;
						
						var isChannel = hi.communityId.IndexOf("ch") > -1;
						var url = (isChannel) ? 
								("https://ch.nicovideo.jp/" + hi.communityId) :
								("https://com.nicovideo.jp/community/" + hi.communityId);
						if (selectedLvList.IndexOf(url) > -1) continue;
						selectedLvList.Add(url);
					} catch (Exception ee) {
						util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
					}
				}
				if (selectedLvList.Count > 10 && MessageBox.Show(selectedLvList.Count + "の放送が選択されています。このまま開きますか？", "", MessageBoxButtons.YesNo) 
				    	!= DialogResult.Yes) return;
				foreach (var url in selectedLvList)
					util.openUrlBrowser(url, config);
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		void HistoryListOpenUserUrlMenuClick(object sender, EventArgs e)
		{
			DataGridView list;
			SortableBindingList<HistoryInfo> ds;
			if (tabControl1.SelectedTab.Text == "通知履歴") {
				list = historyList;;
				ds = historyListDataSource;
			} else {
				list = reserveHistoryList;
				ds = reserveHistoryListDataSource;
			}
			var cur = list.CurrentCell;
			if (cur.RowIndex == -1) return;
			
			var li = ds[cur.RowIndex];
			if (string.IsNullOrEmpty(li.userId)) return;
			
			var url = "https://www.nicovideo.jp/user/" + li.userId;
			util.openUrlBrowser(url, config);
		}
		
		void HistoryListCopyUrlMenuClick(object sender, EventArgs e)
		{
			DataGridView list;
			SortableBindingList<HistoryInfo> ds;
			if (tabControl1.SelectedTab.Text == "通知履歴") {
				list = historyList;;
				ds = historyListDataSource;
			} else {
				list = reserveHistoryList;
				ds = reserveHistoryListDataSource;
			}
			try {
				var selectedLvList = new List<string>();
				foreach (DataGridViewCell c in list.SelectedCells) {
					try {
						var hi = (HistoryInfo)ds[c.RowIndex];
						if (string.IsNullOrEmpty(hi.lvid)) continue;
						
						var url = "https://live.nicovideo.jp/watch/lv" + util.getRegGroup(hi.lvid, "(\\d+)");
						if (selectedLvList.IndexOf(url) > -1) continue;
						selectedLvList.Add(url);
					} catch (Exception ee) {
						util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
					}
				}
				Clipboard.SetText(string.Join("\n", selectedLvList.ToArray()));
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		void HistoryListCopyCommunityUrlMenuClick(object sender, EventArgs e)
		{
			DataGridView list;
			SortableBindingList<HistoryInfo> ds;
			if (tabControl1.SelectedTab.Text == "通知履歴") {
				list = historyList;;
				ds = historyListDataSource;
			} else {
				list = reserveHistoryList;
				ds = reserveHistoryListDataSource;
			}
			try {
				var selectedLvList = new List<string>();
				foreach (DataGridViewCell c in list.SelectedCells) {
					try {
						var hi = (HistoryInfo)ds[c.RowIndex];
						if (string.IsNullOrEmpty(hi.communityId)) continue;
						
						var isChannel = hi.communityId.IndexOf("ch") > -1;
						var url = (isChannel) ? 
								("https://ch.nicovideo.jp/" + hi.communityId) :
								("https://com.nicovideo.jp/community/" + hi.communityId);
						
						if (selectedLvList.IndexOf(url) > -1) continue;
						selectedLvList.Add(url);
					} catch (Exception ee) {
						util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
					}
				}
				Clipboard.SetText(string.Join("\n", selectedLvList.ToArray()));
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		void HistoryListCopyUserUrlMenuClick(object sender, EventArgs e)
		{
			DataGridView list;
			SortableBindingList<HistoryInfo> ds;
			if (tabControl1.SelectedTab.Text == "通知履歴") {
				list = historyList;;
				ds = historyListDataSource;
			} else {
				list = reserveHistoryList;
				ds = reserveHistoryListDataSource;
			}
			var cur = list.CurrentCell;
			if (cur.RowIndex == -1) return;
			
			var li = ds[cur.RowIndex];
			if (string.IsNullOrEmpty(li.userId)) return;
			
			var url = "https://www.nicovideo.jp/user/" + li.userId;
			try {
				Clipboard.SetText(url);
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		void HistoryListDeleteRowMenuClick(object sender, EventArgs e)
		{
			try {
				DataGridView list;
				SortableBindingList<HistoryInfo> ds;
				if (tabControl1.SelectedTab.Text == "通知履歴") {
					list = historyList;;
					ds = historyListDataSource;
				} else {
					list = reserveHistoryList;
					ds = reserveHistoryListDataSource;
				}
			
				if (list.SelectedCells.Count == 0) return;
				var selectedList = new List<HistoryInfo>();
				foreach (DataGridViewCell c in list.SelectedCells) {
					try {
						var i = (HistoryInfo)ds[c.RowIndex];
						if (selectedList.IndexOf(i) > -1) continue;
						else selectedList.Add(i);
					} catch (Exception ee) {
						util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
					}
				}
				
				var ids = selectedList.Select(i => i.lvid);
				var idsStr = string.Join("、", ids);
				
				var id = string.IsNullOrEmpty(idsStr) ? "こ" : (idsStr);
				if (System.Windows.Forms.MessageBox.Show(id + "の行を削除していいですか？", "確認", MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;
				foreach (var i in selectedList)
					ds.Remove(i);
				changedListContent();
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
			
			/*
			
			var cur = historyList.CurrentCell;
			if (cur.RowIndex == -1) return;
			
			try {
				historyListDataSource.RemoveAt(cur.RowIndex);
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
			*/
		}
		public void addHistoryList(HistoryInfo hi, int historyListMax = -1) {
			if (historyListMax == -1) {
				historyListMax = int.Parse(config.get("maxHistoryDisplay"));
			}
			try {
				foreach (var _hi in historyListDataSource)
	       			if (_hi.lvid == hi.lvid) return;
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
			
       		formAction(() => {
				try {
			        var scrollIndex = historyList.FirstDisplayedScrollingRowIndex;
				    
			        if (historyListDataSource.Count >= historyListMax) {
				        var min = historyListDataSource.OrderBy((a) => a.dt).First();
				        historyListDataSource.Remove(min);
				        util.debugWriteLine("addHistoryList list max deleteNotifyIconRecentItem " + min.lvid);
				        //deleteNotifyIconRecentItem(min.lvid);
			        }
			        
	       	    	historyListDataSource.Insert(0, hi);
	       	    	if (scrollIndex != -1 && scrollIndex < historyListDataSource.Count - 1)
	       	    		historyList.FirstDisplayedScrollingRowIndex = scrollIndex;
	       	    	
	           	} catch (Exception e) {
	           		util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
	           		util.debugWriteLine("historyListDataSource.Insert? " + historyListDataSource.Count + " " + historyList.FirstDisplayedScrollingRowIndex);
	           	}
       	    });
		}
		public void addNotAlartList(HistoryInfo hi, int notHistoryListMax = -1) {
			if (notHistoryListMax == -1) 
				notHistoryListMax = int.Parse(config.get("maxNotAlartDisplay"));
			try {
				foreach (var _hi in notAlartListDataSource) 
					if (_hi.lvid == hi.lvid) return;
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
			
       		formAction(() => {
				try {
				    var scrollIndex = notAlartList.FirstDisplayedScrollingRowIndex;
				    
				    if (notAlartListDataSource.Count >= notHistoryListMax) {
				        var min = notAlartListDataSource.OrderBy((a) => a.dt).First();
				        notAlartListDataSource.Remove(min);
			        }
				    
	       	    	notAlartListDataSource.Insert(0, hi);
	       	    	if (scrollIndex != -1)
	       	    		notAlartList.FirstDisplayedScrollingRowIndex = scrollIndex;
	       	    	
	           	} catch (Exception e) {
	           		util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
	           	}
       	    });
		}
		void HistoryListCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			var hi = historyListDataSource[e.RowIndex];
			var style = historyList[e.ColumnIndex, e.RowIndex].Style;
			var defColor = (e.RowIndex % 2 != 0) ? 
					evenRowsColor
					: Color.FromName("window");
			//if (false && e.ColumnIndex == 1) {
			if (e.ColumnIndex == 5 && 
			           (!hi.isInListUser && !string.IsNullOrEmpty(hi.userId))) 
				style.BackColor = Color.FromArgb(255,255,150);
			else if (e.ColumnIndex == 6 && 
					(!hi.isInListCom && !string.IsNullOrEmpty(hi.communityId)))
				style.BackColor = Color.FromArgb(255,255,150);
			else if (historyListColorColumns[e.ColumnIndex] && historyListDataSource[e.RowIndex].onAirMode != 0) {
				Color color;
				if (recentColor == Color.Empty) {
					//color = hi.backColor == Color.White ? defColor : hi.backColor;
					color = hi.backColor;
				} else {
					if (hi.onAirMode == 1)
						color = recentColor;
					else {
						color = (followerOnlyColor == Color.Empty) ?
								recentColor : followerOnlyColor;
					}
				}
				style.BackColor =　color;
				//style.BackColor =　((recentColor == Color.Empty) ? historyListDataSource[e.RowIndex].backColor : recentColor);
			}
			else {
				style.BackColor = defColor;
			}
		}
		void HistoryListCellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
		{
			setMouseDownSelect(historyList, e);
		}
		void NotAlartListCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			var hi = notAlartListDataSource[e.RowIndex];
			if (e.ColumnIndex == 5) {
				e.CellStyle.BackColor = (hi.isInListUser || string.IsNullOrEmpty(hi.userName)) ?
					Color.White : Color.FromArgb(255,255,150);
			} else if (e.ColumnIndex == 6) {
				e.CellStyle.BackColor = (hi.isInListCom || string.IsNullOrEmpty(hi.communityId)) ?
					Color.White : Color.FromArgb(255,255,150);
			} else if (e.ColumnIndex == 7) {
				e.CellStyle.BackColor = (hi.isInListKeyword || string.IsNullOrEmpty(hi.keyword)) ?
					Color.White : Color.FromArgb(255,255,150);
			} else if (e.ColumnIndex == 8) {
				e.CellStyle.BackColor = hi.isInListMemberOnly ?
					Color.White : Color.FromArgb(255,255,150);
			} else {
				e.CellStyle.BackColor = (e.RowIndex % 2 != 0) ? 
					evenRowsColor
					: Color.FromName("window");
			}
		}
		
		void NotAlartListOpenUrlMenuClick(object sender, EventArgs e)
		{
			try {
				var selectedLvList = new List<string>();
				foreach (DataGridViewCell c in notAlartList.SelectedCells) {
					try {
						var hi = (HistoryInfo)notAlartListDataSource[c.RowIndex];
						if (string.IsNullOrEmpty(hi.lvid)) continue;
						
						var url = "https://live.nicovideo.jp/watch/lv" + util.getRegGroup(hi.lvid, "(\\d+)");
						if (selectedLvList.IndexOf(url) > -1) continue;
						selectedLvList.Add(url);
					} catch (Exception ee) {
						util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
					}
				}
				foreach (var url in selectedLvList)
					util.openUrlBrowser(url, config);
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		
		void NotAlartListOpenCommunityUrlMenuClick(object sender, EventArgs e)
		{
			try {
				var selectedLvList = new List<string>();
				foreach (DataGridViewCell c in notAlartList.SelectedCells) {
					try {
						var hi = (HistoryInfo)notAlartListDataSource[c.RowIndex];
						if (string.IsNullOrEmpty(hi.communityId)) continue;
						
						var isChannel = hi.communityId.IndexOf("ch") > -1;
						var url = (isChannel) ? 
								("https://ch.nicovideo.jp/" + hi.communityId) :
								("https://com.nicovideo.jp/community/" + hi.communityId);
						if (selectedLvList.IndexOf(url) > -1) continue;
						selectedLvList.Add(url);
					} catch (Exception ee) {
						util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
					}
				}
				foreach (var url in selectedLvList)
					util.openUrlBrowser(url, config);
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		
		void NotAlartListOpenUserUrlMenuClick(object sender, EventArgs e)
		{
			var cur = notAlartList.CurrentCell;
			if (cur.RowIndex == -1) return;
			
			var li = notAlartListDataSource[cur.RowIndex];
			if (string.IsNullOrEmpty(li.userId)) return;
			
			var url = "https://www.nicovideo.jp/user/" + li.userId;
			util.openUrlBrowser(url, config);
		}
		
		void NotAlartListCopyUrlMenuClick(object sender, EventArgs e)
		{
			try {
				var selectedLvList = new List<string>();
				foreach (DataGridViewCell c in notAlartList.SelectedCells) {
					try {
						var hi = (HistoryInfo)notAlartListDataSource[c.RowIndex];
						if (string.IsNullOrEmpty(hi.lvid)) continue;
						
						var url = "https://live.nicovideo.jp/watch/lv" + util.getRegGroup(hi.lvid, "(\\d+)");
						if (selectedLvList.IndexOf(url) > -1) continue;
						selectedLvList.Add(url);
					} catch (Exception ee) {
						util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
					}
				}
				Clipboard.SetText(string.Join("\n", selectedLvList.ToArray()));
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		
		void NotAlartListCopyCommunityUrlMenuClick(object sender, EventArgs e)
		{
			try {
				var selectedLvList = new List<string>();
				foreach (DataGridViewCell c in notAlartList.SelectedCells) {
					try {
						var hi = (HistoryInfo)notAlartListDataSource[c.RowIndex];
						if (string.IsNullOrEmpty(hi.communityId)) continue;
						
						var isChannel = hi.communityId.IndexOf("ch") > -1;
						var url = (isChannel) ? 
								("https://ch.nicovideo.jp/" + hi.communityId) :
								("https://com.nicovideo.jp/community/" + hi.communityId);
						if (selectedLvList.IndexOf(url) > -1) continue;
						selectedLvList.Add(url);
					} catch (Exception ee) {
						util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
					}
				}
				Clipboard.SetText(string.Join("\n", selectedLvList.ToArray()));
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
			/*
			var cur = notAlartList.CurrentCell;
			if (cur.RowIndex == -1) return;
			
			var li = notAlartListDataSource[cur.RowIndex];
			if (string.IsNullOrEmpty(li.communityId)) return;
			
			var isChannel = li.communityId.IndexOf("ch") > -1;
			var url = (isChannel) ? 
				("https://ch.nicovideo.jp/" + li.communityId) :
				("https://com.nicovideo.jp/community/" + li.communityId);
			
			try {
				Clipboard.SetText(url);
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
			*/
		}
		
		void NotAlartListCopyUserUrlMenuClick(object sender, EventArgs e)
		{
			var cur = notAlartList.CurrentCell;
			if (cur.RowIndex == -1) return;
			
			var li = notAlartListDataSource[cur.RowIndex];
			if (string.IsNullOrEmpty(li.userId)) return;
			
			var url = "https://www.nicovideo.jp/user/" + li.userId;
			try {
				Clipboard.SetText(url);
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		
		void NotAlartListDeleteRowMenuClick(object sender, EventArgs e)
		{
			try {
				if (notAlartList.SelectedCells.Count == 0) return;
				var selectedList = new List<HistoryInfo>();
				foreach (DataGridViewCell c in notAlartList.SelectedCells) {
					try {
						var i = (HistoryInfo)notAlartListDataSource[c.RowIndex];
						if (selectedList.IndexOf(i) > -1) continue;
						else selectedList.Add(i);
					} catch (Exception ee) {
						util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
					}
				}
				
				var ids = selectedList.Select(i => i.lvid);
				var idsStr = string.Join("、", ids);
				
				var id = string.IsNullOrEmpty(idsStr) ? "こ" : (idsStr);
				if (System.Windows.Forms.MessageBox.Show(id + "の行を削除していいですか？", "確認", MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;
				foreach (var i in selectedList)
					notAlartListDataSource.Remove(i);
				changedListContent();
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
			
			/*
			var cur = notAlartList.CurrentCell;
			if (cur.RowIndex == -1) return;
			
			try {
				notAlartListDataSource.RemoveAt(cur.RowIndex);
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
			*/
		}
		
		void NotAlartListCellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
		{
			setMouseDownSelect(notAlartList, e);
		}
		
		void GetComThumbBulkMenuClick(object sender, EventArgs e)
		{
			toolMenuProcess.getThumbBulk(false);
		}
		void GetUserThumbBulkMenuClick(object sender, EventArgs e)
		{
			toolMenuProcess.getThumbBulk(true);
		}
		
		void DisplayHistoryListMenuDropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			var i = displayHistoryListMenu.DropDownItems.IndexOf(e.ClickedItem);
			historyList.Columns[i].Visible = !historyList.Columns[i].Visible;
			((ToolStripMenuItem)displayHistoryListMenu.DropDownItems[i]).Checked = historyList.Columns[i].Visible; 
		}
		void DisplayNotAlartListMenuDropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			var i = displayNotAlartListMenu.DropDownItems.IndexOf(e.ClickedItem);
			notAlartList.Columns[i].Visible = !notAlartList.Columns[i].Visible;
			((ToolStripMenuItem)displayNotAlartListMenu.DropDownItems[i]).Checked = notAlartList.Columns[i].Visible; 
		}
		
		void HistoryListAddAlartListMenuClick(object sender, EventArgs e)
		{
			DataGridView list;
			SortableBindingList<HistoryInfo> ds;
			if (tabControl1.SelectedTab.Text == "通知履歴") {
				list = historyList;;
				ds = historyListDataSource;
			} else {
				list = reserveHistoryList;
				ds = reserveHistoryListDataSource;
			}
			var cur = list.CurrentCell;
			if (cur.RowIndex == -1) return;
			//formAction(() => 
				openAddForm(ds[cur.RowIndex].lvid);
			//);
		}
		
		void NotAlartListAddAlartListMenuClick(object sender, EventArgs e)
		{
			var cur = notAlartList.CurrentCell;
			if (cur.RowIndex == -1) return;
			//formAction(() => 
				openAddForm(notAlartListDataSource[cur.RowIndex].lvid);
			//);
		}
		public void checkHistoryLive() {
			while (true) {
				try {
					//var recentNum = 0;
					var c = historyListDataSource.Count;
					for (var i = 0; i < c; i++) {
						var hi = historyListDataSource[i];
						if (hi.onAirMode == 0) {
							deleteNotifyIconRecentItem(hi.lvid);
							util.debugWriteLine("checkHistoryLIve onairmode 0  deleteNotifyIconRecentItem " + hi.lvid);
							continue;
						}
						
						//util.debugWriteLine(i + " " + alartListDataSource[i].lastHosoDt + " " + alartList[7, i].Style.BackColor);
						util.debugWriteLine("check history live onair " + hi.lvid + " " + hi.type);
						var isOnAir = isOnAirLvid(hi.lvid, hi.type);
						
						if (!isOnAir) {
							historyListDataSource[i].onAirMode = 0;
							util.debugWriteLine("checkHistoryLIve !isOnAir deleteNotifyIconRecentItem " + historyListDataSource[i].lvid);
							deleteNotifyIconRecentItem(historyListDataSource[i].lvid);
						}
						//util.debugWriteLine("test recent live check i " + i);
						for (var j = 0; j < historyList.Columns.Count; j++)
							historyList.UpdateCellValue(j, i);
					}
					break;
					
				} catch (Exception e) {
					util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
					Thread.Sleep(1000);
				}
			}
		}
		
		void ColorHistoryColorColumnMenuDropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			//displayMenuItem.HideDropDown();
			var item = ((ToolStripMenuItem)e.ClickedItem);
			var index = colorHistoryColorColumnMenu.DropDownItems.IndexOf(e.ClickedItem);
			item.Checked = !item.Checked;
			historyListColorColumns[index] = item.Checked;

			while (true) {
				try {
					for (var i = historyList.FirstDisplayedScrollingRowIndex; i < historyListDataSource.Count; i++)
						if (i != -1) historyList.UpdateCellValue(index, i);
					break;
				} catch (Exception ee) {
					util.debugWriteLine(ee.Message + ee.Source + ee.TargetSite + ee.StackTrace);
				}
			}
			var buf = "";
			var items = ((ToolStripMenuItem)sender).DropDownItems;
			foreach (var i in items)
				buf += ((ToolStripMenuItem)i).Checked ? "1" : "0";
			config.set("ColorHistoryListRecentColumns", buf);
		}
		
		void DuplicateCheckMenuClick(object sender, EventArgs e)
		{
			while (true) {
				try {
					var dupliNum = 0;
					dupliNum += DuplicateCheckMenuClickCore(alartListDataSource, alartList);
					dupliNum += DuplicateCheckMenuClickCore(userAlartListDataSource, userAlartList);
					MessageBox.Show(dupliNum.ToString() + "件の重複が見つかりました");
					
					break;
				} catch (Exception ee) {
					util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
				}
			}
		}
		int DuplicateCheckMenuClickCore(SortableBindingList<AlartInfo> dataSource, DataGridView list)
		{
			try {
				var dupliUserAiList = new List<AlartInfo>();
				var dupliComAiList = new List<AlartInfo>();
				var dupliNum = 0;
				foreach (var ai in dataSource) {
					bool isDupliUser = false, isDupliCom = false;
					foreach (var _ai in dataSource) {
						if (ai == _ai) continue;
						if (!string.IsNullOrEmpty(ai.hostId) && _ai.hostId == ai.hostId) {
							isDupliUser = true;
							if (dupliUserAiList.IndexOf(ai) == -1 && dupliComAiList.IndexOf(ai) == -1) 
								dupliUserAiList.Add(ai);
							if (dupliUserAiList.IndexOf(_ai) == -1 && dupliComAiList.IndexOf(_ai) == -1) 
								dupliUserAiList.Add(_ai);
						}
						if (!string.IsNullOrEmpty(ai.communityId) && _ai.communityId == ai.communityId) {
							isDupliCom = true;
							if (dupliUserAiList.IndexOf(ai) == -1 && dupliComAiList.IndexOf(ai) == -1) 
								dupliComAiList.Add(ai);
							if (dupliUserAiList.IndexOf(_ai) == -1 && dupliComAiList.IndexOf(_ai) == -1) 
								dupliComAiList.Add(_ai);
						}
					}
					ai.userIdColorType = isDupliUser ? 2 : 0;
					ai.comIdColorType = isDupliCom ? 2 : 0;
					if (isDupliUser || isDupliCom) dupliNum++;
					
					if (isDupliUser)
						list.UpdateCellValue(1, dataSource.IndexOf(ai));
					if (isDupliCom)
						list.UpdateCellValue(0, dataSource.IndexOf(ai));
				}
				dupliUserAiList.Reverse();
				dupliComAiList.Reverse();
				foreach (var ai in dupliComAiList) {
					dataSource.Remove(ai);
					dataSource.Insert(0, ai);
				}
				foreach (var ai in dupliUserAiList) {
					dataSource.Remove(ai);
					dataSource.Insert(0, ai);
				}
				return dupliNum;
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				return -1;
			}
		}
		void HistorySplitContainerLayout(object sender, LayoutEventArgs e)
		{
			//Debug.WriteLine("aa sp layout " + historySplitContainer.Size);
			if (historyContainerDistance != 0 &&
			    	historySplitContainer.Height != 0) {
				historySplitContainer.SplitterDistance = historyContainerDistance;
				historyContainerDistance = 0;
			}
		}
		void UpdateOnlyFavoriteMenuCheckedChanged(object sender, System.EventArgs e)
		{
			config.set("FavoriteOnly", ((ToolStripMenuItem)sender).Checked.ToString().ToLower());
			Task.Factory.StartNew(() => resetLiveList());
		}
		void FavoriteListRadioBtnCheckedChanged(object sender, EventArgs e)
		{
			if (!((System.Windows.Forms.RadioButton)sender).Checked) return;
			FavoriteListRadioBtnCheckedUpdate();
		}
		void FavoriteListRadioBtnCheckedUpdate() {
			if (favoriteCommunityBtn.Checked) {
				favoriteCommunityPanel.BringToFront();
				favoriteUserPanel.SendToBack();
				//favoriteCommunityPanel.Visible = true;
				//favoriteUserPanel.Visible = false;
			} else {
				favoriteUserPanel.BringToFront();
				favoriteCommunityPanel.SendToBack();
				//favoriteCommunityPanel.Visible = false;
				//favoriteUserPanel.Visible = true;
			}
		}
		void UserAddBtnClick(object sender, EventArgs e)
		{
			util.debugWriteLine("userAddBtnClick " + userAddBtn.Text);
			
			var id = util.getRegGroup(userAddText.Text, "(\\d+)");
			if (id == null) {
				MessageBox.Show("IDを読み込めませんでした", "");
				userAddText.Select();
				return;
			}
			bool isFollow = false;
			var name = util.getUserName(id, out isFollow, check.container, true, config);
			if (name == null) {
				MessageBox.Show("ユーザー情報を取得できませんでした", "");
				userAddText.Select();
				return;
			}
			
			for (var i = 0; i < 10; i++) {
				try {
					foreach (var ai in userAlartListDataSource) {
						if (ai.hostId == id) {
							MessageBox.Show("既に登録されています", "");
							userAddText.Select();
							return;
						}
					}
					
					var behaviors = config.get("defaultBehavior").
							Split(',').ToDictionary(
		        					x => x.Split(':')[0].Replace("ChkBox", ""), 
		        					x => bool.Parse(x.Split(':')[1]));
					var textColor = ColorTranslator.FromHtml(config.get("defaultTextColor"));
					var backColor = ColorTranslator.FromHtml(config.get("defaultBackColor"));
					var isAutoReserve = bool.Parse(config.get("IsDefaultAutoReserve"));
					var soundtype = int.Parse(config.get("defaultSound"));
					var now = DateTime.Now.ToString("yyyy\"/\"MM\"/\"dd HH\":\"mm\":\"ss");
					var followStr = isFollow ? "フォロー解除する" : "フォローする";
					var _ai = new AlartInfo("", id, 
							"", name, "", now, false, false, 
							false, false, false, false, false, 
							false, false, false, false, false, 
							false, false, false, "",
							"フォローする", followStr, "", "", "True,True,True", 
							isAutoReserve, 0);
					_ai.setBehavior(behaviors);
					_ai.textColor = textColor;
					_ai.backColor = backColor;
					_ai.soundType = soundtype;
					userAlartListDataSource.Add(_ai);
					userAddText.Text = "";
					userAddText.Select();
					
					Task.Factory.StartNew(() => {
						new AlartListFileManager(true, this).save();
					});
					return;
				} catch (Exception ee) {
					util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
				}
				Thread.Sleep(1000);
			}
			MessageBox.Show("登録エラーでした", "");
			
		}
		
		
		
		void UserAlartListCurrentCellDirtyStateChanged(object sender, EventArgs e)
		{
			try {
				var cc = userAlartList.CurrentCell;
				if (cc is DataGridViewCheckBoxCell) {
					foreach (DataGridViewCell selectCells in userAlartList.SelectedCells) {
						if (!(selectCells is DataGridViewCheckBoxCell)) return;
						if (selectCells.ColumnIndex < 10 || selectCells.ColumnIndex > 24) return;
					}
					foreach (DataGridViewCheckBoxCell selectCells in userAlartList.SelectedCells) {
						selectCells.Value = (bool)cc.Value;
					}
					
					foreach(DataGridViewCell _cc in userAlartList.SelectedCells) {
				        if (_cc.ColumnIndex >= 10 && _cc.ColumnIndex <= 24) {
							userAlartList.CommitEdit(DataGridViewDataErrorContexts.Commit);
							var target = ((AlartInfo)userAlartListDataSource[_cc.RowIndex]);
							if (_cc.ColumnIndex == 10) target.popup = (bool)(userAlartList[_cc.ColumnIndex, _cc.RowIndex].Value);
							if (_cc.ColumnIndex == 11) target.baloon = (bool)(userAlartList[_cc.ColumnIndex, _cc.RowIndex].Value);
							if (_cc.ColumnIndex == 12) target.browser = (bool)(userAlartList[_cc.ColumnIndex, _cc.RowIndex].Value);
							if (_cc.ColumnIndex == 13) target.mail = (bool)(userAlartList[_cc.ColumnIndex, _cc.RowIndex].Value);
							if (_cc.ColumnIndex == 14) target.sound = (bool)(userAlartList[_cc.ColumnIndex, _cc.RowIndex].Value);
							if (_cc.ColumnIndex == 15) target.appliA = (bool)(userAlartList[_cc.ColumnIndex, _cc.RowIndex].Value);
							if (_cc.ColumnIndex == 16) target.appliB = (bool)(userAlartList[_cc.ColumnIndex, _cc.RowIndex].Value);
							if (_cc.ColumnIndex == 17) target.appliC = (bool)(userAlartList[_cc.ColumnIndex, _cc.RowIndex].Value);
							if (_cc.ColumnIndex == 18) target.appliD = (bool)(userAlartList[_cc.ColumnIndex, _cc.RowIndex].Value);
							if (_cc.ColumnIndex == 19) target.appliE = (bool)(userAlartList[_cc.ColumnIndex, _cc.RowIndex].Value);
							if (_cc.ColumnIndex == 20) target.appliF = (bool)(userAlartList[_cc.ColumnIndex, _cc.RowIndex].Value);
							if (_cc.ColumnIndex == 21) target.appliG = (bool)(userAlartList[_cc.ColumnIndex, _cc.RowIndex].Value);
							if (_cc.ColumnIndex == 22) target.appliH = (bool)(userAlartList[_cc.ColumnIndex, _cc.RowIndex].Value);
							if (_cc.ColumnIndex == 23) target.appliI = (bool)(userAlartList[_cc.ColumnIndex, _cc.RowIndex].Value);
							if (_cc.ColumnIndex == 24) target.appliJ = (bool)(userAlartList[_cc.ColumnIndex, _cc.RowIndex].Value);
							util.debugWriteLine(target.appliA);
						}
					}
				}
				if (cc is DataGridViewComboBoxCell) {
					foreach (DataGridViewCell selectCells in userAlartList.SelectedCells) {
						if (!(selectCells is DataGridViewComboBoxCell)) return;
					}
					foreach (DataGridViewComboBoxCell selectCells in userAlartList.SelectedCells) {
						selectCells.Value = cc.Value;
					}
					
					foreach(DataGridViewCell _cc in userAlartList.SelectedCells) {
						userAlartList.CommitEdit(DataGridViewDataErrorContexts.Commit);
						var target = ((AlartInfo)userAlartListDataSource[cc.RowIndex]);
						if (cc.ColumnIndex == 5) target.IsAnd = (userAlartList[cc.ColumnIndex, cc.RowIndex].Value.ToString());
						if (cc.ColumnIndex == 25) target.SoundType = (userAlartList[cc.ColumnIndex, cc.RowIndex].Value.ToString());
					}
				}
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		
		void AlartListCellFormattingCommon(SortableBindingList<AlartInfo> dataSource, 
				DataGridView list, DataGridViewCellFormattingEventArgs e)
		{
			if (e.ColumnIndex == 8 && dataSource[e.RowIndex].recentColorMode != 0) {
				Color color;
				Color textColor;
				if (recentColor == Color.Empty) {
					color = dataSource[e.RowIndex].backColor;
					textColor = dataSource[e.RowIndex].textColor;
				} else {
					textColor = Color.Black;
					if (dataSource[e.RowIndex].recentColorMode == 1)
						color = recentColor;
					else {
						color = (followerOnlyColor == Color.Empty) ?
								recentColor : followerOnlyColor;
					}
				}
				list[8, e.RowIndex].Style.BackColor =　color;
				list[8, e.RowIndex].Style.ForeColor = textColor;
				
			} else if (e.ColumnIndex == 0 && dataSource[e.RowIndex].comIdColorType != 0) {
				e.CellStyle.BackColor = (dataSource[e.RowIndex].comIdColorType == 1) ? 
					Color.FromArgb(169, 169, 169) : Color.FromArgb(255, 255, 190);
			} else if (e.ColumnIndex == 1 && dataSource[e.RowIndex].userIdColorType != 0) {
				e.CellStyle.BackColor = (dataSource[e.RowIndex].userIdColorType == 1) ? 
					Color.FromArgb(169, 169, 169) : Color.FromArgb(255, 255, 190);
			} else if (e.ColumnIndex == 6 || e.ColumnIndex == 7) {
				var b = (DataGridViewButtonCell)list[e.ColumnIndex, e.RowIndex];
				Color c;
				if ((string)e.Value == "") c = Color.White;
				else if (b.ReadOnly) {
					c = Color.FromArgb(200,200,200);
				} else {
					c = (string)e.Value == "フォローする" ? Color.FromArgb(224,244,224) : Color.FromArgb(224,224,224);
				}
				b.Style.BackColor = c;
			} else if (alartListColorColumns[e.ColumnIndex]) {
				e.CellStyle.BackColor = dataSource[e.RowIndex].backColor;
				e.CellStyle.ForeColor = dataSource[e.RowIndex].textColor;
			} else {
				if (isNotifyOffColumn(e.ColumnIndex))
					e.CellStyle.BackColor = Color.FromArgb(211, 211, 211);
				else {
					e.CellStyle.BackColor = (e.RowIndex % 2 != 0) ? 
							evenRowsColor
							: Color.FromName("window");
				}
				
			}
		}
		
		void UserAlartListCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			AlartListCellFormattingCommon(userAlartListDataSource, userAlartList, e);
		}
		
		void UserAlartListCellParsing(object sender, DataGridViewCellParsingEventArgs e)
		{
			AlartListCellParsingCommon(userAlartListDataSource, e);
		}
		
		void UserAlartListDragDrop(object sender, DragEventArgs e)
		{
			try {
				util.debugWriteLine("dragdrop");
				
				var t = e.Data.GetData(DataFormats.Text).ToString();
				string id = null;
				var lv = util.getRegGroup(t, "(lv\\d+)");
				if (lv != null) id = lv;
				else {
					var user = util.getRegGroup(t, "user/(\\d+)");
					if (user != null) id = user;
				}
				if (id == null) return;
				
				
				Task.Factory.StartNew(() =>
				    formAction(() => 
						openAddForm(id, null, true)
					)
				);
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		void UserAlartListCellValidating(object sender, DataGridViewCellValidatingEventArgs e)
		{
			//nasi
		}
		void ReadNamarokuUserListMenuClick(object sender, EventArgs e)
		{
			var dialog = new OpenFileDialog();
			dialog.DefaultExt = ".ini";
			dialog.FileName = "favoriteuser";
			dialog.Filter = "INI形式(*.ini)|*.ini*";
			
			dialog.Multiselect = false;
			var result = dialog.ShowDialog();
			if (result != DialogResult.OK) return;
			
			//存在チェック
			//Task.Factory.StartNew(() => new AlartListFileManager().ReadNamarokuList(this, alartListDataSource, dialog.FileName, true));
			Task.Factory.StartNew(() => {
				new AlartListFileManager(true, this).ReadNamarokuList(this, alartListDataSource, dialog.FileName, false, true);
				new AlartListFileManager(true, this).save();
			});
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			util.debugWriteLine(util.getUnicodeToUtf8("\u30d4\u30ab\u30c1\u30e5\u30a6"));
			
			Task.Factory.StartNew(() => {
				var ttc = new TimeTableChecker(check, config);
				ttc.start();
			});
			/*
			int lvid = (int)(new Random().NextDouble() * 1000);
			var t = Task.Factory.StartNew(() => {
			                 	/*
			         	var res = util.getPageSource("http://live.nicovideo.jp/watch/lv321714635", check.container);
			         	var hg = new HosoInfoGetter();
			         	if (hg.get("http://live.nicovideo.jp/watch/lv321714635")) {
			         		util.debugWriteLine(hg.category + " " + hg.userId);
			         	}
			         	*
			         	var ri = new RssItem("title", "lv" + lvid.ToString(), "2019-09-17 18:50:00", " 番組へのお便り、解説・聞", "comname", "ch381", "", "http://live.nicovideo.jp/thumb/756037.jpg", "false", "");
				ri.category = new List<string>{"料理"};
				ri.type = "official";
				check.foundLive(new List<RssItem> {ri});
			});
			Task.Factory.StartNew(() => {
			         	mati(t);
			         });
			*/
		}
		/*
		public void cancelLockTask(CancellationTokenSource cts) {
			Task.Factory.StartNew(() => {
				try {
					for (var i = 0; i < 10; i++) {
		             	Thread.Sleep(1000);
		             	if (cts.IsCancellationRequested) return;
					}
					liveListLock = new object();
					
					util.debugWriteLine("addss");
					
	         	} catch (Exception e) {
	         		util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
	         	}
			}, cts.Token);
	    }
		public void cancelLockTaskCancel(CancellationTokenSource cts) {
			try {
				cts.Cancel();
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			} finally {
				cts.Dispose();
			}
		}
		*/
		void HistoryListRowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
		{
			var max = config.get("maxHistoryDisplay");
			sortHistoryList(historyList, historyListDataSource, max);
		}
		void NotAlartListRowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
		{
			var max = config.get("maxNotAlartDisplay");
			sortHistoryList(notAlartList, notAlartListDataSource, max);
		}
		void sortHistoryList(DataGridView list, SortableBindingList<HistoryInfo> data, string maxNum) {
			util.debugWriteLine("sortHistoryList " + maxNum);
			var max = int.Parse(maxNum);
			formAction(() => {
			    var scrollI = list.FirstDisplayedScrollingRowIndex;
	   	    	if (data.Count > max) {
					try {
						for (var i = 0; i < data.Count - max; i++) {
							var _min = data.Min(x => x.dt);
							for (var j = 0; j < data.Count; j++)
								if (data[j].dt == _min) data.RemoveAt(j);
						}
					} catch (Exception e) {
						util.debugWriteLine("sortHistory " + e.Message + e.Source + e.StackTrace + e.TargetSite);
					}
	   	    	}
				
				if (list.SortOrder == SortOrder.None) return;
				var direction = (list.SortOrder == SortOrder.Ascending) ? ListSortDirection.Ascending : ListSortDirection.Descending;
   		       	try {
					list.Sort(list.SortedColumn, direction);
   		       	} catch (Exception e) {
   		       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
   		       	}
				try {
					if (scrollI > -1 && scrollI < list.RowCount)
						list.FirstDisplayedScrollingRowIndex = scrollI;
				} catch (Exception e) {
					util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				}
			});
		}
		
		
		
		void LiveListRowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
		{
			/*
			formAction(() => {
			    var scrollI = liveList.FirstDisplayedScrollingRowIndex;
				if (liveList.SortOrder == SortOrder.None) return;
				var direction = (liveList.SortOrder == SortOrder.Ascending) ? ListSortDirection.Ascending : ListSortDirection.Descending;
   		       	try {
					liveListDataSource..Sort(liveList.SortedColumn, direction);
   		       	} catch (Exception ee) {
   		       		util.debugWriteLine(ee.Message + " " + ee.StackTrace + " " + ee.Source + " " + ee.TargetSite);
   		       	}
				liveList.FirstDisplayedScrollingRowIndex = scrollI;
			});
			*/
		}
		void UpdateMenuClick(object sender, EventArgs e)
		{
			var v = new UpdateForm(int.Parse(config.get("fontSize")));
			v.ShowDialog();
		}
		void FormColorMenuItemClick(object sender, EventArgs e)
		{
			var d = new ColorDialog();
			d.Color = BackColor;
			var r = d.ShowDialog();
			if (r == DialogResult.OK) {
				setBackColor(d.Color);
				config.set("alartBackColor", d.Color.ToArgb().ToString());
			}
		}
		private void setBackColor(Color color) {
			BackColor = color;
			var c = getChildControls(this);
			foreach (var _c in c)
				if (_c.GetType() == typeof(GroupBox) ||
				    _c.GetType() == typeof(System.Windows.Forms.Panel) || 
				    _c.GetType() == typeof(System.Windows.Forms.Form) ||
				   	_c.GetType() == typeof(System.Windows.Forms.TabPage) ||
				   	_c.GetType() == typeof(System.Windows.Forms.TabControl)) 
						_c.BackColor = color;
			
		}
		void CharacterColorMenuItemClick(object sender, EventArgs e)
		{
			var d = new ColorDialog();
			d.Color = label1.ForeColor;
			var r = d.ShowDialog();
			if (r == DialogResult.OK) {
				setForeColor(d.Color);
				config.set("alartForeColor", d.Color.ToArgb().ToString());
			}
		}
		private void setForeColor(Color color) {
			var c = getChildControls(this);
			foreach (var _c in c)
				if (_c.GetType() == typeof(GroupBox) ||
				    _c.GetType() == typeof(System.Windows.Forms.Label) || 
				    _c.GetType() == typeof(System.Windows.Forms.CheckBox)) _c.ForeColor = color;
		}
		private List<Control> getChildControls(Control c) {
			//util.debugWriteLine("cname " + c.Name);
			var ret = new List<Control>();
			foreach (Control _c in c.Controls) {
				var children = getChildControls(_c);
				ret.Add(_c);
				ret.AddRange(children);
				//util.debugWriteLine(c.Name + " " + children.Count);
			}
			//util.debugWriteLine(c.Name + " " + ret.Count);
			return ret;
		}
		private void setSort() {
			var s = new string[]{"live", "alart", "userAlart", "task",
					"history", "notAlart", "reserveHistory"};
			var l = new DataGridView[]{liveList, alartList, 
					userAlartList, taskList, historyList, notAlartList, 
					reserveHistoryList};
			for (var i = 0; i < s.Length; i++) {
				var _columna = config.get(s[i] + "ListSortColumn");
				if (_columna == "-1") continue;
				try {
					var acolumn = l[i].Columns[int.Parse(_columna)];
					var _order = config.get(s[i] + "ListSortOrder");
					if (_order == "no") continue;
					var direction = _order == "asc" ? ListSortDirection.Ascending : ListSortDirection.Descending;
					var _column = config.get(s[i] + "ListSortColumn");
					var column = l[i].Columns[int.Parse(_column)];
					var a = l[i].RowCount;
					var b = l[i].ColumnCount;
					//column.HeaderText;
					//column.SortMode = DataGridViewColumnSortMode.Automatic;
					//alartList.sort
					util.debugWriteLine("ddddd " + s[i] + " " + column.HeaderText);
					l[i].Sort(column, direction);
					
					
				} catch (Exception e) {
					util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
					//util.debugWriteLine("ddddd error " + s[i] + " " + column.HeaderText);
				}
			}
			
		}
		
		void AlartListSorted(object sender, EventArgs e)
		{
			//setSortConfig("alart", alartList);
		}
		
		void UserAlartListSorted(object sender, EventArgs e)
		{
			//setSortConfig("userAlart", userAlartList);
		}
		
		void TaskListSorted(object sender, EventArgs e)
		{
			//setSortConfig("task", taskList);
		}
		
		void HistoryListSorted(object sender, EventArgs e)
		{
			//setSortConfig("history", historyList);
		}
		
		void NotAlartListSorted(object sender, EventArgs e)
		{
			//setSortConfig("notAlart", notAlartList);
		}
		private void setSortConfig(string s, DataGridView l) {
			try {
				var order = l.SortOrder == SortOrder.None ? "no" : (l.SortOrder == SortOrder.Ascending ? "asc" : "desc");
				var column = l.Columns.IndexOf(l.SortedColumn);
				config.set(s + "ListSortOrder", order);
				config.set(s + "ListSortColumn", column.ToString());
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		private void saveSortState() {
			var s = new string[]{"live", "alart", "userAlart", "task", "history", "notAlart", "reserveHistory"};
			var l = new DataGridView[]{liveList, alartList, userAlartList, taskList, historyList, notAlartList, reserveHistoryList};
			for (var i = 0; i < s.Length; i++) setSortConfig(s[i], l[i]);
		}
		private void setCheckableMenuClosingEvent() {
			foreach (var m in displayMenuItem.DropDownItems)
				if (m.GetType() == typeof(ToolStripMenuItem))
					if (((ToolStripMenuItem)m).HasDropDownItems ||
					   		m == liveListColorColumnMenu)
						((ToolStripMenuItem)m).DropDown.Closing += cancelCloseMenuOnClick_Closing;
			displayMenuItem.DropDown.Closing += cancelMenu_Closing;
			
			foreach (var m in updateMenuItem.DropDownItems) {
				if (m.GetType() != typeof(ToolStripMenuItem)) continue;
				var _m = (ToolStripMenuItem)m;
				if (_m.HasDropDownItems)
					_m.DropDown.Closing += cancelCloseMenuOnClick_Closing;
			}
			updateMenuItem.DropDown.Closing += cancelMenu_Closing;
			
			foreach (var m in notifyMenuItem.DropDownItems) {
				if (m.GetType() != typeof(ToolStripMenuItem)) continue;
				var _m = (ToolStripMenuItem)m;
				if (_m.HasDropDownItems)
					_m.DropDown.Closing += cancelCloseMenuOnClick_Closing;
			}
		}
		private void cancelCloseMenuOnClick_Closing(object sender, 
				ToolStripDropDownClosingEventArgs e) {
			//util.debugWriteLine("closing reason " + e.CloseReason);
			if (e.CloseReason == ToolStripDropDownCloseReason.ItemClicked)
				e.Cancel = true;
		}
		private void cancelMenu_Closing(object sender, 
				ToolStripDropDownClosingEventArgs e) {
			if (e.CloseReason == ToolStripDropDownCloseReason.ItemClicked &&
    				(lastClickMenu == updateTopFavoriteMenu ||
    				lastClickMenu == updateOnlyFavoriteMenu ||
    				lastClickMenu == updateAutoSortMenu ||
					lastClickMenu == updateHideMemberOnlyWithoutFavoriteMenu ||
				    lastClickMenu == updateHideMemberOnlyWithFavoriteMenu ||
				    lastClickMenu == updateHideQuestionCategoryMenu ||
				    lastClickMenu == disableFollowMenu))
				e.Cancel = true;
		}
		public void setNotifyMenuHistory(List<RssItem> items) {
			formAction(() => setNotifyMenuHistoryCore(items));
		}
		private void setNotifyMenuHistoryCore(List<RssItem> items) {
			foreach (var _i in items)
				util.debugWriteLine("setNotifyMenuHistoryCore " + _i.lvId);
			try {
				for (var i = 0; i < historyListDataSource.Count; i++) {
					var item = items.Find(ri => ri.lvId == historyListDataSource[i].lvid); 
					if (item != null && historyListDataSource[i].onAirMode == 0) {
						items.Remove(item);
						util.debugWriteLine("setNotifyMenuHistoryCore not add " + item.lvId);
					}
				}
				
				var history = new List<KeyValuePair<DateTime, ToolStripMenuItem>>();
				for (var i = 0; i < notifyIconMenuStrip.Items.Count - 4; i++) {
					history.Add(new KeyValuePair<DateTime, ToolStripMenuItem>(((RssItem)notifyIconMenuStrip.Items[i].Tag).pubDateDt, (ToolStripMenuItem)notifyIconMenuStrip.Items[i]));
				}
				//del date
				var dtList = new List<DateTime>();
				dtList.AddRange(items.Select(x => x.pubDateDt).ToList());
				dtList.AddRange(history.Where(x => items.IndexOf((RssItem)x.Value.Tag) == -1).Select(x => x.Key));
				var delDtList = new List<DateTime>();
				if (dtList.Count > 20) {
					delDtList = dtList.OrderBy(x => x).Take(dtList.Count - 20).ToList();
					items = items.Where(x => delDtList.IndexOf(x.pubDateDt) == -1).ToList();
					history = history.Where(x => delDtList.IndexOf(x.Key) == -1).ToList();
					for (var i = notifyIconMenuStrip.Items.Count - 3; i > -1; i--) {
						var item = notifyIconMenuStrip.Items[i];
						if (item.Tag == null) continue;
						var ri = (RssItem)item.Tag;
						if (delDtList.IndexOf(ri.pubDateDt) > -1)
							notifyIconMenuStrip.Items.Remove(item);
					}
				}
				
				foreach (var item in history) {
					try {
						if (notifyIconMenuStrip.Items.IndexOf(item.Value) > -1) {
							//notifyIconMenuStrip.Items.Remove(item.Value);
						}
					} catch (Exception e) {
						util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
					}
				}
				
				var recentItems = items.OrderByDescending((a) => a.pubDateDt)
					.Where((a) => history.Find((b) => ((RssItem)b.Value.Tag).lvId == a.lvId).Equals(default(KeyValuePair<DateTime, ToolStripMenuItem>)))
					//.Take(5)
					.Select((RssItem a) => getRssItemToNotifyHistory(a));
				history.AddRange(recentItems);
				
				//var addList = history.OrderByDescending(a => a.Key).Take(5);
				var addList = history.OrderByDescending(a => a.Key);
				
				foreach (var item in addList) {
					if (notifyIconMenuStrip.Items.IndexOf(item.Value) == -1) {
						notifyIconMenuStrip.Items.Insert(0, item.Value);
						var rssItem = items.Find(a => a == item.Value.Tag);
						if (rssItem != null && item.Value.Image == null) {
							item.Value.Image = ThumbnailManager.getThumbnailRssUrl(rssItem.thumbnailUrl, false, true);
						}
					}
				}
				notifyIconRecentSeparator.Visible = true;
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
		}
		private KeyValuePair<DateTime, ToolStripMenuItem> getRssItemToNotifyHistory(RssItem a) {
			try {
				if (a.comName == null) a.comName = "";
				var text = a.pubDateDt.ToString("dd日HH:mm") + " " + 
						(a.title.Length > 20 ? a.title.Substring(0, 20) + ".." : a.title) + 
						"(" + (a.comName.Length > 20 ? a.comName.Substring(0, 20) + ".." : a.comName) + ")";
				var menu = new ToolStripMenuItem(text, null, (o, e) => 
						util.openUrlBrowser("https://live.nicovideo.jp/watch/" + a.lvId, config));
				menu.Tag = a;
				var ret = new KeyValuePair<DateTime, ToolStripMenuItem>(a.pubDateDt, menu);
				return ret;
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				return new KeyValuePair<DateTime, ToolStripMenuItem>(DateTime.MinValue, null);
			}
		}
		/*
		public void liveListLockAction(Action a) {
			try {
				for (var i = 0; i < 300; i++) {
					if (!isLiveListLocking) {
						isLiveListLocking = true;
						util.debugWriteLine("live list lock action " + i);
						break;
					}
					if (i == 99) util.debugWriteLine("live list lock action no get");
					Thread.Sleep(100);
				}
				a.Invoke();
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			} finally {
				isLiveListLocking = false;
			}
		}
		*/
		public void removeDuplicateLiveList() {
			try {
				var _liveListDataSource = liveListDataSource.ToList();
				var _liveListDataReserve = liveListDataReserve.ToList();
				var lv = _liveListDataSource.Select((x) => x.lvId).ToList();
				lv.AddRange(_liveListDataReserve.Select((x) => x.lvId).ToList());
				
				var dup = lv.Where(x => lv.Where(y => y == x).Count() > 1).Distinct().ToArray();
				
				if (dup.Length > 0) {
					util.debugWriteLine("duplicate live list " + dup.Length + " " + dup[0]);
					var dup0 = _liveListDataSource.Where((x) => x.lvId == dup[0]).Count();
					if (dup0 > 0)
						util.debugWriteLine("in datasource duplicate " + dup[0] + " " + dup0);
					var dup1 = _liveListDataReserve.Where((x) => x.lvId == dup[0]).Count();
					if (dup1 > 0)
						util.debugWriteLine("in datareserve duplicate " + dup[0] + " " + dup1);
				}
				
				//formAction(() => {
					foreach (var d in dup) {
						LiveInfo i = null;
						try {
							if (_liveListDataSource.Any(x => x.lvId == d)) {
								i = _liveListDataSource.First(x => x.lvId == d);
								if (i != null) {
									formAction(() => liveListDataSource.Remove(i));
									continue;
								}
							}
						} catch (Exception e) {util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);}
						
						try {
							if (_liveListDataReserve.Any(x => x.lvId == d)) {
								i = _liveListDataReserve.First(x => x.lvId == d);
								if (i != null) {
									formAction(() => liveListDataReserve.Remove(i));
								}
							}
						} catch (Exception e) {util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);}
					}
				//});
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
		}
		public void liveListLockAction(Action a, [CallerMemberName] string memberName = "") {
			//test
			try {
				util.debugWriteLine("liveListLockAction memberName " + memberName);
				a.Invoke();
			} catch (Exception e) {
				util.debugWriteLine("liveList LockAction " + e.Message + e.Source + e.StackTrace);
			}
			return;
			/*
			int scrollI = 0;
			var nowUnix = util.getUnixTime();
			lock (liveListLockObj) {
				liveListLock.Add(nowUnix);
				if (liveListLock.Count > 3) {
					for (var i = 0; i < liveListLock.Count - 3; i++)
						liveListLock.RemoveAt(0);
				}
				if (liveListLock.Count >= 2 && nowUnix - liveListLock[0] < 2)
					return;
			}
			bool flg = false;
			Monitor.TryEnter(liveListLock, 100, ref flg);
			if (!flg) return;
			var dt = DateTime.Now;
			//if (isLiveListLocking) {
			//	util.debugWriteLine("live list locking");
			//} else isLiveListLocking = true;
			
			util.debugWriteLine("liveListLockAction 0 " + memberName);
			try {
				scrollI = liveList.FirstDisplayedScrollingRowIndex;
				a.BeginInvoke(null, null).AsyncWaitHandle.WaitOne(2000, false);
			} finally {
				setScrollIndex(liveList, scrollI);
				try {
					//isLiveListLocking = false;
					if (flg) Monitor.Exit(liveListLock);
				} catch (Exception e) {
					util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				}
				util.debugWriteLine("live list action " + memberName + " " + scrollI + " now " + liveList.FirstDisplayedScrollingRowIndex + " " + (DateTime.Now - dt));
			}
			*/
		}
		private void loadControlLayout() {
			try {
				saveMenuSetting();
				saveSortState();
				saveFormState();
				liveList.DataSource = null;
				alartList.DataSource = null;
				userAlartList.DataSource = null;
				taskList.DataSource = null;
				historyList.DataSource = null;
				notAlartList.DataSource = null;
				reserveHistoryList.DataSource = null;
				logList.DataSource = null;
				
				util.debugWriteLine("delete alartlist " + alartList.GetHashCode());
				
				Font = new Font(Font.FontFamily, 9);
				Controls.Clear();
				
				InitializeComponent();
				formInitSetting();
				
				Update();
				
				util.debugWriteLine("delete alartlist2 " + alartList.GetHashCode());
				util.debugWriteLine("load datasource " + alartListDataSource.GetHashCode());
				util.debugWriteLine("load form  datasource " + alartList.DataSource.GetHashCode());
				
				return;
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
		}
		
		private void resultLiveListSort() {
			util.debugWriteLine("resultLiveListSort");
			for (var j = 0; j < 5; j++) {
				try {
					var column = liveList.SortedColumn;
					if (liveList.SortOrder == SortOrder.None) {
						return;
					}
					var isAsc = liveList.SortOrder == SortOrder.Ascending;
					var type = typeof(LiveInfo).GetProperty(column.DataPropertyName);
					var columnName = column.HeaderText;
					
					var keys = liveListDataSource.Select((l) => {
						return getLiveInfoToSortKey(l, type, columnName);
						//if (columnName == "放送時間") 
						//	return l.pubDateDt.ToString();
						//else return type.GetValue(l, null).ToString();
					}).ToList();
					
					List<string> sortedKey = isAsc ? 
							keys.OrderBy((k) => k).ToList() : 
							keys.OrderByDescending((k) => k).ToList();
					
					//List<string> sortedKey = keys.OrderByDescending((k) => k).ToList();
					/*
					= keys.OrderBy((k) => {
	                 	if (columnName == "放送時間")
	                 		return DateTime.Parse(k);
	                 	else if (columnName == "放送ID" || 
	                 	         columnName == "コミュニティID")
	                 		return int.Parse(k)
								columnName == "コミュニティID" ||
					                           });
					*/
					List<LiveInfo> _liveListDataSource = null;
					formAction(() => _liveListDataSource = liveListDataSource.ToList());
					//formAction(() => {
						for (var i = 0; i < _liveListDataSource.Count(); i++) {
							var _key = getLiveInfoToSortKey(_liveListDataSource[i], type, columnName);
							if (_key != sortedKey[i]) {
								var moveLi = _liveListDataSource.Where((l) => {
								    return getLiveInfoToSortKey(l, type, columnName) == sortedKey[i];
									//return type.GetValue(l, null).ToString() == sortedKey[i];
								}).ToList();
		
								foreach (var m in moveLi) {
									if (moveLi.Count > 1) util.debugWriteLine("moveli multi " + m.lvId + " " + m.title + " " + i);
									formAction(() => {
										liveListDataSource.Remove(m);
										liveListDataSource.Insert(i, m);
									});
								}
							}
						}
					//});
					var isChange = false;
					for (var i = 0; i < _liveListDataSource.Count(); i++) {
						if (getLiveInfoToSortKey(_liveListDataSource[i], type, columnName) != sortedKey[i]) {
							isChange = true;
						}
					}
					if (isChange) {
						util.debugWriteLine("live list sort confirm false");
						continue;
					}
					return;
				} catch (Exception e) {
					util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
					Thread.Sleep(100);
				}
			}
		}
		private string getLiveInfoToSortKey(LiveInfo li, 
					PropertyInfo pi, string columnName) {
			if (columnName == "放送時間")
					return li.pubDateDt.ToString();
			else {
				var s = pi.GetValue(li, null).ToString();
				if ((columnName == "放送ID" || columnName == "チャンネルID") && !string.IsNullOrEmpty(s)) {
					return s.Substring(2).PadLeft(12);
				} else return pi.GetValue(li, null).ToString(); 
			}
		}
		public void setScrollIndex(DataGridView list, int scrollI,
				[CallerMemberName] string memberName = "") {
			util.debugWriteLine("set scrollindex " + scrollI + " now " + list.FirstDisplayedScrollingRowIndex + " " + memberName + " " + list.Rows.Count);
			try {
				if (scrollI != -1 && scrollI < list.Rows.Count)
					formAction(() => list.FirstDisplayedScrollingRowIndex = scrollI);
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				util.debugWriteLine("scrollI exception " + liveListScrollIndex);
			} finally {
				//liveListScrollIndex = -1;
			}
		}
		public void saveLiveListScrollIndex() {
			if (liveListScrollIndex == -1)
				liveListScrollIndex = liveList.FirstDisplayedScrollingRowIndex;
			else util.debugWriteLine("saveLiveListScrollIndex already set mem " + liveListScrollIndex + " now " + liveList.FirstDisplayedScrollingRowIndex);
		}
		
		void OpenSettingFolderMenuClick(object sender, EventArgs e)
		{
			string[] jarpath = util.getJarPath();
			string dirPath = jarpath[0];
			try {
				if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);
				System.Diagnostics.Process.Start(dirPath);
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + " " + ee.StackTrace);
			}
		}
		
		void OpenReadmeMenuClick(object sender, EventArgs e)
		{
			string[] jarpath = util.getJarPath();
			string path = jarpath[0] + "/readme.html.url";
			try {
				if (!File.Exists(path)) {
					MessageBox.Show("readme.htmlが見つかりませんでした");
					return;
				}
				util.openUrlBrowser(path, config);
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + " " + ee.StackTrace);
			}
		}
		void AlartListDataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			#if DEBUG
				addLogText("alartlist dataerror " + e.RowIndex + e.Exception);
			#endif
		}
		void LaunchAppMenuItemDropDownOpening(object sender, EventArgs e)
		{
			try {
				launchAppMenuItem.DropDownItems.Clear();
				for (var i = 0; i < 10; i++) {
					var c = ((char)((char)('A') + i)).ToString();
					//util.debugWriteLine("ccc " + c);
					
					var path = config.get("appli" + c + "Path");
					if (!string.IsNullOrEmpty(path)) {
						 
							
						var _n = config.get("appli" + c + "Name");
						var n = "アプリケーション" + c + 
							(string.IsNullOrEmpty(_n) ? "" : ("(" + _n + ")"));
						var item = new ToolStripMenuItem(n);
						item.Tag = c;
						if (!File.Exists(path)) {
							item.Text = c + " ファイルが見つかりませんでした";
							item.Enabled = false;
						}
						launchAppMenuItem.DropDownItems.Add(item);
					}
				}
				if (launchAppMenuItem.DropDownItems.Count == 0) {
					var t = new ToolStripMenuItem("アプリケーションが設定されていませんでした");
					t.Enabled = false;
					launchAppMenuItem.DropDownItems.Add(t);
				}
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		void LaunchAppMenuItemDropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			try {
				var c = e.ClickedItem.Tag.ToString();
				var path = config.get("appli" + c + "Path");
				if (string.IsNullOrEmpty(path)) {
					util.showModelessMessageBox("アプリケーションが選択されてません", "", this, 0);
				} else if (!File.Exists(path)) {
					util.showModelessMessageBox("ファイルが見つかりませんでした", "", this, 0);
				} else Process.Start(path);
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
				util.showModelessMessageBox("アプリケーションエラーで起動できませんでした", "", this, 0);
			}
		}
		void AlartListKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Delete) {
				RemoveLineMenuClick(null, null);
			}
		}
		void openAddTwitterForm(string id = null) {
			try {
	        	var o = new AddTwitterForm(id); o.ShowDialog();
	        	if (o.ret == null) return;
	        	twitterListDataSource.Add(o.ret);
	        	
	        	Task.Factory.StartNew(() => {
					new TwitterListFileManager().save(this);
				});
	        } catch (Exception ee) {
        		util.debugWriteLine(ee.Message + " " + ee.StackTrace);
	        }
		}
		void AddTwitterBtnClick(object sender, EventArgs e)
		{
			openAddTwitterForm();
		}
		
		void OpenTwitterAccountUrlMenuClick(object sender, EventArgs e)
		{
			try {
				var curCell = twitterList.CurrentCell;
				if (curCell == null || curCell.RowIndex == -1) return;
				var ti = (TwitterInfo)twitterListDataSource[twitterList.CurrentCell.RowIndex];
				if (ti.account == null || ti.account == "") return;
				var url = "https://twitter.com/" + ti.account;
				util.openUrlBrowser(url, config);
				
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		
		void TwitterListDeleteRowMenuClick(object sender, EventArgs e)
		{
			try {
				if (twitterList.SelectedCells.Count == 0) return;
				var selectedList = new List<TwitterInfo>();
				foreach (DataGridViewCell c in twitterList.SelectedCells) {
					try {
						var i = (TwitterInfo)twitterListDataSource[c.RowIndex];
						if (selectedList.IndexOf(i) > -1) continue;
						else selectedList.Add(i);
					} catch (Exception ee) {
						util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
					}
				}
				
				var ids = selectedList.Select(i => i.account);
				var idsStr = string.Join("、", ids);
				
				var id = string.IsNullOrEmpty(idsStr) ? "こ" : (idsStr);
				if (System.Windows.Forms.MessageBox.Show(id + "の行を削除していいですか？", "確認", MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;
				foreach (var i in selectedList)
					twitterListDataSource.Remove(i);
				changedListContent();
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		
		void TwitterListCellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
		{
			setMouseDownSelect(twitterList, e);
		}
		public void addTwitterList(TwitterInfo ti) {
			formAction(() => {
				try {
					twitterListDataSource.Add(ti);
	           	} catch (Exception e) {
	           		util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
	           	}
       	    });
		}
		
		void TwitterListCellParsing(object sender, DataGridViewCellParsingEventArgs e)
		{
			util.debugWriteLine("cell parsing " + e.ColumnIndex);
			var target = ((TwitterInfo)twitterListDataSource[e.RowIndex]);
			if (e.ColumnIndex == 17) target.memo = (string)e.Value;
			util.debugWriteLine("cell parcing");
			Task.Factory.StartNew(() =>
					new TwitterListFileManager().save(this));
		}
		
		void TwitterListCurrentCellDirtyStateChanged(object sender, EventArgs e)
		{
			try {
				var cc = twitterList.CurrentCell;
				util.debugWriteLine(cc.RowIndex + " " + cc.ColumnIndex);
				if (cc is DataGridViewCheckBoxCell) {      
			        if (cc.ColumnIndex >= 2 && cc.ColumnIndex <= 16) {
						twitterList.CommitEdit(DataGridViewDataErrorContexts.Commit);
						var target = ((TwitterInfo)twitterListDataSource[cc.RowIndex]);
						if (cc.ColumnIndex == 5) target.popup = (bool)(twitterList[cc.ColumnIndex, cc.RowIndex].Value);
						if (cc.ColumnIndex == 6) target.baloon = (bool)(twitterList[cc.ColumnIndex, cc.RowIndex].Value);
						if (cc.ColumnIndex == 7) target.browser = (bool)(twitterList[cc.ColumnIndex, cc.RowIndex].Value);
						if (cc.ColumnIndex == 8) target.mail = (bool)(twitterList[cc.ColumnIndex, cc.RowIndex].Value);
						if (cc.ColumnIndex == 9) target.sound = (bool)(twitterList[cc.ColumnIndex, cc.RowIndex].Value);
						if (cc.ColumnIndex == 10) target.appliA = (bool)(twitterList[cc.ColumnIndex, cc.RowIndex].Value);
						if (cc.ColumnIndex == 11) target.appliB = (bool)(twitterList[cc.ColumnIndex, cc.RowIndex].Value);
						if (cc.ColumnIndex == 12) target.appliC = (bool)(twitterList[cc.ColumnIndex, cc.RowIndex].Value);
						if (cc.ColumnIndex == 13) target.appliD = (bool)(twitterList[cc.ColumnIndex, cc.RowIndex].Value);
						if (cc.ColumnIndex == 14) target.appliE = (bool)(twitterList[cc.ColumnIndex, cc.RowIndex].Value);
						if (cc.ColumnIndex == 15) target.appliF = (bool)(twitterList[cc.ColumnIndex, cc.RowIndex].Value);
						if (cc.ColumnIndex == 16) target.appliG = (bool)(twitterList[cc.ColumnIndex, cc.RowIndex].Value);
						if (cc.ColumnIndex == 17) target.appliH = (bool)(twitterList[cc.ColumnIndex, cc.RowIndex].Value);
						if (cc.ColumnIndex == 18) target.appliI = (bool)(twitterList[cc.ColumnIndex, cc.RowIndex].Value);
						if (cc.ColumnIndex == 19) target.appliJ = (bool)(twitterList[cc.ColumnIndex, cc.RowIndex].Value);
	//					util.debugWriteLine(target.appliA);
					}
				}
				Task.Factory.StartNew(() =>
						new TwitterListFileManager().save(this));
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		
		void TwitterListCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			e.CellStyle.BackColor = (e.RowIndex % 2 != 0) ? 
					evenRowsColor
					: Color.FromName("window");
		}
		void TwitterListDataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			#if DEBUG
				addLogText("alartlist dataerror " + e.RowIndex + e.Exception);
			#endif
		}
		void TwitterListDragDrop(object sender, DragEventArgs e)
		{
			try {
				util.debugWriteLine("twitter dragdrop");
				
				var t = e.Data.GetData(DataFormats.Text).ToString();
				var m = new Regex("(https://twitter.com/)*@*([^/]*)").Match(t);
				if (!m.Success) return;
				
				var id = m.Groups[2].Value;
				Task.Factory.StartNew(() => formAction(() => openAddTwitterForm(id)));
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		void LiveListColorColumnMenuDropDownOpening(object sender, EventArgs e)
		{
			//var l = liveListColorColumnMenu.DropDownItems;
			//l.Clear();
			//foreach (ToolStripMenuItem i in liveListColorColumnMenu.DropDownItems)
			//	i.Checked = liveListColorColumns[i];
		}
		
		void LiveListColorColumnMenuDropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			var item = ((ToolStripMenuItem)e.ClickedItem);
			var index = liveListColorColumnMenu.DropDownItems.IndexOf(e.ClickedItem);
			item.Checked = !item.Checked;
			liveListColorColumns[index] = item.Checked;

			while (true) {
				try {
					if (liveList.FirstDisplayedScrollingRowIndex == -1) break;
					for (var i = liveList.FirstDisplayedScrollingRowIndex; i < liveListDataSource.Count; i++) {
						if (!liveList.Rows[i].Displayed) break;
						if (i != -1) liveList.UpdateCellValue(index, i);
					}
					break;
				} catch (Exception ee) {
					util.debugWriteLine(ee.Message + ee.Source + ee.TargetSite + ee.StackTrace);
				}
			}
			var buf = "";
			var items = ((ToolStripMenuItem)sender).DropDownItems;
			foreach (var i in items)
				buf += ((ToolStripMenuItem)i).Checked ? "1" : "0";
			config.set("ColorLiveListColumns", buf);
		}
		void LiveListKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Delete) {
				LiveListDeleteRowMenuClick(null, null);
			}
		}
		void TaskListKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Delete) {
				TaskListRemoveLineMenuClick(null, null);
			}
		}
		void TwitterListKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Delete) {
				TwitterListDeleteRowMenuClick(null, null);
			}
		}
		void HistoryListKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Delete) {
				HistoryListDeleteRowMenuClick(null, null);
			}
		}
		void NotAlartListKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Delete) {
				NotAlartListDeleteRowMenuClick(null, null);
			}
		}
		void ContextMenuStrip1Opening(object sender, CancelEventArgs e)
		{
			if (alartList.SelectedCells.Count == 0 || alartList.Rows.Count == 0)
				e.Cancel = true;
		}
		void UpdateMenuItemDropDownOpened(object sender, EventArgs e)
		{
			lastClickMenu = null;
		}
		void UpdateMenuItemDropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			lastClickMenu = e.ClickedItem;
		}
		void DisplayMenuItemDropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			lastClickMenu = e.ClickedItem;
		}
		void DisplayMenuItemDropDownOpened(object sender, EventArgs e)
		{
			lastClickMenu = null;
		}
		public void addReserveHistoryList(HistoryInfo hi) {
			var historyListMax = int.Parse(config.get("maxReserveHistoryDisplay"));
			try {
				foreach (var _hi in reserveHistoryListDataSource)
	       			if (_hi.lvid == hi.lvid) return;
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
			
       		formAction(() => {
				try {
			        var scrollIndex = reserveHistoryList.FirstDisplayedScrollingRowIndex;
				    
			        if (reserveHistoryListDataSource.Count >= historyListMax) {
				        var min = reserveHistoryListDataSource.OrderBy((a) => a.dt).First();
				        reserveHistoryListDataSource.Remove(min);
			        }
			        
	       	    	reserveHistoryListDataSource.Insert(0, hi);
	       	    	if (scrollIndex != -1 && scrollIndex < reserveHistoryListDataSource.Count - 1)
	       	    		reserveHistoryList.FirstDisplayedScrollingRowIndex = scrollIndex;
	       	    	
	           	} catch (Exception e) {
	           		util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
	           		util.debugWriteLine("reserveHistoryListDataSource.Insert? " + reserveHistoryListDataSource.Count + " " + reserveHistoryList.FirstDisplayedScrollingRowIndex);
	           	}
       	    });
		} 
		
		void ReserveHistoryListRowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
		{
			var max = config.get("maxReserveHistoryDisplay");
			sortHistoryList(reserveHistoryList, reserveHistoryListDataSource, max);
		}
		
		void ReserveHistoryListCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			var style = reserveHistoryList[e.ColumnIndex, e.RowIndex].Style;
			var defColor = (e.RowIndex % 2 != 0) ? 
					evenRowsColor
					: Color.FromName("window");
			style.BackColor = defColor;
		}
		void ReserveHistoryListCellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
		{
			setMouseDownSelect(reserveHistoryList, e);
		}
		void DisplayReserveHistoryListMenuDropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			foreach (DataGridViewColumn c in reserveHistoryList.Columns) {
				if (c.HeaderText == e.ClickedItem.Text) {
					var i = (ToolStripMenuItem)e.ClickedItem;  
					c.Visible = i.Checked = !i.Checked; 
				}
			}
		}
		
		void AlartListCellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
		{
			try {
				if (e.RowIndex == -1) return;
				if (e.ColumnIndex == 8 &&
				    	!string.IsNullOrEmpty(alartListDataSource[e.RowIndex].lastLvTitle)) {
					//e.ToolTipText = alartListDataSource[e.RowIndex].lastLvTitle;
					var ai = alartListDataSource[e.RowIndex];
					if (ai.alartHistory.Count == 0) e.ToolTipText = ai.lastHostDate + " " + ai.lastLvid.Replace("e", "") + " " + ai.lastLvTitle;
					else e.ToolTipText = string.Join("\n", ai.alartHistory.Select(x => x.Value));
				}
				
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace);
			}
		}
		void SearchTextKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter) {
				searchBtn.PerformClick();
				e.SuppressKeyPress = true;
			}
		}
		void LiveListSearchTextKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter) {
				liveListSearchBtn.PerformClick();
				e.SuppressKeyPress = true;
			}
		}
		public void checkNotifyLive() {
			while (true) {
				try {
					var items = notifyIconMenuStrip.Items; 
					for (var i = 0; i < items.Count - 4; i++) {
						if (items[i].Tag == null) {
							util.debugWriteLine("checkNotifyLive tag not found " + items[i].Text);
							continue;
						}
						var ri = (RssItem)items[i].Tag;
						
						var isOnAir = false;
						var hi = historyListDataSource.FirstOrDefault(_hi => _hi.lvid == ri.lvId);
						if (hi != null) {
							isOnAir = hi.onAirMode != 0;
						}
						else {
							isOnAir = isOnAirLvid(ri.lvId, ri.type);
						}
						//util.debugWriteLine("checkNotifyLive check " + ri.lvId + " " + isOnAir);
						if (!isOnAir)
							formAction(() => items.Remove(items[i]));
					}
					break;
					
				} catch (Exception e) {
					util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
					Thread.Sleep(1000);
				}
			}
		}
		private void ReflectionListLv(string lvid) {
			try {
				var l = liveListDataSource.Where(x => x.lvId == lvid).ToList();
				var lc = l.Count();
				for (var i = l.Count - 1; i > -1; i--)
					formAction(() => liveListDataSource.Remove(l[i]));
				foreach (var ai in alartListDataSource) {
					if (ai.lastLvid == lvid) {
						ai.recentColorMode = 0;
						ai.lastLvid = lvid + "e";
					}
				}
				foreach (var hi in historyListDataSource) {
					if (hi.lvid == lvid) hi.onAirMode = 0;
				}
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace);
			}
		}
		List<AlartInfo> getSelectedAiList(DataGridView list, SortableBindingList<AlartInfo> dataSource) {
			try {
				if (list.SelectedCells.Count == 0) return null;
				var selectedAIList = new List<AlartInfo>();
				foreach (DataGridViewCell c in list.SelectedCells) {
					try {
						var ai = (AlartInfo)dataSource[c.RowIndex];
						if (selectedAIList.IndexOf(ai) > -1) continue;
						else selectedAIList.Add(ai);
					} catch (Exception ee) {
						util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
					}
				}
				return selectedAIList;
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
				return null;
			}
		}
		void AlartListFollowComMenuClick(object sender, EventArgs e)
		{
			AlartListFollowMenuClickCore(false, true);
		}
		void AlartListFollowUserMenuClick(object sender, EventArgs e)
		{
			AlartListFollowMenuClickCore(true, true);
		}
		void AlartListUnFollowComMenuClick(object sender, EventArgs e)
		{
			AlartListFollowMenuClickCore(false, false);
		}
		void AlartListUnFollowUserMenuClick(object sender, EventArgs e)
		{
			AlartListFollowMenuClickCore(true, false);
		}
		void AlartListFollowMenuClickCore(bool isUser, bool isFollowMode) {
			try {
				var isUserMode = !favoriteCommunityBtn.Checked;
				var list = isUserMode ? userAlartList : alartList;
				var dataSource = isUserMode ? userAlartListDataSource : alartListDataSource;
				
				var selectedAIList = getSelectedAiList(list, dataSource);
				if (selectedAIList == null) {
					MessageBox.Show("選択された行が見つかりませんでした");
					return;
				}
				
				foreach (var ai in selectedAIList) {
					if (isUser) {
						if (ai.hostFollow == (isFollowMode ? "フォローする" : "フォロー解除する"))
							userFollowCellClick(ai, dataSource, list);
					} else {
						if (ai.communityFollow == (isFollowMode ? "フォローする" : "フォロー解除する"))
							comChannelFollowCellClick(ai);
					}
					
				}
				changedListContent();
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		void AlartListFollowMenuOpening(object sender, CancelEventArgs e)
		{
			var isUserMode = !favoriteCommunityBtn.Checked;
			alartListFollowComMenu.Visible = 
					alartListUnFollowComMenu.Visible = !isUserMode;
		}
		void UserAddTextKeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r' || e.KeyChar == '\n') {
				userAddBtn.PerformClick();
				e.Handled = true;
			}
		}
		void LiveListOpenAppliMenuClick(object sender, EventArgs e)
		{
			var dataSource = liveListDataSource;
			var list = liveList;
			
			var selectedRowIndexList = new List<int>();
			foreach (DataGridViewCell c in list.SelectedCells) {
				try {
					if (selectedRowIndexList.IndexOf(c.RowIndex) > -1) continue;
					selectedRowIndexList.Add(c.RowIndex);
					var li = (LiveInfo)dataSource[c.RowIndex];
					
					if (string.IsNullOrEmpty(li.lvId)) return;
			
					var n = ((ToolStripMenuItem)sender).Name.Substring(17, 1);
					var path = config.get("appli" + n + "Path");
					var args = config.get("appli" + n + "Args");
					var url = "https://live.nicovideo.jp/watch/lv" + util.getRegGroup(li.lvId, "(\\d+)");
					
					var ri = util.getRiFromLvid(li.lvId, check.container);
					var appNum = (int)(n[0] - 'A');
					util.appliProcess(path, url, args, ri, check.container, config, appNum, this);
			
				} catch (Exception eee) {
					util.debugWriteLine(eee.Message + eee.Source + eee.StackTrace + eee.TargetSite);
				}
			}
		}
		public void followCheck() {
			if ((alartListDataSource.Count > 0 || userAlartListDataSource.Count > 0)) {
				Task.Factory.StartNew(() => new FollowChecker(this, check.container).check());
			}
		}
		void setNotifyOff() {
			var notifyOffs = new string[] {
					"OffPop", "OffBalloon",
					"OffWeb","OffMail","OffSound","OffAppA",
					"OffAppB","OffAppC","OffAppD",
					"OffAppE","OffAppF","OffAppG",
					"OffAppH","OffAppI","OffAppJ"};
			var isAllOff = true;
			for(var i = 0; i < notifyOffs.Length; i++) {
				var isOff = bool.Parse(config.get(notifyOffs[i]));
				notifyOffList[i + 2] = isOff;
				if (!isOff) isAllOff = false;
			}
			notifyOffList[0] = isAllOff;
			
		}
		void setNotifyOffColumn() {
			var notifyOffsPropName = new string[] {
					"popup","baloon",
					"browser","mail","sound","appliA",
					"appliB","appliC","appliD",
					"appliE","appliF","appliG",
					"appliH","appliI","appliJ"};
			var n = notifyOffList;
			var columns = alartList.Columns;
			for(var i = 2; i < notifyOffList.Length; i++) {
				var isOff = notifyOffList[i];
				foreach (DataGridViewColumn c in columns) {
					util.debugWriteLine("column__ " + c.Name + " " + c.DataPropertyName);
					if (c.DataPropertyName.ToLower().IndexOf(notifyOffsPropName[i - 2]) > -1) {
						break;
					}
				}
			}
			alartList.Refresh();
		}
		bool isNotifyOffColumn(int columnI) {
			var notifyOffsPropName = new string[] {
					"popup","baloon",
					"browser","mail","sound","appliA",
					"appliB","appliC","appliD",
					"appliE","appliF","appliG",
					"appliH","appliI","appliJ"};
			for(var i = 2; i < notifyOffList.Length; i++) {
				var isOff = notifyOffList[i];
				if (!isOff) continue;
				
				foreach (DataGridViewColumn c in alartList.Columns) {
					var a = c.DisplayIndex;
					if (c.Index != columnI) continue;
					if (c.DataPropertyName.ToLower().IndexOf(notifyOffsPropName[i - 2].ToLower()) > -1) {
						return true;
					}
				}
			}
			return false;
		}
		void UserSearchBtnClick(object sender, EventArgs e)
		{
			searchBtnClickCore(userSearchText, userAlartList, 
					userAlartListDataSource, userSearchBtn);
		}
		void UserSearchTextKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter) {
				userSearchBtn.PerformClick();
				e.SuppressKeyPress = true;
			}
		}
		void LiveListFilterBtnClick(object sender, EventArgs e)
		{
			var filterList = new List<LiveListFilterInfo>();
			if (liveListFilterBtn.Tag != null)
				filterList = (List<LiveListFilterInfo>)liveListFilterBtn.Tag;
			var f = new LiveListFilterForm(config, filterList);
			if (f.ShowDialog() != DialogResult.OK) return;
			
			liveListFilterBtn.Tag = f.ret;
			var j = JToken.FromObject(f.ret).ToString(Formatting.None);
			config.set("liveListFilter", j);
			liveListFilterBtn.Tag = f.ret;
			resetLiveList();
		}
		bool isLiveFilterMatch(LiveInfo li) {
			if (liveListFilterBtn.Tag == null) return true;
			var filter = (List<LiveListFilterInfo>)liveListFilterBtn.Tag;
			
			foreach (var f in filter) {
				//必ず含む いずれかを含む 含まない
				//not
				if (f.matchType == "含まない" && 
				    	isLiveFilterMatchCore(li, f.infoType, f.str))
					return false;
				//and
				if (f.matchType == "必ず含む" && 
				    	!isLiveFilterMatchCore(li, f.infoType, f.str))
					return false;
				//or
				if (f.matchType == "いずれかを含む" && 
				    	isLiveFilterMatchCore(li, f.infoType, f.str))
					return true;
			}
			return true;
		}
		bool isLiveFilterMatchCore(LiveInfo li, string infoType, string word) {
			if (li.title == null) li.title = "";
			if (li.comName == null) li.comName = "";
			if (li.comId == null) li.comId = "";
			if (li.hostName == null) li.hostName = "";
			if (li.lvId == null) li.lvId = "";
			
			if (infoType == "タイトル")
				return util.getRegGroup(li.title, "(" + word + ")") != null;
			if (infoType == "チャンネル名")
				return util.getRegGroup(li.comName, "(" + word + ")") != null;
			if (infoType == "チャンネルID")
				return util.getRegGroup(li.comId, "(" + word + ")") != null;
			if (infoType == "放送者名")
				return util.getRegGroup(li.hostName, "(" + word + ")") != null;
			if (infoType == "放送者ID")
				return util.getRegGroup(li.hostId, "(" + word + ")") != null;
			if (infoType == "放送ID")
				return util.getRegGroup(li.lvId, "(" + word + ")") != null;
			if (infoType == "全て") {
				return util.getRegGroup(li.title, "(" + word + ")") != null ||
						util.getRegGroup(li.comName, "(" + word + ")") != null ||
						util.getRegGroup(li.comId, "(" + word + ")") != null ||
						util.getRegGroup(li.hostName, "(" + word + ")") != null ||
						util.getRegGroup(li.lvId, "(" + word + ")") != null;
			}
			return true;
		}
	}
}
