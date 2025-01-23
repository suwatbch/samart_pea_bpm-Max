namespace OfflineTransferManager
{
    partial class ExportingForm
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
            this.transferGroupBox = new System.Windows.Forms.GroupBox();
            this.exportRadioButton = new System.Windows.Forms.RadioButton();
            this.importRadioButton = new System.Windows.Forms.RadioButton();
            this.pathGroupBox = new System.Windows.Forms.GroupBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.exportButton = new System.Windows.Forms.Button();
            this.importButton = new System.Windows.Forms.Button();
            this.pathButton = new System.Windows.Forms.Button();
            this.pathTextBox = new System.Windows.Forms.TextBox();
            this.transferGroupBox.SuspendLayout();
            this.pathGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // transferGroupBox
            // 
            this.transferGroupBox.Controls.Add(this.exportRadioButton);
            this.transferGroupBox.Controls.Add(this.importRadioButton);
            this.transferGroupBox.Location = new System.Drawing.Point(12, 12);
            this.transferGroupBox.Name = "transferGroupBox";
            this.transferGroupBox.Size = new System.Drawing.Size(486, 76);
            this.transferGroupBox.TabIndex = 3;
            this.transferGroupBox.TabStop = false;
            this.transferGroupBox.Text = "เลือกรูปแบบการนำข้อมูลเข้า - ออกระบบ  กรณีที่เครือข่ายมีปัญหา";
            // 
            // exportRadioButton
            // 
            this.exportRadioButton.AutoSize = true;
            this.exportRadioButton.Checked = true;
            this.exportRadioButton.Location = new System.Drawing.Point(18, 31);
            this.exportRadioButton.Name = "exportRadioButton";
            this.exportRadioButton.Size = new System.Drawing.Size(190, 17);
            this.exportRadioButton.TabIndex = 44;
            this.exportRadioButton.TabStop = true;
            this.exportRadioButton.Text = "นำข้อมูลออกจากระบบ (Export Data)";
            this.exportRadioButton.UseVisualStyleBackColor = true;
            this.exportRadioButton.CheckedChanged += new System.EventHandler(this.exportRadioButton_CheckedChanged);
            // 
            // importRadioButton
            // 
            this.importRadioButton.AutoSize = true;
            this.importRadioButton.Location = new System.Drawing.Point(285, 31);
            this.importRadioButton.Name = "importRadioButton";
            this.importRadioButton.Size = new System.Drawing.Size(168, 17);
            this.importRadioButton.TabIndex = 43;
            this.importRadioButton.Text = "นำข้อมูลเข้าระบบ (Import Data)";
            this.importRadioButton.UseVisualStyleBackColor = true;
            this.importRadioButton.CheckedChanged += new System.EventHandler(this.importRadioButton_CheckedChanged);
            // 
            // pathGroupBox
            // 
            this.pathGroupBox.Controls.Add(this.pathTextBox);
            this.pathGroupBox.Controls.Add(this.pathButton);
            this.pathGroupBox.Location = new System.Drawing.Point(12, 94);
            this.pathGroupBox.Name = "pathGroupBox";
            this.pathGroupBox.Size = new System.Drawing.Size(486, 82);
            this.pathGroupBox.TabIndex = 6;
            this.pathGroupBox.TabStop = false;
            this.pathGroupBox.Text = "Directory/Folder/Path สำหรับนำข้อมูลออก";
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // exportButton
            // 
            this.exportButton.Location = new System.Drawing.Point(172, 201);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(168, 23);
            this.exportButton.TabIndex = 8;
            this.exportButton.Text = "นำข้อมูลออกจากระบบ (Export)";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // importButton
            // 
            this.importButton.Location = new System.Drawing.Point(172, 201);
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(168, 23);
            this.importButton.TabIndex = 9;
            this.importButton.Text = "นำข้อมูลเข้าระบบ (Import)";
            this.importButton.UseVisualStyleBackColor = true;
            this.importButton.Click += new System.EventHandler(this.importButton_Click);
            // 
            // pathButton
            // 
            this.pathButton.Location = new System.Drawing.Point(435, 36);
            this.pathButton.Name = "pathButton";
            this.pathButton.Size = new System.Drawing.Size(29, 23);
            this.pathButton.TabIndex = 0;
            this.pathButton.Text = "...";
            this.pathButton.UseVisualStyleBackColor = true;
            this.pathButton.Click += new System.EventHandler(this.pathButton_Click);
            // 
            // pathTextBox
            // 
            this.pathTextBox.BackColor = System.Drawing.Color.White;
            this.pathTextBox.Location = new System.Drawing.Point(18, 38);
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.ReadOnly = true;
            this.pathTextBox.Size = new System.Drawing.Size(411, 20);
            this.pathTextBox.TabIndex = 2;
            // 
            // ExportingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 243);
            this.Controls.Add(this.exportButton);
            this.Controls.Add(this.pathGroupBox);
            this.Controls.Add(this.transferGroupBox);
            this.Controls.Add(this.importButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ExportingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BPM Client Transfer Manager";
            this.transferGroupBox.ResumeLayout(false);
            this.transferGroupBox.PerformLayout();
            this.pathGroupBox.ResumeLayout(false);
            this.pathGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox transferGroupBox;
        private System.Windows.Forms.RadioButton exportRadioButton;
        private System.Windows.Forms.RadioButton importRadioButton;
        private System.Windows.Forms.GroupBox pathGroupBox;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.Button importButton;
        private System.Windows.Forms.Button pathButton;
        private System.Windows.Forms.TextBox pathTextBox;
    }
}

