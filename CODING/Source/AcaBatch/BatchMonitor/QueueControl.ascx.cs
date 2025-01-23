using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections.Specialized;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Avanade.ACA.Batch;
using System.Globalization;
using System.Security.Cryptography;


namespace Avanade.ACA.Batch.BatchMonitor
{

	/// <summary>
	///		Summary description for QueueControl.
	/// </summary>
	public partial  class QueueControl : System.Web.UI.UserControl, IRequestListControl
	{


		protected System.Web.UI.WebControls.PlaceHolder JavaScriptInit;

		public const string FAILURE_DATABASE = "Failed to open database.  Please validate the Database instance of Batch Execution configuration and try again.";
		public const string FAILURE_GET_RESULTS = "Failed to get results from Batch Database.";
		public const string FAILURE_FIND_REQUEST = "Cannot find request, '{0}'.";

		public class StatusEntry
		{
			public string   DisplayAs;
			public string   ChkBoxTag;

			public StatusEntry(string display, string tag)
			{
				DisplayAs = display;
				ChkBoxTag = tag;
			}
		}

		private static ListDictionary statusMappings;

		static QueueControl()
		{
			statusMappings = new ListDictionary();
			statusMappings.Add((int)BatchProcessStatusCode.Enqueued, new StatusEntry("Enqueued", "chkNew"));
			statusMappings.Add((int)BatchProcessStatusCode.EnqueuedRestart, new StatusEntry("Re-enqueued", "chkRestart"));
			statusMappings.Add((int)BatchProcessStatusCode.Paused, new StatusEntry("Paused", "chkPaused"));
			statusMappings.Add((int)BatchProcessStatusCode.Executing, new StatusEntry("Executing", "chkRunning"));
			statusMappings.Add((int)BatchProcessStatusCode.Dequeued, new StatusEntry("Dequeued", "chkDequeued"));
			statusMappings.Add((int)BatchProcessStatusCode.Succeeded, new StatusEntry("Succeeded", "chkSuccess"));
			statusMappings.Add((int)BatchProcessStatusCode.Failed, new StatusEntry("Failed", "chkFailed"));
            statusMappings.Add((int)BatchProcessStatusCode.FailedCanceled, new StatusEntry("Canceled", "chkCancelled"));
            statusMappings.Add((int)BatchProcessStatusCode.FailedSystemCanceled, new StatusEntry("SystemCanceled", "chkSystemCancelled"));//By Nick
			statusMappings.Add((int)BatchProcessStatusCode.FailedTimedOut, new StatusEntry("Timed-out", "chkTimeout"));
		}

//		private int		_currentPageNo;

		protected void Page_Load(object sender, System.EventArgs e)
        {
            if (IsPostBack)
			{
				if ( IsSet("__EVENTTARGET") == false )
				{
					this.PageNumber = 0;
					Refresh( );
				}
				UpdateFilteringFields();
    		}
			else
			{
				this.PageNumber = 0;
			}
		}

		public void AddOneLineOfScript(string script)
		{
            JavaScriptInit = new PlaceHolder(); 
			JavaScriptInit.Controls.Add(new LiteralControl( script ));
			JavaScriptInit.Controls.Add(new LiteralControl("\n"));
		}

		const string DATE_FROM_TAG		= "DateFrom";
		const string DATE_TO_TAG		= "DateTo";
		const string USE_FROM_TAG		= "chkDateFrom";
		const string USE_TO_TAG			= "chkDateTo";
		const string DISABLE_TEMPLATE	= "document.forms(0).elements['{0}'].disabled={1};";
		const string VALUE_TEMPLATE		= "document.forms(0).elements['{0}'].value='{1}';";
		const string CHECKBOX_TEMPLATE	= "document.forms(0).elements['{0}'].checked={1};";

		private void UpdateFilteringFields()
		{
			if (IsSet(DATE_FROM_TAG))
			{
				AddOneLineOfScript(string.Format(DISABLE_TEMPLATE,
					DATE_FROM_TAG,
					"false"));
				AddOneLineOfScript(string.Format(VALUE_TEMPLATE,
					DATE_FROM_TAG,
					Request.Form[DATE_FROM_TAG]));
				AddOneLineOfScript(string.Format(CHECKBOX_TEMPLATE,
					USE_FROM_TAG,
					"true"));
			}
			if (IsSet(DATE_TO_TAG))
			{
				AddOneLineOfScript(string.Format(DISABLE_TEMPLATE,
					DATE_TO_TAG,
					"false"));
				AddOneLineOfScript(string.Format(VALUE_TEMPLATE,
					DATE_TO_TAG,
					Request.Form[DATE_TO_TAG]));
				AddOneLineOfScript(string.Format(CHECKBOX_TEMPLATE,
					USE_TO_TAG,
					"true"));
			}
			foreach (StatusEntry entry in QueueControl.statusMappings.Values)
			{
				AddOneLineOfScript(string.Format(CHECKBOX_TEMPLATE,
					entry.ChkBoxTag,
					IsSet(entry.ChkBoxTag)? "true":"false"));
			}
		}

