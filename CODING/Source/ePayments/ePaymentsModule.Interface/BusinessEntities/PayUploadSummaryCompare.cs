using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using PEA.BPM.ePaymentsModule.Interface.Constants;
using System.Text;

namespace PEA.BPM.ePaymentsModule.Interface.BusinessEntities
{
    [DataContract]
    public class PayUploadSummaryColumn
    {

        [DataMember(Order=1)]
        public const string CompanyId = "CompanyId";

        [DataMember(Order=2)]
        public const string CompanyName = "CompanyName";
    }

    [DataContract]
    public class PayUploadSummaryCompare : IComparer<PaymentUploadSummary>
    {
        private string sortColumn;
        private string sortDirection;

        public PayUploadSummaryCompare(string sortColumn, string sortDirection)
        {
            this.sortColumn = sortColumn;
            this.sortDirection = sortDirection;
        }

        public int Compare(PaymentUploadSummary x, PaymentUploadSummary y)
        {
            PaymentUploadSummary piX = x;
            PaymentUploadSummary piY = y;

            if (piX == null && piY == null)
            {
                return 0;
            }
            else if (piX == null)
            {
                return (sortDirection == SortDirection.Ascending) ? -1 : 1;
            }
            else if (piY == null)
            {
                return (sortDirection == SortDirection.Ascending) ? 1 : -1;
            }
            else
            {
                switch (sortColumn)
                {
                    case PayUploadSummaryColumn.CompanyId:
                        return (sortDirection == SortDirection.Ascending) ?
                            piX.CompanyId.CompareTo(piY.CompanyId) :
                            piY.CompanyId.CompareTo(piX.CompanyId);
                    case PayUploadSummaryColumn.CompanyName:
                        return (sortDirection == SortDirection.Ascending) ?
                            piX.CompanyName.CompareTo(piY.CompanyName) :
                            piY.CompanyName.CompareTo(piX.CompanyName);
                    default:
                        return 0;
                }
            }
        }
    }
}
