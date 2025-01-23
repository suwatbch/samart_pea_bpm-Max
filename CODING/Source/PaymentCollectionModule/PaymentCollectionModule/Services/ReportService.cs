using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.PaymentCollectionModule.Interface.Services;
using Microsoft.Practices.CompositeUI;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using System.Threading;
using System.Collections;
using System.Data;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Reports;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;
using PaymentCollectionModule.SG;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.PaymentCollectionModule.Services
{
    [Service(typeof(IReportService))]
    public class ReportService: IReportService
    {
        #region Service Factory
        public IReportService GetService()
        {
            return GetService(false);
        }

        public IReportService GetService(bool serverService)
        {
            if (serverService || Session.Branch.OnlineConnection)
            {
                return new ReportSG(true);
            }
            else
            {
                return new ReportSG(false);
            }
        }
        #endregion


        public List<CAC01Report> GetReportCAC01(CAC01Param param)
        {
            return GetService((param.BranchId != Session.Branch.Id) ? true : false).GetReportCAC01(param);
        }

        public List<CAC03Report> GetReportCAC03(CAC01Param param)
        {
            return GetService((param.BranchId != Session.Branch.Id) ? true : false).GetReportCAC03(param);
        }

        public List<CAC04Report> GetReportCAC04(CAC01Param param)
        {
            return GetService((param.BranchId != Session.Branch.Id) ? true : false).GetReportCAC04(param);
        }

        public List<CAC05Report> GetReportCAC05(CAC06Param param)
        {
            return GetService((param.BranchId != Session.Branch.Id) ? true : false).GetReportCAC05(param);
        }

        public List<CAC06Report> GetReportCAC06(CAC06Param param)
        {
            return GetService((param.BranchId != Session.Branch.Id) ? true : false).GetReportCAC06(param);
        }

        public List<CAC06Report> GetReportCAC07(CAC06Param param)
        {
            return GetService((param.BranchId != Session.Branch.Id) ? true : false).GetReportCAC07(param);
        }

        public List<CAC09Report> GetReportCAC09(CAC09Param param)
        {
            return GetService((param.BranchId != Session.Branch.Id) ? true : false).GetReportCAC09(param);
        }

        public List<CAC11Report> GetReportCAC11(CAC11Param param)
        {
            return GetService((param.BranchId != Session.Branch.Id) ? true : false).GetReportCAC11(param);
        }

        public List<CAC12Report> GetReportCAC12(CAC09Param param)
        {
            return GetService((param.BranchId != Session.Branch.Id) ? true : false).GetReportCAC12(param);
        }

        public List<CAC13Report> GetReportCAC13(CAC11Param param)
        {
            return GetService((param.BranchId != Session.Branch.Id) ? true : false).GetReportCAC13(param);
        }

        public List<CAC14Report> GetReportCAC14(CAC14Param param)
        {            
            IReportService bs = GetService();
            return bs.GetReportCAC14(param);
        }

        //TODO: INSTALLMENT CASE
        //public List<CAC16Report> GetReportCAC16(CAC16Param param)
        //{
        //    return GetService((param.BranchId != Session.Branch.Id) ? true : false).GetReportCAC16(param);
        //}

        public List<CAC18Report> GetReportCAC18(CAC18Param param)
        {
            IReportService bs = GetService();
            return bs.GetReportCAC18(param);
        }

        public List<CAC19Report> GetReportCAC19(CAC19Param param)
        {
            IReportService bs = GetService();
            return bs.GetReportCAC19(param);
        }

        
    }
}
