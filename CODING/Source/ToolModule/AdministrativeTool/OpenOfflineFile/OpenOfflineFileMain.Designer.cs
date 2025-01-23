namespace AdministrativeTool.OpenOfflineFile
{
    partial class OpenOfflineFileMain
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
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.pnFilter = new System.Windows.Forms.Panel();
            this.txtFilename = new System.Windows.Forms.TextBox();
            this.grpPayment = new System.Windows.Forms.GroupBox();
            this.txtPaymentDate = new System.Windows.Forms.TextBox();
            this.labBranchId = new System.Windows.Forms.Label();
            this.labCashierId = new System.Windows.Forms.Label();
            this.labPaymentDate = new System.Windows.Forms.Label();
            this.txtBranchId = new System.Windows.Forms.TextBox();
            this.txtCashierId = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.labFilename = new System.Windows.Forms.Label();
            this.dataGridViewARPayment = new System.Windows.Forms.DataGridView();
            this.dataGridViewARPaymentType = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pnButton1 = new System.Windows.Forms.Panel();
            this.labTotal1 = new System.Windows.Forms.Label();
            this.labARPaymentType = new System.Windows.Forms.Label();
            this.pnButton2 = new System.Windows.Forms.Panel();
            this.labTotal2 = new System.Windows.Forms.Label();
            this.labARPayment = new System.Windows.Forms.Label();
            this.pnFilter.SuspendLayout();
            this.grpPayment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewARPayment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewARPaymentType)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pnButton1.SuspendLayout();
            this.pnButton2.SuspendLayout();
            this.SuspendLayout();
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // pnFilter
            // 
            this.pnFilter.Controls.Add(this.txtFilename);
            this.pnFilter.Controls.Add(this.grpPayment);
            this.pnFilter.Controls.Add(this.btnBrowse);
            this.pnFilter.Controls.Add(this.labFilename);
            this.pnFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnFilter.Location = new System.Drawing.Point(0, 0);
            this.pnFilter.Name = "pnFilter";
            this.pnFilter.Size = new System.Drawing.Size(734, 89);
            this.pnFilter.TabIndex = 0;
            // 
            // txtFilename
            // 
            this.txtFilename.BackColor = System.Drawing.Color.White;
            this.txtFilename.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilename.Location = new System.Drawing.Point(67, 7);
            this.txtFilename.Name = "txtFilename";
            this.txtFilename.ReadOnly = true;
            this.txtFilename.Size = new System.Drawing.Size(500, 22);
            this.txtFilename.TabIndex = 1;
            // 
            // grpPayment
            // 
            this.grpPayment.Controls.Add(this.txtPaymentDate);
            this.grpPayment.Controls.Add(this.labBranchId);
            this.grpPayment.Controls.Add(this.labCashierId);
            this.grpPayment.Controls.Add(this.labPaymentDate);
            this.grpPayment.Controls.Add(this.txtBranchId);
            this.grpPayment.Controls.Add(this.txtCashierId);
            this.grpPayment.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpPayment.Location = new System.Drawing.Point(3, 33);
            this.grpPayment.Name = "grpPayment";
            this.grpPayment.Size = new System.Drawing.Size(645, 50);
            this.grpPayment.TabIndex = 3;
            this.grpPayment.TabStop = false;
            this.grpPayment.Text = "Payment";
            // 
            // txtPaymentDate
            // 
            this.txtPaymentDate.BackColor = System.Drawing.Color.White;
            this.txtPaymentDate.Location = new System.Drawing.Point(95, 20);
            this.txtPaymentDate.Name = "txtPaymentDate";
            this.txtPaymentDate.ReadOnly = true;
            this.txtPaymentDate.Size = new System.Drawing.Size(135, 22);
            this.txtPaymentDate.TabIndex = 1;
            // 
            // labBranchId
            // 
            this.labBranchId.AutoSize = true;
            this.labBranchId.Location = new System.Drawing.Point(469, 22);
            this.labBranchId.Name = "labBranchId";
            this.labBranchId.Size = new System.Drawing.Size(68, 14);
            this.labBranchId.TabIndex = 4;
            this.labBranchId.Text = "Branch ID : ";
            // 
            // labCashierId
            // 
            this.labCashierId.AutoSize = true;
            this.labCashierId.Location = new System.Drawing.Point(262, 23);
            this.labCashierId.Name = "labCashierId";
            this.labCashierId.Size = new System.Drawing.Size(72, 14);
            this.labCashierId.TabIndex = 2;
            this.labCashierId.Text = "Cashier ID : ";
            // 
            // labPaymentDate
            // 
            this.labPaymentDate.AutoSize = true;
            this.labPaymentDate.Location = new System.Drawing.Point(6, 23);
            this.labPaymentDate.Name = "labPaymentDate";
            this.labPaymentDate.Size = new System.Drawing.Size(91, 14);
            this.labPaymentDate.TabIndex = 0;
            this.labPaymentDate.Text = "Payment Date : ";
            // 
            // txtBranchId
            // 
            this.txtBranchId.BackColor = System.Drawing.Color.White;
            this.txtBranchId.Location = new System.Drawing.Point(539, 20);
            this.txtBranchId.Name = "txtBranchId";
            this.txtBranchId.ReadOnly = true;
            this.txtBranchId.Size = new System.Drawing.Size(100, 22);
            this.txtBranchId.TabIndex = 5;
            // 
            // txtCashierId
            // 
            this.txtCashierId.BackColor = System.Drawing.Color.White;
            this.txtCashierId.Location = new System.Drawing.Point(333, 20);
            this.txtCashierId.Name = "txtCashierId";
            this.txtCashierId.ReadOnly = true;
            this.txtCashierId.Size = new System.Drawing.Size(100, 22);
            this.txtCashierId.TabIndex = 3;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse.Location = new System.Drawing.Point(573, 5);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // labFilename
            // 
            this.labFilename.AutoSize = true;
            this.labFilename.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labFilename.Location = new System.Drawing.Point(3, 10);
            this.labFilename.Name = "labFilename";
            this.labFilename.Size = new System.Drawing.Size(68, 14);
            this.labFilename.TabIndex = 0;
            this.labFilename.Text = "Filename : ";
            // 
            // dataGridViewARPayment
            // 
            this.dataGridViewARPayment.AllowUserToAddRows = false;
            this.dataGridViewARPayment.AllowUserToDeleteRows = false;
            this.dataGridViewARPayment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewARPayment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewARPayment.Location = new System.Drawing.Point(0, 20);
            this.dataGridViewARPayment.MultiSelect = false;
            this.dataGridViewARPayment.Name = "dataGridViewARPayment";
            this.dataGridViewARPayment.ReadOnly = true;
            this.dataGridViewARPayment.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewARPayment.Size = new System.Drawing.Size(734, 141);
            this.dataGridViewARPayment.TabIndex = 1;
            // 
            // dataGridViewARPaymentType
            // 
            this.dataGridViewARPaymentType.AllowUserToAddRows = false;
            this.dataGridViewARPaymentType.AllowUserToDeleteRows = false;
            this.dataGridViewARPaymentType.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewARPaymentType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewARPaymentType.Location = new System.Drawing.Point(0, 20);
            this.dataGridViewARPaymentType.MultiSelect = false;
            this.dataGridViewARPaymentType.Name = "dataGridViewARPaymentType";
            this.dataGridViewARPaymentType.ReadOnly = true;
            this.dataGridViewARPaymentType.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewARPaymentType.Size = new System.Drawing.Size(734, 192);
            this.dataGridViewARPaymentType.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 89);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridViewARPaymentType);
            this.splitContainer1.Panel1.Controls.Add(this.pnButton1);
            this.splitContainer1.Panel1.Controls.Add(this.labARPaymentType);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridViewARPayment);
            this.splitContainer1.Panel2.Controls.Add(this.pnButton2);
            this.splitContainer1.Panel2.Controls.Add(this.labARPayment);
            this.splitContainer1.Size = new System.Drawing.Size(734, 441);
            this.splitContainer1.SplitterDistance = 244;
            this.splitContainer1.TabIndex = 6;
            // 
            // pnButton1
            // 
            this.pnButton1.Controls.Add(this.labTotal1);
            this.pnButton1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnButton1.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnButton1.Location = new System.Drawing.Point(0, 212);
            this.pnButton1.Name = "pnButton1";
            this.pnButton1.Size = new System.Drawing.Size(734, 32);
            this.pnButton1.TabIndex = 2;
            // 
            // labTotal1
            // 
            this.labTotal1.AutoSize = true;
            this.labTotal1.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labTotal1.Location = new System.Drawing.Point(7, 10);
            this.labTotal1.Name = "labTotal1";
            this.labTotal1.Size = new System.Drawing.Size(89, 14);
            this.labTotal1.TabIndex = 0;
            this.labTotal1.Text = "Total Record : 0";
            // 
            // labARPaymentType
            // 
            this.labARPaymentType.BackColor = System.Drawing.SystemColors.Control;
            this.labARPaymentType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labARPaymentType.Dock = System.Windows.Forms.DockStyle.Top;
            this.labARPaymentType.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labARPaymentType.Location = new System.Drawing.Point(0, 0);
            this.labARPaymentType.Name = "labARPaymentType";
            this.labARPaymentType.Size = new System.Drawing.Size(734, 20);
            this.labARPaymentType.TabIndex = 0;
            this.labARPaymentType.Text = "ARPaymentType";
            this.labARPaymentType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnButton2
            // 
            this.pnButton2.Controls.Add(this.labTotal2);
            this.pnButton2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnButton2.Location = new System.Drawing.Point(0, 161);
            this.pnButton2.Name = "pnButton2";
            this.pnButton2.Size = new System.Drawing.Size(734, 32);
            this.pnButton2.TabIndex = 2;
            // 
            // labTotal2
            // 
            this.labTotal2.AutoSize = true;
            this.labTotal2.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labTotal2.Location = new System.Drawing.Point(7, 10);
            this.labTotal2.Name = "labTotal2";
            this.labTotal2.Size = new System.Drawing.Size(89, 14);
            this.labTotal2.TabIndex = 0;
            this.labTotal2.Text = "Total Record : 0";
            // 
            // labARPayment
            // 
            this.labARPayment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labARPayment.Dock = System.Windows.Forms.DockStyle.Top;
            this.labARPayment.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labARPayment.Location = new System.Drawing.Point(0, 0);
            this.labARPayment.Name = "labARPayment";
            this.labARPayment.Size = new System.Drawing.Size(734, 20);
            this.labARPayment.TabIndex = 0;
            this.labARPayment.Text = "ARPayment";
            this.labARPayment.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OpenOfflineFileMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.pnFilter);
            this.Name = "OpenOfflineFileMain";
            this.Size = new System.Drawing.Size(734, 530);
            this.Load += new System.EventHandler(this.OpenOfflineFileMain_Load);
            this.pnFilter.ResumeLayout(false);
            this.pnFilter.PerformLayout();
            this.grpPayment.ResumeLayout(false);
            this.grpPayment.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewARPayment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewARPaymentType)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.pnButton1.ResumeLayout(false);
            this.pnButton1.PerformLayout();
            this.pnButton2.ResumeLayout(false);
            this.pnButton2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Panel pnFilter;
        private System.Windows.Forms.Label labFilename;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.DataGridView dataGridViewARPayment;
        private System.Windows.Forms.TextBox txtBranchId;
        private System.Windows.Forms.TextBox txtCashierId;
        private System.Windows.Forms.DataGridView dataGridViewARPaymentType;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox grpPayment;
        private System.Windows.Forms.Label labARPaymentType;
        private System.Windows.Forms.Label labARPayment;
        private System.Windows.Forms.TextBox txtFilename;
        private System.Windows.Forms.Label labBranchId;
        private System.Windows.Forms.Label labCashierId;
        private System.Windows.Forms.Label labPaymentDate;
        private System.Windows.Forms.TextBox txtPaymentDate;
        private System.Windows.Forms.Panel pnButton1;
        private System.Windows.Forms.Label labTotal1;
        private System.Windows.Forms.Panel pnButton2;
        private System.Windows.Forms.Label labTotal2;
    }
}
