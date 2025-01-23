
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

namespace PEA.BPM.PaymentManagementModule
{
    partial class PaymentView
    {
        /// <summary>
        /// The presenter used by this view.
        /// </summary>
        private PEA.BPM.PaymentManagementModule.PaymentViewPresenter _presenter = null;

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
            this.searchPanel = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.IsuRadioBtn = new System.Windows.Forms.RadioButton();
            this.APRadioBtn = new System.Windows.Forms.RadioButton();
            this.isu3MaskedTb = new System.Windows.Forms.MaskedTextBox();
            this.isu1MaskedTb = new System.Windows.Forms.MaskedTextBox();
            this.runningNoMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.isu2MaskedTb = new System.Windows.Forms.MaskedTextBox();
            this.branchIdMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.paymentDtMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.gAmountMaskedTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.requestorIdMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.paymentClearButton = new System.Windows.Forms.Button();
            this.addPaymentButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.searchPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchPanel
            // 
            this.searchPanel.BackColor = System.Drawing.Color.White;
            this.searchPanel.Controls.Add(this.groupBox1);
            this.searchPanel.Controls.Add(this.groupBox3);
            this.searchPanel.Controls.Add(this.label1);
            this.searchPanel.Location = new System.Drawing.Point(10, 11);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Size = new System.Drawing.Size(210, 338);
            this.searchPanel.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.IsuRadioBtn);
            this.groupBox1.Controls.Add(this.APRadioBtn);
            this.groupBox1.Controls.Add(this.isu3MaskedTb);
            this.groupBox1.Controls.Add(this.isu1MaskedTb);
            this.groupBox1.Controls.Add(this.runningNoMaskedTextBox);
            this.groupBox1.Controls.Add(this.isu2MaskedTb);
            this.groupBox1.Controls.Add(this.branchIdMaskedTextBox);
            this.groupBox1.Controls.Add(this.paymentDtMaskedTextBox);
            this.groupBox1.Location = new System.Drawing.Point(7, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(197, 115);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "�Ţ�����Ӥѭ����";
            // 
            // IsuRadioBtn
            // 
            this.IsuRadioBtn.AutoSize = true;
            this.IsuRadioBtn.Location = new System.Drawing.Point(59, 25);
            this.IsuRadioBtn.Name = "IsuRadioBtn";
            this.IsuRadioBtn.Size = new System.Drawing.Size(48, 18);
            this.IsuRadioBtn.TabIndex = 2;
            this.IsuRadioBtn.Text = "IS-U";
            this.IsuRadioBtn.UseVisualStyleBackColor = true;
            this.IsuRadioBtn.Click += new System.EventHandler(this.APFormat_CheckedChanged);
            // 
            // APRadioBtn
            // 
            this.APRadioBtn.AutoSize = true;
            this.APRadioBtn.Checked = true;
            this.APRadioBtn.Location = new System.Drawing.Point(12, 25);
            this.APRadioBtn.Name = "APRadioBtn";
            this.APRadioBtn.Size = new System.Drawing.Size(45, 18);
            this.APRadioBtn.TabIndex = 1;
            this.APRadioBtn.TabStop = true;
            this.APRadioBtn.Text = "A/P";
            this.APRadioBtn.UseVisualStyleBackColor = true;
            this.APRadioBtn.Click += new System.EventHandler(this.APFormat_CheckedChanged);
            this.APRadioBtn.CheckedChanged += new System.EventHandler(this.APFormat_CheckedChanged);
            // 
            // isu3MaskedTb
            // 
            this.isu3MaskedTb.Location = new System.Drawing.Point(126, 80);
            this.isu3MaskedTb.Name = "isu3MaskedTb";
            this.isu3MaskedTb.Size = new System.Drawing.Size(58, 22);
            this.isu3MaskedTb.TabIndex = 8;
            this.isu3MaskedTb.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.isu3MaskedTb.Leave += new System.EventHandler(this.isu3MaskedTb_Leave);
            this.isu3MaskedTb.Enter += new System.EventHandler(this.maskedTextBox_Enter);
            this.isu3MaskedTb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.isu3MaskedTb_KeyPress);
            this.isu3MaskedTb.KeyUp += new System.Windows.Forms.KeyEventHandler(this.isu3MaskedTb_KeyUp);
            // 
            // isu1MaskedTb
            // 
            this.isu1MaskedTb.Location = new System.Drawing.Point(7, 80);
            this.isu1MaskedTb.Name = "isu1MaskedTb";
            this.isu1MaskedTb.Size = new System.Drawing.Size(51, 22);
            this.isu1MaskedTb.TabIndex = 6;
            this.isu1MaskedTb.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.isu1MaskedTb.Leave += new System.EventHandler(this.isu1MaskedTb_Leave);
            this.isu1MaskedTb.Enter += new System.EventHandler(this.maskedTextBox_Enter);
            this.isu1MaskedTb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.isu1MaskedTb_KeyPress);
            this.isu1MaskedTb.KeyUp += new System.Windows.Forms.KeyEventHandler(this.isu1MaskedTb_KeyUp);
            this.isu1MaskedTb.TextChanged += new System.EventHandler(this.MaskedTextBox_TextChanged);
            // 
            // runningNoMaskedTextBox
            // 
            this.runningNoMaskedTextBox.Location = new System.Drawing.Point(126, 48);
            this.runningNoMaskedTextBox.Name = "runningNoMaskedTextBox";
            this.runningNoMaskedTextBox.Size = new System.Drawing.Size(58, 22);
            this.runningNoMaskedTextBox.TabIndex = 5;
            this.runningNoMaskedTextBox.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.runningNoMaskedTextBox.Leave += new System.EventHandler(this.runningNoMaskedTextBox_Leave);
            this.runningNoMaskedTextBox.Enter += new System.EventHandler(this.maskedTextBox_Enter);
            this.runningNoMaskedTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.runningNoMaskedTextBox_KeyPress);
            this.runningNoMaskedTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.runningNoMaskedTextBox_KeyUp);
            // 
            // isu2MaskedTb
            // 
            this.isu2MaskedTb.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.isu2MaskedTb.Location = new System.Drawing.Point(64, 80);
            this.isu2MaskedTb.Name = "isu2MaskedTb";
            this.isu2MaskedTb.Size = new System.Drawing.Size(58, 22);
            this.isu2MaskedTb.TabIndex = 7;
            this.isu2MaskedTb.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.isu2MaskedTb.Leave += new System.EventHandler(this.isu2MaskedTb_Leave);
            this.isu2MaskedTb.Enter += new System.EventHandler(this.maskedTextBox_Enter);
            this.isu2MaskedTb.KeyUp += new System.Windows.Forms.KeyEventHandler(this.isu2MaskedTb_KeyUp);
            // 
            // branchIdMaskedTextBox
            // 
            this.branchIdMaskedTextBox.Location = new System.Drawing.Point(9, 48);
            this.branchIdMaskedTextBox.Name = "branchIdMaskedTextBox";
            this.branchIdMaskedTextBox.Size = new System.Drawing.Size(40, 22);
            this.branchIdMaskedTextBox.TabIndex = 3;
            this.branchIdMaskedTextBox.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.branchIdMaskedTextBox.Leave += new System.EventHandler(this.branchIdMaskedTextBox_Leave);
            this.branchIdMaskedTextBox.Enter += new System.EventHandler(this.maskedTextBox_Enter);
            this.branchIdMaskedTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.branchIdMaskedTextBox_KeyPress);
            this.branchIdMaskedTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.branchIdMaskedTextBox_KeyUp);
            this.branchIdMaskedTextBox.TextChanged += new System.EventHandler(this.MaskedTextBox_TextChanged);
            // 
            // paymentDtMaskedTextBox
            // 
            this.paymentDtMaskedTextBox.Location = new System.Drawing.Point(53, 48);
            this.paymentDtMaskedTextBox.Name = "paymentDtMaskedTextBox";
            this.paymentDtMaskedTextBox.Size = new System.Drawing.Size(69, 22);
            this.paymentDtMaskedTextBox.TabIndex = 4;
            this.paymentDtMaskedTextBox.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.paymentDtMaskedTextBox.Leave += new System.EventHandler(this.paymentDtMaskedTextBox_Leave);
            this.paymentDtMaskedTextBox.Enter += new System.EventHandler(this.maskedTextBox_Enter);
            this.paymentDtMaskedTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.paymentDtMaskedTextBox_KeyUp);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.gAmountMaskedTextBox);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.requestorIdMaskedTextBox);
            this.groupBox3.Controls.Add(this.paymentClearButton);
            this.groupBox3.Controls.Add(this.addPaymentButton);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(7, 172);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(196, 163);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "�����š�è����Թ";
            // 
            // gAmountMaskedTextBox
            // 
            this.gAmountMaskedTextBox.Location = new System.Drawing.Point(9, 99);
            this.gAmountMaskedTextBox.MaxLength = 17;
            this.gAmountMaskedTextBox.Name = "gAmountMaskedTextBox";
            this.gAmountMaskedTextBox.Size = new System.Drawing.Size(181, 22);
            this.gAmountMaskedTextBox.TabIndex = 10;
            this.gAmountMaskedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.gAmountMaskedTextBox.Leave += new System.EventHandler(this.gAmountMaskedTextBox_Leave);
            this.gAmountMaskedTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.gAmountMaskedTextBox_KeyPress);
            this.gAmountMaskedTextBox.Enter += new System.EventHandler(this.gAmountMaskedTextBox_Enter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 14);
            this.label4.TabIndex = 14;
            this.label4.Text = "���ʼ���ԡ";
            // 
            // requestorIdMaskedTextBox
            // 
            this.requestorIdMaskedTextBox.Location = new System.Drawing.Point(9, 45);
            this.requestorIdMaskedTextBox.Name = "requestorIdMaskedTextBox";
            this.requestorIdMaskedTextBox.Size = new System.Drawing.Size(181, 22);
            this.requestorIdMaskedTextBox.TabIndex = 9;
            this.requestorIdMaskedTextBox.Leave += new System.EventHandler(this.requestorIdMaskedTextBox_Leave);
            this.requestorIdMaskedTextBox.Enter += new System.EventHandler(this.maskedTextBox_Enter);
            this.requestorIdMaskedTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.requestorIdMaskedTextBox_KeyPress);
            this.requestorIdMaskedTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.requestorIdMaskedTextBox_KeyUp);
            this.requestorIdMaskedTextBox.TextChanged += new System.EventHandler(this.MaskedTextBox_TextChanged);
            // 
            // paymentClearButton
            // 
            this.paymentClearButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.paymentClearButton.Location = new System.Drawing.Point(104, 127);
            this.paymentClearButton.Name = "paymentClearButton";
            this.paymentClearButton.Size = new System.Drawing.Size(87, 25);
            this.paymentClearButton.TabIndex = 12;
            this.paymentClearButton.Text = "��ҧ���";
            this.paymentClearButton.UseVisualStyleBackColor = false;
            this.paymentClearButton.Click += new System.EventHandler(this.paymentClearButton_Click);
            // 
            // addPaymentButton
            // 
            this.addPaymentButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.addPaymentButton.Location = new System.Drawing.Point(9, 127);
            this.addPaymentButton.Name = "addPaymentButton";
            this.addPaymentButton.Size = new System.Drawing.Size(87, 25);
            this.addPaymentButton.TabIndex = 11;
            this.addPaymentButton.Text = "����";
            this.addPaymentButton.UseVisualStyleBackColor = false;
            this.addPaymentButton.Click += new System.EventHandler(this.addPaymentButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "�ӹǹ�Թ�ط��";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(8, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "�����Թ�����Ӥѭ����";
            // 
            // PaymentView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(211, 117, 191);
            this.Controls.Add(this.searchPanel);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "PaymentView";
            this.Padding = new System.Windows.Forms.Padding(12, 11, 12, 11);
            this.Size = new System.Drawing.Size(229, 726);
            this.searchPanel.ResumeLayout(false);
            this.searchPanel.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel searchPanel;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button paymentClearButton;
        private System.Windows.Forms.Button addPaymentButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox branchIdMaskedTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox runningNoMaskedTextBox;
        private System.Windows.Forms.MaskedTextBox paymentDtMaskedTextBox;
        private System.Windows.Forms.MaskedTextBox requestorIdMaskedTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton IsuRadioBtn;
        private System.Windows.Forms.RadioButton APRadioBtn;
        private System.Windows.Forms.MaskedTextBox isu3MaskedTb;
        private System.Windows.Forms.MaskedTextBox isu1MaskedTb;
        private System.Windows.Forms.MaskedTextBox isu2MaskedTb;
        private System.Windows.Forms.TextBox gAmountMaskedTextBox;
    }
}

