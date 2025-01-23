using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class PayInvoice
    {
         private List<Invoice> paidInvoices = new List<Invoice>();
         private List<PaymentMethod> paymentMethods = new List<PaymentMethod>();
         private List<PrintingReceipt> receipts = new List<PrintingReceipt>();
         private List<PrintingReceipt> groupDividualReceipts = new List<PrintingReceipt>();
         private ExternalReceipt extReceipt;
         private DateTime paymentDate;
         private string posId;
         private string terminalCode;
         private string branchId;
         private string branchName;
         private decimal? payingAmount;
         private string screenType;
         private string cashierId;
         private string cashierName;
         private string workId;
         private bool isOffline;


        [DataMember(Order=1)]
         public List<Invoice> PaidInvoices
        {
            get { return paidInvoices; }
            set { paidInvoices = value; }
        }


        [DataMember(Order=2)]
        public List<PaymentMethod> PaymentMethods
        {
            get { return paymentMethods; }
            set { paymentMethods = value; }
        }


        [DataMember(Order=3)]
        public List<PrintingReceipt> Receipts
        {
            get { return receipts; }
            set { receipts = value; }
        }

        [DataMember(Order=4)]
        public List<PrintingReceipt> GroupDividualReceipts
        {
            get { return groupDividualReceipts; }
            set { groupDividualReceipts = value; }
        }


        [DataMember(Order=5)]
        public ExternalReceipt ExtReceipt
        {
            get { return extReceipt; }
            set { extReceipt = value; }
        }


        [DataMember(Order=6)]
        public DateTime PaymentDate
        {
            get { return paymentDate; }
            set { paymentDate = value; }
        }


        [DataMember(Order=7)]
        public string PosId
        {
            get { return posId; }
            set { posId = value; }
        }


        [DataMember(Order = 8)]
        public string Terminalcode
        {
            get { return terminalCode; }
            set { terminalCode = value; }
        }


        [DataMember(Order = 9)]
        public string BranchId
        {
            get { return branchId; }
            set { branchId = value; }
        }


        [DataMember(Order = 10)]
        public string BranchName
        {
            get { return branchName; }
            set { branchName = value; }
        }


        [DataMember(Order = 11)]
        public decimal? PayingAmount
        {
            get { return payingAmount; }
            set { payingAmount = value; }
        }


        [DataMember(Order = 12)]
        public string ScreenType
        {
            get { return screenType; }
            set { screenType = value; }
        }


        [DataMember(Order = 13)]
        public string CashierId
        {
            get { return cashierId; }
            set { cashierId = value; }
        }


        [DataMember(Order = 14)]
        public string CashierName
        {
            get { return cashierName; }
            set { cashierName = value; }
        }


        [DataMember(Order = 15)]
        public string WorkId
        {
            get { return workId; }
            set { workId = value; }
        }


        [DataMember(Order = 16)]
        public bool IsOffline
        {
            get { return isOffline; }
            set { isOffline = value; }
        }


    }
}
