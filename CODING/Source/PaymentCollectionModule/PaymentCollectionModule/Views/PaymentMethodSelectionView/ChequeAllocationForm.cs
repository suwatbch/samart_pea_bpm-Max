using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using PEA.BPM.PaymentCollectionModule.Interface.Constants;

namespace PEA.BPM.PaymentCollectionModule.Views.PaymentMethodSelectionView
{
    public partial class ChequeAllocationForm : Form
    {
        public class _Invoice
        {  
            private string _description;
            public string Description
            {
                get { return _description; }
            }

            private decimal _amount;
            public decimal Amount
            {
                get { return _amount; }
            }

            private Invoice _originalInvoice;
            public Invoice OriginalInvoice
            {
                get { return _originalInvoice; }
            }

            private List<_InvoicePaymentMethod> _paymentMethods;
            public List<_InvoicePaymentMethod> PaymentMethods
            {
                get { return _paymentMethods; }
            }

            public _Invoice(Invoice invoice)
            {
                _originalInvoice = invoice;
                _description = string.Format("{0} - {1} {2} {3}", invoice.Name, invoice.DebtType, invoice.RealInvoiceNo, invoice.PeriodString).Trim();
                _amount = invoice.ToPayGAmount.Value;

                _paymentMethods = new List<_InvoicePaymentMethod>();
            }

            public decimal TotalPaidByPaymentMethodAmount
            {
                get
                {
                    decimal result = 0;
                    foreach (_InvoicePaymentMethod ivpm in _paymentMethods)
                    {
                        result += ivpm.Amount;
                    }

                    return result;
                }
            }

            /// <summary>
            /// จำนวนเงินที่เหลือหลังจากกำหนด PaymentMethod
            /// </summary>
            public decimal TotalRemainToPayAmount
            {
                get
                {
                    return _amount - TotalPaidByPaymentMethodAmount;
                }
            }
        }
        public class _PaymentMethod
        {
            private string _description;
            public string Description
            {
                get { return _description; }
            }

            private decimal _amount;
            public decimal Amount
            {
                get { return _amount; }
            }

            private PaymentMethod _originalPaymentMethod;
            public PaymentMethod OriginalPaymentMethod
            {
                get { return _originalPaymentMethod; }
            }

            private List<_InvoicePaymentMethod> _invoices;
            public List<_InvoicePaymentMethod> Invoices
            {
                get { return _invoices; }
            }

            public _PaymentMethod(PaymentMethod paymentMethod)
            {
                _originalPaymentMethod = paymentMethod;
                _description = string.Format("{0} - {1}", paymentMethod.PtName, paymentMethod.Description);
                _amount = paymentMethod.ToPayAmount.Value;

                _invoices = new List<_InvoicePaymentMethod>();
            }

            public decimal TotalPayInvoiceAmount
            {
                get
                {
                    decimal result = 0;
                    foreach (_InvoicePaymentMethod ivpm in _invoices)
                    {
                        result += ivpm.Amount;
                    }

                    return result;
                }
            }

            /// <summary>
            /// จำนวนเงินที่เหลือหลังจากกำหนดให้ Invoice
            /// </summary>
            public decimal TotalRemainAmount
            {
                get
                {
                    return _amount - TotalPayInvoiceAmount;
                }
            }
        }

        public class _InvoicePaymentMethod
        {
            private _Invoice _invoice;
            public _Invoice Invoice
            {
                get { return _invoice; }
            }

            private _PaymentMethod _paymentMethod;
            public _PaymentMethod PaymentMethod
            {
                get { return _paymentMethod; }
            }

            private decimal _amount;
            public decimal Amount
            {
                get { return _amount; }
            }

            public string InvoiceDescription
            {
                get { return _invoice.Description; }
            }

            public string PaymentMethodDescription
            {
                get { return _paymentMethod.Description; }
            }

            private _InvoicePaymentMethod(_Invoice invoice, _PaymentMethod paymentMethod, decimal amount)
            {
                _invoice = invoice;
                _paymentMethod = paymentMethod;
                _amount = amount;

                _invoice.PaymentMethods.Add(this);
                _paymentMethod.Invoices.Add(this);
            }

            public static void Create(_Invoice invoice, _PaymentMethod paymentMethod, decimal amount)
            {
                new _InvoicePaymentMethod(invoice, paymentMethod, amount);
            }
        }

        private bool _isDirty = false;
        private List<_Invoice> _invoices;
        private List<_PaymentMethod> _paymentMethods;
        
        private List<Invoice> _originalInvoices;
        private List<PaymentMethod> _originalPaymentMethods;

        public ChequeAllocationForm()
        {
            InitializeComponent();
            invoiceDataGridView.AutoGenerateColumns = false;
            paymentMethodDataGridView.AutoGenerateColumns = false;
        }
        
        public void SetInvoice(List<Invoice> invoices)
        {
            _originalInvoices = invoices;
            _invoices = new List<_Invoice>();
            foreach (Invoice iv in invoices)
            {
                _invoices.Add(new _Invoice(iv));
            }
        }

