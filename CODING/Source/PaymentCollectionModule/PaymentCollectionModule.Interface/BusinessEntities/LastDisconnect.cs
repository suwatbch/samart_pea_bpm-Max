using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class LastDisconnect
    {
        #region LastDisconnection
        private string _caId;
        private bool _paidDisconnectFlag;
        private string _lastDisconnectNo;
        private DateTime? _lastDisconnectDate;


        [DataMember(Order=1)]
        public string CaId
        {
            get { return _caId; }
            set { _caId = value; }
        }

        /// <summary>
        /// Flag ค่าสถานะของค่าธรรมเนียมการต่อกลับมิเตอร์ มีการชำระหรือยัง
        /// เป็น จริง ถ้า ชำระค่าธรรมเนียมกลับมิเตอร์แล้ว หรือไม่ต้องชำระ(กรณีไม่เคยถูกตัดไฟ)
        /// เป็น เท็จ ถ้า มีค่าธรรมเนียมการต่อกลับมิเตอร์แต่ยังไม่ได้ชำระ
        /// </summary>

        [DataMember(Order=2)]
        public bool PaidDisconnectFlag
        {
            get { return _paidDisconnectFlag; }
            set { _paidDisconnectFlag = value; }
        }

        /// <summary>
        /// DisconnectionDoc หรือ DisconnectionNo ล่าสุดที่ CaId ได้รับ
        /// สามารถเป็นค่า Null ได้ กรณีไม่เคยถูกตัดไฟมาก่อน
        /// </summary>
        /// <value>The last disconnect doc.</value>

        [DataMember(Order=3)]
        public string LastDisconnectDoc
        {
            get { return _lastDisconnectNo; }
            set { _lastDisconnectNo = value; }
        }

        /// <summary>
        /// Disconnection Date ล่าสุดที่ CaId ได้รับ
        /// สามารถเป็นค่า Null ได้ กรณีไม่เคยถูกตัดไฟมาก่อน
        /// </summary>
        /// <value>The last disconnect date.</value>

        [DataMember(Order=4)]
        public DateTime? LastDisconnectDate
        {
            get { return _lastDisconnectDate; }
            set { _lastDisconnectDate = value; }
        }

        #endregion
    }
}
