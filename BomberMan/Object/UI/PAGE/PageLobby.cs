using System;

class PageLobby : UIBase
{
    public Player Player;

    public override void Render()
    {
        Console.SetCursorPosition(50, 2);
        Console.Write("L O B B Y");

        Console.SetCursorPosition(20, 23);
        Console.Write("Player PosX  : {0}", Player.Position.X);

        Console.SetCursorPosition(20, 24);
        Console.Write("Player PosY  : {0}", Player.Position.Y);

        Console.SetCursorPosition(20, 25);
        Console.Write("Player PosY  : {0}", Player._tempPosition.X);

        Console.SetCursorPosition(20, 26);
        Console.Write("Player PosY  : {0}", Player._tempPosition.Y);

        Console.SetCursorPosition(20, 27);
        Console.Write("Player   : {0}", MapObjectManager.I.GetObjectList(MAPOBJECT_TYPE.PLAYER).Count);

        Console.SetCursorPosition(20, 28);
        Console.Write("Obstacle  : {0}", MapObjectManager.I.GetObjectList(MAPOBJECT_TYPE.OBSTACLE).Count);

        Console.SetCursorPosition(20, 29);
        Console.Write("Monster  : {0}", MapObjectManager.I.GetObjectList(MAPOBJECT_TYPE.MONSTER).Count);

        Console.SetCursorPosition(20, 30);
        Console.Write("EXPLODE_BOMB  : {0}", MapObjectManager.I.GetObjectList(MAPOBJECT_TYPE.EXPLODE_BOMB).Count);
    }


    public override UI_NAME Name() { return UI_NAME.LOBBY_PAGE; }
    public override UI_TYPE Type() { return UI_TYPE.PAGE; }
}
