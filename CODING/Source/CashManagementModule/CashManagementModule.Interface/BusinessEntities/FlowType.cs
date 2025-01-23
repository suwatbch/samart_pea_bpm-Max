using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.CashManagementModule.Interface.BusinessEntities
{
    [DataContract]
    public class FlowType
    {
        //01 obsolete, do not use!
        [DataMember(Order=1)]
        public const string SystemInitialCash = "01";                   //�Թ���������ҹ�к�
        [DataMember(Order = 2)]
        public const string MoneyFromBank = "02";                       //�ԡ�Թ�ҡ��Ҥ��, ���Թ����к�
        [DataMember(Order = 3)]
        public const string MoneyFromAnotherCashier = "03";             //�Ѻ�͹�ҡᤪ��������
        [DataMember(Order = 4)]
        public const string MoneyTransferedToAnotherCashier = "04";     //�͹�͡�ᤪ��������
        [DataMember(Order = 5)]
        public const string MoneyReceivedFromPOS = "05";                //�Ѻ�ҡ�к��Ѻ�����Թ
        [DataMember(Order = 6)]
        public const string MoneyDepositToBank = "06";                  //�ӽҡ��Ҥ��
        [DataMember(Order = 7)]
        public const string CashOutFromPOS = "07";                      //�����͡�ҡ�к��Ѻ�����Թ
        [DataMember(Order = 8)]
        public const string MoneyClosingBalance = "08";                 //�ʹ¡�
        [DataMember(Order = 9)]
        public const string MoneyOpeningBalance = "09";                 //�ʹ¡��
        [DataMember(Order = 10)]
        public const string CancelledPOSReceivable = "10";              //¡��ԡ����Ѻ�����Թ
        [DataMember(Order = 11)]
        public const string CancelledPOSPaymentable = "11";             //¡��ԡ��è����Թ
        [DataMember(Order = 12)]
        public const string CancelledMoneyCheckIn = "12";               //¡��ԡ����ԡ�Թ�ҡ��Ҥ��/���Թ����к�
        [DataMember(Order = 13)]
        public const string CancelledBankDelivery = "13";               //¡��ԡ���觸�Ҥ��
        [DataMember(Order = 14)]
        public const string Adjust_MoneyFromBank_Plus = "21";               //��Ѻ�����ʹ¡�ҹ��Թ����к� 
        [DataMember(Order = 15)]
        public const string Adjust_MoneyReceivedFromPOS_Plus = "22";        //��Ѻ�����ʹ¡���Ѻ�ҡ�к��Ѻ�����Թ
        [DataMember(Order = 16)]
        public const string Adjust_MoneyDepositToBank_Minus = "23";         //��ѺŴ�ʹ¡�ҹӽҡ��Ҥ��
        [DataMember(Order = 17)]
        public const string Adjust_CashOutFromPOS_Minus = "24";             //��ѺŴ�ʹ¡�Ҩ����Թ�����Ӥѭ����
        [DataMember(Order = 18)]
        public const string Adjust_MoneyFromBank_Minus = "25";              //��ѺŴ�ʹ¡�ҹ��Թ����к�
        [DataMember(Order = 19)]
        public const string Adjust_MoneyReceivedFromPOS_Minus = "26";       //��ѺŴ�ʹ¡���Ѻ�ҡ�к��Ѻ�����Թ
        [DataMember(Order = 20)]
        public const string Adjust_MoneyDepositToBank_Plus = "27";          //��Ѻ�����ʹ¡�ҹӽҡ��Ҥ��
        [DataMember(Order = 21)]
        public const string Adjust_CashOutFromPOS_Plus = "28";              //��Ѻ�����ʹ¡�Ҩ����Թ�����Ӥѭ����
    }
}
