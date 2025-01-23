using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CallingBillViewSummaryFooterInfo
    {
        private string _header;
        private int _lineCount;
        private int _billCount;
        private decimal? _totalAmount=0;


        [DataMember(Order=1)]
        public string Header
        {
            get { return _header; }
            set { _header = value; }
        }


        [DataMember(Order=2)]
        public int LineCount
        {
            get { return _lineCount; }
            set { _lineCount = value; }
        }


        [DataMember(Order=3)]
        public int BillCount
        {
            get { return _billCount; }
            set { _billCount = value; }
        }


        [DataMember(Order=4)]
        public decimal? TotalAmount
        {
            get { return _totalAmount; }
            set { _totalAmount = value; }
        }
    }
}
