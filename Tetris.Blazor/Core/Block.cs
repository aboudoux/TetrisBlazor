using System;

namespace TetrisBlazor.Core
{
	public struct Block : IBlock
	{
		public Block(int row, int column, string hexColor)
		{
			Row = row;
			Column = column;
			HexColor = hexColor;
		}
		public int Row { get; }
		public int Column { get; }
		public string HexColor { get; }

		public Block MoveRight() => new Block(Row, Column + 1, HexColor);
		public Block MoveLeft() => new Block(Row, Column - 1, HexColor);
		public Block MoveDown(int count = 1) => new Block(Row + count, Column, HexColor);

		public Block ChangePosition(BlockMovement movement) 
			=> new Block(movement.RowMovement(Row), movement.ColumnMovement(Column), HexColor);
	}
}