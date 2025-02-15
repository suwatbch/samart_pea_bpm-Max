
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
    partial class PEACodeSearchView
    {
        /// <summary>
        /// The presenter used by this view.
        /// </summary>
        private PEA.BPM.ToolModule.PEACodeSearchViewPresenter _presenter = null;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.peaTSContainer = new System.Windows.Forms.ToolStripContainer();
            this.okBt = new System.Windows.Forms.Button();
            this.cancelBt = new System.Windows.Forms.Button();
            this.peaGBox = new System.Windows.Forms.GroupBox();
            this.peaDataGV = new System.Windows.Forms.DataGridView();
            this.PeaCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BACode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.branchLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PeaName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BranchNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.peaSearchBt = new System.Windows.Forms.ToolStripSplitButton();
            this.peaSearchText = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripContainer3 = new System.Windows.Forms.ToolStripContainer();
            this.toolStripContainer2 = new System.Windows.Forms.ToolStripContainer();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.peaTSContainer.ContentPanel.SuspendLayout();
            this.peaTSContainer.TopToolStripPanel.SuspendLayout();
            this.peaTSContainer.SuspendLayout();
            this.peaGBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.peaDataGV)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.toolStripContainer3.SuspendLayout();
            this.toolStripContainer2.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // peaTSContainer
            // 
            this.peaTSContainer.BottomToolStripPanelVisible = false;
            // 
            // peaTSContainer.ContentPanel
            // 
            this.peaTSContainer.ContentPanel.Controls.Add(this.okBt);
            this.peaTSContainer.ContentPanel.Controls.Add(this.cancelBt);
            this.peaTSContainer.ContentPanel.Controls.Add(this.peaGBox);
            this.peaTSContainer.ContentPanel.Size = new System.Drawing.Size(806, 436);
            this.peaTSContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.peaTSContainer.LeftToolStripPanelVisible = false;
            this.peaTSContainer.Location = new System.Drawing.Point(0, 0);
            this.peaTSContainer.Name = "peaTSContainer";
            this.peaTSContainer.RightToolStripPanelVisible = false;
            this.peaTSContainer.Size = new System.Drawing.Size(806, 461);
            this.peaTSContainer.TabIndex = 7;
            this.peaTSContainer.Text = "toolStripContainer4";
            // 
            // peaTSContainer.TopToolStripPanel
            // 
            this.peaTSContainer.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // okBt
            // 
            this.okBt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okBt.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okBt.Location = new System.Drawing.Point(530, 395);
            this.okBt.Name = "okBt";
            this.okBt.Size = new System.Drawing.Size(117, 26);
            this.okBt.TabIndex = 6;
            this.okBt.Text = "��ŧ";
            this.okBt.UseVisualStyleBackColor = true;
            this.okBt.Click += new System.EventHandler(this.okBt_Click);
            // 
            // cancelBt
            // 
            this.cancelBt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelBt.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBt.Location = new System.Drawing.Point(654, 395);
            this.cancelBt.Name = "cancelBt";
            this.cancelBt.Size = new System.Drawing.Size(117, 26);
            this.cancelBt.TabIndex = 2;
            this.cancelBt.Text = "¡��ԡ";
            this.cancelBt.UseVisualStyleBackColor = true;
            // 
            // peaGBox
            // 
            this.peaGBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.peaGBox.Controls.Add(this.peaDataGV);
            this.peaGBox.Location = new System.Drawing.Point(11, 11);
            this.peaGBox.Name = "peaGBox";
            this.peaGBox.Size = new System.Drawing.Size(784, 378);
            this.peaGBox.TabIndex = 0;
            this.peaGBox.TabStop = false;
            this.peaGBox.Text = "��¡�ä��ҡ��俿��";
            // 
            // peaDataGV
            // 
            this.peaDataGV.AllowUserToAddRows = false;
            this.peaDataGV.AllowUserToDeleteRows = false;
            this.peaDataGV.AllowUserToOrderColumns = true;
            this.peaDataGV.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.peaDataGV.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.peaDataGV.BackgroundColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(211, 117, 191);
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.peaDataGV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.peaDataGV.ColumnHeadersHeight = 22;
            this.peaDataGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.peaDataGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PeaCode,
            this.BACode,
            this.branchLevel,
            this.PeaName,
            this.BranchNo,
            this.Address});
            this.peaDataGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.peaDataGV.GridColor = System.Drawing.SystemColors.ControlLight;
            this.peaDataGV.Location = new System.Drawing.Point(3, 18);
            this.peaDataGV.MultiSelect = false;
            this.peaDataGV.Name = "peaDataGV";
            this.peaDataGV.ReadOnly = true;
            this.peaDataGV.RowHeadersVisible = false;
            this.peaDataGV.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.peaDataGV.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.peaDataGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.peaDataGV.Size = new System.Drawing.Size(778, 357);
            this.peaDataGV.TabIndex = 0;
            this.peaDataGV.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.peaDataGV_RowEnter);
            this.peaDataGV.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.peaDataGV_CellDoubleClick);
            this.peaDataGV.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.peaDataGV_RowLeave);
            // 
            // PeaCode
            // 
            this.PeaCode.DataPropertyName = "BranchId";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.PeaCode.DefaultCellStyle = dataGridViewCellStyle3;
            this.PeaCode.HeaderText = "���ʡ��俿��";
            this.PeaCode.Name = "PeaCode";
            this.PeaCode.ReadOnly = true;
            this.PeaCode.Width = 80;
            // 
            // BACode
            // 
            this.BACode.DataPropertyName = "BACode";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.BACode.DefaultCellStyle = dataGridViewCellStyle4;
            this.BACode.HeaderText = "���������СԨ";
            this.BACode.Name = "BACode";
            this.BACode.ReadOnly = true;
            this.BACode.Width = 80;
            // 
            // branchLevel
            // 
            this.branchLevel.DataPropertyName = "BranchLevel";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.branchLevel.DefaultCellStyle = dataGridViewCellStyle5;
            this.branchLevel.HeaderText = "�ӴѺ��� ���.";
            this.branchLevel.Name = "branchLevel";
            this.branchLevel.ReadOnly = true;
            // 
            // PeaName
            // 
            this.PeaName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PeaName.DataPropertyName = "BranchName";
            this.PeaName.HeaderText = "���͡��俿��";
            this.PeaName.Name = "PeaName";
            this.PeaName.ReadOnly = true;
            // 
            // BranchNo
            // 
            this.BranchNo.DataPropertyName = "BranchNo";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Format = " ";
            dataGridViewCellStyle6.NullValue = null;
            this.BranchNo.DefaultCellStyle = dataGridViewCellStyle6;
            this.BranchNo.HeaderText = "�Ңҷ��";
            this.BranchNo.Name = "BranchNo";
            this.BranchNo.ReadOnly = true;
            this.BranchNo.Width = 60;
            // 
            // Address
            // 
            this.Address.DataPropertyName = "Address";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.Address.DefaultCellStyle = dataGridViewCellStyle7;
            this.Address.HeaderText = "�������";
            this.Address.Name = "Address";
            this.Address.ReadOnly = true;
            this.Address.Width = 150;
            // 
            // toolStrip1
            // 
            this.toolStrip1.CanOverflow = false;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.peaSearchBt,
            this.peaSearchText,
            this.toolStripLabel2});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(461, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(16, 22);
            this.toolStripLabel1.Text = "   ";
            // 
            // peaSearchBt
            // 
            this.peaSearchBt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.peaSearchBt.Image = global::PEA.BPM.ToolModule.Properties.Resources.ZoomHS;
            this.peaSearchBt.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.peaSearchBt.Name = "peaSearchBt";
            this.peaSearchBt.Size = new System.Drawing.Size(140, 22);
            this.peaSearchBt.Text = "������: ������";
            this.peaSearchBt.ButtonClick += new System.EventHandler(this.peaSearchBt_ButtonClick);
            // 
            // peaSearchText
            // 
            this.peaSearchText.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.peaSearchText.Name = "peaSearchText";
            this.peaSearchText.Size = new System.Drawing.Size(300, 25);
            this.peaSearchText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.peaSearchText_KeyDown);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(0, 22);
            this.toolStripLabel2.Text = "                                                                                ";
            // 
            // toolStripContainer3
            // 
            // 
            // toolStripContainer3.ContentPanel
            // 
            this.toolStripContainer3.ContentPanel.Size = new System.Drawing.Size(806, 436);
            this.toolStripContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer3.LeftToolStripPanelVisible = false;
            this.toolStripContainer3.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer3.Name = "toolStripContainer3";
            this.toolStripContainer3.RightToolStripPanelVisible = false;
            this.toolStripContainer3.Size = new System.Drawing.Size(806, 461);
            this.toolStripContainer3.TabIndex = 6;
            this.toolStripContainer3.Text = "toolStripContainer3";
            // 
            // toolStripContainer2
            // 
            // 
            // toolStripContainer2.ContentPanel
            // 
            this.toolStripContainer2.ContentPanel.Size = new System.Drawing.Size(806, 436);
            this.toolStripContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer2.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer2.Name = "toolStripContainer2";
            this.toolStripContainer2.Size = new System.Drawing.Size(806, 461);
            this.toolStripContainer2.TabIndex = 5;
            this.toolStripContainer2.Text = "toolStripContainer2";
            // 
            // toolStripContainer1
            // 
            this.toolStripContainer1.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(806, 436);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(806, 461);
            this.toolStripContainer1.TabIndex = 4;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Id";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewTextBoxColumn1.Frozen = true;
            this.dataGridViewTextBoxColumn1.HeaderText = "���ʡ��俿��";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Name";
            this.dataGridViewTextBoxColumn2.Frozen = true;
            this.dataGridViewTextBoxColumn2.HeaderText = "���͡��俿��";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 200;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "NumOfLines";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridViewTextBoxColumn3.Frozen = true;
            this.dataGridViewTextBoxColumn3.HeaderText = "�ӹǹ��¡�����Թ";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 150;
            // 
            // PEACodeSearchView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.peaTSContainer);
            this.Controls.Add(this.toolStripContainer3);
            this.Controls.Add(this.toolStripContainer2);
            this.Controls.Add(this.toolStripContainer1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "PEACodeSearchView";
            this.Size = new System.Drawing.Size(806, 461);
            this.peaTSContainer.ContentPanel.ResumeLayout(false);
            this.peaTSContainer.TopToolStripPanel.ResumeLayout(false);
            this.peaTSContainer.TopToolStripPanel.PerformLayout();
            this.peaTSContainer.ResumeLayout(false);
            this.peaTSContainer.PerformLayout();
            this.peaGBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.peaDataGV)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStripContainer3.ResumeLayout(false);
            this.toolStripContainer3.PerformLayout();
            this.toolStripContainer2.ResumeLayout(false);
            this.toolStripContainer2.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer peaTSContainer;
        private System.Windows.Forms.Button cancelBt;
        private System.Windows.Forms.GroupBox peaGBox;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSplitButton peaSearchBt;
        private System.Windows.Forms.ToolStripTextBox peaSearchText;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripContainer toolStripContainer3;
        private System.Windows.Forms.ToolStripContainer toolStripContainer2;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.Button okBt;
        private System.Windows.Forms.DataGridView peaDataGV;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn PeaCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn BACode;
        private System.Windows.Forms.DataGridViewTextBoxColumn branchLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn PeaName;
        private System.Windows.Forms.DataGridViewTextBoxColumn BranchNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address;
    }
}

