using System.Collections.Generic;

class TableManager
{
    private static readonly TableManager _instance = new TableManager();

    private Dictionary<int, EquipmentTable> _mapEquipmentTable;
    private Dictionary<int, ItemTable> _mapItemTable;
    private Dictionary<int, MonsterTable> _mapMonsterTable;
    private Dictionary<int, PlayerTable> _mapPlayerTable;

	private TableManager()
    {
        CreatePlayerTable();
        CreateMonsterTable();
        CreateItemTable();
        CreateEquipmentTable();
    }

    public bool GetPlayerTable(int id, out PlayerTable table)
    {
        return _mapPlayerTable.TryGetValue(id, out table);
    }

    public bool GetEquipmentTable(int id, out EquipmentTable table)
    {
        return _mapEquipmentTable.TryGetValue(id, out table);
    }

    public bool GetMonsterTable(int id, out MonsterTable table)
    {
        return _mapMonsterTable.TryGetValue(id, out table);
    }

    public bool GetItemTable(int id, out ItemTable table)
    {
        return _mapItemTable.TryGetValue(id, out table);
    }

    private void CreatePlayerTable()
    {
        _mapPlayerTable = new Dictionary<int, PlayerTable>();

        _mapPlayerTable.Add(1, new PlayerTable(1, new PlayerStat(1,1,5,5,5,12)));
    }

    private void CreateEquipmentTable()
    {
        _mapEquipmentTable = new Dictionary<int, EquipmentTable>();

        _mapEquipmentTable.Add(1, new EquipmentTable(1, "BOW", EQUIPMENT_TYPE.WEAPON, new PlayerStat(1, 0, 0, 0, 0, 0)));
        _mapEquipmentTable.Add(2, new EquipmentTable(2, "GUN", EQUIPMENT_TYPE.WEAPON, new PlayerStat(0, 1, 0, 0, 0, 0)));
        _mapEquipmentTable.Add(3, new EquipmentTable(3, "SWORD", EQUIPMENT_TYPE.WEAPON, new PlayerStat(0, 0, 0, 1, 0, 0)));

        _mapEquipmentTable.Add(11, new EquipmentTable(11, "ADIDAS", EQUIPMENT_TYPE.SHOES, new PlayerStat(0, 0, 1, 0, 0, 0)));
        _mapEquipmentTable.Add(12, new EquipmentTable(12, "NIKE", EQUIPMENT_TYPE.SHOES, new PlayerStat(0, 0, 0, 0, 0, 1)));
    }

    private void CreateMonsterTable()
    {
        _mapMonsterTable = new Dictionary<int, MonsterTable>();
        _mapMonsterTable.Add(1, new MonsterTable(1, "Vayne", new MonsterStat(1, 2, 100)));
        _mapMonsterTable.Add(2, new MonsterTable(2, "Boss", new MonsterStat(2, 5, 1000))) ;
    }

    private void CreateItemTable()
    {
        _mapItemTable = new Dictionary<int, ItemTable>();

        _mapItemTable.Add(1, new ItemTable(1, "Star", new PlayerStat(0, 0, 13, 0, 0, 0)));
        _mapItemTable.Add(2, new ItemTable(2, "Fire", new PlayerStat(0, 0, 10, 0, 0, 0)));
    }

    public static TableManager I { get { return _instance; } }
}
