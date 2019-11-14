using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public struct PlayerStat
{
	public int AtkRange;
	public int BombCount;
	public float MoveSpeed;

	public int MaxAtkRange;
	public int MaxBombCount;
	public float MaxMoveSpeed;

	public PlayerStat(int atkRange, int bombCount, float mSpeed, int maxRange, int maxCount, float maxSpeed)
	{
		AtkRange = atkRange;
		BombCount = bombCount;
		MoveSpeed = mSpeed;

		MaxAtkRange = maxRange;
		MaxBombCount = maxCount;
		MaxMoveSpeed = maxSpeed;
	}

	public static PlayerStat operator +(PlayerStat a, PlayerStat b)
	{
		return new PlayerStat(a.AtkRange + b.AtkRange, a.BombCount + b.BombCount,
			a.MoveSpeed + b.MoveSpeed, a.MaxAtkRange + b.MaxAtkRange,
			a.MaxBombCount + b.MaxBombCount, a.MaxMoveSpeed + b.MaxMoveSpeed);
	}
	public static void ApplyItemStat(int atkRange, int bombCount, float moveSpeed, ref PlayerStat stat)
	{
		stat.AtkRange += atkRange;
		stat.BombCount += bombCount;
		stat.MoveSpeed = moveSpeed;
	}
}

class Player : MapObjectBase, IMovable, IAttackable
{
	public int InGameBombCount;

	private PlayerStat _itemStat;
	private PlayerStat _rideStat;
	private PlayerStat _baseStat;

	private DIRECTION _direction;
	public Vector2 _tempPosition;   //실시간으로 플레이어가 이동하는게 아니라   temp포지션을 먼저 이동해서 x는 2씩 y는 1씩 변화시켜줘서 위치잡음

	private DateTime _hurtOldTime;
	private readonly float _ResetTime; //플레이어가 펫을 타고 있다가 죽었을 때 트리거가 연속으로 되기 때문에 몇초동안은 피해 안입도록

	private RIDE_TYPE _rideType;

	private bool _haveKick;

	public Player()
	{
		_ResetTime = 1.0f;
	}

	public override void Start()
	{
		PlayerTable table;
		if (TableManager.I.GetPlayerTable(1, out table) == false) { System.Diagnostics.Debug.Assert(false, "Player Table Null"); }

		_baseStat = table.Stat;
		_itemStat = new PlayerStat();
		foreach (var eq in UserInformation.I.Inventory.ListWearEquipment) { _baseStat += eq.Stat; }

		if (SceneManager.I.CurrentScene.Name() == SCENE_NAME.GAME_SCENE) { InGameBombCount = _baseStat.BombCount; }
		else { InGameBombCount = 0; }

		SetRide('♀', RIDE_TYPE.NULL);
		_tempPosition = _transform.Position;
		_haveKick = false;
	}

	public override void Update(float ticks = 0)
	{
		if (GetKey.Down(KEY_TYPE.SPACE)) { CreateBomb(); }

		Move(ticks);
	}

	public void Move(float ticks)
	{
		if (GetKey.Press(KEY_TYPE.UP)) { _direction = DIRECTION.UP; }
		else if (GetKey.Press(KEY_TYPE.DOWN)) { _direction = DIRECTION.DOWN; }
		else if (GetKey.Press(KEY_TYPE.LEFT)) { _direction = DIRECTION.LEFT; }
		else if (GetKey.Press(KEY_TYPE.RIGHT)) { _direction = DIRECTION.RIGHT; }

		if (GetKey.State() == KEY_STATE.UN_PRESS)
		{
			_direction = DIRECTION.NONE;
			return;
		}

		for (int i = 0; i < MapObjectManager.I.GetDicList(MAPOBJECT_TYPE.OBSTACLE).Count; i++)
		{
			Vector2 tempPos = _transform.Position;

			if (_direction == DIRECTION.RIGHT) { tempPos = new Vector2(tempPos.X + 2, tempPos.Y); }
			else if (_direction == DIRECTION.LEFT) { tempPos = new Vector2(tempPos.X - 2, tempPos.Y); }
			else if (_direction == DIRECTION.UP) { tempPos = new Vector2(tempPos.X, tempPos.Y - 1); }
			else if (_direction == DIRECTION.DOWN) { tempPos = new Vector2(tempPos.X, tempPos.Y + 1); }

			if (tempPos == MapObjectManager.I.GetDicList(MAPOBJECT_TYPE.OBSTACLE)[i].Transfrom.Position)
			{
				StopMove();
				return;
			}
		}

		if (_rideType != RIDE_TYPE.STAR)
		{
			for (int i = 0; i < MapObjectManager.I.GetDicList(MAPOBJECT_TYPE.BREAK_OBSTACLE).Count; i++)
			{
				Vector2 tempPos = _transform.Position;

				if (_direction == DIRECTION.RIGHT) { tempPos = new Vector2(tempPos.X + 2, tempPos.Y); }
				else if (_direction == DIRECTION.LEFT) { tempPos = new Vector2(tempPos.X - 2, tempPos.Y); }
				else if (_direction == DIRECTION.UP) { tempPos = new Vector2(tempPos.X, tempPos.Y - 1); }
				else if (_direction == DIRECTION.DOWN) { tempPos = new Vector2(tempPos.X, tempPos.Y + 1); }

				if (tempPos == MapObjectManager.I.GetDicList(MAPOBJECT_TYPE.BREAK_OBSTACLE)[i].Transfrom.Position)
				{
					Kick(MapObjectManager.I.GetDicList(MAPOBJECT_TYPE.BREAK_OBSTACLE)[i]);
					StopMove();
					return;
				}
			}
		}

		if (_direction != DIRECTION.NONE)
		{
			if (_rideType == RIDE_TYPE.NULL) { _tempPosition += (GameManager.I().DicDirection[_direction] * GameStat.MoveSpeed * ticks); }
			else { _tempPosition += (GameManager.I().DicDirection[_direction] * _rideStat.MoveSpeed * ticks); }

			if (Math.Abs(_tempPosition.X - _transform.Position.X) >= 2.0f) { _transform.Position.X = (int)Math.Round(_tempPosition.X); }
			if (Math.Abs(_tempPosition.Y - _transform.Position.Y) >= 1.0f) { _transform.Position.Y = (int)Math.Round(_tempPosition.Y); }
		}
	}
	public void StopMove()
	{
		_tempPosition = _transform.Position;
		_direction = DIRECTION.NONE;
	}

