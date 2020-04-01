using System.Collections.Generic;

namespace TetrisBlazor.Core
{
	public class BlockEqualityComparer : IEqualityComparer<IBlock>
	{
		public bool Equals(IBlock x, IBlock y) => x.Row == y.Row && x.Column == y.Column;

		public int GetHashCode(IBlock obj) {
			unchecked {
				return (obj.Row * 397) ^ obj.Column;
			}
		}
	}
}