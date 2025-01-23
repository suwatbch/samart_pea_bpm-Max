using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.SmartPlus.Interface.BusinessEntities;
using System.Data.Common;

namespace PEA.BPM.SmartPlus.Interface.Services
{
    public interface ISmartPlusService
    {  
        List<ContractorAccountDetailModel> SearchContractAccountDetail(string CaId);
        List<ARInformationInfo> SearchBillInformation(string CaId, string BillFlag);
        string UpdateBillMarkFlagService(string CaId, string InvoiceNo,string AgencyID);
    }
}
