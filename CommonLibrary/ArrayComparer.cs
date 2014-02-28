using System.Collections.Generic;
using System.Linq;

namespace Taigaa.CodeIQ
{
	public class ArrayComparer<T> : IEqualityComparer<IEnumerable<T>>
	{
		public bool Equals(IEnumerable<T> array1, IEnumerable<T> array2)
		{
			return array1.SequenceEqual(array2);
		}

		public int GetHashCode(IEnumerable<T> array)
		{
			int hashCode = 0;
			foreach (var t in array)
			{
				hashCode = hashCode ^ t.GetHashCode();
			}
			return hashCode;
		}
	}

	public class ArrayArrayComparer<T> : IEqualityComparer<IEnumerable<IEnumerable<T>>>
	{
		public bool Equals(IEnumerable<IEnumerable<T>> array1, IEnumerable<IEnumerable<T>> array2)
		{
			bool ret = true;
			if (array1.Count() != array2.Count())
			{
				ret = false;
			}
			else
			{
				for (int i = 0;i<array1.Count();i++)
				{
					ret = ret & array1.ElementAt(i).SequenceEqual(array2.ElementAt(i));
				}
			}
			return ret;
		}

		public int GetHashCode(IEnumerable<IEnumerable<T>> array)
		{
			int hashCode = 0;
			foreach (var tt in array)
			{
				foreach (var t in tt)
				{
					hashCode = hashCode ^ t.GetHashCode();
				}
			}
			return hashCode;
		}
	}
}
