using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.QueueModule.Interface.BusinessEntities;
using System.Data.Common;

namespace PEA.BPM.QueueModule.Interface.Services
{
    public interface IQueueService
    {
        //int PayInvoice(DbTransaction trans, PaymentInfo payment);
        List<SearchInvoiceInfo> SearchInvoiceByCaId(string caId, string TermanalId);
    }
}
