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
        /// Flag ���ʶҹТͧ��Ҹ���������õ�͡�Ѻ������ �ա�ê��������ѧ
        /// �� ��ԧ ��� ���Ф�Ҹ���������Ѻ���������� ��������ͧ����(�ó�����¶١�Ѵ�)
        /// �� �� ��� �դ�Ҹ���������õ�͡�Ѻ���������ѧ��������
        /// </summary>

        [DataMember(Order=2)]
        public bool PaidDisconnectFlag
        {
            get { return _paidDisconnectFlag; }
            set { _paidDisconnectFlag = value; }
        }

        /// <summary>
        /// DisconnectionDoc ���� DisconnectionNo ����ش��� CaId ���Ѻ
        /// ����ö�繤�� Null �� �ó�����¶١�Ѵ��ҡ�͹
        /// </summary>
        /// <value>The last disconnect doc.</value>

        [DataMember(Order=3)]
        public string LastDisconnectDoc
        {
            get { return _lastDisconnectNo; }
            set { _lastDisconnectNo = value; }
        }

        /// <summary>
        /// Disconnection Date ����ش��� CaId ���Ѻ
        /// ����ö�繤�� Null �� �ó�����¶١�Ѵ��ҡ�͹
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
