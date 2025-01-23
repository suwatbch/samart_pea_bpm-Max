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

using Microsoft.Practices.EnterpriseLibrary.Data;
using Avanade.ACA.Batch;

namespace Avanade.ACA.Batch.BatchMonitor
{
	/// <summary>
	/// Summary description for Enqueue.
	/// </summary>
	public partial class Enqueue : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.PlaceHolder TitleScript;

		private Guid requestKey;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			requestKey = Guid.Empty;
			try
			{
				requestKey = new Guid((string)Request.QueryString["request"]);
				TitleScript.Controls.Add(new LiteralControl(
					"document.title = \"Restart a Reqeust\";"));
			}
			catch
			{
			}
			if (!IsPostBack)
			{
				txtAbsExpDate.Text = DateTime.Now.AddDays(7).ToString();
				BatchName.Text = Request.QueryString["batch"];
				PopulateControls( );
			}
			else
			{
			}
		}

		const string NO_OVERRIDE = "No Override";
		private void PopulateControls()
		{
			if (requestKey != Guid.Empty)
			{
				literalRequest.Text = "<tr><td style=\"WIDTH: 45%\" align=\"right\">Request Key:</td>";
				literalRequest.Text += "<td align=\"left\">" + requestKey.ToString() + "</td></tr>";
				literalRequest.Visible = true;
				txtClientName.Enabled = false;
				txtClientName.Text = "(original batch client)";
				btnEnqueue.Text = "Restart Request";
			}
			else
			{
				literalRequest.Visible = false;
			}
			ArrayList priorityList = new ArrayList();
			priorityList.Add(NO_OVERRIDE);
			priorityList.Add(QueuePriorityLevel.Highest.ToString());
			priorityList.Add(QueuePriorityLevel.High.ToString());
			priorityList.Add(QueuePriorityLevel.Normal.ToString());
			priorityList.Add(QueuePriorityLevel.Low.ToString());
			priorityList.Add(QueuePriorityLevel.Lowest.ToString());

			DropDownPriority.DataSource = priorityList;
			DropDownPriority.DataBind();

			Database db = BatchExecutionData.Current.DatabaseInstance;

			IDataReader reader = DefaultBatchManager.Destination.List(db);
			ArrayList destList = new ArrayList();

			destList.Add(NO_OVERRIDE);
			try
			{
				while (reader.Read())
				{
					int i = reader.GetOrdinal("BatchDestName");
					string destName = reader.GetString(i);
					destList.Add(destName);		
				}
				DropDownDestination.DataSource = destList;
				DropDownDestination.DataBind();
			}
			catch (Exception)
			{
				LabelErrorMsg.Text = "Failed to get Destination information from database.";
			}
			finally
			{
				reader.Close();
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

		protected void btnEnqueue_Click(object sender, System.EventArgs e)
		{
			BatchName.Text = BatchName.Text.Trim();
			if (BatchName.Text == string.Empty)
			{
				LabelErrorMsg.Text = "Batch name is empty.";
				return;
			}	
			txtClientName.Text = txtClientName.Text.Trim();	
			if (txtClientName.Text == string.Empty)
			{
				LabelErrorMsg.Text = "Batch client name is empty.";
				return;
			}		
			Database database = BatchExecutionData.Current.DatabaseInstance;
			RequestEnqueueHandle handle = null;

			DateTime absExpDate = DateTime.MaxValue;
			if (txtAbsExpDate.Text != string.Empty)
			{
				absExpDate = DateTime.Parse(txtAbsExpDate.Text);
			}
			object priority = DBNull.Value;
			if (DropDownPriority.SelectedIndex > 0)
			{
				string priText = DropDownPriority.Items[DropDownPriority.SelectedIndex].Text;
				priority = QueuePriorityLevel.Parse(typeof(QueuePriorityLevel), priText);
			}
			NewDestination.Text.Trim();
			object destination  = DBNull.Value;
			if (NewDestination.Text != string.Empty)
			{
				destination = NewDestination.Text;
			}
			else
			{
				if (DropDownDestination.SelectedIndex > 0)
				{
					destination = DropDownDestination.Items[DropDownDestination.SelectedIndex].Text;
				}
			}
			Guid newRequestKey = Guid.NewGuid();

			if (requestKey == Guid.Empty)
			{
				try
				{
					handle = DefaultBatchManager.RequestQueue.CreateRequestEnqueueHandle(
						database,
						newRequestKey,
						Guid.Empty,
						BatchName.Text,
						Guid.Empty);
					handle.BeginEnqueue(
						priority,
						absExpDate,
						destination,
						txtClientName.Text);		
					handle.EndEnqueue(true);
		
					LabelErrorMsg.Text = string.Format(
						"Batch '{0}' has been enqueued successfully.  The new request is: {1}.",
						BatchName.Text,
						newRequestKey.ToString());

					// clear the batch name
					BatchName.Text = string.Empty;
				}

				catch (Exception exp)
				{
					if (handle != null)
					{
						handle.EndEnqueue(false);
					}
					LabelErrorMsg.Text = "Failed to enqueue request.";
					DetailsControl.Log(LabelErrorMsg.Text, exp);
				}		
			}
			else
			{
				try
				{
					DefaultBatchManager.RequestQueue.RestartFromFailure(
						database,
						requestKey,
						newRequestKey,
						priority,
						absExpDate,
						destination);
					LabelErrorMsg.Text = string.Format(
						"Batch '{0}' has been restarted successfully.  The new request is: {1}.",
						BatchName.Text,
						newRequestKey.ToString());

					// disable the controls
					LabelHint.Visible = false;
					btnEnqueue.Visible = false;
					txtAbsExpDate.Enabled = false;
					DropDownDestination.Enabled = false;
					DropDownPriority.Enabled = false;
					txtClientName.Enabled = false;
					BatchName.Enabled = false;
					this.LinkCalendar.Visible = false;
					RefreshOpenerWindow();
				}
				catch (Exception exp)
				{
					LabelErrorMsg.Text = "Failed to restart request.";
					DetailsControl.Log(LabelErrorMsg.Text, exp);
				}		
			}
		}
		
		void RefreshOpenerWindow()
		{
            TitleScript = new PlaceHolder();
            this.TitleScript.Controls.Add(new LiteralControl("\n"));
			this.TitleScript.Controls.Add(new LiteralControl(
				"window.opener.document.forms[0].submit();"));
		}
	}
}
