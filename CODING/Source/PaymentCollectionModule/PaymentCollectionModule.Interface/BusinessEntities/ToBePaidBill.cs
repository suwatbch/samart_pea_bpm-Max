using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using System.Xml.Serialization;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract, Serializable]
    public class ToBePaidBill: Bill
    {
        private bool _isChecked;

        [DataMember(Order = 1)]
        public bool IsChecked
        {
            get { return _isChecked; }
            set { _isChecked = value; }
        }

        public ToBePaidBill() { }

        public ToBePaidBill(Bill bill)
        {
            this.ItemId = bill.ItemId;
            this.InvoiceNo = bill.InvoiceNo;
            this.PaymentId = bill.PaymentId;
            this.PaymentMethodId = bill.PaymentMethodId;
            this.ARPmId = bill.ARPmId;
            this.CustomerId = bill.CustomerId;
            this.BranchId = bill.BranchId;
            this.TechBranchName = bill.TechBranchName;
            this.CommBranchId = bill.CommBranchId;
            this.CommBranchName = bill.CommBranchName;
            this.Name = bill.Name;
            this.Address = bill.Address;
            this.ContractTypeId = bill.ContractTypeId;
            this.DebtId = bill.DebtId;
            this.DebtType = bill.DebtType;
            this.Description = bill.Description;
            this.GAmount = bill.GAmount;
            this.ToPayGAmount = bill.ToPayGAmount;
            this.BookCreateDt = bill.BookCreateDt;
            this.BookTotalVatAmount = bill.BookTotalVatAmount;
            this.BookTotalGAmount = bill.BookTotalGAmount;
            this.BookPaidGAmount = bill.BookPaidGAmount;
            this.BookAdvanceAmount = bill.BookAdvanceAmount;
            this.BookTotalBill = bill.BookTotalBill;
            this.BookTotalBillCollected = bill.BookTotalBillCollected;
            this.SecurityDeposit = bill.SecurityDeposit;
            this.Period = bill.Period;
            this.DueDate = bill.DueDate;
            this.DeferralDate = bill.DeferralDate;
            this.Qty = bill.Qty;
            this.ToPayQty = bill.ToPayQty;
            this.UnitTypeName = bill.UnitTypeName;
            this.TaxCode = bill.TaxCode;
            this.TaxRate = bill.TaxRate;
            this.VatAmount = bill.VatAmount;
            this.ToPayVatAmount = bill.ToPayVatAmount;
            this.BillBookId = bill.BillBookId;
            this.GroupInvoiceId = bill.GroupInvoiceId;
            this.ControllerId = bill.ControllerId;
            this.ControllerId = bill.ControllerId;
            this.DataState = bill.DataState;

            this.MeterId = bill.MeterId;
            this.RateTypeId = bill.RateTypeId;
            this.MeterReadDate = bill.MeterReadDate;
            this.PreviousUnit = bill.PreviousUnit;
            this.LastUnit = bill.LastUnit;
            this.BaseAmount = bill.BaseAmount;
            this.FtUnitPrice = bill.FtUnitPrice;
            this.FtAmount = bill.FtAmount;
            this.AmountExVat = bill.AmountExVat;
            this.FullBaseAmount = bill.FullBaseAmount;
            this.FullFtAmount = bill.FullFtAmount;
            this.FullQty = bill.FullQty;
            this.FullAmount = bill.FullAmount;
            this.FullVatAmount = bill.FullVatAmount;
            this.FullGAmount = bill.FullGAmount;

            this.DisConnectDate = bill.DisConnectDate;
            this.InstallmentFlag = bill.InstallmentFlag;
            this.LastInstallmentFlag = bill.LastInstallmentFlag;
            this.SpotBillFlag = bill.SpotBillFlag;
            this.CancelFlag = bill.CancelFlag;
            this.ModifiedFlag = bill.ModifiedFlag;

            this.UiRefId = bill.UiRefId;

            this.IsChecked = true;
        }

        public Bill ToBill()
        {
            Bill bill = new Bill();
            bill.ItemId = this.ItemId;
            bill.InvoiceNo = this.InvoiceNo;
            bill.PaymentId = this.PaymentId;
            bill.PaymentMethodId = this.PaymentMethodId;
            bill.ARPmId = this.ARPmId;
            bill.CustomerId = this.CustomerId;
            bill.BranchId = this.BranchId;
            bill.TechBranchName = this.TechBranchName;
            bill.CommBranchId = this.CommBranchId;
            bill.CommBranchName = this.CommBranchName;
            bill.Name = this.Name;
            bill.Address = this.Address;
            bill.ContractTypeId = this.ContractTypeId;
            bill.DebtId = this.DebtId;
            bill.DebtType = this.DebtType;
            bill.Description = this.Description;
            bill.GAmount = this.GAmount;
            bill.BookCreateDt = this.BookCreateDt;
            bill.BookTotalVatAmount = this.BookTotalVatAmount;
            bill.BookTotalGAmount = this.BookTotalGAmount;
            bill.BookPaidGAmount = this.BookPaidGAmount;
            bill.BookAdvanceAmount = this.BookAdvanceAmount;
            bill.BookTotalBill = this.BookTotalBill;
            bill.BookTotalBillCollected = this.BookTotalBillCollected;
            bill.SecurityDeposit = this.SecurityDeposit;
            bill.Period = this.Period;
            bill.DueDate = this.DueDate;
            bill.DeferralDate = this.DeferralDate;
            bill.Qty = this.Qty;
            bill.UnitTypeName = this.UnitTypeName;
            bill.TaxCode = this.TaxCode;
            bill.TaxRate = this.TaxRate;
            bill.VatAmount = this.VatAmount;
            bill.BillBookId = this.BillBookId;
            bill.GroupInvoiceId = this.GroupInvoiceId;
            bill.ControllerId = this.ControllerId;
            bill.ControllerId = this.ControllerId;
            bill.DataState = this.DataState;

            bill.MeterId = this.MeterId;
            bill.RateTypeId = this.RateTypeId;
            bill.MeterReadDate = this.MeterReadDate;
            bill.PreviousUnit = this.PreviousUnit;
            bill.LastUnit = this.LastUnit;
            bill.BaseAmount = this.BaseAmount;
            bill.FtUnitPrice = this.FtUnitPrice;
            bill.FtAmount = this.FtAmount;
            bill.AmountExVat = this.AmountExVat;
            bill.FullBaseAmount = this.FullBaseAmount;
            bill.FullFtAmount = this.FullFtAmount;
            bill.FullQty = this.FullQty;
            bill.FullAmount = this.FullAmount;
            bill.FullVatAmount = this.FullVatAmount;
            bill.FullGAmount = this.FullGAmount;

            bill.DisConnectDate = this.DisConnectDate;
            bill.InstallmentFlag = this.InstallmentFlag;
            bill.LastInstallmentFlag = this.LastInstallmentFlag;
            bill.SpotBillFlag = this.SpotBillFlag;
            bill.CancelFlag = this.CancelFlag;
            bill.ModifiedFlag = this.ModifiedFlag;

            bill.UiRefId = this.UiRefId;

            return bill;
        }
    }
}
