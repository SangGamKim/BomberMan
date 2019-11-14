using System.Collections.Generic;

class UserInventory
{
	private Dictionary<int, Equipment> _mapHaveEquipment;
	private List<Equipment> _listWearEquipment;

	private int _uid;

	public UserInventory()
	{
		_mapHaveEquipment = new Dictionary<int, Equipment>();
		_listWearEquipment = new List<Equipment>();
		_uid = 1;
	}

	public bool BuyEquipment(int id)
	{
		EquipmentTable table;
		if (TableManager.I.GetEquipmentTable(id, out table) == false) { System.Diagnostics.Debug.Assert(false, "Buy ItemTable Null"); }

		_mapHaveEquipment.Add(_uid, new Equipment(_uid++, table.Name, table.Type, table.Stat));
		return true;
	}

	public bool WearEquipment(int id)
	{
		if (_mapHaveEquipment[id] == null) { return false; }

		for (int i = 0; i < _listWearEquipment.Count; i++)
		{
			if(_listWearEquipment[i].Type == _mapHaveEquipment[id].Type)
			{
				_mapHaveEquipment.Add(_listWearEquipment[i].ID, _listWearEquipment[i]);
				_listWearEquipment.RemoveAt(i);
				break;
			}
		}

		_listWearEquipment.Add(_mapHaveEquipment[id]);
		_mapHaveEquipment.Remove(id);
		return true;
	}

	public Equipment GetEquipment(int id)
	{
		return _mapHaveEquipment[id];
	}

	public Equipment GetWearEquipment(int id)
	{
		return _listWearEquipment.Find((c) => c.ID == id);
	}


	public Dictionary<int, Equipment> DicHaveEquipment { get { return _mapHaveEquipment; } }
	public List<Equipment> ListWearEquipment { get { return _listWearEquipment; } }
}
