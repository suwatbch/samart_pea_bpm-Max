using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PEA.BPM.Integration.BPMIntegration.Interface.BusinessEntities
{
    [DataContract]
    public class ServiceInfo
    {
		private string 	_SvcId;
		private string 	_Module;
		private string 	_SvcName;
		private string 	_MethodName;
		private string 	_Description;
		private DateTime? 	_ModifiedDt;
		private string 	_ModifiedBy;
		private bool 	_Active;
		private string 	_action;

		[DataMember(Order = 1)]
		public string SvcId
		{
			get { return _SvcId; }
			set { _SvcId = value; }
		}
		[DataMember(Order = 2)]
		public string Module
		{
			get { return _Module; }
			set { _Module = value; }
		}
		[DataMember(Order = 3)]
		public string SvcName
		{
			get { return _SvcName; }
			set { _SvcName = value; }
		}
		[DataMember(Order = 4)]
		public string MethodName
		{
			get { return _MethodName; }
			set { _MethodName = value; }
		}
		[DataMember(Order = 5)]
		public string Description
		{
			get { return _Description; }
			set { _Description = value; }
		}
		[DataMember(Order = 6)]
		public DateTime? ModifiedDt
		{
			get { return _ModifiedDt; }
			set { _ModifiedDt = value; }
		}
		[DataMember(Order = 7)]
		public string ModifiedBy
		{
			get { return _ModifiedBy; }
			set { _ModifiedBy = value; }
		}
		[DataMember(Order = 8)]
		public bool Active
		{
			get { return _Active; }
			set { _Active = value; }
		}
		[DataMember(Order = 9)]
		public string Action
		{
			get { return _action; }
			set { _action = value; }
		}
    }
}
