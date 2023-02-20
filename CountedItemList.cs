public class CountedItemList {
    private List<CountedItem> TheCountedItemList;

    public CountedItemList(List<CountedItem> countedItemList) {
        this.TheCountedItemList = countedItemList;
    }

    private int SearchIndex(Item item) {
        return this.TheCountedItemList.FindIndex(
            countedItem => countedItem.TheItem.ID == item.ID
        );
    }

    public void AddItem(Item item) {
        int index = SearchIndex(item);
        if (index == -1) {
            this.TheCountedItemList.Add(new CountedItem(item, 1));
        }
        else {
            this.TheCountedItemList[index].Increment();
        }
    }

    public void RemoveItem(Item item) {
        int index = SearchIndex(item);
        if (index != -1) {
            CountedItem countedItem = this.TheCountedItemList[index];
            if (countedItem.Quantity == 1) {
                TheCountedItemList.Remove(countedItem);
            }
            else {
                countedItem.Decrement();
            }
        }
    }

    public CountedItem SearchByItem(Item item) {
        return this.TheCountedItemList.Find(countedItem => countedItem.TheItem.ID == item.ID);
    }

    public string Description() {
        List<string> descriptions = new(this.TheCountedItemList.Count);
        foreach(CountedItem countedItem in this.TheCountedItemList) {
            descriptions.Add(countedItem.Description());
        }
        return String.Join("\n", descriptions);
    }
}
