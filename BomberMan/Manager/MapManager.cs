using System.Collections.Generic;

class MapManager
{
    private static readonly MapManager _instance = new MapManager();

    private List<MapBase> _listMap;

    public MapManager()
    {
        _listMap = new List<MapBase>();

        _listMap.Add(new MapLobby());
        _listMap.Add(new MapArcadeA());
        _listMap.Add(new MapArcadeB());
        _listMap.Add(new MapArcadeC());
    }

    // H : 플레이어
    // V : 몬스터 베인
    // S : 보스
    // B : 안부숴지는 블럭
    // K : 부숴지는 블럭
    // O : 샾
    // G : 가방
    // P : 아케이드 시작 포탈
    // T : 스테이지 넘기는 포탈

    /// <summary>
    /// 씬에 플레이어 등록 꼭 해줘야함
    /// 맵에 미리 나올거 정의
    /// </summary>
    /// <param name="mapName"></param>
    /// <returns></returns>
    public Player ChangeMap(MAP_NAME mapName)
    {
        MapObjectManager mom = MapObjectManager.I;

        Player returnValue = null;
		MonsterBase.MonsterCount = 0;

        for (int y = 0; y < _listMap[(int)mapName].GetData().Length; y++)
        {
            for (int x = 0; x < _listMap[(int)mapName].GetData()[y].Length; x++)
            {
                switch (_listMap[(int)mapName].GetData()[y][x])
                {
                    case 'H': { returnValue = mom.CreateObject<Player>(MAPOBJECT_NAME.PLAYER, x * 2, y); } break;
                    case 'V': { mom.CreateObject<Vayne>(MAPOBJECT_NAME.VAYNE, x * 2, y); MonsterBase.MonsterCount++; } break;
                    case 'S': { mom.CreateObject<Boss>(MAPOBJECT_NAME.BOSS, x * 2, y); MonsterBase.MonsterCount++; } break;
                    case 'B': { mom.CreateObject<Block>(MAPOBJECT_NAME.BLOCK, x * 2, y); } break;
                    case 'K': { mom.CreateObject<BreakBlock>(MAPOBJECT_NAME.BREAK_BLOCK, x * 2, y); } break;
                    case 'O': { mom.CreateObject<Shop>(MAPOBJECT_NAME.SHOP, x * 2, y); } break;
                    case 'G': { mom.CreateObject<Bag>(MAPOBJECT_NAME.BAG, x * 2, y); } break;
                    case 'P': { mom.CreateObject<ArcadePortal>(MAPOBJECT_NAME.ARCADE_PORTAL, x * 2, y); } break;
                    case 'T':
                        {
                            StagePortal portal = mom.CreateObject<StagePortal>(MAPOBJECT_NAME.STAGE_PORTAL, x * 2, y);
                            portal.MapName = mapName;
                        }
                        break;
                }
            }
        }

        return returnValue;
    }


    public static MapManager I { get { return _instance; } }
}
