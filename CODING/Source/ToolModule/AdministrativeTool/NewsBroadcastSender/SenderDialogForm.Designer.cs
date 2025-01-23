namespace AdministrativeTool.NewsBroadcastSender
{
    partial class SenderDialogForm
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
            this.components = new System.ComponentModel.Container();
            this.GroupBoxMessage = new System.Windows.Forms.GroupBox();
            this.richTextBoxDetail = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonViewSentUserList = new System.Windows.Forms.Button();
            this.buttonPasteMessage = new System.Windows.Forms.Button();
            this.buttonCopyMessage = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSend = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.LabelHeader = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.textBoxTopic = new System.Windows.Forms.TextBox();
            this.labelTopic = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton2FixedRegis = new System.Windows.Forms.RadioButton();
            this.radioButton3NonFixedRegis = new System.Windows.Forms.RadioButton();
            this.radioButton4Area = new System.Windows.Forms.RadioButton();
            this.comboBoxArea = new System.Windows.Forms.ComboBox();
            this.areaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.comboBoxBranch = new System.Windows.Forms.ComboBox();
            this.branchInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.radioButton5Branch = new System.Windows.Forms.RadioButton();
            this.radioButton1All = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.buttonRoleClear = new System.Windows.Forms.Button();
            this.buttonRoleSelectAll = new System.Windows.Forms.Button();
            this.dataGridViewRole = new System.Windows.Forms.DataGridView();
            this.CheckBoxRole = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.roleIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.roleNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.roleInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButton7SentDateFixed = new System.Windows.Forms.RadioButton();
            this.radioButton6SentDateNow = new System.Windows.Forms.RadioButton();
            this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBox2 = new System.Windows.Forms.MaskedTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButton9EndDateFixed = new System.Windows.Forms.RadioButton();
            this.radioButton8EndDateNow = new System.Windows.Forms.RadioButton();
            this.panel6 = new System.Windows.Forms.Panel();
            this.GroupBoxMessage.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.areaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.branchInfoBindingSource)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRole)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.roleInfoBindingSource)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupBoxMessage
            // 
            this.GroupBoxMessage.Controls.Add(this.richTextBoxDetail);
            this.GroupBoxMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GroupBoxMessage.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.GroupBoxMessage.Location = new System.Drawing.Point(0, 220);
            this.GroupBoxMessage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GroupBoxMessage.Name = "GroupBoxMessage";
            this.GroupBoxMessage.Padding = new System.Windows.Forms.Padding(35, 6, 35, 12);
            this.GroupBoxMessage.Size = new System.Drawing.Size(491, 379);
            this.GroupBoxMessage.TabIndex = 5;
            this.GroupBoxMessage.TabStop = false;
            this.GroupBoxMessage.Text = "ข้อความ:";
            // 
            // richTextBoxDetail
            // 
            this.richTextBoxDetail.BackColor = System.Drawing.SystemColors.Info;
            this.richTextBoxDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxDetail.Location = new System.Drawing.Point(35, 22);
            this.richTextBoxDetail.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.richTextBoxDetail.Name = "richTextBoxDetail";
            this.richTextBoxDetail.Size = new System.Drawing.Size(421, 345);
            this.richTextBoxDetail.TabIndex = 20;
            this.richTextBoxDetail.Text = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonViewSentUserList);
            this.panel1.Controls.Add(this.buttonPasteMessage);
            this.panel1.Controls.Add(this.buttonCopyMessage);
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonSend);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(7, 639);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(949, 46);
            this.panel1.TabIndex = 6;
            // 
            // buttonViewSentUserList
            // 
            this.buttonViewSentUserList.Location = new System.Drawing.Point(12, 11);
            this.buttonViewSentUserList.Name = "buttonViewSentUserList";
            this.buttonViewSentUserList.Size = new System.Drawing.Size(145, 25);
            this.buttonViewSentUserList.TabIndex = 21;
            this.buttonViewSentUserList.Text = "ดูรายการผู้ใช้ที่ส่งแล้ว";
            this.buttonViewSentUserList.UseVisualStyleBackColor = true;
            this.buttonViewSentUserList.Click += new System.EventHandler(this.buttonViewSentUserList_Click);
            // 
            // buttonPasteMessage
            // 
            this.buttonPasteMessage.Location = new System.Drawing.Point(601, 9);
            this.buttonPasteMessage.Name = "buttonPasteMessage";
            this.buttonPasteMessage.Size = new System.Drawing.Size(96, 23);
            this.buttonPasteMessage.TabIndex = 23;
            this.buttonPasteMessage.Text = "Paste ข้อความ";
            this.buttonPasteMessage.UseVisualStyleBackColor = true;
            this.buttonPasteMessage.Click += new System.EventHandler(this.buttonPasteMessage_Click);
            // 
            // buttonCopyMessage
            // 
            this.buttonCopyMessage.Location = new System.Drawing.Point(493, 9);
            this.buttonCopyMessage.Name = "buttonCopyMessage";
            this.buttonCopyMessage.Size = new System.Drawing.Size(102, 23);
            this.buttonCopyMessage.TabIndex = 22;
            this.buttonCopyMessage.Text = "Copy ข้อความ";
            this.buttonCopyMessage.UseVisualStyleBackColor = true;
            this.buttonCopyMessage.Click += new System.EventHandler(this.buttonCopyMessage_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.buttonCancel.Location = new System.Drawing.Point(854, 9);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(87, 28);
            this.buttonCancel.TabIndex = 25;
            this.buttonCancel.Text = "ยกเลิก";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // buttonSend
            // 
            this.buttonSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSend.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.buttonSend.Location = new System.Drawing.Point(750, 9);
            this.buttonSend.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(87, 28);
            this.buttonSend.TabIndex = 24;
            this.buttonSend.Text = "ส่ง";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.ButtonSend_Click);
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.LabelHeader);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(7, 7);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(949, 33);
            this.panel2.TabIndex = 7;
            // 
            // LabelHeader
            // 
            this.LabelHeader.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LabelHeader.AutoSize = true;
            this.LabelHeader.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.LabelHeader.Location = new System.Drawing.Point(412, 4);
            this.LabelHeader.Name = "LabelHeader";
            this.LabelHeader.Size = new System.Drawing.Size(118, 19);
            this.LabelHeader.TabIndex = 0;
            this.LabelHeader.Text = "ส่งข้อความข่าว";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.textBoxTopic);
            this.panel3.Controls.Add(this.labelTopic);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 172);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(10, 12, 10, 12);
            this.panel3.Size = new System.Drawing.Size(491, 48);
            this.panel3.TabIndex = 8;
            // 
            // textBoxTopic
            // 
            this.textBoxTopic.BackColor = System.Drawing.SystemColors.Info;
            this.textBoxTopic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxTopic.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.textBoxTopic.Location = new System.Drawing.Point(88, 12);
            this.textBoxTopic.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxTopic.MaxLength = 255;
            this.textBoxTopic.Name = "textBoxTopic";
            this.textBoxTopic.Size = new System.Drawing.Size(393, 23);
            this.textBoxTopic.TabIndex = 19;
            // 
            // labelTopic
            // 
            this.labelTopic.AutoSize = true;
            this.labelTopic.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelTopic.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.labelTopic.Location = new System.Drawing.Point(10, 12);
            this.labelTopic.Name = "labelTopic";
            this.labelTopic.Size = new System.Drawing.Size(78, 18);
            this.labelTopic.TabIndex = 0;
            this.labelTopic.Text = "หัวข้อข่าว:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton2FixedRegis);
            this.groupBox1.Controls.Add(this.radioButton3NonFixedRegis);
            this.groupBox1.Controls.Add(this.radioButton4Area);
            this.groupBox1.Controls.Add(this.comboBoxArea);
            this.groupBox1.Controls.Add(this.comboBoxBranch);
            this.groupBox1.Controls.Add(this.radioButton5Branch);
            this.groupBox1.Controls.Add(this.radioButton1All);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.groupBox1.Location = new System.Drawing.Point(7, 40);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.MinimumSize = new System.Drawing.Size(440, 140);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(458, 164);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ถึงผู้ใช้ :";
            // 
            // radioButton2FixedRegis
            // 
            this.radioButton2FixedRegis.AutoSize = true;
            this.radioButton2FixedRegis.Location = new System.Drawing.Point(12, 53);
            this.radioButton2FixedRegis.Name = "radioButton2FixedRegis";
            this.radioButton2FixedRegis.Size = new System.Drawing.Size(373, 20);
            this.radioButton2FixedRegis.TabIndex = 2;
            this.radioButton2FixedRegis.TabStop = true;
            this.radioButton2FixedRegis.Text = "ผู้ใช้ทุกคน-ที่ถูกจำกัดสิทธิ์ให้ลงทะเบียนได้เฉพาะการไฟฟ้าที่สังกัด";
            this.radioButton2FixedRegis.UseVisualStyleBackColor = true;
            this.radioButton2FixedRegis.CheckedChanged += new System.EventHandler(this.radioButton2FixedRegis_CheckedChanged);
            // 
            // radioButton3NonFixedRegis
            // 
            this.radioButton3NonFixedRegis.AutoSize = true;
            this.radioButton3NonFixedRegis.Location = new System.Drawing.Point(12, 79);
            this.radioButton3NonFixedRegis.Name = "radioButton3NonFixedRegis";
            this.radioButton3NonFixedRegis.Size = new System.Drawing.Size(368, 20);
            this.radioButton3NonFixedRegis.TabIndex = 3;
            this.radioButton3NonFixedRegis.TabStop = true;
            this.radioButton3NonFixedRegis.Text = "ผู้ใช้ทุกคน-ที่ไม่ถูกจำกัดสิทธิ์การลงทะเบียนตามการไฟฟ้าที่สังกัด";
            this.radioButton3NonFixedRegis.UseVisualStyleBackColor = true;
            this.radioButton3NonFixedRegis.CheckedChanged += new System.EventHandler(this.radioButton3NonFixedRegis_CheckedChanged);
            // 
            // radioButton4Area
            // 
            this.radioButton4Area.AutoSize = true;
            this.radioButton4Area.Location = new System.Drawing.Point(12, 106);
            this.radioButton4Area.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioButton4Area.Name = "radioButton4Area";
            this.radioButton4Area.Size = new System.Drawing.Size(79, 20);
            this.radioButton4Area.TabIndex = 4;
            this.radioButton4Area.TabStop = true;
            this.radioButton4Area.Text = "เฉพาะเขต";
            this.radioButton4Area.UseVisualStyleBackColor = true;
            this.radioButton4Area.CheckedChanged += new System.EventHandler(this.radioButton4Area_CheckedChanged);
            // 
            // comboBoxArea
            // 
            this.comboBoxArea.DataSource = this.areaBindingSource;
            this.comboBoxArea.DisplayMember = "AreaName";
            this.comboBoxArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxArea.Enabled = false;
            this.comboBoxArea.FormattingEnabled = true;
            this.comboBoxArea.Location = new System.Drawing.Point(128, 105);
            this.comboBoxArea.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBoxArea.Name = "comboBoxArea";
            this.comboBoxArea.Size = new System.Drawing.Size(278, 24);
            this.comboBoxArea.TabIndex = 5;
            this.comboBoxArea.ValueMember = "AreaId";
            // 
            // areaBindingSource
            // 
            this.areaBindingSource.DataSource = typeof(PEA.BPM.NewsBroadcast.Interface.BusinessEntities.AreaInfo);
            // 
            // comboBoxBranch
            // 
            this.comboBoxBranch.DataSource = this.branchInfoBindingSource;
            this.comboBoxBranch.DisplayMember = "BranchName";
            this.comboBoxBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBranch.Enabled = false;
            this.comboBoxBranch.FormattingEnabled = true;
            this.comboBoxBranch.Location = new System.Drawing.Point(128, 133);
            this.comboBoxBranch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBoxBranch.Name = "comboBoxBranch";
            this.comboBoxBranch.Size = new System.Drawing.Size(278, 24);
            this.comboBoxBranch.TabIndex = 7;
            this.comboBoxBranch.ValueMember = "BranchId";
            this.comboBoxBranch.SelectedIndexChanged += new System.EventHandler(this.comboBoxBranch_SelectedIndexChanged);
            // 
            // branchInfoBindingSource
            // 
            this.branchInfoBindingSource.DataSource = typeof(PEA.BPM.NewsBroadcast.Interface.BusinessEntities.BranchInfo);
            // 
            // radioButton5Branch
            // 
            this.radioButton5Branch.AutoSize = true;
            this.radioButton5Branch.ForeColor = System.Drawing.SystemColors.ControlText;
            this.radioButton5Branch.Location = new System.Drawing.Point(12, 134);
            this.radioButton5Branch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioButton5Branch.Name = "radioButton5Branch";
            this.radioButton5Branch.Size = new System.Drawing.Size(110, 20);
            this.radioButton5Branch.TabIndex = 6;
            this.radioButton5Branch.Text = "เฉพาะการไฟฟ้า";
            this.radioButton5Branch.UseVisualStyleBackColor = true;
            this.radioButton5Branch.CheckedChanged += new System.EventHandler(this.radioButton5Branch_CheckedChanged);
            // 
            // radioButton1All
            // 
            this.radioButton1All.AutoSize = true;
            this.radioButton1All.Checked = true;
            this.radioButton1All.Location = new System.Drawing.Point(12, 28);
            this.radioButton1All.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioButton1All.Name = "radioButton1All";
            this.radioButton1All.Size = new System.Drawing.Size(81, 20);
            this.radioButton1All.TabIndex = 1;
            this.radioButton1All.TabStop = true;
            this.radioButton1All.Text = "ผู้ใช้ทุกคน";
            this.radioButton1All.UseVisualStyleBackColor = true;
            this.radioButton1All.CheckedChanged += new System.EventHandler(this.radioButton1All_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.buttonRoleClear);
            this.groupBox4.Controls.Add(this.buttonRoleSelectAll);
            this.groupBox4.Controls.Add(this.dataGridViewRole);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(7, 204);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox4.Size = new System.Drawing.Size(458, 435);
            this.groupBox4.TabIndex = 47;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "กลุ่มผู้ใช้";
            // 
            // buttonRoleClear
            // 
            this.buttonRoleClear.Enabled = false;
            this.buttonRoleClear.Location = new System.Drawing.Point(105, 395);
            this.buttonRoleClear.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonRoleClear.Name = "buttonRoleClear";
            this.buttonRoleClear.Size = new System.Drawing.Size(93, 28);
            this.buttonRoleClear.TabIndex = 10;
            this.buttonRoleClear.Text = "เคลียร์";
            this.buttonRoleClear.UseVisualStyleBackColor = true;
            this.buttonRoleClear.Click += new System.EventHandler(this.buttonRoleClear_Click);
            // 
            // buttonRoleSelectAll
            // 
            this.buttonRoleSelectAll.Enabled = false;
            this.buttonRoleSelectAll.Location = new System.Drawing.Point(6, 395);
            this.buttonRoleSelectAll.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonRoleSelectAll.Name = "buttonRoleSelectAll";
            this.buttonRoleSelectAll.Size = new System.Drawing.Size(93, 28);
            this.buttonRoleSelectAll.TabIndex = 9;
            this.buttonRoleSelectAll.Text = "เลือกทั้งหมด";
            this.buttonRoleSelectAll.UseVisualStyleBackColor = true;
            this.buttonRoleSelectAll.Click += new System.EventHandler(this.buttonRoleSelectAll_Click);
            // 
            // dataGridViewRole
            // 
            this.dataGridViewRole.AllowUserToAddRows = false;
            this.dataGridViewRole.AllowUserToDeleteRows = false;
            this.dataGridViewRole.AllowUserToResizeRows = false;
            this.dataGridViewRole.AutoGenerateColumns = false;
            this.dataGridViewRole.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRole.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CheckBoxRole,
            this.roleIdDataGridViewTextBoxColumn,
            this.roleNameDataGridViewTextBoxColumn,
            this.descriptionDataGridViewTextBoxColumn});
            this.dataGridViewRole.DataSource = this.roleInfoBindingSource;
            this.dataGridViewRole.Location = new System.Drawing.Point(6, 20);
            this.dataGridViewRole.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridViewRole.Name = "dataGridViewRole";
            this.dataGridViewRole.RowHeadersWidth = 5;
            this.dataGridViewRole.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewRole.Size = new System.Drawing.Size(442, 367);
            this.dataGridViewRole.TabIndex = 8;
            // 
            // CheckBoxRole
            // 
            this.CheckBoxRole.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.CheckBoxRole.HeaderText = "";
            this.CheckBoxRole.Name = "CheckBoxRole";
            this.CheckBoxRole.Width = 23;
            // 
            // roleIdDataGridViewTextBoxColumn
            // 
            this.roleIdDataGridViewTextBoxColumn.DataPropertyName = "RoleId";
            this.roleIdDataGridViewTextBoxColumn.HeaderText = "RoleId";
            this.roleIdDataGridViewTextBoxColumn.Name = "roleIdDataGridViewTextBoxColumn";
            this.roleIdDataGridViewTextBoxColumn.Visible = false;
            // 
            // roleNameDataGridViewTextBoxColumn
            // 
            this.roleNameDataGridViewTextBoxColumn.DataPropertyName = "RoleName";
            this.roleNameDataGridViewTextBoxColumn.HeaderText = "ประเภทสิทธื์";
            this.roleNameDataGridViewTextBoxColumn.Name = "roleNameDataGridViewTextBoxColumn";
            this.roleNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            this.descriptionDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            this.descriptionDataGridViewTextBoxColumn.HeaderText = "รายละเอียด";
            this.descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            this.descriptionDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // roleInfoBindingSource
            // 
            this.roleInfoBindingSource.DataSource = typeof(PEA.BPM.NewsBroadcast.Interface.BusinessEntities.RoleInfo);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.dateTimePicker1.Location = new System.Drawing.Point(148, 53);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(166, 23);
            this.dateTimePicker1.TabIndex = 13;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.dateTimePicker2.Location = new System.Drawing.Point(148, 51);
            this.dateTimePicker2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(166, 23);
            this.dateTimePicker2.TabIndex = 17;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.radioButton7SentDateFixed);
            this.groupBox2.Controls.Add(this.radioButton6SentDateNow);
            this.groupBox2.Controls.Add(this.maskedTextBox1);
            this.groupBox2.Controls.Add(this.dateTimePicker1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(491, 89);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "วันที่ส่ง";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(111, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 16);
            this.label3.TabIndex = 24;
            this.label3.Text = "วันที่";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(366, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 16);
            this.label1.TabIndex = 23;
            this.label1.Text = "น.";
            // 
            // radioButton7SentDateFixed
            // 
            this.radioButton7SentDateFixed.AutoSize = true;
            this.radioButton7SentDateFixed.Location = new System.Drawing.Point(23, 56);
            this.radioButton7SentDateFixed.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioButton7SentDateFixed.Name = "radioButton7SentDateFixed";
            this.radioButton7SentDateFixed.Size = new System.Drawing.Size(82, 20);
            this.radioButton7SentDateFixed.TabIndex = 12;
            this.radioButton7SentDateFixed.Text = "กำหนดเอง";
            this.radioButton7SentDateFixed.UseVisualStyleBackColor = true;
            // 
            // radioButton6SentDateNow
            // 
            this.radioButton6SentDateNow.AutoSize = true;
            this.radioButton6SentDateNow.Checked = true;
            this.radioButton6SentDateNow.Location = new System.Drawing.Point(23, 24);
            this.radioButton6SentDateNow.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioButton6SentDateNow.Name = "radioButton6SentDateNow";
            this.radioButton6SentDateNow.Size = new System.Drawing.Size(114, 20);
            this.radioButton6SentDateNow.TabIndex = 11;
            this.radioButton6SentDateNow.TabStop = true;
            this.radioButton6SentDateNow.Text = "เวลาขณะที่กดส่ง";
            this.radioButton6SentDateNow.UseVisualStyleBackColor = true;
            // 
            // maskedTextBox1
            // 
            this.maskedTextBox1.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.maskedTextBox1.Location = new System.Drawing.Point(320, 55);
            this.maskedTextBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.maskedTextBox1.Mask = "00:00";
            this.maskedTextBox1.Name = "maskedTextBox1";
            this.maskedTextBox1.Size = new System.Drawing.Size(40, 23);
            this.maskedTextBox1.TabIndex = 14;
            this.maskedTextBox1.Text = "0000";
            this.maskedTextBox1.ValidatingType = typeof(System.DateTime);
            this.maskedTextBox1.TypeValidationCompleted += new System.Windows.Forms.TypeValidationEventHandler(this.maskedTextBox1_TypeValidationCompleted);
            // 
            // maskedTextBox2
            // 
            this.maskedTextBox2.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.maskedTextBox2.Location = new System.Drawing.Point(320, 53);
            this.maskedTextBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.maskedTextBox2.Mask = "00:00";
            this.maskedTextBox2.Name = "maskedTextBox2";
            this.maskedTextBox2.Size = new System.Drawing.Size(40, 23);
            this.maskedTextBox2.TabIndex = 18;
            this.maskedTextBox2.Text = "2359";
            this.maskedTextBox2.ValidatingType = typeof(System.DateTime);
            this.maskedTextBox2.TypeValidationCompleted += new System.Windows.Forms.TypeValidationEventHandler(this.maskedTextBox2_TypeValidationCompleted);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.dateTimePicker2);
            this.groupBox3.Controls.Add(this.radioButton9EndDateFixed);
            this.groupBox3.Controls.Add(this.radioButton8EndDateNow);
            this.groupBox3.Controls.Add(this.maskedTextBox2);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Font = new System.Drawing.Font("Tahoma", 8.75F);
            this.groupBox3.Location = new System.Drawing.Point(0, 89);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Size = new System.Drawing.Size(491, 83);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "วันที่สิ้นสุดการแสดงผลข่าว";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(111, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 14);
            this.label4.TabIndex = 25;
            this.label4.Text = "วันที่";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(366, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 14);
            this.label2.TabIndex = 24;
            this.label2.Text = "น.";
            // 
            // radioButton9EndDateFixed
            // 
            this.radioButton9EndDateFixed.AutoSize = true;
            this.radioButton9EndDateFixed.Font = new System.Drawing.Font("Tahoma", 8.75F);
            this.radioButton9EndDateFixed.Location = new System.Drawing.Point(23, 55);
            this.radioButton9EndDateFixed.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioButton9EndDateFixed.Name = "radioButton9EndDateFixed";
            this.radioButton9EndDateFixed.Size = new System.Drawing.Size(77, 18);
            this.radioButton9EndDateFixed.TabIndex = 16;
            this.radioButton9EndDateFixed.Text = "กำหนดเอง";
            this.radioButton9EndDateFixed.UseVisualStyleBackColor = true;
            // 
            // radioButton8EndDateNow
            // 
            this.radioButton8EndDateNow.AutoSize = true;
            this.radioButton8EndDateNow.Checked = true;
            this.radioButton8EndDateNow.Font = new System.Drawing.Font("Tahoma", 8.75F);
            this.radioButton8EndDateNow.Location = new System.Drawing.Point(23, 23);
            this.radioButton8EndDateNow.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioButton8EndDateNow.Name = "radioButton8EndDateNow";
            this.radioButton8EndDateNow.Size = new System.Drawing.Size(153, 18);
            this.radioButton8EndDateNow.TabIndex = 15;
            this.radioButton8EndDateNow.TabStop = true;
            this.radioButton8EndDateNow.Text = "ภายในวันที่ส่ง (23.59 น.)";
            this.radioButton8EndDateNow.UseVisualStyleBackColor = true;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.GroupBoxMessage);
            this.panel6.Controls.Add(this.panel3);
            this.panel6.Controls.Add(this.groupBox3);
            this.panel6.Controls.Add(this.groupBox2);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel6.Location = new System.Drawing.Point(465, 40);
            this.panel6.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(491, 599);
            this.panel6.TabIndex = 48;
            // 
            // SenderDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(963, 692);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximumSize = new System.Drawing.Size(1009, 730);
            this.MinimumSize = new System.Drawing.Size(979, 730);
            this.Name = "SenderDialogForm";
            this.Padding = new System.Windows.Forms.Padding(7);
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "BPM News Broadcast - ส่งข้อความข่าว";
            this.Load += new System.EventHandler(this.SenderDialogForm_Load);
            this.GroupBoxMessage.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.areaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.branchInfoBindingSource)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRole)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.roleInfoBindingSource)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GroupBoxMessage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label LabelHeader;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox textBoxTopic;
        private System.Windows.Forms.Label labelTopic;
        private System.Windows.Forms.RichTextBox richTextBoxDetail;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton5Branch;
        private System.Windows.Forms.RadioButton radioButton1All;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.MaskedTextBox maskedTextBox2;
        private System.Windows.Forms.MaskedTextBox maskedTextBox1;
        private System.Windows.Forms.RadioButton radioButton6SentDateNow;
        private System.Windows.Forms.RadioButton radioButton7SentDateFixed;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioButton9EndDateFixed;
        private System.Windows.Forms.RadioButton radioButton8EndDateNow;
        private System.Windows.Forms.DataGridView dataGridViewRole;
        private System.Windows.Forms.BindingSource roleInfoBindingSource;
        private System.Windows.Forms.BindingSource branchInfoBindingSource;
        private System.Windows.Forms.ComboBox comboBoxBranch;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox comboBoxArea;
        private System.Windows.Forms.RadioButton radioButton4Area;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button buttonRoleClear;
        private System.Windows.Forms.Button buttonRoleSelectAll;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.BindingSource areaBindingSource;
        private System.Windows.Forms.RadioButton radioButton3NonFixedRegis;
        private System.Windows.Forms.RadioButton radioButton2FixedRegis;
        private System.Windows.Forms.Button buttonCopyMessage;
        private System.Windows.Forms.Button buttonPasteMessage;
        private System.Windows.Forms.Button buttonViewSentUserList;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CheckBoxRole;
        private System.Windows.Forms.DataGridViewTextBoxColumn roleIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn roleNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
    }
}