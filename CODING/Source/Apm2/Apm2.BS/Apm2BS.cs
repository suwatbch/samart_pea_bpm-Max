using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Apm2.Interface.Services;
using PEA.BPM.Apm2.Interface.BusinessEntities;
//using PEA.BPM.ApmModule.Interface.BusinessEntities.ReceiptPrinting;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using PEA.BPM.Apm2.DA;
//using PEA.BPM.Architecture.CommonUtilities;
using System.Web.Security;

namespace PEA.BPM.Apm2.BS
{
    public class Apm2BS : IApm2Service
    {

        public Credential Login(string userId, string password, string LocalIP)
        {
            //Credential credential = new Credential();
            string pwd = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1");

            Apm2DA conData = new Apm2DA();
            Credential conList = conData.Login(userId, pwd, LocalIP);
            
            return conList;

        }

        public string CheckToken(string userId, string Token)
        {

            Apm2DA conData = new Apm2DA();
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
                    Apm2DA conData = new Apm2DA();
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
                    Apm2DA conData = new Apm2DA();
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
