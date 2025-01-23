namespace PEA.BPM.BillPrintingModule.BillPrintingUtilities
{
    partial class CAInputList
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
            this.CAlistBox = new System.Windows.Forms.ListBox();
            this.okBt = new System.Windows.Forms.Button();
            this.cancelBt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CAlistBox
            // 
            this.CAlistBox.FormattingEnabled = true;
            this.CAlistBox.Location = new System.Drawing.Point(2, 3);
            this.CAlistBox.Name = "CAlistBox";
            this.CAlistBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.CAlistBox.Size = new System.Drawing.Size(170, 225);
            this.CAlistBox.TabIndex = 0;
            // 
            // okBt
            // 
            this.okBt.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okBt.Location = new System.Drawing.Point(57, 231);
            this.okBt.Name = "okBt";
            this.okBt.Size = new System.Drawing.Size(55, 22);
            this.okBt.TabIndex = 1;
            this.okBt.Text = "ตกลง";
            this.okBt.UseVisualStyleBackColor = true;
            // 
            // cancelBt
            // 
            this.cancelBt.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBt.Location = new System.Drawing.Point(116, 231);
            this.cancelBt.Name = "cancelBt";
            this.cancelBt.Size = new System.Drawing.Size(56, 22);
            this.cancelBt.TabIndex = 2;
            this.cancelBt.Text = "ยกเลิก";
            this.cancelBt.UseVisualStyleBackColor = true;
            // 
            // CAInputList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(174, 257);
            this.Controls.Add(this.cancelBt);
            this.Controls.Add(this.okBt);
            this.Controls.Add(this.CAlistBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CAInputList";
            this.Text = "หมายเลขผู้ใช้ไฟฟ้า";
            this.Deactivate += new System.EventHandler(this.CAInputList_Deactivate);
            this.Leave += new System.EventHandler(this.CAInputList_Leave);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox CAlistBox;
        private System.Windows.Forms.Button okBt;
        private System.Windows.Forms.Button cancelBt;
    }
}