using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.PaymentCollectionModule.Interface.Services;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Reports;
using PEA.BPM.PaymentCollectionModule.DA;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;

namespace PEA.BPM.PaymentCollectionModule.BS
{
    public class ReportBS: IReportService
    {
        #region IReportService Members
        public List<CAC01Report> GetReportCAC01(CAC01Param param)
        {
            ReportDA da = new ReportDA();
            return da.GetReportCAC01(param);
        }

        public List<CAC03Report> GetReportCAC03(CAC01Param param)
        {
            ReportDA da = new ReportDA();
            return da.GetReportCAC03(param);
        }

        public List<CAC04Report> GetReportCAC04(CAC01Param param)
        {
            ReportDA da = new ReportDA();
            return da.GetReportCAC04(param);
        }

        public List<CAC05Report> GetReportCAC05(CAC06Param param)
        {
            ReportDA da = new ReportDA();
            return da.GetReportCAC05(param);
        }

        public List<CAC06Report> GetReportCAC06(CAC06Param param)
        {
            ReportDA da = new ReportDA();
            return da.GetReportCAC06(param);
        }

        public List<CAC06Report> GetReportCAC07(CAC06Param param)
        {
            ReportDA da = new ReportDA();
            return da.GetReportCAC07(param);
        }

        public List<CAC09Report> GetReportCAC09(CAC09Param param)
        {
            ReportDA da = new ReportDA();
            return da.GetReportCAC09(param);
        }

        public List<CAC11Report> GetReportCAC11(CAC11Param param)
        {
            ReportDA da = new ReportDA();
            return da.GetReportCAC11(param);
        }

        public List<CAC12Report> GetReportCAC12(CAC09Param param)
        {
            ReportDA da = new ReportDA();
            return da.GetReportCAC12(param);
        }

        public List<CAC13Report> GetReportCAC13(CAC11Param param)
        {
            ReportDA da = new ReportDA();
            return da.GetReportCAC13(param);
        }

        public List<CAC14Report> GetReportCAC14(CAC14Param param)
        {
            ReportDA da = new ReportDA();
            return da.GetReportCAC14(param);
        }

        // DCR QR Payment 2023-03 : Report 2.18 
        public List<CAC18Report> GetReportCAC18(CAC18Param param)
        {
            ReportDA da = new ReportDA();
            return da.GetReportCAC18(param);
        }

        public List<CAC19Report> GetReportCAC18(CAC19Param param)
        {
            ReportDA da = new ReportDA();
            return da.GetReportCAC19(param);
        }


        //TODO: INSTALLMENT CASE
        //public List<CAC16Report> GetReportCAC16(CAC16Param param)
        //{
        //    ReportDA da = new ReportDA();
        //    return da.GetReportCAC16(param);
        //}

        //public List<APReport> GetReportAP(APParam param)
        //{
        //    ReportDA da = new ReportDA();
        //    return da.GetReportAP(param);
        //}

        //public List<Branch> GetBranchDetail(string branchId)
        //{
        //        BillingDA da = new BillingDA();
        //        return da.GetBranchDetail(branchId);
        //}

        #endregion


        #region IReportService Members


        public List<CAC19Report> GetReportCAC19(CAC19Param param)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
