using TetrisBlazor.Core;

namespace TetrisBlazor.Tetriminos
{
	public class TetriminoJ :  Tetrimino
	{
		public TetriminoJ(int startColumn = 1)
			: base(HorizontalDownJ(startColumn),
				ToVerticalLeft, ToHorizontalUp, ToVerticalRight, ToHorizontalDown) { }

		private static Shape HorizontalDownJ(int startColumn) => Shape.Create("#0000FF")
			.WithBlocks(
				(1, startColumn),
				(1, startColumn + 1),
				(1, startColumn + 2),
				(2, startColumn + 2));

		private static ShapePositionChanger ToVerticalLeft => ShapePositionChanger.Create()
			.ForRegular(
				(row => row, col => col + 1),
				(row => row + 1, col => col),
				(row => row + 2, col => col - 1),
				(row => row + 1, col => col - 2)
			);

		private static ShapePositionChanger ToHorizontalUp => ShapePositionChanger.Create()
			.ForRegular(
				(row => row + 1, col => col + 1),
				(row => row, col => col),
				(row => row - 1, col => col - 1),
				(row => row - 2, col => col)
			).ForRightBorder(
				(row => row + 1, col => col),
				(row => row, col => col - 1),
				(row => row - 1, col => col - 2),
				(row => row - 2, col => col - 1));

		private static ShapePositionChanger ToVerticalRight => ShapePositionChanger.Create()
			.ForRegular(
				(row => row + 1, col => col - 2),
				(row => row, col => col - 1),
				(row => row - 1, col => col),
				(row => row, col => col + 1)  
			);

		private static ShapePositionChanger ToHorizontalDown => ShapePositionChanger.Create()
			.ForRegular(
				(row => row - 2, col => col),
				(row => row - 1, col => col + 1),
				(row => row, col => col + 2),
				(row => row + 1, col => col + 1)
			).ForRightBorder(
				(row => row - 2, col => col - 1),
				(row => row - 1, col => col),
				(row => row, col => col + 1),
				(row => row + 1, col => col));
	}
}