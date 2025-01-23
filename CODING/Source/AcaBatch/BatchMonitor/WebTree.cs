using System;
using System.Web.UI;
using System.Collections;
using System.Collections.Specialized;

using Avanade.ACA.Batch;

namespace Avanade.ACA.Batch.BatchMonitor
{
	public class WebTreeNode 
	{
		protected int _depth;
		protected bool _expanded;
		protected IBatchDBData _node;
		protected bool _selected;

		public const string ICON_MINUS = "./icons/Minus.ico";
		public const string ICON_PLUS = "./icons/Plus.ico";
		public const string ICON_SPACER = "./icons/Leaf.ico";
		
		public WebTreeNode(IBatchDBData node, 
			int depth,
			bool expanded )
		{
			_depth = depth;
			_expanded = expanded;
			_node = node;
			_selected = false;
		}

		public IBatchDBData Config
		{
			get 
			{
				return _node;
			}
		}

		public int Depth
		{
			get 
			{
				return (_depth-1)*20;
			}
		}

		public bool Expanded
		{
			get
			{
				return _expanded;
			}
			set
			{
				_expanded = value;
			}
		}

		public string Icon
		{
			get
			{
				if (Expandable)
				{
					if (_expanded)
					{
						return ICON_MINUS;
					}
					else
					{
						return ICON_PLUS;
					}
				}
				else
				{
					return ICON_SPACER;
				}
			}
		}

		public virtual bool LinkEnabled
		{
			get
			{
				IBatchDBData dbNode = _node as IBatchDBData;
				if (dbNode != null)
				{
					if (dbNode.Key != Guid.Empty)
					{
                        if (dbNode.GetType() == typeof(Avanade.ACA.Batch.Configuration.ParameterCollection))
                            return false;
                        else
                            return true;                        
					}
				}
				return false;
			} 
		}

		public virtual bool Selected
		{
			get
			{
				return _selected;
			}
			set
			{
				_selected = value;
			}
		}

		public virtual string Indentation
		{
			get
			{
				int spacer = (_depth-1) * 20;
				string text = string.Empty;
				if (_selected)
				{
					text = "<img src='ICONS/Nav1Rt.gif' width='10' Height='10' />";
				}
				else
				{
					spacer += 10;
				}
				text += string.Format(
							"<img src='./icons/spacer.gif' width='{0}' height='20' />",
							spacer);
				return text;
			}
		}

		public virtual bool Expandable
		{
			get
			{
				bool b = (_node.Children.Count > 0);
				return b;
			}
		}

		public virtual string DisplayName
		{
			get
			{
				return _node.DisplayName;
			} 
		}

		public virtual string Status
		{
			get
			{
				return BatchProcessStatusCode.Unknown.ToString();
			}
		}

		public virtual string Actions
		{
			get
			{
				return "";
			}
		}

		public virtual Guid Key
		{
			get
			{
				IBatchDBData dbNode = _node as IBatchDBData;
				if (dbNode != null)
				{
					if (dbNode.Key != Guid.Empty)
						return dbNode.Key;
				}
				return Guid.Empty;
			}
		}

		public void MoveDown()
		{
			_depth++;
		}

		public void InsertBranch(ArrayList list, int index)
		{
			if (_expanded)
			{
				int curDepth = _depth+1;
				foreach (IBatchDBData childConfigNode in _node.Children)
				{
					IBatchDBData node1 = childConfigNode;
					WebTreeNode branch = WebTree.NewWebTreeNode(node1, curDepth, false);
					list.Insert(index++, branch);
				}
			}
		}
	}

	public class ActiveRequestTreeNode : WebTreeNode
	{
		public ActiveRequestTreeNode(IBatchDBData node, 
			int depth,
			bool expanded ) : base(node, depth, expanded )
		{
		}

		public override string DisplayName
		{
			get
			{
				BatchExecutionContextData batchExeContextNode = 
					(BatchExecutionContextData)_node;
				return batchExeContextNode.BatchName + 
					" - " +
					batchExeContextNode.QueuedDate.ToString() +
					": " +
					batchExeContextNode.BatchStatus;
			}
		}

		public override string Status
		{
			get
			{
				BatchExecutionContextData batchExeContextNode = 
					(BatchExecutionContextData)_node;
				return batchExeContextNode.BatchStatus.ToString();
			}
		}

		public override Guid Key
		{
			get
			{
				BatchExecutionContextData batchExeContextNode = 
					(BatchExecutionContextData)_node;
				return batchExeContextNode.Key;
			}
		}

		public override bool LinkEnabled
		{
			get
			{
				return true;
			} 
		}
	}

	public class JobExecutionTreeNode : WebTreeNode
	{
		public JobExecutionTreeNode(IBatchDBData node, 
			int depth,
			bool expanded ) : base(node, depth, expanded )
		{
		}

