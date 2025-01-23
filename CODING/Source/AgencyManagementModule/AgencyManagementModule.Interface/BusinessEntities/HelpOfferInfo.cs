using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    //save info - name does not make sense , to change later
    [DataContract]
    public class HelpOfferInfo
    {
        private bool _printPreview;
        private decimal? _helpTransportCost=0;
        private decimal? _helpSpecialMoney=0;
        private decimal? _helpFarLand=0;
        private decimal? _totalHelp = 0;
        private decimal? _totalCommission = 0;
        private decimal? _baseCmAmount = 0;
        private decimal? _specialCmAmount = 0;
        private decimal? _invCmAmount = 0;
        private bool _overNinety = false;
        private decimal? _taxAmount = 0;
        private decimal? _vatAmount = 0;

        //save info
        private SaveCommissionInfo _saveInfo;
        private bool _enableFine = false;
        private List<FineBookInfo> _fineList = new List<FineBookInfo>();
        private decimal? _fineAmount;
        private string _runningBranchId;
        private string _modifiedBy;

        private List<string> _bookList = new List<string>();


        [DataMember(Order=1)]
        public decimal? BaseCmAmount
        {
            get { return _baseCmAmount; }
            set { _baseCmAmount = value; }
        }


        [DataMember(Order=2)]
        public decimal? SpecialCmAmount
        {
            get { return _specialCmAmount; }
            set { _specialCmAmount = value; }
        }


        [DataMember(Order=3)]
        public decimal? InvCmAmount
        {
            get { return _invCmAmount; }
            set { _invCmAmount = value; }
        }


        [DataMember(Order=4)]
        public bool OverNinety
        {
            get { return _overNinety; }
            set { _overNinety = value; }
        }


        [DataMember(Order=5)]
        public bool PrintPreview
        {
            get { return this._printPreview; }
            set { this._printPreview = value; }
        }


        [DataMember(Order=6)]
        public string RunningBranchId
        {
            get { return _runningBranchId; }
            set { _runningBranchId = value; }
        }


        [DataMember(Order=7)]
        public List<FineBookInfo> FineList
        {
            get { return _fineList; }
            set { _fineList = value; }
        }


        [DataMember(Order=8)]
        public bool EnableFine
        {
            get { return _enableFine; }
            set { _enableFine = value; }
        }


        [DataMember(Order=9)]
        public SaveCommissionInfo SaveInfo
        {
            get { return _saveInfo; }
            set { _saveInfo = value; }
        }


        [DataMember(Order=10)]
        public decimal? TotalHelp
        {
            get { return _totalHelp; }
            set { _totalHelp = value; }
        }


        [DataMember(Order=11)]
        public decimal? TotalCommission
        {
            get { return _totalCommission; }
            set { _totalCommission = value; }
        }


        [DataMember(Order=12)]
        public decimal? HelpTransport
        {
            get { return _helpTransportCost; }
            set { _helpTransportCost = value; }
        }


        [DataMember(Order=13)]
        public decimal? HelpFarLand
        {
            get { return _helpFarLand; }
            set { _helpFarLand = value; }
        }


        [DataMember(Order=14)]
        public decimal? HelpSpecialMoney
        {
            get { return _helpSpecialMoney; }
            set { _helpSpecialMoney = value; }
        }


        [DataMember(Order=15)]
        public decimal? FineAmount
        {
            get { return _fineAmount; }
            set { _fineAmount = value; }
        }


        [DataMember(Order=16)]
        public decimal? TaxAmount
        {
            get { return _taxAmount; }
            set { _taxAmount = value; }
        }

        [DataMember(Order=17)]
        public decimal? VatAmount
        {
            get { return _vatAmount; }
            set { _vatAmount = value; }
        }

        [DataMember(Order=18)]
        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }


        [DataMember(Order=19)]
        public List<string> BookList
        {
            get { return _bookList; }
            set { _bookList = value; }
        }
    }
}
