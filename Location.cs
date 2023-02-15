public class Location
{
    public int ID;
    public string Name;
    public string Description;

    Item ItemRequiredToEnter;
    Quest QuestAvailableHere;
    Location LocationToNorth;
    Location LocationToEast;
    Location LocationToSouth;
    Location LocationToWest;
    public Location(int id, string name, string description, Item item, Quest quest)
    {
        this.ID = id;
        this.Name = name;
        this.Description = description;
        this.ItemRequiredToEnter = item;
        this.QuestAvailableHere = quest;
    }

    public static void GetLocation()
    {
        Player player = new Player(World.Locations[0]);
        Console.WriteLine("Current location: " + player.CurrentLocation.Name);
    }
}