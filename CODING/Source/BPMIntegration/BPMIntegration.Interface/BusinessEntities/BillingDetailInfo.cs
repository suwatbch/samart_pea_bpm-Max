using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]
    public class BillingDetailInfo
    {
        private string _w_01_print_doc;
        private string _w_10_doc_date;
        private string _w_20_buss_place;
        private string _w_30_office_code;
        private string _w_40_sname;
        private string _w_80_cust_name1;
        private string _w_80_cust_name2;
        private string _w_90_cust_name1;
        private string _w_90_cust_name2;
        private string _w_100_sender;
        private string _w_110_post_code;
        private string _w_121_mailing_person;
        private string _w_122_mailing_person;
        private string _w_130_period;
        private string _w_140_reg;
        private string _w_150_cont_acct;
        private string _w_160_device;
        private string _w_170_rate_cat;
        private string _w_171_ettat_code;
        private string _w_180_voltage;
        private string _w_190_multi_n;
        private string _w_191_multi_o;
        private string _w_200_mr_date;
        private string _w_216_address;
        private string _w_217_address;
        private string _w_218_address;
        private string _w_221_address;
        private string _w_222_address;
        private string _w_223_address;
        private string _w_230_post_code;
        private string _w_241_address;
        private string _w_242_address;
        private string _w_243_address;
        private string _w_250_post_code;
        private string _w_255_device_1;
        private string _w_260_zwstand_1_txt;
        private string _w_270_zwstvor_1_txt;
        private string _w_280_umwfakt_1_txt;
        private string _w_290_abrmenge_1_txt;
        private string _w_295_device_2;
        private string _w_300_zwstand_2_txt;
        private string _w_310_zwstvor_2_txt;
        private string _w_320_umwfakt_2_txt;
        private string _w_330_abrmenge_2_txt;
        private string _w_340_peak_txt;
        private string _w_350_dash_txt;
        private string _w_355_device_3;
        private string _w_360_zwstand_3_txt;
        private string _w_370_zwstvor_3_txt;
        private string _w_380_umwfakt_3_txt;
        private string _w_390_abrmenge_3_txt;
        private string _w_400_off_peak_txt;
        private string _w_410_ene_txt;
        private string _w_415_device_4;
        private string _w_420_zwstand_4_txt;
        private string _w_430_zwstvor_4_txt;
        private string _w_440_umwfakt_4_txt;
        private string _w_450_abrmenge_4_txt;
        private string _w_460_hol_txt;
        private string _w_470_zwstand_1_txt;
        private string _w_480_zwstvor_1_txt;
        private string _w_490_consumption_txt;
        private string _w_500_txt6;
        private string _w_510_mr_kw_6_1_txt;
        private string _w_520_mr_kw_6_2_txt;
        private string _w_530_mr_kw_6_3_txt;
        private string _w_540_mr_kw_6_4_txt;
        private string _w_550_mr_kw_6_5;
        private string _w_555_device_6_7;
        private string _w_560_mr_kw_7_1_txt;
        private string _w_570_mr_kw_7_2_txt;
        private string _w_580_mr_kw_7_3_txt;
        private string _w_590_mr_kw_7_4_txt;
        private string _w_600_mr_kw_7_5;
        private string _w_610_mr_kw_8_1_txt;
        private string _w_620_mr_kw_8_2_txt;
        private string _w_630_mr_kw_8_3_txt;
        private string _w_635_mr_kw_8_4_txt;
        private string _w_640_mr_kw_8_5;
        private string _w_650_mr_kw_9_1_txt;
        private string _w_660_mr_kw_9_2_txt;
        private string _w_670_mr_kw_9_3_txt;
        private string _w_680_mr_kw_9_4_txt;
        private string _w_690_mr_kw_9_5;
        private string _w_695_device_9_7;
        private string _w_700_mr_kw_10_1_txt;
        private string _w_710_mr_kw_10_2_txt;
        private string _w_720_mr_kw_10_3_txt;
        private string _w_730_mr_kw_10_4_txt;
        private string _w_740_mr_kw_10_5;
        private string _w_750_mr_kw_11_1_txt;
        private string _w_760_mr_kw_11_2_txt;
        private string _w_770_mr_kw_11_3_txt;
        private string _w_775_mr_kw_11_4_txt;
        private string _w_780_mr_kw_11_5;
        private string _w_790_mr_kw_12_1_txt;
        private string _w_800_mr_kw_12_2_txt;
        private string _w_810_mr_kw_12_3_txt;
        private string _w_820_mr_kw_12_4_txt;
        private string _w_830_mr_kw_12_5;
        private string _w_835_device_12_7;
        private string _w_840_mr_kw_13_1_txt;
        private string _w_850_mr_kw_13_2_txt;
        private string _w_860_mr_kw_13_3_txt;
        private string _w_870_mr_kw_13_4_txt;
        private string _w_890_mr_kw_13_5;
        private string _w_900_mr_kw_14_1_txt;
        private string _w_910_mr_kw_14_2_txt;
        private string _w_920_mr_kw_14_3_txt;
        private string _w_925_mr_kw_14_4_txt;
        private string _w_930_mr_kw_14_5;
        private string _w_940_mr_kw_15_1_txt;
        private string _w_950_mr_kw_15_2_txt;
        private string _w_960_mr_kw_15_3_txt;
        private string _w_970_mr_kw_15_4_txt;
        private string _w_980_mr_kw_15_5;
        private string _w_985_device_15_7;
        private string _w_990_mr_kw_16_1_txt;
        private string _w_1000_mr_kw_16_2_txt;
        private string _w_1010_mr_kw_16_3_txt;
        private string _w_1020_mr_kw_16_4_txt;
        private string _w_1030_mr_kw_16_5;
        private string _w_1040_mr_kw_17_1_txt;
        private string _w_1050_mr_kw_17_2_txt;
        private string _w_1060_mr_kw_17_3_txt;
        private string _w_1065_mr_kw_17_4_txt;
        private string _w_1070_mr_kw_17_5;
        private string _w_1080_service_txt;
        private string _w_1090_service_support_txt;
        private string _w_1100_sum_service_support_txt;
        private string _w_1110_basic_19_1_txt;
        private string _w_1120_description;
        private string _w_1130_kvar_20_1_txt;
        private string _w_1140_kvar_20_2_txt;
        private string _w_1150_kvar_20_3_txt;
        private string _w_1160_kvar_20_4_txt;
        private string _w_1170_kvar_21_1_txt;
        private string _w_1180_kvar_21_2_txt;
        private string _w_1190_kvar_21_3_txt;
        private string _w_1200_kvar_21_4_txt;
        private string _w_1210_gen_kw_amt_txt;
        private string _w_1220_trans_kw_amt_txt;
        private string _w_1230_dist_kw_amt_txt;
        private string _w_1240_gen_kwh_amt_txt;
        private string _w_1250_trans_kwh_amt_txt;
        private string _w_1260_dist_kwh_amt_txt;
        private string _w_1270_dis_supp_amt_txt;
        private string _w_1280_gen_ft_amt_txt;
        private string _w_1290_trans_ft_amt_txt;
        private string _w_1300_dist_ft_amt_txt;
        private string _w_1310_amount_txt;
        private string _w_1320_foreign_amt;
        private string _w_1330_foreign_txt;
        private string _w_1340_foreign_uit;
        private string _w_1345_blue_txt1;
        private string _w_1350_ftgen_txt;
        private string _w_1360_fttrn_txt;
        private string _w_1370_ftdis_txt;
        private string _w_1380_fttot_txt;
        private string _w_1381_ft_peiod_txt;
        private string _w_1390_ftunit_txt;
        private string _w_1400_ftchg_txt;
        private string _w_1410_basic_14_6_txt;
        private string _w_1420_amin_14_7;
        private string _w_1430_ft_basic_txt;
        private string _w_1440_power_txt;
        private string _w_1450_mr_kw_17_6_txt;
        private string _w_1460_mr_kw_17_7;
        private string _w_1470_baht;
        private string _w_1480_net_before_vat_txt;
        private string _w_1485_net_before_vat_left;
        private string _w_1490_tax_rate_txt;
        private string _w_1500_tax_amount_txt;
        private string _w_1505_tax_amount_left;
        private string _w_1510_total_amnt_txt;
        private string _w_1550_case_text1;
        private string _w_1550_case_text2;
        private string _w_1550_case_text3;
        private string _w_1550_case_text4;
        private string _w_1550_case_text5;
        private string _w_1550_case_text6;
        private string _w_1550_case_text7;
        private string _w_1550_case_text8;
        private string _w_1560_spell_amount;
        private string _w_1570_account_number;
        private string _w_1580_payment_due_date;
        private string _w_1581_bank_due_date;
        private string _w_1590_barcode1;
        private string _w_1600_barcode2;
        private string _w_1830_payment_method;
        private string _w_1840_mru;
        private string _w_1841_mru_full;
        private string _w_1850_adjust_amt;
        private string _w_1851_adjust_amt;
        private string _w_1860_trsg;
        private string _w_1861_crsg;
        private string _w_1880_payment_lot;
        private string _w_1890_payer;
        private string _w_1900_absorb_amt_mod;
        private string _w_1901_discount_amt_pea;
        private string _w_1902_absorb_qty;
        private string _w_1910_org_doc;
        private string _w_1911_reverse;
        private string _w_1950_collector_id;
        private string _w_1960_acct_class;
        private string _w_1970_print_dt;
        private DateTime? _w_1971_print_time;
        private string _w_1980_spotbill;
        private string _w_1990_addition;
        private string _w_2000_dispctrl;
        private string _w_2010_noprnt;
        private string _w_2020_noprnt_txt;
        private string _w_2030_barcode_a4;
        private string _w_2040_portion;
        private string _w_2050_mdenr;
        private string _SyncFlag;
        private string _PostBranchServerId;
        private DateTime? _PostDt;
        private DateTime? _CreatedDt;
        private DateTime? _ModifiedDt;
        private string _ModifiedBy;
        private string _FileName;
        private bool _Active;
        private string _action;

        [DataMember(Order = 1)]
        public string W_01_print_doc
        {
            get { return _w_01_print_doc; }
            set { _w_01_print_doc = value; }
        }
        [DataMember(Order = 2)]
        public string W_10_doc_date
        {
            get { return _w_10_doc_date; }
            set { _w_10_doc_date = value; }
        }
        [DataMember(Order = 3)]
        public string W_20_buss_place
        {
            get { return _w_20_buss_place; }
            set { _w_20_buss_place = value; }
        }
        [DataMember(Order = 4)]
        public string W_30_office_code
        {
            get { return _w_30_office_code; }
            set { _w_30_office_code = value; }
        }
        [DataMember(Order = 5)]
        public string W_40_sname
        {
            get { return _w_40_sname; }
            set { _w_40_sname = value; }
        }
        [DataMember(Order = 6)]
        public string W_80_cust_name1
        {
            get { return _w_80_cust_name1; }
            set { _w_80_cust_name1 = value; }
        }
        [DataMember(Order = 7)]
        public string W_80_cust_name2
        {
            get { return _w_80_cust_name2; }
            set { _w_80_cust_name2 = value; }
        }
        [DataMember(Order = 8)]
        public string W_90_cust_name1
        {
            get { return _w_90_cust_name1; }
            set { _w_90_cust_name1 = value; }
        }
        [DataMember(Order = 9)]
        public string W_90_cust_name2
        {
            get { return _w_90_cust_name2; }
            set { _w_90_cust_name2 = value; }
        }
        [DataMember(Order = 10)]
        public string W_100_sender
        {
            get { return _w_100_sender; }
            set { _w_100_sender = value; }
        }
        [DataMember(Order = 11)]
        public string W_110_post_code
        {
            get { return _w_110_post_code; }
            set { _w_110_post_code = value; }
        }
        [DataMember(Order = 12)]
        public string W_121_mailing_person
        {
            get { return _w_121_mailing_person; }
            set { _w_121_mailing_person = value; }
        }
        [DataMember(Order = 13)]
        public string W_122_mailing_person
        {
            get { return _w_122_mailing_person; }
            set { _w_122_mailing_person = value; }
        }
        [DataMember(Order = 14)]
        public string W_130_period
        {
            get { return _w_130_period; }
            set { _w_130_period = value; }
        }
        [DataMember(Order = 15)]
        public string W_140_reg
        {
            get { return _w_140_reg; }
            set { _w_140_reg = value; }
        }
        [DataMember(Order = 16)]
        public string W_150_cont_acct
        {
            get { return _w_150_cont_acct; }
            set { _w_150_cont_acct = value; }
        }
        [DataMember(Order = 17)]
        public string W_160_device
        {
            get { return _w_160_device; }
            set { _w_160_device = value; }
        }
        [DataMember(Order = 18)]
        public string W_170_rate_cat
        {
            get { return _w_170_rate_cat; }
            set { _w_170_rate_cat = value; }
        }
        [DataMember(Order = 19)]
        public string W_171_ettat_code
        {
            get { return _w_171_ettat_code; }
            set { _w_171_ettat_code = value; }
        }
        [DataMember(Order = 20)]
        public string W_180_voltage
        {
            get { return _w_180_voltage; }
            set { _w_180_voltage = value; }
        }
        [DataMember(Order = 21)]
        public string W_190_multi_n
        {
            get { return _w_190_multi_n; }
            set { _w_190_multi_n = value; }
        }
        [DataMember(Order = 22)]
        public string W_191_multi_o
        {
            get { return _w_191_multi_o; }
            set { _w_191_multi_o = value; }
        }
        [DataMember(Order = 23)]
        public string W_200_mr_date
        {
            get { return _w_200_mr_date; }
            set { _w_200_mr_date = value; }
        }
        [DataMember(Order = 24)]
        public string W_216_address
        {
            get { return _w_216_address; }
            set { _w_216_address = value; }
        }
        [DataMember(Order = 25)]
        public string W_217_address
        {
            get { return _w_217_address; }
            set { _w_217_address = value; }
        }
        [DataMember(Order = 26)]
        public string W_218_address
        {
            get { return _w_218_address; }
            set { _w_218_address = value; }
        }
        [DataMember(Order = 27)]
        public string W_221_address
        {
            get { return _w_221_address; }
            set { _w_221_address = value; }
        }
        [DataMember(Order = 28)]
        public string W_222_address
        {
            get { return _w_222_address; }
            set { _w_222_address = value; }
        }
        [DataMember(Order = 29)]
        public string W_223_address
        {
            get { return _w_223_address; }
            set { _w_223_address = value; }
        }
        [DataMember(Order = 30)]
        public string W_230_post_code
        {
            get { return _w_230_post_code; }
            set { _w_230_post_code = value; }
        }
        [DataMember(Order = 31)]
        public string W_241_address
        {
            get { return _w_241_address; }
            set { _w_241_address = value; }
        }
        [DataMember(Order = 32)]
        public string W_242_address
        {
            get { return _w_242_address; }
            set { _w_242_address = value; }
        }
        [DataMember(Order = 33)]
        public string W_243_address
        {
            get { return _w_243_address; }
            set { _w_243_address = value; }
        }
        [DataMember(Order = 34)]
        public string W_250_post_code
        {
            get { return _w_250_post_code; }
            set { _w_250_post_code = value; }
        }
        [DataMember(Order = 35)]
        public string W_255_device_1
        {
            get { return _w_255_device_1; }
            set { _w_255_device_1 = value; }
        }
        [DataMember(Order = 36)]
        public string W_260_zwstand_1_txt
        {
            get { return _w_260_zwstand_1_txt; }
            set { _w_260_zwstand_1_txt = value; }
        }
        [DataMember(Order = 37)]
        public string W_270_zwstvor_1_txt
        {
            get { return _w_270_zwstvor_1_txt; }
            set { _w_270_zwstvor_1_txt = value; }
        }
        [DataMember(Order = 38)]
        public string W_280_umwfakt_1_txt
        {
            get { return _w_280_umwfakt_1_txt; }
            set { _w_280_umwfakt_1_txt = value; }
        }
        [DataMember(Order = 39)]
        public string W_290_abrmenge_1_txt
        {
            get { return _w_290_abrmenge_1_txt; }
            set { _w_290_abrmenge_1_txt = value; }
        }
        [DataMember(Order = 40)]
        public string W_295_device_2
        {
            get { return _w_295_device_2; }
            set { _w_295_device_2 = value; }
        }
        [DataMember(Order = 41)]
        public string W_300_zwstand_2_txt
        {
            get { return _w_300_zwstand_2_txt; }
            set { _w_300_zwstand_2_txt = value; }
        }
        [DataMember(Order = 42)]
        public string W_310_zwstvor_2_txt
        {
            get { return _w_310_zwstvor_2_txt; }
            set { _w_310_zwstvor_2_txt = value; }
        }
        [DataMember(Order = 43)]
        public string W_320_umwfakt_2_txt
        {
            get { return _w_320_umwfakt_2_txt; }
            set { _w_320_umwfakt_2_txt = value; }
        }
        [DataMember(Order = 44)]
        public string W_330_abrmenge_2_txt
        {
            get { return _w_330_abrmenge_2_txt; }
            set { _w_330_abrmenge_2_txt = value; }
        }
        [DataMember(Order = 45)]
        public string W_340_peak_txt
        {
            get { return _w_340_peak_txt; }
            set { _w_340_peak_txt = value; }
        }
        [DataMember(Order = 46)]
        public string W_350_dash_txt
        {
            get { return _w_350_dash_txt; }
            set { _w_350_dash_txt = value; }
        }
        [DataMember(Order = 47)]
        public string W_355_device_3
        {
            get { return _w_355_device_3; }
            set { _w_355_device_3 = value; }
        }
        [DataMember(Order = 48)]
        public string W_360_zwstand_3_txt
        {
            get { return _w_360_zwstand_3_txt; }
            set { _w_360_zwstand_3_txt = value; }
        }
        [DataMember(Order = 49)]
        public string W_370_zwstvor_3_txt
        {
            get { return _w_370_zwstvor_3_txt; }
            set { _w_370_zwstvor_3_txt = value; }
        }
        [DataMember(Order = 50)]
        public string W_380_umwfakt_3_txt
        {
            get { return _w_380_umwfakt_3_txt; }
            set { _w_380_umwfakt_3_txt = value; }
        }

        [DataMember(Order = 51)]
        public string W_390_abrmenge_3_txt
        {
            get { return _w_390_abrmenge_3_txt; }
            set { _w_390_abrmenge_3_txt = value; }
        }
        [DataMember(Order = 52)]
        public string W_400_off_peak_txt
        {
            get { return _w_400_off_peak_txt; }
            set { _w_400_off_peak_txt = value; }
        }
        [DataMember(Order = 53)]
        public string W_410_ene_txt
        {
            get { return _w_410_ene_txt; }
            set { _w_410_ene_txt = value; }
        }
        [DataMember(Order = 54)]
        public string W_415_device_4
        {
            get { return _w_415_device_4; }
            set { _w_415_device_4 = value; }
        }
        [DataMember(Order = 55)]
        public string W_420_zwstand_4_txt
        {
            get { return _w_420_zwstand_4_txt; }
            set { _w_420_zwstand_4_txt = value; }
        }
        [DataMember(Order = 56)]
        public string W_430_zwstvor_4_txt
        {
            get { return _w_430_zwstvor_4_txt; }
            set { _w_430_zwstvor_4_txt = value; }
        }
        [DataMember(Order = 57)]
        public string W_440_umwfakt_4_txt
        {
            get { return _w_440_umwfakt_4_txt; }
            set { _w_440_umwfakt_4_txt = value; }
        }
        [DataMember(Order = 58)]
        public string W_450_abrmenge_4_txt
        {
            get { return _w_450_abrmenge_4_txt; }
            set { _w_450_abrmenge_4_txt = value; }
        }
        [DataMember(Order = 59)]
        public string W_460_hol_txt
        {
            get { return _w_460_hol_txt; }
            set { _w_460_hol_txt = value; }
        }
        [DataMember(Order = 60)]
        public string W_470_zwstand_1_txt
        {
            get { return _w_470_zwstand_1_txt; }
            set { _w_470_zwstand_1_txt = value; }
        }
        [DataMember(Order = 61)]
        public string W_480_zwstvor_1_txt
        {
            get { return _w_480_zwstvor_1_txt; }
            set { _w_480_zwstvor_1_txt = value; }
        }
        [DataMember(Order = 62)]
        public string W_490_consumption_txt
        {
            get { return _w_490_consumption_txt; }
            set { _w_490_consumption_txt = value; }
        }
        [DataMember(Order = 63)]
        public string W_500_txt6
        {
            get { return _w_500_txt6; }
            set { _w_500_txt6 = value; }
        }
        [DataMember(Order = 64)]
        public string W_510_mr_kw_6_1_txt
        {
            get { return _w_510_mr_kw_6_1_txt; }
            set { _w_510_mr_kw_6_1_txt = value; }
        }
        [DataMember(Order = 65)]
        public string W_520_mr_kw_6_2_txt
        {
            get { return _w_520_mr_kw_6_2_txt; }
            set { _w_520_mr_kw_6_2_txt = value; }
        }
        [DataMember(Order = 66)]
        public string W_530_mr_kw_6_3_txt
        {
            get { return _w_530_mr_kw_6_3_txt; }
            set { _w_530_mr_kw_6_3_txt = value; }
        }
        [DataMember(Order = 67)]
        public string W_540_mr_kw_6_4_txt
        {
            get { return _w_540_mr_kw_6_4_txt; }
            set { _w_540_mr_kw_6_4_txt = value; }
        }
        [DataMember(Order = 68)]
        public string W_550_mr_kw_6_5
        {
            get { return _w_550_mr_kw_6_5; }
            set { _w_550_mr_kw_6_5 = value; }
        }
        [DataMember(Order = 69)]
        public string W_555_device_6_7
        {
            get { return _w_555_device_6_7; }
            set { _w_555_device_6_7 = value; }
        }
        [DataMember(Order = 70)]
        public string W_560_mr_kw_7_1_txt
        {
            get { return _w_560_mr_kw_7_1_txt; }
            set { _w_560_mr_kw_7_1_txt = value; }
        }
        [DataMember(Order = 71)]
        public string W_570_mr_kw_7_2_txt
        {
            get { return _w_570_mr_kw_7_2_txt; }
            set { _w_570_mr_kw_7_2_txt = value; }
        }
        [DataMember(Order = 72)]
        public string W_580_mr_kw_7_3_txt
        {
            get { return _w_580_mr_kw_7_3_txt; }
            set { _w_580_mr_kw_7_3_txt = value; }
        }
        [DataMember(Order = 73)]
        public string W_590_mr_kw_7_4_txt
        {
            get { return _w_590_mr_kw_7_4_txt; }
            set { _w_590_mr_kw_7_4_txt = value; }
        }
        [DataMember(Order = 74)]
        public string W_600_mr_kw_7_5
        {
            get { return _w_600_mr_kw_7_5; }
            set { _w_600_mr_kw_7_5 = value; }
        }
        [DataMember(Order = 75)]
        public string W_610_mr_kw_8_1_txt
        {
            get { return _w_610_mr_kw_8_1_txt; }
            set { _w_610_mr_kw_8_1_txt = value; }
        }
        [DataMember(Order = 76)]
        public string W_620_mr_kw_8_2_txt
        {
            get { return _w_620_mr_kw_8_2_txt; }
            set { _w_620_mr_kw_8_2_txt = value; }
        }
        [DataMember(Order = 77)]
        public string W_630_mr_kw_8_3_txt
        {
            get { return _w_630_mr_kw_8_3_txt; }
            set { _w_630_mr_kw_8_3_txt = value; }
        }
        [DataMember(Order = 78)]
        public string W_635_mr_kw_8_4_txt
        {
            get { return _w_635_mr_kw_8_4_txt; }
            set { _w_635_mr_kw_8_4_txt = value; }
        }
        [DataMember(Order = 79)]
        public string W_640_mr_kw_8_5
        {
            get { return _w_640_mr_kw_8_5; }
            set { _w_640_mr_kw_8_5 = value; }
        }
        [DataMember(Order = 80)]
        public string W_650_mr_kw_9_1_txt
        {
            get { return _w_650_mr_kw_9_1_txt; }
            set { _w_650_mr_kw_9_1_txt = value; }
        }
        [DataMember(Order = 81)]
        public string W_660_mr_kw_9_2_txt
        {
            get { return _w_660_mr_kw_9_2_txt; }
            set { _w_660_mr_kw_9_2_txt = value; }
        }
        [DataMember(Order = 82)]
        public string W_670_mr_kw_9_3_txt
        {
            get { return _w_670_mr_kw_9_3_txt; }
            set { _w_670_mr_kw_9_3_txt = value; }
        }
        [DataMember(Order = 83)]
        public string W_680_mr_kw_9_4_txt
        {
            get { return _w_680_mr_kw_9_4_txt; }
            set { _w_680_mr_kw_9_4_txt = value; }
        }
        [DataMember(Order = 84)]
        public string W_690_mr_kw_9_5
        {
            get { return _w_690_mr_kw_9_5; }
            set { _w_690_mr_kw_9_5 = value; }
        }
        [DataMember(Order = 85)]
        public string W_695_device_9_7
        {
            get { return _w_695_device_9_7; }
            set { _w_695_device_9_7 = value; }
        }
        [DataMember(Order = 86)]
        public string W_700_mr_kw_10_1_txt
        {
            get { return _w_700_mr_kw_10_1_txt; }
            set { _w_700_mr_kw_10_1_txt = value; }
        }
        [DataMember(Order = 87)]
        public string W_710_mr_kw_10_2_txt
        {
            get { return _w_710_mr_kw_10_2_txt; }
            set { _w_710_mr_kw_10_2_txt = value; }
        }
        [DataMember(Order = 88)]
        public string W_720_mr_kw_10_3_txt
        {
            get { return _w_720_mr_kw_10_3_txt; }
            set { _w_720_mr_kw_10_3_txt = value; }
        }
        [DataMember(Order = 89)]
        public string W_730_mr_kw_10_4_txt
        {
            get { return _w_730_mr_kw_10_4_txt; }
            set { _w_730_mr_kw_10_4_txt = value; }
        }
        [DataMember(Order = 90)]
        public string W_740_mr_kw_10_5
        {
            get { return _w_740_mr_kw_10_5; }
            set { _w_740_mr_kw_10_5 = value; }
        }
        [DataMember(Order = 91)]
        public string W_750_mr_kw_11_1_txt
        {
            get { return _w_750_mr_kw_11_1_txt; }
            set { _w_750_mr_kw_11_1_txt = value; }
        }
        [DataMember(Order = 92)]
        public string W_760_mr_kw_11_2_txt
        {
            get { return _w_760_mr_kw_11_2_txt; }
            set { _w_760_mr_kw_11_2_txt = value; }
        }
        [DataMember(Order = 93)]
        public string W_770_mr_kw_11_3_txt
        {
            get { return _w_770_mr_kw_11_3_txt; }
            set { _w_770_mr_kw_11_3_txt = value; }
        }
        [DataMember(Order = 94)]
        public string W_775_mr_kw_11_4_txt
        {
            get { return _w_775_mr_kw_11_4_txt; }
            set { _w_775_mr_kw_11_4_txt = value; }
        }
        [DataMember(Order = 95)]
        public string W_780_mr_kw_11_5
        {
            get { return _w_780_mr_kw_11_5; }
            set { _w_780_mr_kw_11_5 = value; }
        }
        [DataMember(Order = 96)]
        public string W_790_mr_kw_12_1_txt
        {
            get { return _w_790_mr_kw_12_1_txt; }
            set { _w_790_mr_kw_12_1_txt = value; }
        }
        [DataMember(Order = 97)]
        public string W_800_mr_kw_12_2_txt
        {
            get { return _w_800_mr_kw_12_2_txt; }
            set { _w_800_mr_kw_12_2_txt = value; }
        }

        [DataMember(Order = 98)]
        public string W_810_mr_kw_12_3_txt
        {
            get { return _w_810_mr_kw_12_3_txt; }
            set { _w_810_mr_kw_12_3_txt = value; }
        }
        [DataMember(Order = 99)]
        public string W_820_mr_kw_12_4_txt
        {
            get { return _w_820_mr_kw_12_4_txt; }
            set { _w_820_mr_kw_12_4_txt = value; }
        }
        [DataMember(Order = 100)]
        public string W_830_mr_kw_12_5
        {
            get { return _w_830_mr_kw_12_5; }
            set { _w_830_mr_kw_12_5 = value; }
        }
        [DataMember(Order = 101)]
        public string W_835_device_12_7
        {
            get { return _w_835_device_12_7; }
            set { _w_835_device_12_7 = value; }
        }
        [DataMember(Order = 102)]
        public string W_840_mr_kw_13_1_txt
        {
            get { return _w_840_mr_kw_13_1_txt; }
            set { _w_840_mr_kw_13_1_txt = value; }
        }
        [DataMember(Order = 103)]
        public string W_850_mr_kw_13_2_txt
        {
            get { return _w_850_mr_kw_13_2_txt; }
            set { _w_850_mr_kw_13_2_txt = value; }
        }
        [DataMember(Order = 104)]
        public string W_860_mr_kw_13_3_txt
        {
            get { return _w_860_mr_kw_13_3_txt; }
            set { _w_860_mr_kw_13_3_txt = value; }
        }
        [DataMember(Order = 105)]
        public string W_870_mr_kw_13_4_txt
        {
            get { return _w_870_mr_kw_13_4_txt; }
            set { _w_870_mr_kw_13_4_txt = value; }
        }
        [DataMember(Order = 106)]
        public string W_890_mr_kw_13_5
        {
            get { return _w_890_mr_kw_13_5; }
            set { _w_890_mr_kw_13_5 = value; }
        }
        [DataMember(Order = 107)]
        public string W_900_mr_kw_14_1_txt
        {
            get { return _w_900_mr_kw_14_1_txt; }
            set { _w_900_mr_kw_14_1_txt = value; }
        }
        [DataMember(Order = 108)]
        public string W_910_mr_kw_14_2_txt
        {
            get { return _w_910_mr_kw_14_2_txt; }
            set { _w_910_mr_kw_14_2_txt = value; }
        }
        [DataMember(Order = 109)]
        public string W_920_mr_kw_14_3_txt
        {
            get { return _w_920_mr_kw_14_3_txt; }
            set { _w_920_mr_kw_14_3_txt = value; }
        }
        [DataMember(Order = 110)]
        public string W_925_mr_kw_14_4_txt
        {
            get { return _w_925_mr_kw_14_4_txt; }
            set { _w_925_mr_kw_14_4_txt = value; }
        }
        [DataMember(Order = 111)]
        public string W_930_mr_kw_14_5
        {
            get { return _w_930_mr_kw_14_5; }
            set { _w_930_mr_kw_14_5 = value; }
        }
        [DataMember(Order = 112)]
        public string W_940_mr_kw_15_1_txt
        {
            get { return _w_940_mr_kw_15_1_txt; }
            set { _w_940_mr_kw_15_1_txt = value; }
        }
        [DataMember(Order = 113)]
        public string W_950_mr_kw_15_2_txt
        {
            get { return _w_950_mr_kw_15_2_txt; }
            set { _w_950_mr_kw_15_2_txt = value; }
        }
        [DataMember(Order = 114)]
        public string W_960_mr_kw_15_3_txt
        {
            get { return _w_960_mr_kw_15_3_txt; }
            set { _w_960_mr_kw_15_3_txt = value; }
        }
        [DataMember(Order = 115)]
        public string W_970_mr_kw_15_4_txt
        {
            get { return _w_970_mr_kw_15_4_txt; }
            set { _w_970_mr_kw_15_4_txt = value; }
        }
        [DataMember(Order = 116)]
        public string W_980_mr_kw_15_5
        {
            get { return _w_980_mr_kw_15_5; }
            set { _w_980_mr_kw_15_5 = value; }
        }
        [DataMember(Order = 117)]
        public string W_985_device_15_7
        {
            get { return _w_985_device_15_7; }
            set { _w_985_device_15_7 = value; }
        }
        [DataMember(Order = 118)]
        public string W_990_mr_kw_16_1_txt
        {
            get { return _w_990_mr_kw_16_1_txt; }
            set { _w_990_mr_kw_16_1_txt = value; }
        }
        [DataMember(Order = 119)]
        public string W_1000_mr_kw_16_2_txt
        {
            get { return _w_1000_mr_kw_16_2_txt; }
            set { _w_1000_mr_kw_16_2_txt = value; }
        }
        [DataMember(Order = 120)]
        public string W_1010_mr_kw_16_3_txt
        {
            get { return _w_1010_mr_kw_16_3_txt; }
            set { _w_1010_mr_kw_16_3_txt = value; }
        }
        [DataMember(Order = 121)]
        public string W_1020_mr_kw_16_4_txt
        {
            get { return _w_1020_mr_kw_16_4_txt; }
            set { _w_1020_mr_kw_16_4_txt = value; }
        }
        [DataMember(Order = 122)]
        public string W_1030_mr_kw_16_5
        {
            get { return _w_1030_mr_kw_16_5; }
            set { _w_1030_mr_kw_16_5 = value; }
        }
        [DataMember(Order = 123)]
        public string W_1040_mr_kw_17_1_txt
        {
            get { return _w_1040_mr_kw_17_1_txt; }
            set { _w_1040_mr_kw_17_1_txt = value; }
        }
        [DataMember(Order = 124)]
        public string W_1050_mr_kw_17_2_txt
        {
            get { return _w_1050_mr_kw_17_2_txt; }
            set { _w_1050_mr_kw_17_2_txt = value; }
        }
        [DataMember(Order = 125)]
        public string W_1060_mr_kw_17_3_txt
        {
            get { return _w_1060_mr_kw_17_3_txt; }
            set { _w_1060_mr_kw_17_3_txt = value; }
        }
        [DataMember(Order = 126)]
        public string W_1065_mr_kw_17_4_txt
        {
            get { return _w_1065_mr_kw_17_4_txt; }
            set { _w_1065_mr_kw_17_4_txt = value; }
        }
        [DataMember(Order = 127)]
        public string W_1070_mr_kw_17_5
        {
            get { return _w_1070_mr_kw_17_5; }
            set { _w_1070_mr_kw_17_5 = value; }
        }
        [DataMember(Order = 128)]
        public string W_1080_service_txt
        {
            get { return _w_1080_service_txt; }
            set { _w_1080_service_txt = value; }
        }
        [DataMember(Order = 129)]
        public string W_1090_service_support_txt
        {
            get { return _w_1090_service_support_txt; }
            set { _w_1090_service_support_txt = value; }
        }
        [DataMember(Order = 130)]
        public string W_1100_sum_service_support_txt
        {
            get { return _w_1100_sum_service_support_txt; }
            set { _w_1100_sum_service_support_txt = value; }
        }
        [DataMember(Order = 131)]
        public string W_1110_basic_19_1_txt
        {
            get { return _w_1110_basic_19_1_txt; }
            set { _w_1110_basic_19_1_txt = value; }
        }
        [DataMember(Order = 132)]
        public string W_1120_description
        {
            get { return _w_1120_description; }
            set { _w_1120_description = value; }
        }
        [DataMember(Order = 133)]
        public string W_1130_kvar_20_1_txt
        {
            get { return _w_1130_kvar_20_1_txt; }
            set { _w_1130_kvar_20_1_txt = value; }
        }
        [DataMember(Order = 134)]
        public string W_1140_kvar_20_2_txt
        {
            get { return _w_1140_kvar_20_2_txt; }
            set { _w_1140_kvar_20_2_txt = value; }
        }
        [DataMember(Order = 135)]
        public string W_1150_kvar_20_3_txt
        {
            get { return _w_1150_kvar_20_3_txt; }
            set { _w_1150_kvar_20_3_txt = value; }
        }
        [DataMember(Order = 136)]
        public string W_1160_kvar_20_4_txt
        {
            get { return _w_1160_kvar_20_4_txt; }
            set { _w_1160_kvar_20_4_txt = value; }
        }
        [DataMember(Order = 137)]
        public string W_1170_kvar_21_1_txt
        {
            get { return _w_1170_kvar_21_1_txt; }
            set { _w_1170_kvar_21_1_txt = value; }
        }
        [DataMember(Order = 138)]
        public string W_1180_kvar_21_2_txt
        {
            get { return _w_1180_kvar_21_2_txt; }
            set { _w_1180_kvar_21_2_txt = value; }
        }
        [DataMember(Order = 139)]
        public string W_1190_kvar_21_3_txt
        {
            get { return _w_1190_kvar_21_3_txt; }
            set { _w_1190_kvar_21_3_txt = value; }
        }
        [DataMember(Order = 140)]
        public string W_1200_kvar_21_4_txt
        {
            get { return _w_1200_kvar_21_4_txt; }
            set { _w_1200_kvar_21_4_txt = value; }
        }
        [DataMember(Order = 141)]
        public string W_1210_gen_kw_amt_txt
        {
            get { return _w_1210_gen_kw_amt_txt; }
            set { _w_1210_gen_kw_amt_txt = value; }
        }
        [DataMember(Order = 142)]
        public string W_1220_trans_kw_amt_txt
        {
            get { return _w_1220_trans_kw_amt_txt; }
            set { _w_1220_trans_kw_amt_txt = value; }
        }

        [DataMember(Order = 143)]
        public string W_1230_dist_kw_amt_txt
        {
            get { return _w_1230_dist_kw_amt_txt; }
            set { _w_1230_dist_kw_amt_txt = value; }
        }
        [DataMember(Order = 144)]
        public string W_1240_gen_kwh_amt_txt
        {
            get { return _w_1240_gen_kwh_amt_txt; }
            set { _w_1240_gen_kwh_amt_txt = value; }
        }
        [DataMember(Order = 145)]
        public string W_1250_trans_kwh_amt_txt
        {
            get { return _w_1250_trans_kwh_amt_txt; }
            set { _w_1250_trans_kwh_amt_txt = value; }
        }
        [DataMember(Order = 146)]
        public string W_1260_dist_kwh_amt_txt
        {
            get { return _w_1260_dist_kwh_amt_txt; }
            set { _w_1260_dist_kwh_amt_txt = value; }
        }
        [DataMember(Order = 147)]
        public string W_1270_dis_supp_amt_txt
        {
            get { return _w_1270_dis_supp_amt_txt; }
            set { _w_1270_dis_supp_amt_txt = value; }
        }
        [DataMember(Order = 148)]
        public string W_1280_gen_ft_amt_txt
        {
            get { return _w_1280_gen_ft_amt_txt; }
            set { _w_1280_gen_ft_amt_txt = value; }
        }
        [DataMember(Order = 149)]
        public string W_1290_trans_ft_amt_txt
        {
            get { return _w_1290_trans_ft_amt_txt; }
            set { _w_1290_trans_ft_amt_txt = value; }
        }
        [DataMember(Order = 150)]
        public string W_1300_dist_ft_amt_txt
        {
            get { return _w_1300_dist_ft_amt_txt; }
            set { _w_1300_dist_ft_amt_txt = value; }
        }
        [DataMember(Order = 151)]
        public string W_1310_amount_txt
        {
            get { return _w_1310_amount_txt; }
            set { _w_1310_amount_txt = value; }
        }
        [DataMember(Order = 152)]
        public string W_1320_foreign_amt
        {
            get { return _w_1320_foreign_amt; }
            set { _w_1320_foreign_amt = value; }
        }
        [DataMember(Order = 153)]
        public string W_1330_foreign_txt
        {
            get { return _w_1330_foreign_txt; }
            set { _w_1330_foreign_txt = value; }
        }
        [DataMember(Order = 154)]
        public string W_1340_foreign_uit
        {
            get { return _w_1340_foreign_uit; }
            set { _w_1340_foreign_uit = value; }
        }
        [DataMember(Order = 155)]
        public string W_1345_blue_txt1
        {
            get { return _w_1345_blue_txt1; }
            set { _w_1345_blue_txt1 = value; }
        }
        [DataMember(Order = 156)]
        public string W_1350_ftgen_txt
        {
            get { return _w_1350_ftgen_txt; }
            set { _w_1350_ftgen_txt = value; }
        }
        [DataMember(Order = 157)]
        public string W_1360_fttrn_txt
        {
            get { return _w_1360_fttrn_txt; }
            set { _w_1360_fttrn_txt = value; }
        }
        [DataMember(Order = 158)]
        public string W_1370_ftdis_txt
        {
            get { return _w_1370_ftdis_txt; }
            set { _w_1370_ftdis_txt = value; }
        }
        [DataMember(Order = 159)]
        public string W_1380_fttot_txt
        {
            get { return _w_1380_fttot_txt; }
            set { _w_1380_fttot_txt = value; }
        }
        [DataMember(Order = 160)]
        public string W_1381_ft_peiod_txt
        {
            get { return _w_1381_ft_peiod_txt; }
            set { _w_1381_ft_peiod_txt = value; }
        }
        [DataMember(Order = 161)]
        public string W_1390_ftunit_txt
        {
            get { return _w_1390_ftunit_txt; }
            set { _w_1390_ftunit_txt = value; }
        }
        [DataMember(Order = 162)]
        public string W_1400_ftchg_txt
        {
            get { return _w_1400_ftchg_txt; }
            set { _w_1400_ftchg_txt = value; }
        }
        [DataMember(Order = 163)]
        public string W_1410_basic_14_6_txt
        {
            get { return _w_1410_basic_14_6_txt; }
            set { _w_1410_basic_14_6_txt = value; }
        }
        [DataMember(Order = 164)]
        public string W_1420_amin_14_7
        {
            get { return _w_1420_amin_14_7; }
            set { _w_1420_amin_14_7 = value; }
        }
        [DataMember(Order = 165)]
        public string W_1430_ft_basic_txt
        {
            get { return _w_1430_ft_basic_txt; }
            set { _w_1430_ft_basic_txt = value; }
        }
        [DataMember(Order = 166)]
        public string W_1440_power_txt
        {
            get { return _w_1440_power_txt; }
            set { _w_1440_power_txt = value; }
        }
        [DataMember(Order = 167)]
        public string W_1450_mr_kw_17_6_txt
        {
            get { return _w_1450_mr_kw_17_6_txt; }
            set { _w_1450_mr_kw_17_6_txt = value; }
        }
        [DataMember(Order = 168)]
        public string W_1460_mr_kw_17_7
        {
            get { return _w_1460_mr_kw_17_7; }
            set { _w_1460_mr_kw_17_7 = value; }
        }
        [DataMember(Order = 169)]
        public string W_1470_baht
        {
            get { return _w_1470_baht; }
            set { _w_1470_baht = value; }
        }
        [DataMember(Order = 170)]
        public string W_1480_net_before_vat_txt
        {
            get { return _w_1480_net_before_vat_txt; }
            set { _w_1480_net_before_vat_txt = value; }
        }
        [DataMember(Order = 171)]
        public string W_1485_net_before_vat_left
        {
            get { return _w_1485_net_before_vat_left; }
            set { _w_1485_net_before_vat_left = value; }
        }
        [DataMember(Order = 172)]
        public string W_1490_tax_rate_txt
        {
            get { return _w_1490_tax_rate_txt; }
            set { _w_1490_tax_rate_txt = value; }
        }
        [DataMember(Order = 173)]
        public string W_1500_tax_amount_txt
        {
            get { return _w_1500_tax_amount_txt; }
            set { _w_1500_tax_amount_txt = value; }
        }
        [DataMember(Order = 174)]
        public string W_1505_tax_amount_left
        {
            get { return _w_1505_tax_amount_left; }
            set { _w_1505_tax_amount_left = value; }
        }
        [DataMember(Order = 175)]
        public string W_1510_total_amnt_txt
        {
            get { return _w_1510_total_amnt_txt; }
            set { _w_1510_total_amnt_txt = value; }
        }
        [DataMember(Order = 176)]
        public string W_1550_case_text1
        {
            get { return _w_1550_case_text1; }
            set { _w_1550_case_text1 = value; }
        }
        [DataMember(Order = 177)]
        public string W_1550_case_text2
        {
            get { return _w_1550_case_text2; }
            set { _w_1550_case_text2 = value; }
        }
        [DataMember(Order = 178)]
        public string W_1550_case_text3
        {
            get { return _w_1550_case_text3; }
            set { _w_1550_case_text3 = value; }
        }
        [DataMember(Order = 179)]
        public string W_1550_case_text4
        {
            get { return _w_1550_case_text4; }
            set { _w_1550_case_text4 = value; }
        }
        [DataMember(Order = 180)]
        public string W_1550_case_text5
        {
            get { return _w_1550_case_text5; }
            set { _w_1550_case_text5 = value; }
        }
        [DataMember(Order = 181)]
        public string W_1550_case_text6
        {
            get { return _w_1550_case_text6; }
            set { _w_1550_case_text6 = value; }
        }
        [DataMember(Order = 182)]
        public string W_1550_case_text7
        {
            get { return _w_1550_case_text7; }
            set { _w_1550_case_text7 = value; }
        }
        [DataMember(Order = 183)]
        public string W_1550_case_text8
        {
            get { return _w_1550_case_text8; }
            set { _w_1550_case_text8 = value; }
        }
        [DataMember(Order = 184)]
        public string W_1560_spell_amount
        {
            get { return _w_1560_spell_amount; }
            set { _w_1560_spell_amount = value; }
        }
        [DataMember(Order = 185)]
        public string W_1570_account_number
        {
            get { return _w_1570_account_number; }
            set { _w_1570_account_number = value; }
        }
        [DataMember(Order = 186)]
        public string W_1580_payment_due_date
        {
            get { return _w_1580_payment_due_date; }
            set { _w_1580_payment_due_date = value; }
        }
        [DataMember(Order = 187)]
        public string W_1581_bank_due_date
        {
            get { return _w_1581_bank_due_date; }
            set { _w_1581_bank_due_date = value; }
        }
        [DataMember(Order = 188)]
        public string W_1590_barcode1
        {
            get { return _w_1590_barcode1; }
            set { _w_1590_barcode1 = value; }
        }

        [DataMember(Order = 189)]
        public string W_1600_barcode2
        {
            get { return _w_1600_barcode2; }
            set { _w_1600_barcode2 = value; }
        }
        [DataMember(Order = 190)]
        public string W_1830_payment_method
        {
            get { return _w_1830_payment_method; }
            set { _w_1830_payment_method = value; }
        }
        [DataMember(Order = 191)]
        public string W_1840_mru
        {
            get { return _w_1840_mru; }
            set { _w_1840_mru = value; }
        }
        [DataMember(Order = 192)]
        public string W_1841_mru_full
        {
            get { return _w_1841_mru_full; }
            set { _w_1841_mru_full = value; }
        }
        [DataMember(Order = 193)]
        public string W_1850_adjust_amt
        {
            get { return _w_1850_adjust_amt; }
            set { _w_1850_adjust_amt = value; }
        }
        [DataMember(Order = 194)]
        public string W_1851_adjust_amt
        {
            get { return _w_1851_adjust_amt; }
            set { _w_1851_adjust_amt = value; }
        }
        [DataMember(Order = 195)]
        public string W_1860_trsg
        {
            get { return _w_1860_trsg; }
            set { _w_1860_trsg = value; }
        }
        [DataMember(Order = 196)]
        public string W_1861_crsg
        {
            get { return _w_1861_crsg; }
            set { _w_1861_crsg = value; }
        }
        [DataMember(Order = 197)]
        public string W_1880_payment_lot
        {
            get { return _w_1880_payment_lot; }
            set { _w_1880_payment_lot = value; }
        }
        [DataMember(Order = 198)]
        public string W_1890_payer
        {
            get { return _w_1890_payer; }
            set { _w_1890_payer = value; }
        }
        [DataMember(Order = 199)]
        public string W_1900_absorb_amt_mod
        {
            get { return _w_1900_absorb_amt_mod; }
            set { _w_1900_absorb_amt_mod = value; }
        }
        [DataMember(Order = 200)]
        public string W_1901_discount_amt_pea
        {
            get { return _w_1901_discount_amt_pea; }
            set { _w_1901_discount_amt_pea = value; }
        }
        [DataMember(Order = 201)]
        public string W_1902_absorb_qty
        {
            get { return _w_1902_absorb_qty; }
            set { _w_1902_absorb_qty = value; }
        }
        [DataMember(Order = 202)]
        public string W_1910_org_doc
        {
            get { return _w_1910_org_doc; }
            set { _w_1910_org_doc = value; }
        }
        [DataMember(Order = 203)]
        public string W_1911_reverse
        {
            get { return _w_1911_reverse; }
            set { _w_1911_reverse = value; }
        }
        [DataMember(Order = 204)]
        public string W_1950_collector_id
        {
            get { return _w_1950_collector_id; }
            set { _w_1950_collector_id = value; }
        }
        [DataMember(Order = 205)]
        public string W_1960_acct_class
        {
            get { return _w_1960_acct_class; }
            set { _w_1960_acct_class = value; }
        }
        [DataMember(Order = 206)]
        public string W_1970_print_dt
        {
            get { return _w_1970_print_dt; }
            set { _w_1970_print_dt = value; }
        }
        [DataMember(Order = 207)]
        public DateTime? W_1971_print_time
        {
            get { return _w_1971_print_time; }
            set { _w_1971_print_time = value; }
        }
        [DataMember(Order = 208)]
        public string W_1980_spotbill
        {
            get { return _w_1980_spotbill; }
            set { _w_1980_spotbill = value; }
        }
        [DataMember(Order = 209)]
        public string W_1990_addition
        {
            get { return _w_1990_addition; }
            set { _w_1990_addition = value; }
        }
        [DataMember(Order = 210)]
        public string W_2000_dispctrl
        {
            get { return _w_2000_dispctrl; }
            set { _w_2000_dispctrl = value; }
        }
        [DataMember(Order = 211)]
        public string W_2010_noprnt
        {
            get { return _w_2010_noprnt; }
            set { _w_2010_noprnt = value; }
        }
        [DataMember(Order = 212)]
        public string W_2020_noprnt_txt
        {
            get { return _w_2020_noprnt_txt; }
            set { _w_2020_noprnt_txt = value; }
        }
        [DataMember(Order = 213)]
        public string W_2030_barcode_a4
        {
            get { return _w_2030_barcode_a4; }
            set { _w_2030_barcode_a4 = value; }
        }
        [DataMember(Order = 214)]
        public string W_2040_portion
        {
            get { return _w_2040_portion; }
            set { _w_2040_portion = value; }
        }
        [DataMember(Order = 215)]
        public string W_2050_mdenr
        {
            get { return _w_2050_mdenr; }
            set { _w_2050_mdenr = value; }
        }
        [DataMember(Order = 216)]
        public string SyncFlag
        {
            get { return _SyncFlag; }
            set { _SyncFlag = value; }
        }
        [DataMember(Order = 217)]
        public string PostBranchServerId
        {
            get { return _PostBranchServerId; }
            set { _PostBranchServerId = value; }
        }
        [DataMember(Order = 218)]
        public DateTime? PostDt
        {
            get { return _PostDt; }
            set { _PostDt = value; }
        }
        [DataMember(Order = 219)]
        public DateTime? CreatedDt
        {
            get { return _CreatedDt; }
            set { _CreatedDt = value; }
        }
        [DataMember(Order = 220)]
        public DateTime? ModifiedDt
        {
            get { return _ModifiedDt; }
            set { _ModifiedDt = value; }
        }
        [DataMember(Order = 221)]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        [DataMember(Order = 222)]
        public string FileName
        {
            get { return _FileName; }
            set { _FileName = value; }
        }
        [DataMember(Order = 223)]
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        [DataMember(Order = 224)]
        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }
    }
}
