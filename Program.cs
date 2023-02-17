public class Program
{
    public static Game MainGame = new();
    public static void Main(string[] args)
    {
        // Create a menu to initialize the player {get a name}
        Menu();

        MainGame.PlayerOne.SetLocation(World.LocationByID(1));
        MainGame.PlayerOne.SetWeapon(World.WeaponByID(1));
        // Add a option dialogue to trigger different Game
        while (MainGame.Running)
        {
            MainLoop();
        }


    }

    public static void Menu()
    {
        string? name = null;
        while (name == null)
        {
            Console.Write("Please enter your name: ");
            name = Console.ReadLine();
        }

        Player playerOne = new(name);
        MainGame.SetMainPlayer(playerOne);
    }

    public static void MainLoop()
    {
        Console.WriteLine("1: Satus\n2: Move\n3: Fight\n4: Map\n5: Location info\n6: Quit");
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
                MainGame.Running = false;
                break;
            default:
                Console.WriteLine("Not a valid option.");
                break;
        }
        Console.ReadKey();
        Console.Clear();
    }
}