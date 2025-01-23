using System.Collections.Generic;
using PEA.BPM.Architecture.ArchitectureBS;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.PaymentCollectionModule.BS;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities.Reports;
using PEA.BPM.WebService;
using WCFExtras.Soap;


namespace BPMService.POS
{

    public class ReportWCFService : IReportWCFService
    {
        private ReportBS _bs;
        private AuthInfo authInfo = SoapHeaderHelper<AuthInfo>.GetInputHeader("AuthInfoValue");

        public ReportWCFService()
        {
            _bs = new ReportBS();
        }

        public CompressData GetReportCAC01_Compress(CAC01Param param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "ReportWCFService", "GetReportCAC01_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<CAC01Report>>(_bs.GetReportCAC01(param));
                });
        }

        public CompressData GetReportCAC03_Compress(CAC01Param param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "ReportWCFService", "GetReportCAC03_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<CAC03Report>>(_bs.GetReportCAC03(param));
                });
        }

        public CompressData GetReportCAC04_Compress(CAC01Param param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "ReportWCFService", "GetReportCAC04_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<CAC04Report>>(_bs.GetReportCAC04(param));
                });
        }

        public CompressData GetReportCAC05_Compress(CAC06Param param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "ReportWCFService", "GetReportCAC05_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<CAC05Report>>(_bs.GetReportCAC05(param));
                });
        }

        public CompressData GetReportCAC06_Compress(CAC06Param param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "ReportWCFService", "GetReportCAC06_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<CAC06Report>>(_bs.GetReportCAC06(param));
                });
        }

        public CompressData GetReportCAC07_Compress(CAC06Param param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "ReportWCFService", "GetReportCAC07_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<CAC06Report>>(_bs.GetReportCAC07(param));
                });
        }

        public CompressData GetReportCAC09_Compress(CAC09Param param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "ReportWCFService", "GetReportCAC09_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<CAC09Report>>(_bs.GetReportCAC09(param));
                });
        }

        public CompressData GetReportCAC11_Compress(CAC11Param param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "ReportWCFService", "GetReportCAC11_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<CAC11Report>>(_bs.GetReportCAC11(param));
                });
        }

        public CompressData GetReportCAC12_Compress(CAC09Param param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "ReportWCFService", "GetReportCAC12_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<CAC12Report>>(_bs.GetReportCAC12(param));
                });
        }

        public CompressData GetReportCAC13_Compress(CAC11Param param)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "ReportWCFService", "GetReportCAC13_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<CAC13Report>>(_bs.GetReportCAC13(param));
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

        //TODO: INSTALLMENT CASE
        //public CompressData GetReportCAC16_Compress(CAC16Param param)
        //{
        //    return ServiceInspector.Instance.AuditTrail(EErrorInModule.POS, authInfo, "ReportWCFService", "GetReportCAC16_Compress",
        //        () =>
        //        {
        //            WebServiceSecurity.CheckAuthorization(this.authInfo);
        //            return ServiceHelper.CompressData<List<CAC16Report>>(_bs.GetReportCAC16(param));
        //        });
        //}

    }
}
