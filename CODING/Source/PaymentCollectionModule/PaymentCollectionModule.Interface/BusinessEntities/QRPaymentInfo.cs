using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract, Serializable]
    public class QRPaymentInfo
    {
        public QRPaymentInfo()
        {
            status = string.Empty;

            QRStatus = false;
            QRPayStatus = false;    
        }

        [DataMember(Order = 1)]
        public string ref1 { get; set; }

        [DataMember(Order = 2)]
        public string ref2 { get; set; }

        [DataMember(Order = 3)]
        public decimal amount { get; set; }

        [DataMember(Order = 4)]
        public string status { get; set; }

        [DataMember(Order = 5)]
        public string referenceNo { get; set; }

        [DataMember(Order = 6)]
        public bool ResponseStatus { get; set; }

        [DataMember(Order = 7)]
        public string ResponseErrorMessage { get; set; }

        [DataMember(Order = 8)]
        public bool QRStatus { get; set; }

        [DataMember(Order = 9)]
        public bool QRPayStatus { get; set; }

    }
}
