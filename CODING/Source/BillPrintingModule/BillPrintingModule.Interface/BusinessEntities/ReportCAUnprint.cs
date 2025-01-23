using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.BillPrintingModule.Interface.BusinessEntities
{
    [DataContract]
    public class ReportCAUnprint
    {
        private string _printDoc;
        private string _bankId;
        private string _bankName;
        private string _dueDate;
        private string _mtNo;
        private string _caId;
        private string _crsg;
        private string _trsg;
        private string _mruId;
        private string _pmId;
        private decimal? _totalAmt;
        private string _fileName;
        private DateTime? _modifiedDt;


        [DataMember(Order=1)]
        public string PrintDoc
        {
            set { _printDoc = value; }
            get { return _printDoc; }
        }


        [DataMember(Order=2)]
        public string BankId
        {
            set { _bankId = value; }
            get { return _bankId; }
        }


        [DataMember(Order=3)]
        public string BankName
        {
            set { _bankName = value; }
            get { return _bankName; }
        }


        [DataMember(Order=4)]
        public string DueDate
        {
            set { _dueDate = value; }
            get { return _dueDate; }
        }


        [DataMember(Order=5)]
        public string MtNo
        {
            set { _mtNo = value; }
            get { return _mtNo; }
        }


        [DataMember(Order=6)]
        public string CaId
        {
            set { _caId = value; }
            get { return _caId; }
        }


        [DataMember(Order=7)]
        public string Crsg
        {
            set { _crsg = value; }
            get { return _crsg; }
        }


        [DataMember(Order=8)]
        public string Trsg
        {
            set { _trsg = value; }
            get { return _trsg; }
        }


        [DataMember(Order=9)]
        public string MruId
        {
            set { _mruId = value; }
            get { return _mruId; }
        }


        [DataMember(Order=10)]
        public string PmId
        {
            set { _pmId = value; }
            get { return _pmId; }
        }


        [DataMember(Order=11)]
        public decimal? TotalAmt
        {
            set { _totalAmt = value; }
            get { return _totalAmt; }
        }


        [DataMember(Order=12)]
        public string FileName
        {
            set { _fileName = value; }
            get { return _fileName; }
        }


        [DataMember(Order=13)]
        public DateTime? ModifiedDt
        {
            set { _modifiedDt = value; }
            get { return _modifiedDt; }
        }

        //Checked A
        //[DataMember(Order=14)]
        public string ImportDate
        {
            get { return _modifiedDt.Value.ToString("dd/MM/yyyy HH:ss", new System.Globalization.CultureInfo("th-TH")); }
        }


    }
}
