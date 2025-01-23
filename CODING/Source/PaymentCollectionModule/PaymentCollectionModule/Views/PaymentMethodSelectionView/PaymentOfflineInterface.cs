using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using PEA.BPM.PaymentCollectionModule.Interface.Constants;
using System.Windows.Forms;
using PEA.BPM.Architecture.CommonUtilities;
using System.Xml.Serialization;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using PEA.BPM.PaymentCollectionModule.Views.SlipPrintingView;
using PEA.BPM.PaymentCollectionModule.Interface.Services;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.Infrastructure.Interface;
using System.Runtime.Serialization;
using PEA.BPM.Architecture.ArchitectureInterface.Constants;
using PEA.BPM.PaymentCollectionModule.Services;
using System.Net;
using System.Globalization;
using PEA.BPM.CashManagementModule.Interface.BusinessEntities;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;
using System.Text.RegularExpressions;
using ProtoBuf;


namespace PEA.BPM.PaymentCollectionModule.Views.PaymentMethodSelectionView
{
    public class PaymentOfflineInterface
    {
        private IBillingService _billingService;

        public PaymentOfflineInterface(IBillingService billingService)
        {
            _billingService = billingService;
        }
      
        //Offline, Begin
        public bool OfflineFileWithOutWorkID()
        {
            bool ret = true;
            bool isSelfCashier = false;
            bool isMainCashier = false;

            string msgCashierInfo = "";

            //เช็ค Cashier หลัก
            if (PEA.BPM.Architecture.ArchitectureTool.Security.Authorization.IsAuthorized(PEA.BPM.CashManagementModule.Interface.Constants.SecurityNames.ForceCloseWork, false))
            {
                isMainCashier = true;
            }


            List<OfflinePayment> offlinePayments = new List<OfflinePayment>();

            offlinePayments = _billingService.GetOfflinePayment(Session.Branch.Id, Session.User.Id, Session.Work.Id);

            int i = 0;
            foreach (OfflinePayment offlinePayment in offlinePayments)
            {
                i = i + 1;

                if (isMainCashier == true)
                {
                    if (offlinePayment.CashierId != null && offlinePayment.CashierName.ToString() != "")
                        msgCashierInfo = msgCashierInfo + "\n" + (i).ToString() + ". " + offlinePayment.CashierName + " จำนวนเงิน " + offlinePayment.GAmount.Value.ToString("#,###.00") + " บาท";

                }
                else
                {
                    if (offlinePayment.CashierId == Session.User.Id)
                    {
                        //msgCashierInfo = "" + msgCashierInfo + " (ของตนเอง)";
                        msgCashierInfo = msgCashierInfo + "\n" + offlinePayment.CashierName + " จำนวนเงิน " + offlinePayment.GAmount.Value.ToString("#,###.00") + " บาท";
                        isSelfCashier = true;
                    }
                }
            }


            if (msgCashierInfo != "")//เช็คว่ามีข้อมูล Offline นอกกะ
            {
                //เช็ค Cashier หลัก
                if (isMainCashier == true)
                {
                    
                    foreach (OfflinePayment offlinePayment in offlinePayments)
                    {
                        if (offlinePayment.CashierId != null && offlinePayment.CashierName.ToString() != "")
                            _billingService.UpdateOfflinePayment(Session.Branch.Id, offlinePayment.CashierId, Session.Terminal.Id, null);
                    }

                    MessageBox.Show(
                                    msgCashierInfo +
                                    "\n" +
                                    "\nระบบได้โอนเงินของทุกคนเข้ากะให้เรียบร้อยแล้ว"
                                    , "สรุปจำนวนเงินที่รับชำระนอกกะ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {

                    if (isSelfCashier == true) //มีข้อมูลนอกกะของตัวเอง 
                    {
                        _billingService.UpdateOfflinePayment(Session.Branch.Id, Session.User.Id, Session.Terminal.Id, Session.Work.Id);

                        MessageBox.Show(
                                        msgCashierInfo +
                                        "\n" +
                                        "\nระบบได้โอนเงินเข้ากะให้เรียบร้อยแล้ว"
                                        , "สรุปจำนวนเงินที่รับชำระนอกกะ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else//ไม่มีข้อมูลนอกกะของตนเอง 
                    {
                        //not show messagebox

                        //MessageBox.Show(
                        //                msgCashierInfo +
                        //                "\n" +
                        //                "\nแจ้งผู้ที่เกี่ยวข้อง เพื่อโอนเงินเข้ากะ"
                        //                , "สรุปจำนวนเงินที่รับชำระนอกกะ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }

            }
            
            return ret;
        }
        //Offline, End
    }
}