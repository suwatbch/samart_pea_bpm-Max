using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Apm2.Interface.BusinessEntities;
using System.Data.Common;

namespace PEA.BPM.Apm2.Interface.Services
{
    public interface IApm2Service
    {
        //int PayInvoice(DbTransaction trans, PaymentInfo payment);


        List<SearchInvoiceInfo> SearchInvoiceByCaId(string caId, string TermanalId);
        List<PrintInvoiceInfo> UpdatePaymentStatus(string caId, string InvoiceNo, string TerminalId);

    }
}
