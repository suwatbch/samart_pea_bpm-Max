namespace PEA.BPM.CashManagementModule
{
    partial class ConfirmCashierTransfering
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
            this.label4 = new System.Windows.Forms.Label();
            this.cancelBt = new System.Windows.Forms.Button();
            this.okBt = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.NoOfChqTxt = new System.Windows.Forms.Label();
            this.receiverTxt = new System.Windows.Forms.Label();
            this.senderTxt = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cashAmtTxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chqAmtTxt = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.totalAmtTxt = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.printNoteCheckBox = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "จำนวนเงินในเช็ค";
            // 
            // cancelBt
            // 
            this.cancelBt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelBt.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.cancelBt.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelBt.Location = new System.Drawing.Point(275, 181);
            this.cancelBt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cancelBt.Name = "cancelBt";
            this.cancelBt.Size = new System.Drawing.Size(84, 29);
            this.cancelBt.TabIndex = 2;
            this.cancelBt.Text = "ยกเลิก";
            this.cancelBt.UseVisualStyleBackColor = false;
            // 
            // okBt
            // 
            this.okBt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okBt.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.okBt.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okBt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okBt.Location = new System.Drawing.Point(185, 181);
            this.okBt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.okBt.Name = "okBt";
            this.okBt.Size = new System.Drawing.Size(84, 29);
            this.okBt.TabIndex = 1;
            this.okBt.Text = "ตกลง";
            this.okBt.UseVisualStyleBackColor = false;
            this.okBt.Click += new System.EventHandler(this.okBt_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.NoOfChqTxt);
            this.groupBox1.Controls.Add(this.receiverTxt);
            this.groupBox1.Controls.Add(this.senderTxt);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cashAmtTxt);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.chqAmtTxt);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.totalAmtTxt);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(9, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(354, 171);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "สรุปรายการนำส่งธนาคาร";
            // 
            // NoOfChqTxt
            // 
            this.NoOfChqTxt.AutoSize = true;
            this.NoOfChqTxt.Location = new System.Drawing.Point(114, 115);
            this.NoOfChqTxt.Name = "NoOfChqTxt";
            this.NoOfChqTxt.Size = new System.Drawing.Size(42, 16);
            this.NoOfChqTxt.TabIndex = 30;
            this.NoOfChqTxt.Text = "label7";
            // 
            // receiverTxt
            // 
            this.receiverTxt.AutoSize = true;
            this.receiverTxt.ForeColor = System.Drawing.Color.Black;
            this.receiverTxt.Location = new System.Drawing.Point(13, 51);
            this.receiverTxt.Name = "receiverTxt";
            this.receiverTxt.Size = new System.Drawing.Size(227, 16);
            this.receiverTxt.TabIndex = 29;
            this.receiverTxt.Text = "<094585 - นาย เกรียงศักด์ มูลทองจาด>";
            // 
            // senderTxt
            // 
            this.senderTxt.AutoSize = true;
            this.senderTxt.ForeColor = System.Drawing.Color.Black;
            this.senderTxt.Location = new System.Drawing.Point(13, 24);
            this.senderTxt.Name = "senderTxt";
            this.senderTxt.Size = new System.Drawing.Size(214, 16);
            this.senderTxt.TabIndex = 28;
            this.senderTxt.Text = "<094585 - นาย กนกวัฒน์ กสิกรไทย>";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(92, 83);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(116, 16);
            this.label7.TabIndex = 27;
            this.label7.Text = ".                         .";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(310, 83);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 16);
            this.label6.TabIndex = 26;
            this.label6.Text = "บาท";
            // 
            // cashAmtTxt
            // 
            this.cashAmtTxt.BackColor = System.Drawing.SystemColors.Window;
            this.cashAmtTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.cashAmtTxt.ForeColor = System.Drawing.Color.Red;
            this.cashAmtTxt.Location = new System.Drawing.Point(210, 84);
            this.cashAmtTxt.Name = "cashAmtTxt";
            this.cashAmtTxt.ReadOnly = true;
            this.cashAmtTxt.Size = new System.Drawing.Size(90, 16);
            this.cashAmtTxt.TabIndex = 25;
            this.cashAmtTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(310, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 16);
            this.label1.TabIndex = 23;
            this.label1.Text = "บาท";
            // 
            // chqAmtTxt
            // 
            this.chqAmtTxt.BackColor = System.Drawing.SystemColors.Window;
            this.chqAmtTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chqAmtTxt.ForeColor = System.Drawing.Color.Red;
            this.chqAmtTxt.Location = new System.Drawing.Point(210, 113);
            this.chqAmtTxt.Name = "chqAmtTxt";
            this.chqAmtTxt.ReadOnly = true;
            this.chqAmtTxt.Size = new System.Drawing.Size(90, 16);
            this.chqAmtTxt.TabIndex = 22;
            this.chqAmtTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(310, 141);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(33, 16);
            this.label14.TabIndex = 21;
            this.label14.Text = "บาท";
            // 
            // totalAmtTxt
            // 
            this.totalAmtTxt.BackColor = System.Drawing.SystemColors.Window;
            this.totalAmtTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.totalAmtTxt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalAmtTxt.ForeColor = System.Drawing.Color.Red;
            this.totalAmtTxt.Location = new System.Drawing.Point(210, 142);
            this.totalAmtTxt.Name = "totalAmtTxt";
            this.totalAmtTxt.ReadOnly = true;
            this.totalAmtTxt.Size = new System.Drawing.Size(90, 16);
            this.totalAmtTxt.TabIndex = 20;
            this.totalAmtTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(121, 141);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(83, 16);
            this.label13.TabIndex = 19;
            this.label13.Text = "รวมทั้งหมด :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "จำนวนเงินสด";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.printNoteCheckBox);
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Controls.Add(this.cancelBt);
            this.panel3.Controls.Add(this.okBt);
            this.panel3.Location = new System.Drawing.Point(6, 9);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(370, 217);
            this.panel3.TabIndex = 5;
            // 
            // printNoteCheckBox
            // 
            this.printNoteCheckBox.AutoSize = true;
            this.printNoteCheckBox.Checked = true;
            this.printNoteCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.printNoteCheckBox.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.printNoteCheckBox.Location = new System.Drawing.Point(17, 181);
            this.printNoteCheckBox.Name = "printNoteCheckBox";
            this.printNoteCheckBox.Size = new System.Drawing.Size(99, 20);
            this.printNoteCheckBox.TabIndex = 4;
            this.printNoteCheckBox.Text = "พิมพ์สลิปโอน";
            this.printNoteCheckBox.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(211, 117, 191);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(381, 231);
            this.panel1.TabIndex = 5;
            // 
            // ConfirmCashierTransfering
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 231);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ConfirmCashierTransfering";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ยืนยันการโอนเงินระหว่างแคชเชียร์";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button cancelBt;
        private System.Windows.Forms.Button okBt;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox chqAmtTxt;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox totalAmtTxt;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox cashAmtTxt;
        private System.Windows.Forms.Label senderTxt;
        private System.Windows.Forms.Label receiverTxt;
        private System.Windows.Forms.Label NoOfChqTxt;
        private System.Windows.Forms.CheckBox printNoteCheckBox;
    }
}