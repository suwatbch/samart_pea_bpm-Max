using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.BillPrintingModule.Interface.BusinessEntities
{
    [DataContract]
    public class GreenBill
    {
        private string w_10_invoice_no;
        private string w_100_sender;
        private string w_110_post_code;
        private string w_121_mailing_person;
        private string w_122_mailing_person;
        private string w_130_period;
        private string w_140_reg;
        private string w_150_contract;
        private string w_160_device;
        private string w_170_rate_cat;
        private string w_171_ettat_code;
        private string w_200_mr_date;
        private string w_211_address;
        private string w_212_address;
        private string w_213_address;
        private string w_241_242_address;
        private string w_243_address;
        private string w_250_post_code;
        private string w_255_device_1;
        private string w_260_zwstand_1_txt;
        private string w_270_zwstvor_1_txt;
        private string w_280_umwfakt_1_txt;
        private string w_290_abrmenge_1_txt;
        private string w_295_device_2;
        private string w_300_zwstand_2_txt;
        private string w_310_zwstvor_2_txt;
        private string w_320_umwfakt_2_txt;
        private string w_330_abrmenge_2_txt;
        private string w_340_peak_txt;
        private string w_350_dash_txt;
        private string w_355_device_3;
        private string w_360_zwstand_3_txt;
        private string w_370_zwstvor_3_txt;
        private string w_380_umwfakt_3_txt;
        private string w_390_abrmenge_3_txt;
        private string w_400_off_peak_txt;
        private string w_410_ene_txt;
        private string w_415_device_4;
        private string w_420_zwstand_4_txt;
        private string w_430_zwstvor_4_txt;
        private string w_440_umwfakt_4_txt;
        private string w_450_abrmenge_4_txt;
        private string w_460_hol_txt;
        private string w_500_txt6;
        private string w_1310_amount_txt;
        private string w_1380_fttot_txt;
        private string w_1381_ft_peiod_txt;
        private string w_1400_ftchg_txt;
        private string w_1450_mr_kw_17_6_txt;
        private string w_1460_mr_kw_17_7;
        private string w_1480_net_before_vat_txt;
        private string w_1490_tax_rate_txt;
        private string w_1500_tax_amount_txt;
        private string w_1510_total_amnt_txt;
        private string w_1550_case_text1;
        private string w_1550_case_text2;
        private string w_1550_case_text3;
        private string w_1550_case_text4;
        private string w_1550_case_text7;
        private string w_1550_case_text8;
        private string w_1570_account_number;
        private string w_1581_bank_due_date;
        private string w_1610_invoice;
        private string w_1620_buss_name;
        private string w_1631_1632_name;
        private string w_1633_name;
        private string w_1640_device_13_l1;
        private string w_1650_rate_cat_13_l2;
        private string w_1660_contract_ac_14_l1;
        private string w_1670_period_15_l1;
        private string w_1680_mr_date_15_l2;
        private string w_1690_unit_after_16_l1;
        private string w_1700_unit_previous_16_l2;
        private string w_1710_consumption_17_l1;
        private string w_1720_based_amount_18_l1;
        private string w_1730_discount_amount_19_l1;
        private string w_1740_disct_descript;
        private string w_1750_baht;
        private string w_1760_ft_amount_20_l1;
        private string w_1770_ft_price_20_l2;
        private string w_1780_net_before_tax_21_l1;
        private string w_1790_tax_amount_22_l1;
        private string w_1800_tax_rate_22_l2;
        private string w_1810_total_value_23_l1;
        private string w_1820_payment_date_24_l1;
        private string w_1874_green;
        private string w_1875_green_mass_print;
        private string billSeqNo;
        private string w_2070_taxid_p;
        private string w_2080_taxid_c;


        [DataMember(Order=1)]
        public string W_10_invoice_no
        {
            get { return w_10_invoice_no; }
            set { w_10_invoice_no = value; }
        }


        [DataMember(Order=2)]
        public string W_100_sender
        {
            get { return w_100_sender; }
            set { w_100_sender = value; }
        }


        [DataMember(Order=3)]
        public string W_110_post_code
        {
            get { return w_110_post_code; }
            set { w_110_post_code = value; }
        }


        [DataMember(Order=4)]
        public string W_121_mailing_person
        {
            get { return w_121_mailing_person; }
            set { w_121_mailing_person = value; }
        }


        [DataMember(Order=5)]
        public string W_122_mailing_person
        {
            get { return w_122_mailing_person; }
            set { w_122_mailing_person = value; }
        }


        [DataMember(Order=6)]
        public string W_130_period
        {
            get { return w_130_period; }
            set { w_130_period = value; }
        }


        [DataMember(Order=7)]
        public string W_140_reg
        {
            get { return w_140_reg; }
            set { w_140_reg = value; }
        }


        [DataMember(Order=8)]
        public string W_150_contract
        {
            get { return w_150_contract; }
            set { w_150_contract = value; }
        }


        [DataMember(Order=9)]
        public string W_160_device
        {
            get { return w_160_device; }
            set { w_160_device = value; }
        }


        [DataMember(Order=10)]
        public string W_170_rate_cat
        {
            get { return w_170_rate_cat; }
            set { w_170_rate_cat = value; }
        }


        [DataMember(Order=11)]
        public string W_171_ettat_code
        {
            get { return w_171_ettat_code; }
            set { w_171_ettat_code = value; }
        }


        [DataMember(Order=12)]
        public string W_200_mr_date
        {
            get { return w_200_mr_date; }
            set { w_200_mr_date = value; }
        }


        [DataMember(Order=13)]
        public string W_211_address
        {
            get { return w_211_address; }
            set { w_211_address = value; }
        }


        [DataMember(Order=14)]
        public string W_212_address
        {
            get { return w_212_address; }
            set { w_212_address = value; }
        }


        [DataMember(Order=15)]
        public string W_213_address
        {
            get { return w_213_address; }
            set { w_213_address = value; }
        }


        [DataMember(Order=16)]
        public string W_241_242_address
        {
            get { return w_241_242_address; }
            set { w_241_242_address = value; }
        }


        [DataMember(Order=17)]
        public string W_243_address
        {
            get { return w_243_address; }
            set { w_243_address = value; }
        }


        [DataMember(Order=18)]
        public string W_250_post_code
        {
            get { return w_250_post_code; }
            set { w_250_post_code = value; }
        }


        [DataMember(Order=19)]
        public string W_255_device_1
        {
            get { return w_255_device_1; }
            set { w_255_device_1 = value; }
        }


        [DataMember(Order=20)]
        public string W_260_zwstand_1_txt
        {
            get { return w_260_zwstand_1_txt; }
            set { w_260_zwstand_1_txt = value; }
        }


        [DataMember(Order=21)]
        public string W_270_zwstvor_1_txt
        {
            get { return w_270_zwstvor_1_txt; }
            set { w_270_zwstvor_1_txt = value; }
        }


        [DataMember(Order=22)]
        public string W_280_umwfakt_1_txt
        {
            get { return w_280_umwfakt_1_txt; }
            set { w_280_umwfakt_1_txt = value; }
        }


        [DataMember(Order=23)]
        public string W_290_abrmenge_1_txt
        {
            get { return w_290_abrmenge_1_txt; }
            set { w_290_abrmenge_1_txt = value; }
        }


        [DataMember(Order=24)]
        public string W_295_device_2
        {
            get { return w_295_device_2; }
            set { w_295_device_2 = value; }
        }


        [DataMember(Order=25)]
        public string W_300_zwstand_2_txt
        {
            get { return w_300_zwstand_2_txt; }
            set { w_300_zwstand_2_txt = value; }
        }


        [DataMember(Order=26)]
        public string W_310_zwstvor_2_txt
        {
            get { return w_310_zwstvor_2_txt; }
            set { w_310_zwstvor_2_txt = value; }
        }


        [DataMember(Order=27)]
        public string W_320_umwfakt_2_txt
        {
            get { return w_320_umwfakt_2_txt; }
            set { w_320_umwfakt_2_txt = value; }
        }


        [DataMember(Order=28)]
        public string W_330_abrmenge_2_txt
        {
            get { return w_330_abrmenge_2_txt; }
            set { w_330_abrmenge_2_txt = value; }
        }


        [DataMember(Order=29)]
        public string W_340_peak_txt
        {
            get { return w_340_peak_txt; }
            set { w_340_peak_txt = value; }
        }


        [DataMember(Order=30)]
        public string W_350_dash_txt
        {
            get { return w_350_dash_txt; }
            set { w_350_dash_txt = value; }
        }


        [DataMember(Order=31)]
        public string W_355_device_3
        {
            get { return w_355_device_3; }
            set { w_355_device_3 = value; }
        }


        [DataMember(Order=32)]
        public string W_360_zwstand_3_txt
        {
            get { return w_360_zwstand_3_txt; }
            set { w_360_zwstand_3_txt = value; }
        }


        [DataMember(Order=33)]
        public string W_370_zwstvor_3_txt
        {
            get { return w_370_zwstvor_3_txt; }
            set { w_370_zwstvor_3_txt = value; }
        }


        [DataMember(Order=34)]
        public string W_380_umwfakt_3_txt
        {
            get { return w_380_umwfakt_3_txt; }
            set { w_380_umwfakt_3_txt = value; }
        }


        [DataMember(Order=35)]
        public string W_390_abrmenge_3_txt
        {
            get { return w_390_abrmenge_3_txt; }
            set { w_390_abrmenge_3_txt = value; }
        }


        [DataMember(Order=36)]
        public string W_400_off_peak_txt
        {
            get { return w_400_off_peak_txt; }
            set { w_400_off_peak_txt = value; }
        }


        [DataMember(Order=37)]
        public string W_410_ene_txt
        {
            get { return w_410_ene_txt; }
            set { w_410_ene_txt = value; }
        }


        [DataMember(Order=38)]
        public string W_415_device_4
        {
            get { return w_415_device_4; }
            set { w_415_device_4 = value; }
        }


        [DataMember(Order=39)]
        public string W_420_zwstand_4_txt
        {
            get { return w_420_zwstand_4_txt; }
            set { w_420_zwstand_4_txt = value; }
        }


        [DataMember(Order=40)]
        public string W_430_zwstvor_4_txt
        {
            get { return w_430_zwstvor_4_txt; }
            set { w_430_zwstvor_4_txt = value; }
        }


        [DataMember(Order=41)]
        public string W_440_umwfakt_4_txt
        {
            get { return w_440_umwfakt_4_txt; }
            set { w_440_umwfakt_4_txt = value; }
        }


        [DataMember(Order=42)]
        public string W_450_abrmenge_4_txt
        {
            get { return w_450_abrmenge_4_txt; }
            set { w_450_abrmenge_4_txt = value; }
        }


        [DataMember(Order=43)]
        public string W_460_hol_txt
        {
            get { return w_460_hol_txt; }
            set { w_460_hol_txt = value; }
        }


        [DataMember(Order=44)]
        public string W_500_txt6
        {
            get { return w_500_txt6; }
            set { w_500_txt6 = value; }
        }


        [DataMember(Order=45)]
        public string W_1310_amount_txt
        {
            get { return w_1310_amount_txt; }
            set { w_1310_amount_txt = value; }
        }


        [DataMember(Order=46)]
        public string W_1380_fttot_txt
        {
            get { return w_1380_fttot_txt; }
            set { w_1380_fttot_txt = value; }
        }


        [DataMember(Order=47)]
        public string W_1381_ft_peiod_txt
        {
            get { return w_1381_ft_peiod_txt; }
            set { w_1381_ft_peiod_txt = value; }
        }


        [DataMember(Order=48)]
        public string W_1400_ftchg_txt
        {
            get { return w_1400_ftchg_txt; }
            set { w_1400_ftchg_txt = value; }
        }


        [DataMember(Order=49)]
        public string W_1450_mr_kw_17_6_txt
        {
            get { return w_1450_mr_kw_17_6_txt; }
            set { w_1450_mr_kw_17_6_txt = value; }
        }


        [DataMember(Order=50)]
        public string W_1460_mr_kw_17_7
        {
            get { return w_1460_mr_kw_17_7; }
            set { w_1460_mr_kw_17_7 = value; }
        }


        [DataMember(Order=51)]
        public string W_1480_net_before_vat_txt
        {
            get { return w_1480_net_before_vat_txt; }
            set { w_1480_net_before_vat_txt = value; }
        }


        [DataMember(Order=52)]
        public string W_1490_tax_rate_txt
        {
            get { return w_1490_tax_rate_txt; }
            set { w_1490_tax_rate_txt = value; }
        }


        [DataMember(Order=53)]
        public string W_1500_tax_amount_txt
        {
            get { return w_1500_tax_amount_txt; }
            set { w_1500_tax_amount_txt = value; }
        }


        [DataMember(Order=54)]
        public string W_1510_total_amnt_txt
        {
            get { return w_1510_total_amnt_txt; }
            set { w_1510_total_amnt_txt = value; }
        }


        [DataMember(Order=55)]
        public string W_1550_case_text1
        {
            get { return w_1550_case_text1; }
            set { w_1550_case_text1 = value; }
        }


        [DataMember(Order=56)]
        public string W_1550_case_text2
        {
            get { return w_1550_case_text2; }
            set { w_1550_case_text2 = value; }
        }


        [DataMember(Order=57)]
        public string W_1550_case_text3
        {
            get { return w_1550_case_text3; }
            set { w_1550_case_text3 = value; }
        }


        [DataMember(Order=58)]
        public string W_1550_case_text4
        {
            get { return w_1550_case_text4; }
            set { w_1550_case_text4 = value; }
        }


        [DataMember(Order=59)]
        public string W_1570_account_number
        {
            get { return w_1570_account_number; }
            set { w_1570_account_number = value; }
        }


        [DataMember(Order=60)]
        public string W_1581_bank_due_date
        {
            get { return w_1581_bank_due_date; }
            set { w_1581_bank_due_date = value; }
        }


        [DataMember(Order=61)]
        public string W_1610_invoice
        {
            get { return w_1610_invoice; }
            set { w_1610_invoice = value; }
        }


        [DataMember(Order=62)]
        public string W_1620_buss_name
        {
            get { return w_1620_buss_name; }
            set { w_1620_buss_name = value; }
        }


        [DataMember(Order=63)]
        public string W_1631_1632_name
        {
            get { return w_1631_1632_name; }
            set { w_1631_1632_name = value; }
        }


        [DataMember(Order=64)]
        public string W_1633_name
        {
            get { return w_1633_name; }
            set { w_1633_name = value; }
        }


        [DataMember(Order=65)]
        public string W_1640_device_13_l1
        {
            get { return w_1640_device_13_l1; }
            set { w_1640_device_13_l1 = value; }
        }


        [DataMember(Order=66)]
        public string W_1650_rate_cat_13_l2
        {
            get { return w_1650_rate_cat_13_l2; }
            set { w_1650_rate_cat_13_l2 = value; }
        }


        [DataMember(Order=67)]
        public string W_1660_contract_ac_14_l1
        {
            get { return w_1660_contract_ac_14_l1; }
            set { w_1660_contract_ac_14_l1 = value; }
        }


        [DataMember(Order=68)]
        public string W_1670_period_15_l1
        {
            get { return w_1670_period_15_l1; }
            set { w_1670_period_15_l1 = value; }
        }


        [DataMember(Order=69)]
        public string W_1680_mr_date_15_l2
        {
            get { return w_1680_mr_date_15_l2; }
            set { w_1680_mr_date_15_l2 = value; }
        }


        [DataMember(Order=70)]
        public string W_1690_unit_after_16_l1
        {
            get { return w_1690_unit_after_16_l1; }
            set { w_1690_unit_after_16_l1 = value; }
        }


        [DataMember(Order=71)]
        public string W_1700_unit_previous_16_l2
        {
            get { return w_1700_unit_previous_16_l2; }
            set { w_1700_unit_previous_16_l2 = value; }
        }


        [DataMember(Order=72)]
        public string W_1710_consumption_17_l1
        {
            get { return w_1710_consumption_17_l1; }
            set { w_1710_consumption_17_l1 = value; }
        }


        [DataMember(Order=73)]
        public string W_1720_based_amount_18_l1
        {
            get { return w_1720_based_amount_18_l1; }
            set { w_1720_based_amount_18_l1 = value; }
        }


        [DataMember(Order=74)]
        public string W_1730_discount_amount_19_l1
        {
            get { return w_1730_discount_amount_19_l1; }
            set { w_1730_discount_amount_19_l1 = value; }
        }


        [DataMember(Order=75)]
        public string W_1740_disct_descript
        {
            get { return w_1740_disct_descript; }
            set { w_1740_disct_descript = value; }
        }


        [DataMember(Order=76)]
        public string W_1750_baht
        {
            get { return w_1750_baht; }
            set { w_1750_baht = value; }
        }


        [DataMember(Order=77)]
        public string W_1760_ft_amount_20_l1
        {
            get { return w_1760_ft_amount_20_l1; }
            set { w_1760_ft_amount_20_l1 = value; }
        }


        [DataMember(Order=78)]
        public string W_1770_ft_price_20_l2
        {
            get { return w_1770_ft_price_20_l2; }
            set { w_1770_ft_price_20_l2 = value; }
        }


        [DataMember(Order=79)]
        public string W_1780_net_before_tax_21_l1
        {
            get { return w_1780_net_before_tax_21_l1; }
            set { w_1780_net_before_tax_21_l1 = value; }
        }


        [DataMember(Order=80)]
        public string W_1790_tax_amount_22_l1
        {
            get { return w_1790_tax_amount_22_l1; }
            set { w_1790_tax_amount_22_l1 = value; }
        }


        [DataMember(Order=81)]
        public string W_1800_tax_rate_22_l2
        {
            get { return w_1800_tax_rate_22_l2; }
            set { w_1800_tax_rate_22_l2 = value; }
        }


        [DataMember(Order=82)]
        public string W_1810_total_value_23_l1
        {
            get { return w_1810_total_value_23_l1; }
            set { w_1810_total_value_23_l1 = value; }
        }


        [DataMember(Order=83)]
        public string W_1820_payment_date_24_l1
        {
            get { return w_1820_payment_date_24_l1; }
            set { w_1820_payment_date_24_l1 = value; }
        }


        [DataMember(Order=84)]
        public string W_1874_green
        {
            get { return w_1874_green; }
            set { w_1874_green = value; }
        }


        [DataMember(Order=85)]
        public string W_1875_green_mass_print
        {
            get { return w_1875_green_mass_print; }
            set { w_1875_green_mass_print = value; }
        }


        [DataMember(Order=86)]
        public string BillSeqNo
        {
            get { return billSeqNo; }
            set { billSeqNo = value; }
        }

        [DataMember(Order = 87)]
        public string W_1550_case_text7
        {
            get { return w_1550_case_text7; }
            set { w_1550_case_text7 = value; }
        }

        [DataMember(Order = 88)]
        public string W_1550_case_text8
        {
            get { return w_1550_case_text8; }
            set { w_1550_case_text8 = value; }
        }
    
        [DataMember(Order = 89)]
        public string W_2070_taxid_p
        {
            get { return w_2070_taxid_p; }
            set { w_2070_taxid_p = value; }
        }

        [DataMember(Order = 90)]
        public string W_2080_taxid_c
        {
            get { return w_2080_taxid_c; }
            set { w_2080_taxid_c = value; }
        }
    }
}
