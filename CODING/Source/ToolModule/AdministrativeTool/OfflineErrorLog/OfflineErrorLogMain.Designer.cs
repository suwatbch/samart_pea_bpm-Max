namespace AdministrativeTool.OfflineErrorLog
{
    partial class OfflineErrorLogMain
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
            this.pnButton = new System.Windows.Forms.Panel();
            this.labTotal = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dataGridViewOfflineErrorLog = new System.Windows.Forms.DataGridView();
            this.pnFilter = new System.Windows.Forms.Panel();
            this.labDateTime = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.pnButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOfflineErrorLog)).BeginInit();
            this.pnFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnButton
            // 
            this.pnButton.Controls.Add(this.labTotal);
            this.pnButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnButton.Location = new System.Drawing.Point(0, 498);
            this.pnButton.Name = "pnButton";
            this.pnButton.Size = new System.Drawing.Size(734, 32);
            this.pnButton.TabIndex = 2;
            // 
            // labTotal
            // 
            this.labTotal.AutoSize = true;
            this.labTotal.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labTotal.Location = new System.Drawing.Point(7, 10);
            this.labTotal.Name = "labTotal";
            this.labTotal.Size = new System.Drawing.Size(89, 14);
            this.labTotal.TabIndex = 0;
            this.labTotal.Text = "Total Record : 0";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(332, 5);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(105, 23);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "OK";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // dataGridViewOfflineErrorLog
            // 
            this.dataGridViewOfflineErrorLog.AllowUserToAddRows = false;
            this.dataGridViewOfflineErrorLog.AllowUserToDeleteRows = false;
            this.dataGridViewOfflineErrorLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOfflineErrorLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewOfflineErrorLog.Location = new System.Drawing.Point(0, 32);
            this.dataGridViewOfflineErrorLog.MultiSelect = false;
            this.dataGridViewOfflineErrorLog.Name = "dataGridViewOfflineErrorLog";
            this.dataGridViewOfflineErrorLog.ReadOnly = true;
            this.dataGridViewOfflineErrorLog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridViewOfflineErrorLog.Size = new System.Drawing.Size(734, 466);
            this.dataGridViewOfflineErrorLog.TabIndex = 1;
            // 
            // pnFilter
            // 
            this.pnFilter.Controls.Add(this.btnRefresh);
            this.pnFilter.Controls.Add(this.labDateTime);
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
            this.labDateTime.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labDateTime.Location = new System.Drawing.Point(6, 9);
            this.labDateTime.Name = "labDateTime";
            this.labDateTime.Size = new System.Drawing.Size(69, 14);
            this.labDateTime.TabIndex = 0;
            this.labDateTime.Text = "DateTime : ";
            // 
            // dtpDate
            // 
            this.dtpDate.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDate.Location = new System.Drawing.Point(72, 6);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(228, 22);
            this.dtpDate.TabIndex = 1;
            this.dtpDate.ValueChanged += new System.EventHandler(this.dtpDate_ValueChanged);
            // 
            // OfflineErrorLogMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridViewOfflineErrorLog);
            this.Controls.Add(this.pnFilter);
            this.Controls.Add(this.pnButton);
            this.Name = "OfflineErrorLogMain";
            this.Size = new System.Drawing.Size(734, 530);
            this.Load += new System.EventHandler(this.OfflineErrorLogMain_Load);
            this.pnButton.ResumeLayout(false);
            this.pnButton.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOfflineErrorLog)).EndInit();
            this.pnFilter.ResumeLayout(false);
            this.pnFilter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnButton;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dataGridViewOfflineErrorLog;
        private System.Windows.Forms.Panel pnFilter;
        private System.Windows.Forms.Label labDateTime;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label labTotal;
    }
}
