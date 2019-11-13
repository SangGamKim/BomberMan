using System;

class StackShop : UIBase
{
	private char _pointImage;
	private Vector2 _pointPosition;

	private DIRECTION _direction;

	private int _equipmentIndex;

	public StackShop()
	{
		_pointImage = '▶';
	}

	public override void Start()
	{
		_pointPosition = new Vector2(96, 5);
		_equipmentIndex = 1;
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

		PrintBag();
		PrintShop();
		PrintEquipmentInformation(_equipmentIndex);
	}

	private void PrintBag()
	{
		Console.SetCursorPosition(10, 2);
		Console.Write("- B A G -");

		int y = 5;
		foreach (var eq in UserInformation.I.Inventory.DicHaveEquipment)
		{
			Console.SetCursorPosition(10, y);
			Console.Write(eq.Value.Name);
			y += 2;
		}
	}

	private void PrintShop()
	{
		Console.SetCursorPosition(102, 2);
		Console.Write("- S H O P -");

		Console.SetCursorPosition(100, 5);
		Console.Write("B O W");

		Console.SetCursorPosition(100, 7);
		Console.Write("G U N");

		Console.SetCursorPosition(100, 9);
		Console.Write("S W O R D");

		Console.SetCursorPosition(100, 11);
		Console.Write("A D I D A S");

		Console.SetCursorPosition(100, 13);
		Console.Write("N I K E");

		Console.SetCursorPosition(100, 15);
		Console.Write("E X I T");

		Console.SetCursorPosition((int)_pointPosition.X, (int)_pointPosition.Y);
		Console.Write(_pointImage);
	}

	private void PrintEquipmentInformation(int id)
	{
		if (id == 0) { return; }
		EquipmentTable table;
		TableManager.I.GetEquipmentTable(id, out table);

		Console.SetCursorPosition(50, 19);
		Console.Write("- 장 비 정 보 -");

		Console.SetCursorPosition(50, 20);
		Console.Write("이름 : {0}", table.Name);

		Console.SetCursorPosition(50, 21);
		Console.Write("시작 폭탄 개수 추가 : {0}", table.Stat.BombCount);

		Console.SetCursorPosition(50, 22);
		Console.Write("시작 폭탄 거리 추가 : {0}", table.Stat.AtkRange);

		Console.SetCursorPosition(50, 23);
		Console.Write("시작 이동 속도 추가 : {0}", table.Stat.MoveSpeed);

		Console.SetCursorPosition(50, 24);
		Console.Write("최대 폭탄 개수 추가 : {0}", table.Stat.MaxBombCount);

		Console.SetCursorPosition(50, 25);
		Console.Write("최대 폭탄 거리 추가 : {0}", table.Stat.MaxAtkRange);

		Console.SetCursorPosition(50, 26);
		Console.Write("최대 이동 속도 추가 : {0}", table.Stat.MaxMoveSpeed);
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
			if (_pointPosition.Y >= 15) { return; }
			_pointPosition.Y += 2;
		}
		else if (_direction == DIRECTION.UP)
		{
			if (_pointPosition.Y <= 5) { return; }
			_pointPosition.Y -= 2;
		}

		_direction = DIRECTION.NONE;

		if (_pointPosition.Y == 5) { _equipmentIndex = 1; }
		else if (_pointPosition.Y == 7) { _equipmentIndex = 2; }
		else if (_pointPosition.Y == 9) { _equipmentIndex = 3; }
		else if (_pointPosition.Y == 11) { _equipmentIndex = 11; }
		else if (_pointPosition.Y == 13) { _equipmentIndex = 12; }
		else if (_pointPosition.Y == 15) { _equipmentIndex = 0; }
	}

	private void CheckPoint(ConsoleKey key)
	{
		if (key == ConsoleKey.Enter)
		{
			if (_pointPosition.Y == 15)
			{
				_currentScene.IsPauseScene = false;
				UIManager.I.DestroyStackUI();
			}
			else
			{
				var pop = UIManager.I.CreateUI<PopupBuyQuestion>(_currentScene);
				pop.BuyEquipmentID = _equipmentIndex;
			}
		}
	}

	public override UI_NAME Name() { return UI_NAME.SHOP_STACK; }
	public override UI_TYPE Type() { return UI_TYPE.STACK; }
}
