﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.4952
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 2.0.50727.4952.
// 
#pragma warning disable 1591

namespace PEA.BPM.Architecture.ArchitectureBS.AuthenticationWS {
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.4927")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="AuthenticationWebServiceSoap", Namespace="http://tempuri.org/")]
    public partial class AuthenticationWebService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback CheckLogInDoubleOperationCompleted;
        
        private System.Threading.SendOrPostCallback UpdateCurIPReqFlagOperationCompleted;
        
        private System.Threading.SendOrPostCallback CheckTokenOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetTokenOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public AuthenticationWebService() {
            this.Url = global::PEA.BPM.Architecture.ArchitectureBS.Properties.Settings.Default.Architecture_ArchitectureBS_AuthenticationWS_AuthenticationWebService;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
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
        public event CheckLogInDoubleCompletedEventHandler CheckLogInDoubleCompleted;
        
        /// <remarks/>
        public event UpdateCurIPReqFlagCompletedEventHandler UpdateCurIPReqFlagCompleted;
        
        /// <remarks/>
        public event CheckTokenCompletedEventHandler CheckTokenCompleted;
        
        /// <remarks/>
        public event GetTokenCompletedEventHandler GetTokenCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/CheckLogInDouble", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string CheckLogInDouble(string userid, string terminalip, int latency, int retrycount) {
            object[] results = this.Invoke("CheckLogInDouble", new object[] {
                        userid,
                        terminalip,
                        latency,
                        retrycount});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void CheckLogInDoubleAsync(string userid, string terminalip, int latency, int retrycount) {
            this.CheckLogInDoubleAsync(userid, terminalip, latency, retrycount, null);
        }
        
        /// <remarks/>
        public void CheckLogInDoubleAsync(string userid, string terminalip, int latency, int retrycount, object userState) {
            if ((this.CheckLogInDoubleOperationCompleted == null)) {
                this.CheckLogInDoubleOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCheckLogInDoubleOperationCompleted);
            }
            this.InvokeAsync("CheckLogInDouble", new object[] {
                        userid,
                        terminalip,
                        latency,
                        retrycount}, this.CheckLogInDoubleOperationCompleted, userState);
        }
        
        private void OnCheckLogInDoubleOperationCompleted(object arg) {
            if ((this.CheckLogInDoubleCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CheckLogInDoubleCompleted(this, new CheckLogInDoubleCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/UpdateCurIPReqFlag", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string UpdateCurIPReqFlag(string userid, string terminalip, string reqflag) {
            object[] results = this.Invoke("UpdateCurIPReqFlag", new object[] {
                        userid,
                        terminalip,
                        reqflag});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void UpdateCurIPReqFlagAsync(string userid, string terminalip, string reqflag) {
            this.UpdateCurIPReqFlagAsync(userid, terminalip, reqflag, null);
        }
        
        /// <remarks/>
        public void UpdateCurIPReqFlagAsync(string userid, string terminalip, string reqflag, object userState) {
            if ((this.UpdateCurIPReqFlagOperationCompleted == null)) {
                this.UpdateCurIPReqFlagOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUpdateCurIPReqFlagOperationCompleted);
            }
            this.InvokeAsync("UpdateCurIPReqFlag", new object[] {
                        userid,
                        terminalip,
                        reqflag}, this.UpdateCurIPReqFlagOperationCompleted, userState);
        }
        
        private void OnUpdateCurIPReqFlagOperationCompleted(object arg) {
            if ((this.UpdateCurIPReqFlagCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UpdateCurIPReqFlagCompleted(this, new UpdateCurIPReqFlagCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/CheckToken", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string CheckToken(string userid, string token) {
            object[] results = this.Invoke("CheckToken", new object[] {
                        userid,
                        token});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void CheckTokenAsync(string userid, string token) {
            this.CheckTokenAsync(userid, token, null);
        }
        
        /// <remarks/>
        public void CheckTokenAsync(string userid, string token, object userState) {
            if ((this.CheckTokenOperationCompleted == null)) {
                this.CheckTokenOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCheckTokenOperationCompleted);
            }
            this.InvokeAsync("CheckToken", new object[] {
                        userid,
                        token}, this.CheckTokenOperationCompleted, userState);
        }
        
        private void OnCheckTokenOperationCompleted(object arg) {
            if ((this.CheckTokenCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CheckTokenCompleted(this, new CheckTokenCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetToken", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetToken(string userid, string hashpwd) {
            object[] results = this.Invoke("GetToken", new object[] {
                        userid,
                        hashpwd});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetTokenAsync(string userid, string hashpwd) {
            this.GetTokenAsync(userid, hashpwd, null);
        }
        
        /// <remarks/>
        public void GetTokenAsync(string userid, string hashpwd, object userState) {
            if ((this.GetTokenOperationCompleted == null)) {
                this.GetTokenOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetTokenOperationCompleted);
            }
            this.InvokeAsync("GetToken", new object[] {
                        userid,
                        hashpwd}, this.GetTokenOperationCompleted, userState);
        }
        
        private void OnGetTokenOperationCompleted(object arg) {
            if ((this.GetTokenCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetTokenCompleted(this, new GetTokenCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.4927")]
    public delegate void CheckLogInDoubleCompletedEventHandler(object sender, CheckLogInDoubleCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.4927")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CheckLogInDoubleCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal CheckLogInDoubleCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.4927")]
    public delegate void UpdateCurIPReqFlagCompletedEventHandler(object sender, UpdateCurIPReqFlagCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.4927")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UpdateCurIPReqFlagCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal UpdateCurIPReqFlagCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.4927")]
    public delegate void CheckTokenCompletedEventHandler(object sender, CheckTokenCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.4927")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CheckTokenCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal CheckTokenCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.4927")]
    public delegate void GetTokenCompletedEventHandler(object sender, GetTokenCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.4927")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetTokenCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetTokenCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591