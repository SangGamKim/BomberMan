
struct PlayerTable
{
    public int ID;

    public PlayerStat Stat;

    public PlayerTable(int id, PlayerStat stat)
    {
        ID = id;
        Stat = stat;
    }
}

struct MonsterTable
{
    public int ID;
    public string Name;
    public MonsterStat Stat;

    public MonsterTable(int id, string name, MonsterStat stat)
    {
        ID = id;
        Name = name;
        Stat = stat;
    }
}

//게임 내 블럭을 깼을 때 나오는 아이템
struct ItemTable
{
    public int ID;
    public string Name;
    public PlayerStat Stat;

    public ItemTable(int id, string name, PlayerStat stat)
    {
        ID = id;
        Name = name;
        Stat = stat;
    }
}

//착용할 장비 아이템
struct EquipmentTable
{
	public int ID;
	public string Name;
	public EQUIPMENT_TYPE Type;
	public PlayerStat Stat;

	public EquipmentTable(int iD, string name, EQUIPMENT_TYPE type, PlayerStat stat)
	{
		ID = iD;
		Name = name;
		Type = type;
		Stat = stat;
	}
}