
class Program
{
    [System.STAThread]
    static void Main(string[] args)
    {
        System.Console.CursorVisible = false;
        System.Console.SetWindowSize(150, 40);

        SceneManager.I.Start();
    }
}
