using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.PaymentManagementModule.Interface.BusinessEntities.Reports;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;

namespace PEA.BPM.PaymentManagementModule.Interface.Services
{
    public interface IReportService
    {
        List<APReport> GetReportAP(APParam param);
    }
}
