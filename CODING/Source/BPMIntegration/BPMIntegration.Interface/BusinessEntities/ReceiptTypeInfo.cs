using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    public class ReceiptTypeInfo
    {
        string _receiptTypeId;
        public string ReceiptTypeId
        {
            get { return this._receiptTypeId; }
            set { this._receiptTypeId = value; }
        }

        string _receiptTypeName;
        public string ReceiptTypeName
        {
            get { return this._receiptTypeName; }
            set { this._receiptTypeName = value; }
        }

        string _paperSize;
        public string PaperSize
        {
            get { return this._paperSize; }
            set { this._paperSize = value; }
        }

        string _headerType;
        public string HeaderType
        {
            get { return this._headerType; }
            set { this._headerType = value; }
        }

        string _syncFlag;
        public string SyncFlag
        {
            get { return this._syncFlag; }
            set { this._syncFlag = value; }
        }

        DateTime? _modifiedDt;
        public DateTime? ModifiedDt
        {
            get { return this._modifiedDt; }
            set { this._modifiedDt = value; }
        }

        string _modifiedBy;
        public string ModifiedBy
        {
            get { return this._modifiedBy; }
            set { this._modifiedBy = value; }
        }

        bool _active;
        public bool Active
        {
            get { return this._active; }
            set { this._active = value; }
        }

    }
}
