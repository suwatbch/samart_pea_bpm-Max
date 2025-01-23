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
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities.ReceiptPrinting;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities;

namespace PEA.BPM.ePaymentsModule.Services
{
    [Service(typeof(IReceiptPrintingService))]
    public class ReceiptPrintingService : IReceiptPrintingService
    {

        #region Service Factory
        public IReceiptPrintingService GetService()
        {
            return GetService(false);
        }

        public IReceiptPrintingService GetService(bool serverService)
        {
            if (serverService || Session.Branch.OnlineConnection)
            {
                return new ReceiptPrintingSG(true);
            }
            else
            {
                return new ReceiptPrintingSG(false);
            }
        }
        #endregion

        #region IBillPrintingService Members

        public List<Bills> PrintGreenBill(ReceiptConditionParam param)
        {
            try
            {
                return GetService().PrintGreenBill(param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region IReceiptPrintingService Members


        public List<PPrintedReceipt> GetPPrintedService(PPrintedReceiptParam param)
        {
            return GetService().GetPPrintedService(param);
        }

        #endregion

        #region IReceiptPrintingService Members


        public List<Company> GetAgentAllService()
        {
            try
            {
                return GetService().GetAgentAllService();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<EPayUpload> SearchAgentPaymentNumber(EPayUpload param)
        {
            try
            {
                return GetService().SearchAgentPaymentNumber(param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /*  Off Line   */

        public List<PPrintedDeposit> SearchDebtClearify(PPrintedDeposit param)
        {
            try
            {
                return GetService().SearchDebtClearify(param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<PPrintedDeposit> GetCADepositPPrinted(List<PPrintedDeposit> paramList)
        {
            try
            {
                return GetService().GetCADepositPPrinted(paramList);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<PPrintedDeposit> GetAgentDepositPPrinted(List<PPrintedDeposit> paramList)
        {
            try
            {
                return GetService().GetAgentDepositPPrinted(paramList);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
    }
}
