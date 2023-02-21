public class Player
{
    public string Name;
    public int CurrentHitPoints = 10;
    public int MaximumHitPoints = 10;
    public int BaseAttack = 1;
    public int Gold = 2;
    public int ExperiencePoints = 0;
    public int Level = 1;
    public CountedItemList Inventory;
    Weapon currentWeapon;
    Location currentLocation;
    QuestList questLog;


    public Player(string name, Weapon weapon, Location location)
    {
        Name = name;
        Inventory = new CountedItemList(new List<CountedItem>());
        questLog = new QuestList();
        currentWeapon = weapon;
        currentLocation = location;
    }

    public void SetWeapon(Weapon weapon) => currentWeapon = weapon;
    public Weapon GetWeapon() => currentWeapon;
    public void SetLocation(Location location) => currentLocation = location;
    public Location GetLocation() => currentLocation;
    public void TakeDamage(int damage) => CurrentHitPoints -= damage;
    public int DealDamage() => BaseAttack + World.RandomGenerator.Next(currentWeapon.MinimumDamage, currentWeapon.MaximumDamage);
    public void AddItemToInventory(Item item) => Inventory.AddItem(item);
    public CountedItem SearchByItem(Item item) => Inventory.SearchByItem(item);
    public void ObtainQuest(Quest quest) => questLog.AddQuest(quest);
    public bool SearchByQuest(Quest quest) => questLog.SearchByQuest(quest);
    public string DisplayQuests() => questLog.DisplayQuests();
    public string DisplayInventory() => Inventory.Description();
    public void HealByAmount(int amount)
    {
        if (CurrentHitPoints + amount > MaximumHitPoints)
            CurrentHitPoints = MaximumHitPoints;
        else
            CurrentHitPoints += amount;
    }

    public bool CheckCompleted(Quest quest)
    {
        CountedItem playerItem = Inventory.SearchByItem(quest.QuestCompletionItem.TheItem);
        if (playerItem != null && playerItem.Quantity >= quest.QuestCompletionItem.Quantity)
        {
            // Het liefst een functie in de QuestLog maken voor dit
            for (int i = 0; i < quest.QuestCompletionItem.Quantity; i++)
                playerItem.Decrement();
            foreach (PlayerQuest playerQuest in questLog.QuestLog)
            {
                if (playerQuest.TheQuest.ID == quest.ID)
                {
                    playerQuest.IsCompleted = true;
                    return true;
                }
            }
        }
        return false;
    }

    public bool LevelUp()
    {
        if (ExperiencePoints >= 60 + (20 * (Level - 1)))
        {
            Level++;
            ExperiencePoints = 0;
            BaseAttack += 1;
            MaximumHitPoints += 5;
            CurrentHitPoints = MaximumHitPoints;
            return true;
        }
        return false;
    }
}