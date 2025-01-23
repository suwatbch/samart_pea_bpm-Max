using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities;
using ProtoBuf;

namespace AdministrativeTool.OpenOfflineFile
{
    public class OpenOfflineFile
    {
        #region Method : GetOfflineItemsFromFile()
        /// <summary>
        /// �ӡ���Դ Offline ���
        /// </summary>
        /// <param name="filename">����������ͧ����Դ</param>
        /// <returns>������ OfflineItems �����ҡ���</returns>
        public static OfflineItems GetOfflineItemsFromFile(string filename)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                OfflineItems obj = Serializer.Deserialize<OfflineItems>(fs);
                return obj;
            }
        }
        #endregion
    }
}