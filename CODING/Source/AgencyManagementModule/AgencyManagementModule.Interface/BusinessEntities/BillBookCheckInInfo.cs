using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

using PEA.BPM.AgencyManagementModule.Interface.Constants;


namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class BillBookCheckInInfo
    {
        private string _runningBranchId;
        private string _bookId; //created after saved
        private string _accountClassId;
        private int _bookType;
        private string _bookOutType;
        private string _billAgentId;
        private string _billAgentName;
        private int? _receiveCount = 0;
        private DateTime? _billPaymentDate;
        private DateTime? _paidDate;
        private int _contractType;
        private int? _billCollectCount = 0;
        private decimal? _billCollectAmount = 0;
        private decimal? _gAmount = 0;
        private decimal? _totalAmount = 0;
        private decimal? _totalVat = 0;
        private DateTime? _returnDueDate;
        private string _bookPeriod;      
        private string _bsId;
        private string _note;
        private DateTime? _modifiedDt;
        private string _modifiedBy;
        private bool _printPreview;
        private string _activeItem;
        private List<BillBookCheckinDetailInfo> _billBookDetailList = new List<BillBookCheckinDetailInfo>();

        //public string RunningBranchId
        //{
        //    get { return this._runningBranchId; }
        //    set { this._runningBranchId = value; }
        //}


        [DataMember(Order=1)]
        public string BookId
        {
            get { return this._bookId; }
            set { this._bookId = value; }
        }


        [DataMember(Order=2)]
        public string AccountClassId
        {
            get { return this._accountClassId; }
            set { this._accountClassId = value; }
        }


        [DataMember(Order=3)]
        public int BookType
        {
            get { return this._bookType; }
            set { this._bookType = value; }
        }

        //Checked TongKung
        public string BookTypeName
        {
            get
            {
                string typeName = null;
                if (_bookType == (int)BookTypeEnum.BILLBOOK)
                    typeName = "สมุดจ่ายบิล";
                else if (_bookType == (int)BookTypeEnum.GROUP_INVOICE)
                    typeName = "ใบแจ้งหนี้หน่วยงาน";
                else
                    typeName = "N/A";

                return typeName;
            }
        }

        [DataMember(Order=4)]
        public string BookOutType
        {
            get { return this._bookOutType; }
            set { this._bookOutType = value; }
        }


        [DataMember(Order=5)]
        public string BillAgentId
        {
            get { return this._billAgentId; }
            set { this._billAgentId = value; }
        }


        [DataMember(Order=6)]
        public string BillAgentName
        {
            get { return this._billAgentName; }
            set { this._billAgentName = value; }
        }

        //Checked TongKung
        public string AgentCompoundName
        {
            get { return string.Format("{0}: {1}", _billAgentId, _billAgentName); }
        }

        //pay date - the day that agent comes to PEA and give all the bill and money to PEA

        [DataMember(Order=7)]
        public DateTime? PaidDate
        {
            get { return this._paidDate; }
            set { this._paidDate = value; }
        }


        [DataMember(Order=8)]
        public int? ReceiveCount
        {
            get { return this._receiveCount; }
            set { this._receiveCount = value; }
        }


        [DataMember(Order=9)]
        public DateTime? BillPaymentDate
        {
            get { return this._billPaymentDate; }
            set { this._billPaymentDate = value; }
        }

      


        [DataMember(Order=10)]
        public int ContractType
        {
            get { return this._contractType; }
            set { this._contractType = value; }
        }

        //Checked TongKung
        public int? BillCollectCount
        {
            get
            {
                _billCollectCount = 0;

                foreach (BillBookCheckinDetailInfo b in _billBookDetailList)
                {
                    if (b.AbsId == AbsIdEnum.COLLECTED)
                    {
                        this._billCollectCount = this._billCollectCount + 1;
                    }
                }
                return this._billCollectCount;
            }
        }

        //Checked TongKung
        public decimal? BillCollectAmount
        {
            get
            {
                _billCollectAmount = 0;
                foreach (BillBookCheckinDetailInfo b in _billBookDetailList)
                {
                    if (b.AbsId == AbsIdEnum.COLLECTED)
                    {
                        if (BookType == (int)BookTypeEnum.BILLBOOK)                        
                            _billCollectAmount += b.TotalAmount; 
                        else
                            _billCollectAmount += b.PaidAmount;
                    }
                }
                return _billCollectAmount;
            }
        }

        //Checked TongKung
        public decimal? TotalVat
        {
            get
            {
                _totalVat = 0;
                foreach (BillBookCheckinDetailInfo b in _billBookDetailList)
                {                    
                    _totalVat += b.Vat; ;
                }
                return _totalVat;
            }
        }

        //Checked TongKung
        public decimal? TotalAmount
        {
            get
            {
                _totalAmount = 0;
                foreach (BillBookCheckinDetailInfo b in _billBookDetailList)
                {
                    _totalAmount += b.TotalAmount;
                }
                return _totalAmount;
            }
        }

        //Checked TongKung
        public decimal? GAmount
        {
            get
            {
                _gAmount = 0;
                foreach (BillBookCheckinDetailInfo b in _billBookDetailList)
                {
                    _gAmount += b.GAmount;
                }
                return _gAmount;
            }
        }



        [DataMember(Order=11)]
        public DateTime? ReturnDueDate
        {
            get { return this._returnDueDate; }
            set { this._returnDueDate = value; }
        }

        [DataMember(Order=12)]
        public string BookPeriod
        {
            get { return this._bookPeriod; }
            set { this._bookPeriod = value; }
        }


        [DataMember(Order=13)]
        public List<BillBookCheckinDetailInfo> BillBookCheckInDetail
        {
            get { return this._billBookDetailList; }
            set { this._billBookDetailList = value; }
        }
        

        [DataMember(Order=14)]
        public string BsId
        {
            get { return this._bsId; }
            set { this._bsId = value; }
        }


        [DataMember(Order=15)]
        public string Note
        {
            get { return this._note; }
            set { this._note = value; }
            
        }


        [DataMember(Order=16)]
        public DateTime? ModifiedDt
        {
            get { return this._modifiedDt; }
            set { this._modifiedDt = value; }
        }


        [DataMember(Order=17)]
        public string ModifiedBy
        {
            get { return this._modifiedBy; }
            set { this._modifiedBy = value; }
        }


        [DataMember(Order=18)]
        public string ActiveItem
        {
            get { return this._activeItem;}
            set { this._activeItem = value; }
        }


        [DataMember(Order=19)]
        public bool PrintPreview
        {
            get { return this._printPreview; }
            set { this._printPreview = value; }
        }
    }
}
