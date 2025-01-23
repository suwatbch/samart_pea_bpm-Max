namespace Tool.ConnectionConfigurationManager
{
    partial class SettingForm
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.branchServerTextBox = new System.Windows.Forms.MaskedTextBox();
            this.branchTestButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.centerServerWSTextBox = new System.Windows.Forms.MaskedTextBox();
            this.centerTestButton = new System.Windows.Forms.Button();
            this.printerGroupBox = new System.Windows.Forms.GroupBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.printerCancelButton = new System.Windows.Forms.Button();
            this.printerSaveButton = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.printerGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.branchServerTextBox);
            this.groupBox3.Controls.Add(this.branchTestButton);
            this.groupBox3.Location = new System.Drawing.Point(14, 188);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(607, 86);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Web Service URL ของเครื่องแม่ข่าย \'ประจำสาขา\'";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(15, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(215, 12);
            this.label3.TabIndex = 45;
            this.label3.Text = "( Ex. http://branchJ01101.pea.co.th/BPMService/ )";
            // 
            // branchServerTextBox
            // 
            this.branchServerTextBox.Location = new System.Drawing.Point(19, 33);
            this.branchServerTextBox.Name = "branchServerTextBox";
            this.branchServerTextBox.Size = new System.Drawing.Size(507, 22);
            this.branchServerTextBox.TabIndex = 46;
            // 
            // branchTestButton
            // 
            this.branchTestButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.branchTestButton.Location = new System.Drawing.Point(533, 31);
            this.branchTestButton.Name = "branchTestButton";
            this.branchTestButton.Size = new System.Drawing.Size(66, 25);
            this.branchTestButton.TabIndex = 3;
            this.branchTestButton.Text = "ทดสอบ";
            this.branchTestButton.UseVisualStyleBackColor = false;
            this.branchTestButton.Click += new System.EventHandler(this.printerSaveButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.centerServerWSTextBox);
            this.groupBox2.Controls.Add(this.centerTestButton);
            this.groupBox2.Location = new System.Drawing.Point(14, 96);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(607, 86);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Web Service URL ของเครื่องแม่ข่าย \'ส่วนกลาง\'";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(15, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(183, 12);
            this.label2.TabIndex = 45;
            this.label2.Text = "( Ex. http://center.pea.co.th/BPMService/ )";
            // 
            // centerServerWSTextBox
            // 
            this.centerServerWSTextBox.Location = new System.Drawing.Point(19, 33);
            this.centerServerWSTextBox.Name = "centerServerWSTextBox";
            this.centerServerWSTextBox.Size = new System.Drawing.Size(507, 22);
            this.centerServerWSTextBox.TabIndex = 46;
            // 
            // centerTestButton
            // 
            this.centerTestButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.centerTestButton.Location = new System.Drawing.Point(533, 31);
            this.centerTestButton.Name = "centerTestButton";
            this.centerTestButton.Size = new System.Drawing.Size(66, 25);
            this.centerTestButton.TabIndex = 3;
            this.centerTestButton.Text = "ทดสอบ";
            this.centerTestButton.UseVisualStyleBackColor = false;
            this.centerTestButton.Click += new System.EventHandler(this.testButton_Click);
            // 
            // printerGroupBox
            // 
            this.printerGroupBox.Controls.Add(this.radioButton1);
            this.printerGroupBox.Controls.Add(this.radioButton2);
            this.printerGroupBox.Location = new System.Drawing.Point(14, 13);
            this.printerGroupBox.Name = "printerGroupBox";
            this.printerGroupBox.Size = new System.Drawing.Size(607, 76);
            this.printerGroupBox.TabIndex = 2;
            this.printerGroupBox.TabStop = false;
            this.printerGroupBox.Text = "รูปแบบการเชื่อมต่อกับฐานข้อมูล";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(19, 36);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(134, 18);
            this.radioButton1.TabIndex = 44;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "เครื่องแม่ข่ายส่วนกลาง";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.Location = new System.Drawing.Point(222, 36);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(145, 18);
            this.radioButton2.TabIndex = 43;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "เครื่องแม่ข่ายประจำสาขา";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // printerCancelButton
            // 
            this.printerCancelButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.printerCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.printerCancelButton.Location = new System.Drawing.Point(534, 281);
            this.printerCancelButton.Name = "printerCancelButton";
            this.printerCancelButton.Size = new System.Drawing.Size(87, 25);
            this.printerCancelButton.TabIndex = 6;
            this.printerCancelButton.Text = "ยกเลิก";
            this.printerCancelButton.UseVisualStyleBackColor = false;
            this.printerCancelButton.Click += new System.EventHandler(this.printerCancelButton_Click);
            // 
            // printerSaveButton
            // 
            this.printerSaveButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.printerSaveButton.Location = new System.Drawing.Point(440, 281);
            this.printerSaveButton.Name = "printerSaveButton";
            this.printerSaveButton.Size = new System.Drawing.Size(87, 25);
            this.printerSaveButton.TabIndex = 3;
            this.printerSaveButton.Text = "ตกลง";
            this.printerSaveButton.UseVisualStyleBackColor = false;
            this.printerSaveButton.Click += new System.EventHandler(this.printerSaveButton_Click);
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 316);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.printerGroupBox);
            this.Controls.Add(this.printerCancelButton);
            this.Controls.Add(this.printerSaveButton);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SettingForm";
            this.Text = "  BPM Client Connnection Configuration Manager";
            this.Load += new System.EventHandler(this.SettingForm_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.printerGroupBox.ResumeLayout(false);
            this.printerGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MaskedTextBox branchServerTextBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox centerServerWSTextBox;
        private System.Windows.Forms.GroupBox printerGroupBox;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Button printerCancelButton;
        private System.Windows.Forms.Button printerSaveButton;
        private System.Windows.Forms.Button branchTestButton;
        private System.Windows.Forms.Button centerTestButton;
    }
}

