using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.CashManagementModule.Interface.BusinessEntities;
using System.Data.Common;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;

namespace PEA.BPM.CashManagementModule.Interface.Services
{
    public interface ICashManagementServices
    {
        //used in module controller
        List<CashierWorkStatusInfo> IsOpenedWork(OpenWorkParam param);
        bool IsSystemInitial(string branchId, string workId);

        //used in openWork and systemInitial
        string OpenWork(OpenWorkParam param);
        OpenWorkInfo LoadOpeningBalance(string cashierId, string flowType);
        string GetWorkPosId(string workId);

        //used in moneyCheckIn
        void CheckInMoney(MoneyCheckInInfo param);
        bool ExistSAPRefNo(string sapRefNo, string workId);
        List<string> LoadSAPReference(string workId);

        //used in cancelMoneyCheckIn
        int CancelMoneyCheckedIn(CancelMoneyCheckedInInfo param);
        List<PaymentMethodInfo> LoadMoneyCheckedIn(string SAPRefNo, string workId);

        //System Initial
        //void SaveSystemInitial(MoneyCheckInInfo param);
        List<PaymentMethodInfo> LoadSystemInitial(string workId);

        //Adjust opening balance
        void SaveAdjustOpenBalance(MoneyCheckInInfo param);
        List<PaymentMethodInfo> LoadAdjustOpenBalance(string workId);

        //cancel bank delivery
        List<BankDeliveryInfo> ListBankDelivery(string workId);
        void CancelBankDelivery(DbTransaction trans, BankDeliveryInfo blInfo);

        //force closeWork
        List<WorkInfo> ListAllOpenWork(string branchId);
        void ForceCloseWork(DbTransaction trans, WorkInfo workInfo);

        void SaveStartOpenBalance(MoneyCheckInInfo param); 

        //void TriggerExport(string branchId, string modifiedBy);
        //Transfer Management
        List<CashierInfo> ListCashier(string keyword, string branchId);
        List<GLBankInfo> ListGLBank(string businessPlace);
        List<GLBankAccountInfo> ListGLBankAccount(string businessPlace, string bankId);
        TrayMoneyInfo GetMoneyInTray(string workId);
        CashierMoneyFlowInfo Transfer(DbTransaction trans, MoneyTransferInfo transferMoney);
        ReportBankPayInDetailInfo GetBankPayInDetailForReport(CashierMoneyFlowInfo flowInfo);

        //used in TransferResponse
        List<CashierMoneyTransferInfo> LoadTransferedRequestItem(string cashierId);
        string ResponseTransferedItems(List<String> transferId, string workId, string status, string posId, string branchId, string modifiedBy);

        //used in TranserferStatus
        List<CashierMoneyTransferInfo> LoadTransferStatusItem(string cashierId);
        void CancelTransferItem(List<String> transferId, string modifiedBy);

        //close work
        CloseWorkSummaryInfo GetCloseWorkFlowItem(string workId);  //R 1.12
        void CloseWork(CloseWorkSubmitInfo submitInfo);
        ReportWorkFlowSummary GetWorkFlowReport(string workId);
        string IsAllWorkClosed(string workId, string branchId);
        void SetBaseline(DbTransaction trans,string workId, string branchId);
        List<BaselineInfo> GetBaseline(string branchId, DateTime baselineDt);
        List<CashierInfo> GetOpenWorkCashierOfBranch(string branchId);

        CashierWorkStatus GetCashierWorkStatus(string workId);

        //historical report
        //List<ReportAvailableInfo> GetWorkBetweenDate(ReportParam param, string output);
        //List<ReportAvailableInfo> GetPayInOfDate(ReportParam param);
        //List<ReportAvailableInfo> GetCloseWorkOfDate(ReportParam param);
        List<ReportDailyRemainInfo> GetHistDailyRemainReport(ReportParam param);
        ReportBankPayInDetailInfo GetHistBankPayInDetailReport(ReportParam param);
        ReportWorkFlowSummary GetHistWorkFlowReport(ReportParam param);
        CashierPosIdInfo LoadCashierPosId(string branchId);
        //List<ReportDailyPayInInfo> GetBankPayInDailyForReport(ReportParam param);
        //List<ReportCloseWorkSummary> GetCloseWorkSummaryReport(ReportParam param);
        List<ChequeInfo> GetChequeDailyRemainReport(ReportParam param);

    }
}
