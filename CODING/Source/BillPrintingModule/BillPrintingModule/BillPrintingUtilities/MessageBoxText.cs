using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.BillPrintingModule.BillPrintingUtilities
{
    public class MessageBoxText
    {
        public const string MsgMissingBranchIdMruId = "กรุณาป้อนรหัสการไฟฟ้าและสายการจดหน่วย";
        public const string MsgMissingBranchId = "กรุณาป้อนรหัสการไฟฟ้า";
        public const string MsgMissingMruId = "กรุณาป้อนสายการจดหน่วย";
        public const string MsgMissingCaId = "กรุณาป้อนรหัสผู้ใช้ไฟ (CA)";
        public const string MsgMissingBillSeqNo = "กรุณาป้อนเลขลำดับ BILL_SEQ";
        public const string MsgMissingMtNo = "กรุณาป้อนเลขที่ มท.";
        public const string MsgMissingPaidBy = "กรุณาป้อนรหัสผู้ชำระเงินรวมศูนย์ (paidby)";
        public const string MsgMissingBankDueDate = "กรุณาป้อนวันหักบัญชี";
        public const string MsgMissingBillPeriod = "กรุณาป้อนบิลประจำเดือน";
        public const string MsgMissingElectricId = "กรุณาป้อนรหัสการไฟฟ้า/รหัสผู้ใช้ไฟ/เลขที่บิล";
        public const string MsgMissingPrePrintItem = "ไม่พบรายการพร้อมพิมพ์ กรุณาเลือกรายการที่ต้องการพิมพ์";
        
        public const string MsgWrongFormatBillPeriod = "ป้อนข้อมูลบิลประจำเดือนไม่ถูกต้อง";
        public const string MsgWrongFormatDueDate = "ป้อนข้อมูลวันกำหนดชำระเงินไม่ถูกต้อง";
        public const string MsgWrongFormatDataReceivedDate = "ป้อนข้อมูลวันที่รับข้อมูลไม่ถูกต้อง";
        public const string MsgWrongFormatPrintDate = "ป้อนข้อมูลวันที่พิมพ์ใบแจ้งค่าไฟฟ้าไม่ถูกต้อง";
        public const string MsgWrongBillType = "กรุณาเลือกประเภทใบแจ้งหนี้ให้ถูกต้อง";

        public const string MsgOnlyChildBranchAllowed = "ระบุได้เฉพาะการไฟฟ้าที่อยู่ภายใต้การไฟฟ้าที่ลงทะเบียนเครื่องเท่านั้น";
        public const string MsgDuplicatedElectricId = "คุณป้อนรหัสการไฟฟ้าซ้ำ";
        public const string MsgPrintingBillDone = "ทำการพิมพ์เรียบร้อยแล้ว";
        public const string MsgProcessingDone = "ประมวลผลเรียบร้อยแล้ว";
        public const string MsgConfirmConditionChanged = "คุณต้องการกำหนดเงื่อนไขการพิมพ์ใหม่ ใช่หรือไม่";
        public const string MsgConfirmPrintingBill = "กรุณากด Yes เพื่อยืนยันการพิมพ์ใบเสร็จ";
        public const string MsgGeneralError = "เกิดข้อผิดพลาด : ";
        public const string CaptionWarning = "คำเตือน";
        public const string CaptionError = "ผิดพลาด";
        public const string CaptionConfirm = "ยืนยัน";
        public const string CaptionInfo = "ข้อความ";
       
        public const string CaptionLoadAllReports = "คุณกำลังเรียกแสดงรายงานของการไฟฟ้าทั้งหมด ซึ่งอาจจะใช้เวลานาน";

    }
}
