using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace PEA.BPM.BillPrintingModule.BillPrintingUtilities
{
    public class OverlapHelper
    {
        List<Int64> value1 = new List<Int64>(); // keep "from" value
        List<Int64> value2 = new List<Int64>(); // keep "to" value
        List<String> value3 = new List<String>(); // keep "branch" value
        List<String> result = new List<String>();
        String input1, input2, input3, v3;
        Int64 v1, v2, len; // len for padding check;

        public void Check(string txt)
        {
            char[] seperator = new char[] { '-' };            
            string[] a = new string[3];
            string[] b = txt.Trim().Split(seperator, StringSplitOptions.None);
            len = b[1].Length;

            if (b.Length == 2 || (b.Length == 3 && b[2] == ""))
            {
                a[0] = b[0];
                a[1] = b[1];
                a[2] = b[1];
            }
            else
            {
                a[0] = b[0];
                a[1] = b[1];
                a[2] = b[2];
            }


            //a[0]=branch,a[1]=from,a[2]=to          

            if (value3.Count != 0)
            {
                if (value3[value3.Count-1].ToString() == a[0])
                {
                    input3 = a[0];
                    input1 = a[1];
                    input2 = a[2];
                    v1 = Convert.ToInt64(a[1]);
                    v2 = Convert.ToInt64(a[2]);
                    v3 = a[0];
                }
                else// clear value1 value2 value3 and write the ans into result
                {
                    result = GetResult();
                    value1.Clear();
                    value2.Clear();
                    value3.Clear();

                    input3 = a[0];
                    input1 = a[1];
                    input2 = a[2];
                    v1 = Convert.ToInt64(a[1]);
                    v2 = Convert.ToInt64(a[2]);
                    v3 = a[0];
                }
            }
            else
            {
                input3 = a[0];
                input1 = a[1];
                input2 = a[2];
                v1 = Convert.ToInt64(a[1]);
                v2 = Convert.ToInt64(a[2]);
                v3 = a[0];
            }

            adapt(txt);// txt is useless, we didn't use it, i'll del' it later.

        }

        public Boolean isOverlap()
        {
            //not used anymore
            for (int i = 0; i < value1.Count; i++)
            {
                if ((v1 <= (int)value2[i] && v2 >= (int)value1[i]))
                {
                    Console.WriteLine("Overlapping...");
                    return true;
                }
            }
            return false;
        }

        public int toInt(String t)
        {
            int result1 = 0;
            int acc = 1;

            for (int i = t.Length - 1; i >= 0; i--)
            {
                result1 += (t[i] - '0') * acc;
                acc *= 10;
            }

            return result1;
        }

        public Boolean validateData(String t)
        {
            Boolean flag = true;

            input1 = "";
            input2 = "";

            for (int i = 0; i < t.Length; i++)
            {
                if (t[i] >= '0' && t[i] <= '9')
                {
                    if (flag)
                        input1 += t[i];
                    else
                        input2 += t[i];
                }
                else if (t[i] == '-' && flag && i != 0)
                    flag = false;
                else
                    return false;
            }

            v1 = Convert.ToInt64(input1);
            v2 = Convert.ToInt64(input2);
            if (input2.Length != 0)
                if (v1 > v2)
                    return false;

            return true;
        }

        public void adapt(String t)
        {
            Int64 min, max;
            Boolean f = true; // flag is set as true to mark that this is non-overlap
            ArrayList temp = new ArrayList();
            ArrayList tt = new ArrayList();

            if (input2.Length == 0)
            {
                for (int i = 0; i < value1.Count; i++)
                {
                    if (v1 >= value1[i] && v1 <= value2[i])
                    {
                        // do nothing, because this value has contained in list already
                        f = false;
                        Console.WriteLine("Overlapping...");
                        break;
                    }
                    else if (v1 == value1[i] - 1)
                    {
                        f = false;
                        for (int j = 0; j < value1.Count; j++)
                        {
                            if (value2[j] == v1 - 1)
                            {
                                value2[j] = value2[i]; 
                                value1.RemoveAt(i); 
                                value2.RemoveAt(i);
                            }
                        }
                        break;
                    }
                    else if (v1 == value2[i] + 1)
                    {
                        value2[i] = v1; // now we got {1-1, 3-4, 6-8}
                        f = false;
                        for (int j = 0; j < value1.Count; j++)
                        {
                            if (value1[j] == v1 + 1)
                            {
                                value1[j] = value1[i];
                                value1.RemoveAt(i);
                                value2.RemoveAt(i);
                            }
                        }

                        break;
                    }
                }
                if (f) // this is non-verlap, it's must be added (this is new value)
                {
                    value1.Add(v1);
                    value2.Add(v1);
                    value3.Add(v3);//********************************************************************************
                }
                //for (int i = 0; i < value1.Count; i++)
                //{
                //    Console.WriteLine("{0} - {1}", value1[i], value2[i]);
                //}
            }
            else if (this.isOverlap())
            {
                // find min and max of the existing data in order to adjust a new range
                min = v1; max = v2;

                for (int i = 0; i < value1.Count; i++)
                {
                    if ((v1 <= value2[i] && v2 >= value1[i]))
                    {
                        if (value1[i] <= min)
                            min = value1[i];

                        if (value2[i] >= max)
                            max = value2[i];

                        tt.Add(i);
                    }
                }

                // delete old values that included in the new range already
                for (int i = 0; i < tt.Count; i++)
                {
                    value1.RemoveAt((int)tt[i] - i);
                    value2.RemoveAt((int)tt[i] - i);
                    value3.RemoveAt((int)tt[i] - i);//***************************************************************
                }

                // set a new range
                value1.Add(min);
                value2.Add(max);
                value3.Add(v3);//************************************************************************************

                //for (int i = 0; i < value1.Count; i++)
                //{
                //    Console.WriteLine("{0} - {1}", value1[i], value2[i]);
                //}
            }
            else
            {
                value1.Add(Convert.ToInt64(input1));
                value2.Add(Convert.ToInt64(input2));
                value3.Add(v3);//************************************************************************************

                // Merge
                for (int i = 0; i < value1.Count - 1; i++)
                {
                    if (value2[value1.Count - 1] == value1[i] - 1)
                    {
                        // merge, for ex. if we have {1-5, 9-10, 6-8} 
                        // we will mearge the results to {1-10}
                        value1[i] = value1[value1.Count - 1]; // now we got {1-5, 6-10, 6-8}
                        for (int j = 0; j < value1.Count - 1; j++)
                        {
                            if (value2[j] == value1[value1.Count - 1] - 1)
                            {
                                // we will merge 1-10
                                value2[j] = value2[i]; // we got {1-10, 6-10, 6-8}
                                value1.RemoveAt(i); // remove 6-10 from list
                                value2.RemoveAt(i);
                                value3.RemoveAt(i);//****************************************************************
                                // now we have {1-10, 6-8}
                            }
                        }
                        value1.RemoveAt(value1.Count - 1); // remove 6-8 from list
                        value2.RemoveAt(value2.Count - 1);
                        value3.RemoveAt(value3.Count - 1);//*********************************************************
                        // now we got {1-10}
                        break;
                    }
                    else if (value1[value1.Count - 1] == value2[i] + 1)
                    {
                        // merge, for ex. if we have {1-2, 5-8, 3-4} 
                        // we will mearge the results to {1-8}
                        value2[i] = value2[value1.Count - 1]; // now we got {1-4, 5-8, 3-4}
                        for (int j = 0; j < value1.Count - 1; j++)
                        {
                            if (value1[j] == value2[value1.Count - 1] + 1)
                            {
                                // we will merge 1-8
                                value1[j] = value1[i]; // we got {1-4, 1-8, 3-4}
                                value1.RemoveAt(i); // remove 1-4 from list
                                value2.RemoveAt(i);
                                value3.RemoveAt(i);//****************************************************************
                                // now we have {1-8, 3-4}
                            }
                        }
                        value1.RemoveAt(value1.Count - 1); // remove 3-4 from list
                        value2.RemoveAt(value2.Count - 1);
                        value3.RemoveAt(value3.Count - 1);//*********************************************************
                        // now we got {1-8}
                        break;
                    }
                }

                //for (int i = 0; i < value1.Count; i++)
                //{
                //    Console.WriteLine("{0} - {1}", value1[i], value2[i]);
                //}
            }
        }

        public String get(int i)
        {
            if (value1[i] == value2[i])
                return (value1[i].ToString());

            return (value3[i].ToString() + "-" + value1[i].ToString() + "-" + value2[i].ToString());
        }

        public int getNum()
        {
            return value1.Count;
        }

        public List<String> GetResult()
        {
            //have to check length before return na krub
            for (int i = 0; i < value1.Count; i++)
            {
                if (len == 4)
                    result.Add(value3[i].ToString() + "-" + value1[i].ToString().PadLeft(4, '0') + "-" + value2[i].ToString().PadLeft(4, '0'));
                else 
                    result.Add(value3[i].ToString() + "-" + value1[i].ToString() + "-" + value2[i].ToString());
            }

            return result;
        }
    }
}
