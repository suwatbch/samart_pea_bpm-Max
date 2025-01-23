namespace PEA.BPM.CashManagementModule
{
    partial class AvailableDayPayIn
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cancelButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.payInGv = new System.Windows.Forms.DataGridView();
            this.Select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.BankName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BankAccNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CashAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChequeAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BankKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.payInGv)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.cancelButton);
            this.panel1.Controls.Add(this.addButton);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(7, 7);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(676, 339);
            this.panel1.TabIndex = 0;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.cancelButton.Location = new System.Drawing.Point(574, 302);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(90, 30);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "ยกเลิก";
            this.cancelButton.UseVisualStyleBackColor = false;
            // 
            // addButton
            // 
            this.addButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.addButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.addButton.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.addButton.Location = new System.Drawing.Point(478, 302);
            this.addButton.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(90, 30);
            this.addButton.TabIndex = 2;
            this.addButton.Text = "ตกลง";
            this.addButton.UseVisualStyleBackColor = false;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.payInGv);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(7, 7);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(663, 290);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "รายการนำฝากประจำวัน";
            // 
            // payInGv
            // 
            this.payInGv.AllowUserToAddRows = false;
            this.payInGv.AllowUserToDeleteRows = false;
            this.payInGv.AllowUserToResizeRows = false;
            this.payInGv.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.payInGv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.payInGv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.payInGv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Select,
            this.BankName,
            this.BankAccNo,
            this.CashAmt,
            this.ChequeAmt,
            this.TotalAmt,
            this.ItemTime,
            this.BankKey});
            this.payInGv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.payInGv.GridColor = System.Drawing.SystemColors.ControlLight;
            this.payInGv.Location = new System.Drawing.Point(3, 20);
            this.payInGv.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.payInGv.Name = "payInGv";
            this.payInGv.RowHeadersVisible = false;
            this.payInGv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.payInGv.Size = new System.Drawing.Size(657, 266);
            this.payInGv.TabIndex = 0;
            // 
            // Select
            // 
            this.Select.DataPropertyName = "Select";
            this.Select.HeaderText = "เลือก";
            this.Select.Name = "Select";
            this.Select.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Select.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Select.Width = 40;
            // 
            // BankName
            // 
            this.BankName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.BankName.DataPropertyName = "BankName";
            this.BankName.HeaderText = "ธนาคาร";
            this.BankName.Name = "BankName";
            this.BankName.ReadOnly = true;
            this.BankName.Width = 204;
            // 
            // BankAccNo
            // 
            this.BankAccNo.DataPropertyName = "BankAccNo";
            this.BankAccNo.HeaderText = "เลขที่บัญชี";
            this.BankAccNo.Name = "BankAccNo";
            this.BankAccNo.ReadOnly = true;
            // 
            // CashAmt
            // 
            this.CashAmt.DataPropertyName = "CashAmt";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle12.Format = "N2";
            this.CashAmt.DefaultCellStyle = dataGridViewCellStyle12;
            this.CashAmt.HeaderText = "เงินสด";
            this.CashAmt.Name = "CashAmt";
            this.CashAmt.ReadOnly = true;
            this.CashAmt.Width = 80;
            // 
            // ChequeAmt
            // 
            this.ChequeAmt.DataPropertyName = "ChequeAmt";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle13.Format = "N2";
            this.ChequeAmt.DefaultCellStyle = dataGridViewCellStyle13;
            this.ChequeAmt.HeaderText = "เช็ค";
            this.ChequeAmt.Name = "ChequeAmt";
            this.ChequeAmt.ReadOnly = true;
            this.ChequeAmt.Width = 80;
            // 
            // TotalAmt
            // 
            this.TotalAmt.DataPropertyName = "TotalAmt";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle14.Format = "N2";
            this.TotalAmt.DefaultCellStyle = dataGridViewCellStyle14;
            this.TotalAmt.HeaderText = "จำนวนเงิน";
            this.TotalAmt.Name = "TotalAmt";
            this.TotalAmt.ReadOnly = true;
            // 
            // ItemTime
            // 
            this.ItemTime.DataPropertyName = "ItemTime";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ItemTime.DefaultCellStyle = dataGridViewCellStyle15;
            this.ItemTime.HeaderText = "เวลา";
            this.ItemTime.Name = "ItemTime";
            this.ItemTime.ReadOnly = true;
            this.ItemTime.Width = 50;
            // 
            // BankKey
            // 
            this.BankKey.DataPropertyName = "BankKey";
            this.BankKey.HeaderText = "BankKey";
            this.BankKey.Name = "BankKey";
            this.BankKey.Visible = false;
            // 
            // AvailableDayPayIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(211, 117, 191);
            this.ClientSize = new System.Drawing.Size(689, 351);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AvailableDayPayIn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "รายการแสดงรายงานรายละเอียดการนำฝากธนาคาร";
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.payInGv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView payInGv;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Select;
        private System.Windows.Forms.DataGridViewTextBoxColumn BankName;
        private System.Windows.Forms.DataGridViewTextBoxColumn BankAccNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CashAmt;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChequeAmt;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalAmt;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn BankKey;
    }
}