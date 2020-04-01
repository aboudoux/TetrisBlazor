using System;
using System.Collections.Generic;
using System.Linq;

namespace TetrisBlazor.Core
{
	public class ShapePositionChanger
	{
		public List<BlockMovement> Regular { get; } = new List<BlockMovement>();
		public List<BlockMovement> RightBorder { get; } = new List<BlockMovement>();

		private ShapePositionChanger() { }

		public static ShapePositionChanger Empty => Create();
		public bool IsEmpty() => !Regular.Any() && !RightBorder.Any();
		public static ShapePositionChanger Create() => new ShapePositionChanger();

		public ShapePositionChanger ForRegular(
			(Func<int, int> rowMovement, Func<int, int> columnMovement) block1,
			(Func<int, int> rowMovement, Func<int, int> columnMovement) block2,
			(Func<int, int> rowMovement, Func<int, int> columnMovement) block3,
			(Func<int, int> rowMovement, Func<int, int> columnMovement) block4)
		{
			Regular.Add(new BlockMovement(block1.rowMovement, block1.columnMovement));
			Regular.Add(new BlockMovement(block2.rowMovement, block2.columnMovement));
			Regular.Add(new BlockMovement(block3.rowMovement, block3.columnMovement));
			Regular.Add(new BlockMovement(block4.rowMovement, block4.columnMovement));
			ForRightBorder(block1, block2, block3, block4);
			return this;
		}

		public ShapePositionChanger ForRightBorder(
			(Func<int, int> rowMovement, Func<int, int> columnMovement) block1,
			(Func<int, int> rowMovement, Func<int, int> columnMovement) block2,
			(Func<int, int> rowMovement, Func<int, int> columnMovement) block3,
			(Func<int, int> rowMovement, Func<int, int> columnMovement) block4) 
		{
			RightBorder.Clear();
			RightBorder.Add(new BlockMovement(block1.rowMovement, block1.columnMovement));
			RightBorder.Add(new BlockMovement(block2.rowMovement, block2.columnMovement));
			RightBorder.Add(new BlockMovement(block3.rowMovement, block3.columnMovement));
			RightBorder.Add(new BlockMovement(block4.rowMovement, block4.columnMovement));
			return this;
		}
	}
}