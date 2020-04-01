using FluentAssertions;
using TetrisBlazor.Tetriminos;
using Xunit;

namespace Tetris.Blazor.Tests {
	public class TetriminoZShould 
	{
		[Fact]
		public void BeInitialized() 
		{
			var shape = new TetriminoZ();
			Check.That(shape).IsEquivalentTo((1, 1), (1, 2), (2, 2), (2, 3));
		}

		[Theory]
		[InlineData(1, 2,3,3,4)]
		[InlineData(2, 3,4,4,5)]
		[InlineData(5, 6,7,7,8)]
		[InlineData(6, 7,8,8,9)]
		[InlineData(7, 8,9,9,10)]
		[InlineData(8, 9,10,10,11)]
		[InlineData(9, 10,11,11,12)]
		[InlineData(10, 10,11,11,12)]
		[InlineData(20, 10,11,11,12)]
		public void MoveRight(int actionCount, int col1, int col2, int col3, int col4)
		{
			var shape = new TetriminoZ();
			for(var i = 0; i < actionCount; i++)
				shape.MoveRight();
			Check.That(shape).IsEquivalentTo((1, col1), (1, col2), (2, col3), (2, col4));
		}

		[Theory]
		[InlineData(1, 6, 7, 7, 8)]
		[InlineData(2, 5, 6, 6, 7)]
		[InlineData(3, 4, 5, 5, 6)]
		[InlineData(4, 3, 4, 4, 5)]
		[InlineData(5, 2, 3, 3, 4)]
		[InlineData(6, 1, 2, 2, 3)]
		[InlineData(7, 1, 2, 2, 3)]
		[InlineData(10, 1, 2, 2, 3)]
		public void MoveLeft(int actionCount, int col1, int col2, int col3, int col4)
		{
			var shape = new TetriminoZ(7);
			for (var i = 0; i < actionCount; i++)
				shape.MoveLeft();
			Check.That(shape).IsEquivalentTo((1, col1), (1, col2), (2, col3), (2, col4));
		}
		private class TurnOneTimeData : TheoryData<int,FakeShape>
		{
			public TurnOneTimeData()
			{
				Add(1, Some.Shape.WithBlocks((1, 2), (2, 2), (2, 1), (3, 1)));
				Add(2, Some.Shape.WithBlocks((1, 3), (2, 3), (2, 2), (3, 2)));
				Add(3, Some.Shape.WithBlocks((1, 4), (2, 4), (2, 3), (3, 3)));
				Add(9, Some.Shape.WithBlocks((1, 10), (2, 10), (2, 9), (3, 9)));
			}
		}

		[Theory]
		[ClassData(typeof(TurnOneTimeData))]
		public void TurnOneTime(int start, FakeShape expected)
		{
			var shape = new TetriminoZ(start);
			shape.Turn();
			Check.That(shape).IsEquivalentTo(expected);
		}

		[Fact]
		public void TurnTwoTime()
		{
			var shape = new TetriminoZ();
			for (var i = 0; i < 4; i++)
				shape.Turn();
			Check.That(shape).IsEquivalentTo((1, 1), (1, 2), (2, 2), (2, 3));
		}

		[Fact]
		public void TurnCorrectlyWhenReachedRightSideInVerticalPosition()
		{
			var shape = new TetriminoZ(9);
			shape.Turn();
			for (var i = 0; i < 5; i++)
				shape.MoveRight();
			shape.Turn();
			Check.That(shape).IsEquivalentTo((1, 10), (1, 11), (2, 11), (2, 12));
		}

		[Fact]
		public void TurnCorrectlyWhenReachedLeftSideInVerticalPosition() {
			var shape = new TetriminoZ();
			shape.Turn();
			for (var i = 0; i < 3; i++)
				shape.MoveLeft();
			shape.Turn();
			Check.That(shape).IsEquivalentTo((1, 1), (1, 2), (2, 2), (2, 3));
		}
	}
}
