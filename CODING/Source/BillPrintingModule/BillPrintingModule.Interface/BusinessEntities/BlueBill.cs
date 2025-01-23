using System.Runtime.Serialization;

namespace PEA.BPM.BillPrintingModule.Interface.BusinessEntities
{
    [DataContract]
    public class BlueBill
    {
        private string w_10_invoice_no;
        private string w_20_buss_place;
        private string w_90_cust_name1;
        private string w_90_cust_name2;
        private string w_130_period;
        private string w_140_reg;
        private string w_150_contract;
        private string w_160_device;
        private string w_170_rate_cat;
        private string w_171_ettat_code;
        private string w_200_mr_date;
        private string w_216_address;
        private string w_217_address;
        private string w_218_address;
        private string w_221_address;
        private string w_222_address;
        private string w_223_address;
        private string w_230_post_code;
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
        private string w_470_zwstand_1_txt;
        private string w_480_zwstvor_1_txt;
        private string w_490_consumption_txt;
        private string w_500_txt6;
        private string w_1310_amount_txt;
        private string w_1320_foreign_amt;
        private string w_1330_foreign_txt;
        private string w_1340_foreign_uit;
        private string w_1345_blue_txt1;
        private string w_1380_fttot_txt;
        private string w_1381_ft_peiod_txt;
        private string w_1400_ftchg_txt;
        private string w_1450_mr_kw_17_6_txt;
        private string w_1460_mr_kw_17_7;
        private string w_1470_baht;
        private string w_1480_net_before_vat_txt;
        private string w_1485_net_before_vat_left;
        private string w_1490_tax_rate_txt;
        private string w_1500_tax_amount_txt;
        private string w_1505_tax_amount_left;
        private string w_1510_total_amnt_txt;
        private string w_1550_case_text1;
        private string w_1550_case_text2;
        private string w_1550_case_text3;
        private string w_1550_case_text4;
        private string w_1550_case_text7;
        private string w_1550_case_text8;
        private string w_1580_payment_due_date;
        private string w_1590_barcode1;
        private string w_1600_barcode2;
        private string w_1880_payment_lot;
        private string w_1950_collector_id;
        private string billSeqNo;
        private string billControllerId;
        private string w_2070_taxid_p;
        private string w_2080_taxid_c;

        //#ISSUE NEW FORM 30-NOVEMBER-2017
        private string w_2081_taxid;
        private string w_2082_taxid;
        private string w_2120_pay_adrc;
        private string w_2130_off_tel;
        private string w_2140_oth_dis1;
        private string w_2150_oth_dis2;
        private string w_2160_oth_dis3;
        private string w_2170_sec_dis1;
        private string w_2180_sec_dis2;
        private string w_2190_sec_dis3;
        private string w_2200_gtot_amn;
        private string w_2210_spell_gtot;
        private string w_2220_mr_date_m1;
        private string w_2230_mr_date_m2;
        private string w_2240_mr_date_m3;
        private string w_2250_mr_date_m4;
        private string w_2260_mr_date_m5;
        private string w_2270_mr_date_m6;
        private string w_2280_unit_m1;
        private string w_2290_unit_m2;
        private string w_2300_unit_m3;
        private string w_2310_unit_m4;
        private string w_2320_unit_m5;
        private string w_2330_unit_m6;

        //*****************************
        private string w_1840_mru;
        private string w_1110_basic_19_1_txt;
        private string w_40_sname;
        private string w_190_multi_n;
        private string w_1100_sum_service_support_txt;
        //**************************** 
        //END #ISSUE

        #region #ISSUE#BLAN2019

        private string w_2340_rec_doc;
        private string w_2350_rec_date;
        private string w_2360_unit_rec;
        private string w_2370_oth_dis4;
        private string w_2380_oth_dis5;
        private string w_2390_oth_dis6;
        private string w_2400_oth_dis7;
        private string w_2410_oth_dis8;
        private string w_2420_oth_dis9;
        private string w_2430_rec_text1;
        private string w_2440_rec_text2;
        private string w_2450_rec_text3;
        private string w_01_print_doc;

        #endregion

        #region GET & SET
        [DataMember(Order = 1)]
        public string W_10_invoice_no
        {
            get { return w_10_invoice_no; }
            set { w_10_invoice_no = value; }
        }


