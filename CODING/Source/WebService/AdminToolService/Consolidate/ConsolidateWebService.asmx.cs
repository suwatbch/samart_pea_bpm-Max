using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace AdminToolService.Consolidate
{
    /// <summary>
    /// Summary description for ConsolidateWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class ConsolidateWebService : System.Web.Services.WebService
    {
        #region Variables
        private ConsolidateBS _consolidateBS;
        #endregion

        #region Constructor
        public ConsolidateWebService()
        {
            _consolidateBS = new ConsolidateBS();
        }
        #endregion

        #region Method : GetConsolidateDisplay()
        /// <summary>
        /// �Ѻ������ Consolidate ����Ѻ�ʴ���
        /// </summary>
        /// <param name="datetime">�ѹ����ͧ�������ʴ���</param>
        /// <returns>DataTable �ͧ�����ŷ����</returns>
        [WebMethod]
        public DataTable GetConsolidateDisplay(DateTime datetime)
        {
            return _consolidateBS.GetConsolidateDisplay(datetime);
        }
        #endregion
    }
}