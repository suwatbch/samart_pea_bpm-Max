using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract, Serializable]
    [KnownType(typeof(Invoice))]
    public class PrintingInvoice : Invoice
    {
        private Invoice _originalInvoice;

        [DataMember(Order=1)]
        public Invoice OriginalInvoice
        {
            get { return _originalInvoice; }
            set { _originalInvoice = value; }
        }

        private PrintingConstraint _printingConstaint;

        [DataMember(Order=2)]
        public PrintingConstraint PrintingConstaint
        {
            get { return _printingConstaint; }
            set { _printingConstaint = value; }
        }

        public PrintingInvoice() { }

        public PrintingInvoice(Invoice invoice, PrintingConstraint printingConstraint)
        {
            this.BranchId = invoice.BranchId;
            this.TechBranchName = invoice.TechBranchName;
            this.CommBranchId = invoice.CommBranchId;
            this.CommBranchName = invoice.CommBranchName;
            this.MruId = invoice.MruId;
            this.CaId = invoice.CaId;
            this.Name = invoice.Name;
            this.PayByName = invoice.PayByName;

            this.CaTaxId = invoice.CaTaxId;
            this.CaTaxBranch = invoice.CaTaxBranch;

            this.Address = invoice.Address;
            this.GroupInvoiceReceiptType = invoice.GroupInvoiceReceiptType;
            this.PaymentId = invoice.PaymentId;
            this.ARPmId = invoice.ARPmId;
            
            this.Bills = invoice.Bills;
            this.PaymentMethods = invoice.PaymentMethods;
            this.PartialPayment = invoice.PartialPayment;
            this.ControllerId = invoice.ControllerId;
            this.ControllerName = invoice.ControllerName;

            this.CaDoc = invoice.CaDoc;
            this.InvoiceNo = invoice.InvoiceNo;
            this.InvoiceDate = invoice.InvoiceDate;

            this.Qty = invoice.Qty;
            this.AmountExVat = invoice.AmountExVat;
            this.VatAmount = invoice.VatAmount;
            this.GAmount = invoice.GAmount;

            this.PaidQty = invoice.PaidQty;
            this.PaidVatAmount = invoice.PaidVatAmount;
            this.PaidGAmount = invoice.PaidGAmount;

            this.ToPayQty = invoice.ToPayQty;
            this.ToPayVatAmount = invoice.ToPayVatAmount;
            this.ToPayGAmount = invoice.ToPayGAmount;
            this.ToPayAdjAmount = invoice.ToPayAdjAmount;

            this.OriginalInvoiceNo = invoice.OriginalInvoiceNo;
            this.OriginalInvoiceDt = invoice.OriginalInvoiceDt;
            this.SpotBillInvoiceNo = invoice.SpotBillInvoiceNo;
            this.InstallmentPeriod = invoice.InstallmentPeriod;
            this.InstallmentTotalPeriod = invoice.InstallmentTotalPeriod;

            this.PrintingConstaint = printingConstraint;
            this.OriginalInvoice = invoice;

            this.NetworkMode = invoice.NetworkMode;
            this.DataState = invoice.DataState;
            this.UiRefId = invoice.UiRefId;

            this.EnergyAmount = invoice.EnergyAmount;
            this.NotificationNo = invoice.NotificationNo;
        }
    }
}
