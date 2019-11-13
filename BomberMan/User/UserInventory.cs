using System.Collections.Generic;

class UserInventory
{
	private Dictionary<int, Equipment> _dicHaveEquipment;
	private List<Equipment> _listWearEquipment;

	private int _uid;

	public UserInventory()
	{
		_dicHaveEquipment = new Dictionary<int, Equipment>();
		_listWearEquipment = new List<Equipment>();
		_uid = 1;
	}

	public bool BuyEquipment(int id)
	{
		EquipmentTable table;
		if (TableManager.I.GetEquipmentTable(id, out table) == false) { System.Diagnostics.Debug.Assert(false, "Buy ItemTable Null"); }

		_dicHaveEquipment.Add(_uid, new Equipment(_uid++, table.Name, table.Type, table.Stat));
		return true;
	}

	public bool WearEquipment(int id)
	{
		if (_dicHaveEquipment[id] == null) { return false; }

		_listWearEquipment.Add(_dicHaveEquipment[id]);
		_dicHaveEquipment.Remove(id);
		return true;
	}

	public Equipment GetEquipment(int id)
	{
		return _dicHaveEquipment[id];
	}

	public Equipment GetWearEquipment(int id)
	{
		return _listWearEquipment.Find((c) => c.ID == id);
	}


	public Dictionary<int, Equipment> DicHaveEquipment { get { return _dicHaveEquipment; } }
	public List<Equipment> ListWearEquipment { get { return _listWearEquipment; } }
}
