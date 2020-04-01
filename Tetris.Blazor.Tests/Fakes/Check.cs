using System.Collections.Generic;
using FluentAssertions;
using TetrisBlazor.Core;

namespace Tetris.Blazor.Tests
{
	public class Check
	{
		private readonly IEnumerable<IBlock> _tetrimino;

		private Check(IEnumerable<IBlock> tetrimino)
		{
			_tetrimino = tetrimino;
		}
		public static Check That(IEnumerable<IBlock> shape) => new Check(shape);

		public void IsEquivalentTo(params (int row, int column)[] point)
			=> _tetrimino.Should().BeEquivalentTo(Some.Shape.WithBlocks(point), c => c.Excluding(a => a.HexColor));

		public void IsEquivalentTo(FakeShape shape) 
			=> _tetrimino.Should().BeEquivalentTo(shape, c => c.Excluding(a => a.HexColor));
	}
}