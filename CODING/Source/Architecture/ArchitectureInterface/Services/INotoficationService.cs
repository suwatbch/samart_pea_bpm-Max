using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.Architecture.ArchitectureInterface.Services
{
    public interface INotoficationService
    {
        void SendNotificationToEmail(string _errorId, string _errorMsg, int _severity, string _jobId, string _interfaceId, int _row, string _branchId, string _suggMsg);
      
    }
}
