using TetrisBlazor.Core;

namespace TetrisBlazor.Tetriminos
{
	public class EmptyTetrimino :Tetrimino
	{
		public EmptyTetrimino() : base(Shape.Empty, ShapePositionChanger.Empty)
		{
		}
	}
}