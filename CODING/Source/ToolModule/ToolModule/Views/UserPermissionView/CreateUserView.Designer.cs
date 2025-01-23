namespace PEA.BPM.ToolModule
{
    partial class CreateUserView
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
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.okBt = new System.Windows.Forms.Button();
            this.departmentText = new System.Windows.Forms.TextBox();
            this.userNameText = new System.Windows.Forms.TextBox();
            this.findBt = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.scopeCBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.rePwdText = new System.Windows.Forms.TextBox();
            this.pwdText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.loginNameText = new System.Windows.Forms.TextBox();
            this.cancelBt = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(41, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 16);
            this.label3.TabIndex = 20;
            this.label3.Text = "หน่วยงาน :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 16);
            this.label2.TabIndex = 19;
            this.label2.Text = "ชื่อ - สกุล :";
            // 
            // okBt
            // 
            this.okBt.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.okBt.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okBt.Enabled = false;
            this.okBt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.okBt.Location = new System.Drawing.Point(224, 233);
            this.okBt.Name = "okBt";
            this.okBt.Size = new System.Drawing.Size(87, 25);
            this.okBt.TabIndex = 3;
            this.okBt.Text = "สร้าง";
            this.okBt.UseVisualStyleBackColor = false;
            this.okBt.Click += new System.EventHandler(this.okBt_Click);
            // 
            // departmentText
            // 
            this.departmentText.Location = new System.Drawing.Point(114, 84);
            this.departmentText.Name = "departmentText";
            this.departmentText.ReadOnly = true;
            this.departmentText.Size = new System.Drawing.Size(282, 23);
            this.departmentText.TabIndex = 18;
            // 
            // userNameText
            // 
            this.userNameText.Location = new System.Drawing.Point(114, 56);
            this.userNameText.Name = "userNameText";
            this.userNameText.ReadOnly = true;
            this.userNameText.Size = new System.Drawing.Size(282, 23);
            this.userNameText.TabIndex = 17;
            // 
            // findBt
            // 
            this.findBt.Location = new System.Drawing.Point(365, 27);
            this.findBt.Name = "findBt";
            this.findBt.Size = new System.Drawing.Size(31, 25);
            this.findBt.TabIndex = 16;
            this.findBt.Text = "...";
            this.findBt.UseVisualStyleBackColor = true;
            this.findBt.Click += new System.EventHandler(this.findBt_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label7.Location = new System.Drawing.Point(266, 197);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(122, 16);
            this.label7.TabIndex = 15;
            this.label7.Text = "(ยาวสุด 20 ตัวอักษร)";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.scopeCBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.departmentText);
            this.groupBox1.Controls.Add(this.userNameText);
            this.groupBox1.Controls.Add(this.findBt);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.rePwdText);
            this.groupBox1.Controls.Add(this.pwdText);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.loginNameText);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.groupBox1.Location = new System.Drawing.Point(8, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(412, 220);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ระบุข้อมูลผู้ใช้งานใหม่";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(47, 116);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 16);
            this.label6.TabIndex = 22;
            this.label6.Text = "ขอบเขต :";
            // 
            // scopeCBox
            // 
            this.scopeCBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.scopeCBox.FormattingEnabled = true;
            this.scopeCBox.Items.AddRange(new object[] {
            "การไฟฟ้าสังกัด",
            "เขตการไฟฟ้า",
            "ทุกการไฟฟ้า"});
            this.scopeCBox.Location = new System.Drawing.Point(114, 113);
            this.scopeCBox.Name = "scopeCBox";
            this.scopeCBox.Size = new System.Drawing.Size(282, 24);
            this.scopeCBox.TabIndex = 21;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 174);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 16);
            this.label5.TabIndex = 13;
            this.label5.Text = "รหัสผ่าน (ทวน) :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(47, 146);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 16);
            this.label4.TabIndex = 12;
            this.label4.Text = "รหัสผ่าน :";
            // 
            // rePwdText
            // 
            this.rePwdText.Enabled = false;
            this.rePwdText.Location = new System.Drawing.Point(114, 171);
            this.rePwdText.MaxLength = 20;
            this.rePwdText.Name = "rePwdText";
            this.rePwdText.PasswordChar = '*';
            this.rePwdText.Size = new System.Drawing.Size(282, 23);
            this.rePwdText.TabIndex = 11;
            this.rePwdText.TextChanged += new System.EventHandler(this.rePwdText_TextChanged);
            this.rePwdText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rePwdText_KeyDown);
            this.rePwdText.Enter += new System.EventHandler(this.rePwdText_Enter);
            // 
            // pwdText
            // 
            this.pwdText.Enabled = false;
            this.pwdText.Location = new System.Drawing.Point(114, 143);
            this.pwdText.MaxLength = 20;
            this.pwdText.Name = "pwdText";
            this.pwdText.PasswordChar = '*';
            this.pwdText.Size = new System.Drawing.Size(282, 23);
            this.pwdText.TabIndex = 10;
            this.pwdText.TextChanged += new System.EventHandler(this.pwdText_TextChanged);
            this.pwdText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pwdText_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "รหัสผู้ใช้ :";
            // 
            // loginNameText
            // 
            this.loginNameText.Location = new System.Drawing.Point(114, 29);
            this.loginNameText.MaxLength = 8;
            this.loginNameText.Name = "loginNameText";
            this.loginNameText.Size = new System.Drawing.Size(243, 23);
            this.loginNameText.TabIndex = 0;
            this.loginNameText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.loginNameText_KeyDown);
            // 
            // cancelBt
            // 
            this.cancelBt.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.cancelBt.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cancelBt.Location = new System.Drawing.Point(317, 233);
            this.cancelBt.Name = "cancelBt";
            this.cancelBt.Size = new System.Drawing.Size(87, 25);
            this.cancelBt.TabIndex = 5;
            this.cancelBt.Text = "ยกเลิก";
            this.cancelBt.UseVisualStyleBackColor = false;
            // 
            // CreateUserView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 269);
            this.Controls.Add(this.okBt);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cancelBt);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "CreateUserView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "สร้างผู้ใช้งานใหม่";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button okBt;
        private System.Windows.Forms.TextBox departmentText;
        private System.Windows.Forms.TextBox userNameText;
        private System.Windows.Forms.Button findBt;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox rePwdText;
        private System.Windows.Forms.TextBox pwdText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox loginNameText;
        private System.Windows.Forms.Button cancelBt;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox scopeCBox;
    }
}