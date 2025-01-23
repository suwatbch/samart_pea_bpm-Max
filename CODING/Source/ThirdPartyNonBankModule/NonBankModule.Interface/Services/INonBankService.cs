using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.NonBankModule.Interface.BusinessEntities;
using System.Data.Common;

namespace PEA.BPM.NonBankModule.Interface.Services
{
    public interface INonBankService
    {
        string Login(string UserId, string Password);
        List<SearchConAccountServiceInfo> SearchConAccountService(string CaId);
        List<SearchContractorServiceInfo> SearchBillInformation(string CaId, string BillFlag);
        string UpdateBillMarkFlagService(string CaId, string InvoiceNo,string AgencyID);
    }
}
