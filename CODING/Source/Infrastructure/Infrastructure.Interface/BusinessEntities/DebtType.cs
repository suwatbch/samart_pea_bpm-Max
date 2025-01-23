using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.Infrastructure.Interface.BusinessEntities
{
    [DataContract]
    public class DebtType
    {
        private string _debtId;

        [DataMember(Order=1)]
        public string DebtId
        {
            get { return _debtId; }
            set { _debtId = value; }
        }

        private string _debtName;

        [DataMember(Order=2)]
        public string DebtName
        {
            get { return _debtName; }
            set { _debtName = value; }
        }

        private string _defultPaperSize;

        [DataMember(Order=3)]
        public string DefultPaperSize
        {
            get { return _defultPaperSize; }
            set { _defultPaperSize = value; }
        }

        private string _defaultTaxCode;

        [DataMember(Order=4)]
        public string DefaultTaxCode
        {
            get { return _defaultTaxCode; }
            set { _defaultTaxCode = value; }
        }

        private string _defaultInterestKey;

        [DataMember(Order=5)]
        public string DefaultInterestKey
        {
            get { return _defaultInterestKey; }
            set { _defaultInterestKey = value; }
        }

        private string _modReceiptHeaderFlag;

        [DataMember(Order=6)]
        public string ModReceiptHeaderFlag
        {
            get { return _modReceiptHeaderFlag; }
            set { _modReceiptHeaderFlag = value; }
        }

        private string _individualReceiptFlag;

        [DataMember(Order=7)]
        public string IndividualReceiptFlag
        {
            get { return _individualReceiptFlag; }
            set { _individualReceiptFlag = value; }
        }

        private string _nonInvoicePaymentFlag;

        [DataMember(Order=8)]
        public string NonInvoicePaymentFlag
        {
            get { return _nonInvoicePaymentFlag; }
            set { _nonInvoicePaymentFlag = value; }
        }

        private string _categoryPaymentCode;

        [DataMember(Order=9)]
        public string CategoryPaymentCode
        {
            get { return _categoryPaymentCode; }
            set { _categoryPaymentCode = value; }
        }

        private string _printDescription;

        [DataMember(Order=10)]
        public string PrintDescription
        {
            get { return _printDescription; }
            set { _printDescription = value; }
        }


        public DebtType(string debtId, string debtName, 
            string defaultPaperSize, string defaultTaxCode, string defaultInterestKey,
            string modReceiptHeaderFlag, string individualReceiptFlag, string nonInvoicePaymentFlag,
            string categoryPaymentCode, string printDescription)
        {
            _debtId = debtId;
            _debtName = debtName;            
            _defultPaperSize = defaultPaperSize;
            _defaultTaxCode = defaultTaxCode;
            _defaultInterestKey = defaultInterestKey;
            _modReceiptHeaderFlag = modReceiptHeaderFlag;
            _individualReceiptFlag = individualReceiptFlag;
            _nonInvoicePaymentFlag = nonInvoicePaymentFlag;
            _categoryPaymentCode = categoryPaymentCode;
            _printDescription = printDescription;
        }
    }
}
