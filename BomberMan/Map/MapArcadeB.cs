

class MapArcadeB : MapBase
{
    public override string[] GetData()
    {
        return new string[]
        {
            "BBBBBBBBBBBBBBBBBBBBBBBBBBBBB",
            "B       KKK   B   KKKKK BV  B",
            "BVBKBKB B  BK V KB  BKK BBB B",
            "BBB     B  B BBB B  BBK     B",
            "BKKKKBKKK  K  V  K  KKKKKKKKB",
            "B  B B BB  BKBKBKB  BB B B  B",
            "B    V  KV B  T  B VK  V    B",
            "BB B B BB  BKBKBKB  BB B B BB",
            "BKKKKKKKK  K  V  K  KKKBKKKKB",
            "BKKBKKKBB  B BBB B  B     BBB",
            "B BBBKKKB  BK V KB  B BKBKBVB",
            "B HBKKKKKKK   B   KKK       B",
            "BBBBBBBBBBBBBBBBBBBBBBBBBBBBB",
        };
    }

    public override MAP_NAME GetName() { return MAP_NAME.ARCADE_B; }
}
