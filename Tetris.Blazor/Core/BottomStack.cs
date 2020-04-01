using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TetrisBlazor.Core
{
	public class BottomStack : IEnumerable<IBlock>
	{
		private readonly int _gameHeight;
		private readonly int _gameWidth;
		private readonly List<IBlock> _completedLineBlocks = new List<IBlock>();
		private readonly List<IBlock> _blocks = new List<IBlock>();
		private readonly BlockEqualityComparer _blockEquality = new BlockEqualityComparer();

		public BottomStack(int gameHeight = Game.Height, int gameWidth = Game.Width)
		{
			_gameHeight = gameHeight;
			_gameWidth = gameWidth;
		}

		public int CompletedLineCount => _completedLineBlocks.Distinct(new ByRow()).Count();

		public IEnumerable<IBlock> CompletedLineBlocks => _completedLineBlocks;

		public void Add(IEnumerable<IBlock> shape)
		{
			_blocks.AddRange(shape);

			_completedLineBlocks.AddRange(
				_blocks.Distinct(new ByRow())
					.Select(block => block.Row)
					.Where(IsCompleteLine)
					.SelectMany(index => _blocks.Where(b => b.Row == index)));

			bool IsCompleteLine(int row)
				=> _blocks.Count(a => a.Row == row) == _gameWidth;
		}

		public IEnumerator<IBlock> GetEnumerator() 
			=> _blocks.Cast<IBlock>().GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator()
			=> GetEnumerator();

		public bool WillCollideBottom(IEnumerable<IBlock> shape)
			=> shape.Any(a => a.Row + 1 > _gameHeight || _blocks.Any(b=>b.Row == a.Row + 1 && b.Column == a.Column));

		public bool WillCollideRight(IEnumerable<IBlock> shape)
			=> shape.Any(a=> _blocks.Any(b => b.Column == a.Column + 1 && b.Row == a.Row));

		public bool WillCollideLeft(IEnumerable<IBlock> shape)
			=> shape.Any(a => _blocks.Any(b => b.Column == a.Column - 1 && b.Row == a.Row));

		public void RemoveCompletedLines()
		{
			if(!_completedLineBlocks.Any()) return;

			var lineIndexes =_completedLineBlocks.Distinct(new ByRow()).Select(a => a.Row).OrderBy(a => a);
			_blocks.RemoveAll(b => _completedLineBlocks.Contains(b, _blockEquality));

			lineIndexes.ForEach(i =>
			{
				var movedBlocks = _blocks.Where(a => a.Row < i).Select(a => ((Block) a).MoveDown()).Cast<IBlock>().ToList();
				_blocks.RemoveAll(a => a.Row < i);
				_blocks.AddRange(movedBlocks);
			});

			_completedLineBlocks.Clear();
		}

		private class ByRow : IEqualityComparer<IBlock>
		{
			public bool Equals(IBlock x, IBlock y) => x.Row == y.Row;

			public int GetHashCode(IBlock obj) => obj.Row;
		}
	}
}