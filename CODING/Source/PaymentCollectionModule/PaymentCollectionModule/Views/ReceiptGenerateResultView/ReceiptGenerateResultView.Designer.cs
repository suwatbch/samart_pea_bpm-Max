
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
    partial class ReceiptGenerateResultView
    {
        /// <summary>
        /// The presenter used by this view.
        /// </summary>
        private PEA.BPM.PaymentCollectionModule.ReceiptGenerateResultViewPresenter _presenter = null;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel10 = new System.Windows.Forms.Panel();
            this.panelButton = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.PaymentNonReceiptGV = new System.Windows.Forms.DataGridView();
            this.CheckboxGV = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.CaIdGV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CaNameGV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InvoiceGV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GAmountGV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PaymentDtGV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PmNameGV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CashierGV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelPayment = new System.Windows.Forms.Panel();
            this.panel12 = new System.Windows.Forms.Panel();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.panel9 = new System.Windows.Forms.Panel();
            this.panelSummary = new System.Windows.Forms.Panel();
            this.txtGenRecAmount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAmountAll = new System.Windows.Forms.TextBox();
            this.txtPaymentGenRec = new System.Windows.Forms.TextBox();
            this.txtPaymentAll = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panelButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PaymentNonReceiptGV)).BeginInit();
            this.panelPayment.SuspendLayout();
            this.panel12.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panelSummary.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(10, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(780, 60);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(13, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(369, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "��¡���Ѻ�����Թ��������������Ѻ�Թ";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel3);
            this.panel4.Controls.Add(this.panel2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(10, 70);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(780, 6);
            this.panel4.TabIndex = 21;
            // 
            // panel3
            // 
            this.panel3.Location = new System.Drawing.Point(3, 6);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(993, 100);
            this.panel3.TabIndex = 23;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Location = new System.Drawing.Point(0, 14);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(996, 243);
            this.panel2.TabIndex = 1;
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnOk.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnOk.Location = new System.Drawing.Point(37, 18);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(98, 30);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "��ŧ";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnCancel.Location = new System.Drawing.Point(145, 18);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(98, 30);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "¡�&�ԡ";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.White;
            this.panel10.Controls.Add(this.panelButton);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel10.Location = new System.Drawing.Point(10, 482);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(780, 65);
            this.panel10.TabIndex = 22;
            // 
            // panelButton
            // 
            this.panelButton.Controls.Add(this.btnCancel);
            this.panelButton.Controls.Add(this.btnOk);
            this.panelButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelButton.Location = new System.Drawing.Point(509, 0);
            this.panelButton.Name = "panelButton";
            this.panelButton.Size = new System.Drawing.Size(271, 65);
            this.panelButton.TabIndex = 4;
            // 
            // panel8
            // 
            this.panel8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel8.Location = new System.Drawing.Point(10, 476);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(780, 6);
            this.panel8.TabIndex = 27;
            // 
            // PaymentNonReceiptGV
            // 
            this.PaymentNonReceiptGV.AllowUserToAddRows = false;
            this.PaymentNonReceiptGV.AllowUserToDeleteRows = false;
            this.PaymentNonReceiptGV.AllowUserToResizeRows = false;
            this.PaymentNonReceiptGV.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.PaymentNonReceiptGV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.PaymentNonReceiptGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CheckboxGV,
            this.CaIdGV,
            this.CaNameGV,
            this.InvoiceGV,
            this.GAmountGV,
            this.PaymentDtGV,
            this.PmNameGV,
            this.CashierGV});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.PaymentNonReceiptGV.DefaultCellStyle = dataGridViewCellStyle6;
            this.PaymentNonReceiptGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PaymentNonReceiptGV.Location = new System.Drawing.Point(0, 0);
            this.PaymentNonReceiptGV.MultiSelect = false;
            this.PaymentNonReceiptGV.Name = "PaymentNonReceiptGV";
            this.PaymentNonReceiptGV.RowHeadersVisible = false;
            this.PaymentNonReceiptGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.PaymentNonReceiptGV.ShowCellToolTips = false;
            this.PaymentNonReceiptGV.Size = new System.Drawing.Size(766, 319);
            this.PaymentNonReceiptGV.TabIndex = 0;
            this.PaymentNonReceiptGV.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.AgentPaymentGV_CellContentClick);
            // 
            // CheckboxGV
            // 
            this.CheckboxGV.Frozen = true;
            this.CheckboxGV.HeaderText = "";
            this.CheckboxGV.Name = "CheckboxGV";
            this.CheckboxGV.Width = 30;
            // 
            // CaIdGV
            // 
            this.CaIdGV.DataPropertyName = "CaId";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.NullValue = null;
            this.CaIdGV.DefaultCellStyle = dataGridViewCellStyle2;
            this.CaIdGV.FillWeight = 120F;
            this.CaIdGV.HeaderText = "�����Ţ������";
            this.CaIdGV.MaxInputLength = 16;
            this.CaIdGV.Name = "CaIdGV";
            this.CaIdGV.ReadOnly = true;
            // 
            // CaNameGV
            // 
            this.CaNameGV.DataPropertyName = "CaName";
            this.CaNameGV.FillWeight = 250F;
            this.CaNameGV.HeaderText = "���ͼ�����";
            this.CaNameGV.Name = "CaNameGV";
            this.CaNameGV.ReadOnly = true;
            this.CaNameGV.Width = 200;
            // 
            // InvoiceGV
            // 
            this.InvoiceGV.DataPropertyName = "InvoiceNo";
            this.InvoiceGV.FillWeight = 150F;
            this.InvoiceGV.HeaderText = "�Ţ������˹��";
            this.InvoiceGV.Name = "InvoiceGV";
            this.InvoiceGV.ReadOnly = true;
            this.InvoiceGV.Width = 150;
            // 
            // GAmountGV
            // 
            this.GAmountGV.DataPropertyName = "GAmount";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = "0.00";
            this.GAmountGV.DefaultCellStyle = dataGridViewCellStyle3;
            this.GAmountGV.FillWeight = 90F;
            this.GAmountGV.HeaderText = "�ӹǹ�Թ";
            this.GAmountGV.Name = "GAmountGV";
            this.GAmountGV.ReadOnly = true;
            this.GAmountGV.Width = 90;
            // 
            // PaymentDtGV
            // 
            this.PaymentDtGV.DataPropertyName = "PaymentDt";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.Format = "dd/MM/yyyy";
            dataGridViewCellStyle4.NullValue = null;
            this.PaymentDtGV.DefaultCellStyle = dataGridViewCellStyle4;
            this.PaymentDtGV.FillWeight = 110F;
            this.PaymentDtGV.HeaderText = "�ѹ����Ѻ�����Թ";
            this.PaymentDtGV.Name = "PaymentDtGV";
            this.PaymentDtGV.ReadOnly = true;
            this.PaymentDtGV.Width = 110;
            // 
            // PmNameGV
            // 
            this.PmNameGV.DataPropertyName = "PmName";
            this.PmNameGV.FillWeight = 120F;
            this.PmNameGV.HeaderText = "��������ê����Թ";
            this.PmNameGV.Name = "PmNameGV";
            this.PmNameGV.ReadOnly = true;
            this.PmNameGV.Width = 120;
            // 
            // CashierGV
            // 
            this.CashierGV.DataPropertyName = "Cashier";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.CashierGV.DefaultCellStyle = dataGridViewCellStyle5;
            this.CashierGV.HeaderText = "����Ѻ����";
            this.CashierGV.Name = "CashierGV";
            this.CashierGV.ReadOnly = true;
            this.CashierGV.Width = 160;
            // 
            // panelPayment
            // 
            this.panelPayment.Controls.Add(this.panel12);
            this.panelPayment.Controls.Add(this.panel9);
            this.panelPayment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPayment.Location = new System.Drawing.Point(0, 0);
            this.panelPayment.Name = "panelPayment";
            this.panelPayment.Padding = new System.Windows.Forms.Padding(7);
            this.panelPayment.Size = new System.Drawing.Size(780, 400);
            this.panelPayment.TabIndex = 0;
            // 
            // panel12
            // 
            this.panel12.Controls.Add(this.chkAll);
            this.panel12.Controls.Add(this.PaymentNonReceiptGV);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel12.Location = new System.Drawing.Point(7, 7);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(766, 319);
            this.panel12.TabIndex = 1;
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(10, 6);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(15, 14);
            this.chkAll.TabIndex = 1;
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.panelSummary);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel9.Location = new System.Drawing.Point(7, 326);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(766, 67);
            this.panel9.TabIndex = 2;
            // 
            // panelSummary
            // 
            this.panelSummary.Controls.Add(this.txtGenRecAmount);
            this.panelSummary.Controls.Add(this.label3);
            this.panelSummary.Controls.Add(this.label4);
            this.panelSummary.Controls.Add(this.label2);
            this.panelSummary.Controls.Add(this.txtAmountAll);
            this.panelSummary.Controls.Add(this.txtPaymentGenRec);
            this.panelSummary.Controls.Add(this.txtPaymentAll);
            this.panelSummary.Controls.Add(this.label5);
            this.panelSummary.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelSummary.Location = new System.Drawing.Point(177, 0);
            this.panelSummary.Name = "panelSummary";
            this.panelSummary.Size = new System.Drawing.Size(589, 67);
            this.panelSummary.TabIndex = 0;
            // 
            // txtGenRecAmount
            // 
            this.txtGenRecAmount.BackColor = System.Drawing.Color.White;
            this.txtGenRecAmount.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtGenRecAmount.ForeColor = System.Drawing.Color.Red;
            this.txtGenRecAmount.Location = new System.Drawing.Point(448, 40);
            this.txtGenRecAmount.Name = "txtGenRecAmount";
            this.txtGenRecAmount.ReadOnly = true;
            this.txtGenRecAmount.Size = new System.Drawing.Size(138, 23);
            this.txtGenRecAmount.TabIndex = 7;
            this.txtGenRecAmount.Text = "0.00";
            this.txtGenRecAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(386, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "���Թ :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label4.Location = new System.Drawing.Point(386, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "���Թ :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(66, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(248, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "��¡���Ѻ�����Թ������������稷����� :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAmountAll
            // 
            this.txtAmountAll.BackColor = System.Drawing.Color.White;
            this.txtAmountAll.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtAmountAll.ForeColor = System.Drawing.Color.Blue;
            this.txtAmountAll.Location = new System.Drawing.Point(448, 11);
            this.txtAmountAll.Name = "txtAmountAll";
            this.txtAmountAll.ReadOnly = true;
            this.txtAmountAll.Size = new System.Drawing.Size(138, 23);
            this.txtAmountAll.TabIndex = 3;
            this.txtAmountAll.Text = "0.00";
            this.txtAmountAll.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPaymentGenRec
            // 
            this.txtPaymentGenRec.BackColor = System.Drawing.Color.White;
            this.txtPaymentGenRec.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtPaymentGenRec.ForeColor = System.Drawing.Color.Red;
            this.txtPaymentGenRec.Location = new System.Drawing.Point(317, 40);
            this.txtPaymentGenRec.Name = "txtPaymentGenRec";
            this.txtPaymentGenRec.ReadOnly = true;
            this.txtPaymentGenRec.Size = new System.Drawing.Size(51, 23);
            this.txtPaymentGenRec.TabIndex = 5;
            this.txtPaymentGenRec.Text = "0";
            this.txtPaymentGenRec.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPaymentAll
            // 
            this.txtPaymentAll.BackColor = System.Drawing.Color.White;
            this.txtPaymentAll.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtPaymentAll.ForeColor = System.Drawing.Color.Blue;
            this.txtPaymentAll.Location = new System.Drawing.Point(317, 11);
            this.txtPaymentAll.Name = "txtPaymentAll";
            this.txtPaymentAll.ReadOnly = true;
            this.txtPaymentAll.Size = new System.Drawing.Size(51, 23);
            this.txtPaymentAll.TabIndex = 1;
            this.txtPaymentAll.Text = "0";
            this.txtPaymentAll.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label5.Location = new System.Drawing.Point(58, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(256, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "��¡���Ѻ�����Թ����ͧ������ҧ����� :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.White;
            this.panel6.Controls.Add(this.panelPayment);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(10, 76);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(780, 400);
            this.panel6.TabIndex = 31;
            // 
            // ReceiptGenerateResultView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(211, 117, 191);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel10);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.Name = "ReceiptGenerateResultView";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(800, 557);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panelButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PaymentNonReceiptGV)).EndInit();
            this.panelPayment.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            this.panel12.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panelSummary.ResumeLayout(false);
            this.panelSummary.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Panel panelButton;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.DataGridView PaymentNonReceiptGV;
        private System.Windows.Forms.Panel panelPayment;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvoiceNoGV;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPaymentAll;
        private System.Windows.Forms.TextBox txtGenRecAmount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPaymentGenRec;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtAmountAll;
        private System.Windows.Forms.Panel panelSummary;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CheckboxGV;
        private System.Windows.Forms.DataGridViewTextBoxColumn CaIdGV;
        private System.Windows.Forms.DataGridViewTextBoxColumn CaNameGV;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvoiceGV;
        private System.Windows.Forms.DataGridViewTextBoxColumn GAmountGV;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaymentDtGV;
        private System.Windows.Forms.DataGridViewTextBoxColumn PmNameGV;
        private System.Windows.Forms.DataGridViewTextBoxColumn CashierGV;

    }
}

