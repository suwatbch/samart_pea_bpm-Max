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
        private NonBankBS NonBankService;

        public BPMNonBankServices()
        {
            CommonService = new CommonBS();
            NonBankService = new NonBankBS();
        }

        [WebMethod]
        public string Login(string UserId, string Password)
        {
            string hashPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(Password,"SHA1");
            string token = CommonService.GetToken(UserId, hashPwd);           
            return token;
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public string UpdateBillMarkFlagService(string CaId, string InvoiceNo, string AgencyID)
        {
            var authUserId  = this.AuthInfoValue.UserId;
            var authToken   = this.AuthInfoValue.AuthToken;
            var checkToken = CommonService.CheckToken(authUserId, authToken);
            if (!string.IsNullOrEmpty(checkToken))
            {
                return checkToken;
            }

            string markflagResult = null;
            try
            {
                markflagResult = NonBankService.UpdateBillMarkFlagService(CaId, InvoiceNo, AgencyID);                
            }
            catch 
            {
                markflagResult =  "Fail";
            }
            return markflagResult;

        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public List<SearchContractorServiceInfo> SearchContractorService(string CaId, string BillFlag)
        {
            #region #Sample data

            //<BranchId>L06304</BranchId>
            //<CaId>020007625564</CaId>
            //<Period>255701</Period>
            //<GAmount>192.8100</GAmount>
            //<FTAmount>31.8600</FTAmount>
            //<VatAmount>12.6100</VatAmount>
            //<CAPmId>C</CAPmId>--B ตัวแทน --C จ่ายเคาร์เตอร์ --D หักบัญชีธนาคาร  --E หักบัตรเครดิต --G ชำระแบบกลุ่ม --A ชำระทันที
             //<AccountClassId>PPS1</AccountClassId>
            //<AccountClassName>เอกชน - รายย่อย</AccountClassName>
            //<ModifiedFlag>0</ModifiedFlag>
            //<BillBookFlag>0</BillBookFlag>
            //<DisconnectionFlag>0</DisconnectionFlag>
            //<PaymentOrderFlag>0</PaymentOrderFlag>
            //<GroupInvoiceFlag>0</GroupInvoiceFlag>
            //<Qty>54.00</Qty>
            //<ReceiveStatus>1</ReceiveStatus>
            //<ARPmId>C</ARPmId>
            //<InvoiceNo>852401582504</InvoiceNo>
            //<DueDt>05/02/2557</DueDt>
            

            var m = new SearchContractorServiceInfo();
            m.BranchId = "L06304";
            
            #endregion

            var modelList = new List<SearchContractorServiceInfo>();
            var authUserId = this.AuthInfoValue.UserId;
            var authToken = this.AuthInfoValue.AuthToken;
            var checkToken = CommonService.CheckToken(authUserId, authToken);
            if (!string.IsNullOrEmpty(checkToken))
            {
                return modelList;
            }

            try
            {  
                WebServiceSecurity.CheckAuthorization(this.AuthInfoValue);
                return NonBankService.SearchContractorService(CaId.Trim(), BillFlag.Trim());
            }
            catch
            {
                return modelList;
            }
        }

        [SoapHeader("AuthInfoValue"), WebMethod]
        public List<SearchConAccountServiceInfo> SearchConAccountService(string CaId)
        {   
            #region #Sample data
            //Test
            //<BranchId>L06304</BranchId>
          //<CaName>นาย สมเกียรติ  ชูเพ็ง</CaName>
          //<CaAddress>เลขที่ 170 ม.2 ต.เขาย่า อ.ศรีบรรพต จ.พัทลุง 93190</CaAddress>
          //<AccountClassId>PPS1</AccountClassId>
          //<AccountClassName>เอกชน - รายย่อย</AccountClassName>
          //<MeterSizeName>METER 1 P 2 W 220 V.,5(15) A.</MeterSizeName>
            //<MeterInstallDt>27/08/2554</MeterInstallDt>
            #endregion

            var model = new SearchConAccountServiceInfo();
            var modelList = new List<SearchConAccountServiceInfo>();

            var authUserId = this.AuthInfoValue.UserId;
            var authToken = this.AuthInfoValue.AuthToken;
            var checkToken = CommonService.CheckToken(authUserId, authToken);
            if (!string.IsNullOrEmpty(checkToken))
            {
                return modelList;
            }
            modelList.Add(model);
            return modelList;
        }
    
    }
}