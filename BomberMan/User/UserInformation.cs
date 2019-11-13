/// <summary>
/// 유저의 돈이나 장비 소모품등 가지고 있는 클래스
/// </summary>
class UserInformation
{
	private static UserInformation _instance = new UserInformation();

	private UserInventory _inventory;

	private UserInformation()
	{
		_inventory = new UserInventory();
	}

	public UserInventory Inventory { get { return _inventory; } }

	public static UserInformation I { get { return _instance; } }
}
