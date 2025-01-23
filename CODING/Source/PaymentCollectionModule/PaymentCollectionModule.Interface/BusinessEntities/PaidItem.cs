using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.PaymentCollectionModule.Interface.Constants;
using PEA.BPM.Architecture.CommonUtilities;

namespace PEA.BPM.PaymentCollectionModule.Interface.BusinessEntities
{
    [DataContract]
    public class PaidItem
    {
        private string _debtId;
        private string _debtType;
        private string _period;
        private string _rateTypeId;
        private string _description;
        private DateTime? _cutOffDate;
        private decimal? _amount;


        [DataMember(Order=1)]
        public string DebtId
        {
            get { return _debtId; }
            set { _debtId = value; }
        }


        [DataMember(Order=2)]
        public string DebtType
        {
            get { return _debtType; }
            set { _debtType = value; }
        }


        [DataMember(Order=3)]
        public string Period
        {
            get { return _period; }
            set { _period = value; }
        }

        //Checked TongKung
        //[DataMember(Order=4)]       
        public string PeriodString
        {
            get { return (_period == null) ? "" : StringConvert.FormatPeriod(_period); }
        }


        [DataMember(Order=5)]
        public string RateTypeId
        {
            get { return _rateTypeId; }
            set { _rateTypeId = value; }
        }

        //Checked TongKung
        [DataMember(Order=6)]
        public string Description
        {
            get
            {
                switch (_debtId.Trim())
                {
                    case "1":
                        string cutOffDate = (_cutOffDate == null) ? "" : string.Format("กำหนดตัดไฟ {0}", _cutOffDate.Value.ToString("dd/MM/yyyy"));
                        return string.Format("{0} {1}", cutOffDate, _description).Trim();
                    default:
                        return _description;
                }
            }
            set { _description = value; }
        }

        //Checked TongKung
        //[DataMember(Order=7)]
        public string FullDescription
        {
            get
            {
                switch (_debtId.Substring(0, 5).Trim())
                {
                    case CodeNames.DebtType.Electric.Id:
                        return string.Format("ประจำเดือน {0} ประเภทอัตรา {1} {2}", PeriodString, RateTypeId, Description);
                    default:
                        return Description;
                }
            }
        }


        [DataMember(Order=8)]
        public DateTime? CutOffDate
        {
            get { return _cutOffDate; }
            set { _cutOffDate = value; }
        }


        [DataMember(Order=9)]
        public decimal? Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

    }
}
