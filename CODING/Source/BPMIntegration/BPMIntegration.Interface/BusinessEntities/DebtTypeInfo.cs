using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Architecture.ArchitectureInterface.Services;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    public class DebtTypeInfo : IListUtility<DebtTypeInfo>
    {
       string _dtId;
       public string DtId
       {
           get { return this._dtId; }
           set { this._dtId = value; }
       }

       string _dtName;
       public string DtName
       {
           get { return  this._dtName; }
           set { this._dtName = value; }
       }

       string _receiptType;
       public string ReceiptType
       {
           get { return this._receiptType; }
           set { this._receiptType = value; }
       }

       string _modReceiptHeaderFlag;
       public string ModReceiptHeaderFlag
       {
           get { return this._modReceiptHeaderFlag; }
           set { this._modReceiptHeaderFlag = value; }
       }

       string _invidualReceiptFlag;
       public string InvidualReceiptFlag
       {
           get { return this._invidualReceiptFlag; }
           set { this._invidualReceiptFlag = value; }
       }

       string _electricCustFlag;
       public string ElectricCustFlag
       {

           get { return this._electricCustFlag; }
           set { this._electricCustFlag = value; }
       }

       string _billBookCustFlag;
       public string BillBookCustFlag
       {
           get { return this._billBookCustFlag; }
           set { this._billBookCustFlag = value ; }
       }

       string _otherCustFlag;
       public string OtherCustFlag
       {
           get { return this._otherCustFlag; }
           set { this._otherCustFlag = value; }
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

       string _action;
       public string Action
       {
           get { return _action; }
           set { _action = value; }
       }


       #region IListUtility<DebtTypeInfo> Members

       public string ToStream()
       {
           throw new Exception("The method or operation is not implemented.");
       }

       public DebtTypeInfo ParseExtract(string txt)
       {
           throw new Exception("The method or operation is not implemented.");
       }

       #endregion
   }
}