        [DataMember(Order = 2)]
        public string W_20_buss_place
        {
            get { return w_20_buss_place; }
            set { w_20_buss_place = value; }
        }


        [DataMember(Order = 3)]
        public string W_90_cust_name1
        {
            get { return w_90_cust_name1; }
            set { w_90_cust_name1 = value; }
        }


        [DataMember(Order = 4)]
        public string W_90_cust_name2
        {
            get { return w_90_cust_name2; }
            set { w_90_cust_name2 = value; }
        }


        [DataMember(Order = 5)]
        public string W_130_period
        {
            get { return w_130_period; }
            set { w_130_period = value; }
        }


        [DataMember(Order = 6)]
        public string W_140_reg
        {
            get { return w_140_reg; }
            set { w_140_reg = value; }
        }


        [DataMember(Order = 7)]
        public string W_150_contract
        {
            get { return w_150_contract; }
            set { w_150_contract = value; }
        }


        [DataMember(Order = 8)]
        public string W_160_device
        {
            get { return w_160_device; }
            set { w_160_device = value; }
        }


        [DataMember(Order = 9)]
        public string W_170_rate_cat
        {
            get { return w_170_rate_cat; }
            set { w_170_rate_cat = value; }
        }


        [DataMember(Order = 10)]
        public string W_171_ettat_code
        {
            get { return w_171_ettat_code; }
            set { w_171_ettat_code = value; }
        }


        [DataMember(Order = 11)]
        public string W_200_mr_date
        {
            get { return w_200_mr_date; }
            set { w_200_mr_date = value; }
        }


        [DataMember(Order = 12)]
        public string W_216_address
        {
            get { return w_216_address; }
            set { w_216_address = value; }
        }


        [DataMember(Order = 13)]
        public string W_217_address
        {
            get { return w_217_address; }
            set { w_217_address = value; }
        }


        [DataMember(Order = 14)]
        public string W_218_address
        {
            get { return w_218_address; }
            set { w_218_address = value; }
        }


        [DataMember(Order = 15)]
        public string W_221_address
        {
            get { return w_221_address; }
            set { w_221_address = value; }
        }


        [DataMember(Order = 16)]
        public string W_222_address
        {
            get { return w_222_address; }
            set { w_222_address = value; }
        }


        [DataMember(Order = 17)]
        public string W_223_address
        {
            get { return w_223_address; }
            set { w_223_address = value; }
        }


        [DataMember(Order = 18)]
        public string W_230_post_code
        {
            get { return w_230_post_code; }
            set { w_230_post_code = value; }
        }


        [DataMember(Order = 19)]
        public string W_255_device_1
        {
            get { return w_255_device_1; }
            set { w_255_device_1 = value; }
        }


        [DataMember(Order = 20)]
        public string W_260_zwstand_1_txt
        {
            get { return w_260_zwstand_1_txt; }
            set { w_260_zwstand_1_txt = value; }
        }


        [DataMember(Order = 21)]
        public string W_270_zwstvor_1_txt
        {
            get { return w_270_zwstvor_1_txt; }
            set { w_270_zwstvor_1_txt = value; }
        }


        [DataMember(Order = 22)]
        public string W_280_umwfakt_1_txt
        {
            get { return w_280_umwfakt_1_txt; }
            set { w_280_umwfakt_1_txt = value; }
        }


        [DataMember(Order = 23)]
        public string W_290_abrmenge_1_txt
        {
            get { return w_290_abrmenge_1_txt; }
            set { w_290_abrmenge_1_txt = value; }
        }


        [DataMember(Order = 24)]
        public string W_295_device_2
        {
            get { return w_295_device_2; }
            set { w_295_device_2 = value; }
        }


        [DataMember(Order = 25)]
        public string W_300_zwstand_2_txt
        {
            get { return w_300_zwstand_2_txt; }
            set { w_300_zwstand_2_txt = value; }
        }


        [DataMember(Order = 26)]
        public string W_310_zwstvor_2_txt
        {
            get { return w_310_zwstvor_2_txt; }
            set { w_310_zwstvor_2_txt = value; }
        }


        [DataMember(Order = 27)]
        public string W_320_umwfakt_2_txt
        {
            get { return w_320_umwfakt_2_txt; }
            set { w_320_umwfakt_2_txt = value; }
        }


