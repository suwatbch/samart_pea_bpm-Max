namespace AdministrativeTool.UnclarifyPayment
{
    partial class UnclarifyPaymentView
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
            this.pnFilter = new System.Windows.Forms.Panel();
            this.impDatePicker = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.errorTypeCBox = new System.Windows.Forms.ComboBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.payPeriodTxt = new System.Windows.Forms.Label();
            this.unclarifyPaymentGv = new System.Windows.Forms.DataGridView();
            this.FileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ImportDt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PaymentDoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReceiptNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InvoiceNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CaId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CaDoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DocType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PaymentDt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CancelFlag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Action = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Blank = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnButton = new System.Windows.Forms.Panel();
            this.footerTxt = new System.Windows.Forms.Label();
            this.labTotal = new System.Windows.Forms.Label();
            this.pnFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.unclarifyPaymentGv)).BeginInit();
            this.pnButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnFilter
            // 
            this.pnFilter.Controls.Add(this.impDatePicker);
            this.pnFilter.Controls.Add(this.label1);
            this.pnFilter.Controls.Add(this.errorTypeCBox);
            this.pnFilter.Controls.Add(this.btnRefresh);
            this.pnFilter.Controls.Add(this.payPeriodTxt);
            this.pnFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnFilter.Location = new System.Drawing.Point(0, 0);
            this.pnFilter.Name = "pnFilter";
            this.pnFilter.Size = new System.Drawing.Size(734, 38);
            this.pnFilter.TabIndex = 0;
            // 
            // impDatePicker
            // 
            this.impDatePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.impDatePicker.Location = new System.Drawing.Point(109, 8);
            this.impDatePicker.Name = "impDatePicker";
            this.impDatePicker.Size = new System.Drawing.Size(135, 21);
            this.impDatePicker.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(267, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "Error Type : ";
            // 
            // errorTypeCBox
            // 
            this.errorTypeCBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.errorTypeCBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.errorTypeCBox.FormattingEnabled = true;
            this.errorTypeCBox.Items.AddRange(new object[] {
            "เลือกทั้งหมด",
            "รายการรับเงินที่ไม่มีหนี้",
            "รายการรับเงินซ้ำ",
            "DocType ที่ไม่ควรส่งมา",
            "จำนวนเงินติดลบ"});
            this.errorTypeCBox.Location = new System.Drawing.Point(345, 8);
            this.errorTypeCBox.Name = "errorTypeCBox";
            this.errorTypeCBox.Size = new System.Drawing.Size(145, 23);
            this.errorTypeCBox.TabIndex = 6;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnRefresh.Location = new System.Drawing.Point(496, 7);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(94, 25);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "แสดงข้อมูล";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // payPeriodTxt
            // 
            this.payPeriodTxt.AutoSize = true;
            this.payPeriodTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.payPeriodTxt.Location = new System.Drawing.Point(23, 11);
            this.payPeriodTxt.Name = "payPeriodTxt";
            this.payPeriodTxt.Size = new System.Drawing.Size(80, 15);
            this.payPeriodTxt.TabIndex = 0;
            this.payPeriodTxt.Text = "Import Date : ";
            // 
            // unclarifyPaymentGv
            // 
            this.unclarifyPaymentGv.AllowUserToAddRows = false;
            this.unclarifyPaymentGv.AllowUserToDeleteRows = false;
            this.unclarifyPaymentGv.AllowUserToResizeRows = false;
            this.unclarifyPaymentGv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.unclarifyPaymentGv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FileName,
            this.ImportDt,
            this.PaymentDoc,
            this.ReceiptNo,
            this.InvoiceNo,
            this.CaId,
            this.CaDoc,
            this.DocType,
            this.PaymentDt,
            this.Amount,
            this.CancelFlag,
            this.Action,
            this.Blank});
            this.unclarifyPaymentGv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.unclarifyPaymentGv.Location = new System.Drawing.Point(0, 38);
            this.unclarifyPaymentGv.MultiSelect = false;
            this.unclarifyPaymentGv.Name = "unclarifyPaymentGv";
            this.unclarifyPaymentGv.ReadOnly = true;
            this.unclarifyPaymentGv.RowHeadersVisible = false;
            this.unclarifyPaymentGv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.unclarifyPaymentGv.Size = new System.Drawing.Size(734, 460);
            this.unclarifyPaymentGv.TabIndex = 1;
            // 
            // FileName
            // 
            this.FileName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.FileName.DataPropertyName = "FileName";
            this.FileName.HeaderText = "FileName";
            this.FileName.Name = "FileName";
            this.FileName.ReadOnly = true;
            // 
            // ImportDt
            // 
            this.ImportDt.DataPropertyName = "ModifiedDt";
            this.ImportDt.HeaderText = "ImportDt";
            this.ImportDt.Name = "ImportDt";
            this.ImportDt.ReadOnly = true;
            // 
            // PaymentDoc
            // 
            this.PaymentDoc.DataPropertyName = "PaymentDoc";
            this.PaymentDoc.HeaderText = "PaymentDoc";
            this.PaymentDoc.Name = "PaymentDoc";
            this.PaymentDoc.ReadOnly = true;
            // 
            // ReceiptNo
            // 
            this.ReceiptNo.DataPropertyName = "ReceiptNo";
            this.ReceiptNo.HeaderText = "ReceiptNo";
            this.ReceiptNo.Name = "ReceiptNo";
            this.ReceiptNo.ReadOnly = true;
            // 
            // InvoiceNo
            // 
            this.InvoiceNo.DataPropertyName = "InvoiceNo";
            this.InvoiceNo.HeaderText = "InvoiceNo";
            this.InvoiceNo.Name = "InvoiceNo";
            this.InvoiceNo.ReadOnly = true;
            // 
            // CaId
            // 
            this.CaId.DataPropertyName = "CaId";
            this.CaId.HeaderText = "CaId";
            this.CaId.Name = "CaId";
            this.CaId.ReadOnly = true;
            // 
            // CaDoc
            // 
            this.CaDoc.DataPropertyName = "CaDoc";
            this.CaDoc.HeaderText = "CaDoc";
            this.CaDoc.Name = "CaDoc";
            this.CaDoc.ReadOnly = true;
            // 
            // DocType
            // 
            this.DocType.DataPropertyName = "DocType";
            this.DocType.HeaderText = "DocType";
            this.DocType.Name = "DocType";
            this.DocType.ReadOnly = true;
            // 
            // PaymentDt
            // 
            this.PaymentDt.DataPropertyName = "PaymentDt";
            this.PaymentDt.HeaderText = "PaymentDt";
            this.PaymentDt.Name = "PaymentDt";
            this.PaymentDt.ReadOnly = true;
            // 
            // Amount
            // 
            this.Amount.DataPropertyName = "Amount";
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            // 
            // CancelFlag
            // 
            this.CancelFlag.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.CancelFlag.DataPropertyName = "CancelFlag";
            this.CancelFlag.HeaderText = "CancelFlag";
            this.CancelFlag.Name = "CancelFlag";
            this.CancelFlag.ReadOnly = true;
            this.CancelFlag.Width = 80;
            // 
            // Action
            // 
            this.Action.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Action.DataPropertyName = "Action";
            this.Action.HeaderText = "Action";
            this.Action.Name = "Action";
            this.Action.ReadOnly = true;
            this.Action.Width = 60;
            // 
            // Blank
            // 
            this.Blank.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Blank.HeaderText = "";
            this.Blank.Name = "Blank";
            this.Blank.ReadOnly = true;
            // 
            // pnButton
            // 
            this.pnButton.Controls.Add(this.footerTxt);
            this.pnButton.Controls.Add(this.labTotal);
            this.pnButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnButton.Location = new System.Drawing.Point(0, 498);
            this.pnButton.Name = "pnButton";
            this.pnButton.Size = new System.Drawing.Size(734, 32);
            this.pnButton.TabIndex = 2;
            // 
            // footerTxt
            // 
            this.footerTxt.AutoSize = true;
            this.footerTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.footerTxt.Location = new System.Drawing.Point(163, 10);
            this.footerTxt.Name = "footerTxt";
            this.footerTxt.Size = new System.Drawing.Size(0, 13);
            this.footerTxt.TabIndex = 2;
            // 
            // labTotal
            // 
            this.labTotal.AutoSize = true;
            this.labTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.labTotal.Location = new System.Drawing.Point(3, 10);
            this.labTotal.Name = "labTotal";
            this.labTotal.Size = new System.Drawing.Size(100, 13);
            this.labTotal.TabIndex = 1;
            this.labTotal.Text = "Total Record : 0";
            // 
            // UnclarifyPaymentView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.unclarifyPaymentGv);
            this.Controls.Add(this.pnFilter);
            this.Controls.Add(this.pnButton);
            this.Name = "UnclarifyPaymentView";
            this.Size = new System.Drawing.Size(734, 530);
            this.pnFilter.ResumeLayout(false);
            this.pnFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.unclarifyPaymentGv)).EndInit();
            this.pnButton.ResumeLayout(false);
            this.pnButton.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnFilter;
        private System.Windows.Forms.Label payPeriodTxt;
        private System.Windows.Forms.DataGridView unclarifyPaymentGv;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel pnButton;
        private System.Windows.Forms.Label labTotal;
        private System.Windows.Forms.Label footerTxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox errorTypeCBox;
        private System.Windows.Forms.DateTimePicker impDatePicker;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ImportDt;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaymentDoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReceiptNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvoiceNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CaId;
        private System.Windows.Forms.DataGridViewTextBoxColumn CaDoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn DocType;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaymentDt;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn CancelFlag;
        private System.Windows.Forms.DataGridViewTextBoxColumn Action;
        private System.Windows.Forms.DataGridViewTextBoxColumn Blank;

    }
}
