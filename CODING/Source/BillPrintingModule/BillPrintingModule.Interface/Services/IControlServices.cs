using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.BillPrintingModule.Interface.BusinessEntities;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;
using System.Data.Common;

namespace PEA.BPM.BillPrintingModule.Interface.Services
{
    public interface IControlServices
    {
        //Used in bill delivery report
        List<DeliveryPlace> GetDeliveryPlace(string createBranchId);
        List<AuthorizedPerson> GetApprover(string createBranchId);
        string InsertDeliveryPlace(DbTransaction trans, DeliveryPlace dp);
        string InsertApprover(DbTransaction trans, AuthorizedPerson approver);
        string UpdateDeliveryPlace(DbTransaction trans, DeliveryPlace dp);
        string UpdateApprover(DbTransaction trans, AuthorizedPerson approver);
        string DeleteDeliveryPlace(DbTransaction trans, DeliveryPlace dp);
        string DeleteApprover(DbTransaction trans, AuthorizedPerson approver);

        //Get all bank information that signed with pea
        List<Bank> GetBank(string branchId);
        List<String> GetChildBranch(string branchId);
        List<Portion> GetPortion(string branchId);
        List<Invoice> GetContractAccountHistory(string caId, string printBranch);

        //Manage Barcode
        List<BarcodeMRU> GetBarcodeMRU(ManageBarcodeParam param);
        void UpdateBarcodeMRU(BarcodeMRU param);
    }
}
