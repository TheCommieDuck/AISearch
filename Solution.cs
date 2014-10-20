using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISearch
{
	public class Solution
	{
		public List<State> SolutionPath { get; private set; }

		public bool WasSuccess { get; private set; }

		public int ExpandedNodes { get; private set; }

		public Solution(Stack<State> path, int expandedNodes, bool success=true)
		{
			if(success)
				SolutionPath = new List<State>(path.Reverse());
			WasSuccess = success;
			ExpandedNodes = expandedNodes;
		}

		public Solution(List<State> path, int expandedNodes)
		{
			SolutionPath = path;
			ExpandedNodes = expandedNodes;
			WasSuccess = true;
		}

		public IEnumerable<string> GetPath()
		{
			//return the first state (i.e. start state)
			//return a string describing the move to be taken, then the state transitioned to
			//return a string saying the final state has been reached, along with the number of nodes considered and the depth of the given solution
			if (!WasSuccess)
			{
				yield return "No solution found.";
				yield break;
			}

			yield return SolutionPath.First().ToString();

			State lastState = SolutionPath.First();
			foreach(State nextState in SolutionPath.Skip(1))
			{
				yield return String.Format("Moving {0}, to arrive at the state: \n{1}", GetMoveFrom(lastState, nextState), nextState);
				lastState = nextState;
			}
			yield return String.Format("Final state reached. Total nodes expanded: {0}. Depth of tree where solution found: {1}", ExpandedNodes, SolutionPath.Count);
		}

		public string GetMoveFrom(State from, State to)
		{
			//the move is going to be from <agent location in from> to <agent location in to>
			return String.Format("from {0} to {1}", from.AgentLocation, to.AgentLocation);
		}
	}
}