        [DataMember(Order = 28)]
        public string W_330_abrmenge_2_txt
        {
            get { return w_330_abrmenge_2_txt; }
            set { w_330_abrmenge_2_txt = value; }
        }


        [DataMember(Order = 29)]
        public string W_340_peak_txt
        {
            get { return w_340_peak_txt; }
            set { w_340_peak_txt = value; }
        }


        [DataMember(Order = 30)]
        public string W_350_dash_txt
        {
            get { return w_350_dash_txt; }
            set { w_350_dash_txt = value; }
        }


        [DataMember(Order = 31)]
        public string W_355_device_3
        {
            get { return w_355_device_3; }
            set { w_355_device_3 = value; }
        }


        [DataMember(Order = 32)]
        public string W_360_zwstand_3_txt
        {
            get { return w_360_zwstand_3_txt; }
            set { w_360_zwstand_3_txt = value; }
        }


        [DataMember(Order = 33)]
        public string W_370_zwstvor_3_txt
        {
            get { return w_370_zwstvor_3_txt; }
            set { w_370_zwstvor_3_txt = value; }
        }


        [DataMember(Order = 34)]
        public string W_380_umwfakt_3_txt
        {
            get { return w_380_umwfakt_3_txt; }
            set { w_380_umwfakt_3_txt = value; }
        }


        [DataMember(Order = 35)]
        public string W_390_abrmenge_3_txt
        {
            get { return w_390_abrmenge_3_txt; }
            set { w_390_abrmenge_3_txt = value; }
        }


        [DataMember(Order = 36)]
        public string W_400_off_peak_txt
        {
            get { return w_400_off_peak_txt; }
            set { w_400_off_peak_txt = value; }
        }


        [DataMember(Order = 37)]
        public string W_410_ene_txt
        {
            get { return w_410_ene_txt; }
            set { w_410_ene_txt = value; }
        }


        [DataMember(Order = 38)]
        public string W_415_device_4
        {
            get { return w_415_device_4; }
            set { w_415_device_4 = value; }
        }


        [DataMember(Order = 39)]
        public string W_420_zwstand_4_txt
        {
            get { return w_420_zwstand_4_txt; }
            set { w_420_zwstand_4_txt = value; }
        }


        [DataMember(Order = 40)]
        public string W_430_zwstvor_4_txt
        {
            get { return w_430_zwstvor_4_txt; }
            set { w_430_zwstvor_4_txt = value; }
        }


        [DataMember(Order = 41)]
        public string W_440_umwfakt_4_txt
        {
            get { return w_440_umwfakt_4_txt; }
            set { w_440_umwfakt_4_txt = value; }
        }


        [DataMember(Order = 42)]
        public string W_450_abrmenge_4_txt
        {
            get { return w_450_abrmenge_4_txt; }
            set { w_450_abrmenge_4_txt = value; }
        }


        [DataMember(Order = 43)]
        public string W_460_hol_txt
        {
            get { return w_460_hol_txt; }
            set { w_460_hol_txt = value; }
        }


        [DataMember(Order = 44)]
        public string W_470_zwstand_1_txt
        {
            get { return w_470_zwstand_1_txt; }
            set { w_470_zwstand_1_txt = value; }
        }


        [DataMember(Order = 45)]
        public string W_480_zwstvor_1_txt
        {
            get { return w_480_zwstvor_1_txt; }
            set { w_480_zwstvor_1_txt = value; }
        }


        [DataMember(Order = 46)]
        public string W_490_consumption_txt
        {
            get { return w_490_consumption_txt; }
            set { w_490_consumption_txt = value; }
        }


        [DataMember(Order = 47)]
        public string W_500_txt6
        {
            get { return w_500_txt6; }
            set { w_500_txt6 = value; }
        }


        [DataMember(Order = 48)]
        public string W_1310_amount_txt
        {
            get { return w_1310_amount_txt; }
            set { w_1310_amount_txt = value; }
        }


        [DataMember(Order = 49)]
        public string W_1320_foreign_amt
        {
            get { return w_1320_foreign_amt; }
            set { w_1320_foreign_amt = value; }
        }


        [DataMember(Order = 50)]
        public string W_1330_foreign_txt
        {
            get { return w_1330_foreign_txt; }
            set { w_1330_foreign_txt = value; }
        }


