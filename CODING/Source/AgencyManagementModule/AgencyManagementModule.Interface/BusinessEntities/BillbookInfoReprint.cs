using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.AgencyManagementModule.Interface.Constants;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class BillbookInfoReprint
    {
        private string _branchId;
        private string _agencyName;
        private string _creatorName;
        private string _billbookId;        
        private string _period;
        private byte? _receiveCount = 0;
        private string _createDt;
        private int? _billTotalCount = 0;
        private string _bookTotalAmount;
        private string _status;
        private string _statusId;
        private int _bookSearchStatus;
        private int? _bookLot = 0;


        [DataMember(Order=1)]
        public string BranchId
        {
            set { _branchId = value; }
            get { return _branchId; }
        }

        [DataMember(Order=2)]
        public string AgencyName
        {
            set { _agencyName = value; }
            get { return _agencyName; }
        }

        [DataMember(Order=3)]
        public string CreatorName
        {
            set { _creatorName = value; }
            get { return _creatorName; }
        }


        [DataMember(Order=4)]
        public string BillBookId
        {
            set { _billbookId = value; }
            get { return _billbookId; }
        }

        //Checked TongKung
        //[DataMember(Order=5)]
        public string BillbookIdText
        {
            get 
            {
                string billbookId = _billbookId;
                string retVal = String.Empty;
                if (billbookId.Length == ModuleConfigurationNames.BillBookIdLength)
                {
                    retVal = billbookId.Substring(ModuleConfigurationNames.BranchCodeLength , ModuleConfigurationNames.BillBookLengthOnly);
                }
                else 
                {
                    retVal = billbookId;
                }
                return retVal;
            }
        }      

        [DataMember(Order=6)]
        public string Period
        {
            set { _period = value; }
            get { return _period; }
        }


        [DataMember(Order=7)]
        public byte? ReceiveCount
        {
            set { _receiveCount = value; }
            get { return _receiveCount; }
        }


        [DataMember(Order=8)]
        public string CreateDt
        {
            set { _createDt = value; }
            get { return _createDt; }
        }


        [DataMember(Order=9)]
        public int? BillTotalCount
        {
            set { _billTotalCount = value; }
            get { return _billTotalCount; }
        }


        [DataMember(Order=10)]
        public string BookTotalAmount
        {
            set { _bookTotalAmount = value; }
            get { return _bookTotalAmount; }
        }


        [DataMember(Order=11)]
        public string Status
        {
            set { _status = value; }
            get { return _status; }
        }


        [DataMember(Order=12)]
        public string StatusId
        {
            set { _statusId = value; }
            get { return _statusId; }
        }

        [DataMember(Order=13)]
        public int BookSearchStatus
        {
            set { _bookSearchStatus = value; }
            get { return _bookSearchStatus; }          
        }


        [DataMember(Order=14)]
        public int? BookLot
        {
            set { _bookLot = value; }
            get { return _bookLot; }
        }

        //Checked TongKung
        //[DataMember(Order=15)]
        public string ReceiveNo
        {
            get { return String.Format("{0}/{1}", BookLot.Value.ToString().PadLeft(2,'0'), ReceiveCount.Value.ToString().PadLeft(2,'0')); }
        }
    }
}
