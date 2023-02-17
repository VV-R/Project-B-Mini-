public class Monster
{
    public int ID;
    public string Name;
    public string NamePlural;
    public int MaximumDamage;
    public int MinimumDamage;
    // Currently not needed
    public int RewardExperience;
    // Currently not needed
    public int RewardGold;
    // Part of items
    public List<Item> Loot = new List<Item>();
    public int CounteditemList;
    public int MaximumHitPoints;
    public int CurrentHitPoints;

    public Monster(int id, string name, string namePlural, int maxDamage, int minDamage, int rewardExperience, int rewardGold, int maximumHitPoints)
    {
        ID = id;
        Name = name;
        NamePlural = namePlural;
        MaximumDamage = maxDamage;
        MinimumDamage = minDamage;
        RewardExperience = rewardExperience;
        RewardGold = rewardGold;
        MaximumHitPoints = maximumHitPoints;
        CurrentHitPoints = maximumHitPoints;
    }

    public void AddItem(Item item) => Loot.Add(item);
    public void TakeDamage(int damage) => CurrentHitPoints -= damage;
    public int DealDamage() => World.RandomGenerator.Next(MinimumDamage, MaximumDamage);
}