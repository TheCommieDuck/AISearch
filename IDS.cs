using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISearch
{
	public partial class SearchMethods
	{
		public static Solution IDS(State startState, State endState, Func<State, State, int> heuristic = null)
		{
			Solution foundSolution = null;
			int expandedNodes = 0;
			int depthLimit = 0;

			while (foundSolution == null || !foundSolution.WasSuccess)
			{
				foundSolution = SearchMethods.DFS(startState, endState, depthLimit, heuristic);
				expandedNodes += foundSolution.ExpandedNodes;
				depthLimit++;
			}

			return new Solution(foundSolution.SolutionPath, expandedNodes);
		}
	}
}
