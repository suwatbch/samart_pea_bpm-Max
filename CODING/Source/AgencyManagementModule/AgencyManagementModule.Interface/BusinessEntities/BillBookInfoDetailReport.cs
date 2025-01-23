using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract, Serializable]
    public class BillBookInfoDetailReport
    {        
        private string _bookBillId;
        private string _printDate;
        private string _branchId;
        //private string _branchName;
        private string _mulId;
        private string _caId;
        private string _period;
        private decimal? _totalAmount = 0;


        [DataMember(Order=1)]
        public string BookBillId
        {
            get { return this._bookBillId;}
            set { this._bookBillId = value; }
        }


        [DataMember(Order=2)]
        public string PrintDate
        {
            get { return this._printDate; }
            set { this._printDate = value; }
        }


        [DataMember(Order=3)]
        public string BranchId
        {
            get { return this._branchId; }
            set { this._branchId = value; }
        }

        //public string BranchName
        //{
        //    get { return this._branchName; }
        //    set { this._branchName = value; }
        //}


        [DataMember(Order=4)]
        public string MRUId
        {
            get { return _mulId; }
            set { _mulId = value; }
        }


        [DataMember(Order=5)]
        public string CaId
        {
            get { return _caId ; }
            set { _caId = value; }
        }


        [DataMember(Order=6)]
        public string Period
        {
            get { return _period; }
            set { _period = value; }
        }


        [DataMember(Order=7)]
        public decimal? TotalAmount
        {
            get { return _totalAmount; }
            set { _totalAmount = value; }
        }
    }
}
