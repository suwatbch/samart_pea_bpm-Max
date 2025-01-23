using System.Collections.Generic;
using PEA.BPM.Architecture.ArchitectureBS;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.ePaymentsModule.BS;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities.Reports;
using PEA.BPM.WebService;
using WCFExtras.Soap;


namespace BPMService.ePayment
{

    public class EPayReportWCFService : IEPayReportWCFService
    {
        private ReportBS _reportService;
        private AuthInfo authInfo = SoapHeaderHelper<AuthInfo>.GetInputHeader("AuthInfoValue");

        public EPayReportWCFService()
        {
            _reportService = new ReportBS();
        }

        public CompressData GetRe01ReportService(RE01Param param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.EPayment, authInfo, "ReportWCFService", "GetRe01ReportService",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<RE01_ReportInfo>>(_reportService.GetRe01ReportService(param));
                });
        }

        public CompressData GetRe02ReportService(RE02ParamInfo param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.EPayment, authInfo, "ReportWCFService", "GetRe02ReportService",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<RE02_ReportInfo>>(_reportService.GetRe02ReportService(param));
                });
        }

        public CompressData GetRe03ReportService(RE03ParamInfo param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.EPayment, authInfo, "ReportWCFService", "GetRe03ReportService",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<RE03_ReportInfo>>(_reportService.GetRe03ReportService(param));
                });
        }

        public CompressData GetRe04ReportService(RE04ParamInfo param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.EPayment, authInfo, "ReportWCFService", "GetRe04ReportService",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<RE04_ReportInfo>>(_reportService.GetRe04ReportService(param));
                });
        }

        public CompressData GetRe05ReportService(RE05ParamInfo param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.EPayment, authInfo, "ReportWCFService", "GetRe05ReportService",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<RE05_ReportInfo>>(_reportService.GetRe05ReportService(param));
                });
        }

        public CompressData GetRe06ReportInfo(RE06ParamInfo param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.EPayment, authInfo, "ReportWCFService", "GetRe06ReportInfo",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<RE06_ReportInfo>>(_reportService.GetRe06ReportInfo(param));
                });
        }

        public CompressData GetRe07ReportInfo(RE07ParamInfo param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.EPayment, authInfo, "ReportWCFService", "GetRe07ReportInfo",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<RE07_ReportInfo>>(_reportService.GetRe07ReportInfo(param));
                });
        }

        public CompressData GetRe08ReportInfo(RE08ParamInfo param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.EPayment, authInfo, "ReportWCFService", "GetRe08ReportInfo",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<RE08_ReportInfo>>(_reportService.GetRe08ReportInfo(param));
                });
        }

        public CompressData GetRe09ReportInfo(RE09ParamInfo param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.EPayment, authInfo, "ReportWCFService", "GetRe09ReportInfo",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<RE09_ReportInfo>>(_reportService.GetRe09ReportInfo(param));
                });
        }

    }
}
