using System.Windows.Forms;
namespace PEA.BPM.Architecture.ArchitectureTool.Security
{
    partial class ResetPwForm
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
            this.Pwbutton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelnewpw = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pw4 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.userId = new System.Windows.Forms.TextBox();
            this.newPwLb = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cancelButton.Location = new System.Drawing.Point(6, 127);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(99, 25);
            this.cancelButton.TabIndex = 18;
            this.cancelButton.Text = "กลับหน้าแรก";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.okButton.Location = new System.Drawing.Point(269, 53);
            this.okButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(102, 25);
            this.okButton.TabIndex = 17;
            this.okButton.Text = "ตกลง";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // Pwbutton
            // 
            this.Pwbutton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Pwbutton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Pwbutton.Location = new System.Drawing.Point(269, 22);
            this.Pwbutton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Pwbutton.Name = "Pwbutton";
            this.Pwbutton.Size = new System.Drawing.Size(102, 25);
            this.Pwbutton.TabIndex = 20;
            this.Pwbutton.Text = "ขอรหัสยืนยัน";
            this.Pwbutton.UseVisualStyleBackColor = true;
            this.Pwbutton.Click += new System.EventHandler(this.Pwbutton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.newPwLb);
            this.groupBox1.Controls.Add(this.labelnewpw);
            this.groupBox1.Controls.Add(this.okButton);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.pw4);
            this.groupBox1.Controls.Add(this.Pwbutton);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.userId);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(6, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(380, 120);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            // 
            // labelnewpw
            // 
            this.labelnewpw.AutoSize = true;
            this.labelnewpw.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.labelnewpw.Location = new System.Drawing.Point(6, 88);
            this.labelnewpw.Name = "labelnewpw";
            this.labelnewpw.Size = new System.Drawing.Size(135, 16);
            this.labelnewpw.TabIndex = 23;
            this.labelnewpw.Text = "รหัสผ่านใหม่ของท่านคือ";
            this.labelnewpw.UseWaitCursor = true;
            this.labelnewpw.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(6, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 16);
            this.label2.TabIndex = 21;
            this.label2.Text = "กรุณาใส่รหัสยืนยัน";
            // 
            // pw4
            // 
            this.pw4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.pw4.Location = new System.Drawing.Point(147, 54);
            this.pw4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pw4.MaxLength = 4;
            this.pw4.Name = "pw4";
            this.pw4.Size = new System.Drawing.Size(116, 23);
            this.pw4.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 16);
            this.label1.TabIndex = 15;
            this.label1.Text = "เลขประจำตัว";
            // 
            // userId
            // 
            this.userId.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.userId.Location = new System.Drawing.Point(147, 24);
            this.userId.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.userId.MaxLength = 8;
            this.userId.Name = "userId";
            this.userId.Size = new System.Drawing.Size(116, 23);
            this.userId.TabIndex = 16;
            this.userId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.userId_KeyPress);
            // 
            // newPwLb
            // 
            this.newPwLb.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.newPwLb.Location = new System.Drawing.Point(147, 81);
            this.newPwLb.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.newPwLb.MaxLength = 4;
            this.newPwLb.Name = "newPwLb";
            this.newPwLb.ReadOnly = true;
            this.newPwLb.Size = new System.Drawing.Size(116, 23);
            this.newPwLb.TabIndex = 25;
            this.newPwLb.Visible = false;
            // 
            // ResetPwForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 156);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ResetPwForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ลืมรหัสผ่าน";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button Pwbutton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox userId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox pw4;
        private Label labelnewpw;
        private TextBox newPwLb;
    }
}