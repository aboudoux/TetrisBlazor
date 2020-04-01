using TetrisBlazor.Core;

namespace TetrisBlazor.Tetriminos
{
	public class TetriminoS :  Tetrimino
	{
		public TetriminoS(int startColumn = 1) 
			: base(
				HorizontalS(startColumn), 
				ToVerticalS, ToHorizontalS) { }

		private static Shape HorizontalS(int startColumn) => Shape.Create("#008000")
			.WithBlocks(
				(1, startColumn + 2),
				(1, startColumn + 1),
				(2, startColumn + 1),
				(2, startColumn));

		private static ShapePositionChanger ToVerticalS => ShapePositionChanger.Create()
			.ForRegular(
				(row => row, col => col - 2),
				(row => row+1, col => col-1),
				(row => row, col => col),
				(row => row+1, col => col + 1)  
			);

		private static ShapePositionChanger ToHorizontalS => ShapePositionChanger.Create()
			.ForRegular(
				(row => row, col => col + 2),
				(row => row - 1, col => col+1),
				(row => row, col => col),
				(row => row - 1, col => col - 1)
			).ForRightBorder(
				(row => row, col => col + 1),
				(row => row - 1, col => col),
				(row => row, col => col-1),
				(row => row - 1, col => col - 2))
		;
	}
}