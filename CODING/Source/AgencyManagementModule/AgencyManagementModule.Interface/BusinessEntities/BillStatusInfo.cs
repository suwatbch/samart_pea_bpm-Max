using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class BillStatusInfo
    {
        private string _billBookId;
        private string _invoiceNo;
        private string _period;
        private string _branchId;
        private string _mruId;
        private string _caId;
        private string _absId;
        private string _pmId;

        //add for syncup 
        private string _aboId;
        private string _allowRepeated;
        private string _inBook;
        private string _rateCatId;
        private string _paidBy;
        private decimal? _ft =0;
        private decimal? _vat = 0;
        private decimal? _totalAmount =0;
        private string _paidType;



        [DataMember(Order=1)]
        public string BillBookId
        {
            set { _billBookId = value; }
            get { return _billBookId; }
        }


        [DataMember(Order=2)]
        public string InvoiceNo
        {
            set { _invoiceNo = value; }
            get { return _invoiceNo; }
        }


        [DataMember(Order=3)]
        public string Period
        {
            set { _period = value; }
            get { return _period; }
        }


        [DataMember(Order=4)]
        public string BranchId
        {
            set { _branchId = value; }
            get { return _branchId; }
        }


        [DataMember(Order=5)]
        public string MruId
        {
            set { _mruId = value; }
            get { return _mruId; }
        }


        [DataMember(Order=6)]
        public string CaId
        {
            set { _caId = value; }
            get { return _caId; }
        }


        [DataMember(Order=7)]
        public string AbsId
        {
            set { _absId = value; }
            get { return _absId; }
        }


        [DataMember(Order=8)]
        public string PmId
        {
            set { _pmId = value; }
            get { return _pmId; }
        }


        #region For sync up only


        [DataMember(Order=9)]
        public string AboId
        {
            set { _aboId = value; }
            get { return _aboId; }
        }


        [DataMember(Order=10)]
        public string AllowRepeated
        {
            set { _allowRepeated = value; }
            get { return _allowRepeated; }
        }


        [DataMember(Order=11)]
        public string InBook
        {
            set { _inBook = value; }
            get { return _inBook; }
        }


        [DataMember(Order=12)]
        public string RateCatId
        {
            set { _rateCatId = value; }
            get { return _rateCatId; }
        }


        [DataMember(Order=13)]
        public string PaidBy
        {
            set { _paidBy = value; }
            get { return _paidBy; }
        }


        [DataMember(Order=14)]
        public decimal? Ft
        {
            set { _ft = value; }
            get { return _ft; }
        }


        [DataMember(Order=15)]
        public decimal? Vat
        {
            set { _vat = value; }
            get { return _vat; }
        }


        [DataMember(Order=16)]
        public decimal? TotalAmount
        {
            set { _totalAmount = value; }
            get { return _totalAmount; }
        }


        [DataMember(Order=17)]
        public string PaidType
        {
            set { _paidType = value; }
            get { return _paidType; }
        }

        #endregion

    }
}
