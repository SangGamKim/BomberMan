class ItemBombRange : ItemBase
{
    public ItemBombRange()
    {
        _image = '사';
    }

    public override MAPOBJECT_NAME Name() { return MAPOBJECT_NAME.BOMB_RANGE; }
}