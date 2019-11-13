using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class StagePortal : MapObjectBase
{
    public MAP_NAME MapName;

    public StagePortal()
    {
        _image = '포';
    }

    public override void Start()
    {
		Active = false;
    }

    public override void Interaction(Player player)
    {
        if (Active == false) { return; }
        if (_transform.IsCross(player.Transfrom))
        {
            if (MapName == MAP_NAME.LAST - 1) { SceneManager.I.ChangeScene(SCENE_NAME.RESULT_SCENE); }
            else { SceneManager.I.GetCurrentScene<GameScene>().ChangeMap(++MapName); }
        }
    }

    public override void Render()
    {
        if (Active) { base.Render(); }
    }

    public void CheckMonsterCount()
    {
        if (MonsterBase.MonsterCount <= 0) { Active = true; }
    }

    public override MAPOBJECT_NAME Name() { return MAPOBJECT_NAME.STAGE_PORTAL; }
    public override MAPOBJECT_TYPE Type() { return MAPOBJECT_TYPE.INTERACTION; }
}
