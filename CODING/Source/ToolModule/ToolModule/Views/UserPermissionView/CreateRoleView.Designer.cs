namespace PEA.BPM.ToolModule
{
    partial class CreateRoleView
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.roleNameText = new System.Windows.Forms.TextBox();
            this.chooseAllBt = new System.Windows.Forms.Button();
            this.cancelBt = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.okBt = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.descText = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.functionGridView = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.chooseNoneBt = new System.Windows.Forms.Button();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.FCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.moduleName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.functionName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubFunctionName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Internal = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.functionId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.functionGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // roleNameText
            // 
            this.roleNameText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.roleNameText.Location = new System.Drawing.Point(131, 26);
            this.roleNameText.Name = "roleNameText";
            this.roleNameText.Size = new System.Drawing.Size(584, 23);
            this.roleNameText.TabIndex = 2;
            // 
            // chooseAllBt
            // 
            this.chooseAllBt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chooseAllBt.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.chooseAllBt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chooseAllBt.Location = new System.Drawing.Point(23, 559);
            this.chooseAllBt.Name = "chooseAllBt";
            this.chooseAllBt.Size = new System.Drawing.Size(100, 26);
            this.chooseAllBt.TabIndex = 11;
            this.chooseAllBt.Text = "เลือกทั้งหมด";
            this.chooseAllBt.UseVisualStyleBackColor = false;
            this.chooseAllBt.Click += new System.EventHandler(this.chooseAllBt_Click);
            // 
            // cancelBt
            // 
            this.cancelBt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelBt.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.cancelBt.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cancelBt.Location = new System.Drawing.Point(627, 559);
            this.cancelBt.Name = "cancelBt";
            this.cancelBt.Size = new System.Drawing.Size(100, 26);
            this.cancelBt.TabIndex = 10;
            this.cancelBt.Text = "ยกเลิก";
            this.cancelBt.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "คำอธิบายเพิ่ม :";
            // 
            // okBt
            // 
            this.okBt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okBt.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.okBt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.okBt.Location = new System.Drawing.Point(521, 559);
            this.okBt.Name = "okBt";
            this.okBt.Size = new System.Drawing.Size(100, 26);
            this.okBt.TabIndex = 9;
            this.okBt.Text = "สร้าง";
            this.okBt.UseVisualStyleBackColor = false;
            this.okBt.Click += new System.EventHandler(this.okBt_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.roleNameText);
            this.groupBox1.Controls.Add(this.descText);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.groupBox1.Location = new System.Drawing.Point(7, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(739, 547);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ระบุข้อมูลกลุ่มผู้ใช้งานใหม่";
            // 
            // descText
            // 
            this.descText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.descText.Location = new System.Drawing.Point(131, 55);
            this.descText.Name = "descText";
            this.descText.Size = new System.Drawing.Size(584, 23);
            this.descText.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.functionGridView);
            this.groupBox2.Location = new System.Drawing.Point(6, 90);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(727, 451);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "รายการฟังก์ชันทั้งหมด";
            // 
            // functionGridView
            // 
            this.functionGridView.AllowUserToAddRows = false;
            this.functionGridView.AllowUserToDeleteRows = false;
            this.functionGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.functionGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.functionGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.functionGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.functionGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.functionGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FCheck,
            this.moduleName,
            this.functionName,
            this.SubFunctionName,
            this.Internal,
            this.functionId});
            this.functionGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.functionGridView.GridColor = System.Drawing.SystemColors.Control;
            this.functionGridView.Location = new System.Drawing.Point(3, 19);
            this.functionGridView.Name = "functionGridView";
            this.functionGridView.RowHeadersVisible = false;
            this.functionGridView.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.functionGridView.RowTemplate.Height = 25;
            this.functionGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.functionGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.functionGridView.Size = new System.Drawing.Size(721, 429);
            this.functionGridView.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(68, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "ชื่อกลุ่ม :";
            // 
            // chooseNoneBt
            // 
            this.chooseNoneBt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chooseNoneBt.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.chooseNoneBt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.chooseNoneBt.Location = new System.Drawing.Point(129, 559);
            this.chooseNoneBt.Name = "chooseNoneBt";
            this.chooseNoneBt.Size = new System.Drawing.Size(100, 26);
            this.chooseNoneBt.TabIndex = 12;
            this.chooseNoneBt.Text = "ไม่เลือกทั้งหมด";
            this.chooseNoneBt.UseVisualStyleBackColor = false;
            this.chooseNoneBt.Click += new System.EventHandler(this.chooseNoneBt_Click);
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // FCheck
            // 
            this.FCheck.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.FCheck.DataPropertyName = "Check";
            this.FCheck.FillWeight = 0.2286546F;
            this.FCheck.HeaderText = "เลือก";
            this.FCheck.MinimumWidth = 40;
            this.FCheck.Name = "FCheck";
            this.FCheck.Width = 40;
            // 
            // moduleName
            // 
            this.moduleName.DataPropertyName = "ModuleName";
            this.moduleName.FillWeight = 1F;
            this.moduleName.HeaderText = "โมดูล";
            this.moduleName.MinimumWidth = 100;
            this.moduleName.Name = "moduleName";
            this.moduleName.ReadOnly = true;
            // 
            // functionName
            // 
            this.functionName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.functionName.DataPropertyName = "FunctionName";
            this.functionName.FillWeight = 1F;
            this.functionName.HeaderText = "ฟังก์ชัน";
            this.functionName.MinimumWidth = 200;
            this.functionName.Name = "functionName";
            this.functionName.ReadOnly = true;
            this.functionName.Width = 200;
            // 
            // SubFunctionName
            // 
            this.SubFunctionName.DataPropertyName = "SubFunctionName";
            this.SubFunctionName.HeaderText = "ฟังก์ชันย่อย";
            this.SubFunctionName.Name = "SubFunctionName";
            this.SubFunctionName.ReadOnly = true;
            // 
            // Internal
            // 
            this.Internal.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Internal.DataPropertyName = "Internal";
            this.Internal.HeaderText = "จำกัดขอบเขต";
            this.Internal.Name = "Internal";
            this.Internal.ReadOnly = true;
            this.Internal.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Internal.Width = 87;
            // 
            // functionId
            // 
            this.functionId.DataPropertyName = "FunctionId";
            this.functionId.FillWeight = 1F;
            this.functionId.HeaderText = "functionId";
            this.functionId.MinimumWidth = 30;
            this.functionId.Name = "functionId";
            this.functionId.ReadOnly = true;
            this.functionId.Visible = false;
            // 
            // CreateRoleView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 602);
            this.Controls.Add(this.chooseAllBt);
            this.Controls.Add(this.cancelBt);
            this.Controls.Add(this.okBt);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chooseNoneBt);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "CreateRoleView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "สร้างกลุ่มผู้ใช้งานใหม่";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.functionGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox roleNameText;
        private System.Windows.Forms.Button chooseAllBt;
        private System.Windows.Forms.Button cancelBt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button okBt;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox descText;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView functionGridView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button chooseNoneBt;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.DataGridViewCheckBoxColumn FCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn moduleName;
        private System.Windows.Forms.DataGridViewTextBoxColumn functionName;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubFunctionName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Internal;
        private System.Windows.Forms.DataGridViewTextBoxColumn functionId;

    }
}