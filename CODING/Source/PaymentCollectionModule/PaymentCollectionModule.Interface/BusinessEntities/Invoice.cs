using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.CommonUtilities;
using System.Globalization;
using PEA.BPM.PaymentCollectionModule.Interface.Constants;
using System.Reflection;
using System.Collections;
using PEA.BPM.PaymentCollectionModule;
using ProtoBuf;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{

    [DataContract]
    public enum NetworkMode
    {
        [EnumMember]
        LocalDatabaseServer,
        [EnumMember]
        OnlineToBpmServer
    }


    [DataContract]
    public enum InvoiceDataStage
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

    //[XmlRootAttribute(ElementName = "Invoice", IsNullable = false)]
    [DataContract,Serializable]
    [ProtoInclude(80, typeof(ToBePaidInvoice))]
    [ProtoInclude(81, typeof(PrintingInvoice))] 
    public class Invoice : CloneBase<Invoice>
    {
        private string _invoiceNo;

        [DataMember(Order=1)]
        public string InvoiceNo
        {
            get { return _invoiceNo; }
            set { _invoiceNo = value; }
        }

        /// <summary>
        /// เลขที่ Invoice จริงๆ ที่ใช้อ้างอิงใน SAP
        /// </summary>
        public string RealInvoiceNo
        {
            get
            {
                if (_invoiceNo == null)
                {
                    return "";
                }
                else if (_installmentTotalPeriod != null && _installmentTotalPeriod > 0)
                {
                    int suffix = 0;
                    if (_originalInvoiceNo != null)
                    {
                        suffix = _originalInvoiceNo.IndexOf("---");
                        if (suffix > -1)
                        {
                            return _originalInvoiceNo.Substring(0, suffix);
                        }
                        else
                        {
                            return _originalInvoiceNo;
                        }
                    }
                    else
                    {
                        suffix = _invoiceNo.IndexOf("---");
                        if (suffix > -1)
                        {
                            return _invoiceNo.Substring(0, suffix);
                        }
                        else
                        {
                            return _invoiceNo;
                        }
                    }
                }
                else if (_invoiceNo != null && _invoiceNo.Length == 22)
                {
                    int suffix = _invoiceNo.IndexOf("---");
                    if (suffix > -1)
                    {
                        return _invoiceNo.Substring(0, suffix);
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    int suffix = _invoiceNo.IndexOf("---");
                    if (suffix > -1)
                    {
                        return _invoiceNo.Substring(0, suffix);
                    }
                    else
                    {
                        return _invoiceNo;
                    }
                    
                }
            }
        }

        //Checked TongKung
        //[DataMember(Order=3)]
        public string DisplayInvoiceNo
        {
            get
            {
                if (_bills.Count > 0 && _bills[0].DebtId == "P00020001")
                {
                    return _bills[0].GroupInvoiceId;
                }
                else
                {
                    return (RealInvoiceNo == null) ? "" : RealInvoiceNo.TrimStart('0');
                }
            }
        }

        private string _paymentSequence;

        [DataMember(Order=4)]
        public string PaymentSequence
        {
            get { return _paymentSequence; }
            set { _paymentSequence = value; }
        }

        private int _rowNum;

        [DataMember(Order=5)]
        public int RowNum
        {
            get { return _rowNum; }
            set { _rowNum = value; }
        }

        private DateTime? _invoiceDate;


        [DataMember(Order=6)]
        public DateTime? InvoiceDate
        {
            //get { return _invoiceDate; }
            get
            {
                if (_installmentTotalPeriod != null && _installmentTotalPeriod > 0)
                {
                    return _originalInvoiceDt;
                }
                else
                {
                    return _invoiceDate;
                }
            }
            set { _invoiceDate = value; }
        }

        private string _branchId;

        [DataMember(Order=7)]
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }

        private string _commBranchId;

        [DataMember(Order=8)]
        public string CommBranchId
        {
            get { return _commBranchId; }
            set { _commBranchId = value; }
        }

        private string _caId;

        [DataMember(Order=9)]
        public string CaId
        {
            get { return _caId; }
            set { _caId = value; }
        }

        //Checked TongKung
        //[DataMember(Order=10)]
        public string DisplayCaId
        {
            get
            {
                return (_caId == null) ? "" : _caId.TrimStart('0');
            }
        }

        private string _bpId;

        [DataMember(Order=11)]
        public string BpId
        {
            get { return _bpId; }
            set { _bpId = value; }
        }

        private string _name;

        [DataMember(Order=12)]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _payByName;

        [DataMember(Order=13)]
        public string PayByName
        {
            get { return _payByName; }
            set { _payByName = value; }
        }

        private string _pmId;

        [DataMember(Order=14)]
        public string PmId
        {
            get { return _pmId; }
            set { _pmId = value; }
        }

        //Checked TongKung
        //[DataMember(Order=15)]
        public string ReceiptPrintName
        {
            get
            {
                if (_payByName == null || _payByName == string.Empty)
                {
                    return _name;
                }
                else
                {
                    return string.Format("{0} ชำระโดย {1}", _name, _payByName);
                }
            }
        }
        
        private string _address;

        [DataMember(Order=16)]
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        private string _groupInvoiceReceiptType;

        [DataMember(Order=17)]
        public string GroupInvoiceReceiptType
        {
            get { return _groupInvoiceReceiptType; }
            set { _groupInvoiceReceiptType = value; }
        }

        private DateTime? _dueDate;

        [DataMember(Order=18)]
        public DateTime? DueDate
        {
            get { return _dueDate; }
            set { _dueDate = value; }
        }

        //Checked TongKung
        //[DataMember(Order=19)]
        public string DisplayDueDate
        {
            get
            {
                if (null == _dueDate)
                {
                    return "";
                }
                else
                {
                    CultureInfo ci = new CultureInfo("th-TH");
                    return _dueDate.Value.ToString("dd/MM/yyyy", ci.DateTimeFormat);
                }
            }
        }

        private string _caDoc;

        [DataMember(Order=20)]
        public string CaDoc
        {
            get { return _caDoc; }
            set { _caDoc = value; }
        }

        private string _spotBillInvoiceNo;

        [DataMember(Order=21)]
        public string SpotBillInvoiceNo
        {
            get { return _spotBillInvoiceNo; }
            set { _spotBillInvoiceNo = value; }
        }

        private string _originalInvoiceNo;

        [DataMember(Order=22)]
        public string OriginalInvoiceNo
        {
            get { return _originalInvoiceNo; }
            set { _originalInvoiceNo = value; }
        }

        private DateTime? _originalInvoiceDt;

        [DataMember(Order=23)]
        public DateTime? OriginalInvoiceDt
        {
            get { return _originalInvoiceDt; }
            set { _originalInvoiceDt = value; }
        }

        private int? _installmentPeriod;

        [DataMember(Order=24)]
        public int? InstallmentPeriod
        {
            get { return _installmentPeriod; }
            set { _installmentPeriod = value; }
        }

        private int? _installmentTotalPeriod;

        [DataMember(Order=25)]
        public int? InstallmentTotalPeriod
        {
            get { return _installmentTotalPeriod; }
            set { _installmentTotalPeriod = value; }
        }
        
        #region Base Description
        private decimal? _qty;

        [DataMember(Order=26)]
        public decimal? Qty
        {
            get { return _qty; }
            set { _qty = value; }
        }

        private decimal? _amountExVat;

        [DataMember(Order=27)]
        public decimal? AmountExVat
        {
            get { return _amountExVat; }
            set { _amountExVat = value; }
        }

        private decimal? _vatAmount;

        [DataMember(Order=28)]
        public decimal? VatAmount
        {
            get { return _vatAmount; }
            set { _vatAmount = value; }
        }

        private decimal? _gAmount = 0;
        /// <summary>
        /// ต้นหนี้
        /// </summary>

        [DataMember(Order=29)]
        public decimal? GAmount
        {
            get { return _gAmount; }
            set { _gAmount = value; }
        }

        #endregion

        #region Paid Description

        private decimal? _paidQty;
        /// <summary>
        /// จำนวนที่ชำระแล้ว
        /// </summary>

        [DataMember(Order=30)]
        public decimal? PaidQty
        {
            get { return _paidQty; }
            set { _paidQty = value; }
        }

        private decimal? _paidVatAmount = 0;
        /// <summary>
        /// ภาษีที่ชำระแล้ว
        /// </summary>

        [DataMember(Order=31)]
        public decimal? PaidVatAmount
        {
            get { return _paidVatAmount; }
            set { _paidVatAmount = value; }
        }

        private decimal? _paidGAmount = 0;
        /// <summary>
        /// หนี้ที่ชำระแล้ว
        /// </summary>

        [DataMember(Order=32)]
        public decimal? PaidGAmount
        {
            get { return _paidGAmount; }
            set { _paidGAmount = value; }
        }

        #endregion
        
        #region To be paid description

        /// <summary>
        /// จำนวนหน่วยที่ต้องจ่าย (หน่วยทั้งหมด - หน่วยที่ชำระแล้ว)
        /// </summary>
        //Checked TongKung
        //[DataMember(Order=33)]
        public decimal? ToBePaidQty
        {
            get { return _qty - _paidQty; }
        }

        /// <summary>
        /// ภาษีที่ต้องจ่าย (ภาษีต้นหนี้ - ภาษีที่ชำระแล้ว)
        /// </summary>
        //Checked TongKung
        //[DataMember(Order=34)]
        public decimal? ToBePaidVatAmount
        {
            get { return _vatAmount - _paidVatAmount; }
        }

        /// <summary>
        /// จำนวนเงินที่ต้องจ่าย (ต้นหนี้ - หนี้ที่ชำระแล้ว)
        /// </summary>
        //Checked TongKung
        //[DataMember(Order=35)]
        public decimal? ToBePaidGAmount
        {
            get { return _gAmount - _paidGAmount; }
        }

        //Checked TongKung
        //[DataMember(Order=36)]
        public decimal? ToBePaidExVatAmount
        {
            get { return ToBePaidGAmount - ToBePaidVatAmount; }
        }

        #endregion

        #region To pay description

        private decimal? _toPayQty = null;

        [DataMember(Order=37)]
        public decimal? ToPayQty
        {
            get { return _toPayQty; }
            set { _toPayQty = value; }
        }

        //Checked TongKung
        //[DataMember(Order=38)]
        public decimal? ToPayExVatAmount
        {
            get { return _toPayGAmount - (_toPayVatAmount == null ? 0 : _toPayVatAmount); }
        }

        private decimal? _toPayVatAmount = null;
        /// <summary>
        /// ภาษีที่กำลังจะจ่าย
        /// </summary>

        [DataMember(Order=39)]
        public decimal? ToPayVatAmount
        {
            get { return _toPayVatAmount; }
            set { _toPayVatAmount = value; }
        }

        private decimal? _toPayGAmount = null;
        /// <summary>
        /// จำนวนเงินที่กำลังจะจ่าย
        /// </summary>

        [DataMember(Order=40)]
        public decimal? ToPayGAmount
        {
            get { return _toPayGAmount; }
            set { _toPayGAmount = value; }
        }

        private decimal? _toPayAdjAmount = 0;
        /// <summary>
        /// จำนวนเงินปัดเศษที่กำลังจะจ่าย
        /// </summary>

        [DataMember(Order=41)]
        public decimal? ToPayAdjAmount
        {
            get { return _toPayAdjAmount; }
            set { _toPayAdjAmount = value; }
        }

        /// <summary>
        /// จำนวนเงินที่จะจ่ายรวมปัดเศษ
        /// </summary>
        //Checked TongKung
        //[DataMember(Order=42)]
        public decimal? ToPayTotalAmount
        {
            get { return _toPayGAmount + _toPayAdjAmount; }
        }

        private decimal? _totalPaidAmount;
        //Checked TongKung
        //[DataMember(Order=43)]
        public decimal? TotalPaidAmount
        {
            get
            {
                _totalPaidAmount = 0;

                if (Bills.Count > 1)
                {
                    foreach (Bill b in Bills)
                    {
                        _totalPaidAmount += b.GAmount;
                    }

                    return _totalPaidAmount - _paidGAmount;
                }
                else
                {
                    if (Bills.Count > 0)
                    {
                        return Bills[0].GAmount;
                    }
                    else
                    {
                        return 0;                        
                    }
                }

            }
        }

        #endregion
        
        private List<Bill> _bills;   

        [DataMember(Order=44)]
        public List<Bill> Bills
        {
            get { return _bills; }
            set { _bills = value; }
        }

        private List<InvoicePaymentMethod> _paymentMethods = new List<InvoicePaymentMethod>();

        [DataMember(Order=45)]
        public List<InvoicePaymentMethod> PaymentMethods
        {
            get { return _paymentMethods; }
            set { _paymentMethods = value; }
        }

        //Checked TongKung
        //[DataMember(Order=46)]
        public decimal TotalPaidByPaymentMethodAmount
        {
            get
            {                
                decimal result = 0;
                foreach (InvoicePaymentMethod ivpm in _paymentMethods)
                {
                    result += ivpm.Amount;
                }

                return result;
            }
        }

        private int? _partialPayment;

        [DataMember(Order=47)]
        public int? PartialPayment
        {
            get { return _partialPayment; }
            set { _partialPayment = value; }
        }

        /// <summary>
        /// จำนวนเงินที่เหลือหลังจากกำหนด PaymentMethod
        /// </summary>
        //Checked TongKung
        //[DataMember(Order=48)]
        public decimal TotalRemainToPayAmount
        {
            get
            {
                return ToPayTotalAmount.Value - TotalPaidByPaymentMethodAmount;
            }
        }

        #region System Properties

        private string _arpmId;

        [DataMember(Order=49)]
        public string ARPmId
        {
            get { return _arpmId; }
            set { _arpmId = value; }
        }

        private string _paymentId;

        [DataMember(Order=50)]
        public string PaymentId
        {
            get { return _paymentId; }
            set { _paymentId = value; }
        }

        private InvoiceDataStage _dataState;

        [DataMember(Order=51)]
        public InvoiceDataStage DataState
        {
            get { return _dataState; }
            set { _dataState = value; }
        }

        // Defalut = Local, but if the data return from SG it'll be Online
        private NetworkMode _networkMode = NetworkMode.LocalDatabaseServer;

        [DataMember(Order=52)]
        public NetworkMode NetworkMode
        {
            get { return _networkMode; }
            set { _networkMode = value; }
        }

        private int _uiRefId;

        [DataMember(Order=53)]
        public int UiRefId
        {
            get { return _uiRefId; }
            set { _uiRefId = value; }
        }

        #endregion

        #region From Bill[0]

        //Checked TongKung
        //[DataMember(Order=54)]
        public string BillBookId
        {
            get { return _bills[0].BillBookId; }
        }

        //Checked TongKung
        //[DataMember(Order=55)]
        public string DebtType
        {
            get
            {
                if (_bills.Count > 1)
                {
                    return "++";
                }
                else if (_bills.Count == 1)
                {
                    return _bills[0].DebtType;
                }        
                else
                {
                    return "";
                }
            }
        }

        //Checked TongKung
        //[DataMember(Order=56)]
        public string Period
        {
            get
            {
                return (_bills.Count > 0) ? _bills[0].Period : "";
            }
        }

        //Checked TongKung
        //[DataMember(Order=57)]
        public string PeriodString
        {
            get
            {
                switch (_bills[0].DebtId.Substring(0, 5))
                {
                    case "M0080":
                        return string.Format("งวดที่ {0}/{1}", _installmentPeriod, _installmentTotalPeriod);
                    default:
                        return (Period == null) ? "" : StringConvert.FormatPeriod(Period);
                }
            }
        }

        #endregion

        private string _techBranchName;

        [DataMember(Order = 58)]
        public string TechBranchName
        {
            get { return _techBranchName; }
            set { _techBranchName = value; }
        }

        private string _commBranchName;

        [DataMember(Order = 59)]
        public string CommBranchName
        {
            get { return _commBranchName; }
            set { _commBranchName = value; }
        }

        private string _controllerId;

        [DataMember(Order = 60)]
        public string ControllerId
        {
            get { return _controllerId; }
            set { _controllerId = value; }
        }

        private string _controllerName;

        [DataMember(Order = 61)]
        public string ControllerName
        {
            get { return _controllerName; }
            set { _controllerName = value; }
        }

        private string _mruId;

        [DataMember(Order = 62)]
        public string MruId
        {
            get { return _mruId; }
            set { _mruId = value; }
        }

        private string _CaTaxId;

        [DataMember(Order = 63)]
        public string CaTaxId
        {
            get { return _CaTaxId; }
            set { _CaTaxId = value; }
        }

        private string _CaTaxBranch;

        [DataMember(Order = 64)]
        public string CaTaxBranch
        {
            get { return _CaTaxBranch; }
            set { _CaTaxBranch = value; }
        }


        private decimal _energyAmount;
        [DataMember(Order = 65)]
        public decimal EnergyAmount
        {

            get { return _energyAmount; }
            set { _energyAmount = value; }

        }

        private string _notificationNo;
        [DataMember(Order = 66)]
        public string NotificationNo
        {
            get { return _notificationNo; }
            set { _notificationNo = value; }
        }

        #region  DCR รวมใบเสร็จแผนผ่อน 2021AUG (Invoice Class) 
        private string      _groupReceiptId;
        private decimal?    _groupReceiptQty;
        private decimal?    _groupReceiptAmount;
        private string      _groupReceiptPeriodText;
        private string      _groupReceiptInstallmentText;
        private string      _groupReceiptInvoiceNo;
        private decimal?    _groupReceiptTotal;
        private decimal?    _groupReceiptVatTotal;
        private string      _groupReceiptMeterIdText;
        private string      _groupReceiptRateTypeText;
        private string      _groupXReceiptId;        

        [DataMember(Order = 67)]
        public string GroupReceiptId
        {
            get { return _groupReceiptId; }
            set { _groupReceiptId = value; }
        }

        [DataMember(Order = 68)]
        public string GroupReceiptInvoiceNo
        {
            get { return _groupReceiptInvoiceNo; }
            set { _groupReceiptInvoiceNo = value; }
        }

        [DataMember(Order = 69)]
        public decimal? GroupReceiptQty
        {
            get { return _groupReceiptQty; }
            set { _groupReceiptQty = value; }
        }

        [DataMember(Order = 70)]
        public decimal? GroupReceiptAmount
        {
            get { return _groupReceiptAmount; }
            set { _groupReceiptAmount = value; }
        }

        [DataMember(Order = 71)]
        public string GroupReceiptPeriodText
        {
            get { return _groupReceiptPeriodText; }
            set { _groupReceiptPeriodText = value; }
        }

        [DataMember(Order = 72)]
        public string GroupReceiptInstallmentText
        {
            get { return _groupReceiptInstallmentText; }
            set { _groupReceiptInstallmentText = value; }
        }

        [DataMember(Order = 73)]
        public decimal? GroupReceiptVatTotal
        {
            get { return _groupReceiptVatTotal; }
            set { _groupReceiptVatTotal = value; }
        }

        [DataMember(Order = 74)]
        public decimal? GroupReceiptTotal
        {
            get { return _groupReceiptTotal; }
            set { _groupReceiptTotal = value; }
        }

        [DataMember(Order = 75)]
        public string GroupReceiptMeterIdText
        {
            get { return _groupReceiptMeterIdText; }
            set { _groupReceiptMeterIdText = value; }
        }

        [DataMember(Order = 76)]
        public string GroupReceiptRateTypeText
        {
            get { return _groupReceiptRateTypeText; }
            set { _groupReceiptRateTypeText = value; }
        }

        [DataMember(Order = 77)]
        public string GroupXReceiptId
        {
            get { return _groupXReceiptId; }
            set { _groupXReceiptId = value; }
        }        

        #endregion
        //TODO: INSTALLMENT CASE
        //public string OriginalDtId
        //{
        //    get
        //    {
        //        if (_bills.Count > 0)
        //        {
        //            return _bills[0].OriginalDtId;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //}

        //public string OriginalCaDoc
        //{
        //    get
        //    {
        //        if (_bills.Count > 0)
        //        {
        //            return _bills[0].OriginalCaDoc;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //}

        //private bool _isInvalidInstment;

        //[DataMember(Order = 63)]
        //public bool IsInvalidInstment
        //{
        //    get { return _isInvalidInstment; }
        //    set { _isInvalidInstment = value; }
        //}


        // DCR 67-020 เกี่ยวกับการค้นหาหนี้ซ้ำเพื่อปรับปรุงหนี้ แต่ถ้าเป็นหนี้ตั้งเองในเครื่อง (ไม่มีใน SAP)
        // หนี้ตั้งเองจะไม่ถูกปรับปรุงหนี้ และไม่ถูก Clear หากเป็น Ca เดียวกัน
        bool _invoiceFromLocal;

        [DataMember(Order = 78)]
        public bool InvoiceFromLoal 
        { 
            get {return _invoiceFromLocal;}
            set {_invoiceFromLocal = value;} 
        }

    }


    [DataContract]
    public class ObjectComparer<ComparableObject> : IComparer<ComparableObject>
    {
        #region Constructor
        public ObjectComparer()
        {
        }
        public ObjectComparer(string p_propertyName)
        {
            //We must have a property name for this comparer to work
            this.PropertyName = p_propertyName;
        }
        public ObjectComparer(string p_propertyName, bool p_MultiColumn)
        {
            //We must have a property name for this comparer to work
            this.PropertyName = p_propertyName;
            this.MultiColumn = p_MultiColumn;
        }
        #endregion


        #region Property
        private bool _MultiColumn;

        [DataMember(Order=1)]
        public bool MultiColumn
        {
            get { return _MultiColumn; }
            set { _MultiColumn = value; }
        }
        private string _propertyName;

        [DataMember(Order=2)]
        public string PropertyName
        {
            get { return _propertyName; }
            set { _propertyName = value; }
        }
        #endregion


        #region IComparer<ComparableObject> Members
        /// <summary>
        /// This comparer is used to sort the generic comparer
        /// The constructor sets the PropertyName that is used
        /// by reflection to access that property in the object to
        /// object compare.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(ComparableObject x, ComparableObject y)
        {
            Type t = x.GetType();
            if (_MultiColumn) // Multi Column Sorting
            {
                string[] sortExpressions = _propertyName.Trim().Split(',');
                for (int i = 0; i < sortExpressions.Length; i++)
                {
                    string fieldName, direction = "ASC";
                    if (sortExpressions[i].Trim().EndsWith(" DESC"))
                    {
                        fieldName = sortExpressions[i].Replace(" DESC", "").Trim();
                        direction = "DESC";
                    }
                    else
                    {
                        fieldName = sortExpressions[i].Replace(" ASC", "").Trim();
                    }
                    //Get property by name
                    PropertyInfo val = t.GetProperty(fieldName);
                    if (val != null)
                    {
                        //Compare values, using IComparable interface of the property's type
                        int iResult = Comparer.DefaultInvariant.Compare(val.GetValue(x, null), val.GetValue(y, null));
                        if (iResult != 0)
                        {
                            //Return if not equal
                            if (direction == "DESC")
                            {
                                //Invert order
                                return -iResult;
                            }
                            else
                            {
                                return iResult;
                            }
                        }
                    }
                    else
                    {
                        throw new Exception(fieldName + " is not a valid property to sort on. It doesn't exist");
                    }
                }
                //Objects have the same sort order
                return 0;
            }
            else
            {
                PropertyInfo val = t.GetProperty(this.PropertyName);
                if (val != null)
                {
                    return Comparer.DefaultInvariant.Compare(val.GetValue(x, null), val.GetValue(y, null));
                }
                else
                {
                    throw new Exception(this.PropertyName + " is not a valid property to sort on. It doesn't exist");
                }
            }
        }
        #endregion




    }
}
