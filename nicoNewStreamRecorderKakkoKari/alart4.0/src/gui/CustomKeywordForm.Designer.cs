/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2019/04/07
 * Time: 8:16
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace namaichi
{
	partial class CustomKeywordForm
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
			this.音設定 = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.キーワード = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.カスタム = new System.Windows.Forms.DataGridViewButtonColumn();
			this.nameLabel = new System.Windows.Forms.Label();
			this.nameText = new System.Windows.Forms.TextBox();
			this.cancelBtn = new System.Windows.Forms.Button();
			this.okBtn = new System.Windows.Forms.Button();
			this.infoLabel = new System.Windows.Forms.Label();
			this.deleteBtn = new System.Windows.Forms.Button();
			this.clearBtn = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
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
									this.音設定,
									this.キーワード,
									this.カスタム});
			this.customList.Location = new System.Drawing.Point(11, 54);
			this.customList.MultiSelect = false;
			this.customList.Name = "customList";
			this.customList.RowHeadersVisible = false;
			this.customList.RowTemplate.Height = 21;
			this.customList.Size = new System.Drawing.Size(445, 160);
			this.customList.TabIndex = 24;
			this.customList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CustomListCellClick);
			this.customList.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.CustomListCellEnter);
			this.customList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.CustomListCellFormatting);
			this.customList.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.CustomListCellParsing);
			this.customList.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.CustomListCellValueChanged);
			this.customList.CurrentCellDirtyStateChanged += new System.EventHandler(this.CustomListCurrentCellDirtyStateChanged);
			// 
			// 条件
			// 
			this.条件.DataPropertyName = "matchType";
			this.条件.HeaderText = "条件";
			this.条件.Items.AddRange(new object[] {
									"必ず含む",
									"いずれかを含む",
									"含まない"});
			this.条件.Name = "条件";
			// 
			// 音設定
			// 
			this.音設定.DataPropertyName = "type";
			this.音設定.HeaderText = "タイプ";
			this.音設定.Items.AddRange(new object[] {
									"ワード",
									"条件の入れ子"});
			this.音設定.Name = "音設定";
			this.音設定.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.音設定.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.音設定.Width = 96;
			// 
			// キーワード
			// 
			this.キーワード.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.キーワード.DataPropertyName = "str";
			this.キーワード.HeaderText = "ワード";
			this.キーワード.Name = "キーワード";
			// 
			// カスタム
			// 
			this.カスタム.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
			this.カスタム.DataPropertyName = "customBtn";
			this.カスタム.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.カスタム.HeaderText = "入れ子設定";
			this.カスタム.Name = "カスタム";
			this.カスタム.Width = 70;
			// 
			// nameLabel
			// 
			this.nameLabel.Location = new System.Drawing.Point(11, 23);
			this.nameLabel.Name = "nameLabel";
			this.nameLabel.Size = new System.Drawing.Size(39, 16);
			this.nameLabel.TabIndex = 25;
			this.nameLabel.Text = "名称：";
			// 
			// nameText
			// 
			this.nameText.Location = new System.Drawing.Point(47, 20);
			this.nameText.Name = "nameText";
			this.nameText.Size = new System.Drawing.Size(134, 19);
			this.nameText.TabIndex = 26;
			// 
			// cancelBtn
			// 
			this.cancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelBtn.Location = new System.Drawing.Point(382, 297);
			this.cancelBtn.Name = "cancelBtn";
			this.cancelBtn.Size = new System.Drawing.Size(74, 23);
			this.cancelBtn.TabIndex = 28;
			this.cancelBtn.Text = "キャンセル";
			this.cancelBtn.UseVisualStyleBackColor = true;
			this.cancelBtn.Click += new System.EventHandler(this.Button4Click);
			// 
			// okBtn
			// 
			this.okBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okBtn.Location = new System.Drawing.Point(302, 297);
			this.okBtn.Name = "okBtn";
			this.okBtn.Size = new System.Drawing.Size(74, 23);
			this.okBtn.TabIndex = 27;
			this.okBtn.Text = "OK";
			this.okBtn.UseVisualStyleBackColor = true;
			this.okBtn.Click += new System.EventHandler(this.OKBtnClick);
			// 
			// infoLabel
			// 
			this.infoLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.infoLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.infoLabel.Location = new System.Drawing.Point(11, 248);
			this.infoLabel.Name = "infoLabel";
			this.infoLabel.Size = new System.Drawing.Size(445, 46);
			this.infoLabel.TabIndex = 29;
			// 
			// deleteBtn
			// 
			this.deleteBtn.Location = new System.Drawing.Point(12, 220);
			this.deleteBtn.Name = "deleteBtn";
			this.deleteBtn.Size = new System.Drawing.Size(75, 23);
			this.deleteBtn.TabIndex = 30;
			this.deleteBtn.Text = "行削除";
			this.deleteBtn.UseVisualStyleBackColor = true;
			this.deleteBtn.Click += new System.EventHandler(this.DeleteBtnClick);
			// 
			// clearBtn
			// 
			this.clearBtn.Location = new System.Drawing.Point(93, 220);
			this.clearBtn.Name = "clearBtn";
			this.clearBtn.Size = new System.Drawing.Size(75, 23);
			this.clearBtn.TabIndex = 30;
			this.clearBtn.Text = "クリア";
			this.clearBtn.UseVisualStyleBackColor = true;
			this.clearBtn.Click += new System.EventHandler(this.ClearBtnClick);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(187, 23);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(249, 16);
			this.label1.TabIndex = 25;
			this.label1.Text = "(任意設定。お気に入りタブで使用する表示名)";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(174, 225);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(249, 16);
			this.label2.TabIndex = 25;
			this.label2.Text = "ワードは正規表現で判定されます。";
			// 
			// CustomKeywordForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(469, 332);
			this.Controls.Add(this.clearBtn);
			this.Controls.Add(this.deleteBtn);
			this.Controls.Add(this.infoLabel);
			this.Controls.Add(this.cancelBtn);
			this.Controls.Add(this.okBtn);
			this.Controls.Add(this.nameText);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.nameLabel);
			this.Controls.Add(this.customList);
			this.Name = "CustomKeywordForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "高度なキーワード設定";
			((System.ComponentModel.ISupportInitialize)(this.customList)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button clearBtn;
		private System.Windows.Forms.Button deleteBtn;
		private System.Windows.Forms.Label infoLabel;
		private System.Windows.Forms.Button okBtn;
		private System.Windows.Forms.Button cancelBtn;
		private System.Windows.Forms.TextBox nameText;
		private System.Windows.Forms.Label nameLabel;
		private System.Windows.Forms.DataGridViewButtonColumn カスタム;
		private System.Windows.Forms.DataGridViewComboBoxColumn 音設定;
		private System.Windows.Forms.DataGridViewTextBoxColumn キーワード;
		private System.Windows.Forms.DataGridViewComboBoxColumn 条件;
		public System.Windows.Forms.DataGridView customList;
		
		void CustomListCellParsing(object sender, System.Windows.Forms.DataGridViewCellParsingEventArgs e)
		{
			util.debugWriteLine("parsing");
		}
	}
}
