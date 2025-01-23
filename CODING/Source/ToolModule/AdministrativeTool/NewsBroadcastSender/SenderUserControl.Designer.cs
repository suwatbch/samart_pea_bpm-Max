namespace AdministrativeTool.NewsBroadcastSender
{
    partial class SenderUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SenderUserControl));
            this.labelHeader = new System.Windows.Forms.Label();
            this.buttonNewMessage = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelSubHeader = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numericUpDownYears = new System.Windows.Forms.NumericUpDown();
            this.comboBoxMonth = new System.Windows.Forms.ComboBox();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.dateTimePickerDisplayDt = new System.Windows.Forms.DateTimePicker();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.dateTimePickerSentDt = new System.Windows.Forms.DateTimePicker();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.groupBoxConnect = new System.Windows.Forms.GroupBox();
            this.textBoxServiceConnect = new System.Windows.Forms.TextBox();
            this.buttonEditWebService = new System.Windows.Forms.Button();
            this.dataGridViewNewsList = new System.Windows.Forms.DataGridView();
            this.sentDtDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.expireDtDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.broadcastTopicDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.newsBroadcastSentInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownYears)).BeginInit();
            this.groupBoxConnect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNewsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.newsBroadcastSentInfoBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // labelHeader
            // 
            this.labelHeader.AutoSize = true;
            this.labelHeader.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.labelHeader.Location = new System.Drawing.Point(6, 10);
            this.labelHeader.Name = "labelHeader";
            this.labelHeader.Size = new System.Drawing.Size(180, 19);
            this.labelHeader.TabIndex = 0;
            this.labelHeader.Text = "BPM News Broadcast";
            // 
            // buttonNewMessage
            // 
            this.buttonNewMessage.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.buttonNewMessage.Image = ((System.Drawing.Image)(resources.GetObject("buttonNewMessage.Image")));
            this.buttonNewMessage.Location = new System.Drawing.Point(10, 6);
            this.buttonNewMessage.Name = "buttonNewMessage";
            this.buttonNewMessage.Size = new System.Drawing.Size(104, 97);
            this.buttonNewMessage.TabIndex = 1;
            this.buttonNewMessage.Text = "เขียนข่าวใหม่...";
            this.buttonNewMessage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonNewMessage.UseVisualStyleBackColor = true;
            this.buttonNewMessage.Click += new System.EventHandler(this.buttonNewMessage_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonNewMessage);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(123, 116);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.labelSubHeader);
            this.panel2.Controls.Add(this.labelHeader);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(10, 10);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(845, 61);
            this.panel2.TabIndex = 0;
            // 
            // labelSubHeader
            // 
            this.labelSubHeader.AutoSize = true;
            this.labelSubHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.labelSubHeader.Location = new System.Drawing.Point(7, 38);
            this.labelSubHeader.Name = "labelSubHeader";
            this.labelSubHeader.Size = new System.Drawing.Size(353, 13);
            this.labelSubHeader.TabIndex = 0;
            this.labelSubHeader.Text = "กระจายข้อมูลข่าวสารถึงผู้ใช้งานโปรแกรม Billing and Payment Management";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(20, 19);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(135, 17);
            this.radioButton1.TabIndex = 6;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "รายการข่าวตามวันที่ส่ง :";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(10, 71);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(845, 116);
            this.panel3.TabIndex = 8;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.groupBox2);
            this.panel4.Controls.Add(this.groupBoxConnect);
            this.panel4.Controls.Add(this.panel1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(845, 116);
            this.panel4.TabIndex = 4;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numericUpDownYears);
            this.groupBox2.Controls.Add(this.comboBoxMonth);
            this.groupBox2.Controls.Add(this.radioButton3);
            this.groupBox2.Controls.Add(this.dateTimePickerDisplayDt);
            this.groupBox2.Controls.Add(this.radioButton2);
            this.groupBox2.Controls.Add(this.dateTimePickerSentDt);
            this.groupBox2.Controls.Add(this.radioButton4);
            this.groupBox2.Controls.Add(this.radioButton1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(123, 47);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(722, 69);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "ประเภทรายการข่าว";
            // 
            // numericUpDownYears
            // 
            this.numericUpDownYears.Enabled = false;
            this.numericUpDownYears.Location = new System.Drawing.Point(665, 20);
            this.numericUpDownYears.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numericUpDownYears.Minimum = new decimal(new int[] {
            2550,
            0,
            0,
            0});
            this.numericUpDownYears.Name = "numericUpDownYears";
            this.numericUpDownYears.Size = new System.Drawing.Size(47, 20);
            this.numericUpDownYears.TabIndex = 11;
            this.numericUpDownYears.Value = new decimal(new int[] {
            2553,
            0,
            0,
            0});
            this.numericUpDownYears.ValueChanged += new System.EventHandler(this.numericUpDownYears_ValueChanged);
            // 
            // comboBoxMonth
            // 
            this.comboBoxMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMonth.Enabled = false;
            this.comboBoxMonth.FormattingEnabled = true;
            this.comboBoxMonth.Items.AddRange(new object[] {
            "มกราคม",
            "กุมภาพันธ์",
            "มีนาคม",
            "เมษายน",
            "พฤษภาคม",
            "มิถุนายน",
            "กรกฎาคม",
            "สิงหาคม",
            "กันยายน",
            "ตุลาคม",
            "พฤศจิกายน",
            "ธันวาคม"});
            this.comboBoxMonth.Location = new System.Drawing.Point(538, 19);
            this.comboBoxMonth.Name = "comboBoxMonth";
            this.comboBoxMonth.Size = new System.Drawing.Size(121, 21);
            this.comboBoxMonth.TabIndex = 10;
            this.comboBoxMonth.SelectedIndexChanged += new System.EventHandler(this.comboBoxMonth_SelectedIndexChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(394, 19);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(147, 17);
            this.radioButton3.TabIndex = 9;
            this.radioButton3.Text = "รายการข่าวตามเดือนที่ส่ง :";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // dateTimePickerDisplayDt
            // 
            this.dateTimePickerDisplayDt.Location = new System.Drawing.Point(213, 43);
            this.dateTimePickerDisplayDt.Name = "dateTimePickerDisplayDt";
            this.dateTimePickerDisplayDt.Size = new System.Drawing.Size(137, 20);
            this.dateTimePickerDisplayDt.TabIndex = 5;
            this.dateTimePickerDisplayDt.ValueChanged += new System.EventHandler(this.dateTimePickerDisplayDt_ValueChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(20, 43);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(187, 17);
            this.radioButton2.TabIndex = 4;
            this.radioButton2.Text = "รายการข่าวที่แสดงผลในวันที่นั้นๆ :";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // dateTimePickerSentDt
            // 
            this.dateTimePickerSentDt.Location = new System.Drawing.Point(161, 19);
            this.dateTimePickerSentDt.Name = "dateTimePickerSentDt";
            this.dateTimePickerSentDt.Size = new System.Drawing.Size(137, 20);
            this.dateTimePickerSentDt.TabIndex = 7;
            this.dateTimePickerSentDt.ValueChanged += new System.EventHandler(this.dateTimePickerSentDt_ValueChanged);
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(394, 43);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(225, 17);
            this.radioButton4.TabIndex = 8;
            this.radioButton4.Text = "รายการข่าวที่ตั้งเวลาส่งล่วงหน้า (ยังไม่ได้ส่ง)";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // groupBoxConnect
            // 
            this.groupBoxConnect.Controls.Add(this.textBoxServiceConnect);
            this.groupBoxConnect.Controls.Add(this.buttonEditWebService);
            this.groupBoxConnect.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxConnect.Location = new System.Drawing.Point(123, 0);
            this.groupBoxConnect.Name = "groupBoxConnect";
            this.groupBoxConnect.Padding = new System.Windows.Forms.Padding(10, 4, 10, 4);
            this.groupBoxConnect.Size = new System.Drawing.Size(722, 47);
            this.groupBoxConnect.TabIndex = 2;
            this.groupBoxConnect.TabStop = false;
            this.groupBoxConnect.Text = "เชื่อมต่อ Server (Path ของ Web Service ในระบบ BPM)";
            // 
            // textBoxServiceConnect
            // 
            this.textBoxServiceConnect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxServiceConnect.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.textBoxServiceConnect.Location = new System.Drawing.Point(10, 17);
            this.textBoxServiceConnect.Name = "textBoxServiceConnect";
            this.textBoxServiceConnect.ReadOnly = true;
            this.textBoxServiceConnect.Size = new System.Drawing.Size(627, 21);
            this.textBoxServiceConnect.TabIndex = 2;
            // 
            // buttonEditWebService
            // 
            this.buttonEditWebService.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonEditWebService.Location = new System.Drawing.Point(637, 17);
            this.buttonEditWebService.Name = "buttonEditWebService";
            this.buttonEditWebService.Size = new System.Drawing.Size(75, 26);
            this.buttonEditWebService.TabIndex = 3;
            this.buttonEditWebService.Text = "แก้ไข";
            this.buttonEditWebService.UseVisualStyleBackColor = true;
            this.buttonEditWebService.Click += new System.EventHandler(this.buttonEditWebService_Click);
            // 
            // dataGridViewNewsList
            // 
            this.dataGridViewNewsList.AutoGenerateColumns = false;
            this.dataGridViewNewsList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewNewsList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewNewsList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sentDtDataGridViewTextBoxColumn,
            this.expireDtDataGridViewTextBoxColumn,
            this.broadcastTopicDataGridViewTextBoxColumn});
            this.dataGridViewNewsList.DataSource = this.newsBroadcastSentInfoBindingSource;
            this.dataGridViewNewsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewNewsList.Location = new System.Drawing.Point(10, 187);
            this.dataGridViewNewsList.MultiSelect = false;
            this.dataGridViewNewsList.Name = "dataGridViewNewsList";
            this.dataGridViewNewsList.RowHeadersVisible = false;
            this.dataGridViewNewsList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewNewsList.Size = new System.Drawing.Size(845, 375);
            this.dataGridViewNewsList.TabIndex = 9;
            this.dataGridViewNewsList.DoubleClick += new System.EventHandler(this.dataGridViewNewsList_DoubleClick);
            this.dataGridViewNewsList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dataGridViewNewsList_MouseDoubleClick);
            // 
            // sentDtDataGridViewTextBoxColumn
            // 
            this.sentDtDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.sentDtDataGridViewTextBoxColumn.DataPropertyName = "SentDt";
            this.sentDtDataGridViewTextBoxColumn.FillWeight = 10.15228F;
            this.sentDtDataGridViewTextBoxColumn.Frozen = true;
            this.sentDtDataGridViewTextBoxColumn.HeaderText = "วัน-เวลาที่ส่ง";
            this.sentDtDataGridViewTextBoxColumn.Name = "sentDtDataGridViewTextBoxColumn";
            this.sentDtDataGridViewTextBoxColumn.Width = 160;
            // 
            // expireDtDataGridViewTextBoxColumn
            // 
            this.expireDtDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.expireDtDataGridViewTextBoxColumn.DataPropertyName = "ExpireDt";
            this.expireDtDataGridViewTextBoxColumn.FillWeight = 282.3079F;
            this.expireDtDataGridViewTextBoxColumn.HeaderText = "วัน-เวลาสิ้นสุดการแสดงผล";
            this.expireDtDataGridViewTextBoxColumn.Name = "expireDtDataGridViewTextBoxColumn";
            this.expireDtDataGridViewTextBoxColumn.Width = 160;
            // 
            // broadcastTopicDataGridViewTextBoxColumn
            // 
            this.broadcastTopicDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.broadcastTopicDataGridViewTextBoxColumn.DataPropertyName = "BroadcastTopic";
            this.broadcastTopicDataGridViewTextBoxColumn.FillWeight = 53.76993F;
            this.broadcastTopicDataGridViewTextBoxColumn.HeaderText = "หัวข้อข่าว";
            this.broadcastTopicDataGridViewTextBoxColumn.Name = "broadcastTopicDataGridViewTextBoxColumn";
            // 
            // newsBroadcastSentInfoBindingSource
            // 
            this.newsBroadcastSentInfoBindingSource.DataSource = typeof(PEA.BPM.NewsBroadcast.Interface.BusinessEntities.NewsBroadcastSentInfo);
            // 
            // SenderUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridViewNewsList);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.MinimumSize = new System.Drawing.Size(404, 228);
            this.Name = "SenderUserControl";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(865, 572);
            this.Load += new System.EventHandler(this.SenderUserControl_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownYears)).EndInit();
            this.groupBoxConnect.ResumeLayout(false);
            this.groupBoxConnect.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNewsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.newsBroadcastSentInfoBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelHeader;
        private System.Windows.Forms.Button buttonNewMessage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label labelSubHeader;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBoxConnect;
        private System.Windows.Forms.TextBox textBoxServiceConnect;
        private System.Windows.Forms.BindingSource newsBroadcastSentInfoBindingSource;
        private System.Windows.Forms.Button buttonEditWebService;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DateTimePicker dateTimePickerSentDt;
        private System.Windows.Forms.DateTimePicker dateTimePickerDisplayDt;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.DataGridView dataGridViewNewsList;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.DataGridViewTextBoxColumn sentDtDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn expireDtDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn broadcastTopicDataGridViewTextBoxColumn;
        private System.Windows.Forms.NumericUpDown numericUpDownYears;
        private System.Windows.Forms.ComboBox comboBoxMonth;
    }
}
