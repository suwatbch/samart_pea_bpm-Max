using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.BillPrintingModule.Interface.Services;
using PEA.BPM.BillPrintingModule.Interface.BusinessEntities;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;
using PEA.BPM.BillPrintingModule.SG;
using System.Data.Common;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.BillPrintingModule.Services
{
    [Service(typeof(IControlServices))]
    public class ControlServices : IControlServices
    {
        public ControlServices()
        {
        }

        #region Service Factory
        public IControlServices GetService()
        {
            return GetService(false);
        }

        public IControlServices GetService(bool serverService)
        {
            if (serverService || Session.Branch.OnlineConnection)
            {
                return new ControlSG(true);
            }
            else
            {
                return new ControlSG(false);
            }
        }
        #endregion

        #region "IControlService Members"

        public List<DeliveryPlace> GetDeliveryPlace(string createBranchId)
        {
            IControlServices bs = GetService();
            return bs.GetDeliveryPlace(createBranchId);
        }

        public List<AuthorizedPerson> GetApprover(string createBranchId)
        {
            IControlServices bs = GetService();
            return bs.GetApprover(createBranchId);
        }

        public string InsertDeliveryPlace(DbTransaction trans, DeliveryPlace dp)
        {
            IControlServices bs = GetService();
            return bs.InsertDeliveryPlace(null, dp);
        }

        public string InsertApprover(DbTransaction trans, AuthorizedPerson approver)
        {
            IControlServices bs = GetService();
            return bs.InsertApprover(null, approver);
        }

        public string UpdateDeliveryPlace(DbTransaction trans, DeliveryPlace dp)
        {
            IControlServices bs = GetService();
            return bs.UpdateDeliveryPlace(null, dp);
        }

        public string UpdateApprover(DbTransaction trans, AuthorizedPerson approver)
        {
            IControlServices bs = GetService();
            return bs.UpdateApprover(null, approver);
        }

        public string DeleteDeliveryPlace(DbTransaction trans, DeliveryPlace dp)
        {
            IControlServices bs = GetService();
            return bs.DeleteDeliveryPlace(null, dp);
        }

        public string DeleteApprover(DbTransaction trans, AuthorizedPerson approver)
        {
            IControlServices bs = GetService();
            return bs.DeleteApprover(null, approver);
        }

        public List<string> GetChildBranch(string branchId)
        {
            IControlServices bs = GetService();
            return bs.GetChildBranch(branchId);
        }

        public List<Bank> GetBank(string branchId)
        {
            IControlServices bs = GetService();
            return bs.GetBank(branchId);
        }

        public List<Portion> GetPortion(string branchId)
        {
            IControlServices bs = GetService();
            return bs.GetPortion(branchId);
        }

        public List<Invoice> GetContractAccountHistory(string caId, string printBranch)
        {
            IControlServices bs = GetService();
            return bs.GetContractAccountHistory(caId, printBranch);
        }

        public List<BarcodeMRU> GetBarcodeMRU(ManageBarcodeParam param)
        {
            IControlServices bs = GetService();
            return bs.GetBarcodeMRU(param);
        }

        public void UpdateBarcodeMRU(BarcodeMRU param)
        {
            IControlServices bs = GetService();
            bs.UpdateBarcodeMRU(param);
        }

        #endregion
    }
}