		private Guid RequestKey
		{
			get
			{
				try
				{
					return new Guid((string)ViewState["RequestKey"]);
				}
				catch
				{
					return Guid.Empty;
				}
			}
			set
			{
				ViewState["RequestKey"] = value.ToString();
			}
		}

		private int PageNumber
		{
			get
			{
				try
				{
					return (int)ViewState["PageNo"];
				}
				catch
				{
					return 0;
				}
			}
			set
			{
				ViewState["PageNo"] = value;
			}
		}

		private void GetStatusFilter(out string status, 
			out object dateFrom,
			out object dateTo)
		{
			string statusFilter = "";

			dateTo = null;
			dateFrom = null;
			foreach (object key in QueueControl.statusMappings.Keys)
			{
				StatusEntry entry = (StatusEntry)QueueControl.statusMappings[key];
				if (IsSet(entry.ChkBoxTag))
				{
					statusFilter += string.Format("{0},", (int)key);
				}
			}
			status = statusFilter;
			try
			{
                //dateFrom = DateTime.Parse(Request.Form[DATE_FROM_TAG]);
                CultureInfo us_culture = new CultureInfo("en-US");
                dateFrom = DateTime.ParseExact(Request.Form[DATE_FROM_TAG], "MM/dd/yyyy hh:mm:ss", us_culture);
			}
			catch(Exception ex)
			{
			}
			try
			{
                //dateTo = DateTime.Parse(Request.Form[DATE_TO_TAG]);
                CultureInfo us_culture = new CultureInfo("en-US");
                dateTo = DateTime.ParseExact(Request.Form[DATE_TO_TAG], "MM/dd/yyyy hh:mm:ss", us_culture);
			}
            catch (Exception ex)
			{
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
				Label1.Text = msg;
				Label1.Visible = true;
				Label2.Visible = false;
			}
			else
			{
				Label1.Visible = false;
				Label2.Visible = true;
			}
		}

		public void LoadResults(Database database, 
			IDataReader dataReader,
			object fromDate, 
			object toDate)
		{
            Application.Lock();            
			DataTable table = new DataTable();
			table.Columns.Add(new DataColumn("Key", typeof(Guid)));
			table.Columns.Add(new DataColumn("Request", typeof(string)));
			table.Columns.Add(new DataColumn("StatusCode", typeof(int)));
			table.Columns.Add(new DataColumn("ToPause", typeof(bool)));
			table.Columns.Add(new DataColumn("QueuedDate", typeof(DateTime)));
			table.Columns.Add(new DataColumn("Status", typeof(string)));
			table.Columns.Add(new DataColumn("Options", typeof(string)));
			while (dataReader.Read())
			{
				DataRow row = table.NewRow();
				int i = dataReader.GetOrdinal("QueuedDate");
				DateTime queuedDate = dataReader.GetDateTime(i);
				if (fromDate != null && queuedDate < (DateTime)fromDate ||
					toDate != null && queuedDate > (DateTime)toDate)
				{
					continue;
				}
				row[0] = dataReader.GetGuid(0);
				i = dataReader.GetOrdinal("BatchStatusCode");
				BatchProcessStatusCode status = (BatchProcessStatusCode)dataReader.GetByte(i);
				i = dataReader.GetOrdinal("BatchName");
				string batchName = dataReader.GetString(i);
				row[1] = batchName;
				row[2] = (int)status;
				i = dataReader.GetOrdinal("ToPause");
				row[3] = dataReader.GetBoolean(i);
				row[4] = queuedDate;
				i = dataReader.GetOrdinal("NextExecKey");
				if (dataReader.IsDBNull(i) == false)
				{
					row[5] = "Restarted";
				}
				else
				{
					row[5] = ((StatusEntry)QueueControl.statusMappings[(int)status]).DisplayAs;
				}			
				string s =  dataReader.GetGuid(0).ToString();
				row[6] = string.Format(
					"<A href=\"javascript:js_PopWin('history.aspx?request={0}&mode=history', 'Details{1}');\"><img src=\"./icons/info.ico\" Alt=\"Open New Window\" border=0 height=16 width=16></a>",
					s,
					s.Replace("-", "") // window name does not like the '-' character
					);
				table.Rows.Add(row);
			}
            Application.UnLock();
			DataView dataView = new DataView(table);
			
