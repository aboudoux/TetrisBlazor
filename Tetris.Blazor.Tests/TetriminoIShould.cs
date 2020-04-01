using FluentAssertions;
using TetrisBlazor.Core;
using TetrisBlazor.Tetriminos;
using Xunit;

namespace Tetris.Blazor.Tests {
	public class TetriminoIShould 
	{
		[Fact]
		public void BeInitialized() 
		{
			var shape = new TetriminoI();
			Check.That(shape).IsEquivalentTo((1, 1), (1, 2), (1, 3), (1, 4));
		}

		[Theory]
		[InlineData(1, 2,3,4,5)]
		[InlineData(2, 3,4,5,6)]
		[InlineData(5, 6,7,8,9)]
		[InlineData(6, 7,8,9,10)]
		[InlineData(7, 8,9,10,11)]
		[InlineData(8, 9,10,11,12)]
		[InlineData(9, 9,10,11,12)]
		[InlineData(10, 9,10,11,12)]
		[InlineData(20, 9,10,11,12)]
		public void MoveRight(int actionCount, int col1, int col2, int col3, int col4)
		{
			var shape = new TetriminoI();
			Call.Action(actionCount, () => shape.MoveRight());
			Check.That(shape).IsEquivalentTo((1, col1), (1, col2), (1, col3), (1, col4));
		}

		[Theory]
		[InlineData(1, 6, 7, 8, 9)]
		[InlineData(2, 5, 6, 7, 8)]
		[InlineData(3, 4, 5, 6, 7)]
		[InlineData(4, 3, 4, 5, 6)]
		[InlineData(5, 2, 3, 4, 5)]
		[InlineData(6, 1, 2, 3, 4)]
		[InlineData(7, 1, 2, 3, 4)]
		[InlineData(10, 1, 2, 3, 4)]
		public void MoveLeft(int actionCount, int col1, int col2, int col3, int col4)
		{
			var shape = new TetriminoI(7);
			Call.Action(actionCount, () => shape.MoveLeft());
			Check.That(shape).IsEquivalentTo((1, col1), (1, col2), (1, col3), (1, col4));
		}
		private class TurnOneTimeData : TheoryData<int,FakeShape>
		{
			public TurnOneTimeData()
			{
				Add(1, Some.Shape.WithBlocks((1, 1), (2, 1), (3, 1), (4, 1)));
				Add(2, Some.Shape.WithBlocks((1, 2), (2, 2), (3, 2), (4, 2)));
				Add(3, Some.Shape.WithBlocks((1, 3), (2, 3), (3, 3), (4, 3)));
				Add(9, Some.Shape.WithBlocks((1, 9), (2, 9), (3, 9), (4, 9)));
			}
		}

		[Theory]
		[ClassData(typeof(TurnOneTimeData))]
		public void TurnOneTime(int start, FakeShape expected)
		{
			var shape = new TetriminoI(start);
			shape.Turn();
			Check.That(shape).IsEquivalentTo(expected);
		}

		[Fact]
		public void TurnTwoTime()
		{
			var shape = new TetriminoI();
			Call.Action(4, () => shape.Turn());
			Check.That(shape).IsEquivalentTo((1, 1), (1, 2), (1, 3), (1, 4));
		}

		[Fact]
		public void TurnCorrectlyWhenReachedRightSideInVerticalPosition()
		{
			var shape = new TetriminoI(9);
			shape.Turn();
			Call.Action(5, () => shape.MoveRight());
			shape.Turn();
			Check.That(shape).IsEquivalentTo((1, 9), (1, 10), (1, 11), (1, 12));
		}

		[Fact]
		public void TurnCorrectlyWhenReachedLeftSideInVerticalPosition() 
		{
			var shape = new TetriminoI();
			shape.Turn();
			Call.Action(2, () => shape.MoveLeft());
			shape.Turn();
			Check.That(shape).IsEquivalentTo((1, 1), (1, 2), (1, 3), (1, 4));
		}

		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(2)]
		public void DontTurnWhenReachedBottomInAnyPosition(int bottomPadding)
		{
			var shape = new TetriminoI();
			var height = Game.Height - bottomPadding;
			Call.Action(height-1, () => shape.MoveDown());
			shape.Turn();
			Check.That(shape).IsEquivalentTo((height, 1), (height, 2), (height, 3), (height, 4));
		}

		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(2)]
		public void DontBugWhenTryingToChangePositionTwiceOnBottom(int bottomPadding)
		{
			var shape = new TetriminoI();
			var height = Game.Height - bottomPadding;
			Call.Action(height - 1, () => shape.MoveDown());
			shape.Turn();
			shape.Turn();
			Check.That(shape).IsEquivalentTo((height, 1), (height, 2), (height, 3), (height, 4));
		}
	}
}
