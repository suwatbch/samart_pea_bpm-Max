using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.EcsModule.Interface.BusinessEntities;
using System.Data.Common;

namespace PEA.BPM.EcsModule.Interface.Services
{
    public interface IEcsService
    {
        List<SearchInvoiceInfo> SearchInvoiceByCaId(string caId, string TermanalId);
        List<PrintInvoiceInfo> UpdatePaymentStatus(string caId, string InvoiceNo, string TerminalId,string User,string Password);
        List<PrintInvoiceInfo> CancelPaymentStatus(string caId, string InvoiceNo, string TerminalId, string User, string Password);
     
    }
}
