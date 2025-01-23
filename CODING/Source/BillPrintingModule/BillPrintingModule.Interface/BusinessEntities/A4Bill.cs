using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.BillPrintingModule.Interface.BusinessEntities
{
    [DataContract]
    public class A4Bill
    {
        private string w_30_invoice_no;
        private string w_40_sname;
        private string w_50_day;
        private string w_60_month;
        private string w_70_year;
        private string w_80_cust_name1_name2;
        private string w_130_period;
        private string w_140_reg;
        private string w_150_contract;
        private string w_160_device;
        private string w_170_rate_cat;
        private string w_171_ettat_code;
        private string w_180_voltage;
        private string w_190_multi_t;
        private string w_200_mr_date;
        private string w_500_txt6;
        private string w_510_mr_kw_6_1_txt;
        private string w_520_mr_kw_6_2_txt;
        private string w_530_mr_kw_6_3_txt;
        private string w_540_mr_kw_6_4_txt;
        private string w_550_mr_kw_6_5;
        private string w_555_device_6_7;
        private string w_560_mr_kw_7_1_txt;
        private string w_570_mr_kw_7_2_txt;
        private string w_580_mr_kw_7_3_txt;
        private string w_590_mr_kw_7_4_txt;
        private string w_600_mr_kw_7_5;
        private string w_610_mr_kw_8_1_txt;
        private string w_620_mr_kw_8_2_txt;
        private string w_630_mr_kw_8_3_txt;
        private string w_640_mr_kw_8_5;
        private string w_650_mr_kw_9_1_txt;
        private string w_660_mr_kw_9_2_txt;
        private string w_670_mr_kw_9_3_txt;
        private string w_680_mr_kw_9_4_txt;
        private string w_690_mr_kw_9_5;
        private string w_695_device_9_7;
        private string w_700_mr_kw_10_1_txt;
        private string w_710_mr_kw_10_2_txt;
        private string w_720_mr_kw_10_3_txt;
        private string w_730_mr_kw_10_4_txt;
        private string w_740_mr_kw_10_5;
        private string w_750_mr_kw_11_1_txt;
        private string w_760_mr_kw_11_2_txt;
        private string w_770_mr_kw_11_3_txt;
        private string w_780_mr_kw_11_5;
        private string w_790_mr_kw_12_1_txt;
        private string w_800_mr_kw_12_2_txt;
        private string w_810_mr_kw_12_3_txt;
        private string w_820_mr_kw_12_4_txt;
        private string w_830_mr_kw_12_5;
        private string w_835_device_12_7;
        private string w_840_mr_kw_13_1_txt;
        private string w_850_mr_kw_13_2_txt;
        private string w_860_mr_kw_13_3_txt;
        private string w_870_mr_kw_13_4_txt;
        private string w_890_mr_kw_13_5;
        private string w_900_mr_kw_14_1_txt;
        private string w_910_mr_kw_14_2_txt;
        private string w_920_mr_kw_14_3_txt;
        private string w_930_mr_kw_14_5;
        private string w_940_mr_kw_15_1_txt;
        private string w_950_mr_kw_15_2_txt;
        private string w_960_mr_kw_15_3_txt;
        private string w_970_mr_kw_15_4_txt;
        private string w_980_mr_kw_15_5;
        private string w_985_device_15_7;
        private string w_990_mr_kw_16_1_txt;
        private string w_1000_mr_kw_16_2_txt;
        private string w_1010_mr_kw_16_3_txt;
        private string w_1020_mr_kw_16_4_txt;
        private string w_1030_mr_kw_16_5;
        private string w_1040_mr_kw_17_1_txt;
        private string w_1050_mr_kw_17_2_txt;
        private string w_1060_mr_kw_17_3_txt;
        private string w_1070_mr_kw_17_5;
        private string w_1080_service_txt;
        private string w_1090_service_support_txt;
        private string w_1100_sum_service_support_txt;
        private string w_1110_basic_19_1_txt;
        private string w_1120_description;
        private string w_1130_kvar_20_1_txt;
        private string w_1140_kvar_20_2_txt;
        private string w_1150_kvar_20_3_txt;
        private string w_1160_kvar_20_4_txt;
        private string w_1170_kvar_21_1_txt;
        private string w_1180_kvar_21_2_txt;
        private string w_1190_kvar_21_3_txt;
        private string w_1200_kvar_21_4_txt;
        private string w_1210_gen_kw_amt_txt;
        private string w_1220_trans_kw_amt_txt;
        private string w_1230_dist_kw_amt_txt;
        private string w_1240_gen_kwh_amt_txt;
        private string w_1250_trans_kwh_amt_txt;
        private string w_1260_dist_kwh_amt_txt;
        private string w_1270_dis_supp_amt_txt;
        private string w_1280_gen_ft_amt_txt;
        private string w_1290_trans_ft_amt_txt;
        private string w_1300_dist_ft_amt_txt;
        private string w_1350_ftgen_txt;
        private string w_1360_fttrn_txt;
        private string w_1370_ftdis_txt;
        private string w_1380_fttot_txt;
        private string w_1390_ftunit_txt;
        private string w_1400_ftchg_txt;
        private string w_1410_basic_14_6_txt;
        private string w_1420_amin_14_7;
        private string w_1430_ft_basic_txt;
        private string w_1440_power_txt;
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
        private string w_1550_case_text5;
        private string w_1550_case_text6;
        private string w_1550_case_text7;
        private string w_1550_case_text8;
        private string w_1560_spell_amount;
        private string w_1580_payment_due_date;
        private string w_1850_1851_adjust_amt;
        private string w_1872_A4;
        private string w_1873_A4_mass_print;
        private string w_2030_barcode_a4;
        private string billSeqNo;

        private string special1580Text;


        [DataMember(Order=1)]
        public string W_30_invoice_no
        {
            get { return w_30_invoice_no; }
            set { w_30_invoice_no = value; }
        }


        [DataMember(Order=2)]
        public string W_40_sname
        {
            get { return w_40_sname; }
            set { w_40_sname = value; }
        }


        [DataMember(Order=3)]
        public string W_50_day
        {
            get { return w_50_day; }
            set { w_50_day = value; }
        }


        [DataMember(Order=4)]
        public string W_60_month
        {
            get { return w_60_month; }
            set { w_60_month = value; }
        }


        [DataMember(Order=5)]
        public string W_70_year
        {
            get { return w_70_year; }
            set { w_70_year = value; }
        }


        [DataMember(Order=6)]
        public string W_80_cust_name1_name2
        {
            get { return w_80_cust_name1_name2; }
            set { w_80_cust_name1_name2 = value; }
        }


        [DataMember(Order=7)]
        public string W_130_period
        {
            get { return w_130_period; }
            set { w_130_period = value; }
        }


        [DataMember(Order=8)]
        public string W_140_reg
        {
            get { return w_140_reg; }
            set { w_140_reg = value; }
        }


        [DataMember(Order=9)]
        public string W_150_contract
        {
            get { return w_150_contract; }
            set { w_150_contract = value; }
        }


        [DataMember(Order=10)]
        public string W_160_device
        {
            get { return w_160_device; }
            set { w_160_device = value; }
        }


        [DataMember(Order=11)]
        public string W_170_rate_cat
        {
            get { return w_170_rate_cat; }
            set { w_170_rate_cat = value; }
        }


        [DataMember(Order=12)]
        public string W_171_ettat_code
        {
            get { return w_171_ettat_code; }
            set { w_171_ettat_code = value; }
        }


        [DataMember(Order=13)]
        public string W_180_voltage
        {
            get { return w_180_voltage; }
            set { w_180_voltage = value; }
        }


        [DataMember(Order=14)]
        public string W_190_multi_t
        {
            get { return w_190_multi_t; }
            set { w_190_multi_t = value; }
        }


        [DataMember(Order=15)]
        public string W_200_mr_date
        {
            get { return w_200_mr_date; }
            set { w_200_mr_date = value; }
        }


        [DataMember(Order=16)]
        public string W_500_txt6
        {
            get { return w_500_txt6; }
            set { w_500_txt6 = value; }
        }


        [DataMember(Order=17)]
        public string W_510_mr_kw_6_1_txt
        {
            get { return w_510_mr_kw_6_1_txt; }
            set { w_510_mr_kw_6_1_txt = value; }
        }


        [DataMember(Order=18)]
        public string W_520_mr_kw_6_2_txt
        {
            get { return w_520_mr_kw_6_2_txt; }
            set { w_520_mr_kw_6_2_txt = value; }
        }


        [DataMember(Order=19)]
        public string W_530_mr_kw_6_3_txt
        {
            get { return w_530_mr_kw_6_3_txt; }
            set { w_530_mr_kw_6_3_txt = value; }
        }


        [DataMember(Order=20)]
        public string W_540_mr_kw_6_4_txt
        {
            get { return w_540_mr_kw_6_4_txt; }
            set { w_540_mr_kw_6_4_txt = value; }
        }


        [DataMember(Order=21)]
        public string W_550_mr_kw_6_5
        {
            get { return w_550_mr_kw_6_5; }
            set { w_550_mr_kw_6_5 = value; }
        }


        [DataMember(Order=22)]
        public string W_555_device_6_7
        {
            get { return w_555_device_6_7; }
            set { w_555_device_6_7 = value; }
        }


        [DataMember(Order=23)]
        public string W_560_mr_kw_7_1_txt
        {
            get { return w_560_mr_kw_7_1_txt; }
            set { w_560_mr_kw_7_1_txt = value; }
        }


        [DataMember(Order=24)]
        public string W_570_mr_kw_7_2_txt
        {
            get { return w_570_mr_kw_7_2_txt; }
            set { w_570_mr_kw_7_2_txt = value; }
        }


        [DataMember(Order=25)]
        public string W_580_mr_kw_7_3_txt
        {
            get { return w_580_mr_kw_7_3_txt; }
            set { w_580_mr_kw_7_3_txt = value; }
        }


        [DataMember(Order=26)]
        public string W_590_mr_kw_7_4_txt
        {
            get { return w_590_mr_kw_7_4_txt; }
            set { w_590_mr_kw_7_4_txt = value; }
        }


        [DataMember(Order=27)]
        public string W_600_mr_kw_7_5
        {
            get { return w_600_mr_kw_7_5; }
            set { w_600_mr_kw_7_5 = value; }
        }


        [DataMember(Order=28)]
        public string W_610_mr_kw_8_1_txt
        {
            get { return w_610_mr_kw_8_1_txt; }
            set { w_610_mr_kw_8_1_txt = value; }
        }


        [DataMember(Order=29)]
        public string W_620_mr_kw_8_2_txt
        {
            get { return w_620_mr_kw_8_2_txt; }
            set { w_620_mr_kw_8_2_txt = value; }
        }


        [DataMember(Order=30)]
        public string W_630_mr_kw_8_3_txt
        {
            get { return w_630_mr_kw_8_3_txt; }
            set { w_630_mr_kw_8_3_txt = value; }
        }


        [DataMember(Order=31)]
        public string W_640_mr_kw_8_5
        {
            get { return w_640_mr_kw_8_5; }
            set { w_640_mr_kw_8_5 = value; }
        }


        [DataMember(Order=32)]
        public string W_650_mr_kw_9_1_txt
        {
            get { return w_650_mr_kw_9_1_txt; }
            set { w_650_mr_kw_9_1_txt = value; }
        }


        [DataMember(Order=33)]
        public string W_660_mr_kw_9_2_txt
        {
            get { return w_660_mr_kw_9_2_txt; }
            set { w_660_mr_kw_9_2_txt = value; }
        }


        [DataMember(Order=34)]
        public string W_670_mr_kw_9_3_txt
        {
            get { return w_670_mr_kw_9_3_txt; }
            set { w_670_mr_kw_9_3_txt = value; }
        }


        [DataMember(Order=35)]
        public string W_680_mr_kw_9_4_txt
        {
            get { return w_680_mr_kw_9_4_txt; }
            set { w_680_mr_kw_9_4_txt = value; }
        }


        [DataMember(Order=36)]
        public string W_690_mr_kw_9_5
        {
            get { return w_690_mr_kw_9_5; }
            set { w_690_mr_kw_9_5 = value; }
        }


        [DataMember(Order=37)]
        public string W_695_device_9_7
        {
            get { return w_695_device_9_7; }
            set { w_695_device_9_7 = value; }
        }


        [DataMember(Order=38)]
        public string W_700_mr_kw_10_1_txt
        {
            get { return w_700_mr_kw_10_1_txt; }
            set { w_700_mr_kw_10_1_txt = value; }
        }


        [DataMember(Order=39)]
        public string W_710_mr_kw_10_2_txt
        {
            get { return w_710_mr_kw_10_2_txt; }
            set { w_710_mr_kw_10_2_txt = value; }
        }


        [DataMember(Order=40)]
        public string W_720_mr_kw_10_3_txt
        {
            get { return w_720_mr_kw_10_3_txt; }
            set { w_720_mr_kw_10_3_txt = value; }
        }


        [DataMember(Order=41)]
        public string W_730_mr_kw_10_4_txt
        {
            get { return w_730_mr_kw_10_4_txt; }
            set { w_730_mr_kw_10_4_txt = value; }
        }


        [DataMember(Order=42)]
        public string W_740_mr_kw_10_5
        {
            get { return w_740_mr_kw_10_5; }
            set { w_740_mr_kw_10_5 = value; }
        }


        [DataMember(Order=43)]
        public string W_750_mr_kw_11_1_txt
        {
            get { return w_750_mr_kw_11_1_txt; }
            set { w_750_mr_kw_11_1_txt = value; }
        }


        [DataMember(Order=44)]
        public string W_760_mr_kw_11_2_txt
        {
            get { return w_760_mr_kw_11_2_txt; }
            set { w_760_mr_kw_11_2_txt = value; }
        }


        [DataMember(Order=45)]
        public string W_770_mr_kw_11_3_txt
        {
            get { return w_770_mr_kw_11_3_txt; }
            set { w_770_mr_kw_11_3_txt = value; }
        }


        [DataMember(Order=46)]
        public string W_780_mr_kw_11_5
        {
            get { return w_780_mr_kw_11_5; }
            set { w_780_mr_kw_11_5 = value; }
        }


        [DataMember(Order=47)]
        public string W_790_mr_kw_12_1_txt
        {
            get { return w_790_mr_kw_12_1_txt; }
            set { w_790_mr_kw_12_1_txt = value; }
        }


        [DataMember(Order=48)]
        public string W_800_mr_kw_12_2_txt
        {
            get { return w_800_mr_kw_12_2_txt; }
            set { w_800_mr_kw_12_2_txt = value; }
        }


        [DataMember(Order=49)]
        public string W_810_mr_kw_12_3_txt
        {
            get { return w_810_mr_kw_12_3_txt; }
            set { w_810_mr_kw_12_3_txt = value; }
        }


        [DataMember(Order=50)]
        public string W_820_mr_kw_12_4_txt
        {
            get { return w_820_mr_kw_12_4_txt; }
            set { w_820_mr_kw_12_4_txt = value; }
        }


        [DataMember(Order=51)]
        public string W_830_mr_kw_12_5
        {
            get { return w_830_mr_kw_12_5; }
            set { w_830_mr_kw_12_5 = value; }
        }


        [DataMember(Order=52)]
        public string W_835_device_12_7
        {
            get { return w_835_device_12_7; }
            set { w_835_device_12_7 = value; }
        }


        [DataMember(Order=53)]
        public string W_840_mr_kw_13_1_txt
        {
            get { return w_840_mr_kw_13_1_txt; }
            set { w_840_mr_kw_13_1_txt = value; }
        }


        [DataMember(Order=54)]
        public string W_850_mr_kw_13_2_txt
        {
            get { return w_850_mr_kw_13_2_txt; }
            set { w_850_mr_kw_13_2_txt = value; }
        }


        [DataMember(Order=55)]
        public string W_860_mr_kw_13_3_txt
        {
            get { return w_860_mr_kw_13_3_txt; }
            set { w_860_mr_kw_13_3_txt = value; }
        }


        [DataMember(Order=56)]
        public string W_870_mr_kw_13_4_txt
        {
            get { return w_870_mr_kw_13_4_txt; }
            set { w_870_mr_kw_13_4_txt = value; }
        }


        [DataMember(Order=57)]
        public string W_890_mr_kw_13_5
        {
            get { return w_890_mr_kw_13_5; }
            set { w_890_mr_kw_13_5 = value; }
        }


        [DataMember(Order=58)]
        public string W_900_mr_kw_14_1_txt
        {
            get { return w_900_mr_kw_14_1_txt; }
            set { w_900_mr_kw_14_1_txt = value; }
        }


        [DataMember(Order=59)]
        public string W_910_mr_kw_14_2_txt
        {
            get { return w_910_mr_kw_14_2_txt; }
            set { w_910_mr_kw_14_2_txt = value; }
        }


        [DataMember(Order=60)]
        public string W_920_mr_kw_14_3_txt
        {
            get { return w_920_mr_kw_14_3_txt; }
            set { w_920_mr_kw_14_3_txt = value; }
        }


        [DataMember(Order=61)]
        public string W_930_mr_kw_14_5
        {
            get { return w_930_mr_kw_14_5; }
            set { w_930_mr_kw_14_5 = value; }
        }


        [DataMember(Order=62)]
        public string W_940_mr_kw_15_1_txt
        {
            get { return w_940_mr_kw_15_1_txt; }
            set { w_940_mr_kw_15_1_txt = value; }
        }


        [DataMember(Order=63)]
        public string W_950_mr_kw_15_2_txt
        {
            get { return w_950_mr_kw_15_2_txt; }
            set { w_950_mr_kw_15_2_txt = value; }
        }


        [DataMember(Order=64)]
        public string W_960_mr_kw_15_3_txt
        {
            get { return w_960_mr_kw_15_3_txt; }
            set { w_960_mr_kw_15_3_txt = value; }
        }


        [DataMember(Order=65)]
        public string W_970_mr_kw_15_4_txt
        {
            get { return w_970_mr_kw_15_4_txt; }
            set { w_970_mr_kw_15_4_txt = value; }
        }


        [DataMember(Order=66)]
        public string W_980_mr_kw_15_5
        {
            get { return w_980_mr_kw_15_5; }
            set { w_980_mr_kw_15_5 = value; }
        }


        [DataMember(Order=67)]
        public string W_985_device_15_7
        {
            get { return w_985_device_15_7; }
            set { w_985_device_15_7 = value; }
        }


        [DataMember(Order=68)]
        public string W_990_mr_kw_16_1_txt
        {
            get { return w_990_mr_kw_16_1_txt; }
            set { w_990_mr_kw_16_1_txt = value; }
        }


        [DataMember(Order=69)]
        public string W_1000_mr_kw_16_2_txt
        {
            get { return w_1000_mr_kw_16_2_txt; }
            set { w_1000_mr_kw_16_2_txt = value; }
        }


        [DataMember(Order=70)]
        public string W_1010_mr_kw_16_3_txt
        {
            get { return w_1010_mr_kw_16_3_txt; }
            set { w_1010_mr_kw_16_3_txt = value; }
        }


        [DataMember(Order=71)]
        public string W_1020_mr_kw_16_4_txt
        {
            get { return w_1020_mr_kw_16_4_txt; }
            set { w_1020_mr_kw_16_4_txt = value; }
        }


        [DataMember(Order=72)]
        public string W_1030_mr_kw_16_5
        {
            get { return w_1030_mr_kw_16_5; }
            set { w_1030_mr_kw_16_5 = value; }
        }


        [DataMember(Order=73)]
        public string W_1040_mr_kw_17_1_txt
        {
            get { return w_1040_mr_kw_17_1_txt; }
            set { w_1040_mr_kw_17_1_txt = value; }
        }


        [DataMember(Order=74)]
        public string W_1050_mr_kw_17_2_txt
        {
            get { return w_1050_mr_kw_17_2_txt; }
            set { w_1050_mr_kw_17_2_txt = value; }
        }


        [DataMember(Order=75)]
        public string W_1060_mr_kw_17_3_txt
        {
            get { return w_1060_mr_kw_17_3_txt; }
            set { w_1060_mr_kw_17_3_txt = value; }
        }


        [DataMember(Order=76)]
        public string W_1070_mr_kw_17_5
        {
            get { return w_1070_mr_kw_17_5; }
            set { w_1070_mr_kw_17_5 = value; }
        }


        [DataMember(Order=77)]
        public string W_1080_service_txt
        {
            get { return w_1080_service_txt; }
            set { w_1080_service_txt = value; }
        }


        [DataMember(Order=78)]
        public string W_1090_service_support_txt
        {
            get { return w_1090_service_support_txt; }
            set { w_1090_service_support_txt = value; }
        }


        [DataMember(Order=79)]
        public string W_1100_sum_service_support_txt
        {
            get { return w_1100_sum_service_support_txt; }
            set { w_1100_sum_service_support_txt = value; }
        }


        [DataMember(Order=80)]
        public string W_1110_basic_19_1_txt
        {
            get { return w_1110_basic_19_1_txt; }
            set { w_1110_basic_19_1_txt = value; }
        }


        [DataMember(Order=81)]
        public string W_1120_description
        {
            get { return w_1120_description; }
            set { w_1120_description = value; }
        }


        [DataMember(Order=82)]
        public string W_1130_kvar_20_1_txt
        {
            get { return w_1130_kvar_20_1_txt; }
            set { w_1130_kvar_20_1_txt = value; }
        }


        [DataMember(Order=83)]
        public string W_1140_kvar_20_2_txt
        {
            get { return w_1140_kvar_20_2_txt; }
            set { w_1140_kvar_20_2_txt = value; }
        }


        [DataMember(Order=84)]
        public string W_1150_kvar_20_3_txt
        {
            get { return w_1150_kvar_20_3_txt; }
            set { w_1150_kvar_20_3_txt = value; }
        }


        [DataMember(Order=85)]
        public string W_1160_kvar_20_4_txt
        {
            get { return w_1160_kvar_20_4_txt; }
            set { w_1160_kvar_20_4_txt = value; }
        }


        [DataMember(Order=86)]
        public string W_1170_kvar_21_1_txt
        {
            get { return w_1170_kvar_21_1_txt; }
            set { w_1170_kvar_21_1_txt = value; }
        }


        [DataMember(Order=87)]
        public string W_1180_kvar_21_2_txt
        {
            get { return w_1180_kvar_21_2_txt; }
            set { w_1180_kvar_21_2_txt = value; }
        }


        [DataMember(Order=88)]
        public string W_1190_kvar_21_3_txt
        {
            get { return w_1190_kvar_21_3_txt; }
            set { w_1190_kvar_21_3_txt = value; }
        }


        [DataMember(Order=89)]
        public string W_1200_kvar_21_4_txt
        {
            get { return w_1200_kvar_21_4_txt; }
            set { w_1200_kvar_21_4_txt = value; }
        }


        [DataMember(Order=90)]
        public string W_1210_gen_kw_amt_txt
        {
            get { return w_1210_gen_kw_amt_txt; }
            set { w_1210_gen_kw_amt_txt = value; }
        }


        [DataMember(Order=91)]
        public string W_1220_trans_kw_amt_txt
        {
            get { return w_1220_trans_kw_amt_txt; }
            set { w_1220_trans_kw_amt_txt = value; }
        }


        [DataMember(Order=92)]
        public string W_1230_dist_kw_amt_txt
        {
            get { return w_1230_dist_kw_amt_txt; }
            set { w_1230_dist_kw_amt_txt = value; }
        }


        [DataMember(Order=93)]
        public string W_1240_gen_kwh_amt_txt
        {
            get { return w_1240_gen_kwh_amt_txt; }
            set { w_1240_gen_kwh_amt_txt = value; }
        }


        [DataMember(Order=94)]
        public string W_1250_trans_kwh_amt_txt
        {
            get { return w_1250_trans_kwh_amt_txt; }
            set { w_1250_trans_kwh_amt_txt = value; }
        }


        [DataMember(Order=95)]
        public string W_1260_dist_kwh_amt_txt
        {
            get { return w_1260_dist_kwh_amt_txt; }
            set { w_1260_dist_kwh_amt_txt = value; }
        }


        [DataMember(Order=96)]
        public string W_1270_dis_supp_amt_txt
        {
            get { return w_1270_dis_supp_amt_txt; }
            set { w_1270_dis_supp_amt_txt = value; }
        }


        [DataMember(Order=97)]
        public string W_1280_gen_ft_amt_txt
        {
            get { return w_1280_gen_ft_amt_txt; }
            set { w_1280_gen_ft_amt_txt = value; }
        }


        [DataMember(Order=98)]
        public string W_1290_trans_ft_amt_txt
        {
            get { return w_1290_trans_ft_amt_txt; }
            set { w_1290_trans_ft_amt_txt = value; }
        }


        [DataMember(Order=99)]
        public string W_1300_dist_ft_amt_txt
        {
            get { return w_1300_dist_ft_amt_txt; }
            set { w_1300_dist_ft_amt_txt = value; }
        }


        [DataMember(Order=100)]
        public string W_1350_ftgen_txt
        {
            get { return w_1350_ftgen_txt; }
            set { w_1350_ftgen_txt = value; }
        }


        [DataMember(Order=101)]
        public string W_1360_fttrn_txt
        {
            get { return w_1360_fttrn_txt; }
            set { w_1360_fttrn_txt = value; }
        }


        [DataMember(Order=102)]
        public string W_1370_ftdis_txt
        {
            get { return w_1370_ftdis_txt; }
            set { w_1370_ftdis_txt = value; }
        }


        [DataMember(Order=103)]
        public string W_1380_fttot_txt
        {
            get { return w_1380_fttot_txt; }
            set { w_1380_fttot_txt = value; }
        }


        [DataMember(Order=104)]
        public string W_1390_ftunit_txt
        {
            get { return w_1390_ftunit_txt; }
            set { w_1390_ftunit_txt = value; }
        }


        [DataMember(Order=105)]
        public string W_1400_ftchg_txt
        {
            get { return w_1400_ftchg_txt; }
            set { w_1400_ftchg_txt = value; }
        }


        [DataMember(Order=106)]
        public string W_1410_basic_14_6_txt
        {
            get { return w_1410_basic_14_6_txt; }
            set { w_1410_basic_14_6_txt = value; }
        }


        [DataMember(Order=107)]
        public string W_1420_amin_14_7
        {
            get { return w_1420_amin_14_7; }
            set { w_1420_amin_14_7 = value; }
        }


        [DataMember(Order=108)]
        public string W_1430_ft_basic_txt
        {
            get { return w_1430_ft_basic_txt; }
            set { w_1430_ft_basic_txt = value; }
        }


        [DataMember(Order=109)]
        public string W_1440_power_txt
        {
            get { return w_1440_power_txt; }
            set { w_1440_power_txt = value; }
        }


        [DataMember(Order=110)]
        public string W_1450_mr_kw_17_6_txt
        {
            get { return w_1450_mr_kw_17_6_txt; }
            set { w_1450_mr_kw_17_6_txt = value; }
        }


        [DataMember(Order=111)]
        public string W_1460_mr_kw_17_7
        {
            get { return w_1460_mr_kw_17_7; }
            set { w_1460_mr_kw_17_7 = value; }
        }


        [DataMember(Order=112)]
        public string W_1480_net_before_vat_txt
        {
            get { return w_1480_net_before_vat_txt; }
            set { w_1480_net_before_vat_txt = value; }
        }


        [DataMember(Order=113)]
        public string W_1490_tax_rate_txt
        {
            get { return w_1490_tax_rate_txt; }
            set { w_1490_tax_rate_txt = value; }
        }


        [DataMember(Order=114)]
        public string W_1500_tax_amount_txt
        {
            get { return w_1500_tax_amount_txt; }
            set { w_1500_tax_amount_txt = value; }
        }


        [DataMember(Order=115)]
        public string W_1510_total_amnt_txt
        {
            get { return w_1510_total_amnt_txt; }
            set { w_1510_total_amnt_txt = value; }
        }


        [DataMember(Order=116)]
        public string W_1550_case_text1
        {
            get { return w_1550_case_text1; }
            set { w_1550_case_text1 = value; }
        }


        [DataMember(Order=117)]
        public string W_1550_case_text2
        {
            get { return w_1550_case_text2; }
            set { w_1550_case_text2 = value; }
        }


        [DataMember(Order=118)]
        public string W_1550_case_text3
        {
            get { return w_1550_case_text3; }
            set { w_1550_case_text3 = value; }
        }


        [DataMember(Order=119)]
        public string W_1550_case_text4
        {
            get { return w_1550_case_text4; }
            set { w_1550_case_text4 = value; }
        }


        [DataMember(Order=120)]
        public string W_1550_case_text5
        {
            get { return w_1550_case_text5; }
            set { w_1550_case_text5 = value; }
        }


        [DataMember(Order=121)]
        public string W_1550_case_text6
        {
            get { return w_1550_case_text6; }
            set { w_1550_case_text6 = value; }
        }


        [DataMember(Order=122)]
        public string W_1550_case_text7
        {
            get { return w_1550_case_text7; }
            set { w_1550_case_text7 = value; }
        }


        [DataMember(Order=123)]
        public string W_1550_case_text8
        {
            get { return w_1550_case_text8; }
            set { w_1550_case_text8 = value; }
        }
    

        [DataMember(Order=124)]
        public string W_1560_spell_amount
        {
            get { return w_1560_spell_amount; }
            set { w_1560_spell_amount = value; }
        }


        [DataMember(Order=125)]
        public string W_1580_payment_due_date
        {
            get { return w_1580_payment_due_date; }
            set { w_1580_payment_due_date = value; }
        }


        [DataMember(Order=126)]
        public string W_1850_1851_adjust_amt
        {
            get { return w_1850_1851_adjust_amt; }
            set { w_1850_1851_adjust_amt = value; }
        }


        [DataMember(Order=127)]
        public string W_1872_A4
        {
            get { return w_1872_A4; }
            set { w_1872_A4 = value; }
        }


        [DataMember(Order=128)]
        public string W_1873_A4_mass_print
        {
            get { return w_1873_A4_mass_print; }
            set { w_1873_A4_mass_print = value; }
        }


        [DataMember(Order=129)]
        public string W_2030_barcode_a4
        {
            get { return w_2030_barcode_a4; }
            set { w_2030_barcode_a4 = value; }
        }


        [DataMember(Order=130)]
        public string BillSeqNo
        {
            get { return billSeqNo; }
            set { billSeqNo = value; }
        }


        [DataMember(Order=131)]
        public string Special1580Text
        {
            get { return special1580Text; }
            set { special1580Text = value; }
        }
        
    }
}
