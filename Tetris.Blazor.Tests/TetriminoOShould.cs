using FluentAssertions;
using TetrisBlazor.Tetriminos;
using Xunit;

namespace Tetris.Blazor.Tests {
	public class TetriminoOShould 
	{
		[Fact]
		public void BeInitialized() 
		{
			var shape = new TetriminoO();
			Check.That(shape).IsEquivalentTo((1, 1), (1, 2), (2, 1), (2, 2));
		}

		[Theory]
		[InlineData(1, 2,3,2,3)]
		[InlineData(2, 3,4,3,4)]
		[InlineData(5, 6,7,6,7)]
		[InlineData(6, 7,8,7,8)]
		[InlineData(7, 8,9,8,9)]
		[InlineData(8, 9,10,9,10)]
		[InlineData(9, 10,11,10,11)]
		[InlineData(10, 11,12,11,12)]
		[InlineData(20, 11,12,11,12)]
		public void MoveRight(int actionCount, int col1, int col2, int col3, int col4)
		{
			var shape = new TetriminoO();
			Call.Action(actionCount, () => shape.MoveRight());
			Check.That(shape).IsEquivalentTo((1, col1), (1, col2), (2, col3), (2, col4));
		}

		[Theory]
		[InlineData(1, 6, 7, 6, 7)]
		[InlineData(2, 5, 6, 5, 6)]
		[InlineData(3, 4, 5, 4, 5)]
		[InlineData(4, 3, 4, 3, 4)]
		[InlineData(5, 2, 3, 2, 3)]
		[InlineData(6, 1, 2, 1, 2)]
		[InlineData(7, 1, 2, 1, 2)]
		[InlineData(10, 1, 2, 1, 2)]
		public void MoveLeft(int actionCount, int col1, int col2, int col3, int col4)
		{
			var shape = new TetriminoO(7);
			Call.Action(actionCount, () => shape.MoveLeft());
			Check.That(shape).IsEquivalentTo((1, col1), (1, col2), (2, col3), (2, col4));
		}
		private class TurnOneTimeData : TheoryData<int,FakeShape>
		{
			public TurnOneTimeData()
			{
				Add(1, Some.Shape.WithBlocks((1, 1), (1, 2), (2, 1), (2, 2)));
				Add(2, Some.Shape.WithBlocks((1, 2), (1, 3), (2, 2), (2, 3)));
				Add(3, Some.Shape.WithBlocks((1, 3), (1, 4), (2, 3), (2, 4)));
				Add(9, Some.Shape.WithBlocks((1, 9), (1, 10), (2, 9), (2, 10)));
			}
		}

		[Theory]
		[ClassData(typeof(TurnOneTimeData))]
		public void TurnOneTime(int start, FakeShape expected)
		{
			var shape = new TetriminoO(start);
			shape.Turn();
			Check.That(shape).IsEquivalentTo(expected);
		}

		[Fact]
		public void TurnMoreTime()
		{
			var shape = new TetriminoO();
			Call.Action(4, () => shape.Turn());
			Check.That(shape).IsEquivalentTo((1,1), (1, 2), (2,1), (2,2));
		}
	}
}
