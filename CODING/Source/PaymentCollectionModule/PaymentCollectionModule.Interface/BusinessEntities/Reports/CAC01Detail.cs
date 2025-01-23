using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Reports
{
    [DataContract]
    public class CAC01Detail
    {
        private string _mru;


        [DataMember(Order=1)]
        public string Mru
        {
            get { return _mru; }
            set { _mru = value; }
        }
        private string _billBookId;


        [DataMember(Order=2)]
        public string BillBookId
        {
            get { return _billBookId; }
            set { _billBookId = value; }
        }
    }
}
