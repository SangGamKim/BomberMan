
class BreakBlock : MapObjectBase
{
    public BreakBlock()
    {
        _image = '□';
    }

    public override void Interaction(ExplodeBomb explodeBomb)
    {
        if (_transform.IsCross(explodeBomb.Transfrom))
        {
            GameManager.I().CreateItem(_transform.Position);

            MapObjectManager.I.RemoveObjectAdd(this);
        }
    }

    public override MAPOBJECT_NAME Name() { return MAPOBJECT_NAME.BREAK_BLOCK; }
    public override MAPOBJECT_TYPE Type() { return MAPOBJECT_TYPE.BREAK_OBSTACLE; }
}
