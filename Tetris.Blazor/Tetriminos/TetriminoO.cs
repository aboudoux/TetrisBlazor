using TetrisBlazor.Core;

namespace TetrisBlazor.Tetriminos
{
	public class TetriminoO : Tetrimino {
		public TetriminoO(int startColumn = 1) 
			: base(Square(startColumn), ShapePositionChanger.Empty)
		{
		}

		private static Shape Square(int startColumn) => Shape.Create("#FFFF00").WithBlocks(
			(1, startColumn), 
			(1, startColumn + 1),
			(2, startColumn),
			(2, startColumn + 1));
	}
}