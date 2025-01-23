using System;
using System.Globalization;

namespace PEA.BPM.Architecture.CommonUtilities
{

    public class DateFormatter
    {
        private static CultureInfo tCulture = new CultureInfo("th-TH", true);
        private static CultureInfo eCulture = new CultureInfo("en-AU", true);

        public static string ToDateThString(DateTime o)
        {
            if (DateTime.MinValue == o)
            {
                return null;
            }
            else
            {
                return o.ToString("dd/MM/yyyy", tCulture);
            }
        }

        public static string ToDateShortThString(DateTime o)
        {
            if (DateTime.MinValue == o)
            {
                return null;
            }
            else
            {
                return o.ToString("dd/MM/yy", tCulture);
            }
        }

        public static string ToDateTimeThString(DateTime o)
        {
            if (DateTime.MinValue == o)
            {
                return null;
            }
            else
            {
                return o.ToString("dd/MM/yyyy HH:mm:ss", tCulture);
            }
        }

        public static DateTime? ToEnDateTime(string o)
        {
            try
            {
                return DateTime.ParseExact(o, "yyyy/MM/dd", eCulture);
            }
            catch
            {
                return null;
            }
        }

        public static string ToYearThString(DateTime o)
        {
            if (DateTime.MinValue == o)
            {
                return null;
            }
            else
            {
                return o.ToString("yyyy", tCulture);
            }
        }

        public static string ToShortYearThString(DateTime o)
        {
            if (DateTime.MinValue == o)
            {
                return null;
            }
            else
            {
                return o.ToString("yy", tCulture);
            }
        }

        public static DateTime? PeriodToDateTime(string period)
        {
            try
            {
                string day = period.Substring(0, 2);
                string month = period.Substring(3, 4);
                return DateTime.Parse(String.Format("{0}/{1}/{2}", "1", day, month), tCulture);
            }
            catch
            {
                return null;
            }
        }

        public static DateTime? PeriodToDateTime2(string period)
        {
            try
            {
                string day = period.Substring(4, 2);
                string month = period.Substring(0, 4);
                return DateTime.Parse(String.Format("{0}/{1}/{2}", "1", day, month), tCulture);
            }
            catch
            {
                return null;
            }
        }
    }

   
}
