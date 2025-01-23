using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.ePaymentsModule.Interface.Services;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities;
using PEA.BPM.ePaymentsModule.DA;
using System.ComponentModel;
using System.Globalization;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities.Reports;

namespace PEA.BPM.ePaymentsModule.BS
{
    public class CommonBS : ICommonService
    {

        #region ICommonService Members

        public string VerifyContractorService(string caId, string period, decimal debtAmount)
        {
            string vResult = null;
            try
            {
                CommonDA conData = new CommonDA();
                vResult = conData.VerifyContractorData(caId, period, debtAmount);
            }

            catch (Exception e)
            {
                throw;
            }
            return vResult;
        }

        public List<SearchContractorInfo> SearchContractorService(string caId, string billFlag)
        {
            try
            {
                if (billFlag.Length == 1 && billFlag.CompareTo("0") > 0 && billFlag.CompareTo("4") < 0)
                {
                    CommonDA conData = new CommonDA();
                    List<SearchContractorInfo> conList = conData.SearchContractorData(caId, billFlag);
                    return conList;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }


        public List<SearchConAccountInfo> SearchConAccountService(string caId)
        {
            try
            {
                CommonDA conData = new CommonDA();
                List<SearchConAccountInfo> conList = conData.SearchConAccountData(caId);
                return conList;
            }
            catch (Exception e)
            {
                throw;
            }        
        }

        public string GetRefDocNo()
        {
            try
            {
                CommonDA conData = new CommonDA();
                string refDocNo = "¡§(Ã´){0}ÅÇ.{1}";
                string tempDoc = conData.GetRefDocNo();
                tempDoc = tempDoc != null ? tempDoc : string.Empty;
                if (tempDoc != String.Empty)
                {                    
                    string nowDate = DateTime.Now.ToString("yyyyMMdd", new CultureInfo("th-TH"));
                    if (nowDate == tempDoc.Substring(0, 8))
                    {
                        refDocNo = String.Format(refDocNo, tempDoc.Substring(8, 3), tempDoc.Substring(4,2) + tempDoc.Substring(5,2) + tempDoc.Substring(0,4));
                    }
                    else if (nowDate.Substring(0,4) == tempDoc.Substring(0, 4))
                    {
                        int seq = Convert.ToInt16(tempDoc.Substring(8, 3));
                        seq = seq + 1;
                        refDocNo = String.Format(refDocNo, seq.ToString().PadLeft(3, '0'), DateTime.Now.ToString("ddMMyyyy", new CultureInfo("th-TH")));
                    }
                    else
                    {
                        string tempDate = DateTime.Now.ToString("ddMMyyyy", new CultureInfo("th-TH"));
                        refDocNo = String.Format(refDocNo, "001", tempDate); 
                    }
                }
                else 
                {
                    string tempDate = DateTime.Now.ToString("ddMMyyyy", new CultureInfo("th-TH"));
                    refDocNo = String.Format(refDocNo, "001", tempDate); 
                }
                return refDocNo;                
            }
            catch (Exception e)
            {
                throw;
            }
        }
   
        public List<Agency> GetAgencyAllService(Agency agency)
        {
            try
            {
                CommonDA conData = new CommonDA();
                List<Agency> agencyList = conData.GetAgencyAllData(agency);
                return agencyList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<AccountClassInfo> GetAccountClassList(AccountClassInfo acParam)
        {
            try
            {               
                CommonDA comDa = new CommonDA();
                List<AccountClassInfo> acList = new List<AccountClassInfo>();
                acList = comDa.GetAccountClassList(acParam);
                return acList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<Company> GetCompanyList(CompanyParamInfo comParam)
        {
            try
            {
                CommonDA comDa = new CommonDA();
                List<Company> comList = new List<Company>();
                comList = comDa.GetCompanyList(comParam);
                return comList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public List<Company> GetCompanyByUploadDtList(DateTime? uploadDt)
        {
            try
            {
                CommonDA comDa = new CommonDA();
                List<Company> comList = new List<Company>();
                comList = comDa.GetCompanyByUploadDtList(uploadDt);
                return comList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
                    
        #endregion

        #region ICommonService Members

        string ICommonService.VerifyContractorService(string caId, string period, decimal debtAmount)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        List<SearchContractorInfo> ICommonService.SearchContractorService(string caId, string billFlag)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        List<SearchConAccountInfo> ICommonService.SearchConAccountService(string caId)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        List<Agency> ICommonService.GetAgencyAllService(Agency agency)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        List<AccountClassInfo> ICommonService.GetAccountClassList(AccountClassInfo acParam)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        List<Company> ICommonService.GetCompanyList(CompanyParamInfo comParam)
        {
            throw new Exception("The method or operation is not implemented.");
        }


        #endregion
    }
}
