using TetrisBlazor.Core;

namespace Tetris.Blazor.Tests
{
	public class FakeBlock : IBlock
	{
		public FakeBlock(int row, int column)
		{
			Row = row;
			Column = column;
			HexColor = "#000000";
		}
		public int Row { get; }
		public int Column { get; }
		public string HexColor { get; }
	}
}