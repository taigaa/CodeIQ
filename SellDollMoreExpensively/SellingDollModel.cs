using System;

namespace Taigaa.CodeIQ.SellDollMoreExpensively
{
	/// <summary>
	/// フィギュアの売却モデル
	/// </summary>
	class SellingDollModel
	{
		private static int[] _prices = { 0, 1, 6, 8, 10, 17, 18, 20, 24, 26, 30 };

		/// <summary>
		/// セット数に応じた売却額(万円)
		/// 例:Prices[2]は2体セットの売却額
		/// </summary>
		public static int[] Prices
		{
			get
			{
				return _prices;
			}
		}

		/// <summary>
		/// 合計額
		/// </summary>
		public int TotalPrice
		{
			get;
			private set;
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="splitSetPattern">売却する分割パターン</param>
		public SellingDollModel(int[] splitSetPattern)
		{
			TotalPrice = 0;
			foreach (int setMount in splitSetPattern)
			{
				try
				{
					TotalPrice += Prices[setMount];
				}
				catch (IndexOutOfRangeException)
				{
					// セット価格の定義を超える場合は合計額を算出しない
					TotalPrice = -1;
					break;
				}
			}
		}
	}
}
