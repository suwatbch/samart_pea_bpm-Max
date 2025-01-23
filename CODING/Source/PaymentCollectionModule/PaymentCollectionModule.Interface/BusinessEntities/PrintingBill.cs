using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract, Serializable]
    public class PrintingBill : Bill
    {
        private PrintingConstraint _printingConstaint;

        public PrintingBill()
        {
        }

        public static PrintingBill CreateInstance(Bill bill, PrintingConstraint printingConstraint)
        {
            PrintingBill pr = new PrintingBill();
            pr.PaymentId = bill.PaymentId;
            pr.ARPmId = bill.ARPmId;
            pr.CustomerId = bill.CustomerId;
            pr.Name = bill.Name;
            pr.Address = bill.Address;
            pr.ContractTypeId = bill.ContractTypeId;
            pr.DebtId = bill.DebtId;
            pr.DebtType = bill.DebtType;
            pr.Description = bill.Description;
            pr.GAmount = bill.GAmount;
            pr.Period = bill.Period;
            pr.Qty = bill.Qty;
            pr.UnitTypeName = bill.UnitTypeName;
            pr.TaxRate = bill.TaxRate;
            pr.VatAmount = bill.VatAmount;
            pr.ControllerId = bill.ControllerId;
            pr.DataState = bill.DataState;

            // for electric item
            pr.MeterId = bill.MeterId;
            pr.RateTypeId = bill.RateTypeId;
            pr.MeterReadDate = bill.MeterReadDate;
            pr.PreviousUnit = bill.PreviousUnit;
            pr.LastUnit = bill.LastUnit;
            pr.BaseAmount = bill.BaseAmount;
            pr.FtUnitPrice = bill.FtUnitPrice;
            pr.FtAmount = bill.FtAmount;
            pr.AmountExVat = bill.AmountExVat;

            pr.BillBookId = bill.BillBookId;
            pr.BookCreateDt = bill.BookCreateDt;
            pr.BookAdvanceAmount = bill.BookAdvanceAmount;
            pr.BookTotalBill = bill.BookTotalBill;
            pr.BookTotalBillCollected = bill.BookTotalBillCollected;
            pr.BookTotalGAmount = bill.BookTotalGAmount;
            pr.PrintingConstaint = printingConstraint;
            //pr.OriginalBill = bill;

            pr.InstallmentFlag = bill.InstallmentFlag;
            pr.LastInstallmentFlag = bill.LastInstallmentFlag;

            pr.UiRefId = bill.UiRefId;

            return pr;
        }


        [DataMember(Order=1)]
        public PrintingConstraint PrintingConstaint
        {
            get { return _printingConstaint; }
            set { _printingConstaint = value; }
        }
    }
}
