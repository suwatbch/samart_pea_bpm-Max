
//----------------------------------------------------------------------------------------
// patterns & practices - Smart Client Software Factory - Guidance Package
//
// This file was generated by the "Add View" recipe.
//
// For more information see: 
// ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.scsf.2006jun/SCSF/html/03-030-Model%20View%20Presenter%20%20MVP%20.htm
//
// Latest version of this Guidance Package: http://go.microsoft.com/fwlink/?LinkId=62182
//----------------------------------------------------------------------------------------

namespace PEA.BPM.ToolModule
{
    partial class EmployeeSearchView
    {
        /// <summary>
        /// The presenter used by this view.
        /// </summary>
        private PEA.BPM.ToolModule.EmployeeSearchViewPresenter _presenter = null;

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
            if (disposing)
            {
                if (_presenter != null)
                    _presenter.Dispose();

                if (components != null)
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.fineBt = new System.Windows.Forms.ToolStripButton();
            this.keywordText = new System.Windows.Forms.ToolStripTextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.employeeGv = new System.Windows.Forms.DataGridView();
            this.okeyBt = new System.Windows.Forms.Button();
            this.cancelBt = new System.Windows.Forms.Button();
            this.employeeId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmployeeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Department = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserFlag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.employeeGv)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fineBt,
            this.keywordText});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(567, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // fineBt
            // 
            this.fineBt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fineBt.Image = global::PEA.BPM.ToolModule.Properties.Resources.ZoomHS;
            this.fineBt.Name = "fineBt";
            this.fineBt.Size = new System.Drawing.Size(137, 22);
            this.fineBt.Text = "���Ҿ�ѡ�ҹ ���.";
            this.fineBt.Click += new System.EventHandler(this.fineBt_Click);
            // 
            // keywordText
            // 
            this.keywordText.Name = "keywordText";
            this.keywordText.Size = new System.Drawing.Size(349, 25);
            this.keywordText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.keywordText_KeyDown);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.employeeGv);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.okeyBt);
            this.splitContainer1.Panel2.Controls.Add(this.cancelBt);
            this.splitContainer1.Size = new System.Drawing.Size(567, 425);
            this.splitContainer1.SplitterDistance = 371;
            this.splitContainer1.TabIndex = 1;
            // 
            // employeeGv
            // 
            this.employeeGv.AllowUserToAddRows = false;
            this.employeeGv.AllowUserToDeleteRows = false;
            this.employeeGv.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.employeeGv.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.employeeGv.BackgroundColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.employeeGv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.employeeGv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.employeeGv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.employeeId,
            this.EmployeeName,
            this.Department,
            this.UserFlag});
            this.employeeGv.GridColor = System.Drawing.SystemColors.Control;
            this.employeeGv.Location = new System.Drawing.Point(12, 5);
            this.employeeGv.Name = "employeeGv";
            this.employeeGv.RowHeadersVisible = false;
            this.employeeGv.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.employeeGv.RowTemplate.Height = 25;
            this.employeeGv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.employeeGv.Size = new System.Drawing.Size(545, 364);
            this.employeeGv.TabIndex = 0;
            this.employeeGv.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.employeeGv_CellDoubleClick);
            // 
            // okeyBt
            // 
            this.okeyBt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okeyBt.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.okeyBt.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okeyBt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.okeyBt.Location = new System.Drawing.Point(352, 7);
            this.okeyBt.Name = "okeyBt";
            this.okeyBt.Size = new System.Drawing.Size(93, 25);
            this.okeyBt.TabIndex = 6;
            this.okeyBt.Text = "��ŧ";
            this.okeyBt.UseVisualStyleBackColor = false;
            this.okeyBt.Click += new System.EventHandler(this.okeyBt_Click);
            // 
            // cancelBt
            // 
            this.cancelBt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelBt.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.cancelBt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cancelBt.Location = new System.Drawing.Point(453, 7);
            this.cancelBt.Name = "cancelBt";
            this.cancelBt.Size = new System.Drawing.Size(93, 25);
            this.cancelBt.TabIndex = 5;
            this.cancelBt.Text = "¡��ԡ";
            this.cancelBt.UseVisualStyleBackColor = false;
            this.cancelBt.Click += new System.EventHandler(this.cancelBt_Click);
            // 
            // employeeId
            // 
            this.employeeId.DataPropertyName = "EmployeeId";
            this.employeeId.HeaderText = "����";
            this.employeeId.Name = "employeeId";
            this.employeeId.ReadOnly = true;
            this.employeeId.Width = 80;
            // 
            // EmployeeName
            // 
            this.EmployeeName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.EmployeeName.DataPropertyName = "EmployeeName";
            this.EmployeeName.HeaderText = "���� - ʡ��";
            this.EmployeeName.Name = "EmployeeName";
            this.EmployeeName.ReadOnly = true;
            // 
            // Department
            // 
            this.Department.DataPropertyName = "Department";
            this.Department.HeaderText = "˹��§ҹ";
            this.Department.Name = "Department";
            this.Department.ReadOnly = true;
            this.Department.Width = 220;
            // 
            // UserFlag
            // 
            this.UserFlag.DataPropertyName = "UserFlag";
            this.UserFlag.HeaderText = "UserFlag";
            this.UserFlag.Name = "UserFlag";
            this.UserFlag.ReadOnly = true;
            this.UserFlag.Visible = false;
            // 
            // EmployeeSearchView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "EmployeeSearchView";
            this.Size = new System.Drawing.Size(567, 450);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.employeeGv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripTextBox keywordText;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView employeeGv;
        private System.Windows.Forms.Button cancelBt;
        private System.Windows.Forms.Button okeyBt;
        private System.Windows.Forms.ToolStripButton fineBt;
        private System.Windows.Forms.DataGridViewTextBoxColumn employeeId;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmployeeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Department;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserFlag;
    }
}

