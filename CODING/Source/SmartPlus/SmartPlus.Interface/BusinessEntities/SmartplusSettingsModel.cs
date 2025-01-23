using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PEA.BPM.SmartPlus.Interface.BusinessEntities
{
    public class SmartplusSettingsModel
    {
        public string UserId            { get; set; }
        public string Token             { get; set; }
        public string CurIP             { get; set; }
        public string ServiceEndPoint   { get; set; }
        public string HashPassword      { get; set; }
    }
}
