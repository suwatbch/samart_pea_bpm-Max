using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.ePaymentsModule.Interface.BusinessEntities.Reports
{
    [DataContract]
   public class RE09ParamInfo
    {
       private string _accountClassId = String.Empty;
       private string _companyId = String.Empty;
       private DateTime? _payDtFrom;
       private DateTime? _payDtTo;
       private string _runningBranchId;


        [DataMember(Order=1)]
       public string AccountClassId
       {
           get { return this._accountClassId; }
           set { this._accountClassId = value; }
       }


        [DataMember(Order=2)]
       public string CompanyId
       {
           get { return this._companyId; }
           set { this._companyId = value; }
       }


        [DataMember(Order=3)]
       public DateTime? PayDtFrom
       {
           get { return this._payDtFrom; }
           set { this._payDtFrom = value; }
       }


        [DataMember(Order=4)]
       public DateTime? PayDtTo
       {
           get { return this._payDtTo; }
           set { this._payDtTo = value; }
       }


        [DataMember(Order=5)]
       public string RunningBranchId
       {
           get { return this._runningBranchId; }
           set { this._runningBranchId = value; }
       }
    }
}
