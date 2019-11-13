class Sprite
{
    private char[] _spriteImage;
    private int _spriteIndex;

    private readonly float _spriteCustomTime;
    private float _time;

    public Sprite(int spriteCount, float spriteTime)
    {
        _spriteImage = new char[spriteCount];
        _spriteCustomTime = spriteTime;
    }

    public void Update(float ticks)
    {
        _time += ticks;

        if (_time >= _spriteCustomTime)
        {
            _time = 0;
            _spriteIndex++;
            _spriteIndex %= _spriteImage.Length;
        }
    }


    public char this[int i]
    {
        get { return _spriteImage[i]; }
        set { _spriteImage[i] = value; }
    }
    public char Image { get { return _spriteImage[_spriteIndex]; } }
}
