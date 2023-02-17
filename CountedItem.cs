public class CountedItem {
    public Item TheItem;
    public int Quantity;

    public CountedItem(Item TheItem, int Quantity) {
        TheItem = TheItem;
        Quantity = Quantity;
    }

    public void Increment(int n = 1) {
        Quantity += n;
    }

    public void Decrement(int n = 1) {
        Quantity -= n;
        if (Quantity < 0) {
            Quantity = 0;
        }
    }

    public string Description() => $"[{Quantity}] {TheItem.Name}";
}
