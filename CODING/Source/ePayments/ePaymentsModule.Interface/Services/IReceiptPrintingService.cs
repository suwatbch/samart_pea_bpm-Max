using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities.ReceiptPrinting;
using System.Data.Common;

namespace PEA.BPM.ePaymentsModule.Interface.Services
{
    public interface IReceiptPrintingService
    {
        List<Bills> PrintGreenBill(ReceiptConditionParam param);
        List<PPrintedReceipt> GetPPrintedService(PPrintedReceiptParam param);
        List<Company> GetAgentAllService();
        List<EPayUpload> SearchAgentPaymentNumber(EPayUpload param);

        /* Off Line  */
        List<PPrintedDeposit> SearchDebtClearify(PPrintedDeposit param);
        List<PPrintedDeposit> GetCADepositPPrinted(List<PPrintedDeposit> paramList);
        List<PPrintedDeposit> GetAgentDepositPPrinted(List<PPrintedDeposit> paramList);
    }
}
  