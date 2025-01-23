using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.PaymentCollectionModule.Views.SlipPrintingView.PreprintedReceipt
{
    public class Header
    {
        private string _receiptNo;
        private string _branchName;
        private string _address;
        private string _customerId;
        private string _customerName;
        private string _customerAddress;
        private string _issueDate;
        private string _invoiceId;
        private decimal? _subTotal;
        private int? _vatRate;
        private decimal? _vatAmount;
        private decimal? _totalAmount;
        private string _miscRev;
        private string _textAmount;
        private string _pageSequence;
        private string _debtId;
        private string _CaTaxId;
        private string _CaTaxBranch;
        private string _branchNameLine2;

        public string ReceiptNo
        {
            get { return _receiptNo; }
            set { _receiptNo = value; }
        }

        public string BranchName
        {
            get { return _branchName; }
            set { _branchName = value; }
        }

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        public string CustomerId
        {
            get { return _customerId; }
            set { _customerId = value; }
        }

        public string CustomerName
        {
            get { return _customerName; }
            set { _customerName = value; }
        }

        public string CustomerAddress
        {
            get { return _customerAddress; }
            set { _customerAddress = value; }
        }

        public string IssueDate
        {
            get { return _issueDate; }
            set { _issueDate = value; }
        }

        public string InvoiceId
        {
            get { return _invoiceId; }
            set { _invoiceId = value; }
        }

        public decimal? SubTotal
        {
            get { return _subTotal; }
            set { _subTotal = value; }
        }

        public int? VatRate
        {
            get { return _vatRate; }
            set { _vatRate = value; }
        }

        public decimal? VatAmount
        {
            get { return _vatAmount; }
            set { _vatAmount = value; }
        }

        public decimal? TotalAmount
        {
            get { return _totalAmount; }
            set { _totalAmount = value; }
        }

        public string MiscRev
        {
            get { return _miscRev; }
            set { _miscRev = value; }
        }

        public string TextAmount
        {
            get { return _textAmount; }
            set { _textAmount = value; }
        }

        public string PageSequence
        {
            get { return _pageSequence; }
            set { _pageSequence = value; }
        }

        public string DebtId
        {
            get { return _debtId; }
            set { _debtId = value; }
        }

        public string CaTaxId
        {
            get { return _CaTaxId; }
            set { _CaTaxId = value; }
        }

        public string CaTaxBranch
        {
            get { return _CaTaxBranch; }
            set { _CaTaxBranch = value; }
        }

        public string CaTaxText
        {
            get {

                
                if (_CaTaxId == null)
                {
                    _CaTaxId = "";
                }

                if (_CaTaxBranch == null)
                {
                    _CaTaxBranch = "";
                }
                

                if (_CaTaxId.Trim()== "" || _CaTaxId == null)
                {
                    return "";
                }
                else
                {
                    if (_CaTaxBranch.Trim() == "" || _CaTaxBranch == null)
                    {
                        //return "(เลขประจำตัวผู้เสียภาษีอากรของผู้จ่ายเงิน" + _CaTaxId.Trim() + ")";
                        return "";
                    }
                    else if (StringConvert.ToInt32(_CaTaxBranch.Trim()) == 0)
                    {
                        return "(เลขประจำตัวผู้เสียภาษีอากรของผู้จ่ายเงิน " + _CaTaxId.Trim() + " สำนักงานใหญ่)";
                    }
                    else 
                    {
                        return "(เลขประจำตัวผู้เสียภาษีอากรของผู้จ่ายเงิน  " + _CaTaxId.Trim() + " สาขา" + _CaTaxBranch.Trim() + ")";

                    }
                }
            }

        }

        public string BranchNameLine2
        {
            get { return _branchNameLine2; }
            set { _branchNameLine2 = value; }
        }
    }
}
