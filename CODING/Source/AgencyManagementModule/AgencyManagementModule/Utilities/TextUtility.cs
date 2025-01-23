using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using PEA.BPM.AgencyManagementModule.Properties;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.AgencyManagementModule.Utilities
{
    public class TextUtility
    {
        public static bool IsValidNumeralChar(char value)
        {
            Regex rg = new Regex(Resources.ValidNumeralChar);
            return rg.IsMatch(value.ToString());
        }

        public static bool IsValidNumeralCurrency(string value)
        {
            return true;
        }

        //public static bool IsValidNumeralDashChar(char value)
        //{
        //    Regex rg = new Regex(Resources.ValidNumeralDashChar);
        //    return rg.IsMatch(value.ToString());
        //}

        //public static bool IsValidNumeralDashSlashChar(char value)
        //{
        //    Regex rg = new Regex(Resources.ValidNumeralDashSlashChar);
        //    return rg.IsMatch(value.ToString());
        //}

        //public static string ConvertToShortThaiDateTime(DateTime? dateValue)
        //{
        //    string retVal = String.Empty;
        //    try
        //    {
        //        if (dateValue != null)
        //        {
        //            DateTime dt = (DateTime)dateValue;
        //            DateTimeFormatInfo _th_dt;
        //            CultureInfo th_culture = new CultureInfo("th-TH");
        //            _th_dt = th_culture.DateTimeFormat;
        //            retVal = dt.ToString("dd/MM/yyyy", _th_dt);
        //        }
        //    }
        //    catch { }        
        //    return retVal;
        //}

        public static string MapBranch(string digit)
        {
            string ret = null;
            if (digit == "001")
                ret = "A";
            else if (digit == "002")
                ret = "B";
            else if (digit == "003")
                ret = "C";
            else if (digit == "004")
                ret = "D";
            else if (digit == "005")
                ret = "E";
            else if (digit == "006")
                ret = "F";
            else if (digit == "007")
                ret = "G";
            else if (digit == "008")
                ret = "H";
            else if (digit == "009")
                ret = "I";
            else if (digit == "010")
                ret = "J";
            else if (digit == "011")
                ret = "K";
            else if (digit == "012")
                ret = "L";
            

            return ret;
        }

        public static bool IsInPeriod(string input, int range)
        {
            CultureInfo th_culture = new CultureInfo("th-TH");
            DateTimeFormatInfo th_dt = th_culture.DateTimeFormat;

            char[] splitter = { '/' };
            string[] sp = input.Split(splitter);
            if (sp[0].Length != 2) return false;
            if (sp[1].Length != 4) return false;
            if (sp.Length != 2) return false;
            string dt = "";
            if (th_dt.NativeCalendarName != "¾Ø·¸ÈÑ¡ÃÒª")
                dt = Session.BpmDateTime.Now.AddYears(543).ToString("MMyyyy", th_dt);
            else
                dt = Session.BpmDateTime.Now.ToString("MMyyyy", th_dt);

            try
            {
                int cmonth = Convert.ToInt32(dt.Substring(0, 2));
                int cyear = Convert.ToInt32(dt.Substring(2, 4));

                int month = Convert.ToInt32(sp[0]);
                int year = Convert.ToInt32(sp[1]);

                if (cyear == year)
                {
                    if ((cmonth == month) || (month >= (cmonth - range)))
                        return true;
                    else
                        return false;
                }
                else if ((cyear - 1) == year)
                {
                    if ((cmonth == 1) && (month == 12))
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsCurrentPeriod(string input)
        {
            CultureInfo th_culture = new CultureInfo("th-TH");
            DateTimeFormatInfo th_dt = th_culture.DateTimeFormat;

            char[] splitter = { '/' };
            string[] sp = input.Split(splitter);
            if (sp[0].Length != 2) return false;
            if (sp[1].Length != 4) return false;
            if (sp.Length != 2) return false;
            string dt = "";
            if (th_dt.NativeCalendarName != "¾Ø·¸ÈÑ¡ÃÒª")
                dt = Session.BpmDateTime.Now.AddYears(543).ToString("MMyyyy", th_dt);
            else
                dt = Session.BpmDateTime.Now.ToString("MMyyyy", th_dt);

            try
            {
                int cmonth = Convert.ToInt32(dt.Substring(0, 2));
                int cyear = Convert.ToInt32(dt.Substring(2, 4));

                int month = Convert.ToInt32(sp[0]);
                int year = Convert.ToInt32(sp[1]);

                if (cyear == year)
                {
                    if ((cmonth == month) || (month == (cmonth - 1)))
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsValidPeriod(string input)
        {
            CultureInfo th_culture = new CultureInfo("th-TH");
            DateTimeFormatInfo th_dt = th_culture.DateTimeFormat;

            char[] splitter = { '/' };
            string[] sp = input.Split(splitter);
            if (sp[0].Length != 2) return false;
            if (sp[1].Length != 4) return false;
            if (sp.Length != 2) return false;
            string dt = "";
            if (th_dt.NativeCalendarName != "¾Ø·¸ÈÑ¡ÃÒª")
                dt = Session.BpmDateTime.Now.AddYears(543).ToString("MMyyyy", th_dt);
            else
                dt = Session.BpmDateTime.Now.ToString("MMyyyy", th_dt);

            try
            {
                int cmonth = Convert.ToInt32(dt.Substring(0, 2));
                int cyear = Convert.ToInt32(dt.Substring(2, 4));

                int month = Convert.ToInt32(sp[0]);
                int year = Convert.ToInt32(sp[1]);

                if (month == 0 || year == 0) return false;
                if (year < 2500) return false;

                if (year > cyear)
                    return false;
                else if (cyear == year && cmonth < month)
                    return false;
                else
                    return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsNumeric(string strTextEntry)
        {
            strTextEntry = strTextEntry.Replace(",", "").Replace(".", "").Trim();
            Regex objNotWholePattern = new Regex("[^0-9]");
            return !objNotWholePattern.IsMatch(strTextEntry);
        }
    }
}
