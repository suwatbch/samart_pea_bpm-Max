using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace PEA.BPM.Architecture.CommonUtilities
{
    public class DaHelper
    {
        public static string GetString(DataRow dr, string columnName)
        {
            if (IsNull(dr, columnName))
            {
                return null;
            }
            else
            {
                return ((string)dr[columnName]).Trim();
            }
        }

        public static DateTime? GetDate(DataRow dr, string columnName)
        {
            if (IsNull(dr, columnName))
            {
                return null;
            }
            else
            {
                return (DateTime)dr[columnName];
            }
        }

        public static DateTime GetDateTime(DataRow dr, string columnName)
        {
            if (IsNull(dr, columnName))
            {
                //SqlDateTime.MinValue
                //return new DateTime(1753,1,1,12,0,0);
                return DateTime.MinValue;
            }
            else
            {
                return (DateTime)dr[columnName];
            }
        }


        public static int? GetInt(DataRow dr, string columnName)
        {
            if (IsNull(dr, columnName))
            {
                return null;
            }
            else
            {
                return (int)dr[columnName];
            }
        }

        public static short? GetShort(DataRow dr, string columnName)
        {
            if (IsNull(dr, columnName))
            {
                return null;
            }
            else
            {
                return (short)dr[columnName];
            }
        }

        public static byte? GetByte(DataRow dr, string columnName)
        {
            if (IsNull(dr, columnName))
            {
                return null;
            }
            else
            {
                return (byte)dr[columnName];
            }
        }

        public static Decimal? GetDecimal(DataRow dr, string columnName)
        {
            if (IsNull(dr, columnName))
            {
                return null;
            }
            else
            {
                return (Decimal)dr[columnName];
            }
        }

        private static bool IsNull(DataRow dr, string columnName)
        {
            return (!dr.Table.Columns.Contains(columnName) || dr.IsNull(columnName)) || DBNull.Value.Equals(dr[columnName]);
        }

        /// <summary>
        /// Input from yyyymm to mm/yyyy
        /// </summary>
        /// <param name="period"></param>
        /// <returns></returns>
        public static string GetBillPeriod(string period)
        {
            string retVal = String.Empty;
            if (period != null && period.Length == 6)
            {
                string year = period.Substring(0, 4);
                string month = period.Substring(4, 2);
                retVal = String.Format("{0}/{1}", month, year);
            }
            return retVal;
        }

        /// <summary>
        /// Input from mm/yyyy to yyyymm
        /// </summary>
        /// <param name="period"></param>
        /// <returns></returns>
        public static string SetBillPeriod(string period)
        {
            string retVal = String.Empty;
            if (period.Length == 7)
            {
                string[] temps = period.Split('/');
                if (temps.Length == 2)
                {
                    retVal = temps[1] + temps[0];
                }
            }
            return retVal;
        }

        public static string ToMoneyFormat(decimal? money)
        {
            if ((money == null) || (money == 0))
                return "0.00";
            else
                return money.Value.ToString("#,###.00");
            //bool minus = false;
            //if (money == null) return "0.00";            

            //string mstr = money.ToString();
            //if (mstr.Contains(",")) return mstr;

            //if (mstr.Contains("-"))
            //{
            //    minus = true;
            //    mstr = mstr.Replace("-", "");
            //}


            //char[] spliter = { '.' };
            //string [] sp = mstr.Split(spliter);
            //if (sp.Length == 1) return mstr + ".00";

            //string pred = null;
            //if (sp[1].Length > 2)
            //    pred = sp[1].Substring(0, 2);
            //else
            //    pred = sp[1].PadRight(2, '0');

            //string left = "";
            //int k = 0;
            //for (int i = sp[0].Length-1; i>=0; i--)
            //{
            //    left = sp[0][i] + left;

            //    k++;
            //    if ( ((k % 3) == 0) && i != 0)
            //        left = ","+ left;
                
            //}

            //string ret= string.Format("{0}.{1}", left, pred );

            //if(minus)
            //    ret = "-"+ret;

            //return ret;            
        }       
    }
}
