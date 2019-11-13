

class MapArcadeC : MapBase
{
    public override string[] GetData()
    {
        return new string[]
        {
            "BBBBBBBBBBBBBBBBBBBBBBBBBBBBBB",
            "B              K             B",
            "B   V          B      K  V   B",
            "B        V     B      K      B",
            "B              B             B",
            "BKKKKKKKKKBKKKKBKKKKBBBBBBBBBB",
            "B         B                  B",
            "B     S   B                  B",
            "B    T    B            H     B",
            "BBBBBBBBBBBBBBBBBBBBBBBBBBBBBB",
         };


    }

    public override MAP_NAME GetName() { return MAP_NAME.ARCADE_C; }
}
