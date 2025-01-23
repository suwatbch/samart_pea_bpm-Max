namespace PEA.BPM.CashManagementModule
{
    partial class AvailableListForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.previewBt = new System.Windows.Forms.Button();
            this.cancelBt = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.inputGv = new System.Windows.Forms.DataGridView();
            this.CashierId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DayCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CashAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChequeAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CloseWorkBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inputGv)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.previewBt);
            this.panel2.Controls.Add(this.cancelBt);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Location = new System.Drawing.Point(7, 6);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(649, 328);
            this.panel2.TabIndex = 1;
            // 
            // previewBt
            // 
            this.previewBt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.previewBt.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.previewBt.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.previewBt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.previewBt.Location = new System.Drawing.Point(443, 289);
            this.previewBt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.previewBt.Name = "previewBt";
            this.previewBt.Size = new System.Drawing.Size(95, 31);
            this.previewBt.TabIndex = 5;
            this.previewBt.Text = "แสดงรายงาน";
            this.previewBt.UseVisualStyleBackColor = false;
            this.previewBt.Click += new System.EventHandler(this.previewBt_Click);
            // 
            // cancelBt
            // 
            this.cancelBt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelBt.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.cancelBt.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelBt.Location = new System.Drawing.Point(544, 289);
            this.cancelBt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cancelBt.Name = "cancelBt";
            this.cancelBt.Size = new System.Drawing.Size(97, 31);
            this.cancelBt.TabIndex = 3;
            this.cancelBt.Text = "ยกเลิก";
            this.cancelBt.UseVisualStyleBackColor = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.inputGv);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(5, 7);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(636, 274);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "รายการที่พบ";
            // 
            // inputGv
            // 
            this.inputGv.AllowUserToAddRows = false;
            this.inputGv.AllowUserToDeleteRows = false;
            this.inputGv.AllowUserToResizeRows = false;
            this.inputGv.BackgroundColor = System.Drawing.SystemColors.Control;
            this.inputGv.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.inputGv.ColumnHeadersHeight = 24;
            this.inputGv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.inputGv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CashierId,
            this.DayCount,
            this.ItemDate,
            this.CashAmt,
            this.ChequeAmt,
            this.CloseWorkBy,
            this.ItemId});
            this.inputGv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputGv.GridColor = System.Drawing.SystemColors.Control;
            this.inputGv.Location = new System.Drawing.Point(3, 20);
            this.inputGv.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.inputGv.MultiSelect = false;
            this.inputGv.Name = "inputGv";
            this.inputGv.ReadOnly = true;
            this.inputGv.RowHeadersVisible = false;
            this.inputGv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.inputGv.Size = new System.Drawing.Size(630, 250);
            this.inputGv.TabIndex = 0;
            this.inputGv.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.inputGv_CellDoubleClick);
            // 
            // CashierId
            // 
            this.CashierId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CashierId.DataPropertyName = "CashierId";
            this.CashierId.HeaderText = "พนักงาน";
            this.CashierId.Name = "CashierId";
            this.CashierId.ReadOnly = true;
            // 
            // DayCount
            // 
            this.DayCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.DayCount.DataPropertyName = "DayCount";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DayCount.DefaultCellStyle = dataGridViewCellStyle1;
            this.DayCount.HeaderText = "กะที่";
            this.DayCount.Name = "DayCount";
            this.DayCount.ReadOnly = true;
            this.DayCount.Width = 60;
            // 
            // ItemDate
            // 
            this.ItemDate.DataPropertyName = "ItemDate";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ItemDate.DefaultCellStyle = dataGridViewCellStyle2;
            this.ItemDate.HeaderText = "วันที่เปิดกะ";
            this.ItemDate.Name = "ItemDate";
            this.ItemDate.ReadOnly = true;
            this.ItemDate.Width = 120;
            // 
            // CashAmt
            // 
            this.CashAmt.DataPropertyName = "CashAmt";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            this.CashAmt.DefaultCellStyle = dataGridViewCellStyle3;
            this.CashAmt.HeaderText = "เงินสด(บาท)";
            this.CashAmt.Name = "CashAmt";
            this.CashAmt.ReadOnly = true;
            this.CashAmt.Width = 120;
            // 
            // ChequeAmt
            // 
            this.ChequeAmt.DataPropertyName = "ChequeAmt";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            this.ChequeAmt.DefaultCellStyle = dataGridViewCellStyle4;
            this.ChequeAmt.HeaderText = "เช็ค(บาท)";
            this.ChequeAmt.Name = "ChequeAmt";
            this.ChequeAmt.ReadOnly = true;
            this.ChequeAmt.Width = 120;
            // 
            // CloseWorkBy
            // 
            this.CloseWorkBy.DataPropertyName = "CloseWorkBy";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.CloseWorkBy.DefaultCellStyle = dataGridViewCellStyle5;
            this.CloseWorkBy.HeaderText = "ปิดกะโดย";
            this.CloseWorkBy.Name = "CloseWorkBy";
            this.CloseWorkBy.ReadOnly = true;
            this.CloseWorkBy.Width = 90;
            // 
            // ItemId
            // 
            this.ItemId.DataPropertyName = "ItemId";
            this.ItemId.HeaderText = "ItemId";
            this.ItemId.Name = "ItemId";
            this.ItemId.ReadOnly = true;
            this.ItemId.Visible = false;
            // 
            // AvailableListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(211, 117, 191);
            this.ClientSize = new System.Drawing.Size(660, 339);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AvailableListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "รายการออกรายงาน";
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.inputGv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button previewBt;
        private System.Windows.Forms.Button cancelBt;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView inputGv;
        private System.Windows.Forms.DataGridViewTextBoxColumn CashierId;
        private System.Windows.Forms.DataGridViewTextBoxColumn DayCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn CashAmt;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChequeAmt;
        private System.Windows.Forms.DataGridViewTextBoxColumn CloseWorkBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemId;
    }
}