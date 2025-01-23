namespace AdministrativeTool.CloseAccountStatus
{
    partial class CloseAccountStatusMain
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnFilter = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.regionCBox = new System.Windows.Forms.ComboBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.comboStatus = new System.Windows.Forms.ComboBox();
            this.labSatus = new System.Windows.Forms.Label();
            this.labDateTime = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.dataGridViewCloseAccountStatus = new System.Windows.Forms.DataGridView();
            this.pnButton = new System.Windows.Forms.Panel();
            this.footerTxt = new System.Windows.Forms.Label();
            this.labTotal = new System.Windows.Forms.Label();
            this.pnFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCloseAccountStatus)).BeginInit();
            this.pnButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnFilter
            // 
            this.pnFilter.Controls.Add(this.label1);
            this.pnFilter.Controls.Add(this.regionCBox);
            this.pnFilter.Controls.Add(this.btnRefresh);
            this.pnFilter.Controls.Add(this.comboStatus);
            this.pnFilter.Controls.Add(this.labSatus);
            this.pnFilter.Controls.Add(this.labDateTime);
            this.pnFilter.Controls.Add(this.dtpDate);
            this.pnFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnFilter.Location = new System.Drawing.Point(0, 0);
            this.pnFilter.Name = "pnFilter";
            this.pnFilter.Size = new System.Drawing.Size(907, 32);
            this.pnFilter.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(312, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 14);
            this.label1.TabIndex = 6;
            this.label1.Text = "Region : ";
            // 
            // regionCBox
            // 
            this.regionCBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.regionCBox.FormattingEnabled = true;
            this.regionCBox.Items.AddRange(new object[] {
            "A00000",
            "B00000",
            "C00000",
            "D00000",
            "E00000",
            "F00000",
            "G00000",
            "H00000",
            "I00000",
            "J00000",
            "K00000",
            "L00000",
            "Z00000"});
            this.regionCBox.Location = new System.Drawing.Point(369, 5);
            this.regionCBox.Name = "regionCBox";
            this.regionCBox.Size = new System.Drawing.Size(108, 21);
            this.regionCBox.TabIndex = 5;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(651, 4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(133, 23);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "OK";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // comboStatus
            // 
            this.comboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboStatus.FormattingEnabled = true;
            this.comboStatus.Location = new System.Drawing.Point(535, 5);
            this.comboStatus.Name = "comboStatus";
            this.comboStatus.Size = new System.Drawing.Size(108, 21);
            this.comboStatus.TabIndex = 3;
            this.comboStatus.SelectionChangeCommitted += new System.EventHandler(this.comboActive_SelectionChangeCommitted);
            // 
            // labSatus
            // 
            this.labSatus.AutoSize = true;
            this.labSatus.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labSatus.Location = new System.Drawing.Point(485, 9);
            this.labSatus.Name = "labSatus";
            this.labSatus.Size = new System.Drawing.Size(50, 14);
            this.labSatus.TabIndex = 2;
            this.labSatus.Text = "Status : ";
            // 
            // labDateTime
            // 
            this.labDateTime.AutoSize = true;
            this.labDateTime.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labDateTime.Location = new System.Drawing.Point(3, 10);
            this.labDateTime.Name = "labDateTime";
            this.labDateTime.Size = new System.Drawing.Size(69, 14);
            this.labDateTime.TabIndex = 0;
            this.labDateTime.Text = "DateTime : ";
            // 
            // dtpDate
            // 
            this.dtpDate.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDate.Location = new System.Drawing.Point(77, 5);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(215, 22);
            this.dtpDate.TabIndex = 1;
            this.dtpDate.ValueChanged += new System.EventHandler(this.dtpDate_ValueChanged);
            // 
            // dataGridViewCloseAccountStatus
            // 
            this.dataGridViewCloseAccountStatus.AllowUserToAddRows = false;
            this.dataGridViewCloseAccountStatus.AllowUserToDeleteRows = false;
            this.dataGridViewCloseAccountStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCloseAccountStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewCloseAccountStatus.Location = new System.Drawing.Point(0, 32);
            this.dataGridViewCloseAccountStatus.MultiSelect = false;
            this.dataGridViewCloseAccountStatus.Name = "dataGridViewCloseAccountStatus";
            this.dataGridViewCloseAccountStatus.ReadOnly = true;
            this.dataGridViewCloseAccountStatus.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewCloseAccountStatus.Size = new System.Drawing.Size(907, 466);
            this.dataGridViewCloseAccountStatus.TabIndex = 1;
            // 
            // pnButton
            // 
            this.pnButton.Controls.Add(this.footerTxt);
            this.pnButton.Controls.Add(this.labTotal);
            this.pnButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnButton.Location = new System.Drawing.Point(0, 498);
            this.pnButton.Name = "pnButton";
            this.pnButton.Size = new System.Drawing.Size(907, 32);
            this.pnButton.TabIndex = 2;
            // 
            // footerTxt
            // 
            this.footerTxt.AutoSize = true;
            this.footerTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.footerTxt.Location = new System.Drawing.Point(163, 10);
            this.footerTxt.Name = "footerTxt";
            this.footerTxt.Size = new System.Drawing.Size(0, 13);
            this.footerTxt.TabIndex = 2;
            // 
            // labTotal
            // 
            this.labTotal.AutoSize = true;
            this.labTotal.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labTotal.Location = new System.Drawing.Point(11, 10);
            this.labTotal.Name = "labTotal";
            this.labTotal.Size = new System.Drawing.Size(89, 14);
            this.labTotal.TabIndex = 1;
            this.labTotal.Text = "Total Record : 0";
            // 
            // CloseAccountStatusMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridViewCloseAccountStatus);
            this.Controls.Add(this.pnFilter);
            this.Controls.Add(this.pnButton);
            this.Name = "CloseAccountStatusMain";
            this.Size = new System.Drawing.Size(907, 530);
            this.Load += new System.EventHandler(this.CloseAccountStatusMain_Load);
            this.pnFilter.ResumeLayout(false);
            this.pnFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCloseAccountStatus)).EndInit();
            this.pnButton.ResumeLayout(false);
            this.pnButton.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnFilter;
        private System.Windows.Forms.Label labDateTime;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.DataGridView dataGridViewCloseAccountStatus;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel pnButton;
        private System.Windows.Forms.Label labSatus;
        private System.Windows.Forms.ComboBox comboStatus;
        private System.Windows.Forms.Label labTotal;
        private System.Windows.Forms.Label footerTxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox regionCBox;

    }
}
