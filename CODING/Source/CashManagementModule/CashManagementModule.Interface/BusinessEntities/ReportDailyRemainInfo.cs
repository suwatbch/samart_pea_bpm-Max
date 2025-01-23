using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace PEA.BPM.CashManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class ReportDailyRemainInfo
    {
        //15.30
        private decimal? _overallCashAmt;
        private decimal? _overallChqAmt;
        //private decimal? _overallPayInAmt;
        private decimal? _overallAmt;

        //Checked TongKung
        public string OverallCashAmtTxt
        {
            //get { return (Split(_overallCashAmt))[0]; }
            get { return Number_Thai(_overallCashAmt.Value.ToString("#,###.00")); }
        }

        //Checked TongKung
        public string OverallChqAmtTxt
        {
            //get { return (Split(_overallChqAmt))[0]; }
            get { return Number_Thai(_overallChqAmt.Value.ToString("#,###.00")); }
        }

        //Checked TongKung
        //public string OverallPayInAmtTxt
        //{
        //    //get { return (Split(_overallChqAmt))[0]; }
        //    get { return Number_Thai(_overallPayInAmt.Value.ToString("#,###.00")); }
        //}

        //Checked TongKung
        public string OverallAmtTxt
        {
            //get { return (Split(_overallAmt))[0]; }
            get { return Number_Thai((Split(_overallAmt))[0]); }
        }

        //Checked TongKung
        public string OverallCashAmtTxt_Frac
        {
            get { return (Split(_overallCashAmt))[1]; }
        }

        //Checked TongKung
        public string OverallChqAmtTxt_Frac
        {
            get { return (Split(_overallChqAmt))[1]; }
        }


        //Checked TongKung
        public string OverallAmt_Frac
        {
            get { return (Split(_overallAmt))[1]; }
        }

        public string ChqCountTxt
        {
            get { return (Number_Thai(string.Format("{0:##,###}", _chqCount))); }
        }

        public string ChqCountTxt_Af
        {
            get { return (Number_Thai(string.Format("{0:##,###}", _chqCount_af))); }
        }

        //public string PayInCountTxt
        //{
        //    get { return (Number_Thai(string.Format("{0:##,###}", _payInCount))); }
        //}

        //public string PayInCountTxt_Af
        //{
        //    get { return (Number_Thai(string.Format("{0:##,###}", _payInCount_af))); }
        //}

        public string CloseWorkDateTxt
        {
            set { _closeWorkDate = value; }
            get {
                return (Number_Thai(Convert.ToInt32(_closeWorkDate.Substring(0, 2)).ToString() + "  เดือน  " + ToMonthName(_closeWorkDate.Substring(2, 2)) + "  พ.ศ. " + _closeWorkDate.Substring(4, 4))); 
                }
        }

        //after 15.30
        private decimal? _overallCashAmt_af;
        private decimal? _overallChqAmt_af;
        //private decimal? _overallPayInAmt_af;
        private decimal? _overallAmt_af;

        //Checked TongKung
        public string OverallAmtTxt_Af_Flac
        {
            get { return (Split(_overallAmt_af))[1]; }
        }

        //Checked TongKung
        public string OverallCashAmtTxt_Af_Flac
        {
            get { return (Split(_overallCashAmt_af))[1]; }
        }

        //Checked TongKung
        public string OverallChqAmtTxt_Af_Flac
        {
            get { return (Split(_overallChqAmt_af))[1]; }
        }

        //Checked TongKung
        public string OverallCashAmtTxt_Af
        {
            //get { return (Split(_overallCashAmt_af))[0]; }
            get { return Number_Thai(_overallCashAmt_af.Value.ToString("#,###.00")); }
        }

        //Checked TongKung
        public string OverallChqAmtTxt_Af
        {
            //get { return (Split(_overallChqAmt_af))[0]; }
            get { return Number_Thai(_overallChqAmt_af.Value.ToString("#,###.00")); }
        }

        //Checked TongKung
        public string OverallAmtTxt_Af
        {
            //get { return (Split(_overallAmt_af))[0]; }
            get { return Number_Thai((Split(_overallAmt_af))[0]); }
        }

        //money type, manual fill
        //private int? _1000c;
        //private int? _500c;
        //private int? _100c;
        //private int? _50c;
        //private int? _20c;
        //private int? _10c;

        private string _1000c;
        private string _500c;
        private string _100c;
        private string _50c;
        private string _20c;
        private string _10c;

        private string _1000Amt;
        private string _500Amt;
        private string _100Amt;
        private string _50Amt;
        private string _20Amt;
        private string _10Amt;
        private string _coinAmt;
        private string _coinAmt_Frag;

        private string _sumTotalAmtTxt;
        private string _closeWorkDate;


        [DataMember(Order=13)]
        public string CoinAmt
        {
            set { _coinAmt = value; }
            get { return Number_Thai(_coinAmt); }
        }


        [DataMember(Order=14)]
        public string CoinAmt_Frag
        {
            set { _coinAmt_Frag = value; }
            get { return _coinAmt_Frag; }
        }

        [DataMember(Order = 15)]
        public string Amt1000
        {
            //    set { _1000Amt = value; }
            //    get { return _1000Amt; }

            set { _1000Amt = value; }
            get { return Number_Thai(_1000Amt); }
        }

        [DataMember(Order = 16)]
        public string Amt500
        {
            //    set { _500Amt = value; }
            //    get { return _500Amt; }

            set { _500Amt = value; }
            get { return Number_Thai(_500Amt); }
        }

        [DataMember(Order = 17)]
        public string Amt100
        {
            //    set { _100Amt = value; }
            //    get { return _100Amt; }

            set { _100Amt = value; }
            get { return Number_Thai(_100Amt); }
        }

        [DataMember(Order = 18)]
        public string Amt50
        {
            //    set { _50Amt = value; }
            //    get { return _50Amt; }

            set { _50Amt = value; }
            get { return Number_Thai(_50Amt); }
        }

        [DataMember(Order = 19)]
        public string Amt20
        {
            //    set { _20Amt = value; }
            //    get { return _20Amt; }

            set { _20Amt = value; }
            get { return Number_Thai(_20Amt); }
        }

        [DataMember(Order = 20)]
        public string Amt10
        {
            //    set { _10Amt = value; }
            //    get { return _10Amt; }

            set { _10Amt = value; }
            get { return Number_Thai(_10Amt); }
        }

        //[DataMember(Order=21)]
        //public int? C1000
        //{
        //    set { _1000c = value; }
        //    get { return _1000c; }
        //}
        
        [DataMember(Order = 21)]
        public string C1000
        {
            set { _1000c = value; }
            get { return Number_Thai(string.Format("{0:##,###}", Convert.ToInt32(_1000c))); }
        }

        //[DataMember(Order=22)]
        //public int? C500
        //{
        //    set { _500c = value; }
        //    get { return _500c; }
        //}

        [DataMember(Order = 22)]
        public string C500
        {
            set { _500c = value; }
            get { return Number_Thai(string.Format("{0:##,###}", Convert.ToInt32(_500c))); }
        }

        //[DataMember(Order=23)]
        //public int? C100
        //{
        //    set { _100c = value; }
        //    get { return _100c; }
        //}

        [DataMember(Order = 23)]
        public string C100
        {
            set { _100c = value; }
            get { return Number_Thai(string.Format("{0:##,###}",Convert.ToInt32(_100c))); }
        }

        //[DataMember(Order=24)]
        //public int? C50
        //{
        //    set { _50c  = value; }
        //    get { return _50c; }
        //}

        [DataMember(Order = 24)]
        public string C50
        {
            set { _50c = value; }
            get { return Number_Thai(string.Format("{0:##,###}", Convert.ToInt32(_50c))); }
        }

        //[DataMember(Order=25)]
        //public int? C20
        //{
        //    set { _20c = value; }
        //    get { _20c; }
        //}

        [DataMember(Order = 25)]
        public string C20
        {
            set { _20c = value; }
            get { return Number_Thai(string.Format("{0:##,###}", Convert.ToInt32(_20c))); }
        }

        //[DataMember(Order = 26)]
        //public int? C10
        //{
        //    set { _10c = value; }
        //    get { _10c; }
        //}

        [DataMember(Order = 26)]
        public string C10
        {
            set { _10c = value; }
            get { return Number_Thai(string.Format("0:##,###", Convert.ToInt32(_10c))); }
        }

        private int _chqCount;

        [DataMember(Order = 27)]
        public int ChqCount
        {
            set { _chqCount = value; }
            get { return _chqCount; }
        }

        private int _chqCount_af;

        [DataMember(Order = 28)]
        public int ChqCount_Af
        {
            set { _chqCount_af = value; }
            get { return _chqCount_af; }
        }

        public ReportDailyRemainInfo()
        {
            _overallCashAmt = 0;
            _overallChqAmt = 0;
            //_overallPayInAmt = 0;
            _overallAmt = 0;
            _overallCashAmt_af = 0;
            _overallChqAmt_af = 0;
            //_overallPayInAmt_af = 0;
            _overallAmt_af = 0;
        }


        [DataMember(Order=29)]
        public decimal? OverallCashAmt
        {
            set { _overallCashAmt = value; }
            get { return _overallCashAmt; }
        }


        [DataMember(Order=30)]
        public decimal? OverallChqAmt
        {
            set { _overallChqAmt = value; }
            get { return _overallChqAmt; }
        }


        [DataMember(Order=31)]
        public decimal? OverallAmt
        {
            set { _overallAmt = value; }
            get { return _overallAmt; }
        }


        [DataMember(Order=32)]
        public decimal? OverallCashAmt_Af
        {
            set { _overallCashAmt_af = value; }
            get { return _overallCashAmt_af; }
        }


        [DataMember(Order=33)]
        public decimal? OverallChqAmt_Af
        {
            set { _overallChqAmt_af = value; }
            get { return _overallChqAmt_af; }
        }


        [DataMember(Order=34)]
        public decimal? OverallAmt_Af
        {
            set { _overallAmt_af = value; }
            get { return _overallAmt_af; }
        }

        //Checked TongKung
        public decimal? SumAmt
        {
            get { 
                decimal? sumAmt = _overallAmt + _overallAmt_af;
                return Math.Round(sumAmt.Value, 2);
            }
        }


        [DataMember(Order=36)]
        public string SumAmtTxt
        {
            set { _sumTotalAmtTxt = value; }
            get { return _sumTotalAmtTxt; }
        }


        [DataMember(Order=37)]
        public string CloseWorkDate
        {
            set { _closeWorkDate = value; }
            get { return _closeWorkDate;  }
        }

        private BindingList<ChequeInfo> _remainCheque = new BindingList<ChequeInfo>();

        [DataMember(Order = 38)]
        public BindingList<ChequeInfo> RemainCheque
        {
            get { return _remainCheque; }
            set { _remainCheque = value; }
        }

        //private BindingList<PayInInfo> _remainPayin = new BindingList<PayInInfo>();

        //[DataMember(Order = 39)]
        //public BindingList<PayInInfo> RemainPayin
        //{
        //    get { return _remainPayin; }
        //    set { _remainPayin = value; }
        //}

        //[DataMember(Order = 40)]
        //public decimal? OverallPayInAmt
        //{
        //    set { _overallPayInAmt = value; }
        //    get { return _overallPayInAmt; }
        //}

        //[DataMember(Order = 41)]
        //public decimal? OverallPayInAmt_Af
        //{
        //    set { _overallPayInAmt_af = value; }
        //    get { return _overallPayInAmt_af; }
        //}

        //private int _payInCount;

        //[DataMember(Order = 42)]
        //public int PayInCount
        //{
        //    set { _payInCount = value; }
        //    get { return _payInCount; }
        //}

        //private int _payInCount_af;

        //[DataMember(Order = 43)]
        //public int PayInCount_Af
        //{
        //    set { _payInCount_af = value; }
        //    get { return _payInCount_af; }
        //}

        public string[] Split(decimal? money)
        {
            string moneyTxt = money.Value == 0 ? "0.00" : money.Value.ToString("#,###.00");
            string[] sp = moneyTxt.Split('.');
            return sp;
        }

        //Checked TongKung
        public string Before_Fraction
        {
            get
            {
                string beforeTxt = _overallAmt.Value == 0 ? "0.00" : _overallAmt.Value.ToString("#,###.00");
                string [] sp = beforeTxt.Split('.');
                return sp[1] == "00" ? "-" : sp[1];
            }
        }

        //Checked TongKung
        public string Before_Int
        {
            get
            {
                //string beforeTxt = _overallAmt.Value == 0 ? "0.00" : _overallAmt.Value.ToString("#,###.00");
                //string[] sp = beforeTxt.Split('.');
                //return sp[0];
                string beforeTxt = _overallAmt.Value == 0 ? "0.00" : _overallAmt.Value.ToString("#,###.00");
                return Number_Thai(beforeTxt);
            }
        }

        //Checked TongKung
        public string After_Fraction
        {
            get
            {
                string afterTxt = _overallAmt_af.Value == 0 ? "0.00" : _overallAmt_af.Value.ToString("#,###.00");
                string[] sp = afterTxt.Split('.');
                if (sp[0] == "0")
                    return "-";

                return sp[1] == "00" ? "-" : sp[1];
            }
        }

        //Checked TongKung
        public string After_Int
        {
            get
            {
                //        string afterTxt = _overallAmt_af.Value == 0 ? "0.00" : _overallAmt_af.Value.ToString("#,###.00");
                //        string[] sp = afterTxt.Split('.');
                //        if (sp[0] == "0")
                //            return "-";
                //        sp[0];

                string afterTxt = _overallAmt_af.Value == 0 ? "0.00" : _overallAmt_af.Value.ToString("#,###.00");
                return Number_Thai(afterTxt);
            }
        }


        //Checked TongKung
        public string Sum_Fraction
        {
            get
            {
                decimal? sumAmt = _overallAmt + _overallAmt_af;
                string sumTxt = sumAmt.Value == 0 ? "0.00" : sumAmt.Value.ToString("#,###.00");
                string[] sp = sumTxt.Split('.');
                return sp[1] == "00" ? "-" : sp[1];
            }
        }


        //Checked TongKung
        public string Sum_Int
        {
            get
            {
                //decimal? sumAmt = _overallAmt + _overallAmt_af;
                //string sumTxt = sumAmt.Value == 0 ? "0.00" : sumAmt.Value.ToString("#,###.00");
                //string[] sp = sumTxt.Split('.');
                //return Number_Thai(sp[0]);

                decimal? sumAmt = _overallAmt + _overallAmt_af;
                string sumTxt = sumAmt.Value == 0 ? "0.00" : sumAmt.Value.ToString("#,###.00");
                return Number_Thai(sumTxt);
            }
        }

        //Convert Number Arabic To Thai 
        public string Number_Thai(string Arabic_Number)
        {
            try
            {
                if (Arabic_Number.Equals("0.00") || Arabic_Number.Equals("0") || Arabic_Number.Equals(".00") || Arabic_Number.Equals("") || Arabic_Number == null)
                {
                    return "0"; 
                }
                else
                {
                    return Arabic_Number.ToString();
                }
            }
            catch 
            {
                return "0";
            }
            
            //try
            //{
            //    if (Arabic_Number.Equals("0.00") || Arabic_Number.Equals("0") || Arabic_Number.Equals(".00") || Arabic_Number.Equals("") || Arabic_Number == null)
            //    {
            //        return "0";
            //    }
            //    else
            //    {
            //        StringBuilder thai_number = new StringBuilder(Arabic_Number);

            //        thai_number.Replace('1', '๑');
            //        thai_number.Replace('2', '๒');
            //        thai_number.Replace('3', '๓');
            //        thai_number.Replace('4', '๔');
            //        thai_number.Replace('5', '๕');
            //        thai_number.Replace('6', '๖');
            //        thai_number.Replace('7', '๗');
            //        thai_number.Replace('8', '๘');
            //        thai_number.Replace('9', '๙');
            //        thai_number.Replace('0', '๐');

            //        return thai_number.ToString();
            //    }
            //}
            //catch 
            //{
            //    return "0";
            //}
        }

        public string ToMonthName(String Month_Number)
        {
            if (Month_Number.Equals(null))
            {
                return "";
            }
            else
            {
                switch (Month_Number)
                {
                    case "01": { return "มกราคม"; }
                    case "02": { return "กุมภาพันธ์"; }
                    case "03": { return "มีนาคม"; }
                    case "04": { return "เมษายน"; }
                    case "05": { return "พฤษภาคม"; }
                    case "06": { return "มิถุนายน"; }
                    case "07": { return "กรกฎาคม"; }
                    case "08": { return "สิงหาคม"; }
                    case "09": { return "กันยายน"; }
                    case "10": { return "ตุลาคม"; }
                    case "11": { return "พฤศจิกายน"; }
                    case "12": { return "ธันวาคม"; }
                    default:
                    return Month_Number;
                }
            }
        }
    }
}
