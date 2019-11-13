using System;


class PageResult : UIBase
{
	public override void Render()
	{
		Console.Clear();
		Console.SetCursorPosition(50, 2);
		Console.Write("R E S U L T");

		Console.SetCursorPosition(50, 5);
		Console.Write("점수 : {0}", GameManager.I().PlayerScore);

		Console.SetCursorPosition(50, 7);
		Console.Write("킬 : {0}", GameManager.I().PlayerKill);

		Console.SetCursorPosition(50, 10);
		Console.Write("종료 하시려면 스페이스바를 누르세요");
	}

	public override UI_NAME Name() { return UI_NAME.RESULT_PAGE; }
    public override UI_TYPE Type() { return UI_TYPE.PAGE; }
}
