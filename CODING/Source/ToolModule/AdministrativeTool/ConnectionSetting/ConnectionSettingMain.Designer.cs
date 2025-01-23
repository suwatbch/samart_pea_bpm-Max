namespace AdministrativeTool.ConnectionSetting
{
    partial class ConnectionSettingMain
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
            this.dataGridViewConnectionSetting = new System.Windows.Forms.DataGridView();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.pnButton = new System.Windows.Forms.Panel();
            this.labTotal = new System.Windows.Forms.Label();
            this.pnFilter = new System.Windows.Forms.Panel();
            this.tbxBranchId = new System.Windows.Forms.TextBox();
            this.labBranchId = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewConnectionSetting)).BeginInit();
            this.pnButton.SuspendLayout();
            this.pnFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewConnectionSetting
            // 
            this.dataGridViewConnectionSetting.AllowUserToAddRows = false;
            this.dataGridViewConnectionSetting.AllowUserToDeleteRows = false;
            this.dataGridViewConnectionSetting.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewConnectionSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewConnectionSetting.Location = new System.Drawing.Point(0, 32);
            this.dataGridViewConnectionSetting.Name = "dataGridViewConnectionSetting";
            this.dataGridViewConnectionSetting.ReadOnly = true;
            this.dataGridViewConnectionSetting.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewConnectionSetting.Size = new System.Drawing.Size(734, 466);
            this.dataGridViewConnectionSetting.TabIndex = 1;
            this.dataGridViewConnectionSetting.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewConnectionSetting_CellMouseDoubleClick);
            this.dataGridViewConnectionSetting.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewConnectionSetting_KeyDown);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(656, 6);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Location = new System.Drawing.Point(575, 6);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 2;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(494, 6);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(198, 5);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(106, 23);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "OK";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // pnButton
            // 
            this.pnButton.Controls.Add(this.labTotal);
            this.pnButton.Controls.Add(this.btnDelete);
            this.pnButton.Controls.Add(this.btnEdit);
            this.pnButton.Controls.Add(this.btnAdd);
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
            this.labTotal.Location = new System.Drawing.Point(3, 10);
            this.labTotal.Name = "labTotal";
            this.labTotal.Size = new System.Drawing.Size(89, 14);
            this.labTotal.TabIndex = 0;
            this.labTotal.Text = "Total Record : 0";
            // 
            // pnFilter
            // 
            this.pnFilter.Controls.Add(this.btnRefresh);
            this.pnFilter.Controls.Add(this.tbxBranchId);
            this.pnFilter.Controls.Add(this.labBranchId);
            this.pnFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnFilter.Location = new System.Drawing.Point(0, 0);
            this.pnFilter.Name = "pnFilter";
            this.pnFilter.Size = new System.Drawing.Size(734, 32);
            this.pnFilter.TabIndex = 0;
            // 
            // tbxBranchId
            // 
            this.tbxBranchId.Location = new System.Drawing.Point(92, 7);
            this.tbxBranchId.MaxLength = 6;
            this.tbxBranchId.Name = "tbxBranchId";
            this.tbxBranchId.Size = new System.Drawing.Size(100, 20);
            this.tbxBranchId.TabIndex = 1;
            // 
            // labBranchId
            // 
            this.labBranchId.AutoSize = true;
            this.labBranchId.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labBranchId.Location = new System.Drawing.Point(18, 10);
            this.labBranchId.Name = "labBranchId";
            this.labBranchId.Size = new System.Drawing.Size(68, 14);
            this.labBranchId.TabIndex = 0;
            this.labBranchId.Text = "Branch ID : ";
            // 
            // ConnectionSettingMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridViewConnectionSetting);
            this.Controls.Add(this.pnFilter);
            this.Controls.Add(this.pnButton);
            this.Name = "ConnectionSettingMain";
            this.Size = new System.Drawing.Size(734, 530);
            this.Load += new System.EventHandler(this.ConnectionSetting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewConnectionSetting)).EndInit();
            this.pnButton.ResumeLayout(false);
            this.pnButton.PerformLayout();
            this.pnFilter.ResumeLayout(false);
            this.pnFilter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewConnectionSetting;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel pnButton;
        private System.Windows.Forms.Panel pnFilter;
        private System.Windows.Forms.Label labTotal;
        private System.Windows.Forms.Label labBranchId;
        private System.Windows.Forms.TextBox tbxBranchId;
    }
}
