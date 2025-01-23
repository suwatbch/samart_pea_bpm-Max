using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract, Serializable]
    public class QRRefundResponse
    {

        [DataMember(Order = 1)]
        public QRRefundResposeData Data { get; set; }

        [DataMember(Order = 2)]
        public bool Status { get; set; }

        [DataMember(Order = 3)]
        public string ErrorMessage { get; set; }

        [DataMember(Order = 4)]
        public string Message { get; set; }
    }

    [DataContract, Serializable]
    public class QRRefundResposeData
    {
        [DataMember(Order = 1)]
        public string QrTransactionId { get; set; }

        [DataMember(Order = 2)]
        public bool RefundStatus { get; set; }

        [DataMember(Order = 3)]
        public bool PayStatus { get; set; }
       
        [DataMember(Order = 4)]
        public string Message { get; set; }


    }
}
