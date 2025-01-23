using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class BookSearchDetail
    {
        private string _billBookId;
        private string _customerId;
        private string _name;
        private int? _totalBill;
        private decimal? _toPayAmount;


        [DataMember(Order=1)]
        public string BillBookId
        {
            get { return _billBookId; }
            set { _billBookId = value; }
        }

        //Checked TongKung
        //[DataMember(Order=2)]
        public string ShortBillBookId
        {
            get
            {
                if (_billBookId != null && _billBookId.Length == 15)
                {
                    return _billBookId.Substring(6, 9);
                }
                else
                {
                    return _billBookId;
                }
            }
        }


        [DataMember(Order=3)]
        public string CustomerId
        {
            get { return _customerId; }
            set { _customerId = value; }
        }


        [DataMember(Order=4)]
        public string CustomerName
        {
            get { return _name; }
            set { _name = value; }
        }


        [DataMember(Order=5)]
        public int? TotalBill
        {
            get { return _totalBill; }
            set { _totalBill = value; }
        }


        [DataMember(Order=6)]
        public decimal? ToPayAmount
        {
            get { return _toPayAmount; }
            set { _toPayAmount = value; }
        }
    }
}
