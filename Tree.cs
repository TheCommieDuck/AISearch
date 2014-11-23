using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISearch
{
	public class Node<T>
	{
		public List<Node<T>> Children { get; set; }

		public T Data { get; set; }

		public Node<T> Parent { get; set; }

		public Node(T data, Node<T> parent)
		{
			Parent = parent;
			Data = data;
			Children = new List<Node<T>>();
		}

		public bool IsLeaf()
		{
			return Children.Count == 0;
		}

		public int Depth()
		{
			return (Parent == null) ? 0 : Parent.Depth() + 1;
		}
	}

	public class Tree<T>
	{
		public Node<T> Root { get; set; }

		public Tree(T root)
		{
			Root = new Node<T>(root, null);
		}

        //note: never used due to speed issues, but keeping for legacy reasons
		public IEnumerable<Node<T>> GetFringe()
		{
			//depth first, not that it matters
			Node<T> currentNode = Root;
			Stack<Node<T>> otherNodes = new Stack<Node<T>>();
			otherNodes.Push(currentNode);
			while(otherNodes.Count > 0)
			{
				Node<T> poppedNode = otherNodes.Pop();
				if (poppedNode.IsLeaf())
					yield return poppedNode;
				else
				{
					foreach (Node<T> child in poppedNode.Children)
						otherNodes.Push(child);
				}
			}
		}

		public List<T> BacktrackFrom(Node<T> node)
		{
			Node<T> currentNode = node;
			List<T> path = new List<T>();
			while(currentNode.Parent != null)
			{
				path.Add(currentNode.Data);
				currentNode = currentNode.Parent;
			}
			path.Reverse();
			return path;
		}
	}
}
