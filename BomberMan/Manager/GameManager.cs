using System;
using System.Collections.Generic;

class GameManager
{
    #region SINGLETON

    static GameManager _instance = null;

    static public void CraeteInstance()
    {
        if (_instance == null)
        {
            _instance = new GameManager();
        }
    }

    static public GameManager I() { return _instance; }


    #endregion SINGLETON

    private Dictionary<DIRECTION, Vector2> _dicDirection;

    private int _playerScore;
    private int _playerKill;

    private GameManager()
    {
        _dicDirection = new Dictionary<DIRECTION, Vector2>();

        _dicDirection.Add(DIRECTION.UP, new Vector2(0, -0.5f));
        _dicDirection.Add(DIRECTION.DOWN, new Vector2(0, 0.5f));
        _dicDirection.Add(DIRECTION.LEFT, new Vector2(-1, 0));
        _dicDirection.Add(DIRECTION.RIGHT, new Vector2(1, 0));
        _dicDirection.Add(DIRECTION.NONE, Vector2.Zero);
    }

    public void CheckMonsterCount(int kill, int score)
    {
        _playerScore += score;
        _playerKill += kill;

        var portal = MapObjectManager.I.GetObject<StagePortal>(MAPOBJECT_TYPE.INTERACTION, MAPOBJECT_NAME.STAGE_PORTAL);
        portal.CheckMonsterCount();
    }

    public void CreateItem(Vector2 pos)
    {
        int ran = new Random().Next(0, 10);
        switch (ran)
        {
            case 0: { MapObjectManager.I.CreateObject<ItemBombCount>(MAPOBJECT_NAME.BOMB_COUNT, pos.X, pos.Y); } break;
            case 1: { MapObjectManager.I.CreateObject<ItemBombRange>(MAPOBJECT_NAME.BOMB_RANGE, pos.X, pos.Y); } break;
            case 2: { MapObjectManager.I.CreateObject<ItemMoveSpeed>(MAPOBJECT_NAME.MOVE_SPEED, pos.X, pos.Y); } break;
            case 3: { MapObjectManager.I.CreateObject<ItemTransformFire>(MAPOBJECT_NAME.TRANSFORM_FIRE, pos.X, pos.Y); } break;
            case 4: { MapObjectManager.I.CreateObject<ItemTransformStar>(MAPOBJECT_NAME.TRANSFORM_STAR, pos.X, pos.Y); } break;
            case 5: { MapObjectManager.I.CreateObject<ItemBombKick>(MAPOBJECT_NAME.BOMB_KICK, pos.X, pos.Y); } break;
            default: break;
        }
    }

    public int PlayerScore { get { return _playerScore; } }
    public int PlayerKill { get { return _playerKill; } }

    public Dictionary<DIRECTION, Vector2> DicDirection { get { return _dicDirection; } }
}
