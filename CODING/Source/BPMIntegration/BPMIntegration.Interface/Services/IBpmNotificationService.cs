using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.Integration.BPMIntegration.Interface.Services
{
    public interface IBpmNotificationService
    {
        void SendNotificationToEmail(string _errorId, string _errorMsg, int _severity, string _jobId, string _interfaceId, int _row, string _branchId, string _suggMsg);

    }
}
