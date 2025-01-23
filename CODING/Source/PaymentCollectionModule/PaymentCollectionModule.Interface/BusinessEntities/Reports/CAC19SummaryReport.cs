using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Reports
{
    [DataContract, Serializable]
    public class CAC19SummaryReport
    {
        private string _cashierId;
        private string _cashierName;
        private int _bpmCount;
        private decimal _bpmGAmount;

        private int _bankCount;
        private decimal _bankGAmount;
        private string _note;

        private int _rowNumber;

        [DataMember(Order = 1)]
        public string CashierId
        {
            get { return _cashierId; }
            set { _cashierId = value; }
        }

        [DataMember(Order = 2)]
        public string ControllerName
        {
            get { return _cashierName; }
            set { _cashierName = value; }
        }

        [DataMember(Order = 3)]
        public int BPMCount
        {
            get { return _bpmCount; }
            set { _bpmCount = value; }
        }

        [DataMember(Order = 4)]
        public decimal BPMTotGAmount
        {
            get { return _bpmGAmount; }
            set { _bpmGAmount = value; }
        }

        [DataMember(Order = 5)]
        public int BankCount
        {
            get { return _bankCount; }
            set { _bankCount = value; }
        }

        [DataMember(Order = 6)]
        public decimal BankTotGAmount
        {
            get { return _bankGAmount; }
            set { _bankGAmount = value; }
        }

        [DataMember(Order = 7)]
        public string Note
        {
            get { return _note; }
            set { _note = value; }
        }

        [DataMember(Order = 8)]
        public int RowNumber
        {
            get { return _rowNumber; }
            set { _rowNumber = value; }
        }
    }
}
