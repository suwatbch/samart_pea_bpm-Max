using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities;


namespace PEA.BPM.Integration.BPMIntegration.Interface.ExportEntities
{
    public class AG_Compensation : IListUtility<AG_Compensation>
    {
        string _cmId;
        string _caId;
        List<string> _billBookRefId = new List<string>();
        DateTime? _bookCreateDt;
        DateTime? _calculateDt;
        decimal? _baseAmount;
        decimal? _vatAmount;
        decimal? _gAmount;
        decimal? _fineAmount;
        decimal? _taxAmount;        
        string _period;
        DateTime? _syncDt;

        public string CmId
        {
            get { return this._cmId; }
            set { this._cmId = value; }
        }

        public string CaId
        {
            get { return this._caId; }
            set { this._caId = value; }
        }

        public List<string> BillBookRefId
        {
            get { return this._billBookRefId; }
            set { this._billBookRefId = value; }
        }

        public DateTime? BookCreateDt
        {
            get { return this._bookCreateDt; }
            set { this._bookCreateDt = value; }
        }

        public DateTime? CalculateDt
        {
            get { return this._calculateDt; }
            set { this._calculateDt = value; }
        }

        public decimal? BaseAmount
        {
            get { return this._baseAmount; }
            set { this._baseAmount = value; }
        }

        public decimal? VatAmount
        {
            get { return this._vatAmount; }
            set { this._vatAmount = value; }
        }

        public decimal? GAmount
        {
            get { return this._gAmount; }
            set { this._gAmount = value; }
        }

        public decimal? FineAmount
        {
            get { return this._fineAmount;}
            set { this._fineAmount = value;}
        }

        public decimal? TaxAmount
        {
            get { return this._taxAmount;}
            set { this._taxAmount = value;}
        }
        public string Period
        {
            get { return this._period; }
            set { this._period = value; }
        }

        public DateTime? SyncDt
        {
            get { return this._syncDt; }
            set { this._syncDt = value; }
        }
        

        #region IListUtility<AG_Compensation> Members

        public string ToStream()
        {
            IFormatProvider formatDate = CultureInfo.CreateSpecificCulture("en-US");
            if (CaId != null)
            {
                string[] template = { CaId, String.Join(",",BillBookRefId.ToArray()), BookCreateDt.Value.ToString("yyyyMMdd", formatDate), CalculateDt.Value.ToString("yyyyMMdd", formatDate), 
                            BaseAmount.Value.ToString("#0.00"), VatAmount.Value.ToString("#0.00"), GAmount.Value.ToString("#0.00"), FineAmount.Value.ToString("#0.00"), TaxAmount.Value.ToString("#0.00"), Period };
                return string.Join("|", template);
            }
            else
                return string.Empty;
        }

        public AG_Compensation ParseExtract(string txt)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}
