namespace PEA.BPM.Architecture.ArchitectureTool.ErrorHandling
{
    partial class CompleteExceptionForm
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
            this.OKBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.MessageTxt = new System.Windows.Forms.TextBox();
            this.ResolveTxt = new System.Windows.Forms.TextBox();
            this.ErrorCodeTxt = new System.Windows.Forms.TextBox();
            this.DebuggingIDTxt = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.StackTrackTxt = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.HelpLinkIL = new System.Windows.Forms.LinkLabel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.CauseTxt = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CopyST1Btn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // OKBtn
            // 
            this.OKBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKBtn.Location = new System.Drawing.Point(614, 468);
            this.OKBtn.Name = "OKBtn";
            this.OKBtn.Size = new System.Drawing.Size(75, 23);
            this.OKBtn.TabIndex = 0;
            this.OKBtn.Text = "ตกลง";
            this.OKBtn.UseVisualStyleBackColor = true;
            this.OKBtn.Click += new System.EventHandler(this.OKBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "รหัสข้อผิดพลาด";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(529, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "รหัส ticket";
            // 
            // MessageTxt
            // 
            this.MessageTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MessageTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MessageTxt.Location = new System.Drawing.Point(3, 16);
            this.MessageTxt.Multiline = true;
            this.MessageTxt.Name = "MessageTxt";
            this.MessageTxt.ReadOnly = true;
            this.MessageTxt.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.MessageTxt.Size = new System.Drawing.Size(665, 72);
            this.MessageTxt.TabIndex = 12;
            // 
            // ResolveTxt
            // 
            this.ResolveTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ResolveTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResolveTxt.Location = new System.Drawing.Point(3, 16);
            this.ResolveTxt.Multiline = true;
            this.ResolveTxt.Name = "ResolveTxt";
            this.ResolveTxt.ReadOnly = true;
            this.ResolveTxt.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ResolveTxt.Size = new System.Drawing.Size(665, 120);
            this.ResolveTxt.TabIndex = 13;
            // 
            // ErrorCodeTxt
            // 
            this.ErrorCodeTxt.Location = new System.Drawing.Point(85, 3);
            this.ErrorCodeTxt.Name = "ErrorCodeTxt";
            this.ErrorCodeTxt.ReadOnly = true;
            this.ErrorCodeTxt.Size = new System.Drawing.Size(100, 20);
            this.ErrorCodeTxt.TabIndex = 15;
            // 
            // DebuggingIDTxt
            // 
            this.DebuggingIDTxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DebuggingIDTxt.Location = new System.Drawing.Point(589, 3);
            this.DebuggingIDTxt.Name = "DebuggingIDTxt";
            this.DebuggingIDTxt.ReadOnly = true;
            this.DebuggingIDTxt.Size = new System.Drawing.Size(100, 20);
            this.DebuggingIDTxt.TabIndex = 16;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.MessageTxt);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(671, 91);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ข้อความ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ResolveTxt);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(3, 221);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(671, 139);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "วิธีแก้ไขเบื้องต้น";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.StackTrackTxt);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(671, 370);
            this.groupBox3.TabIndex = 19;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Stack trace";
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
            this.StackTrackTxt.Size = new System.Drawing.Size(665, 351);
            this.StackTrackTxt.TabIndex = 13;
            this.StackTrackTxt.WordWrap = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(4, 29);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(685, 433);
            this.tabControl1.TabIndex = 20;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox6);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox5);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(677, 407);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.HelpLinkIL);
            this.groupBox6.Controls.Add(this.textBox1);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox6.Location = new System.Drawing.Point(3, 360);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(671, 44);
            this.groupBox6.TabIndex = 20;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Link ที่เกี่ยวข้อง";
            // 
            // HelpLinkIL
            // 
            this.HelpLinkIL.AutoSize = true;
            this.HelpLinkIL.Location = new System.Drawing.Point(6, 16);
            this.HelpLinkIL.Name = "HelpLinkIL";
            this.HelpLinkIL.Size = new System.Drawing.Size(130, 13);
            this.HelpLinkIL.TabIndex = 14;
            this.HelpLinkIL.TabStop = true;
            this.HelpLinkIL.Text = "http://localhost/web.aspx";
            this.HelpLinkIL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.HelpLinkIL_LinkClicked);
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(3, 16);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(665, 25);
            this.textBox1.TabIndex = 13;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.CauseTxt);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox5.Location = new System.Drawing.Point(3, 94);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(671, 127);
            this.groupBox5.TabIndex = 19;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "สาเหตุ";
            // 
            // CauseTxt
            // 
            this.CauseTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CauseTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CauseTxt.Location = new System.Drawing.Point(3, 16);
            this.CauseTxt.Multiline = true;
            this.CauseTxt.Name = "CauseTxt";
            this.CauseTxt.ReadOnly = true;
            this.CauseTxt.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.CauseTxt.Size = new System.Drawing.Size(665, 108);
            this.CauseTxt.TabIndex = 13;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(677, 407);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Stacktrace";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.CopyST1Btn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 373);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(671, 31);
            this.panel1.TabIndex = 21;
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
            // CompleteExceptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 495);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.DebuggingIDTxt);
            this.Controls.Add(this.ErrorCodeTxt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.OKBtn);
            this.Name = "CompleteExceptionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ข้อผิดพลาด";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OKBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox MessageTxt;
        private System.Windows.Forms.TextBox ResolveTxt;
        private System.Windows.Forms.TextBox ErrorCodeTxt;
        private System.Windows.Forms.TextBox DebuggingIDTxt;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox StackTrackTxt;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox CauseTxt;
        private System.Windows.Forms.LinkLabel HelpLinkIL;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button CopyST1Btn;
    }
}