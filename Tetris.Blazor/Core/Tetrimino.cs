using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TetrisBlazor.Core 
{
	public abstract class Tetrimino : IEnumerable<IBlock>
	{
		protected Tetrimino(Shape initialShape, params ShapePositionChanger[] positions)
		{
			Shape = initialShape ?? throw new ArgumentNullException(nameof(initialShape));
			if(positions != null)
				CircularPositions.AddRange(positions);
		}

		private Shape Shape { get; }
		private CircularList<ShapePositionChanger> CircularPositions { get; } = new CircularList<ShapePositionChanger>();

		public void MoveRight() => Shape.MoveRight();
		public void MoveLeft() => Shape.MoveLeft();
		public void MoveDown() => Shape.MoveDown();

		public void Turn()
		{
			if (!Shape.ChangePosition(CircularPositions.Next()))
				CircularPositions.Revert();
		}

		public IEnumerator<IBlock> GetEnumerator() => Shape.Cast<IBlock>().GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}
