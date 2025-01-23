using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Threading;
using System.Data.Common;
using System.Globalization;
using System.ComponentModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities;
using PEA.BPM.ePaymentsModule.Interface.BusinessEntities.ReceiptPrinting;
using System;

namespace PEA.BPM.ePaymentsModule.DA
{ 
    public class ReceiptPrintingDA
    {
        public const int CMD_TIMEOUT = 36000; 

        /* ================== Bill Printing DA ================== */

        public List<GreenBill> PrintGreenBill(ReceiptConditionParam param)
        {
            List<GreenBill> _greenBill = new List<GreenBill>();

            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            DbCommand cmd = db.GetStoredProcCommand("ePay_get_GreenReceipt");
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.AddInParameter(cmd, "@BeginCaId", DbType.String, param.BeginCaId);
            db.AddInParameter(cmd, "@EndCaId", DbType.String, param.EndCaId);
            db.AddInParameter(cmd, "@UploadDt", DbType.DateTime, param.UploadDt);
            db.AddInParameter(cmd, "@ReceiptId", DbType.String, param.ReceiptId);
            db.AddInParameter(cmd, "@CompanyId", DbType.String, param.CompanyId);
            try
            {
                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                return ObjectMappingForGreenBill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<GreenBill> ObjectMappingForGreenBill(DataTable dt)
        {
            List<GreenBill> _greenBill = new List<GreenBill>();

            foreach (DataRow dr in dt.Rows)
            {
                string tmpCaName = "";
                string tmpCaAddr = "";
                string tmpCaAddr2 = "";
                GreenBill gb = new GreenBill();
                gb.W_1610_invoice = DaHelper.GetString(dr, "InvoiceNo");
                gb.W_1820_payment_date_24_l1 = DateFormatter.ToDateShortThString(DaHelper.GetDateTime(dr, "PayDt"));
                gb.W_1620_buss_name = DaHelper.GetString(dr, "PrintBranchId");

                tmpCaName = StringConvert.FormatText(DaHelper.GetString(dr, "CaName").Trim(), 30);
                if (tmpCaName.Contains("\r\n"))
                {
                    gb.W_1631_1632_name = tmpCaName.Substring(0, tmpCaName.IndexOf("\r\n"));
                    tmpCaName = tmpCaName.Replace("\r\n", "");
                    tmpCaName = StringConvert.FormatText(tmpCaName.Replace(gb.W_1631_1632_name.Trim(), ""), 30).Replace("\r\n", "");
                    gb.W_1633_name = tmpCaName;
                }
                else
                {
                    gb.W_1631_1632_name = tmpCaName;
                }

                gb.W_1640_device_13_l1 = DaHelper.GetString(dr, "MeterId");
                gb.W_1650_rate_cat_13_l2 = DaHelper.GetString(dr, "RateTypeId");
                gb.W_1660_contract_ac_14_l1 = DaHelper.GetString(dr, "CaId");
                gb.W_1670_period_15_l1 = StringConvert.FormatPeriod(DaHelper.GetString(dr, "Period"));
                gb.W_1680_mr_date_15_l2 = DateFormatter.ToDateShortThString(DaHelper.GetDateTime(dr, "MeterReadDt"));
                gb.W_1690_unit_after_16_l1 = Convert.ToDecimal(DaHelper.GetDecimal(dr, "LastUnit")).ToString("0.00");
                gb.W_1700_unit_previous_16_l2 = Convert.ToDecimal(DaHelper.GetDecimal(dr, "ReadUnit")).ToString("0.00");
                gb.W_1710_consumption_17_l1 = Convert.ToDecimal(DaHelper.GetDecimal(dr, "Qty")).ToString("0.00");
                gb.W_1720_based_amount_18_l1 = Convert.ToDecimal(DaHelper.GetDecimal(dr, "BaseAmount")).ToString("0.00");
                gb.W_1760_ft_amount_20_l1 = Convert.ToDecimal(DaHelper.GetDecimal(dr, "FtAmount")).ToString("0.00");
                gb.W_1770_ft_price_20_l2 = Convert.ToDecimal(DaHelper.GetDecimal(dr, "FtUnitPrice")).ToString("0.0000");
                gb.W_1780_net_before_tax_21_l1 = Convert.ToDecimal(DaHelper.GetDecimal(dr, "Amount")).ToString("0.00");
                gb.W_1790_tax_amount_22_l1 = Convert.ToDecimal(DaHelper.GetDecimal(dr, "VatAmount")).ToString("0.00");
                gb.W_1800_tax_rate_22_l2 = Convert.ToDecimal(DaHelper.GetDecimal(dr, "TaxRate")).ToString("0");
                gb.W_1810_total_value_23_l1 = Convert.ToDecimal(DaHelper.GetDecimal(dr, "GAmount")).ToString("0.00");
                gb.W_100_sender = DaHelper.GetString(dr, "BranchName");
                gb.W_121_mailing_person = DaHelper.GetString(dr, "CaName");

                tmpCaAddr = StringConvert.FormatText(DaHelper.GetString(dr, "FullCaAddress"), 30);
                if (!tmpCaAddr.Contains("\r\n"))
                {
                    gb.W_211_address = tmpCaAddr.Trim();
                }
                else
                {
                    gb.W_211_address = tmpCaAddr.Substring(0, tmpCaAddr.IndexOf("\r\n"));
                    tmpCaAddr = tmpCaAddr.Replace("\r\n", "");
                    tmpCaAddr2 = StringConvert.FormatText(tmpCaAddr.Replace(gb.W_211_address.Trim(), ""), 30);
                    if (tmpCaAddr2.Contains("\r\n"))
                    {
                        gb.W_212_address = tmpCaAddr2.Substring(0, tmpCaAddr2.IndexOf("\r\n"));
                        gb.W_213_address = StringConvert.FormatText(tmpCaAddr.Replace(gb.W_211_address,"").Replace(gb.W_212_address.Trim(), ""), 30).Replace("\r\n", "");
                    }
                    else
                    {
                        gb.W_212_address = tmpCaAddr2.Replace("\r\n", "");
                    }
                }
                tmpCaAddr = StringConvert.FormatText(DaHelper.GetString(dr, "CaAddress"), 60);
                if (!tmpCaAddr.Contains("\r\n"))
                {
                    gb.W_241_242_address = tmpCaAddr.Trim();
                }
                else
                {
                    gb.W_241_242_address = tmpCaAddr.Substring(0, tmpCaAddr.IndexOf("\r\n"));
                    tmpCaAddr = tmpCaAddr.Replace("\r\n", "");
                    tmpCaAddr2 = StringConvert.FormatText(tmpCaAddr.Replace(gb.W_241_242_address.Trim(), ""), 60);
                    gb.W_243_address = tmpCaAddr2.Replace("\r\n", "");
                }
                gb.W_250_post_code = DaHelper.GetString(dr, "CaPostCode") != null ? DaHelper.GetString(dr, "CaPostCode") : "";

                gb.W_110_post_code = DaHelper.GetString(dr, "PostCode") != null ? DaHelper.GetString(dr, "PostCode") : "";
                gb.W_1570_account_number = DaHelper.GetString(dr, "ExtReceiptId").Contains("-") ? DaHelper.GetString(dr, "ExtReceiptId") : "Ref. " + DaHelper.GetString(dr, "ExtReceiptId");
                gb.W_1310_amount_txt = "*************";
                gb.W_1400_ftchg_txt = "*************";
                gb.W_1480_net_before_vat_txt = "*************";
                gb.W_1500_tax_amount_txt = "*************";
                gb.W_1510_total_amnt_txt = "*************";
                gb.FixedType = DaHelper.GetString(dr, "FixedType");
                gb.W_2070_taxid_p = DaHelper.GetString(dr, "w_2070_taxid_p");
                gb.W_2080_taxid_c = DaHelper.GetString(dr, "w_2080_taxid_c");

                //gb.W_10_invoice_no = DaHelper.GetString(dr, "w_10_invoice_no").Trim();
                //gb.W_122_mailing_person = DaHelper.GetString(dr, "w_122_mailing_person").Trim();
                //gb.W_130_period = DaHelper.GetString(dr, "w_130_period").Trim();
                //gb.W_140_reg = DaHelper.GetString(dr, "w_140_reg").Trim();
                //gb.W_150_contract = DaHelper.GetString(dr, "w_150_contract").Trim();
                //gb.W_160_device = DaHelper.GetString(dr, "w_160_device").Trim();
                //gb.W_170_rate_cat = DaHelper.GetString(dr, "w_170_rate_cat").Trim();
                //gb.W_171_ettat_code = DaHelper.GetString(dr, "w_171_ettat_code").Trim();
                //gb.W_200_mr_date = DaHelper.GetString(dr, "w_200_mr_date").Trim();
                //gb.W_255_device_1 = DaHelper.GetString(dr, "w_255_device_1").Trim();
                //gb.W_260_zwstand_1_txt = DaHelper.GetString(dr, "w_260_zwstand_1_txt").Trim();
                //gb.W_270_zwstvor_1_txt = DaHelper.GetString(dr, "w_270_zwstvor_1_txt").Trim();
                //gb.W_280_umwfakt_1_txt = DaHelper.GetString(dr, "w_280_umwfakt_1_txt").Trim();
                //gb.W_290_abrmenge_1_txt = DaHelper.GetString(dr, "w_290_abrmenge_1_txt").Trim();
                //gb.W_295_device_2 = DaHelper.GetString(dr, "w_295_device_2").Trim();
                //gb.W_300_zwstand_2_txt = DaHelper.GetString(dr, "w_300_zwstand_2_txt").Trim();
                //gb.W_310_zwstvor_2_txt = DaHelper.GetString(dr, "w_310_zwstvor_2_txt").Trim();
                //gb.W_320_umwfakt_2_txt = DaHelper.GetString(dr, "w_320_umwfakt_2_txt").Trim();
                //gb.W_330_abrmenge_2_txt = DaHelper.GetString(dr, "w_330_abrmenge_2_txt").Trim();
                //gb.W_340_peak_txt = DaHelper.GetString(dr, "w_340_peak_txt").Trim();
                //gb.W_350_dash_txt = DaHelper.GetString(dr, "w_350_dash_txt").Trim();
                //gb.W_355_device_3 = DaHelper.GetString(dr, "w_355_device_3").Trim();
                //gb.W_360_zwstand_3_txt = DaHelper.GetString(dr, "w_360_zwstand_3_txt").Trim();
                //gb.W_370_zwstvor_3_txt = DaHelper.GetString(dr, "w_370_zwstvor_3_txt").Trim();
                //gb.W_380_umwfakt_3_txt = DaHelper.GetString(dr, "w_380_umwfakt_3_txt").Trim();
                //gb.W_390_abrmenge_3_txt = DaHelper.GetString(dr, "w_390_abrmenge_3_txt").Trim();
                //gb.W_400_off_peak_txt = DaHelper.GetString(dr, "w_400_off_peak_txt").Trim();
                //gb.W_410_ene_txt = DaHelper.GetString(dr, "w_410_ene_txt").Trim();
                //gb.W_415_device_4 = DaHelper.GetString(dr, "w_415_device_4").Trim();
                //gb.W_420_zwstand_4_txt = DaHelper.GetString(dr, "w_420_zwstand_4_txt").Trim();
                //gb.W_430_zwstvor_4_txt = DaHelper.GetString(dr, "w_430_zwstvor_4_txt").Trim();
                //gb.W_440_umwfakt_4_txt = DaHelper.GetString(dr, "w_440_umwfakt_4_txt").Trim();
                //gb.W_450_abrmenge_4_txt = DaHelper.GetString(dr, "w_450_abrmenge_4_txt").Trim();
                //gb.W_460_hol_txt = DaHelper.GetString(dr, "w_460_hol_txt").Trim();
                //gb.W_500_txt6 = DaHelper.GetString(dr, "w_500_txt6").Trim();
                //gb.W_1380_fttot_txt = DaHelper.GetString(dr, "w_1380_fttot_txt").Trim();
                //gb.W_1381_ft_peiod_txt = DaHelper.GetString(dr, "w_1381_ft_peiod_txt").Trim();
                //gb.W_1450_mr_kw_17_6_txt = DaHelper.GetString(dr, "w_1450_mr_kw_17_6_txt").Trim();
                //gb.W_1460_mr_kw_17_7 = DaHelper.GetString(dr, "w_1460_mr_kw_17_7").Trim();
                //gb.W_1490_tax_rate_txt = DaHelper.GetString(dr, "w_1490_tax_rate_txt").Trim();
                //string tmp1 = DaHelper.GetString(dr, "w_1550_case_text1").Trim();
                //string tmp2 = DaHelper.GetString(dr, "w_1550_case_text2").Trim();
                //string tmp3 = DaHelper.GetString(dr, "w_1550_case_text3").Trim();
                //string tmp4 = DaHelper.GetString(dr, "w_1550_case_text4").Trim();
                //gb.W_1550_case_text1 = tmp1.Substring(0, tmp1.Length > 40 ? 40 : tmp1.Length);
                //gb.W_1550_case_text2 = tmp2.Substring(0, tmp2.Length > 40 ? 40 : tmp2.Length);
                //gb.W_1550_case_text3 = tmp3.Substring(0, tmp3.Length > 40 ? 40 : tmp3.Length);
                //gb.W_1550_case_text4 = tmp4.Substring(0, tmp4.Length > 16 ? 16 : tmp4.Length);
                //gb.W_1581_bank_due_date = DaHelper.GetString(dr, "w_1581_bank_due_date").Trim();
                //gb.W_1730_discount_amount_19_l1 = DaHelper.GetString(dr, "w_1730_discount_amount_19_l1").Trim();
                //gb.W_1740_disct_descript = DaHelper.GetString(dr, "w_1740_disct_descript").Trim();
                //gb.W_1750_baht = DaHelper.GetString(dr, "w_1750_baht").Trim();
                //gb.BillSeqNo = DaHelper.GetString(dr, "BillSeqNo").Trim();

                _greenBill.Add(gb);
            }

            return _greenBill;
        }

        public List<PPrintedReceipt> GetPPrintedData(PPrintedReceiptParam param)
        {
            try
            {
                List<PPrintedReceipt> pPrintedList = new List<PPrintedReceipt>();
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ePay_get_Receipt_PPrinted");
                cmd.CommandTimeout = CMD_TIMEOUT;

                db.AddInParameter(cmd, "UploadDt", DbType.DateTime, param.UploadDt);
                db.AddInParameter(cmd, "PaymentDt", DbType.DateTime, param.PaymentDt);
                db.AddInParameter(cmd, "CompanyId", DbType.String, param.CompanyId);
                db.AddInParameter(cmd, "ReceiptId", DbType.String, param.ReceiptId);
                db.AddInParameter(cmd, "BranchId", DbType.String, param.BranchId);

                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

                foreach (DataRow r in dt.Rows)
                {
                    PPrintedReceipt pPrinted = new PPrintedReceipt();
                    pPrinted.CompName = DaHelper.GetString(r, "CaName");
                    pPrinted.CompAddr = DaHelper.GetString(r, "CaAddress");
                    pPrinted.ReceiptId = DaHelper.GetString(r, "ReceiptId");
                    pPrinted.PaymentDt = DaHelper.GetDate (r, "PaymentDt");
                    pPrinted.Qty = DaHelper.GetInt(r, "Qty");
                    pPrinted.TotalBillCount = DaHelper.GetInt(r, "TotalBillCount");
                    pPrinted.TotalBillAmount = DaHelper.GetDecimal(r, "Amount");
                    pPrinted.BankName = DaHelper.GetString(r, "BankName");
                    pPrinted.TranfAccNo = DaHelper.GetString(r, "TranfAccNo");
                    pPrinted.TranfDt = DaHelper.GetDate(r, "TranfDt");
                    pPrinted.ReceiptBranch = DaHelper.GetString(r, "BranchName");
                    pPrinted.ReceiptAddr = DaHelper.GetString(r, "Address");

                    pPrintedList.Add(pPrinted);
                }

                return pPrintedList;
            }
            catch
            {
                throw;
            }
        }

        public List<DateTime> GetPayDatePPrintedData(PPrintedReceiptParam param)
        {
            try
            {
                List<DateTime> payDtList = new List<DateTime>();
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ePay_get_PayDt_PPrinted");
                cmd.CommandTimeout = CMD_TIMEOUT;

                db.AddInParameter(cmd, "UploadDt", DbType.DateTime, param.UploadDt);
                db.AddInParameter(cmd, "CompanyId", DbType.String, param.CompanyId);
                db.AddInParameter(cmd, "ReceiptId", DbType.String, param.ReceiptId);

                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

                foreach (DataRow r in dt.Rows)
                {
                    DateTime payDt = DateTime.MinValue;
                    payDt = DaHelper.GetDate(r, "PayDt").Value;
                    payDtList.Add(payDt);
                }

                return payDtList;
            }
            catch
            {
                throw;
            }
        }


        public List<Company> GetAgentAllData()
        {
            try
            {
                List<Company> compList = new List<Company>();
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ePay_get_All_Agent");
                cmd.CommandTimeout = CMD_TIMEOUT;

                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

                foreach (DataRow r in dt.Rows)
                {
                    Company comp = new Company();
                    comp.CompanyId = DaHelper.GetString(r, "CompanyId");
                    comp.CompanyName = DaHelper.GetString(r, "CompanyName");
                    compList.Add(comp);
                }

                return compList;
            }
            catch
            {
                throw;
            }
        }

        public List<EPayUpload> SearchAgentPaymentNumber(EPayUpload param)
        {
            try
            {
                List<EPayUpload> paymentList = new List<EPayUpload>();
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ePay_Search_Agent_Payment_Num");
                cmd.CommandTimeout = CMD_TIMEOUT;

                db.AddInParameter(cmd, "UploadDt", DbType.DateTime, param.UploadDt);
                db.AddInParameter(cmd, "CompanyId", DbType.String, param.CompanyId);
                db.AddInParameter(cmd, "PaymentDt", DbType.DateTime , param.PostDt);

                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                int i = 0;
                foreach (DataRow r in dt.Rows)
                {
                    i++;
                    EPayUpload payment = new EPayUpload();
                    payment.PostDt = DaHelper.GetDate(r, "PaymentDt");
                    payment.FileName = "ครั้งที่ " + i;
                    paymentList.Add(payment);
                }

                return paymentList;
            }
            catch
            {
                throw;
            }
        }

        /*   OFF LINE    */

        public List<PPrintedDeposit> SearchDebtClearify(PPrintedDeposit param)
        {
            try
            {
                List<PPrintedDeposit> clearifiedList = new List<PPrintedDeposit>();
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ePay_Search_Deposit_PrePrinted");
                cmd.CommandTimeout = CMD_TIMEOUT;

                db.AddInParameter(cmd, "UploadDt", DbType.DateTime, param.UploadDt);
                db.AddInParameter(cmd, "FixedDt", DbType.DateTime, param.FixedDt);
                db.AddInParameter(cmd, "CompanyId", DbType.String, param.CompanyId);
                db.AddInParameter(cmd, "FixedType", DbType.String, param.FixedType);
                db.AddInParameter(cmd, "ReceiptId", DbType.String, param.ReceiptId);

                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                foreach (DataRow r in dt.Rows)
                {
                    PPrintedDeposit clearified = new PPrintedDeposit();
                    clearified.FixedType = DaHelper.GetString(r, "FixedType");
                    clearified.UploadDt = DaHelper.GetDate(r, "UploadDt");
                    clearified.FixedDt = DaHelper.GetDate(r, "FixedDt");
                    clearified.CompanyId = DaHelper.GetString(r, "CompanyId");
                    clearified.CompanyName = DaHelper.GetString(r, "ComapanyName");
                    clearified.CaId = DaHelper.GetString(r, "CaId");
                    clearified.CaName = DaHelper.GetString(r, "CaName");
                    clearified.GAmount = DaHelper.GetDecimal(r, "GAmount");
                    clearified.ReceiptId = DaHelper.GetString(r, "ReceiptId");
                    clearified.RefDocNo = DaHelper.GetString(r, "RefDocNo");
                    clearified.InvoiceNo = DaHelper.GetString(r, "NewInvoiceNo");
                    clearified.ReceiptId = DaHelper.GetString(r, "ReceiptId");
                    
                    clearifiedList.Add(clearified);
                }

                return clearifiedList;
            }
            catch
            {
                throw;
            }
        }

        public PPrintedDeposit GetCADepositPPrinted(PPrintedDeposit param)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ePay_get_Deposit_CA_PrePrinted");
                cmd.CommandTimeout = CMD_TIMEOUT;

                db.AddInParameter(cmd, "CaId", DbType.String, param.CaId);
                db.AddInParameter(cmd, "CompanyId", DbType.String, param.CompanyId);
                db.AddInParameter(cmd, "UploadDt", DbType.DateTime, param.UploadDt);
                db.AddInParameter(cmd, "FixedDt", DbType.DateTime, param.FixedDt);
                db.AddInParameter(cmd, "InvoiceNo", DbType.String, param.InvoiceNo);
                db.AddInParameter(cmd, "BranchId", DbType.String, param.BranchId);

                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                PPrintedDeposit pPrinted = new PPrintedDeposit();
                foreach (DataRow r in dt.Rows)
                {
                    pPrinted.CaId = DaHelper.GetString(r, "CaId");
                    pPrinted.CaName = DaHelper.GetString(r, "CaName");
                    pPrinted.CaAddress = DaHelper.GetString(r, "CaAddress");
                    pPrinted.CompanyName = DaHelper.GetString(r, "CompanayName");
                    pPrinted.AccountClass = DaHelper.GetString(r, "AccountClass");
                    pPrinted.Qty = DaHelper.GetInt(r, "Qty");
                    pPrinted.GAmount = DaHelper.GetDecimal(r, "GAmount");
                    pPrinted.UploadDt = DaHelper.GetDate(r, "UploadDt");
                    pPrinted.PayDt = DaHelper.GetDate(r, "PayDt");
                    pPrinted.ReceiptId = DaHelper.GetString(r, "ReceiptId");
                    pPrinted.DepositBranch = DaHelper.GetString(r, "BranchName");
                    pPrinted.DepositAddr = DaHelper.GetString(r, "Address");
                }

                return pPrinted;
            }
            catch
            {
                throw;
            }
        }

