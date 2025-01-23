namespace PEA.BPM.PaymentManagementModule.Views.ToBeCancelledPaymentVoucherView
{
    partial class CancelPaymentVoucherForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label16 = new System.Windows.Forms.Label();
            this.repaidDataGridView = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.okButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.changeAmountTextBox = new System.Windows.Forms.TextBox();
            this.ivDescColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ivAmountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.repaidDataGridView)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label16.ForeColor = System.Drawing.Color.Red;
            this.label16.Location = new System.Drawing.Point(6, 47);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(326, 47);
            this.label16.TabIndex = 7;
            this.label16.Text = "จำนวนเงินที่ต้องได้รับ";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // repaidDataGridView
            // 
            this.repaidDataGridView.AllowUserToAddRows = false;
            this.repaidDataGridView.AllowUserToDeleteRows = false;
            this.repaidDataGridView.AllowUserToResizeRows = false;
            this.repaidDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.repaidDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.repaidDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.repaidDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ivDescColumn,
            this.ivAmountColumn,
            this.Column5});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(211, 117, 191);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.repaidDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.repaidDataGridView.EnableHeadersVisualStyles = false;
            this.repaidDataGridView.Location = new System.Drawing.Point(6, 104);
            this.repaidDataGridView.Name = "repaidDataGridView";
            this.repaidDataGridView.ReadOnly = true;
            this.repaidDataGridView.RowHeadersVisible = false;
            this.repaidDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.repaidDataGridView.Size = new System.Drawing.Size(581, 148);
            this.repaidDataGridView.TabIndex = 0;
            this.repaidDataGridView.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.repaidDataGridView_KeyPress);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.okButton);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.panel3.Location = new System.Drawing.Point(5, 248);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(593, 34);
            this.panel3.TabIndex = 1;
            // 
            // okButton
            // 
            this.okButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.okButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.okButton.Location = new System.Drawing.Point(0, 3);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 28);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "ปิด";
            this.okButton.UseVisualStyleBackColor = false;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(211, 117, 191);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.ForeColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(623, 307);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(10, 10);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(5, 0, 5, 5);
            this.panel2.Size = new System.Drawing.Size(603, 287);
            this.panel2.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.changeAmountTextBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.repaidDataGridView);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(5, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(593, 248);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Red;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(581, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "ระบบได้ทำการยกเลิกใบสำคัญจ่ายเรียบร้อยแล้ว โปรดเรียกเก็บเงินจากผู้เบิกตามรายละเอี" +
                "ยดข้างล่าง";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // changeAmountTextBox
            // 
            this.changeAmountTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.changeAmountTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.changeAmountTextBox.ForeColor = System.Drawing.Color.Red;
            this.changeAmountTextBox.Location = new System.Drawing.Point(327, 50);
            this.changeAmountTextBox.Name = "changeAmountTextBox";
            this.changeAmountTextBox.ReadOnly = true;
            this.changeAmountTextBox.Size = new System.Drawing.Size(260, 44);
            this.changeAmountTextBox.TabIndex = 6;
            this.changeAmountTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ivDescColumn
            // 
            this.ivDescColumn.DataPropertyName = "Description";
            this.ivDescColumn.FillWeight = 1F;
            this.ivDescColumn.HeaderText = "รายละเอียดเงินที่ต้องได้รับคืนจากผู้เบิก";
            this.ivDescColumn.MinimumWidth = 400;
            this.ivDescColumn.Name = "ivDescColumn";
            this.ivDescColumn.ReadOnly = true;
            this.ivDescColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ivAmountColumn
            // 
            this.ivAmountColumn.DataPropertyName = "DisplayAmount";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N2";
            this.ivAmountColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.ivAmountColumn.FillWeight = 1F;
            this.ivAmountColumn.HeaderText = "จำนวนเงิน";
            this.ivAmountColumn.MinimumWidth = 90;
            this.ivAmountColumn.Name = "ivAmountColumn";
            this.ivAmountColumn.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.FillWeight = 1000F;
            this.Column5.HeaderText = "";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // CancelPaymentVoucherForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.okButton;
            this.ClientSize = new System.Drawing.Size(623, 307);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CancelPaymentVoucherForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "สรุปการยกเลิกใบสำคัญจ่าย";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CancelPaymentVoucherForm_KeyPress);
            this.Load += new System.EventHandler(this.CancelRepamentForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.repaidDataGridView)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.DataGridView repaidDataGridView;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox changeAmountTextBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn ivDescColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ivAmountColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
    }
}