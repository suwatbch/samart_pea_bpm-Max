
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
    partial class UserHintView
    {
        /// <summary>
        /// The presenter used by this view.
        /// </summary>
        private PEA.BPM.AgencyManagementModule.UserHintViewPresenter _presenter = null;

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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.caption4 = new System.Windows.Forms.Label();
            this.caption3 = new System.Windows.Forms.Label();
            this.caption2 = new System.Windows.Forms.Label();
            this.caption1 = new System.Windows.Forms.Label();
            this.header = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Beige;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.caption4);
            this.panel1.Controls.Add(this.caption3);
            this.panel1.Controls.Add(this.caption2);
            this.panel1.Controls.Add(this.caption1);
            this.panel1.Controls.Add(this.header);
            this.panel1.Location = new System.Drawing.Point(6, 5);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(266, 175);
            this.panel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::PEA.BPM.AgencyManagementModule.Properties.Resources.Help;
            this.pictureBox1.Location = new System.Drawing.Point(12, 10);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(26, 26);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // caption4
            // 
            this.caption4.AutoSize = true;
            this.caption4.Location = new System.Drawing.Point(15, 113);
            this.caption4.Name = "caption4";
            this.caption4.Size = new System.Drawing.Size(106, 16);
            this.caption4.TabIndex = 4;
            this.caption4.Text = "������ҧ \"900011\"";
            // 
            // caption3
            // 
            this.caption3.AutoSize = true;
            this.caption3.Location = new System.Drawing.Point(15, 91);
            this.caption3.Name = "caption3";
            this.caption3.Size = new System.Drawing.Size(146, 16);
            this.caption3.TabIndex = 3;
            this.caption3.Text = "�����ʾ�ѡ�ҹ���俿��";
            // 
            // caption2
            // 
            this.caption2.AutoSize = true;
            this.caption2.Location = new System.Drawing.Point(15, 69);
            this.caption2.Name = "caption2";
            this.caption2.Size = new System.Drawing.Size(225, 16);
            this.caption2.TabIndex = 2;
            this.caption2.Text = "������ö�����駵��᷹���Թ ����";
            // 
            // caption1
            // 
            this.caption1.AutoSize = true;
            this.caption1.Location = new System.Drawing.Point(15, 47);
            this.caption1.Name = "caption1";
            this.caption1.Size = new System.Drawing.Size(151, 16);
            this.caption1.TabIndex = 1;
            this.caption1.Text = "��Сͺ���µ���Ţ 6 ��ѡ";
            // 
            // header
            // 
            this.header.AutoSize = true;
            this.header.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.header.ForeColor = System.Drawing.Color.Blue;
            this.header.Location = new System.Drawing.Point(41, 14);
            this.header.Name = "header";
            this.header.Size = new System.Drawing.Size(70, 13);
            this.header.TabIndex = 0;
            this.header.Text = "���ʵ��᷹";
            // 
            // UserHintView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Beige;
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "UserHintView";
            this.Size = new System.Drawing.Size(278, 185);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label header;
        private System.Windows.Forms.Label caption4;
        private System.Windows.Forms.Label caption3;
        private System.Windows.Forms.Label caption2;
        private System.Windows.Forms.Label caption1;
        private System.Windows.Forms.PictureBox pictureBox1;

    }
}

