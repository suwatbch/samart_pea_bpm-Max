using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.EcsModule.Interface.Services;
using PEA.BPM.EcsModule.Interface.BusinessEntities;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using PEA.BPM.EcsModule.DA;
using System.Web.Security;
using System.Threading;
using System.Linq;

namespace PEA.BPM.EcsModule.BS
{
    public class EcsBS : IEcsService
    {
       
        #region Static variable use for Microsoft Queue

        private static Queue<InputParameters> EcsQueue = new Queue<InputParameters>();//Use this line because we implement safe thread queue ,it's limit by .NET 3.5
        private static int QueueCount = 0;       
        private static int QueueLimitSetting = 0;      //If QueueLimit <= 0 Program will disable EnQueue process.
        private static int QueueDelayTimeSetting = 0;  //Calculate in milisecond 1000 = 1 sec.
        private const string Update = "Update";
        private const string Cancel = "Cancel";
        public class InputParameters
        {           
            public string CaId { get; set; }
            public string InvoiceNo { get; set; }
            public string TerminalId { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public string ConsumeType { get; set; }
        }
        
        #endregion

        public EcsBS()
        {
            var svc = new EcsDA();
            var init = svc.GetEcsSetting();
            QueueLimitSetting = init[0];
            QueueDelayTimeSetting = init[1];           
        }

        #region Not in use 

        public Credential Login(string userId, string password, string LocalIP)
        {
            //Credential credential = new Credential();
            string pwd = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1");

            var conData = new EcsDA();
            Credential conList = conData.Login(userId, pwd, LocalIP);

            return conList;

        }

        public string CheckToken(string userId, string Token)
        {

            var conData = new EcsDA();
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

        public List<SearchInvoiceInfo> SearchInvoiceByCaId(string caId, string TermanalId)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEcsService Members       

        public string UpdatePaymentStatus(string caId, string invoiceNo, string terminalId, string user, string password)
        {
            try
            {
                var ecs     = new EcsDA();
                var result  = ecs.UpdatePaymentStatus(caId, invoiceNo, terminalId, user, password);
                return result; 
            }
            catch
            {
                return "Fail";
            }
        }

        public List<PrintInvoiceInfo> CancelPaymentStatus(string caId, string invoiceNo, string terminalId, string user, string password)
        {

            try
            {
                
                    var conData = new EcsDA();
                    var conList = conData.CancelPaymentStatus(caId, invoiceNo, terminalId, user, password);
                    return conList;    
                
            }
            catch (Exception e)
            {
                throw;
            }          
        }

        #endregion

        #region Queue perform|Created on 05-Aug-2016 by Uthen

       
        public void TaskCheckLimitSetting()
        {
            if (QueueCount >= QueueLimitSetting)
            {
                Thread.Sleep(QueueDelayTimeSetting);                             
            }           
        }

        public void TaskEnQueue(string caId, string invoiceNo, string terminalId, string user, string password,string workType)
        {
            var tmp = new InputParameters();
            tmp.CaId = caId;
            tmp.InvoiceNo = invoiceNo;
            tmp.TerminalId = terminalId;
            tmp.UserName = user;
            tmp.Password = password;
            tmp.ConsumeType = workType;

            EcsQueue.Enqueue(tmp);
            QueueCount = EcsQueue.Count;
           
        }

        public void TaskDeQueue()
        {
            EcsQueue.Dequeue();
            QueueCount = EcsQueue.Count;           
        }       

        #endregion

        #region IEcsService Members


        List<PrintInvoiceInfo> IEcsService.UpdatePaymentStatus(string caId, string InvoiceNo, string TerminalId, string User, string Password)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
