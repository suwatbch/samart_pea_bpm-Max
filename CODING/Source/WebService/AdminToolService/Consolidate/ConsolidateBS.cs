using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace AdminToolService.Consolidate
{
    public class ConsolidateBS
    {
        #region Method : GetConsolidateDisplay()
        public DataTable GetConsolidateDisplay(DateTime datetime)
        {
            try
            {
                ConsolidateDA da = new ConsolidateDA();
                DataTable dt = da.SelectConsolidateByDateTime(datetime);
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