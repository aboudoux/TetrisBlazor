using FluentAssertions;
using TetrisBlazor.Tetriminos;
using Xunit;

namespace Tetris.Blazor.Tests {
	public class TetriminoSShould 
	{
		[Fact]
		public void BeInitialized() 
		{
			var shape = new TetriminoS();
			Check.That(shape).IsEquivalentTo((1, 3), (1, 2), (2, 2), (2, 1));
		}

		[Theory]
		[InlineData(1, 4,3,3,2)]
		[InlineData(2, 5,4,4,3)]
		[InlineData(5, 8,7,7,6)]
		[InlineData(6, 9,8,8,7)]
		[InlineData(7, 10,9,9,8)]
		[InlineData(8, 11,10,10,9)]
		[InlineData(9, 12,11,11,10)]
		[InlineData(10, 12,11,11,10)]
		[InlineData(20, 12,11,11,10)]
		public void MoveRight(int actionCount, int col1, int col2, int col3, int col4)
		{
			var shape = new TetriminoS();
			for(var i = 0; i < actionCount; i++)
				shape.MoveRight();
			Check.That(shape).IsEquivalentTo((1, col1), (1, col2), (2, col3), (2, col4));
		}

		[Theory]
		[InlineData(1, 8, 7, 7, 6)]
		[InlineData(2, 7, 6, 6, 5)]
		[InlineData(3, 6, 5, 5, 4)]
		[InlineData(4, 5, 4, 4, 3)]
		[InlineData(5, 4, 3, 3, 2)]
		[InlineData(6, 3, 2, 2, 1)]
		[InlineData(7, 3, 2, 2, 1)]
		[InlineData(10, 3, 2, 2, 1)]
		public void MoveLeft(int actionCount, int col1, int col2, int col3, int col4)
		{
			var shape = new TetriminoS(7);
			for (var i = 0; i < actionCount; i++)
				shape.MoveLeft();
			Check.That(shape).IsEquivalentTo((1, col1), (1, col2), (2, col3), (2, col4));
		}
		private class TurnOneTimeData : TheoryData<int,FakeShape>
		{
			public TurnOneTimeData()
			{
				Add(1, Some.Shape.WithBlocks((1, 1), (2, 1), (2, 2),(3, 2) ));
				Add(2, Some.Shape.WithBlocks((1, 2), (2, 2), (2, 3), (3, 3)));
				Add(3, Some.Shape.WithBlocks((1, 3), (2, 3), (2, 4), (3, 4)));
				Add(9, Some.Shape.WithBlocks((1, 9), (2, 9), (2, 10), (3, 10)));
				Add(10, Some.Shape.WithBlocks((1, 10), (2, 10), (2, 11), (3, 11)));
			}
		}

		[Theory]
		[ClassData(typeof(TurnOneTimeData))]
		public void TurnOneTime(int start, FakeShape expected)
		{
			var shape = new TetriminoS(start);
			shape.Turn();
			Check.That(shape).IsEquivalentTo(expected);
		}

		[Fact]
		public void TurnTwoTime()
		{
			var shape = new TetriminoS();
			for (var i = 0; i < 4; i++)
				shape.Turn();
			Check.That(shape).IsEquivalentTo((1, 3), (1, 2), (2, 2), (2, 1));
		}

		[Fact]
		public void TurnCorrectlyWhenReachedRightSideInVerticalPosition()
		{
			var shape = new TetriminoS(9);
			shape.Turn();
			for (var i = 0; i < 5; i++)
				shape.MoveRight();
			shape.Turn();
			Check.That(shape).IsEquivalentTo((1, 12), (1, 11), (2, 11), (2, 10));
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
