using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]
    public class GreenReceiptDetailInfo
    {
        private string _ReceiptNo;
        private string _ReceiptPrintDoc;
        private string _PrintBranch;
        private string _BranchId;
        private string _BranchName;
        private string _MruId;
        private string _CaId;
        private string _CaName;
        private string _ReceiptPeriod;
        private string _w_100_sender;
        private string _w_110_post_code;
        private string _w_121_mailing_person;
        private string _w_122_mailing_person;
        private string _w_211_address;
        private string _w_212_address;
        private string _w_213_address;
        private string _w_241_address;
        private string _w_242_address;
        private string _w_243_address;
        private string _w_250_post_code;
        private string _w_1610_invoice_no;
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
        private int? _ReceiptPrintStatus;
        private string _HasGrouped;
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
        public string ReceiptNo
        {
            get { return _ReceiptNo; }
            set { _ReceiptNo = value; }
        }
        [DataMember(Order = 2)]
        public string ReceiptPrintDoc
        {
            get { return _ReceiptPrintDoc; }
            set { _ReceiptPrintDoc = value; }
        }
        [DataMember(Order = 3)]
        public string PrintBranch
        {
            get { return _PrintBranch; }
            set { _PrintBranch = value; }
        }
        [DataMember(Order = 4)]
        public string BranchId
        {
            get { return _BranchId; }
            set { _BranchId = value; }
        }
        [DataMember(Order = 5)]
        public string BranchName
        {
            get { return _BranchName; }
            set { _BranchName = value; }
        }
        [DataMember(Order = 6)]
        public string MruId
        {
            get { return _MruId; }
            set { _MruId = value; }
        }
        [DataMember(Order = 7)]
        public string CaId
        {
            get { return _CaId; }
            set { _CaId = value; }
        }
        [DataMember(Order = 8)]
        public string CaName
        {
            get { return _CaName; }
            set { _CaName = value; }
        }
        [DataMember(Order = 9)]
        public string ReceiptPeriod
        {
            get { return _ReceiptPeriod; }
            set { _ReceiptPeriod = value; }
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
        public string W_211_address
        {
            get { return _w_211_address; }
            set { _w_211_address = value; }
        }
        [DataMember(Order = 15)]
        public string W_212_address
        {
            get { return _w_212_address; }
            set { _w_212_address = value; }
        }
        [DataMember(Order = 16)]
        public string W_213_address
        {
            get { return _w_213_address; }
            set { _w_213_address = value; }
        }
        [DataMember(Order = 17)]
        public string W_241_address
        {
            get { return _w_241_address; }
            set { _w_241_address = value; }
        }
        [DataMember(Order = 18)]
        public string W_242_address
        {
            get { return _w_242_address; }
            set { _w_242_address = value; }
        }
        [DataMember(Order = 19)]
        public string W_243_address
        {
            get { return _w_243_address; }
            set { _w_243_address = value; }
        }
        [DataMember(Order = 20)]
        public string W_250_post_code
        {
            get { return _w_250_post_code; }
            set { _w_250_post_code = value; }
        }
        [DataMember(Order = 21)]
        public string W_1610_invoice_no
        {
            get { return _w_1610_invoice_no; }
            set { _w_1610_invoice_no = value; }
        }
        [DataMember(Order = 22)]
        public string W_1620_buss_name
        {
            get { return _w_1620_buss_name; }
            set { _w_1620_buss_name = value; }
        }
        [DataMember(Order = 23)]
        public string W_1631_name
        {
            get { return _w_1631_name; }
            set { _w_1631_name = value; }
        }
        [DataMember(Order = 24)]
        public string W_1632_name
        {
            get { return _w_1632_name; }
            set { _w_1632_name = value; }
        }
        [DataMember(Order = 25)]
        public string W_1633_name
        {
            get { return _w_1633_name; }
            set { _w_1633_name = value; }
        }
        [DataMember(Order = 26)]
        public string W_1640_device_13_l1
        {
            get { return _w_1640_device_13_l1; }
            set { _w_1640_device_13_l1 = value; }
        }
        [DataMember(Order = 27)]
        public string W_1650_rate_cat_13_l2
        {
            get { return _w_1650_rate_cat_13_l2; }
            set { _w_1650_rate_cat_13_l2 = value; }
        }
        [DataMember(Order = 28)]
        public string W_1660_contract_ac_14_l1
        {
            get { return _w_1660_contract_ac_14_l1; }
            set { _w_1660_contract_ac_14_l1 = value; }
        }
        [DataMember(Order = 29)]
        public string W_1670_period_15_l1
        {
            get { return _w_1670_period_15_l1; }
            set { _w_1670_period_15_l1 = value; }
        }
        [DataMember(Order = 30)]
        public string W_1680_mr_date_15_l2
        {
            get { return _w_1680_mr_date_15_l2; }
            set { _w_1680_mr_date_15_l2 = value; }
        }
        [DataMember(Order = 31)]
        public string W_1690_unit_after_16_l1
        {
            get { return _w_1690_unit_after_16_l1; }
            set { _w_1690_unit_after_16_l1 = value; }
        }
        [DataMember(Order = 32)]
        public string W_1700_unit_previous_16_l2
        {
            get { return _w_1700_unit_previous_16_l2; }
            set { _w_1700_unit_previous_16_l2 = value; }
        }
        [DataMember(Order = 33)]
        public string W_1710_consumption_17_l1
        {
            get { return _w_1710_consumption_17_l1; }
            set { _w_1710_consumption_17_l1 = value; }
        }
        [DataMember(Order = 34)]
        public string W_1720_based_amount_18_l1
        {
            get { return _w_1720_based_amount_18_l1; }
            set { _w_1720_based_amount_18_l1 = value; }
        }
        [DataMember(Order = 35)]
        public string W_1730_discount_amount_19_l1
        {
            get { return _w_1730_discount_amount_19_l1; }
            set { _w_1730_discount_amount_19_l1 = value; }
        }
        [DataMember(Order = 36)]
        public string W_1740_disct_descript
        {
            get { return _w_1740_disct_descript; }
            set { _w_1740_disct_descript = value; }
        }
        [DataMember(Order = 37)]
        public string W_1750_baht
        {
            get { return _w_1750_baht; }
            set { _w_1750_baht = value; }
        }
        [DataMember(Order = 38)]
        public string W_1760_ft_amount_20_l1
        {
            get { return _w_1760_ft_amount_20_l1; }
            set { _w_1760_ft_amount_20_l1 = value; }
        }
        [DataMember(Order = 39)]
        public string W_1770_ft_price_20_l2
        {
            get { return _w_1770_ft_price_20_l2; }
            set { _w_1770_ft_price_20_l2 = value; }
        }
        [DataMember(Order = 40)]
        public string W_1780_net_before_tax_21_l1
        {
            get { return _w_1780_net_before_tax_21_l1; }
            set { _w_1780_net_before_tax_21_l1 = value; }
        }
        [DataMember(Order = 41)]
        public string W_1790_tax_amount_22_l1
        {
            get { return _w_1790_tax_amount_22_l1; }
            set { _w_1790_tax_amount_22_l1 = value; }
        }
        [DataMember(Order = 42)]
        public string W_1800_tax_rate_22_l2
        {
            get { return _w_1800_tax_rate_22_l2; }
            set { _w_1800_tax_rate_22_l2 = value; }
        }
        [DataMember(Order = 43)]
        public string W_1810_total_value_23_l1
        {
            get { return _w_1810_total_value_23_l1; }
            set { _w_1810_total_value_23_l1 = value; }
        }
        [DataMember(Order = 44)]
        public string W_1820_payment_date_24_l1
        {
            get { return _w_1820_payment_date_24_l1; }
            set { _w_1820_payment_date_24_l1 = value; }
        }
        [DataMember(Order = 45)]
        public int? ReceiptPrintStatus
        {
            get { return _ReceiptPrintStatus; }
            set { _ReceiptPrintStatus = value; }
        }
        [DataMember(Order = 46)]
        public string HasGrouped
        {
            get { return _HasGrouped; }
            set { _HasGrouped = value; }
        }
        [DataMember(Order = 47)]
        public string SyncFlag
        {
            get { return _SyncFlag; }
            set { _SyncFlag = value; }
        }
        [DataMember(Order = 48)]
        public string PostBranchServerId
        {
            get { return _PostBranchServerId; }
            set { _PostBranchServerId = value; }
        }
        [DataMember(Order = 49)]
        public DateTime? PostDt
        {
            get { return _PostDt; }
            set { _PostDt = value; }
        }

        [DataMember(Order = 50)]
        public DateTime? CreatedDt
        {
            get { return _CreatedDt; }
            set { _CreatedDt = value; }
        }
        [DataMember(Order = 51)]
        public DateTime? ModifiedDt
        {
            get { return _ModifiedDt; }
            set { _ModifiedDt = value; }
        }
        [DataMember(Order = 52)]
        public string ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        [DataMember(Order = 53)]
        public string FileName
        {
            get { return _FileName; }
            set { _FileName = value; }
        }
        [DataMember(Order = 54)]
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        [DataMember(Order = 55)]
        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }

    }
}
