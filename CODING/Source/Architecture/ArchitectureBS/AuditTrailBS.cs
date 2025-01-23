using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PEA.BPM.Architecture.ArchitectureDA;
using PEA.BPM.Architecture.ArchitectureInterface.Services;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.Architecture.ArchitectureBS
{
   public class AuditTrailBS : IAuditTrailService
   {
      AuditTrailDA _audittrailda = new AuditTrailDA();

      public void SaveMethodCall(int bpmmodule, string servicename, string methodname, string userid, string localip, DateTime executiondate, long executiontime, string debuggingid)
      {
         try
         {
             _audittrailda.SaveMethodCall(bpmmodule, servicename, methodname, userid, localip, executiondate, executiontime, debuggingid);
         }
         catch (Exception)
         {
            // TODO: save data ลง database, log error เก็บไว้
         }
      }
   }
}
