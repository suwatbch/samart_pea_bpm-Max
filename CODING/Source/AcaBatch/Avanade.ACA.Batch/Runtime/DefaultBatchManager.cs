// ===============================================================================
// All contents of this file are proprietary and confidential.
// (c)2002 - 2005 Avanade Inc.  All rights reserved. Patents pending.
// ===============================================================================
using System;
using System.Data;
using System.Collections;
using System.Collections.Specialized;
using System.EnterpriseServices;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Avanade.ACA.Batch.Instrumentation;
using System.Data.Common;

namespace Avanade.ACA.Batch
{
	/// <summary>
	/// The DefaultBatchManager is a class contains a collection of static APIs for
	/// accessing data from the ACA.NET ACA.NET Batch Architecture database.  Most of its
	/// methods are all static and are grouped into inner classes by their subjects.
	/// The functionalities are provided by its inner classes.
	/// <UL>
	/// <LI><see cref="Destination"/></LI>
	/// <LI><see cref="Batch"/></LI>
	/// <LI><see cref="Job"/></LI>
	/// <LI><see cref="Parameter"/></LI>
	/// <LI><see cref="RequestQueue"/></LI>
	/// <LI><see cref="TypeDefinition"/></LI>
	/// <LI><see cref="ExecutionHistory"/></LI>
	/// </UL>
	/// </summary>
	public class DefaultBatchManager
	{
		private const string 
			INVALID_REQUEST = "There were no request that matches the key '{0}'.";

		/// <summary>
		/// This class contains all the static methods for accessing the type definitions 
		/// contained by the ACA.NET Batch database.  It provides the API for accessing both
		/// the Batch type and Job types.  
		/// The categories of the type specifies what kinds of object the types are for.  
		/// </summary>
		public class TypeDefinition
		{
			/// <summary>
			/// The enumeration for the Job and Batch types. CATEGORY_UNKNOWN for unknown
			/// category, CATEGORY_JOB_TYPE for Job and CATEGORY_BATCH_TYPE for Batch.
			/// </summary>
			public enum  TYPE_CATEGORY
			{
				/// <summary>
				/// An unknown category
				/// </summary>
				/// <value>0</value>
				CATEGORY_UNKNOWN,
				/// <summary>
				/// The category code used to indicate a 
				/// type that implements <see cref="Avanade.ACA.Batch.IJob"/>.
				/// </summary>
				/// <value>1</value>
				CATEGORY_JOB_TYPE,
				/// <summary>
				/// The category code used to indicate a 
				/// type that implements <see cref="Avanade.ACA.Batch.IBatch"/>.
				/// </summary>
				/// <value>2</value>
				CATEGORY_BATCH_TYPE
			}

            /// <summary>
            /// Save a new type definition to the ACA.NET Batch Database.  If the type
            /// definition already exists, update its properties.
            /// </summary>
            /// <param name="database">The Database instance that represents ACA.NET Batch Database.</param>
            /// <param name="typeKey">The unique identifier of the type definition.</param>
            /// <param name="typeName">The name of the type definitaion. It must be no more than
            /// 50 characters long.</param>
            /// <param name="typeDesc">The description of the type object.  It must be no more than
            /// 255 characters long.</param>
            /// <param name="typeClass">The .NET runtime class name the type object represents.</param>
            /// <param name="typeCategory">A enumeration value indicating whether the type is
            /// a Batch type, or a Job type.</param>
            /// <returns>void</returns>
			public static void Save(Database database, 
				Guid typeKey, 
				string typeName, 
				string typeDesc,
				string typeClass,
				TYPE_CATEGORY	typeCategory)
			{
				DbCommand cw = database.GetStoredProcCommand( 
					BatchDbConstants.SP.TYPE_SAVE);
                
                database.AddInParameter(cw, BatchDbConstants.Parm.TYPE_KEY, DbType.Guid, typeKey);
                database.AddInParameter(cw, BatchDbConstants.Parm.TYPE_CLASS, DbType.String, typeClass);
                database.AddInParameter(cw, BatchDbConstants.Parm.TYPE_NAME, DbType.String, typeName);
                database.AddInParameter(cw, BatchDbConstants.Parm.TYPE_DESC, DbType.String, typeDesc);
                database.AddInParameter(cw, BatchDbConstants.Parm.TYPE_CATEGORY, DbType.Int16, typeCategory);
				ExecuteNonQueryTx(database, cw);
			}

            /// <summary>
            /// Delete the type definition from the ACA.NET Batch Database.
            /// </summary>
            /// <param name="database">The Database instance that represents ACA.NET Batch Database.</param>
            /// <param name="typeKey">The unique identifier for the type definition.</param>
            /// <returns>void</returns>
			public static void Delete(Database database, Guid typeKey)
			{
				DbCommand cw = database.GetStoredProcCommand( 
					BatchDbConstants.SP.TYPE_DELETE);

                database.AddInParameter(cw, BatchDbConstants.Parm.TYPE_KEY, DbType.Guid, typeKey);
				ExecuteNonQueryTx(database, cw);
			}


            /// <summary>
            /// List all the type definitions in the ACA.NET Batch Database for a certain category.
            /// </summary>
            /// <param name="database">The Database instance that represents ACA.NET Batch Database.</param>
            /// <param name="typeCategory">A enumeration value indicating whether the type is
            /// a Batch type, or a Job type.</param>
            /// <remarks>The column sequence is: TypeKey, Type, TypeName, and TypeDesc.
            /// <P>The calling code is responsible for calling 
            /// <see cref="System.Data.IDataReader.Close"/> on the returning
            /// IDataReader object or the cursor won't be closed.</P></remarks>
            /// <returns>An IDataReader object; each record contains a type definition.</returns>
			public static IDataReader List(Database database, TYPE_CATEGORY typeCategory)
			{
				DbCommand cw = database.GetStoredProcCommand( 
					BatchDbConstants.SP.TYPE_LIST);
                database.AddInParameter(cw, BatchDbConstants.Parm.TYPE_CATEGORY, DbType.Int16, typeCategory);	
				return database.ExecuteReader( cw );
			}

		}

		/// <summary>
		/// Destination class contains a collection of static methods providing access to 
		/// the Batch Destionation definitions within the ACA.NET Batch Database.
		/// </summary>
		/// <remarks>
		/// A Batch destination is a symbolic name representing a destination for the batches 
		/// to be serviced.  Part of the properties of the Batch Server application configurations
		/// is a list of destination names that the server can be known as.  When a batch is 
		/// enqueued, there is a setting for the Batch Request for which batch destination it 
		/// can be executed.  Therefore, the server only service the requests that match one of
		/// its destinations and vice versa.  Multiple server can have the same destinations as
		/// they have same capacity to service the same batch.
		/// <P>The calling code is responsible for calling 
		/// <see cref="System.Data.IDataReader.Close"/> on the returning
		/// IDataReader object or the cursor won't be closed.</P>
		/// </remarks>
		public class Destination
		{
            /// <summary>
            /// Inserts a new Batch Destination or updates an existing Batch Destination.
            /// </summary>
            /// <param name="database">The Database instance that represents ACA.NET Batch Database.</param>
            /// <param name="destinationKey">The unique identifier of the Batch Destination.</param>
            /// <param name="destinationName">The name of the Batch Destination. It must be no more than
            /// 50 characters long.</param>
            /// <param name="destinationDesc">The description of the Batch Destination.  It must 
            /// be no more than 255 characters long.</param>
            /// <returns>void</returns>
			public static void Save(Database database, 
				Guid destinationKey, 
				string destinationName, 
				string destinationDesc)
			{
				DbCommand cw = database.GetStoredProcCommand( 
					BatchDbConstants.SP.DESTINATION_SAVE);
							
				if (destinationDesc == null)
				{
					destinationDesc = String.Empty;
				}

				database.AddInParameter(cw, BatchDbConstants.Parm.DESTINATION_KEY, DbType.Guid, destinationKey );
				database.AddInParameter(cw, BatchDbConstants.Parm.DESTINATION_NAME, DbType.String, destinationName );
				database.AddInParameter(cw, BatchDbConstants.Parm.DESTINATION_DESC, DbType.String, destinationDesc );
				ExecuteNonQueryTx(database, cw);
			}

            /// <summary>
            /// Deletes a Batch Destination definition with a certain key.
            /// </summary>
            /// <param name="database">The Database instance that represents ACA.NET 
            /// Batch Database.</param>
            /// <param name="destinationKey">The unique identifier of the Batch Destination.</param>
            /// <returns>void</returns>
			public static void Delete(Database database, Guid destinationKey)
			{
				DbCommand cw = database.GetStoredProcCommand( 
					BatchDbConstants.SP.DESTINATION_DELETE);
			
				database.AddInParameter(cw, BatchDbConstants.Parm.DESTINATION_KEY, DbType.Guid, destinationKey );
				ExecuteNonQueryTx(database, cw);
			}

            /// <summary>
            /// Lists all the Batch Destination definitions stored in the ACA.NET Batch Database.
            /// </summary>
            /// <param name="database">The Database instance that represents ACA.NET Batch Database.</param>
            /// <remarks>The column sequence is: DestKey, DestName and DestDesc.
            /// <P>The calling code is responsible for calling 
            /// <see cref="System.Data.IDataReader.Close"/> on the returning
            /// IDataReader object or the cursor won't be closed.</P></remarks>
            /// <returns>An IDataReader object; each row contains a Batch Destination.</returns>
			public static IDataReader List(Database database)
			{
				DbCommand cw = database.GetStoredProcCommand( 
					BatchDbConstants.SP.DESTINATION_LIST);
				return database.ExecuteReader( cw );
			}

		}
		
		/// <summary>
		/// This class contains all the static methods for accessing Job definitions with 
		/// the ACA.NET Batch Database.
		/// </summary>
		public class Job
		{
            /// <summary>
            /// Saves a Job definition into the ACA.NET Batch Database.  If the Job definition already
            /// exists, its data will be updated.
            /// </summary>
            /// <param name="database">The Database instance that represents ACA.NET Batch Database.</param>
            /// <param name="jobKey">The unique identifier of the Job definition.</param>
            /// <param name="jobName">Name of the Job with length no longer than 50 characters.</param>
            /// <param name="jobDesc">Description of the Job with length no longer than 255 characters.</param>
            /// <param name="jobTypeKey">The unique identifier of an existing Job Type.</param>
            /// <param name="restartBehavior">An enumeration value indicating whether the Job needs
            /// full restart.</param>
            /// <param name="jobCommitFreq">Number of work units performed by the Job between commits.</param>
            /// <remarks>The work unit is defined by the custom IJob class.  It can be a database records, files,
            /// or anything that's associated with the Batch application.  It will be interpreted and
            /// consumed by the custom code of IJob class.  The ACA.NET Batch Framework merely storing
            /// and passing the jobCommitFreq.</remarks>
            /// <returns>void</returns>
			public static void Save(Database database, 
				Guid jobKey, 
				string jobName, 
				string jobDesc, 
				Guid jobTypeKey,
				JobRestartBehavior restartBehavior,
				int jobCommitFreq)
			{
				DbCommand cw = database.GetStoredProcCommand( 
					BatchDbConstants.SP.JOB_SAVE);
			
				database.AddInParameter(cw, BatchDbConstants.Parm.JOB_KEY, DbType.Guid, jobKey );
				database.AddInParameter(cw, BatchDbConstants.Parm.TYPE_KEY, DbType.Guid, jobTypeKey );
				database.AddInParameter(cw, BatchDbConstants.Parm.JOB_NAME, DbType.String, jobName );
				database.AddInParameter(cw, BatchDbConstants.Parm.JOB_DESC, DbType.String, jobDesc );
				database.AddInParameter(cw, BatchDbConstants.Parm.JOB_RESTART, DbType.Byte, restartBehavior);
				database.AddInParameter(cw, BatchDbConstants.Parm.JOB_COMMIT_FREQ, DbType.Int32, jobCommitFreq );
				ExecuteNonQueryTx(database, cw);
			}

            /// <summary>
            /// Deletes the Job definition from the ACA.NET Batch Database.
            /// </summary>
            /// <param name="database">The Database instance that represents ACA.NET Batch Database.</param>
            /// <param name="jobKey">The unique identifier of the Job definition.</param>
            /// <returns>void</returns>
			public static void Delete( Database database, Guid jobKey )
			{
				DbCommand cw = database.GetStoredProcCommand( 
					BatchDbConstants.SP.JOB_DELETE);
			
				database.AddInParameter(cw, BatchDbConstants.Parm.JOB_KEY, DbType.Guid, jobKey );
				DefaultBatchManager.ExecuteNonQueryTx(database, cw);
			}

            /// <summary>
            ///  Lists all the Job definitions currently stored in the ACA.NET Batch Database.
            /// </summary>
            /// <param name="database">The Database instance that represents ACA.NET Batch Database.</param>
            /// <remarks>The columns in the order selected are: BatchJobKey, TypeKey, BatchJobName, 
            /// BatchJobDesc, BatchJobRestart, BatchJobCmitFreq, and TypeName.
            /// <P>The calling code is responsible for calling 
            /// <see cref="System.Data.IDataReader.Close"/> on the returning
            /// IDataReader object or the cursor won't be closed.</P></remarks>
            /// <returns>An IDataReader object.  Each record contains the data for one Job Definition.</returns>
			public static IDataReader List(Database database)
			{
				DbCommand cw = database.GetStoredProcCommand( 
					BatchDbConstants.SP.JOB_LIST);	
				return database.ExecuteReader( cw );
			}
		}

