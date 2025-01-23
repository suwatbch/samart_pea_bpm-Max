using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.BillPrintingModule.Interface.BusinessEntities;

namespace PEA.BPM.BillPrintingModule.Interface.Services
{
    public interface IBillPrintingServices
    {
        //normal blue
        List<Bills> PrintBlueBill(BillPrintingConditionParam param);
        List<Bills> ReprintBlueBill(BillPrintingConditionParam param);

        //normal green
        List<Bills> PrintGreenBill(BillPrintingConditionParam param);
        List<Bills> ReprintGreenBill(BillPrintingConditionParam param);

        //Normal A4
        List<Bills> PrintA4Bill(BillPrintingConditionParam param);
        
        //Group Invoice
        List<Bills> PrintGroupInvoiceA4Bill(BillPrintingConditionParam param);
        List<Bills> ReprintGroupInvoiceA4Bill(BillPrintingConditionParam param);

        //green by bank
        List<Bills> PrintGreenBillByBank(BillPrintingConditionParam param);
        List<Bills> ReprintGreenBillByBank(BillPrintingConditionParam param);

        //blue by bank
        List<Bills> PrintBlueBillByBank(BillPrintingConditionParam param);
        List<Bills> ReprintBlueBillByBank(BillPrintingConditionParam param);

        //green receipt        
        List<Bills> PrintGreenReceipt(BillPrintingConditionParam param);
        List<Bills> ReprintGreenReceipt(BillPrintingConditionParam param);

        //spot bill
        List<Bills> PrintSpotBill(BillPrintingConditionParam param);
        List<Bills> ReprintSpotBill(BillPrintingConditionParam param);

        /// <summary>
        /// CheckExistRecord - normal blue/green
        /// CheckExistReprintRecord - reprint normal blue/green
        /// CheckExistGroupInvoiceRecord - group invoice
        /// CheckExistReprintGroupInvoiceRecord - reprint group invoice
        /// CheckExistByBank - by bank blue/green (z000)
        /// CheckExistReprintByBank - reprint by bank blue/green (z000)
        /// CheckExistGreenReceipt - print only receipt of green bill
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        List<PrintableId> CheckExistRecord(BillPrintingConditionParam param);
        List<PrintableId> CheckExistReprintRecord(BillPrintingConditionParam param);
        List<PrintableId> CheckExistGroupInvoiceRecord(GroupInvoiceParam param);
        List<PrintableId> CheckExistReprintGroupInvoiceRecord(GroupInvoiceParam param);
        List<PrintableIdByBank> CheckExistByBank(GreenBillParam param);
        List<PrintableIdByBank> CheckExistReprintByBank(GreenBillReprintParam param);
        List<PrintableId> CheckExistGreenReceipt(BillPrintingConditionParam param);
        List<PrintableId> CheckExistReprintGreenReceipt(BillPrintingConditionParam param);
    }
}
