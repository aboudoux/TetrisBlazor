using FluentAssertions;
using TetrisBlazor.Tetriminos;
using Xunit;

namespace Tetris.Blazor.Tests {
	public class TetriminoTShould 
	{
		[Fact]
		public void BeInitialized() 
		{
			var shape = new TetriminoT();
			Check.That(shape).IsEquivalentTo((1, 1), (1,2), (1, 3), (2, 2));
		}

		[Theory]
		[InlineData(1, 2,3,4,3)]
		[InlineData(2, 3,4,5,4)]
		[InlineData(5, 6,7,8,7)]
		[InlineData(6, 7,8,9,8)]
		[InlineData(7, 8,9,10,9)]
		[InlineData(8, 9,10,11,10)]
		[InlineData(9, 10,11,12,11)]
		[InlineData(10, 10, 11, 12, 11)]
		[InlineData(20, 10, 11, 12, 11)]
		public void MoveRight(int actionCount, int col1, int col2, int col3, int col4)
		{
			var shape = new TetriminoT();
			Call.Action(actionCount, () => shape.MoveRight());
			Check.That(shape).IsEquivalentTo((1, col1), (1, col2), (1, col3), (2, col4));
		}

		[Theory]
		[InlineData(1, 6, 7, 8, 7)]
		[InlineData(2, 5, 6, 7, 6)]
		[InlineData(3, 4, 5, 6, 5)]
		[InlineData(4, 3, 4, 5, 4)]
		[InlineData(5, 2, 3, 4, 3)]
		[InlineData(6, 1, 2, 3, 2)]
		[InlineData(7, 1, 2, 3, 2)]
		[InlineData(10, 1, 2, 3, 2)]
		public void MoveLeft(int actionCount, int col1, int col2, int col3, int col4)
		{
			var shape = new TetriminoT(7);
			Call.Action(actionCount, () => shape.MoveLeft());
			Check.That(shape).IsEquivalentTo((1, col1), (1, col2), (1, col3), (2, col4));
		}
		private class TurnOneTimeData : TheoryData<int,FakeShape>
		{
			public TurnOneTimeData()
			{
				Add(1, Some.Shape.WithBlocks((1, 2), (2, 2), (3, 2), (2, 1)));
				Add(2, Some.Shape.WithBlocks((1, 3), (2, 3), (3, 3), (2, 2)));
				Add(3, Some.Shape.WithBlocks((1, 4), (2, 4), (3, 4), (2, 3)));
				Add(9, Some.Shape.WithBlocks((1, 10), (2, 10), (3, 10), (2, 9)));
			}
		}

		[Theory]
		[ClassData(typeof(TurnOneTimeData))]
		public void TurnOneTime(int start, FakeShape expected)
		{
			var shape = new TetriminoT(start);
			shape.Turn();
			Check.That(shape).IsEquivalentTo(expected);
		}

		[Fact]
		public void TurnTwoTime()
		{
			var shape = new TetriminoT();
			Call.Action(2, () => shape.Turn());
			Check.That(shape).IsEquivalentTo((2, 3), (2, 2), (2, 1), (1,2));
		}

		[Fact]
		public void TurnThreeTime() {
			var shape = new TetriminoT();
			Call.Action(3, () => shape.Turn());
			Check.That(shape).IsEquivalentTo((3, 1), (2,1), (1, 1), (2, 2));
		}

		[Fact]
		public void TurnFourTime() {
			var shape = new TetriminoT();
			Call.Action(4,() => shape.Turn());
			Check.That(shape).IsEquivalentTo((1, 1), (1, 2), (1, 3), (2, 2));
		}

		[Fact]
		public void TurnCorrectlyWhenReachedRightSideInVerticalLeftPosition()
		{
			var shape = new TetriminoT(9);
			shape.Turn();
			Call.Action(5, () => shape.MoveRight());
			shape.Turn();
			Check.That(shape).IsEquivalentTo((2,12), (2,11), (2,10), (1, 11));
		}

		[Fact]
		public void TurnCorrectlyWhenReachedRightSideInVerticalRightPosition() 
		{
			var shape = new TetriminoT(9);
			Call.Action(3, () => shape.Turn());
			Call.Action(5, () => shape.MoveRight());
			shape.Turn();
			Check.That(shape).IsEquivalentTo((1, 10), (1, 11), (1, 12), (2, 11));
		}

		[Fact]
		public void TurnCorrectlyWhenReachedLeftSideInVerticalLeftPosition() 
		{
			var shape = new TetriminoT();
			shape.Turn();
			Call.Action(2, () => shape.MoveLeft());
			shape.Turn();
			Check.That(shape).IsEquivalentTo((2, 3), (2, 2), (2, 1), (1, 2));
		}

		[Fact]
		public void TurnCorrectlyWhenReachedLeftSideInVerticalRightPosition() {
			var shape = new TetriminoT();
			Call.Action(3, () => shape.Turn());
			Call.Action(2, () => shape.MoveLeft());
			shape.Turn();
			Check.That(shape).IsEquivalentTo((1, 1), (1, 2), (1, 3), (2, 2));
		}
	}
}
