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
			HashSet<Node<State>> openSet = new HashSet<Node<State>>();
			HashSet<int> closedSet = new HashSet<int>();
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
				//from this stack to 'unvisit' nodes - this won't affect regular DFS, only IDS or any depth-limited search.
				while (currentNode.Parent != parents.Peek())
					closedSet.Remove(parents.Pop().Data.GetHashCode());

				parents.Push(currentNode);
				expandedNodes++;
				if(currentNode.Data.Grid.SequenceEqual(endState.Grid))
					return new Solution(tree.BacktrackFrom(currentNode), expandedNodes);

				//if it's not discovered OR it's not at threshold yet, expand it
				if (!closedSet.Contains(currentNode.Data.GetHashCode()) && (threshold == -1 || heuristic(currentNode.Data, endState) + currentNode.Depth() <= threshold))
				{
					closedSet.Add(currentNode.Data.GetHashCode());
					foreach (State state in currentNode.Data.GetAllMoves().Where(s => !closedSet.Contains(s.GetHashCode())))
					{
						Node<State> newNode = new Node<State>(state, currentNode);
						currentNode.Children.Add(newNode);
						workingStack.Push(newNode);
					}
				}
			}

			//failed
			return new Solution(null, expandedNodes, false);
		}
	}
}
