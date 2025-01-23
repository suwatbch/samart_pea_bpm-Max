using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.AgencyManagementModule.Interface.Services;
using PEA.BPM.AgencyManagementModule.Interface.BusinessEntities;
using PEA.BPM.AgencyManagementModule.DA;
using System.ComponentModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.AgencyManagementModule.BS
{
    public class AgencyPlanningService : IAgencyPlanningService
    {
        public bool SaveAssignedLineofAgent(DbTransaction trans, BindingList<LineInfo> asiLineList)
        {
            AgencyDataAccess agentDa = new AgencyDataAccess();
            //int count = 0;
            Database db = DatabaseFactory.CreateDatabase("POSDatabase");
            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();
                try
                {
                    //System.Diagnostics.Debugger.Break();
                    if (asiLineList[0].AgencyName == "DEL")
                    {
                        agentDa.DeleteAgencyMru(trans, asiLineList[0].AgentId.PadLeft(12, '0'), asiLineList[0].BranchId, Session.User.Id);
                    }
                    else
                    {
                        agentDa.DeleteAgencyMru(trans, asiLineList[0].AgentId.PadLeft(12, '0'), asiLineList[0].BranchId, Session.User.Id);
                        foreach (LineInfo line in asiLineList)
                        {
                            // if (!line.AgentNeedUpdate && !line.AdvNeedUpdate) continue;
                            string postServerId = Session.Branch.PostedServerId;
                            string branchId = line.BranchId;
                            string agentId = line.AgentId.PadLeft(12, '0');
                            string lineId = line.LineId.TrimEnd(' ');
                            string advPay = line.AdvPayment ? "1" : "0";
                            string modifiedBy = line.ModifiedBy;

                            if (agentDa.InsUpdLineOfAgent(trans, agentId, lineId, branchId, postServerId, modifiedBy) <= 0)
                            {
                                trans.Rollback();
                                throw new Exception("PEA.BPM.AgencyManagementModule.BS.AgencyPlanningService.SaveAssignedLineofAgent:Count of update line in ag_ins_AgentMru is less than or equal to zero..");
                                //return false;
                            }


                            if (line.AdvNeedUpdate)
                            {
                                if (agentDa.UpdateMRUSpecialHelpAndAdvPay(trans, lineId, branchId, advPay, modifiedBy, line) <= 0)
                                {
                                    trans.Rollback();
                                    throw new Exception("PEA.BPM.AgencyManagementModule.BS.AgencyPlanningService.SaveAssignedLineofAgent:Count of update line in ag_upd_MruSpecialHelpAndAdvPayment is less than or equal to zero.");
                                    //return false;
                                }
                            }
                        }
                    }

                    trans.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    throw e;
                }
            }

        }




        public BindingList<LineInfo> FindAndDisplayLineOfAgentSearchInfo(string agentId)
        {
            AgencyDataAccess agentDa = new AgencyDataAccess();
            try
            {
                //line = MRU
                AgentInfo agInfo = agentDa.GetAgentInformation(agentId);
                BindingList<LineInfo> lineInfoList = agentDa.SelectMRUsByAgentId(agentId);
                //it is not destroyed b/c this class refers to its memeber

                return lineInfoList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private bool ExtractOperand(string keyword, ref string operation, ref decimal deposit)
        {
            //trim start and end
            keyword = keyword.TrimStart(' ');
            keyword = keyword.TrimEnd(' ');

            if (keyword == "") return false;

            if (keyword[0] != '<' && keyword[0] != '>' && keyword[0] != '=')
                return false;
            else
            {
                operation = keyword[0].ToString();
                keyword = keyword.Substring(1, keyword.Length - 1);
            }

            if (keyword[0] == '=')
            {
                operation += keyword[0].ToString();
                keyword = keyword.Substring(1, keyword.Length - 1);
            }

            try
            {
                deposit = Convert.ToDecimal(keyword);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }


        public List<PeaInfo> FindAndDisplayBranchByKeyword(string keyword, string branchId)
        {
            AgencyDataAccess agentDa = new AgencyDataAccess();
            try
            {
                List<PeaInfo> branchList = agentDa.SelectBranchByKeyword(keyword, branchId);

                return branchList;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public List<AgentInfo> AcquireAgentAssetSearchInformation(AgentSearchInfo searchInfo)
        {
            AgencyDataAccess agentDa = new AgencyDataAccess();
            string branchId = searchInfo.BranchId;
            try
            {
                List<AgentInfo> agentInfoList = null;
                SerachType searchType = searchInfo.Type;
                string keyword = searchInfo.Keyword;

                if (searchType == SerachType.All)
                {
                    if (keyword.Length > 0 && (keyword[0] == '<' || keyword[0] == '>' || keyword[0] == '='))
                    {
                        decimal deposit = 0;
                        string operation = null;
                        if (ExtractOperand(keyword, ref operation, ref deposit))
                            agentInfoList = agentDa.SelectAgentsByDepositOperand(deposit, operation, searchInfo.BranchId);
                    }
                    else
                        agentInfoList = agentDa.SelectAgentsByKeyword(keyword, 1, branchId);
                }
                else if (searchType == SerachType.AgentName)
                {
                    agentInfoList = agentDa.SelectAgentsByKeyword(keyword, 3, branchId);
                }
                else if (searchType == SerachType.AgentId)
                {
                    agentInfoList = agentDa.SelectAgentsByKeyword(keyword, 2, branchId);
                }
                else if (searchType == SerachType.Deposit)
                {
                    decimal deposit = 0;
                    string operation = null;
                    if (ExtractOperand(keyword, ref operation, ref deposit))
                        agentInfoList = agentDa.SelectAgentsByDepositOperand(deposit, operation, branchId);
                }


                return agentInfoList;
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public BindingList<LineInfo> FindAndDisplayLineByKeyword(LineSearchBoxInfo searchInfo)
        {
            AgencyDataAccess agentDa = new AgencyDataAccess();
            try
            {
                BindingList<LineInfo> lineList = null;
                //if (searchInfo.SearchType == LineSearchBoxInfo.LineSearchType.All)
                    lineList = agentDa.SearchAgencyMru(searchInfo.BranchId, searchInfo.MruId);
                //else if (searchInfo.SearchType == LineSearchBoxInfo.LineSearchType.Id)
                //    lineList = agentDa.SelectMruByKeyword(searchInfo.Keyword, searchInfo.BranchId, 2);
                //else if (searchInfo.SearchType == LineSearchBoxInfo.LineSearchType.AreaCode)
                //    lineList = agentDa.SelectMruByKeyword(searchInfo.Keyword, searchInfo.BranchId, 3);


                return lineList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