		/// <summary>
		/// This class contains all the static methods for accessing the ACA.NET Batch Architecture
		/// Batch definitions to the database.
		/// </summary>
		public class Batch
		{
            /// <summary>Saves a Batch definition into the ACA.NET Batch Database.  If the Batch 
            /// definition already exists, its data will be updated.
            /// </summary>
            /// <param name="database">The Database instance that represents ACA.NET Batch Database.</param>
            /// <param name="batchKey">The unique identifier of the Batch definition.</param>
            /// <param name="batchName">Name of the Batch with length no longer than 50 characters.</param>
            /// <param name="batchDesc">Description of the Batch with length no longer than 255 characters.</param>
            /// <param name="batchRestart">An enumeration value indicating how the Batch should
            /// behave when being restarted.  See <see cref="BatchRestartBehavior"/> 
            /// for the available values.</param>
            /// <param name="exePriorityLevel">An enumeration value for the execution priority of
            /// the Batch.  See <see cref="ExecutionPriorityLevel"/>
            /// for the available values.</param>
            /// <param name="queuePriorityLevel">&gt;An enumeration value for the priority of the Batch
            /// to be serviced when it is in the queue.  See <see cref="QueuePriorityLevel"/>
            /// for the available values.</param>
            /// <param name="maxConcurrent">The maximum number of the Batch can be run concurrently.
            /// Since this feature is not supported, the value should be '1'.</param>
            /// <param name="configFile">The path of main configuration file (for example, <I>application.exe.config</I>) 
            /// for the Batch application.  If the Batch application does not need any configuration 
            /// files, use an empty string as the value.</param>
            /// <param name="relativeExpMilliSec">The time-out limit the Batch is allowed to execute
            /// in milliseconds.</param>
            /// <param name="BatchDestKey">The unique identifier of a defined Batch Destinition.</param>
            /// <param name="isolationLevel">The level of isolation of the Batch to be executed.  Only
            /// ExecutionIsolationLevel.Process is supported.</param>
            /// <param name="batchTypeKey">The unique identifier of an existing Batch Type.</param>
            /// <returns>void</returns>
			public static void Save(Database database, 
				Guid batchKey, 
				string batchName, 
				string batchDesc, 
				BatchRestartBehavior batchRestart,
				ExecutionPriorityLevel exePriorityLevel,
				QueuePriorityLevel queuePriorityLevel,
				int maxConcurrent,
				string configFile,
				long relativeExpMilliSec, 
				Guid BatchDestKey,
				ExecutionIsolationLevel isolationLevel,
				Guid batchTypeKey)
			{
				DbCommand cw = database.GetStoredProcCommand( 
					BatchDbConstants.SP.BATCH_SAVE);
			
				database.AddInParameter(cw, BatchDbConstants.Parm.BATCH_KEY, DbType.Guid, batchKey );
				database.AddInParameter(cw, BatchDbConstants.Parm.BATCH_NAME, DbType.String, batchName );
				database.AddInParameter(cw, BatchDbConstants.Parm.BATCH_DESC, DbType.String, batchDesc );
				database.AddInParameter(cw, BatchDbConstants.Parm.BATCH_RESTART, DbType.Byte, batchRestart );
				database.AddInParameter(cw, BatchDbConstants.Parm.BATCH_EXE_PRIORITY, DbType.Byte, exePriorityLevel );
				database.AddInParameter(cw, BatchDbConstants.Parm.BATCH_QUEUE_PRIORITY, DbType.Byte, queuePriorityLevel );
				database.AddInParameter(cw, BatchDbConstants.Parm.BATCH_MAX_CONCURRENT, DbType.Int32, maxConcurrent );
				database.AddInParameter(cw, BatchDbConstants.Parm.BATCH_CONFIG, DbType.String, configFile );
				database.AddInParameter(cw, BatchDbConstants.Parm.BATCH_REL_EXPIRE_DATE, DbType.Int64, relativeExpMilliSec );
				database.AddInParameter(cw, BatchDbConstants.Parm.BATCH_DEST_KEY, DbType.Guid, BatchDestKey );
				database.AddInParameter(cw, BatchDbConstants.Parm.BATCH_ISOLATION, DbType.Byte, isolationLevel );
				database.AddInParameter(cw, BatchDbConstants.Parm.TYPE_KEY, DbType.Guid, batchTypeKey );
				ExecuteNonQueryTx(database, cw);
			}

            /// <summary>
            /// Deletes the Batch definition from the ACA.NET Batch Database.
            /// </summary>
            /// <param name="database">The Database instance that represents ACA.NET Batch Database.</param>
            /// <param name="batchKey">The unique identifier of the Batch definition.</param>
            /// <returns>void</returns>
			public static void Delete( Database database, Guid batchKey )
			{
				DbCommand cw = database.GetStoredProcCommand( 
					BatchDbConstants.SP.BATCH_DELETE);
			
				
				database.AddInParameter(cw, BatchDbConstants.Parm.BATCH_KEY, DbType.Guid, batchKey );
				DefaultBatchManager.ExecuteNonQueryTx(database, cw);
				
			}

            /// <summary>
            /// List all the Batches defined in the database.
            /// </summary>
            /// <param name="database">The Database instance that represents ACA.NET 
            /// Batch Database.</param>
            /// <remarks>The columns in the order selected are: BatchKey, BatchName, BatchDesc, 
            /// BatchSmartRestart, ExePriorityLevel, QuePriorityLevel, MaxConcurrent, ConfigFile, 
            /// RelativeExpDate, BatchDestKey, BatchIsolationID, and TypeKey.
            /// <P>The calling code is responsible for calling 
            /// <see cref="System.Data.IDataReader.Close"/> on the returning
            /// IDataReader object or the cursor won't be closed.</P></remarks>
            /// <returns>An IDataReader object.  Each record contains the data for one Batch 
            /// Definition.</returns>
			public static IDataReader List(Database database)
			{
				DbCommand cw = database.GetStoredProcCommand( 
					BatchDbConstants.SP.BATCH_LIST);	
				return database.ExecuteReader( cw );
			}

            /// <summary>
            /// Lists all the Jobs for a Batch in the order of their sequence number with the Batch.
            /// </summary>
            /// <param name="database">The Database instance that represents ACA.NET Batch Database.</param>
            /// <param name="batchKey">The unique identifier of the Batch definition.</param>
            /// <remarks>The selected columns are: BatchJobKey, TypeKey, BatchKey, BatchJobSequence,
            /// BatchJobName, BatchJobDesc,	JobSmartRestart, BatchJobCmitFreq, TypeName,
            /// and TypeClass.
            /// <P>The calling code is responsible for calling 
            /// <see cref="System.Data.IDataReader.Close"/> on the returning
            /// IDataReader object or the cursor won't be closed.</P></remarks>
            /// <returns>An IDataReader object.  Each record contains a job record the Batch
            /// contains.</returns>
			public static IDataReader ListJobs(Database database, Guid batchKey)
			{
				DbCommand cw = database.GetStoredProcCommand( 
					BatchDbConstants.SP.BATCH_JOB_LIST);	
				database.AddInParameter(cw, BatchDbConstants.Parm.BATCH_KEY, DbType.Guid, batchKey );
				return database.ExecuteReader( cw );
			}

            /// <summary>
            /// Disassociate all Jobs from a Batch definition.  The Job definitions stay intact.
            /// </summary>
            /// <param name="database">The Database instance that represents ACA.NET Batch Database.</param>
            /// <param name="batchKey">The unique identifier of the Batch definition.</param>
            /// <returns>void</returns>
			public static void CleanJobs( Database database, 
				Guid batchKey)
			{
				DbCommand cw = database.GetStoredProcCommand( 
					BatchDbConstants.SP.BATCH_JOB_CLEAN);
			
				database.AddInParameter(cw, BatchDbConstants.Parm.BATCH_KEY, DbType.Guid, batchKey );
				ExecuteNonQueryTx(database, cw);
			}

            /// <summary>
            /// Disassociate one Job for a Batch definition.
            /// </summary>
            /// <param name="database">The Database instance that represents ACA.NET Batch Database.</param>
            /// <param name="batchKey">The unique identifier of the Batch definition.</param>
            /// <param name="jobKey">The unique identifier of the Job definition.</param>
            /// <param name="jobSequenceNo">The original sequence number for the Job within the
            /// Batch.</param>
            /// <returns>void</returns>
			public static void RemoveJob( Database database, 
				Guid batchKey,
				Guid jobKey,
				int jobSequenceNo)
			{
				DbCommand cw = database.GetStoredProcCommand( 
					BatchDbConstants.SP.BATCH_JOB_REMOVE);
			
				database.AddInParameter(cw, BatchDbConstants.Parm.BATCH_KEY, DbType.Guid, batchKey );
				database.AddInParameter(cw, BatchDbConstants.Parm.JOB_KEY, DbType.Guid, jobKey );
				database.AddInParameter(cw, BatchDbConstants.Parm.BATCH_JOB_MAP_SEQ, DbType.Int16, jobSequenceNo );
				ExecuteNonQueryTx(database, cw);
			}

            /// <summary>
            /// Adds a Job to a Batch definition.  If the Job has already associated with the Batch,
            /// updates its sequence number.
            /// </summary>
            /// <param name="database">The Database instance that represents ACA.NET Batch Database.</param>
            /// <param name="batchKey">The unique identifier of the Batch definition.</param>
            /// <param name="jobKey">The unique identifier of the Job definition.</param>
            /// <param name="jobSequenceNo">The sequence number of the Job within all Jobs for
            /// a Batch.  This value starts at 1.</param>
            /// <param name="oldJobSequenceNo">The original sequence number of the Job for the
            /// Batch.  If the Job is new for the Batch, use '0'.</param>
            /// <remarks>A Batch can contain multiple entries of the same Job.  For example, the users
            /// may want to execute the same funtion multiple times.  In that case, AddJob can
            /// be called for each entries with the same Job but different sequence number.
            /// When changing one of the entry from one Job defintion to another, use the 
            /// oldJobSequenceNo to indicate with entry is the one to be changed.</remarks>
            /// <returns>void</returns>
			public static void AddJob(Database database, 
				Guid batchKey,
				Guid jobKey, 
				int jobSequenceNo,
				int oldJobSequenceNo)
			{
				DbCommand cw = database.GetStoredProcCommand( 
					BatchDbConstants.SP.BATCH_JOB_ADD);
			
				database.AddInParameter(cw, BatchDbConstants.Parm.BATCH_KEY, DbType.Guid, batchKey );
				database.AddInParameter(cw, BatchDbConstants.Parm.JOB_KEY, DbType.Guid, jobKey );
				database.AddInParameter(cw, BatchDbConstants.Parm.BATCH_JOB_MAP_SEQ, DbType.Int16, jobSequenceNo );
				database.AddInParameter(cw, BatchDbConstants.Parm.BATCH_JOB_MAP_SEQ_OLD, DbType.Int16, oldJobSequenceNo );
				ExecuteNonQueryTx(database, cw);
			}
		}

		/// <summary>
		/// The Parameter class contains all the static methods for accessing the ACA.NET Batch 
		/// Database Parameter definitions.  The parameter can be a parameter of a Batch, a Job,
		/// a Batch Request, Batch Execution Context, or Job Execution Context.
		/// </summary>
		public class Parameter
		{

            /// <summary>
            /// Adds or update a Parameter to the ACA.NET Batch Database.  If the parmkey
            /// is Guid.Empty, a new Parameter object will be added and its key value assigned; 
            /// otherwise
            /// </summary>
            /// <param name="database">The Database instance that represents ACA.NET Batch Database.
            /// </param>
            /// <param name="parmKey">The unique identifier of the Parameter.</param>
            /// <param name="parmName">The name of the Parameter.</param>
            /// <param name="parmTypeName">The name of the parameter type.  It is optional and is only 
            /// needed when parmTypeKey equal to Guid.Empty.</param>
            /// <param name="parmTypeKey">By ref.  The key of the parameter type.  If the parameter 
            /// type key is unknown, use Guid.Empty instead.</param>
            /// <param name="parmExternalKey">The unique identifier of the object within the 
            /// ACA.NET Batch Architecture this Parameter belongs to.  A Batch key if this is
            /// a Batch Parameter; a Job key if this is a Job Parameter, etc.
            /// The type of the object is determined by <i>parmCategory</i>.
            /// </param>
            /// <param name="parmValue">The serialized value of the Parameter.</param>
            /// <param name="parmCategory">The category of the Parameter since there are many
            /// types of Parameters (Batch, Job, Request, etc).  See <see cref="ParameterCategory"/> for the avilable categories.</param>
            /// <remarks>If the value of <i>parmTypeKey</i> is Guid.Empty, it will try to resolve
            /// the Parameter Type by searching for Parameter Type in the database for what
            /// matches the <I>ParmTypeName</I> parameter.  If the Parameter Type is not found,
            /// it will create a new Parameter type.
            /// </remarks>
            /// <returns>void</returns>
			public static void Save(Database database, 
				ref Guid parmKey, 
				string parmName, 
				string parmTypeName,
				ref Guid parmTypeKey,
				Guid parmExternalKey,
				string parmValue,
				ParameterCategory parmCategory )
			{
				IDbConnection connection = null;                
				DbTransaction curTrans = null;
				
				try
				{
					if ( parmTypeKey == Guid.Empty )
					{
						parmTypeKey = SaveParamType( database, 
							ref connection,
							ref curTrans,
							Guid.NewGuid(), 
							parmTypeName, 
							"");
					}

					DbCommand cw = null;
					if (parmKey != Guid.Empty)
					{
						cw = database.GetStoredProcCommand( 
							BatchDbConstants.SP.PARM_SAVE_BY_KEY);

						database.AddInParameter(cw, BatchDbConstants.Parm.PARAM_KEY, DbType.Guid, parmKey );
						database.AddInParameter(cw, BatchDbConstants.Parm.PARAM_TYPE_KEY, DbType.Guid, parmTypeKey );
						database.AddInParameter(cw, BatchDbConstants.Parm.PARAM_EXT_KEY, DbType.Guid, parmExternalKey );
						database.AddInParameter(cw, BatchDbConstants.Parm.PARAM_NAME, DbType.String, parmName );
						database.AddInParameter(cw, BatchDbConstants.Parm.PARAM_VAL, DbType.String, parmValue );
						database.AddInParameter(cw, BatchDbConstants.Parm.PARAM_CATEGORY, DbType.Int16, parmCategory );
					}
					else
					{				
						parmKey = Guid.NewGuid();
						cw = database.GetStoredProcCommand( 
							  BatchDbConstants.SP.PARM_SAVE_BY_NAME);

						database.AddInParameter(cw, BatchDbConstants.Parm.PARAM_KEY, DbType.Guid, parmKey );
						database.AddInParameter(cw, BatchDbConstants.Parm.PARAM_EXT_KEY, DbType.Guid, parmExternalKey );
						database.AddInParameter(cw, BatchDbConstants.Parm.PARAM_TYPE_KEY, DbType.Guid, parmTypeKey );
						database.AddInParameter(cw, BatchDbConstants.Parm.PARAM_NAME, DbType.String, parmName );
						database.AddInParameter(cw, BatchDbConstants.Parm.PARAM_VAL, DbType.String, parmValue );
						database.AddInParameter(cw, BatchDbConstants.Parm.PARAM_CATEGORY, DbType.Int16, parmCategory );

						database.AddOutParameter(cw, BatchDbConstants.Parm.PARAM_NEW_KEY, DbType.Guid, 16 );
					}
					DefaultBatchManager.JoinTransaction(cw , database, ref curTrans, ref connection);

					ExecuteCommand(database, cw, curTrans);

					if (parmKey == Guid.Empty)
					{
						object key = database.GetParameterValue(cw, BatchDbConstants.Parm.PARAM_NEW_KEY );
						if (key == DBNull.Value)
						{
							throw new ACABatchException(
								string.Format("Could not find parameter '{0}'.",
								parmName));
						}
						parmKey = (Guid)key;
					}
					DefaultBatchManager.EndTransaction(curTrans, connection, true);
				}
				catch (Exception)
				{
					DefaultBatchManager.EndTransaction(curTrans, connection, false);
					throw;
				}
			}

