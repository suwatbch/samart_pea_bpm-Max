using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.ArchitectureDA;
using System.Data;
using PEA.BPM.Architecture.ArchitectureInterface.Services;

namespace PEA.BPM.Architecture.ArchitectureBS
{
    public class CodeTableBS : ICodeTableService
    {
        public DataSet GetUpdatedData(DateTime lastUpdatedDate, string branchId)
        {
            CodeTableDA da = new CodeTableDA();
            return da.GetUpdatedData(lastUpdatedDate, branchId);
        }
    }
}
