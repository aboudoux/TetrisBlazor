using System;

namespace TetrisBlazor.Core
{
	public class BlockMovement
	{
		public Func<int, int> RowMovement { get; }
		public Func<int, int> ColumnMovement { get; }

		public BlockMovement(Func<int, int> rowMovement, Func<int,int> columnMovement)
		{
			RowMovement = rowMovement;
			ColumnMovement = columnMovement;
		}
	}
}