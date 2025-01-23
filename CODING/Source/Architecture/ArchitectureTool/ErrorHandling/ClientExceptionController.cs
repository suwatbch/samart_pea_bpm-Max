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
        /// ��§ҹ exception ����Դ��鹡�Ѻ价�� server
        /// </summary>
        /// <param name="bpmex"></param>
        private static void ReportException(BPMApplicationException bpmex)
        {
            //-- ����¹ exception ����� info ������� process ����������������������� info
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
            //-- ����� error ��� layer �˹�´٨ҡ��Դ exception
            EErrorInLayer layer = ExceptionHelper.GetClientExceptionLayer(ee);
            //-- ���ҧ��� exception            
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
                //-- ����� BPMApplicationException �������ǡ������������
                bpmex = ee as BPMApplicationException;
                bpmex.UpdateStackTrace(3);
            }
            else
            {
                //-- ����ѧ����� BPMApplicationException ����¹����� BPMApplicationException ���¡�͹
                bpmex = ClientExceptionController.ExceptionHandling(module, ee);
                bpmex.UpdateStackTrace(3);
            }

            try
            {
                if (bpmex.DebuggingId.Length == 0)
                {
                    if (bpmex.Module == EErrorInModule.Unknow) bpmex.Module = module; // ����ѧ����� save ����ѧ����������� error �ͧ module �˹ �� assign module ���
                    ReportException(bpmex);
                }
                else AcknowledgeException(bpmex);
            }
            catch (Exception ex)
            {
                //-- �觢�ͤ�����Ѻ� server ����� log �����������ͧ
                Logger.WriteError("ErrorHandling", "�觢����� error ��Ѻ��ѧ server �����", ex.ToString());
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

            //-- ��ҷӧҹ��������� rethrow exception �������Դ��͢�ҧ�͡
            if (!bpmex.CanContinue) throw bpmex;
        }

    }
}