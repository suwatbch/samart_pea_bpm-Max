
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
    partial class ReportUnlockingLogView
    {
        /// <summary>
        /// The presenter used by this view.
        /// </summary>
        private PEA.BPM.ToolModule.ReportUnlockingLogViewPresenter _presenter = null;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.fncCBox = new System.Windows.Forms.ComboBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.branchIdMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.clearButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.reportUnlockingLogDataGridView = new System.Windows.Forms.DataGridView();
            this.checkColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.electricityIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.delColumn = new System.Windows.Forms.DataGridViewLinkColumn();
            this.showReportButton = new System.Windows.Forms.Button();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.billPeriodMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.reportUnlockingLogDataGridView)).BeginInit();
            this.groupBox12.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Controls.Add(this.groupBox7);
            this.panel3.Controls.Add(this.clearButton);
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Controls.Add(this.showReportButton);
            this.panel3.Controls.Add(this.groupBox12);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(253, 650);
            this.panel3.TabIndex = 11;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.fncCBox);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.groupBox1.Location = new System.Drawing.Point(3, 164);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(242, 50);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "�ѧ��ѹ��÷ӧҹ";
            // 
            // fncCBox
            // 
            this.fncCBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fncCBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.fncCBox.FormattingEnabled = true;
            this.fncCBox.Location = new System.Drawing.Point(12, 19);
            this.fncCBox.MaxDropDownItems = 12;
            this.fncCBox.Name = "fncCBox";
            this.fncCBox.Size = new System.Drawing.Size(224, 22);
            this.fncCBox.TabIndex = 8;
            this.fncCBox.SelectionChangeCommitted += new System.EventHandler(this.fncCBox_SelectionChangeCommitted);
            this.fncCBox.DropDown += new System.EventHandler(this.fncCBox_DropDown);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.branchIdMaskedTextBox);
            this.groupBox7.Controls.Add(this.label5);
            this.groupBox7.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.groupBox7.Location = new System.Drawing.Point(3, 111);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(242, 50);
            this.groupBox7.TabIndex = 3;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "��ǡ�ͧ";
            // 
            // branchIdMaskedTextBox
            // 
            this.branchIdMaskedTextBox.Location = new System.Drawing.Point(120, 20);
            this.branchIdMaskedTextBox.Name = "branchIdMaskedTextBox";
            this.branchIdMaskedTextBox.Size = new System.Drawing.Size(110, 23);
            this.branchIdMaskedTextBox.TabIndex = 0;
            this.branchIdMaskedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.branchIdMaskedTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.branchIdMaskedTextBox_KeyPress);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label5.Location = new System.Drawing.Point(18, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 20);
            this.label5.TabIndex = 2;
            this.label5.Text = "���ʡ��俿��:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // clearButton
            // 
            this.clearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.clearButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.clearButton.Location = new System.Drawing.Point(128, 606);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(111, 31);
            this.clearButton.TabIndex = 6;
            this.clearButton.Text = "��ҧ��ͤ���";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.reportUnlockingLogDataGridView);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.groupBox2.Location = new System.Drawing.Point(3, 220);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(242, 380);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "List ���ʡ��俿��";
            // 
            // reportUnlockingLogDataGridView
            // 
            this.reportUnlockingLogDataGridView.AllowUserToAddRows = false;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reportUnlockingLogDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.reportUnlockingLogDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.reportUnlockingLogDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.reportUnlockingLogDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.checkColumn,
            this.electricityIdColumn,
            this.delColumn});
            this.reportUnlockingLogDataGridView.Location = new System.Drawing.Point(9, 19);
            this.reportUnlockingLogDataGridView.Name = "reportUnlockingLogDataGridView";
            this.reportUnlockingLogDataGridView.RowHeadersVisible = false;
            this.reportUnlockingLogDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.reportUnlockingLogDataGridView.Size = new System.Drawing.Size(227, 355);
            this.reportUnlockingLogDataGridView.TabIndex = 0;
            this.reportUnlockingLogDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.reportUnlockingLogDataGridView_CellContentClick);
            // 
            // checkColumn
            // 
            this.checkColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.checkColumn.FillWeight = 50F;
            this.checkColumn.HeaderText = "";
            this.checkColumn.Name = "checkColumn";
            this.checkColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.checkColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.checkColumn.Visible = false;
            // 
            // electricityIdColumn
            // 
            this.electricityIdColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.electricityIdColumn.HeaderText = "���ʡ��俿��";
            this.electricityIdColumn.MaxInputLength = 30;
            this.electricityIdColumn.Name = "electricityIdColumn";
            this.electricityIdColumn.ReadOnly = true;
            // 
            // delColumn
            // 
            this.delColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.delColumn.HeaderText = "ź";
            this.delColumn.Name = "delColumn";
            this.delColumn.Text = "ź";
            this.delColumn.UseColumnTextForLinkValue = true;
            this.delColumn.Width = 30;
            // 
            // showReportButton
            // 
            this.showReportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.showReportButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.showReportButton.Location = new System.Drawing.Point(12, 606);
            this.showReportButton.Name = "showReportButton";
            this.showReportButton.Size = new System.Drawing.Size(110, 31);
            this.showReportButton.TabIndex = 5;
            this.showReportButton.Text = "�ʴ���§ҹ";
            this.showReportButton.UseVisualStyleBackColor = true;
            this.showReportButton.Click += new System.EventHandler(this.showReportButton_Click);
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.billPeriodMaskedTextBox);
            this.groupBox12.Controls.Add(this.label11);
            this.groupBox12.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.groupBox12.Location = new System.Drawing.Point(3, 51);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(242, 56);
            this.groupBox12.TabIndex = 1;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "�ѹ�����ͤ";
            // 
            // billPeriodMaskedTextBox
            // 
            this.billPeriodMaskedTextBox.Location = new System.Drawing.Point(151, 21);
            this.billPeriodMaskedTextBox.Mask = "00/00/0000";
            this.billPeriodMaskedTextBox.Name = "billPeriodMaskedTextBox";
            this.billPeriodMaskedTextBox.Size = new System.Drawing.Size(79, 23);
            this.billPeriodMaskedTextBox.TabIndex = 0;
            this.billPeriodMaskedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.billPeriodMaskedTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.billPeriodMaskedTextBox_KeyPress);
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label11.Location = new System.Drawing.Point(6, 21);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(143, 23);
            this.label11.TabIndex = 2;
            this.label11.Text = "��Ш��ѹ(��/��/����):";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(211, 117, 192);
            this.label12.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(-1, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(250, 50);
            this.label12.TabIndex = 0;
            this.label12.Text = "��§ҹ��ûŴ��ͤ";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ReportUnlockingLogView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Name = "ReportUnlockingLogView";
            this.Size = new System.Drawing.Size(253, 653);
            this.panel3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.reportUnlockingLogDataGridView)).EndInit();
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.MaskedTextBox branchIdMaskedTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView reportUnlockingLogDataGridView;
        private System.Windows.Forms.DataGridViewCheckBoxColumn checkColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn electricityIdColumn;
        private System.Windows.Forms.DataGridViewLinkColumn delColumn;
        private System.Windows.Forms.Button showReportButton;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.MaskedTextBox billPeriodMaskedTextBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox fncCBox;
    }
}

