namespace AdministrativeTool.ConnectionSetting
{
    partial class frmConnectionSettingEdit
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
            this.btnOK = new System.Windows.Forms.Button();
            this.labBranchId = new System.Windows.Forms.Label();
            this.txtBranchId = new System.Windows.Forms.TextBox();
            this.comboActive = new System.Windows.Forms.ComboBox();
            this.labOnline = new System.Windows.Forms.Label();
            this.labBranch = new System.Windows.Forms.Label();
            this.labHeartbeat = new System.Windows.Forms.Label();
            this.labModifiedBy = new System.Windows.Forms.Label();
            this.labActive = new System.Windows.Forms.Label();
            this.txtOnline = new System.Windows.Forms.TextBox();
            this.txtBranch = new System.Windows.Forms.TextBox();
            this.txtHeartbeat = new System.Windows.Forms.TextBox();
            this.txtModifiedBy = new System.Windows.Forms.TextBox();
            this.grpStatus = new System.Windows.Forms.GroupBox();
            this.grpConnectionSetting = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnRecreate = new System.Windows.Forms.Button();
            this.grpStatus.SuspendLayout();
            this.grpConnectionSetting.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(464, 247);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // labBranchId
            // 
            this.labBranchId.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labBranchId.Location = new System.Drawing.Point(12, 15);
            this.labBranchId.Name = "labBranchId";
            this.labBranchId.Size = new System.Drawing.Size(99, 13);
            this.labBranchId.TabIndex = 0;
            this.labBranchId.Text = "Branch ID : ";
            this.labBranchId.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtBranchId
            // 
            this.txtBranchId.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBranchId.Location = new System.Drawing.Point(117, 12);
            this.txtBranchId.MaxLength = 6;
            this.txtBranchId.Name = "txtBranchId";
            this.txtBranchId.Size = new System.Drawing.Size(100, 22);
            this.txtBranchId.TabIndex = 1;
            this.txtBranchId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // comboActive
            // 
            this.comboActive.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboActive.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboActive.FormattingEnabled = true;
            this.comboActive.Location = new System.Drawing.Point(105, 52);
            this.comboActive.Name = "comboActive";
            this.comboActive.Size = new System.Drawing.Size(60, 22);
            this.comboActive.TabIndex = 3;
            this.comboActive.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboActive_KeyPress);
            // 
            // labOnline
            // 
            this.labOnline.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labOnline.Location = new System.Drawing.Point(13, 29);
            this.labOnline.Name = "labOnline";
            this.labOnline.Size = new System.Drawing.Size(86, 13);
            this.labOnline.TabIndex = 0;
            this.labOnline.Text = "Online : ";
            this.labOnline.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labBranch
            // 
            this.labBranch.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labBranch.Location = new System.Drawing.Point(13, 55);
            this.labBranch.Name = "labBranch";
            this.labBranch.Size = new System.Drawing.Size(86, 13);
            this.labBranch.TabIndex = 2;
            this.labBranch.Text = "Branch : ";
            this.labBranch.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labHeartbeat
            // 
            this.labHeartbeat.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labHeartbeat.Location = new System.Drawing.Point(13, 81);
            this.labHeartbeat.Name = "labHeartbeat";
            this.labHeartbeat.Size = new System.Drawing.Size(86, 13);
            this.labHeartbeat.TabIndex = 4;
            this.labHeartbeat.Text = "Heartbeat : ";
            this.labHeartbeat.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labModifiedBy
            // 
            this.labModifiedBy.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labModifiedBy.Location = new System.Drawing.Point(13, 29);
            this.labModifiedBy.Name = "labModifiedBy";
            this.labModifiedBy.Size = new System.Drawing.Size(86, 13);
            this.labModifiedBy.TabIndex = 0;
            this.labModifiedBy.Text = "Modified By : ";
            this.labModifiedBy.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labActive
            // 
            this.labActive.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labActive.Location = new System.Drawing.Point(13, 55);
            this.labActive.Name = "labActive";
            this.labActive.Size = new System.Drawing.Size(86, 13);
            this.labActive.TabIndex = 2;
            this.labActive.Text = "Active : ";
            this.labActive.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtOnline
            // 
            this.txtOnline.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOnline.Location = new System.Drawing.Point(105, 26);
            this.txtOnline.MaxLength = 255;
            this.txtOnline.Name = "txtOnline";
            this.txtOnline.Size = new System.Drawing.Size(490, 22);
            this.txtOnline.TabIndex = 1;
            this.txtOnline.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // txtBranch
            // 
            this.txtBranch.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBranch.Location = new System.Drawing.Point(105, 52);
            this.txtBranch.MaxLength = 255;
            this.txtBranch.Name = "txtBranch";
            this.txtBranch.Size = new System.Drawing.Size(490, 22);
            this.txtBranch.TabIndex = 3;
            this.txtBranch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // txtHeartbeat
            // 
            this.txtHeartbeat.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHeartbeat.Location = new System.Drawing.Point(105, 78);
            this.txtHeartbeat.MaxLength = 255;
            this.txtHeartbeat.Name = "txtHeartbeat";
            this.txtHeartbeat.Size = new System.Drawing.Size(490, 22);
            this.txtHeartbeat.TabIndex = 5;
            this.txtHeartbeat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // txtModifiedBy
            // 
            this.txtModifiedBy.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtModifiedBy.Location = new System.Drawing.Point(105, 26);
            this.txtModifiedBy.MaxLength = 8;
            this.txtModifiedBy.Name = "txtModifiedBy";
            this.txtModifiedBy.Size = new System.Drawing.Size(200, 22);
            this.txtModifiedBy.TabIndex = 1;
            this.txtModifiedBy.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // grpStatus
            // 
            this.grpStatus.Controls.Add(this.txtModifiedBy);
            this.grpStatus.Controls.Add(this.comboActive);
            this.grpStatus.Controls.Add(this.labModifiedBy);
            this.grpStatus.Controls.Add(this.labActive);
            this.grpStatus.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpStatus.Location = new System.Drawing.Point(12, 155);
            this.grpStatus.Name = "grpStatus";
            this.grpStatus.Padding = new System.Windows.Forms.Padding(10);
            this.grpStatus.Size = new System.Drawing.Size(608, 86);
            this.grpStatus.TabIndex = 3;
            this.grpStatus.TabStop = false;
            this.grpStatus.Text = "Status";
            // 
            // grpConnectionSetting
            // 
            this.grpConnectionSetting.Controls.Add(this.txtOnline);
            this.grpConnectionSetting.Controls.Add(this.txtBranch);
            this.grpConnectionSetting.Controls.Add(this.txtHeartbeat);
            this.grpConnectionSetting.Controls.Add(this.labOnline);
            this.grpConnectionSetting.Controls.Add(this.labBranch);
            this.grpConnectionSetting.Controls.Add(this.labHeartbeat);
            this.grpConnectionSetting.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpConnectionSetting.Location = new System.Drawing.Point(12, 38);
            this.grpConnectionSetting.Name = "grpConnectionSetting";
            this.grpConnectionSetting.Padding = new System.Windows.Forms.Padding(10);
            this.grpConnectionSetting.Size = new System.Drawing.Size(608, 111);
            this.grpConnectionSetting.TabIndex = 2;
            this.grpConnectionSetting.TabStop = false;
            this.grpConnectionSetting.Text = "Connection Settings";
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(545, 247);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnRecreate
            // 
            this.btnRecreate.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRecreate.Location = new System.Drawing.Point(12, 247);
            this.btnRecreate.Name = "btnRecreate";
            this.btnRecreate.Size = new System.Drawing.Size(75, 23);
            this.btnRecreate.TabIndex = 6;
            this.btnRecreate.Text = "Recreate";
            this.btnRecreate.UseVisualStyleBackColor = true;
            this.btnRecreate.Click += new System.EventHandler(this.btnRecreate_Click);
            // 
            // frmConnectionSettingEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 282);
            this.Controls.Add(this.btnRecreate);
            this.Controls.Add(this.txtBranchId);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.labBranchId);
            this.Controls.Add(this.grpConnectionSetting);
            this.Controls.Add(this.grpStatus);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConnectionSettingEdit";
            this.Text = "ConnectionSetting ()";
            this.Load += new System.EventHandler(this.frmConnectionSettingEdit_Load);
            this.grpStatus.ResumeLayout(false);
            this.grpStatus.PerformLayout();
            this.grpConnectionSetting.ResumeLayout(false);
            this.grpConnectionSetting.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label labBranchId;
        private System.Windows.Forms.TextBox txtBranchId;
        private System.Windows.Forms.ComboBox comboActive;
        private System.Windows.Forms.Label labOnline;
        private System.Windows.Forms.Label labBranch;
        private System.Windows.Forms.Label labHeartbeat;
        private System.Windows.Forms.Label labModifiedBy;
        private System.Windows.Forms.Label labActive;
        private System.Windows.Forms.TextBox txtOnline;
        private System.Windows.Forms.TextBox txtBranch;
        private System.Windows.Forms.TextBox txtHeartbeat;
        private System.Windows.Forms.TextBox txtModifiedBy;
        private System.Windows.Forms.GroupBox grpStatus;
        private System.Windows.Forms.GroupBox grpConnectionSetting;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnRecreate;
    }
}