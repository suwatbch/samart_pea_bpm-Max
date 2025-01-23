using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Reports;
using System.IO;
using System.Globalization;

namespace PEA.BPM.PaymentCollectionModule.ReportViews.CAC19CriteriaView
{
    [SmartPart]
    public partial class CAC19CriteriaView : UserControl, ICAC19CriteriaView
    {
        private CAC19Param repParam;
        private int _recordCount;
        private List<string> resultReadFile;
        private List<CAC19QRPaymentReport> bankQRConvertList;
        private bool isCancelReadTextProcess; 
        BackgroundWorker readTextFileBgWorker = new BackgroundWorker();

        public CAC19CriteriaView()
        {
            InitializeComponent();

            readTextFileBgWorker.DoWork += new DoWorkEventHandler(bgWorker_DoWork);
            readTextFileBgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgWorker_RunWorkerCompleted);
            readTextFileBgWorker.ProgressChanged += new ProgressChangedEventHandler(m_oWorker_ProgressChanged);
            readTextFileBgWorker.WorkerReportsProgress = true;
            readTextFileBgWorker.WorkerSupportsCancellation = true;
        }

        [CreateNew]
        public CAC19CriteriaViewPresenter Presenter
        {
            set
            {
                _presenter = value;
                _presenter.View = this;
            }
        }


        #region ICAC19CriteriaView Members

        protected override void OnLoad(EventArgs e)
        {
            _presenter.OnViewReady();

            this.branchInfotextBox.Text = string.Format("{0} : {1}", Session.Branch.Id, Session.Branch.Name);

        }

        public void OnWaitCursor(bool set)
        {
            if (set)
            {
                this.Cursor = Cursors.WaitCursor;
            }
            else
            {
                this.Cursor = Cursors.Default;
            }
        }

        #endregion

        private void previewBt_Click(object sender, EventArgs e)
        {
            try
            {

                if (readTextFileBgWorker.IsBusy)
                    return;
                            
                // Read text file 
                resultReadFile = ReadTextFile();
                if (resultReadFile.Count > 0)
                {
                    isCancelReadTextProcess = false;
                    // Run convert ข้อมูลด้วย Async.
                    readTextFileBgWorker.RunWorkerAsync();

                    // เมื่อ Convert object เรียบร้อยจึงจะทำการ Call ให้ presenter แสดงข้อมูลรายงาน.

                    //param.BankQRPayment = resultReadFile;
                    //_presenter.OnShowReport(param);
                }

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                ServiceHelper.TransformErrorMessage(ex);
            }
        }

        private List<string> ReadTextFile()
        {
            List<string> result = new List<string>(); 
            try
            {
                string ref1 = string.Empty;
                string transactionType = string.Empty;
                var fileContent = string.Empty;
                var filePath = string.Empty;

                //Get the path of specified file
                filePath = this.fullPathFileNameTextBox.Text;
                
                string[] lines = File.ReadAllLines(filePath,Encoding.Default);

                // Validate date on text 
                if (lines.Count() > 0)
                {
                    // Check length of text
                    if (lines[0].StartsWith("H"))
                    {
                        // ตรวจสอบวันที่ใน 
                        string effeectiveDt = lines[0].Substring(60, 8);
                        string serverDt = Session.BpmDateTime.Now.ToString("ddMMyyyy", new CultureInfo("th-TH"));
                    }
                    else
                    {
                        // ไม่ใช่ Header.
                        return result;
                    }

                    foreach (string item in lines)
                    {
                        if (item.StartsWith("D"))
                            result.Add(item);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        private void selBankTextFilebutton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openBankFileDialog = new OpenFileDialog();

            openBankFileDialog.RestoreDirectory = true;
            openBankFileDialog.Title = "เลือกข้อมูลชำระเงินจากทางธนาคาร";
            openBankFileDialog.Filter = "txt files (*.txt)|*.txt";

            if (openBankFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.fullPathFileNameTextBox.Text = openBankFileDialog.FileName;

                var fileContent = string.Empty;
                var filePath = string.Empty;

                //Get the path of specified file
                filePath = openBankFileDialog.FileName;


                string lineOne = File.ReadAllLines(this.fullPathFileNameTextBox.Text).FirstOrDefault();
                if (string.IsNullOrEmpty(lineOne) || !lineOne.StartsWith("H") || lineOne.Length != 450)
                {
                    MessageBox.Show("ไฟล์ข้อมูลไม่ตรงตามรูปแบบที่กำหนด", "ข้อมูลการชำระเงินธนาคาร", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.fullPathFileNameTextBox.Text = string.Empty;
                    return;
                }
                else
                {
                    // ตรวจสอบวันที่ในข้อมูลบรรทัดแรก  TryParse
                    string effeectiveDt = lineOne.Substring(60, 8);
                    string serverDt = Session.BpmDateTime.Now.ToString("ddMMyyyy", new CultureInfo("en-US"));

                    if (effeectiveDt != serverDt)
                    {
                        MessageBox.Show("วันที่เอกสารไม่ถูกต้อง", "ตรวจสอบวันที่เอกสาร", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.fullPathFileNameTextBox.Text = string.Empty;
                        return;
                    }
                }
            }


        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e) {
            try
            {
                this._recordCount = this.resultReadFile.Count(); 

                bankQRConvertList = new List<CAC19QRPaymentReport>();

                int iCount = 0; 
                foreach (var item in this.resultReadFile)
                {
                    iCount += 1; 

                    CAC19QRPaymentReport qrPayment = new CAC19QRPaymentReport(item);
                    if (qrPayment.BranchID == Session.Branch.Id)
                        bankQRConvertList.Add(qrPayment);

                    readTextFileBgWorker.ReportProgress((iCount * 100)/_recordCount);

                    if (readTextFileBgWorker.CancellationPending)
                    {
                        isCancelReadTextProcess = true; 
                        e.Cancel = true;
                        readTextFileBgWorker.ReportProgress(0);
                        return;
                    }
                }
            }
            catch (Exception)
            {
            }

        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {

            // กรณีเป็นการยกเลิก
            if (isCancelReadTextProcess)
                return; 

            CAC19Param param = new CAC19Param();
            param.BranchId = Session.Branch.Id;
            param.TransFromDate = this.paymentDateDtPicker.Value;
            param.TransToDate = this.paymentDateDtPicker.Value;
            param.Report = ReportName.CAC19;

            param.BankQRPayment = bankQRConvertList;
            _presenter.OnShowReport(param);
        }

        private void m_oWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lblStatus.Text = "อ่านข้อมูล......" + e.ProgressPercentage.ToString() + " % ";
        }

        private void cancelBt_Click(object sender, EventArgs e)
        {
            if (readTextFileBgWorker.IsBusy)
            {
                // Stop/Cancel async operation 
                readTextFileBgWorker.CancelAsync();
            }
        }

      

    }



}

