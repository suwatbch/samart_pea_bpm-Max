using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities;

namespace PEA.BPM.Architecture.ArchitectureInterface.Services
{
    public interface IErrorHandlingService
    {
        /// <summary>
        /// ใช้รายงาน exception ที่เกิดขึ้นที่ client กลับมายัง server เพื่อเก็บบันทึกและวิเคราะห์สาเหตุต่อไป
        /// </summary>
        /// <param name="excpetioninfo">ข้อมูล exception</param>
        /// <returns></returns>
        ExceptionDebugInfo ReportAndSaveException(BPMApplicationExceptionInfo excpetioninfo, bool clientack);

        void AcknowledgeException(string debuggingid, string updatestacktrace);
    }
}
