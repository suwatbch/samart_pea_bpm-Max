using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.BillPrintingModule.Interface.BusinessEntities
{
    [DataContract]
    public class PrintableIdByBank
    {
        int? _billType;
        int? _bankLotNo;
        string _dueDate;
        string _billPeriod;
        string _bankId;
        string _fromCaId;
        string _toCaId;
        int? _printingStatus;
        string _printDetail;
        string _caId;
        int? _lotSize;


        [DataMember(Order=1)]
        public int? LotSize
        {
            get { return _lotSize; }
            set { _lotSize = value; }
        }


        [DataMember(Order=2)]
        public int? BillType
        {
            get { return _billType; }
            set { _billType = value; }
        }


        [DataMember(Order=3)]
        public int? PrintingStatus
        {
            get { return _printingStatus; }
            set { _printingStatus = value; }
        }


        [DataMember(Order=4)]
        public string CaId  //reprint
        {
            get { return _caId; }
            set { _caId = value; }
        }
       

        [DataMember(Order=5)]
        public string ToCaId
        {
            get { return _toCaId; }
            set { _toCaId = value; }
        }


        [DataMember(Order=6)]
        public string FromCaId
        {
            get { return _fromCaId; }
            set { _fromCaId = value; }
        }        


        [DataMember(Order=7)]
        public string BankId
        {
            get { return _bankId; }
            set { _bankId = value; }
        }


        [DataMember(Order=8)]
        public string BillPeriod
        {
            get { return _billPeriod; }
            set { _billPeriod = value; }
        }


        [DataMember(Order=9)]
        public string DueDate
        {
            get { return _dueDate; }
            set { _dueDate = value; }
        }


        [DataMember(Order=10)]
        public int? BankLotNo
        {
            get { return _bankLotNo; }
            set { _bankLotNo = value; }
        }


        [DataMember(Order=11)]
        public string PrintDetail
        {
            get { return _printDetail; }
            set { _printDetail = value; }
        }
    }
}
