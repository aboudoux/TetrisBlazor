using System;
using System.Collections.Generic;
using System.Linq;

namespace TetrisBlazor.Core
{
	public class RandomList<T>
	{
		public Random Seed = new Random(Guid.NewGuid().GetHashCode());
		private List<T> _elements = new List<T>();

		public RandomList(params T[] param)
		{
			_elements.AddRange(param);
		}

		public T Next() => _elements.ElementAt(Seed.Next(0, _elements.Count));
	}
}