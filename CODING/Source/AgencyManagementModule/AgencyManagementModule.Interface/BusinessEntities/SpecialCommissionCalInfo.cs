using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class SpecialCommissionCalInfo
    {
        private string _bookId;
        private decimal? _ninetyPaidPerBill = 0;
        private decimal? _ninetyNinePaidPerBill = 0;
        private decimal _percent = 0;

        private decimal? _actualAmount = 0;
        private decimal? _totalAmount = 0;
        private int _actualBillCount = 0;
        private int _totalBillCount = 0;
        private bool _allCollected;
        private decimal? _hundPercentPaid = 0;

        private decimal? _bookSpecialCommission = 0;
        private decimal? _totalBaseCommission = 0;
        
        //actual collected
        private int _homeBillCount = 0;
        private int _corpBillCount = 0;
        private int _govBillCount = 0;
        private int _govPaidCount = 0;

        [DataMember(Order=1)]
        public string BookId
        {
            set { _bookId = value; }
            get { return _bookId; }
        }


        [DataMember(Order=2)]
        public decimal Percent
        {
            set { _percent = value; }
            get { return _percent; }
        }


        [DataMember(Order=3)]
        public decimal? NinetyPaidPerBill
        {
            set { _ninetyPaidPerBill = value; }
            get { return _ninetyPaidPerBill; }
        }


        [DataMember(Order=4)]
        public decimal? NinetyNinePaidPerBill
        {
            set { _ninetyNinePaidPerBill = value; }
            get { return _ninetyNinePaidPerBill; }
        }


        [DataMember(Order=5)]
        public decimal? ActualAmount
        {
            set { _actualAmount = value; }
            get { return _actualAmount; }
        }


        [DataMember(Order=6)]
        public decimal? TotalAmount
        {
            set { _totalAmount = value; }
            get { return _totalAmount; }
        }


        [DataMember(Order=7)]
        public int ActualBillCount
        {
            set { _actualBillCount = value; }
            get { return _actualBillCount; }
        }


        [DataMember(Order=8)]
        public int TotalBillCount
        {
            set { _totalBillCount = value; }
            get { return _totalBillCount; }
        }


        [DataMember(Order=9)]
        public bool IsAllBillCollected
        {
            set { _allCollected = value; }
            get { return _allCollected; }
        }


        [DataMember(Order=10)]
        public decimal? PercentPaidOfAllBillCollected
        {
            set { _hundPercentPaid = value; }
            get { return _hundPercentPaid; }
        }


        [DataMember(Order=11)]
        public decimal? BookSpecialCommission
        {
            set { _bookSpecialCommission = value; }
            get { return _bookSpecialCommission; }
        }


        //base commssion

        [DataMember(Order=12)]
        public decimal? TotalBaseCommission
        {
            set { _totalBaseCommission = value; }
            get { return _totalBaseCommission; }
        }


        [DataMember(Order=13)]
        public int HomeBillCount
        {
            set { _homeBillCount = value; }
            get { return _homeBillCount; }
        }


        [DataMember(Order=14)]
        public int CorpBillCount
        {
            set { _corpBillCount = value; }
            get { return _corpBillCount; }
        }


        [DataMember(Order=15)]
        public int GovBillCount
        {
            set { _govBillCount = value; }
            get { return _govBillCount; }
        }


        [DataMember(Order=16)]
        public int GovPaidCount
        {            
            set { _govPaidCount = value; }
            get { return _govPaidCount; }
        }
    }
}
