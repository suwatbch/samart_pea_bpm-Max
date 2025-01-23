using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ComponentModel;
using PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.BillPrintingModule.Interface.BusinessEntities;
using WCFExtras.Soap;


namespace BPMService.BLAN
{
    [SoapHeaders, ServiceContract(Namespace = "http://tempuri.org")]
    public interface IBillPrintingReportWCFService
    {
        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData GetDailyPrintReport(ReportConditionParam param);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData GetDailyUnprintReport(ReportConditionParam param);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData GetBillDeliveryReport(ReportConditionParam param);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData GetStreetRouteReport(ReportConditionParam param);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData GetStreetRouteReceiveReport(ReportConditionParam param);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData GetStreetRouteUnreceiveReport(ReportConditionParam param);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData GetF16Report(ReportConditionParam param);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        List<PrintableId> GetBranchForBillDeliveryReport(ReportConditionParam param);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData GetGrpInvSummaryReport(ReportConditionParam param);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData GetBillingStatusReport(ReportConditionParam param);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData GetPrintGreenByBankReport(ReportConditionParam param);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData GetCAUnprintReport(ReportConditionParam param);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData GetDirectDebitStatusReport(ReportConditionParam param);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        List<ReportGroupingCrossCheck> GetGroupingCrossCheckReport(ReportConditionParam param);

    }
}
