using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class InBoundCollectionInfo
    {
        private string _billType;
        private int _billCount = 0;
        private decimal? _paidPerBill = 0;
        private decimal? _total = 0;


        [DataMember(Order=1)]
        public string BillType
        {
            get { return _billType; }
            set { _billType = value; }
        }


        [DataMember(Order=2)]
        public int BillCount
        {
            get { return _billCount; }
            set { _billCount = value; }
        }


        [DataMember(Order=3)]
        public decimal? PaidPerBill
        {
            get { return _paidPerBill; }
            set { _paidPerBill = value; }
        }


        [DataMember(Order=4)]
        public decimal? Total
        {
            get { return _total; }
            set { _total = value; }
        }

    }
}
