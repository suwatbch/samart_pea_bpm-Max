namespace PEA.BPM.ToolModule
{
    partial class NewsView
    {
        /// <summary>
        /// The presenter used by this view.
        /// </summary>
        private PEA.BPM.ToolModule.NewsViewPresenter _presenter = null;

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
            if (disposing)
            {
                if (_presenter != null)
                    _presenter.Dispose();

                if (components != null)
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gridViewBroadcastLog = new System.Windows.Forms.DataGridView();
            this.readSymbolDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sentDtDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.broadcastTopicDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.broadcastIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.detailDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.expireDtDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isReadDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.isOpenedDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ButtonOpen = new System.Windows.Forms.DataGridViewButtonColumn();
            this.newsBroadcastInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.labelHeader = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.enableBt = new System.Windows.Forms.Button();
            this.closeBt = new System.Windows.Forms.Button();
            this.labelDate = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewBroadcastLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.newsBroadcastInfoBindingSource)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridViewBroadcastLog
            // 
            this.gridViewBroadcastLog.AllowUserToAddRows = false;
            this.gridViewBroadcastLog.AllowUserToDeleteRows = false;
            this.gridViewBroadcastLog.AllowUserToResizeRows = false;
            this.gridViewBroadcastLog.AutoGenerateColumns = false;
            this.gridViewBroadcastLog.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridViewBroadcastLog.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridViewBroadcastLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridViewBroadcastLog.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.readSymbolDataGridViewTextBoxColumn,
            this.sentDtDataGridViewTextBoxColumn,
            this.broadcastTopicDataGridViewTextBoxColumn,
            this.broadcastIdDataGridViewTextBoxColumn,
            this.detailDataGridViewTextBoxColumn,
            this.expireDtDataGridViewTextBoxColumn,
            this.cmdIdDataGridViewTextBoxColumn,
            this.isReadDataGridViewCheckBoxColumn,
            this.isOpenedDataGridViewCheckBoxColumn,
            this.ButtonOpen});
            this.gridViewBroadcastLog.DataSource = this.newsBroadcastInfoBindingSource;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridViewBroadcastLog.DefaultCellStyle = dataGridViewCellStyle6;
            this.gridViewBroadcastLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridViewBroadcastLog.Location = new System.Drawing.Point(10, 90);
            this.gridViewBroadcastLog.MultiSelect = false;
            this.gridViewBroadcastLog.Name = "gridViewBroadcastLog";
            this.gridViewBroadcastLog.ReadOnly = true;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridViewBroadcastLog.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.gridViewBroadcastLog.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.gridViewBroadcastLog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridViewBroadcastLog.Size = new System.Drawing.Size(640, 529);
            this.gridViewBroadcastLog.TabIndex = 6;
            this.gridViewBroadcastLog.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridViewBroadcastLog_CellDoubleClick);
            this.gridViewBroadcastLog.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gridViewBroadcastLog_CellFormatting);
            this.gridViewBroadcastLog.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridViewBroadcastLog_CellClick);
            // 
            // readSymbolDataGridViewTextBoxColumn
            // 
            this.readSymbolDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.readSymbolDataGridViewTextBoxColumn.DataPropertyName = "ReadSymbol";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Red;
            dataGridViewCellStyle2.NullValue = null;
            this.readSymbolDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.readSymbolDataGridViewTextBoxColumn.FillWeight = 40.60914F;
            this.readSymbolDataGridViewTextBoxColumn.Frozen = true;
            this.readSymbolDataGridViewTextBoxColumn.HeaderText = "ข่าวใหม่";
            this.readSymbolDataGridViewTextBoxColumn.Name = "readSymbolDataGridViewTextBoxColumn";
            this.readSymbolDataGridViewTextBoxColumn.ReadOnly = true;
            this.readSymbolDataGridViewTextBoxColumn.Width = 50;
            // 
            // sentDtDataGridViewTextBoxColumn
            // 
            this.sentDtDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.sentDtDataGridViewTextBoxColumn.DataPropertyName = "SentDt";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle3.Format = "dd/MM/yyyy HH:mm น.";
            dataGridViewCellStyle3.NullValue = null;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.sentDtDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.sentDtDataGridViewTextBoxColumn.FillWeight = 124.1205F;
            this.sentDtDataGridViewTextBoxColumn.Frozen = true;
            this.sentDtDataGridViewTextBoxColumn.HeaderText = "วัน-เวลา ที่ประกาศข่าว";
            this.sentDtDataGridViewTextBoxColumn.Name = "sentDtDataGridViewTextBoxColumn";
            this.sentDtDataGridViewTextBoxColumn.ReadOnly = true;
            this.sentDtDataGridViewTextBoxColumn.Width = 130;
            // 
            // broadcastTopicDataGridViewTextBoxColumn
            // 
            this.broadcastTopicDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.broadcastTopicDataGridViewTextBoxColumn.DataPropertyName = "BroadcastTopic";
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.broadcastTopicDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.broadcastTopicDataGridViewTextBoxColumn.FillWeight = 119.3985F;
            this.broadcastTopicDataGridViewTextBoxColumn.HeaderText = "หัวข้อข่าว";
            this.broadcastTopicDataGridViewTextBoxColumn.Name = "broadcastTopicDataGridViewTextBoxColumn";
            this.broadcastTopicDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // broadcastIdDataGridViewTextBoxColumn
            // 
            this.broadcastIdDataGridViewTextBoxColumn.DataPropertyName = "BroadcastId";
            this.broadcastIdDataGridViewTextBoxColumn.HeaderText = "BroadcastId";
            this.broadcastIdDataGridViewTextBoxColumn.Name = "broadcastIdDataGridViewTextBoxColumn";
            this.broadcastIdDataGridViewTextBoxColumn.ReadOnly = true;
            this.broadcastIdDataGridViewTextBoxColumn.Visible = false;
            // 
            // detailDataGridViewTextBoxColumn
            // 
            this.detailDataGridViewTextBoxColumn.DataPropertyName = "Detail";
            this.detailDataGridViewTextBoxColumn.HeaderText = "Detail";
            this.detailDataGridViewTextBoxColumn.Name = "detailDataGridViewTextBoxColumn";
            this.detailDataGridViewTextBoxColumn.ReadOnly = true;
            this.detailDataGridViewTextBoxColumn.Visible = false;
            // 
            // expireDtDataGridViewTextBoxColumn
            // 
            this.expireDtDataGridViewTextBoxColumn.DataPropertyName = "ExpireDt";
            this.expireDtDataGridViewTextBoxColumn.HeaderText = "ExpireDt";
            this.expireDtDataGridViewTextBoxColumn.Name = "expireDtDataGridViewTextBoxColumn";
            this.expireDtDataGridViewTextBoxColumn.ReadOnly = true;
            this.expireDtDataGridViewTextBoxColumn.Visible = false;
            // 
            // cmdIdDataGridViewTextBoxColumn
            // 
            this.cmdIdDataGridViewTextBoxColumn.DataPropertyName = "CmdId";
            this.cmdIdDataGridViewTextBoxColumn.HeaderText = "CmdId";
            this.cmdIdDataGridViewTextBoxColumn.Name = "cmdIdDataGridViewTextBoxColumn";
            this.cmdIdDataGridViewTextBoxColumn.ReadOnly = true;
            this.cmdIdDataGridViewTextBoxColumn.Visible = false;
            // 
            // isReadDataGridViewCheckBoxColumn
            // 
            this.isReadDataGridViewCheckBoxColumn.DataPropertyName = "IsRead";
            this.isReadDataGridViewCheckBoxColumn.HeaderText = "IsRead";
            this.isReadDataGridViewCheckBoxColumn.Name = "isReadDataGridViewCheckBoxColumn";
            this.isReadDataGridViewCheckBoxColumn.ReadOnly = true;
            this.isReadDataGridViewCheckBoxColumn.Visible = false;
            // 
            // isOpenedDataGridViewCheckBoxColumn
            // 
            this.isOpenedDataGridViewCheckBoxColumn.DataPropertyName = "IsOpened";
            this.isOpenedDataGridViewCheckBoxColumn.HeaderText = "IsOpened";
            this.isOpenedDataGridViewCheckBoxColumn.Name = "isOpenedDataGridViewCheckBoxColumn";
            this.isOpenedDataGridViewCheckBoxColumn.ReadOnly = true;
            this.isOpenedDataGridViewCheckBoxColumn.Visible = false;
            // 
            // ButtonOpen
            // 
            this.ButtonOpen.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.ButtonOpen.DefaultCellStyle = dataGridViewCellStyle5;
            this.ButtonOpen.FillWeight = 115.8719F;
            this.ButtonOpen.HeaderText = "";
            this.ButtonOpen.Name = "ButtonOpen";
            this.ButtonOpen.ReadOnly = true;
            this.ButtonOpen.Text = "เปิด";
            this.ButtonOpen.ToolTipText = "เปิด : อ่านข่าวที่ได้รับ";
            this.ButtonOpen.UseColumnTextForButtonValue = true;
            this.ButtonOpen.Width = 80;
            // 
            // newsBroadcastInfoBindingSource
            // 
            this.newsBroadcastInfoBindingSource.DataSource = typeof(PEA.BPM.NewsBroadcast.Interface.BusinessEntities.NewsBroadcastInfo);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRefresh.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.buttonRefresh.Image = global::PEA.BPM.ToolModule.Properties.Resources.RepeatHSR;
            this.buttonRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonRefresh.Location = new System.Drawing.Point(444, 45);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(92, 28);
            this.buttonRefresh.TabIndex = 0;
            this.buttonRefresh.Text = "  รีเฟรช";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // labelHeader
            // 
            this.labelHeader.AutoSize = true;
            this.labelHeader.Font = new System.Drawing.Font("Tahoma", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.labelHeader.Location = new System.Drawing.Point(12, 10);
            this.labelHeader.Name = "labelHeader";
            this.labelHeader.Size = new System.Drawing.Size(281, 35);
            this.labelHeader.TabIndex = 0;
            this.labelHeader.Text = "รายการข่าวประจำวัน";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.enableBt);
            this.panel1.Controls.Add(this.closeBt);
            this.panel1.Controls.Add(this.buttonRefresh);
            this.panel1.Controls.Add(this.labelDate);
            this.panel1.Controls.Add(this.labelHeader);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(10, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(640, 80);
            this.panel1.TabIndex = 5;
            // 
            // enableBt
            // 
            this.enableBt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.enableBt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.enableBt.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.enableBt.Location = new System.Drawing.Point(346, 45);
            this.enableBt.Name = "enableBt";
            this.enableBt.Size = new System.Drawing.Size(92, 28);
            this.enableBt.TabIndex = 3;
            this.enableBt.Text = "Disable";
            this.enableBt.UseVisualStyleBackColor = true;
            this.enableBt.Click += new System.EventHandler(this.enableBt_Click);
            // 
            // closeBt
            // 
            this.closeBt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeBt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.closeBt.Image = global::PEA.BPM.ToolModule.Properties.Resources.DeleteHS;
            this.closeBt.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.closeBt.Location = new System.Drawing.Point(542, 45);
            this.closeBt.Name = "closeBt";
            this.closeBt.Size = new System.Drawing.Size(92, 28);
            this.closeBt.TabIndex = 2;
            this.closeBt.Text = "  ปิด";
            this.closeBt.UseVisualStyleBackColor = true;
            this.closeBt.Click += new System.EventHandler(this.closeBt_Click);
            // 
            // labelDate
            // 
            this.labelDate.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.labelDate.Location = new System.Drawing.Point(17, 54);
            this.labelDate.Name = "labelDate";
            this.labelDate.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelDate.Size = new System.Drawing.Size(159, 18);
            this.labelDate.TabIndex = 1;
            this.labelDate.Text = "วันที่ 1 มกราคม 2552";
            this.labelDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel4
            // 
            this.panel4.AutoSize = true;
            this.panel4.BackColor = System.Drawing.SystemColors.Control;
            this.panel4.Controls.Add(this.gridViewBroadcastLog);
            this.panel4.Controls.Add(this.panel1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(10, 10);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(10);
            this.panel4.Size = new System.Drawing.Size(660, 629);
            this.panel4.TabIndex = 9;
            // 
            // NewsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(211, 117, 191);
            this.Controls.Add(this.panel4);
            this.Name = "NewsView";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(680, 649);
            ((System.ComponentModel.ISupportInitialize)(this.gridViewBroadcastLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.newsBroadcastInfoBindingSource)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gridViewBroadcastLog;
        private System.Windows.Forms.Label labelHeader;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Button closeBt;
        private System.Windows.Forms.Button enableBt;
        private System.Windows.Forms.BindingSource newsBroadcastInfoBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn readSymbolDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sentDtDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn broadcastTopicDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn broadcastIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn detailDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn expireDtDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn branchIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cmdIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isReadDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isOpenedDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewButtonColumn ButtonOpen;
    }
}
