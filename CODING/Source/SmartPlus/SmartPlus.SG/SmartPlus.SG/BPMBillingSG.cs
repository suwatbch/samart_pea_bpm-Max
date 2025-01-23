using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using PEA.BPM.PaymentCollectionModule.Interface.Services;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;
using System.Data.Common;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Reports;
using PEA.BPM.Architecture.ArchitectureTool.Security;
using PEA.BPM.Architecture.ArchitectureTool;
using System.Web.Services.Protocols;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;
using PEA.BPM.CashManagementModule.Interface.BusinessEntities;
using System.ServiceModel;
using WCFExtras.Soap;
using System.ComponentModel;
using System.Reflection;
using System.ServiceModel.Channels;
using PEA.BPM.SmartPlus.Interface.BusinessEntities;

namespace PEA.BPM.SmartPlus.SG
{
    public class BPMBillingSG
    {
        public List<Invoice> GetPOSInfo(string CaId, string Branch)
        {
            var settings    = new SmartplusSettingsModel();
            settings        = GetAppSettings();
            //var remoteAddress   = new System.ServiceModel.EndpointAddress("http://172.16.166.131/BPMCenterAppServices/POS/BillingWCFService.svc");
            var remoteAddress = new System.ServiceModel.EndpointAddress(settings.ServiceEndPoint);
            using (var ws = new PEA.BPM.SmartPlus.SG.POSService.BillingWCFServiceClient(new System.ServiceModel.BasicHttpBinding(), remoteAddress)) 
            { 
                PEA.BPM.Architecture.CommonUtilities.AuthInfo                                   AuthInfoValue   = new PEA.BPM.Architecture.CommonUtilities.AuthInfo();
                PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.CustomerSearchParam  param           = new PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.CustomerSearchParam();

                #region ..NOT USE
                //string AuthToken = null;
                //var auth = new PEA.BPM.SmartPlus.SG.BPMSecurityService.AuthenticationWebService();
                //AuthToken = auth.GetToken("99000033", "723FF2D51C7E247F65F50DF5223AB3FBFEAA249E");
                ////do
                ////{
                   
                ////} while (auth.CheckToken("99000033", AuthToken) == "TokenNotMatch");

                //AuthInfoValue.AuthToken = "99000033-20201020-12:45:21";
                //AuthInfoValue.UserId = "99000033";
                //AuthInfoValue.LocalIP = "127.0.0.1";
                #endregion

                AuthInfoValue.AuthToken     = settings.Token;
                AuthInfoValue.UserId        = settings.UserId;
                AuthInfoValue.LocalIP       = settings.CurIP;

                param.CaId                  = CaId;     
                param.Branch                = Branch;   
                param.IsSearByBP            = true;                

                var result  = new List<Invoice>();
                result      = ServiceHelper.DecompressData<List<Invoice>>(ws.SearchInvoiceByCustomerId_Compress(AuthInfoValue, param));
                return result;
            }            
        }

        public List<Invoice> GetPOSInfoWithInterestDate(string CaId, string Branch,DateTime? InterestDate)
        {
            var settings = new SmartplusSettingsModel();
            settings = GetAppSettings();
            //var remoteAddress   = new System.ServiceModel.EndpointAddress("http://172.16.166.131/BPMCenterAppServices/POS/BillingWCFService.svc");
            var remoteAddress = new System.ServiceModel.EndpointAddress(settings.ServiceEndPoint);
            using (var ws = new PEA.BPM.SmartPlus.SG.POSService.BillingWCFServiceClient(new System.ServiceModel.BasicHttpBinding(), remoteAddress))
            {
                PEA.BPM.Architecture.CommonUtilities.AuthInfo AuthInfoValue = new PEA.BPM.Architecture.CommonUtilities.AuthInfo();
                PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.CustomerSearchParam param = new PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.CustomerSearchParam();

                #region ..NOT USE
                //string AuthToken = null;
                //var auth = new PEA.BPM.SmartPlus.SG.BPMSecurityService.AuthenticationWebService();
                //AuthToken = auth.GetToken("99000033", "723FF2D51C7E247F65F50DF5223AB3FBFEAA249E");
                ////do
                ////{

                ////} while (auth.CheckToken("99000033", AuthToken) == "TokenNotMatch");

                //AuthInfoValue.AuthToken = "99000033-20201020-12:45:21";
                //AuthInfoValue.UserId = "99000033";
                //AuthInfoValue.LocalIP = "127.0.0.1";
                #endregion

                AuthInfoValue.AuthToken = settings.Token;
                AuthInfoValue.UserId    = settings.UserId;
                AuthInfoValue.LocalIP   = settings.CurIP;

                param.CaId          = CaId;
                param.Branch        = Branch;
                param.IsSearByBP    = true;
                param.ToPayDate     = InterestDate;

                var result  = new List<Invoice>();
                result      = ServiceHelper.DecompressData<List<Invoice>>(ws.SearchInvoiceByCustomerId_Compress(AuthInfoValue, param));
                return result;
            }
        }

        private SmartplusSettingsModel GetAppSettings()
        {
            var settings    = new SmartplusSettingsModel();
            var da          = new PEA.BPM.SmartPlus.DA.SmartPlusDA();
            settings        = da.GetSmartPlusSetting();
            return settings;
        }

        public string GetAuthenToken()
        {
            string  AuthToken   = null;            
            var     ws          = new PEA.BPM.SmartPlus.SG.BPMSecurityService.AuthenticationWebService();
            var     settings    = new SmartplusSettingsModel();
            settings            = GetAppSettings();
            do
	        {                
                AuthToken       = ws.GetToken(settings.UserId, settings.HashPassword);  
               
            } while (ws.CheckToken("99000033", AuthToken) == "TokenNotMatch");
            
            return AuthToken;
        }
    }
}
