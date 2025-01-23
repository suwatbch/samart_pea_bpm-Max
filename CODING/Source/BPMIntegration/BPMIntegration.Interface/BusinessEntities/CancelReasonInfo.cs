using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
   public class CancelReasonInfo
    {

       string _crId;
       public string CrId
       {
           get { return this._crId; }
           set { this._crId = value; ; }
       }

       string _description;
       public string Description
       {
           get { return this._description; }
           set { this._description = value; }
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
