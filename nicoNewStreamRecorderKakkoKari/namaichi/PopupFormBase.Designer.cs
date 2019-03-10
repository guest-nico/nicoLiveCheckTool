/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2019/01/17
 * Time: 4:15
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace namaichi
{
	partial class PopupFormBase
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
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.copyUrlMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.copyCommunityUrlMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.copyTitleMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.copyHostNameMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.copyCommunityNameMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.openWebBrowserMenu = new System.Windows.Forms.ToolStripMenuItem();
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
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.copyUrlMenu,
									this.copyCommunityUrlMenu,
									this.toolStripSeparator1,
									this.copyTitleMenu,
									this.copyHostNameMenu,
									this.copyCommunityNameMenu,
									this.toolStripSeparator2,
									this.openWebBrowserMenu,
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
			this.contextMenuStrip1.Size = new System.Drawing.Size(221, 390);
			// 
			// copyUrlMenu
			// 
			this.copyUrlMenu.Name = "copyUrlMenu";
			this.copyUrlMenu.Size = new System.Drawing.Size(220, 22);
			this.copyUrlMenu.Text = "放送URLをコピー";
			this.copyUrlMenu.Click += new System.EventHandler(this.CopyUrlMenuClick);
			// 
			// copyCommunityUrlMenu
			// 
			this.copyCommunityUrlMenu.Name = "copyCommunityUrlMenu";
			this.copyCommunityUrlMenu.Size = new System.Drawing.Size(220, 22);
			this.copyCommunityUrlMenu.Text = "コミュニティURLをコピー";
			this.copyCommunityUrlMenu.Click += new System.EventHandler(this.CopyCommunityUrlMenuClick);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(217, 6);
			// 
			// copyTitleMenu
			// 
			this.copyTitleMenu.Name = "copyTitleMenu";
			this.copyTitleMenu.Size = new System.Drawing.Size(220, 22);
			this.copyTitleMenu.Text = "放送タイトルをコピー";
			this.copyTitleMenu.Click += new System.EventHandler(this.CopyTitleMenuClick);
			// 
			// copyHostNameMenu
			// 
			this.copyHostNameMenu.Name = "copyHostNameMenu";
			this.copyHostNameMenu.Size = new System.Drawing.Size(220, 22);
			this.copyHostNameMenu.Text = "放送者をコピー";
			this.copyHostNameMenu.Click += new System.EventHandler(this.CopyHostNameMenuClick);
			// 
			// copyCommunityNameMenu
			// 
			this.copyCommunityNameMenu.Name = "copyCommunityNameMenu";
			this.copyCommunityNameMenu.Size = new System.Drawing.Size(220, 22);
			this.copyCommunityNameMenu.Text = "コミュニティ名をコピー";
			this.copyCommunityNameMenu.Click += new System.EventHandler(this.CopyCommunityNameMenuClick);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(217, 6);
			// 
			// openWebBrowserMenu
			// 
			this.openWebBrowserMenu.Name = "openWebBrowserMenu";
			this.openWebBrowserMenu.Size = new System.Drawing.Size(220, 22);
			this.openWebBrowserMenu.Text = "Webブラウザで開く";
			this.openWebBrowserMenu.Click += new System.EventHandler(this.OpenWebBrowserMenuClick);
			// 
			// openAppliAMenu
			// 
			this.openAppliAMenu.Name = "openAppliAMenu";
			this.openAppliAMenu.Size = new System.Drawing.Size(220, 22);
			this.openAppliAMenu.Text = "アプリAで開く";
			this.openAppliAMenu.Click += new System.EventHandler(this.OpenAppliMenuClick);
			// 
			// openAppliBMenu
			// 
			this.openAppliBMenu.Name = "openAppliBMenu";
			this.openAppliBMenu.Size = new System.Drawing.Size(220, 22);
			this.openAppliBMenu.Text = "アプリBで開く";
			this.openAppliBMenu.Click += new System.EventHandler(this.OpenAppliMenuClick);
			// 
			// openAppliCMenu
			// 
			this.openAppliCMenu.Name = "openAppliCMenu";
			this.openAppliCMenu.Size = new System.Drawing.Size(220, 22);
			this.openAppliCMenu.Text = "アプリCで開く";
			this.openAppliCMenu.Click += new System.EventHandler(this.OpenAppliMenuClick);
			// 
			// openAppliDMenu
			// 
			this.openAppliDMenu.Name = "openAppliDMenu";
			this.openAppliDMenu.Size = new System.Drawing.Size(220, 22);
			this.openAppliDMenu.Text = "アプリDで開く";
			this.openAppliDMenu.Click += new System.EventHandler(this.OpenAppliMenuClick);
			// 
			// openAppliEMenu
			// 
			this.openAppliEMenu.Name = "openAppliEMenu";
			this.openAppliEMenu.Size = new System.Drawing.Size(220, 22);
			this.openAppliEMenu.Text = "アプリEで開く";
			this.openAppliEMenu.Click += new System.EventHandler(this.OpenAppliMenuClick);
			// 
			// openAppliFMenu
			// 
			this.openAppliFMenu.Name = "openAppliFMenu";
			this.openAppliFMenu.Size = new System.Drawing.Size(220, 22);
			this.openAppliFMenu.Text = "アプリFで開く";
			this.openAppliFMenu.Click += new System.EventHandler(this.OpenAppliMenuClick);
			// 
			// openAppliGMenu
			// 
			this.openAppliGMenu.Name = "openAppliGMenu";
			this.openAppliGMenu.Size = new System.Drawing.Size(220, 22);
			this.openAppliGMenu.Text = "アプリGで開く";
			this.openAppliGMenu.Click += new System.EventHandler(this.OpenAppliMenuClick);
			// 
			// openAppliHMenu
			// 
			this.openAppliHMenu.Name = "openAppliHMenu";
			this.openAppliHMenu.Size = new System.Drawing.Size(220, 22);
			this.openAppliHMenu.Text = "アプリHで開く";
			this.openAppliHMenu.Click += new System.EventHandler(this.OpenAppliMenuClick);
			// 
			// openAppliIMenu
			// 
			this.openAppliIMenu.Name = "openAppliIMenu";
			this.openAppliIMenu.Size = new System.Drawing.Size(220, 22);
			this.openAppliIMenu.Text = "アプリIで開く";
			this.openAppliIMenu.Click += new System.EventHandler(this.OpenAppliMenuClick);
			// 
			// openAppliJMenu
			// 
			this.openAppliJMenu.Name = "openAppliJMenu";
			this.openAppliJMenu.Size = new System.Drawing.Size(220, 22);
			this.openAppliJMenu.Text = "アプリJで開く";
			this.openAppliJMenu.Click += new System.EventHandler(this.OpenAppliMenuClick);
			// 
			// PopupFormBase
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 262);
			this.ContextMenuStrip = this.contextMenuStrip1;
			this.Name = "PopupFormBase";
			this.Text = "PopupFormBase";
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);
		}
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
		private System.Windows.Forms.ToolStripMenuItem openWebBrowserMenu;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem copyCommunityNameMenu;
		private System.Windows.Forms.ToolStripMenuItem copyHostNameMenu;
		private System.Windows.Forms.ToolStripMenuItem copyTitleMenu;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem copyCommunityUrlMenu;
		private System.Windows.Forms.ToolStripMenuItem copyUrlMenu;
		protected System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
	}
}
