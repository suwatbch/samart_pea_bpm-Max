using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;
using PEA.BPM.Architecture.ArchitectureTool;
using System.Collections;

namespace PEA.BPM.BillPrintingModule.BillPrintingUtilities
{
    public class CustomValidation
    {
        public static bool IsValidNumeralChar(char value)
        {
            //plz insert pattern in double quote krub
            Regex rg = new Regex(""); 
            return rg.IsMatch(value.ToString());
        }

        public static bool IsValidAlphaNumeralChar(char value)
        {
            //plz insert pattern in double quote krub
            Regex rg = new Regex("");
            return rg.IsMatch(value.ToString());
        }
        
        public static bool ValidateDate(string date)
        {
            try
            {
                if (!string.IsNullOrEmpty(date))
                {
                    string[] tmp = date.Split('/');
                    if (Convert.ToInt16(tmp[tmp.Length - 1]) < 2500)
                        return false;
                }            

                IFormatProvider provider = CultureInfo.CreateSpecificCulture("th-TH");
                DateTime d = DateTime.Parse(date, provider);
                return true;              

            }
            catch
            {
                return false;
            }
        }

        public static bool ValidateDateExact(string date,string format)
        {
            try
            {
                IFormatProvider provider = CultureInfo.CreateSpecificCulture("th-TH");
                DateTime d = DateTime.ParseExact(date, format, provider);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static List<string> FindBranchByWildCard(string wcStr, List<string> branchList)
        {
            List<string> strList = new List<string>();
            Regex rg = new Regex(wcStr + "[0-9]*", RegexOptions.IgnoreCase);
            foreach (string b in branchList)
            {
                if (rg.IsMatch(b))
                    strList.Add(b);
            }

            return strList;
        }

        public static List<string> FindBranchByWildCard(string wcStr, List<Branch> branchList)
        {
            List<string> strList = new List<string>();
            Regex rg = new Regex(wcStr + "[0-9]*", RegexOptions.IgnoreCase);
            foreach (Branch b in branchList)
            {
                if (rg.IsMatch(b.BranchId))
                    strList.Add(b.BranchId);
            }

            return strList;
        }

        public static List<string> OnlyParentBranch(Branch branch)
        {
            List<string> strList = new List<string>();
            List<Branch> codeList = CodeTable.Instant.ListChildBranch(branch);

            foreach (Branch b in codeList)
            {
                string branchLevel = b.BranchLevel;
                if (branchLevel == "1" || branchLevel == "2" || branchLevel == "3")
                    strList.Add(b.BranchId);
            }

            return strList;
        }


    }
}
