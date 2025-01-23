namespace PEA.BPM.BillPrintingModule
{
    partial class BABankSelection
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bankGv = new System.Windows.Forms.DataGridView();
            this.BankKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BankName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.okBt = new System.Windows.Forms.Button();
            this.cancelBt = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bankGv)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bankGv);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(4, 7);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(408, 204);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "รายการธนาคาร";
            // 
            // bankGv
            // 
            this.bankGv.AllowUserToAddRows = false;
            this.bankGv.AllowUserToDeleteRows = false;
            this.bankGv.AllowUserToResizeColumns = false;
            this.bankGv.AllowUserToResizeRows = false;
            this.bankGv.BackgroundColor = System.Drawing.Color.White;
            this.bankGv.ColumnHeadersHeight = 24;
            this.bankGv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.bankGv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BankKey,
            this.BankName});
            this.bankGv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bankGv.GridColor = System.Drawing.SystemColors.ControlLight;
            this.bankGv.Location = new System.Drawing.Point(3, 19);
            this.bankGv.MultiSelect = false;
            this.bankGv.Name = "bankGv";
            this.bankGv.RowHeadersVisible = false;
            this.bankGv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.bankGv.Size = new System.Drawing.Size(402, 181);
            this.bankGv.TabIndex = 0;
            this.bankGv.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.bankGv_CellDoubleClick);
            // 
            // BankKey
            // 
            this.BankKey.DataPropertyName = "BankKey";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.BankKey.DefaultCellStyle = dataGridViewCellStyle2;
            this.BankKey.HeaderText = "รหัส";
            this.BankKey.Name = "BankKey";
            this.BankKey.ReadOnly = true;
            this.BankKey.Width = 80;
            // 
            // BankName
            // 
            this.BankName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.BankName.DataPropertyName = "BankName";
            this.BankName.HeaderText = "ชื่อสาขาธนาคาร";
            this.BankName.Name = "BankName";
            this.BankName.ReadOnly = true;
            // 
            // okBt
            // 
            this.okBt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okBt.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okBt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okBt.Location = new System.Drawing.Point(129, 218);
            this.okBt.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.okBt.Name = "okBt";
            this.okBt.Size = new System.Drawing.Size(84, 27);
            this.okBt.TabIndex = 2;
            this.okBt.Text = "ตกลง";
            this.okBt.UseVisualStyleBackColor = true;
            this.okBt.Click += new System.EventHandler(this.okBt_Click);
            // 
            // cancelBt
            // 
            this.cancelBt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelBt.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelBt.Location = new System.Drawing.Point(219, 218);
            this.cancelBt.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cancelBt.Name = "cancelBt";
            this.cancelBt.Size = new System.Drawing.Size(84, 27);
            this.cancelBt.TabIndex = 3;
            this.cancelBt.Text = "ยกเลิก";
            this.cancelBt.UseVisualStyleBackColor = true;
            this.cancelBt.Click += new System.EventHandler(this.cancelBt_Click);
            // 
            // BABankSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(417, 253);
            this.Controls.Add(this.cancelBt);
            this.Controls.Add(this.okBt);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BABankSelection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "กรุณาเลือกธนาคาร";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bankGv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView bankGv;
        private System.Windows.Forms.Button okBt;
        private System.Windows.Forms.Button cancelBt;
        private System.Windows.Forms.DataGridViewTextBoxColumn BankKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn BankName;
    }
}