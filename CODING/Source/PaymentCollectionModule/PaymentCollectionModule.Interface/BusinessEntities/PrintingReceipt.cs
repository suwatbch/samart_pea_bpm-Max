using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract, Serializable]
    public class PrintingReceipt : IComparable<PrintingReceipt>
    {
        private string _prefix;
        private string _receiptId;
        private string _customerId;
        private string _customerName;
        private string _customerAddress;

        private string _branchName;

        private string _CaTaxId;
        private string _CaTaxBranch;

        private decimal _energyAmount;

        [DataMember(Order=1)]
        public string BranchName
        {
            get { return _branchName; }
            set { _branchName = value; }
        }

        private string _branchNumber;

        [DataMember(Order=2)]
        public string BranchNumber
        {
            get { return _branchNumber; }
            set { _branchNumber = value; }
        }

        private string _branchAddress;

        [DataMember(Order=3)]
        public string BranchAddress
        {
            get { return _branchAddress; }
            set { _branchAddress = value; }
        }

        private string _terminalCode;

        [DataMember(Order=4)]
        public string TerminalCode
        {
            get { return _terminalCode; }
            set { _terminalCode = value; }
        }

        private string _contractType;
        private decimal _totalAmount;
        private decimal _paidAmount;
        private decimal _changeAmount;
        private decimal _adjChangeAmount;
        private DateTime _paymentDate;
        private string _cashierId;
        private string _cashierName;
        private List<PrintingInvoice> _printingInvoices = new List<PrintingInvoice>();

        

        public PrintingReceipt()
        {
            _printingInvoices           = new List<PrintingInvoice>();
            //รวมใบเสร็จแผนผ่อน ExtReceiptId
            
        }

        public PrintingReceipt(string receiptId, string customerId, string customerName, string CaTaxId, string CaTaxBranch, string customerAddress, string contractType,
            decimal totalAmount, decimal paidAmount, decimal changeAmount, 
            decimal adjChangeAmount, DateTime paymentDate, string cashierId, string cashierName)
        {
            this._receiptId = receiptId;
            this._prefix = receiptId[0].ToString();
            this._customerId = customerId;
            this._customerName = customerName;
            this._CaTaxId = CaTaxId;
            this._CaTaxBranch = CaTaxBranch;
            this._customerAddress = customerAddress;
            this._contractType = contractType;
            this._totalAmount = totalAmount;
            this._paidAmount = paidAmount;
            this._changeAmount = changeAmount;
            this._adjChangeAmount = adjChangeAmount;
            this._paymentDate = paymentDate;
            this._cashierId = cashierId;
            this._cashierName = cashierName;

            //this._energyAmount = energyAmount;

            _printingInvoices = new List<PrintingInvoice>();            
        }

        private short _printingSequence;

        [DataMember(Order=5)]
        public short PrintingSequence
        {
            get { return _printingSequence; }
            set { _printingSequence = value; }
        }

        private short _totalReceipt;

        [DataMember(Order=6)]
        public short TotalReceipt
        {
            get { return _totalReceipt; }
            set { _totalReceipt = value; }
        }

        //Checked TongKung
        //[DataMember(Order=7)]    
        public string PrePrintedHeader
        {
            get
            {
                if (IsTaxReceipt)
                {
                    return string.Format("ใบเสร็จรับเงิน/ใบกำกับภาษี เลขที่ {0}", _receiptId);                    
                }
                else
                {
                    return string.Format("ใบเสร็จรับเงิน เลขที่ {0}", _receiptId);
                }
            }
        }

        //Checked TongKung
        //[DataMember(Order=8)]      
        public string Prefix
        {
            get { return _receiptId[0].ToString(); }
        }


        [DataMember(Order=9)]
        public string ReceiptId
        {
            get { return _receiptId; }
            set { _receiptId = value; }
        }

        //Checked TongKung
        //[DataMember(Order=10)]
        public string DisplayCaId
        {
            get
            {
                return _customerId.TrimStart('0');
            }
        }


        [DataMember(Order=11)]
        public string CustomerId
        {
            get { return _customerId; }
            set { _customerId = value; }
        }


        [DataMember(Order=12)]
        public string CustomerName
        {
            get { return _customerName; }
            set { _customerName = value; }
        }


        [DataMember(Order=13)]
        public string CustomerAddress
        {
            get { return _customerAddress; }
            set { _customerAddress = value; }
        }

        //Checked TongKung
        //[DataMember(Order=14)]
        public bool IsNameAddressModified
        {
            get
            {
                return (_customerName != _printingInvoices[0].Bills[0].Name
                            || _customerAddress != _printingInvoices[0].Bills[0].Address
                            || _CaTaxId != _printingInvoices[0].Bills[0].CaTaxId
                            || _CaTaxBranch != _printingInvoices[0].Bills[0].CaTaxBranch
                        );
            }
        }


        [DataMember(Order=15)]
        public string ContractType
        {
            get { return _contractType; }
            set { _contractType = value; }
        }


        [DataMember(Order=16)]
        public decimal TotalAmount
        {
            get { return _totalAmount; }
            set { _totalAmount = value; }
        }


        [DataMember(Order=17)]
        public decimal PaidAmount
        {
            get { return _paidAmount; }
            set { _paidAmount = value; }
        }


        [DataMember(Order=18)]
        public decimal ChangeAmount
        {
            get { return _changeAmount; }
            set { _changeAmount = value; }
        }


        [DataMember(Order=19)]
        public decimal AdjChangeAmount
        {
            get { return _adjChangeAmount; }
            set { _adjChangeAmount = value; }
        }


        [DataMember(Order=20)]
        public DateTime PaymentDate
        {
            get { return _paymentDate; }
            set { _paymentDate = value; }
        }


        [DataMember(Order=21)]
        public string CashierId
        {
            get { return _cashierId; }
            set { _cashierId = value; }
        }


        [DataMember(Order=22)]
        public string CashierName
        {
            get { return _cashierName; }
            set { _cashierName = value; }
        }

        [DataMember(Order = 23)]
        public List<PrintingInvoice> PrintingInvoices
        {
            get { return _printingInvoices; }
            set { _printingInvoices = value; }
        }



        [DataMember(Order = 24)]
        public string CaTaxId
        {
            get { return _CaTaxId; }
            set { _CaTaxId = value; }
        }

        

        [DataMember(Order = 25)]
        public string CaTaxBranch
        {
            get { return _CaTaxBranch; }
            set { _CaTaxBranch = value; }
        }

        [DataMember(Order = 26)]
        public decimal EnergyAmount
        {
            get { return _energyAmount; }
            set { _energyAmount = value; }
        }

        #region DCR รวมใบเสร็จแผนผ่อน 2021AUG (PrintingReceipt) 

        private string      _groupReceipt;
        private string      _groupReceiptPeriodText;
        private string      _groupReceiptInstallmentText;
        private decimal?    _groupReceiptQty;
        private decimal?    _groupReceiptAmount;
        private decimal?    _groupReceiptVatTotal;
        private decimal?    _groupReceiptTotal;
        private string      _groupReceiptMeterIdText;
        private string      _groupReceiptRateTypeText;
        private string      _groupReceiptIdInstallment;
        private string      _groupXReceiptId;
        private string      _groupReceiptPaymentMethodsWithPipe;
        private string      _groupReceiptPrintingSeqTextWithPipe;
        private string      _groupInvoiceDescriptionText;
        private string      _groupReceiptPaymentMethodsWithPipePOSSlip;
        
        [DataMember(Order = 27)]
        public string GroupReceiptOrNot
        {
            get { return _groupReceipt;  }
            set { _groupReceipt = value; }
        }

        [DataMember(Order = 28)]
        public string GroupReceiptPeriodText
        {
            get { return _groupReceiptPeriodText;  }
            set { _groupReceiptPeriodText = value; }
        }

        [DataMember(Order = 29)]
        public decimal? GroupReceiptQty
        {
            get { return _groupReceiptQty;  }
            set { _groupReceiptQty = value; }
        }

        [DataMember(Order = 30)]
        public decimal? GroupReceiptAmount
        {
            get { return _groupReceiptAmount;  }
            set { _groupReceiptAmount = value; }
        }

        [DataMember(Order = 31)]
        public string GroupReceiptInstallmentText
        {
            get { return _groupReceiptInstallmentText;  }
            set { _groupReceiptInstallmentText = value; }
        }

        [DataMember(Order = 32)]
        public decimal? GroupReceiptVatTotal
        {
            get { return _groupReceiptVatTotal; }
            set { _groupReceiptVatTotal = value; }
        }

        [DataMember(Order = 33)]
        public decimal? GroupReceiptTotal
        {
            get { return _groupReceiptTotal; }
            set { _groupReceiptTotal = value; }
        }

        [DataMember(Order = 34)]
        public string GroupReceiptMeterIdText
        {
            get { return _groupReceiptMeterIdText; }
            set { _groupReceiptMeterIdText = value; }
        }

        [DataMember(Order = 35)]
        public string GroupReceiptRateTypeText
        {
            get { return _groupReceiptRateTypeText; }
            set { _groupReceiptRateTypeText = value; }
        }

        [DataMember(Order = 36)]
        public string GroupReceiptIdInstallment {
            get { return _groupReceiptIdInstallment; }
            set { _groupReceiptIdInstallment = value; }
        }        
        
        [DataMember(Order = 37)]       
        public string GroupXReceiptId
        {
            get { return _groupXReceiptId;  }
            set { _groupXReceiptId = value; }
        }

        [DataMember(Order = 38)]
        public string GroupReceiptPaymentMethodsWithPipe
        {
            get { return _groupReceiptPaymentMethodsWithPipe;  }
            set { _groupReceiptPaymentMethodsWithPipe = value; }
        }

        [DataMember(Order = 39)]
        public string GroupReceiptPrintingSeqTextWithPipe
        {
            get { return _groupReceiptPrintingSeqTextWithPipe; }
            set { _groupReceiptPrintingSeqTextWithPipe = value; }
        }

        [DataMember(Order = 40)]
        public string GroupInvoiceDescriptionText
        {
            get { return _groupInvoiceDescriptionText;  }
            set { _groupInvoiceDescriptionText = value; }
        }

        [DataMember(Order = 41)]
        public string GroupReceiptPaymentMethodsWithPipePOSSlip
        {
            get { return _groupReceiptPaymentMethodsWithPipePOSSlip; }
            set { _groupReceiptPaymentMethodsWithPipePOSSlip = value; }
        }
        
        

        #endregion


        public bool IsTaxReceipt
        {
            get
            {
                Bill bill = _printingInvoices[0].Bills[0];
                return bill.TaxCode != null && bill.TaxCode[0]!='O' && bill.TaxRate != null;
            }
        }

   
        public string ReceiptType
        {
            get
            {
                if (IsTaxReceipt)
                {
                    return "1|3";
                }
                else
                {
                    return "2|4";
                }
            }
        }

        #region IComparable<PrintingReceipt> Members

        public int CompareTo(PrintingReceipt other)
        {
            return _receiptId.CompareTo(other.ReceiptId);
        }

        #endregion
    }
}
