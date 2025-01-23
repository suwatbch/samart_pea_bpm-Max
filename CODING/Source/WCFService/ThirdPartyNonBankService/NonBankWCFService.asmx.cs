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

using PEA.BPM.NonBankModule.Interface.BusinessEntities;
using PEA.BPM.NonBankModule.BS;
using System.Web.Security;
using PEA.BPM.ThirdPartyNonBankServices;
using PEA.BPM.Architecture.ArchitectureBS;




namespace PEA.BPM.ThirdPartyNonBankService
{
    /// <summary>
    /// Summary description for ThirdPartyNonBankService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class BPMNonBankServices : NonBankBaseWebService
    {
        private CommonBS CommonService;
        private NonBankBS NonBankService = new NonBankBS();

        public BPMNonBankServices()
        {
            CommonService = new CommonBS();
            NonBankService = new NonBankBS();
        }

        [WebMethod]
        public string Login(string UserId, string Password)
        {
            string hashPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(Password, "SHA1");
            string token = CommonService.GetToken(UserId, hashPwd);
            return token;
        }


        #region UPDATE MARK FLAG

        [SoapHeader("AuthInfoValue"), WebMethod]
        public string UpdateBillMarkFlagService(string CaId, string InvoiceNo, string AgencyID)
        {
            var authUserId = this.AuthInfoValue.UserId;
            var authToken = this.AuthInfoValue.AuthToken;
            /*
            var checkToken  = CommonService.CheckToken(authUserId, authToken);
            if (!string.IsNullOrEmpty(checkToken))
            {
                return checkToken;
            }
            */
            var result          = ConfirmToken(authUserId, authToken);
            string markflagResult = null;
            var log             = new LoggerModel
            {
                AgencyID        = AgencyID,
                AuthToken       = authToken,
                AuthUserId      = authUserId,
                CaId            = CaId,
                ServiceResultText = result.ConfirmText,
                InvoiceNo       = InvoiceNo,
                ServiceName     = "UpdateBillMarkFlagService"
            };
            if (result.ConfirmToken == false)
            {
                markflagResult = result.ConfirmText;
                try
                {
                    NonbankLogger(log);
                }
                catch (Exception ex)
                {

                }

            }
            else
            {
                try
                {
                    markflagResult = NonBankService.UpdateBillMarkFlagService(CaId, InvoiceNo, AgencyID);
                    markflagResult = markflagResult.Trim();
                    log.ServiceResultText = markflagResult;
                }
                catch
                {
                    markflagResult = "Fail";
                    log.ServiceResultText = markflagResult;
                }
                finally
                {
                    try
                    {
                        NonbankLogger(log);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            return markflagResult;
        }

        #endregion

        [SoapHeader("AuthInfoValue"), WebMethod]
        public List<SearchContractorServiceInfo> SearchContractorService(string CaId, string BillFlag)
        {
            var modelList = new List<SearchContractorServiceInfo>();
            var authUserId = this.AuthInfoValue.UserId;
            var authToken = this.AuthInfoValue.AuthToken;
            var checkToken = CommonService.CheckToken(authUserId, authToken);
            if (!string.IsNullOrEmpty(checkToken))
            {
                var model = new SearchContractorServiceInfo();
                model.Remark = checkToken;
                modelList.Add(model);
                return modelList;
            }

            try
            {
                return NonBankService.SearchBillInformation(CaId.Trim(), BillFlag.Trim());
            }
            catch
            {
                return modelList;
            }
        }

        #region SEARCH ACCOUNT DETAIL

        [SoapHeader("AuthInfoValue"), WebMethod]
        public List<SearchConAccountServiceInfo> SearchConAccountService(string CaId)
        {
            var model = new SearchConAccountServiceInfo();
            var modelList = new List<SearchConAccountServiceInfo>();

            var authUserId = this.AuthInfoValue.UserId;
            var authToken = this.AuthInfoValue.AuthToken;
            var checkToken = CommonService.CheckToken(authUserId, authToken);
            if (!string.IsNullOrEmpty(checkToken))
            {
                model.Remark = checkToken;
                modelList.Add(model);
                return modelList;
            }


            try
            {
                return NonBankService.SearchConAccountService(CaId.Trim());
            }
            catch
            {
                return modelList;
            }
        }

        #endregion

        private ConfirmTokenModel ConfirmToken(string userId, string token)
        {
            var result = CommonService.CheckToken(userId, token);
            var obj = new ConfirmTokenModel();
            if (result == "NotFoundUser" || result == "TokenNotMatch" || result == "TokenExpired")
            {
                obj.ConfirmToken = false;
                obj.ConfirmText = result;
            }
            else
            {
                obj.ConfirmToken = true;
                obj.ConfirmText = result;
            }
            return obj;
        }

        private void NonbankLogger(LoggerModel model)
        {
            try
            {
                var m = model;
                NonBankService.InsertTransactionLog(m.ServiceName, m.AuthUserId, m.AuthToken, m.CaId, m.InvoiceNo, m.AgencyID, m.ServiceResultText);
            }
            catch (Exception ex)
            {
                //Now no need to do
            }
        }
    }


}

public class ConfirmTokenModel
{
    public bool ConfirmToken { get; set; }
    public string ConfirmText { get; set; }
}

public class LoggerModel
{
    public string ServiceName { get; set; }
    public string AuthUserId { get; set; }
    public string AuthToken { get; set; }
    public string CaId { get; set; }
    public string InvoiceNo { get; set; }
    public string AgencyID { get; set; }
    public string ServiceResultText { get; set; }
}