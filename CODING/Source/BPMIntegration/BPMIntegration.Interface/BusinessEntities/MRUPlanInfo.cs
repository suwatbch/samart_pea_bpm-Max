using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.ArchitectureInterface.Services;
using System.Globalization;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Integration.BPMIntegration.Interface.Constants;

using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]
    public class MRUPlanInfo : IListUtility<MRUPlanInfo>
    {
        private string _MruPlanId;
        private string _PortionId;
        private string _BranchId;
        private string _MruId;
        private string _Period;
        private DateTime? _MeterReadDt;
        private DateTime? _MeterReadActDt;
        private DateTime? _TransferDt;
        private DateTime? _TransferActDt;
        private DateTime? _BillPrintDt;
        private DateTime? _BillPrintActDt;
        private DateTime? _BookCreateDt;
        private DateTime? _BookCreateActDt;
        private DateTime? _BookCheckInDt;
        private DateTime? _BookCheckInActDt;
        private DateTime? _PostDt;
        private string _SyncFlag;
        private string _ModifiedBy;
        private DateTime? _ModifiedDt;
        private bool _Active;
        private string _action;
        string _collectCount;

        [DataMember(Order = 1)]
        public string MruPlanId
        {
            get { return _MruPlanId; }
            set { _MruPlanId = value; }
        }
        [DataMember(Order = 2)]
        public string PortionId
        {
            get { return _PortionId; }
            set { _PortionId = value; }
        }
        [DataMember(Order = 3)]
        public string BranchId
        {
            get { return _BranchId; }
            set { _BranchId = value; }
        }
        [DataMember(Order = 4)]
        public string MruId
        {
            get { return _MruId; }
            set { _MruId = value; }
        }
        [DataMember(Order = 5)]
        public string Period
        {
            get { return _Period; }
            set { _Period = value; }
        }
        [DataMember(Order = 6)]
        public DateTime? MeterReadDt
        {
            get { return _MeterReadDt; }
            set { _MeterReadDt = value; }
        }
        [DataMember(Order = 7)]
        public DateTime? MeterReadActDt
        {
            get { return _MeterReadActDt; }
            set { _MeterReadActDt = value; }
        }
        [DataMember(Order = 8)]
        public DateTime? TransferDt
        {
            get { return _TransferDt; }
            set { _TransferDt = value; }
        }
        [DataMember(Order = 9)]
        public DateTime? TransferActDt
        {
            get { return _TransferActDt; }
            set { _TransferActDt = value; }
        }
        [DataMember(Order = 10)]
        public DateTime? BillPrintDt
        {
            get { return _BillPrintDt; }
            set { _BillPrintDt = value; }
        }
        [DataMember(Order = 11)]
        public DateTime? BillPrintActDt
        {
            get { return _BillPrintActDt; }
            set { _BillPrintActDt = value; }
        }
        [DataMember(Order = 12)]
        public DateTime? BookCreateDt
        {
            get { return _BookCreateDt; }
            set { _BookCreateDt = value; }
        }
        [DataMember(Order = 13)]
        public DateTime? BookCreateActDt
        {
            get { return _BookCreateActDt; }
            set { _BookCreateActDt = value; }
        }
        [DataMember(Order = 14)]
        public DateTime? BookCheckInDt
        {
            get { return _BookCheckInDt; }
            set { _BookCheckInDt = value; }
        }
        [DataMember(Order = 15)]
        public DateTime? BookCheckInActDt
        {
            get { return _BookCheckInActDt; }
            set { _BookCheckInActDt = value; }
        }
        [DataMember(Order = 16)]
        public DateTime? PostDt
        {
            get { return _PostDt; }
            set { _PostDt = value; }
        }
        [DataMember(Order = 17)]
        public string SyncFlag
        {
            get { return _SyncFlag; }
            set { _SyncFlag = value; }
        }
        [DataMember(Order = 18)]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        [DataMember(Order = 19)]
        public DateTime? ModifiedDt
        {
            get { return _ModifiedDt; }
            set { _ModifiedDt = value; }
        }
        [DataMember(Order = 20)]
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        [DataMember(Order = 21)]
        public string Action
        {
            get { return this._action; }
            set { this._action = value; }
        }
        [DataMember(Order = 22)]
        public string CollectCount
        {
            get { return _collectCount; }
            set { _collectCount = value; }
        }

        /*
        string _mruPlanId;
        public string MruPlanId
        {
            get { return _mruPlanId; }
            set { _mruPlanId = value; }
        }

        string _portionId;
        public string PortionId
        {
            get { return _portionId; }
            set { _portionId = value; }
        }

        string _collectCount;
        public string CollectCount
        {
            get { return _collectCount; }
            set { _collectCount = value; }
        }

        string _branchId;
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }

        string _mruId;
        public string MruId
        {
            get { return _mruId; }
            set { _mruId = value; }
        }

        string _period;
        public string Period
        {
            get { return _period; }
            set { _period = value; }
        }

        DateTime? _meterReadDt;
        public DateTime? MeterReadDt
        {
            get { return _meterReadDt; }
            set { _meterReadDt = value; }
        }

        DateTime? _meterReadActDt;
        public DateTime? MeterReadActDt
        {
            get { return _meterReadActDt; }
            set { _meterReadActDt = value; }
        }

        DateTime? _transferDt;
        public DateTime? TransferDt
        {
            get { return _transferDt; }
            set { _transferDt = value; }
        }

        DateTime? _transferActDt;
        public DateTime? TransferActDt
        {
            get { return _transferActDt; }
            set { _transferActDt = value; }
        }

        DateTime? _billPrintDt;
        public DateTime? BillPrintDt
        {
            get { return _billPrintDt; }
            set { _billPrintDt = value; }
        }

        DateTime? _billPrintActDt;
        public DateTime? BillPrintActDt
        {
            get { return _billPrintActDt; }
            set { _billPrintActDt = value; }
        }

        DateTime? _bookCreateDt;
        public DateTime? BookCreateDt
        {
            get { return _bookCreateDt; }
            set { _bookCreateDt = value; }
        }

        DateTime? _bookCreateActDt;
        public DateTime? BookCreateActDt
        {
            get { return _bookCreateActDt; }
            set { _bookCreateActDt = value; }
        }

        DateTime? _bookCheckInDt;
        public DateTime? BookCheckInDt
        {
            get { return _bookCheckInDt; }
            set { _bookCheckInDt = value; }
        }

        DateTime? _bookCheckInActDt;
        public DateTime? BookCheckInActDt
        {
            get { return _bookCheckInActDt; }
            set { _bookCheckInActDt = value; }
        }

        string _syncFlag;
        public string SyncFlag
        {
            get { return this._syncFlag; }
            set { this._syncFlag = value; }
        }

        DateTime? _modifiedDt;
        public DateTime? ModifiedDt
        {
            get { return this._modifiedDt; }
            set { this._modifiedDt = value; }
        }

        string _modifiedBy;
        public string ModifiedBy
        {
            get { return this._modifiedBy; }
            set { this._modifiedBy = value; }
        }

        bool _active;
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

        string _action;
        public string Action
        {
            get { return this._action; }
            set { this._action = value; }
        }
        */

       #region IListUtility<MRUPlanInfo> Members

       public string ToStream()
       {
           throw new Exception("The method or operation is not implemented.");
       }

       public MRUPlanInfo ParseExtract(string txt)
       {
           IFormatProvider formatDate = CultureInfo.CreateSpecificCulture("en-EN");
           MRUPlanInfo it = new MRUPlanInfo();
           string[] sp = txt.Split('|');

           int colFixed = InterfaceColumns.MRUPlan;
           int colCounted = sp.Length - 1;
           if (colCounted != colFixed)
               throw new Exception("ไม่สามารถทำรายการได้ เนื่องจาก Columns ใน text file มีจำนวน " + colCounted.ToString() + " แต่ Columns ที่กำหนดไว้มีจำนวน " + colFixed.ToString());


           int i = 1;
           it.BranchId = StringConvert.ToString(sp[i++]);           
           //it.CollectCount = StringConvert.ToString(sp[i++]);
           it.PortionId = StringConvert.ToString(sp[i++]);
           it.MruId = StringConvert.ToString(sp[i++]);
           //string tmp = StringConvert.ToString(sp[i++]);
           //if (tmp != null)
           //    if (tmp.Length == 8)
           //        it.MruId = tmp.Substring(tmp.Length - 4, 4);
           //    else
           //        throw new Exception("Data Invalid [Column Name = MRU, Value = " + tmp + "]");

           it.Period = StringConvert.ToString(sp[i++]);           
           it.MeterReadDt = StringConvert.ToDateTime(sp[i++].Trim());
           it.MeterReadActDt = StringConvert.ToDateTime(sp[i++].Trim());        
           //add new 
           it.TransferActDt = StringConvert.ToDateTime(sp[i++].Trim());
           it.Action = sp[i++].Trim();

           if (it.Action != "O" && it.Action != "1" && it.Action != "2" && it.Action != "3")
               throw new Exception("ไม่สามารถทำรายการได้ เนื่องจากข้อมูลของ [Action] มีค่าเท่ากับ " + it.Action + "] ซึ่งไม่ตรงตามรูปแบบที่กำหนดไว้");

           it.ModifiedBy = "BATCH";
           it.ModifiedDt = DateTime.Now;
           it.SyncFlag = "1";
           it.Active = true;
           return it;
       }

       #endregion
   }
}