        [DataMember(Order = 51)]
        public string W_1340_foreign_uit
        {
            get { return w_1340_foreign_uit; }
            set { w_1340_foreign_uit = value; }
        }


        [DataMember(Order = 52)]
        public string W_1345_blue_txt1
        {
            get { return w_1345_blue_txt1; }
            set { w_1345_blue_txt1 = value; }
        }


        [DataMember(Order = 53)]
        public string W_1380_fttot_txt
        {
            get { return w_1380_fttot_txt; }
            set { w_1380_fttot_txt = value; }
        }


        [DataMember(Order = 54)]
        public string W_1381_ft_peiod_txt
        {
            get { return w_1381_ft_peiod_txt; }
            set { w_1381_ft_peiod_txt = value; }
        }


        [DataMember(Order = 55)]
        public string W_1400_ftchg_txt
        {
            get { return w_1400_ftchg_txt; }
            set { w_1400_ftchg_txt = value; }
        }


        [DataMember(Order = 56)]
        public string W_1450_mr_kw_17_6_txt
        {
            get { return w_1450_mr_kw_17_6_txt; }
            set { w_1450_mr_kw_17_6_txt = value; }
        }


        [DataMember(Order = 57)]
        public string W_1460_mr_kw_17_7
        {
            get { return w_1460_mr_kw_17_7; }
            set { w_1460_mr_kw_17_7 = value; }
        }


        [DataMember(Order = 58)]
        public string W_1470_baht
        {
            get { return w_1470_baht; }
            set { w_1470_baht = value; }
        }


        [DataMember(Order = 59)]
        public string W_1480_net_before_vat_txt
        {
            get { return w_1480_net_before_vat_txt; }
            set { w_1480_net_before_vat_txt = value; }
        }


        [DataMember(Order = 60)]
        public string W_1485_net_before_vat_left
        {
            get { return w_1485_net_before_vat_left; }
            set { w_1485_net_before_vat_left = value; }
        }


        [DataMember(Order = 61)]
        public string W_1490_tax_rate_txt
        {
            get { return w_1490_tax_rate_txt; }
            set { w_1490_tax_rate_txt = value; }
        }


        [DataMember(Order = 62)]
        public string W_1500_tax_amount_txt
        {
            get { return w_1500_tax_amount_txt; }
            set { w_1500_tax_amount_txt = value; }
        }


        [DataMember(Order = 63)]
        public string W_1505_tax_amount_left
        {
            get { return w_1505_tax_amount_left; }
            set { w_1505_tax_amount_left = value; }
        }


        [DataMember(Order = 64)]
        public string W_1510_total_amnt_txt
        {
            get { return w_1510_total_amnt_txt; }
            set { w_1510_total_amnt_txt = value; }
        }


        [DataMember(Order = 65)]
        public string W_1550_case_text1
        {
            get { return w_1550_case_text1; }
            set { w_1550_case_text1 = value; }
        }


        [DataMember(Order = 66)]
        public string W_1550_case_text2
        {
            get { return w_1550_case_text2; }
            set { w_1550_case_text2 = value; }
        }


        [DataMember(Order = 67)]
        public string W_1550_case_text3
        {
            get { return w_1550_case_text3; }
            set { w_1550_case_text3 = value; }
        }


        [DataMember(Order = 68)]
        public string W_1550_case_text4
        {
            get { return w_1550_case_text4; }
            set { w_1550_case_text4 = value; }
        }


        [DataMember(Order = 69)]
        public string W_1580_payment_due_date
        {
            get { return w_1580_payment_due_date; }
            set { w_1580_payment_due_date = value; }
        }


        [DataMember(Order = 70)]
        public string W_1590_barcode1
        {
            get { return w_1590_barcode1; }
            set { w_1590_barcode1 = value; }
        }


        [DataMember(Order = 71)]
        public string W_1600_barcode2
        {
            get { return w_1600_barcode2; }
            set { w_1600_barcode2 = value; }
        }


        [DataMember(Order = 72)]
        public string W_1950_collector_id
        {
            get { return w_1950_collector_id; }
            set { w_1950_collector_id = value; }
        }


        [DataMember(Order = 73)]
        public string BillSeqNo
        {
            get { return billSeqNo; }
            set { billSeqNo = value; }
        }


        [DataMember(Order = 74)]
        public string BillControllerId
        {
            get { return billControllerId; }
            set { billControllerId = value; }
        }

