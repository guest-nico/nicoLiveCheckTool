/*
 * Created by SharpDevelop.
 * User: pc
 * Date: 2018/04/06
 * Time: 20:55
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.ComponentModel;
using SunokoLibrary.Application;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using System.Text;
using System.Threading;
using System.Diagnostics;
using namaichi.alart;
using namaichi.config;
using namaichi.utility;
using namaichi.rec;
using namaichi.info;

//using namaichi.play;



//using SuperSocket.ClientEngine;

//using System.Diagnostics.Process;

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
		public List<LiveInfo> liveListDataReserve = new List<LiveInfo>();
		//public BindingSource alartListDataSource = new BindingSource();
		//public BindingSource taskListDataSource = new BindingSource();
		public Check check = null;
		public TaskCheck taskCheck = null;
		public LiveCheck liveCheck = null;
		
		public Mutex mutex;
		//private Thread madeThread;
		
		private DateTime lastBalloonTime = DateTime.MinValue;
		private string notifyUrl = null;
		public bool[] notifyOffList = new bool[17];
		private bool isAddFormDisplaying = false;
		
		private Thread madeThread;
		public object liveListLock = new object();
		private bool isLiveListTimeProcessing = false;
		
		public MainForm(string[] args, Mutex mutex)
		{
			madeThread = Thread.CurrentThread;
			this.mutex = mutex;
			//this.madeThread = Thread.CurrentThread;
			//args = "-nowindo -stdIO -IsmessageBox=false -IscloseExit=true lv316762771 -ts-start=1785s -ts-end=0s -ts-list=false -ts-list-m3u8=false -ts-list-update=5 -ts-list-open=false -ts-list-command=\"notepad{i}\" -ts-vpos-starttime=true -afterConvertMode=4 -qualityRank=0,1,2,3,4,5 -IsLogFile=true".Split(' ');
			//read std
			if (Array.IndexOf(args, "-std-read") > -1) startStdRead();
			
//			#if !DEBUGE
//				if (config.get("IsLogFile") == "true") 
//					config.set("IsLogFile", "false");
//			#endif
					
			System.Diagnostics.Debug.Listeners.Clear();
			System.Diagnostics.Debug.Listeners.Add(new Logger.TraceListener());
		    
			InitializeComponent();
			Text = "放送チェックツール（仮 " + util.versionStr;
			
			this.args = args;
			
			
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
				if (bool.Parse(config.get("Isminimized"))) {
					this.WindowState = FormWindowState.Minimized;
				}
            }

			util.debugWriteLine("arg len " + args.Length);
			util.debugWriteLine("arg join " + string.Join(" ", args));
			
			
            //nicoSessionComboBox1.Selector.PropertyChanged += Selector_PropertyChanged;
//            checkBoxShowAll.Checked = bool.Parse(config.get("isAllBrowserMode"));
			//if (isInitRun) initRec();
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
			
			alartList.DataSource = alartListDataSource;
			check = new Check(alartListDataSource, this);
			taskList.DataSource = taskListDataSource;
			taskCheck = new TaskCheck(taskListDataSource, this);
			liveList.DataSource = liveListDataSource;
			
			notifyIcon.Icon = Icon;
			
			
//			new PopupForm(null).Show();
//			new SmallPopupForm(null).Show();
			//[2019/01/01 00:00:00] 放送ID：lv10000 コミュニティID：co100001　ユーザーID：100000
			setDoubleBuffered(alartList);
			setDoubleBuffered(taskList);
			setDoubleBuffered(liveList);
			
			applyMenuSetting();
			
			setAppliNameAndContextMenu();
			
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
	        	optionForm o = new optionForm(config); 
	        	var r = o.ShowDialog();
	        	if (r == DialogResult.OK) {
	        		Task.Run(() => {
    		         	check.setCookie();
    		         	check.resetCheck();
    		         	setAppliNameAndContextMenu();
    		         	recentLiveCheck();
    		         });
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
       		if (util.isStdIO) Console.WriteLine("info.log:" + t);
       		if (!util.isShowWindow) return;
       		try {
	       		if (this.IsDisposed) return;
	       		if (!this.IsHandleCreated) return;
	       		if (isInvoke) {
		        	this.Invoke((MethodInvoker)delegate() {
		       		       	try {
				        	    string _t = "";
						    	if (logText.Text.Length != 0) _t += "\r\n";
						    	_t += t;
						    	
					    		logText.AppendText(_t);
								if (logText.Text.Length > 200000) 
									logText.Text = logText.Text.Substring(logText.TextLength - 10000);
		       		       	} catch (Exception e) {
		       		       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
		       		       	}
		
					});
	       		} else {
	       			try {
		        	    string _t = "";
				    	if (logText.Text.Length != 0) _t += "\r\n";
				    	_t += t;
				    	
			    		logText.AppendText(_t);
						if (logText.Text.Length > 20000) 
							logText.Text = logText.Text.Substring(logText.TextLength - 10000);
	   		       	} catch (Exception e) {
	   		       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
	   		       	}
	       		}
	       	} catch (Exception e) {
	       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
	       	}
       		
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
			try {
				if (mutex != null) {
					mutex.ReleaseMutex();
					mutex.Close();
				}
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
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
				} else {
					config.set("Width", RestoreBounds.Width.ToString());
					config.set("Height", RestoreBounds.Height.ToString());
				}
				new AlartListFileManager().save(this);
				new TaskListFileManager().save(this);
				saveMenuSetting();
				
			} catch(Exception e) {
				util.debugWriteLine(e.Message + " " + e.StackTrace);
			}
			//player.stopPlaying(true, true);
			return true;
		}
		
		void mainForm_Load(object sender, EventArgs e)
		{
//			if (!util.isShowWindow) return;
			foreach (DataGridViewColumn c in alartList.Columns)
				c.SortMode = DataGridViewColumnSortMode.Automatic;
			foreach (DataGridViewColumn c in taskList.Columns)
				c.SortMode = DataGridViewColumnSortMode.Automatic;
			foreach (DataGridViewColumn c in liveList.Columns)
				c.SortMode = DataGridViewColumnSortMode.Automatic;
			
			liveList.RowTemplate.Height = liveList.Columns[1].Width;
			
			Task.Run(() => {
				new AlartListFileManager().load(this);
				check.start();
			});
			Task.Run(() => {
				new TaskListFileManager().load(this);
				taskCheck.start();
			});
			Task.Run(() => {
			    liveCheck = new LiveCheck(this);
			    if (bool.Parse(config.get("AutoStart")))
			    	formAction(() => updateAutoUpdateStartMenu.PerformClick());
			    
			});
			categoryBtnDisplayUpdate();
			
			
			return;
			
			var a = util.getJarPath();
			var desc = System.Diagnostics.FileVersionInfo.GetVersionInfo(util.getJarPath()[0] + "/websocket4net.dll");
			if (desc.FileDescription != "WebSocket4Net for .NET 4.5 gettable data bytes") {
				Invoke((MethodInvoker)delegate() {
					System.Windows.Forms.MessageBox.Show("「WebSocket4Net.dll」をver0.86.9以降に同梱されているものと置き換えてください");
				});
			}
			
			
			
			//.net
			var ver = util.Get45PlusFromRegistry();  
			if (ver < 4.52) {
				
				Task.Run(() => {
				    Invoke((MethodInvoker)delegate() {
						var b = new DotNetMessageBox(ver);
						b.Show(this); 
//						System.Windows.Forms.MessageBox.Show("「動作には.NET 4.5.2以上が推奨です。現在は" + ver + "です。");
					});
				});
			}
		}
		
		void versionMenu_Click(object sender, EventArgs e)
		{
			var v = new VersionForm(config);
			v.ShowDialog();
		}
		void startStdRead() {
			Task.Run(() => {
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
				
				
				Task.Run(() =>
					Invoke((MethodInvoker)delegate() {
						openAddForm(id);
					})
				);
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		void TaskListDragDrop(object sender, DragEventArgs e)
		{
			try {
				util.debugWriteLine("dragdrop");
				
				var t = e.Data.GetData(DataFormats.Text).ToString();
				string id = null;
				var lv = util.getRegGroup(t, "(lv\\d+)");
				if (lv == null) return;
				
				Task.Run(() =>
					Invoke((MethodInvoker)delegate() {
						openAddYoyakuForm(lv);
					})
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
		void openAddForm(string id = null) {
			try {
				isAddFormDisplaying = true;
	        	var o = new addForm(this, id); o.ShowDialog();
	        	isAddFormDisplaying = false;
	        	if (o.ret == null) return;
	        	alartListDataSource.Add(o.ret);
	        	//alartList[alartListDataSource.Count - 1]
	        } catch (Exception ee) {
        		util.debugWriteLine(ee.Message + " " + ee.StackTrace);
	        }
		}
		void openAddYoyakuForm(string id = null) {
			try {
	        	var o = new addTaskForm(this, id); o.ShowDialog();
	        	if (o.ret == null) return;
	        	taskListDataSource.Add(o.ret);
	        	//alartList[alartListDataSource.Count - 1]
	        } catch (Exception ee) {
        		util.debugWriteLine(ee.Message + " " + ee.StackTrace);
	        }
		}
		public void updateLastHosoDate(int i, string hosoDate, string lvid) {
			try {
	       		if (this.IsDisposed) return;
	       		if (!this.IsHandleCreated) return;
	        	this.Invoke((MethodInvoker)delegate() {
       		       	try {
//						var now = DateTime.Now.ToString();
	       		        var ai = (info.AlartInfo)alartListDataSource[i]; 
						ai.LastHostDate = hosoDate;//now.Substring(0, now.Length - 0);
						ai.lastLvid = lvid;
						ai.isRecentColor = true;
						
						alartListDataSource.ResetItem(i);
       		       	} catch (Exception e) {
       		       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
       		       	}
	
				});
	       		
	       	} catch (Exception e) {
	       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
	       	}
		}
		public void updateUserName(int i, string userName, bool isFollow) {
			try {
	       		if (this.IsDisposed) return;
	       		if (!this.IsHandleCreated) return;
	        	this.Invoke((MethodInvoker)delegate() {
       		       	try {
	       		        var ai = ((info.AlartInfo)alartListDataSource[i]);
	       		        var follow = (isFollow) ? "フォロー解除する" : "フォローする";
	       		        if (ai.hostName != userName || ai.hostFollow != follow) {
							ai.hostName = userName;
							ai.hostFollow = follow;
							alartListDataSource.ResetItem(i);
	       		        }
       		       	} catch (Exception e) {
       		       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
       		       	}
	
				});
	       		
	       	} catch (Exception e) {
	       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
	       	}
		}
		public void updateCommunityName(int i, string comName, bool isFollow) {
			try {
	       		if (this.IsDisposed) return;
	       		if (!this.IsHandleCreated) return;
	        	this.Invoke((MethodInvoker)delegate() {
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
	       		
	       	} catch (Exception e) {
	       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
	       	}
		}
		public int getAlartListCount() {
			try {
	       		if (this.IsDisposed) return 0;
	       		if (!this.IsHandleCreated) return 0;
	       		var ret = 0;
	        	this.Invoke((MethodInvoker)delegate() {
       		       	try {
						ret = alartListDataSource.Count;
       		       	} catch (Exception e) {
       		       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
       		       	}
				});
	       		return ret;
	       	} catch (Exception e) {
	       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
	       	}
			util.debugWriteLine("alart exception? 0");
			return 0;
		}
		public int getTaskListCount() {
			try {
	       		if (this.IsDisposed) return 0;
	       		if (!this.IsHandleCreated) return 0;
	       		var ret = 0;
	        	this.Invoke((MethodInvoker)delegate() {
       		       	try {
						ret = taskListDataSource.Count;
       		       	} catch (Exception e) {
       		       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
       		       	}
				});
	       		return ret;
	       	} catch (Exception e) {
	       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
	       	}
			util.debugWriteLine("alart exception? 0");
			return 0;
		}
		void AlartListCellClick(object sender, DataGridViewCellEventArgs e)
		{
			util.debugWriteLine(e.ColumnIndex + " " + e.RowIndex);
			if (e.ColumnIndex == -1 || e.RowIndex == -1) return;
			if (alartList[e.ColumnIndex, e.RowIndex].Value.ToString() == "") return;
			if (e.ColumnIndex == 5 && 
			    	alartList[0, e.RowIndex].Value != "") {
				comChannelFollowCellClick(alartListDataSource[e.RowIndex]);
			}
			if (e.ColumnIndex == 6 && 
			    	alartList[1, e.RowIndex].Value != "") {
				userFollowCellClick(alartListDataSource[e.RowIndex]);
			}
			
		}
		public bool comChannelFollowCellClick(AlartInfo ai, bool isLog = true) {
			var rowIndex = alartListDataSource.IndexOf(ai);
			if (rowIndex == -1) return false;
			
			var comId = ai.communityId;
			var isChannel = comId.StartsWith("ch");
			var isFollow = false;
			var isOk = false;
			if (isChannel) {
				if (alartList[5, rowIndex].Value == "フォローする") {
					isOk = new FollowChannel(false).followChannel(comId, check.container, this, config);
					if (isOk) alartList[5, rowIndex].Value = "フォロー解除する";
					isFollow = true;
				} else if (alartList[5, rowIndex].Value == "フォロー解除する") {
					isOk = new FollowChannel(false).unFollowChannel(comId, check.container, this, config);
					if (isOk) alartList[5, rowIndex].Value = "フォローする";
					isFollow = false;
				}
			} else {
				if (alartList[5, rowIndex].Value == "フォローする") {
					isOk = new FollowCommunity(false).followCommunity(comId, check.container, this, config);
					if (isOk) alartList[5, rowIndex].Value = "フォロー解除する";
					isFollow = true;
				} else if (alartList[5, rowIndex].Value == "フォロー解除する") {
					isOk = new FollowCommunity(false).unFollowCommunity(comId, check.container, this, config);
					if (isOk) alartList[5, rowIndex].Value = "フォローする";
					isFollow = false;
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
				alartList[5, rowIndex].Value = (_isFollow) ? "フォロー解除する" : "フォローする";
			}
			return isOk;
		}
		public bool userFollowCellClick(AlartInfo ai, bool isLog = true) {
			//var userId = alartList[1, rowIndex].Value.ToString();
			var rowIndex = alartListDataSource.IndexOf(ai);
			if (rowIndex == -1) return false;
			bool isOk = false;
			if (alartList[6, rowIndex].Value == "フォローする") {
				isOk = new FollowUser().followUser(ai.hostId, check.container, this, config);
				if (isOk) alartList[6, rowIndex].Value = "フォロー解除する";
				if (isLog) addLogText("ユーザーID " + ai.hostId + ((isOk) ? "のフォローに成功しました" : "のフォローに失敗しました。"));
			} else if (alartList[6, rowIndex].Value == "フォロー解除する") {
				isOk = new FollowUser().unFollowUser(ai.hostId, check.container, this, config);
				if (isOk) alartList[6, rowIndex].Value = "フォローする";
				if (isLog) addLogText("ユーザーID " + ai.hostId + ((isOk) ? "フォロー解除に成功しました" : "フォロー解除に失敗しました。"));
			}
			
			if (!isOk) {
				bool isFollow;
				util.getUserName(ai.hostId, out isFollow, check.container);
				alartList[6, rowIndex].Value = (isFollow) ? "フォロー解除する" : "フォローする";
			}
			return isOk;
		}

		void AlartListCellParsing(object sender, System.Windows.Forms.DataGridViewCellParsingEventArgs e)
		{
			util.debugWriteLine(e.ColumnIndex);
			var target = ((AlartInfo)alartListDataSource[e.RowIndex]);
			if (e.ColumnIndex == 24) target.memo = (string)e.Value;
			util.debugWriteLine("cell parcing");
		}
		
		

		void AlartListCurrentCellDirtyStateChanged(object sender, EventArgs e)
		{
			var cc = alartList.CurrentCell;
			if (cc is DataGridViewCheckBoxCell) {   
		        if (cc.ColumnIndex >= 9 && cc.ColumnIndex <= 23) {
					alartList.CommitEdit(DataGridViewDataErrorContexts.Commit);
					var target = ((AlartInfo)alartListDataSource[cc.RowIndex]);
					if (cc.ColumnIndex == 9) target.popup = (bool)(alartList[cc.ColumnIndex, cc.RowIndex].Value);
					if (cc.ColumnIndex == 10) target.baloon = (bool)(alartList[cc.ColumnIndex, cc.RowIndex].Value);
					if (cc.ColumnIndex == 11) target.browser = (bool)(alartList[cc.ColumnIndex, cc.RowIndex].Value);
					if (cc.ColumnIndex == 12) target.mail = (bool)(alartList[cc.ColumnIndex, cc.RowIndex].Value);
					if (cc.ColumnIndex == 13) target.sound = (bool)(alartList[cc.ColumnIndex, cc.RowIndex].Value);
					if (cc.ColumnIndex == 14) target.appliA = (bool)(alartList[cc.ColumnIndex, cc.RowIndex].Value);
					if (cc.ColumnIndex == 15) target.appliB = (bool)(alartList[cc.ColumnIndex, cc.RowIndex].Value);
					if (cc.ColumnIndex == 16) target.appliC = (bool)(alartList[cc.ColumnIndex, cc.RowIndex].Value);
					if (cc.ColumnIndex == 17) target.appliD = (bool)(alartList[cc.ColumnIndex, cc.RowIndex].Value);
					if (cc.ColumnIndex == 18) target.appliE = (bool)(alartList[cc.ColumnIndex, cc.RowIndex].Value);
					if (cc.ColumnIndex == 19) target.appliF = (bool)(alartList[cc.ColumnIndex, cc.RowIndex].Value);
					if (cc.ColumnIndex == 20) target.appliG = (bool)(alartList[cc.ColumnIndex, cc.RowIndex].Value);
					if (cc.ColumnIndex == 21) target.appliH = (bool)(alartList[cc.ColumnIndex, cc.RowIndex].Value);
					if (cc.ColumnIndex == 22) target.appliI = (bool)(alartList[cc.ColumnIndex, cc.RowIndex].Value);
					if (cc.ColumnIndex == 23) target.appliJ = (bool)(alartList[cc.ColumnIndex, cc.RowIndex].Value);
					util.debugWriteLine(target.appliA);
				}
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
			//Task.Run(() => new AlartListFileManager().ReadNamarokuList(this, alartListDataSource, dialog.FileName, true));
			Task.Run(() => new AlartListFileManager().ReadNamarokuList(this, alartListDataSource, dialog.FileName, false, true));
		}

		
		void AlartListColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			util.debugWriteLine("header " + e.ColumnIndex + " " + e.RowIndex);
			
			
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
			try {
				var curCell = alartList.CurrentCell;
				if (curCell == null || curCell.RowIndex == -1) return;
				var ai = (AlartInfo)alartListDataSource[alartList.CurrentCell.RowIndex];
				if (ai.lastLvid == null || ai.lastLvid == "") return;
				var url = "http://live2.nicovideo.jp/watch/lv" + util.getRegGroup(ai.lastLvid, "(\\d+)");
				util.openUrlBrowser(url, config);
				
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		void OpenCommunityUrlClick(object sender, EventArgs e)
		{
			var curCell = alartList.CurrentCell;
			if (curCell == null || curCell.RowIndex == -1) return;
			var ai = (AlartInfo)alartListDataSource[curCell.RowIndex];
			if (ai.communityId == null || ai.communityId == "") return;
			
			var isChannel = ai.communityId.IndexOf("ch") > -1;
			var url = (isChannel) ? 
				("http://ch.nicovideo.jp/" + ai.communityId) :
				("https://com.nicovideo.jp/community/" + ai.communityId);
		
			util.openUrlBrowser(url, config);
			
		}
		void OpenUserUrlClick(object sender, EventArgs e)
		{
			var curCell = alartList.CurrentCell;
			if (curCell == null || curCell.RowIndex == -1) return;
			var ai = (AlartInfo)alartListDataSource[curCell.RowIndex];
			if (ai.hostId == null || ai.hostId == "") return;
			
			var url = "https://www.nicovideo.jp/user/" + ai.hostId;
			util.openUrlBrowser(url, config);
			
		}
		
		void CopyLastHosoMenuClick(object sender, EventArgs e)
		{
			var curCell = alartList.CurrentCell;
			if (curCell == null || curCell.RowIndex == -1) return;
			var ai = (AlartInfo)alartListDataSource[alartList.CurrentCell.RowIndex];
			if (ai.lastLvid == null || ai.lastLvid == "") return;
			var url = "http://live2.nicovideo.jp/watch/lv" + util.getRegGroup(ai.lastLvid, "(\\d+)");
			Clipboard.SetText(url);
		}
		
		void CopyCommunityUrlMenuClick(object sender, EventArgs e)
		{
			var curCell = alartList.CurrentCell;
			if (curCell == null || curCell.RowIndex == -1) return;
			var ai = (AlartInfo)alartListDataSource[curCell.RowIndex];
			if (ai.communityId == null || ai.communityId == "") return;
			
			var isChannel = ai.communityId.IndexOf("ch") > -1;
			var url = (isChannel) ? 
				("http://ch.nicovideo.jp/" + ai.communityId) :
				("https://com.nicovideo.jp/community/" + ai.communityId);
			Clipboard.SetText(url);
		}
		
		void CopyUserUrlMenuClick(object sender, EventArgs e)
		{
			var curCell = alartList.CurrentCell;
			if (curCell == null || curCell.RowIndex == -1) return;
			var ai = (AlartInfo)alartListDataSource[curCell.RowIndex];
			if (ai.hostId == null || ai.hostId == "") return;
			
			var url = "https://www.nicovideo.jp/user/" + ai.hostId;
			Clipboard.SetText(url);
		}
		
		void RemoveLineMenuClick(object sender, EventArgs e)
		{
			var curCell = alartList.CurrentCell;
			if (curCell == null || curCell.RowIndex == -1) return;
			var ai = alartListDataSource[curCell.RowIndex];
			var id = (!string.IsNullOrEmpty(ai.communityId)) ? 
				ai.communityId : 
				((!string.IsNullOrEmpty(ai.hostId)) ? ai.hostId : ai.keyword);
			id = string.IsNullOrEmpty(id) ? "こ" : (id);
			if (MessageBox.Show(id + "の行を削除していいですか？", "確認", MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;
			alartListDataSource.RemoveAt(curCell.RowIndex);
		}
		
		void AlartListCellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
		{
			setMouseDownSelect(alartList, e);
		}
		void taskListCellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
		{
			setMouseDownSelect(taskList, e);
		}
		void LiveListCellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
		{
			setMouseDownSelect(liveList, e);
		}
		void setMouseDownSelect(DataGridView dgv, DataGridViewCellMouseEventArgs e) {
			if (e.Button == MouseButtons.Right) {
				dgv.ClearSelection();
				dgv[e.ColumnIndex, e.RowIndex].Selected = true;
			}
		}
		public void setHosoLogStatusBar(RssItem item) {
			try {
	       		if (this.IsDisposed) return;
	       		if (!this.IsHandleCreated) return;
	       		var ret = 0;
	        	this.Invoke((MethodInvoker)delegate() {
       		       	try {
						var buf = "[" + DateTime.Parse(item.pubDate).ToString("yyyy/MM/dd HH:mm:ss") + "] 放送ID：" + item.lvId + " コミュニティID：" + item.comId + "　ユーザー名：" + item.hostName;
						lastHosoStatusBar.Text = buf;
       		       	} catch (Exception e) {
       		       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
       		       	}
				});
	       		return;
	       	} catch (Exception e) {
	       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
	       	}
			
		}
		public void sortAlartList() {
			var order = (alartList.SortOrder == SortOrder.None) ? "none" : ((alartList.SortOrder == SortOrder.Ascending) ? "asc" : "des");
			util.debugWriteLine("sortAlartList " + order);
			
			if (alartList.SortOrder == SortOrder.None) return;
			var direction = (alartList.SortOrder == SortOrder.Ascending) ? ListSortDirection.Ascending : ListSortDirection.Descending;
			try {
	       		if (this.IsDisposed) return;
	       		if (!this.IsHandleCreated) return;
	       		var ret = 0;
	        	this.Invoke((MethodInvoker)delegate() {
       		       	try {
						alartList.Sort(alartList.SortedColumn, direction);
       		       	} catch (Exception e) {
       		       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
       		       	}
				});
	       		return;
	       	} catch (Exception e) {
	       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
	       	}
		}
		public void sortLiveList() {
			var order = (liveList.SortOrder == SortOrder.None) ? "none" : ((liveList.SortOrder == SortOrder.Ascending) ? "asc" : "des");
			util.debugWriteLine("sortLiveList " + order);
			
			if (liveList.SortOrder == SortOrder.None) return;
			var direction = (liveList.SortOrder == SortOrder.Ascending) ? ListSortDirection.Ascending : ListSortDirection.Descending;
			try {
	       		if (this.IsDisposed) return;
	       		if (!this.IsHandleCreated) return;
	       		var ret = 0;
	        	this.Invoke((MethodInvoker)delegate() {
       		       	try {
						liveList.Sort(liveList.SortedColumn, direction);
       		       	} catch (Exception e) {
       		       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
       		       	}
				});
	       		return;
	       	} catch (Exception e) {
	       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
	       	}
		}
		public void DisplayPopup(RssItem item, Point point, bool isSmall, PopupDisplay pd, int showIndex, AlartInfo ai) {
			try {
	       		if (this.IsDisposed) return;
	       		if (!this.IsHandleCreated) return;
	       		var ret = 0;
	        	this.Invoke((MethodInvoker)delegate() {
       		       	try {
						var f = (isSmall) ? ((Form)new SmallPopupForm(item, config, pd, showIndex, ai)) : ((Form)new PopupForm(item, config, pd, showIndex, ai));
						f.Location = point;
						f.Show();
       		       	} catch (Exception e) {
       		       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
       		       	}
				});
	       		return;
	       	} catch (Exception e) {
	       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
	       	}
		}
		public void DisplayBalloon(RssItem item, AlartInfo ai) {
			try {
	       		if (this.IsDisposed) return;
	       		if (!this.IsHandleCreated) return;
	       		//var ret = 0;
	       		var tipTitle = item.comName;
				var url = "http://live2.nicovideo.jp/watch/" + item.lvId;
				var content = url;
				if (ai != null && ai.keyword != null && ai.keyword != "") content = ai.keyword + "-" + url;
	        	this.Invoke((MethodInvoker)delegate() {
       		       	try {      
						notifyUrl = url;
						notifyIcon.ShowBalloonTip(10000, tipTitle, content, ToolTipIcon.None);
						lastBalloonTime = DateTime.Now;
						Task.Run(() => {
				         	Thread.Sleep(30000);
				         	if (DateTime.Now - lastBalloonTime > TimeSpan.FromSeconds(30)) closeBalloon();
						});
       		       	} catch (Exception e) {
       		       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
       		       	}
				});
	       		return;
	       	} catch (Exception e) {
	       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
	       	}
		}
		private void closeBalloon() {
			try {
	       		if (this.IsDisposed) return;
	       		if (!this.IsHandleCreated) return;
	        	this.Invoke((MethodInvoker)delegate() {
       		       	try {      
						notifyIcon.Visible = false;
						notifyIcon.Visible = true;
       		       	} catch (Exception e) {
       		       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
       		       	}
				});
	       		return;
	       	} catch (Exception e) {
	       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
	       	}
		}
		void CloseNotifyIconMenuClick(object sender, EventArgs e)
		{
			//Close();
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
			if (e.ColumnIndex == 7 && alartListDataSource[e.RowIndex].isRecentColor) {
				alartList[7, e.RowIndex].Style.BackColor =　Color.FromArgb(255, 224, 255);
				
			} else {
				e.CellStyle.BackColor = (e.RowIndex % 2 != 0) ? 
						Color.FromArgb(245, 245, 245)
						: Color.FromName("window");
			}
			
			
		}
		void TaskListCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			//util.debugWriteLine("value " + alartList[7, e.RowIndex].Value);
			//util.debugWriteLine("alartlist cellformatting " + e.ColumnIndex + " " + e.RowIndex);
			//if (keikaTime < TimeSpan.FromSeconds(30)) {
			e.CellStyle.BackColor = (e.RowIndex % 2 != 0) ? 
					Color.FromArgb(245, 245, 245)
					: Color.FromName("window");
			
		}
		public void alartListRemove(AlartInfo ai) {
			try {
	       		if (this.IsDisposed) return;
	       		if (!this.IsHandleCreated) return;
	        	this.Invoke((MethodInvoker)delegate() {
       		       	try {
	       		        
						alartListDataSource.Remove(ai);
       		       	} catch (Exception e) {
       		       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
       		       	}
				});
	       	} catch (Exception e) {
	       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
	       	}
		}
		public void alartListAdd(AlartInfo ai) {
			try {
	       		if (this.IsDisposed) return;
	       		if (!this.IsHandleCreated) return;
	        	this.Invoke((MethodInvoker)delegate() {
       		       	try {
						alartListDataSource.Add(ai);
       		       	} catch (Exception e) {
       		       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
       		       	}
				});
	       	} catch (Exception e) {
	       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
	       	}
		}public void taskListAdd(TaskInfo ti) {
			try {
	       		if (this.IsDisposed) return;
	       		if (!this.IsHandleCreated) return;
	        	this.Invoke((MethodInvoker)delegate() {
       		       	try {
						taskListDataSource.Add(ti);
       		       	} catch (Exception e) {
       		       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
       		       	}
				});
	       	} catch (Exception e) {
	       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
	       	}
		}
		public void setAlartListScrollIndex(int i) {
			try {
	       		if (this.IsDisposed) return;
	       		if (!this.IsHandleCreated) return;
	        	this.Invoke((MethodInvoker)delegate() {
       		       	try {
						alartList.FirstDisplayedScrollingRowIndex = i;
						alartList.CurrentCell = alartList[0, i];
       		       	} catch (Exception e) {
       		       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
       		       	}
				});
	       	} catch (Exception e) {
	       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
	       	}
		}
		public DialogResult showMessageBox(string text, string caption = "", MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.None, MessageBoxDefaultButton defBtn = MessageBoxDefaultButton.Button1) {
			var ret = DialogResult.Cancel;
			try {
	       		if (this.IsDisposed) return DialogResult.Cancel;
	       		if (!this.IsHandleCreated) return DialogResult.Cancel;
	       		 
	        	this.Invoke((MethodInvoker)delegate() {
       		       	try {
						ret = MessageBox.Show(text, caption, buttons, icon, defBtn);
       		       	} catch (Exception e) {
       		       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
       		       	}
				});
	       		return ret;
	       	} catch (Exception e) {
	       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
	       	}
			return ret;
		}
		
		
		
		
		
		void TaskListCellValidating(object sender, DataGridViewCellValidatingEventArgs e)
		{
			if (e.ColumnIndex == 0) {
				DateTime dt;
				if (!DateTime.TryParse(e.FormattedValue.ToString(), out dt)) {
					MessageBox.Show("有効な日時ではありません");
					e.Cancel = true;
				}
			}
		}
		
		
		void TaskListCurrentCellDirtyStateChanged(object sender, System.EventArgs e)
		{
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
		}
		void TaskListCellParsing(object sender, DataGridViewCellParsingEventArgs e)
		{
			util.debugWriteLine("cell parsing " + e.ColumnIndex);
			var target = ((TaskInfo)taskListDataSource[e.RowIndex]);
			if (e.ColumnIndex == 21) target.memo = (string)e.Value;
			util.debugWriteLine("cell parcing");
		}
		void TaskListOpenUrlMenuClick(object sender, EventArgs e)
		{
			try {
				var curCell = taskList.CurrentCell;
				if (curCell == null || curCell.RowIndex == -1) return;
				var ai = (TaskInfo)taskListDataSource[taskList.CurrentCell.RowIndex];
				if (ai.lvId == null || ai.lvId == "") return;
				var url = "http://live2.nicovideo.jp/watch/lv" + util.getRegGroup(ai.lvId, "(\\d+)");
				util.openUrlBrowser(url, config);
				
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		
		void TaskListCopyUrlMenuClick(object sender, EventArgs e)
		{
			var curCell = taskList.CurrentCell;
			if (curCell == null || curCell.RowIndex == -1) return;
			var ai = (TaskInfo)taskListDataSource[taskList.CurrentCell.RowIndex];
			if (ai.lvId == null || ai.lvId == "") return;
			var url = "http://live2.nicovideo.jp/watch/lv" + util.getRegGroup(ai.lvId, "(\\d+)");
			Clipboard.SetText(url);
		}
		
		void TaskListCopyArgsMenuClick(object sender, EventArgs e)
		{
			var curCell = taskList.CurrentCell;
			if (curCell == null || curCell.RowIndex == -1) return;
			var ai = (TaskInfo)taskListDataSource[taskList.CurrentCell.RowIndex];
			if (ai.args == null || ai.args == "") return;
			Clipboard.SetText(ai.args);
		}
		
		void TaskListRemoveLineMenuClick(object sender, EventArgs e)
		{
			var curCell = taskList.CurrentCell;
			if (curCell == null || curCell.RowIndex == -1) return;
			var ti = taskListDataSource[curCell.RowIndex];
			if (MessageBox.Show(ti.lvId + "の行を削除していいですか？", "確認", MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;
			taskListDataSource.RemoveAt(curCell.RowIndex);
		}
		public void taskListRemoveLine(TaskInfo ti) {
			try {
	       		if (this.IsDisposed) return;
	       		if (!this.IsHandleCreated) return;
	        	this.Invoke((MethodInvoker)delegate() {
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
	       		if (IsDisposed) return;
	       		if (!IsHandleCreated) return;
	        	Invoke((System.Windows.Forms.MethodInvoker)delegate() {
       		       	try {
	       		    	ti.status = "完了";
	       		    	if (taskListDataSource.IndexOf(ti) != -1)
	       		    		taskList.UpdateCellValue(4, taskListDataSource.IndexOf(ti));
       		       	} catch (Exception e) {
       		       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
       		       	}
				});
	       	} catch (Exception e) {
	       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
	       	}
		}
		
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
		void applyMenuSetting() {
			var columns = new string[] {"ShowComId",
					"ShowUserId","ShowComName","ShowUserName",
					"ShowKeyword","ShowComFollow","ShowUserFollow",
					"ShowLatestTime","ShowRegistTime","ShowPop",
					"ShowBalloon","ShowWeb", "ShowMail", "ShowSound","ShowAppA",
					"ShowAppB","ShowAppC","ShowAppD","ShowAppE",
					"ShowAppF","ShowAppG","ShowAppH","ShowAppI",
					"ShowAppJ","ShowMemo"};
			for(var i = 0; i < columns.Length; i++) {
				var isDisplay = bool.Parse(config.get(columns[i]));
				alartList.Columns[i].Visible = isDisplay;
				var menu = (ToolStripMenuItem)displayFavoriteTabMenu.DropDownItems[i];
				menu.Checked = isDisplay;
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
			
			var notifyOffs = new string[] {
					"OffPop","OffBalloon",
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
			updateAutoSortMenu.Checked = bool.Parse(config.get("AutoSort"));
			updateHideMemberOnlyWithoutFavoriteMenu.Checked = bool.Parse(config.get("BlindOnlyA"));
			updateHideMemberOnlyWithFavoriteMenu.Checked = bool.Parse(config.get("BlindOnlyB"));
			updateHideQuestionCategoryMenu.Checked = bool.Parse(config.get("BlindQuestion"));
			
			
		}
		void saveMenuSetting() {
			var setting = new Dictionary<string, string>();
			var columns = new string[] {"ShowComId",
					"ShowUserId","ShowComName","ShowUserName",
					"ShowKeyword","ShowComFollow","ShowUserFollow",
					"ShowLatestTime","ShowRegistTime","ShowPop",
					"ShowBalloon","ShowWeb","ShowMail","ShowSound","ShowAppA",
					"ShowAppB","ShowAppC","ShowAppD","ShowAppE",
					"ShowAppF","ShowAppG","ShowAppH","ShowAppI",
					"ShowAppJ","ShowMemo"};
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
			
			var columns = new string[] {"ShowComId",
					"ShowUserId","ShowComName","ShowUserName",
					"ShowKeyword","ShowComFollow","ShowUserFollow",
					"ShowLatestTime","ShowRegistTime","ShowPop",
					"ShowBalloon","ShowWeb","ShowMail","ShowSound","ShowAppA",
					"ShowAppB","ShowAppC","ShowAppD","ShowAppE",
					"ShowAppF","ShowAppG","ShowAppH","ShowAppI",
					"ShowAppJ","ShowMemo"};
			config.set(columns[i], alartList.Columns[i].Visible.ToString().ToLower());
         	
			if (i < 14 || i == 24) return;
			var n = (char)((int)'A' + i - 14);
			var item = (ToolStripMenuItem)contextMenuStrip1.Items.Find("openAppli" + n + "Menu", true)[0];
			item.Visible = alartList.Columns[i].Visible;
         	
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
		
		void SearchBtnClick(object sender, EventArgs e)
		{
			if (searchText.Text == "") return;
			var searchStr = searchText.Text;
			var cur = alartList.CurrentCell;
			if (cur == null || cur.RowIndex == -1) return;
			var targetColumns = new int[] {0, 1, 2, 3, 4, alartList.Columns.Count - 1};
			for (var i = cur.RowIndex; i < alartList.RowCount; i++) {
				foreach (var c in targetColumns) {
					if (i == cur.RowIndex && c <= cur.ColumnIndex) continue;
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
			MessageBox.Show("見つかりませんでした");
		}
		void AlartListRowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
		{
			favoriteNumLabel.Text = "登録数：" + alartList.RowCount + "件";
			if (!favoriteNumLabel.Visible) favoriteNumLabel.Visible = true;
		}
		void AlartListRowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
		{
			favoriteNumLabel.Text = "登録数：" + alartList.RowCount + "件";
			if (!favoriteNumLabel.Visible) favoriteNumLabel.Visible = true;
		}
		void NotifyIconBalloonTipClicked(object sender, EventArgs e)
		{
			var lvid = util.getRegGroup(notifyIcon.BalloonTipText + " " + notifyIcon.BalloonTipTitle, "(lv\\d+)");
			string url = null;
			if (lvid != null) {
				url = "http://live2.nicovideo.jp/watch/" + lvid;
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
			
		}
		
		void NotifyOffMenuItemDropDownOpening(object sender, EventArgs e)
		{
			for(var j = 0; j < notifyOffList.Length; j++) {
				if (j == 1) continue;
				((ToolStripMenuItem)notifyOffMenuItem.DropDownItems[j]).Checked = notifyOffList[j];
			}
		}
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
		public void followUpdate(List<string[]> followList) {
			while (true) {
				try {
					var c = getAlartListCount();
					for (var i = 0; i < c; i++) {
						var ai = alartListDataSource[i];
						if (ai.communityId != null && ai.communityId != "") {
							var isFollow = false;
							var name = ai.communityName;
							var newNameFollow = followList.Find(n => n[0] == ai.communityId);
							if (newNameFollow != null) {
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
								ai.hostName = newNameFollow[1];
								isFollow = true; 
							}
							ai.hostFollow = (isFollow) ? "フォロー解除する" : "フォローする";
						}
					}
					for (var i = 0; i < alartListDataSource.Count; i++) {
						alartList.UpdateCellValue(5, i);
						alartList.UpdateCellValue(6, i);
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
				
				item.Visible = //bool.Parse(config.get("ShowApp" + n));
					//((ToolStripMenuItem)displayFavoriteTabMenu.DropDownItems.Find("isDisplayAppli" + n + "TabMenu", true)[0]).Checked;
					alartList.Columns[i + 14].Visible;
				
				var name = config.get("appli" + n + "Name");
				if (name == "") name = "アプリ" + n;
				item.Text = "最近行われた放送のURLを" + name + "で開く";
				
				alartList.Columns[i + 14].HeaderText = name;
				taskList.Columns[i + 10].HeaderText = name;
			}
		}
		void recentLiveAppliOpenMenu_Click(object sender, EventArgs e)
		{
			try {
				var ai = alartListDataSource[alartList.CurrentCell.RowIndex];
				if (ai.lastLvid == "" || ai.lastLvid == null) return;
				
				var n = ((ToolStripMenuItem)sender).Name.Substring(9, 1);
				var path = config.get("appli" + n + "Path");
				var args = config.get("appli" + n + "Args");
				var url = "http://live2.nicovideo.jp/watch/" + ai.lastLvid;
				
				util.appliProcess(path, url + " " + args);
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		public void recentLiveCheck() {
			//var isCheck30min = bool.Parse(config.get("Ischeck30min"));
			//-1-no check 0-onair 1-30min
			var checkMode = bool.Parse(config.get("IscheckOnAir")) ? 0 : 1;
			if (!bool.Parse(config.get("IscheckRecent"))) checkMode = -1;
			
			var testDt = DateTime.Now;
			while (true) {
				try {
					var recentNum = 0;
					var c = getAlartListCount();
					for (var i = c - 1; i > -1; i--) {
						//util.debugWriteLine(i + " " + alartListDataSource[i].lastHosoDt + " " + alartList[7, i].Style.BackColor);
						var isRecentProcess = false;
						if (checkMode == 0) {
							isRecentProcess = isOnAir(alartListDataSource[i]);
							
						} else if (checkMode == 1) {
							isRecentProcess = DateTime.Now - alartListDataSource[i].lastHosoDt < TimeSpan.FromMinutes(30);
						}
						//var is30min = isCheck30min && DateTime.Now - alartListDataSource[i].lastHosoDt < TimeSpan.FromMinutes(30);
						alartListDataSource[i].isRecentColor = isRecentProcess;
						var isRecentColor = alartList[7, i].Style.BackColor == Color.FromArgb(255, 224, 255);
						if (isRecentProcess != isRecentColor) {
							//alartList[7, i].Style.BackColor = (i % 2 != 0) ? 
							//	Color.FromArgb(245, 245, 245)
							//	: Color.FromName("window");
							alartList.UpdateCellValue(7, i);
						}
						if (isRecentProcess) recentNum++;
					}
					//var ii = notifyIcon.Icon == Icon;
					if (bool.Parse(config.get("IschangeIcon")) &&
					    	checkMode != -1)
						changeIcon(recentNum);
					else if (notifyIcon.Icon != Icon) 
						notifyIcon.Icon = Icon;
					
					break;
					
				} catch (Exception e) {
					util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				}
			}
		}
		private bool isOnAir(AlartInfo ai) {
			if (string.IsNullOrEmpty(ai.lastLvid)) return false;
			
			var elapsed = DateTime.Now - ai.lastHosoDt;
			var isCheck = false;
			if (ai.communityId == null ||
			    	ai.communityId.StartsWith("ch")) {
				isCheck = elapsed < TimeSpan.FromHours(100);
			} else isCheck = elapsed < TimeSpan.FromHours(6);
			if (!isCheck) return false;
			
			var url = "https://live.nicovideo.jp/embed/" + ai.lastLvid;
			var res = util.getPageSource(url, null);
			return res.IndexOf("status-onair\">") > -1;
		}
		private void changeIcon(int recentNum) {
			var n = recentNum;
			if (recentNum > 9) recentNum = 9;
			notifyIcon.Icon = new Icon("Icon/number4_" + recentNum.ToString() + "c.ico");
			//this.Invoke((MethodInvoker)delegate() {
			//	Icon = new Icon("Icon/number4_" + recentNum.ToString() + "c.ico");
			//});
		}
		
		void AlartListCellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			try {
				if (e.RowIndex < 0) return;
				var ai = alartListDataSource[e.RowIndex];
				var action = config.get("doublecmode");
				
				if (action == "なにもしない") return;
				else if (action.StartsWith("最近行われた放送のURLを")) {
					if (ai.lastLvid != null && ai.lastLvid != "") {
						var url = "http://live2.nicovideo.jp/watch/" + ai.lastLvid;
						if (action.EndsWith("開く"))
							util.openUrlBrowser(url, config);
						else Clipboard.SetText(url);
					}
				} else if (action.StartsWith("コミュニティURLを")) {
					if (ai.communityId != null && ai.communityId != "") {
						var url = (ai.communityId.IndexOf("ch") > -1) ? 
								("http://ch.nicovideo.jp/" + ai.communityId) :
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
		    				alartList.UpdateCellValue(6, row);
		    			}
		    			if (!isUser && 
						    ai.communityId == id) {
		    				ai.communityFollow = (isFollow) ? "フォロー解除する" : "フォローする";
		    				var row = alartListDataSource.IndexOf(ai);
		    				alartList.UpdateCellValue(5, row);
		    			}
	       			}
	       			break;
	       		} catch (Exception e) {
		       		util.debugWriteLine(e.Message + " " + e.StackTrace + " " + e.Source + " " + e.TargetSite);
   		       	}
	       	}
		}

		void BulkUserFollowMenuClick(object sender, EventArgs e)
		{
			Task.Run(() =>
				new ListFollow(this, check).userFollow()
			);
		}
		void BulkCommunityFollowMenuClick(object sender, EventArgs e)
		{
			Task.Run(() =>
				new ListFollow(this, check).communityFollow()
			);
		}
		void BulkChannelFollowMenuClick(object sender, EventArgs e)
		{
			Task.Run(() =>
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
			foreach (System.Windows.Forms.RadioButton c in categoryBtnPanel.Controls) {
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
		
		
		void Button1Click(object sender, EventArgs e)
		{
			
		}
		
		void CategoryLeftBtnClick(object sender, EventArgs e)
		{
			var btnsWidth = 0;
			foreach (Control b in categoryBtnPanel.Controls) btnsWidth += b.Width;
			if (btnsWidth <= categoryBtnPanel.Size.Width) return;
			for (var i = 1; i < 16; i++) {
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
			categoryBtnDisplayUpdate();
		}
		void categoryBtnDisplayUpdate() {
			//var freeWidth = liveListSearchText.Location.X - categoryBtnPanel.Location.X - 3;
			var btnsWidth = 16 * 3;
			foreach (Control b in categoryBtnPanel.Controls) btnsWidth += b.Width;
			
			var freeWidth = Size.Width - categoryBtnPanel.Location.X - 175;
			//var isDisplayBtn = categoryBtnPanel.PreferredSize.Width > freeWidth;
			var isDisplayBtn = btnsWidth > freeWidth;
			if (isDisplayBtn != categoryLeftBtn.Visible) {
				categoryLeftBtn.Visible = categoryRightBtn.Visible = isDisplayBtn;
				categoryBtnPanel.Width = (isDisplayBtn) ? (freeWidth - 35) : freeWidth;
			}
			
			var headCateIndex = 0;
			for (var i = 0; i < 16; i++) {
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
		public void formAction(Action a) {
			if (IsDisposed || !util.isShowWindow) return;
			
			if (Thread.CurrentThread == madeThread) {
				try {
					a.Invoke();
				} catch (Exception e) {
					util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				}
			} else {
				try {
					Invoke((MethodInvoker)delegate() {
						try {    
				       		a.Invoke();
				       	} catch (Exception e) {
							util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
						}
					});
				} catch (Exception e) {
					util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
				} 
			}
		}
		public void removeLiveListItem(LiveInfo li) {
			lock(liveListLock) {
				for (var i = liveList.Rows.Count - 1; i > -1; i--) {
					if (liveList.Rows[i].DataBoundItem == li) {
						
//						util.debugWriteLine("remove mae [0] " + liveList.Rows[0].Visible + " " + liveList.Rows[0].Cells[3].Value + " " + liveListDataSource[0].hostName);
						formAction(() => {liveList.Rows.RemoveAt(i);});
//						util.debugWriteLine("remove ato [0] " + liveList.Rows[0].Visible + " " + liveList.Rows[0].Cells[3].Value + " " + liveListDataSource[0].hostName);
						util.debugWriteLine("remove " + i + " " + li.hostName);
						
					}
				}
					//liveListDataSource.Remove(li);
				
				liveListDataReserve.Remove(li);
			}
		}
		public void addLiveListItem(LiveInfo li, char cateChar, bool blindOnlyA, bool blindOnlyB, bool blindQuestion) {
			var isDisp = (cateChar == '全' || li.MainCategory[0] == cateChar);
			var isMemberOnly = !string.IsNullOrEmpty(li.memberOnly); 
	        if (bool.Parse(config.get("BlindOnlyA")) && isMemberOnly &&
			   		!string.IsNullOrEmpty(li.favorite))
	        	isDisp = false;
	        if (bool.Parse(config.get("BlindOnlyB")) && isMemberOnly)
	        	isDisp = false;
	        if (bool.Parse(config.get("BlindQuestion")) && cateChar != '全' &&
	            	string.IsNullOrEmpty(li.lvId))
	        	isDisp = false;
					        
			if (isDisp)	formAction(() => liveListDataSource.Add(li));
			else liveListDataReserve.Add(li);
			
			if (!isLiveListTimeProcessing) {
			//lock(liveListLock) {
				isLiveListTimeProcessing = true;
				Task.Run(() => liveListTimeProcess());
			}
		}
		public bool insertLiveListItem(LiveInfo li, int index, char cateChar) {
			if (cateChar == '全' || li.MainCategory[0] == cateChar) {
				formAction(() => liveListDataSource.Insert(index, li));
				return true;
			} else {
				liveListDataReserve.Add(li);
				return false;
			}
			/*
			if (!isLiveListTimeProcessing) {
			//lock(liveListLock) {
				isLiveListTimeProcessing = true;
				Task.Run(() => liveListTimeProcess());
			}
			*/
		}
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
			lock(liveListLock) {
				util.debugWriteLine("deletelivelistTime 0");
				formAction(() => {
					for (var i = liveListDataSource.Count - 1; i > -1; i--) {
						if (delMin != 0 && ((TimeSpan)(now - liveListDataSource[i].pubDateDt)).TotalMinutes > delMin)
							 liveListDataSource.RemoveAt(i);
						else liveList.UpdateCellValue(8, i);
					}
				});
				util.debugWriteLine("deletelivelistTime 1");
				for (var i = liveListDataReserve.Count - 1; i > -1; i--) {
					if (delMin != 0 && ((TimeSpan)(now - liveListDataReserve[i].pubDateDt)).TotalMinutes > delMin)
						 liveListDataReserve.RemoveAt(i);
				}
			}
			util.debugWriteLine("deletelivelistTime end");
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
			var act = config.get("brodouble");
			var li = liveListDataSource[e.RowIndex];
			var url = "https://live.nicovideo.jp/watch/" + li.lvId;
			
			var comUrl = "";
			if (li.comId != null && li.comId != "") {
				var isChannel = liveListDataSource[e.RowIndex].comId.IndexOf("ch") > -1;
				comUrl = (isChannel) ? 
					("http://ch.nicovideo.jp/" + li.comId) :
					("https://com.nicovideo.jp/community/" + li.comId);
			}
			
			if (act == "1") {
				util.openUrlBrowser(url, config);
			} else if (act == "2") {
				util.openUrlBrowser(comUrl, config);
			} else if (act == "3") {
				Clipboard.SetText(url);
			} else if (act == "4") {
				Clipboard.SetText(comUrl);
			} else if (act == "5") {
				openAddForm(li.comId);
			}
		}
		void LiveListOpenUrlMenuClick(object sender, EventArgs e)
		{
			try {
				var curCell = liveList.CurrentCell;
				if (curCell == null || curCell.RowIndex == -1) return;
				var li = (LiveInfo)liveListDataSource[curCell.RowIndex];
				if (li.lvId == null || li.lvId == "") return;
				var url = "http://live.nicovideo.jp/watch/" + li.lvId;
				util.openUrlBrowser(url, config);
				
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		void LiveListOpenCommunityUrlMenuClick(object sender, EventArgs e)
		{
			try {
				var curCell = liveList.CurrentCell;
				if (curCell == null || curCell.RowIndex == -1) return;
				var li = (LiveInfo)liveListDataSource[curCell.RowIndex];
				if (li.comId == null || li.comId == "") return;
				var isChannel = li.comId.IndexOf("ch") > -1;
				var comUrl = (isChannel) ? 
					("http://ch.nicovideo.jp/" + li.comId) :
					("https://com.nicovideo.jp/community/" + li.comId);
				util.openUrlBrowser(comUrl, config);
				
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
			if (MessageBox.Show("お気に入りリストから" + li.comId + "の行を削除していいですか？", "確認", MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;
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
			var cur = liveList.CurrentCell;
			if (cur.RowIndex == -1) return;
			var li = liveListDataSource[cur.RowIndex];
			if (MessageBox.Show(li.lvId + "の行を削除していいですか？", "確認", MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;
			lock(liveListLock) {
				liveListDataSource.Remove(li);
			}
			setLiveListNum();
		}
		
		void LiveListCopyMenuDropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			var cur = liveList.CurrentCell;
			if (cur.RowIndex == -1) return;
			var li = liveListDataSource[cur.RowIndex];
			var t = e.ClickedItem.Text;
			if (t == "放送URL") {
				var url = "http://live.nicovideo.jp/watch/" + li.lvId;
				Clipboard.SetText(url);
			} else if (t == "コミュニティURL") {
				var isChannel = li.comId.IndexOf("ch") > -1;
				var comUrl = (isChannel) ? 
						("http://ch.nicovideo.jp/" + li.comId) :
						("https://com.nicovideo.jp/community/" + li.comId);
				Clipboard.SetText(comUrl);
			} else if (t == "放送タイトル") {
				Clipboard.SetText(li.title);
			} else if (t == "放送者") {
				Clipboard.SetText(li.hostName);
			} else if (t == "コミュニティ名") {
				Clipboard.SetText(li.comName);
			} else if (t == "説明") {
				Clipboard.SetText(li.description);
			}
		}
		
		public void setLiveListNum() {
			var l = new List<LiveInfo>(liveListDataSource);
			var allC = liveListDataSource.Count + liveListDataReserve.Count;
			
			
			var mainCategories = new string[]{"一般(その他)", "政治",
					"動物", "料理", "演奏してみた", "歌ってみた", "踊ってみた",
					"描いてみた", "講座", "ゲーム", "動画紹介", "R18",
					"顔出し", "凸待ち", "クルーズ待ち"};
			var btns = new System.Windows.Forms.RadioButton[]{commonCategoryBtn, politicsCategoryBtn,
					animalCategoryBtn, cookCategoryBtn, playCategoryBtn, 
					singCategoryBtn, danceCategoryBtn, drawCategoryBtn, 
					lectureCategoryBtn, gameCategoryBtn, introCategoryBtn, 
					r18CategoryBtn, faceCategoryBtn, rushCategoryBtn, cruiseCategoryBtn};
			
			var btnsText = new List<string>();
			for (var i = 0; i < mainCategories.Length; i++) {
				var n = l.Count(x => x.MainCategory == mainCategories[i]);
				var reserveN = liveListDataReserve.Count(x => x.MainCategory == mainCategories[i]);
				var t = (mainCategories[i].StartsWith("一般")) ? "一般" : mainCategories[i];
				btnsText.Add(string.Format(t + "({0}/{1})", n + reserveN, allC));
				
				//test
				foreach (var r in liveListDataReserve) {
					if (r.MainCategory == mainCategories[i]) {
//						util.debugWriteLine(r.hostName + " " + r.MainCategory + " " + r.memberOnly);
						
					}
				}
			}
			formAction(() => {
				allCategoryBtn.Text = string.Format("全て({0}/{1})", allC, allC);
				for (var i = 0; i < mainCategories.Length; i++) {
					btns[i].Text = btnsText[i];
				}
			});
			
		}
		
		void CategoryBtnCheckedChanged(object sender, EventArgs e)
		{
			//util.debugWriteLine((System.Windows.Forms.RadioButton)sender);
			if (!((System.Windows.Forms.RadioButton)sender).Checked) return;
			
			lock(liveListLock) {
				var mainCategories = new string[]{"一般(その他)", "政治",
						"動物", "料理", "演奏してみた", "歌ってみた", "踊ってみた",
						"描いてみた", "講座", "ゲーム", "動画紹介", "R18",
						"顔出し", "凸待ち", "クルーズ待ち"};
				var btns = new System.Windows.Forms.RadioButton[]{commonCategoryBtn, politicsCategoryBtn,
						animalCategoryBtn, cookCategoryBtn, playCategoryBtn, 
						singCategoryBtn, danceCategoryBtn, drawCategoryBtn, 
						lectureCategoryBtn, gameCategoryBtn, introCategoryBtn, 
						r18CategoryBtn, faceCategoryBtn, rushCategoryBtn, cruiseCategoryBtn};
				//var n = ((Control)sender).Name;
				var isAll = sender == allCategoryBtn;
				var cateName = (isAll) ? null : mainCategories[Array.IndexOf(btns, sender)];
				try {
					liveList.CurrentCell = null;
					for (var i = liveList.Rows.Count - 1; i > -1; i--) {
						var isDisplay = (isAll || liveListDataSource[i].MainCategory == cateName);
						var isMemberOnly = !string.IsNullOrEmpty(liveListDataSource[i].memberOnly);
						
				        if (bool.Parse(config.get("BlindOnlyA")) && isMemberOnly &&
						   		string.IsNullOrEmpty(liveListDataSource[i].favorite))
				        	isDisplay = false;
				        if (bool.Parse(config.get("BlindOnlyB")) && isMemberOnly)
				        	isDisplay = false;
						if (bool.Parse(config.get("BlindQuestion")) && cateName != "全て" &&
				            	string.IsNullOrEmpty(liveListDataSource[i].lvId))
				        	isDisplay = false;
				        
						if (!isDisplay) {
							liveListDataReserve.Add(liveListDataSource[i]);
							liveListDataSource.Remove(liveListDataSource[i]);
						}
					}
					//for (var i = 0; i < liveListDataSource.Count; i++)
					//	liveList.Rows[i].Visible =
					//		(isAll || liveListDataSource[i].MainCategory == cateName);
				} catch (Exception ee) {
					util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
				}
				
				getVisiRow();
				
				try {
					for (var i = liveListDataReserve.Count - 1; i > -1; i--) {
						var isDisplay = (isAll || liveListDataReserve[i].MainCategory == cateName);
						var isMemberOnly = !string.IsNullOrEmpty(liveListDataReserve[i].memberOnly);
						
				        if (bool.Parse(config.get("BlindOnlyA")) && isMemberOnly &&
						   		string.IsNullOrEmpty(liveListDataReserve[i].favorite))
				        	isDisplay = false;
				        if (bool.Parse(config.get("BlindOnlyB")) && isMemberOnly)
				        	isDisplay = false;
				        if (bool.Parse(config.get("BlindQuestion")) && cateName != "全て" &&
				            	string.IsNullOrEmpty(liveListDataReserve[i].lvId))
				        	isDisplay = false;
				        if (isDisplay) {
							liveListDataSource.Add(liveListDataReserve[i]);
							liveListDataReserve.RemoveAt(i);
						}
					}
				} catch (Exception ee) {
					util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
				}
				
				util.debugWriteLine("b");
				getVisiRow();
				
				sortLiveList();
				
				util.debugWriteLine("d");
				getVisiRow();
				
				if (bool.Parse(config.get("FavoriteUp")))
					upLiveListFavorite();
				
				util.debugWriteLine("a");
				getVisiRow();
				
				util.debugWriteLine("c ");
				getVisiRow();
				
				for (var i = liveListDataSource.Count - 1; i > -1; i--) {
					var isDisp = (liveListDataSource[i].MainCategory == cateName ||
					            cateName == null);
					if (!isDisp) {
						liveListDataReserve.Add(liveListDataSource[i]);
						liveListDataSource.RemoveAt(i);
					}
				}
				
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
			Task.Run(() => liveCheck.load());
		}
		void UpdateAutoDeleteMenuDropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			((ToolStripMenuItem)updateMenuItem).HideDropDown();
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
			deleteLiveListTime();
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
			((ToolStripMenuItem)updateMenuItem).HideDropDown();
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
			config.set("FavoriteUp", ((ToolStripMenuItem)sender).Checked.ToString().ToLower());
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
		}
		void UpdateHideMemberOnlyWithFavoriteMenuClick(object sender, EventArgs e)
		{
			updateHideMemberOnlyWithoutFavoriteMenu.Checked = false;
			updateHideMemberOnlyWithFavoriteMenu.Checked = 
					!updateHideMemberOnlyWithFavoriteMenu.Checked;
			config.set("BlindOnlyB", ((ToolStripMenuItem)sender).Checked.ToString().ToLower());
		}
		
		void UpdateHideQuestionCategoryMenuCheckedChanged(object sender, EventArgs e)
		{
			config.set("BlindQuestion", ((ToolStripMenuItem)sender).Checked.ToString().ToLower());
		}
		public void upLiveListFavorite() {
			try  {
				//var favorites = liveListDataSource.Select(li => !string.IsNullOrEmpty(li.Favorite));
				//liveListDataSource.
				formAction(() => {
					var cateChar = getCategoryChar();
					
					util.debugWriteLine("e");
					getVisiRow();
					
					var favoriteList = new List<LiveInfo>();
					foreach (var f in liveListDataSource) 
						if (!string.IsNullOrEmpty(f.favorite) &&
						    	(cateChar == '全' || f.MainCategory[0] == cateChar))
							favoriteList.Add(f);
					
					util.debugWriteLine("g");
					getVisiRow();
					
					foreach (var f in favoriteList) liveListDataSource.Remove(f);
					
					util.debugWriteLine("h");
					getVisiRow();
					
					favoriteList.Reverse();
					
					util.debugWriteLine("f");
					getVisiRow();
					
					foreach (var f in favoriteList) {
						liveListDataSource.Insert(0, f);
					}
					
					util.debugWriteLine("g");
					getVisiRow();
					
				});
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
		}
		
		void LiveListSorted(object sender, EventArgs e)
		{
			upLiveListFavorite();
			
		}
		
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
			MessageBox.Show("見つかりませんでした");
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
			//liveListDataSource[cur.RowIndex].thumbnail = img;
			if (img == null) return;
			li.thumbnail = img;
			ThumbnailManager.saveImage(img, li.comId);
			
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
			if (li.thumbnail == null) {
				li.thumbnail = ThumbnailManager.getThumbnailRssUrl(li.thumbnailUrl);
				if (li.thumbnail == null) return;
			}
			var img = ThumbnailManager.writeMemo(li.thumbnail);
			if (img == null) return;
			li.thumbnail = img;
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
			if (li.thumbnail == null) {
				li.thumbnail = ThumbnailManager.getThumbnailRssUrl(li.thumbnailUrl);
				if (li.thumbnail == null) return;
			}
			var img = ThumbnailManager.writeNG(li.thumbnail);
			if (img == null) return;
			li.thumbnail = img;
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
			
			if (e.RowIndex == -1)
				return;
			var ai = alartListDataSource[e.RowIndex];
			
			var isExistComThumb = false;
			System.Drawing.Image img = null;
			if (!string.IsNullOrEmpty(ai.communityId)) {
				isExistComThumb = ThumbnailManager.isExist(ai.communityId, out img);
			}
			try {
				comThumbBox.Image = 
					isExistComThumb ?
						new Bitmap(img, comThumbBox.Size) :
						new Bitmap(System.Drawing.Image.FromFile("ImageCommunity/no thumb com.jpg"), comThumbBox.Size);
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
			
			var isExistUserThumb = false;
			if (!string.IsNullOrEmpty(ai.hostId)) {
				isExistUserThumb = ThumbnailManager.isExist(ai.hostId, out img);
			}
			try {
				userThumbBox.Image = 
					isExistUserThumb ?
						new Bitmap(img, userThumbBox.Size) :
						new Bitmap(System.Drawing.Image.FromFile("ImageUser/blank.jpg"), userThumbBox.Size);
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
	}
}
