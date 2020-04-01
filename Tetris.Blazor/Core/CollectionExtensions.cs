using System;
using System.Collections.Generic;

namespace TetrisBlazor.Core
{
	public static class CollectionExtensions
	{
		public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
		{
			foreach (var item in source)
				action(item);
		}
	}
}