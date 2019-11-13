using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class ResultScene : SceneBase
{
	public override void Start()
	{
		base.Start();
		_currentUI = UIManager.I.CreateUI<PageResult>(this);
	}

	public override void Update(float ticks = 0)
	{
		if (GetKey.Down(KEY_TYPE.SPACE))
		{
			_isEndScene = true;
		}
	}

	public override SCENE_NAME Name() { return SCENE_NAME.RESULT_SCENE; }
}
