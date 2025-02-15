
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
    partial class GreenBillView
    {
        /// <summary>
        /// The presenter used by this view.
        /// </summary>
        private PEA.BPM.BillPrintingModule.GreenBillViewPresenter _presenter = null;

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
            this.panel1 = new System.Windows.Forms.Panel();
            this.clearButton = new System.Windows.Forms.Button();
            this.printButton = new System.Windows.Forms.Button();
            this.greenBillListViewGroupBox = new System.Windows.Forms.GroupBox();
            this.checkAllCheckBox = new System.Windows.Forms.CheckBox();
            this.greenBillDataGridView = new System.Windows.Forms.DataGridView();
            this.checkColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.fromInvoiceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toInvoiceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lotNoColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bankIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dueDateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BillType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BillPred = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrintDetail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.bankSearchBt = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.bankDueDateText = new System.Windows.Forms.MaskedTextBox();
            this.bankIdText = new System.Windows.Forms.MaskedTextBox();
            this.searchBt = new System.Windows.Forms.Button();
            this.bankIdLabel = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.billPredRb = new System.Windows.Forms.RadioButton();
            this.greenRb = new System.Windows.Forms.RadioButton();
            this.billPredText = new System.Windows.Forms.MaskedTextBox();
            this.blueRb = new System.Windows.Forms.RadioButton();
            this.printCaptionTxt = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.greenBillListViewGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.greenBillDataGridView)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.clearButton);
            this.panel1.Controls.Add(this.printButton);
            this.panel1.Controls.Add(this.greenBillListViewGroupBox);
            this.panel1.Controls.Add(this.groupBox6);
            this.panel1.Controls.Add(this.printCaptionTxt);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(250, 608);
            this.panel1.TabIndex = 6;
            // 
            // clearButton
            // 
            this.clearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.clearButton.Location = new System.Drawing.Point(131, 563);
            this.clearButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(105, 27);
            this.clearButton.TabIndex = 2;
            this.clearButton.Text = "¡��ԡ";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // printButton
            // 
            this.printButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.printButton.Location = new System.Drawing.Point(14, 563);
            this.printButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.printButton.Name = "printButton";
            this.printButton.Size = new System.Drawing.Size(105, 27);
            this.printButton.TabIndex = 1;
            this.printButton.Text = "�����";
            this.printButton.UseVisualStyleBackColor = true;
            this.printButton.Click += new System.EventHandler(this.printButton_Click);
            // 
            // greenBillListViewGroupBox
            // 
            this.greenBillListViewGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.greenBillListViewGroupBox.Controls.Add(this.checkAllCheckBox);
            this.greenBillListViewGroupBox.Controls.Add(this.greenBillDataGridView);
            this.greenBillListViewGroupBox.Location = new System.Drawing.Point(3, 178);
            this.greenBillListViewGroupBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.greenBillListViewGroupBox.Name = "greenBillListViewGroupBox";
            this.greenBillListViewGroupBox.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.greenBillListViewGroupBox.Size = new System.Drawing.Size(244, 377);
            this.greenBillListViewGroupBox.TabIndex = 3;
            this.greenBillListViewGroupBox.TabStop = false;
            this.greenBillListViewGroupBox.Text = "��¡�á�͹����� ( 0 ��¡�� )";
            // 
            // checkAllCheckBox
            // 
            this.checkAllCheckBox.AutoSize = true;
            this.checkAllCheckBox.Location = new System.Drawing.Point(8, 26);
            this.checkAllCheckBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkAllCheckBox.Name = "checkAllCheckBox";
            this.checkAllCheckBox.Size = new System.Drawing.Size(15, 14);
            this.checkAllCheckBox.TabIndex = 0;
            this.checkAllCheckBox.UseVisualStyleBackColor = true;
            this.checkAllCheckBox.Click += new System.EventHandler(this.checkAllCheckBox_Click);
            // 
            // greenBillDataGridView
            // 
            this.greenBillDataGridView.AllowUserToAddRows = false;
            this.greenBillDataGridView.AllowUserToDeleteRows = false;
            this.greenBillDataGridView.AllowUserToResizeColumns = false;
            this.greenBillDataGridView.AllowUserToResizeRows = false;
            this.greenBillDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.greenBillDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.checkColumn,
            this.fromInvoiceColumn,
            this.toInvoiceColumn,
            this.lotNoColumn,
            this.bankIdColumn,
            this.dueDateColumn,
            this.BillType,
            this.BillPred,
            this.PrintDetail});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(211, 117, 192);
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.greenBillDataGridView.DefaultCellStyle = dataGridViewCellStyle1;
            this.greenBillDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.greenBillDataGridView.Location = new System.Drawing.Point(3, 20);
            this.greenBillDataGridView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.greenBillDataGridView.Name = "greenBillDataGridView";
            this.greenBillDataGridView.RowHeadersVisible = false;
            this.greenBillDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.greenBillDataGridView.Size = new System.Drawing.Size(238, 353);
            this.greenBillDataGridView.TabIndex = 9;
            this.greenBillDataGridView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.greenBillDataGridView_RowsAdded);
            this.greenBillDataGridView.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.greenBillDataGridView_RowsRemoved);
            this.greenBillDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.greenBillDataGridView_CellContentClick);
            // 
            // checkColumn
            // 
            this.checkColumn.FillWeight = 50F;
            this.checkColumn.HeaderText = "";
            this.checkColumn.Name = "checkColumn";
            this.checkColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.checkColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.checkColumn.Width = 19;
            // 
            // fromInvoiceColumn
            // 
            this.fromInvoiceColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.fromInvoiceColumn.DataPropertyName = "FromCaId";
            this.fromInvoiceColumn.HeaderText = "�ҡ";
            this.fromInvoiceColumn.MaxInputLength = 30;
            this.fromInvoiceColumn.Name = "fromInvoiceColumn";
            this.fromInvoiceColumn.ReadOnly = true;
            this.fromInvoiceColumn.Visible = false;
            // 
            // toInvoiceColumn
            // 
            this.toInvoiceColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.toInvoiceColumn.DataPropertyName = "ToCaId";
            this.toInvoiceColumn.HeaderText = "�֧";
            this.toInvoiceColumn.Name = "toInvoiceColumn";
            this.toInvoiceColumn.Visible = false;
            // 
            // lotNoColumn
            // 
            this.lotNoColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.lotNoColumn.DataPropertyName = "BankLotNo";
            this.lotNoColumn.HeaderText = "��͵";
            this.lotNoColumn.Name = "lotNoColumn";
            this.lotNoColumn.Visible = false;
            this.lotNoColumn.Width = 40;
            // 
            // bankIdColumn
            // 
            this.bankIdColumn.DataPropertyName = "BankId";
            this.bankIdColumn.HeaderText = "bankId";
            this.bankIdColumn.Name = "bankIdColumn";
            this.bankIdColumn.Visible = false;
            // 
            // dueDateColumn
            // 
            this.dueDateColumn.DataPropertyName = "DueDate";
            this.dueDateColumn.HeaderText = "dueDate";
            this.dueDateColumn.Name = "dueDateColumn";
            this.dueDateColumn.Visible = false;
            // 
            // BillType
            // 
            this.BillType.DataPropertyName = "BillType";
            this.BillType.HeaderText = "BillType";
            this.BillType.Name = "BillType";
            this.BillType.Visible = false;
            // 
            // BillPred
            // 
            this.BillPred.DataPropertyName = "BillPred";
            this.BillPred.HeaderText = "BillPred";
            this.BillPred.Name = "BillPred";
            this.BillPred.Visible = false;
            // 
            // PrintDetail
            // 
            this.PrintDetail.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PrintDetail.DataPropertyName = "PrintDetail";
            this.PrintDetail.HeaderText = "��˹��ѡ�ѭ��/��Ҥ��/��͵";
            this.PrintDetail.Name = "PrintDetail";
            this.PrintDetail.ReadOnly = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox6.Controls.Add(this.bankSearchBt);
            this.groupBox6.Controls.Add(this.label2);
            this.groupBox6.Controls.Add(this.bankDueDateText);
            this.groupBox6.Controls.Add(this.bankIdText);
            this.groupBox6.Controls.Add(this.groupBox2);
            this.groupBox6.Controls.Add(this.searchBt);
            this.groupBox6.Controls.Add(this.bankIdLabel);
            this.groupBox6.Location = new System.Drawing.Point(3, 47);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox6.Size = new System.Drawing.Size(244, 123);
            this.groupBox6.TabIndex = 2;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "������駾���������";
            // 
            // bankSearchBt
            // 
            this.bankSearchBt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.bankSearchBt.Location = new System.Drawing.Point(202, 22);
            this.bankSearchBt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bankSearchBt.Name = "bankSearchBt";
            this.bankSearchBt.Size = new System.Drawing.Size(34, 25);
            this.bankSearchBt.TabIndex = 4;
            this.bankSearchBt.Text = "...";
            this.bankSearchBt.UseVisualStyleBackColor = true;
            this.bankSearchBt.Click += new System.EventHandler(this.bankSearchBt_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(7, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "�ѹ��˹��ѡ�ѭ�� :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bankDueDateText
            // 
            this.bankDueDateText.Location = new System.Drawing.Point(125, 54);
            this.bankDueDateText.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bankDueDateText.Mask = "00/00/0000";
            this.bankDueDateText.Name = "bankDueDateText";
            this.bankDueDateText.Size = new System.Drawing.Size(111, 23);
            this.bankDueDateText.TabIndex = 3;
            this.bankDueDateText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.bankDueDateText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.bankDueDateText_KeyDown);
            this.bankDueDateText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.bankDueDateText_KeyPress);
            // 
            // bankIdText
            // 
            this.bankIdText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.bankIdText.Location = new System.Drawing.Point(125, 23);
            this.bankIdText.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bankIdText.Mask = "AAAAA";
            this.bankIdText.Name = "bankIdText";
            this.bankIdText.Size = new System.Drawing.Size(71, 23);
            this.bankIdText.TabIndex = 2;
            this.bankIdText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.bankIdText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.bankIdText_KeyDown);
            this.bankIdText.TextChanged += new System.EventHandler(this.bankIdText_TextChanged);
            // 
            // searchBt
            // 
            this.searchBt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.searchBt.Location = new System.Drawing.Point(8, 85);
            this.searchBt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.searchBt.Name = "searchBt";
            this.searchBt.Size = new System.Drawing.Size(229, 30);
            this.searchBt.TabIndex = 3;
            this.searchBt.Text = "����";
            this.searchBt.UseVisualStyleBackColor = true;
            this.searchBt.Click += new System.EventHandler(this.searchBt_Click);
            // 
            // bankIdLabel
            // 
            this.bankIdLabel.Location = new System.Drawing.Point(38, 21);
            this.bankIdLabel.Name = "bankIdLabel";
            this.bankIdLabel.Size = new System.Drawing.Size(83, 25);
            this.bankIdLabel.TabIndex = 1;
            this.bankIdLabel.Text = "���ʸ�Ҥ�� :";
            this.bankIdLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.billPredRb);
            this.groupBox2.Controls.Add(this.greenRb);
            this.groupBox2.Controls.Add(this.billPredText);
            this.groupBox2.Controls.Add(this.blueRb);
            this.groupBox2.Location = new System.Drawing.Point(8, 30);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(26, 16);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "��������駤��俿�� Direct Debit";
            this.groupBox2.Visible = false;
            // 
            // billPredRb
            // 
            this.billPredRb.AutoSize = true;
            this.billPredRb.BackColor = System.Drawing.SystemColors.Control;
            this.billPredRb.Location = new System.Drawing.Point(206, 52);
            this.billPredRb.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.billPredRb.Name = "billPredRb";
            this.billPredRb.Size = new System.Drawing.Size(14, 13);
            this.billPredRb.TabIndex = 0;
            this.billPredRb.UseVisualStyleBackColor = false;
            this.billPredRb.Visible = false;
            this.billPredRb.CheckedChanged += new System.EventHandler(this.billPredRb_CheckedChanged);
            // 
            // greenRb
            // 
            this.greenRb.AutoSize = true;
            this.greenRb.BackColor = System.Drawing.SystemColors.Control;
            this.greenRb.Checked = true;
            this.greenRb.Location = new System.Drawing.Point(14, 22);
            this.greenRb.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.greenRb.Name = "greenRb";
            this.greenRb.Size = new System.Drawing.Size(149, 20);
            this.greenRb.TabIndex = 0;
            this.greenRb.TabStop = true;
            this.greenRb.Text = "��Ū����Թ��ҹ��Ҥ��";
            this.greenRb.UseVisualStyleBackColor = false;
            this.greenRb.KeyDown += new System.Windows.Forms.KeyEventHandler(this.greenRb_KeyDown);
            // 
            // billPredText
            // 
            this.billPredText.Enabled = false;
            this.billPredText.Location = new System.Drawing.Point(222, 47);
            this.billPredText.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.billPredText.Mask = "00/0000";
            this.billPredText.Name = "billPredText";
            this.billPredText.Size = new System.Drawing.Size(10, 23);
            this.billPredText.TabIndex = 1;
            this.billPredText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.billPredText.Visible = false;
            this.billPredText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.billPredText_KeyDown);
            // 
            // blueRb
            // 
            this.blueRb.AutoSize = true;
            this.blueRb.Location = new System.Drawing.Point(14, 48);
            this.blueRb.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.blueRb.Name = "blueRb";
            this.blueRb.Size = new System.Drawing.Size(186, 20);
            this.blueRb.TabIndex = 1;
            this.blueRb.Text = "��Ū����Թ��ҹ��Ҥ�� (�տ��)";
            this.blueRb.UseVisualStyleBackColor = true;
            this.blueRb.KeyDown += new System.Windows.Forms.KeyEventHandler(this.blueRb_KeyDown);
            // 
            // printCaptionTxt
            // 
            this.printCaptionTxt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.printCaptionTxt.BackColor = System.Drawing.Color.FromArgb(211, 117, 192);
            this.printCaptionTxt.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.printCaptionTxt.ForeColor = System.Drawing.Color.White;
            this.printCaptionTxt.Location = new System.Drawing.Point(-1, 0);
            this.printCaptionTxt.Name = "printCaptionTxt";
            this.printCaptionTxt.Size = new System.Drawing.Size(250, 42);
            this.printCaptionTxt.TabIndex = 0;
            this.printCaptionTxt.Text = "�������駤��俿��\r\n �ѡ�ѭ�ո�Ҥ��";
            this.printCaptionTxt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GreenBillView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "GreenBillView";
            this.Size = new System.Drawing.Size(250, 608);
            this.panel1.ResumeLayout(false);
            this.greenBillListViewGroupBox.ResumeLayout(false);
            this.greenBillListViewGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.greenBillDataGridView)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox greenBillListViewGroupBox;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Button printButton;
        private System.Windows.Forms.DataGridView greenBillDataGridView;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label printCaptionTxt;
        private System.Windows.Forms.Label bankIdLabel;
        private System.Windows.Forms.MaskedTextBox bankDueDateText;
        private System.Windows.Forms.CheckBox checkAllCheckBox;
        private System.Windows.Forms.MaskedTextBox bankIdText;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton greenRb;
        private System.Windows.Forms.RadioButton blueRb;
        private System.Windows.Forms.RadioButton billPredRb;
        private System.Windows.Forms.MaskedTextBox billPredText;
        private System.Windows.Forms.Button bankSearchBt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button searchBt;
        private System.Windows.Forms.DataGridViewCheckBoxColumn checkColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fromInvoiceColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn toInvoiceColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lotNoColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn bankIdColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dueDateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn BillType;
        private System.Windows.Forms.DataGridViewTextBoxColumn BillPred;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrintDetail;
    }
}

