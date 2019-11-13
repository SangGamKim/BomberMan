class ItemBombCount : ItemBase
{
    public ItemBombCount()
    {
        _image = '개';
    }

    public override MAPOBJECT_NAME Name() { return MAPOBJECT_NAME.BOMB_COUNT; }
}