using System.Windows.Input;

public enum KEY_TYPE
{
	UP = Key.Up,
	DOWN = Key.Down,
	LEFT = Key.Left,
	RIGHT = Key.Right,
	SPACE = Key.Space,
	ESC = Key.Escape,
	ENTER = Key.Enter,
	D1 = Key.D1,
	D2 = Key.D2,

	NONE = 1,
}

public enum KEY_STATE
{
	UN_PRESS,
	PRESS,
	DOWN,
}

public enum SCENE_NAME
{
    NONE_SCENE = -1,
    INTRO_SCENE,
    SELECT_SCENE,
    LOBBY_SCENE,
    GAME_SCENE,
    RESULT_SCENE,
}

public enum UI_NAME
{
    NONE,
    INTRO_PAGE,
    SELECT_PAGE,
    LOBBY_PAGE,
    GAME_PAGE,
    RESULT_PAGE,
    SHOP_STACK,
    BAG_STACK,
    OPTION_STACK,
    BUY_QUESTION_POPUP,
    EQUIPINFO_POPUP,
}

public enum UI_TYPE
{
    PAGE,
    STACK,
    POPUP,
}

public enum MAPOBJECT_NAME
{
    PLAYER,

    VAYNE,
    BOSS,

	BLOCK,
    BREAK_BLOCK,

    SHOP,
    BAG,

    BOMB,
    EXPLODEBOMB,

    BOMB_KICK,
    BOMB_RANGE,
    BOMB_COUNT,
    MOVE_SPEED,
    TRANSFORM_FIRE,
    TRANSFORM_STAR,

    ARCADE_PORTAL,
    STAGE_PORTAL,

    MAX,
}

public enum MAPOBJECT_TYPE
{
    PLAYER,
    MONSTER,
    INTERACTION,
    OBSTACLE,
    BREAK_OBSTACLE,
    EXPLODE_BOMB,
}

public enum DIRECTION
{
    NONE = -1,
    UP,
    DOWN,
    LEFT,
    RIGHT,
}

public enum MAP_NAME
{
    LOBBY,
    ARCADE_A,
    ARCADE_B,
    ARCADE_C,

	LAST,
}

public enum RIDE_TYPE
{
    NULL,
    FIRE,
    STAR,
}

public enum EQUIPMENT_TYPE
{
	WEAPON,
	SHOES,
}
