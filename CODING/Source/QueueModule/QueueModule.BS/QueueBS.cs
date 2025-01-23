using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.QueueModule.Interface.Services;
using PEA.BPM.QueueModule.Interface.BusinessEntities;
//using PEA.BPM.QueueModule.Interface.BusinessEntities.ReceiptPrinting;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using PEA.BPM.QueueModule.DA;
//using PEA.BPM.Architecture.CommonUtilities;
using System.Web.Security;

namespace PEA.BPM.QueueModule.BS
{
    public class QueueBS : IQueueService
    {

        public Credential Login(string userId, string password, string LocalIP)
        {
            //Credential credential = new Credential();
            string pwd = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1");

            QueueDA conData = new QueueDA();
            Credential conList = conData.Login(userId, pwd, LocalIP);
            
            return conList;

        }

        public string CheckToken(string userId, string Token)
        {

            QueueDA conData = new QueueDA();
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
                    QueueDA conData = new QueueDA();
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
    }
}
