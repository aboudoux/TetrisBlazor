using System.Collections.Generic;
using System.Linq;

namespace TetrisBlazor.Core
{
	public class CircularList<T>
	{
		private readonly Queue<T> _positionsQueue = new Queue<T>();

		public void AddRange(IEnumerable<T> positionChanger) => positionChanger.ForEach(a=>_positionsQueue.Enqueue(a));

		public T Next()
		{
			var element = _positionsQueue.Dequeue();
			_positionsQueue.Enqueue(element);
			return element;
		}

		public void Revert()
		{
			if(!_positionsQueue.Any())
				return;

			var tempList = _positionsQueue.ToList();

			var firstElement = tempList.Last();
			tempList.RemoveAt(tempList.Count - 1);

			_positionsQueue.Clear();
			_positionsQueue.Enqueue(firstElement);
			tempList.ForEach(a=>_positionsQueue.Enqueue(a));
		}
	}
}