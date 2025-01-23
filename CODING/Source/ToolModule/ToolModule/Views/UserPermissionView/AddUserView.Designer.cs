namespace PEA.BPM.ToolModule
{
    partial class AddUserView
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.userSearchText = new System.Windows.Forms.ToolStripTextBox();
            this.peaTSContainer = new System.Windows.Forms.ToolStripContainer();
            this.okBt = new System.Windows.Forms.Button();
            this.cancelBt = new System.Windows.Forms.Button();
            this.peaGBox = new System.Windows.Forms.GroupBox();
            this.userDataGV = new System.Windows.Forms.DataGridView();
            this.UserId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FullName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BranchId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScopeText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.blankText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.peaSearchBt = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripContainer2 = new System.Windows.Forms.ToolStripContainer();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStripContainer3 = new System.Windows.Forms.ToolStripContainer();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.peaTSContainer.ContentPanel.SuspendLayout();
            this.peaTSContainer.TopToolStripPanel.SuspendLayout();
            this.peaTSContainer.SuspendLayout();
            this.peaGBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.userDataGV)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.toolStripContainer2.SuspendLayout();
            this.toolStripContainer3.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(0, 22);
            this.toolStripLabel2.Text = "                                                                                ";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(16, 22);
            this.toolStripLabel1.Text = "   ";
            // 
            // userSearchText
            // 
            this.userSearchText.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userSearchText.Name = "userSearchText";
            this.userSearchText.Size = new System.Drawing.Size(300, 25);
            this.userSearchText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.userSearchText_KeyDown);
            // 
            // peaTSContainer
            // 
            this.peaTSContainer.BottomToolStripPanelVisible = false;
            // 
            // peaTSContainer.ContentPanel
            // 
            this.peaTSContainer.ContentPanel.Controls.Add(this.okBt);
            this.peaTSContainer.ContentPanel.Controls.Add(this.cancelBt);
            this.peaTSContainer.ContentPanel.Controls.Add(this.peaGBox);
            this.peaTSContainer.ContentPanel.Size = new System.Drawing.Size(543, 427);
            this.peaTSContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.peaTSContainer.LeftToolStripPanelVisible = false;
            this.peaTSContainer.Location = new System.Drawing.Point(0, 5);
            this.peaTSContainer.Name = "peaTSContainer";
            this.peaTSContainer.RightToolStripPanelVisible = false;
            this.peaTSContainer.Size = new System.Drawing.Size(543, 452);
            this.peaTSContainer.TabIndex = 11;
            this.peaTSContainer.Text = "toolStripContainer4";
            // 
            // peaTSContainer.TopToolStripPanel
            // 
            this.peaTSContainer.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // okBt
            // 
            this.okBt.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okBt.Enabled = false;
            this.okBt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.okBt.Location = new System.Drawing.Point(316, 385);
            this.okBt.Name = "okBt";
            this.okBt.Size = new System.Drawing.Size(100, 26);
            this.okBt.TabIndex = 6;
            this.okBt.Text = "ตกลง";
            this.okBt.UseVisualStyleBackColor = true;
            this.okBt.Click += new System.EventHandler(this.okBt_Click);
            // 
            // cancelBt
            // 
            this.cancelBt.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cancelBt.Location = new System.Drawing.Point(422, 385);
            this.cancelBt.Name = "cancelBt";
            this.cancelBt.Size = new System.Drawing.Size(100, 26);
            this.cancelBt.TabIndex = 2;
            this.cancelBt.Text = "ยกเลิก";
            this.cancelBt.UseVisualStyleBackColor = true;
            // 
            // peaGBox
            // 
            this.peaGBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.peaGBox.Controls.Add(this.userDataGV);
            this.peaGBox.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.peaGBox.Location = new System.Drawing.Point(3, 8);
            this.peaGBox.Name = "peaGBox";
            this.peaGBox.Size = new System.Drawing.Size(537, 374);
            this.peaGBox.TabIndex = 0;
            this.peaGBox.TabStop = false;
            this.peaGBox.Text = "ผลลัพธ์การค้นหา";
            // 
            // userDataGV
            // 
            this.userDataGV.AllowUserToAddRows = false;
            this.userDataGV.AllowUserToDeleteRows = false;
            this.userDataGV.AllowUserToOrderColumns = true;
            this.userDataGV.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.userDataGV.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.userDataGV.BackgroundColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(211, 117, 191);
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.userDataGV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.userDataGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.userDataGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.UserId,
            this.FullName,
            this.BranchId,
            this.ScopeText,
            this.blankText});
            this.userDataGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userDataGV.GridColor = System.Drawing.Color.Gainsboro;
            this.userDataGV.Location = new System.Drawing.Point(3, 19);
            this.userDataGV.Name = "userDataGV";
            this.userDataGV.RowHeadersVisible = false;
            this.userDataGV.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userDataGV.RowsDefaultCellStyle = dataGridViewCellStyle7;
            this.userDataGV.RowTemplate.Height = 25;
            this.userDataGV.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.userDataGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.userDataGV.Size = new System.Drawing.Size(531, 352);
            this.userDataGV.TabIndex = 0;
            this.userDataGV.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.userDataGV_CellDoubleClick);
            this.userDataGV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.userDataGV_CellClick);
            // 
            // UserId
            // 
            this.UserId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.UserId.DataPropertyName = "UserId";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.UserId.DefaultCellStyle = dataGridViewCellStyle3;
            this.UserId.HeaderText = "รหัส";
            this.UserId.Name = "UserId";
            this.UserId.ReadOnly = true;
            this.UserId.Width = 80;
            // 
            // FullName
            // 
            this.FullName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.FullName.DataPropertyName = "FullName";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.FullName.DefaultCellStyle = dataGridViewCellStyle4;
            this.FullName.HeaderText = "ชื่อ - สกุล";
            this.FullName.Name = "FullName";
            this.FullName.ReadOnly = true;
            this.FullName.Width = 200;
            // 
            // BranchId
            // 
            this.BranchId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.BranchId.DataPropertyName = "BranchId";
            this.BranchId.HeaderText = "การไฟฟ้า";
            this.BranchId.Name = "BranchId";
            this.BranchId.ReadOnly = true;
            // 
            // ScopeText
            // 
            this.ScopeText.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ScopeText.DataPropertyName = "ScopeText";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Format = " ";
            dataGridViewCellStyle5.NullValue = null;
            this.ScopeText.DefaultCellStyle = dataGridViewCellStyle5;
            this.ScopeText.HeaderText = "ขอบเขต";
            this.ScopeText.Name = "ScopeText";
            this.ScopeText.ReadOnly = true;
            this.ScopeText.Width = 140;
            // 
            // blankText
            // 
            this.blankText.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.blankText.DefaultCellStyle = dataGridViewCellStyle6;
            this.blankText.HeaderText = "";
            this.blankText.Name = "blankText";
            this.blankText.ReadOnly = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.CanOverflow = false;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.peaSearchBt,
            this.userSearchText,
            this.toolStripLabel2});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(461, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // peaSearchBt
            // 
            this.peaSearchBt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.peaSearchBt.Image = global::PEA.BPM.ToolModule.Properties.Resources.ZoomHS;
            this.peaSearchBt.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.peaSearchBt.Name = "peaSearchBt";
            this.peaSearchBt.Size = new System.Drawing.Size(140, 22);
            this.peaSearchBt.Text = "ค้นหาโดย: ทั้งหมด";
            // 
            // toolStripContainer2
            // 
            // 
            // toolStripContainer2.ContentPanel
            // 
            this.toolStripContainer2.ContentPanel.Size = new System.Drawing.Size(543, 427);
            this.toolStripContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer2.Location = new System.Drawing.Point(0, 5);
            this.toolStripContainer2.Name = "toolStripContainer2";
            this.toolStripContainer2.Size = new System.Drawing.Size(543, 452);
            this.toolStripContainer2.TabIndex = 9;
            this.toolStripContainer2.Text = "toolStripContainer2";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Name";
            this.dataGridViewTextBoxColumn2.Frozen = true;
            this.dataGridViewTextBoxColumn2.HeaderText = "ชื่อการไฟฟ้า";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 200;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Id";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewTextBoxColumn1.Frozen = true;
            this.dataGridViewTextBoxColumn1.HeaderText = "รหัสการไฟฟ้า";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // toolStripContainer3
            // 
            // 
            // toolStripContainer3.ContentPanel
            // 
            this.toolStripContainer3.ContentPanel.Size = new System.Drawing.Size(543, 427);
            this.toolStripContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer3.LeftToolStripPanelVisible = false;
            this.toolStripContainer3.Location = new System.Drawing.Point(0, 5);
            this.toolStripContainer3.Name = "toolStripContainer3";
            this.toolStripContainer3.RightToolStripPanelVisible = false;
            this.toolStripContainer3.Size = new System.Drawing.Size(543, 452);
            this.toolStripContainer3.TabIndex = 10;
            this.toolStripContainer3.Text = "toolStripContainer3";
            // 
            // toolStripContainer1
            // 
            this.toolStripContainer1.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(543, 427);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 5);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(543, 452);
            this.toolStripContainer1.TabIndex = 8;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "NumOfLines";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewTextBoxColumn3.Frozen = true;
            this.dataGridViewTextBoxColumn3.HeaderText = "จำนวนสายการเก็บเงิน";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 150;
            // 
            // AddUserView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 457);
            this.Controls.Add(this.peaTSContainer);
            this.Controls.Add(this.toolStripContainer2);
            this.Controls.Add(this.toolStripContainer3);
            this.Controls.Add(this.toolStripContainer1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "AddUserView";
            this.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ค้นหาผู้ใช้งานระบบ";
            this.peaTSContainer.ContentPanel.ResumeLayout(false);
            this.peaTSContainer.TopToolStripPanel.ResumeLayout(false);
            this.peaTSContainer.TopToolStripPanel.PerformLayout();
            this.peaTSContainer.ResumeLayout(false);
            this.peaTSContainer.PerformLayout();
            this.peaGBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.userDataGV)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStripContainer2.ResumeLayout(false);
            this.toolStripContainer2.PerformLayout();
            this.toolStripContainer3.ResumeLayout(false);
            this.toolStripContainer3.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox userSearchText;
        private System.Windows.Forms.ToolStripContainer peaTSContainer;
        private System.Windows.Forms.Button okBt;
        private System.Windows.Forms.Button cancelBt;
        private System.Windows.Forms.GroupBox peaGBox;
        private System.Windows.Forms.DataGridView userDataGV;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSplitButton peaSearchBt;
        private System.Windows.Forms.ToolStripContainer toolStripContainer2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.ToolStripContainer toolStripContainer3;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserId;
        private System.Windows.Forms.DataGridViewTextBoxColumn FullName;
        private System.Windows.Forms.DataGridViewTextBoxColumn BranchId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ScopeText;
        private System.Windows.Forms.DataGridViewTextBoxColumn blankText;
    }
}