        [DataMember(Order = 75)]
        public string W_1880_payment_lot
        {
            get { return w_1880_payment_lot; }
            set { w_1880_payment_lot = value; }
        }

        [DataMember(Order = 76)]
        public string W_1550_case_text7
        {
            get { return w_1550_case_text7; }
            set { w_1550_case_text7 = value; }
        }

        [DataMember(Order = 77)]
        public string W_1550_case_text8
        {
            get { return w_1550_case_text8; }
            set { w_1550_case_text8 = value; }
        }

        [DataMember(Order = 78)]
        public string W_2070_taxid_p
        {
            get { return w_2070_taxid_p; }
            set { w_2070_taxid_p = value; }
        }
        
        [DataMember(Order = 79)]
        public string W_2080_taxid_c
        {
            get { return w_2080_taxid_c; }
            set { w_2080_taxid_c = value; }
        }

        [DataMember(Order = 80)]
        public string W_2081_taxid
        {
            get { return w_2081_taxid; }
            set { w_2081_taxid = value; }
        }

        [DataMember(Order = 81)]
        public string W_2082_taxid
        {
            get { return w_2082_taxid; }
            set { w_2082_taxid = value; }
        }
        [DataMember(Order = 82)]
        public string W_2120_pay_adrc
        {
            get { return w_2120_pay_adrc; }
            set { w_2120_pay_adrc = value; }
        }

        [DataMember(Order = 83)]
        public string W_2130_off_tel
        {
            get { return w_2130_off_tel; }
            set { w_2130_off_tel = value; }
        }

        [DataMember(Order = 84)]
        public string W_2140_oth_dis1
        {
            get { return w_2140_oth_dis1; }
            set { w_2140_oth_dis1 = value; }
        }

        [DataMember(Order = 85)]
        public string W_2150_oth_dis2
        {
            get { return w_2150_oth_dis2; }
            set { w_2150_oth_dis2 = value; }
        }

        [DataMember(Order = 86)]
        public string W_2160_oth_dis3
        {
            get { return w_2160_oth_dis3; }
            set { w_2160_oth_dis3 = value; }
        }

        [DataMember(Order = 87)]
        public string W_2170_sec_dis1
        {
            get { return w_2170_sec_dis1; }
            set { w_2170_sec_dis1 = value; }
        }

        [DataMember(Order = 88)]
        public string W_2180_sec_dis2
        {
            get { return w_2180_sec_dis2; }
            set { w_2180_sec_dis2 = value; }
        }

        [DataMember(Order = 89)]
        public string W_2190_sec_dis3
        {
            get { return w_2190_sec_dis3; }
            set { w_2190_sec_dis3 = value; }
        }

        [DataMember(Order = 90)]
        public string W_2200_gtot_amn
        {
            get { return w_2200_gtot_amn; }
            set { w_2200_gtot_amn = value; }
        }

        [DataMember(Order = 91)]
        public string W_2210_spell_gtot
        {
            get { return w_2210_spell_gtot; }
            set { w_2210_spell_gtot = value; }
        }

        [DataMember(Order = 92)]
        public string W_2220_mr_date_m1
        {
            get { return w_2220_mr_date_m1; }
            set { w_2220_mr_date_m1 = value; }
        }

        [DataMember(Order = 93)]
        public string W_2230_mr_date_m2
        {
            get { return w_2230_mr_date_m2; }
            set { w_2230_mr_date_m2 = value; }
        }

        [DataMember(Order = 94)]
        public string W_2240_mr_date_m3
        {
            get { return w_2240_mr_date_m3; }
            set { w_2240_mr_date_m3 = value; }
        }

        [DataMember(Order = 95)]
        public string W_2250_mr_date_m4
        {
            get { return w_2250_mr_date_m4; }
            set { w_2250_mr_date_m4 = value; }
        }

        [DataMember(Order = 96)]
        public string W_2260_mr_date_m5
        {
            get { return w_2260_mr_date_m5; }
            set { w_2260_mr_date_m5 = value; }
        }

        [DataMember(Order = 97)]
        public string W_2270_mr_date_m6
        {
            get { return w_2270_mr_date_m6; }
            set { w_2270_mr_date_m6 = value; }
        }

