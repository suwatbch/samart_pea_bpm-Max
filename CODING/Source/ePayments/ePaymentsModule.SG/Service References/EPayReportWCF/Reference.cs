﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.5466
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PEA.BPM.ePaymentsModule.SG.EPayReportWCF {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://tempuri.org", ConfigurationName="EPayReportWCF.IEPayReportWCFService")]
    [WCFExtras.Soap.SoapHeadersAttribute()]
    public interface IEPayReportWCFService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEPayReportWCFService/GetRe01ReportService", ReplyAction="http://tempuri.org/IEPayReportWCFService/GetRe01ReportServiceResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities.BPMApplicationExceptionInfo), Action="http://tempuri.org/IEPayReportWCFService/GetRe01ReportServiceBPMApplicationExcept" +
            "ionInfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        PEA.BPM.Architecture.CommonUtilities.CompressData GetRe01ReportService(PEA.BPM.ePaymentsModule.Interface.BusinessEntities.Reports.RE01Param param);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEPayReportWCFService/GetRe02ReportService", ReplyAction="http://tempuri.org/IEPayReportWCFService/GetRe02ReportServiceResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities.BPMApplicationExceptionInfo), Action="http://tempuri.org/IEPayReportWCFService/GetRe02ReportServiceBPMApplicationExcept" +
            "ionInfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        PEA.BPM.Architecture.CommonUtilities.CompressData GetRe02ReportService(PEA.BPM.ePaymentsModule.Interface.BusinessEntities.Reports.RE02ParamInfo param);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEPayReportWCFService/GetRe03ReportService", ReplyAction="http://tempuri.org/IEPayReportWCFService/GetRe03ReportServiceResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities.BPMApplicationExceptionInfo), Action="http://tempuri.org/IEPayReportWCFService/GetRe03ReportServiceBPMApplicationExcept" +
            "ionInfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        PEA.BPM.Architecture.CommonUtilities.CompressData GetRe03ReportService(PEA.BPM.ePaymentsModule.Interface.BusinessEntities.Reports.RE03ParamInfo param);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEPayReportWCFService/GetRe04ReportService", ReplyAction="http://tempuri.org/IEPayReportWCFService/GetRe04ReportServiceResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities.BPMApplicationExceptionInfo), Action="http://tempuri.org/IEPayReportWCFService/GetRe04ReportServiceBPMApplicationExcept" +
            "ionInfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        PEA.BPM.Architecture.CommonUtilities.CompressData GetRe04ReportService(PEA.BPM.ePaymentsModule.Interface.BusinessEntities.Reports.RE04ParamInfo param);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEPayReportWCFService/GetRe05ReportService", ReplyAction="http://tempuri.org/IEPayReportWCFService/GetRe05ReportServiceResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities.BPMApplicationExceptionInfo), Action="http://tempuri.org/IEPayReportWCFService/GetRe05ReportServiceBPMApplicationExcept" +
            "ionInfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        PEA.BPM.Architecture.CommonUtilities.CompressData GetRe05ReportService(PEA.BPM.ePaymentsModule.Interface.BusinessEntities.Reports.RE05ParamInfo param);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEPayReportWCFService/GetRe06ReportInfo", ReplyAction="http://tempuri.org/IEPayReportWCFService/GetRe06ReportInfoResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities.BPMApplicationExceptionInfo), Action="http://tempuri.org/IEPayReportWCFService/GetRe06ReportInfoBPMApplicationException" +
            "InfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        PEA.BPM.Architecture.CommonUtilities.CompressData GetRe06ReportInfo(PEA.BPM.ePaymentsModule.Interface.BusinessEntities.Reports.RE06ParamInfo param);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEPayReportWCFService/GetRe07ReportInfo", ReplyAction="http://tempuri.org/IEPayReportWCFService/GetRe07ReportInfoResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities.BPMApplicationExceptionInfo), Action="http://tempuri.org/IEPayReportWCFService/GetRe07ReportInfoBPMApplicationException" +
            "InfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        PEA.BPM.Architecture.CommonUtilities.CompressData GetRe07ReportInfo(PEA.BPM.ePaymentsModule.Interface.BusinessEntities.Reports.RE07ParamInfo param);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEPayReportWCFService/GetRe08ReportInfo", ReplyAction="http://tempuri.org/IEPayReportWCFService/GetRe08ReportInfoResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities.BPMApplicationExceptionInfo), Action="http://tempuri.org/IEPayReportWCFService/GetRe08ReportInfoBPMApplicationException" +
            "InfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        PEA.BPM.Architecture.CommonUtilities.CompressData GetRe08ReportInfo(PEA.BPM.ePaymentsModule.Interface.BusinessEntities.Reports.RE08ParamInfo param);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEPayReportWCFService/GetRe09ReportInfo", ReplyAction="http://tempuri.org/IEPayReportWCFService/GetRe09ReportInfoResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities.BPMApplicationExceptionInfo), Action="http://tempuri.org/IEPayReportWCFService/GetRe09ReportInfoBPMApplicationException" +
            "InfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        PEA.BPM.Architecture.CommonUtilities.CompressData GetRe09ReportInfo(PEA.BPM.ePaymentsModule.Interface.BusinessEntities.Reports.RE09ParamInfo param);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface IEPayReportWCFServiceChannel : PEA.BPM.ePaymentsModule.SG.EPayReportWCF.IEPayReportWCFService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class EPayReportWCFServiceClient : System.ServiceModel.ClientBase<PEA.BPM.ePaymentsModule.SG.EPayReportWCF.IEPayReportWCFService>, PEA.BPM.ePaymentsModule.SG.EPayReportWCF.IEPayReportWCFService {
        
        public EPayReportWCFServiceClient() {
        }
        
        public EPayReportWCFServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public EPayReportWCFServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public EPayReportWCFServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public EPayReportWCFServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public PEA.BPM.Architecture.CommonUtilities.CompressData GetRe01ReportService(PEA.BPM.ePaymentsModule.Interface.BusinessEntities.Reports.RE01Param param) {
            return base.Channel.GetRe01ReportService(param);
        }
        
        public PEA.BPM.Architecture.CommonUtilities.CompressData GetRe02ReportService(PEA.BPM.ePaymentsModule.Interface.BusinessEntities.Reports.RE02ParamInfo param) {
            return base.Channel.GetRe02ReportService(param);
        }
        
        public PEA.BPM.Architecture.CommonUtilities.CompressData GetRe03ReportService(PEA.BPM.ePaymentsModule.Interface.BusinessEntities.Reports.RE03ParamInfo param) {
            return base.Channel.GetRe03ReportService(param);
        }
        
        public PEA.BPM.Architecture.CommonUtilities.CompressData GetRe04ReportService(PEA.BPM.ePaymentsModule.Interface.BusinessEntities.Reports.RE04ParamInfo param) {
            return base.Channel.GetRe04ReportService(param);
        }
        
        public PEA.BPM.Architecture.CommonUtilities.CompressData GetRe05ReportService(PEA.BPM.ePaymentsModule.Interface.BusinessEntities.Reports.RE05ParamInfo param) {
            return base.Channel.GetRe05ReportService(param);
        }
        
        public PEA.BPM.Architecture.CommonUtilities.CompressData GetRe06ReportInfo(PEA.BPM.ePaymentsModule.Interface.BusinessEntities.Reports.RE06ParamInfo param) {
            return base.Channel.GetRe06ReportInfo(param);
        }
        
        public PEA.BPM.Architecture.CommonUtilities.CompressData GetRe07ReportInfo(PEA.BPM.ePaymentsModule.Interface.BusinessEntities.Reports.RE07ParamInfo param) {
            return base.Channel.GetRe07ReportInfo(param);
        }
        
        public PEA.BPM.Architecture.CommonUtilities.CompressData GetRe08ReportInfo(PEA.BPM.ePaymentsModule.Interface.BusinessEntities.Reports.RE08ParamInfo param) {
            return base.Channel.GetRe08ReportInfo(param);
        }
        
        public PEA.BPM.Architecture.CommonUtilities.CompressData GetRe09ReportInfo(PEA.BPM.ePaymentsModule.Interface.BusinessEntities.Reports.RE09ParamInfo param) {
            return base.Channel.GetRe09ReportInfo(param);
        }
    }
}
