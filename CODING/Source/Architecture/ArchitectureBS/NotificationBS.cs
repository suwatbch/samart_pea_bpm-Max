using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using PEA.BPM.Architecture.ArchitectureInterface.Services;

namespace PEA.BPM.Architecture.ArchitectureBS
{
    public class NotificationBS : INotoficationService
    {
            private const string WS_NOTIFICATION_ADDR = "WS_NOTIFICATION_ADDR";
            private NotificationWS.BPMNotificationWS _nfWS;

            public NotificationBS()
            {
                _nfWS = new NotificationWS.BPMNotificationWS();
                _nfWS.Timeout = 7200000;
                _nfWS.Url = ConfigurationManager.AppSettings[WS_NOTIFICATION_ADDR];

            }

            public void SendNotificationToEmail(string _errorId, string _errorMsg, int _severity, string _jobId, string _interfaceId, int _row, string _branchId, string _suggMsg)
            {
                _nfWS.SendNotificationToEmail(_errorId, _errorMsg, _severity, _jobId, _interfaceId, _row, _branchId, _suggMsg);
            }
    }
}
