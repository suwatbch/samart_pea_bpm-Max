using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.HelpDeskUnlockMarkFlagModule.Interface.Services;
using PEA.BPM.HelpDeskUnlockMarkFlagModule.Interface.BusinessEntities;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using PEA.BPM.HelpDeskUnlockMarkFlagModule.DA;
using System.Web.Security;
using System.Threading;
using System.Linq;

namespace PEA.BPM.HelpDeskUnlockMarkFlagModule.BS
{
    public class HelpDeskUnlockMarkFlagBS : IHelpDeskUnlockMarkFlagService
    {
        HelpDeskUnlockMarkFlagDA svc = new HelpDeskUnlockMarkFlagDA(); 

        public HelpDeskUnlockMarkFlagBS()
        {
            svc = new HelpDeskUnlockMarkFlagDA(); 
        }

        #region IHelpDeskUnlockMarkFlagService Members

        public UnlockMarkFlagServiceInfo UnlockMarkFlagService(string invoiceNo)
        {
            UnlockMarkFlagServiceInfo unlockResult          = new UnlockMarkFlagServiceInfo();
            try
            {
                HelpDeskUnlockMarkFlagDA service            = new HelpDeskUnlockMarkFlagDA();
                unlockResult.UnlockMarkFlagServiceResult    = service.UnlockMarkFlagDA(invoiceNo);
            }
            catch(Exception expt)
            {
                throw expt;
            }
            return unlockResult;
        }

        public SearchARInfo SearchARInfo(string CaId, string Period, double GAmount)
        {
            double gAmount = Convert.ToDouble(GAmount);
            SearchARInfo ArInfo = new SearchARInfo();
            try
            {
                HelpDeskUnlockMarkFlagDA service = new HelpDeskUnlockMarkFlagDA();
                ArInfo = service.SearchARInfoAR(CaId, Period, GAmount);
            }
            catch (Exception expt)
            {
                throw expt;
            }
            return ArInfo;
        }

        public string Login(string UserId, string hashPassword)
        {
            try
            {
                Credential credential = svc.Login(UserId, hashPassword);
                return credential.Token;
            }
            catch (Exception)
            {
                
                throw;
            }            
        }

        public string CheckToken(string UserId, string Token)
        {
            try
            {
                string result = svc.CheckToken(UserId, Token);
                return result;
            }
            catch (Exception)
            {
                throw;
            }     
        }


        #endregion



       }
}
