using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
//using PEA.BPM.PaymentCollectionModule.BS;
//using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;

namespace PEA.BPM.BPMGatewayService.BS
{
    public class BillingWrapper
    {
        //private BillingBS _bs;

        public BillingWrapper()
        {
            //_bs = new BillingBS();
        }

        public List<InvoiceInfo> SearchInvoiceByCustomerId(string contractAccount)
        {
            //CustomerSearchParam param = new CustomerSearchParam();
            //param.CaId = contractAccount;
            //param.IsSearByBP = false;
            //param.ToPayDate = DateTime.Now.Date;
            //List<Invoice> invoices = _bs.SearchInvoiceByCustomerId(param);

            //List<InvoiceInfo> invoiceList = ToShortInvoiceList(invoices);
            List<InvoiceInfo> invoiceList = new List<InvoiceInfo>();

            if (contractAccount == "020004160124" || contractAccount == "20004160124")
            {
                InvoiceInfo info = new InvoiceInfo();
                info.ContractAccountAddress = "เลขที่ 147 ม.4 ต.บางขุนไทร อ.บ้านแหลม จ.เพชรบุรี 76110";
                info.ContractAccountId = "020004160124";
                info.ContractAccountName = "ห.จ.ก.เพชรบุรีโชคอนันต์";
                info.InvoicePeriod = "255504";
                info.PaymentDueDate = "08/05/2555";
                info.ToPayTotalAmount = Convert.ToDecimal(6950.54);
                info.DebtTypeName = "ค่าไฟฟ้า";
                invoiceList.Add(info);
            }

            return invoiceList;
        }

        public Credential Login(string userId, string password)
        {
            Credential credential = new Credential();

            if (userId == "QUSER01" && password == "0987654321")
            {
                credential.Status = "VALID";
                credential.Token = Guid.NewGuid().ToString();
                Session.User.Token.Id = credential.Token;
                Session.User.Id = userId;
            }
            else
            {
                credential.Status = "INVALID";
            }

            return credential;
        }

        //private List<InvoiceInfo> ToShortInvoiceList(List<Invoice> invoices)
        //{
        //    List<InvoiceInfo> invoiceList = new List<InvoiceInfo>();

        //    var grouping = from i in invoices
        //                   group i by new { i.CaId, i.Name, i.Address, i.DebtType, i.Period, i.DisplayDueDate, i.ToPayTotalAmount } into ig
        //                   select new
        //                   {
        //                       ig.Key.CaId,
        //                       ig.Key.Name,
        //                       ig.Key.Address,
        //                       ig.Key.DebtType,
        //                       ig.Key.Period,
        //                       ig.Key.DisplayDueDate,
        //                       ig.Key.ToPayTotalAmount
        //                   };

        //    foreach (var i in grouping)
        //    {
        //        InvoiceInfo info = new InvoiceInfo();
        //        info.ContractAccountId = i.CaId;
        //        info.ContractAccountName = i.Name;
        //        info.ContractAccountAddress = i.Address;
        //        info.DebtTypeName = i.DebtType;
        //        info.InvoicePeriod = i.Period;
        //        info.PaymentDueDate = i.DisplayDueDate;
        //        info.ToPayTotalAmount = i.ToPayTotalAmount;
        //        invoiceList.Add(info);
        //    }

        //    return invoiceList;
        //}
    }
}
