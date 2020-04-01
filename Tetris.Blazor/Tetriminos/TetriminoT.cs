using TetrisBlazor.Core;

namespace TetrisBlazor.Tetriminos
{
	public class TetriminoT :  Tetrimino
	{
		public TetriminoT(int startColumn = 1)
			: base(HorizontalDownT(startColumn),
				ToVerticalLeft, ToHorizontalUp, ToVerticalRight, ToHorizontalDown) { }

		private static Shape HorizontalDownT(int startColumn) => Shape.Create("#663399")
			.WithBlocks(
				(1, startColumn),
				(1, startColumn + 1),
				(1, startColumn + 2),
				(2, startColumn + 1));

		private static ShapePositionChanger ToVerticalLeft => ShapePositionChanger.Create()
			.ForRegular(
				(row => row, col => col + 1),
				(row => row + 1, col => col),
				(row => row + 2, col => col - 1),
				(row => row, col => col - 1)
			);

		private static ShapePositionChanger ToHorizontalUp => ShapePositionChanger.Create()
			.ForRegular(
				(row => row + 1, col => col + 1),
				(row => row, col => col),
				(row => row - 1, col => col - 1),
				(row => row - 1, col => col + 1)
			).ForRightBorder(
				(row => row + 1, col => col),
				(row => row, col => col - 1),
				(row => row - 1, col => col - 2),
				(row => row - 1, col => col));

		private static ShapePositionChanger ToVerticalRight => ShapePositionChanger.Create()
			.ForRegular(
				(row => row + 1, col => col - 2),
				(row => row, col => col - 1),
				(row => row - 1, col => col),
				(row => row + 1, col => col)  
			);

		private static ShapePositionChanger ToHorizontalDown => ShapePositionChanger.Create()
			.ForRegular(
				(row => row - 2, col => col),
				(row => row - 1, col => col + 1),
				(row => row, col => col + 2),
				(row => row, col => col)
			).ForRightBorder(
				(row => row - 2, col => col - 1),
				(row => row - 1, col => col),
				(row => row, col => col + 1),
				(row => row, col => col - 1));
	}
}