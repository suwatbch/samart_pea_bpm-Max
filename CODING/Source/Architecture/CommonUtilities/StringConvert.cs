using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.IO;

namespace PEA.BPM.Architecture.CommonUtilities
{
    public class StringConvert
    {
        // สระที่ไม่นับ ก่ก้ก๊ก๋ก็ก์กิกีกึกืกุกูกั
        protected static char[] noCountChar = new char[] { (char)3656, (char)3657, (char)3658, (char)3659, (char)3655, (char)3660, 
                (char)3636, (char)3637, (char)3638, (char)3639, (char)3640, (char)3641, (char)3633 };

        public static string PadRight(string value, int length)
        {
            string aX = value;

            for (int i = 0; i < noCountChar.Length; i++)
            {
                aX = aX.Replace(noCountChar[i].ToString(), "");
            }

            int space = length - aX.Length;
            if (space < 0) space = 0;
            
            string tmp = string.Format("{0}{1}", value, "".PadRight(space, ' '));

            if (aX.Length > length)
                tmp = tmp.Remove(length + (tmp.Length - aX.Length));

            return tmp;
            //return string.Format("{0}{1}", value, "".PadRight(space, ' ')); ;

        }

        public static string PadLeft(string value, int length)
        {
            string aX = value;

            for (int i = 0; i < noCountChar.Length; i++)
            {
                aX = aX.Replace(noCountChar[i].ToString(), "");
            }

            int space = length - aX.Length;
            if (space < 0) space = 0;

            return string.Format("{0}{1}", "".PadLeft(space, ' '), value);
        }

        public static string FormatText(string input, int len)
        {
            if (input.Length > len)
            {
                try
                {
                    string txt = input;

                    for (int i = 0; i < noCountChar.Length; i++)
                        txt = txt.Replace(noCountChar[i].ToString(), "");

                    int diff = input.Length - txt.Length;
                    string tmp = input.Substring(0, (len - 1) + diff);
                    int tmpIndex = tmp.LastIndexOf(' ');

                    StringBuilder sb = new StringBuilder();
                    sb.Append(input.Substring(0, tmpIndex));
                    sb.Append("\r\n");
                    sb.Append(input.Substring(tmpIndex, input.Length - tmpIndex));
                    sb.Append("\r\n");
                    return sb.ToString();
                }
                catch
                {
                    return input;
                }
            }
            else
                return input;
        }

        public static int TextLength(string input)
        {
            try
            {
                string txt = input;

                for (int i = 0; i < noCountChar.Length; i++)
                    txt = txt.Replace(noCountChar[i].ToString(), "");
                return txt.Length;
            }
            catch
            {
                return input.Length;
            }
        }

        public static int? ToInt32(string value)
        {
            try
            {
                return Convert.ToInt32(value);
            }
            catch
            {
                return null;
            }
        }

        public static decimal? ToDecimal(string value)
        {
            try
            {
                return Convert.ToDecimal(value);
            }
            catch
            {
                return null;
            }
        }

