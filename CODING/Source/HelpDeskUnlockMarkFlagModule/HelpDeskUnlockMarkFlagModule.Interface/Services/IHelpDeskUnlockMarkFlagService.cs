using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.HelpDeskUnlockMarkFlagModule.Interface.BusinessEntities;
using System.Data.Common;

namespace PEA.BPM.HelpDeskUnlockMarkFlagModule.Interface.Services
{
    public interface IHelpDeskUnlockMarkFlagService
    {
        string                          Login(string UserId, string Password);       
        UnlockMarkFlagServiceInfo       UnlockMarkFlagService(string invoiceNo);
        SearchARInfo                    SearchARInfo(string CaId, string Period, double GAmount);
        string                          CheckToken(string UserId, string Token);

    }
}
