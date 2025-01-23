namespace Avanade.ACA.Batch.BatchMonitor
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	
	//using Microsoft.Practices.EnterpriseLibrary.Configuration;
	using Avanade.ACA.Batch;
	using Avanade.ACA.Batch.Configuration;

	/// <summary>
	///		Summary description for BatchControl.
	/// </summary>
	public partial  class BatchControl : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			BatchDatabaseSettingsData batchDatabaseNode = this.GetBatchDatabaseNode();

			if (batchDatabaseNode != null)
			{
				ConfigurationTreeControl1.ConfigurationNode = batchDatabaseNode;
				//ConfigurationTreeControl1.Style = 0;
			}
			else
			{
				ConfigurationTreeControl1.Visible = false;
				btnRefresh.Visible = false;
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

		}

		private BatchDatabaseSettingsData GetBatchDatabaseNode()
		{

			BatchDatabaseSettingsData settings = null;
			Microsoft.Practices.EnterpriseLibrary.Data.Database db = null;
			try 
			{
				db = BatchExecutionData.Current.DatabaseInstance;
                settings = new BatchDatabaseSettingsData(db);                
			}
			catch 
			{
				if (db ==null) 
					Label1.Text = QueueControl.FAILURE_DATABASE;
				else
					Label1.Text = QueueControl.FAILURE_GET_RESULTS;
			}
			return settings;
		}
	}
}
