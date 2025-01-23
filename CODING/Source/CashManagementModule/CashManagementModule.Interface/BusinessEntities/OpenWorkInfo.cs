using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.CashManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class OpenWorkInfo
    {
        string _flowId;
        decimal? _totalAmt=0;
        decimal? _cashAmt=0;
        decimal? _chequeAmt=0;

        [DataMember(Order=1)]
        public string FlowId
        {
            get { return _flowId; }
            set { _flowId = value; }
        }


        [DataMember(Order=2)]
        public decimal? TotalAmt
        {
            get { return _totalAmt; }
            set { _totalAmt = value; }
        }


        [DataMember(Order=3)]
        public decimal? CashAmt
        {
            get { return _cashAmt; }
            set { _cashAmt = value; }
        }


        [DataMember(Order=4)]
        public decimal? ChequeAmt
        {
            get { return _chequeAmt; }
            set { _chequeAmt = value; }
        }

        List<ChequeInfo> _openingCheque = new List<ChequeInfo>();


        [DataMember(Order=5)]
        public List<ChequeInfo> OpeningCheque
        {
            get { return _openingCheque; }
            set { _openingCheque = value; }
        }

        DateTime? _fromDt;

        [DataMember(Order=6)]
        public DateTime? FromDate
        {
            get { return _fromDt; }
            set { _fromDt = value; }
        }
    }

    
}
