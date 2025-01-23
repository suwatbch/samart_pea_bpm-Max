using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.Architecture.ArchitectureBS
{
    public class ServiceInspector
    {
        #region Singleton
        private static ServiceInspector _instance = new ServiceInspector();
        private ServiceInspector()
        {
            _errorhandlingbs = new ErrorHandlingBS();
            _audittrailbs = new AuditTrailBS();
        }
        public static ServiceInspector Instance
        {
            get { return _instance; }
        }
        #endregion

        private AuditTrailBS _audittrailbs;
        private ErrorHandlingBS _errorhandlingbs;

        public T ExceptionTrail<T>(EErrorInModule bpmmodule, AuthInfo authinfo, string servicename, string methodname, Func<T> func)
        {
            try
            {
                return func();
            }
            catch (Exception ee)
            {
                throw ServerExceptionHandling(authinfo, bpmmodule, ee);
            }
        }
        public void ExceptionTrail(EErrorInModule bpmmodule, AuthInfo authinfo, string servicename, string methodname, Action action)
        {
            try
            {
                action();
            }
            catch (Exception ee)
            {
                throw ServerExceptionHandling(authinfo, bpmmodule, ee);
            }
        }

        #region AuditTrail
        public T AuditTrail<T>(EErrorInModule bpmmodule, AuthInfo authinfo, string servicename, string methodname, Func<T> func)
        {
            string debuggingid = null;
            DateTime executiondate = DateTime.Now;
            Stopwatch stopwatch = Stopwatch.StartNew();
            try
            {
                return func();
            }
            catch (Exception ee)
            {
                var exp = ServerExceptionHandling(authinfo, bpmmodule, ee);
                debuggingid = exp.Detail.DebuggingId;
                throw exp;
            }
            finally
            {
                stopwatch.Stop();
                SaveMethodCall(bpmmodule, servicename, methodname, authinfo, executiondate, stopwatch.ElapsedMilliseconds, debuggingid);
                //Debug.WriteLine("Execute : [" + servicename + "] [" + stopwatch.ElapsedMilliseconds + "] ms.");
            }
        }

        public void AuditTrail(EErrorInModule bpmmodule, AuthInfo authinfo, string servicename, string methodname, Action action)
        {
            string debuggingid = null;
            DateTime executiondate = DateTime.Now;
            Stopwatch stopwatch = Stopwatch.StartNew();
            try
            {
                action();
            }
            catch (Exception ee)
            {
                var exp = ServerExceptionHandling(authinfo, bpmmodule, ee);
                debuggingid = exp.Detail.DebuggingId;
                throw exp;
            }
            finally
            {
                stopwatch.Stop();
                SaveMethodCall(bpmmodule, servicename, methodname, authinfo, executiondate, stopwatch.ElapsedMilliseconds, debuggingid);
                //Debug.WriteLine("Execute : [" + servicename + "] [" + stopwatch.ElapsedMilliseconds + "] ms.");
            }
        }

        private void SaveMethodCall(EErrorInModule bpmmodule, string servicename, string methodname, AuthInfo authinfo, DateTime executiondate, long executiontime, string debuggingid)
        {
            string userid = "";
            string localip = "";
            if (authinfo != null)
            {
                if (!string.IsNullOrEmpty(authinfo.UserId)) userid = authinfo.UserId;
                if (!string.IsNullOrEmpty(authinfo.LocalIP)) localip = authinfo.LocalIP;
            }
            _audittrailbs.SaveMethodCall((int)bpmmodule, servicename, methodname, userid, localip, executiondate, executiontime, debuggingid);
        }
        #endregion

        #region จัดการ Exception
        private FaultException<BPMApplicationExceptionInfo> ServerExceptionHandling(AuthInfo authinfo, EErrorInModule module, Exception ee)
        {
            ExceptionHelper.PreserveStackTrace(ee);
            //-- หาว่า error ที่ layer ไหนโดยดูจากชนิด exception
            EErrorInLayer layer = ExceptionHelper.GetServerExceptionLayer(ee);
            //-- สร้างตัว exception
            BPMApplicationException bpmex = ExceptionHelper.ConvertToBPMApplicationException(module, layer, DateTime.Now, ee);
            //-- เปลี่ยน exception ให้เป็น info ที่พร้อม process และใส่ข้อมูลเพิ่มเติมเข้าไปใน info
            BPMApplicationExceptionInfo exinfo = bpmex.GetInfo();
            if (authinfo != null)
            {
                exinfo.UserId = authinfo.UserId;
                exinfo.AuthToken = authinfo.AuthToken;
            }
            //-- เอา info ไป save ลง database
            ExceptionDebugInfo res = ReportAndSaveException(exinfo); // get error code + record to database
            exinfo.ErrorCode = res.ErrorCode;
            exinfo.DebuggingId = res.DebuggingId;
            exinfo.CanContinue = res.CanContinue;
            exinfo.THMessage = res.THMessage;
            exinfo.Cause = res.Cause;
            exinfo.Resolve = res.Resolve;
            exinfo.HelpURL = res.HelpURL;

            //-- เอาไปสร้างเป็น SoapExcetion เพื่อเตรียมส่งกลับไปยัง client
            //return ExceptionHelper.CreateSoapException(exinfo); // ไม่ใช้แล้วเปลี่ยนไปเป็นแบบ FaultException แทนเพราะเป็น WCF
            return ExceptionHelper.CreateTypedFaultException(exinfo);
        }
        private ExceptionDebugInfo ReportAndSaveException(BPMApplicationExceptionInfo exceptioninfo)
        {
            return _errorhandlingbs.ReportAndSaveException(exceptioninfo, false);
        }
        #endregion


    }
}
