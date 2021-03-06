﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISearch
{
	class Program
	{
		public static Random Random;
		static void Main(string[] args)
		{
            //it works.
			Random = new System.Random();

			State startState = new State(4, new char[]{
				'0', '0', '0', '0',
				'0', '0', '0', '0',
				'0', '0', '0', '0',
				'A', 'B', 'C', '☺'
				});

			State endState = new State(4, new char[]{
				'0', '0', '0', '0',
				'0', 'A', '0', '0',
				'0', 'B', '0', '0',
				'0', 'C', '0', '☺'
				});

			for (int i = 0; i < 1; ++i)
			{
				Solution sol = SearchMethods.BFS(startState, endState);
				foreach (string str in sol.GetPath())
					Console.WriteLine(str);
                Console.ReadLine();
			}
			Console.ReadLine();
		}
	}
}
