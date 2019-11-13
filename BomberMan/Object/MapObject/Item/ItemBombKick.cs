class ItemBombKick : ItemBase
{
    public ItemBombKick()
    {
        _image = '킥';
    }

    public override MAPOBJECT_NAME Name() { return MAPOBJECT_NAME.BOMB_KICK; }
}