namespace Avanade.ACA.Batch.BatchMonitor
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using Microsoft.Practices.EnterpriseLibrary.Data;
	using Avanade.ACA.Batch;


	/// <summary>
	///		Summary description for Transitions1.
	/// </summary>
	public partial  class Transitions1 : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if (!IsPostBack && this.Request.Form[this.ID + ":" + btnRefresh.ID] == null)
			{
				UpdateList();
			}
		}

		public void UpdateList( )
		{
			string sort = string.Empty;
			Guid requestKey = Guid.Empty;
			IDataReader dataReader = null;
			if (ViewState["Sort"] != null && (string)(ViewState["Sort"]) != string.Empty)
			{
				sort = (string)ViewState["Sort"];
			}
			try
			{
				string requestIdStr = Request.QueryString["request"];
				requestKey = new Guid(requestIdStr);

				LabelErrorMessage.Text = "<BR>Failed to get transition data for the request.";
				Database database = BatchExecutionData.Current.DatabaseInstance;
				dataReader = 
					DefaultBatchManager.RequestQueue.ListTransitions(
					database, requestKey);
				//BatchQueueKey, DateModified, StatusCode, ToPause
				DataTable dataTable = new DataTable("Transitions for Request: " + requestIdStr);
				dataTable.Columns.Add(new DataColumn("Timestamp", typeof(DateTime)));
				dataTable.Columns.Add(new DataColumn("Status", typeof(string)));
				dataTable.Columns.Add(new DataColumn("Pausing", typeof(bool)));
				while (dataReader.Read())
				{
					DataRow row = dataTable.NewRow();
					dataTable.Rows.Add(row);
					row[0] = dataReader.GetDateTime(1);
					BatchProcessStatusCode statusCode = (BatchProcessStatusCode)
						dataReader.GetByte(2);
					row[1] = statusCode.ToString();
					row[2] = dataReader.GetBoolean(3);
				}
				DataView dataView = new DataView(dataTable);
				dataView.Sort = sort;
				DataGrid1.DataSource = dataView;
				DataGrid1.DataBind();
				LabelErrorMessage.Visible = false;
				dataReader.Close();
				dataReader = null;
			
				LabelErrorMessage.Text = "<BR>Failed to get details information for the request.";

				// try to get the information for the request
				dataReader = 
					DefaultBatchManager.RequestQueue.ListDetails(database, requestKey);

				dataReader.Read();
				int i = dataReader.GetOrdinal("QueuedDate");
				DateTime queuedTime = dataReader.GetDateTime(i);
				i = dataReader.GetOrdinal("BatchStatusCode");
				BatchProcessStatusCode statusCode2 = 
					(BatchProcessStatusCode)dataReader.GetByte(i);
				i = dataReader.GetOrdinal("BatchName");
				string batchName = dataReader.GetString(i);
				Label1.Text = string.Format(
					"{0} - {1}: {2}", batchName, queuedTime, statusCode2);
				Literal1.Text = 
					string.Format("<script language=\"javascript\">document.title = 'Transition: {0}-{1}';</script>", 
					batchName, queuedTime);
				LabelErrorMessage.Visible = false;
			}
			catch 
			{
				Label1.Text = string.Format("Transitions for Request {0}:",
					requestKey.ToString());
				LabelErrorMessage.Visible = true;
			}			
			finally
			{
				if (dataReader != null)
				{
					dataReader.Close();
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

		protected void btnRefresh_Click(object sender, System.EventArgs e)
		{
			UpdateList();
		}

		protected void DataGrid1_Sort(Object sender, DataGridSortCommandEventArgs e) 
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
			UpdateList();
		}
	}
}
