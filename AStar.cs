using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISearch
{
	public partial class SearchMethods
	{
		public static Solution AStar(State startState, State endState, Func<State, State, int> heuristic)
		{
			//AStar is just a GBFS which considers depth as well as the heuristic
			return SearchMethods.GBFS(startState, endState, heuristic, true);
		}
	}
}
