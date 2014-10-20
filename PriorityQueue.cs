using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISearch
{
	//a pretty barebones p.queue
	class PriorityQueue<T>
	{
		private SortedDictionary<int, List<T>> dict;

		public PriorityQueue()
		{
			dict = new SortedDictionary<int, List<T>>();
		}

		public void Add(int priority, T data)
		{
			if (dict.ContainsKey(priority))
				dict[priority].Add(data);
			else
				dict[priority] = new List<T>(new[] { data });
		}

		public void Remove(int priority, T data)
		{
			dict[priority].Remove(data);
		}

		public T Pop()
		{
			List<int> removals = new List<int>();
			T ret = default(T);

			foreach(var queue in dict)
			{
				if (queue.Value.Count == 0)
				{
					removals.Add(queue.Key);
					continue;
				}
				else
				{
					T item = queue.Value.First();
					queue.Value.Remove(item);
					ret = item;
					break;
				}
			}
			foreach (int i in removals)
				dict.Remove(i);
			return ret;
		}
	}
}
