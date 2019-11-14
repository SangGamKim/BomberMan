using System;

class Bomb : MapObjectBase, IAttackable, IMovable
{

	private Sprite _sprite;

	private Player _player;
	private ExplodeBomb _createBomb;

	private float _explodeTime;
	private float _durationTime;

	private int _range;
	private float _moveSpeed;

	private DIRECTION _direction;
	private Vector2 _tempPosition;

	private bool _isPlayerCollision;

	public Bomb()
	{
		_sprite = new Sprite(2, 0.1f);
		_sprite[0] = '○';
		_sprite[1] = 'ㅇ';
		_explodeTime = 2.0f;
	}

	public override void Start()
	{
		base.Start();
		_direction = DIRECTION.NONE;
		_durationTime = 0;
		_moveSpeed = 5;
		_isPlayerCollision = false;
		Active = true;
	}

	public override void Update(float ticks = 0)
	{
		if (_direction != DIRECTION.NONE) { Move(ticks); }
		else
		{
			_sprite.Update(ticks);

			_durationTime += ticks;
			if (_durationTime >= _explodeTime) { CreateBomb(); }
		}

		if (_isPlayerCollision == false)
		{
			if (_transform.IsCross(_player.Transfrom) == false) { _isPlayerCollision = true; }
		}
	}

	public override void Interaction(ExplodeBomb explodeBomb)
	{
		if (Active == false) { return; }
		if (_createBomb == explodeBomb) { return; }

		if (_transform.IsCross(explodeBomb.Transfrom)) { CreateBomb(); }
	}

	public override void Render()
	{
		Console.SetCursorPosition((int)_transform.Position.X, (int)_transform.Position.Y);
		Console.Write(_sprite.Image);
	}

	public void SetBomb(Player player, int range)
	{
		_player = player;
		_range = range;
		_tempPosition = player.Position;
	}

	public void CreateBomb()
	{
		Active = false;

		if (_player.GameStat.BombCount > _player.InGameBombCount) { _player.InGameBombCount++; }

		bool left = true;
		bool right = true;
		bool top = true;
		bool bot = true;

		Rect rect = new Rect(Vector2.Zero, new Vector2(_transform.Size.X, _transform.Size.Y));

		_createBomb = MapObjectManager.I.CreateObject<ExplodeBomb>(MAPOBJECT_NAME.EXPLODEBOMB, _transform.Position.X, _transform.Position.Y);
		for (int count = 1; count <= _range; count++)
		{
			for (int i = 0; i < MapObjectManager.I.GetDicList(MAPOBJECT_TYPE.OBSTACLE).Count; i++)
			{
				rect.Position = new Vector2(_transform.Position.X - (count * 2), _transform.Position.Y);
				if (MapObjectManager.I.GetDicList(MAPOBJECT_TYPE.OBSTACLE)[i].Transfrom.Position == rect.Position) { left = false; }

				rect.Position = new Vector2(_transform.Position.X + (count * 2), _transform.Position.Y);
				if (MapObjectManager.I.GetDicList(MAPOBJECT_TYPE.OBSTACLE)[i].Transfrom.Position == rect.Position) { right = false; }

				rect.Position = new Vector2(_transform.Position.X, _transform.Position.Y - (1 * count));
				if (MapObjectManager.I.GetDicList(MAPOBJECT_TYPE.OBSTACLE)[i].Transfrom.Position == rect.Position) { top = false; }

				rect.Position = new Vector2(_transform.Position.X, _transform.Position.Y + (1 * count));
				if (MapObjectManager.I.GetDicList(MAPOBJECT_TYPE.OBSTACLE)[i].Transfrom.Position == rect.Position) { bot = false; }
			}

			if (left) { MapObjectManager.I.CreateObject<ExplodeBomb>(MAPOBJECT_NAME.EXPLODEBOMB, _transform.Position.X - (count * 2), _transform.Position.Y); }
			if (right) { MapObjectManager.I.CreateObject<ExplodeBomb>(MAPOBJECT_NAME.EXPLODEBOMB, _transform.Position.X + (count * 2), _transform.Position.Y); }
			if (top) { MapObjectManager.I.CreateObject<ExplodeBomb>(MAPOBJECT_NAME.EXPLODEBOMB, _transform.Position.X, _transform.Position.Y - count); }
			if (bot) { MapObjectManager.I.CreateObject<ExplodeBomb>(MAPOBJECT_NAME.EXPLODEBOMB, _transform.Position.X, _transform.Position.Y + count); }
		}

		MapObjectManager.I.RemoveObjectAdd(this);

	}

	public void Move(float ticks)
	{
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

		for (int i = 0; i < MapObjectManager.I.GetDicList(MAPOBJECT_TYPE.BREAK_OBSTACLE).Count; i++)
		{
			Vector2 tempPos = _transform.Position;

			if (_direction == DIRECTION.RIGHT) { tempPos = new Vector2(tempPos.X + 2, tempPos.Y); }
			else if (_direction == DIRECTION.LEFT) { tempPos = new Vector2(tempPos.X - 2, tempPos.Y); }
			else if (_direction == DIRECTION.UP) { tempPos = new Vector2(tempPos.X, tempPos.Y - 1); }
			else if (_direction == DIRECTION.DOWN) { tempPos = new Vector2(tempPos.X, tempPos.Y + 1); }

			if (tempPos == MapObjectManager.I.GetDicList(MAPOBJECT_TYPE.BREAK_OBSTACLE)[i].Transfrom.Position)
			{
				StopMove();
				return;
			}
		}

		for (int i = 0; i < MapObjectManager.I.GetDicList(MAPOBJECT_TYPE.MONSTER).Count; i++)
		{
			Vector2 tempPos = _transform.Position;

			if (_direction == DIRECTION.RIGHT) { tempPos = new Vector2(tempPos.X + 2, tempPos.Y); }
			else if (_direction == DIRECTION.LEFT) { tempPos = new Vector2(tempPos.X - 2, tempPos.Y); }
			else if (_direction == DIRECTION.UP) { tempPos = new Vector2(tempPos.X, tempPos.Y - 1); }
			else if (_direction == DIRECTION.DOWN) { tempPos = new Vector2(tempPos.X, tempPos.Y + 1); }

			if (tempPos == MapObjectManager.I.GetDicList(MAPOBJECT_TYPE.MONSTER)[i].Transfrom.Position)
			{
				StopMove();
				return;
			}
		}

		if (_direction != DIRECTION.NONE)
		{
			_tempPosition += (GameManager.I().DicDirection[_direction] * _moveSpeed * ticks);

			if (Math.Abs(_tempPosition.X - _transform.Position.X) >= 2.0f) { _transform.Position.X = (int)Math.Round(_tempPosition.X); }
			if (Math.Abs(_tempPosition.Y - _transform.Position.Y) >= 1.0f) { _transform.Position.Y = (int)Math.Round(_tempPosition.Y); }
		}
	}
	public void StopMove()
	{
		_tempPosition = _transform.Position;
		_direction = DIRECTION.NONE;
	}

	public override MAPOBJECT_NAME Name() { return MAPOBJECT_NAME.BOMB; }
	public override MAPOBJECT_TYPE Type() { return MAPOBJECT_TYPE.BREAK_OBSTACLE; }

	public DIRECTION Direction { get { return _direction; } set { _direction = value; } }
}
