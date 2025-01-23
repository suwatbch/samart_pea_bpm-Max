using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    /// <summary>
    /// Use in BillBookInfoMaster Report
    /// </summary>
    /// 
    [DataContract]
    public class BillBookDetailReportListInfo
    {
        int? _billType = 0; // 0 This month 1 Previous month
        string _absId;
        string _branchId;
        string _mruId;
        int? _billCount = 0;
        string _payRepeat; // 1 : First time , 2: Repeat time, 3: none :
        decimal? _totalNet = 0;
        decimal? _electricPrice = 0;
        decimal? _ftPrice = 0;
        decimal? _vat = 0;
        decimal? _baseAmount = 0;
        bool _isAdvPaid;

        [DataMember(Order=1)]
        public int? BillType
        {
            get { return this._billType; }
            set { this._billType = value; }
        }


        [DataMember(Order=2)]
        public string AbsId
        {
            get { return this._absId; }
            set { this._absId = value; }
        }


        [DataMember(Order=3)]
        public string BranchId
        {
            get { return this._branchId; }
            set { this._branchId = value; }
        }
      

        [DataMember(Order=4)]
        public string MRUId
        {
            get { return this._mruId; }
            set { this._mruId = value; }
        }   

        [DataMember(Order=5)]
        public decimal? ElectricPrice
        {
            get { return this._electricPrice; }
            set { this._electricPrice = value; }
        }

        [DataMember(Order=6)]
        public decimal? FtPrice
        {
            get { return this._ftPrice; }
            set { this._ftPrice = value; }
        }

        [DataMember(Order=7)]
        public decimal? Vat
        {
            get { return this._vat; }
            set { this._vat = value; }
        }

        [DataMember(Order=8)]
        public int? BillCount
        {
            get { return this._billCount; }
            set { this._billCount = value; }
        }

        [DataMember(Order=9)]
        public string PayRepeat
        {
            get { return this._payRepeat; }
            set { this._payRepeat = value; }
        }

        [DataMember(Order=10)]
        public decimal? TotalNet
        {
            get 
            {
                return this._totalNet;
            }
            set
            {
                this._totalNet = value;
            }
        }


        [DataMember(Order=11)]
        public decimal? BaseAmount
        {
            get { return this._baseAmount; }
            set { this._baseAmount = value; }
        }


        [DataMember(Order=12)]
        public bool IsAdvPaid
        {
            get { return this._isAdvPaid; }
            set { this._isAdvPaid = value; }
        }
    }
}
