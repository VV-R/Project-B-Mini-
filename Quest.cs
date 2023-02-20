public class Quest
{
    public int ID;
    public string Name;
    public string Description;
    public int RewardExperiencePoints;
    public int RewardGold;
    public Item RewardItem;

    public Weapon RewardWeapon;
    public CountedItem QuestCompletionItem;
    public Quest(int id, string name, string description, int exp, int gold, Item item, Weapon weapon)
    {
        ID = id;
        Name = name;
        Description = description;
        RewardExperiencePoints = exp;
        RewardGold = gold;
        RewardItem = item;
        RewardWeapon = weapon;
    }
}

public class PlayerQuest
{
    public Quest TheQuest;
    public bool IsCompleted;
    public PlayerQuest(Quest quest)
    {
        TheQuest = quest;
        IsCompleted = false;
    }
}

public class QuestList
{
    public List<PlayerQuest> QuestLog = new();
}