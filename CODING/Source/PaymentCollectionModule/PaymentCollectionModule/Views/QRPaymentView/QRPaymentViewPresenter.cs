using System;
using System.Collections.Generic;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.EventBroker;
using PEA.BPM.Infrastructure.Interface;
using PEA.BPM.Infrastructure.Library.UI;
using System.Windows.Forms;
using PEA.BPM.PaymentCollectionModule.Interface.Constants;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.ArchitectureInterface.Constants;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using PEA.BPM.Architecture.ArchitectureTool;
using PEA.BPM.PaymentCollectionModule.Interface.Services;
using System.Linq;

namespace PEA.BPM.PaymentCollectionModule.Views.QRPaymentView
{
    public class QRPaymentViewPresenter : Presenter<IQRPaymentView>
    {
        public IBillingService _billingService;
        private QRPaymentInfo _qrPaymentInfo;

        [InjectionConstructor]
        public QRPaymentViewPresenter([ServiceDependency] IBillingService billingService)
        {
            _billingService = billingService;
        }


        bool isCheckingQRStatus = false;
        PaymentMethod _qrPayment;
        [EventSubscription(Constants.EventTopicNames.QRPaymentMethodClick, Thread = ThreadOption.UserInterface)]
        public void QRPaymentPaymentMethodClickHandler(object sender, EventArgs<PaymentMethod> e)
        {
            View.InitialMode();
            View.QRPaymentMethod = e.Data;
            _qrPayment = e.Data;
            // Raise request 

            // Save text file QR Payment. 
            SmartScreenDisplayQRPayment();

            View.InitialMode(); // Initial data. 

            View.CheckStateOfQR(); // Compare state and new QR Ref1, Ref2 ถ้าเหมือนเดิมและเคยตรวจสอบ QR มีในระบบแล้ว จะให้ Offline.

            ShowView();
        }

        private void ShowView()
        {
            PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo info = new PEA.BPM.Infrastructure.Library.UI.WindowSmartPartInfo();
            info.Modal = true;
            info.Keys.Add(WindowWorkspaceSetting.StartPosition, FormStartPosition.CenterParent);
            info.Keys.Add(WindowWorkspaceSetting.FormBorderStyle, FormBorderStyle.FixedToolWindow);
            //info.Keys.Add(WindowWorkspaceSetting.CancelButton, View.CancelButton);
            info.MaximizeBox = false;
            info.MinimizeBox = false;
            info.ControlBox = false;
            info.Title = " ## QRPayment";
            View.ResetForm();
            WorkItem.Workspaces[WorkspaceNames.ModalWindows].Show(View, info);
        }


        public void SmartScreenDisplayQRPayment()
        {
            // 20230310 DCR QR Payment
            try
            {
                string scrPath = CodeTable.Instant.GetAppSettingValue("SCR_PATH");
                string qrPath = BPMPath.ConfigPath + "\\" + scrPath;
                string pathFileName = qrPath + "\\QRDisplay.txt";

                if (!Directory.Exists(qrPath))
                {
                    Directory.CreateDirectory(qrPath);
                }

                // Crate text file for smart screen display QR Code
                using (StreamWriter writer = new StreamWriter(pathFileName))
                {
                    // View.QRPaymentMethod.BankName : store ref1|ref2
                    string data = string.Format("{0}|{1}", View.QRPaymentMethod.BankName, View.QRPaymentMethod.ToPayAmount.Value);
                    writer.WriteLine(data);
                }

                // Create QRDisplay.ok
                pathFileName = qrPath + "\\QRDisplay.ok";
                using (File.Create(pathFileName)) { }

            }
            catch
            {
                //throw;
            }
        }

        public void SmartScreenTurnOffQRPayment(bool isCancel)
        {
            string pathFilename = string.Empty;
            //20230310 DCR QR Payment

            try
            {
                string scrPath = CodeTable.Instant.GetAppSettingValue("SCR_PATH");
                string qrPath = BPMPath.ConfigPath + "\\" + scrPath;

                if (isCancel)
                    pathFilename = qrPath + "\\QRCANCEL";
                else
                    pathFilename = qrPath + "\\QRSUCCESS";

                if (!Directory.Exists(qrPath))
                {
                    Directory.CreateDirectory(qrPath);
                }

                using (File.Create(pathFilename + ".txt")) { }
                using (File.Create(pathFilename + ".ok")) { }
            }
            catch
            {
            }

        }

