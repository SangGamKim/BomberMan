
class Block : MapObjectBase
{
    public Block()
    {
        _image = '■';
    }

    public override MAPOBJECT_NAME Name() { return MAPOBJECT_NAME.BLOCK; }
    public override MAPOBJECT_TYPE Type() { return MAPOBJECT_TYPE.OBSTACLE; }
}
