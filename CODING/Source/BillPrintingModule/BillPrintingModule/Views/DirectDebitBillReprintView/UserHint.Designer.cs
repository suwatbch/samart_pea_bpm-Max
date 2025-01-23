namespace PEA.BPM.BillPrintingModule.Views
{
    partial class UserHint
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
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.caption4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.caption3 = new System.Windows.Forms.Label();
            this.caption2 = new System.Windows.Forms.Label();
            this.caption1 = new System.Windows.Forms.Label();
            this.header = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 133);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(227, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "เพื่อพิมพ์ทุกบิลเดือนโดยไม่สนใจเงื่อนไขข้างต้น";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Enabled = false;
            this.checkBox1.Location = new System.Drawing.Point(159, 114);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::PEA.BPM.BillPrintingModule.Properties.Resources.Help;
            this.pictureBox1.Location = new System.Drawing.Point(18, 11);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(26, 26);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // caption4
            // 
            this.caption4.AutoSize = true;
            this.caption4.Location = new System.Drawing.Point(16, 114);
            this.caption4.Name = "caption4";
            this.caption4.Size = new System.Drawing.Size(137, 13);
            this.caption4.TabIndex = 4;
            this.caption4.Text = "** นำเครื่องหมายถูกออกจาก";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Beige;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.caption4);
            this.panel1.Controls.Add(this.caption3);
            this.panel1.Controls.Add(this.caption2);
            this.panel1.Controls.Add(this.caption1);
            this.panel1.Controls.Add(this.header);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(251, 169);
            this.panel1.TabIndex = 1;
            // 
            // caption3
            // 
            this.caption3.AutoSize = true;
            this.caption3.Location = new System.Drawing.Point(16, 80);
            this.caption3.Name = "caption3";
            this.caption3.Size = new System.Drawing.Size(163, 13);
            this.caption3.TabIndex = 3;
            this.caption3.Text = "มีผู้ใช้ไฟที่มีบิลมากกว่า 1 บิลเดือน";
            // 
            // caption2
            // 
            this.caption2.AutoSize = true;
            this.caption2.Location = new System.Drawing.Point(16, 61);
            this.caption2.Name = "caption2";
            this.caption2.Size = new System.Drawing.Size(208, 13);
            this.caption2.TabIndex = 2;
            this.caption2.Text = "กรณีการกรุ๊ปส่งธนาคารเพื่อตัดบัญชีธนาคาร";
            // 
            // caption1
            // 
            this.caption1.AutoSize = true;
            this.caption1.Location = new System.Drawing.Point(16, 42);
            this.caption1.Name = "caption1";
            this.caption1.Size = new System.Drawing.Size(139, 13);
            this.caption1.TabIndex = 1;
            this.caption1.Text = "ระบุเงื่อนไขบิลเดือนเพิ่มเติม";
            // 
            // header
            // 
            this.header.AutoSize = true;
            this.header.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.header.ForeColor = System.Drawing.Color.Blue;
            this.header.Location = new System.Drawing.Point(47, 15);
            this.header.Name = "header";
            this.header.Size = new System.Drawing.Size(109, 13);
            this.header.TabIndex = 0;
            this.header.Text = "บิลเดือน (เพิ่มเติม)";
            // 
            // UserHint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Beige;
            this.ClientSize = new System.Drawing.Size(251, 169);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "UserHint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = " ข้อเสนอแนะ";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label caption4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label caption3;
        private System.Windows.Forms.Label caption2;
        private System.Windows.Forms.Label caption1;
        private System.Windows.Forms.Label header;
    }
}