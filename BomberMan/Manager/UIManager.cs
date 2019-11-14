using System.Collections.Generic;

class UIManager
{
	private static readonly UIManager _instance = new UIManager();

	private Dictionary<UIBase, List<UIBase>> _dicPageStack;
	private List<UIBase> _listPopup;
	private UIBase _currentPage;

	private UIManager()
	{
		_dicPageStack = new Dictionary<UIBase, List<UIBase>>();
		_listPopup = new List<UIBase>();
	}

	/// <summary>
	/// UI를 만드는 함수
	/// UI를 만들고 Scene과 UI를 서로 연결
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="scene"></param>
	/// <returns></returns>
	public T CreateUI<T>(SceneBase scene) where T : UIBase, new()
	{
		var newUI = new T();
		scene.CurrentUI = newUI;
		newUI.CurrentScene = scene;

		newUI.Start();

		switch (newUI.Type())
		{
			case UI_TYPE.PAGE:
				{
					_currentPage = newUI;
					_dicPageStack.Add(_currentPage, new List<UIBase>());
				}
				break;
			case UI_TYPE.STACK: { _dicPageStack[_currentPage].Add(newUI); } break;
			case UI_TYPE.POPUP: { _listPopup.Add(newUI); } break;
			default: break;
		}
		return newUI;
	}

	/// <summary>
	/// UI 전체를 지우는 함수
	/// </summary>
	public void DestroyPageUI()
	{
		if (_currentPage == null) { return; }

		_dicPageStack[_currentPage].Clear();
		_dicPageStack.Remove(_currentPage);
		_currentPage = null;
	}
	/// <summary>
	/// Stack에 있는 UI를 지움 stack과 같은 역할 함수
	/// </summary>
	public void DestroyStackUI()
	{
		var delStack = _dicPageStack[_currentPage][_dicPageStack[_currentPage].Count - 1];
		if (delStack == null) { return; }

		if (_dicPageStack[_currentPage].Count > 1)
		{
			delStack.CurrentScene.CurrentUI = _dicPageStack[_currentPage][_dicPageStack[_currentPage].Count - 2];
			_dicPageStack[_currentPage][_dicPageStack[_currentPage].Count - 2].Start();
		}
		else
		{
			delStack.CurrentScene.CurrentUI = _currentPage;
			_currentPage.Start();
		}

		_dicPageStack[_currentPage].Remove(delStack);
		delStack = null;
	}
	/// <summary>
	/// 팝업창에 있는 UI를 지움
	/// 팝업창을 지우는데 따로 선택 해주지 않는다면 팝업창 맨앞에 떠있는걸 지움
	/// </summary>
	/// <param name="name"></param>
	public void DestroyPopupUI(UI_NAME name = UI_NAME.NONE)
	{
		var delPopup = _listPopup.Find((c) => c.Name() == name);
		if (delPopup == null)
		{
			if (_listPopup.Count < 1) { return; }
			delPopup = _listPopup.FindLast((c) => c != null);
		}

		_listPopup.Remove(delPopup);

		if (_listPopup.Count > 0)
		{
			var popup = _listPopup.FindLast((c) => c is UIBase);
			delPopup.CurrentScene.CurrentUI = popup;
			popup.Start();
		}
		else if (_dicPageStack[_currentPage].Count > 0)
		{
			delPopup.CurrentScene.CurrentUI = _dicPageStack[_currentPage][_dicPageStack[_currentPage].Count - 1];
			_dicPageStack[_currentPage][_dicPageStack[_currentPage].Count - 1].Start();
		}
		else if (_currentPage != null)
		{
			delPopup.CurrentScene.CurrentUI = _currentPage;
			_currentPage.Start();
		}


		delPopup = null;
	}


	public T GetPageUI<T>() where T : UIBase
	{
		return _currentPage as T;
	}

	public T GetStackUI<T>(UI_NAME name) where T : UIBase
	{
		return _dicPageStack[_currentPage].Find((c) => c.Name() == name) as T;
	}

	public T GetPopupUI<T>(UI_NAME name) where T : UIBase
	{
		return _listPopup.Find((c) => c.Name() == name) as T;
	}


	public static UIManager I { get { return _instance; } }
}