            /// <summary>
            /// Saves a Parameter Type in the Batch Database.  If there are existing Paramter Type
            /// that has the same name, it's data will be updated.  If this operation is done within
            /// an existing transcation, pass in the connection and transaction.  Otherwise, set
            /// both objects' value to null prior to calling this method.  A new transaction will
            /// be opened.  The caller must call </summary>
            /// <param name="database">The Database instance that represents ACA.NET Batch Database.</param>
            /// <param name="connection">The database connection object obtained by invoking
            /// </param>
            /// <param name="transaction">The database transaction object obtained by invoking
            /// </param>
            /// <param name="parmTypeKey">The unique identifier of the Parameter Type.</param>
            /// <param name="parmTypeName">The name of the Parameter Type with length no
            /// more than 50 characters.</param>
            /// <param name="parmTypeDesc">The description of the Parameter Type; no more than
            /// 255 characters in length.</param>
            /// <returns>System.Guid</returns>
			public static Guid SaveParamType(Database database, 
				ref IDbConnection connection,
				ref DbTransaction transaction,
				Guid parmTypeKey, 
				string parmTypeName, 
				string parmTypeDesc)
			{
				DbCommand cw = database.GetStoredProcCommand( 
					BatchDbConstants.SP.PARM_TYPE_SAVE);
			
				database.AddInParameter(cw, BatchDbConstants.Parm.PARAM_TYPE_KEY, DbType.Guid, parmTypeKey );
				database.AddInParameter(cw, BatchDbConstants.Parm.PARAM_TYPE_NAME, DbType.String, parmTypeName );
				database.AddInParameter(cw, BatchDbConstants.Parm.PARAM_TYPE_DESC, DbType.String, parmTypeDesc );
                database.AddOutParameter(cw, BatchDbConstants.Parm.PARAM_TYPE_KEY_NEW, DbType.Guid, 16);

				DefaultBatchManager.JoinTransaction(cw, database, ref transaction, ref connection);
				ExecuteCommand(database, cw, transaction);
				return (Guid)database.GetParameterValue(cw, BatchDbConstants.Parm.PARAM_TYPE_KEY_NEW );
			}

            /// <summary>
            /// Deletes a Parameter Type from the Batch Database.
            /// </summary>
            /// <param name="database">The Database instance that represents ACA.NET Batch Database.</param>
            /// <param name="parmTypeKey">The unique identifier of the Parameter Type.</param>
            /// <returns>void</returns>
			public static void DeleteParamType( Database database, 
				Guid parmTypeKey )
			{
				DbCommand cw = database.GetStoredProcCommand( 
					BatchDbConstants.SP.PARM_TYPE_DELETE);
			
				database.AddInParameter(cw, BatchDbConstants.Parm.PARAM_TYPE_KEY, DbType.Guid, parmTypeKey );
				ExecuteNonQueryTx(database, cw);
			}

            /// <summary>
            /// Saves a Parameter to the Batch Database by its name.  If another Parameter for the
            /// same external key and category already exists, its data will be updated.
            /// </summary>
            /// <param name="database">The Database instance that represents ACA.NET Batch Database.</param>
            /// <param name="connection">The database connection object obtained by invoking
            /// </param>
            /// <param name="transaction">The database transaction object obtained by invoking
            /// </param>
            /// <param name="parmName">The name of the Parameter.</param>
            /// <param name="parmTypeName">The name of the Parameter Type with length no
            /// more than 50 characters.</param>
            /// <param name="parmTypeKey">By ref.  The key of the parameter type.  If the parameter 
            /// type key is unknown, use Guid.Empty instead.</param>
            /// <param name="parmExternalKey">The unique identifier of the object within the 
            /// ACA.NET Batch Architecture this Parameter belongs to.  A Batch key if this is
            /// a Batch Parameter; a Job key if this is a Job Parameter, etc.
            /// The type of the object is determined by </param>
            /// <param name="parmValue">The serialized value of the Parameter.</param>
            /// <param name="parmCategory">The category of the Parameter since there are many
            /// types of Parameters (Batch, Job, Request, etc).  See </param>
            /// <returns>System.Guid</returns>
			public static Guid SaveByName(Database database, 
				ref IDbConnection connection,
				ref DbTransaction transaction,
				string parmName, 
				string parmTypeName,
				ref Guid parmTypeKey,
				Guid parmExternalKey,
				string parmValue,
				ParameterCategory parmCategory)
			{
				if ( parmTypeKey == Guid.Empty )
				{
					parmTypeKey = SaveParamType( database, 
						ref connection,
						ref transaction,
						Guid.NewGuid(), 
						parmTypeName, 
						"");
				}
				Guid parmKey = Guid.NewGuid();
				DbCommand cw = database.GetStoredProcCommand( 
					BatchDbConstants.SP.PARM_SAVE_BY_NAME);
			
				database.AddInParameter(cw, BatchDbConstants.Parm.PARAM_KEY, DbType.Guid, parmKey );
				database.AddInParameter(cw, BatchDbConstants.Parm.PARAM_EXT_KEY, DbType.Guid, parmExternalKey );
				database.AddInParameter(cw, BatchDbConstants.Parm.PARAM_TYPE_KEY, DbType.Guid, parmTypeKey );
				database.AddInParameter(cw, BatchDbConstants.Parm.PARAM_NAME, DbType.String, parmName );
				database.AddInParameter(cw, BatchDbConstants.Parm.PARAM_VAL, DbType.String, parmValue );
				database.AddInParameter(cw, BatchDbConstants.Parm.PARAM_CATEGORY, DbType.Int16, parmCategory );
				database.AddOutParameter(cw, BatchDbConstants.Parm.PARAM_NEW_KEY, DbType.Guid, 16);

				DefaultBatchManager.JoinTransaction(cw, database, ref transaction, ref connection);
			
				ExecuteCommand(database, cw, transaction);
				parmKey = (Guid)database.GetParameterValue(cw, BatchDbConstants.Parm.PARAM_NEW_KEY );
				return parmKey;
			}

            /// <summary>
            /// Deletes the Parameter object from the Batch Database.
            /// </summary>
            /// <param name="database">The Database instance that represents ACA.NET Batch Database.</param>
            /// <param name="parmKey">The unique identifier of the Parameter.</param>
            /// <returns>void</returns>
			public static void Delete( Database database, 
				Guid parmKey )
			{
				DbCommand cw = database.GetStoredProcCommand( 
					BatchDbConstants.SP.PARM_DELETE);
			
				database.AddInParameter(cw, BatchDbConstants.Parm.PARAM_KEY, DbType.Guid, parmKey );
				ExecuteNonQueryTx(database, cw);
			}

            /// <summary>
            /// Lists all the Parameters for a object stored in the ACA.NET Batch Database.
            /// </summary>
            /// <param name="database">The Database instance that represents ACA.NET Batch Database.</param>
            /// <param name="parmExternalKey">The unique identifier of the object within the 
            /// ACA.NET Batch Architecture this Parameter belongs to.  A Batch key if this is
            /// a Batch Parameter; a Job key if this is a Job Parameter, etc.  
            /// The type of the object is determined by <i>parmCategory</i>.
            /// </param>
            /// <param name="parmCategory">The category of the Parameter since there are many
            /// types of Parameters (Batch, Job, Request, etc).  See <see cref="ParameterCategory"/>
            /// for the avilable categories.</param>
            /// <remarks>The columns in the order selected are: BatchParmKey, ParmExternalKey,  
            /// ParmName, ParmVal, ParmCategory, ParmTypeName, and ParmTypeKey.
            /// <P>The calling code is responsible for calling 
            /// <see cref="System.Data.IDataReader.Close"/> on the returning
            /// IDataReader object or the cursor won't be closed.</P></remarks>
            /// <returns>An IDataReader object.  Each record contains a parameter object.</returns>
			public static IDataReader List(Database database, 
				Guid parmExternalKey, 
				ParameterCategory parmCategory)
			{
				DbCommand cw = database.GetStoredProcCommand( 
					BatchDbConstants.SP.PARM_LIST);	
				database.AddInParameter(cw, BatchDbConstants.Parm.PARAM_EXT_KEY, DbType.Guid, parmExternalKey );
				database.AddInParameter(cw, BatchDbConstants.Parm.PARAM_CATEGORY, DbType.Int16, parmCategory );
				return database.ExecuteReader( cw );
			}

            /// <summary>
            /// Copies the Parameter form one category to the next.  This is used by 
            /// </summary>
            /// <param name="database">The Database instance that represents ACA.NET Batch Database.</param>
            /// <param name="connection">The database connection object obtained by invoking
            /// </param>
            /// <param name="transaction">The database transaction object obtained by invoking
            /// </param>
            /// <param name="parmExternalKey">The key for the external object that this Parameter
            /// is associated with.  For example, batch key for Batch object.</param>
            /// <param name="newParmExternalKey">The for the external object that the new
            /// Parameter is associated with.</param>
            /// <param name="parmCategory">The category of the original parameter.  
            /// See </param>
            /// <param name="newParmCategory">The category of the new parameter.</param>
            /// <returns>void</returns>
			internal static void Copy(Database database, 
				ref IDbConnection connection,
				ref DbTransaction transaction,
				Guid parmExternalKey, 
				Guid newParmExternalKey,
				ParameterCategory parmCategory, 
				ParameterCategory newParmCategory)
			{
				DbCommand cw = database.GetStoredProcCommand( 
					BatchDbConstants.SP.PARM_LIST);	

				DbCommand cwWrite = database.GetStoredProcCommand( 
					BatchDbConstants.SP.PARM_ADD);
				
				database.AddInParameter(cw, BatchDbConstants.Parm.PARAM_EXT_KEY, DbType.Guid, parmExternalKey );
				database.AddInParameter(cw, BatchDbConstants.Parm.PARAM_CATEGORY, DbType.Int16, parmCategory );

				IDataReader reader = null;

				try
				{			
					reader = database.ExecuteReader( cw );
					//Select sequence: BatchParmKey, ParmExternalKey, ParmName,
					//ParmVal, ParmCategory, ParmTypeName, ParmTypeKey
					while (reader.Read())
					{	
						Guid parmKey = Guid.NewGuid();
						Guid parmTypeKey = reader.GetGuid(6);
						string parmName = reader.GetString(2);
						string parmValue = String.Empty;

						if (!reader.IsDBNull(3))
						{
							parmValue = reader.GetString(3);
						}

						if (cwWrite.Parameters.Count == 0)
						{
							database.AddInParameter(cwWrite, BatchDbConstants.Parm.PARAM_KEY, DbType.Guid, parmKey );
							database.AddInParameter(cwWrite, BatchDbConstants.Parm.PARAM_EXT_KEY, DbType.Guid, newParmExternalKey );
							database.AddInParameter(cwWrite, BatchDbConstants.Parm.PARAM_TYPE_KEY, DbType.Guid, parmTypeKey );
							database.AddInParameter(cwWrite, BatchDbConstants.Parm.PARAM_NAME, DbType.String, parmName );
							database.AddInParameter(cwWrite, BatchDbConstants.Parm.PARAM_VAL, DbType.String, parmValue );
							database.AddInParameter(cwWrite, BatchDbConstants.Parm.PARAM_CATEGORY, DbType.Int16, newParmCategory );
						}
						else
						{
							database.SetParameterValue(cwWrite, BatchDbConstants.Parm.PARAM_KEY, parmKey );
							database.SetParameterValue(cwWrite, BatchDbConstants.Parm.PARAM_EXT_KEY, newParmExternalKey );
							database.SetParameterValue(cwWrite, BatchDbConstants.Parm.PARAM_TYPE_KEY, parmTypeKey );
							database.SetParameterValue(cwWrite, BatchDbConstants.Parm.PARAM_NAME, parmName );
							database.SetParameterValue(cwWrite, BatchDbConstants.Parm.PARAM_VAL, parmValue );
							database.SetParameterValue(cwWrite, BatchDbConstants.Parm.PARAM_CATEGORY, newParmCategory );
						}

						DefaultBatchManager.JoinTransaction(cwWrite, 
							database, 
							ref transaction, 
							ref connection);

						ExecuteCommand(database, cwWrite, transaction);
					}
				}
				finally
				{
					if( reader != null )
					{
						reader.Close();
					}
				}
			}
		}

		/// <summary>
		/// RequestQueue class contains all the static methods for accessing the ACA.NET 
		/// Batch definitions to and from the Batch Database.
		/// </summary>
		public class RequestQueue
		{
            /// <summary>
            /// Creates a RequestEnqueueHandle for enqueuing a Batch Request.  The handle wraps
            /// the transaction and database connection necessary for performing multiple database
            /// operations needed to putting a Request in the ACA.NET Batch Queue.
            /// </summary>
            /// <param name="database">The Database instance that represents ACA.NET Batch Database.</param>
            /// <param name="requestKey">The unique identifier of a Batch Request.</param>
            /// <param name="batchKey">The unique identifier of a Batch definition.  Use Guid.Empty
            /// if it is unknown and resolve to use the batchName instead to find the Batch 
            /// definition.</param>
            /// <param name="batchName">The name of the Batch definition; needed only if the
            /// batchKey is unknown.</param>
            /// <param name="parentRequestKey">If the Request is a spun-off by another Request,
            /// this is the unique identifier of the Request that creates the request.  Otherwise,
            /// this should be Guid.Empty.</param>
            /// <returns>A handle that can be invoked to do the actual enqueuing.</returns>
			public static RequestEnqueueHandle CreateRequestEnqueueHandle( Database database, 
				Guid requestKey, 
				Guid batchKey,
				string batchName,
				Guid parentRequestKey)
			{
				return new RequestEnqueueHandle( database, 
					requestKey,
					batchKey, 
					batchName,
					parentRequestKey);
			}

