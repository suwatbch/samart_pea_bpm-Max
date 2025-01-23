using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.Infrastructure.Interface.BusinessEntities
{
    [DataContract]
    public class CalendarInfo
    {
        string _nwDay;
        string _branchId;    
        DateTime? _modifiedDt;       
        string _cdType;        
        string  _active;

        public CalendarInfo(string NWDay, string BranchId,  DateTime? ModifiedDt, 
                                string CdType, string Active)
        {
            this._nwDay = NWDay;
            this._branchId = BranchId;
          
            this._modifiedDt = ModifiedDt;           
            this._cdType = CdType;
            this._active = Active;
        }


        [DataMember(Order=1)]
        public string NWDay
        {
            get { return this._nwDay; }
            set { this._nwDay = value; }
        }
        

        [DataMember(Order=2)]
        public string BranchId
        {
            get { return this._branchId; }
            set { this._branchId = value; }
        }       


        [DataMember(Order=3)]
        public DateTime? ModifiedDt
        {
            get { return this._modifiedDt; }
            set { this._modifiedDt = value; }
        }


        [DataMember(Order=4)]
        public string CdType
        {
            get { return this._cdType; }
            set { this._cdType = value; }
        }


        [DataMember(Order=5)]
        public string Active
        {
            get { return this._active; }
            set { this._active = value; }
        }
    }
}
