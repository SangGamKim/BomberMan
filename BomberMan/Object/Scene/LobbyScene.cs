
class LobbyScene : SceneBase
{
    private Player _player;

    public override void Start()
    {
        base.Start();

        GameManager.CraeteInstance();

        _currentUI = UIManager.I.CreateUI<PageLobby>(this);

        _player = MapManager.I.ChangeMap(MAP_NAME.LOBBY);
        UIManager.I.GetPageUI<PageLobby>().Player = _player;
    }

    public override void Update(float ticks = 0)
    {
        base.Update(ticks);

        foreach (var obj in MapObjectManager.I.GetDic())
        {
            for (int i = 0; i < obj.Value.Count; i++) { obj.Value[i].Update(ticks); }
        }
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
        for (int i = 0; i < MapObjectManager.I.GetDicList(MAPOBJECT_TYPE.INTERACTION).Count; i++)
        {
            MapObjectManager.I.GetDicList(MAPOBJECT_TYPE.INTERACTION)[i].Interaction(_player);
        }
    }


    public override SCENE_NAME Name() { return SCENE_NAME.LOBBY_SCENE; }
}
