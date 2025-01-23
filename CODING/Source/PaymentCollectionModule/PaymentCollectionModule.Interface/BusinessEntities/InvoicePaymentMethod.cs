using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.CommonUtilities;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using PEA.BPM.PaymentCollectionModule.Interface.Constants;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract, Serializable]
    public class InvoicePaymentMethod: IComparable<InvoicePaymentMethod>
    {
        private int _invoiceUiRefId;

        [DataMember(Order=1)]
        public int InvoiceUiRefId
        {
            get { return _invoiceUiRefId; }
            set { _invoiceUiRefId = value; }
        }

        public Invoice GetInvoice(List<Invoice> invoices)
        {            
            Invoice iv = invoices.Find(delegate(Invoice ivx)
                {
                    return ivx.UiRefId == _invoiceUiRefId;
                }
            );

            return iv;
        }

        private int _paymentMethodUiRefId;

        [DataMember(Order=2)]
        public int PaymentMethodUiRefId
        {
            get { return _paymentMethodUiRefId; }
            set { _paymentMethodUiRefId = value; }
        }

        public PaymentMethod GetPaymentMethod(List<PaymentMethod> paymentMethods)
        {
            PaymentMethod pm = paymentMethods.Find(delegate(PaymentMethod pmx)
                {
                    return pmx.UiRefId == _paymentMethodUiRefId;
                }
            );

            return pm;
        }

        private string _ptId;

        [DataMember(Order=3)]
        public string PtId
        {
            get { return _ptId; }
            set { _ptId = value; }
        }

        private decimal _amount;

        [DataMember(Order=4)]
        public decimal Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public InvoicePaymentMethod() { }

        public InvoicePaymentMethod(Invoice invoice, PaymentMethod paymentMethod, decimal amount)
        {
            _invoiceUiRefId = invoice.UiRefId;
            _paymentMethodUiRefId = paymentMethod.UiRefId;
            _amount = amount;
            _ptId = paymentMethod.PtId;

            if (invoice.PaymentMethods == null)
            {
                invoice.PaymentMethods = new List<InvoicePaymentMethod>();
            }

            invoice.PaymentMethods.Add(this);
            paymentMethod.ToPayInvoices.Add(this);
        }

        public InvoicePaymentMethod(InvoicePaymentMethod pm)
        {
            this.InvoiceUiRefId = pm.InvoiceUiRefId;
            this.PaymentMethodUiRefId = pm.PaymentMethodUiRefId;
            this.PtId = pm.PtId;
            this.Amount = pm.Amount;
        }

        public static void Create(Invoice invoice, PaymentMethod paymentMethod, decimal amount)
        {
            new InvoicePaymentMethod(invoice, paymentMethod, amount);
        }

        #region IComparable<InvoicePaymentMethod> Members

        public int CompareTo(InvoicePaymentMethod other)
        {
            int t1 = PaymentMethod.GetOrderPtNumber(_ptId);
            int t2 = PaymentMethod.GetOrderPtNumber(other.PtId);

            return t1.CompareTo(t2);
        }

        #endregion
    }
}
