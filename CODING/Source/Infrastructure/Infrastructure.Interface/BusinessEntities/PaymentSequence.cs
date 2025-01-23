using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.Infrastructure.Interface.BusinessEntities
{
    [DataContract]
    public class PaymentSequence
    {
        private string _sequence;

        [DataMember(Order=1)]
        public string Sequence
        {
            get { return _sequence; }
            set { _sequence = value; }
        }

        private string _dtId;

        [DataMember(Order=2)]
        public string DtId
        {
            get { return _dtId; }
            set { _dtId = value; }
        }


        public PaymentSequence(string sequence, string dtId)
        {
            _sequence = sequence;
            _dtId = dtId;            
        }
    }
}
