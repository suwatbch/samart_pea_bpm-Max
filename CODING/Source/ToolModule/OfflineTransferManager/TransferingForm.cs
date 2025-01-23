using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using PEA.BPM.Architecture.ArchitectureInterface.Constants;

namespace OfflineTransferManager
{
    public partial class ExportingForm : Form
    {
        public ExportingForm()
        {
            InitializeComponent();
        }

        private void exportRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (exportRadioButton.Checked == true)
            {
                pathGroupBox.Text = "Directory/Folder/Path ����Ѻ�Ӣ������͡";
                exportButton.Visible = true;
                importButton.Visible = false;
                pathTextBox.Text = "";
            }
        }

        private void importRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (importRadioButton.Checked == true)
            {
                pathGroupBox.Text = "Directory/Folder/Path ����Ѻ�Ӣ��������";
                exportButton.Visible = false;
                importButton.Visible = true;
                pathTextBox.Text = "";
            }
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(BPMPath.ConfigPath + "\\OfflineData"))
            {
                MessageBox.Show("�к���辺 Path �ͧ�����зӡ�ù��͡�ҡ�к�", "��ͼԴ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (pathTextBox.Text.Trim() == String.Empty)
            {
                MessageBox.Show("��س��к� Directory/Folder/Path ���кѹ�֡���������͹��͡�ҡ�к�", "��ͼԴ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            string[] fileEntries = Directory.GetFiles(BPMPath.ConfigPath + "\\OfflineData");

            if (fileEntries.Length > 0)
            {
                foreach (string pFilename in fileEntries)
                {
                    File.Copy(pFilename, pathTextBox.Text.Trim() + Path.GetFileName(pFilename));

                    if (!Directory.Exists(BPMPath.ConfigPath + "\\OfflineData\\Done"))
                    {
                        Directory.CreateDirectory(BPMPath.ConfigPath + "\\OfflineData\\Done");
                    }

                    File.Move(pFilename, BPMPath.ConfigPath + "\\OfflineData\\Done\\" + Path.GetFileName(pFilename));
                }

                MessageBox.Show("�к��ӡ�ùӢ������͡��ѧ Directory/Folder/Path ����к�������º��������", "��ͤ�����͹", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("����բ����š�ê����Թ����ҧ����к�  ��з�����͢����ջѭ��", "��ͤ�����͹", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void importButton_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(BPMPath.ConfigPath + "\\OfflineData"))
            {
                MessageBox.Show("�к���辺 Path �ͧ�����зӡ�ù�����к�", "��ͼԴ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (pathTextBox.Text.Trim() == String.Empty)
            {
                MessageBox.Show("��س��к� Directory/Folder/Path ���йӢ���������к�", "��ͼԴ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string[] fileEntries = Directory.GetFiles(pathTextBox.Text.Trim(), "?-*.txt", SearchOption.TopDirectoryOnly);

                if (fileEntries.Length > 0)
                {
                    if (!Directory.Exists(BPMPath.ConfigPath + "\\OfflineData\\Done"))
                    {
                        Directory.CreateDirectory(BPMPath.ConfigPath + "\\OfflineData\\Done");
                    }

                    foreach (string pFilename in fileEntries)
                    {
                        File.Copy(pFilename, BPMPath.ConfigPath + "\\OfflineData\\" + Path.GetFileName(pFilename));
                    }

                    MessageBox.Show("�к��ӡ�ùӢ���������к����º��������", "��ͤ�����͹", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("����բ����š�ê����Թ����ҧ����к�  ��з�����͢����ջѭ��", "��ͤ�����͹", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("�к��������ö�Ӣ���������к���", "��ͼԴ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }    
        }

        private void pathButton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                pathTextBox.Text = folderBrowserDialog1.SelectedPath;
            }
        }
    }
}