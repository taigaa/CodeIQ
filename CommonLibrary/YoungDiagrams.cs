using System.Collections.Generic;

namespace Taigaa.CodeIQ
{
	/// <summary>
	/// ヤング図形を現すオブジェクトクラス
	/// 参考：http://www.cnw.ne.jp/~dr-j/youngue.html
	/// </summary>
	public class YoungDiagrams
	{
		/// <summary>
		/// 分割パターンのリスト
		/// </summary>
		public List<int[]> PartedNumbersPatternList
		{
			get;
			private set;
		}

		/// <summary>
		/// 分割パターン数
		/// </summary>
		public int PatternMount
		{
			get;
			private set;
		}

		private int[] box;
		private int counter;
		private int height; // 図形を積む高さ

		/// <summary>
		/// ヤング図形オブジェクトを生成するコンストラクタ
		/// </summary>
		/// <param name="partingNumber">分割対象自然数</param>
		public YoungDiagrams(int partingNumber)
		{
			PartedNumbersPatternList = new List<int[]>();
			this.box = new int[partingNumber];
			this.counter = 0;
			this.height = 1;
			while (this.height <= partingNumber)
			{
				GetPartition(this.height, partingNumber, partingNumber);
				this.height++;
			}
			PatternMount = this.counter;
		}

		/// <summary>
		/// 分割された数値を取得する再帰的メソッド。
		/// </summary>
		/// <param name="dimension">分割対象の次元</param>
		/// <param name="partMount">分割数</param>
		/// <param name="partingNumber">分割対象自然数</param>
		private void GetPartition(int dimension, int partMount, int partingNumber)
		{
			if (dimension > 0)
			{
				var upper = partMount - dimension + 1;
				var lower = ((partMount - 1) / dimension) + 1;
				for (int i = (partingNumber > upper) ? upper : partingNumber; i >= lower; i--)
				{
					this.box[this.height - dimension] = i;
					GetPartition(dimension - 1, partMount - i, i);
				}
			}
			else
			{
				PartedNumbersPatternList.Add((int[])this.box.Clone());
				this.counter++;
			}
		}
	}
}
