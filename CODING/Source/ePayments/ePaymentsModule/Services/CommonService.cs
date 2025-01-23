using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.ePaymentsModule.Interface.Services;
using Microsoft.Practices.CompositeUI;
using System.Threading;
using System.Collections;
using PEA.BPM.ePaymentsModule.SG;
using System.Data;
using System.Configuration;
using System.Data.Common;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities.Reports;

namespace PEA.BPM.ePaymentsModule.Services
{
    [Service(typeof(ICommonService))]
    public class CommonService : ICommonService
    {

        public CommonService()
        {
        }

        #region Service Factory
        public ICommonService GetService()
        {
            return GetService(false);
        }

        public ICommonService GetService(bool serverService)
        {
            if (serverService || Session.Branch.OnlineConnection)
            {
                return new CommonSG(true);
            }
            else
            {
                return new CommonSG(false);
            }
        }
        #endregion


        #region ICommonService Members

        public string VerifyContractorService(string caId, string period, decimal debtAmount)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<SearchContractorInfo> SearchContractorService(string caId, string billFlag)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<SearchConAccountInfo> SearchConAccountService(string caId)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<Agency> GetAgencyAllService(Agency agency)
        {
            try
            {
                return GetService().GetAgencyAllService(agency);
            }
            catch
            {
                throw;
            }
        }

        #endregion


        #region ICommonService Members


        public List<AccountClassInfo> GetAccountClassList(AccountClassInfo acParam)
        {
            try
            {
                return GetService().GetAccountClassList(acParam);
            }
            catch
            {
                throw;
            }
        }
    


        public List<Company> GetCompanyList(CompanyParamInfo comParam)
        {
            return GetService().GetCompanyList(comParam);
        }

        #endregion

        #region ICommonService Members


        public List<Company> GetCompanyByUploadDtList(DateTime? uploadDt)
        {
            try
            {
                return GetService().GetCompanyByUploadDtList(uploadDt);
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}
