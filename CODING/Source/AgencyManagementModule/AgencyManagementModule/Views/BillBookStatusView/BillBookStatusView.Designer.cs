
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

namespace PEA.BPM.AgencyManagementModule
{
    partial class BillBookStatusView
    {
        /// <summary>
        /// The presenter used by this view.
        /// </summary>
        private PEA.BPM.AgencyManagementModule.BillBookStatusViewPresenter _presenter = null;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.billbookIdText = new System.Windows.Forms.ToolStripTextBox();
            this.findBt = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.billbookGv = new System.Windows.Forms.DataGridView();
            this.Rs = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.BranchId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AgencyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreatorName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BookId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Period = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReceiveCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreateDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BillCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BillAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BookStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StatusId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.viewCancel = new System.Windows.Forms.RadioButton();
            this.viewCut = new System.Windows.Forms.RadioButton();
            this.viewNotCut = new System.Windows.Forms.RadioButton();
            this.viewAll = new System.Windows.Forms.RadioButton();
            this.printBt = new System.Windows.Forms.Button();
            this.cancelBt = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.sumCollectRep = new System.Windows.Forms.CheckBox();
            this.cantCollectRep = new System.Windows.Forms.CheckBox();
            this.billBookDetailRep = new System.Windows.Forms.CheckBox();
            this.billBookRep = new System.Windows.Forms.CheckBox();
            this.preview = new System.Windows.Forms.CheckBox();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.billbookGv)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.billbookIdText,
            this.findBt});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1024, 26);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("CordiaUPC", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(102, 23);
            this.toolStripLabel1.Text = "������ش���º��   ";
            // 
            // billbookIdText
            // 
            this.billbookIdText.Name = "billbookIdText";
            this.billbookIdText.Size = new System.Drawing.Size(303, 26);
            this.billbookIdText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.billbookIdText_KeyDown);
            // 
            // findBt
            // 
            this.findBt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.findBt.Image = global::PEA.BPM.AgencyManagementModule.Properties.Resources.ZoomHS;
            this.findBt.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.findBt.Name = "findBt";
            this.findBt.Size = new System.Drawing.Size(23, 23);
            this.findBt.Text = "����";
            this.findBt.ToolTipText = "���������";
            this.findBt.Click += new System.EventHandler(this.findBt_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.billbookGv);
            this.groupBox1.Location = new System.Drawing.Point(15, 104);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(987, 460);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "��¡����ش���º��";
            // 
            // billbookGv
            // 
            this.billbookGv.AllowUserToAddRows = false;
            this.billbookGv.AllowUserToDeleteRows = false;
            this.billbookGv.BackgroundColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.billbookGv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.billbookGv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.billbookGv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Rs,
            this.BranchId,
            this.AgencyName,
            this.CreatorName,
            this.BookId,
            this.Period,
            this.ReceiveCount,
            this.CreateDate,
            this.BillCount,
            this.BillAmount,
            this.BookStatus,
            this.StatusId});
            this.billbookGv.GridColor = System.Drawing.SystemColors.Control;
            this.billbookGv.Location = new System.Drawing.Point(11, 23);
            this.billbookGv.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.billbookGv.Name = "billbookGv";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.Gold;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.billbookGv.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.billbookGv.RowHeadersWidth = 15;
            this.billbookGv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.billbookGv.Size = new System.Drawing.Size(955, 430);
            this.billbookGv.TabIndex = 0;
            this.billbookGv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.billbookGv_CellContentClick);
            // 
            // Rs
            // 
            this.Rs.HeaderText = "���͡";
            this.Rs.Name = "Rs";
            this.Rs.Width = 40;
            // 
            // BranchId
            // 
            this.BranchId.DataPropertyName = "BranchId";
            this.BranchId.HeaderText = "�Ңҡ��俿��";
            this.BranchId.Name = "BranchId";
            this.BranchId.ReadOnly = true;
            this.BranchId.Width = 115;
            // 
            // AgencyName
            // 
            this.AgencyName.DataPropertyName = "AgencyName";
            this.AgencyName.HeaderText = "���᷹���Թ";
            this.AgencyName.Name = "AgencyName";
            this.AgencyName.ReadOnly = true;
            this.AgencyName.Width = 110;
            // 
            // CreatorName
            // 
            this.CreatorName.DataPropertyName = "CreatorName";
            this.CreatorName.HeaderText = "���Ѵ����ش";
            this.CreatorName.Name = "CreatorName";
            this.CreatorName.ReadOnly = true;
            // 
            // BookId
            // 
            this.BookId.DataPropertyName = "BillbookIdText";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.BookId.DefaultCellStyle = dataGridViewCellStyle2;
            this.BookId.HeaderText = "�Ţ�����ش���º��";
            this.BookId.Name = "BookId";
            this.BookId.ReadOnly = true;
            this.BookId.Width = 125;
            // 
            // Period
            // 
            this.Period.DataPropertyName = "Period";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Period.DefaultCellStyle = dataGridViewCellStyle3;
            this.Period.HeaderText = "�����͹";
            this.Period.Name = "Period";
            this.Period.ReadOnly = true;
            this.Period.Width = 80;
            // 
            // ReceiveCount
            // 
            this.ReceiveCount.DataPropertyName = "ReceiveNo";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ReceiveCount.DefaultCellStyle = dataGridViewCellStyle4;
            this.ReceiveCount.HeaderText = "���駷��/��������͡��ش";
            this.ReceiveCount.Name = "ReceiveCount";
            this.ReceiveCount.ReadOnly = true;
            this.ReceiveCount.Width = 150;
            // 
            // CreateDate
            // 
            this.CreateDate.DataPropertyName = "CreateDt";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.CreateDate.DefaultCellStyle = dataGridViewCellStyle5;
            this.CreateDate.HeaderText = "�ѹ����͡��ش";
            this.CreateDate.Name = "CreateDate";
            this.CreateDate.ReadOnly = true;
            this.CreateDate.Width = 110;
            // 
            // BillCount
            // 
            this.BillCount.DataPropertyName = "BillTotalCount";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.BillCount.DefaultCellStyle = dataGridViewCellStyle6;
            this.BillCount.HeaderText = "�ӹǹ�����";
            this.BillCount.Name = "BillCount";
            this.BillCount.ReadOnly = true;
            this.BillCount.Width = 110;
            // 
            // BillAmount
            // 
            this.BillAmount.DataPropertyName = "BookTotalAmount";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.BillAmount.DefaultCellStyle = dataGridViewCellStyle7;
            this.BillAmount.HeaderText = "�ӹǹ�Թ";
            this.BillAmount.Name = "BillAmount";
            this.BillAmount.ReadOnly = true;
            this.BillAmount.Width = 120;
            // 
            // BookStatus
            // 
            this.BookStatus.DataPropertyName = "Status";
            this.BookStatus.HeaderText = "ʶҹ�";
            this.BookStatus.Name = "BookStatus";
            this.BookStatus.ReadOnly = true;
            // 
            // StatusId
            // 
            this.StatusId.DataPropertyName = "StatusId";
            this.StatusId.HeaderText = "StatusId";
            this.StatusId.Name = "StatusId";
            this.StatusId.ReadOnly = true;
            this.StatusId.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.viewCancel);
            this.groupBox2.Controls.Add(this.viewCut);
            this.groupBox2.Controls.Add(this.viewNotCut);
            this.groupBox2.Controls.Add(this.viewAll);
            this.groupBox2.Location = new System.Drawing.Point(15, 28);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(443, 68);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "�ʴ���¡����ش���º��";
            // 
            // viewCancel
            // 
            this.viewCancel.AutoSize = true;
            this.viewCancel.Location = new System.Drawing.Point(238, 39);
            this.viewCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.viewCancel.Name = "viewCancel";
            this.viewCancel.Size = new System.Drawing.Size(148, 20);
            this.viewCancel.TabIndex = 3;
            this.viewCancel.TabStop = true;
            this.viewCancel.Text = "��ش���º�ŷ��١¡��ԡ";
            this.viewCancel.UseVisualStyleBackColor = true;
            this.viewCancel.CheckedChanged += new System.EventHandler(this.viewCancel_CheckedChanged);
            // 
            // viewCut
            // 
            this.viewCut.AutoSize = true;
            this.viewCut.Location = new System.Drawing.Point(27, 39);
            this.viewCut.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.viewCut.Name = "viewCut";
            this.viewCut.Size = new System.Drawing.Size(160, 20);
            this.viewCut.TabIndex = 2;
            this.viewCut.TabStop = true;
            this.viewCut.Text = "��ش���º�ŷ��Ѵ��������";
            this.viewCut.UseVisualStyleBackColor = true;
            this.viewCut.CheckedChanged += new System.EventHandler(this.viewCut_CheckedChanged);
            // 
            // viewNotCut
            // 
            this.viewNotCut.AutoSize = true;
            this.viewNotCut.Checked = true;
            this.viewNotCut.Location = new System.Drawing.Point(238, 17);
            this.viewNotCut.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.viewNotCut.Name = "viewNotCut";
            this.viewNotCut.Size = new System.Drawing.Size(167, 20);
            this.viewNotCut.TabIndex = 1;
            this.viewNotCut.TabStop = true;
            this.viewNotCut.Text = "��ش���º�ŷ���ѧ���Ѵ����";
            this.viewNotCut.UseVisualStyleBackColor = true;
            this.viewNotCut.CheckedChanged += new System.EventHandler(this.viewNotCut_CheckedChanged);
            // 
            // viewAll
            // 
            this.viewAll.AutoSize = true;
            this.viewAll.Location = new System.Drawing.Point(27, 17);
            this.viewAll.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.viewAll.Name = "viewAll";
            this.viewAll.Size = new System.Drawing.Size(65, 20);
            this.viewAll.TabIndex = 0;
            this.viewAll.TabStop = true;
            this.viewAll.Text = "������";
            this.viewAll.UseVisualStyleBackColor = true;
            this.viewAll.CheckedChanged += new System.EventHandler(this.viewAll_CheckedChanged);
            // 
            // printBt
            // 
            this.printBt.Image = global::PEA.BPM.AgencyManagementModule.Properties.Resources.PrintHS;
            this.printBt.Location = new System.Drawing.Point(741, 572);
            this.printBt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.printBt.Name = "printBt";
            this.printBt.Size = new System.Drawing.Size(117, 28);
            this.printBt.TabIndex = 3;
            this.printBt.Text = "  ��������";
            this.printBt.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.printBt.UseVisualStyleBackColor = true;
            this.printBt.Click += new System.EventHandler(this.printBt_Click);
            // 
            // cancelBt
            // 
            this.cancelBt.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBt.Location = new System.Drawing.Point(864, 572);
            this.cancelBt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cancelBt.Name = "cancelBt";
            this.cancelBt.Size = new System.Drawing.Size(117, 28);
            this.cancelBt.TabIndex = 4;
            this.cancelBt.Text = "�͡";
            this.cancelBt.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.sumCollectRep);
            this.groupBox3.Controls.Add(this.cantCollectRep);
            this.groupBox3.Controls.Add(this.billBookDetailRep);
            this.groupBox3.Controls.Add(this.billBookRep);
            this.groupBox3.Location = new System.Drawing.Point(465, 28);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Size = new System.Drawing.Size(403, 68);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "�ٻẺ��§ҹ";
            // 
            // sumCollectRep
            // 
            this.sumCollectRep.AutoSize = true;
            this.sumCollectRep.Enabled = false;
            this.sumCollectRep.Location = new System.Drawing.Point(226, 40);
            this.sumCollectRep.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.sumCollectRep.Name = "sumCollectRep";
            this.sumCollectRep.Size = new System.Drawing.Size(107, 20);
            this.sumCollectRep.TabIndex = 3;
            this.sumCollectRep.Text = "��ػ������Թ";
            this.sumCollectRep.UseVisualStyleBackColor = true;
            this.sumCollectRep.CheckedChanged += new System.EventHandler(this.sumCollectRep_CheckedChanged);
            // 
            // cantCollectRep
            // 
            this.cantCollectRep.AutoSize = true;
            this.cantCollectRep.Enabled = false;
            this.cantCollectRep.Location = new System.Drawing.Point(28, 40);
            this.cantCollectRep.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cantCollectRep.Name = "cantCollectRep";
            this.cantCollectRep.Size = new System.Drawing.Size(136, 20);
            this.cantCollectRep.TabIndex = 2;
            this.cantCollectRep.Text = "�١˹�������Թ�����";
            this.cantCollectRep.UseVisualStyleBackColor = true;
            this.cantCollectRep.CheckedChanged += new System.EventHandler(this.cantCollectRep_CheckedChanged);
            // 
            // billBookDetailRep
            // 
            this.billBookDetailRep.AutoSize = true;
            this.billBookDetailRep.Location = new System.Drawing.Point(226, 20);
            this.billBookDetailRep.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.billBookDetailRep.Name = "billBookDetailRep";
            this.billBookDetailRep.Size = new System.Drawing.Size(150, 20);
            this.billBookDetailRep.TabIndex = 1;
            this.billBookDetailRep.Text = "��������´��ش���º��";
            this.billBookDetailRep.UseVisualStyleBackColor = true;
            this.billBookDetailRep.CheckedChanged += new System.EventHandler(this.billBookDetailRep_CheckedChanged);
            // 
            // billBookRep
            // 
            this.billBookRep.AutoSize = true;
            this.billBookRep.Checked = true;
            this.billBookRep.CheckState = System.Windows.Forms.CheckState.Checked;
            this.billBookRep.Location = new System.Drawing.Point(28, 20);
            this.billBookRep.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.billBookRep.Name = "billBookRep";
            this.billBookRep.Size = new System.Drawing.Size(88, 20);
            this.billBookRep.TabIndex = 0;
            this.billBookRep.Text = "��ش���º��";
            this.billBookRep.UseVisualStyleBackColor = true;
            this.billBookRep.CheckedChanged += new System.EventHandler(this.billBookRep_CheckedChanged);
            // 
            // preview
            // 
            this.preview.AutoSize = true;
            this.preview.Checked = true;
            this.preview.CheckState = System.Windows.Forms.CheckState.Checked;
            this.preview.Location = new System.Drawing.Point(26, 572);
            this.preview.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.preview.Name = "preview";
            this.preview.Size = new System.Drawing.Size(149, 20);
            this.preview.TabIndex = 6;
            this.preview.Text = "�ʴ�������ҧ��͹�����";
            this.preview.UseVisualStyleBackColor = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "BillbookId";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewTextBoxColumn1.HeaderText = "�Ţ�����ش���º��";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 140;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Period";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridViewTextBoxColumn2.HeaderText = "�ѹ����͡��ش";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 80;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "ReceiveCount";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridViewTextBoxColumn3.HeaderText = "�ӹǹ�Թ";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 120;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "CreateDt";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle12;
            this.dataGridViewTextBoxColumn4.HeaderText = "�ӹǹ�����";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 120;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "BillTotalCount";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle13;
            this.dataGridViewTextBoxColumn5.HeaderText = "ʶҹ�";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 140;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "BookTotalAmount";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle14;
            this.dataGridViewTextBoxColumn6.HeaderText = "�ӹǹ�Թ";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Width = 140;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "Status";
            this.dataGridViewTextBoxColumn7.HeaderText = "ʶҹ�";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Width = 140;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "StatusId";
            this.dataGridViewTextBoxColumn8.HeaderText = "StatusId";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Visible = false;
            // 
            // BillBookStatusView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.preview);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.cancelBt);
            this.Controls.Add(this.printBt);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "BillBookStatusView";
            this.Size = new System.Drawing.Size(1024, 608);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.billbookGv)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox billbookIdText;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView billbookGv;
        private System.Windows.Forms.Button printBt;
        private System.Windows.Forms.Button cancelBt;
        private System.Windows.Forms.RadioButton viewCancel;
        private System.Windows.Forms.RadioButton viewCut;
        private System.Windows.Forms.RadioButton viewNotCut;
        private System.Windows.Forms.RadioButton viewAll;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox sumCollectRep;
        private System.Windows.Forms.CheckBox cantCollectRep;
        private System.Windows.Forms.CheckBox billBookDetailRep;
        private System.Windows.Forms.CheckBox billBookRep;
        private System.Windows.Forms.ToolStripButton findBt;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.CheckBox preview;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Rs;
        private System.Windows.Forms.DataGridViewTextBoxColumn BranchId;
        private System.Windows.Forms.DataGridViewTextBoxColumn AgencyName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreatorName;
        private System.Windows.Forms.DataGridViewTextBoxColumn BookId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Period;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReceiveCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreateDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn BillCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn BillAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn BookStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn StatusId;
    }
}

