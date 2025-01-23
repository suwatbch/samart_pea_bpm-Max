// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================

namespace Avanade.ACA.Batch
{

	/// <summary>
	/// Contains constants for Stored Procedure names, parameter names, and other constants used in the Security library.
	/// </summary>
	internal class BatchDbConstants
	{
		/// <summary>
		/// The database key used by the Primary Credentials to connect to an ACA.NET Security database.
		/// </summary>
		internal const string DATABASE_KEY_BATCH_ARCHITECTURE = "BATCH";

		internal class SP
		{
			internal const string TYPE_SAVE = "ACA_BA_Type_Save";
			internal const string TYPE_DELETE = "ACA_BA_Type_Del";
			internal const string TYPE_LIST = "ACA_BA_Type_List";

			internal const string DESTINATION_SAVE = "ACA_BA_Dest_Save";
			internal const string DESTINATION_DELETE = "ACA_BA_Dest_Del";
			internal const string DESTINATION_LIST = "ACA_BA_Dest_List";

			internal const string JOB_SAVE = "ACA_BA_Job_Save";
			internal const string JOB_DELETE = "ACA_BA_Job_Del";
			internal const string JOB_LIST = "ACA_BA_Job_List";

			internal const string BATCH_SAVE = "ACA_BA_BA_Save";
			internal const string BATCH_DELETE = "ACA_BA_BA_Del";
			internal const string BATCH_LIST = "ACA_BA_BA_List";
			internal const string BATCH_JOB_LIST = "ACA_BA_BA_JobLst";
			internal const string BATCH_JOB_CLEAN = "ACA_BA_BA_JobCln";
			internal const string BATCH_JOB_REMOVE = "ACA_BA_BA_JobRmv";
			internal const string BATCH_JOB_ADD = "ACA_BA_BA_JobAdd";

			internal const string PARM_ADD = "ACA_BA_Parm_Add";
			internal const string PARM_DELETE = "ACA_BA_Parm_Del";
			internal const string PARM_LIST = "ACA_BA_Parm_List";
			internal const string PARM_SAVE_BY_NAME = "ACA_BA_Parm_SaveNM";
			internal const string PARM_SAVE_BY_KEY = "ACA_BA_Parm_Save";
			internal const string PARM_TYPE_SAVE = "ACA_BA_ParmT_Save";
			internal const string PARM_TYPE_DELETE = "ACA_BA_ParmT_Del";

			internal const string Q_DEQUEUE = "ACA_BA_Q_DE";
			internal const string Q_ENQUEUE_W_KEY = "ACA_BA_Q_EN_Key";
			internal const string Q_ENQUEUE_W_NAME = "ACA_BA_Q_EN_NM";
			internal const string Q_ENQUEUE_JOB = "ACA_BA_Q_EN_Job";
			internal const string Q_RESTART_REQUEST = "ACA_BA_Q_Restart";
			internal const string Q_SET_PARENT_KEY = "ACA_BA_Q_SParent";
			internal const string Q_ARCHIEVE = "ACA_BA_Q_Archive";
			internal const string Q_ARCHIVE_EXPIRED = "ACA_Hist_ArchExp";
			internal const string Q_UPDATE_STATUS = "ACA_BA_Q_UStatus";
			internal const string Q_SET_PAUSE = "ACA_BA_Q_SetPause";

			internal const string HIST_REQ_CLEAN = "ACA_Hist_Clean";
			internal const string HIST_REQ_CLEAN_OLDER = "ACA_Hist_CleanOld";
			internal const string HIST_REQ_CLEAN_ALL = "ACA_Hist_CleanAll";

			internal const string REQUEST_JOBEXEC_LIST = "ACA_BA_JobE_List";
			internal const string REQUEST_TRANSITION_LIST = "ACA_BA_Tran_List";
			internal const string REQUEST_LIST_BY_Q_DATE = "ACA_BA_Q_LDate";
			internal const string REQUEST_LIST_RESTART_HISTORY = "ACA_BA_Q_LHistory";
			internal const string REQUEST_LIST_BY_PARENT = "ACA_BA_Q_LChild";
			internal const string REQUEST_LIST_BY_BATCH = "ACA_BA_Q_LBatch";
			internal const string REQUEST_LIST_BY_STATUS = "ACA_BA_Q_ListS";
			internal const string REQUEST_LIST_DETAILS = "ACA_BA_Q_List";
			internal const string REQUEST_CHECK_STATUS = "ACA_BA_Q_CKStatus";
			internal const string REQUEST_DELETE = "ACA_BA_Q_Del";
			internal const string REQUEST_GET = "ACA_BA_Q_Get";

