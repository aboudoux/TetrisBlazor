using System;

namespace Tetris.Blazor.Tests
{
	public static class Call
	{
		public static void Action(int time, Action action)
		{
			for (var i = 0; i < time; i++)
				action();
		}

		public static void ActionUntil(Func<bool> predicate, Action action) 
		{
			while (predicate())
				action();
		}
	}
}