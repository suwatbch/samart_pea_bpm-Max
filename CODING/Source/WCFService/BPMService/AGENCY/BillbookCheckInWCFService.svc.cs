using System.Collections.Generic;
using PEA.BPM.AgencyManagementModule.BS;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.ArchitectureBS;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.WebService;
using WCFExtras.Soap;


namespace BPMService.AGENCY
{

    public class BillbookCheckInWCFService : IBillbookCheckInWCFService
    {
        private BillbookCheckInService _billbookCheckInService;
        private AuthInfo authInfo = SoapHeaderHelper<AuthInfo>.GetInputHeader("AuthInfoValue");

        public BillbookCheckInWCFService()
        {
            _billbookCheckInService = new BillbookCheckInService();
        }

        public BillBookCheckInInfo GetBillBookCheckInInfo(string billBookId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "BillbookCheckInWCFService", "GetBillBookCheckInInfo",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _billbookCheckInService.GetBillBookCheckInInfo(billBookId);
                });
        }

        public CompressData GetBillBookCheckInInfo_Compress(string billBookId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "BillbookCheckInWCFService", "GetBillBookCheckInInfo_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<BillBookCheckInInfo>(_billbookCheckInService.GetBillBookCheckInInfo(billBookId));
                });
        }

        public BillBookCheckInInfo GetGroupInvoiceCheckInInfo(string groupIvId, string branchId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "BillbookCheckInWCFService", "GetGroupInvoiceCheckInInfo",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _billbookCheckInService.GetGroupInvoiceCheckInInfo(groupIvId, branchId);
                });
        }

        public CompressData GetGroupInvoiceCheckInInfo_Compress(string groupIvId, string branchId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "BillbookCheckInWCFService", "GetGroupInvoiceCheckInInfo_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<BillBookCheckInInfo>(_billbookCheckInService.GetGroupInvoiceCheckInInfo(groupIvId, branchId));
                });
        }

        public BillBookCheckInInfo GetBillBookCheckInCancel(string billBookId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "BillbookCheckInWCFService", "GetBillBookCheckInCancel",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _billbookCheckInService.GetBillBookCheckInCancel(billBookId);
                });
        }

        public CompressData GetBillBookCheckInCancel_Compress(string billBookId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "BillbookCheckInWCFService", "GetBillBookCheckInCancel_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<BillBookCheckInInfo>(_billbookCheckInService.GetBillBookCheckInCancel(billBookId));
                });
        }

        public bool CreateBillBookCheckIn(CompressData biilBookCheckIn, string branchId, string terminalId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "BillbookCheckInWCFService", "CreateBillBookCheckIn",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _billbookCheckInService.CreateBillBookCheckIn(ServiceHelper.DecompressData<BillBookCheckInInfo>(biilBookCheckIn), branchId, terminalId);
                });
        }

        public bool CancelBillBookCheckIn(CompressData biilBookCheckIn)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "BillbookCheckInWCFService", "CancelBillBookCheckIn",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _billbookCheckInService.CancelBillBookCheckIn(ServiceHelper.DecompressData<BillBookCheckInInfo>(biilBookCheckIn));
                });
        }

        public bool CheckIsFullyPaid(CompressData biilBookCheckIn)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "BillbookCheckInWCFService", "CheckIsFullyPaid",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _billbookCheckInService.CheckIsFullyPaid(ServiceHelper.DecompressData<BillBookCheckInInfo>(biilBookCheckIn));
                });
        }

        public bool CheckIsSubmitGroupSameDay(CompressData biilBookCheckIn)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "BillbookCheckInWCFService", "CheckIsSubmitGroupSameDay",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _billbookCheckInService.CheckIsSubmitGroupSameDay(ServiceHelper.DecompressData<BillBookCheckInInfo>(biilBookCheckIn));
                });
        }

        public BillBookCheckInInfo GetBillBookCheckInHistory(string billBookId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "BillbookCheckInWCFService", "GetBillBookCheckInHistory",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _billbookCheckInService.GetBillBookCheckInHistory(billBookId);
                });
        }

        public CompressData GetBillBookCheckInHistory_Compress(string billBookId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "BillbookCheckInWCFService", "GetBillBookCheckInHistory_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<BillBookCheckInInfo>(_billbookCheckInService.GetBillBookCheckInHistory(billBookId));
                });
        }

        public BillBookCheckInInfo GetGroupInvoiceCheckInHistory(string groupIvId, string branchId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "BillbookCheckInWCFService", "GetGroupInvoiceCheckInHistory",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _billbookCheckInService.GetGroupInvoiceCheckInHistory(groupIvId, branchId);
                });
        }

        public CompressData GetGroupInvoiceCheckInHistory_Compress(string groupIvId, string branchId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "BillbookCheckInWCFService", "GetGroupInvoiceCheckInHistory_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<BillBookCheckInInfo>(_billbookCheckInService.GetGroupInvoiceCheckInHistory(groupIvId, branchId));
                });
        }

        public List<ChequeInfo> GetChequeList(string billBookId, string invId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "BillbookCheckInWCFService", "GetChequeList",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _billbookCheckInService.GetChequeList(billBookId, invId);
                });
        }

        public CompressData GetChequeList_Compress(string billBookId, string invId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "BillbookCheckInWCFService", "GetChequeList_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<ChequeInfo>>(_billbookCheckInService.GetChequeList(billBookId, invId));
                });
        }

        public decimal GetAdvPaidFromPOS(string billBookId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "BillbookCheckInWCFService", "GetAdvPaidFromPOS",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    decimal retVal = _billbookCheckInService.GetAdvPaidFromPOS(billBookId);
                    return retVal;
                });
        }

        public CompressData GetBookCheckInHistory_Compress(string bookId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "BillbookCheckInWCFService", "GetBookCheckInHistory_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<BillBookCheckInInfo>(_billbookCheckInService.GetBookCheckInHistory(bookId));
                });
        }

        public void BillBookSaveState(CompressData billbookCheckIn, string modifiedBy)
        {
            ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "BillbookCheckInWCFService", "BillBookSaveState",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    _billbookCheckInService.BillBookSaveState(ServiceHelper.DecompressData<BillBookCheckInInfo>(billbookCheckIn), modifiedBy);
                });
        }

    }
}
