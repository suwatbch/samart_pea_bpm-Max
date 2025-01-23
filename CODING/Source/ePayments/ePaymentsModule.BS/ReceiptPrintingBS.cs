using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.ePaymentsModule.Interface.Services;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities.ReceiptPrinting;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using PEA.BPM.ePaymentsModule.DA;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.ePaymentsModule.Interface.Constants;
//using PEA.BPM.ePaymentsModule.Utilities;

namespace PEA.BPM.ePaymentsModule.BS  
{
    public class ReceiptPrintingBS : IReceiptPrintingService
    {
        /* ================== Bill Printing BS ================== */

        public List<Bills> PrintGreenBill(ReceiptConditionParam param)
        {
            ReceiptPrintingDA da = new ReceiptPrintingDA();
            List<GreenBill> _greenBill = da.PrintGreenBill(param); //store bills data for layout design.
            return AdjustGreenBillLayout(_greenBill);
        }

        private List<Bills> AdjustGreenBillLayout(List<GreenBill> _greenBill)
        {
            List<Bills> _bills = new List<Bills>();

            foreach (GreenBill gb in _greenBill)
            {
                string resultTxt;
                string blank = " ";

                StringBuilder sb = new StringBuilder();
                //sb.Append(@"///////////////".PadLeft(35,' '));
                //sb.Append("\r\n");
                //sb.AppendFormat(string.Format("{0}{1}",
                //    blank.PadRight(78, ' '),
                //    gb.W_10_invoice_no));//1 R1
                sb.Append("\r\n");
                sb.AppendFormat(string.Format("{0}{1}{2}",
                    @"///////////////".PadLeft(35, ' '),
                    blank.PadRight(43, ' '),
                    gb.W_10_invoice_no));//1 R1
                sb.Append("\r\n");//CR&LF to line 2
                sb.Append("\r\n");//CR&LF to line 3
                sb.AppendFormat(String.Format("{0}{1}{2}",
                    blank.PadRight(4, ' '),
                    gb.W_1610_invoice.PadRight(21, ' '),
                    gb.W_1820_payment_date_24_l1));//3 L1
                sb.Append("\r\n");//CR&LF to line 4
                sb.AppendFormat("{0}{1}{2}{3}{4}{5}{6}{7}",
                    blank.PadRight(35, ' '),
                    //**********Right Part**********//
                    gb.W_140_reg.PadLeft(8, ' '),
                    blank.PadRight(2, ' '),
                    gb.W_150_contract.PadRight(18, ' '),
                    gb.W_160_device.PadRight(11, ' '),
                    gb.W_171_ettat_code.PadRight(6, ' '),
                    gb.W_200_mr_date.PadRight(9, ' '),
                    gb.W_130_period.PadRight(7, ' '));//4 R1-6
                sb.Append("\r\n");//CR&LF to line 5
                sb.Append("\r\n");//CR&LF to line 6
                sb.AppendFormat("{0}{1}{2}{3}{4}",
                    blank.PadRight(23, ' '),
                    //gb.W_1620_buss_name.PadRight(12, ' '),
                    StringConvert.PadRight(gb.W_1620_buss_name, 12),
                    //**********Right Part**********//
                    blank.PadRight(6, ' '),
                    StringConvert.PadRight(gb.W_100_sender, 44),
                    gb.W_110_post_code);//6 L1,R1,R2
                sb.Append("\r\n");//CR&LF to line 7
                sb.AppendFormat("{0}{1}{2}{3}",
                    blank.PadRight(4, ' '),
                    StringConvert.PadRight(gb.W_1631_1632_name, 31),
                    //**********Right Part**********//
                    blank.PadRight(6, ' '),
                    gb.W_121_mailing_person);//7 L1,R1
                sb.Append("\r\n");//CR&LF to line 8
                sb.AppendFormat("{0}{1}{2}{3}",
                    blank.PadRight(4, ' '),
                    StringConvert.PadRight(gb.W_1633_name, 31),
                    //**********Right Part**********//
                    blank.PadRight(6, ' '),
                    gb.W_122_mailing_person);//8 L1,R1
                sb.Append("\r\n");//CR&LF to line 9
                sb.AppendFormat("{0}{1}{2}{3}",
                    blank.PadRight(4, ' '),
                    StringConvert.PadRight(gb.W_211_address, 31),
                    //**********Right Part**********//
                    blank.PadRight(6, ' '),
                    gb.W_241_242_address);//9 L1,R1
                sb.Append("\r\n");//CR&LF to line 10
                sb.AppendFormat("{0}{1}{2}{3}{4}",
                    blank.PadRight(4, ' '),
                    StringConvert.PadRight(gb.W_212_address, 31),
                    //**********Right Part**********//
                    blank.PadRight(6, ' '),
                    StringConvert.PadRight(gb.W_243_address, 44),
                    gb.W_250_post_code);//10 L1,R1,R2
                sb.Append("\r\n");//CR&LF to line 11
                sb.AppendFormat("{0}{1}{2}",
                    blank.PadRight(4, ' '),
                    StringConvert.PadRight(gb.W_213_address, 31),
                    //**********Right Part**********//
                    gb.W_500_txt6.PadLeft(12, ' '));//11 L1,R1
                sb.Append("\r\n");//CR&LF to line 12
                sb.AppendFormat("{0}{1}{2}{3}{4}{5}{6}{7}{8}",
                    blank.PadRight(8, ' '),
                    StringConvert.PadRight(gb.W_1640_device_13_l1 == null ? "" : gb.W_1640_device_13_l1, 19),
                    gb.W_1650_rate_cat_13_l2.PadRight(8, ' '),
                    //**********Right Part**********//
                    blank.PadRight(1, ' '),
                    StringConvert.PadRight(gb.W_255_device_1, 12),
                    StringConvert.PadLeft(gb.W_260_zwstand_1_txt, 12),
                    StringConvert.PadLeft(gb.W_270_zwstvor_1_txt, 12),
                    StringConvert.PadLeft(gb.W_280_umwfakt_1_txt, 10),
                    StringConvert.PadLeft(gb.W_290_abrmenge_1_txt, 14));//12 L1,L2,R1-4
                sb.Append("\r\n");//CR&LF to line 13
                sb.AppendFormat("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}",
                    blank.PadRight(11, ' '),
                    gb.W_1660_contract_ac_14_l1.Trim() == string.Empty ? blank.PadRight(24, ' ') : gb.W_140_reg.PadRight(7, ' ') + gb.W_1660_contract_ac_14_l1.PadRight(17, ' '),
                    //**********Right Part**********//
                    blank.PadRight(1, ' '),
                    StringConvert.PadRight(gb.W_295_device_2, 4),
                    gb.W_350_dash_txt.PadRight(6, ' '),
                    gb.W_340_peak_txt.PadLeft(2, ' '),
                    StringConvert.PadLeft(gb.W_300_zwstand_2_txt, 12),
                    StringConvert.PadLeft(gb.W_310_zwstvor_2_txt, 12),
                    StringConvert.PadLeft(gb.W_320_umwfakt_2_txt, 10),
                    StringConvert.PadLeft(gb.W_330_abrmenge_2_txt, 14));//13 L1,R1-6
                sb.Append("\r\n");//CR&LF to line 14
                sb.AppendFormat("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}",
                    blank.PadRight(8, ' '),
                    gb.W_1670_period_15_l1.PadRight(19, ' '),
                    gb.W_1680_mr_date_15_l2.PadRight(8, ' '),
                    //**********Right Part**********//
                    blank.PadRight(1, ' '),
                    StringConvert.PadRight(gb.W_355_device_3, 4),
                    gb.W_410_ene_txt.PadRight(6, ' '),
                    gb.W_400_off_peak_txt.PadLeft(2, ' '),
                    StringConvert.PadLeft(gb.W_360_zwstand_3_txt, 12),
                    StringConvert.PadLeft(gb.W_370_zwstvor_3_txt, 12),
                    StringConvert.PadLeft(gb.W_380_umwfakt_3_txt, 10),
                    StringConvert.PadLeft(gb.W_390_abrmenge_3_txt, 14));//14 L1,L2,R1-6
                sb.Append("\r\n");//CR&LF to line 15
                sb.AppendFormat("{0}{1}{2}{3}{4}{5}{6}{7}",
                    gb.W_1690_unit_after_16_l1.PadLeft(18, ' '),
                    gb.W_1700_unit_previous_16_l2.PadLeft(17, ' '),
                    //**********Right Part**********//
                    blank.PadRight(1, ' '),
                    StringConvert.PadRight(gb.W_415_device_4, 10),
                    gb.W_460_hol_txt.PadLeft(2, ' '),
                    StringConvert.PadLeft(gb.W_420_zwstand_4_txt, 12),
                    StringConvert.PadLeft(gb.W_430_zwstvor_4_txt, 12),
                    StringConvert.PadLeft(gb.W_440_umwfakt_4_txt, 10),
                    StringConvert.PadLeft(gb.W_450_abrmenge_4_txt, 14));//15 L1,L2,R1-5
                sb.Append("\r\n");//CR&LF to line 16
                sb.AppendFormat("{0}{1}{2}{3}",
                    gb.W_1710_consumption_17_l1.PadLeft(31, ' '),
                    blank.PadRight(4, ' '),
                    //**********Right Part**********//
                    blank.PadRight(2, ' '),
                    StringConvert.PadRight(gb.W_1550_case_text1, 40));//16 L1,N1
                sb.Append("\r\n");//CR&LF to line 17
                sb.AppendFormat("{0}{1}{2}{3}{4}{5}",
                    gb.W_1720_based_amount_18_l1.PadLeft(31, ' '),
                    blank.PadRight(4, ' '),
                    //**********Right Part**********//
                    blank.PadRight(2, ' '),
                    StringConvert.PadRight(gb.W_1550_case_text2, 40),
                    blank.PadRight(6, ' '),
                    gb.W_1310_amount_txt.PadLeft(13, ' '));//17 L1,N1,R1
                sb.Append("\r\n");//CR&LF to line 18
                sb.AppendFormat("{0}{1}{2}{3}{4}{5}{6}{7}{8}",
                    blank.PadRight(2, ' '),
                    StringConvert.PadRight(gb.W_1740_disct_descript, 15),
                    gb.W_1730_discount_amount_19_l1.PadLeft(14, ' '),
                    blank.PadRight(1, ' '),
                    StringConvert.PadRight(gb.W_1750_baht, 3),
                    //**********Right Part**********//
                    blank.PadRight(2, ' '),
                    StringConvert.PadRight(gb.W_1550_case_text3, 40),
                    StringConvert.PadLeft(gb.W_1460_mr_kw_17_7, 6),
                    gb.W_1450_mr_kw_17_6_txt.PadLeft(13, ' '));//18 L1-3,N1,R1,R2
                sb.Append("\r\n");//CR&LF to line 19
                sb.AppendFormat("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}",
                    blank.PadRight(5, ' '),
                    gb.W_1770_ft_price_20_l2.PadRight(10, ' '),
                    gb.W_1760_ft_amount_20_l1.PadLeft(16, ' '),
                    blank.PadRight(4, ' '),
                    //**********Right Part**********//
                    blank.PadRight(2, ' '),
                    StringConvert.PadRight(gb.W_1550_case_text4, 16),
                    StringConvert.PadLeft(gb.W_1381_ft_peiod_txt, 14) + "   ",
                    gb.W_1380_fttot_txt.PadRight(6, ' '),
                    blank.PadRight(6, ' '),
                    gb.W_1400_ftchg_txt.PadLeft(14, ' '));//19 L1,L2,N1,R1,R2
                sb.Append("\r\n");//CR&LF to line 20
                sb.AppendFormat("{0}{1}{2}{3}",
                    gb.W_1780_net_before_tax_21_l1.PadLeft(31, ' '),
                    blank.PadRight(4, ' '),
                    //**********Right Part**********//
                    blank.PadRight(48, ' '),
                    gb.W_1480_net_before_vat_txt.PadLeft(13, ' '));//20 L1,R1
                sb.Append("\r\n");//CR&LF to line 21
                sb.AppendFormat("{0}{1}{2}{3}{4}{5}{6}{7}",
                    blank.PadRight(9, ' '),
                    gb.W_1800_tax_rate_22_l2.PadRight(5, ' '),
                    gb.W_1790_tax_amount_22_l1.PadLeft(17, ' '),
                    blank.PadRight(4, ' '),
                    //**********Right Part**********//
                    "/////".PadLeft(8,' ') + blank.PadRight(3, ' ') ,
                    gb.W_1570_account_number.PadRight(31, ' '),
                    gb.W_1490_tax_rate_txt.PadRight(6, ' '),
                    gb.W_1500_tax_amount_txt.PadLeft(13, ' '));//21 L1,L2,R1-3
                sb.Append("\r\n");//CR&LF to line 22
                sb.AppendFormat("{0}{1}{2}{3}{4}",
                    gb.FixedType.Trim() == "1" ? "(ชำระบางส่วน)".PadLeft(21, ' ') + gb.W_1810_total_value_23_l1.PadLeft(11, ' ') : 
                    gb.W_1810_total_value_23_l1.PadLeft(31, ' '),
                    blank.PadRight(4, ' '),
                    //**********Right Part**********//
                    blank.PadRight(20, ' '),
                    FormatStringDate(gb.W_1581_bank_due_date).PadRight(28, ' '),
                    gb.W_1510_total_amnt_txt.PadLeft(13, '*'));//22 L1,R1,R2
                sb.Append("\r\n");//CR&LF to line 23
                sb.AppendFormat("{0}{1}",
                    //blank.PadRight(18, ' '),
                    //blank.PadRight(9 + 8, ' '));//23 L1
                    blank.PadRight(2, ' '),
                    StringConvert.PadRight(gb.W_2080_taxid_c, 33));
                sb.Append("\r\n");//CR&LF to line 24                    
                resultTxt = sb.ToString();

                Bills _bill = new Bills();
                _bill.BillId = gb.W_140_reg + "-" + gb.W_150_contract;
                _bill.BillTxt = resultTxt;
                _bills.Add(_bill);
            }
            return _bills;
        }

