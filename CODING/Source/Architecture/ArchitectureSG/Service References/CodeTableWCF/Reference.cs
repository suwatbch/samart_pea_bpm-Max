﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.5466
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PEA.BPM.Architecture.ArchitectureSG.CodeTableWCF {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://tempuri.org", ConfigurationName="CodeTableWCF.ICodeTableWCFService")]
    public interface ICodeTableWCFService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICodeTableWCFService/GetUpdatedData", ReplyAction="http://tempuri.org/ICodeTableWCFService/GetUpdatedDataResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities.BPMApplicationExceptionInfo), Action="http://tempuri.org/ICodeTableWCFService/GetUpdatedDataBPMApplicationExceptionInfo" +
            "Fault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        PEA.BPM.Architecture.CommonUtilities.CompressData GetUpdatedData(System.DateTime lastModifiedDt, string branchId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface ICodeTableWCFServiceChannel : PEA.BPM.Architecture.ArchitectureSG.CodeTableWCF.ICodeTableWCFService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class CodeTableWCFServiceClient : System.ServiceModel.ClientBase<PEA.BPM.Architecture.ArchitectureSG.CodeTableWCF.ICodeTableWCFService>, PEA.BPM.Architecture.ArchitectureSG.CodeTableWCF.ICodeTableWCFService {
        
        public CodeTableWCFServiceClient() {
        }
        
        public CodeTableWCFServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CodeTableWCFServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CodeTableWCFServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CodeTableWCFServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public PEA.BPM.Architecture.CommonUtilities.CompressData GetUpdatedData(System.DateTime lastModifiedDt, string branchId) {
            return base.Channel.GetUpdatedData(lastModifiedDt, branchId);
        }
    }
}
