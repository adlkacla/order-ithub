using System.Collections.Generic;
namespace OrderHub.Services;
public class CatalogService
{
    private readonly List<Product> products = new();
    public void AddProduct(string name, decimal price, string
    category)
    {
        // ЗАДАЧА: добавьте товар в список products
        products.Add(new Product(name, price, category));
    }
    public void PrintSortedByNaturalOrder()
    {
        // ЗАДАЧА 1: вызовите Sort() без аргументов — сработает IComparable < Product >
        // ЗАДАЧА 2: выведите каждый товар через foreach
        products.Sort();
        foreach(var product in products)
            Console.WriteLine($"{product.Name} ({product.Category}) - {product.Price}р");
    }
    public void PrintSortedByName()
    {
        // ЗАДАЧА: вызовите Sort(new NameComparer())
        // ЗАДАЧА: выведите результат
        products.Sort(new NameComparer());
        foreach (var product in products)
            Console.WriteLine($"{product.Name} ({product.Category}) - {product.Price}р");
    }
    public void PrintSortedByNameLength()
    {
        // ЗАДАЧА: отсортируйте лямбдой по длине названия —
        // Sort((a, b) => a.Name.Length.CompareTo(b.Name.Length));
        // это делегат Comparison<Product>
        // ЗАДАЧА: выведите результат и поясните в консоли, что тип Product не менялся
        products.Sort((a,b)=> a.Name.Length.CompareTo(b.Name.Length));
        foreach (var product in products)
            Console.WriteLine($"{product.Name} ({product.Category}) - {product.Price}р");
    }
    // ЗАДАЧА: добавьте метод PrintAll(), который печатает каталог в исходном порядке
    public void PrintAll()
    {
        foreach (var product in products)
            Console.WriteLine($"{product.Name} ({product.Category}) - {product.Price}р");
    }
}