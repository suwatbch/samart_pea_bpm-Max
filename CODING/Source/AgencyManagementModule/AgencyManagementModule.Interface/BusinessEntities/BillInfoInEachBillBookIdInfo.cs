using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class BillInfoInEachBillBookIdInfo
    {
        #region "Local Variable"
        private string _billBookId;
        private string _branchId;
        private string _lineNo;
        private int?  _caCount;
        private decimal? _amount = 0;
        private decimal? _baseAmount = 0;
        private decimal? _ft = 0;
        private decimal? _vat = 0;
        private string _absId;
        private string _pmId;
        #endregion

        #region "Property"

        [DataMember(Order=1)]
        public string BillBookId
        {
            get { return this._billBookId; }
            set { this._billBookId = value; }
        }

        [DataMember(Order=2)]
        public string BranchId
        {
            get { return this._branchId; }
            set { this._branchId = value; }
        }


        [DataMember(Order=3)]
        public string LineNo
        {
            get { return _lineNo; }
            set { this._lineNo = value; }
        }

        [DataMember(Order=4)]
        public int? CaCount
        {
            get { return _caCount; }
            set { this._caCount = value; }
        }

        [DataMember(Order=5)]
        public decimal? Amount
        {
            get { return _amount; }
            set { this._amount = value; }
        }


        [DataMember(Order=6)]
        public decimal? BaseAmount
        {
            get { return _baseAmount; }
            set { this._baseAmount = value; }
        }

        [DataMember(Order=7)]
        public decimal? FT
        {
            get { return _ft; }
            set { this._ft = value; }
        }

        [DataMember(Order=8)]
        public decimal? Vat
        {
            get { return _vat; }
            set { this._vat = value; }
        }

        [DataMember(Order=9)]
        public string AbsId
        {
            get { return _absId; }
            set { this._absId = value; }
        }

        [DataMember(Order=10)]
        public string PmId
        {
            get { return this._pmId; }
            set { this._pmId = value; }
        }
        #endregion
    }
}
