using System;
using System.Collections.Generic;
using PEA.BPM.Architecture.ArchitectureBS;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.CashManagementModule.BS;
using PEA.BPM.CashManagementModule.Interface.BusinessEntities;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;
using PEA.BPM.WebService;
using WCFExtras.Soap;


namespace BPMService.Cashier
{

    public class CashierWCFService : ICashierWCFService
    {
        private CashManagementBS _bs;
        private CashReportBS _repBs;
        private AuthInfo authInfo = SoapHeaderHelper<AuthInfo>.GetInputHeader("AuthInfoValue");

        public CashierWCFService()
        {
            _bs = new CashManagementBS();
            _repBs = new CashReportBS();
        }

        public CompressData IsOpenedWork(OpenWorkParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Cash, authInfo, "CashierWCFService", "IsOpenedWork",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<CashierWorkStatusInfo>>(_bs.IsOpenedWork(param));
                });
        }

        public bool IsSystemInitial(string branchId, string workId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Cash, authInfo, "CashierWCFService", "IsSystemInitial",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _bs.IsSystemInitial(branchId, workId);
                });
        }

        public string OpenWork(OpenWorkParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Cash, authInfo, "CashierWCFService", "OpenWork",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _bs.OpenWork(param);
                });
        }

        public CompressData LoadOpeningBalance(string cashierId, string flowType)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Cash, authInfo, "CashierWCFService", "LoadOpeningBalance",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<OpenWorkInfo>(_bs.LoadOpeningBalance(cashierId, flowType));
                });
        }

        public CompressData ListCashier(string keyword, string branchId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Cash, authInfo, "CashierWCFService", "ListCashier",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<CashierInfo>>(_bs.ListCashier(keyword, branchId));
                });
        }

        public CompressData ListGLBank(string businessPlace)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Cash, authInfo, "CashierWCFService", "ListGLBank",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<GLBankInfo>>(_bs.ListGLBank(businessPlace));
                });
        }

        public CompressData ListGLBankAccount(string businessPlace, string bankId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Cash, authInfo, "CashierWCFService", "ListGLBankAccount",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<GLBankAccountInfo>>(_bs.ListGLBankAccount(businessPlace, bankId));
                });
        }

        public CompressData GetMoneyInTray(string workId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Cash, authInfo, "CashierWCFService", "GetMoneyInTray",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<TrayMoneyInfo>(_bs.GetMoneyInTray(workId));
                });
        }

        public CompressData Transfer(MoneyTransferInfo transferMoney)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Cash, authInfo, "CashierWCFService", "Transfer",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<CashierMoneyFlowInfo>(_bs.Transfer(null, transferMoney));
                });
        }

        public CompressData GetBankPayInDetailForReport(CashierMoneyFlowInfo flowInfo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Cash, authInfo, "CashierWCFService", "GetBankPayInDetailForReport",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<ReportBankPayInDetailInfo>(_bs.GetBankPayInDetailForReport(flowInfo));
                });
        }

        public CompressData LoadTransferedRequestItem(string cashierId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Cash, authInfo, "CashierWCFService", "LoadTransferedRequestItem",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<CashierMoneyTransferInfo>>(_bs.LoadTransferedRequestItem(cashierId));
                });
        }

        public string ResponseTransferedItems(CompressData transferId, string workId, string status, string posId, string branchId, string modifiedBy)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Cash, authInfo, "CashierWCFService", "ResponseTransferedItems",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _bs.ResponseTransferedItems(ServiceHelper.DecompressData<List<String>>(transferId), workId, status, posId, branchId, modifiedBy);
                });
        }

        public void CancelTransferItem(CompressData transferId, string modifiedBy)
        {
            ServiceInspector.Instance.AuditTrail(EErrorInModule.Cash, authInfo, "CashierWCFService", "CancelTransferItem",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    _bs.CancelTransferItem(ServiceHelper.DecompressData<List<String>>(transferId), modifiedBy);
                });
        }

        public CompressData LoadTransferStatusItem(string cashierId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Cash, authInfo, "CashierWCFService", "LoadTransferStatusItem",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<CashierMoneyTransferInfo>>(_bs.LoadTransferStatusItem(cashierId));
                });
        }

        public CompressData GetCloseWorkFlowItem(string workId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Cash, authInfo, "CashierWCFService", "GetCloseWorkFlowItem",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<CloseWorkSummaryInfo>(_bs.GetCloseWorkFlowItem(workId));
                });
        }

        public void CloseWork(CloseWorkSubmitInfo submitInfo)
        {
            ServiceInspector.Instance.AuditTrail(EErrorInModule.Cash, authInfo, "CashierWCFService", "CloseWork",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    _bs.CloseWork(submitInfo);
                });
        }

        public CompressData GetWorkFlowReport(string workId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Cash, authInfo, "CashierWCFService", "GetWorkFlowReport",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<ReportWorkFlowSummary>(_bs.GetWorkFlowReport(workId));
                });
        }

        public CompressData GetWorkFlowDelayedReport(string workId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Cash, authInfo, "CashierWCFService", "GetWorkFlowDelayedReport",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<ReportWorkFlowSummary>(_repBs.GetWorkFlowDelayedReport(workId));
                });
        }

        public void CheckInMoney(MoneyCheckInInfo param)
        {
            ServiceInspector.Instance.AuditTrail(EErrorInModule.Cash, authInfo, "CashierWCFService", "CheckInMoney",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    _bs.CheckInMoney(param);
                });
        }

        public CompressData LoadMoneyCheckedIn(string SAPRefNo, string workId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Cash, authInfo, "CashierWCFService", "LoadMoneyCheckedIn",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<PaymentMethodInfo>>(_bs.LoadMoneyCheckedIn(SAPRefNo, workId));
                });
        }

        public int CancelMoneyCheckedIn(CancelMoneyCheckedInInfo param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Cash, authInfo, "CashierWCFService", "CancelMoneyCheckedIn",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _bs.CancelMoneyCheckedIn(param);
                });
        }

        public bool ExistSAPRefNo(string sapRefNo, string workId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Cash, authInfo, "CashierWCFService", "ExistSAPRefNo",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _bs.ExistSAPRefNo(sapRefNo, workId);
                });
        }

        public CompressData GetHistDailyRemainReport(ReportParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Cash, authInfo, "CashierWCFService", "GetHistDailyRemainReport",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<ReportDailyRemainInfo>>(_bs.GetHistDailyRemainReport(param));
                });
        }

        public CompressData GetHistBankPayInDetailReport(ReportParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Cash, authInfo, "CashierWCFService", "GetHistBankPayInDetailReport",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<ReportBankPayInDetailInfo>(_bs.GetHistBankPayInDetailReport(param));
                });
        }

        public CompressData GetHistWorkFlowReport(ReportParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Cash, authInfo, "CashierWCFService", "GetHistWorkFlowReport",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<ReportWorkFlowSummary>(_bs.GetHistWorkFlowReport(param));
                });
        }

        public CompressData GetWorkBetweenDate(ReportParam param, string output)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Cash, authInfo, "CashierWCFService", "GetWorkBetweenDate",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<ReportAvailableInfo>>(_repBs.GetWorkBetweenDate(param, output));
                });
        }

        public CompressData ListBankDelivery(string workId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Cash, authInfo, "CashierWCFService", "ListBankDelivery",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<BankDeliveryInfo>>(_bs.ListBankDelivery(workId));
                });
        }

        public void CancelBankDelivery(BankDeliveryInfo blInfo)
        {
            ServiceInspector.Instance.AuditTrail(EErrorInModule.Cash, authInfo, "CashierWCFService", "CancelBankDelivery",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    _bs.CancelBankDelivery(null, blInfo);
                });
        }

        public CompressData ListAllOpenWork(string branchId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Cash, authInfo, "CashierWCFService", "ListAllOpenWork",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<WorkInfo>>(_bs.ListAllOpenWork(branchId));
                });
        }

        public void ForceCloseWork(WorkInfo workInfo)
        {
            ServiceInspector.Instance.AuditTrail(EErrorInModule.Cash, authInfo, "CashierWCFService", "ForceCloseWork",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    _bs.ForceCloseWork(null, workInfo);
                });
        }

        public CashierPosIdInfo LoadCashierPosId(string branchId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Cash, authInfo, "CashierWCFService", "LoadCashierPosId",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _bs.LoadCashierPosId(branchId);
                });
        }

        public string IsAllWorkClosed(string workId, string branchId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Cash, authInfo, "CashierWCFService", "IsAllWorkClosed",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _bs.IsAllWorkClosed(workId, branchId);
                });
        }

        public void SetBaseline(string workId, string branchId)
        {
            ServiceInspector.Instance.AuditTrail(EErrorInModule.Cash, authInfo, "CashierWCFService", "SetBaseline",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    _bs.SetBaseline(null, workId, branchId);
                });
        }

        public CompressData GetBaseline(string branchId, DateTime baselineDt)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Cash, authInfo, "CashierWCFService", "GetBaseline",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<BaselineInfo>>(_bs.GetBaseline(branchId, baselineDt));
                });
        }

        public string GetWorkPosId(string workId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Cash, authInfo, "CashierWCFService", "GetWorkPosId",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _bs.GetWorkPosId(workId);
                });
        }

        public List<string> LoadSAPReference(string workId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Cash, authInfo, "CashierWCFService", "LoadSAPReference",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _bs.LoadSAPReference(workId);
                });
        }

        public CompressData GetBankPayInDailyForReport(ReportParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Cash, authInfo, "CashierWCFService", "GetBankPayInDailyForReport",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<ReportDailyPayInInfo>>(_repBs.GetBankPayInDailyForReport(param));
                });
        }

        public CompressData GetCloseWorkSummaryReport(ReportParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Cash, authInfo, "CashierWCFService", "GetCloseWorkSummaryReport",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<ReportCloseWorkSummary>>(_repBs.GetCloseWorkSummaryReport(param));
                });
        }

        public CompressData GetChequeDailyRemainReport(ReportParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Cash, authInfo, "CashierWCFService", "GetChequeDailyRemainReport",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<ChequeInfo>>(_bs.GetChequeDailyRemainReport(param));
                });
        }

        public CompressData GetPayInOfDate(ReportParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Cash, authInfo, "CashierWCFService", "GetPayInOfDate",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<ReportAvailableInfo>>(_repBs.GetPayInOfDate(param));
                });
        }

        public CompressData GetCloseWorkOfDate(ReportParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Cash, authInfo, "CashierWCFService", "GetCloseWorkOfDate",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<ReportAvailableInfo>>(_repBs.GetCloseWorkOfDate(param));
                });
        }

        public void SaveStartOpenBalance(MoneyCheckInInfo param)
        {
            ServiceInspector.Instance.AuditTrail(EErrorInModule.Cash, authInfo, "CashierWCFService", "SaveStartOpenBalance",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    _bs.SaveStartOpenBalance(param);
                });
        }

        public CompressData LoadSystemInitial(string workId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Cash, authInfo, "CashierWCFService", "LoadSystemInitial",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<PaymentMethodInfo>>(_bs.LoadSystemInitial(workId));
                });
        }

        public void SaveAdjustOpenBalance(MoneyCheckInInfo param)
        {
            ServiceInspector.Instance.AuditTrail(EErrorInModule.Cash, authInfo, "CashierWCFService", "SaveAdjustOpenBalance",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    _bs.SaveAdjustOpenBalance(param);
                });
        }

        public CompressData LoadAdjustOpenBalance(string workId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Cash, authInfo, "CashierWCFService", "LoadAdjustOpenBalance",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<PaymentMethodInfo>>(_bs.LoadAdjustOpenBalance(workId));
                });
        }

        public CompressData GetOpenWorkCashierOfBranch(string branchId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Cash, authInfo, "CashierWCFService", "GetOpenWorkCashierOfBranch",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<CashierInfo>>(_bs.GetOpenWorkCashierOfBranch(branchId));
                });
        }

        public CashierWorkStatus GetCashierWorkStatus(string workId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Cash, authInfo, "CashierWCFService", "GetCashierWorkStatus",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _bs.GetCashierWorkStatus(workId);
                });
        }
    }
}
