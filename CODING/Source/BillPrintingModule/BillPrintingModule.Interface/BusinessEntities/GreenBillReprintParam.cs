using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.BillPrintingModule.Interface.BusinessEntities
{
    [DataContract]
    public class GreenBillReprintParam
    {
        private string _billPeriod;
        private string _dueDate;
        private string _bankId;
        private string[] _fromCaId;
        private string[] _toCaId;
        private int? _billType;
        private string _commBranchId;
        private string _printedBy;
        private int? _printCondition;


        [DataMember(Order=1)]
        public int? PrintCondition
        {
            get { return _printCondition; }
            set { _printCondition = value; }
        }


        [DataMember(Order=2)]
        public string CommBranchId
        {
            get { return _commBranchId; }
            set { _commBranchId = value; }
        }


        [DataMember(Order=3)]
        public string PrintedBy
        {
            get { return _printedBy; }
            set { _printedBy = value; }
        }


        [DataMember(Order=4)]
        public string[] FromCaId
        {
            get { return _fromCaId; }
            set { _fromCaId = value; }
        }


        [DataMember(Order=5)]
        public string[] ToCaId
        {
            get { return _toCaId; }
            set { _toCaId = value; }
        }


        [DataMember(Order=6)]
        public string BillPeriod
        {
            get { return _billPeriod; }
            set { _billPeriod = value; }
        }


        [DataMember(Order=7)]
        public string DueDate
        {
            get { return _dueDate; }
            set { _dueDate = value; }
        }


        [DataMember(Order=8)]
        public string BankId
        {
            get { return _bankId; }
            set { _bankId = value; }
        }


        [DataMember(Order=9)]
        public int? BillType
        {
            get { return _billType; }
            set { _billType = value; }
        }
    }
}
