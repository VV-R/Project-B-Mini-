class CountedItem {
    Item TheItem;
    int Quantity;

    public CountedItem(Item TheItem, int Quantity) {
        this.TheItem = TheItem;
        this.Quantity = Quantity;
    }

    public void Increment(int n = 1) {
        this.Quantity += n;
    }

    public void Decrement(int n = 1) {
        this.Quantity -= n;
        if (this.Quantity < 0) {
            this.Quantity = 0;
        }
    }

    public string Description() => $"[{Quantity}] {TheItem.Name}";
}
