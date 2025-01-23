namespace PEA.BPM.Architecture.ArchitectureTool.Security
{
    partial class ChangePwdForm
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
            this.userNameText = new System.Windows.Forms.TextBox();
            this.cancelBt = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.pwdText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rePwdText = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.okBt = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblPw = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblPasswordStrength = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.oldPwdText = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // userNameText
            // 
            this.userNameText.Location = new System.Drawing.Point(124, 33);
            this.userNameText.Name = "userNameText";
            this.userNameText.ReadOnly = true;
            this.userNameText.Size = new System.Drawing.Size(261, 23);
            this.userNameText.TabIndex = 1;
            this.userNameText.TabStop = false;
            // 
            // cancelBt
            // 
            this.cancelBt.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.cancelBt.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cancelBt.Location = new System.Drawing.Point(318, 435);
            this.cancelBt.Name = "cancelBt";
            this.cancelBt.Size = new System.Drawing.Size(87, 25);
            this.cancelBt.TabIndex = 2;
            this.cancelBt.Text = "ยกเลิก";
            this.cancelBt.UseVisualStyleBackColor = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label7.Location = new System.Drawing.Point(296, 96);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 16);
            this.label7.TabIndex = 11;
            this.label7.Text = "(ยาวสุด 20 ตัว)";
            // 
            // pwdText
            // 
            this.pwdText.Location = new System.Drawing.Point(124, 89);
            this.pwdText.MaxLength = 20;
            this.pwdText.Name = "pwdText";
            this.pwdText.PasswordChar = '*';
            this.pwdText.Size = new System.Drawing.Size(166, 23);
            this.pwdText.TabIndex = 10;
            this.pwdText.TextChanged += new System.EventHandler(this.pwdText_TextChanged);
            this.pwdText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pwdText_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(61, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "ชื่อผู้ใช้ :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // rePwdText
            // 
            this.rePwdText.Location = new System.Drawing.Point(124, 120);
            this.rePwdText.MaxLength = 20;
            this.rePwdText.Name = "rePwdText";
            this.rePwdText.PasswordChar = '*';
            this.rePwdText.Size = new System.Drawing.Size(166, 23);
            this.rePwdText.TabIndex = 13;
            this.rePwdText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rePwdText_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 123);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 16);
            this.label5.TabIndex = 12;
            this.label5.Text = "รหัสผ่าน (ทวน) :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // okBt
            // 
            this.okBt.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.okBt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.okBt.Location = new System.Drawing.Point(224, 435);
            this.okBt.Name = "okBt";
            this.okBt.Size = new System.Drawing.Size(87, 25);
            this.okBt.TabIndex = 1;
            this.okBt.Text = "ตกลง";
            this.okBt.UseVisualStyleBackColor = false;
            this.okBt.Click += new System.EventHandler(this.okBt_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.lblPw);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblPasswordStrength);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.oldPwdText);
            this.groupBox1.Controls.Add(this.userNameText);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.rePwdText);
            this.groupBox1.Controls.Add(this.pwdText);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.groupBox1.Location = new System.Drawing.Point(10, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(395, 426);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "แก้ไขรหัสผ่าน";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(13, 174);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(103, 16);
            this.label15.TabIndex = 25;
            this.label15.Text = "ตัวอย่างรหัสผ่าน :";
            this.label15.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(24, 399);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(220, 16);
            this.label13.TabIndex = 24;
            this.label13.Text = "7. ห้ามตั้งรหัสผ่านใหม่ซ้ำกับรหัสผ่านเดิม";
            this.label13.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.Red;
            this.label14.Location = new System.Drawing.Point(24, 378);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(232, 16);
            this.label14.TabIndex = 23;
            this.label14.Text = "6. ห้ามมีชื่อหรือรหัสพนักงานอยู่ในรหัสผ่าน";
            this.label14.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblPw
            // 
            this.lblPw.AutoSize = true;
            this.lblPw.Location = new System.Drawing.Point(24, 357);
            this.lblPw.Name = "lblPw";
            this.lblPw.Size = new System.Drawing.Size(278, 16);
            this.lblPw.TabIndex = 22;
            this.lblPw.Text = "5. รหัสผ่านต้องมีความยาว ตั้งแต่ 10 ตัวอักษรขึ้นไป";
            this.lblPw.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(24, 336);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(282, 16);
            this.label8.TabIndex = 21;
            this.label8.Text = "4. สัญลักษณ์ (Symbols) เช่น ! @ # $ % ^ เป็นต้น";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(24, 315);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(288, 16);
            this.label11.TabIndex = 20;
            this.label11.Text = "3. ตัวเลข (Numbers) คือ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9";
            this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(24, 294);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(341, 16);
            this.label10.TabIndex = 19;
            this.label10.Text = "2. อักษรตัวพิมพ์ใหญ่ (Uppercase letters) เช่น A, B, C เป็นต้น";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(24, 273);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(328, 16);
            this.label9.TabIndex = 18;
            this.label9.Text = "1. อักษรตัวพิมพ์เล็ก (Lowercase letter) เช่น a, b, c เป็นต้น";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(11, 252);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(154, 16);
            this.label3.TabIndex = 16;
            this.label3.Text = "รหัสผ่านต้องประกอบด้วย";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblPasswordStrength
            // 
            this.lblPasswordStrength.AutoSize = true;
            this.lblPasswordStrength.BackColor = System.Drawing.Color.Red;
            this.lblPasswordStrength.ForeColor = System.Drawing.Color.Black;
            this.lblPasswordStrength.Location = new System.Drawing.Point(125, 149);
            this.lblPasswordStrength.Name = "lblPasswordStrength";
            this.lblPasswordStrength.Size = new System.Drawing.Size(40, 16);
            this.lblPasswordStrength.TabIndex = 15;
            this.lblPasswordStrength.Text = "Weak";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 149);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 16);
            this.label2.TabIndex = 14;
            this.label2.Text = "ความปลอดภัย :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 16);
            this.label6.TabIndex = 8;
            this.label6.Text = "รหัสผ่าน (เก่า) :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // oldPwdText
            // 
            this.oldPwdText.Location = new System.Drawing.Point(124, 61);
            this.oldPwdText.MaxLength = 20;
            this.oldPwdText.Name = "oldPwdText";
            this.oldPwdText.PasswordChar = '*';
            this.oldPwdText.Size = new System.Drawing.Size(166, 23);
            this.oldPwdText.TabIndex = 7;
            this.oldPwdText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.oldPwdText_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "รหัสผ่าน (ใหม่) :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(121, 174);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(83, 16);
            this.label12.TabIndex = 26;
            this.label12.Text = "Pe@1234567";
            this.label12.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ChangePwdForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 472);
            this.Controls.Add(this.cancelBt);
            this.Controls.Add(this.okBt);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "ChangePwdForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox userNameText;
        private System.Windows.Forms.Button cancelBt;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox pwdText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox rePwdText;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button okBt;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox oldPwdText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblPasswordStrength;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblPw;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label12;
    }
}