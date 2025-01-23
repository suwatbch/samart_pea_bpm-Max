
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

namespace PEA.BPM.BillPrintingModule
{
    partial class ManageBarcodeView
    {
        /// <summary>
        /// The presenter used by this view.
        /// </summary>
        private PEA.BPM.BillPrintingModule.ManageBarcodeViewPresenter _presenter = null;

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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mruListGridView = new System.Windows.Forms.DataGridView();
            this.BranchId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MruId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsPrinted = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.submitButton = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.branchCBox = new System.Windows.Forms.ComboBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.toMruIdMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.mruIdMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.closeBt = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mruListGridView)).BeginInit();
            this.panel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.mruListGridView);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.groupBox1.Location = new System.Drawing.Point(3, 94);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(447, 289);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "������ͧ���¶١���·������ͧ��þ���������";
            // 
            // mruListGridView
            // 
            this.mruListGridView.AllowUserToAddRows = false;
            this.mruListGridView.AllowUserToDeleteRows = false;
            this.mruListGridView.AllowUserToResizeColumns = false;
            this.mruListGridView.AllowUserToResizeRows = false;
            this.mruListGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mruListGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BranchId,
            this.MruId,
            this.IsPrinted});
            this.mruListGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mruListGridView.Location = new System.Drawing.Point(3, 19);
            this.mruListGridView.Name = "mruListGridView";
            this.mruListGridView.RowHeadersVisible = false;
            this.mruListGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.mruListGridView.Size = new System.Drawing.Size(441, 267);
            this.mruListGridView.TabIndex = 0;
            this.mruListGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.mruListGridView_CellContentClick);
            // 
            // BranchId
            // 
            this.BranchId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.BranchId.DataPropertyName = "BranchId";
            this.BranchId.HeaderText = "���� ���.";
            this.BranchId.Name = "BranchId";
            // 
            // MruId
            // 
            this.MruId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.MruId.DataPropertyName = "MruId";
            this.MruId.FillWeight = 3F;
            this.MruId.HeaderText = "���";
            this.MruId.Name = "MruId";
            this.MruId.Width = 80;
            // 
            // IsPrinted
            // 
            this.IsPrinted.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.IsPrinted.DataPropertyName = "IsPrinted";
            this.IsPrinted.HeaderText = "���͡";
            this.IsPrinted.Name = "IsPrinted";
            this.IsPrinted.Width = 60;
            // 
            // submitButton
            // 
            this.submitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.submitButton.Enabled = false;
            this.submitButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submitButton.Location = new System.Drawing.Point(130, 389);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(90, 25);
            this.submitButton.TabIndex = 2;
            this.submitButton.Text = "�ѹ�֡";
            this.submitButton.UseVisualStyleBackColor = true;
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Controls.Add(this.closeBt);
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Controls.Add(this.submitButton);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(453, 424);
            this.panel3.TabIndex = 5;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.branchCBox);
            this.groupBox2.Controls.Add(this.searchButton);
            this.groupBox2.Controls.Add(this.toMruIdMaskedTextBox);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.mruIdMaskedTextBox);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(443, 85);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "���ҡ��俿��-��¡�è�˹���";
            // 
            // branchCBox
            // 
            this.branchCBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.branchCBox.FormattingEnabled = true;
            this.branchCBox.Location = new System.Drawing.Point(113, 23);
            this.branchCBox.Name = "branchCBox";
            this.branchCBox.Size = new System.Drawing.Size(312, 24);
            this.branchCBox.TabIndex = 4;
            this.branchCBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.branchCBox_KeyPress);
            // 
            // searchButton
            // 
            this.searchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.searchButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchButton.Image = global::PEA.BPM.BillPrintingModule.Properties.Resources.ZoomHS;
            this.searchButton.Location = new System.Drawing.Point(346, 51);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(79, 27);
            this.searchButton.TabIndex = 3;
            this.searchButton.Text = "����";
            this.searchButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.searchButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // toMruIdMaskedTextBox
            // 
            this.toMruIdMaskedTextBox.Location = new System.Drawing.Point(246, 54);
            this.toMruIdMaskedTextBox.Mask = "0000";
            this.toMruIdMaskedTextBox.Name = "toMruIdMaskedTextBox";
            this.toMruIdMaskedTextBox.Size = new System.Drawing.Size(81, 23);
            this.toMruIdMaskedTextBox.TabIndex = 2;
            this.toMruIdMaskedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toMruIdMaskedTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toMruIdMaskedTextBox_KeyDown);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label4.Location = new System.Drawing.Point(209, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "�֧";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mruIdMaskedTextBox
            // 
            this.mruIdMaskedTextBox.Location = new System.Drawing.Point(113, 53);
            this.mruIdMaskedTextBox.Mask = "0000";
            this.mruIdMaskedTextBox.Name = "mruIdMaskedTextBox";
            this.mruIdMaskedTextBox.Size = new System.Drawing.Size(80, 23);
            this.mruIdMaskedTextBox.TabIndex = 1;
            this.mruIdMaskedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mruIdMaskedTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mruIdMaskedTextBox_KeyDown);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(42, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "�ҡ��� :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label5.Location = new System.Drawing.Point(13, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "���ʡ��俿�� :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // closeBt
            // 
            this.closeBt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.closeBt.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeBt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeBt.Location = new System.Drawing.Point(226, 389);
            this.closeBt.Name = "closeBt";
            this.closeBt.Size = new System.Drawing.Size(90, 25);
            this.closeBt.TabIndex = 3;
            this.closeBt.Text = "�Դ˹�ҵ�ҧ";
            this.closeBt.UseVisualStyleBackColor = true;
            this.closeBt.Click += new System.EventHandler(this.closeBt_Click);
            // 
            // ManageBarcodeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Name = "ManageBarcodeView";
            this.Size = new System.Drawing.Size(453, 424);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mruListGridView)).EndInit();
            this.panel3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView mruListGridView;
        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button closeBt;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.MaskedTextBox toMruIdMaskedTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MaskedTextBox mruIdMaskedTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.ComboBox branchCBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn BranchId;
        private System.Windows.Forms.DataGridViewTextBoxColumn MruId;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsPrinted;
    }
}

