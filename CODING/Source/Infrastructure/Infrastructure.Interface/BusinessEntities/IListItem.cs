using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.Infrastructure.Interface.BusinessEntities
{

    //[DataMember(Order=1)]
    public interface IListItem
    {
        string DisplayText { get;}
    }
}
