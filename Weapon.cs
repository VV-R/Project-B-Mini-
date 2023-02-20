public class Weapon : Item {
    public int MinimumDamage;
    public int MaximumDamage;

    public Weapon(
        int id, string name, string namePlural,
        int minimumDamage, int maximumDamage
    ) : base(id, name, namePlural) {
        MinimumDamage = minimumDamage;
        MaximumDamage = maximumDamage;
    }

}
