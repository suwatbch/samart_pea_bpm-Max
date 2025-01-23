namespace PEA.BPM.ToolModule
{
    partial class EditUserView
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.branchTxt = new System.Windows.Forms.TextBox();
            this.departmentText = new System.Windows.Forms.TextBox();
            this.userNameText = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.rePwdText = new System.Windows.Forms.TextBox();
            this.pwdText = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.scopeCBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.okBt = new System.Windows.Forms.Button();
            this.cancelBt = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.branchTxt);
            this.groupBox1.Controls.Add(this.departmentText);
            this.groupBox1.Controls.Add(this.userNameText);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.rePwdText);
            this.groupBox1.Controls.Add(this.pwdText);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.scopeCBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.groupBox1.Location = new System.Drawing.Point(6, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(431, 213);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 77);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 16);
            this.label6.TabIndex = 22;
            this.label6.Text = "การไฟฟ้าสังกัด :";
            // 
            // branchTxt
            // 
            this.branchTxt.Location = new System.Drawing.Point(114, 74);
            this.branchTxt.Name = "branchTxt";
            this.branchTxt.ReadOnly = true;
            this.branchTxt.Size = new System.Drawing.Size(305, 23);
            this.branchTxt.TabIndex = 21;
            // 
            // departmentText
            // 
            this.departmentText.Location = new System.Drawing.Point(114, 45);
            this.departmentText.Name = "departmentText";
            this.departmentText.ReadOnly = true;
            this.departmentText.Size = new System.Drawing.Size(305, 23);
            this.departmentText.TabIndex = 20;
            // 
            // userNameText
            // 
            this.userNameText.Location = new System.Drawing.Point(114, 16);
            this.userNameText.Name = "userNameText";
            this.userNameText.ReadOnly = true;
            this.userNameText.Size = new System.Drawing.Size(305, 23);
            this.userNameText.TabIndex = 19;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label7.Location = new System.Drawing.Point(115, 191);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(126, 16);
            this.label7.TabIndex = 16;
            this.label7.Text = "(ยาวสุด. 20 ตัวอักษร)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 168);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 16);
            this.label5.TabIndex = 17;
            this.label5.Text = "รหัสผ่าน (ทวน) :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(47, 137);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 16);
            this.label2.TabIndex = 14;
            this.label2.Text = "รหัสผ่าน :";
            // 
            // rePwdText
            // 
            this.rePwdText.Location = new System.Drawing.Point(114, 163);
            this.rePwdText.MaxLength = 20;
            this.rePwdText.Name = "rePwdText";
            this.rePwdText.Size = new System.Drawing.Size(305, 23);
            this.rePwdText.TabIndex = 18;
            this.rePwdText.TextChanged += new System.EventHandler(this.rePwdText_TextChanged);
            this.rePwdText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rePwdText_KeyDown);
            this.rePwdText.Leave += new System.EventHandler(this.rePwdText_Leave);
            this.rePwdText.Enter += new System.EventHandler(this.rePwdText_Enter);
            // 
            // pwdText
            // 
            this.pwdText.Location = new System.Drawing.Point(114, 134);
            this.pwdText.MaxLength = 20;
            this.pwdText.Name = "pwdText";
            this.pwdText.Size = new System.Drawing.Size(305, 23);
            this.pwdText.TabIndex = 15;
            this.pwdText.TextChanged += new System.EventHandler(this.pwdText_TextChanged);
            this.pwdText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pwdText_KeyDown);
            this.pwdText.Leave += new System.EventHandler(this.pwdText_Leave);
            this.pwdText.Enter += new System.EventHandler(this.pwdText_Enter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(47, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "ขอบเขต :";
            // 
            // scopeCBox
            // 
            this.scopeCBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.scopeCBox.FormattingEnabled = true;
            this.scopeCBox.Items.AddRange(new object[] {
            "การไฟฟ้าสังกัด",
            "เขตการไฟฟ้า",
            "ทุกการไฟฟ้า"});
            this.scopeCBox.Location = new System.Drawing.Point(114, 104);
            this.scopeCBox.Name = "scopeCBox";
            this.scopeCBox.Size = new System.Drawing.Size(305, 24);
            this.scopeCBox.TabIndex = 6;
            this.scopeCBox.SelectedIndexChanged += new System.EventHandler(this.scopeCBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(41, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "หน่วยงาน :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(53, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "ชื่อผู้ใช้ :";
            // 
            // okBt
            // 
            this.okBt.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.okBt.Enabled = false;
            this.okBt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.okBt.Location = new System.Drawing.Point(234, 221);
            this.okBt.Name = "okBt";
            this.okBt.Size = new System.Drawing.Size(87, 25);
            this.okBt.TabIndex = 4;
            this.okBt.Text = "ตกลง";
            this.okBt.UseVisualStyleBackColor = false;
            this.okBt.Click += new System.EventHandler(this.okBt_Click);
            // 
            // cancelBt
            // 
            this.cancelBt.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.cancelBt.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cancelBt.Location = new System.Drawing.Point(327, 221);
            this.cancelBt.Name = "cancelBt";
            this.cancelBt.Size = new System.Drawing.Size(87, 25);
            this.cancelBt.TabIndex = 5;
            this.cancelBt.Text = "ยกเลิก";
            this.cancelBt.UseVisualStyleBackColor = false;
            // 
            // EditUserView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 256);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.okBt);
            this.Controls.Add(this.cancelBt);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "EditUserView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "แก้ไขข้อมูลผู้ใช้งาน";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button okBt;
        private System.Windows.Forms.Button cancelBt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox scopeCBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox rePwdText;
        private System.Windows.Forms.TextBox pwdText;
        private System.Windows.Forms.TextBox userNameText;
        private System.Windows.Forms.TextBox departmentText;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox branchTxt;
    }
}