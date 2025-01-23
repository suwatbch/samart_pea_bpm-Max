namespace PEA.BPM.PaymentCollectionModule.ReportViews.CAC19CriteriaView
{
    partial class CAC19CriteriaView
    {

        /// <summary>
        /// The presenter used by this view.
        /// </summary>
        private PEA.BPM.PaymentCollectionModule.CAC19CriteriaViewPresenter _presenter = null;

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
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblWarning = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.selBankTextFilebutton = new System.Windows.Forms.Button();
            this.fullPathFileNameTextBox = new System.Windows.Forms.TextBox();
            this.branchInfotextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.paymentDateDtPicker = new System.Windows.Forms.DateTimePicker();
            this.previewBt = new System.Windows.Forms.Button();
            this.cancelBt = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.lblWarning);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.previewBt);
            this.panel2.Controls.Add(this.cancelBt);
            this.panel2.Location = new System.Drawing.Point(7, 7);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(542, 222);
            this.panel2.TabIndex = 1;
            // 
            // lblWarning
            // 
            this.lblWarning.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWarning.ForeColor = System.Drawing.Color.Red;
            this.lblWarning.Location = new System.Drawing.Point(9, 190);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(333, 19);
            this.lblWarning.TabIndex = 27;
            this.lblWarning.Text = "รายงานนี้ใช้สำหรับตรวจสอบข้อมูลในวันปัจจุบันเท่านั้น";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblStatus);
            this.groupBox1.Controls.Add(this.selBankTextFilebutton);
            this.groupBox1.Controls.Add(this.fullPathFileNameTextBox);
            this.groupBox1.Controls.Add(this.branchInfotextBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.paymentDateDtPicker);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(517, 164);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ตรวจสอบรายการรับชำระเงินด้วย QR Payment เทียบกับรายงานของธนาคาร";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(18, 134);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(474, 19);
            this.lblStatus.TabIndex = 26;
            // 
            // selBankTextFilebutton
            // 
            this.selBankTextFilebutton.Location = new System.Drawing.Point(464, 97);
            this.selBankTextFilebutton.Name = "selBankTextFilebutton";
            this.selBankTextFilebutton.Size = new System.Drawing.Size(28, 23);
            this.selBankTextFilebutton.TabIndex = 25;
            this.selBankTextFilebutton.Text = "...";
            this.selBankTextFilebutton.UseVisualStyleBackColor = true;
            this.selBankTextFilebutton.Click += new System.EventHandler(this.selBankTextFilebutton_Click);
            // 
            // fullPathFileNameTextBox
            // 
            this.fullPathFileNameTextBox.Location = new System.Drawing.Point(136, 97);
            this.fullPathFileNameTextBox.Name = "fullPathFileNameTextBox";
            this.fullPathFileNameTextBox.ReadOnly = true;
            this.fullPathFileNameTextBox.Size = new System.Drawing.Size(322, 23);
            this.fullPathFileNameTextBox.TabIndex = 24;
            // 
            // branchInfotextBox
            // 
            this.branchInfotextBox.Location = new System.Drawing.Point(136, 28);
            this.branchInfotextBox.Name = "branchInfotextBox";
            this.branchInfotextBox.ReadOnly = true;
            this.branchInfotextBox.Size = new System.Drawing.Size(356, 23);
            this.branchInfotextBox.TabIndex = 23;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(18, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 16);
            this.label3.TabIndex = 22;
            this.label3.Text = "ไฟล์จากธนาคาร :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(18, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 16);
            this.label2.TabIndex = 21;
            this.label2.Text = "วันที่ :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 16);
            this.label1.TabIndex = 19;
            this.label1.Text = "สาขา :";
            // 
            // paymentDateDtPicker
            // 
            this.paymentDateDtPicker.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.paymentDateDtPicker.CustomFormat = "dd/MM/yyyy";
            this.paymentDateDtPicker.Enabled = false;
            this.paymentDateDtPicker.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paymentDateDtPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.paymentDateDtPicker.Location = new System.Drawing.Point(136, 59);
            this.paymentDateDtPicker.Name = "paymentDateDtPicker";
            this.paymentDateDtPicker.Size = new System.Drawing.Size(154, 23);
            this.paymentDateDtPicker.TabIndex = 20;
            // 
            // previewBt
            // 
            this.previewBt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.previewBt.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.previewBt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.previewBt.Location = new System.Drawing.Point(350, 186);
            this.previewBt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.previewBt.Name = "previewBt";
            this.previewBt.Size = new System.Drawing.Size(88, 28);
            this.previewBt.TabIndex = 5;
            this.previewBt.Text = "ตกลง";
            this.previewBt.UseVisualStyleBackColor = false;
            this.previewBt.Click += new System.EventHandler(this.previewBt_Click);
            // 
            // cancelBt
            // 
            this.cancelBt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelBt.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.cancelBt.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelBt.Location = new System.Drawing.Point(444, 186);
            this.cancelBt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cancelBt.Name = "cancelBt";
            this.cancelBt.Size = new System.Drawing.Size(88, 28);
            this.cancelBt.TabIndex = 3;
            this.cancelBt.Text = "ยกเลิก";
            this.cancelBt.UseVisualStyleBackColor = false;
            this.cancelBt.Click += new System.EventHandler(this.cancelBt_Click);
            // 
            // CAC19CriteriaView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(117)))), ((int)(((byte)(191)))));
            this.Controls.Add(this.panel2);
            this.Name = "CAC19CriteriaView";
            this.Size = new System.Drawing.Size(557, 238);
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker paymentDateDtPicker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button previewBt;
        private System.Windows.Forms.Button cancelBt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox fullPathFileNameTextBox;
        private System.Windows.Forms.TextBox branchInfotextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button selBankTextFilebutton;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblWarning;
    }
}
