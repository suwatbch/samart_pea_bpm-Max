
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

namespace PEA.BPM.PaymentCollectionModule
{
    partial class CAC18CriteriaView
    {
        /// <summary>
        /// The presenter used by this view.
        /// </summary>
        private PEA.BPM.PaymentCollectionModule.CAC18CriteriaViewPresenter _presenter = null;

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
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.transactionToDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.branchIdMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.transactionFromDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.controllerIdMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.displayReportButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.searchPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchPanel
            // 
            this.searchPanel.BackColor = System.Drawing.Color.White;
            this.searchPanel.Controls.Add(this.label3);
            this.searchPanel.Controls.Add(this.groupBox1);
            this.searchPanel.Controls.Add(this.label1);
            this.searchPanel.Location = new System.Drawing.Point(10, 10);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Size = new System.Drawing.Size(210, 343);
            this.searchPanel.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(7, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "QR Payment";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.transactionToDateTimePicker);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.branchIdMaskedTextBox);
            this.groupBox1.Controls.Add(this.transactionFromDateTimePicker);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.controllerIdMaskedTextBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.displayReportButton);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(10, 48);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(193, 280);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "���͹�";
            // 
            // transactionToDateTimePicker
            // 
            this.transactionToDateTimePicker.CustomFormat = "dd/MM/yyyy";
            this.transactionToDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.transactionToDateTimePicker.Location = new System.Drawing.Point(18, 163);
            this.transactionToDateTimePicker.Name = "transactionToDateTimePicker";
            this.transactionToDateTimePicker.Size = new System.Drawing.Size(109, 20);
            this.transactionToDateTimePicker.TabIndex = 37;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(15, 147);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(19, 13);
            this.label10.TabIndex = 36;
            this.label10.Text = "�֧";
            // 
            // branchIdMaskedTextBox
            // 
            this.branchIdMaskedTextBox.Location = new System.Drawing.Point(18, 37);
            this.branchIdMaskedTextBox.Name = "branchIdMaskedTextBox";
            this.branchIdMaskedTextBox.Size = new System.Drawing.Size(156, 20);
            this.branchIdMaskedTextBox.TabIndex = 1;
            // 
            // transactionFromDateTimePicker
            // 
            this.transactionFromDateTimePicker.CustomFormat = "dd/MM/yyyy";
            this.transactionFromDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.transactionFromDateTimePicker.Location = new System.Drawing.Point(18, 121);
            this.transactionFromDateTimePicker.Name = "transactionFromDateTimePicker";
            this.transactionFromDateTimePicker.Size = new System.Drawing.Size(109, 20);
            this.transactionFromDateTimePicker.TabIndex = 31;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 105);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 13);
            this.label7.TabIndex = 33;
            this.label7.Text = "�ѹ����Ѻ����";
            // 
            // controllerIdMaskedTextBox
            // 
            this.controllerIdMaskedTextBox.Location = new System.Drawing.Point(18, 76);
            this.controllerIdMaskedTextBox.Name = "controllerIdMaskedTextBox";
            this.controllerIdMaskedTextBox.Size = new System.Drawing.Size(156, 20);
            this.controllerIdMaskedTextBox.TabIndex = 3;
            this.controllerIdMaskedTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.branchIdMaskedTextBox_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 30;
            this.label2.Text = "���ʼ���Ѻ�Թ";
            // 
            // displayReportButton
            // 
            this.displayReportButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.displayReportButton.Location = new System.Drawing.Point(18, 194);
            this.displayReportButton.Name = "displayReportButton";
            this.displayReportButton.Size = new System.Drawing.Size(85, 23);
            this.displayReportButton.TabIndex = 4;
            this.displayReportButton.Text = "�ʴ���§ҹ";
            this.displayReportButton.UseVisualStyleBackColor = false;
            this.displayReportButton.Click += new System.EventHandler(this.displayReportButton_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "���� ���.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(7, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "��§ҹ����Ѻ�����Թ���� ";
            // 
            // CAC18CriteriaView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(117)))), ((int)(((byte)(191)))));
            this.Controls.Add(this.searchPanel);
            this.Name = "CAC18CriteriaView";
            this.Size = new System.Drawing.Size(230, 519);
            this.searchPanel.ResumeLayout(false);
            this.searchPanel.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel searchPanel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button displayReportButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox controllerIdMaskedTextBox;
        private System.Windows.Forms.DateTimePicker transactionFromDateTimePicker;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.MaskedTextBox branchIdMaskedTextBox;
        private System.Windows.Forms.DateTimePicker transactionToDateTimePicker;
        private System.Windows.Forms.Label label10;
    }
}

