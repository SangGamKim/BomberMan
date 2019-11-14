
class Vayne : MonsterBase
{
    public Vayne()
    {
        _image = '베';
    }

    public override void Start()
    {
		Init(1);
		base.Start();
    }

    public override MAPOBJECT_NAME Name() { return MAPOBJECT_NAME.VAYNE; }
}
