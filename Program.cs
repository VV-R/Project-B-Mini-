public class Program
{
    public static Game MainGame;
    public static void Main(string[] args)
    {
        Console.Clear();
        // Create a menu to initialize the player {get a name}
        Player player = Menu();

        player.Gold = 1000;
        MainGame = new Game(player);

        // Add a option dialogue to trigger different Game
        while (MainGame.Running)
        {
            MainLoop();
        }
    }

    public static Player Menu()
    {
        string? name = null;
        while (name == null)
        {
            Console.Write("Please enter your name: ");
            name = Console.ReadLine();
        }
        return new Player(name, World.WeaponByID(1), World.LocationByID(1));
    }

    public static void MainLoop()
    {
        Console.WriteLine("1: Satus\n2: Move\n3: Fight\n4: Map\n5: Location info\n6: Look around\n7: Quit");
        int.TryParse(Console.ReadLine(), out int result);
        switch (result)
        {
            case 1:
                MainGame.PlayerStatus();
                break;
            case 2:
                MainGame.MoveToLocation();
                break;
            case 3:
                MainGame.BattleSequence();
                break;
            case 4:
                MainGame.Map();
                break;
            case 5:
                MainGame.LocationDescription();
                break;
            case 6:
                MainGame.TriggerEvent();
                break;
            case 7:
                System.Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Not a valid option.");
                break;
        }
        Console.ReadKey();
        Console.Clear();
    }
}