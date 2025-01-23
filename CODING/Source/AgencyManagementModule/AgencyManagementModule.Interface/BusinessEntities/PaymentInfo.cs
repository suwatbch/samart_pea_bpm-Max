using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{

    [DataContract]
    public class PaymentInfo
    {
        string _paymentId;
        DateTime? _paymentDt;
        string _posId;
        string _cashierId;
        string _branchId;      
        string _modifiedBy;


        [DataMember(Order=1)]
        public string PaymentId
        {
            get { return this._paymentId; }
            set { this._paymentId = value; }
        }


        [DataMember(Order=2)]
        public DateTime? PaymentDt
        {
            get { return this._paymentDt; }
            set { this._paymentDt = value; }
        }


        [DataMember(Order=3)]
        public string PosId
        {
            get { return this._posId; }
            set { this._posId = value; }
        }


        [DataMember(Order=4)]
        public string CashierId
        {
            get { return this._cashierId; }
            set { this._cashierId = value; }
        }


        [DataMember(Order=5)]
        public string BranchId
        {
            get { return this._branchId; }
            set { this._branchId = value; }
        }      


        [DataMember(Order=6)]
        public string ModifiedBy
        {
            get { return this._modifiedBy; }
            set { this._modifiedBy = value; }
        }
    }
}
