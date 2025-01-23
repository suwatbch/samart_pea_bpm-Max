using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CCommissionCalBatchInfo
    {
        private List<string> _bookList = new List<string>();
        private string _cmId;
        private DateTime? _calDt;
        private int? _calCount;
        //private string _taxCodeId;
        private decimal? _cmAmount;
        private decimal? _fineAmount;
        private decimal? _taxAmount;
        private decimal? _vatAmount;
        private decimal? _transCost;
        private decimal? _farLandHelp;
        private decimal? _specialMoney;
        private int? _rtId;


        [DataMember(Order=1)]
        public List<string> BookList
        {
            get { return _bookList; }
            set { _bookList = value; }
        }


        [DataMember(Order=2)]
        public string CmId
        {
            get { return _cmId; }
            set { _cmId = value; }
        }


        [DataMember(Order=3)]
        public DateTime? CalDt
        {
            get { return _calDt; }
            set { _calDt = value; }
        }


        [DataMember(Order=4)]
        public int? CalCount
        {
            get { return _calCount; }
            set { _calCount = value; }
        }


        [DataMember(Order=5)]
        public decimal? CmAmount
        {
            get { return _cmAmount; }
            set { _cmAmount = value; }
        }


        [DataMember(Order=6)]
        public decimal? FineAmount
        {
            get { return _fineAmount; }
            set { _fineAmount = value; }
        }


        [DataMember(Order=7)]
        public decimal? TaxAmount
        {
            get { return _taxAmount; }
            set { _taxAmount = value; }
        }


        [DataMember(Order=8)]
        public decimal? VatAmount
        {
            get { return _vatAmount; }
            set { _vatAmount = value; }
        }


        [DataMember(Order=9)]
        public decimal? TransCost
        {
            get { return _transCost; }
            set { _transCost = value; }
        }


        [DataMember(Order=10)]
        public decimal? FarLandHelp
        {
            get { return _farLandHelp; }
            set { _farLandHelp = value; }
        }


        [DataMember(Order=11)]
        public decimal? SpecialMoney
        {
            get { return _specialMoney; }
            set { _specialMoney = value; }
        }


        [DataMember(Order=12)]
        public int? RtId
        {
            get { return _rtId; }
            set { _rtId = value; }
        }

    }


}