			string sort = string.Empty;
			if (ViewState["Sort"] != null && (string)(ViewState["Sort"]) != string.Empty)
			{
				sort = (string)ViewState["Sort"];
				dataView.Sort = sort;
				LabelSort.Text = "Sort by: " + sort;
			}
			
			int currentPageNo = this.PageNumber;
			DataGrid1.CurrentPageIndex = currentPageNo;
			DataGrid1.DataSource = dataView;
			DataGrid1.DataKeyField = "Key";
			DataGrid1.DataBind();
			if (currentPageNo >= DataGrid1.PageCount)
			{
				currentPageNo = DataGrid1.PageCount - 1;
				DataGrid1.CurrentPageIndex = currentPageNo;
				this.PageNumber = currentPageNo;
			}
			if (DataGrid1.Items.Count > 0)
			{
				DataGrid1.Visible = true;
				DetailsControl1.Visible = true;
				// the starting index for the DataGrid and the DataView is different
				// the DataGrid starts from the current page which could be in the
				// middle of the record for DataView
				int startingRecordIndex = DataGrid1.CurrentPageIndex * DataGrid1.PageSize;
				for (int i = 0; i < DataGrid1.Items.Count; i++)
				{
					int viewIndex = startingRecordIndex + i;
					int statusInt = (int)(dataView[viewIndex][2]);
					DataGridItem item = DataGrid1.Items[i];
					GetButtons((Guid)DataGrid1.DataKeys[i], 
						item, 
						(BatchProcessStatusCode)statusInt, 
						(bool)(dataView[viewIndex][3]),
						(string)(dataView[viewIndex][1]));
				}
				Label1.Visible = false;
				
				Label2.Visible = true;
			}
			else
			{
				DataGrid1.Visible = false;
				DetailsControl1.Visible = false;
				ShowErrorMessage("There are no matching requests.", true);
				Label2.Visible = false;
			}
		}

		private void GetButtons(Guid key,
			DataGridItem item, 
			BatchProcessStatusCode status, 
			bool toPause,
			string batchName)
		{
			const string templateRestart = 
					"javascript:js_PopWin('enqueue.aspx?request={0}&batch={1}', 'RestartRequest');";
			LinkButton btnPause = (LinkButton)item.FindControl("btnPause");
			LinkButton btnStop = (LinkButton)item.FindControl("btnStop");
			LinkButton btnRun = (LinkButton)item.FindControl("btnRun");
			HyperLink btnRestart = (HyperLink)item.FindControl("btnRestart");
			CheckBox chkbox = (CheckBox)item.FindControl("pausing");

			//string keyString = "=" + key.ToString();
			if (chkbox != null)
			{
				chkbox.Checked = toPause;
			}
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
					if (DetailsControl.GetRestartCount(key) == 1)
					{
						btnRestart.NavigateUrl = string.Format(templateRestart,
								key.ToString().Replace("-", ""),
								HttpUtility.HtmlEncode(batchName));
						btnRestart.Visible = true;
					}
					break;
				default:
					break;
			}
		}

		public void DataGrid1_Page(Object sender, DataGridPageChangedEventArgs e) 
		{
			this.PageNumber = e.NewPageIndex;
			Refresh();
			ShowSelectedImage( );
		}

		protected void DataGrid1_SortCommand(Object sender, DataGridSortCommandEventArgs e) 
		{
			string order = " ASC";
			if (ViewState["Sort"] != null && (string)ViewState["Sort"] != string.Empty)
			{
				string lastSort = (string)ViewState["Sort"];
				if (lastSort.IndexOf(order) >= 0)
				{
					order = " DESC";
				}
			} 
			ViewState["Sort"] = e.SortExpression + order;
			Refresh();
			DetailsControl1.ConfigurationNode = null;
		}

