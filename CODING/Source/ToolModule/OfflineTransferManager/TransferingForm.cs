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
                pathGroupBox.Text = "Directory/Folder/Path สำหรับนำข้อมูลออก";
                exportButton.Visible = true;
                importButton.Visible = false;
                pathTextBox.Text = "";
            }
        }

        private void importRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (importRadioButton.Checked == true)
            {
                pathGroupBox.Text = "Directory/Folder/Path สำหรับนำข้อมูลเข้า";
                exportButton.Visible = false;
                importButton.Visible = true;
                pathTextBox.Text = "";
            }
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(BPMPath.ConfigPath + "\\OfflineData"))
            {
                MessageBox.Show("ระบบไม่พบ Path ของไฟล์ที่จะทำการนำออกจากระบบ", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (pathTextBox.Text.Trim() == String.Empty)
            {
                MessageBox.Show("กรุณาระบุ Directory/Folder/Path ที่จะบันทึกข้อมูลเพื่อนำออกจากระบบ", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                MessageBox.Show("ระบบทำการนำข้อมูลออกไปยัง Directory/Folder/Path ที่ระบุไว้เรียบร้อยแล้ว", "ข้อความเตือน", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("ไม่มีข้อมูลการชำระเงินคงค้างที่ระบบ  ขณะที่เครือข่ายมีปัญหา", "ข้อความเตือน", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void importButton_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(BPMPath.ConfigPath + "\\OfflineData"))
            {
                MessageBox.Show("ระบบไม่พบ Path ของไฟล์ที่จะทำการนำเข้าระบบ", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (pathTextBox.Text.Trim() == String.Empty)
            {
                MessageBox.Show("กรุณาระบุ Directory/Folder/Path ที่จะนำข้อมูลเข้าระบบ", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                    MessageBox.Show("ระบบทำการนำข้อมูลเข้าระบบเรียบร้อยแล้ว", "ข้อความเตือน", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("ไม่มีข้อมูลการชำระเงินคงค้างที่ระบบ  ขณะที่เครือข่ายมีปัญหา", "ข้อความเตือน", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ระบบไม่สามารถนำข้อมูลเข้าระบบได้", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
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