using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Reports;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;

namespace PEA.BPM.PaymentCollectionModule.Interface.Services
{
    public interface IReportService
    {
        List<CAC01Report> GetReportCAC01(CAC01Param param);
        List<CAC03Report> GetReportCAC03(CAC01Param param);
        List<CAC04Report> GetReportCAC04(CAC01Param param);
        List<CAC05Report> GetReportCAC05(CAC06Param param);
        List<CAC06Report> GetReportCAC06(CAC06Param param);
        List<CAC06Report> GetReportCAC07(CAC06Param param);
        List<CAC09Report> GetReportCAC09(CAC09Param param);
        List<CAC11Report> GetReportCAC11(CAC11Param param);
        List<CAC12Report> GetReportCAC12(CAC09Param param);
        List<CAC13Report> GetReportCAC13(CAC11Param param);
        List<CAC14Report> GetReportCAC14(CAC14Param param);

        //DCR QR Payment 2023-03 : Report 2.18
        List<CAC18Report> GetReportCAC18(CAC18Param param);
        List<CAC19Report> GetReportCAC19(CAC19Param param);

        //TODO: INSTALLMENT CASE
        //List<CAC16Report> GetReportCAC16(CAC16Param param);
        //List<APReport> GetReportAP(APParam param);
        //List<Branch> GetBranchDetail(string branchId);
    }
}
