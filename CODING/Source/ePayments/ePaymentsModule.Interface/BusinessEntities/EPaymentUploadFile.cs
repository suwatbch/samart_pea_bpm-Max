using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.ePaymentsModule.Interface.BusinessEntities
{
    [DataContract]
    public class EPaymentUploadFile
    {
        private EPayUpload _ePayUpload;
        private List<EpayUploadItem> _ePayUploadItem = new List<EpayUploadItem>();
        private List<EPayClearify> _ePayClearify = new List<EPayClearify>();
        private string _posId;
        private DateTime _currentDate;

        public EPaymentUploadFile()
        {
            // Dufault Constructor
        }

        public EPaymentUploadFile(EPayUpload ePayUpload, List<EpayUploadItem> ePayUploadItem, List<EPayClearify> ePayClerify)
        {
            this._ePayUpload = ePayUpload;
            this._ePayUploadItem = ePayUploadItem;
            this._ePayClearify = ePayClerify;
        }


        [DataMember(Order=1)]
        public EPayUpload EPaymentUpload
        {
            get { return this._ePayUpload; }
            set { this._ePayUpload = value; }
        }


        [DataMember(Order=2)]
        public List<EpayUploadItem> EpaymentUploadItem
        {
            get { return this._ePayUploadItem; }
            set { this._ePayUploadItem = value; }
        }


        [DataMember(Order=3)]
        public List<EPayClearify> EPaymentClearify
        {
            get { return this._ePayClearify ; }
            set { this._ePayClearify = value; }
        }


        [DataMember(Order=4)]
        public string PosId
        {
            get { return this._posId; }
            set { this._posId = value; }
        }


        [DataMember(Order=5)]
        public DateTime CurrentDate
        {
            get { return this._currentDate; }
            set { this._currentDate = value; }
        }
    }
}
