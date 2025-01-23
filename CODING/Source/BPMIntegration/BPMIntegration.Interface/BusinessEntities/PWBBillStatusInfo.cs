using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.CommonUtilities;

using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]
    public class PWBBillStatusInfo : IListUtility<PWBBillStatusInfo>
    {
        private string _BillPred;
        private string _BranchId;
        private string _MruId;
        private string _Portion;
        private string _ReaderNo;
        private string _BranchName;
        private int? _CaBlue;
        private int? _CaGreen;
        private int? _CaA4;
        private int? _CaSpotBill;
        private int? _CaGrpInv;
        private int? _CaTypeF;
        private int? _CaOther;
        private int? _CaTotal;
        private int? _TotPrintBlue;
        private int? _NoPrintBlue;
        private int? _TotPrintGreen;
        private int? _NoPrintGreen;
        private int? _TotPrintA4;
        private int? _NoPrintA4;
        private int? _TotPrintSpotBill;
        private int? _NoPrintSpotBill;
        private int? _TotPrintGrpInv;
        private int? _NoPrintGrpInv;
        private int? _TotPrintTypeF;
        private int? _NoPrintTypeF;
        private int? _AnyOther;
        private int? _TotalPrint;
        private int? _TotalNoPrint;
        private string _ModifiedBy;
        private DateTime? _ModifiedDt;
        private string _Action;

        [DataMember(Order = 1)]
        public string BillPred
        {
            get { return _BillPred; }
            set { _BillPred = value; }
        }
        [DataMember(Order = 2)]
        public string BranchId
        {
            get { return _BranchId; }
            set { _BranchId = value; }
        }
        [DataMember(Order = 3)]
        public string MruId
        {
            get { return _MruId; }
            set { _MruId = value; }
        }
        [DataMember(Order = 4)]
        public string Portion
        {
            get { return _Portion; }
            set { _Portion = value; }
        }
        [DataMember(Order = 5)]
        public string ReaderNo
        {
            get { return _ReaderNo; }
            set { _ReaderNo = value; }
        }
        [DataMember(Order = 6)]
        public string BranchName
        {
            get { return _BranchName; }
            set { _BranchName = value; }
        }
        [DataMember(Order = 7)]
        public int? CaBlue
        {
            get { return _CaBlue; }
            set { _CaBlue = value; }
        }
        [DataMember(Order = 8)]
        public int? CaGreen
        {
            get { return _CaGreen; }
            set { _CaGreen = value; }
        }
        [DataMember(Order = 9)]
        public int? CaA4
        {
            get { return _CaA4; }
            set { _CaA4 = value; }
        }
        [DataMember(Order = 10)]
        public int? CaSpotBill
        {
            get { return _CaSpotBill; }
            set { _CaSpotBill = value; }
        }
        [DataMember(Order = 11)]
        public int? CaGrpInv
        {
            get { return _CaGrpInv; }
            set { _CaGrpInv = value; }
        }
        [DataMember(Order = 12)]
        public int? CaTypeF
        {
            get { return _CaTypeF; }
            set { _CaTypeF = value; }
        }
        [DataMember(Order = 13)]
        public int? CaOther
        {
            get { return _CaOther; }
            set { _CaOther = value; }
        }
        [DataMember(Order = 14)]
        public int? CaTotal
        {
            get { return _CaTotal; }
            set { _CaTotal = value; }
        }
        [DataMember(Order = 15)]
        public int? TotPrintBlue
        {
            get { return _TotPrintBlue; }
            set { _TotPrintBlue = value; }
        }
        [DataMember(Order = 16)]
        public int? NoPrintBlue
        {
            get { return _NoPrintBlue; }
            set { _NoPrintBlue = value; }
        }
        [DataMember(Order = 17)]
        public int? TotPrintGreen
        {
            get { return _TotPrintGreen; }
            set { _TotPrintGreen = value; }
        }
        [DataMember(Order = 18)]
        public int? NoPrintGreen
        {
            get { return _NoPrintGreen; }
            set { _NoPrintGreen = value; }
        }
        [DataMember(Order = 19)]
        public int? TotPrintA4
        {
            get { return _TotPrintA4; }
            set { _TotPrintA4 = value; }
        }
        [DataMember(Order = 20)]
        public int? NoPrintA4
        {
            get { return _NoPrintA4; }
            set { _NoPrintA4 = value; }
        }
        [DataMember(Order = 21)]
        public int? TotPrintSpotBill
        {
            get { return _TotPrintSpotBill; }
            set { _TotPrintSpotBill = value; }
        }
        [DataMember(Order = 22)]
        public int? NoPrintSpotBill
        {
            get { return _NoPrintSpotBill; }
            set { _NoPrintSpotBill = value; }
        }
        [DataMember(Order = 23)]
        public int? TotPrintGrpInv
        {
            get { return _TotPrintGrpInv; }
            set { _TotPrintGrpInv = value; }
        }
        [DataMember(Order = 24)]
        public int? NoPrintGrpInv
        {
            get { return _NoPrintGrpInv; }
            set { _NoPrintGrpInv = value; }
        }
        [DataMember(Order = 25)]
        public int? TotPrintTypeF
        {
            get { return _TotPrintTypeF; }
            set { _TotPrintTypeF = value; }
        }
        [DataMember(Order = 26)]
        public int? NoPrintTypeF
        {
            get { return _NoPrintTypeF; }
            set { _NoPrintTypeF = value; }
        }
        [DataMember(Order = 27)]
        public int? AnyOther
        {
            get { return _AnyOther; }
            set { _AnyOther = value; }
        }
        [DataMember(Order = 28)]
        public int? TotalPrint
        {
            get { return _TotalPrint; }
            set { _TotalPrint = value; }
        }
        [DataMember(Order = 29)]
        public int? TotalNoPrint
        {
            get { return _TotalNoPrint; }
            set { _TotalNoPrint = value; }
        }
        [DataMember(Order = 30)]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        [DataMember(Order = 31)]
        public DateTime? ModifiedDt
        {
            get { return _ModifiedDt; }
            set { _ModifiedDt = value; }
        }
        [DataMember(Order = 32)]
        public string Action
        {
            get { return _Action; }
            set { _Action = value; }
        }


        #region IListUtility<PWBBillStatusInfo> Members

        public string ToStream()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public PWBBillStatusInfo ParseExtract(string txt)
        {
            PWBBillStatusInfo obj = new PWBBillStatusInfo();

            string[] sp = txt.Split('|');
            int i = 1;           

            obj.BranchId = StringConvert.ToString(sp[i++].Trim());
            obj.BillPred = StringConvert.ToString(sp[i++].Trim());
            obj.MruId = StringConvert.ToString(sp[i++].Trim());
            obj.Portion = StringConvert.ToString(sp[i++].Trim());
            obj.ReaderNo = StringConvert.ToString(sp[i++].Trim());
            obj.CaBlue = StringConvert.ToInt32(sp[i++].Trim());
            obj.CaSpotBill = StringConvert.ToInt32(sp[i++].Trim());
            obj.CaGreen = StringConvert.ToInt32(sp[i++].Trim());
            obj.CaA4 = StringConvert.ToInt32(sp[i++].Trim());
            obj.CaGrpInv = StringConvert.ToInt32(sp[i++].Trim());
            obj.CaTypeF = StringConvert.ToInt32(sp[i++].Trim());
            obj.CaOther = StringConvert.ToInt32(sp[i++].Trim());
            obj.TotPrintBlue = StringConvert.ToInt32(sp[i++].Trim());
            obj.NoPrintBlue = StringConvert.ToInt32(sp[i++].Trim());
            obj.TotPrintSpotBill = StringConvert.ToInt32(sp[i++].Trim());
            obj.NoPrintSpotBill = StringConvert.ToInt32(sp[i++].Trim());
            obj.TotPrintGreen = StringConvert.ToInt32(sp[i++].Trim());
            obj.NoPrintGreen = StringConvert.ToInt32(sp[i++].Trim());
            obj.TotPrintA4 = StringConvert.ToInt32(sp[i++].Trim());
            obj.NoPrintA4 = StringConvert.ToInt32(sp[i++].Trim());
            obj.TotPrintGrpInv = StringConvert.ToInt32(sp[i++].Trim());
            obj.NoPrintGrpInv = StringConvert.ToInt32(sp[i++].Trim());
            obj.TotPrintTypeF = StringConvert.ToInt32(sp[i++].Trim());
            obj.NoPrintTypeF = StringConvert.ToInt32(sp[i++].Trim());
            obj.AnyOther = StringConvert.ToInt32(sp[i++].Trim()); //No Print

            return obj;
        }

        #endregion
    }
}
