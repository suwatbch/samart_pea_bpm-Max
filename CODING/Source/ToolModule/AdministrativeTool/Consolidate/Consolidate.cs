using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace AdministrativeTool.Consolidate
{
    public class Consolidate
    {
        #region Method : ExportConsolidateFile()
        /// <summary>
        /// ทำการส่งออก Consolidate ไฟล์
        /// </summary>
        /// <param name="filename">ชื่อไฟล์ที่ต้องการส่งออก</param>
        /// <param name="dataTable">DataTable ของข้อมูลที่ต้องการส่งออก</param>
        /// <param name="delimiter">ทำการแบ่งข้อมูลโดย string ที่กำหนด</param>
        public static void ExportConsolidateFile(string filename, DataTable dataTable, string delimiter)
        {
            using (StreamWriter sw = new StreamWriter(filename, false, Encoding.Default))
            {
                // หาข้อมูลแต่ละ Row
                foreach (DataRow row in dataTable.Rows)
                {
                    string rowstr = string.Empty;

                    // หาข้อมูลแต่ละ Column
                    foreach (DataColumn col in dataTable.Columns)
                    {
                        string temp;
                        if (row[col.ColumnName] == DBNull.Value)
                        {
                            temp = "";
                        }
                        else
                        {
                            // ตรวจสอบว่าข้อมูลเป็น DateTime หรือไม่ ถ้าใช่ทำการกำหนด Format ของ DateTime
                            if (row[col.ColumnName].GetType().Name == "DateTime")
                            {
                                temp = ((DateTime)row[col.ColumnName]).ToString("yyyy-MM-dd HH:mm:ss");
                            }
                            else
                            {
                                temp = row[col.ColumnName].ToString();
                            }
                        }

                        if (string.IsNullOrEmpty(rowstr))
                        {
                            rowstr = temp;
                        }
                        else
                        {
                            rowstr += delimiter + temp;
                        }
                    }

                    sw.WriteLine(rowstr);
                }
                sw.Close();
            }
        }
        #endregion
    }
}