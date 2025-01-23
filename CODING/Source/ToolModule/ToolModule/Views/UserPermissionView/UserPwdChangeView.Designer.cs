namespace PEA.BPM.ToolModule
{
    partial class UserPwdChangeView
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
            this.okBt = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.userNameText = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.rePwdText = new System.Windows.Forms.TextBox();
            this.pwdText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cancelBt = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // okBt
            // 
            this.okBt.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.okBt.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okBt.Enabled = false;
            this.okBt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.okBt.Location = new System.Drawing.Point(149, 151);
            this.okBt.Name = "okBt";
            this.okBt.Size = new System.Drawing.Size(87, 25);
            this.okBt.TabIndex = 4;
            this.okBt.Text = "ตกลง";
            this.okBt.UseVisualStyleBackColor = false;
            this.okBt.Click += new System.EventHandler(this.okBt_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.userNameText);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.rePwdText);
            this.groupBox1.Controls.Add(this.pwdText);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.groupBox1.Location = new System.Drawing.Point(6, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(334, 141);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // userNameText
            // 
            this.userNameText.Location = new System.Drawing.Point(106, 19);
            this.userNameText.Name = "userNameText";
            this.userNameText.ReadOnly = true;
            this.userNameText.Size = new System.Drawing.Size(217, 23);
            this.userNameText.TabIndex = 1;
            this.userNameText.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label7.Location = new System.Drawing.Point(114, 103);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(126, 16);
            this.label7.TabIndex = 11;
            this.label7.Text = "(ยาวสุด. 20 ตัวอักษร)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 16);
            this.label5.TabIndex = 12;
            this.label5.Text = "รหัสผ่าน (ทวน) :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "รหัสผ่าน (ใหม่) :";
            // 
            // rePwdText
            // 
            this.rePwdText.Location = new System.Drawing.Point(106, 79);
            this.rePwdText.MaxLength = 20;
            this.rePwdText.Name = "rePwdText";
            this.rePwdText.PasswordChar = '*';
            this.rePwdText.Size = new System.Drawing.Size(217, 23);
            this.rePwdText.TabIndex = 13;
            this.rePwdText.TextChanged += new System.EventHandler(this.rePwdText_TextChanged);
            this.rePwdText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rePwdText_KeyDown);
            // 
            // pwdText
            // 
            this.pwdText.Location = new System.Drawing.Point(106, 48);
            this.pwdText.MaxLength = 20;
            this.pwdText.Name = "pwdText";
            this.pwdText.PasswordChar = '*';
            this.pwdText.Size = new System.Drawing.Size(217, 23);
            this.pwdText.TabIndex = 10;
            this.pwdText.TextChanged += new System.EventHandler(this.pwdText_TextChanged);
            this.pwdText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pwdText_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "ชื่อผู้ใช้ :";
            // 
            // cancelBt
            // 
            this.cancelBt.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.cancelBt.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cancelBt.Location = new System.Drawing.Point(242, 151);
            this.cancelBt.Name = "cancelBt";
            this.cancelBt.Size = new System.Drawing.Size(87, 25);
            this.cancelBt.TabIndex = 5;
            this.cancelBt.Text = "ยกเลิก";
            this.cancelBt.UseVisualStyleBackColor = false;
            // 
            // UserPwdChangeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 185);
            this.Controls.Add(this.okBt);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cancelBt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "UserPwdChangeView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ตั้งค่ารหัสผ่านใหม่";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button okBt;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox userNameText;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox rePwdText;
        private System.Windows.Forms.TextBox pwdText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cancelBt;
    }
}