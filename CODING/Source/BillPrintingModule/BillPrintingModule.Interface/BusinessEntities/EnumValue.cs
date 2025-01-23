using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.BillPrintingModule.Interface.BusinessEntities
{

    [DataContract]
    public enum PrintingCondition
    {
        [EnumMember]
        AllPrinting,
        [EnumMember]
        BranchPrinting,
        [EnumMember]
        MruPrinting,
        [EnumMember]
        UserPrinting,
        [EnumMember]
        BillSeqPrinting,
        [EnumMember]
        PaidByPrinting,
        [EnumMember]
        MtNoPrinting,
        [EnumMember]
        BankPrinting,
        [EnumMember]
        LotNoPrinting
    }


    [DataContract]  
    public enum BillType
    {
        [EnumMember]
        A4Bill,//0
        [EnumMember]
        BlueBill,//1
        [EnumMember]
        GreenBill,//2
        [EnumMember]
        GroupInvoiceA4Bill,//3
        [EnumMember]
        BlueBillByBank,//4
        [EnumMember]
        GreenBillByBank,//5
        [EnumMember]
        GreenReceipt,//6
        [EnumMember]
        ReprintBlueBill,//7
        [EnumMember]
        ReprintGreenBill,//8               
        [EnumMember]
        ReprintGroupInvoiceA4Bill,//9
        [EnumMember]
        ReprintGreenBillByBank,//10                
        [EnumMember]
        ReprintBlueBillByBank,//11
        [EnumMember]
        ReprintGreenReceipt,//12
        [EnumMember]
        SpotBill, //13 - new added to prevent wrong mass printing
        [EnumMember]
        ReprintSpotBill, //14
        [EnumMember]
        ReprintA4Bill
    }
}
