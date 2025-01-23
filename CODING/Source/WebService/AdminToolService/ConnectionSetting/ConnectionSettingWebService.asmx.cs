using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace AdminToolService.ConnectionSetting
{
    /// <summary>
    /// Summary description for ConnectionSettingWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class ConnectionSettingWebService : System.Web.Services.WebService
    {
        #region Variables
        private ConnectionSettingBS _connectionSettingBS;
        #endregion

        #region Constructor
        public ConnectionSettingWebService()
        {
            _connectionSettingBS = new ConnectionSettingBS();
        }
        #endregion

        #region Method : GetConnectionSettingDisplay()
        /// <summary>
        /// �Ѻ������ ConnectionSetting ����Ѻ�ʴ���
        /// </summary>
        /// <param name="branchId">Branch ID ����ͧ����ʴ��� (null = �ʴ�������)</param>
        /// <param name="active">0=�ʴ��Ţ����ŷ�� Inactive, 1=�ʴ��Ţ����ŷ�� Active, null=�ʴ�������</param>
        /// <returns>DataTable �ͧ�����ŷ����</returns>
        [WebMethod]
        public DataTable GetConnectionSettingDisplay(string branchId, string active)
        {
            return _connectionSettingBS.GetConnectionSettingDisplay(branchId, active);
        }
        #endregion

        #region Method : GetConnectionSettingInfo()
        /// <summary>
        /// �Ѻ�����Ţͧ ConnectionSetting ����˹�
        /// </summary>
        /// <param name="branchId">Branch ID �ͧ  ConnectionSetting ����ͧ���</param>
        /// <returns>ConnectionSetting �ͧ�����ŷ����</returns>
        [WebMethod]
        public ConnectionSettingInfo GetConnectionSettingInfo(string branchId)
        {
            return _connectionSettingBS.GetConnectionSettingInfo(branchId);
        }
        #endregion

        #region Method : AddConnectionSetting()
        /// <summary>
        /// ���� ConnectionSetting ����
        /// </summary>
        /// <param name="connectionSettingInfo">������ ConnectionSetting ����ͧ�������</param>
        /// <returns>�ӹǹ�Ǣͧ�����ŷ���ռ�</returns>
        [WebMethod]
        public int AddConnectionSetting(ConnectionSettingInfo connectionSettingInfo)
        {
            return _connectionSettingBS.AddConnectionSetting(connectionSettingInfo);
        }
        #endregion

        #region Method : UpdateConnectionSetting()
        /// <summary>
        /// ��� ConnectionSetting ���
        /// </summary>
        /// <param name="targetBranchId">Branch ID �ͧ  ConnectionSetting ����ͧ������</param>
        /// <param name="connectionSettingInfo">������ ConnectionSetting ������</param>
        /// <returns>�ӹǹ�Ǣͧ�����ŷ���ռ�</returns>
        [WebMethod]
        public int UpdateConnectionSetting(string targetBranchId, ConnectionSettingInfo connectionSettingInfo)
        {
            return _connectionSettingBS.UpdateConnectionSetting(targetBranchId, connectionSettingInfo);
        }
        #endregion

        #region Method : UpdateConnectionSetting()
        /// <summary>
        /// ź������ͧ ConnectionSetting ����˹�
        /// </summary>
        /// <param name="branchIdList">������ͧ Branch ID ����ͧ���ź ��蹴�������ͧ���� (,)</param>
        /// <returns>�ӹǹ�Ǣͧ�����ŷ���ռ�</returns>
        [WebMethod]
        public int DeleteConnectionSettingByBranchIdList(string branchIdList)
        {
            return _connectionSettingBS.DeleteConnectionSettingByBranchIdList(branchIdList);
        }
        #endregion
    }
}