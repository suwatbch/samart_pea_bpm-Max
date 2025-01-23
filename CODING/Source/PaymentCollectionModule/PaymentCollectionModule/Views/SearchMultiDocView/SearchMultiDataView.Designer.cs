namespace PEA.BPM.PaymentCollectionModule.Views.SearchMultiDocView
{
    partial class SearchMultiDataView
    {

        SearchMultiDataViewPresenter _presenter = null; 
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.inputDocGroupBox = new System.Windows.Forms.GroupBox();
            this.inputTextRowCountTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.clearInputButton = new System.Windows.Forms.Button();
            this.btnConvertStringToList = new System.Windows.Forms.Button();
            this.caIdInputtextBox = new System.Windows.Forms.TextBox();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.grSearch = new System.Windows.Forms.GroupBox();
            this.documentListDataGridView = new System.Windows.Forms.DataGridView();
            this.colDocumentNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colInvoiceCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Result = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.docNoCountGroupBox = new System.Windows.Forms.GroupBox();
            this.docNoCountAlltextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnCancelSearch = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.invoiceCountTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.failSearchDocCounttextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.successSearchDocTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.inputDocGroupBox.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.grSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.documentListDataGridView)).BeginInit();
            this.docNoCountGroupBox.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Location = new System.Drawing.Point(8, 10);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(10);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnCancel);
            this.splitContainer1.Panel2.Controls.Add(this.btnOK);
            this.splitContainer1.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel2_Paint);
            this.splitContainer1.Size = new System.Drawing.Size(929, 637);
            this.splitContainer1.SplitterDistance = 586;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.inputDocGroupBox);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(929, 586);
            this.splitContainer2.SplitterDistance = 306;
            this.splitContainer2.SplitterIncrement = 2;
            this.splitContainer2.TabIndex = 0;
            // 
            // inputDocGroupBox
            // 
            this.inputDocGroupBox.Controls.Add(this.inputTextRowCountTextBox);
            this.inputDocGroupBox.Controls.Add(this.label7);
            this.inputDocGroupBox.Controls.Add(this.label8);
            this.inputDocGroupBox.Controls.Add(this.clearInputButton);
            this.inputDocGroupBox.Controls.Add(this.btnConvertStringToList);
            this.inputDocGroupBox.Controls.Add(this.caIdInputtextBox);
            this.inputDocGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputDocGroupBox.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.inputDocGroupBox.Location = new System.Drawing.Point(0, 0);
            this.inputDocGroupBox.Name = "inputDocGroupBox";
            this.inputDocGroupBox.Size = new System.Drawing.Size(306, 586);
            this.inputDocGroupBox.TabIndex = 0;
            this.inputDocGroupBox.TabStop = false;
            this.inputDocGroupBox.Text = "กรอกหมายเลขใบคำร้อง";
            this.inputDocGroupBox.Enter += new System.EventHandler(this.inputDocGroupBox_Enter);
            // 
            // inputTextRowCountTextBox
            // 
            this.inputTextRowCountTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.inputTextRowCountTextBox.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.inputTextRowCountTextBox.ForeColor = System.Drawing.Color.Red;
            this.inputTextRowCountTextBox.Location = new System.Drawing.Point(87, 552);
            this.inputTextRowCountTextBox.Name = "inputTextRowCountTextBox";
            this.inputTextRowCountTextBox.ReadOnly = true;
            this.inputTextRowCountTextBox.Size = new System.Drawing.Size(50, 23);
            this.inputTextRowCountTextBox.TabIndex = 71;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label7.Location = new System.Drawing.Point(6, 555);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 16);
            this.label7.TabIndex = 69;
            this.label7.Text = "จำนวนข้อมูล";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label8.Location = new System.Drawing.Point(143, 555);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 16);
            this.label8.TabIndex = 70;
            this.label8.Text = "รายการ";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // clearInputButton
            // 
            this.clearInputButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.clearInputButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.clearInputButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.clearInputButton.ForeColor = System.Drawing.Color.Black;
            this.clearInputButton.Location = new System.Drawing.Point(198, 549);
            this.clearInputButton.Name = "clearInputButton";
            this.clearInputButton.Size = new System.Drawing.Size(102, 27);
            this.clearInputButton.TabIndex = 66;
            this.clearInputButton.Text = "ล้างค่า";
            this.clearInputButton.UseVisualStyleBackColor = false;
            this.clearInputButton.Click += new System.EventHandler(this.clearInputButton_Click);
            // 
            // btnConvertStringToList
            // 
            this.btnConvertStringToList.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnConvertStringToList.Location = new System.Drawing.Point(271, 244);
            this.btnConvertStringToList.Name = "btnConvertStringToList";
            this.btnConvertStringToList.Size = new System.Drawing.Size(32, 30);
            this.btnConvertStringToList.TabIndex = 1;
            this.btnConvertStringToList.Text = ">>";
            this.btnConvertStringToList.UseVisualStyleBackColor = true;
            this.btnConvertStringToList.Click += new System.EventHandler(this.btnConvertStringToList_Click);
            // 
            // caIdInputtextBox
            // 
            this.caIdInputtextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.caIdInputtextBox.Location = new System.Drawing.Point(8, 22);
            this.caIdInputtextBox.Multiline = true;
            this.caIdInputtextBox.Name = "caIdInputtextBox";
            this.caIdInputtextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.caIdInputtextBox.Size = new System.Drawing.Size(259, 522);
            this.caIdInputtextBox.TabIndex = 0;
            this.caIdInputtextBox.TextChanged += new System.EventHandler(this.caIdInputtextBox_TextChanged);
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.grSearch);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.docNoCountGroupBox);
            this.splitContainer3.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer3.Size = new System.Drawing.Size(619, 586);
            this.splitContainer3.SplitterDistance = 459;
            this.splitContainer3.TabIndex = 0;
            // 
            // grSearch
            // 
            this.grSearch.Controls.Add(this.documentListDataGridView);
            this.grSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grSearch.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.grSearch.Location = new System.Drawing.Point(0, 0);
            this.grSearch.Name = "grSearch";
            this.grSearch.Size = new System.Drawing.Size(619, 459);
            this.grSearch.TabIndex = 3;
            this.grSearch.TabStop = false;
            this.grSearch.Text = "ค้นหาข้อมูล ";
            // 
            // documentListDataGridView
            // 
            this.documentListDataGridView.AllowUserToAddRows = false;
            this.documentListDataGridView.AllowUserToDeleteRows = false;
            this.documentListDataGridView.AllowUserToResizeColumns = false;
            this.documentListDataGridView.AllowUserToResizeRows = false;
            this.documentListDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.documentListDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.documentListDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDocumentNo,
            this.Status,
            this.colInvoiceCount,
            this.Result});
            this.documentListDataGridView.Location = new System.Drawing.Point(6, 22);
            this.documentListDataGridView.Name = "documentListDataGridView";
            this.documentListDataGridView.ReadOnly = true;
            this.documentListDataGridView.Size = new System.Drawing.Size(606, 437);
            this.documentListDataGridView.TabIndex = 0;
            // 
            // colDocumentNo
            // 
            this.colDocumentNo.DataPropertyName = "DocumentNo";
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.colDocumentNo.DefaultCellStyle = dataGridViewCellStyle1;
            this.colDocumentNo.FillWeight = 150F;
            this.colDocumentNo.HeaderText = "หมายเลขใบคำร้อง";
            this.colDocumentNo.Name = "colDocumentNo";
            this.colDocumentNo.ReadOnly = true;
            this.colDocumentNo.Width = 150;
            // 
            // Status
            // 
            this.Status.DataPropertyName = "Status";
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Status.DefaultCellStyle = dataGridViewCellStyle2;
            this.Status.HeaderText = "สถานะ";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            // 
            // colInvoiceCount
            // 
            this.colInvoiceCount.DataPropertyName = "InvoiceCount";
            this.colInvoiceCount.HeaderText = "จำนวนหนี้";
            this.colInvoiceCount.Name = "colInvoiceCount";
            this.colInvoiceCount.ReadOnly = true;
            // 
            // Result
            // 
            this.Result.DataPropertyName = "Result";
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Result.DefaultCellStyle = dataGridViewCellStyle3;
            this.Result.HeaderText = "หมายเหตุ";
            this.Result.Name = "Result";
            this.Result.ReadOnly = true;
            this.Result.Width = 200;
            // 
            // docNoCountGroupBox
            // 
            this.docNoCountGroupBox.Controls.Add(this.docNoCountAlltextBox);
            this.docNoCountGroupBox.Controls.Add(this.label10);
            this.docNoCountGroupBox.Controls.Add(this.label9);
            this.docNoCountGroupBox.Controls.Add(this.btnSearch);
            this.docNoCountGroupBox.Controls.Add(this.btnCancelSearch);
            this.docNoCountGroupBox.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.docNoCountGroupBox.Location = new System.Drawing.Point(6, 6);
            this.docNoCountGroupBox.Name = "docNoCountGroupBox";
            this.docNoCountGroupBox.Size = new System.Drawing.Size(234, 114);
            this.docNoCountGroupBox.TabIndex = 0;
            this.docNoCountGroupBox.TabStop = false;
            this.docNoCountGroupBox.Text = "ข้อมูลหมายเลขใบคำร้อง";
            // 
            // docNoCountAlltextBox
            // 
            this.docNoCountAlltextBox.BackColor = System.Drawing.SystemColors.Window;
            this.docNoCountAlltextBox.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.docNoCountAlltextBox.ForeColor = System.Drawing.Color.MediumBlue;
            this.docNoCountAlltextBox.Location = new System.Drawing.Point(102, 22);
            this.docNoCountAlltextBox.Name = "docNoCountAlltextBox";
            this.docNoCountAlltextBox.ReadOnly = true;
            this.docNoCountAlltextBox.Size = new System.Drawing.Size(50, 23);
            this.docNoCountAlltextBox.TabIndex = 68;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label10.Location = new System.Drawing.Point(53, 25);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(43, 16);
            this.label10.TabIndex = 66;
            this.label10.Text = "จำนวน";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label9.Location = new System.Drawing.Point(158, 25);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 16);
            this.label9.TabIndex = 67;
            this.label9.Text = "รายการ";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnSearch.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSearch.ForeColor = System.Drawing.Color.Black;
            this.btnSearch.Location = new System.Drawing.Point(32, 72);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(84, 32);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "ค้นหา";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnCancelSearch
            // 
            this.btnCancelSearch.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnCancelSearch.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnCancelSearch.ForeColor = System.Drawing.Color.Black;
            this.btnCancelSearch.Location = new System.Drawing.Point(122, 72);
            this.btnCancelSearch.Name = "btnCancelSearch";
            this.btnCancelSearch.Size = new System.Drawing.Size(84, 32);
            this.btnCancelSearch.TabIndex = 1;
            this.btnCancelSearch.Text = "หยุดค้นหา";
            this.btnCancelSearch.UseVisualStyleBackColor = false;
            this.btnCancelSearch.Click += new System.EventHandler(this.btnCancelSearch_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.invoiceCountTextBox);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.failSearchDocCounttextBox);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.successSearchDocTextBox);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.groupBox3.Location = new System.Drawing.Point(246, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(366, 114);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "ผลการค้นหา";
            // 
            // invoiceCountTextBox
            // 
            this.invoiceCountTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.invoiceCountTextBox.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.invoiceCountTextBox.ForeColor = System.Drawing.Color.MediumBlue;
            this.invoiceCountTextBox.Location = new System.Drawing.Point(159, 77);
            this.invoiceCountTextBox.Name = "invoiceCountTextBox";
            this.invoiceCountTextBox.ReadOnly = true;
            this.invoiceCountTextBox.Size = new System.Drawing.Size(50, 23);
            this.invoiceCountTextBox.TabIndex = 77;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label5.Location = new System.Drawing.Point(84, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 16);
            this.label5.TabIndex = 75;
            this.label5.Text = "รายการหนี้";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label6.Location = new System.Drawing.Point(215, 80);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 16);
            this.label6.TabIndex = 76;
            this.label6.Text = "รายการ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // failSearchDocCounttextBox
            // 
            this.failSearchDocCounttextBox.BackColor = System.Drawing.SystemColors.Window;
            this.failSearchDocCounttextBox.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.failSearchDocCounttextBox.ForeColor = System.Drawing.Color.Red;
            this.failSearchDocCounttextBox.Location = new System.Drawing.Point(159, 47);
            this.failSearchDocCounttextBox.Name = "failSearchDocCounttextBox";
            this.failSearchDocCounttextBox.ReadOnly = true;
            this.failSearchDocCounttextBox.Size = new System.Drawing.Size(50, 23);
            this.failSearchDocCounttextBox.TabIndex = 74;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(64, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 16);
            this.label3.TabIndex = 72;
            this.label3.Text = "ค้นหาไม่สำเร็จ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label4.Location = new System.Drawing.Point(215, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 16);
            this.label4.TabIndex = 73;
            this.label4.Text = "รายการ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // successSearchDocTextBox
            // 
            this.successSearchDocTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.successSearchDocTextBox.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.successSearchDocTextBox.ForeColor = System.Drawing.Color.MediumBlue;
            this.successSearchDocTextBox.Location = new System.Drawing.Point(159, 18);
            this.successSearchDocTextBox.Name = "successSearchDocTextBox";
            this.successSearchDocTextBox.ReadOnly = true;
            this.successSearchDocTextBox.Size = new System.Drawing.Size(50, 23);
            this.successSearchDocTextBox.TabIndex = 71;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(79, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 16);
            this.label1.TabIndex = 69;
            this.label1.Text = "ค้นหาสำเร็จ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(215, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 16);
            this.label2.TabIndex = 70;
            this.label2.Text = "รายการ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(808, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(108, 33);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "ยกเลิก";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnOK.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnOK.ForeColor = System.Drawing.Color.Black;
            this.btnOK.Location = new System.Drawing.Point(695, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(108, 33);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "ตกลง";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // SearchMultiDataView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(117)))), ((int)(((byte)(191)))));
            this.Controls.Add(this.splitContainer1);
            this.Name = "SearchMultiDataView";
            this.Size = new System.Drawing.Size(946, 655);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.inputDocGroupBox.ResumeLayout(false);
            this.inputDocGroupBox.PerformLayout();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.grSearch.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.documentListDataGridView)).EndInit();
            this.docNoCountGroupBox.ResumeLayout(false);
            this.docNoCountGroupBox.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox inputDocGroupBox;
        private System.Windows.Forms.TextBox caIdInputtextBox;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.GroupBox grSearch;
        private System.Windows.Forms.DataGridView documentListDataGridView;
        private System.Windows.Forms.Button btnConvertStringToList;
        private System.Windows.Forms.Button btnCancelSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox docNoCountAlltextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox docNoCountGroupBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox failSearchDocCounttextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox successSearchDocTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox invoiceCountTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDocumentNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInvoiceCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Result;
        private System.Windows.Forms.Button clearInputButton;
        private System.Windows.Forms.TextBox inputTextRowCountTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}
