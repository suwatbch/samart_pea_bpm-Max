using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [Serializable]
    public class BillingReverseInfo : IListUtility<BillingReverseInfo>
    {
        string _w_01_invoiceNo;

        public string W_01_invoiceNo
        {
            get { return _w_01_invoiceNo; }
            set { _w_01_invoiceNo = value; }
        }
        string _w_1970_print_dt;

        public string W_1970_print_dt
        {
            get { return _w_1970_print_dt; }
            set { _w_1970_print_dt = value; }
        }
        string _w_1910_org_doc;

        public string W_1910_org_doc
        {
            get { return _w_1910_org_doc; }
            set { _w_1910_org_doc = value; }
        }
        string _sto_budat;

        public string Sto_budat
        {
            get { return _sto_budat; }
            set { _sto_budat = value; }
        }
        string _intopbel;

        public string Intopbel
        {
            get { return _intopbel; }
            set { _intopbel = value; }
        }
        string _iceason;

        public string Iceason
        {
            get { return _iceason; }
            set { _iceason = value; }
        }
        string _stokz;

        public string Stokz
        {
            get { return _stokz; }
            set { _stokz = value; }
        }
        string _modifiedBy;

        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }

        #region IListUtility<BillingReverseInfo> Members

        public string ToStream()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public BillingReverseInfo ParseExtract(string txt)
        {
            BillingReverseInfo obj = new BillingReverseInfo();

            string[] sp = txt.Split('|');

            obj.W_01_invoiceNo = StringConvert.ToString(sp[1]);
            obj.W_1970_print_dt = StringConvert.ToString(sp[2]);
            obj.W_1910_org_doc = StringConvert.ToString(sp[3]);
            obj.Sto_budat = StringConvert.ToString(sp[4]);
            obj.Intopbel = StringConvert.ToString(sp[5]);
            obj.Iceason = StringConvert.ToString(sp[6]);
            obj.Stokz = StringConvert.ToString(sp[7]);
            obj.ModifiedBy = "BATCH";

            return obj;
        }

        #endregion
    }
}
