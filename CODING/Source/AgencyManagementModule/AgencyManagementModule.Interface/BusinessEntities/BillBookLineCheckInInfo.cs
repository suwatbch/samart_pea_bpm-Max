using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class BillBookLineCheckInInfo
    {
        string _branchId = String.Empty;
        string _mruId = String.Empty;
        int? _collectBillCount = 0;
        decimal? _collectBillAmount = 0;
        int? _notCollectBillCount = 0;
        decimal? _notCollectBillAmount = 0;
        int? _threeMonthBillCount = 0;
        decimal? _threeMonthAmount = 0;
        DateTime? _submitDate;



        [DataMember(Order = 1)]
        public string BranchId
        {
            get { return this._branchId; }
            set { this._branchId = value; }
        }

        [DataMember(Order = 2)]
        public string MruId
        {
            get { return this._mruId; }
            set { this._mruId = value; }
        }

        [DataMember(Order = 3)]
        public int? CollectBillCount
        {
            get { return this._collectBillCount; }
            set { this._collectBillCount = value; }
        }

        [DataMember(Order = 4)]
        public decimal? CollectBillAmount
        {
            get { return this._collectBillAmount; }
            set { this._collectBillAmount = value; }
        }

        [DataMember(Order = 5)]
        public int? NotCollectBillCount
        {
            get { return this._notCollectBillCount; }
            set { this._notCollectBillCount = value; }
        }

        [DataMember(Order = 6)]
        public decimal? NotCollectBillAmount
        {
            get { return this._notCollectBillAmount; }
            set { this._notCollectBillAmount = value; }
        }

        [DataMember(Order = 7)]
        public int? ThreeMonthBillCount
        {
            get { return this._threeMonthBillCount; }
            set { this._threeMonthBillCount = value; }
        }


        [DataMember(Order = 8)]
        public decimal? ThreeMonthAmount
        {
            get { return this._threeMonthAmount; }
            set { this._threeMonthAmount = value; }
        }


        [DataMember(Order = 9)]
        public DateTime? SubmitDate
        {
            get { return this._submitDate; }
            set { this._submitDate = value; }
        }

        //Checked TongKung
        //[DataMember(Order=10)]
        public string WarningDt
        {
            get
            {
                DateTimeFormatInfo _th_dt;
                CultureInfo th_culture = new CultureInfo("th-TH");
                _th_dt = th_culture.DateTimeFormat;
                return _submitDate.Value.ToString("dd/MM/yyyy", _th_dt);
            }
        }

        //Checked TongKung
        //[DataMember(Order=11)]
        public decimal? TotalAmount
        {
            get
            {
                return (this._collectBillAmount + this._notCollectBillAmount + this._threeMonthAmount);
            }
        }

        //Checked TongKung
        //[DataMember(Order=12)]
        public int? TotalCount
        {
            get
            {
                return (this._collectBillCount + this._notCollectBillCount + this._threeMonthBillCount);
            }
        }

    }
}