        public void SetPaymentMethod(List<PaymentMethod> paymentMethods)
        {
            _originalPaymentMethods = paymentMethods;
            _paymentMethods = new List<_PaymentMethod>();
            foreach (PaymentMethod pm in paymentMethods)
            {
                if (pm.PtId == CodeNames.PaymentType.Cheque.Id)
                {
                    _paymentMethods.Add(new _PaymentMethod(pm));
                }
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (_isDirty)
            {
                for(int i=0; i<_invoices.Count; i++)
                {
                    _Invoice iv = _invoices[i];
                    _originalInvoices.Remove(iv.OriginalInvoice);
                    _originalInvoices.Insert(i, iv.OriginalInvoice);
                }

                for (int i = 0; i < _paymentMethods.Count; i++)
                {
                    _PaymentMethod pm = _paymentMethods[i];
                    _originalPaymentMethods.Remove(pm.OriginalPaymentMethod);
                    _originalPaymentMethods.Insert(i, pm.OriginalPaymentMethod);
                }
            }

            this.DialogResult = DialogResult.OK;
        }

        private void ChequeAllocationForm_Shown(object sender, EventArgs e)
        {
            invoiceDataGridView.DataSource = new BindingList<_Invoice>(_invoices);
            paymentMethodDataGridView.DataSource = new BindingList<_PaymentMethod>(_paymentMethods);

            AllocateCheque();

            invoiceDataGridView.Focus();
        }

        private void AllocateCheque()
        {
            foreach (_Invoice iv in _invoices)
            {
                iv.PaymentMethods.Clear();
            }

            foreach (_PaymentMethod pm in _paymentMethods)
            {
                pm.Invoices.Clear();

                foreach (_Invoice iv in _invoices)
                {
                    decimal ttrma = pm.TotalRemainAmount; // ตังค์ที่เหลือจาก Payment Method
                    if (ttrma > 0)
                    {
                        decimal ttrmtpa = iv.TotalRemainToPayAmount; // ตังค์ที่ต้องจ่ายอีก
                        if (ttrmtpa > 0)
                        {
                            if (ttrma >= ttrmtpa) // ตังค์เหลือมากกว่า
                            {
                                _InvoicePaymentMethod.Create(iv, pm, ttrmtpa);
                            }
                            else // ตังค์เหลือน้อยกว่า
                            {
                                _InvoicePaymentMethod.Create(iv, pm, ttrma);
                            }
                        }
                    }
                }
            }
        }

        private void ivUpButton_Click(object sender, EventArgs e)
        {
            MoveCurrentInvoice(-1);
        }

        private void ivDownButton_Click(object sender, EventArgs e)
        {
            MoveCurrentInvoice(1);
        }

        private void pmUpButton_Click(object sender, EventArgs e)
        {
            MoveCurrentPaymentMethod(-1);
        }

        private void pmDownButton_Click(object sender, EventArgs e)
        {
            MoveCurrentPaymentMethod(1);
        }

        private void MoveCurrentInvoice(int direction)
        {
            DataGridViewRow row = invoiceDataGridView.CurrentRow;
            int index = row.Index;

            if (_invoices.Count > 1 &&
                ( (direction<0 && index > 0) || (direction > 0 && index < _invoices.Count - 1))
                )
            {
                invoiceDataGridView.Enabled = false;
                _Invoice iv = (_Invoice)row.DataBoundItem;
                _invoices.RemoveAt(index);
                _invoices.Insert(index + direction, iv);                
                invoiceDataGridView.Enabled = true;
                invoiceDataGridView.CurrentCell = invoiceDataGridView.Rows[index + direction].Cells[0];

                _isDirty = true;

                AllocateCheque();
            }
        }

        private void MoveCurrentPaymentMethod(int direction)
        {
            DataGridViewRow row = paymentMethodDataGridView.CurrentRow;
            int index = row.Index;

            if (_paymentMethods.Count > 1 &&
                ((direction < 0 && index > 0) || (direction > 0 && index < _paymentMethods.Count - 1))
                )
            {
                paymentMethodDataGridView.Enabled = false;
                _PaymentMethod pm = (_PaymentMethod)row.DataBoundItem;
                _paymentMethods.RemoveAt(index);
                _paymentMethods.Insert(index + direction, pm);
                paymentMethodDataGridView.Enabled = true;
                paymentMethodDataGridView.CurrentCell = paymentMethodDataGridView.Rows[index + direction].Cells[0];

                _isDirty = true;

                AllocateCheque();
            }
        }

        private void invoiceDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (invoiceDataGridView.Columns[e.ColumnIndex].Name == ivDescColumn.Name)
            {
                _Invoice invoice = (_Invoice)invoiceDataGridView.CurrentRow.DataBoundItem;
                using (AllocationDetailForm af = new AllocationDetailForm())
                {
                    af.SetDescription(invoice);
                    af.ShowDialog();
                }
            }
        }

        private void paymentMethodDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (paymentMethodDataGridView.Columns[e.ColumnIndex].Name == pmDescColumn.Name)
            {
                _PaymentMethod paymentMethod = (_PaymentMethod)paymentMethodDataGridView.CurrentRow.DataBoundItem;
                using (AllocationDetailForm af = new AllocationDetailForm())
                {
                    af.SetDescription(paymentMethod);
                    af.ShowDialog();
                }
            }
        }
    }
}