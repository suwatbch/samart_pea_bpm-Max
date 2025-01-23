using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using ArchitectureSG;
using PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.Architecture.ArchitectureTool.ErrorHandling
{
    /// <summary>
    /// 
    /// </summary>
    public class ClientExceptionController
    {
        private static void AcknowledgeException(BPMApplicationException bpmex)
        {
            ErrorHandlingSG.Instance.AcknowledgeException(bpmex.DebuggingId, bpmex.FullStackTrace);
        }

        /// <summary>
        /// รายงาน exception ที่เกิดขึ้นกลับไปที่ server
        /// </summary>
        /// <param name="bpmex"></param>
        private static void ReportException(BPMApplicationException bpmex)
        {
            //-- เปลี่ยน exception ให้เป็น info ที่พร้อม process และใส่ข้อมูลเพิ่มเติมเข้าไปใน info
            BPMApplicationExceptionInfo exinfo = bpmex.GetInfo();

            ExceptionDebugInfo res = ErrorHandlingSG.Instance.ReportAndSaveException(exinfo, true);
            bpmex.ErrorCode = res.ErrorCode;
            bpmex.DebuggingId = res.DebuggingId;
            bpmex.CanContinue = res.CanContinue;
            bpmex.THMessage = res.THMessage;
            bpmex.Cause = res.Cause;
            bpmex.Resolve = res.Resolve;
            bpmex.HelpURL = res.HelpURL;
        }



        public static BPMApplicationException ExceptionHandling(EErrorInModule module, Exception ee)
        {
            ExceptionHelper.PreserveStackTrace(ee);
            //-- หาว่า error ที่ layer ไหนโดยดูจากชนิด exception
            EErrorInLayer layer = ExceptionHelper.GetClientExceptionLayer(ee);
            //-- สร้างตัว exception            
            BPMApplicationException bpmex = ExceptionHelper.ConvertToBPMApplicationException(module, layer, Session.BpmDateTime.Now, ee);
            return bpmex;
        }

        public static void ShowExceptionDialog(EErrorInModule module, Exception ee)
        {
            ShowExceptionDialog(null, module, ee);
        }
        public static void ShowExceptionDialog(Form parent, EErrorInModule module, Exception ee)
        {
            BPMApplicationException bpmex = null;
            Type eetype = ee.GetType();
            if (eetype == typeof(BPMApplicationException))
            {
                //-- ถ้าเป็น BPMApplicationException อยู่แล้วก็เอาไปใช้ได้เลย
                bpmex = ee as BPMApplicationException;
                bpmex.UpdateStackTrace(3);
            }
            else
            {
                //-- ถ้ายังไม่เป็น BPMApplicationException เปลี่ยนให้เป็น BPMApplicationException เสียก่อน
                bpmex = ClientExceptionController.ExceptionHandling(module, ee);
                bpmex.UpdateStackTrace(3);
            }

            try
            {
                if (bpmex.DebuggingId.Length == 0)
                {
                    if (bpmex.Module == EErrorInModule.Unknow) bpmex.Module = module; // ถ้ายังไม่ได้ save และยังไม่รู้ว่าเป็น error ของ module ไหน ก็ assign module ให้
                    ReportException(bpmex);
                }
                else AcknowledgeException(bpmex);
            }
            catch (Exception ex)
            {
                //-- ส่งข้อความกลับไป server ไม่ได้ log ทิ้งไว้ในเครื่อง
                Logger.WriteError("ErrorHandling", "ส่งข้อมูล error กลับไปยัง server ไม่ได้", ex.ToString());
            }

            //if (bpmex.DebuggingId.Length == 0) // show complete error message box
            //{
               //CompleteExceptionForm frm = new CompleteExceptionForm();
               //frm.SetException(bpmex);
               //frm.ShowDialog();
            //}
            //else
            //{
            SimpleExceptionForm frm = new SimpleExceptionForm();
            frm.SetException(bpmex);
            frm.ShowDialog(parent);
            //}

            //-- ถ้าทำงานต่อไม่ได้ก็ rethrow exception ให้ไประเบิดต่อข้างนอก
            if (!bpmex.CanContinue) throw bpmex;
        }

    }
}