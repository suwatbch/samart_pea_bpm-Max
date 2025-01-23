using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.EcsModule.Interface.BusinessEntities
{
    [DataContract]
    public class SearchInvoiceInfo
    {
        private string _returnType;
        private string _caId;
        private string _caName;
        private string _invoiceNo;
        private string _period;
        private string _dueDate;
        private string _amount;
        private string _remark;
        


        [DataMember(Order=1)]
        public string ReturnType
        {
            get { return this._returnType; }
            set { this._returnType = value; }
        }

        [DataMember(Order=2)]
        public string CaId
        {
            get { return this._caId; }
            set { this._caId = value; }
        }

        [DataMember(Order=3)]
        public string CaName
        {
            get { return this._caName; }
            set { this._caName = value; }
        }

        [DataMember(Order=4)]
        public string InvoiceNo
        {
            get { return this._invoiceNo; }
            set { this._invoiceNo = value; }
        }

        [DataMember(Order=5)]
        public string Period
        {
            get { return this._period; }
            set { this._period = value; }
        }

        [DataMember(Order=6)]
        public string DueDate
        {
            get { return this._dueDate; }
            set { this._dueDate = value; }
        }

        [DataMember(Order=7)]
        public string Amount
        {
            get { return this._amount; }
            set { this._amount = value; }
        }

        [DataMember(Order=8)]
        public string Remark
        {
            get { return this._remark; }
            set { this._remark = value; }
        }


    
    }
}
