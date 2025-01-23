using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace AdminToolService.OfflineErrorLog
{
    public class OfflineErrorLogBS
    {
        #region Method : GetOfflineErrorLogDisplay()
        public DataTable GetOfflineErrorLogDisplay(DateTime datetime, string active)
        {
            try
            {
                OfflineErrorLogDA da = new OfflineErrorLogDA();
                DataTable dt = da.SelectOfflineErrorLogByDateTimeActive(datetime, active);
                return dt;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public void UpdateLogStatus(string fileName, string status)
        {
            try
            {
                OfflineErrorLogDA da = new OfflineErrorLogDA();
                da.UpdateLogStatus(fileName, status);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        #endregion
    }
}