
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
    partial class DualBillView
    {
        /// <summary>
        /// The presenter used by this view.
        /// </summary>
        private PEA.BPM.BillPrintingModule.DualBillViewPresenter _presenter = null;

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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.billCorrectGb = new System.Windows.Forms.GroupBox();
            this.normalBillRb = new System.Windows.Forms.RadioButton();
            this.fixedBillRb = new System.Windows.Forms.RadioButton();
            this.clearButton = new System.Windows.Forms.Button();
            this.printButton = new System.Windows.Forms.Button();
            this.billTypeGroupBox = new System.Windows.Forms.GroupBox();
            this.spotbillRadioButton = new System.Windows.Forms.RadioButton();
            this.blueBillRadioButton = new System.Windows.Forms.RadioButton();
            this.greenBillRadioButton = new System.Windows.Forms.RadioButton();
            this.dualBillListViewGroupBox = new System.Windows.Forms.GroupBox();
            this.dualBillDataGridView = new System.Windows.Forms.DataGridView();
            this.checkColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.electricityIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.delColumn = new System.Windows.Forms.DataGridViewLinkColumn();
            this.branchIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mruIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gvMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Item1 = new System.Windows.Forms.ToolStripMenuItem();
            this.electricIdGroupBox = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.userIdMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.toMruIdMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.mruIdMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.branchIdMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.printConditionGroupBox = new System.Windows.Forms.GroupBox();
            this.printUserRadioButton = new System.Windows.Forms.RadioButton();
            this.printMruRadioButton = new System.Windows.Forms.RadioButton();
            this.printBranchRadioButton = new System.Windows.Forms.RadioButton();
            this.printAllRadioButton = new System.Windows.Forms.RadioButton();
            this.billPeriodGroupBox = new System.Windows.Forms.GroupBox();
            this.billPeriodMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.billCorrectGb.SuspendLayout();
            this.billTypeGroupBox.SuspendLayout();
            this.dualBillListViewGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dualBillDataGridView)).BeginInit();
            this.gvMenuStrip.SuspendLayout();
            this.electricIdGroupBox.SuspendLayout();
            this.printConditionGroupBox.SuspendLayout();
            this.billPeriodGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.billCorrectGb);
            this.panel1.Controls.Add(this.clearButton);
            this.panel1.Controls.Add(this.printButton);
            this.panel1.Controls.Add(this.billTypeGroupBox);
            this.panel1.Controls.Add(this.dualBillListViewGroupBox);
            this.panel1.Controls.Add(this.electricIdGroupBox);
            this.panel1.Controls.Add(this.printConditionGroupBox);
            this.panel1.Controls.Add(this.billPeriodGroupBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(250, 650);
            this.panel1.TabIndex = 5;
            // 
            // billCorrectGb
            // 
            this.billCorrectGb.Controls.Add(this.normalBillRb);
            this.billCorrectGb.Controls.Add(this.fixedBillRb);
            this.billCorrectGb.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.billCorrectGb.Location = new System.Drawing.Point(5, 107);
            this.billCorrectGb.Name = "billCorrectGb";
            this.billCorrectGb.Size = new System.Drawing.Size(242, 50);
            this.billCorrectGb.TabIndex = 5;
            this.billCorrectGb.TabStop = false;
            this.billCorrectGb.Text = "ʶҹк��";
            // 
            // normalBillRb
            // 
            this.normalBillRb.AutoSize = true;
            this.normalBillRb.Checked = true;
            this.normalBillRb.Location = new System.Drawing.Point(38, 20);
            this.normalBillRb.Name = "normalBillRb";
            this.normalBillRb.Size = new System.Drawing.Size(66, 20);
            this.normalBillRb.TabIndex = 0;
            this.normalBillRb.TabStop = true;
            this.normalBillRb.Text = "��Ż���";
            this.normalBillRb.UseVisualStyleBackColor = true;
            // 
            // fixedBillRb
            // 
            this.fixedBillRb.AutoSize = true;
            this.fixedBillRb.BackColor = System.Drawing.SystemColors.Control;
            this.fixedBillRb.Location = new System.Drawing.Point(140, 20);
            this.fixedBillRb.Name = "fixedBillRb";
            this.fixedBillRb.Size = new System.Drawing.Size(73, 20);
            this.fixedBillRb.TabIndex = 1;
            this.fixedBillRb.TabStop = true;
            this.fixedBillRb.Text = "������";
            this.fixedBillRb.UseVisualStyleBackColor = false;
            // 
            // clearButton
            // 
            this.clearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.clearButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.clearButton.Location = new System.Drawing.Point(129, 615);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(110, 27);
            this.clearButton.TabIndex = 2;
            this.clearButton.Text = "¡��ԡ";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // printButton
            // 
            this.printButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.printButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.printButton.Location = new System.Drawing.Point(10, 615);
            this.printButton.Name = "printButton";
            this.printButton.Size = new System.Drawing.Size(110, 27);
            this.printButton.TabIndex = 1;
            this.printButton.Text = "�ʴ���¡��";
            this.printButton.UseVisualStyleBackColor = true;
            this.printButton.Click += new System.EventHandler(this.printButton_Click);
            // 
            // billTypeGroupBox
            // 
            this.billTypeGroupBox.Controls.Add(this.spotbillRadioButton);
            this.billTypeGroupBox.Controls.Add(this.blueBillRadioButton);
            this.billTypeGroupBox.Controls.Add(this.greenBillRadioButton);
            this.billTypeGroupBox.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.billTypeGroupBox.Location = new System.Drawing.Point(5, 34);
            this.billTypeGroupBox.Name = "billTypeGroupBox";
            this.billTypeGroupBox.Size = new System.Drawing.Size(242, 70);
            this.billTypeGroupBox.TabIndex = 0;
            this.billTypeGroupBox.TabStop = false;
            this.billTypeGroupBox.Text = "���������";
            // 
            // spotbillRadioButton
            // 
            this.spotbillRadioButton.AutoSize = true;
            this.spotbillRadioButton.BackColor = System.Drawing.SystemColors.Control;
            this.spotbillRadioButton.Location = new System.Drawing.Point(23, 43);
            this.spotbillRadioButton.Name = "spotbillRadioButton";
            this.spotbillRadioButton.Size = new System.Drawing.Size(118, 20);
            this.spotbillRadioButton.TabIndex = 2;
            this.spotbillRadioButton.TabStop = true;
            this.spotbillRadioButton.Text = "spotbill �ҧ�����";
            this.spotbillRadioButton.UseVisualStyleBackColor = false;
            // 
            // blueBillRadioButton
            // 
            this.blueBillRadioButton.AutoSize = true;
            this.blueBillRadioButton.Checked = true;
            this.blueBillRadioButton.Location = new System.Drawing.Point(23, 20);
            this.blueBillRadioButton.Name = "blueBillRadioButton";
            this.blueBillRadioButton.Size = new System.Drawing.Size(81, 20);
            this.blueBillRadioButton.TabIndex = 0;
            this.blueBillRadioButton.TabStop = true;
            this.blueBillRadioButton.Text = "��ŵ��᷹";
            this.blueBillRadioButton.UseVisualStyleBackColor = true;
            this.blueBillRadioButton.CheckedChanged += new System.EventHandler(this.blueBillRadioButton_CheckedChanged);
            // 
            // greenBillRadioButton
            // 
            this.greenBillRadioButton.AutoSize = true;
            this.greenBillRadioButton.BackColor = System.Drawing.SystemColors.Control;
            this.greenBillRadioButton.Location = new System.Drawing.Point(140, 20);
            this.greenBillRadioButton.Name = "greenBillRadioButton";
            this.greenBillRadioButton.Size = new System.Drawing.Size(83, 20);
            this.greenBillRadioButton.TabIndex = 1;
            this.greenBillRadioButton.TabStop = true;
            this.greenBillRadioButton.Text = "��Ÿ�Ҥ��";
            this.greenBillRadioButton.UseVisualStyleBackColor = false;
            this.greenBillRadioButton.CheckedChanged += new System.EventHandler(this.greenBillRadioButton_CheckedChanged);
            // 
            // dualBillListViewGroupBox
            // 
            this.dualBillListViewGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dualBillListViewGroupBox.Controls.Add(this.dualBillDataGridView);
            this.dualBillListViewGroupBox.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dualBillListViewGroupBox.Location = new System.Drawing.Point(3, 413);
            this.dualBillListViewGroupBox.Name = "dualBillListViewGroupBox";
            this.dualBillListViewGroupBox.Size = new System.Drawing.Size(244, 196);
            this.dualBillListViewGroupBox.TabIndex = 4;
            this.dualBillListViewGroupBox.TabStop = false;
            this.dualBillListViewGroupBox.Text = "��¡�á�͹�����";
            // 
            // dualBillDataGridView
            // 
            this.dualBillDataGridView.AllowUserToAddRows = false;
            this.dualBillDataGridView.AllowUserToDeleteRows = false;
            this.dualBillDataGridView.AllowUserToResizeColumns = false;
            this.dualBillDataGridView.AllowUserToResizeRows = false;
            this.dualBillDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dualBillDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dualBillDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.checkColumn,
            this.electricityIdColumn,
            this.delColumn,
            this.branchIdColumn,
            this.mruIdColumn});
            this.dualBillDataGridView.ContextMenuStrip = this.gvMenuStrip;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(211, 117, 192);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dualBillDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.dualBillDataGridView.Location = new System.Drawing.Point(7, 19);
            this.dualBillDataGridView.Name = "dualBillDataGridView";
            this.dualBillDataGridView.RowHeadersVisible = false;
            this.dualBillDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dualBillDataGridView.Size = new System.Drawing.Size(229, 171);
            this.dualBillDataGridView.TabIndex = 0;
            this.dualBillDataGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dualBillDataGridView_KeyDown);
            this.dualBillDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dualBillDataGridView_CellContentClick);
            // 
            // checkColumn
            // 
            this.checkColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.checkColumn.FillWeight = 50F;
            this.checkColumn.HeaderText = "";
            this.checkColumn.Name = "checkColumn";
            this.checkColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
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
            this.delColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.delColumn.Text = "ź";
            this.delColumn.UseColumnTextForLinkValue = true;
            this.delColumn.Width = 30;
            // 
            // branchIdColumn
            // 
            this.branchIdColumn.HeaderText = "���ʡ��";
            this.branchIdColumn.Name = "branchIdColumn";
            this.branchIdColumn.Visible = false;
            // 
            // mruIdColumn
            // 
            this.mruIdColumn.HeaderText = "���";
            this.mruIdColumn.Name = "mruIdColumn";
            this.mruIdColumn.Visible = false;
            // 
            // gvMenuStrip
            // 
            this.gvMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Item1});
            this.gvMenuStrip.Name = "gvMenuStrip";
            this.gvMenuStrip.Size = new System.Drawing.Size(84, 26);
            // 
            // Item1
            // 
            this.Item1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Item1.Name = "Item1";
            this.Item1.ShowShortcutKeys = false;
            this.Item1.Size = new System.Drawing.Size(83, 22);
            this.Item1.Text = "�ҧ";
            this.Item1.Click += new System.EventHandler(this.Item1_Click);
            // 
            // electricIdGroupBox
            // 
            this.electricIdGroupBox.Controls.Add(this.label6);
            this.electricIdGroupBox.Controls.Add(this.userIdMaskedTextBox);
            this.electricIdGroupBox.Controls.Add(this.toMruIdMaskedTextBox);
            this.electricIdGroupBox.Controls.Add(this.label4);
            this.electricIdGroupBox.Controls.Add(this.mruIdMaskedTextBox);
            this.electricIdGroupBox.Controls.Add(this.branchIdMaskedTextBox);
            this.electricIdGroupBox.Controls.Add(this.label5);
            this.electricIdGroupBox.Controls.Add(this.label2);
            this.electricIdGroupBox.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.electricIdGroupBox.Location = new System.Drawing.Point(5, 282);
            this.electricIdGroupBox.Name = "electricIdGroupBox";
            this.electricIdGroupBox.Size = new System.Drawing.Size(242, 125);
            this.electricIdGroupBox.TabIndex = 3;
            this.electricIdGroupBox.TabStop = false;
            this.electricIdGroupBox.Text = "�����š�þ����";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label6.Location = new System.Drawing.Point(11, 94);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 20);
            this.label6.TabIndex = 26;
            this.label6.Text = "�Ţ������ :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // userIdMaskedTextBox
            // 
            this.userIdMaskedTextBox.Enabled = false;
            this.userIdMaskedTextBox.Location = new System.Drawing.Point(111, 94);
            this.userIdMaskedTextBox.Mask = "000000000000";
            this.userIdMaskedTextBox.Name = "userIdMaskedTextBox";
            this.userIdMaskedTextBox.Size = new System.Drawing.Size(124, 23);
            this.userIdMaskedTextBox.TabIndex = 25;
            this.userIdMaskedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.userIdMaskedTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.userIdMaskedTextBox_KeyDown);
            // 
            // toMruIdMaskedTextBox
            // 
            this.toMruIdMaskedTextBox.Location = new System.Drawing.Point(111, 69);
            this.toMruIdMaskedTextBox.Mask = "0000";
            this.toMruIdMaskedTextBox.Name = "toMruIdMaskedTextBox";
            this.toMruIdMaskedTextBox.Size = new System.Drawing.Size(124, 23);
            this.toMruIdMaskedTextBox.TabIndex = 2;
            this.toMruIdMaskedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toMruIdMaskedTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toMruIdMaskedTextBox_KeyDown);
            this.toMruIdMaskedTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.toMruIdMaskedTextBox_KeyPress);
            this.toMruIdMaskedTextBox.TextChanged += new System.EventHandler(this.toMruIdMaskedTextBox_TextChanged);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label4.Location = new System.Drawing.Point(11, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 20);
            this.label4.TabIndex = 24;
            this.label4.Text = "�֧��� :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mruIdMaskedTextBox
            // 
            this.mruIdMaskedTextBox.Location = new System.Drawing.Point(111, 44);
            this.mruIdMaskedTextBox.Mask = "0000";
            this.mruIdMaskedTextBox.Name = "mruIdMaskedTextBox";
            this.mruIdMaskedTextBox.Size = new System.Drawing.Size(124, 23);
            this.mruIdMaskedTextBox.TabIndex = 1;
            this.mruIdMaskedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mruIdMaskedTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mruIdMaskedTextBox_KeyDown);
            this.mruIdMaskedTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mruIdMaskedTextBox_KeyPress);
            this.mruIdMaskedTextBox.TextChanged += new System.EventHandler(this.mruIdMaskedTextBox_TextChanged);
            // 
            // branchIdMaskedTextBox
            // 
            this.branchIdMaskedTextBox.Location = new System.Drawing.Point(111, 19);
            this.branchIdMaskedTextBox.Mask = "A00000";
            this.branchIdMaskedTextBox.Name = "branchIdMaskedTextBox";
            this.branchIdMaskedTextBox.Size = new System.Drawing.Size(124, 23);
            this.branchIdMaskedTextBox.TabIndex = 0;
            this.branchIdMaskedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.branchIdMaskedTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.branchIdMaskedTextBox_KeyDown);
            this.branchIdMaskedTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.branchIdMaskedTextBox_KeyPress);
            this.branchIdMaskedTextBox.TextChanged += new System.EventHandler(this.branchIdMaskedTextBox_TextChanged);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label5.Location = new System.Drawing.Point(11, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 20);
            this.label5.TabIndex = 2;
            this.label5.Text = "���ʡ��俿�� :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(11, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 20);
            this.label2.TabIndex = 23;
            this.label2.Text = "�ҡ��� :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // printConditionGroupBox
            // 
            this.printConditionGroupBox.Controls.Add(this.printUserRadioButton);
            this.printConditionGroupBox.Controls.Add(this.printMruRadioButton);
            this.printConditionGroupBox.Controls.Add(this.printBranchRadioButton);
            this.printConditionGroupBox.Controls.Add(this.printAllRadioButton);
            this.printConditionGroupBox.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.printConditionGroupBox.Location = new System.Drawing.Point(5, 213);
            this.printConditionGroupBox.Name = "printConditionGroupBox";
            this.printConditionGroupBox.Size = new System.Drawing.Size(242, 66);
            this.printConditionGroupBox.TabIndex = 2;
            this.printConditionGroupBox.TabStop = false;
            this.printConditionGroupBox.Text = "�ͺࢵ��þ����";
            // 
            // printUserRadioButton
            // 
            this.printUserRadioButton.AutoSize = true;
            this.printUserRadioButton.BackColor = System.Drawing.SystemColors.Control;
            this.printUserRadioButton.Location = new System.Drawing.Point(123, 40);
            this.printUserRadioButton.Name = "printUserRadioButton";
            this.printUserRadioButton.Size = new System.Drawing.Size(114, 20);
            this.printUserRadioButton.TabIndex = 4;
            this.printUserRadioButton.Text = "�����Ţ������";
            this.printUserRadioButton.UseVisualStyleBackColor = false;
            this.printUserRadioButton.CheckedChanged += new System.EventHandler(this.printUserRadioButton_CheckedChanged);
            // 
            // printMruRadioButton
            // 
            this.printMruRadioButton.AutoSize = true;
            this.printMruRadioButton.BackColor = System.Drawing.SystemColors.Control;
            this.printMruRadioButton.Location = new System.Drawing.Point(9, 40);
            this.printMruRadioButton.Name = "printMruRadioButton";
            this.printMruRadioButton.Size = new System.Drawing.Size(113, 20);
            this.printMruRadioButton.TabIndex = 2;
            this.printMruRadioButton.Text = "��¡�è�˹���";
            this.printMruRadioButton.UseVisualStyleBackColor = false;
            this.printMruRadioButton.CheckedChanged += new System.EventHandler(this.printMruRadioButton_CheckedChanged);
            // 
            // printBranchRadioButton
            // 
            this.printBranchRadioButton.AutoSize = true;
            this.printBranchRadioButton.Location = new System.Drawing.Point(123, 16);
            this.printBranchRadioButton.Name = "printBranchRadioButton";
            this.printBranchRadioButton.Size = new System.Drawing.Size(77, 20);
            this.printBranchRadioButton.TabIndex = 1;
            this.printBranchRadioButton.Text = "���俿��";
            this.printBranchRadioButton.UseVisualStyleBackColor = true;
            this.printBranchRadioButton.CheckedChanged += new System.EventHandler(this.printBranchRadioButton_CheckedChanged);
            // 
            // printAllRadioButton
            // 
            this.printAllRadioButton.AutoSize = true;
            this.printAllRadioButton.Checked = true;
            this.printAllRadioButton.Location = new System.Drawing.Point(9, 16);
            this.printAllRadioButton.Name = "printAllRadioButton";
            this.printAllRadioButton.Size = new System.Drawing.Size(65, 20);
            this.printAllRadioButton.TabIndex = 0;
            this.printAllRadioButton.TabStop = true;
            this.printAllRadioButton.Text = "������";
            this.printAllRadioButton.UseVisualStyleBackColor = true;
            this.printAllRadioButton.CheckedChanged += new System.EventHandler(this.printAllRadioButton_CheckedChanged);
            // 
            // billPeriodGroupBox
            // 
            this.billPeriodGroupBox.Controls.Add(this.billPeriodMaskedTextBox);
            this.billPeriodGroupBox.Controls.Add(this.label3);
            this.billPeriodGroupBox.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.billPeriodGroupBox.Location = new System.Drawing.Point(5, 160);
            this.billPeriodGroupBox.Name = "billPeriodGroupBox";
            this.billPeriodGroupBox.Size = new System.Drawing.Size(242, 50);
            this.billPeriodGroupBox.TabIndex = 1;
            this.billPeriodGroupBox.TabStop = false;
            this.billPeriodGroupBox.Text = "�����͹";
            // 
            // billPeriodMaskedTextBox
            // 
            this.billPeriodMaskedTextBox.Location = new System.Drawing.Point(149, 17);
            this.billPeriodMaskedTextBox.Mask = "00/0000";
            this.billPeriodMaskedTextBox.Name = "billPeriodMaskedTextBox";
            this.billPeriodMaskedTextBox.Size = new System.Drawing.Size(74, 23);
            this.billPeriodMaskedTextBox.TabIndex = 0;
            this.billPeriodMaskedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.billPeriodMaskedTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.billPeriodMaskedTextBox_KeyDown);
            this.billPeriodMaskedTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.billPeriodMaskedTextBox_KeyPress);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(6, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "�����͹ (��/����) :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(211, 117, 192);
            this.label1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(-1, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(250, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "�������駤��俿��";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DualBillView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.panel1);
            this.Name = "DualBillView";
            this.Size = new System.Drawing.Size(253, 653);
            this.panel1.ResumeLayout(false);
            this.billCorrectGb.ResumeLayout(false);
            this.billCorrectGb.PerformLayout();
            this.billTypeGroupBox.ResumeLayout(false);
            this.billTypeGroupBox.PerformLayout();
            this.dualBillListViewGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dualBillDataGridView)).EndInit();
            this.gvMenuStrip.ResumeLayout(false);
            this.electricIdGroupBox.ResumeLayout(false);
            this.electricIdGroupBox.PerformLayout();
            this.printConditionGroupBox.ResumeLayout(false);
            this.printConditionGroupBox.PerformLayout();
            this.billPeriodGroupBox.ResumeLayout(false);
            this.billPeriodGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox dualBillListViewGroupBox;
        private System.Windows.Forms.GroupBox electricIdGroupBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox printConditionGroupBox;
        private System.Windows.Forms.RadioButton printMruRadioButton;
        private System.Windows.Forms.RadioButton printBranchRadioButton;
        private System.Windows.Forms.RadioButton printAllRadioButton;
        private System.Windows.Forms.GroupBox billPeriodGroupBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox billPeriodMaskedTextBox;
        private System.Windows.Forms.MaskedTextBox mruIdMaskedTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox branchIdMaskedTextBox;
        private System.Windows.Forms.GroupBox billTypeGroupBox;
        private System.Windows.Forms.RadioButton greenBillRadioButton;
        private System.Windows.Forms.RadioButton blueBillRadioButton;
        private System.Windows.Forms.DataGridView dualBillDataGridView;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Button printButton;
        private System.Windows.Forms.DataGridViewCheckBoxColumn checkColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn electricityIdColumn;
        private System.Windows.Forms.DataGridViewLinkColumn delColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn branchIdColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mruIdColumn;
        private System.Windows.Forms.MaskedTextBox toMruIdMaskedTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton printUserRadioButton;
        private System.Windows.Forms.MaskedTextBox userIdMaskedTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox billCorrectGb;
        private System.Windows.Forms.RadioButton normalBillRb;
        private System.Windows.Forms.RadioButton fixedBillRb;
        private System.Windows.Forms.RadioButton spotbillRadioButton;
        private System.Windows.Forms.ContextMenuStrip gvMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem Item1;
    }
}

