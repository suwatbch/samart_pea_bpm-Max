using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml.Serialization;


namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class ToBePaidInvoice: Invoice
    {
        private Invoice _originalInvoice;

        private bool _isChecked;

        [DataMember( Order = 1)]
        public bool IsChecked
        {
            get { return _isChecked; }
            set { _isChecked = value; }
        }

        private List<ToBePaidBill> _toBePaidBill;

        [DataMember(Order = 2)]
        public List<ToBePaidBill> ToBePaidBill
        {
            get { return _toBePaidBill; }
            set { _toBePaidBill = value; }
        }

        public ToBePaidInvoice() { }
        public ToBePaidInvoice(Invoice invoice)
        {
            _originalInvoice = invoice;

            this.InvoiceNo = invoice.InvoiceNo;
            this.InvoiceDate = invoice.InvoiceDate;
            this.BranchId = invoice.BranchId;
            this.TechBranchName = invoice.TechBranchName;
            this.CommBranchId = invoice.CommBranchId;
            this.CommBranchName = invoice.CommBranchName;
            this.MruId = invoice.MruId;
            this.BpId = invoice.BpId;
            this.CaId = invoice.CaId;
            this.Name = invoice.Name;
            this.PayByName = invoice.PayByName;
            this.Address = invoice.Address;
            this.PmId = invoice.PmId;
            this.GroupInvoiceReceiptType = invoice.GroupInvoiceReceiptType;
            this.DueDate = invoice.DueDate;
            this.Qty = invoice.Qty;
            this.AmountExVat = invoice.AmountExVat;
            this.VatAmount = invoice.VatAmount;
            this.GAmount = invoice.GAmount;
            this.Amount = invoice.Amount;
            this.PaidQty = invoice.PaidQty;
            this.PaidVatAmount = invoice.PaidVatAmount;
            this.PaidGAmount = invoice.PaidGAmount;
            this.ToPayQty = invoice.ToPayQty;
            this.ToPayVatAmount = invoice.ToPayVatAmount;
            this.ToPayGAmount = invoice.ToPayGAmount;
            this.CaDoc = invoice.CaDoc;
            this.OriginalInvoiceNo = invoice.OriginalInvoiceNo;
            this.InstallmentPeriod = invoice.InstallmentPeriod;
            this.InstallmentTotalPeriod = invoice.InstallmentTotalPeriod;
            this.UiRefId = invoice.UiRefId;
            this.ControllerId = invoice.ControllerId;
            this.ControllerName = invoice.ControllerName;
            this.Bills = invoice.Bills;
            this.PaymentMethods = invoice.PaymentMethods;
            this.DataState = invoice.DataState;
            this.NetworkMode = invoice.NetworkMode;
            //TODO: INSTALLMENT CASE
            //this.IsInvalidInstment = invoice.IsInvalidInstment;
            this.CaTaxId = invoice.CaTaxId;
            this.CaTaxBranch = invoice.CaTaxBranch;

            this.EnergyAmount = invoice.EnergyAmount;
            
            this.IsChecked = true;

            // DCR 67-020 Rev.1 Flag create invoince by user.
            this.InvoiceFromLoal = invoice.InvoiceFromLoal;
        }

        public Invoice ToInvoice()
        {
            _originalInvoice.Name = this.Name;
            _originalInvoice.Address = this.Address;

            // DCR 67-020 Edit payment for receipt.
            _originalInvoice.PayByName = this.PayByName;

            //Tax13
            _originalInvoice.CaTaxId = this.CaTaxId;
            _originalInvoice.CaTaxBranch = this.CaTaxBranch;

            _originalInvoice.UiRefId = this.UiRefId;
            _originalInvoice.ToPayQty = this.ToPayQty;
            _originalInvoice.ToPayVatAmount = this.ToPayVatAmount;
            _originalInvoice.ToPayGAmount = this.ToPayGAmount;
            //TODO: INSTALLMENT CASE
            //_originalInvoice.IsInvalidInstment = this.IsInvalidInstment;

            return _originalInvoice;
        }
    }
}
