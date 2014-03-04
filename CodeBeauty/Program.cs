using System;

namespace Taigaa.CodeIQ.CodeBeauty
{
	/// <summary>
	/// Microsoft Visual C# 2013
	/// 
	/// 10進数を2進数表記に変換し、
	/// その文字列を3進数とみなし10進数にもどすと解が得られます。
	/// これを必要数分10進数でインクリメントしながら求めていきます。
	/// </summary>
	class Program
	{
		static void Main(string[] args)
		{
			for (int n = 1; n <= 100; n++)
			{
				string bitString = Convert.ToString(n, 2);
				int number = 0;
				for (int k = 0; k < bitString.Length; k++)
				{
					number += (int)((bitString[k] == '1') ? Math.Pow(3, bitString.Length - k - 1) : 0);
				}
				Console.WriteLine(number);
			}

			Console.ReadLine();
		}
	}
}
