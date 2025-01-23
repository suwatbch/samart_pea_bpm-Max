using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.BillPrintingModule.Interface.BusinessEntities
{
    [DataContract]
    public class BillingDetail
    {
        private string _branchId;

        [DataMember(Order=1)]
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }

        private string _mruId;

        [DataMember(Order=2)]
        public string MruId
        {
            get { return _mruId; }
            set { _mruId = value; }
        }

        private string _caId;

        [DataMember(Order=3)]
        public string CaId
        {
            get { return _caId; }
            set { _caId = value; }
        }

        private string _billPred;

        [DataMember(Order=4)]
        public string BillPred
        {
            get { return _billPred; }
            set { _billPred = value; }
        }

        private int? _billPred_Log;

        [DataMember(Order=5)]
        public int? BillPred_Log
        {
            get { return _billPred_Log; }
            set { _billPred_Log = value; }
        }

        private string _invoiceNo;

        [DataMember(Order=6)]
        public string InvoiceNo
        {
            get { return _invoiceNo; }
            set { _invoiceNo = value; }
        }

        private string _refNewInv;

        [DataMember(Order=7)]
        public string RefNewInv
        {
            get { return _refNewInv; }
            set { _refNewInv = value; }
        }

        private string _w_01_print_doc;

        [DataMember(Order=8)]
        public string W_01_print_doc
        {
            get { return _w_01_print_doc; }
            set { _w_01_print_doc = value; }
        }

        private string _w_10_invoice_no;

        [DataMember(Order=9)]
        public string W_10_invoice_no
        {
            get { return _w_10_invoice_no; }
            set { _w_10_invoice_no = value; }
        }

        private string _w_20_buss_place;

        [DataMember(Order=10)]
        public string W_20_buss_place
        {
            get { return _w_20_buss_place; }
            set { _w_20_buss_place = value; }
        }

        private string _w_30_invoice_no;

        [DataMember(Order=11)]
        public string W_30_invoice_no
        {
            get { return _w_30_invoice_no; }
            set { _w_30_invoice_no = value; }
        }

        private string _w_40_sname;

        [DataMember(Order=12)]
        public string W_40_sname
        {
            get { return _w_40_sname; }
            set { _w_40_sname = value; }
        }

        private string _w_50_day;

        [DataMember(Order=13)]
        public string W_50_day
        {
            get { return _w_50_day; }
            set { _w_50_day = value; }
        }

        private string _w_60_month;

        [DataMember(Order=14)]
        public string W_60_month
        {
            get { return _w_60_month; }
            set { _w_60_month = value; }
        }

        private string _w_70_year;

        [DataMember(Order=15)]
        public string W_70_year
        {
            get { return _w_70_year; }
            set { _w_70_year = value; }
        }

        private string _w_80_cust_name1_name2;

        [DataMember(Order=16)]
        public string W_80_cust_name1_name2
        {
            get { return _w_80_cust_name1_name2; }
            set { _w_80_cust_name1_name2 = value; }
        }

        private string _w_90_cust_name1;

        [DataMember(Order=17)]
        public string W_90_cust_name1
        {
            get { return _w_90_cust_name1; }
            set { _w_90_cust_name1 = value; }
        }

        private string _w_90_cust_name2;

        [DataMember(Order=18)]
        public string W_90_cust_name2
        {
            get { return _w_90_cust_name2; }
            set { _w_90_cust_name2 = value; }
        }

        private string _w_100_sender;

        [DataMember(Order=19)]
        public string W_100_sender
        {
            get { return _w_100_sender; }
            set { _w_100_sender = value; }
        }

        private string _w_110_post_code;

        [DataMember(Order=20)]
        public string W_110_post_code
        {
            get { return _w_110_post_code; }
            set { _w_110_post_code = value; }
        }

        private string _w_121_mailing_person;

        [DataMember(Order=21)]
        public string W_121_mailing_person
        {
            get { return _w_121_mailing_person; }
            set { _w_121_mailing_person = value; }
        }

        private string _w_122_mailing_person;

        [DataMember(Order=22)]
        public string W_122_mailing_person
        {
            get { return _w_122_mailing_person; }
            set { _w_122_mailing_person = value; }
        }

        private string _w_130_period;

        [DataMember(Order=23)]
        public string W_130_period
        {
            get { return _w_130_period; }
            set { _w_130_period = value; }
        }

        private string _w_140_reg;

        [DataMember(Order=24)]
        public string W_140_reg
        {
            get { return _w_140_reg; }
            set { _w_140_reg = value; }
        }

        private string _w_150_contract;

        [DataMember(Order=25)]
        public string W_150_contract
        {
            get { return _w_150_contract; }
            set { _w_150_contract = value; }
        }

        private string _w_160_device;

        [DataMember(Order=26)]
        public string W_160_device
        {
            get { return _w_160_device; }
            set { _w_160_device = value; }
        }

        private string _w_170_rate_cat;

        [DataMember(Order=27)]
        public string W_170_rate_cat
        {
            get { return _w_170_rate_cat; }
            set { _w_170_rate_cat = value; }
        }

        private string _w_180_voltage;

        [DataMember(Order=28)]
        public string W_180_voltage
        {
            get { return _w_180_voltage; }
            set { _w_180_voltage = value; }
        }

        private string _w_190_multi_t;

        [DataMember(Order=29)]
        public string W_190_multi_t
        {
            get { return _w_190_multi_t; }
            set { _w_190_multi_t = value; }
        }

        private string _w_200_mr_date;

        [DataMember(Order=30)]
        public string W_200_mr_date
        {
            get { return _w_200_mr_date; }
            set { _w_200_mr_date = value; }
        }

        private string _w_211_address;

        [DataMember(Order=31)]
        public string W_211_address
        {
            get { return _w_211_address; }
            set { _w_211_address = value; }
        }

        private string _w_212_address;

        [DataMember(Order=32)]
        public string W_212_address
        {
            get { return _w_212_address; }
            set { _w_212_address = value; }
        }

        private string _w_213_address;

        [DataMember(Order=33)]
        public string W_213_address
        {
            get { return _w_213_address; }
            set { _w_213_address = value; }
        }

        private string _w_216_address;

        [DataMember(Order=34)]
        public string W_216_address
        {
            get { return _w_216_address; }
            set { _w_216_address = value; }
        }

        private string _w_217_address;

        [DataMember(Order=35)]
        public string W_217_address
        {
            get { return _w_217_address; }
            set { _w_217_address = value; }
        }

        private string _w_218_address;

        [DataMember(Order=36)]
        public string W_218_address
        {
            get { return _w_218_address; }
            set { _w_218_address = value; }
        }

        private string _w_221_address;

        [DataMember(Order=37)]
        public string W_221_address
        {
            get { return _w_221_address; }
            set { _w_221_address = value; }
        }

        private string _w_222_address;

        [DataMember(Order=38)]
        public string W_222_address
        {
            get { return _w_222_address; }
            set { _w_222_address = value; }
        }

        private string _w_223_address;

        [DataMember(Order=39)]
        public string W_223_address
        {
            get { return _w_223_address; }
            set { _w_223_address = value; }
        }

        private string _w_230_post_code;

        [DataMember(Order=40)]
        public string W_230_post_code
        {
            get { return _w_230_post_code; }
            set { _w_230_post_code = value; }
        }

        private string _w_241_242_address;

        [DataMember(Order=41)]
        public string W_241_242_address
        {
            get { return _w_241_242_address; }
            set { _w_241_242_address = value; }
        }

        private string _w_243_address;

        [DataMember(Order=42)]
        public string W_243_address
        {
            get { return _w_243_address; }
            set { _w_243_address = value; }
        }

        private string _w_250_post_code;

        [DataMember(Order=43)]
        public string W_250_post_code
        {
            get { return _w_250_post_code; }
            set { _w_250_post_code = value; }
        }

        private string _w_260_zwstand_1_txt;

        [DataMember(Order=44)]
        public string W_260_zwstand_1_txt
        {
            get { return _w_260_zwstand_1_txt; }
            set { _w_260_zwstand_1_txt = value; }
        }

        private string _w_270_zwstvor_1_txt;

        [DataMember(Order=45)]
        public string W_270_zwstvor_1_txt
        {
            get { return _w_270_zwstvor_1_txt; }
            set { _w_270_zwstvor_1_txt = value; }
        }

        private string _w_280_umwfakt_1_txt;

        [DataMember(Order=46)]
        public string W_280_umwfakt_1_txt
        {
            get { return _w_280_umwfakt_1_txt; }
            set { _w_280_umwfakt_1_txt = value; }
        }

        private string _w_290_abrmenge_1_txt;

        [DataMember(Order=47)]
        public string W_290_abrmenge_1_txt
        {
            get { return _w_290_abrmenge_1_txt; }
            set { _w_290_abrmenge_1_txt = value; }
        }

        private string _w_300_zwstand_2_txt;

        [DataMember(Order=48)]
        public string W_300_zwstand_2_txt
        {
            get { return _w_300_zwstand_2_txt; }
            set { _w_300_zwstand_2_txt = value; }
        }

        private string _w_310_zwstvor_2_txt;

        [DataMember(Order=49)]
        public string W_310_zwstvor_2_txt
        {
            get { return _w_310_zwstvor_2_txt; }
            set { _w_310_zwstvor_2_txt = value; }
        }

        private string _w_320_umwfakt_2_txt;

        [DataMember(Order=50)]
        public string W_320_umwfakt_2_txt
        {
            get { return _w_320_umwfakt_2_txt; }
            set { _w_320_umwfakt_2_txt = value; }
        }

        private string _w_330_abrmenge_2_txt;

        [DataMember(Order=51)]
        public string W_330_abrmenge_2_txt
        {
            get { return _w_330_abrmenge_2_txt; }
            set { _w_330_abrmenge_2_txt = value; }
        }

        private string _w_340_peak_txt;

        [DataMember(Order=52)]
        public string W_340_peak_txt
        {
            get { return _w_340_peak_txt; }
            set { _w_340_peak_txt = value; }
        }

        private string _w_350_dash_txt;

        [DataMember(Order=53)]
        public string W_350_dash_txt
        {
            get { return _w_350_dash_txt; }
            set { _w_350_dash_txt = value; }
        }

        private string _w_360_zwstand_3_txt;

        [DataMember(Order=54)]
        public string W_360_zwstand_3_txt
        {
            get { return _w_360_zwstand_3_txt; }
            set { _w_360_zwstand_3_txt = value; }
        }

        private string _w_370_zwstvor_3_txt;

        [DataMember(Order=55)]
        public string W_370_zwstvor_3_txt
        {
            get { return _w_370_zwstvor_3_txt; }
            set { _w_370_zwstvor_3_txt = value; }
        }

        private string _w_380_umwfakt_3_txt;

        [DataMember(Order=56)]
        public string W_380_umwfakt_3_txt
        {
            get { return _w_380_umwfakt_3_txt; }
            set { _w_380_umwfakt_3_txt = value; }
        }

        private string _w_390_abrmenge_3_txt;

        [DataMember(Order=57)]
        public string W_390_abrmenge_3_txt
        {
            get { return _w_390_abrmenge_3_txt; }
            set { _w_390_abrmenge_3_txt = value; }
        }

        private string _w_400_off_peak_txt;

        [DataMember(Order=58)]
        public string W_400_off_peak_txt
        {
            get { return _w_400_off_peak_txt; }
            set { _w_400_off_peak_txt = value; }
        }

        private string _w_410_ene_txt;

        [DataMember(Order=59)]
        public string W_410_ene_txt
        {
            get { return _w_410_ene_txt; }
            set { _w_410_ene_txt = value; }
        }

        private string _w_420_zwstand_4_txt;

        [DataMember(Order=60)]
        public string W_420_zwstand_4_txt
        {
            get { return _w_420_zwstand_4_txt; }
            set { _w_420_zwstand_4_txt = value; }
        }

        private string _w_430_zwstvor_4_txt;

        [DataMember(Order=61)]
        public string W_430_zwstvor_4_txt
        {
            get { return _w_430_zwstvor_4_txt; }
            set { _w_430_zwstvor_4_txt = value; }
        }

        private string _w_440_umwfakt_4_txt;

        [DataMember(Order=62)]
        public string W_440_umwfakt_4_txt
        {
            get { return _w_440_umwfakt_4_txt; }
            set { _w_440_umwfakt_4_txt = value; }
        }

        private string _w_450_abrmenge_4_txt;

        [DataMember(Order=63)]
        public string W_450_abrmenge_4_txt
        {
            get { return _w_450_abrmenge_4_txt; }
            set { _w_450_abrmenge_4_txt = value; }
        }

        private string _w_460_hol_txt;

        [DataMember(Order=64)]
        public string W_460_hol_txt
        {
            get { return _w_460_hol_txt; }
            set { _w_460_hol_txt = value; }
        }

        private string _w_470_zwstand_1_txt;

        [DataMember(Order=65)]
        public string W_470_zwstand_1_txt
        {
            get { return _w_470_zwstand_1_txt; }
            set { _w_470_zwstand_1_txt = value; }
        }

        private string _w_480_zwstvor_1_txt;

        [DataMember(Order=66)]
        public string W_480_zwstvor_1_txt
        {
            get { return _w_480_zwstvor_1_txt; }
            set { _w_480_zwstvor_1_txt = value; }
        }

        private string _w_490_consumption_txt;

        [DataMember(Order=67)]
        public string W_490_consumption_txt
        {
            get { return _w_490_consumption_txt; }
            set { _w_490_consumption_txt = value; }
        }

        private string _w_500_txt6;

        [DataMember(Order=68)]
        public string W_500_txt6
        {
            get { return _w_500_txt6; }
            set { _w_500_txt6 = value; }
        }

        private string _w_510_mr_kw_6_1_txt;

        [DataMember(Order=69)]
        public string W_510_mr_kw_6_1_txt
        {
            get { return _w_510_mr_kw_6_1_txt; }
            set { _w_510_mr_kw_6_1_txt = value; }
        }

        private string _w_520_mr_kw_6_2_txt;

        [DataMember(Order=70)]
        public string W_520_mr_kw_6_2_txt
        {
            get { return _w_520_mr_kw_6_2_txt; }
            set { _w_520_mr_kw_6_2_txt = value; }
        }

        private string _w_530_mr_kw_6_3_txt;

        [DataMember(Order=71)]
        public string W_530_mr_kw_6_3_txt
        {
            get { return _w_530_mr_kw_6_3_txt; }
            set { _w_530_mr_kw_6_3_txt = value; }
        }

        private string _w_540_mr_kw_6_4_txt;

        [DataMember(Order=72)]
        public string W_540_mr_kw_6_4_txt
        {
            get { return _w_540_mr_kw_6_4_txt; }
            set { _w_540_mr_kw_6_4_txt = value; }
        }

        private string _w_550_mr_kw_6_5;

        [DataMember(Order=73)]
        public string W_550_mr_kw_6_5
        {
            get { return _w_550_mr_kw_6_5; }
            set { _w_550_mr_kw_6_5 = value; }
        }

        private string _w_560_mr_kw_7_1_txt;

        [DataMember(Order=74)]
        public string W_560_mr_kw_7_1_txt
        {
            get { return _w_560_mr_kw_7_1_txt; }
            set { _w_560_mr_kw_7_1_txt = value; }
        }

        private string _w_570_mr_kw_7_2_txt;

        [DataMember(Order=75)]
        public string W_570_mr_kw_7_2_txt
        {
            get { return _w_570_mr_kw_7_2_txt; }
            set { _w_570_mr_kw_7_2_txt = value; }
        }

        private string _w_580_mr_kw_7_3_txt;

        [DataMember(Order=76)]
        public string W_580_mr_kw_7_3_txt
        {
            get { return _w_580_mr_kw_7_3_txt; }
            set { _w_580_mr_kw_7_3_txt = value; }
        }

        private string _w_590_mr_kw_7_4_txt;

        [DataMember(Order=77)]
        public string W_590_mr_kw_7_4_txt
        {
            get { return _w_590_mr_kw_7_4_txt; }
            set { _w_590_mr_kw_7_4_txt = value; }
        }

        private string _w_600_mr_kw_7_5;

        [DataMember(Order=78)]
        public string W_600_mr_kw_7_5
        {
            get { return _w_600_mr_kw_7_5; }
            set { _w_600_mr_kw_7_5 = value; }
        }

        private string _w_610_mr_kw_8_1_txt;

        [DataMember(Order=79)]
        public string W_610_mr_kw_8_1_txt
        {
            get { return _w_610_mr_kw_8_1_txt; }
            set { _w_610_mr_kw_8_1_txt = value; }
        }

        private string _w_620_mr_kw_8_2_txt;

        [DataMember(Order=80)]
        public string W_620_mr_kw_8_2_txt
        {
            get { return _w_620_mr_kw_8_2_txt; }
            set { _w_620_mr_kw_8_2_txt = value; }
        }

        private string _w_630_mr_kw_8_3_txt;

        [DataMember(Order=81)]
        public string W_630_mr_kw_8_3_txt
        {
            get { return _w_630_mr_kw_8_3_txt; }
            set { _w_630_mr_kw_8_3_txt = value; }
        }

        private string _w_640_mr_kw_8_5;

        [DataMember(Order=82)]
        public string W_640_mr_kw_8_5
        {
            get { return _w_640_mr_kw_8_5; }
            set { _w_640_mr_kw_8_5 = value; }
        }

        private string _w_650_mr_kw_9_1_txt;

        [DataMember(Order=83)]
        public string W_650_mr_kw_9_1_txt
        {
            get { return _w_650_mr_kw_9_1_txt; }
            set { _w_650_mr_kw_9_1_txt = value; }
        }

        private string _w_660_mr_kw_9_2_txt;

        [DataMember(Order=84)]
        public string W_660_mr_kw_9_2_txt
        {
            get { return _w_660_mr_kw_9_2_txt; }
            set { _w_660_mr_kw_9_2_txt = value; }
        }

        private string _w_670_mr_kw_9_3_txt;

        [DataMember(Order=85)]
        public string W_670_mr_kw_9_3_txt
        {
            get { return _w_670_mr_kw_9_3_txt; }
            set { _w_670_mr_kw_9_3_txt = value; }
        }

        private string _w_680_mr_kw_9_4_txt;

        [DataMember(Order=86)]
        public string W_680_mr_kw_9_4_txt
        {
            get { return _w_680_mr_kw_9_4_txt; }
            set { _w_680_mr_kw_9_4_txt = value; }
        }

        private string _w_690_mr_kw_9_5;

        [DataMember(Order=87)]
        public string W_690_mr_kw_9_5
        {
            get { return _w_690_mr_kw_9_5; }
            set { _w_690_mr_kw_9_5 = value; }
        }

        private string _w_700_mr_kw_10_1_txt;

        [DataMember(Order=88)]
        public string W_700_mr_kw_10_1_txt
        {
            get { return _w_700_mr_kw_10_1_txt; }
            set { _w_700_mr_kw_10_1_txt = value; }
        }

        private string _w_710_mr_kw_10_2_txt;

        [DataMember(Order=89)]
        public string W_710_mr_kw_10_2_txt
        {
            get { return _w_710_mr_kw_10_2_txt; }
            set { _w_710_mr_kw_10_2_txt = value; }
        }

        private string _w_720_mr_kw_10_3_txt;

        [DataMember(Order=90)]
        public string W_720_mr_kw_10_3_txt
        {
            get { return _w_720_mr_kw_10_3_txt; }
            set { _w_720_mr_kw_10_3_txt = value; }
        }

        private string _w_730_mr_kw_10_4_txt;

        [DataMember(Order=91)]
        public string W_730_mr_kw_10_4_txt
        {
            get { return _w_730_mr_kw_10_4_txt; }
            set { _w_730_mr_kw_10_4_txt = value; }
        }

        private string _w_740_mr_kw_10_5;

        [DataMember(Order=92)]
        public string W_740_mr_kw_10_5
        {
            get { return _w_740_mr_kw_10_5; }
            set { _w_740_mr_kw_10_5 = value; }
        }

        private string _w_750_mr_kw_11_1_txt;

        [DataMember(Order=93)]
        public string W_750_mr_kw_11_1_txt
        {
            get { return _w_750_mr_kw_11_1_txt; }
            set { _w_750_mr_kw_11_1_txt = value; }
        }

        private string _w_760_mr_kw_11_2_txt;

        [DataMember(Order=94)]
        public string W_760_mr_kw_11_2_txt
        {
            get { return _w_760_mr_kw_11_2_txt; }
            set { _w_760_mr_kw_11_2_txt = value; }
        }

        private string _w_770_mr_kw_11_3_txt;

        [DataMember(Order=95)]
        public string W_770_mr_kw_11_3_txt
        {
            get { return _w_770_mr_kw_11_3_txt; }
            set { _w_770_mr_kw_11_3_txt = value; }
        }

        private string _w_780_mr_kw_11_5;

        [DataMember(Order=96)]
        public string W_780_mr_kw_11_5
        {
            get { return _w_780_mr_kw_11_5; }
            set { _w_780_mr_kw_11_5 = value; }
        }

        private string _w_790_mr_kw_12_1_txt;

        [DataMember(Order=97)]
        public string W_790_mr_kw_12_1_txt
        {
            get { return _w_790_mr_kw_12_1_txt; }
            set { _w_790_mr_kw_12_1_txt = value; }
        }

        private string _w_800_mr_kw_12_2_txt;

        [DataMember(Order=98)]
        public string W_800_mr_kw_12_2_txt
        {
            get { return _w_800_mr_kw_12_2_txt; }
            set { _w_800_mr_kw_12_2_txt = value; }
        }

        private string _w_810_mr_kw_12_3_txt;

        [DataMember(Order=99)]
        public string W_810_mr_kw_12_3_txt
        {
            get { return _w_810_mr_kw_12_3_txt; }
            set { _w_810_mr_kw_12_3_txt = value; }
        }

        private string _w_820_mr_kw_12_4_txt;

        [DataMember(Order=100)]
        public string W_820_mr_kw_12_4_txt
        {
            get { return _w_820_mr_kw_12_4_txt; }
            set { _w_820_mr_kw_12_4_txt = value; }
        }

        private string _w_830_mr_kw_12_5;

        [DataMember(Order=101)]
        public string W_830_mr_kw_12_5
        {
            get { return _w_830_mr_kw_12_5; }
            set { _w_830_mr_kw_12_5 = value; }
        }

        private string _w_840_mr_kw_13_1_txt;

        [DataMember(Order=102)]
        public string W_840_mr_kw_13_1_txt
        {
            get { return _w_840_mr_kw_13_1_txt; }
            set { _w_840_mr_kw_13_1_txt = value; }
        }

        private string _w_850_mr_kw_13_2_txt;

        [DataMember(Order=103)]
        public string W_850_mr_kw_13_2_txt
        {
            get { return _w_850_mr_kw_13_2_txt; }
            set { _w_850_mr_kw_13_2_txt = value; }
        }

        private string _w_860_mr_kw_13_3_txt;

        [DataMember(Order=104)]
        public string W_860_mr_kw_13_3_txt
        {
            get { return _w_860_mr_kw_13_3_txt; }
            set { _w_860_mr_kw_13_3_txt = value; }
        }

        private string _w_870_mr_kw_13_4_txt;

        [DataMember(Order=105)]
        public string W_870_mr_kw_13_4_txt
        {
            get { return _w_870_mr_kw_13_4_txt; }
            set { _w_870_mr_kw_13_4_txt = value; }
        }

        private string _w_890_mr_kw_13_5;

        [DataMember(Order=106)]
        public string W_890_mr_kw_13_5
        {
            get { return _w_890_mr_kw_13_5; }
            set { _w_890_mr_kw_13_5 = value; }
        }

        private string _w_900_mr_kw_14_1_txt;

        [DataMember(Order=107)]
        public string W_900_mr_kw_14_1_txt
        {
            get { return _w_900_mr_kw_14_1_txt; }
            set { _w_900_mr_kw_14_1_txt = value; }
        }

        private string _w_910_mr_kw_14_2_txt;

        [DataMember(Order=108)]
        public string W_910_mr_kw_14_2_txt
        {
            get { return _w_910_mr_kw_14_2_txt; }
            set { _w_910_mr_kw_14_2_txt = value; }
        }

        private string _w_920_mr_kw_14_3_txt;

        [DataMember(Order=109)]
        public string W_920_mr_kw_14_3_txt
        {
            get { return _w_920_mr_kw_14_3_txt; }
            set { _w_920_mr_kw_14_3_txt = value; }
        }

        private string _w_930_mr_kw_14_5;

        [DataMember(Order=110)]
        public string W_930_mr_kw_14_5
        {
            get { return _w_930_mr_kw_14_5; }
            set { _w_930_mr_kw_14_5 = value; }
        }

        private string _w_940_mr_kw_15_1_txt;

        [DataMember(Order=111)]
        public string W_940_mr_kw_15_1_txt
        {
            get { return _w_940_mr_kw_15_1_txt; }
            set { _w_940_mr_kw_15_1_txt = value; }
        }

        private string _w_950_mr_kw_15_2_txt;

        [DataMember(Order=112)]
        public string W_950_mr_kw_15_2_txt
        {
            get { return _w_950_mr_kw_15_2_txt; }
            set { _w_950_mr_kw_15_2_txt = value; }
        }

        private string _w_960_mr_kw_15_3_txt;

        [DataMember(Order=113)]
        public string W_960_mr_kw_15_3_txt
        {
            get { return _w_960_mr_kw_15_3_txt; }
            set { _w_960_mr_kw_15_3_txt = value; }
        }

        private string _w_970_mr_kw_15_4_txt;

        [DataMember(Order=114)]
        public string W_970_mr_kw_15_4_txt
        {
            get { return _w_970_mr_kw_15_4_txt; }
            set { _w_970_mr_kw_15_4_txt = value; }
        }

        private string _w_980_mr_kw_15_5;

        [DataMember(Order=115)]
        public string W_980_mr_kw_15_5
        {
            get { return _w_980_mr_kw_15_5; }
            set { _w_980_mr_kw_15_5 = value; }
        }

        private string _w_990_mr_kw_16_1_txt;

        [DataMember(Order=116)]
        public string W_990_mr_kw_16_1_txt
        {
            get { return _w_990_mr_kw_16_1_txt; }
            set { _w_990_mr_kw_16_1_txt = value; }
        }

        private string _w_1000_mr_kw_16_2_txt;

        [DataMember(Order=117)]
        public string W_1000_mr_kw_16_2_txt
        {
            get { return _w_1000_mr_kw_16_2_txt; }
            set { _w_1000_mr_kw_16_2_txt = value; }
        }

        private string _w_1010_mr_kw_16_3_txt;

        [DataMember(Order=118)]
        public string W_1010_mr_kw_16_3_txt
        {
            get { return _w_1010_mr_kw_16_3_txt; }
            set { _w_1010_mr_kw_16_3_txt = value; }
        }

        private string _w_1020_mr_kw_16_4_txt;

        [DataMember(Order=119)]
        public string W_1020_mr_kw_16_4_txt
        {
            get { return _w_1020_mr_kw_16_4_txt; }
            set { _w_1020_mr_kw_16_4_txt = value; }
        }

        private string _w_1030_mr_kw_16_5;

        [DataMember(Order=120)]
        public string W_1030_mr_kw_16_5
        {
            get { return _w_1030_mr_kw_16_5; }
            set { _w_1030_mr_kw_16_5 = value; }
        }

        private string _w_1040_mr_kw_17_1_txt;

        [DataMember(Order=121)]
        public string W_1040_mr_kw_17_1_txt
        {
            get { return _w_1040_mr_kw_17_1_txt; }
            set { _w_1040_mr_kw_17_1_txt = value; }
        }

        private string _w_1050_mr_kw_17_2_txt;

        [DataMember(Order=122)]
        public string W_1050_mr_kw_17_2_txt
        {
            get { return _w_1050_mr_kw_17_2_txt; }
            set { _w_1050_mr_kw_17_2_txt = value; }
        }

        private string _w_1060_mr_kw_17_3_txt;

        [DataMember(Order=123)]
        public string W_1060_mr_kw_17_3_txt
        {
            get { return _w_1060_mr_kw_17_3_txt; }
            set { _w_1060_mr_kw_17_3_txt = value; }
        }

        private string _w_1070_mr_kw_17_5;

        [DataMember(Order=124)]
        public string W_1070_mr_kw_17_5
        {
            get { return _w_1070_mr_kw_17_5; }
            set { _w_1070_mr_kw_17_5 = value; }
        }

        private string _w_1080_service_txt;

        [DataMember(Order=125)]
        public string W_1080_service_txt
        {
            get { return _w_1080_service_txt; }
            set { _w_1080_service_txt = value; }
        }

        private string _w_1090_service_support_txt;

        [DataMember(Order=126)]
        public string W_1090_service_support_txt
        {
            get { return _w_1090_service_support_txt; }
            set { _w_1090_service_support_txt = value; }
        }

        private string _w_1100_sum_service_support_txt;

        [DataMember(Order=127)]
        public string W_1100_sum_service_support_txt
        {
            get { return _w_1100_sum_service_support_txt; }
            set { _w_1100_sum_service_support_txt = value; }
        }

        private string _w_1110_basic_19_1_txt;

        [DataMember(Order=128)]
        public string W_1110_basic_19_1_txt
        {
            get { return _w_1110_basic_19_1_txt; }
            set { _w_1110_basic_19_1_txt = value; }
        }

        private string _w_1120_description;

        [DataMember(Order=129)]
        public string W_1120_description
        {
            get { return _w_1120_description; }
            set { _w_1120_description = value; }
        }

        private string _w_1130_kvar_20_1_txt;

        [DataMember(Order=130)]
        public string W_1130_kvar_20_1_txt
        {
            get { return _w_1130_kvar_20_1_txt; }
            set { _w_1130_kvar_20_1_txt = value; }
        }

        private string _w_1140_kvar_20_2_txt;

        [DataMember(Order=131)]
        public string W_1140_kvar_20_2_txt
        {
            get { return _w_1140_kvar_20_2_txt; }
            set { _w_1140_kvar_20_2_txt = value; }
        }

        private string _w_1150_kvar_20_3_txt;

        [DataMember(Order=132)]
        public string W_1150_kvar_20_3_txt
        {
            get { return _w_1150_kvar_20_3_txt; }
            set { _w_1150_kvar_20_3_txt = value; }
        }

        private string _w_1160_kvar_20_4_txt;

        [DataMember(Order=133)]
        public string W_1160_kvar_20_4_txt
        {
            get { return _w_1160_kvar_20_4_txt; }
            set { _w_1160_kvar_20_4_txt = value; }
        }

        private string _w_1170_kvar_21_1_txt;

        [DataMember(Order=134)]
        public string W_1170_kvar_21_1_txt
        {
            get { return _w_1170_kvar_21_1_txt; }
            set { _w_1170_kvar_21_1_txt = value; }
        }

        private string _w_1180_kvar_21_2_txt;

        [DataMember(Order=135)]
        public string W_1180_kvar_21_2_txt
        {
            get { return _w_1180_kvar_21_2_txt; }
            set { _w_1180_kvar_21_2_txt = value; }
        }

        private string _w_1190_kvar_21_3_txt;

        [DataMember(Order=136)]
        public string W_1190_kvar_21_3_txt
        {
            get { return _w_1190_kvar_21_3_txt; }
            set { _w_1190_kvar_21_3_txt = value; }
        }

        private string _w_1200_kvar_21_4_txt;

        [DataMember(Order=137)]
        public string W_1200_kvar_21_4_txt
        {
            get { return _w_1200_kvar_21_4_txt; }
            set { _w_1200_kvar_21_4_txt = value; }
        }

        private string _w_1210_gen_kw_amt_txt;

        [DataMember(Order=138)]
        public string W_1210_gen_kw_amt_txt
        {
            get { return _w_1210_gen_kw_amt_txt; }
            set { _w_1210_gen_kw_amt_txt = value; }
        }

        private string _w_1220_trans_kw_amt_txt;

        [DataMember(Order=139)]
        public string W_1220_trans_kw_amt_txt
        {
            get { return _w_1220_trans_kw_amt_txt; }
            set { _w_1220_trans_kw_amt_txt = value; }
        }

        private string _w_1230_dist_kw_amt_txt;

        [DataMember(Order=140)]
        public string W_1230_dist_kw_amt_txt
        {
            get { return _w_1230_dist_kw_amt_txt; }
            set { _w_1230_dist_kw_amt_txt = value; }
        }

        private string _w_1240_gen_kwh_amt_txt;

        [DataMember(Order=141)]
        public string W_1240_gen_kwh_amt_txt
        {
            get { return _w_1240_gen_kwh_amt_txt; }
            set { _w_1240_gen_kwh_amt_txt = value; }
        }

        private string _w_1250_trans_kwh_amt_txt;

        [DataMember(Order=142)]
        public string W_1250_trans_kwh_amt_txt
        {
            get { return _w_1250_trans_kwh_amt_txt; }
            set { _w_1250_trans_kwh_amt_txt = value; }
        }

        private string _w_1260_dist_kwh_amt_txt;

        [DataMember(Order=143)]
        public string W_1260_dist_kwh_amt_txt
        {
            get { return _w_1260_dist_kwh_amt_txt; }
            set { _w_1260_dist_kwh_amt_txt = value; }
        }

        private string _w_1270_dis_supp_amt_txt;

        [DataMember(Order=144)]
        public string W_1270_dis_supp_amt_txt
        {
            get { return _w_1270_dis_supp_amt_txt; }
            set { _w_1270_dis_supp_amt_txt = value; }
        }

        private string _w_1280_gen_ft_amt_txt;

        [DataMember(Order=145)]
        public string W_1280_gen_ft_amt_txt
        {
            get { return _w_1280_gen_ft_amt_txt; }
            set { _w_1280_gen_ft_amt_txt = value; }
        }

        private string _w_1290_trans_ft_amt_txt;

        [DataMember(Order=146)]
        public string W_1290_trans_ft_amt_txt
        {
            get { return _w_1290_trans_ft_amt_txt; }
            set { _w_1290_trans_ft_amt_txt = value; }
        }

        private string _w_1300_dist_ft_amt_txt;

        [DataMember(Order=147)]
        public string W_1300_dist_ft_amt_txt
        {
            get { return _w_1300_dist_ft_amt_txt; }
            set { _w_1300_dist_ft_amt_txt = value; }
        }

        private string _w_1310_amount_txt;

        [DataMember(Order=148)]
        public string W_1310_amount_txt
        {
            get { return _w_1310_amount_txt; }
            set { _w_1310_amount_txt = value; }
        }

        private string _w_1320_foreign_amt;

        [DataMember(Order=149)]
        public string W_1320_foreign_amt
        {
            get { return _w_1320_foreign_amt; }
            set { _w_1320_foreign_amt = value; }
        }

        private string _w_1330_foreign_txt;

        [DataMember(Order=150)]
        public string W_1330_foreign_txt
        {
            get { return _w_1330_foreign_txt; }
            set { _w_1330_foreign_txt = value; }
        }

        private string _w_1340_foreign_uit;

        [DataMember(Order=151)]
        public string W_1340_foreign_uit
        {
            get { return _w_1340_foreign_uit; }
            set { _w_1340_foreign_uit = value; }
        }

        private string _w_1350_ftgen_txt;

        [DataMember(Order=152)]
        public string W_1350_ftgen_txt
        {
            get { return _w_1350_ftgen_txt; }
            set { _w_1350_ftgen_txt = value; }
        }

        private string _w_1360_fttrn_txt;

        [DataMember(Order=153)]
        public string W_1360_fttrn_txt
        {
            get { return _w_1360_fttrn_txt; }
            set { _w_1360_fttrn_txt = value; }
        }

        private string _w_1370_ftdis_txt;

        [DataMember(Order=154)]
        public string W_1370_ftdis_txt
        {
            get { return _w_1370_ftdis_txt; }
            set { _w_1370_ftdis_txt = value; }
        }

        private string _w_1380_fttot_txt;

        [DataMember(Order=155)]
        public string W_1380_fttot_txt
        {
            get { return _w_1380_fttot_txt; }
            set { _w_1380_fttot_txt = value; }
        }

        private string _w_1390_ftunit_txt;

        [DataMember(Order=156)]
        public string W_1390_ftunit_txt
        {
            get { return _w_1390_ftunit_txt; }
            set { _w_1390_ftunit_txt = value; }
        }

        private string _w_1400_ftchg_txt;

        [DataMember(Order=157)]
        public string W_1400_ftchg_txt
        {
            get { return _w_1400_ftchg_txt; }
            set { _w_1400_ftchg_txt = value; }
        }

        private string _w_1410_basic_14_6_txt;

        [DataMember(Order=158)]
        public string W_1410_basic_14_6_txt
        {
            get { return _w_1410_basic_14_6_txt; }
            set { _w_1410_basic_14_6_txt = value; }
        }

        private string _w_1420_amin_14_7;

        [DataMember(Order=159)]
        public string W_1420_amin_14_7
        {
            get { return _w_1420_amin_14_7; }
            set { _w_1420_amin_14_7 = value; }
        }

        private string _w_1430_ft_basic_txt;

        [DataMember(Order=160)]
        public string W_1430_ft_basic_txt
        {
            get { return _w_1430_ft_basic_txt; }
            set { _w_1430_ft_basic_txt = value; }
        }

        private string _w_1440_power_txt;

        [DataMember(Order=161)]
        public string W_1440_power_txt
        {
            get { return _w_1440_power_txt; }
            set { _w_1440_power_txt = value; }
        }

        private string _w_1450_mr_kw_17_6_txt;

        [DataMember(Order=162)]
        public string W_1450_mr_kw_17_6_txt
        {
            get { return _w_1450_mr_kw_17_6_txt; }
            set { _w_1450_mr_kw_17_6_txt = value; }
        }

        private string _w_1460_mr_kw_17_7;

        [DataMember(Order=163)]
        public string W_1460_mr_kw_17_7
        {
            get { return _w_1460_mr_kw_17_7; }
            set { _w_1460_mr_kw_17_7 = value; }
        }

        private string _w_1470_baht;

        [DataMember(Order=164)]
        public string W_1470_baht
        {
            get { return _w_1470_baht; }
            set { _w_1470_baht = value; }
        }

        private string _w_1480_net_before_vat_txt;

        [DataMember(Order=165)]
        public string W_1480_net_before_vat_txt
        {
            get { return _w_1480_net_before_vat_txt; }
            set { _w_1480_net_before_vat_txt = value; }
        }

        private string _w_1490_tax_rate_txt;

        [DataMember(Order=166)]
        public string W_1490_tax_rate_txt
        {
            get { return _w_1490_tax_rate_txt; }
            set { _w_1490_tax_rate_txt = value; }
        }

        private string _w_1500_tax_amount_txt;

        [DataMember(Order=167)]
        public string W_1500_tax_amount_txt
        {
            get { return _w_1500_tax_amount_txt; }
            set { _w_1500_tax_amount_txt = value; }
        }

        private string _w_1510_total_amnt_txt;

        [DataMember(Order=168)]
        public string W_1510_total_amnt_txt
        {
            get { return _w_1510_total_amnt_txt; }
            set { _w_1510_total_amnt_txt = value; }
        }     

        private string _w_1560_spell_amount;

        [DataMember(Order=169)]
        public string W_1560_spell_amount
        {
            get { return _w_1560_spell_amount; }
            set { _w_1560_spell_amount = value; }
        }

        private string _w_1570_account_number;

        [DataMember(Order=170)]
        public string W_1570_account_number
        {
            get { return _w_1570_account_number; }
            set { _w_1570_account_number = value; }
        }

        private string _w_1580_payment_due_date;

        [DataMember(Order=171)]
        public string W_1580_payment_due_date
        {
            get { return _w_1580_payment_due_date; }
            set { _w_1580_payment_due_date = value; }
        }

        private string _w_1581_bank_due_date;

        [DataMember(Order=172)]
        public string W_1581_bank_due_date
        {
            get { return _w_1581_bank_due_date; }
            set { _w_1581_bank_due_date = value; }
        }

        private string _w_1590_barcode1;

        [DataMember(Order=173)]
        public string W_1590_barcode1
        {
            get { return _w_1590_barcode1; }
            set { _w_1590_barcode1 = value; }
        }

        private string _w_1600_barcode2;

        [DataMember(Order=174)]
        public string W_1600_barcode2
        {
            get { return _w_1600_barcode2; }
            set { _w_1600_barcode2 = value; }
        }

        private string _w_1610_invoice;

        [DataMember(Order=175)]
        public string W_1610_invoice
        {
            get { return _w_1610_invoice; }
            set { _w_1610_invoice = value; }
        }

        private string _w_1620_buss_name;

        [DataMember(Order=176)]
        public string W_1620_buss_name
        {
            get { return _w_1620_buss_name; }
            set { _w_1620_buss_name = value; }
        }

        private string _w_1631_1632_name;

        [DataMember(Order=177)]
        public string W_1631_1632_name
        {
            get { return _w_1631_1632_name; }
            set { _w_1631_1632_name = value; }
        }

        private string _w_1633_name;

        [DataMember(Order=178)]
        public string W_1633_name
        {
            get { return _w_1633_name; }
            set { _w_1633_name = value; }
        }

        private string _w_1640_device_13_l1;

        [DataMember(Order=179)]
        public string W_1640_device_13_l1
        {
            get { return _w_1640_device_13_l1; }
            set { _w_1640_device_13_l1 = value; }
        }

        private string _w_1650_rate_cat_13_l2;

        [DataMember(Order=180)]
        public string W_1650_rate_cat_13_l2
        {
            get { return _w_1650_rate_cat_13_l2; }
            set { _w_1650_rate_cat_13_l2 = value; }
        }

        private string _w_1660_contract_ac_14_l1;

        [DataMember(Order=181)]
        public string W_1660_contract_ac_14_l1
        {
            get { return _w_1660_contract_ac_14_l1; }
            set { _w_1660_contract_ac_14_l1 = value; }
        }

        private string _w_1670_period_15_l1;

        [DataMember(Order=182)]
        public string W_1670_period_15_l1
        {
            get { return _w_1670_period_15_l1; }
            set { _w_1670_period_15_l1 = value; }
        }

        private string _w_1680_mr_date_15_l2;

        [DataMember(Order=183)]
        public string W_1680_mr_date_15_l2
        {
            get { return _w_1680_mr_date_15_l2; }
            set { _w_1680_mr_date_15_l2 = value; }
        }

        private string _w_1690_unit_after_16_l1;

        [DataMember(Order=184)]
        public string W_1690_unit_after_16_l1
        {
            get { return _w_1690_unit_after_16_l1; }
            set { _w_1690_unit_after_16_l1 = value; }
        }

        private string _w_1700_unit_previous_16_l2;

        [DataMember(Order=185)]
        public string W_1700_unit_previous_16_l2
        {
            get { return _w_1700_unit_previous_16_l2; }
            set { _w_1700_unit_previous_16_l2 = value; }
        }

        private string _w_1710_consumption_17_l1;

        [DataMember(Order=186)]
        public string W_1710_consumption_17_l1
        {
            get { return _w_1710_consumption_17_l1; }
            set { _w_1710_consumption_17_l1 = value; }
        }

        private string _w_1720_based_amount_18_l1;

        [DataMember(Order=187)]
        public string W_1720_based_amount_18_l1
        {
            get { return _w_1720_based_amount_18_l1; }
            set { _w_1720_based_amount_18_l1 = value; }
        }

        private string _w_1730_discount_amount_19_l1;

        [DataMember(Order=188)]
        public string W_1730_discount_amount_19_l1
        {
            get { return _w_1730_discount_amount_19_l1; }
            set { _w_1730_discount_amount_19_l1 = value; }
        }

        private string _w_1740_disct_descript;

        [DataMember(Order=189)]
        public string W_1740_disct_descript
        {
            get { return _w_1740_disct_descript; }
            set { _w_1740_disct_descript = value; }
        }

        private string _w_1750_baht;

        [DataMember(Order=190)]
        public string W_1750_baht
        {
            get { return _w_1750_baht; }
            set { _w_1750_baht = value; }
        }

        private string _w_1760_ft_amount_20_l1;

        [DataMember(Order=191)]
        public string W_1760_ft_amount_20_l1
        {
            get { return _w_1760_ft_amount_20_l1; }
            set { _w_1760_ft_amount_20_l1 = value; }
        }

        private string _w_1770_ft_price_20_l2;

        [DataMember(Order=192)]
        public string W_1770_ft_price_20_l2
        {
            get { return _w_1770_ft_price_20_l2; }
            set { _w_1770_ft_price_20_l2 = value; }
        }

        private string _w_1780_net_before_tax_21_l1;

        [DataMember(Order=193)]
        public string W_1780_net_before_tax_21_l1
        {
            get { return _w_1780_net_before_tax_21_l1; }
            set { _w_1780_net_before_tax_21_l1 = value; }
        }

        private string _w_1790_tax_amount_22_l1;

        [DataMember(Order=194)]
        public string W_1790_tax_amount_22_l1
        {
            get { return _w_1790_tax_amount_22_l1; }
            set { _w_1790_tax_amount_22_l1 = value; }
        }

        private string _w_1800_tax_rate_22_l2;

        [DataMember(Order=195)]
        public string W_1800_tax_rate_22_l2
        {
            get { return _w_1800_tax_rate_22_l2; }
            set { _w_1800_tax_rate_22_l2 = value; }
        }

        private string _w_1810_total_value_23_l1;

        [DataMember(Order=196)]
        public string W_1810_total_value_23_l1
        {
            get { return _w_1810_total_value_23_l1; }
            set { _w_1810_total_value_23_l1 = value; }
        }

        private string _w_1820_payment_date_24_l1;

        [DataMember(Order=197)]
        public string W_1820_payment_date_24_l1
        {
            get { return _w_1820_payment_date_24_l1; }
            set { _w_1820_payment_date_24_l1 = value; }
        }

        private string _w_1830_payment_method;

        [DataMember(Order=198)]
        public string W_1830_payment_method
        {
            get { return _w_1830_payment_method; }
            set { _w_1830_payment_method = value; }
        }

        private string _w_1840_mru;

        [DataMember(Order=199)]
        public string W_1840_mru
        {
            get { return _w_1840_mru; }
            set { _w_1840_mru = value; }
        }

        private string _w_1850_1851_adjust_amt;

        [DataMember(Order=200)]
        public string W_1850_1851_adjust_amt
        {
            get { return _w_1850_1851_adjust_amt; }
            set { _w_1850_1851_adjust_amt = value; }
        }

        private string _w_1860_trsg;

        [DataMember(Order=201)]
        public string W_1860_trsg
        {
            get { return _w_1860_trsg; }
            set { _w_1860_trsg = value; }
        }

        private string _w_1861_crsg;

        [DataMember(Order=202)]
        public string W_1861_crsg
        {
            get { return _w_1861_crsg; }
            set { _w_1861_crsg = value; }
        }

        private string _w_1870_blue;

        [DataMember(Order=203)]
        public string W_1870_blue
        {
            get { return _w_1870_blue; }
            set { _w_1870_blue = value; }
        }

        private string _w_1871_blue_mass_print;

        [DataMember(Order=204)]
        public string W_1871_blue_mass_print
        {
            get { return _w_1871_blue_mass_print; }
            set { _w_1871_blue_mass_print = value; }
        }

        private string _w_1872_A4;

        [DataMember(Order=205)]
        public string W_1872_A4
        {
            get { return _w_1872_A4; }
            set { _w_1872_A4 = value; }
        }

        private string _w_1873_A4_mass_print;

        [DataMember(Order=206)]
        public string W_1873_A4_mass_print
        {
            get { return _w_1873_A4_mass_print; }
            set { _w_1873_A4_mass_print = value; }
        }

        private string _w_1874_green;

        [DataMember(Order=207)]
        public string W_1874_green
        {
            get { return _w_1874_green; }
            set { _w_1874_green = value; }
        }

        private string _w_1875_green_mass_print;

        [DataMember(Order=208)]
        public string W_1875_green_mass_print
        {
            get { return _w_1875_green_mass_print; }
            set { _w_1875_green_mass_print = value; }
        }

        private string _w_1880_payment_lot;

        [DataMember(Order=209)]
        public string W_1880_payment_lot
        {
            get { return _w_1880_payment_lot; }
            set { _w_1880_payment_lot = value; }
        }

        private string _w_1890_payer;

        [DataMember(Order=210)]
        public string W_1890_payer
        {
            get { return _w_1890_payer; }
            set { _w_1890_payer = value; }
        }

        private string _w_1900_absorb_amt_mod;

        [DataMember(Order=211)]
        public string W_1900_absorb_amt_mod
        {
            get { return _w_1900_absorb_amt_mod; }
            set { _w_1900_absorb_amt_mod = value; }
        }

        private string _w_1901_discount_amt_pea;

        [DataMember(Order=212)]
        public string W_1901_discount_amt_pea
        {
            get { return _w_1901_discount_amt_pea; }
            set { _w_1901_discount_amt_pea = value; }
        }

        private string _w_1902_absorb_qty;

        [DataMember(Order=213)]
        public string W_1902_absorb_qty
        {
            get { return _w_1902_absorb_qty; }
            set { _w_1902_absorb_qty = value; }
        }

        private string _w_1910_org_doc;

        [DataMember(Order=214)]
        public string W_1910_org_doc
        {
            get { return _w_1910_org_doc; }
            set { _w_1910_org_doc = value; }
        }

        private string _w_1920_paid_by;

        [DataMember(Order=215)]
        public string W_1920_paid_by
        {
            get { return _w_1920_paid_by; }
            set { _w_1920_paid_by = value; }
        }

        private string _w_1930_mt_no;

        [DataMember(Order=216)]
        public string W_1930_mt_no
        {
            get { return _w_1930_mt_no; }
            set { _w_1930_mt_no = value; }
        }

        private string _w_1940_bank_id;

        [DataMember(Order=217)]
        public string W_1940_bank_id
        {
            get { return _w_1940_bank_id; }
            set { _w_1940_bank_id = value; }
        }

        private string _syncFlag;

        [DataMember(Order=218)]
        public string SyncFlag
        {
            get { return _syncFlag; }
            set { _syncFlag = value; }
        }

        private string _modifiedBy = Session.User.Id; //default

        [DataMember(Order=219)]
        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }

        //new fields added on 26 August 2008 by TongKung
        private string _w_255_device_1;

        [DataMember(Order=220)]
        public string W_255_device_1
        {
            get { return _w_255_device_1; }
            set { _w_255_device_1 = value; }
        }

        private string _w_295_device_2;

        [DataMember(Order=221)]
        public string W_295_device_2
        {
            get { return _w_295_device_2; }
            set { _w_295_device_2 = value; }
        }

        private string _w_355_device_3;

        [DataMember(Order=222)]
        public string W_355_device_3
        {
            get { return _w_355_device_3; }
            set { _w_355_device_3 = value; }
        }

        private string _w_415_device_4;

        [DataMember(Order=223)]
        public string W_415_device_4
        {
            get { return _w_415_device_4; }
            set { _w_415_device_4 = value; }
        }

        private string _w_555_device_6_7;

        [DataMember(Order=224)]
        public string W_555_device_6_7
        {
            get { return _w_555_device_6_7; }
            set { _w_555_device_6_7 = value; }
        }

        private string _w_695_device_9_7;

        [DataMember(Order=225)]
        public string W_695_device_9_7
        {
            get { return _w_695_device_9_7; }
            set { _w_695_device_9_7 = value; }
        }

        private string _w_835_device_12_7;

        [DataMember(Order=226)]
        public string W_835_device_12_7
        {
            get { return _w_835_device_12_7; }
            set { _w_835_device_12_7 = value; }
        }

        private string _w_985_device_15_7;

        [DataMember(Order=227)]
        public string W_985_device_15_7
        {
            get { return _w_985_device_15_7; }
            set { _w_985_device_15_7 = value; }
        }

        private string _w_1485_net_before_vat_left;

        [DataMember(Order=228)]
        public string W_1485_net_before_vat_left
        {
            get { return _w_1485_net_before_vat_left; }
            set { _w_1485_net_before_vat_left = value; }
        }

        private string _w_1505_tax_amount_left;

        [DataMember(Order=229)]
        public string W_1505_tax_amount_left
        {
            get { return _w_1505_tax_amount_left; }
            set { _w_1505_tax_amount_left = value; }
        }

        private string _w_1345_blue_txt1;

        [DataMember(Order=230)]
        public string W_1345_blue_txt1
        {
            get { return _w_1345_blue_txt1; }
            set { _w_1345_blue_txt1 = value; }
        }

        //adjust fields changed on 26 August 2008 by TongKung
        private string _w_1550_case_text1;

        [DataMember(Order=231)]
        public string W_1550_case_text1
        {
            get { return _w_1550_case_text1; }
            set { _w_1550_case_text1 = value; }
        }

        private string _w_1550_case_text2;

        [DataMember(Order=232)]
        public string W_1550_case_text2
        {
            get { return _w_1550_case_text2; }
            set { _w_1550_case_text2 = value; }
        }

        private string _w_1550_case_text3;

        [DataMember(Order=233)]
        public string W_1550_case_text3
        {
            get { return _w_1550_case_text3; }
            set { _w_1550_case_text3 = value; }
        }

        private string _w_1550_case_text4;

        [DataMember(Order=234)]
        public string W_1550_case_text4
        {
            get { return _w_1550_case_text4; }
            set { _w_1550_case_text4 = value; }
        }

        private string _w_1550_case_text5;

        [DataMember(Order=235)]
        public string W_1550_case_text5
        {
            get { return _w_1550_case_text5; }
            set { _w_1550_case_text5 = value; }
        }
    }
}
