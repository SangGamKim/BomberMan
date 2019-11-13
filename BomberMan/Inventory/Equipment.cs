
class Equipment
{
	private readonly int _uid;
	private readonly string _name;
	private readonly EQUIPMENT_TYPE _type;
	private PlayerStat _stat;

	public Equipment(int uid, string name, EQUIPMENT_TYPE type, PlayerStat stat)
	{
		_stat = stat;
		_uid = uid;
		_type = type;
		_name = name;
	}

	public int ID { get { return _uid; } }
	public string Name { get { return _name; } }
	public PlayerStat Stat { get { return _stat; } set { _stat = value; } }
}


