using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISearch
{
	public partial class SearchMethods
	{
		public static Solution BFS(State startState, State endState)
		{
            Tree<State> tree = new Tree<State>(startState);
            HashSet<int> closedSet = new HashSet<int>();
            Queue<Node<State>> workingQueue = new Queue<Node<State>>();

            int expandedNodes = 0;
            workingQueue.Enqueue(tree.Root);

            while (workingQueue.Count > 0)
            {
                Node<State> currentNode = workingQueue.Dequeue();
                if (currentNode.Data.Grid.SequenceEqual(endState.Grid))
                    return new Solution(tree.BacktrackFrom(currentNode), expandedNodes);

                if (!closedSet.Contains(currentNode.Data.GetHashCode()))
                {
                    //visit this node, then expand it.
                    closedSet.Add(currentNode.Data.GetHashCode());
                    expandedNodes++;

                    foreach (State state in currentNode.Data.GetAllMoves().Where(s => !closedSet.Contains(s.GetHashCode())))
                    {
                        Node<State> newNode = new Node<State>(state, currentNode);
                        currentNode.Children.Add(newNode);
                        workingQueue.Enqueue(newNode);
                    }
                }
            }
			/*Dictionary<State, State> parents = new Dictionary<State, State>();
			int expandedNodes = 0;

			//we start at depth 0. we have 1 node to consider before we move down a level. Then, we have 
			int depth = 0;
			int nodesUntilNextLevel = 1;
			int pendingNodesUntilNextLevel = 0;
			workingQueue.Enqueue(startState);

			while (workingQueue.Count > 0)
			{
				State currentState = workingQueue.Dequeue();
				expandedNodes++;
				closedSet.Add(currentState.GetHashCode());

				if (currentState.Grid.SequenceEqual(endState.Grid))
				{
					List<State> path = new List<State>();
					State currentBacktracedState = endState;
					while(!currentBacktracedState.Grid.SequenceEqual(startState.Grid))
					{
						path.Add(currentBacktracedState);
						currentBacktracedState = parents[currentBacktracedState];
					}
					path.Add(startState);
					path.Reverse();
					return new Solution(path, expandedNodes);
				}

				IEnumerable<State> nextStates = currentState.GetAllMoves().Where(s => !closedSet.Contains(s.GetHashCode()));

				//all the children we get are going to count the number of states to consider on the next level.
				//we've taken a node from this level. if this was the end of this level, then we move down 1 in the tree.
				pendingNodesUntilNextLevel += nextStates.Count();
				nodesUntilNextLevel--;
				if (nodesUntilNextLevel == 0)
				{
					Console.WriteLine("Moving down a level; next level has {0} nodes", pendingNodesUntilNextLevel);
					depth++;
					nodesUntilNextLevel = pendingNodesUntilNextLevel;
					pendingNodesUntilNextLevel = 0;
				}

				foreach (State nextState in nextStates)
				{

					workingQueue.Enqueue(nextState);
					//we need to consider the state we moved FROM to backtrace a path. 
					//since BFS scans from left to right, the first time we encounter some new state A, with a parent B, then all children of A will be expanded BEFORE any
					//children of A coming from a node with a parent C. So after we find the state, we can ignore it - because if it is part of a path, only the first parent is
					//considered.
					if(!parents.ContainsKey(nextState))
						parents.Add(nextState, currentState);
				}
			}*/
			return new Solution(null, expandedNodes, false);
		}
	}
}
