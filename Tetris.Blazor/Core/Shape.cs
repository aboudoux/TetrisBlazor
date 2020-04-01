using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TetrisBlazor.Core
{
	public class Shape : IEnumerable<Block>
	{
		private readonly int _gameWidth;
		private readonly int _gameHeight;
		private readonly string _hexColor;
		private List<Block> _blocks = new List<Block>();

		public Shape(int gameWidth, int gameHeight, string hexColor)
		{
			_gameWidth = gameWidth;
			_gameHeight = gameHeight;
			_hexColor = hexColor;
		}

		public static Shape Empty => Create("#000000");

		public static Shape Create(string hexColor, int gameWidth = Game.Width, int gameHeight = Game.Height) => new Shape(gameWidth, gameHeight, hexColor);

		public Shape WithBlocks((int row, int column) block1, (int row, int column) block2, (int row, int column) block3, (int row, int column) block4) {
			_blocks.Add(new Block(block1.row, block1.column,_hexColor));
			_blocks.Add(new Block(block2.row, block2.column,_hexColor));
			_blocks.Add(new Block(block3.row, block3.column,_hexColor));
			_blocks.Add(new Block(block4.row, block4.column, _hexColor));
			return this;
		}

		public void MoveRight()
		{
			if(!RightBorderReached())
				_blocks = _blocks.Select(a=>a.MoveRight()).ToList();

			bool RightBorderReached() => _blocks.Any(a => a.Column >= _gameWidth);
		}

		public void MoveLeft()
		{
			if (!LeftBorderReached())
				_blocks = _blocks.Select(a => a.MoveLeft()).ToList();

			bool LeftBorderReached() => _blocks.Any(a => a.Column <= 1);
		}

		public void MoveDown()
		{
			if (!BottomReached())
				_blocks = _blocks.Select(a => a.MoveDown()).ToList();

			bool BottomReached() => _blocks.Any(a => a.Row >= _gameHeight);
		}

		public bool ChangePosition(ShapePositionChanger movements)
		{
			if(movements.IsEmpty())
				return true;

			var index = 0;
			var transformedShape = _blocks.Select(a => a.ChangePosition(movements.Regular.ElementAt(index++))).ToList();
			if (IsExceedBottomBorder(transformedShape))
				return false;

			index = 0;
			if (IsExceedRightBorder(transformedShape))
				transformedShape = _blocks.Select(a => a.ChangePosition(movements.RightBorder.ElementAt(index++))).ToList();

			_blocks = transformedShape;
			return true;

			bool IsExceedRightBorder(List<Block> shape) => shape.Any(a => a.Column > _gameWidth);
			bool IsExceedBottomBorder(List<Block> shape) => shape.Any(a => a.Row > _gameHeight);
		}

		public IEnumerator<Block> GetEnumerator() => _blocks.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}