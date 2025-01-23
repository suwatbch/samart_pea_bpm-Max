using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class OneTouchSearchParam
    {
        private string _notificationNo;

        [DataMember(Order=1)]
        public string NotificationNo
        {
            get { return _notificationNo; }
            set { _notificationNo = value; }
        }

        public OneTouchSearchParam()
        { 
        
        }

        public OneTouchSearchParam(string InvoiceNo)
        {
            _notificationNo = NotificationNo;
        }
    }
}
