namespace PR210Engine;

class Program
{
    private static Game? game;

    static void Main(string[] argStrings)
    {
        game = new(800, 600, "PR210Engine");
        game.Run();
    }
}