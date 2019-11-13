using System.Windows.Input;

static public class Utility
{
	public static readonly long DeltaTime = (10000 * 1000 / _frame); //프레임은 보통 30이상으로 해줘야 캐릭터 이동속도에 대처 가능
	private const long _frame = 30;
}

sealed class InputKey
{
	private static readonly InputKey _instance = new InputKey();

	private KEY_TYPE _type;
	private KEY_STATE _state;

	private InputKey()
	{
		_type = KEY_TYPE.NONE;
		_state = KEY_STATE.UN_PRESS;
	}

	public void Update()
	{
		GetKeyUp();
	}

	public bool GetKeyDown(KEY_TYPE type)
	{
		if (_state != KEY_STATE.DOWN)
		{
			if (Keyboard.IsKeyDown((Key)type))
			{
				_type = type;
				_state = KEY_STATE.DOWN;
				return true;
			}
		}
		return false;
	}

	public bool GetKeyPress(KEY_TYPE type)
	{
		if (GetKeyDown(type))
		{
			_state = KEY_STATE.PRESS;
			return true;
		}
		return false;
	}


	private void GetKeyUp()
	{
		if (Keyboard.IsKeyUp((Key)_type)) { _state = KEY_STATE.UN_PRESS; }
	}


	public KEY_STATE State { get { return _state; } set { _state = value; } }
	public static InputKey I { get { return _instance; } }
}

static class GetKey
{
	public static void Update()
	{
		InputKey.I.Update();
	}

	public static bool Down(KEY_TYPE type)
	{
		return InputKey.I.GetKeyDown(type);
	}

	public static bool Press(KEY_TYPE type)
	{
		return InputKey.I.GetKeyPress(type);
	}

	public static KEY_STATE State()
	{
		return InputKey.I.State;
	}

	public static void ChangeState(KEY_STATE state)
	{
		InputKey.I.State = state;
	}
}

public struct Vector2
{
	public float X;
	public float Y;

	public Vector2(float x, float y)
	{
		X = x;
		Y = y;
	}

	#region 오류 
	public override bool Equals(object obj) { return base.Equals(obj); }
	public override int GetHashCode() { return base.GetHashCode(); }
	#endregion

	public static Vector2 operator +(Vector2 a, Vector2 b)
	{
		return new Vector2(a.X + b.X, a.Y + b.Y);
	}

	public static Vector2 operator -(Vector2 a, Vector2 b)
	{
		return new Vector2(a.X - b.X, a.Y - b.Y);
	}

	public static Vector2 operator *(Vector2 a, float b)
	{
		return new Vector2(a.X * b, a.Y * b);
	}

	public static bool operator ==(Vector2 a, Vector2 b)
	{
		return (a.X == b.X && a.Y == b.Y);
	}

	public static bool operator !=(Vector2 a, Vector2 b)
	{
		return !(a == b);
	}


    public static Vector2 Zero { get { return new Vector2(0, 0); } }
}

public struct Rect
{
	public Vector2 Position;
	public Vector2 Size;

	private float _right => Position.X + Size.X;
	private float _bottom => Position.Y + Size.Y;

	public Rect(Vector2 pos, Vector2 size)
	{
		Position.X = pos.X;
		Position.Y = pos.Y;
		Size.X = size.X;
		Size.Y = size.Y;
	}

	public bool IsCross(Rect rt)
	{
		return !(Position.X >= (rt.Position.X + rt.Size.X) || (Position.X + Size.X) <= rt.Position.X ||
				 Position.Y >= (rt.Position.Y + rt.Size.Y) || (Position.Y + Size.Y) <= rt.Position.Y);
	}
	public float GetCrossArea(Rect rt)
	{
		if (IsCross(rt) == false) { return 0; }

		float px = Position.X > rt.Position.X ? Position.X : rt.Position.X;
		float lx = _right < rt._right ? _right : rt._right;
		px = lx - px;

		float py = Position.Y > rt.Position.Y ? Position.Y : rt.Position.Y;
		float ly = _bottom < rt._bottom ? _bottom : rt._bottom;
		py = ly - py;

		return px * py;
	}
}