            /// <summary>
            /// Finds a Request in the Queue that has either "Enqueued" or 
            /// "Enqueue Restarted" status that has not been expired and matches 
            /// the destination filter.  Marks the status of the Request as being 
            /// "Dequeued" and return its setting.
            /// </summary>
            /// <param name="database">The Database instance that represents ACA.NET Batch Database.</param>
            /// <param name="destinationFilter">A string containing the name of Destinations
            /// the server is capable of servicing, delimited by a comma (",")
            /// character.  For example, "Dest1,Dest2" (no spaces in string) for 
            /// filtering on both Destination Dest1 and Dest2.</param>
            /// <param name="serverName">The name of the Batch Server.</param>
            /// <param name="requestKey">Output parameter for the uniqie identifier of the 
            /// Request object.</param>
            /// <param name="batchName">Output parameter for the name of the Batch the Request
            /// is set up to run.</param>
            /// <param name="isolationLevel">The execution isolation level of the found 
            /// Request.</param>
            /// <param name="priority">The execution priority level of the found request.</param>
            /// <param name="configurationFile">The configuration file path, if the Batch instance
            /// has been configured to use any.</param>
            /// <remarks>Dequeuing actually means marking that a Request is being serviced.
            /// The actual request stays in the queue and its status code turned into
            /// Dequeued.
            /// <P>The sequence of requests being dequeued is based on their queuing priority
            /// level, then by their queued date.</P><P>A Request can only be dequeued by one Server.</P></remarks>
            /// <returns>True means that a non-expired and enqueued Request is found that 
            /// matches the destination filters.  False means otherwise.</returns>
			public static bool Dequeue(Database database, 
				string destinationFilter, 
				string serverName,
				out Guid requestKey,
				out string batchName,
				out ExecutionIsolationLevel isolationLevel,
				out ExecutionPriorityLevel priority,
				out string configurationFile)
			{
				// select the highest priority, oldest request that have the status of either
				// enqueue or enqueueRestart
				DbCommand cw = database.GetStoredProcCommand( 
					BatchDbConstants.SP.Q_DEQUEUE);

				// opening a connection
				IDbConnection connection = null;                
				DbTransaction  curTrans = null;
				try
				{
					// Start a local transaction
					DefaultBatchManager.JoinTransaction(cw, 
						database, 
						ref curTrans, 
						ref connection);

					database.AddInParameter(cw, BatchDbConstants.Parm.Q_DEST_FILTER, DbType.String, destinationFilter );
					database.AddInParameter(cw, BatchDbConstants.Parm.Q_BATCH_SERVER_NAME, DbType.String, serverName );
					database.AddOutParameter(cw,BatchDbConstants.Parm.Q_KEY, DbType.Guid, 16);
					database.AddOutParameter(cw,BatchDbConstants.Parm.BATCH_NAME, DbType.String, 50);
					database.AddOutParameter(cw,BatchDbConstants.Parm.BATCH_ISOLATION, DbType.Byte, 1);
					database.AddOutParameter(cw,BatchDbConstants.Parm.BATCH_EXE_PRIORITY, DbType.Byte, 1);
					database.AddOutParameter(cw,BatchDbConstants.Parm.BATCH_CONFIG, DbType.String, 255);
					// dequeue
					ExecuteCommand(database, cw, curTrans);

					object obj = database.GetParameterValue(cw, BatchDbConstants.Parm.Q_KEY );
					if (obj != DBNull.Value)
					{
						requestKey = (Guid)obj;
						batchName = (string)database.GetParameterValue(cw,BatchDbConstants.Parm.BATCH_NAME);
						byte isolationValue = (byte)database.GetParameterValue(cw,BatchDbConstants.Parm.BATCH_ISOLATION);
						byte executionPriorityValue = (byte)database.GetParameterValue(cw,BatchDbConstants.Parm.BATCH_EXE_PRIORITY);
						
						isolationLevel = (ExecutionIsolationLevel)
							Enum.ToObject(typeof(ExecutionIsolationLevel), isolationValue);

						priority = (ExecutionPriorityLevel)
							Enum.ToObject(typeof(ExecutionPriorityLevel), executionPriorityValue);
						
						configurationFile = String.Empty;

						object configurationFileObject = database.GetParameterValue(cw,BatchDbConstants.Parm.BATCH_CONFIG);
						if (configurationFileObject != DBNull.Value)
						{
							configurationFile = (string)configurationFileObject;
						}

						DefaultBatchManager.EndTransaction( curTrans, connection, true );
                        BatchInstrumentationProvider.Instance.FireRequestDequeuedEvent(
                                    requestKey,
                                    batchName,
                                    BatchProcessStatusCode.Dequeued.ToString());

						return true;
					}
					else
					{
						requestKey = Guid.Empty;
						batchName = "";
						isolationLevel = ExecutionIsolationLevel.Thread;
						priority = ExecutionPriorityLevel.Normal;
						configurationFile = "";
						DefaultBatchManager.EndTransaction( curTrans, connection, false ); 
						return false;
					}
				}
				catch (Exception)
				{
					DefaultBatchManager.EndTransaction( curTrans, connection, false ); 
					throw;
				}
			}


            /// <summary>
            /// List all the requests, including the active ones and the archived ones, 
            /// filtering by their status code.
            /// </summary>
            /// <param name="database">The Database instance that represents ACA.NET Batch 
            /// Database.
            /// </param>
            /// <param name="statusCodeFilter">A string contains the filter.  The format is
            /// "statusCode1,statusCode2,...", with no spaces in between.  
            /// The status code are integer value defined in 
            /// Avanade.ACA.Batch.BatchProcessStatusCode enumeration.
            /// </param>
            /// <remarks>The columns in the order selected are: BatchQueueKey, BatchName, BatchDesc,
            /// BatchSmartRestart, ConfigFile, IsolationLevel, ExePriorityLevel, QuePriorityLevel,
            /// MaxConcurrent, DestFilter, BatchTypeClass, RelativeExpDate,	AbsExpDate,
            /// BatchServerName, QueuedDate, StartDate,	LastUpdateDate,	BatchStatusCode, 
            /// OrigBatchKey, LastExecKey, BatchClientName,	ToPause, ParentRequestKey, 
            /// and NextExecutionKey.
            /// <P>The calling code is responsible for calling 
            /// <see cref="System.Data.IDataReader.Close"/> on the returning
            /// IDataReader object or the cursor won't be closed.</P><P>Since there are large number of columns gets selected, it would be hard to parse
            /// the result set.  <B>DefaultBatchManager.RequestQueue.ReadResults()</B> provides a
            /// easy way to get data from each.</P></remarks>
            /// <returns>A IDataReader object containing the selected records.</returns>
			public static IDataReader ListByStatus(Database database, 
				string statusCodeFilter)
			{
				DbCommand cw = database.GetStoredProcCommand( 
					BatchDbConstants.SP.REQUEST_LIST_BY_STATUS);	
				database.AddInParameter(cw, BatchDbConstants.Parm.Q_STATUS_FILTER, DbType.String, statusCodeFilter );
				return database.ExecuteReader( cw );
			}

            /// <summary>
            /// List all the requests, including the active and archived ones, filtered by the origial
            /// Batch Key.
            /// </summary>
            /// <param name="database">The Database instance that represents ACA.NET Batch Database.</param>
            /// <param name="batchKey">A unique identifier of the Batch definition.</param>
            /// <remarks>The columns in the order selected are: BatchQueueKey, BatchName, BatchDesc,
            /// BatchSmartRestart, ConfigFile, IsolationLevel, ExePriorityLevel, QuePriorityLevel,
            /// MaxConcurrent, DestFilter, BatchTypeClass, RelativeExpDate,	AbsExpDate,
            /// BatchServerName, QueuedDate, StartDate,	LastUpdateDate,	BatchStatusCode, 
            /// OrigBatchKey, LastExecKey, BatchClientName,	ToPause, ParentRequestKey, 
            /// and NextExecutionKey.
            /// <P>The calling code is responsible for calling 
            /// <see cref="System.Data.IDataReader.Close"/> on the returning
            /// IDataReader object or the cursor won't be closed.</P><P>Since there are large number of columns gets selected, it would be hard to parse
            /// the result set.  <B>DefaultBatchManager.RequestQueue.ReadResults()</B> provides a
            /// easy way to get data from each.</P></remarks>
            /// <returns>A IDataReader object containing the selected records.</returns>
			public static IDataReader ListByBatch(Database database, 
				Guid batchKey)
			{
				DbCommand cw = database.GetStoredProcCommand( 
					BatchDbConstants.SP.REQUEST_LIST_BY_BATCH);	
				database.AddInParameter(cw, BatchDbConstants.Parm.BATCH_KEY, DbType.Guid, batchKey );
				return database.ExecuteReader( cw );
			}

            /// <summary>
            /// List all the requests, including the active and archived ones, filtered by date 
            /// the request enters the queue.
            /// </summary>
            /// <param name="database">The Database instance that represents ACA.NET Batch 
            /// Database.</param>
            /// <param name="startDate">The start date for the filtering.</param>
            /// <param name="endDate">The end date for the filtering.</param>
            /// <remarks>The columns in the order selected are: HistQueueKey, BatchName, BatchDesc,
            /// BatchSmartRestart, ConfigFile, IsolationLevel, ExePriorityLevel, QuePriorityLevel,
            /// MaxConcurrent, DestFilter, BatchTypeClass, RelativeExpDate,	AbsExpDate,
            /// BatchServerName, QueuedDate, StartDate,	LastUpdateDate,	BatchStatusCode, 
            /// OrigBatchKey, LastExecKey, BatchClientName,	ToPause, ParentRequestKey, 
            /// and NextExecutionKey.
            /// <P>The calling code is responsible for calling 
            /// <see cref="System.Data.IDataReader.Close"/> on the returning
            /// IDataReader object or the cursor won't be closed.</P><P>Since there are large number of columns gets selected, it would be hard to parse
            /// the result set.  <B>DefaultBatchManager.RequestQueue.ReadResults()</B> provides a
            /// easy way to get data from each.</P></remarks>
            /// <returns>A IDataReader object containing the selected records.</returns>
			public static IDataReader ListByDate(Database database, 
				DateTime startDate, 
				DateTime endDate)
			{
				DbCommand cw = database.GetStoredProcCommand( 
					BatchDbConstants.SP.REQUEST_LIST_BY_Q_DATE);	
				DateTime DBMaxDate =  DateTime.Parse("12/31/4712");
				DateTime DBMinDate =  DateTime.Parse("1/1/1900");

				if (startDate > DBMaxDate)
				{
					startDate = DBMaxDate;
				}
				else if (startDate < DBMinDate)
				{
					startDate = DBMinDate;
				}
				if (endDate > DBMaxDate)
				{
					endDate = DBMaxDate;
				}
				else if (endDate < DBMinDate)
				{
					endDate = DBMinDate;
				}

				database.AddInParameter(cw, BatchDbConstants.Parm.START_DATE, DbType.DateTime, startDate );
				database.AddInParameter(cw, BatchDbConstants.Parm.END_DATE, DbType.DateTime, endDate );

				return database.ExecuteReader( cw );
			}

            /// <summary>
            /// List all the requests, including the active and archived ones, filtered by the 
            /// parent requests.  The result data includes the parent request and all its 
            /// children ordered from the oldest to the latest.
            /// </summary>
            /// <param name="database">The Database instance that represents ACA.NET Batch Database.</param>
            /// <param name="parentRequestKey">The key of the load-balancing request that
            /// enqueued other child requests.</param>
            /// <remarks>The columns in the order selected are: HistQueueKey, BatchName, BatchDesc,
            /// BatchSmartRestart, ConfigFile, IsolationLevel, ExePriorityLevel, QuePriorityLevel,
            /// MaxConcurrent, DestFilter, BatchTypeClass, RelativeExpDate,	AbsExpDate,
            /// BatchServerName, QueuedDate, StartDate,	LastUpdateDate,	BatchStatusCode, 
            /// OrigBatchKey, LastExecKey, BatchClientName,	ToPause, ParentRequestKey, 
            /// and NextExecutionKey.
            /// <P>This method is used for managing the Load-Balancing Requests.  A Load-
            /// Balancing Request (request) enqueues (child) other requests when it executes.</P><P>The calling code is responsible for calling 
            /// <see cref="System.Data.IDataReader.Close"/> on the returning
            /// IDataReader object or the cursor won't be closed.</P><P>Since there are large number of columns gets selected, it would be hard to 
            /// parse the result set.  <B>DefaultBatchManager.RequestQueue.ReadResults()</B> 
            /// provides a easy way to get data from each.</P></remarks>
            /// <returns>A IDataReader object containing the selected records.</returns>
			public static IDataReader ListByParent(Database database, 
				Guid parentRequestKey)
			{
				DbCommand cw = database.GetStoredProcCommand( 
					BatchDbConstants.SP.REQUEST_LIST_BY_PARENT);	
				database.AddInParameter(cw, BatchDbConstants.Parm.Q_PARENT_KEY, DbType.Guid, parentRequestKey );

				return database.ExecuteReader( cw );
			}

            /// <summary>
            /// List all the restart history of a requests, including the active and 
            /// archived ones.
            /// </summary>
            /// <param name="database">The Database instance that represents ACA.NET Batch 
            /// Database.</param>
            /// <param name="requestKey">The uniqie identifier of the Request object.</param>
            /// <remarks><P>The columns in the order selected are: HistQueueKey, BatchName, BatchDesc,
            /// BatchSmartRestart, ConfigFile, IsolationLevel, ExePriorityLevel, QuePriorityLevel,
            /// MaxConcurrent, DestFilter, BatchTypeClass, RelativeExpDate,	AbsExpDate,
            /// BatchServerName, QueuedDate, StartDate,	LastUpdateDate,	BatchStatusCode, 
            /// OrigBatchKey, LastExecKey, BatchClientName,	ToPause, ParentRequestKey, 
            /// and NextExecutionKey.</P><P>The calling code is responsible for calling 
            /// <see cref="System.Data.IDataReader.Close"/> on the returning
            /// IDataReader object or the cursor won't be closed.</P><P>Since there are large number of columns gets selected, it would be hard to 
            /// parse the result set.  <B>DefaultBatchManager.RequestQueue.ReadResults()</B> 
            /// provides a easy way to get data from each.</P></remarks>
            /// <returns>A IDataReader object containing the selected records.  The list starts
            /// all the way from the very first request in the restart chain to the latest 
            /// attempt, if the request belongs to a series of restart and failure.  If the 
            /// request has not been restarted, the list will contains only one record.</returns>
			public static IDataReader ListRestarts(Database database, 
				Guid requestKey)
			{
				DbCommand cw = database.GetStoredProcCommand( 
					BatchDbConstants.SP.REQUEST_LIST_RESTART_HISTORY);	
				database.AddInParameter(cw, BatchDbConstants.Parm.Q_KEY, DbType.Guid, requestKey );

				return database.ExecuteReader( cw );
			}

            /// <summary>
            /// List the request details filtering by its key.
            /// There should be only one row in the results.
            /// </summary>
            /// <param name="database">The Database instance that represents ACA.NET Batch Database.</param>
            /// <param name="requestKey">The uniqie identifier of the Request object.</param>
            /// <remarks>The columns in the order selected are: HistQueueKey, BatchName, BatchDesc,
            /// BatchSmartRestart, ConfigFile, IsolationLevel, ExePriorityLevel, QuePriorityLevel,
            /// MaxConcurrent, DestFilter, BatchTypeClass, RelativeExpDate,	AbsExpDate,
            /// BatchServerName, QueuedDate, StartDate,	LastUpdateDate,	BatchStatusCode, 
            /// OrigBatchKey, LastExecKey, BatchClientName,	ToPause, ParentRequestKey, 
            /// and NextExecutionKey.
            /// <P>The calling code is responsible for calling 
            /// <see cref="System.Data.IDataReader.Close"/> on the returning
            /// IDataReader object or the cursor won't be closed.</P><B>DefaultBatchManager.RequestQueue.ReadResults()</B> provides a
            /// easy way to get data from each.
            /// </remarks>
            /// <returns>A IDataReader object containing the selected records.</returns>
			public static IDataReader ListDetails(Database database, 
				Guid requestKey)
			{
				DbCommand cw = database.GetStoredProcCommand( 
					BatchDbConstants.SP.REQUEST_LIST_DETAILS);	
				database.AddInParameter(cw, BatchDbConstants.Parm.Q_KEY, DbType.Guid, requestKey );

				return database.ExecuteReader( cw );
			}

            /// <summary>
            /// Lists all the Job details for the Request.
            /// </summary>
            /// <param name="database">The Database instance that represents ACA.NET Batch 
            /// Database.</param>
            /// <param name="requestKey">The unique key of the Request object.</param>
            /// <remarks>The columns in the order selected are::BatchJobExecKey, BatchQueueKey,	BatchJobName,
            /// BatchJobDesc, JobSeq, JobSmartRestart, BatchJobCmitFreq, JobTypeClass, StartDate,
            /// JobStatusCode, LastCommitDate, WorkUnitCount, RestartCount,
            /// CommitCount, OrigJobKey, and LastExeStatus.
            /// <P>The calling code is responsible for calling <B>Close( )</B> on the returning
            /// IDataReader object or the cursor won't be closed.</P></remarks>
            /// <returns>A IDataReader object containing the selected job execution records.
            /// </returns>
			public static IDataReader ListJobExecution( Database database, 
				Guid requestKey )
			{
				DbCommand cw = database.GetStoredProcCommand( 
					BatchDbConstants.SP.REQUEST_JOBEXEC_LIST);	
				database.AddInParameter(cw, BatchDbConstants.Parm.Q_KEY, DbType.Guid, requestKey );
				return database.ExecuteReader( cw );
			}

