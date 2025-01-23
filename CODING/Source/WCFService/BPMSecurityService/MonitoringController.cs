using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using PEA.BPM.WebService.Security.Cashier;
using System.Collections.Generic;

namespace PEA.BPM.WebService.Security
{
    [System.ComponentModel.DataObject]
    public class MonitoringController
    {

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select)]
        public List<string> CASGetCashierIdList()
        {
            return CashierCachingController.Instance.GetCashierIdList();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select)]
        public CachingCashierWorkStatus[] CASGetCachingCashierWorkStatusByCashierId(string cashierid)
        {
            return CashierCachingController.Instance.GetCachingCashierWorkStatusByCashierId(cashierid);
        }
    }
}
