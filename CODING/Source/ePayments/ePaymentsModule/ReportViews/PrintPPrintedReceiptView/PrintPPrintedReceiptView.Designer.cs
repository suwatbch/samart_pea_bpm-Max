
//----------------------------------------------------------------------------------------
// patterns & practices - Smart Client Software Factory - Guidance Package
//
// This file was generated by the "Add View" recipe.
//
// For more information see: 
// ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.scsf.2006jun/SCSF/html/03-030-Model%20View%20Presenter%20%20MVP%20.htm
//
// Latest version of this Guidance Package: http://go.microsoft.com/fwlink/?LinkId=62182
//----------------------------------------------------------------------------------------

namespace PEA.BPM.ePaymentsModule
{
    partial class PrintPPrintedReceiptView
    {
        /// <summary>
        /// The presenter used by this view.
        /// </summary>
        private PEA.BPM.ePaymentsModule.PrintPPrintedReceiptViewPresenter _presenter = null;

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
            this.searchPanel = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtReceiptId = new System.Windows.Forms.MaskedTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnClearReceiptSearch = new System.Windows.Forms.Button();
            this.btnReceiptSearch = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbPaymentNum = new System.Windows.Forms.ComboBox();
            this.cmbPaymentDt = new System.Windows.Forms.DateTimePicker();
            this.cmbAgent = new System.Windows.Forms.ComboBox();
            this.cmbUploadDt = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.searchPanel.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchPanel
            // 
            this.searchPanel.BackColor = System.Drawing.Color.White;
            this.searchPanel.Controls.Add(this.groupBox2);
            this.searchPanel.Controls.Add(this.groupBox1);
            this.searchPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchPanel.Location = new System.Drawing.Point(10, 10);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Size = new System.Drawing.Size(211, 464);
            this.searchPanel.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtReceiptId);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.btnClearReceiptSearch);
            this.groupBox2.Controls.Add(this.btnReceiptSearch);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 9F);
            this.groupBox2.Location = new System.Drawing.Point(6, 321);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(198, 125);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "��˹����͹䢡�þ���������";
            // 
            // txtReceiptId
            // 
            this.txtReceiptId.BackColor = System.Drawing.Color.White;
            this.txtReceiptId.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtReceiptId.Location = new System.Drawing.Point(8, 47);
            this.txtReceiptId.Mask = "A000000000000000";
            this.txtReceiptId.Name = "txtReceiptId";
            this.txtReceiptId.Size = new System.Drawing.Size(184, 22);
            this.txtReceiptId.TabIndex = 20;
            this.txtReceiptId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtReceiptId_KeyDown);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label9.Location = new System.Drawing.Point(12, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 14);
            this.label9.TabIndex = 19;
            this.label9.Text = "�Ţ���������Ѻ�Թ";
            // 
            // btnClearReceiptSearch
            // 
            this.btnClearReceiptSearch.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnClearReceiptSearch.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnClearReceiptSearch.Location = new System.Drawing.Point(103, 85);
            this.btnClearReceiptSearch.Name = "btnClearReceiptSearch";
            this.btnClearReceiptSearch.Size = new System.Drawing.Size(85, 25);
            this.btnClearReceiptSearch.TabIndex = 6;
            this.btnClearReceiptSearch.Text = "��ҧ���";
            this.btnClearReceiptSearch.UseVisualStyleBackColor = false;
            this.btnClearReceiptSearch.Click += new System.EventHandler(this.btnClearReceiptSearch_Click);
            // 
            // btnReceiptSearch
            // 
            this.btnReceiptSearch.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnReceiptSearch.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnReceiptSearch.Location = new System.Drawing.Point(12, 85);
            this.btnReceiptSearch.Name = "btnReceiptSearch";
            this.btnReceiptSearch.Size = new System.Drawing.Size(85, 25);
            this.btnReceiptSearch.TabIndex = 5;
            this.btnReceiptSearch.Text = "�����";
            this.btnReceiptSearch.UseVisualStyleBackColor = false;
            this.btnReceiptSearch.Click += new System.EventHandler(this.btnReceiptSearch_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbPaymentNum);
            this.groupBox1.Controls.Add(this.cmbPaymentDt);
            this.groupBox1.Controls.Add(this.cmbAgent);
            this.groupBox1.Controls.Add(this.cmbUploadDt);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.btnPrint);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.groupBox1.Location = new System.Drawing.Point(6, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(198, 287);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "��˹����͹䢡�þ���������";
            // 
            // cmbPaymentNum
            // 
            this.cmbPaymentNum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaymentNum.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cmbPaymentNum.FormattingEnabled = true;
            this.cmbPaymentNum.Location = new System.Drawing.Point(8, 208);
            this.cmbPaymentNum.Name = "cmbPaymentNum";
            this.cmbPaymentNum.Size = new System.Drawing.Size(184, 22);
            this.cmbPaymentNum.TabIndex = 24;
            // 
            // cmbPaymentDt
            // 
            this.cmbPaymentDt.CalendarFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cmbPaymentDt.CustomFormat = "dd/MM/yyyy";
            this.cmbPaymentDt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cmbPaymentDt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.cmbPaymentDt.Location = new System.Drawing.Point(8, 154);
            this.cmbPaymentDt.Name = "cmbPaymentDt";
            this.cmbPaymentDt.Size = new System.Drawing.Size(184, 22);
            this.cmbPaymentDt.TabIndex = 22;
            this.cmbPaymentDt.ValueChanged += new System.EventHandler(this.cmbPaymentDt_ValueChanged);
            // 
            // cmbAgent
            // 
            this.cmbAgent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAgent.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cmbAgent.FormattingEnabled = true;
            this.cmbAgent.Location = new System.Drawing.Point(8, 101);
            this.cmbAgent.Name = "cmbAgent";
            this.cmbAgent.Size = new System.Drawing.Size(184, 22);
            this.cmbAgent.TabIndex = 21;
            this.cmbAgent.SelectedIndexChanged += new System.EventHandler(this.cmbAgent_SelectedIndexChanged);
            // 
            // cmbUploadDt
            // 
            this.cmbUploadDt.CalendarFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cmbUploadDt.CustomFormat = "dd/MM/yyyy";
            this.cmbUploadDt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cmbUploadDt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.cmbUploadDt.Location = new System.Drawing.Point(8, 48);
            this.cmbUploadDt.Name = "cmbUploadDt";
            this.cmbUploadDt.Size = new System.Drawing.Size(184, 22);
            this.cmbUploadDt.TabIndex = 20;
            this.cmbUploadDt.ValueChanged += new System.EventHandler(this.cmbUploadDt_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label4.Location = new System.Drawing.Point(12, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 14);
            this.label4.TabIndex = 19;
            this.label4.Text = "�ѹ�������ż����";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(12, 190);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 14);
            this.label1.TabIndex = 14;
            this.label1.Text = "���駷�����";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(12, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 14);
            this.label2.TabIndex = 12;
            this.label2.Text = "�ѹ�������Թ";
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnClear.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnClear.Location = new System.Drawing.Point(103, 246);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(85, 25);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "��ҧ���";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label6.Location = new System.Drawing.Point(12, 83);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 14);
            this.label6.TabIndex = 10;
            this.label6.Text = "���᷹";
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnPrint.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnPrint.Location = new System.Drawing.Point(12, 246);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(85, 25);
            this.btnPrint.TabIndex = 5;
            this.btnPrint.Text = "�����";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // PrintPPrintedReceiptView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(211, 117, 191);
            this.Controls.Add(this.searchPanel);
            this.Name = "PrintPPrintedReceiptView";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(231, 519);
            this.searchPanel.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel searchPanel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbPaymentNum;
        private System.Windows.Forms.DateTimePicker cmbPaymentDt;
        private System.Windows.Forms.ComboBox cmbAgent;
        private System.Windows.Forms.DateTimePicker cmbUploadDt;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnClearReceiptSearch;
        private System.Windows.Forms.Button btnReceiptSearch;
        private System.Windows.Forms.MaskedTextBox txtReceiptId;
    }
}

