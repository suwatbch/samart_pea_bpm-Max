using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.ePaymentsModule.Interface.BusinessEntities
{
    [DataContract]
    public class VerifyContractorInfo
    {
        private string _caId;
        private string _period;
        private decimal _debtAmount;


        [DataMember(Order=1)]
        public string CaId
        {
            get { return this._caId; }
            set { this._caId = value; }
        }


        [DataMember(Order=2)]
        public string Period
        {
            get { return this._period; }
            set { this._period = value; }
        }


        [DataMember(Order=3)]
        public decimal DebtAmount
        {
            get { return this._debtAmount; }
            set { this._debtAmount = value; }
        }
    }
}
