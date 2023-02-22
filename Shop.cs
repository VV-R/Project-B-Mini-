public class Shop
{
    public int ID;
    public string Name;
    public List<(CountedItem item, int price)> Inventory;

    public Shop(
        int id, string name, List<(CountedItem item, int price)> inventory
    )
    {
        ID = id;
        Name = name;
        Inventory = inventory;
    }

    private void Purchase(int index, CountedItem countedItem, Player player, int price)
    {
        Console.WriteLine($"How many {countedItem.TheItem.NamePlural} would you like to buy?");
        int quantity;
        while (true)
        {
            if (!int.TryParse(Console.ReadLine(), out quantity))
            {
                Console.WriteLine("Invalid answer.");
            }
            if (quantity > countedItem.Quantity)
            {
                Console.WriteLine($"Not enough stock... The maximum you can buy is {countedItem.Quantity}");
                return;
            }
            if (quantity * price > player.Gold)
            {
                Console.WriteLine("You can't afford that...");
                return;
            }
            else break;
        }
        Console.WriteLine($"Are you sure you want to buy {quantity} {countedItem.TheItem.NamePlural} for {quantity * price}? (y/n)");
        while (true)
        {
            switch (Console.ReadLine().ToLower())
            {
                case "y":
                    this.Transaction(countedItem, index, player, price, quantity);
                    return;
                case "n":
                    Console.WriteLine("Ok...");
                    return;
            }
        }
    }

    private void Transaction(CountedItem countedItem, int index, Player player, int price, int quantity)
    {
        int cost = quantity * price;
        player.Gold -= cost;
        for (int i = 0; i < quantity; i++)
        {
            player.Inventory.AddItem(countedItem.TheItem);
        }
        countedItem.Quantity -= quantity;
        if (countedItem.Quantity == 0) Inventory.RemoveAt(index);
        Console.WriteLine($"You bought {quantity} {countedItem.TheItem.NamePlural} for {cost} gold.");
    }

    public void browse(Player player)
    {
        CountedItem countedItem;
        Console.WriteLine("Index)[Amount] Item > Price");
        while (true)
        {
            for (int i = 0; i < Inventory.Count; i++)
            {
                (countedItem, int price) = Inventory[i];
                Console.WriteLine($"{i})[{countedItem.Quantity}] {countedItem.TheItem.Name} > {price}");
            }

            Console.WriteLine($"\n{player.Name} gold: {player.Gold}");

            Console.WriteLine("Please enter which item you'd like to buy or e to exit:");

            string answer = Console.ReadLine();
            int index;
            if (int.TryParse(answer, out index))
            {
                int price;
                try
                {
                    (countedItem, price) = Inventory[index];
                }
                catch
                {
                    Console.WriteLine($"There is no item that corresponds with number: {index}");
                    continue;
                }
                this.Purchase(index, countedItem, player, price);
            }
            else if (answer.ToLower() == "e")
            {
                Console.WriteLine("Goodbye...");
                return;
            }
            else
            {
                Console.WriteLine("Invalid answer.");
            }
        }
    }
}
