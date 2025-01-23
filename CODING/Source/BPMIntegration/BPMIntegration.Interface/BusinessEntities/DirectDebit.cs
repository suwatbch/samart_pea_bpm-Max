using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.ArchitectureInterface.Services;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    public class DirectDebit : IListUtility<DirectDebit>
    {
        private string _printDoc;
        public string PrintDoc
        {
            get { return _printDoc; }
            set { _printDoc = value; }
        }

        private string _printBranch;
        public string PrintBranch
        {
            get { return _printBranch; }
            set { _printBranch = value; }
        }

        private string _branchId;
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }

        private string _mruId;
        public string MruId
        {
            get { return _mruId; }
            set { _mruId = value; }
        }

        private string _caId;
        public string CaId
        {
            get { return _caId; }
            set { _caId = value; }
        }

        private string _period;
        public string Period
        {
            get { return _period; }
            set { _period = value; }
        }

        private string _paymentMethod;
        public string PaymentMethod
        {
            get { return _paymentMethod; }
            set { _paymentMethod = value; }
        }

        private string _receiptNo;
        public string ReceiptNo
        {
            get { return _receiptNo; }
            set { _receiptNo = value; }
        }

        private string _bankId;
        public string BankId
        {
            get { return _bankId; }
            set { _bankId = value; }
        }

        private string _bankDueDate;
        public string BankDueDate
        {
            get { return _bankDueDate; }
            set { _bankDueDate = value; }
        }

        private string _mtNo;
        public string MtNo
        {
            get { return _mtNo; }
            set { _mtNo = value; }
        }

        private string _grpInvPaymentDueDate;
        public string GrpInvPaymentDueDate
        {
            get { return _grpInvPaymentDueDate; }
            set { _grpInvPaymentDueDate = value; }
        }

        private string _groupingDate;
        public string GroupingDate
        {
            get { return _groupingDate; }
            set { _groupingDate = value; }
        }

        private string _action;
        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }

        private string _modifiedBy;
        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }

        private int _executionLine;
        public int ExecutionLine
        {
            get { return _executionLine; }
            set { _executionLine = value; }
        }      
        
        #region IListUtility<DirectDebitBlue> Members

        public string ToStream()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public DirectDebit ParseExtract(string txt)
        {
            DirectDebit it = new DirectDebit();

            string[] sp = txt.Split('|');

            it.PrintDoc = StringConvert.ToString(sp[1]); //1
            it.PrintBranch = StringConvert.ToString(sp[2]);
            it.BranchId = StringConvert.ToString(sp[3]);
            it.MruId = StringConvert.ToString(sp[4]);
            it.CaId = StringConvert.ToString(sp[5]);
            it.Period = StringConvert.ToString(sp[6]);
            it.PaymentMethod = StringConvert.ToString(sp[7]); 
            it.ReceiptNo = StringConvert.ToString(sp[8]); 
            it.BankId = StringConvert.ToString(sp[9]); 

            //validate and set value to BankDueDate
            if (it.PaymentMethod == "D" || it.PaymentMethod == "F" || it.PaymentMethod == "E")
            {
                it.BankDueDate = StringConvert.ToString(sp[10]);

                if (!String.IsNullOrEmpty(it.BankDueDate))
                {
                    if (it.BankDueDate.Length == 8)
                    {
                        int tmp2 = (Convert.ToInt32(it.BankDueDate.Substring(0, 4)) + 543);
                        it.BankDueDate = tmp2.ToString() + it.BankDueDate.Remove(0, 4); //yyyymmdd
                    }
                }
            }

            it.MtNo = StringConvert.ToString(sp[11]);
            it.GrpInvPaymentDueDate = StringConvert.ToString(sp[12]); // input = yyyymmdd,output = yyyymmdd (thai)
            if (!String.IsNullOrEmpty(it.GrpInvPaymentDueDate))
            {
                if (it.GrpInvPaymentDueDate.Length == 8)
                {
                    int tmp2 = (Convert.ToInt32(it.GrpInvPaymentDueDate.Substring(0, 4)) + 543);
                    it.GrpInvPaymentDueDate = tmp2.ToString() + it.GrpInvPaymentDueDate.Remove(0, 4); //yyyymmdd
                }
            }

            it.GroupingDate = StringConvert.ToString(sp[13]); //6 input = yyyymmdd, output = yyyymmdd (thai)
                if (!String.IsNullOrEmpty(it.GroupingDate))
                {
                    if (it.GroupingDate.Length == 8)
                    {
                        int tmp2 = (Convert.ToInt32(it.GroupingDate.Substring(0, 4)) + 543);
                        it.GroupingDate = tmp2.ToString() + it.GroupingDate.Remove(0, 4); //yyyymmdd
                }
            }

            it.Action = StringConvert.ToString(sp[14]);

            if (!String.IsNullOrEmpty(it.Action))
            {
                if (it.Action.IndexOfAny(new char[] { '1', '2', '3' }) != 0)
                    throw new Exception("Data Invalid [Column Name = Action].");
            }
            else
                throw new Exception("Data Invalid [Column Name = Action].");


            it.ModifiedBy = "BATCH";

            return it;
        }

        #endregion
    }
}

   