﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.5466
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PEA.BPM.AgencyManagementModule.SG.BillBookCheckInWCF {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://tempuri.org", ConfigurationName="BillBookCheckInWCF.IBillbookCheckInWCFService")]
    [WCFExtras.Soap.SoapHeadersAttribute()]
    public interface IBillbookCheckInWCFService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBillbookCheckInWCFService/GetBillBookCheckInInfo", ReplyAction="http://tempuri.org/IBillbookCheckInWCFService/GetBillBookCheckInInfoResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities.BPMApplicationExceptionInfo), Action="http://tempuri.org/IBillbookCheckInWCFService/GetBillBookCheckInInfoBPMApplicatio" +
            "nExceptionInfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        PEA.BPM.AgencyManagementModule.Interface.BusinessEntities.BillBookCheckInInfo GetBillBookCheckInInfo(string billBookId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBillbookCheckInWCFService/GetBillBookCheckInInfo_Compress", ReplyAction="http://tempuri.org/IBillbookCheckInWCFService/GetBillBookCheckInInfo_CompressResp" +
            "onse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities.BPMApplicationExceptionInfo), Action="http://tempuri.org/IBillbookCheckInWCFService/GetBillBookCheckInInfo_CompressBPMA" +
            "pplicationExceptionInfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        PEA.BPM.Architecture.CommonUtilities.CompressData GetBillBookCheckInInfo_Compress(string billBookId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBillbookCheckInWCFService/GetGroupInvoiceCheckInInfo", ReplyAction="http://tempuri.org/IBillbookCheckInWCFService/GetGroupInvoiceCheckInInfoResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities.BPMApplicationExceptionInfo), Action="http://tempuri.org/IBillbookCheckInWCFService/GetGroupInvoiceCheckInInfoBPMApplic" +
            "ationExceptionInfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        PEA.BPM.AgencyManagementModule.Interface.BusinessEntities.BillBookCheckInInfo GetGroupInvoiceCheckInInfo(string groupIvId, string branchId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBillbookCheckInWCFService/GetGroupInvoiceCheckInInfo_Compress" +
            "", ReplyAction="http://tempuri.org/IBillbookCheckInWCFService/GetGroupInvoiceCheckInInfo_Compress" +
            "Response")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities.BPMApplicationExceptionInfo), Action="http://tempuri.org/IBillbookCheckInWCFService/GetGroupInvoiceCheckInInfo_Compress" +
            "BPMApplicationExceptionInfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        PEA.BPM.Architecture.CommonUtilities.CompressData GetGroupInvoiceCheckInInfo_Compress(string groupIvId, string branchId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBillbookCheckInWCFService/GetBillBookCheckInCancel", ReplyAction="http://tempuri.org/IBillbookCheckInWCFService/GetBillBookCheckInCancelResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities.BPMApplicationExceptionInfo), Action="http://tempuri.org/IBillbookCheckInWCFService/GetBillBookCheckInCancelBPMApplicat" +
            "ionExceptionInfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        PEA.BPM.AgencyManagementModule.Interface.BusinessEntities.BillBookCheckInInfo GetBillBookCheckInCancel(string billBookId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBillbookCheckInWCFService/GetBillBookCheckInCancel_Compress", ReplyAction="http://tempuri.org/IBillbookCheckInWCFService/GetBillBookCheckInCancel_CompressRe" +
            "sponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities.BPMApplicationExceptionInfo), Action="http://tempuri.org/IBillbookCheckInWCFService/GetBillBookCheckInCancel_CompressBP" +
            "MApplicationExceptionInfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        PEA.BPM.Architecture.CommonUtilities.CompressData GetBillBookCheckInCancel_Compress(string billBookId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBillbookCheckInWCFService/CreateBillBookCheckIn", ReplyAction="http://tempuri.org/IBillbookCheckInWCFService/CreateBillBookCheckInResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities.BPMApplicationExceptionInfo), Action="http://tempuri.org/IBillbookCheckInWCFService/CreateBillBookCheckInBPMApplication" +
            "ExceptionInfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        bool CreateBillBookCheckIn(PEA.BPM.Architecture.CommonUtilities.CompressData biilBookCheckIn, string branchId, string terminalId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBillbookCheckInWCFService/CancelBillBookCheckIn", ReplyAction="http://tempuri.org/IBillbookCheckInWCFService/CancelBillBookCheckInResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities.BPMApplicationExceptionInfo), Action="http://tempuri.org/IBillbookCheckInWCFService/CancelBillBookCheckInBPMApplication" +
            "ExceptionInfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        bool CancelBillBookCheckIn(PEA.BPM.Architecture.CommonUtilities.CompressData biilBookCheckIn);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IBillbookCheckInWCFService/CheckIsFullyPaid", ReplyAction = "http://tempuri.org/IBillbookCheckInWCFService/CheckIsFullyPaidResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities.BPMApplicationExceptionInfo), Action = "http://tempuri.org/IBillbookCheckInWCFService/CheckIsFullyPaidBPMApplication" +
            "ExceptionInfoFault", Name = "BPMApplicationExceptionInfo", Namespace = "http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction = WCFExtras.Soap.SoapHeaderDirection.In)]
        bool CheckIsFullyPaid(PEA.BPM.Architecture.CommonUtilities.CompressData biilBookCheckIn);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IBillbookCheckInWCFService/CheckIsSubmitGroupSameDay", ReplyAction = "http://tempuri.org/IBillbookCheckInWCFService/CheckIsSubmitGroupSameDayResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities.BPMApplicationExceptionInfo), Action = "http://tempuri.org/IBillbookCheckInWCFService/CheckIsSubmitGroupSameDayBPMApplication" +
            "ExceptionInfoFault", Name = "BPMApplicationExceptionInfo", Namespace = "http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction = WCFExtras.Soap.SoapHeaderDirection.In)]
        bool CheckIsSubmitGroupSameDay(PEA.BPM.Architecture.CommonUtilities.CompressData biilBookCheckIn);

        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBillbookCheckInWCFService/GetBillBookCheckInHistory", ReplyAction="http://tempuri.org/IBillbookCheckInWCFService/GetBillBookCheckInHistoryResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities.BPMApplicationExceptionInfo), Action="http://tempuri.org/IBillbookCheckInWCFService/GetBillBookCheckInHistoryBPMApplica" +
            "tionExceptionInfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        PEA.BPM.AgencyManagementModule.Interface.BusinessEntities.BillBookCheckInInfo GetBillBookCheckInHistory(string billBookId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBillbookCheckInWCFService/GetBillBookCheckInHistory_Compress", ReplyAction="http://tempuri.org/IBillbookCheckInWCFService/GetBillBookCheckInHistory_CompressR" +
            "esponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities.BPMApplicationExceptionInfo), Action="http://tempuri.org/IBillbookCheckInWCFService/GetBillBookCheckInHistory_CompressB" +
            "PMApplicationExceptionInfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        PEA.BPM.Architecture.CommonUtilities.CompressData GetBillBookCheckInHistory_Compress(string billBookId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBillbookCheckInWCFService/GetGroupInvoiceCheckInHistory", ReplyAction="http://tempuri.org/IBillbookCheckInWCFService/GetGroupInvoiceCheckInHistoryRespon" +
            "se")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities.BPMApplicationExceptionInfo), Action="http://tempuri.org/IBillbookCheckInWCFService/GetGroupInvoiceCheckInHistoryBPMApp" +
            "licationExceptionInfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        PEA.BPM.AgencyManagementModule.Interface.BusinessEntities.BillBookCheckInInfo GetGroupInvoiceCheckInHistory(string groupIvId, string branchId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBillbookCheckInWCFService/GetGroupInvoiceCheckInHistory_Compr" +
            "ess", ReplyAction="http://tempuri.org/IBillbookCheckInWCFService/GetGroupInvoiceCheckInHistory_Compr" +
            "essResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities.BPMApplicationExceptionInfo), Action="http://tempuri.org/IBillbookCheckInWCFService/GetGroupInvoiceCheckInHistory_Compr" +
            "essBPMApplicationExceptionInfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        PEA.BPM.Architecture.CommonUtilities.CompressData GetGroupInvoiceCheckInHistory_Compress(string groupIvId, string branchId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBillbookCheckInWCFService/GetChequeList", ReplyAction="http://tempuri.org/IBillbookCheckInWCFService/GetChequeListResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities.BPMApplicationExceptionInfo), Action="http://tempuri.org/IBillbookCheckInWCFService/GetChequeListBPMApplicationExceptio" +
            "nInfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        PEA.BPM.AgencyManagementModule.Interface.BusinessEntities.ChequeInfo[] GetChequeList(string billBookId, string invId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBillbookCheckInWCFService/GetChequeList_Compress", ReplyAction="http://tempuri.org/IBillbookCheckInWCFService/GetChequeList_CompressResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities.BPMApplicationExceptionInfo), Action="http://tempuri.org/IBillbookCheckInWCFService/GetChequeList_CompressBPMApplicatio" +
            "nExceptionInfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        PEA.BPM.Architecture.CommonUtilities.CompressData GetChequeList_Compress(string billBookId, string invId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBillbookCheckInWCFService/GetAdvPaidFromPOS", ReplyAction="http://tempuri.org/IBillbookCheckInWCFService/GetAdvPaidFromPOSResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities.BPMApplicationExceptionInfo), Action="http://tempuri.org/IBillbookCheckInWCFService/GetAdvPaidFromPOSBPMApplicationExce" +
            "ptionInfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        decimal GetAdvPaidFromPOS(string billBookId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBillbookCheckInWCFService/GetBookCheckInHistory_Compress", ReplyAction="http://tempuri.org/IBillbookCheckInWCFService/GetBookCheckInHistory_CompressRespo" +
            "nse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities.BPMApplicationExceptionInfo), Action="http://tempuri.org/IBillbookCheckInWCFService/GetBookCheckInHistory_CompressBPMAp" +
            "plicationExceptionInfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        PEA.BPM.Architecture.CommonUtilities.CompressData GetBookCheckInHistory_Compress(string bookId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBillbookCheckInWCFService/BillBookSaveState", ReplyAction="http://tempuri.org/IBillbookCheckInWCFService/BillBookSaveStateResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities.BPMApplicationExceptionInfo), Action="http://tempuri.org/IBillbookCheckInWCFService/BillBookSaveStateBPMApplicationExce" +
            "ptionInfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        void BillBookSaveState(PEA.BPM.Architecture.CommonUtilities.CompressData billbookCheckIn, string modifiedBy);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface IBillbookCheckInWCFServiceChannel : PEA.BPM.AgencyManagementModule.SG.BillBookCheckInWCF.IBillbookCheckInWCFService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class BillbookCheckInWCFServiceClient : System.ServiceModel.ClientBase<PEA.BPM.AgencyManagementModule.SG.BillBookCheckInWCF.IBillbookCheckInWCFService>, PEA.BPM.AgencyManagementModule.SG.BillBookCheckInWCF.IBillbookCheckInWCFService {
        
        public BillbookCheckInWCFServiceClient() {
        }
        
        public BillbookCheckInWCFServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public BillbookCheckInWCFServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BillbookCheckInWCFServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BillbookCheckInWCFServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public PEA.BPM.AgencyManagementModule.Interface.BusinessEntities.BillBookCheckInInfo GetBillBookCheckInInfo(string billBookId) {
            return base.Channel.GetBillBookCheckInInfo(billBookId);
        }
        
        public PEA.BPM.Architecture.CommonUtilities.CompressData GetBillBookCheckInInfo_Compress(string billBookId) {
            return base.Channel.GetBillBookCheckInInfo_Compress(billBookId);
        }
        
        public PEA.BPM.AgencyManagementModule.Interface.BusinessEntities.BillBookCheckInInfo GetGroupInvoiceCheckInInfo(string groupIvId, string branchId) {
            return base.Channel.GetGroupInvoiceCheckInInfo(groupIvId, branchId);
        }
        
        public PEA.BPM.Architecture.CommonUtilities.CompressData GetGroupInvoiceCheckInInfo_Compress(string groupIvId, string branchId) {
            return base.Channel.GetGroupInvoiceCheckInInfo_Compress(groupIvId, branchId);
        }
        
        public PEA.BPM.AgencyManagementModule.Interface.BusinessEntities.BillBookCheckInInfo GetBillBookCheckInCancel(string billBookId) {
            return base.Channel.GetBillBookCheckInCancel(billBookId);
        }
        
        public PEA.BPM.Architecture.CommonUtilities.CompressData GetBillBookCheckInCancel_Compress(string billBookId) {
            return base.Channel.GetBillBookCheckInCancel_Compress(billBookId);
        }
        
        public bool CreateBillBookCheckIn(PEA.BPM.Architecture.CommonUtilities.CompressData biilBookCheckIn, string branchId, string terminalId) {
            return base.Channel.CreateBillBookCheckIn(biilBookCheckIn, branchId, terminalId);
        }
        
        public bool CancelBillBookCheckIn(PEA.BPM.Architecture.CommonUtilities.CompressData biilBookCheckIn) {
            return base.Channel.CancelBillBookCheckIn(biilBookCheckIn);
        }

        public bool CheckIsFullyPaid(PEA.BPM.Architecture.CommonUtilities.CompressData biilBookCheckIn)
        {
            return base.Channel.CheckIsFullyPaid(biilBookCheckIn);
        }

        public bool CheckIsSubmitGroupSameDay(PEA.BPM.Architecture.CommonUtilities.CompressData biilBookCheckIn)
        {
            return base.Channel.CheckIsSubmitGroupSameDay(biilBookCheckIn);
        }

        public PEA.BPM.AgencyManagementModule.Interface.BusinessEntities.BillBookCheckInInfo GetBillBookCheckInHistory(string billBookId) {
            return base.Channel.GetBillBookCheckInHistory(billBookId);
        }
        
        public PEA.BPM.Architecture.CommonUtilities.CompressData GetBillBookCheckInHistory_Compress(string billBookId) {
            return base.Channel.GetBillBookCheckInHistory_Compress(billBookId);
        }
        
        public PEA.BPM.AgencyManagementModule.Interface.BusinessEntities.BillBookCheckInInfo GetGroupInvoiceCheckInHistory(string groupIvId, string branchId) {
            return base.Channel.GetGroupInvoiceCheckInHistory(groupIvId, branchId);
        }
        
        public PEA.BPM.Architecture.CommonUtilities.CompressData GetGroupInvoiceCheckInHistory_Compress(string groupIvId, string branchId) {
            return base.Channel.GetGroupInvoiceCheckInHistory_Compress(groupIvId, branchId);
        }
        
        public PEA.BPM.AgencyManagementModule.Interface.BusinessEntities.ChequeInfo[] GetChequeList(string billBookId, string invId) {
            return base.Channel.GetChequeList(billBookId, invId);
        }
        
        public PEA.BPM.Architecture.CommonUtilities.CompressData GetChequeList_Compress(string billBookId, string invId) {
            return base.Channel.GetChequeList_Compress(billBookId, invId);
        }
        
        public decimal GetAdvPaidFromPOS(string billBookId) {
            return base.Channel.GetAdvPaidFromPOS(billBookId);
        }
        
        public PEA.BPM.Architecture.CommonUtilities.CompressData GetBookCheckInHistory_Compress(string bookId) {
            return base.Channel.GetBookCheckInHistory_Compress(bookId);
        }
        
        public void BillBookSaveState(PEA.BPM.Architecture.CommonUtilities.CompressData billbookCheckIn, string modifiedBy) {
            base.Channel.BillBookSaveState(billbookCheckIn, modifiedBy);
        }
    }
}
