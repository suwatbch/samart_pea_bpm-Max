using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.ApmModule.Interface.Services;
using PEA.BPM.ApmModule.Interface.BusinessEntities;
//using PEA.BPM.ApmModule.Interface.BusinessEntities.ReceiptPrinting;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using PEA.BPM.ApmModule.DA;
//using PEA.BPM.Architecture.CommonUtilities;
using System.Web.Security;

namespace PEA.BPM.ApmModule.BS
{
    public class ApmBS : IApmService
    {

        public Credential Login(string userId, string password, string LocalIP)
        {
            //Credential credential = new Credential();
            string pwd = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1");

            ApmDA conData = new ApmDA();
            Credential conList = conData.Login(userId, pwd, LocalIP);
            
            return conList;

        }

        public string CheckToken(string userId, string Token)
        {

            ApmDA conData = new ApmDA();
            string status = conData.CheckToken(userId, Token);

            if (status == "VALID")
            {
                //Nothing
            }
            else 
            {
                throw new Exception("Token Invalid");
            }

            return "VALID";
        }

        public List<SearchInvoiceInfo> SearchInvoiceByCaId(string caId, string TerminalId)
        {
            try
            {
                //if (billFlag.Length == 1 && billFlag.CompareTo("0") > 0 && billFlag.CompareTo("4") < 0)
                {
                    ApmDA conData = new ApmDA();
                    List<SearchInvoiceInfo> conList = conData.SearchInvoiceByCaId(caId, TerminalId);
                    return conList;
                }
                //else
                //{
                //    return null;
                //}
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<PrintInvoiceInfo> UpdatePaymentStatus(string caId, string InvoiceNo, string TerminalId)
        {
            try
            {
                //if (billFlag.Length == 1 && billFlag.CompareTo("0") > 0 && billFlag.CompareTo("4") < 0)
                {
                    ApmDA conData = new ApmDA();
                    List<PrintInvoiceInfo> conList = conData.UpdatePaymentStatus(caId, InvoiceNo, TerminalId);
                    return conList;
                }
                //else
                //{
                //    return null;
                //}
            }
            catch (Exception e)
            {
                throw;
            }
        }

    }
}
