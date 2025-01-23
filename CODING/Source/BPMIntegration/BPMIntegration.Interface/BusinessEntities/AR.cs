using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.ArchitectureInterface.Services;
using PEA.BPM.Integration.BPMIntegration.Interface.Constants;
using System.Globalization;
using PEA.BPM.Architecture.CommonUtilities;
using System.IO;

using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]
    public class AR : IListUtility<AR>
    {
        private string _ItemId;
        private string _CaDoc;
        private string _CaId;
        private string _DtId;
        private string _Description;
        private string _InvoiceNo;
        private DateTime? _InvoiceDt;
        private string _GroupInvoiceNo;
        private string _BillBookId;
        private string _Period;
        private string _MeterId;
        private string _RateTypeId;
        private DateTime? _MeterReadDt;
        private Decimal? _ReadUnit;
        private Decimal? _LastUnit;
        private decimal? _BaseAmount;
        private decimal? _FTUnitPrice;
        private decimal? _FTAmount;
        private decimal? _UnitPrice;
        private Decimal? _Qty;
        private string _UnitTypeId;
        private decimal? _Amount;
        private string _TaxCode;
        private decimal? _VatAmount;
        private decimal? _GAmount;
        private DateTime? _DueDt;
        private DateTime? _DueDt2;
        private DateTime? _DisconnectDt;
        private string _DisconnectDoc;
        private string _SubstDocNo;
        private string _OriginalInvoiceNo;
        private string _SpotBillInvoiceNo;
        private string _InterestLockFlag;
        private string _InterestKey;
        private string _InstallmentFlag;
        private int? _InstallmentPeriod;
        private int? _InstallmentTotalPeriod;
        private string _PaymentOrderFlag;
        private DateTime? _PaymentOrderDt;
        private string _CheckInRefNo;
        private string _CancelFlag;
        private string _ModifiedFlag;
        private string _POSDebtFlag;
        private string _BranchId;
        private string _TechBranchId;
        private string _CommBranchId;
        private string _SyncFlag;
        private string _PostBranchServerId;
        private DateTime? _PostDt;
        private DateTime? _CreatedDt;
        private DateTime? _ModifiedDt;
        private string _ModifiedBy;
        private string _FileName;
        private Boolean _Active;
        private string _Action;
        private string _main;
        private string _sub;
        private string _hh;

        [DataMember(Order = 1)]
        public string ItemId
        {
            get { return _ItemId; }
            set { _ItemId = value; }
        }

        [DataMember(Order = 2)]
        public string CaDoc
        {
            get { return _CaDoc; }
            set { _CaDoc = value; }
        }

        [DataMember(Order = 3)]
        public string CaId
        {
            get { return _CaId; }
            set { _CaId = value; }
        }

        [DataMember(Order = 4)]
        public string DtId
        {
            get { return _DtId; }
            set { _DtId = value; }
        }

        [DataMember(Order = 5)]
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        [DataMember(Order = 6)]
        public string InvoiceNo
        {
            get { return _InvoiceNo; }
            set { _InvoiceNo = value; }
        }

        [DataMember(Order = 7)]
        public DateTime? InvoiceDt
        {
            get { return _InvoiceDt; }
            set { _InvoiceDt = value; }
        }

        [DataMember(Order = 8)]
        public string GroupInvoiceNo
        {
            get { return _GroupInvoiceNo; }
            set { _GroupInvoiceNo = value; }
        }

        [DataMember(Order = 9)]
        public string BillBookId
        {
            get { return _BillBookId; }
            set { _BillBookId = value; }
        }

        [DataMember(Order = 10)]
        public string Period
        {
            get { return _Period; }
            set { _Period = value; }
        }

        [DataMember(Order = 11)]
        public string MeterId
        {
            get { return _MeterId; }
            set { _MeterId = value; }
        }

        [DataMember(Order = 12)]
        public string RateTypeId
        {
            get { return _RateTypeId; }
            set { _RateTypeId = value; }
        }

        [DataMember(Order = 13)]
        public DateTime? MeterReadDt
        {
            get { return _MeterReadDt; }
            set { _MeterReadDt = value; }
        }

        [DataMember(Order = 14)]
        public Decimal? ReadUnit
        {
            get { return _ReadUnit; }
            set { _ReadUnit = value; }
        }

        [DataMember(Order = 15)]
        public Decimal? LastUnit
        {
            get { return _LastUnit; }
            set { _LastUnit = value; }
        }

        [DataMember(Order = 16)]
        public decimal? BaseAmount
        {
            get { return _BaseAmount; }
            set { _BaseAmount = value; }
        }

        [DataMember(Order = 17)]
        public decimal? FTUnitPrice
        {
            get { return _FTUnitPrice; }
            set { _FTUnitPrice = value; }
        }

        [DataMember(Order = 18)]
        public decimal? FTAmount
        {
            get { return _FTAmount; }
            set { _FTAmount = value; }
        }

        [DataMember(Order = 19)]
        public decimal? UnitPrice
        {
            get { return _UnitPrice; }
            set { _UnitPrice = value; }
        }

        [DataMember(Order = 20)]
        public decimal? Qty
        {
            get { return _Qty; }
            set { _Qty = value; }
        }

        [DataMember(Order = 21)]
        public string UnitTypeId
        {
            get { return _UnitTypeId; }
            set { _UnitTypeId = value; }
        }

        [DataMember(Order = 22)]
        public decimal? Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        [DataMember(Order = 23)]
        public string TaxCode
        {
            get { return _TaxCode; }
            set { _TaxCode = value; }
        }

        [DataMember(Order = 24)]
        public decimal? VatAmount
        {
            get { return _VatAmount; }
            set { _VatAmount = value; }
        }

        [DataMember(Order = 25)]
        public decimal? GAmount
        {
            get { return _GAmount; }
            set { _GAmount = value; }
        }

        [DataMember(Order = 26)]
        public DateTime? DueDt
        {
            get { return _DueDt; }
            set { _DueDt = value; }
        }

        [DataMember(Order = 27)]
        public DateTime? DueDt2
        {
            get { return _DueDt2; }
            set { _DueDt2 = value; }
        }

        [DataMember(Order = 28)]
        public DateTime? DisconnectDt
        {
            get { return _DisconnectDt; }
            set { _DisconnectDt = value; }
        }

        [DataMember(Order = 29)]
        public string DisconnectDoc
        {
            get { return _DisconnectDoc; }
            set { _DisconnectDoc = value; }
        }

        [DataMember(Order = 30)]
        public string SubstDocNo
        {
            get { return _SubstDocNo; }
            set { _SubstDocNo = value; }
        }

        [DataMember(Order = 31)]
        public string OriginalInvoiceNo
        {
            get { return _OriginalInvoiceNo; }
            set { _OriginalInvoiceNo = value; }
        }

        [DataMember(Order = 32)]
        public string SpotBillInvoiceNo
        {
            get { return _SpotBillInvoiceNo; }
            set { _SpotBillInvoiceNo = value; }
        }

        [DataMember(Order = 33)]
        public string InterestLockFlag
        {
            get { return _InterestLockFlag; }
            set { _InterestLockFlag = value; }
        }

        [DataMember(Order = 34)]
        public string InterestKey
        {
            get { return _InterestKey; }
            set { _InterestKey = value; }
        }

        [DataMember(Order = 35)]
        public string InstallmentFlag
        {
            get { return _InstallmentFlag; }
            set { _InstallmentFlag = value; }
        }

        [DataMember(Order = 36)]
        public int? InstallmentPeriod
        {
            get { return _InstallmentPeriod; }
            set { _InstallmentPeriod = value; }
        }

        [DataMember(Order = 37)]
        public int? InstallmentTotalPeriod
        {
            get { return _InstallmentTotalPeriod; }
            set { _InstallmentTotalPeriod = value; }
        }

        [DataMember(Order = 38)]
        public string PaymentOrderFlag
        {
            get { return _PaymentOrderFlag; }
            set { _PaymentOrderFlag = value; }
        }

        [DataMember(Order = 39)]
        public DateTime? PaymentOrderDt
        {
            get { return _PaymentOrderDt; }
            set { _PaymentOrderDt = value; }
        }

        [DataMember(Order = 40)]
        public string CheckInRefNo
        {
            get { return _CheckInRefNo; }
            set { _CheckInRefNo = value; }
        }

        [DataMember(Order = 41)]
        public string CancelFlag
        {
            get { return _CancelFlag; }
            set { _CancelFlag = value; }
        }

        [DataMember(Order = 42)]
        public string ModifiedFlag
        {
            get { return _ModifiedFlag; }
            set { _ModifiedFlag = value; }
        }

        [DataMember(Order = 43)]
        public string POSDebtFlag
        {
            get { return _POSDebtFlag; }
            set { _POSDebtFlag = value; }
        }

        [DataMember(Order = 44)]
        public string BranchId
        {
            get { return _BranchId; }
            set { _BranchId = value; }
        }

        [DataMember(Order = 45)]
        public string TechBranchId
        {
            get { return _TechBranchId; }
            set { _TechBranchId = value; }
        }

        [DataMember(Order = 46)]
        public string CommBranchId
        {
            get { return _CommBranchId; }
            set { _CommBranchId = value; }
        }

        [DataMember(Order = 47)]
        public string SyncFlag
        {
            get { return _SyncFlag; }
            set { _SyncFlag = value; }
        }

        [DataMember(Order = 48)]
        public string PostBranchServerId
        {
            get { return _PostBranchServerId; }
            set { _PostBranchServerId = value; }
        }

        [DataMember(Order = 49)]
        public DateTime? PostDt
        {
            get { return _PostDt; }
            set { _PostDt = value; }
        }

        [DataMember(Order = 50)]
        public DateTime? CreatedDt
        {
            get { return _CreatedDt; }
            set { _CreatedDt = value; }
        }

        [DataMember(Order = 51)]
        public DateTime? ModifiedDt
        {
            get { return _ModifiedDt; }
            set { _ModifiedDt = value; }
        }

        [DataMember(Order = 52)]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }

        [DataMember(Order = 53)]
        public string FileName
        {
            get { return _FileName; }
            set { _FileName = value; }
        }

        [DataMember(Order = 54)]
        public Boolean Active
        {
            get { return _Active; }
            set { _Active = value; }
        }

        [DataMember(Order = 55)]
        public string Action
        {
            get { return _Action; }
            set { _Action = value; }
        }

        [DataMember(Order = 56)]
        public string Main
        {
            get { return _main; }
            set { _main = value; }
        }

        [DataMember(Order = 57)]
        public string Sub
        {
            get { return _sub; }
            set { _sub = value; }
        }

        [DataMember(Order = 58)]
        public string HH
        {
            get { return _hh; }
            set { _hh = value; }
        }


        #region IListUtility<AR> Members

        public string ToStream()
        {
            //IFormatProvider formatDate = CultureInfo.CreateSpecificCulture("th-TH");
            //string[] template = { CaId, DtId, Description, InvoiceNo,GroupInvoiceNo, BillBookId, Period,
            //                      MeterReadDt.Value.ToString("ddMMyyyy", formatDate), ReadUnit.Value.ToString(),
            //                      LastUnit.Value.ToString(), BaseAmount.Value.ToString(), FTUnitPrice.Value.ToString(),
            //                      FTAmount.Value.ToString(), UnitPrice.Value.ToString(), Amount.Value.ToString(),
            //                      Qty.Value.ToString(), UnitTypeId, TaxCode, VatAmount.Value.ToString(),
            //                      GAmount.Value.ToString(), DueDt.Value.ToString("ddMMyyyy", formatDate), DueDt2.Value.ToString("ddMMyyyy", formatDate),
            //                      InterestKey, ControllerId, PaidAmount.Value.ToString(), CutOffDt.Value.ToString("ddMMyyyy", formatDate),
            //                      SubstDocNo, InstallmentFlag, InstallmentPeriod.ToString(), InstallmentTotalPeriod.ToString(),
            //                      SpotBillInvoiceNo, PaymentOrderFlag, CancelFlag, ModifiedFlag, POSDebtFlag,
            //                      PostDt.Value.ToString("ddMMyyyy", formatDate), Action };
            //return string.Join("\t", template);
            return string.Empty;
        }

        IFormatProvider usFormatDate = CultureInfo.CreateSpecificCulture("en-US");

        private string GetString(DateTime? value)
        {
            return value == null ? "" : value.Value.ToString("yyyy-MM-dd", usFormatDate);
        }

        private string GetString(Decimal? value)
        {
            return value == null ? "" : value.Value.ToString();
        }

        private string GetString(int? value)
        {
            return value == null ? "" : value.Value.ToString();
        }

        public string ToBulkLoadString()
        {
            string[] template = new string[] {HH, ItemId, CaId, Main, Sub, Description,InvoiceNo, GetString(InvoiceDt), // 6
                GroupInvoiceNo, Period, MeterId, RateTypeId, GetString(MeterReadDt), // 5
                GetString(ReadUnit), GetString(LastUnit), GetString(BaseAmount), // 3
                GetString(FTUnitPrice), GetString(FTAmount), GetString(UnitPrice), GetString(Qty), GetString(Amount), // 5
                UnitTypeId, TaxCode, GetString(VatAmount), GetString(GAmount), // 4
                GetString(DueDt), GetString(DueDt2), InterestLockFlag, InterestKey, GetString(DisconnectDt), DisconnectDoc, SubstDocNo, // 7
                GetString(InstallmentPeriod), GetString(InstallmentTotalPeriod), SpotBillInvoiceNo, CancelFlag, PaymentOrderFlag, // 5
                GetString(PaymentOrderDt), ModifiedFlag, Action}; // 3

            return string.Join("|", template);        
        }

        public AR ParseExtract(string txt)
        {
            AR it = new AR();
            string[] sp = txt.Split('|');

            const int colFixed = InterfaceColumns.AR;
            int colCounted = sp.Length - 1;
            if (colCounted != colFixed)
                throw new Exception("ไม่สามารถทำรายการได้ เนื่องจาก Columns ใน text file มีจำนวน " + colCounted.ToString() + " แต่ Columns ที่กำหนดไว้มีจำนวน " + colFixed.ToString());


            int i = 0;
            it.HH = StringConvert.ToString(sp[i].Trim());//0
            i = i + 1;
            it.ItemId = StringConvert.ToString(sp[i].Trim());//1
            i = i + 1;
            it.CaId = StringConvert.ToString(sp[i].Trim());//2
            i = i + 1;
            it.Main = StringConvert.ToString(sp[i].Trim()); //3
            i = i + 1;
            it.Sub = StringConvert.ToString(sp[i].Trim()); //4
            i = i + 1;
            it.Description = StringConvert.ToString(sp[i].Trim()); //5
            i = i + 1;
            it.InvoiceNo = StringConvert.ToString(sp[i].Trim());//6
            i = i + 1;
            it.InvoiceDt = StringConvert.ToDateTime(sp[i].Trim()); //7
            i = i + 1;
            it.GroupInvoiceNo = StringConvert.ToString(sp[i].Trim());//8
            i = i + 1;
            it.Period = StringConvert.ToString(sp[i].Trim());//9
            i = i + 1;
            it.MeterId = StringConvert.ToString(sp[i].Trim());//10
            i = i + 1;
            it.RateTypeId = StringConvert.ToString(sp[i].Trim());//11
            i = i + 1;
            it.MeterReadDt = StringConvert.ToDateTime(sp[i].Trim());//12
            i = i + 1;
            it.ReadUnit = StringConvert.ToDecimal(sp[i].Trim()) ;//13
            i = i + 1;
            it.LastUnit = StringConvert.ToDecimal(sp[i].Trim()); //14
            i = i + 1;
            it.BaseAmount = StringConvert.ToDecimal(sp[i].Trim()); //15
            i = i + 1;
            it.FTUnitPrice = StringConvert.ToDecimal(sp[i].Trim());//16
            i = i + 1;
            it.FTAmount = StringConvert.ToDecimal(sp[i].Trim()); //17
            i = i + 1;
            it.UnitPrice = StringConvert.ToDecimal(sp[i].Trim());//18
            i = i + 1;
            it.Qty = StringConvert.ToDecimal(sp[i].Trim());//19
            i = i + 1;
            it.Amount = StringConvert.ToDecimal(sp[i].Trim()); //20
            i = i + 1;
            it.UnitTypeId = StringConvert.ToString(sp[i].Trim()); //21
            i = i + 1;
            it.TaxCode = StringConvert.ToString(sp[i].Trim());//22
            i = i + 1;
            it.VatAmount = StringConvert.ToDecimal(sp[i].Trim());//23
            i = i + 1;
            it.GAmount = StringConvert.ToDecimal(sp[i].Trim());//24
            i = i + 1;        
            it.DueDt = StringConvert.ToDateTime(sp[i].Trim());//25
            i = i + 1;
            it.DueDt2 = StringConvert.ToDateTime(sp[i].Trim());  //26
            i = i + 1;
            it.InterestLockFlag = StringConvert.ToString(sp[i].Trim()); //27 InterestLockReason
            i = i + 1;
            it.InterestKey = StringConvert.ToString(sp[i].Trim());//28
            i = i + 1;
            it.DisconnectDt = StringConvert.ToDateTime(sp[i].Trim()); //29 CutOfDt
            i = i + 1;
            it.DisconnectDoc = StringConvert.ToString(sp[i].Trim());//30
            i = i + 1;
            it.SubstDocNo = StringConvert.ToString(sp[i].Trim());//31    
            i = i + 1;
            it.InstallmentPeriod = StringConvert.ToInt32(sp[i].Trim());//32
            i = i + 1;
            it.InstallmentTotalPeriod = StringConvert.ToInt32(sp[i].Trim());//33
            i = i + 1;
            it.SpotBillInvoiceNo = StringConvert.ToString(sp[i].Trim()); //34
            i = i + 1;
            it.CancelFlag = StringConvert.ToString(sp[i].Trim());//35
            i = i + 1;
            it.PaymentOrderFlag = StringConvert.ToString(sp[i].Trim());//36
            i = i + 1;
            it.PaymentOrderDt = StringConvert.ToDateTime(sp[i].Trim());//37
            i = i + 1;
            it.ModifiedFlag = StringConvert.ToString(sp[i].Trim());//38
            i = i + 1;
            it.Action = it.CancelFlag == "1" ? "3" : StringConvert.ToString(sp[i].Trim());

            if (it.Action != "O" && it.Action != "0" && it.Action != "1" && it.Action != "2" && it.Action != "3")
                throw new Exception("ไม่สามารถทำรายการได้ เนื่องจากข้อมูลของ [Action] มีค่าเท่ากับ " + it.Action + "] ซึ่งไม่ตรงตามรูปแบบที่กำหนดไว้");

            it.ModifiedBy = "BATCH";          

            return it;           
        }

        #endregion
    }
}
