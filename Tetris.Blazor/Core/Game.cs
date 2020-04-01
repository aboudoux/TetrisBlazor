using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using TetrisBlazor.Tetriminos;

namespace TetrisBlazor.Core
{
	public class Game
	{
		public const int Width = 12;
		public const int Height = 24;
		private BlockEqualityComparer _blockComparer = new BlockEqualityComparer();
		private readonly RandomList<Func<Random,Tetrimino>> _randomTetrimino = new RandomList<Func<Random, Tetrimino>>(
			r => new TetriminoI(r.Next(1, Width - 3)),
			r => new TetriminoJ(r.Next(1, Width - 3)),
			r => new TetriminoL(r.Next(1, Width - 3)),
			r => new TetriminoO(r.Next(1, Width - 3)),
			r => new TetriminoS(r.Next(1, Width - 3)),
			r => new TetriminoZ(r.Next(1, Width - 3)),
			r => new TetriminoT(r.Next(1, Width - 3)));

		private object _locker = new object();

		private Game()
		{
		}

		public EventHandler Tick { get; set; }

		private Tetrimino _currentTetrimino = new EmptyTetrimino();
		private BottomStack _bottomStack = new BottomStack();
		private Score _score = new Score();
		private TimeSpan _clockTime = TimeSpan.FromMinutes(1);

		public int Score => _score.Value;
		public int Level { get; private set; } = 1;
		public string Clock => _clockTime.ToString(@"mm\:ss");

		public bool IsTetris => _bottomStack.CompletedLineCount == 4;

		public static Game New() => new Game();

		public void NextLevel()
		{
			Level++;
			_gameTimer.Interval -= 50;
			_clockTime = TimeSpan.FromMinutes(1);
		}

		public void AddShape()
		{
			_currentTetrimino = _randomTetrimino.Next().Invoke(_randomTetrimino.Seed);
			if(_bottomStack.WillCollideBottom(_currentTetrimino))
				Stop();
		}

		public void RemoveCompletedLines()
		{
			_score.AddCompletedLines(_bottomStack.CompletedLineCount);
			_bottomStack.RemoveCompletedLines();
		}

		public bool IsInCompletedLine(IBlock block) =>
			_bottomStack.CompletedLineBlocks.Contains(block, _blockComparer);

		public void MoveLeft()
		{
			if (!_bottomStack.WillCollideLeft(_currentTetrimino))
				_currentTetrimino.MoveLeft();
		}

		public void MoveRight()
		{
			if (!_bottomStack.WillCollideRight(_currentTetrimino))
				_currentTetrimino.MoveRight();
		}

		public void MoveDown()
		{
			_score.AddUserMoveDown(Level);
			InternalMoveDown();
		}

		private void InternalMoveDown()
		{
			lock (_locker)
			{
				if (_bottomStack.WillCollideBottom(_currentTetrimino))
				{
					_bottomStack.Add(_currentTetrimino);
					AddShape();
				}
				else
				{
					_currentTetrimino.MoveDown();
				}
			}
		} 
		public void Turn() => _currentTetrimino.Turn();

		public IEnumerable<IBlock> AllBlocks() => _currentTetrimino.Concat(_bottomStack);


		private Timer _clock = new Timer();
		private Timer _gameTimer = new Timer();
		public void Start()
		{
			_clock.Interval = 1000;
			_clock.Enabled = true;
			_clock.Elapsed += ClockOnElapsed;
			_clock.Start();

			_gameTimer.Interval = 500;
			_gameTimer.Enabled = true;
			_gameTimer.Elapsed += GameTimerOnElapsed;
			_gameTimer.Start();
		}

		private void ClockOnElapsed(object sender, ElapsedEventArgs e)
		{
			_clockTime = _clockTime.Subtract(TimeSpan.FromSeconds(1));
			if (_clockTime.Seconds <= 0)
			{
				NextLevel();
			}
		}

		private void GameTimerOnElapsed(object sender, ElapsedEventArgs e)
		{
			InternalMoveDown();
			Tick?.Invoke(this, null);
		}

		public bool Over { get; private set; }

		public void Stop()
		{
			Over = true;
			_gameTimer.Stop();
			_clock.Stop();
		}
	}
}