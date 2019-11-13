
class Vayne : MonsterBase
{
    public Vayne()
    {
        _image = '베';
    }

    public override void Start()
    {
        base.Start();

        MonsterTable table;
        if (TableManager.I.GetMonsterTable(1, out table) == false) { System.Diagnostics.Debug.Assert(false, "MonsterTable Null"); }
        _stat = table.Stat;
    }

    public override MAPOBJECT_NAME Name() { return MAPOBJECT_NAME.VAYNE; }
}
