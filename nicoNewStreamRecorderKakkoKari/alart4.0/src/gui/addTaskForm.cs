/*
 * Created by SharpDevelop.
 * User: user
 * Date: 2019/01/04
 * Time: 7:23
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using namaichi.info;
using namaichi.rec;

namespace namaichi
{
	/// <summary>
	/// Description of addForm.
	/// </summary>
	public partial class addTaskForm : Form
	{
//		public int addType = -1; //-1-no 0-user 1-community or channel 2-official
		public TaskInfo ret = null;
		private MainForm form;
		public addTaskForm(MainForm form, string id)
		{
			this.form = form;
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			
			
			var now = DateTime.Now;
			try {
				yearList.Text = now.Year.ToString();
				monthList.Text = now.Month.ToString();
				dayList.Text = now.Day.ToString();
				hourList.Text = now.Hour.ToString();
				minuteList.Text = now.Minute.ToString();
				secondList.Text = now.Second.ToString();
			} catch (Exception e) {
				util.debugWriteLine(e.Message + e.Source + e.StackTrace + e.TargetSite);
			}
			lvidText.Text = id;
			setAppliName();
			
			util.setFontSize(int.Parse(form.config.get("fontSize")), this, false);
		}
		
		void Button4Click(object sender, EventArgs e)
		{
			Close();
		}
		
		void Button3Click(object sender, EventArgs e)
		{
			var startTime = yearList.Text + "/" + monthList.Text + 
					"/" + dayList.Text + " " + hourList.Text + ":" +
					minuteList.Text + ":" + secondList.Text;
			DateTime _dt;
			if (!DateTime.TryParse(startTime, out _dt)) {
				util.showMessageBoxCenterForm(this, "有効な日時ではありませんでした");
				return;
			}
			startTime = _dt.ToString("yyyy\"/\"MM\"/\"dd HH\":\"mm\":\"ss");
			
			var lvid = util.getRegGroup(lvidText.Text, "(lv\\d+)");
			if (lvid == null) lvid = "";
			
			var now = DateTime.Now.ToString("yyyy\"/\"MM\"/\"dd HH\":\"mm\":\"ss");
			var addDate = now;//now.Substring(0, now.Length - 3);
			
			var _ret = new TaskInfo(startTime, lvid, 
					argsText.Text, addDate, "待機中",
					isPopupChkBox.Checked,
					isBaloonChkBox.Checked, isWebChkBox.Checked, 
					isMailChkBox.Checked, isSoundChkBox.Checked,
					appliAChkBox.Checked, appliBChkBox.Checked, 
					appliCChkBox.Checked, appliDChkBox.Checked, 
					appliEChkBox.Checked, appliFChkBox.Checked,
					appliGChkBox.Checked, appliHChkBox.Checked,
					appliIChkBox.Checked, appliJChkBox.Checked,
					memoText.Text, isDelete.Checked);
			
			ret = _ret;
			Close();
		}
		void GetInfoFromHosoIdBtnClick(object sender, EventArgs e)
		{
			var lvid = util.getRegGroup(lvidText.Text, "(lv\\d+)");
			if (lvid == null) return;
			lvidText.Text = lvid;
			
			var hig = new HosoInfoGetter();
			if (hig.get(lvidText.Text, form.check.container)) {
				var t = "";

				if (hig.title != null) t += hig.title;
				var isFollow = false;
				if (hig.communityId != null) {
					var comName = util.getCommunityName(hig.communityId, out isFollow, form.check.container);
					if (comName != null) t += (t == "" ? "" : " ") + comName;
				}
				if (hig.userId != null) {
					var userName = util.getUserName(hig.userId, out isFollow, form.check.container, false, form.config);
					if (userName != null) t += (t == "" ? "" : " ") + userName;
				}
				if (hig.dt != DateTime.MinValue) {
					t += (t == "" ? "" : " ") + hig.dt.ToString("yyyy\"/\"MM\"/\"dd HH\":\"mm\":\"ss");
					yearList.Text = hig.dt.Year.ToString(); monthList.Text = hig.dt.Month.ToString();
					dayList.Text = hig.dt.Day.ToString(); hourList.Text = hig.dt.Hour.ToString();
					minuteList.Text = hig.dt.Minute.ToString(); secondList.Text = hig.dt.Second.ToString();
				}
				memoText.Text = t;
			} else {
				util.showMessageBoxCenterForm(this, hig.type == "official a" ? "公式放送でした" : "しっぱい");
			}
		}
		
		void AddTaskFormLoad(object sender, EventArgs e)
		{
			if (lvidText.Text != "") {
				getInfoFromHosoIdBtn.PerformClick();
			}
		}
		void setAppliName() {
			for (var i = 0; i < 10; i++) {
				var n = "appli" + (char)('A' + i);
				var c = form.config.get(n + "Name");
				if (string.IsNullOrEmpty(c)) continue;
				Controls.Find(n + "ChkBox", true)[0].Text =	"ｱﾌﾟﾘ" + ((char)('A' + i)) + "(" + c + ")";
			}
		}
	}
}
