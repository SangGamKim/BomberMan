using System.Collections.Generic;

/// <summary>
/// 게임에 나오는 유닛관리
/// </summary>
class MapObjectManager
{
    private static readonly MapObjectManager _instance = new MapObjectManager();

    private Dictionary<MAPOBJECT_TYPE, List<MapObjectBase>> _mapObjectList;
    private List<MapObjectBase> _listTempRemoveObject;

	private MapObjectManager()
    {
        _mapObjectList = new Dictionary<MAPOBJECT_TYPE, List<MapObjectBase>>();
        _listTempRemoveObject = new List<MapObjectBase>();

        _mapObjectList.Add(MAPOBJECT_TYPE.OBSTACLE, new List<MapObjectBase>());
        _mapObjectList.Add(MAPOBJECT_TYPE.BREAK_OBSTACLE, new List<MapObjectBase>());
        _mapObjectList.Add(MAPOBJECT_TYPE.INTERACTION, new List<MapObjectBase>());
        _mapObjectList.Add(MAPOBJECT_TYPE.MONSTER, new List<MapObjectBase>());
        _mapObjectList.Add(MAPOBJECT_TYPE.EXPLODE_BOMB, new List<MapObjectBase>());
        _mapObjectList.Add(MAPOBJECT_TYPE.PLAYER, new List<MapObjectBase>());
    }

    public T CreateObject<T>(MAPOBJECT_NAME name, float x, float y) where T : MapObjectBase, new()
    {
        var pool = ObjectPool.I.PullFromPool<T>(name);
        pool.SetPosition(x, y);
        pool.Start();

        _mapObjectList[pool.Type()].Add(pool);

        return pool as T;
    }

	/// <summary>
	/// 서로 상호작용을 해야해서 바로 지우지 않고 임시로 담아둠
	/// </summary>
	/// <param name="obj"></param>
    public void RemoveObjectAdd(MapObjectBase obj)
    {
        _listTempRemoveObject.Add(obj);
    }

	/// <summary>
	/// 상호작용이 끝난 후 지울 것들을 지움
	/// </summary>
    public void DestroyObject()
    {
        for (int i = 0; i < _listTempRemoveObject.Count; i++)
        {
            _mapObjectList[_listTempRemoveObject[i].Type()].Remove(_listTempRemoveObject[i]);
            ObjectPool.I.PushToPull(_listTempRemoveObject[i]);
        }

        _listTempRemoveObject.Clear();
    }

	public void DestroyAllOjbect()
	{
		_listTempRemoveObject.Clear();
		foreach (var obj in _mapObjectList)
		{
			for (int i = 0; i < obj.Value.Count; i++) { ObjectPool.I.PushToPull(obj.Value[i]); }
			obj.Value.Clear();
		}
	}

    public T GetObject<T>(MAPOBJECT_TYPE type, MAPOBJECT_NAME name)  where T : MapObjectBase
    {
		return _mapObjectList[type].Find((c) => c.Name() == name) as T;
	}

    public List<MapObjectBase> GetObjectList(MAPOBJECT_TYPE type)
    {
        return _mapObjectList[type];
    }

    public Dictionary<MAPOBJECT_TYPE, List<MapObjectBase>> GetMap()
    {
        return _mapObjectList;
    }


    public static MapObjectManager I { get { return _instance; } }
}
