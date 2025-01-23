using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace AdminToolService.OfflineErrorLog
{
    /// <summary>
    /// Summary description for OfflineErrorLogWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class OfflineErrorLogWebService : System.Web.Services.WebService
    {
        #region Variables
        private OfflineErrorLogBS _offlineErrorLogBS;
        #endregion

        #region Constructor
        public OfflineErrorLogWebService()
        {
            _offlineErrorLogBS = new OfflineErrorLogBS();
        }
        #endregion

        #region Method : GetOfflineErrorLogDisplay
        /// <summary>
        /// �Ѻ������ OfflineErrorLog ����Ѻ�ʴ���
        /// </summary>
        /// <param name="datetime">�ѹ����ͧ����ʴ���</param>
        /// <param name="active">0=�ʴ��Ţ����ŷ�� Inactive, 1=�ʴ��Ţ����ŷ�� Active, null=�ʴ�������</param>
        /// <returns>DataTable �ͧ�����ŷ����</returns>
        [WebMethod]
        public DataTable GetOfflineErrorLogDisplay(DateTime datetime, string active)
        {
            return _offlineErrorLogBS.GetOfflineErrorLogDisplay(datetime, active);
        }

        [WebMethod]
        public void UpdateLogStatus(string fileName, string status)
        {
            _offlineErrorLogBS.UpdateLogStatus(fileName, status);
        }

        #endregion
    }
}