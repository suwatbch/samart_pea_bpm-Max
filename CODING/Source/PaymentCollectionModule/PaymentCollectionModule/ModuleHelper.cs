using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;

namespace PEA.BPM.PaymentCollectionModule
{
    internal class ModuleHelper
    {
        public enum CheckType
        {
            CheckExistItem,
            CheckConflictSearch,
            CheckDuplicatGroupInvoice
        }

        public static string GetBranch(string branchId)
        {
            string convertChar = branchId.Substring(0, 3);
            switch (convertChar)
            {
                case "001":
                    return string.Format("{0}{1}", "A", branchId.Substring(3, 5));
                case "002":
                    return string.Format("{0}{1}", "B", branchId.Substring(3, 5));
                case "003":
                    return string.Format("{0}{1}", "C", branchId.Substring(3, 5));
                case "004":
                    return string.Format("{0}{1}", "D", branchId.Substring(3, 5));
                case "005":
                    return string.Format("{0}{1}", "E", branchId.Substring(3, 5));
                case "006":
                    return string.Format("{0}{1}", "F", branchId.Substring(3, 5));
                case "007":
                    return string.Format("{0}{1}", "G", branchId.Substring(3, 5));
                case "008":
                    return string.Format("{0}{1}", "H", branchId.Substring(3, 5));
                case "009":
                    return string.Format("{0}{1}", "I", branchId.Substring(3, 5));
                case "010":
                    return string.Format("{0}{1}", "J", branchId.Substring(3, 5));
                case "011":
                    return string.Format("{0}{1}", "K", branchId.Substring(3, 5));
                case "012":
                    return string.Format("{0}{1}", "L", branchId.Substring(3, 5));
                case "013":
                    return string.Format("{0}{1}", "M", branchId.Substring(3, 5));
                case "014":
                    return string.Format("{0}{1}", "N", branchId.Substring(3, 5));
                case "015":
                    return string.Format("{0}{1}", "O", branchId.Substring(3, 5));
                case "016":
                    return string.Format("{0}{1}", "P", branchId.Substring(3, 5));
                case "017":
                    return string.Format("{0}{1}", "Q", branchId.Substring(3, 5));
                case "018":
                    return string.Format("{0}{1}", "R", branchId.Substring(3, 5));
                case "019":
                    return string.Format("{0}{1}", "S", branchId.Substring(3, 5));
                case "020":
                    return string.Format("{0}{1}", "T", branchId.Substring(3, 5));
                case "021":
                    return string.Format("{0}{1}", "U", branchId.Substring(3, 5));
                case "022":
                    return string.Format("{0}{1}", "V", branchId.Substring(3, 5));
                case "023":
                    return string.Format("{0}{1}", "W", branchId.Substring(3, 5));
                case "024":
                    return string.Format("{0}{1}", "X", branchId.Substring(3, 5));
                case "025":
                    return string.Format("{0}{1}", "Y", branchId.Substring(3, 5));
                case "026":
                    return string.Format("{0}{1}", "Z", branchId.Substring(3, 5));
                default:
                    return branchId;
            }
        }

