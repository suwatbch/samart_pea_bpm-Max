using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PEA.BPM.Architecture.ArchitectureInterface.Services
{
   public interface IAuditTrailService
   {
       void SaveMethodCall(int bpmmodule, string servicename, string methodname, string userid, string localip, DateTime executiondate, long executiontime, string debuggingid);
   }
}
