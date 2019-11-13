using System;

abstract class SceneBase : ObjectBase
{
    protected UIBase _currentUI;

    protected DateTime _oldTime;
    protected long _ticks;

    protected bool _isEndScene;
    protected bool _isPauseScene;

    public override void Start()
    {
        _isEndScene = false;
        _isPauseScene = false;
        _oldTime = DateTime.Now;
        _ticks = Utility.DeltaTime;
    }

    public override void Update(float ticks = 0)
    {
        GetKey.Update();
        _currentUI.Update();
    }

    public override void LastUpdate() { }

    public override void Render()
    {
        Console.Clear();
        _currentUI.Render();
    }

    public virtual void Interaction() { }

    public void Running()
    {
        Start();

        while (_isEndScene == false)
        {
            long temp = (DateTime.Now - _oldTime).Ticks;
            _oldTime = DateTime.Now;

            _ticks += temp;

            if (_ticks > Utility.DeltaTime)
            {
                Update(_ticks / (float)(10000 * 1000));
                Interaction();
                LastUpdate();
                Render();

                _ticks -= Utility.DeltaTime;
            }

            while (_isPauseScene)
            {
                _currentUI.Render();
                _currentUI.Update();
            }
        }

		Console.Clear();
    }


    public abstract SCENE_NAME Name();

    public UIBase CurrentUI { get { return _currentUI; } set { _currentUI = value; } }
    public bool IsEndScene { get { return _isEndScene; } set { _isEndScene = value; } }
    public bool IsPauseScene
    {
        get { return _isPauseScene; }
        set
        {
            _isPauseScene = value;

            if (_isPauseScene == false)
            {
                _ticks = 0;
                _oldTime = DateTime.Now;
            }
        }
    }

}
