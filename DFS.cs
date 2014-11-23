using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISearch
{
	public partial class SearchMethods
	{
		public static Solution DFS(State startState, State endState, int threshold = -1, Func<State, State, int> heuristic=null)
		{
			Tree<State> tree = new Tree<State>(startState);
			HashSet<int> closedSet = new HashSet<int>();

            //if we're not using a heuristic (i.e. it's (I)DFS, not IDA*), our heuristic is nothing. This means we just consider depth for the threshold, not depth+heuristic.
			if (heuristic == null)
				heuristic = ((s, e) => 0);
				
			int expandedNodes = 0;

			Stack<Node<State>> workingStack = new Stack<Node<State>>();
			Stack<Node<State>> parents = new Stack<Node<State>>();

			workingStack.Push(tree.Root);
			parents.Push(null);

			while(workingStack.Count > 0)
			{
				Node<State> currentNode = workingStack.Pop();

				//we keep track of the immediate parent of the working node. If we exhaust a node's child nodes, then proceed to go back up the tree, we pop nodes
				//from this stack to 'unvisit' nodes - this won't affect regular DFS, only depth-limited search.
				while (currentNode.Parent != parents.Peek())
					closedSet.Remove(parents.Pop().Data.GetHashCode());

				parents.Push(currentNode);
				
				if(currentNode.Data.Grid.SequenceEqual(endState.Grid))
					return new Solution(tree.BacktrackFrom(currentNode), expandedNodes);

				//if it's not discovered and either we have no threshold (it's DFS not IDFS/IDA*), or we haven't hit the threshold yet, we expand.
				if (!closedSet.Contains(currentNode.Data.GetHashCode()) && (threshold == -1 || heuristic(currentNode.Data, endState) + currentNode.Depth() <= threshold))
				{
                    //visit this node, then expand it.
					closedSet.Add(currentNode.Data.GetHashCode());
                    expandedNodes++;

					foreach (State state in currentNode.Data.GetAllMoves().Where(s => !closedSet.Contains(s.GetHashCode())))
					{
						Node<State> newNode = new Node<State>(state, currentNode);
						currentNode.Children.Add(newNode);
						workingStack.Push(newNode);
					}
				}
			}

			//failed if we get to here
			return new Solution(null, expandedNodes, false);
		}
	}
}
