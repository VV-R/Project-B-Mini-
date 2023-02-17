public class Game
{
    public Player PlayerOne;
    public bool Running = true;
    // public double ChanceOfDouble = 0.2;


    public void SetMainPlayer(Player player) => PlayerOne = player;

    public void BattleSequence()
    {
        Location currentLocation = PlayerOne.GetLocation();
        if (currentLocation.MonsterLivingHere == null)
        {
            Console.WriteLine($"There is are no monsters in {currentLocation.Name}.");
            return;
        }

        Monster currentMonster = currentLocation.MonsterLivingHere;

        // For if we want to add double battles
        // double result = World.RandomGenerator.NextDouble();
        // bool isDoubleBattle = result < ChanceOfDouble;

        Console.WriteLine($"A wild {currentMonster.Name} appeared");
        while (currentMonster.CurrentHitPoints > 0 || PlayerOne.CurrentHitPoints > 0)
        {
            Console.WriteLine($"{currentMonster.Name} HP: {currentMonster.CurrentHitPoints}/{currentMonster.CurrentHitPoints}");
            Console.WriteLine($"{PlayerOne.Name} HP: {PlayerOne.CurrentHitPoints}/{PlayerOne.MaximumHitPoints}");

            Console.WriteLine("1: Fight\n 2: Flee");
            int.TryParse(Console.ReadLine(), out int choice);

            if (choice == 1)
            {
                int playerDamage = PlayerOne.DealDamage();
                Console.WriteLine($"You hit {currentMonster.Name} for {playerDamage} points of damage!!");
            }


            int monsterDamage = currentMonster.DealDamage();
            PlayerOne.TakeDamage(monsterDamage);
            Console.WriteLine($"{currentMonster.Name} did {monsterDamage} points of damage!");
            Console.ReadKey();
            Console.Clear();
        }
    }

    public void MoveToLocation()
    {
        Location currentLocation = PlayerOne.GetLocation();
        Console.WriteLine("Where would you like to go?");
        Console.WriteLine($"You are at: {currentLocation.Name}. From here you can go:");
        Console.WriteLine(currentLocation.Compass());

        Console.Write("\nEnter a direction (Arrow keys/NSWE): ");
        ConsoleKeyInfo direction = Console.ReadKey();
        Console.WriteLine();
        switch (direction.Key)
        {
            // West
            case ConsoleKey.LeftArrow:
            case ConsoleKey.W:
                if (currentLocation.LocationToEast != null)
                    PlayerOne.SetLocation(currentLocation.LocationToEast);
                else
                    Console.WriteLine("There is nothing towards the West.");
                break;

            // North
            case ConsoleKey.UpArrow:
            case ConsoleKey.N:
                if (currentLocation.LocationToNorth != null)
                    PlayerOne.SetLocation(currentLocation.LocationToNorth);
                else
                    Console.WriteLine("There is nothing towards the North.");
                break;

            // East
            case ConsoleKey.RightArrow:
            case ConsoleKey.E:
                if (currentLocation.LocationToEast != null)
                    PlayerOne.SetLocation(currentLocation.LocationToEast);
                else
                    Console.WriteLine("There is nothing towards the East.");
                break;

            // South
            case ConsoleKey.DownArrow:
            case ConsoleKey.S:
                if (currentLocation.LocationToSouth != null)
                    PlayerOne.SetLocation(currentLocation.LocationToSouth);
                else
                    Console.WriteLine("There is nothing towards the South.");
                break;

            default:
                Console.WriteLine($"({direction.Key}) is not a valid move input.");
                break;
        }

        if (currentLocation != PlayerOne.GetLocation())
        {
            currentLocation = PlayerOne.GetLocation();
            Console.WriteLine($"You are now at: {currentLocation.Name}.");
        }
    }

    public void PlayerStatus()
    {
        // Print out the current stat of the player {maybe even current quests}
        Console.WriteLine($"HP: {PlayerOne.CurrentHitPoints}/{PlayerOne.MaximumHitPoints}");
        Console.WriteLine($"Inventory:\n{PlayerOne.DisplayInventory()}");
        Console.WriteLine($"Quests:\n{PlayerOne.DisplayQuests()}");
    }

    public void Map()
    {
        // Print out a map with legend
        Console.WriteLine();
        Console.WriteLine("      P\n      A\n    VFTGBS\n      H");
        Console.WriteLine("\n\nLegend:");
        Console.WriteLine("  H: Your house");
        Console.WriteLine("  T: Town square");
        Console.WriteLine("  F: Farmer");
        Console.WriteLine("  V: Farmer's field");
        Console.WriteLine("  P: Alchemist's garden");
        Console.WriteLine("  B: Bridge");
        Console.WriteLine("  S: Spider forest");
    }

    public void LocationDescription()
    {
        // Print out the current location a description
        Location currentLocation = PlayerOne.GetLocation();
        Console.WriteLine($"You are at: {currentLocation.Name}.");
        Console.WriteLine($"Description: {currentLocation.Description}");
    }

    public void Dialogue()
    {
        // Have an option to talk with some one on the location to get quests or to get information
    }

}