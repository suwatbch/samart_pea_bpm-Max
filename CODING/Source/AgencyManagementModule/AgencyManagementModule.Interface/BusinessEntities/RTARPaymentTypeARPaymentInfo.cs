using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    class RTARPaymentTypeARPaymentInfo
    {
        string _aRPtId;
        string _aRPmId;
        decimal? _amount;
        string _modifiedBy;


        [DataMember(Order=1)]
        public string ARPtId
        {
            get { return this._aRPtId; }
            set { this._aRPtId = value; }
        }


        [DataMember(Order=2)]
        public string ARPmId
        {
            get { return this._aRPmId; }
            set { this._aRPmId = value; }
        }


        [DataMember(Order=3)]
        public decimal? Amount
        {
            get { return this._amount; }
            set { this._amount = value; }
        }


        [DataMember(Order=4)]
        public string ModifiedBy
        {
            get { return this._modifiedBy; }
            set { this._modifiedBy = value; }
        }

    }
}
