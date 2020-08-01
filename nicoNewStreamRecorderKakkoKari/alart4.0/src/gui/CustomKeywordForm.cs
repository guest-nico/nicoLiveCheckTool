/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2019/04/07
 * Time: 8:16
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using namaichi.info;

namespace namaichi
{
	/// <summary>
	/// Description of CustomKeywordForm.
	/// </summary>
	public partial class CustomKeywordForm : Form
	{
		public List<CustomKeywordInfo> ret = null;
		public SortableBindingList<CustomKeywordInfo> customKwListDataSource = new SortableBindingList<CustomKeywordInfo>();
		private config.config config = null;
		public CustomKeywordForm(config.config config, bool isFirst = false, List<CustomKeywordInfo> ckis = null)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			
			nameLabel.Visible = isFirst;
			nameText.Visible = isFirst;
			
			
			customList.DataSource = customKwListDataSource;
			
			if (ckis != null) {
				foreach (var c in ckis)
					customKwListDataSource.Add(c);
			}
			if (isFirst && ckis != null && ckis.Count > 0)
				nameText.Text = ckis[0].name;
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			this.config = config;
			util.setFontSize(int.Parse(config.get("fontSize")), this, false);
			
		}
		
		void Button4Click(object sender, EventArgs e)
		{
			Close();
		}
		
		void OKBtnClick(object sender, EventArgs e)
		{
			for (var j = customKwListDataSource.Count - 1; j > -1; j--) {
				if ((customKwListDataSource[j].type == "ワード" && string.IsNullOrEmpty(customKwListDataSource[j].str)) ||
				    	(customKwListDataSource[j].type == "条件の入れ子" && customKwListDataSource[j].cki == null))
					customKwListDataSource.RemoveAt(j);
			}
			if (customKwListDataSource.Count == 0) {
				//MessageBox.Show("有効なデータがありませんでした");
				Close();
			}
			
			
			if (customKwListDataSource.Count != 0) {
				ret = new List<CustomKeywordInfo>(customKwListDataSource);
				if (nameText.Visible) {
					if (nameText.Text != "") ret[0].name = nameText.Text;
					else {
						var _cki = customKwListDataSource[0];
						for (var i = 0; i < 10; i++) {
							if (_cki.str == "" || _cki.str == null) _cki = _cki.cki[0];
							else {
								ret[0].name = _cki.str;
								break;
							}
						}
						JToken.FromObject(ret).ToString(Formatting.None);
					}
				}
			}
			DialogResult = DialogResult.OK;
			Close();
		}
		
		void CustomListCellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex != 3) return;
			if (customKwListDataSource[e.RowIndex].type != "条件の入れ子") return;
			var f = new CustomKeywordForm(config, false, customKwListDataSource[e.RowIndex].cki);
			f.ShowDialog();
			if (f.ret == null) return;
			customKwListDataSource[e.RowIndex].cki = f.ret;
			infoLabel.Text = JToken.FromObject(f.ret).ToString(Formatting.None);
			//customKwListDataSource[e.RowIndex].str = 
			//		JToken.FromObject(f.ret).ToString(Formatting.None);
		}

		void CustomListCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			
			if (e.RowIndex < 0 || e.RowIndex > customKwListDataSource.Count　-1) return;
			if (e.ColumnIndex == 3) 
				e.CellStyle.BackColor = (customKwListDataSource[e.RowIndex].type == "条件の入れ子" ? Color.FromArgb(224,244,224) : Color.FromArgb(200,200,200));
			if (e.ColumnIndex == 2)
				e.CellStyle.BackColor = (customKwListDataSource[e.RowIndex].type == "条件の入れ子" ? Color.FromArgb(224,224,224) : Color.White);
			
		}
		
		void CustomListCurrentCellDirtyStateChanged(object sender, EventArgs e)
		{
			var cc = customList.CurrentCell;
			if (cc.RowIndex < 0 || cc.RowIndex > customKwListDataSource.Count - 1) return;
			if (cc.ColumnIndex == 1) {
				customList.CommitEdit(DataGridViewDataErrorContexts.Commit);
				customKwListDataSource[cc.RowIndex].type = cc.Value.ToString();
				
				
				customList.UpdateCellValue(3, cc.RowIndex);
				
				
				customList.Columns[2].ReadOnly = customKwListDataSource[cc.RowIndex].type == "条件の入れ子";
				//customList.Columns[2].DefaultCellStyle.BackColor = Color.FromArgb(225,225,225);
				customList.UpdateCellValue(2, cc.RowIndex);
				
			}
		}
		
		void CustomListCellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0 || e.RowIndex > customKwListDataSource.Count - 1) return;
			try {
				if (e.ColumnIndex == 1) {
					customKwListDataSource[e.RowIndex].type = customList[e.ColumnIndex, e.RowIndex].Value.ToString();
					customList.UpdateCellValue(3, e.RowIndex);
				}
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace);
			}
		}
		
		void CustomListCellEnter(object sender, DataGridViewCellEventArgs e)
		{
			var c = customKwListDataSource[e.RowIndex]; 
			if (c.type == "条件の入れ子") {
				if (c.cki == null) infoLabel.Text = "条件の入れ子(未設定)";
				else infoLabel.Text = "条件の入れ子(" + JToken.FromObject(c.cki).ToString(Formatting.None) + ")";
			} else infoLabel.Text = "ワード(" + c.str + ")";
		}
		
		void DeleteBtnClick(object sender, EventArgs e)
		{
			var cur = customList.CurrentCell;
			if (cur.RowIndex == -1) return;
			try {
				customKwListDataSource.RemoveAt(cur.RowIndex);
			} catch (Exception ee) {
				util.debugWriteLine(ee.Message + ee.Source + ee.StackTrace + ee.TargetSite);
			}
		}
		
		void ClearBtnClick(object sender, EventArgs e)
		{
			customKwListDataSource.Clear();
		}
	}
}
