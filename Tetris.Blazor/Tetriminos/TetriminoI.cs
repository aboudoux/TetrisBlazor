using TetrisBlazor.Core;

namespace TetrisBlazor.Tetriminos
{
	public class TetriminoI : Tetrimino
	{
		public TetriminoI(int startColumn = 1)
		: base(
			HorizontalBar(startColumn),
			ToVerticalPosition, ToHorizontalPosition
		) { }

		private static Shape HorizontalBar(int startColumn) => Shape.Create("#00FFFF")
			.WithBlocks(
				(1, startColumn), 
				(1, startColumn + 1), 
				(1, startColumn + 2), 
				(1, startColumn + 3));

		private static ShapePositionChanger ToVerticalPosition => ShapePositionChanger.Create()
			.ForRegular(
				(row => row, col => col), 
				(row => row + 1, col => col - 1),
				(row => row + 2, col => col - 2),
				(row => row + 3, col => col - 3));

		private static ShapePositionChanger ToHorizontalPosition => ShapePositionChanger.Create()
			.ForRegular(
				(row => row, col => col), 
				(row => row - 1, col => col + 1),
				(row => row - 2, col => col + 2),
				(row => row - 3, col => col + 3))
			.ForRightBorder(
				(row => row, col => col - 3),
				(row => row - 1, col => col - 2),
				(row => row - 2, col => col - 1),
				(row => row - 3, col => col));
	}
}