        [DataMember(Order = 98)]
        public string W_2280_unit_m1
        {
            get { return w_2280_unit_m1; }
            set { w_2280_unit_m1 = value; }
        }

        [DataMember(Order = 99)]
        public string W_2290_unit_m2
        {
            get { return w_2290_unit_m2; }
            set { w_2290_unit_m2 = value; }
        }

        [DataMember(Order = 100)]
        public string W_2300_unit_m3
        {
            get { return w_2300_unit_m3; }
            set { w_2300_unit_m3 = value; }
        }

        [DataMember(Order = 101)]
        public string W_2310_unit_m4
        {
            get { return w_2310_unit_m4; }
            set { w_2310_unit_m4 = value; }
        }

        [DataMember(Order = 102)]
        public string W_2320_unit_m5
        {
            get { return w_2320_unit_m5; }
            set { w_2320_unit_m5 = value; }
        }

        [DataMember(Order = 103)]
        public string W_2330_unit_m6
        {
            get { return w_2330_unit_m6; }
            set { w_2330_unit_m6 = value; }
        }

        [DataMember(Order = 104)]
        public string W_1840_mru
        {
            get { return w_1840_mru; }
            set { w_1840_mru = value; }
        }

        [DataMember(Order = 105)]
        public string W_1110_basic_19_1_txt
        {
            get { return w_1110_basic_19_1_txt; }
            set { w_1110_basic_19_1_txt = value; }
        }

        [DataMember(Order = 106)]
        public string W_40_sname
        {
            get { return w_40_sname; }
            set { w_40_sname = value; }
        }

        [DataMember(Order = 107)]
        public string W_190_multi_n
        {
            get { return w_190_multi_n; }
            set { w_190_multi_n = value; }
        }

        [DataMember(Order = 108)]
        public string W_1100_sum_service_support_txt
        {
            get { return w_1100_sum_service_support_txt; }
            set { w_1100_sum_service_support_txt = value; }
        }
        #endregion

        #region #ISSUE#BLAN2019 GET SET

        [DataMember(Order = 109)]
        public string W_2340_rec_doc
        {
            get { return w_2340_rec_doc; }
            set { w_2340_rec_doc = value; }
        }

        [DataMember(Order = 110)]
        public string W_2350_rec_date
        {
            get { return w_2350_rec_date; }
            set { w_2350_rec_date = value; }
        }

        [DataMember(Order = 111)]
        public string W_2360_unit_rec
        {
            get { return w_2360_unit_rec; }
            set { w_2360_unit_rec = value; }
        }

        [DataMember(Order = 112)]
        public string W_2370_oth_dis4
        {
            get { return w_2370_oth_dis4; }
            set { w_2370_oth_dis4 = value; }
        }

        [DataMember(Order = 113)]
        public string W_2380_oth_dis5
        {
            get { return w_2380_oth_dis5; }
            set { w_2380_oth_dis5 = value; }
        }

        [DataMember(Order = 114)]
        public string W_2390_oth_dis6
        {
            get { return w_2390_oth_dis6; }
            set { w_2390_oth_dis6 = value; }
        }

        [DataMember(Order = 115)]
        public string W_2400_oth_dis7
        {
            get { return w_2400_oth_dis7; }
            set { w_2400_oth_dis7 = value; }
        }

        [DataMember(Order = 116)]
        public string W_2410_oth_dis8
        {
            get { return w_2410_oth_dis8; }
            set { w_2410_oth_dis8 = value; }
        }

        [DataMember(Order = 117)]
        public string W_2420_oth_dis9
        {
            get { return w_2420_oth_dis9; }
            set { w_2420_oth_dis9 = value; }
        }

        [DataMember(Order = 118)]
        public string W_2430_rec_text1
        {
            get { return w_2430_rec_text1; }
            set { w_2430_rec_text1 = value; }
        }

        [DataMember(Order = 119)]
        public string W_2440_rec_text2
        {
            get { return w_2440_rec_text2; }
            set { w_2440_rec_text2 = value; }
        }

        [DataMember(Order = 120)]
        public string W_2450_rec_text3
        {
            get { return w_2450_rec_text3; }
            set { w_2450_rec_text3 = value; }
        }

        [DataMember(Order = 121)]
        public string W_01_print_doc
        {
            get { return w_01_print_doc; }
            set { w_01_print_doc = value; }
        }


        #endregion
    }
}
