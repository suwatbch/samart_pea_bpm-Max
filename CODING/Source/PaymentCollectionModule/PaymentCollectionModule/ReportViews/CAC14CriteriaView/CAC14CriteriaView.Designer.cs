
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
    partial class CAC14CriteriaView
    {
        /// <summary>
        /// The presenter used by this view.
        /// </summary>
        private PEA.BPM.PaymentCollectionModule.CAC14CriteriaViewPresenter _presenter = null;

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
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.billBookIdMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.displayReportButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.searchPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchPanel
            // 
            this.searchPanel.BackColor = System.Drawing.Color.White;
            this.searchPanel.Controls.Add(this.label2);
            this.searchPanel.Controls.Add(this.groupBox1);
            this.searchPanel.Controls.Add(this.label1);
            this.searchPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchPanel.Location = new System.Drawing.Point(10, 10);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Size = new System.Drawing.Size(216, 179);
            this.searchPanel.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(7, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "����㺡ӡѺ����";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.billBookIdMaskedTextBox);
            this.groupBox1.Controls.Add(this.displayReportButton);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(10, 62);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(193, 103);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "���͹�";
            // 
            // billBookIdMaskedTextBox
            // 
            this.billBookIdMaskedTextBox.Location = new System.Drawing.Point(18, 42);
            this.billBookIdMaskedTextBox.Name = "billBookIdMaskedTextBox";
            this.billBookIdMaskedTextBox.Size = new System.Drawing.Size(156, 20);
            this.billBookIdMaskedTextBox.TabIndex = 1;
            this.billBookIdMaskedTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.billBookIdMaskedTextBox_KeyPress);
            // 
            // displayReportButton
            // 
            this.displayReportButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.displayReportButton.Location = new System.Drawing.Point(18, 69);
            this.displayReportButton.Name = "displayReportButton";
            this.displayReportButton.Size = new System.Drawing.Size(85, 23);
            this.displayReportButton.TabIndex = 3;
            this.displayReportButton.Text = "�ʴ���§ҹ";
            this.displayReportButton.UseVisualStyleBackColor = false;
            this.displayReportButton.Click += new System.EventHandler(this.displayReportButton_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "�Ţ��� ��.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(7, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(191, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "��§ҹ����¹�������俿�����";
            // 
            // CAC14CriteriaView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(211, 117, 191);
            this.Controls.Add(this.searchPanel);
            this.Name = "CAC14CriteriaView";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(236, 519);
            this.searchPanel.ResumeLayout(false);
            this.searchPanel.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel searchPanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.MaskedTextBox billBookIdMaskedTextBox;
        private System.Windows.Forms.Button displayReportButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
    }
}

