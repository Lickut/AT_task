using System;
using System.Collections.Generic;
using System.Linq;

namespace AT_task
{
	public class OrderUtils
	{
		public static bool IsSorted<T> (IEnumerable<T> list, IComparer<T> comparer)
		{
			var y = list.First ();
			return list.Skip (1).All (x => {
				bool b = comparer.Compare (x, y) <= 0;
				y = x;
				return b;
			});	
		}
	}

	public class sortDateDescending: IComparer<DateTime>
	{
		int IComparer<DateTime>.Compare (DateTime a, DateTime b)
		{
			if (a > b)
				return 1;
			if (a < b)
				return -1;
			else
				return 0;
		}
	}

	public class sortDateAscending: IComparer<DateTime>
	{
		int IComparer<DateTime>.Compare (DateTime a, DateTime b)
		{
			if (a < b)
				return 1;
			if (a > b)
				return -1;
			else
				return 0;
		}
	}
}