        public void checkStatusQRPayment()
        {
            /// DCR QR Payment for 2023-08
            /// Check status QR Payment
            /// Create object qr payment info. 
            QRPaymentInfo qrinfo = new QRPaymentInfo();

            View.QRPaymentCheckStatusResult = string.Empty;

            string stringQRNotFoundItem = CodeTable.Instant.GetAppSettingValue("QRPayment_NotFoundQR_Item");

            var pmData = View.QRPaymentMethod.BankName.Split('|').ToList();
            qrinfo.ref1 = pmData[0];
            qrinfo.ref2 = pmData[1];
            qrinfo.amount = View.QRPaymentMethod.ToPayAmount.Value;
            try
            {
                QRPaymentResponse _qrPaymentResponse = _billingService.CheckStatusQRPayment(qrinfo);
                if (_qrPaymentResponse.Status && _qrPaymentResponse.Message == "Success" && _qrPaymentResponse.Data.PayStatus)
                {
                    //View.QRPaymentInfo = _qrPaymentInfo;
                    if (View.QRPaymentInfo == null)
                        View.QRPaymentInfo = qrinfo;

                    View.QRPaymentInfo.status = _qrPaymentResponse.Message;
                    View.QRPaymentMethod.GroupBankName = _qrPaymentResponse.Data.PayBankRef;  // Group bank name คือตำแหน่งที่เก็บข้อมูล เลขที่อ้างอิง ของ BPM (PayApprovalCode ของ PEA api)
                }
                else if (_qrPaymentResponse.Status && _qrPaymentResponse.Message == "Success" && _qrPaymentResponse.Data != null &&
                    _qrPaymentResponse.Data.QrStatus == true && _qrPaymentResponse.Data.PayStatus == false)
                {
                    // _qrPaymentResponse.Data.QrStatus == true :  มีข้อมูล QR ในระบบเรียบร้อยแล้ว
                    //  _qrPaymentResponse.Message == "Success" :  สถานะการสอบถาม Success.
                    // _qrPaymentResponse.Status == false       :  ยังไม่มีการชำระ
                    //  Allow offline  มีข้อมูลในระบบ 
                    // Clear response status. 
                    if (View.QRPaymentInfo == null)
                        View.QRPaymentInfo = new QRPaymentInfo();

                    View.QRPaymentInfo.ResponseStatus = false;
                    View.QRPaymentInfo.ResponseErrorMessage = string.Empty;
                    View.QRPaymentInfo.QRStatus = _qrPaymentResponse.Data.QrStatus;
                    View.QRPaymentInfo.QRPayStatus = _qrPaymentResponse.Data.PayStatus;


                    View.QRPaymentCheckStatusResult = _qrPaymentResponse.ErrorMessage;

                    // Assing state of result.
                    View.QRPaymentInfoStateOfStatus.status = View.QRPaymentInfo.status;
                    View.QRPaymentInfoStateOfStatus.QRPayStatus = View.QRPaymentInfo.QRPayStatus;
                    View.QRPaymentInfoStateOfStatus.QRStatus = View.QRPaymentInfo.QRStatus;
                    View.QRPaymentInfoStateOfStatus.ref1 = qrinfo.ref1;
                    View.QRPaymentInfoStateOfStatus.ref2 = qrinfo.ref2;
                }
                else if (_qrPaymentResponse.Status == false && _qrPaymentResponse.ErrorMessage == stringQRNotFoundItem)
                {

                    if (View.QRPaymentInfo == null)
                    {
                        View.QRPaymentInfo = new QRPaymentInfo();
                    }
                    else
                    {
                        // Clear response status. 
                        View.QRPaymentInfo.ResponseStatus = false;
                        View.QRPaymentInfo.ResponseErrorMessage = string.Empty;

                    }

                    View.QRPaymentInfo.ResponseStatus = _qrPaymentResponse.Status;
                    View.QRPaymentInfo.ResponseErrorMessage = _qrPaymentResponse.ErrorMessage;
                    View.QRPaymentCheckStatusResult = _qrPaymentResponse.ErrorMessage;
                }
              
                else
                {
                    // Clear response status. 
                    if (View.QRPaymentInfo == null)
                        View.QRPaymentInfo = new QRPaymentInfo();

                    View.QRPaymentInfo.ResponseStatus = false;
                    View.QRPaymentInfo.ResponseErrorMessage = string.Empty;

                    View.QRPaymentCheckStatusResult = _qrPaymentResponse.ErrorMessage;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //throw;
            }

        }

        public void PostOfflinePay()
        {

            // Payment method. 
            //View.QRPaymentMethod

            /// DCR QR Payment for 2023-08
            /// Check status QR Payment
            /// Create object qr payment info. 
            QRPaymentInfo qrOfflinePayResultinfo = new QRPaymentInfo();

            var pmData = View.QRPaymentMethod.BankName.Split('|').ToList();
            qrOfflinePayResultinfo.ref1 = pmData[0];
            qrOfflinePayResultinfo.ref2 = pmData[1];
            qrOfflinePayResultinfo.amount = View.QRPaymentMethod.ToPayAmount.Value;
            try
            {
                QRRefundResponse _qrPaymentResponse = _billingService.QRPostOfflinePayment(qrOfflinePayResultinfo);
                if (_qrPaymentResponse.Status && _qrPaymentResponse.Message == "Success" && _qrPaymentResponse.Data.PayStatus)
                {
                    //View.QRPaymentInfo = _qrPaymentInfo;
                    if (View.QRPaymentInfo == null)
                        View.QRPaymentInfo = qrOfflinePayResultinfo;

                    View.QRPaymentInfo.status = _qrPaymentResponse.Message;
                }
                else
                {
                    View.QRPaymentCheckStatusResult = _qrPaymentResponse.Message;
                }

            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
            }


        }


    }
}
