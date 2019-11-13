using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class ExplodeBomb : MapObjectBase
{
    private Sprite _sprite;

    private float _destroyTime;
    private float _durationTime;

    public ExplodeBomb()
    {
        _destroyTime = 0.2f;

        _sprite = new Sprite(3, 0.07f);
        _sprite[0] = '▩';
        _sprite[1] = '▦';
        _sprite[2] = '▒';
    }

    public override void Start()
    {
        _durationTime = 0;
    }

    public override void Update(float ticks = 0)
    {
        _sprite.Update(ticks);

        _durationTime += ticks;
        if(_durationTime >= _destroyTime) { MapObjectManager.I.RemoveObjectAdd(this); }
    }

    public override void Interaction(Player player)
    {
        if (_transform.IsCross(player.Transfrom)) { player.Hurt(); }
    }

    public override void Render()
    {
        Console.SetCursorPosition((int)_transform.Position.X, (int)_transform.Position.Y);
        Console.Write(_sprite.Image);
    }


    public override MAPOBJECT_NAME Name() { return MAPOBJECT_NAME.EXPLODEBOMB; }
    public override MAPOBJECT_TYPE Type() { return MAPOBJECT_TYPE.EXPLODE_BOMB; }
}
