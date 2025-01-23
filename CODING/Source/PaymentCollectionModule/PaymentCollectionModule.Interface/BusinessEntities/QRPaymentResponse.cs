using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract, Serializable]
    public class QRPaymentResponse
    {

        [DataMember(Order = 1)]
        public QRPaymentResposeData Data { get; set; }

        [DataMember(Order = 2)]
        public bool Status { get; set; }

        [DataMember(Order = 3)]
        public string ErrorMessage { get; set; }

        [DataMember(Order = 4)]
        public string Message { get; set; }
    }

    [DataContract, Serializable]
    public class QRPaymentResposeData
    {
        [DataMember(Order = 1)]
        public bool QrStatus { get; set; }

        [DataMember(Order = 2)]
        public bool PayStatus  { get; set; }

        [DataMember(Order = 3)]
        public string PayDate { get; set; }

        [DataMember(Order = 4)]
        public string PayFromBank { get; set; }

        [DataMember(Order = 5)]
        public string PayFromName  { get; set; }

        [DataMember(Order = 6)]
        public string PayBankRef { get; set; }

    }
}
