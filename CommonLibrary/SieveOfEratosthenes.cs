using System;
using System.Collections.Generic;
using System.Linq;

namespace Taigaa.CodeIQ
{
	/// <summary>
	/// 素数の計算(エラトステネスのふるい)
	/// 引用：http://gushwell.ifdef.jp/etude/PrimeNumber.html
	/// </summary>
	public class SieveOfEratosthenes
	{
		/// <summary>
		/// 指定数値より小さい素数を取得する
		/// </summary>
		/// <param name="maxnum">調査上限値</param>
		/// <returns>素数の</returns>
		public static IEnumerable<int> GetPrimeNumbers(int maxnum)
		{
			int[] primes = Enumerable.Range(0, maxnum + 1).ToArray();
			primes[1] = -1;  // -1 : 素数ではない
			int squareroot = (int)Math.Sqrt(maxnum);
			for (int i = 2; i <= squareroot; i++)
			{
				if (primes[i] > 0)
				{
					for (int n = i * 2; n <= maxnum; n += i)
					{
						primes[n] = -1;
					}
				}
			}
			return primes.Where(n => n > 0);
		}
	}
}
