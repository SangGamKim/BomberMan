using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class StackBag : UIBase
{
	private char _pointImage;
	private Vector2 _pointPosition;
	private DIRECTION _direction;
	private int _exitPosY;

	private Dictionary<int, int> _dicYEquipmentIndex;

	public StackBag()
	{
		_pointImage = '▶';
	}

	public override void Start()
	{
		_pointPosition = new Vector2(96, 5);
		_dicYEquipmentIndex = new Dictionary<int, int>();
		_exitPosY = 5;

		foreach (var eq in UserInformation.I.Inventory.DicHaveEquipment)
		{
			_dicYEquipmentIndex.Add(_exitPosY, eq.Value.ID);
			_exitPosY += 2;
		}
	}

	public override void Update(float ticks = 0)
	{
		ConsoleKey key = Console.ReadKey().Key;
		Move(key);
		CheckPoint(key);
	}

	public override void Render()
	{
		Console.Clear();
		PrintWearEquipment();
		PrintBag();
	}

	private void PrintBag()
	{
		Console.SetCursorPosition((int)_pointPosition.X, (int)_pointPosition.Y);
		Console.Write(_pointImage);
		Console.SetCursorPosition(100, 2);
		Console.Write("- B A G -");

		_exitPosY = 5;
		foreach (var eq in UserInformation.I.Inventory.DicHaveEquipment)
		{
			Console.SetCursorPosition(100, _exitPosY);
			Console.Write(eq.Value.Name);
			_exitPosY += 2;
		}

		Console.SetCursorPosition(100, _exitPosY);
		Console.Write("E X I T");
	}

	private void PrintWearEquipment()
	{
		Console.SetCursorPosition(10, 2);
		Console.Write("- W E A R -");

		_exitPosY = 5;
		foreach (var eq in UserInformation.I.Inventory.ListWearEquipment)
		{
			Console.SetCursorPosition(10, _exitPosY);
			Console.Write(eq.Name);
			_exitPosY += 2;
		}
	}

	private void Move(ConsoleKey key)
	{
		switch (key)
		{
			case ConsoleKey.UpArrow: { _direction = DIRECTION.UP; } break;
			case ConsoleKey.DownArrow: { _direction = DIRECTION.DOWN; } break;
			default: break;
		}

		if (_direction == DIRECTION.DOWN)
		{
			if (_pointPosition.Y >= _exitPosY) { return; }
			_pointPosition.Y += 2;
		}
		else if (_direction == DIRECTION.UP)
		{
			if (_pointPosition.Y <= 5) { return; }
			_pointPosition.Y -= 2;
		}
		_direction = DIRECTION.NONE;
	}

	private void CheckPoint(ConsoleKey key)
	{
		if (key == ConsoleKey.Enter)
		{
			if (_pointPosition.Y == _exitPosY)
			{
				_currentScene.IsPauseScene = false;
				UIManager.I.DestroyStackUI();
			}
			else
			{
				var pop = UIManager.I.CreateUI<PopupEquipmentInfo>(_currentScene);
				pop.EquipmentID = _dicYEquipmentIndex[(int)_pointPosition.Y];
			}
		}
	}


	public override UI_NAME Name() { return UI_NAME.BAG_STACK; }
	public override UI_TYPE Type() { return UI_TYPE.STACK; }
}
