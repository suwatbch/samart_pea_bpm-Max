using System.Collections.Generic;
using PEA.BPM.Architecture.ArchitectureBS;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.BillPrintingModule.BS;
using PEA.BPM.BillPrintingModule.Interface.BusinessEntities;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;
using WCFExtras.Soap;


namespace BPMService.BLAN
{

    public class ControlWCFService : IControlWCFService
    {
        private ControlBS _bs;
        private AuthInfo authInfo = SoapHeaderHelper<AuthInfo>.GetInputHeader("AuthInfoValue");

        public ControlWCFService()
        {
            _bs = new ControlBS();
        }

        public List<DeliveryPlace> GetDeliveryPlace(string createBranchId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "ControlWCFService", "GetDeliveryPlace",
                () =>
                {
                    return _bs.GetDeliveryPlace(createBranchId);
                });
        }

        public string InsertDeliveryPlace(DeliveryPlace dp)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "ControlWCFService", "InsertDeliveryPlace",
                () =>
                {
                    return _bs.InsertDeliveryPlace(null, dp);
                });
        }

        public string UpdateDeliveryPlace(DeliveryPlace dp)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "ControlWCFService", "UpdateDeliveryPlace",
                () =>
                {
                    return _bs.UpdateDeliveryPlace(null, dp);
                });
        }

        public string DeleteDeliveryPlace(DeliveryPlace dp)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "ControlWCFService", "DeleteDeliveryPlace",
                () =>
                {
                    return _bs.DeleteDeliveryPlace(null, dp);
                });
        }

        public List<string> GetChildBranch(string branchId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "ControlWCFService", "GetChildBranch",
                () =>
                {
                    return _bs.GetChildBranch(branchId);
                });
        }

        public List<Bank> GetBank(string branchId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "ControlWCFService", "GetBank",
                () =>
                {
                    return _bs.GetBank(branchId);
                });
        }

        public List<Portion> GetPortion(string branchId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "ControlWCFService", "GetPortion",
                () =>
                {
                    return _bs.GetPortion(branchId);
                });
        }

        public List<AuthorizedPerson> GetApprover(string createBranchId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "ControlWCFService", "GetApprover",
                () =>
                {
                    return _bs.GetApprover(createBranchId);
                });
        }

        public string InsertApprover(AuthorizedPerson approver)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "ControlWCFService", "InsertApprover",
                () =>
                {
                    return _bs.InsertApprover(null, approver);
                });
        }

        public string UpdateApprover(AuthorizedPerson approver)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "ControlWCFService", "UpdateApprover",
                () =>
                {
                    return _bs.UpdateApprover(null, approver);
                });
        }

        public string DeleteApprover(AuthorizedPerson approver)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "ControlWCFService", "DeleteApprover",
                () =>
                {
                    return _bs.DeleteApprover(null, approver);
                });
        }

        public List<Invoice> GetContractAccountHistory(string caId, string printBranch)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "ControlWCFService", "GetContractAccountHistory",
                () =>
                {
                    return _bs.GetContractAccountHistory(caId, printBranch);
                });
        }

        public List<BarcodeMRU> GetBarcodeMRU(ManageBarcodeParam param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "ControlWCFService", "GetBarcodeMRU",
                () =>
                {
                    return _bs.GetBarcodeMRU(param);
                });
        }

        public void UpdateBarcodeMRU(BarcodeMRU param)
        {
            ServiceInspector.Instance.AuditTrail(EErrorInModule.BLAN, authInfo, "ControlWCFService", "UpdateBarcodeMRU",
                () =>
                {
                    _bs.UpdateBarcodeMRU(param);
                });
        }

    }
}
