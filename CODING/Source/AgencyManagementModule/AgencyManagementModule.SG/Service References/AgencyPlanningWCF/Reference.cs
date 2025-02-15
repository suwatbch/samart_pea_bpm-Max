﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.5466
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PEA.BPM.AgencyManagementModule.SG.AgencyPlanningWCF {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://tempuri.org", ConfigurationName="AgencyPlanningWCF.IAgencyPlanningWCFService")]
    [WCFExtras.Soap.SoapHeadersAttribute()]
    public interface IAgencyPlanningWCFService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAgencyPlanningWCFService/SaveAssignedLineofAgent", ReplyAction="http://tempuri.org/IAgencyPlanningWCFService/SaveAssignedLineofAgentResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities.BPMApplicationExceptionInfo), Action="http://tempuri.org/IAgencyPlanningWCFService/SaveAssignedLineofAgentBPMApplicatio" +
            "nExceptionInfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        bool SaveAssignedLineofAgent(PEA.BPM.Architecture.CommonUtilities.CompressData asiLineList);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAgencyPlanningWCFService/FindAndDisplayLineOfAgentSearchInfo", ReplyAction="http://tempuri.org/IAgencyPlanningWCFService/FindAndDisplayLineOfAgentSearchInfoR" +
            "esponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities.BPMApplicationExceptionInfo), Action="http://tempuri.org/IAgencyPlanningWCFService/FindAndDisplayLineOfAgentSearchInfoB" +
            "PMApplicationExceptionInfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        PEA.BPM.AgencyManagementModule.Interface.BusinessEntities.LineInfo[] FindAndDisplayLineOfAgentSearchInfo(string agentId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAgencyPlanningWCFService/FindAndDisplayLineOfAgentSearchInfo_" +
            "Compress", ReplyAction="http://tempuri.org/IAgencyPlanningWCFService/FindAndDisplayLineOfAgentSearchInfo_" +
            "CompressResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities.BPMApplicationExceptionInfo), Action="http://tempuri.org/IAgencyPlanningWCFService/FindAndDisplayLineOfAgentSearchInfo_" +
            "CompressBPMApplicationExceptionInfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        PEA.BPM.Architecture.CommonUtilities.CompressData FindAndDisplayLineOfAgentSearchInfo_Compress(string agentId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAgencyPlanningWCFService/AcquireAgentAssetSearchInformation", ReplyAction="http://tempuri.org/IAgencyPlanningWCFService/AcquireAgentAssetSearchInformationRe" +
            "sponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities.BPMApplicationExceptionInfo), Action="http://tempuri.org/IAgencyPlanningWCFService/AcquireAgentAssetSearchInformationBP" +
            "MApplicationExceptionInfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        PEA.BPM.AgencyManagementModule.Interface.BusinessEntities.AgentInfo[] AcquireAgentAssetSearchInformation(PEA.BPM.AgencyManagementModule.Interface.BusinessEntities.AgentSearchInfo searchInfo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAgencyPlanningWCFService/AcquireAgentAssetSearchInformation_C" +
            "ompress", ReplyAction="http://tempuri.org/IAgencyPlanningWCFService/AcquireAgentAssetSearchInformation_C" +
            "ompressResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities.BPMApplicationExceptionInfo), Action="http://tempuri.org/IAgencyPlanningWCFService/AcquireAgentAssetSearchInformation_C" +
            "ompressBPMApplicationExceptionInfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        PEA.BPM.Architecture.CommonUtilities.CompressData AcquireAgentAssetSearchInformation_Compress(PEA.BPM.Architecture.CommonUtilities.CompressData searchInfo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAgencyPlanningWCFService/FindAndDisplayBranchByKeyword", ReplyAction="http://tempuri.org/IAgencyPlanningWCFService/FindAndDisplayBranchByKeywordRespons" +
            "e")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities.BPMApplicationExceptionInfo), Action="http://tempuri.org/IAgencyPlanningWCFService/FindAndDisplayBranchByKeywordBPMAppl" +
            "icationExceptionInfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        PEA.BPM.AgencyManagementModule.Interface.BusinessEntities.PeaInfo[] FindAndDisplayBranchByKeyword(string keyword, string branchId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAgencyPlanningWCFService/FindAndDisplayBranchByKeyword_Compre" +
            "ss", ReplyAction="http://tempuri.org/IAgencyPlanningWCFService/FindAndDisplayBranchByKeyword_Compre" +
            "ssResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities.BPMApplicationExceptionInfo), Action="http://tempuri.org/IAgencyPlanningWCFService/FindAndDisplayBranchByKeyword_Compre" +
            "ssBPMApplicationExceptionInfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        PEA.BPM.Architecture.CommonUtilities.CompressData FindAndDisplayBranchByKeyword_Compress(string keyword, string branchId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAgencyPlanningWCFService/FindAndDisplayLineByKeyword", ReplyAction="http://tempuri.org/IAgencyPlanningWCFService/FindAndDisplayLineByKeywordResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities.BPMApplicationExceptionInfo), Action="http://tempuri.org/IAgencyPlanningWCFService/FindAndDisplayLineByKeywordBPMApplic" +
            "ationExceptionInfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        PEA.BPM.AgencyManagementModule.Interface.BusinessEntities.LineInfo[] FindAndDisplayLineByKeyword(PEA.BPM.AgencyManagementModule.Interface.BusinessEntities.LineSearchBoxInfo searchInfo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAgencyPlanningWCFService/FindAndDisplayLineByKeyword_Compress" +
            "", ReplyAction="http://tempuri.org/IAgencyPlanningWCFService/FindAndDisplayLineByKeyword_Compress" +
            "Response")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities.BPMApplicationExceptionInfo), Action="http://tempuri.org/IAgencyPlanningWCFService/FindAndDisplayLineByKeyword_Compress" +
            "BPMApplicationExceptionInfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        PEA.BPM.Architecture.CommonUtilities.CompressData FindAndDisplayLineByKeyword_Compress(PEA.BPM.Architecture.CommonUtilities.CompressData searchInfo);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface IAgencyPlanningWCFServiceChannel : PEA.BPM.AgencyManagementModule.SG.AgencyPlanningWCF.IAgencyPlanningWCFService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class AgencyPlanningWCFServiceClient : System.ServiceModel.ClientBase<PEA.BPM.AgencyManagementModule.SG.AgencyPlanningWCF.IAgencyPlanningWCFService>, PEA.BPM.AgencyManagementModule.SG.AgencyPlanningWCF.IAgencyPlanningWCFService {
        
        public AgencyPlanningWCFServiceClient() {
        }
        
        public AgencyPlanningWCFServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AgencyPlanningWCFServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AgencyPlanningWCFServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AgencyPlanningWCFServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool SaveAssignedLineofAgent(PEA.BPM.Architecture.CommonUtilities.CompressData asiLineList) {
            return base.Channel.SaveAssignedLineofAgent(asiLineList);
        }
        
        public PEA.BPM.AgencyManagementModule.Interface.BusinessEntities.LineInfo[] FindAndDisplayLineOfAgentSearchInfo(string agentId) {
            return base.Channel.FindAndDisplayLineOfAgentSearchInfo(agentId);
        }
        
        public PEA.BPM.Architecture.CommonUtilities.CompressData FindAndDisplayLineOfAgentSearchInfo_Compress(string agentId) {
            return base.Channel.FindAndDisplayLineOfAgentSearchInfo_Compress(agentId);
        }
        
        public PEA.BPM.AgencyManagementModule.Interface.BusinessEntities.AgentInfo[] AcquireAgentAssetSearchInformation(PEA.BPM.AgencyManagementModule.Interface.BusinessEntities.AgentSearchInfo searchInfo) {
            return base.Channel.AcquireAgentAssetSearchInformation(searchInfo);
        }
        
        public PEA.BPM.Architecture.CommonUtilities.CompressData AcquireAgentAssetSearchInformation_Compress(PEA.BPM.Architecture.CommonUtilities.CompressData searchInfo) {
            return base.Channel.AcquireAgentAssetSearchInformation_Compress(searchInfo);
        }
        
        public PEA.BPM.AgencyManagementModule.Interface.BusinessEntities.PeaInfo[] FindAndDisplayBranchByKeyword(string keyword, string branchId) {
            return base.Channel.FindAndDisplayBranchByKeyword(keyword, branchId);
        }
        
        public PEA.BPM.Architecture.CommonUtilities.CompressData FindAndDisplayBranchByKeyword_Compress(string keyword, string branchId) {
            return base.Channel.FindAndDisplayBranchByKeyword_Compress(keyword, branchId);
        }
        
        public PEA.BPM.AgencyManagementModule.Interface.BusinessEntities.LineInfo[] FindAndDisplayLineByKeyword(PEA.BPM.AgencyManagementModule.Interface.BusinessEntities.LineSearchBoxInfo searchInfo) {
            return base.Channel.FindAndDisplayLineByKeyword(searchInfo);
        }
        
        public PEA.BPM.Architecture.CommonUtilities.CompressData FindAndDisplayLineByKeyword_Compress(PEA.BPM.Architecture.CommonUtilities.CompressData searchInfo) {
            return base.Channel.FindAndDisplayLineByKeyword_Compress(searchInfo);
        }
    }
}
