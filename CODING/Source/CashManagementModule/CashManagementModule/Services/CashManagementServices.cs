using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI;
using System.Configuration;
using PEA.BPM.CashManagementModule.Interface.Services;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.CashManagementModule.SG;
using PEA.BPM.CashManagementModule.Interface.BusinessEntities;
using System.Data.Common;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;

namespace PEA.BPM.CashManagementModule.Services
{
    [Service(typeof(ICashManagementServices))]
    public class CashManagementServices : ICashManagementServices, ICashReportServices
    {
        public CashManagementServices()
        {
        }

        #region Service Factory
        public ICashManagementServices GetService()
        {
            return GetService(false);
        }

        public ICashManagementServices GetService(bool serverService)
        {
            try
            {
                if (serverService || Session.Branch.OnlineConnection)
                {
                    return new CashManagementSG(true);
                }
                else
                {
                    return new CashManagementSG(false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ICashReportServices GetReportService()
        {
            return GetReportService(true);
        }

        public ICashReportServices GetReportService(bool serverService)
        {
            try
            {
                if (serverService || Session.Branch.OnlineConnection)
                {
                    return new ReportSG(true);
                }
                else
                {
                    return new ReportSG(false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region ICashManagementServices Members

        public List<CashierWorkStatusInfo> IsOpenedWork(OpenWorkParam param)
        {
            ICashManagementServices bs = GetService();
            return bs.IsOpenedWork(param);
        }

        public bool IsSystemInitial(string branchId, string workId)
        {
            ICashManagementServices bs = GetService();
            return bs.IsSystemInitial(branchId, workId);
        }

        public string OpenWork(OpenWorkParam param)
        {
            ICashManagementServices bs = GetService();
            return bs.OpenWork(param);
        }


        public OpenWorkInfo LoadOpeningBalance(string cashierId, string flowType)
        {
            ICashManagementServices bs = GetService();
            return bs.LoadOpeningBalance(cashierId, flowType);
        }


        public List<CashierInfo> ListCashier(string keyword, string branchId)
        {
            ICashManagementServices bs = GetService();
            return bs.ListCashier(keyword, branchId);
        }

        public List<GLBankInfo> ListGLBank(string businessPlace)
        {
            ICashManagementServices bs = GetService();
            return bs.ListGLBank(businessPlace);
        }

        public List<GLBankAccountInfo> ListGLBankAccount(string businessPlace, string bankId)
        {
            ICashManagementServices bs = GetService();
            return bs.ListGLBankAccount(businessPlace, bankId);
        }

        public TrayMoneyInfo GetMoneyInTray(string workId)
        {
            ICashManagementServices bs = GetService();
            return bs.GetMoneyInTray(workId);
        }

        public List<CashierMoneyTransferInfo> LoadTransferedRequestItem(string cashierId)
        {
            ICashManagementServices bs = GetService();
            return bs.LoadTransferedRequestItem(cashierId);
        }

        public List<CashierMoneyTransferInfo> LoadTransferStatusItem(string cashierId)
        {
            ICashManagementServices bs = GetService();
            return bs.LoadTransferStatusItem(cashierId);
        }

        public void CancelTransferItem(List<String> list, string modifiedBy)
        {
            ICashManagementServices bs = GetService();
            bs.CancelTransferItem(list, modifiedBy);
        }

        public string ResponseTransferedItems(List<String> transferId, string workId, string status, string posId, string branchId, string modifiedBy)
        {
            ICashManagementServices bs = GetService();
            return bs.ResponseTransferedItems(transferId, workId, status, posId, branchId, modifiedBy);
        }

        public CashierMoneyFlowInfo Transfer(DbTransaction trans, MoneyTransferInfo transferMoney)
        {
            ICashManagementServices bs = GetService();
            return bs.Transfer(trans, transferMoney);
        }

        public ReportBankPayInDetailInfo GetBankPayInDetailForReport(CashierMoneyFlowInfo flowInfo)
        {
            ICashManagementServices bs = GetService();
            return bs.GetBankPayInDetailForReport(flowInfo);
        }

        public CloseWorkSummaryInfo GetCloseWorkFlowItem(string workId)
        {
            ICashManagementServices bs = GetService();
            return bs.GetCloseWorkFlowItem(workId);
        }

        public void CloseWork(CloseWorkSubmitInfo submitInfo)
        {
            ICashManagementServices bs = GetService();
            bs.CloseWork(submitInfo);
        }

        public ReportWorkFlowSummary GetWorkFlowReport(string workId)
        {
            ICashManagementServices bs = GetService();
            return bs.GetWorkFlowReport(workId);
        }

        public ReportWorkFlowSummary GetWorkFlowDelayedReport(string workId)
        {
            ICashReportServices bs = GetReportService();
            return bs.GetWorkFlowDelayedReport(workId);
        }

        public void CheckInMoney(MoneyCheckInInfo param)
        {
            ICashManagementServices bs = GetService();
            bs.CheckInMoney(param);
        }

        public int CancelMoneyCheckedIn(CancelMoneyCheckedInInfo param)
        {
            ICashManagementServices bs = GetService();
            return bs.CancelMoneyCheckedIn(param);            
        }

        public List<PaymentMethodInfo> LoadMoneyCheckedIn(string SAPRefNo, string workId)
        {
            ICashManagementServices bs = GetService();
            return bs.LoadMoneyCheckedIn(SAPRefNo, workId);
        }

        public List<ReportDailyRemainInfo> GetHistDailyRemainReport(ReportParam param)
        {
            ICashManagementServices bs = GetService();
            return bs.GetHistDailyRemainReport(param);
        }

        public ReportBankPayInDetailInfo GetHistBankPayInDetailReport(ReportParam param)
        {
            ICashManagementServices bs = GetService();
            return bs.GetHistBankPayInDetailReport(param);
        }

        public ReportWorkFlowSummary GetHistWorkFlowReport(ReportParam param)
        {
            ICashManagementServices bs = GetService();
            return bs.GetHistWorkFlowReport(param);
        }

        public bool ExistSAPRefNo(string sapRefNo, string workId)
        {
            ICashManagementServices bs = GetService();
            return bs.ExistSAPRefNo(sapRefNo, workId);
        }

        public List<ReportAvailableInfo> GetWorkBetweenDate(ReportParam param, string output)
        {
            ICashReportServices bs = GetReportService();
            return bs.GetWorkBetweenDate(param, output);
        }

        public List<BankDeliveryInfo> ListBankDelivery(string workId)
        {
            ICashManagementServices bs = GetService();
            return bs.ListBankDelivery(workId);
        }

        public void CancelBankDelivery(DbTransaction trans, BankDeliveryInfo blInfo)
        {
            ICashManagementServices bs = GetService();
            bs.CancelBankDelivery(trans, blInfo);
        }

        public List<WorkInfo> ListAllOpenWork(string branchId)
        {
            ICashManagementServices bs = GetService();
            return bs.ListAllOpenWork(branchId);
        }

        public void ForceCloseWork(DbTransaction trans, WorkInfo workInfo)
        {
            ICashManagementServices bs = GetService();
            bs.ForceCloseWork(trans, workInfo);
        }

        public CashierPosIdInfo LoadCashierPosId(string branchId)
        {
            ICashManagementServices bs = GetService();
            return bs.LoadCashierPosId(branchId);
        }

        public string IsAllWorkClosed(string workId, string branchId)
        {
            ICashManagementServices bs = GetService();
            return bs.IsAllWorkClosed(workId, branchId);
        }

        public void SetBaseline(DbTransaction trans, string workId, string branchId)
        {
            ICashManagementServices bs = GetService();
            bs.SetBaseline(trans, workId, branchId);
        }

        public List<BaselineInfo> GetBaseline(string branchId, DateTime baselineDt)
        {
            ICashManagementServices bs = GetService();
            return bs.GetBaseline(branchId, baselineDt);
        }

        public string GetWorkPosId(string workId)
        {
            ICashManagementServices bs = GetService();
            return bs.GetWorkPosId(workId);
        }

        public List<string> LoadSAPReference(string workId)
        {
            ICashManagementServices bs = GetService();
            return bs.LoadSAPReference(workId);
        }

        public List<ReportDailyPayInInfo> GetBankPayInDailyForReport(ReportParam param)
        {
            ICashReportServices bs = GetReportService();
            return bs.GetBankPayInDailyForReport(param);
        }

        public List<ReportAvailableInfo> GetPayInOfDate(ReportParam param)
        {
            ICashReportServices bs = GetReportService();
            return bs.GetPayInOfDate(param);
        }

        public List<ReportAvailableInfo> GetCloseWorkOfDate(ReportParam param)
        {
            ICashReportServices bs = GetReportService();
            return bs.GetCloseWorkOfDate(param);
        }

        public List<ReportCloseWorkSummary> GetCloseWorkSummaryReport(ReportParam param)
        {
            ICashReportServices bs = GetReportService();
            return bs.GetCloseWorkSummaryReport(param);
        }

        public List<ChequeInfo> GetChequeDailyRemainReport(ReportParam param)
        {
            ICashManagementServices bs = GetService();
            return bs.GetChequeDailyRemainReport(param);
        }

        public List<CashierInfo> GetOpenWorkCashierOfBranch(string branchId)
        {
            ICashManagementServices bs = GetService();
            return bs.GetOpenWorkCashierOfBranch(branchId);
        }

        #endregion


        #region ICashManagementServices Members


        public void SaveStartOpenBalance(MoneyCheckInInfo param)
        {
            ICashManagementServices bs = GetService();
            bs.SaveStartOpenBalance(param);
        }

        public List<PaymentMethodInfo> LoadSystemInitial(string workId)
        {
            ICashManagementServices bs = GetService();
            return bs.LoadSystemInitial(workId);
        }

        public void SaveAdjustOpenBalance(MoneyCheckInInfo param)
        {
            ICashManagementServices bs = GetService();
            bs.SaveAdjustOpenBalance(param);
        }

        public List<PaymentMethodInfo> LoadAdjustOpenBalance(string workId)
        {
            ICashManagementServices bs = GetService();
            return bs.LoadAdjustOpenBalance(workId);
        }

        public CashierWorkStatus GetCashierWorkStatus(string workId)
        {
            ICashManagementServices bs = GetService();
            return bs.GetCashierWorkStatus(workId);
        }

        #endregion

    }
}
