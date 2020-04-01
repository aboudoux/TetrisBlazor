namespace TetrisBlazor.Core
{
	public interface IBlock
	{
		int Row { get; }
		int Column { get;  }

		string HexColor { get; }
	}
}