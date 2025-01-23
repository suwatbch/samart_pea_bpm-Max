namespace PEA.BPM.CashManagementModule
{
    partial class FillMoneyTypeForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.subTotalTxt = new System.Windows.Forms.TextBox();
            this.totalAmtTxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.JangNum = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.previewBt = new System.Windows.Forms.Button();
            this.cancelBt = new System.Windows.Forms.Button();
            this.moneyTypeGv = new PEA.BPM.CashManagementModule.CustomControls.MoneyGridView(this.components);
            this.panel2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.JangNum.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.moneyTypeGv)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.groupBox3);
            this.panel2.Controls.Add(this.previewBt);
            this.panel2.Controls.Add(this.cancelBt);
            this.panel2.Location = new System.Drawing.Point(6, 6);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(439, 334);
            this.panel2.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(18, 291);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 34);
            this.button1.TabIndex = 4;
            this.button1.Text = "อัตโนมัติ";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.subTotalTxt);
            this.groupBox3.Controls.Add(this.totalAmtTxt);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.JangNum);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(10, 4);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Size = new System.Drawing.Size(421, 283);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "เงินสดคงเหลือ";
            // 
            // subTotalTxt
            // 
            this.subTotalTxt.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subTotalTxt.Location = new System.Drawing.Point(117, 27);
            this.subTotalTxt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.subTotalTxt.Name = "subTotalTxt";
            this.subTotalTxt.ReadOnly = true;
            this.subTotalTxt.Size = new System.Drawing.Size(134, 36);
            this.subTotalTxt.TabIndex = 0;
            this.subTotalTxt.TabStop = false;
            this.subTotalTxt.Text = "0.00";
            this.subTotalTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // totalAmtTxt
            // 
            this.totalAmtTxt.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalAmtTxt.Location = new System.Drawing.Point(272, 27);
            this.totalAmtTxt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.totalAmtTxt.Name = "totalAmtTxt";
            this.totalAmtTxt.ReadOnly = true;
            this.totalAmtTxt.Size = new System.Drawing.Size(139, 36);
            this.totalAmtTxt.TabIndex = 0;
            this.totalAmtTxt.TabStop = false;
            this.totalAmtTxt.Text = "0.00";
            this.totalAmtTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(249, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 42);
            this.label2.TabIndex = 4;
            this.label2.Text = "/";
            // 
            // JangNum
            // 
            this.JangNum.Controls.Add(this.moneyTypeGv);
            this.JangNum.Location = new System.Drawing.Point(8, 70);
            this.JangNum.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.JangNum.Name = "JangNum";
            this.JangNum.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.JangNum.Size = new System.Drawing.Size(407, 208);
            this.JangNum.TabIndex = 2;
            this.JangNum.TabStop = false;
            this.JangNum.Text = "รายการแจงนับ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(5, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 35);
            this.label1.TabIndex = 0;
            this.label1.Text = "รวมเงิน:";
            // 
            // previewBt
            // 
            this.previewBt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.previewBt.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.previewBt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.previewBt.Location = new System.Drawing.Point(211, 291);
            this.previewBt.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.previewBt.Name = "previewBt";
            this.previewBt.Size = new System.Drawing.Size(104, 34);
            this.previewBt.TabIndex = 2;
            this.previewBt.Text = "ตกลง";
            this.previewBt.UseVisualStyleBackColor = false;
            this.previewBt.Click += new System.EventHandler(this.previewBt_Click);
            // 
            // cancelBt
            // 
            this.cancelBt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelBt.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.cancelBt.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelBt.Location = new System.Drawing.Point(321, 291);
            this.cancelBt.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.cancelBt.Name = "cancelBt";
            this.cancelBt.Size = new System.Drawing.Size(104, 34);
            this.cancelBt.TabIndex = 3;
            this.cancelBt.Text = "ยกเลิก";
            this.cancelBt.UseVisualStyleBackColor = false;
            // 
            // moneyTypeGv
            // 
            this.moneyTypeGv.AllowUserToAddRows = false;
            this.moneyTypeGv.AllowUserToDeleteRows = false;
            this.moneyTypeGv.AllowUserToResizeColumns = false;
            this.moneyTypeGv.AllowUserToResizeRows = false;
            this.moneyTypeGv.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.moneyTypeGv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.moneyTypeGv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.moneyTypeGv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.moneyTypeGv.GridColor = System.Drawing.SystemColors.ControlLight;
            this.moneyTypeGv.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.moneyTypeGv.Location = new System.Drawing.Point(3, 20);
            this.moneyTypeGv.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.moneyTypeGv.Name = "moneyTypeGv";
            this.moneyTypeGv.RowHeadersWidth = 10;
            this.moneyTypeGv.Size = new System.Drawing.Size(401, 184);
            this.moneyTypeGv.TabIndex = 1;
            this.moneyTypeGv.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.moneyTypeGv_CellClick);
            
            // 
            // FillMoneyTypeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(211, 117, 191);
            this.ClientSize = new System.Drawing.Size(450, 346);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FillMoneyTypeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "แจงประเภทของเงินในแต่ละรายการ";
            this.panel2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.JangNum.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.moneyTypeGv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox JangNum;
        private PEA.BPM.CashManagementModule.CustomControls.MoneyGridView moneyTypeGv;
        private System.Windows.Forms.TextBox totalAmtTxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button previewBt;
        private System.Windows.Forms.Button cancelBt;
        private System.Windows.Forms.TextBox subTotalTxt;
        private System.Windows.Forms.Label label2;
    }
}