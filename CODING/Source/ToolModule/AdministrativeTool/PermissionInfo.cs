using System;
using System.Collections.Generic;
using System.Text;

namespace AdministrativeTool
{
    public class PermissionInfo
    {
        protected bool _isCorrect;

        /// <summary>
        /// ʶҹС����ҹ���
        /// </summary>
        /// <param name="isRead">��ҹ����?</param>
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
