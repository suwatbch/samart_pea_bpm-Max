namespace AdministrativeTool.OutOfShiftList
{
    partial class OutOfShiftMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OutOfShiftMain));
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dataGridViewOutOfShift = new System.Windows.Forms.DataGridView();
            this.CorrectedId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CorrectedPairId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CorrectedCaseCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CorrectedStage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CorrectedBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CorrectedDt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PaymentId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PaymentDt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PosId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CashierId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BranchId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OriginalPaymentId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PaidChannel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CmScope = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WorkId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WorkFlag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SyncFlag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PostDt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PostBranchServerId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModifiedDt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModifiedBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Active = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labStartDateTime = new System.Windows.Forms.Label();
            this.pnFilter = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.dtpEndDt = new System.Windows.Forms.DateTimePicker();
            this.dtpStartDt = new System.Windows.Forms.DateTimePicker();
            this.comboCaseCode = new System.Windows.Forms.ComboBox();
            this.labCaseCode = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labTotal = new System.Windows.Forms.Label();
            this.pnButton = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOutOfShift)).BeginInit();
            this.pnFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(629, 39);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(105, 23);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "OK";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // dataGridViewOutOfShift
            // 
            this.dataGridViewOutOfShift.AllowUserToAddRows = false;
            this.dataGridViewOutOfShift.AllowUserToDeleteRows = false;
            this.dataGridViewOutOfShift.AllowUserToResizeColumns = false;
            this.dataGridViewOutOfShift.AllowUserToResizeRows = false;
            this.dataGridViewOutOfShift.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOutOfShift.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CorrectedId,
            this.CorrectedPairId,
            this.CorrectedCaseCode,
            this.CorrectedStage,
            this.CorrectedBy,
            this.CorrectedDt,
            this.PaymentId,
            this.PaymentDt,
            this.PosId,
            this.CashierId,
            this.BranchId,
            this.OriginalPaymentId,
            this.PaidChannel,
            this.CmScope,
            this.WorkId,
            this.WorkFlag,
            this.SyncFlag,
            this.PostDt,
            this.PostBranchServerId,
            this.ModifiedDt,
            this.ModifiedBy,
            this.Active});
            this.dataGridViewOutOfShift.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewOutOfShift.Location = new System.Drawing.Point(0, 64);
            this.dataGridViewOutOfShift.Name = "dataGridViewOutOfShift";
            this.dataGridViewOutOfShift.ReadOnly = true;
            this.dataGridViewOutOfShift.RowHeadersVisible = false;
            this.dataGridViewOutOfShift.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewOutOfShift.Size = new System.Drawing.Size(763, 437);
            this.dataGridViewOutOfShift.TabIndex = 4;
            // 
            // CorrectedId
            // 
            this.CorrectedId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.CorrectedId.DataPropertyName = "CorrectedId";
            this.CorrectedId.Frozen = true;
            this.CorrectedId.HeaderText = "CorrectedId";
            this.CorrectedId.Name = "CorrectedId";
            this.CorrectedId.ReadOnly = true;
            this.CorrectedId.Width = 88;
            // 
            // CorrectedPairId
            // 
            this.CorrectedPairId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.CorrectedPairId.DataPropertyName = "CorrectedPairId";
            this.CorrectedPairId.Frozen = true;
            this.CorrectedPairId.HeaderText = "CorrectedPairId";
            this.CorrectedPairId.Name = "CorrectedPairId";
            this.CorrectedPairId.ReadOnly = true;
            this.CorrectedPairId.Width = 106;
            // 
            // CorrectedCaseCode
            // 
            this.CorrectedCaseCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.CorrectedCaseCode.DataPropertyName = "CorrectedCaseCode";
            this.CorrectedCaseCode.HeaderText = "CorrectedCaseCode";
            this.CorrectedCaseCode.Name = "CorrectedCaseCode";
            this.CorrectedCaseCode.ReadOnly = true;
            this.CorrectedCaseCode.Width = 128;
            // 
            // CorrectedStage
            // 
            this.CorrectedStage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.CorrectedStage.DataPropertyName = "CorrectedStage";
            this.CorrectedStage.HeaderText = "CorrectedStage";
            this.CorrectedStage.Name = "CorrectedStage";
            this.CorrectedStage.ReadOnly = true;
            this.CorrectedStage.Width = 107;
            // 
            // CorrectedBy
            // 
            this.CorrectedBy.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.CorrectedBy.DataPropertyName = "CorrectedBy";
            this.CorrectedBy.HeaderText = "CorrectedBy";
            this.CorrectedBy.Name = "CorrectedBy";
            this.CorrectedBy.ReadOnly = true;
            this.CorrectedBy.Width = 91;
            // 
            // CorrectedDt
            // 
            this.CorrectedDt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.CorrectedDt.DataPropertyName = "CorrectedDt";
            this.CorrectedDt.HeaderText = "CorrectedDt";
            this.CorrectedDt.Name = "CorrectedDt";
            this.CorrectedDt.ReadOnly = true;
            this.CorrectedDt.Width = 90;
            // 
            // PaymentId
            // 
            this.PaymentId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.PaymentId.DataPropertyName = "PaymentId";
            this.PaymentId.HeaderText = "PaymentId";
            this.PaymentId.Name = "PaymentId";
            this.PaymentId.ReadOnly = true;
            this.PaymentId.Width = 83;
            // 
            // PaymentDt
            // 
            this.PaymentDt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.PaymentDt.DataPropertyName = "PaymentDt";
            this.PaymentDt.HeaderText = "PaymentDt";
            this.PaymentDt.Name = "PaymentDt";
            this.PaymentDt.ReadOnly = true;
            this.PaymentDt.Width = 85;
            // 
            // PosId
            // 
            this.PosId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.PosId.DataPropertyName = "PosId";
            this.PosId.HeaderText = "PosId";
            this.PosId.Name = "PosId";
            this.PosId.ReadOnly = true;
            this.PosId.Width = 60;
            // 
            // CashierId
            // 
            this.CashierId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.CashierId.DataPropertyName = "CashierId";
            this.CashierId.HeaderText = "CashierId";
            this.CashierId.Name = "CashierId";
            this.CashierId.ReadOnly = true;
            this.CashierId.Width = 77;
            // 
            // BranchId
            // 
            this.BranchId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.BranchId.DataPropertyName = "BranchId";
            this.BranchId.HeaderText = "BranchId";
            this.BranchId.Name = "BranchId";
            this.BranchId.ReadOnly = true;
            this.BranchId.Width = 76;
            // 
            // OriginalPaymentId
            // 
            this.OriginalPaymentId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.OriginalPaymentId.DataPropertyName = "OriginalPaymentId";
            this.OriginalPaymentId.HeaderText = "OriginalPaymentId";
            this.OriginalPaymentId.Name = "OriginalPaymentId";
            this.OriginalPaymentId.ReadOnly = true;
            this.OriginalPaymentId.Width = 118;
            // 
            // PaidChannel
            // 
            this.PaidChannel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.PaidChannel.DataPropertyName = "PaidChannel";
            this.PaidChannel.HeaderText = "PaidChannel";
            this.PaidChannel.Name = "PaidChannel";
            this.PaidChannel.ReadOnly = true;
            this.PaidChannel.Width = 93;
            // 
            // CmScope
            // 
            this.CmScope.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.CmScope.DataPropertyName = "CmScope";
            this.CmScope.HeaderText = "CmScope";
            this.CmScope.Name = "CmScope";
            this.CmScope.ReadOnly = true;
            this.CmScope.Width = 79;
            // 
            // WorkId
            // 
            this.WorkId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.WorkId.DataPropertyName = "WorkId";
            this.WorkId.HeaderText = "WorkId";
            this.WorkId.Name = "WorkId";
            this.WorkId.ReadOnly = true;
            this.WorkId.Width = 68;
            // 
            // WorkFlag
            // 
            this.WorkFlag.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.WorkFlag.DataPropertyName = "WorkFlag";
            this.WorkFlag.HeaderText = "WorkFlag";
            this.WorkFlag.Name = "WorkFlag";
            this.WorkFlag.ReadOnly = true;
            this.WorkFlag.Width = 79;
            // 
            // SyncFlag
            // 
            this.SyncFlag.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.SyncFlag.DataPropertyName = "SyncFlag";
            this.SyncFlag.HeaderText = "SyncFlag";
            this.SyncFlag.Name = "SyncFlag";
            this.SyncFlag.ReadOnly = true;
            this.SyncFlag.Width = 77;
            // 
            // PostDt
            // 
            this.PostDt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.PostDt.DataPropertyName = "PostDt";
            this.PostDt.HeaderText = "PostDt";
            this.PostDt.Name = "PostDt";
            this.PostDt.ReadOnly = true;
            this.PostDt.Width = 65;
            // 
            // PostBranchServerId
            // 
            this.PostBranchServerId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.PostBranchServerId.DataPropertyName = "PostBranchServerId";
            this.PostBranchServerId.HeaderText = "PostBranchServerId";
            this.PostBranchServerId.Name = "PostBranchServerId";
            this.PostBranchServerId.ReadOnly = true;
            this.PostBranchServerId.Width = 128;
            // 
            // ModifiedDt
            // 
            this.ModifiedDt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ModifiedDt.DataPropertyName = "ModifiedDt";
            this.ModifiedDt.HeaderText = "ModifiedDt";
            this.ModifiedDt.Name = "ModifiedDt";
            this.ModifiedDt.ReadOnly = true;
            this.ModifiedDt.Width = 84;
            // 
            // ModifiedBy
            // 
            this.ModifiedBy.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ModifiedBy.DataPropertyName = "ModifiedBy";
            this.ModifiedBy.HeaderText = "ModifiedBy";
            this.ModifiedBy.Name = "ModifiedBy";
            this.ModifiedBy.ReadOnly = true;
            this.ModifiedBy.Width = 85;
            // 
            // Active
            // 
            this.Active.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Active.DataPropertyName = "Active";
            this.Active.HeaderText = "Active";
            this.Active.Name = "Active";
            this.Active.ReadOnly = true;
            this.Active.Width = 63;
            // 
            // labStartDateTime
            // 
            this.labStartDateTime.AutoSize = true;
            this.labStartDateTime.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labStartDateTime.Location = new System.Drawing.Point(81, 43);
            this.labStartDateTime.Name = "labStartDateTime";
            this.labStartDateTime.Size = new System.Drawing.Size(70, 14);
            this.labStartDateTime.TabIndex = 0;
            this.labStartDateTime.Text = "Start Date : ";
            // 
            // pnFilter
            // 
            this.pnFilter.Controls.Add(this.label2);
            this.pnFilter.Controls.Add(this.pictureBox1);
            this.pnFilter.Controls.Add(this.dtpEndDt);
            this.pnFilter.Controls.Add(this.dtpStartDt);
            this.pnFilter.Controls.Add(this.comboCaseCode);
            this.pnFilter.Controls.Add(this.labCaseCode);
            this.pnFilter.Controls.Add(this.label1);
            this.pnFilter.Controls.Add(this.btnRefresh);
            this.pnFilter.Controls.Add(this.labStartDateTime);
            this.pnFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnFilter.Location = new System.Drawing.Point(0, 0);
            this.pnFilter.Name = "pnFilter";
            this.pnFilter.Size = new System.Drawing.Size(763, 64);
            this.pnFilter.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.ForeColor = System.Drawing.Color.MediumBlue;
            this.label2.Location = new System.Drawing.Point(81, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(238, 18);
            this.label2.TabIndex = 9;
            this.label2.Text = "Payment Log ของรายการรับเงินนอกกะ";
            // 
            // pictureBox1
            // 
            this.pictureBox1.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.ErrorImage")));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(8, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(66, 60);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // dtpEndDt
            // 
            this.dtpEndDt.CustomFormat = "dd/MM/yyyy";
            this.dtpEndDt.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEndDt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDt.Location = new System.Drawing.Point(312, 39);
            this.dtpEndDt.Name = "dtpEndDt";
            this.dtpEndDt.Size = new System.Drawing.Size(94, 22);
            this.dtpEndDt.TabIndex = 1;
            // 
            // dtpStartDt
            // 
            this.dtpStartDt.CustomFormat = "dd/MM/yyyy";
            this.dtpStartDt.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStartDt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDt.Location = new System.Drawing.Point(147, 39);
            this.dtpStartDt.Name = "dtpStartDt";
            this.dtpStartDt.Size = new System.Drawing.Size(94, 22);
            this.dtpStartDt.TabIndex = 1;
            // 
            // comboCaseCode
            // 
            this.comboCaseCode.BackColor = System.Drawing.Color.White;
            this.comboCaseCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboCaseCode.FormattingEnabled = true;
            this.comboCaseCode.Items.AddRange(new object[] {
            "All",
            "U00"});
            this.comboCaseCode.Location = new System.Drawing.Point(495, 40);
            this.comboCaseCode.Name = "comboCaseCode";
            this.comboCaseCode.Size = new System.Drawing.Size(102, 21);
            this.comboCaseCode.TabIndex = 7;
            // 
            // labCaseCode
            // 
            this.labCaseCode.AutoSize = true;
            this.labCaseCode.Location = new System.Drawing.Point(417, 44);
            this.labCaseCode.Name = "labCaseCode";
            this.labCaseCode.Size = new System.Drawing.Size(79, 13);
            this.labCaseCode.TabIndex = 6;
            this.labCaseCode.Text = "CaseCode ID : ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(246, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "End Date :";
            // 
            // labTotal
            // 
            this.labTotal.AutoSize = true;
            this.labTotal.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labTotal.Location = new System.Drawing.Point(7, 10);
            this.labTotal.Name = "labTotal";
            this.labTotal.Size = new System.Drawing.Size(89, 14);
            this.labTotal.TabIndex = 0;
            this.labTotal.Text = "Total Record : 0";
            // 
            // pnButton
            // 
            this.pnButton.Controls.Add(this.labTotal);
            this.pnButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnButton.Location = new System.Drawing.Point(0, 501);
            this.pnButton.Name = "pnButton";
            this.pnButton.Size = new System.Drawing.Size(763, 32);
            this.pnButton.TabIndex = 5;
            // 
            // OutOfShiftMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridViewOutOfShift);
            this.Controls.Add(this.pnFilter);
            this.Controls.Add(this.pnButton);
            this.Name = "OutOfShiftMain";
            this.Size = new System.Drawing.Size(763, 533);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOutOfShift)).EndInit();
            this.pnFilter.ResumeLayout(false);
            this.pnFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnButton.ResumeLayout(false);
            this.pnButton.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label labStartDateTime;
        private System.Windows.Forms.Panel pnFilter;
        private System.Windows.Forms.DateTimePicker dtpStartDt;
        private System.Windows.Forms.Label labTotal;
        private System.Windows.Forms.Panel pnButton;
        private System.Windows.Forms.Label labCaseCode;
        private System.Windows.Forms.ComboBox comboCaseCode;
        private System.Windows.Forms.DataGridView dataGridViewOutOfShift;
        private System.Windows.Forms.DateTimePicker dtpEndDt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn CorrectedId;
        private System.Windows.Forms.DataGridViewTextBoxColumn CorrectedPairId;
        private System.Windows.Forms.DataGridViewTextBoxColumn CorrectedCaseCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn CorrectedStage;
        private System.Windows.Forms.DataGridViewTextBoxColumn CorrectedBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn CorrectedDt;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaymentId;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaymentDt;
        private System.Windows.Forms.DataGridViewTextBoxColumn PosId;
        private System.Windows.Forms.DataGridViewTextBoxColumn CashierId;
        private System.Windows.Forms.DataGridViewTextBoxColumn BranchId;
        private System.Windows.Forms.DataGridViewTextBoxColumn OriginalPaymentId;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaidChannel;
        private System.Windows.Forms.DataGridViewTextBoxColumn CmScope;
        private System.Windows.Forms.DataGridViewTextBoxColumn WorkId;
        private System.Windows.Forms.DataGridViewTextBoxColumn WorkFlag;
        private System.Windows.Forms.DataGridViewTextBoxColumn SyncFlag;
        private System.Windows.Forms.DataGridViewTextBoxColumn PostDt;
        private System.Windows.Forms.DataGridViewTextBoxColumn PostBranchServerId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModifiedDt;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModifiedBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn Active;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
    }
}
