using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class BookFineDetailInfo : IComparable<BookFineDetailInfo>
    {
        //by book
        private int _bookCount;
        private decimal? _bookAmount=0;
        private DateTime? _paidDate;
        private decimal? _bookAdvAmount=0;
        private decimal? _bookPaidAmount=0;
        private decimal? _bookRemainDebtAmount=0;
        private decimal? _bookAdvFineAmount=0;
        private int? _bookAdvOverdueDay = 0;
        private decimal? _returnBookFineAmount = 0;
        private int? _returnOverdueDay = 0;
        private decimal? _totalFine = 0;
        private string _receiptId;
        private string _bookId;
        private DateTimeFormatInfo _th_dt;

        public BookFineDetailInfo()
        {
            CultureInfo th_culture = new CultureInfo("th-TH");
            _th_dt = th_culture.DateTimeFormat;
        }


        [DataMember(Order=1)]
        public decimal? BookAdvAmount
        {
            set { _bookAdvAmount = value; }
            get { return _bookAdvAmount; }
        }


        [DataMember(Order=2)]
        public string BookId
        {
            set { _bookId = value; }
            get { return _bookId.Length==15?_bookId.Substring(6, 9):_bookId; }
        }


        [DataMember(Order=3)]
        public string ReceiptId
        {
            set { _receiptId = value; }
            get { return _receiptId; }
        }


        [DataMember(Order=4)]
        public int BookCount
        {
            set { _bookCount = value; }
            get { return _bookCount; }
        }


        [DataMember(Order=5)]
        public decimal? BookAmount
        {
            set { _bookAmount = value; }
            get { return _bookAmount; }
        }


        [DataMember(Order=6)]
        public DateTime? PaidDate
        {
            set { _paidDate = value; }
            get { return _paidDate; }
        }

        //Checked TongKung
        //[DataMember(Order=7)]
        public string PaidDateStr
        {
            get {
                if (_paidDate == null)
                    return "N/A";
                else
                    return _paidDate.Value.ToString("dd/MM/yyyy", _th_dt);
            }
        }    

        [DataMember(Order=8)]
        public decimal? BookPaidAmount
        {
            set { _bookPaidAmount = value; }
            get { return _bookPaidAmount; }
        }


        [DataMember(Order=9)]
        public decimal? BookRemainDebtAmount
        {
            set { _bookRemainDebtAmount = value; }
            get { return _bookRemainDebtAmount; }
        }


        [DataMember(Order=10)]
        public decimal? BookAdvFineAmount
        {
            set { _bookAdvFineAmount = value; }
            get { return _bookAdvFineAmount; }
        }


        [DataMember(Order=11)]
        public int? BookAdvOverdueDay
        {
            set { _bookAdvOverdueDay = value; }
            get { return _bookAdvOverdueDay; }
        }


        [DataMember(Order=12)]
        public decimal? ReturnBookFineAmount
        {
            set { _returnBookFineAmount = value; }
            get { return _returnBookFineAmount; }
        }


        [DataMember(Order=13)]
        public int? ReturnOverdueDay
        {
            set { _returnOverdueDay = value; }
            get { return _returnOverdueDay; }
        }


        [DataMember(Order=14)]
        public decimal? BookTotalFine
        {
            set { _totalFine = value; }
            get { return _totalFine; }
        }

        public int CompareTo(BookFineDetailInfo other)
        {
            return _paidDate.Value.CompareTo(other._paidDate);
        }
    }


    [DataContract]
    public class FineDetailInfo
    {
        private string _agentId;
        private string _period;
        private decimal? _finePerBill = 0;
        private string _createdDate;
        private decimal? _returnedInvAmount = 0;
        private bool _isCalculated = false;

        //header gv
        private decimal? _totalAmount = 0;
        private decimal? _totalAdvPayAmount = 0;
        private string _advPayDate;
        private decimal? _totalRemainDebtAmount = 0;
        private string _returnDate;

        private List<BookFineDetailInfo> _bookFineDetail;

        public FineDetailInfo()
        {
            _bookFineDetail = new List<BookFineDetailInfo>();
        }

        public void Refresh()
        {
            for(int i=0; i< _bookFineDetail.Count; i++)
            {
                _bookFineDetail[i].BookCount = i + 1;
            }
        }


        [DataMember(Order=1)]
        public List<BookFineDetailInfo> BookFineDetail
        {
            set { _bookFineDetail = value; }
            get { return _bookFineDetail; }
        }


        [DataMember(Order=2)]
        public string AgentId
        {
            set { _agentId = value; }
            get { return _agentId; }
        }


        [DataMember(Order=3)]
        public string Period
        {
            set { _period = value; }
            get { return _period; }
        }


        [DataMember(Order=4)]
        public decimal? FinePerBill
        {
            set { _finePerBill = value; }
            get { return _finePerBill; }
        }


        [DataMember(Order=5)]
        public string CreateDate
        {
            set { _createdDate = value; }
            get { return _createdDate; }
        }


        [DataMember(Order=6)]
        public decimal? ReturnedInvAmount
        {
            set { _returnedInvAmount = value; }
            get { return _returnedInvAmount; }
        }


        [DataMember(Order=7)]
        public decimal? TotalAmount
        {
            set { _totalAmount = value; }
            get { return _totalAmount; }
        }


        [DataMember(Order=8)]
        public decimal? TotalAdvPayAmount
        {
            set { _totalAdvPayAmount = value; }
            get { return _totalAdvPayAmount; }
        }


        [DataMember(Order=9)]
        public string AdvPayDate
        {
            set { _advPayDate = value; }
            get { return _advPayDate; }
        }


        [DataMember(Order=10)]
        public decimal? TotalRemainDebtAmount
        {
            set { _totalRemainDebtAmount = value; }
            get { return _totalRemainDebtAmount; }
        }


        [DataMember(Order=11)]
        public string ReturnDate
        {
            set { _returnDate = value; }
            get { return _returnDate; }
        }


        [DataMember(Order=12)]
        public bool IsCalculated
        {
            get { return  _isCalculated; }
            set { _isCalculated = value; }
        }      
    }
}
