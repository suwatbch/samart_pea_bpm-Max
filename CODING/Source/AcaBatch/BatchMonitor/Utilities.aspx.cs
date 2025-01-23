using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

//using Microsoft.Practices.EnterpriseLibrary.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Avanade.ACA.Batch.Configuration;
using Avanade.ACA.Batch;

namespace Avanade.ACA.Batch.BatchMonitor
{
	/// <summary>
	/// Summary description for Utilities.
	/// </summary>
	public partial class Utilities : System.Web.UI.Page
	{

		const string TEMPLATE_DATE = "<input type=\"text\" name=\"DateOlder\" value=\"{0}\"> ";
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (IsPostBack)
			{
				// Put user code to initialize the page here
				if (Request.Form["OlderDate"] != null && 
					Request.Form["OlderDate"] != string.Empty)
				{
					btnCleanOlder_Click(sender, e);
				}
			}
			else
			{
				LiteralDate.Text = string.Format(
					TEMPLATE_DATE,
					DateTime.Now.AddDays(-7));
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
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		
		private Database GetDatabase()
		{
			Database database =  BatchExecutionData.Current.DatabaseInstance;
			
			if (database == null)
			{
				ShowMessage(
					QueueControl.FAILURE_DATABASE,
					true);
			}
			return database;
		}

		private const string FAILED_DELETE_ARCHIVE = "Failed to delete data from archive.";

		protected void btnCleanArchive_Click(object sender, System.EventArgs e)
		{
			Database database = GetDatabase();
			
			if (database == null)
			{
				return;
			}
			try
			{
				DefaultBatchManager.ExecutionHistory.CleanAll(
					database);
				ShowMessage(
					"All archived requests have been deleted.",
					true);
			}
			catch (Exception exp)
			{
				ShowMessage(
					FAILED_DELETE_ARCHIVE, true);
				DetailsControl.Log(FAILED_DELETE_ARCHIVE, exp);
			}
		}

		protected void btnCleanOlder_Click(object sender, System.EventArgs e)
		{
			Database database = GetDatabase();
			
			if (database == null)
			{
				return;
			}
			try
			{
				DateTime olderDate = DateTime.Parse((string)Request.Form["DateOlder"]);	

				DefaultBatchManager.ExecutionHistory.CleanOlderThan(
					database, olderDate);
				ShowMessage(
					string.Format("All archived requests prior to {0} have been deleted.",
								olderDate), true);
			}
			catch (Exception exp)
			{
				ShowMessage(
					FAILED_DELETE_ARCHIVE, true);
				DetailsControl.Log(FAILED_DELETE_ARCHIVE, exp);
			}
		}

		private void ShowMessage(string msg, bool toShow)
		{
			LabelErrorMsg.Text = msg;
			LabelErrorMsg.Visible = toShow;
		}

		private const string FAILED_ARCHIVE_REQUEST = "Failed to archive request.";

		protected void btnArchiveExpired_Click(object sender, System.EventArgs e)
		{
			Database database = GetDatabase();
			
			if (database == null)
			{
				return;
			}
			try
			{
				DateTime olderDate = DateTime.Parse((string)Request.Form["DateOlder"]);

				DefaultBatchManager.RequestQueue.ArchiveExpired( database );		
					ShowMessage(
						"All expired requests that have not be dequeued have been archived and removed from the request queue.",
						true);
			}
			catch (Exception exp)
			{
				ShowMessage(
					FAILED_ARCHIVE_REQUEST, true);
				DetailsControl.Log(FAILED_ARCHIVE_REQUEST, exp);
			}
		}
	}
}
