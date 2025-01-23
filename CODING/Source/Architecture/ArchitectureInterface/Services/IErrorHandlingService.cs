using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities;

namespace PEA.BPM.Architecture.ArchitectureInterface.Services
{
    public interface IErrorHandlingService
    {
        /// <summary>
        /// ����§ҹ exception ����Դ��鹷�� client ��Ѻ���ѧ server �����纺ѹ�֡��������������˵ص���
        /// </summary>
        /// <param name="excpetioninfo">������ exception</param>
        /// <returns></returns>
        ExceptionDebugInfo ReportAndSaveException(BPMApplicationExceptionInfo excpetioninfo, bool clientack);

        void AcknowledgeException(string debuggingid, string updatestacktrace);
    }
}
