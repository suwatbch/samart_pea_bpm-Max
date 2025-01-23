using System;
using System.Globalization;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.ePaymentsModule.Interface.Constants;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities;

namespace PEA.BPM.ePaymentsModule.Utilities
{
    public static class CommonUtilities
    {
        static EPayUploadItemCompare ePayCompare;

        public static void SortUploadItem(ref List<EpayUploadItem> ePaymentList, string sortColumn, string sortDirect)
        {
            ePayCompare = new EPayUploadItemCompare(sortColumn, sortDirect);
            ePaymentList.Sort(ePayCompare);
        }

        public static EpayUploadItem SearchSysItem(List<EpayUploadItem> ePayItemList, EpayUploadItem target)
        {
            int first = 0;
            int mid = 0;
            int last = ePayItemList.Count - 1;
            
            while (first <= last)
            {
                mid = (first + last) / 2;
                if (target.CaId.CompareTo(ePayItemList[mid].SysCaId) > 0)
                {
                    first = mid + 1;
                }
                else if (target.CaId.CompareTo(ePayItemList[mid].SysCaId) < 0)
                {
                    last = mid - 1;
                }
                else
                {
                    first = last + 1;
                }
            }
            return ePayItemList[mid];
        }

        public static string GetThaiMonth(int month)
        {
            string thaiMonth = "";

            switch (month)
            {
                case 1:
                    thaiMonth = "มกราคม";        
                    break;
                case 2:
                    thaiMonth = "กุมภาพันธ์";
                    break;
                case 3:
                    thaiMonth = "มีนาคม";
                    break;
                case 4:
                    thaiMonth = "เมษายน";
                    break;
                case 5:
                    thaiMonth = "พฤษภาคม";
                    break;
                case 6:
                    thaiMonth = "มิถุนายน";
                    break;
                case 7:
                    thaiMonth = "กรกฎาคม";
                    break;
                case 8:
                    thaiMonth = "สิงหาคม";
                    break;
                case 9:
                    thaiMonth = "กันยายน";
                    break;
                case 10:
                    thaiMonth = "ตุลาคม";
                    break;
                case 11:
                    thaiMonth = "พฤศจิกายน";
                    break;
                case 12:
                    thaiMonth = "ธันวาคม";
                    break;
                default:
                    thaiMonth = null;
                    break;
            }
            return thaiMonth;
        }

        public static string GetShortThaiMonth(int month)
        {
            string thaiMonth = "";

            switch (month)
            {
                case 1:
                    thaiMonth = "ม.ค.";
                    break;
                case 2:
                    thaiMonth = "ก.พ.";
                    break;
                case 3:
                    thaiMonth = "มี.ค.";
                    break;
                case 4:
                    thaiMonth = "เม.ย.";
                    break;
                case 5:
                    thaiMonth = "พ.ค.";
                    break;
                case 6:
                    thaiMonth = "มิ.ย.";
                    break;
                case 7:
                    thaiMonth = "ก.ค.";
                    break;
                case 8:
                    thaiMonth = "ส.ค.";
                    break;
                case 9:
                    thaiMonth = "ก.ย.";
                    break;
                case 10:
                    thaiMonth = "ต.ค.";
                    break;
                case 11:
                    thaiMonth = "พ.ย.";
                    break;
                case 12:
                    thaiMonth = "ธ.ค.";
                    break;
                default:
                    thaiMonth = null;
                    break;
            }
            return thaiMonth;
        }

        public static string GetPeriod(DateTime dt)
        {
            DateTimeFormatInfo _th_dt;
            CultureInfo th_culture = new CultureInfo("th-TH");
            _th_dt = th_culture.DateTimeFormat;
            return dt.ToString("MM/yyyy", _th_dt);
        }

    }
}
