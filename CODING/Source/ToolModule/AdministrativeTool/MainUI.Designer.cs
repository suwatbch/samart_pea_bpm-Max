namespace AdministrativeTool
{
    partial class MainUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainUI));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonCloseAccountStatus = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonOfflineErrorLog = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonOpenOfflineFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonNewsBroadcast = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonConnectionSetting = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonOutOfBalance = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainpanel = new System.Windows.Forms.Panel();
            this.ctlDiffuseDlg = new ctlDiffuseDlgLib.ctlDiffuseDlg();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.mainpanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.Silver;
            this.toolStrip1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator6,
            this.toolStripButtonCloseAccountStatus,
            this.toolStripSeparator4,
            this.toolStripButtonOfflineErrorLog,
            this.toolStripSeparator2,
            this.toolStripButtonOpenOfflineFile,
            this.toolStripSeparator3,
            this.toolStripButtonNewsBroadcast,
            this.toolStripSeparator5,
            this.toolStripButtonConnectionSetting,
            this.toolStripSeparator1,
            this.toolStripButtonOutOfBalance});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(1260, 36);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 36);
            // 
            // toolStripButtonCloseAccountStatus
            // 
            this.toolStripButtonCloseAccountStatus.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButtonCloseAccountStatus.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonCloseAccountStatus.Image")));
            this.toolStripButtonCloseAccountStatus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCloseAccountStatus.Name = "toolStripButtonCloseAccountStatus";
            this.toolStripButtonCloseAccountStatus.Size = new System.Drawing.Size(105, 33);
            this.toolStripButtonCloseAccountStatus.Text = "  Work Status  ";
            this.toolStripButtonCloseAccountStatus.Click += new System.EventHandler(this.toolStripButtonCloseAccountStatus_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 36);
            // 
            // toolStripButtonOfflineErrorLog
            // 
            this.toolStripButtonOfflineErrorLog.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButtonOfflineErrorLog.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonOfflineErrorLog.Image")));
            this.toolStripButtonOfflineErrorLog.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOfflineErrorLog.Name = "toolStripButtonOfflineErrorLog";
            this.toolStripButtonOfflineErrorLog.Size = new System.Drawing.Size(115, 33);
            this.toolStripButtonOfflineErrorLog.Text = "Offline error log";
            this.toolStripButtonOfflineErrorLog.Click += new System.EventHandler(this.toolStripButtonOfflineErrorLog_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 36);
            // 
            // toolStripButtonOpenOfflineFile
            // 
            this.toolStripButtonOpenOfflineFile.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButtonOpenOfflineFile.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonOpenOfflineFile.Image")));
            this.toolStripButtonOpenOfflineFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOpenOfflineFile.Name = "toolStripButtonOpenOfflineFile";
            this.toolStripButtonOpenOfflineFile.Size = new System.Drawing.Size(125, 33);
            this.toolStripButtonOpenOfflineFile.Text = " Open Offline File ";
            this.toolStripButtonOpenOfflineFile.Click += new System.EventHandler(this.toolStripButtonOpenOfflineFile_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 36);
            // 
            // toolStripButtonNewsBroadcast
            // 
            this.toolStripButtonNewsBroadcast.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButtonNewsBroadcast.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonNewsBroadcast.Image")));
            this.toolStripButtonNewsBroadcast.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNewsBroadcast.Name = "toolStripButtonNewsBroadcast";
            this.toolStripButtonNewsBroadcast.Size = new System.Drawing.Size(115, 33);
            this.toolStripButtonNewsBroadcast.Text = "News Broadcast";
            this.toolStripButtonNewsBroadcast.Click += new System.EventHandler(this.toolStripButtonNewsBroadcast_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 36);
            // 
            // toolStripButtonConnectionSetting
            // 
            this.toolStripButtonConnectionSetting.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButtonConnectionSetting.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonConnectionSetting.Image")));
            this.toolStripButtonConnectionSetting.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonConnectionSetting.Name = "toolStripButtonConnectionSetting";
            this.toolStripButtonConnectionSetting.Size = new System.Drawing.Size(131, 33);
            this.toolStripButtonConnectionSetting.Text = "Connection Settting";
            this.toolStripButtonConnectionSetting.Click += new System.EventHandler(this.toolStripButtonConnectionSetting_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 36);
            // 
            // toolStripButtonOutOfBalance
            // 
            this.toolStripButtonOutOfBalance.Font = new System.Drawing.Font("Calibri", 9.75F);
            this.toolStripButtonOutOfBalance.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonOutOfBalance.Image")));
            this.toolStripButtonOutOfBalance.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOutOfBalance.Name = "toolStripButtonOutOfBalance";
            this.toolStripButtonOutOfBalance.Size = new System.Drawing.Size(91, 33);
            this.toolStripButtonOutOfBalance.Tag = "Out Of Shift";
            this.toolStripButtonOutOfBalance.Text = "Out Of Shift";
            this.toolStripButtonOutOfBalance.Click += new System.EventHandler(this.toolStripButtonOutOfBalance_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 756);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1260, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(38, 17);
            this.toolStripStatusLabel1.Text = "Ready";

            //this._statusLabel.Text = "Ready";
            //DF#112 2021-DEC-07 Uthen.P 
            //แสดงข้อความล่างซ้าย Ready --> Ready (Rev.1) เนื่องจากมีการแก้ไข BPM V204
            //this.toolStripStatusLabel1.Text = "Ready (Rev.1)";

            // 
            // mainpanel
            // 
            this.mainpanel.Controls.Add(this.ctlDiffuseDlg);
            this.mainpanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainpanel.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainpanel.Location = new System.Drawing.Point(0, 36);
            this.mainpanel.Margin = new System.Windows.Forms.Padding(4);
            this.mainpanel.Name = "mainpanel";
            this.mainpanel.Size = new System.Drawing.Size(1260, 720);
            this.mainpanel.TabIndex = 2;
            // 
            // ctlDiffuseDlg
            // 
            this.ctlDiffuseDlg.BackColor = System.Drawing.Color.Transparent;
            this.ctlDiffuseDlg.Location = new System.Drawing.Point(0, 0);
            this.ctlDiffuseDlg.Name = "ctlDiffuseDlg";
            this.ctlDiffuseDlg.Size = new System.Drawing.Size(22, 23);
            this.ctlDiffuseDlg.TabIndex = 0;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Tag = "BPM Monitor";
            this.notifyIcon1.Text = "BPM Monitor";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // MainUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1260, 778);
            this.Controls.Add(this.mainpanel);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BPM Monitor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainUI_Load);
            this.Resize += new System.EventHandler(this.MainUI_Resize);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.mainpanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonConnectionSetting;
        private System.Windows.Forms.ToolStripButton toolStripButtonCloseAccountStatus;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButtonOfflineErrorLog;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripButton toolStripButtonOpenOfflineFile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.Panel mainpanel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton toolStripButtonNewsBroadcast;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private ctlDiffuseDlgLib.ctlDiffuseDlg ctlDiffuseDlg;
        private System.Windows.Forms.ToolStripButton toolStripButtonOutOfBalance;
    }
}

