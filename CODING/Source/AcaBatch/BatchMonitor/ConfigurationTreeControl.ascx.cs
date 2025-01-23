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

	using Avanade.ACA.Batch;

	/// <summary>
	///		Summary description for ConfigurationTreeControl.
	/// </summary>
	public partial  class ConfigurationTreeControl : System.Web.UI.UserControl
	{
		//private ApplicationConfigurationNode _configNode;
		private IBatchDBData _configNode;
		protected System.Web.UI.WebControls.ImageButton Plus;

		const int	_imageIndex = 1;
		protected System.Web.UI.WebControls.Repeater Repeater1;
		protected System.Web.UI.WebControls.Literal LiteralRowBreak;
		protected System.Web.UI.WebControls.Literal LiteralTreeTD;

		WebTree		_tree;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack && _configNode != null)
			{
				DataList1.DataSource  = ShowDetails(0);
				DataList1.DataBind();
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

		public IBatchDBData ConfigurationNode
		{
			get 
			{
				return _configNode;
			}
			set
			{
				if (_configNode != value)
				{
					_configNode = value;
				}
			}
		}

		public void ClearViewState()
		{
			ViewState.Clear();
		}

		protected void DataList1_ItemCommand(object sender, DataListCommandEventArgs e)
		{
			string command = e.CommandName;
			switch (command)
			{
				case "Expand":
					break;
				case "Details":
					int index = e.Item.ItemIndex;
					
					DataList1.DataSource = ShowDetails(index);
					DataList1.DataBind();
					break;
			}
		}

		private WebTree ShowDetails(int index)
		{
			WebTree tree = BuildTree();
			WebTreeNode treeNode = null;
			if (index == 0)	// the user didn't actually click on an item
			{
				// try to find the the been clicked before
				try
				{
					string keyString = (string)ViewState["ObjectKey"];
					treeNode = tree[new Guid(keyString)];
					treeNode.Selected = true;
					DetailsControl1.ConfigurationNode = (IBatchDBData)treeNode.Config;
				}
				catch
				{
				}
			}

			if (treeNode == null)
			{
				treeNode = tree[index];
				treeNode.Selected = true;
				DetailsControl1.ConfigurationNode = (IBatchDBData)tree.GetConfigAt(index);
				ViewState["ObjectKey"] = DetailsControl1.ConfigurationNode.Key.ToString();
			}

			LiteralTransition.Text = treeNode.Actions;
			if (DetailsControl1.ConfigurationNode is BatchExecutionContextData)
			{
				ViewState["RequestKey"] = treeNode.Key.ToString();
			}
			return tree;
		}

		private WebTree BuildTree()
		{
			if (_tree != null)
			{
				return _tree;
			}
			_tree = new WebTree(_configNode);
			int total = 0;
			if (ViewState["Total"] != null)
			{
				total = (int)ViewState["Total"];
			}
					
			for (int i = 0; i< total; i++)
			{
				bool expanded = (bool)ViewState[i.ToString()];
				_tree.SetExpansionAt(i, expanded);
			}
			return _tree;
		}

		public void OnExpand_Click(object sender, ImageClickEventArgs e)
		{
			ImageButton imgBtn = (ImageButton)sender;
			DataListItem item = (DataListItem)imgBtn.Parent;
			int index = item.ItemIndex;

			WebTree tree = BuildTree();
			tree.ToggleAt(index);
			tree.GetState(ViewState);	
			ShowDetails(0);

			DataList1.DataSource = tree;
			DataList1.DataBind();
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

	}
}
