public class Item {
    public int ID;
    public string Name;
    public string NamePlural;

    public Item(int id, string name, string namePlural) {
        ID = id;
        Name = name;
        NamePlural = namePlural;
    }
}

public class ConsumableItem : Item {
    public int HealEffect;

    public ConsumableItem(int ID, string Name, string NamePlural, int healEffect) : base(ID, Name, NamePlural) {
        HealEffect = healEffect;
    }

    public void Consume(Player player) {
        player.HealByAmount(HealEffect);
        player.Inventory.RemoveItem(this);
    }
}

