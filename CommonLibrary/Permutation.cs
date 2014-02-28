using System.Collections.Generic;
using System.Linq;

namespace Taigaa.CodeIQ
{
	/// <summary>
	/// 順列を取得するためのクラス
	/// 引用：http://gushwell.ifdef.jp/etude/Permutation.html
	/// </summary>
	public class Permutation
	{
		public static IEnumerable<IEnumerable<T>> Enumerate<T>(IEnumerable<T> nums)
		{
			return GetPermutations<T>(new List<T>(), nums.ToList());
		}

		private static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> perm, IEnumerable<T> nums)
		{
			if (nums.Count() == 0)
			{
				yield return perm;
			}
			else
			{
				foreach (var n in nums)
				{
					var result = GetPermutations<T>(perm.Concat(new T[] { n }), nums.Where(x => !x.Equals(n)));
					foreach (var xs in result) yield return xs;
				}
			}
		}
	}
}
