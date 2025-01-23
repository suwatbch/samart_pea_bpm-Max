using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace AdminToolService.OutOfShift
{
    public class OutOfShiftBS
    {
        #region Method : SelectOutOfShiftByDateTime

        public DataTable SelectOutOfShift(DateTime startDt, DateTime endDt, string caseCode)
        {
            try
            {
                OutOfShiftDA da = new OutOfShiftDA();
                DataTable dt = da.SelectOutOfShift(startDt, endDt, caseCode);
                return dt;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public DateTime GetDBDateTime()
        {
            try
            {
                OutOfShiftDA da = new OutOfShiftDA();
                DateTime dtTime = da.GetDBDateTime();
                return dtTime;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public DataTable SelectCountOutOfShift(DateTime stDt, string caseCode)
        {
            try
            {
                OutOfShiftDA da = new OutOfShiftDA();
                DataTable dt = da.SelectCountOutOfShift(stDt, caseCode);
                return dt;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        #endregion
    }
}
