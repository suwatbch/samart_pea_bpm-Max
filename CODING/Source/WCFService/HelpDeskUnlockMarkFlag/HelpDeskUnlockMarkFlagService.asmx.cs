using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;

using System.ComponentModel;
using System.Collections.Generic;
using PEA.BPM.Architecture.ArchitectureInterface.Services;
using PEA.BPM.Architecture.ArchitectureDA;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities;
using PEA.BPM.Architecture.ArchitectureInterface.Constants;
using System.Configuration;

using PEA.BPM.HelpDeskUnlockMarkFlagModule.Interface.BusinessEntities;
using PEA.BPM.HelpDeskUnlockMarkFlagModule.BS;
using System.Web.Security;
using PEA.BPM.HelpDeskUnlockMarkFlagServices;
using PEA.BPM.Architecture.ArchitectureBS;


namespace HelpDeskUnlockMarkFlagService
{
    /// <summary>
    /// Summary description for HelpDeskUnlockMarkFlagService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class HelpDeskUnlockMarkFlagWebService : HelpDeskUnlockBaseWebService
    {       
        private HelpDeskUnlockMarkFlagBS    UnlockService;

        public HelpDeskUnlockMarkFlagWebService()
        {           
            UnlockService           = new HelpDeskUnlockMarkFlagBS();
        }

        [WebMethod]
        public string Login(string UserId, string Password)
        {
            string hashPwd  = FormsAuthentication.HashPasswordForStoringInConfigFile(Password,"SHA1");
            string token    = UnlockService.Login(UserId, hashPwd);
            //string IsAuthenticated = CommonService.IsAuthenticated(UserId, hashPwd, "BPMHelpdeskUnlockMarkflagService");
            return token;
        }

        [WebMethod]
        public bool CheckToken(string UserId, string Token)
        {
            string token = UnlockService.CheckToken(UserId, Token);
            if (token == "TokenMatch")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        
        [SoapHeader("AuthInfoValue"), WebMethod]
        public string UnlockMarkFlag(string invoiceNo)
        {
            //Disable WebServiceSecurity because we use this in internal org.
            //WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);

            UnlockMarkFlagServiceInfo unlockInfo = new UnlockMarkFlagServiceInfo();
            try
            {
                unlockInfo = UnlockService.UnlockMarkFlagService(invoiceNo);
                if (!string.IsNullOrEmpty(unlockInfo.UnlockMarkFlagServiceResult))
                {
                    unlockInfo.UnlockMarkFlagServiceResult = "Success";
                }
                else
                {
                    unlockInfo.UnlockMarkFlagServiceResult = "Fail";
                }
            }
            catch 
            {
                unlockInfo.UnlockMarkFlagServiceResult = "Fail";
            }

            return unlockInfo.UnlockMarkFlagServiceResult;
        }
        
        [SoapHeader("AuthInfoValue"), WebMethod]
        public SearchARInfo SearchARInfoService(string CaId, string Period, string GAmount)
        {
            //Disable WebServiceSecurity because we use this in internal org.
            //WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);
            SearchARInfo ArInfo = new SearchARInfo();
            double gAmount = 0.0;
           
            try
            {
                gAmount = Convert.ToDouble(GAmount);
                ArInfo = UnlockService.SearchARInfo(CaId, Period, gAmount);               
            }
            catch
            {
                ArInfo =  null;
            }

            return ArInfo;
        }


    
    }
}