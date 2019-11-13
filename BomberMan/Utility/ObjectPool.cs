using System.Collections.Generic;

class ObjectPool
{
    private static readonly ObjectPool _instance = new ObjectPool();

    private Dictionary<MAPOBJECT_NAME, List<MapObjectBase>> _dicObjectPool;
    private Dictionary<MapObjectBase, MapObjectBase> _dicSpawnObject;

    public ObjectPool()
    {
        _dicObjectPool = new Dictionary<MAPOBJECT_NAME, List<MapObjectBase>>();
        _dicSpawnObject = new Dictionary<MapObjectBase, MapObjectBase>();

        CreateObject();
    }

    public T PullFromPool<T>(MAPOBJECT_NAME obj) where T : MapObjectBase, new()
    {
        T returnValue = null;
        List<MapObjectBase> pool = null;

        if (_dicObjectPool.TryGetValue(obj, out pool))
        {
            if (pool.Count <= 0)
            {
                T newObject = new T();
                return newObject;
            }

            returnValue = pool[0] as T;
            pool.RemoveAt(0);
            _dicSpawnObject.Add(returnValue, returnValue);
        }

        return returnValue;
    }

    public void PushToPull(MapObjectBase obj)
    {
        if (_dicSpawnObject.TryGetValue(obj, out obj))
        {
            _dicObjectPool[obj.Name()].Add(obj);
            _dicSpawnObject.Remove(obj);
        }
        else { obj = null; }
    }

    /// <summary>
    /// 게임에 나오는 모든 맵오브젝트들은 여기에 추가해줘야 쓸 수 있음
    /// </summary>
    private void CreateObject()
    {
        for (MAPOBJECT_NAME i = 0; i < MAPOBJECT_NAME.MAX; i++)
        {
            switch (i)
            {
                case MAPOBJECT_NAME.PLAYER: { CreateObjectPool<Player>(1); } break;
                case MAPOBJECT_NAME.VAYNE: { CreateObjectPool<Vayne>(5); } break;
                case MAPOBJECT_NAME.BOSS: { CreateObjectPool<Boss>(1); } break;
                case MAPOBJECT_NAME.BLOCK: { CreateObjectPool<Block>(50); } break;
                case MAPOBJECT_NAME.BREAK_BLOCK: { CreateObjectPool<BreakBlock>(50); } break;
                case MAPOBJECT_NAME.BOMB: { CreateObjectPool<Bomb>(8); } break;
                case MAPOBJECT_NAME.EXPLODEBOMB: { CreateObjectPool<ExplodeBomb>(50); } break;
                case MAPOBJECT_NAME.BOMB_KICK: { CreateObjectPool<ItemBombKick>(10); } break;
                case MAPOBJECT_NAME.BOMB_RANGE: { CreateObjectPool<ItemBombRange>(10); } break;
                case MAPOBJECT_NAME.BOMB_COUNT: { CreateObjectPool<ItemBombCount>(10); } break;
                case MAPOBJECT_NAME.MOVE_SPEED: { CreateObjectPool<ItemMoveSpeed>(10); } break;
                case MAPOBJECT_NAME.TRANSFORM_FIRE: { CreateObjectPool<ItemTransformFire>(10); } break;
                case MAPOBJECT_NAME.TRANSFORM_STAR: { CreateObjectPool<ItemTransformStar>(10); } break;
                case MAPOBJECT_NAME.STAGE_PORTAL: { CreateObjectPool<StagePortal>(1); } break;
                case MAPOBJECT_NAME.ARCADE_PORTAL: { CreateObjectPool<ArcadePortal>(1); } break;
                case MAPOBJECT_NAME.SHOP: { CreateObjectPool<Shop>(1); } break;
                case MAPOBJECT_NAME.BAG: { CreateObjectPool<Bag>(1); } break;
                default: break;
            }
        }
    }
    private void CreateObjectPool<T>(int size) where T : MapObjectBase, new()
    {
        List<MapObjectBase> temp = new List<MapObjectBase>();

        for (int i = 0; i < size; i++) { temp.Add(new T()); }

        _dicObjectPool.Add(temp[0].Name(), temp);
    }


    public static ObjectPool I { get { return _instance; } }
}