using System.Collections.Generic;

class SceneManager
{
	private static readonly SceneManager _instance = new SceneManager();

	private Dictionary<SCENE_NAME, SceneBase> _dicScene;
	private SceneBase _currentScene;

	private SceneManager()
	{
		_dicScene = new Dictionary<SCENE_NAME, SceneBase>();

		_dicScene.Add(SCENE_NAME.INTRO_SCENE, new IntroScene());
		_dicScene.Add(SCENE_NAME.LOBBY_SCENE, new LobbyScene());
		_dicScene.Add(SCENE_NAME.GAME_SCENE, new GameScene());
		_dicScene.Add(SCENE_NAME.RESULT_SCENE, new ResultScene());
	}

	public void Start()
	{
		_currentScene = _dicScene[SCENE_NAME.INTRO_SCENE];
		_currentScene.Running();
	}

	public void ChangeScene(SCENE_NAME nextScene)
	{
		_dicScene[_currentScene.Name()].IsEndScene = true;
		UIManager.I.DestroyPageUI();
		MapObjectManager.I.DestroyAllOjbect();

		_dicScene.TryGetValue(nextScene, out _currentScene);
		_currentScene.Running();
	}

	public T GetCurrentScene<T>() where T : SceneBase
	{
		return _currentScene as T;
	}

	public SceneBase CurrentScene { get { return _currentScene; } }

	public static SceneManager I { get { return _instance; } }
}
