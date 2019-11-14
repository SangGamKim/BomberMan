using System;

public struct MonsterStat
{
	public int Life;
	public float MoveSpeed;
	public int PlayerScore;

	public MonsterStat(int life, float movSpeed, int score)
	{
		Life = life;
		MoveSpeed = movSpeed;
		PlayerScore = score;
	}
}


abstract class MonsterBase : MapObjectBase, IMovable
{
	public static int MonsterCount;

	protected MonsterStat _stat;

	protected DIRECTION _direction;

	protected Vector2 _tempPosition;

	public override void Start()
	{
		base.Start();
		_direction = (DIRECTION)new Random().Next(0, 4);
		_tempPosition = _transform.Position;
	}

	protected void Init(int id)
	{
		MonsterTable table;
		if (TableManager.I.GetMonsterTable(id, out table) == false) { System.Diagnostics.Debug.Assert(false, "MonsterBoss Null"); }
		_stat = table.Stat;
	}

	public override void Update(float ticks = 0)
	{
		Move(ticks);
	}

	public override void Interaction(Player player)
	{
		if (_transform.IsCross(player.Transfrom)) { player.Hurt(); }
	}

	public override void Interaction(ExplodeBomb explodeBomb)
	{
		if (_transform.IsCross(explodeBomb.Transfrom)) { Hurt(); }
	}

	public virtual void Move(float ticks)
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

		if (_direction != DIRECTION.NONE)
		{
			_tempPosition += (GameManager.I().DicDirection[_direction] * _stat.MoveSpeed * ticks);

			if (Math.Abs(_tempPosition.X - _transform.Position.X) >= 2.0f) { _transform.Position.X = (int)Math.Round(_tempPosition.X); }
			if (Math.Abs(_tempPosition.Y - _transform.Position.Y) >= 1.0f) { _transform.Position.Y = (int)Math.Round(_tempPosition.Y); }
		}
	}
	public void StopMove()
	{
		_tempPosition = _transform.Position;
		_direction = (DIRECTION)new Random().Next(0, 4);
	}

	private void Hurt()
	{
		if (Active == false) { return; }

		_stat.Life--;

		if (_stat.Life <= 0)
		{
			Active = false;

			MonsterCount--;
			MapObjectManager.I.RemoveObjectAdd(this);

			GameManager.I().CheckMonsterCount(1, _stat.PlayerScore);
		}
	}

	public override MAPOBJECT_TYPE Type() { return MAPOBJECT_TYPE.MONSTER; }
}
