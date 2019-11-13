
abstract class ItemBase : MapObjectBase
{
    private float _durationTime;
    private float _noDeathTime; //태어나자마자 폭탄에 맞아 죽으면 안되니 무적인 시간을 만들어줌 (터지는 폭탄의 지속시간보다는 길어야함)

    public override void Start()
    {
        _durationTime = 0;
        _noDeathTime = 0.3f;
    }

    public override void Update(float ticks = 0)
    {
        if (_durationTime < _noDeathTime) { _durationTime += ticks; }
    }

    public override void Interaction(Player player)
    {
        if (player.RideType == RIDE_TYPE.STAR) { return; }

        if (_transform.IsCross(player.Transfrom))
        {
            player.GetItem(Name());
            MapObjectManager.I.RemoveObjectAdd(this);
        }
    }

    public override void Interaction(ExplodeBomb explodeBomb)
    {
        if (_durationTime < _noDeathTime) { return; }
        if (_transform.IsCross(explodeBomb.Transfrom)) { MapObjectManager.I.RemoveObjectAdd(this); }
    }

    public override MAPOBJECT_TYPE Type() { return MAPOBJECT_TYPE.INTERACTION; }
}
