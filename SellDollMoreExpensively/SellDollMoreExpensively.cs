using System;
using System.Text;

namespace Taigaa.CodeIQ.SellDollMoreExpensively
{
	class SellDollMoreExpensively
	{
		/// <summary>
		/// 実行用メインメソッド
		/// </summary>
		/// <param name="args">コマンドライン引数</param>
		static void Main(string[] args)
		{
			bool isExecuting = true;
			while (isExecuting)
			{
				var dollMount = 0;
				Console.Write("売却したいフィギュアの数を入力してください(数字以外で終了) : ");
				try
				{
					dollMount = int.Parse(Console.ReadLine());

					// セットの全パターンを取得
					YoungDiagrams ydObject = new YoungDiagrams(dollMount);
					Console.WriteLine("{0}体の分割パターンは{1}種類", dollMount, ydObject.PatternMount);

					// セットのパターンごとに売却額をチェック
					int maxPrice = 0;
					int[] maxPricePattern = null;
					foreach (int[] splitSetPattern in ydObject.PartedNumbersPatternList)
					{
						SellingDollModel sell = new SellingDollModel(splitSetPattern);
						if (sell.TotalPrice > maxPrice)
						{
							// 売却額が今までより高ければ候補替え
							maxPrice = sell.TotalPrice;
							maxPricePattern = splitSetPattern;
						}
					}
					Console.WriteLine("売却最高額:{0}万円", maxPrice);
					PrintResult(maxPricePattern, maxPrice);
				}
				catch (FormatException)
				{
					isExecuting = false;
				}
			}
		}

		/// <summary>
		/// 結果出力
		/// </summary>
		/// <param name="pricePattern">分割パターン</param>
		/// <param name="TotalPrice">合計額</param>
		private static void PrintResult(int[] pricePattern, int totalPrice)
		{
			StringBuilder setBuf = new StringBuilder("set=");
			StringBuilder priceBuf = new StringBuilder("price=");
			for (int i = 0; i < pricePattern.Length; i++)
			{
				setBuf.Append((i == 0 || pricePattern[i] == 0) ? "" : "+");
				setBuf.Append(pricePattern[i] == 0 ? "" : pricePattern[i].ToString());

				priceBuf.Append((i == 0 || pricePattern[i] == 0) ? "" : "+");
				priceBuf.Append(pricePattern[i] == 0 ? "" : SellingDollModel.Prices[pricePattern[i]].ToString());
			}
			priceBuf.Append("=").Append(totalPrice.ToString());

			Console.WriteLine("{0}\n{1}", setBuf.ToString(), priceBuf.ToString());
		}
	}
}
