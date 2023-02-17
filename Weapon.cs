class Weapon : Item {
    public int MinimumDamage;
    public int MaximumDamage;

    public Weapon(
        int ID, string Name, string NamePlural,
        int MinimumDamage, int MaximumDamage
    ) {
        this.ID = ID;
        this.Name = Name;
        this.NamePlural = NamePlural;
        this.MinimumDamage = MinimumDamage;
        this.MaximumDamage = MaximumDamage;
    }

}
