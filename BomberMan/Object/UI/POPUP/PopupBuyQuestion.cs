using System;

class PopupBuyQuestion : UIBase
{
	public int BuyEquipmentID;

	public override void Update(float ticks = 0)
	{
		ConsoleKey key = Console.ReadKey().Key;

		if (key == ConsoleKey.D1)
		{
			UserInformation.I.Inventory.BuyEquipment(BuyEquipmentID);
			UIManager.I.DestroyPopupUI(UI_NAME.BUY_QUESTION_POPUP);
		}
		else if (key == ConsoleKey.D2)
		{
			UIManager.I.DestroyPopupUI(UI_NAME.BUY_QUESTION_POPUP);
		}
	}

	public override void Render()
	{
		Console.Clear();
		Console.SetCursorPosition(45, 10);
		Console.Write("구입 하시겠습니까 ?");

		Console.SetCursorPosition(35, 15);
		Console.Write("1. 구입");

		Console.SetCursorPosition(65, 15);
		Console.Write("2. 나가기");
	}

	public override UI_NAME Name() { return UI_NAME.BUY_QUESTION_POPUP; }
	public override UI_TYPE Type() { return UI_TYPE.POPUP; }
}
