using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace PEA.BPM.Architecture.ArchitectureInterface.Services
{
    public interface ICodeTableService
    {
        DataSet GetUpdatedData(DateTime lastUpdatedDate, string branchId);
    }
}
