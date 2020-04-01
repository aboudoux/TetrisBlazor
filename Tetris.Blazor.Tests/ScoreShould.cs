using FluentAssertions;
using TetrisBlazor.Core;
using Xunit;

namespace Tetris.Blazor.Tests
{
	public class ScoreShould
	{
		[Theory]
		[InlineData(1,40)]
		[InlineData(2,100)]
		[InlineData(3,300)]
		[InlineData(4,1200)]
		public void IncrementDependingToNumberLines(int lines, int expectedScore)
		{
			var score = new Score();
			score.AddCompletedLines(lines);
			score.Value.Should().Be(expectedScore);
		}
	}
}