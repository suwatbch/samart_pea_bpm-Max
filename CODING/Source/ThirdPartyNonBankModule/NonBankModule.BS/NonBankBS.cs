using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.NonBankModule.Interface.Services;
using PEA.BPM.NonBankModule.Interface.BusinessEntities;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using PEA.BPM.NonBankModule.DA;
using System.Web.Security;
using System.Threading;
using System.Linq;

namespace PEA.BPM.NonBankModule.BS
{
    public class NonBankBS : INonBankService
    {
        
       
        #region Static variable use for Microsoft Queue

        private static Queue<InputParameters> NonBankQueue = new Queue<InputParameters>();//Use this line because we implement safe thread queue ,it's limit by .NET 3.5
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

        public NonBankBS()
        {
            var svc     = new NonBankDA();
            var init    = svc.GetNonBankSetting();
            QueueLimitSetting = init[0];
            QueueDelayTimeSetting = init[1];           
        }        

        #region Queue perform|Created on 05-Aug-2016 by Uthen

        public List<PrintInvoiceInfo> TaskWithQueue(string caId, string invoiceNo, string terminalId, string user, string password,string workType)
        {
            var result = new List<PrintInvoiceInfo>();
            try
            {
                TaskEnQueue(caId, invoiceNo, terminalId, user, password,workType);
                TaskCheckLimitSetting();

                var q = NonBankQueue.First();
                if (q.ConsumeType == Update)
                {
                    var conData = new NonBankDA();
                    result = conData.UpdatePaymentStatus(q.CaId, q.InvoiceNo, q.TerminalId, q.UserName, q.Password);
                }
                else
                {
                    var conData = new NonBankDA();
                    result = conData.CancelPaymentStatus(q.CaId, q.InvoiceNo, q.TerminalId, q.UserName, q.Password);
                }
               
                             
                if (result != null)
                {
                    TaskDeQueue();
                }
            }
            catch (Exception)
            {                
                throw;
            }           
           
            return result;            
        }
       
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

            NonBankQueue.Enqueue(tmp);
            QueueCount = NonBankQueue.Count;
           
        }

        public void TaskDeQueue()
        {
            NonBankQueue.Dequeue();
            QueueCount = NonBankQueue.Count;           
        }       

        #endregion

        #region INonBankService Members

        public string Login(string UserId, string Password)
        {
            try
            {
                var         svc                 = new NonBankDA();
                string      hashPwd             = FormsAuthentication.HashPasswordForStoringInConfigFile(Password, "SHA1");
                Credential  credential          = svc.Login(UserId, hashPwd);
                return      credential.Token;
            }
            catch (Exception)
            {

                throw;
            }            
        }

        //ค้นหาข้อมูล ผู้ใช้ไฟ
        public List<SearchConAccountServiceInfo> SearchConAccountService(string CaId)
        {
            var modelList = new List<SearchConAccountServiceInfo>();
            try
            {
                 NonBankDA service  = new NonBankDA();
                 modelList          = service.SearchContractorInformation(CaId);
                 return modelList;
            }
            catch (Exception)
            {                
                throw;
            }            
        }

        //ค้นหาข้อมูล หนี้
        public List<SearchContractorServiceInfo> SearchBillInformation(string CaId, string BillFlag)
        {
            List<SearchContractorServiceInfo> conList = new List<SearchContractorServiceInfo>();
            try
            {
                NonBankDA conData = new NonBankDA();
                conList = conData.SearchBillInformation(CaId, BillFlag);                
            }
            catch (Exception)
            {                
                throw;
            }
            return conList;           
        }

        //อัพเดท การชำระเงิน
        public string UpdateBillMarkFlagService(string CaId, string InvoiceNo, string AgencyID)
        {            
            try
            {
                NonBankDA updMarkService = new NonBankDA();
                var result  = "";
                result      = updMarkService.UpdateBillMarkFlagData(CaId, InvoiceNo, AgencyID);
                return result;
            }
            catch
            {
                throw;
            }            
        }

        public void InsertTransactionLog(string serviceName, string userId, string token, string caId, string invoiceNo, string agencyId, string serviceResultText)
        {
            try
            {
                NonBankDA updMarkService = new NonBankDA();
                updMarkService.InsertNonBankTransactionLog(serviceName, userId, token, caId, invoiceNo, agencyId, serviceResultText);
            }
            catch
            {

            }
        }

        #endregion
    }
}
