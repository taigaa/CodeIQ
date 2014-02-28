using System;

namespace Taigaa.CodeIQ.MiniTest
{
	delegate void PrintSpendTime(int time);
	class Question2
	{
		static void Main(string[] args)
		{
			PrintSpendTime printTime = ((int second) => { Console.WriteLine("You spend {0} sec.", second); });
			for (int i = 1; i <= 10; i++)
			{
				printTime(i);
			}

			Console.ReadLine();
		}
	}
}
