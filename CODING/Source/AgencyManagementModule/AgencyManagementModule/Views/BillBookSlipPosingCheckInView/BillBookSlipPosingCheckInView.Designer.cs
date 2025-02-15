
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

namespace PEA.BPM.AgencyManagementModule
{
    partial class BillBookSlipPosingCheckInView
    {
        /// <summary>
        /// The presenter used by this view.
        /// </summary>
        private PEA.BPM.AgencyManagementModule.BillBookSlipPosingCheckInViewPresenter _presenter = null;

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
            this.BillBookIdText = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.collectrdo = new System.Windows.Forms.RadioButton();
            this.notCollectrdo = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.caIdText = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.invoiceGV = new System.Windows.Forms.DataGridView();
            this.Seq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PeaCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mru = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PaymentPeriod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PmId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.billPeriodText = new System.Windows.Forms.MaskedTextBox();
            this.mruIdText = new System.Windows.Forms.MaskedTextBox();
            this.branchIdText = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBox2 = new System.Windows.Forms.MaskedTextBox();
            this.recordBt = new System.Windows.Forms.Button();
            this.ClearButton = new System.Windows.Forms.Button();
            this.cancelBt = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.invoiceGV)).BeginInit();
            this.SuspendLayout();
            // 
            // BillBookIdText
            // 
            this.BillBookIdText.BackColor = System.Drawing.SystemColors.Control;
            this.BillBookIdText.Enabled = false;
            this.BillBookIdText.Location = new System.Drawing.Point(118, 7);
            this.BillBookIdText.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BillBookIdText.Mask = "00-0000000";
            this.BillBookIdText.Name = "BillBookIdText";
            this.BillBookIdText.Size = new System.Drawing.Size(113, 23);
            this.BillBookIdText.TabIndex = 0;
            this.BillBookIdText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BillBookIdText_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "��ش���º���Ţ��� :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.collectrdo);
            this.groupBox1.Controls.Add(this.notCollectrdo);
            this.groupBox1.Location = new System.Drawing.Point(7, 31);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(425, 50);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "��駤��俿��";
            // 
            // collectrdo
            // 
            this.collectrdo.AutoSize = true;
            this.collectrdo.Location = new System.Drawing.Point(165, 19);
            this.collectrdo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.collectrdo.Name = "collectrdo";
            this.collectrdo.Size = new System.Drawing.Size(59, 20);
            this.collectrdo.TabIndex = 1;
            this.collectrdo.Text = "�ҧ��";
            this.collectrdo.UseVisualStyleBackColor = true;
            // 
            // notCollectrdo
            // 
            this.notCollectrdo.AutoSize = true;
            this.notCollectrdo.Checked = true;
            this.notCollectrdo.Location = new System.Drawing.Point(28, 19);
            this.notCollectrdo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.notCollectrdo.Name = "notCollectrdo";
            this.notCollectrdo.Size = new System.Drawing.Size(74, 20);
            this.notCollectrdo.TabIndex = 0;
            this.notCollectrdo.TabStop = true;
            this.notCollectrdo.Text = "�ҧ�����";
            this.notCollectrdo.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.caIdText);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.invoiceGV);
            this.groupBox2.Controls.Add(this.billPeriodText);
            this.groupBox2.Controls.Add(this.mruIdText);
            this.groupBox2.Controls.Add(this.branchIdText);
            this.groupBox2.Controls.Add(this.maskedTextBox2);
            this.groupBox2.Location = new System.Drawing.Point(8, 83);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(425, 348);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // caIdText
            // 
            this.caIdText.Location = new System.Drawing.Point(192, 33);
            this.caIdText.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.caIdText.MaxLength = 12;
            this.caIdText.Name = "caIdText";
            this.caIdText.Size = new System.Drawing.Size(104, 23);
            this.caIdText.TabIndex = 2;
            this.caIdText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.caIdText_KeyDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(316, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 16);
            this.label6.TabIndex = 10;
            this.label6.Text = "�����͹";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(192, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "�����Ţ������";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(156, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "���";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(62, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "���� ���.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "*";
            // 
            // invoiceGV
            // 
            this.invoiceGV.AllowUserToAddRows = false;
            this.invoiceGV.AllowUserToDeleteRows = false;
            this.invoiceGV.AllowUserToResizeColumns = false;
            this.invoiceGV.AllowUserToResizeRows = false;
            this.invoiceGV.BackgroundColor = System.Drawing.SystemColors.Window;
            this.invoiceGV.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.invoiceGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.invoiceGV.ColumnHeadersVisible = false;
            this.invoiceGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Seq,
            this.PeaCode,
            this.mru,
            this.UserId,
            this.PaymentPeriod,
            this.PmId});
            this.invoiceGV.Location = new System.Drawing.Point(9, 60);
            this.invoiceGV.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.invoiceGV.Name = "invoiceGV";
            this.invoiceGV.RowHeadersVisible = false;
            this.invoiceGV.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.invoiceGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.invoiceGV.Size = new System.Drawing.Size(410, 281);
            this.invoiceGV.TabIndex = 4;
            this.invoiceGV.KeyDown += new System.Windows.Forms.KeyEventHandler(this.invoiceGV_KeyDown);
            // 
            // Seq
            // 
            this.Seq.Frozen = true;
            this.Seq.HeaderText = "";
            this.Seq.Name = "Seq";
            this.Seq.ReadOnly = true;
            this.Seq.Width = 25;
            // 
            // PeaCode
            // 
            this.PeaCode.DataPropertyName = "BranchId";
            this.PeaCode.HeaderText = "���� ���.";
            this.PeaCode.MaxInputLength = 6;
            this.PeaCode.Name = "PeaCode";
            this.PeaCode.ReadOnly = true;
            this.PeaCode.Width = 90;
            // 
            // mru
            // 
            this.mru.DataPropertyName = "mruId";
            this.mru.HeaderText = "���";
            this.mru.MaxInputLength = 4;
            this.mru.Name = "mru";
            this.mru.ReadOnly = true;
            this.mru.Width = 42;
            // 
            // UserId
            // 
            this.UserId.DataPropertyName = "caId";
            this.UserId.HeaderText = "���ʼ�����";
            this.UserId.MaxInputLength = 12;
            this.UserId.Name = "UserId";
            this.UserId.ReadOnly = true;
            this.UserId.Width = 90;
            // 
            // PaymentPeriod
            // 
            this.PaymentPeriod.DataPropertyName = "Period";
            this.PaymentPeriod.HeaderText = "�����͹";
            this.PaymentPeriod.MaxInputLength = 6;
            this.PaymentPeriod.Name = "PaymentPeriod";
            this.PaymentPeriod.ReadOnly = true;
            this.PaymentPeriod.Width = 62;
            // 
            // PmId
            // 
            this.PmId.DataPropertyName = "PmId";
            this.PmId.HeaderText = "PmId";
            this.PmId.Name = "PmId";
            this.PmId.ReadOnly = true;
            this.PmId.Visible = false;
            // 
            // billPeriodText
            // 
            this.billPeriodText.BeepOnError = true;
            this.billPeriodText.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.billPeriodText.Location = new System.Drawing.Point(301, 33);
            this.billPeriodText.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.billPeriodText.Mask = "00/0000";
            this.billPeriodText.Name = "billPeriodText";
            this.billPeriodText.Size = new System.Drawing.Size(73, 23);
            this.billPeriodText.TabIndex = 3;
            this.billPeriodText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.billPeriodText_KeyDown);
            // 
            // mruIdText
            // 
            this.mruIdText.BeepOnError = true;
            this.mruIdText.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.mruIdText.Location = new System.Drawing.Point(146, 33);
            this.mruIdText.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.mruIdText.Mask = "0000";
            this.mruIdText.Name = "mruIdText";
            this.mruIdText.Size = new System.Drawing.Size(44, 23);
            this.mruIdText.TabIndex = 1;
            this.mruIdText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mruIdText_KeyDown);
            // 
            // branchIdText
            // 
            this.branchIdText.BeepOnError = true;
            this.branchIdText.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.branchIdText.Location = new System.Drawing.Point(41, 33);
            this.branchIdText.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.branchIdText.Mask = "A00000";
            this.branchIdText.Name = "branchIdText";
            this.branchIdText.ResetOnSpace = false;
            this.branchIdText.Size = new System.Drawing.Size(102, 23);
            this.branchIdText.TabIndex = 0;
            this.branchIdText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.branchIdText_KeyDown);
            // 
            // maskedTextBox2
            // 
            this.maskedTextBox2.Enabled = false;
            this.maskedTextBox2.Location = new System.Drawing.Point(12, 33);
            this.maskedTextBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.maskedTextBox2.Name = "maskedTextBox2";
            this.maskedTextBox2.Size = new System.Drawing.Size(27, 23);
            this.maskedTextBox2.TabIndex = 0;
            // 
            // recordBt
            // 
            this.recordBt.Image = global::PEA.BPM.AgencyManagementModule.Properties.Resources.saveHS;
            this.recordBt.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.recordBt.Location = new System.Drawing.Point(95, 445);
            this.recordBt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.recordBt.Name = "recordBt";
            this.recordBt.Size = new System.Drawing.Size(105, 28);
            this.recordBt.TabIndex = 1;
            this.recordBt.Text = "�ѹ�֡";
            this.recordBt.UseVisualStyleBackColor = true;
            this.recordBt.Click += new System.EventHandler(this.recordBt_Click);
            // 
            // ClearButton
            // 
            this.ClearButton.Image = global::PEA.BPM.AgencyManagementModule.Properties.Resources.RepeatHS;
            this.ClearButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ClearButton.Location = new System.Drawing.Point(209, 445);
            this.ClearButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(105, 28);
            this.ClearButton.TabIndex = 2;
            this.ClearButton.Text = "��͹����";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // cancelBt
            // 
            this.cancelBt.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBt.Location = new System.Drawing.Point(322, 445);
            this.cancelBt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cancelBt.Name = "cancelBt";
            this.cancelBt.Size = new System.Drawing.Size(105, 28);
            this.cancelBt.TabIndex = 3;
            this.cancelBt.Text = "�͡";
            this.cancelBt.UseVisualStyleBackColor = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.Frozen = true;
            this.dataGridViewTextBoxColumn1.HeaderText = "";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 25;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "BranchId";
            this.dataGridViewTextBoxColumn2.HeaderText = "���� ���.";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 90;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "mruId";
            this.dataGridViewTextBoxColumn3.HeaderText = "���";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 42;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "caId";
            this.dataGridViewTextBoxColumn4.HeaderText = "���ʼ�����";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 90;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "billPeriod";
            this.dataGridViewTextBoxColumn5.HeaderText = "�����͹";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 62;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "billPeriod";
            this.dataGridViewTextBoxColumn6.HeaderText = "�����͹";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 62;
            // 
            // BillBookSlipPosingCheckInView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cancelBt);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.recordBt);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BillBookIdText);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "BillBookSlipPosingCheckInView";
            this.Size = new System.Drawing.Size(436, 482);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.invoiceGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MaskedTextBox BillBookIdText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button recordBt;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.Button cancelBt;
        private System.Windows.Forms.RadioButton collectrdo;
        private System.Windows.Forms.RadioButton notCollectrdo;
        private System.Windows.Forms.DataGridView invoiceGV;
        private System.Windows.Forms.MaskedTextBox billPeriodText;
        private System.Windows.Forms.MaskedTextBox mruIdText;
        private System.Windows.Forms.MaskedTextBox branchIdText;
        private System.Windows.Forms.MaskedTextBox maskedTextBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn PmsId;
        private System.Windows.Forms.TextBox caIdText;
        private System.Windows.Forms.DataGridViewTextBoxColumn Seq;
        private System.Windows.Forms.DataGridViewTextBoxColumn PeaCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn mru;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserId;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaymentPeriod;
        private System.Windows.Forms.DataGridViewTextBoxColumn PmId;
    }
}

