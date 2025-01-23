using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.BillPrintingModule.Interface.BusinessEntities
{
    [DataContract]
    public class PrintableId
    {
        private string branchId;
        private string mruId;
        private string caId;
        private string fromNumberId;
        private string toNumberId;
        private string mtNo;
        private int? printingStatus; // 0,1,2 print,printed,no data
        private string billPeriod;      
        private int? billPeriodLog; //bill volumn number used in report
        private bool? isZeroRecord;
        private string printType; // for checkBranchForBillDelivery 1-Blue,2-Green
        private string grvInvFlag;
        private int? lotNo;// used in checkExistByBank    
        private string bankId;// used in checkExistByBank
        private string dueDate;// used in checkExistByBank
        private string txtId;// used in billProcessingListView
        private int? billCount; //group invoice


        [DataMember(Order=1)]
        public string TxtId
        {
            get { return txtId; }
            set { txtId = value; }
        }


        [DataMember(Order=2)]
        public string BankId
        {
            get { return bankId; }
            set { bankId = value; }
        }


        [DataMember(Order=3)]
        public string DueDate
        {
            get { return dueDate; }
            set { dueDate = value; }
        }


        [DataMember(Order=4)]
        public int? LotNo
        {
            get { return lotNo; }
            set { lotNo = value; }
        }


        [DataMember(Order=5)]
        public int? BillCount
        {
            get { return billCount; }
            set { billCount = value; }
        }
         

        [DataMember(Order=6)]
        public string MtNo
        {
            get { return mtNo; }
            set { mtNo = value; }
        }


        [DataMember(Order=7)]
        public string GrvInvFlag
        {
            get { return grvInvFlag; }
            set { grvInvFlag = value; }
        }   


        [DataMember(Order=8)]
        public bool? IsZeroRecord
        {
            get { return isZeroRecord; }
            set { isZeroRecord = value; }
        }


        [DataMember(Order=9)]
        public string BranchId
        {
            get { return branchId; }
            set { branchId = value; }
        }


        [DataMember(Order=10)]
        public string MruId
        {
            get { return mruId; }
            set { mruId = value; }
        }


        [DataMember(Order=11)]
        public string CaId
        {
            get { return caId; }
            set { caId = value; }
        }


        [DataMember(Order=12)]
        public string FromNumberId
        {
            get { return fromNumberId; }
            set { fromNumberId = value; }
        }
        

        [DataMember(Order=13)]
        public string ToNumberId
        {
            get { return toNumberId; }
            set { toNumberId = value; }
        }


        [DataMember(Order=14)]
        public int? PrintingStatus
        {
            get { return printingStatus; }
            set { printingStatus = value; }
        }


        [DataMember(Order=15)]
        public int? BillPeriodLog
        {
            get { return billPeriodLog; }
            set { billPeriodLog = value; }
        }


        [DataMember(Order=16)]
        public string BillPeriod
        {
            get { return billPeriod; }
            set { billPeriod = value; }
        }


        [DataMember(Order=17)]
        public string PrintType
        {
            get { return printType; }
            set { printType = value; }
        }
    }
}
