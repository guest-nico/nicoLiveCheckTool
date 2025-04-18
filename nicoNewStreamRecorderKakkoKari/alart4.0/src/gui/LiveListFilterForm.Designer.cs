/*
 * Created by SharpDevelop.
 * User: kogak
 * Date: 2025/04/11
 * Time: 2:04
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace namaichi.gui
{
	partial class LiveListFilterForm
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
			this.customList = new System.Windows.Forms.DataGridView();
			this.条件 = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.Column1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.キーワード = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cancelBtn = new System.Windows.Forms.Button();
			this.okBtn = new System.Windows.Forms.Button();
			this.clearBtn = new System.Windows.Forms.Button();
			this.deleteBtn = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.customList)).BeginInit();
			this.SuspendLayout();
			// 
			// customList
			// 
			this.customList.AllowUserToResizeRows = false;
			this.customList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.customList.ColumnHeadersHeight = 25;
			this.customList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.customList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
									this.条件,
									this.Column1,
									this.キーワード});
			this.customList.Location = new System.Drawing.Point(12, 25);
			this.customList.MultiSelect = false;
			this.customList.Name = "customList";
			this.customList.RowHeadersVisible = false;
			this.customList.RowTemplate.Height = 21;
			this.customList.Size = new System.Drawing.Size(387, 183);
			this.customList.TabIndex = 25;
			// 
			// 条件
			// 
			this.条件.DataPropertyName = "infoType";
			this.条件.HeaderText = "対象";
			this.条件.Items.AddRange(new object[] {
									"タイトル",
									"チャンネル名",
									"チャンネルID",
									"放送者名",
									"放送者ID",
									"放送ID",
									"全て"});
			this.条件.Name = "条件";
			// 
			// Column1
			// 
			this.Column1.DataPropertyName = "matchType";
			this.Column1.HeaderText = "条件";
			this.Column1.Items.AddRange(new object[] {
									"必ず含む",
									"いずれかを含む",
									"含まない"});
			this.Column1.Name = "Column1";
			// 
			// キーワード
			// 
			this.キーワード.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.キーワード.DataPropertyName = "str";
			this.キーワード.HeaderText = "ワード";
			this.キーワード.Name = "キーワード";
			// 
			// cancelBtn
			// 
			this.cancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelBtn.Location = new System.Drawing.Point(324, 214);
			this.cancelBtn.Name = "cancelBtn";
			this.cancelBtn.Size = new System.Drawing.Size(74, 23);
			this.cancelBtn.TabIndex = 30;
			this.cancelBtn.Text = "キャンセル";
			this.cancelBtn.UseVisualStyleBackColor = true;
			this.cancelBtn.Click += new System.EventHandler(this.CancelBtnClick);
			// 
			// okBtn
			// 
			this.okBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okBtn.Location = new System.Drawing.Point(244, 214);
			this.okBtn.Name = "okBtn";
			this.okBtn.Size = new System.Drawing.Size(74, 23);
			this.okBtn.TabIndex = 29;
			this.okBtn.Text = "OK";
			this.okBtn.UseVisualStyleBackColor = true;
			this.okBtn.Click += new System.EventHandler(this.OkBtnClick);
			// 
			// clearBtn
			// 
			this.clearBtn.Location = new System.Drawing.Point(93, 214);
			this.clearBtn.Name = "clearBtn";
			this.clearBtn.Size = new System.Drawing.Size(75, 23);
			this.clearBtn.TabIndex = 32;
			this.clearBtn.Text = "クリア";
			this.clearBtn.UseVisualStyleBackColor = true;
			this.clearBtn.Click += new System.EventHandler(this.ClearBtnClick);
			// 
			// deleteBtn
			// 
			this.deleteBtn.Location = new System.Drawing.Point(12, 214);
			this.deleteBtn.Name = "deleteBtn";
			this.deleteBtn.Size = new System.Drawing.Size(75, 23);
			this.deleteBtn.TabIndex = 31;
			this.deleteBtn.Text = "行削除";
			this.deleteBtn.UseVisualStyleBackColor = true;
			this.deleteBtn.Click += new System.EventHandler(this.DeleteBtnClick);
			// 
			// LiveListFilterForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(411, 249);
			this.Controls.Add(this.clearBtn);
			this.Controls.Add(this.deleteBtn);
			this.Controls.Add(this.cancelBtn);
			this.Controls.Add(this.okBtn);
			this.Controls.Add(this.customList);
			this.Name = "LiveListFilterForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "フィルター条件";
			((System.ComponentModel.ISupportInitialize)(this.customList)).EndInit();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button deleteBtn;
		private System.Windows.Forms.Button clearBtn;
		private System.Windows.Forms.DataGridViewComboBoxColumn Column1;
		private System.Windows.Forms.Button okBtn;
		private System.Windows.Forms.Button cancelBtn;
		private System.Windows.Forms.DataGridViewTextBoxColumn キーワード;
		private System.Windows.Forms.DataGridViewComboBoxColumn 条件;
		public System.Windows.Forms.DataGridView customList;
	}
}
