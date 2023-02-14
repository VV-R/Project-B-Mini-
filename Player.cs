public class Player
{
    public string Name;
    public int CurrentHitPoints = 10;
    public int MaximumHitPoints = 10;
    // Currently not needed
    // public int Gold;
    // public int ExperiencePoints;
    // public int Level;
    Weapon CurrentWeapon;
    Location CurrentLocation;
    QuestList QuestLog;
    CountedItemList Inventory;

    public Player(string name)
    {
        Name = name;
    }

    public void SetWeapon(Weapon weapon) => CurrentWeapon = weapon;
    public Weapon GetWeapon() => CurrentWeapon;
    public void SetLocation(Location location) => CurrentLocation = location;
    public Location GetLocation() => CurrentLocation;
    public void TakeDamage(int damage) => CurrentHitPoints -= damage;
    public int DealDamage() => World.RandomGenerator.Next(CurrentWeapon.MinimumDamage, CurrentWeapon.MaximumDamage);
}