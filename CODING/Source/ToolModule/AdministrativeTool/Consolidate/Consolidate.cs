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
        /// �ӡ�����͡ Consolidate ���
        /// </summary>
        /// <param name="filename">����������ͧ������͡</param>
        /// <param name="dataTable">DataTable �ͧ�����ŷ���ͧ������͡</param>
        /// <param name="delimiter">�ӡ���觢������� string ����˹�</param>
        public static void ExportConsolidateFile(string filename, DataTable dataTable, string delimiter)
        {
            using (StreamWriter sw = new StreamWriter(filename, false, Encoding.Default))
            {
                // �Ң��������� Row
                foreach (DataRow row in dataTable.Rows)
                {
                    string rowstr = string.Empty;

                    // �Ң��������� Column
                    foreach (DataColumn col in dataTable.Columns)
                    {
                        string temp;
                        if (row[col.ColumnName] == DBNull.Value)
                        {
                            temp = "";
                        }
                        else
                        {
                            // ��Ǩ�ͺ��Ң������� DateTime ������� �����ӡ�á�˹� Format �ͧ DateTime
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