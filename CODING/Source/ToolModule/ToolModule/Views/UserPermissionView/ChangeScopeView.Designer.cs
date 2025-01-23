namespace PEA.BPM.ToolModule
{
    partial class ChangeScopeView
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.userNameText = new System.Windows.Forms.TextBox();
            this.scopeCBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.okBt = new System.Windows.Forms.Button();
            this.cancelBt = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "ขอบเขต :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.userNameText);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.scopeCBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.groupBox1.Location = new System.Drawing.Point(2, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(330, 88);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // userNameText
            // 
            this.userNameText.Location = new System.Drawing.Point(77, 18);
            this.userNameText.Name = "userNameText";
            this.userNameText.ReadOnly = true;
            this.userNameText.Size = new System.Drawing.Size(240, 23);
            this.userNameText.TabIndex = 8;
            // 
            // scopeCBox
            // 
            this.scopeCBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.scopeCBox.FormattingEnabled = true;
            this.scopeCBox.Items.AddRange(new object[] {
            "การไฟฟ้าสังกัด",
            "เขตการไฟฟ้า",
            "ทุกการไฟฟ้า"});
            this.scopeCBox.Location = new System.Drawing.Point(77, 47);
            this.scopeCBox.Name = "scopeCBox";
            this.scopeCBox.Size = new System.Drawing.Size(240, 24);
            this.scopeCBox.TabIndex = 6;
            this.scopeCBox.SelectedIndexChanged += new System.EventHandler(this.scopeCBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "ชื่อผู้ใช้ :";
            // 
            // okBt
            // 
            this.okBt.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.okBt.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okBt.Enabled = false;
            this.okBt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.okBt.Location = new System.Drawing.Point(139, 94);
            this.okBt.Name = "okBt";
            this.okBt.Size = new System.Drawing.Size(87, 25);
            this.okBt.TabIndex = 7;
            this.okBt.Text = "ตกลง";
            this.okBt.UseVisualStyleBackColor = false;
            this.okBt.Click += new System.EventHandler(this.okBt_Click);
            // 
            // cancelBt
            // 
            this.cancelBt.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.cancelBt.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cancelBt.Location = new System.Drawing.Point(232, 94);
            this.cancelBt.Name = "cancelBt";
            this.cancelBt.Size = new System.Drawing.Size(87, 25);
            this.cancelBt.TabIndex = 8;
            this.cancelBt.Text = "ยกเลิก";
            this.cancelBt.UseVisualStyleBackColor = false;
            // 
            // ChangeScopeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 129);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.okBt);
            this.Controls.Add(this.cancelBt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ChangeScopeView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "กำหนดขอบเขตการใช้งาน";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox scopeCBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button okBt;
        private System.Windows.Forms.Button cancelBt;
        private System.Windows.Forms.TextBox userNameText;
    }
}