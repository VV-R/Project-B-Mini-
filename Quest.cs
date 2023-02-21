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
    public string DisplayQuests()
    {
        string empty_string = "";
        foreach (var quest in QuestLog)
        {
            empty_string += $"{quest.TheQuest.Name} - {quest.TheQuest.Description} - {(quest.IsCompleted ? "Completed" : "In progress")}\n";
        }
        return empty_string;
    }
    public void AddQuest(Quest quest) => QuestLog.Add(new PlayerQuest(quest));
    public bool SearchByQuest(Quest quest)
    {
        foreach (PlayerQuest playerQuest in QuestLog)
        {
            if (playerQuest.TheQuest.ID == quest.ID)
                return true;
        }
        return false;
    }
}