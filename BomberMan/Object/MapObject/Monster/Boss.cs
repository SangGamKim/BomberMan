using System;

class Boss : MonsterBase
{
    private Player _player;

    private string[] _usedImage;
    private string[] _normalImage;
    private string[] _attackImage;

    private float _attackTime;
    private float _stopTime;

    private bool _isAttack;

    public Boss()
    {
        _normalImage = new string[]
        {
            "※※※※",
            "※※※※",
            "※※※※",
            "※※※※",
        };

        _attackImage = new string[]
        {
            "공격공격",
            "공격공격",
            "공격공격",
            "공격공격",
        };

        _usedImage = _normalImage;
        _transform.Size = new Vector2(_usedImage[0].Length * 2, _usedImage.Length);
    }

    public override void Start()
    {
        base.Start();

        MonsterTable table;
        if(TableManager.I.GetMonsterTable(2, out table) == false) { System.Diagnostics.Debug.Assert(false, "MonsterBoss Null"); }
        _stat = table.Stat;

        _isAttack = false;
        _attackTime = 0;
        _stopTime = 0;
    }

    public override void Render()
    {
        for (int i = 0; i < _usedImage.Length; i++)
        {
            Console.SetCursorPosition((int)_transform.Position.X, (int)_transform.Position.Y + i);
            Console.WriteLine(_usedImage[i]);
        }
    }

    public override void Move(float ticks)
    {
        if (_attackTime > 5.0f) { AttackState(); }

        if (_stopTime > 3.0f) { NormalState(); }

        if (_isAttack) { _stopTime += ticks; }
        else
        {
            _attackTime += ticks;
            SetDiction();

            _tempPosition += (GameManager.I().DicDirection[_direction] * _stat.MoveSpeed * ticks);

            if (Math.Abs(_tempPosition.X - _transform.Position.X) >= 2.0f) { _transform.Position.X = (int)Math.Round(_tempPosition.X); }
            if (Math.Abs(_tempPosition.Y - _transform.Position.Y) >= 1.0f) { _transform.Position.Y = (int)Math.Round(_tempPosition.Y); }
        }
    }

    public override void Interaction(ExplodeBomb explodeBomb)
    {
        if (_isAttack) { base.Interaction(explodeBomb); }
    }

    public override void Interaction(Player player)
    {
        if (_isAttack) { base.Interaction(player); }
    }

    private void SetDiction()
    {
        if (_player == null) { _player = MapObjectManager.I.GetObject<Player>(MAPOBJECT_TYPE.PLAYER, MAPOBJECT_NAME.PLAYER); }

        if (_player.Position.X < _transform.Position.X) { _direction = DIRECTION.LEFT; }
        else if (_player.Position.X > _transform.Position.X) { _direction = DIRECTION.RIGHT; }
        else if (_player.Position.Y < _transform.Position.Y) { _direction = DIRECTION.UP; }
        else if (_player.Position.Y > _transform.Position.Y) { _direction = DIRECTION.DOWN; }
        else { _direction = DIRECTION.NONE; }
    }

    private void AttackState()
    {
        _isAttack = true;
        _usedImage = _attackImage;
        _attackTime = 0;
    }
    private void NormalState()
    {
        _isAttack = false;
        _usedImage = _normalImage;
        _stopTime = 0;
    }

    public override MAPOBJECT_NAME Name() { return MAPOBJECT_NAME.BOSS; }
}
