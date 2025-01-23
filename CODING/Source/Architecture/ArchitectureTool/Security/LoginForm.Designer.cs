namespace PEA.BPM.Architecture.ArchitectureTool.Security
{
    partial class LoginForm
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
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.pwdText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.userIdText = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RePw = new System.Windows.Forms.Label();
            this.BpmGuide = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cancelButton.Location = new System.Drawing.Point(161, 87);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 25);
            this.cancelButton.TabIndex = 18;
            this.cancelButton.Text = "ยกเลิก";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.okButton.Location = new System.Drawing.Point(80, 87);
            this.okButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 25);
            this.okButton.TabIndex = 17;
            this.okButton.Text = "ตกลง";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(40, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 16);
            this.label2.TabIndex = 15;
            this.label2.Text = "รหัสผ่าน";
            // 
            // pwdText
            // 
            this.pwdText.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.pwdText.Location = new System.Drawing.Point(100, 47);
            this.pwdText.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pwdText.Name = "pwdText";
            this.pwdText.PasswordChar = '*';
            this.pwdText.Size = new System.Drawing.Size(136, 23);
            this.pwdText.TabIndex = 16;
            this.pwdText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pwdText_KeyDown);
            this.pwdText.Enter += new System.EventHandler(this.pwdText_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(16, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 16);
            this.label1.TabIndex = 13;
            this.label1.Text = "เลขประจำตัว";
            // 
            // userIdText
            // 
            this.userIdText.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.userIdText.Location = new System.Drawing.Point(100, 15);
            this.userIdText.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.userIdText.Name = "userIdText";
            this.userIdText.Size = new System.Drawing.Size(136, 23);
            this.userIdText.TabIndex = 14;
            this.userIdText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.userIdText_KeyDown);
            this.userIdText.Enter += new System.EventHandler(this.userIdText_Enter);
            // 
            // groupBox1
            // 
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(6, -2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(242, 82);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            // 
            // RePw
            // 
            this.RePw.AutoSize = true;
            this.RePw.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RePw.ForeColor = System.Drawing.Color.Blue;
            this.RePw.Location = new System.Drawing.Point(3, 131);
            this.RePw.Name = "RePw";
            this.RePw.Size = new System.Drawing.Size(68, 16);
            this.RePw.TabIndex = 20;
            this.RePw.Text = "ลืมรหัสผ่าน";
            this.RePw.Click += new System.EventHandler(this.RePw_Click);
            // 
            // BpmGuide
            // 
            this.BpmGuide.AutoSize = true;
            this.BpmGuide.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BpmGuide.ForeColor = System.Drawing.Color.Blue;
            this.BpmGuide.Location = new System.Drawing.Point(188, 131);
            this.BpmGuide.Name = "BpmGuide";
            this.BpmGuide.Size = new System.Drawing.Size(60, 16);
            this.BpmGuide.TabIndex = 21;
            this.BpmGuide.Text = "คู่มือ BPM";
            this.BpmGuide.Click += new System.EventHandler(this.BpmGuide_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 152);
            this.Controls.Add(this.BpmGuide);
            this.Controls.Add(this.RePw);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pwdText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.userIdText);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = " ล็อกอิน";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox pwdText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox userIdText;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label RePw;
        private System.Windows.Forms.Label BpmGuide;
    }
}