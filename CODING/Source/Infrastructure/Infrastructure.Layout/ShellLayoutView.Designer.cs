namespace PEA.BPM.Infrastructure.Layout
{
    partial class ShellLayoutView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this._statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._mainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.workIdLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._connectioninfoLab = new System.Windows.Forms.ToolStripStatusLabel();
            this._userStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._dateStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._timeStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._connectionStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._mainToolStrip = new System.Windows.Forms.ToolStrip();
            this._centerWorkspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this._mainStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _mainMenuStrip
            // 
            this._mainMenuStrip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this._mainMenuStrip.Name = "_mainMenuStrip";
            this._mainMenuStrip.Size = new System.Drawing.Size(710, 24);
            this._mainMenuStrip.TabIndex = 4;
            this._mainMenuStrip.Text = "_mainMenuStrip";
            // 
            // _statusLabel
            // 
            this._statusLabel.Name = "_statusLabel";
            this._statusLabel.Size = new System.Drawing.Size(38, 17);
            //this._statusLabel.Text = "Ready";
            //DF#112 2021-DEC-07 Uthen.P 
            //แสดงข้อความล่างซ้าย Ready --> Ready (Rev.1) เนื่องจากมีการแก้ไข BPM V204
            this._statusLabel.Text = "Ready (Rev.1)";
            this._statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            
             


            // 
            // _mainStatusStrip
            // 
            this._mainStatusStrip.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._mainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._statusLabel,
            this.workIdLabel,
            this.toolStripStatusLabel1,
            this._connectioninfoLab,
            this._userStatusLabel,
            this._dateStatusLabel,
            this._timeStatusLabel,
            this._connectionStatusLabel});
            this._mainStatusStrip.Location = new System.Drawing.Point(0, 462);
            this._mainStatusStrip.Name = "_mainStatusStrip";
            this._mainStatusStrip.Size = new System.Drawing.Size(710, 22);
            this._mainStatusStrip.TabIndex = 6;
            this._mainStatusStrip.Text = "_mainStatusStrip";
            // 
            // workIdLabel
            // 
            this.workIdLabel.Name = "workIdLabel";
            this.workIdLabel.Size = new System.Drawing.Size(0, 17);
            this.workIdLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _connectioninfoLab
            // 
            this._connectioninfoLab.Name = "_connectioninfoLab";
            this._connectioninfoLab.Size = new System.Drawing.Size(77, 17);
            this._connectioninfoLab.Spring = true;
            this._connectioninfoLab.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _userStatusLabel
            // 
            this._userStatusLabel.AutoSize = false;
            this._userStatusLabel.Image = global::PEA.BPM.Infrastructure.Layout.Properties.Resources.User;
            this._userStatusLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._userStatusLabel.Name = "_userStatusLabel";
            this._userStatusLabel.Size = new System.Drawing.Size(200, 17);
            this._userStatusLabel.Text = "-";
            this._userStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._userStatusLabel.ToolTipText = "ชื่อผู้ใช้งาน";
            // 
            // _dateStatusLabel
            // 
            this._dateStatusLabel.AutoSize = false;
            this._dateStatusLabel.Image = global::PEA.BPM.Infrastructure.Layout.Properties.Resources.Calendar;
            this._dateStatusLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._dateStatusLabel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._dateStatusLabel.Name = "_dateStatusLabel";
            this._dateStatusLabel.Size = new System.Drawing.Size(100, 17);
            this._dateStatusLabel.Text = "00/00/0000";
            this._dateStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._dateStatusLabel.ToolTipText = "วันที่";
            // 
            // _timeStatusLabel
            // 
            this._timeStatusLabel.AutoSize = false;
            this._timeStatusLabel.Image = global::PEA.BPM.Infrastructure.Layout.Properties.Resources.Clock;
            this._timeStatusLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._timeStatusLabel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._timeStatusLabel.Name = "_timeStatusLabel";
            this._timeStatusLabel.Size = new System.Drawing.Size(80, 17);
            this._timeStatusLabel.Text = "00:00:00";
            this._timeStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._timeStatusLabel.ToolTipText = "เวลา";
            // 
            // _connectionStatusLabel
            // 
            this._connectionStatusLabel.AutoSize = false;
            this._connectionStatusLabel.Image = global::PEA.BPM.Infrastructure.Layout.Properties.Resources.green;
            this._connectionStatusLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._connectionStatusLabel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._connectionStatusLabel.Name = "_connectionStatusLabel";
            this._connectionStatusLabel.Size = new System.Drawing.Size(60, 17);
            this._connectionStatusLabel.Tag = "green";
            this._connectionStatusLabel.Text = "Online";
            this._connectionStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._connectionStatusLabel.ToolTipText = "สถานะระบบเครือข่าย";
            // 
            // _mainToolStrip
            // 
            this._mainToolStrip.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._mainToolStrip.Location = new System.Drawing.Point(0, 24);
            this._mainToolStrip.Name = "_mainToolStrip";
            this._mainToolStrip.Size = new System.Drawing.Size(710, 25);
            this._mainToolStrip.TabIndex = 5;
            this._mainToolStrip.Text = "_mainToolStrip";
            // 
            // _centerWorkspace
            // 
            this._centerWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this._centerWorkspace.Location = new System.Drawing.Point(0, 49);
            this._centerWorkspace.Name = "_centerWorkspace";
            this._centerWorkspace.Size = new System.Drawing.Size(710, 413);
            this._centerWorkspace.TabIndex = 7;
            this._centerWorkspace.Text = "_centerWorkspace";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(51, 17);
            this.toolStripStatusLabel1.Text = "";//(Release 14 QA)
            // 
            // ShellLayoutView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._centerWorkspace);
            this.Controls.Add(this._mainStatusStrip);
            this.Controls.Add(this._mainToolStrip);
            this.Controls.Add(this._mainMenuStrip);
            this.Name = "ShellLayoutView";
            this.Size = new System.Drawing.Size(710, 484);
            this._mainStatusStrip.ResumeLayout(false);
            this._mainStatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip _mainMenuStrip;
        private System.Windows.Forms.ToolStripStatusLabel _statusLabel;
        private System.Windows.Forms.StatusStrip _mainStatusStrip;
        private System.Windows.Forms.ToolStrip _mainToolStrip;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace _centerWorkspace;
        private System.Windows.Forms.ToolStripStatusLabel _userStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel _dateStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel _timeStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel _connectionStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel workIdLabel;
        private System.Windows.Forms.ToolStripStatusLabel _connectioninfoLab;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}
