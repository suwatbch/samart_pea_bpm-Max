using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [Serializable]
    public class BillingReceiptInfo : IListUtility<BillingReceiptInfo>
    {

        private string _receiptNo;
        private string _printDoc;
        private string _receiptPeriod;
        private string _w_211_address;
        private string _w_212_address;
        private string _w_213_address;
        private string _w_1620_buss_name;
        private string _w_1631_name;
        private string _w_1632_name;
        private string _w_1633_name;
        private string _w_1640_device_13_l1;
        private string _w_1650_rate_cat_13_l2;
        private string _w_1660_contract_ac_14_l1;
        private string _w_1670_period_15_l1;
        private string _w_1680_mr_date_15_l2;
        private string _w_1690_unit_after_16_l1;
        private string _w_1700_unit_previous_16_l2;
        private string _w_1710_consumption_17_l1;
        private string _w_1720_based_amount_18_l1;
        private string _w_1730_discount_amount_19_l1;
        private string _w_1740_disct_descript;
        private string _w_1750_baht;
        private string _w_1760_ft_amount_20_l1;
        private string _w_1770_ft_price_20_l2;
        private string _w_1780_net_before_tax_21_l1;
        private string _w_1790_tax_amount_22_l1;
        private string _w_1800_tax_rate_22_l2;
        private string _w_1810_total_value_23_l1;
        private string _w_1820_payment_date_24_l1;
        private string _action;
        private string _modifiedBy;
        private int _executionLine;     

        public string ReceiptNo
        {
            set { _receiptNo = value; }
            get { return _receiptNo; }
        }

        public string ReceiptPeriod
        {
            set { _receiptPeriod = value; }
            get { return _receiptPeriod; }
        }

        public string PrintDoc
        {
            set { _printDoc = value; }
            get { return _printDoc; }
        }

        public string w_211_address
        {
            set { _w_211_address = value; }
            get { return _w_211_address; }
        }

        public string w_212_address
        {
            set { _w_212_address = value; }
            get { return _w_212_address; }
        }

        public string w_213_address
        {
            set { _w_213_address = value; }
            get { return _w_213_address; }
        }

        public string w_1620_buss_name
        {
            set { _w_1620_buss_name = value; }
            get { return _w_1620_buss_name; }
        }

        public string w_1631_name
        {
            set { _w_1631_name = value; }
            get { return _w_1631_name; }
        }

        public string w_1632_name
        {
            set { _w_1632_name = value; }
            get { return _w_1632_name; }
        }

        public string w_1633_name
        {
            set { _w_1633_name = value; }
            get { return _w_1633_name; }
        }

        public string w_1640_device_13_l1
        {
            set { _w_1640_device_13_l1 = value; }
            get { return _w_1640_device_13_l1; }
        }

        public string w_1650_rate_cat_13_l2
        {
            set { _w_1650_rate_cat_13_l2 = value; }
            get { return _w_1650_rate_cat_13_l2; }
        }

        public string w_1660_contract_ac_14_l1
        {
            set { _w_1660_contract_ac_14_l1 = value; }
            get { return _w_1660_contract_ac_14_l1; }
        }

        public string w_1670_period_15_l1
        {
            set { _w_1670_period_15_l1 = value; }
            get { return _w_1670_period_15_l1; }
        }

        public string w_1680_mr_date_15_l2
        {
            set { _w_1680_mr_date_15_l2 = value; }
            get { return _w_1680_mr_date_15_l2; }
        }

        public string w_1690_unit_after_16_l1
        {
            set { _w_1690_unit_after_16_l1 = value; }
            get { return _w_1690_unit_after_16_l1; }
        }

        public string w_1700_unit_previous_16_l2
        {
            set { _w_1700_unit_previous_16_l2 = value; }
            get { return _w_1700_unit_previous_16_l2; }
        }

        public string w_1710_consumption_17_l1
        {
            set { _w_1710_consumption_17_l1 = value; }
            get { return _w_1710_consumption_17_l1; }
        }

        public string w_1720_based_amount_18_l1
        {
            set { _w_1720_based_amount_18_l1 = value; }
            get { return _w_1720_based_amount_18_l1; }
        }

        public string w_1730_discount_amount_19_l1
        {
            set { _w_1730_discount_amount_19_l1 = value; }
            get { return _w_1730_discount_amount_19_l1; }
        }

        public string w_1740_disct_descript
        {
            set { _w_1740_disct_descript = value; }
            get { return _w_1740_disct_descript; }
        }

        public string w_1750_baht
        {
            set { _w_1750_baht = value; }
            get { return _w_1750_baht; }
        }

        public string w_1760_ft_amount_20_l1
        {
            set { _w_1760_ft_amount_20_l1 = value; }
            get { return _w_1760_ft_amount_20_l1; }
        }

        public string w_1770_ft_price_20_l2
        {
            set { _w_1770_ft_price_20_l2 = value; }
            get { return _w_1770_ft_price_20_l2; }
        }

        public string w_1780_net_before_tax_21_l1
        {
            set { _w_1780_net_before_tax_21_l1 = value; }
            get { return _w_1780_net_before_tax_21_l1; }
        }

        public string w_1790_tax_amount_22_l1
        {
            set { _w_1790_tax_amount_22_l1 = value; }
            get { return _w_1790_tax_amount_22_l1; }
        }

        public string w_1800_tax_rate_22_l2
        {
            set { _w_1800_tax_rate_22_l2 = value; }
            get { return _w_1800_tax_rate_22_l2; }
        }

        public string w_1810_total_value_23_l1
        {
            set { _w_1810_total_value_23_l1 = value; }
            get { return _w_1810_total_value_23_l1; }
        }

        public string w_1820_payment_date_24_l1
        {
            set { _w_1820_payment_date_24_l1 = value; }
            get { return _w_1820_payment_date_24_l1; }
        }

        public string Action
        {
            set { _action = value; }
            get { return _action; }
        }

        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }

        public int ExecutionLine
        {
            get { return _executionLine; }
            set { _executionLine = value; }
        }




        #region IListUtility<BillingReceiptInfo> Members

        public string ToStream()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public BillingReceiptInfo ParseExtract(string txt)
        {
            BillingReceiptInfo it = new BillingReceiptInfo();

            string[] sp = txt.Split('|');

            it.ReceiptNo = StringConvert.ToString(sp[1]); //1
            it.PrintDoc = StringConvert.ToString(sp[2]);
            it.w_211_address = StringConvert.ToString(sp[3]);
            it.w_212_address = StringConvert.ToString(sp[4]);
            it.w_213_address = StringConvert.ToString(sp[5]);
            it.w_1620_buss_name = StringConvert.ToString(sp[6]);
            it.w_1631_name = StringConvert.ToString(sp[7]);
            it.w_1632_name = StringConvert.ToString(sp[8]);
            it.w_1633_name = StringConvert.ToString(sp[9]);
            it.w_1640_device_13_l1 = StringConvert.ToString(sp[10]);
            it.w_1650_rate_cat_13_l2 = StringConvert.ToString(sp[11]);
            it.w_1660_contract_ac_14_l1 = StringConvert.ToString(sp[12]);
            string period = StringConvert.ToString(sp[13]);
            if (period != null && period.Length >= 6)
            {
                it.ReceiptPeriod = Convert.ToString((Convert.ToInt32(period.Substring(0, 4)) + 543)) + period.Substring(4, 2);
                it.w_1670_period_15_l1 = it.ReceiptPeriod.Substring(4, 2) + "/" + it.ReceiptPeriod.Substring(0, 4);
            }
            else
            {
                it.ReceiptPeriod = "";
                it.w_1670_period_15_l1 = "";
            }

            string mrDateEn = StringConvert.ToString(sp[14]);

            try
            {
                if (mrDateEn != null && mrDateEn.Length >= 8)
                {
                    string thaiDate = Convert.ToString((Convert.ToInt32(mrDateEn.Substring(0, 4)) + 543));
                    it.w_1680_mr_date_15_l2 = mrDateEn.Substring(6, 2) + "/"+mrDateEn.Substring(4, 2) +"/"+ thaiDate.Substring(2, 2);
                }
            }
            catch (Exception)
            {
                it.w_1680_mr_date_15_l2 = "";
            }

            it.w_1690_unit_after_16_l1 = StringConvert.ToString(sp[15]);
            it.w_1700_unit_previous_16_l2 = StringConvert.ToString(sp[16]);
            it.w_1710_consumption_17_l1 = StringConvert.ToString(sp[17]);
            it.w_1720_based_amount_18_l1 = StringConvert.ToString(sp[18]);
            it.w_1730_discount_amount_19_l1 = StringConvert.ToString(sp[19]);
            it.w_1740_disct_descript = StringConvert.ToString(sp[20]);
            it.w_1750_baht = StringConvert.ToString(sp[21]);
            it.w_1760_ft_amount_20_l1 = StringConvert.ToString(sp[22]);
            it.w_1770_ft_price_20_l2 = StringConvert.ToString(sp[23]);
            it.w_1780_net_before_tax_21_l1 = StringConvert.ToString(sp[24]);
            it.w_1790_tax_amount_22_l1 = StringConvert.ToString(sp[25]);
            it.w_1800_tax_rate_22_l2 = StringConvert.ToString(sp[26]);
            it.w_1810_total_value_23_l1 = StringConvert.ToString(sp[27]);
            string pmDateEn = StringConvert.ToString(sp[28]);

            try
            {
                if (pmDateEn != null && pmDateEn.Length >= 8)
                {
                    string thaiDate = Convert.ToString((Convert.ToInt32(pmDateEn.Substring(0, 4)) + 543));
                    it.w_1820_payment_date_24_l1 = pmDateEn.Substring(6, 2) +"/" + pmDateEn.Substring(4, 2) +"/"+ thaiDate.Substring(2, 2);
                }
            }
            catch (Exception)
            {
                it.w_1820_payment_date_24_l1 = "";
            }

            it.Action = StringConvert.ToString(sp[29]);
            it.ModifiedBy = "BATCH";

            return it;
        }

        #endregion
    }
}
