using System;

class PageIntro : UIBase
{
    public override void Update(float ticks = 0)
    {
        if (GetKey.Down(KEY_TYPE.SPACE)) { SceneManager.I.ChangeScene(SCENE_NAME.LOBBY_SCENE); }
    }

    public override void Render()
    {
        Console.SetCursorPosition(50, 5);
        Console.Write("B o m B E R");

        Console.SetCursorPosition(50, 7);
        Console.Write("M A N");

        Console.SetCursorPosition(50, 9);
        Console.Write("PUSH SPACE BAR!");
    }

    public override UI_NAME Name() { return UI_NAME.INTRO_PAGE; }
    public override UI_TYPE Type() { return UI_TYPE.PAGE; }
}
