using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class BillBookPrePaidInfo
    {
        string _billBookId;
        string _paymentDt;
        string _receiptId;
        decimal? _amount;


        [DataMember(Order=1)]
        public string BillBookId
        {
            get { return this._billBookId;}
            set { this._billBookId = value;}
        }

        [DataMember(Order=2)]
        public string PaymentDt
        {
            get { return this._paymentDt; }
            set { this._paymentDt = value; }
        }

        [DataMember(Order=3)]
        public string ReceiptId
        {
            get { return this._receiptId;}
            set { this._receiptId = value;}
        }

        [DataMember(Order=4)]
        public  decimal? Amount
        {
            get { return this._amount;}
            set { this._amount = value;}
        }
    }
}
