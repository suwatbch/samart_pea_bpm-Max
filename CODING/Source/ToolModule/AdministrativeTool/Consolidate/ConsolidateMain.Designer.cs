namespace AdministrativeTool.Consolidate
{
    partial class ConsolidateMain
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
            this.dataGridViewConsolidate = new System.Windows.Forms.DataGridView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.pnButton = new System.Windows.Forms.Panel();
            this.labTotal = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.pnFilter = new System.Windows.Forms.Panel();
            this.labDateTime = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewConsolidate)).BeginInit();
            this.pnButton.SuspendLayout();
            this.pnFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewConsolidate
            // 
            this.dataGridViewConsolidate.AllowUserToAddRows = false;
            this.dataGridViewConsolidate.AllowUserToDeleteRows = false;
            this.dataGridViewConsolidate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewConsolidate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewConsolidate.Location = new System.Drawing.Point(0, 32);
            this.dataGridViewConsolidate.MultiSelect = false;
            this.dataGridViewConsolidate.Name = "dataGridViewConsolidate";
            this.dataGridViewConsolidate.ReadOnly = true;
            this.dataGridViewConsolidate.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewConsolidate.Size = new System.Drawing.Size(734, 466);
            this.dataGridViewConsolidate.TabIndex = 1;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(212, 5);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(101, 23);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "แสดงข้อมูล";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // pnButton
            // 
            this.pnButton.Controls.Add(this.labTotal);
            this.pnButton.Controls.Add(this.btnExport);
            this.pnButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnButton.Location = new System.Drawing.Point(0, 498);
            this.pnButton.Name = "pnButton";
            this.pnButton.Size = new System.Drawing.Size(734, 32);
            this.pnButton.TabIndex = 2;
            // 
            // labTotal
            // 
            this.labTotal.AutoSize = true;
            this.labTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.labTotal.Location = new System.Drawing.Point(3, 10);
            this.labTotal.Name = "labTotal";
            this.labTotal.Size = new System.Drawing.Size(100, 13);
            this.labTotal.TabIndex = 0;
            this.labTotal.Text = "Total Record : 0";
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Location = new System.Drawing.Point(656, 6);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 1;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // pnFilter
            // 
            this.pnFilter.Controls.Add(this.labDateTime);
            this.pnFilter.Controls.Add(this.btnRefresh);
            this.pnFilter.Controls.Add(this.dtpDate);
            this.pnFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnFilter.Location = new System.Drawing.Point(0, 0);
            this.pnFilter.Name = "pnFilter";
            this.pnFilter.Size = new System.Drawing.Size(734, 32);
            this.pnFilter.TabIndex = 0;
            // 
            // labDateTime
            // 
            this.labDateTime.AutoSize = true;
            this.labDateTime.Location = new System.Drawing.Point(3, 10);
            this.labDateTime.Name = "labDateTime";
            this.labDateTime.Size = new System.Drawing.Size(62, 13);
            this.labDateTime.TabIndex = 0;
            this.labDateTime.Text = "DateTime : ";
            // 
            // dtpDate
            // 
            this.dtpDate.Location = new System.Drawing.Point(71, 6);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(135, 20);
            this.dtpDate.TabIndex = 1;
            this.dtpDate.ValueChanged += new System.EventHandler(this.dtpDate_ValueChanged);
            // 
            // ConsolidateMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridViewConsolidate);
            this.Controls.Add(this.pnButton);
            this.Controls.Add(this.pnFilter);
            this.Name = "ConsolidateMain";
            this.Size = new System.Drawing.Size(734, 530);
            this.Load += new System.EventHandler(this.ConsolidateMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewConsolidate)).EndInit();
            this.pnButton.ResumeLayout(false);
            this.pnButton.PerformLayout();
            this.pnFilter.ResumeLayout(false);
            this.pnFilter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewConsolidate;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel pnButton;
        private System.Windows.Forms.Panel pnFilter;
        private System.Windows.Forms.Label labDateTime;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label labTotal;
    }
}