			internal const string JOB_EXEC_UPDATE_STATUS = "ACA_BA_JobE_UStat";
			internal const string JOB_EXEC_CHECK_STATUS = "ACA_BA_JobE_CKStat";

			internal const string EXCEPTION_SAVE = "ACA_BA_Exp_Save";
			internal const string EXCEPTION_LIST = "ACA_BA_Exp_List";

            internal const string REQUEST_ERROR_LOG = "ACA_INT_ListErrorLog";

            internal const string EXEC_SERV_GET_BATCH_QUEUE_KEY = "ACA_ExS_GetBatchQueueKey";
            internal const string EXEC_SERV_GET_SERVER = "ACA_ExS_GetExecServer";
            internal const string EXEC_SERV_GET_SERVER_PRIORITY = "ACA_ExS_GetExecServerSystemPriority";
            internal const string EXEC_SERV_GET_SERVER_CONFIG = "ACA_ExS_GetExecServerConfig";
            internal const string EXEC_SERV_GET_SERVER_SYNC = "ACA_ExS_GetExecServerSync";
            internal const string EXEC_SERV_ADD_SERVER = "ACA_ExS_InsertExecServer";
            internal const string EXEC_SERV_ADD_SERVER_SYSTEM = "ACA_ExS_InsertExecServerSystem";
            internal const string EXEC_SERV_ADD_LOG = "ACA_ExS_InsertExecLog";
            internal const string EXEC_SERV_UPDATE_SERVER = "ACA_ExS_UpdateExecServer";
            internal const string EXEC_SERV_UPDATE_SERVER_SYNC = "ACA_ExS_UpdateExecServerSync";
            internal const string EXEC_SERV_CLEAR_SERVER_SYNC_STATUS = "ACA_ExS_ClearExecServerSyncStatus";
		}

		/// <summary>
		/// Stored procedure parameter name
		/// </summary>
		internal class Parm
		{
			internal const string TYPE_KEY = "vTypeKey"; 
			internal const string TYPE_CLASS = "vTypeClass"; 
			internal const string TYPE_NAME = "vTypeName"; 
			internal const string TYPE_DESC = "vTypeDesc";
			internal const string TYPE_CATEGORY = "vTypeCategory";

			internal const string DESTINATION_KEY = "vBatchDestKey"; 
			internal const string DESTINATION_NAME = "vBatchDestName"; 
			internal const string DESTINATION_DESC = "vBatchDestDesc";

			internal const string JOB_KEY = "vBatchJobKey";
			internal const string JOB_NAME = "vBatchJobName";
			internal const string JOB_DESC = "vBatchJobDesc";
			internal const string JOB_RESTART = "vJobSmartRestart";
			internal const string JOB_COMMIT_FREQ = "vBatchJobCmitFreq";

			internal const string PARAM_KEY = "vBatchParmKey";
			internal const string PARAM_TYPE_KEY = "vParmTypeKey";
			internal const string PARAM_TYPE_KEY_NEW = "vNewParmTypeKey";
			internal const string PARAM_NAME = "vParmName";
			internal const string PARAM_VAL = "vParmVal";
			internal const string PARAM_EXT_KEY = "vParmExternalKey";
			internal const string PARAM_CATEGORY = "vParmCategory";
			internal const string PARAM_NEW_KEY = "vNewBatchParmKey";
			internal const string PARAM_TYPE_NAME = "vParmTypeName";
			internal const string PARAM_TYPE_DESC = "vParmTypeDesc";

			internal const string BATCH_KEY = "vBatchKey";
			internal const string BATCH_NAME = "vBatchName";
			internal const string BATCH_DESC = "vBatchDesc";
			internal const string BATCH_RESTART = "vBatchSmartRestart"; 
			internal const string BATCH_EXE_PRIORITY = "vExePriorityLevel"; 
			internal const string BATCH_QUEUE_PRIORITY = "vQuePriorityLevel";
			internal const string BATCH_MAX_CONCURRENT = "vMaxConcurrent";
			internal const string BATCH_CONFIG = "vConfigFile";
			internal const string BATCH_REL_EXPIRE_DATE = "vRelativeExpDate";
			internal const string BATCH_DEST_KEY = "vBatchDestKey";
			internal const string BATCH_ISOLATION = "vBatchIsoLevel";
			internal const string BATCH_JOB_MAP_SEQ = "vBatchJobSequence";
			internal const string BATCH_JOB_MAP_SEQ_OLD = "vOldJobSequence";

