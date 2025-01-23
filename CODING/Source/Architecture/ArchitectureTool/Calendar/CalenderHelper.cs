using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;
using System.Collections;
using System.Globalization;

namespace PEA.BPM.Architecture.ArchitectureTool
{
    public class CalenderHelper
    {
        DateTimeFormatInfo _th_dt;
        Hashtable _holidays = new Hashtable();
        List<CalendarInfo> _calendar;

        public CalenderHelper()
        {
            _calendar = CodeTable.Instant.ListCendar();
            initHoliday();
        }

        public bool IsNonWorkingDay(DateTime date)
        {
            CultureInfo th_culture = new CultureInfo("th-TH");
            _th_dt = th_culture.DateTimeFormat;
            string tempDate = date.ToString("yyyyMMdd", _th_dt);
            return _holidays.Contains(tempDate);
        }


        private void initHoliday()
        {
            foreach (PEA.BPM.Infrastructure.Interface.BusinessEntities.CalendarInfo ca in _calendar)
            {
                _holidays.Add(ca.NWDay, ca.NWDay);
            }
        }

        public int NumberOfBusinessDays(DateTime startDate, DateTime endDate)
        {
            int numberOfBusiness = 0;
            if (startDate == endDate) return 0;
            if (startDate < endDate)
            {
                while (startDate < endDate)
                {
                    CultureInfo th_culture = new CultureInfo("th-TH");
                    _th_dt = th_culture.DateTimeFormat;
                    string tempDate = startDate.ToString("yyyyMMdd", _th_dt);

                    if (!_holidays.Contains(tempDate))
                    {
                        numberOfBusiness += 1;
                    }
                    startDate = startDate.AddDays(1);
                }
            }
            else 
            {
                while (startDate > endDate )
                {
                    CultureInfo th_culture = new CultureInfo("th-TH");
                    _th_dt = th_culture.DateTimeFormat;
                    string tempDate = startDate.ToString("yyyyMMdd", _th_dt);

                    if (!_holidays.Contains(tempDate))
                    {
                        numberOfBusiness -= 1;
                    }
                    startDate = startDate.AddDays(-1);
                }
            }
            return numberOfBusiness;
        }

        //public int NumberOfFineDays(DateTime startDate, DateTime endDate)
        //{
        //    if (startDate == endDate) return 0;
        //    if (startDate > endDate) return 0;
        //    while (IsNonWorkingDay(startDate))
        //    {
        //        startDate = startDate.AddDays(1);
        //    }
        //    TimeSpan diffDate = endDate - startDate;
        //    return (int)diffDate.TotalDays;
        //}

        public DateTime GetFirstWorkingDay(DateTime dueDate)
        {
            while (IsNonWorkingDay(dueDate))
            {
                dueDate = dueDate.AddDays(1);
            }

            return dueDate;
        }

        public DateTime NextBusinessDay(DateTime startDate, int days)
        {
            DateTime date = startDate;
            CultureInfo th_culture = new CultureInfo("th-TH");
            _th_dt = th_culture.DateTimeFormat;
            string tempDate = String.Empty;
            int countDay = 0;
            while (countDay < days) 
            {               
                date = date.AddDays(1.0);
                tempDate = date.ToString("yyyyMMdd", _th_dt);
                if (!_holidays.ContainsValue(tempDate))
                {
                    countDay += 1;
                }
            }           
            return date;
        }


       
        public DateTime PreviousBusinessDay(DateTime endDate, int days)
        {
            DateTime date = endDate;
            CultureInfo th_culture = new CultureInfo("th-TH");
            _th_dt = th_culture.DateTimeFormat;
            string tempDate = String.Empty;
            int countDay = 0;
            while(countDay < days)
            {
                date = date.AddDays(-1.0);
                tempDate = date.ToString("yyyyMMdd", _th_dt);
                if (!_holidays.Contains(tempDate))
                {
                    countDay +=1;
                }
            }           
            return date;
        }
    }
}
