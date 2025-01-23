using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.PaymentManagementModule.Interface.BusinessEntities;
using System.Windows.Forms;

namespace PEA.BPM.PaymentManagementModule
{
    internal class ModuleHelper
    {

        internal static bool CheckDuplicateAPItem(List<ToBePaidAP> toBePaidAP, APInfo ap)
        {
            foreach (ToBePaidAP toBePaid in toBePaidAP)
            {
                if (toBePaid.PaymentVoucher != null && toBePaid.PaymentVoucher.Length > 0 && toBePaid.PaymentVoucher == ap.PaymentVoucher)
                {
                    return false;
                }
            }

            return true;
        }
        
        internal static void Alert(string msg)
        {
            MessageBox.Show(msg, "ข้อความเตือน", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        internal static void AlertAndFocusTextBox(string msg, object obj)
        {
            Alert(msg);
            if(obj is TextBox)
            {
                TextBox tb = (obj as TextBox);
                tb.SelectAll();
                tb.Focus();
            }
            else if( obj is MaskedTextBox)
            {
                MaskedTextBox tb = (obj as MaskedTextBox);
                tb.SelectAll();
                tb.Focus();
            }
            
        }
    }
}
