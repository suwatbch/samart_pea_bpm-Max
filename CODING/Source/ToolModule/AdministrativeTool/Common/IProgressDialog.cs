using System;

namespace AdministrativeTool.Common
{
    #region Interface : IProgressDialog
    /// <summary>
    /// Interface ����Ѻ Form ����ͧ��÷��� Progress Dialog
    /// </summary>
    public interface IProgressDialog
    {
        #region Method : SetProgressStatus()
        /// <summary>
        /// ��Ѻ��ا����ʴ���
        /// </summary>
        /// <param name="progressValue">��Ңͧ Progress (%)</param>
        /// <param name="statusText">��ͤ�������ͧ����ʴ�</param>
        void SetProgressStatus(int? progressValue, string statusText);
        #endregion
    }
    #endregion
}