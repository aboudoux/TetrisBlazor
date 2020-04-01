namespace TetrisBlazor.Core
{
	public class Score
	{
		public void AddCompletedLines(int line)
		{
			switch (line)
			{
				case 1: Value += 40; break;
				case 2: Value += 100; break;
				case 3: Value += 300; break;
				case 4: Value += 1200; break;
			}
		}

		public void AddUserMoveDown(int bonus) => Value += bonus;

		public int Value { get; private set; }
	}
}