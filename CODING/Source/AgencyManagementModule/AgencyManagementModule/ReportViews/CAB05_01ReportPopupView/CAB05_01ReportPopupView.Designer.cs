
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
    partial class CAB05_01ReportPopupView
    {
        /// <summary>
        /// The presenter used by this view.
        /// </summary>
        private PEA.BPM.AgencyManagementModule.CAB05_01ReportPopupViewPresenter _presenter = null;

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
            this.previewCb = new System.Windows.Forms.CheckBox();
            this.Closebt = new System.Windows.Forms.Button();
            this.printbt = new System.Windows.Forms.Button();
            this.periodFromTb = new System.Windows.Forms.MaskedTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.periodToTb = new System.Windows.Forms.MaskedTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.endAgencyIdBt = new System.Windows.Forms.Button();
            this.endAgencyIdTb = new System.Windows.Forms.TextBox();
            this.startAgencyIdBt = new System.Windows.Forms.Button();
            this.startAgencyIdTb = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.printByAgencyrb = new System.Windows.Forms.RadioButton();
            this.printAllrb = new System.Windows.Forms.RadioButton();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // previewCb
            // 
            this.previewCb.AutoSize = true;
            this.previewCb.Checked = true;
            this.previewCb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.previewCb.Location = new System.Drawing.Point(39, 219);
            this.previewCb.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.previewCb.Name = "previewCb";
            this.previewCb.Size = new System.Drawing.Size(149, 20);
            this.previewCb.TabIndex = 13;
            this.previewCb.Text = "�ʴ�������ҧ��͹�����";
            this.previewCb.UseVisualStyleBackColor = true;
            // 
            // Closebt
            // 
            this.Closebt.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Closebt.Location = new System.Drawing.Point(452, 214);
            this.Closebt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Closebt.Name = "Closebt";
            this.Closebt.Size = new System.Drawing.Size(107, 28);
            this.Closebt.TabIndex = 12;
            this.Closebt.Text = "¡��ԡ";
            this.Closebt.UseVisualStyleBackColor = true;
            this.Closebt.Click += new System.EventHandler(this.Closebt_Click);
            // 
            // printbt
            // 
            this.printbt.Location = new System.Drawing.Point(338, 214);
            this.printbt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.printbt.Name = "printbt";
            this.printbt.Size = new System.Drawing.Size(107, 28);
            this.printbt.TabIndex = 11;
            this.printbt.Text = "�������§ҹ";
            this.printbt.UseVisualStyleBackColor = true;
            this.printbt.Click += new System.EventHandler(this.printbt_Click);
            // 
            // periodFromTb
            // 
            this.periodFromTb.Location = new System.Drawing.Point(84, 24);
            this.periodFromTb.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.periodFromTb.Mask = "00/0000";
            this.periodFromTb.Name = "periodFromTb";
            this.periodFromTb.Size = new System.Drawing.Size(116, 23);
            this.periodFromTb.TabIndex = 0;
            this.periodFromTb.KeyDown += new System.Windows.Forms.KeyEventHandler(this.periodFromTb_KeyDown);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.periodToTb);
            this.groupBox2.Controls.Add(this.periodFromTb);
            this.groupBox2.Location = new System.Drawing.Point(18, 126);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(541, 71);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "�����͹";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(210, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "�֧";
            // 
            // periodToTb
            // 
            this.periodToTb.Location = new System.Drawing.Point(241, 24);
            this.periodToTb.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.periodToTb.Mask = "00/0000";
            this.periodToTb.Name = "periodToTb";
            this.periodToTb.Size = new System.Drawing.Size(116, 23);
            this.periodToTb.TabIndex = 1;
            this.periodToTb.KeyDown += new System.Windows.Forms.KeyEventHandler(this.periodToTb_KeyDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.endAgencyIdBt);
            this.groupBox1.Controls.Add(this.endAgencyIdTb);
            this.groupBox1.Controls.Add(this.startAgencyIdBt);
            this.groupBox1.Controls.Add(this.startAgencyIdTb);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.printByAgencyrb);
            this.groupBox1.Controls.Add(this.printAllrb);
            this.groupBox1.Location = new System.Drawing.Point(18, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(541, 105);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "���᷹���Թ";
            // 
            // endAgencyIdBt
            // 
            this.endAgencyIdBt.Location = new System.Drawing.Point(468, 60);
            this.endAgencyIdBt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.endAgencyIdBt.Name = "endAgencyIdBt";
            this.endAgencyIdBt.Size = new System.Drawing.Size(35, 28);
            this.endAgencyIdBt.TabIndex = 15;
            this.endAgencyIdBt.Text = "...";
            this.endAgencyIdBt.UseVisualStyleBackColor = true;
            this.endAgencyIdBt.Click += new System.EventHandler(this.endAgencyIdBt_Click);
            // 
            // endAgencyIdTb
            // 
            this.endAgencyIdTb.Enabled = false;
            this.endAgencyIdTb.Location = new System.Drawing.Point(376, 63);
            this.endAgencyIdTb.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.endAgencyIdTb.Name = "endAgencyIdTb";
            this.endAgencyIdTb.Size = new System.Drawing.Size(84, 23);
            this.endAgencyIdTb.TabIndex = 14;
            // 
            // startAgencyIdBt
            // 
            this.startAgencyIdBt.Location = new System.Drawing.Point(299, 58);
            this.startAgencyIdBt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.startAgencyIdBt.Name = "startAgencyIdBt";
            this.startAgencyIdBt.Size = new System.Drawing.Size(35, 28);
            this.startAgencyIdBt.TabIndex = 13;
            this.startAgencyIdBt.Text = "...";
            this.startAgencyIdBt.UseVisualStyleBackColor = true;
            this.startAgencyIdBt.Click += new System.EventHandler(this.startAgencyIdBt_Click);
            // 
            // startAgencyIdTb
            // 
            this.startAgencyIdTb.Enabled = false;
            this.startAgencyIdTb.Location = new System.Drawing.Point(206, 62);
            this.startAgencyIdTb.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.startAgencyIdTb.Name = "startAgencyIdTb";
            this.startAgencyIdTb.Size = new System.Drawing.Size(84, 23);
            this.startAgencyIdTb.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(343, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 16);
            this.label1.TabIndex = 11;
            this.label1.Text = "�֧";
            // 
            // printByAgencyrb
            // 
            this.printByAgencyrb.AutoSize = true;
            this.printByAgencyrb.Location = new System.Drawing.Point(42, 62);
            this.printByAgencyrb.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.printByAgencyrb.Name = "printByAgencyrb";
            this.printByAgencyrb.Size = new System.Drawing.Size(145, 20);
            this.printByAgencyrb.TabIndex = 10;
            this.printByAgencyrb.Text = "�к����ʵ��᷹���Թ";
            this.printByAgencyrb.UseVisualStyleBackColor = true;
            this.printByAgencyrb.Click += new System.EventHandler(this.printByAgencyrb_Click);
            // 
            // printAllrb
            // 
            this.printAllrb.AutoSize = true;
            this.printAllrb.Checked = true;
            this.printAllrb.Location = new System.Drawing.Point(42, 34);
            this.printAllrb.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.printAllrb.Name = "printAllrb";
            this.printAllrb.Size = new System.Drawing.Size(207, 20);
            this.printAllrb.TabIndex = 9;
            this.printAllrb.TabStop = true;
            this.printAllrb.Text = "���᷹���Թ������㹡��俿��";
            this.printAllrb.UseVisualStyleBackColor = true;
            this.printAllrb.Click += new System.EventHandler(this.printAllrb_Click);
            // 
            // CAB05_01ReportPopupView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.previewCb);
            this.Controls.Add(this.Closebt);
            this.Controls.Add(this.printbt);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "CAB05_01ReportPopupView";
            this.Size = new System.Drawing.Size(579, 260);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox previewCb;
        private System.Windows.Forms.Button Closebt;
        private System.Windows.Forms.Button printbt;
        private System.Windows.Forms.MaskedTextBox periodFromTb;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox periodToTb;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button endAgencyIdBt;
        private System.Windows.Forms.TextBox endAgencyIdTb;
        private System.Windows.Forms.Button startAgencyIdBt;
        private System.Windows.Forms.TextBox startAgencyIdTb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton printByAgencyrb;
        private System.Windows.Forms.RadioButton printAllrb;
    }
}

