using System.Collections.Generic;
using PEA.BPM.Architecture.ArchitectureBS;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Reports;
//using PEA.BPM.WebService;
using WCFExtras.Soap;
using BPMLINQReport;
using System.Configuration;
using System;
//using PEA.BPM.PaymentCollectionModule.BS;

namespace BPMReportService.POS
{
    public class ReportWCFService : IReportWCFService
    {
        private POSReport _bs;
        private AuthInfo authInfo = SoapHeaderHelper<AuthInfo>.GetInputHeader("AuthInfoValue");

        public ReportWCFService()
        {
            _bs = new POSReport();
        }

        public CompressData GetReportCAC01_Compress(CAC01Param param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "ReportWCFService", "GetReportCAC01_Compress",
                () =>
                {
                    List<CAC01Report> report = null;
                    WebServiceSecurity.CheckAuthorization(this.authInfo);

                    if (param.Report == ReportName.CAC01_1)
                        report = _bs.GetReportCAC01_1(param.BranchId, param.ControllerId, param.TransFromDate);
                    else if (param.Report == ReportName.CAC01_2)
                        report = _bs.GetReportCAC01_2(param.BranchId, param.ControllerId, param.TransFromDate);

                    return ServiceHelper.CompressData<List<CAC01Report>>(report);
                });
        }

        public CompressData GetReportCAC03_Compress(CAC01Param param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "ReportWCFService", "GetReportCAC03_Compress",
                () =>
                {
                    List<CAC03Report> report = null;
                    WebServiceSecurity.CheckAuthorization(this.authInfo);

                    if (param.Report == ReportName.CAC03_1 || param.Report == ReportName.CAC03_2)
                        report = _bs.GetReportCAC03_1(param.BranchId, param.ControllerId, param.TransFromDate, param.TransToDate, param.FromTime, param.ToTime, param.BankKey);
                    else if (param.Report == ReportName.CAC03_3 || param.Report == ReportName.CAC03_4)
                        report = _bs.GetReportCAC03_2(param.BranchId, param.ControllerId, param.TransFromDate, param.TransToDate, param.FromTime, param.ToTime, param.BankKey);

                    return ServiceHelper.CompressData<List<CAC03Report>>(report);
                });
        }

        public CompressData GetReportCAC04_Compress(CAC01Param param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "ReportWCFService", "GetReportCAC04_Compress",
                () =>
                {
                    List<CAC04Report> report = null;
                    WebServiceSecurity.CheckAuthorization(this.authInfo);

                    if (param.Report == ReportName.CAC04_1 || param.Report == ReportName.CAC04_2)
                        report = _bs.GetReportCAC04_1(param.BranchId, param.ControllerId, param.TransFromDate, param.TransToDate, param.FromTime, param.ToTime, param.BankKey);
                    else if (param.Report == ReportName.CAC04_3 || param.Report == ReportName.CAC04_4)
                        report = _bs.GetReportCAC04_2(param.BranchId, param.ControllerId, param.TransFromDate, param.TransToDate, param.FromTime, param.ToTime, param.BankKey);

                    return ServiceHelper.CompressData<List<CAC04Report>>(report);
                });
        }

        public CompressData GetReportCAC05_Compress(CAC06Param param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "ReportWCFService", "GetReportCAC05_Compress",
                () =>
                {
                    List<CAC05Report> report = null;
                    WebServiceSecurity.CheckAuthorization(this.authInfo);

                    if (param.Report == ReportName.CAC05_1)
                        report = _bs.GetReportCAC05_1(param.BranchId, param.ControllerId, param.TransFromDate);
                    else if (param.Report == ReportName.CAC05_2)
                        report = _bs.GetReportCAC05_2(param.BranchId, param.ControllerId, param.TransFromDate);

                    return ServiceHelper.CompressData<List<CAC05Report>>(report);
                });
        }

        public CompressData GetReportCAC06_Compress(CAC06Param param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "ReportWCFService", "GetReportCAC06_Compress",
                () =>
                {
                    List<CAC06Report> report = null;
                    WebServiceSecurity.CheckAuthorization(this.authInfo);

                    if (param.Report == ReportName.CAC06_1)
                        report = _bs.GetReportCAC06_1(param.BranchId, param.ControllerId, param.TransFromDate);
                    else if (param.Report == ReportName.CAC06_2 || param.Report == ReportName.CAC06_3)
                        report = _bs.GetReportCAC06_2(param.BranchId, param.ControllerId, param.TransFromDate);

                    return ServiceHelper.CompressData<List<CAC06Report>>(report);
                });
        }

        public CompressData GetReportCAC07_Compress(CAC06Param param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "ReportWCFService", "GetReportCAC07_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<CAC06Report>>(_bs.GetReportCAC07(param.BranchId, param.ControllerId, param.TransFromDate));
                });
        }

        public CompressData GetReportCAC09_Compress(CAC09Param param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "ReportWCFService", "GetReportCAC09_Compress",
                () =>
                {
                    List<CAC09Report> report = null;
                    WebServiceSecurity.CheckAuthorization(this.authInfo);

                    if (param.Report == ReportName.CAC09_1)
                        report = _bs.GetReportCAC09_1(param.BranchId, param.PosId, param.TransFromDate);
                    else if (param.Report == ReportName.CAC09_2)
                        report = _bs.GetReportCAC09_2(param.BranchId, param.PosId, param.TransFromDate);

                    return ServiceHelper.CompressData<List<CAC09Report>>(report);
                });
        }

        public CompressData GetReportCAC11_Compress(CAC11Param param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "ReportWCFService", "GetReportCAC11_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<CAC11Report>>(_bs.GetReportCAC11(param.BranchId, param.PosId, param.ControllerId, param.TransFromDate, param.TransToDate));
                });
        }

        public CompressData GetReportCAC12_Compress(CAC09Param param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "ReportWCFService", "GetReportCAC12_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<CAC12Report>>(_bs.GetReportCAC12(param.BranchId, param.TransFromDate));
                });
        }

        public CompressData GetReportCAC13_Compress(CAC11Param param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "ReportWCFService", "GetReportCAC13_Compress",
                () =>
                {
                    //no scope protection since UI parameter forces
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<CAC13Report>>(_bs.GetReportCAC13(param.BranchId, param.PosId, param.ControllerId, param.TransFromDate, param.TransToDate));
                });
        }

        public CompressData GetReportCAC14_Compress(CAC14Param param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "ReportWCFService", "GetReportCAC14_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<CAC14Report>>(_bs.GetReportCAC14(param));
                });
        }

        public CompressData GetReportCAC18_Compress(CAC18Param param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "ReportWCFService", "GetReportCAC18_Compress",
                 () =>
                 {
                     List<CAC18Report> report = null;
                     WebServiceSecurity.CheckAuthorization(this.authInfo);

                     report = _bs.GetReportCAC18(param.BranchId, param.ControllerId, param.TransFromDate,param.TransToDate);

                     return ServiceHelper.CompressData<List<CAC18Report>>(report);
                 });
        }

        public CompressData GetReportCAC19_Compress(CAC19Param param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "ReportWCFService", "GetReportCAC19_Compress",
                 () =>
                 {
                     List<CAC19Report> report = null;
                     WebServiceSecurity.CheckAuthorization(this.authInfo);

                     report = _bs.GetReportCAC19(param.BranchId, param.ControllerId, param.TransFromDate,param.TransToDate);

                     return ServiceHelper.CompressData<List<CAC19Report>>(report);
                 });
        }
    }
}
