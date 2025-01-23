namespace PEA.BPM.CashManagementModule
{
    partial class ConfirmBankDelivering
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.separatedPayInCheck = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.sameBranchLb = new System.Windows.Forms.Label();
            this.sameBankLb = new System.Windows.Forms.Label();
            this.otherBankLb = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.totalAmtTxt = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.b2 = new System.Windows.Forms.Label();
            this.chqAnoBankTxt = new System.Windows.Forms.TextBox();
            this.b1 = new System.Windows.Forms.Label();
            this.chqSameBankTxt = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.chqSameBranchTxt = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cashAmtTxt = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.c2 = new System.Windows.Forms.Label();
            this.c1 = new System.Windows.Forms.Label();
            this.c0 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cancelBt = new System.Windows.Forms.Button();
            this.printBt = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(211, 117, 191);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(460, 244);
            this.panel1.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(211, 117, 191);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(460, 244);
            this.panel2.TabIndex = 5;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.separatedPayInCheck);
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Controls.Add(this.cancelBt);
            this.panel3.Controls.Add(this.printBt);
            this.panel3.Location = new System.Drawing.Point(8, 8);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(446, 228);
            this.panel3.TabIndex = 5;
            // 
            // separatedPayInCheck
            // 
            this.separatedPayInCheck.AutoSize = true;
            this.separatedPayInCheck.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.separatedPayInCheck.Location = new System.Drawing.Point(13, 190);
            this.separatedPayInCheck.Name = "separatedPayInCheck";
            this.separatedPayInCheck.Size = new System.Drawing.Size(131, 20);
            this.separatedPayInCheck.TabIndex = 4;
            this.separatedPayInCheck.Text = "นำฝากแบบแยกเช็ค";
            this.separatedPayInCheck.UseVisualStyleBackColor = true;
            this.separatedPayInCheck.CheckedChanged += new System.EventHandler(this.printAllCheck_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.sameBranchLb);
            this.groupBox1.Controls.Add(this.sameBankLb);
            this.groupBox1.Controls.Add(this.otherBankLb);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.totalAmtTxt);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.b2);
            this.groupBox1.Controls.Add(this.chqAnoBankTxt);
            this.groupBox1.Controls.Add(this.b1);
            this.groupBox1.Controls.Add(this.chqSameBankTxt);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.chqSameBranchTxt);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cashAmtTxt);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.c2);
            this.groupBox1.Controls.Add(this.c1);
            this.groupBox1.Controls.Add(this.c0);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(9, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(429, 177);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "สรุปรายการนำส่งธนาคาร";
            // 
            // sameBranchLb
            // 
            this.sameBranchLb.AutoSize = true;
            this.sameBranchLb.Location = new System.Drawing.Point(199, 58);
            this.sameBranchLb.Name = "sameBranchLb";
            this.sameBranchLb.Size = new System.Drawing.Size(42, 16);
            this.sameBranchLb.TabIndex = 21;
            this.sameBranchLb.Text = "label7";
            // 
            // sameBankLb
            // 
            this.sameBankLb.AutoSize = true;
            this.sameBankLb.Location = new System.Drawing.Point(199, 86);
            this.sameBankLb.Name = "sameBankLb";
            this.sameBankLb.Size = new System.Drawing.Size(42, 16);
            this.sameBankLb.TabIndex = 20;
            this.sameBankLb.Text = "label7";
            // 
            // otherBankLb
            // 
            this.otherBankLb.AutoSize = true;
            this.otherBankLb.Location = new System.Drawing.Point(136, 114);
            this.otherBankLb.Name = "otherBankLb";
            this.otherBankLb.Size = new System.Drawing.Size(42, 16);
            this.otherBankLb.TabIndex = 19;
            this.otherBankLb.Text = "label7";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(385, 144);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(33, 16);
            this.label14.TabIndex = 18;
            this.label14.Text = "บาท";
            // 
            // totalAmtTxt
            // 
            this.totalAmtTxt.BackColor = System.Drawing.SystemColors.Window;
            this.totalAmtTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.totalAmtTxt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalAmtTxt.ForeColor = System.Drawing.Color.Red;
            this.totalAmtTxt.Location = new System.Drawing.Point(285, 145);
            this.totalAmtTxt.Name = "totalAmtTxt";
            this.totalAmtTxt.ReadOnly = true;
            this.totalAmtTxt.Size = new System.Drawing.Size(90, 16);
            this.totalAmtTxt.TabIndex = 17;
            this.totalAmtTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(165, 143);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(83, 16);
            this.label13.TabIndex = 16;
            this.label13.Text = "รวมทั้งหมด :";
            // 
            // b2
            // 
            this.b2.AutoSize = true;
            this.b2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.b2.ForeColor = System.Drawing.Color.Black;
            this.b2.Location = new System.Drawing.Point(385, 115);
            this.b2.Name = "b2";
            this.b2.Size = new System.Drawing.Size(31, 16);
            this.b2.TabIndex = 15;
            this.b2.Text = "บาท";
            // 
            // chqAnoBankTxt
            // 
            this.chqAnoBankTxt.BackColor = System.Drawing.SystemColors.Window;
            this.chqAnoBankTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chqAnoBankTxt.ForeColor = System.Drawing.Color.Red;
            this.chqAnoBankTxt.Location = new System.Drawing.Point(285, 116);
            this.chqAnoBankTxt.Name = "chqAnoBankTxt";
            this.chqAnoBankTxt.ReadOnly = true;
            this.chqAnoBankTxt.Size = new System.Drawing.Size(90, 16);
            this.chqAnoBankTxt.TabIndex = 14;
            this.chqAnoBankTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // b1
            // 
            this.b1.AutoSize = true;
            this.b1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.b1.ForeColor = System.Drawing.Color.Black;
            this.b1.Location = new System.Drawing.Point(385, 87);
            this.b1.Name = "b1";
            this.b1.Size = new System.Drawing.Size(31, 16);
            this.b1.TabIndex = 13;
            this.b1.Text = "บาท";
            // 
            // chqSameBankTxt
            // 
            this.chqSameBankTxt.BackColor = System.Drawing.SystemColors.Window;
            this.chqSameBankTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chqSameBankTxt.ForeColor = System.Drawing.Color.Red;
            this.chqSameBankTxt.Location = new System.Drawing.Point(285, 88);
            this.chqSameBankTxt.Name = "chqSameBankTxt";
            this.chqSameBankTxt.ReadOnly = true;
            this.chqSameBankTxt.Size = new System.Drawing.Size(90, 16);
            this.chqSameBankTxt.TabIndex = 12;
            this.chqSameBankTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(385, 59);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(31, 16);
            this.label10.TabIndex = 11;
            this.label10.Text = "บาท";
            // 
            // chqSameBranchTxt
            // 
            this.chqSameBranchTxt.BackColor = System.Drawing.SystemColors.Window;
            this.chqSameBranchTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chqSameBranchTxt.ForeColor = System.Drawing.Color.Red;
            this.chqSameBranchTxt.Location = new System.Drawing.Point(285, 60);
            this.chqSameBranchTxt.Name = "chqSameBranchTxt";
            this.chqSameBranchTxt.ReadOnly = true;
            this.chqSameBranchTxt.Size = new System.Drawing.Size(90, 16);
            this.chqSameBranchTxt.TabIndex = 10;
            this.chqSameBranchTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(385, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 16);
            this.label6.TabIndex = 6;
            this.label6.Text = "บาท";
            // 
            // cashAmtTxt
            // 
            this.cashAmtTxt.BackColor = System.Drawing.SystemColors.Window;
            this.cashAmtTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.cashAmtTxt.ForeColor = System.Drawing.Color.Red;
            this.cashAmtTxt.Location = new System.Drawing.Point(285, 32);
            this.cashAmtTxt.Name = "cashAmtTxt";
            this.cashAmtTxt.ReadOnly = true;
            this.cashAmtTxt.Size = new System.Drawing.Size(90, 16);
            this.cashAmtTxt.TabIndex = 5;
            this.cashAmtTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(58, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(212, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = ".                                                 .";
            // 
            // c2
            // 
            this.c2.AutoSize = true;
            this.c2.ForeColor = System.Drawing.Color.Black;
            this.c2.Location = new System.Drawing.Point(14, 114);
            this.c2.Name = "c2";
            this.c2.Size = new System.Drawing.Size(93, 16);
            this.c2.TabIndex = 3;
            this.c2.Text = "เช็ค ต่างธนาคาร";
            // 
            // c1
            // 
            this.c1.AutoSize = true;
            this.c1.ForeColor = System.Drawing.Color.Black;
            this.c1.Location = new System.Drawing.Point(14, 86);
            this.c1.Name = "c1";
            this.c1.Size = new System.Drawing.Size(164, 16);
            this.c1.TabIndex = 2;
            this.c1.Text = "เช็ค ธนาคารต่างสาขากับบัญชี";
            // 
            // c0
            // 
            this.c0.AutoSize = true;
            this.c0.ForeColor = System.Drawing.Color.Black;
            this.c0.Location = new System.Drawing.Point(14, 58);
            this.c0.Name = "c0";
            this.c0.Size = new System.Drawing.Size(170, 16);
            this.c0.TabIndex = 1;
            this.c0.Text = "เช็ค ธนาคารสาขาเดียวกับบัญชี";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(14, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "เงินสด";
            // 
            // cancelBt
            // 
            this.cancelBt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelBt.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.cancelBt.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelBt.Location = new System.Drawing.Point(339, 189);
            this.cancelBt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cancelBt.Name = "cancelBt";
            this.cancelBt.Size = new System.Drawing.Size(98, 29);
            this.cancelBt.TabIndex = 3;
            this.cancelBt.Text = "ยกเลิก";
            this.cancelBt.UseVisualStyleBackColor = false;
            // 
            // printBt
            // 
            this.printBt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.printBt.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.printBt.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.printBt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.printBt.Location = new System.Drawing.Point(231, 189);
            this.printBt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.printBt.Name = "printBt";
            this.printBt.Size = new System.Drawing.Size(102, 29);
            this.printBt.TabIndex = 1;
            this.printBt.Text = "ตกลง";
            this.printBt.UseVisualStyleBackColor = false;
            this.printBt.Click += new System.EventHandler(this.printBt_Click);
            // 
            // ConfirmBankDelivering
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 244);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ConfirmBankDelivering";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ยืนยันการนำเงินฝากธนาคาร";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label c2;
        private System.Windows.Forms.Label c1;
        private System.Windows.Forms.Label c0;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cancelBt;
        private System.Windows.Forms.Button printBt;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox cashAmtTxt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox totalAmtTxt;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label b2;
        private System.Windows.Forms.TextBox chqAnoBankTxt;
        private System.Windows.Forms.Label b1;
        private System.Windows.Forms.TextBox chqSameBankTxt;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox chqSameBranchTxt;
        private System.Windows.Forms.Label sameBranchLb;
        private System.Windows.Forms.Label sameBankLb;
        private System.Windows.Forms.Label otherBankLb;
        private System.Windows.Forms.CheckBox separatedPayInCheck;
    }
}