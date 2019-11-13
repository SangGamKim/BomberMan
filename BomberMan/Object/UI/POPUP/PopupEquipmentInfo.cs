using System;

class PopupEquipmentInfo : UIBase
{
	public int EquipmentID;

	public override void Update(float ticks = 0)
	{
		ConsoleKey key = Console.ReadKey().Key;

		if (key == ConsoleKey.D1)
		{
			if (UserInformation.I.Inventory.WearEquipment(EquipmentID) == false) { System.Diagnostics.Debug.Assert(false, "Equipment Table Null"); }

			UIManager.I.DestroyPopupUI(UI_NAME.EQUIPINFO_POPUP);
		}
		else if (key == ConsoleKey.D2)
		{
			UIManager.I.DestroyPopupUI(UI_NAME.EQUIPINFO_POPUP);
		}
	}

	public override void Render()
	{
		Console.Clear();
		Console.SetCursorPosition(45, 10);
		Console.Write("착용 하시겠습니까 ?");

		Console.SetCursorPosition(35, 15);
		Console.Write("1. 착용");

		Console.SetCursorPosition(65, 15);
		Console.Write("2. 나가기");

		Console.SetCursorPosition(100, 15);
		Console.Write(EquipmentID);

		PrintEquipmentInformation(EquipmentID);
	}

	private void PrintEquipmentInformation(int id)
	{
		if (id == 0) { return; }
		var table = UserInformation.I.Inventory.GetEquipment(id);

		Console.SetCursorPosition(45, 19);
		Console.Write("- 장 비 정 보 -");
		Console.SetCursorPosition(45, 20);
		Console.Write("이름 : {0}", table.Name);
		Console.SetCursorPosition(45, 21);
		Console.Write("시작 폭탄 개수 추가 : {0}", table.Stat.BombCount);
		Console.SetCursorPosition(45, 22);
		Console.Write("시작 폭탄 거리 추가 : {0}", table.Stat.AtkRange);
		Console.SetCursorPosition(45, 23);
		Console.Write("시작 이동 속도 추가 : {0}", table.Stat.MoveSpeed);
		Console.SetCursorPosition(45, 24);
		Console.Write("최대 폭탄 개수 추가 : {0}", table.Stat.MaxBombCount);
		Console.SetCursorPosition(45, 25);
		Console.Write("최대 폭탄 거리 추가 : {0}", table.Stat.MaxAtkRange);
		Console.SetCursorPosition(45, 26);
		Console.Write("최대 이동 속도 추가 : {0}", table.Stat.MaxMoveSpeed);
	}


	public override UI_NAME Name() { return UI_NAME.EQUIPINFO_POPUP; }
	public override UI_TYPE Type() { return UI_TYPE.POPUP; }
}
