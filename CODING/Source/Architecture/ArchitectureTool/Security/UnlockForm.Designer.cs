namespace PEA.BPM.Architecture.ArchitectureTool.Security
{
    partial class UnlockForm
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
            this.remarkTextBox = new System.Windows.Forms.TextBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.userIdTextBox = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.captionTextBox = new System.Windows.Forms.TextBox();
            this.remarkComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(41, 134);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "���˵�";
            // 
            // remarkTextBox
            // 
            this.remarkTextBox.Location = new System.Drawing.Point(84, 131);
            this.remarkTextBox.MaxLength = 250;
            this.remarkTextBox.Name = "remarkTextBox";
            this.remarkTextBox.Size = new System.Drawing.Size(324, 21);
            this.remarkTextBox.TabIndex = 13;
            this.remarkTextBox.Visible = false;
            this.remarkTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.remarkTextBox_KeyPress);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(159, 157);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(69, 23);
            this.cancelButton.TabIndex = 16;
            this.cancelButton.Text = "¡��ԡ";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(84, 157);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(69, 23);
            this.okButton.TabIndex = 15;
            this.okButton.Text = "��ŧ";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "���ʼ�ҹ";
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(84, 105);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.PasswordChar = '*';
            this.passwordTextBox.Size = new System.Drawing.Size(144, 21);
            this.passwordTextBox.TabIndex = 11;
            this.passwordTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.passwordTextBox_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "�Ţ��Шӵ��";
            // 
            // userIdTextBox
            // 
            this.userIdTextBox.Location = new System.Drawing.Point(84, 79);
            this.userIdTextBox.Name = "userIdTextBox";
            this.userIdTextBox.Size = new System.Drawing.Size(144, 21);
            this.userIdTextBox.TabIndex = 10;
            this.userIdTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.userIdTextBox_KeyPress);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::PEA.BPM.Architecture.ArchitectureTool.Properties.Resources.Unlock;
            this.pictureBox1.Location = new System.Drawing.Point(19, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(53, 50);
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
            // 
            // captionTextBox
            // 
            this.captionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.captionTextBox.Font = new System.Drawing.Font("Tahoma", 8.5F);
            this.captionTextBox.Location = new System.Drawing.Point(84, 12);
            this.captionTextBox.Multiline = true;
            this.captionTextBox.Name = "captionTextBox";
            this.captionTextBox.ReadOnly = true;
            this.captionTextBox.Size = new System.Drawing.Size(344, 50);
            this.captionTextBox.TabIndex = 20;
            this.captionTextBox.TabStop = false;
            this.captionTextBox.Text = "��ҹ������Ѻ͹حҵ�����ҹ�ѧ��ѹ���\r\n��سһ�͹�Ţ��Шӵ��������ʼ�ҹ������ӹ�" +
                "� ������˵�㹡�ûŴ��ͤ";
            // 
            // remarkComboBox
            // 
            this.remarkComboBox.FormattingEnabled = true;
            this.remarkComboBox.Location = new System.Drawing.Point(84, 130);
            this.remarkComboBox.Name = "remarkComboBox";
            this.remarkComboBox.Size = new System.Drawing.Size(344, 21);
            this.remarkComboBox.TabIndex = 21;
            this.remarkComboBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.remarkComboBox_KeyPress);
            // 
            // UnlockForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 193);
            this.Controls.Add(this.remarkComboBox);
            this.Controls.Add(this.captionTextBox);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.remarkTextBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.userIdTextBox);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UnlockForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " �Ŵ��ͤ�ѧ��ѹ";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox remarkTextBox;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox userIdTextBox;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox captionTextBox;
        private System.Windows.Forms.ComboBox remarkComboBox;
    }
}