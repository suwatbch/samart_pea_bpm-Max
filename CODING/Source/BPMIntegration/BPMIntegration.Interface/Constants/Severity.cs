using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.Integration.BPMIntegration.Interface.Constants
{
    public static class Severity
    {
        public const int Level1 = 1;
        public const int Level2 = 2;
        public const int Level3 = 3;
        public const int Level4 = 4;
        public const int Level5 = 5;
        /// <summary>
        /// Error เกี่ยวกับฟังก์ชันที่ควบคุม
        /// </summary>
        public const int Level6 = 6;
        /// <summary>
        /// Error เกี่ยวกับตัวไฟล์ Interface
        /// </summary>
        public const int Level7 = 7;
        /// <summary>
        /// Error เกี่ยวกับ Interface ที่อยู่นอกเหนือระบบ
        /// </summary>
        public const int Level8 = 8;
        /// <summary>
        /// Error เกี่ยวกับโครงสร้าง Interface
        /// </summary>
        public const int Level9 = 9;
        /// <summary>
        /// Error ที่ไม่ทราบสาเหตุ
        /// </summary>
        public const int Level10 = 10;
    }
}
