using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.AgencyManagementModule.Interface.Constants;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class BillBookInfoMasterReport
    {       
        private string _billBookId;
        private int? _sumBill = 0;
        //private DateTime _printDate;        
        private string _period;
        private string _branchName;
        private string _peaSectionHeaderName;        
        private string _agencyId;
        private string _agencyName;
        private string _receiveTime;
        private int? _billMonth = 0; // 0 : This Monthly, 1 Previous month
        private decimal? _totalAsset = 0;
        private string _billReturnedDate;
        private List<BillBookDetailReportListInfo> _billBookList = new List<BillBookDetailReportListInfo>();
        private List<BillBookDetailReportListInfo> _billReportList = new List<BillBookDetailReportListInfo>();
        private string _tentativeDate;
        private string _creatorName;
        private decimal? _intownReceive = 0;
        private bool _isPrePaid;
        private string _billKeeperName;
        private decimal? _advPayAmount = 0;
        private int? _bookLot = 0;

        //Checked TongKung
        //[DataMember(Order=1)]
        public string PrintDate
        {
            get
            {
                return Session.BpmDateTime.Now.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
            }
        }

        [DataMember(Order=2)]
        public string BillBookId
        {
            get { return this._billBookId; }
            set { this._billBookId = value; }
        }


        [DataMember(Order=3)]
        public int? SumBill
        {
            get { return this._sumBill; }
            set { this._sumBill = value; }
        }
          

        [DataMember(Order=4)]
        public string BranchName
        {
            get { return this._branchName; }
            set { this._branchName = value; }
        }

        //public string PEASectionHeaderName
        //{
        //    get { return this._peaSectionHeaderName; }
        //    set { this._peaSectionHeaderName = value; }
        //}


        [DataMember(Order=5)]
        public string Period
        {
            get { return this._period; }
            set { this._period = value; }
        }


        [DataMember(Order=6)]
        public string AgencyID
        {
            get { return this._agencyId; }
            set { this._agencyId = value; }
        }


        [DataMember(Order=7)]
        public string AgencyName
        {
            get { return this._agencyName; }
            set { this._agencyName = value; }
        }


        [DataMember(Order=8)]
        public string ReceiveTime
        {
            get { return this._receiveTime; }
            set { this._receiveTime = value; }
        }


        [DataMember(Order=9)]
        public int? BillMonth
        {
            get { return this._billMonth; }
            set { this._billMonth = value; }
        }


        [DataMember(Order=10)]
        public decimal? TotalAsset
        {
            get { return this._totalAsset; }
            set { this._totalAsset = value; }
        }


        [DataMember(Order=11)]
        public string BillReturnedDate
        {
            get { return _billReturnedDate; }
            set { _billReturnedDate = value; }
        }


        [DataMember(Order=12)]
        public List<BillBookDetailReportListInfo> BillBookList
        {
            get { return this._billBookList; }
            set { this._billBookList = value; }
        }

        //Checked TongKung
        //[DataMember(Order=13)]
        public int? TotalBillReceive
        {
            get
            {
                int? retVal = 0;
                foreach (BillBookDetailReportListInfo b in BillBookList)
                {
                    retVal += b.BillCount;
                }
                return retVal;
            }
        }

        //Checked TongKung
        //[DataMember(Order=14)]
        public decimal? TotalElectricReceive
        {
            get
            {
                decimal? retVal = 0;
                foreach (BillBookDetailReportListInfo b in BillBookList)
                {
                    retVal += b.ElectricPrice;
                }
                return retVal;
            }
        }

        //Checked TongKung
        //[DataMember(Order=15)]
        public int? TotalPutBill
        {
            get
            {
                int? retVal = 0;
                foreach (BillBookDetailReportListInfo b in BillBookList)
                {
                    if (b.AbsId == AbsIdEnum.PAST)
                    {
                        retVal += b.BillCount;
                    }
                }
                return retVal;
            }
        }

        //Checked TongKung
        //[DataMember(Order=16)]
        public decimal? TotalPutBillElectric
        {
            get
            {
                decimal? retVal = 0;
                foreach (BillBookDetailReportListInfo b in BillBookList)
                {
                    if (b.AbsId == AbsIdEnum.PAST)
                    {
                        retVal += b.ElectricPrice;
                    }
                }
                return retVal;
            }
        }



        [DataMember(Order=17)]
        public string TentativeDate
        {
            get { return this._tentativeDate; }
            set { this._tentativeDate = value; }
        }

        [DataMember(Order=18)]
        public decimal? IntownReceive
        {
            get
            {
                return this._intownReceive;
            }
            set
            {
                this._intownReceive = value;
            }
        }

        [DataMember(Order=19)]
        public string CreatorName
        {
            get { return this._creatorName; }
            set { this._creatorName = value; }
        }


        [DataMember(Order=20)]
        public List<BillBookDetailReportListInfo> BillReportList
        {
            get { return this._billReportList; }
            set { this._billReportList = value; }
        }


        [DataMember(Order=21)]
        public bool IsPrePaid
        {
            get { return this._isPrePaid; }
            set { this._isPrePaid = value; }
        }


        [DataMember(Order=22)]
        public string BillKeeperName
        {
            get { return this._billKeeperName; }
            set { this._billKeeperName = value; }
        }


        [DataMember(Order=23)]
        public decimal? AdvPayAmount
        {
            get { return this._advPayAmount; }
            set { this._advPayAmount = value; }
        }


        [DataMember(Order=24)]
        public int? BookLot
        {
            get { return this._bookLot; }
            set { this._bookLot = value; }
        }
    }
}
