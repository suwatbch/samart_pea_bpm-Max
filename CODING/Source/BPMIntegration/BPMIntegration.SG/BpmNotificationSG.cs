using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.Integration.BPMIntegration.Interface.Services;
using PEA.BPM.Integration.BPMIntegration.SG.NotificationWCFService;
using System.ServiceModel;
using WCFExtras.Soap;

namespace PEA.BPM.Integration.BPMIntegration.SG
{
    public class BpmNotificationSG : IBpmNotificationService
    {
        private const string WS_BPM_ADDR = "WS_BPM_ADDR";
        private const string WS_BPM_ADDR2 = "WS_BPM_ADDR2"; //Optional for the secondary server in Load Balance
        private NotificationWCFService.NotificationWCFServiceClient _ws;

        public BpmNotificationSG()
        {
            try
            {
                string wsUrl = ConfigurationManager.AppSettings[WS_BPM_ADDR];
                if (string.IsNullOrEmpty(wsUrl)) return; // ถ้า AppConfig ถูก Comment ทิ้ง(บังคับปิดการทำงาน)ให้ return
                _ws = new NotificationWCFServiceClient("BasicHttpBinding_INotificationWCFService", new EndpointAddress(wsUrl));
                _ws.InnerChannel.OperationTimeout = new TimeSpan(0, 5, 0);
                //EndpointAddress endPoint = new EndpointAddress(wsUrl);
                //_ws.Endpoint.Address = endPoint;

            }
            catch (Exception)
            {
                throw;
            }

        }

        public void SendNotificationToEmail(string _errorId, string _errorMsg, int _severity, string _jobId, string _interfaceId, int _row, string _branchId, string _suggMsg)
        {
            if (_ws.Endpoint.Address.IsNone) return; // ถ้าไม่มีการทำหนด Address ให้ return
            bool isCannotSendPrimaryAddress = false;
            try
            {
                _ws.SendNotificationToEmail(_errorId, _errorMsg, _severity, _jobId, _interfaceId, _row, _branchId, _suggMsg);
            }
            catch (Exception)
            {
                isCannotSendPrimaryAddress = true;
            }

            if (isCannotSendPrimaryAddress)
            {
                try
                {
                    string wsUrl = ConfigurationManager.AppSettings[WS_BPM_ADDR2];
                    _ws = new NotificationWCFServiceClient("BasicHttpBinding_INotificationWCFService", new EndpointAddress(wsUrl));
                    _ws.InnerChannel.OperationTimeout = new TimeSpan(0, 5, 0);
                    //EndpointAddress endPoint = new EndpointAddress(wsUrl);
                    //_ws.Endpoint.Address = endPoint;
                    _ws.SendNotificationToEmail(_errorId, _errorMsg, _severity, _jobId, _interfaceId, _row, _branchId, _suggMsg);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

    }
}
