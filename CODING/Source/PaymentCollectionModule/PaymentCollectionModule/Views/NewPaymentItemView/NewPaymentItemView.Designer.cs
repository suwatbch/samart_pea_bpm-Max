
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

namespace PEA.BPM.PaymentCollectionModule
{
    partial class NewPaymentItemView
    {
        /// <summary>
        /// The presenter used by this view.
        /// </summary>
        private PEA.BPM.PaymentCollectionModule.NewPaymentItemViewPresenter _presenter = null;

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
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.onlineStatusLabel = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dueDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.totalExcludeVatLabel = new System.Windows.Forms.Label();
            this.taxLabel = new System.Windows.Forms.Label();
            this.amountMaskedTextBox = new PEA.BPM.Architecture.CommonUtilities.TextBoxDecimal();
            this.unitTypeComboBox = new System.Windows.Forms.ComboBox();
            this.vatRateComboBox = new System.Windows.Forms.ComboBox();
            this.debtTypeComboBox = new System.Windows.Forms.ComboBox();
            this.IvIdMaskedTextBox = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.vatLabel = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.descriptionMaskedTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.qtyMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.searchButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.caTaxBranchMaskedTextBox = new PEA.BPM.Architecture.CommonUtilities.TextBoxDecimal();
            this.caTaxIdMaskedTextBox = new PEA.BPM.Architecture.CommonUtilities.TextBoxDecimal();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.addressMaskedTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.nameMaskedTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.customerIdMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(10, 10);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(693, 519);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.onlineStatusLabel);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.searchButton);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.customerIdMaskedTextBox);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.groupBox1.Location = new System.Drawing.Point(5, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(683, 465);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // onlineStatusLabel
            // 
            this.onlineStatusLabel.AutoSize = true;
            this.onlineStatusLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.onlineStatusLabel.ForeColor = System.Drawing.Color.Red;
            this.onlineStatusLabel.Location = new System.Drawing.Point(349, 18);
            this.onlineStatusLabel.Name = "onlineStatusLabel";
            this.onlineStatusLabel.Size = new System.Drawing.Size(287, 16);
            this.onlineStatusLabel.TabIndex = 27;
            this.onlineStatusLabel.Text = "���͢����ջѭ�� ��سҡ�͡������ ����-��������ͧ";
            this.onlineStatusLabel.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dueDateTimePicker);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.totalExcludeVatLabel);
            this.groupBox3.Controls.Add(this.taxLabel);
            this.groupBox3.Controls.Add(this.amountMaskedTextBox);
            this.groupBox3.Controls.Add(this.unitTypeComboBox);
            this.groupBox3.Controls.Add(this.vatRateComboBox);
            this.groupBox3.Controls.Add(this.debtTypeComboBox);
            this.groupBox3.Controls.Add(this.IvIdMaskedTextBox);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.vatLabel);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.descriptionMaskedTextBox);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.qtyMaskedTextBox);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(25, 204);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(629, 243);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "��������´˹�����ͧ��ê���";
            // 
            // dueDateTimePicker
            // 
            this.dueDateTimePicker.CustomFormat = "dd/MM/yyyy";
            this.dueDateTimePicker.Enabled = false;
            this.dueDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dueDateTimePicker.Location = new System.Drawing.Point(504, 58);
            this.dueDateTimePicker.Name = "dueDateTimePicker";
            this.dueDateTimePicker.Size = new System.Drawing.Size(101, 23);
            this.dueDateTimePicker.TabIndex = 8;
            this.dueDateTimePicker.Value = new System.DateTime(2007, 7, 23, 0, 0, 0, 0);
            this.dueDateTimePicker.ValueChanged += new System.EventHandler(this.dueDateTimePicker_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(411, 61);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(82, 16);
            this.label10.TabIndex = 28;
            this.label10.Text = "�ѹ�ú��˹�";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // totalExcludeVatLabel
            // 
            this.totalExcludeVatLabel.Location = new System.Drawing.Point(152, 151);
            this.totalExcludeVatLabel.Name = "totalExcludeVatLabel";
            this.totalExcludeVatLabel.Size = new System.Drawing.Size(237, 16);
            this.totalExcludeVatLabel.TabIndex = 21;
            this.totalExcludeVatLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // taxLabel
            // 
            this.taxLabel.AutoSize = true;
            this.taxLabel.Location = new System.Drawing.Point(395, 177);
            this.taxLabel.Name = "taxLabel";
            this.taxLabel.Size = new System.Drawing.Size(25, 16);
            this.taxLabel.TabIndex = 20;
            this.taxLabel.Text = "(0)";
            this.taxLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // amountMaskedTextBox
            // 
            this.amountMaskedTextBox.Location = new System.Drawing.Point(152, 204);
            this.amountMaskedTextBox.MaxLength = 16;
            this.amountMaskedTextBox.MaxLengthDecimalPoint = 2;
            this.amountMaskedTextBox.Name = "amountMaskedTextBox";
            this.amountMaskedTextBox.Size = new System.Drawing.Size(237, 23);
            this.amountMaskedTextBox.TabIndex = 13;
            this.amountMaskedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.amountMaskedTextBox.TextChanged += new System.EventHandler(this.amountMaskedTextBox_TextChanged);
            this.amountMaskedTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.amountMaskedTextBox_KeyPress);
            // 
            // unitTypeComboBox
            // 
            this.unitTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.unitTypeComboBox.FormattingEnabled = true;
            this.unitTypeComboBox.Location = new System.Drawing.Point(347, 117);
            this.unitTypeComboBox.Name = "unitTypeComboBox";
            this.unitTypeComboBox.Size = new System.Drawing.Size(258, 24);
            this.unitTypeComboBox.TabIndex = 11;
            this.unitTypeComboBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.unitTypeComboBox_KeyPress);
            this.unitTypeComboBox.Click += new System.EventHandler(this.unitTypeComboBox_Click);
            // 
            // vatRateComboBox
            // 
            this.vatRateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.vatRateComboBox.Enabled = false;
            this.vatRateComboBox.FormattingEnabled = true;
            this.vatRateComboBox.Location = new System.Drawing.Point(152, 174);
            this.vatRateComboBox.Name = "vatRateComboBox";
            this.vatRateComboBox.Size = new System.Drawing.Size(237, 24);
            this.vatRateComboBox.TabIndex = 12;
            this.vatRateComboBox.SelectedIndexChanged += new System.EventHandler(this.vatRateComboBox_SelectedIndexChanged);
            this.vatRateComboBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.vatRateComboBox_KeyPress);
            // 
            // debtTypeComboBox
            // 
            this.debtTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.debtTypeComboBox.FormattingEnabled = true;
            this.debtTypeComboBox.Location = new System.Drawing.Point(152, 27);
            this.debtTypeComboBox.Name = "debtTypeComboBox";
            this.debtTypeComboBox.Size = new System.Drawing.Size(453, 24);
            this.debtTypeComboBox.TabIndex = 6;
            this.debtTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.debtTypeComboBox_SelectedIndexChanged);
            this.debtTypeComboBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.debtTypeComboBox_KeyPress);
            this.debtTypeComboBox.Click += new System.EventHandler(this.debtTypeComboBox_Click);
            // 
            // IvIdMaskedTextBox
            // 
            this.IvIdMaskedTextBox.Location = new System.Drawing.Point(152, 57);
            this.IvIdMaskedTextBox.MaxLength = 22;
            this.IvIdMaskedTextBox.Name = "IvIdMaskedTextBox";
            this.IvIdMaskedTextBox.Size = new System.Drawing.Size(237, 23);
            this.IvIdMaskedTextBox.TabIndex = 7;
            this.IvIdMaskedTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IvIdMaskedTextBox_KeyPress);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(45, 60);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(88, 16);
            this.label14.TabIndex = 2;
            this.label14.Text = "�Ţ������˹��";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // vatLabel
            // 
            this.vatLabel.AutoSize = true;
            this.vatLabel.Location = new System.Drawing.Point(283, 177);
            this.vatLabel.Name = "vatLabel";
            this.vatLabel.Size = new System.Drawing.Size(0, 16);
            this.vatLabel.TabIndex = 14;
            this.vatLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(73, 207);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 16);
            this.label11.TabIndex = 15;
            this.label11.Text = "���������";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(100, 177);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 16);
            this.label9.TabIndex = 12;
            this.label9.Text = "����";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // descriptionMaskedTextBox
            // 
            this.descriptionMaskedTextBox.Location = new System.Drawing.Point(152, 88);
            this.descriptionMaskedTextBox.MaxLength = 50;
            this.descriptionMaskedTextBox.Name = "descriptionMaskedTextBox";
            this.descriptionMaskedTextBox.Size = new System.Drawing.Size(453, 23);
            this.descriptionMaskedTextBox.TabIndex = 9;
            this.descriptionMaskedTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.descriptionMaskedTextBox_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(22, 91);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(111, 16);
            this.label8.TabIndex = 4;
            this.label8.Text = "��������´�������";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // qtyMaskedTextBox
            // 
            this.qtyMaskedTextBox.Location = new System.Drawing.Point(152, 118);
            this.qtyMaskedTextBox.Name = "qtyMaskedTextBox";
            this.qtyMaskedTextBox.PromptChar = ' ';
            this.qtyMaskedTextBox.Size = new System.Drawing.Size(137, 23);
            this.qtyMaskedTextBox.TabIndex = 10;
            this.qtyMaskedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.qtyMaskedTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.qtyMaskedTextBox_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(90, 121);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 16);
            this.label7.TabIndex = 6;
            this.label7.Text = "�ӹǹ";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(295, 121);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 16);
            this.label6.TabIndex = 8;
            this.label6.Text = "˹��¹Ѻ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 151);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(134, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "�ӹǹ�Թ (����������)";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(68, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "������˹��";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // searchButton
            // 
            this.searchButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.searchButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.searchButton.Location = new System.Drawing.Point(306, 15);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(28, 22);
            this.searchButton.TabIndex = 1;
            this.searchButton.Text = "...";
            this.searchButton.UseVisualStyleBackColor = false;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.caTaxBranchMaskedTextBox);
            this.groupBox2.Controls.Add(this.caTaxIdMaskedTextBox);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.addressMaskedTextBox);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.nameMaskedTextBox);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(25, 43);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(629, 155);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "��������ǹ���";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.Red;
            this.label15.Location = new System.Drawing.Point(149, 58);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(377, 16);
            this.label15.TabIndex = 16;
            this.label15.Text = "* �к��Ţ��Шӵ�Ǽ��������������Ң� �óռ����俿��������к� VAT";
            // 
            // caTaxBranchMaskedTextBox
            // 
            this.caTaxBranchMaskedTextBox.BackColor = System.Drawing.SystemColors.Menu;
            this.caTaxBranchMaskedTextBox.Location = new System.Drawing.Point(440, 78);
            this.caTaxBranchMaskedTextBox.MaxLength = 5;
            this.caTaxBranchMaskedTextBox.MaxLengthDecimalPoint = 2;
            this.caTaxBranchMaskedTextBox.Name = "caTaxBranchMaskedTextBox";
            this.caTaxBranchMaskedTextBox.Size = new System.Drawing.Size(165, 23);
            this.caTaxBranchMaskedTextBox.TabIndex = 4;
            this.caTaxBranchMaskedTextBox.TextChanged += new System.EventHandler(this.caTaxBranchMaskedTextBox_TextChanged);
            this.caTaxBranchMaskedTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.caTaxBranchMaskedTextBox_KeyPress);
            // 
            // caTaxIdMaskedTextBox
            // 
            this.caTaxIdMaskedTextBox.BackColor = System.Drawing.SystemColors.Menu;
            this.caTaxIdMaskedTextBox.Location = new System.Drawing.Point(152, 78);
            this.caTaxIdMaskedTextBox.MaxLength = 13;
            this.caTaxIdMaskedTextBox.MaxLengthDecimalPoint = 2;
            this.caTaxIdMaskedTextBox.Name = "caTaxIdMaskedTextBox";
            this.caTaxIdMaskedTextBox.Size = new System.Drawing.Size(237, 23);
            this.caTaxIdMaskedTextBox.TabIndex = 3;
            this.caTaxIdMaskedTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.caTaxIdMaskedTextBox_KeyPress);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(400, 83);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(36, 16);
            this.label12.TabIndex = 15;
            this.label12.Text = "�Ң�";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(19, 83);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(127, 16);
            this.label13.TabIndex = 13;
            this.label13.Text = "�Ţ��Шӵ�Ǽ����������";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // addressMaskedTextBox
            // 
            this.addressMaskedTextBox.Location = new System.Drawing.Point(152, 109);
            this.addressMaskedTextBox.MaxLength = 200;
            this.addressMaskedTextBox.Name = "addressMaskedTextBox";
            this.addressMaskedTextBox.ReadOnly = true;
            this.addressMaskedTextBox.Size = new System.Drawing.Size(453, 23);
            this.addressMaskedTextBox.TabIndex = 5;
            this.addressMaskedTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.addressMaskedTextBox_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(100, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "�������";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nameMaskedTextBox
            // 
            this.nameMaskedTextBox.Location = new System.Drawing.Point(152, 26);
            this.nameMaskedTextBox.MaxLength = 150;
            this.nameMaskedTextBox.Name = "nameMaskedTextBox";
            this.nameMaskedTextBox.ReadOnly = true;
            this.nameMaskedTextBox.Size = new System.Drawing.Size(237, 23);
            this.nameMaskedTextBox.TabIndex = 2;
            this.nameMaskedTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.nameMaskedTextBox_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(109, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "����";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(1, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "�����Ţ������/�����١˹��";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // customerIdMaskedTextBox
            // 
            this.customerIdMaskedTextBox.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.customerIdMaskedTextBox.Location = new System.Drawing.Point(177, 15);
            this.customerIdMaskedTextBox.Mask = "CCCCCCCCCCCCCC";
            this.customerIdMaskedTextBox.Name = "customerIdMaskedTextBox";
            this.customerIdMaskedTextBox.Size = new System.Drawing.Size(123, 23);
            this.customerIdMaskedTextBox.TabIndex = 0;
            this.customerIdMaskedTextBox.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.customerIdMaskedTextBox_MaskInputRejected);
            this.customerIdMaskedTextBox.Enter += new System.EventHandler(this.unitTypeComboBox_Click);
            this.customerIdMaskedTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.customerIdMaskedTextBox_KeyPress);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cancelButton);
            this.panel2.Controls.Add(this.okButton);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.panel2.Location = new System.Drawing.Point(5, 470);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(683, 44);
            this.panel2.TabIndex = 1;
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.cancelButton.Location = new System.Drawing.Point(107, 6);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(101, 36);
            this.cancelButton.TabIndex = 15;
            this.cancelButton.Text = "¡��ԡ";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // okButton
            // 
            this.okButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.okButton.Location = new System.Drawing.Point(0, 6);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(101, 36);
            this.okButton.TabIndex = 14;
            this.okButton.Text = "��ŧ";
            this.okButton.UseVisualStyleBackColor = false;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // NewPaymentItemView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(211, 117, 191);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "NewPaymentItemView";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(713, 539);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox customerIdMaskedTextBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;

        //private System.Windows.Forms.MaskedTextBox nameMaskedTextBox;
        private System.Windows.Forms.TextBox nameMaskedTextBox;

        //private System.Windows.Forms.MaskedTextBox addressMaskedTextBox;
        private System.Windows.Forms.TextBox addressMaskedTextBox;

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox descriptionMaskedTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.MaskedTextBox qtyMaskedTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label vatLabel;
        private System.Windows.Forms.TextBox IvIdMaskedTextBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox debtTypeComboBox;
        private System.Windows.Forms.Label onlineStatusLabel;
        private System.Windows.Forms.ComboBox vatRateComboBox;
        private System.Windows.Forms.ComboBox unitTypeComboBox;
        private PEA.BPM.Architecture.CommonUtilities.TextBoxDecimal amountMaskedTextBox;
        private System.Windows.Forms.Label taxLabel;
        private System.Windows.Forms.Label totalExcludeVatLabel;
        private System.Windows.Forms.DateTimePicker dueDateTimePicker;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private PEA.BPM.Architecture.CommonUtilities.TextBoxDecimal caTaxIdMaskedTextBox;
        private PEA.BPM.Architecture.CommonUtilities.TextBoxDecimal caTaxBranchMaskedTextBox;
        private System.Windows.Forms.Label label15;
    }
}

