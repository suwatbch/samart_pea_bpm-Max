using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class SAPSearchParam
    {
        private string _caId;

        [DataMember(Order=1)]
        public string CaId
        {
            get { return _caId; }
            set { _caId = value; }
        }

        private string _caDocNo;

        [DataMember(Order=2)]
        public string CaDocNo
        {
            get { return _caDocNo; }
            set { _caDocNo = value; }
        }

        private string _invoiceNo;

        [DataMember(Order=3)]
        public string InvoiceNo
        {
            get { return _invoiceNo; }
            set { _invoiceNo = value; }
        }

        private string _posId;

        [DataMember(Order=4)]
        public string PosId
        {
            get { return _posId; }
            set { _posId = value; }
        }

        private string _UserName;

        [DataMember(Order = 5)]
        public string userName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        private string _password;

        [DataMember(Order = 6)]
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public SAPSearchParam()
        { }

        public SAPSearchParam(string caId, string caDocNo, string InvoiceNo, string posId)
        {
            _caId = caId;
            _caDocNo = caDocNo;
            _invoiceNo = InvoiceNo;
            _posId = posId;
            _UserName = userName;
            _password = Password;
        }
    }
}
