using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.AgencyManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class CommissionInfo
    {
        private List<CommissionBaseInfo> _baseCommission = new List<CommissionBaseInfo>();
        private SpecialCommissionInfo _specialCommission;
        private InvoiceCommissionInfo _invoiceCommission;
        private FineInfo _fineInfo;
        private FineDetailInfo _fineDetailInfo;           
        //keep special help, in case quiring commission data for reprint
        private HelpOfferInfo _helpInfo;
        private SaveCommissionInfo _saveInfo;  //save format

        decimal? _vatRate = 0;
        decimal? _taxRate = 0;


        [DataMember(Order=1)]
        public SaveCommissionInfo SaveInfo
        {
            set { _saveInfo = value; }
            get { return _saveInfo; }
        }


        [DataMember(Order=2)]
        public List<CommissionBaseInfo> BaseCommission
        {
            set { _baseCommission = value; }
            get { return _baseCommission; }
        }


        [DataMember(Order=3)]
        public SpecialCommissionInfo SpecialCommission
        {
            set { _specialCommission = value; }
            get { return _specialCommission; }
        }


        [DataMember(Order=4)]
        public InvoiceCommissionInfo InvoiceCommission
        {
            set { _invoiceCommission = value; }
            get { return _invoiceCommission; }
        }


        [DataMember(Order=5)]
        public FineInfo FineInfo
        {
            set { _fineInfo = value; }
            get { return _fineInfo; }
        }


        [DataMember(Order=6)]
        public FineDetailInfo FineDetail
        {
            set { _fineDetailInfo = value; }
            get { return _fineDetailInfo; }
        }

        //public TaxCalculationInfo TaxInfo
        //{
        //    set { _taxInfo = value; }
        //    get { return _taxInfo; }
        //}

        //public TaxDetailInfo TaxDetail
        //{
        //    set { _taxDetailInfo = value; }
        //    get { return _taxDetailInfo; }
        //}


        [DataMember(Order=7)]
        public HelpOfferInfo HelpInfo
        {
            set { _helpInfo = value; }
            get { return _helpInfo; }
        }


        [DataMember(Order=8)]
        public decimal? VatRate
        {
            get { return this._vatRate; }
            set { this._vatRate = value; }
        }


        [DataMember(Order=9)]
        public decimal? TaxRate
        {
            get { return this._taxRate; }
            set { this._taxRate = value; }
        }

    }
}
