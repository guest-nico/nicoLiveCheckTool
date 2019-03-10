using System.Runtime.InteropServices;
/*
 * Created by SharpDevelop.
 * User: pc
 * Date: 2018/04/06
 * Time: 20:55
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace namaichi
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.logText = new System.Windows.Forms.TextBox();
			this.groupLabel = new System.Windows.Forms.Label();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.readNamarokuListMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.終了ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.notifyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.notifyOffMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.allOffMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			this.popupOffMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.balloonOffMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.webOffMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mailOffMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.soundOffMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.appliAOffMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.appliBOffMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.appliCOffMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.appliDOffMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.appliEOffMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.appliFOffMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.appliGOffMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.appliHOffMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.appliIOffMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.appliJOffMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.displayMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.displayFavoriteTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isDisplayCommunityIdTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isDisplayUserIdTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isDisplayCommunityNameTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isDisplayUserNameTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isDisplayKeywordTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isDisplayCommunityFollowTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isDisplayUserFollowTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isDisplayLastHosoDtTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isDisplayAddDateDtTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isDisplayPopupTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isDisplayBalloonTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isDisplayWebTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isDisplayMailTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isDisplaySoundTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isDisplayAppliATabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isDisplayAppliBTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isDisplayAppliCTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isDisplayAppliDTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isDisplayAppliETabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isDisplayAppliFTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isDisplayAppliGTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isDisplayAppliHTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isDisplayAppliITabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isDisplayAppliJTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isDisplayMemoTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.displayTaskTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isTaskListDisplayStartTimeTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isTaskListDisplayLvidTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isTaskListDisplayArgsTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isTaskListDisplayAddDtTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isTaskListDisplayStatusTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isTaskListDisplayPopupTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isTaskListDisplayBalloonTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isTaskListDisplayWebTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isTaskListDisplayMailTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isTaskListDisplaySoundTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isTaskListDisplayAppliATabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isTaskListDisplayAppliBTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isTaskListDisplayAppliCTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isTaskListDisplayAppliDTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isTaskListDisplayAppliETabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isTaskListDisplayAppliFTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isTaskListDisplayAppliGTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isTaskListDisplayAppliHTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isTaskListDisplayAppliITabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isTaskListDisplayAppliJTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isTaskListDisplayDeleteTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isTaskListDisplayMemoTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.bulkUserFollowMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.bulkCommunityFollowMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.bulkChannelFollowMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
			this.optionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.バージョン情報VToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.lastHosoStatusBar = new System.Windows.Forms.ToolStripStatusLabel();
			this.addBtn = new System.Windows.Forms.Button();
			this.alartList = new System.Windows.Forms.DataGridView();
			this.ｺﾐｭﾆﾃｨID = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ﾕｰｻﾞｰID = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ｺﾐｭﾆﾃｨ名 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ﾕｰｻﾞｰ名 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ｷｰﾜｰﾄﾞ = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ｺﾐｭﾆﾃｨﾌｫﾛｰ = new System.Windows.Forms.DataGridViewButtonColumn();
			this.ﾕｰｻﾞｰﾌｫﾛｰ = new System.Windows.Forms.DataGridViewButtonColumn();
			this.最近の放送日時 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.登録日時 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ﾎﾟｯﾌﾟｱｯﾌﾟ = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.ﾊﾞﾙｰﾝ = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.Web = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.ﾒｰﾙ = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.音 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.アプリA = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.アプリB = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.アプリC = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.ｱﾌﾟﾘC = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.ｱﾌﾟﾘE = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.ｱﾌﾟﾘF = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.ｱﾌﾟﾘG = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.ｱﾌﾟﾘH = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.ｱﾌﾟﾘI = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.ｱﾌﾟﾘJ = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.openLastHosoMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.openCommunityUrlMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.openUserUrlMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.copyLastHosoMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.copyCommunityUrlMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.copyUserUrlMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.removeLineMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
			this.openAppliAMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.openAppliBMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.openAppliCMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.openAppliDMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.openAppliEMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.openAppliFMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.openAppliGMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.openAppliHMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.openAppliIMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.openAppliJMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.notifyIconMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.openNotifyIconMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.closeNotifyIconMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.favoriteNumLabel = new System.Windows.Forms.Label();
			this.searchBtn = new System.Windows.Forms.Button();
			this.searchText = new System.Windows.Forms.TextBox();
			this.downButton = new System.Windows.Forms.Button();
			this.upButton = new System.Windows.Forms.Button();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.addYoyakuBtn = new System.Windows.Forms.Button();
			this.taskList = new System.Windows.Forms.DataGridView();
			this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.登録日時task = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dataGridViewCheckBoxColumn2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dataGridViewCheckBoxColumn3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.taskMailChkBox = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dataGridViewCheckBoxColumn4 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dataGridViewCheckBoxColumn5 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dataGridViewCheckBoxColumn6 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dataGridViewCheckBoxColumn7 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dataGridViewCheckBoxColumn8 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dataGridViewCheckBoxColumn9 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dataGridViewCheckBoxColumn10 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.taskAppliG = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.taskAppliH = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.taskAppliI = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.taskAppliJ = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.自動削除 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.taskListOpenUrlMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.taskListCopyUrlMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.taskListCopyArgsMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.taskListRemoveLineMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.alartList)).BeginInit();
			this.contextMenuStrip1.SuspendLayout();
			this.notifyIconMenuStrip.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.taskList)).BeginInit();
			this.contextMenuStrip2.SuspendLayout();
			this.SuspendLayout();
			// 
			// logText
			// 
			this.logText.Location = new System.Drawing.Point(400, 10);
			this.logText.Margin = new System.Windows.Forms.Padding(2);
			this.logText.Multiline = true;
			this.logText.Name = "logText";
			this.logText.ReadOnly = true;
			this.logText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.logText.Size = new System.Drawing.Size(269, 42);
			this.logText.TabIndex = 5;
			// 
			// groupLabel
			// 
			this.groupLabel.Location = new System.Drawing.Point(0, 0);
			this.groupLabel.Name = "groupLabel";
			this.groupLabel.Size = new System.Drawing.Size(100, 23);
			this.groupLabel.TabIndex = 0;
			this.groupLabel.Text = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.fileMenuItem,
									this.notifyMenuItem,
									this.displayMenuItem,
									this.toolMenuItem,
									this.helpMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
			this.menuStrip1.Size = new System.Drawing.Size(899, 26);
			this.menuStrip1.TabIndex = 11;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileMenuItem
			// 
			this.fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.readNamarokuListMenu,
									this.toolStripSeparator1,
									this.終了ToolStripMenuItem});
			this.fileMenuItem.Name = "fileMenuItem";
			this.fileMenuItem.ShowShortcutKeys = false;
			this.fileMenuItem.Size = new System.Drawing.Size(85, 22);
			this.fileMenuItem.Text = "ファイル(&F)";
			// 
			// readNamarokuListMenu
			// 
			this.readNamarokuListMenu.Name = "readNamarokuListMenu";
			this.readNamarokuListMenu.Size = new System.Drawing.Size(363, 22);
			this.readNamarokuListMenu.Text = "namarokuの登録コミュニティ設定を読み込んでみる";
			this.readNamarokuListMenu.Click += new System.EventHandler(this.ReadNamarokuListMenuClick);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(360, 6);
			// 
			// 終了ToolStripMenuItem
			// 
			this.終了ToolStripMenuItem.Name = "終了ToolStripMenuItem";
			this.終了ToolStripMenuItem.Size = new System.Drawing.Size(363, 22);
			this.終了ToolStripMenuItem.Text = "終了(&X)";
			this.終了ToolStripMenuItem.Click += new System.EventHandler(this.endMenu_Click);
			// 
			// notifyMenuItem
			// 
			this.notifyMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.notifyOffMenuItem});
			this.notifyMenuItem.Name = "notifyMenuItem";
			this.notifyMenuItem.Size = new System.Drawing.Size(63, 22);
			this.notifyMenuItem.Text = "通知(&N)";
			// 
			// notifyOffMenuItem
			// 
			this.notifyOffMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.allOffMenuItem,
									this.toolStripSeparator7,
									this.popupOffMenuItem,
									this.balloonOffMenuItem,
									this.webOffMenuItem,
									this.mailOffMenuItem,
									this.soundOffMenuItem,
									this.appliAOffMenuItem,
									this.appliBOffMenuItem,
									this.appliCOffMenuItem,
									this.appliDOffMenuItem,
									this.appliEOffMenuItem,
									this.appliFOffMenuItem,
									this.appliGOffMenuItem,
									this.appliHOffMenuItem,
									this.appliIOffMenuItem,
									this.appliJOffMenuItem});
			this.notifyOffMenuItem.Name = "notifyOffMenuItem";
			this.notifyOffMenuItem.Size = new System.Drawing.Size(147, 22);
			this.notifyOffMenuItem.Text = "一時機能OFF";
			this.notifyOffMenuItem.DropDownOpening += new System.EventHandler(this.NotifyOffMenuItemDropDownOpening);
			this.notifyOffMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.NotifyOffMenuItemDropDownItemClicked);
			// 
			// allOffMenuItem
			// 
			this.allOffMenuItem.Name = "allOffMenuItem";
			this.allOffMenuItem.Size = new System.Drawing.Size(183, 22);
			this.allOffMenuItem.Text = "全通知　OFF";
			// 
			// toolStripSeparator7
			// 
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new System.Drawing.Size(180, 6);
			// 
			// popupOffMenuItem
			// 
			this.popupOffMenuItem.Name = "popupOffMenuItem";
			this.popupOffMenuItem.Size = new System.Drawing.Size(183, 22);
			this.popupOffMenuItem.Text = "ポップアップ　OFF";
			// 
			// balloonOffMenuItem
			// 
			this.balloonOffMenuItem.Name = "balloonOffMenuItem";
			this.balloonOffMenuItem.Size = new System.Drawing.Size(183, 22);
			this.balloonOffMenuItem.Text = "バルーン　OFF";
			// 
			// webOffMenuItem
			// 
			this.webOffMenuItem.Name = "webOffMenuItem";
			this.webOffMenuItem.Size = new System.Drawing.Size(183, 22);
			this.webOffMenuItem.Text = "Web　OFF";
			// 
			// mailOffMenuItem
			// 
			this.mailOffMenuItem.Name = "mailOffMenuItem";
			this.mailOffMenuItem.Size = new System.Drawing.Size(183, 22);
			this.mailOffMenuItem.Text = "メール　OFF";
			// 
			// soundOffMenuItem
			// 
			this.soundOffMenuItem.Name = "soundOffMenuItem";
			this.soundOffMenuItem.Size = new System.Drawing.Size(183, 22);
			this.soundOffMenuItem.Text = "音　OFF";
			// 
			// appliAOffMenuItem
			// 
			this.appliAOffMenuItem.Name = "appliAOffMenuItem";
			this.appliAOffMenuItem.Size = new System.Drawing.Size(183, 22);
			this.appliAOffMenuItem.Text = "アプリA　OFF";
			// 
			// appliBOffMenuItem
			// 
			this.appliBOffMenuItem.Name = "appliBOffMenuItem";
			this.appliBOffMenuItem.Size = new System.Drawing.Size(183, 22);
			this.appliBOffMenuItem.Text = "アプリB　OFF";
			// 
			// appliCOffMenuItem
			// 
			this.appliCOffMenuItem.Name = "appliCOffMenuItem";
			this.appliCOffMenuItem.Size = new System.Drawing.Size(183, 22);
			this.appliCOffMenuItem.Text = "アプリC　OFF";
			// 
			// appliDOffMenuItem
			// 
			this.appliDOffMenuItem.Name = "appliDOffMenuItem";
			this.appliDOffMenuItem.Size = new System.Drawing.Size(183, 22);
			this.appliDOffMenuItem.Text = "アプリD　OFF";
			// 
			// appliEOffMenuItem
			// 
			this.appliEOffMenuItem.Name = "appliEOffMenuItem";
			this.appliEOffMenuItem.Size = new System.Drawing.Size(183, 22);
			this.appliEOffMenuItem.Text = "アプリE　OFF";
			// 
			// appliFOffMenuItem
			// 
			this.appliFOffMenuItem.Name = "appliFOffMenuItem";
			this.appliFOffMenuItem.Size = new System.Drawing.Size(183, 22);
			this.appliFOffMenuItem.Text = "アプリF　OFF";
			// 
			// appliGOffMenuItem
			// 
			this.appliGOffMenuItem.Name = "appliGOffMenuItem";
			this.appliGOffMenuItem.Size = new System.Drawing.Size(183, 22);
			this.appliGOffMenuItem.Text = "アプリG　OFF";
			// 
			// appliHOffMenuItem
			// 
			this.appliHOffMenuItem.Name = "appliHOffMenuItem";
			this.appliHOffMenuItem.Size = new System.Drawing.Size(183, 22);
			this.appliHOffMenuItem.Text = "アプリH　OFF";
			// 
			// appliIOffMenuItem
			// 
			this.appliIOffMenuItem.Name = "appliIOffMenuItem";
			this.appliIOffMenuItem.Size = new System.Drawing.Size(183, 22);
			this.appliIOffMenuItem.Text = "アプリI　OFF";
			// 
			// appliJOffMenuItem
			// 
			this.appliJOffMenuItem.Name = "appliJOffMenuItem";
			this.appliJOffMenuItem.Size = new System.Drawing.Size(183, 22);
			this.appliJOffMenuItem.Text = "アプリJ　OFF";
			// 
			// displayMenuItem
			// 
			this.displayMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.displayFavoriteTabMenu,
									this.displayTaskTabMenu});
			this.displayMenuItem.Name = "displayMenuItem";
			this.displayMenuItem.Size = new System.Drawing.Size(62, 22);
			this.displayMenuItem.Text = "表示(&V)";
			// 
			// displayFavoriteTabMenu
			// 
			this.displayFavoriteTabMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.isDisplayCommunityIdTabMenu,
									this.isDisplayUserIdTabMenu,
									this.isDisplayCommunityNameTabMenu,
									this.isDisplayUserNameTabMenu,
									this.isDisplayKeywordTabMenu,
									this.isDisplayCommunityFollowTabMenu,
									this.isDisplayUserFollowTabMenu,
									this.isDisplayLastHosoDtTabMenu,
									this.isDisplayAddDateDtTabMenu,
									this.isDisplayPopupTabMenu,
									this.isDisplayBalloonTabMenu,
									this.isDisplayWebTabMenu,
									this.isDisplayMailTabMenu,
									this.isDisplaySoundTabMenu,
									this.isDisplayAppliATabMenu,
									this.isDisplayAppliBTabMenu,
									this.isDisplayAppliCTabMenu,
									this.isDisplayAppliDTabMenu,
									this.isDisplayAppliETabMenu,
									this.isDisplayAppliFTabMenu,
									this.isDisplayAppliGTabMenu,
									this.isDisplayAppliHTabMenu,
									this.isDisplayAppliITabMenu,
									this.isDisplayAppliJTabMenu,
									this.isDisplayMemoTabMenu});
			this.displayFavoriteTabMenu.Name = "displayFavoriteTabMenu";
			this.displayFavoriteTabMenu.Size = new System.Drawing.Size(160, 22);
			this.displayFavoriteTabMenu.Text = "お気に入りタブ";
			// 
			// isDisplayCommunityIdTabMenu
			// 
			this.isDisplayCommunityIdTabMenu.Name = "isDisplayCommunityIdTabMenu";
			this.isDisplayCommunityIdTabMenu.Size = new System.Drawing.Size(196, 22);
			this.isDisplayCommunityIdTabMenu.Text = "コミュニティID";
			this.isDisplayCommunityIdTabMenu.Click += new System.EventHandler(this.IsAlartListDisplayTabMenuClick);
			// 
			// isDisplayUserIdTabMenu
			// 
			this.isDisplayUserIdTabMenu.Name = "isDisplayUserIdTabMenu";
			this.isDisplayUserIdTabMenu.Size = new System.Drawing.Size(196, 22);
			this.isDisplayUserIdTabMenu.Text = "ユーザーID";
			this.isDisplayUserIdTabMenu.Click += new System.EventHandler(this.IsAlartListDisplayTabMenuClick);
			// 
			// isDisplayCommunityNameTabMenu
			// 
			this.isDisplayCommunityNameTabMenu.Name = "isDisplayCommunityNameTabMenu";
			this.isDisplayCommunityNameTabMenu.Size = new System.Drawing.Size(196, 22);
			this.isDisplayCommunityNameTabMenu.Text = "コミュニティ名";
			this.isDisplayCommunityNameTabMenu.Click += new System.EventHandler(this.IsAlartListDisplayTabMenuClick);
			// 
			// isDisplayUserNameTabMenu
			// 
			this.isDisplayUserNameTabMenu.Name = "isDisplayUserNameTabMenu";
			this.isDisplayUserNameTabMenu.Size = new System.Drawing.Size(196, 22);
			this.isDisplayUserNameTabMenu.Text = "ユーザー名";
			this.isDisplayUserNameTabMenu.Click += new System.EventHandler(this.IsAlartListDisplayTabMenuClick);
			// 
			// isDisplayKeywordTabMenu
			// 
			this.isDisplayKeywordTabMenu.Name = "isDisplayKeywordTabMenu";
			this.isDisplayKeywordTabMenu.Size = new System.Drawing.Size(196, 22);
			this.isDisplayKeywordTabMenu.Text = "キーワード";
			this.isDisplayKeywordTabMenu.Click += new System.EventHandler(this.IsAlartListDisplayTabMenuClick);
			// 
			// isDisplayCommunityFollowTabMenu
			// 
			this.isDisplayCommunityFollowTabMenu.Name = "isDisplayCommunityFollowTabMenu";
			this.isDisplayCommunityFollowTabMenu.Size = new System.Drawing.Size(196, 22);
			this.isDisplayCommunityFollowTabMenu.Text = "コミュニティフォロー";
			this.isDisplayCommunityFollowTabMenu.Click += new System.EventHandler(this.IsAlartListDisplayTabMenuClick);
			// 
			// isDisplayUserFollowTabMenu
			// 
			this.isDisplayUserFollowTabMenu.Name = "isDisplayUserFollowTabMenu";
			this.isDisplayUserFollowTabMenu.Size = new System.Drawing.Size(196, 22);
			this.isDisplayUserFollowTabMenu.Text = "ユーザーフォロー";
			this.isDisplayUserFollowTabMenu.Click += new System.EventHandler(this.IsAlartListDisplayTabMenuClick);
			// 
			// isDisplayLastHosoDtTabMenu
			// 
			this.isDisplayLastHosoDtTabMenu.Name = "isDisplayLastHosoDtTabMenu";
			this.isDisplayLastHosoDtTabMenu.Size = new System.Drawing.Size(196, 22);
			this.isDisplayLastHosoDtTabMenu.Text = "最近の放送日時";
			this.isDisplayLastHosoDtTabMenu.Click += new System.EventHandler(this.IsAlartListDisplayTabMenuClick);
			// 
			// isDisplayAddDateDtTabMenu
			// 
			this.isDisplayAddDateDtTabMenu.Name = "isDisplayAddDateDtTabMenu";
			this.isDisplayAddDateDtTabMenu.Size = new System.Drawing.Size(196, 22);
			this.isDisplayAddDateDtTabMenu.Text = "登録日時";
			this.isDisplayAddDateDtTabMenu.Click += new System.EventHandler(this.IsAlartListDisplayTabMenuClick);
			// 
			// isDisplayPopupTabMenu
			// 
			this.isDisplayPopupTabMenu.Name = "isDisplayPopupTabMenu";
			this.isDisplayPopupTabMenu.Size = new System.Drawing.Size(196, 22);
			this.isDisplayPopupTabMenu.Text = "ポップアップ";
			this.isDisplayPopupTabMenu.Click += new System.EventHandler(this.IsAlartListDisplayTabMenuClick);
			// 
			// isDisplayBalloonTabMenu
			// 
			this.isDisplayBalloonTabMenu.Name = "isDisplayBalloonTabMenu";
			this.isDisplayBalloonTabMenu.Size = new System.Drawing.Size(196, 22);
			this.isDisplayBalloonTabMenu.Text = "バルーン";
			this.isDisplayBalloonTabMenu.Click += new System.EventHandler(this.IsAlartListDisplayTabMenuClick);
			// 
			// isDisplayWebTabMenu
			// 
			this.isDisplayWebTabMenu.Name = "isDisplayWebTabMenu";
			this.isDisplayWebTabMenu.Size = new System.Drawing.Size(196, 22);
			this.isDisplayWebTabMenu.Text = "Web";
			this.isDisplayWebTabMenu.Click += new System.EventHandler(this.IsAlartListDisplayTabMenuClick);
			// 
			// isDisplayMailTabMenu
			// 
			this.isDisplayMailTabMenu.Name = "isDisplayMailTabMenu";
			this.isDisplayMailTabMenu.Size = new System.Drawing.Size(196, 22);
			this.isDisplayMailTabMenu.Text = "メール";
			this.isDisplayMailTabMenu.Click += new System.EventHandler(this.IsAlartListDisplayTabMenuClick);
			// 
			// isDisplaySoundTabMenu
			// 
			this.isDisplaySoundTabMenu.Name = "isDisplaySoundTabMenu";
			this.isDisplaySoundTabMenu.Size = new System.Drawing.Size(196, 22);
			this.isDisplaySoundTabMenu.Text = "音";
			this.isDisplaySoundTabMenu.Click += new System.EventHandler(this.IsAlartListDisplayTabMenuClick);
			// 
			// isDisplayAppliATabMenu
			// 
			this.isDisplayAppliATabMenu.Name = "isDisplayAppliATabMenu";
			this.isDisplayAppliATabMenu.Size = new System.Drawing.Size(196, 22);
			this.isDisplayAppliATabMenu.Text = "アプリA";
			this.isDisplayAppliATabMenu.Click += new System.EventHandler(this.IsAlartListDisplayTabMenuClick);
			// 
			// isDisplayAppliBTabMenu
			// 
			this.isDisplayAppliBTabMenu.Name = "isDisplayAppliBTabMenu";
			this.isDisplayAppliBTabMenu.Size = new System.Drawing.Size(196, 22);
			this.isDisplayAppliBTabMenu.Text = "アプリB";
			this.isDisplayAppliBTabMenu.Click += new System.EventHandler(this.IsAlartListDisplayTabMenuClick);
			// 
			// isDisplayAppliCTabMenu
			// 
			this.isDisplayAppliCTabMenu.Name = "isDisplayAppliCTabMenu";
			this.isDisplayAppliCTabMenu.Size = new System.Drawing.Size(196, 22);
			this.isDisplayAppliCTabMenu.Text = "アプリC";
			this.isDisplayAppliCTabMenu.Click += new System.EventHandler(this.IsAlartListDisplayTabMenuClick);
			// 
			// isDisplayAppliDTabMenu
			// 
			this.isDisplayAppliDTabMenu.Name = "isDisplayAppliDTabMenu";
			this.isDisplayAppliDTabMenu.Size = new System.Drawing.Size(196, 22);
			this.isDisplayAppliDTabMenu.Text = "アプリD";
			this.isDisplayAppliDTabMenu.Click += new System.EventHandler(this.IsAlartListDisplayTabMenuClick);
			// 
			// isDisplayAppliETabMenu
			// 
			this.isDisplayAppliETabMenu.Name = "isDisplayAppliETabMenu";
			this.isDisplayAppliETabMenu.Size = new System.Drawing.Size(196, 22);
			this.isDisplayAppliETabMenu.Text = "アプリE";
			this.isDisplayAppliETabMenu.Click += new System.EventHandler(this.IsAlartListDisplayTabMenuClick);
			// 
			// isDisplayAppliFTabMenu
			// 
			this.isDisplayAppliFTabMenu.Name = "isDisplayAppliFTabMenu";
			this.isDisplayAppliFTabMenu.Size = new System.Drawing.Size(196, 22);
			this.isDisplayAppliFTabMenu.Text = "アプリF";
			this.isDisplayAppliFTabMenu.Click += new System.EventHandler(this.IsAlartListDisplayTabMenuClick);
			// 
			// isDisplayAppliGTabMenu
			// 
			this.isDisplayAppliGTabMenu.Name = "isDisplayAppliGTabMenu";
			this.isDisplayAppliGTabMenu.Size = new System.Drawing.Size(196, 22);
			this.isDisplayAppliGTabMenu.Text = "アプリG";
			this.isDisplayAppliGTabMenu.Click += new System.EventHandler(this.IsAlartListDisplayTabMenuClick);
			// 
			// isDisplayAppliHTabMenu
			// 
			this.isDisplayAppliHTabMenu.Name = "isDisplayAppliHTabMenu";
			this.isDisplayAppliHTabMenu.Size = new System.Drawing.Size(196, 22);
			this.isDisplayAppliHTabMenu.Text = "アプリH";
			this.isDisplayAppliHTabMenu.Click += new System.EventHandler(this.IsAlartListDisplayTabMenuClick);
			// 
			// isDisplayAppliITabMenu
			// 
			this.isDisplayAppliITabMenu.Name = "isDisplayAppliITabMenu";
			this.isDisplayAppliITabMenu.Size = new System.Drawing.Size(196, 22);
			this.isDisplayAppliITabMenu.Text = "アプリI";
			this.isDisplayAppliITabMenu.Click += new System.EventHandler(this.IsAlartListDisplayTabMenuClick);
			// 
			// isDisplayAppliJTabMenu
			// 
			this.isDisplayAppliJTabMenu.Name = "isDisplayAppliJTabMenu";
			this.isDisplayAppliJTabMenu.Size = new System.Drawing.Size(196, 22);
			this.isDisplayAppliJTabMenu.Text = "アプリJ";
			this.isDisplayAppliJTabMenu.Click += new System.EventHandler(this.IsAlartListDisplayTabMenuClick);
			// 
			// isDisplayMemoTabMenu
			// 
			this.isDisplayMemoTabMenu.Name = "isDisplayMemoTabMenu";
			this.isDisplayMemoTabMenu.Size = new System.Drawing.Size(196, 22);
			this.isDisplayMemoTabMenu.Text = "メモ";
			this.isDisplayMemoTabMenu.Click += new System.EventHandler(this.IsAlartListDisplayTabMenuClick);
			// 
			// displayTaskTabMenu
			// 
			this.displayTaskTabMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.isTaskListDisplayStartTimeTabMenu,
									this.isTaskListDisplayLvidTabMenu,
									this.isTaskListDisplayArgsTabMenu,
									this.isTaskListDisplayAddDtTabMenu,
									this.isTaskListDisplayStatusTabMenu,
									this.isTaskListDisplayPopupTabMenu,
									this.isTaskListDisplayBalloonTabMenu,
									this.isTaskListDisplayWebTabMenu,
									this.isTaskListDisplayMailTabMenu,
									this.isTaskListDisplaySoundTabMenu,
									this.isTaskListDisplayAppliATabMenu,
									this.isTaskListDisplayAppliBTabMenu,
									this.isTaskListDisplayAppliCTabMenu,
									this.isTaskListDisplayAppliDTabMenu,
									this.isTaskListDisplayAppliETabMenu,
									this.isTaskListDisplayAppliFTabMenu,
									this.isTaskListDisplayAppliGTabMenu,
									this.isTaskListDisplayAppliHTabMenu,
									this.isTaskListDisplayAppliITabMenu,
									this.isTaskListDisplayAppliJTabMenu,
									this.isTaskListDisplayDeleteTabMenu,
									this.isTaskListDisplayMemoTabMenu});
			this.displayTaskTabMenu.Name = "displayTaskTabMenu";
			this.displayTaskTabMenu.Size = new System.Drawing.Size(160, 22);
			this.displayTaskTabMenu.Text = "予約起動タブ";
			// 
			// isTaskListDisplayStartTimeTabMenu
			// 
			this.isTaskListDisplayStartTimeTabMenu.Name = "isTaskListDisplayStartTimeTabMenu";
			this.isTaskListDisplayStartTimeTabMenu.Size = new System.Drawing.Size(148, 22);
			this.isTaskListDisplayStartTimeTabMenu.Text = "起動時刻";
			this.isTaskListDisplayStartTimeTabMenu.Click += new System.EventHandler(this.IsTaskListDisplayTabMenuClick);
			// 
			// isTaskListDisplayLvidTabMenu
			// 
			this.isTaskListDisplayLvidTabMenu.Name = "isTaskListDisplayLvidTabMenu";
			this.isTaskListDisplayLvidTabMenu.Size = new System.Drawing.Size(148, 22);
			this.isTaskListDisplayLvidTabMenu.Text = "放送ID";
			this.isTaskListDisplayLvidTabMenu.Click += new System.EventHandler(this.IsTaskListDisplayTabMenuClick);
			// 
			// isTaskListDisplayArgsTabMenu
			// 
			this.isTaskListDisplayArgsTabMenu.Name = "isTaskListDisplayArgsTabMenu";
			this.isTaskListDisplayArgsTabMenu.Size = new System.Drawing.Size(148, 22);
			this.isTaskListDisplayArgsTabMenu.Text = "引数";
			this.isTaskListDisplayArgsTabMenu.Click += new System.EventHandler(this.IsTaskListDisplayTabMenuClick);
			// 
			// isTaskListDisplayAddDtTabMenu
			// 
			this.isTaskListDisplayAddDtTabMenu.Name = "isTaskListDisplayAddDtTabMenu";
			this.isTaskListDisplayAddDtTabMenu.Size = new System.Drawing.Size(148, 22);
			this.isTaskListDisplayAddDtTabMenu.Text = "登録日時";
			this.isTaskListDisplayAddDtTabMenu.Click += new System.EventHandler(this.IsTaskListDisplayTabMenuClick);
			// 
			// isTaskListDisplayStatusTabMenu
			// 
			this.isTaskListDisplayStatusTabMenu.Name = "isTaskListDisplayStatusTabMenu";
			this.isTaskListDisplayStatusTabMenu.Size = new System.Drawing.Size(148, 22);
			this.isTaskListDisplayStatusTabMenu.Text = "状態";
			this.isTaskListDisplayStatusTabMenu.Click += new System.EventHandler(this.IsTaskListDisplayTabMenuClick);
			// 
			// isTaskListDisplayPopupTabMenu
			// 
			this.isTaskListDisplayPopupTabMenu.Name = "isTaskListDisplayPopupTabMenu";
			this.isTaskListDisplayPopupTabMenu.Size = new System.Drawing.Size(148, 22);
			this.isTaskListDisplayPopupTabMenu.Text = "ポップアップ";
			this.isTaskListDisplayPopupTabMenu.Click += new System.EventHandler(this.IsTaskListDisplayTabMenuClick);
			// 
			// isTaskListDisplayBalloonTabMenu
			// 
			this.isTaskListDisplayBalloonTabMenu.Name = "isTaskListDisplayBalloonTabMenu";
			this.isTaskListDisplayBalloonTabMenu.Size = new System.Drawing.Size(148, 22);
			this.isTaskListDisplayBalloonTabMenu.Text = "バルーン";
			this.isTaskListDisplayBalloonTabMenu.Click += new System.EventHandler(this.IsTaskListDisplayTabMenuClick);
			// 
			// isTaskListDisplayWebTabMenu
			// 
			this.isTaskListDisplayWebTabMenu.Name = "isTaskListDisplayWebTabMenu";
			this.isTaskListDisplayWebTabMenu.Size = new System.Drawing.Size(148, 22);
			this.isTaskListDisplayWebTabMenu.Text = "Web";
			this.isTaskListDisplayWebTabMenu.Click += new System.EventHandler(this.IsTaskListDisplayTabMenuClick);
			// 
			// isTaskListDisplayMailTabMenu
			// 
			this.isTaskListDisplayMailTabMenu.Name = "isTaskListDisplayMailTabMenu";
			this.isTaskListDisplayMailTabMenu.Size = new System.Drawing.Size(148, 22);
			this.isTaskListDisplayMailTabMenu.Text = "メール";
			this.isTaskListDisplayMailTabMenu.Click += new System.EventHandler(this.IsTaskListDisplayTabMenuClick);
			// 
			// isTaskListDisplaySoundTabMenu
			// 
			this.isTaskListDisplaySoundTabMenu.Name = "isTaskListDisplaySoundTabMenu";
			this.isTaskListDisplaySoundTabMenu.Size = new System.Drawing.Size(148, 22);
			this.isTaskListDisplaySoundTabMenu.Text = "音";
			this.isTaskListDisplaySoundTabMenu.Click += new System.EventHandler(this.IsTaskListDisplayTabMenuClick);
			// 
			// isTaskListDisplayAppliATabMenu
			// 
			this.isTaskListDisplayAppliATabMenu.Name = "isTaskListDisplayAppliATabMenu";
			this.isTaskListDisplayAppliATabMenu.Size = new System.Drawing.Size(148, 22);
			this.isTaskListDisplayAppliATabMenu.Text = "アプリA";
			this.isTaskListDisplayAppliATabMenu.Click += new System.EventHandler(this.IsTaskListDisplayTabMenuClick);
			// 
			// isTaskListDisplayAppliBTabMenu
			// 
			this.isTaskListDisplayAppliBTabMenu.Name = "isTaskListDisplayAppliBTabMenu";
			this.isTaskListDisplayAppliBTabMenu.Size = new System.Drawing.Size(148, 22);
			this.isTaskListDisplayAppliBTabMenu.Text = "アプリB";
			this.isTaskListDisplayAppliBTabMenu.Click += new System.EventHandler(this.IsTaskListDisplayTabMenuClick);
			// 
			// isTaskListDisplayAppliCTabMenu
			// 
			this.isTaskListDisplayAppliCTabMenu.Name = "isTaskListDisplayAppliCTabMenu";
			this.isTaskListDisplayAppliCTabMenu.Size = new System.Drawing.Size(148, 22);
			this.isTaskListDisplayAppliCTabMenu.Text = "アプリC";
			this.isTaskListDisplayAppliCTabMenu.Click += new System.EventHandler(this.IsTaskListDisplayTabMenuClick);
			// 
			// isTaskListDisplayAppliDTabMenu
			// 
			this.isTaskListDisplayAppliDTabMenu.Name = "isTaskListDisplayAppliDTabMenu";
			this.isTaskListDisplayAppliDTabMenu.Size = new System.Drawing.Size(148, 22);
			this.isTaskListDisplayAppliDTabMenu.Text = "アプリD";
			this.isTaskListDisplayAppliDTabMenu.Click += new System.EventHandler(this.IsTaskListDisplayTabMenuClick);
			// 
			// isTaskListDisplayAppliETabMenu
			// 
			this.isTaskListDisplayAppliETabMenu.Name = "isTaskListDisplayAppliETabMenu";
			this.isTaskListDisplayAppliETabMenu.Size = new System.Drawing.Size(148, 22);
			this.isTaskListDisplayAppliETabMenu.Text = "アプリE";
			this.isTaskListDisplayAppliETabMenu.Click += new System.EventHandler(this.IsTaskListDisplayTabMenuClick);
			// 
			// isTaskListDisplayAppliFTabMenu
			// 
			this.isTaskListDisplayAppliFTabMenu.Name = "isTaskListDisplayAppliFTabMenu";
			this.isTaskListDisplayAppliFTabMenu.Size = new System.Drawing.Size(148, 22);
			this.isTaskListDisplayAppliFTabMenu.Text = "アプリF";
			this.isTaskListDisplayAppliFTabMenu.Click += new System.EventHandler(this.IsTaskListDisplayTabMenuClick);
			// 
			// isTaskListDisplayAppliGTabMenu
			// 
			this.isTaskListDisplayAppliGTabMenu.Name = "isTaskListDisplayAppliGTabMenu";
			this.isTaskListDisplayAppliGTabMenu.Size = new System.Drawing.Size(148, 22);
			this.isTaskListDisplayAppliGTabMenu.Text = "アプリG";
			this.isTaskListDisplayAppliGTabMenu.Click += new System.EventHandler(this.IsTaskListDisplayTabMenuClick);
			// 
			// isTaskListDisplayAppliHTabMenu
			// 
			this.isTaskListDisplayAppliHTabMenu.Name = "isTaskListDisplayAppliHTabMenu";
			this.isTaskListDisplayAppliHTabMenu.Size = new System.Drawing.Size(148, 22);
			this.isTaskListDisplayAppliHTabMenu.Text = "アプリH";
			this.isTaskListDisplayAppliHTabMenu.Click += new System.EventHandler(this.IsTaskListDisplayTabMenuClick);
			// 
			// isTaskListDisplayAppliITabMenu
			// 
			this.isTaskListDisplayAppliITabMenu.Name = "isTaskListDisplayAppliITabMenu";
			this.isTaskListDisplayAppliITabMenu.Size = new System.Drawing.Size(148, 22);
			this.isTaskListDisplayAppliITabMenu.Text = "アプリI";
			this.isTaskListDisplayAppliITabMenu.Click += new System.EventHandler(this.IsTaskListDisplayTabMenuClick);
			// 
			// isTaskListDisplayAppliJTabMenu
			// 
			this.isTaskListDisplayAppliJTabMenu.Name = "isTaskListDisplayAppliJTabMenu";
			this.isTaskListDisplayAppliJTabMenu.Size = new System.Drawing.Size(148, 22);
			this.isTaskListDisplayAppliJTabMenu.Text = "アプリJ";
			this.isTaskListDisplayAppliJTabMenu.Click += new System.EventHandler(this.IsTaskListDisplayTabMenuClick);
			// 
			// isTaskListDisplayDeleteTabMenu
			// 
			this.isTaskListDisplayDeleteTabMenu.Name = "isTaskListDisplayDeleteTabMenu";
			this.isTaskListDisplayDeleteTabMenu.Size = new System.Drawing.Size(148, 22);
			this.isTaskListDisplayDeleteTabMenu.Text = "削除";
			this.isTaskListDisplayDeleteTabMenu.Click += new System.EventHandler(this.IsTaskListDisplayTabMenuClick);
			// 
			// isTaskListDisplayMemoTabMenu
			// 
			this.isTaskListDisplayMemoTabMenu.Name = "isTaskListDisplayMemoTabMenu";
			this.isTaskListDisplayMemoTabMenu.Size = new System.Drawing.Size(148, 22);
			this.isTaskListDisplayMemoTabMenu.Text = "メモ";
			this.isTaskListDisplayMemoTabMenu.Click += new System.EventHandler(this.IsTaskListDisplayTabMenuClick);
			// 
			// toolMenuItem
			// 
			this.toolMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.bulkUserFollowMenu,
									this.bulkCommunityFollowMenu,
									this.bulkChannelFollowMenu,
									this.toolStripSeparator9,
									this.optionMenuItem});
			this.toolMenuItem.Name = "toolMenuItem";
			this.toolMenuItem.ShowShortcutKeys = false;
			this.toolMenuItem.Size = new System.Drawing.Size(74, 22);
			this.toolMenuItem.Text = "ツール(&T)";
			// 
			// bulkUserFollowMenu
			// 
			this.bulkUserFollowMenu.Name = "bulkUserFollowMenu";
			this.bulkUserFollowMenu.Size = new System.Drawing.Size(292, 22);
			this.bulkUserFollowMenu.Text = "お気に入りユーザーを一括フォロー";
			this.bulkUserFollowMenu.Click += new System.EventHandler(this.BulkUserFollowMenuClick);
			// 
			// bulkCommunityFollowMenu
			// 
			this.bulkCommunityFollowMenu.Name = "bulkCommunityFollowMenu";
			this.bulkCommunityFollowMenu.Size = new System.Drawing.Size(292, 22);
			this.bulkCommunityFollowMenu.Text = "お気に入りコミュニティを一括フォロー";
			this.bulkCommunityFollowMenu.Click += new System.EventHandler(this.BulkCommunityFollowMenuClick);
			// 
			// bulkChannelFollowMenu
			// 
			this.bulkChannelFollowMenu.Name = "bulkChannelFollowMenu";
			this.bulkChannelFollowMenu.Size = new System.Drawing.Size(292, 22);
			this.bulkChannelFollowMenu.Text = "お気に入りチャンネルを一括フォロー";
			this.bulkChannelFollowMenu.Click += new System.EventHandler(this.BulkChannelFollowMenuClick);
			// 
			// toolStripSeparator9
			// 
			this.toolStripSeparator9.Name = "toolStripSeparator9";
			this.toolStripSeparator9.Size = new System.Drawing.Size(289, 6);
			// 
			// optionMenuItem
			// 
			this.optionMenuItem.Name = "optionMenuItem";
			this.optionMenuItem.ShowShortcutKeys = false;
			this.optionMenuItem.Size = new System.Drawing.Size(292, 22);
			this.optionMenuItem.Text = "オプション(&O)";
			this.optionMenuItem.Click += new System.EventHandler(this.optionItem_Select);
			// 
			// helpMenuItem
			// 
			this.helpMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.バージョン情報VToolStripMenuItem});
			this.helpMenuItem.Name = "helpMenuItem";
			this.helpMenuItem.ShowShortcutKeys = false;
			this.helpMenuItem.Size = new System.Drawing.Size(75, 22);
			this.helpMenuItem.Text = "ヘルプ(&H)";
			// 
			// バージョン情報VToolStripMenuItem
			// 
			this.バージョン情報VToolStripMenuItem.Name = "バージョン情報VToolStripMenuItem";
			this.バージョン情報VToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
			this.バージョン情報VToolStripMenuItem.Text = "バージョン情報(&A)";
			this.バージョン情報VToolStripMenuItem.Click += new System.EventHandler(this.versionMenu_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.lastHosoStatusBar});
			this.statusStrip1.Location = new System.Drawing.Point(0, 340);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(899, 22);
			this.statusStrip1.TabIndex = 21;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// lastHosoStatusBar
			// 
			this.lastHosoStatusBar.BackColor = System.Drawing.SystemColors.Control;
			this.lastHosoStatusBar.Name = "lastHosoStatusBar";
			this.lastHosoStatusBar.Size = new System.Drawing.Size(0, 17);
			// 
			// addBtn
			// 
			this.addBtn.Location = new System.Drawing.Point(6, 18);
			this.addBtn.Name = "addBtn";
			this.addBtn.Size = new System.Drawing.Size(75, 23);
			this.addBtn.TabIndex = 22;
			this.addBtn.Text = "新規登録";
			this.addBtn.UseVisualStyleBackColor = true;
			this.addBtn.Click += new System.EventHandler(this.AddBtnClick);
			// 
			// alartList
			// 
			this.alartList.AllowDrop = true;
			this.alartList.AllowUserToAddRows = false;
			this.alartList.AllowUserToDeleteRows = false;
			this.alartList.AllowUserToResizeRows = false;
			this.alartList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.alartList.ColumnHeadersHeight = 25;
			this.alartList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.alartList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
									this.ｺﾐｭﾆﾃｨID,
									this.ﾕｰｻﾞｰID,
									this.ｺﾐｭﾆﾃｨ名,
									this.ﾕｰｻﾞｰ名,
									this.ｷｰﾜｰﾄﾞ,
									this.ｺﾐｭﾆﾃｨﾌｫﾛｰ,
									this.ﾕｰｻﾞｰﾌｫﾛｰ,
									this.最近の放送日時,
									this.登録日時,
									this.ﾎﾟｯﾌﾟｱｯﾌﾟ,
									this.ﾊﾞﾙｰﾝ,
									this.Web,
									this.ﾒｰﾙ,
									this.音,
									this.アプリA,
									this.アプリB,
									this.アプリC,
									this.ｱﾌﾟﾘC,
									this.ｱﾌﾟﾘE,
									this.ｱﾌﾟﾘF,
									this.ｱﾌﾟﾘG,
									this.ｱﾌﾟﾘH,
									this.ｱﾌﾟﾘI,
									this.ｱﾌﾟﾘJ,
									this.comment});
			this.alartList.ContextMenuStrip = this.contextMenuStrip1;
			this.alartList.Location = new System.Drawing.Point(6, 60);
			this.alartList.MultiSelect = false;
			this.alartList.Name = "alartList";
			this.alartList.RowHeadersVisible = false;
			this.alartList.RowTemplate.Height = 21;
			this.alartList.Size = new System.Drawing.Size(865, 216);
			this.alartList.TabIndex = 23;
			this.alartList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.AlartListCellClick);
			this.alartList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.AlartListCellFormatting);
			this.alartList.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.AlartListCellMouseDoubleClick);
			this.alartList.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.AlartListCellMouseDown);
			this.alartList.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.AlartListCellParsing);
			this.alartList.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.AlartListColumnHeaderMouseClick);
			this.alartList.CurrentCellDirtyStateChanged += new System.EventHandler(this.AlartListCurrentCellDirtyStateChanged);
			this.alartList.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.AlartListRowsAdded);
			this.alartList.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.AlartListRowsRemoved);
			this.alartList.DragDrop += new System.Windows.Forms.DragEventHandler(this.AlartListDragDrop);
			this.alartList.DragEnter += new System.Windows.Forms.DragEventHandler(this.listDragEnter);
			// 
			// ｺﾐｭﾆﾃｨID
			// 
			this.ｺﾐｭﾆﾃｨID.DataPropertyName = "communityId";
			this.ｺﾐｭﾆﾃｨID.FillWeight = 2F;
			this.ｺﾐｭﾆﾃｨID.HeaderText = "ｺﾐｭﾆﾃｨID";
			this.ｺﾐｭﾆﾃｨID.MinimumWidth = 2;
			this.ｺﾐｭﾆﾃｨID.Name = "ｺﾐｭﾆﾃｨID";
			this.ｺﾐｭﾆﾃｨID.Width = 77;
			// 
			// ﾕｰｻﾞｰID
			// 
			this.ﾕｰｻﾞｰID.DataPropertyName = "hostId";
			this.ﾕｰｻﾞｰID.HeaderText = "ﾕｰｻﾞｰID";
			this.ﾕｰｻﾞｰID.Name = "ﾕｰｻﾞｰID";
			// 
			// ｺﾐｭﾆﾃｨ名
			// 
			this.ｺﾐｭﾆﾃｨ名.DataPropertyName = "communityName";
			this.ｺﾐｭﾆﾃｨ名.HeaderText = "ｺﾐｭﾆﾃｨ名";
			this.ｺﾐｭﾆﾃｨ名.Name = "ｺﾐｭﾆﾃｨ名";
			// 
			// ﾕｰｻﾞｰ名
			// 
			this.ﾕｰｻﾞｰ名.DataPropertyName = "hostName";
			this.ﾕｰｻﾞｰ名.HeaderText = "ﾕｰｻﾞｰ名";
			this.ﾕｰｻﾞｰ名.Name = "ﾕｰｻﾞｰ名";
			// 
			// ｷｰﾜｰﾄﾞ
			// 
			this.ｷｰﾜｰﾄﾞ.DataPropertyName = "keyword";
			this.ｷｰﾜｰﾄﾞ.HeaderText = "ｷｰﾜｰﾄﾞ";
			this.ｷｰﾜｰﾄﾞ.Name = "ｷｰﾜｰﾄﾞ";
			// 
			// ｺﾐｭﾆﾃｨﾌｫﾛｰ
			// 
			this.ｺﾐｭﾆﾃｨﾌｫﾛｰ.DataPropertyName = "communityFollow";
			this.ｺﾐｭﾆﾃｨﾌｫﾛｰ.HeaderText = "ｺﾐｭﾆﾃｨﾌｫﾛｰ";
			this.ｺﾐｭﾆﾃｨﾌｫﾛｰ.Name = "ｺﾐｭﾆﾃｨﾌｫﾛｰ";
			// 
			// ﾕｰｻﾞｰﾌｫﾛｰ
			// 
			this.ﾕｰｻﾞｰﾌｫﾛｰ.DataPropertyName = "hostFollow";
			this.ﾕｰｻﾞｰﾌｫﾛｰ.HeaderText = "ﾕｰｻﾞｰﾌｫﾛｰ";
			this.ﾕｰｻﾞｰﾌｫﾛｰ.Name = "ﾕｰｻﾞｰﾌｫﾛｰ";
			// 
			// 最近の放送日時
			// 
			this.最近の放送日時.DataPropertyName = "lastHostDate";
			this.最近の放送日時.HeaderText = "最近の放送日時";
			this.最近の放送日時.MinimumWidth = 112;
			this.最近の放送日時.Name = "最近の放送日時";
			this.最近の放送日時.ReadOnly = true;
			this.最近の放送日時.Width = 112;
			// 
			// 登録日時
			// 
			this.登録日時.DataPropertyName = "addDate";
			this.登録日時.HeaderText = "登録日時";
			this.登録日時.MinimumWidth = 112;
			this.登録日時.Name = "登録日時";
			this.登録日時.ReadOnly = true;
			this.登録日時.Width = 112;
			// 
			// ﾎﾟｯﾌﾟｱｯﾌﾟ
			// 
			this.ﾎﾟｯﾌﾟｱｯﾌﾟ.DataPropertyName = "popup";
			this.ﾎﾟｯﾌﾟｱｯﾌﾟ.HeaderText = "ﾎﾟｯﾌﾟｱｯﾌﾟ";
			this.ﾎﾟｯﾌﾟｱｯﾌﾟ.MinimumWidth = 45;
			this.ﾎﾟｯﾌﾟｱｯﾌﾟ.Name = "ﾎﾟｯﾌﾟｱｯﾌﾟ";
			this.ﾎﾟｯﾌﾟｱｯﾌﾟ.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.ﾎﾟｯﾌﾟｱｯﾌﾟ.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.ﾎﾟｯﾌﾟｱｯﾌﾟ.Width = 45;
			// 
			// ﾊﾞﾙｰﾝ
			// 
			this.ﾊﾞﾙｰﾝ.DataPropertyName = "baloon";
			this.ﾊﾞﾙｰﾝ.HeaderText = "ﾊﾞﾙｰﾝ";
			this.ﾊﾞﾙｰﾝ.MinimumWidth = 45;
			this.ﾊﾞﾙｰﾝ.Name = "ﾊﾞﾙｰﾝ";
			this.ﾊﾞﾙｰﾝ.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.ﾊﾞﾙｰﾝ.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.ﾊﾞﾙｰﾝ.Width = 45;
			// 
			// Web
			// 
			this.Web.DataPropertyName = "browser";
			this.Web.HeaderText = "Web";
			this.Web.MinimumWidth = 45;
			this.Web.Name = "Web";
			this.Web.Width = 45;
			// 
			// ﾒｰﾙ
			// 
			this.ﾒｰﾙ.DataPropertyName = "mail";
			this.ﾒｰﾙ.HeaderText = "ﾒｰﾙ";
			this.ﾒｰﾙ.MinimumWidth = 45;
			this.ﾒｰﾙ.Name = "ﾒｰﾙ";
			this.ﾒｰﾙ.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.ﾒｰﾙ.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.ﾒｰﾙ.Width = 45;
			// 
			// 音
			// 
			this.音.DataPropertyName = "sound";
			this.音.HeaderText = "音";
			this.音.MinimumWidth = 45;
			this.音.Name = "音";
			this.音.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.音.Width = 45;
			// 
			// アプリA
			// 
			this.アプリA.DataPropertyName = "appliA";
			this.アプリA.HeaderText = "ｱﾌﾟﾘA";
			this.アプリA.MinimumWidth = 45;
			this.アプリA.Name = "アプリA";
			this.アプリA.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.アプリA.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.アプリA.Width = 45;
			// 
			// アプリB
			// 
			this.アプリB.DataPropertyName = "appliB";
			this.アプリB.HeaderText = "ｱﾌﾟﾘB";
			this.アプリB.MinimumWidth = 45;
			this.アプリB.Name = "アプリB";
			this.アプリB.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.アプリB.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.アプリB.Width = 45;
			// 
			// アプリC
			// 
			this.アプリC.DataPropertyName = "appliC";
			this.アプリC.HeaderText = "ｱﾌﾟﾘC";
			this.アプリC.MinimumWidth = 45;
			this.アプリC.Name = "アプリC";
			this.アプリC.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.アプリC.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.アプリC.Width = 45;
			// 
			// ｱﾌﾟﾘC
			// 
			this.ｱﾌﾟﾘC.DataPropertyName = "appliD";
			this.ｱﾌﾟﾘC.HeaderText = "ｱﾌﾟﾘD";
			this.ｱﾌﾟﾘC.MinimumWidth = 45;
			this.ｱﾌﾟﾘC.Name = "ｱﾌﾟﾘC";
			this.ｱﾌﾟﾘC.Width = 45;
			// 
			// ｱﾌﾟﾘE
			// 
			this.ｱﾌﾟﾘE.DataPropertyName = "appliE";
			this.ｱﾌﾟﾘE.HeaderText = "ｱﾌﾟﾘE";
			this.ｱﾌﾟﾘE.MinimumWidth = 45;
			this.ｱﾌﾟﾘE.Name = "ｱﾌﾟﾘE";
			this.ｱﾌﾟﾘE.Width = 45;
			// 
			// ｱﾌﾟﾘF
			// 
			this.ｱﾌﾟﾘF.DataPropertyName = "appliF";
			this.ｱﾌﾟﾘF.HeaderText = "ｱﾌﾟﾘF";
			this.ｱﾌﾟﾘF.MinimumWidth = 45;
			this.ｱﾌﾟﾘF.Name = "ｱﾌﾟﾘF";
			this.ｱﾌﾟﾘF.Width = 45;
			// 
			// ｱﾌﾟﾘG
			// 
			this.ｱﾌﾟﾘG.DataPropertyName = "appliG";
			this.ｱﾌﾟﾘG.HeaderText = "ｱﾌﾟﾘG";
			this.ｱﾌﾟﾘG.MinimumWidth = 45;
			this.ｱﾌﾟﾘG.Name = "ｱﾌﾟﾘG";
			this.ｱﾌﾟﾘG.Width = 45;
			// 
			// ｱﾌﾟﾘH
			// 
			this.ｱﾌﾟﾘH.DataPropertyName = "appliH";
			this.ｱﾌﾟﾘH.HeaderText = "ｱﾌﾟﾘH";
			this.ｱﾌﾟﾘH.MinimumWidth = 45;
			this.ｱﾌﾟﾘH.Name = "ｱﾌﾟﾘH";
			this.ｱﾌﾟﾘH.Width = 45;
			// 
			// ｱﾌﾟﾘI
			// 
			this.ｱﾌﾟﾘI.DataPropertyName = "appliI";
			this.ｱﾌﾟﾘI.HeaderText = "ｱﾌﾟﾘI";
			this.ｱﾌﾟﾘI.MinimumWidth = 45;
			this.ｱﾌﾟﾘI.Name = "ｱﾌﾟﾘI";
			this.ｱﾌﾟﾘI.Width = 45;
			// 
			// ｱﾌﾟﾘJ
			// 
			this.ｱﾌﾟﾘJ.DataPropertyName = "appliJ";
			this.ｱﾌﾟﾘJ.HeaderText = "ｱﾌﾟﾘJ";
			this.ｱﾌﾟﾘJ.MinimumWidth = 45;
			this.ｱﾌﾟﾘJ.Name = "ｱﾌﾟﾘJ";
			this.ｱﾌﾟﾘJ.Width = 45;
			// 
			// comment
			// 
			this.comment.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.comment.DataPropertyName = "memo";
			this.comment.HeaderText = "ﾒﾓ";
			this.comment.MinimumWidth = 30;
			this.comment.Name = "comment";
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.openLastHosoMenu,
									this.openCommunityUrlMenu,
									this.openUserUrlMenu,
									this.toolStripSeparator2,
									this.copyLastHosoMenu,
									this.copyCommunityUrlMenu,
									this.copyUserUrlMenu,
									this.toolStripSeparator3,
									this.removeLineMenu,
									this.toolStripSeparator8,
									this.openAppliAMenu,
									this.openAppliBMenu,
									this.openAppliCMenu,
									this.openAppliDMenu,
									this.openAppliEMenu,
									this.openAppliFMenu,
									this.openAppliGMenu,
									this.openAppliHMenu,
									this.openAppliIMenu,
									this.openAppliJMenu});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(302, 396);
			// 
			// openLastHosoMenu
			// 
			this.openLastHosoMenu.Name = "openLastHosoMenu";
			this.openLastHosoMenu.Size = new System.Drawing.Size(301, 22);
			this.openLastHosoMenu.Text = "最近行われた放送のURLを開く";
			this.openLastHosoMenu.Click += new System.EventHandler(this.OpenLastHosoClick);
			// 
			// openCommunityUrlMenu
			// 
			this.openCommunityUrlMenu.Name = "openCommunityUrlMenu";
			this.openCommunityUrlMenu.Size = new System.Drawing.Size(301, 22);
			this.openCommunityUrlMenu.Text = "コミュニティURLを開く";
			this.openCommunityUrlMenu.Click += new System.EventHandler(this.OpenCommunityUrlClick);
			// 
			// openUserUrlMenu
			// 
			this.openUserUrlMenu.Name = "openUserUrlMenu";
			this.openUserUrlMenu.Size = new System.Drawing.Size(301, 22);
			this.openUserUrlMenu.Text = "ユーザーURLを開く";
			this.openUserUrlMenu.Click += new System.EventHandler(this.OpenUserUrlClick);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(298, 6);
			// 
			// copyLastHosoMenu
			// 
			this.copyLastHosoMenu.Name = "copyLastHosoMenu";
			this.copyLastHosoMenu.Size = new System.Drawing.Size(301, 22);
			this.copyLastHosoMenu.Text = "最近行われた放送のURLをコピー";
			this.copyLastHosoMenu.Click += new System.EventHandler(this.CopyLastHosoMenuClick);
			// 
			// copyCommunityUrlMenu
			// 
			this.copyCommunityUrlMenu.Name = "copyCommunityUrlMenu";
			this.copyCommunityUrlMenu.Size = new System.Drawing.Size(301, 22);
			this.copyCommunityUrlMenu.Text = "コミュニティURLをコピー";
			this.copyCommunityUrlMenu.Click += new System.EventHandler(this.CopyCommunityUrlMenuClick);
			// 
			// copyUserUrlMenu
			// 
			this.copyUserUrlMenu.Name = "copyUserUrlMenu";
			this.copyUserUrlMenu.Size = new System.Drawing.Size(301, 22);
			this.copyUserUrlMenu.Text = "ユーザーURLをコピー";
			this.copyUserUrlMenu.Click += new System.EventHandler(this.CopyUserUrlMenuClick);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(298, 6);
			// 
			// removeLineMenu
			// 
			this.removeLineMenu.Name = "removeLineMenu";
			this.removeLineMenu.Size = new System.Drawing.Size(301, 22);
			this.removeLineMenu.Text = "この行を削除";
			this.removeLineMenu.Click += new System.EventHandler(this.RemoveLineMenuClick);
			// 
			// toolStripSeparator8
			// 
			this.toolStripSeparator8.Name = "toolStripSeparator8";
			this.toolStripSeparator8.Size = new System.Drawing.Size(298, 6);
			// 
			// openAppliAMenu
			// 
			this.openAppliAMenu.Name = "openAppliAMenu";
			this.openAppliAMenu.Size = new System.Drawing.Size(301, 22);
			this.openAppliAMenu.Text = "最近行われた放送のURLをアプリAで開く";
			this.openAppliAMenu.Click += new System.EventHandler(this.recentLiveAppliOpenMenu_Click);
			// 
			// openAppliBMenu
			// 
			this.openAppliBMenu.Name = "openAppliBMenu";
			this.openAppliBMenu.Size = new System.Drawing.Size(301, 22);
			this.openAppliBMenu.Text = "最近行われた放送のURLをアプリBで開く";
			this.openAppliBMenu.Click += new System.EventHandler(this.recentLiveAppliOpenMenu_Click);
			// 
			// openAppliCMenu
			// 
			this.openAppliCMenu.Name = "openAppliCMenu";
			this.openAppliCMenu.Size = new System.Drawing.Size(301, 22);
			this.openAppliCMenu.Text = "最近行われた放送のURLをアプリCで開く";
			this.openAppliCMenu.Click += new System.EventHandler(this.recentLiveAppliOpenMenu_Click);
			// 
			// openAppliDMenu
			// 
			this.openAppliDMenu.Name = "openAppliDMenu";
			this.openAppliDMenu.Size = new System.Drawing.Size(301, 22);
			this.openAppliDMenu.Text = "最近行われた放送のURLをアプリDで開く";
			this.openAppliDMenu.Click += new System.EventHandler(this.recentLiveAppliOpenMenu_Click);
			// 
			// openAppliEMenu
			// 
			this.openAppliEMenu.Name = "openAppliEMenu";
			this.openAppliEMenu.Size = new System.Drawing.Size(301, 22);
			this.openAppliEMenu.Text = "最近行われた放送のURLをアプリEで開く";
			this.openAppliEMenu.Click += new System.EventHandler(this.recentLiveAppliOpenMenu_Click);
			// 
			// openAppliFMenu
			// 
			this.openAppliFMenu.Name = "openAppliFMenu";
			this.openAppliFMenu.Size = new System.Drawing.Size(301, 22);
			this.openAppliFMenu.Text = "最近行われた放送のURLをアプリFで開く";
			this.openAppliFMenu.Click += new System.EventHandler(this.recentLiveAppliOpenMenu_Click);
			// 
			// openAppliGMenu
			// 
			this.openAppliGMenu.Name = "openAppliGMenu";
			this.openAppliGMenu.Size = new System.Drawing.Size(301, 22);
			this.openAppliGMenu.Text = "最近行われた放送のURLをアプリGで開く";
			this.openAppliGMenu.Click += new System.EventHandler(this.recentLiveAppliOpenMenu_Click);
			// 
			// openAppliHMenu
			// 
			this.openAppliHMenu.Name = "openAppliHMenu";
			this.openAppliHMenu.Size = new System.Drawing.Size(301, 22);
			this.openAppliHMenu.Text = "最近行われた放送のURLをアプリHで開く";
			this.openAppliHMenu.Click += new System.EventHandler(this.recentLiveAppliOpenMenu_Click);
			// 
			// openAppliIMenu
			// 
			this.openAppliIMenu.Name = "openAppliIMenu";
			this.openAppliIMenu.Size = new System.Drawing.Size(301, 22);
			this.openAppliIMenu.Text = "最近行われた放送のURLをアプリIで開く";
			this.openAppliIMenu.Click += new System.EventHandler(this.recentLiveAppliOpenMenu_Click);
			// 
			// openAppliJMenu
			// 
			this.openAppliJMenu.Name = "openAppliJMenu";
			this.openAppliJMenu.Size = new System.Drawing.Size(301, 22);
			this.openAppliJMenu.Text = "最近行われた放送のURLをアプリJで開く";
			this.openAppliJMenu.Click += new System.EventHandler(this.recentLiveAppliOpenMenu_Click);
			// 
			// notifyIcon
			// 
			this.notifyIcon.BalloonTipText = "https://www.youtube.com/watch?v=kzEDjatOkM0";
			this.notifyIcon.BalloonTipTitle = "title";
			this.notifyIcon.ContextMenuStrip = this.notifyIconMenuStrip;
			this.notifyIcon.Text = "ニコ生放送チェックツール（仮";
			this.notifyIcon.Visible = true;
			this.notifyIcon.BalloonTipClicked += new System.EventHandler(this.NotifyIconBalloonTipClicked);
			this.notifyIcon.DoubleClick += new System.EventHandler(this.NotifyIconDoubleClick);
			// 
			// notifyIconMenuStrip
			// 
			this.notifyIconMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.openNotifyIconMenu,
									this.toolStripSeparator4,
									this.closeNotifyIconMenu});
			this.notifyIconMenuStrip.Name = "notifyIconMenuStrip";
			this.notifyIconMenuStrip.Size = new System.Drawing.Size(101, 54);
			// 
			// openNotifyIconMenu
			// 
			this.openNotifyIconMenu.Name = "openNotifyIconMenu";
			this.openNotifyIconMenu.Size = new System.Drawing.Size(100, 22);
			this.openNotifyIconMenu.Text = "開く";
			this.openNotifyIconMenu.Click += new System.EventHandler(this.OpenNotifyIconMenuClick);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(97, 6);
			// 
			// closeNotifyIconMenu
			// 
			this.closeNotifyIconMenu.Name = "closeNotifyIconMenu";
			this.closeNotifyIconMenu.Size = new System.Drawing.Size(100, 22);
			this.closeNotifyIconMenu.Text = "終了";
			this.closeNotifyIconMenu.Click += new System.EventHandler(this.CloseNotifyIconMenuClick);
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Location = new System.Drawing.Point(12, 29);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(887, 308);
			this.tabControl1.TabIndex = 24;
			// 
			// tabPage1
			// 
			this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
			this.tabPage1.Controls.Add(this.favoriteNumLabel);
			this.tabPage1.Controls.Add(this.searchBtn);
			this.tabPage1.Controls.Add(this.searchText);
			this.tabPage1.Controls.Add(this.downButton);
			this.tabPage1.Controls.Add(this.upButton);
			this.tabPage1.Controls.Add(this.alartList);
			this.tabPage1.Controls.Add(this.addBtn);
			this.tabPage1.Controls.Add(this.logText);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(879, 282);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "お気に入り設定";
			// 
			// favoriteNumLabel
			// 
			this.favoriteNumLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.favoriteNumLabel.Location = new System.Drawing.Point(742, 23);
			this.favoriteNumLabel.Name = "favoriteNumLabel";
			this.favoriteNumLabel.Size = new System.Drawing.Size(106, 23);
			this.favoriteNumLabel.TabIndex = 28;
			this.favoriteNumLabel.Text = "登録数：件";
			this.favoriteNumLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.favoriteNumLabel.Visible = false;
			// 
			// searchBtn
			// 
			this.searchBtn.Location = new System.Drawing.Point(309, 18);
			this.searchBtn.Name = "searchBtn";
			this.searchBtn.Size = new System.Drawing.Size(75, 23);
			this.searchBtn.TabIndex = 27;
			this.searchBtn.Text = "検索";
			this.searchBtn.UseVisualStyleBackColor = true;
			this.searchBtn.Click += new System.EventHandler(this.SearchBtnClick);
			// 
			// searchText
			// 
			this.searchText.Location = new System.Drawing.Point(203, 20);
			this.searchText.Name = "searchText";
			this.searchText.Size = new System.Drawing.Size(100, 19);
			this.searchText.TabIndex = 26;
			// 
			// downButton
			// 
			this.downButton.Location = new System.Drawing.Point(139, 18);
			this.downButton.Name = "downButton";
			this.downButton.Size = new System.Drawing.Size(30, 23);
			this.downButton.TabIndex = 25;
			this.downButton.Text = "↓";
			this.downButton.UseVisualStyleBackColor = true;
			this.downButton.Click += new System.EventHandler(this.DownButtonClick);
			// 
			// upButton
			// 
			this.upButton.Location = new System.Drawing.Point(103, 18);
			this.upButton.Name = "upButton";
			this.upButton.Size = new System.Drawing.Size(30, 23);
			this.upButton.TabIndex = 24;
			this.upButton.Text = "↑";
			this.upButton.UseVisualStyleBackColor = true;
			this.upButton.Click += new System.EventHandler(this.UpButtonClick);
			// 
			// tabPage2
			// 
			this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
			this.tabPage2.Controls.Add(this.addYoyakuBtn);
			this.tabPage2.Controls.Add(this.taskList);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(879, 282);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "予約起動";
			// 
			// addYoyakuBtn
			// 
			this.addYoyakuBtn.Location = new System.Drawing.Point(6, 18);
			this.addYoyakuBtn.Name = "addYoyakuBtn";
			this.addYoyakuBtn.Size = new System.Drawing.Size(75, 23);
			this.addYoyakuBtn.TabIndex = 25;
			this.addYoyakuBtn.Text = "新規登録";
			this.addYoyakuBtn.UseVisualStyleBackColor = true;
			this.addYoyakuBtn.Click += new System.EventHandler(this.AddYoyakuBtnClick);
			// 
			// taskList
			// 
			this.taskList.AllowDrop = true;
			this.taskList.AllowUserToAddRows = false;
			this.taskList.AllowUserToDeleteRows = false;
			this.taskList.AllowUserToResizeRows = false;
			this.taskList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.taskList.ColumnHeadersHeight = 25;
			this.taskList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.taskList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
									this.dataGridViewTextBoxColumn1,
									this.dataGridViewTextBoxColumn2,
									this.dataGridViewTextBoxColumn3,
									this.登録日時task,
									this.status,
									this.dataGridViewCheckBoxColumn1,
									this.dataGridViewCheckBoxColumn2,
									this.dataGridViewCheckBoxColumn3,
									this.taskMailChkBox,
									this.dataGridViewCheckBoxColumn4,
									this.dataGridViewCheckBoxColumn5,
									this.dataGridViewCheckBoxColumn6,
									this.dataGridViewCheckBoxColumn7,
									this.dataGridViewCheckBoxColumn8,
									this.dataGridViewCheckBoxColumn9,
									this.dataGridViewCheckBoxColumn10,
									this.taskAppliG,
									this.taskAppliH,
									this.taskAppliI,
									this.taskAppliJ,
									this.自動削除,
									this.dataGridViewTextBoxColumn8});
			this.taskList.ContextMenuStrip = this.contextMenuStrip2;
			this.taskList.Location = new System.Drawing.Point(6, 60);
			this.taskList.MultiSelect = false;
			this.taskList.Name = "taskList";
			this.taskList.RowHeadersVisible = false;
			this.taskList.RowTemplate.Height = 21;
			this.taskList.Size = new System.Drawing.Size(865, 216);
			this.taskList.TabIndex = 24;
			this.taskList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.TaskListCellFormatting);
			this.taskList.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.taskListCellMouseDown);
			this.taskList.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.TaskListCellParsing);
			this.taskList.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.TaskListCellValidating);
			this.taskList.CurrentCellDirtyStateChanged += new System.EventHandler(this.TaskListCurrentCellDirtyStateChanged);
			this.taskList.DragDrop += new System.Windows.Forms.DragEventHandler(this.TaskListDragDrop);
			this.taskList.DragEnter += new System.Windows.Forms.DragEventHandler(this.listDragEnter);
			// 
			// dataGridViewTextBoxColumn1
			// 
			this.dataGridViewTextBoxColumn1.DataPropertyName = "taskTimeStr";
			this.dataGridViewTextBoxColumn1.FillWeight = 2F;
			this.dataGridViewTextBoxColumn1.HeaderText = "起動時刻";
			this.dataGridViewTextBoxColumn1.MinimumWidth = 2;
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.Width = 112;
			// 
			// dataGridViewTextBoxColumn2
			// 
			this.dataGridViewTextBoxColumn2.DataPropertyName = "lvId";
			this.dataGridViewTextBoxColumn2.HeaderText = "放送ID";
			this.dataGridViewTextBoxColumn2.MinimumWidth = 80;
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.Width = 80;
			// 
			// dataGridViewTextBoxColumn3
			// 
			this.dataGridViewTextBoxColumn3.DataPropertyName = "args";
			this.dataGridViewTextBoxColumn3.HeaderText = "引数";
			this.dataGridViewTextBoxColumn3.MinimumWidth = 100;
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			// 
			// 登録日時task
			// 
			this.登録日時task.DataPropertyName = "addDate";
			this.登録日時task.HeaderText = "登録日時";
			this.登録日時task.Name = "登録日時task";
			this.登録日時task.ReadOnly = true;
			this.登録日時task.Width = 112;
			// 
			// status
			// 
			this.status.DataPropertyName = "status";
			this.status.HeaderText = "状態";
			this.status.MinimumWidth = 45;
			this.status.Name = "status";
			this.status.ReadOnly = true;
			this.status.Width = 45;
			// 
			// dataGridViewCheckBoxColumn1
			// 
			this.dataGridViewCheckBoxColumn1.DataPropertyName = "popup";
			this.dataGridViewCheckBoxColumn1.HeaderText = "ﾎﾟｯﾌﾟｱｯﾌﾟ";
			this.dataGridViewCheckBoxColumn1.MinimumWidth = 45;
			this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
			this.dataGridViewCheckBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewCheckBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.dataGridViewCheckBoxColumn1.Width = 45;
			// 
			// dataGridViewCheckBoxColumn2
			// 
			this.dataGridViewCheckBoxColumn2.DataPropertyName = "baloon";
			this.dataGridViewCheckBoxColumn2.HeaderText = "ﾊﾞﾙｰﾝ";
			this.dataGridViewCheckBoxColumn2.MinimumWidth = 45;
			this.dataGridViewCheckBoxColumn2.Name = "dataGridViewCheckBoxColumn2";
			this.dataGridViewCheckBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewCheckBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.dataGridViewCheckBoxColumn2.Width = 45;
			// 
			// dataGridViewCheckBoxColumn3
			// 
			this.dataGridViewCheckBoxColumn3.DataPropertyName = "browser";
			this.dataGridViewCheckBoxColumn3.HeaderText = "Web";
			this.dataGridViewCheckBoxColumn3.MinimumWidth = 45;
			this.dataGridViewCheckBoxColumn3.Name = "dataGridViewCheckBoxColumn3";
			this.dataGridViewCheckBoxColumn3.Width = 45;
			// 
			// taskMailChkBox
			// 
			this.taskMailChkBox.DataPropertyName = "mail";
			this.taskMailChkBox.HeaderText = "ﾒｰﾙ";
			this.taskMailChkBox.MinimumWidth = 45;
			this.taskMailChkBox.Name = "taskMailChkBox";
			this.taskMailChkBox.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.taskMailChkBox.Width = 45;
			// 
			// dataGridViewCheckBoxColumn4
			// 
			this.dataGridViewCheckBoxColumn4.DataPropertyName = "sound";
			this.dataGridViewCheckBoxColumn4.HeaderText = "音";
			this.dataGridViewCheckBoxColumn4.MinimumWidth = 45;
			this.dataGridViewCheckBoxColumn4.Name = "dataGridViewCheckBoxColumn4";
			this.dataGridViewCheckBoxColumn4.Width = 45;
			// 
			// dataGridViewCheckBoxColumn5
			// 
			this.dataGridViewCheckBoxColumn5.DataPropertyName = "appliA";
			this.dataGridViewCheckBoxColumn5.HeaderText = "ｱﾌﾟﾘA";
			this.dataGridViewCheckBoxColumn5.MinimumWidth = 45;
			this.dataGridViewCheckBoxColumn5.Name = "dataGridViewCheckBoxColumn5";
			this.dataGridViewCheckBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewCheckBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.dataGridViewCheckBoxColumn5.Width = 45;
			// 
			// dataGridViewCheckBoxColumn6
			// 
			this.dataGridViewCheckBoxColumn6.DataPropertyName = "appliB";
			this.dataGridViewCheckBoxColumn6.HeaderText = "ｱﾌﾟﾘB";
			this.dataGridViewCheckBoxColumn6.MinimumWidth = 45;
			this.dataGridViewCheckBoxColumn6.Name = "dataGridViewCheckBoxColumn6";
			this.dataGridViewCheckBoxColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewCheckBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.dataGridViewCheckBoxColumn6.Width = 45;
			// 
			// dataGridViewCheckBoxColumn7
			// 
			this.dataGridViewCheckBoxColumn7.DataPropertyName = "appliC";
			this.dataGridViewCheckBoxColumn7.HeaderText = "ｱﾌﾟﾘC";
			this.dataGridViewCheckBoxColumn7.MinimumWidth = 45;
			this.dataGridViewCheckBoxColumn7.Name = "dataGridViewCheckBoxColumn7";
			this.dataGridViewCheckBoxColumn7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewCheckBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.dataGridViewCheckBoxColumn7.Width = 45;
			// 
			// dataGridViewCheckBoxColumn8
			// 
			this.dataGridViewCheckBoxColumn8.DataPropertyName = "appliD";
			this.dataGridViewCheckBoxColumn8.HeaderText = "ｱﾌﾟﾘD";
			this.dataGridViewCheckBoxColumn8.MinimumWidth = 45;
			this.dataGridViewCheckBoxColumn8.Name = "dataGridViewCheckBoxColumn8";
			this.dataGridViewCheckBoxColumn8.Width = 45;
			// 
			// dataGridViewCheckBoxColumn9
			// 
			this.dataGridViewCheckBoxColumn9.DataPropertyName = "appliE";
			this.dataGridViewCheckBoxColumn9.HeaderText = "ｱﾌﾟﾘE";
			this.dataGridViewCheckBoxColumn9.MinimumWidth = 45;
			this.dataGridViewCheckBoxColumn9.Name = "dataGridViewCheckBoxColumn9";
			this.dataGridViewCheckBoxColumn9.Width = 45;
			// 
			// dataGridViewCheckBoxColumn10
			// 
			this.dataGridViewCheckBoxColumn10.DataPropertyName = "appliF";
			this.dataGridViewCheckBoxColumn10.HeaderText = "ｱﾌﾟﾘF";
			this.dataGridViewCheckBoxColumn10.MinimumWidth = 45;
			this.dataGridViewCheckBoxColumn10.Name = "dataGridViewCheckBoxColumn10";
			this.dataGridViewCheckBoxColumn10.Width = 45;
			// 
			// taskAppliG
			// 
			this.taskAppliG.DataPropertyName = "appliG";
			this.taskAppliG.HeaderText = "ｱﾌﾟﾘG";
			this.taskAppliG.MinimumWidth = 45;
			this.taskAppliG.Name = "taskAppliG";
			this.taskAppliG.Width = 45;
			// 
			// taskAppliH
			// 
			this.taskAppliH.DataPropertyName = "appliH";
			this.taskAppliH.HeaderText = "ｱﾌﾟﾘH";
			this.taskAppliH.MinimumWidth = 45;
			this.taskAppliH.Name = "taskAppliH";
			this.taskAppliH.Width = 45;
			// 
			// taskAppliI
			// 
			this.taskAppliI.DataPropertyName = "appliI";
			this.taskAppliI.HeaderText = "ｱﾌﾟﾘI";
			this.taskAppliI.MinimumWidth = 45;
			this.taskAppliI.Name = "taskAppliI";
			this.taskAppliI.Width = 45;
			// 
			// taskAppliJ
			// 
			this.taskAppliJ.DataPropertyName = "appliJ";
			this.taskAppliJ.HeaderText = "ｱﾌﾟﾘJ";
			this.taskAppliJ.MinimumWidth = 45;
			this.taskAppliJ.Name = "taskAppliJ";
			this.taskAppliJ.Width = 45;
			// 
			// 自動削除
			// 
			this.自動削除.DataPropertyName = "isDelete";
			this.自動削除.HeaderText = "削除";
			this.自動削除.MinimumWidth = 45;
			this.自動削除.Name = "自動削除";
			this.自動削除.Width = 45;
			// 
			// dataGridViewTextBoxColumn8
			// 
			this.dataGridViewTextBoxColumn8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridViewTextBoxColumn8.DataPropertyName = "memo";
			this.dataGridViewTextBoxColumn8.HeaderText = "ﾒﾓ";
			this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
			// 
			// contextMenuStrip2
			// 
			this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.taskListOpenUrlMenu,
									this.toolStripSeparator5,
									this.taskListCopyUrlMenu,
									this.taskListCopyArgsMenu,
									this.toolStripSeparator6,
									this.taskListRemoveLineMenu});
			this.contextMenuStrip2.Name = "contextMenuStrip1";
			this.contextMenuStrip2.Size = new System.Drawing.Size(185, 104);
			// 
			// taskListOpenUrlMenu
			// 
			this.taskListOpenUrlMenu.Name = "taskListOpenUrlMenu";
			this.taskListOpenUrlMenu.Size = new System.Drawing.Size(184, 22);
			this.taskListOpenUrlMenu.Text = "放送のURLを開く";
			this.taskListOpenUrlMenu.Click += new System.EventHandler(this.TaskListOpenUrlMenuClick);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(181, 6);
			// 
			// taskListCopyUrlMenu
			// 
			this.taskListCopyUrlMenu.Name = "taskListCopyUrlMenu";
			this.taskListCopyUrlMenu.Size = new System.Drawing.Size(184, 22);
			this.taskListCopyUrlMenu.Text = "放送のURLをコピー";
			this.taskListCopyUrlMenu.Click += new System.EventHandler(this.TaskListCopyUrlMenuClick);
			// 
			// taskListCopyArgsMenu
			// 
			this.taskListCopyArgsMenu.Name = "taskListCopyArgsMenu";
			this.taskListCopyArgsMenu.Size = new System.Drawing.Size(184, 22);
			this.taskListCopyArgsMenu.Text = "引数をコピー";
			this.taskListCopyArgsMenu.Click += new System.EventHandler(this.TaskListCopyArgsMenuClick);
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(181, 6);
			// 
			// taskListRemoveLineMenu
			// 
			this.taskListRemoveLineMenu.Name = "taskListRemoveLineMenu";
			this.taskListRemoveLineMenu.Size = new System.Drawing.Size(184, 22);
			this.taskListRemoveLineMenu.Text = "この行を削除";
			this.taskListRemoveLineMenu.Click += new System.EventHandler(this.TaskListRemoveLineMenuClick);
			// 
			// MainForm
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(899, 362);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "MainForm";
			this.Text = "ニコ生新配信録画ツール（仮 ver0.86.15";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.form_Close);
			this.Load += new System.EventHandler(this.mainForm_Load);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.alartList)).EndInit();
			this.contextMenuStrip1.ResumeLayout(false);
			this.notifyIconMenuStrip.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.taskList)).EndInit();
			this.contextMenuStrip2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
		private System.Windows.Forms.ToolStripMenuItem bulkChannelFollowMenu;
		private System.Windows.Forms.ToolStripMenuItem bulkCommunityFollowMenu;
		private System.Windows.Forms.ToolStripMenuItem bulkUserFollowMenu;
		private System.Windows.Forms.ToolStripMenuItem isTaskListDisplayMailTabMenu;
		private System.Windows.Forms.ToolStripMenuItem isDisplayMailTabMenu;
		private System.Windows.Forms.ToolStripMenuItem mailOffMenuItem;
		private System.Windows.Forms.DataGridViewCheckBoxColumn taskMailChkBox;
		private System.Windows.Forms.DataGridViewCheckBoxColumn ﾒｰﾙ;
		private System.Windows.Forms.ToolStripMenuItem openAppliJMenu;
		private System.Windows.Forms.ToolStripMenuItem openAppliIMenu;
		private System.Windows.Forms.ToolStripMenuItem openAppliHMenu;
		private System.Windows.Forms.ToolStripMenuItem openAppliGMenu;
		private System.Windows.Forms.ToolStripMenuItem openAppliFMenu;
		private System.Windows.Forms.ToolStripMenuItem openAppliEMenu;
		private System.Windows.Forms.ToolStripMenuItem openAppliDMenu;
		private System.Windows.Forms.ToolStripMenuItem openAppliCMenu;
		private System.Windows.Forms.ToolStripMenuItem openAppliBMenu;
		private System.Windows.Forms.ToolStripMenuItem openAppliAMenu;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
		private System.Windows.Forms.ToolStripMenuItem appliJOffMenuItem;
		private System.Windows.Forms.ToolStripMenuItem appliIOffMenuItem;
		private System.Windows.Forms.ToolStripMenuItem appliHOffMenuItem;
		private System.Windows.Forms.ToolStripMenuItem appliGOffMenuItem;
		private System.Windows.Forms.ToolStripMenuItem appliFOffMenuItem;
		private System.Windows.Forms.ToolStripMenuItem appliEOffMenuItem;
		private System.Windows.Forms.ToolStripMenuItem appliDOffMenuItem;
		private System.Windows.Forms.ToolStripMenuItem appliCOffMenuItem;
		private System.Windows.Forms.ToolStripMenuItem appliBOffMenuItem;
		private System.Windows.Forms.ToolStripMenuItem appliAOffMenuItem;
		private System.Windows.Forms.ToolStripMenuItem soundOffMenuItem;
		private System.Windows.Forms.ToolStripMenuItem webOffMenuItem;
		private System.Windows.Forms.ToolStripMenuItem balloonOffMenuItem;
		private System.Windows.Forms.ToolStripMenuItem popupOffMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
		private System.Windows.Forms.ToolStripMenuItem allOffMenuItem;
		private System.Windows.Forms.ToolStripMenuItem notifyOffMenuItem;
		private System.Windows.Forms.ToolStripMenuItem notifyMenuItem;
		private System.Windows.Forms.Label favoriteNumLabel;
		private System.Windows.Forms.TextBox searchText;
		private System.Windows.Forms.Button searchBtn;
		private System.Windows.Forms.Button upButton;
		private System.Windows.Forms.Button downButton;
		private System.Windows.Forms.ToolStripMenuItem isTaskListDisplayMemoTabMenu;
		private System.Windows.Forms.ToolStripMenuItem isTaskListDisplayDeleteTabMenu;
		private System.Windows.Forms.ToolStripMenuItem isTaskListDisplayAppliJTabMenu;
		private System.Windows.Forms.ToolStripMenuItem isTaskListDisplayAppliITabMenu;
		private System.Windows.Forms.ToolStripMenuItem isTaskListDisplayAppliHTabMenu;
		private System.Windows.Forms.ToolStripMenuItem isTaskListDisplayAppliGTabMenu;
		private System.Windows.Forms.ToolStripMenuItem isTaskListDisplayAppliFTabMenu;
		private System.Windows.Forms.ToolStripMenuItem isTaskListDisplayAppliETabMenu;
		private System.Windows.Forms.ToolStripMenuItem isTaskListDisplayAppliDTabMenu;
		private System.Windows.Forms.ToolStripMenuItem isTaskListDisplayAppliCTabMenu;
		private System.Windows.Forms.ToolStripMenuItem isTaskListDisplayAppliBTabMenu;
		private System.Windows.Forms.ToolStripMenuItem isTaskListDisplayAppliATabMenu;
		private System.Windows.Forms.ToolStripMenuItem isTaskListDisplaySoundTabMenu;
		private System.Windows.Forms.ToolStripMenuItem isTaskListDisplayWebTabMenu;
		private System.Windows.Forms.ToolStripMenuItem isTaskListDisplayBalloonTabMenu;
		private System.Windows.Forms.ToolStripMenuItem isTaskListDisplayPopupTabMenu;
		private System.Windows.Forms.ToolStripMenuItem isTaskListDisplayStatusTabMenu;
		private System.Windows.Forms.ToolStripMenuItem isTaskListDisplayAddDtTabMenu;
		private System.Windows.Forms.ToolStripMenuItem isTaskListDisplayArgsTabMenu;
		private System.Windows.Forms.ToolStripMenuItem isTaskListDisplayLvidTabMenu;
		private System.Windows.Forms.ToolStripMenuItem isTaskListDisplayStartTimeTabMenu;
		private System.Windows.Forms.ToolStripMenuItem displayTaskTabMenu;
		private System.Windows.Forms.DataGridViewCheckBoxColumn taskAppliJ;
		private System.Windows.Forms.DataGridViewCheckBoxColumn taskAppliI;
		private System.Windows.Forms.DataGridViewCheckBoxColumn taskAppliH;
		private System.Windows.Forms.DataGridViewCheckBoxColumn taskAppliG;
		private System.Windows.Forms.ToolStripMenuItem isDisplayAppliJTabMenu;
		private System.Windows.Forms.ToolStripMenuItem isDisplayAppliITabMenu;
		private System.Windows.Forms.ToolStripMenuItem isDisplayAppliHTabMenu;
		private System.Windows.Forms.ToolStripMenuItem isDisplayAppliGTabMenu;
		private System.Windows.Forms.DataGridViewCheckBoxColumn ｱﾌﾟﾘJ;
		private System.Windows.Forms.DataGridViewCheckBoxColumn ｱﾌﾟﾘI;
		private System.Windows.Forms.DataGridViewCheckBoxColumn ｱﾌﾟﾘH;
		private System.Windows.Forms.DataGridViewCheckBoxColumn ｱﾌﾟﾘG;
		private System.Windows.Forms.ToolStripMenuItem isDisplayMemoTabMenu;
		private System.Windows.Forms.ToolStripMenuItem isDisplayAppliFTabMenu;
		private System.Windows.Forms.ToolStripMenuItem isDisplayAppliETabMenu;
		private System.Windows.Forms.ToolStripMenuItem isDisplayAppliDTabMenu;
		private System.Windows.Forms.ToolStripMenuItem isDisplayAppliCTabMenu;
		private System.Windows.Forms.ToolStripMenuItem isDisplayAppliBTabMenu;
		private System.Windows.Forms.ToolStripMenuItem isDisplayAppliATabMenu;
		private System.Windows.Forms.ToolStripMenuItem isDisplaySoundTabMenu;
		private System.Windows.Forms.ToolStripMenuItem isDisplayWebTabMenu;
		private System.Windows.Forms.ToolStripMenuItem isDisplayBalloonTabMenu;
		private System.Windows.Forms.ToolStripMenuItem isDisplayPopupTabMenu;
		private System.Windows.Forms.ToolStripMenuItem isDisplayAddDateDtTabMenu;
		private System.Windows.Forms.ToolStripMenuItem isDisplayLastHosoDtTabMenu;
		private System.Windows.Forms.ToolStripMenuItem isDisplayUserFollowTabMenu;
		private System.Windows.Forms.ToolStripMenuItem isDisplayCommunityFollowTabMenu;
		private System.Windows.Forms.ToolStripMenuItem isDisplayKeywordTabMenu;
		private System.Windows.Forms.ToolStripMenuItem isDisplayUserNameTabMenu;
		private System.Windows.Forms.ToolStripMenuItem isDisplayCommunityNameTabMenu;
		private System.Windows.Forms.ToolStripMenuItem isDisplayUserIdTabMenu;
		private System.Windows.Forms.ToolStripMenuItem isDisplayCommunityIdTabMenu;
		private System.Windows.Forms.ToolStripMenuItem displayFavoriteTabMenu;
		private System.Windows.Forms.ToolStripMenuItem displayMenuItem;
		private System.Windows.Forms.DataGridViewTextBoxColumn status;
		private System.Windows.Forms.ToolStripMenuItem taskListCopyArgsMenu;
		private System.Windows.Forms.ToolStripMenuItem taskListRemoveLineMenu;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripMenuItem taskListCopyUrlMenu;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem taskListOpenUrlMenu;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
		private System.Windows.Forms.DataGridViewTextBoxColumn 登録日時task;
		private System.Windows.Forms.DataGridViewCheckBoxColumn 自動削除;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
		private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn10;
		private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn9;
		private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn8;
		private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn7;
		private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn6;
		private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn5;
		private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn4;
		private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn3;
		private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn2;
		private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
		public System.Windows.Forms.DataGridView taskList;
		private System.Windows.Forms.Button addYoyakuBtn;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.DataGridViewCheckBoxColumn ｱﾌﾟﾘF;
		private System.Windows.Forms.DataGridViewCheckBoxColumn ｱﾌﾟﾘE;
		private System.Windows.Forms.DataGridViewCheckBoxColumn ｱﾌﾟﾘC;
		private System.Windows.Forms.DataGridViewCheckBoxColumn Web;
		private System.Windows.Forms.DataGridViewCheckBoxColumn 音;
		private System.Windows.Forms.ToolStripMenuItem closeNotifyIconMenu;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem openNotifyIconMenu;
		private System.Windows.Forms.ContextMenuStrip notifyIconMenuStrip;
		public System.Windows.Forms.NotifyIcon notifyIcon;
		private System.Windows.Forms.DataGridViewCheckBoxColumn ﾊﾞﾙｰﾝ;
		private System.Windows.Forms.DataGridViewCheckBoxColumn ﾎﾟｯﾌﾟｱｯﾌﾟ;
		private System.Windows.Forms.DataGridViewTextBoxColumn ｷｰﾜｰﾄﾞ;
		private System.Windows.Forms.ToolStripMenuItem removeLineMenu;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem copyUserUrlMenu;
		private System.Windows.Forms.ToolStripMenuItem copyCommunityUrlMenu;
		private System.Windows.Forms.ToolStripMenuItem copyLastHosoMenu;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem openUserUrlMenu;
		private System.Windows.Forms.ToolStripMenuItem openCommunityUrlMenu;
		private System.Windows.Forms.ToolStripMenuItem openLastHosoMenu;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem readNamarokuListMenu;
		private System.Windows.Forms.DataGridViewButtonColumn ﾕｰｻﾞｰﾌｫﾛｰ;
		private System.Windows.Forms.DataGridViewButtonColumn ｺﾐｭﾆﾃｨﾌｫﾛｰ;
		private System.Windows.Forms.DataGridViewTextBoxColumn 登録日時;
		private System.Windows.Forms.DataGridViewTextBoxColumn 最近の放送日時;
		private System.Windows.Forms.DataGridViewTextBoxColumn ﾕｰｻﾞｰ名;
		private System.Windows.Forms.DataGridViewTextBoxColumn ｺﾐｭﾆﾃｨ名;
		private System.Windows.Forms.DataGridViewTextBoxColumn ﾕｰｻﾞｰID;
		private System.Windows.Forms.DataGridViewTextBoxColumn ｺﾐｭﾆﾃｨID;
		private System.Windows.Forms.DataGridViewTextBoxColumn comment;
		private System.Windows.Forms.DataGridViewCheckBoxColumn アプリC;
		private System.Windows.Forms.DataGridViewCheckBoxColumn アプリB;
		private System.Windows.Forms.DataGridViewCheckBoxColumn アプリA;
		public System.Windows.Forms.DataGridView alartList;
		private System.Windows.Forms.Button addBtn;
		private System.Windows.Forms.ToolStripStatusLabel lastHosoStatusBar;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripMenuItem バージョン情報VToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 終了ToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		public System.Windows.Forms.ToolStripMenuItem optionMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toolMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fileMenuItem;
		private System.Windows.Forms.MenuStrip menuStrip1;
		public System.Windows.Forms.TextBox logText;
		private System.Windows.Forms.Label groupLabel;
		
		
	}
}
