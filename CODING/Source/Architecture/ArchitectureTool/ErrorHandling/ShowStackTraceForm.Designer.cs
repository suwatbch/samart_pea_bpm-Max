namespace PEA.BPM.Architecture.ArchitectureTool.ErrorHandling
{
    partial class ShowStackTraceForm
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
            this.StackTrackTxt = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CopyST1Btn = new System.Windows.Forms.Button();
            this.OKBtn = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.StackTrackTxt);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(630, 354);
            this.groupBox3.TabIndex = 22;
            this.groupBox3.TabStop = false;
            // 
            // StackTrackTxt
            // 
            this.StackTrackTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.StackTrackTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StackTrackTxt.Location = new System.Drawing.Point(3, 16);
            this.StackTrackTxt.Multiline = true;
            this.StackTrackTxt.Name = "StackTrackTxt";
            this.StackTrackTxt.ReadOnly = true;
            this.StackTrackTxt.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.StackTrackTxt.Size = new System.Drawing.Size(624, 335);
            this.StackTrackTxt.TabIndex = 13;
            this.StackTrackTxt.WordWrap = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.OKBtn);
            this.panel1.Controls.Add(this.CopyST1Btn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 354);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(630, 31);
            this.panel1.TabIndex = 23;
            // 
            // CopyST1Btn
            // 
            this.CopyST1Btn.Location = new System.Drawing.Point(3, 3);
            this.CopyST1Btn.Name = "CopyST1Btn";
            this.CopyST1Btn.Size = new System.Drawing.Size(139, 23);
            this.CopyST1Btn.TabIndex = 0;
            this.CopyST1Btn.Text = "Copy to clipboard";
            this.CopyST1Btn.UseVisualStyleBackColor = true;
            this.CopyST1Btn.Click += new System.EventHandler(this.CopyST1Btn_Click);
            // 
            // OKBtn
            // 
            this.OKBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKBtn.Location = new System.Drawing.Point(552, 3);
            this.OKBtn.Name = "OKBtn";
            this.OKBtn.Size = new System.Drawing.Size(75, 23);
            this.OKBtn.TabIndex = 1;
            this.OKBtn.Text = "ตกลง";
            this.OKBtn.UseVisualStyleBackColor = true;
            // 
            // ShowStackTraceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 385);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.panel1);
            this.Name = "ShowStackTraceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Stack trace";
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox StackTrackTxt;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button CopyST1Btn;
        private System.Windows.Forms.Button OKBtn;
    }
}