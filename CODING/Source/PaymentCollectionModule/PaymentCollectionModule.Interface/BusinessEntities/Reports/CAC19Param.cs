using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Reports
{
    [DataContract]
    public class CAC19Param : ReportParam
    {
        [DataMember(Order = 1)]
        public string BranchId;

        [DataMember(Order = 2)]
        public string PosId;

        [DataMember(Order = 3)]
        public string ControllerId;

        [DataMember(Order = 4)]
        public DateTime? TransFromDate;

        [DataMember(Order = 5)]
        public DateTime? TransToDate;

        [DataMember(Order = 6)]
        public string FileName;

        [DataMember(Order = 7)]
        public List<CAC19QRPaymentReport> BankQRPayment; 

    }
}
