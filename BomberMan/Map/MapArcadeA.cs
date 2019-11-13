

class MapArcadeA : MapBase
{
    public override string[] GetData()
    {
        return new string[]
        {
            "BBBBBBBBBBBBBBBBBBBBBBBBBBBBBB",
            "B H KB      VK               B",
            "BKK  K      KK              TB",
            "B    B      KV               B",
            "BBBBBBBBBBBBBBBBBBBBBBBBBBBBBB",
        };
    }

    public override MAP_NAME GetName() { return MAP_NAME.ARCADE_A; }
}
