using System;

namespace AdministrativeTool.Common
{
    #region Interface : IProgressDialog
    /// <summary>
    /// Interface สำหรับ Form ที่ต้องการทำเป็น Progress Dialog
    /// </summary>
    public interface IProgressDialog
    {
        #region Method : SetProgressStatus()
        /// <summary>
        /// ปรับปรุงการแสดงผล
        /// </summary>
        /// <param name="progressValue">ค่าของ Progress (%)</param>
        /// <param name="statusText">ข้อความที่ต้องการแสดง</param>
        void SetProgressStatus(int? progressValue, string statusText);
        #endregion
    }
    #endregion
}