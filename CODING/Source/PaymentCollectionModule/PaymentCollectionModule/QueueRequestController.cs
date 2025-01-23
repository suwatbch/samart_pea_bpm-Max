using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Infrastructure.Interface;
using Microsoft.Practices.ObjectBuilder;
using System.IO;
using System.Windows.Forms;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.PaymentCollectionModule
{
    public class QueueRequestController : WorkItemController
    {
        [InjectionConstructor]
        public QueueRequestController()
        {
        }
        
        public void Run()
        {
            /// Write file to specified path with Queue Application
            /// 

            string path = LocalSettingHelper.Instance().ReadString("QueueFilePath");
            if (path != null)
            {
                if (Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                DirectoryInfo dinf = new DirectoryInfo(path);

                using (FileStream fs = new FileStream(
                    string.Format(@"{0}\{1}", dinf.FullName, "queue.txt"),
                    FileMode.Create))
                {
                    StreamWriter sw = new StreamWriter(fs);
                    sw.Write(DateTime.Now.ToString("hh:mm:ss"));
                    sw.Close();
                    fs.Close();

                    this.SetStatusText("ระบบได้ทำการส่งข้อมูลเรียก 'ผู้รอรับบริการถัดไป' ไปยังระบบจัดการคิวแล้ว...");
                }
            }
        }
    }
}