        private string FormatStringDate(string txt)
        {
            if (txt.Trim().Length == 8)
                return txt.Substring(6, 2) + "/" + txt.Substring(4, 2) + "/" + txt.Substring(0, 4);
            else
                return string.IsNullOrEmpty(txt) ? " " : txt;
        }

        public List<PPrintedReceipt> GetPPrintedService(PPrintedReceiptParam param)
        {
            try
            {
                ReceiptPrintingDA da = new ReceiptPrintingDA();
                List<PPrintedReceipt> pPrintedList = da.GetPPrintedData(param);
                if (pPrintedList.Count > 0)
                {
                    pPrintedList[0].PayDtList = da.GetPayDatePPrintedData(param);
                }
                return pPrintedList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Company> GetAgentAllService()
        {
            try
            {
                ReceiptPrintingDA da = new ReceiptPrintingDA();
                List<Company> compList = da.GetAgentAllData();
                return compList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<EPayUpload> SearchAgentPaymentNumber(EPayUpload param)
        {
            try
            {
                ReceiptPrintingDA da = new ReceiptPrintingDA();
                List<EPayUpload> paymentList = da.SearchAgentPaymentNumber(param);
                return paymentList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

            /*   Off Line   */

        public List<PPrintedDeposit> SearchDebtClearify(PPrintedDeposit param)
        {
            try
            {
                ReceiptPrintingDA da = new ReceiptPrintingDA();
                List<PPrintedDeposit> clerifiedList = da.SearchDebtClearify(param);
                return clerifiedList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<PPrintedDeposit> GetCADepositPPrinted(List<PPrintedDeposit> paramList)
        {
            try
            {
                ReceiptPrintingDA da = new ReceiptPrintingDA();
                List<PPrintedDeposit> pPrintedDepositList = new List<PPrintedDeposit>();
                foreach (PPrintedDeposit pPrinted in paramList)
                {
                    PPrintedDeposit result  = da.GetCADepositPPrinted(pPrinted);
                    pPrintedDepositList.Add(result);
                }
                return pPrintedDepositList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<PPrintedDeposit> GetAgentDepositPPrinted(List<PPrintedDeposit> paramList)
        {
            try
            {
                ReceiptPrintingDA da = new ReceiptPrintingDA();
                List<PPrintedDeposit> pPrintedDepositList = new List<PPrintedDeposit>();
                foreach (PPrintedDeposit pPrinted in paramList)
                {
                    PPrintedDeposit result = da.GetAgentDepositPPrinted(pPrinted);
                    pPrintedDepositList.Add(result);
                }
                return pPrintedDepositList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}

