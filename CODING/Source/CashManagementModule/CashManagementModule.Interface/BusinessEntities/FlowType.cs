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
        public const string SystemInitialCash = "01";                   //เงินเริ่มต้นใช้งานระบบ
        [DataMember(Order = 2)]
        public const string MoneyFromBank = "02";                       //เบิกเงินจากธนาคาร, นำเงินเข้าระบบ
        [DataMember(Order = 3)]
        public const string MoneyFromAnotherCashier = "03";             //รับโอนจากแคชเชียร์อื่น
        [DataMember(Order = 4)]
        public const string MoneyTransferedToAnotherCashier = "04";     //โอนออกไปแคชเชียร์อื่น
        [DataMember(Order = 5)]
        public const string MoneyReceivedFromPOS = "05";                //รับจากระบบรับชำระเงิน
        [DataMember(Order = 6)]
        public const string MoneyDepositToBank = "06";                  //นำฝากธนาคาร
        [DataMember(Order = 7)]
        public const string CashOutFromPOS = "07";                      //จ่ายออกจากระบบรับชำระเงิน
        [DataMember(Order = 8)]
        public const string MoneyClosingBalance = "08";                 //ยอดยกไป
        [DataMember(Order = 9)]
        public const string MoneyOpeningBalance = "09";                 //ยอดยกมา
        [DataMember(Order = 10)]
        public const string CancelledPOSReceivable = "10";              //ยกเลิกการรับชำระเงิน
        [DataMember(Order = 11)]
        public const string CancelledPOSPaymentable = "11";             //ยกเลิกการจ่ายเงิน
        [DataMember(Order = 12)]
        public const string CancelledMoneyCheckIn = "12";               //ยกเลิกการเบิกเงินจากธนาคาร/นำเงินเข้าระบบ
        [DataMember(Order = 13)]
        public const string CancelledBankDelivery = "13";               //ยกเลิกนำส่งธนาคาร
        [DataMember(Order = 14)]
        public const string Adjust_MoneyFromBank_Plus = "21";               //ปรับเพิ่มยอดยกมานำเงินเข้าระบบ 
        [DataMember(Order = 15)]
        public const string Adjust_MoneyReceivedFromPOS_Plus = "22";        //ปรับเพิ่มยอดยกมารับจากระบบรับชำระเงิน
        [DataMember(Order = 16)]
        public const string Adjust_MoneyDepositToBank_Minus = "23";         //ปรับลดยอดยกมานำฝากธนาคาร
        [DataMember(Order = 17)]
        public const string Adjust_CashOutFromPOS_Minus = "24";             //ปรับลดยอดยกมาจ่ายเงินตามใบสำคัญจ่าย
        [DataMember(Order = 18)]
        public const string Adjust_MoneyFromBank_Minus = "25";              //ปรับลดยอดยกมานำเงินเข้าระบบ
        [DataMember(Order = 19)]
        public const string Adjust_MoneyReceivedFromPOS_Minus = "26";       //ปรับลดยอดยกมารับจากระบบรับชำระเงิน
        [DataMember(Order = 20)]
        public const string Adjust_MoneyDepositToBank_Plus = "27";          //ปรับเพิ่มยอดยกมานำฝากธนาคาร
        [DataMember(Order = 21)]
        public const string Adjust_CashOutFromPOS_Plus = "28";              //ปรับเพิ่มยอดยกมาจ่ายเงินตามใบสำคัญจ่าย
    }
}
