using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace AdminToolService.CloseAccountStatus
{
    public class CloseAccountStatusBS
    {
        #region Method : GetCloseAccountStatusDisplay()
        public DataTable GetCloseAccountStatusDisplay(DateTime datetime, string status, string region)
        {
            try
            {
                CloseAccountStatusDA da = new CloseAccountStatusDA();
                DataTable dt = da.SelectCloseAccountStatusByDateTimeStatus(datetime, status, region);
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