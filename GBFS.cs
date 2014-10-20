using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISearch
{
	public partial class SearchMethods
	{
		public static Solution GBFS(State startState, State endState, Func<State, State, int> heuristic, bool considerDepth=false)
		{
			Tree<State> tree = new Tree<State>(startState);
			HashSet<Node<State>> openSet = new HashSet<Node<State>>();
			PriorityQueue<Node<State>> fringe = new PriorityQueue<Node<State>>();
			HashSet<int> closedSet = new HashSet<int>();

			Node<State> currentNode = tree.Root;
			int expandedNodes = 0;
			while(!currentNode.Data.Equals(endState))
			{
				//expand this node and add it to the closed set + remove it from the open set
				expandedNodes++;
				closedSet.Add(currentNode.Data.GetHashCode());
				openSet.Remove(currentNode);

				//consider all nodes we can get from here that are not in the closed set
				IEnumerable<Node<State>> successors = currentNode.Data.GetAllMoves().Where(s => !closedSet.Contains(s.GetHashCode()))
					.Select(d => new Node<State>(d, currentNode));

				//add them to the tree, and the open set
				//also keep track of the new nodes
				foreach(Node<State> successor in successors)
				{
					currentNode.Children.Add(successor);
					openSet.Add(successor);
					fringe.Add(heuristic(successor.Data, endState) + (considerDepth? successor.Depth() : 0), successor);
				}

				//get the first node that has a heuristic value equal to the lowest heuristic value in the fringe and make that our current node
				currentNode = fringe.Pop();

				//Console.WriteLine("Considering the node with state\n{0} and the best move has cost {1}", currentNode.Data.ToString(), heuristic(currentNode.Data, endState));
			}
			Console.WriteLine("depth is {0}", tree.BacktrackFrom(currentNode).Count());
			//so now we've reached the final state, so return the solution
			return new Solution(tree.BacktrackFrom(currentNode), expandedNodes);
		}
	}
}
