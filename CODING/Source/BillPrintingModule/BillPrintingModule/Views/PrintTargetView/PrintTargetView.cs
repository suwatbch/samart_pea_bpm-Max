//----------------------------------------------------------------------------------------
// patterns & practices - Smart Client Software Factory - Guidance Package
//
// This file was generated by the "Add View" recipe.
//
// This class is the concrete implementation of a View in the Model-View-Presenter 
// pattern. Communication between the Presenter and this class is acheived through 
// an interface to facilitate separation and testability.
// Note that the Presenter generated by the same recipe, will automatically be created
// by CAB through [CreateNew] and bidirectional references will be added.
//
// For more information see:
// ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.scsf.2006jun/SCSF/html/03-030-Model%20View%20Presenter%20%20MVP%20.htm
//
// Latest version of this Guidance Package: http://go.microsoft.com/fwlink/?LinkId=62182
//----------------------------------------------------------------------------------------

using System;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.BillPrintingModule.Interface.Constants;

namespace PEA.BPM.BillPrintingModule
{
    [SmartPart]
    public partial class PrintTargetView : UserControl, IPrintTargetView
    {
        private string _oldPath;

        #region "Code Generated"

        public PrintTargetView()
        {
            InitializeComponent();
            LoadSetting();
        }

        /// <summary>
        /// Sets the presenter. The dependency injection system will automatically
        /// create a new presenter for you.
        /// </summary>
        [CreateNew]
        public PrintTargetViewPresenter Presenter
        {
            set
            {
                _presenter = value;
                _presenter.View = this;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            _presenter.OnViewReady();
        }

        #endregion

        private void LoadSetting()
        {
            LocalSettingHelper hp = LocalSettingHelper.Instance();
            string printTarget = (string)hp.Read(LocalSettingNames.PrintTarget);
            if (printTarget == "A")
            {
                printTargetPathText.Text = (string)hp.Read(LocalSettingNames.FilePrintTargetPath);
                toPrinterCheck.Checked = true;
                toFileCheck.Checked = true;
            }
            else if (printTarget == "F")
            {
                printTargetPathText.Text = (string)hp.Read(LocalSettingNames.FilePrintTargetPath);
                toFileCheck.Checked = true;
                toPrinterCheck.Checked = false;
            }
            else if (printTarget == "P")
            {
                toPrinterCheck.Checked = true;
                toFileCheck.Checked = false;
            }
            else
                MessageBox.Show("Internal error, read output target failed! ");

            _oldPath = printTargetPathText.Text;

        }

        private void toFileCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (toFileCheck.Checked)
            {
                printTargetPathText.Enabled = true;
                toPrinterCheck.Enabled = true;
                browseBt.Enabled = true;
            }
            else
            {
                printTargetPathText.Enabled = false;
                toPrinterCheck.Checked = true;
                toPrinterCheck.Enabled = false;
                browseBt.Enabled = false;
            }
        }

        private void browseBt_Click(object sender, EventArgs e)
        {
            string oldPath = printTargetPathText.Text;
            targetDlg.ShowDialog();
            if (System.IO.Directory.Exists(targetDlg.SelectedPath))
            {
                printTargetPathText.Text = targetDlg.SelectedPath;
            }
            else
            {
                MessageBox.Show("��ä����������͡���١��ͧ", "�Դ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                printTargetPathText.Text = oldPath;
            }
        }

        private void toPrinterCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (toPrinterCheck.Checked)
                choosePrinterBt.Enabled = true;
            else
                choosePrinterBt.Enabled = false;
        }

        private void choosePrinterBt_Click(object sender, EventArgs e)
        {
            _presenter.ShowPrinterSetupDialog();
        }

        private void okBt_Click(object sender, EventArgs e)
        {
            try
            {
                LocalSettingHelper hp = LocalSettingHelper.Instance();
                if (toFileCheck.Checked && toPrinterCheck.Checked)
                {
                    if (!System.IO.Directory.Exists(printTargetPathText.Text))
                    {
                        try    {
                            System.IO.Directory.CreateDirectory(printTargetPathText.Text);
                        }
                        catch
                        {
                            MessageBox.Show("�������ö���ҧ��ä��������˹��� \n��سҵ�Ǩ�ͺ�Է�ԡ����ҹ���͵�Ǩ�ͺ path �������ҧ", "�Դ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            printTargetPathText.Focus();
                            printTargetPathText.SelectAll();
                            return;
                        }
                    }

                    hp.Update(LocalSettingNames.PrintTarget, "A"); //All 
                    hp.Update(LocalSettingNames.FilePrintTargetPath, printTargetPathText.Text);
                }
                else if (toFileCheck.Checked)
                {
                    //create new                    
                    if (!System.IO.Directory.Exists(printTargetPathText.Text))
                    {
                        try   {
                            System.IO.Directory.CreateDirectory(printTargetPathText.Text);
                        }
                        catch
                        {
                            MessageBox.Show("�������ö���ҧ��ä��������˹��� \n��سҵ�Ǩ�ͺ�Է�ԡ����ҹ���͵�Ǩ�ͺ path �������ҧ", "�Դ��Ҵ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            printTargetPathText.Focus();
                            printTargetPathText.SelectAll();
                            return;
                        }
                    }

                    hp.Update(LocalSettingNames.PrintTarget, "F"); //File
                    hp.Update(LocalSettingNames.FilePrintTargetPath, printTargetPathText.Text);
                }
                else if (toPrinterCheck.Checked)
                {
                    hp.Update(LocalSettingNames.PrintTarget, "P"); //Printer
                    hp.Remove(LocalSettingNames.FilePrintTargetPath);
                }
                else
                    MessageBox.Show("Internal Error, Output target setting failed!");
            }
            catch // TODO: ���� ?
            {
            }

            _presenter.OnCloseView();
        }

        private void printTargetPathText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                printTargetPathText.Text = _oldPath;
            }
        }

        #region "Event Handling"


 


        #endregion
    }
}

