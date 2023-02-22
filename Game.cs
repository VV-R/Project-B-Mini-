public class Game
{
    public Player PlayerOne;
    public bool Running = true;
    public Location CurrentLocation;
    public bool PassCheck = false;
    // public double ChanceOfDouble = 0.2;

    public Game(Player player)
    {
        CurrentLocation = player.GetLocation();
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
        while (currentMonster.CurrentHitPoints > 0 && PlayerOne.CurrentHitPoints > 0)
        {
            Console.WriteLine($"{currentMonster.Name} HP: {currentMonster.CurrentHitPoints}/{currentMonster.MaximumHitPoints}");
            Console.WriteLine($"{PlayerOne.Name} HP: {PlayerOne.CurrentHitPoints}/{PlayerOne.MaximumHitPoints}");

            Console.WriteLine("1: Fight\n2: Use item\n3: Flee");
            int.TryParse(Console.ReadLine(), out int choice);

            if (choice == 1)
            {
                int playerDamage = PlayerOne.DealDamage();
                Console.WriteLine($"You hit {currentMonster.Name} for {playerDamage} points of damage!!");
                currentMonster.TakeDamage(playerDamage);
            }
            else if (choice == 2)
                UseItem();
            else if (choice == 3)
            {
                if (World.RandomGenerator.NextDouble() < 0.2)
                    break;
            }
            int monsterDamage = currentMonster.DealDamage();
            PlayerOne.TakeDamage(monsterDamage);
            Console.WriteLine($"{currentMonster.Name} did {monsterDamage} points of damage!");
            Console.ReadKey();
            Console.Clear();
        }
        if (PlayerOne.CurrentHitPoints <= 0)
        {
            Console.WriteLine("You have lost....");
            Console.WriteLine("You go back home and rest up.");
            PlayerOne.SetLocation(World.LocationByID(1));
            PlayerOne.CurrentHitPoints = PlayerOne.MaximumHitPoints;
            CurrentLocation = PlayerOne.GetLocation();
            Console.WriteLine($"You are now at: {CurrentLocation.Name}.");
        }
        else if (currentMonster.CurrentHitPoints <= 0)
        {
            Console.WriteLine("You won the fight!");
            int index = World.RandomGenerator.Next(currentMonster.Loot.Count);
            Item drop = currentMonster.Loot[index];
            PlayerOne.AddItemToInventory(drop);
            PlayerOne.Gold += currentMonster.RewardGold;
            PlayerOne.ExperiencePoints += currentMonster.RewardExperience;
            Console.WriteLine($"{currentMonster.Name} dropped {drop.Name} and {currentMonster.RewardGold} Gold");
            Console.WriteLine($"Obtained {currentMonster.RewardExperience} EXP");
            Console.WriteLine($"Current Healt {PlayerOne.CurrentHitPoints}/{PlayerOne.MaximumHitPoints}");
        }
        else
        {
            Console.WriteLine("You fled from the fight");
        }
        currentMonster.CurrentHitPoints = currentMonster.MaximumHitPoints;
        if (PlayerOne.LevelUp())
            OnLevelUp();
    }

    public void MoveToLocation()
    {
        Console.WriteLine("Where would you like to go?");
        Console.WriteLine($"You are at: {CurrentLocation.Name}. From here you can go:");
        Console.WriteLine(CurrentLocation.Compass());

        Console.Write("\nEnter a direction (Arrow keys/NESW): ");
        ConsoleKeyInfo direction = Console.ReadKey();
        Console.WriteLine();
        switch (direction.Key)
        {
            // West
            case ConsoleKey.LeftArrow:
            case ConsoleKey.W:
                if (CurrentLocation.LocationToWest != null)
                    PlayerOne.SetLocation(CurrentLocation.LocationToWest);
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
        Weapon currentWeapon = PlayerOne.GetWeapon();
        Console.WriteLine($"Name: {PlayerOne.Name}");
        Console.WriteLine($"Level: {PlayerOne.Level}");
        Console.WriteLine($"ATK: {PlayerOne.BaseAttack}");
        Console.WriteLine($"HP: {PlayerOne.CurrentHitPoints}/{PlayerOne.MaximumHitPoints}");
        Console.Write($"EXP: {PlayerOne.ExperiencePoints} - ");
        Console.WriteLine($"EXP needed for Level Up: {(60 + (20 * (PlayerOne.Level - 1))) - PlayerOne.ExperiencePoints}");
        Console.WriteLine($"Current weapon: {currentWeapon.Name}");
        Console.WriteLine($"Gold: {PlayerOne.Gold}");
        Console.WriteLine($"\nInventory:\n{PlayerOne.DisplayInventory()}");
        Console.WriteLine($"\nQuests:\n{PlayerOne.DisplayQuests()}");
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
        if (PlayerOne.LevelUp())
            OnLevelUp();
    }

    public void UseItem()
    {
        Console.WriteLine("Enter an integer or write exit to exit.");
        Console.WriteLine("Inventory:");
        Console.WriteLine("Index - Amount - Item");
        Console.WriteLine(PlayerOne.InventoryWithIndex());
        bool used = false;
        while (!used)
        {
            string index = Console.ReadLine();
            if (int.TryParse(index, out int result))
                used = PlayerOne.UseItem(result);
            else if (index.ToLower().Contains("exit"))
                break;
            else
                Console.WriteLine("Please enter a interger or write exit to exit.");
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
        Console.WriteLine("There seems to be a shop...");
        Console.WriteLine("Let's check it out!");
        Shop shop = World.ShopByID(World.SHOP_ID_TOWN_SQUARE);
        shop.browse(PlayerOne);
    }

    private void guardPostEvent()
    {
        // maybe some dialogue between the player and the guard
    }

    private void guardPostCheck()
    {
        Item pass = World.ItemByID(7);
        if (PlayerOne.SearchByItem(pass) != null)
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
        Quest quest = CurrentLocation.QuestAvailableHere;
        if (!PlayerOne.SearchByQuest(quest))
        {
            PlayerOne.ObtainQuest(quest);
            // Tekst voor wanneer de player de quest krijgt
            Console.WriteLine("Alchemist: Those rats art nibbling on mine own h'rbs! I couldst very much useth an adventur'r to taketh careth of those folk …");
        }
        else if (PlayerOne.CheckCompleted(quest))
        {
            if (quest.RewardItem != null)
                PlayerOne.AddItemToInventory(quest.RewardItem);

            if (quest.RewardWeapon != null)
                PlayerOne.SetWeapon(quest.RewardWeapon);
            PlayerOne.Gold += quest.RewardGold;
            PlayerOne.ExperiencePoints += quest.RewardExperiencePoints;
            // Tekst voor wanneer hij gecomplete is.
            Console.WriteLine("Alchemist: Thank thee so much! Here you go, I have a gift for thee");
            Console.WriteLine("Obtained a Club!");
        }
        else
        {
            // Tekst voor wanneer de player nog niet klaar is met de quest
            Console.WriteLine("Alchemist: What are you doing here? Those rats are still nibbling on mine h'rbs!");
        }
    }

    private void bridgeEvent()
    {
        Quest quest = CurrentLocation.QuestAvailableHere;
        if (!PlayerOne.SearchByQuest(quest))
        {
            PlayerOne.ObtainQuest(quest);
            // Tekst voor wanneer de player de quest krijgt
            Console.WriteLine("King: Please adventurer, please releaseth the town folks of their feareth by killing the spiders!");
        }
        else if (PlayerOne.CheckCompleted(quest))
        {

            PlayerOne.AddItemToInventory(quest.RewardItem);
            PlayerOne.Gold += quest.RewardGold;
            PlayerOne.ExperiencePoints += quest.RewardExperiencePoints;
            // Tekst voor wanneer hij gecomplete is.
            Console.WriteLine("King: Thank thee so much! Here you go, I have a gift for thee\n\n\n");
            Console.WriteLine("Obtained the winners medal!");
            Console.WriteLine("><--- Thank you for playing ---><");
            System.Environment.Exit(0);
        }
        else
        {
            Console.WriteLine("King: What are you doing here? Those spiders are still crawling around!");
        }
    }

    private void farmHouseEvent()
    {
        Quest quest = CurrentLocation.QuestAvailableHere;
        if (!PlayerOne.SearchByQuest(quest))
        {
            PlayerOne.ObtainQuest(quest);
            // Tekst voor wanneer de player de quest krijgt
            Console.WriteLine("Farmer: I can't w'rk mine own landeth with those pesky snakes slith'ring 'round! Shall thee holp me?");
        }
        else if (PlayerOne.CheckCompleted(quest))
        {

            if (quest.RewardItem != null)
                PlayerOne.AddItemToInventory(quest.RewardItem);
            if (quest.RewardWeapon != null)
                PlayerOne.SetWeapon(quest.RewardWeapon);
            PlayerOne.Gold += quest.RewardGold;
            PlayerOne.ExperiencePoints += quest.RewardExperiencePoints;
            // Tekst voor wanneer hij gecomplete is.
            Console.WriteLine("Farmer: Thank thee so much! Here you go, I have a gift for thee");
            Console.WriteLine("Obtained the Adventure pass, maybe you can cross the bridge now?");
        }
        else
        {
            // Tekst voor wanneer de player nog niet klaar is met de quest
            Console.WriteLine("Farmer: What are you doing here? Those pesky snakes are still slith'ring 'round!");
        }
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

    private void OnLevelUp()
    {
        Console.WriteLine("Your level has been increased!");
        Console.WriteLine($"Your HP is now {PlayerOne.MaximumHitPoints}");
        Console.WriteLine($"Your ATK is now {PlayerOne.BaseAttack}");
    }
}
