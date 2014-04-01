using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace Taigaa.CodeIQ.UglyDuckCode
{
	/// <summary>
	/// 文字列の数式を演算する方法について
	/// 参考：http://whoopsidaisies.hatenablog.com/entry/2013/12/11/151652
	/// // xFunc.Maths - Apache License, Version 2.0
	/// </summary>
	class Program
	{
		// 使用できる演算子
		private static readonly ReadOnlyCollection<string> Operators = Array.AsReadOnly(new string[] { "", "+", "-" });
		//private static readonly ReadOnlyCollection<string> Operators = Array.AsReadOnly(new string[] { "", "+", "-", "*", "/" });

		static void Main(string[] args)
		{
			string[] textLines = File.ReadAllLines("../../magic_number.txt", Encoding.Default);
			foreach (var numberString in textLines)
			{
				var answer = GetMagicNumberCalculatioinAnswers(numberString).Count();
				Console.WriteLine("鍵：{0}", answer);
			}

			Console.ReadLine();
		}


		/// <summary>
		/// 箱に書かれた数字から鍵を求めて返す
		/// </summary>
		/// <param name="magicNumberString">箱に書かれた数字</param>
		/// <returns>鍵の条件を満たすリスト</returns>
		private static IEnumerable<double> GetMagicNumberCalculatioinAnswers(string magicNumberString)
		{
			// 1桁の素数リストを取得
			var primeNumbers = CodeIQ.SieveOfEratosthenes.GetPrimeNumbers(10);

			// 演算方法のパターン数
			int patternCount = (int)Math.Pow(Operators.Count(), magicNumberString.Length - 1);

			// n進数表記文字列の桁数フォーマット(nは演算子の数)
			var formatBuilder = new StringBuilder();
			for (int i = 0; i < magicNumberString.Length - 1; i++)
			{
				formatBuilder.Append("0");
			}

			var answerList = new List<double>();
			for (int k = 0; k < patternCount; k++)
			{
				var calcResultList = new List<double>();

				// 前0付きでn進数表記に変換し演算子パターンを取得(nは演算子の数)
				string ternary = int.Parse(ConvertNotationToString(k, Operators.Count())).ToString(formatBuilder.ToString());

				// 数式の生成
				var calculusBuilder = new StringBuilder();
				for (int i = 0; i < magicNumberString.Length - 1; i++)
				{
					calculusBuilder.Append(magicNumberString[i]);
					calculusBuilder.Append(Operators[Convert.ToInt16(ternary[i]) - Convert.ToInt16('0')]);
				}
				calculusBuilder.Append(magicNumberString[magicNumberString.Length - 1]);


				// 演算、負数は除外
				var calculus = new xFunc.Maths.Parser().Parse(calculusBuilder.ToString());
				try
				{
					double result = double.Parse(calculus.Calculate().ToString());
					if (result > 0)
					{
						calcResultList.Add(result);
					}
				}
				catch (Exception)
				{
					continue;
				}


				// 素数で割り切れる数に絞る
				foreach (var prime in primeNumbers)
				{
					foreach (var primeMultiple in calcResultList.Where(p => p%prime == 0))
					{
						answerList.Add(primeMultiple);
					}
				}

			}

			return answerList.Distinct();
		}


		/// <summary>
		/// 与えられた10進数を指定の基数を表す文字列にして返す
		/// </summary>
		/// <param name="value">10進数</param>
		/// <param name="toBase">基数</param>
		/// <returns>変換後の文字列</returns>
		private static string ConvertNotationToString(int value, int toBase)
		{
			var buf = new StringBuilder();
			while (value >= toBase)
			{
				buf.Insert(0, value % toBase);
				value /= toBase;
			}
			buf.Insert(0, value);

			return buf.ToString();
		}
	}
}
