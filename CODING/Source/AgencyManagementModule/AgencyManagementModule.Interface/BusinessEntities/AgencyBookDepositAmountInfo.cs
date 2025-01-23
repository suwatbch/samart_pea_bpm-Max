using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class AgencyBookDepositAmountInfo
    {
        private decimal? _agencyDeposit = 0;
        private decimal? _toalBookAmount = 0;

        public bool IsOverDeposit
        {
            get {
                if (_agencyDeposit < _toalBookAmount)
                    return true;
                else
                    return false;
            }
        }


        [DataMember(Order=1)]
        public decimal? AgencyDeposit
        {
            set { _agencyDeposit = value; }
            get { return _agencyDeposit; }
        }


        [DataMember(Order=2)]
        public decimal? TotalBookAmount
        {
            set { _toalBookAmount = value; }
            get { return _toalBookAmount; }
        }
    }
}
