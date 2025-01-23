using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.PaymentManagementModule.Interface.Services;
using PEA.BPM.PaymentManagementModule.Interface.BusinessEntities.Reports;
using PEA.BPM.PaymentManagementModule.DA;
using PEA.BPM.PaymentManagementModule.Interface.BusinessEntities;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;

namespace PEA.BPM.PaymentManagementModule.BS
{
    public class ReportBS : IReportService
    {
        #region IReportService Members

        public List<APReport> GetReportAP(APParam param)
        {
            ReportDA da = new ReportDA();
            return da.GetReportAP(param);
        }

        #endregion

    }
}
