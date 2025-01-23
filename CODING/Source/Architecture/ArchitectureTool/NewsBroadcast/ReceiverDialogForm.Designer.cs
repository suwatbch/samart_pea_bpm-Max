namespace PEA.BPM.Architecture.ArchitectureTool.NewsBroadcast
{
    partial class RecieverDialogForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecieverDialogForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelDate = new System.Windows.Forms.Label();
            this.labelHeader = new System.Windows.Forms.Label();
            this.groupBoxMessage = new System.Windows.Forms.GroupBox();
            this.richTextBoxDetail = new System.Windows.Forms.RichTextBox();
            this.buttonClose = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBoxRead = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.textBoxTopic = new System.Windows.Forms.TextBox();
            this.labelTopic = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.groupBoxMessage.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.labelDate);
            this.panel1.Controls.Add(this.labelHeader);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(10, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(441, 65);
            this.panel1.TabIndex = 0;
            // 
            // labelDate
            // 
            this.labelDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.labelDate.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.labelDate.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.labelDate.Location = new System.Drawing.Point(10, 33);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(421, 28);
            this.labelDate.TabIndex = 1;
            this.labelDate.Text = "วันที่: 1 มกราคม 2552 เวลา: 16:00";
            this.labelDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelHeader
            // 
            this.labelHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.labelHeader.AutoSize = true;
            this.labelHeader.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.labelHeader.ForeColor = System.Drawing.Color.Red;
            this.labelHeader.Location = new System.Drawing.Point(115, 6);
            this.labelHeader.Name = "labelHeader";
            this.labelHeader.Size = new System.Drawing.Size(214, 25);
            this.labelHeader.TabIndex = 0;
            this.labelHeader.Text = "ประกาศข่าวจาก BPM";
            // 
            // groupBoxMessage
            // 
            this.groupBoxMessage.Controls.Add(this.richTextBoxDetail);
            this.groupBoxMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxMessage.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.groupBoxMessage.Location = new System.Drawing.Point(10, 120);
            this.groupBoxMessage.Name = "groupBoxMessage";
            this.groupBoxMessage.Padding = new System.Windows.Forms.Padding(10, 5, 10, 10);
            this.groupBoxMessage.Size = new System.Drawing.Size(441, 180);
            this.groupBoxMessage.TabIndex = 1;
            this.groupBoxMessage.TabStop = false;
            this.groupBoxMessage.Text = "ข้อความ :";
            // 
            // richTextBoxDetail
            // 
            this.richTextBoxDetail.BackColor = System.Drawing.SystemColors.Info;
            this.richTextBoxDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBoxDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxDetail.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.richTextBoxDetail.Location = new System.Drawing.Point(10, 22);
            this.richTextBoxDetail.Name = "richTextBoxDetail";
            this.richTextBoxDetail.ReadOnly = true;
            this.richTextBoxDetail.Size = new System.Drawing.Size(421, 148);
            this.richTextBoxDetail.TabIndex = 2;
            this.richTextBoxDetail.Text = "";
            this.richTextBoxDetail.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.richTextBoxDetail_LinkClicked);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.buttonClose.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.buttonClose.Location = new System.Drawing.Point(161, 47);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(111, 34);
            this.buttonClose.TabIndex = 5;
            this.buttonClose.Text = "ปิด";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.buttonClose);
            this.panel2.Controls.Add(this.groupBox3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(10, 300);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(441, 84);
            this.panel2.TabIndex = 3;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.groupBox3.Controls.Add(this.checkBoxRead);
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(441, 45);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            // 
            // checkBoxRead
            // 
            this.checkBoxRead.AutoSize = true;
            this.checkBoxRead.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.checkBoxRead.Location = new System.Drawing.Point(151, 12);
            this.checkBoxRead.Name = "checkBoxRead";
            this.checkBoxRead.Size = new System.Drawing.Size(139, 21);
            this.checkBoxRead.TabIndex = 4;
            this.checkBoxRead.Text = "ได้อ่านข้อความแล้ว";
            this.checkBoxRead.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxRead.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.textBoxTopic);
            this.panel3.Controls.Add(this.labelTopic);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(10, 87);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(441, 33);
            this.panel3.TabIndex = 4;
            // 
            // textBoxTopic
            // 
            this.textBoxTopic.BackColor = System.Drawing.SystemColors.Info;
            this.textBoxTopic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxTopic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxTopic.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.textBoxTopic.Location = new System.Drawing.Point(48, 0);
            this.textBoxTopic.Name = "textBoxTopic";
            this.textBoxTopic.ReadOnly = true;
            this.textBoxTopic.Size = new System.Drawing.Size(393, 23);
            this.textBoxTopic.TabIndex = 1;
            // 
            // labelTopic
            // 
            this.labelTopic.AutoSize = true;
            this.labelTopic.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelTopic.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.labelTopic.Location = new System.Drawing.Point(0, 0);
            this.labelTopic.Name = "labelTopic";
            this.labelTopic.Size = new System.Drawing.Size(48, 18);
            this.labelTopic.TabIndex = 0;
            this.labelTopic.Text = "เรื่อง :";
            // 
            // panel5
            // 
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(10, 75);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(441, 12);
            this.panel5.TabIndex = 2;
            // 
            // RecieverDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 394);
            this.Controls.Add(this.groupBoxMessage);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(243, 320);
            this.Name = "RecieverDialogForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ประกาศข่าวจาก BPM";
            this.Load += new System.EventHandler(this.RecieverDialogForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBoxMessage.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelHeader;
        private System.Windows.Forms.GroupBox groupBoxMessage;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox textBoxTopic;
        private System.Windows.Forms.Label labelTopic;
        private System.Windows.Forms.CheckBox checkBoxRead;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RichTextBox richTextBoxDetail;
        private System.Windows.Forms.Panel panel5;
    }
}