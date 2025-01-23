using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class CancelledInfo
    {
        private PrintingInfo _printingInfo;

        [DataMember(Order=1)]
        public PrintingInfo PrintingInfo
        {
            get { return _printingInfo; }
            set { _printingInfo = value; }
        }

        private List<PaidMethod> _paidMethods = new List<PaidMethod>();

        [DataMember(Order=2)]
        public List<PaidMethod> PaidMethods
        {
            get { return _paidMethods; }
            set { _paidMethods = value; }
        }

        public CancelledInfo()
        {
        }
    }
}
