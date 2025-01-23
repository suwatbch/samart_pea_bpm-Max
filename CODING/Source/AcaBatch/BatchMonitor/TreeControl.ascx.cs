namespace Avanade.ACA.Batch.BatchMonitor
{
	using System;
	using System.Data;
    using System.Data.SqlClient;
	using System.Drawing;
	using System.Web;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Collections;
	using System.Collections.Specialized;

	using Microsoft.Practices.EnterpriseLibrary.Data;
	using Avanade.ACA.Batch.Configuration;
	using Avanade.ACA.Batch;


	/// <summary>
	///		Summary description for TreeControl.
	/// </summary>
	public partial  class TreeControl : System.Web.UI.UserControl, IRequestListControl
	{
		protected System.Web.UI.WebControls.PlaceHolder vbScript = new PlaceHolder();

//		private Guid _requestKey;
//		private Guid _selectObjectKey;
		private BatchExecutionData _BatchExecutionData;
		private Hashtable _keyToStateMappings;
		private bool	_newTreeControlExist;

		private bool IsSet(string tag)
		{
            bool returnValue = true;
            if (tag == "Key")
            {
                if (this.Key.Text == null || this.Key.Text == string.Empty)
                {
                    returnValue = false;
                }
            }
            else if (tag == "State")
            {
                if (this.State.Text == null || this.State.Text == string.Empty)
                {
                    returnValue = false;
                }
            }
            //if (Request.Form[tag] == null || Request.Form[tag] == string.Empty)
            //{
            //    return false;
            //}
            return returnValue;
		}

		private Guid GetPresetNodeKey()
		{
			Guid requestKey;
			try
			{
				string keyString = (string)ViewState["NodeKey"];
				requestKey = new Guid(keyString);
				return requestKey;
			}
			catch
			{
				return Guid.Empty;
			}
		}

		private void SetPresetNodeKey( string keyString )
		{
			ViewState["NodeKey"] = keyString;
		}

		private const string PLATFORM = "Windows NT 5.";
		protected void Page_Load(object sender, System.EventArgs e)
		{
			_newTreeControlExist = false;

			string userAgent = Request.Headers["User-Agent"];
			if (userAgent != null && userAgent != string.Empty)
			{
				int index = userAgent.IndexOf(PLATFORM);
				try
				{
					char c = userAgent[index + PLATFORM.Length];
					if (c == '1')
					{
						_newTreeControlExist = true;
					}
				}
				catch
				{
				}
			}

			_keyToStateMappings = new Hashtable();
			if ( IsSet("Key"))
			{
				//SetPresetNodeKey( Request.Form["Key"] );
                SetPresetNodeKey(this.Key.Text.Trim());
			}
			if (IsSet("State"))
			{
				ParseTreeExpansionState();			
			}
			Refresh();            
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
            this.tree1.SelectedNodeChanged +=new EventHandler(tree1_SelectedNodeChanged);
		}
		#endregion

		private void ShowErrorMessage(string msg, bool bShow)
		{
			if (bShow == true)
			{
				Label1.Text = msg;
				Label1.Visible = true;
			}
			else
			{
				Label1.Visible = false;
			}
		}

		private void ParseTreeExpansionState()
		{
			//string states = (string)Request.Form["State"];
            string states = this.State.Text.Trim();   
			if (states == null)
			{
				return;
			}
			string[] tokens = states.Split('&');
			foreach(string token in tokens)
			{
				try
				{
					string[] equation = token.Split('=');
					if (equation.Length == 2)
					{
						//Guid key = new Guid(equation[0]);
						bool opened = Boolean.Parse(equation[1]);
						_keyToStateMappings[equation[0]] = opened;
					}
				}
				catch
				{
				}
			}
		}

		public void AddClientScript(PlaceHolder placeHolder, string script)
		{
			placeHolder.Controls.Add(new LiteralControl( script ));
			placeHolder.Controls.Add(new LiteralControl("\n"));
		}
                
		public void OutputClientTreeObject( )
		{
			clientTreeObj.Controls.Clear( );

			AddClientScript(clientTreeObj, 
				"<P class=\"Head3\">Click on the items to view their details.</P>");
		}

		public void Refresh(Guid key)
		{
			// refresh the whole tree
			Refresh();
		}

		public void Refresh()
		{
			IDataReader dataReader = null;
			string mode;
			string requestIdStr = Request.QueryString["request"];
			Guid requestKey;

			_BatchExecutionData = null;

			try
			{
				requestKey = new Guid(requestIdStr);
				if (!IsPostBack)
				{
					SetPresetNodeKey(requestIdStr);
				}
			}
			catch
			{
				if (Request.QueryString["request"] == null)
				{
					ShowErrorMessage("No Request Key.", true);
				}
				else
				{
					ShowErrorMessage(
						string.Format("Invalid Request Key.", requestIdStr), 
						true);
				}
				return;
			}
			if ( Request.QueryString["mode"] == null || 
				Request.QueryString["mode"] == string.Empty)
			{
				mode = "history";
			}
			else
			{
				mode = Request.QueryString["mode"];
			}

			bool hasChildren = false;
			try
			{
				_BatchExecutionData = QueueControl.GetNewBatchExecutionData();
				Database database = _BatchExecutionData.DatabaseInstance;

				// populates the children of batchExecuteNode according the the different
				// mode getting from the QueryString
				switch (mode)
				{
					case DetailsControl.MODE_HISTORY:
						// getting the restarting history of the batch.  Trace all the way back
						// to the original request.
						dataReader = DefaultBatchManager.RequestQueue.ListRestarts(
							database, requestKey);
						DetailsControl1.Style = DetailsControl.LinkStyle.NoHistoryLink;
						// creating a new BatchExecutionContextData for each restart.
						break;
					case DetailsControl.MODE_CHILD:
						// adding the parent
						dataReader = DefaultBatchManager.RequestQueue.ListDetails(
							database, requestKey);

						dataReader.Read();
						QueueControl.LoadResults(_BatchExecutionData, database, dataReader);	

						// here we combine the parent and child in the Children collection
						// but we will sepeerate
						dataReader = DefaultBatchManager.RequestQueue.ListByParent(
							database, requestKey);
						break;
				}				

				
				while (dataReader.Read())
				{
					QueueControl.LoadResults(_BatchExecutionData, database, dataReader);	
					hasChildren = true;
				}
			}
			catch (Exception exp)
			{
				ShowErrorMessage(exp.Message, true);
			}	
			finally
			{
				if (dataReader != null)
				{
					dataReader.Close();
				}
				if (!hasChildren)
				{
					ShowErrorMessage("The request key does not match any batch request", true);
				}
				else
				{
					LoadResults( _BatchExecutionData );
				}
			}
		}

        protected void tree1_SelectedNodeChanged(object sender, EventArgs e)
        {
            string str = string.Empty; 
            TreeNode node = new TreeNode();
            node = tree1.SelectedNode;
            if (node.Value.Length == 36 && node.Value.Substring(8, 1) == "-" && node.Value.Substring(13, 1) == "-" && node.Value.Substring(18, 1) == "-" && node.Value.Substring(23, 1) == "-")
            {
                foreach (TreeNode node1 in tree1.Nodes)
                {
                    if (node1.Expanded == true)
                    {
                        str = str + node1.Text + "=true&";
                    }
                    else
                    {
                        str = str + node1.Text + "=false&";
                    }
                }
                this.State.Text = str;
                this.Key.Text = tree1.SelectedValue;   
                SetPresetNodeKey(tree1.SelectedValue.ToString());
                Refresh(); 
           }
        }

        
        private void AddChildNode(IBatchDBData node, TreeNode root, string parentText, string curText)
        {
            Guid key = Guid.Empty;
            string nodeText = string.Empty;

            TreeControl.GetDisplayName(node, ref nodeText, ref key);
            TreeNode treenode = new TreeNode(); 

            treenode.Text = nodeText;
            treenode.Value = key.ToString();

            if (node.Children.Count == 1)
            {
                treenode.Expanded = true;
            }
            root.ChildNodes.Add(treenode);   
             
            for (int i = 0; i < node.Children.Count; i++)
            {
                AddChildNode((IBatchDBData)node.Children[i], treenode, curText, curText + i.ToString());

            }
        }

        private void AddNode(IBatchDBData node,TreeNode root, string parentText, string curText)
        {
                Guid key = Guid.Empty;
                string nodeText = string.Empty;

                TreeControl.GetDisplayName(node, ref nodeText, ref key);

                root.Text = nodeText;
                root.Value = key.ToString();
                
                if (node.Children.Count == 1)
                {
                    root.Expanded = true;
                }
                tree1.Nodes.Add(root);
                for (int i = 0; i < node.Children.Count; i++)
                {
                    AddChildNode((IBatchDBData)node.Children[i], root, curText, curText + i.ToString());

                }
        }

		public void LoadResults(BatchExecutionData BatchExecutionData)
		{
			OutputClientTreeObject();
            
            IList children = BatchExecutionData.Children;
			Guid selectedObjectKey = GetPresetNodeKey( );
            if (!this.IsPostBack)
            {           
                for (int i = 0; i < children.Count; i++)
                {
                    AddNode((IBatchDBData)children[i], new TreeNode(),  string.Empty  , "node" + i.ToString());
                }
            }
            IBatchDBData selectedNode = null;
			if (selectedObjectKey != Guid.Empty)
			{
				selectedNode = FindChildNodeByKey( _BatchExecutionData, selectedObjectKey );
				if ( selectedNode != null )
				{
					DetailsControl1.ConfigurationNode = selectedNode;
				}
			}
		}

        private IBatchDBData FindChildNodeByKey(IBatchDBData root, Guid objectKey)
        {
            IList children = root.Children;
            for (int i = 0; i < children.Count; i++)
            {
                if (children[i] is IBatchDBData)
                {
                    IBatchDBData dbNode = (IBatchDBData)children[i];
                    if (dbNode.Key == objectKey)
                    {
                        return dbNode;
                    }
                }
                IBatchDBData obj = FindChildNodeByKey((IBatchDBData)children[i], objectKey);
                if (obj != null)
                {
                    return obj;
                }
            }
            return null;
        }

        //private IBatchDBData FindChildNodeByKey( IBatchDBData root, 
        //                                          Guid objectKey )
        //{
        //    IList children = root.Children;
        //    for (int i = 0; i < children.Count; i++)
        //    {
        //        if ( children[i] is IBatchDBData )
        //        {
        //            IBatchDBData dbNode = (IBatchDBData)children[i];
        //            if (dbNode.Key == objectKey)
        //            {
        //                return dbNode;
        //            }
        //        }
        //        IBatchDBData obj = FindChildNodeByKey( (IBatchDBData) children[i], objectKey );
        //        if ( obj != null )
        //        {
        //            return obj;
        //        }
        //    }
        //    return null;
        //}

		public static void GetDisplayName(IBatchData node, 
			ref string nodeText, ref Guid key)
		{
			nodeText = node.DisplayName;
			Type type = node.GetType();
			switch (type.Name)
			{
				case "BatchExecutionContextData":
					BatchExecutionContextData contextNode = (BatchExecutionContextData)node;
					nodeText = contextNode.BatchName + "- " + 
						contextNode.QueuedDate + 
						": " + 
						contextNode.BatchStatus.ToString();
					key = contextNode.Key;
					break;
				case "JobExecutionContextData":
					JobExecutionContextData jobContextNode = (JobExecutionContextData)node;
					nodeText = jobContextNode.Sequence.ToString() + 
						". " + jobContextNode.JobName + 
						": " +
						jobContextNode.Status.ToString();
					key = jobContextNode.Key;
					break;
				case "ParameterData":
					ParameterData parameterNode = (ParameterData)node;
					key = parameterNode.Key;
					break;
				case "ExceptionData":
					ExceptionData exceptionNode = (ExceptionData)node;
					key = exceptionNode.Key;
					break;
			}
		}
	}
}
