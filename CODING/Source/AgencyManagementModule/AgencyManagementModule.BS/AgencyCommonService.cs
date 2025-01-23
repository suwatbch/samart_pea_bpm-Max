using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.AgencyManagementModule.Interface.Services;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using PEA.BPM.AgencyManagementModule.DA;
using PEA.BPM.AgencyManagementModule.BS.Constants;
using System.ComponentModel;

namespace PEA.BPM.AgencyManagementModule.BS
{
    public class AgencyCommonService : IAgencyCommonService
    {
        private IAgencyRepository _agencyrepo;
        private IBillBookRepository _billbookrepo;
        public AgencyCommonService()
            : this(new AgencyDataAccess(), new BillBookDataAccess())
        {
        }
        public AgencyCommonService(IAgencyRepository agencyrepo, IBillBookRepository billbookrepo)
        {
            _agencyrepo = agencyrepo;
            _billbookrepo = billbookrepo;
        }

        private List<string> GetLineList(string lineKey)
        {
            //parse 0001-0009;0020
            List<string> mruList = new List<string>();
            char[] spliter1 = { ';', ':', ',' };
            string[] sp = lineKey.Split(spliter1);
            foreach (string st in sp)
            {
                string str = st.Replace(" ", ""); //remove space 
                char[] spliter2 = { '-' };
                string[] lines = str.Split(spliter2);

                if (lines.Length == 1)
                {
                    if (lines[0].Length == BSConstant.MruIdLength)
                        mruList.Add(lines[0]);

                    continue;
                }

                Exception ei = new Exception("ป้อนช่วงของสายการเก็บเงินผิด");
                if (lines.Length > 2) throw ei;

                try
                {
                    int start = Convert.ToInt32(lines[0]);
                    int end = Convert.ToInt32(lines[1]);
                    if (end < start) throw ei;
                    for (int j = start; j <= end; j++)
                        mruList.Add(j.ToString().PadLeft(BSConstant.MruIdLength, '0'));
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            return mruList;
        }

        public AgentInfo FindAndDisplayAgentSearchInfo(string agentId)
        {
            IAgencyRepository repo = _agencyrepo.NewInstance();
            AgentInfo agentInfo = repo.GetAgentInformation(agentId);
            return agentInfo;
        }

        public PeaInfo FindAndDisplayBranchSearchInfo(string basedBranchId, string branchId)
        {
            IAgencyRepository repo = _agencyrepo.NewInstance();
            PeaInfo peaInfo = null;
            peaInfo = repo.GetBranchInformation(branchId);
            return peaInfo;
        }

        public void FindAndDisplayBranchInfo(string branchId)
        {
            IAgencyRepository repo = _agencyrepo.NewInstance();
            PeaInfo peaInfo = null;
            peaInfo = repo.GetBranchInformation(branchId);
        }

        public BindingList<LineInfo> FindAndDisplayLineSearchInfo(string branchId, string lineKey)
        {
            //line = MRU
            IAgencyRepository repo = _agencyrepo.NewInstance();
            BindingList<LineInfo> lineInfoList = new BindingList<LineInfo>();

            List<string> lineList = GetLineList(lineKey);
            lineList.Sort();

            for (int i = 0; i < lineList.Count; i++)
            {
                LineInfo lineInfo = repo.GetLineInformation(lineList[i], branchId);
                if (lineInfo != null)
                {
                    lineInfo.AdvPayment = true;
                    lineInfoList.Add(lineInfo);
                }
            }
            return lineInfoList;
        }

        public List<PeaInfo> GetBranches( string branchId)
        {
            IAgencyRepository repo = _agencyrepo.NewInstance();
            List<PeaInfo> branchList = repo.SelectBranchLevelFour(branchId);
            return branchList;
        }

        public HashInfoCollection GetBillStatusList(string bsId)
        {
            HashInfoCollection retVals = new HashInfoCollection();
            retVals = _billbookrepo.GetBillStatusInformation(bsId);
            return retVals;
        }

        public string GetContractTypeList(string ctId)
        {
            string retVal = String.Empty;
            retVal = _billbookrepo.GetContractTypeInformation(ctId);               
            return retVal;
        }

        public HashInfoCollection GetPmList(string pmId)
        {
            HashInfoCollection retVals = new HashInfoCollection();
            retVals = _billbookrepo.GetPmInformation(pmId);                
            return retVals;
        }

        public HashInfoCollection GetAbsList(string absId)
        {
            HashInfoCollection retVals = new HashInfoCollection();
            retVals = _billbookrepo.GetAbsInformation(absId);                
            return retVals;
        }



    }
}
