using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEA.BPM.ePaymentsModule.Interface.BusinessEntities.Reports
{
    [DataContract]
   public class RE07ParamInfo
    {
       private string _accountClassId = String.Empty;
       private string _companyId = String.Empty;
       private DateTime? _uploadDtFrom;
       private DateTime? _uploadDtTo;
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
       public DateTime? UploadDtFrom
       {
           get { return this._uploadDtFrom; }
           set { this._uploadDtFrom = value; }
       }


        [DataMember(Order=4)]
       public DateTime? UploadDtTo
       {
           get { return this._uploadDtTo; }
           set { this._uploadDtTo = value; }
       }


        [DataMember(Order=5)]
       public string RunningBranchId
       {
           get { return this._runningBranchId; }
           set { this._runningBranchId = value; }
       }
    }
}
