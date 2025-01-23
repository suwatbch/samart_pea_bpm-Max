using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ProtoBuf;

namespace PEA.BPM.ePaymentsModule.Interface.BusinessEntities
{    
    public static class ObjectClone
    {
        public static EpayUploadItem GetClone(EpayUploadItem cloneThis)
        {
            MemoryStream ptMs = new MemoryStream();
            Serializer.Serialize<EpayUploadItem>(ptMs, cloneThis);
            ptMs.Flush();
            ptMs.Position = 0;
            EpayUploadItem clone = Serializer.Deserialize<EpayUploadItem>(ptMs);
            return clone;
        } 
    }

}
