using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.NewsBroadcast.Interface.BusinessEntities;

namespace PEA.BPM.NewsBroadcast.Interface.Services
{
    public interface INewsBroadcastService
    {
        List<NewsBroadcastInfo> GetNewsBroadcastNow(DateTime _nowDt, string _userId, int _cmdId);
        List<NewsBroadcastInfo> GetNewsBroadcastHistory(DateTime _nowDt, string _userId, int _cmdId);
        void UpdateNewsBroadcastUserOpened(int _broadcastId, string _userId);
        void UpdateNewsBroadcastUserRead(int _broadcastId, string _userId);
    }
}
