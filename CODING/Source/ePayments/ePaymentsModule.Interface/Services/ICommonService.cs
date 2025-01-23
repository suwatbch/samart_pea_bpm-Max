using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities.Reports;

namespace PEA.BPM.ePaymentsModule.Interface.Services
{
    public interface ICommonService
    {
        string VerifyContractorService(string caId, string period, decimal debtAmount);
        List<SearchContractorInfo> SearchContractorService(string caId, string billFlag);
        List<SearchConAccountInfo> SearchConAccountService(string caId);

        List<Agency> GetAgencyAllService(Agency agency);
        List<AccountClassInfo> GetAccountClassList(AccountClassInfo acParam);
        List<Company> GetCompanyList(CompanyParamInfo comParam);
        List<Company> GetCompanyByUploadDtList(DateTime? uploadDt);
    }
}
