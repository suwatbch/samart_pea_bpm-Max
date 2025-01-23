namespace PEA.BPM.ToolModule
{
    partial class RoleFunctionView
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
            this.okBt = new System.Windows.Forms.Button();
            this.functionListGb = new System.Windows.Forms.GroupBox();
            this.functionGv = new System.Windows.Forms.DataGridView();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.Select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ModuleName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FunctionName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubFunction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Internal = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.FunctionId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.functionListGb.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.functionGv)).BeginInit();
            this.SuspendLayout();
            // 
            // okBt
            // 
            this.okBt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okBt.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.okBt.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.okBt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.okBt.Location = new System.Drawing.Point(559, 525);
            this.okBt.Name = "okBt";
            this.okBt.Size = new System.Drawing.Size(117, 25);
            this.okBt.TabIndex = 9;
            this.okBt.Text = "ตกลง";
            this.okBt.UseVisualStyleBackColor = false;
            // 
            // functionListGb
            // 
            this.functionListGb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.functionListGb.Controls.Add(this.functionGv);
            this.functionListGb.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.functionListGb.Location = new System.Drawing.Point(7, 7);
            this.functionListGb.Name = "functionListGb";
            this.functionListGb.Size = new System.Drawing.Size(686, 512);
            this.functionListGb.TabIndex = 8;
            this.functionListGb.TabStop = false;
            this.functionListGb.Text = "<>";
            // 
            // functionGv
            // 
            this.functionGv.AllowUserToAddRows = false;
            this.functionGv.AllowUserToDeleteRows = false;
            this.functionGv.AllowUserToOrderColumns = true;
            this.functionGv.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.functionGv.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.functionGv.BackgroundColor = System.Drawing.SystemColors.Window;
            this.functionGv.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.functionGv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.functionGv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Select,
            this.ModuleName,
            this.FunctionName,
            this.SubFunction,
            this.Internal,
            this.FunctionId});
            this.functionGv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.functionGv.GridColor = System.Drawing.SystemColors.Control;
            this.functionGv.Location = new System.Drawing.Point(3, 19);
            this.functionGv.Name = "functionGv";
            this.functionGv.RowHeadersVisible = false;
            this.functionGv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.functionGv.Size = new System.Drawing.Size(680, 490);
            this.functionGv.TabIndex = 0;
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // Select
            // 
            this.Select.DataPropertyName = "Check";
            this.Select.HeaderText = "เลือก";
            this.Select.Name = "Select";
            this.Select.ReadOnly = true;
            this.Select.Visible = false;
            this.Select.Width = 40;
            // 
            // ModuleName
            // 
            this.ModuleName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ModuleName.DataPropertyName = "ModuleName";
            this.ModuleName.HeaderText = "โมดูล";
            this.ModuleName.Name = "ModuleName";
            this.ModuleName.ReadOnly = true;
            // 
            // FunctionName
            // 
            this.FunctionName.DataPropertyName = "FunctionName";
            this.FunctionName.HeaderText = "ฟังก์ชัน";
            this.FunctionName.Name = "FunctionName";
            this.FunctionName.ReadOnly = true;
            this.FunctionName.Width = 200;
            // 
            // SubFunction
            // 
            this.SubFunction.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.SubFunction.DataPropertyName = "SubFunctionName";
            this.SubFunction.HeaderText = "ฟังก์ชันย่อย";
            this.SubFunction.Name = "SubFunction";
            this.SubFunction.ReadOnly = true;
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
            // FunctionId
            // 
            this.FunctionId.DataPropertyName = "FunctionId";
            this.FunctionId.HeaderText = "รหัส";
            this.FunctionId.Name = "FunctionId";
            this.FunctionId.ReadOnly = true;
            this.FunctionId.Visible = false;
            // 
            // RoleFunctionView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 562);
            this.Controls.Add(this.okBt);
            this.Controls.Add(this.functionListGb);
            this.Name = "RoleFunctionView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "รายการฟังก์ชัน";
            this.functionListGb.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.functionGv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button okBt;
        private System.Windows.Forms.GroupBox functionListGb;
        private System.Windows.Forms.DataGridView functionGv;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Select;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModuleName;
        private System.Windows.Forms.DataGridViewTextBoxColumn FunctionName;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubFunction;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Internal;
        private System.Windows.Forms.DataGridViewTextBoxColumn FunctionId;
    }
}