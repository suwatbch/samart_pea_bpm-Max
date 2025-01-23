using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.BillPrintingModule.Interface.Services;
using PEA.BPM.BillPrintingModule.Interface.BusinessEntities;
using System.Configuration;
using PEA.BPM.BillPrintingModule.SG;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.BillPrintingModule.Services
{
    [Service(typeof(IBillPrintingServices))]
    public class BillPrintingServices : IBillPrintingServices
    {
        public BillPrintingServices()
        {
        }

        #region Service Factory
        public IBillPrintingServices GetService()
        {
            return GetService(false);
        }

        public IBillPrintingServices GetService(bool serverService)
        {
            if (serverService || Session.Branch.OnlineConnection)
            {
                return new BillPrintingSG(true);
            }
            else
            {
                return new BillPrintingSG(false);
            }
        }

        #endregion

        #region IBillPrintingService Members

        public List<Bills> PrintA4Bill(BillPrintingConditionParam param)
        {
            IBillPrintingServices bs = GetService();
            return bs.PrintA4Bill(param);
        }

        public List<Bills> PrintBlueBill(BillPrintingConditionParam param)
        {
            IBillPrintingServices bs = GetService();
            return bs.PrintBlueBill(param);
        }

        public List<Bills> PrintGreenBill(BillPrintingConditionParam param)
        {
            IBillPrintingServices bs = GetService();
            return bs.PrintGreenBill(param);
        }

        public List<Bills> ReprintBlueBill(BillPrintingConditionParam param)
        {
            IBillPrintingServices bs = GetService();
            return bs.ReprintBlueBill(param);
        }

        public List<Bills> ReprintGreenBill(BillPrintingConditionParam param)
        {
            IBillPrintingServices bs = GetService();
            return bs.ReprintGreenBill(param);
        }

        public List<Bills> PrintGroupInvoiceA4Bill(BillPrintingConditionParam param)
        {
            IBillPrintingServices bs = GetService();
            return bs.PrintGroupInvoiceA4Bill(param);
        }

        public List<Bills> ReprintGroupInvoiceA4Bill(BillPrintingConditionParam param)
        {
            IBillPrintingServices bs = GetService();
            return bs.ReprintGroupInvoiceA4Bill(param);
        }

        public List<PrintableId> CheckExistRecord(BillPrintingConditionParam param)
        {
            IBillPrintingServices bs = GetService();
            return bs.CheckExistRecord(param);
        }
        
        public List<PrintableId> CheckExistReprintRecord(BillPrintingConditionParam param)
        {
            IBillPrintingServices bs = GetService();
            return bs.CheckExistReprintRecord(param);
        }

        public List<PrintableId> CheckExistGroupInvoiceRecord(GroupInvoiceParam param)
        {
            IBillPrintingServices bs = GetService();
            return bs.CheckExistGroupInvoiceRecord(param);
        }

        public List<PrintableId> CheckExistReprintGroupInvoiceRecord(GroupInvoiceParam param)
        {
            IBillPrintingServices bs = GetService();
            return bs.CheckExistReprintGroupInvoiceRecord(param);
        }        

        public List<PrintableIdByBank> CheckExistByBank(GreenBillParam param)
        {
            IBillPrintingServices bs = GetService();
            return bs.CheckExistByBank(param);
        }        

        public List<PrintableIdByBank> CheckExistReprintByBank(GreenBillReprintParam param)
        {
            IBillPrintingServices bs = GetService();
            return bs.CheckExistReprintByBank(param);
        }

        public List<PrintableId> CheckExistGreenReceipt(BillPrintingConditionParam param)
        {
            IBillPrintingServices bs = GetService();
            return bs.CheckExistGreenReceipt(param);
        }

        public List<PrintableId> CheckExistReprintGreenReceipt(BillPrintingConditionParam param)
        {
            IBillPrintingServices bs = GetService();
            return bs.CheckExistReprintGreenReceipt(param);
        }

        private bool IsAllNotFound(List<PrintableId> checkRecs)
        {
            bool ret = true;
            foreach (PrintableId pId in checkRecs)
            {
                if (pId.PrintingStatus != 2)
                    return false;
            }

            return ret;
        }

        public List<Bills> PrintGreenBillByBank(BillPrintingConditionParam param)
        {
            IBillPrintingServices bs = GetService();
            return bs.PrintGreenBillByBank(param);
        }

        public List<Bills> PrintBlueBillByBank(BillPrintingConditionParam param)
        {
            IBillPrintingServices bs = GetService();
            return bs.PrintBlueBillByBank(param);
        }

        public List<Bills> ReprintGreenBillByBank(BillPrintingConditionParam param)
        {
            IBillPrintingServices bs = GetService();
            return bs.ReprintGreenBillByBank(param);
        }      

        public List<Bills> ReprintBlueBillByBank(BillPrintingConditionParam param)
        {
            IBillPrintingServices bs = GetService();
            return bs.ReprintBlueBillByBank(param);
        }

        public List<Bills> PrintGreenReceipt(BillPrintingConditionParam param)
        {
            IBillPrintingServices bs = GetService();
            return bs.PrintGreenReceipt(param);
        }

        public List<Bills> ReprintGreenReceipt(BillPrintingConditionParam param)
        {
            IBillPrintingServices bs = GetService();
            return bs.ReprintGreenReceipt(param);
        }

        public List<Bills> PrintSpotBill(BillPrintingConditionParam param)
        {
            IBillPrintingServices bs = GetService();
            return bs.PrintSpotBill(param);
        }

        public List<Bills> ReprintSpotBill(BillPrintingConditionParam param)
        {
            IBillPrintingServices bs = GetService();
            return bs.ReprintSpotBill(param);
        }

        #endregion
    }
}
