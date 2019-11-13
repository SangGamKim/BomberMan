
class Shop : MapObjectBase
{
    public Shop()
    {
        _image = '샾';
    }

    public override void Interaction(Player player)
    {
        if (_transform.IsCross(player.Transfrom))
        {
            player.ReturnPos(new Vector2(_transform.Position.X, _transform.Position.Y + 1));

            var scene = SceneManager.I.GetCurrentScene<LobbyScene>();
            scene.IsPauseScene = true;
            UIManager.I.CreateUI<StackShop>(scene);
        }
    }

    public override MAPOBJECT_NAME Name() { return MAPOBJECT_NAME.SHOP; }
    public override MAPOBJECT_TYPE Type() { return MAPOBJECT_TYPE.INTERACTION; }
}
