
class IntroScene : SceneBase
{
    public override void Start()
    {
        base.Start();
       _currentUI =  UIManager.I.CreateUI<PageIntro>(this);
    }

    public override SCENE_NAME Name() { return SCENE_NAME.INTRO_SCENE; }
}
