using System;
using System.Collections;
using System.Collections.Specialized;
using System.Reflection;
using System.ComponentModel;

using Avanade.ACA.Batch;
using Avanade.ACA.Batch.Configuration;
using Avanade.ACA.Batch.Configuration.IO;

namespace Avanade.ACA.Batch.BatchMonitor
{
	/// <summary>
	/// Summary description for NodePropertyMappings.
	/// </summary>
	public class NodePropertyMappings
	{
		private static ListDictionary _mappings;
		static NodePropertyMappings()
		{
			_mappings = new ListDictionary();
			string[] RequestProperties = new string[]	{
							"BatchName",
							"QueuedDate",
							"Description", 
							"BatchStatus",
							"ToPause",
							"StartDate",
							"IsARestart",
							"HasBeenRestarted",
							"BatchClientName",
							"BatchServerName",
							"LastUpdateDate",
							"AbsoluteExpirationDate",
							"QueuePriorityLevel",
							"Destination",
							"ConfigurationFilePath",
							"ExecutionPriorityLevel",
							"RelativeExpirationDate",
							"RestartBehavior" 
			};

			string[] JobExecProperties = new string[] {
							"JobName", 
						    "Description",
							"Sequence",
							"Status",
							"StartDate",
							"JobType",
							"RestartBehavior",
							"CommitCount",
							"RestartCount",
							"WorkUnitCount",
							"RestartBehavior",
							"CommitFrequency",
			};

			string[] ParameterProperties = new string[]	{
							"DisplayName",
							"Value",
							"ValueType"	
			};

			string[] ExceptionProperties = new string[]	{
							"ExceptionType",
							"Message",
							"Source",
							"Target",
							"StackTrace" 
			};

			string[] BatchProperties = new string[] {
							"DisplayName", 
							"Description",
							"BatchType",
							"Destination",
							"RestartBehavior",
							"ExecutionPriorityLevel",
							"QueuePriorityLevel",
							"ConfigurationFilePath",
							"RelativeExpirationDate"
			};

			string[] JobProperties = new string[] {
							"DisplayName", 
							"Description",
							"JobType",
							"RestartBehavior",
							"CommitFrequency"
			};

			string[] JobRefProperties = new String[] {
							"DisplayName",
							"SequenceNum"
			};

			string[] TypeProperties = new string[] {
							"DisplayName",
							"Description",
							"TypeName"
			};

			string[] DestProperties = new string[] {
						   "DisplayName",
						   "Description"
			};

			string[] FileParameterProperties = new string[] {
							"FullPath"
				};

			string[] FileSetParameterProperties = new string[] {
							"FullPath",
							"SearchPattern"
				};

			string[] XmlFileParameterProperties = new string[] {
							"FullPath",
							"ColumnXPaths",
							"RecordsXPath"
				};

			string[] FixedWidthFileParameterProperties = new string[] {
							"FullPath",
							"ColumnWidths"
				};
			string[] CharSeparatedFileParameterProperties = new string[] {
							"FullPath",
							"Separator"
				};
			
			_mappings.Add(typeof(BatchTypeData), TypeProperties);
			_mappings.Add(typeof(JobTypeData), TypeProperties);
			_mappings.Add(typeof(DestinationData), DestProperties);
			_mappings.Add(typeof(ParameterData), ParameterProperties);
			_mappings.Add(typeof(BatchExecutionContextData), RequestProperties);
			_mappings.Add(typeof(JobExecutionContextData), JobExecProperties);
			_mappings.Add(typeof(ExceptionData), ExceptionProperties);
			_mappings.Add(typeof(BatchDefinitionData), BatchProperties);
			_mappings.Add(typeof(JobDefinitionData), JobProperties);
			_mappings.Add(typeof(JobReferenceData), JobRefProperties);
			_mappings.Add(typeof(FileParameterData), FileParameterProperties);
			_mappings.Add(typeof(FileSetParameterData), FileSetParameterProperties);
			_mappings.Add(typeof(XmlFileParameterData), XmlFileParameterProperties);
			_mappings.Add(typeof(FixedWidthFileParameterData), FixedWidthFileParameterProperties);
			_mappings.Add(typeof(CharacterSeparatedFileParameterData), CharSeparatedFileParameterProperties);
		}

		public static ArrayList GetProperties(object obj)
		{
			string[] properties = _mappings[obj.GetType()] as string[];
			ArrayList combinedProperties = new ArrayList();
			if (properties != null)
			{
				combinedProperties.AddRange(properties);
			}
			Type nodeType = obj.GetType(); 

			foreach (Type t in _mappings.Keys)
			{
				if (t == nodeType || nodeType.IsSubclassOf(t))
				{
					string[] ps = _mappings[t] as string[];
					
					foreach (string s in ps)
					{
						if (!combinedProperties.Contains(s))
						{
							combinedProperties.Add(s);
						}
						break;
					}
					break;
				}
			}
			if (combinedProperties.Count == 0)
			{
				IBatchDBData node = obj as IBatchDBData;
				if (node != null && node.Key != Guid.Empty)
				{
					Type t = obj.GetType();
					PropertyInfo[] propertiyInfos = t.GetProperties();
					if (propertiyInfos.Length == 0)
					{
						return null;
					}

					foreach (PropertyInfo propertyInfo in propertiyInfos)
					{
						BrowsableAttribute[] browsableAttributes = 
							(BrowsableAttribute[])
							propertyInfo.GetCustomAttributes(typeof(BrowsableAttribute), true);
						if (browsableAttributes.Length > 0)
						{
							BrowsableAttribute browsableAttribute = browsableAttributes[0];
							if (browsableAttribute.Browsable == false)
							{
								continue;
							}
						}
						combinedProperties.Add(propertyInfo.Name);
					}
				}
			}
			return combinedProperties;
		}
	}
}
