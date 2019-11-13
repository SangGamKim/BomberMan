
abstract class UIBase : ObjectBase
{
    protected SceneBase _currentScene; //유아이랑 씬은 뗄 수 없는 사이이기 때문에 멤버변수로 가지고 있음


    abstract public UI_NAME Name();
    abstract public UI_TYPE Type();

    public SceneBase CurrentScene { get { return _currentScene; } set { _currentScene = value; } }
}
