

class MapLobby : MapBase
{
    public override string[] GetData()
    {
        return new string[]
       {
        "BBBBBBB",
        "BG P OB",
        "B     B",
        "B  H  B",
        "BBBBBBB",
       };
    }

    public override MAP_NAME GetName() { return MAP_NAME.LOBBY; }
}
