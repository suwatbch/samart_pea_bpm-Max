using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.ePaymentsModule.Interface.BusinessEntities.ReceiptPrinting
{
    [DataContract]
    public class Bills
    {
        string _billId;
        string _billTxt;
           

        [DataMember(Order=1)]
        public string BillId
        { 
            get { return _billId; }
            set { _billId = value; }
        }


        [DataMember(Order=2)]
        public string BillTxt
        {
            get { return _billTxt; }
            set { _billTxt = value; }
        }
    }
}
