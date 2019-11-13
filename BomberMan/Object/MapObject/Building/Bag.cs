
class Bag : MapObjectBase
{
    public Bag()
    {
        _image = '가';
    }

    public override void Interaction(Player player)
    {
        if (_transform.IsCross(player.Transfrom))
        {
            player.ReturnPos(new Vector2(_transform.Position.X, _transform.Position.Y + 1));

            LobbyScene scene = SceneManager.I.GetCurrentScene<LobbyScene>();
            scene.IsPauseScene = true;
            UIManager.I.CreateUI<StackBag>(scene);
        }
    }

    public override MAPOBJECT_NAME Name() { return MAPOBJECT_NAME.BAG; }
    public override MAPOBJECT_TYPE Type() { return MAPOBJECT_TYPE.INTERACTION; }
}
