using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    class ARPaymentInfo
    {
        int? _arPmId = 0;
        string _itemId;
        string _pmId;
        decimal _amount;
        int _cancelARPmId;
        string _modifiedBy;



        [DataMember(Order=1)]
        public int? ArPmId
        {
            get { return this._arPmId; }
            set { this._arPmId = value; }
        }


        [DataMember(Order=2)]
        public string ItemId
        {
            get { return this._itemId ; }
            set { this._itemId = value ; }
        }

        [DataMember(Order=3)]
        public string PmId
        {
            get { return this._pmId; }
            set { this._pmId = value; }
        }

        [DataMember(Order=4)]
        public decimal Amount
        {
            get { return this._amount; }
            set { this._amount = value; }
        }

        [DataMember(Order=5)]
        public int CancelARPmId
        {
            get { return this._cancelARPmId; }
            set { this._cancelARPmId = value; }
        }

        [DataMember(Order=6)]
        public string ModifiedBy
        {
            get { return this._modifiedBy; }
            set { this._modifiedBy = value; }
        }


    }
}
