using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using PEA.BPM.ePaymentsModule.Interface.Constants;
using System.Text;

namespace PEA.BPM.ePaymentsModule.Interface.BusinessEntities
{
    [DataContract]
    public class EPayUploadItemColumn
    {

        [DataMember(Order=1)]
        public const string CaId = "CaId";

        [DataMember(Order=2)]
        public const string SysCaId = "SysCaId";

        [DataMember(Order=3)]
        public const string CompanyId = "CompanyId";

        [DataMember(Order=4)]
        public const string InvoiceNo = "InvoiceNo";

        [DataMember(Order=5)]
        public const string SysInvoiceNo = "SysInvoiceNo";

        [DataMember(Order=6)]
        public const string PayUploadFormat = "PayUploadFormat";
    }

    [DataContract]
    public class EPayUploadItemCompare : IComparer<EpayUploadItem>
    {
        private string sortColumn;
        private string sortDirection;

        public EPayUploadItemCompare(string sortColumn, string sortDirection)
        {
            this.sortColumn = sortColumn;
            this.sortDirection = sortDirection;
        }

        public int Compare(EpayUploadItem x, EpayUploadItem y)
        {
            EpayUploadItem piX = x;
            EpayUploadItem piY = y;

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
                    case EPayUploadItemColumn.CompanyId:
                        return (sortDirection == SortDirection.Ascending) ?
                            piX.CompanyId.CompareTo(piY.CompanyId) :
                            piY.CompanyId.CompareTo(piX.CompanyId);
                    case EPayUploadItemColumn.CaId:
                        return (sortDirection == SortDirection.Ascending) ?
                            piX.CaId.CompareTo(piY.CaId) :
                            piY.CaId.CompareTo(piX.CaId);
                    case EPayUploadItemColumn.SysCaId:
                        return (sortDirection == SortDirection.Ascending) ?
                            piX.SysCaId.CompareTo(piY.SysCaId) :
                            piY.SysCaId.CompareTo(piX.SysCaId);
                    case EPayUploadItemColumn.InvoiceNo:
                        return (sortDirection == SortDirection.Ascending) ?
                            piX.InvoiceNo.CompareTo(piY.InvoiceNo) :
                            piY.InvoiceNo.CompareTo(piX.InvoiceNo);
                    case EPayUploadItemColumn.SysInvoiceNo:
                        return (sortDirection == SortDirection.Ascending) ?
                            piX.SysInvoiceNo.CompareTo(piY.SysInvoiceNo) :
                            piY.SysInvoiceNo.CompareTo(piX.SysInvoiceNo);
                    case EPayUploadItemColumn.PayUploadFormat:
                        return (sortDirection == SortDirection.Ascending) ?
                            piX.PayUploadFormat.CompareTo(piY.PayUploadFormat) :
                            piY.PayUploadFormat.CompareTo(piX.PayUploadFormat);
                    default:
                        return 0;
                }
            }
        }

    }

}
