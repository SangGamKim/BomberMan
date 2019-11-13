
/// <summary>
/// 오브젝트 폴더 안에 있는 모든것들의 부모클래쓰
/// </summary>
abstract class ObjectBase
{
	public bool Active;

	public virtual void Start() { }
	public virtual void Update(float ticks = 0) { }
	public virtual void LastUpdate() { }
	public virtual void Render() { }
}