            /// <summary>
            /// Lists all the execution exceptions for a Job Execution.
            /// </summary>
            /// <param name="database">The Database instance that represents ACA.NET Batch 
            /// Database.</param>
            /// <param name="jobExecutionKey">The uniqie identifier of the Job Execution 
            /// record of a ACA.NET
            /// Batch Request.</param>
            /// <remarks><P>The calling code is responsible for calling <B>Close( )</B> on the returning
            /// IDataReader object or the cursor won't be closed.</P></remarks>
            /// <returns>A IDataReader object containing the selected exceptions.</returns>
			public static IDataReader ListExceptions(Database database,
				Guid jobExecutionKey)
			{
				DbCommand cw = database.GetStoredProcCommand( 
					BatchDbConstants.SP.EXCEPTION_LIST);	
				database.AddInParameter(cw,BatchDbConstants.Parm.Q_JOB_KEY, DbType.Guid, jobExecutionKey);
				return database.ExecuteReader( cw );
			}

            /// <summary>
            /// Parses the current record of the IDataReader object into the 
            /// correspondent variables.<BR/>
            /// This is a helper method for parsing the result getting from any of the
            /// <b><I>DefaultBatchManager.RequestQueue.List...( )</I></b> methods since the 
            /// IDataReader has large amount of columns and can be hard to parse.
            /// </summary>
            /// <param name="reader">The IDataReader object that has already been
            /// called <B>Read( )</B> on.</param>
            /// <param name="requestKey">Output parameter.  The unique key for the 
            /// Request.</param>
            /// <param name="batchName">Output parameter.  The name of the Batch instance
            /// the Request is based on.</param>
            /// <param name="batchDesc">Output parameter.  The description for the
            /// Batch instance the Request is based on.</param>
            /// <param name="restartBehavior">Output parameter.  The restart behavior 
            /// for the Batch Request.</param>
            /// <param name="configFile">Output parameter.  The configuration file for
            /// the instance the Request is based on.  If the Batch is not configured to
            /// use a configuration file, this value will be string.Empty.</param>
            /// <param name="isolationLevel">Output parameter.  The execution isolation
            /// level for the Request.  Always Process.</param>
            /// <param name="exePriorityLevel">Output parameter.  The execution priority
            /// level for the Request.</param>
            /// <param name="quePriorityLevel">Output parameter.  The queuing priority
            /// level for the Request.</param>
            /// <param name="maxConcurrent">Output parameter.  The maximum concurrency of
            /// the Request.  Always 1.</param>
            /// <param name="destFilter">Output parameter.  The Destination that the
            /// request is configure to run at.</param>
            /// <param name="batchTypeClass">Output parameter.  The runtime type of
            /// the IBatch class the request uses.</param>
            /// <param name="relativeExpDate">Output parameter.  The execution time-out
            /// value of the request.  In milliseconds.</param>
            /// <param name="absExpDate">Output parameter.  The expriation date for the
            /// request if it hasn't been serviced by any Batch Server (dequeued).</param>
            /// <param name="batchServerName">Output parameter.  The name of the
            /// Batch Server servicing the request.  If the request hasn't been dequeued,
            /// this value will be string.Empty.</param>
            /// <param name="batchClientName">Output parameter.  The name of the Batch
            /// Client the enqueued the request.</param>
            /// <param name="queuedDate">Output parameter.  The time stamp for when the
            /// request has been enqueued.</param>
            /// <param name="startDate">Output parameter.  The time stamp for when the
            /// request starts it execution.  DateTime.MinValue for the requests who's
            /// execution has not started.</param>
            /// <param name="lastUpdateDate">Output parameter.  The time stamp for when
            /// the last time the request's status has been updated.</param>
            /// <param name="batchStatusCode">Output parameter.  The status code of the
            /// request.</param>
            /// <param name="origBatchKey">Output parameter.  The key for the original
            /// batch instance that the request is configured for.  Noted that if the
            /// Batch definition has been changed since the request was enqueued, the
            /// origBatchKey might not refer to the original configuration anymore.</param>
            /// <param name="lastExecKey">Output parameter.  The key of the last request
            /// in the restart chain.  If the request was enqueued as a restart of a previously
            /// failed batch, the value would be null.</param>
            /// <param name="toPause">Output parameter.  True if the request being asked
            /// to pause or is already paused.  False if the request is being ask to be 
            /// resumed from a pausing state or is currently not paused.</param>
            /// <param name="parentRequestKey">Output parameter.  The key for the Load-
            /// Balancing request that enqueued the request.  Null if the request is
            /// not enqueued as a result of running Load-Balancing batch.</param>
            /// <param name="nextExecutionKey">Output parameter.  The key of the next request
            /// in the restart chain.  If the request was not a restart of some failed request,
            /// this value would be null.</param>
            /// <returns>void</returns>
			public static void ReadResults(IDataReader reader,
				out Guid requestKey,
				out string batchName, 
				out string batchDesc, 
				out BatchRestartBehavior restartBehavior, 
				out string configFile, 
				out ExecutionIsolationLevel isolationLevel, 
				out ExecutionPriorityLevel exePriorityLevel,
				out QueuePriorityLevel quePriorityLevel,
				out int maxConcurrent, 
				out string destFilter, 
				out string batchTypeClass,
				out long relativeExpDate, 
				out DateTime absExpDate, 
				out string batchServerName, 
				out string batchClientName,
				out DateTime queuedDate, 
				out DateTime startDate, 
				out DateTime lastUpdateDate, 
				out BatchProcessStatusCode batchStatusCode, 
				out Guid origBatchKey, 
				out Guid lastExecKey,
				out bool toPause,
				out Guid parentRequestKey,
				out Guid nextExecutionKey)
			{
				#region Initializing Out Parameters
				batchName = "";
				batchDesc = "";
				restartBehavior = BatchRestartBehavior.RestartAtFailedJob;
				configFile = "";
				isolationLevel = ExecutionIsolationLevel.Thread;
				exePriorityLevel = ExecutionPriorityLevel.Normal;
				quePriorityLevel = QueuePriorityLevel.Normal;
				maxConcurrent = 1;
				destFilter = "";
				batchTypeClass = "";
				relativeExpDate = 0;
				absExpDate = DateTime.MaxValue;
				batchServerName = "";
				batchClientName = "";
				queuedDate = DateTime.MinValue;
				startDate = DateTime.MinValue;
				lastUpdateDate = DateTime.MaxValue;
				origBatchKey = Guid.Empty;
				lastExecKey = Guid.Empty;
				batchStatusCode = BatchProcessStatusCode.Unknown;
				parentRequestKey = Guid.Empty;
				toPause = false;
				nextExecutionKey = Guid.Empty;
				#endregion
				// The columns in the order selected are:: BatchQueueKey, BatchName, BatchDesc, 
				// BatchSmartRestart, ConfigFile, IsolationLevel, ExePriorityLevel,
				// QuePriorityLevel, MaxConcurrent, DestFilter, BatchTypeClass,
				// RelativeExpDate, AbsExpDate, BatchClientName, BatchServerName,
				// QueuedDate, StartDate, LastUpdateDate, BatchStatusCode, OrigBatchKey,
				// LastExecKey, ToPause, ParentRequestKey, and NextExcutionKey.
				requestKey = reader.GetGuid(0);
				batchName = reader.GetString(1); 
				batchDesc = String.Empty;
				if (!reader.IsDBNull(2))
				{
					batchDesc = reader.GetString(2);
				}
				restartBehavior = (BatchRestartBehavior)reader.GetByte(3);
				if (!reader.IsDBNull(4))
				{
					configFile = reader.GetString(4); 
				}
				isolationLevel = (ExecutionIsolationLevel)reader.GetByte(5); 
				exePriorityLevel = (ExecutionPriorityLevel)reader.GetByte(6);
				quePriorityLevel = (QueuePriorityLevel)reader.GetByte(7);
				maxConcurrent = reader.GetInt32(8); 
				if (!reader.IsDBNull(9))
				{
					destFilter = reader.GetString(9); 
				}
				if (!reader.IsDBNull(10))
				{
					batchTypeClass = reader.GetString(10);
				}
				relativeExpDate = reader.GetInt64(11); 
				absExpDate = reader.GetDateTime(12); 
				if (!reader.IsDBNull(13))
				{
					batchClientName = reader.GetString(13);
				}
				if (reader.IsDBNull(14) == false)
				{
					batchServerName = reader.GetString(14);
				}
				queuedDate = reader.GetDateTime(15); 
				// the following value is maxvalue
				if (reader.IsDBNull(16) == false)
				{
					startDate = reader.GetDateTime(16); 
				}
				// the following can be null
				if (reader.IsDBNull(17) == false)
				{
					lastUpdateDate = reader.GetDateTime(17);
				}
				batchStatusCode = (BatchProcessStatusCode)reader.GetByte(18); 
				if (reader.IsDBNull(19) == false)
				{
					origBatchKey = reader.GetGuid(19); 
				}
				if (reader.IsDBNull(20) == false)
				{
					lastExecKey = reader.GetGuid(20);
				}
				toPause = reader.GetBoolean(21);
				if (reader.IsDBNull(22) == false)
				{
					parentRequestKey = reader.GetGuid(22);
				}
				if (reader.IsDBNull(23) == false)
				{
					nextExecutionKey = reader.GetGuid(23);
				}
			}

            /// <summary>
            /// Gets the detailed information for a Request.
            /// </summary>
            /// <param name="database">The Database instance that represents ACA.NET Batch Database.</param>
            /// <param name="requestKey">The unique key for the Request.</param>
            /// <param name="batchName">Output parameter.  The name of the Batch instance
            /// the Request is based on.</param>
            /// <param name="batchDesc">Output parameter.  The description for the
            /// Batch instance the Request is based on.</param>
            /// <param name="restartBehavior">Output parameter.  The restart behavior 
            /// for the Batch Request.</param>
            /// <param name="configFile">Output parameter.  The configuration file for
            /// the instance the Request is based on.  If the Batch is not configured to
            /// use a configuration file, this value will be string.Empty.</param>
            /// <param name="isolationLevel">Output parameter.  The execution isolation
            /// level for the Request.  Always Process.</param>
            /// <param name="exePriorityLevel">Output parameter.  The execution priority
            /// level for the Request.</param>
            /// <param name="quePriorityLevel">Output parameter.  The queuing priority
            /// level for the Request.</param>
            /// <param name="maxConcurrent">Output parameter.  The maximum concurrency of
            /// the Request.  Always 1.</param>
            /// <param name="destFilter">Output parameter.  The Destination that the
            /// request is configure to run at.</param>
            /// <param name="batchTypeClass">Output parameter.  The runtime type of
            /// the IBatch class the request uses.</param>
            /// <param name="relativeExpDateMilliSec">Output parameter.  The execution time-out
            /// value of the request.  In milliseconds.</param>
            /// <param name="absExpDate">Output parameter.  The expriation date for the
            /// request if it hasn't been serviced by any Batch Server (dequeued).</param>
            /// <param name="batchServerName">Output parameter.  The name of the
            /// Batch Server servicing the request.  If the request hasn't been dequeued,
            /// this value will be string.Empty.</param>
            /// <param name="batchClientName">Output parameter.  The name of the Batch
            /// Client the enqueued the request.</param>
            /// <param name="queuedDate">Output parameter.  The time stamp for when the
            /// request has been enqueued.</param>
            /// <param name="startDate">Output parameter.  The time stamp for when the
            /// request starts it execution.  DateTime.MinValue for the requests who's
            /// execution has not started.</param>
            /// <param name="lastUpdateDate">Output parameter.  The time stamp for when
            /// the last time the request's status has been updated.</param>
            /// <param name="batchStatusCode">Output parameter.  The status code of the
            /// request.</param>
            /// <param name="origBatchKey">Output parameter.  The key for the original
            /// batch instance that the request is configured for.  Noted that if the
            /// Batch definition has been changed since the request was enqueued, the
            /// origBatchKey might not refer to the original configuration anymore.</param>
            /// <param name="lastExecKey">Output parameter.  The key of the last request
            /// in the restart chain.  If the request was enqueued as a restart of a previously
            /// failed batch, the value would be null.</param>
            /// <param name="toPause">Output parameter.  True if the request being asked
            /// to pause or is already paused.  False if the request is being ask to be 
            /// resumed from a pausing state or is currently not paused.</param>
            /// <param name="parentRequestKey">Output parameter.  The key for the Load-
            /// Balancing request that enqueued the request.  Null if the request is
            /// not enqueued as a result of running Load-Balancing batch.</param>
            /// <param name="nextExecutionKey">Output parameter.  The key of the next request
            /// in the restart chain.  If the request was not a restart of some failed request,
            /// this value would be null.</param>
            /// <returns>void</returns>
			public static void Get(
				Database database, 
				Guid requestKey,
				out string batchName, 
				out string batchDesc, 
				out BatchRestartBehavior restartBehavior, 
				out string configFile, 
				out ExecutionIsolationLevel isolationLevel, 
				out ExecutionPriorityLevel exePriorityLevel,
				out QueuePriorityLevel quePriorityLevel,
				out int maxConcurrent, 
				out string destFilter, 
				out string batchTypeClass,
				out long relativeExpDateMilliSec, 
				out DateTime absExpDate, 
				out string batchServerName, 
				out string batchClientName,
				out DateTime queuedDate, 
				out DateTime startDate, 
				out DateTime lastUpdateDate, 
				out BatchProcessStatusCode batchStatusCode, 
				out Guid origBatchKey, 
				out Guid lastExecKey,
				out bool toPause,
				out Guid parentRequestKey,
				out Guid nextExecutionKey)
			{
				IDataReader dataReader = null;
				try
				{
					dataReader = ListDetails(database, 
						requestKey);
					dataReader.Read();
					Guid tempKey = Guid.Empty;
					DefaultBatchManager.RequestQueue.ReadResults(
						dataReader,
						out tempKey,
						out batchName, 
						out batchDesc, 
						out restartBehavior, 
						out configFile, 
						out isolationLevel, 
						out exePriorityLevel,
						out quePriorityLevel,
						out maxConcurrent, 
						out destFilter, 
						out batchTypeClass,
						out relativeExpDateMilliSec, 
						out absExpDate, 
						out batchServerName, 
						out batchClientName,
						out queuedDate, 
						out startDate, 
						out lastUpdateDate, 
						out batchStatusCode, 
						out origBatchKey, 
						out lastExecKey,
						out toPause,
						out parentRequestKey,
						out nextExecutionKey);
				}
				finally
				{
					if (dataReader != null)
					{
						dataReader.Close();
					}
				}
			}

            /// <summary>
            /// Cancel an request that has neither been completed nor cancelled.
            /// </summary>
            /// <param name="database">The Database instance that represents ACA.NET Batch Database.</param>
            /// <param name="batchQueueRequestKey">The key for the request.</param>
            /// <returns>void</returns>
			public static void Delete( Database database, Guid batchQueueRequestKey )
			{
				try
				{
					// archieve the request
					using (
						DbCommand cw = database.GetStoredProcCommand( 
							BatchDbConstants.SP.Q_ARCHIEVE))
					{			
						database.AddInParameter(cw, BatchDbConstants.Parm.Q_KEY, DbType.Guid, batchQueueRequestKey );
						ExecuteNonQueryTx(database, cw);
					}
				}
				catch(Exception e)
				{
					throw new ACABatchException("Failed to archieve the batch.", e);
				}

			}


