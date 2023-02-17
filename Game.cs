public class Game
{
    public Player PlayerOne;
    public bool Running = true;
    public Location CurrentLocation;
    public bool PassCheck = false;
    // public double ChanceOfDouble = 0.2;

    public Game(Player player)
    {
        CurrentLocation = World.LocationByID(1);
        PlayerOne = player;
    }

    public void BattleSequence()
    {
        if (CurrentLocation.MonsterLivingHere == null)
        {
            Console.WriteLine($"There is are no monsters in {CurrentLocation.Name}.");
            return;
        }

        Monster currentMonster = CurrentLocation.MonsterLivingHere;

        // For if we want to add double battles
        // double result = World.RandomGenerator.NextDouble();
        // bool isDoubleBattle = result < ChanceOfDouble;

        Console.WriteLine($"A wild {currentMonster.Name} appeared");
        while (currentMonster.CurrentHitPoints > 0 || PlayerOne.CurrentHitPoints > 0)
        {
            Console.WriteLine($"{currentMonster.Name} HP: {currentMonster.CurrentHitPoints}/{currentMonster.MaximumHitPoints}");
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
        if (PlayerOne.CurrentHitPoints > 0)
        {
            Console.WriteLine("You have lost....");
            Console.WriteLine("You go back home and rest up.");
            PlayerOne.SetLocation(World.LocationByID(1));
            CurrentLocation = PlayerOne.GetLocation();
            Console.WriteLine($"You are now at: {CurrentLocation.Name}.");
        }
        else
        {
            Console.WriteLine("You won the fight!");
            int index = World.RandomGenerator.Next(currentMonster.Loot.Count);
            Item drop = currentMonster.Loot[index];
            PlayerOne.AddItemToInventory(drop);
            Console.WriteLine($"{currentMonster.Name} dropped {drop.Name}");
            Console.WriteLine($"Current Healt {PlayerOne.CurrentHitPoints}/{PlayerOne.MaximumHitPoints}");
        }
    }

    public void MoveToLocation()
    {
        Console.WriteLine("Where would you like to go?");
        Console.WriteLine($"You are at: {CurrentLocation.Name}. From here you can go:");
        Console.WriteLine(CurrentLocation.Compass());

        Console.Write("\nEnter a direction (Arrow keys/NSWE): ");
        ConsoleKeyInfo direction = Console.ReadKey();
        Console.WriteLine();
        switch (direction.Key)
        {
            // West
            case ConsoleKey.LeftArrow:
            case ConsoleKey.W:
                if (CurrentLocation.LocationToEast != null)
                    PlayerOne.SetLocation(CurrentLocation.LocationToEast);
                else
                    Console.WriteLine("There is nothing towards the West.");
                break;

            // North
            case ConsoleKey.UpArrow:
            case ConsoleKey.N:
                if (CurrentLocation.LocationToNorth != null)
                    PlayerOne.SetLocation(CurrentLocation.LocationToNorth);
                else
                    Console.WriteLine("There is nothing towards the North.");
                break;

            // East
            case ConsoleKey.RightArrow:
            case ConsoleKey.E:
                if (CurrentLocation.LocationToEast != null && (CurrentLocation.LocationToEast.ID != 3 || PassCheck))
                    PlayerOne.SetLocation(CurrentLocation.LocationToEast);
                else if (CurrentLocation.LocationToEast != null && CurrentLocation.LocationToEast.ID == 3)
                    guardPostCheck();
                else
                    Console.WriteLine("There is nothing towards the East.");
                break;

            // South
            case ConsoleKey.DownArrow:
            case ConsoleKey.S:
                if (CurrentLocation.LocationToSouth != null)
                    PlayerOne.SetLocation(CurrentLocation.LocationToSouth);
                else
                    Console.WriteLine("There is nothing towards the South.");
                break;

            default:
                Console.WriteLine($"({direction.Key}) is not a valid move input.");
                break;
        }

        if (CurrentLocation != PlayerOne.GetLocation())
        {
            CurrentLocation = PlayerOne.GetLocation();
            LocationDescription();
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
        Console.WriteLine($"You are at: {CurrentLocation.Name}.");
        Console.WriteLine($"Description: {CurrentLocation.Description}");
    }

    public void TriggerEvent()
    {
        // Have an option to talk with some one on the location to get quests or to get information
        switch (CurrentLocation.ID)
        {
            // Home
            case 1:
                homeEvent();
                break;
            case 2:
                townSquareEvent();
                break;
            case 3:
                guardPostEvent();
                break;
            case 4:
                alchemistHutEvent();
                break;
            case 6:
                farmHouseEvent();
                break;
            case 8:
                bridgeEvent();
                break;
            case 5:
            case 7:
            case 9:
                monsterEvent();
                break;
            default:
                break;
        }
    }

    private void homeEvent()
    {
        Console.WriteLine("You decide to rest at your house.");
        Console.WriteLine("You feel rejuvenated.");
        PlayerOne.CurrentHitPoints = PlayerOne.MaximumHitPoints;
    }

    private void townSquareEvent()
    {
        // IF the player has the Adventure pass maybe add some optional side quests.
    }

    private void guardPostEvent()
    {
        // maybe some dialogue between the player and the guard
    }

    private void guardPostCheck()
    {
        Item pass = World.ItemByID(7);
        if (PlayerOne.SearchByItem(pass))
        {
            PassCheck = true;
            PlayerOne.SetLocation(CurrentLocation.LocationToEast);
            Console.WriteLine("Guard: You may pass. hero.");
        }
        else
        {
            Console.WriteLine("Guard: Turn back at once, peasant! Unless thee hast proof of thy grit.");
        }
    }

    private void alchemistHutEvent()
    {
        Quest quest = World.QuestByID(1);
        if (!PlayerOne.SearchByQuest(quest))
            PlayerOne.ObtainQuest(quest);
    }

    private void bridgeEvent()
    {
        Quest quest = World.QuestByID(3);
        if (!PlayerOne.SearchByQuest(quest))
            PlayerOne.ObtainQuest(quest);
    }

    private void farmHouseEvent()
    {
        Quest quest = World.QuestByID(2);
        if (!PlayerOne.SearchByQuest(quest))
            PlayerOne.ObtainQuest(quest);
    }

    private void monsterEvent()
    {
        Console.WriteLine("You look around.....");
        System.Threading.Thread.Sleep(1000);
        if (World.RandomGenerator.NextDouble() < 0.2)
            BattleSequence();
        else
            Console.WriteLine("But nothing happened.");
    }
}