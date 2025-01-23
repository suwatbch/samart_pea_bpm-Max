using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    public interface IListUtility<T>
    {
        string ToStream(); 
        T ParseExtract(string txt); 
    }
}
