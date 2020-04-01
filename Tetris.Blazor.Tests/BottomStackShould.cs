using System.Linq;
using FluentAssertions;
using TetrisBlazor.Core;
using TetrisBlazor.Tetriminos;
using Xunit;

namespace Tetris.Blazor.Tests
{
	public class BottomStackShould
	{
		[Fact]
		public void AddSomeShape()
		{
			var shape = new TetriminoJ();
			Call.Action(Game.Height, shape.MoveDown);
			var bottomStack = new BottomStack();
			bottomStack.Add(shape);
			bottomStack.Should().NotBeEmpty();

		}

		[Theory]
		[InlineData(3, false)]
		[InlineData(2, true)]
		[InlineData(1, true)]
		public void SignalCollisionIfTouchingBottom(int padding, bool expected)
		{
			var shape = new TetriminoJ();
			var bottomStack = new BottomStack();

			Call.Action(Game.Height - padding, shape.MoveDown);
			bottomStack.WillCollideBottom(shape).Should().Be(expected);
		}

		[Theory]
		[InlineData(6, false)]
		[InlineData(5, false)]
		[InlineData(4, true)]
		
		public void SignalCollisionIfTouchingOtherBlock(int padding, bool expected)
		{
			var bottomStack = new BottomStack();
			var shape1 = new TetriminoJ();
			var shape2 = new TetriminoL();

			shape1.Turn();
			

			Call.Action(Game.Height, shape1.MoveDown);
			bottomStack.Add(shape1);

			Call.Action(Game.Height - padding, shape2.MoveDown);
			bottomStack.WillCollideBottom(shape2).Should().Be(expected);
		}

		[Fact]
		public void GetTotalLines()
		{
			var bottomStack = new BottomStack();
			var square1 = new TetriminoO();
			var square2 = new TetriminoO(3);
			var square3 = new TetriminoO(5);
			var square4 = new TetriminoO(7);
			var square5 = new TetriminoO(9);
			var square6 = new TetriminoO(11);
			Call.Action(Game.Height, () =>
			{
				square1.MoveDown();
				square2.MoveDown();
				square3.MoveDown();
				square4.MoveDown();
				square5.MoveDown();
				square6.MoveDown();
			});
			bottomStack.Add(square1);
			bottomStack.Add(square2);
			bottomStack.Add(square3);
			bottomStack.Add(square4);
			bottomStack.Add(square5);


			bottomStack.CompletedLineCount.Should().Be(0);
			bottomStack.Add(square6);
			bottomStack.CompletedLineCount.Should().Be(2);
			bottomStack.CompletedLineBlocks.Should().HaveCount(24);
		}

		[Fact]
		public void RemoveTotalLines()
		{
			var bottomStack = new BottomStack();
			var square1 = new TetriminoO();
			var square2 = new TetriminoO(3);
			var square3 = new TetriminoO(5);
			var square4 = new TetriminoO(7);
			var square5 = new TetriminoO(9);
			var tetriminoJ = new TetriminoJ(10);
			tetriminoJ.Turn();
			tetriminoJ.MoveRight();

			Call.Action(Game.Height, () => {
				square1.MoveDown();
				square2.MoveDown();
				square3.MoveDown();
				square4.MoveDown();
				square5.MoveDown();
				tetriminoJ.MoveDown();
			});

			bottomStack.Add(square1);
			bottomStack.Add(square2);
			bottomStack.Add(square3);
			bottomStack.Add(square4);
			bottomStack.Add(square5);
			bottomStack.Add(tetriminoJ);

			bottomStack.RemoveCompletedLines();
			bottomStack.CompletedLineCount.Should().Be(0);
			bottomStack.Count().Should().Be(12);

			Check.That(bottomStack).IsEquivalentTo((24,1), (24, 2), (24, 3), (24, 4), (24, 5), (24, 6), (24, 7), (24, 8), (24, 9), (24, 10), (24, 12), (23, 12));
		}

		[Fact]
		public void RemoveLineSeparated()
		{
			var bottomStack = new BottomStack();
			var t1_1 = new TetriminoT(1);
			Call.Action(2, () => t1_1.Turn());
			var t1_2 = new TetriminoT(4);
			Call.Action(2, () => t1_2.Turn());
			var t1_3 = new TetriminoT(7);
			Call.Action(2, () => t1_3.Turn());
			var o1_1 = new TetriminoO(10);

			var i3_1 = new TetriminoI(1);
			var i3_2 = new TetriminoI(5);
			var l3_1 = new TetriminoJ(9);
			Call.Action(2, () => l3_1.Turn());


			var i4_1 = new TetriminoI(1);
			var i4_2 = new TetriminoI(5);
			var o4_1 = new TetriminoO(10);

			var lastBar = new TetriminoI();

			
			bottomStack.PushBottom(t1_1);
			bottomStack.PushBottom(t1_2);
			bottomStack.PushBottom(t1_3);
			bottomStack.PushBottom(o1_1);

			bottomStack.PushBottom(i3_1);
			bottomStack.PushBottom(i3_2);
			bottomStack.PushBottom(l3_1);

			bottomStack.PushBottom(i4_1);
			bottomStack.PushBottom(i4_2);
			bottomStack.PushBottom(o4_1);

			lastBar.Turn();
			Call.Action(12, () => lastBar.MoveRight());
			bottomStack.PushBottom(lastBar);

			bottomStack.CompletedLineCount.Should().Be(3);
			bottomStack.RemoveCompletedLines();
			bottomStack.Count().Should().Be(8);
			Check.That(bottomStack).IsEquivalentTo((24,2),(24,5),(24,8),(24,10),(24,11),(24,12),(23,10),(23,11));
		}
	}

	public static class BottomStackExtensions
	{
		public static void PushBottom(this BottomStack stack, Tetrimino shape)
		{
			Call.ActionUntil(() => !stack.WillCollideBottom(shape), shape.MoveDown);
			stack.Add(shape);
		}
	}
}