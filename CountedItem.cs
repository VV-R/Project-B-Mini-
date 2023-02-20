public class CountedItem {
    public Item TheItem;
    public int Quantity;

    public CountedItem(Item theItem, int quantity) {
        TheItem = theItem;
        Quantity = quantity;
    }

    public void Increment(int n = 1) => Quantity += 1;

    public void Decrement(int n = 1) {
        Quantity -= n;
        if (Quantity < 0) {
            Quantity = 0;
        }
    }

    public string Description() => $"[{Quantity}] {TheItem.Name}";
}
