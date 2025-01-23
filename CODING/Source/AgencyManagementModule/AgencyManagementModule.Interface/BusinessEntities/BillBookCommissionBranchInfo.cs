using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
   public class BillBookCommissionBranchInfo
    {
       private string _area;
       private string _areaName;
       private string _branchId;
       private string _branchName;
       private string _cmId;
       private decimal? _baseCmAmount = 0;
       private decimal? _specialCmAmount = 0;
       private decimal? _invCmAmount = 0;
       private string _overNinety;
       private decimal? _transCost = 0;
       private decimal? _farLandHelp = 0;
       private decimal? _specialMoney = 0;
       private string _agencyId;
       private string _bookPeriod;
       private string _bpTypeId;
       private int? _totalBillCollected = 0;
       private decimal? _totalCollectedAmount = 0;
       private int? _totalBill = 0;
       private decimal? _totalBookAmount = 0;


        [DataMember(Order=1)]
       public string AreaName
       {
           get { return this._areaName; }
           set { this._areaName = value; }
       }


        [DataMember(Order=2)]
       public string Area
       {
           get { return this._area; }
           set { this._area = value; }
       }


        [DataMember(Order=3)]
       public string BranchId
       {
           get { return this._branchId; }
           set { this._branchId = value; }
       }


        [DataMember(Order=4)]
       public string BranchName
       {
           get { return this._branchName; }
           set { this._branchName = value; }
       }


        [DataMember(Order=5)]
       public string CmId
       {
           get { return this._cmId; }
           set { this._cmId = value; }
       }


        [DataMember(Order=6)]
       public decimal? BaseCmAmount
       {
           get { return this._baseCmAmount; }
           set { this._baseCmAmount = value; }
       }


        [DataMember(Order=7)]
       public decimal? SpecialCmAmount
       {
           get { return this._specialCmAmount; }
           set { this._specialCmAmount = value; }
       }


        [DataMember(Order=8)]
       public decimal? InvCmAmount
       {
           get { return this._invCmAmount; }
           set { this._invCmAmount = value; }
       }


        [DataMember(Order=9)]
       public string OverNinety
       {
           get { return this._overNinety; }
           set { this._overNinety = value; }
       }


        [DataMember(Order=10)]
       public decimal? TransCost
       {
           get { return this._transCost; }
           set { this._transCost = value; }
       }


        [DataMember(Order=11)]
       public decimal? FarLandHelp
       {
           get { return this._farLandHelp; }
           set { this._farLandHelp = value; }
       }


        [DataMember(Order=12)]
       public decimal? SpecialMoney
       {
           get { return this._specialMoney; }
           set { this._specialMoney = value; }
       }


        [DataMember(Order=13)]
       public int? TotalBillCollected
       {
           get { return this._totalBillCollected; }
           set { this._totalBillCollected = value; }
       }


        [DataMember(Order=14)]
       public decimal? TotalCollectedAmount
       {
           get { return this._totalCollectedAmount; }
           set { this._totalCollectedAmount = value; }
       }


        [DataMember(Order=15)]
       public int? TotalBill
       {
           get { return this._totalBill; }
           set { this._totalBill = value; }
       }


        [DataMember(Order=16)]
       public decimal? TotalBookAmount
       {
           get { return this._totalBookAmount; }
           set { this._totalBookAmount = value; }
       }


        [DataMember(Order=17)]
       public string AgencyId
       {
           get { return this._agencyId; }
           set { this._agencyId = value; }
       }
 

        [DataMember(Order=18)]
       public string BookPeriod
       {
           get { return this._bookPeriod; }
           set { this._bookPeriod = value; }
       }
  

        [DataMember(Order=19)]
       public string BpTypeId
       {
           get { return this._bpTypeId; }
           set { this._bpTypeId = value; }
       }


       //[DataMember(Order=20)]
       public decimal? ExtraMoney
       {
           get { return (TransCost + SpecialMoney + FarLandHelp); }
       }

    }
}
