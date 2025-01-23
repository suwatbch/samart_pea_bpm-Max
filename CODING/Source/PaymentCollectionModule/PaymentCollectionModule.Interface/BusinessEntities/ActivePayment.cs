using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class ActivePayment
    {
        //debt
        private List<BusinessPartnerInfo> _bp = new List<BusinessPartnerInfo>();
        private List<ContractAccountInfo> _ca = new List<ContractAccountInfo>();
        private List<ARInfo> _ar= new List<ARInfo>();
        //private List<PayFromSAPInfo> _payFromSAP;
        private List<DisconnectionDoc> _disDoc = new List<DisconnectionDoc>();
        private List<RTDisconnectionDocCaDoc> _rtDisDoc = new List<RTDisconnectionDocCaDoc>();
        //payment
        private List<ARPaymentInfo> _arPayment = new List<ARPaymentInfo>();
        private List<RTARPaymentInfo> _rtARPayment = new List<RTARPaymentInfo>();
        private List<ARPaymentTypeInfo> _paymentType = new List<ARPaymentTypeInfo>();
        private List<PaymentInfo> _payment= new List<PaymentInfo>();
        private List<ReceiptInfo> _receipt = new List<ReceiptInfo>();
        private List<ReceiptItemInfo> _receiptItem = new List<ReceiptItemInfo>();


        [DataMember(Order=1)]
        public List<BusinessPartnerInfo> BP
        {
            get { return _bp; }
            set { _bp = value; }
        }


        [DataMember(Order=2)]
        public List<ContractAccountInfo> CA
        {
            get { return _ca; }
            set { _ca = value; }
        }


        [DataMember(Order=3)]
        public List<ARInfo> AR
        {
            get { return _ar; }
            set { _ar = value; }
        }

        //rfc uses only

        [DataMember(Order=4)]
        public List<DisconnectionDoc> DisDoc
        {
            get { return _disDoc; }
            set { _disDoc = value; }
        }

        //rfc uses only

        [DataMember(Order=5)]
        public List<RTDisconnectionDocCaDoc> RTDisDoc
        {
            get { return _rtDisDoc; }
            set { _rtDisDoc = value; }
        }


        [DataMember(Order=6)]
        public List<ARPaymentInfo> ARPayment
        {
            get { return _arPayment; }
            set { _arPayment = value; }
        }


        [DataMember(Order=7)]
        public List<RTARPaymentInfo> RTARPayment
        {
            get { return _rtARPayment; }
            set { _rtARPayment = value; }
        }


        [DataMember(Order=8)]
        public List<ARPaymentTypeInfo> PaymentType
        {
            get { return _paymentType; }
            set { _paymentType = value; }
        }


        [DataMember(Order=9)]
        public List<PaymentInfo> Payment
        {
            get { return _payment; }
            set { _payment = value; }
        }


        [DataMember(Order=10)]
        public List<ReceiptInfo> Receipt
        {
            get { return _receipt; }
            set { _receipt = value; }
        }


        [DataMember(Order=11)]
        public List<ReceiptItemInfo> ReceiptItem
        {
            get { return _receiptItem; }
            set { _receiptItem = value; }
        }


    }
}
