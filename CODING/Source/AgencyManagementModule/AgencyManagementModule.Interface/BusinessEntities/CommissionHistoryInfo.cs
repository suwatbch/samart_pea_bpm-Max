using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CommissionHistoryInfo
    {
        private decimal? _fineAmount;
        private decimal? _transCost;
        private decimal? _farLandHelp;
        private decimal? _specialMoney;


        [DataMember(Order=1)]
        public decimal? FineAmount
        {
            set { _fineAmount = value; }
            get { return _fineAmount; }
        }


        [DataMember(Order=2)]
        public decimal? TransCost
        {
            set { _transCost = value; }
            get { return _transCost; }
        }


        [DataMember(Order=3)]
        public decimal? FarLandHelp
        {
            set { _farLandHelp = value; }
            get { return _farLandHelp; }
        }


        [DataMember(Order=4)]
        public decimal? SpecialMoney
        {
            set { _specialMoney = value; }
            get { return _specialMoney; }
        }

    }
}
