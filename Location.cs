public class Location
{
    public int ID;
    public string Name;
    public string Description;

    public Item ItemRequiredToEnter;
    public Quest QuestAvailableHere;
    public Location LocationToNorth;
    public Location LocationToEast;
    public Location LocationToSouth;
    public Location LocationToWest;
    public Monster MonsterLivingHere;
    
    public Location(int id, string name, string description, Item item, Quest quest, Monster monster)
    {
        this.ID = id;
        this.Name = name;
        this.Description = description;
        this.ItemRequiredToEnter = item;
        this.QuestAvailableHere = quest;
        this.MonsterLivingHere = monster;
    }

      public string Compass()
    {
        string s = "From here you can go:\n";
        if (LocationToNorth != null)
        {
            s += "    N\n    |\n";
        }
        if (LocationToWest != null)
        {
            s += "W---|";
        }
        else
        {
            s += "    |";
        }
        if (LocationToEast != null)
        {
            s += "---E";
        }
        s += "\n";
        if (LocationToSouth != null)
        {
            s += "    |\n    S\n";
        }
        return s;
    }
}