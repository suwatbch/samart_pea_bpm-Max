using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities;

namespace PEA.BPM.Integration.BPMIntegration.Interface.ExportEntities
{
    public class AG_BillBookInvoice : IListUtility<AG_BillBookInvoice>
    {
        string _action;
        string _crsg;
        string _caId;
        string _invoiceNo;
        string _billBookId;
        string _modifiedDate;
        string _modifiedTime;
        DateTime? _syncDt;

        public string Action
        {
            get { return this._action; }
            set { this._action = value; }
        }

        public string Crsg
        {
            get { return this._crsg; }
            set { this._crsg = value; }
        }

        public string CaId
        {
            get { return this._caId; }
            set { this._caId = value; }
        }

        public string InvoiceNo
        {
            get { return this._invoiceNo; }
            set { this._invoiceNo = value; }
        }

        public string BillBookId
        {
            get { return this._billBookId; }
            set { this._billBookId = value; }
        }

        public string ModifiedDate
        {
            get { return this._modifiedDate; }
            set { this._modifiedDate = value; }
        }

        public string ModifiedTime
        {
            get { return this._modifiedTime; }
            set { this._modifiedTime = value; }
        }

        public DateTime? SyncDt
        {
            get { return this._syncDt; }
            set { this._syncDt = value; }
        }

        #region IListUtility<AG_BillBookInvoice> Members

        public string ToStream()
        {
            IFormatProvider formatDate = CultureInfo.CreateSpecificCulture("en-US");
            if (CaId != null)
            {
                string[] template = { Action, Crsg, CaId, InvoiceNo, BillBookId, ModifiedDate, ModifiedTime };
                return string.Join("|", template);
            }
            else
                return string.Empty;
        }

        public AG_BillBookInvoice ParseExtract(string txt)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}
