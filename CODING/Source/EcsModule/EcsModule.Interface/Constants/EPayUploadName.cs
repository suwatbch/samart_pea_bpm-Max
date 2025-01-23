using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace PEA.BPM.EcsModule.Interface.Constants
{
    public static class EPayUploadName
    {
        public static int FILE_MIN_LEN = 133;
        public static int FILE_MAX_LEN = 137;

        public static string[] strName =  { "BRANCH_ID", 
                                            "CA_ID", 
                                            "REC_NO", 
                                            "PERIOD", 
                                            "COMPANY_ID",
                                            "ACT_CODE", 
                                            "POS_NO", 
                                            "INVOICE",
                                            "PAY_DT", 
                                            "DUE_DT",
                                            "OUTSOURCE_AMOUNT", 
                                            "VAT", 
                                            "SYS_AMOUNT", 
                                            "COMPANY_INFO",
                                            "UPLOAD_STATUS",
                                            "FILE_NAME"
                                          };
    }

    [Serializable]
    public class EPaymentUploadStatus
    {
        public static string CLEAR = "0";
        public static string NOCAID = "1";
        public static string LESSDEBT = "2";
        public static string MOREDEBT = "3";
        public static string DUPPLICATE = "4";
        public static string PAID = "5";
        public static string INCONSIS = "6";

        public static string CLEAR_MSG = "ตัดหนี้ได้";
        public static string NOCAID_MSG = "ไม่พบหนี้";
        public static string LESSDEBT_MSG = "น้อยกว่ายอดหนี้";
        public static string MOREDEBT_MSG = "มากกว่ายอดหนี้";
        public static string DUPPLICATE_MSG = "ข้อมูลซ้ำซ้อนในไฟล์";
        public static string PAID_MSG = "มีการชำระเงินในระบบ POS แล้ว";
        public static string INCONSIS_MSG = "ข้อมูลไม่ตรง";

        public static string GetStatusMessage(string code)
        {
            if (code == "0")
                return CLEAR_MSG;
            else if (code == "1")
                return NOCAID_MSG;
            else if (code == "2")
                return LESSDEBT_MSG;
            else if (code == "3")
                return MOREDEBT_MSG;
            else if (code == "4")
                return DUPPLICATE_MSG;
            else if (code == "5")
                return PAID_MSG;
            else if (code == "6")
                return INCONSIS_MSG;
            else
                return null;
        }
    }

    [Serializable]
    public enum CLEARIFY_TYPE : int
    {
        CLEAR_PAYMENT = 0,
        PARTIAL_PAYMENT = 1,
        RETURN_CUSTOMER = 2,
        RETURN_AGENT = 3
            
    }
}
