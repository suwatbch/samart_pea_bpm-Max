using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.ePaymentsModule.Interface.Services;
using Microsoft.Practices.CompositeUI;
using System.Threading;
using System.Collections;
using PEA.BPM.ePaymentsModule.SG;
using System.Data;
using System.Configuration;
using System.Data.Common;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities;

namespace PEA.BPM.ePaymentsModule.Services
{
    [Service(typeof(IBillingService))]
    public class BillingService : IBillingService
    {
        public BillingService()
        {

        }

        #region Service Factory
        public IBillingService GetService()
        {
            return GetService(false);
        }

        public IBillingService GetService(bool serverService)
        {
            if (serverService || Session.Branch.OnlineConnection)
            {
                return new BillingSG(true);
            }
            else
            {
                return new BillingSG(false);
            }
        }
        #endregion

        #region IBillingService Members

        //public int PayInvoice(DbTransaction trans, PaymentInfo payment)
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}

        public string UpdateBillMarkFlagService(string caId, string invoiceNo, string modifiedBy, string posetServerId)
        {
            return GetService().UpdateBillMarkFlagService(caId, invoiceNo, modifiedBy, posetServerId);
        }

        public void DelBillMarkFlagService()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<EpayUploadItem> GetDebtComparableService(string caInvoceNo)
        {
            try
            {
                return GetService().GetDebtComparableService(caInvoceNo);
            }
            catch
            {
                throw;
            }
        }

        public List<Company> SearchCompany(Company compParm)
        {
            try
            {
                return GetService().SearchCompany(compParm);
            }
            catch
            {
                throw;
            }
        }

        public List<ClearifyInfo> SearchDebtService(SearchDebtParam searhDebtParam)
        {
            try
            {
                return GetService().SearchDebtService(searhDebtParam);
            }
            catch
            {
                throw;
            }
        }

        public void InsertEPayUploadService(List<EPaymentUploadFile> ePayFileList)
        {
            try
            {
                GetService().InsertEPayUploadService(ePayFileList);
            }
            catch
            {
                throw;
            }
        }

        public bool IsUploadFileNameExist(string fileName)
        {
            try
            {
                return GetService().IsUploadFileNameExist(fileName);
            }
            catch
            {
                throw;
            }
        }

        public List<AgentPayment> GetAgentPaymentService(AgentPayment agentPayment)
        {
            try
            {
                return GetService().GetAgentPaymentService(agentPayment);
            }
            catch
            {
                throw;
            }
        }

        public void InsertAgentPaymentService(List<AgentPayment> agentPayList)
        {
            try
            {
                GetService().InsertAgentPaymentService(agentPayList);
            }
            catch
            {
                throw;
            }
        }

        public List<BPMClearifyInfo> SearchBPMDebtClearify(SearchDebtParam searchDebtParam)
        {
            try
            {
                return GetService().SearchBPMDebtClearify(searchDebtParam);
            }
            catch
            {
                throw;
            }
        }
      
        public bool SaveClearify(DbTransaction trans, SaveClearifyInfo saveClearifyItem)
        {
            try
            {
                return GetService().SaveClearify(trans,saveClearifyItem);
            }
            catch
            {
                throw;
            }
        }

        public List<CancelPayment> SearchAgentPayment(CancelPayment payment)
        {
            try
            {
                return GetService().SearchAgentPayment(payment);
            }
            catch
            {
                throw;
            }
        }

        public void InsertCancelPayment(List<CancelPayment> cancelPayment)
        {
            try
            {
                GetService().InsertCancelPayment(cancelPayment);
            }
            catch
            {
                throw;
            }
        }

        #endregion

    }
}
