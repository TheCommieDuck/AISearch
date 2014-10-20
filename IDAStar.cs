using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISearch
{
	public partial class SearchMethods
	{
		public static Solution IDAStar(State startState, State endState, Func<State, State, int> heuristic)
		{
			return IDS(startState, endState, heuristic);
		}
	}
}
