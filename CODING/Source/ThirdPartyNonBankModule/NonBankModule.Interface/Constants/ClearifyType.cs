using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.NonBankModule.Interface.Constants
{
    public static class ClearifyType
    {
        public static string PAID_SOME = "1";
        public static string CA_REFUND = "2";
        public static string AGENT_REFUND = "3";

        public static string PAID_SOME_MSG = "�����Թ�ҧ��ǹ";
        public static string CA_REFUND_MSG = "�׹�Թ��������";
        public static string AGENT_REFUND_MSG = "�׹�Թ�����᷹";

        public static string GetStatusMessage(string code)
        {
            if (code == "1")
                return PAID_SOME_MSG;
            else if (code == "2")
                return CA_REFUND_MSG;
            else if (code == "3")
                return AGENT_REFUND_MSG;
            else
                return null;
        }
    }
}
