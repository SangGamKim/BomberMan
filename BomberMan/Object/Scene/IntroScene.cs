
class IntroScene : SceneBase
{
    public override void Start()
    {
        base.Start();
       _currentUI =  UIManager.I.CreateUI<PageIntro>(this);
    }

	public override void Update(float ticks = 0)
	{
		if (GetKey.Down(KEY_TYPE.SPACE)) { SceneManager.I.ChangeScene(SCENE_NAME.LOBBY_SCENE); }
	}

	public override SCENE_NAME Name() { return SCENE_NAME.INTRO_SCENE; }
}
