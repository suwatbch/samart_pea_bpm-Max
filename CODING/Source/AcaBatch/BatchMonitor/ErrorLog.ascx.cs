using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Avanade.ACA.Batch;
using System.Drawing;

namespace Avanade.ACA.Batch.BatchMonitor
{
    public partial class ErrorLog1 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            // Put user code to initialize the page here
            if (!IsPostBack && this.Request.Form[this.ID + ":" + btnRefresh.ID] == null)
            {
                UpdateList();
            }
        }

        public void UpdateList()
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

                LabelErrorMessage.Text = "<BR>Failed to get error data for the request.";
                Database database = BatchExecutionData.Current.DatabaseInstance;
                dataReader =
                    DefaultBatchManager.RequestQueue.ListErrorLog(database, requestKey);
                //BatchQueueKey, DateModified, StatusCode, ToPause
                DataTable dataTable = new DataTable("Processing Log for Request: " + requestIdStr);
                dataTable.Columns.Add(new DataColumn("บันทึกเวลา", typeof(DateTime)));
                dataTable.Columns.Add(new DataColumn("ประเภทความผิดพลาด", typeof(string)));
                dataTable.Columns.Add(new DataColumn("คำอธิบายความผิดพลาด", typeof(string)));
                dataTable.Columns.Add(new DataColumn("บรรทัดที่ผิด", typeof(string)));
                //BatchKey(0),ErrorType(1),ModuleName(2),ErrorMsg(3),SuggMsg(4),ErrorLine(5),InsDate(6)
                bool changeHeader = false;

                while (dataReader.Read())
                {
                    DataRow row = dataTable.NewRow();
                    dataTable.Rows.Add(row);
                    row[0] = dataReader.GetDateTime(6);                    
                    row[1] = string.Format("{0} [{1}]", dataReader.GetString(1), dataReader.GetString(2));
                    row[2] = string.Format("{0} [{1}]", dataReader.GetString(3), dataReader.GetString(4));
                    row[3] = dataReader.GetInt32(5).ToString();

                    if (dataReader.GetString(1) == "Process Batch Successed")
                        changeHeader = true;
                }

                //change header 
                if (changeHeader)
                {
                    dataTable.Columns[1].ColumnName = "ประเภทข้อความ";
                    dataTable.Columns[2].ColumnName = "ข้อความอธิบาย";
                    dataTable.Columns[3].ColumnName = "จำนวนข้อมูลนำเข้า";
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
                    string.Format("<script language=\"javascript\">document.title = 'Batch Logging: {0}-{1}';</script>",
                    batchName, queuedTime);
                LabelErrorMessage.Visible = false;
            }
            catch(Exception e)
            {
                Label1.Text = string.Format("ErrorLog for Request {0}:",
                    requestKey.ToString());
                LabelErrorMessage.Visible = true;
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

        protected void TimerAutorefresh_Tick(object sender, EventArgs e)
        {
            if (cbAutoRefresh.Checked)
            {
                UpdateList();
                
            }
        }
    }
}

