using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities.ReceiptPrinting;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities.Reports;

namespace PEA.BPM.ePaymentsModule.Interface.Services
{
    public interface IReportService
    {
        List<RE01_ReportInfo> GetRe01ReportService(RE01Param param);
        List<RE02_ReportInfo> GetRe02ReportService(RE02ParamInfo param);
        List<RE03_ReportInfo> GetRe03ReportService(RE03ParamInfo param);
        List<RE04_ReportInfo> GetRe04ReportService(RE04ParamInfo param);
        List<RE05_ReportInfo> GetRe05ReportService(RE05ParamInfo param);
        List<RE06_ReportInfo> GetRe06ReportInfo(RE06ParamInfo param);
        List<RE07_ReportInfo> GetRe07ReportInfo(RE07ParamInfo param);
        List<RE08_ReportInfo> GetRe08ReportInfo(RE08ParamInfo param);
        List<RE09_ReportInfo> GetRe09ReportInfo(RE09ParamInfo param);
    }
}
 