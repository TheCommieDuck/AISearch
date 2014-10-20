using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISearch
{
	public static class Heuristics
	{
		public static int ManhattanDistance(State current, State end)
		{
			int totalScore = 0;
			//consider every square in the grid which isn't an empty space, and how far said square is
			foreach(char nonEmpty in current.Grid.Where(c => c != '0'))
			{
				int currentIndex = Array.FindIndex(current.Grid, c => c == nonEmpty);
				Position currentPosition = new Position((byte)(currentIndex % current.Width), (byte)(currentIndex / current.Height));
				int endIndex = Array.FindIndex(end.Grid, c => c == nonEmpty);
				Position endPosition = new Position((byte)(endIndex % end.Width), (byte)(endIndex / end.Height));
				totalScore += Math.Abs(endPosition.X - currentPosition.X);
				totalScore += Math.Abs(endPosition.Y - currentPosition.Y);
			}
			return totalScore;
		}
	}
}
