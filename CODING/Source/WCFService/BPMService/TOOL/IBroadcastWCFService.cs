using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.ComponentModel;
using PEA.BPM.Architecture.ArchitectureInterface.BusinessEntities;
using PEA.BPM.Architecture.CommonUtilities;
using PEA.BPM.NewsBroadcast.Interface.BusinessEntities;
using PEA.BPM.NewsBroadcast.Interface.Services;
using WCFExtras.Soap;


namespace BPMService.TOOL
{
    [SoapHeaders, ServiceContract(Namespace = "http://tempuri.org")]
    public interface IBroadcastWCFService
    {
        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData GetNewsBroadcastNow(DateTime _nowDt, string _userId, int _cmdId);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData GetNewsBroadcastHistory(DateTime _nowDt, string _userId, int _cmdId);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData GetNewsBroadcastSent(DateTime _sentDt);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData GetNewsBroadcastNowForSender(DateTime _nowDt, int _cmdId);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData GetNewsBroadcastScheduled(DateTime _nowDt, int _cmdId);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData GetNewsBroadcastSentMonthYears(DateTime _sentDt);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData GetNewsBroadcastUser(int _broadcastId);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        void UpdateNewsBroadcastUserOpened(int _broadcastId, string _userId);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        void UpdateNewsBroadcastUserRead(int _broadcastId, string _userId);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData GetArea();

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData GetBranch(string _areaId);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData GetUser(string _roleId);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        CompressData GetRole();

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        void InsertNewsBroadcast(string _broadcastTopic, string _detail, DateTime _sentDt, DateTime _expireDt, int _cmdId);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        void InsertNewsBroadcastUser(int _broadcastId, string _userId, string _areaId, string _branchId, string _branchName2, string _roleId, string _roleName);

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        int GetLastNewsBroadcastId();

        [SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        [FaultContract(typeof(BPMApplicationExceptionInfo))]
        bool IsDuplicateUser(int _broadcastId, string _userId);



        #region WebMethod Old

        //[SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        //CompressData GetBroadcastList(DateTime _nowDt, string _branchId, int _cmdId);
        //[SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        //DataSet GetBroadcastListByBroadcastId(int _broadcastId);
        //[SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        //DataSet GetBroadcastListBySentDate(DateTime _sentDate);
        //[SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        //void InsertNewBroadcast(string _broadcastTopic, string _detail, DateTime _sentDate, int _recieverId, string _branchId);
        //[SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        //DataSet GetBranchArea();
        //[SoapHeader("AuthInfoValue", typeof(AuthInfo), Direction = SoapHeaderDirection.In), OperationContract]
        //DataSet GetArea();

        #endregion


    }
}
