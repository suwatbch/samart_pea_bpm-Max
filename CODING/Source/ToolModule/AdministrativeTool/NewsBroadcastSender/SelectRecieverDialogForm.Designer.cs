namespace AdministrativeTool.NewsBroadcastSender
{
    partial class SelectRecieverDialogForm
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
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonClose = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dataGridViewUserList = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.broadcastUserBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.areaInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.branchInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.branchInfoBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.broadcastIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.branchIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.branchName2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isOpenedDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.openedDtDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isReadDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.readDtDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.areaIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.roleIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.roleNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modifiedDtDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.activeDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUserList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.broadcastUserBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.areaInfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.branchInfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.branchInfoBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(240, 150);
            this.dataGridView1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(6, 500);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(866, 39);
            this.panel1.TabIndex = 3;
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(778, 7);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 1;
            this.buttonClose.Text = "ปิด";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dataGridViewUserList);
            this.groupBox4.Controls.Add(this.panel2);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(6, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(866, 500);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "ผู้ใช้ BPM :";
            // 
            // dataGridViewUserList
            // 
            this.dataGridViewUserList.AllowUserToAddRows = false;
            this.dataGridViewUserList.AllowUserToDeleteRows = false;
            this.dataGridViewUserList.AllowUserToOrderColumns = true;
            this.dataGridViewUserList.AllowUserToResizeRows = false;
            this.dataGridViewUserList.AutoGenerateColumns = false;
            this.dataGridViewUserList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewUserList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewUserList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.broadcastIdDataGridViewTextBoxColumn,
            this.branchIdDataGridViewTextBoxColumn,
            this.userIdDataGridViewTextBoxColumn,
            this.branchName2DataGridViewTextBoxColumn,
            this.isOpenedDataGridViewCheckBoxColumn,
            this.openedDtDataGridViewTextBoxColumn,
            this.isReadDataGridViewCheckBoxColumn,
            this.readDtDataGridViewTextBoxColumn,
            this.areaIdDataGridViewTextBoxColumn,
            this.roleIdDataGridViewTextBoxColumn,
            this.roleNameDataGridViewTextBoxColumn,
            this.modifiedDtDataGridViewTextBoxColumn,
            this.activeDataGridViewCheckBoxColumn});
            this.dataGridViewUserList.DataSource = this.broadcastUserBindingSource;
            this.dataGridViewUserList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewUserList.Location = new System.Drawing.Point(3, 19);
            this.dataGridViewUserList.Name = "dataGridViewUserList";
            this.dataGridViewUserList.ReadOnly = true;
            this.dataGridViewUserList.RowHeadersVisible = false;
            this.dataGridViewUserList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewUserList.Size = new System.Drawing.Size(860, 444);
            this.dataGridViewUserList.TabIndex = 0;
            this.dataGridViewUserList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewUserList_CellFormatting);
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 463);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(860, 34);
            this.panel2.TabIndex = 3;
            // 
            // broadcastUserBindingSource
            // 
            this.broadcastUserBindingSource.DataSource = typeof(PEA.BPM.NewsBroadcast.Interface.BusinessEntities.NewsBroadcastUserInfo);
            // 
            // areaInfoBindingSource
            // 
            this.areaInfoBindingSource.DataSource = typeof(PEA.BPM.NewsBroadcast.Interface.BusinessEntities.AreaInfo);
            // 
            // branchInfoBindingSource
            // 
            this.branchInfoBindingSource.DataSource = typeof(PEA.BPM.NewsBroadcast.Interface.BusinessEntities.BranchInfo);
            // 
            // branchInfoBindingSource1
            // 
            this.branchInfoBindingSource1.DataSource = typeof(PEA.BPM.NewsBroadcast.Interface.BusinessEntities.BranchInfo);
            // 
            // broadcastIdDataGridViewTextBoxColumn
            // 
            this.broadcastIdDataGridViewTextBoxColumn.DataPropertyName = "BroadcastId";
            this.broadcastIdDataGridViewTextBoxColumn.HeaderText = "Broadcast ID";
            this.broadcastIdDataGridViewTextBoxColumn.Name = "broadcastIdDataGridViewTextBoxColumn";
            this.broadcastIdDataGridViewTextBoxColumn.ReadOnly = true;
            this.broadcastIdDataGridViewTextBoxColumn.Visible = false;
            // 
            // branchIdDataGridViewTextBoxColumn
            // 
            this.branchIdDataGridViewTextBoxColumn.DataPropertyName = "BranchId";
            this.branchIdDataGridViewTextBoxColumn.FillWeight = 85.62521F;
            this.branchIdDataGridViewTextBoxColumn.HeaderText = "การไฟฟ้าที่สังกัด";
            this.branchIdDataGridViewTextBoxColumn.Name = "branchIdDataGridViewTextBoxColumn";
            this.branchIdDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // userIdDataGridViewTextBoxColumn
            // 
            this.userIdDataGridViewTextBoxColumn.DataPropertyName = "UserId";
            this.userIdDataGridViewTextBoxColumn.FillWeight = 77.51942F;
            this.userIdDataGridViewTextBoxColumn.HeaderText = "รหัสผู้ใช้";
            this.userIdDataGridViewTextBoxColumn.Name = "userIdDataGridViewTextBoxColumn";
            this.userIdDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // branchName2DataGridViewTextBoxColumn
            // 
            this.branchName2DataGridViewTextBoxColumn.DataPropertyName = "BranchName2";
            this.branchName2DataGridViewTextBoxColumn.FillWeight = 68.49777F;
            this.branchName2DataGridViewTextBoxColumn.HeaderText = "Server ที่เชื่อมต่อ";
            this.branchName2DataGridViewTextBoxColumn.Name = "branchName2DataGridViewTextBoxColumn";
            this.branchName2DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // isOpenedDataGridViewCheckBoxColumn
            // 
            this.isOpenedDataGridViewCheckBoxColumn.DataPropertyName = "IsOpened";
            this.isOpenedDataGridViewCheckBoxColumn.FillWeight = 104.5643F;
            this.isOpenedDataGridViewCheckBoxColumn.HeaderText = "Popup แสดงผลแล้ว";
            this.isOpenedDataGridViewCheckBoxColumn.Name = "isOpenedDataGridViewCheckBoxColumn";
            this.isOpenedDataGridViewCheckBoxColumn.ReadOnly = true;
            this.isOpenedDataGridViewCheckBoxColumn.Visible = false;
            // 
            // openedDtDataGridViewTextBoxColumn
            // 
            this.openedDtDataGridViewTextBoxColumn.DataPropertyName = "OpenedDt";
            this.openedDtDataGridViewTextBoxColumn.FillWeight = 157.3611F;
            this.openedDtDataGridViewTextBoxColumn.HeaderText = "วัน-เวลาที่ Popup แสดงผล";
            this.openedDtDataGridViewTextBoxColumn.Name = "openedDtDataGridViewTextBoxColumn";
            this.openedDtDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // isReadDataGridViewCheckBoxColumn
            // 
            this.isReadDataGridViewCheckBoxColumn.DataPropertyName = "IsRead";
            this.isReadDataGridViewCheckBoxColumn.FillWeight = 87.33495F;
            this.isReadDataGridViewCheckBoxColumn.HeaderText = "อ่านข้อความแล้ว";
            this.isReadDataGridViewCheckBoxColumn.Name = "isReadDataGridViewCheckBoxColumn";
            this.isReadDataGridViewCheckBoxColumn.ReadOnly = true;
            this.isReadDataGridViewCheckBoxColumn.Visible = false;
            // 
            // readDtDataGridViewTextBoxColumn
            // 
            this.readDtDataGridViewTextBoxColumn.DataPropertyName = "ReadDt";
            this.readDtDataGridViewTextBoxColumn.FillWeight = 157.3611F;
            this.readDtDataGridViewTextBoxColumn.HeaderText = "วัน-เวลาที่อ่านข้อความ";
            this.readDtDataGridViewTextBoxColumn.Name = "readDtDataGridViewTextBoxColumn";
            this.readDtDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // areaIdDataGridViewTextBoxColumn
            // 
            this.areaIdDataGridViewTextBoxColumn.DataPropertyName = "AreaId";
            this.areaIdDataGridViewTextBoxColumn.HeaderText = "AreaId";
            this.areaIdDataGridViewTextBoxColumn.Name = "areaIdDataGridViewTextBoxColumn";
            this.areaIdDataGridViewTextBoxColumn.ReadOnly = true;
            this.areaIdDataGridViewTextBoxColumn.Visible = false;
            // 
            // roleIdDataGridViewTextBoxColumn
            // 
            this.roleIdDataGridViewTextBoxColumn.DataPropertyName = "RoleId";
            this.roleIdDataGridViewTextBoxColumn.HeaderText = "RoleId";
            this.roleIdDataGridViewTextBoxColumn.Name = "roleIdDataGridViewTextBoxColumn";
            this.roleIdDataGridViewTextBoxColumn.ReadOnly = true;
            this.roleIdDataGridViewTextBoxColumn.Visible = false;
            // 
            // roleNameDataGridViewTextBoxColumn
            // 
            this.roleNameDataGridViewTextBoxColumn.DataPropertyName = "RoleName";
            this.roleNameDataGridViewTextBoxColumn.HeaderText = "RoleName";
            this.roleNameDataGridViewTextBoxColumn.Name = "roleNameDataGridViewTextBoxColumn";
            this.roleNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.roleNameDataGridViewTextBoxColumn.Visible = false;
            // 
            // modifiedDtDataGridViewTextBoxColumn
            // 
            this.modifiedDtDataGridViewTextBoxColumn.DataPropertyName = "ModifiedDt";
            this.modifiedDtDataGridViewTextBoxColumn.HeaderText = "ModifiedDt";
            this.modifiedDtDataGridViewTextBoxColumn.Name = "modifiedDtDataGridViewTextBoxColumn";
            this.modifiedDtDataGridViewTextBoxColumn.ReadOnly = true;
            this.modifiedDtDataGridViewTextBoxColumn.Visible = false;
            // 
            // activeDataGridViewCheckBoxColumn
            // 
            this.activeDataGridViewCheckBoxColumn.DataPropertyName = "Active";
            this.activeDataGridViewCheckBoxColumn.FillWeight = 61.73611F;
            this.activeDataGridViewCheckBoxColumn.HeaderText = "สถานะใช้งาน";
            this.activeDataGridViewCheckBoxColumn.Name = "activeDataGridViewCheckBoxColumn";
            this.activeDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // SelectRecieverDialogForm
            // 
            this.ClientSize = new System.Drawing.Size(872, 539);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.MinimumSize = new System.Drawing.Size(585, 200);
            this.Name = "SelectRecieverDialogForm";
            this.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "รายการผู้ใช้ระบบ BPM ที่ส่งถึง";
            this.Load += new System.EventHandler(this.SelectRecieverDialogForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUserList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.broadcastUserBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.areaInfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.branchInfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.branchInfoBindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.BindingSource areaInfoBindingSource;
        private System.Windows.Forms.BindingSource branchInfoBindingSource;
        private System.Windows.Forms.BindingSource branchInfoBindingSource1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView dataGridViewUserList;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.BindingSource broadcastUserBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn broadcastIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn branchIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn userIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn branchName2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isOpenedDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn openedDtDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isReadDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn readDtDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn areaIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn roleIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn roleNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn modifiedDtDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn activeDataGridViewCheckBoxColumn;
    }
}