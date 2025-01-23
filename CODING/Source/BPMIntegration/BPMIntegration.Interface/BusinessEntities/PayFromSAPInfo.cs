using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.ArchitectureInterface.Services;
using System.Globalization;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Integration.BPMIntegration.Interface.Constants;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    public class PayFromSAPInfo : IListUtility<PayFromSAPInfo>
    {
        string _paymentDocNo;
        public string PaymentDocNo
        {
            get { return _paymentDocNo; }
            set { _paymentDocNo = value; }
        }

        string _receiptNo;
        public string ReceiptNo
        {
            get { return _receiptNo; }
            set { _receiptNo = value; }
        }

        string _invoiceNo;
        public string InvoiceNo
        {
            get { return _invoiceNo; }
            set { _invoiceNo = value; }
        }

        string _caDoc;
        public string CaDoc
        {
            get { return _caDoc; }
            set { _caDoc = value; }
        }

        string _docType;
        public string DocType
        {
            get { return _docType; }
            set { _docType = value; }
        }

        DateTime? _dueDt;
        public DateTime? DueDt
        {
            get { return _dueDt; }
            set { _dueDt = value; }
        }

        DateTime? _paymentDt;
        public DateTime? PaymentDt
        {
            get { return _paymentDt; }
            set { _paymentDt = value; }
        }

        decimal? _vatAmount;
        public decimal? VatAmount
        {
            get { return _vatAmount; }
            set { _vatAmount = value; }
        }

        decimal? _amount;
        public decimal? Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        string _cancelFlag;
        public string CancelFlag
        {
            get { return _cancelFlag; }
            set { _cancelFlag = value; }
        }

        string _action;
        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }

        #region IListUtility<PayFromSAPInfo> Members

        public string ToStream()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        IFormatProvider formatDate = CultureInfo.CreateSpecificCulture("en-US");
        private string GetString(DateTime? value)
        {
            if (value == null)
            {
                return "";
            }
            else
            {
                return value.Value.ToString("yyyy-MM-dd", formatDate);
            }
        }

        private string GetString(Decimal? value)
        {
            if (value == null)
            {
                return "";
            }
            else
            {
                return value.Value.ToString();
            }
        }

        public string ToBulkLoadString()
        {
            string[] template = new string[] {PaymentDocNo, ReceiptNo, InvoiceNo, CaDoc, DocType, GetString(DueDt), GetString(PaymentDt), // 6
                GetString(VatAmount), GetString(Amount), CancelFlag, Action}; // 4

            return string.Join("|", template);
        }

        public PayFromSAPInfo ParseExtract(string txt)
        {
            IFormatProvider formatDate = CultureInfo.CreateSpecificCulture("en-EN");
            PayFromSAPInfo p = new PayFromSAPInfo();
            string[] sp = txt.Split('|');

            int colFixed = InterfaceColumns.PayFromSAP;
            int colCounted = sp.Length - 1;
            if (colCounted != colFixed)
                throw new Exception("ไม่สามารถทำรายการได้ เนื่องจาก Columns ใน text file มีจำนวน " + colCounted.ToString() + " แต่ Columns ที่กำหนดไว้มีจำนวน " + colFixed.ToString());


            int i = 1;
            p.PaymentDocNo = sp[i++].Trim();
            p.ReceiptNo = sp[i++].Trim();
            p.InvoiceNo = sp[i++].Trim();
            p.CaDoc = sp[i++].Trim();
            p.DocType = sp[i++].Trim();
            p.DueDt = StringConvert.ToDateTime(sp[i++].Trim());
            p.PaymentDt = StringConvert.ToDateTime(sp[i++].Trim());
            p.VatAmount = StringConvert.ToDecimal(sp[i++].Trim());
            p.Amount = StringConvert.ToDecimal(sp[i++].Trim());
            p.CancelFlag = sp[i++].Trim();
            p.Action = sp[i++].Trim();
            return p;
        }

        #endregion
    }
}
