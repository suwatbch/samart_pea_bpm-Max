using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.ApmModule.Interface.BusinessEntities;
using System.Data.Common;

namespace PEA.BPM.ApmModule.Interface.Services
{
    public interface IApmService
    {
        //int PayInvoice(DbTransaction trans, PaymentInfo payment);


        List<SearchInvoiceInfo> SearchInvoiceByCaId(string caId, string TermanalId);
        List<PrintInvoiceInfo> UpdatePaymentStatus(string caId, string InvoiceNo, string TerminalId);

    }
}
