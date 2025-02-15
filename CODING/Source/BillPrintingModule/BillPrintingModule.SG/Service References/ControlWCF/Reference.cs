﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.5466
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PEA.BPM.BillPrintingModule.SG.ControlWCF {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://tempuri.org", ConfigurationName="ControlWCF.IControlWCFService")]
    [WCFExtras.Soap.SoapHeadersAttribute()]
    public interface IControlWCFService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IControlWCFService/GetDeliveryPlace", ReplyAction="http://tempuri.org/IControlWCFService/GetDeliveryPlaceResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities.BPMApplicationExceptionInfo), Action="http://tempuri.org/IControlWCFService/GetDeliveryPlaceBPMApplicationExceptionInfo" +
            "Fault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        PEA.BPM.BillPrintingModule.Interface.BusinessEntities.DeliveryPlace[] GetDeliveryPlace(string createBranchId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IControlWCFService/InsertDeliveryPlace", ReplyAction="http://tempuri.org/IControlWCFService/InsertDeliveryPlaceResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities.BPMApplicationExceptionInfo), Action="http://tempuri.org/IControlWCFService/InsertDeliveryPlaceBPMApplicationExceptionI" +
            "nfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        string InsertDeliveryPlace(PEA.BPM.BillPrintingModule.Interface.BusinessEntities.DeliveryPlace dp);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IControlWCFService/UpdateDeliveryPlace", ReplyAction="http://tempuri.org/IControlWCFService/UpdateDeliveryPlaceResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities.BPMApplicationExceptionInfo), Action="http://tempuri.org/IControlWCFService/UpdateDeliveryPlaceBPMApplicationExceptionI" +
            "nfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        string UpdateDeliveryPlace(PEA.BPM.BillPrintingModule.Interface.BusinessEntities.DeliveryPlace dp);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IControlWCFService/DeleteDeliveryPlace", ReplyAction="http://tempuri.org/IControlWCFService/DeleteDeliveryPlaceResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities.BPMApplicationExceptionInfo), Action="http://tempuri.org/IControlWCFService/DeleteDeliveryPlaceBPMApplicationExceptionI" +
            "nfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        string DeleteDeliveryPlace(PEA.BPM.BillPrintingModule.Interface.BusinessEntities.DeliveryPlace dp);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IControlWCFService/GetChildBranch", ReplyAction="http://tempuri.org/IControlWCFService/GetChildBranchResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities.BPMApplicationExceptionInfo), Action="http://tempuri.org/IControlWCFService/GetChildBranchBPMApplicationExceptionInfoFa" +
            "ult", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        string[] GetChildBranch(string branchId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IControlWCFService/GetBank", ReplyAction="http://tempuri.org/IControlWCFService/GetBankResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities.BPMApplicationExceptionInfo), Action="http://tempuri.org/IControlWCFService/GetBankBPMApplicationExceptionInfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        PEA.BPM.Infrastructure.Interface.BusinessEntities.Bank[] GetBank(string branchId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IControlWCFService/GetPortion", ReplyAction="http://tempuri.org/IControlWCFService/GetPortionResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities.BPMApplicationExceptionInfo), Action="http://tempuri.org/IControlWCFService/GetPortionBPMApplicationExceptionInfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        PEA.BPM.BillPrintingModule.Interface.BusinessEntities.Portion[] GetPortion(string branchId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IControlWCFService/GetApprover", ReplyAction="http://tempuri.org/IControlWCFService/GetApproverResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities.BPMApplicationExceptionInfo), Action="http://tempuri.org/IControlWCFService/GetApproverBPMApplicationExceptionInfoFault" +
            "", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        PEA.BPM.BillPrintingModule.Interface.BusinessEntities.AuthorizedPerson[] GetApprover(string createBranchId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IControlWCFService/InsertApprover", ReplyAction="http://tempuri.org/IControlWCFService/InsertApproverResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities.BPMApplicationExceptionInfo), Action="http://tempuri.org/IControlWCFService/InsertApproverBPMApplicationExceptionInfoFa" +
            "ult", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        string InsertApprover(PEA.BPM.BillPrintingModule.Interface.BusinessEntities.AuthorizedPerson approver);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IControlWCFService/UpdateApprover", ReplyAction="http://tempuri.org/IControlWCFService/UpdateApproverResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities.BPMApplicationExceptionInfo), Action="http://tempuri.org/IControlWCFService/UpdateApproverBPMApplicationExceptionInfoFa" +
            "ult", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        string UpdateApprover(PEA.BPM.BillPrintingModule.Interface.BusinessEntities.AuthorizedPerson approver);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IControlWCFService/DeleteApprover", ReplyAction="http://tempuri.org/IControlWCFService/DeleteApproverResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities.BPMApplicationExceptionInfo), Action="http://tempuri.org/IControlWCFService/DeleteApproverBPMApplicationExceptionInfoFa" +
            "ult", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        string DeleteApprover(PEA.BPM.BillPrintingModule.Interface.BusinessEntities.AuthorizedPerson approver);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IControlWCFService/GetContractAccountHistory", ReplyAction="http://tempuri.org/IControlWCFService/GetContractAccountHistoryResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities.BPMApplicationExceptionInfo), Action="http://tempuri.org/IControlWCFService/GetContractAccountHistoryBPMApplicationExce" +
            "ptionInfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        PEA.BPM.BillPrintingModule.Interface.BusinessEntities.Invoice[] GetContractAccountHistory(string caId, string printBranch);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IControlWCFService/GetBarcodeMRU", ReplyAction="http://tempuri.org/IControlWCFService/GetBarcodeMRUResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities.BPMApplicationExceptionInfo), Action="http://tempuri.org/IControlWCFService/GetBarcodeMRUBPMApplicationExceptionInfoFau" +
            "lt", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        PEA.BPM.BillPrintingModule.Interface.BusinessEntities.BarcodeMRU[] GetBarcodeMRU(PEA.BPM.BillPrintingModule.Interface.BusinessEntities.ManageBarcodeParam param);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IControlWCFService/UpdateBarcodeMRU", ReplyAction="http://tempuri.org/IControlWCFService/UpdateBarcodeMRUResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities.BPMApplicationExceptionInfo), Action="http://tempuri.org/IControlWCFService/UpdateBarcodeMRUBPMApplicationExceptionInfo" +
            "Fault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        void UpdateBarcodeMRU(PEA.BPM.BillPrintingModule.Interface.BusinessEntities.BarcodeMRU param);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface IControlWCFServiceChannel : PEA.BPM.BillPrintingModule.SG.ControlWCF.IControlWCFService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class ControlWCFServiceClient : System.ServiceModel.ClientBase<PEA.BPM.BillPrintingModule.SG.ControlWCF.IControlWCFService>, PEA.BPM.BillPrintingModule.SG.ControlWCF.IControlWCFService {
        
        public ControlWCFServiceClient() {
        }
        
        public ControlWCFServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ControlWCFServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ControlWCFServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ControlWCFServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public PEA.BPM.BillPrintingModule.Interface.BusinessEntities.DeliveryPlace[] GetDeliveryPlace(string createBranchId) {
            return base.Channel.GetDeliveryPlace(createBranchId);
        }
        
        public string InsertDeliveryPlace(PEA.BPM.BillPrintingModule.Interface.BusinessEntities.DeliveryPlace dp) {
            return base.Channel.InsertDeliveryPlace(dp);
        }
        
        public string UpdateDeliveryPlace(PEA.BPM.BillPrintingModule.Interface.BusinessEntities.DeliveryPlace dp) {
            return base.Channel.UpdateDeliveryPlace(dp);
        }
        
        public string DeleteDeliveryPlace(PEA.BPM.BillPrintingModule.Interface.BusinessEntities.DeliveryPlace dp) {
            return base.Channel.DeleteDeliveryPlace(dp);
        }
        
        public string[] GetChildBranch(string branchId) {
            return base.Channel.GetChildBranch(branchId);
        }
        
        public PEA.BPM.Infrastructure.Interface.BusinessEntities.Bank[] GetBank(string branchId) {
            return base.Channel.GetBank(branchId);
        }
        
        public PEA.BPM.BillPrintingModule.Interface.BusinessEntities.Portion[] GetPortion(string branchId) {
            return base.Channel.GetPortion(branchId);
        }
        
        public PEA.BPM.BillPrintingModule.Interface.BusinessEntities.AuthorizedPerson[] GetApprover(string createBranchId) {
            return base.Channel.GetApprover(createBranchId);
        }
        
        public string InsertApprover(PEA.BPM.BillPrintingModule.Interface.BusinessEntities.AuthorizedPerson approver) {
            return base.Channel.InsertApprover(approver);
        }
        
        public string UpdateApprover(PEA.BPM.BillPrintingModule.Interface.BusinessEntities.AuthorizedPerson approver) {
            return base.Channel.UpdateApprover(approver);
        }
        
        public string DeleteApprover(PEA.BPM.BillPrintingModule.Interface.BusinessEntities.AuthorizedPerson approver) {
            return base.Channel.DeleteApprover(approver);
        }
        
        public PEA.BPM.BillPrintingModule.Interface.BusinessEntities.Invoice[] GetContractAccountHistory(string caId, string printBranch) {
            return base.Channel.GetContractAccountHistory(caId, printBranch);
        }
        
        public PEA.BPM.BillPrintingModule.Interface.BusinessEntities.BarcodeMRU[] GetBarcodeMRU(PEA.BPM.BillPrintingModule.Interface.BusinessEntities.ManageBarcodeParam param) {
            return base.Channel.GetBarcodeMRU(param);
        }
        
        public void UpdateBarcodeMRU(PEA.BPM.BillPrintingModule.Interface.BusinessEntities.BarcodeMRU param) {
            base.Channel.UpdateBarcodeMRU(param);
        }
    }
}
