using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using PEA.BPM.Architecture.ArchitectureDA.ErrorHandlingDSTableAdapters;
using PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities;
using System.Data.SqlClient;

namespace PEA.BPM.Architecture.ArchitectureDA
{
    public class ErrorHandlingDA
    {
        /// <summary>
        /// group message ที่ซ้ำๆ กันแต่มีเลข running อยู่ใน message ให้เป็นชุดเดียวกัน
        /// </summary>
        /// <param name="message">Message</param>
        /// <returns>Message ที่ group แล้ว</returns>
        private string SearchForGrouping(string message)
        {
            string connstr = ConfigurationManager.ConnectionStrings["AuditDatabase"].ConnectionString;
            using (SqlConnection sqlconn = new SqlConnection(connstr))
            {
                mErrorGroupingTableAdapter ada = new mErrorGroupingTableAdapter();
                ada.Connection = sqlconn;
                ErrorHandlingDS.mErrorGroupingDataTable dt = ada.GetData();
                string lowermsg = message.ToLower();
                foreach (ErrorHandlingDS.mErrorGroupingRow drow in dt)
                {
                    //-- หาดูว่า match กับ case ไหนบ้าง ถ้าเจอว่า match ก็ส่ง GMessage ออกไป
                    if (lowermsg.StartsWith(drow.RegularExp.ToLower())) return drow.GMessage;
                }
            }
            return message; // ไม่ match กับใครเลยก็ส่งตัวมันเองออกไป
        }
        /// <summary>
        /// สร้าง ErrorCode จาก Module + Layer + Running ตาม message
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="trans"></param>
        /// <param name="exceptioninfo"></param>
        /// <returns></returns>
        private string CreateErrorCodeFromInfo(SqlConnection conn, SqlTransaction trans, BPMApplicationExceptionInfo exceptioninfo)
        {
            string prefixcode = exceptioninfo.Module.ToString("00") + exceptioninfo.Layer.ToString("0");
            mErrorMessageKeyTableAdapter ada = new mErrorMessageKeyTableAdapter();
            ada.Connection = conn;
            ada.SetTransaction(trans);
            int runningcode = (int)ada.GetNextKeyByKeyName(prefixcode);
            return prefixcode + "-" + runningcode.ToString("0000");
        }
        private string CreateDebuggingID(SqlConnection conn, SqlTransaction trans)
        {
            mErrorMessageKeyTableAdapter ada = new mErrorMessageKeyTableAdapter();
            ada.Connection = conn;
            ada.SetTransaction(trans);
            int runningcode = (int)ada.GetNextKeyByKeyName("DebuggingID");
            DateTime dtnow = DateTime.Now;
            return dtnow.Year.ToString() + dtnow.ToString("MMdd") + "-" + runningcode.ToString();
        }
        private ExceptionDebugInfo SaveErrorMessage(SqlConnection conn, SqlTransaction trans, string groupedmessage, BPMApplicationExceptionInfo exceptioninfo, bool clientack)
        {
            ExceptionDebugInfo edinfo = new ExceptionDebugInfo();

            mErrorMessageTableAdapter ada = new mErrorMessageTableAdapter();
            ada.Connection = conn;
            ada.SetTransaction(trans);

            tErrorMessageTableAdapter adadetail = new tErrorMessageTableAdapter();
            adadetail.Connection = conn;
            adadetail.SetTransaction(trans);

            DateTime dtnow = DateTime.Now;
            int merrorkey;
            ErrorHandlingDS.mErrorMessageDataTable dt = ada.GetOneByMessageModuleLayer(groupedmessage, exceptioninfo.Module, exceptioninfo.Layer);
            if (dt.Count == 0) //-- ยังไม่มีใน database ให้ สร้างอันใหม่
            {
                //-- save mErrorMessage
                edinfo.ErrorCode = CreateErrorCodeFromInfo(conn, trans, exceptioninfo);
                object obj = ada.CreateOne(
                    edinfo.ErrorCode,
                    exceptioninfo.OriginalType,
                    exceptioninfo.Module,
                    exceptioninfo.Layer,
                    groupedmessage,
                    exceptioninfo.StackTrace,
                    exceptioninfo.Source,
                    exceptioninfo.CanContinue,
                    dtnow);
                merrorkey = (int)obj;
            }
            else
            {
                ErrorHandlingDS.mErrorMessageRow drow = dt[0];
                merrorkey = drow.EMID;
                edinfo.ErrorCode = drow.ErrorCode;
                edinfo.CanContinue = drow.CanContinue;
                edinfo.THMessage = drow.THMessage;
                edinfo.Cause = drow.Cause;
                edinfo.Resolve = drow.Resolve;
                edinfo.HelpURL = drow.HelpURL;
            }

            //-- save tErrorMessage
            edinfo.DebuggingId = CreateDebuggingID(conn, trans);
            adadetail.Insert(merrorkey,
                             dtnow,
                             exceptioninfo.UserId,
                             edinfo.DebuggingId,
                             exceptioninfo.Message,
                             exceptioninfo.StackTrace,
                             clientack);

            ada.UpdateOccur(dtnow, merrorkey);
            return edinfo;
        }

        public ExceptionDebugInfo ReportAndSaveException(BPMApplicationExceptionInfo exceptioninfo, bool clientack)
        {
            string groupedmessage = SearchForGrouping(exceptioninfo.Message);

            string connstr = ConfigurationManager.ConnectionStrings["AuditDatabase"].ConnectionString;
            using (SqlConnection sqlconn = new SqlConnection(connstr))
            {
                sqlconn.Open();
                SqlTransaction trans = sqlconn.BeginTransaction("BPMExceptionAuditTrans");
                try
                {
                    ExceptionDebugInfo edinfo = SaveErrorMessage(sqlconn, trans, groupedmessage, exceptioninfo, clientack);
                    trans.Commit();
                    return edinfo;
                }
                catch
                {
                    trans.Rollback();
                    throw;
                }
            } // end sqlconnection
        }


        public ExceptionDebugInfo GetExceptionResolveInfo(string errorcode)
        {
            string connstr = ConfigurationManager.ConnectionStrings["AuditDatabase"].ConnectionString;
            using (SqlConnection sqlconn = new SqlConnection(connstr))
            {
                ExceptionDebugInfo edinfo = new ExceptionDebugInfo();

                mErrorMessageTableAdapter ada = new mErrorMessageTableAdapter();
                ada.Connection = sqlconn;
                ErrorHandlingDS.mErrorMessageDataTable dt = ada.GetByErrorCode(errorcode);
                if (dt.Count == 0) return edinfo;
                ErrorHandlingDS.mErrorMessageRow drow = dt[0];

                edinfo.ErrorCode = drow.ErrorCode;
                edinfo.THMessage = drow.THMessage;
                edinfo.Cause = drow.Cause;
                edinfo.Resolve = drow.Resolve;
                edinfo.HelpURL = drow.HelpURL;
                edinfo.CanContinue = drow.CanContinue;
                return edinfo;
            }
        }

        public void AcknowledgeException(string debuggingid, string updatestacktrace)
        {
            string connstr = ConfigurationManager.ConnectionStrings["AuditDatabase"].ConnectionString;
            using (SqlConnection sqlconn = new SqlConnection(connstr))
            {
                tErrorMessageTableAdapter ada = new tErrorMessageTableAdapter();
                ada.Connection = sqlconn;
                ada.UpdateOneStackAndClientAck(updatestacktrace, debuggingid);
            }
        }
    }
}
