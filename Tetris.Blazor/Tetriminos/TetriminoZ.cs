using TetrisBlazor.Core;

namespace TetrisBlazor.Tetriminos
{
	public class TetriminoZ :  Tetrimino
	{
		public TetriminoZ(int startColumn = 1) 
			: base(
				HorizontalZ(startColumn), 
				ToVerticalZ, ToHorizontalZ) { }

		private static Shape HorizontalZ(int startColumn) => Shape.Create("#ff0000")
			.WithBlocks(
				(1, startColumn),
				(1, startColumn + 1),
				(2, startColumn + 1),
				(2, startColumn + 2));

		private static ShapePositionChanger ToVerticalZ => ShapePositionChanger.Create()
			.ForRegular(
				(row => row, col => col + 1),
				(row => row + 1, col => col),
				(row => row, col => col - 1),
				(row => row + 1, col => col - 2)  
			);

		private static ShapePositionChanger ToHorizontalZ => ShapePositionChanger.Create()
			.ForRegular(
				(row => row, col => col - 1),
				(row => row - 1, col => col),
				(row => row, col => col + 1),
				(row => row - 1, col => col + 2)
			).ForRightBorder(
				(row => row, col => col - 2),
				(row => row - 1, col => col -1),
				(row => row, col => col),
				(row => row - 1, col => col + 1)
				);
	}
}