		public bool ShowSelectedNodeNoRefresh( Guid requestKey  )
		{
			string errorMsg = "";
			BatchExecutionContextData node = (BatchExecutionContextData)
				QueueControl.GetExecutionContextNode(requestKey, ref errorMsg);	

			if (errorMsg == string.Empty)
			{
				this.RequestKey = requestKey;
				ShowSelectedImage( );
				DetailsControl1.ConfigurationNode = node;
				ShowErrorMessage(string.Empty, false);
				return true;
			}
			else
			{
				ShowErrorMessage(errorMsg, true);
				return false;
			}

		}

		public void DataGrid1_ItemCommand(object sender, System.Web.UI.WebControls.DataGridCommandEventArgs args)
		{
			if (args.CommandName == "Page")
				return;
			int index = args.Item.ItemIndex;
			object obj = null;
			Guid requestKey = Guid.Empty;

			if (index < DataGrid1.Items.Count && index >= 0)
			{
				obj = DataGrid1.DataKeys[index];
				if (obj != null)
				{
					requestKey = (Guid)obj;
				}
			}
			switch (args.CommandName)
			{
				case "Details":
					// display as the currently selected request
					if (ShowSelectedNodeNoRefresh( requestKey ) == false)
						return;
					break;
				case COMMAND_PAUSE:
					DetailsControl1.SetPause(requestKey, true);
					ShowSelectedNodeNoRefresh( requestKey );
					break;
				case COMMAND_RUN:
					DetailsControl1.SetPause(requestKey, false);
					ShowSelectedNodeNoRefresh( requestKey );
					break;
				case COMMAND_STOP:
					DetailsControl1.CancelRequest(requestKey);
					ShowSelectedNodeNoRefresh( requestKey );
					break;
//				case COMMAND_RESTART:
//					DetailsControl1.RestartRequest(requestKey);
//					ShowSelectedNodeNoRefresh( requestKey );
//					break;
			}
		}

		const string COMMAND_PAUSE = "Pause";
		const string COMMAND_RUN = "Run";
		const string COMMAND_STOP = "Stop";
		const string COMMAND_RESTART = "Restart";

		private void ShowSelectedImage( )
		{
			Guid requestKey = Guid.Empty;

			// get the selected request key from the viewState
			requestKey = this.RequestKey;

			for (int j = 0; j < DataGrid1.Items.Count; j++)
			{
				DataGridItem item = DataGrid1.Items[j];
				System.Web.UI.WebControls.Image image = 
					(System.Web.UI.WebControls.Image)item.Cells[0].FindControl("SelectedImage");
				if (image != null)
				{
					image.Visible = (requestKey == (Guid)DataGrid1.DataKeys[j]);
				}
			}
		}

		private bool IsSet(string tag)
		{
			if (Request.Form[tag] == null || Request.Form[tag] == string.Empty)
			{
				return false;
			}
			return true;
		}

		public void Refresh( Guid key )
		{
			Refresh();
			ShowSelectedNodeNoRefresh( key  );
		}

		public void Refresh()
		{
			BatchExecutionData executionNode = null;
			IDataReader dataReader = null;
			Database database = null;

			executionNode = GetNewBatchExecutionData();
			object fromDate;
			object toDate;
			string statusFilter;
             
			GetStatusFilter(out statusFilter, out fromDate, out toDate);


			if (statusFilter == string.Empty)
			{
				ShowErrorMessage("Please select filters from above.", true);
				return;
			}
			
			Label2.Text = "Click on the item to view the details.";
			try
			{
				database = executionNode.DatabaseInstance;
                    dataReader =
                        DefaultBatchManager.RequestQueue.ListByStatus(
                        database,
                        statusFilter);
                    LoadResults(database, dataReader, fromDate, toDate);
			}
			catch (Exception exp)
			{
				string msg;
				if (database == null)
				{
					msg = FAILURE_DATABASE;
				}
				else
				{
					msg = FAILURE_GET_RESULTS;
				}
				ShowErrorMessage(msg, true);
				DetailsControl.Log(msg, exp);
			}
			finally
			{
				if (dataReader != null)
				{
					dataReader.Close();
				}

                lbLastUpdateDataGrid1.Text = "Grid Refreshed at: " + DateTime.Now.ToLongTimeString();

                if (lbLastUpdateDataGrid1.BackColor != Color.Orange)
                    lbLastUpdateDataGrid1.BackColor = Color.Orange;
                else
                    lbLastUpdateDataGrid1.BackColor = Color.White;
			}
		}

		#region Static Methods
		/// <summary>
		/// Gets a new BatchExecutionData.
		/// </summary>
		/// <returns>A new BatchExecutionData object.</returns>
		public static BatchExecutionData GetNewBatchExecutionData()
		{
			return BatchExecutionData.GetNewInstance();
		
		}

