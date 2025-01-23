using PEA.BPM.Architecture.ArchitectureBS;
using PEA.BPM.Architecture.CommonUtilities;


namespace BPMService.ARCH
{

    public class NotificationWCFService : INotificationWCFService
    {
        private NotificationBS bs;

        public NotificationWCFService()
        {
            bs = new NotificationBS();
        }

        public void SendNotificationToEmail(string _errorId, string _errorMsg, int _severity, string _jobId, string _interfaceId, int _row, string _branchId, string _suggMsg)
        {
            ServiceInspector.Instance.ExceptionTrail(EErrorInModule.Architecture, null, "NotificationWCFService", "SendNotificationToEmail",
                () =>
                {
                    bs.SendNotificationToEmail(_errorId, _errorMsg, _severity, _jobId, _interfaceId, _row, _branchId, _suggMsg);
                });
        }

    }
}