        public static string ToString(string value)
        {
            try
            {
                value = value.Trim();

                if (value != string.Empty)
                    return value;
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }

        public static string FormatPeriod(string period)
        {
            if (period != null && period.Length == 6)
            {
                string year = period.Substring(0, 4);
                string month = period.Substring(4, 2);

                return string.Format("{0}/{1}", month, year);
            }
            else
            {
                return "-";
            }
        }

        public static string FormatPeriodThai(string period)
        {
            string[] months = new string[]{"ม.ค.", "ก.พ.","มี.ค.","เม.ย.",
            "พ.ค.","มิ.ย.","ก.ค.","ส.ค.",
            "ก.ย.","ต.ค.","พ.ย.","ธ.ค."};

            if (period.Length == 6)
            {
                int year = Convert.ToInt32(period.Substring(0, 4));
                int month = Convert.ToInt32(period.Substring(4, 2));

                return string.Format("{0} {1}", months[month - 1], year);
            }
            else
            {
                return "-";
            }
        }

        public static string GetMonthName(int monthNo)
        {
            string retVal = String.Empty;
            switch (monthNo)
            {
                case 1:
                    retVal = "มกราคม";
                    break;
                case 2:
                    retVal = "กุมภาพันธ์";
                    break;
                case 3:
                    retVal = "มีนาคม";
                    break;
                case 4:
                    retVal = "เมษายน";
                    break;
                case 5:
                    retVal = "พฤษภาคม";
                    break;
                case 6:
                    retVal = "มิถุนายน";
                    break;
                case 7:
                    retVal = "กรกฏาคม";
                    break;
                case 8:
                    retVal = "สิงหาคม";
                    break;
                case 9:
                    retVal = "กันยายน";
                    break;
                case 10:
                    retVal = "ตุลาคม";
                    break;
                case 11:
                    retVal = "พฤศจิกายน";
                    break;
                case 12:
                    retVal = "ธันวาคม";
                    break;
                default:
                    retVal = "-";
                    break;
            }
            return retVal;
        }

        public static string UnFormatPeriod(string period)
        {
            string[] elements = period.Split('/');
            if (elements.Length == 2)
            {
                string month = elements[0];
                string year = elements[1];
                if (year.Length == 2)
                {
                    year = "25" + year;
                }

                return string.Format("{0}{1}", year, month);
            }
            else
            {
                return "000000";
            }
        }

        public static DateTime? ToDateTime(string dateTime)
        {
            try
            {
                if (dateTime.Length == 10)
                {
                    IFormatProvider provider = CultureInfo.CreateSpecificCulture("th-TH");

                    DateTime dt = Convert.ToDateTime(dateTime, provider);
                    if (dt.Year < 1900 || dt.Year > 9999)
                    {
                        return null;
                    }
                    else
                    {
                        return dt;
                    }
                }
                else  if (dateTime.Length == 8)
                    {
                        int year = Convert.ToInt16(dateTime.Substring(0, 4));

                        if (year != 9999) // ยกเว้น 1 case ถ้า SAP ส่งมาเป็น 9999 ให้ถือว่าเป็น ค.ศ. เสมอ
                        {
                            if (year < 1900 || year > 2500) // มากกว่า 2500 สันนิฐานว่าส่งมาเป็น พ.ศ. ซึ่งผิด ต้องส่งมาเป็น ค.ศ. เท่านั้น
                                return null;
                        }

                        int month = Convert.ToInt16(dateTime.Substring(4, 2));
                        int day = Convert.ToInt16(dateTime.Substring(6, 2));
                        DateTime? dt = new DateTime(year, month, day);
                        return dt;
                    }
                    else
                        return null;
            }
            catch
            {
                return null;
            }

        }

        public static DateTime? ToDateTime(string dateTime, IFormatProvider provider)
        {
            try
            {
                string year = dateTime.Substring(0, 4);
                string month = dateTime.Substring(4, 2);
                string day = dateTime.Substring(6, 2);
                string hour = "00";
                string minute = "00";
                string second = "00";

                if (dateTime.Trim().Length > 8)
                {
                    hour = dateTime.Substring(8, 2);
                    minute = dateTime.Substring(10, 2);
                    second = dateTime.Substring(12, 2);
                }

                DateTime dt = Convert.ToDateTime(
                    String.Format("{0}/{1}/{2} {3}:{4}:{5}", year, month, day, hour, minute, second),
                    provider);

                if (dt.Year < 1900 || dt.Year > 9999)
                {
                    return null;
                }
                else
                {
                    return dt;
                }
              
            }
            catch
            {
                return null;
            }

        }

        public static string ConvertAmountToText(string money)
        {
            money = Convert.ToDecimal(money).ToString("#0.00");

            if (money.Trim() == "0.00" || money.Trim() == "0")
                return "ศูนย์บาทถ้วน";

            string[] thNum = { "ศูนย์", "หนึ่ง", "สอง", "สาม", "สี่", "ห้า", "หก", "เจ็ด", "แปด", "เก้า" };
            string[] thOrder = { "", "สิบ", "ร้อย", "พัน", "หมื่น", "แสน", "ล้าน" };
            string baht = "บาท";
            string stang = "สตางค์";
            string tuan = "ถ้วน";
            string part = "";
            string remainpart;
            string strValue = "";
            ulong v1, v2;
            int remain;

            money = money.Replace(",", "");
            char[] seperator = new char[] { '.' };
            string[] tmp = money.Split(seperator, StringSplitOptions.RemoveEmptyEntries);
            if (tmp.Length > 1)
            {
                v1 = System.Convert.ToUInt64(tmp[0]);
                v2 = System.Convert.ToUInt64(tmp[1]);
            }
            else
            {
                v1 = System.Convert.ToUInt64(tmp[0]);
                v2 = 0;
            }

            while (v1 >= 1000000)
            {
                remain = (int)(v1 % 1000000);
                v1 = v1 / 1000000;

                if ((remainpart = StringConvert.ConvBaht(remain, thNum, thOrder)) != "")
                    strValue = remainpart + part + strValue;
                part += thOrder[thOrder.Length - 1];
            }

            strValue = StringConvert.ConvBaht((int)v1, thNum, thOrder) + part + strValue;
            if (strValue != "") strValue += baht;

            if (v2 == 0)
                strValue += tuan;
            else
                strValue += (StringConvert.ConvBaht((int)v2, thNum, thOrder) + stang);

            return strValue;
        }

        private static string ConvBaht(int num, string[] thNum, string[] thOrder)
        {
            int i, n;
            string result = "";
            string ed = "เอ็ด";
            string yi = "ยี่";

            i = 0;

            while (num > 0)
            {
                n = num % 10;

                if (n > 0)
                {
                    result = thOrder[i] + result;

                    switch (i * 10 + n)
                    {
                        case 1:
                            if (num > 9)
                                result = ed + result;
                            else
                                result = thNum[n] + result;
                            break;
                        case 11:
                            break;
                        case 12:
                            result = yi + result;
                            break;
                        default:
                            result = thNum[n] + result;
                            break;
                    }
                }

                num = num / 10;
                i++;
            }

            return result;
        }

        /// <summary>
        /// Get diff month between period1 and period (Period2-1) 2.Format period is yyyymm
        /// </summary>
        /// <param name="period1"></param>
        /// <param name="period2"></param>
        /// <returns>int number of differance period</returns>
        public static int DiffPeriod(string period1, string period2)
        {
            if (period1.Length == 6 & period2.Length == 6)
            {
                int yearP1 = Convert.ToInt32(period1.Substring(0, 4));
                int monthP1 = Convert.ToInt32(period1.Substring(4, 2));

                int yearP2 = Convert.ToInt32(period2.Substring(0, 4));
                int monthP2 = Convert.ToInt32(period2.Substring(4, 2));

                DateTime P1 = new DateTime(yearP1, monthP1, 1);
                DateTime P2 = new DateTime(yearP2, monthP2, 1);

                int countMonth = 0;
                while (P1 < P2)
                {
                    countMonth = countMonth + 1;
                    P1 = P1.AddMonths(1);
                }

                return countMonth;
            }
            else
            {
                throw new Exception("Invalide period formation");
            }
        }

        /// <summary>
        /// Format MRU in list of string to display in report
        /// </summary>
        /// <param name="period1"></param>
        /// <param name="period2"></param>
        /// <returns>string</returns>
        public static string FormatMru(List<String> listString)
        {
            listString.Sort();

            List<Int32> list = new List<Int32>();
            List<Int32> list2 = new List<Int32>();
            StringBuilder sb = new StringBuilder();

            foreach (String s in listString)
                list.Add(Convert.ToInt32(s));

            for (int i = 0; i < list.Count; i++)
            {
                list2.Add(list[i]);

                if (i + 1 != list.Count && list[i + 1] == list[i] + 1)
                    continue;

                if (list2.Count > 1)
                {
                    sb.Append(list2[0].ToString().PadLeft(4, '0') + "-" + list2[list2.Count - 1].ToString().PadLeft(4, '0') + ",  ");
                }
                else
                {
                    sb.Append(list2[0].ToString().PadLeft(4, '0') + ",  ");
                }

                list2.Clear();
            }

            string result = sb.ToString();
            
            if (result.EndsWith(",  "))
                result = result.Remove(result.Length - 3, 3);

            return result;
        }

        public static string InsertComma(string input, int length)
        {
            bool _minusFlag = false;
            string _oriInput = input;

            if (input.Contains("-"))
            {
                _minusFlag = true;
                string tmp = input.TrimStart('-');
                input = tmp;
            }

            if (!String.IsNullOrEmpty(input))
            {
                string integer = string.Empty;
                string portion = string.Empty;
                string b = string.Empty;

                if (input.Contains("."))
                {
                    integer = input.Substring(0, input.IndexOf('.'));
                    portion = input.Substring(input.IndexOf('.'), input.Length - input.IndexOf('.'));
                }
                else
                {
                    integer = input;
                    portion = ".00";
                }

                char[] a = integer.ToCharArray();
                int count = 1;

                for (int i = a.Length - 1; i >= 0; i--)
                {
                    if (count % 3 == 0)
                    {
                        b += a[i] + ",";
                        count = 0;
                    }
                    else
                        b += a[i];

                    count++;
                }

                a = b.ToCharArray();
                b = string.Empty;

                for (int i = a.Length - 1; i >= 0; i--)
                    b += a[i];

                if (!_minusFlag)
                    input = b.StartsWith(",") ? b.Remove(0, 1) + portion : b + portion;
                else
                    input = b.StartsWith(",") ? "-" + b.Remove(0, 1) + portion : "-" + b + portion;
            }
            else
                return string.Empty;

            if (input.Length > length)
                return _oriInput;
            else
                return input;
        }

        public static string InsertComma(string input)
        {
            bool _minusFlag = false;

            if (input.Contains("-"))
            {
                _minusFlag = true;
                string tmp = input.TrimStart('-');
                input = tmp;
            }

            if (!String.IsNullOrEmpty(input))
            {
                string integer = string.Empty;
                string portion = string.Empty;
                string b = string.Empty;

                if (input.Contains("."))
                {
                    integer = input.Substring(0, input.IndexOf('.'));
                    portion = input.Substring(input.IndexOf('.'), input.Length - input.IndexOf('.'));
                }
                else
                {
                    integer = input;
                    portion = ".00";
                }

                char[] a = integer.ToCharArray();
                int count = 1;

                for (int i = a.Length - 1; i >= 0; i--)
                {
                    if (count % 3 == 0)
                    {
                        b += a[i] + ",";
                        count = 0;
                    }
                    else
                        b += a[i];

                    count++;
                }

                a = b.ToCharArray();
                b = string.Empty;

                for (int i = a.Length - 1; i >= 0; i--)
                    b += a[i];

                if (!_minusFlag)
                    return b.StartsWith(",") ? b.Remove(0, 1) + portion : b + portion;
                else
                    return b.StartsWith(",") ? "-" + b.Remove(0, 1) + portion : "-" + b + portion;
            }
            else
                return string.Empty;
        }

        public static string FormatTextIgnoreSpace(string input, int len)
        {
            if (input.Length > len)
            {
                try
                {
                    string txt = input;

                    for (int i = 0; i < noCountChar.Length; i++)
                        txt = txt.Replace(noCountChar[i].ToString(), "");

                    int diff = input.Length - txt.Length;
                    string tmp = input.Substring(0, (len - 1) + diff);
                    int tmpIndex = tmp.LastIndexOf(' ');

                    StringBuilder sb = new StringBuilder();
                    sb.Append(input.Substring(0, len));
                    sb.Append("\r\n");
                    sb.Append(input.Substring(tmpIndex, input.Length - tmpIndex));
                    sb.Append("\r\n");
                    return sb.ToString();
                }
                catch
                {
                    return input;
                }
            }
            else
                return input;
        }

    }
}
