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

namespace AdministrativeTool.OfflineErrorLogWS {
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;
    using System.Data;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.4927")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="OfflineErrorLogWebServiceSoap", Namespace="http://tempuri.org/")]
    public partial class OfflineErrorLogWebService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback GetOfflineErrorLogDisplayOperationCompleted;
        
        private System.Threading.SendOrPostCallback UpdateLogStatusOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public OfflineErrorLogWebService() {
            this.Url = global::AdministrativeTool.Properties.Settings.Default.AdministrativeTool_OfflineErrorLogWS_OfflineErrorLogWebService;
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
        public event GetOfflineErrorLogDisplayCompletedEventHandler GetOfflineErrorLogDisplayCompleted;
        
        /// <remarks/>
        public event UpdateLogStatusCompletedEventHandler UpdateLogStatusCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetOfflineErrorLogDisplay", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataTable GetOfflineErrorLogDisplay(System.DateTime datetime, string active) {
            object[] results = this.Invoke("GetOfflineErrorLogDisplay", new object[] {
                        datetime,
                        active});
            return ((System.Data.DataTable)(results[0]));
        }
        
        /// <remarks/>
        public void GetOfflineErrorLogDisplayAsync(System.DateTime datetime, string active) {
            this.GetOfflineErrorLogDisplayAsync(datetime, active, null);
        }
        
        /// <remarks/>
        public void GetOfflineErrorLogDisplayAsync(System.DateTime datetime, string active, object userState) {
            if ((this.GetOfflineErrorLogDisplayOperationCompleted == null)) {
                this.GetOfflineErrorLogDisplayOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetOfflineErrorLogDisplayOperationCompleted);
            }
            this.InvokeAsync("GetOfflineErrorLogDisplay", new object[] {
                        datetime,
                        active}, this.GetOfflineErrorLogDisplayOperationCompleted, userState);
        }
        
        private void OnGetOfflineErrorLogDisplayOperationCompleted(object arg) {
            if ((this.GetOfflineErrorLogDisplayCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetOfflineErrorLogDisplayCompleted(this, new GetOfflineErrorLogDisplayCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/UpdateLogStatus", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void UpdateLogStatus(string fileName, string status) {
            this.Invoke("UpdateLogStatus", new object[] {
                        fileName,
                        status});
        }
        
        /// <remarks/>
        public void UpdateLogStatusAsync(string fileName, string status) {
            this.UpdateLogStatusAsync(fileName, status, null);
        }
        
        /// <remarks/>
        public void UpdateLogStatusAsync(string fileName, string status, object userState) {
            if ((this.UpdateLogStatusOperationCompleted == null)) {
                this.UpdateLogStatusOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUpdateLogStatusOperationCompleted);
            }
            this.InvokeAsync("UpdateLogStatus", new object[] {
                        fileName,
                        status}, this.UpdateLogStatusOperationCompleted, userState);
        }
        
        private void OnUpdateLogStatusOperationCompleted(object arg) {
            if ((this.UpdateLogStatusCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UpdateLogStatusCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    public delegate void GetOfflineErrorLogDisplayCompletedEventHandler(object sender, GetOfflineErrorLogDisplayCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.4927")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetOfflineErrorLogDisplayCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetOfflineErrorLogDisplayCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataTable Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataTable)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.4927")]
    public delegate void UpdateLogStatusCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
}

#pragma warning restore 1591