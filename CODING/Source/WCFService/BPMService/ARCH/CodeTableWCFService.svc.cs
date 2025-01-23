using System;
using System.Data;
using PEA.BPM.Architecture.ArchitectureBS;
using PEA.BPM.Architecture.CommonUtilities;


namespace BPMService.ARCH
{

    public class CodeTableWCFService : ICodeTableWCFService
    {
        private CodeTableBS _bs;

        public CodeTableWCFService()
        {
            _bs = new CodeTableBS();
        }

        public CompressData GetUpdatedData(DateTime lastModifiedDt, string branchId)
        {
            return ServiceInspector.Instance.AuditTrail(EErrorInModule.Architecture, null, "CodeTableWCFService", "GetUpdatedData",
                () =>
                {
                    return ServiceHelper.CompressDataBF<DataSet>(_bs.GetUpdatedData(lastModifiedDt, branchId));
                });
        }
    }


}
