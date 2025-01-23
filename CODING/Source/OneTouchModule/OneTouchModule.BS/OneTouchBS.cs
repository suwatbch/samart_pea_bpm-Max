using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.OneTouchModule.Interface.Services;
using PEA.BPM.OneTouchModule.Interface.BusinessEntities;
//using PEA.BPM.OneTouchModule.Interface.BusinessEntities.ReceiptPrinting;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using PEA.BPM.OneTouchModule.DA;
//using PEA.BPM.Architecture.CommonUtilities;
using System.Web.Security;

namespace PEA.BPM.OneTouchModule.BS
{
    public class OneTouchBS : IOneTouchService
    {

        public Credential Login(string userId, string password, string LocalIP)
        {
            //Credential credential = new Credential();
            string pwd = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1");

            OneTouchDA conData = new OneTouchDA();
            Credential conList = conData.Login(userId, pwd, LocalIP);
            
            return conList;

        }

        public string CheckToken(string userId, string Token)
        {

            OneTouchDA conData = new OneTouchDA();
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

        public List<OneTouchInfo> SearchPayment(string NotificationNo)
        {
            try
            {
                //if (billFlag.Length == 1 && billFlag.CompareTo("0") > 0 && billFlag.CompareTo("4") < 0)
                {
                    OneTouchDA conData = new OneTouchDA();
                    List<OneTouchInfo> conList = conData.SearchPayment(NotificationNo);
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

        public string UpdatePayment(string NotificationNo, string InvoiceNo, string ReceiptId, string DebtId)
        {
            try
            {
                //if (billFlag.Length == 1 && billFlag.CompareTo("0") > 0 && billFlag.CompareTo("4") < 0)
                {
                    OneTouchDA conData = new OneTouchDA();

                    return conData.UpdatePayment(NotificationNo, InvoiceNo, ReceiptId, DebtId);
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
