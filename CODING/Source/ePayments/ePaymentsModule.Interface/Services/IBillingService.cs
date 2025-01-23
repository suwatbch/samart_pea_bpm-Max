using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities;
using System.Data.Common;

namespace PEA.BPM.ePaymentsModule.Interface.Services
{
    public interface IBillingService
    {
        //int PayInvoice(DbTransaction trans, PaymentInfo payment);

        string UpdateBillMarkFlagService(string caId, string invoiceNo, string modifiedBy, string posetServerId);
        void DelBillMarkFlagService();

        List<ClearifyInfo> SearchDebtService(SearchDebtParam searchDebtParam);
        List<EpayUploadItem> GetDebtComparableService(string caInvoceNo);
        List<Company> SearchCompany(Company compParm);
        List<BPMClearifyInfo> SearchBPMDebtClearify(SearchDebtParam searchDebtParam);

        void InsertEPayUploadService(List<EPaymentUploadFile> ePayFileList);
        bool IsUploadFileNameExist(string fileName);
        List<AgentPayment> GetAgentPaymentService(AgentPayment agentPayment);
        void InsertAgentPaymentService(List<AgentPayment> agentPayList);

        bool SaveClearify(DbTransaction trans, SaveClearifyInfo saveClearifyItem);

        List<CancelPayment> SearchAgentPayment(CancelPayment payment);
        void InsertCancelPayment(List<CancelPayment> cancelPayment);
    }
}
