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

namespace PEA.BPM.Integration.BPMIntegration.SG.AgencyDLWS {
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
    [System.Web.Services.WebServiceBindingAttribute(Name="AgencyIntegrationWebServiceSoap", Namespace="http://tempuri.org/")]
    public partial class AgencyIntegrationWebService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private AuthenInfo authenInfoValueField;
        
        private System.Threading.SendOrPostCallback DownloadBillBookOperationCompleted;
        
        private System.Threading.SendOrPostCallback DownloadBillStatusInfoOperationCompleted;
        
        private System.Threading.SendOrPostCallback DownloadBillBookDetailOperationCompleted;
        
        private System.Threading.SendOrPostCallback DownloadBillBookInputItemOperationCompleted;
        
        private System.Threading.SendOrPostCallback DownloadBillBookInputSetOperationCompleted;
        
        private System.Threading.SendOrPostCallback DownloadAgencyCommissionOperationCompleted;
        
        private System.Threading.SendOrPostCallback DownloadRTAgencyContractMruOperationCompleted;
        
        private System.Threading.SendOrPostCallback DownloadRTAgencyCommissionBillBookOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public AgencyIntegrationWebService() {
            this.Url = global::PEA.BPM.Integration.BPMIntegration.SG.Properties.Settings.Default.PEA_BPM_Integration_BPMIntegration_SG_AgencyDLWS_AgencyIntegrationWebService;
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
        public event DownloadBillBookCompletedEventHandler DownloadBillBookCompleted;
        
        /// <remarks/>
        public event DownloadBillStatusInfoCompletedEventHandler DownloadBillStatusInfoCompleted;
        
        /// <remarks/>
        public event DownloadBillBookDetailCompletedEventHandler DownloadBillBookDetailCompleted;
        
        /// <remarks/>
        public event DownloadBillBookInputItemCompletedEventHandler DownloadBillBookInputItemCompleted;
        
        /// <remarks/>
        public event DownloadBillBookInputSetCompletedEventHandler DownloadBillBookInputSetCompleted;
        
        /// <remarks/>
        public event DownloadAgencyCommissionCompletedEventHandler DownloadAgencyCommissionCompleted;
        
        /// <remarks/>
        public event DownloadRTAgencyContractMruCompletedEventHandler DownloadRTAgencyContractMruCompleted;
        
        /// <remarks/>
        public event DownloadRTAgencyCommissionBillBookCompletedEventHandler DownloadRTAgencyCommissionBillBookCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("AuthenInfoValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/DownloadBillBook", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public CompressData DownloadBillBook(string branchId, System.DateTime lastModifiedDt, System.DateTime serverDate) {
            object[] results = this.Invoke("DownloadBillBook", new object[] {
                        branchId,
                        lastModifiedDt,
                        serverDate});
            return ((CompressData)(results[0]));
        }
        
        /// <remarks/>
        public void DownloadBillBookAsync(string branchId, System.DateTime lastModifiedDt, System.DateTime serverDate) {
            this.DownloadBillBookAsync(branchId, lastModifiedDt, serverDate, null);
        }
        
        /// <remarks/>
        public void DownloadBillBookAsync(string branchId, System.DateTime lastModifiedDt, System.DateTime serverDate, object userState) {
            if ((this.DownloadBillBookOperationCompleted == null)) {
                this.DownloadBillBookOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDownloadBillBookOperationCompleted);
            }
            this.InvokeAsync("DownloadBillBook", new object[] {
                        branchId,
                        lastModifiedDt,
                        serverDate}, this.DownloadBillBookOperationCompleted, userState);
        }
        
        private void OnDownloadBillBookOperationCompleted(object arg) {
            if ((this.DownloadBillBookCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DownloadBillBookCompleted(this, new DownloadBillBookCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("AuthenInfoValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/DownloadBillStatusInfo", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public CompressData DownloadBillStatusInfo(string branchId, System.DateTime lastModifiedDt, System.DateTime serverDate) {
            object[] results = this.Invoke("DownloadBillStatusInfo", new object[] {
                        branchId,
                        lastModifiedDt,
                        serverDate});
            return ((CompressData)(results[0]));
        }
        
        /// <remarks/>
        public void DownloadBillStatusInfoAsync(string branchId, System.DateTime lastModifiedDt, System.DateTime serverDate) {
            this.DownloadBillStatusInfoAsync(branchId, lastModifiedDt, serverDate, null);
        }
        
        /// <remarks/>
        public void DownloadBillStatusInfoAsync(string branchId, System.DateTime lastModifiedDt, System.DateTime serverDate, object userState) {
            if ((this.DownloadBillStatusInfoOperationCompleted == null)) {
                this.DownloadBillStatusInfoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDownloadBillStatusInfoOperationCompleted);
            }
            this.InvokeAsync("DownloadBillStatusInfo", new object[] {
                        branchId,
                        lastModifiedDt,
                        serverDate}, this.DownloadBillStatusInfoOperationCompleted, userState);
        }
        
        private void OnDownloadBillStatusInfoOperationCompleted(object arg) {
            if ((this.DownloadBillStatusInfoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DownloadBillStatusInfoCompleted(this, new DownloadBillStatusInfoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("AuthenInfoValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/DownloadBillBookDetail", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public CompressData DownloadBillBookDetail(string branchId, System.DateTime lastModifiedDt, System.DateTime serverDate) {
            object[] results = this.Invoke("DownloadBillBookDetail", new object[] {
                        branchId,
                        lastModifiedDt,
                        serverDate});
            return ((CompressData)(results[0]));
        }
        
        /// <remarks/>
        public void DownloadBillBookDetailAsync(string branchId, System.DateTime lastModifiedDt, System.DateTime serverDate) {
            this.DownloadBillBookDetailAsync(branchId, lastModifiedDt, serverDate, null);
        }
        
        /// <remarks/>
        public void DownloadBillBookDetailAsync(string branchId, System.DateTime lastModifiedDt, System.DateTime serverDate, object userState) {
            if ((this.DownloadBillBookDetailOperationCompleted == null)) {
                this.DownloadBillBookDetailOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDownloadBillBookDetailOperationCompleted);
            }
            this.InvokeAsync("DownloadBillBookDetail", new object[] {
                        branchId,
                        lastModifiedDt,
                        serverDate}, this.DownloadBillBookDetailOperationCompleted, userState);
        }
        
        private void OnDownloadBillBookDetailOperationCompleted(object arg) {
            if ((this.DownloadBillBookDetailCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DownloadBillBookDetailCompleted(this, new DownloadBillBookDetailCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("AuthenInfoValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/DownloadBillBookInputItem", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public CompressData DownloadBillBookInputItem(string branchId, System.DateTime lastModifiedDt, System.DateTime serverDate) {
            object[] results = this.Invoke("DownloadBillBookInputItem", new object[] {
                        branchId,
                        lastModifiedDt,
                        serverDate});
            return ((CompressData)(results[0]));
        }
        
        /// <remarks/>
        public void DownloadBillBookInputItemAsync(string branchId, System.DateTime lastModifiedDt, System.DateTime serverDate) {
            this.DownloadBillBookInputItemAsync(branchId, lastModifiedDt, serverDate, null);
        }
        
        /// <remarks/>
        public void DownloadBillBookInputItemAsync(string branchId, System.DateTime lastModifiedDt, System.DateTime serverDate, object userState) {
            if ((this.DownloadBillBookInputItemOperationCompleted == null)) {
                this.DownloadBillBookInputItemOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDownloadBillBookInputItemOperationCompleted);
            }
            this.InvokeAsync("DownloadBillBookInputItem", new object[] {
                        branchId,
                        lastModifiedDt,
                        serverDate}, this.DownloadBillBookInputItemOperationCompleted, userState);
        }
        
        private void OnDownloadBillBookInputItemOperationCompleted(object arg) {
            if ((this.DownloadBillBookInputItemCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DownloadBillBookInputItemCompleted(this, new DownloadBillBookInputItemCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("AuthenInfoValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/DownloadBillBookInputSet", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public CompressData DownloadBillBookInputSet(string branchId, System.DateTime lastModifiedDt, System.DateTime serverDate) {
            object[] results = this.Invoke("DownloadBillBookInputSet", new object[] {
                        branchId,
                        lastModifiedDt,
                        serverDate});
            return ((CompressData)(results[0]));
        }
        
        /// <remarks/>
        public void DownloadBillBookInputSetAsync(string branchId, System.DateTime lastModifiedDt, System.DateTime serverDate) {
            this.DownloadBillBookInputSetAsync(branchId, lastModifiedDt, serverDate, null);
        }
        
        /// <remarks/>
        public void DownloadBillBookInputSetAsync(string branchId, System.DateTime lastModifiedDt, System.DateTime serverDate, object userState) {
            if ((this.DownloadBillBookInputSetOperationCompleted == null)) {
                this.DownloadBillBookInputSetOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDownloadBillBookInputSetOperationCompleted);
            }
            this.InvokeAsync("DownloadBillBookInputSet", new object[] {
                        branchId,
                        lastModifiedDt,
                        serverDate}, this.DownloadBillBookInputSetOperationCompleted, userState);
        }
        
        private void OnDownloadBillBookInputSetOperationCompleted(object arg) {
            if ((this.DownloadBillBookInputSetCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DownloadBillBookInputSetCompleted(this, new DownloadBillBookInputSetCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("AuthenInfoValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/DownloadAgencyCommission", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public CompressData DownloadAgencyCommission(string branchId, System.DateTime lastModifiedDt, System.DateTime serverDate) {
            object[] results = this.Invoke("DownloadAgencyCommission", new object[] {
                        branchId,
                        lastModifiedDt,
                        serverDate});
            return ((CompressData)(results[0]));
        }
        
        /// <remarks/>
        public void DownloadAgencyCommissionAsync(string branchId, System.DateTime lastModifiedDt, System.DateTime serverDate) {
            this.DownloadAgencyCommissionAsync(branchId, lastModifiedDt, serverDate, null);
        }
        
        /// <remarks/>
        public void DownloadAgencyCommissionAsync(string branchId, System.DateTime lastModifiedDt, System.DateTime serverDate, object userState) {
            if ((this.DownloadAgencyCommissionOperationCompleted == null)) {
                this.DownloadAgencyCommissionOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDownloadAgencyCommissionOperationCompleted);
            }
            this.InvokeAsync("DownloadAgencyCommission", new object[] {
                        branchId,
                        lastModifiedDt,
                        serverDate}, this.DownloadAgencyCommissionOperationCompleted, userState);
        }
        
        private void OnDownloadAgencyCommissionOperationCompleted(object arg) {
            if ((this.DownloadAgencyCommissionCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DownloadAgencyCommissionCompleted(this, new DownloadAgencyCommissionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("AuthenInfoValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/DownloadRTAgencyContractMru", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public CompressData DownloadRTAgencyContractMru(string branchId, System.DateTime lastModifiedDt, System.DateTime serverDate) {
            object[] results = this.Invoke("DownloadRTAgencyContractMru", new object[] {
                        branchId,
                        lastModifiedDt,
                        serverDate});
            return ((CompressData)(results[0]));
        }
        
        /// <remarks/>
        public void DownloadRTAgencyContractMruAsync(string branchId, System.DateTime lastModifiedDt, System.DateTime serverDate) {
            this.DownloadRTAgencyContractMruAsync(branchId, lastModifiedDt, serverDate, null);
        }
        
        /// <remarks/>
        public void DownloadRTAgencyContractMruAsync(string branchId, System.DateTime lastModifiedDt, System.DateTime serverDate, object userState) {
            if ((this.DownloadRTAgencyContractMruOperationCompleted == null)) {
                this.DownloadRTAgencyContractMruOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDownloadRTAgencyContractMruOperationCompleted);
            }
            this.InvokeAsync("DownloadRTAgencyContractMru", new object[] {
                        branchId,
                        lastModifiedDt,
                        serverDate}, this.DownloadRTAgencyContractMruOperationCompleted, userState);
        }
        
        private void OnDownloadRTAgencyContractMruOperationCompleted(object arg) {
            if ((this.DownloadRTAgencyContractMruCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DownloadRTAgencyContractMruCompleted(this, new DownloadRTAgencyContractMruCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("AuthenInfoValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/DownloadRTAgencyCommissionBillBook", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public CompressData DownloadRTAgencyCommissionBillBook(string branchId, System.DateTime lastModifiedDt, System.DateTime serverDate) {
            object[] results = this.Invoke("DownloadRTAgencyCommissionBillBook", new object[] {
                        branchId,
                        lastModifiedDt,
                        serverDate});
            return ((CompressData)(results[0]));
        }
        
        /// <remarks/>
        public void DownloadRTAgencyCommissionBillBookAsync(string branchId, System.DateTime lastModifiedDt, System.DateTime serverDate) {
            this.DownloadRTAgencyCommissionBillBookAsync(branchId, lastModifiedDt, serverDate, null);
        }
        
        /// <remarks/>
        public void DownloadRTAgencyCommissionBillBookAsync(string branchId, System.DateTime lastModifiedDt, System.DateTime serverDate, object userState) {
            if ((this.DownloadRTAgencyCommissionBillBookOperationCompleted == null)) {
                this.DownloadRTAgencyCommissionBillBookOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDownloadRTAgencyCommissionBillBookOperationCompleted);
            }
            this.InvokeAsync("DownloadRTAgencyCommissionBillBook", new object[] {
                        branchId,
                        lastModifiedDt,
                        serverDate}, this.DownloadRTAgencyCommissionBillBookOperationCompleted, userState);
        }
        
        private void OnDownloadRTAgencyCommissionBillBookOperationCompleted(object arg) {
            if ((this.DownloadRTAgencyCommissionBillBookCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DownloadRTAgencyCommissionBillBookCompleted(this, new DownloadRTAgencyCommissionBillBookCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    public delegate void DownloadBillBookCompletedEventHandler(object sender, DownloadBillBookCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.4927")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DownloadBillBookCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal DownloadBillBookCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void DownloadBillStatusInfoCompletedEventHandler(object sender, DownloadBillStatusInfoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.4927")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DownloadBillStatusInfoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal DownloadBillStatusInfoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void DownloadBillBookDetailCompletedEventHandler(object sender, DownloadBillBookDetailCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.4927")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DownloadBillBookDetailCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal DownloadBillBookDetailCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void DownloadBillBookInputItemCompletedEventHandler(object sender, DownloadBillBookInputItemCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.4927")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DownloadBillBookInputItemCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal DownloadBillBookInputItemCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void DownloadBillBookInputSetCompletedEventHandler(object sender, DownloadBillBookInputSetCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.4927")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DownloadBillBookInputSetCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal DownloadBillBookInputSetCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void DownloadAgencyCommissionCompletedEventHandler(object sender, DownloadAgencyCommissionCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.4927")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DownloadAgencyCommissionCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal DownloadAgencyCommissionCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void DownloadRTAgencyContractMruCompletedEventHandler(object sender, DownloadRTAgencyContractMruCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.4927")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DownloadRTAgencyContractMruCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal DownloadRTAgencyContractMruCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void DownloadRTAgencyCommissionBillBookCompletedEventHandler(object sender, DownloadRTAgencyCommissionBillBookCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.4927")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DownloadRTAgencyCommissionBillBookCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal DownloadRTAgencyCommissionBillBookCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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