		public override string DisplayName
		{
			get
			{
				JobExecutionContextData jobExeContextNode = 
					(JobExecutionContextData)_node;
				return jobExeContextNode.Sequence.ToString() + 
					". " +
					jobExeContextNode.JobName + 
					": " +
					jobExeContextNode.Status;
			}
		}

		public override string Status
		{
			get
			{
				JobExecutionContextData jobExeContextNode = 
					(JobExecutionContextData)_node;
				return jobExeContextNode.Status.ToString();
			}
		}

		public override Guid Key
		{
			get
			{
				JobExecutionContextData jobExeContextNode = 
					(JobExecutionContextData)_node;
				return jobExeContextNode.Key;
			}
		}

		public override bool LinkEnabled
		{
			get
			{
				return true;
			} 
		}
	}

	public class WebTree : CollectionBase
	{
		private WebTreeNode _root;

		public static WebTreeNode NewWebTreeNode(IBatchDBData node,
			int depth, 
			bool expanded)
		{
			if (node is BatchExecutionContextData)
			{
				return new ActiveRequestTreeNode(node, depth, expanded);
			}
			else if (node is JobExecutionContextData)
			{
				return new JobExecutionTreeNode(node, depth, expanded);
			}
			else
			{
				return new WebTreeNode(node, depth, expanded);
			}
		}

		public WebTree(IBatchDBData rootConfig)
		{
			_root = WebTree.NewWebTreeNode(rootConfig, 0, true);
			_root.InsertBranch(InnerList, 0);
		}

		public int SetExpansionAt(int index, bool expanded)
		{
			CheckIndexRange( index );
			System.Diagnostics.Debug.WriteLine(index.ToString() + ":" + expanded);

			int originalCount = InnerList.Count;
			WebTreeNode branch = (WebTreeNode)InnerList[index];
			branch.Expanded = expanded;
			if (branch.Expanded == false)
			{
                //colasping the node
				index++;
				while (index < InnerList.Count)
				{
					WebTreeNode node = (WebTreeNode)InnerList[index];
					if (node.Depth <= branch.Depth)
					{
						break;
					}			
					// removing the node
					InnerList.RemoveAt(index);
				}
			}
			else
			{
				//expanding the node
				branch.InsertBranch(InnerList, index+1);
			}
			return InnerList.Count - originalCount;
		}

		public void ToggleAt(int index)
		{
			CheckIndexRange( index );
			WebTreeNode branch = (WebTreeNode)InnerList[index];

			SetExpansionAt(index, !branch.Expanded);
		}


		public void Add( WebTreeNode node )
		{
			InnerList.Add( node );
		}

		private void CheckIndexRange(int index)
		{
			if (index >= InnerList.Count)
			{
				throw new IndexOutOfRangeException(
					string.Format("Index requested: {0}, total count: {1}.", index, InnerList.Count));				
			}
		}

		public object GetConfigAt(int index)
		{
			CheckIndexRange( index );
			WebTreeNode treeNode = (WebTreeNode)InnerList[index];
			if (treeNode != null)
			{
				return treeNode.Config;
			}
			return null;
		}

//		/// <summary>
//		/// Not used
//		/// </summary>
//		/// <param name="index"></param>
//		/// <returns></returns>
//		public ArrayList GetConfigProperties(int index)
//		{
//			CheckIndexRange( index );
//
//			WebTreeNode node = (WebTreeNode)InnerList[index];
//			if (node != null)
//			{
//				return NodePropertyMappings.GetProperties(node.Config);
//			}
//			else
//			{
//				return new ArrayList();
//			}
//		}

		public void GetState(StateBag states)
		{
			//ArrayList stateList = new ArrayList();
			object objKeyPreserve = states["ObjectKey"];
			object reqKeyPreserve = states["RequestKey"];
			states.Clear();
			states["ObjectKey"] = objKeyPreserve;
			states["RequestKey"] = reqKeyPreserve;
			for (int i = 0; i < InnerList.Count; i++)
			{
				WebTreeNode node = (WebTreeNode)InnerList[i];
				states[i.ToString()] = node.Expanded;
			}
			states["Total"] = InnerList.Count;
		}

		public WebTreeNode this[int index]
		{
			get 
			{
				CheckIndexRange( index );
				return (WebTreeNode)InnerList[index];
			}
		}

		public WebTreeNode this[Guid key]
		{
			get
			{
				if (key == Guid.Empty)
				{
					return null;
				}
				for (int i = 0; i < InnerList.Count; i++)
				{
					WebTreeNode node = (WebTreeNode)InnerList[i];
					if (node.Key == key)
					{
						return node;
					}
				}
				return null;
			}
		}

		public void MoveUpHead()
		{
			for (int i = 1; i < InnerList.Count; i++)
			{
				WebTreeNode node = (WebTreeNode)InnerList[i];
				node.MoveDown();
			}
		}
	} 
}
