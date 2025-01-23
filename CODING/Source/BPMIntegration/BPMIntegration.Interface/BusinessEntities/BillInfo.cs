using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [Serializable]
    public class BillInfo : IListUtility<BillInfo>
    {
        private string _caId;
        public string CaId
        {
            get { return _caId; }
            set { _caId = value; }
        }

        private string _discNo;
        public string DiscNo
        {
            get { return _discNo; }
            set { _discNo = value; }
        }

        private string _main;
        public string Main
        {
            get { return _main; }
            set { _main = value; }
        }

        private string _sub;
        public string Sub
        {
            get { return _sub; }
            set { _sub = value; }
        }

        private string _caDoc;
        public string CaDoc
        {
            get { return _caDoc; }
            set { _caDoc = value; }
        }

        private string _receiptId;
        public string ReceiptId
        {
            get { return _receiptId; }
            set { _receiptId = value; }
        }

        private decimal? _gAmount;
        public decimal? GAmount
        {
            get { return _gAmount; }
            set { _gAmount = value; }
        }

        private string _cashiertId;
        public string CashierId
        {
            get { return _cashiertId; }
            set { _cashiertId = value; }
        }

        private DateTime? _paymentDt;
        public DateTime? PaymentDt
        {
            get { return _paymentDt; }
            set { _paymentDt = value; }
        }

        private string _techBranchId;
        public string TechBranchId
        {
            get { return _techBranchId; }
            set { _techBranchId = value; }
        }

        private DateTime? _dueDt;
        public DateTime? DueDt
        {
            get { return _dueDt; }
            set { _dueDt = value; }
        }


        public string ToStream()
        {
            IFormatProvider formatDate = CultureInfo.CreateSpecificCulture("en-US");
            string[] template = { ReceiptId  == null ? string.Empty : ReceiptId, 
                                CaId == null ? string.Empty : CaId, 
                                DiscNo == null ? string.Empty : DiscNo, 
                                CaDoc == null ? string.Empty : CaDoc, 
                                Main == null ? string.Empty : Main, 
                                Sub == null ? string.Empty : Sub, 
                                GAmount == null ? string.Empty : GAmount.Value.ToString("#0.00"), 
                                CashierId == null ? string.Empty : CashierId, 
                                PaymentDt  == null ? string.Empty : PaymentDt.Value.ToString("yyyyMMdd", formatDate),
                                PaymentDt  == null ? string.Empty : PaymentDt.Value.ToString("HHmm", formatDate),
                                TechBranchId == null ? string.Empty : TechBranchId, 
                                DueDt  == null ? string.Empty : DueDt.Value.ToString("yyyyMMdd", formatDate),};
            return string.Join("\t", template);
        }

        public BillInfo ParseExtract(string txt)
        {
            throw new Exception("The method or operation is not implemented.");
        }

    }
}
