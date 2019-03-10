/*
 * Created by SharpDevelop.
 * User: user
 * Date: 2019/01/04
 * Time: 7:23
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace namaichi
{
	partial class addTaskForm
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
			this.button4 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.argsText = new System.Windows.Forms.TextBox();
			this.lvidText = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.memoText = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.yearList = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.monthList = new System.Windows.Forms.NumericUpDown();
			this.dayList = new System.Windows.Forms.NumericUpDown();
			this.label11 = new System.Windows.Forms.Label();
			this.isDelete = new System.Windows.Forms.CheckBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.minuteList = new System.Windows.Forms.NumericUpDown();
			this.hourList = new System.Windows.Forms.NumericUpDown();
			this.secondList = new System.Windows.Forms.NumericUpDown();
			this.label8 = new System.Windows.Forms.Label();
			this.getInfoFromHosoIdBtn = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.isMailChkBox = new System.Windows.Forms.CheckBox();
			this.appliJChkBox = new System.Windows.Forms.CheckBox();
			this.appliIChkBox = new System.Windows.Forms.CheckBox();
			this.appliHChkBox = new System.Windows.Forms.CheckBox();
			this.appliGChkBox = new System.Windows.Forms.CheckBox();
			this.appliFChkBox = new System.Windows.Forms.CheckBox();
			this.appliEChkBox = new System.Windows.Forms.CheckBox();
			this.isWebChkBox = new System.Windows.Forms.CheckBox();
			this.isSoundChkBox = new System.Windows.Forms.CheckBox();
			this.isBaloonChkBox = new System.Windows.Forms.CheckBox();
			this.isPopupChkBox = new System.Windows.Forms.CheckBox();
			this.appliDChkBox = new System.Windows.Forms.CheckBox();
			this.appliCChkBox = new System.Windows.Forms.CheckBox();
			this.appliBChkBox = new System.Windows.Forms.CheckBox();
			this.appliAChkBox = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.yearList)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.monthList)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dayList)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.minuteList)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.hourList)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.secondList)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(265, 330);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(74, 23);
			this.button4.TabIndex = 3;
			this.button4.Text = "キャンセル";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.Button4Click);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(185, 330);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(74, 23);
			this.button3.TabIndex = 2;
			this.button3.Text = "OK";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.Button3Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(28, 36);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(68, 15);
			this.label1.TabIndex = 4;
			this.label1.Text = "開始時刻";
			// 
			// argsText
			// 
			this.argsText.Location = new System.Drawing.Point(102, 103);
			this.argsText.Name = "argsText";
			this.argsText.Size = new System.Drawing.Size(237, 19);
			this.argsText.TabIndex = 11;
			// 
			// lvidText
			// 
			this.lvidText.Location = new System.Drawing.Point(102, 79);
			this.lvidText.Name = "lvidText";
			this.lvidText.Size = new System.Drawing.Size(156, 19);
			this.lvidText.TabIndex = 10;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(28, 106);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(68, 15);
			this.label3.TabIndex = 9;
			this.label3.Text = "引数";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(28, 82);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(68, 15);
			this.label4.TabIndex = 8;
			this.label4.Text = "放送ID";
			// 
			// memoText
			// 
			this.memoText.Location = new System.Drawing.Point(53, 262);
			this.memoText.Name = "memoText";
			this.memoText.Size = new System.Drawing.Size(286, 19);
			this.memoText.TabIndex = 14;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(28, 265);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(25, 21);
			this.label5.TabIndex = 15;
			this.label5.Text = "メモ";
			// 
			// yearList
			// 
			this.yearList.Location = new System.Drawing.Point(102, 34);
			this.yearList.Maximum = new decimal(new int[] {
									99999,
									0,
									0,
									0});
			this.yearList.Name = "yearList";
			this.yearList.Size = new System.Drawing.Size(60, 19);
			this.yearList.TabIndex = 25;
			this.yearList.Value = new decimal(new int[] {
									5000,
									0,
									0,
									0});
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(168, 36);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(15, 21);
			this.label2.TabIndex = 26;
			this.label2.Text = "年";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(238, 36);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(15, 21);
			this.label10.TabIndex = 28;
			this.label10.Text = "月";
			// 
			// monthList
			// 
			this.monthList.Location = new System.Drawing.Point(188, 34);
			this.monthList.Maximum = new decimal(new int[] {
									12,
									0,
									0,
									0});
			this.monthList.Minimum = new decimal(new int[] {
									1,
									0,
									0,
									0});
			this.monthList.Name = "monthList";
			this.monthList.Size = new System.Drawing.Size(44, 19);
			this.monthList.TabIndex = 27;
			this.monthList.Value = new decimal(new int[] {
									12,
									0,
									0,
									0});
			// 
			// dayList
			// 
			this.dayList.Location = new System.Drawing.Point(259, 34);
			this.dayList.Maximum = new decimal(new int[] {
									31,
									0,
									0,
									0});
			this.dayList.Minimum = new decimal(new int[] {
									1,
									0,
									0,
									0});
			this.dayList.Name = "dayList";
			this.dayList.Size = new System.Drawing.Size(44, 19);
			this.dayList.TabIndex = 27;
			this.dayList.Value = new decimal(new int[] {
									12,
									0,
									0,
									0});
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(309, 36);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(15, 21);
			this.label11.TabIndex = 28;
			this.label11.Text = "日";
			// 
			// isDelete
			// 
			this.isDelete.Location = new System.Drawing.Point(28, 300);
			this.isDelete.Name = "isDelete";
			this.isDelete.Size = new System.Drawing.Size(216, 24);
			this.isDelete.TabIndex = 8;
			this.isDelete.Text = "実行後に自動で削除する";
			this.isDelete.UseVisualStyleBackColor = true;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(223, 58);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(15, 21);
			this.label6.TabIndex = 31;
			this.label6.Text = "分";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(152, 58);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(15, 21);
			this.label7.TabIndex = 32;
			this.label7.Text = "時";
			// 
			// minuteList
			// 
			this.minuteList.Location = new System.Drawing.Point(173, 56);
			this.minuteList.Maximum = new decimal(new int[] {
									59,
									0,
									0,
									0});
			this.minuteList.Name = "minuteList";
			this.minuteList.Size = new System.Drawing.Size(44, 19);
			this.minuteList.TabIndex = 29;
			this.minuteList.Value = new decimal(new int[] {
									50,
									0,
									0,
									0});
			// 
			// hourList
			// 
			this.hourList.Location = new System.Drawing.Point(102, 56);
			this.hourList.Maximum = new decimal(new int[] {
									23,
									0,
									0,
									0});
			this.hourList.Name = "hourList";
			this.hourList.Size = new System.Drawing.Size(44, 19);
			this.hourList.TabIndex = 30;
			this.hourList.Value = new decimal(new int[] {
									23,
									0,
									0,
									0});
			// 
			// secondList
			// 
			this.secondList.Location = new System.Drawing.Point(244, 56);
			this.secondList.Maximum = new decimal(new int[] {
									59,
									0,
									0,
									0});
			this.secondList.Name = "secondList";
			this.secondList.Size = new System.Drawing.Size(44, 19);
			this.secondList.TabIndex = 29;
			this.secondList.Value = new decimal(new int[] {
									50,
									0,
									0,
									0});
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(294, 58);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(15, 21);
			this.label8.TabIndex = 31;
			this.label8.Text = "秒";
			// 
			// getInfoFromHosoIdBtn
			// 
			this.getInfoFromHosoIdBtn.Location = new System.Drawing.Point(264, 77);
			this.getInfoFromHosoIdBtn.Name = "getInfoFromHosoIdBtn";
			this.getInfoFromHosoIdBtn.Size = new System.Drawing.Size(75, 23);
			this.getInfoFromHosoIdBtn.TabIndex = 33;
			this.getInfoFromHosoIdBtn.Text = "情報取得";
			this.getInfoFromHosoIdBtn.UseVisualStyleBackColor = true;
			this.getInfoFromHosoIdBtn.Click += new System.EventHandler(this.GetInfoFromHosoIdBtnClick);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.isMailChkBox);
			this.groupBox1.Controls.Add(this.appliJChkBox);
			this.groupBox1.Controls.Add(this.appliIChkBox);
			this.groupBox1.Controls.Add(this.appliHChkBox);
			this.groupBox1.Controls.Add(this.appliGChkBox);
			this.groupBox1.Controls.Add(this.appliFChkBox);
			this.groupBox1.Controls.Add(this.appliEChkBox);
			this.groupBox1.Controls.Add(this.isWebChkBox);
			this.groupBox1.Controls.Add(this.isSoundChkBox);
			this.groupBox1.Controls.Add(this.isBaloonChkBox);
			this.groupBox1.Controls.Add(this.isPopupChkBox);
			this.groupBox1.Controls.Add(this.appliDChkBox);
			this.groupBox1.Controls.Add(this.appliCChkBox);
			this.groupBox1.Controls.Add(this.appliBChkBox);
			this.groupBox1.Controls.Add(this.appliAChkBox);
			this.groupBox1.Location = new System.Drawing.Point(28, 128);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(329, 118);
			this.groupBox1.TabIndex = 34;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "動作";
			// 
			// isMailChkBox
			// 
			this.isMailChkBox.Location = new System.Drawing.Point(255, 18);
			this.isMailChkBox.Name = "isMailChkBox";
			this.isMailChkBox.Size = new System.Drawing.Size(68, 24);
			this.isMailChkBox.TabIndex = 13;
			this.isMailChkBox.Text = "メール";
			this.isMailChkBox.UseVisualStyleBackColor = true;
			// 
			// appliJChkBox
			// 
			this.appliJChkBox.Location = new System.Drawing.Point(172, 84);
			this.appliJChkBox.Name = "appliJChkBox";
			this.appliJChkBox.Size = new System.Drawing.Size(62, 24);
			this.appliJChkBox.TabIndex = 12;
			this.appliJChkBox.Text = "アプリJ";
			this.appliJChkBox.UseVisualStyleBackColor = true;
			// 
			// appliIChkBox
			// 
			this.appliIChkBox.Location = new System.Drawing.Point(89, 84);
			this.appliIChkBox.Name = "appliIChkBox";
			this.appliIChkBox.Size = new System.Drawing.Size(62, 24);
			this.appliIChkBox.TabIndex = 11;
			this.appliIChkBox.Text = "アプリI";
			this.appliIChkBox.UseVisualStyleBackColor = true;
			// 
			// appliHChkBox
			// 
			this.appliHChkBox.Location = new System.Drawing.Point(6, 84);
			this.appliHChkBox.Name = "appliHChkBox";
			this.appliHChkBox.Size = new System.Drawing.Size(62, 24);
			this.appliHChkBox.TabIndex = 10;
			this.appliHChkBox.Text = "アプリH";
			this.appliHChkBox.UseVisualStyleBackColor = true;
			// 
			// appliGChkBox
			// 
			this.appliGChkBox.Location = new System.Drawing.Point(255, 62);
			this.appliGChkBox.Name = "appliGChkBox";
			this.appliGChkBox.Size = new System.Drawing.Size(62, 24);
			this.appliGChkBox.TabIndex = 9;
			this.appliGChkBox.Text = "アプリG";
			this.appliGChkBox.UseVisualStyleBackColor = true;
			// 
			// appliFChkBox
			// 
			this.appliFChkBox.Location = new System.Drawing.Point(172, 62);
			this.appliFChkBox.Name = "appliFChkBox";
			this.appliFChkBox.Size = new System.Drawing.Size(62, 24);
			this.appliFChkBox.TabIndex = 8;
			this.appliFChkBox.Text = "アプリF";
			this.appliFChkBox.UseVisualStyleBackColor = true;
			// 
			// appliEChkBox
			// 
			this.appliEChkBox.Location = new System.Drawing.Point(89, 62);
			this.appliEChkBox.Name = "appliEChkBox";
			this.appliEChkBox.Size = new System.Drawing.Size(62, 24);
			this.appliEChkBox.TabIndex = 7;
			this.appliEChkBox.Text = "アプリE";
			this.appliEChkBox.UseVisualStyleBackColor = true;
			// 
			// isWebChkBox
			// 
			this.isWebChkBox.Location = new System.Drawing.Point(172, 18);
			this.isWebChkBox.Name = "isWebChkBox";
			this.isWebChkBox.Size = new System.Drawing.Size(68, 24);
			this.isWebChkBox.TabIndex = 6;
			this.isWebChkBox.Text = "Web";
			this.isWebChkBox.UseVisualStyleBackColor = true;
			// 
			// isSoundChkBox
			// 
			this.isSoundChkBox.Location = new System.Drawing.Point(6, 40);
			this.isSoundChkBox.Name = "isSoundChkBox";
			this.isSoundChkBox.Size = new System.Drawing.Size(82, 24);
			this.isSoundChkBox.TabIndex = 5;
			this.isSoundChkBox.Text = "音";
			this.isSoundChkBox.UseVisualStyleBackColor = true;
			// 
			// isBaloonChkBox
			// 
			this.isBaloonChkBox.Location = new System.Drawing.Point(89, 18);
			this.isBaloonChkBox.Name = "isBaloonChkBox";
			this.isBaloonChkBox.Size = new System.Drawing.Size(82, 24);
			this.isBaloonChkBox.TabIndex = 4;
			this.isBaloonChkBox.Text = "バルーン";
			this.isBaloonChkBox.UseVisualStyleBackColor = true;
			// 
			// isPopupChkBox
			// 
			this.isPopupChkBox.Location = new System.Drawing.Point(6, 18);
			this.isPopupChkBox.Name = "isPopupChkBox";
			this.isPopupChkBox.Size = new System.Drawing.Size(82, 24);
			this.isPopupChkBox.TabIndex = 3;
			this.isPopupChkBox.Text = "ポップアップ";
			this.isPopupChkBox.UseVisualStyleBackColor = true;
			// 
			// appliDChkBox
			// 
			this.appliDChkBox.Location = new System.Drawing.Point(6, 62);
			this.appliDChkBox.Name = "appliDChkBox";
			this.appliDChkBox.Size = new System.Drawing.Size(62, 24);
			this.appliDChkBox.TabIndex = 2;
			this.appliDChkBox.Text = "アプリD";
			this.appliDChkBox.UseVisualStyleBackColor = true;
			// 
			// appliCChkBox
			// 
			this.appliCChkBox.Location = new System.Drawing.Point(255, 40);
			this.appliCChkBox.Name = "appliCChkBox";
			this.appliCChkBox.Size = new System.Drawing.Size(62, 24);
			this.appliCChkBox.TabIndex = 2;
			this.appliCChkBox.Text = "アプリC";
			this.appliCChkBox.UseVisualStyleBackColor = true;
			// 
			// appliBChkBox
			// 
			this.appliBChkBox.Location = new System.Drawing.Point(172, 40);
			this.appliBChkBox.Name = "appliBChkBox";
			this.appliBChkBox.Size = new System.Drawing.Size(62, 24);
			this.appliBChkBox.TabIndex = 1;
			this.appliBChkBox.Text = "アプリB";
			this.appliBChkBox.UseVisualStyleBackColor = true;
			// 
			// appliAChkBox
			// 
			this.appliAChkBox.Location = new System.Drawing.Point(89, 40);
			this.appliAChkBox.Name = "appliAChkBox";
			this.appliAChkBox.Size = new System.Drawing.Size(62, 24);
			this.appliAChkBox.TabIndex = 0;
			this.appliAChkBox.Text = "アプリA";
			this.appliAChkBox.UseVisualStyleBackColor = true;
			// 
			// addTaskForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(385, 370);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.getInfoFromHosoIdBtn);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.secondList);
			this.Controls.Add(this.minuteList);
			this.Controls.Add(this.hourList);
			this.Controls.Add(this.isDelete);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.dayList);
			this.Controls.Add(this.monthList);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.yearList);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.memoText);
			this.Controls.Add(this.argsText);
			this.Controls.Add(this.lvidText);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button3);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Name = "addTaskForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "予約追加";
			this.Load += new System.EventHandler(this.AddTaskFormLoad);
			((System.ComponentModel.ISupportInitialize)(this.yearList)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.monthList)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dayList)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.minuteList)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.hourList)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.secondList)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.CheckBox isMailChkBox;
		private System.Windows.Forms.Button getInfoFromHosoIdBtn;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.NumericUpDown secondList;
		private System.Windows.Forms.CheckBox appliGChkBox;
		private System.Windows.Forms.CheckBox appliHChkBox;
		private System.Windows.Forms.CheckBox appliIChkBox;
		private System.Windows.Forms.CheckBox appliJChkBox;
		private System.Windows.Forms.NumericUpDown hourList;
		private System.Windows.Forms.NumericUpDown minuteList;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.CheckBox isDelete;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.NumericUpDown dayList;
		private System.Windows.Forms.NumericUpDown monthList;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.NumericUpDown yearList;
		private System.Windows.Forms.CheckBox appliDChkBox;
		private System.Windows.Forms.CheckBox appliEChkBox;
		private System.Windows.Forms.CheckBox appliFChkBox;
		private System.Windows.Forms.CheckBox isWebChkBox;
		private System.Windows.Forms.CheckBox isSoundChkBox;
		private System.Windows.Forms.CheckBox isPopupChkBox;
		private System.Windows.Forms.CheckBox isBaloonChkBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox memoText;
		private System.Windows.Forms.CheckBox appliAChkBox;
		private System.Windows.Forms.CheckBox appliBChkBox;
		private System.Windows.Forms.CheckBox appliCChkBox;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox lvidText;
		private System.Windows.Forms.TextBox argsText;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
	}
}
