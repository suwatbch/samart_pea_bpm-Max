using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using PEA.BPM.Architecture.ArchitectureDA.AuditTrailDSTableAdapters;

namespace PEA.BPM.Architecture.ArchitectureDA
{
   public class AuditTrailDA
   {

      public void SaveMethodCall(int bpmmodule, string servicename, string methodname, string userid, string localip, DateTime executiondate, long executiontime, string debuggingid)
      {
         string connstr = ConfigurationManager.ConnectionStrings["AuditDatabase"].ConnectionString;
         using (SqlConnection sqlconn = new SqlConnection(connstr))
         {
            tAuditMethodCallTableAdapter ada = new tAuditMethodCallTableAdapter();
            ada.Connection = sqlconn;
            ada.Insert(bpmmodule, servicename, methodname, userid, localip, executiondate, executiontime, debuggingid);
         }
      }


   }
}