            /// <summary>
            /// Get certain fields for a batch request.
            /// </summary>
            /// <param name="database">The Database instance that represents ACA.NET Batch Database.
            /// </param>
            /// <param name="requestKey">The key for the request.</param>
            /// <param name="batchRestartBehavior">The restart behavior.</param>
            /// <param name="statusCode">The status code.</param>
            /// <param name="nextExecutionKey">The key of the next request in the restart 
            /// chain.</param>
            /// <returns>void</returns>
			internal static void GetRequestData(Database database, 
				Guid requestKey, 
				out BatchRestartBehavior batchRestartBehavior,
				out BatchProcessStatusCode statusCode,
				out Guid nextExecutionKey)
			{
				IDataReader detailsReader = null;
				try
				{
					detailsReader = ListDetails(database, requestKey);
					if (detailsReader.Read())
					{
						batchRestartBehavior = (BatchRestartBehavior)detailsReader.GetByte(3);
						statusCode = (BatchProcessStatusCode)detailsReader.GetByte(18); 
						nextExecutionKey = Guid.Empty;
						if (detailsReader.IsDBNull(23) == false)
						{
							nextExecutionKey =  detailsReader.GetGuid(23);
						}
					}
					else
					{
						throw new BatchRequestNotFoundException (
							string.Format(INVALID_REQUEST, requestKey.ToString()));
					}
				}
				catch (Exception)
				{
					throw new BatchRequestNotFoundException (
						string.Format(INVALID_REQUEST, requestKey.ToString()));
				}
				finally
				{
					if (detailsReader != null)
					{
						detailsReader.Close();
					}
				}
			}

            /// <summary>GetRequestParameterCategoryMappings</summary>
            /// <param name="batchRestartBehavior">Avanade.ACA.Batch.BatchRestartBehavior</param>
            /// <param name="failedJobRestartBehavior">Avanade.ACA.Batch.JobRestartBehavior</param>
            /// <returns>System.Collections.Specialized.ListDictionary</returns>
			private static ListDictionary GetRequestParameterCategoryMappings(
				BatchRestartBehavior batchRestartBehavior,
				JobRestartBehavior failedJobRestartBehavior)
			{
				ListDictionary categoryMappings = new ListDictionary();

				switch (batchRestartBehavior)
				{
					case BatchRestartBehavior.Full:
						categoryMappings.Add(ParameterCategory.RequestOriginal, 
							new ParameterCategory[] { ParameterCategory.RequestOriginal,
														ParameterCategory.BatchExecutionContextCurrent,
														ParameterCategory.BatchExecutionContextEndOfSucceededJob});
						break;
					case BatchRestartBehavior.RestartAtFailedJob:
						categoryMappings.Add(ParameterCategory.RequestOriginal, 
							new ParameterCategory[] { ParameterCategory.RequestOriginal });
						if (failedJobRestartBehavior == JobRestartBehavior.Full)
						{
							categoryMappings.Add(ParameterCategory.BatchExecutionContextEndOfSucceededJob,
								new ParameterCategory[] { ParameterCategory.BatchExecutionContextCurrent,
															ParameterCategory.BatchExecutionContextEndOfSucceededJob});
						}
						else
						{
							categoryMappings.Add(ParameterCategory.BatchExecutionContextCurrent, 
								new ParameterCategory[] { ParameterCategory.BatchExecutionContextCurrent });
							categoryMappings.Add(ParameterCategory.BatchExecutionContextEndOfSucceededJob, 
								new ParameterCategory[] { ParameterCategory.BatchExecutionContextEndOfSucceededJob });
						}
						break;
					case BatchRestartBehavior.SkipFailedJobAndContinue:
						categoryMappings.Add(ParameterCategory.RequestOriginal, 
							new ParameterCategory[] { ParameterCategory.RequestOriginal });
						categoryMappings.Add(ParameterCategory.BatchExecutionContextCurrent, 
							new ParameterCategory[] { ParameterCategory.BatchExecutionContextCurrent });
						categoryMappings.Add(ParameterCategory.BatchExecutionContextEndOfSucceededJob, 
							new ParameterCategory[] { ParameterCategory.BatchExecutionContextEndOfSucceededJob });
						break;
				}
				return categoryMappings;
			}

            /// <summary>GetJobParameterCategoryMappings</summary>
            /// <param name="batchRestartBehavior">Avanade.ACA.Batch.BatchRestartBehavior</param>
            /// <param name="failedJobRestartBehavior">Avanade.ACA.Batch.JobRestartBehavior</param>
            /// <returns>System.Collections.Specialized.ListDictionary</returns>
			private static ListDictionary GetJobParameterCategoryMappings(
				BatchRestartBehavior batchRestartBehavior,
				JobRestartBehavior failedJobRestartBehavior)
			{
				ListDictionary categoryMappings = new ListDictionary();


				switch (batchRestartBehavior)
				{
					case BatchRestartBehavior.Full:
						categoryMappings.Add(ParameterCategory.JobExecutionContextOriginal, 
							new ParameterCategory[] { ParameterCategory.JobExecutionContextOriginal,
													ParameterCategory.JobExecutionContextCurrent});
						break;
					case BatchRestartBehavior.RestartAtFailedJob:
						if (failedJobRestartBehavior == JobRestartBehavior.Full)
						{
							categoryMappings.Add(ParameterCategory.JobExecutionContextOriginal, 
								new ParameterCategory[] { ParameterCategory.JobExecutionContextOriginal,
														ParameterCategory.JobExecutionContextCurrent });
						}
						else
						{
							categoryMappings.Add(ParameterCategory.JobExecutionContextOriginal, 
								new ParameterCategory[] { ParameterCategory.JobExecutionContextOriginal });
							categoryMappings.Add(ParameterCategory.JobExecutionContextCurrent, 
								new ParameterCategory[] { ParameterCategory.JobExecutionContextCurrent });
						}
						break;
					case BatchRestartBehavior.SkipFailedJobAndContinue:
						categoryMappings.Add(ParameterCategory.JobExecutionContextOriginal, 
							new ParameterCategory[] { ParameterCategory.JobExecutionContextOriginal });
						categoryMappings.Add(ParameterCategory.JobExecutionContextCurrent, 
							new ParameterCategory[] { ParameterCategory.JobExecutionContextCurrent });
						break;
				}
				
				return categoryMappings;
			}

            /// <summary>
            /// Copy parameter from one category to another.  Used internally.
            /// </summary>
            /// <param name="database">The Database instance that represents ACA.NET Batch Database.</param>
            /// <param name="connection">The database connection object obtained by invoking
            /// </param>
            /// <param name="transaction">The database transaction object obtained by invoking
            /// </param>
            /// <param name="parmExternalKey">The key for the object this parameter belongs
            /// to.</param>
            /// <param name="newParmExternalKey">The key for the new object the new parameter
            /// belongs to.</param>
            /// <param name="categoryMappings">A ListDictionary contains the mappings between 
            /// new and old categories.</param>
            /// <returns>void</returns>
			internal static void CopyParameters(Database database, 
				ref IDbConnection connection,
				ref DbTransaction transaction,
				Guid parmExternalKey, 
				Guid newParmExternalKey,
				ListDictionary categoryMappings)
			{
				IDataReader reader = null;
				ArrayList commandWriterArray = new ArrayList();

				try
				{
					foreach (object key in categoryMappings.Keys)
					{
						DbCommand cwRead = database.GetStoredProcCommand( 
							BatchDbConstants.SP.PARM_LIST );	

						ParameterCategory parmCategory = (ParameterCategory)key;

                        database.AddInParameter(cwRead, BatchDbConstants.Parm.PARAM_EXT_KEY, DbType.Guid, parmExternalKey);
                        database.AddInParameter(cwRead, BatchDbConstants.Parm.PARAM_CATEGORY, DbType.Int16, parmCategory);

						DefaultBatchManager.JoinTransaction( cwRead, 
							database, 
							ref transaction, 
							ref connection );

						reader = ExecuteReader(database, cwRead, transaction);

						//Select sequence: BatchParmKey, ParmExternalKey, ParmName,
						//ParmVal, ParmCategory, ParmTypeName, ParmTypeKey
						while (reader.Read())
						{	
							Guid parmTypeKey = reader.GetGuid(6);
							string parmName = reader.GetString(2);
							string parmValue = String.Empty;
							if (!reader.IsDBNull(3))
							{
								parmValue = reader.GetString(3);
							}

							ParameterCategory[] categories = (ParameterCategory[])categoryMappings[key];
							foreach (ParameterCategory newParmCategory in categories)
							{
								DbCommand cwWrite = database.GetStoredProcCommand( 
									BatchDbConstants.SP.PARM_SAVE_BY_NAME);

								Guid parmKey = Guid.NewGuid();

								database.AddInParameter(cwWrite, BatchDbConstants.Parm.PARAM_KEY, DbType.Guid, parmKey );
								database.AddInParameter(cwWrite, BatchDbConstants.Parm.PARAM_EXT_KEY, DbType.Guid, newParmExternalKey );
								database.AddInParameter(cwWrite, BatchDbConstants.Parm.PARAM_TYPE_KEY, DbType.Guid, parmTypeKey );
								database.AddInParameter(cwWrite, BatchDbConstants.Parm.PARAM_NAME, DbType.String, parmName );
								database.AddInParameter(cwWrite, BatchDbConstants.Parm.PARAM_VAL, DbType.String, parmValue );
								database.AddInParameter(cwWrite, BatchDbConstants.Parm.PARAM_CATEGORY, DbType.Int16, newParmCategory );
								database.AddOutParameter(cwWrite, BatchDbConstants.Parm.PARAM_NEW_KEY, DbType.Guid, 16 ); 
								commandWriterArray.Add(cwWrite);
							}
						}
						if (reader != null)
						{
							reader.Close();
							reader = null;
						}
					}
					// now the reader has been closed, we can do the writting
					foreach (object obj in commandWriterArray)
					{
						DbCommand cwWrite = (DbCommand)obj;
						DefaultBatchManager.JoinTransaction(cwWrite, 
							database, 
							ref transaction, 
							ref connection);
						ExecuteCommand(database, cwWrite, transaction);
					}
				}
				finally
				{
					if( reader != null )
					{
						reader.Close();
					}
				}				
			}

            /// <summary>
            /// Restart a ACA.NET Batch Request from its failure state.
            /// </summary>
            /// <param name="database">The Database instance that represents ACA.NET Batch 
            /// Database.</param>
            /// <param name="requestKey">The unique key for the Request.</param>
            /// <param name="newRequestKey">The unique identifier of the new Request to be enqueued
            /// as a result of the restart.</param>
            /// <param name="queuePriorityLevel">The queuing priority of the Request.  Use
            /// DBNull.Value if not wishing to override the priority of the failed request.</param>
            /// <param name="absoluteExpDate">The expiration date for the new Request in the Queue.
            /// </param>
            /// <param name="BatchDestName">The Destination of the Batch.  Use DBNull.Value if
            /// wishing to use the Destination of the failed Request for the new Request.</param>
            /// <remarks>This method creates a new Request that has all the data of the failed 
            /// Request and puts it in the queue.</remarks>
            /// <returns>void</returns>
			public static void RestartFromFailure(Database database, 
				Guid requestKey, 
				Guid newRequestKey,
				object queuePriorityLevel,
				DateTime absoluteExpDate, 
				object BatchDestName)
			{
				BatchRestartBehavior batchRestartBehavior;
				BatchProcessStatusCode statusCode;
				Guid nextExecKey;

				GetRequestData(database, requestKey, out batchRestartBehavior, out statusCode, out nextExecKey);
				if (nextExecKey != Guid.Empty)
				{
					throw new ACABatchException(string.Format(
						"Request {0} has already been restarted.",
						requestKey));
				}

				const byte NULL_SUBSTITUTE_VALUE = 127;

				object priorityObject = queuePriorityLevel == DBNull.Value ? NULL_SUBSTITUTE_VALUE : queuePriorityLevel;

				// copy the request data from the original failed request to the new request
				DbCommand cw = database.GetStoredProcCommand( 
					BatchDbConstants.SP.Q_RESTART_REQUEST);

				IDbConnection connection = null;                
				DbTransaction  curTrans = null;
				try
				{
					// Start a local transaction
					DefaultBatchManager.OpenTransaction(
						cw,
						database, 
						ref curTrans, 
						ref connection,
						IsolationLevel.ReadCommitted);

					database.AddInParameter(cw, BatchDbConstants.Parm.Q_KEY, DbType.Guid, requestKey );
					database.AddInParameter(cw, BatchDbConstants.Parm.Q_NEW_KEY, DbType.Guid, newRequestKey );
					database.AddInParameter(cw, BatchDbConstants.Parm.BATCH_QUEUE_PRIORITY, DbType.Byte, priorityObject );
					database.AddInParameter(cw, BatchDbConstants.Parm.Q_ABS_EXP_DATE, DbType.DateTime, absoluteExpDate );
					database.AddInParameter(cw, BatchDbConstants.Parm.DESTINATION_NAME, DbType.String, BatchDestName );
					database.AddOutParameter(cw, BatchDbConstants.Parm.BATCH_KEY, DbType.Guid, 16 );
					ExecuteCommand(database, cw, curTrans);

					// make a copy of Job Execution for each job exection, get the batch restart 
					// behavior for the request
					JobRestartBehavior failedJobRestartBehavior = RestartJobs(database, 
						ref connection, 
						ref curTrans, 
						requestKey, 
						newRequestKey,
						batchRestartBehavior);

					// copys parameters of the original request to the new request based on the restart behaviors
					ListDictionary requestParameterMappings = GetRequestParameterCategoryMappings(
						batchRestartBehavior,
						failedJobRestartBehavior);
					CopyParameters(database,
						ref connection,
						ref curTrans,
						requestKey,
						newRequestKey,
						requestParameterMappings);

					DefaultBatchManager.EndTransaction( curTrans, connection, true ); 
				}
				catch (Exception)
				{
					DefaultBatchManager.EndTransaction( curTrans, connection, false ); 
					throw;
				}	
			}

