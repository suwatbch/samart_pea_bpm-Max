using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class AgencyBillCollectionDetailReport
    {
        int _rowId;
        string _agencyId;
        string _agencyName;
        string _assetType;       
        DateTime? _signContractDt;

        decimal? _totalAsset = 0;
        decimal? _assetValue = 0;     
        decimal? _TotalValue = 0;
        decimal? _totalBillElective = 0;
        decimal? _assetLimit = 0;
        decimal? _totalCollectElective = 0;

        int? _totalBillReceive;
        int? _totalBillCollect;
        int? _billReceiveCount;


        [DataMember(Order=1)]
        public int RowId
        {
            get { return this._rowId; }
            set { this._rowId = value; }
        }
    


        [DataMember(Order=2)]
        public string AgencyId
        {
            get { return this._agencyId; }
            set { this._agencyId = value; }
        }

        [DataMember(Order=3)]
        public string AgencyName
        {
            get { return this._agencyName; }
            set { this._agencyName = value; }
        }

        [DataMember(Order=4)]
        public string AssetType
        {
            get { return this._assetType; }
            set { this._assetType = value; }
        }        


        [DataMember(Order=5)]
        public DateTime? SignContractDt
        {
            get { return this._signContractDt; }
            set { this._signContractDt = value; }
        }


        [DataMember(Order=6)]
        public decimal? TotalValue
        {
            get { return this._TotalValue; }
            set { this._TotalValue = value; }
        }

        [DataMember(Order=7)]
        public decimal? TotalAsset
        {
            get { return this._totalAsset; }
            set { this._totalAsset = value; }
        }

        [DataMember(Order=8)]
        public decimal? AssetValue
        {
            get { return this._assetValue; }
            set { this._assetValue = value; }
        }

        [DataMember(Order=9)]
        public decimal? AssetLimit
        {
            get { return this._assetLimit;  }
            set { this._assetLimit = value; }
        }      

        [DataMember(Order=10)]
        public decimal? TotalBillElective
        {
            get { return this._totalBillElective; }
            set { this._totalBillElective = value; }
        }

        [DataMember(Order=11)]
        public decimal? TotalCollectElective
        {
            get { return this._totalCollectElective; }
            set { this._totalCollectElective = value; }
        }


        [DataMember(Order=12)]
        public int? TotalBillReceive
        {
            get { return this._totalBillReceive; }
            set { this._totalBillReceive = value; }
        }        

        [DataMember(Order=13)]
        public int? TotalBillCollect
        {
            get { return this._totalBillCollect; }
            set { this._totalBillCollect = value; }
        }      

        [DataMember(Order=14)]
        public int? BillReceiveCount
        {
            get { return this._billReceiveCount; }
            set { this._billReceiveCount = value ; }
        }
    }
}
