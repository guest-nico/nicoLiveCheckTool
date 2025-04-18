/*
 * Created by SharpDevelop.
 * User: kogak
 * Date: 2025/04/11
 * Time: 2:04
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using namaichi.info;

namespace namaichi.gui
{
	/// <summary>
	/// Description of LiveListFilterForm.
	/// </summary>
	public partial class LiveListFilterForm : Form
	{
		public List<LiveListFilterInfo> ret = new List<LiveListFilterInfo>();
		public SortableBindingList<LiveListFilterInfo> customListDataSource = new SortableBindingList<LiveListFilterInfo>();
		public LiveListFilterForm(config.config config, List<LiveListFilterInfo> llfiList = null)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			customList.DataSource = customListDataSource;
			
			
			if (llfiList != null) {
				foreach (var c in llfiList)
					customListDataSource.Add(c);
			}
			util.setFontSize(int.Parse(config.get("fontSize")), this, false);
		}
		void OkBtnClick(object sender, EventArgs e)
		{
			if (customListDataSource.Count == 0) {
				//MessageBox.Show("有効なデータがありませんでした");
				Close();
			}
			
			if (customListDataSource.Count != 0) {
				ret = new List<LiveListFilterInfo>(customListDataSource);
				
				foreach (var llfi in customListDataSource) {
					//ret.Add(llfi);
				}
				//JToken.FromObject(ret).ToString(Formatting.None);
			}
			DialogResult = DialogResult.OK;
			Close();
		}
		void CancelBtnClick(object sender, EventArgs e)
		{
			Close();
		}
		void DeleteBtnClick(object sender, EventArgs e)
		{
			var cur = customList.CurrentCell;
			if (cur.RowIndex == -1) return;
			try {
				customListDataSource.RemoveAt(cur.RowIndex);
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		void ClearBtnClick(object sender, EventArgs e)
		{
			customListDataSource.Clear();
		}
	}
}
