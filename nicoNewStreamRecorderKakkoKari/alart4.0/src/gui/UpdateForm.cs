﻿/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2020/01/11
 * Time: 21:01
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace namaichi
{
	/// <summary>
	/// Description of UpdateForm.
	/// </summary>
	public partial class UpdateForm : Form
	{
		public UpdateForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		void okBtnClick(object sender, EventArgs e)
		{
			Close();
		}
	}
}
