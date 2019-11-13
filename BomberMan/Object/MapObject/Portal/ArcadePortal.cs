

class ArcadePortal : MapObjectBase
{
	public ArcadePortal()
	{
		_image = '포';
	}

	public override void Interaction(Player player)
	{
		if (_transform.IsCross(player.Transfrom)) { SceneManager.I.ChangeScene(SCENE_NAME.GAME_SCENE); }
	}


	public override MAPOBJECT_NAME Name() { return MAPOBJECT_NAME.ARCADE_PORTAL; }
	public override MAPOBJECT_TYPE Type() { return MAPOBJECT_TYPE.INTERACTION; }
}
