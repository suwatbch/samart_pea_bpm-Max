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
                    thaiMonth = "���Ҥ�";        
                    break;
                case 2:
                    thaiMonth = "����Ҿѹ��";
                    break;
                case 3:
                    thaiMonth = "�չҤ�";
                    break;
                case 4:
                    thaiMonth = "����¹";
                    break;
                case 5:
                    thaiMonth = "����Ҥ�";
                    break;
                case 6:
                    thaiMonth = "�Զع�¹";
                    break;
                case 7:
                    thaiMonth = "�á�Ҥ�";
                    break;
                case 8:
                    thaiMonth = "�ԧ�Ҥ�";
                    break;
                case 9:
                    thaiMonth = "�ѹ��¹";
                    break;
                case 10:
                    thaiMonth = "���Ҥ�";
                    break;
                case 11:
                    thaiMonth = "��Ȩԡ�¹";
                    break;
                case 12:
                    thaiMonth = "�ѹ�Ҥ�";
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
                    thaiMonth = "�.�.";
                    break;
                case 2:
                    thaiMonth = "�.�.";
                    break;
                case 3:
                    thaiMonth = "��.�.";
                    break;
                case 4:
                    thaiMonth = "��.�.";
                    break;
                case 5:
                    thaiMonth = "�.�.";
                    break;
                case 6:
                    thaiMonth = "��.�.";
                    break;
                case 7:
                    thaiMonth = "�.�.";
                    break;
                case 8:
                    thaiMonth = "�.�.";
                    break;
                case 9:
                    thaiMonth = "�.�.";
                    break;
                case 10:
                    thaiMonth = "�.�.";
                    break;
                case 11:
                    thaiMonth = "�.�.";
                    break;
                case 12:
                    thaiMonth = "�.�.";
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