            /// <summary>
            /// Reenqueue jobs of the request.  Part of the request restart process.
            /// </summary>
            /// <param name="database">The Database instance that represents ACA.NET Batch Database.</param>
            /// <param name="connection">The database connection object obtained by invoking
            /// DefaultBatchManager.JoinTransaction( ).</param>
            /// <param name="transaction">The database transaction object obtained by invoking
            /// DefaultBatchManager.JoinTransaction( ).</param>
            /// <param name="batchQueueRequestKey">The key for the request.</param>
            /// <param name="newBatchQueueRequestKey">The key for the new request.</param>
            /// <param name="batchRestartBehavior">Avanade.ACA.Batch.BatchRestartBehavior</param>
            /// <returns>Avanade.ACA.Batch.JobRestartBehavior</returns>
			private static JobRestartBehavior RestartJobs(Database database, 
				ref IDbConnection connection,
				ref DbTransaction transaction,				
				Guid batchQueueRequestKey,
				Guid newBatchQueueRequestKey,
				BatchRestartBehavior batchRestartBehavior)
			{
				JobRestartBehavior lastFailedJobRestartBehavior;

				ListDictionary jobParameterCategoryMappings = new ListDictionary();

				DbCommand cw = database.GetStoredProcCommand( 
					BatchDbConstants.SP.REQUEST_JOBEXEC_LIST);	
				
				database.AddInParameter(cw, BatchDbConstants.Parm.Q_KEY, DbType.Guid, batchQueueRequestKey );

				DefaultBatchManager.JoinTransaction(cw, 
					database, 
					ref transaction, 
					ref connection);

				IDataReader reader = null;
				ArrayList commands = new ArrayList();

				ListDictionary origExecKeyMappings = new ListDictionary();

				try
				{			
					reader = ExecuteReader(database, cw, transaction);
					while (reader.Read())
					{	
						// The columns are: BatchJobExecKey, BatchQueueKey, BatchJobName,
						// BatchJobDesc, JobSeq, JobSmartRestart, BatchJobCmitFreq, JobTypeClass,
						// StartDate, JobStatusCode, LastCommitDate, WorkUnitCount,  
						// RestartCount, CommitCount, OrigJobKey, and LastExeStatus.
						Guid batchJobExecKey = Guid.NewGuid();
						Guid origBatchJobExecKey = reader.GetGuid(0);
						string jobName = reader.GetString(2);
						string jobDesc = String.Empty;
						if (!reader.IsDBNull(3))
						{
							jobDesc = reader.GetString(3);
						}
						int seqNo = reader.GetInt16(4);
						byte restartBehavior = reader.GetByte(5);

						int cmitFreq = reader.GetInt32(6);
						string typeClass = reader.GetString(7);
						// use the status code of the old job execution record as the lasExeStatus value
						byte lastExeStatus = reader.GetByte(9);

						int restartCount = reader.GetInt32(12);
						Guid origJobKey = reader.GetGuid(14);

						origExecKeyMappings.Add(batchJobExecKey, origBatchJobExecKey);

						DbCommand cwWrite = database.GetStoredProcCommand( 
							BatchDbConstants.SP.Q_ENQUEUE_JOB);

						database.AddInParameter(cwWrite, BatchDbConstants.Parm.Q_JOB_KEY, DbType.Guid, batchJobExecKey );
						database.AddInParameter(cwWrite, BatchDbConstants.Parm.Q_KEY, DbType.Guid, newBatchQueueRequestKey );
						database.AddInParameter(cwWrite, BatchDbConstants.Parm.JOB_NAME, DbType.String, jobName );
						database.AddInParameter(cwWrite, BatchDbConstants.Parm.JOB_DESC, DbType.String, jobDesc );
						database.AddInParameter(cwWrite, BatchDbConstants.Parm.Q_JOB_SEQ, DbType.Int32, seqNo );
						database.AddInParameter(cwWrite, BatchDbConstants.Parm.JOB_RESTART, DbType.Byte, restartBehavior );
						database.AddInParameter(cwWrite, BatchDbConstants.Parm.Q_JOB_RESATRT_COUNT, DbType.Int32, restartCount );
						database.AddInParameter(cwWrite, BatchDbConstants.Parm.JOB_COMMIT_FREQ, DbType.Int32, cmitFreq );
						database.AddInParameter(cwWrite, BatchDbConstants.Parm.Q_JOB_TYPE_CLASS, DbType.String, typeClass );
						database.AddInParameter(cwWrite, BatchDbConstants.Parm.Q_JOB_ORIG_JOB_KEY, DbType.Guid, origJobKey );
						database.AddInParameter(cwWrite, BatchDbConstants.Parm.Q_JOB_LAST_EXE_STATUS, DbType.Byte, lastExeStatus );
						
						commands.Add(cwWrite);
					}
				}
				finally
				{
					if( reader != null )
					{
						reader.Close();
					}
				}

				// find the last job that has status of new
				int lastFailedIndex = -1;
				lastFailedJobRestartBehavior = JobRestartBehavior.Smart;
				for (int i = commands.Count - 1; i >= 0; i--)
				{
					DbCommand cwTemp = (DbCommand)commands[i];
					Guid batchJobExecKey = (Guid)
                        database.GetParameterValue(cwTemp, BatchDbConstants.Parm.Q_JOB_KEY);
					byte lastCode = (byte)
                        database.GetParameterValue(cwTemp, BatchDbConstants.Parm.Q_JOB_LAST_EXE_STATUS);
					byte behavior = (byte)
						database.GetParameterValue(cwTemp, BatchDbConstants.Parm.JOB_RESTART);
					JobRestartBehavior restartBehavior = 
						behavior == 0 ? JobRestartBehavior.Full : JobRestartBehavior.Smart;
					// the last non-new job 
					StatusCode temp = StatusCode.New;
					if (lastCode != (byte)temp  && lastFailedIndex == -1)
					{
						lastFailedIndex = i;
						temp = StatusCode.Success;
						if (lastCode != (byte)temp)
						{
							lastFailedJobRestartBehavior = restartBehavior;
						}
						jobParameterCategoryMappings.Add(batchJobExecKey,
							GetJobParameterCategoryMappings(batchRestartBehavior,
								lastFailedJobRestartBehavior));
					}
					else
					{
						// treat the successful job as if they has settings of smart restarts
						// since if it's new, then modifies parameter set is equevilent to the
						// original one; if it's successful, we need to present the modifies
						// parameter set as well.
						jobParameterCategoryMappings.Add(batchJobExecKey,
							GetJobParameterCategoryMappings(BatchRestartBehavior.RestartAtFailedJob,
							JobRestartBehavior.Smart));
					}
				}

				foreach (DbCommand cwWrite in commands)
				{
					DefaultBatchManager.JoinTransaction(cwWrite, 
						database, 
						ref transaction, 
						ref connection);

					Guid batchJobExecKey = (Guid)
                        database.GetParameterValue(cwWrite, BatchDbConstants.Parm.Q_JOB_KEY);
					// create the job exection context
					ExecuteCommand(database, cwWrite, transaction);

					Guid origBatchJobExecKey = (Guid)origExecKeyMappings[batchJobExecKey];

					// copy the original jobExec parameters to jobExec parameters
					CopyParameters(database, 
						ref connection, 
						ref transaction, 
						origBatchJobExecKey,
						batchJobExecKey,
						(ListDictionary)jobParameterCategoryMappings[batchJobExecKey]);
				}
				return lastFailedJobRestartBehavior;
			}

            /// <summary>
            /// Check the status of a Request.
            /// </summary>
            /// <param name="database">The Database instance that represents ACA.NET Batch Database.</param>
            /// <param name="requestKey">The key for the request.</param>
            /// <param name="status">Output.  The status code of the request.</param>
            /// <param name="toPause">Output.  Whether the request is being asked to paused.</param>
            /// <returns>void</returns>
			public static void CheckStatus( Database database, 
				Guid requestKey, 
				out BatchProcessStatusCode status,
				out bool toPause)
			{
				DbCommand cw = database.GetStoredProcCommand( 
					BatchDbConstants.SP.REQUEST_CHECK_STATUS);
			
				database.AddInParameter(cw, BatchDbConstants.Parm.Q_KEY, DbType.Guid, requestKey );
				database.AddOutParameter(cw, BatchDbConstants.Parm.Q_STATUS_CODE, DbType.Byte, 1 );
				database.AddOutParameter(cw, BatchDbConstants.Parm.Q_TO_PAUSE, DbType.Boolean, 1 );
				ExecuteNonQueryTx(database, cw);

				object statusValue = database.GetParameterValue(cw, BatchDbConstants.Parm.Q_STATUS_CODE );
				object toPauseValue = database.GetParameterValue(cw, BatchDbConstants.Parm.Q_TO_PAUSE );
				
				if (statusValue is DBNull
					|| toPauseValue is DBNull)
				{
					throw new BatchRequestNotFoundException(
						string.Format(INVALID_REQUEST, requestKey.ToString()));
				}
				
				byte statusCode = (byte)statusValue;
				status = (BatchProcessStatusCode)statusCode;
			

				 try
                {
                    //toPause = (bool)toPauseValue;
                    toPause = Convert.ToBoolean(toPauseValue);
                }
                catch (Exception)
                {
                    throw new InvalidCastException("Unable to cast Byte to boolean");
                }
			}

            /// <summary>
            /// Creates a RequestCommitHandle and returns it to be used for committing the
            /// request and job execution context and status.
            /// </summary>
            /// <param name="database">The Database instance that represents ACA.NET Batch Database.</param>
            /// <param name="requestKey">The key of the request.</param>
            /// <returns>An RequestCommitHandle object.</returns>
			public static RequestCommitHandle CreateCommitStatusHandle( Database database, 
				Guid requestKey )
			{				
				return new RequestCommitHandle( database, requestKey );
			}

            /// <summary>
            /// Creates a RequestCommitHandle and returns it to be used for committing the
            /// request and job execution context and status. This overload accepts
            /// a transaction to be passed in and used by all of the database
            /// commands the </summary>
            /// <param name="database">The Database instance that represents ACA.NET Batch Database.</param>
            /// <param name="requestKey">The key of the request.</param>
            /// <param name="transaction">The transaction object the database commands will participate in.</param>
            /// <returns>Avanade.ACA.Batch.RequestCommitHandle</returns>
			public static RequestCommitHandle CreateCommitStatusHandle( Database database, 
				Guid requestKey, DbTransaction transaction)
			{				
				return new RequestCommitHandle(database, requestKey, transaction);
			}

            /// <summary>
            /// Asks the request to be paused or resumed.
            /// </summary>
            /// <param name="database">The Database instance that represents ACA.NET Batch Database.</param>
            /// <param name="requestKey">The key for the request.</param>
            /// <param name="toPause">A boolean value.  True for pausing, False for resuming.</param>
            /// <remarks>If the request has not been dequeued, its status code will be set to 
            /// either "Paused" or "Resumed" immediately.  Otherwise,
            /// the Batch Server will pause the request once it sense the pauing status
            /// has been changed.</remarks>
            /// <returns>void</returns>
			public static void SetPause( Database database, 
				Guid requestKey,
				bool toPause)
			{
				DbCommand cw = database.GetStoredProcCommand( 
					BatchDbConstants.SP.Q_SET_PAUSE);
			
				database.AddInParameter(cw, BatchDbConstants.Parm.Q_KEY, DbType.Guid, requestKey );
				database.AddInParameter(cw, BatchDbConstants.Parm.Q_TO_PAUSE, DbType.Boolean, toPause );
				DefaultBatchManager.ExecuteNonQueryTx(database, cw);
			}

            /// <summary>
            /// Lists all the state transition history of a request, in a decending order by time
            /// stamp.
            /// </summary>
            /// <param name="database">The Database instance that represents ACA.NET Batch Database.</param>
            /// <param name="batchQueueRequestKey">Key of the request.</param>
            /// <remarks>The columns in the order selected are::BatchQueueKey, DateModified, 
            /// StatusCode, and ToPause.
            /// <BR/>The calling code is responsible for calling <B>Close( )</B> on the returning
            /// IDataReader object or the cursor won't be closed.</remarks>
            /// <returns>An IDataReader object containing all the transition records.</returns>
			public static IDataReader ListTransitions( Database database, Guid batchQueueRequestKey )
			{
				DbCommand cw = database.GetStoredProcCommand( 
					BatchDbConstants.SP.REQUEST_TRANSITION_LIST);
			
				database.AddInParameter(cw, BatchDbConstants.Parm.Q_KEY, DbType.Guid, batchQueueRequestKey );
				return database.ExecuteReader( cw );
			}

            /// <summary>
            /// Provide Error Log Monitor
            /// </summary>
            /// <param name="database"></param>
            /// <param name="batchQueueRequestKey"></param>
            /// <returns></returns>
            public static IDataReader ListErrorLog(Database database, Guid batchQueueRequestKey)
            {
                DbCommand cw = database.GetStoredProcCommand(
                    BatchDbConstants.SP.REQUEST_ERROR_LOG);

                database.AddInParameter(cw, BatchDbConstants.Parm.Q_KEY, DbType.Guid, batchQueueRequestKey);
                return database.ExecuteReader(cw);
            }

            /// <summary>
            /// Moves all the expired request from the active queue to the archive.
            /// </summary>
            /// <param name="database">The Database instance that represents ACA.NET 
            /// Batch Database.</param>
            /// <remarks>When a request is enqueued, the absolute expiration date dictates
            /// when the request will become expired if it hasn't been dequeued.  An expired
            /// request stays expired in the active queue until it gets cancelled or
            /// archived.</remarks>
            /// <returns>void</returns>
			public static void ArchiveExpired( Database database )
			{
				// archieve the request
				DbCommand cw = database.GetStoredProcCommand( 
					BatchDbConstants.SP.Q_ARCHIVE_EXPIRED);
				DefaultBatchManager.ExecuteNonQueryTx(database, cw);
			}
		}

		/// <summary>
		/// ExecutionHistory class contains the static methods managing the archived requests
		/// in the ACA.NET Batch Database.
		/// </summary>
		public class ExecutionHistory
		{
            /// <summary>
            /// Deletes all data for one archived request from the ACA.NET Batch Database.
            /// </summary>
            /// <param name="database">The Database instance that represents ACA.NET Batch 
            /// Database.</param>
            /// <param name="batchQueueRequestKey">The key for the request to be
            /// deleted.</param>
            /// <returns>void</returns>
			public static void Clean( Database database, Guid batchQueueRequestKey )
			{
				// archieve the request
				DbCommand cw = database.GetStoredProcCommand( 
					BatchDbConstants.SP.HIST_REQ_CLEAN);
			
				database.AddInParameter(cw, BatchDbConstants.Parm.Q_KEY, DbType.Guid, batchQueueRequestKey );
				DefaultBatchManager.ExecuteNonQueryTx(database, cw);
			}

            /// <summary>
            /// Deletes all data for all the archived requests from the ACA.NET Batch Database.
            /// </summary>
            /// <param name="database">The Database instance that represents ACA.NET Batch 
            /// Database.</param>
            /// <returns>void</returns>
			public static void CleanAll( Database database )
			{
				// archieve the request
				DbCommand cw = database.GetStoredProcCommand( 
					BatchDbConstants.SP.HIST_REQ_CLEAN_ALL);
			
				DefaultBatchManager.ExecuteNonQueryTx(database, cw);
			}

            /// <summary>
            /// Deletes all data for all the archived requests that are enqueud prior to a
            /// certain date from the ACA.NET Batch Database.
            /// </summary>
            /// <param name="database">The Database instance that represents ACA.NET 
            /// Batch Database.</param>
            /// <param name="olderThanDate">A date to compare the enqueued date with.</param>
            /// <returns>void</returns>
			public static void CleanOlderThan( Database database, DateTime olderThanDate )
			{
				// archieve the request
				DbCommand cw = database.GetStoredProcCommand( 
					BatchDbConstants.SP.HIST_REQ_CLEAN_OLDER);
			
				database.AddInParameter(cw, BatchDbConstants.Parm.Q_ENTER_DATE, DbType.DateTime, olderThanDate );
				ExecuteNonQueryTx(database, cw);
			}
		}  
		// end of History

