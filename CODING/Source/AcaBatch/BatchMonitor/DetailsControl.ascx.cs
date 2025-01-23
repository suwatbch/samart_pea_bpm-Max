namespace Avanade.ACA.Batch.BatchMonitor
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Web.Caching;
	using System.Reflection;
	using System.ComponentModel;
	using System.Collections;
	using System.Collections.Specialized;
	using System.Diagnostics;

	using Avanade.ACA.Batch;
	using Avanade.ACA.Batch.Configuration;
	using Microsoft.Practices.EnterpriseLibrary.Data;

	/// <summary>
	///	DetailsControl uses its ViewState["RequestKey"] to stored the requestKey,
	///	it the configuration node is a BatchExecutionContextData.  Otherwise, the
	///	ViewState["RequestKey"] is set to empty;
	/// </summary>
	public partial  class DetailsControl : System.Web.UI.UserControl
	{

		public enum LinkStyle : byte
		{
			Default,
			//NoNewWindowLink,
			NoHistoryLink
		}

		private LinkStyle _linkStyle;

		//private IBatchDBData _configNode;
		private IBatchDBData _configNode;
		private ArrayList _properties;

		public const string MODE_HISTORY = "history";
		public const string MODE_CHILD = "child";
        public const string MODE_ERRORLOG = "errorlog";

		protected void Page_Load(object sender, System.EventArgs e)
		{		
		}

		private Guid GetPresetRequestKey()
		{
			Guid requestKey;
			try
			{
				string keyString = (string)ViewState["RequestKey"];
				requestKey = new Guid(keyString);
				return requestKey;
			}
			catch
			{
				return Guid.Empty;
			}
		}

		private void SetPresetRequestKey( Guid key )
		{
			ViewState["RequestKey"] = key.ToString();
		}

		public LinkStyle Style
		{
			set
			{
				_linkStyle = value;
			}
		}

		public IBatchDBData ConfigurationNode
		{
			get 
			{
				return _configNode;
			}
			set
			{
                
				LabelObjectType.Text = String.Empty;//"Please select an item from the list.";
				if( value == null ) // Need to make sure that nothing is displayed on the control
				{
					_configNode = value;
					// clean up the action toolbar
					UpdateToolbar( null );
					// populate datagrid with "empty" table
					DataTable table = new DataTable();
					DataColumn col1 = new DataColumn("Property", typeof(string));
					DataColumn col2 = new DataColumn("Value", typeof(string));
					table.Columns.Add(col1);
					table.Columns.Add(col2);
					DataRow row = table.NewRow();
					row[0] = "";
					row[1] = "No Property";
					table.Rows.Add(row);
					DataGrid1.DataSource = table;
					DataGrid1.DataBind();
					// clean up the links
					Links.Text = "";
				}

				if (_configNode != value)
				{
					_configNode = value;

					if (_configNode is BatchExecutionContextData)
					{
						SetPresetRequestKey( _configNode.Key );
					}
					_properties = NodePropertyMappings.GetProperties(
						_configNode);
					UpdateControl();
				}
			}
		}

		private void FormatSegment(string textIn, ref string textOut, ref int index)
		{
			char[] Whitespaces = new char[] { ' ', '\t', '\n' };
			const string LINE_BREAKER = "<FONT color=\"silver\"><B>...</B></FONT><BR>";
			const int START_BREAKING_INDEX = 40;
			if (index >= textIn.Length)
			{
				index = -1;
				return;
			}
			if (textIn.Length <= index + START_BREAKING_INDEX)
			{
				textOut = System.Web.HttpUtility.HtmlEncode( textIn.Substring(index) );
				index = -1;
				return;
			}
			string text = textIn.Substring(index, START_BREAKING_INDEX);
			// search from the previous 10 character
			int firstSpace = text.IndexOfAny( Whitespaces, START_BREAKING_INDEX - 10 ); 
			
			//			if ( firstSpace < 0)
			//			{
			//				firstSpace = text.IndexOfAny( Whitespaces, START_BREAKING_INDEX - 15 ); // search from the 35th character
			//			}
			if (firstSpace >= 0)
			{
				// insert a line break before the whitespace
				textOut = System.Web.HttpUtility.HtmlEncode( text.Substring(0, firstSpace) );
				textOut += LINE_BREAKER;
				index += firstSpace + 1;
			}
			else
			{
				// insert a line break after the 50th character
				textOut =  System.Web.HttpUtility.HtmlEncode( text );
				textOut += LINE_BREAKER;
				index += START_BREAKING_INDEX;
			}
		}

		private string FormatString(object obj)
		{
			const string NOT_SET = "Not Set";
			if (obj == null)
			{
				return NOT_SET;
			}
            //if (obj is DateTime)
            //{
            //    if ((DateTime)obj == DateTime.Parse("1/1/0001 12:00:00 AM"))
            //    {
            //        return NOT_SET;
            //    }
            //}
			
			string textSrc = obj.ToString();
			string textOut = string.Empty;
			int index = 0;
			while (index >= 0)
			{
				string textSeg = string.Empty;
				FormatSegment(textSrc, ref textSeg, ref index);
				textOut += textSeg;
			}
			return textOut;
		}

		private void UpdateControl()
		{
			Type currentObjType = _configNode.GetType();
			DataTable table = new DataTable();
			DataColumn col1 = new DataColumn("Property", typeof(string));
			DataColumn col2 = new DataColumn("Value", typeof(string));

			table.Columns.Add(col1);
			table.Columns.Add(col2);

			UpdateToolbar( null );
			if (_properties.Count == 0)
			{
				DataRow row = table.NewRow();
				row[0] = "";
				row[1] = "No Property";
				table.Rows.Add(row);
			}
			else
			{
				foreach (string key in _properties)
				{
					if (key == null)
					{
						break;
					}
					DataRow row = table.NewRow();
					row[0] = key;

					PropertyInfo pInfo = currentObjType.GetProperty(key);
					if (pInfo == null)
					{
						row[1] = "No Property";
					}
					else
					{
						object obj = pInfo.GetValue(_configNode, null);
						row[1] = FormatString(obj);
					}
					table.Rows.Add(row);
				}
			}

			DataGrid1.DataSource = table;
			DataGrid1.DataBind();

			if (_configNode is BatchExecutionContextData)
			{
				UpdateRequestSpecificControls();
			}
			else if (_configNode is BatchDefinitionData)
			{
				UpdateBatchSpecificControls();
			}
			else
			{
				// otherwise, need to remove links
				Links.Text = "";
			}
			Guid objectKey = ((IBatchDBData)_configNode).Key;
			if (objectKey != Guid.Empty)
			{
                FieldInfo fieldInfo = (FieldInfo)currentObjType.GetField("LabelLiteral",
                    BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
				if (fieldInfo != null)
				{
					LabelObjectType.Text = (string)fieldInfo.GetValue(_configNode);
				}
			}
		}

		private void UpdateRequestSpecificControls()
		{
			BatchExecutionContextData contextNode = (BatchExecutionContextData)_configNode;

			UpdateToolbar( contextNode );  // if there is a next exec key, it has already been restarted.

			string templateTransition = "<A href=\"javascript:js_PopWin('transitions.aspx?request={0}', 'Transitions{1}');\">Transitions...</A>&nbsp;&nbsp;\n";
			string templateHistory = "<A href=\"javascript:js_PopWin('history.aspx?request={0}&mode={1}', 'History{2}');\">Detailed History...</A>&nbsp;&nbsp;\n";
            string batchLog = "<A href=\"javascript:js_PopWin('errorlog.aspx?request={0}&mode={1}', 'BatchLog{2}');\">Batch Log...</A>&nbsp;&nbsp;\n";
			string templateChild = "<A href=\"javascript:js_PopWin('history.aspx?request={0}&mode={1}', 'Child{2}');\">Load Balancing Details...</A>&nbsp;&nbsp;\n";
			string text = "";
			
			string s = contextNode.Key.ToString();
			s = s.Replace("-", "");
			text += string.Format(templateTransition, 
				contextNode.Key.ToString(), s);

			if (_linkStyle != LinkStyle.NoHistoryLink)
			{
				text += string.Format(templateHistory,
					contextNode.Key.ToString(), MODE_HISTORY, s);
			}

            text += string.Format(batchLog, contextNode.Key.ToString(), MODE_ERRORLOG, s);
                

			if (contextNode.ChildRequestCount > 0)
			{
				text += string.Format(templateChild,
					contextNode.Key.ToString(), MODE_CHILD, s);
			}
			if (contextNode.ParentRequestKey != Guid.Empty)
			{
				s = contextNode.ParentRequestKey.ToString();
				s = s.Replace("-", ""); 
				text += string.Format(templateChild,
					contextNode.ParentRequestKey.ToString(), MODE_CHILD, s);
			}
			Links.Text = text;
		}

		private void UpdateBatchSpecificControls()
		{
			BatchDefinitionData BatchDefinitionData = (BatchDefinitionData)_configNode;

			string templateEnqueue = "<A href=\"javascript:js_PopWin('enqueue.aspx?batch={0}', 'Enqueue');\">Enqueue...</A>&nbsp;&nbsp;\n";
			
			Links.Text = string.Format(templateEnqueue, 
				HttpUtility.HtmlEncode(BatchDefinitionData.DisplayName));
		}

		private void UpdateToolbar( BatchExecutionContextData contextNode)
		{
			btnPause.Visible = false;
			btnStop.Visible = false;
			btnRun.Visible = false;
			btnRestart.Visible = false;

			if (contextNode == null)
			{
				return;
			}
			string statusStr = contextNode.BatchStatus.ToString(); 
			bool toPause = contextNode.ToPause; 
			bool restarted = contextNode.HasBeenRestarted;

			const string templateRestart = 
					  "javascript:js_PopWin('enqueue.aspx?request={0}&batch={1}', 'RestartRequest');";

			if (statusStr == string.Empty)
			{
				return;
			}
			BatchProcessStatusCode status = (BatchProcessStatusCode)
				Enum.Parse(typeof(BatchProcessStatusCode), statusStr);

			switch (status)
			{
				case BatchProcessStatusCode.Executing:
				case BatchProcessStatusCode.Paused:
				case BatchProcessStatusCode.Dequeued:
				case BatchProcessStatusCode.Enqueued:
				case BatchProcessStatusCode.EnqueuedRestart:
					btnStop.Visible = true;
					if (toPause == false)
					{
						btnPause.Visible = true;
					}
					else
					{
						btnRun.Visible = true;
					}
					break;
				case BatchProcessStatusCode.Failed:
				case BatchProcessStatusCode.FailedTimedOut:
				case BatchProcessStatusCode.FailedCanceled:
					if (!restarted)
					{
						btnRestart.Visible = true;
						btnRestart.NavigateUrl = string.Format(templateRestart,
							contextNode.Key.ToString().Replace("-", ""),
							HttpUtility.HtmlEncode(contextNode.BatchName));
					}
					break;
			}
		}

		public string Headings
		{
			get
			{
				if (_configNode != null)
				{
					return "Request: <EM>" + _configNode.DisplayName + "</EM>";
				}
				else
				{
					return "";
				}
			}
		}


		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		private void ShowErrorMessage(string msg, bool bShow)
		{
			if (bShow == true)
			{
				Label1.Text = "<br>" + msg;
				Label1.Visible = true;
			}
			else
			{
				Label1.Visible = false;
			}
		}

		private void RefreshRequestProperty( Guid requestKey )
		{
			if (requestKey == Guid.Empty)
			{
				ShowErrorMessage("Please first select a node.", true);
				return;
			}

			IRequestListControl requestListCtrl = (IRequestListControl)Parent;
			requestListCtrl.Refresh( requestKey );
		}

		#region Static Methods
		public static int GetRestartCount(Guid requestKey)
		{
			if (requestKey == Guid.Empty)
			{
				return 0;
			}
			int i = 0;

			Database db = BatchExecutionData.Current.DatabaseInstance;//BatchExecutionData.Current//..DatabaseInstance;
			IDataReader reader = null;
			try
			{
				reader = DefaultBatchManager.RequestQueue.ListRestarts(db, requestKey);
				while (reader.Read())
				{
					Guid key = reader.GetGuid(0);
					if (requestKey == key)
					{
						i = 1;
					}
					else if (i > 0)
					{
						i++;
					}
				}
			}
			catch
			{
				// don't want to expose the exception here.
			}
			finally
			{
				if (reader != null)
				{
					reader.Close();
				}
			}
			return i;
		}

		private const string SUCCESS_CANCEL = "Request has been cancelled.";
		private const string FAILURE_CANCEL = "Failed to cancel request '{0}'.";
		public void CancelRequest(Guid key)
		{
			if (key == Guid.Empty)
			{
				return;
			}
			Database database = BatchExecutionData.Current.DatabaseInstance;
				
			RequestCommitHandle handle = 
				DefaultBatchManager.RequestQueue.CreateCommitStatusHandle(
				database, key);
			try
			{
				handle.CommitStatus(BatchProcessStatusCode.FailedCanceled);
				handle.EndCommit(true);
				// archive the request for restart
				DefaultBatchManager.RequestQueue.Delete(database, key);

				ShowErrorMessage(SUCCESS_CANCEL, true);
				RefreshRequestProperty( key );
			}
			catch (Exception exp)
			{
				handle.EndCommit(false);
				string msg = string.Format(FAILURE_CANCEL,
					key.ToString());
				Log(msg, exp);
				ShowErrorMessage(msg, true);
			}
		}

		private const string SUCCESS_PAUSE_RESUME = "Request is preparing to {0}.";
		private const string FAILURE_PAUSE_RESUME = "Failed to {0} request '{1}'.";
		public void SetPause(Guid key, bool toPause)
		{
			if (key == Guid.Empty)
			{
				return;
			}
			string actionStr = toPause ? "pause" : "resume";
			Database db = BatchExecutionData.Current.DatabaseInstance;
			
			try
			{
				DefaultBatchManager.RequestQueue.SetPause(db, key, toPause);

				ShowErrorMessage(string.Format(
						SUCCESS_PAUSE_RESUME, actionStr), true);
				RefreshRequestProperty( key );
			}
			catch (Exception exp)
			{
				string msg = string.Format(FAILURE_PAUSE_RESUME,
					actionStr,
					key.ToString()
					);
				Log(msg, exp);
				ShowErrorMessage(msg, true);
			}
		}

		private const string SUCCESS_RESTART = "Request has been restarted.";
		private const string FAILURE_RESTART = "Failed to restart request '{0}'.";

		public void RestartRequest(Guid key,
			object queuePriorityLevel,
			DateTime absExpDate,
			object batchDestName)
		{
			if (key == Guid.Empty)
			{
				throw new ACABatchException("Invalid request key.");
			}
			Guid newRequestKey = Guid.NewGuid();
			Database db = BatchExecutionData.Current.DatabaseInstance;
			try
			{
				DefaultBatchManager.RequestQueue.RestartFromFailure(
					db, 
					key, 
					newRequestKey,
					queuePriorityLevel,
					absExpDate,
					batchDestName);
				ShowErrorMessage(SUCCESS_RESTART, true);
				RefreshRequestProperty( key );
			}
			catch (Exception exp)
			{
				string msg = string.Format(FAILURE_RESTART,
					key.ToString());
				Log(msg, exp);
				ShowErrorMessage(msg, true);
			}
		}

		private const string ACA_BATCH_MONITOR  = "ACA.NET Batch Monitor 4.0";
		private const string LINE_SEPARATOR		= "\n";

		public static void Log(string customMsg,
			Exception exp)
		{
			if (HttpContext.Current.Trace.IsEnabled)
			{
				HttpContext.Current.Trace.Warn(ACA_BATCH_MONITOR, customMsg, exp);
			}
			else
			{
				string message = customMsg;

				while (exp != null)
				{
					message += LINE_SEPARATOR;
					message += exp.ToString();
					exp = exp.InnerException;
				}
				System.Diagnostics.Debug.WriteLine(message);
			}
		}
		#endregion

		protected void btnPause_Click(object sender, System.EventArgs e)
		{
			Guid requestKey = GetPresetRequestKey();
			if (requestKey != Guid.Empty)
			{
				SetPause(requestKey, true);
			}	
		}

		protected void btnRun_Click(object sender, System.EventArgs e)
		{
			Guid requestKey = GetPresetRequestKey();
			if (requestKey != Guid.Empty)
			{
				SetPause(requestKey, false);
			}		
		}

		protected void btnStop_Click(object sender, System.EventArgs e)
		{
			Guid requestKey = GetPresetRequestKey();
			if (requestKey != Guid.Empty)
			{
				CancelRequest(requestKey);
			}	
		}
	}
}
