using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class GroupInvoiceInfo
    {

        string _groupInvoiceId;
        string _groupInvoiceNo;
        string _branchId;
        string _caId;
        string _caName;
        string _invoiceNo;
        decimal? _amount;
        DateTime? _returnDate;
        int _returnTime;
        string _modifiedBy;


        [DataMember(Order=1)]
        public string GroupInvoiceId
        {
            get { return this._groupInvoiceId; }
            set { this._groupInvoiceId = value; }
        }


        [DataMember(Order=2)]
        public string GroupInvoiceNo
        {
            get { return this._groupInvoiceNo; }
            set { this._groupInvoiceNo = value; }
        }


        [DataMember(Order=3)]
        public string BranchId
        {
            get { return this._branchId; }
            set { this._branchId = value; }
        }


        [DataMember(Order=4)]
        public string CaId
        {
            get { return this._caId; }
            set { this._caId = value; }
        }


        [DataMember(Order=5)]
        public string CaName
        {
            get { return this._caName; }
            set { this._caName = value; }
        }


        [DataMember(Order=6)]
        public string InvoiceNo
        {
            get { return this._invoiceNo; }
            set { this._invoiceNo = value; }
        }


        [DataMember(Order=7)]
        public decimal? Amount
        {
            get { return this._amount; }
            set { this._amount = value; }
        }


        [DataMember(Order=8)]
        public DateTime? ReturnDate
        {
            get { return this._returnDate ; }
            set { this._returnDate = value; }
        }

        //Checked TongKung
        //[DataMember(Order=9)]
        public string ReturnDateText
        {
            get
            {
                if (ReturnDate == null)
                {
                    return string.Empty;
                }
                else
                {
                    return ReturnDate.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                }
            }
        }

        [DataMember(Order=10)]
        public int ReturnTime
        {
            get { return this._returnTime; }
            set { this._returnTime = value; }
        }


        [DataMember(Order=11)]
        public string ModifiedBy
        {
            get { return this._modifiedBy; }
            set { this._modifiedBy = value; }
        }
    }
}
