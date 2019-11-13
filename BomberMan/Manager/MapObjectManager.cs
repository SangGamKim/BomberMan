using System.Collections.Generic;

/// <summary>
/// 게임에 나오는 유닛관리
/// </summary>
class MapObjectManager
{
    private static readonly MapObjectManager _instance = new MapObjectManager();

    private Dictionary<MAPOBJECT_TYPE, List<MapObjectBase>> _dicObjectList;
    private List<MapObjectBase> _listTempRemoveObject;

	private MapObjectManager()
    {
        _dicObjectList = new Dictionary<MAPOBJECT_TYPE, List<MapObjectBase>>();
        _listTempRemoveObject = new List<MapObjectBase>();

        _dicObjectList.Add(MAPOBJECT_TYPE.OBSTACLE, new List<MapObjectBase>());
        _dicObjectList.Add(MAPOBJECT_TYPE.BREAK_OBSTACLE, new List<MapObjectBase>());
        _dicObjectList.Add(MAPOBJECT_TYPE.INTERACTION, new List<MapObjectBase>());
        _dicObjectList.Add(MAPOBJECT_TYPE.MONSTER, new List<MapObjectBase>());
        _dicObjectList.Add(MAPOBJECT_TYPE.EXPLODE_BOMB, new List<MapObjectBase>());
        _dicObjectList.Add(MAPOBJECT_TYPE.PLAYER, new List<MapObjectBase>());
    }

    public T CreateObject<T>(MAPOBJECT_NAME name, float x, float y) where T : MapObjectBase, new()
    {
        var pool = ObjectPool.I.PullFromPool<T>(name);
        pool.SetPosition(x, y);
        pool.Start();

        _dicObjectList[pool.Type()].Add(pool);

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
            _dicObjectList[_listTempRemoveObject[i].Type()].Remove(_listTempRemoveObject[i]);
            ObjectPool.I.PushToPull(_listTempRemoveObject[i]);
        }

        _listTempRemoveObject.Clear();
    }

	public void DestroyAllOjbect()
	{
		_listTempRemoveObject.Clear();
		foreach (var obj in _dicObjectList)
		{
			for (int i = 0; i < obj.Value.Count; i++) { ObjectPool.I.PushToPull(obj.Value[i]); }
			obj.Value.Clear();
		}
	}

    public T GetObject<T>(MAPOBJECT_TYPE type, MAPOBJECT_NAME name)  where T : MapObjectBase
    {
		return _dicObjectList[type].Find((c) => c.Name() == name) as T;
	}

    public List<MapObjectBase> GetDicList(MAPOBJECT_TYPE type)
    {
        return _dicObjectList[type];
    }

    public Dictionary<MAPOBJECT_TYPE, List<MapObjectBase>> GetDic()
    {
        return _dicObjectList;
    }


    public static MapObjectManager I { get { return _instance; } }
}
