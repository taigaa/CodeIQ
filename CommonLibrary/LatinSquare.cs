using System;
using System.Collections.Generic;
using System.Linq;

namespace Taigaa.CodeIQ
{
	public class LatinSquare
	{
		/// <summary>
		/// 与えられた構成要素から生成されるラテン方格を取得する
		/// 全てのラテン方格は標準形の行列を入れ替えたもので列挙できる
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="elements">構成要素</param>
		/// <returns>ラテン方格の列挙</returns>
		public static IEnumerable<IEnumerable<IEnumerable<T>>> GetLatinSquare<T>(IEnumerable<T> elements)
		{
			// 標準形
			var standard = GetStandardLatinSquare(elements);

			List<IEnumerable<IEnumerable<T>>> squares = new List<IEnumerable<IEnumerable<T>>>();

			foreach (var st in standard)
			{
				// 行方向に列挙
				foreach (var stPermutated in Permutation.Enumerate(st))
				{
					squares.Add(stPermutated);

					// さらに列方向に列挙
					foreach (var chgPermutated in Permutation.Enumerate(GetRowColumnChangeArray(stPermutated)))
					{
						squares.Add(GetRowColumnChangeArray(chgPermutated));
					}
				}
			}

			// 重複を除去
			return squares.Distinct<IEnumerable<IEnumerable<T>>>(new ArrayArrayComparer<T>());
		}

		/// <summary>
		/// 与えられた構成要素から生成されるラテン方格の標準形を取得する
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="elements">構成要素</param>
		/// <returns>ラテン方格標準形の列挙</returns>
		public static IEnumerable<IEnumerable<IEnumerable<T>>> GetStandardLatinSquare<T>(IEnumerable<T> elements)
		{
			var square = InitSquare(elements);

			var firstElements = elements.Where(e => !e.Equals(square.ElementAt(1).ElementAt(0))).ToList();
			var squares = MakeSubSquare(square, 1, new List<T>(), firstElements);

			return squares;
		}

		/// <summary>
		/// ラテン方格標準形の基礎部分を生成して返す
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="elements">構成要素</param>
		/// <returns>ラテン方格標準形基礎</returns>
		private static IEnumerable<IEnumerable<T>> InitSquare<T>(IEnumerable<T> elements)
		{
			T[][] square = new T[elements.Count()][];
			square[0] = elements.OrderBy(n => n).ToArray();
			for (int i = 1; i < square.Length; i++)
			{
				if (square[i] == null) square[i] = new T[elements.Count()];
				square[i][0] = square[0][i];
			}

			return square;
		}

		/// <summary>
		/// ラテン方格標準形の候補を列挙する再帰的メソッド
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="square">作成中のラテン方格</param>
		/// <param name="lineNumber">作成中の行番号</param>
		/// <param name="line">作成作業用リスト</param>
		/// <param name="elements">作成のための構成要素</param>
		/// <returns>ラテン方格標準形</returns>
		private static IEnumerable<IEnumerable<IEnumerable<T>>> MakeSubSquare<T>(IEnumerable<IEnumerable<T>> square,
																				int lineNumber,
																				IEnumerable<T> line,
																				IEnumerable<T> elements)
		{
			if (lineNumber == square.Count())
			{
				yield return square;
			}
			else
			{
				var lines = MakeLine(square, lineNumber, line, elements);
				foreach (var checking in lines.Where(l => l != null))
				{
					List<T> tmp = new List<T>();
					// 作成中行の決定済み位置まで追加
					for (int i = 0; i < square.ElementAt(0).Count() - checking.Count(); i++)
					{
						tmp.Add(square.ElementAt(lineNumber).ElementAt(i));
					}
					// 作成した順列を追加
					for (int i = 0; i < checking.Count(); i++)
					{
						tmp.Add(checking.ElementAt(i));
					}

					// 作成した行を含む形でラテン方格候補として再作成
					List<IEnumerable<T>> newSquare = new List<IEnumerable<T>>();
					for (int i = 0; i < square.Count(); i++)
					{
						if (i == lineNumber)
						{
							newSquare.Add(tmp.ToArray());
						}
						else
						{
							newSquare.Add(square.ElementAt(i));
						}
					}

					// 次の行の構成要素を用意
					List<T> newElements = new List<T>();
					try
					{
						newElements = square.ElementAt(0).Where(e => !e.Equals(square.ElementAt(lineNumber + 1).ElementAt(0))).ToList();
					}
					catch (ArgumentOutOfRangeException) { /* 最終行処理後の行無し対応 */ }

					var result = MakeSubSquare(newSquare, lineNumber + 1, new List<T>(), newElements);
					foreach (var s in result) yield return s;
				}
			}
		}

		/// <summary>
		/// ラテン方格標準形の行の並び候補を列挙する再帰的メソッド
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="square">作成中のラテン方格</param>
		/// <param name="lineNumber">作成中の行番号</param>
		/// <param name="line">作成作業用リスト</param>
		/// <param name="elements">作成のための構成要素</param>
		/// <returns>行候補</returns>
		private static IEnumerable<IEnumerable<T>> MakeLine<T>(IEnumerable<IEnumerable<T>> square, int lineNumber, IEnumerable<T> line, IEnumerable<T> elements)
		{
			if (elements.Count() == 0)
			{
				bool isNG = false;
				for (int i = 0; i < line.Count(); i++)
				{
					// 縦方向重複判定
					for (int l = 0; l < lineNumber; l++)
					{
						if (square.ElementAt(l).ElementAt(square.ElementAt(0).Count() - line.Count() + i).Equals(line.ElementAt(i)))
						{
							isNG = true;
						}
					}
				}
				if (isNG)
				{
					yield return null;
				}
				else
				{
					yield return line;
				}
			}
			else
			{
				foreach (var n in elements)
				{
					var result = MakeLine(square, lineNumber, line.Concat(new T[] { n }), elements.Where(x => !x.Equals(n)));
					foreach (var xs in result) yield return xs;
				}
			}
		}

		/// <summary>
		/// 与えられた行列を入れ替えたもので返す
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="square">入れ替え対象行列(ジャグ配列相当だが実質2次元配列が前提)</param>
		/// <returns>行列入れ替え結果</returns>
		public static IEnumerable<IEnumerable<T>> GetRowColumnChangeArray<T>(IEnumerable<IEnumerable<T>> square)
		{
			T[][] newSquare = new T[square.Count()][];
			for (int i = 0; i < square.Count();i++ )
			{
				if (newSquare[i] == null) newSquare[i] = new T[square.Count()];
				for (int j=0;j<square.Count();j++)
				{
					newSquare[i][j] = square.ElementAt(j).ElementAt(i);
				}
			}
			return newSquare;
		}
	}
}
