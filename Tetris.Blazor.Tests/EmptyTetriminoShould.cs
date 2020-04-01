using TetrisBlazor.Tetriminos;
using Xunit;

namespace Tetris.Blazor.Tests
{
	public class EmptyTetriminoShould
	{
		[Fact]
		public void DoAllMoveWithoutError()
		{
			var shape = new EmptyTetrimino();
			shape.MoveDown();
			shape.MoveLeft();
			shape.MoveRight();
			shape.Turn();
		}
	}
}