using System.Collections.Generic;
using System.ComponentModel;
namespace OrderHub.Services;

public class CategorySummary
{
    public string Category { get; set; } = "";
    public int Count { get; set; }
    public decimal AveragePrice { get; set; }
}

public class ReportService
{
    // ЗАДАЧА: метод принимает IEnumerable<Product> products и decimal threshold
    public List<string> ExpensiveProducts(IEnumerable<Product> products, decimal threshold)
    {
        // ЗАДАЧА 1: отфильтруйте товары дешевле порога через Where
        // ЗАДАЧА 2: отсортируйте по цене через OrderBy
        // ЗАДАЧА 3: спроецируйте каждый товар в строку через Select
        // подсказка: строка должна содержать название и цену,
        // например $"{p.Name} — {p.Price:0.00} ₽"
        // ЗАДАЧА 4: материализуйте результат вызовом ToList() и верните его
        // переписывать это обычным foreach с if внутри нельзя — цель этапа именно LINQ
        return products.Where(p => p.Price > threshold)
            .OrderBy(p => p.Price)
            .Select(p => $"{p.Name} - {p.Price:0.00}p")
            .ToList();
    }
    public int CountExpensive(IEnumerable<Product> products, decimal threshold)
    {
        // ЗАДАЧА: верните количество товаров дороже порога одним вызовом Count
        // подсказка: products.Count(p => p.Price > threshold)
        return products.Count(p=>p.Price>threshold);
    }
    public bool HasVeryExpensive(IEnumerable<Product> products, decimal threshold)
    {
        // ЗАДАЧА: верните результат Any — есть ли хоть один товар дороже порога
        return products.Any(p => p.Price > threshold);
    }
    public bool AllInStock(IEnumerable<Product> products)
    {
        // ЗАДАЧА: через All верните true, только если у всех товаров цена больше нуля
        return products.All(p => p.Price > 0);
    }
    public Product? Cheapest(IEnumerable<Product> products)
    {
        // ЗАДАЧА: найдите самый дешёвый товар
        // подсказка: сортировать не нужно — есть OrderBy(p => p.Price).First(),
        // либо FirstOrDefault, чтобы не бросать исключение на пустой коллекции
        return products.OrderBy(p=>p.Price).FirstOrDefault();
    }

    // ЗАДАЧА: метод принимает IEnumerable<Product> и возвращает IEnumerable<CategorySummary> 
    public List<CategorySummary> SummarizeByCategory(IEnumerable<Product> products)
    {
        // ЗАДАЧА 1: сгруппируйте товары по полю Category через GroupBy
        // ЗАДАЧА 2: для каждой группы посчитайте количество товаров (g.Count())
        // ЗАДАЧА 3: посчитайте среднюю цену (g.Average(p => p.Price))
        // ЗАДАЧА 4: спроецируйте результат в CategorySummary с полями
        // Category = g.Key, Count = …, AveragePrice = …
        // ЗАДАЧА 5: отсортируйте сводку по убыванию количества товаров через OrderByDescending
        // ЗАДАЧА 6: материализуйте через ToList()
        // ВАЖНО: агрегаты обязаны считаться LINQ-методами прямо на группе;
        // ручной цикл со счётчиком не засчитывается
        return products.GroupBy(p => p.Category)
            .Select(g => new CategorySummary
            {
                Category = g.Key,
                Count = g.Count(),
                AveragePrice = g.Average(p => p.Price)
            })
            .OrderByDescending(s => s.Count)
            .ToList();
    }
    // ЗАДАЧА: найти самую дорогую категорию (средняя цена максимальна)
    // подсказка: OrderByDescending(s => s.AveragePrice).First()
    public CategorySummary MostExpensiveCategory(IEnumerable<Product> products)
    {
        return SummarizeByCategory(products).OrderByDescending(s => s.AveragePrice).First();
    }
    // ЗАДАЧА: найти категории, где товаров не меньше заданного числа,
    // и спроецировать только названия таких категорий через Select
    public List<string> CategoriesWithAtLeast(IEnumerable<Product> products, int minCount)
    {
        return SummarizeByCategory(products)
            .Where(s => s.Count >= minCount)
            .Select(s => s.Category)
            .ToList();
    }

    public List<string> TopCustomers(IEnumerable<Customer> customers, IEnumerable<Order> orders, int topCount)
    {
        // ЗАДАЧА 1: соедините коллекции через Join по ключу Customer.Id / Order.CustomerName,
        // если идентификаторы строковые; при строковом ключе соединяйте по Name
        // ЗАДАЧА 2: сгруппируйте результат по имени клиента через GroupBy
        // ЗАДАЧА 3: посчитайте сумму заказов клиента (Sum)
        // ЗАДАЧА 4: отсортируйте по убыванию суммы (OrderByDescending)
        // ЗАДАЧА 5: оставьте первые topCount записей через Take
        // ЗАДАЧА 6: спроецируйте в строку $"{имя} — {сумма:0.00} ₽"
        // ручная агрегация в переменных через foreach не засчитывается
        return customers.Join(orders,
                c => c.Name,
                o => o.CustomerName,
                (c, o) => new { c.Name, o.Price })
            .GroupBy(x => x.Name)
            .Select(g => new { Name = g.Key, Total = g.Sum(x => x.Price) })
            .OrderByDescending(x => x.Total)
            .Take(topCount)
            .Select(x => $"{x.Name} - {x.Total:0.00}р")
            .ToList();
    }
    // ЗАДАЧА: посчитайте общую выручку по заказам заданного года одним LINQ-вызовом
    // подсказка: orders.Where(o => o.CreatedAt.Year == 2026).Sum(o => o.Price)
    public decimal RevenueByYear(IEnumerable<Order> orders, int year)
    {
        return orders.Where(o => o.CreatedAt.Year == year).Sum(o => o.Price);
    }
    // ЗАДАЧА: найдите клиента с наибольшим числом заказов
    // подсказка: GroupBy → OrderByDescending(g => g.Count()) → Select(g => g.Key) → FirstOrDefault()
    public string? MostActiveCustomer(IEnumerable<Order> orders)
    {
        return orders.GroupBy(o => o.CustomerName)
            .OrderByDescending(g => g.Count())
            .Select(g => g.Key)
            .FirstOrDefault();
    }
    // ЗАДАЧА: выведите заказы, у которых сумма выше средней по всем заказам
    // подсказка: сначала посчитайте среднее отдельным запросом, затем Where
    public void PrintAboveAverage(IEnumerable<Order> orders)
    {
        if (!orders.Any()) return;

        decimal average = orders.Average(o => o.Price);
        foreach (var order in orders.Where(o => o.Price > average))
            Console.WriteLine(order);
    }

}