		/// <summary>
		/// Get the full object model of a BatchExecutionContext node, including its
		/// children, called when the reference to a BatchExectionNode is not needed.
		/// </summary>
		/// <param name="requestKey"></param>
		/// <param name="errorMessage"></param>
		/// <returns>A new BatchExecutionContextData.</returns>
		/// <remarks>This moethod will create a new BatchExecutionData and add the
		/// newly created BatchExecutionContextData as its only child.  It also
		/// incurs multiple accesses to the Database to get the full branch under
		/// the BatchExecutionContextData, include the JobExecutionNode and parameter nodes.
		/// </remarks>
		public static object GetExecutionContextNode(Guid requestKey, ref string errorMessage)
		{

			IDataReader dataReader = null;
			Database database = null;

			BatchExecutionData executionNode = GetNewBatchExecutionData();

			try
			{
				database = executionNode.DatabaseInstance;
				dataReader = DefaultBatchManager.RequestQueue.ListDetails(
					database, 
					requestKey);
				dataReader.Read();
				QueueControl.LoadResults(executionNode, 
					database, 
					dataReader);
				errorMessage = string.Empty;
				return executionNode.LoadRequestedBatch(database,requestKey);
			}
			catch (Exception exp)
			{		
				string msg;
				if (database == null)
				{
					msg = FAILURE_DATABASE;
				}
				else
				{
					msg = string.Format(FAILURE_FIND_REQUEST, requestKey.ToString());
				}
				errorMessage = msg + "<P>Errors: " + exp.Message;
				DetailsControl.Log(msg, exp);
				return null;
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
		/// Create a new BatchExecutionContextData based on the current record
		/// in the IDataReader object and add the new node to the children of
		/// a BatchExecutionNode object.
		/// </summary>
		/// <param name="executionNode"></param>
		/// <param name="database">An ACA.NET Database object.</param>
		/// <param name="dataReader">The IDataReader object that contains the
		/// results from invoking DefaultBatchManager.RequestQueue.List* methods.
		/// </param>
		static public void LoadResults(BatchExecutionData executionNode,
			Database database, 
			IDataReader dataReader)
		{
			Guid					requestKey;
			string					batchName;
			BatchRestartBehavior	restartBehavior;
			ExecutionIsolationLevel	isolationLevel;
			ExecutionPriorityLevel	exePriorityLevel;
			QueuePriorityLevel		quePriorityLevel;
			int						maxConcurrent;
			string					destination;
			string					batchTypeName;
			long					relativeExpirationDateInTicks;
			DateTime				absExpDate;
			string					batchServerName;
			string					batchClientName;
			DateTime				queuedDate;
			DateTime				startDate;
			DateTime				lastUpdateDate;
			BatchProcessStatusCode	batchStatusCode;
			Guid					originalBatchKey;
			Guid					lastExecutionKey;
			bool					toPause;
			Guid					parentRequestKey;
			string					configFile;
			string					batchDesc;
			Guid					nextExecKey;

			DefaultBatchManager.RequestQueue.ReadResults(dataReader,
				out requestKey,	out batchName, out batchDesc, 
				out restartBehavior, out configFile, out isolationLevel, 
				out exePriorityLevel, out quePriorityLevel,	out maxConcurrent, 
				out destination, out batchTypeName,	out relativeExpirationDateInTicks, 
				out absExpDate, out batchServerName, out batchClientName,
				out queuedDate, out startDate, out lastUpdateDate, 
				out batchStatusCode, out originalBatchKey, out lastExecutionKey,
				out toPause, out parentRequestKey, out nextExecKey);
			executionNode.LoadRequestedBatch(database, requestKey,
				batchName, batchDesc, 
				restartBehavior, configFile, isolationLevel, 
				exePriorityLevel, quePriorityLevel,	maxConcurrent, 
				destination, batchTypeName,	relativeExpirationDateInTicks, 
				absExpDate, batchServerName, batchClientName,
				queuedDate, startDate, lastUpdateDate, 
				batchStatusCode, originalBatchKey, lastExecutionKey,
				toPause, parentRequestKey, nextExecKey);
		}

		#endregion

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            if (cbAutoRefresh.Checked)
            {
                //lbLastUpdateDataGrid1.Text = "Grid Refreshed at: " + DateTime.Now.ToLongTimeString();
                Refresh();
                UpdateFilteringFields();
            }
        }

	}
}
