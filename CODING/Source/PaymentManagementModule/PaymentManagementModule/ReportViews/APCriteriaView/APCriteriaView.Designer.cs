
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

namespace PEA.BPM.PaymentManagementModule
{
    partial class APCriteriaView
    {
        /// <summary>
        /// The presenter used by this view.
        /// </summary>
        private PEA.BPM.PaymentManagementModule.APCriteriaViewPresenter _presenter = null;

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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.branchIdMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.displayReportButton = new System.Windows.Forms.Button();
            this.transactionFromDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.posIdMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.cashierIdMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.transactionToDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.searchPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchPanel
            // 
            this.searchPanel.BackColor = System.Drawing.Color.White;
            this.searchPanel.Controls.Add(this.groupBox1);
            this.searchPanel.Controls.Add(this.label1);
            this.searchPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.searchPanel.Location = new System.Drawing.Point(10, 10);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Size = new System.Drawing.Size(216, 354);
            this.searchPanel.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.transactionToDateTimePicker);
            this.groupBox1.Controls.Add(this.branchIdMaskedTextBox);
            this.groupBox1.Controls.Add(this.displayReportButton);
            this.groupBox1.Controls.Add(this.transactionFromDateTimePicker);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.posIdMaskedTextBox);
            this.groupBox1.Controls.Add(this.cashierIdMaskedTextBox);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.groupBox1.Location = new System.Drawing.Point(10, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(193, 305);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "���͹�";
            // 
            // branchIdMaskedTextBox
            // 
            this.branchIdMaskedTextBox.Location = new System.Drawing.Point(6, 42);
            this.branchIdMaskedTextBox.Name = "branchIdMaskedTextBox";
            this.branchIdMaskedTextBox.Size = new System.Drawing.Size(180, 22);
            this.branchIdMaskedTextBox.TabIndex = 1;
            this.branchIdMaskedTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.branchIdMaskedTextBox_KeyPress);
            // 
            // displayReportButton
            // 
            this.displayReportButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.displayReportButton.Location = new System.Drawing.Point(6, 261);
            this.displayReportButton.Name = "displayReportButton";
            this.displayReportButton.Size = new System.Drawing.Size(100, 28);
            this.displayReportButton.TabIndex = 3;
            this.displayReportButton.Text = "�ʴ���§ҹ";
            this.displayReportButton.UseVisualStyleBackColor = false;
            this.displayReportButton.Click += new System.EventHandler(this.displayReportButton_Click);
            // 
            // transactionFromDateTimePicker
            // 
            this.transactionFromDateTimePicker.CustomFormat = "dd/MM/yyyy";
            this.transactionFromDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.transactionFromDateTimePicker.Location = new System.Drawing.Point(6, 183);
            this.transactionFromDateTimePicker.Name = "transactionFromDateTimePicker";
            this.transactionFromDateTimePicker.Size = new System.Drawing.Size(180, 22);
            this.transactionFromDateTimePicker.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 166);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 14);
            this.label3.TabIndex = 16;
            this.label3.Text = "�ѹ������";
            // 
            // posIdMaskedTextBox
            // 
            this.posIdMaskedTextBox.Location = new System.Drawing.Point(6, 135);
            this.posIdMaskedTextBox.Name = "posIdMaskedTextBox";
            this.posIdMaskedTextBox.Size = new System.Drawing.Size(180, 22);
            this.posIdMaskedTextBox.TabIndex = 2;
            // 
            // cashierIdMaskedTextBox
            // 
            this.cashierIdMaskedTextBox.Location = new System.Drawing.Point(6, 89);
            this.cashierIdMaskedTextBox.Name = "cashierIdMaskedTextBox";
            this.cashierIdMaskedTextBox.Size = new System.Drawing.Size(180, 22);
            this.cashierIdMaskedTextBox.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 117);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 14);
            this.label5.TabIndex = 2;
            this.label5.Text = "��������ͧ�����Թ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 71);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 14);
            this.label7.TabIndex = 2;
            this.label7.Text = "���ʼ������Թ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 14);
            this.label6.TabIndex = 0;
            this.label6.Text = "���� ���.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(7, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(198, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "��§ҹ��è����Թ�����Ӥѭ����";
            // 
            // transactionToDateTimePicker
            // 
            this.transactionToDateTimePicker.CustomFormat = "dd/MM/yyyy";
            this.transactionToDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.transactionToDateTimePicker.Location = new System.Drawing.Point(7, 230);
            this.transactionToDateTimePicker.Name = "transactionToDateTimePicker";
            this.transactionToDateTimePicker.Size = new System.Drawing.Size(180, 22);
            this.transactionToDateTimePicker.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 213);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 14);
            this.label2.TabIndex = 18;
            this.label2.Text = "�֧";
            // 
            // APCriteriaView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(211, 117, 191);
            this.Controls.Add(this.searchPanel);
            this.Name = "APCriteriaView";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(236, 519);
            this.Load += new System.EventHandler(this.APCriteriaView_Load);
            this.searchPanel.ResumeLayout(false);
            this.searchPanel.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel searchPanel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.MaskedTextBox branchIdMaskedTextBox;
        private System.Windows.Forms.DateTimePicker transactionFromDateTimePicker;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MaskedTextBox cashierIdMaskedTextBox;
        private System.Windows.Forms.Button displayReportButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox posIdMaskedTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker transactionToDateTimePicker;
    }
}

