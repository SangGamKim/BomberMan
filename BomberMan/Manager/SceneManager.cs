using System.Collections.Generic;

class SceneManager
{
	private static readonly SceneManager _instance = new SceneManager();

	private Dictionary<SCENE_NAME, SceneBase> _mapScene;
	private SceneBase _currentScene;

	private SceneManager()
	{
		_mapScene = new Dictionary<SCENE_NAME, SceneBase>();

		_mapScene.Add(SCENE_NAME.INTRO_SCENE, new IntroScene());
		_mapScene.Add(SCENE_NAME.LOBBY_SCENE, new LobbyScene());
		_mapScene.Add(SCENE_NAME.GAME_SCENE, new GameScene());
		_mapScene.Add(SCENE_NAME.RESULT_SCENE, new ResultScene());
	}

	public void Start()
	{
		_currentScene = _mapScene[SCENE_NAME.INTRO_SCENE];
		_currentScene.Running();
	}

	public void ChangeScene(SCENE_NAME nextScene)
	{
		_currentScene.IsEndScene = true;

		UIManager.I.DestroyPageUI();
		MapObjectManager.I.DestroyAllOjbect();

		_mapScene.TryGetValue(nextScene, out _currentScene);
		_currentScene.Running();
	}

	public T GetCurrentScene<T>() where T : SceneBase
	{
		return _currentScene as T;
	}

	public SceneBase CurrentScene { get { return _currentScene; } }

	public static SceneManager I { get { return _instance; } }
}
