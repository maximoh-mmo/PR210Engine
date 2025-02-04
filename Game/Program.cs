namespace PR210Game;

class Program
{
    private static Game.Game? game;

    private static void Main(string[] argStrings)
    {
        game = new(800, 600, "PR210Engine");
        game.Run();
    }
}