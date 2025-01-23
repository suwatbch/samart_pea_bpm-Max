using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BPMSAPConnector;
using System.Globalization;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;

namespace RFCTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //FindNow();

            ZTABTEXT z = new ZTABTEXT();

            z.Add("1");

            string msg = "";
            string conn = "CLIENT=220 USER=bpmsapc220 PASSWD=password TYPE=3 LANG=TH R3NAME=PED GROUP=PEA-BPM MSHOST=172.16.166.21";
            BPMSAPProxy submit = new BPMSAPProxy(conn);
            submit.Timeout = 5 * 60 * 1000;
            submit.Zpos_Submit_Post("TRP00100000143864_20191107_104540_A03501.txt", z, out msg);
            var test = msg;
            var test2 = "";
        }

        private void FindNow()
        {
            try
            {

                string conn = comboBox1.Text;
                BPMSAPProxy sapConn = new BPMSAPProxy(conn);

                ZCA_MTR0060Table z60 = new ZCA_MTR0060Table();
                ZCA_MTR0090Table z90 = new ZCA_MTR0090Table();
                ZCA_TRR0010Table z10 = new ZCA_TRR0010Table();
                ZCA_TRR0020Table z20 = new ZCA_TRR0020Table();
                ZCA_TRR0040Table z40 = new ZCA_TRR0040Table();
                ZCA_TRR0045Table z45 = new ZCA_TRR0045Table();

                ZCADOC zCaDoc = null;
                if (textBox2.Text != string.Empty)
                {
                    zCaDoc = new ZCADOC();
                    zCaDoc.Add(textBox2.Text.PadLeft(12, '0'));
                }

                string refDoc = "";
                if (textBox3.Text != string.Empty)
                {
                    refDoc = textBox3.Text.PadLeft(16, '0');
                }

                string caNo = "";
                if (textBox1.Text != string.Empty)
                {
                    caNo = textBox1.Text.PadLeft(12, '0');
                }

                string action = "0";
                IFormatProvider formatDate = CultureInfo.CreateSpecificCulture("en-EN");
                //string startDate = DateTime.Now.AddMonths(-3).ToString("yyyyMMdd", formatDate);
                //string startDate = DateTime.Now.AddDays(-DateTime.Now.Day + 1).AddMonths(-Convert.ToInt32(maskedPreviousMonths.Text)).ToString("yyyyMMdd", formatDate);
                string startDate = DateTime.Now.AddDays(-Convert.ToInt32(maskedPreviousMonths.Text)).ToString("yyyyMMdd", formatDate);

                sapConn.Zpos_Zcaci029(action, zCaDoc, caNo, "12345", refDoc, startDate, ref z60, ref z90, ref z10, ref z20, ref z40, ref z45);

                dataGridView1.DataSource = z60.ToADODataTable();
                dataGridView2.DataSource = z90.ToADODataTable();
                dataGridView3.DataSource = z10.ToADODataTable();
                dataGridView4.DataSource = z20.ToADODataTable();

                dataGridView5.DataSource = z40.ToADODataTable();
                dataGridView6.DataSource = z45.ToADODataTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //private void FindNow()
        //{
        //    try
        //    {

        //        string conn = comboBox1.Text;
        //        BPMSAPProxy sapConn = new BPMSAPProxy(conn);

        //        ZCA_MTR0060Table z60 = new ZCA_MTR0060Table();
        //        ZCA_MTR0090Table z90 = new ZCA_MTR0090Table();
        //        ZCA_TRR0010Table z10 = new ZCA_TRR0010Table();
        //        ZCA_TRR0020Table z20 = new ZCA_TRR0020Table();
        //        ZCA_TRR0040Table z40 = new ZCA_TRR0040Table();
        //        ZCA_TRR0045Table z45 = new ZCA_TRR0045Table();

        //        ZCADOC zCaDoc = null;
        //        if (textBox2.Text != string.Empty)
        //        {
        //            zCaDoc = new ZCADOC();
        //            zCaDoc.Add(textBox2.Text.PadLeft(12, '0'));
        //        }

        //        string refDoc = "";
        //        if (textBox3.Text != string.Empty)
        //        {
        //            refDoc = textBox3.Text.PadLeft(16, '0');
        //        }

        //        string caNo = "";
        //        if (textBox1.Text != string.Empty)
        //        {
        //            caNo = textBox1.Text.PadLeft(12, '0');
        //        }

        //        string action = "0";
        //        IFormatProvider formatDate = CultureInfo.CreateSpecificCulture("en-EN");
        //        //string startDate = DateTime.Now.AddMonths(-3).ToString("yyyyMMdd", formatDate);
        //        //string startDate = DateTime.Now.AddDays(-DateTime.Now.Day + 1).AddMonths(-Convert.ToInt32(maskedPreviousMonths.Text)).ToString("yyyyMMdd", formatDate);
        //        string startDate = DateTime.Now.AddDays(-Convert.ToInt32(maskedPreviousMonths.Text)).ToString("yyyyMMdd", formatDate);

        //        for (int m = 0; m < 100000; m++)
        //        {

        //            caNo = (Convert.ToInt64(caNo) + 1).ToString().PadLeft(12, '0');

        //            sapConn.Zpos_Zcaci029(action, zCaDoc, caNo, "12345", refDoc, startDate, ref z60, ref z90, ref z10, ref z20, ref z40, ref z45);

        //            List<ARInfo> arr = ConvertToARList(z10);



        //            IFormatProvider provider = CultureInfo.CreateSpecificCulture("en-EN");
        //            List<ARInfo> list = new List<ARInfo>();

        //            for (int i = 0; i < z10.Count; i++)
        //            {
        //                string itemId = StringConvert.ToString(z10[i].Ca_Doc);

        //                ARInfo ar = list.Find(delegate(ARInfo a)
        //                {
        //                    return a.ItemId == itemId;
        //                }
        //                    );


        //                if (ar != null)
        //                {
        //                    foreach (ARInfo a in list)
        //                    {
        //                        if (a.ItemId == itemId)
        //                        {
        //                            a.Amount += StringConvert.ToDecimal(z10[i].Amt_Tax_Base);
        //                            a.VatAmount += StringConvert.ToDecimal(z10[i].Amt_Tax);
        //                            a.GAmount += StringConvert.ToDecimal(z10[i].Amt);
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    ARInfo obj = new ARInfo();

        //                    obj.ItemId = StringConvert.ToString(z10[i].Ca_Doc);
        //                    obj.CaId = StringConvert.ToString(z10[i].Ca_No);
        //                    obj.DtId = "M" + StringConvert.ToString(z10[i].Main_Transaction) + StringConvert.ToString(z10[i].Sub_Transaction);
        //                    obj.Description = StringConvert.ToString(z10[i].Text);
        //                    obj.InvoiceNo = StringConvert.ToString(z10[i].Ref_Doc);
        //                    obj.InvoiceDt = StringConvert.ToDateTime(z10[i].Doc_Date, provider);
        //                    obj.GroupInvoiceNo = StringConvert.ToString(z10[i].Classification);
        //                    obj.Period = StringConvert.ToString(z10[i].Allocation_Date);
        //                    obj.MeterId = StringConvert.ToString(z10[i].Equip_No);
        //                    obj.RateTypeId = StringConvert.ToString(z10[i].Rate_Type);
        //                    obj.MeterReadDt = StringConvert.ToDateTime(z10[i].Meter_Date, provider);
        //                    obj.ReadUnit = StringConvert.ToDecimal(z10[i].Meter_Unit);
        //                    obj.LastUnit = StringConvert.ToDecimal(z10[i].Meter_Unit_Last);
        //                    obj.BaseAmount = StringConvert.ToDecimal(z10[i].Amt_Base);
        //                    obj.FTUnitPrice = StringConvert.ToDecimal(z10[i].Ft_Unit);
        //                    obj.FTAmount = StringConvert.ToDecimal(z10[i].Ft_Amt);
        //                    obj.UnitPrice = StringConvert.ToDecimal(z10[i].Unit_Price);
        //                    obj.Qty = StringConvert.ToDecimal(z10[i].Quantity);
        //                    obj.Amount = StringConvert.ToDecimal(z10[i].Amt_Tax_Base);
        //                    obj.UnitTypeId = StringConvert.ToString(z10[i].Uom);
        //                    obj.TaxCode = StringConvert.ToString(z10[i].Tax_Code);
        //                    obj.VatAmount = StringConvert.ToDecimal(z10[i].Amt_Tax);
        //                    obj.GAmount = StringConvert.ToDecimal(z10[i].Amt);
        //                    obj.DueDt = StringConvert.ToDateTime(z10[i].Due_Date_Payment, provider);
        //                    obj.DueDt2 = StringConvert.ToDateTime(z10[i].Deferral, provider);
        //                    obj.InterestLockFlag = StringConvert.ToString(z10[i].Int_Lock_Reason);
        //                    obj.InterestKey = StringConvert.ToString(z10[i].Interest_Key);
        //                    obj.DisconnectDt = StringConvert.ToDateTime(z10[i].Date_Issue, provider);
        //                    obj.DisconnectDoc = StringConvert.ToString(z10[i].Doc_Disconnect);
        //                    obj.SubstDocNo = StringConvert.ToString(z10[i].Substitude_No);
        //                    obj.InstallmentPeriod = StringConvert.ToInt32(z10[i].Repetition_Item);
        //                    obj.InstallmentTotalPeriod = StringConvert.ToInt32(z10[i].No_Installment);
        //                    obj.SpotBillInvoiceNo = StringConvert.ToString(z10[i].Device_Value);
        //                    obj.CancelFlag = StringConvert.ToString(z10[i].Item_Cancled);
        //                    obj.PaymentOrderFlag = StringConvert.ToString(z10[i].Item_Included);
        //                    obj.PaymentOrderDt = StringConvert.ToDateTime(z10[i].Date_Payment, provider);
        //                    obj.ModifiedFlag = StringConvert.ToString(z10[i].Dunn_Lock_Reason);
        //                    obj.Action = z10[i].Action;

        //                    obj.SyncFlag = "0";
        //                    obj.PostDt = DateTime.Now;
        //                    obj.PostBranchServerId = "";
        //                    obj.ModifiedDt = DateTime.Now;
        //                    obj.ModifiedBy = "RFC_CALL";

        //                    if (z10[i].Id == "TRR0010A")
        //                        obj.FileType = "A";
        //                    else if (z10[i].Id == "TRR0010B")
        //                        obj.FileType = "B";
        //                    else if (z10[i].Id == "TRR0010C")
        //                        obj.FileType = "C";
        //                    else if (z10[i].Id == "TRR0010D")
        //                        obj.FileType = "D";
        //                    else if (z10[i].Id == "TRR0010E")
        //                        obj.FileType = "E";
        //                    else if (z10[i].Id == "TRR0010F")
        //                        obj.FileType = "F";


        //                    if (obj.DtId == "M00710010")
        //                    {
        //                        MessageBox.Show(obj.CaId);
        //                    }

        //                    list.Add(obj);
        //                }

        //            }






        //            //List<PayFromSAPInfo> ps = ConvertToPayFromSAPList(z20);


        //            //List<PayFromSAPInfo> list1 = new List<PayFromSAPInfo>();
        //            //for (int i = 0; i < z20.Count; i++)
        //            //{
        //            //    PayFromSAPInfo obj = new PayFromSAPInfo();

        //            //    obj.PaymentDocNo = StringConvert.ToString(z20[i].Payment_Doc);
        //            //    obj.ReceiptNo = StringConvert.ToString(z20[i].Receipt_No);
        //            //    obj.InvoiceNo = StringConvert.ToString(z20[i].Invoice_No);
        //            //    obj.CaDoc = StringConvert.ToString(z20[i].Ca_Doc);
        //            //    obj.DocType = StringConvert.ToString(z20[i].Doc_Type);
        //            //    obj.PaymentDt = StringConvert.ToDateTime(z20[i].Date_Payment, provider);
        //            //    obj.PmId = StringConvert.ToString(z20[i].Payment_Id);
        //            //    obj.VatAmount = StringConvert.ToDecimal(z20[i].Tax_Amt);
        //            //    obj.Amount = StringConvert.ToDecimal(z20[i].Amt);
        //            //    obj.CancelFlag = StringConvert.ToString(z20[i].Flag);
        //            //    obj.DueDt = StringConvert.ToDateTime(z20[i].Inv_Duedate, provider);
        //            //    obj.SyncFlag = "0";
        //            //    obj.ModifiedBy = "RFC_CALL";
        //            //    obj.ModifiedDt = DateTime.Now;
        //            //    obj.Action = z20[i].Action;

        //            //    if (z20[i].Ca_Doc != null)
        //            //    {
        //            //        string caNo123 = (arr.Count > 0 ? arr[0].CaId : "");
        //            //        MessageBox.Show(obj.CaDoc + " : " + caNo123);
        //            //    }

        //            //    list1.Add(obj);
        //            //}


        //        }




        //        dataGridView1.DataSource = z60.ToADODataTable();
        //        dataGridView2.DataSource = z90.ToADODataTable();
        //        dataGridView3.DataSource = z10.ToADODataTable();
        //        dataGridView4.DataSource = z20.ToADODataTable();

        //        dataGridView5.DataSource = z40.ToADODataTable();
        //        dataGridView6.DataSource = z45.ToADODataTable();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}


        private List<ARInfo> ConvertToARList(ZCA_TRR0010Table z10)
        {
            IFormatProvider provider = CultureInfo.CreateSpecificCulture("en-EN");
            List<ARInfo> list = new List<ARInfo>();

            for (int i = 0; i < z10.Count; i++)
            {
                string itemId = StringConvert.ToString(z10[i].Ca_Doc);

                ARInfo ar = list.Find(delegate(ARInfo a)
                {
                    return a.ItemId == itemId;
                }
                    );


                if (ar != null)
                {
                    foreach (ARInfo a in list)
                    {
                        if (a.ItemId == itemId)
                        {
                            a.Amount += StringConvert.ToDecimal(z10[i].Amt_Tax_Base);
                            a.VatAmount += StringConvert.ToDecimal(z10[i].Amt_Tax);
                            a.GAmount += StringConvert.ToDecimal(z10[i].Amt);
                        }
                    }
                }
                else
                {
                    ARInfo obj = new ARInfo();

                    obj.ItemId = StringConvert.ToString(z10[i].Ca_Doc);
                    obj.CaId = StringConvert.ToString(z10[i].Ca_No);
                    obj.DtId = "M" + StringConvert.ToString(z10[i].Main_Transaction) + StringConvert.ToString(z10[i].Sub_Transaction);
                    obj.Description = StringConvert.ToString(z10[i].Text);
                    obj.InvoiceNo = StringConvert.ToString(z10[i].Ref_Doc);
                    obj.InvoiceDt = StringConvert.ToDateTime(z10[i].Doc_Date, provider);
                    obj.GroupInvoiceNo = StringConvert.ToString(z10[i].Classification);
                    obj.Period = StringConvert.ToString(z10[i].Allocation_Date);
                    obj.MeterId = StringConvert.ToString(z10[i].Equip_No);
                    obj.RateTypeId = StringConvert.ToString(z10[i].Rate_Type);
                    obj.MeterReadDt = StringConvert.ToDateTime(z10[i].Meter_Date, provider);
                    obj.ReadUnit = StringConvert.ToDecimal(z10[i].Meter_Unit);
                    obj.LastUnit = StringConvert.ToDecimal(z10[i].Meter_Unit_Last);
                    obj.BaseAmount = StringConvert.ToDecimal(z10[i].Amt_Base);
                    obj.FTUnitPrice = StringConvert.ToDecimal(z10[i].Ft_Unit);
                    obj.FTAmount = StringConvert.ToDecimal(z10[i].Ft_Amt);
                    obj.UnitPrice = StringConvert.ToDecimal(z10[i].Unit_Price);
                    obj.Qty = StringConvert.ToDecimal(z10[i].Quantity);
                    obj.Amount = StringConvert.ToDecimal(z10[i].Amt_Tax_Base);
                    obj.UnitTypeId = StringConvert.ToString(z10[i].Uom);
                    obj.TaxCode = StringConvert.ToString(z10[i].Tax_Code);
                    obj.VatAmount = StringConvert.ToDecimal(z10[i].Amt_Tax);
                    obj.GAmount = StringConvert.ToDecimal(z10[i].Amt);
                    obj.DueDt = StringConvert.ToDateTime(z10[i].Due_Date_Payment, provider);
                    obj.DueDt2 = StringConvert.ToDateTime(z10[i].Deferral, provider);
                    obj.InterestLockFlag = StringConvert.ToString(z10[i].Int_Lock_Reason);
                    obj.InterestKey = StringConvert.ToString(z10[i].Interest_Key);
                    obj.DisconnectDt = StringConvert.ToDateTime(z10[i].Date_Issue, provider);
                    obj.DisconnectDoc = StringConvert.ToString(z10[i].Doc_Disconnect);
                    obj.SubstDocNo = StringConvert.ToString(z10[i].Substitude_No);
                    obj.InstallmentPeriod = StringConvert.ToInt32(z10[i].Repetition_Item);
                    obj.InstallmentTotalPeriod = StringConvert.ToInt32(z10[i].No_Installment);
                    obj.SpotBillInvoiceNo = StringConvert.ToString(z10[i].Device_Value);
                    obj.CancelFlag = StringConvert.ToString(z10[i].Item_Cancled);
                    obj.PaymentOrderFlag = StringConvert.ToString(z10[i].Item_Included);
                    obj.PaymentOrderDt = StringConvert.ToDateTime(z10[i].Date_Payment, provider);
                    obj.ModifiedFlag = StringConvert.ToString(z10[i].Dunn_Lock_Reason);
                    obj.Action = z10[i].Action;

                    obj.SyncFlag = "0";
                    obj.PostDt = DateTime.Now;
                    obj.PostBranchServerId = "";
                    obj.ModifiedDt = DateTime.Now;
                    obj.ModifiedBy = "RFC_CALL";

                    if (z10[i].Id == "TRR0010A")
                        obj.FileType = "A";
                    else if (z10[i].Id == "TRR0010B")
                        obj.FileType = "B";
                    else if (z10[i].Id == "TRR0010C")
                        obj.FileType = "C";
                    else if (z10[i].Id == "TRR0010D")
                        obj.FileType = "D";
                    else if (z10[i].Id == "TRR0010E")
                        obj.FileType = "E";
                    else if (z10[i].Id == "TRR0010F")
                        obj.FileType = "F";


                    list.Add(obj);
                }

            }
            return list;
        }

        private List<PayFromSAPInfo> ConvertToPayFromSAPList(ZCA_TRR0020Table z20)
        {
            IFormatProvider provider = CultureInfo.CreateSpecificCulture("en-EN");
            List<PayFromSAPInfo> list = new List<PayFromSAPInfo>();
            for (int i = 0; i < z20.Count; i++)
            {
                PayFromSAPInfo obj = new PayFromSAPInfo();

                obj.PaymentDocNo = StringConvert.ToString(z20[i].Payment_Doc);
                obj.ReceiptNo = StringConvert.ToString(z20[i].Receipt_No);
                obj.InvoiceNo = StringConvert.ToString(z20[i].Invoice_No);
                obj.CaDoc = StringConvert.ToString(z20[i].Ca_Doc);
                obj.DocType = StringConvert.ToString(z20[i].Doc_Type);
                obj.PaymentDt = StringConvert.ToDateTime(z20[i].Date_Payment, provider);
                obj.PmId = StringConvert.ToString(z20[i].Payment_Id);
                obj.VatAmount = StringConvert.ToDecimal(z20[i].Tax_Amt);
                obj.Amount = StringConvert.ToDecimal(z20[i].Amt);
                obj.CancelFlag = StringConvert.ToString(z20[i].Flag);
                obj.DueDt = StringConvert.ToDateTime(z20[i].Inv_Duedate, provider);
                obj.SyncFlag = "0";
                obj.ModifiedBy = "RFC_CALL";
                obj.ModifiedDt = DateTime.Now;
                obj.Action = z20[i].Action;

                list.Add(obj);
            }

            if (list.Count > 0)
            {
                list.Sort(new ObjectComparer<PayFromSAPInfo>("PaymentDocNo ASC, CaDoc ASC, InvoiceNo ASC, DueDt ASC", true));

                for (int i = 0; i < list.Count; i++)
                {
                    decimal? totalAmount = 0;

                    List<PayFromSAPInfo> ps = list.FindAll(delegate(PayFromSAPInfo p)
                    {
                        return p.PaymentDocNo == list[i].PaymentDocNo;
                    }
                                                );

                    if (ps.Count > 0)
                    {
                        foreach (PayFromSAPInfo p in ps)
                        {
                            totalAmount += p.Amount;
                        }
                    }

                    if (i == 0)
                    {
                        list[i].TotalAmount = totalAmount;
                        list[i].ARPmId = string.Format("ZX{0}{1}", list[i].PaymentDocNo, "00000001");
                        list[i].ARPtId = string.Format("ZX{0}{1}", list[i].PaymentDocNo, "00000001");
                        list[i].ReceiptId = string.Format("ZX{0}{1}", list[i].PaymentDocNo, "01");
                    }
                    else
                    {
                        if (list[i].PaymentDocNo == list[i - 1].PaymentDocNo)
                        {
                            list[i].TotalAmount = totalAmount;
                            list[i].ARPmId = string.Format("ZX{0}{1}", list[i].PaymentDocNo, (Convert.ToInt32(list[i - 1].ARPmId.Substring(14, 8)) + 1).ToString().PadLeft(8, '0'));
                            list[i].ARPtId = list[i - 1].ARPtId;
                            list[i].ReceiptId = string.Format("ZX{0}{1}", list[i].PaymentDocNo, (Convert.ToInt32(list[i - 1].ReceiptId.Substring(14, 2)) + 1).ToString().PadLeft(2, '0'));
                        }
                        else
                        {
                            list[i].TotalAmount = totalAmount;
                            list[i].ARPmId = string.Format("ZX{0}{1}", list[i].PaymentDocNo, "00000001");
                            list[i].ARPtId = string.Format("ZX{0}{1}", list[i].PaymentDocNo, "00000001");
                            list[i].ReceiptId = string.Format("ZX{0}{1}", list[i].PaymentDocNo, "01");
                        }
                    }
                }

            }

            return list;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                FindNow();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string conn = comboBox1.Text;
            BPMSAPProxy submit = new BPMSAPProxy(conn);
            submit.Zpos_Submit(textBox4.Text);
        }

    }
}