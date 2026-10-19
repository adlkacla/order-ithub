public class WarehouseService
{
    Dictionary<string, int> stock = new();

    public void Receive(string productName, int quantity)
    {
        if (stock.ContainsKey(productName))
            stock[productName] += quantity;
        else
            stock.Add(productName, quantity);

        Console.WriteLine($"Товара {productName} на складе {stock[productName]}");
    }

    public bool Sell(string productName, int quantity)
    {
        if (!stock.TryGetValue(productName, out int availableQuantity)||availableQuantity<quantity)
        {
            Console.WriteLine($"Нет товара {productName} в количестве {quantity} на складе");
            return false;
        }
        stock[productName] -= quantity;
        if (quantity == availableQuantity)
            stock.Remove(productName);

        return true;
    }

    public bool TryGetStock(string productName, out int quantity)
    {
        if (stock.TryGetValue(productName, out int available))
        {
            quantity = available;
            return true;
        }
        else
        {
            quantity = 0;
            return false;
        }
    }

    public void PrintStock()
    {
        var ordered = stock.OrderBy(a=> a.Key);
        foreach (KeyValuePair<string,int> val in ordered)
        {
            Console.WriteLine($"{val.Key}: {val.Value}шт");
        }
    }
}