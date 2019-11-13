
class GameScene : SceneBase
{
    private Player _player;

    public override void Start()
    {
        base.Start();

		_currentUI = UIManager.I.CreateUI<PageGame>(this);

		ChangeMap(MAP_NAME.ARCADE_A);
	}

    public override void Update(float ticks = 0)
    {
        base.Update(ticks);

        foreach (var obj in MapObjectManager.I.GetDic())
        {
            for (int i = 0; i < obj.Value.Count; i++) { obj.Value[i].Update(ticks); }
        }
    }

    public override void LastUpdate()
    {
        MapObjectManager.I.DestroyObject();
    }

    public override void Render()
    {
        base.Render();
        foreach (var obj in MapObjectManager.I.GetDic())
        {
            for (int i = 0; i < obj.Value.Count; i++) { obj.Value[i].Render(); }
        }
    }

	public override void Interaction()
	{
		for (int i = 0; i < MapObjectManager.I.GetDicList(MAPOBJECT_TYPE.MONSTER).Count; i++)
		{
			MapObjectManager.I.GetDicList(MAPOBJECT_TYPE.MONSTER)[i].Interaction(_player);

			for (int y = 0; y < MapObjectManager.I.GetDicList(MAPOBJECT_TYPE.EXPLODE_BOMB).Count; y++)
			{
				MapObjectManager.I.GetDicList(MAPOBJECT_TYPE.MONSTER)[i].Interaction(MapObjectManager.I.GetDicList(MAPOBJECT_TYPE.EXPLODE_BOMB)[y] as ExplodeBomb);
			}

		}

		for (int i = 0; i < MapObjectManager.I.GetDicList(MAPOBJECT_TYPE.BREAK_OBSTACLE).Count; i++)
		{
			MapObjectManager.I.GetDicList(MAPOBJECT_TYPE.BREAK_OBSTACLE)[i].Interaction(_player);

			for (int y = 0; y < MapObjectManager.I.GetDicList(MAPOBJECT_TYPE.EXPLODE_BOMB).Count; y++)
			{
				MapObjectManager.I.GetDicList(MAPOBJECT_TYPE.BREAK_OBSTACLE)[i].Interaction(MapObjectManager.I.GetDicList(MAPOBJECT_TYPE.EXPLODE_BOMB)[y] as ExplodeBomb);
			}
		}

		for (int i = 0; i < MapObjectManager.I.GetDicList(MAPOBJECT_TYPE.INTERACTION).Count; i++)
		{
			MapObjectManager.I.GetDicList(MAPOBJECT_TYPE.INTERACTION)[i].Interaction(_player);

			for (int y = 0; y < MapObjectManager.I.GetDicList(MAPOBJECT_TYPE.EXPLODE_BOMB).Count; y++)
			{
				MapObjectManager.I.GetDicList(MAPOBJECT_TYPE.INTERACTION)[i].Interaction(MapObjectManager.I.GetDicList(MAPOBJECT_TYPE.EXPLODE_BOMB)[y] as ExplodeBomb);
			}
		}

		for (int i = 0; i < MapObjectManager.I.GetDicList(MAPOBJECT_TYPE.EXPLODE_BOMB).Count; i++)
		{
			MapObjectManager.I.GetDicList(MAPOBJECT_TYPE.EXPLODE_BOMB)[i].Interaction(_player);

		}
	}

	public void ChangeMap(MAP_NAME name)
	{
		MapObjectManager.I.DestroyAllOjbect();

		_player = MapManager.I.ChangeMap(name);

		UIManager.I.GetPageUI<PageGame>().Player = _player;
	}
	
	public override SCENE_NAME Name() { return SCENE_NAME.GAME_SCENE; }
}
