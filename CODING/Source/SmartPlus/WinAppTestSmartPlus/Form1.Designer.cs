namespace WinAppTestSmartPlus
{
    partial class Form1
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtCaId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSearchContractorService = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBoxARResult = new System.Windows.Forms.GroupBox();
            this.gvArResult = new System.Windows.Forms.DataGridView();
            this.groupBoxListOfInvoiceNo = new System.Windows.Forms.GroupBox();
            this.rchTxtListInvoiceNo = new System.Windows.Forms.RichTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnMarkFlag = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMarkFlagInvoiceNo = new System.Windows.Forms.TextBox();
            this.txtMarkFlagCaid = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.rchMarkFlagResult = new System.Windows.Forms.RichTextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCancelInvoiceNo = new System.Windows.Forms.TextBox();
            this.txtCancelCaid = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rchCancelResult = new System.Windows.Forms.RichTextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.cboBillFlag = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtInterestDay = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtInterestCaId = new System.Windows.Forms.TextBox();
            this.btnCallInterestByDate = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBoxARResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvArResult)).BeginInit();
            this.groupBoxListOfInvoiceNo.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Font = new System.Drawing.Font("BrowalliaUPC", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.tabControl1.Location = new System.Drawing.Point(2, 4);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1126, 576);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.Location = new System.Drawing.Point(4, 38);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabPage1.Size = new System.Drawing.Size(1118, 534);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "SearchConTractorService";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.cboBillFlag);
            this.groupBox3.Controls.Add(this.txtInterestDay);
            this.groupBox3.Controls.Add(this.txtInterestCaId);
            this.groupBox3.Controls.Add(this.txtCaId);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.btnCallInterestByDate);
            this.groupBox3.Controls.Add(this.btnSearchContractorService);
            this.groupBox3.Location = new System.Drawing.Point(11, 10);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBox3.Size = new System.Drawing.Size(1099, 145);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Input parameters";
            // 
            // txtCaId
            // 
            this.txtCaId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCaId.Font = new System.Drawing.Font("BrowalliaUPC", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtCaId.Location = new System.Drawing.Point(63, 29);
            this.txtCaId.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtCaId.MaximumSize = new System.Drawing.Size(191, 30);
            this.txtCaId.MinimumSize = new System.Drawing.Size(191, 30);
            this.txtCaId.Name = "txtCaId";
            this.txtCaId.Size = new System.Drawing.Size(191, 30);
            this.txtCaId.TabIndex = 0;
            this.txtCaId.Text = "020003474155";
            this.txtCaId.TextChanged += new System.EventHandler(this.txtCaId_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 27);
            this.label1.MaximumSize = new System.Drawing.Size(0, 54);
            this.label1.MinimumSize = new System.Drawing.Size(0, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 54);
            this.label1.TabIndex = 1;
            this.label1.Text = "CaId:";
            // 
            // btnSearchContractorService
            // 
            this.btnSearchContractorService.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchContractorService.Font = new System.Drawing.Font("BrowalliaUPC", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSearchContractorService.Location = new System.Drawing.Point(63, 104);
            this.btnSearchContractorService.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnSearchContractorService.MaximumSize = new System.Drawing.Size(101, 69);
            this.btnSearchContractorService.MinimumSize = new System.Drawing.Size(101, 20);
            this.btnSearchContractorService.Name = "btnSearchContractorService";
            this.btnSearchContractorService.Size = new System.Drawing.Size(101, 31);
            this.btnSearchContractorService.TabIndex = 2;
            this.btnSearchContractorService.Text = "Call WS";
            this.btnSearchContractorService.UseVisualStyleBackColor = true;
            this.btnSearchContractorService.Click += new System.EventHandler(this.btnSearchContractorService_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(8, 160);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBoxARResult);
            this.splitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBoxListOfInvoiceNo);
            this.splitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Size = new System.Drawing.Size(1102, 340);
            this.splitContainer1.SplitterDistance = 839;
            this.splitContainer1.TabIndex = 3;
            // 
            // groupBoxARResult
            // 
            this.groupBoxARResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxARResult.Controls.Add(this.gvArResult);
            this.groupBoxARResult.Location = new System.Drawing.Point(3, 5);
            this.groupBoxARResult.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBoxARResult.Name = "groupBoxARResult";
            this.groupBoxARResult.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBoxARResult.Size = new System.Drawing.Size(832, 330);
            this.groupBoxARResult.TabIndex = 0;
            this.groupBoxARResult.TabStop = false;
            this.groupBoxARResult.Text = "AR Information Result";
            // 
            // gvArResult
            // 
            this.gvArResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gvArResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvArResult.Location = new System.Drawing.Point(7, 32);
            this.gvArResult.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.gvArResult.Name = "gvArResult";
            this.gvArResult.ReadOnly = true;
            this.gvArResult.RowTemplate.Height = 24;
            this.gvArResult.Size = new System.Drawing.Size(819, 288);
            this.gvArResult.TabIndex = 0;
            // 
            // groupBoxListOfInvoiceNo
            // 
            this.groupBoxListOfInvoiceNo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxListOfInvoiceNo.Controls.Add(this.rchTxtListInvoiceNo);
            this.groupBoxListOfInvoiceNo.Location = new System.Drawing.Point(4, 7);
            this.groupBoxListOfInvoiceNo.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBoxListOfInvoiceNo.Name = "groupBoxListOfInvoiceNo";
            this.groupBoxListOfInvoiceNo.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBoxListOfInvoiceNo.Size = new System.Drawing.Size(251, 328);
            this.groupBoxListOfInvoiceNo.TabIndex = 0;
            this.groupBoxListOfInvoiceNo.TabStop = false;
            this.groupBoxListOfInvoiceNo.Text = "List of InvoiceNo";
            // 
            // rchTxtListInvoiceNo
            // 
            this.rchTxtListInvoiceNo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rchTxtListInvoiceNo.Location = new System.Drawing.Point(6, 30);
            this.rchTxtListInvoiceNo.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.rchTxtListInvoiceNo.Name = "rchTxtListInvoiceNo";
            this.rchTxtListInvoiceNo.Size = new System.Drawing.Size(236, 288);
            this.rchTxtListInvoiceNo.TabIndex = 0;
            this.rchTxtListInvoiceNo.Text = "";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer2);
            this.tabPage2.Location = new System.Drawing.Point(4, 38);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabPage2.Size = new System.Drawing.Size(1118, 828);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Update Mark Flag";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.Location = new System.Drawing.Point(8, 13);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox4);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox5);
            this.splitContainer2.Size = new System.Drawing.Size(1102, 861);
            this.splitContainer2.SplitterDistance = 914;
            this.splitContainer2.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.btnMarkFlag);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.txtMarkFlagInvoiceNo);
            this.groupBox4.Controls.Add(this.txtMarkFlagCaid);
            this.groupBox4.Location = new System.Drawing.Point(4, 7);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBox4.Size = new System.Drawing.Size(906, 848);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Markflag Parameters";
            // 
            // btnMarkFlag
            // 
            this.btnMarkFlag.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMarkFlag.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnMarkFlag.Location = new System.Drawing.Point(370, 368);
            this.btnMarkFlag.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnMarkFlag.MaximumSize = new System.Drawing.Size(180, 72);
            this.btnMarkFlag.MinimumSize = new System.Drawing.Size(180, 72);
            this.btnMarkFlag.Name = "btnMarkFlag";
            this.btnMarkFlag.Size = new System.Drawing.Size(180, 72);
            this.btnMarkFlag.TabIndex = 3;
            this.btnMarkFlag.Text = "Call Mark Flag WS";
            this.btnMarkFlag.UseVisualStyleBackColor = true;
            this.btnMarkFlag.Click += new System.EventHandler(this.btnMarkFlag_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 194);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 30);
            this.label3.TabIndex = 2;
            this.label3.Text = "InvoiceNo:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 30);
            this.label2.TabIndex = 1;
            this.label2.Text = "Caid:";
            // 
            // txtMarkFlagInvoiceNo
            // 
            this.txtMarkFlagInvoiceNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMarkFlagInvoiceNo.Location = new System.Drawing.Point(7, 254);
            this.txtMarkFlagInvoiceNo.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtMarkFlagInvoiceNo.Multiline = true;
            this.txtMarkFlagInvoiceNo.Name = "txtMarkFlagInvoiceNo";
            this.txtMarkFlagInvoiceNo.Size = new System.Drawing.Size(892, 51);
            this.txtMarkFlagInvoiceNo.TabIndex = 0;
            // 
            // txtMarkFlagCaid
            // 
            this.txtMarkFlagCaid.Location = new System.Drawing.Point(8, 120);
            this.txtMarkFlagCaid.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtMarkFlagCaid.Name = "txtMarkFlagCaid";
            this.txtMarkFlagCaid.Size = new System.Drawing.Size(242, 36);
            this.txtMarkFlagCaid.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.rchMarkFlagResult);
            this.groupBox5.Location = new System.Drawing.Point(3, 7);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBox5.Size = new System.Drawing.Size(178, 848);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Mark Result";
            // 
            // rchMarkFlagResult
            // 
            this.rchMarkFlagResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rchMarkFlagResult.Location = new System.Drawing.Point(3, 34);
            this.rchMarkFlagResult.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.rchMarkFlagResult.Name = "rchMarkFlagResult";
            this.rchMarkFlagResult.Size = new System.Drawing.Size(172, 809);
            this.rchMarkFlagResult.TabIndex = 0;
            this.rchMarkFlagResult.Text = "";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.splitContainer3);
            this.tabPage3.Location = new System.Drawing.Point(4, 38);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1118, 828);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Cancel Payment";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer3.Location = new System.Drawing.Point(7, 11);
            this.splitContainer3.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer3.Size = new System.Drawing.Size(1102, 861);
            this.splitContainer3.SplitterDistance = 914;
            this.splitContainer3.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtCancelInvoiceNo);
            this.groupBox1.Controls.Add(this.txtCancelCaid);
            this.groupBox1.Location = new System.Drawing.Point(4, 7);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBox1.Size = new System.Drawing.Size(906, 848);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cancel Parameters";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(370, 368);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnCancel.MaximumSize = new System.Drawing.Size(158, 72);
            this.btnCancel.MinimumSize = new System.Drawing.Size(158, 72);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(158, 72);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Call Cancel WS";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 194);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 30);
            this.label4.TabIndex = 2;
            this.label4.Text = "InvoiceNo:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 30);
            this.label5.TabIndex = 1;
            this.label5.Text = "Caid:";
            // 
            // txtCancelInvoiceNo
            // 
            this.txtCancelInvoiceNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCancelInvoiceNo.Location = new System.Drawing.Point(7, 254);
            this.txtCancelInvoiceNo.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtCancelInvoiceNo.Multiline = true;
            this.txtCancelInvoiceNo.Name = "txtCancelInvoiceNo";
            this.txtCancelInvoiceNo.Size = new System.Drawing.Size(892, 51);
            this.txtCancelInvoiceNo.TabIndex = 0;
            // 
            // txtCancelCaid
            // 
            this.txtCancelCaid.Location = new System.Drawing.Point(8, 120);
            this.txtCancelCaid.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtCancelCaid.Name = "txtCancelCaid";
            this.txtCancelCaid.Size = new System.Drawing.Size(242, 36);
            this.txtCancelCaid.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.rchCancelResult);
            this.groupBox2.Location = new System.Drawing.Point(3, 7);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBox2.Size = new System.Drawing.Size(178, 848);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Cancel Result";
            // 
            // rchCancelResult
            // 
            this.rchCancelResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rchCancelResult.Location = new System.Drawing.Point(3, 34);
            this.rchCancelResult.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.rchCancelResult.Name = "rchCancelResult";
            this.rchCancelResult.Size = new System.Drawing.Size(172, 809);
            this.rchCancelResult.TabIndex = 0;
            this.rchCancelResult.Text = "";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.groupBox7);
            this.tabPage4.Controls.Add(this.groupBox6);
            this.tabPage4.Location = new System.Drawing.Point(4, 38);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(1118, 828);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Tools";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox7.Location = new System.Drawing.Point(580, 7);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBox7.Size = new System.Drawing.Size(533, 865);
            this.groupBox7.TabIndex = 1;
            this.groupBox7.TabStop = false;
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox6.Location = new System.Drawing.Point(8, 7);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBox6.Size = new System.Drawing.Size(385, 865);
            this.groupBox6.TabIndex = 0;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Paste InvoiceNo Here";
            // 
            // cboBillFlag
            // 
            this.cboBillFlag.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboBillFlag.Font = new System.Drawing.Font("BrowalliaUPC", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cboBillFlag.FormattingEnabled = true;
            this.cboBillFlag.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.cboBillFlag.Location = new System.Drawing.Point(63, 67);
            this.cboBillFlag.Name = "cboBillFlag";
            this.cboBillFlag.Size = new System.Drawing.Size(56, 33);
            this.cboBillFlag.TabIndex = 3;
            this.cboBillFlag.Text = "1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 67);
            this.label6.MaximumSize = new System.Drawing.Size(0, 54);
            this.label6.MinimumSize = new System.Drawing.Size(0, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 54);
            this.label6.TabIndex = 1;
            this.label6.Text = "Flag:";
            // 
            // txtInterestDay
            // 
            this.txtInterestDay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInterestDay.Font = new System.Drawing.Font("BrowalliaUPC", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtInterestDay.Location = new System.Drawing.Point(902, 70);
            this.txtInterestDay.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtInterestDay.MaximumSize = new System.Drawing.Size(191, 30);
            this.txtInterestDay.MinimumSize = new System.Drawing.Size(191, 30);
            this.txtInterestDay.Name = "txtInterestDay";
            this.txtInterestDay.Size = new System.Drawing.Size(191, 30);
            this.txtInterestDay.TabIndex = 0;
            this.txtInterestDay.Text = "2021-06-10";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(680, 70);
            this.label7.MaximumSize = new System.Drawing.Size(0, 54);
            this.label7.MinimumSize = new System.Drawing.Size(0, 54);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(216, 54);
            this.label7.TabIndex = 1;
            this.label7.Text = "วันที่ คำนวณดอกเบี้ยล่วงหน้า:";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(846, 21);
            this.label8.MaximumSize = new System.Drawing.Size(0, 54);
            this.label8.MinimumSize = new System.Drawing.Size(0, 54);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 54);
            this.label8.TabIndex = 1;
            this.label8.Text = "CaId:";
            // 
            // txtInterestCaId
            // 
            this.txtInterestCaId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInterestCaId.Font = new System.Drawing.Font("BrowalliaUPC", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtInterestCaId.Location = new System.Drawing.Point(902, 21);
            this.txtInterestCaId.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtInterestCaId.MaximumSize = new System.Drawing.Size(191, 30);
            this.txtInterestCaId.MinimumSize = new System.Drawing.Size(191, 30);
            this.txtInterestCaId.Name = "txtInterestCaId";
            this.txtInterestCaId.Size = new System.Drawing.Size(191, 30);
            this.txtInterestCaId.TabIndex = 0;
            this.txtInterestCaId.Text = "020003474155";
            // 
            // btnCallInterestByDate
            // 
            this.btnCallInterestByDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCallInterestByDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCallInterestByDate.Font = new System.Drawing.Font("BrowalliaUPC", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnCallInterestByDate.Location = new System.Drawing.Point(939, 107);
            this.btnCallInterestByDate.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnCallInterestByDate.MaximumSize = new System.Drawing.Size(101, 69);
            this.btnCallInterestByDate.MinimumSize = new System.Drawing.Size(101, 20);
            this.btnCallInterestByDate.Name = "btnCallInterestByDate";
            this.btnCallInterestByDate.Size = new System.Drawing.Size(101, 33);
            this.btnCallInterestByDate.TabIndex = 2;
            this.btnCallInterestByDate.Text = "Call WS";
            this.btnCallInterestByDate.UseVisualStyleBackColor = true;
            this.btnCallInterestByDate.Click += new System.EventHandler(this.btnCallInterestByDate_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1142, 581);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("BrowalliaUPC", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "Form1";
            this.Text = "Smartplus WS Tester V.5 Interest Date";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBoxARResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvArResult)).EndInit();
            this.groupBoxListOfInvoiceNo.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnSearchContractorService;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCaId;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBoxARResult;
        private System.Windows.Forms.DataGridView gvArResult;
        private System.Windows.Forms.GroupBox groupBoxListOfInvoiceNo;
        private System.Windows.Forms.RichTextBox rchTxtListInvoiceNo;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnMarkFlag;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMarkFlagInvoiceNo;
        private System.Windows.Forms.TextBox txtMarkFlagCaid;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RichTextBox rchMarkFlagResult;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCancelInvoiceNo;
        private System.Windows.Forms.TextBox txtCancelCaid;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox rchCancelResult;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.ComboBox cboBillFlag;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtInterestDay;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtInterestCaId;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnCallInterestByDate;
    }
}

