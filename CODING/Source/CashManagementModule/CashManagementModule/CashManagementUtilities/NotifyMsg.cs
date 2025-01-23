using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using PEA.BPM.CashManagementModule.Interface.BusinessEntities;
using PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities;

namespace PEA.BPM.CashManagementModule.CashManagementUtilities
{
    public class NotifyMsg
    {
        public static void ShowForceCloseWorkMsg(WorkStatus whom)
        {
            string cashierTxt = string.Format("({0}) - {1}", whom.CloseWorkBy, whom.CashierName);
            MessageBox.Show("ระบบได้ทำการปิดกะนี้ไปแล้ว \n\nผู้ปิดกะ: " + cashierTxt, "สถานะปิดกะ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }
    }
}
