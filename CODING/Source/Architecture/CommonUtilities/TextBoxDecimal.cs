using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.Architecture.CommonUtilities
{
    public class TextBoxDecimal : System.Windows.Forms.TextBox
    {
        private int maxLengthDecimalPoint = 2;
        public int MaxLengthDecimalPoint
        {
            get { return maxLengthDecimalPoint; }
            set { maxLengthDecimalPoint = value; }
        }

        int WM_KEYDOWN = 0x0100,
            WM_PASTE = 0x0302;

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == WM_PASTE)
            {
                System.Windows.Forms.IDataObject obj = System.Windows.Forms.Clipboard.GetDataObject();
                string input = (string)obj.GetData(typeof(string));
                    foreach (char c in input)
                    {
                        if (!char.IsDigit(c))
                        {
                            m.Result = (IntPtr)0;
                            return;
                        }
                    }
            }
            base.WndProc(ref m);
        }

        protected virtual void OnKeyPress( System.Windows.Forms.KeyPressEventArgs e )
        {
            textboxForDecimal_KeyPress(this, e, maxLengthDecimalPoint);
        }

        public void OnKeyPressValidateDecimal(System.Windows.Forms.KeyPressEventArgs e)
        {
            textboxForDecimal_KeyPress(this, e, maxLengthDecimalPoint);
        }

        private void textboxForDecimal_KeyPress(System.Windows.Forms.TextBox tb, System.Windows.Forms.KeyPressEventArgs e, int maxLengthDecimalPoint)
        {
            int posOfCursor = tb.SelectionStart;
            int posOfDot = tb.Text.IndexOf('.');

            //if (sender == tb)
            if (tb != null)
            {
                //if key in '0-9, ., backspace, delete
                if (char.IsNumber(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == '\b')
                {
                    //Can not key for "."
                    //  - is the first char .
                    //  - more than 1 char in string.
                    if (e.KeyChar == '.')
                    {
                        if (posOfCursor == 0)
                            e.Handled = true;
                        if (tb.Text == "" || tb.Text == String.Empty)
                            e.Handled = true;
                        if (tb.Text.IndexOf('.') >= 0)
                            e.Handled = true;
                    }
                    //Can not key for "number"
                    else if (char.IsNumber(e.KeyChar))
                    {
                        if (tb.Text.Length > 0)
                        {
                            //int posOfCursor = tb.SelectionStart;
                            //int posOfDot = tb.Text.IndexOf('.');
                            //found "."
                            if (posOfDot >= 0)
                            {
                                if (posOfCursor > posOfDot)
                                {
                                    if (tb.Text.Split('.')[1].Length >= maxLengthDecimalPoint)
                                    {
                                        //if (posOfCursor == posOfDot)
                                        if (posOfCursor == tb.Text.Length)
                                            e.Handled = true;
                                        else
                                        {
                                            tb.Text = tb.Text.Substring(0, tb.Text.Length - 1);
                                            tb.SelectionStart = posOfCursor;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    e.Handled = true;
                }
            }

        }


    }
}