			internal const string Q_KEY = "vBatchQueueKey";
			internal const string Q_NEW_KEY = "vNewBatchQueueKey";
			internal const string Q_START_DATE = "vStartDate";
			internal const string Q_ENTER_DATE = "vQueuedDate";
			internal const string Q_LAST_UPDATE_DATE = "vLastUpdateDate";
			internal const string Q_ABS_EXP_DATE = "vAbsExpDate";
			internal const string Q_ORIG_BATCH_KEY = "vOrigBatchKey";
			internal const string Q_STATUS_CODE = "vBatchStatusCode";
			internal const string Q_DEST_FILTER = "vDestFilter";
			internal const string Q_BATCH_SERVER_NAME = "vBatchServerName";
			internal const string Q_BATCH_CLIENT_NAME = "vBatchClientName";
			internal const string Q_TO_PAUSE = "vToPause";
			internal const string Q_PARENT_KEY = "vParentRequestKey";
			internal const string Q_STATUS_FILTER = "vStatusCodeFilter";
			internal const string Q_LAST_EXEC_KEY = "vLastExecKey";
			internal const string Q_NEXT_EXEC_KEY = "vNextExecKey";
			internal const string Q_BATCH_TYPE_CLASS = "vBatchTypeClass";

			internal const string Q_JOB_KEY = "vBatchJobExecKey";
			internal const string Q_JOB_SEQ = "vJobSeq";
			internal const string Q_JOB_TYPE_CLASS = "vJobTypeClass";
			internal const string Q_JOB_STATUS_CODE = "vJobStatusCode";
			internal const string Q_JOB_LAST_COMMIT = "vLastCommitDate";
			internal const string Q_JOB_WORK_UNIT_COUNT = "vWorkUnitCount";
			internal const string Q_JOB_RESATRT_COUNT = "vRestartCount";
			internal const string Q_JOB_COMMIT_COUNT = "vCommitCount";
			internal const string Q_JOB_ORIG_JOB_KEY = "vOrigJobKey";
			internal const string Q_JOB_LAST_EXE_STATUS = "vLastExeStatus";

			internal const string EXP_KEY = "vBatchExcptnKey";
			internal const string EXP_TYPE = "vExceptionType";
			internal const string EXP_MESSAGE = "vMessage";
			internal const string EXP_SOURCE = "vSource";
			internal const string EXP_TRAGET_SITE = "vTargetSite";
			internal const string EXP_STACK_TRACE = "vStackTrace";
			internal const string EXP_INNER_EXP_KEY = "vInnerExpKey";

			internal const string START_DATE = "vStartDate";
			internal const string END_DATE = "vEndDate";

            internal const string EXEC_SERV_ID = "ExecServerId";
            internal const string EXEC_SERV_EXECUTION_SERVER_NAME = "ExecServerSystemName";
            internal const string EXEC_SERV_BATCH_NAME = "BatchName";
            internal const string EXEC_SERV_EXECUTION_DATE_TIME = "ExecutedDt";
            internal const string EXEC_SERV_ENABLE_FALSETOLERANCE = "EnableFalseTolerance";
            internal const string EXEC_SERV_ENABLE_PRIORITY = "EnablePriorityMode";
            internal const string EXEC_SERV_ENABLE_PROGRESSLOG = "EnableProgressLog";
            internal const string EXEC_SERV_SYNC_INTERVAL = "SyncCycleInterval";
            internal const string EXEC_SERV_TIMEOUT = "TimeOut";
            internal const string EXEC_SERV_NUM_OF_SERVER = "NumberOfServer";
            internal const string EXEC_SERV_ACTIVE = "Active";
            internal const string EXEC_SERV_PRIORITY = "Priority";
            internal const string EXEC_SERV_DESTINATION_FILTER = "vDestFilter";
            internal const string EXEC_SERV_BATCH_QUEUE_KEY = "BatchQueueKey";
            internal const string EXEC_SERV_MESSAGE_LOG = "Message";

		}
	}
}