        internal static bool CheckDuplicateInvoiceItem(List<ToBePaidInvoice> toBePaidInvoices, List<Invoice> invoices)
        {
            foreach (Invoice inv in invoices)
            {
                foreach (ToBePaidInvoice toBePaidInv in toBePaidInvoices)
                {
                    if (toBePaidInv.InvoiceNo != null && toBePaidInv.InvoiceNo.Length > 0 && toBePaidInv.InvoiceNo == inv.InvoiceNo)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        internal static bool CheckDuplicateBillItemForBillBook(List<ToBePaidBill> toBePaidBills, List<Bill> bills)
        {
            foreach (Bill b in bills)
            {
                foreach (ToBePaidBill toBePaidB in toBePaidBills)
                {
                    if (toBePaidB.BillBookId != null && toBePaidB.CustomerId != null && toBePaidB.DebtId != null)
                        if (b.BillBookId != null && b.CustomerId != null && b.DebtId != null)
                            if (toBePaidB.BillBookId == b.BillBookId && toBePaidB.CustomerId == b.CustomerId && toBePaidB.DebtId == b.DebtId)
                                return false;
                }
            }

            return true;
        }

        internal static bool CheckSameCustomerForBillBook(List<ToBePaidBill> toBePaidBills, List<Bill> bills)
        {
            foreach (Bill b in bills)
            {
                foreach (ToBePaidBill toBePaidB in toBePaidBills)
                {
                    if (toBePaidB.BillBookId != null && toBePaidB.CustomerId != null && toBePaidB.DebtId != null)
                        if (b.BillBookId != null && b.CustomerId != null && b.DebtId != null)
                            if (toBePaidB.CustomerId != b.CustomerId)
                                return false;
                }
            }

            return true;
        }


        internal static bool CheckAddedItem(List<ToBePaidBill> toBePaidBills, List<Bill> bills, CheckType checkType)
        {
            //List<ToBePaidBill> toAddBills = new List<ToBePaidBill>();

            for (int iCount2 = 0; iCount2 < bills.Count; iCount2++)
            {
                Bill b2 = bills[iCount2];

                for (int iCount1 = 0; iCount1 < toBePaidBills.Count; iCount1++)
                {
                    Bill b1 = toBePaidBills[iCount1];

                    switch (checkType)
                    {
                        case ModuleHelper.CheckType.CheckExistItem:
                            if (CheckExistItem(b1, b2))
                            {
                                return true;
                            }
                            break;
                        //case ModuleHelper.CheckType.CheckConflictSearch:
                        //    if (CheckConflictSearch(b1, b2))
                        //    {
                        //        return true;
                        //    }
                        //    break;
                        //case ModuleHelper.CheckType.CheckDuplicatGroupInvoice:
                        //    if (CheckDuplicatGroupInvoice(b1, b2))
                        //    {
                        //        return true;
                        //    }
                        //    break;
                    }
                }
            }

            return false;
        }

        private static bool CheckExistItem(Bill b1, Bill b2)
        {
            bool result = false;
            if (b1.BillBookId != null && b1.BillBookId.Length > 0 && b1.BillBookId == b2.BillBookId)
            {
                result = true;
            }
            //if (b1.DebtId == b2.DebtId 
            //    && b1.DisConnectDate != null 
            //    && b2.DisConnectDate != null
            //    && b1.ItemId == null & b2.ItemId == null)
            //{
            //    if (b1.DisConnectDate.Value == b2.DisConnectDate.Value)
            //    {
            //        result = true;
            //    }
            //}

            return result;
        }

        private static bool CheckConflictSearch(Bill b1, Bill b2)
        {
            bool result = false;
            if (b1.BillBookId != null && b2.GroupInvoiceId != null)
            {
                result = true;
            }

            return result;
        }

        private static bool CheckDuplicatGroupInvoice(Bill b1, Bill b2)
        {
            bool result = false;
            if (b1.GroupInvoiceId != null && b2.GroupInvoiceId != null && b1.GroupInvoiceId != b2.GroupInvoiceId)
            {
                result = true;
            }

            return result;
        }

        internal static bool CheckAddedCancelReceiptItem(List<ToBeCancelledReceipt> inListReceipts, List<Receipt> toBeAddedReceipts)
        {
            foreach (Receipt ar in toBeAddedReceipts)
            {
                foreach (ToBeCancelledReceipt lr in inListReceipts)
                {
                    if (ar.ReceiptId == lr.ReceiptId)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        internal static bool CheckAddedReprintReceiptItem(List<ToBeReprintedReceipt> inListReceipts, List<Receipt> toBeAddedReceipts)
        {
            foreach (Receipt ar in toBeAddedReceipts)
            {
                foreach (ToBeReprintedReceipt lr in inListReceipts)
                {
                    if (ar.ReceiptId == lr.ReceiptId)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