        public PPrintedDeposit GetAgentDepositPPrinted(PPrintedDeposit param)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("POSDatabase");
                DbCommand cmd = db.GetStoredProcCommand("ePay_get_Deposit_Agent_PrePrinted");
                cmd.CommandTimeout = CMD_TIMEOUT;

                db.AddInParameter(cmd, "CaId", DbType.String, param.CaId);
                db.AddInParameter(cmd, "CompanyId", DbType.String, param.CompanyId);
                db.AddInParameter(cmd, "UploadDt", DbType.DateTime, param.UploadDt);
                db.AddInParameter(cmd, "FixedDt", DbType.DateTime, param.FixedDt);
                db.AddInParameter(cmd, "InvoiceNo", DbType.String, param.InvoiceNo);
                db.AddInParameter(cmd, "BranchId", DbType.String, param.BranchId);

                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];
                PPrintedDeposit pPrinted = new PPrintedDeposit();
                foreach (DataRow r in dt.Rows)
                {
                    pPrinted.CompanyName = DaHelper.GetString(r, "CompanyName");
                    pPrinted.CompanyAddr = DaHelper.GetString(r, "CompanyAddr");
                    pPrinted.CaId = DaHelper.GetString(r, "CaId");
                    pPrinted.Qty = DaHelper.GetInt(r, "Qty");
                    pPrinted.GAmount = DaHelper.GetDecimal(r, "GAmount");
                    pPrinted.DebtAmount = DaHelper.GetDecimal(r, "DebtAmount");
                    pPrinted.UploadDt = DaHelper.GetDate(r, "UploadDt");
                    pPrinted.PayDt = DaHelper.GetDate(r, "PayDt");
                    pPrinted.ReceiptId = DaHelper.GetString(r, "ReceiptId");
                    pPrinted.DepositBranch = DaHelper.GetString(r, "BranchName");
                    pPrinted.DepositAddr = DaHelper.GetString(r, "Address");
                }

                return pPrinted;
            }
            catch
            {
                throw;
            }
        }
    }
}
 