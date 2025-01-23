using System.Collections.Generic;
using PEA.BPM.Architecture.ArchitectureBS;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.BillPrintingModule.BS;
using PEA.BPM.BillPrintingModule.Interface.BusinessEntities;
using PEA.BPM.WebService;
using WCFExtras.Soap;


namespace BPMService.BLAN
{

    public class BillPrintingWCFService : IBillPrintingWCFService
    {
        private BillPrintingBS _bs;
        private AuthInfo authInfo = SoapHeaderHelper<AuthInfo>.GetInputHeader("AuthInfoValue");

        public BillPrintingWCFService()
        {
            _bs = new BillPrintingBS();
        }

        public List<Bills> PrintBlueBill_NoCompress(BillPrintingConditionParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "BillPrintingWCFService", "PrintBlueBill_NoCompress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return new List<Bills>(_bs.PrintBlueBill(param));
                });
        }

        public CompressData PrintBlueBill(BillPrintingConditionParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "BillPrintingWCFService", "PrintBlueBill",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<Bills>>(_bs.PrintBlueBill(param));
                });
        }

        public CompressData PrintGreenBill(BillPrintingConditionParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "BillPrintingWCFService", "PrintGreenBill",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<Bills>>(_bs.PrintGreenBill(param));
                });
        }

        public List<Bills> PrintA4Bill_NoCompress(BillPrintingConditionParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "BillPrintingWCFService", "PrintA4Bill_NoCompress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return new List<Bills>(_bs.PrintA4Bill(param));
                });
        }

        public CompressData PrintA4Bill(BillPrintingConditionParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "BillPrintingWCFService", "PrintA4Bill",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<Bills>>(_bs.PrintA4Bill(param));
                });
        }

        public CompressData ReprintBlueBill(BillPrintingConditionParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "BillPrintingWCFService", "ReprintBlueBill",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<Bills>>(_bs.ReprintBlueBill(param));
                });
        }

        public CompressData ReprintGreenBill(BillPrintingConditionParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "BillPrintingWCFService", "ReprintGreenBill",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<Bills>>(_bs.ReprintGreenBill(param));
                });
        }

        public CompressData PrintGroupInvoiceA4Bill(BillPrintingConditionParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "BillPrintingWCFService", "PrintGroupInvoiceA4Bill",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<Bills>>(_bs.PrintGroupInvoiceA4Bill(param));
                });
        }

        public CompressData ReprintGroupInvoiceA4Bill(BillPrintingConditionParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "BillPrintingWCFService", "ReprintGroupInvoiceA4Bill",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<Bills>>(_bs.ReprintGroupInvoiceA4Bill(param));
                });
        }

        public CompressData PrintGreenBillByBank(BillPrintingConditionParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "BillPrintingWCFService", "PrintGreenBillByBank",
                () =>
                {
                    //WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<Bills>>(_bs.PrintGreenBillByBank(param));
                });
        }

        public CompressData ReprintGreenBillByBank(BillPrintingConditionParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "BillPrintingWCFService", "ReprintGreenBillByBank",
                () =>
                {
                    //WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<Bills>>(_bs.ReprintGreenBillByBank(param));
                });
        }

        public CompressData PrintBlueBillByBank(BillPrintingConditionParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "BillPrintingWCFService", "PrintBlueBillByBank",
                () =>
                {
                    //WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<Bills>>(_bs.PrintBlueBillByBank(param));
                });
        }

        public CompressData ReprintBlueBillByBank(BillPrintingConditionParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "BillPrintingWCFService", "ReprintBlueBillByBank",
                () =>
                {
                    //WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<Bills>>(_bs.ReprintBlueBillByBank(param));
                });
        }

        public List<PrintableId> CheckExistRecord_NoCompress(BillPrintingConditionParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "BillPrintingWCFService", "CheckExistRecord_NoCompress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return new List<PrintableId>(_bs.CheckExistRecord(param));
                });
        }

        public CompressData CheckExistRecord(BillPrintingConditionParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "BillPrintingWCFService", "CheckExistRecord",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<PrintableId>>(_bs.CheckExistRecord(param));
                });
        }

        public CompressData CheckExistReprintRecord(BillPrintingConditionParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "BillPrintingWCFService", "CheckExistReprintRecord",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<PrintableId>>(_bs.CheckExistReprintRecord(param));
                });
        }

        public CompressData CheckExistGroupInvoiceRecord(GroupInvoiceParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "BillPrintingWCFService", "CheckExistGroupInvoiceRecord",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<PrintableId>>(_bs.CheckExistGroupInvoiceRecord(param));
                });
        }

        public CompressData CheckExistReprintGroupInvoiceRecord(GroupInvoiceParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "BillPrintingWCFService", "CheckExistReprintGroupInvoiceRecord",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<PrintableId>>(_bs.CheckExistReprintGroupInvoiceRecord(param));
                });
        }

        public CompressData CheckExistByBank(GreenBillParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "BillPrintingWCFService", "CheckExistByBank",
                () =>
                {
                    //WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<PrintableIdByBank>>(_bs.CheckExistByBank(param));
                });
        }

        public CompressData CheckExistReprintByBank(GreenBillReprintParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "BillPrintingWCFService", "CheckExistReprintByBank",
                () =>
                {
                    //WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<PrintableIdByBank>>(_bs.CheckExistReprintByBank(param));
                });
        }

        public CompressData CheckExistGreenReceipt(BillPrintingConditionParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "BillPrintingWCFService", "CheckExistGreenReceipt",
                () =>
                {
                    //WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<PrintableId>>(_bs.CheckExistGreenReceipt(param));
                });
        }

        public CompressData CheckExistReprintGreenReceipt(BillPrintingConditionParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "BillPrintingWCFService", "CheckExistReprintGreenReceipt",
                () =>
                {
                    //WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<PrintableId>>(_bs.CheckExistReprintGreenReceipt(param));
                });
        }

        public CompressData PrintGreenReceipt(BillPrintingConditionParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "BillPrintingWCFService", "PrintGreenReceipt",
                () =>
                {
                    //WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<Bills>>(_bs.PrintGreenReceipt(param));
                });
        }

        public CompressData ReprintGreenReceipt(BillPrintingConditionParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "BillPrintingWCFService", "ReprintGreenReceipt",
                () =>
                {
                    //WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<Bills>>(_bs.ReprintGreenReceipt(param));
                });
        }

        public CompressData PrintSpotBill(BillPrintingConditionParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "BillPrintingWCFService", "PrintSpotBill",
                () =>
                {
                    //WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<Bills>>(_bs.PrintSpotBill(param));
                });
        }

        public CompressData ReprintSpotBill(BillPrintingConditionParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "BillPrintingWCFService", "ReprintSpotBill",
                () =>
                {
                    //WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<Bills>>(_bs.ReprintSpotBill(param));
                });
        }

    }
}
