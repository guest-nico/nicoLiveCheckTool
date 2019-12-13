using System.Runtime.InteropServices;
using System.Windows.Forms;

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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.groupLabel = new System.Windows.Forms.Label();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.readNamarokuListMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.readNamarokuUserListMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.bulkAddFromFollowComMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.終了ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.updateMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.updateLiveListMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.updateCateCategoryMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.updateCateAllMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.updateCateCommon = new System.Windows.Forms.ToolStripMenuItem();
			this.updateCateTryMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.updateCatePlayMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.updateCatePresenMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.updateCateSuperIchibaMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.updateCateFaceMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.updateCateRushMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.updateCateR18Menu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
			this.updateAutoUpdateStartMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.updateAutoUpdateStopMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.updateAutoUpdateFirstMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
			this.updateAutoDeleteMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.updateNoDelMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.update5minDelMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.update10minDelMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.update15minDelMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.update20minDelMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.update30minDelMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.update1hourDelMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.update6hourDelMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
			this.updateTopFavoriteMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.updateOnlyFavoriteMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.updateAutoSortMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.updateHideMemberOnlyWithoutFavoriteMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.updateHideMemberOnlyWithFavoriteMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.updateHideQuestionCategoryMenu = new System.Windows.Forms.ToolStripMenuItem();
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
			this.isDisplayIsAndTabMenu = new System.Windows.Forms.ToolStripMenuItem();
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
			this.isDisplaySoundTypeTabMenu = new System.Windows.Forms.ToolStripMenuItem();
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
			this.displayOnAirTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isDisplayThumbnailOnAirTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isDisplayTitleOnAirTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isDisplayHostNameOnAirTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isDisplayCommunityNameOnAirTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isDisplayDescriptionOnAirTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isDisplayLvidOnAirTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isDisplayCommunityIDOnAirTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isDisplayElapsedTimeOnAirTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isDisplayCategoryOnAirTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isDisplayFaceOnAirTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isDisplayRushOnAirTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isDisplayCruiseOnAirTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isDisplayCasOnAirTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isDisplayMemberOnlyOnAirTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isDisplayTypeOnAirTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isDisplayFavoriteOnAirTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.isDisplayMemoOnAirTabMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.displayHistoryListMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem12 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem13 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem14 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem15 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem16 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem17 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem18 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem19 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem20 = new System.Windows.Forms.ToolStripMenuItem();
			this.displayNotAlartListMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.displayHistoryDtMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator20 = new System.Windows.Forms.ToolStripSeparator();
			this.colorColumnMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.colorCommunityIdMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.colorUserIdMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.colorCommunityNameMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.colorUserNameMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.colorKeywordMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.colorIsAndMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.colorComFollowMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.colorUserFollowMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.colorRecentLiveDtMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.colorAddDtMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.colorPopupMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.colorBaloonMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.colorWebMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.colorMailMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.colorSoundMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.colorAppliAMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.colorAppliBMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.colorAppliCMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.colorAppliDMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.colorAppliEMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.colorAppliFMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.colorAppliGMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.colorAppliHMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.colorAppliIMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.colorAppliJMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.colorSountTypeMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.colorMemoMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.colorHistoryColorColumnMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem21 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem22 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem23 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem24 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem25 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem26 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem27 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem28 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem29 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem30 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator21 = new System.Windows.Forms.ToolStripSeparator();
			this.disableFollowMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.checkExistsComMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.checkExistsUserMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator18 = new System.Windows.Forms.ToolStripSeparator();
			this.getUserInfoFromComMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.getComThumbBulkMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.getUserThumbBulkMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator19 = new System.Windows.Forms.ToolStripSeparator();
			this.bulkUserFollowMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.bulkCommunityFollowMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.bulkChannelFollowMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
			this.duplicateCheckMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator29 = new System.Windows.Forms.ToolStripSeparator();
			this.optionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.バージョン情報VToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.lastHosoStatusBar = new System.Windows.Forms.ToolStripStatusLabel();
			this.existCheckStatusBar = new System.Windows.Forms.ToolStripStatusLabel();
			this.liveListUpdateStatusBar = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.addBtn = new System.Windows.Forms.Button();
			this.alartList = new System.Windows.Forms.DataGridView();
			this.ｺﾐｭﾆﾃｨID = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ﾕｰｻﾞｰID = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ｺﾐｭﾆﾃｨ名 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ﾕｰｻﾞｰ名 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ｷｰﾜｰﾄﾞ = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.合致条件 = new System.Windows.Forms.DataGridViewComboBoxColumn();
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
			this.音設定 = new System.Windows.Forms.DataGridViewComboBoxColumn();
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
			this.editLineMenu = new System.Windows.Forms.ToolStripMenuItem();
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
			this.TabPages = new System.Windows.Forms.TabControl();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.liveList = new System.Windows.Forms.DataGridView();
			this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewImageColumn();
			this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewButtonColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewButtonColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewCheckBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewCheckBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewCheckBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewCheckBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewCheckBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewCheckBoxColumn16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.liveListOpenUrlMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.liveListOpenCommunityUrlMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
			this.liveListCopyMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.liveListUrlCopyMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.liveListCommunityUrlCopyMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
			this.liveListTitleCopyMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.liveListHostNameCopyMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.liveListCommunityNameCopyMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.liveListDescriptionCopyMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
			this.liveListWriteSamuneMemoMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.liveListNGthumbnailMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.liveListUpdateSamuneMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
			this.liveListAddFavoriteCommunityMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.liveListRemoveFavoriteCommunityMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
			this.liveListDeleteRowMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.liveListSearchBtn = new System.Windows.Forms.Button();
			this.liveListSearchText = new System.Windows.Forms.TextBox();
			this.categoryRightBtn = new System.Windows.Forms.Button();
			this.categoryLeftBtn = new System.Windows.Forms.Button();
			this.categoryBtnPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.allCategoryBtn = new System.Windows.Forms.RadioButton();
			this.commonCategoryBtn = new System.Windows.Forms.RadioButton();
			this.tryCategoryBtn = new System.Windows.Forms.RadioButton();
			this.liveCategoryBtn = new System.Windows.Forms.RadioButton();
			this.reqCategoryBtn = new System.Windows.Forms.RadioButton();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.favoriteCommunityPanel = new System.Windows.Forms.Panel();
			this.upButton = new System.Windows.Forms.Button();
			this.downButton = new System.Windows.Forms.Button();
			this.userThumbBox = new System.Windows.Forms.PictureBox();
			this.logText = new System.Windows.Forms.TextBox();
			this.comThumbBox = new System.Windows.Forms.PictureBox();
			this.searchText = new System.Windows.Forms.TextBox();
			this.favoriteNumLabel = new System.Windows.Forms.Label();
			this.searchBtn = new System.Windows.Forms.Button();
			this.favoriteUserPanel = new System.Windows.Forms.Panel();
			this.favoriteUserThumbBox = new System.Windows.Forms.PictureBox();
			this.favoriteUserNumLabel = new System.Windows.Forms.Label();
			this.userAddBtn = new System.Windows.Forms.Button();
			this.userAddText = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.userAlartList = new System.Windows.Forms.DataGridView();
			this.dataGridViewTextBoxColumn25 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn26 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn27 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn28 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn29 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewComboBoxColumn1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.dataGridViewButtonColumn3 = new System.Windows.Forms.DataGridViewButtonColumn();
			this.dataGridViewButtonColumn4 = new System.Windows.Forms.DataGridViewButtonColumn();
			this.dataGridViewTextBoxColumn30 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn31 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewCheckBoxColumn17 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dataGridViewCheckBoxColumn18 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dataGridViewCheckBoxColumn19 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dataGridViewCheckBoxColumn20 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dataGridViewCheckBoxColumn21 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dataGridViewCheckBoxColumn22 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dataGridViewCheckBoxColumn23 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dataGridViewCheckBoxColumn24 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dataGridViewCheckBoxColumn25 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dataGridViewCheckBoxColumn26 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dataGridViewCheckBoxColumn27 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dataGridViewCheckBoxColumn28 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dataGridViewCheckBoxColumn29 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dataGridViewCheckBoxColumn30 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dataGridViewCheckBoxColumn31 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dataGridViewComboBoxColumn2 = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.dataGridViewTextBoxColumn32 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.contextMenuStrip4 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem32 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator30 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItem33 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem35 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator31 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItem36 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem37 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator32 = new System.Windows.Forms.ToolStripSeparator();
			this.openAppliAUserFavoriteMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.openAppliBUserFavoriteMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.openAppliCUserFavoriteMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.openAppliDUserFavoriteMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.openAppliEUserFavoriteMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.openAppliFUserFavoriteMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.openAppliGUserFavoriteMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.openAppliHUserFavoriteMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.openAppliIUserFavoriteMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.openAppliJUserFavoriteMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.favoriteCommunityBtn = new System.Windows.Forms.RadioButton();
			this.favoriteUserBtn = new System.Windows.Forms.RadioButton();
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
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.historySplitContainer = new System.Windows.Forms.SplitContainer();
			this.label1 = new System.Windows.Forms.Label();
			this.historyList = new System.Windows.Forms.DataGridView();
			this.放送開始日時 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.放送タイトル = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.放送者 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.コミュニティ名 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.放送ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ユーザーID = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.コミュニティID = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.historyKeyword = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.お気に入り = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.説明 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.historyListMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.historyListOpenUrlMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.historyListOpenCommunityUrlMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.historyListOpenUserUrlMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator24 = new System.Windows.Forms.ToolStripSeparator();
			this.historyListCopyUrlMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.historyListCopyCommunityUrlMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.historyListCopyUserUrlMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator25 = new System.Windows.Forms.ToolStripSeparator();
			this.historyListAddAlartListMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator27 = new System.Windows.Forms.ToolStripSeparator();
			this.historyListDeleteRowMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.label3 = new System.Windows.Forms.Label();
			this.notAlartList = new System.Windows.Forms.DataGridView();
			this.dataGridViewTextBoxColumn16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn22 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.キーワード = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn24 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.notAlartListMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.notAlartListOpenUrlMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.notAlartListOpenCommunityUrlMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.notAlartListOpenUserUrlMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator22 = new System.Windows.Forms.ToolStripSeparator();
			this.notAlartListCopyUrlMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.notAlartListCopyCommunityUrlMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.notAlartListCopyUserUrlMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator26 = new System.Windows.Forms.ToolStripSeparator();
			this.notAlartListAddAlartListMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator28 = new System.Windows.Forms.ToolStripSeparator();
			this.notAlartListDeleteRowMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.logList = new System.Windows.Forms.DataGridView();
			this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.logListMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.logListCopyMessageMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator23 = new System.Windows.Forms.ToolStripSeparator();
			this.logListDeleteRowMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.alartList)).BeginInit();
			this.contextMenuStrip1.SuspendLayout();
			this.notifyIconMenuStrip.SuspendLayout();
			this.TabPages.SuspendLayout();
			this.tabPage3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.liveList)).BeginInit();
			this.contextMenuStrip3.SuspendLayout();
			this.categoryBtnPanel.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.favoriteCommunityPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.userThumbBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.comThumbBox)).BeginInit();
			this.favoriteUserPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.favoriteUserThumbBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.userAlartList)).BeginInit();
			this.contextMenuStrip4.SuspendLayout();
			this.tabPage2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.taskList)).BeginInit();
			this.contextMenuStrip2.SuspendLayout();
			this.tabPage4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.historySplitContainer)).BeginInit();
			this.historySplitContainer.Panel1.SuspendLayout();
			this.historySplitContainer.Panel2.SuspendLayout();
			this.historySplitContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.historyList)).BeginInit();
			this.historyListMenu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.notAlartList)).BeginInit();
			this.notAlartListMenu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.logList)).BeginInit();
			this.logListMenu.SuspendLayout();
			this.SuspendLayout();
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
									this.updateMenuItem,
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
									this.readNamarokuUserListMenu,
									this.bulkAddFromFollowComMenu,
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
			this.readNamarokuListMenu.Size = new System.Drawing.Size(382, 22);
			this.readNamarokuListMenu.Text = "namarokuの登録コミュニティ設定を読み込んでみる(&N)";
			this.readNamarokuListMenu.Click += new System.EventHandler(this.ReadNamarokuListMenuClick);
			// 
			// readNamarokuUserListMenu
			// 
			this.readNamarokuUserListMenu.Name = "readNamarokuUserListMenu";
			this.readNamarokuUserListMenu.Size = new System.Drawing.Size(382, 22);
			this.readNamarokuUserListMenu.Text = "namarokuの登録ユーザー設定を読み込んでみる(&U)";
			this.readNamarokuUserListMenu.Click += new System.EventHandler(this.ReadNamarokuUserListMenuClick);
			// 
			// bulkAddFromFollowComMenu
			// 
			this.bulkAddFromFollowComMenu.Name = "bulkAddFromFollowComMenu";
			this.bulkAddFromFollowComMenu.Size = new System.Drawing.Size(382, 22);
			this.bulkAddFromFollowComMenu.Text = "参加中のコミュニティから一括登録する(&C)";
			this.bulkAddFromFollowComMenu.Click += new System.EventHandler(this.BulkAddFromFollowComMenuClick);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(379, 6);
			// 
			// 終了ToolStripMenuItem
			// 
			this.終了ToolStripMenuItem.Name = "終了ToolStripMenuItem";
			this.終了ToolStripMenuItem.Size = new System.Drawing.Size(382, 22);
			this.終了ToolStripMenuItem.Text = "終了(&X)";
			this.終了ToolStripMenuItem.Click += new System.EventHandler(this.endMenu_Click);
			// 
			// updateMenuItem
			// 
			this.updateMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.updateLiveListMenu,
									this.updateCateCategoryMenu,
									this.toolStripSeparator15,
									this.updateAutoUpdateStartMenu,
									this.updateAutoUpdateStopMenu,
									this.updateAutoUpdateFirstMenu,
									this.toolStripSeparator16,
									this.updateAutoDeleteMenu,
									this.toolStripSeparator17,
									this.updateTopFavoriteMenu,
									this.updateOnlyFavoriteMenu,
									this.updateAutoSortMenu,
									this.updateHideMemberOnlyWithoutFavoriteMenu,
									this.updateHideMemberOnlyWithFavoriteMenu,
									this.updateHideQuestionCategoryMenu});
			this.updateMenuItem.Name = "updateMenuItem";
			this.updateMenuItem.Size = new System.Drawing.Size(62, 22);
			this.updateMenuItem.Text = "更新(&R)";
			// 
			// updateLiveListMenu
			// 
			this.updateLiveListMenu.Name = "updateLiveListMenu";
			this.updateLiveListMenu.Size = new System.Drawing.Size(323, 22);
			this.updateLiveListMenu.Text = "番組一覧の更新(&N)";
			this.updateLiveListMenu.Click += new System.EventHandler(this.UpdateLiveListMenuClick);
			// 
			// updateCateCategoryMenu
			// 
			this.updateCateCategoryMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.updateCateAllMenu,
									this.updateCateCommon,
									this.updateCateTryMenu,
									this.updateCatePlayMenu,
									this.updateCatePresenMenu,
									this.updateCateSuperIchibaMenu,
									this.updateCateFaceMenu,
									this.updateCateRushMenu,
									this.updateCateR18Menu});
			this.updateCateCategoryMenu.Name = "updateCateCategoryMenu";
			this.updateCateCategoryMenu.Size = new System.Drawing.Size(323, 22);
			this.updateCateCategoryMenu.Text = "更新するカテゴリを限定する(&C)";
			this.updateCateCategoryMenu.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.UpdateCateCategoryMenuDropDownItemClicked);
			// 
			// updateCateAllMenu
			// 
			this.updateCateAllMenu.Checked = true;
			this.updateCateAllMenu.CheckState = System.Windows.Forms.CheckState.Checked;
			this.updateCateAllMenu.Name = "updateCateAllMenu";
			this.updateCateAllMenu.Size = new System.Drawing.Size(154, 22);
			this.updateCateAllMenu.Text = "すべて(&A)";
			// 
			// updateCateCommon
			// 
			this.updateCateCommon.Name = "updateCateCommon";
			this.updateCateCommon.Size = new System.Drawing.Size(154, 22);
			this.updateCateCommon.Text = "一般(&B)";
			// 
			// updateCateTryMenu
			// 
			this.updateCateTryMenu.Name = "updateCateTryMenu";
			this.updateCateTryMenu.Size = new System.Drawing.Size(154, 22);
			this.updateCateTryMenu.Text = "やってみた(&C)";
			// 
			// updateCatePlayMenu
			// 
			this.updateCatePlayMenu.Name = "updateCatePlayMenu";
			this.updateCatePlayMenu.Size = new System.Drawing.Size(154, 22);
			this.updateCatePlayMenu.Text = "ゲーム(&D)";
			// 
			// updateCatePresenMenu
			// 
			this.updateCatePresenMenu.Name = "updateCatePresenMenu";
			this.updateCatePresenMenu.Size = new System.Drawing.Size(154, 22);
			this.updateCatePresenMenu.Text = "動画紹介(&E)";
			// 
			// updateCateSuperIchibaMenu
			// 
			this.updateCateSuperIchibaMenu.Name = "updateCateSuperIchibaMenu";
			this.updateCateSuperIchibaMenu.Size = new System.Drawing.Size(154, 22);
			this.updateCateSuperIchibaMenu.Text = "遊ぶ(&F)";
			this.updateCateSuperIchibaMenu.Visible = false;
			// 
			// updateCateFaceMenu
			// 
			this.updateCateFaceMenu.Name = "updateCateFaceMenu";
			this.updateCateFaceMenu.Size = new System.Drawing.Size(154, 22);
			this.updateCateFaceMenu.Text = "顔出し(&G)";
			this.updateCateFaceMenu.Visible = false;
			// 
			// updateCateRushMenu
			// 
			this.updateCateRushMenu.Name = "updateCateRushMenu";
			this.updateCateRushMenu.Size = new System.Drawing.Size(154, 22);
			this.updateCateRushMenu.Text = "凸待ち(&H)";
			this.updateCateRushMenu.Visible = false;
			// 
			// updateCateR18Menu
			// 
			this.updateCateR18Menu.Name = "updateCateR18Menu";
			this.updateCateR18Menu.Size = new System.Drawing.Size(154, 22);
			this.updateCateR18Menu.Text = "R-18(&I)";
			this.updateCateR18Menu.Visible = false;
			// 
			// toolStripSeparator15
			// 
			this.toolStripSeparator15.Name = "toolStripSeparator15";
			this.toolStripSeparator15.Size = new System.Drawing.Size(320, 6);
			// 
			// updateAutoUpdateStartMenu
			// 
			this.updateAutoUpdateStartMenu.Name = "updateAutoUpdateStartMenu";
			this.updateAutoUpdateStartMenu.Size = new System.Drawing.Size(323, 22);
			this.updateAutoUpdateStartMenu.Text = "番組一覧の自動更新開始(&A)";
			this.updateAutoUpdateStartMenu.Click += new System.EventHandler(this.UpdateAutoUpdateStartMenuClick);
			// 
			// updateAutoUpdateStopMenu
			// 
			this.updateAutoUpdateStopMenu.Enabled = false;
			this.updateAutoUpdateStopMenu.Name = "updateAutoUpdateStopMenu";
			this.updateAutoUpdateStopMenu.Size = new System.Drawing.Size(323, 22);
			this.updateAutoUpdateStopMenu.Text = "番組一覧の自動更新停止(&S)";
			this.updateAutoUpdateStopMenu.Click += new System.EventHandler(this.UpdateAutoUpdateStopMenuClick);
			// 
			// updateAutoUpdateFirstMenu
			// 
			this.updateAutoUpdateFirstMenu.CheckOnClick = true;
			this.updateAutoUpdateFirstMenu.Name = "updateAutoUpdateFirstMenu";
			this.updateAutoUpdateFirstMenu.Size = new System.Drawing.Size(323, 22);
			this.updateAutoUpdateFirstMenu.Text = "起動時に自動更新を開始する(&W)";
			// 
			// toolStripSeparator16
			// 
			this.toolStripSeparator16.Name = "toolStripSeparator16";
			this.toolStripSeparator16.Size = new System.Drawing.Size(320, 6);
			// 
			// updateAutoDeleteMenu
			// 
			this.updateAutoDeleteMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.updateNoDelMenu,
									this.update5minDelMenu,
									this.update10minDelMenu,
									this.update15minDelMenu,
									this.update20minDelMenu,
									this.update30minDelMenu,
									this.update1hourDelMenu,
									this.update6hourDelMenu});
			this.updateAutoDeleteMenu.Name = "updateAutoDeleteMenu";
			this.updateAutoDeleteMenu.Size = new System.Drawing.Size(323, 22);
			this.updateAutoDeleteMenu.Text = "番組一覧の自動削除基準(&B)";
			this.updateAutoDeleteMenu.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.UpdateAutoDeleteMenuDropDownItemClicked);
			// 
			// updateNoDelMenu
			// 
			this.updateNoDelMenu.Checked = true;
			this.updateNoDelMenu.CheckState = System.Windows.Forms.CheckState.Checked;
			this.updateNoDelMenu.Name = "updateNoDelMenu";
			this.updateNoDelMenu.Size = new System.Drawing.Size(222, 22);
			this.updateNoDelMenu.Text = "自動で削除しない(&A)";
			// 
			// update5minDelMenu
			// 
			this.update5minDelMenu.Name = "update5minDelMenu";
			this.update5minDelMenu.Size = new System.Drawing.Size(222, 22);
			this.update5minDelMenu.Text = "5分以上経過した放送(&B)";
			// 
			// update10minDelMenu
			// 
			this.update10minDelMenu.Name = "update10minDelMenu";
			this.update10minDelMenu.Size = new System.Drawing.Size(222, 22);
			this.update10minDelMenu.Text = "10分以上経過した放送(&C)";
			// 
			// update15minDelMenu
			// 
			this.update15minDelMenu.Name = "update15minDelMenu";
			this.update15minDelMenu.Size = new System.Drawing.Size(222, 22);
			this.update15minDelMenu.Text = "15分以上経過した放送(&D)";
			// 
			// update20minDelMenu
			// 
			this.update20minDelMenu.Name = "update20minDelMenu";
			this.update20minDelMenu.Size = new System.Drawing.Size(222, 22);
			this.update20minDelMenu.Text = "20分以上経過した放送(&E)";
			// 
			// update30minDelMenu
			// 
			this.update30minDelMenu.Name = "update30minDelMenu";
			this.update30minDelMenu.Size = new System.Drawing.Size(222, 22);
			this.update30minDelMenu.Text = "30分以上経過した放送(&F)";
			// 
			// update1hourDelMenu
			// 
			this.update1hourDelMenu.Name = "update1hourDelMenu";
			this.update1hourDelMenu.Size = new System.Drawing.Size(222, 22);
			this.update1hourDelMenu.Text = "1時間以上経過した放送(&G)";
			// 
			// update6hourDelMenu
			// 
			this.update6hourDelMenu.Name = "update6hourDelMenu";
			this.update6hourDelMenu.Size = new System.Drawing.Size(222, 22);
			this.update6hourDelMenu.Text = "6時間以上経過した放送(&H)";
			// 
			// toolStripSeparator17
			// 
			this.toolStripSeparator17.Name = "toolStripSeparator17";
			this.toolStripSeparator17.Size = new System.Drawing.Size(320, 6);
			// 
			// updateTopFavoriteMenu
			// 
			this.updateTopFavoriteMenu.CheckOnClick = true;
			this.updateTopFavoriteMenu.Name = "updateTopFavoriteMenu";
			this.updateTopFavoriteMenu.Size = new System.Drawing.Size(323, 22);
			this.updateTopFavoriteMenu.Text = "お気に入り放送を上位に表示する(&T)";
			this.updateTopFavoriteMenu.CheckedChanged += new System.EventHandler(this.UpdateTopFavoriteMenuCheckedChanged);
			// 
			// updateOnlyFavoriteMenu
			// 
			this.updateOnlyFavoriteMenu.CheckOnClick = true;
			this.updateOnlyFavoriteMenu.Name = "updateOnlyFavoriteMenu";
			this.updateOnlyFavoriteMenu.Size = new System.Drawing.Size(323, 22);
			this.updateOnlyFavoriteMenu.Text = "お気に入り放送のみを表示する(&F)";
			this.updateOnlyFavoriteMenu.CheckedChanged += new System.EventHandler(this.UpdateOnlyFavoriteMenuCheckedChanged);
			// 
			// updateAutoSortMenu
			// 
			this.updateAutoSortMenu.CheckOnClick = true;
			this.updateAutoSortMenu.Name = "updateAutoSortMenu";
			this.updateAutoSortMenu.Size = new System.Drawing.Size(323, 22);
			this.updateAutoSortMenu.Text = "更新完了時に自動で並び変える(&S)";
			this.updateAutoSortMenu.CheckedChanged += new System.EventHandler(this.UpdateAutoSortMenuCheckedChanged);
			// 
			// updateHideMemberOnlyWithoutFavoriteMenu
			// 
			this.updateHideMemberOnlyWithoutFavoriteMenu.Name = "updateHideMemberOnlyWithoutFavoriteMenu";
			this.updateHideMemberOnlyWithoutFavoriteMenu.Size = new System.Drawing.Size(323, 22);
			this.updateHideMemberOnlyWithoutFavoriteMenu.Text = "コミュ限を非表示(お気に入りを除く)(&D)";
			this.updateHideMemberOnlyWithoutFavoriteMenu.Click += new System.EventHandler(this.UpdateHideMemberOnlyWithoutFavoriteMenuClick);
			// 
			// updateHideMemberOnlyWithFavoriteMenu
			// 
			this.updateHideMemberOnlyWithFavoriteMenu.Name = "updateHideMemberOnlyWithFavoriteMenu";
			this.updateHideMemberOnlyWithFavoriteMenu.Size = new System.Drawing.Size(323, 22);
			this.updateHideMemberOnlyWithFavoriteMenu.Text = "コミュ限を非表示(お気に入りを含む)(&N)";
			this.updateHideMemberOnlyWithFavoriteMenu.Click += new System.EventHandler(this.UpdateHideMemberOnlyWithFavoriteMenuClick);
			// 
			// updateHideQuestionCategoryMenu
			// 
			this.updateHideQuestionCategoryMenu.CheckOnClick = true;
			this.updateHideQuestionCategoryMenu.Name = "updateHideQuestionCategoryMenu";
			this.updateHideQuestionCategoryMenu.Size = new System.Drawing.Size(323, 22);
			this.updateHideQuestionCategoryMenu.Text = "？状態の番組を個別カテゴリに表示しない(&Q)";
			this.updateHideQuestionCategoryMenu.CheckedChanged += new System.EventHandler(this.UpdateHideQuestionCategoryMenuCheckedChanged);
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
									this.displayTaskTabMenu,
									this.displayOnAirTabMenu,
									this.displayHistoryListMenu,
									this.displayNotAlartListMenu,
									this.toolStripSeparator20,
									this.colorColumnMenu,
									this.colorHistoryColorColumnMenu,
									this.toolStripSeparator21,
									this.disableFollowMenu});
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
									this.isDisplayIsAndTabMenu,
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
									this.isDisplaySoundTypeTabMenu,
									this.isDisplayMemoTabMenu});
			this.displayFavoriteTabMenu.Name = "displayFavoriteTabMenu";
			this.displayFavoriteTabMenu.Size = new System.Drawing.Size(220, 22);
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
			// isDisplayIsAndTabMenu
			// 
			this.isDisplayIsAndTabMenu.Name = "isDisplayIsAndTabMenu";
			this.isDisplayIsAndTabMenu.Size = new System.Drawing.Size(196, 22);
			this.isDisplayIsAndTabMenu.Text = "合致条件";
			this.isDisplayIsAndTabMenu.Visible = false;
			this.isDisplayIsAndTabMenu.Click += new System.EventHandler(this.IsAlartListDisplayTabMenuClick);
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
			// isDisplaySoundTypeTabMenu
			// 
			this.isDisplaySoundTypeTabMenu.Name = "isDisplaySoundTypeTabMenu";
			this.isDisplaySoundTypeTabMenu.Size = new System.Drawing.Size(196, 22);
			this.isDisplaySoundTypeTabMenu.Text = "音設定";
			this.isDisplaySoundTypeTabMenu.Click += new System.EventHandler(this.IsAlartListDisplayTabMenuClick);
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
			this.displayTaskTabMenu.Size = new System.Drawing.Size(220, 22);
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
			// displayOnAirTabMenu
			// 
			this.displayOnAirTabMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.isDisplayThumbnailOnAirTabMenu,
									this.isDisplayTitleOnAirTabMenu,
									this.isDisplayHostNameOnAirTabMenu,
									this.isDisplayCommunityNameOnAirTabMenu,
									this.isDisplayDescriptionOnAirTabMenu,
									this.isDisplayLvidOnAirTabMenu,
									this.isDisplayCommunityIDOnAirTabMenu,
									this.isDisplayElapsedTimeOnAirTabMenu,
									this.isDisplayCategoryOnAirTabMenu,
									this.isDisplayFaceOnAirTabMenu,
									this.isDisplayRushOnAirTabMenu,
									this.isDisplayCruiseOnAirTabMenu,
									this.isDisplayCasOnAirTabMenu,
									this.isDisplayMemberOnlyOnAirTabMenu,
									this.isDisplayTypeOnAirTabMenu,
									this.isDisplayFavoriteOnAirTabMenu,
									this.isDisplayMemoOnAirTabMenu});
			this.displayOnAirTabMenu.Name = "displayOnAirTabMenu";
			this.displayOnAirTabMenu.Size = new System.Drawing.Size(220, 22);
			this.displayOnAirTabMenu.Text = "放送中タブ";
			this.displayOnAirTabMenu.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.DisplayOnAirTabMenuDropDownItemClicked);
			// 
			// isDisplayThumbnailOnAirTabMenu
			// 
			this.isDisplayThumbnailOnAirTabMenu.Name = "isDisplayThumbnailOnAirTabMenu";
			this.isDisplayThumbnailOnAirTabMenu.Size = new System.Drawing.Size(162, 22);
			this.isDisplayThumbnailOnAirTabMenu.Text = "サムネ";
			// 
			// isDisplayTitleOnAirTabMenu
			// 
			this.isDisplayTitleOnAirTabMenu.Name = "isDisplayTitleOnAirTabMenu";
			this.isDisplayTitleOnAirTabMenu.Size = new System.Drawing.Size(162, 22);
			this.isDisplayTitleOnAirTabMenu.Text = "放送タイトル";
			// 
			// isDisplayHostNameOnAirTabMenu
			// 
			this.isDisplayHostNameOnAirTabMenu.Name = "isDisplayHostNameOnAirTabMenu";
			this.isDisplayHostNameOnAirTabMenu.Size = new System.Drawing.Size(162, 22);
			this.isDisplayHostNameOnAirTabMenu.Text = "放送者";
			// 
			// isDisplayCommunityNameOnAirTabMenu
			// 
			this.isDisplayCommunityNameOnAirTabMenu.Name = "isDisplayCommunityNameOnAirTabMenu";
			this.isDisplayCommunityNameOnAirTabMenu.Size = new System.Drawing.Size(162, 22);
			this.isDisplayCommunityNameOnAirTabMenu.Text = "コミュニティ名";
			// 
			// isDisplayDescriptionOnAirTabMenu
			// 
			this.isDisplayDescriptionOnAirTabMenu.Name = "isDisplayDescriptionOnAirTabMenu";
			this.isDisplayDescriptionOnAirTabMenu.Size = new System.Drawing.Size(162, 22);
			this.isDisplayDescriptionOnAirTabMenu.Text = "説明";
			// 
			// isDisplayLvidOnAirTabMenu
			// 
			this.isDisplayLvidOnAirTabMenu.Name = "isDisplayLvidOnAirTabMenu";
			this.isDisplayLvidOnAirTabMenu.Size = new System.Drawing.Size(162, 22);
			this.isDisplayLvidOnAirTabMenu.Text = "放送ID";
			// 
			// isDisplayCommunityIDOnAirTabMenu
			// 
			this.isDisplayCommunityIDOnAirTabMenu.Name = "isDisplayCommunityIDOnAirTabMenu";
			this.isDisplayCommunityIDOnAirTabMenu.Size = new System.Drawing.Size(162, 22);
			this.isDisplayCommunityIDOnAirTabMenu.Text = "コミュニティID";
			// 
			// isDisplayElapsedTimeOnAirTabMenu
			// 
			this.isDisplayElapsedTimeOnAirTabMenu.Name = "isDisplayElapsedTimeOnAirTabMenu";
			this.isDisplayElapsedTimeOnAirTabMenu.Size = new System.Drawing.Size(162, 22);
			this.isDisplayElapsedTimeOnAirTabMenu.Text = "放送時間";
			// 
			// isDisplayCategoryOnAirTabMenu
			// 
			this.isDisplayCategoryOnAirTabMenu.Name = "isDisplayCategoryOnAirTabMenu";
			this.isDisplayCategoryOnAirTabMenu.Size = new System.Drawing.Size(162, 22);
			this.isDisplayCategoryOnAirTabMenu.Text = "カテゴリー";
			// 
			// isDisplayFaceOnAirTabMenu
			// 
			this.isDisplayFaceOnAirTabMenu.Name = "isDisplayFaceOnAirTabMenu";
			this.isDisplayFaceOnAirTabMenu.Size = new System.Drawing.Size(162, 22);
			this.isDisplayFaceOnAirTabMenu.Text = "顔";
			// 
			// isDisplayRushOnAirTabMenu
			// 
			this.isDisplayRushOnAirTabMenu.Name = "isDisplayRushOnAirTabMenu";
			this.isDisplayRushOnAirTabMenu.Size = new System.Drawing.Size(162, 22);
			this.isDisplayRushOnAirTabMenu.Text = "凸";
			// 
			// isDisplayCruiseOnAirTabMenu
			// 
			this.isDisplayCruiseOnAirTabMenu.Name = "isDisplayCruiseOnAirTabMenu";
			this.isDisplayCruiseOnAirTabMenu.Size = new System.Drawing.Size(162, 22);
			this.isDisplayCruiseOnAirTabMenu.Text = "クルーズ";
			// 
			// isDisplayCasOnAirTabMenu
			// 
			this.isDisplayCasOnAirTabMenu.Name = "isDisplayCasOnAirTabMenu";
			this.isDisplayCasOnAirTabMenu.Size = new System.Drawing.Size(162, 22);
			this.isDisplayCasOnAirTabMenu.Text = "実験";
			// 
			// isDisplayMemberOnlyOnAirTabMenu
			// 
			this.isDisplayMemberOnlyOnAirTabMenu.Name = "isDisplayMemberOnlyOnAirTabMenu";
			this.isDisplayMemberOnlyOnAirTabMenu.Size = new System.Drawing.Size(162, 22);
			this.isDisplayMemberOnlyOnAirTabMenu.Text = "限定";
			// 
			// isDisplayTypeOnAirTabMenu
			// 
			this.isDisplayTypeOnAirTabMenu.Name = "isDisplayTypeOnAirTabMenu";
			this.isDisplayTypeOnAirTabMenu.Size = new System.Drawing.Size(162, 22);
			this.isDisplayTypeOnAirTabMenu.Text = "種類";
			// 
			// isDisplayFavoriteOnAirTabMenu
			// 
			this.isDisplayFavoriteOnAirTabMenu.Name = "isDisplayFavoriteOnAirTabMenu";
			this.isDisplayFavoriteOnAirTabMenu.Size = new System.Drawing.Size(162, 22);
			this.isDisplayFavoriteOnAirTabMenu.Text = "お気に入り";
			// 
			// isDisplayMemoOnAirTabMenu
			// 
			this.isDisplayMemoOnAirTabMenu.Name = "isDisplayMemoOnAirTabMenu";
			this.isDisplayMemoOnAirTabMenu.Size = new System.Drawing.Size(162, 22);
			this.isDisplayMemoOnAirTabMenu.Text = "メモ";
			// 
			// displayHistoryListMenu
			// 
			this.displayHistoryListMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.toolStripMenuItem11,
									this.toolStripMenuItem12,
									this.toolStripMenuItem13,
									this.toolStripMenuItem14,
									this.toolStripMenuItem15,
									this.toolStripMenuItem16,
									this.toolStripMenuItem17,
									this.toolStripMenuItem18,
									this.toolStripMenuItem19,
									this.toolStripMenuItem20});
			this.displayHistoryListMenu.Name = "displayHistoryListMenu";
			this.displayHistoryListMenu.Size = new System.Drawing.Size(220, 22);
			this.displayHistoryListMenu.Text = "通知履歴";
			this.displayHistoryListMenu.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.DisplayHistoryListMenuDropDownItemClicked);
			// 
			// toolStripMenuItem11
			// 
			this.toolStripMenuItem11.Name = "toolStripMenuItem11";
			this.toolStripMenuItem11.Size = new System.Drawing.Size(162, 22);
			this.toolStripMenuItem11.Text = "放送開始日時";
			// 
			// toolStripMenuItem12
			// 
			this.toolStripMenuItem12.Name = "toolStripMenuItem12";
			this.toolStripMenuItem12.Size = new System.Drawing.Size(162, 22);
			this.toolStripMenuItem12.Text = "放送タイトル";
			// 
			// toolStripMenuItem13
			// 
			this.toolStripMenuItem13.Name = "toolStripMenuItem13";
			this.toolStripMenuItem13.Size = new System.Drawing.Size(162, 22);
			this.toolStripMenuItem13.Text = "放送者";
			// 
			// toolStripMenuItem14
			// 
			this.toolStripMenuItem14.Name = "toolStripMenuItem14";
			this.toolStripMenuItem14.Size = new System.Drawing.Size(162, 22);
			this.toolStripMenuItem14.Text = "コミュニティ名";
			// 
			// toolStripMenuItem15
			// 
			this.toolStripMenuItem15.Name = "toolStripMenuItem15";
			this.toolStripMenuItem15.Size = new System.Drawing.Size(162, 22);
			this.toolStripMenuItem15.Text = "放送ID";
			// 
			// toolStripMenuItem16
			// 
			this.toolStripMenuItem16.Name = "toolStripMenuItem16";
			this.toolStripMenuItem16.Size = new System.Drawing.Size(162, 22);
			this.toolStripMenuItem16.Text = "ユーザーID";
			// 
			// toolStripMenuItem17
			// 
			this.toolStripMenuItem17.Name = "toolStripMenuItem17";
			this.toolStripMenuItem17.Size = new System.Drawing.Size(162, 22);
			this.toolStripMenuItem17.Text = "コミュニティID";
			// 
			// toolStripMenuItem18
			// 
			this.toolStripMenuItem18.Name = "toolStripMenuItem18";
			this.toolStripMenuItem18.Size = new System.Drawing.Size(162, 22);
			this.toolStripMenuItem18.Text = "キーワード";
			this.toolStripMenuItem18.Visible = false;
			// 
			// toolStripMenuItem19
			// 
			this.toolStripMenuItem19.Name = "toolStripMenuItem19";
			this.toolStripMenuItem19.Size = new System.Drawing.Size(162, 22);
			this.toolStripMenuItem19.Text = "お気に入り";
			// 
			// toolStripMenuItem20
			// 
			this.toolStripMenuItem20.Name = "toolStripMenuItem20";
			this.toolStripMenuItem20.Size = new System.Drawing.Size(162, 22);
			this.toolStripMenuItem20.Text = "説明";
			// 
			// displayNotAlartListMenu
			// 
			this.displayNotAlartListMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.displayHistoryDtMenu,
									this.toolStripMenuItem2,
									this.toolStripMenuItem3,
									this.toolStripMenuItem4,
									this.toolStripMenuItem5,
									this.toolStripMenuItem6,
									this.toolStripMenuItem7,
									this.toolStripMenuItem1,
									this.toolStripMenuItem8,
									this.toolStripMenuItem9});
			this.displayNotAlartListMenu.Name = "displayNotAlartListMenu";
			this.displayNotAlartListMenu.Size = new System.Drawing.Size(220, 22);
			this.displayNotAlartListMenu.Text = "通知しなかった履歴";
			this.displayNotAlartListMenu.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.DisplayNotAlartListMenuDropDownItemClicked);
			// 
			// displayHistoryDtMenu
			// 
			this.displayHistoryDtMenu.Name = "displayHistoryDtMenu";
			this.displayHistoryDtMenu.Size = new System.Drawing.Size(162, 22);
			this.displayHistoryDtMenu.Text = "放送開始日時";
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(162, 22);
			this.toolStripMenuItem2.Text = "放送タイトル";
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(162, 22);
			this.toolStripMenuItem3.Text = "放送者";
			// 
			// toolStripMenuItem4
			// 
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new System.Drawing.Size(162, 22);
			this.toolStripMenuItem4.Text = "コミュニティ名";
			// 
			// toolStripMenuItem5
			// 
			this.toolStripMenuItem5.Name = "toolStripMenuItem5";
			this.toolStripMenuItem5.Size = new System.Drawing.Size(162, 22);
			this.toolStripMenuItem5.Text = "放送ID";
			// 
			// toolStripMenuItem6
			// 
			this.toolStripMenuItem6.Name = "toolStripMenuItem6";
			this.toolStripMenuItem6.Size = new System.Drawing.Size(162, 22);
			this.toolStripMenuItem6.Text = "ユーザーID";
			// 
			// toolStripMenuItem7
			// 
			this.toolStripMenuItem7.Name = "toolStripMenuItem7";
			this.toolStripMenuItem7.Size = new System.Drawing.Size(162, 22);
			this.toolStripMenuItem7.Text = "コミュニティID";
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(162, 22);
			this.toolStripMenuItem1.Text = "キーワード";
			// 
			// toolStripMenuItem8
			// 
			this.toolStripMenuItem8.Name = "toolStripMenuItem8";
			this.toolStripMenuItem8.Size = new System.Drawing.Size(162, 22);
			this.toolStripMenuItem8.Text = "お気に入り";
			// 
			// toolStripMenuItem9
			// 
			this.toolStripMenuItem9.Name = "toolStripMenuItem9";
			this.toolStripMenuItem9.Size = new System.Drawing.Size(162, 22);
			this.toolStripMenuItem9.Text = "説明";
			// 
			// toolStripSeparator20
			// 
			this.toolStripSeparator20.Name = "toolStripSeparator20";
			this.toolStripSeparator20.Size = new System.Drawing.Size(217, 6);
			// 
			// colorColumnMenu
			// 
			this.colorColumnMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.colorCommunityIdMenu,
									this.colorUserIdMenu,
									this.colorCommunityNameMenu,
									this.colorUserNameMenu,
									this.colorKeywordMenu,
									this.colorIsAndMenu,
									this.colorComFollowMenu,
									this.colorUserFollowMenu,
									this.colorRecentLiveDtMenu,
									this.colorAddDtMenu,
									this.colorPopupMenu,
									this.colorBaloonMenu,
									this.colorWebMenu,
									this.colorMailMenu,
									this.colorSoundMenu,
									this.colorAppliAMenu,
									this.colorAppliBMenu,
									this.colorAppliCMenu,
									this.colorAppliDMenu,
									this.colorAppliEMenu,
									this.colorAppliFMenu,
									this.colorAppliGMenu,
									this.colorAppliHMenu,
									this.colorAppliIMenu,
									this.colorAppliJMenu,
									this.colorSountTypeMenu,
									this.colorMemoMenu});
			this.colorColumnMenu.Name = "colorColumnMenu";
			this.colorColumnMenu.Size = new System.Drawing.Size(220, 22);
			this.colorColumnMenu.Text = "お気に入りリストの色列";
			this.colorColumnMenu.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ColorColumnMenuDropDownItemClicked);
			// 
			// colorCommunityIdMenu
			// 
			this.colorCommunityIdMenu.Name = "colorCommunityIdMenu";
			this.colorCommunityIdMenu.Size = new System.Drawing.Size(196, 22);
			this.colorCommunityIdMenu.Text = "コミュニティID";
			// 
			// colorUserIdMenu
			// 
			this.colorUserIdMenu.Name = "colorUserIdMenu";
			this.colorUserIdMenu.Size = new System.Drawing.Size(196, 22);
			this.colorUserIdMenu.Text = "ユーザーID";
			// 
			// colorCommunityNameMenu
			// 
			this.colorCommunityNameMenu.Name = "colorCommunityNameMenu";
			this.colorCommunityNameMenu.Size = new System.Drawing.Size(196, 22);
			this.colorCommunityNameMenu.Text = "コミュニティ名";
			// 
			// colorUserNameMenu
			// 
			this.colorUserNameMenu.Name = "colorUserNameMenu";
			this.colorUserNameMenu.Size = new System.Drawing.Size(196, 22);
			this.colorUserNameMenu.Text = "ユーザー名";
			// 
			// colorKeywordMenu
			// 
			this.colorKeywordMenu.Name = "colorKeywordMenu";
			this.colorKeywordMenu.Size = new System.Drawing.Size(196, 22);
			this.colorKeywordMenu.Text = "キーワード";
			// 
			// colorIsAndMenu
			// 
			this.colorIsAndMenu.Name = "colorIsAndMenu";
			this.colorIsAndMenu.Size = new System.Drawing.Size(196, 22);
			this.colorIsAndMenu.Text = "合致条件";
			this.colorIsAndMenu.Visible = false;
			// 
			// colorComFollowMenu
			// 
			this.colorComFollowMenu.Name = "colorComFollowMenu";
			this.colorComFollowMenu.Size = new System.Drawing.Size(196, 22);
			this.colorComFollowMenu.Text = "コミュニティフォロー";
			this.colorComFollowMenu.Visible = false;
			// 
			// colorUserFollowMenu
			// 
			this.colorUserFollowMenu.Name = "colorUserFollowMenu";
			this.colorUserFollowMenu.Size = new System.Drawing.Size(196, 22);
			this.colorUserFollowMenu.Text = "ユーザーフォロー";
			this.colorUserFollowMenu.Visible = false;
			// 
			// colorRecentLiveDtMenu
			// 
			this.colorRecentLiveDtMenu.Name = "colorRecentLiveDtMenu";
			this.colorRecentLiveDtMenu.Size = new System.Drawing.Size(196, 22);
			this.colorRecentLiveDtMenu.Text = "最近の放送日時";
			this.colorRecentLiveDtMenu.Visible = false;
			// 
			// colorAddDtMenu
			// 
			this.colorAddDtMenu.Name = "colorAddDtMenu";
			this.colorAddDtMenu.Size = new System.Drawing.Size(196, 22);
			this.colorAddDtMenu.Text = "登録日時";
			// 
			// colorPopupMenu
			// 
			this.colorPopupMenu.Name = "colorPopupMenu";
			this.colorPopupMenu.Size = new System.Drawing.Size(196, 22);
			this.colorPopupMenu.Text = "ポップアップ";
			// 
			// colorBaloonMenu
			// 
			this.colorBaloonMenu.Name = "colorBaloonMenu";
			this.colorBaloonMenu.Size = new System.Drawing.Size(196, 22);
			this.colorBaloonMenu.Text = "バルーン";
			// 
			// colorWebMenu
			// 
			this.colorWebMenu.Name = "colorWebMenu";
			this.colorWebMenu.Size = new System.Drawing.Size(196, 22);
			this.colorWebMenu.Text = "Web";
			// 
			// colorMailMenu
			// 
			this.colorMailMenu.Name = "colorMailMenu";
			this.colorMailMenu.Size = new System.Drawing.Size(196, 22);
			this.colorMailMenu.Text = "メール";
			// 
			// colorSoundMenu
			// 
			this.colorSoundMenu.Name = "colorSoundMenu";
			this.colorSoundMenu.Size = new System.Drawing.Size(196, 22);
			this.colorSoundMenu.Text = "音";
			// 
			// colorAppliAMenu
			// 
			this.colorAppliAMenu.Name = "colorAppliAMenu";
			this.colorAppliAMenu.Size = new System.Drawing.Size(196, 22);
			this.colorAppliAMenu.Text = "アプリA";
			// 
			// colorAppliBMenu
			// 
			this.colorAppliBMenu.Name = "colorAppliBMenu";
			this.colorAppliBMenu.Size = new System.Drawing.Size(196, 22);
			this.colorAppliBMenu.Text = "アプリB";
			// 
			// colorAppliCMenu
			// 
			this.colorAppliCMenu.Name = "colorAppliCMenu";
			this.colorAppliCMenu.Size = new System.Drawing.Size(196, 22);
			this.colorAppliCMenu.Text = "アプリC";
			// 
			// colorAppliDMenu
			// 
			this.colorAppliDMenu.Name = "colorAppliDMenu";
			this.colorAppliDMenu.Size = new System.Drawing.Size(196, 22);
			this.colorAppliDMenu.Text = "アプリD";
			// 
			// colorAppliEMenu
			// 
			this.colorAppliEMenu.Name = "colorAppliEMenu";
			this.colorAppliEMenu.Size = new System.Drawing.Size(196, 22);
			this.colorAppliEMenu.Text = "アプリE";
			// 
			// colorAppliFMenu
			// 
			this.colorAppliFMenu.Name = "colorAppliFMenu";
			this.colorAppliFMenu.Size = new System.Drawing.Size(196, 22);
			this.colorAppliFMenu.Text = "アプリF";
			// 
			// colorAppliGMenu
			// 
			this.colorAppliGMenu.Name = "colorAppliGMenu";
			this.colorAppliGMenu.Size = new System.Drawing.Size(196, 22);
			this.colorAppliGMenu.Text = "アプリG";
			// 
			// colorAppliHMenu
			// 
			this.colorAppliHMenu.Name = "colorAppliHMenu";
			this.colorAppliHMenu.Size = new System.Drawing.Size(196, 22);
			this.colorAppliHMenu.Text = "アプリH";
			// 
			// colorAppliIMenu
			// 
			this.colorAppliIMenu.Name = "colorAppliIMenu";
			this.colorAppliIMenu.Size = new System.Drawing.Size(196, 22);
			this.colorAppliIMenu.Text = "アプリI";
			// 
			// colorAppliJMenu
			// 
			this.colorAppliJMenu.Name = "colorAppliJMenu";
			this.colorAppliJMenu.Size = new System.Drawing.Size(196, 22);
			this.colorAppliJMenu.Text = "アプリJ";
			// 
			// colorSountTypeMenu
			// 
			this.colorSountTypeMenu.Name = "colorSountTypeMenu";
			this.colorSountTypeMenu.Size = new System.Drawing.Size(196, 22);
			this.colorSountTypeMenu.Text = "色設定";
			this.colorSountTypeMenu.Visible = false;
			// 
			// colorMemoMenu
			// 
			this.colorMemoMenu.Name = "colorMemoMenu";
			this.colorMemoMenu.Size = new System.Drawing.Size(196, 22);
			this.colorMemoMenu.Text = "メモ";
			// 
			// colorHistoryColorColumnMenu
			// 
			this.colorHistoryColorColumnMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.toolStripMenuItem21,
									this.toolStripMenuItem22,
									this.toolStripMenuItem23,
									this.toolStripMenuItem24,
									this.toolStripMenuItem25,
									this.toolStripMenuItem26,
									this.toolStripMenuItem27,
									this.toolStripMenuItem28,
									this.toolStripMenuItem29,
									this.toolStripMenuItem30});
			this.colorHistoryColorColumnMenu.Name = "colorHistoryColorColumnMenu";
			this.colorHistoryColorColumnMenu.Size = new System.Drawing.Size(220, 22);
			this.colorHistoryColorColumnMenu.Text = "通知履歴の放送中の色列";
			this.colorHistoryColorColumnMenu.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ColorHistoryColorColumnMenuDropDownItemClicked);
			// 
			// toolStripMenuItem21
			// 
			this.toolStripMenuItem21.Name = "toolStripMenuItem21";
			this.toolStripMenuItem21.Size = new System.Drawing.Size(162, 22);
			this.toolStripMenuItem21.Text = "放送開始日時";
			// 
			// toolStripMenuItem22
			// 
			this.toolStripMenuItem22.Name = "toolStripMenuItem22";
			this.toolStripMenuItem22.Size = new System.Drawing.Size(162, 22);
			this.toolStripMenuItem22.Text = "放送タイトル";
			// 
			// toolStripMenuItem23
			// 
			this.toolStripMenuItem23.Name = "toolStripMenuItem23";
			this.toolStripMenuItem23.Size = new System.Drawing.Size(162, 22);
			this.toolStripMenuItem23.Text = "放送者";
			// 
			// toolStripMenuItem24
			// 
			this.toolStripMenuItem24.Name = "toolStripMenuItem24";
			this.toolStripMenuItem24.Size = new System.Drawing.Size(162, 22);
			this.toolStripMenuItem24.Text = "コミュニティ名";
			// 
			// toolStripMenuItem25
			// 
			this.toolStripMenuItem25.Name = "toolStripMenuItem25";
			this.toolStripMenuItem25.Size = new System.Drawing.Size(162, 22);
			this.toolStripMenuItem25.Text = "放送ID";
			// 
			// toolStripMenuItem26
			// 
			this.toolStripMenuItem26.Name = "toolStripMenuItem26";
			this.toolStripMenuItem26.Size = new System.Drawing.Size(162, 22);
			this.toolStripMenuItem26.Text = "ユーザーID";
			// 
			// toolStripMenuItem27
			// 
			this.toolStripMenuItem27.Name = "toolStripMenuItem27";
			this.toolStripMenuItem27.Size = new System.Drawing.Size(162, 22);
			this.toolStripMenuItem27.Text = "コミュニティID";
			// 
			// toolStripMenuItem28
			// 
			this.toolStripMenuItem28.Name = "toolStripMenuItem28";
			this.toolStripMenuItem28.Size = new System.Drawing.Size(162, 22);
			this.toolStripMenuItem28.Text = "キーワード";
			this.toolStripMenuItem28.Visible = false;
			// 
			// toolStripMenuItem29
			// 
			this.toolStripMenuItem29.Name = "toolStripMenuItem29";
			this.toolStripMenuItem29.Size = new System.Drawing.Size(162, 22);
			this.toolStripMenuItem29.Text = "お気に入り";
			// 
			// toolStripMenuItem30
			// 
			this.toolStripMenuItem30.Name = "toolStripMenuItem30";
			this.toolStripMenuItem30.Size = new System.Drawing.Size(162, 22);
			this.toolStripMenuItem30.Text = "説明";
			// 
			// toolStripSeparator21
			// 
			this.toolStripSeparator21.Name = "toolStripSeparator21";
			this.toolStripSeparator21.Size = new System.Drawing.Size(217, 6);
			// 
			// disableFollowMenu
			// 
			this.disableFollowMenu.CheckOnClick = true;
			this.disableFollowMenu.Name = "disableFollowMenu";
			this.disableFollowMenu.Size = new System.Drawing.Size(220, 22);
			this.disableFollowMenu.Text = "フォローの列を無効にする";
			this.disableFollowMenu.CheckedChanged += new System.EventHandler(this.DisableFollowMenuCheckedChanged);
			// 
			// toolMenuItem
			// 
			this.toolMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.checkExistsComMenu,
									this.checkExistsUserMenu,
									this.toolStripSeparator18,
									this.getUserInfoFromComMenu,
									this.getComThumbBulkMenu,
									this.getUserThumbBulkMenu,
									this.toolStripSeparator19,
									this.bulkUserFollowMenu,
									this.bulkCommunityFollowMenu,
									this.bulkChannelFollowMenu,
									this.toolStripSeparator9,
									this.duplicateCheckMenu,
									this.toolStripSeparator29,
									this.optionMenuItem});
			this.toolMenuItem.Name = "toolMenuItem";
			this.toolMenuItem.ShowShortcutKeys = false;
			this.toolMenuItem.Size = new System.Drawing.Size(74, 22);
			this.toolMenuItem.Text = "ツール(&T)";
			// 
			// checkExistsComMenu
			// 
			this.checkExistsComMenu.Name = "checkExistsComMenu";
			this.checkExistsComMenu.Size = new System.Drawing.Size(358, 22);
			this.checkExistsComMenu.Text = "コミュニティ存在チェック＋未取得コミュ名取得(&C)";
			this.checkExistsComMenu.Click += new System.EventHandler(this.CheckExistsComMenuClick);
			// 
			// checkExistsUserMenu
			// 
			this.checkExistsUserMenu.Name = "checkExistsUserMenu";
			this.checkExistsUserMenu.Size = new System.Drawing.Size(358, 22);
			this.checkExistsUserMenu.Text = "ユーザー存在チェック＋未取得ユーザー名取得(&U)";
			this.checkExistsUserMenu.Click += new System.EventHandler(this.CheckExistsUserMenuClick);
			// 
			// toolStripSeparator18
			// 
			this.toolStripSeparator18.Name = "toolStripSeparator18";
			this.toolStripSeparator18.Size = new System.Drawing.Size(355, 6);
			// 
			// getUserInfoFromComMenu
			// 
			this.getUserInfoFromComMenu.Name = "getUserInfoFromComMenu";
			this.getUserInfoFromComMenu.Size = new System.Drawing.Size(358, 22);
			this.getUserInfoFromComMenu.Text = "コミュの最新放送からユーザ情報を一括取得(&V)";
			this.getUserInfoFromComMenu.Click += new System.EventHandler(this.GetUserInfoFromComMenuClick);
			// 
			// getComThumbBulkMenu
			// 
			this.getComThumbBulkMenu.Name = "getComThumbBulkMenu";
			this.getComThumbBulkMenu.Size = new System.Drawing.Size(358, 22);
			this.getComThumbBulkMenu.Text = "未取得コミュ画一括取得(&G)";
			this.getComThumbBulkMenu.Click += new System.EventHandler(this.GetComThumbBulkMenuClick);
			// 
			// getUserThumbBulkMenu
			// 
			this.getUserThumbBulkMenu.Name = "getUserThumbBulkMenu";
			this.getUserThumbBulkMenu.Size = new System.Drawing.Size(358, 22);
			this.getUserThumbBulkMenu.Text = "未取得ユーザ画一括取得(&H)";
			this.getUserThumbBulkMenu.Click += new System.EventHandler(this.GetUserThumbBulkMenuClick);
			// 
			// toolStripSeparator19
			// 
			this.toolStripSeparator19.Name = "toolStripSeparator19";
			this.toolStripSeparator19.Size = new System.Drawing.Size(355, 6);
			// 
			// bulkUserFollowMenu
			// 
			this.bulkUserFollowMenu.Name = "bulkUserFollowMenu";
			this.bulkUserFollowMenu.Size = new System.Drawing.Size(358, 22);
			this.bulkUserFollowMenu.Text = "お気に入りユーザーを一括フォロー";
			this.bulkUserFollowMenu.Click += new System.EventHandler(this.BulkUserFollowMenuClick);
			// 
			// bulkCommunityFollowMenu
			// 
			this.bulkCommunityFollowMenu.Name = "bulkCommunityFollowMenu";
			this.bulkCommunityFollowMenu.Size = new System.Drawing.Size(358, 22);
			this.bulkCommunityFollowMenu.Text = "お気に入りコミュニティを一括フォロー";
			this.bulkCommunityFollowMenu.Click += new System.EventHandler(this.BulkCommunityFollowMenuClick);
			// 
			// bulkChannelFollowMenu
			// 
			this.bulkChannelFollowMenu.Name = "bulkChannelFollowMenu";
			this.bulkChannelFollowMenu.Size = new System.Drawing.Size(358, 22);
			this.bulkChannelFollowMenu.Text = "お気に入りチャンネルを一括フォロー";
			this.bulkChannelFollowMenu.Click += new System.EventHandler(this.BulkChannelFollowMenuClick);
			// 
			// toolStripSeparator9
			// 
			this.toolStripSeparator9.Name = "toolStripSeparator9";
			this.toolStripSeparator9.Size = new System.Drawing.Size(355, 6);
			// 
			// duplicateCheckMenu
			// 
			this.duplicateCheckMenu.Name = "duplicateCheckMenu";
			this.duplicateCheckMenu.Size = new System.Drawing.Size(358, 22);
			this.duplicateCheckMenu.Text = "重複チェック";
			this.duplicateCheckMenu.Click += new System.EventHandler(this.DuplicateCheckMenuClick);
			// 
			// toolStripSeparator29
			// 
			this.toolStripSeparator29.Name = "toolStripSeparator29";
			this.toolStripSeparator29.Size = new System.Drawing.Size(355, 6);
			// 
			// optionMenuItem
			// 
			this.optionMenuItem.Name = "optionMenuItem";
			this.optionMenuItem.ShowShortcutKeys = false;
			this.optionMenuItem.Size = new System.Drawing.Size(358, 22);
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
									this.lastHosoStatusBar,
									this.existCheckStatusBar,
									this.liveListUpdateStatusBar,
									this.toolStripStatusLabel1});
			this.statusStrip1.Location = new System.Drawing.Point(0, 420);
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
			// existCheckStatusBar
			// 
			this.existCheckStatusBar.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
			this.existCheckStatusBar.Name = "existCheckStatusBar";
			this.existCheckStatusBar.Size = new System.Drawing.Size(4, 17);
			// 
			// liveListUpdateStatusBar
			// 
			this.liveListUpdateStatusBar.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
			this.liveListUpdateStatusBar.Name = "liveListUpdateStatusBar";
			this.liveListUpdateStatusBar.Size = new System.Drawing.Size(4, 17);
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(4, 17);
			// 
			// addBtn
			// 
			this.addBtn.Location = new System.Drawing.Point(6, 8);
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
									this.合致条件,
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
									this.音設定,
									this.comment});
			this.alartList.ContextMenuStrip = this.contextMenuStrip1;
			this.alartList.Location = new System.Drawing.Point(6, 50);
			this.alartList.Name = "alartList";
			this.alartList.RowHeadersVisible = false;
			this.alartList.RowTemplate.Height = 21;
			this.alartList.Size = new System.Drawing.Size(864, 276);
			this.alartList.TabIndex = 23;
			this.alartList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.AlartListCellClick);
			this.alartList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.AlartListCellFormatting);
			this.alartList.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.AlartListCellMouseDoubleClick);
			this.alartList.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.AlartListCellMouseDown);
			this.alartList.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.AlartListCellParsing);
			this.alartList.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.AlartListCellValidating);
			this.alartList.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.AlartListColumnHeaderMouseClick);
			this.alartList.CurrentCellDirtyStateChanged += new System.EventHandler(this.AlartListCurrentCellDirtyStateChanged);
			this.alartList.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.AlartListRowEnter);
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
			this.ｺﾐｭﾆﾃｨID.ReadOnly = true;
			this.ｺﾐｭﾆﾃｨID.Width = 77;
			// 
			// ﾕｰｻﾞｰID
			// 
			this.ﾕｰｻﾞｰID.DataPropertyName = "hostId";
			this.ﾕｰｻﾞｰID.HeaderText = "ﾕｰｻﾞｰID";
			this.ﾕｰｻﾞｰID.Name = "ﾕｰｻﾞｰID";
			this.ﾕｰｻﾞｰID.ReadOnly = true;
			// 
			// ｺﾐｭﾆﾃｨ名
			// 
			this.ｺﾐｭﾆﾃｨ名.DataPropertyName = "communityName";
			this.ｺﾐｭﾆﾃｨ名.HeaderText = "ｺﾐｭﾆﾃｨ名";
			this.ｺﾐｭﾆﾃｨ名.Name = "ｺﾐｭﾆﾃｨ名";
			this.ｺﾐｭﾆﾃｨ名.ReadOnly = true;
			// 
			// ﾕｰｻﾞｰ名
			// 
			this.ﾕｰｻﾞｰ名.DataPropertyName = "hostName";
			this.ﾕｰｻﾞｰ名.HeaderText = "ﾕｰｻﾞｰ名";
			this.ﾕｰｻﾞｰ名.Name = "ﾕｰｻﾞｰ名";
			this.ﾕｰｻﾞｰ名.ReadOnly = true;
			// 
			// ｷｰﾜｰﾄﾞ
			// 
			this.ｷｰﾜｰﾄﾞ.DataPropertyName = "keyword";
			this.ｷｰﾜｰﾄﾞ.HeaderText = "ｷｰﾜｰﾄﾞ";
			this.ｷｰﾜｰﾄﾞ.Name = "ｷｰﾜｰﾄﾞ";
			// 
			// 合致条件
			// 
			this.合致条件.DataPropertyName = "isAnd";
			this.合致条件.HeaderText = "合致条件";
			this.合致条件.Items.AddRange(new object[] {
									"全て合致",
									"いずれか"});
			this.合致条件.Name = "合致条件";
			this.合致条件.Visible = false;
			this.合致条件.Width = 70;
			// 
			// ｺﾐｭﾆﾃｨﾌｫﾛｰ
			// 
			this.ｺﾐｭﾆﾃｨﾌｫﾛｰ.DataPropertyName = "communityFollow";
			this.ｺﾐｭﾆﾃｨﾌｫﾛｰ.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.ｺﾐｭﾆﾃｨﾌｫﾛｰ.HeaderText = "ｺﾐｭﾆﾃｨﾌｫﾛｰ";
			this.ｺﾐｭﾆﾃｨﾌｫﾛｰ.Name = "ｺﾐｭﾆﾃｨﾌｫﾛｰ";
			// 
			// ﾕｰｻﾞｰﾌｫﾛｰ
			// 
			this.ﾕｰｻﾞｰﾌｫﾛｰ.DataPropertyName = "hostFollow";
			this.ﾕｰｻﾞｰﾌｫﾛｰ.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
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
			// 音設定
			// 
			this.音設定.DataPropertyName = "soundType";
			this.音設定.HeaderText = "音設定";
			this.音設定.Items.AddRange(new object[] {
									"ﾃﾞﾌｫﾙﾄ",
									"音A",
									"音B",
									"音C"});
			this.音設定.Name = "音設定";
			this.音設定.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.音設定.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.音設定.Width = 67;
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
									this.editLineMenu,
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
			this.contextMenuStrip1.Size = new System.Drawing.Size(302, 418);
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
			// editLineMenu
			// 
			this.editLineMenu.Name = "editLineMenu";
			this.editLineMenu.Size = new System.Drawing.Size(301, 22);
			this.editLineMenu.Text = "この行を編集";
			this.editLineMenu.Click += new System.EventHandler(this.EditLineMenuClick);
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
			this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
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
			// TabPages
			// 
			this.TabPages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.TabPages.Controls.Add(this.tabPage3);
			this.TabPages.Controls.Add(this.tabPage1);
			this.TabPages.Controls.Add(this.tabPage2);
			this.TabPages.Controls.Add(this.tabPage4);
			this.TabPages.Location = new System.Drawing.Point(12, 29);
			this.TabPages.Name = "TabPages";
			this.TabPages.SelectedIndex = 0;
			this.TabPages.Size = new System.Drawing.Size(886, 388);
			this.TabPages.TabIndex = 24;
			// 
			// tabPage3
			// 
			this.tabPage3.BackColor = System.Drawing.SystemColors.Control;
			this.tabPage3.Controls.Add(this.liveList);
			this.tabPage3.Controls.Add(this.liveListSearchBtn);
			this.tabPage3.Controls.Add(this.liveListSearchText);
			this.tabPage3.Controls.Add(this.categoryRightBtn);
			this.tabPage3.Controls.Add(this.categoryLeftBtn);
			this.tabPage3.Controls.Add(this.categoryBtnPanel);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Margin = new System.Windows.Forms.Padding(0);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(878, 362);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "放送中";
			// 
			// liveList
			// 
			this.liveList.AllowDrop = true;
			this.liveList.AllowUserToAddRows = false;
			this.liveList.AllowUserToDeleteRows = false;
			this.liveList.AllowUserToResizeRows = false;
			this.liveList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.liveList.ColumnHeadersHeight = 25;
			this.liveList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.liveList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
									this.dataGridViewTextBoxColumn4,
									this.dataGridViewTextBoxColumn5,
									this.dataGridViewTextBoxColumn6,
									this.dataGridViewTextBoxColumn7,
									this.dataGridViewTextBoxColumn9,
									this.dataGridViewButtonColumn1,
									this.dataGridViewButtonColumn2,
									this.dataGridViewTextBoxColumn10,
									this.dataGridViewTextBoxColumn11,
									this.dataGridViewCheckBoxColumn11,
									this.dataGridViewCheckBoxColumn12,
									this.dataGridViewCheckBoxColumn13,
									this.dataGridViewCheckBoxColumn14,
									this.Column3,
									this.dataGridViewCheckBoxColumn15,
									this.dataGridViewCheckBoxColumn16,
									this.Column1,
									this.Column2});
			this.liveList.ContextMenuStrip = this.contextMenuStrip3;
			this.liveList.Location = new System.Drawing.Point(6, 24);
			this.liveList.MultiSelect = false;
			this.liveList.Name = "liveList";
			this.liveList.RowHeadersVisible = false;
			this.liveList.RowTemplate.Height = 21;
			this.liveList.Size = new System.Drawing.Size(864, 335);
			this.liveList.TabIndex = 24;
			this.liveList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.LiveListCellFormatting);
			this.liveList.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.LiveListCellMouseDoubleClick);
			this.liveList.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.LiveListCellMouseDown);
			this.liveList.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.LiveListColumnWidthChanged);
			this.liveList.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.LiveListRowsAdded);
			this.liveList.Sorted += new System.EventHandler(this.LiveListSorted);
			// 
			// dataGridViewTextBoxColumn4
			// 
			this.dataGridViewTextBoxColumn4.DataPropertyName = "newItem";
			this.dataGridViewTextBoxColumn4.FillWeight = 2F;
			this.dataGridViewTextBoxColumn4.HeaderText = "新";
			this.dataGridViewTextBoxColumn4.MinimumWidth = 2;
			this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
			this.dataGridViewTextBoxColumn4.ReadOnly = true;
			this.dataGridViewTextBoxColumn4.Visible = false;
			this.dataGridViewTextBoxColumn4.Width = 77;
			// 
			// dataGridViewTextBoxColumn5
			// 
			this.dataGridViewTextBoxColumn5.DataPropertyName = "thumbnailUrl";
			this.dataGridViewTextBoxColumn5.HeaderText = "ｻﾑﾈ";
			this.dataGridViewTextBoxColumn5.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
			this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
			this.dataGridViewTextBoxColumn5.ReadOnly = true;
			this.dataGridViewTextBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.dataGridViewTextBoxColumn5.Width = 38;
			// 
			// dataGridViewTextBoxColumn6
			// 
			this.dataGridViewTextBoxColumn6.DataPropertyName = "title";
			this.dataGridViewTextBoxColumn6.HeaderText = "放送タイトル";
			this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
			this.dataGridViewTextBoxColumn6.ReadOnly = true;
			// 
			// dataGridViewTextBoxColumn7
			// 
			this.dataGridViewTextBoxColumn7.DataPropertyName = "hostName";
			this.dataGridViewTextBoxColumn7.HeaderText = "放送者";
			this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
			this.dataGridViewTextBoxColumn7.ReadOnly = true;
			// 
			// dataGridViewTextBoxColumn9
			// 
			this.dataGridViewTextBoxColumn9.DataPropertyName = "comName";
			this.dataGridViewTextBoxColumn9.HeaderText = "コミュニティ名";
			this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
			this.dataGridViewTextBoxColumn9.ReadOnly = true;
			// 
			// dataGridViewButtonColumn1
			// 
			this.dataGridViewButtonColumn1.DataPropertyName = "description";
			this.dataGridViewButtonColumn1.HeaderText = "説明";
			this.dataGridViewButtonColumn1.Name = "dataGridViewButtonColumn1";
			this.dataGridViewButtonColumn1.ReadOnly = true;
			this.dataGridViewButtonColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewButtonColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// dataGridViewButtonColumn2
			// 
			this.dataGridViewButtonColumn2.DataPropertyName = "lvId";
			this.dataGridViewButtonColumn2.HeaderText = "放送ID";
			this.dataGridViewButtonColumn2.Name = "dataGridViewButtonColumn2";
			this.dataGridViewButtonColumn2.ReadOnly = true;
			this.dataGridViewButtonColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewButtonColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.dataGridViewButtonColumn2.Width = 70;
			// 
			// dataGridViewTextBoxColumn10
			// 
			this.dataGridViewTextBoxColumn10.DataPropertyName = "comId";
			this.dataGridViewTextBoxColumn10.HeaderText = "コミュニティID";
			this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
			this.dataGridViewTextBoxColumn10.ReadOnly = true;
			this.dataGridViewTextBoxColumn10.Width = 70;
			// 
			// dataGridViewTextBoxColumn11
			// 
			this.dataGridViewTextBoxColumn11.DataPropertyName = "elapsedTime";
			this.dataGridViewTextBoxColumn11.HeaderText = "放送時間";
			this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
			this.dataGridViewTextBoxColumn11.ReadOnly = true;
			this.dataGridViewTextBoxColumn11.Width = 70;
			// 
			// dataGridViewCheckBoxColumn11
			// 
			this.dataGridViewCheckBoxColumn11.DataPropertyName = "mainCategory";
			this.dataGridViewCheckBoxColumn11.HeaderText = "カテゴリー";
			this.dataGridViewCheckBoxColumn11.Name = "dataGridViewCheckBoxColumn11";
			this.dataGridViewCheckBoxColumn11.ReadOnly = true;
			this.dataGridViewCheckBoxColumn11.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewCheckBoxColumn11.Width = 80;
			// 
			// dataGridViewCheckBoxColumn12
			// 
			this.dataGridViewCheckBoxColumn12.DataPropertyName = "face";
			this.dataGridViewCheckBoxColumn12.HeaderText = "顔";
			this.dataGridViewCheckBoxColumn12.Name = "dataGridViewCheckBoxColumn12";
			this.dataGridViewCheckBoxColumn12.ReadOnly = true;
			this.dataGridViewCheckBoxColumn12.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewCheckBoxColumn12.Width = 20;
			// 
			// dataGridViewCheckBoxColumn13
			// 
			this.dataGridViewCheckBoxColumn13.DataPropertyName = "rush";
			this.dataGridViewCheckBoxColumn13.HeaderText = "凸";
			this.dataGridViewCheckBoxColumn13.Name = "dataGridViewCheckBoxColumn13";
			this.dataGridViewCheckBoxColumn13.ReadOnly = true;
			this.dataGridViewCheckBoxColumn13.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewCheckBoxColumn13.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.dataGridViewCheckBoxColumn13.Width = 20;
			// 
			// dataGridViewCheckBoxColumn14
			// 
			this.dataGridViewCheckBoxColumn14.DataPropertyName = "cruise";
			this.dataGridViewCheckBoxColumn14.HeaderText = "ｸﾙｰｽﾞ";
			this.dataGridViewCheckBoxColumn14.Name = "dataGridViewCheckBoxColumn14";
			this.dataGridViewCheckBoxColumn14.ReadOnly = true;
			this.dataGridViewCheckBoxColumn14.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewCheckBoxColumn14.Width = 45;
			// 
			// Column3
			// 
			this.Column3.DataPropertyName = "cas";
			this.Column3.HeaderText = "実験";
			this.Column3.Name = "Column3";
			this.Column3.ReadOnly = true;
			this.Column3.Width = 35;
			// 
			// dataGridViewCheckBoxColumn15
			// 
			this.dataGridViewCheckBoxColumn15.DataPropertyName = "memberOnly";
			this.dataGridViewCheckBoxColumn15.HeaderText = "限定";
			this.dataGridViewCheckBoxColumn15.Name = "dataGridViewCheckBoxColumn15";
			this.dataGridViewCheckBoxColumn15.ReadOnly = true;
			this.dataGridViewCheckBoxColumn15.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewCheckBoxColumn15.Width = 36;
			// 
			// dataGridViewCheckBoxColumn16
			// 
			this.dataGridViewCheckBoxColumn16.DataPropertyName = "type";
			this.dataGridViewCheckBoxColumn16.HeaderText = "種類";
			this.dataGridViewCheckBoxColumn16.Name = "dataGridViewCheckBoxColumn16";
			this.dataGridViewCheckBoxColumn16.ReadOnly = true;
			this.dataGridViewCheckBoxColumn16.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewCheckBoxColumn16.Width = 70;
			// 
			// Column1
			// 
			this.Column1.DataPropertyName = "favorite";
			this.Column1.HeaderText = "お気に入り";
			this.Column1.Name = "Column1";
			this.Column1.ReadOnly = true;
			// 
			// Column2
			// 
			this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.Column2.DataPropertyName = "memo";
			this.Column2.HeaderText = "メモ";
			this.Column2.MinimumWidth = 30;
			this.Column2.Name = "Column2";
			// 
			// contextMenuStrip3
			// 
			this.contextMenuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.liveListOpenUrlMenu,
									this.liveListOpenCommunityUrlMenu,
									this.toolStripSeparator10,
									this.liveListCopyMenu,
									this.toolStripSeparator13,
									this.liveListWriteSamuneMemoMenu,
									this.liveListNGthumbnailMenu,
									this.liveListUpdateSamuneMenu,
									this.toolStripSeparator11,
									this.liveListAddFavoriteCommunityMenu,
									this.liveListRemoveFavoriteCommunityMenu,
									this.toolStripSeparator12,
									this.liveListDeleteRowMenu});
			this.contextMenuStrip3.Name = "contextMenuStrip1";
			this.contextMenuStrip3.Size = new System.Drawing.Size(293, 226);
			// 
			// liveListOpenUrlMenu
			// 
			this.liveListOpenUrlMenu.Name = "liveListOpenUrlMenu";
			this.liveListOpenUrlMenu.Size = new System.Drawing.Size(292, 22);
			this.liveListOpenUrlMenu.Text = "放送URLを開く";
			this.liveListOpenUrlMenu.Click += new System.EventHandler(this.LiveListOpenUrlMenuClick);
			// 
			// liveListOpenCommunityUrlMenu
			// 
			this.liveListOpenCommunityUrlMenu.Name = "liveListOpenCommunityUrlMenu";
			this.liveListOpenCommunityUrlMenu.Size = new System.Drawing.Size(292, 22);
			this.liveListOpenCommunityUrlMenu.Text = "コミュニティURLを開く";
			this.liveListOpenCommunityUrlMenu.Click += new System.EventHandler(this.LiveListOpenCommunityUrlMenuClick);
			// 
			// toolStripSeparator10
			// 
			this.toolStripSeparator10.Name = "toolStripSeparator10";
			this.toolStripSeparator10.Size = new System.Drawing.Size(289, 6);
			// 
			// liveListCopyMenu
			// 
			this.liveListCopyMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.liveListUrlCopyMenu,
									this.liveListCommunityUrlCopyMenu,
									this.toolStripSeparator14,
									this.liveListTitleCopyMenu,
									this.liveListHostNameCopyMenu,
									this.liveListCommunityNameCopyMenu,
									this.liveListDescriptionCopyMenu});
			this.liveListCopyMenu.Name = "liveListCopyMenu";
			this.liveListCopyMenu.Size = new System.Drawing.Size(292, 22);
			this.liveListCopyMenu.Text = "コピー";
			this.liveListCopyMenu.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.LiveListCopyMenuDropDownItemClicked);
			// 
			// liveListUrlCopyMenu
			// 
			this.liveListUrlCopyMenu.Name = "liveListUrlCopyMenu";
			this.liveListUrlCopyMenu.Size = new System.Drawing.Size(172, 22);
			this.liveListUrlCopyMenu.Text = "放送URL";
			// 
			// liveListCommunityUrlCopyMenu
			// 
			this.liveListCommunityUrlCopyMenu.Name = "liveListCommunityUrlCopyMenu";
			this.liveListCommunityUrlCopyMenu.Size = new System.Drawing.Size(172, 22);
			this.liveListCommunityUrlCopyMenu.Text = "コミュニティURL";
			// 
			// toolStripSeparator14
			// 
			this.toolStripSeparator14.Name = "toolStripSeparator14";
			this.toolStripSeparator14.Size = new System.Drawing.Size(169, 6);
			// 
			// liveListTitleCopyMenu
			// 
			this.liveListTitleCopyMenu.Name = "liveListTitleCopyMenu";
			this.liveListTitleCopyMenu.Size = new System.Drawing.Size(172, 22);
			this.liveListTitleCopyMenu.Text = "放送タイトル";
			// 
			// liveListHostNameCopyMenu
			// 
			this.liveListHostNameCopyMenu.Name = "liveListHostNameCopyMenu";
			this.liveListHostNameCopyMenu.Size = new System.Drawing.Size(172, 22);
			this.liveListHostNameCopyMenu.Text = "放送者";
			// 
			// liveListCommunityNameCopyMenu
			// 
			this.liveListCommunityNameCopyMenu.Name = "liveListCommunityNameCopyMenu";
			this.liveListCommunityNameCopyMenu.Size = new System.Drawing.Size(172, 22);
			this.liveListCommunityNameCopyMenu.Text = "コミュニティ名";
			// 
			// liveListDescriptionCopyMenu
			// 
			this.liveListDescriptionCopyMenu.Name = "liveListDescriptionCopyMenu";
			this.liveListDescriptionCopyMenu.Size = new System.Drawing.Size(172, 22);
			this.liveListDescriptionCopyMenu.Text = "説明";
			// 
			// toolStripSeparator13
			// 
			this.toolStripSeparator13.Name = "toolStripSeparator13";
			this.toolStripSeparator13.Size = new System.Drawing.Size(289, 6);
			// 
			// liveListWriteSamuneMemoMenu
			// 
			this.liveListWriteSamuneMemoMenu.Name = "liveListWriteSamuneMemoMenu";
			this.liveListWriteSamuneMemoMenu.Size = new System.Drawing.Size(292, 22);
			this.liveListWriteSamuneMemoMenu.Text = "サムネにメモを書く";
			this.liveListWriteSamuneMemoMenu.Click += new System.EventHandler(this.LiveListWriteSamuneMemoMenuClick);
			// 
			// liveListNGthumbnailMenu
			// 
			this.liveListNGthumbnailMenu.Name = "liveListNGthumbnailMenu";
			this.liveListNGthumbnailMenu.Size = new System.Drawing.Size(292, 22);
			this.liveListNGthumbnailMenu.Text = "NGサムネにする(簡易NG)";
			this.liveListNGthumbnailMenu.Click += new System.EventHandler(this.LiveListNGthumbnailMenuClick);
			// 
			// liveListUpdateSamuneMenu
			// 
			this.liveListUpdateSamuneMenu.Name = "liveListUpdateSamuneMenu";
			this.liveListUpdateSamuneMenu.Size = new System.Drawing.Size(292, 22);
			this.liveListUpdateSamuneMenu.Text = "サムネを再取得する";
			this.liveListUpdateSamuneMenu.Click += new System.EventHandler(this.LiveListUpdateSamuneMenuClick);
			// 
			// toolStripSeparator11
			// 
			this.toolStripSeparator11.Name = "toolStripSeparator11";
			this.toolStripSeparator11.Size = new System.Drawing.Size(289, 6);
			// 
			// liveListAddFavoriteCommunityMenu
			// 
			this.liveListAddFavoriteCommunityMenu.Name = "liveListAddFavoriteCommunityMenu";
			this.liveListAddFavoriteCommunityMenu.Size = new System.Drawing.Size(292, 22);
			this.liveListAddFavoriteCommunityMenu.Text = "コミュニティをお気に入りに登録する";
			this.liveListAddFavoriteCommunityMenu.Click += new System.EventHandler(this.LiveListAddFavoriteCommunityMenuClick);
			// 
			// liveListRemoveFavoriteCommunityMenu
			// 
			this.liveListRemoveFavoriteCommunityMenu.Name = "liveListRemoveFavoriteCommunityMenu";
			this.liveListRemoveFavoriteCommunityMenu.Size = new System.Drawing.Size(292, 22);
			this.liveListRemoveFavoriteCommunityMenu.Text = "コミュニティをお気に入りから削除する";
			this.liveListRemoveFavoriteCommunityMenu.Click += new System.EventHandler(this.LiveListRemoveFavoriteCommunityMenuClick);
			// 
			// toolStripSeparator12
			// 
			this.toolStripSeparator12.Name = "toolStripSeparator12";
			this.toolStripSeparator12.Size = new System.Drawing.Size(289, 6);
			// 
			// liveListDeleteRowMenu
			// 
			this.liveListDeleteRowMenu.Name = "liveListDeleteRowMenu";
			this.liveListDeleteRowMenu.Size = new System.Drawing.Size(292, 22);
			this.liveListDeleteRowMenu.Text = "この行を削除する";
			this.liveListDeleteRowMenu.Click += new System.EventHandler(this.LiveListDeleteRowMenuClick);
			// 
			// liveListSearchBtn
			// 
			this.liveListSearchBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.liveListSearchBtn.Location = new System.Drawing.Point(839, 1);
			this.liveListSearchBtn.Name = "liveListSearchBtn";
			this.liveListSearchBtn.Size = new System.Drawing.Size(38, 23);
			this.liveListSearchBtn.TabIndex = 12;
			this.liveListSearchBtn.Text = "検索";
			this.liveListSearchBtn.UseVisualStyleBackColor = true;
			this.liveListSearchBtn.Click += new System.EventHandler(this.LiveListSearchBtnClick);
			// 
			// liveListSearchText
			// 
			this.liveListSearchText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.liveListSearchText.Location = new System.Drawing.Point(742, 4);
			this.liveListSearchText.Name = "liveListSearchText";
			this.liveListSearchText.Size = new System.Drawing.Size(95, 19);
			this.liveListSearchText.TabIndex = 11;
			// 
			// categoryRightBtn
			// 
			this.categoryRightBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.categoryRightBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("categoryRightBtn.BackgroundImage")));
			this.categoryRightBtn.FlatAppearance.BorderSize = 0;
			this.categoryRightBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.categoryRightBtn.Location = new System.Drawing.Point(723, 7);
			this.categoryRightBtn.Margin = new System.Windows.Forms.Padding(0);
			this.categoryRightBtn.Name = "categoryRightBtn";
			this.categoryRightBtn.Size = new System.Drawing.Size(16, 16);
			this.categoryRightBtn.TabIndex = 10;
			this.categoryRightBtn.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.categoryRightBtn.UseVisualStyleBackColor = true;
			this.categoryRightBtn.Click += new System.EventHandler(this.CategoryRightBtnClick);
			// 
			// categoryLeftBtn
			// 
			this.categoryLeftBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.categoryLeftBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("categoryLeftBtn.BackgroundImage")));
			this.categoryLeftBtn.FlatAppearance.BorderSize = 0;
			this.categoryLeftBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.categoryLeftBtn.Location = new System.Drawing.Point(707, 7);
			this.categoryLeftBtn.Margin = new System.Windows.Forms.Padding(0);
			this.categoryLeftBtn.Name = "categoryLeftBtn";
			this.categoryLeftBtn.Size = new System.Drawing.Size(16, 16);
			this.categoryLeftBtn.TabIndex = 9;
			this.categoryLeftBtn.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.categoryLeftBtn.UseVisualStyleBackColor = true;
			this.categoryLeftBtn.Click += new System.EventHandler(this.CategoryLeftBtnClick);
			// 
			// categoryBtnPanel
			// 
			this.categoryBtnPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.categoryBtnPanel.Controls.Add(this.allCategoryBtn);
			this.categoryBtnPanel.Controls.Add(this.commonCategoryBtn);
			this.categoryBtnPanel.Controls.Add(this.tryCategoryBtn);
			this.categoryBtnPanel.Controls.Add(this.liveCategoryBtn);
			this.categoryBtnPanel.Controls.Add(this.reqCategoryBtn);
			this.categoryBtnPanel.Location = new System.Drawing.Point(1, 1);
			this.categoryBtnPanel.Name = "categoryBtnPanel";
			this.categoryBtnPanel.Size = new System.Drawing.Size(703, 22);
			this.categoryBtnPanel.TabIndex = 8;
			this.categoryBtnPanel.WrapContents = false;
			this.categoryBtnPanel.SizeChanged += new System.EventHandler(this.CategoryBtnPanelSizeChanged);
			// 
			// allCategoryBtn
			// 
			this.allCategoryBtn.Appearance = System.Windows.Forms.Appearance.Button;
			this.allCategoryBtn.AutoSize = true;
			this.allCategoryBtn.BackColor = System.Drawing.SystemColors.Control;
			this.allCategoryBtn.Checked = true;
			this.allCategoryBtn.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
			this.allCategoryBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.allCategoryBtn.Location = new System.Drawing.Point(0, 0);
			this.allCategoryBtn.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.allCategoryBtn.Name = "allCategoryBtn";
			this.allCategoryBtn.Size = new System.Drawing.Size(65, 22);
			this.allCategoryBtn.TabIndex = 2;
			this.allCategoryBtn.TabStop = true;
			this.allCategoryBtn.Text = "全て(0/0)";
			this.allCategoryBtn.UseCompatibleTextRendering = true;
			this.allCategoryBtn.UseMnemonic = false;
			this.allCategoryBtn.UseVisualStyleBackColor = true;
			// 
			// commonCategoryBtn
			// 
			this.commonCategoryBtn.Appearance = System.Windows.Forms.Appearance.Button;
			this.commonCategoryBtn.AutoSize = true;
			this.commonCategoryBtn.BackColor = System.Drawing.SystemColors.Control;
			this.commonCategoryBtn.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
			this.commonCategoryBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.commonCategoryBtn.Location = new System.Drawing.Point(68, 0);
			this.commonCategoryBtn.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.commonCategoryBtn.Name = "commonCategoryBtn";
			this.commonCategoryBtn.Size = new System.Drawing.Size(67, 22);
			this.commonCategoryBtn.TabIndex = 2;
			this.commonCategoryBtn.Text = "一般(0/0)";
			this.commonCategoryBtn.UseCompatibleTextRendering = true;
			this.commonCategoryBtn.UseMnemonic = false;
			this.commonCategoryBtn.UseVisualStyleBackColor = true;
			// 
			// tryCategoryBtn
			// 
			this.tryCategoryBtn.Appearance = System.Windows.Forms.Appearance.Button;
			this.tryCategoryBtn.AutoSize = true;
			this.tryCategoryBtn.BackColor = System.Drawing.SystemColors.Control;
			this.tryCategoryBtn.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
			this.tryCategoryBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.tryCategoryBtn.Location = new System.Drawing.Point(138, 0);
			this.tryCategoryBtn.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.tryCategoryBtn.Name = "tryCategoryBtn";
			this.tryCategoryBtn.Size = new System.Drawing.Size(91, 22);
			this.tryCategoryBtn.TabIndex = 3;
			this.tryCategoryBtn.Text = "やってみた(0/0)";
			this.tryCategoryBtn.UseCompatibleTextRendering = true;
			this.tryCategoryBtn.UseMnemonic = false;
			this.tryCategoryBtn.UseVisualStyleBackColor = true;
			// 
			// liveCategoryBtn
			// 
			this.liveCategoryBtn.Appearance = System.Windows.Forms.Appearance.Button;
			this.liveCategoryBtn.AutoSize = true;
			this.liveCategoryBtn.BackColor = System.Drawing.SystemColors.Control;
			this.liveCategoryBtn.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
			this.liveCategoryBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.liveCategoryBtn.Location = new System.Drawing.Point(232, 0);
			this.liveCategoryBtn.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.liveCategoryBtn.Name = "liveCategoryBtn";
			this.liveCategoryBtn.Size = new System.Drawing.Size(73, 22);
			this.liveCategoryBtn.TabIndex = 4;
			this.liveCategoryBtn.Text = "ゲーム(0/0)";
			this.liveCategoryBtn.UseCompatibleTextRendering = true;
			this.liveCategoryBtn.UseMnemonic = false;
			this.liveCategoryBtn.UseVisualStyleBackColor = true;
			// 
			// reqCategoryBtn
			// 
			this.reqCategoryBtn.Appearance = System.Windows.Forms.Appearance.Button;
			this.reqCategoryBtn.AutoSize = true;
			this.reqCategoryBtn.BackColor = System.Drawing.SystemColors.Control;
			this.reqCategoryBtn.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
			this.reqCategoryBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.reqCategoryBtn.Location = new System.Drawing.Point(308, 0);
			this.reqCategoryBtn.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.reqCategoryBtn.Name = "reqCategoryBtn";
			this.reqCategoryBtn.Size = new System.Drawing.Size(92, 22);
			this.reqCategoryBtn.TabIndex = 5;
			this.reqCategoryBtn.Text = "動画紹介(0/0)";
			this.reqCategoryBtn.UseCompatibleTextRendering = true;
			this.reqCategoryBtn.UseMnemonic = false;
			this.reqCategoryBtn.UseVisualStyleBackColor = true;
			// 
			// tabPage1
			// 
			this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
			this.tabPage1.Controls.Add(this.favoriteCommunityPanel);
			this.tabPage1.Controls.Add(this.favoriteUserPanel);
			this.tabPage1.Controls.Add(this.favoriteCommunityBtn);
			this.tabPage1.Controls.Add(this.favoriteUserBtn);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(878, 362);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "お気に入り設定";
			// 
			// favoriteCommunityPanel
			// 
			this.favoriteCommunityPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.favoriteCommunityPanel.Controls.Add(this.addBtn);
			this.favoriteCommunityPanel.Controls.Add(this.upButton);
			this.favoriteCommunityPanel.Controls.Add(this.downButton);
			this.favoriteCommunityPanel.Controls.Add(this.alartList);
			this.favoriteCommunityPanel.Controls.Add(this.userThumbBox);
			this.favoriteCommunityPanel.Controls.Add(this.logText);
			this.favoriteCommunityPanel.Controls.Add(this.comThumbBox);
			this.favoriteCommunityPanel.Controls.Add(this.searchText);
			this.favoriteCommunityPanel.Controls.Add(this.favoriteNumLabel);
			this.favoriteCommunityPanel.Controls.Add(this.searchBtn);
			this.favoriteCommunityPanel.Location = new System.Drawing.Point(0, 30);
			this.favoriteCommunityPanel.Margin = new System.Windows.Forms.Padding(0);
			this.favoriteCommunityPanel.Name = "favoriteCommunityPanel";
			this.favoriteCommunityPanel.Size = new System.Drawing.Size(878, 332);
			this.favoriteCommunityPanel.TabIndex = 35;
			// 
			// upButton
			// 
			this.upButton.Location = new System.Drawing.Point(103, 8);
			this.upButton.Name = "upButton";
			this.upButton.Size = new System.Drawing.Size(30, 23);
			this.upButton.TabIndex = 24;
			this.upButton.Text = "↑";
			this.upButton.UseVisualStyleBackColor = true;
			this.upButton.Click += new System.EventHandler(this.UpButtonClick);
			// 
			// downButton
			// 
			this.downButton.Location = new System.Drawing.Point(139, 8);
			this.downButton.Name = "downButton";
			this.downButton.Size = new System.Drawing.Size(30, 23);
			this.downButton.TabIndex = 25;
			this.downButton.Text = "↓";
			this.downButton.UseVisualStyleBackColor = true;
			this.downButton.Click += new System.EventHandler(this.DownButtonClick);
			// 
			// userThumbBox
			// 
			this.userThumbBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.userThumbBox.Image = ((System.Drawing.Image)(resources.GetObject("userThumbBox.Image")));
			this.userThumbBox.Location = new System.Drawing.Point(825, 0);
			this.userThumbBox.Name = "userThumbBox";
			this.userThumbBox.Size = new System.Drawing.Size(43, 43);
			this.userThumbBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.userThumbBox.TabIndex = 29;
			this.userThumbBox.TabStop = false;
			// 
			// logText
			// 
			this.logText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.logText.Location = new System.Drawing.Point(403, 0);
			this.logText.Multiline = true;
			this.logText.Name = "logText";
			this.logText.ReadOnly = true;
			this.logText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.logText.Size = new System.Drawing.Size(266, 47);
			this.logText.TabIndex = 32;
			// 
			// comThumbBox
			// 
			this.comThumbBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.comThumbBox.Image = ((System.Drawing.Image)(resources.GetObject("comThumbBox.Image")));
			this.comThumbBox.Location = new System.Drawing.Point(775, 0);
			this.comThumbBox.Name = "comThumbBox";
			this.comThumbBox.Size = new System.Drawing.Size(43, 43);
			this.comThumbBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.comThumbBox.TabIndex = 29;
			this.comThumbBox.TabStop = false;
			// 
			// searchText
			// 
			this.searchText.Location = new System.Drawing.Point(203, 10);
			this.searchText.Name = "searchText";
			this.searchText.Size = new System.Drawing.Size(100, 19);
			this.searchText.TabIndex = 26;
			// 
			// favoriteNumLabel
			// 
			this.favoriteNumLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.favoriteNumLabel.Location = new System.Drawing.Point(661, 13);
			this.favoriteNumLabel.Name = "favoriteNumLabel";
			this.favoriteNumLabel.Size = new System.Drawing.Size(106, 23);
			this.favoriteNumLabel.TabIndex = 28;
			this.favoriteNumLabel.Text = "登録数：0件";
			this.favoriteNumLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.favoriteNumLabel.Visible = false;
			// 
			// searchBtn
			// 
			this.searchBtn.Location = new System.Drawing.Point(309, 8);
			this.searchBtn.Name = "searchBtn";
			this.searchBtn.Size = new System.Drawing.Size(75, 23);
			this.searchBtn.TabIndex = 27;
			this.searchBtn.Text = "検索";
			this.searchBtn.UseVisualStyleBackColor = true;
			this.searchBtn.Click += new System.EventHandler(this.SearchBtnClick);
			// 
			// favoriteUserPanel
			// 
			this.favoriteUserPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.favoriteUserPanel.Controls.Add(this.favoriteUserThumbBox);
			this.favoriteUserPanel.Controls.Add(this.favoriteUserNumLabel);
			this.favoriteUserPanel.Controls.Add(this.userAddBtn);
			this.favoriteUserPanel.Controls.Add(this.userAddText);
			this.favoriteUserPanel.Controls.Add(this.label6);
			this.favoriteUserPanel.Controls.Add(this.userAlartList);
			this.favoriteUserPanel.Location = new System.Drawing.Point(0, 30);
			this.favoriteUserPanel.Name = "favoriteUserPanel";
			this.favoriteUserPanel.Size = new System.Drawing.Size(878, 332);
			this.favoriteUserPanel.TabIndex = 33;
			// 
			// favoriteUserThumbBox
			// 
			this.favoriteUserThumbBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.favoriteUserThumbBox.Image = ((System.Drawing.Image)(resources.GetObject("favoriteUserThumbBox.Image")));
			this.favoriteUserThumbBox.Location = new System.Drawing.Point(825, 0);
			this.favoriteUserThumbBox.Name = "favoriteUserThumbBox";
			this.favoriteUserThumbBox.Size = new System.Drawing.Size(43, 43);
			this.favoriteUserThumbBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.favoriteUserThumbBox.TabIndex = 30;
			this.favoriteUserThumbBox.TabStop = false;
			// 
			// favoriteUserNumLabel
			// 
			this.favoriteUserNumLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.favoriteUserNumLabel.Location = new System.Drawing.Point(661, 13);
			this.favoriteUserNumLabel.Name = "favoriteUserNumLabel";
			this.favoriteUserNumLabel.Size = new System.Drawing.Size(106, 23);
			this.favoriteUserNumLabel.TabIndex = 29;
			this.favoriteUserNumLabel.Text = "登録数：0件";
			this.favoriteUserNumLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.favoriteUserNumLabel.Visible = false;
			// 
			// userAddBtn
			// 
			this.userAddBtn.Location = new System.Drawing.Point(263, 15);
			this.userAddBtn.Name = "userAddBtn";
			this.userAddBtn.Size = new System.Drawing.Size(50, 23);
			this.userAddBtn.TabIndex = 27;
			this.userAddBtn.Text = "追加";
			this.userAddBtn.UseVisualStyleBackColor = true;
			this.userAddBtn.Click += new System.EventHandler(this.UserAddBtnClick);
			// 
			// userAddText
			// 
			this.userAddText.Location = new System.Drawing.Point(77, 17);
			this.userAddText.Name = "userAddText";
			this.userAddText.Size = new System.Drawing.Size(180, 19);
			this.userAddText.TabIndex = 26;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(6, 20);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(65, 15);
			this.label6.TabIndex = 25;
			this.label6.Text = "ユーザーID：";
			// 
			// userAlartList
			// 
			this.userAlartList.AllowDrop = true;
			this.userAlartList.AllowUserToAddRows = false;
			this.userAlartList.AllowUserToDeleteRows = false;
			this.userAlartList.AllowUserToResizeRows = false;
			this.userAlartList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.userAlartList.ColumnHeadersHeight = 25;
			this.userAlartList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.userAlartList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
									this.dataGridViewTextBoxColumn25,
									this.dataGridViewTextBoxColumn26,
									this.dataGridViewTextBoxColumn27,
									this.dataGridViewTextBoxColumn28,
									this.dataGridViewTextBoxColumn29,
									this.dataGridViewComboBoxColumn1,
									this.dataGridViewButtonColumn3,
									this.dataGridViewButtonColumn4,
									this.dataGridViewTextBoxColumn30,
									this.dataGridViewTextBoxColumn31,
									this.dataGridViewCheckBoxColumn17,
									this.dataGridViewCheckBoxColumn18,
									this.dataGridViewCheckBoxColumn19,
									this.dataGridViewCheckBoxColumn20,
									this.dataGridViewCheckBoxColumn21,
									this.dataGridViewCheckBoxColumn22,
									this.dataGridViewCheckBoxColumn23,
									this.dataGridViewCheckBoxColumn24,
									this.dataGridViewCheckBoxColumn25,
									this.dataGridViewCheckBoxColumn26,
									this.dataGridViewCheckBoxColumn27,
									this.dataGridViewCheckBoxColumn28,
									this.dataGridViewCheckBoxColumn29,
									this.dataGridViewCheckBoxColumn30,
									this.dataGridViewCheckBoxColumn31,
									this.dataGridViewComboBoxColumn2,
									this.dataGridViewTextBoxColumn32});
			this.userAlartList.ContextMenuStrip = this.contextMenuStrip4;
			this.userAlartList.Location = new System.Drawing.Point(6, 50);
			this.userAlartList.Name = "userAlartList";
			this.userAlartList.RowHeadersVisible = false;
			this.userAlartList.RowTemplate.Height = 21;
			this.userAlartList.Size = new System.Drawing.Size(864, 276);
			this.userAlartList.TabIndex = 24;
			this.userAlartList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.AlartListCellClick);
			this.userAlartList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.UserAlartListCellFormatting);
			this.userAlartList.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.AlartListCellMouseDoubleClick);
			this.userAlartList.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.AlartListCellMouseDown);
			this.userAlartList.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.UserAlartListCellParsing);
			this.userAlartList.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.UserAlartListCellValidating);
			this.userAlartList.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.AlartListColumnHeaderMouseClick);
			this.userAlartList.CurrentCellDirtyStateChanged += new System.EventHandler(this.UserAlartListCurrentCellDirtyStateChanged);
			this.userAlartList.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.AlartListRowEnter);
			this.userAlartList.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.UserAlartListRowsAdded);
			this.userAlartList.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.UserAlartListRowsRemoved);
			this.userAlartList.DragDrop += new System.Windows.Forms.DragEventHandler(this.UserAlartListDragDrop);
			this.userAlartList.DragEnter += new System.Windows.Forms.DragEventHandler(this.listDragEnter);
			// 
			// dataGridViewTextBoxColumn25
			// 
			this.dataGridViewTextBoxColumn25.DataPropertyName = "communityId";
			this.dataGridViewTextBoxColumn25.FillWeight = 2F;
			this.dataGridViewTextBoxColumn25.HeaderText = "ｺﾐｭﾆﾃｨID";
			this.dataGridViewTextBoxColumn25.MinimumWidth = 2;
			this.dataGridViewTextBoxColumn25.Name = "dataGridViewTextBoxColumn25";
			this.dataGridViewTextBoxColumn25.ReadOnly = true;
			this.dataGridViewTextBoxColumn25.Visible = false;
			this.dataGridViewTextBoxColumn25.Width = 77;
			// 
			// dataGridViewTextBoxColumn26
			// 
			this.dataGridViewTextBoxColumn26.DataPropertyName = "hostId";
			this.dataGridViewTextBoxColumn26.HeaderText = "ﾕｰｻﾞｰID";
			this.dataGridViewTextBoxColumn26.Name = "dataGridViewTextBoxColumn26";
			this.dataGridViewTextBoxColumn26.ReadOnly = true;
			// 
			// dataGridViewTextBoxColumn27
			// 
			this.dataGridViewTextBoxColumn27.DataPropertyName = "communityName";
			this.dataGridViewTextBoxColumn27.HeaderText = "ｺﾐｭﾆﾃｨ名";
			this.dataGridViewTextBoxColumn27.Name = "dataGridViewTextBoxColumn27";
			this.dataGridViewTextBoxColumn27.ReadOnly = true;
			this.dataGridViewTextBoxColumn27.Visible = false;
			// 
			// dataGridViewTextBoxColumn28
			// 
			this.dataGridViewTextBoxColumn28.DataPropertyName = "hostName";
			this.dataGridViewTextBoxColumn28.HeaderText = "ﾕｰｻﾞｰ名";
			this.dataGridViewTextBoxColumn28.Name = "dataGridViewTextBoxColumn28";
			this.dataGridViewTextBoxColumn28.ReadOnly = true;
			// 
			// dataGridViewTextBoxColumn29
			// 
			this.dataGridViewTextBoxColumn29.DataPropertyName = "keyword";
			this.dataGridViewTextBoxColumn29.HeaderText = "ｷｰﾜｰﾄﾞ";
			this.dataGridViewTextBoxColumn29.Name = "dataGridViewTextBoxColumn29";
			this.dataGridViewTextBoxColumn29.Visible = false;
			// 
			// dataGridViewComboBoxColumn1
			// 
			this.dataGridViewComboBoxColumn1.DataPropertyName = "isAnd";
			this.dataGridViewComboBoxColumn1.HeaderText = "合致条件";
			this.dataGridViewComboBoxColumn1.Items.AddRange(new object[] {
									"全て合致",
									"いずれか"});
			this.dataGridViewComboBoxColumn1.Name = "dataGridViewComboBoxColumn1";
			this.dataGridViewComboBoxColumn1.Visible = false;
			this.dataGridViewComboBoxColumn1.Width = 70;
			// 
			// dataGridViewButtonColumn3
			// 
			this.dataGridViewButtonColumn3.DataPropertyName = "communityFollow";
			this.dataGridViewButtonColumn3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.dataGridViewButtonColumn3.HeaderText = "ｺﾐｭﾆﾃｨﾌｫﾛｰ";
			this.dataGridViewButtonColumn3.Name = "dataGridViewButtonColumn3";
			this.dataGridViewButtonColumn3.Visible = false;
			// 
			// dataGridViewButtonColumn4
			// 
			this.dataGridViewButtonColumn4.DataPropertyName = "hostFollow";
			this.dataGridViewButtonColumn4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.dataGridViewButtonColumn4.HeaderText = "ﾕｰｻﾞｰﾌｫﾛｰ";
			this.dataGridViewButtonColumn4.Name = "dataGridViewButtonColumn4";
			// 
			// dataGridViewTextBoxColumn30
			// 
			this.dataGridViewTextBoxColumn30.DataPropertyName = "lastHostDate";
			this.dataGridViewTextBoxColumn30.HeaderText = "最近の放送日時";
			this.dataGridViewTextBoxColumn30.MinimumWidth = 112;
			this.dataGridViewTextBoxColumn30.Name = "dataGridViewTextBoxColumn30";
			this.dataGridViewTextBoxColumn30.ReadOnly = true;
			this.dataGridViewTextBoxColumn30.Width = 112;
			// 
			// dataGridViewTextBoxColumn31
			// 
			this.dataGridViewTextBoxColumn31.DataPropertyName = "addDate";
			this.dataGridViewTextBoxColumn31.HeaderText = "登録日時";
			this.dataGridViewTextBoxColumn31.MinimumWidth = 112;
			this.dataGridViewTextBoxColumn31.Name = "dataGridViewTextBoxColumn31";
			this.dataGridViewTextBoxColumn31.ReadOnly = true;
			this.dataGridViewTextBoxColumn31.Width = 112;
			// 
			// dataGridViewCheckBoxColumn17
			// 
			this.dataGridViewCheckBoxColumn17.DataPropertyName = "popup";
			this.dataGridViewCheckBoxColumn17.HeaderText = "ﾎﾟｯﾌﾟｱｯﾌﾟ";
			this.dataGridViewCheckBoxColumn17.MinimumWidth = 45;
			this.dataGridViewCheckBoxColumn17.Name = "dataGridViewCheckBoxColumn17";
			this.dataGridViewCheckBoxColumn17.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewCheckBoxColumn17.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.dataGridViewCheckBoxColumn17.Width = 45;
			// 
			// dataGridViewCheckBoxColumn18
			// 
			this.dataGridViewCheckBoxColumn18.DataPropertyName = "baloon";
			this.dataGridViewCheckBoxColumn18.HeaderText = "ﾊﾞﾙｰﾝ";
			this.dataGridViewCheckBoxColumn18.MinimumWidth = 45;
			this.dataGridViewCheckBoxColumn18.Name = "dataGridViewCheckBoxColumn18";
			this.dataGridViewCheckBoxColumn18.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewCheckBoxColumn18.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.dataGridViewCheckBoxColumn18.Width = 45;
			// 
			// dataGridViewCheckBoxColumn19
			// 
			this.dataGridViewCheckBoxColumn19.DataPropertyName = "browser";
			this.dataGridViewCheckBoxColumn19.HeaderText = "Web";
			this.dataGridViewCheckBoxColumn19.MinimumWidth = 45;
			this.dataGridViewCheckBoxColumn19.Name = "dataGridViewCheckBoxColumn19";
			this.dataGridViewCheckBoxColumn19.Width = 45;
			// 
			// dataGridViewCheckBoxColumn20
			// 
			this.dataGridViewCheckBoxColumn20.DataPropertyName = "mail";
			this.dataGridViewCheckBoxColumn20.HeaderText = "ﾒｰﾙ";
			this.dataGridViewCheckBoxColumn20.MinimumWidth = 45;
			this.dataGridViewCheckBoxColumn20.Name = "dataGridViewCheckBoxColumn20";
			this.dataGridViewCheckBoxColumn20.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewCheckBoxColumn20.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.dataGridViewCheckBoxColumn20.Width = 45;
			// 
			// dataGridViewCheckBoxColumn21
			// 
			this.dataGridViewCheckBoxColumn21.DataPropertyName = "sound";
			this.dataGridViewCheckBoxColumn21.HeaderText = "音";
			this.dataGridViewCheckBoxColumn21.MinimumWidth = 45;
			this.dataGridViewCheckBoxColumn21.Name = "dataGridViewCheckBoxColumn21";
			this.dataGridViewCheckBoxColumn21.Width = 45;
			// 
			// dataGridViewCheckBoxColumn22
			// 
			this.dataGridViewCheckBoxColumn22.DataPropertyName = "appliA";
			this.dataGridViewCheckBoxColumn22.HeaderText = "ｱﾌﾟﾘA";
			this.dataGridViewCheckBoxColumn22.MinimumWidth = 45;
			this.dataGridViewCheckBoxColumn22.Name = "dataGridViewCheckBoxColumn22";
			this.dataGridViewCheckBoxColumn22.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewCheckBoxColumn22.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.dataGridViewCheckBoxColumn22.Width = 45;
			// 
			// dataGridViewCheckBoxColumn23
			// 
			this.dataGridViewCheckBoxColumn23.DataPropertyName = "appliB";
			this.dataGridViewCheckBoxColumn23.HeaderText = "ｱﾌﾟﾘB";
			this.dataGridViewCheckBoxColumn23.MinimumWidth = 45;
			this.dataGridViewCheckBoxColumn23.Name = "dataGridViewCheckBoxColumn23";
			this.dataGridViewCheckBoxColumn23.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewCheckBoxColumn23.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.dataGridViewCheckBoxColumn23.Width = 45;
			// 
			// dataGridViewCheckBoxColumn24
			// 
			this.dataGridViewCheckBoxColumn24.DataPropertyName = "appliC";
			this.dataGridViewCheckBoxColumn24.HeaderText = "ｱﾌﾟﾘC";
			this.dataGridViewCheckBoxColumn24.MinimumWidth = 45;
			this.dataGridViewCheckBoxColumn24.Name = "dataGridViewCheckBoxColumn24";
			this.dataGridViewCheckBoxColumn24.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewCheckBoxColumn24.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.dataGridViewCheckBoxColumn24.Width = 45;
			// 
			// dataGridViewCheckBoxColumn25
			// 
			this.dataGridViewCheckBoxColumn25.DataPropertyName = "appliD";
			this.dataGridViewCheckBoxColumn25.HeaderText = "ｱﾌﾟﾘD";
			this.dataGridViewCheckBoxColumn25.MinimumWidth = 45;
			this.dataGridViewCheckBoxColumn25.Name = "dataGridViewCheckBoxColumn25";
			this.dataGridViewCheckBoxColumn25.Width = 45;
			// 
			// dataGridViewCheckBoxColumn26
			// 
			this.dataGridViewCheckBoxColumn26.DataPropertyName = "appliE";
			this.dataGridViewCheckBoxColumn26.HeaderText = "ｱﾌﾟﾘE";
			this.dataGridViewCheckBoxColumn26.MinimumWidth = 45;
			this.dataGridViewCheckBoxColumn26.Name = "dataGridViewCheckBoxColumn26";
			this.dataGridViewCheckBoxColumn26.Width = 45;
			// 
			// dataGridViewCheckBoxColumn27
			// 
			this.dataGridViewCheckBoxColumn27.DataPropertyName = "appliF";
			this.dataGridViewCheckBoxColumn27.HeaderText = "ｱﾌﾟﾘF";
			this.dataGridViewCheckBoxColumn27.MinimumWidth = 45;
			this.dataGridViewCheckBoxColumn27.Name = "dataGridViewCheckBoxColumn27";
			this.dataGridViewCheckBoxColumn27.Width = 45;
			// 
			// dataGridViewCheckBoxColumn28
			// 
			this.dataGridViewCheckBoxColumn28.DataPropertyName = "appliG";
			this.dataGridViewCheckBoxColumn28.HeaderText = "ｱﾌﾟﾘG";
			this.dataGridViewCheckBoxColumn28.MinimumWidth = 45;
			this.dataGridViewCheckBoxColumn28.Name = "dataGridViewCheckBoxColumn28";
			this.dataGridViewCheckBoxColumn28.Width = 45;
			// 
			// dataGridViewCheckBoxColumn29
			// 
			this.dataGridViewCheckBoxColumn29.DataPropertyName = "appliH";
			this.dataGridViewCheckBoxColumn29.HeaderText = "ｱﾌﾟﾘH";
			this.dataGridViewCheckBoxColumn29.MinimumWidth = 45;
			this.dataGridViewCheckBoxColumn29.Name = "dataGridViewCheckBoxColumn29";
			this.dataGridViewCheckBoxColumn29.Width = 45;
			// 
			// dataGridViewCheckBoxColumn30
			// 
			this.dataGridViewCheckBoxColumn30.DataPropertyName = "appliI";
			this.dataGridViewCheckBoxColumn30.HeaderText = "ｱﾌﾟﾘI";
			this.dataGridViewCheckBoxColumn30.MinimumWidth = 45;
			this.dataGridViewCheckBoxColumn30.Name = "dataGridViewCheckBoxColumn30";
			this.dataGridViewCheckBoxColumn30.Width = 45;
			// 
			// dataGridViewCheckBoxColumn31
			// 
			this.dataGridViewCheckBoxColumn31.DataPropertyName = "appliJ";
			this.dataGridViewCheckBoxColumn31.HeaderText = "ｱﾌﾟﾘJ";
			this.dataGridViewCheckBoxColumn31.MinimumWidth = 45;
			this.dataGridViewCheckBoxColumn31.Name = "dataGridViewCheckBoxColumn31";
			this.dataGridViewCheckBoxColumn31.Width = 45;
			// 
			// dataGridViewComboBoxColumn2
			// 
			this.dataGridViewComboBoxColumn2.DataPropertyName = "soundType";
			this.dataGridViewComboBoxColumn2.HeaderText = "音設定";
			this.dataGridViewComboBoxColumn2.Items.AddRange(new object[] {
									"ﾃﾞﾌｫﾙﾄ",
									"音A",
									"音B",
									"音C"});
			this.dataGridViewComboBoxColumn2.Name = "dataGridViewComboBoxColumn2";
			this.dataGridViewComboBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewComboBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.dataGridViewComboBoxColumn2.Width = 67;
			// 
			// dataGridViewTextBoxColumn32
			// 
			this.dataGridViewTextBoxColumn32.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridViewTextBoxColumn32.DataPropertyName = "memo";
			this.dataGridViewTextBoxColumn32.HeaderText = "ﾒﾓ";
			this.dataGridViewTextBoxColumn32.MinimumWidth = 30;
			this.dataGridViewTextBoxColumn32.Name = "dataGridViewTextBoxColumn32";
			// 
			// contextMenuStrip4
			// 
			this.contextMenuStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.toolStripMenuItem10,
									this.toolStripMenuItem32,
									this.toolStripSeparator30,
									this.toolStripMenuItem33,
									this.toolStripMenuItem35,
									this.toolStripSeparator31,
									this.toolStripMenuItem36,
									this.toolStripMenuItem37,
									this.toolStripSeparator32,
									this.openAppliAUserFavoriteMenu,
									this.openAppliBUserFavoriteMenu,
									this.openAppliCUserFavoriteMenu,
									this.openAppliDUserFavoriteMenu,
									this.openAppliEUserFavoriteMenu,
									this.openAppliFUserFavoriteMenu,
									this.openAppliGUserFavoriteMenu,
									this.openAppliHUserFavoriteMenu,
									this.openAppliIUserFavoriteMenu,
									this.openAppliJUserFavoriteMenu});
			this.contextMenuStrip4.Name = "contextMenuStrip1";
			this.contextMenuStrip4.Size = new System.Drawing.Size(302, 374);
			// 
			// toolStripMenuItem10
			// 
			this.toolStripMenuItem10.Name = "toolStripMenuItem10";
			this.toolStripMenuItem10.Size = new System.Drawing.Size(301, 22);
			this.toolStripMenuItem10.Text = "最近行われた放送のURLを開く";
			this.toolStripMenuItem10.Click += new System.EventHandler(this.OpenLastHosoClick);
			// 
			// toolStripMenuItem32
			// 
			this.toolStripMenuItem32.Name = "toolStripMenuItem32";
			this.toolStripMenuItem32.Size = new System.Drawing.Size(301, 22);
			this.toolStripMenuItem32.Text = "ユーザーURLを開く";
			this.toolStripMenuItem32.Click += new System.EventHandler(this.OpenUserUrlClick);
			// 
			// toolStripSeparator30
			// 
			this.toolStripSeparator30.Name = "toolStripSeparator30";
			this.toolStripSeparator30.Size = new System.Drawing.Size(298, 6);
			// 
			// toolStripMenuItem33
			// 
			this.toolStripMenuItem33.Name = "toolStripMenuItem33";
			this.toolStripMenuItem33.Size = new System.Drawing.Size(301, 22);
			this.toolStripMenuItem33.Text = "最近行われた放送のURLをコピー";
			this.toolStripMenuItem33.Click += new System.EventHandler(this.CopyLastHosoMenuClick);
			// 
			// toolStripMenuItem35
			// 
			this.toolStripMenuItem35.Name = "toolStripMenuItem35";
			this.toolStripMenuItem35.Size = new System.Drawing.Size(301, 22);
			this.toolStripMenuItem35.Text = "ユーザーURLをコピー";
			this.toolStripMenuItem35.Click += new System.EventHandler(this.CopyUserUrlMenuClick);
			// 
			// toolStripSeparator31
			// 
			this.toolStripSeparator31.Name = "toolStripSeparator31";
			this.toolStripSeparator31.Size = new System.Drawing.Size(298, 6);
			// 
			// toolStripMenuItem36
			// 
			this.toolStripMenuItem36.Name = "toolStripMenuItem36";
			this.toolStripMenuItem36.Size = new System.Drawing.Size(301, 22);
			this.toolStripMenuItem36.Text = "この行を編集";
			this.toolStripMenuItem36.Click += new System.EventHandler(this.EditLineMenuClick);
			// 
			// toolStripMenuItem37
			// 
			this.toolStripMenuItem37.Name = "toolStripMenuItem37";
			this.toolStripMenuItem37.Size = new System.Drawing.Size(301, 22);
			this.toolStripMenuItem37.Text = "この行を削除";
			this.toolStripMenuItem37.Click += new System.EventHandler(this.RemoveLineMenuClick);
			// 
			// toolStripSeparator32
			// 
			this.toolStripSeparator32.Name = "toolStripSeparator32";
			this.toolStripSeparator32.Size = new System.Drawing.Size(298, 6);
			// 
			// openAppliAUserFavoriteMenu
			// 
			this.openAppliAUserFavoriteMenu.Name = "openAppliAUserFavoriteMenu";
			this.openAppliAUserFavoriteMenu.Size = new System.Drawing.Size(301, 22);
			this.openAppliAUserFavoriteMenu.Text = "最近行われた放送のURLをアプリAで開く";
			this.openAppliAUserFavoriteMenu.Click += new System.EventHandler(this.recentLiveAppliOpenMenu_Click);
			// 
			// openAppliBUserFavoriteMenu
			// 
			this.openAppliBUserFavoriteMenu.Name = "openAppliBUserFavoriteMenu";
			this.openAppliBUserFavoriteMenu.Size = new System.Drawing.Size(301, 22);
			this.openAppliBUserFavoriteMenu.Text = "最近行われた放送のURLをアプリBで開く";
			this.openAppliBUserFavoriteMenu.Click += new System.EventHandler(this.recentLiveAppliOpenMenu_Click);
			// 
			// openAppliCUserFavoriteMenu
			// 
			this.openAppliCUserFavoriteMenu.Name = "openAppliCUserFavoriteMenu";
			this.openAppliCUserFavoriteMenu.Size = new System.Drawing.Size(301, 22);
			this.openAppliCUserFavoriteMenu.Text = "最近行われた放送のURLをアプリCで開く";
			this.openAppliCUserFavoriteMenu.Click += new System.EventHandler(this.recentLiveAppliOpenMenu_Click);
			// 
			// openAppliDUserFavoriteMenu
			// 
			this.openAppliDUserFavoriteMenu.Name = "openAppliDUserFavoriteMenu";
			this.openAppliDUserFavoriteMenu.Size = new System.Drawing.Size(301, 22);
			this.openAppliDUserFavoriteMenu.Text = "最近行われた放送のURLをアプリDで開く";
			this.openAppliDUserFavoriteMenu.Click += new System.EventHandler(this.recentLiveAppliOpenMenu_Click);
			// 
			// openAppliEUserFavoriteMenu
			// 
			this.openAppliEUserFavoriteMenu.Name = "openAppliEUserFavoriteMenu";
			this.openAppliEUserFavoriteMenu.Size = new System.Drawing.Size(301, 22);
			this.openAppliEUserFavoriteMenu.Text = "最近行われた放送のURLをアプリEで開く";
			this.openAppliEUserFavoriteMenu.Click += new System.EventHandler(this.recentLiveAppliOpenMenu_Click);
			// 
			// openAppliFUserFavoriteMenu
			// 
			this.openAppliFUserFavoriteMenu.Name = "openAppliFUserFavoriteMenu";
			this.openAppliFUserFavoriteMenu.Size = new System.Drawing.Size(301, 22);
			this.openAppliFUserFavoriteMenu.Text = "最近行われた放送のURLをアプリFで開く";
			this.openAppliFUserFavoriteMenu.Click += new System.EventHandler(this.recentLiveAppliOpenMenu_Click);
			// 
			// openAppliGUserFavoriteMenu
			// 
			this.openAppliGUserFavoriteMenu.Name = "openAppliGUserFavoriteMenu";
			this.openAppliGUserFavoriteMenu.Size = new System.Drawing.Size(301, 22);
			this.openAppliGUserFavoriteMenu.Text = "最近行われた放送のURLをアプリGで開く";
			this.openAppliGUserFavoriteMenu.Click += new System.EventHandler(this.recentLiveAppliOpenMenu_Click);
			// 
			// openAppliHUserFavoriteMenu
			// 
			this.openAppliHUserFavoriteMenu.Name = "openAppliHUserFavoriteMenu";
			this.openAppliHUserFavoriteMenu.Size = new System.Drawing.Size(301, 22);
			this.openAppliHUserFavoriteMenu.Text = "最近行われた放送のURLをアプリHで開く";
			this.openAppliHUserFavoriteMenu.Click += new System.EventHandler(this.recentLiveAppliOpenMenu_Click);
			// 
			// openAppliIUserFavoriteMenu
			// 
			this.openAppliIUserFavoriteMenu.Name = "openAppliIUserFavoriteMenu";
			this.openAppliIUserFavoriteMenu.Size = new System.Drawing.Size(301, 22);
			this.openAppliIUserFavoriteMenu.Text = "最近行われた放送のURLをアプリIで開く";
			this.openAppliIUserFavoriteMenu.Click += new System.EventHandler(this.recentLiveAppliOpenMenu_Click);
			// 
			// openAppliJUserFavoriteMenu
			// 
			this.openAppliJUserFavoriteMenu.Name = "openAppliJUserFavoriteMenu";
			this.openAppliJUserFavoriteMenu.Size = new System.Drawing.Size(301, 22);
			this.openAppliJUserFavoriteMenu.Text = "最近行われた放送のURLをアプリJで開く";
			this.openAppliJUserFavoriteMenu.Click += new System.EventHandler(this.recentLiveAppliOpenMenu_Click);
			// 
			// favoriteCommunityBtn
			// 
			this.favoriteCommunityBtn.Appearance = System.Windows.Forms.Appearance.Button;
			this.favoriteCommunityBtn.BackColor = System.Drawing.SystemColors.Control;
			this.favoriteCommunityBtn.Checked = true;
			this.favoriteCommunityBtn.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
			this.favoriteCommunityBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.favoriteCommunityBtn.Location = new System.Drawing.Point(0, 0);
			this.favoriteCommunityBtn.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.favoriteCommunityBtn.Name = "favoriteCommunityBtn";
			this.favoriteCommunityBtn.Size = new System.Drawing.Size(76, 23);
			this.favoriteCommunityBtn.TabIndex = 34;
			this.favoriteCommunityBtn.TabStop = true;
			this.favoriteCommunityBtn.Text = "コミュニティID";
			this.favoriteCommunityBtn.UseCompatibleTextRendering = true;
			this.favoriteCommunityBtn.UseMnemonic = false;
			this.favoriteCommunityBtn.UseVisualStyleBackColor = true;
			this.favoriteCommunityBtn.CheckedChanged += new System.EventHandler(this.FavoriteListRadioBtnCheckedChanged);
			// 
			// favoriteUserBtn
			// 
			this.favoriteUserBtn.Appearance = System.Windows.Forms.Appearance.Button;
			this.favoriteUserBtn.BackColor = System.Drawing.SystemColors.Control;
			this.favoriteUserBtn.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
			this.favoriteUserBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.favoriteUserBtn.Location = new System.Drawing.Point(79, 0);
			this.favoriteUserBtn.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.favoriteUserBtn.Name = "favoriteUserBtn";
			this.favoriteUserBtn.Size = new System.Drawing.Size(67, 23);
			this.favoriteUserBtn.TabIndex = 33;
			this.favoriteUserBtn.Text = "ユーザーID";
			this.favoriteUserBtn.UseCompatibleTextRendering = true;
			this.favoriteUserBtn.UseMnemonic = false;
			this.favoriteUserBtn.UseVisualStyleBackColor = true;
			this.favoriteUserBtn.CheckedChanged += new System.EventHandler(this.FavoriteListRadioBtnCheckedChanged);
			// 
			// tabPage2
			// 
			this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
			this.tabPage2.Controls.Add(this.addYoyakuBtn);
			this.tabPage2.Controls.Add(this.taskList);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(878, 362);
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
			this.taskList.Size = new System.Drawing.Size(864, 296);
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
			// tabPage4
			// 
			this.tabPage4.BackColor = System.Drawing.SystemColors.Control;
			this.tabPage4.Controls.Add(this.historySplitContainer);
			this.tabPage4.Location = new System.Drawing.Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Size = new System.Drawing.Size(878, 362);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "通知履歴";
			// 
			// historySplitContainer
			// 
			this.historySplitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.historySplitContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.historySplitContainer.Location = new System.Drawing.Point(6, 0);
			this.historySplitContainer.Name = "historySplitContainer";
			this.historySplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// historySplitContainer.Panel1
			// 
			this.historySplitContainer.Panel1.Controls.Add(this.label1);
			this.historySplitContainer.Panel1.Controls.Add(this.historyList);
			// 
			// historySplitContainer.Panel2
			// 
			this.historySplitContainer.Panel2.Controls.Add(this.label3);
			this.historySplitContainer.Panel2.Controls.Add(this.notAlartList);
			this.historySplitContainer.Size = new System.Drawing.Size(871, 359);
			this.historySplitContainer.SplitterDistance = 170;
			this.historySplitContainer.TabIndex = 30;
			this.historySplitContainer.Layout += new System.Windows.Forms.LayoutEventHandler(this.HistorySplitContainerLayout);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(0, 3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(152, 14);
			this.label1.TabIndex = 26;
			this.label1.Text = "通知履歴";
			// 
			// historyList
			// 
			this.historyList.AllowDrop = true;
			this.historyList.AllowUserToAddRows = false;
			this.historyList.AllowUserToDeleteRows = false;
			this.historyList.AllowUserToResizeRows = false;
			this.historyList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.historyList.ColumnHeadersHeight = 25;
			this.historyList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.historyList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
									this.放送開始日時,
									this.放送タイトル,
									this.放送者,
									this.コミュニティ名,
									this.放送ID,
									this.ユーザーID,
									this.コミュニティID,
									this.historyKeyword,
									this.お気に入り,
									this.説明});
			this.historyList.ContextMenuStrip = this.historyListMenu;
			this.historyList.Location = new System.Drawing.Point(0, 20);
			this.historyList.MultiSelect = false;
			this.historyList.Name = "historyList";
			this.historyList.ReadOnly = true;
			this.historyList.RowHeadersVisible = false;
			this.historyList.RowTemplate.Height = 21;
			this.historyList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.historyList.Size = new System.Drawing.Size(867, 143);
			this.historyList.TabIndex = 25;
			this.historyList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.HistoryListCellFormatting);
			this.historyList.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.HistoryListCellMouseDown);
			this.historyList.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.HistoryListRowsAdded);
			// 
			// 放送開始日時
			// 
			this.放送開始日時.DataPropertyName = "dt";
			this.放送開始日時.HeaderText = "放送開始日時";
			this.放送開始日時.Name = "放送開始日時";
			this.放送開始日時.ReadOnly = true;
			this.放送開始日時.Width = 115;
			// 
			// 放送タイトル
			// 
			this.放送タイトル.DataPropertyName = "title";
			this.放送タイトル.HeaderText = "放送タイトル";
			this.放送タイトル.Name = "放送タイトル";
			this.放送タイトル.ReadOnly = true;
			// 
			// 放送者
			// 
			this.放送者.DataPropertyName = "userName";
			this.放送者.HeaderText = "放送者";
			this.放送者.Name = "放送者";
			this.放送者.ReadOnly = true;
			// 
			// コミュニティ名
			// 
			this.コミュニティ名.DataPropertyName = "communityName";
			this.コミュニティ名.HeaderText = "コミュニティ名";
			this.コミュニティ名.Name = "コミュニティ名";
			this.コミュニティ名.ReadOnly = true;
			// 
			// 放送ID
			// 
			this.放送ID.DataPropertyName = "lvid";
			this.放送ID.HeaderText = "放送ID";
			this.放送ID.Name = "放送ID";
			this.放送ID.ReadOnly = true;
			// 
			// ユーザーID
			// 
			this.ユーザーID.DataPropertyName = "userId";
			this.ユーザーID.HeaderText = "ユーザーID";
			this.ユーザーID.Name = "ユーザーID";
			this.ユーザーID.ReadOnly = true;
			// 
			// コミュニティID
			// 
			this.コミュニティID.DataPropertyName = "communityId";
			this.コミュニティID.HeaderText = "コミュニティID";
			this.コミュニティID.Name = "コミュニティID";
			this.コミュニティID.ReadOnly = true;
			// 
			// historyKeyword
			// 
			this.historyKeyword.DataPropertyName = "keyword";
			this.historyKeyword.HeaderText = "キーワード";
			this.historyKeyword.Name = "historyKeyword";
			this.historyKeyword.ReadOnly = true;
			this.historyKeyword.Visible = false;
			// 
			// お気に入り
			// 
			this.お気に入り.DataPropertyName = "favorite";
			this.お気に入り.HeaderText = "お気に入り";
			this.お気に入り.Name = "お気に入り";
			this.お気に入り.ReadOnly = true;
			// 
			// 説明
			// 
			this.説明.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.説明.DataPropertyName = "description";
			this.説明.HeaderText = "説明";
			this.説明.MinimumWidth = 30;
			this.説明.Name = "説明";
			this.説明.ReadOnly = true;
			// 
			// historyListMenu
			// 
			this.historyListMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.historyListOpenUrlMenu,
									this.historyListOpenCommunityUrlMenu,
									this.historyListOpenUserUrlMenu,
									this.toolStripSeparator24,
									this.historyListCopyUrlMenu,
									this.historyListCopyCommunityUrlMenu,
									this.historyListCopyUserUrlMenu,
									this.toolStripSeparator25,
									this.historyListAddAlartListMenu,
									this.toolStripSeparator27,
									this.historyListDeleteRowMenu});
			this.historyListMenu.Name = "logListMenu";
			this.historyListMenu.Size = new System.Drawing.Size(221, 198);
			// 
			// historyListOpenUrlMenu
			// 
			this.historyListOpenUrlMenu.Name = "historyListOpenUrlMenu";
			this.historyListOpenUrlMenu.Size = new System.Drawing.Size(220, 22);
			this.historyListOpenUrlMenu.Text = "放送URLを開く";
			this.historyListOpenUrlMenu.Click += new System.EventHandler(this.HistoryListOpenUrlMenuClick);
			// 
			// historyListOpenCommunityUrlMenu
			// 
			this.historyListOpenCommunityUrlMenu.Name = "historyListOpenCommunityUrlMenu";
			this.historyListOpenCommunityUrlMenu.Size = new System.Drawing.Size(220, 22);
			this.historyListOpenCommunityUrlMenu.Text = "コミュニティURLを開く";
			this.historyListOpenCommunityUrlMenu.Click += new System.EventHandler(this.HistoryListOpenCommunityUrlMenuClick);
			// 
			// historyListOpenUserUrlMenu
			// 
			this.historyListOpenUserUrlMenu.Name = "historyListOpenUserUrlMenu";
			this.historyListOpenUserUrlMenu.Size = new System.Drawing.Size(220, 22);
			this.historyListOpenUserUrlMenu.Text = "ユーザーURLを開く";
			this.historyListOpenUserUrlMenu.Visible = false;
			this.historyListOpenUserUrlMenu.Click += new System.EventHandler(this.HistoryListOpenUserUrlMenuClick);
			// 
			// toolStripSeparator24
			// 
			this.toolStripSeparator24.Name = "toolStripSeparator24";
			this.toolStripSeparator24.Size = new System.Drawing.Size(217, 6);
			// 
			// historyListCopyUrlMenu
			// 
			this.historyListCopyUrlMenu.Name = "historyListCopyUrlMenu";
			this.historyListCopyUrlMenu.Size = new System.Drawing.Size(220, 22);
			this.historyListCopyUrlMenu.Text = "URLをコピー";
			this.historyListCopyUrlMenu.Click += new System.EventHandler(this.HistoryListCopyUrlMenuClick);
			// 
			// historyListCopyCommunityUrlMenu
			// 
			this.historyListCopyCommunityUrlMenu.Name = "historyListCopyCommunityUrlMenu";
			this.historyListCopyCommunityUrlMenu.Size = new System.Drawing.Size(220, 22);
			this.historyListCopyCommunityUrlMenu.Text = "コミュニティURLをコピー";
			this.historyListCopyCommunityUrlMenu.Click += new System.EventHandler(this.HistoryListCopyCommunityUrlMenuClick);
			// 
			// historyListCopyUserUrlMenu
			// 
			this.historyListCopyUserUrlMenu.Name = "historyListCopyUserUrlMenu";
			this.historyListCopyUserUrlMenu.Size = new System.Drawing.Size(220, 22);
			this.historyListCopyUserUrlMenu.Text = "ユーザーURLをコピー";
			this.historyListCopyUserUrlMenu.Visible = false;
			this.historyListCopyUserUrlMenu.Click += new System.EventHandler(this.HistoryListCopyUserUrlMenuClick);
			// 
			// toolStripSeparator25
			// 
			this.toolStripSeparator25.Name = "toolStripSeparator25";
			this.toolStripSeparator25.Size = new System.Drawing.Size(217, 6);
			// 
			// historyListAddAlartListMenu
			// 
			this.historyListAddAlartListMenu.Name = "historyListAddAlartListMenu";
			this.historyListAddAlartListMenu.Size = new System.Drawing.Size(220, 22);
			this.historyListAddAlartListMenu.Text = "お気に入りに登録する";
			this.historyListAddAlartListMenu.Click += new System.EventHandler(this.HistoryListAddAlartListMenuClick);
			// 
			// toolStripSeparator27
			// 
			this.toolStripSeparator27.Name = "toolStripSeparator27";
			this.toolStripSeparator27.Size = new System.Drawing.Size(217, 6);
			// 
			// historyListDeleteRowMenu
			// 
			this.historyListDeleteRowMenu.Name = "historyListDeleteRowMenu";
			this.historyListDeleteRowMenu.Size = new System.Drawing.Size(220, 22);
			this.historyListDeleteRowMenu.Text = "この行を削除する";
			this.historyListDeleteRowMenu.Click += new System.EventHandler(this.HistoryListDeleteRowMenuClick);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(0, 3);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(595, 14);
			this.label3.TabIndex = 28;
			this.label3.Text = "通知しなかった履歴(コミュニティまたはユーザーの条件に適合していたが、その他の条件が外れていたため通知しなかった放送)";
			// 
			// notAlartList
			// 
			this.notAlartList.AllowDrop = true;
			this.notAlartList.AllowUserToAddRows = false;
			this.notAlartList.AllowUserToDeleteRows = false;
			this.notAlartList.AllowUserToResizeRows = false;
			this.notAlartList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.notAlartList.ColumnHeadersHeight = 25;
			this.notAlartList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.notAlartList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
									this.dataGridViewTextBoxColumn16,
									this.dataGridViewTextBoxColumn17,
									this.dataGridViewTextBoxColumn18,
									this.dataGridViewTextBoxColumn19,
									this.dataGridViewTextBoxColumn20,
									this.dataGridViewTextBoxColumn21,
									this.dataGridViewTextBoxColumn22,
									this.キーワード,
									this.dataGridViewTextBoxColumn23,
									this.dataGridViewTextBoxColumn24});
			this.notAlartList.ContextMenuStrip = this.notAlartListMenu;
			this.notAlartList.Location = new System.Drawing.Point(0, 20);
			this.notAlartList.MultiSelect = false;
			this.notAlartList.Name = "notAlartList";
			this.notAlartList.ReadOnly = true;
			this.notAlartList.RowHeadersVisible = false;
			this.notAlartList.RowTemplate.Height = 21;
			this.notAlartList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.notAlartList.Size = new System.Drawing.Size(867, 161);
			this.notAlartList.TabIndex = 29;
			this.notAlartList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.NotAlartListCellFormatting);
			this.notAlartList.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.NotAlartListCellMouseDown);
			this.notAlartList.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.NotAlartListRowsAdded);
			// 
			// dataGridViewTextBoxColumn16
			// 
			this.dataGridViewTextBoxColumn16.DataPropertyName = "dt";
			this.dataGridViewTextBoxColumn16.HeaderText = "放送開始日時";
			this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
			this.dataGridViewTextBoxColumn16.ReadOnly = true;
			this.dataGridViewTextBoxColumn16.Width = 115;
			// 
			// dataGridViewTextBoxColumn17
			// 
			this.dataGridViewTextBoxColumn17.DataPropertyName = "title";
			this.dataGridViewTextBoxColumn17.HeaderText = "放送タイトル";
			this.dataGridViewTextBoxColumn17.Name = "dataGridViewTextBoxColumn17";
			this.dataGridViewTextBoxColumn17.ReadOnly = true;
			// 
			// dataGridViewTextBoxColumn18
			// 
			this.dataGridViewTextBoxColumn18.DataPropertyName = "userName";
			this.dataGridViewTextBoxColumn18.HeaderText = "放送者";
			this.dataGridViewTextBoxColumn18.Name = "dataGridViewTextBoxColumn18";
			this.dataGridViewTextBoxColumn18.ReadOnly = true;
			// 
			// dataGridViewTextBoxColumn19
			// 
			this.dataGridViewTextBoxColumn19.DataPropertyName = "communityName";
			this.dataGridViewTextBoxColumn19.HeaderText = "コミュニティ名";
			this.dataGridViewTextBoxColumn19.Name = "dataGridViewTextBoxColumn19";
			this.dataGridViewTextBoxColumn19.ReadOnly = true;
			// 
			// dataGridViewTextBoxColumn20
			// 
			this.dataGridViewTextBoxColumn20.DataPropertyName = "lvid";
			this.dataGridViewTextBoxColumn20.HeaderText = "放送ID";
			this.dataGridViewTextBoxColumn20.Name = "dataGridViewTextBoxColumn20";
			this.dataGridViewTextBoxColumn20.ReadOnly = true;
			// 
			// dataGridViewTextBoxColumn21
			// 
			this.dataGridViewTextBoxColumn21.DataPropertyName = "userId";
			this.dataGridViewTextBoxColumn21.HeaderText = "ユーザーID";
			this.dataGridViewTextBoxColumn21.Name = "dataGridViewTextBoxColumn21";
			this.dataGridViewTextBoxColumn21.ReadOnly = true;
			// 
			// dataGridViewTextBoxColumn22
			// 
			this.dataGridViewTextBoxColumn22.DataPropertyName = "communityId";
			this.dataGridViewTextBoxColumn22.HeaderText = "コミュニティID";
			this.dataGridViewTextBoxColumn22.Name = "dataGridViewTextBoxColumn22";
			this.dataGridViewTextBoxColumn22.ReadOnly = true;
			// 
			// キーワード
			// 
			this.キーワード.DataPropertyName = "keyword";
			this.キーワード.HeaderText = "キーワード";
			this.キーワード.Name = "キーワード";
			this.キーワード.ReadOnly = true;
			// 
			// dataGridViewTextBoxColumn23
			// 
			this.dataGridViewTextBoxColumn23.DataPropertyName = "favorite";
			this.dataGridViewTextBoxColumn23.HeaderText = "お気に入り";
			this.dataGridViewTextBoxColumn23.Name = "dataGridViewTextBoxColumn23";
			this.dataGridViewTextBoxColumn23.ReadOnly = true;
			// 
			// dataGridViewTextBoxColumn24
			// 
			this.dataGridViewTextBoxColumn24.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridViewTextBoxColumn24.DataPropertyName = "description";
			this.dataGridViewTextBoxColumn24.HeaderText = "説明";
			this.dataGridViewTextBoxColumn24.MinimumWidth = 30;
			this.dataGridViewTextBoxColumn24.Name = "dataGridViewTextBoxColumn24";
			this.dataGridViewTextBoxColumn24.ReadOnly = true;
			// 
			// notAlartListMenu
			// 
			this.notAlartListMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.notAlartListOpenUrlMenu,
									this.notAlartListOpenCommunityUrlMenu,
									this.notAlartListOpenUserUrlMenu,
									this.toolStripSeparator22,
									this.notAlartListCopyUrlMenu,
									this.notAlartListCopyCommunityUrlMenu,
									this.notAlartListCopyUserUrlMenu,
									this.toolStripSeparator26,
									this.notAlartListAddAlartListMenu,
									this.toolStripSeparator28,
									this.notAlartListDeleteRowMenu});
			this.notAlartListMenu.Name = "logListMenu";
			this.notAlartListMenu.Size = new System.Drawing.Size(221, 198);
			// 
			// notAlartListOpenUrlMenu
			// 
			this.notAlartListOpenUrlMenu.Name = "notAlartListOpenUrlMenu";
			this.notAlartListOpenUrlMenu.Size = new System.Drawing.Size(220, 22);
			this.notAlartListOpenUrlMenu.Text = "放送URLを開く";
			this.notAlartListOpenUrlMenu.Click += new System.EventHandler(this.NotAlartListOpenUrlMenuClick);
			// 
			// notAlartListOpenCommunityUrlMenu
			// 
			this.notAlartListOpenCommunityUrlMenu.Name = "notAlartListOpenCommunityUrlMenu";
			this.notAlartListOpenCommunityUrlMenu.Size = new System.Drawing.Size(220, 22);
			this.notAlartListOpenCommunityUrlMenu.Text = "コミュニティURLを開く";
			this.notAlartListOpenCommunityUrlMenu.Click += new System.EventHandler(this.NotAlartListOpenCommunityUrlMenuClick);
			// 
			// notAlartListOpenUserUrlMenu
			// 
			this.notAlartListOpenUserUrlMenu.Name = "notAlartListOpenUserUrlMenu";
			this.notAlartListOpenUserUrlMenu.Size = new System.Drawing.Size(220, 22);
			this.notAlartListOpenUserUrlMenu.Text = "ユーザーURLを開く";
			this.notAlartListOpenUserUrlMenu.Visible = false;
			this.notAlartListOpenUserUrlMenu.Click += new System.EventHandler(this.NotAlartListOpenUserUrlMenuClick);
			// 
			// toolStripSeparator22
			// 
			this.toolStripSeparator22.Name = "toolStripSeparator22";
			this.toolStripSeparator22.Size = new System.Drawing.Size(217, 6);
			// 
			// notAlartListCopyUrlMenu
			// 
			this.notAlartListCopyUrlMenu.Name = "notAlartListCopyUrlMenu";
			this.notAlartListCopyUrlMenu.Size = new System.Drawing.Size(220, 22);
			this.notAlartListCopyUrlMenu.Text = "URLをコピー";
			this.notAlartListCopyUrlMenu.Click += new System.EventHandler(this.NotAlartListCopyUrlMenuClick);
			// 
			// notAlartListCopyCommunityUrlMenu
			// 
			this.notAlartListCopyCommunityUrlMenu.Name = "notAlartListCopyCommunityUrlMenu";
			this.notAlartListCopyCommunityUrlMenu.Size = new System.Drawing.Size(220, 22);
			this.notAlartListCopyCommunityUrlMenu.Text = "コミュニティURLをコピー";
			this.notAlartListCopyCommunityUrlMenu.Click += new System.EventHandler(this.NotAlartListCopyCommunityUrlMenuClick);
			// 
			// notAlartListCopyUserUrlMenu
			// 
			this.notAlartListCopyUserUrlMenu.Name = "notAlartListCopyUserUrlMenu";
			this.notAlartListCopyUserUrlMenu.Size = new System.Drawing.Size(220, 22);
			this.notAlartListCopyUserUrlMenu.Text = "ユーザーURLをコピー";
			this.notAlartListCopyUserUrlMenu.Visible = false;
			this.notAlartListCopyUserUrlMenu.Click += new System.EventHandler(this.NotAlartListCopyUserUrlMenuClick);
			// 
			// toolStripSeparator26
			// 
			this.toolStripSeparator26.Name = "toolStripSeparator26";
			this.toolStripSeparator26.Size = new System.Drawing.Size(217, 6);
			// 
			// notAlartListAddAlartListMenu
			// 
			this.notAlartListAddAlartListMenu.Name = "notAlartListAddAlartListMenu";
			this.notAlartListAddAlartListMenu.Size = new System.Drawing.Size(220, 22);
			this.notAlartListAddAlartListMenu.Text = "お気に入りに登録する";
			this.notAlartListAddAlartListMenu.Click += new System.EventHandler(this.NotAlartListAddAlartListMenuClick);
			// 
			// toolStripSeparator28
			// 
			this.toolStripSeparator28.Name = "toolStripSeparator28";
			this.toolStripSeparator28.Size = new System.Drawing.Size(217, 6);
			// 
			// notAlartListDeleteRowMenu
			// 
			this.notAlartListDeleteRowMenu.Name = "notAlartListDeleteRowMenu";
			this.notAlartListDeleteRowMenu.Size = new System.Drawing.Size(220, 22);
			this.notAlartListDeleteRowMenu.Text = "この行を削除する";
			this.notAlartListDeleteRowMenu.Click += new System.EventHandler(this.NotAlartListDeleteRowMenuClick);
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label5.Location = new System.Drawing.Point(594, 26);
			this.label5.Name = "label5";
			this.label5.Padding = new System.Windows.Forms.Padding(1);
			this.label5.Size = new System.Drawing.Size(110, 15);
			this.label5.TabIndex = 31;
			this.label5.Text = "スマホ通知受信中";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.label5.Visible = false;
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label4.Location = new System.Drawing.Point(594, 10);
			this.label4.Name = "label4";
			this.label4.Padding = new System.Windows.Forms.Padding(1);
			this.label4.Size = new System.Drawing.Size(110, 15);
			this.label4.TabIndex = 31;
			this.label4.Text = "ブラウザ通知受信中";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.label4.Visible = false;
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label2.Location = new System.Drawing.Point(594, -6);
			this.label2.Name = "label2";
			this.label2.Padding = new System.Windows.Forms.Padding(1);
			this.label2.Size = new System.Drawing.Size(110, 15);
			this.label2.TabIndex = 31;
			this.label2.Text = "RSS受信中";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.label2.Visible = false;
			// 
			// logList
			// 
			this.logList.AllowDrop = true;
			this.logList.AllowUserToAddRows = false;
			this.logList.AllowUserToDeleteRows = false;
			this.logList.AllowUserToResizeRows = false;
			this.logList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.logList.ColumnHeadersHeight = 17;
			this.logList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.logList.ColumnHeadersVisible = false;
			this.logList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
									this.dataGridViewTextBoxColumn12,
									this.dataGridViewTextBoxColumn15});
			this.logList.ContextMenuStrip = this.logListMenu;
			this.logList.Location = new System.Drawing.Point(677, -10);
			this.logList.MultiSelect = false;
			this.logList.Name = "logList";
			this.logList.RowHeadersVisible = false;
			this.logList.RowHeadersWidth = 35;
			this.logList.RowTemplate.Height = 18;
			this.logList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.logList.Size = new System.Drawing.Size(170, 55);
			this.logList.TabIndex = 30;
			this.logList.Visible = false;
			// 
			// dataGridViewTextBoxColumn12
			// 
			this.dataGridViewTextBoxColumn12.DataPropertyName = "dt";
			this.dataGridViewTextBoxColumn12.HeaderText = "日時";
			this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
			this.dataGridViewTextBoxColumn12.ReadOnly = true;
			this.dataGridViewTextBoxColumn12.Visible = false;
			this.dataGridViewTextBoxColumn12.Width = 115;
			// 
			// dataGridViewTextBoxColumn15
			// 
			this.dataGridViewTextBoxColumn15.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridViewTextBoxColumn15.DataPropertyName = "msg";
			this.dataGridViewTextBoxColumn15.HeaderText = "メッセージ";
			this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
			this.dataGridViewTextBoxColumn15.ReadOnly = true;
			// 
			// logListMenu
			// 
			this.logListMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.logListCopyMessageMenu,
									this.toolStripSeparator23,
									this.logListDeleteRowMenu});
			this.logListMenu.Name = "logListMenu";
			this.logListMenu.Size = new System.Drawing.Size(185, 54);
			this.logListMenu.Opening += new System.ComponentModel.CancelEventHandler(this.LogListMenuOpening);
			// 
			// logListCopyMessageMenu
			// 
			this.logListCopyMessageMenu.Name = "logListCopyMessageMenu";
			this.logListCopyMessageMenu.Size = new System.Drawing.Size(184, 22);
			this.logListCopyMessageMenu.Text = "メッセージをコピー";
			this.logListCopyMessageMenu.Click += new System.EventHandler(this.LogListCopyMessageMenuClick);
			// 
			// toolStripSeparator23
			// 
			this.toolStripSeparator23.Name = "toolStripSeparator23";
			this.toolStripSeparator23.Size = new System.Drawing.Size(181, 6);
			// 
			// logListDeleteRowMenu
			// 
			this.logListDeleteRowMenu.Name = "logListDeleteRowMenu";
			this.logListDeleteRowMenu.Size = new System.Drawing.Size(184, 22);
			this.logListDeleteRowMenu.Text = "この行を削除する";
			this.logListDeleteRowMenu.Click += new System.EventHandler(this.LogListDeleteRowMenuClick);
			// 
			// MainForm
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(899, 442);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.logList);
			this.Controls.Add(this.TabPages);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.menuStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "MainForm";
			this.Text = "ニコ生放送チェックツール（仮 ver0.86.15";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.form_Close);
			this.Load += new System.EventHandler(this.mainForm_Load);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.alartList)).EndInit();
			this.contextMenuStrip1.ResumeLayout(false);
			this.notifyIconMenuStrip.ResumeLayout(false);
			this.TabPages.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.tabPage3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.liveList)).EndInit();
			this.contextMenuStrip3.ResumeLayout(false);
			this.categoryBtnPanel.ResumeLayout(false);
			this.categoryBtnPanel.PerformLayout();
			this.tabPage1.ResumeLayout(false);
			this.favoriteCommunityPanel.ResumeLayout(false);
			this.favoriteCommunityPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.userThumbBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.comThumbBox)).EndInit();
			this.favoriteUserPanel.ResumeLayout(false);
			this.favoriteUserPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.favoriteUserThumbBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.userAlartList)).EndInit();
			this.contextMenuStrip4.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.taskList)).EndInit();
			this.contextMenuStrip2.ResumeLayout(false);
			this.tabPage4.ResumeLayout(false);
			this.historySplitContainer.Panel1.ResumeLayout(false);
			this.historySplitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.historySplitContainer)).EndInit();
			this.historySplitContainer.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.historyList)).EndInit();
			this.historyListMenu.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.notAlartList)).EndInit();
			this.notAlartListMenu.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.logList)).EndInit();
			this.logListMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.ToolStripMenuItem updateCateSuperIchibaMenu;
		private System.Windows.Forms.ToolStripMenuItem readNamarokuUserListMenu;
		private System.Windows.Forms.ToolStripMenuItem openAppliJUserFavoriteMenu;
		private System.Windows.Forms.ToolStripMenuItem openAppliIUserFavoriteMenu;
		private System.Windows.Forms.ToolStripMenuItem openAppliHUserFavoriteMenu;
		private System.Windows.Forms.ToolStripMenuItem openAppliGUserFavoriteMenu;
		private System.Windows.Forms.ToolStripMenuItem openAppliFUserFavoriteMenu;
		private System.Windows.Forms.ToolStripMenuItem openAppliEUserFavoriteMenu;
		private System.Windows.Forms.ToolStripMenuItem openAppliDUserFavoriteMenu;
		private System.Windows.Forms.ToolStripMenuItem openAppliCUserFavoriteMenu;
		private System.Windows.Forms.ToolStripMenuItem openAppliBUserFavoriteMenu;
		private System.Windows.Forms.ToolStripMenuItem openAppliAUserFavoriteMenu;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator32;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem37;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem36;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator31;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem35;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem33;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator30;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem32;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem10;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip4;
		private System.Windows.Forms.Label favoriteUserNumLabel;
		private System.Windows.Forms.PictureBox favoriteUserThumbBox;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn32;
		private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn2;
		private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn31;
		private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn30;
		private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn29;
		private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn28;
		private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn27;
		private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn26;
		private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn25;
		private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn24;
		private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn23;
		private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn22;
		private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn21;
		private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn20;
		private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn19;
		private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn18;
		private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn17;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn31;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn30;
		private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn4;
		private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn3;
		private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn29;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn28;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn27;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn26;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn25;
		public System.Windows.Forms.DataGridView userAlartList;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox userAddText;
		private System.Windows.Forms.Button userAddBtn;
		private System.Windows.Forms.Panel favoriteUserPanel;
		private System.Windows.Forms.Panel favoriteCommunityPanel;
		private System.Windows.Forms.RadioButton favoriteUserBtn;
		private System.Windows.Forms.RadioButton favoriteCommunityBtn;
		private System.Windows.Forms.ToolStripMenuItem updateOnlyFavoriteMenu;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator29;
		private System.Windows.Forms.ToolStripMenuItem duplicateCheckMenu;
		private System.Windows.Forms.TextBox logText;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem30;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem29;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem28;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem27;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem26;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem25;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem24;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem23;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem22;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem21;
		private System.Windows.Forms.ToolStripMenuItem colorHistoryColorColumnMenu;
		private System.Windows.Forms.SplitContainer historySplitContainer;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator28;
		private System.Windows.Forms.ToolStripMenuItem notAlartListAddAlartListMenu;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator27;
		private System.Windows.Forms.ToolStripMenuItem historyListAddAlartListMenu;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem20;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem19;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem18;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem17;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem16;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem15;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem14;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem13;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem12;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem11;
		private System.Windows.Forms.ToolStripMenuItem displayHistoryListMenu;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem9;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem displayHistoryDtMenu;
		private System.Windows.Forms.ToolStripMenuItem displayNotAlartListMenu;
		//private System.Windows.Forms.ToolStripMenuItem displayNotAlartListMenu;
		private System.Windows.Forms.DataGridViewTextBoxColumn historyKeyword;
		private System.Windows.Forms.DataGridViewTextBoxColumn キーワード;
		private System.Windows.Forms.ToolStripMenuItem getUserThumbBulkMenu;
		private System.Windows.Forms.ToolStripMenuItem getComThumbBulkMenu;
		private System.Windows.Forms.ToolStripMenuItem notAlartListDeleteRowMenu;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator26;
		private System.Windows.Forms.ToolStripMenuItem notAlartListCopyUserUrlMenu;
		private System.Windows.Forms.ToolStripMenuItem notAlartListCopyCommunityUrlMenu;
		private System.Windows.Forms.ToolStripMenuItem notAlartListCopyUrlMenu;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator22;
		private System.Windows.Forms.ToolStripMenuItem notAlartListOpenUserUrlMenu;
		private System.Windows.Forms.ToolStripMenuItem notAlartListOpenCommunityUrlMenu;
		private System.Windows.Forms.ToolStripMenuItem notAlartListOpenUrlMenu;
		private System.Windows.Forms.ContextMenuStrip notAlartListMenu;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn24;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn23;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn22;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn21;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn20;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn19;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn18;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
		public System.Windows.Forms.DataGridView notAlartList;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.DataGridViewTextBoxColumn 放送タイトル;
		private System.Windows.Forms.DataGridViewTextBoxColumn 放送開始日時;
		private System.Windows.Forms.DataGridViewTextBoxColumn お気に入り;
		private System.Windows.Forms.ToolStripMenuItem historyListDeleteRowMenu;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator25;
		private System.Windows.Forms.ToolStripMenuItem historyListCopyUserUrlMenu;
		private System.Windows.Forms.ToolStripMenuItem historyListCopyCommunityUrlMenu;
		private System.Windows.Forms.ToolStripMenuItem historyListCopyUrlMenu;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator24;
		private System.Windows.Forms.ToolStripMenuItem historyListOpenUserUrlMenu;
		private System.Windows.Forms.ToolStripMenuItem historyListOpenCommunityUrlMenu;
		private System.Windows.Forms.ToolStripMenuItem historyListOpenUrlMenu;
		private System.Windows.Forms.ContextMenuStrip historyListMenu;
		private System.Windows.Forms.DataGridViewTextBoxColumn 説明;
		private System.Windows.Forms.DataGridViewTextBoxColumn ユーザーID;
		private System.Windows.Forms.DataGridViewTextBoxColumn コミュニティID;
		private System.Windows.Forms.DataGridViewTextBoxColumn 放送ID;
		private System.Windows.Forms.DataGridViewTextBoxColumn コミュニティ名;
		private System.Windows.Forms.DataGridViewTextBoxColumn 放送者;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
		public System.Windows.Forms.DataGridView logList;
		private System.Windows.Forms.ToolStripMenuItem logListDeleteRowMenu;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator23;
		private System.Windows.Forms.ToolStripMenuItem logListCopyMessageMenu;
		private System.Windows.Forms.ContextMenuStrip logListMenu;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
		public System.Windows.Forms.DataGridView historyList;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.ToolStripMenuItem disableFollowMenu;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator21;
		private System.Windows.Forms.ToolStripMenuItem colorIsAndMenu;
		private System.Windows.Forms.ToolStripMenuItem isDisplayIsAndTabMenu;
		private System.Windows.Forms.DataGridViewComboBoxColumn 合致条件;
		private System.Windows.Forms.ToolStripMenuItem colorSountTypeMenu;
		private System.Windows.Forms.ToolStripMenuItem isDisplaySoundTypeTabMenu;
		private System.Windows.Forms.DataGridViewComboBoxColumn 音設定;
		private System.Windows.Forms.ToolStripMenuItem colorUserFollowMenu;
		private System.Windows.Forms.ToolStripMenuItem colorComFollowMenu;
		private System.Windows.Forms.ToolStripMenuItem colorMemoMenu;
		private System.Windows.Forms.ToolStripMenuItem colorAppliJMenu;
		private System.Windows.Forms.ToolStripMenuItem colorAppliIMenu;
		private System.Windows.Forms.ToolStripMenuItem colorAppliHMenu;
		private System.Windows.Forms.ToolStripMenuItem colorAppliGMenu;
		private System.Windows.Forms.ToolStripMenuItem colorAppliFMenu;
		private System.Windows.Forms.ToolStripMenuItem colorAppliEMenu;
		private System.Windows.Forms.ToolStripMenuItem colorAppliDMenu;
		private System.Windows.Forms.ToolStripMenuItem colorAppliCMenu;
		private System.Windows.Forms.ToolStripMenuItem colorAppliBMenu;
		private System.Windows.Forms.ToolStripMenuItem colorAppliAMenu;
		private System.Windows.Forms.ToolStripMenuItem colorSoundMenu;
		private System.Windows.Forms.ToolStripMenuItem colorMailMenu;
		private System.Windows.Forms.ToolStripMenuItem colorWebMenu;
		private System.Windows.Forms.ToolStripMenuItem colorBaloonMenu;
		private System.Windows.Forms.ToolStripMenuItem colorPopupMenu;
		private System.Windows.Forms.ToolStripMenuItem colorAddDtMenu;
		private System.Windows.Forms.ToolStripMenuItem colorRecentLiveDtMenu;
		private System.Windows.Forms.ToolStripMenuItem colorKeywordMenu;
		private System.Windows.Forms.ToolStripMenuItem colorUserNameMenu;
		private System.Windows.Forms.ToolStripMenuItem colorCommunityNameMenu;
		private System.Windows.Forms.ToolStripMenuItem colorUserIdMenu;
		private System.Windows.Forms.ToolStripMenuItem colorCommunityIdMenu;
		private System.Windows.Forms.ToolStripMenuItem colorColumnMenu;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator20;
		private System.Windows.Forms.ToolStripMenuItem bulkAddFromFollowComMenu;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator19;
		private System.Windows.Forms.ToolStripMenuItem getUserInfoFromComMenu;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator18;
		private System.Windows.Forms.ToolStripMenuItem checkExistsUserMenu;
		private System.Windows.Forms.ToolStripMenuItem checkExistsComMenu;
		private System.Windows.Forms.ToolStripMenuItem editLineMenu;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.ToolStripStatusLabel liveListUpdateStatusBar;
		private System.Windows.Forms.ToolStripStatusLabel existCheckStatusBar;
		private System.Windows.Forms.ToolStripMenuItem isDisplayMemoOnAirTabMenu;
		private System.Windows.Forms.ToolStripMenuItem isDisplayFavoriteOnAirTabMenu;
		private System.Windows.Forms.ToolStripMenuItem isDisplayTypeOnAirTabMenu;
		private System.Windows.Forms.ToolStripMenuItem isDisplayMemberOnlyOnAirTabMenu;
		private System.Windows.Forms.ToolStripMenuItem isDisplayCasOnAirTabMenu;
		private System.Windows.Forms.ToolStripMenuItem isDisplayCruiseOnAirTabMenu;
		private System.Windows.Forms.ToolStripMenuItem isDisplayRushOnAirTabMenu;
		private System.Windows.Forms.ToolStripMenuItem isDisplayFaceOnAirTabMenu;
		private System.Windows.Forms.ToolStripMenuItem isDisplayCategoryOnAirTabMenu;
		private System.Windows.Forms.ToolStripMenuItem isDisplayElapsedTimeOnAirTabMenu;
		private System.Windows.Forms.ToolStripMenuItem isDisplayCommunityIDOnAirTabMenu;
		private System.Windows.Forms.ToolStripMenuItem isDisplayLvidOnAirTabMenu;
		private System.Windows.Forms.ToolStripMenuItem isDisplayDescriptionOnAirTabMenu;
		private System.Windows.Forms.ToolStripMenuItem isDisplayCommunityNameOnAirTabMenu;
		private System.Windows.Forms.ToolStripMenuItem isDisplayHostNameOnAirTabMenu;
		private System.Windows.Forms.ToolStripMenuItem isDisplayTitleOnAirTabMenu;
		private System.Windows.Forms.ToolStripMenuItem isDisplayThumbnailOnAirTabMenu;
		private System.Windows.Forms.ToolStripMenuItem displayOnAirTabMenu;
		private System.Windows.Forms.PictureBox comThumbBox;
		private System.Windows.Forms.PictureBox userThumbBox;
		private System.Windows.Forms.ToolStripMenuItem liveListNGthumbnailMenu;
		private System.Windows.Forms.ToolStripMenuItem updateNoDelMenu;
		private System.Windows.Forms.ToolStripMenuItem update6hourDelMenu;
		private System.Windows.Forms.ToolStripMenuItem update1hourDelMenu;
		private System.Windows.Forms.ToolStripMenuItem update30minDelMenu;
		private System.Windows.Forms.ToolStripMenuItem update20minDelMenu;
		private System.Windows.Forms.ToolStripMenuItem update15minDelMenu;
		private System.Windows.Forms.ToolStripMenuItem update10minDelMenu;
		private System.Windows.Forms.ToolStripMenuItem update5minDelMenu;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
		private System.Windows.Forms.ToolStripMenuItem updateHideQuestionCategoryMenu;
		private System.Windows.Forms.ToolStripMenuItem updateHideMemberOnlyWithFavoriteMenu;
		private System.Windows.Forms.ToolStripMenuItem updateHideMemberOnlyWithoutFavoriteMenu;
		private System.Windows.Forms.ToolStripMenuItem updateAutoSortMenu;
		private System.Windows.Forms.ToolStripMenuItem updateTopFavoriteMenu;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator17;
		private System.Windows.Forms.ToolStripMenuItem updateAutoDeleteMenu;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator16;
		private System.Windows.Forms.ToolStripMenuItem updateAutoUpdateFirstMenu;
		private System.Windows.Forms.ToolStripMenuItem updateAutoUpdateStopMenu;
		private System.Windows.Forms.ToolStripMenuItem updateAutoUpdateStartMenu;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;
		private System.Windows.Forms.ToolStripMenuItem updateCateR18Menu;
		private System.Windows.Forms.ToolStripMenuItem updateCateRushMenu;
		private System.Windows.Forms.ToolStripMenuItem updateCateFaceMenu;
		private System.Windows.Forms.ToolStripMenuItem updateCatePresenMenu;
		private System.Windows.Forms.ToolStripMenuItem updateCatePlayMenu;
		private System.Windows.Forms.ToolStripMenuItem updateCateTryMenu;
		private System.Windows.Forms.ToolStripMenuItem updateCateCommon;
		private System.Windows.Forms.ToolStripMenuItem updateCateAllMenu;
		private System.Windows.Forms.ToolStripMenuItem liveListDescriptionCopyMenu;
		private System.Windows.Forms.ToolStripMenuItem liveListCommunityNameCopyMenu;
		private System.Windows.Forms.ToolStripMenuItem liveListHostNameCopyMenu;
		private System.Windows.Forms.ToolStripMenuItem liveListTitleCopyMenu;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
		private System.Windows.Forms.ToolStripMenuItem liveListCommunityUrlCopyMenu;
		private System.Windows.Forms.ToolStripMenuItem liveListUrlCopyMenu;
		private System.Windows.Forms.ToolStripMenuItem liveListDeleteRowMenu;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
		private System.Windows.Forms.ToolStripMenuItem liveListRemoveFavoriteCommunityMenu;
		private System.Windows.Forms.ToolStripMenuItem liveListAddFavoriteCommunityMenu;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
		private System.Windows.Forms.ToolStripMenuItem liveListUpdateSamuneMenu;
		private System.Windows.Forms.ToolStripMenuItem liveListWriteSamuneMemoMenu;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
		private System.Windows.Forms.ToolStripMenuItem liveListCopyMenu;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
		private System.Windows.Forms.ToolStripMenuItem liveListOpenCommunityUrlMenu;
		private System.Windows.Forms.ToolStripMenuItem liveListOpenUrlMenu;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip3;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewCheckBoxColumn16;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewCheckBoxColumn15;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewCheckBoxColumn14;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewCheckBoxColumn13;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewCheckBoxColumn12;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewCheckBoxColumn11;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewButtonColumn2;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewButtonColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
		private System.Windows.Forms.DataGridViewImageColumn dataGridViewTextBoxColumn5;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
		public System.Windows.Forms.DataGridView liveList;
		private System.Windows.Forms.Button liveListSearchBtn;
		private System.Windows.Forms.TextBox liveListSearchText;
		private System.Windows.Forms.Button categoryRightBtn;
		private System.Windows.Forms.Button categoryLeftBtn;
		private System.Windows.Forms.RadioButton reqCategoryBtn;
		private System.Windows.Forms.RadioButton liveCategoryBtn;
		private System.Windows.Forms.RadioButton commonCategoryBtn;
		public System.Windows.Forms.FlowLayoutPanel categoryBtnPanel;
		private System.Windows.Forms.RadioButton tryCategoryBtn;
		private System.Windows.Forms.RadioButton allCategoryBtn;
		private System.Windows.Forms.ToolStripMenuItem updateCateCategoryMenu;
		private System.Windows.Forms.ToolStripMenuItem updateLiveListMenu;
		private System.Windows.Forms.ToolStripMenuItem updateMenuItem;
		private System.Windows.Forms.TabPage tabPage3;
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
		private System.Windows.Forms.TabControl TabPages;
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
		private System.Windows.Forms.Label groupLabel;

	}
}
