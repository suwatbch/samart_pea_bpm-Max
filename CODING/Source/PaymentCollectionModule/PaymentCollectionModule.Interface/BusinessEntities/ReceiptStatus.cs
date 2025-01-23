using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract, Serializable]
    public class ReceiptStatus
    {
        string _id;

        [DataMember(Order=1)]
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        //Checked TongKung
        //[DataMember(Order=2)]
        public string IdPrefix
        {
            get { return _id[0].ToString(); }
        }

        bool _isCancelled;

        [DataMember(Order=3)]
        public bool IsCancelled
        {
            get { return _isCancelled; }
            set { _isCancelled = value; }
        }

        short _printingSequence;

        [DataMember(Order=4)]
        public short PrintingSequence
        {
            get { return _printingSequence; }
            set { _printingSequence = value; }
        }

        short _totalReceipt;

        [DataMember(Order=5)]
        public short TotalReceipt
        {
            get { return _totalReceipt; }
            set { _totalReceipt = value; }
        }
    }
}
