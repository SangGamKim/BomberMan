using System;

class PageGame : UIBase
{
	public Player Player;

	public override void Render()
	{
		Console.SetCursorPosition(20, 25);
		Console.Write("Player AtkRange : {0}", Player.GameStat.AtkRange);

		Console.SetCursorPosition(20, 26);
		Console.Write("Player BombCount : {0}", Player.GameStat.BombCount);

		Console.SetCursorPosition(20, 27);
		Console.Write("Player MoveSpeed : {0}", Player.GameStat.MoveSpeed);

		Console.SetCursorPosition(20, 28);
		Console.Write("Player MaxAtkRange : {0}", Player.GameStat.MaxAtkRange);

		Console.SetCursorPosition(20, 29);
		Console.Write("Player MaxBombCount : {0}", Player.GameStat.MaxBombCount);

		Console.SetCursorPosition(20, 30);
		Console.Write("Player MaxMoveSpeed : {0}", Player.GameStat.MaxMoveSpeed);

		Console.SetCursorPosition(20, 31);
		Console.Write("Player HaveKick : {0}", Player.HaveKick);
	}

	public override UI_NAME Name() { return UI_NAME.GAME_PAGE; }
	public override UI_TYPE Type() { return UI_TYPE.PAGE; }
}
