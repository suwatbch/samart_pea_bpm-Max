﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.5466
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PEA.BPM.NewsBroadcast.SG.BroadcastWCF {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
        "e.BusinessEntities")]
    [System.SerializableAttribute()]
    public partial class BPMApplicationExceptionInfo : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ErrorCodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string OriginalTypeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime OccurWhenField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ModuleField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int LayerField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DebuggingIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MessageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StackTraceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SourceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool CanContinueField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AuthTokenField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private PEA.BPM.NewsBroadcast.SG.BroadcastWCF.SimpleExceptionInfo[] AdditionalInfoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string THMessageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CauseField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ResolveField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string HelpURLField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ErrorCode {
            get {
                return this.ErrorCodeField;
            }
            set {
                if ((object.ReferenceEquals(this.ErrorCodeField, value) != true)) {
                    this.ErrorCodeField = value;
                    this.RaisePropertyChanged("ErrorCode");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string OriginalType {
            get {
                return this.OriginalTypeField;
            }
            set {
                if ((object.ReferenceEquals(this.OriginalTypeField, value) != true)) {
                    this.OriginalTypeField = value;
                    this.RaisePropertyChanged("OriginalType");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public System.DateTime OccurWhen {
            get {
                return this.OccurWhenField;
            }
            set {
                if ((this.OccurWhenField.Equals(value) != true)) {
                    this.OccurWhenField = value;
                    this.RaisePropertyChanged("OccurWhen");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public int Module {
            get {
                return this.ModuleField;
            }
            set {
                if ((this.ModuleField.Equals(value) != true)) {
                    this.ModuleField = value;
                    this.RaisePropertyChanged("Module");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public int Layer {
            get {
                return this.LayerField;
            }
            set {
                if ((this.LayerField.Equals(value) != true)) {
                    this.LayerField = value;
                    this.RaisePropertyChanged("Layer");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=5)]
        public string DebuggingId {
            get {
                return this.DebuggingIdField;
            }
            set {
                if ((object.ReferenceEquals(this.DebuggingIdField, value) != true)) {
                    this.DebuggingIdField = value;
                    this.RaisePropertyChanged("DebuggingId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=6)]
        public string Message {
            get {
                return this.MessageField;
            }
            set {
                if ((object.ReferenceEquals(this.MessageField, value) != true)) {
                    this.MessageField = value;
                    this.RaisePropertyChanged("Message");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=7)]
        public string StackTrace {
            get {
                return this.StackTraceField;
            }
            set {
                if ((object.ReferenceEquals(this.StackTraceField, value) != true)) {
                    this.StackTraceField = value;
                    this.RaisePropertyChanged("StackTrace");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=8)]
        public string Source {
            get {
                return this.SourceField;
            }
            set {
                if ((object.ReferenceEquals(this.SourceField, value) != true)) {
                    this.SourceField = value;
                    this.RaisePropertyChanged("Source");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=9)]
        public bool CanContinue {
            get {
                return this.CanContinueField;
            }
            set {
                if ((this.CanContinueField.Equals(value) != true)) {
                    this.CanContinueField = value;
                    this.RaisePropertyChanged("CanContinue");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=10)]
        public string UserId {
            get {
                return this.UserIdField;
            }
            set {
                if ((object.ReferenceEquals(this.UserIdField, value) != true)) {
                    this.UserIdField = value;
                    this.RaisePropertyChanged("UserId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=11)]
        public string AuthToken {
            get {
                return this.AuthTokenField;
            }
            set {
                if ((object.ReferenceEquals(this.AuthTokenField, value) != true)) {
                    this.AuthTokenField = value;
                    this.RaisePropertyChanged("AuthToken");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=12)]
        public PEA.BPM.NewsBroadcast.SG.BroadcastWCF.SimpleExceptionInfo[] AdditionalInfo {
            get {
                return this.AdditionalInfoField;
            }
            set {
                if ((object.ReferenceEquals(this.AdditionalInfoField, value) != true)) {
                    this.AdditionalInfoField = value;
                    this.RaisePropertyChanged("AdditionalInfo");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=13)]
        public string THMessage {
            get {
                return this.THMessageField;
            }
            set {
                if ((object.ReferenceEquals(this.THMessageField, value) != true)) {
                    this.THMessageField = value;
                    this.RaisePropertyChanged("THMessage");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=14)]
        public string Cause {
            get {
                return this.CauseField;
            }
            set {
                if ((object.ReferenceEquals(this.CauseField, value) != true)) {
                    this.CauseField = value;
                    this.RaisePropertyChanged("Cause");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=15)]
        public string Resolve {
            get {
                return this.ResolveField;
            }
            set {
                if ((object.ReferenceEquals(this.ResolveField, value) != true)) {
                    this.ResolveField = value;
                    this.RaisePropertyChanged("Resolve");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=16)]
        public string HelpURL {
            get {
                return this.HelpURLField;
            }
            set {
                if ((object.ReferenceEquals(this.HelpURLField, value) != true)) {
                    this.HelpURLField = value;
                    this.RaisePropertyChanged("HelpURL");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SimpleExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
        "e.BusinessEntities")]
    [System.SerializableAttribute()]
    public partial class SimpleExceptionInfo : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MessageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StackTraceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AdditionalField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TypeNameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Message {
            get {
                return this.MessageField;
            }
            set {
                if ((object.ReferenceEquals(this.MessageField, value) != true)) {
                    this.MessageField = value;
                    this.RaisePropertyChanged("Message");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string StackTrace {
            get {
                return this.StackTraceField;
            }
            set {
                if ((object.ReferenceEquals(this.StackTraceField, value) != true)) {
                    this.StackTraceField = value;
                    this.RaisePropertyChanged("StackTrace");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public string Additional {
            get {
                return this.AdditionalField;
            }
            set {
                if ((object.ReferenceEquals(this.AdditionalField, value) != true)) {
                    this.AdditionalField = value;
                    this.RaisePropertyChanged("Additional");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public string TypeName {
            get {
                return this.TypeNameField;
            }
            set {
                if ((object.ReferenceEquals(this.TypeNameField, value) != true)) {
                    this.TypeNameField = value;
                    this.RaisePropertyChanged("TypeName");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://tempuri.org", ConfigurationName="BroadcastWCF.IBroadcastWCFService")]
    [WCFExtras.Soap.SoapHeadersAttribute()]
    public interface IBroadcastWCFService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBroadcastWCFService/GetNewsBroadcastNow", ReplyAction="http://tempuri.org/IBroadcastWCFService/GetNewsBroadcastNowResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.NewsBroadcast.SG.BroadcastWCF.BPMApplicationExceptionInfo), Action="http://tempuri.org/IBroadcastWCFService/GetNewsBroadcastNowBPMApplicationExceptio" +
            "nInfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        PEA.BPM.Architecture.CommonUtilities.CompressData GetNewsBroadcastNow(System.DateTime _nowDt, string _userId, int _cmdId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBroadcastWCFService/GetNewsBroadcastHistory", ReplyAction="http://tempuri.org/IBroadcastWCFService/GetNewsBroadcastHistoryResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.NewsBroadcast.SG.BroadcastWCF.BPMApplicationExceptionInfo), Action="http://tempuri.org/IBroadcastWCFService/GetNewsBroadcastHistoryBPMApplicationExce" +
            "ptionInfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        PEA.BPM.Architecture.CommonUtilities.CompressData GetNewsBroadcastHistory(System.DateTime _nowDt, string _userId, int _cmdId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBroadcastWCFService/GetNewsBroadcastSent", ReplyAction="http://tempuri.org/IBroadcastWCFService/GetNewsBroadcastSentResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.NewsBroadcast.SG.BroadcastWCF.BPMApplicationExceptionInfo), Action="http://tempuri.org/IBroadcastWCFService/GetNewsBroadcastSentBPMApplicationExcepti" +
            "onInfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        PEA.BPM.Architecture.CommonUtilities.CompressData GetNewsBroadcastSent(System.DateTime _sentDt);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBroadcastWCFService/GetNewsBroadcastNowForSender", ReplyAction="http://tempuri.org/IBroadcastWCFService/GetNewsBroadcastNowForSenderResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.NewsBroadcast.SG.BroadcastWCF.BPMApplicationExceptionInfo), Action="http://tempuri.org/IBroadcastWCFService/GetNewsBroadcastNowForSenderBPMApplicatio" +
            "nExceptionInfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        PEA.BPM.Architecture.CommonUtilities.CompressData GetNewsBroadcastNowForSender(System.DateTime _nowDt, int _cmdId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBroadcastWCFService/GetNewsBroadcastScheduled", ReplyAction="http://tempuri.org/IBroadcastWCFService/GetNewsBroadcastScheduledResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.NewsBroadcast.SG.BroadcastWCF.BPMApplicationExceptionInfo), Action="http://tempuri.org/IBroadcastWCFService/GetNewsBroadcastScheduledBPMApplicationEx" +
            "ceptionInfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        PEA.BPM.Architecture.CommonUtilities.CompressData GetNewsBroadcastScheduled(System.DateTime _nowDt, int _cmdId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBroadcastWCFService/GetNewsBroadcastSentMonthYears", ReplyAction="http://tempuri.org/IBroadcastWCFService/GetNewsBroadcastSentMonthYearsResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.NewsBroadcast.SG.BroadcastWCF.BPMApplicationExceptionInfo), Action="http://tempuri.org/IBroadcastWCFService/GetNewsBroadcastSentMonthYearsBPMApplicat" +
            "ionExceptionInfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        PEA.BPM.Architecture.CommonUtilities.CompressData GetNewsBroadcastSentMonthYears(System.DateTime _sentDt);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBroadcastWCFService/GetNewsBroadcastUser", ReplyAction="http://tempuri.org/IBroadcastWCFService/GetNewsBroadcastUserResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.NewsBroadcast.SG.BroadcastWCF.BPMApplicationExceptionInfo), Action="http://tempuri.org/IBroadcastWCFService/GetNewsBroadcastUserBPMApplicationExcepti" +
            "onInfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        PEA.BPM.Architecture.CommonUtilities.CompressData GetNewsBroadcastUser(int _broadcastId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBroadcastWCFService/UpdateNewsBroadcastUserOpened", ReplyAction="http://tempuri.org/IBroadcastWCFService/UpdateNewsBroadcastUserOpenedResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.NewsBroadcast.SG.BroadcastWCF.BPMApplicationExceptionInfo), Action="http://tempuri.org/IBroadcastWCFService/UpdateNewsBroadcastUserOpenedBPMApplicati" +
            "onExceptionInfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        void UpdateNewsBroadcastUserOpened(int _broadcastId, string _userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBroadcastWCFService/UpdateNewsBroadcastUserRead", ReplyAction="http://tempuri.org/IBroadcastWCFService/UpdateNewsBroadcastUserReadResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.NewsBroadcast.SG.BroadcastWCF.BPMApplicationExceptionInfo), Action="http://tempuri.org/IBroadcastWCFService/UpdateNewsBroadcastUserReadBPMApplication" +
            "ExceptionInfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        void UpdateNewsBroadcastUserRead(int _broadcastId, string _userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBroadcastWCFService/GetArea", ReplyAction="http://tempuri.org/IBroadcastWCFService/GetAreaResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.NewsBroadcast.SG.BroadcastWCF.BPMApplicationExceptionInfo), Action="http://tempuri.org/IBroadcastWCFService/GetAreaBPMApplicationExceptionInfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        PEA.BPM.Architecture.CommonUtilities.CompressData GetArea();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBroadcastWCFService/GetBranch", ReplyAction="http://tempuri.org/IBroadcastWCFService/GetBranchResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.NewsBroadcast.SG.BroadcastWCF.BPMApplicationExceptionInfo), Action="http://tempuri.org/IBroadcastWCFService/GetBranchBPMApplicationExceptionInfoFault" +
            "", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        PEA.BPM.Architecture.CommonUtilities.CompressData GetBranch(string _areaId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBroadcastWCFService/GetUser", ReplyAction="http://tempuri.org/IBroadcastWCFService/GetUserResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.NewsBroadcast.SG.BroadcastWCF.BPMApplicationExceptionInfo), Action="http://tempuri.org/IBroadcastWCFService/GetUserBPMApplicationExceptionInfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        PEA.BPM.Architecture.CommonUtilities.CompressData GetUser(string _roleId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBroadcastWCFService/GetRole", ReplyAction="http://tempuri.org/IBroadcastWCFService/GetRoleResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.NewsBroadcast.SG.BroadcastWCF.BPMApplicationExceptionInfo), Action="http://tempuri.org/IBroadcastWCFService/GetRoleBPMApplicationExceptionInfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        PEA.BPM.Architecture.CommonUtilities.CompressData GetRole();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBroadcastWCFService/InsertNewsBroadcast", ReplyAction="http://tempuri.org/IBroadcastWCFService/InsertNewsBroadcastResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.NewsBroadcast.SG.BroadcastWCF.BPMApplicationExceptionInfo), Action="http://tempuri.org/IBroadcastWCFService/InsertNewsBroadcastBPMApplicationExceptio" +
            "nInfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        void InsertNewsBroadcast(string _broadcastTopic, string _detail, System.DateTime _sentDt, System.DateTime _expireDt, int _cmdId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBroadcastWCFService/InsertNewsBroadcastUser", ReplyAction="http://tempuri.org/IBroadcastWCFService/InsertNewsBroadcastUserResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.NewsBroadcast.SG.BroadcastWCF.BPMApplicationExceptionInfo), Action="http://tempuri.org/IBroadcastWCFService/InsertNewsBroadcastUserBPMApplicationExce" +
            "ptionInfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        void InsertNewsBroadcastUser(int _broadcastId, string _userId, string _areaId, string _branchId, string _branchName2, string _roleId, string _roleName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBroadcastWCFService/GetLastNewsBroadcastId", ReplyAction="http://tempuri.org/IBroadcastWCFService/GetLastNewsBroadcastIdResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.NewsBroadcast.SG.BroadcastWCF.BPMApplicationExceptionInfo), Action="http://tempuri.org/IBroadcastWCFService/GetLastNewsBroadcastIdBPMApplicationExcep" +
            "tionInfoFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        int GetLastNewsBroadcastId();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBroadcastWCFService/IsDuplicateUser", ReplyAction="http://tempuri.org/IBroadcastWCFService/IsDuplicateUserResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(PEA.BPM.NewsBroadcast.SG.BroadcastWCF.BPMApplicationExceptionInfo), Action="http://tempuri.org/IBroadcastWCFService/IsDuplicateUserBPMApplicationExceptionInf" +
            "oFault", Name="BPMApplicationExceptionInfo", Namespace="http://schemas.datacontract.org/2004/07/PEA.BPM.Architecture.ArchitectureInterfac" +
            "e.BusinessEntities")]
        [WCFExtras.Soap.SoapHeaderAttribute("AuthInfoValue", typeof(PEA.BPM.Architecture.CommonUtilities.AuthInfo), Direction=WCFExtras.Soap.SoapHeaderDirection.In)]
        bool IsDuplicateUser(int _broadcastId, string _userId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface IBroadcastWCFServiceChannel : PEA.BPM.NewsBroadcast.SG.BroadcastWCF.IBroadcastWCFService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class BroadcastWCFServiceClient : System.ServiceModel.ClientBase<PEA.BPM.NewsBroadcast.SG.BroadcastWCF.IBroadcastWCFService>, PEA.BPM.NewsBroadcast.SG.BroadcastWCF.IBroadcastWCFService {
        
        public BroadcastWCFServiceClient() {
        }
        
        public BroadcastWCFServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public BroadcastWCFServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BroadcastWCFServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BroadcastWCFServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public PEA.BPM.Architecture.CommonUtilities.CompressData GetNewsBroadcastNow(System.DateTime _nowDt, string _userId, int _cmdId) {
            return base.Channel.GetNewsBroadcastNow(_nowDt, _userId, _cmdId);
        }
        
        public PEA.BPM.Architecture.CommonUtilities.CompressData GetNewsBroadcastHistory(System.DateTime _nowDt, string _userId, int _cmdId) {
            return base.Channel.GetNewsBroadcastHistory(_nowDt, _userId, _cmdId);
        }
        
        public PEA.BPM.Architecture.CommonUtilities.CompressData GetNewsBroadcastSent(System.DateTime _sentDt) {
            return base.Channel.GetNewsBroadcastSent(_sentDt);
        }
        
        public PEA.BPM.Architecture.CommonUtilities.CompressData GetNewsBroadcastNowForSender(System.DateTime _nowDt, int _cmdId) {
            return base.Channel.GetNewsBroadcastNowForSender(_nowDt, _cmdId);
        }
        
        public PEA.BPM.Architecture.CommonUtilities.CompressData GetNewsBroadcastScheduled(System.DateTime _nowDt, int _cmdId) {
            return base.Channel.GetNewsBroadcastScheduled(_nowDt, _cmdId);
        }
        
        public PEA.BPM.Architecture.CommonUtilities.CompressData GetNewsBroadcastSentMonthYears(System.DateTime _sentDt) {
            return base.Channel.GetNewsBroadcastSentMonthYears(_sentDt);
        }
        
        public PEA.BPM.Architecture.CommonUtilities.CompressData GetNewsBroadcastUser(int _broadcastId) {
            return base.Channel.GetNewsBroadcastUser(_broadcastId);
        }
        
        public void UpdateNewsBroadcastUserOpened(int _broadcastId, string _userId) {
            base.Channel.UpdateNewsBroadcastUserOpened(_broadcastId, _userId);
        }
        
        public void UpdateNewsBroadcastUserRead(int _broadcastId, string _userId) {
            base.Channel.UpdateNewsBroadcastUserRead(_broadcastId, _userId);
        }
        
        public PEA.BPM.Architecture.CommonUtilities.CompressData GetArea() {
            return base.Channel.GetArea();
        }
        
        public PEA.BPM.Architecture.CommonUtilities.CompressData GetBranch(string _areaId) {
            return base.Channel.GetBranch(_areaId);
        }
        
        public PEA.BPM.Architecture.CommonUtilities.CompressData GetUser(string _roleId) {
            return base.Channel.GetUser(_roleId);
        }
        
        public PEA.BPM.Architecture.CommonUtilities.CompressData GetRole() {
            return base.Channel.GetRole();
        }
        
        public void InsertNewsBroadcast(string _broadcastTopic, string _detail, System.DateTime _sentDt, System.DateTime _expireDt, int _cmdId) {
            base.Channel.InsertNewsBroadcast(_broadcastTopic, _detail, _sentDt, _expireDt, _cmdId);
        }
        
        public void InsertNewsBroadcastUser(int _broadcastId, string _userId, string _areaId, string _branchId, string _branchName2, string _roleId, string _roleName) {
            base.Channel.InsertNewsBroadcastUser(_broadcastId, _userId, _areaId, _branchId, _branchName2, _roleId, _roleName);
        }
        
        public int GetLastNewsBroadcastId() {
            return base.Channel.GetLastNewsBroadcastId();
        }
        
        public bool IsDuplicateUser(int _broadcastId, string _userId) {
            return base.Channel.IsDuplicateUser(_broadcastId, _userId);
        }
    }
}
