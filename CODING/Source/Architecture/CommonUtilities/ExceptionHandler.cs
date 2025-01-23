using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace PEA.BPM.Architecture.CommonUtilities
{
    public class ExceptionHandler
    {
        public static void HandleError(string moduleName, string title, Exception ex, Form parent)
        {
            Logger.WriteError(moduleName, title, ex.ToString());
            if (ex.GetType() == typeof(SystemException))
            {
                MessageBox.Show(parent, ex.Message, "¢ÈÕº‘¥æ≈“¥", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show(parent, ex.ToString(), "¢ÈÕº‘¥æ≈“¥", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void HandleError(string moduleName, string title, Exception ex)
        {
            Logger.WriteError(moduleName, title, ex.ToString());
            if (ex.GetType() == typeof(SystemException))
            {
                MessageBox.Show(ex.Message, "¢ÈÕº‘¥æ≈“¥", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show(ex.ToString(), "¢ÈÕº‘¥æ≈“¥", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
