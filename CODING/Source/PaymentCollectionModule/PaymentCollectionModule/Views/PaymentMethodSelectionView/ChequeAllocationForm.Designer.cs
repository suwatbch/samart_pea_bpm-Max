namespace PEA.BPM.PaymentCollectionModule.Views.PaymentMethodSelectionView
{
    partial class ChequeAllocationForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pmDownButton = new System.Windows.Forms.Button();
            this.pmUpButton = new System.Windows.Forms.Button();
            this.ivDownButton = new System.Windows.Forms.Button();
            this.ivUpButton = new System.Windows.Forms.Button();
            this.paymentMethodDataGridView = new System.Windows.Forms.DataGridView();
            this.pmDescColumn = new System.Windows.Forms.DataGridViewLinkColumn();
            this.pmAmountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.invoiceDataGridView = new System.Windows.Forms.DataGridView();
            this.ivDescColumn = new System.Windows.Forms.DataGridViewLinkColumn();
            this.ivAmountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paymentMethodDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.invoiceDataGridView)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(211, 117, 191);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(608, 361);
            this.panel1.TabIndex = 0;
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
            this.panel2.Size = new System.Drawing.Size(588, 341);
            this.panel2.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pmDownButton);
            this.groupBox1.Controls.Add(this.pmUpButton);
            this.groupBox1.Controls.Add(this.ivDownButton);
            this.groupBox1.Controls.Add(this.ivUpButton);
            this.groupBox1.Controls.Add(this.paymentMethodDataGridView);
            this.groupBox1.Controls.Add(this.invoiceDataGridView);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(5, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(578, 302);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // pmDownButton
            // 
            this.pmDownButton.Image = global::PEA.BPM.PaymentCollectionModule.Properties.Resources.DownArrow;
            this.pmDownButton.Location = new System.Drawing.Point(544, 205);
            this.pmDownButton.Name = "pmDownButton";
            this.pmDownButton.Size = new System.Drawing.Size(23, 23);
            this.pmDownButton.TabIndex = 5;
            this.pmDownButton.UseVisualStyleBackColor = true;
            this.pmDownButton.Click += new System.EventHandler(this.pmDownButton_Click);
            // 
            // pmUpButton
            // 
            this.pmUpButton.Image = global::PEA.BPM.PaymentCollectionModule.Properties.Resources.UpArrow;
            this.pmUpButton.Location = new System.Drawing.Point(544, 176);
            this.pmUpButton.Name = "pmUpButton";
            this.pmUpButton.Size = new System.Drawing.Size(23, 23);
            this.pmUpButton.TabIndex = 4;
            this.pmUpButton.UseVisualStyleBackColor = true;
            this.pmUpButton.Click += new System.EventHandler(this.pmUpButton_Click);
            // 
            // ivDownButton
            // 
            this.ivDownButton.Image = global::PEA.BPM.PaymentCollectionModule.Properties.Resources.DownArrow;
            this.ivDownButton.Location = new System.Drawing.Point(544, 66);
            this.ivDownButton.Name = "ivDownButton";
            this.ivDownButton.Size = new System.Drawing.Size(23, 23);
            this.ivDownButton.TabIndex = 3;
            this.ivDownButton.UseVisualStyleBackColor = true;
            this.ivDownButton.Click += new System.EventHandler(this.ivDownButton_Click);
            // 
            // ivUpButton
            // 
            this.ivUpButton.Image = global::PEA.BPM.PaymentCollectionModule.Properties.Resources.UpArrow;
            this.ivUpButton.Location = new System.Drawing.Point(544, 37);
            this.ivUpButton.Name = "ivUpButton";
            this.ivUpButton.Size = new System.Drawing.Size(23, 23);
            this.ivUpButton.TabIndex = 2;
            this.ivUpButton.UseVisualStyleBackColor = true;
            this.ivUpButton.Click += new System.EventHandler(this.ivUpButton_Click);
            // 
            // paymentMethodDataGridView
            // 
            this.paymentMethodDataGridView.AllowUserToAddRows = false;
            this.paymentMethodDataGridView.AllowUserToDeleteRows = false;
            this.paymentMethodDataGridView.AllowUserToResizeRows = false;
            this.paymentMethodDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.paymentMethodDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.paymentMethodDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.paymentMethodDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.pmDescColumn,
            this.pmAmountColumn,
            this.dataGridViewTextBoxColumn3});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(211, 117, 191);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.paymentMethodDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.paymentMethodDataGridView.EnableHeadersVisualStyles = false;
            this.paymentMethodDataGridView.Location = new System.Drawing.Point(11, 158);
            this.paymentMethodDataGridView.Name = "paymentMethodDataGridView";
            this.paymentMethodDataGridView.ReadOnly = true;
            this.paymentMethodDataGridView.RowHeadersVisible = false;
            this.paymentMethodDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.paymentMethodDataGridView.Size = new System.Drawing.Size(527, 133);
            this.paymentMethodDataGridView.TabIndex = 1;
            this.paymentMethodDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.paymentMethodDataGridView_CellContentClick);
            // 
            // pmDescColumn
            // 
            this.pmDescColumn.DataPropertyName = "Description";
            this.pmDescColumn.FillWeight = 1F;
            this.pmDescColumn.HeaderText = "ลำดับเช็คที่ใช้ตัดชำระรายการหนี้";
            this.pmDescColumn.MinimumWidth = 400;
            this.pmDescColumn.Name = "pmDescColumn";
            this.pmDescColumn.ReadOnly = true;
            this.pmDescColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.pmDescColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // pmAmountColumn
            // 
            this.pmAmountColumn.DataPropertyName = "Amount";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N2";
            this.pmAmountColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.pmAmountColumn.FillWeight = 1F;
            this.pmAmountColumn.HeaderText = "จำนวนเงิน";
            this.pmAmountColumn.MinimumWidth = 90;
            this.pmAmountColumn.Name = "pmAmountColumn";
            this.pmAmountColumn.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.FillWeight = 1000F;
            this.dataGridViewTextBoxColumn3.HeaderText = "";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // invoiceDataGridView
            // 
            this.invoiceDataGridView.AllowUserToAddRows = false;
            this.invoiceDataGridView.AllowUserToDeleteRows = false;
            this.invoiceDataGridView.AllowUserToResizeRows = false;
            this.invoiceDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.invoiceDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.invoiceDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.invoiceDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ivDescColumn,
            this.ivAmountColumn,
            this.Column5});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(211, 117, 191);
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.invoiceDataGridView.DefaultCellStyle = dataGridViewCellStyle4;
            this.invoiceDataGridView.EnableHeadersVisualStyles = false;
            this.invoiceDataGridView.Location = new System.Drawing.Point(11, 19);
            this.invoiceDataGridView.Name = "invoiceDataGridView";
            this.invoiceDataGridView.ReadOnly = true;
            this.invoiceDataGridView.RowHeadersVisible = false;
            this.invoiceDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.invoiceDataGridView.Size = new System.Drawing.Size(527, 133);
            this.invoiceDataGridView.TabIndex = 0;
            this.invoiceDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.invoiceDataGridView_CellContentClick);
            // 
            // ivDescColumn
            // 
            this.ivDescColumn.DataPropertyName = "Description";
            this.ivDescColumn.FillWeight = 1F;
            this.ivDescColumn.HeaderText = "ลำดับการตัดชำระรายการหนี้ด้วยเช็ค";
            this.ivDescColumn.MinimumWidth = 400;
            this.ivDescColumn.Name = "ivDescColumn";
            this.ivDescColumn.ReadOnly = true;
            this.ivDescColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ivDescColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // ivAmountColumn
            // 
            this.ivAmountColumn.DataPropertyName = "Amount";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            this.ivAmountColumn.DefaultCellStyle = dataGridViewCellStyle3;
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
            // panel3
            // 
            this.panel3.Controls.Add(this.cancelButton);
            this.panel3.Controls.Add(this.okButton);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.panel3.Location = new System.Drawing.Point(5, 302);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(578, 34);
            this.panel3.TabIndex = 1;
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(81, 3);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 30);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "ยกเลิก";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // okButton
            // 
            this.okButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.okButton.Location = new System.Drawing.Point(0, 3);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 30);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "ตกลง";
            this.okButton.UseVisualStyleBackColor = false;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // ChequeAllocationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(608, 361);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ChequeAllocationForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = " จัดการข้อมูลการชำระรายการหนี้ด้วยเช็ค";
            this.Shown += new System.EventHandler(this.ChequeAllocationForm_Shown);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.paymentMethodDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.invoiceDataGridView)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView paymentMethodDataGridView;
        private System.Windows.Forms.DataGridView invoiceDataGridView;
        private System.Windows.Forms.Button ivDownButton;
        private System.Windows.Forms.Button ivUpButton;
        private System.Windows.Forms.Button pmDownButton;
        private System.Windows.Forms.Button pmUpButton;
        private System.Windows.Forms.DataGridViewLinkColumn ivDescColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ivAmountColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewLinkColumn pmDescColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pmAmountColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    }
}