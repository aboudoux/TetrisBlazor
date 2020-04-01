using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TetrisBlazor.Core;

namespace Tetris.Blazor.Tests
{
	public class FakeShape : IEnumerable<IBlock>
	{
		private readonly List<FakeBlock> _blocks = new List<FakeBlock>();

		public FakeShape WithBlocks(params (int row, int column)[] point)
		{
			_blocks.AddRange( point.Select(a=>new FakeBlock(a.row, a.column)));
			return this;
		}

		public IEnumerator<IBlock> GetEnumerator() => _blocks.Cast<IBlock>().GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}