        /// <summary>
        /// This method is used by ACA.NET Batch Framework and not meant to be used by others.
        /// It opens a transaction and connection and associate it with a command writer.
        /// </summary>
        /// <param name="cw">A DbCommand object containing the database command
        /// that needs to run in a new transaction.</param>
        /// <param name="database">The Database instance that represents ACA.NET Batch Database.</param>
        /// <param name="transaction">Output.  A closed database transaction object.</param>
        /// <param name="connection">Output.  A closed database connection object.</param>
        /// <param name="isolationLevel">The transaction isolation level.</param>
        /// <returns>void</returns>
		public static void OpenTransaction(DbCommand cw, 
			Database database,
			ref DbTransaction transaction, 
			ref IDbConnection connection,
			IsolationLevel isolationLevel )
		{
			if (!System.EnterpriseServices.ContextUtil.IsInTransaction)
			{
				Type connectionType = database.CreateConnection().GetType();
				string connectionString = database.CreateConnection().ConnectionString;	//database.ConnectionString;

				connection = (IDbConnection)Activator.CreateInstance(connectionType);
				connection.ConnectionString = connectionString;
				connection.Open();

				// Start a local transaction
                transaction = (DbTransaction) connection.BeginTransaction(isolationLevel);
				cw.Transaction = transaction;                
				cw.Connection = (DbConnection) connection;
			}
		}

        /// <summary>ExecuteNonQueryTx</summary>
        /// <param name="database">Microsoft.Practices.EnterpriseLibrary.Data.Database</param>
        /// <param name="cw">System.Data.Common.DbCommand</param>
        /// <returns>void</returns>
		private static void ExecuteNonQueryTx
			(
			Database database,
			DbCommand cw
			)
		{
			if (ContextUtil.IsInTransaction)
			{
				database.ExecuteNonQuery(cw);
			}
			else
			{
				IDbConnection connection	= null;                
				DbTransaction transaction	= null;
				try
				{
                    
					connection = database.CreateConnection();
					connection.Open();                    
                    transaction = (DbTransaction) connection.BeginTransaction();
					database.ExecuteNonQuery(cw, transaction);
					transaction.Commit();
				}
				catch
				{
					transaction.Rollback();
					throw;
				}
				finally
				{
					if (connection != null)
					{
						connection.Close();
					}
					if (transaction != null)
					{
						transaction.Dispose();
					}
				}
			}
		}

        /// <summary>
        /// This method is used by ACA.NET Batch Framework and not meant to be used by others.
        /// Makes the database command participating in an opened transaction, or, if the
        /// connection is closed, opens a new transaction with "Serialization" 
        /// isolation level and have the database command participating in it.
        /// </summary>
        /// <param name="cw">AA DbCommand object containing the database command
        /// that needs to run in a new transaction.</param>
        /// <param name="database">The Database instance that represents ACA.NET Batch Database.</param>
        /// <param name="transaction">Output.  The database transaction object.  It will be
        /// assigned to a new value if the input value of the connection is null.</param>
        /// <param name="connection">Output.  The database connection object.  It will be
        /// assigned to a new connection if its input value is null.</param>
        /// <returns>void</returns>
		public static void JoinTransaction(DbCommand cw, 
			Database database,
			ref DbTransaction transaction, 
			ref IDbConnection connection )
		{
			if (connection == null)
			{
				OpenTransaction( cw,
					database,
					ref transaction,
					ref connection,
					IsolationLevel.Serializable );
			}
			else
			{
				cw.Transaction = transaction;                
				cw.Connection = (DbConnection) connection;
			}
		}

        /// <summary>
        /// Ends a transcation.
        /// This method is used by ACA.NET Batch Framework and not meant to be used by others.
        /// </summary>
        /// <param name="transaction">Output.  The database transaction object.  It will be
        /// assigned to a new value if the input value of the connection is null.</param>
        /// <param name="connection">Output.  The database connection object.  It will be
        /// assigned to a new connection if its input value is null.</param>
        /// <param name="succeeded">True if the transaction is to be committed.  False if
        /// it is to be rolled back.</param>
        /// <returns>void</returns>
		public static void EndTransaction( DbTransaction transaction, 
			IDbConnection connection, 
			bool succeeded )
		{
			try
			{
				if (transaction != null)
				{
					if (succeeded)
					{
						transaction.Commit();
					}
					else
					{
						try
						{
							transaction.Rollback();
						}
						catch
						{
							// It's a work-around to the known Microsoft problem, as the 
							// transaction has already been rolled back in the database.  
							// Just catch the exception here and ignore it.
						}
					}
					transaction.Dispose();
					transaction = null;
				}
			}
			finally
			{
				if (connection != null)
				{
					connection.Close();
				}
				connection = null;
			}	
		}

        /// <summary>
        /// This method gets around the fact the EnterpriseLibrary Data Block does explicit null
        /// checking on the transaction value in ExecuteNonQuery method with a transaction.
        /// </summary>
        /// <param name="db">Microsoft.Practices.EnterpriseLibrary.Data.Database</param>
        /// <param name="command">System.Data.Common.DbCommand</param>
        /// <param name="transaction">System.Data.Common.DbTransaction</param>
        /// <returns>void</returns>
		internal static void ExecuteCommand(Database db, DbCommand command, DbTransaction transaction)
		{
			if (transaction == null)
			{
				db.ExecuteNonQuery(command); 
			}
			else
			{
				db.ExecuteNonQuery(command, transaction);
			}
		}

        /// <summary>ExecuteReader</summary>
        /// <param name="db">Microsoft.Practices.EnterpriseLibrary.Data.Database</param>
        /// <param name="command">System.Data.Common.DbCommand</param>
        /// <param name="transaction">System.Data.Common.DbTransaction</param>
        /// <returns>System.Data.IDataReader</returns>
        internal static IDataReader ExecuteReader(Database db, DbCommand command, DbTransaction transaction)
		{
			if (transaction == null)
			{
				return db.ExecuteReader(command); 
			}
			else
			{
				return db.ExecuteReader(command, transaction);
			}
        }

        #region Active/Standby False Tolerance

        public static bool GetBatchQueue(Database db, DbTransaction trans,
         string vDestFilter,
         out Guid batchQueueKey,
         out string batchName)
        {
            try
            {
                DbCommand cmd = db.GetStoredProcCommand(BatchDbConstants.SP.EXEC_SERV_GET_BATCH_QUEUE_KEY);
                db.AddInParameter(cmd, BatchDbConstants.Parm.EXEC_SERV_DESTINATION_FILTER, DbType.String, vDestFilter);
                DataSet ds = db.ExecuteDataSet(cmd, trans);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    batchName = (string)ds.Tables[0].Rows[0][BatchDbConstants.Parm.EXEC_SERV_BATCH_NAME];
                    batchQueueKey = (Guid)ds.Tables[0].Rows[0][BatchDbConstants.Parm.EXEC_SERV_BATCH_QUEUE_KEY];
                    return true;
                }
                else
                {
                    batchName = String.Empty;
                    batchQueueKey = Guid.Empty;
                    return false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }



        public static bool GetExecServer(Database db, DbTransaction trans,
            string batchName,
            out int execServerId,
            out string execServerSystemName,
            out Guid batchQueueKey,
            out DateTime executedDt)
        {
            try
            {
                DbCommand cmd = db.GetStoredProcCommand(BatchDbConstants.SP.EXEC_SERV_GET_SERVER);
                db.AddInParameter(cmd, BatchDbConstants.Parm.EXEC_SERV_BATCH_NAME, DbType.String, batchName);
                DataSet ds = db.ExecuteDataSet(cmd, trans);
                if (ds.Tables[0].Rows.Count > 0)
                {

                    execServerId = (int)ds.Tables[0].Rows[0][BatchDbConstants.Parm.EXEC_SERV_ID];
                    execServerSystemName = (string)ds.Tables[0].Rows[0][BatchDbConstants.Parm.EXEC_SERV_EXECUTION_SERVER_NAME];
                    batchQueueKey = (Guid)ds.Tables[0].Rows[0][BatchDbConstants.Parm.EXEC_SERV_BATCH_QUEUE_KEY];
                    executedDt = (DateTime)ds.Tables[0].Rows[0][BatchDbConstants.Parm.EXEC_SERV_EXECUTION_DATE_TIME];
                    return true;
                }
                else
                {
                    execServerId = 0;
                    execServerSystemName = String.Empty;
                    batchQueueKey = Guid.Empty;
                    executedDt = DateTime.MinValue;
                    return false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static DataTable GetExecServerSync(Database db, DbTransaction trans,
           int execServerId)
        {
            try
            {
                DbCommand cmd = db.GetStoredProcCommand(BatchDbConstants.SP.EXEC_SERV_GET_SERVER_SYNC);
                db.AddInParameter(cmd, BatchDbConstants.Parm.EXEC_SERV_ID, DbType.Int32, execServerId);
                DataSet ds = db.ExecuteDataSet(cmd, trans);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0];
                }
                else
                {
                    return new DataTable();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static int GetExecServerSystemPriority(Database db, DbTransaction trans,
          string execServerSystemName)
        {
            try
            {
                DbCommand cmd = db.GetStoredProcCommand(BatchDbConstants.SP.EXEC_SERV_GET_SERVER_PRIORITY);
                db.AddInParameter(cmd, BatchDbConstants.Parm.EXEC_SERV_EXECUTION_SERVER_NAME, DbType.String, execServerSystemName);
                return (int)db.ExecuteScalar(cmd, trans);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static bool GetExecServerConfig(Database db, DbTransaction trans,
            out bool enableFalseTolerance,
            out bool enablePriorityMode,
            out bool enableProgressLog,
            out int syncCycleLatency,
            out int timeOut,
            out int numberOfServer)
        {
            try
            {
                DbCommand cmd = db.GetStoredProcCommand(BatchDbConstants.SP.EXEC_SERV_GET_SERVER_CONFIG);
                DataSet ds = db.ExecuteDataSet(cmd, trans);

                if (ds.Tables[0].Rows.Count > 0)
                {

                    enableFalseTolerance = (bool)ds.Tables[0].Rows[0][BatchDbConstants.Parm.EXEC_SERV_ENABLE_FALSETOLERANCE];
                    enablePriorityMode = (bool)ds.Tables[0].Rows[0][BatchDbConstants.Parm.EXEC_SERV_ENABLE_PRIORITY];
                    enableProgressLog = (bool)ds.Tables[0].Rows[0][BatchDbConstants.Parm.EXEC_SERV_ENABLE_PROGRESSLOG];
                    syncCycleLatency = (int)ds.Tables[0].Rows[0][BatchDbConstants.Parm.EXEC_SERV_SYNC_INTERVAL];
                    timeOut = (int)ds.Tables[0].Rows[0][BatchDbConstants.Parm.EXEC_SERV_TIMEOUT];
                    numberOfServer = (int)ds.Tables[0].Rows[0][BatchDbConstants.Parm.EXEC_SERV_NUM_OF_SERVER];
                    return true;
                }
                else
                {
                    enableFalseTolerance = false;
                    enablePriorityMode = false;
                    enableProgressLog = false;
                    syncCycleLatency = 0;
                    timeOut = 0;
                    numberOfServer = 1;
                    return false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void InsertExecServer(Database db, DbTransaction trans,
           string execServerSystemName,
           string batchName,
            Guid batchQueueKey)
        {
            try
            {
                DbCommand cmd = db.GetStoredProcCommand(BatchDbConstants.SP.EXEC_SERV_ADD_SERVER);

                db.AddInParameter(cmd, BatchDbConstants.Parm.EXEC_SERV_BATCH_NAME, DbType.String, batchName);
                db.AddInParameter(cmd, BatchDbConstants.Parm.EXEC_SERV_EXECUTION_SERVER_NAME, DbType.String, execServerSystemName);
                db.AddInParameter(cmd, BatchDbConstants.Parm.EXEC_SERV_BATCH_QUEUE_KEY, DbType.Guid, batchQueueKey);
                ExecuteCommand(db, cmd, trans);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void InsertExecServerSystem(Database db, DbTransaction trans,
            string execServerSystemName)
        {
            try
            {
                DbCommand cmd = db.GetStoredProcCommand(BatchDbConstants.SP.EXEC_SERV_ADD_SERVER_SYSTEM);
                db.AddInParameter(cmd, BatchDbConstants.Parm.EXEC_SERV_EXECUTION_SERVER_NAME, DbType.String, execServerSystemName);
                ExecuteCommand(db, cmd, trans);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void InsertExecLog(Database db,
             string execServerSystemName,
             string batchName,
             string message)
        {
            try
            {
                using (DbConnection conn = db.CreateConnection())
                {
                    conn.Open();
                    DbTransaction trans = conn.BeginTransaction();
                    DbCommand cmd = db.GetStoredProcCommand(BatchDbConstants.SP.EXEC_SERV_ADD_LOG);

                    db.AddInParameter(cmd, BatchDbConstants.Parm.EXEC_SERV_BATCH_NAME, DbType.String, batchName);
                    db.AddInParameter(cmd, BatchDbConstants.Parm.EXEC_SERV_EXECUTION_SERVER_NAME, DbType.String, execServerSystemName);
                    db.AddInParameter(cmd, BatchDbConstants.Parm.EXEC_SERV_MESSAGE_LOG, DbType.String, message);
                    ExecuteCommand(db, cmd, trans);
                    trans.Commit();
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void UpdateExecServer(Database db, DbTransaction trans,
            int execServerId,
            string execServerSystemName,
            Guid batchQueueKey)
        {
            try
            {
                DbCommand cmd = db.GetStoredProcCommand(BatchDbConstants.SP.EXEC_SERV_UPDATE_SERVER);

                db.AddInParameter(cmd, BatchDbConstants.Parm.EXEC_SERV_ID, DbType.Int32, execServerId);
                db.AddInParameter(cmd, BatchDbConstants.Parm.EXEC_SERV_EXECUTION_SERVER_NAME, DbType.String, execServerSystemName);
                db.AddInParameter(cmd, BatchDbConstants.Parm.EXEC_SERV_BATCH_QUEUE_KEY, DbType.Guid, batchQueueKey);
                ExecuteCommand(db, cmd, trans);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void UpdateExecServerSync(Database db, DbTransaction trans,
           int execServerId,
           string execServerSystemName,
           bool active)
        {
            try
            {
                DbCommand cmd = db.GetStoredProcCommand(BatchDbConstants.SP.EXEC_SERV_UPDATE_SERVER_SYNC);

                db.AddInParameter(cmd, BatchDbConstants.Parm.EXEC_SERV_ID, DbType.Int32, execServerId);
                db.AddInParameter(cmd, BatchDbConstants.Parm.EXEC_SERV_EXECUTION_SERVER_NAME, DbType.String, execServerSystemName);
                db.AddInParameter(cmd, BatchDbConstants.Parm.EXEC_SERV_ACTIVE, DbType.Boolean, active);
                ExecuteCommand(db, cmd, trans);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void ClearExecServerSync(Database db, DbTransaction trans,
          int execServerId)
        {
            try
            {
                DbCommand cmd = db.GetStoredProcCommand(BatchDbConstants.SP.EXEC_SERV_CLEAR_SERVER_SYNC_STATUS);

                db.AddInParameter(cmd, BatchDbConstants.Parm.EXEC_SERV_ID, DbType.Int32, execServerId);
                ExecuteCommand(db, cmd, trans);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion Active/Standby False Tolerance
    }
}
