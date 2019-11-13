

abstract class MapObjectBase : ObjectBase
{
    protected char _image;

    protected Rect _transform;

    public MapObjectBase()
    {
        _transform.Size = new Vector2(2.0f, 1.0f);
    }

	public override void Start()
	{
		Active = true;
	}

	public override void Render()
    {
        System.Console.SetCursorPosition((int)_transform.Position.X, (int)_transform.Position.Y);
        System.Console.Write(_image);
    }

    public virtual void Interaction(Player player) { }

    public virtual void Interaction(ExplodeBomb explodeBomb) { }

    public void SetPosition(float x, float y)
    {
        _transform.Position = new Vector2(x, y);
    }

    public abstract MAPOBJECT_NAME Name();
    public abstract MAPOBJECT_TYPE Type();

    public Rect Transfrom { get { return _transform; } }
    public Vector2 Position { get { return _transform.Position; } }
}
