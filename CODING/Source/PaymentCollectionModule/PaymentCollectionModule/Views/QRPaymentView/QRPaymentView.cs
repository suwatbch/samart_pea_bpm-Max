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
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Architecture.ArchitectureTool;
using PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities;
using System.IO;
using System.Globalization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using PEA.BPM.Architecture.ArchitectureInterface.Constants;

namespace PEA.BPM.PaymentCollectionModule.Views.QRPaymentView
{
    [SmartPart]
    public partial class QRPaymentView : UserControl, IQRPaymentView
    {
        decimal _balancePaid = 0;
        bool _processQRPayment = false;
        private QRPaymentInfo _qrPaymentInfo;
        private QRPaymentInfo _postOfflineResultInfo;

        PaymentMethod _pmQR = null;
        BackgroundWorker bgWorker = new BackgroundWorker();
        BackgroundWorker bgPostOfflinePayWorker = new BackgroundWorker();

        QRPaymentInfo _qrPaymentInfoStateOfStatus;
        public QRPaymentView()
        {
            InitializeComponent();

            bgWorker.DoWork += new DoWorkEventHandler(bgWorker_DoWork);
            bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgWorker_RunWorkerCompleted);

            bgPostOfflinePayWorker.DoWork += new DoWorkEventHandler(bgPostOfflinePayWorker_DoWork);
            bgPostOfflinePayWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgPostOfflinePayWorker_RunWorkerCompleted);
        }

        [CreateNew]
        public QRPaymentViewPresenter Presenter
        {
            set
            {
                _presenter = value;
                _presenter.View = this;
            }
            get
            {
                return _presenter;
            }
        }

        public void ResetForm()
        {
        }

        #region IQRPaymentView Members

        public decimal balancePaid
        {
            get
            {
                return this._balancePaid;
            }
            set
            {
                this._balancePaid = value;
            }
        }

        public PaymentMethod QRPaymentMethod
        {
            get
            {
                return _pmQR;
            }
            set
            {
                _pmQR = value;

                AssignDataForQR();
            }
        }

        public void AssignDataForQR()
        {
            // Display QR data , Ref1, Ref2.
            var qrData = _pmQR.BankName.Split('|').ToList();
            this.qrRef1textBox.Text = qrData[0];
            this.qrRef2textBox.Text = qrData[1];

            qrAmounttextBox.Text = _pmQR.ToPayAmount.Value.ToString();
        }

        public bool processedQR
        {
            get
            {
                return _processQRPayment;
            }
            set
            {
                _processQRPayment = value;
            }
        }

        public void OfflineProcess()
        {
            this.lblStatus.Text = string.Empty;
            this.checkStatusQRButton.Enabled = false; // ไม่ให้ทำการ ตรวจสอบสถานะ.
            this.qrReferencetextBox.ReadOnly = false;
            this.qrReferencetextBox.Enabled = true;
        }

        public void InitialMode()
        {
            OfflineDisabled();

            // 20231222  ไม่สามารถออกจากหน้าจอได้ ต้องกดตรวจสอบสถานะ 1 ครั้ง
            this.cancelButton.Enabled = false;

            // display button and offline 
            this.checkStatusQRButton.Visible = true;
            this.chkOffLineQRPayment.Visible = true;
            this.chkOffLineQRPayment.Checked = false;

            // ไม่สามารถทำ Offline
            // ต้องทำการตรวจสอบสถานะอย่างน้อย 1 ครั้ง
            // กรอกรหัสอ้างอิงไม่ได้. 
            this.chkOffLineQRPayment.Enabled = false;
            this.checkStatusQRButton.Enabled = true;

            // Clear ค่าเดิมที่แสดงอยู่
            this.qrReferencetextBox.Text = string.Empty;
            this.qrReferencetextBox.ReadOnly = true;
            this.qrReferencetextBox.Enabled = false;


            // Reset result check status model 
            if (_qrPaymentInfo != null)
            {

                this.lblStatus.Text = string.Empty;
                this.QRPaymentCheckStatusResult = string.Empty;

                _qrPaymentInfo.amount = 0;
                _qrPaymentInfo.ref1 = string.Empty;
                _qrPaymentInfo.ref2 = string.Empty;
                _qrPaymentInfo.status = string.Empty;
                _qrPaymentInfo.referenceNo = string.Empty;
                _qrPaymentInfo.QRStatus = false; // false ยังไม่ได้สร้าง QR ในระบบ PEA QR Service.
                _qrPaymentInfo.QRPayStatus = false; // false QR ในระบบของ PEA QR Service ยังไม่ได้รับการชำระ
            }
        }


        public void CheckStateOfQR()
        {

            // Clear state. 
            if (_qrPaymentInfoStateOfStatus != null && _qrPaymentInfo != null)
            {
                if (this.qrRef1textBox.Text.Trim() != _qrPaymentInfoStateOfStatus.ref1 || this.qrRef2textBox.Text.Trim() != _qrPaymentInfoStateOfStatus.ref2) // กรณี Ref1 หรือ Ref2 ไม่ตรงกัน 
                {
                    _qrPaymentInfoStateOfStatus = new QRPaymentInfo();
                }
                else
                {
                    // กรณี Ref1, Ref2 เดิม.
                    // Offline. 
                    if (Session.IsNetworkConnectionAvailable == true && Session.IsForcedOffline == false && _qrPaymentInfoStateOfStatus.QRStatus && _qrPaymentInfoStateOfStatus.QRPayStatus == false)
                    {
                        // เปิดให้ทำการ Offline ได้. 
                        this.CheckStatusQREnabled(false);
                        this.OfflineEnabled();
                    }
                }
            }
            else
            {
                _qrPaymentInfoStateOfStatus = new QRPaymentInfo();
            }

        }

        public void OfflineEnabled()
        {
            this.chkOffLineQRPayment.Enabled = true;

            // Can't input qrReference.
            this.qrReferencetextBox.Enabled = false;
            this.qrReferencetextBox.Text = string.Empty;

            this.CheckStatusQREnabled(true);
        }

        public void CheckStatusQREnabled(bool enableFlag)
        {
            //ขณะทำการตรวจสอบสถานะ 
            //ไม่สามารถทำ Offline
            //ไม่สามารถกดปุ่มตกลง และปุ่มยกเลิก รอจนกว่าจะตรวจสอบเสร็จ 

            this.checkStatusQRButton.Enabled = enableFlag;
            this.okButton.Enabled = enableFlag;
            this.cancelButton.Enabled = enableFlag;

            if (enableFlag == false)
            {
                // clean status label.
                this.lblStatus.Text = string.Empty;
            }
        }

        public void CheckStatusQRProcess()
        {

            if (Session.IsNetworkConnectionAvailable == true && Session.IsForcedOffline == false)
            {
                this.CheckStatusQREnabled(false);
                OfflineDisabled();

                bgWorker.RunWorkerAsync();
            }
            else
            {
                MessageBox.Show("ไม่สามารถทำการสอบถามสถานะได้ เนื่องจาก Network offline ", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                // ตรวจสอบหากเคยมีการสอบถามสถานะ ว่ามี QR Code ในระบบ PEA API แล้วสามารถทำการ Offline ได้
                if (this.QRPaymentInfoStateOfStatus.QRStatus && this.QRPaymentInfoStateOfStatus.QRPayStatus == false)
                    OfflineEnabled();
                else
                    OfflineDisabled();
            }
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Presenter.checkStatusQRPayment();
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string stringQRNotFoundItem = CodeTable.Instant.GetAppSettingValue("QRPayment_NotFoundQR_Item");

            // ผลการตรวจสอบ สถานะการชำระเงินด้วย QR Payment. 
            if (this.QRPaymentInfo != null && this.QRPaymentInfo.status.ToUpper() == "SUCCESS")
            {
                QRPaymentSueecess();
            }
            else
            {
                // สถานะการชำระเงิน ไม่สำเร็จ. 
                this.CheckStatusQREnabled(true);
                this.checkStatusQRButton.Focus();

                if (QRPaymentInfo.ResponseStatus == false && QRPaymentInfo.ResponseErrorMessage == stringQRNotFoundItem) // ไม่พบ QR ในระบบ
                    this.OfflineDisabled();
                else if (QRPaymentInfo.ResponseStatus == false && QRPaymentInfo.QRPayStatus == false && QRPaymentInfo.QRStatus == true) // ตรวจสอบสถานะ(ครั้งนี้) แล้ว มี QR ในระบบแล้ว
                    this.OfflineEnabled();
                else if (this.QRPaymentInfoStateOfStatus.QRStatus && this.QRPaymentInfoStateOfStatus.QRPayStatus == false) // เคยตรวจสอบถานะ มี QR ในระบบแล้ว
                    this.OfflineEnabled();
                else if (QRPaymentInfo.ResponseStatus == false && QRPaymentInfo.ResponseErrorMessage == null)
                    this.OfflineDisabled();

            }

            // 20231222 ปุ่มยกเลิกจะต้องทำกดปุ่มตรวจสอบสถานะ 1 ครั้ง 
            if (!cancelButton.Enabled)
                this.cancelButton.Enabled = true;


            // Display 
            this.lblStatus.Text = this.QRPaymentCheckStatusResult;
        }

        private void bgPostOfflinePayWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Presenter.PostOfflinePay();

        }

        private void bgPostOfflinePayWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }

        public void OfflineDisabled()
        {
            this.chkOffLineQRPayment.Enabled = false;
        }

        public void DisplayReferenceNo()
        {
            this.qrReferencetextBox.Text = this._pmQR.GroupBankName;
        }

        public QRPaymentInfo QRPaymentInfo
        {
            get
            {
                return _qrPaymentInfo;
            }
            set
            {
                _qrPaymentInfo = value;
            }
        }

        public void QRPaymentSueecess()
        {
            // ตรวจสอบสถานะการชำระเงิน สำเร็จ 
            DisplayReferenceNo();

            this.okButton.Enabled = true;
            this.qrReferencetextBox.Enabled = true; // เพื่อปรับให้เป็น Read Only.
            this.qrReferencetextBox.ReadOnly = true;
            this.okButton.ForeColor = Color.Blue;

            // ซ่อมปุ่ม offline, และปุ่มตรวจสอบสถานะ.
            this.checkStatusQRButton.Visible = false;
            this.chkOffLineQRPayment.Visible = false;
            this.okButton.Focus();
        }

        private string _qrPaymentCheckStatusResult;

        public string QRPaymentCheckStatusResult
        {
            set
            {
                _qrPaymentCheckStatusResult = value;
            }

            get
            {
                return _qrPaymentCheckStatusResult;
            }
        }

        public QRPaymentInfo PostOfflineResultInfo
        {
            get
            {
                return _postOfflineResultInfo;
            }
            set
            {
                _postOfflineResultInfo = value;
            }
        }

        public void PostOfflinePaymentProcess()
        {
            try
            {
                this.bgPostOfflinePayWorker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                //throw;
            }

        }

        #endregion

        private void okButton_Click(object sender, EventArgs e)
        {
            // Assign reference value from bank to payment.
            if (!string.IsNullOrEmpty(qrReferencetextBox.Text))
            {

                _pmQR.GroupBankName = qrReferencetextBox.Text.Trim();
                _pmQR.CashierChequeFlag = "1"; // is QR payment online transaction 

                // Validate offline QR payment flag. 
                // Property online flag.
                if (this.chkOffLineQRPayment.Checked)
                {
                    _pmQR.CashierChequeFlag = "0";
                    // Post data to pea service.
                    PostOfflinePaymentProcess();
                }
                else
                {
                    _pmQR.CashierChequeFlag = "1";
                }


                // Test text file turn off qr on smart screen.
                if (_pmQR.GroupBankName.Trim().Length > 0)
                    _presenter.SmartScreenTurnOffQRPayment(false);

                this.ParentForm.Close();
            }
            else
            {
                MessageBox.Show("ขั้นตอนการชำระผ่าน QR Payment ยังไม่สำเร็จ.", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Exclamation,MessageBoxDefaultButton.Button2);
            }
        }

        private void okButton_VisibleChanged(object sender, EventArgs e)
        {
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            // 20231214 : 2.0.5 (REV.3) 
            string confirmMessage = "การยกเลิกรายการ จะไม่สามารถออกใบเสร็จรับเงินครั้งนี้ได้   " + Environment.NewLine + "หากยืนยันยกเลิกรายการ กดปุ่ม OK เพื่อกลับไปหน้าจอระบุวิธีการชำระเงิน";
            if (MessageBox.Show(confirmMessage, "ยืนยัน", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                _pmQR.GroupBankName = qrReferencetextBox.Text.Trim();
                //_pmQR.CashierChequeFlag = "1"; // is QR payment online transaction 

                // Text file turn off qr on smart screen.
                _pmQR.GroupBankName = string.Empty;
                _presenter.SmartScreenTurnOffQRPayment(true);

                this.ParentForm.Close();
            }
        }

        private void chkOffLineQRPayment_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkOffLineQRPayment.CheckState == CheckState.Checked)
            {
                OfflineProcess();
            }
            else
            {
                OfflineEnabled();
                CheckStatusQREnabled(true);
            }
        }

        private void checkStatusQRButton_Click(object sender, EventArgs e)
        {
            CheckStatusQRProcess();
        }

        private void lblStatus_Click(object sender, EventArgs e)
        {

        }


        #region IQRPaymentView Members


        public QRPaymentInfo QRPaymentInfoStateOfStatus
        {
            get
            {
                return _qrPaymentInfoStateOfStatus;
            }
            set
            {
                _qrPaymentInfoStateOfStatus = value;
            }
        }

        #endregion
    }
}
