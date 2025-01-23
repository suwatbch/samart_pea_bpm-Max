﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.4927
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 2.0.50727.4927.
// 
#pragma warning disable 1591

namespace PEA.BPM.Integration.BPMIntegration.SG.CashDLWS {
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;
    using PEA.BPM.Architecture.CommonUtilities;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.4927")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="CashManagementWebServiceSoap", Namespace="http://tempuri.org/")]
    public partial class CashManagementWebService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private AuthenInfo authenInfoValueField;
        
        private System.Threading.SendOrPostCallback GetUpdateCashierWorkStatusOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetUpdateCashierMoneyTransferOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetUpdateCashierMoneyFlowOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetUpdateCashierMoneyFlowItemOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public CashManagementWebService() {
            this.Url = global::PEA.BPM.Integration.BPMIntegration.SG.Properties.Settings.Default.PEA_BPM_Integration_BPMIntegration_SG_CashDLWS_CashManagementWebService;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public AuthenInfo AuthenInfoValue {
            get {
                return this.authenInfoValueField;
            }
            set {
                this.authenInfoValueField = value;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event GetUpdateCashierWorkStatusCompletedEventHandler GetUpdateCashierWorkStatusCompleted;
        
        /// <remarks/>
        public event GetUpdateCashierMoneyTransferCompletedEventHandler GetUpdateCashierMoneyTransferCompleted;
        
        /// <remarks/>
        public event GetUpdateCashierMoneyFlowCompletedEventHandler GetUpdateCashierMoneyFlowCompleted;
        
        /// <remarks/>
        public event GetUpdateCashierMoneyFlowItemCompletedEventHandler GetUpdateCashierMoneyFlowItemCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("AuthenInfoValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetUpdateCashierWorkStatus", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public CompressData GetUpdateCashierWorkStatus(string branchId, System.DateTime lastModifiedDate, System.DateTime serverDate) {
            object[] results = this.Invoke("GetUpdateCashierWorkStatus", new object[] {
                        branchId,
                        lastModifiedDate,
                        serverDate});
            return ((CompressData)(results[0]));
        }
        
        /// <remarks/>
        public void GetUpdateCashierWorkStatusAsync(string branchId, System.DateTime lastModifiedDate, System.DateTime serverDate) {
            this.GetUpdateCashierWorkStatusAsync(branchId, lastModifiedDate, serverDate, null);
        }
        
        /// <remarks/>
        public void GetUpdateCashierWorkStatusAsync(string branchId, System.DateTime lastModifiedDate, System.DateTime serverDate, object userState) {
            if ((this.GetUpdateCashierWorkStatusOperationCompleted == null)) {
                this.GetUpdateCashierWorkStatusOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetUpdateCashierWorkStatusOperationCompleted);
            }
            this.InvokeAsync("GetUpdateCashierWorkStatus", new object[] {
                        branchId,
                        lastModifiedDate,
                        serverDate}, this.GetUpdateCashierWorkStatusOperationCompleted, userState);
        }
        
        private void OnGetUpdateCashierWorkStatusOperationCompleted(object arg) {
            if ((this.GetUpdateCashierWorkStatusCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetUpdateCashierWorkStatusCompleted(this, new GetUpdateCashierWorkStatusCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("AuthenInfoValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetUpdateCashierMoneyTransfer", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public CompressData GetUpdateCashierMoneyTransfer(string branchId, System.DateTime lastModifiedDate, System.DateTime serverDate) {
            object[] results = this.Invoke("GetUpdateCashierMoneyTransfer", new object[] {
                        branchId,
                        lastModifiedDate,
                        serverDate});
            return ((CompressData)(results[0]));
        }
        
        /// <remarks/>
        public void GetUpdateCashierMoneyTransferAsync(string branchId, System.DateTime lastModifiedDate, System.DateTime serverDate) {
            this.GetUpdateCashierMoneyTransferAsync(branchId, lastModifiedDate, serverDate, null);
        }
        
        /// <remarks/>
        public void GetUpdateCashierMoneyTransferAsync(string branchId, System.DateTime lastModifiedDate, System.DateTime serverDate, object userState) {
            if ((this.GetUpdateCashierMoneyTransferOperationCompleted == null)) {
                this.GetUpdateCashierMoneyTransferOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetUpdateCashierMoneyTransferOperationCompleted);
            }
            this.InvokeAsync("GetUpdateCashierMoneyTransfer", new object[] {
                        branchId,
                        lastModifiedDate,
                        serverDate}, this.GetUpdateCashierMoneyTransferOperationCompleted, userState);
        }
        
        private void OnGetUpdateCashierMoneyTransferOperationCompleted(object arg) {
            if ((this.GetUpdateCashierMoneyTransferCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetUpdateCashierMoneyTransferCompleted(this, new GetUpdateCashierMoneyTransferCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("AuthenInfoValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetUpdateCashierMoneyFlow", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public CompressData GetUpdateCashierMoneyFlow(string branchId, System.DateTime lastModifiedDate, System.DateTime serverDate) {
            object[] results = this.Invoke("GetUpdateCashierMoneyFlow", new object[] {
                        branchId,
                        lastModifiedDate,
                        serverDate});
            return ((CompressData)(results[0]));
        }
        
        /// <remarks/>
        public void GetUpdateCashierMoneyFlowAsync(string branchId, System.DateTime lastModifiedDate, System.DateTime serverDate) {
            this.GetUpdateCashierMoneyFlowAsync(branchId, lastModifiedDate, serverDate, null);
        }
        
        /// <remarks/>
        public void GetUpdateCashierMoneyFlowAsync(string branchId, System.DateTime lastModifiedDate, System.DateTime serverDate, object userState) {
            if ((this.GetUpdateCashierMoneyFlowOperationCompleted == null)) {
                this.GetUpdateCashierMoneyFlowOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetUpdateCashierMoneyFlowOperationCompleted);
            }
            this.InvokeAsync("GetUpdateCashierMoneyFlow", new object[] {
                        branchId,
                        lastModifiedDate,
                        serverDate}, this.GetUpdateCashierMoneyFlowOperationCompleted, userState);
        }
        
        private void OnGetUpdateCashierMoneyFlowOperationCompleted(object arg) {
            if ((this.GetUpdateCashierMoneyFlowCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetUpdateCashierMoneyFlowCompleted(this, new GetUpdateCashierMoneyFlowCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("AuthenInfoValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetUpdateCashierMoneyFlowItem", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public CompressData GetUpdateCashierMoneyFlowItem(string branchId, System.DateTime lastModifiedDate, System.DateTime serverDate) {
            object[] results = this.Invoke("GetUpdateCashierMoneyFlowItem", new object[] {
                        branchId,
                        lastModifiedDate,
                        serverDate});
            return ((CompressData)(results[0]));
        }
        
        /// <remarks/>
        public void GetUpdateCashierMoneyFlowItemAsync(string branchId, System.DateTime lastModifiedDate, System.DateTime serverDate) {
            this.GetUpdateCashierMoneyFlowItemAsync(branchId, lastModifiedDate, serverDate, null);
        }
        
        /// <remarks/>
        public void GetUpdateCashierMoneyFlowItemAsync(string branchId, System.DateTime lastModifiedDate, System.DateTime serverDate, object userState) {
            if ((this.GetUpdateCashierMoneyFlowItemOperationCompleted == null)) {
                this.GetUpdateCashierMoneyFlowItemOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetUpdateCashierMoneyFlowItemOperationCompleted);
            }
            this.InvokeAsync("GetUpdateCashierMoneyFlowItem", new object[] {
                        branchId,
                        lastModifiedDate,
                        serverDate}, this.GetUpdateCashierMoneyFlowItemOperationCompleted, userState);
        }
        
        private void OnGetUpdateCashierMoneyFlowItemOperationCompleted(object arg) {
            if ((this.GetUpdateCashierMoneyFlowItemCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetUpdateCashierMoneyFlowItemCompleted(this, new GetUpdateCashierMoneyFlowItemCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.4927")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://tempuri.org/", IsNullable=false)]
    public partial class AuthenInfo : System.Web.Services.Protocols.SoapHeader {
        
        private string userIdField;
        
        private string authTokenField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        public string UserId {
            get {
                return this.userIdField;
            }
            set {
                this.userIdField = value;
            }
        }
        
        /// <remarks/>
        public string AuthToken {
            get {
                return this.authTokenField;
            }
            set {
                this.authTokenField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.4927")]
    public delegate void GetUpdateCashierWorkStatusCompletedEventHandler(object sender, GetUpdateCashierWorkStatusCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.4927")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetUpdateCashierWorkStatusCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetUpdateCashierWorkStatusCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public CompressData Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((CompressData)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.4927")]
    public delegate void GetUpdateCashierMoneyTransferCompletedEventHandler(object sender, GetUpdateCashierMoneyTransferCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.4927")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetUpdateCashierMoneyTransferCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetUpdateCashierMoneyTransferCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public CompressData Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((CompressData)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.4927")]
    public delegate void GetUpdateCashierMoneyFlowCompletedEventHandler(object sender, GetUpdateCashierMoneyFlowCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.4927")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetUpdateCashierMoneyFlowCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetUpdateCashierMoneyFlowCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public CompressData Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((CompressData)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.4927")]
    public delegate void GetUpdateCashierMoneyFlowItemCompletedEventHandler(object sender, GetUpdateCashierMoneyFlowItemCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.4927")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetUpdateCashierMoneyFlowItemCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetUpdateCashierMoneyFlowItemCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public CompressData Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((CompressData)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591