public class Player
{
    public string Name;
    public int CurrentHitPoints = 10;
    public int MaximumHitPoints = 10;
    // Currently not needed
    // public int Gold;
    // public int ExperiencePoints;
    // public int Level;
    public CountedItemList Inventory;
    Weapon currentWeapon;
    Location currentLocation;
    QuestList questLog;


    public Player(string name)
    {
        Name = name;
    }

    public void SetWeapon(Weapon weapon) => currentWeapon = weapon;
    public Weapon GetWeapon() => currentWeapon;
    public void SetLocation(Location location) => currentLocation = location;
    public Location GetLocation() => currentLocation;
    public void TakeDamage(int damage) => CurrentHitPoints -= damage;
    public int DealDamage() => World.RandomGenerator.Next(currentWeapon.MinimumDamage, currentWeapon.MaximumDamage);
}