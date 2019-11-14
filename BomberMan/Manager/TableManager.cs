using System.Collections.Generic;

class TableManager
{
    private static readonly TableManager _instance = new TableManager();

    private Dictionary<int, EquipmentTable> _dicEquipmentTable;
    private Dictionary<int, ItemTable> _dicItemTable;
    private Dictionary<int, MonsterTable> _dicMonsterTable;
    private Dictionary<int, PlayerTable> _dicPlayerTable;

	private TableManager()
    {
        CreatePlayerTable();
        CreateMonsterTable();
        CreateItemTable();
        CreateEquipmentTable();
    }

    public bool GetPlayerTable(int id, out PlayerTable table)
    {
        return _dicPlayerTable.TryGetValue(id, out table);
    }

    public bool GetEquipmentTable(int id, out EquipmentTable table)
    {
        return _dicEquipmentTable.TryGetValue(id, out table);
    }

    public bool GetMonsterTable(int id, out MonsterTable table)
    {
        return _dicMonsterTable.TryGetValue(id, out table);
    }

    public bool GetItemTable(int id, out ItemTable table)
    {
        return _dicItemTable.TryGetValue(id, out table);
    }

    private void CreatePlayerTable()
    {
        _dicPlayerTable = new Dictionary<int, PlayerTable>();

        _dicPlayerTable.Add(1, new PlayerTable(1, new PlayerStat(1,1,5,5,5,12)));
    }

    private void CreateEquipmentTable()
    {
        _dicEquipmentTable = new Dictionary<int, EquipmentTable>();

        _dicEquipmentTable.Add(1, new EquipmentTable(1, "BOW", EQUIPMENT_TYPE.WEAPON, new PlayerStat(1, 0, 0, 0, 0, 0)));
        _dicEquipmentTable.Add(2, new EquipmentTable(2, "GUN", EQUIPMENT_TYPE.WEAPON, new PlayerStat(0, 1, 0, 0, 0, 0)));
        _dicEquipmentTable.Add(3, new EquipmentTable(3, "SWORD", EQUIPMENT_TYPE.WEAPON, new PlayerStat(0, 0, 0, 1, 0, 0)));

        _dicEquipmentTable.Add(11, new EquipmentTable(11, "ADIDAS", EQUIPMENT_TYPE.SHOES, new PlayerStat(0, 0, 1, 0, 0, 0)));
        _dicEquipmentTable.Add(12, new EquipmentTable(12, "NIKE", EQUIPMENT_TYPE.SHOES, new PlayerStat(0, 0, 0, 0, 0, 1)));
    }

    private void CreateMonsterTable()
    {
        _dicMonsterTable = new Dictionary<int, MonsterTable>();
        _dicMonsterTable.Add(1, new MonsterTable(1, "Vayne", new MonsterStat(1, 2, 100)));
        _dicMonsterTable.Add(2, new MonsterTable(2, "Boss", new MonsterStat(2, 5, 1000))) ;
    }

    private void CreateItemTable()
    {
        _dicItemTable = new Dictionary<int, ItemTable>();

        _dicItemTable.Add(1, new ItemTable(1, "Star", new PlayerStat(0, 0, 13, 0, 0, 0)));
        _dicItemTable.Add(2, new ItemTable(2, "Fire", new PlayerStat(0, 0, 10, 0, 0, 0)));
    }

    public static TableManager I { get { return _instance; } }
}
