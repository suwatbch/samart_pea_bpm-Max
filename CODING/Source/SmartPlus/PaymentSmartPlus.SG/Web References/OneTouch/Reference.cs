﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.9151
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 2.0.50727.9151.
// 
#pragma warning disable 1591

namespace PaymentSmartPlus.SG.OneTouch {
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;
    using System.Data;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.9136")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="CSS_BPMSoap", Namespace="http://tempuri.org/")]
    public partial class CSS_BPM : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback SelectReqChargeOperationCompleted;
        
        private System.Threading.SendOrPostCallback UpdateReqChargeOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public CSS_BPM() {
            this.Url = global::PaymentSmartPlus.SG.Properties.Settings.Default.PaymentCollectionModule_SG_OneTouch_CSS_BPM;
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
        public event SelectReqChargeCompletedEventHandler SelectReqChargeCompleted;
        
        /// <remarks/>
        public event UpdateReqChargeCompletedEventHandler UpdateReqChargeCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SelectReqCharge", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public ReqCharge SelectReqCharge(string NotificationNo) {
            object[] results = this.Invoke("SelectReqCharge", new object[] {
                        NotificationNo});
            return ((ReqCharge)(results[0]));
        }
        
        /// <remarks/>
        public void SelectReqChargeAsync(string NotificationNo) {
            this.SelectReqChargeAsync(NotificationNo, null);
        }
        
        /// <remarks/>
        public void SelectReqChargeAsync(string NotificationNo, object userState) {
            if ((this.SelectReqChargeOperationCompleted == null)) {
                this.SelectReqChargeOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSelectReqChargeOperationCompleted);
            }
            this.InvokeAsync("SelectReqCharge", new object[] {
                        NotificationNo}, this.SelectReqChargeOperationCompleted, userState);
        }
        
        private void OnSelectReqChargeOperationCompleted(object arg) {
            if ((this.SelectReqChargeCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SelectReqChargeCompleted(this, new SelectReqChargeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/UpdateReqCharge", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public UpdateStatus UpdateReqCharge(string NotificationNo, string InvoiceNo, string DebtId, decimal Vat, decimal Amount, string ReceiptId) {
            object[] results = this.Invoke("UpdateReqCharge", new object[] {
                        NotificationNo,
                        InvoiceNo,
                        DebtId,
                        Vat,
                        Amount,
                        ReceiptId});
            return ((UpdateStatus)(results[0]));
        }
        
        /// <remarks/>
        public void UpdateReqChargeAsync(string NotificationNo, string InvoiceNo, string DebtId, decimal Vat, decimal Amount, string ReceiptId) {
            this.UpdateReqChargeAsync(NotificationNo, InvoiceNo, DebtId, Vat, Amount, ReceiptId, null);
        }
        
        /// <remarks/>
        public void UpdateReqChargeAsync(string NotificationNo, string InvoiceNo, string DebtId, decimal Vat, decimal Amount, string ReceiptId, object userState) {
            if ((this.UpdateReqChargeOperationCompleted == null)) {
                this.UpdateReqChargeOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUpdateReqChargeOperationCompleted);
            }
            this.InvokeAsync("UpdateReqCharge", new object[] {
                        NotificationNo,
                        InvoiceNo,
                        DebtId,
                        Vat,
                        Amount,
                        ReceiptId}, this.UpdateReqChargeOperationCompleted, userState);
        }
        
        private void OnUpdateReqChargeOperationCompleted(object arg) {
            if ((this.UpdateReqChargeCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UpdateReqChargeCompleted(this, new UpdateReqChargeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.9136")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class ReqCharge {
        
        private System.Data.DataTable reqChargeRecordField;
        
        /// <remarks/>
        public System.Data.DataTable ReqChargeRecord {
            get {
                return this.reqChargeRecordField;
            }
            set {
                this.reqChargeRecordField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.9136")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class UpdateStatus {
        
        private string returnField;
        
        /// <remarks/>
        public string Return {
            get {
                return this.returnField;
            }
            set {
                this.returnField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.9136")]
    public delegate void SelectReqChargeCompletedEventHandler(object sender, SelectReqChargeCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.9136")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SelectReqChargeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SelectReqChargeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public ReqCharge Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((ReqCharge)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.9136")]
    public delegate void UpdateReqChargeCompletedEventHandler(object sender, UpdateReqChargeCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.9136")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UpdateReqChargeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal UpdateReqChargeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public UpdateStatus Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((UpdateStatus)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591