using Taigaa.CodeIQ;
using System;
using System.Linq;

namespace Taigaa.CodeIQ.LatinSquareAnagrams
{
	class Program
	{
		static void Main(string[] args)
		{
			System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
			sw.Start();


			//var elements = new[] { 0, 1, 2 };
			//var elements = new[] { 0, 1, 2, 3 };
			//var elements = new[] { 0, 1, 2, 3, 4 };
			//var elements = new[] { 0, 1, 2, 3, 4, 5 };
			var elements = new[] { 'u', 's', 'e', 'r' };
			//var elements = new[] { 'a', 'l', 'e', 'r', 't' };

			//var squares = LatinSquare.GetStandardLatinSquare(elements);
			var squares = LatinSquare.GetLatinSquare(elements);

			// 単語として意味をなさないものを除く
			/*
			 * 【{a, e, l, s, t}の辞書】
			 * steal = 盗む
			 * stela = 記念碑
			 * telas = スペインの織物
			 * teals = コガモ（複数形）
			 * elast = 柔軟弾力を意味する接頭辞
			 * least = 最小
			 * laste = lassen(溶接する)というオランダ語の仮定法(出典)
			 * astel = 差し矢
			 */
			//var dictionary = new string[] { "steal", "stela", "telas", "teals", "elast", "least", "laste", "astel" };
			//var dictionary = new string[] { "user", "rues", "ruse", "sure" };
			var dictionary = new string[] { "user", "erus", "resu", "sure" };

			// 行でチェック
			var lineChecked = squares.Select(ss => ss.Where(s => dictionary.Contains(new String(s.ToArray())))
													 .Where(s => s.Count() != 0))
									 .Where(ss => ss.Count() == squares.ElementAt(0).Count());

			// 列でチェック
			var rowCheckd = lineChecked.Select(ss => LatinSquare.GetRowColumnChangeArray(ss))
								       .Select(ss => ss.Where(s => dictionary.Contains(new String(s.ToArray())))
												       .Where(s => s.Count() != 0))
								       .Where(ss => ss.Count() == squares.ElementAt(0).Count());

			// 結果表示
			foreach (var square in rowCheckd)
			{
				foreach (var line in square)
				{
					foreach (var element in line)
					{
						Console.Write("{0}", element);
					}
					Console.WriteLine();
				}
				Console.WriteLine();
			}


			sw.Stop();
			Console.WriteLine("経過時間 = {0}", sw.Elapsed);

			Console.ReadLine();
		}
	}
}