	public void ReturnPos(Vector2 pos)
	{
		_tempPosition = pos;
		_transform.Position = pos;
	}

	public void GetItem(MAPOBJECT_NAME itemName)
	{
		switch (itemName)
		{
			case MAPOBJECT_NAME.BOMB_COUNT:
				{
					if (GameStat.BombCount < GameStat.MaxBombCount)
					{
						InGameBombCount++;
						PlayerStat.ApplyItemStat(0, 1, 0, ref _itemStat);
					}
				}
				break;
			case MAPOBJECT_NAME.BOMB_RANGE:
				{
					if (GameStat.AtkRange < GameStat.MaxAtkRange) { PlayerStat.ApplyItemStat(1, 0, 0, ref _itemStat); }
				}
				break;
			case MAPOBJECT_NAME.MOVE_SPEED:
				{
					if (GameStat.MoveSpeed < GameStat.MaxMoveSpeed) { PlayerStat.ApplyItemStat(0, 0, 1, ref _itemStat); }
				}
				break;
			case MAPOBJECT_NAME.BOMB_KICK:
				{
					_haveKick = true;
				}
				break;
			case MAPOBJECT_NAME.TRANSFORM_FIRE:
				{
					ItemTable table;
					if (TableManager.I.GetItemTable(1, out table) == false) { System.Diagnostics.Debug.Assert(false, "Item Table Null"); }

					SetRide('♨', RIDE_TYPE.FIRE, table.Stat);
				}
				break;
			case MAPOBJECT_NAME.TRANSFORM_STAR:
				{
					ItemTable table;
					if (TableManager.I.GetItemTable(2, out table) == false) { System.Diagnostics.Debug.Assert(false, "Item Table Null"); }

					SetRide('★', RIDE_TYPE.STAR, table.Stat);
				}
				break;
			default: break;
		}
	}

	public void CreateBomb()
	{
		if (InGameBombCount <= 0) { return; }

		for (int i = 0; i < MapObjectManager.I.GetDicList(MAPOBJECT_TYPE.OBSTACLE).Count; i++)
		{
			if (MapObjectManager.I.GetDicList(MAPOBJECT_TYPE.OBSTACLE)[i].Position == _transform.Position) { return; }
		}

		for (int i = 0; i < MapObjectManager.I.GetDicList(MAPOBJECT_TYPE.BREAK_OBSTACLE).Count; i++)
		{
			if (MapObjectManager.I.GetDicList(MAPOBJECT_TYPE.BREAK_OBSTACLE)[i].Position == _transform.Position) { return; }
		}

		InGameBombCount--;
		Bomb bomb = MapObjectManager.I.CreateObject<Bomb>(MAPOBJECT_NAME.BOMB, _transform.Position.X, _transform.Position.Y);
		bomb.SetBomb(this, GameStat.AtkRange);
	}

	public void Hurt()
	{
		TimeSpan temp = DateTime.Now - _hurtOldTime;

		if (temp.Seconds < _ResetTime) { return; }

		if (_rideType != RIDE_TYPE.NULL)
		{
			_hurtOldTime = DateTime.Now;

			SetRide('♀', RIDE_TYPE.NULL);
		}
		else { SceneManager.I.ChangeScene(SCENE_NAME.RESULT_SCENE); }
	}

	private void SetRide(char img, RIDE_TYPE type, PlayerStat stat = new PlayerStat())
	{
		if (_rideType != RIDE_TYPE.NULL && type != RIDE_TYPE.NULL) { return; }

		_image = img;
		_rideStat = stat;
		_rideType = type;
	}

	private void Kick(MapObjectBase obj)
	{
		if (obj.Name() != MAPOBJECT_NAME.BOMB) { return; }
		if (_rideType != RIDE_TYPE.NULL) { return; }
		if (_haveKick == false) { return; }

		if (_tempPosition - obj.Position != Vector2.Zero)
		{
			Bomb bomb = obj as Bomb;
			bomb.Direction = _direction;
		}
	}

	public override MAPOBJECT_NAME Name() { return MAPOBJECT_NAME.PLAYER; }
	public override MAPOBJECT_TYPE Type() { return MAPOBJECT_TYPE.PLAYER; }

	public bool HaveKick { get { return _haveKick; } }
	public RIDE_TYPE RideType { get { return _rideType; } }
	public DIRECTION Direction { get { return _direction; } }
	public PlayerStat GameStat { get { return _baseStat + _itemStat; } }
}
