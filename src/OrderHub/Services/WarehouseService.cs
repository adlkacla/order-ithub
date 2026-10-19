public class WarehouseService
{
    // ЗАДАЧА: объявите Dictionary<string, int> stock — название товара → количество
    // подсказка: поле инициализируется прямо при объявлении: = new Dictionary<string, int>();


    Dictionary<string, int> stock = new();

    public void Receive(string productName, int quantity)
    {
        // ЗАДАЧА 1: проверьте наличие товара через TryGetValue
        // ЗАДАЧА 2: если товар есть — увеличьте количество (stock[productName] = existing + quantity)
        // ЗАДАЧА 3: если товара нет — добавьте новую запись через индексатор
        // ЗАДАЧА 4: выведите итоговое количество товара на складе

        if (stock.ContainsKey(productName))
            stock[productName] += quantity;
        else
            stock.Add(productName, quantity);

        Console.WriteLine($"Товара {productName} на складе {stock[productName]}");
    }

    public bool Sell(string productName, int quantity)
    {
        // ЗАДАЧА 1: если товара нет вообще — сообщите об этом и верните false
        // (никаких исключений: используйте TryGetValue)
        // ЗАДАЧА 2: если товара недостаточно — выведите предупреждение и верните false
        // ЗАДАЧА 3: уменьшите остаток на quantity
        // ЗАДАЧА 4: если остаток стал равен нулю — удалите запись через Remove
        // и сообщите, что товар снят с учёта
        // ЗАДАЧА 5: верните true


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
        // ЗАДАЧА: реализуйте безопасный поиск остатка через TryGetValue
        // ЗАДАЧА: при отсутствии товара присвойте quantity = 0 — значение по умолчанию,
        // чтобы у вызывающего кода не было неинициализированной переменной


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
        // ЗАДАЧА: переберите склад через foreach по KeyValuePair<string, int>
        // ЗАДАЧА: отсортируйте вывод по алфавиту (OrderBy можно использовать уже сейчас)

        var ordered = stock.OrderBy(a=> a.Key);
        foreach (KeyValuePair<string,int> val in ordered)
        {
            Console.WriteLine($"{val.Key}: {val.Value}шт");
        }
    }
}