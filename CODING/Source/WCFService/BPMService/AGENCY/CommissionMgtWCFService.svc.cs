using System.Collections.Generic;
using PEA.BPM.AgencyManagementModule.BS;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.ArchitectureBS;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.WebService;
using WCFExtras.Soap;


namespace BPMService.AGENCY
{

    public class CommissionMgtWCFService : ICommissionMgtWCFService
    {
        private CommissionMgtService _commissionMgtService;
        private AuthInfo authInfo = SoapHeaderHelper<AuthInfo>.GetInputHeader("AuthInfoValue");

        public CommissionMgtWCFService()
        {
            _commissionMgtService = new CommissionMgtService();
        }

        public CommissionInfo CalculateCommissionAndFine(BookSearchInfo searchInfo, FeeBaseElement comRate, BooniesCommissionInfo hp)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CommissionMgtWCFService", "CalculateCommissionAndFine",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _commissionMgtService.CalculateCommissionAndFine(searchInfo, comRate, hp);
                });
        }

        public CompressData CalculateCommissionAndFine_Compress(CompressData searchInfo, CompressData comRate, CompressData hp)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CommissionMgtWCFService", "CalculateCommissionAndFine_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<CommissionInfo>(_commissionMgtService.CalculateCommissionAndFine(ServiceHelper.DecompressData<BookSearchInfo>(searchInfo), ServiceHelper.DecompressData<FeeBaseElement>(comRate), ServiceHelper.DecompressData<BooniesCommissionInfo>(hp)));
                });
        }

        public decimal? GetAndDisplayAdvPaymentAmount(CompressData searchInfo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CommissionMgtWCFService", "GetAndDisplayAdvPaymentAmount",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _commissionMgtService.GetAndDisplayAdvPaymentAmount(ServiceHelper.DecompressData<BookSearchInfo>(searchInfo));
                });
        }

        public AgentInfo GetAgentHelpInformation(string agentId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CommissionMgtWCFService", "GetAgentHelpInformation",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _commissionMgtService.GetAgentHelpInformation(agentId);
                });
        }

        public CompressData GetAgentHelpInformation_Compress(string agentId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CommissionMgtWCFService", "GetAgentHelpInformation_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<AgentInfo>(_commissionMgtService.GetAgentHelpInformation(agentId));
                });
        }

        public string SaveCommission(CompressData flavour)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CommissionMgtWCFService", "SaveCommission",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _commissionMgtService.SaveCommission(null, ServiceHelper.DecompressData<HelpOfferInfo>(flavour));
                });
        }

        public List<string> GetListOfCreatedDate(BookSearchInfo searchInfo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CommissionMgtWCFService", "GetListOfCreatedDate",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _commissionMgtService.GetListOfCreatedDate(searchInfo);
                });
        }

        public CompressData GetListOfCreatedDate_Compress(CompressData searchInfo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CommissionMgtWCFService", "GetListOfCreatedDate_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<string>>(_commissionMgtService.GetListOfCreatedDate(ServiceHelper.DecompressData<BookSearchInfo>(searchInfo)));
                });
        }

        public List<string> BookListByCreateDate(BookSearchInfo searchInfo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CommissionMgtWCFService", "BookListByCreateDate",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _commissionMgtService.BookListByCreateDate(searchInfo);
                });
        }

        public CompressData BookListByCreateDate_Compress(CompressData searchInfo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CommissionMgtWCFService", "BookListByCreateDate_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<string>>(_commissionMgtService.BookListByCreateDate(ServiceHelper.DecompressData<BookSearchInfo>(searchInfo)));
                });
        }

        public bool IsBookAvailable(CompressData searchInfo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CommissionMgtWCFService", "IsBookAvailable",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _commissionMgtService.IsBookAvailable(ServiceHelper.DecompressData<BookSearchInfo>(searchInfo));
                });
        }

        public bool IsBookAlreadyPaid(CompressData searchInfo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CommissionMgtWCFService", "IsBookAlreadyPaid",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _commissionMgtService.IsBookAlreadyPaid(ServiceHelper.DecompressData<BookSearchInfo>(searchInfo));
                });
        }

        public FeeBaseElement LoadCommissionRate(BookSearchInfo searchInfo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CommissionMgtWCFService", "LoadCommissionRate",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _commissionMgtService.LoadCommissionRate(searchInfo);
                });
        }

        public CompressData LoadCommissionRate_Compress(CompressData searchInfo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CommissionMgtWCFService", "LoadCommissionRate_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<FeeBaseElement>(_commissionMgtService.LoadCommissionRate(ServiceHelper.DecompressData<BookSearchInfo>(searchInfo)));
                });
        }

        public bool IsCommissionCalculated(CompressData searchInfo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CommissionMgtWCFService", "IsCommissionCalculated",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _commissionMgtService.IsCommissionCalculated(ServiceHelper.DecompressData<BookSearchInfo>(searchInfo));
                });
        }

        public List<CalculatedCommissionInfo> GetCalCountOfPeriodByAgentId(BookSearchInfo searchInfo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CommissionMgtWCFService", "GetCalCountOfPeriodByAgentId",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _commissionMgtService.GetCalCountOfPeriodByAgentId(searchInfo);
                });
        }

        public CompressData GetCalCountOfPeriodByAgentId_Compress(CompressData searchInfo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CommissionMgtWCFService", "GetCalCountOfPeriodByAgentId_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<CalculatedCommissionInfo>>(_commissionMgtService.GetCalCountOfPeriodByAgentId(ServiceHelper.DecompressData<BookSearchInfo>(searchInfo)));
                });
        }

        public int GetCommissionCountOfPeriod(CompressData searchInfo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CommissionMgtWCFService", "GetCommissionCountOfPeriod",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _commissionMgtService.GetCommissionCountOfPeriod(ServiceHelper.DecompressData<BookSearchInfo>(searchInfo));
                });
        }

        public TravelHelpRate GetTravelHelpRate(TravelHelpRateConditionInfo spcCondition)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CommissionMgtWCFService", "GetTravelHelpRate",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _commissionMgtService.GetTravelHelpRate(spcCondition);
                });
        }

        public CompressData GetTravelHelpRate_Compress(CompressData spcCondition)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CommissionMgtWCFService", "GetTravelHelpRate_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<TravelHelpRate>(_commissionMgtService.GetTravelHelpRate(ServiceHelper.DecompressData<TravelHelpRateConditionInfo>(spcCondition)));
                });
        }

        public FeeBaseElement GetFeeBase(string branchId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CommissionMgtWCFService", "GetFeeBase",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _commissionMgtService.GetFeeBase(branchId);
                });
        }

        public CompressData GetFeeBase_Compress(string branchId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CommissionMgtWCFService", "GetFeeBase_Compress",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<FeeBaseElement>(_commissionMgtService.GetFeeBase(branchId));
                });
        }

        public bool IsBillBookCalculated(string billbookId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CommissionMgtWCFService", "IsBillBookCalculated",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return _commissionMgtService.IsBillBookCalculated(billbookId);
                });
        }

        public CompressData GetCommissionHistory(BookSearchInfo searchInfo)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Agency, authInfo, "CommissionMgtWCFService", "GetCommissionHistory",
                () =>
                {
                    WebServiceSecurity.CheckAuthorization(this.authInfo);
                    return ServiceHelper.CompressData<List<CommissionHistoryInfo>>(_commissionMgtService.GetCommissionHistory(searchInfo));
                });
        }

    }
}
