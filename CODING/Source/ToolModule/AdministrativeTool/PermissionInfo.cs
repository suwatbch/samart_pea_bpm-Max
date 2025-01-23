using System;
using System.Collections.Generic;
using System.Text;

namespace AdministrativeTool
{
    public class PermissionInfo
    {
        protected bool _isCorrect;

        /// <summary>
        /// สถานะการอ่านไฟล์
        /// </summary>
        /// <param name="isRead">อ่านแล้ว?</param>
        public PermissionInfo(bool isCorrect)
        {
           _isCorrect= isCorrect;
        }
 
        public bool IsCorrect
        {
            get
            {
                return _isCorrect;
            }
            set
            {
                _isCorrect = value;
            }
        }
    }
}
