using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.CommonUtilities;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using PEA.BPM.PaymentCollectionModule.Interface.Constants;
using ProtoBuf;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{

    //wait to verify by Tong Kung
    [DataContract]
    public enum BillDataStage
    {
        [EnumMember]
        Invoice,
        [EnumMember]
        Offline,
        [EnumMember]
        NewItem,
        [EnumMember]
        NewItemOneTouch
    }

    //[XmlRootAttribute(ElementName = "Bill", IsNullable = false)]
    [DataContract, Serializable]
    [ProtoInclude(84, typeof(ToBePaidBill))]
    [ProtoInclude(85, typeof(PrintingBill))]
    public class Bill : CloneBase<Bill>
    {
        private string _itemId;
        private string _invoiceNo;

        [DataMember(Order = 1)]
        public string InvoiceNo
        {
            get { return _invoiceNo; }
            set { _invoiceNo = value; }
        }

        private string _customerId;
        private string _branchId;
        private string _debtId;
        private string _description;
        private string _groupInvoiceId;
        private string _billBookId;
        private string _period;
        private DateTime? _meterReadDate;
        private decimal? _previousUnit;
        private decimal? _lastUnit;
        private decimal? _fullBaseAmount;
        private decimal? _fullFtAmount;
        private Decimal? _fullQty;
        private decimal? _fullAmount;
        private decimal? _fullVatAmount;
        private decimal? _fullGAmount;
        private decimal? _baseAmount;
        private decimal? _ftUnitPrice;
        private decimal? _ftAmount;
        private decimal? _amountExVat;
        private Decimal? _qty;
        private Decimal? _toPayQty;
        private decimal? _unitPrice;
        private string _unitTypeId;
        private string _unitTypeName;
        private string _taxCode;
        private decimal? _taxRate;
        private decimal? _vatAmount;
        private decimal? _toPayVatAmount;
        private decimal? _gAmount;
        private decimal? _toPayGAmount;
        private decimal? _leftInstallmentAmount;

        private DateTime? _dueDate;

        private string _CaTaxId;
        private string _CaTaxBranch;

        private string _subGroupInvoiceNo;
        private decimal _energyAmount;

        private string _notificationNo;
        private string _discStatusId;
        private string _isServiceEndOfTheYear;
        private string _isExpenseDuringTheYear;
        private decimal? _baseGroupTotalAmount;

        

        

        [DataMember(Order = 2)]
        public DateTime? DueDate
        {
            get { return _dueDate; }
            set { _dueDate = value; }
        }

        private DateTime? _deferralDate;

        [DataMember(Order = 3)]
        public DateTime? DeferralDate
        {
            get { return _deferralDate; }
            set { _deferralDate = value; }
        }

        private DateTime? _originalDueDate;

        [DataMember(Order = 4)]
        public DateTime? OriginalDueDate
        {
            get { return _originalDueDate; }
            set { _originalDueDate = value; }
        }

        private string _interestLockFlag;
        private string _interestKey;
        private decimal? _interestRate;
        private DateTime? _disconnectDate;
        private string _disconnectDocNo;
        private string _installmentFlag;
        private string _lastInstallmentFlag;
        private string _spotBillFlag;
        private string _cancelFlag;
        private string _modifiedFlag;

        private string _customerName;
        private string _customerAddress;
        private string _contractTypeId;
        private string _accountclass;
        private string _meterId;
        private string _rateTypeId;
        private decimal? _securityDeposit;
        private string _paymentMethodId;
        private string _paymentOrderFlag;
        private DateTime? _paymentOrderDt;
        private string _debtTypeName;
        private string _controllerId;
        private string _techBranchName;
        private string _commBranchId;
        private string _commBranchName;
        //TODO: INSTALLMENT CASE
        //private string _originalDtId;
        //private string _mainCaDoc;
        //private string _originalCaDoc;

        private DateTime? _bookCreateDt;

        private decimal? _bookTotalVatAmount;

        [DataMember(Order = 5)]
        public decimal? BookTotalVatAmount
        {
            get { return _bookTotalVatAmount; }
            set { _bookTotalVatAmount = value; }
        }

        private decimal? _bookTotalGAmount;

        [DataMember(Order = 6)]
        public decimal? BookTotalGAmount
        {
            get { return _bookTotalGAmount; }
            set { _bookTotalGAmount = value; }
        }

        private decimal? _bookPaidGAmount;

        [DataMember(Order = 7)]
        public decimal? BookPaidGAmount
        {
            get { return _bookPaidGAmount; }
            set { _bookPaidGAmount = value; }
        }

        private decimal? _bookAdvanceAmount;

        [DataMember(Order = 8)]
        public decimal? BookAdvanceAmount
        {
            get { return _bookAdvanceAmount; }
            set { _bookAdvanceAmount = value; }
        }

        private int? _bookTotalBill;

        [DataMember(Order = 9)]
        public int? BookTotalBill
        {
            get { return _bookTotalBill; }
            set { _bookTotalBill = value; }
        }

        private int? _bookTotalBillCollected;

        [DataMember(Order = 10)]
        public int? BookTotalBillCollected
        {
            get { return _bookTotalBillCollected; }
            set { _bookTotalBillCollected = value; }
        }

        private string _message;
        private string _paymentId;
        private string _arpmId;
        private BillDataStage _dataState = BillDataStage.Invoice;

        private int _uiRefId;


        [DataMember(Order = 11)]
        public int UiRefId
        {
            get { return _uiRefId; }
            set { _uiRefId = value; }
        }

        public Bill()
        {
        }


        [DataMember(Order = 12)]
        public string ItemId
        {
            get { return _itemId; }
            set { _itemId = value; }
        }


        [DataMember(Order = 13)]
        public string CustomerId
        {
            get { return _customerId; }
            set { _customerId = value; }
        }


        [DataMember(Order = 14)]
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }

        //Checked A
        //[DataMember(Order=15)]
        public string ShortDebtId
        {
            get { return _debtId.Substring(0, 5); }
        }


        [DataMember(Order = 16)]
        public string DebtId
        {
            get { return _debtId; }
            set { _debtId = value; }
        }

        public bool IsElectricDebt()
        {
            return ShortDebtId == "M0100"
                || ShortDebtId == "M0200"
                || ShortDebtId == "M9000"
                || DebtId == "M00175800" || DebtId == "M99080012"//ค่าพลังงาน
                || ShortDebtId == "M9900"  //ค่าไฟฟ้า(ยกฐานะ)
                || ShortDebtId == "M9901"  //ค่าไฟฟ้าปรับปรุง(ยกฐานะ)
                || DebtId == "M00110120"   //ปป.ค่าไฟ (เพิ่มหนี้)AJ
                ;
        }

        public bool IsAjDebt()
        {
            return 
                //ShortDebtId == "M9900"  //ค่าไฟฟ้า(ยกฐานะ)
                ShortDebtId == "M9901"  //ค่าไฟฟ้าปรับปรุง(ยกฐานะ)
                || DebtId == "M00110120"   //ปป.ค่าไฟ (เพิ่มหนี้)AJ
                ;
        }

        public bool IsInstallmentDebt()
        {
            return ShortDebtId == "M0080";
        }


        [DataMember(Order = 17)]
        public string Description
        {
            get
            {
                return _description;

                //switch (_debtId.Trim())
                //{
                //    case "1":
                //        string cutOffDate = (_cutOffDate==null)? "-": 
                //            string.Format("กำหนดตัดไฟ {0}", _cutOffDate.Value.ToString("dd/MM/yyyy"));
                //        return string.Format("{0} {1}", cutOffDate, _description).Trim();
                //    default:
                //        return _description;
                //}
            }
            set { _description = value; }
        }


        [DataMember(Order = 18)]
        public string GroupInvoiceId
        {
            get { return _groupInvoiceId; }
            set { _groupInvoiceId = value; }
        }


        [DataMember(Order = 19)]
        public string BillBookId
        {
            get { return _billBookId; }
            set { _billBookId = value; }
        }


        public string ShortBillBookId
        {
            get
            {
                if (_billBookId != null && _billBookId.Length == 15)
                {
                    return _billBookId.Substring(6, 9);
                }
                else
                {
                    return _billBookId;
                }
            }
        }


        [DataMember(Order = 21)]
        public string Period
        {
            get { return _period; }
            set { _period = value; }
        }


        [DataMember(Order = 22)]
        public DateTime? MeterReadDate
        {
            get { return _meterReadDate; }
            set { _meterReadDate = value; }
        }


        [DataMember(Order = 23)]
        public decimal? PreviousUnit
        {
            get { return _previousUnit; }
            set { _previousUnit = value; }
        }


        [DataMember(Order = 24)]
        public decimal? LastUnit
        {
            get { return _lastUnit; }
            set { _lastUnit = value; }
        }


        [DataMember(Order = 25)]
        public decimal? FullBaseAmount
        {
            get { return _fullBaseAmount; }
            set { _fullBaseAmount = value; }
        }


        [DataMember(Order = 26)]
        public decimal? FullFtAmount
        {
            get { return _fullFtAmount; }
            set { _fullFtAmount = value; }
        }


        [DataMember(Order = 27)]
        public Decimal? FullQty
        {
            get { return _fullQty; }
            set { _fullQty = value; }
        }


        [DataMember(Order = 28)]
        public decimal? FullAmount
        {
            get { return _fullAmount; }
            set { _fullAmount = value; }
        }


        [DataMember(Order = 29)]
        public decimal? FullVatAmount
        {
            get { return _fullVatAmount; }
            set { _fullVatAmount = value; }
        }


        [DataMember(Order = 30)]
        public decimal? FullGAmount
        {
            get { return _fullGAmount; }
            set { _fullGAmount = value; }
        }


        [DataMember(Order = 31)]
        public decimal? BaseAmount
        {
            get { return _baseAmount; }
            set { _baseAmount = value; }
        }


        [DataMember(Order = 32)]
        public decimal? FtUnitPrice
        {
            get { return _ftUnitPrice; }
            set { _ftUnitPrice = value; }
        }


        [DataMember(Order = 33)]
        public decimal? FtAmount
        {
            get { return _ftAmount; }
            set { _ftAmount = value; }
        }


        [DataMember(Order = 34)]
        public decimal? AmountExVat
        {
            get
            {
                if (null == _amountExVat)
                {
                    return _vatAmount == null ? _gAmount : _gAmount - _vatAmount;
                }
                else
                {
                    return _amountExVat;
                }
            }
            set { _amountExVat = value; }
        }


        [DataMember(Order = 35)]
        public Decimal? Qty
        {
            get { return _qty; }
            set { _qty = value; }
        }


        [DataMember(Order = 36)]
        public Decimal? ToPayQty
        {
            get { return _toPayQty; }
            set { _toPayQty = value; }
        }


        [DataMember(Order = 37)]
        public string UnitTypeId
        {
            get { return _unitTypeId; }
            set { _unitTypeId = value; }
        }


        [DataMember(Order = 38)]
        public string UnitTypeName
        {
            get
            {
                //AJ
                if (IsAjDebt())
                    _unitTypeId = "KWH";

                if (_unitTypeId == "KWH")
                {
                    return "หน่วย";
                }
                else
                {
                    return _unitTypeName;
                }
            }
            set { _unitTypeName = value; }
        }


        [DataMember(Order = 39)]
        public string TaxCode
        {
            get { return _taxCode; }
            set { _taxCode = value; }
        }


        [DataMember(Order = 40)]
        public decimal? TaxRate
        {
            get { return _taxRate; }
            set { _taxRate = value; }
        }


        [DataMember(Order = 41)]
        public decimal? VatAmount
        {
            get { return _vatAmount; }
            set { _vatAmount = value; }
        }


        [DataMember(Order = 42)]
        public decimal? ToPayVatAmount
        {
            get { return _toPayVatAmount; }
            set { _toPayVatAmount = value; }
        }


        [DataMember(Order = 43)]
        public decimal? GAmount
        {
            get { return _gAmount; }
            set { _gAmount = value; }
        }


        [DataMember(Order = 44)]
        public decimal? ToPayGAmount
        {
            get { return _toPayGAmount; }
            set { _toPayGAmount = value; }
        }


        [DataMember(Order = 45)]
        public decimal? LeftInstallmentAmount
        {
            get { return _leftInstallmentAmount; }
            set { _leftInstallmentAmount = value; }
        }


        [DataMember(Order = 46)]
        public string InterestLockFlag
        {
            get { return _interestLockFlag; }
            set { _interestLockFlag = value; }
        }


        [DataMember(Order = 47)]
        public string InterestKey
        {
            get { return _interestKey; }
            set { _interestKey = value; }
        }


        [DataMember(Order = 48)]
        public decimal? InterestRate
        {
            get { return _interestRate; }
            set { _interestRate = value; }
        }


        [DataMember(Order = 49)]
        public DateTime? DisConnectDate
        {
            get { return _disconnectDate; }
            set { _disconnectDate = value; }
        }


        [DataMember(Order = 50)]
        public string DisconnectDocNo
        {
            get { return _disconnectDocNo; }
            set { _disconnectDocNo = value; }
        }


        [DataMember(Order = 51)]
        public string InstallmentFlag
        {
            get { return _installmentFlag; }
            set { _installmentFlag = value; }
        }


        [DataMember(Order = 52)]
        public string LastInstallmentFlag
        {
            get { return _lastInstallmentFlag; }
            set { _lastInstallmentFlag = value; }
        }


        [DataMember(Order = 53)]
        public string SpotBillFlag
        {
            get { return _spotBillFlag; }
            set { _spotBillFlag = value; }
        }


        [DataMember(Order = 54)]
        public string CancelFlag
        {
            get { return _cancelFlag; }
            set { _cancelFlag = value; }
        }


        [DataMember(Order = 55)]
        public string ModifiedFlag
        {
            get { return _modifiedFlag; }
            set { _modifiedFlag = value; }
        }


        [DataMember(Order = 56)]
        public string Name
        {
            get { return _customerName; }
            set { _customerName = value; }
        }


        [DataMember(Order = 57)]
        public string Address
        {
            get { return _customerAddress; }
            set { _customerAddress = value; }
        }


        [DataMember(Order = 58)]
        public string ContractTypeId
        {
            get { return _contractTypeId; }
            set { _contractTypeId = value; }
        }


        [DataMember(Order = 59)]
        public string DebtType
        {
            get
            {
                switch (_debtId.Substring(0, 5))
                {
                    case "M0080":
                        return string.Format("{0} (ผ่อนชำระ)", _debtTypeName);
                    default:
                        return _debtTypeName;
                }
            }
            set { _debtTypeName = value; }
        }

  

        public string FullDescription
        {
            get
            {
                switch (_debtId.Substring(0, 5))
                {
                    case CodeNames.DebtType.Electric.Id:
                        return string.Format("ประจำเดือน {0} ประเภทอัตรา {1} {2}", PeriodString, RateTypeId, Description);
                    default:
                        return Description;
                }
            }
        }


        public string PrePrintedItemDescription
        {
            get
            {
                switch (_debtId.Substring(0, 5).Trim())
                {
                    case CodeNames.DebtType.Electric.Id:
                        return string.Format("ค่าไฟฟ้าประจำเดือน {0}\n{1}", StringConvert.FormatPeriodThai(_period), _description).Trim();
                    case CodeNames.DebtType.SecurityDeposit.Id:
                        return string.Format("{0}", _debtTypeName).Trim();
                    case CodeNames.DebtType.NonInvoiceY900.Id:
                        return string.Format("{0}\n{1}", _debtTypeName, Description).Trim();
                    case CodeNames.DebtType.NonInvoiceY905.Id:
                        return string.Format("{0}\n{1}", _debtTypeName, Description).Trim();
                    case CodeNames.DebtType.NonInvoiceY910.Id:
                        return string.Format("{0}\n{1}", _debtTypeName, Description).Trim();
                    case CodeNames.DebtType.NonInvoiceY920.Id:
                        return string.Format("{0}\n{1}", _debtTypeName, Description).Trim();
                    case CodeNames.DebtType.NonInvoiceY930.Id:
                        return string.Format("{0}\n{1}", _debtTypeName, Description).Trim();
                    case CodeNames.DebtType.NonInvoiceY990.Id:
                        return string.Format("{0}\n{1}", _debtTypeName, Description).Trim();
                    default:
                        switch (_debtId.Substring(0, 2).Trim().ToUpper())
                        {
                            case "MZ":
                                return string.Format("{0}\n{1}", _debtTypeName, Description).Trim();
                        }

                        switch (_debtId)
                        {
                            case CodeNames.DebtType.AgencyGroupInvoicing.Id:
                                //if (_energyAmount > 0)
                                //{
                                //    return string.Format("ค่าไฟฟ้าชำระตามหนังสือแจ้งค่าไฟฟ้าเลขที่\n{0}",
                                //        _groupInvoiceId, _invoiceNo);
                                //}
                                //else
                                //{
                                //    return string.Format("ค่าไฟฟ้าชำระตามหนังสือแจ้งค่าไฟฟ้าเลขที่\n{0}\nรายละเอียดตามใบแนบใบกำกับภาษี",
                                //        _groupInvoiceId, _invoiceNo);
                                //}
                                if (_isServiceEndOfTheYear=="1" )
                                {
                                    return string.Format("ค่าบริการโทรคมนาคมบนเสาไฟฟ้า\nของการไฟฟ้าส่วนภูมิภาค ประจำปี\nชำระตามหนังสือแจ้งเลขที่\n{0}\nรายละเอียดตามใบแนบใบกำกับภาษี",
                                        _groupInvoiceId, _invoiceNo); //20180424 Kanokwan.L เปลี่ยนข้อความ
                                }
                                else if (IsExpenseDuringTheYear == "1" )
                                {
                                    return string.Format("ค่าบริการโทรคมนาคมบนเสาไฟฟ้าของการไฟฟ้าส่วนภูมิภาค\nชำระตามหนังสือแจ้งเลขที่\n{0}\nรายละเอียดตามใบแนบใบกำกับภาษี",
                                        _groupInvoiceId, _invoiceNo); //20180509 Kanokwan.L เปลี่ยนข้อความ
                                }
                                else
                                {
                                    return string.Format("ค่าไฟฟ้าชำระตามหนังสือแจ้งค่าไฟฟ้าเลขที่\n{0}\nรายละเอียดตามใบแนบใบกำกับภาษี",
                                           _groupInvoiceId, _invoiceNo);
                                }
                            default:
                                return string.Format("{0}", _debtTypeName).Trim();
                        }
                }

            }
        }


        public string PrintDescription
        {
            get
            {
                switch (_debtId.Substring(0, 5).Trim())
                {
                    case CodeNames.DebtType.Electric.Id:
                        return string.Format("{0}\n{1}", _debtTypeName, _description);
                    case CodeNames.DebtType.SalesOrder.Id:
                        if (null != _description && _description.Length > 0)
                        {
                            return _description;
                        }
                        else
                        {
                            return _debtTypeName;
                        }
                    case CodeNames.DebtType.NonInvoiceY900.Id:
                        return string.Format("{0}\n{1}", _debtTypeName, _description).Trim();
                    case CodeNames.DebtType.NonInvoiceY905.Id:
                        return string.Format("{0}\n{1}", _debtTypeName, _description).Trim();
                    case CodeNames.DebtType.NonInvoiceY910.Id:
                        return string.Format("{0}\n{1}", _debtTypeName, _description).Trim();
                    case CodeNames.DebtType.NonInvoiceY920.Id:
                        return string.Format("{0}\n{1}", _debtTypeName, _description).Trim();
                    case CodeNames.DebtType.NonInvoiceY930.Id:
                        return string.Format("{0}\n{1}", _debtTypeName, _description).Trim();
                    case CodeNames.DebtType.NonInvoiceY990.Id:
                        return string.Format("{0}\n{1}", _debtTypeName, _description).Trim();
                    default:
                        switch (_debtId.Substring(0, 2).Trim().ToUpper())
                        {
                            case "MZ":
                                return string.Format("{0}\n{1}", _debtTypeName, Description).Trim();
                        }
                        switch (_debtId)
                        {
                            case CodeNames.DebtType.Interest.Id:
                                return string.Format("เรียกเก็บค่าดอกเบี้ย {0}\n{1}", PeriodString, _description);
                            case CodeNames.DebtType.AgencyGroupInvoicing.Id:
                                return string.Format("ค่าไฟฟ้า\nรายละเอียดตามใบแนบใบกำกับภาษี",
                                    _groupInvoiceId, _invoiceNo);
                            default:
                                return _debtTypeName;
                        }
                }
            }
        }


        [DataMember(Order = 63)]
        public string PaymentMethodId
        {
            get { return _paymentMethodId; }
            set { _paymentMethodId = value; }
        }


        [DataMember(Order = 64)]
        public string PaymentOrderFlag
        {
            get { return _paymentOrderFlag; }
            set { _paymentOrderFlag = value; }
        }


        [DataMember(Order = 65)]
        public DateTime? PaymentOrderDt
        {
            get { return _paymentOrderDt; }
            set { _paymentOrderDt = value; }
        }


        [DataMember(Order = 66)]
        public decimal? SecurityDeposit
        {
            get { return _securityDeposit; }
            set { _securityDeposit = value; }
        }


        public string DisplayMeterId
        {
            get
            {
                return _meterId.TrimStart('0');
            }
        }


        [DataMember(Order = 68)]
        public string MeterId
        {
            get { return _meterId; }
            set { _meterId = value; }
        }


        [DataMember(Order = 69)]
        public string RateTypeId
        {
            get { return _rateTypeId; }
            set { _rateTypeId = value; }
        }


        [DataMember(Order = 70)]
        public string PaymentId
        {
            get { return _paymentId; }
            set { _paymentId = value; }
        }


        public string PeriodString
        {
            get { return (_period == null) ? "" : StringConvert.FormatPeriod(_period); }
        }


        [DataMember(Order = 72)]
        public DateTime? BookCreateDt
        {
            get { return _bookCreateDt; }
            set { _bookCreateDt = value; }
        }


        [DataMember(Order = 73)]
        public string AccountClass
        {
            get { return _accountclass; }
            set { _accountclass = value; }
        }



        [DataMember(Order = 74)]
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }


        [DataMember(Order = 75)]
        public BillDataStage DataState
        {
            get { return _dataState; }
            set { _dataState = value; }
        }


        [DataMember(Order = 76)]
        public decimal? UnitPrice
        {
            get { return _unitPrice; }
            set { _unitPrice = value; }
        }


        [DataMember(Order = 77)]
        public string ARPmId
        {
            get { return _arpmId; }
            set { _arpmId = value; }
        }


        [DataMember(Order = 78)]
        public string ControllerId
        {
            get { return _controllerId; }
            set { _controllerId = value; }
        }


        [DataMember(Order = 79)]
        public string TechBranchName
        {
            get { return _techBranchName; }
            set { _techBranchName = value; }
        }


        [DataMember(Order = 80)]
        public string CommBranchId
        {
            get { return _commBranchId; }
            set { _commBranchId = value; }
        }


        [DataMember(Order = 81)]
        public string CommBranchName
        {
            get { return _commBranchName; }
            set { _commBranchName = value; }
        }

        [DataMember(Order = 82)]
        public string CaTaxId
        {
            get { return _CaTaxId; }
            set { _CaTaxId = value; }
        }

        [DataMember(Order = 83)]
        public string CaTaxBranch
        {
            get { return _CaTaxBranch; }
            set { _CaTaxBranch = value; }
        }

        [DataMember(Order = 86)]
        public string SubGroupInvoiceNo
        {
            get { return _subGroupInvoiceNo; }
            set { _subGroupInvoiceNo = value; }
        }

        [DataMember(Order = 87)]
        public decimal EnergyAmount
        {
            get { return _energyAmount; }
            set { _energyAmount = value; }
        }

        [DataMember(Order = 88)]
        public string NotificationNo
        {
            get { return _notificationNo; }
            set { _notificationNo = value; }
        }
        [DataMember(Order = 89)]
        public string DiscStatusId
        {
            get { return _discStatusId; }
            set { _discStatusId = value; }
        }
        [DataMember(Order = 90)]
        public string IsServiceEndOfTheYear
        {
            get { return _isServiceEndOfTheYear; }
            set { _isServiceEndOfTheYear = value; }
        }
        [DataMember(Order = 91)]
        public string IsExpenseDuringTheYear
        {
            get { return _isExpenseDuringTheYear; }
            set { _isExpenseDuringTheYear = value; }
        }
        [DataMember(Order = 92)]
        public decimal? BaseGroupTotalAmount
        {
            get { return _baseGroupTotalAmount; }
            set { _baseGroupTotalAmount = value; }
        }


        
        //TODO: INSTALLMENT CASE
        //[DataMember(Order = 84)]
        //public string OriginalDtId
        //{
        //    get { return _originalDtId; }
        //    set { _originalDtId = value; }
        //}

        //[DataMember(Order = 85)]
        //public string MainCaDoc
        //{
        //    get { return _mainCaDoc; }
        //    set { _mainCaDoc = value; }
        //}

        //[DataMember(Order = 86)]
        //public string OriginalCaDoc
        //{
        //    get { return _originalCaDoc; }
        //    set { _originalCaDoc = value; }
        //}

    }
}
